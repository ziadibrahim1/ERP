using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Constructions;
using BusinessLayer.Security;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Constructions.Transactions;

public class frmContracts : frmHeaderDetails
{
	private DataTable dtClients;

	private DataTable dtProjects;

	private DataTable dtBuildings;

	private DataTable dtUnits;

	private DataTable dtFloors;

	private DataTable dtSamples;

	private DataTable dtFinishingTypes;

	private DataTable dtInstallmentsTypes;

	private ValueList vlInstallmentsTypes = new ValueList();

	private DataRow DrUnit;

	private bool IsGenerateContractTotalJV = false;

	private IContainer components = null;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblInstallmentCount;

	private UltraTextEditor txtInstallmentCount;

	private UltraComboEditor cboClient;

	private UltraLabel lblClient;

	private UltraLabel lblInstallmentPeriod;

	private UltraTextEditor txtTotalInstallment;

	private UltraLabel lblTotalInstallment;

	public UltraButton btnClientSearch;

	private UltraLabel lblBuildings;

	private UltraComboEditor cboBuildings;

	private UltraLabel lblProjects;

	private UltraComboEditor cboProject;

	private UltraLabel lblFloor;

	private UltraLabel lblUnit;

	private UltraLabel lblSample;

	private UltraTextEditor txtSize;

	private UltraLabel lblSize;

	private UltraLabel lblFinishingType;

	private UltraComboEditor cboFinishingType;

	private UltraTextEditor txtUnitPrice;

	private UltraLabel lblUnitPrice;

	private UltraTextEditor txtSalesPrice;

	private UltraLabel lblSalesPrice;

	private UltraTextEditor txtDiscountPercentage;

	private UltraLabel lblDiscountPercentage;

	private UltraTextEditor txtReservationInstallment;

	private UltraLabel lblReservationInstallment;

	private UltraLabel lblReservationInstallmentDate;

	private UltraDateTimeEditor dtpReservationInstallmentDate;

	private UltraLabel lblContractInstallmentDate;

	private UltraDateTimeEditor dtpContractInstallmentDate;

	private UltraTextEditor txtContractInstallment;

	private UltraLabel lblContractInstallment;

	private UltraLabel lblMaintenanceInstallmentDate;

	private UltraDateTimeEditor dtpMaintenanceInstallmentDate;

	private UltraTextEditor txtTotalRestAmount;

	private UltraLabel lblTotalRestAmount;

	private UltraTextEditor txtRestAmount;

	private UltraLabel lblRestAmount;

	private UltraTextEditor txtMaintenanceInstallment;

	private UltraLabel lblMaintenanceInstallment;

	private UltraTextEditor txtInstallmentPeriod;

	private UltraComboEditor cboFloor;

	private UltraComboEditor cboSample;

	private UltraComboEditor cboUnit;

	private UltraLabel lblSizeMeter;

	private UltraDateTimeEditor dtpFinishingStartDate;

	private UltraLabel lblFinishingStartDate;

	private UltraLabel lblFinishingAmount;

	private UltraTextEditor txtFinishingAmount;

	private UltraLabel lblFinishingInstallmentCount;

	private UltraTextEditor txtFinishingInstallmentCount;

	private UltraLabel lblFinishingInstallmentPeriod;

	private UltraTextEditor txtFinishingInstallmentPeriod;

	private UltraLabel lblRoundingValue;

	private UltraTextEditor txtRoundingValue;

	private UltraCheckEditor chkIsCanceled;

	private UltraDateTimeEditor dtpCanceledDate;

	private UltraLabel lblCancelationDate;

