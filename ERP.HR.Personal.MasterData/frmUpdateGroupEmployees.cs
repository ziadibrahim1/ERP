using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.HR;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.HR.Personal.MasterData;

public class frmUpdateGroupEmployees : frmBase
{
	private DataTable dtBranch;

	private DataTable dtGroups;

	private DataTable dtSalaryLists;

	private DataTable dtExtraTimeRule;

	private DataTable dtLateRule;

	private DataTable dtAbsentRule;

	private DataTable dtPenalityRule;

	private DataTable dtVacationRule;

	private DataTable dtFeedingRule;

	private DataTable dtLeaveRule;

	private DataTable dtSubAccDetails;

	private string GroupID;

	private DataRow drGroup;

	private IContainer components = null;

	public UltraLabel lblRoot;

	public UltraComboEditor cboGroup;

	public UltraButton btnSaveAndClose;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraButton btnCancel;

	public UltraButton btnSave;

	public UltraLabel lblTitle2;

	private UltraCheckEditor chkModRules;

	private UltraPanel pnlAccounts;

	private UltraCheckEditor chkModIsMonthlySalary;

	private UltraCheckEditor chkModInSalaryTax;

	private UltraCheckEditor chkModSalaryAtEndOFMonth;

	private UltraPanel pnlPOS;

	private UltraCheckEditor chkModAccounts;

	private UltraCheckEditor chkModSalaryList;

	private UltraCheckEditor chkIsMonthlySalary;

	private UltraCheckEditor chkSalaryAtEndOFMonth;

	private UltraCheckEditor chkInSalaryTax;

	private UltraComboEditor cboSalaryList;

	private UltraLabel lblSalaryList;

	private UltraComboEditor cboSalaryAccount;

	private UltraLabel ultraLabel17;

	private UltraComboEditor cboPenaltyAccount;

	private UltraLabel ultraLabel16;

	private UltraComboEditor cboAdvanceAccount;

	private UltraLabel ultraLabel15;

	private UltraComboEditor cboLeavePermissionRule;

	private UltraLabel lblLeaveRule;

	private UltraComboEditor cboVacationRule;

	private UltraLabel ultraLabel13;

	private UltraComboEditor cboPenalityRule;

	private UltraLabel ultraLabel12;

	private UltraComboEditor cboFeedingRule;

	private UltraComboEditor cboAbsenceRule;

	private UltraLabel lblFeedingRule;

	private UltraLabel ultraLabel11;

	private UltraComboEditor cboLateRule;

	private UltraLabel ultraLabel10;

	private UltraComboEditor cboExtraTimeRule;

	private UltraLabel ultraLabel9;

	private UltraCheckEditor chkModBranch;

	private UltraComboEditor cboBranch;

	private UltraLabel lblBranch;

