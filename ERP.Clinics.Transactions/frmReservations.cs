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
using ERP.Clinics.Reports;
using ERP.Properties;
using ERP.StockControl.MasterData;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;

namespace ERP.Clinics.Transactions;

public class frmReservations : frmHeaderManyDetails
{
	private DataTable dtItems;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtItemPrices;

	private DataTable dtProceduresPrices;

	private DataTable dtDoctors;

	private DataTable dtPriceType;

	private DataTable dtBatchs;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtStores;

	private DataTable dtUnits;

	private DataTable dtClinics;

	private DataTable dtTaxs;

	private DataTable dtPatients;

	private DataTable dtVisaType;

	private DataTable dtPOSDefaultData;

	private DataTable dtCurrency;

	private DataTable dtUsers;

	private DataTable dtProcedures;

	private DataTable dtSteps;

	private DataTable dtReservationProcedures;

	private DataTable dtReservationProceduresSteps;

	private DataTable dtReservationProceduresStepsItems;

	private DataTable dtAccruals;

	private DataTable dtPayments;

	private DataTable dtReservationType = new DataTable();

	private ValueList vlVisaType = new ValueList();

	private ValueList vlUsers = new ValueList();

	private ValueList vlDoctors = new ValueList();

	private ValueList vlClinics = new ValueList();

	private ValueList vlProcedures = new ValueList();

	private ValueList vlSteps = new ValueList();

	private ValueList vlDetailsItems = new ValueList();

	private ValueList vlStepsItems = new ValueList();

	private ValueList vlDetailsBarCode = new ValueList();

	private ValueList vlStepsBarCode = new ValueList();

	private ValueList vlDetailsUnits = new ValueList();

	private ValueList vlDetailsStores = new ValueList();

	private ValueList vlDetailsBatchs = new ValueList();

	private ValueList vlStepsUnits = new ValueList();

	private ValueList vlStepsStores = new ValueList();

	private ValueList vlStepsBatchs = new ValueList();

	private ValueList vlDetailsTaxs = new ValueList();

	private ValueList vlProceduresTaxs = new ValueList();

	private ValueList vlDetailsColors = new ValueList();

	private ValueList vlStepsColors = new ValueList();

	private ValueList vlDetailsSizes = new ValueList();

	private ValueList vlStepsSizes = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private int NewPriceUserID = 0;

	private int DiscountUserID = 0;

	private decimal UserSalesDiscount = default(decimal);

	private int ReservationPaymentID = 0;

	private bool UsingBatchNoAndValidityPeriod = false;

	private bool UsingSalesDiscountLevels = false;

	private DataSet ds;

	private int newID = -100000;

	private int ClinicID;

	private int DoctorID;

	private int ClinicScheduleID;

	private DateTime dtAppointmentDate;

	private string OrderNo;

	private string ShiftDetailID;

	private string ShiftDetailUserID;

	private IContainer components = null;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboClinic;

	private UltraLabel lblClinics;

	private UltraLabel lblPatientName;

	private UltraComboEditor cboPatients;

	private UltraTextEditor txtOrderNo;

	private UltraLabel lblOrderNo;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	public UltraButton btnPatientSearch;

	private UltraLabel lblDoctor;

	private UltraComboEditor cboDoctor;

	private UltraLabel lblAppointmentDate;

	private UltraDateTimeEditor dtpAppointmentDate;

	private UltraTextEditor txtFees;

	private UltraCheckEditor chkConfirmed;

	private UltraCheckEditor chkCanceled;

	private UltraCheckEditor chkArrived;

	private UltraDateTimeEditor dtpArrivalDate;

	private UltraCheckEditor chkClosed;

	public UltraButton btnPatientAdd;

	private UltraLabel lblPaidAmount;

	private UltraTextEditor txtPaidAmount;

	private UltraCheckEditor chkVisa;

	private UltraTextEditor txtVisaNo;

	private UltraLabel lblVisaNo;

	private UltraComboEditor cboVisaType;

	private UltraButton btnOrderPayments;

	private UltraTabPageControl ultraTabPageControl2;

	protected internal UltraGrid ULGDataPayments;

	private UltraLabel lblRestAmount;

	private UltraTextEditor txtRestAmount;

	private UltraLabel lblNetPrice;

	private UltraTextEditor txtNetprice;

	private UltraLabel lblTaxTotalValue;

	private UltraTextEditor txtTaxTotalValue;

	private UltraLabel lblDiscBeforeTaxRatio;

	private UltraTextEditor txtDiscBeforeTaxRatio;

	private UltraLabel lblDiscBeforeTaxValue;

	private UltraTextEditor txtDiscBeforeTaxValue;

	private UltraLabel lblGrossValue;

	private UltraTextEditor txtGrossValue;

	private UltraLabel lblCommercialTax;

	private UltraTextEditor txtCommercialTax;

	private UltraCheckEditor chkStore;

	private UltraComboEditor cboStore;

	private UltraComboEditor cboTax;

	private UltraCheckEditor chkTax;

	public UltraButton btnPriceTypeSearch;

	private UltraLabel lblPriceType;

	private UltraComboEditor cboPriceType;

	private UltraTextEditor txtTotalQty;

	private UltraLabel ultraLabel1;

	private UltraTabPageControl ultraTabPageControl3;

	protected internal UltraGrid ULGAccruals;

	private UltraLabel lblType;

	private UltraComboEditor cboReservationType;

	private UltraCheckEditor chkIsExtended;

	private UltraTabPageControl ultraTabPageControl4;

	public UltraGrid ULGDataProcedures;

	private UltraLabel lblBalance;

	private UltraTextEditor txtBranchBalance;

	private UltraTextEditor txtFeesDiscValue;

	private UltraTextEditor txtFeesDiscRatio;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private UltraTextEditor txtFeesAmount;

	private UltraCheckEditor chkIsOnAccount;

	public UltraButton btnPatientHistory;

