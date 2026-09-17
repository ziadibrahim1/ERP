using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.HR;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.HR.Personal.Transactions;

public class frmEmployeesAdministrativeStructure : frmDetails
{
	private DataTable dtEmployees;

	private DataTable dtAdministrativeStructure;

	private DataTable dtEmployeeData;

	private ValueList vlAdministrativeStructure = new ValueList();

	private IContainer components = null;

	private UltraComboEditor cboEmployeeCode;

	public frmEmployeesAdministrativeStructure()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		((Control)(object)lblHeader).Text = (GlobalVariables.IsArabic ? "إسم الموظف" : "Employee");
	}

	public override void PrepareData()
	{
		dtEmployees = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboHeader, dtEmployees, "SubAccountID", "SubAccountName");
		GlobalFunctions.FillCombo(cboEmployeeCode, dtEmployees, "SubAccountID", "EmployeeNo");
		dtAdministrativeStructure = AdministrativeStructure.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlAdministrativeStructure.ValueListItems.Clear();
		for (int i = 0; i < dtAdministrativeStructure.Rows.Count; i++)
		{
			vlAdministrativeStructure.ValueListItems.Add(dtAdministrativeStructure.Rows[i]["AdministrativeStructureID"], dtAdministrativeStructure.Rows[i]["AdministrativeStructureName"].ToString());
		}
		dtDetails = EmployeesAdministrativeStructure.SelectBySubAccountID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeAdministrativeStructureID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdministrativeStructureID"].Header).Caption = (GlobalVariables.IsArabic ? "الهيكل الادارى" : "Administrative Structure");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdministrativeStructureID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdministrativeStructureID"].ValueList = (IValueList)(object)vlAdministrativeStructure;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdministrativeStructureID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromDate"].Header).Caption = (GlobalVariables.IsArabic ? "من تاريخ" : "From Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToDate"].Header).Caption = (GlobalVariables.IsArabic ? "الى تاريخ" : "To Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Note"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Note"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Note"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
	}

	public override void DisplayData()
	{
		base.DisplayData();
		((TextEditorControlBase)cboEmployeeCode).ValueChanged -= cboEmployeeCode_ValueChanged;
		cboEmployeeCode.SelectedIndex = cboHeader.SelectedIndex;
		((TextEditorControlBase)cboEmployeeCode).ValueChanged += cboEmployeeCode_ValueChanged;
		dtDetails = EmployeesAdministrativeStructure.SelectBySubAccountID(((TextEditorControlBase)cboHeader).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtEmployeeData = Employees.SelectBySubAccountIDWithOutImage(((TextEditorControlBase)cboHeader).Value.ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dtDetails.Rows.Count == 0 && dtEmployeeData.Rows.Count > 0)
		{
			DataRow dataRow = dtDetails.NewRow();
			dataRow["EmployeeAdministrativeStructureID"] = "-1";
			dataRow["SubAccountID"] = ((TextEditorControlBase)cboHeader).Value;
			dataRow["AdministrativeStructureID"] = dtEmployeeData.Rows[0]["AdministrativeStructureID"];
			dataRow["FromDate"] = dtEmployeeData.Rows[0]["HireDate"];
			dtDetails.Rows.Add(dataRow);
		}
		InitGrid();
	}

	public override bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["FromDate"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال تاريخ البداية", "Please Insert From Date");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["FromDate"]).Selected = true;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["AdministrativeStructureID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار الهيكل الادارى", "Please Select Administrative Structure");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["AdministrativeStructureID"]).Selected = true;
				return false;
			}
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
		{
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
			{
				if (k != j && ((((UltraGridBase)ULGData).Rows[k].Cells["ToDate"].Value != DBNull.Value && DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["FromDate"].Value.ToString()) >= DateTime.Parse(((UltraGridBase)ULGData).Rows[k].Cells["FromDate"].Value.ToString()) && DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["FromDate"].Value.ToString()) <= DateTime.Parse(((UltraGridBase)ULGData).Rows[k].Cells["ToDate"].Value.ToString())) || (((UltraGridBase)ULGData).Rows[k].Cells["ToDate"].Value != DBNull.Value && ((UltraGridBase)ULGData).Rows[j].Cells["ToDate"].Value != DBNull.Value && DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ToDate"].Value.ToString()) >= DateTime.Parse(((UltraGridBase)ULGData).Rows[k].Cells["FromDate"].Value.ToString()) && DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ToDate"].Value.ToString()) <= DateTime.Parse(((UltraGridBase)ULGData).Rows[k].Cells["ToDate"].Value.ToString())) || (((UltraGridBase)ULGData).Rows[k].Cells["ToDate"].Value != DBNull.Value && ((UltraGridBase)ULGData).Rows[j].Cells["ToDate"].Value != DBNull.Value && DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["FromDate"].Value.ToString()) <= DateTime.Parse(((UltraGridBase)ULGData).Rows[k].Cells["FromDate"].Value.ToString()) && DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ToDate"].Value.ToString()) >= DateTime.Parse(((UltraGridBase)ULGData).Rows[k].Cells["ToDate"].Value.ToString()))))
				{
					GlobalVariables.InformationMB.Show("هذا التاريخ واقع فى فترة من قبل", "this Date in Another Period");
					((GridItemBase)((UltraGridBase)ULGData).Rows[k]).Selected = true;
					return false;
				}
			}
		}
		return true;
	}

	public override void SaveData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value = ((TextEditorControlBase)cboHeader).Value.ToString();
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["EmployeeAdministrativeStructureID"].Value.ToString() + ",";
			}
			Main.SyncDeleteForUpdate("HR_EmployeesAdministrativeStructure", "SubAccountID", ((TextEditorControlBase)cboHeader).Value.ToString(), "EmployeeAdministrativeStructureID", text, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				DataView dataView = new DataView(dtDetails);
				dataView.Sort = " FromDate Desc ";
				DataRow dataRow = dataView.ToTable().Rows[0];
				EmployeesAdministrativeStructure.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
				if (dtEmployeeData == null || dtEmployeeData.Rows.Count == 0)
				{
					Employees.Insert_Update(dtEmployees.Select(" SubAccountID= " + ((TextEditorControlBase)cboHeader).Value.ToString())[0]["SubAccountNumber"].ToString(), "Null", ((TextEditorControlBase)cboHeader).Value.ToString(), "0", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", dataRow["AdministrativeStructureID"].ToString(), "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "0", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "0", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "1", "0", "0", "Null", "Null", "Null", "Null", "Null", "Null", "0", "Null", "0", "Null", "0", "0", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				}
				else
				{
					Employees.Insert_Update((dtEmployeeData.Rows[0]["EmployeeNo"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["EmployeeNo"].ToString(), (dtEmployeeData.Rows[0]["EmployeeOrder"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["EmployeeOrder"].ToString(), ((TextEditorControlBase)cboHeader).Value.ToString(), (dtEmployeeData.Rows[0]["IsSalesMan"] == DBNull.Value) ? "0" : (bool.Parse(dtEmployeeData.Rows[0]["IsSalesMan"].ToString()) ? "1" : "0"), "Null", (dtEmployeeData.Rows[0]["SalesCommissionRatio"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["SalesCommissionRatio"].ToString(), (dtEmployeeData.Rows[0]["CollectionCommissionRatio"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["CollectionCommissionRatio"].ToString(), (dtEmployeeData.Rows[0]["PersonalIDNo"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["PersonalIDNo"].ToString(), (dtEmployeeData.Rows[0]["PersonalIDIssueDate"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["PersonalIDIssueDate"].ToString(), (dtEmployeeData.Rows[0]["PersonalIDExpireDate"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["PersonalIDExpireDate"].ToString(), (dtEmployeeData.Rows[0]["BirthDate"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["BirthDate"].ToString(), (dtEmployeeData.Rows[0]["GenderID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["GenderID"].ToString(), (dtEmployeeData.Rows[0]["CountryID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["CountryID"].ToString(), (dtEmployeeData.Rows[0]["CityID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["CityID"].ToString(), (dtEmployeeData.Rows[0]["AreaID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["AreaID"].ToString(), (dtEmployeeData.Rows[0]["Address"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["Address"].ToString(), (dtEmployeeData.Rows[0]["NationalityID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["NationalityID"].ToString(), (dtEmployeeData.Rows[0]["MilitaryServiceID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["MilitaryServiceID"].ToString(), (dtEmployeeData.Rows[0]["PostponedToDate"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["PostponedToDate"].ToString(), (dtEmployeeData.Rows[0]["SocialStatusID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["SocialStatusID"].ToString(), (dtEmployeeData.Rows[0]["QualificationNameID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["QualificationNameID"].ToString(), (dtEmployeeData.Rows[0]["UniversityID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["UniversityID"].ToString(), (dtEmployeeData.Rows[0]["SectionNameID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["SectionNameID"].ToString(), (dtEmployeeData.Rows[0]["QualificationYear"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["QualificationYear"].ToString(), (dtEmployeeData.Rows[0]["SectionNameNotes"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["SectionNameNotes"].ToString(), (dtEmployeeData.Rows[0]["EMail"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["EMail"].ToString(), (dtEmployeeData.Rows[0]["StateID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["StateID"].ToString(), dataRow["AdministrativeStructureID"].ToString(), (dtEmployeeData.Rows[0]["AdministrativeLevelID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["AdministrativeLevelID"].ToString(), (dtEmployeeData.Rows[0]["DegreeID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["DegreeID"].ToString(), (dtEmployeeData.Rows[0]["PositionID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["PositionID"].ToString(), (dtEmployeeData.Rows[0]["DirectManagerID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["DirectManagerID"].ToString(), (dtEmployeeData.Rows[0]["HireDate"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["HireDate"].ToString(), (dtEmployeeData.Rows[0]["EmployeeEndDate"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["EmployeeEndDate"].ToString(), (dtEmployeeData.Rows[0]["LeavingWorkReasonID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["LeavingWorkReasonID"].ToString(), (dtEmployeeData.Rows[0]["LeavingWorkNotes"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["LeavingWorkNotes"].ToString(), (dtEmployeeData.Rows[0]["ContractFromDate"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["ContractFromDate"].ToString(), (dtEmployeeData.Rows[0]["ContractToDate"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["ContractToDate"].ToString(), (dtEmployeeData.Rows[0]["Notes"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["Notes"].ToString(), (dtEmployeeData.Rows[0]["HasPassport"] == DBNull.Value) ? "0" : (bool.Parse(dtEmployeeData.Rows[0]["HasPassport"].ToString()) ? "1" : "0"), (dtEmployeeData.Rows[0]["PassportNo"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["PassportNo"].ToString(), (dtEmployeeData.Rows[0]["PassportExpireDate"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["PassportExpireDate"].ToString(), (dtEmployeeData.Rows[0]["ApplicantID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["ApplicantID"].ToString(), (dtEmployeeData.Rows[0]["DrivingLicenseID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["DrivingLicenseID"].ToString(), (dtEmployeeData.Rows[0]["DrivingLicenseExpireDate"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["DrivingLicenseExpireDate"].ToString(), (dtEmployeeData.Rows[0]["BankID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["BankID"].ToString(), (dtEmployeeData.Rows[0]["BankAccountNo"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["BankAccountNo"].ToString(), (dtEmployeeData.Rows[0]["FirstInssuranceDate"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["FirstInssuranceDate"].ToString(), (dtEmployeeData.Rows[0]["InssuranceEndDate"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["InssuranceEndDate"].ToString(), (dtEmployeeData.Rows[0]["SocialInssuranceNo"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["SocialInssuranceNo"].ToString(), (dtEmployeeData.Rows[0]["SocialInssuranceValue"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["SocialInssuranceValue"].ToString(), (dtEmployeeData.Rows[0]["InclusiveHealthInsuranceValue"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["InclusiveHealthInsuranceValue"].ToString(), (dtEmployeeData.Rows[0]["SocialInssuranceOfficeID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["SocialInssuranceOfficeID"].ToString(), (dtEmployeeData.Rows[0]["InssuranceDate"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["InssuranceDate"].ToString(), (dtEmployeeData.Rows[0]["HasPrivateCar"] == DBNull.Value) ? "0" : (bool.Parse(dtEmployeeData.Rows[0]["HasPrivateCar"].ToString()) ? "1" : "0"), (dtEmployeeData.Rows[0]["WorkOfficePermissionCode"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["WorkOfficePermissionCode"].ToString(), (dtEmployeeData.Rows[0]["WorkOfficeFromDate"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["WorkOfficeFromDate"].ToString(), (dtEmployeeData.Rows[0]["WorkOfficeToDate"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["WorkOfficeToDate"].ToString(), (dtEmployeeData.Rows[0]["WorkOfficeSendDate"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["WorkOfficeSendDate"].ToString(), (dtEmployeeData.Rows[0]["WorkOfficeIsSend"] == DBNull.Value) ? "0" : (bool.Parse(dtEmployeeData.Rows[0]["WorkOfficeIsSend"].ToString()) ? "1" : "0"), (dtEmployeeData.Rows[0]["CustomVacationBalance"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["CustomVacationBalance"].ToString(), (dtEmployeeData.Rows[0]["OccasionVacationBalance"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["OccasionVacationBalance"].ToString(), (dtEmployeeData.Rows[0]["CustomVacationBalanceLastYear"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["CustomVacationBalanceLastYear"].ToString(), (dtEmployeeData.Rows[0]["OccasionVacationBalanceLastYear"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["OccasionVacationBalanceLastYear"].ToString(), (dtEmployeeData.Rows[0]["IsMonthlySalary"] == DBNull.Value) ? "0" : (bool.Parse(dtEmployeeData.Rows[0]["IsMonthlySalary"].ToString()) ? "1" : "0"), (dtEmployeeData.Rows[0]["InSalaryTax"] == DBNull.Value) ? "0" : (bool.Parse(dtEmployeeData.Rows[0]["InSalaryTax"].ToString()) ? "1" : "0"), (dtEmployeeData.Rows[0]["InServicePercent"] == DBNull.Value) ? "0" : (bool.Parse(dtEmployeeData.Rows[0]["InServicePercent"].ToString()) ? "1" : "0"), (dtEmployeeData.Rows[0]["ServicePercentFromDate"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["ServicePercentFromDate"].ToString(), (dtEmployeeData.Rows[0]["StartBasicSalary"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["StartBasicSalary"].ToString(), (dtEmployeeData.Rows[0]["StartVariantSalary"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["StartVariantSalary"].ToString(), (dtEmployeeData.Rows[0]["StartSalary"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["StartSalary"].ToString(), (dtEmployeeData.Rows[0]["CurrentBasicSalary"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["CurrentBasicSalary"].ToString(), (dtEmployeeData.Rows[0]["CurrentVariantSalary"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["CurrentVariantSalary"].ToString(), (dtEmployeeData.Rows[0]["NotInsuranceVariantSalary"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["NotInsuranceVariantSalary"].ToString(), (dtEmployeeData.Rows[0]["NetCurrentSalary"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["NetCurrentSalary"].ToString(), (dtEmployeeData.Rows[0]["IsFixedSalaryTax"] == DBNull.Value) ? "0" : (bool.Parse(dtEmployeeData.Rows[0]["IsFixedSalaryTax"].ToString()) ? "1" : "0"), (dtEmployeeData.Rows[0]["FixedSalaryTaxValue"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["FixedSalaryTaxValue"].ToString(), (dtEmployeeData.Rows[0]["IsFixedTaxPercentage"] == DBNull.Value) ? "0" : (bool.Parse(dtEmployeeData.Rows[0]["IsFixedTaxPercentage"].ToString()) ? "1" : "0"), (dtEmployeeData.Rows[0]["IsSalaryAtEndOfMonth"] == DBNull.Value) ? "0" : dtEmployeeData.Rows[0]["IsSalaryAtEndOfMonth"].ToString(), (dtEmployeeData.Rows[0]["HealthInsuranceTypeID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["HealthInsuranceTypeID"].ToString(), (dtEmployeeData.Rows[0]["HealthInsuranceStartDate"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["HealthInsuranceStartDate"].ToString(), (dtEmployeeData.Rows[0]["HealthInsuranceEndDate"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["HealthInsuranceEndDate"].ToString(), (dtEmployeeData.Rows[0]["ExtraTimeRuleID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["ExtraTimeRuleID"].ToString(), (dtEmployeeData.Rows[0]["DelayRuleID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["DelayRuleID"].ToString(), (dtEmployeeData.Rows[0]["AbsentRuleID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["AbsentRuleID"].ToString(), (dtEmployeeData.Rows[0]["PenaltyRuleID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["PenaltyRuleID"].ToString(), (dtEmployeeData.Rows[0]["VacationRuleID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["VacationRuleID"].ToString(), (dtEmployeeData.Rows[0]["LeavePermissionRuleID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["LeavePermissionRuleID"].ToString(), (dtEmployeeData.Rows[0]["FeedingRuleID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["FeedingRuleID"].ToString(), (dtEmployeeData.Rows[0]["SalaryListID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["SalaryListID"].ToString(), (dtEmployeeData.Rows[0]["CurrencyID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["CurrencyID"].ToString(), (dtEmployeeData.Rows[0]["SalaryAccountID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["SalaryAccountID"].ToString(), (dtEmployeeData.Rows[0]["AdvanceAccountID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["AdvanceAccountID"].ToString(), (dtEmployeeData.Rows[0]["PenaltyAccountID"] == DBNull.Value) ? "Null" : dtEmployeeData.Rows[0]["PenaltyAccountID"].ToString(), (dtEmployeeData.Rows[0]["Deleted"] == DBNull.Value) ? "0" : (bool.Parse(dtEmployeeData.Rows[0]["Deleted"].ToString()) ? "1" : "0"), GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				}
			}
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
			SaveError = true;
		}
	}

	private void cboEmployeeCode_ValueChanged(object sender, EventArgs e)
	{
		if (cboEmployeeCode.SelectedIndex > -1)
		{
			cboHeader.SelectedIndex = cboEmployeeCode.SelectedIndex;
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "FromDate" && ((UltraGridBase)ULGData).ActiveRow.Cells["FromDate"].Value == DBNull.Value && ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 1)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["FromDate"].Value = DateTime.Parse(((UltraGridBase)ULGData).Rows[((UltraGridBase)ULGData).ActiveRow.Index - 1].Cells["ToDate"].Value.ToString()).AddDays(1.0);
		}
	}

	public override void btnHeaderSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Employees("-1", "-1", IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboHeader).Value = num;
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.cboEmployeeCode = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboEmployeeCode).BeginInit();
		base.SuspendLayout();
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		((AppearanceBase)val8).FontData.BoldAsString = "True";
		((AppearanceBase)val8).FontData.Name = "Arial";
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		((TextEditorControlBase)base.cboHeader).Appearance = (AppearanceBase)(object)val8;
		base.cboHeader.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboHeader.DropDownListAlignment = (DropDownListAlignment)2;
		((System.Windows.Forms.Control)(object)base.cboHeader).Location = new System.Drawing.Point(501, 68);
		((System.Windows.Forms.Control)(object)base.cboHeader).Size = new System.Drawing.Size(234, 24);
		((AppearanceBase)val9).FontData.BoldAsString = "True";
		((AppearanceBase)val9).FontData.Name = "Arial";
		((ControlBase)base.lblHeader).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)base.lblHeader).Location = new System.Drawing.Point(281, 72);
		((System.Windows.Forms.Control)(object)base.lblHeader).Size = new System.Drawing.Size(52, 17);
		((TextEditorControlBase)this.cboEmployeeCode).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.cboEmployeeCode).Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.cboEmployeeCode.AutoCompleteMode = (AutoCompleteMode)3;
		((System.Windows.Forms.Control)(object)this.cboEmployeeCode).Location = new System.Drawing.Point(398, 67);
		((System.Windows.Forms.Control)(object)this.cboEmployeeCode).Name = "cboEmployeeCode";
		((System.Windows.Forms.Control)(object)this.cboEmployeeCode).Size = new System.Drawing.Size(97, 25);
		((System.Windows.Forms.Control)(object)this.cboEmployeeCode).TabIndex = 604;
		((TextEditorControlBase)this.cboEmployeeCode).ValueChanged += new System.EventHandler(cboEmployeeCode_ValueChanged);
		base.ClientSize = new System.Drawing.Size(1000, 500);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboEmployeeCode);
		base.Name = "frmEmployeesAdministrativeStructure";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveAndClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboEmployeeCode, 0);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboEmployeeCode).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