	public frmContracts()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		TableName = "Con_Contracts";
		IDCol = "ContractID";
		NoCol = "ContractCode";
		DateCol = "ContractDate";
	}

	public frmContracts(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public frmContracts(DataRow drUnit)
		: this()
	{
		DrUnit = drUnit;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		IsGenerateContractTotalJV = GlobalFunctions.GetOption("GenerateContractTotalJVInRealState");
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		dtProjects = Projects.FillCombo("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboProject, dtProjects, "ProjectID", "ProjectName");
		dtBuildings = Buildings.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBuildings, dtBuildings, "BuildingID", "BuildingName");
		dtUnits = BuildingsUnits.FillCombo((DrUnit == null) ? "-1" : DrUnit["BuildingID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboUnit, dtUnits, "BuildingUnitID", "BuildingUnitCode");
		dtFloors = Floors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboFloor, dtFloors, "FloorID", "FloorName");
		dtSamples = BuildingsSamples.FillCombo((DrUnit == null) ? "-1" : DrUnit["BuildingID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSample, dtSamples, "BuildingSampleID", "BuildingSampleCode");
		dtFinishingTypes = FinishingTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboFinishingType, dtFinishingTypes, "FinishingTypeID", "FinishingName");
		dtInstallmentsTypes = InstallmentsTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInstallmentsTypes.ValueListItems.Clear();
		for (int i = 0; i < dtInstallmentsTypes.Rows.Count; i++)
		{
			vlInstallmentsTypes.ValueListItems.Add(dtInstallmentsTypes.Rows[i]["InstallmentTypeID"], dtInstallmentsTypes.Rows[i]["InstallmentTypeName"].ToString());
		}
		dtDetails = ContractsInstallments.SelectByContractID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContractInstallmentID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InstallmentTypeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContractInstallmentDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContractInstallmentAmount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPaid"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InstallmentTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع القسط" : "Installment Type");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContractInstallmentDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ القسط" : "Installment Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContractInstallmentAmount"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة القسط" : "Installment Amount");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPaid"].Header).Caption = (GlobalVariables.IsArabic ? "مدفوع" : "Paid");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InstallmentTypeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContractInstallmentDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContractInstallmentAmount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPaid"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InstallmentTypeID"].ValueList = (IValueList)(object)vlInstallmentsTypes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContractInstallmentAmount"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPaid"].DefaultCellValue = false;
	}

	public override void FillData()
	{
		if (DrUnit != null)
		{
			DataTable dataTable = Contracts.SelectByBuildingUnitID(DrUnit["BuildingUnitID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			if (dataTable.Rows.Count == 0)
			{
				drMaster = null;
				btnAddClick();
			}
			else if (dataTable.Rows.Count > 0)
			{
				RowID = dataTable.Rows[0]["ContractID"].ToString();
				DataTable dataTable2 = Contracts.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0");
				if (dataTable2.Rows.Count > 0)
				{
					drMaster = dataTable2.Rows[0];
				}
				else
				{
					drMaster = null;
				}
			}
		}
		else if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable3 = Contracts.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0");
			if (dataTable3.Rows.Count > 0)
			{
				drMaster = dataTable3.Rows[0];
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
		if (DrUnit != null)
		{
			((TextEditorControlBase)cboProject).Value = DrUnit["ProjectID"];
			((TextEditorControlBase)cboBuildings).Value = DrUnit["BuildingID"];
			((TextEditorControlBase)cboFloor).Value = DrUnit["FloorID"];
			((TextEditorControlBase)cboSample).Value = DrUnit["BuildingSampleID"];
			((TextEditorControlBase)cboUnit).Value = DrUnit["BuildingUnitID"];
			((Control)(object)txtSize).Text = decimal.Parse(DrUnit["Size"].ToString()).ToString("G29");
			((Control)(object)txtUnitPrice).Text = decimal.Parse(DrUnit["Price"].ToString()).ToString("G29");
		}
		else if (drMaster != null)
		{
			DataRow dataRow = BuildingsUnits.Select(drMaster["BuildingUnitID"].ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false).Rows[0];
			((TextEditorControlBase)cboProject).Value = Buildings.Select(dataRow["BuildingID"].ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false).Rows[0]["ProjectID"];
			((TextEditorControlBase)cboBuildings).Value = dataRow["BuildingID"];
			((TextEditorControlBase)cboFloor).Value = dataRow["FloorID"];
			((TextEditorControlBase)cboSample).Value = dataRow["BuildingSampleID"];
			((TextEditorControlBase)cboUnit).Value = dataRow["BuildingUnitID"];
			((Control)(object)txtSize).Text = decimal.Parse(dataRow["Size"].ToString()).ToString("G29");
			((Control)(object)txtUnitPrice).Text = decimal.Parse(dataRow["Price"].ToString()).ToString("G29");
		}
		if (drMaster != null)
		{
			((TextEditorControlBase)txtSalesPrice).ValueChanged -= txt_ValueChanged;
			((TextEditorControlBase)txtReservationInstallment).ValueChanged -= txt_ValueChanged;
			dtpReservationInstallmentDate.ValueChanged -= dtpDate_ValueChanged;
			((TextEditorControlBase)txtContractInstallment).ValueChanged -= txt_ValueChanged;
			dtpContractInstallmentDate.ValueChanged -= dtpContractInstallmentDate_ValueChanged;
			((TextEditorControlBase)txtMaintenanceInstallment).ValueChanged -= txt_ValueChanged;
			dtpMaintenanceInstallmentDate.ValueChanged -= dtpDate_ValueChanged;
			((TextEditorControlBase)txtInstallmentCount).ValueChanged -= txt_ValueChanged;
			((TextEditorControlBase)txtInstallmentPeriod).ValueChanged -= txt_ValueChanged;
			((TextEditorControlBase)txtFinishingInstallmentCount).ValueChanged -= txt_ValueChanged;
			((TextEditorControlBase)txtFinishingInstallmentPeriod).ValueChanged -= txt_ValueChanged;
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["ContractCode"].ToString();
			((TextEditorControlBase)cboClient).Value = drMaster["SubAccountID"];
			((TextEditorControlBase)cboFinishingType).Value = drMaster["FinishingTypeID"];
			((Control)(object)txtUnitPrice).Text = drMaster["TotalPrice"].ToString();
			((Control)(object)txtSalesPrice).Text = drMaster["SalesPrice"].ToString();
			((Control)(object)txtDiscountPercentage).Text = drMaster["DiscountPercentage"].ToString();
			((Control)(object)txtReservationInstallment).Text = drMaster["ReservationInstallment"].ToString();
			dtpReservationInstallmentDate.Value = drMaster["ReservationDate"];
			((Control)(object)txtContractInstallment).Text = drMaster["ContractInstallment"].ToString();
			dtpContractInstallmentDate.Value = drMaster["ContractDate"];
			((Control)(object)txtRestAmount).Text = drMaster["RestAmount"].ToString();
			((Control)(object)txtMaintenanceInstallment).Text = drMaster["MaintenanceInstallment"].ToString();
			dtpMaintenanceInstallmentDate.Value = drMaster["MaintenanceDate"];
			((Control)(object)txtTotalRestAmount).Text = drMaster["TotalRestAmount"].ToString();
			((Control)(object)txtInstallmentCount).Text = drMaster["InstallmentCount"].ToString();
			((Control)(object)txtInstallmentPeriod).Text = drMaster["InstallmentPeriod"].ToString();
			((UltraToggleEditorBase)chkIsCanceled).Checked = bool.Parse(drMaster["IsCanceled"].ToString());
			dtpCanceledDate.Value = drMaster["CanceledDate"];
			((Control)(object)txtRoundingValue).Text = drMaster["RoundingValue"].ToString();
			dtpFinishingStartDate.Value = drMaster["FinishingStartDate"];
			((Control)(object)txtFinishingAmount).Text = drMaster["FinishingAmount"].ToString();
			((Control)(object)txtFinishingInstallmentCount).Text = drMaster["FinishingInstallmentCount"].ToString();
			((Control)(object)txtFinishingInstallmentPeriod).Text = drMaster["FinishingInstallmentPeriod"].ToString();
			((TextEditorControlBase)txtSalesPrice).ValueChanged += txt_ValueChanged;
			((TextEditorControlBase)txtReservationInstallment).ValueChanged += txt_ValueChanged;
			dtpReservationInstallmentDate.ValueChanged += dtpDate_ValueChanged;
			((TextEditorControlBase)txtContractInstallment).ValueChanged += txt_ValueChanged;
			dtpContractInstallmentDate.ValueChanged += dtpContractInstallmentDate_ValueChanged;
			((TextEditorControlBase)txtMaintenanceInstallment).ValueChanged += txt_ValueChanged;
			dtpMaintenanceInstallmentDate.ValueChanged += dtpDate_ValueChanged;
			((TextEditorControlBase)txtInstallmentCount).ValueChanged += txt_ValueChanged;
			((TextEditorControlBase)txtInstallmentPeriod).ValueChanged += txt_ValueChanged;
			((TextEditorControlBase)txtFinishingInstallmentCount).ValueChanged += txt_ValueChanged;
			((TextEditorControlBase)txtFinishingInstallmentPeriod).ValueChanged += txt_ValueChanged;
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			dtDetails = ContractsInstallments.SelectByContractID(drMaster["ContractID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
			CalculateTotalInstallmenatsAmounts();
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
		((EditorButtonControlBase)txtCode).ReadOnly = NavMode;
		((EditorButtonControlBase)cboClient).ReadOnly = NavMode || (Updating && dtDetails != null && dtDetails.Select(" IsPaid=1 ").Length != 0);
		((EditorButtonControlBase)cboProject).ReadOnly = true;
		((EditorButtonControlBase)cboBuildings).ReadOnly = true;
		((EditorButtonControlBase)cboFloor).ReadOnly = true;
		((EditorButtonControlBase)cboSample).ReadOnly = true;
		((EditorButtonControlBase)cboUnit).ReadOnly = true;
		((EditorButtonControlBase)txtSize).ReadOnly = true;
		((EditorButtonControlBase)cboFinishingType).ReadOnly = NavMode;
		((EditorButtonControlBase)txtUnitPrice).ReadOnly = true;
		((EditorButtonControlBase)txtSalesPrice).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscountPercentage).ReadOnly = NavMode;
		((EditorButtonControlBase)txtReservationInstallment).ReadOnly = NavMode || dtDetails.Select(" InstallmentTypeID=1 And IsPaid=1 ").Length != 0 || dtDetails.Select(" InstallmentTypeID=1  ").Length > 1;
		((EditorButtonControlBase)dtpReservationInstallmentDate).ReadOnly = NavMode || dtDetails.Select(" InstallmentTypeID=1  And IsPaid=1").Length != 0 || dtDetails.Select(" InstallmentTypeID=1  ").Length > 1;
		((EditorButtonControlBase)txtContractInstallment).ReadOnly = NavMode || dtDetails.Select(" InstallmentTypeID=2  And IsPaid=1").Length != 0 || dtDetails.Select(" InstallmentTypeID=2  ").Length > 1;
		((EditorButtonControlBase)dtpContractInstallmentDate).ReadOnly = NavMode || dtDetails.Select(" InstallmentTypeID=2  And IsPaid=1").Length != 0 || dtDetails.Select(" InstallmentTypeID=2  ").Length > 1;
		((EditorButtonControlBase)txtRestAmount).ReadOnly = true;
		((EditorButtonControlBase)txtMaintenanceInstallment).ReadOnly = NavMode || dtDetails.Select(" InstallmentTypeID=3 And IsPaid=1").Length != 0 || dtDetails.Select(" InstallmentTypeID=3  ").Length > 1;
		((EditorButtonControlBase)dtpMaintenanceInstallmentDate).ReadOnly = NavMode || dtDetails.Select(" InstallmentTypeID=3 And IsPaid=1 ").Length != 0 || dtDetails.Select(" InstallmentTypeID=3  ").Length > 1;
		((EditorButtonControlBase)txtTotalRestAmount).ReadOnly = true;
		((EditorButtonControlBase)txtInstallmentCount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtInstallmentPeriod).ReadOnly = NavMode;
		((EditorButtonControlBase)txtRoundingValue).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpFinishingStartDate).ReadOnly = NavMode || dtDetails.Select(" InstallmentTypeID=5  And IsPaid=1").Length != 0 || dtDetails.Select(" InstallmentTypeID=5  ").Length > 1;
		((EditorButtonControlBase)txtFinishingAmount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtFinishingInstallmentCount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtFinishingInstallmentPeriod).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)btnClientSearch).Visible = Adding || (Updating && dtDetails != null && dtDetails.Select(" IsPaid=1 ").Length == 0);
		((Control)(object)btnCopyTo).Visible = false;
		UltraButton obj = btnNext;
		UltraButton obj2 = btnPriveous;
		bool flag = (((Control)(object)btnSearch).Visible = DrUnit == null);
		bool visible = (((Control)(object)obj2).Visible = flag);
		((Control)(object)obj).Visible = visible;
		UltraButton obj3 = btnAdd;
		visible = (((Control)(object)btnPrint).Visible = false);
		((Control)(object)obj3).Visible = visible;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtSalesPrice).ValueChanged -= txt_ValueChanged;
		((TextEditorControlBase)txtReservationInstallment).ValueChanged -= txt_ValueChanged;
		dtpReservationInstallmentDate.ValueChanged -= dtpDate_ValueChanged;
		((TextEditorControlBase)txtContractInstallment).ValueChanged -= txt_ValueChanged;
		dtpContractInstallmentDate.ValueChanged -= dtpContractInstallmentDate_ValueChanged;
		((TextEditorControlBase)txtMaintenanceInstallment).ValueChanged -= txt_ValueChanged;
		dtpMaintenanceInstallmentDate.ValueChanged -= dtpDate_ValueChanged;
		((TextEditorControlBase)txtInstallmentCount).ValueChanged -= txt_ValueChanged;
		((TextEditorControlBase)txtInstallmentPeriod).ValueChanged -= txt_ValueChanged;
		((TextEditorControlBase)txtFinishingInstallmentCount).ValueChanged -= txt_ValueChanged;
		((TextEditorControlBase)txtFinishingInstallmentPeriod).ValueChanged -= txt_ValueChanged;
		dtpContractInstallmentDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? Contracts.GetCodeByBranchID(dtpContractInstallmentDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		cboClient.SelectedIndex = -1;
		cboFinishingType.SelectedIndex = -1;
		((Control)(object)txtSalesPrice).Text = "0";
		((Control)(object)txtDiscountPercentage).Text = "0";
		((Control)(object)txtReservationInstallment).Text = "0";
		dtpReservationInstallmentDate.Value = null;
		((Control)(object)txtContractInstallment).Text = "0";
		dtpContractInstallmentDate.Value = null;
		((Control)(object)txtRestAmount).Text = "0";
		((Control)(object)txtMaintenanceInstallment).Text = "0";
		dtpMaintenanceInstallmentDate.Value = null;
		((Control)(object)txtTotalRestAmount).Text = "0";
		((Control)(object)txtInstallmentCount).Text = "0";
		((Control)(object)txtInstallmentPeriod).Text = "0";
		((Control)(object)txtRoundingValue).Text = "0";
		dtpFinishingStartDate.Value = null;
		((Control)(object)txtFinishingAmount).Text = "0";
		((Control)(object)txtFinishingInstallmentCount).Text = "0";
		((Control)(object)txtFinishingInstallmentPeriod).Text = "0";
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)txtSalesPrice).ValueChanged += txt_ValueChanged;
		((TextEditorControlBase)txtReservationInstallment).ValueChanged += txt_ValueChanged;
		dtpReservationInstallmentDate.ValueChanged += dtpDate_ValueChanged;
		((TextEditorControlBase)txtContractInstallment).ValueChanged += txt_ValueChanged;
		dtpContractInstallmentDate.ValueChanged += dtpContractInstallmentDate_ValueChanged;
		((TextEditorControlBase)txtMaintenanceInstallment).ValueChanged += txt_ValueChanged;
		dtpMaintenanceInstallmentDate.ValueChanged += dtpDate_ValueChanged;
		((TextEditorControlBase)txtInstallmentCount).ValueChanged += txt_ValueChanged;
		((TextEditorControlBase)txtInstallmentPeriod).ValueChanged += txt_ValueChanged;
		((TextEditorControlBase)txtFinishingInstallmentCount).ValueChanged += txt_ValueChanged;
		((TextEditorControlBase)txtFinishingInstallmentPeriod).ValueChanged += txt_ValueChanged;
	}

	public override bool ValidateData()
	{
		if (cboClient.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار العميل" : "Please Select Client");
			((TextEditorControlBase)cboClient).Focus();
			cboClient.DropDown();
			return false;
		}
		if (((Control)(object)txtSalesPrice).Text == "" || decimal.Parse(((Control)(object)txtSalesPrice).Text) <= 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال سعر البيع" : "Please Enter Sales Price");
			((TextEditorControlBase)txtSalesPrice).Focus();
			return false;
		}
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم  العقد" : "Please Enter The Contract Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if ((((Control)(object)txtReservationInstallment).Text == "" || decimal.Parse(((Control)(object)txtReservationInstallment).Text) <= 0m) && (dtpReservationInstallmentDate.Value == null || dtpReservationInstallmentDate.Value == DBNull.Value))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار تاريخ دفعة الحجز" : "Please Select Reservation Date");
			((Control)(object)dtpReservationInstallmentDate).Focus();
			return false;
		}
		if (dtpContractInstallmentDate.Value == null || dtpContractInstallmentDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاءإختيار تاريخ دفعة التعاقد" : "Please Select Contract Date");
			((Control)(object)dtpContractInstallmentDate).Focus();
			return false;
		}
		if (decimal.Parse((((Control)(object)txtFinishingAmount).Text == "" || ((Control)(object)txtFinishingAmount).Text == ".") ? "0" : ((Control)(object)txtFinishingAmount).Text) > 0m && dtpFinishingStartDate.Value == null)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاءإختيار تاريخ بداية التشطيبات " : "Please Select Finishing Start Date");
			((Control)(object)dtpFinishingStartDate).Focus();
			return false;
		}
		if ((((Control)(object)txtMaintenanceInstallment).Text == "" || decimal.Parse(((Control)(object)txtMaintenanceInstallment).Text) <= 0m) && (dtpMaintenanceInstallmentDate.Value == null || dtpMaintenanceInstallmentDate.Value == DBNull.Value))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاءإختيار تاريخ دفعة الصيانة" : "Please Select Maintenance Date");
			((Control)(object)dtpMaintenanceInstallmentDate).Focus();
			return false;
		}
		if (decimal.Parse((((Control)(object)txtSalesPrice).Text == "" || ((Control)(object)txtSalesPrice).Text == ".") ? "0" : ((Control)(object)txtSalesPrice).Text) + decimal.Parse((((Control)(object)txtFinishingAmount).Text == "" || ((Control)(object)txtFinishingAmount).Text == ".") ? "0" : ((Control)(object)txtFinishingAmount).Text) + decimal.Parse((((Control)(object)txtMaintenanceInstallment).Text == "" || ((Control)(object)txtMaintenanceInstallment).Text == ".") ? "0" : ((Control)(object)txtMaintenanceInstallment).Text) - decimal.Parse((((Control)(object)txtTotalInstallment).Text == "" || ((Control)(object)txtTotalInstallment).Text == ".") ? "0" : ((Control)(object)txtTotalInstallment).Text) > decimal.Parse("0.001"))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "قيمة الاقساط لا تساوى سعر البيع" : "Total Installment Amount Not Equal Sales Price");
			return false;
		}
		if (((Control)(object)txtContractInstallment).Text == "" || decimal.Parse(((Control)(object)txtContractInstallment).Text) <= 0m)
		{
			GlobalVariables.QuestionMB.Show("هل تريد الحفظ بدون دفعة تعاقد؟", "Do you Want To Save Without Contract Payment ?");
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtContractInstallment).Focus();
				return false;
			}
		}
		else if (Contracts.CheckCode(Adding ? "0" : drMaster["ContractID"].ToString(), dtpContractInstallmentDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtCode).Text, GlobalVariables.CurrentBranchID))
		{
			string codeByBranchID = Contracts.GetCodeByBranchID(dtpContractInstallmentDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل التعاقد", "Please insert details for this Contract");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ContractInstallmentDate"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال تاريخ القسط  ", "Please Enter Installment Date");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ContractInstallmentDate"];
				((UltraGridBase)ULGData).Rows[i].Cells["ContractInstallmentDate"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["ContractInstallmentAmount"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ContractInstallmentAmount"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال قيمة القسط   ", "Please Enter Installment Amount ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ContractInstallmentAmount"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["InstallmentTypeID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار نوع القسط   ", "Please Select Installment Type ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["InstallmentTypeID"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
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

	public override void btnOKClick()
	{
		((Control)(object)btnOK).Focus();
		if (!ValidateData())
		{
			return;
		}
		DataSaved = true;
		if (Adding)
		{
			AddData();
			if (DataSaved)
			{
				Adding = false;
				SetControls(NavMode: true);
				FillData();
			}
			return;
		}
		UpdateData();
		if (DataSaved)
		{
			Updating = false;
			SetControls(NavMode: true);
			if (RowID != "" && TableName != "")
			{
				UsersTransactions.DeleteByRowID(TableName, RowID);
			}
			FillData();
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
		if (((DataTable)((UltraGridBase)ULGData).DataSource).Select(" IsPaid=1 ").Length != 0)
		{
			GlobalVariables.QuestionMB.Show("  يوجد دفعات مسددة على هذا التعاقد هل تريد الغاء التعاقد؟", "There Is Paid Installments on this Contract Are you Sure You Want To Cancel This Contract ?");
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
			return;
		}
		GlobalVariables.QuestionMB.Show(" هل تريد الغاء التعاقد؟", "Are you Sure You Want To Cancel This Contract ?");
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

	public override void AddData()
	{
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Expected O, but got Unknown
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = Contracts.Insert_Update("-1", ((Control)(object)txtCode).Text, ((TextEditorControlBase)cboClient).Value.ToString(), ((TextEditorControlBase)cboUnit).Value.ToString(), (cboFinishingType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboFinishingType).Value.ToString(), (((Control)(object)txtUnitPrice).Text == "") ? "0" : ((Control)(object)txtUnitPrice).Text, (((Control)(object)txtSalesPrice).Text == "") ? "0" : ((Control)(object)txtSalesPrice).Text, (((Control)(object)txtDiscountPercentage).Text == "") ? "0" : ((Control)(object)txtDiscountPercentage).Text, (decimal.Parse((((Control)(object)txtUnitPrice).Text == "") ? "0" : ((Control)(object)txtUnitPrice).Text) - decimal.Parse((((Control)(object)txtSalesPrice).Text == "") ? "0" : ((Control)(object)txtSalesPrice).Text)).ToString(), (((Control)(object)txtReservationInstallment).Text == "") ? "0" : ((Control)(object)txtReservationInstallment).Text, (dtpReservationInstallmentDate.Value == null) ? "Null" : dtpReservationInstallmentDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtContractInstallment).Text == "") ? "0" : ((Control)(object)txtContractInstallment).Text, (dtpContractInstallmentDate.Value == null) ? "Null" : dtpContractInstallmentDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtRestAmount).Text == "") ? "0" : ((Control)(object)txtRestAmount).Text, (((Control)(object)txtMaintenanceInstallment).Text == "") ? "0" : ((Control)(object)txtMaintenanceInstallment).Text, (dtpMaintenanceInstallmentDate.Value == null) ? "Null" : dtpMaintenanceInstallmentDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtTotalRestAmount).Text == "") ? "0" : ((Control)(object)txtTotalRestAmount).Text, (((Control)(object)txtInstallmentCount).Text == "") ? "0" : ((Control)(object)txtInstallmentCount).Text, (((Control)(object)txtInstallmentPeriod).Text == "") ? "0" : ((Control)(object)txtInstallmentPeriod).Text, (dtpFinishingStartDate.Value == null) ? "Null" : dtpFinishingStartDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtFinishingAmount).Text == "") ? "0" : ((Control)(object)txtFinishingAmount).Text, (((Control)(object)txtFinishingInstallmentCount).Text == "") ? "0" : ((Control)(object)txtFinishingInstallmentCount).Text, (((Control)(object)txtFinishingInstallmentPeriod).Text == "") ? "0" : ((Control)(object)txtFinishingInstallmentPeriod).Text, IsGenerateContractTotalJV ? "1" : "0", (((Control)(object)txtRoundingValue).Text == "") ? "0" : ((Control)(object)txtRoundingValue).Text, "Null", ((Control)(object)txtNotes).Text, "0", "Null", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ContractInstallmentID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["ContractID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			ContractsInstallments.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			ContractsInstallments.GenerateSalesJV(num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
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
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Expected O, but got Unknown
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d2: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = Contracts.Insert_Update(drMaster["ContractID"].ToString(), ((Control)(object)txtCode).Text, ((TextEditorControlBase)cboClient).Value.ToString(), ((TextEditorControlBase)cboUnit).Value.ToString(), (cboFinishingType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboFinishingType).Value.ToString(), (((Control)(object)txtUnitPrice).Text == "") ? "0" : ((Control)(object)txtUnitPrice).Text, (((Control)(object)txtSalesPrice).Text == "") ? "0" : ((Control)(object)txtSalesPrice).Text, (((Control)(object)txtDiscountPercentage).Text == "") ? "0" : ((Control)(object)txtDiscountPercentage).Text, (decimal.Parse((((Control)(object)txtUnitPrice).Text == "") ? "0" : ((Control)(object)txtUnitPrice).Text) - decimal.Parse((((Control)(object)txtSalesPrice).Text == "") ? "0" : ((Control)(object)txtSalesPrice).Text)).ToString(), (((Control)(object)txtReservationInstallment).Text == "") ? "0" : ((Control)(object)txtReservationInstallment).Text, (dtpReservationInstallmentDate.Value == null) ? "Null" : dtpReservationInstallmentDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtContractInstallment).Text == "") ? "0" : ((Control)(object)txtContractInstallment).Text, (dtpContractInstallmentDate.Value == null) ? "Null" : dtpContractInstallmentDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtRestAmount).Text == "") ? "0" : ((Control)(object)txtRestAmount).Text, (((Control)(object)txtMaintenanceInstallment).Text == "") ? "0" : ((Control)(object)txtMaintenanceInstallment).Text, (dtpMaintenanceInstallmentDate.Value == null) ? "Null" : dtpMaintenanceInstallmentDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtTotalRestAmount).Text == "") ? "0" : ((Control)(object)txtTotalRestAmount).Text, (((Control)(object)txtInstallmentCount).Text == "") ? "0" : ((Control)(object)txtInstallmentCount).Text, (((Control)(object)txtInstallmentPeriod).Text == "") ? "0" : ((Control)(object)txtInstallmentPeriod).Text, (dtpFinishingStartDate.Value == null) ? "Null" : dtpFinishingStartDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtFinishingAmount).Text == "") ? "0" : ((Control)(object)txtFinishingAmount).Text, (((Control)(object)txtFinishingInstallmentCount).Text == "") ? "0" : ((Control)(object)txtFinishingInstallmentCount).Text, (((Control)(object)txtFinishingInstallmentPeriod).Text == "") ? "0" : ((Control)(object)txtFinishingInstallmentPeriod).Text, bool.Parse(drMaster["IsGenerateContractTotalJV"].ToString()) ? "1" : "0", (((Control)(object)txtRoundingValue).Text == "") ? "0" : ((Control)(object)txtRoundingValue).Text, (drMaster["TotalJVID"] == DBNull.Value) ? "Null" : drMaster["TotalJVID"].ToString(), ((Control)(object)txtNotes).Text, bool.Parse(drMaster["IsCanceled"].ToString()) ? "1" : "0", (drMaster["CanceledDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["CanceledDate"].ToString()).ToString(GlobalVariables.DateLongFormate), (drMaster["CanceledJVID"] == DBNull.Value) ? "Null" : drMaster["CanceledJVID"].ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ContractID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["ContractInstallmentID"].Value.ToString() + ",";
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			ContractsInstallments.DeleteJVIDForUpdate(num.ToString(), text);
			Main.DeleteForUpdate("Con_ContractsInstallments", "ContractID", drMaster["ContractID"].ToString(), "ContractInstallmentID", text);
			ContractsInstallments.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			ContractsInstallments.GenerateSalesJV(num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
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
			if (drMaster != null)
			{
				frmSelectDate frmSelectDate2 = new frmSelectDate(GlobalVariables.IsArabic ? "تاريخ الالغاء" : "Canceletion Date");
				frmSelectDate2.WindowState = FormWindowState.Normal;
				frmSelectDate2.ShowDialog();
				if (!frmSelectDate2.DateTimeValue.HasValue)
				{
					Main.RollbackBulkTrans(FromServer: false);
					DataSaved = false;
					return;
				}
				Contracts.GenerateCanceledJV(drMaster["ContractID"].ToString(), frmSelectDate2.DateTimeValue.Value.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
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

	public override void btnPrintClick()
	{
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ContractsSearchReport(-1, 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["ContractID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		dtProjects = Projects.FillCombo("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboProject, dtProjects, "ProjectID", "ProjectName");
		dtBuildings = Buildings.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBuildings, dtBuildings, "BuildingID", "BuildingName");
		dtUnits = BuildingsUnits.FillCombo((DrUnit == null) ? "-1" : DrUnit["BuildingID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboUnit, dtUnits, "BuildingUnitID", "BuildingUnitCode");
		dtFloors = Floors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboFloor, dtFloors, "FloorID", "FloorName");
		dtSamples = BuildingsSamples.FillCombo((DrUnit == null) ? "-1" : DrUnit["BuildingID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSample, dtSamples, "BuildingSampleID", "BuildingSampleCode");
		dtFinishingTypes = FinishingTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboFinishingType, dtFinishingTypes, "FinishingTypeID", "FinishingName");
		dtInstallmentsTypes = InstallmentsTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInstallmentsTypes.ValueListItems.Clear();
		for (int i = 0; i < dtInstallmentsTypes.Rows.Count; i++)
		{
			vlInstallmentsTypes.ValueListItems.Add(dtInstallmentsTypes.Rows[i]["InstallmentTypeID"], dtInstallmentsTypes.Rows[i]["InstallmentTypeName"].ToString());
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "IsPaid")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (ULGData.ActiveCell != null && (((UltraGridBase)ULGData).ActiveRow.Cells["PaymentID"].Value != DBNull.Value || (((UltraGridBase)ULGData).ActiveRow.Cells["InstallmentTypeID"].Value != DBNull.Value && int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["InstallmentTypeID"].Value.ToString()) < 4)))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ContractInstallmentAmount")
		{
			CalculateTotalInstallmenatsAmounts();
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && (((UltraGridBase)ULGData).ActiveRow.Cells["PaymentID"].Value != DBNull.Value || (((UltraGridBase)ULGData).ActiveRow.Cells["InstallmentTypeID"].Value != DBNull.Value && int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["InstallmentTypeID"].Value.ToString()) < 4)))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا يمكن حذف هذا القسط تم سداده" : "Cannot Delete This Installment Is Already Paid");
			((CancelEventArgs)(object)e).Cancel = true;
		}
		else
		{
			base.ULGData_BeforeRowsDeleted(sender, e);
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ContractInstallmentAmount")
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalculateTotalInstallmenatsAmounts();
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtInteger_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void txtRoundingValue_ValueChanged(object sender, EventArgs e)
	{
		CalculateInstallments();
	}

	private void txt_ValueChanged(object sender, EventArgs e)
	{
		CalculateAmount();
		CalculateInstallments();
	}

	private void btnClientSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Clients("-1", "-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboClient).Value = num;
		}
	}

	public void CalculateInstallments()
	{
		if (Adding)
		{
			dtDetails.Rows.Clear();
			decimal num = default(decimal);
			if (((Control)(object)txtReservationInstallment).Text != "" && decimal.Parse(((Control)(object)txtReservationInstallment).Text) > 0m && dtpReservationInstallmentDate.Value != null)
			{
				DataRow dataRow = dtDetails.NewRow();
				dataRow["ContractInstallmentID"] = -1;
				dataRow["ContractInstallmentDate"] = dtpReservationInstallmentDate.DateTime;
				dataRow["ContractInstallmentAmount"] = ((Control)(object)txtReservationInstallment).Text;
				dataRow["InstallmentTypeID"] = 1;
				dataRow["IsPaid"] = false;
				dtDetails.Rows.Add(dataRow);
				num += decimal.Parse(((Control)(object)txtReservationInstallment).Text);
			}
			if (((Control)(object)txtContractInstallment).Text != "" && decimal.Parse(((Control)(object)txtContractInstallment).Text) > 0m && dtpContractInstallmentDate.Value != null)
			{
				DataRow dataRow2 = dtDetails.NewRow();
				dataRow2["ContractInstallmentID"] = -1;
				dataRow2["ContractInstallmentDate"] = dtpContractInstallmentDate.DateTime;
				dataRow2["ContractInstallmentAmount"] = ((Control)(object)txtContractInstallment).Text;
				dataRow2["InstallmentTypeID"] = 2;
				dataRow2["IsPaid"] = false;
				dtDetails.Rows.Add(dataRow2);
				num += decimal.Parse(((Control)(object)txtContractInstallment).Text);
			}
			if (((Control)(object)txtMaintenanceInstallment).Text != "" && decimal.Parse(((Control)(object)txtMaintenanceInstallment).Text) > 0m && dtpMaintenanceInstallmentDate.Value != null)
			{
				DataRow dataRow3 = dtDetails.NewRow();
				dataRow3["ContractInstallmentID"] = -1;
				dataRow3["ContractInstallmentDate"] = dtpMaintenanceInstallmentDate.DateTime;
				dataRow3["ContractInstallmentAmount"] = ((Control)(object)txtMaintenanceInstallment).Text;
				dataRow3["InstallmentTypeID"] = 3;
				dataRow3["IsPaid"] = false;
				dtDetails.Rows.Add(dataRow3);
				num += decimal.Parse(((Control)(object)txtMaintenanceInstallment).Text);
			}
			if (((Control)(object)txtInstallmentCount).Text != "" && ((Control)(object)txtInstallmentCount).Text != "." && decimal.Parse(((Control)(object)txtInstallmentCount).Text) > 0m && ((Control)(object)txtInstallmentPeriod).Text != "" && ((Control)(object)txtInstallmentPeriod).Text != "." && decimal.Parse(((Control)(object)txtInstallmentPeriod).Text) > 0m && dtpContractInstallmentDate.Value != null)
			{
				decimal num2 = default(decimal);
				decimal num3 = default(decimal);
				decimal num4 = default(decimal);
				decimal num5 = default(decimal);
				decimal num6 = default(decimal);
				decimal num7 = default(decimal);
				int num8 = int.Parse(((Control)(object)txtInstallmentCount).Text);
				decimal num9 = decimal.Parse((((Control)(object)txtRoundingValue).Text == "" || ((Control)(object)txtRoundingValue).Text == ".") ? "0" : ((Control)(object)txtRoundingValue).Text);
				num7 = decimal.Parse(((Control)(object)txtRestAmount).Text) / (decimal)int.Parse(((Control)(object)txtInstallmentCount).Text);
				if (num9 > 0m)
				{
					num3 = num7 % num9;
					num4 = num9 / 2m;
					num5 = num9 - num3;
					num6 = ((!(num3 < num4)) ? (num7 + num5) : (num7 - num3));
				}
				else
				{
					num6 = num7;
				}
				num2 = decimal.Parse(((Control)(object)txtInstallmentPeriod).Text) / decimal.Parse(((Control)(object)txtInstallmentCount).Text);
				decimal num10 = num2;
				for (int i = 0; i < int.Parse(((Control)(object)txtInstallmentCount).Text); i++)
				{
					DataRow dataRow4 = dtDetails.NewRow();
					dataRow4["ContractInstallmentID"] = -1;
					dataRow4["ContractInstallmentDate"] = dtpContractInstallmentDate.DateTime.AddMonths((int)num10);
					if (i == num8 - 1)
					{
						dataRow4["ContractInstallmentAmount"] = decimal.Parse(((Control)(object)txtRestAmount).Text) - num6 * (decimal)(num8 - 1);
					}
					else
					{
						dataRow4["ContractInstallmentAmount"] = num6;
					}
					dataRow4["InstallmentTypeID"] = 4;
					dataRow4["IsPaid"] = false;
					dtDetails.Rows.Add(dataRow4);
					num += num6;
					num10 += num2;
				}
			}
			if (((Control)(object)txtFinishingInstallmentCount).Text != "" && ((Control)(object)txtFinishingInstallmentCount).Text != "." && decimal.Parse(((Control)(object)txtFinishingInstallmentCount).Text) > 0m && ((Control)(object)txtFinishingInstallmentPeriod).Text != "" && ((Control)(object)txtFinishingInstallmentPeriod).Text != "." && decimal.Parse(((Control)(object)txtFinishingInstallmentPeriod).Text) > 0m && dtpFinishingStartDate.Value != null)
			{
				decimal num11 = default(decimal);
				decimal num12 = default(decimal);
				decimal num13 = default(decimal);
				decimal num14 = default(decimal);
				decimal num15 = default(decimal);
				int num16 = int.Parse(((Control)(object)txtFinishingInstallmentCount).Text);
				decimal num17 = decimal.Parse((((Control)(object)txtFinishingAmount).Text == "" || ((Control)(object)txtFinishingAmount).Text == ".") ? "0" : ((Control)(object)txtFinishingAmount).Text);
				decimal num18 = decimal.Parse((((Control)(object)txtRoundingValue).Text == "" || ((Control)(object)txtRoundingValue).Text == ".") ? "0" : ((Control)(object)txtRoundingValue).Text);
				num15 = num17 / (decimal)num16;
				decimal num19;
				if (num18 > 0m)
				{
					num12 = num15 % num18;
					num13 = num18 / 2m;
					num14 = num18 - num12;
					num19 = ((!(num12 < num13)) ? (num15 + num14) : (num15 - num12));
				}
				else
				{
					num19 = num15;
				}
				num11 = decimal.Parse(((Control)(object)txtFinishingInstallmentPeriod).Text) / decimal.Parse(((Control)(object)txtFinishingInstallmentCount).Text);
				decimal num20 = num11;
				for (int j = 0; j < num16; j++)
				{
					DataRow dataRow5 = dtDetails.NewRow();
					dataRow5["ContractInstallmentID"] = -1;
					dataRow5["ContractInstallmentDate"] = dtpFinishingStartDate.DateTime.AddMonths((int)num20);
					if (j == num16 - 1)
					{
						dataRow5["ContractInstallmentAmount"] = num17 - num19 * (decimal)(num16 - 1);
					}
					else
					{
						dataRow5["ContractInstallmentAmount"] = num19;
					}
					dataRow5["InstallmentTypeID"] = 5;
					dataRow5["IsPaid"] = false;
					dtDetails.Rows.Add(dataRow5);
					num += num19;
					num20 += num11;
				}
			}
			((UltraGridBase)ULGData).UpdateData();
			((Control)(object)txtTotalInstallment).Text = num.ToString();
		}
		if (Updating)
		{
			if (((Control)(object)txtReservationInstallment).Text != "" && decimal.Parse(((Control)(object)txtReservationInstallment).Text) > 0m && dtpReservationInstallmentDate.Value != null && dtDetails.Rows.Count > 0 && dtDetails.Select(" InstallmentTypeID =1 And IsPaid=0").Length == 1)
			{
				dtDetails.Select(" InstallmentTypeID =1 And IsPaid=0")[0]["ContractInstallmentDate"] = dtpReservationInstallmentDate.DateTime;
				dtDetails.Select(" InstallmentTypeID =1 And IsPaid=0")[0]["ContractInstallmentAmount"] = ((Control)(object)txtReservationInstallment).Text;
			}
			if (((Control)(object)txtContractInstallment).Text != "" && decimal.Parse(((Control)(object)txtContractInstallment).Text) > 0m && dtpContractInstallmentDate.Value != null && dtDetails.Rows.Count > 0 && dtDetails.Select(" InstallmentTypeID =2 And IsPaid=0").Length == 1)
			{
				dtDetails.Select(" InstallmentTypeID =2 And IsPaid=0")[0]["ContractInstallmentDate"] = dtpContractInstallmentDate.DateTime;
				dtDetails.Select(" InstallmentTypeID =2 And IsPaid=0")[0]["ContractInstallmentAmount"] = ((Control)(object)txtContractInstallment).Text;
			}
			if (((Control)(object)txtMaintenanceInstallment).Text != "" && decimal.Parse(((Control)(object)txtMaintenanceInstallment).Text) > 0m && dtpMaintenanceInstallmentDate.Value != null && dtDetails.Rows.Count > 0 && dtDetails.Select(" InstallmentTypeID =3 And IsPaid=0").Length == 1)
			{
				dtDetails.Select(" InstallmentTypeID =3 And IsPaid=0")[0]["ContractInstallmentDate"] = dtpMaintenanceInstallmentDate.DateTime;
				dtDetails.Select(" InstallmentTypeID =3 And IsPaid=0")[0]["ContractInstallmentAmount"] = ((Control)(object)txtMaintenanceInstallment).Text;
			}
		}
		CalculateTotalInstallmenatsAmounts();
	}

	public void CalculateAmount()
	{
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		decimal num3 = default(decimal);
		decimal num4 = decimal.Parse((((Control)(object)txtUnitPrice).Text == "" || ((Control)(object)txtUnitPrice).Text == ".") ? "0" : ((Control)(object)txtUnitPrice).Text);
		decimal num5 = decimal.Parse((((Control)(object)txtSalesPrice).Text == "" || ((Control)(object)txtSalesPrice).Text == ".") ? "0" : ((Control)(object)txtSalesPrice).Text);
		decimal num6 = decimal.Parse((((Control)(object)txtReservationInstallment).Text == "" || ((Control)(object)txtReservationInstallment).Text == ".") ? "0" : ((Control)(object)txtReservationInstallment).Text);
		decimal num7 = decimal.Parse((((Control)(object)txtContractInstallment).Text == "" || ((Control)(object)txtContractInstallment).Text == ".") ? "0" : ((Control)(object)txtContractInstallment).Text);
		decimal num8 = decimal.Parse((((Control)(object)txtMaintenanceInstallment).Text == "" || ((Control)(object)txtMaintenanceInstallment).Text == ".") ? "0" : ((Control)(object)txtMaintenanceInstallment).Text);
		decimal num9 = decimal.Parse((((Control)(object)txtFinishingAmount).Text == "" || ((Control)(object)txtFinishingAmount).Text == ".") ? "0" : ((Control)(object)txtFinishingAmount).Text);
		num = num5 - num6 - num7;
		num2 = num5 - num6 - num7 + num8 + num9;
		num3 = ((num4 == 0m) ? 0m : ((num5 - num4) / num4 * 100m));
		((Control)(object)txtDiscountPercentage).Text = num3.ToString();
		((Control)(object)txtRestAmount).Text = num.ToString();
		((Control)(object)txtTotalRestAmount).Text = num2.ToString();
	}

	public void CalculateTotalInstallmenatsAmounts()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse((((UltraGridBase)ULGData).Rows[i].Cells["ContractInstallmentAmount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["ContractInstallmentAmount"].Value.ToString());
		}
		((Control)(object)txtTotalInstallment).Text = num.ToString();
	}

	private void dtpContractInstallmentDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = Contracts.GetCodeByBranchID(dtpContractInstallmentDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
		CalculateInstallments();
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		CalculateInstallments();
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
			if (((UltraToggleEditorBase)chkIsCanceled).Checked)
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل التعاقد لأنها لاغي", "Cannot Update This Contract Because It Is Canceled");
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
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Expected O, but got Unknown
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Expected O, but got Unknown
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Expected O, but got Unknown
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Expected O, but got Unknown
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Expected O, but got Unknown
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Expected O, but got Unknown
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Expected O, but got Unknown
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Expected O, but got Unknown
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Expected O, but got Unknown
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Expected O, but got Unknown
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Expected O, but got Unknown
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Expected O, but got Unknown
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Expected O, but got Unknown
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Expected O, but got Unknown
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Expected O, but got Unknown
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Expected O, but got Unknown
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Expected O, but got Unknown
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Expected O, but got Unknown
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Expected O, but got Unknown
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Expected O, but got Unknown
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Expected O, but got Unknown
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Expected O, but got Unknown
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Expected O, but got Unknown
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Expected O, but got Unknown
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Expected O, but got Unknown
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Expected O, but got Unknown
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Expected O, but got Unknown
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Expected O, but got Unknown
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Expected O, but got Unknown
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Expected O, but got Unknown
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Expected O, but got Unknown
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Expected O, but got Unknown
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Expected O, but got Unknown
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Expected O, but got Unknown
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Expected O, but got Unknown
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Expected O, but got Unknown
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Expected O, but got Unknown
		//IL_0794: Unknown result type (might be due to invalid IL or missing references)
		//IL_079e: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Constructions.Transactions.frmContracts));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblInstallmentCount = new UltraLabel();
		this.txtInstallmentCount = new UltraTextEditor();
		this.cboClient = new UltraComboEditor();
		this.lblClient = new UltraLabel();
		this.lblInstallmentPeriod = new UltraLabel();
		this.txtTotalInstallment = new UltraTextEditor();
		this.lblTotalInstallment = new UltraLabel();
		this.btnClientSearch = new UltraButton();
		this.lblBuildings = new UltraLabel();
		this.cboBuildings = new UltraComboEditor();
		this.lblProjects = new UltraLabel();
		this.cboProject = new UltraComboEditor();
		this.lblFloor = new UltraLabel();
		this.lblUnit = new UltraLabel();
		this.lblSample = new UltraLabel();
		this.txtSize = new UltraTextEditor();
		this.lblSize = new UltraLabel();
		this.lblFinishingType = new UltraLabel();
		this.cboFinishingType = new UltraComboEditor();
		this.txtUnitPrice = new UltraTextEditor();
		this.lblUnitPrice = new UltraLabel();
		this.txtSalesPrice = new UltraTextEditor();
		this.lblSalesPrice = new UltraLabel();
		this.txtDiscountPercentage = new UltraTextEditor();
		this.lblDiscountPercentage = new UltraLabel();
		this.txtReservationInstallment = new UltraTextEditor();
		this.lblReservationInstallment = new UltraLabel();
		this.lblReservationInstallmentDate = new UltraLabel();
		this.dtpReservationInstallmentDate = new UltraDateTimeEditor();
		this.lblContractInstallmentDate = new UltraLabel();
		this.dtpContractInstallmentDate = new UltraDateTimeEditor();
		this.txtContractInstallment = new UltraTextEditor();
		this.lblContractInstallment = new UltraLabel();
		this.lblMaintenanceInstallmentDate = new UltraLabel();
		this.dtpMaintenanceInstallmentDate = new UltraDateTimeEditor();
		this.txtTotalRestAmount = new UltraTextEditor();
		this.lblTotalRestAmount = new UltraLabel();
		this.txtRestAmount = new UltraTextEditor();
		this.lblRestAmount = new UltraLabel();
		this.txtMaintenanceInstallment = new UltraTextEditor();
		this.lblMaintenanceInstallment = new UltraLabel();
		this.txtInstallmentPeriod = new UltraTextEditor();
		this.cboFloor = new UltraComboEditor();
		this.cboSample = new UltraComboEditor();
		this.cboUnit = new UltraComboEditor();
		this.lblSizeMeter = new UltraLabel();
		this.dtpFinishingStartDate = new UltraDateTimeEditor();
		this.lblFinishingStartDate = new UltraLabel();
		this.lblFinishingAmount = new UltraLabel();
		this.txtFinishingAmount = new UltraTextEditor();
		this.lblFinishingInstallmentCount = new UltraLabel();
		this.txtFinishingInstallmentCount = new UltraTextEditor();
		this.lblFinishingInstallmentPeriod = new UltraLabel();
		this.txtFinishingInstallmentPeriod = new UltraTextEditor();
		this.lblRoundingValue = new UltraLabel();
		this.txtRoundingValue = new UltraTextEditor();
		this.chkIsCanceled = new UltraCheckEditor();
		this.dtpCanceledDate = new UltraDateTimeEditor();
		this.lblCancelationDate = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInstallmentCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalInstallment).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBuildings).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboProject).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSize).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboFinishingType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUnitPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalesPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtReservationInstallment).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpReservationInstallmentDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpContractInstallmentDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtContractInstallment).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpMaintenanceInstallmentDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalRestAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRestAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMaintenanceInstallment).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInstallmentPeriod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboFloor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSample).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFinishingStartDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFinishingAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFinishingInstallmentCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFinishingInstallmentPeriod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRoundingValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsCanceled).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCanceledDate).BeginInit();
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
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
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
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblInstallmentCount, "lblInstallmentCount");
		this.lblInstallmentCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInstallmentCount).Name = "lblInstallmentCount";
		((ControlBase)this.lblInstallmentCount).WrapText = false;
		resources.ApplyResources(this.txtInstallmentCount, "txtInstallmentCount");
		((System.Windows.Forms.Control)(object)this.txtInstallmentCount).Name = "txtInstallmentCount";
		((TextEditorControlBase)this.txtInstallmentCount).ValueChanged += new System.EventHandler(txt_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtInstallmentCount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInteger_KeyPress);
		resources.ApplyResources(this.cboClient, "cboClient");
		((TextEditorControlBase)this.cboClient).AlwaysInEditMode = true;
		this.cboClient.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClient).Name = "cboClient";
		resources.ApplyResources(this.lblClient, "lblClient");
		this.lblClient.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClient).Name = "lblClient";
		((ControlBase)this.lblClient).WrapText = false;
		resources.ApplyResources(this.lblInstallmentPeriod, "lblInstallmentPeriod");
		this.lblInstallmentPeriod.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInstallmentPeriod).Name = "lblInstallmentPeriod";
		((ControlBase)this.lblInstallmentPeriod).WrapText = false;
		resources.ApplyResources(this.txtTotalInstallment, "txtTotalInstallment");
		((System.Windows.Forms.Control)(object)this.txtTotalInstallment).Name = "txtTotalInstallment";
		resources.ApplyResources(this.lblTotalInstallment, "lblTotalInstallment");
		this.lblTotalInstallment.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalInstallment).Name = "lblTotalInstallment";
		((ControlBase)this.lblTotalInstallment).WrapText = false;
		resources.ApplyResources(this.btnClientSearch, "btnClientSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance10");
		((ControlBase)this.btnClientSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Name = "btnClientSearch";
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Click += new System.EventHandler(btnClientSearch_Click);
		resources.ApplyResources(this.lblBuildings, "lblBuildings");
		this.lblBuildings.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBuildings).Name = "lblBuildings";
		((ControlBase)this.lblBuildings).WrapText = false;
		resources.ApplyResources(this.cboBuildings, "cboBuildings");
		((TextEditorControlBase)this.cboBuildings).AlwaysInEditMode = true;
		this.cboBuildings.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboBuildings).Name = "cboBuildings";
		resources.ApplyResources(this.lblProjects, "lblProjects");
		this.lblProjects.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblProjects).Name = "lblProjects";
		((ControlBase)this.lblProjects).WrapText = false;
		resources.ApplyResources(this.cboProject, "cboProject");
		((TextEditorControlBase)this.cboProject).AlwaysInEditMode = true;
		this.cboProject.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboProject).Name = "cboProject";
		resources.ApplyResources(this.lblFloor, "lblFloor");
		this.lblFloor.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFloor).Name = "lblFloor";
		((ControlBase)this.lblFloor).WrapText = false;
		resources.ApplyResources(this.lblUnit, "lblUnit");
		this.lblUnit.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUnit).Name = "lblUnit";
		((ControlBase)this.lblUnit).WrapText = false;
		resources.ApplyResources(this.lblSample, "lblSample");
		this.lblSample.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSample).Name = "lblSample";
		((ControlBase)this.lblSample).WrapText = false;
		resources.ApplyResources(this.txtSize, "txtSize");
		((System.Windows.Forms.Control)(object)this.txtSize).Name = "txtSize";
		resources.ApplyResources(this.lblSize, "lblSize");
		this.lblSize.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSize).Name = "lblSize";
		((ControlBase)this.lblSize).WrapText = false;
		resources.ApplyResources(this.lblFinishingType, "lblFinishingType");
		this.lblFinishingType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFinishingType).Name = "lblFinishingType";
		((ControlBase)this.lblFinishingType).WrapText = false;
		resources.ApplyResources(this.cboFinishingType, "cboFinishingType");
		((TextEditorControlBase)this.cboFinishingType).AlwaysInEditMode = true;
		this.cboFinishingType.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboFinishingType).Name = "cboFinishingType";
		resources.ApplyResources(this.txtUnitPrice, "txtUnitPrice");
		((System.Windows.Forms.Control)(object)this.txtUnitPrice).Name = "txtUnitPrice";
		resources.ApplyResources(this.lblUnitPrice, "lblUnitPrice");
		this.lblUnitPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUnitPrice).Name = "lblUnitPrice";
		((ControlBase)this.lblUnitPrice).WrapText = false;
		resources.ApplyResources(this.txtSalesPrice, "txtSalesPrice");
		((System.Windows.Forms.Control)(object)this.txtSalesPrice).Name = "txtSalesPrice";
		((TextEditorControlBase)this.txtSalesPrice).ValueChanged += new System.EventHandler(txt_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtSalesPrice).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblSalesPrice, "lblSalesPrice");
		this.lblSalesPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSalesPrice).Name = "lblSalesPrice";
		((ControlBase)this.lblSalesPrice).WrapText = false;
		resources.ApplyResources(this.txtDiscountPercentage, "txtDiscountPercentage");
		((System.Windows.Forms.Control)(object)this.txtDiscountPercentage).Name = "txtDiscountPercentage";
		resources.ApplyResources(this.lblDiscountPercentage, "lblDiscountPercentage");
		this.lblDiscountPercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscountPercentage).Name = "lblDiscountPercentage";
		((ControlBase)this.lblDiscountPercentage).WrapText = false;
		resources.ApplyResources(this.txtReservationInstallment, "txtReservationInstallment");
		((System.Windows.Forms.Control)(object)this.txtReservationInstallment).Name = "txtReservationInstallment";
		((TextEditorControlBase)this.txtReservationInstallment).ValueChanged += new System.EventHandler(txt_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtReservationInstallment).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblReservationInstallment, "lblReservationInstallment");
		this.lblReservationInstallment.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReservationInstallment).Name = "lblReservationInstallment";
		((ControlBase)this.lblReservationInstallment).WrapText = false;
		resources.ApplyResources(this.lblReservationInstallmentDate, "lblReservationInstallmentDate");
		this.lblReservationInstallmentDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReservationInstallmentDate).Name = "lblReservationInstallmentDate";
		((ControlBase)this.lblReservationInstallmentDate).WrapText = false;
		resources.ApplyResources(this.dtpReservationInstallmentDate, "dtpReservationInstallmentDate");
		((UltraWinEditorMaskedControlBase)this.dtpReservationInstallmentDate).AlwaysInEditMode = true;
		this.dtpReservationInstallmentDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpReservationInstallmentDate).Name = "dtpReservationInstallmentDate";
		this.dtpReservationInstallmentDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.lblContractInstallmentDate, "lblContractInstallmentDate");
		this.lblContractInstallmentDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblContractInstallmentDate).Name = "lblContractInstallmentDate";
		((ControlBase)this.lblContractInstallmentDate).WrapText = false;
		resources.ApplyResources(this.dtpContractInstallmentDate, "dtpContractInstallmentDate");
		((UltraWinEditorMaskedControlBase)this.dtpContractInstallmentDate).AlwaysInEditMode = true;
		this.dtpContractInstallmentDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpContractInstallmentDate).Name = "dtpContractInstallmentDate";
		this.dtpContractInstallmentDate.ValueChanged += new System.EventHandler(dtpContractInstallmentDate_ValueChanged);
		resources.ApplyResources(this.txtContractInstallment, "txtContractInstallment");
		((System.Windows.Forms.Control)(object)this.txtContractInstallment).Name = "txtContractInstallment";
		((TextEditorControlBase)this.txtContractInstallment).ValueChanged += new System.EventHandler(txt_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtContractInstallment).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblContractInstallment, "lblContractInstallment");
		this.lblContractInstallment.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblContractInstallment).Name = "lblContractInstallment";
		((ControlBase)this.lblContractInstallment).WrapText = false;
		resources.ApplyResources(this.lblMaintenanceInstallmentDate, "lblMaintenanceInstallmentDate");
		this.lblMaintenanceInstallmentDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMaintenanceInstallmentDate).Name = "lblMaintenanceInstallmentDate";
		((ControlBase)this.lblMaintenanceInstallmentDate).WrapText = false;
		resources.ApplyResources(this.dtpMaintenanceInstallmentDate, "dtpMaintenanceInstallmentDate");
		((UltraWinEditorMaskedControlBase)this.dtpMaintenanceInstallmentDate).AlwaysInEditMode = true;
		this.dtpMaintenanceInstallmentDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpMaintenanceInstallmentDate).Name = "dtpMaintenanceInstallmentDate";
		this.dtpMaintenanceInstallmentDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.txtTotalRestAmount, "txtTotalRestAmount");
		((System.Windows.Forms.Control)(object)this.txtTotalRestAmount).Name = "txtTotalRestAmount";
		resources.ApplyResources(this.lblTotalRestAmount, "lblTotalRestAmount");
		this.lblTotalRestAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalRestAmount).Name = "lblTotalRestAmount";
		((ControlBase)this.lblTotalRestAmount).WrapText = false;
		resources.ApplyResources(this.txtRestAmount, "txtRestAmount");
		((System.Windows.Forms.Control)(object)this.txtRestAmount).Name = "txtRestAmount";
		resources.ApplyResources(this.lblRestAmount, "lblRestAmount");
		this.lblRestAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRestAmount).Name = "lblRestAmount";
		((ControlBase)this.lblRestAmount).WrapText = false;
		resources.ApplyResources(this.txtMaintenanceInstallment, "txtMaintenanceInstallment");
		((System.Windows.Forms.Control)(object)this.txtMaintenanceInstallment).Name = "txtMaintenanceInstallment";
		((TextEditorControlBase)this.txtMaintenanceInstallment).ValueChanged += new System.EventHandler(txt_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtMaintenanceInstallment).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblMaintenanceInstallment, "lblMaintenanceInstallment");
		this.lblMaintenanceInstallment.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMaintenanceInstallment).Name = "lblMaintenanceInstallment";
		((ControlBase)this.lblMaintenanceInstallment).WrapText = false;
		resources.ApplyResources(this.txtInstallmentPeriod, "txtInstallmentPeriod");
		((System.Windows.Forms.Control)(object)this.txtInstallmentPeriod).Name = "txtInstallmentPeriod";
		((TextEditorControlBase)this.txtInstallmentPeriod).ValueChanged += new System.EventHandler(txt_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtInstallmentPeriod).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInteger_KeyPress);
		resources.ApplyResources(this.cboFloor, "cboFloor");
		((TextEditorControlBase)this.cboFloor).AlwaysInEditMode = true;
		this.cboFloor.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboFloor).Name = "cboFloor";
		resources.ApplyResources(this.cboSample, "cboSample");
		((TextEditorControlBase)this.cboSample).AlwaysInEditMode = true;
		this.cboSample.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSample).Name = "cboSample";
		resources.ApplyResources(this.cboUnit, "cboUnit");
		((TextEditorControlBase)this.cboUnit).AlwaysInEditMode = true;
		this.cboUnit.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboUnit).Name = "cboUnit";
		resources.ApplyResources(this.lblSizeMeter, "lblSizeMeter");
		this.lblSizeMeter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSizeMeter).Name = "lblSizeMeter";
		resources.ApplyResources(this.dtpFinishingStartDate, "dtpFinishingStartDate");
		((UltraWinEditorMaskedControlBase)this.dtpFinishingStartDate).AlwaysInEditMode = true;
		this.dtpFinishingStartDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpFinishingStartDate).Name = "dtpFinishingStartDate";
		this.dtpFinishingStartDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.lblFinishingStartDate, "lblFinishingStartDate");
		this.lblFinishingStartDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFinishingStartDate).Name = "lblFinishingStartDate";
		((ControlBase)this.lblFinishingStartDate).WrapText = false;
		resources.ApplyResources(this.lblFinishingAmount, "lblFinishingAmount");
		this.lblFinishingAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFinishingAmount).Name = "lblFinishingAmount";
		((ControlBase)this.lblFinishingAmount).WrapText = false;
		resources.ApplyResources(this.txtFinishingAmount, "txtFinishingAmount");
		((System.Windows.Forms.Control)(object)this.txtFinishingAmount).Name = "txtFinishingAmount";
		resources.ApplyResources(this.lblFinishingInstallmentCount, "lblFinishingInstallmentCount");
		this.lblFinishingInstallmentCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFinishingInstallmentCount).Name = "lblFinishingInstallmentCount";
		((ControlBase)this.lblFinishingInstallmentCount).WrapText = false;
		resources.ApplyResources(this.txtFinishingInstallmentCount, "txtFinishingInstallmentCount");
		((System.Windows.Forms.Control)(object)this.txtFinishingInstallmentCount).Name = "txtFinishingInstallmentCount";
		((TextEditorControlBase)this.txtFinishingInstallmentCount).ValueChanged += new System.EventHandler(txt_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtFinishingInstallmentCount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInteger_KeyPress);
		resources.ApplyResources(this.lblFinishingInstallmentPeriod, "lblFinishingInstallmentPeriod");
		this.lblFinishingInstallmentPeriod.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFinishingInstallmentPeriod).Name = "lblFinishingInstallmentPeriod";
		((ControlBase)this.lblFinishingInstallmentPeriod).WrapText = false;
		resources.ApplyResources(this.txtFinishingInstallmentPeriod, "txtFinishingInstallmentPeriod");
		((System.Windows.Forms.Control)(object)this.txtFinishingInstallmentPeriod).Name = "txtFinishingInstallmentPeriod";
		((TextEditorControlBase)this.txtFinishingInstallmentPeriod).ValueChanged += new System.EventHandler(txt_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtFinishingInstallmentPeriod).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInteger_KeyPress);
		resources.ApplyResources(this.lblRoundingValue, "lblRoundingValue");
		this.lblRoundingValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRoundingValue).Name = "lblRoundingValue";
		((ControlBase)this.lblRoundingValue).WrapText = false;
		resources.ApplyResources(this.txtRoundingValue, "txtRoundingValue");
		((System.Windows.Forms.Control)(object)this.txtRoundingValue).Name = "txtRoundingValue";
		((TextEditorControlBase)this.txtRoundingValue).ValueChanged += new System.EventHandler(txtRoundingValue_ValueChanged);
		resources.ApplyResources(this.chkIsCanceled, "chkIsCanceled");
		((System.Windows.Forms.Control)(object)this.chkIsCanceled).Name = "chkIsCanceled";
		resources.ApplyResources(this.dtpCanceledDate, "dtpCanceledDate");
		((UltraWinEditorMaskedControlBase)this.dtpCanceledDate).AlwaysInEditMode = true;
		this.dtpCanceledDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpCanceledDate).Name = "dtpCanceledDate";
		resources.ApplyResources(this.lblCancelationDate, "lblCancelationDate");
		this.lblCancelationDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCancelationDate).Name = "lblCancelationDate";
		((ControlBase)this.lblCancelationDate).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpCanceledDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsCanceled);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSizeMeter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboUnit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSample);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboFloor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtFinishingInstallmentPeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtInstallmentPeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtMaintenanceInstallment);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCancelationDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMaintenanceInstallment);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMaintenanceInstallmentDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpMaintenanceInstallmentDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRoundingValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtFinishingAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRoundingValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalRestAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFinishingAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalRestAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRestAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRestAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblContractInstallmentDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpContractInstallmentDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtContractInstallment);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblContractInstallment);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFinishingStartDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReservationInstallmentDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFinishingStartDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpReservationInstallmentDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtReservationInstallment);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReservationInstallment);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSalesPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtUnitPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUnitPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFinishingType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboFinishingType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSize);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSize);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSample);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUnit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFloor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBuildings);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBuildings);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblProjects);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboProject);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalInstallment);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalInstallment);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFinishingInstallmentPeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInstallmentPeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtFinishingInstallmentCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFinishingInstallmentCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtInstallmentCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInstallmentCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Name = "frmContracts";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInstallmentCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtInstallmentCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFinishingInstallmentCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtFinishingInstallmentCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInstallmentPeriod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFinishingInstallmentPeriod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalInstallment, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalInstallment, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboProject, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblProjects, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBuildings, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBuildings, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFloor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUnit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSample, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSize, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSize, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboFinishingType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFinishingType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUnitPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtUnitPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSalesPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSalesPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReservationInstallment, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtReservationInstallment, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpReservationInstallmentDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpFinishingStartDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReservationInstallmentDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFinishingStartDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblContractInstallment, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtContractInstallment, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpContractInstallmentDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblContractInstallmentDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRestAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRestAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalRestAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFinishingAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalRestAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRoundingValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtFinishingAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRoundingValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpMaintenanceInstallmentDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMaintenanceInstallmentDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMaintenanceInstallment, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCancelationDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtMaintenanceInstallment, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtInstallmentPeriod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtFinishingInstallmentPeriod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboFloor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSample, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboUnit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSizeMeter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsCanceled, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpCanceledDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
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
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInstallmentCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalInstallment).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBuildings).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboProject).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSize).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboFinishingType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUnitPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalesPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtReservationInstallment).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpReservationInstallmentDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpContractInstallmentDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtContractInstallment).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpMaintenanceInstallmentDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalRestAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRestAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMaintenanceInstallment).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInstallmentPeriod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboFloor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSample).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFinishingStartDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFinishingAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFinishingInstallmentCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFinishingInstallmentPeriod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRoundingValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsCanceled).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCanceledDate).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
