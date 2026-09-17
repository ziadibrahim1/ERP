using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.HR;
using BusinessLayer.Privilege;
using BusinessLayer.SafesAndBanks;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Accounting.MasterData;
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

namespace ERP.HR.Personal.MasterData;

public class frmEmployeeTree : frmTree2
{
	private string ApplicantID = "";

	private bool ImageChanged = false;

	private bool CanModAdministrativeStructure = false;

	private DataTable dtStores;

	private DataTable dtReports;

	private DataTable dtAccounts;

	private DataTable dtCountries;

	private DataTable dtLeavingWorkReasons;

	private DataTable dtCities;

	private DataTable dtAreas;

	private DataTable dtGender;

	private DataTable dtNationality;

	private DataTable dtMilitaryService;

	private DataTable dtSocialStatus;

	private DataTable dtQualification;

	private DataTable dtSections;

	private DataTable dtUniversty;

	private DataTable dtState;

	private DataTable dtAdministrativeStructure;

	private DataTable dtAdministrativeLevel;

	private DataTable dtDegree;

	private DataTable dtPosition;

	private DataTable dtDirectManager;

	private DataTable dtBank;

	private DataTable dtSocialInssuranceOffice;

	private DataTable dtBranch;

	private DataTable dtContacts;

	private DataTable dtCertificates;

	private DataTable dtDocuments;

	private DataTable dtDocumentsTypes;

	private DataTable dtFamilyRelatives;

	private DataTable dtEmployeeFamilyRelatives;

	private DataTable dtAllownces;

	private DataTable dtEmployeeAllownces;

	private DataTable dtYearIncreases;

	private DataTable dtEmployeeYearIncreases;

	private DataTable dtMotivations;

	private DataTable dtEmployeeMotivations;

	private DataTable dtDeductions;

	private DataTable dtEmployeeDeductions;

	private DataTable dtSubAccDetails;

	private DataTable dtSalaryLists;

	private DataTable dtCurrency;

	private DataTable dtLeaveRule;

	private DataTable dtHealthInsuranceTypes;

	private DataTable dtExtraTimeRule;

	private DataTable dtLateRule;

	private DataTable dtAbsentRule;

	private DataTable dtPenalityRule;

	private DataTable dtVacationRule;

	private DataTable dtFeedingRule;

	private ValueList vlDocumentsTypes = new ValueList();

	private ValueList vlAllownces = new ValueList();

	private ValueList vlDeductions = new ValueList();

	private ValueList vlYearIncreases = new ValueList();

	private ValueList vlMotivations = new ValueList();

	private ValueList vlFamilyRelatives = new ValueList();

	private IContainer components = null;

	public UltraButton btnItemsSearch;

	private UltraTextEditor txtItems;

	public UltraTree TreeAccounts;

	public UltraTabControl tabItemType;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl tabItem;

	private UltraComboEditor cboState;

	private UltraComboEditor cboGender;

	private UltraLabel lblState;

	private UltraLabel lblGender;

	private UltraTabPageControl tabService;

	private UltraTabPageControl tabRecipe;

	public UltraGrid ULGContacts;

	private UltraLabel lblAccounts;

	private UltraTextEditor txtAddress;

	private UltraLabel lblAddress;

	private UltraLabel lblBirthDate;

	private UltraDateTimeEditor dtpBirthDate;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraComboEditor cboSocialStatus;

	private UltraComboEditor cboMilitaryService;

	private UltraLabel lblSocialStatus;

	private UltraComboEditor cboNationality;

	private UltraLabel lblMilitaryService;

	private UltraLabel lblNationality;

	private UltraTextEditor txtPersonalIDNo;

	private UltraLabel lblPersonalIDNo;

	private UltraComboEditor cboCity;

	private UltraLabel lblCity;

	private UltraTabPageControl ultraTabPageControl1;

	public UltraGrid ULGCetificates;

	private UltraTabPageControl ultraTabPageControl2;

	public UltraGrid ULGDocuments;

	private UltraButton btnPic;

	private UltraPictureBox picEmployee;

	private OpenFileDialog ofdItemPic;

	private UltraDateTimeEditor dtpIDExpireDate;

	private UltraDateTimeEditor dtpIDIssueDate;

	private UltraLabel lblIDExpireDate;

	private UltraLabel lblIDIssueDate;

	private UltraComboEditor cboCountry;

	private UltraLabel lblCountry;

	private UltraComboEditor cboArea;

	private UltraLabel lblArea;

	private UltraLabel lblQualificationYear;

	private UltraComboEditor cboQualificationYear;

	private UltraComboEditor cboQualification;

	private UltraTextEditor txtSectionNameNotes;

	private UltraLabel lblQualification;

	private UltraLabel lblSectionNameNotes;

	private UltraTextEditor txtEMail;

	private UltraLabel lblEMail;

	private UltraCheckEditor chkHasPrivateCar;

	private UltraCheckEditor chkHasPassport;

	private UltraTextEditor txtEmployeeNo;

	private UltraTextEditor txtPassportNo;

	private UltraTextEditor txtDrivingLicenseID;

	private UltraLabel lblEmployeeNo;

	private UltraLabel lblPassportNo;

	private UltraLabel lblDrivingLicenseID;

	private UltraDateTimeEditor dtpPassportExpireDate;

	private UltraLabel lblPassportExpireDate;

	private UltraTabPageControl ultraTabPageControl3;

	public UltraTree treeAdministrativeStructure;

	private UltraDateTimeEditor dtpHireDate;

	private UltraLabel lblAdministrativeStructure;

	private UltraLabel lblHireDate;

	private UltraComboEditor cboDirectManager;

	private UltraLabel lblDirectManager;

	private UltraComboEditor cboDegree;

	private UltraLabel lblDegreeID;

	private UltraComboEditor cboPosition;

	private UltraLabel lblPosition;

	private UltraComboEditor cboAdministrativeLevel;

	private UltraLabel lblAdministrativeLevel;

	private UltraTabPageControl ultraTabPageControl4;

	private UltraTextEditor txtNetCurrentSalary;

	private UltraTextEditor txtStartSalary;

	private UltraLabel lblNetCurrentSalary;

	private UltraLabel lblStartSalary;

	private UltraTabPageControl ultraTabPageControl5;

	private UltraLabel lblToDate;

	private UltraLabel lblWorkOfficeFromDate;

	private UltraLabel lblInssuranceDate;

	private UltraLabel lblFirstInssuranceDate;

	private UltraComboEditor cboSocialInssuranceOffice;

	private UltraLabel lblSocialInssuranceOffice;

	private UltraTextEditor txtWorkOfficePermissionCode;

	private UltraLabel lblWorkOfficePermissionCode;

	private UltraTextEditor txtSocialInssuranceNo;

	private UltraLabel lblSocialInssuranceNo;

	private UltraDateTimeEditor dtpWorkOfficeToDate;

	private UltraDateTimeEditor dtpWorkOfficeFromDate;

	private UltraDateTimeEditor dtpInssuranceDate;

	private UltraDateTimeEditor dtpFirstInssuranceDate;

	private UltraComboEditor cboBranch;

	private UltraLabel lblBranch;

	private UltraCheckEditor chkIsSalesMan;

	private UltraComboEditor cboSectionName;

	private UltraComboEditor cboUniversity;

	private UltraLabel lblSectionName;

	private UltraLabel lblUniversity;

	private UltraDateTimeEditor dtpEndDate;

	private UltraLabel lblEndDate;

	private UltraCheckEditor chkWorkOfficeIsSend;

	private UltraLabel lblSendDate;

	private UltraDateTimeEditor dtpWorkOfficeSendDate;

	private UltraTextEditor txtCurrentBasicSalary;

	private UltraTextEditor txtCurrentVariantSalary;

	private UltraTextEditor txtStartBasicSalary;

	private UltraLabel ultraLabel7;

	private UltraTextEditor txtStartVariantSalary;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel3;

	private UltraComboEditor cboHealthInsuranceType;

	private UltraLabel ultraLabel8;

	public UltraGrid ULGAllownces;

	private UltraLabel ultraLabel14;

	public UltraGrid ULGDeductions;

	private UltraLabel lblDeductions;

	private UltraTabPageControl ultraTabPageControl6;

	private UltraLabel ultraLabel1;

	private UltraDateTimeEditor dtpServicePercentFromDate;

	private UltraCheckEditor chkInServicePercent;

	private UltraCheckEditor chkInSalaryTax;

	private UltraCheckEditor chkIsMonthlySalary;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel2;

	private UltraTextEditor txtCollectionCommission;

	private UltraTextEditor txtSalesCommission;

	private UltraLabel lblCollectionCommission;

	private UltraLabel lblSalesCommissionRatio;

	private UltraComboEditor cboSalaryList;

	private UltraLabel lblSalaryList;

	private UltraComboEditor cboLeavePermissionRule;

	private UltraLabel lblLeaveRule;

	private UltraComboEditor cboVacationRule;

	private UltraLabel ultraLabel13;

	private UltraComboEditor cboPenalityRule;

	private UltraLabel ultraLabel12;

	private UltraComboEditor cboAbsenceRule;

	private UltraLabel ultraLabel11;

	private UltraComboEditor cboLateRule;

	private UltraLabel ultraLabel10;

	private UltraComboEditor cboExtraTimeRule;

	private UltraLabel ultraLabel9;

	private UltraComboEditor cboBank;

	private UltraLabel lblBank;

	private UltraTextEditor txtBankAccountNo;

	private UltraLabel lblBankAccountNo;

	private UltraComboEditor cboSalaryAccount;

	private UltraLabel ultraLabel17;

	private UltraComboEditor cboPenaltyAccount;

	private UltraLabel ultraLabel16;

	private UltraComboEditor cboAdvanceAccount;

	private UltraLabel ultraLabel15;

	private UltraTextEditor txtFixedSalaryValue;

	private UltraCheckEditor chkIsFixedSalaryTax;

	private UltraCheckEditor chkSalaryAtEndOFMonth;

	private UltraTextEditor txtOrder;

	private UltraLabel ultraLabel18;

	private UltraComboEditor cboFeedingRule;

	private UltraLabel lblFeedingRule;

	private UltraComboEditor cboCurrency;

	private UltraLabel lblCurrency;

	public UltraGrid ULGYearIncrease;

	private UltraLabel ultraLabel19;

	private UltraLabel ultraLabel20;

	public UltraGrid ULGMotivation;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem changeParentTSMenu;

	private ToolStripMenuItem setAsGroupToolStripMenuItem;

	private ToolStripMenuItem setAsSubAccountToolStripMenuItem;

	private ToolStripMenuItem modifyGroupEmployeesToolStripMenuItem;

	private UltraTextEditor txtNotInsuranceVariantSalary;

	private UltraLabel ultraLabel21;

	private UltraCheckEditor chkIsFixedTaxPercentage;

	private UltraDateTimeEditor dtpPostponedToDate;

	private UltraLabel ultraLabel22;

	private UltraDateTimeEditor dtpDrivingLicenseExpireDate;

	private UltraLabel ultraLabel23;

	private UltraDateTimeEditor dtpContractToDate;

	private UltraDateTimeEditor dtpContractFromDate;

	private UltraLabel lblContractToDate;

	private UltraLabel lblContractFromDate;

	private UltraTextEditor txtLeavingWorkNotes;

	private UltraLabel ultraLabel25;

	private UltraComboEditor cboLeavingWorkReason;

	private UltraLabel ultraLabel24;

	private UltraLabel ultraLabel26;

	private UltraLabel ultraLabel27;

	private UltraDateTimeEditor dtpHealthInsuranceEndDate;

	private UltraDateTimeEditor dtpHealthInsuranceStartDate;

	private UltraLabel lblInsuranceEndDate;

	private UltraDateTimeEditor dtpInssuranceEndDate;

	public UltraButton btnStoreSearch;

	private UltraComboEditor cboSalesManDefaultStore;

	private UltraLabel ultraLabel28;

	private UltraTextEditor txtSocialInsuranceValue;

	private UltraLabel ultraLabel29;

	private UltraTextEditor txtInclusiveHealthInsuranceValue;

	private UltraLabel lblInclusiveHealthInsuranceValue;

	public UltraGrid ULGFamilyRelativesHealthInsurance;

	private GroupBox groupBox1;