	public frmReservations()
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
		InitializeComponent();
		TableName = "CL_Reservations";
		IDCol = "ReservationID";
		NoCol = "ReservationCode";
		DateCol = "VoucherDate";
	}

	public frmReservations(int ID, int _clinicID, int _doctorID, string _orderNo, DateTime _appointmentdate, int _clinicschedule)
		: this()
	{
		RowID = ID.ToString();
		ClinicID = _clinicID;
		DoctorID = _doctorID;
		OrderNo = _orderNo;
		dtAppointmentDate = _appointmentdate;
		ClinicScheduleID = _clinicschedule;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtpAppointmentDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtpArrivalDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		if (UsingColors)
		{
			dtItemsColorCategorysDetails = ItemsColorCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlDetailsColors.ValueListItems.Clear();
			vlStepsColors.ValueListItems.Clear();
			for (int i = 0; i < dtColors.Rows.Count; i++)
			{
				vlDetailsColors.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
				vlStepsColors.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtItemsSizeCategorysDetails = ItemsSizeCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlDetailsSizes.ValueListItems.Clear();
			vlStepsSizes.ValueListItems.Clear();
			for (int j = 0; j < dtSizes.Rows.Count; j++)
			{
				vlDetailsSizes.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
				vlStepsSizes.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
			}
		}
		UserSalesDiscount = Users.SelectSalesDiscount(GlobalVariables.UserID, IsFromServer: false);
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		UsingSalesDiscountLevels = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from G_DiscountSettings ").Rows[0][0].ToString()) > 0;
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlDetailsBatchs.ValueListItems.Clear();
			vlStepsBatchs.ValueListItems.Clear();
			for (int k = 0; k < dtBatchs.Rows.Count; k++)
			{
				vlDetailsBatchs.ValueListItems.Add(dtBatchs.Rows[k]["BatchID"], dtBatchs.Rows[k]["BatchName"].ToString());
				vlStepsBatchs.ValueListItems.Add(dtBatchs.Rows[k]["BatchID"], dtBatchs.Rows[k]["BatchName"].ToString());
			}
		}
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlDetailsStores.ValueListItems.Clear();
		vlStepsStores.ValueListItems.Clear();
		for (int l = 0; l < dtStores.Rows.Count; l++)
		{
			vlDetailsStores.ValueListItems.Add(dtStores.Rows[l]["StoreID"], dtStores.Rows[l]["StoreName"].ToString());
			vlStepsStores.ValueListItems.Add(dtStores.Rows[l]["StoreID"], dtStores.Rows[l]["StoreName"].ToString());
		}
		GlobalFunctions.FillCombo(cboStore, dtStores, "StoreID", "StoreName");
		dtItems = Items.FillComboWithoutCode("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlDetailsItems.ValueListItems.Clear();
		vlStepsItems.ValueListItems.Clear();
		vlDetailsBarCode.ValueListItems.Clear();
		vlStepsBarCode.ValueListItems.Clear();
		for (int m = 0; m < dtItems.Rows.Count; m++)
		{
			vlDetailsItems.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["Name"].ToString());
			vlDetailsBarCode.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["ItemBarCode"].ToString());
			vlStepsItems.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["Name"].ToString());
			vlStepsBarCode.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["ItemBarCode"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlDetailsUnits.ValueListItems.Clear();
		vlStepsUnits.ValueListItems.Clear();
		for (int n = 0; n < dtUnits.Rows.Count; n++)
		{
			vlDetailsUnits.ValueListItems.Add(dtUnits.Rows[n]["UnitID"], dtUnits.Rows[n]["UnitName"].ToString());
			vlStepsUnits.ValueListItems.Add(dtUnits.Rows[n]["UnitID"], dtUnits.Rows[n]["UnitName"].ToString());
		}
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTax, dtTaxs, "TaxID", "TaxName");
		vlDetailsTaxs.ValueListItems.Clear();
		vlProceduresTaxs.ValueListItems.Clear();
		for (int num = 0; num < dtTaxs.Rows.Count; num++)
		{
			vlDetailsTaxs.ValueListItems.Add(dtTaxs.Rows[num]["TaxID"], dtTaxs.Rows[num]["TaxName"].ToString());
			vlProceduresTaxs.ValueListItems.Add(dtTaxs.Rows[num]["TaxID"], dtTaxs.Rows[num]["TaxName"].ToString());
		}
		dtPOSDefaultData = BusinessLayer.POS.Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		dtDoctors = Doctors.FillCombo2(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDoctor, dtDoctors, "DoctorID", "DoctorName");
		vlDoctors.ValueListItems.Clear();
		for (int num2 = 0; num2 < dtDoctors.Rows.Count; num2++)
		{
			vlDoctors.ValueListItems.Add(dtDoctors.Rows[num2]["DoctorID"], dtDoctors.Rows[num2]["DoctorName"].ToString());
		}
		dtClinics = BusinessLayer.Clinics.Clinics.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClinic, dtClinics, "ClinicID", "ClinicName");
		vlClinics.ValueListItems.Clear();
		for (int num3 = 0; num3 < dtClinics.Rows.Count; num3++)
		{
			vlClinics.ValueListItems.Add(dtClinics.Rows[num3]["ClinicID"], dtClinics.Rows[num3]["ClinicName"].ToString());
		}
		dtPatients = Patients.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPatients, dtPatients, "PatientID", "PatientName");
		dtVisaType = VisaTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboVisaType, dtVisaType, "VisaTypeID", "VisaTypeName");
		vlVisaType.ValueListItems.Clear();
		for (int num4 = 0; num4 < dtVisaType.Rows.Count; num4++)
		{
			vlVisaType.ValueListItems.Add(dtVisaType.Rows[num4]["VisaTypeID"], dtVisaType.Rows[num4]["VisaTypeName"].ToString());
		}
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUsers.ValueListItems.Clear();
		for (int num5 = 0; num5 < dtUsers.Rows.Count; num5++)
		{
			vlUsers.ValueListItems.Add(dtUsers.Rows[num5]["User_ID"], dtUsers.Rows[num5]["UserName"].ToString());
		}
		((TextEditorControlBase)cboReservationType).ValueChanged -= cboReservationType_ValueChanged;
		dtReservationType.Clear();
		dtReservationType.Columns.Add("TypeID");
		dtReservationType.Columns.Add("TypeName");
		dtReservationType.Rows.Add(1, GlobalVariables.IsArabic ? "كشف" : "Examination");
		dtReservationType.Rows.Add(2, GlobalVariables.IsArabic ? "إستشارة" : "Consultation");
		if (bool.Parse(dtDoctors.Select("DoctorID = " + DoctorID)[0]["IsExtended"].ToString()))
		{
			dtReservationType.Rows.Add(3, GlobalVariables.IsArabic ? "تمديد" : "Extension");
			((Control)(object)chkIsExtended).Visible = true;
		}
		GlobalFunctions.FillCombo(cboReservationType, dtReservationType, "TypeID", "TypeName");
		cboReservationType.SelectedIndex = 0;
		((TextEditorControlBase)cboReservationType).ValueChanged += cboReservationType_ValueChanged;
		dtProcedures = Procedures.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlProcedures.ValueListItems.Clear();
		for (int num6 = 0; num6 < dtProcedures.Rows.Count; num6++)
		{
			vlProcedures.ValueListItems.Add(dtProcedures.Rows[num6]["ProcedureID"], dtProcedures.Rows[num6]["ProcedureName"].ToString());
		}
		dtSteps = Steps.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlSteps.ValueListItems.Clear();
		for (int num7 = 0; num7 < dtSteps.Rows.Count; num7++)
		{
			vlSteps.ValueListItems.Add(dtSteps.Rows[num7]["StepID"], dtSteps.Rows[num7]["StepName"].ToString());
		}
		dtDetails = ReservationsItems.SelectByReservationID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtPayments = ReservationsPayments.SelectByReservationID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtAccruals = Reservations.SelectNotPaid("0", "0", GlobalVariables.IsArabic ? "1" : "0");
		dtReservationProcedures = ReservationsProcedures.SelectByReservationID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtReservationProceduresSteps = ReservationsProceduresSteps.SelectByReservationID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtReservationProceduresStepsItems = ReservationsProceduresStepsItems.SelectByReservationID("0", GlobalVariables.IsArabic ? "1" : "0");
		ds = new DataSet();
		ds.Tables.Add(dtReservationProcedures);
		ds.Tables.Add(dtReservationProceduresSteps);
		ds.Tables.Add(dtReservationProceduresStepsItems);
		ds.Tables[0].TableName = "dtReservationProcedures";
		ds.Tables[1].TableName = "dtReservationProceduresSteps";
		ds.Tables[2].TableName = "dtReservationProceduresStepsItems";
		ds.Relations.Add(ds.Tables[0].Columns["ReservationProcedureID"], ds.Tables[1].Columns["ReservationProcedureID"]);
		ds.Relations.Add(ds.Tables[1].Columns["ReservationProcedureStepID"], ds.Tables[2].Columns["ReservationProcedureStepID"]);
		((UltraGridBase)ULGDataProcedures).DataSource = ds;
		((UltraGridBase)ULGDataPayments).DataSource = dtPayments;
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGAccruals).DataSource = dtAccruals;
		InitGrid();
		InitGridPayment();
		InitGridAccruals();
		InitGridProcedures();
	}

	public override void FillData()
	{
		if (RowID == "" || RowID == "-1")
		{
			drMaster = null;
			btnAddClick();
			((TextEditorControlBase)cboClinic).Value = ClinicID;
			((TextEditorControlBase)cboDoctor).Value = DoctorID;
			((Control)(object)txtOrderNo).Text = OrderNo;
			dtpAppointmentDate.DateTime = dtAppointmentDate;
			((Control)(object)btnOK).Visible = false;
			((EditorButtonControlBase)txtCode).ReadOnly = true;
			((Control)(object)btnSaveClose).Text = (GlobalVariables.IsArabic ? "F2حفظ و عودة" : " Save And Back F2 ");
			return;
		}
		DataTable dataTable = Reservations.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
		if (dataTable.Rows.Count > 0)
		{
			drMaster = dataTable.Rows[0];
			DisplayData();
			if (drMaster != null)
			{
				if (!Convert.ToBoolean(drMaster["IsClosed"]))
				{
					btnUpdateClick();
					((Control)(object)btnOK).Visible = false;
					((Control)(object)btnSaveClose).Text = (GlobalVariables.IsArabic ? "F2حفظ و عودة" : " Save And Back F2 ");
				}
				else
				{
					((Control)(object)btnAdd).Visible = false;
				}
			}
			((EditorButtonControlBase)txtCode).ReadOnly = true;
		}
		else
		{
			drMaster = null;
		}
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (drMaster != null)
		{
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["ReservationCode"].ToString();
			((TextEditorControlBase)cboClinic).Value = drMaster["ClinicID"];
			((TextEditorControlBase)cboDoctor).Value = drMaster["DoctorID"];
			((TextEditorControlBase)cboPriceType).ValueChanged -= cboPriceType_ValueChanged;
			((TextEditorControlBase)cboPriceType).Value = drMaster["PriceTypeID"];
			if (drMaster["PriceTypeID"] != DBNull.Value)
			{
				dtProceduresPrices = ProceduresPrices.GetPrice(((TextEditorControlBase)cboPriceType).Value.ToString(), IsFromServer: false);
			}
			((TextEditorControlBase)cboPriceType).ValueChanged += cboPriceType_ValueChanged;
			((TextEditorControlBase)cboPatients).ValueChanged -= cboPatients_ValueChanged;
			((TextEditorControlBase)cboPatients).Value = drMaster["PatientID"];
			((Control)(object)txtBranchBalance).Text = decimal.Parse(Math.Round(Patients.GetBalance(drMaster["PatientID"].ToString(), "," + (Adding ? GlobalVariables.CurrentBranchID : drMaster["BranchID"].ToString()) + ",", IsFromServer: false), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)cboPatients).ValueChanged += cboPatients_ValueChanged;
			dtpDate.Value = (DateTime)drMaster["VoucherDate"];
			((Control)(object)txtOrderNo).Text = drMaster["OrderNo"].ToString();
			dtpAppointmentDate.Value = (DateTime)drMaster["AppointmentDate"];
			((UltraToggleEditorBase)chkIsExtended).Checked = bool.Parse(drMaster["IsExtended"].ToString());
			((UltraToggleEditorBase)chkIsOnAccount).Checked = bool.Parse(drMaster["IsOnAccount"].ToString());
			((TextEditorControlBase)cboReservationType).ValueChanged -= cboReservationType_ValueChanged;
			((TextEditorControlBase)cboReservationType).Value = drMaster["ReservationType"];
			((TextEditorControlBase)cboReservationType).ValueChanged += cboReservationType_ValueChanged;
			((Control)(object)txtFeesAmount).Text = decimal.Parse(drMaster["Fees"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtFees).Text = decimal.Parse(drMaster["FeesNet"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((UltraToggleEditorBase)chkConfirmed).Checked = bool.Parse(drMaster["IsConfirmed"].ToString());
			((UltraToggleEditorBase)chkCanceled).Checked = bool.Parse(drMaster["IsCancelled"].ToString());
			((UltraToggleEditorBase)chkArrived).Checked = bool.Parse(drMaster["IsArrived"].ToString());
			dtpArrivalDate.Value = drMaster["ArrivalDate"];
			((UltraToggleEditorBase)chkClosed).Checked = bool.Parse(drMaster["IsClosed"].ToString());
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((TextEditorControlBase)txtFeesDiscValue).ValueChanged -= txtFeesDiscValue_ValueChanged;
			((Control)(object)txtFeesDiscValue).Text = decimal.Parse(drMaster["FeesDiscountValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtFeesDiscValue).ValueChanged += txtFeesDiscValue_ValueChanged;
			((TextEditorControlBase)txtFeesDiscRatio).ValueChanged -= txtFeesDiscRatio_ValueChanged;
			((Control)(object)txtFeesDiscRatio).Text = decimal.Parse(drMaster["FeesDiscountRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtFeesDiscRatio).ValueChanged += txtFeesDiscRatio_ValueChanged;
			((Control)(object)txtGrossValue).Text = decimal.Parse(drMaster["GrossValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(drMaster["DiscountBeforeTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse(drMaster["DiscountbeforeTaxRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
			((Control)(object)txtTaxTotalValue).Text = decimal.Parse(drMaster["TaxTotalValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtCommercialTax).ValueChanged -= txtCommercialTax_ValueChanged;
			((Control)(object)txtCommercialTax).Text = decimal.Parse(drMaster["CommercialTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtCommercialTax).ValueChanged += txtCommercialTax_ValueChanged;
			((TextEditorControlBase)txtPaidAmount).ValueChanged -= txtPaidAmount_ValueChanged;
			((Control)(object)txtPaidAmount).Text = decimal.Parse(drMaster["PaidAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtPaidAmount).ValueChanged += txtPaidAmount_ValueChanged;
			((TextEditorControlBase)txtRestAmount).ValueChanged -= txtCommercialTax_ValueChanged;
			((Control)(object)txtRestAmount).Text = decimal.Parse(drMaster["RestAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtRestAmount).ValueChanged += txtCommercialTax_ValueChanged;
			((Control)(object)txtNetprice).Text = decimal.Parse(drMaster["NetPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = ReservationsItems.SelectByReservationID(drMaster["ReservationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtPayments = ReservationsPayments.SelectByReservationID(drMaster["ReservationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtAccruals = Reservations.SelectNotPaid(drMaster["PatientID"].ToString(), drMaster["ReservationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			((UltraGridBase)ULGDataPayments).DataSource = dtPayments;
			((UltraGridBase)ULGAccruals).DataSource = dtAccruals;
			dtReservationProcedures = ReservationsProcedures.SelectByReservationIDNotClosed(drMaster["ReservationID"].ToString(), drMaster["ClinicID"].ToString(), drMaster["PatientID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtReservationProceduresSteps = ReservationsProceduresSteps.SelectByReservationIDNotClosed(drMaster["ReservationID"].ToString(), drMaster["ClinicID"].ToString(), drMaster["PatientID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtReservationProceduresStepsItems = ReservationsProceduresStepsItems.SelectByReservationIDNotClosed(drMaster["ReservationID"].ToString(), drMaster["ClinicID"].ToString(), drMaster["PatientID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			ds = new DataSet();
			ds.Tables.Add(dtReservationProcedures);
			ds.Tables.Add(dtReservationProceduresSteps);
			ds.Tables.Add(dtReservationProceduresStepsItems);
			ds.Tables[0].TableName = "dtReservationProcedures";
			ds.Tables[1].TableName = "dtReservationProceduresSteps";
			ds.Tables[2].TableName = "dtReservationProceduresStepsItems";
			ds.Relations.Add(ds.Tables[0].Columns["ReservationProcedureID"], ds.Tables[1].Columns["ReservationProcedureID"]);
			ds.Relations.Add(ds.Tables[1].Columns["ReservationProcedureStepID"], ds.Tables[2].Columns["ReservationProcedureStepID"]);
			((UltraGridBase)ULGDataProcedures).DataSource = ds;
			InitGrid();
			InitGridPayment();
			InitGridAccruals();
			InitGridProcedures();
			if (dtAccruals.Rows.Count > 0)
			{
				((UltraTabControlBase)UTCDetails).Tabs["Accruals"].Visible = true;
				((UltraTabControlBase)UTCDetails).Tabs["Accruals"].Selected = true;
			}
			else
			{
				((UltraTabControlBase)UTCDetails).Tabs["Accruals"].Visible = false;
			}
			if (drMaster["IsConfirmed"].Equals(true) || drMaster["IsClosed"].Equals(true))
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
		}
		else
		{
			ClearControls();
		}
		((TextEditorControlBase)cboPatients).Focus();
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPatients).ReadOnly = NavMode;
		((EditorButtonControlBase)cboClinic).ReadOnly = true;
		((EditorButtonControlBase)cboDoctor).ReadOnly = true;
		((EditorButtonControlBase)txtOrderNo).ReadOnly = true;
		((EditorButtonControlBase)dtpAppointmentDate).ReadOnly = true;
		((Control)(object)chkIsExtended).Enabled = !NavMode;
		((EditorButtonControlBase)cboReservationType).ReadOnly = NavMode;
		((Control)(object)txtBranchBalance).Visible = !NavMode;
		((Control)(object)btnPatientAdd).Visible = !NavMode;
		((EditorButtonControlBase)txtFees).ReadOnly = true;
		((Control)(object)chkConfirmed).Enabled = !NavMode;
		((Control)(object)chkCanceled).Enabled = !NavMode;
		((Control)(object)chkArrived).Enabled = !NavMode;
		((EditorButtonControlBase)dtpArrivalDate).ReadOnly = true;
		((Control)(object)chkClosed).Enabled = !NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)btnSearch).Visible = false;
		UltraButton obj = btnUpdate;
		bool enabled = (((Control)(object)btnPatientSearch).Visible = !NavMode);
		((Control)(object)obj).Enabled = enabled;
		((Control)(object)btnRefreshData).Visible = false;
		((EditorButtonControlBase)txtPaidAmount).ReadOnly = !Adding;
		((UltraToggleEditorBase)chkVisa).Checked = false;
		((Control)(object)chkVisa).Visible = Adding;
		((Control)(object)cboVisaType).Visible = Adding;
		((Control)(object)lblVisaNo).Visible = Adding;
		((Control)(object)txtVisaNo).Visible = Adding;
		((Control)(object)btnOrderPayments).Visible = Updating;
		((EditorButtonControlBase)txtDiscBeforeTaxValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscBeforeTaxRatio).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCommercialTax).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPriceType).ReadOnly = !CanModifyPriceType;
		((Control)(object)btnPriceTypeSearch).Visible = !NavMode && !CanModifyPriceType;
		NewPriceUserID = 0;
		DiscountUserID = 0;
		((Control)(object)chkStore).Visible = !NavMode;
		((Control)(object)cboStore).Visible = !NavMode;
		((Control)(object)chkTax).Visible = !NavMode;
		((Control)(object)cboTax).Visible = !NavMode;
		if (Adding || Updating)
		{
			DataView dataView = new DataView(dtStores);
			dataView.RowFilter = " Locked =0  And BranchID=" + GlobalVariables.CurrentBranchID;
			DataTable dataTable = dataView.ToTable();
			vlDetailsStores.ValueListItems.Clear();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				vlDetailsStores.ValueListItems.Add(dataTable.Rows[i]["StoreID"], dataTable.Rows[i]["StoreName"].ToString());
			}
			GlobalFunctions.FillCombo(cboStore, dataTable, "StoreID", "StoreName");
		}
		else
		{
			vlDetailsStores.ValueListItems.Clear();
			for (int j = 0; j < dtStores.Rows.Count; j++)
			{
				vlDetailsStores.ValueListItems.Add(dtStores.Rows[j]["StoreID"], dtStores.Rows[j]["StoreName"].ToString());
			}
			GlobalFunctions.FillCombo(cboStore, dtStores, "StoreID", "StoreName");
		}
		if (Adding)
		{
			DataView dataView2 = new DataView(dtItems);
			dataView2.RowFilter = " IsActive =1 ";
			DataTable dataTable2 = dataView2.ToTable();
			vlDetailsItems.ValueListItems.Clear();
			vlDetailsBarCode.ValueListItems.Clear();
			for (int k = 0; k < dataTable2.Rows.Count; k++)
			{
				vlDetailsItems.ValueListItems.Add(dataTable2.Rows[k]["ItemID"], dataTable2.Rows[k]["Name"].ToString());
				vlDetailsBarCode.ValueListItems.Add(dataTable2.Rows[k]["ItemID"], dataTable2.Rows[k]["ItemBarCode"].ToString());
			}
			DataView dataView3 = new DataView(dtVisaType);
			dataView3.RowFilter = " IsActive =1 ";
			DataTable dataTable3 = dataView3.ToTable();
			GlobalFunctions.FillCombo(cboVisaType, dataTable3, "VisaTypeID", "VisaTypeName");
			vlVisaType.ValueListItems.Clear();
			for (int l = 0; l < dataTable3.Rows.Count; l++)
			{
				vlVisaType.ValueListItems.Add(dataTable3.Rows[l]["VisaTypeID"], dataTable3.Rows[l]["VisaTypeName"].ToString());
			}
		}
		else
		{
			vlDetailsItems.ValueListItems.Clear();
			vlDetailsBarCode.ValueListItems.Clear();
			for (int m = 0; m < dtItems.Rows.Count; m++)
			{
				vlDetailsItems.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["Name"].ToString());
				vlDetailsBarCode.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["ItemBarCode"].ToString());
			}
			GlobalFunctions.FillCombo(cboVisaType, dtVisaType, "VisaTypeID", "VisaTypeName");
			vlVisaType.ValueListItems.Clear();
			for (int n = 0; n < dtVisaType.Rows.Count; n++)
			{
				vlVisaType.ValueListItems.Add(dtVisaType.Rows[n]["VisaTypeID"], dtVisaType.Rows[n]["VisaTypeName"].ToString());
			}
		}
		if (Updating)
		{
			for (int num = 0; num < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; num++)
			{
				if (((UltraGridBase)ULGData).Rows[num].Cells["ItemID"].Value == DBNull.Value)
				{
					continue;
				}
				DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).Rows[num].Cells["ItemID"].Value.ToString())[0];
				if (UsingBatchNoAndValidityPeriod)
				{
					ValueList batchsValueList = getBatchsValueList(int.Parse(dataRow["ItemID"].ToString()));
					((UltraGridBase)ULGData).Rows[num].Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
					if (((DisposableObjectCollectionBase)batchsValueList.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGData).Rows[num].Cells["BatchID"].Value = DBNull.Value;
					}
				}
				if (!bool.Parse(dataRow["IsService"].ToString()))
				{
					int unitTypeID = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).Rows[num].Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
					ValueList unitsValueList = getUnitsValueList(unitTypeID);
					((UltraGridBase)ULGData).Rows[num].Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
					if (((DisposableObjectCollectionBase)unitsValueList.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGData).Rows[num].Cells["UnitID"].Value = DBNull.Value;
					}
				}
				if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[num].Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
					if (((UltraGridBase)ULGData).Rows[num].Cells["ColorID"].ValueList.ItemCount == 0)
					{
						((UltraGridBase)ULGData).Rows[num].Cells["ColorID"].Value = DBNull.Value;
					}
				}
				if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[num].Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
					if (((UltraGridBase)ULGData).Rows[num].Cells["ItemSizeID"].ValueList.ItemCount == 0)
					{
						((UltraGridBase)ULGData).Rows[num].Cells["ItemSizeID"].Value = DBNull.Value;
					}
				}
			}
		}
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((Control)(object)txtCode).Text = (Adding ? Reservations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		dtpDate.ValueChanged += dtpDate_ValueChanged;
		((TextEditorControlBase)cboPatients).ValueChanged -= cboPatients_ValueChanged;
		cboPatients.SelectedIndex = -1;
		((TextEditorControlBase)cboPatients).ValueChanged += cboPatients_ValueChanged;
		((TextEditorControlBase)txtBranchBalance).Clear();
		cboClinic.SelectedIndex = -1;
		cboDoctor.SelectedIndex = -1;
		((TextEditorControlBase)txtOrderNo).Clear();
		dtpAppointmentDate.DateTime = DateTime.Now;
		((UltraToggleEditorBase)chkIsOnAccount).Checked = false;
		((UltraToggleEditorBase)chkIsExtended).Checked = false;
		((TextEditorControlBase)txtPaidAmount).ValueChanged -= txtPaidAmount_ValueChanged;
		((Control)(object)txtPaidAmount).Text = "0";
		((TextEditorControlBase)txtPaidAmount).ValueChanged += txtPaidAmount_ValueChanged;
		((UltraToggleEditorBase)chkVisa).Checked = false;
		cboVisaType.SelectedIndex = -1;
		((Control)(object)txtVisaNo).Text = "";
		((TextEditorControlBase)txtFees).Clear();
		((UltraToggleEditorBase)chkConfirmed).Checked = false;
		((UltraToggleEditorBase)chkCanceled).Checked = false;
		((UltraToggleEditorBase)chkArrived).Checked = false;
		dtpArrivalDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((UltraToggleEditorBase)chkClosed).Checked = false;
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)cboPriceType).ValueChanged -= cboPriceType_ValueChanged;
		cboPriceType.SelectedIndex = -1;
		((TextEditorControlBase)cboPriceType).ValueChanged += cboPriceType_ValueChanged;
		((UltraToggleEditorBase)chkStore).Checked = false;
		cboStore.SelectedIndex = -1;
		((UltraToggleEditorBase)chkTax).Checked = false;
		cboTax.SelectedIndex = -1;
		((Control)(object)txtGrossValue).Text = "0";
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
		((Control)(object)txtDiscBeforeTaxValue).Text = "0";
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
		((Control)(object)txtDiscBeforeTaxRatio).Text = "0";
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
		((Control)(object)txtTaxTotalValue).Text = "0";
		((TextEditorControlBase)txtFeesDiscValue).ValueChanged -= txtFeesDiscValue_ValueChanged;
		((Control)(object)txtFeesDiscValue).Text = "0";
		((TextEditorControlBase)txtFeesDiscValue).ValueChanged += txtFeesDiscValue_ValueChanged;
		((TextEditorControlBase)txtFeesDiscRatio).ValueChanged -= txtFeesDiscRatio_ValueChanged;
		((Control)(object)txtFeesDiscRatio).Text = "0";
		((TextEditorControlBase)txtFeesDiscRatio).ValueChanged += txtFeesDiscRatio_ValueChanged;
		((Control)(object)txtFees).Text = "0";
		((Control)(object)txtNetprice).Text = "0";
		((TextEditorControlBase)txtCommercialTax).ValueChanged -= txtCommercialTax_ValueChanged;
		((Control)(object)txtCommercialTax).Text = "0";
		((TextEditorControlBase)txtCommercialTax).ValueChanged += txtCommercialTax_ValueChanged;
		((TextEditorControlBase)txtPaidAmount).ValueChanged -= txtPaidAmount_ValueChanged;
		((Control)(object)txtPaidAmount).Text = "0";
		((TextEditorControlBase)txtPaidAmount).ValueChanged += txtPaidAmount_ValueChanged;
		((TextEditorControlBase)txtRestAmount).ValueChanged -= txtCommercialTax_ValueChanged;
		((Control)(object)txtRestAmount).Text = "0";
		((TextEditorControlBase)txtRestAmount).ValueChanged += txtCommercialTax_ValueChanged;
		((Control)(object)txtNetprice).Text = "0";
		((Control)(object)txtTotalQty).Text = "0";
		((TextEditorControlBase)cboReservationType).ValueChanged -= cboReservationType_ValueChanged;
		cboReservationType.SelectedIndex = 0;
		CalculateFees();
		((TextEditorControlBase)cboReservationType).ValueChanged += cboReservationType_ValueChanged;
		((DataTable)((UltraGridBase)ULGDataPayments).DataSource).Rows.Clear();
		((DataSet)((UltraGridBase)ULGDataProcedures).DataSource).Tables[2].Rows.Clear();
		((DataSet)((UltraGridBase)ULGDataProcedures).DataSource).Tables[1].Rows.Clear();
		((DataSet)((UltraGridBase)ULGDataProcedures).DataSource).Tables[0].Rows.Clear();
		newID = -100000;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)((!Adding && !Updating) ? 2 : 6);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReservationItemID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Hidden = !UsingColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Hidden = !UsingSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		if (UsingColors)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].ValueList = (IValueList)(object)vlDetailsColors;
		}
		if (UsingSizes)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlDetailsSizes;
		}
		if (UsingBatchNoAndValidityPeriod)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].ValueList = (IValueList)(object)vlDetailsBatchs;
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = true;
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "Batch No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "BarCode");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحدة" : "Unit price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "مخزن" : "Store");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "ألإجمالى" : "Total Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Header).Caption = (GlobalVariables.IsArabic ? "الخصم" : "Discount");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Header).Caption = (GlobalVariables.IsArabic ? "الضريبة" : "Tax");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الضريبة" : "Tax Value");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الصافى" : "Net");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlDetailsItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].ValueList = (IValueList)(object)vlDetailsBarCode;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlDetailsUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].ValueList = (IValueList)(object)vlDetailsStores;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].ValueList = (IValueList)(object)vlDetailsTaxs;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualUnitSalesPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
	}

	public void InitGridPayment()
	{
		GlobalFunctions.PrepareGrid(ULGDataPayments);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["ReservationPaymentID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["ReservationPaymentNo"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.1);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["ReservationPaymentDate"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.15) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeID"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.1);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeNo"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.1);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Amount"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.1);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.1);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.2);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Approved"].Width = (int)((double)((Control)(object)ULGDataPayments).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["ReservationPaymentNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["ReservationPaymentDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الفيزا" : "Visa Type");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الفيزا" : "Visa No");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].Header).Caption = (GlobalVariables.IsArabic ? "User" : "User");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Amount"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة" : "Amount");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "البيان" : "Notes");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Approved"].Header).Caption = (GlobalVariables.IsArabic ? "معتمد" : "Approved");
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["ReservationPaymentNo"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["ReservationPaymentDate"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeID"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeNo"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Amount"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Approved"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["VisaTypeID"].ValueList = (IValueList)(object)vlVisaType;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].ValueList = (IValueList)(object)vlUsers;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Amount"].DefaultCellValue = 0;
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

	public void InitGridAccruals()
	{
		GlobalFunctions.PrepareGrid(ULGAccruals);
		((UltraGridBase)ULGAccruals).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGAccruals).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["ReservationCode"].Width = (int)((double)((Control)(object)ULGAccruals).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["AppointmentDate"].Width = (int)((double)((Control)(object)ULGAccruals).Width * 0.1);
		((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["DoctorName"].Width = (int)((double)((Control)(object)ULGAccruals).Width * 0.2);
		((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["ClinicName"].Width = (int)((double)((Control)(object)ULGAccruals).Width * 0.2);
		((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["NetPrice"].Width = (int)((double)((Control)(object)ULGAccruals).Width * 0.1);
		((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["PaidAmount"].Width = (int)((double)((Control)(object)ULGAccruals).Width * 0.1);
		((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["RestAmount"].Width = (int)((double)((Control)(object)ULGAccruals).Width * 0.1);
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns).Exists("Payment"))
		{
			((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns.Insert(1, "Payment");
		}
		((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["Payment"].DefaultCellValue = (GlobalVariables.IsArabic ? "سداد" : "Payment");
		((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["Payment"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["Payment"].Header).Caption = (GlobalVariables.IsArabic ? "سداد" : "Payment");
		((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["Payment"].Width = (int)((double)((Control)(object)ULGAccruals).Width * 0.1);
		((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["Payment"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["Payment"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["Payment"].Header).VisiblePosition = ((!GlobalVariables.IsArabic) ? ((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["Payment"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns).Count - 2));
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGAccruals).Rows).Count; i++)
		{
			((UltraGridBase)ULGAccruals).Rows[i].Cells["Payment"].Value = (GlobalVariables.IsArabic ? "سداد" : "Payment");
		}
		((HeaderBase)((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["ReservationCode"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الحجز" : "Reservation No");
		((HeaderBase)((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["AppointmentDate"].Header).Caption = (GlobalVariables.IsArabic ? "ميعاد الحجز" : "Appointment Date");
		((HeaderBase)((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["DoctorName"].Header).Caption = (GlobalVariables.IsArabic ? "الطبيب" : "Doctor");
		((HeaderBase)((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["ClinicName"].Header).Caption = (GlobalVariables.IsArabic ? "العيادة" : "Clinic");
		((HeaderBase)((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["NetPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الصافي" : "Net Price");
		((HeaderBase)((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["PaidAmount"].Header).Caption = (GlobalVariables.IsArabic ? "المدفوع" : "Paid Amount");
		((HeaderBase)((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["RestAmount"].Header).Caption = (GlobalVariables.IsArabic ? "المتبقي على العميل" : "Rest Amount");
		((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["ReservationCode"].Hidden = false;
		((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["AppointmentDate"].Hidden = false;
		((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["DoctorName"].Hidden = false;
		((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["ClinicName"].Hidden = false;
		((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["NetPrice"].Hidden = false;
		((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["PaidAmount"].Hidden = false;
		((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["RestAmount"].Hidden = false;
		((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["PaidAmount"].DefaultCellValue = 0;
		((UltraGridBase)ULGAccruals).DisplayLayout.Bands[0].Columns["RestAmount"].DefaultCellValue = 0;
	}

	public void InitGridProcedures()
	{
		GlobalFunctions.PrepareGrid(ULGDataProcedures);
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Override.AllowAddNew = (AllowAddNew)((!Adding && !Updating) ? 2 : 6);
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["ReservationProcedureID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["ProcedureID"].Width = (int)((double)((Control)(object)ULGDataProcedures).Width * 0.1);
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["Price"].Width = (int)((double)((Control)(object)ULGDataProcedures).Width * 0.1);
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["Discount"].Width = (int)((double)((Control)(object)ULGDataProcedures).Width * 0.08);
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["TaxID"].Width = (int)((double)((Control)(object)ULGDataProcedures).Width * 0.1);
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["TaxValue"].Width = (int)((double)((Control)(object)ULGDataProcedures).Width * 0.08);
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["NetPrice"].Width = (int)((double)((Control)(object)ULGDataProcedures).Width * 0.1);
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["StartDate"].Width = (int)((double)((Control)(object)ULGDataProcedures).Width * 0.1);
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["IsClosed"].Width = (int)((double)((Control)(object)ULGDataProcedures).Width * 0.1);
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGDataProcedures).Width * 0.18) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["ProcedureID"].Header).Caption = (GlobalVariables.IsArabic ? "الإجراء" : "Procedure");
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["Price"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["Discount"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الخصم" : "Discount");
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["TaxID"].Header).Caption = (GlobalVariables.IsArabic ? "الضريبة" : "Tax");
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["TaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الضريبة" : "Tax Value");
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["NetPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الصافي" : "Net Price");
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["StartDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ البدء" : "Start Date");
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["IsClosed"].Header).Caption = (GlobalVariables.IsArabic ? "مغلق" : "Closed");
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["ProcedureID"].Hidden = false;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["Price"].Hidden = false;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["Discount"].Hidden = false;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["TaxID"].Hidden = false;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["TaxValue"].Hidden = false;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["NetPrice"].Hidden = false;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["StartDate"].Hidden = false;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["IsClosed"].Hidden = false;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["ProcedureID"].ValueList = (IValueList)(object)vlProcedures;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["TaxID"].ValueList = (IValueList)(object)vlProceduresTaxs;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["Price"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["Discount"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["IsClosed"].DefaultCellValue = false;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[0].Columns["StartDate"].DefaultCellValue = dtAppointmentDate.Date;
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[1].Columns["StepID"].Header).Caption = (GlobalVariables.IsArabic ? "المرحلة" : "Step");
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[1].Columns["PricePercentage"].Header).Caption = (GlobalVariables.IsArabic ? "النسبة المئوية للسعر" : "Price Percentage");
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[1].Columns["ExpectedDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ المتوقع" : "Expected Date");
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[1].Columns["Description"].Header).Caption = (GlobalVariables.IsArabic ? "الوصف" : "Description");
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[1].Columns["Price"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[1].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[1].Columns["IsExecuted"].Header).Caption = (GlobalVariables.IsArabic ? "تمت" : "Executed");
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[1].Columns["StepID"].Hidden = false;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[1].Columns["PricePercentage"].Hidden = false;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[1].Columns["ExpectedDate"].Hidden = false;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[1].Columns["Description"].Hidden = false;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[1].Columns["Price"].Hidden = false;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[1].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[1].Columns["IsExecuted"].Hidden = false;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[1].Columns["StepID"].ValueList = (IValueList)(object)vlSteps;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[1].Columns["PricePercentage"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[1].Columns["Price"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[1].Columns["IsExecuted"].DefaultCellValue = false;
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["ColorID"].Hidden = !UsingColors;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["ItemSizeID"].Hidden = !UsingSizes;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["ColorID"].ValueList = (IValueList)(object)vlStepsColors;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlStepsSizes;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["ColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["ItemSizeID"].DefaultCellValue = 1;
		if (UsingBatchNoAndValidityPeriod)
		{
			((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["BatchID"].Hidden = false;
			((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["BatchID"].ValueList = (IValueList)(object)vlStepsBatchs;
		}
		else
		{
			((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["BatchID"].Hidden = true;
		}
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "Batch No");
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["ItemBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "BarCode");
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "مخزن" : "Store");
		((HeaderBase)((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["ItemBarCode"].Hidden = false;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["ItemID"].ValueList = (IValueList)(object)vlStepsItems;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["ItemBarCode"].ValueList = (IValueList)(object)vlStepsBarCode;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["UnitID"].ValueList = (IValueList)(object)vlStepsUnits;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["StoreID"].ValueList = (IValueList)(object)vlStepsStores;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGDataProcedures).DisplayLayout.Bands[2].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
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

	public override bool ValidateData()
	{
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
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم  الحجز" : "Please Enter The Reservation Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (cboPatients.SelectedIndex < 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار إسم المريض" : "Please Select Patient Name ");
			((TextEditorControlBase)cboPatients).Focus();
			return false;
		}
		if (cboClinic.SelectedIndex < 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار إسم العيادة" : "Please Select Clinic Name ");
			((TextEditorControlBase)cboClinic).Focus();
			return false;
		}
		if (cboDoctor.SelectedIndex < 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار إسم الدكتور" : "Please Select Doctor Name ");
			((TextEditorControlBase)cboDoctor).Focus();
			return false;
		}
		if (cboReservationType.SelectedIndex < 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار نوع الحجز" : "Please Select Reservation Type");
			((TextEditorControlBase)cboReservationType).Focus();
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الصنف  ", "Please Enter Item Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
				((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
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
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Qty"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value == DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار الوحدة  ", "Please Select Unit Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value == DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار المخزن  ", "Please Select Store");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال سعر الوحدة  ", "Please Enter Unit Price ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إدخال إجمالى السعر  ", "Please Enter Total price");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (UsingBatchNoAndValidityPeriod && bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) && (((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value == DBNull.Value || dtBatchs.Select("BatchID=" + ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value.ToString()).Length == 0))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء اختيار سريل", "Please choose Batch No");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"];
				ULGData.PerformAction((UltraGridAction)24);
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
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب العميل الافتراضى من اعدادات البيع  ", "Please Select Default Client From Sales Settings ");
			return false;
		}
		if (dtPOSDefaultData.Rows[0]["BranchSafeID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد خزينة الفرع من اعدادات البيع  ", "Please Select Branch Safe From Sales Settings ");
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
		if (((UltraToggleEditorBase)chkClosed).Checked && !((UltraToggleEditorBase)chkIsExtended).Checked && !((UltraToggleEditorBase)chkIsOnAccount).Checked && (decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text) < decimal.Parse(((Control)(object)txtNetprice).Text) || ((DisposableObjectCollectionBase)((UltraGridBase)ULGAccruals).Rows).Count > 0))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء سداد قيمة الكشف و المستحقات قبل اغلاق الكشف" : "The Fees And Accruals Should Be Paid Before Closing");
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("CL_Reservations", "ReservationCode", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["ReservationCode"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = Reservations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذا الحجز متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Reservation Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByBranchID;
		}
		else if (((Control)(object)txtOrderNo).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال رقم الحجز", "please Insert Reservation Order No");
			((TextEditorControlBase)txtOrderNo).Focus();
			return false;
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataProcedures).Rows).Count; j++)
		{
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataProcedures).Rows[j].ChildBands[0].Rows).Count; k++)
			{
				if (!bool.Parse(((UltraGridBase)ULGDataProcedures).Rows[j].ChildBands[0].Rows[k].Cells["IsExecuted"].Value.ToString()))
				{
					continue;
				}
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataProcedures).Rows[j].ChildBands[0].Rows[k].ChildBands[0].Rows).Count; l++)
				{
					if (((UltraGridBase)ULGDataProcedures).Rows[j].ChildBands[0].Rows[k].ChildBands[0].Rows[l].Cells["StoreID"].Value == DBNull.Value)
					{
						GlobalVariables.InformationMB.Show("برجاء ادخال المخزن الخاص بالعيادة  ", "Please Enter Clinic Store");
						return false;
					}
				}
			}
		}
		return true;
	}

	public override void AddData()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Expected O, but got Unknown
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Expected O, but got Unknown
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Expected O, but got Unknown
		//IL_0906: Unknown result type (might be due to invalid IL or missing references)
		//IL_0910: Expected O, but got Unknown
		//IL_1769: Unknown result type (might be due to invalid IL or missing references)
		//IL_1773: Expected O, but got Unknown
		DataRow dataRow = dtPatients.Select(" PatientID= " + ((TextEditorControlBase)cboPatients).Value.ToString())[0];
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
			int num = Reservations.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboPatients).Value.ToString(), (dataRow["SubAccountID"] == DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultSubAccountID"].ToString() : dataRow["SubAccountID"].ToString(), (cboPriceType.SelectedIndex > -1) ? ((TextEditorControlBase)cboPriceType).Value.ToString() : "Null", ((TextEditorControlBase)cboDoctor).Value.ToString(), ((TextEditorControlBase)cboClinic).Value.ToString(), "Null", "0", "Null", "0", (ClinicScheduleID == -1) ? "Null" : ClinicScheduleID.ToString(), ((Control)(object)txtOrderNo).Text, dtpAppointmentDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboReservationType.SelectedIndex > -1) ? ((TextEditorControlBase)cboReservationType).Value.ToString() : "Null", (((Control)(object)txtFeesAmount).Text == "") ? "0" : ((Control)(object)txtFeesAmount).Text, (((Control)(object)txtFeesDiscRatio).Text == "") ? "0" : ((Control)(object)txtFeesDiscRatio).Text, (((Control)(object)txtFeesDiscValue).Text == "") ? "0" : ((Control)(object)txtFeesDiscValue).Text, (((Control)(object)txtFees).Text == "") ? "0" : ((Control)(object)txtFees).Text, ((UltraToggleEditorBase)chkConfirmed).Checked ? "1" : "0", ((UltraToggleEditorBase)chkCanceled).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsOnAccount).Checked ? "1" : "0", ((UltraToggleEditorBase)chkArrived).Checked ? "1" : "0", (dtpArrivalDate.Value == DBNull.Value) ? "Null" : dtpArrivalDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscBeforeTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text, (((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, "0", "0", (((Control)(object)txtCommercialTax).Text == "") ? "0" : ((Control)(object)txtCommercialTax).Text, (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, (((Control)(object)txtPaidAmount).Text == "") ? "0" : ((Control)(object)txtPaidAmount).Text, (((Control)(object)txtRestAmount).Text == "") ? "0" : ((Control)(object)txtRestAmount).Text, ((UltraToggleEditorBase)chkClosed).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsExtended).Checked ? "1" : "0", ((Control)(object)txtNotes).Text, ShiftDetailID, ShiftDetailUserID, "Null", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).UpdateData();
			dtDetails.AcceptChanges();
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				((UltraGridBase)ULGData).Rows[j].Cells["ReservationID"].Value = num.ToString();
				((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[j].Cells["ReservationItemID"].Value = -1;
				((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				((UltraGridBase)ULGData).Rows[j].Cells["VoucherDate"].Value = dtpAppointmentDate.DateTime;
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				ReservationsItems.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			}
			if (((Control)(object)txtPaidAmount).Text != "" && decimal.Parse(((Control)(object)txtPaidAmount).Text) > 0m)
			{
				ReservationPaymentID = ReservationsPayments.Insert_Update("-1", ReservationsPayments.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkVisa).Checked ? ((TextEditorControlBase)cboVisaType).Value.ToString() : "Null", ((UltraToggleEditorBase)chkVisa).Checked ? ((Control)(object)txtVisaNo).Text.ToString() : "Null", ((Control)(object)txtPaidAmount).Text, num.ToString(), GlobalVariables.UserID, ((Control)(object)txtNotes).Text, ShiftDetailID, ShiftDetailUserID, "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				ShiftsDetails.ClinicReservationPaymentsJVAdding(ReservationPaymentID.ToString(), ShiftDetailID, GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			}
			string text = (bool.Parse(dtClinics.Select("ClinicID = " + ((TextEditorControlBase)cboClinic).Value.ToString())[0]["AutoIssueItems"].ToString()) ? "1" : "0");
			ULGDataProcedures.AfterCellUpdate -= new CellEventHandler(ULGDataProcedures_AfterCellUpdate);
			string text2 = ",";
			string text3 = ",";
			int num2 = 0;
			int num3 = 0;
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataProcedures).Rows).Count; k++)
			{
				if (((UltraGridBase)ULGDataProcedures).Rows[k].Cells["ReservationID"].Value == DBNull.Value)
				{
					num2 = ReservationsProcedures.Insert_Update("-1", num.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[k].Cells["ProcedureID"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[k].Cells["Price"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[k].Cells["Discount"].Value.ToString(), (((UltraGridBase)ULGDataProcedures).Rows[k].Cells["TaxID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGDataProcedures).Rows[k].Cells["TaxID"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[k].Cells["TaxValue"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[k].Cells["NetPrice"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[k].Cells["StartDate"].Value.ToString(), (((UltraGridBase)ULGDataProcedures).Rows[k].Cells["Notes"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGDataProcedures).Rows[k].Cells["Notes"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[k].Cells["IsClosed"].Value.Equals(true) ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				}
				else if (bool.Parse(((UltraGridBase)ULGDataProcedures).Rows[k].Cells["IsClosed"].Value.ToString()))
				{
					text2 = text2 + ((UltraGridBase)ULGDataProcedures).Rows[k].Cells["ReservationProcedureID"].Value.ToString() + ",";
				}
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows).Count; l++)
				{
					if (((UltraGridBase)ULGDataProcedures).Rows[k].Cells["ReservationID"].Value == DBNull.Value)
					{
						num3 = ReservationsProceduresSteps.Insert_Update("-1", num.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].Cells["IsExecuted"].Value.Equals(true) ? num.ToString() : "Null", num2.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].Cells["StepID"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].Cells["Description"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].Cells["ExpectedDate"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].Cells["PricePercentage"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].Cells["Price"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].Cells["Discount"].Value.ToString(), (((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].Cells["TaxID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].Cells["TaxID"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].Cells["TaxValue"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].Cells["NetPrice"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].Cells["Notes"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].Cells["IsExecuted"].Value.Equals(true) ? "1" : "0", ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].Cells["IsExecuted"].Value.Equals(true) ? text : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					}
					else if (bool.Parse(((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].Cells["IsExecuted"].Value.ToString()) && ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].Cells["ExecutionReservationID"].Value == DBNull.Value)
					{
						text3 = text3 + ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].Cells["ReservationProcedureStepID"].Value.ToString() + ",";
					}
					for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].ChildBands[0].Rows).Count; m++)
					{
						if (((UltraGridBase)ULGDataProcedures).Rows[k].Cells["ReservationID"].Value == DBNull.Value)
						{
							ReservationsProceduresStepsItems.Insert_Update("-1", num.ToString(), num2.ToString(), num3.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].ChildBands[0].Rows[m].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].ChildBands[0].Rows[m].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].ChildBands[0].Rows[m].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].ChildBands[0].Rows[m].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].ChildBands[0].Rows[m].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].ChildBands[0].Rows[m].Cells["BatchID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].ChildBands[0].Rows[m].Cells["BatchID"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].ChildBands[0].Rows[m].Cells["Qty"].Value.ToString(), (((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].ChildBands[0].Rows[m].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].ChildBands[0].Rows[m].Cells["UnitID"].Value.ToString(), (((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].ChildBands[0].Rows[m].Cells["StoreID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].ChildBands[0].Rows[m].Cells["StoreID"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].ChildBands[0].Rows[m].Cells["Notes"].Value.ToString(), dtpAppointmentDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
						}
					}
				}
			}
			if (text2 != ",")
			{
				ReservationsProcedures.SetIsClosed(text2, GlobalVariables.UserID);
			}
			if (text3 != ",")
			{
				ReservationsProceduresSteps.SetIsExecuted(text3, num.ToString(), GlobalVariables.UserID);
			}
			ULGDataProcedures.AfterCellUpdate += new CellEventHandler(ULGDataProcedures_AfterCellUpdate);
			ShiftsDetails.SalesClinicJVAdding(num.ToString(), ShiftDetailID, GlobalVariables.UserID);
			RowID = num.ToString();
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
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Expected O, but got Unknown
		//IL_07f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fe: Expected O, but got Unknown
		//IL_1c1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c24: Expected O, but got Unknown
		DataRow dataRow = dtPatients.Select(" PatientID= " + ((TextEditorControlBase)cboPatients).Value.ToString())[0];
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
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			int num = Reservations.Insert_Update(drMaster["ReservationID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboPatients).Value.ToString(), (dataRow["SubAccountID"] == DBNull.Value) ? dtPOSDefaultData.Rows[0]["DefaultSubAccountID"].ToString() : dataRow["SubAccountID"].ToString(), (cboPriceType.SelectedIndex > -1) ? ((TextEditorControlBase)cboPriceType).Value.ToString() : "Null", ((TextEditorControlBase)cboDoctor).Value.ToString(), ((TextEditorControlBase)cboClinic).Value.ToString(), "Null", "0", "Null", "0", (ClinicScheduleID == -1) ? "Null" : ClinicScheduleID.ToString(), ((Control)(object)txtOrderNo).Text, dtpAppointmentDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboReservationType.SelectedIndex > -1) ? ((TextEditorControlBase)cboReservationType).Value.ToString() : "Null", (((Control)(object)txtFeesAmount).Text == "") ? "0" : ((Control)(object)txtFeesAmount).Text, (((Control)(object)txtFeesDiscRatio).Text == "") ? "0" : ((Control)(object)txtFeesDiscRatio).Text, (((Control)(object)txtFeesDiscValue).Text == "") ? "0" : ((Control)(object)txtFeesDiscValue).Text, (((Control)(object)txtFees).Text == "") ? "0" : ((Control)(object)txtFees).Text, ((UltraToggleEditorBase)chkConfirmed).Checked ? "1" : "0", ((UltraToggleEditorBase)chkCanceled).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsOnAccount).Checked ? "1" : "0", ((UltraToggleEditorBase)chkArrived).Checked ? "1" : "0", (dtpArrivalDate.Value == DBNull.Value) ? "Null" : dtpArrivalDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscBeforeTaxValue).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text, (((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, "0", "0", (((Control)(object)txtCommercialTax).Text == "") ? "0" : ((Control)(object)txtCommercialTax).Text, (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, (((Control)(object)txtPaidAmount).Text == "") ? "0" : ((Control)(object)txtPaidAmount).Text, (((Control)(object)txtRestAmount).Text == "") ? "0" : ((Control)(object)txtRestAmount).Text, ((UltraToggleEditorBase)chkClosed).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsExtended).Checked ? "1" : "0", ((Control)(object)txtNotes).Text, ShiftDetailID, ShiftDetailUserID, (drMaster["StockControlJVID"] == DBNull.Value) ? "Null" : drMaster["StockControlJVID"].ToString(), "1", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				((UltraGridBase)ULGData).Rows[j].Cells["ReservationID"].Value = num;
				((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				((UltraGridBase)ULGData).Rows[j].Cells["VoucherDate"].Value = dtpAppointmentDate.DateTime;
				text = text + ((UltraGridBase)ULGData).Rows[j].Cells["ReservationItemID"].Value.ToString() + ",";
			}
			Main.DeleteForUpdate("CL_ReservationsItems", "ReservationID", drMaster["ReservationID"].ToString(), "ReservationItemID", text);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				ReservationsItems.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			}
			string text2 = (bool.Parse(dtClinics.Select("ClinicID = " + ((TextEditorControlBase)cboClinic).Value.ToString())[0]["AutoIssueItems"].ToString()) ? "1" : "0");
			ULGDataProcedures.AfterCellUpdate -= new CellEventHandler(ULGDataProcedures_AfterCellUpdate);
			string text3 = ",";
			string text4 = ",";
			string text5 = ",";
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataProcedures).Rows).Count; k++)
			{
				text3 = text3 + ((UltraGridBase)ULGDataProcedures).Rows[k].Cells["ReservationProcedureID"].Value.ToString() + ",";
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows).Count; l++)
				{
					text4 = text4 + ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].Cells["ReservationProcedureStepID"].Value.ToString() + ",";
					for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].ChildBands[0].Rows).Count; m++)
					{
						text5 = text5 + ((UltraGridBase)ULGDataProcedures).Rows[k].ChildBands[0].Rows[l].ChildBands[0].Rows[m].Cells["ReservationProcedureStepItemID"].Value.ToString() + ",";
					}
				}
			}
			Main.DeleteForUpdate("CL_ReservationsProceduresStepsItems", "ReservationID", drMaster["ReservationID"].ToString(), "ReservationProcedureStepItemID", text5);
			Main.DeleteForUpdate("CL_ReservationsProceduresSteps", "ReservationID", drMaster["ReservationID"].ToString(), "ReservationProcedureStepID", text4);
			Main.DeleteForUpdate("CL_ReservationsProcedures", "ReservationID", drMaster["ReservationID"].ToString(), "ReservationProcedureID", text3);
			string text6 = ",";
			string text7 = ",";
			int num2 = 0;
			int num3 = 0;
			for (int n = 0; n < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataProcedures).Rows).Count; n++)
			{
				if (((UltraGridBase)ULGDataProcedures).Rows[n].Cells["ReservationID"].Value == DBNull.Value || ((UltraGridBase)ULGDataProcedures).Rows[n].Cells["ReservationID"].Value.ToString() == drMaster["ReservationID"].ToString())
				{
					num2 = ReservationsProcedures.Insert_Update((int.Parse(((UltraGridBase)ULGDataProcedures).Rows[n].Cells["ReservationProcedureID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGDataProcedures).Rows[n].Cells["ReservationProcedureID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGDataProcedures).Rows[n].Cells["ReservationProcedureID"].Value.ToString(), num.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[n].Cells["ProcedureID"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[n].Cells["Price"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[n].Cells["Discount"].Value.ToString(), (((UltraGridBase)ULGDataProcedures).Rows[n].Cells["TaxID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGDataProcedures).Rows[n].Cells["TaxID"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[n].Cells["TaxValue"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[n].Cells["NetPrice"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[n].Cells["StartDate"].Value.ToString(), (((UltraGridBase)ULGDataProcedures).Rows[n].Cells["Notes"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGDataProcedures).Rows[n].Cells["Notes"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[n].Cells["IsClosed"].Value.Equals(true) ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				}
				else if (bool.Parse(((UltraGridBase)ULGDataProcedures).Rows[n].Cells["IsClosed"].Value.ToString()))
				{
					text6 = text6 + ((UltraGridBase)ULGDataProcedures).Rows[n].Cells["ReservationProcedureID"].Value.ToString() + ",";
				}
				for (int num4 = 0; num4 < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows).Count; num4++)
				{
					if (((UltraGridBase)ULGDataProcedures).Rows[n].Cells["ReservationID"].Value == DBNull.Value || ((UltraGridBase)ULGDataProcedures).Rows[n].Cells["ReservationID"].Value.ToString() == drMaster["ReservationID"].ToString())
					{
						num3 = ReservationsProceduresSteps.Insert_Update((int.Parse(((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].Cells["ReservationProcedureStepID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].Cells["ReservationProcedureStepID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].Cells["ReservationProcedureStepID"].Value.ToString(), num.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].Cells["IsExecuted"].Value.Equals(true) ? num.ToString() : "Null", num2.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].Cells["StepID"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].Cells["Description"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].Cells["ExpectedDate"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].Cells["PricePercentage"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].Cells["Price"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].Cells["Discount"].Value.ToString(), (((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].Cells["TaxID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].Cells["TaxID"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].Cells["TaxValue"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].Cells["NetPrice"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].Cells["Notes"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].Cells["IsExecuted"].Value.Equals(true) ? "1" : "0", ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].Cells["IsExecuted"].Value.Equals(true) ? text2 : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					}
					else if (bool.Parse(((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].Cells["IsExecuted"].Value.ToString()) && ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].Cells["ExecutionReservationID"].Value == DBNull.Value)
					{
						text7 = text7 + ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].Cells["ReservationProcedureStepID"].Value.ToString() + ",";
					}
					for (int num5 = 0; num5 < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].ChildBands[0].Rows).Count; num5++)
					{
						if (((UltraGridBase)ULGDataProcedures).Rows[n].Cells["ReservationID"].Value == DBNull.Value || ((UltraGridBase)ULGDataProcedures).Rows[n].Cells["ReservationID"].Value.ToString() == drMaster["ReservationID"].ToString())
						{
							ReservationsProceduresStepsItems.Insert_Update((int.Parse(((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].ChildBands[0].Rows[num5].Cells["ReservationProcedureStepItemID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].ChildBands[0].Rows[num5].Cells["ReservationProcedureStepItemID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].ChildBands[0].Rows[num5].Cells["ReservationProcedureStepItemID"].Value.ToString(), num.ToString(), num2.ToString(), num3.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].ChildBands[0].Rows[num5].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].ChildBands[0].Rows[num5].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].ChildBands[0].Rows[num5].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].ChildBands[0].Rows[num5].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].ChildBands[0].Rows[num5].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].ChildBands[0].Rows[num5].Cells["BatchID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].ChildBands[0].Rows[num5].Cells["BatchID"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].ChildBands[0].Rows[num5].Cells["Qty"].Value.ToString(), (((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].ChildBands[0].Rows[num5].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].ChildBands[0].Rows[num5].Cells["UnitID"].Value.ToString(), (((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].ChildBands[0].Rows[num5].Cells["StoreID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].ChildBands[0].Rows[num5].Cells["StoreID"].Value.ToString(), ((UltraGridBase)ULGDataProcedures).Rows[n].ChildBands[0].Rows[num4].ChildBands[0].Rows[num5].Cells["Notes"].Value.ToString(), dtpAppointmentDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
						}
					}
				}
			}
			if (text6 != ",")
			{
				ReservationsProcedures.SetIsClosed(text6, GlobalVariables.UserID);
			}
			if (text7 != ",")
			{
				ReservationsProceduresSteps.SetIsExecuted(text7, num.ToString(), GlobalVariables.UserID);
			}
			ULGDataProcedures.AfterCellUpdate += new CellEventHandler(ULGDataProcedures_AfterCellUpdate);
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

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			ReservationsProceduresStepsItems.DeleteVirtualByReservationID(drMaster["ReservationID"].ToString(), GlobalVariables.UserID);
			ReservationsProceduresSteps.DeleteVirtualByReservationID(drMaster["ReservationID"].ToString(), GlobalVariables.UserID);
			ReservationsProcedures.DeleteVirtualByReservationID(drMaster["ReservationID"].ToString(), GlobalVariables.UserID);
			ReservationsItems.DeleteVirtualByReservationID(drMaster["ReservationID"].ToString(), GlobalVariables.UserID);
			ReservationsPayments.DeleteVirtualByReservationID(drMaster["ReservationID"].ToString(), GlobalVariables.UserID);
			Reservations.DeleteVirtual(drMaster["ReservationID"].ToString(), GlobalVariables.UserID);
			if (drMaster["StockControlJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["StockControlJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
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

	public bool SaveClose(bool Close)
	{
		ValidateForShift();
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
						DataTable dataTable = Reservations.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
						if (dataTable.Rows.Count > 0)
						{
							drMaster = dataTable.Rows[0];
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
				UpdateData();
				if (!Close)
				{
					DataTable dataTable2 = Reservations.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
					if (dataTable2.Rows.Count > 0)
					{
						drMaster = dataTable2.Rows[0];
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

	public override void btnCancelClick()
	{
		base.btnCancelClick();
		Close();
	}

	public override void btnPrintClick()
	{
		if (!(RowID != ""))
		{
			return;
		}
		ReportDocument reportDocument = new ReportDocument();
		reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_CL_Reservations2_A.rpt" : "Rep_CL_Reservations2_E.rpt"));
		GlobalFunctions.ConfigureReport(reportDocument);
		reportDocument.SetParameterValue("@ReservationID", RowID);
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
		if (ReservationPaymentID != 0)
		{
			ReportDocument reportDocument2 = new ReportDocument();
			reportDocument2.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_CL_ReservationsPayments_A.rpt" : "Rep_CL_ReservationsPayments_E.rpt"));
			GlobalFunctions.ConfigureReport(reportDocument2);
			reportDocument2.SetParameterValue("@ReservationPaymentID", ReservationPaymentID);
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
			ReservationPaymentID = 0;
		}
		GC.Collect();
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
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

	public override void btnRefreshDataClick()
	{
		object value = ((TextEditorControlBase)cboTax).Value;
		object value2 = ((TextEditorControlBase)cboClinic).Value;
		object value3 = ((TextEditorControlBase)cboDoctor).Value;
		object value4 = ((TextEditorControlBase)cboPriceType).Value;
		object value5 = ((TextEditorControlBase)cboPatients).Value;
		object value6 = ((TextEditorControlBase)cboStore).Value;
		((TextEditorControlBase)cboStore).ValueChanged -= cboStore_ValueChanged;
		((TextEditorControlBase)cboTax).ValueChanged -= cboTax_ValueChanged;
		((TextEditorControlBase)cboPatients).ValueChanged -= cboPatients_ValueChanged;
		((TextEditorControlBase)cboPriceType).ValueChanged -= cboPriceType_ValueChanged;
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		if (UsingColors)
		{
			dtItemsColorCategorysDetails = ItemsColorCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlDetailsColors.ValueListItems.Clear();
			vlStepsColors.ValueListItems.Clear();
			for (int i = 0; i < dtColors.Rows.Count; i++)
			{
				vlDetailsColors.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
				vlStepsColors.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtItemsSizeCategorysDetails = ItemsSizeCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlDetailsSizes.ValueListItems.Clear();
			vlStepsSizes.ValueListItems.Clear();
			for (int j = 0; j < dtSizes.Rows.Count; j++)
			{
				vlDetailsSizes.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
				vlStepsSizes.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
			}
		}
		UserSalesDiscount = Users.SelectSalesDiscount(GlobalVariables.UserID, IsFromServer: false);
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		UsingSalesDiscountLevels = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from G_DiscountSettings ").Rows[0][0].ToString()) > 0;
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlDetailsBatchs.ValueListItems.Clear();
			vlStepsBatchs.ValueListItems.Clear();
			for (int k = 0; k < dtBatchs.Rows.Count; k++)
			{
				vlDetailsBatchs.ValueListItems.Add(dtBatchs.Rows[k]["BatchID"], dtBatchs.Rows[k]["BatchName"].ToString());
				vlStepsBatchs.ValueListItems.Add(dtBatchs.Rows[k]["BatchID"], dtBatchs.Rows[k]["BatchName"].ToString());
			}
		}
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlDetailsStores.ValueListItems.Clear();
		vlStepsStores.ValueListItems.Clear();
		for (int l = 0; l < dtStores.Rows.Count; l++)
		{
			vlDetailsStores.ValueListItems.Add(dtStores.Rows[l]["StoreID"], dtStores.Rows[l]["StoreName"].ToString());
			vlStepsStores.ValueListItems.Add(dtStores.Rows[l]["StoreID"], dtStores.Rows[l]["StoreName"].ToString());
		}
		GlobalFunctions.FillCombo(cboStore, dtStores, "StoreID", "StoreName");
		dtItems = Items.FillComboWithoutCode("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlDetailsItems.ValueListItems.Clear();
		vlDetailsBarCode.ValueListItems.Clear();
		vlStepsItems.ValueListItems.Clear();
		vlStepsBarCode.ValueListItems.Clear();
		for (int m = 0; m < dtItems.Rows.Count; m++)
		{
			vlDetailsItems.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["Name"].ToString());
			vlDetailsBarCode.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["ItemBarCode"].ToString());
			vlStepsItems.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["Name"].ToString());
			vlStepsBarCode.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["ItemBarCode"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlDetailsUnits.ValueListItems.Clear();
		vlStepsUnits.ValueListItems.Clear();
		for (int n = 0; n < dtUnits.Rows.Count; n++)
		{
			vlDetailsUnits.ValueListItems.Add(dtUnits.Rows[n]["UnitID"], dtUnits.Rows[n]["UnitName"].ToString());
			vlStepsUnits.ValueListItems.Add(dtUnits.Rows[n]["UnitID"], dtUnits.Rows[n]["UnitName"].ToString());
		}
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTax, dtTaxs, "TaxID", "TaxName");
		vlDetailsTaxs.ValueListItems.Clear();
		vlProceduresTaxs.ValueListItems.Clear();
		for (int num = 0; num < dtTaxs.Rows.Count; num++)
		{
			vlDetailsTaxs.ValueListItems.Add(dtTaxs.Rows[num]["TaxID"], dtTaxs.Rows[num]["TaxName"].ToString());
			vlProceduresTaxs.ValueListItems.Add(dtTaxs.Rows[num]["TaxID"], dtTaxs.Rows[num]["TaxName"].ToString());
		}
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
		dtPOSDefaultData = BusinessLayer.POS.Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		dtDoctors = Doctors.FillCombo2(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDoctor, dtDoctors, "DoctorID", "DoctorName");
		dtClinics = BusinessLayer.Clinics.Clinics.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClinic, dtClinics, "ClinicID", "ClinicName");
		dtPatients = Patients.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPatients, dtPatients, "PatientID", "PatientName");
		dtVisaType = VisaTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboVisaType, dtVisaType, "VisaTypeID", "VisaTypeName");
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
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUsers.ValueListItems.Clear();
		for (int num4 = 0; num4 < dtUsers.Rows.Count; num4++)
		{
			vlUsers.ValueListItems.Add(dtUsers.Rows[num4]["User_ID"], dtUsers.Rows[num4]["UserName"].ToString());
		}
		dtProcedures = Procedures.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlProcedures.ValueListItems.Clear();
		for (int num5 = 0; num5 < dtProcedures.Rows.Count; num5++)
		{
			vlProcedures.ValueListItems.Add(dtProcedures.Rows[num5]["ProcedureID"], dtProcedures.Rows[num5]["ProcedureName"].ToString());
		}
		dtSteps = Steps.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlSteps.ValueListItems.Clear();
		for (int num6 = 0; num6 < dtSteps.Rows.Count; num6++)
		{
			vlSteps.ValueListItems.Add(dtSteps.Rows[num6]["StepID"], dtSteps.Rows[num6]["StepName"].ToString());
		}
		((TextEditorControlBase)cboTax).Value = value;
		((TextEditorControlBase)cboClinic).Value = value2;
		((TextEditorControlBase)cboDoctor).Value = value3;
		((TextEditorControlBase)cboPriceType).Value = value4;
		((TextEditorControlBase)cboPatients).Value = value5;
		((TextEditorControlBase)cboStore).Value = value6;
		((TextEditorControlBase)cboStore).ValueChanged += cboStore_ValueChanged;
		((TextEditorControlBase)cboTax).ValueChanged += cboTax_ValueChanged;
		((TextEditorControlBase)cboPatients).ValueChanged += cboPatients_ValueChanged;
		((TextEditorControlBase)cboPriceType).ValueChanged += cboPriceType_ValueChanged;
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["ForceBarcodeUse"].ToString()) && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty"))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value == DBNull.Value)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "NetPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" && !bool.Parse(dtItems.Select(" ItemID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["CanModifyPrice"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxID" && ((UltraToggleEditorBase)chkTax).Checked)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "StoreID") && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Expected O, but got Unknown
		if (ULGData.ActiveCell != null)
		{
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode")
			{
				ULGData_CellListSelect(ULGData, e);
			}
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
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
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxID" && e.Cell.Value == DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TaxValue"].Value = 0;
				CalculateGoss();
			}
			CalculateRow(((UltraGridBase)ULGData).ActiveRow);
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
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
		else if (e.KeyCode == Keys.F6)
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
					vlDetailsBatchs.ValueListItems.Clear();
					for (int i = 0; i < dtBatchs.Rows.Count; i++)
					{
						vlDetailsBatchs.ValueListItems.Add(dtBatchs.Rows[i]["BatchID"], dtBatchs.Rows[i]["BatchName"].ToString());
					}
				}
				else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value)
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
		else if (e.KeyCode == Keys.F8)
		{
			if (Adding || Updating)
			{
				if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode")
				{
					int num = (Adding ? SearchFunctions.Items("-1", "-1", "-1", "1", "1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false) : SearchFunctions.Items("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false));
					if (num != 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = num;
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = num;
					}
				}
				else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxID")
				{
					int num2 = SearchFunctions.TaxsSearch(IsFromServer: false);
					if (num2 != 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = num2;
					}
				}
				else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select(" ItemID = '" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString() + "'")[0]["EnforceBatchNo"].ToString()))
				{
					int num3 = SearchFunctions.ItemsBatchesSearch(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()), 0, IsFromServer: false);
					if (num3 != 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = num3;
					}
				}
			}
			e.Handled = true;
		}
		else
		{
			if (e.KeyCode != Keys.F9)
			{
				return;
			}
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemSizeID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ColorID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID") && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value)
			{
				DataTable dataTable = Items.Select(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
				frmImageViewer frmImageViewer2 = new frmImageViewer((dataTable.Rows[0]["ItemPic"] == DBNull.Value) ? null : ImageFunctions.BinaryToImage((byte[])dataTable.Rows[0]["ItemPic"]), GlobalVariables.IsArabic ? dataTable.Rows[0]["ItemNameAr"].ToString() : dataTable.Rows[0]["ItemNameEn"].ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate));
				frmImageViewer2.WindowState = FormWindowState.Normal;
				frmImageViewer2.ShowDialog();
				if (Adding && frmImageViewer2.Saved)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = (frmImageViewer2.ColorID.Equals("-1") ? ((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value : frmImageViewer2.ColorID);
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = (frmImageViewer2.ItemSizeID.Equals("-1") ? ((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value : frmImageViewer2.ItemSizeID);
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = (frmImageViewer2.BatchID.Equals("-1") ? ((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value : frmImageViewer2.BatchID);
					((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = (frmImageViewer2.StoreID.Equals("-1") ? ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value : frmImageViewer2.StoreID);
				}
			}
			e.Handled = true;
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue"))
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0d23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2d: Expected O, but got Unknown
		//IL_0d3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d45: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if ((((KeyedSubObjectBase)e.Cell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID") && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"];
			object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = e.Cell.Value);
			obj.Value = value;
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((cboClinic.SelectedIndex > -1) ? dtClinics.Select("ClinicID = " + ((TextEditorControlBase)cboClinic).Value.ToString())[0]["StoreID"] : dataRow["StoreID"]);
			if (dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["ApplySalesTax"].ToString()))
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = ((dtPOSDefaultData.Rows[0]["TaxID"] == DBNull.Value) ? dtItems.Select(" ItemID= " + e.Cell.Value)[0]["TaxID"] : dtPOSDefaultData.Rows[0]["TaxID"]);
			}
			else
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = DBNull.Value;
			}
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && cboPatients.SelectedIndex > -1)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * Math.Round(decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()), 3) / 100m;
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				CalculateGoss();
				CalculateRow(e.Cell.Row);
				CalculateTotalsTax();
			}
			int num = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtItems.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"].ToString()) : 0);
			if (UsingBatchNoAndValidityPeriod)
			{
				if (num != 0)
				{
					e.Cell.Row.Cells["BatchID"].Value = DBNull.Value;
					e.Cell.Row.Cells["BatchID"].ValueList = (IValueList)(object)getBatchsValueList(num);
				}
				else
				{
					e.Cell.Row.Cells["BatchID"].ValueList = null;
					e.Cell.Row.Cells["BatchID"].Value = DBNull.Value;
				}
			}
			int num2 = ((((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value != DBNull.Value) ? int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()) : 0);
			if (num2 != 0)
			{
				e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
				e.Cell.Row.Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(num2);
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
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "TaxID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			CalculateRow(e.Cell.Row);
			CalculateTotalsTax();
		}
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "BatchID")
		{
			int num3 = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtBatchs.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"].ToString()) : 0);
			if (num3 != 0)
			{
				DataRow dataRow2 = dtItems.Select(" ItemID= " + num3)[0];
				UltraGridCell obj2 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"];
				object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = num3);
				obj2.Value = value;
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow2["UnitID"];
				int num4 = ((((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value != DBNull.Value) ? int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()) : 0);
				if (num4 != 0)
				{
					e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
					e.Cell.Row.Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(num4);
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

	private void ULGData_AfterExitEditMode(object sender, EventArgs e)
	{
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Expected O, but got Unknown
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Expected O, but got Unknown
		ULGData.AfterExitEditMode -= ULGData_AfterExitEditMode;
		if (ULGData.ActiveCell != null && ULGData.ActiveCell.Value != null && ULGData.ActiveCell.Value != DBNull.Value && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID") && dtItems.Select(" ItemID= " + ULGData.ActiveCell.Value.ToString()).Length == 0)
		{
			UltraGridCell activeCell = ULGData.ActiveCell;
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
			object obj2 = (((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = DBNull.Value);
			object value2 = (obj.Value = obj2);
			activeCell.Value = value2;
		}
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				try
				{
					UltraGridRow val = ((UltraGridBase)ULGData).Rows[i];
					if (((UltraGridBase)ULGData).ActiveRow.Index != i && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString() == val.Cells["ItemID"].Value.ToString() && ((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value.ToString() == val.Cells["ColorID"].Value.ToString() && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value.ToString() == val.Cells["ItemSizeID"].Value.ToString() && ((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value.ToString() == val.Cells["BatchID"].Value.ToString() && ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value.ToString() == val.Cells["StoreID"].Value.ToString() && ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString() == val.Cells["UnitID"].Value.ToString())
					{
						GlobalVariables.InformationMB.Show("لا يمكن تكرار الصنف  مع نفس المخزن", "Cannot Duplicate The Same Item With  Same Store");
						ULGData.BeforeRowsDeleted -= new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
						((UltraGridBase)ULGData).ActiveRow.Delete(false);
						ULGData.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
						((GridItemBase)val).Selected = true;
						val.Activate();
					}
				}
				catch
				{
				}
			}
		}
		ULGData.AfterExitEditMode += ULGData_AfterExitEditMode;
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

	private void chkArrived_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)dtpArrivalDate).Enabled = ((UltraToggleEditorBase)chkArrived).Checked;
		if (((UltraToggleEditorBase)chkArrived).Checked)
		{
			dtpArrivalDate.DateTime = DateTime.Now;
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

	private void txtPaidAmount_ValueChanged(object sender, EventArgs e)
	{
		((TextEditorControlBase)txtPaidAmount).ValueChanged -= txtPaidAmount_ValueChanged;
		((Control)(object)txtPaidAmount).Text = decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtPaidAmount).ValueChanged += txtPaidAmount_ValueChanged;
	}

	private void chkVisa_CheckedChanged(object sender, EventArgs e)
	{
		UltraComboEditor obj = cboVisaType;
		bool enabled = (((Control)(object)txtVisaNo).Enabled = ((UltraToggleEditorBase)chkVisa).Checked);
		((Control)(object)obj).Enabled = enabled;
	}

	private void btnOrderPayments_Click(object sender, EventArgs e)
	{
		if (decimal.Parse((((Control)(object)txtRestAmount).Text == "" || ((Control)(object)txtRestAmount).Text == ".") ? "0" : ((Control)(object)txtRestAmount).Text) != 0m)
		{
			if (SaveClose(Close: false) && Updating)
			{
				frmReservationsPayments frmReservationsPayments2 = new frmReservationsPayments(int.Parse(drMaster["ReservationID"].ToString()), decimal.Parse(((Control)(object)txtNetprice).Text.ToString()) - decimal.Parse(((Control)(object)txtPaidAmount).Text.ToString()));
				frmReservationsPayments2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmReservationsPayments2.lblTitle).Text = (GlobalVariables.IsArabic ? "سداد" : "Payments");
				frmReservationsPayments2.CanAdd = true;
				frmReservationsPayments2.ShowDialog();
				dtPayments = ReservationsPayments.SelectByReservationID(drMaster["ReservationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
				DataTable dataTable = dtPayments;
				object obj = dtPayments.Compute(" Sum(Amount) ", "");
				((TextEditorControlBase)txtPaidAmount).ValueChanged -= txtPaidAmount_ValueChanged;
				((Control)(object)txtPaidAmount).Text = decimal.Parse((obj == DBNull.Value) ? "0" : obj.ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse((((Control)(object)txtNetprice).Text == "" || ((Control)(object)txtNetprice).Text == ".") ? "0" : ((Control)(object)txtNetprice).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((TextEditorControlBase)txtPaidAmount).ValueChanged += txtPaidAmount_ValueChanged;
				((UltraGridBase)ULGDataPayments).DataSource = dtPayments;
				InitGridPayment();
				base.btnOKClick();
			}
		}
		else
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "القيمة مسددة بالكامل" : "The Amount Is Fully Paid");
		}
	}

	private void lblRestAmount_Click(object sender, EventArgs e)
	{
	}

	private void txtRestAmount_ValueChanged(object sender, EventArgs e)
	{
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

	private void chkTax_CheckedChanged(object sender, EventArgs e)
	{
		((EditorButtonControlBase)cboTax).ReadOnly = !((UltraToggleEditorBase)chkTax).Checked;
	}

	private void cboTax_ValueChanged(object sender, EventArgs e)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Expected O, but got Unknown
		if (cboTax.SelectedIndex > -1)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["TaxID"].Value = ((TextEditorControlBase)cboTax).Value;
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].DefaultCellValue = ((TextEditorControlBase)cboTax).Value;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
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

	private void btnPriceTypeSearch_Click(object sender, EventArgs e)
	{
		frmPricesTypesChange frmPricesTypesChange2 = new frmPricesTypesChange(base.Name);
		frmPricesTypesChange2.WindowState = FormWindowState.Normal;
		frmPricesTypesChange2.ShowDialog();
		((TextEditorControlBase)cboPriceType).Value = ((frmPricesTypesChange2.PriceTypeID > 0) ? ((object)frmPricesTypesChange2.PriceTypeID) : ((TextEditorControlBase)cboPriceType).Value);
		NewPriceUserID = frmPricesTypesChange2.UserID;
	}

	private void cboPriceType_ValueChanged(object sender, EventArgs e)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Expected O, but got Unknown
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Expected O, but got Unknown
		if (cboPriceType.SelectedIndex > -1)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			ULGDataProcedures.AfterCellUpdate -= new CellEventHandler(ULGDataProcedures_AfterCellUpdate);
			dtItemPrices = ItemsPrices.GetPriceWithItemDiscount(((TextEditorControlBase)cboPriceType).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtProceduresPrices = ProceduresPrices.GetPrice(((TextEditorControlBase)cboPriceType).Value.ToString(), IsFromServer: false);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * Math.Round(decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()), 3) / 100m;
				((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataProcedures).Rows).Count; j++)
			{
				((UltraGridBase)ULGDataProcedures).Rows[j].Cells["Price"].Value = decimal.Parse(dtProceduresPrices.Select(" ProcedureID = " + ((UltraGridBase)ULGDataProcedures).Rows[j].Cells["ProcedureID"].Value.ToString())[0]["Price"].ToString());
				CalculateProcedureRow(((UltraGridBase)ULGDataProcedures).Rows[j]);
			}
			CalculateGoss();
			CalculateTotalsTax();
			ULGDataProcedures.AfterCellUpdate += new CellEventHandler(ULGDataProcedures_AfterCellUpdate);
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		else
		{
			dtItemPrices = null;
			dtProceduresPrices = null;
		}
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

	private void txtCommercialTax_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			CalculateNetTotals();
		}
	}

	private void txtDiscBeforeTaxRatio_ValueChanged(object sender, EventArgs e)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Expected O, but got Unknown
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Expected O, but got Unknown
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		if (CalculateGrossWithoutItemUnderDiscount() > 0m)
		{
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			ULGDataProcedures.AfterCellUpdate -= new CellEventHandler(ULGDataProcedures_AfterCellUpdate);
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * CalculateGrossWithoutItemUnderDiscount(), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if ((cboPatients.SelectedIndex > -1 && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > decimal.Parse(dtPatients.Select(" PatientID = " + ((TextEditorControlBase)cboPatients).Value)[0]["DiscountPercentage"].ToString()) && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > UserSalesDiscount) || (cboPatients.SelectedIndex == -1 && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > UserSalesDiscount))
			{
				OpenChangeDiscountForm();
			}
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			ULGDataProcedures.AfterCellUpdate += new CellEventHandler(ULGDataProcedures_AfterCellUpdate);
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

	private void txtDiscBeforeTaxValue_ValueChanged(object sender, EventArgs e)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		if (CalculateGrossWithoutItemUnderDiscount() > 0m)
		{
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == "0" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) / CalculateGrossWithoutItemUnderDiscount() * 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if ((cboPatients.SelectedIndex > -1 && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > decimal.Parse(dtPatients.Select(" PatientID= " + ((TextEditorControlBase)cboPatients).Value)[0]["DiscountPercentage"].ToString()) && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > UserSalesDiscount) || (cboPatients.SelectedIndex == -1 && decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) > UserSalesDiscount))
			{
				OpenChangeDiscountForm();
			}
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
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

	private void cboPatients_ValueChanged(object sender, EventArgs e)
	{
		//IL_0617: Unknown result type (might be due to invalid IL or missing references)
		//IL_0621: Expected O, but got Unknown
		//IL_062f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Expected O, but got Unknown
		//IL_0a0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a14: Expected O, but got Unknown
		//IL_0a22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2c: Expected O, but got Unknown
		if (cboPatients.SelectedIndex <= -1)
		{
			return;
		}
		((Control)(object)txtBranchBalance).Text = decimal.Parse(Math.Round(Patients.GetBalance(((TextEditorControlBase)cboPatients).Value.ToString(), "," + (Adding ? GlobalVariables.CurrentBranchID : drMaster["BranchID"].ToString()) + ",", IsFromServer: false), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		if (dtPatients.Select(" PatientID= " + ((TextEditorControlBase)cboPatients).Value)[0]["Notes"].ToString() != "")
		{
			GlobalVariables.InformationMB.Show(dtPatients.Select(" PatientID= " + ((TextEditorControlBase)cboPatients).Value)[0]["Notes"].ToString());
		}
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
		((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse(dtPatients.Select(" PatientID= " + ((TextEditorControlBase)cboPatients).Value)[0]["DiscountPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
		((TextEditorControlBase)txtFeesDiscRatio).ValueChanged -= txtFeesDiscRatio_ValueChanged;
		((Control)(object)txtFeesDiscRatio).Text = decimal.Parse(dtPatients.Select(" PatientID= " + ((TextEditorControlBase)cboPatients).Value)[0]["DiscountPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtFeesDiscRatio).ValueChanged += txtFeesDiscRatio_ValueChanged;
		CalculateFees();
		((TextEditorControlBase)cboPriceType).ValueChanged -= cboPriceType_ValueChanged;
		if (dtPatients.Select(" PatientID= " + ((TextEditorControlBase)cboPatients).Value)[0]["PriceTypeID"] != DBNull.Value)
		{
			((TextEditorControlBase)cboPriceType).Value = dtPatients.Select(" PatientID= " + ((TextEditorControlBase)cboPatients).Value)[0]["PriceTypeID"];
		}
		else if (cboDoctor.SelectedIndex > -1 && dtDoctors.Select(" DoctorID = " + ((TextEditorControlBase)cboDoctor).Value)[0]["PriceTypeID"] != DBNull.Value)
		{
			((TextEditorControlBase)cboPriceType).Value = dtDoctors.Select(" DoctorID= " + ((TextEditorControlBase)cboDoctor).Value)[0]["PriceTypeID"];
		}
		else if (cboClinic.SelectedIndex > -1 && dtClinics.Select(" ClinicID = " + ((TextEditorControlBase)cboClinic).Value)[0]["PriceTypeID"] != DBNull.Value)
		{
			((TextEditorControlBase)cboPriceType).Value = dtClinics.Select(" ClinicID= " + ((TextEditorControlBase)cboClinic).Value)[0]["PriceTypeID"];
		}
		((TextEditorControlBase)cboPriceType).ValueChanged += cboPriceType_ValueChanged;
		dtReservationProcedures = ReservationsProcedures.SelectByReservationIDNotClosed("0", ((TextEditorControlBase)cboClinic).Value.ToString(), ((TextEditorControlBase)cboPatients).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		dtReservationProceduresSteps = ReservationsProceduresSteps.SelectByReservationIDNotClosed("0", ((TextEditorControlBase)cboClinic).Value.ToString(), ((TextEditorControlBase)cboPatients).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		dtReservationProceduresStepsItems = ReservationsProceduresStepsItems.SelectByReservationIDNotClosed("0", ((TextEditorControlBase)cboClinic).Value.ToString(), ((TextEditorControlBase)cboPatients).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		ds = new DataSet();
		ds.Tables.Add(dtReservationProcedures);
		ds.Tables.Add(dtReservationProceduresSteps);
		ds.Tables.Add(dtReservationProceduresStepsItems);
		ds.Tables[0].TableName = "dtReservationProcedures";
		ds.Tables[1].TableName = "dtReservationProceduresSteps";
		ds.Tables[2].TableName = "dtReservationProceduresStepsItems";
		ds.Relations.Add(ds.Tables[0].Columns["ReservationProcedureID"], ds.Tables[1].Columns["ReservationProcedureID"]);
		ds.Relations.Add(ds.Tables[1].Columns["ReservationProcedureStepID"], ds.Tables[2].Columns["ReservationProcedureStepID"]);
		((UltraGridBase)ULGDataProcedures).DataSource = ds;
		InitGridProcedures();
		if (cboPriceType.SelectedIndex > -1)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			ULGDataProcedures.AfterCellUpdate -= new CellEventHandler(ULGDataProcedures_AfterCellUpdate);
			dtItemPrices = ItemsPrices.GetPriceWithItemDiscount(((TextEditorControlBase)cboPriceType).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtProceduresPrices = ProceduresPrices.GetPrice(((TextEditorControlBase)cboPriceType).Value.ToString(), IsFromServer: false);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * Math.Round(decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()), 3) / 100m;
					((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
					CalculateRow(((UltraGridBase)ULGData).Rows[i]);
				}
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataProcedures).Rows).Count; j++)
			{
				if (((UltraGridBase)ULGDataProcedures).Rows[j].Cells["ProcedureID"].Value != DBNull.Value)
				{
					((UltraGridBase)ULGDataProcedures).Rows[j].Cells["Price"].Value = decimal.Parse(dtProceduresPrices.Select(" ProcedureID= " + ((UltraGridBase)ULGDataProcedures).Rows[j].Cells["ProcedureID"].Value.ToString())[0]["Price"].ToString());
					CalculateProcedureRow(((UltraGridBase)ULGDataProcedures).Rows[j]);
				}
			}
			CalculateGoss();
			CalculateTotalsTax();
			ULGDataProcedures.AfterCellUpdate += new CellEventHandler(ULGDataProcedures_AfterCellUpdate);
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		else
		{
			dtItemPrices = null;
			dtProceduresPrices = null;
			((TextEditorControlBase)cboPriceType).ValueChanged -= cboPriceType_ValueChanged;
			cboPriceType.SelectedIndex = -1;
			((TextEditorControlBase)cboPriceType).ValueChanged += cboPriceType_ValueChanged;
		}
		dtAccruals = Reservations.SelectNotPaid(((TextEditorControlBase)cboPatients).Value.ToString(), Adding ? "-1" : drMaster["ReservationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGAccruals).DataSource = dtAccruals;
		InitGridAccruals();
		if (dtAccruals.Rows.Count > 0)
		{
			((UltraTabControlBase)UTCDetails).Tabs["Accruals"].Visible = true;
			((UltraTabControlBase)UTCDetails).Tabs["Accruals"].Selected = true;
		}
		else
		{
			((UltraTabControlBase)UTCDetails).Tabs[0].Selected = true;
			((UltraTabControlBase)UTCDetails).Tabs["Accruals"].Visible = false;
		}
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Expected O, but got Unknown
		if (cboPriceType.SelectedIndex > -1)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			dtItemPrices = ItemsPrices.GetPriceWithItemDiscount(((TextEditorControlBase)cboPriceType).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * Math.Round(decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()), 3) / 100m;
				((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateGoss();
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		else
		{
			dtItemPrices = null;
		}
	}

	private void CalculateFees()
	{
		decimal num = default(decimal);
		if (cboReservationType.SelectedIndex > -1)
		{
			num = ((cboReservationType.SelectedIndex == 0) ? decimal.Parse(dtDoctors.Select("DoctorID =" + DoctorID)[0]["Fees"].ToString()) : ((cboReservationType.SelectedIndex != 1) ? decimal.Parse(dtDoctors.Select("DoctorID =" + DoctorID)[0]["ExtendedFees"].ToString()) : decimal.Parse(dtDoctors.Select("DoctorID =" + DoctorID)[0]["ConsultingFees"].ToString())));
		}
		((Control)(object)txtFeesAmount).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtFeesDiscValue).ValueChanged -= txtFeesDiscValue_ValueChanged;
		((Control)(object)txtFeesDiscValue).Text = decimal.Parse((decimal.Parse(((Control)(object)txtFeesAmount).Text) * decimal.Parse(((Control)(object)txtFeesDiscRatio).Text) / 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtFeesDiscValue).ValueChanged += txtFeesDiscValue_ValueChanged;
		CalculateNetFees();
	}

	private void CalculateNetFees()
	{
		((Control)(object)txtFees).Text = decimal.Parse((decimal.Parse(((Control)(object)txtFeesAmount).Text) - decimal.Parse((((Control)(object)txtFeesDiscValue).Text == "" || ((Control)(object)txtFeesDiscValue).Text == ".") ? "0" : ((Control)(object)txtFeesDiscValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		CalculateNetTotals();
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
		((Control)(object)txtTaxTotalValue).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		CalculateNetTotals();
	}

	private void CalculateRow(UltraGridRow Row)
	{
		if (((Control)(object)txtDiscBeforeTaxRatio).Text != "" && ((Control)(object)txtDiscBeforeTaxRatio).Text != "." && ((Control)(object)txtGrossValue).Text != "" && ((Control)(object)txtGrossValue).Text != ".")
		{
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && Row.Cells["ItemID"].Value != DBNull.Value && decimal.Parse(dtItemPrices.Select(" ItemID= " + Row.Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()) > 0m)
			{
				Row.Cells["DisCount"].Value = 0;
			}
			else
			{
				Row.Cells["DisCount"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) * decimal.Parse(((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m;
			}
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

	private void CalculateNetTotals()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataProcedures).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataProcedures).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (bool.Parse(((UltraGridBase)ULGDataProcedures).Rows[i].ChildBands[0].Rows[j].Cells["IsExecuted"].Value.ToString()) && (((UltraGridBase)ULGDataProcedures).Rows[i].ChildBands[0].Rows[j].Cells["ExecutionReservationID"].Value == DBNull.Value || (drMaster != null && ((UltraGridBase)ULGDataProcedures).Rows[i].ChildBands[0].Rows[j].Cells["ExecutionReservationID"].Value.ToString() == drMaster["ReservationID"].ToString())))
				{
					num += decimal.Parse(((UltraGridBase)ULGDataProcedures).Rows[i].ChildBands[0].Rows[j].Cells["NetPrice"].Value.ToString());
				}
			}
		}
		((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse((((Control)(object)txtFees).Text == "" || ((Control)(object)txtFees).Text == ".") ? "0" : ((Control)(object)txtFees).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text) - decimal.Parse((((Control)(object)txtCommercialTax).Text == "" || ((Control)(object)txtCommercialTax).Text == ".") ? "0" : ((Control)(object)txtCommercialTax).Text) + num).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtRestAmount).ValueChanged -= txtCommercialTax_ValueChanged;
		((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtRestAmount).ValueChanged += txtCommercialTax_ValueChanged;
	}

	private void CalculateRowActualUnitSalesPrice(UltraGridRow Row)
	{
		Row.Cells["ActualUnitSalesPrice"].Value = (decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) + decimal.Parse(Row.Cells["TaxValue"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString()) + decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text)) / decimal.Parse(Row.Cells["Qty"].Value.ToString());
	}

	private decimal CalculateGrossWithoutItemUnderDiscount()
	{
		decimal result = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value != DBNull.Value && decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()) > 0m)
			{
				result += 0m;
				continue;
			}
			((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString());
			result += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
		}
		return result;
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

	public void OpenChangeDiscountForm()
	{
		frmChangeDiscount frmChangeDiscount2 = new frmChangeDiscount(decimal.Parse(((Control)(object)txtGrossValue).Text), decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text), decimal.Parse(((Control)(object)txtDiscBeforeTaxRatio).Text));
		frmChangeDiscount2.WindowState = FormWindowState.Normal;
		frmChangeDiscount2.ShowDialog();
		if (frmChangeDiscount2.Cancel)
		{
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse((cboPatients.SelectedIndex > -1 && decimal.Parse(dtPatients.Select(" PatientID = " + ((TextEditorControlBase)cboPatients).Value)[0]["DiscountPercentage"].ToString()) > UserSalesDiscount) ? dtPatients.Select(" PatientID= " + ((TextEditorControlBase)cboPatients).Value)[0]["DiscountPercentage"].ToString() : UserSalesDiscount.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * CalculateGrossWithoutItemUnderDiscount(), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			return;
		}
		((Control)(object)txtDiscBeforeTaxRatio).Text = string.Concat(frmChangeDiscount2.DiscountRatio);
		((Control)(object)txtDiscBeforeTaxValue).Text = string.Concat(frmChangeDiscount2.DiscountValue);
		DiscountUserID = frmChangeDiscount2.UserID;
		frmChangeDiscount2.Dispose();
	}

	public void OpenChangeDiscountFormForFees()
	{
		frmChangeDiscount frmChangeDiscount2 = new frmChangeDiscount(decimal.Parse((((Control)(object)txtFeesAmount).Text == "" || ((Control)(object)txtFeesAmount).Text == ".") ? "0" : ((Control)(object)txtFeesAmount).Text), decimal.Parse(((Control)(object)txtFeesDiscValue).Text), decimal.Parse(((Control)(object)txtFeesDiscRatio).Text));
		frmChangeDiscount2.WindowState = FormWindowState.Normal;
		frmChangeDiscount2.ShowDialog();
		if (frmChangeDiscount2.Cancel)
		{
			((Control)(object)txtFeesDiscRatio).Text = decimal.Parse((cboPatients.SelectedIndex > -1 && decimal.Parse(dtPatients.Select(" PatientID = " + ((TextEditorControlBase)cboPatients).Value)[0]["DiscountPercentage"].ToString()) > UserSalesDiscount) ? dtPatients.Select(" PatientID= " + ((TextEditorControlBase)cboPatients).Value)[0]["DiscountPercentage"].ToString() : UserSalesDiscount.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtFeesDiscValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtFeesDiscRatio).Text == "" || ((Control)(object)txtFeesDiscRatio).Text == ".") ? "0" : ((Control)(object)txtFeesDiscRatio).Text) / 100m * decimal.Parse((((Control)(object)txtFeesAmount).Text == "" || ((Control)(object)txtFeesAmount).Text == ".") ? "0" : ((Control)(object)txtFeesAmount).Text), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			return;
		}
		((Control)(object)txtFeesDiscRatio).Text = string.Concat(frmChangeDiscount2.DiscountRatio);
		((Control)(object)txtFeesDiscValue).Text = string.Concat(frmChangeDiscount2.DiscountValue);
		DiscountUserID = frmChangeDiscount2.UserID;
		frmChangeDiscount2.Dispose();
	}

	private void cboReservationType_ValueChanged(object sender, EventArgs e)
	{
		CalculateFees();
	}

	private void ULGAccruals_ClickCellButton(object sender, CellEventArgs e)
	{
		if (((UltraGridBase)ULGAccruals).ActiveRow != null)
		{
			frmReservationsPayments frmReservationsPayments2 = new frmReservationsPayments(int.Parse(((UltraGridBase)ULGAccruals).ActiveRow.Cells["ReservationID"].Value.ToString()), decimal.Parse(((UltraGridBase)ULGAccruals).ActiveRow.Cells["RestAmount"].Value.ToString()));
			frmReservationsPayments2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmReservationsPayments2.lblTitle).Text = (GlobalVariables.IsArabic ? "سداد" : "Payments");
			frmReservationsPayments2.CanAdd = true;
			frmReservationsPayments2.ShowDialog();
			dtAccruals = Reservations.SelectNotPaid(((TextEditorControlBase)cboPatients).Value.ToString(), Adding ? "-1" : drMaster["ReservationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGAccruals).DataSource = dtAccruals;
			InitGridAccruals();
		}
	}

	private void ULGAccruals_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGAccruals).ActiveRow).Selected = true;
	}

	private void ULGDataPayments_ClickCellButton(object sender, CellEventArgs e)
	{
		if (((UltraGridBase)ULGDataPayments).ActiveRow != null && ((KeyedSubObjectBase)e.Cell.Column).Key == "Print")
		{
			ReportDocument reportDocument = new ReportDocument();
			reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_CL_ReservationsPayments_A.rpt" : "Rep_CL_ReservationsPayments_E.rpt"));
			GlobalFunctions.ConfigureReport(reportDocument);
			reportDocument.SetParameterValue("@ReservationPaymentID", ((UltraGridBase)ULGDataPayments).ActiveRow.Cells["ReservationPaymentID"].Value);
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

	private void ULGDataProcedures_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((GridItemBase)((UltraGridBase)ULGDataProcedures).ActiveRow).Band.Index == 0)
		{
			if (bool.Parse(((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["IsClosed"].Value.ToString()))
			{
				((GridItemBase)((UltraGridBase)ULGDataProcedures).ActiveRow).Selected = true;
			}
			else if (((KeyedSubObjectBase)ULGDataProcedures.ActiveCell.Column).Key == "Discount")
			{
				if (((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["ReservationID"].Value != DBNull.Value && (drMaster == null || !(((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["ReservationID"].Value.ToString() == drMaster["ReservationID"].ToString())))
				{
					((GridItemBase)((UltraGridBase)ULGDataProcedures).ActiveRow).Selected = true;
				}
			}
			else if (((KeyedSubObjectBase)ULGDataProcedures.ActiveCell.Column).Key == "Price")
			{
				if (((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["ProcedureID"].Value != DBNull.Value)
				{
					if (bool.Parse(dtProcedures.Select("ProcedureID = " + ((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["ProcedureID"].Value.ToString())[0]["CanModifyPrice"].ToString()))
					{
						if (((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["ReservationID"].Value != DBNull.Value && (drMaster == null || !(((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["ReservationID"].Value.ToString() == drMaster["ReservationID"].ToString())))
						{
							((GridItemBase)((UltraGridBase)ULGDataProcedures).ActiveRow).Selected = true;
						}
					}
					else
					{
						((GridItemBase)((UltraGridBase)ULGDataProcedures).ActiveRow).Selected = true;
					}
				}
				else
				{
					((GridItemBase)((UltraGridBase)ULGDataProcedures).ActiveRow).Selected = true;
				}
			}
			else if (((KeyedSubObjectBase)ULGDataProcedures.ActiveCell.Column).Key == "NetPrice" || ((KeyedSubObjectBase)ULGDataProcedures.ActiveCell.Column).Key == "TaxValue")
			{
				((GridItemBase)((UltraGridBase)ULGDataProcedures).ActiveRow).Selected = true;
			}
			else if (((KeyedSubObjectBase)ULGDataProcedures.ActiveCell.Column).Key != "IsClosed" && ((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["ReservationID"].Value != DBNull.Value && (Adding || (Updating && drMaster["ReservationID"].ToString() != ((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["ReservationID"].Value.ToString())))
			{
				((GridItemBase)((UltraGridBase)ULGDataProcedures).ActiveRow).Selected = true;
			}
		}
		else if (((GridItemBase)((UltraGridBase)ULGDataProcedures).ActiveRow).Band.Index == 1)
		{
			if (((KeyedSubObjectBase)ULGDataProcedures.ActiveCell.Column).Key == "PricePercentage" || ((KeyedSubObjectBase)ULGDataProcedures.ActiveCell.Column).Key == "Price" || ((KeyedSubObjectBase)ULGDataProcedures.ActiveCell.Column).Key == "StepID" || bool.Parse(((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["IsExecuted"].Value.ToString()))
			{
				((GridItemBase)((UltraGridBase)ULGDataProcedures).ActiveRow).Selected = true;
			}
			else if (((KeyedSubObjectBase)ULGDataProcedures.ActiveCell.Column).Key != "IsExecuted" && ((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["ReservationID"].Value != DBNull.Value && (Adding || (Updating && drMaster["ReservationID"].ToString() != ((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["ReservationID"].Value.ToString())))
			{
				((GridItemBase)((UltraGridBase)ULGDataProcedures).ActiveRow).Selected = true;
			}
		}
		else if (((GridItemBase)((UltraGridBase)ULGDataProcedures).ActiveRow).Band.Index == 2)
		{
			((GridItemBase)((UltraGridBase)ULGDataProcedures).ActiveRow).Selected = true;
		}
	}

	private void ULGDataProcedures_AfterRowInsert(object sender, RowEventArgs e)
	{
		if (((GridItemBase)e.Row).Band.Index == 0)
		{
			e.Row.Cells["ReservationProcedureID"].Value = ++newID;
		}
		else if (((GridItemBase)e.Row).Band.Index == 1)
		{
			e.Row.Cells["ReservationProcedureStepID"].Value = ++newID;
		}
		else if (((GridItemBase)e.Row).Band.Index == 2)
		{
			e.Row.Cells["ReservationProcedureStepItemID"].Value = ++newID;
		}
	}

	private void ULGDataProcedures_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_0a6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a74: Expected O, but got Unknown
		//IL_0a82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8c: Expected O, but got Unknown
		object obj = null;
		ULGDataProcedures.CellListSelect -= new CellEventHandler(ULGDataProcedures_CellListSelect);
		ULGDataProcedures.AfterCellUpdate -= new CellEventHandler(ULGDataProcedures_AfterCellUpdate);
		if (((GridItemBase)e.Cell).Band.Index == 0)
		{
			if (((KeyedSubObjectBase)e.Cell.Column).Key == "ProcedureID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
			{
				((UltraGridBase)ULGDataProcedures).UpdateData();
				DataRow dataRow = dtProcedures.Select(" ProcedureID= " + e.Cell.Value.ToString())[0];
				((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["ReservationID"].Value = (Adding ? DBNull.Value : drMaster["ReservationID"]);
				if (dtProceduresPrices != null && dtProceduresPrices.Rows.Count > 0 && cboPatients.SelectedIndex > -1)
				{
					((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["Price"].Value = decimal.Parse(dtProceduresPrices.Select(" ProcedureID= " + e.Cell.Value.ToString())[0]["Price"].ToString());
					CalculateProcedureRow(e.Cell.Row);
					CalculateNetTotals();
				}
				((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["StartDate"].Value = dtpAppointmentDate.Value;
				DataTable dataTable = ProceduresSteps.SelectByProcedureID(e.Cell.Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
				DataRow[] array = dtReservationProceduresSteps.Select("ReservationProcedureID = " + e.Cell.Row.Cells["ReservationProcedureID"].Value.ToString());
				DataRow[] array2 = array;
				foreach (DataRow row in array2)
				{
					dtReservationProceduresSteps.Rows.Remove(row);
				}
				DataRow[] array3 = dtReservationProceduresStepsItems.Select("ReservationProcedureID = " + e.Cell.Row.Cells["ReservationProcedureID"].Value.ToString());
				DataRow[] array4 = array3;
				foreach (DataRow row2 in array4)
				{
					dtReservationProceduresStepsItems.Rows.Remove(row2);
				}
				if (cboClinic.SelectedIndex > -1)
				{
					obj = dtClinics.Select("ClinicID = " + ((TextEditorControlBase)cboClinic).Value.ToString())[0]["StoreID"];
				}
				foreach (DataRow row3 in dataTable.Rows)
				{
					DataRow dataRow3 = dtReservationProceduresSteps.NewRow();
					dataRow3["ReservationProcedureStepID"] = ++newID;
					dataRow3["ReservationID"] = (Adding ? DBNull.Value : drMaster["ReservationID"]);
					dataRow3["ReservationProcedureID"] = e.Cell.Row.Cells["ReservationProcedureID"].Value;
					dataRow3["StepID"] = row3["StepID"];
					dataRow3["PricePercentage"] = row3["PricePercentage"];
					dataRow3["Description"] = row3["Description"];
					dataRow3["ExpectedDate"] = DateTime.Parse(((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["StartDate"].Value.ToString()).AddDays(int.Parse(row3["ExpectedDelay"].ToString()));
					dataRow3["Discount"] = decimal.Parse(row3["PricePercentage"].ToString()) * decimal.Parse(((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["Discount"].Value.ToString()) / 100m;
					dataRow3["Price"] = decimal.Parse(row3["PricePercentage"].ToString()) * decimal.Parse(((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["Price"].Value.ToString()) / 100m;
					dataRow3["TaxID"] = e.Cell.Row.Cells["TaxID"].Value;
					dataRow3["TaxValue"] = decimal.Parse(row3["PricePercentage"].ToString()) * decimal.Parse(((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["TaxValue"].Value.ToString()) / 100m;
					dataRow3["NetPrice"] = decimal.Parse(row3["PricePercentage"].ToString()) * decimal.Parse(((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["NetPrice"].Value.ToString()) / 100m;
					dataRow3["IsExecuted"] = false;
					dataRow3["AutoIssueItems"] = false;
					dtReservationProceduresSteps.Rows.Add(dataRow3);
					DataTable dataTable2 = ProceduresStepsItems.SelectByProcedureStepID(row3["ProcedureStepID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
					foreach (DataRow row4 in dataTable2.Rows)
					{
						DataRow dataRow5 = dtReservationProceduresStepsItems.NewRow();
						dataRow5["ReservationProcedureStepItemID"] = ++newID;
						dataRow5["ReservationID"] = (Adding ? DBNull.Value : drMaster["ReservationID"]);
						dataRow5["ReservationProcedureID"] = e.Cell.Row.Cells["ReservationProcedureID"].Value;
						dataRow5["ReservationProcedureStepID"] = dataRow3["ReservationProcedureStepID"];
						dataRow5["ItemID"] = row4["ItemID"];
						dataRow5["ItemBarcode"] = row4["ItemID"];
						dataRow5["ColorID"] = row4["ColorID"];
						dataRow5["ItemSizeID"] = row4["ItemSizeID"];
						dataRow5["BatchID"] = row4["BatchID"];
						dataRow5["Qty"] = row4["Qty"];
						dataRow5["UnitID"] = row4["UnitID"];
						dataRow5["StoreID"] = ((obj == null) ? row4["StoreID"] : obj);
						dataRow5["Notes"] = row4["Notes"];
						dtReservationProceduresStepsItems.Rows.Add(dataRow5);
					}
				}
				InitGridProcedures();
			}
			else if (((KeyedSubObjectBase)e.Cell.Column).Key == "TaxID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
			{
				((UltraGridBase)ULGDataProcedures).UpdateData();
				CalculateProcedureRow(e.Cell.Row);
				for (int k = 0; k < ((DisposableObjectCollectionBase)ULGDataProcedures.ActiveCell.Row.ChildBands[0].Rows).Count; k++)
				{
					ULGDataProcedures.ActiveCell.Row.ChildBands[0].Rows[k].Cells["TaxID"].Value = e.Cell.Value.ToString();
					ULGDataProcedures.ActiveCell.Row.ChildBands[0].Rows[k].Cells["TaxValue"].Value = decimal.Parse(ULGDataProcedures.ActiveCell.Row.ChildBands[0].Rows[k].Cells["PricePercentage"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["TaxValue"].Value.ToString()) / 100m;
				}
			}
		}
		ULGDataProcedures.CellListSelect += new CellEventHandler(ULGDataProcedures_CellListSelect);
		ULGDataProcedures.AfterCellUpdate += new CellEventHandler(ULGDataProcedures_AfterCellUpdate);
	}

	private void CalculateProcedureRow(UltraGridRow Row)
	{
		if (Row.Cells["ReservationID"].Value == DBNull.Value || (drMaster != null && Row.Cells["ReservationID"].Value.ToString() == drMaster["ReservationID"].ToString()))
		{
			if (Row.Cells["TaxID"].Value != DBNull.Value)
			{
				Row.Cells["TaxValue"].Value = decimal.Parse(dtTaxs.Select(" TaxID= " + Row.Cells["TaxID"].Value.ToString())[0]["TaxPercent"].ToString()) / 100m * (decimal.Parse(Row.Cells["Price"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString()));
			}
			else
			{
				Row.Cells["TaxValue"].Value = 0;
			}
			Row.Cells["NetPrice"].Value = decimal.Parse(Row.Cells["Price"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString());
		}
	}

	private void ULGDataProcedures_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_07ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b6: Expected O, but got Unknown
		ULGDataProcedures.AfterCellUpdate -= new CellEventHandler(ULGDataProcedures_AfterCellUpdate);
		if (ULGDataProcedures.ActiveCell == null)
		{
			return;
		}
		if (((GridItemBase)ULGDataProcedures.ActiveCell).Band.Index == 0)
		{
			if ((((KeyedSubObjectBase)ULGDataProcedures.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGDataProcedures.ActiveCell.Column).Key == "Price") && ULGDataProcedures.ActiveCell.Value == DBNull.Value)
			{
				ULGDataProcedures.ActiveCell.Value = 0;
			}
			if (((KeyedSubObjectBase)ULGDataProcedures.ActiveCell.Column).Key == "Discount")
			{
				CalculateProcedureRow(((UltraGridBase)ULGDataProcedures).ActiveRow);
				for (int i = 0; i < ((DisposableObjectCollectionBase)ULGDataProcedures.ActiveCell.Row.ChildBands[0].Rows).Count; i++)
				{
					ULGDataProcedures.ActiveCell.Row.ChildBands[0].Rows[i].Cells["Discount"].Value = decimal.Parse(ULGDataProcedures.ActiveCell.Row.ChildBands[0].Rows[i].Cells["PricePercentage"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["Discount"].Value.ToString()) / 100m;
					ULGDataProcedures.ActiveCell.Row.ChildBands[0].Rows[i].Cells["NetPrice"].Value = decimal.Parse(ULGDataProcedures.ActiveCell.Row.ChildBands[0].Rows[i].Cells["Price"].Value.ToString()) - decimal.Parse(ULGDataProcedures.ActiveCell.Row.ChildBands[0].Rows[i].Cells["Discount"].Value.ToString());
				}
				CalculateNetTotals();
			}
			if (((KeyedSubObjectBase)ULGDataProcedures.ActiveCell.Column).Key == "Price")
			{
				CalculateProcedureRow(((UltraGridBase)ULGDataProcedures).ActiveRow);
				for (int j = 0; j < ((DisposableObjectCollectionBase)ULGDataProcedures.ActiveCell.Row.ChildBands[0].Rows).Count; j++)
				{
					ULGDataProcedures.ActiveCell.Row.ChildBands[0].Rows[j].Cells["TaxID"].Value = ((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["TaxID"].Value;
					ULGDataProcedures.ActiveCell.Row.ChildBands[0].Rows[j].Cells["Discount"].Value = decimal.Parse(ULGDataProcedures.ActiveCell.Row.ChildBands[0].Rows[j].Cells["PricePercentage"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["Discount"].Value.ToString()) / 100m;
					ULGDataProcedures.ActiveCell.Row.ChildBands[0].Rows[j].Cells["TaxValue"].Value = decimal.Parse(ULGDataProcedures.ActiveCell.Row.ChildBands[0].Rows[j].Cells["PricePercentage"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["TaxValue"].Value.ToString()) / 100m;
					ULGDataProcedures.ActiveCell.Row.ChildBands[0].Rows[j].Cells["Price"].Value = decimal.Parse(ULGDataProcedures.ActiveCell.Row.ChildBands[0].Rows[j].Cells["PricePercentage"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["Price"].Value.ToString()) / 100m;
					ULGDataProcedures.ActiveCell.Row.ChildBands[0].Rows[j].Cells["NetPrice"].Value = decimal.Parse(ULGDataProcedures.ActiveCell.Row.ChildBands[0].Rows[j].Cells["Price"].Value.ToString()) - decimal.Parse(ULGDataProcedures.ActiveCell.Row.ChildBands[0].Rows[j].Cells["Discount"].Value.ToString());
				}
				CalculateNetTotals();
			}
		}
		else if (((GridItemBase)ULGDataProcedures.ActiveCell).Band.Index == 1 && ((KeyedSubObjectBase)ULGDataProcedures.ActiveCell.Column).Key == "IsExecuted")
		{
			((UltraGridBase)ULGDataProcedures).UpdateData();
			if (dtReservationProceduresSteps.Select("IsExecuted = false And ReservationProcedureID = " + ((UltraGridBase)ULGDataProcedures).ActiveRow.Cells["ReservationProcedureID"].Value.ToString()).Length == 0)
			{
				((UltraGridBase)ULGDataProcedures).ActiveRow.ParentRow.Cells["IsClosed"].Value = true;
			}
			CalculateNetTotals();
		}
		ULGDataProcedures.AfterCellUpdate += new CellEventHandler(ULGDataProcedures_AfterCellUpdate);
	}

	private void ULGDataProcedures_AfterRowsDeleted(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		ULGDataProcedures.AfterCellUpdate -= new CellEventHandler(ULGDataProcedures_AfterCellUpdate);
		CalculateNetTotals();
		ULGDataProcedures.AfterCellUpdate += new CellEventHandler(ULGDataProcedures_AfterCellUpdate);
	}

	private void ULGDataProcedures_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		if (((GridItemBase)((UltraGridBase)ULGDataProcedures).ActiveRow).Band.Index > 0)
		{
			((CancelEventArgs)(object)e).Cancel = true;
			return;
		}
		for (int i = 0; i < e.Rows.Length; i++)
		{
			if (bool.Parse(e.Rows[i].Cells["IsClosed"].Value.ToString()))
			{
				((CancelEventArgs)(object)e).Cancel = true;
				continue;
			}
			if (e.Rows[i].Cells["ReservationID"].Value != DBNull.Value && e.Rows[i].Cells["ReservationID"].Value.ToString() != drMaster["ReservationID"].ToString())
			{
				((CancelEventArgs)(object)e).Cancel = true;
				continue;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)e.Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (bool.Parse(e.Rows[i].ChildBands[0].Rows[j].Cells["IsExecuted"].Value.ToString()))
				{
					((CancelEventArgs)(object)e).Cancel = true;
					break;
				}
			}
		}
	}

	private void txtFeesDiscValue_ValueChanged(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			return;
		}
		if (decimal.Parse((((Control)(object)txtFeesAmount).Text == "" || ((Control)(object)txtFeesAmount).Text == ".") ? "0" : ((Control)(object)txtFeesAmount).Text) > 0m)
		{
			((TextEditorControlBase)txtFeesDiscRatio).ValueChanged -= txtFeesDiscRatio_ValueChanged;
			((TextEditorControlBase)txtFeesDiscValue).ValueChanged -= txtFeesDiscValue_ValueChanged;
			((Control)(object)txtFeesDiscRatio).Text = decimal.Parse((decimal.Parse((((Control)(object)txtFeesDiscValue).Text == "" || ((Control)(object)txtFeesDiscValue).Text == "0" || ((Control)(object)txtFeesDiscValue).Text == ".") ? "0" : ((Control)(object)txtFeesDiscValue).Text) / decimal.Parse((((Control)(object)txtFeesAmount).Text == "" || ((Control)(object)txtFeesAmount).Text == "0" || ((Control)(object)txtFeesAmount).Text == ".") ? "0" : ((Control)(object)txtFeesAmount).Text) * 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if ((cboPatients.SelectedIndex > -1 && decimal.Parse((((Control)(object)txtFeesDiscRatio).Text == "") ? "0" : ((Control)(object)txtFeesDiscRatio).Text) > decimal.Parse(dtPatients.Select(" PatientID= " + ((TextEditorControlBase)cboPatients).Value)[0]["DiscountPercentage"].ToString()) && decimal.Parse((((Control)(object)txtFeesDiscRatio).Text == "") ? "0" : ((Control)(object)txtFeesDiscRatio).Text) > UserSalesDiscount) || (cboPatients.SelectedIndex == -1 && decimal.Parse((((Control)(object)txtFeesDiscRatio).Text == "") ? "0" : ((Control)(object)txtFeesDiscRatio).Text) > UserSalesDiscount))
			{
				OpenChangeDiscountFormForFees();
			}
			CalculateTotalsTax();
			((TextEditorControlBase)txtFeesDiscRatio).ValueChanged += txtFeesDiscRatio_ValueChanged;
			((TextEditorControlBase)txtFeesDiscValue).ValueChanged += txtFeesDiscValue_ValueChanged;
		}
		else
		{
			((TextEditorControlBase)txtFeesDiscRatio).ValueChanged -= txtFeesDiscRatio_ValueChanged;
			((TextEditorControlBase)txtFeesDiscValue).ValueChanged -= txtFeesDiscValue_ValueChanged;
			((Control)(object)txtFeesDiscRatio).Text = "0";
			((Control)(object)txtFeesDiscValue).Text = "0";
			((TextEditorControlBase)txtFeesDiscRatio).ValueChanged += txtFeesDiscRatio_ValueChanged;
			((TextEditorControlBase)txtFeesDiscValue).ValueChanged += txtFeesDiscValue_ValueChanged;
		}
		CalculateNetFees();
	}

	private void txtFeesDiscRatio_ValueChanged(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			return;
		}
		if (decimal.Parse((((Control)(object)txtFeesAmount).Text == "" || ((Control)(object)txtFeesAmount).Text == ".") ? "0" : ((Control)(object)txtFeesAmount).Text) > 0m)
		{
			((TextEditorControlBase)txtFeesDiscValue).ValueChanged -= txtFeesDiscValue_ValueChanged;
			((TextEditorControlBase)txtFeesDiscRatio).ValueChanged -= txtFeesDiscRatio_ValueChanged;
			((Control)(object)txtFeesDiscValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtFeesDiscRatio).Text == "" || ((Control)(object)txtFeesDiscRatio).Text == ".") ? "0" : ((Control)(object)txtFeesDiscRatio).Text) / 100m * decimal.Parse((((Control)(object)txtFeesAmount).Text == "" || ((Control)(object)txtFeesAmount).Text == ".") ? "0" : ((Control)(object)txtFeesAmount).Text), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if ((cboPatients.SelectedIndex > -1 && decimal.Parse((((Control)(object)txtFeesDiscRatio).Text == "" || ((Control)(object)txtFeesDiscRatio).Text == ".") ? "0" : ((Control)(object)txtFeesDiscRatio).Text) > decimal.Parse(dtPatients.Select(" PatientID = " + ((TextEditorControlBase)cboPatients).Value)[0]["DiscountPercentage"].ToString()) && decimal.Parse((((Control)(object)txtFeesDiscRatio).Text == "" || ((Control)(object)txtFeesDiscRatio).Text == ".") ? "0" : ((Control)(object)txtFeesDiscRatio).Text) > UserSalesDiscount) || (cboPatients.SelectedIndex == -1 && decimal.Parse((((Control)(object)txtFeesDiscRatio).Text == "" || ((Control)(object)txtFeesDiscRatio).Text == ".") ? "0" : ((Control)(object)txtFeesDiscRatio).Text) > UserSalesDiscount))
			{
				OpenChangeDiscountFormForFees();
			}
			((TextEditorControlBase)txtFeesDiscRatio).ValueChanged += txtFeesDiscRatio_ValueChanged;
			((TextEditorControlBase)txtFeesDiscValue).ValueChanged += txtFeesDiscValue_ValueChanged;
		}
		else
		{
			((TextEditorControlBase)txtFeesDiscRatio).ValueChanged -= txtFeesDiscRatio_ValueChanged;
			((TextEditorControlBase)txtFeesDiscValue).ValueChanged -= txtFeesDiscValue_ValueChanged;
			((Control)(object)txtFeesDiscRatio).Text = "0";
			((Control)(object)txtFeesDiscValue).Text = "0";
			((TextEditorControlBase)txtFeesDiscRatio).ValueChanged += txtFeesDiscRatio_ValueChanged;
			((TextEditorControlBase)txtFeesDiscValue).ValueChanged += txtFeesDiscValue_ValueChanged;
		}
		CalculateNetFees();
	}

	private void btnPatientHistory_Click(object sender, EventArgs e)
	{
		if (cboPatients.SelectedIndex > -1)
		{
			int patientID = int.Parse(((TextEditorControlBase)cboPatients).Value.ToString());
			frmReservationsInquiry frmReservationsInquiry2 = new frmReservationsInquiry(patientID, ClinicID, DoctorID);
			frmReservationsInquiry2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
			((Control)(object)frmReservationsInquiry2.lblTitle).Text = (GlobalVariables.IsArabic ? "استعلام" : "Inquiry");
			frmReservationsInquiry2.Location = new Point(0, 0);
			frmReservationsInquiry2.ShowDialog();
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
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Expected O, but got Unknown
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Expected O, but got Unknown
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Expected O, but got Unknown
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Expected O, but got Unknown
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Expected O, but got Unknown
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Expected O, but got Unknown
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Expected O, but got Unknown
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Expected O, but got Unknown
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Expected O, but got Unknown
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Expected O, but got Unknown
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Expected O, but got Unknown
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Expected O, but got Unknown
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Expected O, but got Unknown
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Expected O, but got Unknown
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Expected O, but got Unknown
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Expected O, but got Unknown
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Expected O, but got Unknown
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Expected O, but got Unknown
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Expected O, but got Unknown
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Expected O, but got Unknown
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Expected O, but got Unknown
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Expected O, but got Unknown
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Expected O, but got Unknown
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Expected O, but got Unknown
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Expected O, but got Unknown
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Expected O, but got Unknown
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Expected O, but got Unknown
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Expected O, but got Unknown
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Expected O, but got Unknown
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Expected O, but got Unknown
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Expected O, but got Unknown
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Expected O, but got Unknown
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Expected O, but got Unknown
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Expected O, but got Unknown
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Expected O, but got Unknown
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Expected O, but got Unknown
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Expected O, but got Unknown
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Expected O, but got Unknown
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Expected O, but got Unknown
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Expected O, but got Unknown
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Expected O, but got Unknown
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Expected O, but got Unknown
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Expected O, but got Unknown
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Expected O, but got Unknown
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Expected O, but got Unknown
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Expected O, but got Unknown
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Expected O, but got Unknown
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Expected O, but got Unknown
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Expected O, but got Unknown
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Expected O, but got Unknown
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Expected O, but got Unknown
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Expected O, but got Unknown
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Expected O, but got Unknown
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Expected O, but got Unknown
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Expected O, but got Unknown
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Expected O, but got Unknown
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Expected O, but got Unknown
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Expected O, but got Unknown
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Expected O, but got Unknown
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Expected O, but got Unknown
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Expected O, but got Unknown
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Expected O, but got Unknown
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Expected O, but got Unknown
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Expected O, but got Unknown
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Expected O, but got Unknown
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Expected O, but got Unknown
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Expected O, but got Unknown
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Expected O, but got Unknown
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Expected O, but got Unknown
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Expected O, but got Unknown
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Expected O, but got Unknown
		//IL_0b02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0c: Expected O, but got Unknown
		//IL_0b4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b54: Expected O, but got Unknown
		//IL_113c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1146: Expected O, but got Unknown
		//IL_1484: Unknown result type (might be due to invalid IL or missing references)
		//IL_148e: Expected O, but got Unknown
		//IL_1875: Unknown result type (might be due to invalid IL or missing references)
		//IL_187f: Expected O, but got Unknown
		//IL_18bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_18c7: Expected O, but got Unknown
		//IL_18d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_18df: Expected O, but got Unknown
		//IL_18ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f7: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Clinics.Transactions.frmReservations));
		UltraTab val = new UltraTab();
		UltraTab val2 = new UltraTab();
		UltraTab val3 = new UltraTab();
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
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGDataPayments = new UltraGrid();
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.ULGAccruals = new UltraGrid();
		this.ultraTabPageControl4 = new UltraTabPageControl();
		this.ULGDataProcedures = new UltraGrid();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboClinic = new UltraComboEditor();
		this.lblClinics = new UltraLabel();
		this.lblPatientName = new UltraLabel();
		this.cboPatients = new UltraComboEditor();
		this.txtOrderNo = new UltraTextEditor();
		this.lblOrderNo = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.btnPatientSearch = new UltraButton();
		this.lblDoctor = new UltraLabel();
		this.cboDoctor = new UltraComboEditor();
		this.lblAppointmentDate = new UltraLabel();
		this.dtpAppointmentDate = new UltraDateTimeEditor();
		this.txtFees = new UltraTextEditor();
		this.chkConfirmed = new UltraCheckEditor();
		this.chkCanceled = new UltraCheckEditor();
		this.chkArrived = new UltraCheckEditor();
		this.dtpArrivalDate = new UltraDateTimeEditor();
		this.chkClosed = new UltraCheckEditor();
		this.btnPatientAdd = new UltraButton();
		this.lblPaidAmount = new UltraLabel();
		this.txtPaidAmount = new UltraTextEditor();
		this.chkVisa = new UltraCheckEditor();
		this.txtVisaNo = new UltraTextEditor();
		this.lblVisaNo = new UltraLabel();
		this.cboVisaType = new UltraComboEditor();
		this.btnOrderPayments = new UltraButton();
		this.lblRestAmount = new UltraLabel();
		this.txtRestAmount = new UltraTextEditor();
		this.lblNetPrice = new UltraLabel();
		this.txtNetprice = new UltraTextEditor();
		this.lblTaxTotalValue = new UltraLabel();
		this.txtTaxTotalValue = new UltraTextEditor();
		this.lblDiscBeforeTaxRatio = new UltraLabel();
		this.txtDiscBeforeTaxRatio = new UltraTextEditor();
		this.lblDiscBeforeTaxValue = new UltraLabel();
		this.txtDiscBeforeTaxValue = new UltraTextEditor();
		this.lblGrossValue = new UltraLabel();
		this.txtGrossValue = new UltraTextEditor();
		this.lblCommercialTax = new UltraLabel();
		this.txtCommercialTax = new UltraTextEditor();
		this.chkStore = new UltraCheckEditor();
		this.cboStore = new UltraComboEditor();
		this.cboTax = new UltraComboEditor();
		this.chkTax = new UltraCheckEditor();
		this.btnPriceTypeSearch = new UltraButton();
		this.lblPriceType = new UltraLabel();
		this.cboPriceType = new UltraComboEditor();
		this.txtTotalQty = new UltraTextEditor();
		this.ultraLabel1 = new UltraLabel();
		this.lblType = new UltraLabel();
		this.cboReservationType = new UltraComboEditor();
		this.chkIsExtended = new UltraCheckEditor();
		this.lblBalance = new UltraLabel();
		this.txtBranchBalance = new UltraTextEditor();
		this.txtFeesDiscValue = new UltraTextEditor();
		this.txtFeesDiscRatio = new UltraTextEditor();
		this.ultraLabel2 = new UltraLabel();
		this.ultraLabel3 = new UltraLabel();
		this.ultraLabel4 = new UltraLabel();
		this.txtFeesAmount = new UltraTextEditor();
		this.chkIsOnAccount = new UltraCheckEditor();
		this.btnPatientHistory = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataPayments).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGAccruals).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataProcedures).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClinic).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPatients).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOrderNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDoctor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpAppointmentDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFees).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkConfirmed).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkCanceled).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkArrived).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpArrivalDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkClosed).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaidAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkVisa).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRestAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCommercialTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboReservationType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsExtended).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBranchBalance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFeesDiscValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFeesDiscRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFeesAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsOnAccount).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl4);
		((UltraTabControlBase)base.UTCDetails).TabOrientation = (TabOrientation)1;
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		((KeyedSubObjectBase)val).Key = "Payment";
		val.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val, "ultraTab1");
		((SubObjectBase)val).ForceApplyResources = "";
		((KeyedSubObjectBase)val2).Key = "Accruals";
		val2.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val2, "ultraTab2");
		val2.Visible = false;
		((SubObjectBase)val2).ForceApplyResources = "";
		((KeyedSubObjectBase)val3).Key = "Procedures";
		val3.TabPage = this.ultraTabPageControl4;
		resources.ApplyResources(val3, "ultraTab3");
		((SubObjectBase)val3).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[3] { val, val2, val3 });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl4, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl3, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		resources.ApplyResources(base.ULGData, "ULGData");
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val4, "appearance21");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val5, "appearance22");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val6, "appearance23");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val6;
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val7, "appearance24");
		((AppearanceBase)val7).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val8).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val8).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val8).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val8, "appearance25");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val9).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val9, "appearance26");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val10, "appearance27");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val10;
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.AfterExitEditMode += new System.EventHandler(ULGData_AfterExitEditMode);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		resources.ApplyResources(base.btnSetting, "btnSetting");
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val11).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val11).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val11).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val11).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val11).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val11, "appearance28");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val11;
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
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val12, "appearance1");
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val13).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val13).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val13, "appearance2");
		((AppearanceBase)val13).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val14).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val14, "appearance3");
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val14;
		((AppearanceBase)val15).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val15).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val15).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val15, "appearance4");
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val16).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val16).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val16).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val16, "appearance5");
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val16;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataPayments).Name = "ULGDataPayments";
		((UltraControlBase)this.ULGDataPayments).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataPayments.AfterEnterEditMode += new System.EventHandler(ULGDataPayments_AfterEnterEditMode);
		this.ULGDataPayments.ClickCellButton += new CellEventHandler(ULGDataPayments_ClickCellButton);
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ULGAccruals);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		resources.ApplyResources(this.ULGAccruals, "ULGAccruals");
		((UltraGridBase)this.ULGAccruals).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGAccruals).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGAccruals).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val17, "appearance6");
		((UltraGridBase)this.ULGAccruals).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val17;
		((UltraGridBase)this.ULGAccruals).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val18).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val18).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val18).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val18, "appearance7");
		((AppearanceBase)val18).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGAccruals).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val18;
		((UltraGridBase)this.ULGAccruals).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val19).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val19, "appearance8");
		((UltraGridBase)this.ULGAccruals).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val19;
		((AppearanceBase)val20).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val20).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val20).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val20, "appearance9");
		((UltraGridBase)this.ULGAccruals).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val20;
		((UltraGridBase)this.ULGAccruals).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGAccruals).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val21).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val21).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val21).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val21).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val21, "appearance10");
		((UltraGridBase)this.ULGAccruals).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val21;
		((UltraGridBase)this.ULGAccruals).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGAccruals).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGAccruals).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGAccruals).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGAccruals).Name = "ULGAccruals";
		((UltraControlBase)this.ULGAccruals).UseFlatMode = (DefaultableBoolean)1;
		this.ULGAccruals.AfterEnterEditMode += new System.EventHandler(ULGAccruals_AfterEnterEditMode);
		this.ULGAccruals.ClickCellButton += new CellEventHandler(ULGAccruals_ClickCellButton);
		resources.ApplyResources(this.ultraTabPageControl4, "ultraTabPageControl4");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataProcedures);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Name = "ultraTabPageControl4";
		resources.ApplyResources(this.ULGDataProcedures, "ULGDataProcedures");
		((AppearanceBase)val22).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val22).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val22).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val22).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val22, "appearance11");
		((SpecialBoxBase)((UltraGridBase)this.ULGDataProcedures).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val22;
		((AppearanceBase)val23).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val23, "appearance12");
		((UltraGridBase)this.ULGDataProcedures).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val23;
		((SpecialBoxBase)((UltraGridBase)this.ULGDataProcedures).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val24).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val24).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val24).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val24).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val24, "appearance13");
		((UltraGridBase)this.ULGDataProcedures).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val24;
		((UltraGridBase)this.ULGDataProcedures).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataProcedures).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val25).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val25).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val25, "appearance14");
		((UltraGridBase)this.ULGDataProcedures).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val25;
		((AppearanceBase)val26).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val26).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val26, "appearance15");
		((UltraGridBase)this.ULGDataProcedures).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val26;
		((UltraGridBase)this.ULGDataProcedures).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGDataProcedures).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val27).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val27, "appearance16");
		((UltraGridBase)this.ULGDataProcedures).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val27;
		((AppearanceBase)val28).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val28, "appearance17");
		((AppearanceBase)val28).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGDataProcedures).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val28;
		((UltraGridBase)this.ULGDataProcedures).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGDataProcedures).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val29).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val29).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val29).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val29).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val29).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val29, "appearance18");
		((UltraGridBase)this.ULGDataProcedures).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val29;
		((UltraGridBase)this.ULGDataProcedures).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGDataProcedures).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val30).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val30).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val30, "appearance19");
		((UltraGridBase)this.ULGDataProcedures).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val30;
		((UltraGridBase)this.ULGDataProcedures).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val31).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val31, "appearance20");
		((UltraGridBase)this.ULGDataProcedures).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val31;
		((System.Windows.Forms.Control)(object)this.ULGDataProcedures).Name = "ULGDataProcedures";
		this.ULGDataProcedures.AfterCellUpdate += new CellEventHandler(ULGDataProcedures_AfterCellUpdate);
		this.ULGDataProcedures.AfterEnterEditMode += new System.EventHandler(ULGDataProcedures_AfterEnterEditMode);
		this.ULGDataProcedures.AfterRowsDeleted += new System.EventHandler(ULGDataProcedures_AfterRowsDeleted);
		this.ULGDataProcedures.AfterRowInsert += new RowEventHandler(ULGDataProcedures_AfterRowInsert);
		this.ULGDataProcedures.CellListSelect += new CellEventHandler(ULGDataProcedures_CellListSelect);
		this.ULGDataProcedures.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGDataProcedures_BeforeRowsDeleted);
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.cboClinic, "cboClinic");
		((TextEditorControlBase)this.cboClinic).AlwaysInEditMode = true;
		this.cboClinic.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClinic).Name = "cboClinic";
		resources.ApplyResources(this.lblClinics, "lblClinics");
		this.lblClinics.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClinics).Name = "lblClinics";
		((ControlBase)this.lblClinics).WrapText = false;
		resources.ApplyResources(this.lblPatientName, "lblPatientName");
		this.lblPatientName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPatientName).Name = "lblPatientName";
		((ControlBase)this.lblPatientName).WrapText = false;
		resources.ApplyResources(this.cboPatients, "cboPatients");
		((TextEditorControlBase)this.cboPatients).AlwaysInEditMode = true;
		this.cboPatients.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboPatients).Name = "cboPatients";
		((TextEditorControlBase)this.cboPatients).ValueChanged += new System.EventHandler(cboPatients_ValueChanged);
		resources.ApplyResources(this.txtOrderNo, "txtOrderNo");
		((System.Windows.Forms.Control)(object)this.txtOrderNo).Name = "txtOrderNo";
		resources.ApplyResources(this.lblOrderNo, "lblOrderNo");
		this.lblOrderNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOrderNo).Name = "lblOrderNo";
		((ControlBase)this.lblOrderNo).WrapText = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.btnPatientSearch, "btnPatientSearch");
		((AppearanceBase)val32).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val32, "appearance29");
		((ControlBase)this.btnPatientSearch).Appearance = (AppearanceBase)(object)val32;
		((System.Windows.Forms.Control)(object)this.btnPatientSearch).Name = "btnPatientSearch";
		((System.Windows.Forms.Control)(object)this.btnPatientSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnPatientSearch).Click += new System.EventHandler(btnPatientSearch_Click);
		resources.ApplyResources(this.lblDoctor, "lblDoctor");
		this.lblDoctor.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDoctor).Name = "lblDoctor";
		((ControlBase)this.lblDoctor).WrapText = false;
		resources.ApplyResources(this.cboDoctor, "cboDoctor");
		((TextEditorControlBase)this.cboDoctor).AlwaysInEditMode = true;
		this.cboDoctor.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboDoctor).Name = "cboDoctor";
		resources.ApplyResources(this.lblAppointmentDate, "lblAppointmentDate");
		this.lblAppointmentDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAppointmentDate).Name = "lblAppointmentDate";
		((ControlBase)this.lblAppointmentDate).WrapText = false;
		resources.ApplyResources(this.dtpAppointmentDate, "dtpAppointmentDate");
		((UltraWinEditorMaskedControlBase)this.dtpAppointmentDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpAppointmentDate).Name = "dtpAppointmentDate";
		resources.ApplyResources(this.txtFees, "txtFees");
		((System.Windows.Forms.Control)(object)this.txtFees).Name = "txtFees";
		((EditorButtonControlBase)this.txtFees).ReadOnly = true;
		resources.ApplyResources(this.chkConfirmed, "chkConfirmed");
		((System.Windows.Forms.Control)(object)this.chkConfirmed).Name = "chkConfirmed";
		resources.ApplyResources(this.chkCanceled, "chkCanceled");
		((System.Windows.Forms.Control)(object)this.chkCanceled).Name = "chkCanceled";
		resources.ApplyResources(this.chkArrived, "chkArrived");
		((System.Windows.Forms.Control)(object)this.chkArrived).Name = "chkArrived";
		((UltraToggleEditorBase)this.chkArrived).CheckedChanged += new System.EventHandler(chkArrived_CheckedChanged);
		resources.ApplyResources(this.dtpArrivalDate, "dtpArrivalDate");
		((UltraWinEditorMaskedControlBase)this.dtpArrivalDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpArrivalDate).Name = "dtpArrivalDate";
		resources.ApplyResources(this.chkClosed, "chkClosed");
		((System.Windows.Forms.Control)(object)this.chkClosed).Name = "chkClosed";
		resources.ApplyResources(this.btnPatientAdd, "btnPatientAdd");
		((AppearanceBase)val33).Image = ERP.Properties.Resources.New;
		resources.ApplyResources(val33, "appearance30");
		((ControlBase)this.btnPatientAdd).Appearance = (AppearanceBase)(object)val33;
		((System.Windows.Forms.Control)(object)this.btnPatientAdd).Name = "btnPatientAdd";
		((System.Windows.Forms.Control)(object)this.btnPatientAdd).Click += new System.EventHandler(btnPatientAdd_Click);
		resources.ApplyResources(this.lblPaidAmount, "lblPaidAmount");
		this.lblPaidAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPaidAmount).Name = "lblPaidAmount";
		((ControlBase)this.lblPaidAmount).WrapText = false;
		resources.ApplyResources(this.txtPaidAmount, "txtPaidAmount");
		((System.Windows.Forms.Control)(object)this.txtPaidAmount).Name = "txtPaidAmount";
		((TextEditorControlBase)this.txtPaidAmount).ValueChanged += new System.EventHandler(txtPaidAmount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtPaidAmount).Enter += new System.EventHandler(textBox_Enter);
		((System.Windows.Forms.Control)(object)this.txtPaidAmount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
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
		resources.ApplyResources(this.btnOrderPayments, "btnOrderPayments");
		((System.Windows.Forms.Control)(object)this.btnOrderPayments).Name = "btnOrderPayments";
		((System.Windows.Forms.Control)(object)this.btnOrderPayments).Click += new System.EventHandler(btnOrderPayments_Click);
		resources.ApplyResources(this.lblRestAmount, "lblRestAmount");
		this.lblRestAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRestAmount).Name = "lblRestAmount";
		((ControlBase)this.lblRestAmount).WrapText = false;
		((System.Windows.Forms.Control)(object)this.lblRestAmount).Click += new System.EventHandler(lblRestAmount_Click);
		resources.ApplyResources(this.txtRestAmount, "txtRestAmount");
		((System.Windows.Forms.Control)(object)this.txtRestAmount).Name = "txtRestAmount";
		((EditorButtonControlBase)this.txtRestAmount).ReadOnly = true;
		((TextEditorControlBase)this.txtRestAmount).ValueChanged += new System.EventHandler(txtRestAmount_ValueChanged);
		resources.ApplyResources(this.lblNetPrice, "lblNetPrice");
		this.lblNetPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNetPrice).Name = "lblNetPrice";
		((ControlBase)this.lblNetPrice).WrapText = false;
		resources.ApplyResources(this.txtNetprice, "txtNetprice");
		((System.Windows.Forms.Control)(object)this.txtNetprice).Name = "txtNetprice";
		((EditorButtonControlBase)this.txtNetprice).ReadOnly = true;
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
		resources.ApplyResources(this.lblCommercialTax, "lblCommercialTax");
		this.lblCommercialTax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCommercialTax).Name = "lblCommercialTax";
		((ControlBase)this.lblCommercialTax).WrapText = false;
		resources.ApplyResources(this.txtCommercialTax, "txtCommercialTax");
		((System.Windows.Forms.Control)(object)this.txtCommercialTax).Name = "txtCommercialTax";
		((TextEditorControlBase)this.txtCommercialTax).ValueChanged += new System.EventHandler(txtCommercialTax_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtCommercialTax).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.chkStore, "chkStore");
		((System.Windows.Forms.Control)(object)this.chkStore).Name = "chkStore";
		((UltraToggleEditorBase)this.chkStore).CheckedChanged += new System.EventHandler(chkStore_CheckedChanged);
		resources.ApplyResources(this.cboStore, "cboStore");
		((TextEditorControlBase)this.cboStore).AlwaysInEditMode = true;
		this.cboStore.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboStore).Name = "cboStore";
		((EditorButtonControlBase)this.cboStore).ReadOnly = true;
		((TextEditorControlBase)this.cboStore).ValueChanged += new System.EventHandler(cboStore_ValueChanged);
		resources.ApplyResources(this.cboTax, "cboTax");
		((TextEditorControlBase)this.cboTax).AlwaysInEditMode = true;
		this.cboTax.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboTax).Name = "cboTax";
		((EditorButtonControlBase)this.cboTax).ReadOnly = true;
		((TextEditorControlBase)this.cboTax).ValueChanged += new System.EventHandler(cboTax_ValueChanged);
		resources.ApplyResources(this.chkTax, "chkTax");
		((System.Windows.Forms.Control)(object)this.chkTax).Name = "chkTax";
		((UltraToggleEditorBase)this.chkTax).CheckedChanged += new System.EventHandler(chkTax_CheckedChanged);
		resources.ApplyResources(this.btnPriceTypeSearch, "btnPriceTypeSearch");
		((AppearanceBase)val34).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val34, "appearance31");
		((ControlBase)this.btnPriceTypeSearch).Appearance = (AppearanceBase)(object)val34;
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
		resources.ApplyResources(this.txtTotalQty, "txtTotalQty");
		((System.Windows.Forms.Control)(object)this.txtTotalQty).Name = "txtTotalQty";
		((EditorButtonControlBase)this.txtTotalQty).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtTotalQty).TabStop = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.lblType, "lblType");
		this.lblType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblType).Name = "lblType";
		((ControlBase)this.lblType).WrapText = false;
		resources.ApplyResources(this.cboReservationType, "cboReservationType");
		((TextEditorControlBase)this.cboReservationType).AlwaysInEditMode = true;
		this.cboReservationType.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboReservationType).Name = "cboReservationType";
		((TextEditorControlBase)this.cboReservationType).ValueChanged += new System.EventHandler(cboReservationType_ValueChanged);
		resources.ApplyResources(this.chkIsExtended, "chkIsExtended");
		((System.Windows.Forms.Control)(object)this.chkIsExtended).Name = "chkIsExtended";
		resources.ApplyResources(this.lblBalance, "lblBalance");
		this.lblBalance.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBalance).Name = "lblBalance";
		((ControlBase)this.lblBalance).WrapText = false;
		resources.ApplyResources(this.txtBranchBalance, "txtBranchBalance");
		((System.Windows.Forms.Control)(object)this.txtBranchBalance).Name = "txtBranchBalance";
		((EditorButtonControlBase)this.txtBranchBalance).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtBranchBalance).TabStop = false;
		resources.ApplyResources(this.txtFeesDiscValue, "txtFeesDiscValue");
		((System.Windows.Forms.Control)(object)this.txtFeesDiscValue).Name = "txtFeesDiscValue";
		((TextEditorControlBase)this.txtFeesDiscValue).ValueChanged += new System.EventHandler(txtFeesDiscValue_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtFeesDiscValue).Enter += new System.EventHandler(textBox_Enter);
		((System.Windows.Forms.Control)(object)this.txtFeesDiscValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.txtFeesDiscRatio, "txtFeesDiscRatio");
		((System.Windows.Forms.Control)(object)this.txtFeesDiscRatio).Name = "txtFeesDiscRatio";
		((TextEditorControlBase)this.txtFeesDiscRatio).ValueChanged += new System.EventHandler(txtFeesDiscRatio_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtFeesDiscRatio).Enter += new System.EventHandler(textBox_Enter);
		((System.Windows.Forms.Control)(object)this.txtFeesDiscRatio).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		this.ultraLabel4.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		((ControlBase)this.ultraLabel4).WrapText = false;
		resources.ApplyResources(this.txtFeesAmount, "txtFeesAmount");
		((System.Windows.Forms.Control)(object)this.txtFeesAmount).Name = "txtFeesAmount";
		resources.ApplyResources(this.chkIsOnAccount, "chkIsOnAccount");
		((System.Windows.Forms.Control)(object)this.chkIsOnAccount).Name = "chkIsOnAccount";
		resources.ApplyResources(this.btnPatientHistory, "btnPatientHistory");
		((AppearanceBase)val35).Image = ERP.Properties.Resources.Update;
		resources.ApplyResources(val35, "appearance32");
		((ControlBase)this.btnPatientHistory).Appearance = (AppearanceBase)(object)val35;
		((System.Windows.Forms.Control)(object)this.btnPatientHistory).Name = "btnPatientHistory";
		((System.Windows.Forms.Control)(object)this.btnPatientHistory).Click += new System.EventHandler(btnPatientHistory_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBranchBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboReservationType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCommercialTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCommercialTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRestAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRestAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNetPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNetprice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtFeesDiscRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtFeesDiscValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOrderPayments);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkVisa);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVisaNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboVisaType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPaidAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPaidAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPatientHistory);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPatientAdd);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkClosed);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpArrivalDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkArrived);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkCanceled);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsOnAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsExtended);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkConfirmed);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtFeesAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtFees);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAppointmentDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpAppointmentDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDoctor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboDoctor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPatientSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtOrderNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPatientName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPatients);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOrderNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClinics);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClinic);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmReservations";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UTCDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClinic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClinics, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOrderNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPatients, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPatientName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtOrderNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPatientSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboDoctor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDoctor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpAppointmentDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAppointmentDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtFees, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtFeesAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkConfirmed, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsExtended, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsOnAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkCanceled, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkArrived, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpArrivalDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkClosed, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPatientAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPatientHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPaidAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPaidAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboVisaType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVisaNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkVisa, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOrderPayments, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtFeesDiscValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtFeesDiscRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNetprice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNetPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel4, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRestAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRestAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCommercialTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCommercialTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboReservationType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBranchBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataPayments).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGAccruals).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataProcedures).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClinic).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPatients).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOrderNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDoctor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpAppointmentDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFees).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkConfirmed).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkCanceled).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkArrived).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpArrivalDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkClosed).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaidAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkVisa).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRestAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCommercialTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboReservationType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsExtended).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBranchBalance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFeesDiscValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFeesDiscRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFeesAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsOnAccount).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
