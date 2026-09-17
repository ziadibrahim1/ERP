using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.HR;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTree;

namespace ERP.HR.Personal.MasterData;

public class frmPenaltyRules : frmHeaderManyDetails
{
	private DataTable dtReports;

	private DataTable dtEmployees;

	private DataTable dtEmployeesDetails;

	private DataTable dtPenaltyNames;

	private ValueList vlPenaltyNames = new ValueList();

	private IContainer components = null;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraTextEditor txtEnglishName;

	private UltraLabel lblEnglishName;

	private UltraTextEditor txtArabicName;

	private UltraLabel lblArabicName;

	private UltraTabPageControl ultraTabPageControl2;

	public UltraButton btnEmployeesSearch;

	public UltraTextEditor txtEmployees;

	public UltraTree TreeEmployees;

	protected internal UltraCheckEditor chkAll;

	private NumericUpDown txtSalaryDays;

	private NumericUpDown txtSalaryWorkHours;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel7;

	private UltraLabel lblActualMonthDays;

	private UltraLabel ultraLabel8;

	public frmPenaltyRules()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		TableName = "HR_PenaltyRules";
		IDCol = "PenaltyRuleID";
		NoCol = "PenaltyRuleNo";
		DateCol = "GetDate()";
	}

	public frmPenaltyRules(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtPenaltyNames = PenaltyNames.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlPenaltyNames.ValueListItems.Clear();
		for (int i = 0; i < dtPenaltyNames.Rows.Count; i++)
		{
			vlPenaltyNames.ValueListItems.Add(dtPenaltyNames.Rows[i]["PenaltyNameID"], dtPenaltyNames.Rows[i]["PenaltyName"].ToString());
		}
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtEmployees = SubAccounts.FillReportTreeEmployeesBySubAccountTypeIDs(GlobalVariables.EmployeeSubAccountTypeIDs, "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dtEmployees != null)
		{
			TreeFunctions.FillTree(TreeEmployees, dtEmployees, "ParentID", "SubAccountID", "SubAccountName", "EmployeeNo", "IsMain");
		}
		dtDetails = PenaltyRulesDetails.SelectByPenaltyRuleID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PenaltyRuleDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PenaltyNameID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FirstPenalty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SecondPenalty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ThirdPenalty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FourthPenalty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsMonthly"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["isQuarterly"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSixMonth"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsYearly"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PenaltyNameID"].Header).Caption = (GlobalVariables.IsArabic ? "الجزاء" : "Penalty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FirstPenalty"].Header).Caption = (GlobalVariables.IsArabic ? " (الجزاء الاول (يوم" : "First Penalty(Day)");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SecondPenalty"].Header).Caption = (GlobalVariables.IsArabic ? " (الجزاء الثانى (يوم" : "Second penalty(Day)");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ThirdPenalty"].Header).Caption = (GlobalVariables.IsArabic ? " (الجزاء الثالث (يوم" : "Third Penalty(Day)");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FourthPenalty"].Header).Caption = (GlobalVariables.IsArabic ? " (الجزاء الرابع (يوم" : "Fourth penalty(Day)");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsMonthly"].Header).Caption = (GlobalVariables.IsArabic ? "شهرى" : "Monthly");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["isQuarterly"].Header).Caption = (GlobalVariables.IsArabic ? "ربع سنوى" : "Quarterly");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSixMonth"].Header).Caption = (GlobalVariables.IsArabic ? "نصف سنوى" : "Biannually");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsYearly"].Header).Caption = (GlobalVariables.IsArabic ? "سنوى" : "Annually");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PenaltyNameID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FirstPenalty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SecondPenalty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ThirdPenalty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FourthPenalty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsMonthly"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["isQuarterly"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSixMonth"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsYearly"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsMonthly"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["isQuarterly"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSixMonth"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsYearly"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PenaltyNameID"].ValueList = (IValueList)(object)vlPenaltyNames;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = PenaltyRules.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
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
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Expected O, but got Unknown
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Expected O, but got Unknown
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Expected O, but got Unknown
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Expected O, but got Unknown
		base.DisplayData();
		if (drMaster != null)
		{
			((Control)(object)txtCode).Text = drMaster["PenaltyRuleNo"].ToString();
			((Control)(object)txtArabicName).Text = drMaster["PenaltyRuleNameAr"].ToString();
			((Control)(object)txtEnglishName).Text = drMaster["PenaltyRuleNameEn"].ToString();
			txtSalaryDays.Value = Convert.ToDecimal(drMaster["SalaryDays"]);
			txtSalaryWorkHours.Value = Convert.ToDecimal(drMaster["SalaryWorkHours"]);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			dtDetails = PenaltyRulesDetails.SelectByPenaltyRuleID(drMaster["PenaltyRuleID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtEmployeesDetails = Employees.SelectByPenaltyRuleID(drMaster["PenaltyRuleID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			((UltraToggleEditorBase)chkAll).Checked = false;
			TreeEmployees.AfterCheck -= new AfterNodeChangedEventHandler(TreeEmployees_AfterCheck);
			TreeEmployees.BeforeCheck -= new BeforeCheckEventHandler(TreeEmployees_BeforeCheck);
			TreeFunctions.SetAllTreeNodesCheckState(Checked: false, TreeEmployees);
			SetCheckedSubAccounts(dtEmployeesDetails);
			TreeEmployees.AfterCheck += new AfterNodeChangedEventHandler(TreeEmployees_AfterCheck);
			TreeEmployees.BeforeCheck += new BeforeCheckEventHandler(TreeEmployees_BeforeCheck);
			((UltraGridBase)ULGData).DataSource = dtDetails;
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
		((EditorButtonControlBase)txtArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEnglishName).ReadOnly = NavMode;
		txtSalaryDays.ReadOnly = NavMode;
		txtSalaryWorkHours.ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)btnEmployeesSearch).Visible = !NavMode;
		((Control)(object)chkAll).Enabled = !NavMode;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		((Control)(object)txtCode).Text = (Adding ? PenaltyRules.GetCode(IsFromServer: true) : "");
		((TextEditorControlBase)txtArabicName).Clear();
		((TextEditorControlBase)txtEnglishName).Clear();
		txtSalaryDays.Value = 30m;
		txtSalaryWorkHours.Value = 8m;
		((TextEditorControlBase)txtNotes).Clear();
		TreeFunctions.SetAllTreeNodesCheckState(Checked: false, TreeEmployees);
		((Control)(object)txtEmployees).Text = "";
		((UltraToggleEditorBase)chkAll).Checked = false;
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم  الأذن" : "Please Enter The Voucher Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال اسم لائحة الجزاءات بالعربية", "Please Enter Penalty Rule Arabic Name");
			return false;
		}
		if (Main.CheckForValue("HR_PenaltyRules", "PenaltyRuleNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["PenaltyRuleNo"].ToString(), IsFromServer: true) > 0)
		{
			string code = PenaltyRules.GetCode(IsFromServer: true);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Voucher Number Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = code;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا الإذن", "Please insert details for this Voucher");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["PenaltyNameID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الجزاء   ", "Please Enter Penalty Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["PenaltyNameID"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["FirstPenalty"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال الجزاء الاول  ", "Please Enter First penalty ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["FirstPenalty"];
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["SecondPenalty"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال الجزاء الثانى  ", "Please Enter Second penalty ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["SecondPenalty"];
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["ThirdPenalty"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال الجزاء الثالث  ", "Please Enter Third penalty ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ThirdPenalty"];
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["FourthPenalty"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال الجزاء الرابع  ", "Please Enter Fourth penalty ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["FourthPenalty"];
				return false;
			}
			if ((((UltraGridBase)ULGData).Rows[i].Cells["IsMonthly"].Value == DBNull.Value || !bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsMonthly"].Value.ToString())) && (((UltraGridBase)ULGData).Rows[i].Cells["isQuarterly"].Value == DBNull.Value || !bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["isQuarterly"].Value.ToString())) && (((UltraGridBase)ULGData).Rows[i].Cells["IsSixMonth"].Value == DBNull.Value || !bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsSixMonth"].Value.ToString())) && (((UltraGridBase)ULGData).Rows[i].Cells["IsYearly"].Value == DBNull.Value || !bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsYearly"].Value.ToString())))
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار مدة الاحتساب  ", "Please Select Period Count ");
				return false;
			}
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = PenaltyRules.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, txtSalaryDays.Value.ToString(), txtSalaryWorkHours.Value.ToString(), ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["PenaltyRuleDetailID"].Value = "-1";
				((UltraGridBase)ULGData).Rows[i].Cells["PenaltyRuleID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			PenaltyRulesDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
			string nodeCheckedIDs = GetNodeCheckedIDs();
			if (nodeCheckedIDs != ",")
			{
				Employees.UpdatePenaltyRuleIDBySubAccountIDs(nodeCheckedIDs, num.ToString(), IsFromServer: true);
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
			int num = PenaltyRules.Insert_Update(drMaster["PenaltyRuleID"].ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, txtSalaryDays.Value.ToString(), txtSalaryWorkHours.Value.ToString(), ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["PenaltyRuleDetailID"].Value.ToString() + ",";
				((UltraGridBase)ULGData).Rows[i].Cells["PenaltyRuleID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			Main.SyncDeleteForUpdate("HR_PenaltyRulesDetails", "PenaltyRuleID", drMaster["PenaltyRuleID"].ToString(), "PenaltyRuleDetailID", text, IsFromServer: true);
			PenaltyRulesDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
			string nodeCheckedIDs = GetNodeCheckedIDs();
			if (nodeCheckedIDs != ",")
			{
				Employees.UpdatePenaltyRuleIDBySubAccountIDs(nodeCheckedIDs, num.ToString(), IsFromServer: true);
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
			PenaltyRulesDetails.DeleteByPenaltyRuleID(drMaster["PenaltyRuleID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			PenaltyRules.Delete(drMaster["PenaltyRuleID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnPrintClick()
	{
		if (RowID != "")
		{
			string val = "";
			if (dtReports.Rows.Count > 0)
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
				val = dtReports.Rows[0]["isoCode"].ToString();
			}
			else
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_HR_PenaltyRules_A.rpt" : "Rep_HR_PenaltyRules_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@PenaltyRuleIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			GlobalVariables.ReportDocument.SetParameterValue("@PenaltyRuleIDs", "," + RowID + ",", "Rep_HR_PenaltyRulesSubAccount");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0", "Rep_HR_PenaltyRulesSubAccount");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.PenaltyRulesReport(IsFromServer: true);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["PenaltyRuleID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtPenaltyNames = PenaltyNames.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlPenaltyNames.ValueListItems.Clear();
		for (int i = 0; i < dtPenaltyNames.Rows.Count; i++)
		{
			vlPenaltyNames.ValueListItems.Add(dtPenaltyNames.Rows[i]["PenaltyNameID"], dtPenaltyNames.Rows[i]["PenaltyName"].ToString());
		}
		dtEmployees = SubAccounts.FillReportTreeEmployeesBySubAccountTypeIDs(GlobalVariables.EmployeeSubAccountTypeIDs, "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dtEmployees != null)
		{
			TreeFunctions.FillTree(TreeEmployees, dtEmployees, "ParentID", "SubAccountID", "SubAccountName", "EmployeeNo", "IsMain");
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "FirstPenalty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "SecondPenalty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ThirdPenalty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "FourthPenalty"))
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
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

	private void ULGData_CellChange(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Expected O, but got Unknown
		ULGData.CellChange -= new CellEventHandler(ULGData_CellChange);
		((UltraGridBase)ULGData).UpdateData();
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "IsMonthly" && ((UltraGridBase)ULGData).ActiveRow.Cells["IsMonthly"].Value.Equals(true))
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["isQuarterly"].Value = false;
			((UltraGridBase)ULGData).ActiveRow.Cells["IsSixMonth"].Value = false;
			((UltraGridBase)ULGData).ActiveRow.Cells["IsYearly"].Value = false;
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "isQuarterly" && ((UltraGridBase)ULGData).ActiveRow.Cells["isQuarterly"].Value.Equals(true))
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["IsMonthly"].Value = false;
			((UltraGridBase)ULGData).ActiveRow.Cells["IsSixMonth"].Value = false;
			((UltraGridBase)ULGData).ActiveRow.Cells["IsYearly"].Value = false;
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "IsSixMonth" && ((UltraGridBase)ULGData).ActiveRow.Cells["IsSixMonth"].Value.Equals(true))
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["IsMonthly"].Value = false;
			((UltraGridBase)ULGData).ActiveRow.Cells["isQuarterly"].Value = false;
			((UltraGridBase)ULGData).ActiveRow.Cells["IsYearly"].Value = false;
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "IsYearly" && ((UltraGridBase)ULGData).ActiveRow.Cells["IsYearly"].Value.Equals(true))
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["IsMonthly"].Value = false;
			((UltraGridBase)ULGData).ActiveRow.Cells["IsSixMonth"].Value = false;
			((UltraGridBase)ULGData).ActiveRow.Cells["isQuarterly"].Value = false;
		}
		ULGData.CellChange += new CellEventHandler(ULGData_CellChange);
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
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Expected O, but got Unknown
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Expected O, but got Unknown
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Expected O, but got Unknown
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Expected O, but got Unknown
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Expected O, but got Unknown
		//IL_0aca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad4: Expected O, but got Unknown
		//IL_0ae2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aec: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HR.Personal.MasterData.frmPenaltyRules));
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
		Override val13 = new Override();
		Appearance val14 = new Appearance();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.btnEmployeesSearch = new UltraButton();
		this.txtEmployees = new UltraTextEditor();
		this.TreeEmployees = new UltraTree();
		this.chkAll = new UltraCheckEditor();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.txtEnglishName = new UltraTextEditor();
		this.lblEnglishName = new UltraLabel();
		this.txtArabicName = new UltraTextEditor();
		this.lblArabicName = new UltraLabel();
		this.txtSalaryDays = new System.Windows.Forms.NumericUpDown();
		this.txtSalaryWorkHours = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel6 = new UltraLabel();
		this.ultraLabel5 = new UltraLabel();
		this.ultraLabel7 = new UltraLabel();
		this.lblActualMonthDays = new UltraLabel();
		this.ultraLabel8 = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtEmployees).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeEmployees).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalaryDays).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalaryWorkHours).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		((KeyedSubObjectBase)val).Key = "Employees";
		val.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val, "ultraTab1");
		((SubObjectBase)val).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[1] { val });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
		resources.ApplyResources(base.ULGData, "ULGData");
		((UltraGridBase)base.ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val2, "appearance4");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val3, "appearance5");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val4, "appearance6");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val5, "appearance7");
		((AppearanceBase)val5).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val6).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val6).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val6).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val6, "appearance8");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val7, "appearance9");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val8, "appearance10");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		base.ULGData.CellChange += new CellEventHandler(ULGData_CellChange);
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
		resources.ApplyResources(val9, "appearance11");
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
		((AppearanceBase)val10).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val10).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val10).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val10).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val10).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		resources.ApplyResources(val10, "appearance13");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val10;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.btnEmployeesSearch);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.txtEmployees);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.TreeEmployees);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.chkAll);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.btnEmployeesSearch, "btnEmployeesSearch");
		((AppearanceBase)val11).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val11, "appearance14");
		((ControlBase)this.btnEmployeesSearch).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.btnEmployeesSearch).Name = "btnEmployeesSearch";
		((System.Windows.Forms.Control)(object)this.btnEmployeesSearch).Click += new System.EventHandler(btnEmployeesSearch_Click);
		resources.ApplyResources(this.txtEmployees, "txtEmployees");
		((System.Windows.Forms.Control)(object)this.txtEmployees).Name = "txtEmployees";
		((TextEditorControlBase)this.txtEmployees).ValueChanged += new System.EventHandler(txtEmployees_ValueChanged);
		resources.ApplyResources(this.TreeEmployees, "TreeEmployees");
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance2");
		this.TreeEmployees.Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.TreeEmployees).Name = "TreeEmployees";
		val13.NodeStyle = (NodeStyle)1;
		this.TreeEmployees.Override = val13;
		((UltraControlBase)this.TreeEmployees).UseAppStyling = false;
		this.TreeEmployees.AfterCheck += new AfterNodeChangedEventHandler(TreeEmployees_AfterCheck);
		this.TreeEmployees.BeforeCheck += new BeforeCheckEventHandler(TreeEmployees_BeforeCheck);
		resources.ApplyResources(this.chkAll, "chkAll");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val14, "appearance15");
		((UltraToggleEditorBase)this.chkAll).Appearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.chkAll).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAll).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAll).Name = "chkAll";
		((UltraControlBase)this.chkAll).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAll).CheckedChanged += new System.EventHandler(chkAll_CheckedChanged);
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
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
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		this.ultraLabel6.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		((ControlBase)this.ultraLabel6).WrapText = false;
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		this.ultraLabel5.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.ultraLabel7, "ultraLabel7");
		this.ultraLabel7.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel7).Name = "ultraLabel7";
		((ControlBase)this.ultraLabel7).WrapText = false;
		resources.ApplyResources(this.lblActualMonthDays, "lblActualMonthDays");
		this.lblActualMonthDays.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblActualMonthDays).Name = "lblActualMonthDays";
		((ControlBase)this.lblActualMonthDays).WrapText = false;
		resources.ApplyResources(this.ultraLabel8, "ultraLabel8");
		((System.Windows.Forms.Control)(object)this.ultraLabel8).Name = "ultraLabel8";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel8);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblActualMonthDays);
		base.Controls.Add(this.txtSalaryDays);
		base.Controls.Add(this.txtSalaryWorkHours);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel7);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArabicName);
		base.Name = "frmPenaltyRules";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel7, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel5, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel6, 0);
		base.Controls.SetChildIndex(this.txtSalaryWorkHours, 0);
		base.Controls.SetChildIndex(this.txtSalaryDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UTCDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblActualMonthDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel8, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtEmployees).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeEmployees).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalaryDays).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalaryWorkHours).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