	public frmEmployeeTree()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Expected O, but got Unknown
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Expected O, but got Unknown
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		InitializeComponent();
		IDCol = "SubAccountID";
		NoCol = "SubAccountNumber";
		NameCol = "SubAccountNameAr";
		NameEnCol = "SubAccountNameEn";
		ParentIDCol = "ParentID";
		IsMainCol = "IsMain";
		AdditionalCol1 = "SubAccountTypeID";
		ItemLevelCol = "LevelID";
		TableName = "A_SubAccounts";
		LevelsTable = "A_SubAccounts_Levels";
		LevelsCol = "LevelID";
		LevelsWidthCol = "Width";
		AllowAddRoot = false;
	}

	public override void PrepareData()
	{
		if (TableName != "" && LevelsTable != "")
		{
			dtLevels = Main.SyncExecuteQuery_DataTable("TreeLevels_Select '" + LevelsCol + "','" + LevelsWidthCol + "','" + LevelsTable + "'");
			dtChart = SubAccounts.FillTreeBySubAccountTypeIDs(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.BranchIDs, IsFromServer: true);
			treeChart.Nodes.Clear();
			if (dtChart.Rows.Count > 0)
			{
				FillTree("0");
			}
		}
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtAccounts = Accounts.Select("-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		TreeFunctions.FillTree(TreeAccounts, dtAccounts, "ParentID", "AccountID", GlobalVariables.IsArabic ? "AccountNameAr" : "AccountNameEn", "AccountNumber", "IsMain");
		dtGender = Gender.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboGender, dtGender, "GenderID", "GenderName");
		dtCountries = Countries.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCountry, dtCountries, "CountryID", "CountryName");
		dtCities = Cities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCity, dtCities, "CityID", "CityName");
		dtAreas = Areas.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboArea, dtAreas, "AreaID", "AreaName");
		dtNationality = Nationalities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboNationality, dtNationality, "NationalityID", "NationalityName");
		dtLeavingWorkReasons = LeaveWorkReasons.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboLeavingWorkReason, dtLeavingWorkReasons, "LeavingWorkReasonID", "LeavingWorkReasonName");
		dtMilitaryService = MilitaryService.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboMilitaryService, dtMilitaryService, "MilitaryServiceID", "MilitaryServiceName");
		dtSocialStatus = SocialStatus.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSocialStatus, dtSocialStatus, "SocialStatusID", "SocialStatusName");
		dtUniversty = Universities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboUniversity, dtUniversty, "UniversityID", "UniversityName");
		dtQualification = QualificationNames.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboQualification, dtQualification, "QualificationNameID", "QualificationName");
		dtSections = SectionsNames.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSectionName, dtSections, "SectionNameID", "SectionName");
		dtState = States.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboState, dtState, "StateID", "StateName");
		dtHealthInsuranceTypes = HealthInsuranceTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboHealthInsuranceType, dtHealthInsuranceTypes, "HealthInsuranceTypeID", "HealthInsuranceTypeName");
		dtExtraTimeRule = ExtraTimeRules.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboExtraTimeRule, dtExtraTimeRule, "ExtraTimeRuleID", "ExtraTimeRuleName");
		dtLateRule = DelayRules.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboLateRule, dtLateRule, "DelayRuleID", "DelayRuleName");
		dtAbsentRule = AbsentRules.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboAbsenceRule, dtAbsentRule, "AbsentRuleID", "AbsentRuleName");
		dtPenalityRule = PenaltyRules.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboPenalityRule, dtPenalityRule, "PenaltyRuleID", "PenaltyRuleName");
		dtVacationRule = VacationRules.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboVacationRule, dtVacationRule, "VacationRuleID", "VacationRuleName");
		dtFeedingRule = FeedingRules.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboFeedingRule, dtFeedingRule, "FeedingRuleID", "FeedingRuleName");
		dtAdministrativeStructure = AdministrativeStructure.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		TreeFunctions.FillTree(treeAdministrativeStructure, dtAdministrativeStructure, "ParentID", "AdministrativeStructureID", GlobalVariables.IsArabic ? "AdministrativeStructureNameAr" : "AdministrativeStructureNameEn", "AdministrativeStructureNumber", "IsMain");
		dtAdministrativeLevel = AdministrativeLevels.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboAdministrativeLevel, dtAdministrativeLevel, "AdministrativeLevelID", "AdministrativeLevelName");
		dtDegree = Degrees.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboDegree, dtDegree, "DegreeID", "DegreeName");
		dtPosition = Positions.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboPosition, dtPosition, "PositionID", "PositionName");
		DataView dataView = new DataView(dtChart);
		dataView.RowFilter = "IsMain=0";
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		dtDirectManager = dataView.ToTable();
		GlobalFunctions.FillCombo(cboDirectManager, dtDirectManager, "SubAccountID", GlobalVariables.IsArabic ? "SubAccountNameAr" : "SubAccountNameEn");
		dtBank = Banks.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboBank, dtBank, "BankID", "BankName");
		dtSocialInssuranceOffice = SocialInssuranceOffices.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSocialInssuranceOffice, dtSocialInssuranceOffice, "SocialInssuranceOfficeID", "SocialInssuranceOfficeName");
		dtBranch = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboBranch, dtBranch, "BranchID", "BranchName");
		dtSalaryLists = SalaryLists.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSalaryList, dtSalaryLists, "SalaryListID", "SalaryListName");
		dtCurrency = Currency.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", GlobalVariables.IsArabic ? "CurrencyNameAr" : "CurrencyNameEn");
		dtLeaveRule = LeavePermissionRule.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboLeavePermissionRule, dtLeaveRule, "LeavePermissionRuleID", "LeavePermissionRuleName");
		dtDocumentsTypes = DocumentsTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlDocumentsTypes.ValueListItems.Clear();
		for (int i = 0; i < dtDocumentsTypes.Rows.Count; i++)
		{
			vlDocumentsTypes.ValueListItems.Add(dtDocumentsTypes.Rows[i]["DocumentTypeID"], dtDocumentsTypes.Rows[i]["DocumentTypeName"].ToString());
		}
		dtFamilyRelatives = BusinessLayer.HR.FamilyRelatives.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlFamilyRelatives.ValueListItems.Clear();
		for (int j = 0; j < dtFamilyRelatives.Rows.Count; j++)
		{
			vlFamilyRelatives.ValueListItems.Add(dtFamilyRelatives.Rows[j]["FamilyRelativeID"], dtFamilyRelatives.Rows[j]["FamilyRelativeName"].ToString());
		}
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSalesManDefaultStore, dtStores, "StoreID", "StoreName");
		dtAllownces = Allowances.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlAllownces.ValueListItems.Clear();
		for (int k = 0; k < dtAllownces.Rows.Count; k++)
		{
			vlAllownces.ValueListItems.Add(dtAllownces.Rows[k]["AllowanceID"], dtAllownces.Rows[k]["AllowanceName"].ToString());
		}
		dtDeductions = Deductions.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlDeductions.ValueListItems.Clear();
		for (int l = 0; l < dtDeductions.Rows.Count; l++)
		{
			vlDeductions.ValueListItems.Add(dtDeductions.Rows[l]["DeductionID"], dtDeductions.Rows[l]["DeductionName"].ToString());
		}
		dtYearIncreases = YearIncreases.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlYearIncreases.ValueListItems.Clear();
		for (int m = 0; m < dtYearIncreases.Rows.Count; m++)
		{
			vlYearIncreases.ValueListItems.Add(dtYearIncreases.Rows[m]["YearIncreaseID"], dtYearIncreases.Rows[m]["YearIncreaseName"].ToString());
		}
		dtMotivations = Motivations.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlMotivations.ValueListItems.Clear();
		for (int n = 0; n < dtMotivations.Rows.Count; n++)
		{
			vlMotivations.ValueListItems.Add(dtMotivations.Rows[n]["MotivationID"], dtMotivations.Rows[n]["MotivationName"].ToString());
		}
		for (int num = DateTime.Now.Year; num >= 1950; num--)
		{
			cboQualificationYear.Items.Add((object)num.ToString(), num.ToString());
		}
		dtSubAccDetails = SubAccounts_Details.SelectBySubAccountIDWithAccountName("0", "1", IsFromServer: true);
		dtContacts = EmployeesContacts.SelectBySubAccountID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtEmployeeFamilyRelatives = EmployeesFamilyRelativesHealthInsurance.SelectBySubAccountID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtDocuments = EmployeesDocuments.SelectBySubAccountIDWithoutImage("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtCertificates = EmployeesCertificates.SelectBySubAccountID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtEmployeeAllownces = EmployeesAllowances.SelectBySubAccountID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtEmployeeDeductions = EmployeesDeductions.SelectBySubAccountID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtEmployeeYearIncreases = EmployeeYearIncreases.SelectBySubAccountID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtEmployeeMotivations = EmployeesMotivations.SelectBySubAccountID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGridContacts();
		InitGridDocuments();
		InitGridEmpRelatives();
		InitGridCertificates();
		InitGridAllownces();
		InitGridDeductions();
		InitGridYearIncreases();
		InitGridMotivations();
	}

	public override void ClearControls()
	{
		base.ClearControls();
		ImageChanged = false;
		if (Adding)
		{
			treeAdministrativeStructure.CollapseAll();
			TreeFunctions.SetAllTreeNodesCheckState(Checked: false, treeAdministrativeStructure);
			((Control)(object)txtCode).Text = SubAccounts.GetCode((SelectedNode == null) ? "Null" : ((KeyedSubObjectBase)SelectedNode).Key, IsFromServer: true);
			((Control)(object)txtEmployeeNo).Text = Employees.GetCode(IsFromServer: true);
			((Control)(object)txtOrder).Text = "";
			((Control)(object)txtLeavingWorkNotes).Text = "";
			picEmployee.Image = null;
			dtpBirthDate.Value = DBNull.Value;
			dtpPostponedToDate.Value = DBNull.Value;
			dtpDrivingLicenseExpireDate.Value = DBNull.Value;
			dtpEndDate.Value = DBNull.Value;
			dtpFirstInssuranceDate.Value = DBNull.Value;
			dtpHireDate.ValueChanged -= dtpHireDate_ValueChanged;
			dtpContractFromDate.ValueChanged -= dtpContractFromDate_ValueChanged;
			dtpHireDate.Value = DBNull.Value;
			dtpContractFromDate.Value = DBNull.Value;
			dtpContractToDate.Value = DBNull.Value;
			dtpHireDate.ValueChanged -= dtpHireDate_ValueChanged;
			dtpContractFromDate.ValueChanged -= dtpContractFromDate_ValueChanged;
			dtpIDExpireDate.Value = DBNull.Value;
			dtpIDIssueDate.Value = DBNull.Value;
			dtpInssuranceDate.Value = DBNull.Value;
			dtpInssuranceEndDate.Value = DBNull.Value;
			dtpHealthInsuranceStartDate.Value = DBNull.Value;
			dtpHealthInsuranceEndDate.Value = DBNull.Value;
			dtpPassportExpireDate.Value = DBNull.Value;
			dtpServicePercentFromDate.Value = DBNull.Value;
			dtpWorkOfficeFromDate.Value = DBNull.Value;
			dtpWorkOfficeSendDate.Value = DBNull.Value;
			dtpWorkOfficeToDate.Value = DBNull.Value;
			((Control)(object)txtName).Select();
		}
		dtContacts.Rows.Clear();
		dtDocuments.Rows.Clear();
		dtEmployeeFamilyRelatives.Clear();
		dtCertificates.Rows.Clear();
	}

	public override void SetControls(bool NavMode)
	{
		ImageChanged = false;
		CanModAdministrativeStructure = TreeFunctions.GetTreeFirstCheckedNodeID(treeAdministrativeStructure) == "";
		base.SetControls(NavMode);
		((Control)(object)btnPic).Visible = !NavMode;
		((EditorButtonControlBase)dtpBirthDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpPostponedToDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpDrivingLicenseExpireDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpEndDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpFirstInssuranceDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpHireDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpContractFromDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpContractToDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpIDExpireDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpIDIssueDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpInssuranceDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpPassportExpireDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpServicePercentFromDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpWorkOfficeFromDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpWorkOfficeSendDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpWorkOfficeToDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpInssuranceEndDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpHealthInsuranceStartDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpHealthInsuranceEndDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtLeavingWorkNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAddress).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBankAccountNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCollectionCommission).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCurrentBasicSalary).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCurrentVariantSalary).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotInsuranceVariantSalary).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDrivingLicenseID).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEMail).ReadOnly = NavMode;
		((EditorButtonControlBase)txtOrder).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNetCurrentSalary).ReadOnly = true;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPassportNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPersonalIDNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSalesCommission).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSectionNameNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSocialInssuranceNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSocialInsuranceValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtInclusiveHealthInsuranceValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtStartBasicSalary).ReadOnly = NavMode;
		((EditorButtonControlBase)txtStartSalary).ReadOnly = NavMode;
		((EditorButtonControlBase)txtStartVariantSalary).ReadOnly = NavMode;
		((Control)(object)chkIsFixedSalaryTax).Enabled = !NavMode;
		((EditorButtonControlBase)txtFixedSalaryValue).ReadOnly = NavMode;
		((Control)(object)chkIsFixedTaxPercentage).Enabled = !NavMode && ((UltraToggleEditorBase)chkIsFixedSalaryTax).Checked && ((Control)(object)chkIsFixedSalaryTax).Enabled;
		((EditorButtonControlBase)txtWorkOfficePermissionCode).ReadOnly = NavMode;
		((EditorButtonControlBase)cboLeavingWorkReason).ReadOnly = NavMode;
		((EditorButtonControlBase)cboAbsenceRule).ReadOnly = NavMode;
		((EditorButtonControlBase)cboAdministrativeLevel).ReadOnly = NavMode || (Updating && cboAdministrativeLevel.SelectedIndex > -1);
		((EditorButtonControlBase)cboArea).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBank).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBranch).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCity).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCountry).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCurrency).ReadOnly = NavMode;
		((EditorButtonControlBase)cboDegree).ReadOnly = NavMode || (Updating && cboDegree.SelectedIndex > -1);
		((EditorButtonControlBase)cboDirectManager).ReadOnly = NavMode;
		((EditorButtonControlBase)cboExtraTimeRule).ReadOnly = NavMode;
		((EditorButtonControlBase)cboGender).ReadOnly = NavMode;
		((EditorButtonControlBase)cboHealthInsuranceType).ReadOnly = NavMode;
		((EditorButtonControlBase)cboLateRule).ReadOnly = NavMode;
		((EditorButtonControlBase)cboLeavePermissionRule).ReadOnly = NavMode;
		((EditorButtonControlBase)cboFeedingRule).ReadOnly = NavMode;
		((EditorButtonControlBase)cboMilitaryService).ReadOnly = NavMode;
		((EditorButtonControlBase)cboNationality).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPenalityRule).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPosition).ReadOnly = NavMode || (Updating && cboPosition.SelectedIndex > -1);
		((EditorButtonControlBase)cboQualification).ReadOnly = NavMode;
		((EditorButtonControlBase)cboQualificationYear).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSalaryList).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSectionName).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSocialInssuranceOffice).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSocialStatus).ReadOnly = NavMode;
		((EditorButtonControlBase)cboState).ReadOnly = NavMode;
		((EditorButtonControlBase)cboUniversity).ReadOnly = NavMode;
		((EditorButtonControlBase)cboVacationRule).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSalaryAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboAdvanceAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPenaltyAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSalesManDefaultStore).ReadOnly = NavMode;
		if (Adding)
		{
			((TextEditorControlBase)cboSalesManDefaultStore).Clear();
		}
		else if (Updating && cboBranch.SelectedIndex > -1)
		{
			object value = ((TextEditorControlBase)cboSalesManDefaultStore).Value;
			DataView dataView = new DataView(dtStores);
			dataView.RowFilter = " Locked =0  And BranchID=" + ((TextEditorControlBase)cboBranch).Value.ToString();
			DataTable dt = dataView.ToTable();
			GlobalFunctions.FillCombo(cboSalesManDefaultStore, dt, "StoreID", "StoreName");
			((TextEditorControlBase)cboSalesManDefaultStore).Value = value;
		}
		else
		{
			GlobalFunctions.FillCombo(cboSalesManDefaultStore, dtStores, "StoreID", "StoreName");
		}
		((Control)(object)chkHasPassport).Enabled = !NavMode;
		((Control)(object)chkHasPrivateCar).Enabled = !NavMode;
		((Control)(object)chkSalaryAtEndOFMonth).Enabled = !NavMode;
		((Control)(object)chkInSalaryTax).Enabled = !NavMode;
		((Control)(object)chkInServicePercent).Enabled = !NavMode;
		((Control)(object)chkIsMonthlySalary).Enabled = !NavMode;
		((Control)(object)chkIsSalesMan).Enabled = !NavMode;
		((Control)(object)chkWorkOfficeIsSend).Enabled = !NavMode;
		((UltraGridBase)ULGCetificates).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((UltraGridBase)ULGContacts).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((UltraGridBase)ULGDocuments).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((UltraGridBase)ULGAllownces).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((UltraGridBase)ULGDeductions).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((UltraGridBase)ULGYearIncrease).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((UltraGridBase)ULGMotivation).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((UltraGridBase)ULGFamilyRelativesHealthInsurance).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((UltraGridBase)ULGFamilyRelativesHealthInsurance).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((!NavMode) ? 1 : 2);
		((Control)(object)TreeAccounts).Visible = !NavMode;
		((Control)(object)txtItems).Visible = !NavMode;
		((Control)(object)btnItemsSearch).Visible = !NavMode;
		((Control)(object)lblAccounts).Visible = NavMode;
		if (SelectedNode != null && (Adding || Updating))
		{
			TreeAccounts.CollapseAll();
			TreeFunctions.SetAllTreeNodesCheckState(Checked: false, TreeAccounts);
			DataTable dataTable = SubAccounts_Details.SelectBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, "1", IsFromServer: true);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				TreeFunctions.SetNodeCheckState(CheckState.Checked, dataTable.Rows[i]["AccountID"].ToString(), TreeAccounts);
			}
		}
		((UltraGridBase)ULGContacts).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((UltraTabControlBase)tabItemType).VisibleTabs[0].Visible = Tap1;
		((UltraTabControlBase)tabItemType).VisibleTabs[1].Visible = Tap2;
		((UltraTabControlBase)tabItemType).VisibleTabs[2].Visible = Tap3;
		((UltraTabControlBase)tabItemType).VisibleTabs[3].Visible = Tap4;
		((UltraTabControlBase)tabItemType).VisibleTabs[4].Visible = Tap5;
		((UltraTabControlBase)tabItemType).VisibleTabs[5].Visible = Tap6;
		((UltraTabControlBase)tabItemType).VisibleTabs[6].Visible = Tap7;
		((UltraTabControlBase)tabItemType).VisibleTabs[7].Visible = Tap8;
		((UltraTabControlBase)tabItemType).VisibleTabs[8].Visible = Tap9;
	}

	public override void DisplayData()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_13b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c3: Expected O, but got Unknown
		base.DisplayData();
		if (SelectedNode == null)
		{
			return;
		}
		treeAdministrativeStructure.BeforeCheck -= new BeforeCheckEventHandler(treeAdministrativeStructure_BeforeCheck);
		((TextEditorControlBase)txtCurrentBasicSalary).ValueChanged -= txtCurrentBasicSalary_ValueChanged;
		((Control)(object)txtCurrentBasicSalary).KeyPress -= txt_KeyPress;
		((TextEditorControlBase)txtCurrentVariantSalary).ValueChanged -= txtCurrentVariantSalary_ValueChanged;
		((Control)(object)txtCurrentVariantSalary).KeyPress -= txt_KeyPress;
		TreeFunctions.SetAllTreeNodesCheckState(Checked: false, treeAdministrativeStructure);
		picEmployee.Image = null;
		dtpBirthDate.Value = DBNull.Value;
		dtpPostponedToDate.Value = DBNull.Value;
		dtpDrivingLicenseExpireDate.Value = DBNull.Value;
		dtpEndDate.Value = DBNull.Value;
		dtpFirstInssuranceDate.Value = DBNull.Value;
		dtpInssuranceEndDate.Value = DBNull.Value;
		dtpHealthInsuranceStartDate.Value = DBNull.Value;
		dtpHealthInsuranceEndDate.Value = DBNull.Value;
		dtpHireDate.Value = DBNull.Value;
		dtpIDExpireDate.Value = DBNull.Value;
		dtpIDIssueDate.Value = DBNull.Value;
		dtpInssuranceDate.Value = DBNull.Value;
		dtpPassportExpireDate.Value = DBNull.Value;
		dtpServicePercentFromDate.Value = DBNull.Value;
		dtpWorkOfficeFromDate.Value = DBNull.Value;
		dtpWorkOfficeSendDate.Value = DBNull.Value;
		dtpWorkOfficeToDate.Value = DBNull.Value;
		((TextEditorControlBase)txtAddress).Clear();
		((TextEditorControlBase)txtBankAccountNo).Clear();
		((TextEditorControlBase)txtCollectionCommission).Clear();
		((TextEditorControlBase)txtCurrentBasicSalary).Clear();
		((TextEditorControlBase)txtCurrentVariantSalary).Clear();
		((TextEditorControlBase)txtNotInsuranceVariantSalary).Clear();
		((TextEditorControlBase)txtDrivingLicenseID).Clear();
		((TextEditorControlBase)txtEMail).Clear();
		((TextEditorControlBase)txtEmployeeNo).Clear();
		((TextEditorControlBase)txtOrder).Clear();
		((TextEditorControlBase)txtNetCurrentSalary).Clear();
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)txtPassportNo).Clear();
		((TextEditorControlBase)txtPersonalIDNo).Clear();
		((TextEditorControlBase)txtSalesCommission).Clear();
		((TextEditorControlBase)txtSectionNameNotes).Clear();
		((TextEditorControlBase)txtSocialInssuranceNo).Clear();
		((TextEditorControlBase)txtSocialInsuranceValue).Clear();
		((TextEditorControlBase)txtInclusiveHealthInsuranceValue).Clear();
		((TextEditorControlBase)txtStartBasicSalary).Clear();
		((TextEditorControlBase)txtStartSalary).Clear();
		((TextEditorControlBase)txtStartVariantSalary).Clear();
		((TextEditorControlBase)txtFixedSalaryValue).Clear();
		((TextEditorControlBase)txtWorkOfficePermissionCode).Clear();
		((TextEditorControlBase)txtLeavingWorkNotes).Clear();
		((TextEditorControlBase)cboAbsenceRule).Clear();
		((TextEditorControlBase)cboAdministrativeLevel).Clear();
		((TextEditorControlBase)cboLeavingWorkReason).Clear();
		((TextEditorControlBase)cboArea).Clear();
		((TextEditorControlBase)cboBank).Clear();
		((TextEditorControlBase)cboBranch).Clear();
		((TextEditorControlBase)cboCity).Clear();
		((TextEditorControlBase)cboCountry).Clear();
		((TextEditorControlBase)cboCurrency).Clear();
		((TextEditorControlBase)cboDegree).Clear();
		((TextEditorControlBase)cboDirectManager).Clear();
		((TextEditorControlBase)cboExtraTimeRule).Clear();
		((TextEditorControlBase)cboGender).Clear();
		((TextEditorControlBase)cboHealthInsuranceType).Clear();
		((TextEditorControlBase)cboLateRule).Clear();
		((TextEditorControlBase)cboLeavePermissionRule).Clear();
		((TextEditorControlBase)cboFeedingRule).Clear();
		((TextEditorControlBase)cboMilitaryService).Clear();
		((TextEditorControlBase)cboNationality).Clear();
		((TextEditorControlBase)cboPenalityRule).Clear();
		((TextEditorControlBase)cboPosition).Clear();
		((TextEditorControlBase)cboQualification).Clear();
		((TextEditorControlBase)cboQualificationYear).Clear();
		((TextEditorControlBase)cboSalaryList).Clear();
		((TextEditorControlBase)cboSectionName).Clear();
		((TextEditorControlBase)cboSocialInssuranceOffice).Clear();
		((TextEditorControlBase)cboSocialStatus).Clear();
		((TextEditorControlBase)cboState).Clear();
		((TextEditorControlBase)cboUniversity).Clear();
		((TextEditorControlBase)cboVacationRule).Clear();
		((TextEditorControlBase)cboSalaryAccount).Clear();
		((TextEditorControlBase)cboAdvanceAccount).Clear();
		((TextEditorControlBase)cboPenaltyAccount).Clear();
		((UltraToggleEditorBase)chkHasPassport).Checked = false;
		((UltraToggleEditorBase)chkHasPrivateCar).Checked = false;
		((UltraToggleEditorBase)chkInSalaryTax).Checked = false;
		((UltraToggleEditorBase)chkInServicePercent).Checked = false;
		((UltraToggleEditorBase)chkIsMonthlySalary).Checked = false;
		((UltraToggleEditorBase)chkIsSalesMan).Checked = false;
		((UltraToggleEditorBase)chkWorkOfficeIsSend).Checked = false;
		((UltraToggleEditorBase)chkIsFixedSalaryTax).Checked = false;
		((UltraToggleEditorBase)chkSalaryAtEndOFMonth).Checked = false;
		changeParentTSMenu.Enabled = !Convert.ToBoolean(((DataRow)((SubObjectBase)SelectedNode).Tag)[IsMainCol]);
		setAsGroupToolStripMenuItem.Enabled = !Convert.ToBoolean(((DataRow)((SubObjectBase)SelectedNode).Tag)[IsMainCol]);
		modifyGroupEmployeesToolStripMenuItem.Enabled = Convert.ToBoolean(((DataRow)((SubObjectBase)SelectedNode).Tag)[IsMainCol]);
		setAsSubAccountToolStripMenuItem.Enabled = ((DisposableObjectCollectionBase)SelectedNode.Nodes).Count == 0 && Convert.ToBoolean(((DataRow)((SubObjectBase)SelectedNode).Tag)[IsMainCol]);
		((Control)(object)lblAccounts).Text = "";
		dtSubAccDetails = SubAccounts_Details.SelectBySubAccountIDWithAccountName(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		for (int i = 0; i < dtSubAccDetails.Rows.Count; i++)
		{
			UltraLabel obj = lblAccounts;
			((Control)(object)obj).Text = string.Concat(((Control)(object)obj).Text, dtSubAccDetails.Rows[i]["AccountName"], " \n");
		}
		treeAdministrativeStructure.CollapseAll();
		GlobalFunctions.FillCombo(cboSalaryAccount, dtSubAccDetails, "AccountID", "AccountName");
		GlobalFunctions.FillCombo(cboAdvanceAccount, dtSubAccDetails, "AccountID", "AccountName");
		GlobalFunctions.FillCombo(cboPenaltyAccount, dtSubAccDetails, "AccountID", "AccountName");
		DataTable dataTable = Employees.SelectBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.Rows[0];
			if (dataRow["AdministrativeStructureID"] != DBNull.Value)
			{
				treeAdministrativeStructure.GetNodeByKey(dataRow["AdministrativeStructureID"].ToString()).CheckedState = CheckState.Checked;
				treeAdministrativeStructure.ActiveNode = treeAdministrativeStructure.GetNodeByKey(dataRow["AdministrativeStructureID"].ToString());
			}
			picEmployee.Image = ((dataRow["EmployeeImage"] == DBNull.Value) ? null : ImageFunctions.BinaryToImage((byte[])dataRow["EmployeeImage"]));
			ApplicantID = dataRow["ApplicantID"].ToString();
			dtpBirthDate.Value = dataRow["BirthDate"];
			dtpPostponedToDate.Value = dataRow["PostponedToDate"];
			dtpDrivingLicenseExpireDate.Value = dataRow["DrivingLicenseExpireDate"];
			dtpEndDate.Value = dataRow["EmployeeEndDate"];
			dtpFirstInssuranceDate.Value = dataRow["FirstInssuranceDate"];
			dtpInssuranceEndDate.Value = dataRow["InssuranceEndDate"];
			dtpHealthInsuranceStartDate.Value = dataRow["HealthInsuranceStartDate"];
			dtpHealthInsuranceEndDate.Value = dataRow["HealthInsuranceEndDate"];
			dtpHireDate.ValueChanged -= dtpHireDate_ValueChanged;
			dtpContractFromDate.ValueChanged -= dtpContractFromDate_ValueChanged;
			dtpHireDate.Value = dataRow["HireDate"];
			dtpContractFromDate.Value = dataRow["ContractFromDate"];
			dtpContractToDate.Value = dataRow["ContractToDate"];
			dtpHireDate.ValueChanged += dtpHireDate_ValueChanged;
			dtpContractFromDate.ValueChanged += dtpContractFromDate_ValueChanged;
			dtpIDExpireDate.Value = dataRow["PersonalIDExpireDate"];
			dtpIDIssueDate.Value = dataRow["PersonalIDIssueDate"];
			dtpInssuranceDate.Value = dataRow["InssuranceDate"];
			dtpPassportExpireDate.Value = dataRow["PassportExpireDate"];
			dtpServicePercentFromDate.Value = dataRow["ServicePercentFromDate"];
			dtpWorkOfficeFromDate.Value = dataRow["WorkOfficeFromDate"];
			dtpWorkOfficeSendDate.Value = dataRow["WorkOfficeSendDate"];
			dtpWorkOfficeToDate.Value = dataRow["WorkOfficeToDate"];
			((Control)(object)txtLeavingWorkNotes).Text = dataRow["LeavingWorkNotes"].ToString();
			((Control)(object)txtAddress).Text = dataRow["Address"].ToString();
			((Control)(object)txtBankAccountNo).Text = dataRow["BankAccountNo"].ToString();
			((Control)(object)txtCollectionCommission).Text = dataRow["CollectionCommissionRatio"].ToString();
			((Control)(object)txtCurrentBasicSalary).Text = dataRow["CurrentBasicSalary"].ToString();
			((Control)(object)txtCurrentVariantSalary).Text = dataRow["CurrentVariantSalary"].ToString();
			((Control)(object)txtNotInsuranceVariantSalary).Text = dataRow["NotInsuranceVariantSalary"].ToString();
			((Control)(object)txtDrivingLicenseID).Text = dataRow["DrivingLicenseID"].ToString();
			((Control)(object)txtEMail).Text = dataRow["EMail"].ToString();
			((Control)(object)txtEmployeeNo).Text = dataRow["EmployeeNo"].ToString();
			((Control)(object)txtOrder).Text = dataRow["EmployeeOrder"].ToString();
			((Control)(object)txtNetCurrentSalary).Text = dataRow["NetCurrentSalary"].ToString();
			((Control)(object)txtNotes).Text = dataRow["Notes"].ToString();
			((Control)(object)txtPassportNo).Text = dataRow["PassportNo"].ToString();
			((Control)(object)txtPersonalIDNo).Text = dataRow["PersonalIDNo"].ToString();
			((Control)(object)txtSalesCommission).Text = dataRow["SalesCommissionRatio"].ToString();
			((Control)(object)txtSectionNameNotes).Text = dataRow["SectionNameNotes"].ToString();
			((Control)(object)txtSocialInsuranceValue).Text = dataRow["SocialInssuranceValue"].ToString();
			((Control)(object)txtInclusiveHealthInsuranceValue).Text = dataRow["InclusiveHealthInsuranceValue"].ToString();
			((Control)(object)txtSocialInssuranceNo).Text = dataRow["SocialInssuranceNo"].ToString();
			((Control)(object)txtStartBasicSalary).Text = dataRow["StartBasicSalary"].ToString();
			((Control)(object)txtStartSalary).Text = dataRow["StartSalary"].ToString();
			((Control)(object)txtStartVariantSalary).Text = dataRow["StartVariantSalary"].ToString();
			((Control)(object)txtFixedSalaryValue).Text = dataRow["FixedSalaryTaxValue"].ToString();
			((Control)(object)txtWorkOfficePermissionCode).Text = dataRow["WorkOfficePermissionCode"].ToString();
			((TextEditorControlBase)cboLeavingWorkReason).Value = dataRow["LeavingWorkReasonID"];
			((TextEditorControlBase)cboAbsenceRule).Value = dataRow["AbsentRuleID"];
			((TextEditorControlBase)cboAdministrativeLevel).Value = dataRow["AdministrativeLevelID"];
			((TextEditorControlBase)cboArea).Value = dataRow["AreaID"];
			((TextEditorControlBase)cboBank).Value = dataRow["BankID"];
			((TextEditorControlBase)cboBranch).Value = dataRow["BranchID"];
			((TextEditorControlBase)cboCity).Value = dataRow["CityID"];
			((TextEditorControlBase)cboCountry).Value = dataRow["CountryID"];
			((TextEditorControlBase)cboCurrency).Value = dataRow["CurrencyID"];
			((TextEditorControlBase)cboDegree).Value = dataRow["DegreeID"];
			((TextEditorControlBase)cboDirectManager).Value = dataRow["DirectManagerID"];
			((TextEditorControlBase)cboExtraTimeRule).Value = dataRow["ExtraTimeRuleID"];
			((TextEditorControlBase)cboGender).Value = dataRow["GenderID"];
			((TextEditorControlBase)cboHealthInsuranceType).Value = dataRow["HealthInsuranceTypeID"];
			((TextEditorControlBase)cboLateRule).Value = dataRow["DelayRuleID"];
			((TextEditorControlBase)cboLeavePermissionRule).Value = dataRow["LeavePermissionRuleID"];
			((TextEditorControlBase)cboFeedingRule).Value = dataRow["FeedingRuleID"];
			((TextEditorControlBase)cboMilitaryService).Value = dataRow["MilitaryServiceID"];
			((TextEditorControlBase)cboNationality).Value = dataRow["NationalityID"];
			((TextEditorControlBase)cboPenalityRule).Value = dataRow["PenaltyRuleID"];
			((TextEditorControlBase)cboPosition).Value = dataRow["PositionID"];
			((TextEditorControlBase)cboQualification).Value = dataRow["QualificationNameID"];
			((TextEditorControlBase)cboQualificationYear).Value = dataRow["QualificationYear"];
			((TextEditorControlBase)cboSalaryList).Value = dataRow["SalaryListID"];
			((TextEditorControlBase)cboSectionName).Value = dataRow["SectionNameID"];
			((TextEditorControlBase)cboSocialInssuranceOffice).Value = dataRow["SocialInssuranceOfficeID"];
			((TextEditorControlBase)cboSocialStatus).Value = dataRow["SocialStatusID"];
			((TextEditorControlBase)cboState).Value = dataRow["StateID"];
			((TextEditorControlBase)cboUniversity).Value = dataRow["UniversityID"];
			((TextEditorControlBase)cboVacationRule).Value = dataRow["VacationRuleID"];
			((TextEditorControlBase)cboSalaryAccount).Value = dataRow["SalaryAccountID"];
			((TextEditorControlBase)cboAdvanceAccount).Value = dataRow["AdvanceAccountID"];
			((TextEditorControlBase)cboPenaltyAccount).Value = dataRow["PenaltyAccountID"];
			((TextEditorControlBase)cboSalesManDefaultStore).Value = dataRow["SalesManDefaultStoreID"];
			((UltraToggleEditorBase)chkHasPassport).Checked = !dataRow["HasPassport"].Equals(DBNull.Value) && Convert.ToBoolean(dataRow["HasPassport"]);
			((UltraToggleEditorBase)chkHasPrivateCar).Checked = !dataRow["HasPrivateCar"].Equals(DBNull.Value) && Convert.ToBoolean(dataRow["HasPrivateCar"]);
			((UltraToggleEditorBase)chkInSalaryTax).Checked = !dataRow["InSalaryTax"].Equals(DBNull.Value) && Convert.ToBoolean(dataRow["InSalaryTax"]);
			((UltraToggleEditorBase)chkInServicePercent).Checked = !dataRow["InServicePercent"].Equals(DBNull.Value) && Convert.ToBoolean(dataRow["InServicePercent"]);
			((UltraToggleEditorBase)chkIsMonthlySalary).Checked = !dataRow["IsMonthlySalary"].Equals(DBNull.Value) && Convert.ToBoolean(dataRow["IsMonthlySalary"]);
			((UltraToggleEditorBase)chkIsSalesMan).Checked = !dataRow["IsSalesMan"].Equals(DBNull.Value) && Convert.ToBoolean(dataRow["IsSalesMan"]);
			((UltraToggleEditorBase)chkWorkOfficeIsSend).Checked = !dataRow["WorkOfficeIsSend"].Equals(DBNull.Value) && Convert.ToBoolean(dataRow["WorkOfficeIsSend"]);
			((UltraToggleEditorBase)chkIsFixedSalaryTax).Checked = !dataRow["IsFixedSalaryTax"].Equals(DBNull.Value) && Convert.ToBoolean(dataRow["IsFixedSalaryTax"]);
			((UltraToggleEditorBase)chkIsFixedTaxPercentage).Checked = !dataRow["IsFixedTaxPercentage"].Equals(DBNull.Value) && Convert.ToBoolean(dataRow["IsFixedTaxPercentage"]);
			((UltraToggleEditorBase)chkSalaryAtEndOFMonth).Checked = !dataRow["IsSalaryAtEndOfMonth"].Equals(DBNull.Value) && Convert.ToBoolean(dataRow["IsSalaryAtEndOfMonth"]);
		}
		dtContacts = EmployeesContacts.SelectBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtDocuments = EmployeesDocuments.SelectBySubAccountIDWithoutImage(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtEmployeeFamilyRelatives = EmployeesFamilyRelativesHealthInsurance.SelectBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtCertificates = EmployeesCertificates.SelectBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtEmployeeAllownces = EmployeesAllowances.SelectBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtEmployeeDeductions = EmployeesDeductions.SelectBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtEmployeeYearIncreases = EmployeeYearIncreases.SelectBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtEmployeeMotivations = EmployeesMotivations.SelectBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGridContacts();
		InitGridDocuments();
		InitGridEmpRelatives();
		InitGridCertificates();
		InitGridAllownces();
		InitGridDeductions();
		InitGridYearIncreases();
		InitGridMotivations();
		CalculateTotalSalary();
		treeAdministrativeStructure.BeforeCheck += new BeforeCheckEventHandler(treeAdministrativeStructure_BeforeCheck);
		((TextEditorControlBase)txtCurrentBasicSalary).ValueChanged += txtCurrentBasicSalary_ValueChanged;
		((Control)(object)txtCurrentBasicSalary).KeyPress += txt_KeyPress;
		((TextEditorControlBase)txtCurrentVariantSalary).ValueChanged += txtCurrentVariantSalary_ValueChanged;
		((Control)(object)txtCurrentVariantSalary).KeyPress += txt_KeyPress;
	}

	private void InitGridContacts()
	{
		((UltraGridBase)ULGContacts).DataSource = dtContacts;
		GlobalFunctions.PrepareGrid(ULGContacts);
		((UltraGridBase)ULGContacts).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGContacts).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["EmployeeContactID"].DefaultCellValue = -1;
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = GlobalVariables.CurrentBranchID;
		((HeaderBase)((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactNumber"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الهاتف" : "ContactNumber");
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactNumber"].Hidden = false;
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactNumber"].Width = (int)((double)((Control)(object)ULGContacts).Width * 0.5) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactNote"].Header).Caption = (GlobalVariables.IsArabic ? "البيان" : "Notes");
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactNote"].Hidden = false;
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactNote"].Width = (int)((double)((Control)(object)ULGContacts).Width * 0.5);
	}

	private void InitGridCertificates()
	{
		((UltraGridBase)ULGCetificates).DataSource = dtCertificates;
		GlobalFunctions.PrepareGrid(ULGCetificates);
		((UltraGridBase)ULGCetificates).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGCetificates).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGCetificates).DisplayLayout.Bands[0].Columns["EmployeeCertificateID"].DefaultCellValue = -1;
		((UltraGridBase)ULGCetificates).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = GlobalVariables.CurrentBranchID;
		((HeaderBase)((UltraGridBase)ULGCetificates).DisplayLayout.Bands[0].Columns["CertificateNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "الشهاده AR" : "Certificate AR");
		((UltraGridBase)ULGCetificates).DisplayLayout.Bands[0].Columns["CertificateNameAr"].Hidden = false;
		((UltraGridBase)ULGCetificates).DisplayLayout.Bands[0].Columns["CertificateNameAr"].Width = (int)((double)((Control)(object)ULGCetificates).Width * 0.45);
		((HeaderBase)((UltraGridBase)ULGCetificates).DisplayLayout.Bands[0].Columns["CertificateNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الشهاده EN" : "Certificate EN");
		((UltraGridBase)ULGCetificates).DisplayLayout.Bands[0].Columns["CertificateNameEn"].Hidden = false;
		((UltraGridBase)ULGCetificates).DisplayLayout.Bands[0].Columns["CertificateNameEn"].Width = (int)((double)((Control)(object)ULGCetificates).Width * 0.35);
		((HeaderBase)((UltraGridBase)ULGCetificates).DisplayLayout.Bands[0].Columns["CertificateDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGCetificates).DisplayLayout.Bands[0].Columns["CertificateDate"].Hidden = false;
		((UltraGridBase)ULGCetificates).DisplayLayout.Bands[0].Columns["CertificateDate"].Width = (int)((double)((Control)(object)ULGCetificates).Width * 0.2) - GlobalVariables.ScrollWidth;
	}

	private void InitGridEmpRelatives()
	{
		((UltraGridBase)ULGFamilyRelativesHealthInsurance).DataSource = dtEmployeeFamilyRelatives;
		GlobalFunctions.PrepareGrid(ULGFamilyRelativesHealthInsurance);
		((UltraGridBase)ULGFamilyRelativesHealthInsurance).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGFamilyRelativesHealthInsurance).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGFamilyRelativesHealthInsurance).DisplayLayout.Bands[0].Columns["EmployeeFamilyRelativeHealthInsuranceID"].DefaultCellValue = -1;
		((UltraGridBase)ULGFamilyRelativesHealthInsurance).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = GlobalVariables.CurrentBranchID;
		((UltraGridBase)ULGFamilyRelativesHealthInsurance).DisplayLayout.Bands[0].Columns["IsWorking"].DefaultCellValue = false;
		((HeaderBase)((UltraGridBase)ULGFamilyRelativesHealthInsurance).DisplayLayout.Bands[0].Columns["FamilyRelativeID"].Header).Caption = (GlobalVariables.IsArabic ? "صلة القرابة" : "RelationShip");
		((UltraGridBase)ULGFamilyRelativesHealthInsurance).DisplayLayout.Bands[0].Columns["FamilyRelativeID"].Hidden = false;
		((UltraGridBase)ULGFamilyRelativesHealthInsurance).DisplayLayout.Bands[0].Columns["FamilyRelativeID"].Width = (int)((double)((Control)(object)ULGFamilyRelativesHealthInsurance).Width * 0.2);
		((UltraGridBase)ULGFamilyRelativesHealthInsurance).DisplayLayout.Bands[0].Columns["FamilyRelativeID"].ValueList = (IValueList)(object)vlFamilyRelatives;
		((HeaderBase)((UltraGridBase)ULGFamilyRelativesHealthInsurance).DisplayLayout.Bands[0].Columns["IsWorking"].Header).Caption = (GlobalVariables.IsArabic ? "يعمل" : "Is Working");
		((UltraGridBase)ULGFamilyRelativesHealthInsurance).DisplayLayout.Bands[0].Columns["IsWorking"].Hidden = false;
		((UltraGridBase)ULGFamilyRelativesHealthInsurance).DisplayLayout.Bands[0].Columns["IsWorking"].Width = (int)((double)((Control)(object)ULGFamilyRelativesHealthInsurance).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGFamilyRelativesHealthInsurance).DisplayLayout.Bands[0].Columns["BirthDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الميلاد" : "Birth Date");
		((UltraGridBase)ULGFamilyRelativesHealthInsurance).DisplayLayout.Bands[0].Columns["BirthDate"].Hidden = false;
		((UltraGridBase)ULGFamilyRelativesHealthInsurance).DisplayLayout.Bands[0].Columns["BirthDate"].Width = (int)((double)((Control)(object)ULGFamilyRelativesHealthInsurance).Width * 0.17);
		((HeaderBase)((UltraGridBase)ULGFamilyRelativesHealthInsurance).DisplayLayout.Bands[0].Columns["FamilyRelativeNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم بالعربيه" : "Ar. Name");
		((UltraGridBase)ULGFamilyRelativesHealthInsurance).DisplayLayout.Bands[0].Columns["FamilyRelativeNameAr"].Hidden = false;
		((UltraGridBase)ULGFamilyRelativesHealthInsurance).DisplayLayout.Bands[0].Columns["FamilyRelativeNameAr"].Width = (int)((double)((Control)(object)ULGFamilyRelativesHealthInsurance).Width * 0.17);
		((HeaderBase)((UltraGridBase)ULGFamilyRelativesHealthInsurance).DisplayLayout.Bands[0].Columns["FamilyRelativeNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم بالانجليزية" : "En. Name");
		((UltraGridBase)ULGFamilyRelativesHealthInsurance).DisplayLayout.Bands[0].Columns["FamilyRelativeNameEn"].Hidden = false;
		((UltraGridBase)ULGFamilyRelativesHealthInsurance).DisplayLayout.Bands[0].Columns["FamilyRelativeNameEn"].Width = (int)((double)((Control)(object)ULGFamilyRelativesHealthInsurance).Width * 0.36) - GlobalVariables.ScrollWidth;
	}

	private void InitGridDocuments()
	{
		((UltraGridBase)ULGDocuments).DataSource = dtDocuments;
		GlobalFunctions.PrepareGrid(ULGDocuments);
		((UltraGridBase)ULGDocuments).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDocuments).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGDocuments).DisplayLayout.Bands[0].Columns["EmployeeDocumentID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDocuments).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = GlobalVariables.CurrentBranchID;
		((UltraGridBase)ULGDocuments).DisplayLayout.Bands[0].Columns["Deliverd"].DefaultCellValue = false;
		((HeaderBase)((UltraGridBase)ULGDocuments).DisplayLayout.Bands[0].Columns["DocumentTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع المستند" : "DocumentType");
		((UltraGridBase)ULGDocuments).DisplayLayout.Bands[0].Columns["DocumentTypeID"].Hidden = false;
		((UltraGridBase)ULGDocuments).DisplayLayout.Bands[0].Columns["DocumentTypeID"].Width = (int)((double)((Control)(object)ULGDocuments).Width * 0.2);
		((UltraGridBase)ULGDocuments).DisplayLayout.Bands[0].Columns["DocumentTypeID"].ValueList = (IValueList)(object)vlDocumentsTypes;
		((HeaderBase)((UltraGridBase)ULGDocuments).DisplayLayout.Bands[0].Columns["Deliverd"].Header).Caption = (GlobalVariables.IsArabic ? "مستلم" : "Deliverd");
		((UltraGridBase)ULGDocuments).DisplayLayout.Bands[0].Columns["Deliverd"].Hidden = false;
		((UltraGridBase)ULGDocuments).DisplayLayout.Bands[0].Columns["Deliverd"].Width = (int)((double)((Control)(object)ULGDocuments).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDocuments).DisplayLayout.Bands[0].Columns["DeliverdDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الاستلام" : "Deliverd Date");
		((UltraGridBase)ULGDocuments).DisplayLayout.Bands[0].Columns["DeliverdDate"].Hidden = false;
		((UltraGridBase)ULGDocuments).DisplayLayout.Bands[0].Columns["DeliverdDate"].Width = (int)((double)((Control)(object)ULGDocuments).Width * 0.17);
		((HeaderBase)((UltraGridBase)ULGDocuments).DisplayLayout.Bands[0].Columns["ValidTo"].Header).Caption = (GlobalVariables.IsArabic ? "ساري حتي" : "Valid To");
		((UltraGridBase)ULGDocuments).DisplayLayout.Bands[0].Columns["ValidTo"].Hidden = false;
		((UltraGridBase)ULGDocuments).DisplayLayout.Bands[0].Columns["ValidTo"].Width = (int)((double)((Control)(object)ULGDocuments).Width * 0.17);
		((HeaderBase)((UltraGridBase)ULGDocuments).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "البيان" : "Notes");
		((UltraGridBase)ULGDocuments).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDocuments).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGDocuments).Width * 0.36) - GlobalVariables.ScrollWidth;
	}

	private void InitGridAllownces()
	{
		((UltraGridBase)ULGAllownces).DataSource = dtEmployeeAllownces;
		GlobalFunctions.PrepareGrid(ULGAllownces);
		((UltraGridBase)ULGAllownces).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGAllownces).DisplayLayout.Bands[0].Columns["EmployeeAllowanceID"].DefaultCellValue = -1;
		((UltraGridBase)ULGAllownces).DisplayLayout.Bands[0].Columns["IsByDate"].DefaultCellValue = false;
		((UltraGridBase)ULGAllownces).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = GlobalVariables.CurrentBranchID;
		((HeaderBase)((UltraGridBase)ULGAllownces).DisplayLayout.Bands[0].Columns["AllowanceID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع البدل" : "AllownceType");
		((UltraGridBase)ULGAllownces).DisplayLayout.Bands[0].Columns["AllowanceID"].Hidden = false;
		((UltraGridBase)ULGAllownces).DisplayLayout.Bands[0].Columns["AllowanceID"].Width = (int)((double)((Control)(object)ULGAllownces).Width * 0.3);
		((UltraGridBase)ULGAllownces).DisplayLayout.Bands[0].Columns["AllowanceID"].ValueList = (IValueList)(object)vlAllownces;
		((HeaderBase)((UltraGridBase)ULGAllownces).DisplayLayout.Bands[0].Columns["IsPercent"].Header).Caption = (GlobalVariables.IsArabic ? "نسبه" : "IsPercent");
		((UltraGridBase)ULGAllownces).DisplayLayout.Bands[0].Columns["IsPercent"].Hidden = false;
		((UltraGridBase)ULGAllownces).DisplayLayout.Bands[0].Columns["IsPercent"].Width = (int)((double)((Control)(object)ULGAllownces).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGAllownces).DisplayLayout.Bands[0].Columns["Value"].Header).Caption = (GlobalVariables.IsArabic ? "القيمه" : "Value");
		((UltraGridBase)ULGAllownces).DisplayLayout.Bands[0].Columns["Value"].Hidden = false;
		((UltraGridBase)ULGAllownces).DisplayLayout.Bands[0].Columns["Value"].Width = (int)((double)((Control)(object)ULGAllownces).Width * 0.12);
		((HeaderBase)((UltraGridBase)ULGAllownces).DisplayLayout.Bands[0].Columns["IsByDate"].Header).Caption = (GlobalVariables.IsArabic ? "محدد بتاريخ" : "Is By Date");
		((UltraGridBase)ULGAllownces).DisplayLayout.Bands[0].Columns["IsByDate"].Hidden = false;
		((UltraGridBase)ULGAllownces).DisplayLayout.Bands[0].Columns["IsByDate"].Width = (int)((double)((Control)(object)ULGAllownces).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGAllownces).DisplayLayout.Bands[0].Columns["FromDate"].Header).Caption = (GlobalVariables.IsArabic ? "من تاريخ" : "From Date");
		((UltraGridBase)ULGAllownces).DisplayLayout.Bands[0].Columns["FromDate"].Hidden = false;
		((UltraGridBase)ULGAllownces).DisplayLayout.Bands[0].Columns["FromDate"].Width = (int)((double)((Control)(object)ULGAllownces).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGAllownces).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "البيان" : "Notes");
		((UltraGridBase)ULGAllownces).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGAllownces).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGAllownces).Width * 0.2) - GlobalVariables.ScrollWidth;
	}

	private void InitGridDeductions()
	{
		((UltraGridBase)ULGDeductions).DataSource = dtEmployeeDeductions;
		GlobalFunctions.PrepareGrid(ULGDeductions);
		((UltraGridBase)ULGDeductions).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDeductions).DisplayLayout.Bands[0].Columns["EmployeeDeductionID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDeductions).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = GlobalVariables.CurrentBranchID;
		((HeaderBase)((UltraGridBase)ULGDeductions).DisplayLayout.Bands[0].Columns["DeductionID"].Header).Caption = (GlobalVariables.IsArabic ? "الإستقطاع" : "Deduction");
		((UltraGridBase)ULGDeductions).DisplayLayout.Bands[0].Columns["DeductionID"].Hidden = false;
		((UltraGridBase)ULGDeductions).DisplayLayout.Bands[0].Columns["DeductionID"].Width = (int)((double)((Control)(object)ULGDeductions).Width * 0.4);
		((UltraGridBase)ULGDeductions).DisplayLayout.Bands[0].Columns["DeductionID"].ValueList = (IValueList)(object)vlDeductions;
		((HeaderBase)((UltraGridBase)ULGDeductions).DisplayLayout.Bands[0].Columns["IsPercent"].Header).Caption = (GlobalVariables.IsArabic ? "نسبه" : "IsPercent");
		((UltraGridBase)ULGDeductions).DisplayLayout.Bands[0].Columns["IsPercent"].Hidden = false;
		((UltraGridBase)ULGDeductions).DisplayLayout.Bands[0].Columns["IsPercent"].Width = (int)((double)((Control)(object)ULGDeductions).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGDeductions).DisplayLayout.Bands[0].Columns["Value"].Header).Caption = (GlobalVariables.IsArabic ? "القيمه" : "Value");
		((UltraGridBase)ULGDeductions).DisplayLayout.Bands[0].Columns["Value"].Hidden = false;
		((UltraGridBase)ULGDeductions).DisplayLayout.Bands[0].Columns["Value"].Width = (int)((double)((Control)(object)ULGDeductions).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGDeductions).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "البيان" : "Notes");
		((UltraGridBase)ULGDeductions).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDeductions).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGDeductions).Width * 0.25) - GlobalVariables.ScrollWidth;
	}

	private void InitGridYearIncreases()
	{
		((UltraGridBase)ULGYearIncrease).DataSource = dtEmployeeYearIncreases;
		GlobalFunctions.PrepareGrid(ULGYearIncrease);
		((UltraGridBase)ULGYearIncrease).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGYearIncrease).DisplayLayout.Bands[0].Columns["EmployeeYearIncreaseID"].DefaultCellValue = -1;
		((UltraGridBase)ULGYearIncrease).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = GlobalVariables.CurrentBranchID;
		((HeaderBase)((UltraGridBase)ULGYearIncrease).DisplayLayout.Bands[0].Columns["YearIncreaseID"].Header).Caption = (GlobalVariables.IsArabic ? "العلاوه" : "Increases");
		((UltraGridBase)ULGYearIncrease).DisplayLayout.Bands[0].Columns["YearIncreaseID"].Hidden = false;
		((UltraGridBase)ULGYearIncrease).DisplayLayout.Bands[0].Columns["YearIncreaseID"].Width = (int)((double)((Control)(object)ULGYearIncrease).Width * 0.4);
		((UltraGridBase)ULGYearIncrease).DisplayLayout.Bands[0].Columns["YearIncreaseID"].ValueList = (IValueList)(object)vlYearIncreases;
		((HeaderBase)((UltraGridBase)ULGYearIncrease).DisplayLayout.Bands[0].Columns["IsPercent"].Header).Caption = (GlobalVariables.IsArabic ? "نسبه" : "IsPercent");
		((UltraGridBase)ULGYearIncrease).DisplayLayout.Bands[0].Columns["IsPercent"].Hidden = false;
		((UltraGridBase)ULGYearIncrease).DisplayLayout.Bands[0].Columns["IsPercent"].Width = (int)((double)((Control)(object)ULGYearIncrease).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGYearIncrease).DisplayLayout.Bands[0].Columns["Value"].Header).Caption = (GlobalVariables.IsArabic ? "القيمه" : "Value");
		((UltraGridBase)ULGYearIncrease).DisplayLayout.Bands[0].Columns["Value"].Hidden = false;
		((UltraGridBase)ULGYearIncrease).DisplayLayout.Bands[0].Columns["Value"].Width = (int)((double)((Control)(object)ULGYearIncrease).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGYearIncrease).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "البيان" : "Notes");
		((UltraGridBase)ULGYearIncrease).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGYearIncrease).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGYearIncrease).Width * 0.25) - GlobalVariables.ScrollWidth;
	}

	private void InitGridMotivations()
	{
		((UltraGridBase)ULGMotivation).DataSource = dtEmployeeMotivations;
		GlobalFunctions.PrepareGrid(ULGMotivation);
		((UltraGridBase)ULGMotivation).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGMotivation).DisplayLayout.Bands[0].Columns["EmployeeMotivationID"].DefaultCellValue = -1;
		((UltraGridBase)ULGMotivation).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = GlobalVariables.CurrentBranchID;
		((HeaderBase)((UltraGridBase)ULGMotivation).DisplayLayout.Bands[0].Columns["MotivationID"].Header).Caption = (GlobalVariables.IsArabic ? "حافز" : "Motivation");
		((UltraGridBase)ULGMotivation).DisplayLayout.Bands[0].Columns["MotivationID"].Hidden = false;
		((UltraGridBase)ULGMotivation).DisplayLayout.Bands[0].Columns["MotivationID"].Width = (int)((double)((Control)(object)ULGMotivation).Width * 0.3);
		((UltraGridBase)ULGMotivation).DisplayLayout.Bands[0].Columns["MotivationID"].ValueList = (IValueList)(object)vlMotivations;
		((HeaderBase)((UltraGridBase)ULGMotivation).DisplayLayout.Bands[0].Columns["IsPercent"].Header).Caption = (GlobalVariables.IsArabic ? "نسبه" : "IsPercent");
		((UltraGridBase)ULGMotivation).DisplayLayout.Bands[0].Columns["IsPercent"].Hidden = false;
		((UltraGridBase)ULGMotivation).DisplayLayout.Bands[0].Columns["IsPercent"].Width = (int)((double)((Control)(object)ULGMotivation).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGMotivation).DisplayLayout.Bands[0].Columns["IsSalary"].Header).Caption = (GlobalVariables.IsArabic ? "في الراتب" : "IsSalary");
		((UltraGridBase)ULGMotivation).DisplayLayout.Bands[0].Columns["IsSalary"].Hidden = false;
		((UltraGridBase)ULGMotivation).DisplayLayout.Bands[0].Columns["IsPercent"].Width = (int)((double)((Control)(object)ULGMotivation).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGMotivation).DisplayLayout.Bands[0].Columns["Value"].Header).Caption = (GlobalVariables.IsArabic ? "القيمه" : "Value");
		((UltraGridBase)ULGMotivation).DisplayLayout.Bands[0].Columns["Value"].Hidden = false;
		((UltraGridBase)ULGMotivation).DisplayLayout.Bands[0].Columns["Value"].Width = (int)((double)((Control)(object)ULGMotivation).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGMotivation).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "البيان" : "Notes");
		((UltraGridBase)ULGMotivation).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGMotivation).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGMotivation).Width * 0.25) - GlobalVariables.ScrollWidth;
	}

	public override int TreeAddData()
	{
		int num = 0;
		Main.StartBulkTrans(FromServer: true);
		try
		{
			num = SubAccounts.Insert_Update("-1", GetCode(), ((Control)(object)txtName).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? "Null" : ((Control)(object)txtNameEn).Text.Trim(), (SelectedNode == null) ? "Null" : ((DataRow)((SubObjectBase)SelectedNode).Tag)[0].ToString(), "0", (NodeLevel + 1).ToString(), "2", "1", "0", ((TextEditorControlBase)cboBranch).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			SubAccounts_Details.Insert_UpdateByAccIDs(num.ToString(), TreeFunctions.GetTreeCheckedNodesIDs(TreeAccounts), "0", GlobalVariables.CurrentBranchID, IsFromServer: true);
			string treeFirstCheckedNodeID = TreeFunctions.GetTreeFirstCheckedNodeID(treeAdministrativeStructure);
			Employees.Insert_Update((((Control)(object)txtEmployeeNo).Text == "") ? GetCode() : ((Control)(object)txtEmployeeNo).Text, ((Control)(object)txtOrder).Text, num.ToString(), ((UltraToggleEditorBase)chkIsSalesMan).Checked ? "1" : "0", (!((UltraToggleEditorBase)chkIsSalesMan).Checked) ? "Null" : ((cboSalesManDefaultStore.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesManDefaultStore).Value.ToString()), (((Control)(object)txtSalesCommission).Text == "") ? "0" : ((Control)(object)txtSalesCommission).Text, (((Control)(object)txtCollectionCommission).Text == "") ? "0" : ((Control)(object)txtCollectionCommission).Text, (((Control)(object)txtPersonalIDNo).Text == "") ? "Null" : ((Control)(object)txtPersonalIDNo).Text, (dtpIDIssueDate.Value == null) ? "Null" : dtpIDIssueDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpIDExpireDate.Value == null) ? "Null" : dtpIDExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpBirthDate.Value == null) ? "Null" : dtpBirthDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboGender.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboGender).Value.ToString(), (cboCountry.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCountry).Value.ToString(), (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), (((Control)(object)txtAddress).Text == "") ? "Null" : ((Control)(object)txtAddress).Text, (cboNationality.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboNationality).Value.ToString(), (cboMilitaryService.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboMilitaryService).Value.ToString(), (dtpPostponedToDate.Value == null) ? "Null" : dtpPostponedToDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboSocialStatus.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSocialStatus).Value.ToString(), (cboQualification.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboQualification).Value.ToString(), (cboUniversity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUniversity).Value.ToString(), (cboSectionName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSectionName).Value.ToString(), (cboQualificationYear.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboQualificationYear).Value.ToString(), (((Control)(object)txtSectionNameNotes).Text == "") ? "Null" : ((Control)(object)txtSectionNameNotes).Text, (((Control)(object)txtEMail).Text == "") ? "Null" : ((Control)(object)txtEMail).Text, (cboState.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboState).Value.ToString(), (treeFirstCheckedNodeID == "") ? "Null" : treeFirstCheckedNodeID, (cboAdministrativeLevel.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAdministrativeLevel).Value.ToString(), (cboDegree.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDegree).Value.ToString(), (cboPosition.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPosition).Value.ToString(), (cboDirectManager.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDirectManager).Value.ToString(), (dtpHireDate.Value == null) ? "Null" : dtpHireDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpEndDate.Value == null) ? "Null" : dtpEndDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboLeavingWorkReason.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLeavingWorkReason).Value.ToString(), (((Control)(object)txtLeavingWorkNotes).Text == "") ? "Null" : ((Control)(object)txtLeavingWorkNotes).Text, (dtpContractFromDate.Value == null) ? "Null" : dtpContractFromDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpContractToDate.Value == null) ? "Null" : dtpContractToDate.DateTime.ToString(GlobalVariables.DateShortFormate), (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, ((UltraToggleEditorBase)chkHasPassport).Checked ? "1" : "0", (((Control)(object)txtPassportNo).Text == "") ? "Null" : ((Control)(object)txtPassportNo).Text, (dtpPassportExpireDate.Value == null) ? "Null" : dtpPassportExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), "Null", (((Control)(object)txtDrivingLicenseID).Text == "") ? "Null" : ((Control)(object)txtDrivingLicenseID).Text, (dtpDrivingLicenseExpireDate.Value == null) ? "Null" : dtpDrivingLicenseExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboBank.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBank).Value.ToString(), (((Control)(object)txtBankAccountNo).Text == "") ? "Null" : ((Control)(object)txtBankAccountNo).Text, (dtpFirstInssuranceDate.Value == null) ? "Null" : dtpFirstInssuranceDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpInssuranceEndDate.Value == null) ? "Null" : dtpInssuranceEndDate.DateTime.ToString(GlobalVariables.DateShortFormate), (((Control)(object)txtSocialInssuranceNo).Text == "") ? "Null" : ((Control)(object)txtSocialInssuranceNo).Text, (((Control)(object)txtSocialInsuranceValue).Text == "") ? "0" : ((Control)(object)txtSocialInsuranceValue).Text, (((Control)(object)txtInclusiveHealthInsuranceValue).Text == "") ? "0" : ((Control)(object)txtInclusiveHealthInsuranceValue).Text, (cboSocialInssuranceOffice.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSocialInssuranceOffice).Value.ToString(), (dtpInssuranceDate.Value == null) ? "Null" : dtpInssuranceDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((UltraToggleEditorBase)chkHasPrivateCar).Checked ? "1" : "0", (((Control)(object)txtWorkOfficePermissionCode).Text == "") ? "Null" : ((Control)(object)txtWorkOfficePermissionCode).Text, (dtpWorkOfficeFromDate.Value == null) ? "Null" : dtpWorkOfficeFromDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpWorkOfficeToDate.Value == null) ? "Null" : dtpWorkOfficeToDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpWorkOfficeSendDate.Value == null) ? "Null" : dtpWorkOfficeSendDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((UltraToggleEditorBase)chkWorkOfficeIsSend).Checked ? "1" : "0", "0", "0", "0", "0", ((UltraToggleEditorBase)chkIsMonthlySalary).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInSalaryTax).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInServicePercent).Checked ? "1" : "0", (dtpServicePercentFromDate.Value == null) ? "Null" : dtpServicePercentFromDate.DateTime.ToString(GlobalVariables.DateShortFormate), (((Control)(object)txtStartBasicSalary).Text == "") ? "0" : ((Control)(object)txtStartBasicSalary).Text, (((Control)(object)txtStartVariantSalary).Text == "") ? "0" : ((Control)(object)txtStartVariantSalary).Text, (((Control)(object)txtStartSalary).Text == "") ? "0" : ((Control)(object)txtStartSalary).Text, (((Control)(object)txtCurrentBasicSalary).Text == "") ? "0" : ((Control)(object)txtCurrentBasicSalary).Text, (((Control)(object)txtCurrentVariantSalary).Text == "") ? "0" : ((Control)(object)txtCurrentVariantSalary).Text, (((Control)(object)txtNotInsuranceVariantSalary).Text == "") ? "0" : ((Control)(object)txtNotInsuranceVariantSalary).Text, (((Control)(object)txtNetCurrentSalary).Text == "") ? "0" : ((Control)(object)txtNetCurrentSalary).Text, ((UltraToggleEditorBase)chkIsFixedSalaryTax).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsFixedSalaryTax).Checked ? ((Control)(object)txtFixedSalaryValue).Text : "Null", ((UltraToggleEditorBase)chkIsFixedTaxPercentage).Checked ? "1" : "0", ((UltraToggleEditorBase)chkSalaryAtEndOFMonth).Checked ? "1" : "0", (cboHealthInsuranceType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboHealthInsuranceType).Value.ToString(), (dtpHealthInsuranceStartDate.Value == null) ? "Null" : dtpHealthInsuranceStartDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpHealthInsuranceEndDate.Value == null) ? "Null" : dtpHealthInsuranceEndDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboExtraTimeRule.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboExtraTimeRule).Value.ToString(), (cboLateRule.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLateRule).Value.ToString(), (cboAbsenceRule.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAbsenceRule).Value.ToString(), (cboPenalityRule.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPenalityRule).Value.ToString(), (cboVacationRule.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboVacationRule).Value.ToString(), (cboLeavePermissionRule.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLeavePermissionRule).Value.ToString(), (cboFeedingRule.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboFeedingRule).Value.ToString(), (cboSalaryList.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalaryList).Value.ToString(), (cboCurrency.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCurrency).Value.ToString(), (cboSalaryAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalaryAccount).Value.ToString(), (cboAdvanceAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAdvanceAccount).Value.ToString(), (cboPenaltyAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPenaltyAccount).Value.ToString(), "0", ((TextEditorControlBase)cboBranch).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			if (ImageChanged)
			{
				Employees.EmployeeImage_Update(num.ToString(), (Image)picEmployee.Image, IsFromServer: true);
			}
			dtContacts.AcceptChanges();
			if (dtContacts.Rows.Count > 0)
			{
				for (int i = 0; i < dtContacts.Rows.Count; i++)
				{
					dtContacts.Rows[i]["SubAccountID"] = num;
					dtContacts.Rows[i]["BranchID"] = ((TextEditorControlBase)cboBranch).Value.ToString();
				}
				EmployeesContacts.Insert_UpdateByTable(dtContacts, GlobalVariables.CurrentBranchID, IsFromServer: true);
			}
			dtEmployeeFamilyRelatives.AcceptChanges();
			if (dtEmployeeFamilyRelatives.Rows.Count > 0)
			{
				for (int j = 0; j < dtEmployeeFamilyRelatives.Rows.Count; j++)
				{
					dtEmployeeFamilyRelatives.Rows[j]["SubAccountID"] = num;
					dtEmployeeFamilyRelatives.Rows[j]["BranchID"] = ((TextEditorControlBase)cboBranch).Value.ToString();
				}
				EmployeesFamilyRelativesHealthInsurance.Insert_UpdateByTable(dtEmployeeFamilyRelatives, GlobalVariables.CurrentBranchID, IsFromServer: true);
			}
			dtDocuments.AcceptChanges();
			if (dtDocuments.Rows.Count > 0)
			{
				for (int k = 0; k < dtDocuments.Rows.Count; k++)
				{
					dtDocuments.Rows[k]["SubAccountID"] = num;
					dtDocuments.Rows[k]["BranchID"] = ((TextEditorControlBase)cboBranch).Value.ToString();
				}
				EmployeesDocuments.Insert_UpdateByTable(dtDocuments, GlobalVariables.CurrentBranchID, IsFromServer: true);
			}
			dtCertificates.AcceptChanges();
			if (dtCertificates.Rows.Count > 0)
			{
				for (int l = 0; l < dtCertificates.Rows.Count; l++)
				{
					dtCertificates.Rows[l]["SubAccountID"] = num;
					dtCertificates.Rows[l]["BranchID"] = ((TextEditorControlBase)cboBranch).Value.ToString();
				}
				EmployeesCertificates.Insert_UpdateByTable(dtCertificates, GlobalVariables.CurrentBranchID, IsFromServer: true);
			}
			dtEmployeeAllownces.AcceptChanges();
			if (dtEmployeeAllownces.Rows.Count > 0)
			{
				for (int m = 0; m < dtEmployeeAllownces.Rows.Count; m++)
				{
					dtEmployeeAllownces.Rows[m]["SubAccountID"] = num;
					dtEmployeeAllownces.Rows[m]["IsSalary"] = 1;
					dtEmployeeAllownces.Rows[m]["BranchID"] = ((TextEditorControlBase)cboBranch).Value.ToString();
				}
				EmployeesAllowances.Insert_UpdateByTable(dtEmployeeAllownces, GlobalVariables.CurrentBranchID, IsFromServer: true);
			}
			dtEmployeeDeductions.AcceptChanges();
			if (dtEmployeeDeductions.Rows.Count > 0)
			{
				for (int n = 0; n < dtEmployeeDeductions.Rows.Count; n++)
				{
					dtEmployeeDeductions.Rows[n]["SubAccountID"] = num;
					dtEmployeeDeductions.Rows[n]["IsSalary"] = 1;
					dtEmployeeDeductions.Rows[n]["BranchID"] = ((TextEditorControlBase)cboBranch).Value.ToString();
				}
				EmployeesDeductions.Insert_UpdateByTable(dtEmployeeDeductions, GlobalVariables.CurrentBranchID, IsFromServer: true);
			}
			dtEmployeeYearIncreases.AcceptChanges();
			if (dtEmployeeYearIncreases.Rows.Count > 0)
			{
				for (int num2 = 0; num2 < dtEmployeeYearIncreases.Rows.Count; num2++)
				{
					dtEmployeeYearIncreases.Rows[num2]["SubAccountID"] = num;
					dtEmployeeYearIncreases.Rows[num2]["BranchID"] = ((TextEditorControlBase)cboBranch).Value.ToString();
				}
				EmployeeYearIncreases.Insert_UpdateByTable(dtEmployeeYearIncreases, GlobalVariables.CurrentBranchID, IsFromServer: true);
			}
			dtEmployeeMotivations.AcceptChanges();
			if (dtEmployeeMotivations.Rows.Count > 0)
			{
				for (int num3 = 0; num3 < dtEmployeeMotivations.Rows.Count; num3++)
				{
					dtEmployeeMotivations.Rows[num3]["SubAccountID"] = num;
					dtEmployeeMotivations.Rows[num3]["BranchID"] = ((TextEditorControlBase)cboBranch).Value.ToString();
				}
				EmployeesMotivations.Insert_UpdateByTable(dtEmployeeMotivations, GlobalVariables.CurrentBranchID, IsFromServer: true);
			}
			Main.EndBulkTrans(FromServer: true);
			ClearControls();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
		return num;
	}

	public override void TreeUpdateData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			SubAccounts.Insert_Update(((KeyedSubObjectBase)SelectedNode).Key, GetCode(), ((Control)(object)txtName).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? "Null" : ((Control)(object)txtNameEn).Text.Trim(), (SelectedNode.Parent == null) ? "Null" : ((KeyedSubObjectBase)SelectedNode.Parent).Key, ((bool)((DataRow)((SubObjectBase)SelectedNode).Tag)[IsMainCol]) ? "1" : "0", NodeLevel.ToString(), ((DataRow)((SubObjectBase)SelectedNode).Tag)[AdditionalCol1].ToString(), "1", "0", ((TextEditorControlBase)cboBranch).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			SubAccounts_Details.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			string treeCheckedNodesIDs = TreeFunctions.GetTreeCheckedNodesIDs(TreeAccounts);
			SubAccounts_Details.Insert_UpdateByAccIDs(((KeyedSubObjectBase)SelectedNode).Key, treeCheckedNodesIDs, "0", GlobalVariables.CurrentBranchID, IsFromServer: true);
			string treeFirstCheckedNodeID = TreeFunctions.GetTreeFirstCheckedNodeID(treeAdministrativeStructure);
			if (((DisposableObjectCollectionBase)SelectedNode.Nodes).Count > 0)
			{
				GlobalVariables.QuestionMB.Show("هل تريد ربط كل محتويات المجموعه بنفس الحسابات؟", "Would you like to Bind All Group Contents With The Same Accounts?");
				if (GlobalVariables.MessageBoxResult == 'Y')
				{
					SaveAccountsToChildNades(SelectedNode, treeCheckedNodesIDs);
				}
			}
			EmployeesCertificates.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			EmployeesContacts.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			EmployeesDocuments.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			EmployeesFamilyRelativesHealthInsurance.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			EmployeesAllowances.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, "1", GlobalVariables.UserID, IsFromServer: true);
			EmployeesDeductions.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, "1", GlobalVariables.UserID, IsFromServer: true);
			EmployeeYearIncreases.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			EmployeesMotivations.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			Employees.Insert_Update((((Control)(object)txtEmployeeNo).Text == "") ? GetCode() : ((Control)(object)txtEmployeeNo).Text, ((Control)(object)txtOrder).Text, ((KeyedSubObjectBase)SelectedNode).Key, ((UltraToggleEditorBase)chkIsSalesMan).Checked ? "1" : "0", (!((UltraToggleEditorBase)chkIsSalesMan).Checked) ? "Null" : ((cboSalesManDefaultStore.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesManDefaultStore).Value.ToString()), (((Control)(object)txtSalesCommission).Text == "") ? "0" : ((Control)(object)txtSalesCommission).Text, (((Control)(object)txtCollectionCommission).Text == "") ? "0" : ((Control)(object)txtCollectionCommission).Text, (((Control)(object)txtPersonalIDNo).Text == "") ? "Null" : ((Control)(object)txtPersonalIDNo).Text, (dtpIDIssueDate.Value == null) ? "Null" : dtpIDIssueDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpIDExpireDate.Value == null) ? "Null" : dtpIDExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpBirthDate.Value == null) ? "Null" : dtpBirthDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboGender.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboGender).Value.ToString(), (cboCountry.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCountry).Value.ToString(), (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), (((Control)(object)txtAddress).Text == "") ? "Null" : ((Control)(object)txtAddress).Text, (cboNationality.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboNationality).Value.ToString(), (cboMilitaryService.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboMilitaryService).Value.ToString(), (dtpPostponedToDate.Value == null) ? "Null" : dtpPostponedToDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboSocialStatus.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSocialStatus).Value.ToString(), (cboQualification.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboQualification).Value.ToString(), (cboUniversity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUniversity).Value.ToString(), (cboSectionName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSectionName).Value.ToString(), (cboQualificationYear.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboQualificationYear).Value.ToString(), (((Control)(object)txtSectionNameNotes).Text == "") ? "Null" : ((Control)(object)txtSectionNameNotes).Text, (((Control)(object)txtEMail).Text == "") ? "Null" : ((Control)(object)txtEMail).Text, (cboState.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboState).Value.ToString(), (treeFirstCheckedNodeID == "") ? "Null" : treeFirstCheckedNodeID, (cboAdministrativeLevel.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAdministrativeLevel).Value.ToString(), (cboDegree.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDegree).Value.ToString(), (cboPosition.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPosition).Value.ToString(), (cboDirectManager.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDirectManager).Value.ToString(), (dtpHireDate.Value == null) ? "Null" : dtpHireDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpEndDate.Value == null) ? "Null" : dtpEndDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboLeavingWorkReason.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLeavingWorkReason).Value.ToString(), (((Control)(object)txtLeavingWorkNotes).Text == "") ? "Null" : ((Control)(object)txtLeavingWorkNotes).Text, (dtpContractFromDate.Value == null) ? "Null" : dtpContractFromDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpContractToDate.Value == null) ? "Null" : dtpContractToDate.DateTime.ToString(GlobalVariables.DateShortFormate), (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, ((UltraToggleEditorBase)chkHasPassport).Checked ? "1" : "0", (((Control)(object)txtPassportNo).Text == "") ? "Null" : ((Control)(object)txtPassportNo).Text, (dtpPassportExpireDate.Value == null) ? "Null" : dtpPassportExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), (ApplicantID == "") ? "Null" : ApplicantID, (((Control)(object)txtDrivingLicenseID).Text == "") ? "Null" : ((Control)(object)txtDrivingLicenseID).Text, (dtpDrivingLicenseExpireDate.Value == null) ? "Null" : dtpDrivingLicenseExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboBank.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBank).Value.ToString(), (((Control)(object)txtBankAccountNo).Text == "") ? "Null" : ((Control)(object)txtBankAccountNo).Text, (dtpFirstInssuranceDate.Value == null) ? "Null" : dtpFirstInssuranceDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpInssuranceEndDate.Value == null) ? "Null" : dtpInssuranceEndDate.DateTime.ToString(GlobalVariables.DateShortFormate), (((Control)(object)txtSocialInssuranceNo).Text == "") ? "Null" : ((Control)(object)txtSocialInssuranceNo).Text, (((Control)(object)txtSocialInsuranceValue).Text == "") ? "0" : ((Control)(object)txtSocialInsuranceValue).Text, (((Control)(object)txtInclusiveHealthInsuranceValue).Text == "") ? "0" : ((Control)(object)txtInclusiveHealthInsuranceValue).Text, (cboSocialInssuranceOffice.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSocialInssuranceOffice).Value.ToString(), (dtpInssuranceDate.Value == null) ? "Null" : dtpInssuranceDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((UltraToggleEditorBase)chkHasPrivateCar).Checked ? "1" : "0", (((Control)(object)txtWorkOfficePermissionCode).Text == "") ? "Null" : ((Control)(object)txtWorkOfficePermissionCode).Text, (dtpWorkOfficeFromDate.Value == null) ? "Null" : dtpWorkOfficeFromDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpWorkOfficeToDate.Value == null) ? "Null" : dtpWorkOfficeToDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpWorkOfficeSendDate.Value == null) ? "Null" : dtpWorkOfficeSendDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((UltraToggleEditorBase)chkWorkOfficeIsSend).Checked ? "1" : "0", "0", "0", "0", "0", ((UltraToggleEditorBase)chkIsMonthlySalary).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInSalaryTax).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInServicePercent).Checked ? "1" : "0", (dtpServicePercentFromDate.Value == null) ? "Null" : dtpServicePercentFromDate.DateTime.ToString(GlobalVariables.DateShortFormate), (((Control)(object)txtStartBasicSalary).Text == "") ? "0" : ((Control)(object)txtStartBasicSalary).Text, (((Control)(object)txtStartVariantSalary).Text == "") ? "0" : ((Control)(object)txtStartVariantSalary).Text, (((Control)(object)txtStartSalary).Text == "") ? "0" : ((Control)(object)txtStartSalary).Text, (((Control)(object)txtCurrentBasicSalary).Text == "") ? "0" : ((Control)(object)txtCurrentBasicSalary).Text, (((Control)(object)txtCurrentVariantSalary).Text == "") ? "0" : ((Control)(object)txtCurrentVariantSalary).Text, (((Control)(object)txtNotInsuranceVariantSalary).Text == "") ? "0" : ((Control)(object)txtNotInsuranceVariantSalary).Text, (((Control)(object)txtNetCurrentSalary).Text == "") ? "0" : ((Control)(object)txtNetCurrentSalary).Text, ((UltraToggleEditorBase)chkIsFixedSalaryTax).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsFixedSalaryTax).Checked ? ((Control)(object)txtFixedSalaryValue).Text : "Null", ((UltraToggleEditorBase)chkIsFixedTaxPercentage).Checked ? "1" : "0", ((UltraToggleEditorBase)chkSalaryAtEndOFMonth).Checked ? "1" : "0", (cboHealthInsuranceType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboHealthInsuranceType).Value.ToString(), (dtpHealthInsuranceStartDate.Value == null) ? "Null" : dtpHealthInsuranceStartDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpHealthInsuranceEndDate.Value == null) ? "Null" : dtpHealthInsuranceEndDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboExtraTimeRule.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboExtraTimeRule).Value.ToString(), (cboLateRule.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLateRule).Value.ToString(), (cboAbsenceRule.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAbsenceRule).Value.ToString(), (cboPenalityRule.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPenalityRule).Value.ToString(), (cboVacationRule.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboVacationRule).Value.ToString(), (cboLeavePermissionRule.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLeavePermissionRule).Value.ToString(), (cboFeedingRule.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboFeedingRule).Value.ToString(), (cboSalaryList.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalaryList).Value.ToString(), (cboCurrency.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCurrency).Value.ToString(), (cboSalaryAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalaryAccount).Value.ToString(), (cboAdvanceAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAdvanceAccount).Value.ToString(), (cboPenaltyAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPenaltyAccount).Value.ToString(), "0", ((TextEditorControlBase)cboBranch).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			if (ImageChanged)
			{
				Employees.EmployeeImage_Update(((KeyedSubObjectBase)SelectedNode).Key, (Image)picEmployee.Image, IsFromServer: true);
			}
			dtContacts.AcceptChanges();
			if (dtContacts.Rows.Count > 0)
			{
				for (int i = 0; i < dtContacts.Rows.Count; i++)
				{
					dtContacts.Rows[i]["EmployeeContactID"] = -1;
					dtContacts.Rows[i]["SubAccountID"] = ((KeyedSubObjectBase)SelectedNode).Key;
					dtContacts.Rows[i]["BranchID"] = ((TextEditorControlBase)cboBranch).Value.ToString();
				}
				EmployeesContacts.Insert_UpdateByTable(dtContacts, GlobalVariables.CurrentBranchID, IsFromServer: true);
			}
			dtEmployeeFamilyRelatives.AcceptChanges();
			if (dtEmployeeFamilyRelatives.Rows.Count > 0)
			{
				for (int j = 0; j < dtEmployeeFamilyRelatives.Rows.Count; j++)
				{
					dtEmployeeFamilyRelatives.Rows[j]["EmployeeFamilyRelativeHealthInsuranceID"] = -1;
					dtEmployeeFamilyRelatives.Rows[j]["SubAccountID"] = ((KeyedSubObjectBase)SelectedNode).Key;
					dtEmployeeFamilyRelatives.Rows[j]["BranchID"] = ((TextEditorControlBase)cboBranch).Value.ToString();
				}
				EmployeesFamilyRelativesHealthInsurance.Insert_UpdateByTable(dtEmployeeFamilyRelatives, GlobalVariables.CurrentBranchID, IsFromServer: true);
			}
			dtDocuments.AcceptChanges();
			if (dtDocuments.Rows.Count > 0)
			{
				for (int k = 0; k < dtDocuments.Rows.Count; k++)
				{
					dtDocuments.Rows[k]["EmployeeDocumentID"] = -1;
					dtDocuments.Rows[k]["SubAccountID"] = ((KeyedSubObjectBase)SelectedNode).Key;
					dtDocuments.Rows[k]["BranchID"] = ((TextEditorControlBase)cboBranch).Value.ToString();
				}
				EmployeesDocuments.Insert_UpdateByTable(dtDocuments, GlobalVariables.CurrentBranchID, IsFromServer: true);
			}
			dtCertificates.AcceptChanges();
			if (dtCertificates.Rows.Count > 0)
			{
				for (int l = 0; l < dtCertificates.Rows.Count; l++)
				{
					dtCertificates.Rows[l]["EmployeeCertificateID"] = -1;
					dtCertificates.Rows[l]["SubAccountID"] = ((KeyedSubObjectBase)SelectedNode).Key;
					dtCertificates.Rows[l]["BranchID"] = ((TextEditorControlBase)cboBranch).Value.ToString();
				}
				EmployeesCertificates.Insert_UpdateByTable(dtCertificates, GlobalVariables.CurrentBranchID, IsFromServer: true);
			}
			dtEmployeeAllownces.AcceptChanges();
			if (dtEmployeeAllownces.Rows.Count > 0)
			{
				for (int m = 0; m < dtEmployeeAllownces.Rows.Count; m++)
				{
					dtEmployeeAllownces.Rows[m]["EmployeeAllowanceID"] = -1;
					dtEmployeeAllownces.Rows[m]["SubAccountID"] = ((KeyedSubObjectBase)SelectedNode).Key;
					dtEmployeeAllownces.Rows[m]["IsSalary"] = 1;
					dtEmployeeAllownces.Rows[m]["BranchID"] = ((TextEditorControlBase)cboBranch).Value.ToString();
				}
				EmployeesAllowances.Insert_UpdateByTable(dtEmployeeAllownces, GlobalVariables.CurrentBranchID, IsFromServer: true);
			}
			dtEmployeeDeductions.AcceptChanges();
			if (dtEmployeeDeductions.Rows.Count > 0)
			{
				for (int n = 0; n < dtEmployeeDeductions.Rows.Count; n++)
				{
					dtEmployeeDeductions.Rows[n]["EmployeeDeductionID"] = -1;
					dtEmployeeDeductions.Rows[n]["SubAccountID"] = ((KeyedSubObjectBase)SelectedNode).Key;
					dtEmployeeDeductions.Rows[n]["IsSalary"] = 1;
					dtEmployeeDeductions.Rows[n]["BranchID"] = ((TextEditorControlBase)cboBranch).Value.ToString();
				}
				EmployeesDeductions.Insert_UpdateByTable(dtEmployeeDeductions, GlobalVariables.CurrentBranchID, IsFromServer: true);
			}
			dtEmployeeYearIncreases.AcceptChanges();
			if (dtEmployeeYearIncreases.Rows.Count > 0)
			{
				for (int num = 0; num < dtEmployeeYearIncreases.Rows.Count; num++)
				{
					dtEmployeeYearIncreases.Rows[num]["EmployeeYearIncreaseID"] = -1;
					dtEmployeeYearIncreases.Rows[num]["SubAccountID"] = ((KeyedSubObjectBase)SelectedNode).Key;
					dtEmployeeYearIncreases.Rows[num]["BranchID"] = ((TextEditorControlBase)cboBranch).Value.ToString();
				}
				EmployeeYearIncreases.Insert_UpdateByTable(dtEmployeeYearIncreases, GlobalVariables.CurrentBranchID, IsFromServer: true);
			}
			dtEmployeeMotivations.AcceptChanges();
			if (dtEmployeeMotivations.Rows.Count > 0)
			{
				for (int num2 = 0; num2 < dtEmployeeMotivations.Rows.Count; num2++)
				{
					dtEmployeeMotivations.Rows[num2]["EmployeeMotivationID"] = -1;
					dtEmployeeMotivations.Rows[num2]["SubAccountID"] = ((KeyedSubObjectBase)SelectedNode).Key;
					dtEmployeeMotivations.Rows[num2]["BranchID"] = ((TextEditorControlBase)cboBranch).Value.ToString();
				}
				EmployeesMotivations.Insert_UpdateByTable(dtEmployeeMotivations, GlobalVariables.CurrentBranchID, IsFromServer: true);
			}
			Main.EndBulkTrans(FromServer: true);
			ClearControls();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void SaveAccountsToChildNades(UltraTreeNode Node, string AccountIDs)
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)Node.Nodes).Count; i++)
		{
			SubAccounts_Details.DeleteBySubAccountID(((KeyedSubObjectBase)Node.Nodes[i]).Key, GlobalVariables.UserID, IsFromServer: true);
			SubAccounts_Details.Insert_UpdateByAccIDs(((KeyedSubObjectBase)Node.Nodes[i]).Key, AccountIDs, "0", GlobalVariables.CurrentBranchID, IsFromServer: true);
			if (((DisposableObjectCollectionBase)Node.Nodes[i].Nodes).Count > 0)
			{
				SaveAccountsToChildNades(Node.Nodes[i], AccountIDs);
			}
		}
	}

	public override void TreeDeleteData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			EmployeesCertificates.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			EmployeesContacts.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			EmployeesDocuments.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			EmployeesFamilyRelativesHealthInsurance.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			EmployeesAllowances.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, "-1", GlobalVariables.UserID, IsFromServer: true);
			EmployeesDeductions.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, "-1", GlobalVariables.UserID, IsFromServer: true);
			EmployeeYearIncreases.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			EmployeesMotivations.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			Employees.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			SubAccounts_Details.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			SubAccounts.Delete(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void ULGFamilyRelativesHealthInsurance_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGFamilyRelativesHealthInsurance).ActiveRow).Selected = true;
		}
	}

	private void chkIsSalesMan_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboSalesManDefaultStore).Enabled = ((UltraToggleEditorBase)chkIsSalesMan).Checked;
	}

	private void txtEmployeeNo_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && !Adding && !Updating)
		{
			DataTable dataTable = Employees.SelectByEmployeeCode(((Control)(object)txtEmployeeNo).Text, IsFromServer: true);
			if (dataTable.Rows.Count > 0)
			{
				treeChart.CollapseAll();
				treeChart.ActiveNode = treeChart.GetNodeByKey(dataTable.Rows[0]["SubAccountID"].ToString());
				treeChart.ActiveNode.Selected = true;
			}
		}
	}

	private void cboBranch_ValueChanged(object sender, EventArgs e)
	{
		if (cboBranch.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtStores);
			dataView.RowFilter = " Locked =0  And BranchID=" + ((TextEditorControlBase)cboBranch).Value.ToString();
			DataTable dt = dataView.ToTable();
			GlobalFunctions.FillCombo(cboSalesManDefaultStore, dt, "StoreID", "StoreName");
		}
		else
		{
			((TextEditorControlBase)cboSalesManDefaultStore).Clear();
		}
	}

	private void dtpHireDate_ValueChanged(object sender, EventArgs e)
	{
		dtpHireDate.ValueChanged -= dtpHireDate_ValueChanged;
		dtpContractFromDate.ValueChanged -= dtpContractFromDate_ValueChanged;
		dtpContractFromDate.Value = dtpHireDate.Value;
		if (dtpHireDate.Value != null)
		{
			dtpContractToDate.Value = dtpHireDate.DateTime.AddYears(1).AddDays(-1.0);
		}
		else
		{
			dtpContractToDate.Value = null;
		}
		dtpContractFromDate.ValueChanged += dtpContractFromDate_ValueChanged;
		dtpHireDate.ValueChanged += dtpHireDate_ValueChanged;
	}

	private void dtpContractFromDate_ValueChanged(object sender, EventArgs e)
	{
		dtpContractFromDate.ValueChanged -= dtpContractFromDate_ValueChanged;
		if (dtpContractFromDate.Value != null)
		{
			dtpContractToDate.Value = dtpContractFromDate.DateTime.AddYears(1).AddDays(-1.0);
		}
		else
		{
			dtpContractToDate.Value = null;
		}
		dtpContractFromDate.ValueChanged += dtpContractFromDate_ValueChanged;
	}

	public override bool ValidateData()
	{
		CalculateTotalSalary();
		if (cboBranch.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل الفرع", "Please Enter Branch");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["JobData"];
			((TextEditorControlBase)cboBranch).Focus();
			cboBranch.DropDown();
			return false;
		}
		if (cboState.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل حالة الموظف ", "Enter Employee State");
			((TextEditorControlBase)cboState).Focus();
			cboState.DropDown();
			return false;
		}
		if (cboAbsenceRule.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل لائحة الغياب ", "Enter Employee Absence Rule");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Setting"];
			((TextEditorControlBase)cboAbsenceRule).Focus();
			cboAbsenceRule.DropDown();
			return false;
		}
		if (cboExtraTimeRule.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل لائحة الإضافي ", "Enter Employee OverTime Rule");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Setting"];
			((TextEditorControlBase)cboExtraTimeRule).Focus();
			cboExtraTimeRule.DropDown();
			return false;
		}
		if (cboLateRule.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل لائحة التأخير ", "Enter Employee Late Rule");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Setting"];
			((TextEditorControlBase)cboLateRule).Focus();
			cboLateRule.DropDown();
			return false;
		}
		if (cboLeavePermissionRule.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل لائحة الاذونات ", "Enter Employee Leave Permission Rule");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Setting"];
			((TextEditorControlBase)cboLeavePermissionRule).Focus();
			cboLeavePermissionRule.DropDown();
			return false;
		}
		if (cboPenalityRule.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل لائحة الجزاءات ", "Enter Employee Penality Rule");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Setting"];
			((TextEditorControlBase)cboPenalityRule).Focus();
			cboPenalityRule.DropDown();
			return false;
		}
		if (cboVacationRule.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل لائحة الإجازات ", "Enter Employee Vacation Rule");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Setting"];
			((TextEditorControlBase)cboVacationRule).Focus();
			cboVacationRule.DropDown();
			return false;
		}
		if (cboAdvanceAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل حساب السلف ", "Enter Employee Advance Account");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Setting"];
			((TextEditorControlBase)cboAdvanceAccount).Focus();
			cboAdvanceAccount.DropDown();
			return false;
		}
		if (cboPenaltyAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل حساب الجزاءات ", "Enter Employee Penalty Account");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Setting"];
			((TextEditorControlBase)cboPenaltyAccount).Focus();
			cboPenaltyAccount.DropDown();
			return false;
		}
		if (dtpHealthInsuranceStartDate.Value != null && dtpHealthInsuranceEndDate.Value != null && Convert.ToDateTime(dtpHealthInsuranceStartDate.Value) > Convert.ToDateTime(dtpHealthInsuranceEndDate.Value))
		{
			GlobalVariables.InformationMB.Show("تاريخ انتهاء التأمين الصحي بعد تاريخ بداية التأمين الصحي", "The Health Insurance Start date is after the Health Insurance End date and this is not allowed");
			((Control)(object)dtpHealthInsuranceStartDate).Focus();
			return false;
		}
		if (dtpHireDate.Value != null && dtpInssuranceDate.Value != null && Convert.ToDateTime(dtpHireDate.Value) > Convert.ToDateTime(dtpInssuranceDate.Value))
		{
			GlobalVariables.InformationMB.Show("تاريخ التعيين بعد تاريخ التأمينات", "The Hire date is after the Insurance date and this is not allowed");
			((Control)(object)dtpInssuranceDate).Focus();
			return false;
		}
		if (dtpInssuranceDate.Value != null && dtpFirstInssuranceDate.Value != null && Convert.ToDateTime(dtpInssuranceDate.Value) > Convert.ToDateTime(dtpFirstInssuranceDate.Value))
		{
			GlobalVariables.InformationMB.Show("تاريخ  التأمينات بعد تاريخ بداية التأمينات", "The Inssurance date is after the first Insurance End date and this is not allowed");
			((Control)(object)dtpFirstInssuranceDate).Focus();
			return false;
		}
		if (dtpFirstInssuranceDate.Value != null && dtpInssuranceEndDate.Value != null && Convert.ToDateTime(dtpFirstInssuranceDate.Value) > Convert.ToDateTime(dtpInssuranceEndDate.Value))
		{
			GlobalVariables.InformationMB.Show("تاريخ بدايه التأمينات بعد تاريخ انتهاء التأمينات", "The First Inssurance date is after the Insurance End date and this is not allowed");
			((Control)(object)dtpFirstInssuranceDate).Focus();
			return false;
		}
		if (dtpContractFromDate.Value != null && dtpContractToDate.Value != null && Convert.ToDateTime(dtpContractFromDate.Value) > Convert.ToDateTime(dtpContractToDate.Value))
		{
			GlobalVariables.InformationMB.Show("تاريخ بداية التعاقد بعد تاريخ نهاية التعاقد", "The Contract start date is after the Contract end date and this is not allowed");
			((Control)(object)dtpInssuranceDate).Focus();
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGFamilyRelativesHealthInsurance).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGFamilyRelativesHealthInsurance).Rows[i].Cells["FamilyRelativeNameAr"].Value == DBNull.Value || ((UltraGridBase)ULGFamilyRelativesHealthInsurance).Rows[i].Cells["FamilyRelativeNameAr"].Value.ToString().Trim() == "")
			{
				GlobalVariables.InformationMB.Show("من فضلك ادخل الاسم بالعربية ", "Please Select The Arabic Name");
				((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Inssurance"];
				ULGFamilyRelativesHealthInsurance.ActiveCell = ((UltraGridBase)ULGFamilyRelativesHealthInsurance).Rows[i].Cells["FamilyRelativeNameAr"];
				return false;
			}
			if (((UltraGridBase)ULGFamilyRelativesHealthInsurance).Rows[i].Cells["FamilyRelativeID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("من فضلك ادخل  صلة القرابة", "Please Select The Realtionship");
				((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Inssurance"];
				ULGFamilyRelativesHealthInsurance.ActiveCell = ((UltraGridBase)ULGFamilyRelativesHealthInsurance).Rows[i].Cells["FamilyRelativeID"];
				return false;
			}
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDocuments).Rows).Count; j++)
		{
			if (((UltraGridBase)ULGDocuments).Rows[j].Cells["DocumentTypeID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("من فضلك ادخل نوع المستند", "Enter Document Type");
				((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Documents"];
				ULGDocuments.ActiveCell = ((UltraGridBase)ULGDocuments).Rows[j].Cells["DocumentTypeID"];
				return false;
			}
		}
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGAllownces).Rows).Count; k++)
		{
			if (((UltraGridBase)ULGAllownces).Rows[k].Cells["IsByDate"].Value.Equals(true) && ((UltraGridBase)ULGAllownces).Rows[k].Cells["FromDate"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("من فضلك ادخل التاريخ", "Enter From Date");
				((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Salary"];
				ULGAllownces.ActiveCell = ((UltraGridBase)ULGAllownces).Rows[k].Cells["FromDate"];
				return false;
			}
		}
		if (((Control)(object)txtEmployeeNo).Text == "")
		{
			((Control)(object)txtEmployeeNo).Text = GetCode();
		}
		if (Employees.Check_Code(Adding ? "0" : ((KeyedSubObjectBase)SelectedNode).Key, ((Control)(object)txtEmployeeNo).Text, IsFromServer: true))
		{
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["PersonalData"];
			string code = Employees.GetCode(IsFromServer: true);
			GlobalVariables.QuestionMB.Show("رقم هذا الموظف متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Employee Number Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtEmployeeNo).Focus();
				return false;
			}
			((Control)(object)txtEmployeeNo).Text = code;
		}
		return base.ValidateData();
	}

	public override bool HasTransactionValidation()
	{
		string text = SubAccounts.SelectRelations(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true).Rows[0]["Relations"].ToString().Replace("-", "\n");
		if (text != "")
		{
			GlobalVariables.InformationMB.Show(text, text);
			return true;
		}
		return base.HasTransactionValidation();
	}

	private void Tree_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Expected O, but got Unknown
		TreeAccounts.AfterCheck -= new AfterNodeChangedEventHandler(Tree_AfterCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			TreeFunctions.SetParentCheckedState(e.TreeNode.Parent);
		}
		string treeCheckedNodesIDs = TreeFunctions.GetTreeCheckedNodesIDs(TreeAccounts);
		if (treeCheckedNodesIDs != ",")
		{
			string[] array = treeCheckedNodesIDs.TrimEnd(',').TrimStart(',').Split(',');
			dtSubAccDetails.Rows.Clear();
			for (int j = 0; j < array.Length; j++)
			{
				DataRow dataRow = dtSubAccDetails.NewRow();
				DataRow dataRow2 = dtAccounts.Select("AccountID=" + array[j])[0];
				dataRow["AccountID"] = dataRow2["AccountID"];
				dataRow["AccountName"] = dataRow2[GlobalVariables.IsArabic ? "AccountNameAr" : "AccountNameEn"];
				dtSubAccDetails.Rows.Add(dataRow);
			}
			GlobalFunctions.FillCombo(cboSalaryAccount, dtSubAccDetails, "AccountID", "AccountName");
			GlobalFunctions.FillCombo(cboAdvanceAccount, dtSubAccDetails, "AccountID", "AccountName");
			GlobalFunctions.FillCombo(cboPenaltyAccount, dtSubAccDetails, "AccountID", "AccountName");
		}
		TreeAccounts.AfterCheck += new AfterNodeChangedEventHandler(Tree_AfterCheck);
	}

	private void txtItems_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtAccounts);
		dataView.RowFilter = (GlobalVariables.IsArabic ? "AccountNameAr" : "AccountNameEn") + " Like '%" + ((Control)(object)txtItems).Text.Trim() + "%' ";
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TreeAccounts.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			TreeAccounts.ActiveNode = TreeAccounts.GetNodeByKey(dataView.ToTable().Rows[0]["AccountID"].ToString());
		}
	}

	public override void TreeSearch()
	{
		int num = SearchFunctions.Employees("-1", "-1", IsFromServer: true);
		if (num != 0)
		{
			treeChart.CollapseAll();
			treeChart.ActiveNode = treeChart.GetNodeByKey(num.ToString());
			treeChart.GetNodeByKey(num.ToString()).Selected = true;
		}
	}

	private void btnItemsSearch_Click(object sender, EventArgs e)
	{
		DataTable dataTable = SearchFunctions.AccountsReport(IsFromServer: true);
		if (dataTable.Rows.Count > 0)
		{
			TreeAccounts.CollapseAll();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				TreeAccounts.GetNodeByKey(dataTable.Rows[i]["AccountID"].ToString()).CheckedState = CheckState.Checked;
			}
		}
	}

	public override void btnPrintClick()
	{
		string val = "";
		if (dtReports.Rows.Count > 0)
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
			val = dtReports.Rows[0]["isoCode"].ToString();
		}
		else
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_HR_Employees_A.rpt" : "Rep_HR_Employees_E.rpt"));
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txt2_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbersNegative(sender, e);
	}

	private void btnPic_Click(object sender, EventArgs e)
	{
		if (ofdItemPic.ShowDialog() == DialogResult.OK)
		{
			picEmployee.Image = null;
			picEmployee.Image = ImageFunctions.ScaleImage(Image.FromFile(ofdItemPic.FileName), 100);
			picEmployee.ScaleImage = (ScaleImage)2;
			ImageChanged = true;
		}
	}

	private void cboCountry_ValueChanged(object sender, EventArgs e)
	{
		if (cboCountry.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtCities);
			dataView.RowFilter = "CountryID=" + ((TextEditorControlBase)cboCountry).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboCity.DataSource = dataView;
			cboCity.DisplayMember = "CityName";
			cboCity.ValueMember = "CityID";
		}
	}

	private void cboCity_ValueChanged(object sender, EventArgs e)
	{
		if (cboCity.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtAreas);
			dataView.RowFilter = "CityID=" + ((TextEditorControlBase)cboCity).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboArea.DataSource = dataView;
			cboArea.DisplayMember = "AreaName";
			cboArea.ValueMember = "AreaID";
		}
	}

	private void ULGDocuments_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGDocuments).ActiveRow).Selected = true;
		}
	}

	private void ULGCetificates_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGCetificates).ActiveRow).Selected = true;
		}
	}

	private void ULGContacts_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGContacts).ActiveRow).Selected = true;
		}
	}

	private void ULGAllownces_AfterEnterEditMode(object sender, EventArgs e)
	{
		if ((!Adding && !Updating) || ((KeyedSubObjectBase)ULGAllownces.ActiveCell.Column).Key == "IsPercent")
		{
			((GridItemBase)((UltraGridBase)ULGAllownces).ActiveRow).Selected = true;
		}
	}

	private void ULGYearIncrease_AfterEnterEditMode(object sender, EventArgs e)
	{
		if ((!Adding && !Updating) || ((KeyedSubObjectBase)ULGYearIncrease.ActiveCell.Column).Key == "IsPercent")
		{
			((GridItemBase)((UltraGridBase)ULGYearIncrease).ActiveRow).Selected = true;
		}
	}

	private void ULGDeductions_AfterEnterEditMode(object sender, EventArgs e)
	{
		if ((!Adding && !Updating) || ((KeyedSubObjectBase)ULGDeductions.ActiveCell.Column).Key == "IsPercent")
		{
			((GridItemBase)((UltraGridBase)ULGDeductions).ActiveRow).Selected = true;
		}
	}

	private void ULGMotivation_AfterEnterEditMode(object sender, EventArgs e)
	{
		if ((!Adding && !Updating) || ((KeyedSubObjectBase)ULGMotivation.ActiveCell.Column).Key == "IsPercent")
		{
			((GridItemBase)((UltraGridBase)ULGMotivation).ActiveRow).Selected = true;
		}
	}

	private void treeAdministrativeStructure_BeforeCheck(object sender, BeforeCheckEventArgs e)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		if ((!Adding && !Updating) || (Updating && !CanModAdministrativeStructure))
		{
			((CancelEventArgs)(object)e).Cancel = true;
			return;
		}
		treeAdministrativeStructure.BeforeCheck -= new BeforeCheckEventHandler(treeAdministrativeStructure_BeforeCheck);
		TreeFunctions.SetAllTreeNodesCheckState(Checked: false, treeAdministrativeStructure);
		treeAdministrativeStructure.BeforeCheck += new BeforeCheckEventHandler(treeAdministrativeStructure_BeforeCheck);
	}

	private void cboQualification_ValueChanged(object sender, EventArgs e)
	{
		if (cboQualification.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtSections);
			dataView.RowFilter = "QualificationNameID=" + ((TextEditorControlBase)cboQualification).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboSectionName.DataSource = dataView;
			cboSectionName.DisplayMember = "SectionName";
			cboSectionName.ValueMember = "SectionNameID";
		}
	}

	private void CalculateTotalSalary()
	{
		((UltraGridBase)ULGAllownces).UpdateData();
		((UltraGridBase)ULGDeductions).UpdateData();
		((UltraGridBase)ULGYearIncrease).UpdateData();
		decimal num = Convert.ToDecimal((((Control)(object)txtCurrentBasicSalary).Text == "") ? "0" : ((Control)(object)txtCurrentBasicSalary).Text);
		decimal num2 = Convert.ToDecimal((((Control)(object)txtNotInsuranceVariantSalary).Text == "" || ((Control)(object)txtNotInsuranceVariantSalary).Text == "-") ? "0" : ((Control)(object)txtNotInsuranceVariantSalary).Text);
		decimal num3 = num + num2 + Convert.ToDecimal((((Control)(object)txtCurrentVariantSalary).Text == "") ? "0" : ((Control)(object)txtCurrentVariantSalary).Text);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGAllownces).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGAllownces).Rows[i].Cells["Value"].Value != null && ((UltraGridBase)ULGAllownces).Rows[i].Cells["Value"].Value != DBNull.Value)
			{
				if (Convert.ToBoolean(((UltraGridBase)ULGAllownces).Rows[i].Cells["IsPercent"].Value))
				{
					num3 += Convert.ToDecimal(((UltraGridBase)ULGAllownces).Rows[i].Cells["Value"].Value) * num / 100m;
				}
				else
				{
					num3 += Convert.ToDecimal(((UltraGridBase)ULGAllownces).Rows[i].Cells["Value"].Value);
				}
			}
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGYearIncrease).Rows).Count; j++)
		{
			if (((UltraGridBase)ULGYearIncrease).Rows[j].Cells["Value"].Value != null && ((UltraGridBase)ULGYearIncrease).Rows[j].Cells["Value"].Value != DBNull.Value)
			{
				if (Convert.ToBoolean(((UltraGridBase)ULGYearIncrease).Rows[j].Cells["IsPercent"].Value))
				{
					num3 += Convert.ToDecimal(((UltraGridBase)ULGYearIncrease).Rows[j].Cells["Value"].Value) * num / 100m;
				}
				else
				{
					num3 += Convert.ToDecimal(((UltraGridBase)ULGYearIncrease).Rows[j].Cells["Value"].Value);
				}
			}
		}
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDeductions).Rows).Count; k++)
		{
			if (((UltraGridBase)ULGDeductions).Rows[k].Cells["Value"].Value != null && ((UltraGridBase)ULGDeductions).Rows[k].Cells["Value"].Value != DBNull.Value)
			{
				if (Convert.ToBoolean(((UltraGridBase)ULGDeductions).Rows[k].Cells["IsPercent"].Value))
				{
					num3 -= Convert.ToDecimal(((UltraGridBase)ULGDeductions).Rows[k].Cells["Value"].Value) * num / 100m;
				}
				else
				{
					num3 -= Convert.ToDecimal(((UltraGridBase)ULGDeductions).Rows[k].Cells["Value"].Value);
				}
			}
		}
		((Control)(object)txtNetCurrentSalary).Text = num3.ToString();
	}

	private void ULGAllownces_CellListSelect(object sender, CellEventArgs e)
	{
		((UltraGridBase)ULGAllownces).UpdateData();
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "AllowanceID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			e.Cell.Row.Cells["IsPercent"].Value = dtAllownces.Select("AllowanceID=" + e.Cell.Value)[0]["IsPrecent"];
			e.Cell.Row.Cells["Value"].Value = dtAllownces.Select("AllowanceID=" + e.Cell.Value)[0]["Value"];
			CalculateTotalSalary();
		}
	}

	private void ULGDeductions_CellListSelect(object sender, CellEventArgs e)
	{
		((UltraGridBase)ULGDeductions).UpdateData();
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "DeductionID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			e.Cell.Row.Cells["IsPercent"].Value = dtDeductions.Select("DeductionID=" + e.Cell.Value)[0]["IsPrecent"];
			e.Cell.Row.Cells["Value"].Value = dtDeductions.Select("DeductionID=" + e.Cell.Value)[0]["Value"];
			CalculateTotalSalary();
		}
	}

	private void ULGYearIncrease_CellListSelect(object sender, CellEventArgs e)
	{
		((UltraGridBase)ULGYearIncrease).UpdateData();
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "YearIncreaseID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			e.Cell.Row.Cells["IsPercent"].Value = dtYearIncreases.Select("YearIncreaseID=" + e.Cell.Value)[0]["IsPrecent"];
			e.Cell.Row.Cells["Value"].Value = dtYearIncreases.Select("YearIncreaseID=" + e.Cell.Value)[0]["Value"];
			CalculateTotalSalary();
		}
	}

	private void ULGMotivation_CellListSelect(object sender, CellEventArgs e)
	{
		((UltraGridBase)ULGMotivation).UpdateData();
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "MotivationID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			e.Cell.Row.Cells["IsPercent"].Value = dtMotivations.Select("MotivationID=" + e.Cell.Value)[0]["IsPrecent"];
			e.Cell.Row.Cells["IsSalary"].Value = dtMotivations.Select("MotivationID=" + e.Cell.Value)[0]["IsSalary"];
			e.Cell.Row.Cells["Value"].Value = dtMotivations.Select("MotivationID=" + e.Cell.Value)[0]["Value"];
		}
	}

	private void ULGAllownces_AfterCellUpdate(object sender, CellEventArgs e)
	{
		CalculateTotalSalary();
	}

	private void ULGDeductions_AfterCellUpdate(object sender, CellEventArgs e)
	{
		CalculateTotalSalary();
	}

	private void ULGYearIncrease_AfterCellUpdate(object sender, CellEventArgs e)
	{
		CalculateTotalSalary();
	}

	private void ULGMotivation_AfterCellUpdate(object sender, CellEventArgs e)
	{
	}

	private void txtCurrentBasicSalary_ValueChanged(object sender, EventArgs e)
	{
		CalculateTotalSalary();
	}

	private void txtNotInsuranceVariantSalary_ValueChanged(object sender, EventArgs e)
	{
		CalculateTotalSalary();
	}

	private void txtCurrentVariantSalary_ValueChanged(object sender, EventArgs e)
	{
		CalculateTotalSalary();
	}

	private void ULGAllownces_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalculateTotalSalary();
	}

	private void ULGDeductions_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalculateTotalSalary();
	}

	private void ULGYearIncrease_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalculateTotalSalary();
	}

	private void ULGMotivation_AfterRowsDeleted(object sender, EventArgs e)
	{
	}

	private void chkIsFixedSalaryTax_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)txtFixedSalaryValue).Enabled = ((UltraToggleEditorBase)chkIsFixedSalaryTax).Checked;
		((Control)(object)chkIsFixedTaxPercentage).Enabled = ((UltraToggleEditorBase)chkIsFixedSalaryTax).Checked && ((Control)(object)chkIsFixedSalaryTax).Enabled;
	}

	private void changeParentTSMenu_Click(object sender, EventArgs e)
	{
		if (SelectedNode != null)
		{
			frmUpdateParent frmUpdateParent2 = new frmUpdateParent(((KeyedSubObjectBase)SelectedNode).Key, (SelectedNode.Parent != null) ? ((KeyedSubObjectBase)SelectedNode.Parent).Key : "");
			((Control)(object)frmUpdateParent2.lblTitle).Text = (GlobalVariables.IsArabic ? "تغيير المجموعه" : "Update Group");
			frmUpdateParent2.ShowDialog();
			btnRefreshDataClick();
		}
	}

	private void setAsGroupToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!HasTransactionValidation())
		{
			SubAccounts.UpdateIsMain(((KeyedSubObjectBase)SelectedNode).Key, "1", GlobalVariables.UserID, IsFromServer: true);
			DataRow dataRow = dtChart.Select(IDCol + "=" + ((KeyedSubObjectBase)SelectedNode).Key)[0];
			dataRow[IsMainCol] = 1;
			((SubObjectBase)SelectedNode).Tag = dataRow;
			SelectedNode.Override.NodeAppearance.Image = Resources.folderfortree;
			SelectedNode.ExpandAll();
			changeParentTSMenu.Enabled = false;
			setAsGroupToolStripMenuItem.Enabled = false;
			setAsSubAccountToolStripMenuItem.Enabled = true;
		}
	}

	private void setAsSubAccountToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (((DisposableObjectCollectionBase)SelectedNode.Nodes).Count > 0)
		{
			GlobalVariables.InformationMB.Show(" لايمكن تحويله لحساب تحليلي لوجود عناصر تحته", "Cannot set this Node as SubAccount It Has Sub Nodes");
			return;
		}
		SubAccounts.UpdateIsMain(((KeyedSubObjectBase)SelectedNode).Key, "0", GlobalVariables.UserID, IsFromServer: true);
		DataRow dataRow = dtChart.Select(IDCol + "=" + ((KeyedSubObjectBase)SelectedNode).Key)[0];
		dataRow[IsMainCol] = 0;
		((SubObjectBase)SelectedNode).Tag = dataRow;
		SelectedNode.Override.NodeAppearance.Image = null;
		SelectedNode.ExpandAll();
		changeParentTSMenu.Enabled = true;
		setAsGroupToolStripMenuItem.Enabled = true;
		setAsSubAccountToolStripMenuItem.Enabled = false;
	}

	private void modifyGroupEmployeesToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (SelectedNode != null)
		{
			frmUpdateGroupEmployees frmUpdateGroupEmployees2 = new frmUpdateGroupEmployees(((KeyedSubObjectBase)SelectedNode).Key);
			frmUpdateGroupEmployees2.ShowDialog();
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
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected O, but got Unknown
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Expected O, but got Unknown
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected O, but got Unknown
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Expected O, but got Unknown
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Expected O, but got Unknown
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Expected O, but got Unknown
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Expected O, but got Unknown
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Expected O, but got Unknown
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Expected O, but got Unknown
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Expected O, but got Unknown
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Expected O, but got Unknown
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Expected O, but got Unknown
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Expected O, but got Unknown
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Expected O, but got Unknown
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Expected O, but got Unknown
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Expected O, but got Unknown
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Expected O, but got Unknown
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Expected O, but got Unknown
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Expected O, but got Unknown
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Expected O, but got Unknown
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Expected O, but got Unknown
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Expected O, but got Unknown
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Expected O, but got Unknown
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Expected O, but got Unknown
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Expected O, but got Unknown
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Expected O, but got Unknown
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Expected O, but got Unknown
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Expected O, but got Unknown
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Expected O, but got Unknown
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Expected O, but got Unknown
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Expected O, but got Unknown
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Expected O, but got Unknown
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Expected O, but got Unknown
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Expected O, but got Unknown
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Expected O, but got Unknown
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Expected O, but got Unknown
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Expected O, but got Unknown
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Expected O, but got Unknown
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Expected O, but got Unknown
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Expected O, but got Unknown
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Expected O, but got Unknown
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Expected O, but got Unknown
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Expected O, but got Unknown
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Expected O, but got Unknown
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Expected O, but got Unknown
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Expected O, but got Unknown
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Expected O, but got Unknown
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Expected O, but got Unknown
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Expected O, but got Unknown
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Expected O, but got Unknown
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Expected O, but got Unknown
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Expected O, but got Unknown
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Expected O, but got Unknown
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Expected O, but got Unknown
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Expected O, but got Unknown
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Expected O, but got Unknown
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Expected O, but got Unknown
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Expected O, but got Unknown
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Expected O, but got Unknown
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Expected O, but got Unknown
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Expected O, but got Unknown
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Expected O, but got Unknown
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Expected O, but got Unknown
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Expected O, but got Unknown
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Expected O, but got Unknown
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Expected O, but got Unknown
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Expected O, but got Unknown
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Expected O, but got Unknown
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Expected O, but got Unknown
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Expected O, but got Unknown
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Expected O, but got Unknown
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Expected O, but got Unknown
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Expected O, but got Unknown
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Expected O, but got Unknown
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Expected O, but got Unknown
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Expected O, but got Unknown
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Expected O, but got Unknown
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Expected O, but got Unknown
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Expected O, but got Unknown
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Expected O, but got Unknown
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Expected O, but got Unknown
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Expected O, but got Unknown
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Expected O, but got Unknown
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Expected O, but got Unknown
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Expected O, but got Unknown
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Expected O, but got Unknown
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Expected O, but got Unknown
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Expected O, but got Unknown
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Expected O, but got Unknown
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Expected O, but got Unknown
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Expected O, but got Unknown
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Expected O, but got Unknown
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Expected O, but got Unknown
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Expected O, but got Unknown
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Expected O, but got Unknown
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Expected O, but got Unknown
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Expected O, but got Unknown
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Expected O, but got Unknown
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Expected O, but got Unknown
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Expected O, but got Unknown
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Expected O, but got Unknown
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Expected O, but got Unknown
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Expected O, but got Unknown
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Expected O, but got Unknown
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Expected O, but got Unknown
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Expected O, but got Unknown
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Expected O, but got Unknown
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Expected O, but got Unknown
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Expected O, but got Unknown
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Expected O, but got Unknown
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Expected O, but got Unknown
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Expected O, but got Unknown
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Expected O, but got Unknown
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Expected O, but got Unknown
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Expected O, but got Unknown
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Expected O, but got Unknown
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Expected O, but got Unknown
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Expected O, but got Unknown
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Expected O, but got Unknown
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Expected O, but got Unknown
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Expected O, but got Unknown
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Expected O, but got Unknown
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Expected O, but got Unknown
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Expected O, but got Unknown
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Expected O, but got Unknown
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Expected O, but got Unknown
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Expected O, but got Unknown
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Expected O, but got Unknown
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Expected O, but got Unknown
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Expected O, but got Unknown
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Expected O, but got Unknown
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Expected O, but got Unknown
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Expected O, but got Unknown
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Expected O, but got Unknown
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Expected O, but got Unknown
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Expected O, but got Unknown
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Expected O, but got Unknown
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Expected O, but got Unknown
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Expected O, but got Unknown
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Expected O, but got Unknown
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Expected O, but got Unknown
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Expected O, but got Unknown
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Expected O, but got Unknown
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Expected O, but got Unknown
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Expected O, but got Unknown
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Expected O, but got Unknown
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Expected O, but got Unknown
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Expected O, but got Unknown
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Expected O, but got Unknown
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Expected O, but got Unknown
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Expected O, but got Unknown
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Expected O, but got Unknown
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Expected O, but got Unknown
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Expected O, but got Unknown
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Expected O, but got Unknown
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Expected O, but got Unknown
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Expected O, but got Unknown
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Expected O, but got Unknown
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Expected O, but got Unknown
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Expected O, but got Unknown
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Expected O, but got Unknown
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Expected O, but got Unknown
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Expected O, but got Unknown
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Expected O, but got Unknown
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Expected O, but got Unknown
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Expected O, but got Unknown
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Expected O, but got Unknown
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Expected O, but got Unknown
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Expected O, but got Unknown
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Expected O, but got Unknown
		//IL_059f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a9: Expected O, but got Unknown
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Expected O, but got Unknown
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Expected O, but got Unknown
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Expected O, but got Unknown
		//IL_05cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d5: Expected O, but got Unknown
		//IL_05d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e0: Expected O, but got Unknown
		//IL_05e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05eb: Expected O, but got Unknown
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f6: Expected O, but got Unknown
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Expected O, but got Unknown
		//IL_0602: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Expected O, but got Unknown
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Expected O, but got Unknown
		//IL_0618: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Expected O, but got Unknown
		//IL_0623: Unknown result type (might be due to invalid IL or missing references)
		//IL_062d: Expected O, but got Unknown
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Expected O, but got Unknown
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_0643: Expected O, but got Unknown
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_064e: Expected O, but got Unknown
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0659: Expected O, but got Unknown
		//IL_065a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0664: Expected O, but got Unknown
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_066f: Expected O, but got Unknown
		//IL_0670: Unknown result type (might be due to invalid IL or missing references)
		//IL_067a: Expected O, but got Unknown
		//IL_067b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0685: Expected O, but got Unknown
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_0690: Expected O, but got Unknown
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Expected O, but got Unknown
		//IL_069c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a6: Expected O, but got Unknown
		//IL_06a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b1: Expected O, but got Unknown
		//IL_06b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bc: Expected O, but got Unknown
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Expected O, but got Unknown
		//IL_06c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d2: Expected O, but got Unknown
		//IL_06d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dd: Expected O, but got Unknown
		//IL_06de: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e8: Expected O, but got Unknown
		//IL_06e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f3: Expected O, but got Unknown
		//IL_06f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fe: Expected O, but got Unknown
		//IL_06ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0709: Expected O, but got Unknown
		//IL_070a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0714: Expected O, but got Unknown
		//IL_0715: Unknown result type (might be due to invalid IL or missing references)
		//IL_071f: Expected O, but got Unknown
		//IL_0720: Unknown result type (might be due to invalid IL or missing references)
		//IL_072a: Expected O, but got Unknown
		//IL_072b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0735: Expected O, but got Unknown
		//IL_0736: Unknown result type (might be due to invalid IL or missing references)
		//IL_0740: Expected O, but got Unknown
		//IL_0741: Unknown result type (might be due to invalid IL or missing references)
		//IL_074b: Expected O, but got Unknown
		//IL_074c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0756: Expected O, but got Unknown
		//IL_0757: Unknown result type (might be due to invalid IL or missing references)
		//IL_0761: Expected O, but got Unknown
		//IL_0762: Unknown result type (might be due to invalid IL or missing references)
		//IL_076c: Expected O, but got Unknown
		//IL_076d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0777: Expected O, but got Unknown
		//IL_0778: Unknown result type (might be due to invalid IL or missing references)
		//IL_0782: Expected O, but got Unknown
		//IL_0783: Unknown result type (might be due to invalid IL or missing references)
		//IL_078d: Expected O, but got Unknown
		//IL_078e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0798: Expected O, but got Unknown
		//IL_0799: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a3: Expected O, but got Unknown
		//IL_07a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ae: Expected O, but got Unknown
		//IL_07af: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b9: Expected O, but got Unknown
		//IL_07ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c4: Expected O, but got Unknown
		//IL_07c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cf: Expected O, but got Unknown
		//IL_07d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07da: Expected O, but got Unknown
		//IL_07db: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e5: Expected O, but got Unknown
		//IL_07e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f0: Expected O, but got Unknown
		//IL_07f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fb: Expected O, but got Unknown
		//IL_07fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0806: Expected O, but got Unknown
		//IL_0807: Unknown result type (might be due to invalid IL or missing references)
		//IL_0811: Expected O, but got Unknown
		//IL_0812: Unknown result type (might be due to invalid IL or missing references)
		//IL_081c: Expected O, but got Unknown
		//IL_081d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0827: Expected O, but got Unknown
		//IL_0828: Unknown result type (might be due to invalid IL or missing references)
		//IL_0832: Expected O, but got Unknown
		//IL_0833: Unknown result type (might be due to invalid IL or missing references)
		//IL_083d: Expected O, but got Unknown
		//IL_083e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0848: Expected O, but got Unknown
		//IL_0849: Unknown result type (might be due to invalid IL or missing references)
		//IL_0853: Expected O, but got Unknown
		//IL_0854: Unknown result type (might be due to invalid IL or missing references)
		//IL_085e: Expected O, but got Unknown
		//IL_085f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0869: Expected O, but got Unknown
		//IL_086a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0874: Expected O, but got Unknown
		//IL_0875: Unknown result type (might be due to invalid IL or missing references)
		//IL_087f: Expected O, but got Unknown
		//IL_0880: Unknown result type (might be due to invalid IL or missing references)
		//IL_088a: Expected O, but got Unknown
		//IL_088b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0895: Expected O, but got Unknown
		//IL_0896: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a0: Expected O, but got Unknown
		//IL_08a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ab: Expected O, but got Unknown
		//IL_08ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b6: Expected O, but got Unknown
		//IL_08b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c1: Expected O, but got Unknown
		//IL_08c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cc: Expected O, but got Unknown
		//IL_08cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d7: Expected O, but got Unknown
		//IL_08d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e2: Expected O, but got Unknown
		//IL_08e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ed: Expected O, but got Unknown
		//IL_08ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f8: Expected O, but got Unknown
		//IL_08f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0903: Expected O, but got Unknown
		//IL_0904: Unknown result type (might be due to invalid IL or missing references)
		//IL_090e: Expected O, but got Unknown
		//IL_090f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0919: Expected O, but got Unknown
		//IL_091a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0924: Expected O, but got Unknown
		//IL_0925: Unknown result type (might be due to invalid IL or missing references)
		//IL_092f: Expected O, but got Unknown
		//IL_0930: Unknown result type (might be due to invalid IL or missing references)
		//IL_093a: Expected O, but got Unknown
		//IL_093b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0945: Expected O, but got Unknown
		//IL_0946: Unknown result type (might be due to invalid IL or missing references)
		//IL_0950: Expected O, but got Unknown
		//IL_0951: Unknown result type (might be due to invalid IL or missing references)
		//IL_095b: Expected O, but got Unknown
		//IL_095c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0966: Expected O, but got Unknown
		//IL_0967: Unknown result type (might be due to invalid IL or missing references)
		//IL_0971: Expected O, but got Unknown
		//IL_0972: Unknown result type (might be due to invalid IL or missing references)
		//IL_097c: Expected O, but got Unknown
		//IL_097d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0987: Expected O, but got Unknown
		//IL_0988: Unknown result type (might be due to invalid IL or missing references)
		//IL_0992: Expected O, but got Unknown
		//IL_0993: Unknown result type (might be due to invalid IL or missing references)
		//IL_099d: Expected O, but got Unknown
		//IL_099e: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a8: Expected O, but got Unknown
		//IL_09a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b3: Expected O, but got Unknown
		//IL_09b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09be: Expected O, but got Unknown
		//IL_09bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c9: Expected O, but got Unknown
		//IL_09ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d4: Expected O, but got Unknown
		//IL_09d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09df: Expected O, but got Unknown
		//IL_09e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ea: Expected O, but got Unknown
		//IL_09eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f5: Expected O, but got Unknown
		//IL_09f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a00: Expected O, but got Unknown
		//IL_0a01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0b: Expected O, but got Unknown
		//IL_0a0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a16: Expected O, but got Unknown
		//IL_0a17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a21: Expected O, but got Unknown
		//IL_0a22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2c: Expected O, but got Unknown
		//IL_0a2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a37: Expected O, but got Unknown
		//IL_0a38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a42: Expected O, but got Unknown
		//IL_0a43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4d: Expected O, but got Unknown
		//IL_0a4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a58: Expected O, but got Unknown
		//IL_0a59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a63: Expected O, but got Unknown
		//IL_0a64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6e: Expected O, but got Unknown
		//IL_0a6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a79: Expected O, but got Unknown
		//IL_0a7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a84: Expected O, but got Unknown
		//IL_0a85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8f: Expected O, but got Unknown
		//IL_0a90: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9a: Expected O, but got Unknown
		//IL_0a9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa5: Expected O, but got Unknown
		//IL_0aa6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab0: Expected O, but got Unknown
		//IL_0ab1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0abb: Expected O, but got Unknown
		//IL_0abc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac6: Expected O, but got Unknown
		//IL_0ac7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad1: Expected O, but got Unknown
		//IL_0ad2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0adc: Expected O, but got Unknown
		//IL_0add: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae7: Expected O, but got Unknown
		//IL_0ae8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af2: Expected O, but got Unknown
		//IL_0af3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afd: Expected O, but got Unknown
		//IL_0afe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b08: Expected O, but got Unknown
		//IL_0b09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b13: Expected O, but got Unknown
		//IL_0b14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1e: Expected O, but got Unknown
		//IL_0b1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b29: Expected O, but got Unknown
		//IL_0b2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b34: Expected O, but got Unknown
		//IL_0b35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3f: Expected O, but got Unknown
		//IL_0b40: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4a: Expected O, but got Unknown
		//IL_0b4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b55: Expected O, but got Unknown
		//IL_0b56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b60: Expected O, but got Unknown
		//IL_0b61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b6b: Expected O, but got Unknown
		//IL_0b77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b81: Expected O, but got Unknown
		//IL_0b82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8c: Expected O, but got Unknown
		//IL_0b8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b97: Expected O, but got Unknown
		//IL_0b98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba2: Expected O, but got Unknown
		//IL_0ba3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bad: Expected O, but got Unknown
		//IL_0bae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb8: Expected O, but got Unknown
		//IL_0bb9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc3: Expected O, but got Unknown
		//IL_0bc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bce: Expected O, but got Unknown
		//IL_0bcf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd9: Expected O, but got Unknown
		//IL_0bda: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be4: Expected O, but got Unknown
		//IL_0be5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bef: Expected O, but got Unknown
		//IL_0bf0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfa: Expected O, but got Unknown
		//IL_0bfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c05: Expected O, but got Unknown
		//IL_0c06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c10: Expected O, but got Unknown
		//IL_0c11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1b: Expected O, but got Unknown
		//IL_0c1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c26: Expected O, but got Unknown
		//IL_0c27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c31: Expected O, but got Unknown
		//IL_0c32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3c: Expected O, but got Unknown
		//IL_0c3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c47: Expected O, but got Unknown
		//IL_0c48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c52: Expected O, but got Unknown
		//IL_0c53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5d: Expected O, but got Unknown
		//IL_0c5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c68: Expected O, but got Unknown
		//IL_0c69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c73: Expected O, but got Unknown
		//IL_0c74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7e: Expected O, but got Unknown
		//IL_0c7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c89: Expected O, but got Unknown
		//IL_0c8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c94: Expected O, but got Unknown
		//IL_0c95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9f: Expected O, but got Unknown
		//IL_0ca0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0caa: Expected O, but got Unknown
		//IL_0cab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb5: Expected O, but got Unknown
		//IL_0cb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc0: Expected O, but got Unknown
		//IL_0cc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccb: Expected O, but got Unknown
		//IL_0ccc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd6: Expected O, but got Unknown
		//IL_0cd7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce1: Expected O, but got Unknown
		//IL_0ce2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cec: Expected O, but got Unknown
		//IL_0ced: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf7: Expected O, but got Unknown
		//IL_0cf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d02: Expected O, but got Unknown
		//IL_0d03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d0d: Expected O, but got Unknown
		//IL_0d0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d18: Expected O, but got Unknown
		//IL_0d19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d23: Expected O, but got Unknown
		//IL_0d24: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2e: Expected O, but got Unknown
		//IL_0d2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d39: Expected O, but got Unknown
		//IL_0d3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d44: Expected O, but got Unknown
		//IL_0d45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d4f: Expected O, but got Unknown
		//IL_0d50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5a: Expected O, but got Unknown
		//IL_0d5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d65: Expected O, but got Unknown
		//IL_0d66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d70: Expected O, but got Unknown
		//IL_0d71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7b: Expected O, but got Unknown
		//IL_0d7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d86: Expected O, but got Unknown
		//IL_0d87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d91: Expected O, but got Unknown
		//IL_0d92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9c: Expected O, but got Unknown
		//IL_0d9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da7: Expected O, but got Unknown
		//IL_0da8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db2: Expected O, but got Unknown
		//IL_0db3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dbd: Expected O, but got Unknown
		//IL_0dbe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc8: Expected O, but got Unknown
		//IL_0dc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd3: Expected O, but got Unknown
		//IL_0dd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dde: Expected O, but got Unknown
		//IL_0ddf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de9: Expected O, but got Unknown
		//IL_0df5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dff: Expected O, but got Unknown
		//IL_0e00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0a: Expected O, but got Unknown
		//IL_30a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_30b1: Expected O, but got Unknown
		//IL_4eb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_4ebf: Expected O, but got Unknown
		//IL_4efd: Unknown result type (might be due to invalid IL or missing references)
		//IL_4f07: Expected O, but got Unknown
		//IL_52b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_52be: Expected O, but got Unknown
		//IL_52fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_5306: Expected O, but got Unknown
		//IL_584c: Unknown result type (might be due to invalid IL or missing references)
		//IL_5856: Expected O, but got Unknown
		//IL_5894: Unknown result type (might be due to invalid IL or missing references)
		//IL_589e: Expected O, but got Unknown
		//IL_5c4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c55: Expected O, but got Unknown
		//IL_5c93: Unknown result type (might be due to invalid IL or missing references)
		//IL_5c9d: Expected O, but got Unknown
		//IL_85ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_85d4: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HR.Personal.MasterData.frmEmployeeTree));
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
		Override val31 = new Override();
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
		Appearance val61 = new Appearance();
		Appearance val62 = new Appearance();
		Appearance val63 = new Appearance();
		Appearance val64 = new Appearance();
		Appearance val65 = new Appearance();
		Appearance val66 = new Appearance();
		Appearance val67 = new Appearance();
		Appearance val68 = new Appearance();
		Appearance val69 = new Appearance();
		Appearance val70 = new Appearance();
		Appearance val71 = new Appearance();
		Appearance val72 = new Appearance();
		Appearance val73 = new Appearance();
		Appearance val74 = new Appearance();
		Appearance val75 = new Appearance();
		Appearance val76 = new Appearance();
		Appearance val77 = new Appearance();
		Appearance val78 = new Appearance();
		Appearance val79 = new Appearance();
		Appearance val80 = new Appearance();
		Appearance val81 = new Appearance();
		Appearance val82 = new Appearance();
		Appearance val83 = new Appearance();
		Appearance val84 = new Appearance();
		Appearance val85 = new Appearance();
		Appearance val86 = new Appearance();
		Appearance val87 = new Appearance();
		Appearance val88 = new Appearance();
		Appearance val89 = new Appearance();
		Appearance val90 = new Appearance();
		Appearance val91 = new Appearance();
		Appearance val92 = new Appearance();
		Appearance val93 = new Appearance();
		Appearance val94 = new Appearance();
		Appearance val95 = new Appearance();
		Appearance val96 = new Appearance();
		Appearance val97 = new Appearance();
		Appearance val98 = new Appearance();
		Appearance val99 = new Appearance();
		Appearance val100 = new Appearance();
		Appearance val101 = new Appearance();
		Appearance val102 = new Appearance();
		Appearance val103 = new Appearance();
		Appearance val104 = new Appearance();
		Appearance val105 = new Appearance();
		Appearance val106 = new Appearance();
		Appearance val107 = new Appearance();
		Appearance val108 = new Appearance();
		Appearance val109 = new Appearance();
		Appearance val110 = new Appearance();
		Appearance val111 = new Appearance();
		Appearance val112 = new Appearance();
		Appearance val113 = new Appearance();
		Appearance val114 = new Appearance();
		Appearance val115 = new Appearance();
		Appearance val116 = new Appearance();
		Appearance val117 = new Appearance();
		Appearance val118 = new Appearance();
		Appearance val119 = new Appearance();
		Appearance val120 = new Appearance();
		Appearance val121 = new Appearance();
		Appearance val122 = new Appearance();
		Appearance val123 = new Appearance();
		Appearance val124 = new Appearance();
		Appearance val125 = new Appearance();
		Appearance val126 = new Appearance();
		Appearance val127 = new Appearance();
		Appearance val128 = new Appearance();
		Appearance val129 = new Appearance();
		Appearance val130 = new Appearance();
		Appearance val131 = new Appearance();
		Appearance val132 = new Appearance();
		Appearance val133 = new Appearance();
		Appearance val134 = new Appearance();
		Appearance val135 = new Appearance();
		Appearance val136 = new Appearance();
		Appearance val137 = new Appearance();
		Appearance val138 = new Appearance();
		Appearance val139 = new Appearance();
		Appearance val140 = new Appearance();
		Appearance val141 = new Appearance();
		Appearance val142 = new Appearance();
		Appearance val143 = new Appearance();
		Appearance val144 = new Appearance();
		Appearance val145 = new Appearance();
		Appearance val146 = new Appearance();
		Appearance val147 = new Appearance();
		Appearance val148 = new Appearance();
		Appearance val149 = new Appearance();
		Appearance val150 = new Appearance();
		Appearance val151 = new Appearance();
		Appearance val152 = new Appearance();
		Appearance val153 = new Appearance();
		Appearance val154 = new Appearance();
		Appearance val155 = new Appearance();
		Appearance val156 = new Appearance();
		Appearance val157 = new Appearance();
		Appearance val158 = new Appearance();
		Appearance val159 = new Appearance();
		Appearance val160 = new Appearance();
		Appearance val161 = new Appearance();
		Appearance val162 = new Appearance();
		Appearance val163 = new Appearance();
		Appearance val164 = new Appearance();
		Appearance val165 = new Appearance();
		Appearance val166 = new Appearance();
		Appearance val167 = new Appearance();
		Appearance val168 = new Appearance();
		Appearance val169 = new Appearance();
		Appearance val170 = new Appearance();
		Appearance val171 = new Appearance();
		Appearance val172 = new Appearance();
		Appearance val173 = new Appearance();
		Appearance val174 = new Appearance();
		Appearance val175 = new Appearance();
		Appearance val176 = new Appearance();
		Appearance val177 = new Appearance();
		Appearance val178 = new Appearance();
		Override val179 = new Override();
		Appearance val180 = new Appearance();
		Appearance val181 = new Appearance();
		Appearance val182 = new Appearance();
		Appearance val183 = new Appearance();
		Appearance val184 = new Appearance();
		UltraTab val185 = new UltraTab();
		UltraTab val186 = new UltraTab();
		UltraTab val187 = new UltraTab();
		UltraTab val188 = new UltraTab();
		UltraTab val189 = new UltraTab();
		UltraTab val190 = new UltraTab();
		UltraTab val191 = new UltraTab();
		UltraTab val192 = new UltraTab();
		UltraTab val193 = new UltraTab();
		Appearance val194 = new Appearance();
		this.tabItem = new UltraTabPageControl();
		this.dtpPostponedToDate = new UltraDateTimeEditor();
		this.ultraLabel22 = new UltraLabel();
		this.chkHasPrivateCar = new UltraCheckEditor();
		this.chkHasPassport = new UltraCheckEditor();
		this.btnPic = new UltraButton();
		this.picEmployee = new UltraPictureBox();
		this.cboCountry = new UltraComboEditor();
		this.lblCountry = new UltraLabel();
		this.cboArea = new UltraComboEditor();
		this.lblArea = new UltraLabel();
		this.cboCity = new UltraComboEditor();
		this.lblCity = new UltraLabel();
		this.txtEmployeeNo = new UltraTextEditor();
		this.txtPassportNo = new UltraTextEditor();
		this.txtDrivingLicenseID = new UltraTextEditor();
		this.txtPersonalIDNo = new UltraTextEditor();
		this.lblEmployeeNo = new UltraLabel();
		this.lblPassportNo = new UltraLabel();
		this.lblDrivingLicenseID = new UltraLabel();
		this.lblPersonalIDNo = new UltraLabel();
		this.dtpDrivingLicenseExpireDate = new UltraDateTimeEditor();
		this.dtpPassportExpireDate = new UltraDateTimeEditor();
		this.ultraLabel23 = new UltraLabel();
		this.dtpIDExpireDate = new UltraDateTimeEditor();
		this.lblPassportExpireDate = new UltraLabel();
		this.dtpIDIssueDate = new UltraDateTimeEditor();
		this.lblIDExpireDate = new UltraLabel();
		this.dtpBirthDate = new UltraDateTimeEditor();
		this.lblIDIssueDate = new UltraLabel();
		this.lblBirthDate = new UltraLabel();
		this.cboSocialStatus = new UltraComboEditor();
		this.cboMilitaryService = new UltraComboEditor();
		this.lblSocialStatus = new UltraLabel();
		this.cboNationality = new UltraComboEditor();
		this.lblMilitaryService = new UltraLabel();
		this.cboGender = new UltraComboEditor();
		this.lblNationality = new UltraLabel();
		this.lblGender = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.txtAddress = new UltraTextEditor();
		this.lblAddress = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.ultraLabel28 = new UltraLabel();
		this.btnStoreSearch = new UltraButton();
		this.cboSalesManDefaultStore = new UltraComboEditor();
		this.txtLeavingWorkNotes = new UltraTextEditor();
		this.ultraLabel25 = new UltraLabel();
		this.cboLeavingWorkReason = new UltraComboEditor();
		this.ultraLabel24 = new UltraLabel();
		this.chkIsSalesMan = new UltraCheckEditor();
		this.treeAdministrativeStructure = new UltraTree();
		this.dtpContractToDate = new UltraDateTimeEditor();
		this.dtpContractFromDate = new UltraDateTimeEditor();
		this.dtpEndDate = new UltraDateTimeEditor();
		this.lblContractToDate = new UltraLabel();
		this.dtpHireDate = new UltraDateTimeEditor();
		this.lblEndDate = new UltraLabel();
		this.lblContractFromDate = new UltraLabel();
		this.lblAdministrativeStructure = new UltraLabel();
		this.lblHireDate = new UltraLabel();
		this.cboDirectManager = new UltraComboEditor();
		this.lblDirectManager = new UltraLabel();
		this.cboDegree = new UltraComboEditor();
		this.lblDegreeID = new UltraLabel();
		this.cboPosition = new UltraComboEditor();
		this.lblPosition = new UltraLabel();
		this.cboBranch = new UltraComboEditor();
		this.lblBranch = new UltraLabel();
		this.cboAdministrativeLevel = new UltraComboEditor();
		this.lblAdministrativeLevel = new UltraLabel();
		this.ultraTabPageControl6 = new UltraTabPageControl();
		this.chkSalaryAtEndOFMonth = new UltraCheckEditor();
		this.ultraLabel1 = new UltraLabel();
		this.dtpServicePercentFromDate = new UltraDateTimeEditor();
		this.chkInServicePercent = new UltraCheckEditor();
		this.chkInSalaryTax = new UltraCheckEditor();
		this.chkIsMonthlySalary = new UltraCheckEditor();
		this.ultraLabel4 = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.txtCollectionCommission = new UltraTextEditor();
		this.txtSalesCommission = new UltraTextEditor();
		this.lblCollectionCommission = new UltraLabel();
		this.lblSalesCommissionRatio = new UltraLabel();
		this.cboSalaryList = new UltraComboEditor();
		this.lblSalaryList = new UltraLabel();
		this.cboLeavePermissionRule = new UltraComboEditor();
		this.lblLeaveRule = new UltraLabel();
		this.cboVacationRule = new UltraComboEditor();
		this.ultraLabel13 = new UltraLabel();
		this.cboSalaryAccount = new UltraComboEditor();
		this.cboPenalityRule = new UltraComboEditor();
		this.ultraLabel17 = new UltraLabel();
		this.ultraLabel12 = new UltraLabel();
		this.cboPenaltyAccount = new UltraComboEditor();
		this.ultraLabel16 = new UltraLabel();
		this.cboFeedingRule = new UltraComboEditor();
		this.cboAbsenceRule = new UltraComboEditor();
		this.lblFeedingRule = new UltraLabel();
		this.cboAdvanceAccount = new UltraComboEditor();
		this.ultraLabel11 = new UltraLabel();
		this.ultraLabel15 = new UltraLabel();
		this.cboLateRule = new UltraComboEditor();
		this.ultraLabel10 = new UltraLabel();
		this.cboExtraTimeRule = new UltraComboEditor();
		this.ultraLabel9 = new UltraLabel();
		this.cboBank = new UltraComboEditor();
		this.lblBank = new UltraLabel();
		this.txtBankAccountNo = new UltraTextEditor();
		this.lblBankAccountNo = new UltraLabel();
		this.ultraTabPageControl4 = new UltraTabPageControl();
		this.ultraLabel20 = new UltraLabel();
		this.ultraLabel19 = new UltraLabel();
		this.ULGMotivation = new UltraGrid();
		this.ULGYearIncrease = new UltraGrid();
		this.cboCurrency = new UltraComboEditor();
		this.lblCurrency = new UltraLabel();
		this.txtFixedSalaryValue = new UltraTextEditor();
		this.chkIsFixedTaxPercentage = new UltraCheckEditor();
		this.chkIsFixedSalaryTax = new UltraCheckEditor();
		this.ULGDeductions = new UltraGrid();
		this.ULGAllownces = new UltraGrid();
		this.txtCurrentBasicSalary = new UltraTextEditor();
		this.txtCurrentVariantSalary = new UltraTextEditor();
		this.txtNetCurrentSalary = new UltraTextEditor();
		this.txtStartBasicSalary = new UltraTextEditor();
		this.ultraLabel7 = new UltraLabel();
		this.txtNotInsuranceVariantSalary = new UltraTextEditor();
		this.txtStartVariantSalary = new UltraTextEditor();
		this.ultraLabel5 = new UltraLabel();
		this.ultraLabel6 = new UltraLabel();
		this.txtStartSalary = new UltraTextEditor();
		this.ultraLabel21 = new UltraLabel();
		this.ultraLabel3 = new UltraLabel();
		this.lblNetCurrentSalary = new UltraLabel();
		this.lblDeductions = new UltraLabel();
		this.lblStartSalary = new UltraLabel();
		this.ultraLabel14 = new UltraLabel();
		this.ultraTabPageControl5 = new UltraTabPageControl();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.ULGFamilyRelativesHealthInsurance = new UltraGrid();
		this.txtInclusiveHealthInsuranceValue = new UltraTextEditor();
		this.lblInclusiveHealthInsuranceValue = new UltraLabel();
		this.txtSocialInsuranceValue = new UltraTextEditor();
		this.ultraLabel29 = new UltraLabel();
		this.ultraLabel26 = new UltraLabel();
		this.ultraLabel27 = new UltraLabel();
		this.dtpHealthInsuranceEndDate = new UltraDateTimeEditor();
		this.dtpHealthInsuranceStartDate = new UltraDateTimeEditor();
		this.chkWorkOfficeIsSend = new UltraCheckEditor();
		this.lblSendDate = new UltraLabel();
		this.lblToDate = new UltraLabel();
		this.lblWorkOfficeFromDate = new UltraLabel();
		this.lblInssuranceDate = new UltraLabel();
		this.lblInsuranceEndDate = new UltraLabel();
		this.lblFirstInssuranceDate = new UltraLabel();
		this.cboHealthInsuranceType = new UltraComboEditor();
		this.ultraLabel8 = new UltraLabel();
		this.cboSocialInssuranceOffice = new UltraComboEditor();
		this.lblSocialInssuranceOffice = new UltraLabel();
		this.txtWorkOfficePermissionCode = new UltraTextEditor();
		this.lblWorkOfficePermissionCode = new UltraLabel();
		this.txtSocialInssuranceNo = new UltraTextEditor();
		this.lblSocialInssuranceNo = new UltraLabel();
		this.dtpWorkOfficeSendDate = new UltraDateTimeEditor();
		this.dtpWorkOfficeToDate = new UltraDateTimeEditor();
		this.dtpWorkOfficeFromDate = new UltraDateTimeEditor();
		this.dtpInssuranceDate = new UltraDateTimeEditor();
		this.dtpInssuranceEndDate = new UltraDateTimeEditor();
		this.dtpFirstInssuranceDate = new UltraDateTimeEditor();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGDocuments = new UltraGrid();
		this.ultraTabPageControl1 = new UltraTabPageControl();
		this.lblQualificationYear = new UltraLabel();
		this.cboQualificationYear = new UltraComboEditor();
		this.cboSectionName = new UltraComboEditor();
		this.cboUniversity = new UltraComboEditor();
		this.lblSectionName = new UltraLabel();
		this.cboQualification = new UltraComboEditor();
		this.lblUniversity = new UltraLabel();
		this.txtSectionNameNotes = new UltraTextEditor();
		this.lblQualification = new UltraLabel();
		this.lblSectionNameNotes = new UltraLabel();
		this.ULGCetificates = new UltraGrid();
		this.tabRecipe = new UltraTabPageControl();
		this.txtEMail = new UltraTextEditor();
		this.lblEMail = new UltraLabel();
		this.ULGContacts = new UltraGrid();
		this.tabService = new UltraTabPageControl();
		this.TreeAccounts = new UltraTree();
		this.lblAccounts = new UltraLabel();
		this.txtItems = new UltraTextEditor();
		this.btnItemsSearch = new UltraButton();
		this.cboState = new UltraComboEditor();
		this.lblState = new UltraLabel();
		this.tabItemType = new UltraTabControl();
		this.ultraTabSharedControlsPage1 = new UltraTabSharedControlsPage();
		this.ofdItemPic = new System.Windows.Forms.OpenFileDialog();
		this.txtOrder = new UltraTextEditor();
		this.ultraLabel18 = new UltraLabel();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.changeParentTSMenu = new System.Windows.Forms.ToolStripMenuItem();
		this.setAsGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.setAsSubAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.modifyGroupEmployeesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		((System.ComponentModel.ISupportInitialize)base.dtChart).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.treeChart).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtName).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabItem).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dtpPostponedToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkHasPrivateCar).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkHasPassport).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEmployeeNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassportNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDrivingLicenseID).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPersonalIDNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDrivingLicenseExpireDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpPassportExpireDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpIDExpireDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpIDIssueDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBirthDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSocialStatus).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboMilitaryService).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboNationality).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboGender).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboSalesManDefaultStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLeavingWorkNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLeavingWorkReason).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsSalesMan).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.treeAdministrativeStructure).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpContractToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpContractFromDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpEndDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpHireDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDirectManager).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDegree).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPosition).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAdministrativeLevel).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.chkSalaryAtEndOFMonth).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpServicePercentFromDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInServicePercent).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInSalaryTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsMonthlySalary).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCollectionCommission).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalesCommission).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalaryList).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLeavePermissionRule).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVacationRule).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalaryAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPenalityRule).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPenaltyAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboFeedingRule).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAbsenceRule).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAdvanceAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLateRule).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboExtraTimeRule).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBank).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBankAccountNo).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGMotivation).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGYearIncrease).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFixedSalaryValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsFixedTaxPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsFixedSalaryTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGDeductions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGAllownces).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCurrentBasicSalary).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCurrentVariantSalary).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetCurrentSalary).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtStartBasicSalary).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotInsuranceVariantSalary).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtStartVariantSalary).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtStartSalary).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGFamilyRelativesHealthInsurance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInclusiveHealthInsuranceValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSocialInsuranceValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpHealthInsuranceEndDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpHealthInsuranceStartDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkWorkOfficeIsSend).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboHealthInsuranceType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSocialInssuranceOffice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtWorkOfficePermissionCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSocialInssuranceNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpWorkOfficeSendDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpWorkOfficeToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpWorkOfficeFromDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpInssuranceDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpInssuranceEndDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFirstInssuranceDate).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDocuments).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboQualificationYear).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSectionName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboUniversity).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboQualification).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSectionNameNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGCetificates).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabRecipe).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGContacts).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabService).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TreeAccounts).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboState).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tabItemType).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabItemType).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtOrder).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(base.treeChart, "treeChart");
		((System.Windows.Forms.Control)(object)base.treeChart).ContextMenuStrip = this.contextMenuStrip1;
		resources.ApplyResources(base.txtCode, "txtCode");
		((EditorButtonControlBase)base.txtCode).ReadOnly = true;
		resources.ApplyResources(base.txtName, "txtName");
		((EditorButtonControlBase)base.txtName).ReadOnly = true;
		resources.ApplyResources(base.lblPath, "lblPath");
		resources.ApplyResources(base.txtNameEn, "txtNameEn");
		((EditorButtonControlBase)base.txtNameEn).ReadOnly = true;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnAddRoot, "btnAddRoot");
		resources.ApplyResources(base.label1, "label1");
		((AppearanceBase)val).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints");
		((AppearanceBase)val).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val, "appearance184");
		((ControlBase)base.label1).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(base.label2, "label2");
		((AppearanceBase)val2).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val2).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val2).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints1");
		((AppearanceBase)val2).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val2).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		resources.ApplyResources(val2, "appearance185");
		((ControlBase)base.label2).Appearance = (AppearanceBase)(object)val2;
		resources.ApplyResources(base.label3, "label3");
		((AppearanceBase)val3).FontData.BoldAsString = resources.GetString("resource.BoldAsString2");
		((AppearanceBase)val3).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString2");
		((AppearanceBase)val3).FontData.Name = resources.GetString("resource.Name2");
		((AppearanceBase)val3).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints2");
		((AppearanceBase)val3).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString2");
		((AppearanceBase)val3).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString2");
		resources.ApplyResources(val3, "appearance186");
		((ControlBase)base.label3).Appearance = (AppearanceBase)(object)val3;
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
		((AppearanceBase)val4).FontData.BoldAsString = resources.GetString("resource.BoldAsString3");
		((AppearanceBase)val4).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString3");
		((AppearanceBase)val4).FontData.Name = resources.GetString("resource.Name3");
		((AppearanceBase)val4).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString3");
		((AppearanceBase)val4).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString3");
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val4, "appearance4");
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val4;
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.btnImport, "btnImport");
		resources.ApplyResources(base.btnExport, "btnExport");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.tabItem, "tabItem");
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.dtpPostponedToDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel22);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.chkHasPrivateCar);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.chkHasPassport);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.btnPic);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.picEmployee);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboCountry);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblCountry);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboArea);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblArea);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboCity);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblCity);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtEmployeeNo);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtPassportNo);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtDrivingLicenseID);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtPersonalIDNo);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblEmployeeNo);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblPassportNo);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblDrivingLicenseID);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblPersonalIDNo);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.dtpDrivingLicenseExpireDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.dtpPassportExpireDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel23);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.dtpIDExpireDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblPassportExpireDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.dtpIDIssueDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblIDExpireDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.dtpBirthDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblIDIssueDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblBirthDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboSocialStatus);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboMilitaryService);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblSocialStatus);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboNationality);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblMilitaryService);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboGender);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblNationality);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblGender);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtAddress);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblAddress);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		((System.Windows.Forms.Control)(object)this.tabItem).Name = "tabItem";
		resources.ApplyResources(this.dtpPostponedToDate, "dtpPostponedToDate");
		((UltraWinEditorMaskedControlBase)this.dtpPostponedToDate).AlwaysInEditMode = true;
		this.dtpPostponedToDate.DateTime = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpPostponedToDate).Name = "dtpPostponedToDate";
		this.dtpPostponedToDate.Value = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		resources.ApplyResources(this.ultraLabel22, "ultraLabel22");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val5, "appearance187");
		((ControlBase)this.ultraLabel22).Appearance = (AppearanceBase)(object)val5;
		this.ultraLabel22.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel22).Name = "ultraLabel22";
		((ControlBase)this.ultraLabel22).WrapText = false;
		resources.ApplyResources(this.chkHasPrivateCar, "chkHasPrivateCar");
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val6, "appearance188");
		((UltraToggleEditorBase)this.chkHasPrivateCar).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.chkHasPrivateCar).Name = "chkHasPrivateCar";
		resources.ApplyResources(this.chkHasPassport, "chkHasPassport");
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance189");
		((UltraToggleEditorBase)this.chkHasPassport).Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.chkHasPassport).Name = "chkHasPassport";
		resources.ApplyResources(this.btnPic, "btnPic");
		((System.Windows.Forms.Control)(object)this.btnPic).Name = "btnPic";
		((System.Windows.Forms.Control)(object)this.btnPic).Click += new System.EventHandler(btnPic_Click);
		resources.ApplyResources(this.picEmployee, "picEmployee");
		this.picEmployee.BorderShadowColor = System.Drawing.Color.Empty;
		this.picEmployee.BorderStyle = (UIElementBorderStyle)2;
		((System.Windows.Forms.Control)(object)this.picEmployee).Name = "picEmployee";
		resources.ApplyResources(this.cboCountry, "cboCountry");
		((System.Windows.Forms.Control)(object)this.cboCountry).Name = "cboCountry";
		((TextEditorControlBase)this.cboCountry).ValueChanged += new System.EventHandler(cboCountry_ValueChanged);
		resources.ApplyResources(this.lblCountry, "lblCountry");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance190");
		((ControlBase)this.lblCountry).Appearance = (AppearanceBase)(object)val8;
		this.lblCountry.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCountry).Name = "lblCountry";
		((ControlBase)this.lblCountry).WrapText = false;
		resources.ApplyResources(this.cboArea, "cboArea");
		((System.Windows.Forms.Control)(object)this.cboArea).Name = "cboArea";
		resources.ApplyResources(this.lblArea, "lblArea");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance191");
		((ControlBase)this.lblArea).Appearance = (AppearanceBase)(object)val9;
		this.lblArea.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArea).Name = "lblArea";
		((ControlBase)this.lblArea).WrapText = false;
		resources.ApplyResources(this.cboCity, "cboCity");
		((System.Windows.Forms.Control)(object)this.cboCity).Name = "cboCity";
		((TextEditorControlBase)this.cboCity).ValueChanged += new System.EventHandler(cboCity_ValueChanged);
		resources.ApplyResources(this.lblCity, "lblCity");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance192");
		((ControlBase)this.lblCity).Appearance = (AppearanceBase)(object)val10;
		this.lblCity.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCity).Name = "lblCity";
		((ControlBase)this.lblCity).WrapText = false;
		resources.ApplyResources(this.txtEmployeeNo, "txtEmployeeNo");
		((System.Windows.Forms.Control)(object)this.txtEmployeeNo).Name = "txtEmployeeNo";
		((System.Windows.Forms.Control)(object)this.txtEmployeeNo).KeyDown += new System.Windows.Forms.KeyEventHandler(txtEmployeeNo_KeyDown);
		resources.ApplyResources(this.txtPassportNo, "txtPassportNo");
		((System.Windows.Forms.Control)(object)this.txtPassportNo).Name = "txtPassportNo";
		resources.ApplyResources(this.txtDrivingLicenseID, "txtDrivingLicenseID");
		((System.Windows.Forms.Control)(object)this.txtDrivingLicenseID).Name = "txtDrivingLicenseID";
		resources.ApplyResources(this.txtPersonalIDNo, "txtPersonalIDNo");
		((System.Windows.Forms.Control)(object)this.txtPersonalIDNo).Name = "txtPersonalIDNo";
		resources.ApplyResources(this.lblEmployeeNo, "lblEmployeeNo");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance193");
		((ControlBase)this.lblEmployeeNo).Appearance = (AppearanceBase)(object)val11;
		this.lblEmployeeNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEmployeeNo).Name = "lblEmployeeNo";
		((ControlBase)this.lblEmployeeNo).WrapText = false;
		resources.ApplyResources(this.lblPassportNo, "lblPassportNo");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance194");
		((ControlBase)this.lblPassportNo).Appearance = (AppearanceBase)(object)val12;
		this.lblPassportNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPassportNo).Name = "lblPassportNo";
		((ControlBase)this.lblPassportNo).WrapText = false;
		resources.ApplyResources(this.lblDrivingLicenseID, "lblDrivingLicenseID");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance195");
		((ControlBase)this.lblDrivingLicenseID).Appearance = (AppearanceBase)(object)val13;
		this.lblDrivingLicenseID.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDrivingLicenseID).Name = "lblDrivingLicenseID";
		((ControlBase)this.lblDrivingLicenseID).WrapText = false;
		resources.ApplyResources(this.lblPersonalIDNo, "lblPersonalIDNo");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val14, "appearance196");
		((ControlBase)this.lblPersonalIDNo).Appearance = (AppearanceBase)(object)val14;
		this.lblPersonalIDNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPersonalIDNo).Name = "lblPersonalIDNo";
		((ControlBase)this.lblPersonalIDNo).WrapText = false;
		resources.ApplyResources(this.dtpDrivingLicenseExpireDate, "dtpDrivingLicenseExpireDate");
		((UltraWinEditorMaskedControlBase)this.dtpDrivingLicenseExpireDate).AlwaysInEditMode = true;
		this.dtpDrivingLicenseExpireDate.DateTime = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpDrivingLicenseExpireDate).Name = "dtpDrivingLicenseExpireDate";
		this.dtpDrivingLicenseExpireDate.Value = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		resources.ApplyResources(this.dtpPassportExpireDate, "dtpPassportExpireDate");
		((UltraWinEditorMaskedControlBase)this.dtpPassportExpireDate).AlwaysInEditMode = true;
		this.dtpPassportExpireDate.DateTime = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpPassportExpireDate).Name = "dtpPassportExpireDate";
		this.dtpPassportExpireDate.Value = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		resources.ApplyResources(this.ultraLabel23, "ultraLabel23");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val15, "appearance197");
		((ControlBase)this.ultraLabel23).Appearance = (AppearanceBase)(object)val15;
		this.ultraLabel23.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel23).Name = "ultraLabel23";
		((ControlBase)this.ultraLabel23).WrapText = false;
		resources.ApplyResources(this.dtpIDExpireDate, "dtpIDExpireDate");
		((UltraWinEditorMaskedControlBase)this.dtpIDExpireDate).AlwaysInEditMode = true;
		this.dtpIDExpireDate.DateTime = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpIDExpireDate).Name = "dtpIDExpireDate";
		this.dtpIDExpireDate.Value = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		resources.ApplyResources(this.lblPassportExpireDate, "lblPassportExpireDate");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val16, "appearance198");
		((ControlBase)this.lblPassportExpireDate).Appearance = (AppearanceBase)(object)val16;
		this.lblPassportExpireDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPassportExpireDate).Name = "lblPassportExpireDate";
		((ControlBase)this.lblPassportExpireDate).WrapText = false;
		resources.ApplyResources(this.dtpIDIssueDate, "dtpIDIssueDate");
		((UltraWinEditorMaskedControlBase)this.dtpIDIssueDate).AlwaysInEditMode = true;
		this.dtpIDIssueDate.DateTime = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpIDIssueDate).Name = "dtpIDIssueDate";
		this.dtpIDIssueDate.Value = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		resources.ApplyResources(this.lblIDExpireDate, "lblIDExpireDate");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val17, "appearance199");
		((ControlBase)this.lblIDExpireDate).Appearance = (AppearanceBase)(object)val17;
		this.lblIDExpireDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblIDExpireDate).Name = "lblIDExpireDate";
		((ControlBase)this.lblIDExpireDate).WrapText = false;
		resources.ApplyResources(this.dtpBirthDate, "dtpBirthDate");
		((UltraWinEditorMaskedControlBase)this.dtpBirthDate).AlwaysInEditMode = true;
		this.dtpBirthDate.DateTime = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpBirthDate).Name = "dtpBirthDate";
		this.dtpBirthDate.Value = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		resources.ApplyResources(this.lblIDIssueDate, "lblIDIssueDate");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val18).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val18, "appearance200");
		((ControlBase)this.lblIDIssueDate).Appearance = (AppearanceBase)(object)val18;
		this.lblIDIssueDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblIDIssueDate).Name = "lblIDIssueDate";
		((ControlBase)this.lblIDIssueDate).WrapText = false;
		resources.ApplyResources(this.lblBirthDate, "lblBirthDate");
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val19, "appearance201");
		((ControlBase)this.lblBirthDate).Appearance = (AppearanceBase)(object)val19;
		this.lblBirthDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBirthDate).Name = "lblBirthDate";
		((ControlBase)this.lblBirthDate).WrapText = false;
		resources.ApplyResources(this.cboSocialStatus, "cboSocialStatus");
		((System.Windows.Forms.Control)(object)this.cboSocialStatus).Name = "cboSocialStatus";
		resources.ApplyResources(this.cboMilitaryService, "cboMilitaryService");
		((System.Windows.Forms.Control)(object)this.cboMilitaryService).Name = "cboMilitaryService";
		resources.ApplyResources(this.lblSocialStatus, "lblSocialStatus");
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val20, "appearance202");
		((ControlBase)this.lblSocialStatus).Appearance = (AppearanceBase)(object)val20;
		this.lblSocialStatus.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSocialStatus).Name = "lblSocialStatus";
		((ControlBase)this.lblSocialStatus).WrapText = false;
		resources.ApplyResources(this.cboNationality, "cboNationality");
		((System.Windows.Forms.Control)(object)this.cboNationality).Name = "cboNationality";
		resources.ApplyResources(this.lblMilitaryService, "lblMilitaryService");
		((AppearanceBase)val21).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val21).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val21, "appearance203");
		((ControlBase)this.lblMilitaryService).Appearance = (AppearanceBase)(object)val21;
		this.lblMilitaryService.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMilitaryService).Name = "lblMilitaryService";
		((ControlBase)this.lblMilitaryService).WrapText = false;
		resources.ApplyResources(this.cboGender, "cboGender");
		((System.Windows.Forms.Control)(object)this.cboGender).Name = "cboGender";
		resources.ApplyResources(this.lblNationality, "lblNationality");
		((AppearanceBase)val22).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val22).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val22, "appearance204");
		((ControlBase)this.lblNationality).Appearance = (AppearanceBase)(object)val22;
		this.lblNationality.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNationality).Name = "lblNationality";
		((ControlBase)this.lblNationality).WrapText = false;
		resources.ApplyResources(this.lblGender, "lblGender");
		((AppearanceBase)val23).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val23).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val23, "appearance205");
		((ControlBase)this.lblGender).Appearance = (AppearanceBase)(object)val23;
		this.lblGender.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGender).Name = "lblGender";
		((ControlBase)this.lblGender).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.txtAddress, "txtAddress");
		((System.Windows.Forms.Control)(object)this.txtAddress).Name = "txtAddress";
		resources.ApplyResources(this.lblAddress, "lblAddress");
		((AppearanceBase)val24).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val24).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val24, "appearance206");
		((ControlBase)this.lblAddress).Appearance = (AppearanceBase)(object)val24;
		this.lblAddress.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAddress).Name = "lblAddress";
		((ControlBase)this.lblAddress).WrapText = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((AppearanceBase)val25).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val25).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val25, "appearance207");
		((ControlBase)this.lblNotes).Appearance = (AppearanceBase)(object)val25;
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel28);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.btnStoreSearch);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.cboSalesManDefaultStore);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.txtLeavingWorkNotes);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel25);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.cboLeavingWorkReason);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel24);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.chkIsSalesMan);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.treeAdministrativeStructure);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.dtpContractToDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.dtpContractFromDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.dtpEndDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblContractToDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.dtpHireDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblEndDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblContractFromDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblAdministrativeStructure);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblHireDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.cboDirectManager);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblDirectManager);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.cboDegree);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblDegreeID);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.cboPosition);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblPosition);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.cboBranch);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblBranch);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.cboAdministrativeLevel);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblAdministrativeLevel);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		resources.ApplyResources(this.ultraLabel28, "ultraLabel28");
		((AppearanceBase)val26).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val26).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val26, "appearance208");
		((ControlBase)this.ultraLabel28).Appearance = (AppearanceBase)(object)val26;
		this.ultraLabel28.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel28).Name = "ultraLabel28";
		((ControlBase)this.ultraLabel28).WrapText = false;
		((UltraButtonBase)this.btnStoreSearch).AcceptsFocus = false;
		resources.ApplyResources(this.btnStoreSearch, "btnStoreSearch");
		((AppearanceBase)val27).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val27, "appearance209");
		((ControlBase)this.btnStoreSearch).Appearance = (AppearanceBase)(object)val27;
		((System.Windows.Forms.Control)(object)this.btnStoreSearch).Name = "btnStoreSearch";
		resources.ApplyResources(this.cboSalesManDefaultStore, "cboSalesManDefaultStore");
		((TextEditorControlBase)this.cboSalesManDefaultStore).AlwaysInEditMode = true;
		this.cboSalesManDefaultStore.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSalesManDefaultStore).Name = "cboSalesManDefaultStore";
		resources.ApplyResources(this.txtLeavingWorkNotes, "txtLeavingWorkNotes");
		((System.Windows.Forms.Control)(object)this.txtLeavingWorkNotes).Name = "txtLeavingWorkNotes";
		resources.ApplyResources(this.ultraLabel25, "ultraLabel25");
		((AppearanceBase)val28).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val28).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val28, "appearance210");
		((ControlBase)this.ultraLabel25).Appearance = (AppearanceBase)(object)val28;
		this.ultraLabel25.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel25).Name = "ultraLabel25";
		((ControlBase)this.ultraLabel25).WrapText = false;
		resources.ApplyResources(this.cboLeavingWorkReason, "cboLeavingWorkReason");
		((System.Windows.Forms.Control)(object)this.cboLeavingWorkReason).Name = "cboLeavingWorkReason";
		resources.ApplyResources(this.ultraLabel24, "ultraLabel24");
		((AppearanceBase)val29).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val29).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val29, "appearance211");
		((ControlBase)this.ultraLabel24).Appearance = (AppearanceBase)(object)val29;
		this.ultraLabel24.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel24).Name = "ultraLabel24";
		((ControlBase)this.ultraLabel24).WrapText = false;
		resources.ApplyResources(this.chkIsSalesMan, "chkIsSalesMan");
		((AppearanceBase)val30).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val30, "appearance212");
		((UltraToggleEditorBase)this.chkIsSalesMan).Appearance = (AppearanceBase)(object)val30;
		((System.Windows.Forms.Control)(object)this.chkIsSalesMan).Name = "chkIsSalesMan";
		((UltraToggleEditorBase)this.chkIsSalesMan).CheckedChanged += new System.EventHandler(chkIsSalesMan_CheckedChanged);
		resources.ApplyResources(this.treeAdministrativeStructure, "treeAdministrativeStructure");
		((System.Windows.Forms.Control)(object)this.treeAdministrativeStructure).Name = "treeAdministrativeStructure";
		val31.NodeStyle = (NodeStyle)1;
		this.treeAdministrativeStructure.Override = val31;
		this.treeAdministrativeStructure.BeforeCheck += new BeforeCheckEventHandler(treeAdministrativeStructure_BeforeCheck);
		resources.ApplyResources(this.dtpContractToDate, "dtpContractToDate");
		((UltraWinEditorMaskedControlBase)this.dtpContractToDate).AlwaysInEditMode = true;
		this.dtpContractToDate.DateTime = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpContractToDate).Name = "dtpContractToDate";
		this.dtpContractToDate.Value = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		resources.ApplyResources(this.dtpContractFromDate, "dtpContractFromDate");
		((UltraWinEditorMaskedControlBase)this.dtpContractFromDate).AlwaysInEditMode = true;
		this.dtpContractFromDate.DateTime = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpContractFromDate).Name = "dtpContractFromDate";
		this.dtpContractFromDate.Value = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		this.dtpContractFromDate.ValueChanged += new System.EventHandler(dtpContractFromDate_ValueChanged);
		resources.ApplyResources(this.dtpEndDate, "dtpEndDate");
		((UltraWinEditorMaskedControlBase)this.dtpEndDate).AlwaysInEditMode = true;
		this.dtpEndDate.DateTime = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpEndDate).Name = "dtpEndDate";
		this.dtpEndDate.Value = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		resources.ApplyResources(this.lblContractToDate, "lblContractToDate");
		((AppearanceBase)val32).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val32).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val32, "appearance213");
		((ControlBase)this.lblContractToDate).Appearance = (AppearanceBase)(object)val32;
		this.lblContractToDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblContractToDate).Name = "lblContractToDate";
		((ControlBase)this.lblContractToDate).WrapText = false;
		resources.ApplyResources(this.dtpHireDate, "dtpHireDate");
		((UltraWinEditorMaskedControlBase)this.dtpHireDate).AlwaysInEditMode = true;
		this.dtpHireDate.DateTime = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpHireDate).Name = "dtpHireDate";
		this.dtpHireDate.Value = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		this.dtpHireDate.ValueChanged += new System.EventHandler(dtpHireDate_ValueChanged);
		resources.ApplyResources(this.lblEndDate, "lblEndDate");
		((AppearanceBase)val33).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val33).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val33, "appearance214");
		((ControlBase)this.lblEndDate).Appearance = (AppearanceBase)(object)val33;
		this.lblEndDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEndDate).Name = "lblEndDate";
		((ControlBase)this.lblEndDate).WrapText = false;
		resources.ApplyResources(this.lblContractFromDate, "lblContractFromDate");
		((AppearanceBase)val34).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val34).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val34, "appearance215");
		((ControlBase)this.lblContractFromDate).Appearance = (AppearanceBase)(object)val34;
		this.lblContractFromDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblContractFromDate).Name = "lblContractFromDate";
		((ControlBase)this.lblContractFromDate).WrapText = false;
		resources.ApplyResources(this.lblAdministrativeStructure, "lblAdministrativeStructure");
		((AppearanceBase)val35).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val35).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val35, "appearance216");
		((ControlBase)this.lblAdministrativeStructure).Appearance = (AppearanceBase)(object)val35;
		this.lblAdministrativeStructure.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAdministrativeStructure).Name = "lblAdministrativeStructure";
		((ControlBase)this.lblAdministrativeStructure).WrapText = false;
		resources.ApplyResources(this.lblHireDate, "lblHireDate");
		((AppearanceBase)val36).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val36).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val36, "appearance217");
		((ControlBase)this.lblHireDate).Appearance = (AppearanceBase)(object)val36;
		this.lblHireDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblHireDate).Name = "lblHireDate";
		((ControlBase)this.lblHireDate).WrapText = false;
		resources.ApplyResources(this.cboDirectManager, "cboDirectManager");
		((System.Windows.Forms.Control)(object)this.cboDirectManager).Name = "cboDirectManager";
		resources.ApplyResources(this.lblDirectManager, "lblDirectManager");
		((AppearanceBase)val37).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val37).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val37, "appearance218");
		((ControlBase)this.lblDirectManager).Appearance = (AppearanceBase)(object)val37;
		this.lblDirectManager.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDirectManager).Name = "lblDirectManager";
		((ControlBase)this.lblDirectManager).WrapText = false;
		resources.ApplyResources(this.cboDegree, "cboDegree");
		((System.Windows.Forms.Control)(object)this.cboDegree).Name = "cboDegree";
		resources.ApplyResources(this.lblDegreeID, "lblDegreeID");
		((AppearanceBase)val38).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val38).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val38, "appearance219");
		((ControlBase)this.lblDegreeID).Appearance = (AppearanceBase)(object)val38;
		this.lblDegreeID.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDegreeID).Name = "lblDegreeID";
		((ControlBase)this.lblDegreeID).WrapText = false;
		resources.ApplyResources(this.cboPosition, "cboPosition");
		((System.Windows.Forms.Control)(object)this.cboPosition).Name = "cboPosition";
		resources.ApplyResources(this.lblPosition, "lblPosition");
		((AppearanceBase)val39).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val39).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val39, "appearance220");
		((ControlBase)this.lblPosition).Appearance = (AppearanceBase)(object)val39;
		this.lblPosition.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPosition).Name = "lblPosition";
		((ControlBase)this.lblPosition).WrapText = false;
		resources.ApplyResources(this.cboBranch, "cboBranch");
		((System.Windows.Forms.Control)(object)this.cboBranch).Name = "cboBranch";
		((TextEditorControlBase)this.cboBranch).ValueChanged += new System.EventHandler(cboBranch_ValueChanged);
		resources.ApplyResources(this.lblBranch, "lblBranch");
		((AppearanceBase)val40).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val40).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val40, "appearance221");
		((ControlBase)this.lblBranch).Appearance = (AppearanceBase)(object)val40;
		this.lblBranch.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBranch).Name = "lblBranch";
		((ControlBase)this.lblBranch).WrapText = false;
		resources.ApplyResources(this.cboAdministrativeLevel, "cboAdministrativeLevel");
		((System.Windows.Forms.Control)(object)this.cboAdministrativeLevel).Name = "cboAdministrativeLevel";
		resources.ApplyResources(this.lblAdministrativeLevel, "lblAdministrativeLevel");
		((AppearanceBase)val41).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val41).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val41, "appearance222");
		((ControlBase)this.lblAdministrativeLevel).Appearance = (AppearanceBase)(object)val41;
		this.lblAdministrativeLevel.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAdministrativeLevel).Name = "lblAdministrativeLevel";
		((ControlBase)this.lblAdministrativeLevel).WrapText = false;
		resources.ApplyResources(this.ultraTabPageControl6, "ultraTabPageControl6");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.chkSalaryAtEndOFMonth);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.dtpServicePercentFromDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.chkInServicePercent);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.chkInSalaryTax);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.chkIsMonthlySalary);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.txtCollectionCommission);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.txtSalesCommission);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.lblCollectionCommission);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesCommissionRatio);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.cboSalaryList);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.lblSalaryList);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.cboLeavePermissionRule);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.lblLeaveRule);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.cboVacationRule);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel13);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.cboSalaryAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.cboPenalityRule);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel17);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel12);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.cboPenaltyAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel16);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.cboFeedingRule);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.cboAbsenceRule);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.lblFeedingRule);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.cboAdvanceAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel11);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel15);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.cboLateRule);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel10);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.cboExtraTimeRule);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel9);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.cboBank);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.lblBank);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.txtBankAccountNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.lblBankAccountNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Name = "ultraTabPageControl6";
		resources.ApplyResources(this.chkSalaryAtEndOFMonth, "chkSalaryAtEndOFMonth");
		((AppearanceBase)val42).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val42, "appearance223");
		((UltraToggleEditorBase)this.chkSalaryAtEndOFMonth).Appearance = (AppearanceBase)(object)val42;
		((System.Windows.Forms.Control)(object)this.chkSalaryAtEndOFMonth).Name = "chkSalaryAtEndOFMonth";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val43).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val43).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val43, "appearance224");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val43;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.dtpServicePercentFromDate, "dtpServicePercentFromDate");
		((UltraWinEditorMaskedControlBase)this.dtpServicePercentFromDate).AlwaysInEditMode = true;
		this.dtpServicePercentFromDate.DateTime = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpServicePercentFromDate).Name = "dtpServicePercentFromDate";
		this.dtpServicePercentFromDate.Value = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		resources.ApplyResources(this.chkInServicePercent, "chkInServicePercent");
		((AppearanceBase)val44).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val44, "appearance225");
		((UltraToggleEditorBase)this.chkInServicePercent).Appearance = (AppearanceBase)(object)val44;
		((UltraToggleEditorBase)this.chkInServicePercent).Checked = true;
		((UltraToggleEditorBase)this.chkInServicePercent).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkInServicePercent).Name = "chkInServicePercent";
		resources.ApplyResources(this.chkInSalaryTax, "chkInSalaryTax");
		((AppearanceBase)val45).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val45, "appearance226");
		((UltraToggleEditorBase)this.chkInSalaryTax).Appearance = (AppearanceBase)(object)val45;
		((UltraToggleEditorBase)this.chkInSalaryTax).Checked = true;
		((UltraToggleEditorBase)this.chkInSalaryTax).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkInSalaryTax).Name = "chkInSalaryTax";
		resources.ApplyResources(this.chkIsMonthlySalary, "chkIsMonthlySalary");
		((AppearanceBase)val46).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val46, "appearance227");
		((UltraToggleEditorBase)this.chkIsMonthlySalary).Appearance = (AppearanceBase)(object)val46;
		((UltraToggleEditorBase)this.chkIsMonthlySalary).Checked = true;
		((UltraToggleEditorBase)this.chkIsMonthlySalary).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsMonthlySalary).Name = "chkIsMonthlySalary";
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		((AppearanceBase)val47).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val47).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val47, "appearance228");
		((ControlBase)this.ultraLabel4).Appearance = (AppearanceBase)(object)val47;
		this.ultraLabel4.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val48).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val48).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val48, "appearance229");
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val48;
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		resources.ApplyResources(this.txtCollectionCommission, "txtCollectionCommission");
		((System.Windows.Forms.Control)(object)this.txtCollectionCommission).Name = "txtCollectionCommission";
		((System.Windows.Forms.Control)(object)this.txtCollectionCommission).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtSalesCommission, "txtSalesCommission");
		((System.Windows.Forms.Control)(object)this.txtSalesCommission).Name = "txtSalesCommission";
		((System.Windows.Forms.Control)(object)this.txtSalesCommission).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblCollectionCommission, "lblCollectionCommission");
		((AppearanceBase)val49).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val49).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val49, "appearance230");
		((ControlBase)this.lblCollectionCommission).Appearance = (AppearanceBase)(object)val49;
		this.lblCollectionCommission.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCollectionCommission).Name = "lblCollectionCommission";
		((ControlBase)this.lblCollectionCommission).WrapText = false;
		resources.ApplyResources(this.lblSalesCommissionRatio, "lblSalesCommissionRatio");
		((AppearanceBase)val50).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val50).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val50, "appearance231");
		((ControlBase)this.lblSalesCommissionRatio).Appearance = (AppearanceBase)(object)val50;
		this.lblSalesCommissionRatio.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSalesCommissionRatio).Name = "lblSalesCommissionRatio";
		((ControlBase)this.lblSalesCommissionRatio).WrapText = false;
		resources.ApplyResources(this.cboSalaryList, "cboSalaryList");
		((System.Windows.Forms.Control)(object)this.cboSalaryList).Name = "cboSalaryList";
		resources.ApplyResources(this.lblSalaryList, "lblSalaryList");
		((AppearanceBase)val51).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val51).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val51, "appearance232");
		((ControlBase)this.lblSalaryList).Appearance = (AppearanceBase)(object)val51;
		this.lblSalaryList.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSalaryList).Name = "lblSalaryList";
		((ControlBase)this.lblSalaryList).WrapText = false;
		resources.ApplyResources(this.cboLeavePermissionRule, "cboLeavePermissionRule");
		((System.Windows.Forms.Control)(object)this.cboLeavePermissionRule).Name = "cboLeavePermissionRule";
		resources.ApplyResources(this.lblLeaveRule, "lblLeaveRule");
		((AppearanceBase)val52).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val52).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val52, "appearance233");
		((ControlBase)this.lblLeaveRule).Appearance = (AppearanceBase)(object)val52;
		this.lblLeaveRule.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLeaveRule).Name = "lblLeaveRule";
		((ControlBase)this.lblLeaveRule).WrapText = false;
		resources.ApplyResources(this.cboVacationRule, "cboVacationRule");
		((System.Windows.Forms.Control)(object)this.cboVacationRule).Name = "cboVacationRule";
		resources.ApplyResources(this.ultraLabel13, "ultraLabel13");
		((AppearanceBase)val53).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val53).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val53, "appearance234");
		((ControlBase)this.ultraLabel13).Appearance = (AppearanceBase)(object)val53;
		this.ultraLabel13.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel13).Name = "ultraLabel13";
		((ControlBase)this.ultraLabel13).WrapText = false;
		resources.ApplyResources(this.cboSalaryAccount, "cboSalaryAccount");
		((System.Windows.Forms.Control)(object)this.cboSalaryAccount).Name = "cboSalaryAccount";
		resources.ApplyResources(this.cboPenalityRule, "cboPenalityRule");
		((System.Windows.Forms.Control)(object)this.cboPenalityRule).Name = "cboPenalityRule";
		resources.ApplyResources(this.ultraLabel17, "ultraLabel17");
		((AppearanceBase)val54).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val54).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val54, "appearance235");
		((ControlBase)this.ultraLabel17).Appearance = (AppearanceBase)(object)val54;
		this.ultraLabel17.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel17).Name = "ultraLabel17";
		((ControlBase)this.ultraLabel17).WrapText = false;
		resources.ApplyResources(this.ultraLabel12, "ultraLabel12");
		((AppearanceBase)val55).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val55).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val55, "appearance236");
		((ControlBase)this.ultraLabel12).Appearance = (AppearanceBase)(object)val55;
		this.ultraLabel12.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel12).Name = "ultraLabel12";
		((ControlBase)this.ultraLabel12).WrapText = false;
		resources.ApplyResources(this.cboPenaltyAccount, "cboPenaltyAccount");
		((System.Windows.Forms.Control)(object)this.cboPenaltyAccount).Name = "cboPenaltyAccount";
		resources.ApplyResources(this.ultraLabel16, "ultraLabel16");
		((AppearanceBase)val56).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val56).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val56, "appearance237");
		((ControlBase)this.ultraLabel16).Appearance = (AppearanceBase)(object)val56;
		this.ultraLabel16.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel16).Name = "ultraLabel16";
		((ControlBase)this.ultraLabel16).WrapText = false;
		resources.ApplyResources(this.cboFeedingRule, "cboFeedingRule");
		((System.Windows.Forms.Control)(object)this.cboFeedingRule).Name = "cboFeedingRule";
		resources.ApplyResources(this.cboAbsenceRule, "cboAbsenceRule");
		((System.Windows.Forms.Control)(object)this.cboAbsenceRule).Name = "cboAbsenceRule";
		resources.ApplyResources(this.lblFeedingRule, "lblFeedingRule");
		((AppearanceBase)val57).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val57).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val57, "appearance238");
		((ControlBase)this.lblFeedingRule).Appearance = (AppearanceBase)(object)val57;
		this.lblFeedingRule.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFeedingRule).Name = "lblFeedingRule";
		((ControlBase)this.lblFeedingRule).WrapText = false;
		resources.ApplyResources(this.cboAdvanceAccount, "cboAdvanceAccount");
		((System.Windows.Forms.Control)(object)this.cboAdvanceAccount).Name = "cboAdvanceAccount";
		resources.ApplyResources(this.ultraLabel11, "ultraLabel11");
		((AppearanceBase)val58).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val58).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val58, "appearance239");
		((ControlBase)this.ultraLabel11).Appearance = (AppearanceBase)(object)val58;
		this.ultraLabel11.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel11).Name = "ultraLabel11";
		((ControlBase)this.ultraLabel11).WrapText = false;
		resources.ApplyResources(this.ultraLabel15, "ultraLabel15");
		((AppearanceBase)val59).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val59).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val59, "appearance240");
		((ControlBase)this.ultraLabel15).Appearance = (AppearanceBase)(object)val59;
		this.ultraLabel15.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel15).Name = "ultraLabel15";
		((ControlBase)this.ultraLabel15).WrapText = false;
		resources.ApplyResources(this.cboLateRule, "cboLateRule");
		((System.Windows.Forms.Control)(object)this.cboLateRule).Name = "cboLateRule";
		resources.ApplyResources(this.ultraLabel10, "ultraLabel10");
		((AppearanceBase)val60).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val60).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val60, "appearance241");
		((ControlBase)this.ultraLabel10).Appearance = (AppearanceBase)(object)val60;
		this.ultraLabel10.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel10).Name = "ultraLabel10";
		((ControlBase)this.ultraLabel10).WrapText = false;
		resources.ApplyResources(this.cboExtraTimeRule, "cboExtraTimeRule");
		((System.Windows.Forms.Control)(object)this.cboExtraTimeRule).Name = "cboExtraTimeRule";
		resources.ApplyResources(this.ultraLabel9, "ultraLabel9");
		((AppearanceBase)val61).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val61).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val61, "appearance242");
		((ControlBase)this.ultraLabel9).Appearance = (AppearanceBase)(object)val61;
		this.ultraLabel9.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel9).Name = "ultraLabel9";
		((ControlBase)this.ultraLabel9).WrapText = false;
		resources.ApplyResources(this.cboBank, "cboBank");
		((System.Windows.Forms.Control)(object)this.cboBank).Name = "cboBank";
		resources.ApplyResources(this.lblBank, "lblBank");
		((AppearanceBase)val62).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val62).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val62, "appearance243");
		((ControlBase)this.lblBank).Appearance = (AppearanceBase)(object)val62;
		((System.Windows.Forms.Control)(object)this.lblBank).Name = "lblBank";
		((ControlBase)this.lblBank).WrapText = false;
		resources.ApplyResources(this.txtBankAccountNo, "txtBankAccountNo");
		((System.Windows.Forms.Control)(object)this.txtBankAccountNo).Name = "txtBankAccountNo";
		resources.ApplyResources(this.lblBankAccountNo, "lblBankAccountNo");
		((AppearanceBase)val63).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val63).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val63, "appearance244");
		((ControlBase)this.lblBankAccountNo).Appearance = (AppearanceBase)(object)val63;
		this.lblBankAccountNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBankAccountNo).Name = "lblBankAccountNo";
		((ControlBase)this.lblBankAccountNo).WrapText = false;
		resources.ApplyResources(this.ultraTabPageControl4, "ultraTabPageControl4");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel20);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel19);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ULGMotivation);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ULGYearIncrease);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.txtFixedSalaryValue);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.chkIsFixedTaxPercentage);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.chkIsFixedSalaryTax);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDeductions);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ULGAllownces);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.txtCurrentBasicSalary);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.txtCurrentVariantSalary);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.txtNetCurrentSalary);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.txtStartBasicSalary);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel7);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.txtNotInsuranceVariantSalary);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.txtStartVariantSalary);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.txtStartSalary);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel21);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblNetCurrentSalary);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblDeductions);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblStartSalary);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel14);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Name = "ultraTabPageControl4";
		resources.ApplyResources(this.ultraLabel20, "ultraLabel20");
		((AppearanceBase)val64).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val64).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val64, "appearance245");
		((ControlBase)this.ultraLabel20).Appearance = (AppearanceBase)(object)val64;
		this.ultraLabel20.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel20).Name = "ultraLabel20";
		((ControlBase)this.ultraLabel20).WrapText = false;
		resources.ApplyResources(this.ultraLabel19, "ultraLabel19");
		((AppearanceBase)val65).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val65).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val65, "appearance246");
		((ControlBase)this.ultraLabel19).Appearance = (AppearanceBase)(object)val65;
		this.ultraLabel19.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel19).Name = "ultraLabel19";
		((ControlBase)this.ultraLabel19).WrapText = false;
		resources.ApplyResources(this.ULGMotivation, "ULGMotivation");
		((AppearanceBase)val66).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val66).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val66).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val66).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val66, "appearance65");
		((SpecialBoxBase)((UltraGridBase)this.ULGMotivation).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val66;
		((AppearanceBase)val67).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val67, "appearance66");
		((UltraGridBase)this.ULGMotivation).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val67;
		((SpecialBoxBase)((UltraGridBase)this.ULGMotivation).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val68).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val68).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val68).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val68).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val68, "appearance67");
		((UltraGridBase)this.ULGMotivation).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val68;
		((UltraGridBase)this.ULGMotivation).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGMotivation).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val69).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val69).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val69, "appearance68");
		((UltraGridBase)this.ULGMotivation).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val69;
		((AppearanceBase)val70).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val70).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val70, "appearance69");
		((UltraGridBase)this.ULGMotivation).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val70;
		((UltraGridBase)this.ULGMotivation).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGMotivation).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val71).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val71, "appearance70");
		((UltraGridBase)this.ULGMotivation).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val71;
		((AppearanceBase)val72).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val72, "appearance71");
		((AppearanceBase)val72).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGMotivation).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val72;
		((UltraGridBase)this.ULGMotivation).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGMotivation).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val73).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val73).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val73).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val73).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val73).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val73, "appearance72");
		((UltraGridBase)this.ULGMotivation).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val73;
		((UltraGridBase)this.ULGMotivation).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGMotivation).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val74).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val74).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val74, "appearance73");
		((UltraGridBase)this.ULGMotivation).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val74;
		((UltraGridBase)this.ULGMotivation).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val75).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val75, "appearance74");
		((UltraGridBase)this.ULGMotivation).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val75;
		((System.Windows.Forms.Control)(object)this.ULGMotivation).Name = "ULGMotivation";
		this.ULGMotivation.AfterCellUpdate += new CellEventHandler(ULGMotivation_AfterCellUpdate);
		this.ULGMotivation.AfterEnterEditMode += new System.EventHandler(ULGMotivation_AfterEnterEditMode);
		this.ULGMotivation.AfterRowsDeleted += new System.EventHandler(ULGMotivation_AfterRowsDeleted);
		this.ULGMotivation.CellListSelect += new CellEventHandler(ULGMotivation_CellListSelect);
		resources.ApplyResources(this.ULGYearIncrease, "ULGYearIncrease");
		((AppearanceBase)val76).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val76).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val76).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val76).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val76, "appearance75");
		((SpecialBoxBase)((UltraGridBase)this.ULGYearIncrease).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val76;
		((AppearanceBase)val77).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val77, "appearance76");
		((UltraGridBase)this.ULGYearIncrease).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val77;
		((SpecialBoxBase)((UltraGridBase)this.ULGYearIncrease).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val78).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val78).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val78).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val78).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val78, "appearance77");
		((UltraGridBase)this.ULGYearIncrease).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val78;
		((UltraGridBase)this.ULGYearIncrease).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGYearIncrease).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val79).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val79).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val79, "appearance78");
		((UltraGridBase)this.ULGYearIncrease).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val79;
		((AppearanceBase)val80).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val80).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val80, "appearance79");
		((UltraGridBase)this.ULGYearIncrease).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val80;
		((UltraGridBase)this.ULGYearIncrease).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGYearIncrease).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val81).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val81, "appearance80");
		((UltraGridBase)this.ULGYearIncrease).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val81;
		((AppearanceBase)val82).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val82, "appearance81");
		((AppearanceBase)val82).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGYearIncrease).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val82;
		((UltraGridBase)this.ULGYearIncrease).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGYearIncrease).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val83).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val83).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val83).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val83).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val83).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val83, "appearance82");
		((UltraGridBase)this.ULGYearIncrease).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val83;
		((UltraGridBase)this.ULGYearIncrease).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGYearIncrease).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val84).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val84).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val84, "appearance83");
		((UltraGridBase)this.ULGYearIncrease).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val84;
		((UltraGridBase)this.ULGYearIncrease).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val85).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val85, "appearance84");
		((UltraGridBase)this.ULGYearIncrease).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val85;
		((System.Windows.Forms.Control)(object)this.ULGYearIncrease).Name = "ULGYearIncrease";
		this.ULGYearIncrease.AfterCellUpdate += new CellEventHandler(ULGYearIncrease_AfterCellUpdate);
		this.ULGYearIncrease.AfterEnterEditMode += new System.EventHandler(ULGYearIncrease_AfterEnterEditMode);
		this.ULGYearIncrease.AfterRowsDeleted += new System.EventHandler(ULGYearIncrease_AfterRowsDeleted);
		this.ULGYearIncrease.CellListSelect += new CellEventHandler(ULGYearIncrease_CellListSelect);
		resources.ApplyResources(this.cboCurrency, "cboCurrency");
		((System.Windows.Forms.Control)(object)this.cboCurrency).Name = "cboCurrency";
		resources.ApplyResources(this.lblCurrency, "lblCurrency");
		((AppearanceBase)val86).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val86).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val86, "appearance247");
		((ControlBase)this.lblCurrency).Appearance = (AppearanceBase)(object)val86;
		this.lblCurrency.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCurrency).Name = "lblCurrency";
		((ControlBase)this.lblCurrency).WrapText = false;
		resources.ApplyResources(this.txtFixedSalaryValue, "txtFixedSalaryValue");
		((System.Windows.Forms.Control)(object)this.txtFixedSalaryValue).Name = "txtFixedSalaryValue";
		((System.Windows.Forms.Control)(object)this.txtFixedSalaryValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.chkIsFixedTaxPercentage, "chkIsFixedTaxPercentage");
		((AppearanceBase)val87).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val87, "appearance248");
		((UltraToggleEditorBase)this.chkIsFixedTaxPercentage).Appearance = (AppearanceBase)(object)val87;
		((System.Windows.Forms.Control)(object)this.chkIsFixedTaxPercentage).Name = "chkIsFixedTaxPercentage";
		((UltraToggleEditorBase)this.chkIsFixedTaxPercentage).CheckedChanged += new System.EventHandler(chkIsFixedSalaryTax_CheckedChanged);
		resources.ApplyResources(this.chkIsFixedSalaryTax, "chkIsFixedSalaryTax");
		((AppearanceBase)val88).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val88, "appearance249");
		((UltraToggleEditorBase)this.chkIsFixedSalaryTax).Appearance = (AppearanceBase)(object)val88;
		((System.Windows.Forms.Control)(object)this.chkIsFixedSalaryTax).Name = "chkIsFixedSalaryTax";
		((UltraToggleEditorBase)this.chkIsFixedSalaryTax).CheckedChanged += new System.EventHandler(chkIsFixedSalaryTax_CheckedChanged);
		resources.ApplyResources(this.ULGDeductions, "ULGDeductions");
		((AppearanceBase)val89).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val89).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val89).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val89).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val89, "appearance88");
		((SpecialBoxBase)((UltraGridBase)this.ULGDeductions).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val89;
		((AppearanceBase)val90).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val90, "appearance89");
		((UltraGridBase)this.ULGDeductions).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val90;
		((SpecialBoxBase)((UltraGridBase)this.ULGDeductions).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val91).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val91).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val91).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val91).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val91, "appearance90");
		((UltraGridBase)this.ULGDeductions).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val91;
		((UltraGridBase)this.ULGDeductions).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDeductions).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val92).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val92).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val92, "appearance91");
		((UltraGridBase)this.ULGDeductions).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val92;
		((AppearanceBase)val93).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val93).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val93, "appearance92");
		((UltraGridBase)this.ULGDeductions).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val93;
		((UltraGridBase)this.ULGDeductions).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGDeductions).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val94).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val94, "appearance93");
		((UltraGridBase)this.ULGDeductions).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val94;
		((AppearanceBase)val95).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val95, "appearance94");
		((AppearanceBase)val95).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGDeductions).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val95;
		((UltraGridBase)this.ULGDeductions).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGDeductions).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val96).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val96).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val96).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val96).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val96).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val96, "appearance95");
		((UltraGridBase)this.ULGDeductions).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val96;
		((UltraGridBase)this.ULGDeductions).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGDeductions).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val97).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val97).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val97, "appearance96");
		((UltraGridBase)this.ULGDeductions).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val97;
		((UltraGridBase)this.ULGDeductions).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val98).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val98, "appearance97");
		((UltraGridBase)this.ULGDeductions).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val98;
		((System.Windows.Forms.Control)(object)this.ULGDeductions).Name = "ULGDeductions";
		this.ULGDeductions.AfterCellUpdate += new CellEventHandler(ULGDeductions_AfterCellUpdate);
		this.ULGDeductions.AfterEnterEditMode += new System.EventHandler(ULGDeductions_AfterEnterEditMode);
		this.ULGDeductions.AfterRowsDeleted += new System.EventHandler(ULGDeductions_AfterRowsDeleted);
		this.ULGDeductions.CellListSelect += new CellEventHandler(ULGDeductions_CellListSelect);
		resources.ApplyResources(this.ULGAllownces, "ULGAllownces");
		((AppearanceBase)val99).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val99).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val99).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val99).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val99, "appearance98");
		((SpecialBoxBase)((UltraGridBase)this.ULGAllownces).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val99;
		((AppearanceBase)val100).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val100, "appearance99");
		((UltraGridBase)this.ULGAllownces).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val100;
		((SpecialBoxBase)((UltraGridBase)this.ULGAllownces).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val101).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val101).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val101).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val101).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val101, "appearance100");
		((UltraGridBase)this.ULGAllownces).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val101;
		((UltraGridBase)this.ULGAllownces).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGAllownces).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val102).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val102).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val102, "appearance101");
		((UltraGridBase)this.ULGAllownces).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val102;
		((AppearanceBase)val103).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val103).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val103, "appearance102");
		((UltraGridBase)this.ULGAllownces).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val103;
		((UltraGridBase)this.ULGAllownces).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGAllownces).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val104).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val104, "appearance103");
		((UltraGridBase)this.ULGAllownces).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val104;
		((AppearanceBase)val105).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val105, "appearance104");
		((AppearanceBase)val105).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGAllownces).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val105;
		((UltraGridBase)this.ULGAllownces).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGAllownces).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val106).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val106).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val106).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val106).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val106).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val106, "appearance105");
		((UltraGridBase)this.ULGAllownces).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val106;
		((UltraGridBase)this.ULGAllownces).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGAllownces).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val107).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val107).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val107, "appearance106");
		((UltraGridBase)this.ULGAllownces).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val107;
		((UltraGridBase)this.ULGAllownces).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val108).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val108, "appearance107");
		((UltraGridBase)this.ULGAllownces).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val108;
		((System.Windows.Forms.Control)(object)this.ULGAllownces).Name = "ULGAllownces";
		this.ULGAllownces.AfterCellUpdate += new CellEventHandler(ULGAllownces_AfterCellUpdate);
		this.ULGAllownces.AfterEnterEditMode += new System.EventHandler(ULGAllownces_AfterEnterEditMode);
		this.ULGAllownces.AfterRowsDeleted += new System.EventHandler(ULGAllownces_AfterRowsDeleted);
		this.ULGAllownces.CellListSelect += new CellEventHandler(ULGAllownces_CellListSelect);
		resources.ApplyResources(this.txtCurrentBasicSalary, "txtCurrentBasicSalary");
		((System.Windows.Forms.Control)(object)this.txtCurrentBasicSalary).Name = "txtCurrentBasicSalary";
		((TextEditorControlBase)this.txtCurrentBasicSalary).ValueChanged += new System.EventHandler(txtCurrentBasicSalary_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtCurrentBasicSalary).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtCurrentVariantSalary, "txtCurrentVariantSalary");
		((System.Windows.Forms.Control)(object)this.txtCurrentVariantSalary).Name = "txtCurrentVariantSalary";
		((TextEditorControlBase)this.txtCurrentVariantSalary).ValueChanged += new System.EventHandler(txtCurrentVariantSalary_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtCurrentVariantSalary).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtNetCurrentSalary, "txtNetCurrentSalary");
		((System.Windows.Forms.Control)(object)this.txtNetCurrentSalary).Name = "txtNetCurrentSalary";
		((System.Windows.Forms.Control)(object)this.txtNetCurrentSalary).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtStartBasicSalary, "txtStartBasicSalary");
		((System.Windows.Forms.Control)(object)this.txtStartBasicSalary).Name = "txtStartBasicSalary";
		((System.Windows.Forms.Control)(object)this.txtStartBasicSalary).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel7, "ultraLabel7");
		((AppearanceBase)val109).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val109).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val109, "appearance250");
		((ControlBase)this.ultraLabel7).Appearance = (AppearanceBase)(object)val109;
		this.ultraLabel7.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel7).Name = "ultraLabel7";
		((ControlBase)this.ultraLabel7).WrapText = false;
		resources.ApplyResources(this.txtNotInsuranceVariantSalary, "txtNotInsuranceVariantSalary");
		((System.Windows.Forms.Control)(object)this.txtNotInsuranceVariantSalary).Name = "txtNotInsuranceVariantSalary";
		((TextEditorControlBase)this.txtNotInsuranceVariantSalary).ValueChanged += new System.EventHandler(txtNotInsuranceVariantSalary_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtNotInsuranceVariantSalary).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt2_KeyPress);
		resources.ApplyResources(this.txtStartVariantSalary, "txtStartVariantSalary");
		((System.Windows.Forms.Control)(object)this.txtStartVariantSalary).Name = "txtStartVariantSalary";
		((System.Windows.Forms.Control)(object)this.txtStartVariantSalary).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		((AppearanceBase)val110).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val110).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val110, "appearance251");
		((ControlBase)this.ultraLabel5).Appearance = (AppearanceBase)(object)val110;
		this.ultraLabel5.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		((AppearanceBase)val111).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val111).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val111, "appearance252");
		((ControlBase)this.ultraLabel6).Appearance = (AppearanceBase)(object)val111;
		this.ultraLabel6.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		((ControlBase)this.ultraLabel6).WrapText = false;
		resources.ApplyResources(this.txtStartSalary, "txtStartSalary");
		((System.Windows.Forms.Control)(object)this.txtStartSalary).Name = "txtStartSalary";
		((System.Windows.Forms.Control)(object)this.txtStartSalary).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel21, "ultraLabel21");
		((AppearanceBase)val112).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val112).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val112, "appearance253");
		((ControlBase)this.ultraLabel21).Appearance = (AppearanceBase)(object)val112;
		this.ultraLabel21.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel21).Name = "ultraLabel21";
		((ControlBase)this.ultraLabel21).WrapText = false;
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		((AppearanceBase)val113).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val113).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val113, "appearance254");
		((ControlBase)this.ultraLabel3).Appearance = (AppearanceBase)(object)val113;
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.lblNetCurrentSalary, "lblNetCurrentSalary");
		((AppearanceBase)val114).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val114).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val114, "appearance255");
		((ControlBase)this.lblNetCurrentSalary).Appearance = (AppearanceBase)(object)val114;
		this.lblNetCurrentSalary.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNetCurrentSalary).Name = "lblNetCurrentSalary";
		((ControlBase)this.lblNetCurrentSalary).WrapText = false;
		resources.ApplyResources(this.lblDeductions, "lblDeductions");
		((AppearanceBase)val115).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val115).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val115, "appearance256");
		((ControlBase)this.lblDeductions).Appearance = (AppearanceBase)(object)val115;
		this.lblDeductions.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDeductions).Name = "lblDeductions";
		((ControlBase)this.lblDeductions).WrapText = false;
		resources.ApplyResources(this.lblStartSalary, "lblStartSalary");
		((AppearanceBase)val116).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val116).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val116, "appearance257");
		((ControlBase)this.lblStartSalary).Appearance = (AppearanceBase)(object)val116;
		this.lblStartSalary.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStartSalary).Name = "lblStartSalary";
		((ControlBase)this.lblStartSalary).WrapText = false;
		resources.ApplyResources(this.ultraLabel14, "ultraLabel14");
		((AppearanceBase)val117).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val117).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val117, "appearance258");
		((ControlBase)this.ultraLabel14).Appearance = (AppearanceBase)(object)val117;
		this.ultraLabel14.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel14).Name = "ultraLabel14";
		((ControlBase)this.ultraLabel14).WrapText = false;
		resources.ApplyResources(this.ultraTabPageControl5, "ultraTabPageControl5");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add(this.groupBox1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.txtInclusiveHealthInsuranceValue);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.lblInclusiveHealthInsuranceValue);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.txtSocialInsuranceValue);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel29);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel26);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel27);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.dtpHealthInsuranceEndDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.dtpHealthInsuranceStartDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.chkWorkOfficeIsSend);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.lblSendDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.lblToDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.lblWorkOfficeFromDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.lblInssuranceDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.lblInsuranceEndDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.lblFirstInssuranceDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.cboHealthInsuranceType);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel8);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.cboSocialInssuranceOffice);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.lblSocialInssuranceOffice);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.txtWorkOfficePermissionCode);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.lblWorkOfficePermissionCode);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.txtSocialInssuranceNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.lblSocialInssuranceNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.dtpWorkOfficeSendDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.dtpWorkOfficeToDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.dtpWorkOfficeFromDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.dtpInssuranceDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.dtpInssuranceEndDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.dtpFirstInssuranceDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Name = "ultraTabPageControl5";
		resources.ApplyResources(this.groupBox1, "groupBox1");
		this.groupBox1.Controls.Add((System.Windows.Forms.Control)(object)this.ULGFamilyRelativesHealthInsurance);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.TabStop = false;
		resources.ApplyResources(this.ULGFamilyRelativesHealthInsurance, "ULGFamilyRelativesHealthInsurance");
		((AppearanceBase)val118).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val118).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val118).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val118).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val118, "appearance117");
		((SpecialBoxBase)((UltraGridBase)this.ULGFamilyRelativesHealthInsurance).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val118;
		((AppearanceBase)val119).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val119, "appearance118");
		((UltraGridBase)this.ULGFamilyRelativesHealthInsurance).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val119;
		((SpecialBoxBase)((UltraGridBase)this.ULGFamilyRelativesHealthInsurance).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val120).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val120).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val120).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val120).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val120, "appearance119");
		((UltraGridBase)this.ULGFamilyRelativesHealthInsurance).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val120;
		((UltraGridBase)this.ULGFamilyRelativesHealthInsurance).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGFamilyRelativesHealthInsurance).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val121).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val121).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val121, "appearance120");
		((UltraGridBase)this.ULGFamilyRelativesHealthInsurance).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val121;
		((AppearanceBase)val122).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val122).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val122, "appearance121");
		((UltraGridBase)this.ULGFamilyRelativesHealthInsurance).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val122;
		((UltraGridBase)this.ULGFamilyRelativesHealthInsurance).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGFamilyRelativesHealthInsurance).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val123).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val123, "appearance122");
		((UltraGridBase)this.ULGFamilyRelativesHealthInsurance).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val123;
		((AppearanceBase)val124).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val124, "appearance123");
		((AppearanceBase)val124).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGFamilyRelativesHealthInsurance).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val124;
		((UltraGridBase)this.ULGFamilyRelativesHealthInsurance).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGFamilyRelativesHealthInsurance).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val125).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val125).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val125).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val125).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val125).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val125, "appearance124");
		((UltraGridBase)this.ULGFamilyRelativesHealthInsurance).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val125;
		((UltraGridBase)this.ULGFamilyRelativesHealthInsurance).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGFamilyRelativesHealthInsurance).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val126).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val126).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val126, "appearance125");
		((UltraGridBase)this.ULGFamilyRelativesHealthInsurance).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val126;
		((UltraGridBase)this.ULGFamilyRelativesHealthInsurance).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val127).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val127, "appearance126");
		((UltraGridBase)this.ULGFamilyRelativesHealthInsurance).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val127;
		((System.Windows.Forms.Control)(object)this.ULGFamilyRelativesHealthInsurance).Name = "ULGFamilyRelativesHealthInsurance";
		this.ULGFamilyRelativesHealthInsurance.AfterEnterEditMode += new System.EventHandler(ULGFamilyRelativesHealthInsurance_AfterEnterEditMode);
		resources.ApplyResources(this.txtInclusiveHealthInsuranceValue, "txtInclusiveHealthInsuranceValue");
		((System.Windows.Forms.Control)(object)this.txtInclusiveHealthInsuranceValue).Name = "txtInclusiveHealthInsuranceValue";
		((System.Windows.Forms.Control)(object)this.txtInclusiveHealthInsuranceValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblInclusiveHealthInsuranceValue, "lblInclusiveHealthInsuranceValue");
		((AppearanceBase)val128).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val128).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val128, "appearance259");
		((ControlBase)this.lblInclusiveHealthInsuranceValue).Appearance = (AppearanceBase)(object)val128;
		this.lblInclusiveHealthInsuranceValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInclusiveHealthInsuranceValue).Name = "lblInclusiveHealthInsuranceValue";
		((ControlBase)this.lblInclusiveHealthInsuranceValue).WrapText = false;
		resources.ApplyResources(this.txtSocialInsuranceValue, "txtSocialInsuranceValue");
		((System.Windows.Forms.Control)(object)this.txtSocialInsuranceValue).Name = "txtSocialInsuranceValue";
		((System.Windows.Forms.Control)(object)this.txtSocialInsuranceValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel29, "ultraLabel29");
		((AppearanceBase)val129).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val129).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val129, "appearance260");
		((ControlBase)this.ultraLabel29).Appearance = (AppearanceBase)(object)val129;
		this.ultraLabel29.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel29).Name = "ultraLabel29";
		((ControlBase)this.ultraLabel29).WrapText = false;
		resources.ApplyResources(this.ultraLabel26, "ultraLabel26");
		((AppearanceBase)val130).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val130).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val130, "appearance261");
		((ControlBase)this.ultraLabel26).Appearance = (AppearanceBase)(object)val130;
		this.ultraLabel26.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel26).Name = "ultraLabel26";
		((ControlBase)this.ultraLabel26).WrapText = false;
		resources.ApplyResources(this.ultraLabel27, "ultraLabel27");
		((AppearanceBase)val131).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val131).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val131, "appearance262");
		((ControlBase)this.ultraLabel27).Appearance = (AppearanceBase)(object)val131;
		this.ultraLabel27.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel27).Name = "ultraLabel27";
		((ControlBase)this.ultraLabel27).WrapText = false;
		resources.ApplyResources(this.dtpHealthInsuranceEndDate, "dtpHealthInsuranceEndDate");
		((UltraWinEditorMaskedControlBase)this.dtpHealthInsuranceEndDate).AlwaysInEditMode = true;
		this.dtpHealthInsuranceEndDate.DateTime = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpHealthInsuranceEndDate).Name = "dtpHealthInsuranceEndDate";
		this.dtpHealthInsuranceEndDate.Value = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		resources.ApplyResources(this.dtpHealthInsuranceStartDate, "dtpHealthInsuranceStartDate");
		((UltraWinEditorMaskedControlBase)this.dtpHealthInsuranceStartDate).AlwaysInEditMode = true;
		this.dtpHealthInsuranceStartDate.DateTime = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpHealthInsuranceStartDate).Name = "dtpHealthInsuranceStartDate";
		this.dtpHealthInsuranceStartDate.Value = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		resources.ApplyResources(this.chkWorkOfficeIsSend, "chkWorkOfficeIsSend");
		((AppearanceBase)val132).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val132, "appearance263");
		((UltraToggleEditorBase)this.chkWorkOfficeIsSend).Appearance = (AppearanceBase)(object)val132;
		((System.Windows.Forms.Control)(object)this.chkWorkOfficeIsSend).Name = "chkWorkOfficeIsSend";
		resources.ApplyResources(this.lblSendDate, "lblSendDate");
		((AppearanceBase)val133).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val133).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val133, "appearance264");
		((ControlBase)this.lblSendDate).Appearance = (AppearanceBase)(object)val133;
		this.lblSendDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSendDate).Name = "lblSendDate";
		((ControlBase)this.lblSendDate).WrapText = false;
		resources.ApplyResources(this.lblToDate, "lblToDate");
		((AppearanceBase)val134).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val134).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val134, "appearance265");
		((ControlBase)this.lblToDate).Appearance = (AppearanceBase)(object)val134;
		this.lblToDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblToDate).Name = "lblToDate";
		((ControlBase)this.lblToDate).WrapText = false;
		resources.ApplyResources(this.lblWorkOfficeFromDate, "lblWorkOfficeFromDate");
		((AppearanceBase)val135).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val135).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val135, "appearance266");
		((ControlBase)this.lblWorkOfficeFromDate).Appearance = (AppearanceBase)(object)val135;
		this.lblWorkOfficeFromDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblWorkOfficeFromDate).Name = "lblWorkOfficeFromDate";
		((ControlBase)this.lblWorkOfficeFromDate).WrapText = false;
		resources.ApplyResources(this.lblInssuranceDate, "lblInssuranceDate");
		((AppearanceBase)val136).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val136).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val136, "appearance267");
		((ControlBase)this.lblInssuranceDate).Appearance = (AppearanceBase)(object)val136;
		this.lblInssuranceDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInssuranceDate).Name = "lblInssuranceDate";
		((ControlBase)this.lblInssuranceDate).WrapText = false;
		resources.ApplyResources(this.lblInsuranceEndDate, "lblInsuranceEndDate");
		((AppearanceBase)val137).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val137).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val137, "appearance268");
		((ControlBase)this.lblInsuranceEndDate).Appearance = (AppearanceBase)(object)val137;
		this.lblInsuranceEndDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInsuranceEndDate).Name = "lblInsuranceEndDate";
		((ControlBase)this.lblInsuranceEndDate).WrapText = false;
		resources.ApplyResources(this.lblFirstInssuranceDate, "lblFirstInssuranceDate");
		((AppearanceBase)val138).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val138).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val138, "appearance269");
		((ControlBase)this.lblFirstInssuranceDate).Appearance = (AppearanceBase)(object)val138;
		this.lblFirstInssuranceDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFirstInssuranceDate).Name = "lblFirstInssuranceDate";
		((ControlBase)this.lblFirstInssuranceDate).WrapText = false;
		resources.ApplyResources(this.cboHealthInsuranceType, "cboHealthInsuranceType");
		((System.Windows.Forms.Control)(object)this.cboHealthInsuranceType).Name = "cboHealthInsuranceType";
		resources.ApplyResources(this.ultraLabel8, "ultraLabel8");
		((AppearanceBase)val139).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val139).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val139, "appearance270");
		((ControlBase)this.ultraLabel8).Appearance = (AppearanceBase)(object)val139;
		this.ultraLabel8.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel8).Name = "ultraLabel8";
		((ControlBase)this.ultraLabel8).WrapText = false;
		resources.ApplyResources(this.cboSocialInssuranceOffice, "cboSocialInssuranceOffice");
		((System.Windows.Forms.Control)(object)this.cboSocialInssuranceOffice).Name = "cboSocialInssuranceOffice";
		resources.ApplyResources(this.lblSocialInssuranceOffice, "lblSocialInssuranceOffice");
		((AppearanceBase)val140).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val140).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val140, "appearance271");
		((ControlBase)this.lblSocialInssuranceOffice).Appearance = (AppearanceBase)(object)val140;
		this.lblSocialInssuranceOffice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSocialInssuranceOffice).Name = "lblSocialInssuranceOffice";
		((ControlBase)this.lblSocialInssuranceOffice).WrapText = false;
		resources.ApplyResources(this.txtWorkOfficePermissionCode, "txtWorkOfficePermissionCode");
		((System.Windows.Forms.Control)(object)this.txtWorkOfficePermissionCode).Name = "txtWorkOfficePermissionCode";
		resources.ApplyResources(this.lblWorkOfficePermissionCode, "lblWorkOfficePermissionCode");
		((AppearanceBase)val141).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val141).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val141, "appearance272");
		((ControlBase)this.lblWorkOfficePermissionCode).Appearance = (AppearanceBase)(object)val141;
		this.lblWorkOfficePermissionCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblWorkOfficePermissionCode).Name = "lblWorkOfficePermissionCode";
		((ControlBase)this.lblWorkOfficePermissionCode).WrapText = false;
		resources.ApplyResources(this.txtSocialInssuranceNo, "txtSocialInssuranceNo");
		((System.Windows.Forms.Control)(object)this.txtSocialInssuranceNo).Name = "txtSocialInssuranceNo";
		resources.ApplyResources(this.lblSocialInssuranceNo, "lblSocialInssuranceNo");
		((AppearanceBase)val142).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val142).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val142, "appearance273");
		((ControlBase)this.lblSocialInssuranceNo).Appearance = (AppearanceBase)(object)val142;
		this.lblSocialInssuranceNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSocialInssuranceNo).Name = "lblSocialInssuranceNo";
		((ControlBase)this.lblSocialInssuranceNo).WrapText = false;
		resources.ApplyResources(this.dtpWorkOfficeSendDate, "dtpWorkOfficeSendDate");
		((UltraWinEditorMaskedControlBase)this.dtpWorkOfficeSendDate).AlwaysInEditMode = true;
		this.dtpWorkOfficeSendDate.DateTime = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpWorkOfficeSendDate).Name = "dtpWorkOfficeSendDate";
		this.dtpWorkOfficeSendDate.Value = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		resources.ApplyResources(this.dtpWorkOfficeToDate, "dtpWorkOfficeToDate");
		((UltraWinEditorMaskedControlBase)this.dtpWorkOfficeToDate).AlwaysInEditMode = true;
		this.dtpWorkOfficeToDate.DateTime = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpWorkOfficeToDate).Name = "dtpWorkOfficeToDate";
		this.dtpWorkOfficeToDate.Value = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		resources.ApplyResources(this.dtpWorkOfficeFromDate, "dtpWorkOfficeFromDate");
		((UltraWinEditorMaskedControlBase)this.dtpWorkOfficeFromDate).AlwaysInEditMode = true;
		this.dtpWorkOfficeFromDate.DateTime = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpWorkOfficeFromDate).Name = "dtpWorkOfficeFromDate";
		this.dtpWorkOfficeFromDate.Value = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		resources.ApplyResources(this.dtpInssuranceDate, "dtpInssuranceDate");
		((UltraWinEditorMaskedControlBase)this.dtpInssuranceDate).AlwaysInEditMode = true;
		this.dtpInssuranceDate.DateTime = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpInssuranceDate).Name = "dtpInssuranceDate";
		this.dtpInssuranceDate.Value = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		resources.ApplyResources(this.dtpInssuranceEndDate, "dtpInssuranceEndDate");
		((UltraWinEditorMaskedControlBase)this.dtpInssuranceEndDate).AlwaysInEditMode = true;
		this.dtpInssuranceEndDate.DateTime = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpInssuranceEndDate).Name = "dtpInssuranceEndDate";
		this.dtpInssuranceEndDate.Value = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		resources.ApplyResources(this.dtpFirstInssuranceDate, "dtpFirstInssuranceDate");
		((UltraWinEditorMaskedControlBase)this.dtpFirstInssuranceDate).AlwaysInEditMode = true;
		this.dtpFirstInssuranceDate.DateTime = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpFirstInssuranceDate).Name = "dtpFirstInssuranceDate";
		this.dtpFirstInssuranceDate.Value = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDocuments);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.ULGDocuments, "ULGDocuments");
		((AppearanceBase)val143).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val143).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val143).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val143).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val143, "appearance142");
		((SpecialBoxBase)((UltraGridBase)this.ULGDocuments).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val143;
		((AppearanceBase)val144).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val144, "appearance143");
		((UltraGridBase)this.ULGDocuments).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val144;
		((SpecialBoxBase)((UltraGridBase)this.ULGDocuments).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val145).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val145).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val145).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val145).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val145, "appearance144");
		((UltraGridBase)this.ULGDocuments).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val145;
		((UltraGridBase)this.ULGDocuments).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDocuments).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val146).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val146).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val146, "appearance145");
		((UltraGridBase)this.ULGDocuments).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val146;
		((AppearanceBase)val147).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val147).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val147, "appearance146");
		((UltraGridBase)this.ULGDocuments).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val147;
		((UltraGridBase)this.ULGDocuments).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGDocuments).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val148).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val148, "appearance147");
		((UltraGridBase)this.ULGDocuments).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val148;
		((AppearanceBase)val149).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val149, "appearance148");
		((AppearanceBase)val149).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGDocuments).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val149;
		((UltraGridBase)this.ULGDocuments).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGDocuments).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val150).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val150).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val150).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val150).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val150).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val150, "appearance149");
		((UltraGridBase)this.ULGDocuments).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val150;
		((UltraGridBase)this.ULGDocuments).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGDocuments).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val151).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val151).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val151, "appearance150");
		((UltraGridBase)this.ULGDocuments).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val151;
		((UltraGridBase)this.ULGDocuments).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val152).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val152, "appearance151");
		((UltraGridBase)this.ULGDocuments).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val152;
		((System.Windows.Forms.Control)(object)this.ULGDocuments).Name = "ULGDocuments";
		this.ULGDocuments.AfterEnterEditMode += new System.EventHandler(ULGDocuments_AfterEnterEditMode);
		resources.ApplyResources(this.ultraTabPageControl1, "ultraTabPageControl1");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.lblQualificationYear);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.cboQualificationYear);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.cboSectionName);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.cboUniversity);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.lblSectionName);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.cboQualification);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.lblUniversity);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.txtSectionNameNotes);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.lblQualification);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.lblSectionNameNotes);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.ULGCetificates);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Name = "ultraTabPageControl1";
		resources.ApplyResources(this.lblQualificationYear, "lblQualificationYear");
		((AppearanceBase)val153).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val153).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val153, "appearance274");
		((ControlBase)this.lblQualificationYear).Appearance = (AppearanceBase)(object)val153;
		this.lblQualificationYear.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblQualificationYear).Name = "lblQualificationYear";
		((ControlBase)this.lblQualificationYear).WrapText = false;
		resources.ApplyResources(this.cboQualificationYear, "cboQualificationYear");
		((System.Windows.Forms.Control)(object)this.cboQualificationYear).Name = "cboQualificationYear";
		resources.ApplyResources(this.cboSectionName, "cboSectionName");
		((System.Windows.Forms.Control)(object)this.cboSectionName).Name = "cboSectionName";
		resources.ApplyResources(this.cboUniversity, "cboUniversity");
		((System.Windows.Forms.Control)(object)this.cboUniversity).Name = "cboUniversity";
		resources.ApplyResources(this.lblSectionName, "lblSectionName");
		((AppearanceBase)val154).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val154).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val154, "appearance275");
		((ControlBase)this.lblSectionName).Appearance = (AppearanceBase)(object)val154;
		this.lblSectionName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSectionName).Name = "lblSectionName";
		((ControlBase)this.lblSectionName).WrapText = false;
		resources.ApplyResources(this.cboQualification, "cboQualification");
		((System.Windows.Forms.Control)(object)this.cboQualification).Name = "cboQualification";
		((TextEditorControlBase)this.cboQualification).ValueChanged += new System.EventHandler(cboQualification_ValueChanged);
		resources.ApplyResources(this.lblUniversity, "lblUniversity");
		((AppearanceBase)val155).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val155).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val155, "appearance276");
		((ControlBase)this.lblUniversity).Appearance = (AppearanceBase)(object)val155;
		this.lblUniversity.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUniversity).Name = "lblUniversity";
		((ControlBase)this.lblUniversity).WrapText = false;
		resources.ApplyResources(this.txtSectionNameNotes, "txtSectionNameNotes");
		((System.Windows.Forms.Control)(object)this.txtSectionNameNotes).Name = "txtSectionNameNotes";
		resources.ApplyResources(this.lblQualification, "lblQualification");
		((AppearanceBase)val156).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val156).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val156, "appearance277");
		((ControlBase)this.lblQualification).Appearance = (AppearanceBase)(object)val156;
		this.lblQualification.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblQualification).Name = "lblQualification";
		((ControlBase)this.lblQualification).WrapText = false;
		resources.ApplyResources(this.lblSectionNameNotes, "lblSectionNameNotes");
		((AppearanceBase)val157).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val157).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val157, "appearance278");
		((ControlBase)this.lblSectionNameNotes).Appearance = (AppearanceBase)(object)val157;
		this.lblSectionNameNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSectionNameNotes).Name = "lblSectionNameNotes";
		((ControlBase)this.lblSectionNameNotes).WrapText = false;
		resources.ApplyResources(this.ULGCetificates, "ULGCetificates");
		((AppearanceBase)val158).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val158).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val158).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val158).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val158, "appearance157");
		((SpecialBoxBase)((UltraGridBase)this.ULGCetificates).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val158;
		((AppearanceBase)val159).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val159, "appearance158");
		((UltraGridBase)this.ULGCetificates).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val159;
		((SpecialBoxBase)((UltraGridBase)this.ULGCetificates).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val160).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val160).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val160).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val160).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val160, "appearance159");
		((UltraGridBase)this.ULGCetificates).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val160;
		((UltraGridBase)this.ULGCetificates).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGCetificates).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val161).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val161).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val161, "appearance160");
		((UltraGridBase)this.ULGCetificates).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val161;
		((AppearanceBase)val162).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val162).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val162, "appearance161");
		((UltraGridBase)this.ULGCetificates).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val162;
		((UltraGridBase)this.ULGCetificates).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGCetificates).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val163).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val163, "appearance162");
		((UltraGridBase)this.ULGCetificates).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val163;
		((AppearanceBase)val164).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val164, "appearance163");
		((AppearanceBase)val164).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGCetificates).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val164;
		((UltraGridBase)this.ULGCetificates).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGCetificates).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val165).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val165).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val165).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val165).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val165).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val165, "appearance164");
		((UltraGridBase)this.ULGCetificates).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val165;
		((UltraGridBase)this.ULGCetificates).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGCetificates).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val166).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val166).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val166, "appearance165");
		((UltraGridBase)this.ULGCetificates).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val166;
		((UltraGridBase)this.ULGCetificates).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val167).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val167, "appearance166");
		((UltraGridBase)this.ULGCetificates).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val167;
		((System.Windows.Forms.Control)(object)this.ULGCetificates).Name = "ULGCetificates";
		this.ULGCetificates.AfterEnterEditMode += new System.EventHandler(ULGCetificates_AfterEnterEditMode);
		resources.ApplyResources(this.tabRecipe, "tabRecipe");
		((System.Windows.Forms.Control)(object)this.tabRecipe).Controls.Add((System.Windows.Forms.Control)(object)this.txtEMail);
		((System.Windows.Forms.Control)(object)this.tabRecipe).Controls.Add((System.Windows.Forms.Control)(object)this.lblEMail);
		((System.Windows.Forms.Control)(object)this.tabRecipe).Controls.Add((System.Windows.Forms.Control)(object)this.ULGContacts);
		((System.Windows.Forms.Control)(object)this.tabRecipe).Name = "tabRecipe";
		resources.ApplyResources(this.txtEMail, "txtEMail");
		((System.Windows.Forms.Control)(object)this.txtEMail).Name = "txtEMail";
		resources.ApplyResources(this.lblEMail, "lblEMail");
		((AppearanceBase)val168).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val168).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val168, "appearance279");
		((ControlBase)this.lblEMail).Appearance = (AppearanceBase)(object)val168;
		this.lblEMail.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEMail).Name = "lblEMail";
		((ControlBase)this.lblEMail).WrapText = false;
		resources.ApplyResources(this.ULGContacts, "ULGContacts");
		((AppearanceBase)val169).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val169).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val169).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val169).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val169, "appearance168");
		((SpecialBoxBase)((UltraGridBase)this.ULGContacts).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val169;
		((AppearanceBase)val170).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val170, "appearance169");
		((UltraGridBase)this.ULGContacts).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val170;
		((SpecialBoxBase)((UltraGridBase)this.ULGContacts).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val171).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val171).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val171).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val171).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val171, "appearance170");
		((UltraGridBase)this.ULGContacts).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val171;
		((UltraGridBase)this.ULGContacts).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGContacts).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val172).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val172).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val172, "appearance171");
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val172;
		((AppearanceBase)val173).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val173).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val173, "appearance172");
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val173;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val174).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val174, "appearance173");
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val174;
		((AppearanceBase)val175).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val175, "appearance174");
		((AppearanceBase)val175).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val175;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val176).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val176).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val176).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val176).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val176).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val176, "appearance175");
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val176;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val177).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val177).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val177, "appearance176");
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val177;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val178).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val178, "appearance177");
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val178;
		((System.Windows.Forms.Control)(object)this.ULGContacts).Name = "ULGContacts";
		this.ULGContacts.AfterEnterEditMode += new System.EventHandler(ULGContacts_AfterEnterEditMode);
		resources.ApplyResources(this.tabService, "tabService");
		((System.Windows.Forms.Control)(object)this.tabService).Controls.Add((System.Windows.Forms.Control)(object)this.TreeAccounts);
		((System.Windows.Forms.Control)(object)this.tabService).Controls.Add((System.Windows.Forms.Control)(object)this.lblAccounts);
		((System.Windows.Forms.Control)(object)this.tabService).Controls.Add((System.Windows.Forms.Control)(object)this.txtItems);
		((System.Windows.Forms.Control)(object)this.tabService).Controls.Add((System.Windows.Forms.Control)(object)this.btnItemsSearch);
		((System.Windows.Forms.Control)(object)this.tabService).Name = "tabService";
		resources.ApplyResources(this.TreeAccounts, "TreeAccounts");
		((System.Windows.Forms.Control)(object)this.TreeAccounts).Name = "TreeAccounts";
		val179.NodeStyle = (NodeStyle)1;
		this.TreeAccounts.Override = val179;
		this.TreeAccounts.AfterCheck += new AfterNodeChangedEventHandler(Tree_AfterCheck);
		resources.ApplyResources(this.lblAccounts, "lblAccounts");
		((System.Windows.Forms.Control)(object)this.lblAccounts).Name = "lblAccounts";
		resources.ApplyResources(this.txtItems, "txtItems");
		((System.Windows.Forms.Control)(object)this.txtItems).Name = "txtItems";
		((TextEditorControlBase)this.txtItems).ValueChanged += new System.EventHandler(txtItems_ValueChanged);
		resources.ApplyResources(this.btnItemsSearch, "btnItemsSearch");
		((AppearanceBase)val180).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val180, "appearance280");
		((ControlBase)this.btnItemsSearch).Appearance = (AppearanceBase)(object)val180;
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Name = "btnItemsSearch";
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Click += new System.EventHandler(btnItemsSearch_Click);
		resources.ApplyResources(this.cboState, "cboState");
		((System.Windows.Forms.Control)(object)this.cboState).Name = "cboState";
		resources.ApplyResources(this.lblState, "lblState");
		((AppearanceBase)val181).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val181).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val181, "appearance281");
		((ControlBase)this.lblState).Appearance = (AppearanceBase)(object)val181;
		this.lblState.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblState).Name = "lblState";
		resources.ApplyResources(this.tabItemType, "tabItemType");
		((AppearanceBase)val182).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val182, "appearance180");
		((AppearanceBase)val182).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.tabItemType).Appearance = (AppearanceBase)(object)val182;
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.tabItem);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.tabService);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.tabRecipe);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl1);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl4);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl5);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl6);
		((System.Windows.Forms.Control)(object)this.tabItemType).Name = "tabItemType";
		((UltraTabControlBase)this.tabItemType).SharedControlsPage = this.ultraTabSharedControlsPage1;
		resources.ApplyResources(val183, "appearance181");
		((UltraTabControlBase)this.tabItemType).TabHeaderAreaAppearance = (AppearanceBase)(object)val183;
		resources.ApplyResources(val184, "appearance182");
		((UltraTabControlBase)this.tabItemType).TabListButtonAppearance = (AppearanceBase)(object)val184;
		((UltraTabControlBase)this.tabItemType).TabOrientation = (TabOrientation)2;
		((KeyedSubObjectBase)val185).Key = "PersonalData";
		val185.TabPage = this.tabItem;
		resources.ApplyResources(val185, "ultraTab1");
		((SubObjectBase)val185).ForceApplyResources = "";
		((KeyedSubObjectBase)val186).Key = "JobData";
		val186.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val186, "ultraTab2");
		((SubObjectBase)val186).ForceApplyResources = "";
		((KeyedSubObjectBase)val187).Key = "Setting";
		val187.TabPage = this.ultraTabPageControl6;
		resources.ApplyResources(val187, "ultraTab3");
		((SubObjectBase)val187).ForceApplyResources = "";
		((KeyedSubObjectBase)val188).Key = "Salary";
		val188.TabPage = this.ultraTabPageControl4;
		resources.ApplyResources(val188, "ultraTab4");
		((SubObjectBase)val188).ForceApplyResources = "";
		((KeyedSubObjectBase)val189).Key = "Inssurance";
		val189.TabPage = this.ultraTabPageControl5;
		resources.ApplyResources(val189, "ultraTab5");
		((SubObjectBase)val189).ForceApplyResources = "";
		((KeyedSubObjectBase)val190).Key = "Documents";
		val190.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val190, "ultraTab6");
		((SubObjectBase)val190).ForceApplyResources = "";
		((KeyedSubObjectBase)val191).Key = "Education";
		val191.TabPage = this.ultraTabPageControl1;
		resources.ApplyResources(val191, "ultraTab7");
		((SubObjectBase)val191).ForceApplyResources = "";
		((KeyedSubObjectBase)val192).Key = "Contacts";
		val192.TabPage = this.tabRecipe;
		resources.ApplyResources(val192, "ultraTab8");
		((SubObjectBase)val192).ForceApplyResources = "";
		((KeyedSubObjectBase)val193).Key = "Accounts";
		val193.TabPage = this.tabService;
		resources.ApplyResources(val193, "ultraTab9");
		((SubObjectBase)val193).ForceApplyResources = "";
		((UltraTabControlBase)this.tabItemType).Tabs.AddRange((UltraTab[])(object)new UltraTab[9] { val185, val186, val187, val188, val189, val190, val191, val192, val193 });
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		this.ofdItemPic.FileName = "openFileDialog1";
		resources.ApplyResources(this.ofdItemPic, "ofdItemPic");
		resources.ApplyResources(this.txtOrder, "txtOrder");
		((System.Windows.Forms.Control)(object)this.txtOrder).Name = "txtOrder";
		resources.ApplyResources(this.ultraLabel18, "ultraLabel18");
		((AppearanceBase)val194).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val194).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val194, "appearance282");
		((ControlBase)this.ultraLabel18).Appearance = (AppearanceBase)(object)val194;
		this.ultraLabel18.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel18).Name = "ultraLabel18";
		resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
		this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(18, 18);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.changeParentTSMenu, this.setAsGroupToolStripMenuItem, this.setAsSubAccountToolStripMenuItem, this.modifyGroupEmployeesToolStripMenuItem });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.ShowImageMargin = false;
		resources.ApplyResources(this.changeParentTSMenu, "changeParentTSMenu");
		this.changeParentTSMenu.Name = "changeParentTSMenu";
		this.changeParentTSMenu.Click += new System.EventHandler(changeParentTSMenu_Click);
		resources.ApplyResources(this.setAsGroupToolStripMenuItem, "setAsGroupToolStripMenuItem");
		this.setAsGroupToolStripMenuItem.Name = "setAsGroupToolStripMenuItem";
		this.setAsGroupToolStripMenuItem.Click += new System.EventHandler(setAsGroupToolStripMenuItem_Click);
		resources.ApplyResources(this.setAsSubAccountToolStripMenuItem, "setAsSubAccountToolStripMenuItem");
		this.setAsSubAccountToolStripMenuItem.Name = "setAsSubAccountToolStripMenuItem";
		this.setAsSubAccountToolStripMenuItem.Click += new System.EventHandler(setAsSubAccountToolStripMenuItem_Click);
		resources.ApplyResources(this.modifyGroupEmployeesToolStripMenuItem, "modifyGroupEmployeesToolStripMenuItem");
		this.modifyGroupEmployeesToolStripMenuItem.Name = "modifyGroupEmployeesToolStripMenuItem";
		this.modifyGroupEmployeesToolStripMenuItem.Click += new System.EventHandler(modifyGroupEmployeesToolStripMenuItem_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.tabItemType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboState);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblState);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtOrder);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel18);
		base.Name = "frmEmployeeTree";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnImport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnExport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel18, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtOrder, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblState, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboState, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.tabItemType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.treeChart, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.label1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.label2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.label3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAddRoot, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		((System.ComponentModel.ISupportInitialize)base.dtChart).EndInit();
		((System.ComponentModel.ISupportInitialize)base.treeChart).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtName).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.tabItem).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.tabItem).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dtpPostponedToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkHasPrivateCar).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkHasPassport).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEmployeeNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassportNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDrivingLicenseID).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPersonalIDNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDrivingLicenseExpireDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpPassportExpireDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpIDExpireDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpIDIssueDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBirthDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSocialStatus).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboMilitaryService).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboNationality).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboGender).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cboSalesManDefaultStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLeavingWorkNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLeavingWorkReason).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsSalesMan).EndInit();
		((System.ComponentModel.ISupportInitialize)this.treeAdministrativeStructure).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpContractToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpContractFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpEndDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpHireDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDirectManager).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDegree).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPosition).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAdministrativeLevel).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.chkSalaryAtEndOFMonth).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpServicePercentFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInServicePercent).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInSalaryTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsMonthlySalary).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCollectionCommission).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalesCommission).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalaryList).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLeavePermissionRule).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVacationRule).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalaryAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPenalityRule).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPenaltyAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboFeedingRule).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAbsenceRule).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAdvanceAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLateRule).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboExtraTimeRule).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBank).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBankAccountNo).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGMotivation).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGYearIncrease).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFixedSalaryValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsFixedTaxPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsFixedSalaryTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGDeductions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGAllownces).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCurrentBasicSalary).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCurrentVariantSalary).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetCurrentSalary).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtStartBasicSalary).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotInsuranceVariantSalary).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtStartVariantSalary).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtStartSalary).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).PerformLayout();
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGFamilyRelativesHealthInsurance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInclusiveHealthInsuranceValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSocialInsuranceValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpHealthInsuranceEndDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpHealthInsuranceStartDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkWorkOfficeIsSend).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboHealthInsuranceType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSocialInssuranceOffice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtWorkOfficePermissionCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSocialInssuranceNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpWorkOfficeSendDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpWorkOfficeToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpWorkOfficeFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpInssuranceDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpInssuranceEndDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFirstInssuranceDate).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDocuments).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cboQualificationYear).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSectionName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboUniversity).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboQualification).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSectionNameNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGCetificates).EndInit();
		((System.Windows.Forms.Control)(object)this.tabRecipe).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.tabRecipe).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGContacts).EndInit();
		((System.Windows.Forms.Control)(object)this.tabService).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.tabService).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TreeAccounts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboState).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tabItemType).EndInit();
		((System.Windows.Forms.Control)(object)this.tabItemType).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtOrder).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
