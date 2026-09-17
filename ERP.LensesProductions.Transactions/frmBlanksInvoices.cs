using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Clinics;
using BusinessLayer.General;
using BusinessLayer.LensesProductions;
using BusinessLayer.POS;
using BusinessLayer.Privilege;
using BusinessLayer.StockControl;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Classes.DirectPrinting;
using ERP.POS.MasterData;
using ERP.Properties;
using ERP.StockControl.MasterData;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.LensesProductions.Transactions;

public class frmBlanksInvoices : frmHeaderManyDetails
{
	private InputLanguage Language;

	private DataTable dtDoctors;

	private DataTable dtBlankAdditions;

	private DataTable dtBlankTypes;

	private DataTable dtInvoiceAdditions;

	private DataTable dtTaxes;

	private DataTable dtInvoicePayments;

	private DataTable dtUsers;

	private DataTable dtVisaType;

	private DataTable dtReports;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtClients;

	private DataTable dtSalesMan;

	private DataTable dtSalesMan1;

	private DataTable dtSalesMan2;

	private DataTable dtLines;

	private DataTable dtBranches;

	private DataTable dtBlankTypePrices;

	private DataTable dtBlankAdditionPrices;

	private DataTable dtPriceType;

	private DataTable dtPOSDefaultData;

	private DataTable dtShiftDetails;

	private DataTable dtCurrency;

	private string ManufacturingClientAccountID = "";

	private ValueList vlGlassesType = new ValueList();

	private ValueList vlBlankAdditions = new ValueList();

	private ValueList vlBlankTypes = new ValueList();

	private ValueList vlInvoiceDetailsTaxes = new ValueList();

	private ValueList vlInvoiceAdditionTaxes = new ValueList();

	private ValueList vlDColors = new ValueList();

	private ValueList vlDSizes = new ValueList();

	private ValueList vlRColors = new ValueList();

	private ValueList vlRSizes = new ValueList();

	private ValueList vlUsers = new ValueList();

	private ValueList vlVisaType = new ValueList();

	private ValueList vlEye = new ValueList();

	private decimal ClientsGlassesHistoryID = default(decimal);

	private string ShiftDetailID;

	private string ShiftDetailUserID;

	private int NewPriceUserID = 0;

	private int DiscountUserID = 0;

	private decimal UserSalesDiscount = default(decimal);

	private bool UsingSalesDiscountLevels = false;

	private bool fromApprovalForm = false;

	private IContainer components = null;

	private UltraLabel lblCommercialTax;

	private UltraTextEditor txtCommercialTax;

	private UltraLabel lblDiscAfterTaxRatio;

	private UltraTextEditor txtDiscAfterTaxRatio;

	private UltraLabel lblDiscAfterTaxValue;

	private UltraTextEditor txtDiscAfterTaxValue;

	private UltraLabel lblTaxTotalValue;

	private UltraTextEditor txtTaxTotalValue;

	private UltraLabel lblDiscBeforeTaxRatio;

	private UltraTextEditor txtDiscBeforeTaxRatio;

	private UltraLabel lblDiscBeforeTaxValue;

	private UltraTextEditor txtDiscBeforeTaxValue;

	private UltraLabel lblGrossValue;

	private UltraTextEditor txtGrossValue;

	private UltraLabel lblNetPrice;

	private UltraTextEditor txtNetprice;

	private UltraLabel lblPaidAmount;

	private UltraTextEditor txtPaidAmount;

	private UltraTextEditor txtRestAmount;

	private UltraLabel lblRestAmount;

	public UltraButton btnPriceTypeSearch;

	private UltraLabel lblPriceType;

	private UltraComboEditor cboPriceType;

	public UltraButton btnClientSearch;

	private UltraLabel lblClient;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraComboEditor cboClient;

	private UltraComboEditor cboTax;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraCheckEditor chkTax;

	public UltraButton btnClientBalance;

	private UltraComboEditor cboClientCode;

	private UltraLabel lblBalance;

	private UltraTextEditor txtBranchBalance;

	private UltraTextEditor txtTotalQty;

	private UltraLabel ultraLabel1;

	private UltraLabel lblShiftNo;

	private UltraTextEditor txtShiftNo;

	private UltraLabel lblShiftDate;

	private UltraDateTimeEditor dtpShiftDate;

	private UltraComboEditor cboBranches;

	private UltraLabel lblSalesMan;

	private UltraComboEditor cboSalesMan;

	public UltraButton btnSalesManSearch;

	private UltraTextEditor txtClientCardNo;

	private UltraLabel lblClientCard;

	private UltraLabel lblTravelNo;

	private UltraComboEditor cboTravelNo;

	private UltraLabel lblBranches;

	public UltraButton btnBranchesSearch;

	public UltraButton btnSalesMan2Search;

	private UltraLabel lblSalesMan2;

	private UltraComboEditor cboSalesMan2;

	private UltraCheckEditor chkApplyTax;

	private UltraComboEditor cboLine;

	private UltraLabel lblLine;

	public UltraButton btnLineSearch;

	private UltraTabPageControl ultraTabPageControl2;

	private UltraTabPageControl ultraTabPageControl3;

	protected internal UltraGrid ULGDataAdditions;

	protected internal UltraGrid ULGDataPayments;

	private UltraComboEditor cboVisaType;

	private UltraCheckEditor chkVisa;

	private UltraTextEditor txtVisaNo;

	private UltraLabel lblVisaNo;

	private UltraDateTimeEditor dtpDeliverdDate;

	private UltraCheckEditor chkIsDeliverd;

	private UltraTextEditor txtVisionTestClient;

	private UltraLabel ultraLabel2;

	private UltraGroupBox UGBGlassesHistory;

	public UltraButton btnLTranspose;

	public UltraButton btnRTranspose;

	private UltraDateTimeEditor dtpGlassesHistoryDate;

	private UltraLabel lblDoctor;

	private UltraLabel ultraLabel3;

	private UltraComboEditor cboDoctor;

	public UltraButton btnAddHistory;

	private UltraLabel lblR;

	private UltraComboEditor cboRColorDistance;

	private UltraLabel lblRSph;

	private UltraLabel lblDistance;

	private UltraTextEditor txtRAdd;

	private UltraLabel lblRAxis;

	private UltraLabel lblRCyl;

	private UltraTextEditor txtRAxisDistance;

	private UltraComboEditor cboRSizeDistance;

	private UltraComboEditor cboRColorReading;

	private UltraTextEditor txtRAxisReading;

	private UltraComboEditor cboRSizeReading;

	private UltraLabel lblRAdd;

	private UltraLabel lblL;

	private UltraComboEditor cboLColorDistance;

	private UltraLabel lblLSph;

	private UltraTextEditor txtLAdd;

	private UltraLabel lblLAxis;

	private UltraLabel lblLCyl;

	private UltraTextEditor txtLAxisDistance;

	private UltraComboEditor cboLSizeDistance;

	private UltraComboEditor cboLColorReading;

	private UltraTextEditor txtLAxisReading;

	private UltraComboEditor cboLSizeReading;

	private UltraLabel lblLAdd;

	private UltraLabel lblReading;

	private UltraLabel lblIPD;

	private UltraLabel lblmmDistance;

	private UltraLabel lblIPDDistance;

	private UltraLabel lblmmReading;

	private UltraLabel lblIPDReading;

	private UltraTextEditor txtIPDReading;

	private UltraTextEditor txtIPDDistance;