	public frmUpdateGroupEmployees(string groupID)
	{
		GroupID = groupID;
		InitializeComponent();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtGroups = SubAccounts.GroupsFillComboBySubAccountTypeIDs(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboGroup, dtGroups, "SubAccountID", "SubAccountName");
		dtSalaryLists = SalaryLists.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSalaryList, dtSalaryLists, "SalaryListID", "SalaryListName");
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
		dtLeaveRule = LeavePermissionRule.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboLeavePermissionRule, dtLeaveRule, "LeavePermissionRuleID", "LeavePermissionRuleName");
		dtBranch = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboBranch, dtBranch, "BranchID", "BranchName");
		dtSubAccDetails = SubAccounts_Details.SelectBySubAccountIDWithAccountName(GroupID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSalaryAccount, dtSubAccDetails, "AccountID", "AccountName");
		GlobalFunctions.FillCombo(cboAdvanceAccount, dtSubAccDetails, "AccountID", "AccountName");
		GlobalFunctions.FillCombo(cboPenaltyAccount, dtSubAccDetails, "AccountID", "AccountName");
		DisplayData();
		((Control)(object)btnSave).Enabled = true;
		((Control)(object)btnSaveAndClose).Enabled = true;
		((Control)(object)btnCancel).Enabled = true;
	}

	public void DisplayData()
	{
		drGroup = Employees.SelectBySubAccountID(GroupID, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true).Rows[0];
		((TextEditorControlBase)cboGroup).Value = GroupID;
		((TextEditorControlBase)cboAbsenceRule).Value = drGroup["AbsentRuleID"];
		((TextEditorControlBase)cboAdvanceAccount).Value = drGroup["AdvanceAccountID"];
		((TextEditorControlBase)cboExtraTimeRule).Value = drGroup["ExtraTimeRuleID"];
		((TextEditorControlBase)cboFeedingRule).Value = drGroup["FeedingRuleID"];
		((TextEditorControlBase)cboLateRule).Value = drGroup["DelayRuleID"];
		((TextEditorControlBase)cboLeavePermissionRule).Value = drGroup["LeavePermissionRuleID"];
		((TextEditorControlBase)cboPenalityRule).Value = drGroup["PenaltyRuleID"];
		((TextEditorControlBase)cboPenaltyAccount).Value = drGroup["PenaltyAccountID"];
		((TextEditorControlBase)cboSalaryAccount).Value = drGroup["SalaryAccountID"];
		((TextEditorControlBase)cboSalaryList).Value = drGroup["SalaryListID"];
		((TextEditorControlBase)cboVacationRule).Value = drGroup["VacationRuleID"];
		((UltraToggleEditorBase)chkInSalaryTax).Checked = Convert.ToBoolean(drGroup["InSalaryTax"]);
		((UltraToggleEditorBase)chkIsMonthlySalary).Checked = Convert.ToBoolean(drGroup["IsMonthlySalary"]);
		((UltraToggleEditorBase)chkSalaryAtEndOFMonth).Checked = Convert.ToBoolean(drGroup["IsSalaryAtEndOfMonth"]);
	}

	public bool ValidateData()
	{
		if (((UltraToggleEditorBase)chkModAccounts).Checked)
		{
			if (cboAdvanceAccount.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("من فضلك ادخل حساب السلف ", "Enter Employee Advance Account");
				((TextEditorControlBase)cboAdvanceAccount).Focus();
				cboAdvanceAccount.DropDown();
				return false;
			}
			if (cboPenaltyAccount.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("من فضلك ادخل حساب الجزاءات ", "Enter Employee Penalty Account");
				((TextEditorControlBase)cboPenaltyAccount).Focus();
				cboPenaltyAccount.DropDown();
				return false;
			}
		}
		if (((UltraToggleEditorBase)chkModRules).Checked)
		{
			if (cboAbsenceRule.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("من فضلك ادخل لائحة الغياب ", "Enter Employee Absence Rule");
				((TextEditorControlBase)cboAbsenceRule).Focus();
				cboAbsenceRule.DropDown();
				return false;
			}
			if (cboExtraTimeRule.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("من فضلك ادخل لائحة الإضافي ", "Enter Employee OverTime Rule");
				((TextEditorControlBase)cboExtraTimeRule).Focus();
				cboExtraTimeRule.DropDown();
				return false;
			}
			if (cboLateRule.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("من فضلك ادخل لائحة التأخير ", "Enter Employee Late Rule");
				((TextEditorControlBase)cboLateRule).Focus();
				cboLateRule.DropDown();
				return false;
			}
			if (cboLeavePermissionRule.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("من فضلك ادخل لائحة الاذونات ", "Enter Employee Leave Permission Rule");
				((TextEditorControlBase)cboLeavePermissionRule).Focus();
				cboLeavePermissionRule.DropDown();
				return false;
			}
			if (cboPenalityRule.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("من فضلك ادخل لائحة الجزاءات ", "Enter Employee Penality Rule");
				((TextEditorControlBase)cboPenalityRule).Focus();
				cboPenalityRule.DropDown();
				return false;
			}
			if (cboVacationRule.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("من فضلك ادخل لائحة الإجازات ", "Enter Employee Vacation Rule");
				((TextEditorControlBase)cboVacationRule).Focus();
				cboVacationRule.DropDown();
				return false;
			}
		}
		return true;
	}

	public void Save()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			if (((UltraToggleEditorBase)chkModAccounts).Checked && SubAccounts_Details.CheckAccounts(((TextEditorControlBase)cboGroup).Value.ToString(), (cboSalaryAccount.SelectedIndex == -1) ? "-1" : ((TextEditorControlBase)cboSalaryAccount).Value.ToString(), (cboAdvanceAccount.SelectedIndex == -1) ? "-1" : ((TextEditorControlBase)cboAdvanceAccount).Value.ToString(), (cboPenaltyAccount.SelectedIndex == -1) ? "-1" : ((TextEditorControlBase)cboPenaltyAccount).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true))
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "أحد الحسابات غير موجوده لاحد الموظفين" : "One Of These Selected Accounts Does Not Exists For One Of The Group Employees");
				Main.EndBulkTrans(FromServer: true);
			}
			else
			{
				Employees.UpdateGroupEmployees(((TextEditorControlBase)cboGroup).Value.ToString(), (!((UltraToggleEditorBase)chkModIsMonthlySalary).Checked) ? "-1" : (((UltraToggleEditorBase)chkIsMonthlySalary).Checked ? "1" : "0"), (!((UltraToggleEditorBase)chkModInSalaryTax).Checked) ? "-1" : (((UltraToggleEditorBase)chkInSalaryTax).Checked ? "1" : "0"), (!((UltraToggleEditorBase)chkModSalaryAtEndOFMonth).Checked) ? "-1" : (((UltraToggleEditorBase)chkSalaryAtEndOFMonth).Checked ? "1" : "0"), (!((UltraToggleEditorBase)chkModRules).Checked) ? "-1" : ((cboExtraTimeRule.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboExtraTimeRule).Value.ToString()), (!((UltraToggleEditorBase)chkModRules).Checked) ? "-1" : ((cboLateRule.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLateRule).Value.ToString()), (!((UltraToggleEditorBase)chkModRules).Checked) ? "-1" : ((cboAbsenceRule.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAbsenceRule).Value.ToString()), (!((UltraToggleEditorBase)chkModRules).Checked) ? "-1" : ((cboPenalityRule.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPenalityRule).Value.ToString()), (!((UltraToggleEditorBase)chkModRules).Checked) ? "-1" : ((cboVacationRule.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboVacationRule).Value.ToString()), (!((UltraToggleEditorBase)chkModRules).Checked) ? "-1" : ((cboLeavePermissionRule.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLeavePermissionRule).Value.ToString()), (!((UltraToggleEditorBase)chkModRules).Checked) ? "-1" : ((cboFeedingRule.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboFeedingRule).Value.ToString()), (!((UltraToggleEditorBase)chkModSalaryList).Checked) ? "-1" : ((cboSalaryList.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalaryList).Value.ToString()), (!((UltraToggleEditorBase)chkModAccounts).Checked) ? "-1" : ((cboSalaryAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalaryAccount).Value.ToString()), (!((UltraToggleEditorBase)chkModAccounts).Checked) ? "-1" : ((cboAdvanceAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAdvanceAccount).Value.ToString()), (!((UltraToggleEditorBase)chkModAccounts).Checked) ? "-1" : ((cboPenaltyAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPenaltyAccount).Value.ToString()), (!((UltraToggleEditorBase)chkModBranch).Checked) ? "-1" : ((cboBranch.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBranch).Value.ToString()), GlobalVariables.UserID, IsFromServer: true);
				Main.EndBulkTrans(FromServer: true);
				GlobalVariables.InformationMB.Show("تم الحفظ بنجاح", "Saved Successfully.");
			}
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (ValidateData())
		{
			Save();
		}
	}

	private void btnSaveAndClose_Click(object sender, EventArgs e)
	{
		if (ValidateData())
		{
			Save();
			Dispose();
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	private void chkModAccounts_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)pnlAccounts).Enabled = ((UltraToggleEditorBase)chkModRules).Checked;
	}

	private void chkModPOS_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)pnlPOS).Enabled = ((UltraToggleEditorBase)chkModAccounts).Checked;
	}

	private void chkModIsMonthlySalary_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)chkIsMonthlySalary).Enabled = ((UltraToggleEditorBase)chkModIsMonthlySalary).Checked;
	}

	private void chkModSalaryAtEndOFMonth_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)chkSalaryAtEndOFMonth).Enabled = ((UltraToggleEditorBase)chkModSalaryAtEndOFMonth).Checked;
	}

	private void chkModInSalaryTax_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)chkInSalaryTax).Enabled = ((UltraToggleEditorBase)chkModInSalaryTax).Checked;
	}

	private void chkModSalaryList_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboSalaryList).Enabled = ((UltraToggleEditorBase)chkModSalaryList).Checked;
	}

	private void chkModifyBranch_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboBranch).Enabled = ((UltraToggleEditorBase)chkModBranch).Checked;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HR.Personal.MasterData.frmUpdateGroupEmployees));
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
		this.pnlAccounts = new UltraPanel();
		this.cboLeavePermissionRule = new UltraComboEditor();
		this.lblLeaveRule = new UltraLabel();
		this.cboVacationRule = new UltraComboEditor();
		this.ultraLabel13 = new UltraLabel();
		this.cboPenalityRule = new UltraComboEditor();
		this.ultraLabel12 = new UltraLabel();
		this.cboFeedingRule = new UltraComboEditor();
		this.cboAbsenceRule = new UltraComboEditor();
		this.lblFeedingRule = new UltraLabel();
		this.ultraLabel11 = new UltraLabel();
		this.cboLateRule = new UltraComboEditor();
		this.ultraLabel10 = new UltraLabel();
		this.cboExtraTimeRule = new UltraComboEditor();
		this.ultraLabel9 = new UltraLabel();
		this.pnlPOS = new UltraPanel();
		this.cboSalaryAccount = new UltraComboEditor();
		this.ultraLabel17 = new UltraLabel();
		this.cboPenaltyAccount = new UltraComboEditor();
		this.ultraLabel16 = new UltraLabel();
		this.cboAdvanceAccount = new UltraComboEditor();
		this.ultraLabel15 = new UltraLabel();
		this.lblRoot = new UltraLabel();
		this.cboGroup = new UltraComboEditor();
		this.btnSaveAndClose = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.btnCancel = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.chkModRules = new UltraCheckEditor();
		this.chkModIsMonthlySalary = new UltraCheckEditor();
		this.chkModInSalaryTax = new UltraCheckEditor();
		this.chkModSalaryAtEndOFMonth = new UltraCheckEditor();
		this.chkModAccounts = new UltraCheckEditor();
		this.chkModSalaryList = new UltraCheckEditor();
		this.chkIsMonthlySalary = new UltraCheckEditor();
		this.chkSalaryAtEndOFMonth = new UltraCheckEditor();
		this.chkInSalaryTax = new UltraCheckEditor();
		this.cboSalaryList = new UltraComboEditor();
		this.lblSalaryList = new UltraLabel();
		this.chkModBranch = new UltraCheckEditor();
		this.cboBranch = new UltraComboEditor();
		this.lblBranch = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlAccounts).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboLeavePermissionRule).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVacationRule).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPenalityRule).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboFeedingRule).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAbsenceRule).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLateRule).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboExtraTimeRule).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlPOS.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlPOS).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboSalaryAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPenaltyAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAdvanceAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboGroup).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModRules).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModIsMonthlySalary).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModInSalaryTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModSalaryAtEndOFMonth).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModAccounts).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModSalaryList).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsMonthlySalary).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkSalaryAtEndOFMonth).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInSalaryTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalaryList).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranch).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.pnlAccounts, "pnlAccounts");
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val, "appearance31");
		this.pnlAccounts.Appearance = (AppearanceBase)(object)val;
		this.pnlAccounts.BorderStyle = (UIElementBorderStyle)7;
		resources.ApplyResources(this.pnlAccounts.ClientArea, "pnlAccounts.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.cboLeavePermissionRule);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.lblLeaveRule);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.cboVacationRule);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel13);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.cboPenalityRule);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel12);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.cboFeedingRule);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.cboAbsenceRule);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.lblFeedingRule);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel11);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.cboLateRule);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel10);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.cboExtraTimeRule);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel9);
		((System.Windows.Forms.Control)(object)this.pnlAccounts).Name = "pnlAccounts";
		((UltraControlBase)this.pnlAccounts).UseAppStyling = false;
		resources.ApplyResources(this.cboLeavePermissionRule, "cboLeavePermissionRule");
		((System.Windows.Forms.Control)(object)this.cboLeavePermissionRule).Name = "cboLeavePermissionRule";
		resources.ApplyResources(this.lblLeaveRule, "lblLeaveRule");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val2, "appearance32");
		((ControlBase)this.lblLeaveRule).Appearance = (AppearanceBase)(object)val2;
		this.lblLeaveRule.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLeaveRule).Name = "lblLeaveRule";
		((ControlBase)this.lblLeaveRule).WrapText = false;
		resources.ApplyResources(this.cboVacationRule, "cboVacationRule");
		((System.Windows.Forms.Control)(object)this.cboVacationRule).Name = "cboVacationRule";
		resources.ApplyResources(this.ultraLabel13, "ultraLabel13");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val3).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val3, "appearance33");
		((ControlBase)this.ultraLabel13).Appearance = (AppearanceBase)(object)val3;
		this.ultraLabel13.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel13).Name = "ultraLabel13";
		((ControlBase)this.ultraLabel13).WrapText = false;
		resources.ApplyResources(this.cboPenalityRule, "cboPenalityRule");
		((System.Windows.Forms.Control)(object)this.cboPenalityRule).Name = "cboPenalityRule";
		resources.ApplyResources(this.ultraLabel12, "ultraLabel12");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val4, "appearance34");
		((ControlBase)this.ultraLabel12).Appearance = (AppearanceBase)(object)val4;
		this.ultraLabel12.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel12).Name = "ultraLabel12";
		((ControlBase)this.ultraLabel12).WrapText = false;
		resources.ApplyResources(this.cboFeedingRule, "cboFeedingRule");
		((System.Windows.Forms.Control)(object)this.cboFeedingRule).Name = "cboFeedingRule";
		resources.ApplyResources(this.cboAbsenceRule, "cboAbsenceRule");
		((System.Windows.Forms.Control)(object)this.cboAbsenceRule).Name = "cboAbsenceRule";
		resources.ApplyResources(this.lblFeedingRule, "lblFeedingRule");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val5, "appearance35");
		((ControlBase)this.lblFeedingRule).Appearance = (AppearanceBase)(object)val5;
		this.lblFeedingRule.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFeedingRule).Name = "lblFeedingRule";
		((ControlBase)this.lblFeedingRule).WrapText = false;
		resources.ApplyResources(this.ultraLabel11, "ultraLabel11");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val6, "appearance36");
		((ControlBase)this.ultraLabel11).Appearance = (AppearanceBase)(object)val6;
		this.ultraLabel11.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel11).Name = "ultraLabel11";
		((ControlBase)this.ultraLabel11).WrapText = false;
		resources.ApplyResources(this.cboLateRule, "cboLateRule");
		((System.Windows.Forms.Control)(object)this.cboLateRule).Name = "cboLateRule";
		resources.ApplyResources(this.ultraLabel10, "ultraLabel10");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance37");
		((ControlBase)this.ultraLabel10).Appearance = (AppearanceBase)(object)val7;
		this.ultraLabel10.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel10).Name = "ultraLabel10";
		((ControlBase)this.ultraLabel10).WrapText = false;
		resources.ApplyResources(this.cboExtraTimeRule, "cboExtraTimeRule");
		((System.Windows.Forms.Control)(object)this.cboExtraTimeRule).Name = "cboExtraTimeRule";
		resources.ApplyResources(this.ultraLabel9, "ultraLabel9");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance38");
		((ControlBase)this.ultraLabel9).Appearance = (AppearanceBase)(object)val8;
		this.ultraLabel9.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel9).Name = "ultraLabel9";
		((ControlBase)this.ultraLabel9).WrapText = false;
		resources.ApplyResources(this.pnlPOS, "pnlPOS");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val9, "appearance1");
		this.pnlPOS.Appearance = (AppearanceBase)(object)val9;
		this.pnlPOS.BorderStyle = (UIElementBorderStyle)7;
		resources.ApplyResources(this.pnlPOS.ClientArea, "pnlPOS.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlPOS.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.cboSalaryAccount);
		((System.Windows.Forms.Control)(object)this.pnlPOS.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel17);
		((System.Windows.Forms.Control)(object)this.pnlPOS.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.cboPenaltyAccount);
		((System.Windows.Forms.Control)(object)this.pnlPOS.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel16);
		((System.Windows.Forms.Control)(object)this.pnlPOS.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.cboAdvanceAccount);
		((System.Windows.Forms.Control)(object)this.pnlPOS.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel15);
		((System.Windows.Forms.Control)(object)this.pnlPOS).Name = "pnlPOS";
		((UltraControlBase)this.pnlPOS).UseAppStyling = false;
		resources.ApplyResources(this.cboSalaryAccount, "cboSalaryAccount");
		((System.Windows.Forms.Control)(object)this.cboSalaryAccount).Name = "cboSalaryAccount";
		resources.ApplyResources(this.ultraLabel17, "ultraLabel17");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance39");
		((ControlBase)this.ultraLabel17).Appearance = (AppearanceBase)(object)val10;
		this.ultraLabel17.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel17).Name = "ultraLabel17";
		((ControlBase)this.ultraLabel17).WrapText = false;
		resources.ApplyResources(this.cboPenaltyAccount, "cboPenaltyAccount");
		((System.Windows.Forms.Control)(object)this.cboPenaltyAccount).Name = "cboPenaltyAccount";
		resources.ApplyResources(this.ultraLabel16, "ultraLabel16");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance40");
		((ControlBase)this.ultraLabel16).Appearance = (AppearanceBase)(object)val11;
		this.ultraLabel16.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel16).Name = "ultraLabel16";
		((ControlBase)this.ultraLabel16).WrapText = false;
		resources.ApplyResources(this.cboAdvanceAccount, "cboAdvanceAccount");
		((System.Windows.Forms.Control)(object)this.cboAdvanceAccount).Name = "cboAdvanceAccount";
		resources.ApplyResources(this.ultraLabel15, "ultraLabel15");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance41");
		((ControlBase)this.ultraLabel15).Appearance = (AppearanceBase)(object)val12;
		this.ultraLabel15.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel15).Name = "ultraLabel15";
		((ControlBase)this.ultraLabel15).WrapText = false;
		resources.ApplyResources(this.lblRoot, "lblRoot");
		this.lblRoot.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRoot).Name = "lblRoot";
		((ControlBase)this.lblRoot).WrapText = false;
		resources.ApplyResources(this.cboGroup, "cboGroup");
		this.cboGroup.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboGroup).Name = "cboGroup";
		((TextEditorControlBase)this.cboGroup).Nullable = false;
		resources.ApplyResources(this.btnSaveAndClose, "btnSaveAndClose");
		((AppearanceBase)val13).Image = resources.GetObject("appearance9.Image");
		resources.ApplyResources(val13, "appearance9");
		((ControlBase)this.btnSaveAndClose).Appearance = (AppearanceBase)(object)val13;
		((ControlBase)this.btnSaveAndClose).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSaveAndClose).Name = "btnSaveAndClose";
		((System.Windows.Forms.Control)(object)this.btnSaveAndClose).Click += new System.EventHandler(btnSaveAndClose_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val14).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val14, "appearance42");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val15).Image = resources.GetObject("appearance43.Image");
		resources.ApplyResources(val15, "appearance43");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val15;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((AppearanceBase)val16).Image = resources.GetObject("appearance44.Image");
		resources.ApplyResources(val16, "appearance44");
		((ControlBase)this.btnCancel).Appearance = (AppearanceBase)(object)val16;
		((ControlBase)this.btnCancel).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val17).Image = resources.GetObject("appearance45.Image");
		resources.ApplyResources(val17, "appearance45");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val17;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val18).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val18).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val18, "appearance46");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val18;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.chkModRules, "chkModRules");
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val19, "appearance47");
		((UltraToggleEditorBase)this.chkModRules).Appearance = (AppearanceBase)(object)val19;
		((System.Windows.Forms.Control)(object)this.chkModRules).Name = "chkModRules";
		((UltraToggleEditorBase)this.chkModRules).CheckedChanged += new System.EventHandler(chkModAccounts_CheckedChanged);
		resources.ApplyResources(this.chkModIsMonthlySalary, "chkModIsMonthlySalary");
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val20, "appearance48");
		((UltraToggleEditorBase)this.chkModIsMonthlySalary).Appearance = (AppearanceBase)(object)val20;
		((System.Windows.Forms.Control)(object)this.chkModIsMonthlySalary).Name = "chkModIsMonthlySalary";
		((UltraToggleEditorBase)this.chkModIsMonthlySalary).CheckedChanged += new System.EventHandler(chkModIsMonthlySalary_CheckedChanged);
		resources.ApplyResources(this.chkModInSalaryTax, "chkModInSalaryTax");
		((AppearanceBase)val21).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val21, "appearance49");
		((UltraToggleEditorBase)this.chkModInSalaryTax).Appearance = (AppearanceBase)(object)val21;
		((System.Windows.Forms.Control)(object)this.chkModInSalaryTax).Name = "chkModInSalaryTax";
		((UltraToggleEditorBase)this.chkModInSalaryTax).CheckedChanged += new System.EventHandler(chkModInSalaryTax_CheckedChanged);
		resources.ApplyResources(this.chkModSalaryAtEndOFMonth, "chkModSalaryAtEndOFMonth");
		((AppearanceBase)val22).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val22, "appearance50");
		((UltraToggleEditorBase)this.chkModSalaryAtEndOFMonth).Appearance = (AppearanceBase)(object)val22;
		((System.Windows.Forms.Control)(object)this.chkModSalaryAtEndOFMonth).Name = "chkModSalaryAtEndOFMonth";
		((UltraToggleEditorBase)this.chkModSalaryAtEndOFMonth).CheckedChanged += new System.EventHandler(chkModSalaryAtEndOFMonth_CheckedChanged);
		resources.ApplyResources(this.chkModAccounts, "chkModAccounts");
		((AppearanceBase)val23).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val23, "appearance51");
		((UltraToggleEditorBase)this.chkModAccounts).Appearance = (AppearanceBase)(object)val23;
		((System.Windows.Forms.Control)(object)this.chkModAccounts).Name = "chkModAccounts";
		((UltraToggleEditorBase)this.chkModAccounts).CheckedChanged += new System.EventHandler(chkModPOS_CheckedChanged);
		resources.ApplyResources(this.chkModSalaryList, "chkModSalaryList");
		((AppearanceBase)val24).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val24, "appearance52");
		((UltraToggleEditorBase)this.chkModSalaryList).Appearance = (AppearanceBase)(object)val24;
		((System.Windows.Forms.Control)(object)this.chkModSalaryList).Name = "chkModSalaryList";
		((UltraToggleEditorBase)this.chkModSalaryList).CheckedChanged += new System.EventHandler(chkModSalaryList_CheckedChanged);
		resources.ApplyResources(this.chkIsMonthlySalary, "chkIsMonthlySalary");
		((AppearanceBase)val25).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val25, "appearance53");
		((UltraToggleEditorBase)this.chkIsMonthlySalary).Appearance = (AppearanceBase)(object)val25;
		((UltraToggleEditorBase)this.chkIsMonthlySalary).Checked = true;
		((UltraToggleEditorBase)this.chkIsMonthlySalary).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsMonthlySalary).Name = "chkIsMonthlySalary";
		resources.ApplyResources(this.chkSalaryAtEndOFMonth, "chkSalaryAtEndOFMonth");
		((AppearanceBase)val26).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val26, "appearance54");
		((UltraToggleEditorBase)this.chkSalaryAtEndOFMonth).Appearance = (AppearanceBase)(object)val26;
		((System.Windows.Forms.Control)(object)this.chkSalaryAtEndOFMonth).Name = "chkSalaryAtEndOFMonth";
		resources.ApplyResources(this.chkInSalaryTax, "chkInSalaryTax");
		((AppearanceBase)val27).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val27, "appearance55");
		((UltraToggleEditorBase)this.chkInSalaryTax).Appearance = (AppearanceBase)(object)val27;
		((UltraToggleEditorBase)this.chkInSalaryTax).Checked = true;
		((UltraToggleEditorBase)this.chkInSalaryTax).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkInSalaryTax).Name = "chkInSalaryTax";
		resources.ApplyResources(this.cboSalaryList, "cboSalaryList");
		((System.Windows.Forms.Control)(object)this.cboSalaryList).Name = "cboSalaryList";
		resources.ApplyResources(this.lblSalaryList, "lblSalaryList");
		((AppearanceBase)val28).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val28).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val28, "appearance56");
		((ControlBase)this.lblSalaryList).Appearance = (AppearanceBase)(object)val28;
		this.lblSalaryList.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSalaryList).Name = "lblSalaryList";
		((ControlBase)this.lblSalaryList).WrapText = false;
		resources.ApplyResources(this.chkModBranch, "chkModBranch");
		((AppearanceBase)val29).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val29, "appearance57");
		((UltraToggleEditorBase)this.chkModBranch).Appearance = (AppearanceBase)(object)val29;
		((System.Windows.Forms.Control)(object)this.chkModBranch).Name = "chkModBranch";
		((UltraToggleEditorBase)this.chkModBranch).CheckedChanged += new System.EventHandler(chkModifyBranch_CheckedChanged);
		resources.ApplyResources(this.cboBranch, "cboBranch");
		((System.Windows.Forms.Control)(object)this.cboBranch).Name = "cboBranch";
		resources.ApplyResources(this.lblBranch, "lblBranch");
		((AppearanceBase)val30).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val30).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val30, "appearance58");
		((ControlBase)this.lblBranch).Appearance = (AppearanceBase)(object)val30;
		this.lblBranch.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBranch).Name = "lblBranch";
		((ControlBase)this.lblBranch).WrapText = false;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkInSalaryTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsMonthlySalary);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSalaryList);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSalaryList);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModInSalaryTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkSalaryAtEndOFMonth);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModIsMonthlySalary);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModSalaryAtEndOFMonth);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSaveAndClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModSalaryList);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlPOS);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlAccounts);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModRules);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModAccounts);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRoot);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboGroup);
		base.Name = "frmUpdateGroupEmployees";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboGroup, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRoot, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModAccounts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModRules, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlAccounts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlPOS, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModSalaryList, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSaveAndClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModSalaryAtEndOFMonth, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModIsMonthlySalary, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkSalaryAtEndOFMonth, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModInSalaryTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSalaryList, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSalaryList, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsMonthlySalary, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkInSalaryTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlAccounts.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlAccounts).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.cboLeavePermissionRule).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVacationRule).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPenalityRule).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboFeedingRule).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAbsenceRule).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLateRule).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboExtraTimeRule).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlPOS.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlPOS.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlPOS).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.cboSalaryAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPenaltyAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAdvanceAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboGroup).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModRules).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModIsMonthlySalary).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModInSalaryTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModSalaryAtEndOFMonth).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModAccounts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModSalaryList).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsMonthlySalary).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkSalaryAtEndOFMonth).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInSalaryTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalaryList).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranch).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
