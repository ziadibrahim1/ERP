using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.HR;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.HR.Personal.MasterData;

public class frmVacationRules : frmButtons
{
	private DataTable dtEmployees = new DataTable();

	private DataTable dtEmployeesDetails = new DataTable();

	private DataRow drMaster;

	private IContainer components = null;

	public UltraButton btnCopyTo;

	public UltraButton btnSearch;

	public UltraButton btnPriveous;

	public UltraButton btnNext;

	public UltraTextEditor txtCode;

	public UltraLabel lblCode;

	public UltraLabel lblTitle;

	public UltraLabel lblTitle2;

	private UltraTextEditor txtEnglishName;

	private UltraLabel lblEnglishName;

	private UltraTextEditor txtArabicName;

	private UltraLabel lblArabicName;

	private UltraTextEditor txtCustomStartingDays;

	private UltraLabel lblCustomStartingDays;

	private UltraTextEditor txtCustomMaxDays;

	private UltraLabel lblCustomMaxDays;

	private RadioButton rbFromInssuranceDate;

	private RadioButton rbFromHireDate;

	public UltraGroupBox UGBDetails;

	public UltraTree TreeEmployees;

	protected internal UltraCheckEditor chkAll;

	public UltraButton btnEmployeesSearch;

	public UltraTextEditor txtEmployees;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraLabel lblOccasionPeriod;

	private UltraTextEditor txtOccasionPeriod;

	private UltraLabel lblOccasionPeriodRaise;

	private UltraLabel ultraLabel9;

	private UltraTextEditor txtOccasionPeriodRaise;

	private UltraLabel ultraLabel10;

	private UltraLabel lblOccasionStartingDays;

	private UltraTextEditor txtOccasionStartingDays;

	private UltraLabel lblOccasionMaxDays;

	private UltraLabel ultraLabel13;

	private UltraTextEditor txtOccasionMaxDays;

	private UltraLabel ultraLabel14;

	private UltraLabel lblCustomPeriod;

	private UltraTextEditor txtCustomPeriod;

	private UltraLabel lblCustomPeriodRaise;

	private UltraLabel ultraLabel7;

	private UltraTextEditor txtCustomperiodRaise;

	private UltraLabel ultraLabel8;

	protected internal UltraCheckEditor chkSeniorRule;

	private UltraLabel lblVacationCalculatedFrom;

	private UltraLabel lblSeniorRuleCustomStartingDays;

	private UltraLabel lblSeniorRuleOccasionPeriod;

	private UltraLabel lblSeniorRuleOccasionPeriodRaise;

	private UltraTextEditor txtSeniorRuleOccasionPeriod;

	private UltraLabel ultraLabel6;

	private UltraTextEditor txtSeniorRuleOccasionPeriodRaise;

	private UltraTextEditor txtSeniorRuleOccasionMaxDays;

	private UltraLabel ultraLabel11;

	private UltraLabel ultraLabel12;

	private UltraTextEditor txtSeniorRuleCustomStartingDays;

	private UltraTextEditor txtSeniorRuleOccasionStartingDays;

	private UltraLabel lblSeniorRuleCustomMaxDays;

	private UltraLabel lblSeniorRuleOccasionMaxDays;

	private UltraLabel ultraLabel17;

	private UltraLabel lblSeniorRuleOccasionStartingDays;

	private UltraLabel lblSeniorRuleCustomPeriod;

	private UltraLabel ultraLabel20;

	private UltraTextEditor txtSeniorRuleCustomMaxDays;

	private UltraLabel ultraLabel21;

	private UltraTextEditor txtSeniorRuleCustomPeriod;

	private UltraLabel ultraLabel22;

	private UltraLabel lblSeniorRuleCustomPeriodRaise;

	private UltraLabel ultraLabel24;

	private UltraTextEditor txtSeniorRuleCustomPeriodRaise;

	protected internal UltraCheckEditor chkIsAutoTransferVacations;

	private NumericUpDown txtSalaryDays;

	private NumericUpDown txtSalaryWorkHours;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel4;

	private UltraLabel lblActualMonthDays;

	private UltraLabel ultraLabel15;

	private UltraLabel ultraLabel16;

	private UltraTextEditor txtDeductedSickVacationPercent;

	private UltraLabel ultraLabel18;

	public frmVacationRules()
	{
		InitializeComponent();
		TableName = "HR_VacationRules";
		IDCol = "VacationRuleID";
		NoCol = "VacationRuleNo";
		DateCol = "GetDate()";
	}