	public frmBlanksInvoices()
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
		InitializeComponent();
		TableName = "Lns_BlanksInvoices";
		IDCol = "BlankInvoiceID";
		NoCol = "BlankInvoiceNo";
		DateCol = "BlankInvoiceDate";
	}

	public frmBlanksInvoices(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public frmBlanksInvoices(int ID, bool _fromApprovalForm)
		: this()
	{
		RowID = ID.ToString();
		fromApprovalForm = _fromApprovalForm;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		AutoPrint = false;
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtBlankAdditions = BlanksAdditions.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlBlankAdditions.ValueListItems.Clear();
		for (int i = 0; i < dtBlankAdditions.Rows.Count; i++)
		{
			vlBlankAdditions.ValueListItems.Add(dtBlankAdditions.Rows[i]["BlankAdditionID"], dtBlankAdditions.Rows[i]["BlankAdditionName"].ToString());
		}
		dtDoctors = Doctors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDoctor, dtDoctors, "DoctorID", "DoctorName");
		vlGlassesType.ValueListItems.Clear();
		vlGlassesType.ValueListItems.Add((object)1, GlobalVariables.IsArabic ? "قراءة" : "Reading");
		vlGlassesType.ValueListItems.Add((object)2, GlobalVariables.IsArabic ? "مسافات" : "Distance");
		vlGlassesType.ValueListItems.Add((object)3, GlobalVariables.IsArabic ? "شمس" : "Sun");
		vlGlassesType.ValueListItems.Add((object)4, GlobalVariables.IsArabic ? "مالتي فوكال" : "Multi-Focal");
		vlGlassesType.ValueListItems.Add((object)5, GlobalVariables.IsArabic ? "باي فوكال" : "Bi-Focal");
		vlGlassesType.ValueListItems.Add((object)6, GlobalVariables.IsArabic ? "باي فوكال مخفي" : "Invisible Bi-Focal");
		dtBlankTypes = BlanksTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlBlankTypes.ValueListItems.Clear();
		for (int j = 0; j < dtBlankTypes.Rows.Count; j++)
		{
			vlBlankTypes.ValueListItems.Add(dtBlankTypes.Rows[j]["BlankTypeID"], dtBlankTypes.Rows[j]["BlankTypeName"].ToString());
		}
		dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboRColorDistance, dtColors, "ColorID", "ColorName");
		GlobalFunctions.FillCombo(cboRColorReading, dtColors, "ColorID", "ColorName");
		GlobalFunctions.FillCombo(cboLColorDistance, dtColors, "ColorID", "ColorName");
		GlobalFunctions.FillCombo(cboLColorReading, dtColors, "ColorID", "ColorName");
		vlDColors.ValueListItems.Clear();
		vlRColors.ValueListItems.Clear();
		for (int k = 0; k < dtColors.Rows.Count; k++)
		{
			vlDColors.ValueListItems.Add(dtColors.Rows[k]["ColorID"], dtColors.Rows[k]["ColorName"].ToString());
			vlRColors.ValueListItems.Add(dtColors.Rows[k]["ColorID"], dtColors.Rows[k]["ColorName"].ToString());
		}
		dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboRSizeDistance, dtSizes, "ItemSizeID", "ItemSizeName");
		GlobalFunctions.FillCombo(cboRSizeReading, dtSizes, "ItemSizeID", "ItemSizeName");
		GlobalFunctions.FillCombo(cboLSizeDistance, dtSizes, "ItemSizeID", "ItemSizeName");
		GlobalFunctions.FillCombo(cboLSizeReading, dtSizes, "ItemSizeID", "ItemSizeName");
		vlDSizes.ValueListItems.Clear();
		vlRSizes.ValueListItems.Clear();
		for (int l = 0; l < dtSizes.Rows.Count; l++)
		{
			vlDSizes.ValueListItems.Add(dtSizes.Rows[l]["ItemSizeID"], dtSizes.Rows[l]["ItemSizeName"].ToString());
			vlRSizes.ValueListItems.Add(dtSizes.Rows[l]["ItemSizeID"], dtSizes.Rows[l]["ItemSizeName"].ToString());
		}
		UserSalesDiscount = Users.SelectSalesDiscount(GlobalVariables.UserID, IsFromServer: false);
		UsingSalesDiscountLevels = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from G_DiscountSettings ").Rows[0][0].ToString()) > 0;
		vlEye.ValueListItems.Clear();
		vlEye.ValueListItems.Add((object)0, GlobalVariables.IsArabic ? "L" : "L");
		vlEye.ValueListItems.Add((object)1, GlobalVariables.IsArabic ? "R" : "R");
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUsers.ValueListItems.Clear();
		for (int m = 0; m < dtUsers.Rows.Count; m++)
		{
			vlUsers.ValueListItems.Add(dtUsers.Rows[m]["User_ID"], dtUsers.Rows[m]["UserName"].ToString());
		}
		dtVisaType = VisaTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboVisaType, dtVisaType, "VisaTypeID", "VisaTypeName");
		vlVisaType.ValueListItems.Clear();
		for (int n = 0; n < dtVisaType.Rows.Count; n++)
		{
			vlVisaType.ValueListItems.Add(dtVisaType.Rows[n]["VisaTypeID"], dtVisaType.Rows[n]["VisaTypeName"].ToString());
		}
		dtPOSDefaultData = BusinessLayer.POS.Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		if (!fromApprovalForm)
		{
			if (dtPOSDefaultData.Rows.Count == 0)
			{
				GlobalVariables.InformationMB.Show("حساب التصنيع غير معرف فى الاعدادات ", "Manufacturing Account Not Defined in Settings");
				return;
			}
			ManufacturingClientAccountID = dtPOSDefaultData.Rows[0]["ManufacturingClientAccountID"].ToString();
		}
		if (ManufacturingClientAccountID != "" || fromApprovalForm)
		{
			dtClients = SubAccounts.FillComboByAccountID_LensesProdcution(fromApprovalForm ? "-1" : ManufacturingClientAccountID, "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "Name");
			GlobalFunctions.FillCombo(cboClientCode, dtClients, "SubAccountID", "ClientSupplierNo");
			dtSalesMan = SubAccounts.SelectBySubAccountTypeIDs(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboSalesMan, dtSalesMan, "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboSalesMan2, dtSalesMan, "SubAccountID", "SubAccountName");
			dtLines = Lines.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboLine, dtLines, "LineID", "LineName");
			dtBranches = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboBranches, dtBranches, "BranchID", "BranchName");
			dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
			cboTravelNo.Items.Clear();
			cboTravelNo.Items.Add((object)1, "1");
			cboTravelNo.Items.Add((object)2, "2");
			cboTravelNo.Items.Add((object)3, "3");
			cboTravelNo.Items.Add((object)4, "4");
			cboTravelNo.Items.Add((object)5, "5");
			cboTravelNo.Items.Add((object)6, "6");
			cboTravelNo.Items.Add((object)7, "7");
			cboTravelNo.Items.Add((object)8, "8");
			cboTravelNo.Items.Add((object)9, "9");
			cboTravelNo.Items.Add((object)10, "10");
			dtTaxes = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlInvoiceDetailsTaxes.ValueListItems.Clear();
			vlInvoiceAdditionTaxes.ValueListItems.Clear();
			for (int num = 0; num < dtTaxes.Rows.Count; num++)
			{
				vlInvoiceDetailsTaxes.ValueListItems.Add(dtTaxes.Rows[num]["TaxID"], dtTaxes.Rows[num]["TaxName"].ToString());
				vlInvoiceAdditionTaxes.ValueListItems.Add(dtTaxes.Rows[num]["TaxID"], dtTaxes.Rows[num]["TaxName"].ToString());
			}
			dtDetails = BlanksInvoicesDetails.SelectByBlankInvoiceID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtInvoiceAdditions = BlanksInvoicesAdditions.SelectByBlankInvoiceID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtInvoicePayments = BlanksInvoicesPayments.SelectByBlankInvoiceID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			((UltraGridBase)ULGData).DataSource = dtDetails;
			((UltraGridBase)ULGDataAdditions).DataSource = dtInvoiceAdditions;
			((UltraGridBase)ULGDataPayments).DataSource = dtInvoicePayments;
			((UltraGridBase)ULGDataPayments).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
			((UltraGridBase)ULGDataPayments).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
			((UltraGridBase)ULGDataPayments).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
			InitGrid();
		}
		else
		{
			GlobalVariables.InformationMB.Show("حساب التصنيع غير معرف فى الاعدادات ", "Manufacturing Account Not Defined in Settings");
		}
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = BlanksInvoices.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
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
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Expected O, but got Unknown
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Expected O, but got Unknown
		//IL_092b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0935: Expected O, but got Unknown
		//IL_0943: Unknown result type (might be due to invalid IL or missing references)
		//IL_094d: Expected O, but got Unknown
		base.DisplayData();
		if (drMaster != null)
		{
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["BlankInvoiceNo"].ToString();
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			dtpDate.Value = (DateTime)drMaster["BlankInvoiceDate"];
			dtpDate.ValueChanged += dtpDate_ValueChanged;
			((TextEditorControlBase)cboBranches).Value = drMaster["SubAccountBranchID"];
			((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
			((TextEditorControlBase)cboClient).Value = drMaster["SubAccountID"];
			((TextEditorControlBase)txtVisionTestClient).Value = drMaster["ClientName"];
			ClientsGlassesHistoryID = ((drMaster["ClientsGlassesHistoryID"] != DBNull.Value) ? int.Parse(drMaster["ClientsGlassesHistoryID"].ToString()) : 0);
			((TextEditorControlBase)cboLine).Value = drMaster["LineID"];
			FillClientGlassesHistory(ClientsGlassesHistoryID, ApplyOnDetails: false);
			((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
			((TextEditorControlBase)cboClientCode).ValueChanged -= cboClientCode_ValueChanged;
			((TextEditorControlBase)cboClientCode).Value = drMaster["SubAccountID"];
			((TextEditorControlBase)cboClientCode).ValueChanged += cboClientCode_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			ULGDataAdditions.AfterCellUpdate -= new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
			((TextEditorControlBase)cboPriceType).ValueChanged -= cboPriceType_ValueChanged;
			((TextEditorControlBase)cboPriceType).Value = drMaster["PriceTypeID"];
			((TextEditorControlBase)cboPriceType).ValueChanged += cboPriceType_ValueChanged;
			((TextEditorControlBase)cboSalesMan).Value = drMaster["EmployeeID"];
			((TextEditorControlBase)cboSalesMan2).Value = drMaster["EmployeeID2"];
			((UltraToggleEditorBase)chkApplyTax).CheckedChanged -= chkApplyTax_CheckedChanged;
			((UltraToggleEditorBase)chkApplyTax).Checked = bool.Parse(drMaster["ApplyTax"].ToString());
			((UltraToggleEditorBase)chkApplyTax).CheckedChanged += chkApplyTax_CheckedChanged;
			((UltraToggleEditorBase)chkIsDeliverd).Checked = bool.Parse(drMaster["IsDeliverd"].ToString());
			dtpDeliverdDate.Value = ((drMaster["DeliverdDate"] == DBNull.Value) ? DBNull.Value : drMaster["DeliverdDate"]);
			((Control)(object)txtGrossValue).Text = decimal.Parse(drMaster["GrossValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTotalQty).Text = decimal.Parse(drMaster["TotalQty"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(drMaster["DiscountBeforeTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse(drMaster["DiscountbeforeTaxRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
			((Control)(object)txtTaxTotalValue).Text = decimal.Parse(drMaster["TaxTotalValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged -= txtDiscAfterTaxValue_ValueChanged;
			((Control)(object)txtDiscAfterTaxValue).Text = decimal.Parse(drMaster["DiscountAfterTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged += txtDiscAfterTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged -= txtDiscAfterTaxRatio_ValueChanged;
			((Control)(object)txtDiscAfterTaxRatio).Text = decimal.Parse(drMaster["DiscountAfterTaxRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged += txtDiscAfterTaxRatio_ValueChanged;
			((TextEditorControlBase)txtCommercialTax).ValueChanged -= txtAdditionalValues_ValueChanged;
			((Control)(object)txtCommercialTax).Text = decimal.Parse(drMaster["CommercialTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtCommercialTax).ValueChanged += txtAdditionalValues_ValueChanged;
			((TextEditorControlBase)txtPaidAmount).ValueChanged -= txtPaidAmount_ValueChanged;
			((Control)(object)txtPaidAmount).Text = decimal.Parse(drMaster["PaidAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtPaidAmount).ValueChanged += txtPaidAmount_ValueChanged;
			((TextEditorControlBase)txtRestAmount).ValueChanged -= txtAdditionalValues_ValueChanged;
			((Control)(object)txtRestAmount).Text = decimal.Parse(drMaster["RestAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtRestAmount).ValueChanged += txtAdditionalValues_ValueChanged;
			((Control)(object)txtNetprice).Text = decimal.Parse(drMaster["NetPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)cboTravelNo).Value = drMaster["TravelNo"];
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			dtShiftDetails = ShiftsDetails.SelectByShiftDetailID(drMaster["ShiftDetailID"].ToString());
			if (dtShiftDetails.Rows.Count > 0)
			{
				((Control)(object)txtShiftNo).Text = dtShiftDetails.Rows[0]["ShiftDetailNo"].ToString();
				dtpShiftDate.Value = (DateTime)dtShiftDetails.Rows[0]["StartDate"];
			}
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = BlanksInvoicesDetails.SelectByBlankInvoiceID(drMaster["BlankInvoiceID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtInvoiceAdditions = BlanksInvoicesAdditions.SelectByBlankInvoiceID(drMaster["BlankInvoiceID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtInvoicePayments = BlanksInvoicesPayments.SelectByBlankInvoiceID(drMaster["BlankInvoiceID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			((UltraGridBase)ULGData).DataSource = dtDetails;
			((UltraGridBase)ULGDataAdditions).DataSource = dtInvoiceAdditions;
			((UltraGridBase)ULGDataPayments).DataSource = dtInvoicePayments;
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
			CalculateGoss();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			ULGDataAdditions.AfterCellUpdate += new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
		}
		else
		{
			ClearControls();
		}
		((TextEditorControlBase)txtCode).Focus();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		GlobalFunctions.PrepareGrid(ULGDataPayments);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		GlobalFunctions.PrepareGrid(ULGDataAdditions);
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoiceDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankTypeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Addition"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MM"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Ax"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IPD"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PH"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GlassesTypeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsRightEye"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DColorID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DItemSizeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RColorID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RItemSizeID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Blank Type");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MM"].Header).Caption = "MM";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Addition"].Header).Caption = "Add";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Ax"].Header).Caption = "Ax";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IPD"].Header).Caption = "IPD";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PH"].Header).Caption = "PH";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحدة" : "Unit price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الاجمالي" : "Total Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Header).Caption = (GlobalVariables.IsArabic ? "الخصم" : "Discount");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Header).Caption = (GlobalVariables.IsArabic ? "الضريبة" : "Tax");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الضريبة" : "Tax Value");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الصافى" : "Net");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsRightEye"].Header).Caption = (GlobalVariables.IsArabic ? "Eye" : "Eye");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GlassesTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "النوع" : "Type");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DColorID"].Header).Caption = "D- " + GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DItemSizeID"].Header).Caption = "D- " + GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RColorID"].Header).Caption = "R- " + GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RItemSizeID"].Header).Caption = "R- " + GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DItemSizeID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RItemSizeID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Addition"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MM"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Ax"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IPD"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PH"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankTypeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsRightEye"].Width = (int)((double)((Control)(object)ULGData).Width * 0.03);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GlassesTypeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DColorID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DItemSizeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RColorID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RItemSizeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MM"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Addition"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Ax"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IPD"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PH"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankTypeID"].ValueList = (IValueList)(object)vlBlankTypes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].ValueList = (IValueList)(object)vlInvoiceDetailsTaxes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DColorID"].ValueList = (IValueList)(object)vlDColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DItemSizeID"].ValueList = (IValueList)(object)vlDSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RColorID"].ValueList = (IValueList)(object)vlRColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RItemSizeID"].ValueList = (IValueList)(object)vlRSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsRightEye"].ValueList = (IValueList)(object)vlEye;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GlassesTypeID"].ValueList = (IValueList)(object)vlGlassesType;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["BlankInvoicePaymentID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["BlankInvoicePaymentNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["BlankInvoicePaymentDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["TotalAmount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["BlankInvoicePaymentNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["BlankInvoicePaymentDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الفيزا" : "Visa Type");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الفيزا" : "Visa No");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["TotalAmount"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة" : "Amount");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].Header).Caption = (GlobalVariables.IsArabic ? "User" : "User");
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["BlankInvoicePaymentNo"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["BlankInvoicePaymentDate"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeID"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeNo"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["TotalAmount"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeID"].ValueList = (IValueList)(object)vlVisaType;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].ValueList = (IValueList)(object)vlUsers;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["TotalAmount"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["BlankInvoiceAdditionID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["BlankAdditionID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["Discount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["TaxID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["TaxValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["NetPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["BlankAdditionID"].Header).Caption = (GlobalVariables.IsArabic ? "إضافة التصنيع" : "Blank Addition");
		((HeaderBase)((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة" : "Amount");
		((HeaderBase)((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الاجمالى" : "Total");
		((HeaderBase)((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["Discount"].Header).Caption = (GlobalVariables.IsArabic ? "الخصم" : "Discount");
		((HeaderBase)((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["TaxID"].Header).Caption = (GlobalVariables.IsArabic ? "الضريبة" : "Tax");
		((HeaderBase)((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["TaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الضريبة" : "Tax Value");
		((HeaderBase)((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["NetPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الصافى" : "Net");
		((HeaderBase)((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["BlankAdditionID"].Hidden = false;
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["Discount"].Hidden = false;
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["TaxID"].Hidden = false;
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["TaxValue"].Hidden = false;
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["NetPrice"].Hidden = false;
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["BlankAdditionID"].ValueList = (IValueList)(object)vlBlankAdditions;
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["TaxID"].ValueList = (IValueList)(object)vlInvoiceAdditionTaxes;
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["Discount"].DefaultCellValue = 0;
	}

	public override void btnCopyToClick()
	{
		if (!CanAdd)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		Adding = true;
		((UltraToggleEditorBase)chkIsDeliverd).Checked = false;
		dtpDeliverdDate.DateTime = dtpDate.DateTime;
		drMaster = null;
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		dtpDate.ValueChanged += dtpDate_ValueChanged;
		SetControls(NavMode: false);
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)txtCode).ReadOnly = Adding;
		((EditorButtonControlBase)dtpDate).ReadOnly = true;
		((Control)(object)chkApplyTax).Visible = dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["AllowEditTax"].ToString());
		((EditorButtonControlBase)cboClient).ReadOnly = NavMode || (cboBranches.SelectedIndex > -1 && ((TextEditorControlBase)cboBranches).Value.ToString() != GlobalVariables.CurrentBranchID && !ViewAllBranches);
		((EditorButtonControlBase)cboClientCode).ReadOnly = NavMode || (cboBranches.SelectedIndex > -1 && ((TextEditorControlBase)cboBranches).Value.ToString() != GlobalVariables.CurrentBranchID && !ViewAllBranches);
		((Control)(object)lblClientCard).Visible = UsingSalesDiscountLevels;
		((Control)(object)txtClientCardNo).Visible = UsingSalesDiscountLevels;
		((EditorButtonControlBase)cboBranches).ReadOnly = !ViewAllBranches;
		((EditorButtonControlBase)cboPriceType).ReadOnly = !CanModifyPriceType;
		((EditorButtonControlBase)cboSalesMan).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSalesMan2).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)btnAddHistory).Visible = !NavMode;
		((EditorButtonControlBase)txtVisionTestClient).ReadOnly = NavMode;
		((Control)(object)btnRTranspose).Visible = !NavMode;
		((Control)(object)btnLTranspose).Visible = !NavMode;
		((EditorButtonControlBase)txtDiscBeforeTaxValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscBeforeTaxRatio).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscAfterTaxValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscAfterTaxRatio).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCommercialTax).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPaidAmount).ReadOnly = true;
		((Control)(object)txtBranchBalance).Visible = !NavMode;
		((Control)(object)lblBalance).Visible = !NavMode;
		((Control)(object)btnClientSearch).Visible = !NavMode;
		((Control)(object)btnClientBalance).Visible = !NavMode;
		((Control)(object)btnPriceTypeSearch).Visible = !NavMode && !CanModifyPriceType;
		((Control)(object)btnBranchesSearch).Visible = !NavMode;
		((Control)(object)btnSalesManSearch).Visible = (Adding || Updating) && cboLine.SelectedIndex == -1;
		((Control)(object)btnSalesMan2Search).Visible = (Adding || Updating) && cboLine.SelectedIndex == -1;
		NewPriceUserID = 0;
		DiscountUserID = 0;
		((EditorButtonControlBase)cboTravelNo).ReadOnly = NavMode;
		((Control)(object)dtpShiftDate).Visible = !Adding;
		((Control)(object)txtShiftNo).Visible = !Adding;
		((Control)(object)lblShiftDate).Visible = !Adding;
		((Control)(object)lblShiftNo).Visible = !Adding;
		((UltraToggleEditorBase)chkVisa).Checked = false;
		((Control)(object)chkVisa).Visible = Adding;
		((Control)(object)cboVisaType).Visible = Adding;
		((Control)(object)lblVisaNo).Visible = Adding;
		((Control)(object)txtVisaNo).Visible = Adding;
		object value = ((TextEditorControlBase)cboSalesMan).Value;
		object value2 = ((TextEditorControlBase)cboSalesMan2).Value;
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		if (Updating || Adding)
		{
			if ((Updating && drMaster != null && drMaster["LineID"] != DBNull.Value) || (Adding && cboLine.SelectedIndex > -1))
			{
				dtSalesMan1 = SubAccounts.SelectSalesMan1ByLineDate(GlobalVariables.EmployeeSubAccountTypeIDs, Adding ? ((TextEditorControlBase)cboLine).Value.ToString() : drMaster["LineID"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
				GlobalFunctions.FillCombo(cboSalesMan, dtSalesMan1, "SubAccountID", "SubAccountName");
				dtSalesMan2 = SubAccounts.SelectSalesMan2ByLineDate(GlobalVariables.EmployeeSubAccountTypeIDs, Adding ? ((TextEditorControlBase)cboLine).Value.ToString() : drMaster["LineID"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
				GlobalFunctions.FillCombo(cboSalesMan2, dtSalesMan2, "SubAccountID", "SubAccountName");
				((Control)(object)btnSalesMan2Search).Visible = false;
				((Control)(object)btnSalesManSearch).Visible = false;
			}
			else
			{
				GlobalFunctions.FillCombo(cboSalesMan, dtSalesMan, "SubAccountID", "SubAccountName");
				GlobalFunctions.FillCombo(cboSalesMan2, dtSalesMan, "SubAccountID", "SubAccountName");
			}
		}
		else
		{
			GlobalFunctions.FillCombo(cboSalesMan, dtSalesMan, "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboSalesMan2, dtSalesMan, "SubAccountID", "SubAccountName");
		}
		((TextEditorControlBase)cboSalesMan).Value = value;
		((TextEditorControlBase)cboSalesMan2).Value = value2;
		if (Adding)
		{
			ClearClientGlassesHistory();
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
	}

	public override void ClearControls()
	{
		base.ClearControls();
		NewPriceUserID = 0;
		DiscountUserID = 0;
		((TextEditorControlBase)txtCode).Clear();
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		dtpDate.ValueChanged += dtpDate_ValueChanged;
		((Control)(object)txtCode).Text = (Adding ? BlanksInvoices.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: true) : "");
		ClientsGlassesHistoryID = default(decimal);
		ClearClientGlassesHistory();
		((TextEditorControlBase)txtVisionTestClient).Clear();
		((TextEditorControlBase)cboPriceType).ValueChanged -= cboPriceType_ValueChanged;
		cboPriceType.SelectedIndex = -1;
		((TextEditorControlBase)cboPriceType).ValueChanged += cboPriceType_ValueChanged;
		((UltraToggleEditorBase)chkIsDeliverd).Checked = false;
		dtpDeliverdDate.DateTime = dtpDate.DateTime;
		cboSalesMan.SelectedIndex = -1;
		cboSalesMan2.SelectedIndex = -1;
		cboLine.SelectedIndex = -1;
		((UltraToggleEditorBase)chkTax).Checked = false;
		cboTax.SelectedIndex = -1;
		((UltraToggleEditorBase)chkApplyTax).Checked = dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["ApplySalesTax"].ToString());
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)txtBranchBalance).Clear();
		((Control)(object)txtGrossValue).Text = "0";
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
		((Control)(object)txtDiscBeforeTaxValue).Text = "0";
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
		((Control)(object)txtDiscBeforeTaxRatio).Text = "0";
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
		((Control)(object)txtTaxTotalValue).Text = "0";
		((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged -= txtDiscAfterTaxValue_ValueChanged;
		((Control)(object)txtDiscAfterTaxValue).Text = "0";
		((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged += txtDiscAfterTaxValue_ValueChanged;
		((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged -= txtDiscAfterTaxRatio_ValueChanged;
		((Control)(object)txtDiscAfterTaxRatio).Text = "0";
		((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged += txtDiscAfterTaxRatio_ValueChanged;
		((TextEditorControlBase)txtCommercialTax).ValueChanged -= txtAdditionalValues_ValueChanged;
		((Control)(object)txtCommercialTax).Text = "0";
		((TextEditorControlBase)txtCommercialTax).ValueChanged += txtAdditionalValues_ValueChanged;
		((TextEditorControlBase)txtPaidAmount).ValueChanged -= txtPaidAmount_ValueChanged;
		((Control)(object)txtPaidAmount).Text = "0";
		((TextEditorControlBase)txtPaidAmount).ValueChanged += txtPaidAmount_ValueChanged;
		((TextEditorControlBase)txtRestAmount).ValueChanged -= txtAdditionalValues_ValueChanged;
		((Control)(object)txtRestAmount).Text = "0";
		((TextEditorControlBase)txtRestAmount).ValueChanged += txtAdditionalValues_ValueChanged;
		((Control)(object)txtNetprice).Text = "0";
		((Control)(object)txtTotalQty).Text = "0";
		((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
		cboClient.SelectedIndex = -1;
		((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
		((TextEditorControlBase)cboBranches).Value = GlobalVariables.CurrentBranchID;
		if (dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["DefaultSubAccountID"] != DBNull.Value && Adding)
		{
			((TextEditorControlBase)cboClient).Value = dtPOSDefaultData.Rows[0]["DefaultSubAccountID"];
		}
		((TextEditorControlBase)cboTravelNo).Clear();
		((UltraToggleEditorBase)chkVisa).Checked = false;
		cboVisaType.SelectedIndex = -1;
		((Control)(object)txtVisaNo).Text = "";
		((DataTable)((UltraGridBase)ULGDataAdditions).DataSource).Rows.Clear();
		((DataTable)((UltraGridBase)ULGDataPayments).DataSource).Rows.Clear();
	}

	public override void CallButtons(KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1 && ((Control)(object)btnAdd).Enabled && ((Control)(object)btnAdd).Visible)
		{
			SendKeys.Send("{tab}");
			btnAddClick();
		}
		else if (e.KeyCode == Keys.F2 && ((Control)(object)btnUpdate).Enabled && ((Control)(object)btnUpdate).Visible)
		{
			btnUpdateClick();
		}
		else if (e.KeyCode == Keys.F3 && ((Control)(object)btnDelete).Enabled && ((Control)(object)btnDelete).Visible)
		{
			btnDeleteClick();
		}
		else if (e.KeyCode == Keys.F4 && ((Control)(object)btnPrint).Enabled && ((Control)(object)btnPrint).Visible)
		{
			btnPrint_Click(null, null);
		}
		else if (e.KeyCode == Keys.F5 && ((Control)(object)btnRefreshData).Enabled && ((Control)(object)btnRefreshData).Visible)
		{
			btnRefreshDataClick();
		}
		if (!Adding && !Updating)
		{
			if (e.KeyCode == Keys.F8 && ((Control)(object)btnSearch).Enabled && ((Control)(object)btnSearch).Visible)
			{
				btnSearch_Click(null, null);
			}
			else if (e.KeyValue == 39 && ((Control)(object)btnNext).Enabled && ((Control)(object)btnNext).Visible)
			{
				NextData();
			}
			else if (e.KeyValue == 37 && ((Control)(object)btnPriveous).Enabled && ((Control)(object)btnPriveous).Visible)
			{
				PriveousData();
			}
			else if (e.KeyCode == Keys.F7 && ((Control)(object)btnCopyTo).Enabled && ((Control)(object)btnCopyTo).Visible)
			{
				btnCopyToClick();
			}
		}
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
			if (bool.Parse(drMaster["IsJVCreated"].ToString()) && int.Parse(BlanksInvoices.CheckShiftDetailsClosedByBlankInvoiceID(drMaster["BlankInvoiceID"].ToString()).Rows[0]["Counter"].ToString()) > 0)
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لإنها تم إرسالها الى المعمل و لوجود قيد عليها", "Cannot Update This Transaction Because It Is Related To A Closed Shift And Already Has A JV. ");
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

	public void FastPrint()
	{
		DataTable dataTable = Main.SyncExecuteQuery_DataTable(" Rep_Lns_BlanksInvoices " + RowID + "," + (GlobalVariables.IsArabic ? "1" : "0"));
		DataTable dataTable2 = Main.ExecuteQuery_DataTable(" Rep_POS_Settings_SelectByBranchID " + GlobalVariables.CurrentBranchID + "," + (GlobalVariables.IsArabic ? "1" : "0"));
		DataTable dataTable3 = Main.SyncExecuteQuery_DataTable(" Rep_Lns_BlanksInvoicesByTax " + RowID + "," + (GlobalVariables.IsArabic ? "1" : "0"));
		Color black = Color.Black;
		Color darkBlue = Color.DarkBlue;
		Color gray = Color.Gray;
		float lineWidth = 0.03f;
		DataRow dataRow = dataTable.Rows[0];
		Font font = new Font("Times New Roman", 7f, FontStyle.Bold);
		Font font2 = new Font("Times New Roman", 7f, FontStyle.Bold);
		Font font3 = new Font("Times New Roman", 6f, FontStyle.Regular);
		Font font4 = new Font("Times New Roman", 7f, FontStyle.Regular);
		Font font5 = new Font("Times New Roman", 9f, FontStyle.Regular);
		Font font6 = new Font("Times New Roman", 7f, FontStyle.Bold);
		Font font7 = new Font("Times New Roman", 8f, FontStyle.Bold);
		Font font8 = new Font("Free 3 of 9 Extended", 18f, FontStyle.Regular);
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
		instance.AddEmptyCell((1f - 44f / instance.OverallWidth) / 2f, 1f, DrawRectangle: false);
		instance.AddTextCell(dataTable2.Rows[0]["CompanyName"].ToString(), font, 44f / instance.OverallWidth, 7f, StringAlignment.Center, black, DrawRectangle: false);
		instance.AddEmptyCell((1f - 44f / instance.OverallWidth) / 2f, 1f, DrawRectangle: false);
		instance.AcceptChanges();
		instance.AddEmptyCell(1f, 1f, DrawRectangle: false);
		instance.AcceptChanges();
		instance.AddTextCell("*" + dataRow["BlankInvoiceNo"].ToString() + "*", font8, 0.5f, 5f, StringAlignment.Far, black, DrawRectangle: true, gray, lineWidth);
		instance.AddTextCell(dataRow["BlankInvoiceNo"].ToString(), font7, 0.2f, 5f, StringAlignment.Far, black, DrawRectangle: true, gray, lineWidth);
		instance.AddTextCell("فاتورة مبيعات برقم ", font7, 0.3f, 5f, StringAlignment.Near, darkBlue, DrawRectangle: true, gray, lineWidth);
		instance.AcceptChanges();
		instance.AddTextCell(((DateTime)dataRow["BlankInvoiceDate"]).ToString("dd/MM/yyyy hh:mm:ss tt"), font7, 0.7f, 5f, StringAlignment.Far, black, DrawRectangle: true, gray, lineWidth);
		instance.AddTextCell("بتاريخ", font7, 0.3f, 5f, StringAlignment.Near, darkBlue, DrawRectangle: true, gray, lineWidth);
		instance.AcceptChanges();
		instance.AddTextCell(dataRow["BranchName"].ToString(), font7, 0.7f, 5f, StringAlignment.Near, black, DrawRectangle: true, gray, lineWidth);
		instance.AddTextCell("فرع", font7, 0.3f, 5f, StringAlignment.Near, darkBlue, DrawRectangle: true, gray, lineWidth);
		instance.AcceptChanges();
		instance.AddTextCell(dataRow["UserName"].ToString(), font7, 0.7f, 5f, StringAlignment.Near, black, DrawRectangle: true, gray, lineWidth);
		instance.AddTextCell("اسم البائع", font7, 0.3f, 5f, StringAlignment.Near, darkBlue, DrawRectangle: true, gray, lineWidth);
		instance.AcceptChanges();
		instance.AddTextCell(dataRow["ClientName"].ToString(), font7, 0.7f, 5f, StringAlignment.Near, black, DrawRectangle: true, gray, lineWidth);
		instance.AddTextCell("العميل", font7, 0.3f, 5f, StringAlignment.Near, darkBlue, DrawRectangle: true, gray, lineWidth);
		instance.AcceptChanges();
		if (dataRow["SalesManName"].ToString().Trim().Length > 0)
		{
			instance.AddTextCell(dataRow["SalesManName"].ToString(), font5, 0.7f, 5f, StringAlignment.Near, black, DrawRectangle: true, gray, lineWidth);
			instance.AddTextCell("اسم مندوب ", font5, 0.3f, 5f, StringAlignment.Near, darkBlue, DrawRectangle: true, gray, lineWidth);
			instance.AcceptChanges();
		}
		instance.AddEmptyCell(1f, 3f, DrawRectangle: false);
		instance.AcceptChanges();
		instance.AddTextCell("إجمالي", font2, 18f / instance.OverallWidth, 5f, StringAlignment.Center, darkBlue, DrawRectangle: true, gray, lineWidth);
		instance.AddTextCell("سعر الوحده", font2, 8f / instance.OverallWidth, 5f, StringAlignment.Center, darkBlue, DrawRectangle: true, gray, lineWidth);
		instance.AddTextCell("الوحده", font2, 8f / instance.OverallWidth, 5f, StringAlignment.Center, darkBlue, DrawRectangle: true, gray, lineWidth);
		instance.AddTextCell("الكميه", font2, 8f / instance.OverallWidth, 5f, StringAlignment.Center, darkBlue, DrawRectangle: true, gray, lineWidth);
		instance.AddTextCell("الصنف", font2, 28f / instance.OverallWidth, 5f, StringAlignment.Center, darkBlue, DrawRectangle: true, gray, lineWidth);
		instance.AcceptChanges();
		foreach (DataRow row in dataTable.Rows)
		{
			instance.AddTextCell(decimal.Parse(row["TotalPrice"].ToString(), NumberStyles.Currency).ToString("0.00"), font3, 18f / instance.OverallWidth, 4f, StringAlignment.Far, black, DrawRectangle: true, gray, lineWidth);
			instance.AddTextCell(decimal.Parse(row["UnitPrice"].ToString(), NumberStyles.Currency).ToString("0.00"), font3, 8f / instance.OverallWidth, 4f, StringAlignment.Far, black, DrawRectangle: true, gray, lineWidth);
			instance.AddTextCell(row["UnitName"].ToString(), font3, 8f / instance.OverallWidth, 4f, StringAlignment.Center, black, DrawRectangle: true, gray, lineWidth);
			instance.AddTextCell(decimal.Parse(row["Qty"].ToString(), NumberStyles.Float).ToString("0.##"), font3, 8f / instance.OverallWidth, 4f, StringAlignment.Far, black, DrawRectangle: true, gray, lineWidth);
			instance.AddTextCell(row["ItemName"].ToString(), font3, 28f / instance.OverallWidth, 4f, StringAlignment.Center, black, DrawRectangle: true, gray, lineWidth);
			instance.AcceptChanges();
		}
		instance.AddTextCell(decimal.Parse(dataRow["GrossValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 18f / instance.OverallWidth, 5f, StringAlignment.Far, black, DrawRectangle: true, gray, lineWidth);
		instance.AddTextCell("الإجمالي", font6, 8f / instance.OverallWidth, 5f, StringAlignment.Far, darkBlue, DrawRectangle: true, gray, lineWidth);
		instance.AddTextCell(decimal.Parse(((Control)(object)txtTotalQty).Text.ToString(), NumberStyles.Float).ToString("0.##"), font3, float.Parse((16f / instance.OverallWidth).ToString()), 5f, StringAlignment.Far, darkBlue, DrawRectangle: true, gray, lineWidth);
		instance.AddTextCell("إجمالي الكميه", font6, float.Parse((28f / instance.OverallWidth).ToString()), 5f, StringAlignment.Center, black, DrawRectangle: true, gray, lineWidth);
		instance.AcceptChanges();
		if (decimal.Parse(dataRow["DiscountBeforeTaxValue"].ToString()) != 0m)
		{
			instance.AddTextCell(decimal.Parse(dataRow["DiscountBeforeTaxValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 18f / instance.OverallWidth, 5f, StringAlignment.Far, black, DrawRectangle: true, gray, lineWidth);
			instance.AddTextCell("خصم قبل الضرائب", font6, 52f / instance.OverallWidth, 5f, StringAlignment.Far, darkBlue, DrawRectangle: true, gray, lineWidth);
			instance.AcceptChanges();
		}
		if (decimal.Parse(dataRow["TaxTotalValue"].ToString()) != 0m)
		{
			try
			{
				if (dataTable3 != null)
				{
					foreach (DataRow row2 in dataTable3.Rows)
					{
						instance.AddTextCell(decimal.Parse(row2["TaxValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font3, 18f / instance.OverallWidth, 4f, StringAlignment.Far, black, DrawRectangle: true, gray, lineWidth);
						instance.AddTextCell(row2["TaxName"].ToString(), font3, 52f / instance.OverallWidth, 4f, StringAlignment.Near, darkBlue, DrawRectangle: true, gray, lineWidth);
						instance.AcceptChanges();
					}
				}
			}
			catch
			{
			}
		}
		if (decimal.Parse(dataRow["DiscountAfterTaxValue"].ToString()) != 0m)
		{
			instance.AddTextCell(decimal.Parse(dataRow["DiscountAfterTaxValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 18f / instance.OverallWidth, 5f, StringAlignment.Far, black, DrawRectangle: true, gray, lineWidth);
			instance.AddTextCell("خصم بعد الضرائب", font6, 52f / instance.OverallWidth, 5f, StringAlignment.Far, darkBlue, DrawRectangle: true, gray, lineWidth);
			instance.AcceptChanges();
		}
		if (decimal.Parse(dataRow["CommercialTaxValue"].ToString()) != 0m)
		{
			instance.AddTextCell(decimal.Parse(dataRow["CommercialTaxValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 18f / instance.OverallWidth, 5f, StringAlignment.Far, black, DrawRectangle: true, gray, lineWidth);
			instance.AddTextCell("ضريبة ارباح تجارية وصناعية", font6, 52f / instance.OverallWidth, 5f, StringAlignment.Far, darkBlue, DrawRectangle: true, gray, lineWidth);
			instance.AcceptChanges();
		}
		if (decimal.Parse(dataRow["NetPrice"].ToString()) != 0m)
		{
			instance.AddTextCell(decimal.Parse(dataRow["NetPrice"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 18f / instance.OverallWidth, 5f, StringAlignment.Far, black, DrawRectangle: true, gray, lineWidth);
			instance.AddTextCell("صافى الفاتورة", font6, 52f / instance.OverallWidth, 5f, StringAlignment.Far, darkBlue, DrawRectangle: true, gray, lineWidth);
			instance.AcceptChanges();
		}
		if (decimal.Parse(dataRow["PaidAmount"].ToString()) != 0m)
		{
			instance.AddTextCell(decimal.Parse(dataRow["PaidAmount"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 18f / instance.OverallWidth, 5f, StringAlignment.Far, black, DrawRectangle: true, gray, lineWidth);
			instance.AddTextCell("المدفوع", font6, 52f / instance.OverallWidth, 5f, StringAlignment.Far, darkBlue, DrawRectangle: true, gray, lineWidth);
			instance.AcceptChanges();
		}
		if (decimal.Parse(dataRow["RestAmount"].ToString()) != 0m)
		{
			instance.AddTextCell(decimal.Parse(dataRow["RestAmount"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 18f / instance.OverallWidth, 5f, StringAlignment.Far, black, DrawRectangle: true, gray, lineWidth);
			instance.AddTextCell("المتبقى", font6, 52f / instance.OverallWidth, 5f, StringAlignment.Far, darkBlue, DrawRectangle: true, gray, lineWidth);
			instance.AcceptChanges();
		}
		instance.AddTextCell(decimal.Parse(dataRow["CurrentBalance"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 42f / instance.OverallWidth, 5f, StringAlignment.Far, black, DrawRectangle: true, gray, lineWidth);
		instance.AddTextCell("الرصيد عند الحالى بالفرع", font6, 28f / instance.OverallWidth, 5f, StringAlignment.Center, darkBlue, DrawRectangle: true, gray, lineWidth);
		instance.AcceptChanges();
		if (decimal.Parse(dataRow["TaxTotalValue"].ToString()) != 0m)
		{
			instance.AddTextCell(dataTable2.Rows[0]["TaxNo"].ToString(), font3, 0.7f, 6f, StringAlignment.Far, black, DrawRectangle: false, gray, lineWidth);
			instance.AddTextCell("الرقم الضريبي : ", font5, 0.3f, 5f, StringAlignment.Near, darkBlue, DrawRectangle: false, gray, lineWidth);
			instance.AcceptChanges();
		}
		instance.AddTextCell(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), font3, 1f, 5f, StringAlignment.Center, Color.Black, DrawRectangle: false);
		instance.AcceptChanges();
		if (dataTable2.Rows[0]["Message"].ToString().Trim().Length > 0)
		{
			instance.AddTextCell(dataTable2.Rows[0]["Message"].ToString().Trim(), font2, 1f, float.Parse((Math.Ceiling((double)dataTable2.Rows[0]["Message"].ToString().Trim().Length / 85.0) * 3.0).ToString()), StringAlignment.Center, Color.Black, DrawRectangle: false);
			instance.AcceptChanges();
		}
		instance.AddTextCell("Powered by Future Solutions : www.fs-scs.com", font3, 1f, 6f, StringAlignment.Center, Color.Black, DrawRectangle: false);
		instance.AcceptChanges();
		instance.PrinterSettings.Copies = 1;
		instance.Print();
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
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Lns_BlanksInvoices_A.rpt" : "Rep_Lns_BlanksInvoices_E.rpt"));
			}
			GlobalVariables.IsRepOnlineConn = true;
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@BlankInvoiceID", RowID);
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.BlanksInvoicesReport(-1, 0, -1, -1, -1, IsFromServer: true);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["BlankInvoiceID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		object value = ((TextEditorControlBase)cboDoctor).Value;
		object value2 = ((TextEditorControlBase)cboRColorDistance).Value;
		object value3 = ((TextEditorControlBase)cboRColorReading).Value;
		object value4 = ((TextEditorControlBase)cboLColorDistance).Value;
		object value5 = ((TextEditorControlBase)cboLColorReading).Value;
		object value6 = ((TextEditorControlBase)cboRSizeDistance).Value;
		object value7 = ((TextEditorControlBase)cboRSizeReading).Value;
		object value8 = ((TextEditorControlBase)cboLSizeDistance).Value;
		object value9 = ((TextEditorControlBase)cboLSizeReading).Value;
		object value10 = ((TextEditorControlBase)cboVisaType).Value;
		object value11 = ((TextEditorControlBase)cboSalesMan).Value;
		object value12 = ((TextEditorControlBase)cboSalesMan2).Value;
		object value13 = ((TextEditorControlBase)cboLine).Value;
		object value14 = ((TextEditorControlBase)cboBranches).Value;
		object value15 = ((TextEditorControlBase)cboPriceType).Value;
		dtBlankAdditions = BlanksAdditions.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlBlankAdditions.ValueListItems.Clear();
		for (int i = 0; i < dtBlankAdditions.Rows.Count; i++)
		{
			vlBlankAdditions.ValueListItems.Add(dtBlankAdditions.Rows[i]["BlankAdditionID"], dtBlankAdditions.Rows[i]["BlankAdditionName"].ToString());
		}
		dtDoctors = Doctors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDoctor, dtDoctors, "DoctorID", "DoctorName");
		dtBlankTypes = BlanksTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlBlankTypes.ValueListItems.Clear();
		for (int j = 0; j < dtBlankTypes.Rows.Count; j++)
		{
			vlBlankTypes.ValueListItems.Add(dtBlankTypes.Rows[j]["BlankTypeID"], dtBlankTypes.Rows[j]["BlankTypeName"].ToString());
		}
		dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboRColorDistance, dtColors, "ColorID", "ColorName");
		GlobalFunctions.FillCombo(cboRColorReading, dtColors, "ColorID", "ColorName");
		GlobalFunctions.FillCombo(cboLColorDistance, dtColors, "ColorID", "ColorName");
		GlobalFunctions.FillCombo(cboLColorReading, dtColors, "ColorID", "ColorName");
		vlDColors.ValueListItems.Clear();
		vlRColors.ValueListItems.Clear();
		for (int k = 0; k < dtColors.Rows.Count; k++)
		{
			vlDColors.ValueListItems.Add(dtColors.Rows[k]["ColorID"], dtColors.Rows[k]["ColorName"].ToString());
			vlRColors.ValueListItems.Add(dtColors.Rows[k]["ColorID"], dtColors.Rows[k]["ColorName"].ToString());
		}
		dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboRSizeDistance, dtSizes, "ItemSizeID", "ItemSizeName");
		GlobalFunctions.FillCombo(cboRSizeReading, dtSizes, "ItemSizeID", "ItemSizeName");
		GlobalFunctions.FillCombo(cboLSizeDistance, dtSizes, "ItemSizeID", "ItemSizeName");
		GlobalFunctions.FillCombo(cboLSizeReading, dtSizes, "ItemSizeID", "ItemSizeName");
		vlDSizes.ValueListItems.Clear();
		vlRSizes.ValueListItems.Clear();
		for (int l = 0; l < dtSizes.Rows.Count; l++)
		{
			vlDSizes.ValueListItems.Add(dtSizes.Rows[l]["ItemSizeID"], dtSizes.Rows[l]["ItemSizeName"].ToString());
			vlRSizes.ValueListItems.Add(dtSizes.Rows[l]["ItemSizeID"], dtSizes.Rows[l]["ItemSizeName"].ToString());
		}
		dtPOSDefaultData = BusinessLayer.POS.Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		if (Adding)
		{
			DataView dataView = new DataView(dtVisaType);
			dataView.RowFilter = " IsActive =1 ";
			DataTable dataTable = dataView.ToTable();
			GlobalFunctions.FillCombo(cboVisaType, dataTable, "VisaTypeID", "VisaTypeName");
			vlVisaType.ValueListItems.Clear();
			for (int m = 0; m < dataTable.Rows.Count; m++)
			{
				vlVisaType.ValueListItems.Add(dataTable.Rows[m]["VisaTypeID"], dataTable.Rows[m]["VisaTypeName"].ToString());
			}
		}
		else
		{
			GlobalFunctions.FillCombo(cboVisaType, dtVisaType, "VisaTypeID", "VisaTypeName");
			vlVisaType.ValueListItems.Clear();
			for (int n = 0; n < dtVisaType.Rows.Count; n++)
			{
				vlVisaType.ValueListItems.Add(dtVisaType.Rows[n]["VisaTypeID"], dtVisaType.Rows[n]["VisaTypeName"].ToString());
			}
		}
		dtSalesMan = SubAccounts.SelectBySubAccountTypeIDs(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSalesMan, dtSalesMan, "SubAccountID", "SubAccountName");
		GlobalFunctions.FillCombo(cboSalesMan2, dtSalesMan, "SubAccountID", "SubAccountName");
		dtLines = Lines.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboLine, dtLines, "LineID", "LineName");
		dtTaxes = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInvoiceDetailsTaxes.ValueListItems.Clear();
		vlInvoiceAdditionTaxes.ValueListItems.Clear();
		for (int num = 0; num < dtTaxes.Rows.Count; num++)
		{
			vlInvoiceDetailsTaxes.ValueListItems.Add(dtTaxes.Rows[num]["TaxID"], dtTaxes.Rows[num]["TaxName"].ToString());
			vlInvoiceAdditionTaxes.ValueListItems.Add(dtTaxes.Rows[num]["TaxID"], dtTaxes.Rows[num]["TaxName"].ToString());
		}
		dtBranches = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBranches, dtBranches, "BranchID", "BranchName");
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
		((TextEditorControlBase)cboDoctor).Value = value;
		((TextEditorControlBase)cboRColorDistance).Value = value2;
		((TextEditorControlBase)cboRColorReading).Value = value3;
		((TextEditorControlBase)cboLColorDistance).Value = value4;
		((TextEditorControlBase)cboLColorReading).Value = value5;
		((TextEditorControlBase)cboRSizeDistance).Value = value6;
		((TextEditorControlBase)cboRSizeReading).Value = value7;
		((TextEditorControlBase)cboLSizeDistance).Value = value8;
		((TextEditorControlBase)cboLSizeReading).Value = value9;
		((TextEditorControlBase)cboVisaType).Value = value10;
		((TextEditorControlBase)cboSalesMan).Value = value11;
		((TextEditorControlBase)cboSalesMan2).Value = value12;
		((TextEditorControlBase)cboLine).Value = value13;
		((TextEditorControlBase)cboBranches).Value = value14;
		((TextEditorControlBase)cboPriceType).Value = value15;
	}

	public override void btnDeleteClick()
	{
		if (!ValidateForShift() || drMaster == null)
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
		if (bool.Parse(drMaster["IsJVCreated"].ToString()) && int.Parse(BlanksInvoices.CheckShiftDetailsClosedByBlankInvoiceID(drMaster["BlankInvoiceID"].ToString()).Rows[0]["Counter"].ToString()) > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لوردية مفلقة ولأنه تم عمل قيد عليها", "Cannot Delete This Transaction Because It Is Related To A Closed Shift And Already Has A JV. ");
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

	private void btnClientSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.LnsProductionSubAccounts(ManufacturingClientAccountID, "1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboClient).Value = num;
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

	private void btnClientBalance_Click(object sender, EventArgs e)
	{
		if (cboClient.SelectedIndex != -1)
		{
			try
			{
				frmSubAccountBalanceWithCurrency frmSubAccountBalanceWithCurrency2 = new frmSubAccountBalanceWithCurrency(int.Parse(((TextEditorControlBase)cboClient).Value.ToString()), "-1");
				frmSubAccountBalanceWithCurrency2.WindowState = FormWindowState.Normal;
				frmSubAccountBalanceWithCurrency2.ShowDialog();
			}
			catch
			{
				GlobalVariables.InformationMB.Show("لايمكن الإستعلام عن رصيد العميل لتعزر الإتصال بالخادم", "Client Balance not Available ");
			}
		}
	}

	public override bool ValidateData()
	{
		if (dtpDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال تاريخ الأذن", "Please Enter The Voucher Date");
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
		if (dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["ManufacturingClientAccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال حساب عملاء التصنيع من إعدادات البيع المباشر ", "Please Enter Default Manufacturing Client Account From Direct Sales Settings");
			return false;
		}
		if (dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["ManufacturingBranchID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال فرع التصنيع من إعدادات البيع المباشر ", "Please Enter Default Manufacturing Branch From Direct Sales Settings");
			return false;
		}
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال رقم  الأذن", "Please Enter The Voucher Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (cboClient.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار العميل", "Please Select Client");
			((TextEditorControlBase)cboClient).Focus();
			cboClient.DropDown();
			return false;
		}
		DataTable dataTable = BusinessLayer.POS.Settings.ValidateManufacturingClientAccountData(((TextEditorControlBase)cboClient).Value.ToString(), dtPOSDefaultData.Rows[0]["ManufacturingClientAccountID"].ToString());
		if (int.Parse(dataTable.Rows[0]["Relation"].ToString()) == 0)
		{
			GlobalVariables.InformationMB.Show("لايوجد ربط بين حساب العميل وحساب عملاء التصنيع  ", "There is No Relation Between Client Account And Manufacturing Client Account");
			return false;
		}
		if (cboPriceType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء نوع السعر", "Please Select Price Type");
			((TextEditorControlBase)cboPriceType).Focus();
			return false;
		}
		if (Convert.ToBoolean(dtPOSDefaultData.Rows[0]["EnforceSalesManSelection"]) && cboSalesMan.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء اختيار رجل البيع ", "Please Select SalesMan");
			((TextEditorControlBase)cboSalesMan).Focus();
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
		((UltraGridBase)ULGData).UpdateData();
		decimal num = decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["CreditLimit"].ToString());
		if (decimal.Parse((((Control)(object)txtBranchBalance).Text == "" || ((Control)(object)txtBranchBalance).Text == ".") ? "0" : ((Control)(object)txtBranchBalance).Text) + decimal.Parse((((Control)(object)txtRestAmount).Text == "" || ((Control)(object)txtRestAmount).Text == ".") ? "0" : ((Control)(object)txtRestAmount).Text) - ((Adding || (drMaster != null && drMaster["SubAccountID"].ToString() != ((TextEditorControlBase)cboClient).Value.ToString())) ? 0m : ((drMaster != null) ? decimal.Parse(drMaster["RestAmount"].ToString()) : decimal.Parse((((Control)(object)txtRestAmount).Text == "" || ((Control)(object)txtRestAmount).Text == ".") ? "0" : ((Control)(object)txtRestAmount).Text))) > num && num != 0m)
		{
			GlobalVariables.InformationMB.Show("  الحد الاقصى لإئتمان العميل \n" + num, " Max Client Credit Limit \n" + num);
			return false;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا الإذن", "Please insert details for this Voucher");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["BlankTypeID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الخامة  ", "Please Enter Blank Type Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["BlankTypeID"];
				((UltraGridBase)ULGData).Rows[i].Cells["BlankTypeID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["GlassesTypeID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال النوع  ", "Please Enter Glasses Type");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["GlassesTypeID"];
				((UltraGridBase)ULGData).Rows[i].Cells["GlassesTypeID"].DroppedDown = true;
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
			if (((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value == DBNull.Value || (!Convert.ToBoolean(((UltraGridBase)ULGData).Rows[i].Cells["IsNoBlank"].Value) && decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) <= 0m))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال سعر الوحدة  ", "Please Enter Unit Price ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value == DBNull.Value || (!Convert.ToBoolean(((UltraGridBase)ULGData).Rows[i].Cells["IsNoBlank"].Value) && decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString()) <= 0m))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إدخال إجمالى السعر  ", "Please Enter Total price");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["IsRightEye"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيارالنوع  ", "Please Select Eye ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["IsRightEye"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
		}
		if (dtDetails.Rows.Count > 1 && int.Parse(dtDetails.Compute("Count(IsRightEye)", "IsRightEye = " + dtDetails.Rows[0]["IsRightEye"].ToString()).ToString()) == dtDetails.Rows.Count)
		{
			GlobalVariables.QuestionMB.Show("جميع العدسات لنفس العين", "All Lenses Have The Same Eye.");
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				return false;
			}
		}
		DataTable dataTable2 = BusinessLayer.POS.Settings.ValidateCashierData(GlobalVariables.UserID, GlobalVariables.CurrentBranchID);
		if (dataTable2.Rows[0]["SubAccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد الحساب التحليلى للمستخدم  ", "Please Set user SubAccount");
			return false;
		}
		if (dataTable2.Rows[0]["CashierAccount"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب الكاشير من إعدادات البيع المباشر  ", "Please Set Cashier Account From POS Setting");
			return false;
		}
		if (int.Parse(dataTable2.Rows[0]["Relation"].ToString()) == 0)
		{
			GlobalVariables.InformationMB.Show("لايوجد ربط بين حساب الكاشير وحساب المستخدم  ", "There is No Relation Between Cashier Account And user SubAccount");
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("Lns_BlanksInvoices", "BlankInvoiceNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["BlankInvoiceNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = BlanksInvoices.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: true);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Voucher Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByBranchID;
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
			DataTable dataTable2 = Currency.FillCurrencyByDate(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			if (dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["CurrencyID"] == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال عملة الفرع من إعدادات البيع المباشر ", "Please Enter Default Currency From POS Settings");
				return false;
			}
			if (decimal.Parse(dataTable2.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال سعر الصرف" : "Please Enter The Exchange Rate");
				return false;
			}
			if (!Updating)
			{
				DataTable dataTable3 = ShiftsDetailsUsers.SelectNotClosed(ShiftDetailID, GlobalVariables.UserID, GlobalVariables.CurrentBranchID);
				if (dataTable3.Rows.Count == 0)
				{
					ShiftDetailUserID = ShiftsDetailsUsers.Insert_Update("-1", ShiftDetailID, GlobalVariables.UserID, GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate), "Null", "0", dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dataTable2.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), "0", "0", "0", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID).ToString();
				}
				else
				{
					ShiftDetailUserID = dataTable3.Rows[0]["ShiftDetailUserID"].ToString();
				}
			}
		}
		return true;
	}

	public override void AddData()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected O, but got Unknown
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Expected O, but got Unknown
		//IL_067a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0684: Expected O, but got Unknown
		//IL_092e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0938: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: true);
		int num;
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
			ULGDataAdditions.AfterCellUpdate -= new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
			((UltraGridBase)ULGDataAdditions).UpdateData();
			dtInvoiceAdditions.AcceptChanges();
			CalculateGoss();
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataAdditions).Rows).Count; j++)
			{
				CalculateAdditionRow(((UltraGridBase)ULGDataAdditions).Rows[j]);
				CalculateAdditionRowActualUnitSalesPrice(((UltraGridBase)ULGDataAdditions).Rows[j]);
			}
			CalculateTotalsTax();
			ULGDataAdditions.AfterCellUpdate += new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
			num = BlanksInvoices.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", dtPOSDefaultData.Rows[0]["ManufacturingClientAccountID"].ToString(), ((TextEditorControlBase)cboClient).Value.ToString(), ((Control)(object)txtVisionTestClient).Text, (ClientsGlassesHistoryID == 0m) ? "Null" : ClientsGlassesHistoryID.ToString(), (((TextEditorControlBase)cboPriceType).Value == DBNull.Value) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), (cboLine.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLine).Value.ToString(), (cboSalesMan.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesMan).Value.ToString(), (cboSalesMan2.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesMan2).Value.ToString(), ((UltraToggleEditorBase)chkApplyTax).Checked ? "1" : "0", (((Control)(object)txtTotalQty).Text == "") ? "0" : ((Control)(object)txtTotalQty).Text, (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscBeforeTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text, (((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, (((Control)(object)txtDiscAfterTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text, (((Control)(object)txtDiscAfterTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxRatio).Text, (((Control)(object)txtCommercialTax).Text == "") ? "0" : ((Control)(object)txtCommercialTax).Text, (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, (((Control)(object)txtPaidAmount).Text == "") ? "0" : ((Control)(object)txtPaidAmount).Text, (((Control)(object)txtRestAmount).Text == "") ? "0" : ((Control)(object)txtRestAmount).Text, ((Control)(object)txtNotes).Text, "Null", "Null", "0", "Null", "0", "Null", "0", "Null", "0", "Null", "0", "Null", "0", ((UltraToggleEditorBase)chkIsDeliverd).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsDeliverd).Checked ? dtpDeliverdDate.DateTime.ToString(GlobalVariables.DateLongFormate) : "Null", "0", (cboTravelNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTravelNo).Value.ToString(), ShiftDetailID, ShiftDetailUserID, GlobalVariables.UserID, (NewPriceUserID > 0) ? NewPriceUserID.ToString() : "Null", (DiscountUserID > 0) ? DiscountUserID.ToString() : "Null", dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["BranchID"].ToString(), dtPOSDefaultData.Rows[0]["ManufacturingBranchID"].ToString(), "Null", "Null", "1", "Null", "Null", "Null", "0", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
				{
					((UltraGridBase)ULGData).Rows[k].Cells["BlankInvoiceDetailID"].Value = -1;
					((UltraGridBase)ULGData).Rows[k].Cells["BlankInvoiceID"].Value = num;
					((UltraGridBase)ULGData).Rows[k].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
					((UltraGridBase)ULGData).Rows[k].Cells["IsFinished"].Value = false;
					((UltraGridBase)ULGData).Rows[k].Cells["IsReturned"].Value = false;
					((UltraGridBase)ULGData).Rows[k].Cells["ReturnDate"].Value = DBNull.Value;
					((UltraGridBase)ULGData).Rows[k].Cells["ReturnReasonID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).Rows[k].Cells["IsFinalReturned"].Value = false;
					((UltraGridBase)ULGData).Rows[k].Cells["IsReturnReceived"].Value = false;
					((UltraGridBase)ULGData).Rows[k].Cells["ReturnReceivedDate"].Value = DBNull.Value;
					((UltraGridBase)ULGData).Rows[k].Cells["ReturnJVID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).Rows[k].Cells["ShiftDetailID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).Rows[k].Cells["ShiftDetailUserID"].Value = DBNull.Value;
					bool flag = Convert.ToBoolean(((UltraGridBase)ULGData).Rows[k].Cells["ISNoBlank"].Value);
				}
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				BlanksInvoicesDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataAdditions).Rows).Count > 0)
			{
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataAdditions).Rows).Count; l++)
				{
					((UltraGridBase)ULGDataAdditions).Rows[l].Cells["BlankInvoiceAdditionID"].Value = -1;
					((UltraGridBase)ULGDataAdditions).Rows[l].Cells["BlankInvoiceID"].Value = num;
					((UltraGridBase)ULGDataAdditions).Rows[l].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				}
				BlanksInvoicesAdditions.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataAdditions).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
			return;
		}
		if (DataSaved)
		{
			RowID = num.ToString();
		}
		if (!DataSaved)
		{
			return;
		}
		string text = "";
		GlobalVariables.QuestionMB.Show("هل تريد طباعة الفاتورة ؟", "Are You Sure You want to Print This Invoice?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			return;
		}
		ReportDocument reportDocument = new ReportDocument();
		try
		{
			if (dtReports.Rows.Count > 0)
			{
				if (dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString().Trim() == "Rep_Lns_BlanksInvoicesFastPrint")
				{
					FastPrint();
					return;
				}
				reportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
			}
			else
			{
				reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Lns_BlanksInvoices_A.rpt" : "Rep_Lns_BlanksInvoices_E.rpt"));
			}
			GlobalFunctions.ConfigureReportOnline(reportDocument);
			string val = "";
			reportDocument.SetParameterValue("@BlankInvoiceID", RowID);
			reportDocument.SetParameterValue("@IsoCode", val);
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
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
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Expected O, but got Unknown
		//IL_0a46: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a50: Expected O, but got Unknown
		//IL_0b1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b27: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: true);
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
			ULGDataAdditions.AfterCellUpdate -= new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
			((UltraGridBase)ULGDataAdditions).UpdateData();
			CalculateGoss();
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataAdditions).Rows).Count; j++)
			{
				CalculateAdditionRow(((UltraGridBase)ULGDataAdditions).Rows[j]);
				CalculateAdditionRowActualUnitSalesPrice(((UltraGridBase)ULGDataAdditions).Rows[j]);
			}
			CalculateTotalsTax();
			ULGDataAdditions.AfterCellUpdate += new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
			int num = BlanksInvoices.Insert_Update(drMaster["BlankInvoiceID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", dtPOSDefaultData.Rows[0]["ManufacturingClientAccountID"].ToString(), ((TextEditorControlBase)cboClient).Value.ToString(), ((Control)(object)txtVisionTestClient).Text, (ClientsGlassesHistoryID == 0m) ? "Null" : ClientsGlassesHistoryID.ToString(), (((TextEditorControlBase)cboPriceType).Value == DBNull.Value) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), (cboLine.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLine).Value.ToString(), (cboSalesMan.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesMan).Value.ToString(), (cboSalesMan2.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesMan2).Value.ToString(), ((UltraToggleEditorBase)chkApplyTax).Checked ? "1" : "0", (((Control)(object)txtTotalQty).Text == "") ? "0" : ((Control)(object)txtTotalQty).Text, (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscBeforeTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text, (((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, (((Control)(object)txtDiscAfterTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text, (((Control)(object)txtDiscAfterTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscAfterTaxRatio).Text, (((Control)(object)txtCommercialTax).Text == "") ? "0" : ((Control)(object)txtCommercialTax).Text, (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, (((Control)(object)txtPaidAmount).Text == "") ? "0" : ((Control)(object)txtPaidAmount).Text, (((Control)(object)txtRestAmount).Text == "") ? "0" : ((Control)(object)txtRestAmount).Text, ((Control)(object)txtNotes).Text, (drMaster["MaterialFromStoreID"] == DBNull.Value) ? "Null" : drMaster["MaterialFromStoreID"].ToString(), (drMaster["ToStoreID"] == DBNull.Value) ? "Null" : drMaster["ToStoreID"].ToString(), bool.Parse(drMaster["IsSent"].ToString()) ? "1" : "0", (drMaster["SentDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["SentDate"].ToString()).ToString(GlobalVariables.DateLongFormate), bool.Parse(drMaster["IsFactoryReceive"].ToString()) ? "1" : "0", (drMaster["FactoryReceiveDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["FactoryReceiveDate"].ToString()).ToString(GlobalVariables.DateLongFormate), bool.Parse(drMaster["IsFinished"].ToString()) ? "1" : "0", (drMaster["FinishedDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["FinishedDate"].ToString()).ToString(GlobalVariables.DateLongFormate), bool.Parse(drMaster["IsFactorySent"].ToString()) ? "1" : "0", (drMaster["FactorySentDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["FactorySentDate"].ToString()).ToString(GlobalVariables.DateLongFormate), bool.Parse(drMaster["IsBranchReceive"].ToString()) ? "1" : "0", (drMaster["BranchReceiveDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["BranchReceiveDate"].ToString()).ToString(GlobalVariables.DateLongFormate), bool.Parse(drMaster["IsJVCreated"].ToString()) ? "1" : "0", ((UltraToggleEditorBase)chkIsDeliverd).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsDeliverd).Checked ? dtpDeliverdDate.DateTime.ToString(GlobalVariables.DateLongFormate) : "Null", "0", (cboTravelNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTravelNo).Value.ToString(), drMaster["ShiftDetailID"].ToString(), drMaster["ShiftDetailUserID"].ToString(), drMaster["User_ID"].ToString(), (NewPriceUserID > 0) ? NewPriceUserID.ToString() : "Null", (DiscountUserID > 0) ? DiscountUserID.ToString() : "Null", dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["BranchID"].ToString(), dtPOSDefaultData.Rows[0]["ManufacturingBranchID"].ToString(), (drMaster["TransferJVID"] == DBNull.Value) ? "Null" : drMaster["TransferJVID"].ToString(), (drMaster["TransferJVID2"] == DBNull.Value) ? "Null" : drMaster["TransferJVID2"].ToString(), "1", (drMaster["StockControlJVID"] == DBNull.Value) ? "Null" : drMaster["StockControlJVID"].ToString(), (drMaster["StockControlJVID2"] == DBNull.Value) ? "Null" : drMaster["StockControlJVID2"].ToString(), (drMaster["SalesJVID"] == DBNull.Value) ? "Null" : drMaster["SalesJVID"].ToString(), bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", "1", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				string text = ",";
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
				{
					((UltraGridBase)ULGData).Rows[k].Cells["BlankInvoiceID"].Value = num;
					((UltraGridBase)ULGData).Rows[k].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
					text = text + ((UltraGridBase)ULGData).Rows[k].Cells["BlankInvoiceDetailID"].Value.ToString() + ",";
				}
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				Main.SyncDeleteForUpdate("Lns_BlanksInvoicesDetails", "BlankInvoiceID", drMaster["BlankInvoiceID"].ToString(), "BlankInvoiceDetailID", text, IsFromServer: true);
				BlanksInvoicesDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataAdditions).Rows).Count > 0)
			{
				string text2 = ",";
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataAdditions).Rows).Count; l++)
				{
					((UltraGridBase)ULGDataAdditions).Rows[l].Cells["BlankInvoiceID"].Value = num;
					((UltraGridBase)ULGDataAdditions).Rows[l].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
					text2 = text2 + ((UltraGridBase)ULGDataAdditions).Rows[l].Cells["BlankInvoiceAdditionID"].Value.ToString() + ",";
				}
				Main.SyncDeleteForUpdate("Lns_BlanksInvoicesAdditions", "BlankInvoiceID", drMaster["BlankInvoiceID"].ToString(), "BlankInvoiceAdditionID", text2, IsFromServer: true);
				BlanksInvoicesAdditions.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataAdditions).DataSource, GlobalVariables.UserID, IsFromServer: true);
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
		Main.StartBulkTrans(FromServer: true);
		try
		{
			BlanksInvoices.DeleteVirtual(drMaster["BlankInvoiceID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			BlanksInvoicesDetails.DeleteVirtualByBlankInvoiceID(drMaster["BlankInvoiceID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			BlanksInvoicesAdditions.DeleteVirtualByBlankInvoiceID(drMaster["BlankInvoiceID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void CalculateGoss()
	{
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num2 += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
			((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString());
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataAdditions).Rows).Count; j++)
		{
			((UltraGridBase)ULGDataAdditions).Rows[j].Cells["TotalPrice"].Value = num2 * decimal.Parse(((UltraGridBase)ULGDataAdditions).Rows[j].Cells["UnitPrice"].Value.ToString());
			num += decimal.Parse(((UltraGridBase)ULGDataAdditions).Rows[j].Cells["TotalPrice"].Value.ToString());
		}
		((Control)(object)txtTotalQty).Text = decimal.Parse(num2.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtGrossValue).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
		if (CalculateGrossWithoutItemUnderDiscount() > 0m)
		{
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * CalculateGrossWithoutItemUnderDiscount(), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
		else
		{
			((Control)(object)txtDiscBeforeTaxValue).Text = "0";
		}
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
	}

	private void CalculateTotalsTax()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TaxValue"].Value.ToString());
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataAdditions).Rows).Count; j++)
		{
			num += decimal.Parse(((UltraGridBase)ULGDataAdditions).Rows[j].Cells["TaxValue"].Value.ToString());
		}
		((Control)(object)txtTaxTotalValue).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		CalculateNetTotals();
	}

	private void CalculateRow(UltraGridRow Row)
	{
		if (((Control)(object)txtGrossValue).Text != "" && ((Control)(object)txtGrossValue).Text != ".")
		{
			if (dtBlankTypePrices != null && dtBlankTypePrices.Rows.Count > 0 && Row.Cells["BlankTypeID"].Value != DBNull.Value && decimal.Parse(dtBlankTypePrices.Select(" BlankTypeID= " + Row.Cells["BlankTypeID"].Value.ToString())[0]["DiscountPercentage"].ToString()) > 0m)
			{
				Row.Cells["DisCount"].Value = 0;
			}
			else
			{
				Row.Cells["DisCount"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) * decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m;
			}
		}
		if (Row.Cells["TaxID"].Value != DBNull.Value)
		{
			Row.Cells["TaxValue"].Value = decimal.Parse(dtTaxes.Select(" TaxID= " + Row.Cells["TaxID"].Value.ToString())[0]["TaxPercent"].ToString()) / 100m * (decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString()));
		}
		else
		{
			Row.Cells["TaxValue"].Value = 0;
		}
		Row.Cells["NetPrice"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString());
	}

	private void CalculateAdditionRow(UltraGridRow Row)
	{
		if (((Control)(object)txtGrossValue).Text != "" && ((Control)(object)txtGrossValue).Text != ".")
		{
			if (dtBlankAdditionPrices != null && dtBlankAdditionPrices.Rows.Count > 0 && Row.Cells["BlankAdditionID"].Value != DBNull.Value && decimal.Parse(dtBlankAdditionPrices.Select(" BlankAdditionID= " + Row.Cells["BlankAdditionID"].Value.ToString())[0]["DiscountPercentage"].ToString()) > 0m)
			{
				Row.Cells["DisCount"].Value = 0;
			}
			else
			{
				Row.Cells["DisCount"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) * decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m;
			}
		}
		if (Row.Cells["TaxID"].Value != DBNull.Value)
		{
			Row.Cells["TaxValue"].Value = decimal.Parse(dtTaxes.Select(" TaxID= " + Row.Cells["TaxID"].Value.ToString())[0]["TaxPercent"].ToString()) / 100m * (decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString()));
		}
		else
		{
			Row.Cells["TaxValue"].Value = 0;
		}
		Row.Cells["NetPrice"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString());
	}

	private void CalculateNetTotals()
	{
		((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged -= txtDiscAfterTaxValue_ValueChanged;
		((Control)(object)txtDiscAfterTaxValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscAfterTaxRatio).Text == "" || ((Control)(object)txtDiscAfterTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxRatio).Text) / 100m * (decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text)), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtDiscAfterTaxValue).ValueChanged += txtDiscAfterTaxValue_ValueChanged;
		((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) - decimal.Parse((((Control)(object)txtDiscAfterTaxValue).Text == "" || ((Control)(object)txtDiscAfterTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text) - decimal.Parse((((Control)(object)txtCommercialTax).Text == "" || ((Control)(object)txtCommercialTax).Text == ".") ? "0" : ((Control)(object)txtCommercialTax).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtRestAmount).ValueChanged -= txtAdditionalValues_ValueChanged;
		((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtRestAmount).ValueChanged += txtAdditionalValues_ValueChanged;
	}

	private void CalculateAdditionRowActualUnitSalesPrice(UltraGridRow Row)
	{
		Row.Cells["ActualUnitSalesPrice"].Value = (decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) + decimal.Parse(Row.Cells["TaxValue"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString()) + decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) * -decimal.Parse((((Control)(object)txtDiscAfterTaxValue).Text == "" || ((Control)(object)txtDiscAfterTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text) / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text)) / decimal.Parse((((Control)(object)txtTotalQty).Text == "" || ((Control)(object)txtTotalQty).Text == ".") ? "0" : ((Control)(object)txtTotalQty).Text);
	}

	private void CalculateRowActualUnitSalesPrice(UltraGridRow Row)
	{
		Row.Cells["ActualUnitSalesPrice"].Value = (decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) + decimal.Parse(Row.Cells["TaxValue"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString()) + decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) * -decimal.Parse((((Control)(object)txtDiscAfterTaxValue).Text == "" || ((Control)(object)txtDiscAfterTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text) / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text)) / decimal.Parse(Row.Cells["Qty"].Value.ToString());
	}

	private decimal CalculateGrossWithoutItemUnderDiscount()
	{
		if (cboPriceType.SelectedIndex > -1 && (dtBlankTypePrices == null || dtBlankTypePrices.Rows.Count == 0))
		{
			dtBlankTypePrices = BlanksTypesPrices.GetPriceWithItemDiscount(((TextEditorControlBase)cboPriceType).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
		}
		if (cboPriceType.SelectedIndex > -1 && (dtBlankAdditionPrices == null || dtBlankAdditionPrices.Rows.Count == 0))
		{
			dtBlankAdditionPrices = BlanksAdditionsPrices.GetPriceWithItemDiscount(((TextEditorControlBase)cboPriceType).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
		}
		decimal result = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (dtBlankTypePrices != null && dtBlankTypePrices.Rows.Count > 0 && ((UltraGridBase)ULGData).Rows[i].Cells["BlankTypeID"].Value != DBNull.Value && decimal.Parse(dtBlankTypePrices.Select(" BlankTypeID= " + ((UltraGridBase)ULGData).Rows[i].Cells["BlankTypeID"].Value.ToString())[0]["DiscountPercentage"].ToString()) > 0m)
			{
				result += 0m;
				continue;
			}
			((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString());
			result += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataAdditions).Rows).Count; j++)
		{
			if (dtBlankAdditionPrices != null && dtBlankAdditionPrices.Rows.Count > 0 && ((UltraGridBase)ULGDataAdditions).Rows[j].Cells["BlankAdditionID"].Value != DBNull.Value && decimal.Parse(dtBlankAdditionPrices.Select(" BlankAdditionID= " + ((UltraGridBase)ULGDataAdditions).Rows[j].Cells["BlankAdditionID"].Value.ToString())[0]["DiscountPercentage"].ToString()) > 0m)
			{
				result += 0m;
				continue;
			}
			((UltraGridBase)ULGDataAdditions).Rows[j].Cells["TotalPrice"].Value = decimal.Parse((((Control)(object)txtTotalQty).Text == "" || ((Control)(object)txtTotalQty).Text == ".") ? "0" : ((Control)(object)txtTotalQty).Text) * decimal.Parse(((UltraGridBase)ULGDataAdditions).Rows[j].Cells["UnitPrice"].Value.ToString());
			result += decimal.Parse(((UltraGridBase)ULGDataAdditions).Rows[j].Cells["TotalPrice"].Value.ToString());
		}
		return result;
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (drMaster != null && Updating && bool.Parse(drMaster["IsSent"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "NetPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((UltraGridBase)ULGData).ActiveRow.Cells["BlankTypeID"].Value != DBNull.Value && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" && !bool.Parse(dtBlankTypePrices.Select(" BlankTypeID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["BlankTypeID"].Value.ToString())[0]["CanModifyPrice"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxID" && ((UltraToggleEditorBase)chkTax).Checked)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Expected O, but got Unknown
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "BlankTypeID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			((UltraGridBase)ULGData).ActiveRow.Cells["IsNoBlank"].Value = dtBlankTypes.Select("BlankTypeID = " + ((UltraGridBase)ULGData).ActiveRow.Cells["BlankTypeID"].Value.ToString())[0]["IsNoBlank"];
			if (dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["ApplySalesTax"].ToString()) && ((UltraToggleEditorBase)chkApplyTax).Checked)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dtPOSDefaultData.Rows[0]["TaxID"];
			}
			else
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = DBNull.Value;
			}
			if (dtBlankTypePrices != null && dtBlankTypePrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dtBlankTypePrices.Select(" BlankTypeID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["BlankTypeID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtBlankTypePrices.Select(" BlankTypeID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["BlankTypeID"].Value.ToString())[0]["Price"].ToString()) * Math.Round(decimal.Parse(dtBlankTypePrices.Select(" BlankTypeID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["BlankTypeID"].Value.ToString())[0]["DiscountPercentage"].ToString()), 3) / 100m;
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				CalculateGoss();
				CalculateRow(e.Cell.Row);
				CalculateTotalsTax();
			}
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "IsRightEye" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			ApplyGlassesHistoryOnRow(e.Cell.Row.Index);
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "TaxID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			CalculateRow(e.Cell.Row);
			CalculateTotalsTax();
		}
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterExitEditMode(object sender, EventArgs e)
	{
		ULGData.AfterExitEditMode -= ULGData_AfterExitEditMode;
		if (ULGData.ActiveCell != null && ULGData.ActiveCell.Value != null && ULGData.ActiveCell.Value != DBNull.Value && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BlankTypeID" && dtBlankTypes.Select(" BlankTypeID= " + ULGData.ActiveCell.Value.ToString()).Length == 0)
		{
			UltraGridCell activeCell = ULGData.ActiveCell;
			object value = (((UltraGridBase)ULGData).ActiveRow.Cells["BlankTypeID"].Value = DBNull.Value);
			activeCell.Value = value;
		}
		ULGData.AfterExitEditMode += ULGData_AfterExitEditMode;
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyValue == 38 && (ULGData.ActiveCell.ValueList != null || ULGData.ActiveCell.Column.ValueList != null))
		{
			ULGData.PerformAction((UltraGridAction)19);
			ULGData.PerformAction((UltraGridAction)24);
			e.Handled = true;
		}
		else if (e.KeyValue == 40 && (ULGData.ActiveCell.ValueList != null || ULGData.ActiveCell.Column.ValueList != null))
		{
			ULGData.PerformAction((UltraGridAction)20);
			ULGData.PerformAction((UltraGridAction)24);
			e.Handled = true;
		}
		else
		{
			if (e.KeyCode != Keys.F8)
			{
				return;
			}
			if ((Adding || Updating) && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxID")
			{
				int num = SearchFunctions.TaxsSearch(IsFromServer: false);
				if (num != 0)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = num;
				}
			}
			e.Handled = true;
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "MM" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Addition" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Ax" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "IPD" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "PH"))
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Expected O, but got Unknown
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Expected O, but got Unknown
		//IL_0804: Unknown result type (might be due to invalid IL or missing references)
		//IL_080e: Expected O, but got Unknown
		if (ULGData.ActiveCell == null)
		{
			return;
		}
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BlankTypeID")
		{
			ULGData_CellListSelect(ULGData, e);
		}
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue") && ULGData.ActiveCell.Value == DBNull.Value)
		{
			ULGData.ActiveCell.Value = 0;
		}
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty")
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			ULGDataAdditions.AfterCellUpdate -= new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
			decimal num = default(decimal);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
			}
			((Control)(object)txtTotalQty).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataAdditions).Rows).Count; j++)
			{
				if (((UltraGridBase)ULGDataAdditions).Rows[j].Cells["BlankAdditionID"].Value != DBNull.Value)
				{
					((UltraGridBase)ULGDataAdditions).Rows[j].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGDataAdditions).Rows[j].Cells["UnitPrice"].Value.ToString()) * decimal.Parse((((Control)(object)txtTotalQty).Text == "" || ((Control)(object)txtTotalQty).Text == ".") ? "0" : ((Control)(object)txtTotalQty).Text);
					CalculateAdditionRow(((UltraGridBase)ULGDataAdditions).Rows[j]);
				}
			}
			ULGDataAdditions.AfterCellUpdate += new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
			CalculateGoss();
		}
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice")
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			CalculateGoss();
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > 0m)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value.ToString()) / decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			CalculateGoss();
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxID" && e.Cell.Value == DBNull.Value)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["TaxValue"].Value = 0;
			CalculateGoss();
		}
		if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "DColorID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "DItemSizeID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Addition") && ULGData.ActiveCell.Row.Cells["Addition"].Text != "" && decimal.TryParse(ULGData.ActiveCell.Row.Cells["Addition"].Text, out var result) && decimal.TryParse(ULGData.ActiveCell.Row.Cells["DColorID"].Text, out var result2) && ULGData.ActiveCell.Row.Cells["DItemSizeID"].Value != DBNull.Value)
		{
			string text = (result2 + result).ToString();
			DataRow[] array = dtColors.Select("ColorName = '" + text + "' or   ColorName = '+" + text + "'");
			if (array.Length != 0)
			{
				ULGData.ActiveCell.Row.Cells["RColorID"].Value = array[0]["ColorID"];
				ULGData.ActiveCell.Row.Cells["RItemSizeID"].Value = ULGData.ActiveCell.Row.Cells["DItemSizeID"].Value;
			}
		}
		CalculateRow(((UltraGridBase)ULGData).ActiveRow);
		CalculateTotalsTax();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Expected O, but got Unknown
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		CalculateGoss();
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
		}
		ULGDataAdditions.AfterCellUpdate -= new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
		((Control)(object)txtTotalQty).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataAdditions).Rows).Count; j++)
		{
			if (((UltraGridBase)ULGDataAdditions).Rows[j].Cells["BlankAdditionID"].Value != DBNull.Value)
			{
				((UltraGridBase)ULGDataAdditions).Rows[j].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGDataAdditions).Rows[j].Cells["UnitPrice"].Value.ToString()) * decimal.Parse((((Control)(object)txtTotalQty).Text == "" || ((Control)(object)txtTotalQty).Text == ".") ? "0" : ((Control)(object)txtTotalQty).Text);
				CalculateAdditionRow(((UltraGridBase)ULGDataAdditions).Rows[j]);
			}
		}
		ULGDataAdditions.AfterCellUpdate += new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
		CalculateTotalsTax();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		if (dtPOSDefaultData.Rows.Count > 0 && !bool.Parse(dtPOSDefaultData.Rows[0]["ForceBarcodeUse"].ToString()))
		{
			base.ULGData_BeforeRowsDeleted(sender, e);
		}
	}

	private void cboClient_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.Clients((cboBranches.SelectedIndex == -1) ? "-1" : ((TextEditorControlBase)cboBranches).Value.ToString(), "1", IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboClient).Value = num;
			}
		}
	}

	private void cboClient_ValueChanged(object sender, EventArgs e)
	{
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Expected O, but got Unknown
		//IL_0852: Unknown result type (might be due to invalid IL or missing references)
		//IL_085c: Expected O, but got Unknown
		((TextEditorControlBase)cboClientCode).ValueChanged -= cboClientCode_ValueChanged;
		((TextEditorControlBase)cboClientCode).Value = ((TextEditorControlBase)cboClient).Value;
		((TextEditorControlBase)cboClientCode).ValueChanged += cboClientCode_ValueChanged;
		if (cboClient.SelectedIndex <= -1)
		{
			return;
		}
		if (dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["Notes"].ToString() != "")
		{
			GlobalVariables.InformationMB.Show(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["Notes"].ToString());
		}
		((Control)(object)txtBranchBalance).Text = decimal.Parse(Math.Round(SubAccounts.Balance(((TextEditorControlBase)cboClient).Value.ToString(), "-1", Adding ? ("," + GlobalVariables.CurrentBranchID + ",") : drMaster["BranchID"].ToString(), dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), IsFromServer: false, 0), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
		((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["BlanksProductionDiscountPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
		if (dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"] != DBNull.Value)
		{
			((TextEditorControlBase)cboPriceType).ValueChanged -= cboPriceType_ValueChanged;
			((TextEditorControlBase)cboPriceType).Value = dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"];
			((TextEditorControlBase)cboPriceType).ValueChanged += cboPriceType_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			dtBlankTypePrices = BlanksTypesPrices.GetPriceWithItemDiscount(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtBlankAdditionPrices = BlanksAdditionsPrices.GetPriceWithItemDiscount(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["BlankTypeID"].Value != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(dtBlankTypePrices.Select(" BlankTypeID= " + ((UltraGridBase)ULGData).Rows[i].Cells["BlankTypeID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtBlankTypePrices.Select(" BlankTypeID= " + ((UltraGridBase)ULGData).Rows[i].Cells["BlankTypeID"].Value.ToString())[0]["Price"].ToString()) * Math.Round(decimal.Parse(dtBlankTypePrices.Select(" BlankTypeID= " + ((UltraGridBase)ULGData).Rows[i].Cells["BlankTypeID"].Value.ToString())[0]["DiscountPercentage"].ToString()), 3) / 100m;
					((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
					CalculateRow(((UltraGridBase)ULGData).Rows[i]);
				}
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataAdditions).Rows).Count; j++)
			{
				if (((UltraGridBase)ULGDataAdditions).Rows[j].Cells["BlankAdditionID"].Value != DBNull.Value)
				{
					((UltraGridBase)ULGDataAdditions).Rows[j].Cells["UnitPrice"].Value = decimal.Parse(dtBlankAdditionPrices.Select(" BlankAdditionID= " + ((UltraGridBase)ULGDataAdditions).Rows[j].Cells["BlankAdditionID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtBlankAdditionPrices.Select(" BlankAdditionID= " + ((UltraGridBase)ULGDataAdditions).Rows[j].Cells["BlankAdditionID"].Value.ToString())[0]["Price"].ToString()) * Math.Round(decimal.Parse(dtBlankAdditionPrices.Select(" BlankAdditionID= " + ((UltraGridBase)ULGDataAdditions).Rows[j].Cells["BlankAdditionID"].Value.ToString())[0]["DiscountPercentage"].ToString()), 3) / 100m;
					((UltraGridBase)ULGDataAdditions).Rows[j].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGDataAdditions).Rows[j].Cells["UnitPrice"].Value.ToString()) * decimal.Parse((((Control)(object)txtTotalQty).Text == "" || ((Control)(object)txtTotalQty).Text == ".") ? "0" : ((Control)(object)txtTotalQty).Text);
					CalculateAdditionRow(((UltraGridBase)ULGDataAdditions).Rows[j]);
				}
			}
			CalculateGoss();
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		else
		{
			dtBlankTypePrices = null;
			dtBlankAdditionPrices = null;
			((TextEditorControlBase)cboPriceType).ValueChanged -= cboPriceType_ValueChanged;
			cboPriceType.SelectedIndex = -1;
			((TextEditorControlBase)cboPriceType).ValueChanged += cboPriceType_ValueChanged;
		}
		if (dtClients.Select("SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["LineID"] != DBNull.Value)
		{
			((TextEditorControlBase)cboLine).Value = dtClients.Select("SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["LineID"];
			dtSalesMan1 = SubAccounts.SelectSalesMan1ByLineDate(GlobalVariables.EmployeeSubAccountTypeIDs, ((TextEditorControlBase)cboLine).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboSalesMan, dtSalesMan1, "SubAccountID", "SubAccountName");
			dtSalesMan2 = SubAccounts.SelectSalesMan2ByLineDate(GlobalVariables.EmployeeSubAccountTypeIDs, ((TextEditorControlBase)cboLine).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboSalesMan2, dtSalesMan2, "SubAccountID", "SubAccountName");
			((Control)(object)btnSalesMan2Search).Visible = false;
			((Control)(object)btnSalesManSearch).Visible = false;
		}
		else
		{
			cboLine.SelectedIndex = -1;
			GlobalFunctions.FillCombo(cboSalesMan, dtSalesMan, "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboSalesMan2, dtSalesMan, "SubAccountID", "SubAccountName");
			((Control)(object)btnSalesMan2Search).Visible = Adding || Updating;
			((Control)(object)btnSalesManSearch).Visible = Adding || Updating;
		}
	}

	private void cboClient_Leave(object sender, EventArgs e)
	{
		InputLanguage.CurrentInputLanguage = Language;
	}

	private void cboClient_Enter(object sender, EventArgs e)
	{
		Language = InputLanguage.CurrentInputLanguage;
		InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("ar-Eg"));
	}

	private void chkVisa_CheckedChanged(object sender, EventArgs e)
	{
		UltraComboEditor obj = cboVisaType;
		bool enabled = (((Control)(object)txtVisaNo).Enabled = ((UltraToggleEditorBase)chkVisa).Checked);
		((Control)(object)obj).Enabled = enabled;
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

	private void cboTax_ValueChanged(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		ULGDataAdditions.AfterCellUpdate -= new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["TaxID"].Value = ((TextEditorControlBase)cboTax).Value;
			CalculateRow(((UltraGridBase)ULGData).Rows[i]);
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataAdditions).Rows).Count; j++)
		{
			((UltraGridBase)ULGDataAdditions).Rows[j].Cells["TaxID"].Value = ((TextEditorControlBase)cboTax).Value;
			CalculateAdditionRow(((UltraGridBase)ULGDataAdditions).Rows[j]);
		}
		CalculateTotalsTax();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].DefaultCellValue = ((TextEditorControlBase)cboTax).Value;
		((UltraGridBase)ULGDataAdditions).DisplayLayout.Bands[0].Columns["TaxID"].DefaultCellValue = ((TextEditorControlBase)cboTax).Value;
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		ULGDataAdditions.AfterCellUpdate += new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
	}

	private void ULGDataAdditions_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (drMaster != null && Updating && bool.Parse(drMaster["IsSent"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGDataAdditions).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGDataAdditions.ActiveCell.Column).Key == "NetPrice" || ((KeyedSubObjectBase)ULGDataAdditions.ActiveCell.Column).Key == "TaxValue" || ((KeyedSubObjectBase)ULGDataAdditions.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGDataAdditions.ActiveCell.Column).Key == "TotalPrice")
		{
			((GridItemBase)((UltraGridBase)ULGDataAdditions).ActiveRow).Selected = true;
		}
		else if (dtBlankAdditionPrices != null && ((UltraGridBase)ULGDataAdditions).ActiveRow.Cells["BlankAdditionID"].Value != DBNull.Value && ((KeyedSubObjectBase)ULGDataAdditions.ActiveCell.Column).Key == "UnitPrice" && !bool.Parse(dtBlankAdditionPrices.Select(" BlankAdditionID =" + ((UltraGridBase)ULGDataAdditions).ActiveRow.Cells["BlankAdditionID"].Value.ToString())[0]["CanModifyPrice"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGDataAdditions).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGDataAdditions.ActiveCell.Column).Key == "TaxID" && ((UltraToggleEditorBase)chkTax).Checked)
		{
			((GridItemBase)((UltraGridBase)ULGDataAdditions).ActiveRow).Selected = true;
		}
	}

	private void ULGDataAdditions_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Expected O, but got Unknown
		if (ULGDataAdditions.ActiveCell != null)
		{
			if (((KeyedSubObjectBase)ULGDataAdditions.ActiveCell.Column).Key == "BlankAdditionID")
			{
				ULGDataAdditions_CellListSelect(ULGDataAdditions, e);
			}
			ULGDataAdditions.AfterCellUpdate -= new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
			if ((((KeyedSubObjectBase)ULGDataAdditions.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGDataAdditions.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGDataAdditions.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGDataAdditions.ActiveCell.Column).Key == "TaxValue") && ULGDataAdditions.ActiveCell.Value == DBNull.Value)
			{
				ULGDataAdditions.ActiveCell.Value = 0;
			}
			if (((KeyedSubObjectBase)ULGDataAdditions.ActiveCell.Column).Key == "UnitPrice")
			{
				((UltraGridBase)ULGDataAdditions).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGDataAdditions).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse((((Control)(object)txtTotalQty).Text == "" || ((Control)(object)txtTotalQty).Text == ".") ? "0" : ((Control)(object)txtTotalQty).Text);
				CalculateGoss();
			}
			else if (((KeyedSubObjectBase)ULGDataAdditions.ActiveCell.Column).Key == "TotalPrice" && decimal.Parse((((Control)(object)txtTotalQty).Text == "" || ((Control)(object)txtTotalQty).Text == ".") ? "0" : ((Control)(object)txtTotalQty).Text) > 0m)
			{
				((UltraGridBase)ULGDataAdditions).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(((UltraGridBase)ULGDataAdditions).ActiveRow.Cells["TotalPrice"].Value.ToString()) / decimal.Parse((((Control)(object)txtTotalQty).Text == "" || ((Control)(object)txtTotalQty).Text == ".") ? "0" : ((Control)(object)txtTotalQty).Text);
				CalculateGoss();
			}
			else if (((KeyedSubObjectBase)ULGDataAdditions.ActiveCell.Column).Key == "TaxID" && e.Cell.Value == DBNull.Value)
			{
				((UltraGridBase)ULGDataAdditions).ActiveRow.Cells["TaxValue"].Value = 0;
				CalculateGoss();
			}
			CalculateAdditionRow(((UltraGridBase)ULGDataAdditions).ActiveRow);
			CalculateTotalsTax();
			ULGDataAdditions.AfterCellUpdate += new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
		}
	}

	private void ULGDataAdditions_AfterExitEditMode(object sender, EventArgs e)
	{
		ULGDataAdditions.AfterExitEditMode -= ULGDataAdditions_AfterExitEditMode;
		if (ULGDataAdditions.ActiveCell != null && ULGDataAdditions.ActiveCell.Value != null && ULGDataAdditions.ActiveCell.Value != DBNull.Value && ((KeyedSubObjectBase)ULGDataAdditions.ActiveCell.Column).Key == "BlankAdditionID" && dtBlankAdditions.Select(" BlankAdditionID= " + ULGDataAdditions.ActiveCell.Value.ToString()).Length == 0)
		{
			UltraGridCell activeCell = ULGDataAdditions.ActiveCell;
			object value = (((UltraGridBase)ULGDataAdditions).ActiveRow.Cells["BlankAdditionID"].Value = DBNull.Value);
			activeCell.Value = value;
		}
		ULGDataAdditions.AfterExitEditMode += ULGDataAdditions_AfterExitEditMode;
	}

	private void ULGDataAdditions_AfterRowsDeleted(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		ULGDataAdditions.AfterCellUpdate -= new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
		CalculateGoss();
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataAdditions).Rows).Count; i++)
		{
			CalculateAdditionRow(((UltraGridBase)ULGDataAdditions).Rows[i]);
		}
		CalculateTotalsTax();
		ULGDataAdditions.AfterCellUpdate += new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
	}

	private void ULGDataAdditions_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Expected O, but got Unknown
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Expected O, but got Unknown
		ULGDataAdditions.CellListSelect -= new CellEventHandler(ULGDataAdditions_CellListSelect);
		ULGDataAdditions.AfterCellUpdate -= new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "BlankAdditionID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGDataAdditions).UpdateData();
			if (dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["ApplySalesTax"].ToString()) && ((UltraToggleEditorBase)chkApplyTax).Checked)
			{
				((UltraGridBase)ULGDataAdditions).ActiveRow.Cells["TaxID"].Value = dtPOSDefaultData.Rows[0]["TaxID"];
			}
			else
			{
				((UltraGridBase)ULGDataAdditions).ActiveRow.Cells["TaxID"].Value = DBNull.Value;
			}
			if (dtBlankAdditionPrices != null && dtBlankAdditionPrices.Rows.Count > 0 && cboClient.SelectedIndex > -1)
			{
				((UltraGridBase)ULGDataAdditions).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dtBlankAdditionPrices.Select(" BlankAdditionID= " + ((UltraGridBase)ULGDataAdditions).ActiveRow.Cells["BlankAdditionID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtBlankAdditionPrices.Select(" BlankAdditionID= " + ((UltraGridBase)ULGDataAdditions).ActiveRow.Cells["BlankAdditionID"].Value.ToString())[0]["Price"].ToString()) * Math.Round(decimal.Parse(dtBlankAdditionPrices.Select(" BlankAdditionID= " + ((UltraGridBase)ULGDataAdditions).ActiveRow.Cells["BlankAdditionID"].Value.ToString())[0]["DiscountPercentage"].ToString()), 3) / 100m;
				((UltraGridBase)ULGDataAdditions).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGDataAdditions).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse((((Control)(object)txtTotalQty).Text == "" || ((Control)(object)txtTotalQty).Text == ".") ? "0" : ((Control)(object)txtTotalQty).Text);
				CalculateGoss();
				CalculateAdditionRow(e.Cell.Row);
				CalculateTotalsTax();
			}
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "TaxID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGDataAdditions).UpdateData();
			CalculateAdditionRow(e.Cell.Row);
			CalculateTotalsTax();
		}
		ULGDataAdditions.CellListSelect += new CellEventHandler(ULGDataAdditions_CellListSelect);
		ULGDataAdditions.AfterCellUpdate += new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
	}

	private void ULGDataAdditions_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyValue == 38 && (ULGData.ActiveCell.ValueList != null || ULGData.ActiveCell.Column.ValueList != null))
		{
			ULGData.PerformAction((UltraGridAction)19);
			ULGData.PerformAction((UltraGridAction)24);
			e.Handled = true;
		}
		else if (e.KeyValue == 40 && (ULGData.ActiveCell.ValueList != null || ULGData.ActiveCell.Column.ValueList != null))
		{
			ULGData.PerformAction((UltraGridAction)20);
			ULGData.PerformAction((UltraGridAction)24);
			e.Handled = true;
		}
		else
		{
			if (e.KeyCode != Keys.F8)
			{
				return;
			}
			if ((Adding || Updating) && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxID")
			{
				int num = SearchFunctions.TaxsSearch(IsFromServer: false);
				if (num != 0)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = num;
				}
			}
			e.Handled = true;
		}
	}

	private void ULGDataAdditions_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "NetPrice"))
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void cboPriceType_ValueChanged(object sender, EventArgs e)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Expected O, but got Unknown
		if (cboPriceType.SelectedIndex > -1)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			dtBlankTypePrices = BlanksTypesPrices.GetPriceWithItemDiscount(((TextEditorControlBase)cboPriceType).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtBlankAdditionPrices = BlanksAdditionsPrices.GetPriceWithItemDiscount(((TextEditorControlBase)cboPriceType).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(dtBlankTypePrices.Select(" BlankTypeID= " + ((UltraGridBase)ULGData).Rows[i].Cells["BlankTypeID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtBlankTypePrices.Select(" BlankTypeID= " + ((UltraGridBase)ULGData).Rows[i].Cells["BlankTypeID"].Value.ToString())[0]["Price"].ToString()) * Math.Round(decimal.Parse(dtBlankTypePrices.Select(" BlankTypeID= " + ((UltraGridBase)ULGData).Rows[i].Cells["BlankTypeID"].Value.ToString())[0]["DiscountPercentage"].ToString()), 3) / 100m;
				((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataAdditions).Rows).Count; j++)
			{
				((UltraGridBase)ULGDataAdditions).Rows[j].Cells["UnitPrice"].Value = decimal.Parse(dtBlankAdditionPrices.Select(" BlankAdditionID= " + ((UltraGridBase)ULGDataAdditions).Rows[j].Cells["BlankAdditionID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtBlankAdditionPrices.Select(" BlankAdditionID= " + ((UltraGridBase)ULGDataAdditions).Rows[j].Cells["BlankAdditionID"].Value.ToString())[0]["Price"].ToString()) * Math.Round(decimal.Parse(dtBlankAdditionPrices.Select(" BlankAdditionID= " + ((UltraGridBase)ULGDataAdditions).Rows[j].Cells["BlankAdditionID"].Value.ToString())[0]["DiscountPercentage"].ToString()), 3) / 100m;
				((UltraGridBase)ULGDataAdditions).Rows[j].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGDataAdditions).Rows[j].Cells["UnitPrice"].Value.ToString()) * decimal.Parse((((Control)(object)txtTotalQty).Text == "" || ((Control)(object)txtTotalQty).Text == ".") ? "0" : ((Control)(object)txtTotalQty).Text);
				CalculateAdditionRow(((UltraGridBase)ULGDataAdditions).Rows[j]);
			}
			CalculateGoss();
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		else
		{
			dtBlankTypePrices = null;
			dtBlankAdditionPrices = null;
		}
	}

	private void chkTax_CheckedChanged(object sender, EventArgs e)
	{
		((EditorButtonControlBase)cboTax).ReadOnly = !((UltraToggleEditorBase)chkTax).Checked;
	}

	private void chkApplyTax_CheckedChanged(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Expected O, but got Unknown
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		ULGDataAdditions.AfterCellUpdate -= new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["ApplySalesTax"].ToString()) && ((UltraToggleEditorBase)chkApplyTax).Checked)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["TaxID"].Value = dtPOSDefaultData.Rows[0]["TaxID"];
			}
			else
			{
				((UltraGridBase)ULGData).Rows[i].Cells["TaxID"].Value = DBNull.Value;
			}
			CalculateRow(((UltraGridBase)ULGData).Rows[i]);
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataAdditions).Rows).Count; j++)
		{
			if (dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["ApplySalesTax"].ToString()) && ((UltraToggleEditorBase)chkApplyTax).Checked)
			{
				((UltraGridBase)ULGDataAdditions).Rows[j].Cells["TaxID"].Value = dtPOSDefaultData.Rows[0]["TaxID"];
			}
			else
			{
				((UltraGridBase)ULGDataAdditions).Rows[j].Cells["TaxID"].Value = DBNull.Value;
			}
			CalculateAdditionRow(((UltraGridBase)ULGDataAdditions).Rows[j]);
		}
		CalculateTotalsTax();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		ULGDataAdditions.AfterCellUpdate += new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
	}

	private void btnLineSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.GLines(IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboLine).Value = num;
		}
	}

	public void OpenChangeDiscountForm(decimal GrossWithoutItemUnderDiscount)
	{
		frmChangeDiscount frmChangeDiscount2 = new frmChangeDiscount(GrossWithoutItemUnderDiscount, decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text), decimal.Parse(((Control)(object)txtDiscBeforeTaxRatio).Text));
		frmChangeDiscount2.WindowState = FormWindowState.Normal;
		frmChangeDiscount2.ShowDialog();
		if (frmChangeDiscount2.Cancel)
		{
			((Control)(object)txtDiscBeforeTaxRatio).Text = ((cboClient.SelectedIndex > -1 && decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["BlanksProductionDiscountPercentage"].ToString()) > UserSalesDiscount) ? dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["BlanksProductionDiscountPercentage"].ToString() : UserSalesDiscount.ToString());
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * GrossWithoutItemUnderDiscount, 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			return;
		}
		((Control)(object)txtDiscBeforeTaxRatio).Text = string.Concat(frmChangeDiscount2.DiscountRatio);
		((Control)(object)txtDiscBeforeTaxValue).Text = string.Concat(frmChangeDiscount2.DiscountValue);
		DiscountUserID = frmChangeDiscount2.UserID;
		frmChangeDiscount2.Dispose();
	}

	private void txtDiscBeforeTaxValue_ValueChanged(object sender, EventArgs e)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Expected O, but got Unknown
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		decimal num = CalculateGrossWithoutItemUnderDiscount();
		if (num > 0m)
		{
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			ULGDataAdditions.AfterCellUpdate -= new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == "0" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) / num * 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if ((cboClient.SelectedIndex > -1 && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["BlanksProductionDiscountPercentage"].ToString()) && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > UserSalesDiscount) || (cboClient.SelectedIndex == -1 && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > UserSalesDiscount))
			{
				OpenChangeDiscountForm(num);
			}
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataAdditions).Rows).Count; j++)
			{
				CalculateAdditionRow(((UltraGridBase)ULGDataAdditions).Rows[j]);
			}
			CalculateTotalsTax();
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			ULGDataAdditions.AfterCellUpdate += new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
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
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Expected O, but got Unknown
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		decimal num = CalculateGrossWithoutItemUnderDiscount();
		if (num > 0m)
		{
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			ULGDataAdditions.AfterCellUpdate -= new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * num, 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if ((cboClient.SelectedIndex > -1 && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["BlanksProductionDiscountPercentage"].ToString()) && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > UserSalesDiscount) || (cboClient.SelectedIndex == -1 && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > UserSalesDiscount))
			{
				OpenChangeDiscountForm(num);
			}
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataAdditions).Rows).Count; j++)
			{
				CalculateAdditionRow(((UltraGridBase)ULGDataAdditions).Rows[j]);
			}
			CalculateTotalsTax();
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			ULGDataAdditions.AfterCellUpdate += new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
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

	private void txtDiscAfterTaxValue_ValueChanged(object sender, EventArgs e)
	{
		if ((Adding || Updating) && decimal.Parse((((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text) > 0m)
		{
			((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged -= txtDiscAfterTaxRatio_ValueChanged;
			((Control)(object)txtDiscAfterTaxRatio).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscAfterTaxValue).Text == "" || ((Control)(object)txtDiscAfterTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text) * 100m / (decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text)), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscAfterTaxRatio).ValueChanged += txtDiscAfterTaxRatio_ValueChanged;
			((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) - decimal.Parse((((Control)(object)txtDiscAfterTaxValue).Text == "" || ((Control)(object)txtDiscAfterTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscAfterTaxValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text) - decimal.Parse((((Control)(object)txtCommercialTax).Text == "" || ((Control)(object)txtCommercialTax).Text == ".") ? "0" : ((Control)(object)txtCommercialTax).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtRestAmount).ValueChanged -= txtAdditionalValues_ValueChanged;
			((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtRestAmount).ValueChanged += txtAdditionalValues_ValueChanged;
		}
	}

	private void txtDiscAfterTaxRatio_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			CalculateNetTotals();
		}
	}

	private void txtAdditionalValues_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			CalculateNetTotals();
		}
	}

	private void txtPaidAmount_ValueChanged(object sender, EventArgs e)
	{
		((TextEditorControlBase)txtRestAmount).ValueChanged -= txtAdditionalValues_ValueChanged;
		((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtRestAmount).ValueChanged += txtAdditionalValues_ValueChanged;
	}

	private void textBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void textBox_Enter(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		((Control)(UltraTextEditor)sender).Select();
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Expected O, but got Unknown
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Expected O, but got Unknown
		if (cboPriceType.SelectedIndex > -1)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			ULGDataAdditions.AfterCellUpdate -= new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
			dtBlankTypePrices = BlanksTypesPrices.GetPriceWithItemDiscount(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtBlankAdditionPrices = BlanksAdditionsPrices.GetPriceWithItemDiscount(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["PriceTypeID"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(dtBlankTypePrices.Select(" BlankTypeID = " + ((UltraGridBase)ULGData).Rows[i].Cells["BlankTypeID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtBlankTypePrices.Select(" BlankTypeID= " + ((UltraGridBase)ULGData).Rows[i].Cells["BlankTypeID"].Value.ToString())[0]["Price"].ToString()) * Math.Round(decimal.Parse(dtBlankTypePrices.Select(" BlankTypeID= " + ((UltraGridBase)ULGData).Rows[i].Cells["BlankTypeID"].Value.ToString())[0]["DiscountPercentage"].ToString()), 3) / 100m;
				((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataAdditions).Rows).Count; j++)
			{
				((UltraGridBase)ULGDataAdditions).Rows[j].Cells["UnitPrice"].Value = decimal.Parse(dtBlankAdditionPrices.Select(" BlankAdditionID= " + ((UltraGridBase)ULGDataAdditions).Rows[j].Cells["BlankAdditionID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtBlankAdditionPrices.Select(" BlankAdditionID= " + ((UltraGridBase)ULGDataAdditions).Rows[j].Cells["BlankAdditionID"].Value.ToString())[0]["Price"].ToString()) * Math.Round(decimal.Parse(dtBlankAdditionPrices.Select(" BlankAdditionID= " + ((UltraGridBase)ULGDataAdditions).Rows[j].Cells["BlankAdditionID"].Value.ToString())[0]["DiscountPercentage"].ToString()), 3) / 100m;
				((UltraGridBase)ULGDataAdditions).Rows[j].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGDataAdditions).Rows[j].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGDataAdditions).Rows[j].Cells["Qty"].Value.ToString());
				CalculateAdditionRow(((UltraGridBase)ULGDataAdditions).Rows[j]);
			}
			CalculateGoss();
			CalculateTotalsTax();
			ULGDataAdditions.AfterCellUpdate += new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		else
		{
			dtBlankTypePrices = (dtBlankAdditionPrices = null);
		}
		if (Adding)
		{
			((Control)(object)txtCode).Text = BlanksInvoices.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: true);
		}
	}

	private void btnStoreTransfer_Click(object sender, EventArgs e)
	{
	}

	private void btnSalesManSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Employees("-1", "-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboSalesMan).Value = num;
		}
	}

	private void btnSalesMan2Search_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Employees("-1", "-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboSalesMan2).Value = num;
		}
	}

	private void btnBranchesSearch_Click(object sender, EventArgs e)
	{
		frmBranchesChange frmBranchesChange2 = new frmBranchesChange(base.Name);
		frmBranchesChange2.WindowState = FormWindowState.Normal;
		frmBranchesChange2.ShowDialog();
		((TextEditorControlBase)cboBranches).Value = ((frmBranchesChange2.BranchID > 0) ? ((object)frmBranchesChange2.BranchID) : ((TextEditorControlBase)cboBranches).Value);
	}

	private void txtClientCardNo_KeyUp(object sender, KeyEventArgs e)
	{
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Expected O, but got Unknown
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Expected O, but got Unknown
		if (cboClient.SelectedIndex > -1)
		{
			if (e.KeyCode != Keys.Return || !(((Control)(object)txtClientCardNo).Text != ""))
			{
				return;
			}
			DataTable discountRatio = BlanksInvoices.GetDiscountRatio(((TextEditorControlBase)cboClient).Value.ToString(), ((Control)(object)txtClientCardNo).Text);
			if (discountRatio.Rows.Count > 0)
			{
				((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
				((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				ULGDataAdditions.AfterCellUpdate -= new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
				((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse(discountRatio.Rows[0]["Percentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * CalculateGrossWithoutItemUnderDiscount(), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
				{
					CalculateRow(((UltraGridBase)ULGData).Rows[i]);
				}
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataAdditions).Rows).Count; j++)
				{
					CalculateAdditionRow(((UltraGridBase)ULGDataAdditions).Rows[j]);
				}
				CalculateTotalsTax();
				((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
				((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
				ULGDataAdditions.AfterCellUpdate += new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				((TextEditorControlBase)txtClientCardNo).Clear();
			}
			else
			{
				GlobalVariables.InformationMB.Show("لا يوجد خصم لهذا العميل", "There is no Discount For this SubAccount");
				((TextEditorControlBase)txtClientCardNo).Clear();
			}
		}
		else
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار العميل", "Please Select Client");
			((TextEditorControlBase)txtClientCardNo).Clear();
		}
	}

	private void cboBranches_ValueChanged(object sender, EventArgs e)
	{
		if (cboBranches.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtClients);
			dataView.RowFilter = " BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString() + " And ( ForAllBranches=1 or BranchID=  " + GlobalVariables.CurrentBranchID + " ) ";
			GlobalFunctions.FillCombo(cboClient, dataView.ToTable(), "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboClientCode, dataView.ToTable(), "SubAccountID", "ClientSupplierNo");
		}
		else
		{
			GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboClientCode, dtClients, "SubAccountID", "ClientSupplierNo");
		}
	}

	private void txtBarCode_Enter(object sender, EventArgs e)
	{
		CultureInfo culture = new CultureInfo("en-us");
		InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(culture);
	}

	private void btnAddHistory_Click(object sender, EventArgs e)
	{
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Expected O, but got Unknown
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Expected O, but got Unknown
		if (cboClient.SelectedIndex > -1)
		{
			int num = int.Parse(((TextEditorControlBase)cboClient).Value.ToString());
			frmPOSClientsGlassesHistory frmPOSClientsGlassesHistory2 = new frmPOSClientsGlassesHistory(islabmodule: false, num, ((Control)(object)cboClient).Text, -1m);
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
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				FillClientGlassesHistory(frmPOSClientsGlassesHistory2.ClientsGlassesHistoryID, ApplyOnDetails: true);
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
			object value = ((TextEditorControlBase)cboDoctor).Value;
			dtDoctors = Doctors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboDoctor, dtDoctors, "DoctorID", "DoctorName");
			((TextEditorControlBase)cboDoctor).Value = value;
		}
	}

	private void FillClientGlassesHistory(decimal ClientsGlassesHistoryID, bool ApplyOnDetails)
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
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["DColorID"].Value = (((UltraGridBase)ULGData).Rows[i].Cells["IsRightEye"].Value.ToString().Equals("1") ? dataRow["RColorIDDistance"] : dataRow["LColorIDDistance"]);
				((UltraGridBase)ULGData).Rows[i].Cells["DItemSizeID"].Value = (((UltraGridBase)ULGData).Rows[i].Cells["IsRightEye"].Value.ToString().Equals("1") ? dataRow["RItemSizeIDDistance"] : dataRow["LItemSizeIDDistance"]);
				((UltraGridBase)ULGData).Rows[i].Cells["RColorID"].Value = (((UltraGridBase)ULGData).Rows[i].Cells["IsRightEye"].Value.ToString().Equals("1") ? dataRow["RColorIDReading"] : dataRow["LColorIDReading"]);
				((UltraGridBase)ULGData).Rows[i].Cells["RItemSizeID"].Value = (((UltraGridBase)ULGData).Rows[i].Cells["IsRightEye"].Value.ToString().Equals("1") ? dataRow["RItemSizeIDReading"] : dataRow["LItemSizeIDReading"]);
				((UltraGridBase)ULGData).Rows[i].Cells["Addition"].Value = (((UltraGridBase)ULGData).Rows[i].Cells["IsRightEye"].Value.ToString().Equals("1") ? dataRow["RAdd"] : dataRow["LAdd"]);
				((UltraGridBase)ULGData).Rows[i].Cells["Ax"].Value = (((UltraGridBase)ULGData).Rows[i].Cells["IsRightEye"].Value.ToString().Equals("1") ? dataRow["RAxDistance"] : dataRow["LAxDistance"]);
				((UltraGridBase)ULGData).Rows[i].Cells["IPD"].Value = ((dataRow["IPDDistance"] == DBNull.Value) ? ((object)0) : dataRow["IPDDistance"]);
			}
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DColorID"].DefaultCellValue = dataRow["RColorIDDistance"];
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DItemSizeID"].DefaultCellValue = dataRow["RItemSizeIDDistance"];
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RColorID"].DefaultCellValue = dataRow["RColorIDReading"];
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RItemSizeID"].DefaultCellValue = dataRow["RItemSizeIDReading"];
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Addition"].DefaultCellValue = ((dataRow["RAdd"] == DBNull.Value) ? ((object)0) : dataRow["RAdd"]);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Ax"].DefaultCellValue = ((dataRow["RAxDistance"] == DBNull.Value) ? ((object)0) : dataRow["RAxDistance"]);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IPD"].DefaultCellValue = ((dataRow["IPDDistance"] == DBNull.Value) ? ((object)0) : dataRow["IPDDistance"]);
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
		cboLColorDistance.SelectedIndex = -1;
		cboLSizeDistance.SelectedIndex = -1;
		((TextEditorControlBase)txtLAxisDistance).Clear();
		cboLColorReading.SelectedIndex = -1;
		cboLSizeReading.SelectedIndex = -1;
		((TextEditorControlBase)txtLAxisReading).Clear();
		((TextEditorControlBase)txtIPDDistance).Clear();
		((TextEditorControlBase)txtIPDReading).Clear();
		((TextEditorControlBase)txtLAdd).Clear();
		((TextEditorControlBase)txtRAdd).Clear();
		cboDoctor.SelectedIndex = -1;
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

	public void ApplyGlassesHistoryOnRow(int rowIndex)
	{
		if (ClientsGlassesHistoryID != 0m)
		{
			((UltraGridBase)ULGData).Rows[rowIndex].Cells["DColorID"].Value = (((UltraGridBase)ULGData).Rows[rowIndex].Cells["IsRightEye"].Value.ToString().Equals("1") ? ((TextEditorControlBase)cboRColorDistance).Value : ((TextEditorControlBase)cboLColorDistance).Value);
			((UltraGridBase)ULGData).Rows[rowIndex].Cells["DItemSizeID"].Value = (((UltraGridBase)ULGData).Rows[rowIndex].Cells["IsRightEye"].Value.ToString().Equals("1") ? ((TextEditorControlBase)cboRSizeDistance).Value : ((TextEditorControlBase)cboLSizeDistance).Value);
			((UltraGridBase)ULGData).Rows[rowIndex].Cells["RColorID"].Value = (((UltraGridBase)ULGData).Rows[rowIndex].Cells["IsRightEye"].Value.ToString().Equals("1") ? ((TextEditorControlBase)cboRColorReading).Value : ((TextEditorControlBase)cboLColorReading).Value);
			((UltraGridBase)ULGData).Rows[rowIndex].Cells["RItemSizeID"].Value = (((UltraGridBase)ULGData).Rows[rowIndex].Cells["IsRightEye"].Value.ToString().Equals("1") ? ((TextEditorControlBase)cboRSizeReading).Value : ((TextEditorControlBase)cboLSizeReading).Value);
			((UltraGridBase)ULGData).Rows[rowIndex].Cells["Addition"].Value = (((UltraGridBase)ULGData).Rows[rowIndex].Cells["IsRightEye"].Value.ToString().Equals("1") ? ((Control)(object)txtRAdd).Text : ((Control)(object)txtLAdd).Text);
			((UltraGridBase)ULGData).Rows[rowIndex].Cells["Ax"].Value = (((UltraGridBase)ULGData).Rows[rowIndex].Cells["IsRightEye"].Value.ToString().Equals("1") ? ((Control)(object)txtRAxisDistance).Text : ((Control)(object)txtLAxisDistance).Text);
			((UltraGridBase)ULGData).Rows[rowIndex].Cells["IPD"].Value = (((Control)(object)txtIPDDistance).Text.Equals(string.Empty) ? "0" : ((Control)(object)txtIPDDistance).Text);
		}
	}

	public void ApplyGlassesHistoryOnDetails(bool ClearLensesData)
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["DColorID"].Value = (((UltraGridBase)ULGData).Rows[i].Cells["IsRightEye"].Value.ToString().Equals("1") ? ((TextEditorControlBase)cboRColorDistance).Value : ((TextEditorControlBase)cboLColorDistance).Value);
			((UltraGridBase)ULGData).Rows[i].Cells["DItemSizeID"].Value = (((UltraGridBase)ULGData).Rows[i].Cells["IsRightEye"].Value.ToString().Equals("1") ? ((TextEditorControlBase)cboRSizeDistance).Value : ((TextEditorControlBase)cboLSizeDistance).Value);
			((UltraGridBase)ULGData).Rows[i].Cells["RColorID"].Value = (((UltraGridBase)ULGData).Rows[i].Cells["IsRightEye"].Value.ToString().Equals("1") ? ((TextEditorControlBase)cboRColorReading).Value : ((TextEditorControlBase)cboLColorReading).Value);
			((UltraGridBase)ULGData).Rows[i].Cells["RItemSizeID"].Value = (((UltraGridBase)ULGData).Rows[i].Cells["IsRightEye"].Value.ToString().Equals("1") ? ((TextEditorControlBase)cboRSizeReading).Value : ((TextEditorControlBase)cboLSizeReading).Value);
			((UltraGridBase)ULGData).Rows[i].Cells["Addition"].Value = (((UltraGridBase)ULGData).Rows[i].Cells["IsRightEye"].Value.ToString().Equals("1") ? ((Control)(object)txtRAdd).Text : ((Control)(object)txtLAdd).Text);
			((UltraGridBase)ULGData).Rows[i].Cells["Ax"].Value = (((UltraGridBase)ULGData).Rows[i].Cells["IsRightEye"].Value.ToString().Equals("1") ? ((Control)(object)txtRAxisDistance).Text : ((Control)(object)txtLAxisDistance).Text);
			((UltraGridBase)ULGData).Rows[i].Cells["IPD"].Value = (((Control)(object)txtIPDDistance).Text.Equals(string.Empty) ? "0" : ((Control)(object)txtIPDDistance).Text);
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DColorID"].DefaultCellValue = ((TextEditorControlBase)cboRColorDistance).Value;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DItemSizeID"].DefaultCellValue = ((TextEditorControlBase)cboRSizeDistance).Value;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RColorID"].DefaultCellValue = ((TextEditorControlBase)cboRColorReading).Value;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RItemSizeID"].DefaultCellValue = ((TextEditorControlBase)cboRSizeReading).Value;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Addition"].DefaultCellValue = (((Control)(object)txtRAdd).Text.Equals(string.Empty) ? "0" : ((Control)(object)txtRAdd).Text);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Ax"].DefaultCellValue = (((Control)(object)txtRAxisDistance).Text.Equals(string.Empty) ? "0" : ((Control)(object)txtRAxisDistance).Text);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IPD"].DefaultCellValue = (((Control)(object)txtIPDDistance).Text.Equals(string.Empty) ? "0" : ((Control)(object)txtIPDDistance).Text);
	}

	public override void btnAttachFileClick()
	{
		if (RowID != "")
		{
			DataRow dataRow = null;
			if (base.Tag != null)
			{
				dataRow = (DataRow)base.Tag;
			}
			else if (GlobalVariables.dtForms.Select("FormFullName = '" + GetType().Namespace + "." + GetType().Name + "'").Length != 0)
			{
				dataRow = GlobalVariables.dtForms.Select("FormFullName = '" + GetType().Namespace + "." + GetType().Name + "'")[0];
			}
			bool option = GlobalFunctions.GetOption("ArchivingInEnglish");
			string text = dataRow[option ? "FormNameEn" : "FormNameAr"].ToString();
			string text2 = GlobalVariables.dtForms.Select("formID=" + dataRow["ParentID"])[0]["ParentID"].ToString();
			string text3 = GlobalVariables.dtForms.Select("formID=" + text2)[0][option ? "FormNameEn" : "FormNameAr"].ToString();
			string text4 = text3 + "\\" + text + "\\" + ((Control)(object)txtCode).Text.Replace('\\', '-').Replace('/', '-').Replace('*', '-')
				.Replace('?', '-')
				.Replace('؟', '-')
				.Replace(':', '-')
				.Replace('<', '-')
				.Replace('>', '-')
				.Replace('"', '-') + "\\";
			string path = text4 + RowID + "\\";
			frmAttachFiles frmAttachFiles2 = new frmAttachFiles(path, text4, RowID, ((Control)(object)btnUpdate).Enabled, ((Control)(object)btnDelete).Enabled, IsFromServer: true);
			frmAttachFiles2.ShowDialog();
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
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Expected O, but got Unknown
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Expected O, but got Unknown
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Expected O, but got Unknown
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Expected O, but got Unknown
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Expected O, but got Unknown
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Expected O, but got Unknown
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Expected O, but got Unknown
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Expected O, but got Unknown
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Expected O, but got Unknown
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Expected O, but got Unknown
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Expected O, but got Unknown
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Expected O, but got Unknown
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Expected O, but got Unknown
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Expected O, but got Unknown
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Expected O, but got Unknown
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Expected O, but got Unknown
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Expected O, but got Unknown
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Expected O, but got Unknown
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Expected O, but got Unknown
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Expected O, but got Unknown
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Expected O, but got Unknown
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Expected O, but got Unknown
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Expected O, but got Unknown
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Expected O, but got Unknown
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Expected O, but got Unknown
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Expected O, but got Unknown
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Expected O, but got Unknown
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Expected O, but got Unknown
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Expected O, but got Unknown
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Expected O, but got Unknown
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Expected O, but got Unknown
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Expected O, but got Unknown
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Expected O, but got Unknown
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Expected O, but got Unknown
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Expected O, but got Unknown
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Expected O, but got Unknown
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Expected O, but got Unknown
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Expected O, but got Unknown
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Expected O, but got Unknown
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Expected O, but got Unknown
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Expected O, but got Unknown
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Expected O, but got Unknown
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Expected O, but got Unknown
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Expected O, but got Unknown
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Expected O, but got Unknown
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Expected O, but got Unknown
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Expected O, but got Unknown
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Expected O, but got Unknown
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Expected O, but got Unknown
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Expected O, but got Unknown
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Expected O, but got Unknown
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Expected O, but got Unknown
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Expected O, but got Unknown
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Expected O, but got Unknown
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Expected O, but got Unknown
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Expected O, but got Unknown
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Expected O, but got Unknown
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Expected O, but got Unknown
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Expected O, but got Unknown
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Expected O, but got Unknown
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Expected O, but got Unknown
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Expected O, but got Unknown
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Expected O, but got Unknown
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Expected O, but got Unknown
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Expected O, but got Unknown
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Expected O, but got Unknown
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Expected O, but got Unknown
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Expected O, but got Unknown
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Expected O, but got Unknown
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Expected O, but got Unknown
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Expected O, but got Unknown
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Expected O, but got Unknown
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Expected O, but got Unknown
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Expected O, but got Unknown
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Expected O, but got Unknown
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Expected O, but got Unknown
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Expected O, but got Unknown
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Expected O, but got Unknown
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Expected O, but got Unknown
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Expected O, but got Unknown
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Expected O, but got Unknown
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Expected O, but got Unknown
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Expected O, but got Unknown
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_0582: Expected O, but got Unknown
		//IL_0583: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Expected O, but got Unknown
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0598: Expected O, but got Unknown
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a3: Expected O, but got Unknown
		//IL_05a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Expected O, but got Unknown
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Expected O, but got Unknown
		//IL_05ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c4: Expected O, but got Unknown
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Expected O, but got Unknown
		//IL_05d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05da: Expected O, but got Unknown
		//IL_05db: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Expected O, but got Unknown
		//IL_05e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f0: Expected O, but got Unknown
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Expected O, but got Unknown
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0606: Expected O, but got Unknown
		//IL_0607: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Expected O, but got Unknown
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_061c: Expected O, but got Unknown
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0627: Expected O, but got Unknown
		//IL_0cc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cce: Expected O, but got Unknown
		//IL_0d0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d16: Expected O, but got Unknown
		//IL_10d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_10de: Expected O, but got Unknown
		//IL_1134: Unknown result type (might be due to invalid IL or missing references)
		//IL_113e: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.LensesProductions.Transactions.frmBlanksInvoices));
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
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGDataAdditions = new UltraGrid();
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.ULGDataPayments = new UltraGrid();
		this.lblCommercialTax = new UltraLabel();
		this.txtCommercialTax = new UltraTextEditor();
		this.lblDiscAfterTaxRatio = new UltraLabel();
		this.txtDiscAfterTaxRatio = new UltraTextEditor();
		this.lblDiscAfterTaxValue = new UltraLabel();
		this.txtDiscAfterTaxValue = new UltraTextEditor();
		this.lblTaxTotalValue = new UltraLabel();
		this.txtTaxTotalValue = new UltraTextEditor();
		this.lblDiscBeforeTaxRatio = new UltraLabel();
		this.txtDiscBeforeTaxRatio = new UltraTextEditor();
		this.lblDiscBeforeTaxValue = new UltraLabel();
		this.txtDiscBeforeTaxValue = new UltraTextEditor();
		this.lblGrossValue = new UltraLabel();
		this.txtGrossValue = new UltraTextEditor();
		this.lblNetPrice = new UltraLabel();
		this.txtNetprice = new UltraTextEditor();
		this.lblPaidAmount = new UltraLabel();
		this.txtPaidAmount = new UltraTextEditor();
		this.txtRestAmount = new UltraTextEditor();
		this.lblRestAmount = new UltraLabel();
		this.btnPriceTypeSearch = new UltraButton();
		this.lblPriceType = new UltraLabel();
		this.cboPriceType = new UltraComboEditor();
		this.btnClientSearch = new UltraButton();
		this.lblClient = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.cboClient = new UltraComboEditor();
		this.cboTax = new UltraComboEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.chkTax = new UltraCheckEditor();
		this.btnClientBalance = new UltraButton();
		this.cboClientCode = new UltraComboEditor();
		this.lblBalance = new UltraLabel();
		this.txtBranchBalance = new UltraTextEditor();
		this.txtTotalQty = new UltraTextEditor();
		this.ultraLabel1 = new UltraLabel();
		this.lblShiftNo = new UltraLabel();
		this.txtShiftNo = new UltraTextEditor();
		this.lblShiftDate = new UltraLabel();
		this.dtpShiftDate = new UltraDateTimeEditor();
		this.cboBranches = new UltraComboEditor();
		this.lblSalesMan = new UltraLabel();
		this.cboSalesMan = new UltraComboEditor();
		this.btnSalesManSearch = new UltraButton();
		this.txtClientCardNo = new UltraTextEditor();
		this.lblClientCard = new UltraLabel();
		this.lblTravelNo = new UltraLabel();
		this.cboTravelNo = new UltraComboEditor();
		this.lblBranches = new UltraLabel();
		this.btnBranchesSearch = new UltraButton();
		this.btnSalesMan2Search = new UltraButton();
		this.lblSalesMan2 = new UltraLabel();
		this.cboSalesMan2 = new UltraComboEditor();
		this.chkApplyTax = new UltraCheckEditor();
		this.cboLine = new UltraComboEditor();
		this.lblLine = new UltraLabel();
		this.btnLineSearch = new UltraButton();
		this.cboVisaType = new UltraComboEditor();
		this.chkVisa = new UltraCheckEditor();
		this.txtVisaNo = new UltraTextEditor();
		this.lblVisaNo = new UltraLabel();
		this.dtpDeliverdDate = new UltraDateTimeEditor();
		this.chkIsDeliverd = new UltraCheckEditor();
		this.txtVisionTestClient = new UltraTextEditor();
		this.ultraLabel2 = new UltraLabel();
		this.UGBGlassesHistory = new UltraGroupBox();
		this.btnLTranspose = new UltraButton();
		this.btnRTranspose = new UltraButton();
		this.dtpGlassesHistoryDate = new UltraDateTimeEditor();
		this.lblDoctor = new UltraLabel();
		this.ultraLabel3 = new UltraLabel();
		this.cboDoctor = new UltraComboEditor();
		this.btnAddHistory = new UltraButton();
		this.lblR = new UltraLabel();
		this.cboRColorDistance = new UltraComboEditor();
		this.lblRSph = new UltraLabel();
		this.lblDistance = new UltraLabel();
		this.txtRAdd = new UltraTextEditor();
		this.lblRAxis = new UltraLabel();
		this.lblRCyl = new UltraLabel();
		this.txtRAxisDistance = new UltraTextEditor();
		this.cboRSizeDistance = new UltraComboEditor();
		this.cboRColorReading = new UltraComboEditor();
		this.txtRAxisReading = new UltraTextEditor();
		this.cboRSizeReading = new UltraComboEditor();
		this.lblRAdd = new UltraLabel();
		this.lblL = new UltraLabel();
		this.cboLColorDistance = new UltraComboEditor();
		this.lblLSph = new UltraLabel();
		this.txtLAdd = new UltraTextEditor();
		this.lblLAxis = new UltraLabel();
		this.lblLCyl = new UltraLabel();
		this.txtLAxisDistance = new UltraTextEditor();
		this.cboLSizeDistance = new UltraComboEditor();
		this.cboLColorReading = new UltraComboEditor();
		this.txtLAxisReading = new UltraTextEditor();
		this.cboLSizeReading = new UltraComboEditor();
		this.lblLAdd = new UltraLabel();
		this.lblReading = new UltraLabel();
		this.lblIPD = new UltraLabel();
		this.lblmmDistance = new UltraLabel();
		this.lblIPDDistance = new UltraLabel();
		this.lblmmReading = new UltraLabel();
		this.lblIPDReading = new UltraLabel();
		this.txtIPDReading = new UltraTextEditor();
		this.txtIPDDistance = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataAdditions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataPayments).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCommercialTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscAfterTaxRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscAfterTaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaidAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRestAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClientCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBranchBalance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtShiftNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpShiftDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesMan).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientCardNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTravelNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesMan2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkApplyTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLine).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkVisa).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDeliverdDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsDeliverd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisionTestClient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBGlassesHistory).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dtpGlassesHistoryDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDoctor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboRColorDistance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRAdd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRAxisDistance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboRSizeDistance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboRColorReading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRAxisReading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboRSizeReading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLColorDistance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLAdd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLAxisDistance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLSizeDistance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLColorReading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLAxisReading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLSizeReading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtIPDReading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtIPDDistance).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		val.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val, "ultraTab1");
		((SubObjectBase)val).ForceApplyResources = "";
		val2.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val2, "ultraTab2");
		((SubObjectBase)val2).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[2] { val, val2 });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl3, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val6).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val7).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val7).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val7).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val8).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(base.ULGData, "ULGData");
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.AfterExitEditMode += new System.EventHandler(ULGData_AfterExitEditMode);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		((AppearanceBase)val10).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val10).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance18");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val10;
		resources.ApplyResources(base.txtCode, "txtCode");
		((System.Windows.Forms.Control)(object)base.txtCode).TabStop = false;
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.btnClose, "btnClose");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataAdditions);
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		((UltraGridBase)this.ULGDataAdditions).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataAdditions).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataAdditions).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((UltraGridBase)this.ULGDataAdditions).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGDataAdditions).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val12).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val12).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val12).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataAdditions).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGDataAdditions).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val13).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataAdditions).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val13;
		((AppearanceBase)val14).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val14).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)2;
		((UltraGridBase)this.ULGDataAdditions).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGDataAdditions).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataAdditions).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val15).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val15).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val15).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Black;
		((UltraGridBase)this.ULGDataAdditions).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGDataAdditions).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataAdditions).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataAdditions).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataAdditions).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(this.ULGDataAdditions, "ULGDataAdditions");
		((System.Windows.Forms.Control)(object)this.ULGDataAdditions).Name = "ULGDataAdditions";
		((UltraControlBase)this.ULGDataAdditions).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataAdditions.AfterCellUpdate += new CellEventHandler(ULGDataAdditions_AfterCellUpdate);
		this.ULGDataAdditions.AfterEnterEditMode += new System.EventHandler(ULGDataAdditions_AfterEnterEditMode);
		this.ULGDataAdditions.AfterExitEditMode += new System.EventHandler(ULGDataAdditions_AfterExitEditMode);
		this.ULGDataAdditions.AfterRowsDeleted += new System.EventHandler(ULGDataAdditions_AfterRowsDeleted);
		this.ULGDataAdditions.CellListSelect += new CellEventHandler(ULGDataAdditions_CellListSelect);
		((System.Windows.Forms.Control)(object)this.ULGDataAdditions).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGDataAdditions_KeyDown);
		((System.Windows.Forms.Control)(object)this.ULGDataAdditions).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGDataAdditions_KeyPress);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataPayments);
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val16;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val17).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val17).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val17).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val17).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val17;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val18).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val18;
		((AppearanceBase)val19).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val19).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val19).BackGradientStyle = (GradientStyle)2;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val19;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val20).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val20).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val20).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Black;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val20;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(this.ULGDataPayments, "ULGDataPayments");
		((System.Windows.Forms.Control)(object)this.ULGDataPayments).Name = "ULGDataPayments";
		((UltraControlBase)this.ULGDataPayments).UseFlatMode = (DefaultableBoolean)1;
		resources.ApplyResources(this.lblCommercialTax, "lblCommercialTax");
		this.lblCommercialTax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCommercialTax).Name = "lblCommercialTax";
		((ControlBase)this.lblCommercialTax).WrapText = false;
		resources.ApplyResources(this.txtCommercialTax, "txtCommercialTax");
		((System.Windows.Forms.Control)(object)this.txtCommercialTax).Name = "txtCommercialTax";
		((TextEditorControlBase)this.txtCommercialTax).ValueChanged += new System.EventHandler(txtAdditionalValues_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtCommercialTax).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblDiscAfterTaxRatio, "lblDiscAfterTaxRatio");
		this.lblDiscAfterTaxRatio.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscAfterTaxRatio).Name = "lblDiscAfterTaxRatio";
		((ControlBase)this.lblDiscAfterTaxRatio).WrapText = false;
		resources.ApplyResources(this.txtDiscAfterTaxRatio, "txtDiscAfterTaxRatio");
		((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxRatio).Name = "txtDiscAfterTaxRatio";
		((TextEditorControlBase)this.txtDiscAfterTaxRatio).ValueChanged += new System.EventHandler(txtDiscAfterTaxRatio_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxRatio).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblDiscAfterTaxValue, "lblDiscAfterTaxValue");
		this.lblDiscAfterTaxValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscAfterTaxValue).Name = "lblDiscAfterTaxValue";
		((ControlBase)this.lblDiscAfterTaxValue).WrapText = false;
		resources.ApplyResources(this.txtDiscAfterTaxValue, "txtDiscAfterTaxValue");
		((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxValue).Name = "txtDiscAfterTaxValue";
		((TextEditorControlBase)this.txtDiscAfterTaxValue).ValueChanged += new System.EventHandler(txtDiscAfterTaxValue_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblTaxTotalValue, "lblTaxTotalValue");
		this.lblTaxTotalValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaxTotalValue).Name = "lblTaxTotalValue";
		((ControlBase)this.lblTaxTotalValue).WrapText = false;
		resources.ApplyResources(this.txtTaxTotalValue, "txtTaxTotalValue");
		((System.Windows.Forms.Control)(object)this.txtTaxTotalValue).Name = "txtTaxTotalValue";
		((EditorButtonControlBase)this.txtTaxTotalValue).ReadOnly = true;
		resources.ApplyResources(this.lblDiscBeforeTaxRatio, "lblDiscBeforeTaxRatio");
		this.lblDiscBeforeTaxRatio.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio).Name = "lblDiscBeforeTaxRatio";
		((ControlBase)this.lblDiscBeforeTaxRatio).WrapText = false;
		resources.ApplyResources(this.txtDiscBeforeTaxRatio, "txtDiscBeforeTaxRatio");
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio).Name = "txtDiscBeforeTaxRatio";
		((TextEditorControlBase)this.txtDiscBeforeTaxRatio).ValueChanged += new System.EventHandler(txtDiscBeforeTaxRatio_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio).Enter += new System.EventHandler(textBox_Enter);
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblDiscBeforeTaxValue, "lblDiscBeforeTaxValue");
		this.lblDiscBeforeTaxValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue).Name = "lblDiscBeforeTaxValue";
		((ControlBase)this.lblDiscBeforeTaxValue).WrapText = false;
		resources.ApplyResources(this.txtDiscBeforeTaxValue, "txtDiscBeforeTaxValue");
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue).Name = "txtDiscBeforeTaxValue";
		((TextEditorControlBase)this.txtDiscBeforeTaxValue).ValueChanged += new System.EventHandler(txtDiscBeforeTaxValue_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue).Enter += new System.EventHandler(textBox_Enter);
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblGrossValue, "lblGrossValue");
		this.lblGrossValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGrossValue).Name = "lblGrossValue";
		((ControlBase)this.lblGrossValue).WrapText = false;
		resources.ApplyResources(this.txtGrossValue, "txtGrossValue");
		((System.Windows.Forms.Control)(object)this.txtGrossValue).Name = "txtGrossValue";
		((EditorButtonControlBase)this.txtGrossValue).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtGrossValue).TabStop = false;
		resources.ApplyResources(this.lblNetPrice, "lblNetPrice");
		((System.Windows.Forms.Control)(object)this.lblNetPrice).Name = "lblNetPrice";
		resources.ApplyResources(this.txtNetprice, "txtNetprice");
		((System.Windows.Forms.Control)(object)this.txtNetprice).Name = "txtNetprice";
		((EditorButtonControlBase)this.txtNetprice).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtNetprice).TabStop = false;
		resources.ApplyResources(this.lblPaidAmount, "lblPaidAmount");
		this.lblPaidAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPaidAmount).Name = "lblPaidAmount";
		((ControlBase)this.lblPaidAmount).WrapText = false;
		resources.ApplyResources(this.txtPaidAmount, "txtPaidAmount");
		((System.Windows.Forms.Control)(object)this.txtPaidAmount).Name = "txtPaidAmount";
		((TextEditorControlBase)this.txtPaidAmount).ValueChanged += new System.EventHandler(txtPaidAmount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtPaidAmount).Enter += new System.EventHandler(textBox_Enter);
		((System.Windows.Forms.Control)(object)this.txtPaidAmount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.txtRestAmount, "txtRestAmount");
		((System.Windows.Forms.Control)(object)this.txtRestAmount).Name = "txtRestAmount";
		((EditorButtonControlBase)this.txtRestAmount).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtRestAmount).TabStop = false;
		((TextEditorControlBase)this.txtRestAmount).ValueChanged += new System.EventHandler(txtAdditionalValues_ValueChanged);
		resources.ApplyResources(this.lblRestAmount, "lblRestAmount");
		this.lblRestAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRestAmount).Name = "lblRestAmount";
		((ControlBase)this.lblRestAmount).WrapText = false;
		((AppearanceBase)val21).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnPriceTypeSearch).Appearance = (AppearanceBase)(object)val21;
		resources.ApplyResources(this.btnPriceTypeSearch, "btnPriceTypeSearch");
		((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch).Name = "btnPriceTypeSearch";
		((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch).Click += new System.EventHandler(btnPriceTypeSearch_Click);
		this.lblPriceType.AutoEllipsis = false;
		resources.ApplyResources(this.lblPriceType, "lblPriceType");
		((System.Windows.Forms.Control)(object)this.lblPriceType).Name = "lblPriceType";
		((ControlBase)this.lblPriceType).WrapText = false;
		((TextEditorControlBase)this.cboPriceType).AlwaysInEditMode = true;
		this.cboPriceType.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboPriceType, "cboPriceType");
		((System.Windows.Forms.Control)(object)this.cboPriceType).Name = "cboPriceType";
		((System.Windows.Forms.Control)(object)this.cboPriceType).TabStop = false;
		((TextEditorControlBase)this.cboPriceType).ValueChanged += new System.EventHandler(cboPriceType_ValueChanged);
		((AppearanceBase)val22).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnClientSearch).Appearance = (AppearanceBase)(object)val22;
		resources.ApplyResources(this.btnClientSearch, "btnClientSearch");
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Name = "btnClientSearch";
		((System.Windows.Forms.Control)(object)this.btnClientSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Click += new System.EventHandler(btnClientSearch_Click);
		this.lblClient.AutoEllipsis = false;
		resources.ApplyResources(this.lblClient, "lblClient");
		((System.Windows.Forms.Control)(object)this.lblClient).Name = "lblClient";
		((ControlBase)this.lblClient).WrapText = false;
		this.lblNotes.AutoEllipsis = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		((TextEditorControlBase)this.cboClient).AlwaysInEditMode = true;
		this.cboClient.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboClient, "cboClient");
		((System.Windows.Forms.Control)(object)this.cboClient).Name = "cboClient";
		((TextEditorControlBase)this.cboClient).ValueChanged += new System.EventHandler(cboClient_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboClient).Enter += new System.EventHandler(cboClient_Enter);
		((System.Windows.Forms.Control)(object)this.cboClient).KeyDown += new System.Windows.Forms.KeyEventHandler(cboClient_KeyDown);
		((System.Windows.Forms.Control)(object)this.cboClient).Leave += new System.EventHandler(cboClient_Leave);
		((TextEditorControlBase)this.cboTax).AlwaysInEditMode = true;
		this.cboTax.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboTax, "cboTax");
		((System.Windows.Forms.Control)(object)this.cboTax).Name = "cboTax";
		((EditorButtonControlBase)this.cboTax).ReadOnly = true;
		((TextEditorControlBase)this.cboTax).ValueChanged += new System.EventHandler(cboTax_ValueChanged);
		this.lblDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblDate, "lblDate");
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		((System.Windows.Forms.Control)(object)this.dtpDate).TabStop = false;
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.chkTax, "chkTax");
		((System.Windows.Forms.Control)(object)this.chkTax).Name = "chkTax";
		((UltraToggleEditorBase)this.chkTax).CheckedChanged += new System.EventHandler(chkTax_CheckedChanged);
		((AppearanceBase)val23).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val23).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints");
		((ControlBase)this.btnClientBalance).Appearance = (AppearanceBase)(object)val23;
		resources.ApplyResources(this.btnClientBalance, "btnClientBalance");
		((System.Windows.Forms.Control)(object)this.btnClientBalance).Name = "btnClientBalance";
		((System.Windows.Forms.Control)(object)this.btnClientBalance).Click += new System.EventHandler(btnClientBalance_Click);
		((TextEditorControlBase)this.cboClientCode).AlwaysInEditMode = true;
		this.cboClientCode.AutoCompleteMode = (AutoCompleteMode)3;
		resources.ApplyResources(this.cboClientCode, "cboClientCode");
		((System.Windows.Forms.Control)(object)this.cboClientCode).Name = "cboClientCode";
		((TextEditorControlBase)this.cboClientCode).ValueChanged += new System.EventHandler(cboClientCode_ValueChanged);
		this.lblBalance.AutoEllipsis = false;
		resources.ApplyResources(this.lblBalance, "lblBalance");
		((System.Windows.Forms.Control)(object)this.lblBalance).Name = "lblBalance";
		((ControlBase)this.lblBalance).WrapText = false;
		resources.ApplyResources(this.txtBranchBalance, "txtBranchBalance");
		((System.Windows.Forms.Control)(object)this.txtBranchBalance).Name = "txtBranchBalance";
		((EditorButtonControlBase)this.txtBranchBalance).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtBranchBalance).TabStop = false;
		resources.ApplyResources(this.txtTotalQty, "txtTotalQty");
		((System.Windows.Forms.Control)(object)this.txtTotalQty).Name = "txtTotalQty";
		((EditorButtonControlBase)this.txtTotalQty).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtTotalQty).TabStop = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		this.lblShiftNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblShiftNo, "lblShiftNo");
		((System.Windows.Forms.Control)(object)this.lblShiftNo).Name = "lblShiftNo";
		((ControlBase)this.lblShiftNo).WrapText = false;
		resources.ApplyResources(this.txtShiftNo, "txtShiftNo");
		((System.Windows.Forms.Control)(object)this.txtShiftNo).Name = "txtShiftNo";
		((EditorButtonControlBase)this.txtShiftNo).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtShiftNo).TabStop = false;
		this.lblShiftDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblShiftDate, "lblShiftDate");
		((System.Windows.Forms.Control)(object)this.lblShiftDate).Name = "lblShiftDate";
		((ControlBase)this.lblShiftDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpShiftDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpShiftDate, "dtpShiftDate");
		((System.Windows.Forms.Control)(object)this.dtpShiftDate).Name = "dtpShiftDate";
		((EditorButtonControlBase)this.dtpShiftDate).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.dtpShiftDate).TabStop = false;
		((TextEditorControlBase)this.cboBranches).AlwaysInEditMode = true;
		this.cboBranches.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboBranches, "cboBranches");
		((System.Windows.Forms.Control)(object)this.cboBranches).Name = "cboBranches";
		((EditorButtonControlBase)this.cboBranches).ReadOnly = true;
		((TextEditorControlBase)this.cboBranches).ValueChanged += new System.EventHandler(cboBranches_ValueChanged);
		this.lblSalesMan.AutoEllipsis = false;
		resources.ApplyResources(this.lblSalesMan, "lblSalesMan");
		((System.Windows.Forms.Control)(object)this.lblSalesMan).Name = "lblSalesMan";
		((ControlBase)this.lblSalesMan).WrapText = false;
		((TextEditorControlBase)this.cboSalesMan).AlwaysInEditMode = true;
		this.cboSalesMan.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboSalesMan, "cboSalesMan");
		((System.Windows.Forms.Control)(object)this.cboSalesMan).Name = "cboSalesMan";
		((System.Windows.Forms.Control)(object)this.cboSalesMan).TabStop = false;
		((AppearanceBase)val24).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnSalesManSearch).Appearance = (AppearanceBase)(object)val24;
		resources.ApplyResources(this.btnSalesManSearch, "btnSalesManSearch");
		((System.Windows.Forms.Control)(object)this.btnSalesManSearch).Name = "btnSalesManSearch";
		((System.Windows.Forms.Control)(object)this.btnSalesManSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnSalesManSearch).Click += new System.EventHandler(btnSalesManSearch_Click);
		resources.ApplyResources(this.txtClientCardNo, "txtClientCardNo");
		((System.Windows.Forms.Control)(object)this.txtClientCardNo).Name = "txtClientCardNo";
		((System.Windows.Forms.Control)(object)this.txtClientCardNo).KeyUp += new System.Windows.Forms.KeyEventHandler(txtClientCardNo_KeyUp);
		this.lblClientCard.AutoEllipsis = false;
		resources.ApplyResources(this.lblClientCard, "lblClientCard");
		((System.Windows.Forms.Control)(object)this.lblClientCard).Name = "lblClientCard";
		((ControlBase)this.lblClientCard).WrapText = false;
		this.lblTravelNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblTravelNo, "lblTravelNo");
		((System.Windows.Forms.Control)(object)this.lblTravelNo).Name = "lblTravelNo";
		((ControlBase)this.lblTravelNo).WrapText = false;
		((TextEditorControlBase)this.cboTravelNo).AlwaysInEditMode = true;
		this.cboTravelNo.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboTravelNo, "cboTravelNo");
		((System.Windows.Forms.Control)(object)this.cboTravelNo).Name = "cboTravelNo";
		((System.Windows.Forms.Control)(object)this.cboTravelNo).TabStop = false;
		this.lblBranches.AutoEllipsis = false;
		resources.ApplyResources(this.lblBranches, "lblBranches");
		((System.Windows.Forms.Control)(object)this.lblBranches).Name = "lblBranches";
		((ControlBase)this.lblBranches).WrapText = false;
		((AppearanceBase)val25).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnBranchesSearch).Appearance = (AppearanceBase)(object)val25;
		resources.ApplyResources(this.btnBranchesSearch, "btnBranchesSearch");
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).Name = "btnBranchesSearch";
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).Click += new System.EventHandler(btnBranchesSearch_Click);
		((AppearanceBase)val26).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnSalesMan2Search).Appearance = (AppearanceBase)(object)val26;
		resources.ApplyResources(this.btnSalesMan2Search, "btnSalesMan2Search");
		((System.Windows.Forms.Control)(object)this.btnSalesMan2Search).Name = "btnSalesMan2Search";
		((System.Windows.Forms.Control)(object)this.btnSalesMan2Search).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnSalesMan2Search).Click += new System.EventHandler(btnSalesMan2Search_Click);
		this.lblSalesMan2.AutoEllipsis = false;
		resources.ApplyResources(this.lblSalesMan2, "lblSalesMan2");
		((System.Windows.Forms.Control)(object)this.lblSalesMan2).Name = "lblSalesMan2";
		((ControlBase)this.lblSalesMan2).WrapText = false;
		((TextEditorControlBase)this.cboSalesMan2).AlwaysInEditMode = true;
		this.cboSalesMan2.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboSalesMan2, "cboSalesMan2");
		((System.Windows.Forms.Control)(object)this.cboSalesMan2).Name = "cboSalesMan2";
		((System.Windows.Forms.Control)(object)this.cboSalesMan2).TabStop = false;
		resources.ApplyResources(this.chkApplyTax, "chkApplyTax");
		((System.Windows.Forms.Control)(object)this.chkApplyTax).Name = "chkApplyTax";
		((UltraToggleEditorBase)this.chkApplyTax).CheckedChanged += new System.EventHandler(chkApplyTax_CheckedChanged);
		((TextEditorControlBase)this.cboLine).AlwaysInEditMode = true;
		this.cboLine.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboLine, "cboLine");
		((System.Windows.Forms.Control)(object)this.cboLine).Name = "cboLine";
		((EditorButtonControlBase)this.cboLine).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.cboLine).TabStop = false;
		this.lblLine.AutoEllipsis = false;
		resources.ApplyResources(this.lblLine, "lblLine");
		((System.Windows.Forms.Control)(object)this.lblLine).Name = "lblLine";
		((ControlBase)this.lblLine).WrapText = false;
		((AppearanceBase)val27).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnLineSearch).Appearance = (AppearanceBase)(object)val27;
		resources.ApplyResources(this.btnLineSearch, "btnLineSearch");
		((System.Windows.Forms.Control)(object)this.btnLineSearch).Name = "btnLineSearch";
		((System.Windows.Forms.Control)(object)this.btnLineSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnLineSearch).Click += new System.EventHandler(btnLineSearch_Click);
		((TextEditorControlBase)this.cboVisaType).AlwaysInEditMode = true;
		resources.ApplyResources(this.cboVisaType, "cboVisaType");
		this.cboVisaType.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboVisaType).Name = "cboVisaType";
		((System.Windows.Forms.Control)(object)this.cboVisaType).TabStop = false;
		resources.ApplyResources(this.chkVisa, "chkVisa");
		((System.Windows.Forms.Control)(object)this.chkVisa).Name = "chkVisa";
		((UltraToggleEditorBase)this.chkVisa).CheckedChanged += new System.EventHandler(chkVisa_CheckedChanged);
		resources.ApplyResources(this.txtVisaNo, "txtVisaNo");
		((System.Windows.Forms.Control)(object)this.txtVisaNo).Name = "txtVisaNo";
		resources.ApplyResources(this.lblVisaNo, "lblVisaNo");
		this.lblVisaNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaNo).Name = "lblVisaNo";
		((ControlBase)this.lblVisaNo).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpDeliverdDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpDeliverdDate, "dtpDeliverdDate");
		this.dtpDeliverdDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDeliverdDate).Name = "dtpDeliverdDate";
		((EditorButtonControlBase)this.dtpDeliverdDate).ReadOnly = true;
		resources.ApplyResources(this.chkIsDeliverd, "chkIsDeliverd");
		((System.Windows.Forms.Control)(object)this.chkIsDeliverd).Name = "chkIsDeliverd";
		resources.ApplyResources(this.txtVisionTestClient, "txtVisionTestClient");
		((System.Windows.Forms.Control)(object)this.txtVisionTestClient).Name = "txtVisionTestClient";
		((System.Windows.Forms.Control)(object)this.txtVisionTestClient).KeyUp += new System.Windows.Forms.KeyEventHandler(txtClientCardNo_KeyUp);
		this.ultraLabel2.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.UGBGlassesHistory, "UGBGlassesHistory");
		this.UGBGlassesHistory.BorderStyle = (GroupBoxBorderStyle)4;
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.btnLTranspose);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.btnRTranspose);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.dtpGlassesHistoryDate);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblDoctor);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
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
		((AppearanceBase)val28).Image = ERP.Properties.Resources.Productions;
		((ControlBase)this.btnLTranspose).Appearance = (AppearanceBase)(object)val28;
		resources.ApplyResources(this.btnLTranspose, "btnLTranspose");
		((System.Windows.Forms.Control)(object)this.btnLTranspose).Name = "btnLTranspose";
		((System.Windows.Forms.Control)(object)this.btnLTranspose).Click += new System.EventHandler(btnLTranspose_Click);
		((AppearanceBase)val29).Image = ERP.Properties.Resources.Productions;
		((ControlBase)this.btnRTranspose).Appearance = (AppearanceBase)(object)val29;
		resources.ApplyResources(this.btnRTranspose, "btnRTranspose");
		((System.Windows.Forms.Control)(object)this.btnRTranspose).Name = "btnRTranspose";
		((System.Windows.Forms.Control)(object)this.btnRTranspose).Click += new System.EventHandler(btnRTranspose_Click);
		((UltraWinEditorMaskedControlBase)this.dtpGlassesHistoryDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpGlassesHistoryDate, "dtpGlassesHistoryDate");
		((System.Windows.Forms.Control)(object)this.dtpGlassesHistoryDate).Name = "dtpGlassesHistoryDate";
		((EditorButtonControlBase)this.dtpGlassesHistoryDate).ReadOnly = true;
		((AppearanceBase)val30).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val30).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.lblDoctor).Appearance = (AppearanceBase)(object)val30;
		this.lblDoctor.AutoEllipsis = false;
		resources.ApplyResources(this.lblDoctor, "lblDoctor");
		((System.Windows.Forms.Control)(object)this.lblDoctor).Name = "lblDoctor";
		((ControlBase)this.lblDoctor).WrapText = false;
		((AppearanceBase)val31).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val31).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.ultraLabel3).Appearance = (AppearanceBase)(object)val31;
		this.ultraLabel3.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.cboDoctor, "cboDoctor");
		((System.Windows.Forms.Control)(object)this.cboDoctor).Name = "cboDoctor";
		((EditorButtonControlBase)this.cboDoctor).ReadOnly = true;
		((AppearanceBase)val32).Image = ERP.Properties.Resources.New;
		((ControlBase)this.btnAddHistory).Appearance = (AppearanceBase)(object)val32;
		resources.ApplyResources(this.btnAddHistory, "btnAddHistory");
		((System.Windows.Forms.Control)(object)this.btnAddHistory).Name = "btnAddHistory";
		((System.Windows.Forms.Control)(object)this.btnAddHistory).Click += new System.EventHandler(btnAddHistory_Click);
		resources.ApplyResources(this.lblR, "lblR");
		((System.Windows.Forms.Control)(object)this.lblR).Name = "lblR";
		this.cboRColorDistance.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		resources.ApplyResources(this.cboRColorDistance, "cboRColorDistance");
		((System.Windows.Forms.Control)(object)this.cboRColorDistance).Name = "cboRColorDistance";
		((EditorButtonControlBase)this.cboRColorDistance).ReadOnly = true;
		((AppearanceBase)val33).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val33).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.lblRSph).Appearance = (AppearanceBase)(object)val33;
		this.lblRSph.AutoEllipsis = false;
		resources.ApplyResources(this.lblRSph, "lblRSph");
		((System.Windows.Forms.Control)(object)this.lblRSph).Name = "lblRSph";
		((ControlBase)this.lblRSph).WrapText = false;
		((AppearanceBase)val34).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val34).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.lblDistance).Appearance = (AppearanceBase)(object)val34;
		this.lblDistance.AutoEllipsis = false;
		resources.ApplyResources(this.lblDistance, "lblDistance");
		((System.Windows.Forms.Control)(object)this.lblDistance).Name = "lblDistance";
		((ControlBase)this.lblDistance).WrapText = false;
		resources.ApplyResources(this.txtRAdd, "txtRAdd");
		((System.Windows.Forms.Control)(object)this.txtRAdd).Name = "txtRAdd";
		((EditorButtonControlBase)this.txtRAdd).ReadOnly = true;
		((AppearanceBase)val35).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val35).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.lblRAxis).Appearance = (AppearanceBase)(object)val35;
		this.lblRAxis.AutoEllipsis = false;
		resources.ApplyResources(this.lblRAxis, "lblRAxis");
		((System.Windows.Forms.Control)(object)this.lblRAxis).Name = "lblRAxis";
		((ControlBase)this.lblRAxis).WrapText = false;
		((AppearanceBase)val36).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val36).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.lblRCyl).Appearance = (AppearanceBase)(object)val36;
		this.lblRCyl.AutoEllipsis = false;
		resources.ApplyResources(this.lblRCyl, "lblRCyl");
		((System.Windows.Forms.Control)(object)this.lblRCyl).Name = "lblRCyl";
		((ControlBase)this.lblRCyl).WrapText = false;
		resources.ApplyResources(this.txtRAxisDistance, "txtRAxisDistance");
		((System.Windows.Forms.Control)(object)this.txtRAxisDistance).Name = "txtRAxisDistance";
		((EditorButtonControlBase)this.txtRAxisDistance).ReadOnly = true;
		this.cboRSizeDistance.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		resources.ApplyResources(this.cboRSizeDistance, "cboRSizeDistance");
		((System.Windows.Forms.Control)(object)this.cboRSizeDistance).Name = "cboRSizeDistance";
		((EditorButtonControlBase)this.cboRSizeDistance).ReadOnly = true;
		this.cboRColorReading.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		resources.ApplyResources(this.cboRColorReading, "cboRColorReading");
		((System.Windows.Forms.Control)(object)this.cboRColorReading).Name = "cboRColorReading";
		((EditorButtonControlBase)this.cboRColorReading).ReadOnly = true;
		resources.ApplyResources(this.txtRAxisReading, "txtRAxisReading");
		((System.Windows.Forms.Control)(object)this.txtRAxisReading).Name = "txtRAxisReading";
		((EditorButtonControlBase)this.txtRAxisReading).ReadOnly = true;
		this.cboRSizeReading.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		resources.ApplyResources(this.cboRSizeReading, "cboRSizeReading");
		((System.Windows.Forms.Control)(object)this.cboRSizeReading).Name = "cboRSizeReading";
		((EditorButtonControlBase)this.cboRSizeReading).ReadOnly = true;
		((AppearanceBase)val37).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val37).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.lblRAdd).Appearance = (AppearanceBase)(object)val37;
		this.lblRAdd.AutoEllipsis = false;
		resources.ApplyResources(this.lblRAdd, "lblRAdd");
		((System.Windows.Forms.Control)(object)this.lblRAdd).Name = "lblRAdd";
		((ControlBase)this.lblRAdd).WrapText = false;
		resources.ApplyResources(this.lblL, "lblL");
		((System.Windows.Forms.Control)(object)this.lblL).Name = "lblL";
		this.cboLColorDistance.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		resources.ApplyResources(this.cboLColorDistance, "cboLColorDistance");
		((System.Windows.Forms.Control)(object)this.cboLColorDistance).Name = "cboLColorDistance";
		((EditorButtonControlBase)this.cboLColorDistance).ReadOnly = true;
		((AppearanceBase)val38).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val38).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.lblLSph).Appearance = (AppearanceBase)(object)val38;
		this.lblLSph.AutoEllipsis = false;
		resources.ApplyResources(this.lblLSph, "lblLSph");
		((System.Windows.Forms.Control)(object)this.lblLSph).Name = "lblLSph";
		((ControlBase)this.lblLSph).WrapText = false;
		resources.ApplyResources(this.txtLAdd, "txtLAdd");
		((System.Windows.Forms.Control)(object)this.txtLAdd).Name = "txtLAdd";
		((EditorButtonControlBase)this.txtLAdd).ReadOnly = true;
		((AppearanceBase)val39).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val39).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.lblLAxis).Appearance = (AppearanceBase)(object)val39;
		this.lblLAxis.AutoEllipsis = false;
		resources.ApplyResources(this.lblLAxis, "lblLAxis");
		((System.Windows.Forms.Control)(object)this.lblLAxis).Name = "lblLAxis";
		((ControlBase)this.lblLAxis).WrapText = false;
		((AppearanceBase)val40).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val40).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.lblLCyl).Appearance = (AppearanceBase)(object)val40;
		this.lblLCyl.AutoEllipsis = false;
		resources.ApplyResources(this.lblLCyl, "lblLCyl");
		((System.Windows.Forms.Control)(object)this.lblLCyl).Name = "lblLCyl";
		((ControlBase)this.lblLCyl).WrapText = false;
		resources.ApplyResources(this.txtLAxisDistance, "txtLAxisDistance");
		((System.Windows.Forms.Control)(object)this.txtLAxisDistance).Name = "txtLAxisDistance";
		((EditorButtonControlBase)this.txtLAxisDistance).ReadOnly = true;
		this.cboLSizeDistance.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		resources.ApplyResources(this.cboLSizeDistance, "cboLSizeDistance");
		((System.Windows.Forms.Control)(object)this.cboLSizeDistance).Name = "cboLSizeDistance";
		((EditorButtonControlBase)this.cboLSizeDistance).ReadOnly = true;
		this.cboLColorReading.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		resources.ApplyResources(this.cboLColorReading, "cboLColorReading");
		((System.Windows.Forms.Control)(object)this.cboLColorReading).Name = "cboLColorReading";
		((EditorButtonControlBase)this.cboLColorReading).ReadOnly = true;
		resources.ApplyResources(this.txtLAxisReading, "txtLAxisReading");
		((System.Windows.Forms.Control)(object)this.txtLAxisReading).Name = "txtLAxisReading";
		((EditorButtonControlBase)this.txtLAxisReading).ReadOnly = true;
		this.cboLSizeReading.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		resources.ApplyResources(this.cboLSizeReading, "cboLSizeReading");
		((System.Windows.Forms.Control)(object)this.cboLSizeReading).Name = "cboLSizeReading";
		((EditorButtonControlBase)this.cboLSizeReading).ReadOnly = true;
		((AppearanceBase)val41).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val41).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.lblLAdd).Appearance = (AppearanceBase)(object)val41;
		this.lblLAdd.AutoEllipsis = false;
		resources.ApplyResources(this.lblLAdd, "lblLAdd");
		((System.Windows.Forms.Control)(object)this.lblLAdd).Name = "lblLAdd";
		((ControlBase)this.lblLAdd).WrapText = false;
		((AppearanceBase)val42).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val42).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.lblReading).Appearance = (AppearanceBase)(object)val42;
		this.lblReading.AutoEllipsis = false;
		resources.ApplyResources(this.lblReading, "lblReading");
		((System.Windows.Forms.Control)(object)this.lblReading).Name = "lblReading";
		((ControlBase)this.lblReading).WrapText = false;
		((AppearanceBase)val43).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val43).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.lblIPD).Appearance = (AppearanceBase)(object)val43;
		this.lblIPD.AutoEllipsis = false;
		resources.ApplyResources(this.lblIPD, "lblIPD");
		((System.Windows.Forms.Control)(object)this.lblIPD).Name = "lblIPD";
		((ControlBase)this.lblIPD).WrapText = false;
		((AppearanceBase)val44).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val44).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.lblmmDistance).Appearance = (AppearanceBase)(object)val44;
		this.lblmmDistance.AutoEllipsis = false;
		resources.ApplyResources(this.lblmmDistance, "lblmmDistance");
		((System.Windows.Forms.Control)(object)this.lblmmDistance).Name = "lblmmDistance";
		((ControlBase)this.lblmmDistance).WrapText = false;
		((AppearanceBase)val45).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val45).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.lblIPDDistance).Appearance = (AppearanceBase)(object)val45;
		this.lblIPDDistance.AutoEllipsis = false;
		resources.ApplyResources(this.lblIPDDistance, "lblIPDDistance");
		((System.Windows.Forms.Control)(object)this.lblIPDDistance).Name = "lblIPDDistance";
		((ControlBase)this.lblIPDDistance).WrapText = false;
		((AppearanceBase)val46).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val46).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.lblmmReading).Appearance = (AppearanceBase)(object)val46;
		this.lblmmReading.AutoEllipsis = false;
		resources.ApplyResources(this.lblmmReading, "lblmmReading");
		((System.Windows.Forms.Control)(object)this.lblmmReading).Name = "lblmmReading";
		((ControlBase)this.lblmmReading).WrapText = false;
		((AppearanceBase)val47).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val47).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.lblIPDReading).Appearance = (AppearanceBase)(object)val47;
		this.lblIPDReading.AutoEllipsis = false;
		resources.ApplyResources(this.lblIPDReading, "lblIPDReading");
		((System.Windows.Forms.Control)(object)this.lblIPDReading).Name = "lblIPDReading";
		((ControlBase)this.lblIPDReading).WrapText = false;
		resources.ApplyResources(this.txtIPDReading, "txtIPDReading");
		((System.Windows.Forms.Control)(object)this.txtIPDReading).Name = "txtIPDReading";
		((EditorButtonControlBase)this.txtIPDReading).ReadOnly = true;
		resources.ApplyResources(this.txtIPDDistance, "txtIPDDistance");
		((System.Windows.Forms.Control)(object)this.txtIPDDistance).Name = "txtIPDDistance";
		((EditorButtonControlBase)this.txtIPDDistance).ReadOnly = true;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBGlassesHistory);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDeliverdDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsDeliverd);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVisaNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkVisa);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboVisaType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSalesMan2Search);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesMan2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSalesMan2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnBranchesSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTravelNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTravelNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClientCard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVisionTestClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtClientCardNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSalesManSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesMan);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSalesMan);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblShiftNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtShiftNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblShiftDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpShiftDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClientCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnLineSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLine);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboLine);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBranchBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkApplyTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCommercialTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCommercialTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscAfterTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscAfterTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNetPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNetprice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPaidAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPaidAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRestAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRestAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Name = "frmBlanksInvoices";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRestAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRestAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPaidAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPaidAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNetprice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNetPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscAfterTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscAfterTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscAfterTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCommercialTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCommercialTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkApplyTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBranchBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboLine, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLine, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnLineSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClientCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpShiftDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblShiftDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtShiftNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblShiftNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSalesMan, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSalesMan, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSalesManSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtClientCardNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVisionTestClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClientCard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTravelNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTravelNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnBranchesSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSalesMan2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSalesMan2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSalesMan2Search, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboVisaType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkVisa, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVisaNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsDeliverd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDeliverdDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBGlassesHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UTCDetails, 0);
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
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataAdditions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataPayments).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCommercialTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscAfterTaxRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscAfterTaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaidAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRestAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClientCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBranchBalance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtShiftNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpShiftDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesMan).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientCardNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTravelNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesMan2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkApplyTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLine).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkVisa).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDeliverdDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsDeliverd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisionTestClient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBGlassesHistory).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dtpGlassesHistoryDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDoctor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboRColorDistance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRAdd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRAxisDistance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboRSizeDistance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboRColorReading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRAxisReading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboRSizeReading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLColorDistance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLAdd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLAxisDistance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLSizeDistance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLColorReading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLAxisReading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLSizeReading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtIPDReading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtIPDDistance).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