	public frmVacationRules(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void SetSecurity()
	{
		base.SetSecurity();
		UltraButton obj = btnNext;
		UltraButton obj2 = btnPriveous;
		bool flag = (((Control)(object)btnSearch).Enabled = CanSearching);
		bool enabled = (((Control)(object)obj2).Enabled = flag);
		((Control)(object)obj).Enabled = enabled;
	}

	public override void CallButtons(KeyEventArgs e)
	{
		base.CallButtons(e);
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

	public override void PrepareData()
	{
		base.PrepareData();
		dtEmployees = SubAccounts.FillReportTreeEmployeesBySubAccountTypeIDs(GlobalVariables.EmployeeSubAccountTypeIDs, "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dtEmployees != null)
		{
			TreeFunctions.FillTree(TreeEmployees, dtEmployees, "ParentID", "SubAccountID", "SubAccountName", "EmployeeNo", "IsMain");
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
			DataTable dataTable = VacationRules.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
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
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Expected O, but got Unknown
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Expected O, but got Unknown
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Expected O, but got Unknown
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Expected O, but got Unknown
		if (drMaster != null)
		{
			((Control)(object)txtCode).Text = drMaster["VacationRuleNo"].ToString();
			((Control)(object)txtArabicName).Text = drMaster["VacationRuleNameAr"].ToString();
			((Control)(object)txtEnglishName).Text = drMaster["VacationRuleNameEn"].ToString();
			rbFromHireDate.Checked = bool.Parse(drMaster["FromHireDate"].ToString());
			rbFromInssuranceDate.Checked = bool.Parse(drMaster["FromInssuranceDate"].ToString());
			((Control)(object)txtCustomStartingDays).Text = drMaster["CustomStartingDays"].ToString();
			((Control)(object)txtCustomMaxDays).Text = drMaster["CustomMaxDays"].ToString();
			((Control)(object)txtCustomPeriod).Text = drMaster["CustomPeriod"].ToString();
			((Control)(object)txtCustomperiodRaise).Text = drMaster["CustomPeriodRaise"].ToString();
			((Control)(object)txtOccasionStartingDays).Text = drMaster["OccasionStartingDays"].ToString();
			((Control)(object)txtOccasionMaxDays).Text = drMaster["OccasionMaxDays"].ToString();
			((Control)(object)txtOccasionPeriod).Text = drMaster["OccasionPeriod"].ToString();
			((Control)(object)txtOccasionPeriodRaise).Text = drMaster["OccasionPeriodRaise"].ToString();
			((UltraToggleEditorBase)chkSeniorRule).Checked = bool.Parse(drMaster["SeniorRule"].ToString());
			((UltraToggleEditorBase)chkIsAutoTransferVacations).Checked = bool.Parse(drMaster["IsAutoTransferVacations"].ToString());
			((Control)(object)txtSeniorRuleCustomStartingDays).Text = drMaster["SeniorRuleCustomStartingDays"].ToString();
			((Control)(object)txtSeniorRuleCustomMaxDays).Text = drMaster["SeniorRuleCustomMaxDays"].ToString();
			((Control)(object)txtSeniorRuleCustomPeriod).Text = drMaster["SeniorRuleCustomPeriod"].ToString();
			((Control)(object)txtSeniorRuleCustomPeriodRaise).Text = drMaster["SeniorRuleCustomPeriodRaise"].ToString();
			((Control)(object)txtSeniorRuleOccasionStartingDays).Text = drMaster["SeniorRuleOccasionStartingDays"].ToString();
			((Control)(object)txtSeniorRuleOccasionMaxDays).Text = drMaster["SeniorRuleOccasionMaxDays"].ToString();
			((Control)(object)txtSeniorRuleOccasionPeriod).Text = drMaster["SeniorRuleOccasionPeriod"].ToString();
			((Control)(object)txtSeniorRuleOccasionPeriodRaise).Text = drMaster["SeniorRuleOccasionPeriodRaise"].ToString();
			txtSalaryDays.Value = Convert.ToDecimal(drMaster["SalaryDays"]);
			txtSalaryWorkHours.Value = Convert.ToDecimal(drMaster["SalaryWorkHours"]);
			((TextEditorControlBase)txtDeductedSickVacationPercent).Value = Convert.ToDecimal(drMaster["DeductedSickVacationPercent"]);
			dtEmployeesDetails = Employees.SelectByVacationRuleID(drMaster["VacationRuleID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			((UltraToggleEditorBase)chkAll).Checked = false;
			TreeEmployees.AfterCheck -= new AfterNodeChangedEventHandler(TreeEmployees_AfterCheck);
			TreeEmployees.BeforeCheck -= new BeforeCheckEventHandler(TreeEmployees_BeforeCheck);
			TreeFunctions.SetAllTreeNodesCheckState(Checked: false, TreeEmployees);
			SetCheckedSubAccounts(dtEmployeesDetails);
			TreeEmployees.AfterCheck += new AfterNodeChangedEventHandler(TreeEmployees_AfterCheck);
			TreeEmployees.BeforeCheck += new BeforeCheckEventHandler(TreeEmployees_BeforeCheck);
			chkSeniorRule_CheckedChanged(null, null);
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
		((EditorButtonControlBase)txtArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEnglishName).ReadOnly = NavMode;
		rbFromHireDate.Enabled = !NavMode;
		rbFromInssuranceDate.Enabled = !NavMode;
		((EditorButtonControlBase)txtCustomStartingDays).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCustomMaxDays).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCustomPeriod).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCustomperiodRaise).ReadOnly = NavMode;
		((EditorButtonControlBase)txtOccasionStartingDays).ReadOnly = NavMode;
		((EditorButtonControlBase)txtOccasionMaxDays).ReadOnly = NavMode;
		((EditorButtonControlBase)txtOccasionPeriod).ReadOnly = NavMode;
		((EditorButtonControlBase)txtOccasionPeriodRaise).ReadOnly = NavMode;
		((Control)(object)chkSeniorRule).Enabled = !NavMode;
		((Control)(object)chkIsAutoTransferVacations).Enabled = !NavMode;
		((EditorButtonControlBase)txtSeniorRuleCustomStartingDays).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSeniorRuleCustomMaxDays).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSeniorRuleCustomPeriod).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSeniorRuleCustomPeriodRaise).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSeniorRuleOccasionStartingDays).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSeniorRuleOccasionMaxDays).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSeniorRuleOccasionPeriod).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSeniorRuleOccasionPeriodRaise).ReadOnly = NavMode;
		txtSalaryDays.ReadOnly = NavMode;
		txtSalaryWorkHours.ReadOnly = NavMode;
		((EditorButtonControlBase)txtDeductedSickVacationPercent).ReadOnly = NavMode;
		((Control)(object)btnEmployeesSearch).Visible = !NavMode;
		((Control)(object)chkAll).Enabled = !NavMode;
		((TextEditorControlBase)txtArabicName).Focus();
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtArabicName).Clear();
		((TextEditorControlBase)txtEnglishName).Clear();
		rbFromHireDate.Checked = true;
		rbFromInssuranceDate.Checked = false;
		((Control)(object)txtCustomStartingDays).Text = "0";
		((Control)(object)txtCustomMaxDays).Text = "0";
		((Control)(object)txtCustomPeriod).Text = "0";
		((Control)(object)txtCustomperiodRaise).Text = "0";
		((Control)(object)txtOccasionStartingDays).Text = "0";
		((Control)(object)txtOccasionMaxDays).Text = "0";
		((Control)(object)txtOccasionPeriod).Text = "0";
		((Control)(object)txtOccasionPeriodRaise).Text = "0";
		((UltraToggleEditorBase)chkSeniorRule).Checked = false;
		((UltraToggleEditorBase)chkIsAutoTransferVacations).Checked = false;
		((Control)(object)txtSeniorRuleCustomStartingDays).Text = "0";
		((Control)(object)txtSeniorRuleCustomMaxDays).Text = "0";
		((Control)(object)txtSeniorRuleCustomPeriod).Text = "0";
		((Control)(object)txtSeniorRuleCustomPeriodRaise).Text = "0";
		((Control)(object)txtSeniorRuleOccasionStartingDays).Text = "0";
		((Control)(object)txtSeniorRuleOccasionMaxDays).Text = "0";
		((Control)(object)txtSeniorRuleOccasionPeriod).Text = "0";
		((Control)(object)txtSeniorRuleOccasionPeriodRaise).Text = "0";
		txtSalaryDays.Value = 30m;
		txtSalaryWorkHours.Value = 8m;
		((TextEditorControlBase)txtDeductedSickVacationPercent).Value = 100;
		((Control)(object)txtCode).Text = (Adding ? VacationRules.GetCode(IsFromServer: true) : "");
		TreeFunctions.SetAllTreeNodesCheckState(Checked: false, TreeEmployees);
		((Control)(object)txtEmployees).Text = "";
		((UltraToggleEditorBase)chkAll).Checked = false;
		chkSeniorRule_CheckedChanged(null, null);
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال كود لائحة الاجازات", "Please Enter Vacation Rule");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال إسم لائحة الاجازات بالعربية", "Please Enter Vacation Rule Arabic Name");
			((TextEditorControlBase)txtArabicName).Focus();
			return false;
		}
		if (Main.CheckForValue("HR_VacationRules", "VacationRuleNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["VacationRuleNo"].ToString(), IsFromServer: true) > 0)
		{
			string code = VacationRules.GetCode(IsFromServer: true);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Voucher Number Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = code;
		}
		((Control)(object)txtCustomStartingDays).Text = ((((Control)(object)txtCustomStartingDays).Text == "") ? "0" : ((Control)(object)txtCustomStartingDays).Text);
		((Control)(object)txtCustomMaxDays).Text = ((((Control)(object)txtCustomMaxDays).Text == "") ? "0" : ((Control)(object)txtCustomMaxDays).Text);
		((Control)(object)txtOccasionStartingDays).Text = ((((Control)(object)txtOccasionStartingDays).Text == "") ? "0" : ((Control)(object)txtOccasionStartingDays).Text);
		((Control)(object)txtOccasionMaxDays).Text = ((((Control)(object)txtOccasionMaxDays).Text == "") ? "0" : ((Control)(object)txtOccasionMaxDays).Text);
		((Control)(object)txtSeniorRuleCustomStartingDays).Text = ((((Control)(object)txtSeniorRuleCustomStartingDays).Text == "") ? "0" : ((Control)(object)txtSeniorRuleCustomStartingDays).Text);
		((Control)(object)txtSeniorRuleCustomMaxDays).Text = ((((Control)(object)txtSeniorRuleCustomMaxDays).Text == "") ? "0" : ((Control)(object)txtSeniorRuleCustomMaxDays).Text);
		((Control)(object)txtSeniorRuleOccasionStartingDays).Text = ((((Control)(object)txtSeniorRuleOccasionStartingDays).Text == "") ? "0" : ((Control)(object)txtSeniorRuleOccasionStartingDays).Text);
		((Control)(object)txtSeniorRuleOccasionMaxDays).Text = ((((Control)(object)txtSeniorRuleOccasionMaxDays).Text == "") ? "0" : ((Control)(object)txtSeniorRuleOccasionMaxDays).Text);
		if (int.Parse(((Control)(object)txtCustomStartingDays).Text) > int.Parse(((Control)(object)txtCustomMaxDays).Text))
		{
			GlobalVariables.InformationMB.Show("لابد ان يكون الحد الاقصى للاجازات الاعتيادية أكبر من او يساوى الاجازات الاعتيادية", "Custom Starting Days Greater Than Custom Max Days");
			((TextEditorControlBase)txtCustomStartingDays).Focus();
			return false;
		}
		if (int.Parse(((Control)(object)txtOccasionStartingDays).Text) > int.Parse(((Control)(object)txtOccasionMaxDays).Text))
		{
			GlobalVariables.InformationMB.Show("لابد ان يكون الحد الاقصى للاجازات العارضة أكبر من او يساوى الاجازات العارضة", "Occasion Starting Days Greater Than Occasion Max Days");
			((TextEditorControlBase)txtOccasionStartingDays).Focus();
			return false;
		}
		if (((UltraToggleEditorBase)chkSeniorRule).Checked)
		{
			if (int.Parse(((Control)(object)txtSeniorRuleCustomStartingDays).Text) > int.Parse(((Control)(object)txtSeniorRuleCustomMaxDays).Text))
			{
				GlobalVariables.InformationMB.Show("لابد ان يكون الحد الاقصى للاجازات الاعتيادية أكبر من او يساوى الاجازات الاعتيادية", "Custom Starting Days Greater Than Custom Max Days");
				((TextEditorControlBase)txtSeniorRuleCustomStartingDays).Focus();
				return false;
			}
			if (int.Parse(((Control)(object)txtSeniorRuleOccasionStartingDays).Text) > int.Parse(((Control)(object)txtSeniorRuleOccasionMaxDays).Text))
			{
				GlobalVariables.InformationMB.Show("لابد ان يكون الحد الاقصى للاجازات العارضة أكبر من او يساوى الاجازات العارضة", "Occasion Starting Days Greater Than Occasion Max Days");
				((TextEditorControlBase)txtSeniorRuleOccasionStartingDays).Focus();
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
			int num = VacationRules.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, rbFromHireDate.Checked ? "1" : "0", rbFromInssuranceDate.Checked ? "1" : "0", (((Control)(object)txtCustomStartingDays).Text == "") ? "0" : ((Control)(object)txtCustomStartingDays).Text, (((Control)(object)txtCustomMaxDays).Text == "") ? "0" : ((Control)(object)txtCustomMaxDays).Text, (((Control)(object)txtCustomPeriod).Text == "") ? "0" : ((Control)(object)txtCustomPeriod).Text, (((Control)(object)txtCustomperiodRaise).Text == "") ? "0" : ((Control)(object)txtCustomperiodRaise).Text, (((Control)(object)txtOccasionStartingDays).Text == "") ? "0" : ((Control)(object)txtOccasionStartingDays).Text, (((Control)(object)txtOccasionMaxDays).Text == "") ? "0" : ((Control)(object)txtOccasionMaxDays).Text, (((Control)(object)txtOccasionPeriod).Text == "") ? "0" : ((Control)(object)txtOccasionPeriod).Text, (((Control)(object)txtOccasionPeriodRaise).Text == "") ? "0" : ((Control)(object)txtOccasionPeriodRaise).Text, ((UltraToggleEditorBase)chkSeniorRule).Checked ? "1" : "0", (!((UltraToggleEditorBase)chkSeniorRule).Checked) ? "0" : ((((Control)(object)txtSeniorRuleCustomStartingDays).Text == "") ? "0" : ((Control)(object)txtSeniorRuleCustomStartingDays).Text), (!((UltraToggleEditorBase)chkSeniorRule).Checked) ? "0" : ((((Control)(object)txtSeniorRuleCustomMaxDays).Text == "") ? "0" : ((Control)(object)txtSeniorRuleCustomMaxDays).Text), (!((UltraToggleEditorBase)chkSeniorRule).Checked) ? "0" : ((((Control)(object)txtSeniorRuleCustomPeriod).Text == "") ? "0" : ((Control)(object)txtSeniorRuleCustomPeriod).Text), (!((UltraToggleEditorBase)chkSeniorRule).Checked) ? "0" : ((((Control)(object)txtSeniorRuleCustomPeriodRaise).Text == "") ? "0" : ((Control)(object)txtSeniorRuleCustomPeriodRaise).Text), (!((UltraToggleEditorBase)chkSeniorRule).Checked) ? "0" : ((((Control)(object)txtSeniorRuleOccasionStartingDays).Text == "") ? "0" : ((Control)(object)txtSeniorRuleOccasionStartingDays).Text), (!((UltraToggleEditorBase)chkSeniorRule).Checked) ? "0" : ((((Control)(object)txtSeniorRuleOccasionMaxDays).Text == "") ? "0" : ((Control)(object)txtSeniorRuleOccasionMaxDays).Text), (!((UltraToggleEditorBase)chkSeniorRule).Checked) ? "0" : ((((Control)(object)txtSeniorRuleOccasionPeriod).Text == "") ? "0" : ((Control)(object)txtSeniorRuleOccasionPeriod).Text), (!((UltraToggleEditorBase)chkSeniorRule).Checked) ? "0" : ((((Control)(object)txtSeniorRuleOccasionPeriodRaise).Text == "") ? "0" : ((Control)(object)txtSeniorRuleOccasionPeriodRaise).Text), ((UltraToggleEditorBase)chkIsAutoTransferVacations).Checked ? "1" : "0", txtSalaryDays.Value.ToString(), txtSalaryWorkHours.Value.ToString(), ((TextEditorControlBase)txtDeductedSickVacationPercent).Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			string nodeCheckedIDs = GetNodeCheckedIDs();
			if (nodeCheckedIDs != ",")
			{
				Employees.UpdateVacationRuleIDBySubAccountIDs(nodeCheckedIDs, num.ToString(), IsFromServer: true);
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
			int num = VacationRules.Insert_Update(drMaster["VacationRuleID"].ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, rbFromHireDate.Checked ? "1" : "0", rbFromInssuranceDate.Checked ? "1" : "0", (((Control)(object)txtCustomStartingDays).Text == "") ? "0" : ((Control)(object)txtCustomStartingDays).Text, (((Control)(object)txtCustomMaxDays).Text == "") ? "0" : ((Control)(object)txtCustomMaxDays).Text, (((Control)(object)txtCustomPeriod).Text == "") ? "0" : ((Control)(object)txtCustomPeriod).Text, (((Control)(object)txtCustomperiodRaise).Text == "") ? "0" : ((Control)(object)txtCustomperiodRaise).Text, (((Control)(object)txtOccasionStartingDays).Text == "") ? "0" : ((Control)(object)txtOccasionStartingDays).Text, (((Control)(object)txtOccasionMaxDays).Text == "") ? "0" : ((Control)(object)txtOccasionMaxDays).Text, (((Control)(object)txtOccasionPeriod).Text == "") ? "0" : ((Control)(object)txtOccasionPeriod).Text, (((Control)(object)txtOccasionPeriodRaise).Text == "") ? "0" : ((Control)(object)txtOccasionPeriodRaise).Text, ((UltraToggleEditorBase)chkSeniorRule).Checked ? "1" : "0", (!((UltraToggleEditorBase)chkSeniorRule).Checked) ? "0" : ((((Control)(object)txtSeniorRuleCustomStartingDays).Text == "") ? "0" : ((Control)(object)txtSeniorRuleCustomStartingDays).Text), (!((UltraToggleEditorBase)chkSeniorRule).Checked) ? "0" : ((((Control)(object)txtSeniorRuleCustomMaxDays).Text == "") ? "0" : ((Control)(object)txtSeniorRuleCustomMaxDays).Text), (!((UltraToggleEditorBase)chkSeniorRule).Checked) ? "0" : ((((Control)(object)txtSeniorRuleCustomPeriod).Text == "") ? "0" : ((Control)(object)txtSeniorRuleCustomPeriod).Text), (!((UltraToggleEditorBase)chkSeniorRule).Checked) ? "0" : ((((Control)(object)txtSeniorRuleCustomPeriodRaise).Text == "") ? "0" : ((Control)(object)txtSeniorRuleCustomPeriodRaise).Text), (!((UltraToggleEditorBase)chkSeniorRule).Checked) ? "0" : ((((Control)(object)txtSeniorRuleOccasionStartingDays).Text == "") ? "0" : ((Control)(object)txtSeniorRuleOccasionStartingDays).Text), (!((UltraToggleEditorBase)chkSeniorRule).Checked) ? "0" : ((((Control)(object)txtSeniorRuleOccasionMaxDays).Text == "") ? "0" : ((Control)(object)txtSeniorRuleOccasionMaxDays).Text), (!((UltraToggleEditorBase)chkSeniorRule).Checked) ? "0" : ((((Control)(object)txtSeniorRuleOccasionPeriod).Text == "") ? "0" : ((Control)(object)txtSeniorRuleOccasionPeriod).Text), (!((UltraToggleEditorBase)chkSeniorRule).Checked) ? "0" : ((((Control)(object)txtSeniorRuleOccasionPeriodRaise).Text == "") ? "0" : ((Control)(object)txtSeniorRuleOccasionPeriodRaise).Text), ((UltraToggleEditorBase)chkIsAutoTransferVacations).Checked ? "1" : "0", txtSalaryDays.Value.ToString(), txtSalaryWorkHours.Value.ToString(), ((TextEditorControlBase)txtDeductedSickVacationPercent).Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			string nodeCheckedIDs = GetNodeCheckedIDs();
			if (nodeCheckedIDs != ",")
			{
				Employees.UpdateVacationRuleIDBySubAccountIDs(nodeCheckedIDs, num.ToString(), IsFromServer: true);
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
		VacationRules.Delete(drMaster["VacationRuleID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
	}

	public override void btnPrintClick()
	{
		if (RowID != "")
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_HR_VacationRules_A.rpt" : "Rep_HR_VacationRules_E.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@VacationRuleIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public string GetNodeCheckedIDs()
	{
		string text = ",";
		for (int i = 0; i < ((DisposableObjectCollectionBase)TreeEmployees.Nodes).Count; i++)
		{
			text = ((((DisposableObjectCollectionBase)TreeEmployees.Nodes[i].Nodes).Count != 0 || Convert.ToBoolean(((SubObjectBase)TreeEmployees.Nodes[i]).Tag) || TreeEmployees.Nodes[i].CheckedState != CheckState.Checked) ? (text + GetNodeCheckedChildsIDs(TreeEmployees.Nodes[i])) : (text + ((KeyedSubObjectBase)TreeEmployees.Nodes[i]).Key + ","));
		}
		return text;
	}

	public static string GetNodeCheckedChildsIDs(UltraTreeNode Node)
	{
		string text = "";
		for (int i = 0; i < ((DisposableObjectCollectionBase)Node.Nodes).Count; i++)
		{
			text = ((((DisposableObjectCollectionBase)Node.Nodes[i].Nodes).Count != 0 || Convert.ToBoolean(((SubObjectBase)Node.Nodes[i]).Tag) || Node.Nodes[i].CheckedState != CheckState.Checked) ? (text + GetNodeCheckedChildsIDs(Node.Nodes[i])) : (text + ((KeyedSubObjectBase)Node.Nodes[i]).Key + ","));
		}
		return text;
	}

	public virtual void btnCopyToClick()
	{
		if (!CanAdd)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		Adding = true;
		drMaster = null;
		SetControls(NavMode: false);
	}

	public override void btnUpdateClick()
	{
		if (drMaster != null)
		{
			RowID = drMaster[IDCol].ToString();
			base.btnUpdateClick();
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

	public override void btnCancelClick()
	{
		base.btnCancelClick();
		if (Adding)
		{
			drMaster = null;
		}
	}

	private void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.VacationRulesReport(IsFromServer: true);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["VacationRuleID"].ToString();
			FillData();
		}
	}

	private void btnPriveous_Click(object sender, EventArgs e)
	{
		PriveousData();
	}

	private void btnNext_Click(object sender, EventArgs e)
	{
		NextData();
	}

	public virtual void PriveousData()
	{
		if (dtSearchResult != null && dtSearchResult.Rows.Count > 1)
		{
			if (drMaster != null)
			{
				for (int i = 0; i < dtSearchResult.Rows.Count - 1; i++)
				{
					if (dtSearchResult.Rows[i][IDCol].ToString() == drMaster[IDCol].ToString())
					{
						RowID = dtSearchResult.Rows[i + 1][IDCol].ToString();
						FillData();
						break;
					}
				}
			}
			else
			{
				RowID = dtSearchResult.Rows[dtSearchResult.Rows.Count - 1][IDCol].ToString();
				FillData();
			}
		}
		else
		{
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, "", "0");
			if (dataTable.Rows.Count > 0)
			{
				RowID = dataTable.Rows[0][IDCol].ToString();
			}
			else
			{
				RowID = "";
			}
			FillData();
		}
	}

	public virtual void NextData()
	{
		if (dtSearchResult != null && dtSearchResult.Rows.Count > 1)
		{
			if (drMaster != null)
			{
				for (int i = 1; i < dtSearchResult.Rows.Count; i++)
				{
					if (dtSearchResult.Rows[i][IDCol].ToString() == drMaster[IDCol].ToString())
					{
						RowID = dtSearchResult.Rows[i - 1][IDCol].ToString();
						FillData();
						break;
					}
				}
			}
			else
			{
				RowID = dtSearchResult.Rows[0][IDCol].ToString();
				FillData();
			}
		}
		else
		{
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, "", "1");
			if (dataTable.Rows.Count > 0)
			{
				RowID = dataTable.Rows[0][IDCol].ToString();
			}
			else
			{
				RowID = "";
			}
			FillData();
		}
	}

	private void btnCopyTo_Click(object sender, EventArgs e)
	{
		btnCopyToClick();
	}

	private void txtCode_KeyUp(object sender, KeyEventArgs e)
	{
		if (!CanSearching || e.KeyCode != Keys.Return || TableName.Length <= 0 || NoCol.Length <= 0 || ((Control)(object)txtCode).Text.Length <= 0)
		{
			return;
		}
		if (Adding || Updating)
		{
			e.Handled = true;
			SendKeys.Send("{tab}");
			return;
		}
		DataTable comboData = Main.GetComboData(TableName, IDCol, NoCol + "=''" + ((Control)(object)txtCode).Text.Trim() + "'' And Deleted=0");
		if (comboData.Rows.Count > 0)
		{
			RowID = comboData.Rows[0][IDCol].ToString();
			dtSearchResult = null;
		}
		else
		{
			RowID = "";
		}
		FillData();
	}

	public override void btnRefreshDataClick()
	{
		dtEmployees = SubAccounts.FillReportTreeEmployeesBySubAccountTypeIDs(GlobalVariables.EmployeeSubAccountTypeIDs, "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dtEmployees != null)
		{
			TreeFunctions.FillTree(TreeEmployees, dtEmployees, "ParentID", "SubAccountID", "SubAccountName", "EmployeeNo", "IsMain");
		}
	}

	private void TreeEmployees_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		TreeEmployees.AfterCheck -= new AfterNodeChangedEventHandler(TreeEmployees_AfterCheck);
		TreeEmployees.BeforeCheck -= new BeforeCheckEventHandler(TreeEmployees_BeforeCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			TreeFunctions.SetParentCheckedState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAll).CheckedChanged -= chkAll_CheckedChanged;
		SetCheckBoxAllState(TreeEmployees, chkAll);
		((UltraToggleEditorBase)chkAll).CheckedChanged += chkAll_CheckedChanged;
		TreeEmployees.AfterCheck += new AfterNodeChangedEventHandler(TreeEmployees_AfterCheck);
		TreeEmployees.BeforeCheck += new BeforeCheckEventHandler(TreeEmployees_BeforeCheck);
	}

	private void TreeEmployees_BeforeCheck(object sender, BeforeCheckEventArgs e)
	{
		if (!Adding && !Updating)
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
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
		TreeEmployees.AfterCheck -= new AfterNodeChangedEventHandler(TreeEmployees_AfterCheck);
		TreeEmployees.BeforeCheck -= new BeforeCheckEventHandler(TreeEmployees_BeforeCheck);
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAll).Checked, TreeEmployees);
		TreeEmployees.AfterCheck += new AfterNodeChangedEventHandler(TreeEmployees_AfterCheck);
		TreeEmployees.BeforeCheck += new BeforeCheckEventHandler(TreeEmployees_BeforeCheck);
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

	public void SetCheckedSubAccounts(DataTable dtEmployees)
	{
		for (int i = 0; i < dtEmployees.Rows.Count; i++)
		{
			UltraTreeNode nodeByKey = TreeEmployees.GetNodeByKey(dtEmployees.Rows[i]["SubAccountID"].ToString());
			nodeByKey.CheckedState = CheckState.Checked;
			((UltraControlBase)TreeEmployees).Update();
			if (nodeByKey.Parent != null)
			{
				TreeFunctions.SetParentCheckedState(nodeByKey.Parent);
			}
		}
		((UltraToggleEditorBase)chkAll).CheckedChanged -= chkAll_CheckedChanged;
		SetCheckBoxAllState(TreeEmployees, chkAll);
		((UltraToggleEditorBase)chkAll).CheckedChanged += chkAll_CheckedChanged;
	}

	private void txtEmployees_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtEmployees);
		dataView.RowFilter = "SubAccountName Like '%" + ((Control)(object)txtEmployees).Text.Trim() + "%' ";
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TreeEmployees.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			TreeEmployees.ActiveNode = TreeEmployees.GetNodeByKey(dataView.ToTable().Rows[0]["SubAccountID"].ToString());
		}
	}

	private void btnEmployeesSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.EmployeesReport("1", "-1", IsFromServer: true);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeEmployees.GetNodeByKey(dtSearchResult.Rows[i]["SubAccountID"].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void chkSeniorRule_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)txtSeniorRuleCustomStartingDays).Enabled = ((UltraToggleEditorBase)chkSeniorRule).Checked;
		((Control)(object)txtSeniorRuleCustomMaxDays).Enabled = ((UltraToggleEditorBase)chkSeniorRule).Checked;
		((Control)(object)txtSeniorRuleCustomPeriod).Enabled = ((UltraToggleEditorBase)chkSeniorRule).Checked;
		((Control)(object)txtSeniorRuleCustomPeriodRaise).Enabled = ((UltraToggleEditorBase)chkSeniorRule).Checked;
		((Control)(object)txtSeniorRuleOccasionStartingDays).Enabled = ((UltraToggleEditorBase)chkSeniorRule).Checked;
		((Control)(object)txtSeniorRuleOccasionMaxDays).Enabled = ((UltraToggleEditorBase)chkSeniorRule).Checked;
		((Control)(object)txtSeniorRuleOccasionPeriod).Enabled = ((UltraToggleEditorBase)chkSeniorRule).Checked;
		((Control)(object)txtSeniorRuleOccasionPeriodRaise).Enabled = ((UltraToggleEditorBase)chkSeniorRule).Checked;
	}

	private void txtDeductedSickVacationPercent_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
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
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Expected O, but got Unknown
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Expected O, but got Unknown
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Expected O, but got Unknown
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Expected O, but got Unknown
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Expected O, but got Unknown
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Expected O, but got Unknown
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Expected O, but got Unknown
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Expected O, but got Unknown
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Expected O, but got Unknown
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Expected O, but got Unknown
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Expected O, but got Unknown
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Expected O, but got Unknown
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0ef5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eff: Expected O, but got Unknown
		//IL_0f0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f17: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HR.Personal.MasterData.frmVacationRules));
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
		Override val11 = new Override();
		Appearance val12 = new Appearance();
		Appearance val13 = new Appearance();
		Appearance val14 = new Appearance();
		this.btnCopyTo = new UltraButton();
		this.btnSearch = new UltraButton();
		this.btnPriveous = new UltraButton();
		this.btnNext = new UltraButton();
		this.txtCode = new UltraTextEditor();
		this.lblCode = new UltraLabel();
		this.lblTitle = new UltraLabel();
		this.lblTitle2 = new UltraLabel();
		this.txtEnglishName = new UltraTextEditor();
		this.lblEnglishName = new UltraLabel();
		this.txtArabicName = new UltraTextEditor();
		this.lblArabicName = new UltraLabel();
		this.txtCustomStartingDays = new UltraTextEditor();
		this.lblCustomStartingDays = new UltraLabel();
		this.txtCustomMaxDays = new UltraTextEditor();
		this.lblCustomMaxDays = new UltraLabel();
		this.rbFromInssuranceDate = new System.Windows.Forms.RadioButton();
		this.rbFromHireDate = new System.Windows.Forms.RadioButton();
		this.UGBDetails = new UltraGroupBox();
		this.btnEmployeesSearch = new UltraButton();
		this.txtEmployees = new UltraTextEditor();
		this.TreeEmployees = new UltraTree();
		this.chkAll = new UltraCheckEditor();
		this.ultraLabel1 = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.lblCustomPeriod = new UltraLabel();
		this.txtCustomPeriod = new UltraTextEditor();
		this.lblCustomPeriodRaise = new UltraLabel();
		this.ultraLabel7 = new UltraLabel();
		this.txtCustomperiodRaise = new UltraTextEditor();
		this.ultraLabel8 = new UltraLabel();
		this.lblOccasionPeriod = new UltraLabel();
		this.txtOccasionPeriod = new UltraTextEditor();
		this.lblOccasionPeriodRaise = new UltraLabel();
		this.ultraLabel9 = new UltraLabel();
		this.txtOccasionPeriodRaise = new UltraTextEditor();
		this.ultraLabel10 = new UltraLabel();
		this.lblOccasionStartingDays = new UltraLabel();
		this.txtOccasionStartingDays = new UltraTextEditor();
		this.lblOccasionMaxDays = new UltraLabel();
		this.ultraLabel13 = new UltraLabel();
		this.txtOccasionMaxDays = new UltraTextEditor();
		this.ultraLabel14 = new UltraLabel();
		this.chkSeniorRule = new UltraCheckEditor();
		this.lblVacationCalculatedFrom = new UltraLabel();
		this.lblSeniorRuleCustomStartingDays = new UltraLabel();
		this.lblSeniorRuleOccasionPeriod = new UltraLabel();
		this.lblSeniorRuleOccasionPeriodRaise = new UltraLabel();
		this.txtSeniorRuleOccasionPeriod = new UltraTextEditor();
		this.ultraLabel6 = new UltraLabel();
		this.txtSeniorRuleOccasionPeriodRaise = new UltraTextEditor();
		this.txtSeniorRuleOccasionMaxDays = new UltraTextEditor();
		this.ultraLabel11 = new UltraLabel();
		this.ultraLabel12 = new UltraLabel();
		this.txtSeniorRuleCustomStartingDays = new UltraTextEditor();
		this.txtSeniorRuleOccasionStartingDays = new UltraTextEditor();
		this.lblSeniorRuleCustomMaxDays = new UltraLabel();
		this.lblSeniorRuleOccasionMaxDays = new UltraLabel();
		this.ultraLabel17 = new UltraLabel();
		this.lblSeniorRuleOccasionStartingDays = new UltraLabel();
		this.lblSeniorRuleCustomPeriod = new UltraLabel();
		this.ultraLabel20 = new UltraLabel();
		this.txtSeniorRuleCustomMaxDays = new UltraTextEditor();
		this.ultraLabel21 = new UltraLabel();
		this.txtSeniorRuleCustomPeriod = new UltraTextEditor();
		this.ultraLabel22 = new UltraLabel();
		this.lblSeniorRuleCustomPeriodRaise = new UltraLabel();
		this.ultraLabel24 = new UltraLabel();
		this.txtSeniorRuleCustomPeriodRaise = new UltraTextEditor();
		this.chkIsAutoTransferVacations = new UltraCheckEditor();
		this.txtSalaryDays = new System.Windows.Forms.NumericUpDown();
		this.txtSalaryWorkHours = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel3 = new UltraLabel();
		this.ultraLabel5 = new UltraLabel();
		this.ultraLabel4 = new UltraLabel();
		this.lblActualMonthDays = new UltraLabel();
		this.ultraLabel15 = new UltraLabel();
		this.ultraLabel16 = new UltraLabel();
		this.txtDeductedSickVacationPercent = new UltraTextEditor();
		this.ultraLabel18 = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCustomStartingDays).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCustomMaxDays).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtEmployees).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeEmployees).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCustomPeriod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCustomperiodRaise).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOccasionPeriod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOccasionPeriodRaise).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOccasionStartingDays).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOccasionMaxDays).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkSeniorRule).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSeniorRuleOccasionPeriod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSeniorRuleOccasionPeriodRaise).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSeniorRuleOccasionMaxDays).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSeniorRuleCustomStartingDays).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSeniorRuleOccasionStartingDays).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSeniorRuleCustomMaxDays).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSeniorRuleCustomPeriod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSeniorRuleCustomPeriodRaise).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsAutoTransferVacations).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalaryDays).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalaryWorkHours).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDeductedSickVacationPercent).BeginInit();
		base.SuspendLayout();
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
		((AppearanceBase)val).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val, "appearance14");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		((AppearanceBase)val2).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val2).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val2).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val2).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val2, "appearance2");
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val2;
		base.cboTransactionBranch.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboTransactionBranch.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnCopyTo, "btnCopyTo");
		((System.Windows.Forms.Control)(object)this.btnCopyTo).Name = "btnCopyTo";
		((System.Windows.Forms.Control)(object)this.btnCopyTo).Click += new System.EventHandler(btnCopyTo_Click);
		resources.ApplyResources(this.btnSearch, "btnSearch");
		((AppearanceBase)val3).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val3, "appearance15");
		((ControlBase)this.btnSearch).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.btnSearch).Name = "btnSearch";
		((System.Windows.Forms.Control)(object)this.btnSearch).Click += new System.EventHandler(btnSearch_Click);
		resources.ApplyResources(this.btnPriveous, "btnPriveous");
		((AppearanceBase)val4).Image = ERP.Properties.Resources.BarLeft;
		resources.ApplyResources(val4, "appearance16");
		((ControlBase)this.btnPriveous).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.btnPriveous).Name = "btnPriveous";
		((System.Windows.Forms.Control)(object)this.btnPriveous).Click += new System.EventHandler(btnPriveous_Click);
		resources.ApplyResources(this.btnNext, "btnNext");
		((AppearanceBase)val5).Image = ERP.Properties.Resources.BarRight;
		resources.ApplyResources(val5, "appearance17");
		((ControlBase)this.btnNext).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.btnNext).Name = "btnNext";
		((System.Windows.Forms.Control)(object)this.btnNext).Click += new System.EventHandler(btnNext_Click);
		resources.ApplyResources(this.txtCode, "txtCode");
		((System.Windows.Forms.Control)(object)this.txtCode).Name = "txtCode";
		((System.Windows.Forms.Control)(object)this.txtCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtCode_KeyUp);
		resources.ApplyResources(this.lblCode, "lblCode");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val6).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val6, "appearance18");
		((ControlBase)this.lblCode).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.lblCode).Name = "lblCode";
		((UltraControlBase)this.lblCode).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val7).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val7, "appearance19");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val8).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val8, "appearance20");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.txtEnglishName, "txtEnglishName");
		((System.Windows.Forms.Control)(object)this.txtEnglishName).Name = "txtEnglishName";
		resources.ApplyResources(this.lblEnglishName, "lblEnglishName");
		this.lblEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEnglishName).Name = "lblEnglishName";
		((ControlBase)this.lblEnglishName).WrapText = false;
		resources.ApplyResources(this.txtArabicName, "txtArabicName");
		((System.Windows.Forms.Control)(object)this.txtArabicName).Name = "txtArabicName";
		resources.ApplyResources(this.lblArabicName, "lblArabicName");
		this.lblArabicName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArabicName).Name = "lblArabicName";
		((ControlBase)this.lblArabicName).WrapText = false;
		resources.ApplyResources(this.txtCustomStartingDays, "txtCustomStartingDays");
		((System.Windows.Forms.Control)(object)this.txtCustomStartingDays).Name = "txtCustomStartingDays";
		((System.Windows.Forms.Control)(object)this.txtCustomStartingDays).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblCustomStartingDays, "lblCustomStartingDays");
		this.lblCustomStartingDays.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCustomStartingDays).Name = "lblCustomStartingDays";
		((ControlBase)this.lblCustomStartingDays).WrapText = false;
		resources.ApplyResources(this.txtCustomMaxDays, "txtCustomMaxDays");
		((System.Windows.Forms.Control)(object)this.txtCustomMaxDays).Name = "txtCustomMaxDays";
		((System.Windows.Forms.Control)(object)this.txtCustomMaxDays).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblCustomMaxDays, "lblCustomMaxDays");
		this.lblCustomMaxDays.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCustomMaxDays).Name = "lblCustomMaxDays";
		((ControlBase)this.lblCustomMaxDays).WrapText = false;
		resources.ApplyResources(this.rbFromInssuranceDate, "rbFromInssuranceDate");
		this.rbFromInssuranceDate.BackColor = System.Drawing.Color.Transparent;
		this.rbFromInssuranceDate.Name = "rbFromInssuranceDate";
		this.rbFromInssuranceDate.TabStop = true;
		this.rbFromInssuranceDate.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.rbFromHireDate, "rbFromHireDate");
		this.rbFromHireDate.BackColor = System.Drawing.Color.Transparent;
		this.rbFromHireDate.Name = "rbFromHireDate";
		this.rbFromHireDate.TabStop = true;
		this.rbFromHireDate.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.UGBDetails, "UGBDetails");
		this.UGBDetails.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.btnEmployeesSearch);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtEmployees);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.TreeEmployees);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.chkAll);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Name = "UGBDetails";
		resources.ApplyResources(this.btnEmployeesSearch, "btnEmployeesSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance21");
		((ControlBase)this.btnEmployeesSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnEmployeesSearch).Name = "btnEmployeesSearch";
		((System.Windows.Forms.Control)(object)this.btnEmployeesSearch).Click += new System.EventHandler(btnEmployeesSearch_Click);
		resources.ApplyResources(this.txtEmployees, "txtEmployees");
		((System.Windows.Forms.Control)(object)this.txtEmployees).Name = "txtEmployees";
		((TextEditorControlBase)this.txtEmployees).ValueChanged += new System.EventHandler(txtEmployees_ValueChanged);
		resources.ApplyResources(this.TreeEmployees, "TreeEmployees");
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance10");
		this.TreeEmployees.Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.TreeEmployees).Name = "TreeEmployees";
		val11.NodeStyle = (NodeStyle)1;
		this.TreeEmployees.Override = val11;
		((UltraControlBase)this.TreeEmployees).UseAppStyling = false;
		this.TreeEmployees.AfterCheck += new AfterNodeChangedEventHandler(TreeEmployees_AfterCheck);
		this.TreeEmployees.BeforeCheck += new BeforeCheckEventHandler(TreeEmployees_BeforeCheck);
		resources.ApplyResources(this.chkAll, "chkAll");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance22");
		((UltraToggleEditorBase)this.chkAll).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.chkAll).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAll).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAll).Name = "chkAll";
		((UltraControlBase)this.chkAll).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAll).CheckedChanged += new System.EventHandler(chkAll_CheckedChanged);
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.lblCustomPeriod, "lblCustomPeriod");
		this.lblCustomPeriod.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCustomPeriod).Name = "lblCustomPeriod";
		((ControlBase)this.lblCustomPeriod).WrapText = false;
		resources.ApplyResources(this.txtCustomPeriod, "txtCustomPeriod");
		((System.Windows.Forms.Control)(object)this.txtCustomPeriod).Name = "txtCustomPeriod";
		((System.Windows.Forms.Control)(object)this.txtCustomPeriod).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblCustomPeriodRaise, "lblCustomPeriodRaise");
		this.lblCustomPeriodRaise.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCustomPeriodRaise).Name = "lblCustomPeriodRaise";
		((ControlBase)this.lblCustomPeriodRaise).WrapText = false;
		resources.ApplyResources(this.ultraLabel7, "ultraLabel7");
		this.ultraLabel7.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel7).Name = "ultraLabel7";
		((ControlBase)this.ultraLabel7).WrapText = false;
		resources.ApplyResources(this.txtCustomperiodRaise, "txtCustomperiodRaise");
		((System.Windows.Forms.Control)(object)this.txtCustomperiodRaise).Name = "txtCustomperiodRaise";
		((System.Windows.Forms.Control)(object)this.txtCustomperiodRaise).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel8, "ultraLabel8");
		this.ultraLabel8.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel8).Name = "ultraLabel8";
		((ControlBase)this.ultraLabel8).WrapText = false;
		resources.ApplyResources(this.lblOccasionPeriod, "lblOccasionPeriod");
		this.lblOccasionPeriod.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOccasionPeriod).Name = "lblOccasionPeriod";
		((ControlBase)this.lblOccasionPeriod).WrapText = false;
		resources.ApplyResources(this.txtOccasionPeriod, "txtOccasionPeriod");
		((System.Windows.Forms.Control)(object)this.txtOccasionPeriod).Name = "txtOccasionPeriod";
		((System.Windows.Forms.Control)(object)this.txtOccasionPeriod).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblOccasionPeriodRaise, "lblOccasionPeriodRaise");
		this.lblOccasionPeriodRaise.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOccasionPeriodRaise).Name = "lblOccasionPeriodRaise";
		((ControlBase)this.lblOccasionPeriodRaise).WrapText = false;
		resources.ApplyResources(this.ultraLabel9, "ultraLabel9");
		this.ultraLabel9.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel9).Name = "ultraLabel9";
		((ControlBase)this.ultraLabel9).WrapText = false;
		resources.ApplyResources(this.txtOccasionPeriodRaise, "txtOccasionPeriodRaise");
		((System.Windows.Forms.Control)(object)this.txtOccasionPeriodRaise).Name = "txtOccasionPeriodRaise";
		((System.Windows.Forms.Control)(object)this.txtOccasionPeriodRaise).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel10, "ultraLabel10");
		this.ultraLabel10.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel10).Name = "ultraLabel10";
		((ControlBase)this.ultraLabel10).WrapText = false;
		resources.ApplyResources(this.lblOccasionStartingDays, "lblOccasionStartingDays");
		this.lblOccasionStartingDays.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOccasionStartingDays).Name = "lblOccasionStartingDays";
		((ControlBase)this.lblOccasionStartingDays).WrapText = false;
		resources.ApplyResources(this.txtOccasionStartingDays, "txtOccasionStartingDays");
		((System.Windows.Forms.Control)(object)this.txtOccasionStartingDays).Name = "txtOccasionStartingDays";
		((System.Windows.Forms.Control)(object)this.txtOccasionStartingDays).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblOccasionMaxDays, "lblOccasionMaxDays");
		this.lblOccasionMaxDays.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOccasionMaxDays).Name = "lblOccasionMaxDays";
		((ControlBase)this.lblOccasionMaxDays).WrapText = false;
		resources.ApplyResources(this.ultraLabel13, "ultraLabel13");
		this.ultraLabel13.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel13).Name = "ultraLabel13";
		((ControlBase)this.ultraLabel13).WrapText = false;
		resources.ApplyResources(this.txtOccasionMaxDays, "txtOccasionMaxDays");
		((System.Windows.Forms.Control)(object)this.txtOccasionMaxDays).Name = "txtOccasionMaxDays";
		((System.Windows.Forms.Control)(object)this.txtOccasionMaxDays).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel14, "ultraLabel14");
		this.ultraLabel14.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel14).Name = "ultraLabel14";
		((ControlBase)this.ultraLabel14).WrapText = false;
		resources.ApplyResources(this.chkSeniorRule, "chkSeniorRule");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance23");
		((UltraToggleEditorBase)this.chkSeniorRule).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.chkSeniorRule).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkSeniorRule).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkSeniorRule).Name = "chkSeniorRule";
		((UltraControlBase)this.chkSeniorRule).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkSeniorRule).CheckedChanged += new System.EventHandler(chkSeniorRule_CheckedChanged);
		resources.ApplyResources(this.lblVacationCalculatedFrom, "lblVacationCalculatedFrom");
		this.lblVacationCalculatedFrom.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVacationCalculatedFrom).Name = "lblVacationCalculatedFrom";
		((ControlBase)this.lblVacationCalculatedFrom).WrapText = false;
		resources.ApplyResources(this.lblSeniorRuleCustomStartingDays, "lblSeniorRuleCustomStartingDays");
		this.lblSeniorRuleCustomStartingDays.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSeniorRuleCustomStartingDays).Name = "lblSeniorRuleCustomStartingDays";
		((ControlBase)this.lblSeniorRuleCustomStartingDays).WrapText = false;
		resources.ApplyResources(this.lblSeniorRuleOccasionPeriod, "lblSeniorRuleOccasionPeriod");
		this.lblSeniorRuleOccasionPeriod.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSeniorRuleOccasionPeriod).Name = "lblSeniorRuleOccasionPeriod";
		((ControlBase)this.lblSeniorRuleOccasionPeriod).WrapText = false;
		resources.ApplyResources(this.lblSeniorRuleOccasionPeriodRaise, "lblSeniorRuleOccasionPeriodRaise");
		this.lblSeniorRuleOccasionPeriodRaise.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSeniorRuleOccasionPeriodRaise).Name = "lblSeniorRuleOccasionPeriodRaise";
		((ControlBase)this.lblSeniorRuleOccasionPeriodRaise).WrapText = false;
		resources.ApplyResources(this.txtSeniorRuleOccasionPeriod, "txtSeniorRuleOccasionPeriod");
		((System.Windows.Forms.Control)(object)this.txtSeniorRuleOccasionPeriod).Name = "txtSeniorRuleOccasionPeriod";
		((System.Windows.Forms.Control)(object)this.txtSeniorRuleOccasionPeriod).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		this.ultraLabel6.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		((ControlBase)this.ultraLabel6).WrapText = false;
		resources.ApplyResources(this.txtSeniorRuleOccasionPeriodRaise, "txtSeniorRuleOccasionPeriodRaise");
		((System.Windows.Forms.Control)(object)this.txtSeniorRuleOccasionPeriodRaise).Name = "txtSeniorRuleOccasionPeriodRaise";
		((System.Windows.Forms.Control)(object)this.txtSeniorRuleOccasionPeriodRaise).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtSeniorRuleOccasionMaxDays, "txtSeniorRuleOccasionMaxDays");
		((System.Windows.Forms.Control)(object)this.txtSeniorRuleOccasionMaxDays).Name = "txtSeniorRuleOccasionMaxDays";
		((System.Windows.Forms.Control)(object)this.txtSeniorRuleOccasionMaxDays).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel11, "ultraLabel11");
		this.ultraLabel11.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel11).Name = "ultraLabel11";
		((ControlBase)this.ultraLabel11).WrapText = false;
		resources.ApplyResources(this.ultraLabel12, "ultraLabel12");
		this.ultraLabel12.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel12).Name = "ultraLabel12";
		((ControlBase)this.ultraLabel12).WrapText = false;
		resources.ApplyResources(this.txtSeniorRuleCustomStartingDays, "txtSeniorRuleCustomStartingDays");
		((System.Windows.Forms.Control)(object)this.txtSeniorRuleCustomStartingDays).Name = "txtSeniorRuleCustomStartingDays";
		((System.Windows.Forms.Control)(object)this.txtSeniorRuleCustomStartingDays).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtSeniorRuleOccasionStartingDays, "txtSeniorRuleOccasionStartingDays");
		((System.Windows.Forms.Control)(object)this.txtSeniorRuleOccasionStartingDays).Name = "txtSeniorRuleOccasionStartingDays";
		((System.Windows.Forms.Control)(object)this.txtSeniorRuleOccasionStartingDays).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblSeniorRuleCustomMaxDays, "lblSeniorRuleCustomMaxDays");
		this.lblSeniorRuleCustomMaxDays.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSeniorRuleCustomMaxDays).Name = "lblSeniorRuleCustomMaxDays";
		((ControlBase)this.lblSeniorRuleCustomMaxDays).WrapText = false;
		resources.ApplyResources(this.lblSeniorRuleOccasionMaxDays, "lblSeniorRuleOccasionMaxDays");
		this.lblSeniorRuleOccasionMaxDays.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSeniorRuleOccasionMaxDays).Name = "lblSeniorRuleOccasionMaxDays";
		((ControlBase)this.lblSeniorRuleOccasionMaxDays).WrapText = false;
		resources.ApplyResources(this.ultraLabel17, "ultraLabel17");
		this.ultraLabel17.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel17).Name = "ultraLabel17";
		((ControlBase)this.ultraLabel17).WrapText = false;
		resources.ApplyResources(this.lblSeniorRuleOccasionStartingDays, "lblSeniorRuleOccasionStartingDays");
		this.lblSeniorRuleOccasionStartingDays.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSeniorRuleOccasionStartingDays).Name = "lblSeniorRuleOccasionStartingDays";
		((ControlBase)this.lblSeniorRuleOccasionStartingDays).WrapText = false;
		resources.ApplyResources(this.lblSeniorRuleCustomPeriod, "lblSeniorRuleCustomPeriod");
		this.lblSeniorRuleCustomPeriod.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSeniorRuleCustomPeriod).Name = "lblSeniorRuleCustomPeriod";
		((ControlBase)this.lblSeniorRuleCustomPeriod).WrapText = false;
		resources.ApplyResources(this.ultraLabel20, "ultraLabel20");
		this.ultraLabel20.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel20).Name = "ultraLabel20";
		((ControlBase)this.ultraLabel20).WrapText = false;
		resources.ApplyResources(this.txtSeniorRuleCustomMaxDays, "txtSeniorRuleCustomMaxDays");
		((System.Windows.Forms.Control)(object)this.txtSeniorRuleCustomMaxDays).Name = "txtSeniorRuleCustomMaxDays";
		((System.Windows.Forms.Control)(object)this.txtSeniorRuleCustomMaxDays).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel21, "ultraLabel21");
		this.ultraLabel21.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel21).Name = "ultraLabel21";
		((ControlBase)this.ultraLabel21).WrapText = false;
		resources.ApplyResources(this.txtSeniorRuleCustomPeriod, "txtSeniorRuleCustomPeriod");
		((System.Windows.Forms.Control)(object)this.txtSeniorRuleCustomPeriod).Name = "txtSeniorRuleCustomPeriod";
		((System.Windows.Forms.Control)(object)this.txtSeniorRuleCustomPeriod).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel22, "ultraLabel22");
		this.ultraLabel22.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel22).Name = "ultraLabel22";
		((ControlBase)this.ultraLabel22).WrapText = false;
		resources.ApplyResources(this.lblSeniorRuleCustomPeriodRaise, "lblSeniorRuleCustomPeriodRaise");
		this.lblSeniorRuleCustomPeriodRaise.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSeniorRuleCustomPeriodRaise).Name = "lblSeniorRuleCustomPeriodRaise";
		((ControlBase)this.lblSeniorRuleCustomPeriodRaise).WrapText = false;
		resources.ApplyResources(this.ultraLabel24, "ultraLabel24");
		this.ultraLabel24.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel24).Name = "ultraLabel24";
		((ControlBase)this.ultraLabel24).WrapText = false;
		resources.ApplyResources(this.txtSeniorRuleCustomPeriodRaise, "txtSeniorRuleCustomPeriodRaise");
		((System.Windows.Forms.Control)(object)this.txtSeniorRuleCustomPeriodRaise).Name = "txtSeniorRuleCustomPeriodRaise";
		((System.Windows.Forms.Control)(object)this.txtSeniorRuleCustomPeriodRaise).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.chkIsAutoTransferVacations, "chkIsAutoTransferVacations");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val14, "appearance24");
		((UltraToggleEditorBase)this.chkIsAutoTransferVacations).Appearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.chkIsAutoTransferVacations).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkIsAutoTransferVacations).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkIsAutoTransferVacations).Name = "chkIsAutoTransferVacations";
		((UltraControlBase)this.chkIsAutoTransferVacations).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkIsAutoTransferVacations).CheckedChanged += new System.EventHandler(chkSeniorRule_CheckedChanged);
		resources.ApplyResources(this.txtSalaryDays, "txtSalaryDays");
		this.txtSalaryDays.Maximum = new decimal(new int[4] { 31, 0, 0, 0 });
		this.txtSalaryDays.Name = "txtSalaryDays";
		this.txtSalaryDays.Value = new decimal(new int[4] { 30, 0, 0, 0 });
		resources.ApplyResources(this.txtSalaryWorkHours, "txtSalaryWorkHours");
		this.txtSalaryWorkHours.DecimalPlaces = 1;
		this.txtSalaryWorkHours.Increment = new decimal(new int[4] { 5, 0, 0, 65536 });
		this.txtSalaryWorkHours.Maximum = new decimal(new int[4] { 24, 0, 0, 0 });
		this.txtSalaryWorkHours.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.txtSalaryWorkHours.Name = "txtSalaryWorkHours";
		this.txtSalaryWorkHours.Value = new decimal(new int[4] { 8, 0, 0, 0 });
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		this.ultraLabel5.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		this.ultraLabel4.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		((ControlBase)this.ultraLabel4).WrapText = false;
		resources.ApplyResources(this.lblActualMonthDays, "lblActualMonthDays");
		this.lblActualMonthDays.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblActualMonthDays).Name = "lblActualMonthDays";
		((ControlBase)this.lblActualMonthDays).WrapText = false;
		resources.ApplyResources(this.ultraLabel15, "ultraLabel15");
		((System.Windows.Forms.Control)(object)this.ultraLabel15).Name = "ultraLabel15";
		resources.ApplyResources(this.ultraLabel16, "ultraLabel16");
		this.ultraLabel16.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel16).Name = "ultraLabel16";
		((ControlBase)this.ultraLabel16).WrapText = false;
		resources.ApplyResources(this.txtDeductedSickVacationPercent, "txtDeductedSickVacationPercent");
		((System.Windows.Forms.Control)(object)this.txtDeductedSickVacationPercent).Name = "txtDeductedSickVacationPercent";
		((System.Windows.Forms.Control)(object)this.txtDeductedSickVacationPercent).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDeductedSickVacationPercent_KeyPress);
		resources.ApplyResources(this.ultraLabel18, "ultraLabel18");
		this.ultraLabel18.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel18).Name = "ultraLabel18";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel16);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDeductedSickVacationPercent);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel18);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel15);
		base.Controls.Add(this.txtSalaryDays);
		base.Controls.Add(this.txtSalaryWorkHours);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblActualMonthDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSeniorRuleCustomStartingDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSeniorRuleOccasionPeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSeniorRuleOccasionPeriodRaise);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSeniorRuleOccasionPeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSeniorRuleOccasionPeriodRaise);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSeniorRuleOccasionMaxDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel11);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel12);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSeniorRuleCustomStartingDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSeniorRuleOccasionStartingDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSeniorRuleCustomMaxDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSeniorRuleOccasionMaxDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel17);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSeniorRuleOccasionStartingDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSeniorRuleCustomPeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel20);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSeniorRuleCustomMaxDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel21);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSeniorRuleCustomPeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel22);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSeniorRuleCustomPeriodRaise);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel24);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSeniorRuleCustomPeriodRaise);
		base.Controls.Add(this.rbFromInssuranceDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVacationCalculatedFrom);
		base.Controls.Add(this.rbFromHireDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsAutoTransferVacations);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkSeniorRule);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBDetails);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCustomStartingDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCopyTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPriveous);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNext);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOccasionPeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOccasionPeriodRaise);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtOccasionPeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel9);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtOccasionPeriodRaise);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtOccasionMaxDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel14);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel13);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCustomStartingDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtOccasionStartingDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCustomMaxDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOccasionMaxDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel10);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOccasionStartingDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCustomPeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCustomMaxDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCustomPeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel8);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCustomPeriodRaise);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel7);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCustomperiodRaise);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmVacationRules";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCustomperiodRaise, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel7, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCustomPeriodRaise, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel8, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCustomPeriod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCustomMaxDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCustomPeriod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOccasionStartingDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel10, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOccasionMaxDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCustomMaxDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtOccasionStartingDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCustomStartingDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel13, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel14, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtOccasionMaxDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtOccasionPeriodRaise, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel9, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtOccasionPeriod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOccasionPeriodRaise, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOccasionPeriod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCustomStartingDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkSeniorRule, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsAutoTransferVacations, 0);
		base.Controls.SetChildIndex(this.rbFromHireDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVacationCalculatedFrom, 0);
		base.Controls.SetChildIndex(this.rbFromInssuranceDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSeniorRuleCustomPeriodRaise, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel24, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSeniorRuleCustomPeriodRaise, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel22, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSeniorRuleCustomPeriod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel21, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSeniorRuleCustomMaxDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel20, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSeniorRuleCustomPeriod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSeniorRuleOccasionStartingDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel17, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSeniorRuleOccasionMaxDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSeniorRuleCustomMaxDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSeniorRuleOccasionStartingDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSeniorRuleCustomStartingDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel12, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel11, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSeniorRuleOccasionMaxDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSeniorRuleOccasionPeriodRaise, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel6, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSeniorRuleOccasionPeriod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSeniorRuleOccasionPeriodRaise, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSeniorRuleOccasionPeriod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSeniorRuleCustomStartingDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel4, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel5, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblActualMonthDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex(this.txtSalaryWorkHours, 0);
		base.Controls.SetChildIndex(this.txtSalaryDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel15, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel18, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDeductedSickVacationPercent, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel16, 0);
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCustomStartingDays).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCustomMaxDays).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBDetails).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.UGBDetails).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtEmployees).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeEmployees).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCustomPeriod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCustomperiodRaise).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOccasionPeriod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOccasionPeriodRaise).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOccasionStartingDays).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOccasionMaxDays).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkSeniorRule).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSeniorRuleOccasionPeriod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSeniorRuleOccasionPeriodRaise).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSeniorRuleOccasionMaxDays).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSeniorRuleCustomStartingDays).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSeniorRuleOccasionStartingDays).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSeniorRuleCustomMaxDays).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSeniorRuleCustomPeriod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSeniorRuleCustomPeriodRaise).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsAutoTransferVacations).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalaryDays).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalaryWorkHours).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDeductedSickVacationPercent).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
