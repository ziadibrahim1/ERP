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
using Infragistics.Win.UltraWinMaskedEdit;
using Infragistics.Win.UltraWinTree;

namespace ERP.HR.Personal.MasterData;

public class frmExtraTimeRules : frmButtons
{
	private DataTable dtReports;

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

	private UltraTextEditor txtNotes;

	private UltraLabel lblNotes;

	public UltraGroupBox UGBDetails;

	public UltraTree TreeEmployees;

	protected internal UltraCheckEditor chkAll;

	public UltraButton btnEmployeesSearch;

	public UltraTextEditor txtEmployees;

	private UltraLabel lblWorkExtraHour;

	private UltraMaskedEdit txtWorkExtraHour;

	private UltraLabel lblVacationExtraHour;

	private UltraMaskedEdit txtVacationExtraHour;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraLabel lblWorkExtraHourNight;

	private UltraMaskedEdit txtWorkExtraHourNight;

	private UltraCheckEditor chkIsAuto;

	private NumericUpDown txtCalcAfter;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private NumericUpDown txtSalaryWorkHours;

	private NumericUpDown txtSalaryDays;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel7;

	private UltraLabel ultraLabel8;

	private UltraLabel ultraLabel9;

	private UltraCheckEditor chkBreakIncluded;

	private UltraMaskedEdit txtWorkExtraHourFrom;

	private UltraLabel lblTo;

	private UltraMaskedEdit txtWorkExtraHourTo;

	private UltraLabel ultraLabel10;

	private UltraLabel lblWorkExtraHourFrom;

	private UltraLabel ultraLabel11;

	private UltraLabel lblMaxMonthExtraTime;

	private UltraTextEditor txtMaxMonthExtraTime;

	private UltraCheckEditor chkEarlyCheckIncluded;

	public frmExtraTimeRules()
	{
		InitializeComponent();
		TableName = "HR_ExtraTimeRules";
		IDCol = "ExtraTimeRuleID";
		NoCol = "ExtraTimeRuleNo";
		DateCol = "GetDate()";
	}

	public frmExtraTimeRules(int ID)
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
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
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
			DataTable dataTable = ExtraTimeRules.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
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
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Expected O, but got Unknown
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Expected O, but got Unknown
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Expected O, but got Unknown
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Expected O, but got Unknown
		if (drMaster != null)
		{
			((Control)(object)txtCode).Text = drMaster["ExtraTimeRuleNo"].ToString();
			((Control)(object)txtArabicName).Text = drMaster["ExtraTimeRuleNameAr"].ToString();
			((Control)(object)txtEnglishName).Text = drMaster["ExtraTimeRuleNameEn"].ToString();
			if (!drMaster["VacationExtraHour"].Equals(DBNull.Value))
			{
				txtVacationExtraHour.Value = drMaster["VacationExtraHour"].ToString();
			}
			if (!drMaster["WorkExtraHour"].Equals(DBNull.Value))
			{
				txtWorkExtraHour.Value = drMaster["WorkExtraHour"].ToString();
			}
			if (!drMaster["WorkExtraHourFrom"].Equals(DBNull.Value))
			{
				txtWorkExtraHourFrom.Value = drMaster["WorkExtraHourFrom"].ToString();
			}
			if (!drMaster["WorkExtraHourTo"].Equals(DBNull.Value))
			{
				txtWorkExtraHourTo.Value = drMaster["WorkExtraHourTo"].ToString();
			}
			if (!drMaster["WorkExtraHourNight"].Equals(DBNull.Value))
			{
				txtWorkExtraHourNight.Value = drMaster["WorkExtraHourNight"].ToString();
			}
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((UltraToggleEditorBase)chkBreakIncluded).Checked = !drMaster["BreakIncluded"].Equals(DBNull.Value) && Convert.ToBoolean(drMaster["BreakIncluded"]);
			((UltraToggleEditorBase)chkEarlyCheckIncluded).Checked = !drMaster["EarlyCheckIncluded"].Equals(DBNull.Value) && Convert.ToBoolean(drMaster["EarlyCheckIncluded"]);
			((UltraToggleEditorBase)chkIsAuto).Checked = !drMaster["IsAuto"].Equals(DBNull.Value) && Convert.ToBoolean(drMaster["IsAuto"]);
			txtCalcAfter.Value = Convert.ToDecimal(drMaster["CalcAfterMI"]);
			txtSalaryDays.Value = Convert.ToDecimal(drMaster["SalaryDays"]);
			((TextEditorControlBase)txtMaxMonthExtraTime).Value = Convert.ToInt64(drMaster["MaxMonthExtraTime"]);
			txtSalaryWorkHours.Value = Convert.ToDecimal(drMaster["SalaryWorkHours"]);
			dtEmployeesDetails = Employees.SelectByExtraTimeRuleID(drMaster["ExtraTimeRuleID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			((UltraToggleEditorBase)chkAll).Checked = false;
			TreeEmployees.AfterCheck -= new AfterNodeChangedEventHandler(TreeEmployees_AfterCheck);
			TreeEmployees.BeforeCheck -= new BeforeCheckEventHandler(TreeEmployees_BeforeCheck);
			TreeFunctions.SetAllTreeNodesCheckState(Checked: false, TreeEmployees);
			SetCheckedSubAccounts(dtEmployeesDetails);
			TreeEmployees.AfterCheck += new AfterNodeChangedEventHandler(TreeEmployees_AfterCheck);
			TreeEmployees.BeforeCheck += new BeforeCheckEventHandler(TreeEmployees_BeforeCheck);
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
		txtVacationExtraHour.ReadOnly = NavMode;
		txtWorkExtraHour.ReadOnly = NavMode;
		txtWorkExtraHourFrom.ReadOnly = NavMode;
		txtWorkExtraHourTo.ReadOnly = NavMode;
		((EditorButtonControlBase)txtMaxMonthExtraTime).ReadOnly = NavMode;
		txtWorkExtraHourNight.ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		txtCalcAfter.ReadOnly = NavMode;
		txtSalaryDays.ReadOnly = NavMode;
		txtSalaryWorkHours.ReadOnly = NavMode;
		((Control)(object)btnEmployeesSearch).Visible = !NavMode;
		((Control)(object)chkAll).Enabled = !NavMode;
		((Control)(object)chkBreakIncluded).Enabled = !NavMode;
		((Control)(object)chkEarlyCheckIncluded).Enabled = !NavMode;
		((Control)(object)chkIsAuto).Enabled = !NavMode;
		((TextEditorControlBase)txtArabicName).Focus();
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtArabicName).Clear();
		((TextEditorControlBase)txtEnglishName).Clear();
		txtVacationExtraHour.Value = null;
		txtWorkExtraHour.Value = null;
		txtWorkExtraHourFrom.Value = "06:00:00";
		txtWorkExtraHourTo.Value = "19:00:00";
		((TextEditorControlBase)txtMaxMonthExtraTime).Value = 0;
		txtWorkExtraHourNight.Value = null;
		txtCalcAfter.Value = 0m;
		txtSalaryDays.Value = 30m;
		txtSalaryWorkHours.Value = 8m;
		((TextEditorControlBase)txtNotes).Clear();
		((Control)(object)txtCode).Text = (Adding ? ExtraTimeRules.GetCode(IsFromServer: true) : "");
		TreeFunctions.SetAllTreeNodesCheckState(Checked: false, TreeEmployees);
		((Control)(object)txtEmployees).Text = "";
		((UltraToggleEditorBase)chkBreakIncluded).Checked = false;
		((UltraToggleEditorBase)chkEarlyCheckIncluded).Checked = false;
		((UltraToggleEditorBase)chkIsAuto).Checked = false;
		((UltraToggleEditorBase)chkAll).Checked = false;
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال كود لائحة الاضافى", "Please Enter Extra Time Rule");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال إسم لائحة الاضافى بالعربية", "Please Enter Extra Time Rule Arabic Name");
			((TextEditorControlBase)txtArabicName).Focus();
			return false;
		}
		if (txtVacationExtraHour.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال إضافى يوم الاجازة  ", "Please Enter Vacation Extra Hour");
			((Control)(object)txtVacationExtraHour).Focus();
			return false;
		}
		if (txtWorkExtraHourFrom.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(" برجاء إدخال إضافى يوم عمل نهارى من  ", "Please Enter Work Extra Hour Day From");
			((Control)(object)txtWorkExtraHourFrom).Focus();
			return false;
		}
		if (txtWorkExtraHourTo.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(" برجاء إدخال إضافى يوم عمل نهارى الى  ", "Please Enter Work Extra Hour Day To");
			((Control)(object)txtWorkExtraHourTo).Focus();
			return false;
		}
		if (DateTime.Parse(txtWorkExtraHourTo.Value.ToString()) < DateTime.Parse(txtWorkExtraHourFrom.Value.ToString()))
		{
			GlobalVariables.InformationMB.Show("  إضافى يوم عمل نهارى الى  قبل من", "Work Extra Hour Day To Before From");
			return false;
		}
		if (txtWorkExtraHour.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال إضافى يوم عمل  ", "Please Enter Work Extra Hour");
			((Control)(object)txtWorkExtraHour).Focus();
			return false;
		}
		if (Main.CheckForValue("HR_ExtraTimeRules", "ExtraTimeRuleNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["ExtraTimeRuleNo"].ToString(), IsFromServer: true) > 0)
		{
			string code = ExtraTimeRules.GetCode(IsFromServer: true);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Voucher Number Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = code;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = ExtraTimeRules.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, txtVacationExtraHour.Value.ToString(), txtWorkExtraHour.Value.ToString(), txtWorkExtraHourFrom.Value.ToString(), txtWorkExtraHourTo.Value.ToString(), txtWorkExtraHourNight.Value.ToString(), ((Control)(object)txtNotes).Text, ((UltraToggleEditorBase)chkBreakIncluded).Checked ? "1" : "0", ((UltraToggleEditorBase)chkEarlyCheckIncluded).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsAuto).Checked ? "1" : "0", txtCalcAfter.Value.ToString(), txtSalaryDays.Value.ToString(), txtSalaryWorkHours.Value.ToString(), ((TextEditorControlBase)txtMaxMonthExtraTime).Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			string nodeCheckedIDs = GetNodeCheckedIDs();
			if (nodeCheckedIDs != ",")
			{
				Employees.UpdateExtraTimeRuleIDBySubAccountIDs(nodeCheckedIDs, num.ToString(), IsFromServer: true);
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
			int num = ExtraTimeRules.Insert_Update(drMaster["ExtraTimeRuleID"].ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, txtVacationExtraHour.Value.ToString(), txtWorkExtraHour.Value.ToString(), txtWorkExtraHourFrom.Value.ToString(), txtWorkExtraHourTo.Value.ToString(), txtWorkExtraHourNight.Value.ToString(), ((Control)(object)txtNotes).Text, ((UltraToggleEditorBase)chkBreakIncluded).Checked ? "1" : "0", ((UltraToggleEditorBase)chkEarlyCheckIncluded).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsAuto).Checked ? "1" : "0", txtCalcAfter.Value.ToString(), txtSalaryDays.Value.ToString(), txtSalaryWorkHours.Value.ToString(), ((TextEditorControlBase)txtMaxMonthExtraTime).Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			string nodeCheckedIDs = GetNodeCheckedIDs();
			if (nodeCheckedIDs != ",")
			{
				Employees.UpdateExtraTimeRuleIDBySubAccountIDs(nodeCheckedIDs, num.ToString(), IsFromServer: true);
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
		ExtraTimeRules.Delete(drMaster["ExtraTimeRuleID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
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
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_HR_ExtraTimeRules_A.rpt" : "Rep_HR_ExtraTimeRules_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@ExtraTimeRuleIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
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
		dtSearchResult = SearchFunctions.ExtraTimeRulesReport(IsFromServer: true);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["ExtraTimeRuleID"].ToString();
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

	private void chkIsAuto_CheckedChanged(object sender, EventArgs e)
	{
		txtCalcAfter.Enabled = ((UltraToggleEditorBase)chkIsAuto).Checked;
	}

	private void txtMaxMonthExtraTime_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
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
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected O, but got Unknown
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected O, but got Unknown
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Expected O, but got Unknown
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Expected O, but got Unknown
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Expected O, but got Unknown
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Expected O, but got Unknown
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Expected O, but got Unknown
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Expected O, but got Unknown
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Expected O, but got Unknown
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Expected O, but got Unknown
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Expected O, but got Unknown
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Expected O, but got Unknown
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Expected O, but got Unknown
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Expected O, but got Unknown
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Expected O, but got Unknown
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Expected O, but got Unknown
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Expected O, but got Unknown
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Expected O, but got Unknown
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Expected O, but got Unknown
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Expected O, but got Unknown
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Expected O, but got Unknown
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Expected O, but got Unknown
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Expected O, but got Unknown
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Expected O, but got Unknown
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Expected O, but got Unknown
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Expected O, but got Unknown
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Expected O, but got Unknown
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Expected O, but got Unknown
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Expected O, but got Unknown
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Expected O, but got Unknown
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Expected O, but got Unknown
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Expected O, but got Unknown
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Expected O, but got Unknown
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Expected O, but got Unknown
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Expected O, but got Unknown
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Expected O, but got Unknown
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Expected O, but got Unknown
		//IL_0be8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf2: Expected O, but got Unknown
		//IL_0c00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0a: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HR.Personal.MasterData.frmExtraTimeRules));
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
		Appearance val15 = new Appearance();
		Appearance val16 = new Appearance();
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
		this.txtNotes = new UltraTextEditor();
		this.lblNotes = new UltraLabel();
		this.UGBDetails = new UltraGroupBox();
		this.btnEmployeesSearch = new UltraButton();
		this.txtEmployees = new UltraTextEditor();
		this.TreeEmployees = new UltraTree();
		this.chkAll = new UltraCheckEditor();
		this.lblWorkExtraHour = new UltraLabel();
		this.txtWorkExtraHour = new UltraMaskedEdit();
		this.lblVacationExtraHour = new UltraLabel();
		this.txtVacationExtraHour = new UltraMaskedEdit();
		this.ultraLabel1 = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.lblWorkExtraHourNight = new UltraLabel();
		this.txtWorkExtraHourNight = new UltraMaskedEdit();
		this.chkIsAuto = new UltraCheckEditor();
		this.txtCalcAfter = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel3 = new UltraLabel();
		this.ultraLabel4 = new UltraLabel();
		this.txtSalaryWorkHours = new System.Windows.Forms.NumericUpDown();
		this.txtSalaryDays = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel5 = new UltraLabel();
		this.ultraLabel6 = new UltraLabel();
		this.ultraLabel7 = new UltraLabel();
		this.ultraLabel8 = new UltraLabel();
		this.ultraLabel9 = new UltraLabel();
		this.chkBreakIncluded = new UltraCheckEditor();
		this.txtWorkExtraHourFrom = new UltraMaskedEdit();
		this.lblTo = new UltraLabel();
		this.txtWorkExtraHourTo = new UltraMaskedEdit();
		this.ultraLabel10 = new UltraLabel();
		this.lblWorkExtraHourFrom = new UltraLabel();
		this.ultraLabel11 = new UltraLabel();
		this.lblMaxMonthExtraTime = new UltraLabel();
		this.txtMaxMonthExtraTime = new UltraTextEditor();
		this.chkEarlyCheckIncluded = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtEmployees).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeEmployees).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsAuto).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCalcAfter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalaryWorkHours).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalaryDays).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkBreakIncluded).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMaxMonthExtraTime).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkEarlyCheckIncluded).BeginInit();
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
		resources.ApplyResources(val, "appearance16");
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
		resources.ApplyResources(val3, "appearance17");
		((ControlBase)this.btnSearch).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.btnSearch).Name = "btnSearch";
		((System.Windows.Forms.Control)(object)this.btnSearch).Click += new System.EventHandler(btnSearch_Click);
		resources.ApplyResources(this.btnPriveous, "btnPriveous");
		((AppearanceBase)val4).Image = ERP.Properties.Resources.BarLeft;
		resources.ApplyResources(val4, "appearance18");
		((ControlBase)this.btnPriveous).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.btnPriveous).Name = "btnPriveous";
		((System.Windows.Forms.Control)(object)this.btnPriveous).Click += new System.EventHandler(btnPriveous_Click);
		resources.ApplyResources(this.btnNext, "btnNext");
		((AppearanceBase)val5).Image = ERP.Properties.Resources.BarRight;
		resources.ApplyResources(val5, "appearance19");
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
		resources.ApplyResources(val6, "appearance20");
		((ControlBase)this.lblCode).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.lblCode).Name = "lblCode";
		((UltraControlBase)this.lblCode).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val7).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val7, "appearance21");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val8).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val8, "appearance22");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val8;
		this.lblTitle2.AutoEllipsis = false;
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
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.UGBDetails, "UGBDetails");
		this.UGBDetails.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.btnEmployeesSearch);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtEmployees);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.TreeEmployees);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.chkAll);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Name = "UGBDetails";
		resources.ApplyResources(this.btnEmployeesSearch, "btnEmployeesSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance23");
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
		resources.ApplyResources(val12, "appearance24");
		((UltraToggleEditorBase)this.chkAll).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.chkAll).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAll).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAll).Name = "chkAll";
		((UltraControlBase)this.chkAll).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAll).CheckedChanged += new System.EventHandler(chkAll_CheckedChanged);
		resources.ApplyResources(this.lblWorkExtraHour, "lblWorkExtraHour");
		this.lblWorkExtraHour.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblWorkExtraHour).Name = "lblWorkExtraHour";
		((ControlBase)this.lblWorkExtraHour).WrapText = false;
		resources.ApplyResources(this.txtWorkExtraHour, "txtWorkExtraHour");
		this.txtWorkExtraHour.DisplayMode = (MaskMode)1;
		this.txtWorkExtraHour.EditAs = (EditAsType)8;
		this.txtWorkExtraHour.InputMask = "{LOC}hh:mm";
		((System.Windows.Forms.Control)(object)this.txtWorkExtraHour).Name = "txtWorkExtraHour";
		this.txtWorkExtraHour.NonAutoSizeHeight = 24;
		resources.ApplyResources(this.lblVacationExtraHour, "lblVacationExtraHour");
		this.lblVacationExtraHour.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVacationExtraHour).Name = "lblVacationExtraHour";
		((ControlBase)this.lblVacationExtraHour).WrapText = false;
		resources.ApplyResources(this.txtVacationExtraHour, "txtVacationExtraHour");
		this.txtVacationExtraHour.DisplayMode = (MaskMode)1;
		this.txtVacationExtraHour.EditAs = (EditAsType)8;
		this.txtVacationExtraHour.InputMask = "{LOC}hh:mm";
		((System.Windows.Forms.Control)(object)this.txtVacationExtraHour).Name = "txtVacationExtraHour";
		this.txtVacationExtraHour.NonAutoSizeHeight = 24;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.lblWorkExtraHourNight, "lblWorkExtraHourNight");
		this.lblWorkExtraHourNight.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblWorkExtraHourNight).Name = "lblWorkExtraHourNight";
		((ControlBase)this.lblWorkExtraHourNight).WrapText = false;
		resources.ApplyResources(this.txtWorkExtraHourNight, "txtWorkExtraHourNight");
		this.txtWorkExtraHourNight.DisplayMode = (MaskMode)1;
		this.txtWorkExtraHourNight.EditAs = (EditAsType)8;
		this.txtWorkExtraHourNight.InputMask = "{LOC}hh:mm";
		((System.Windows.Forms.Control)(object)this.txtWorkExtraHourNight).Name = "txtWorkExtraHourNight";
		this.txtWorkExtraHourNight.NonAutoSizeHeight = 24;
		resources.ApplyResources(this.chkIsAuto, "chkIsAuto");
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance25");
		((UltraToggleEditorBase)this.chkIsAuto).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.chkIsAuto).Name = "chkIsAuto";
		((UltraToggleEditorBase)this.chkIsAuto).CheckedChanged += new System.EventHandler(chkIsAuto_CheckedChanged);
		resources.ApplyResources(this.txtCalcAfter, "txtCalcAfter");
		this.txtCalcAfter.Increment = new decimal(new int[4] { 5, 0, 0, 0 });
		this.txtCalcAfter.Maximum = new decimal(new int[4] { 120, 0, 0, 0 });
		this.txtCalcAfter.Name = "txtCalcAfter";
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val14, "appearance26");
		((ControlBase)this.ultraLabel3).Appearance = (AppearanceBase)(object)val14;
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		((ControlBase)this.ultraLabel4).WrapText = false;
		resources.ApplyResources(this.txtSalaryWorkHours, "txtSalaryWorkHours");
		this.txtSalaryWorkHours.DecimalPlaces = 1;
		this.txtSalaryWorkHours.Increment = new decimal(new int[4] { 5, 0, 0, 65536 });
		this.txtSalaryWorkHours.Maximum = new decimal(new int[4] { 24, 0, 0, 0 });
		this.txtSalaryWorkHours.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.txtSalaryWorkHours.Name = "txtSalaryWorkHours";
		this.txtSalaryWorkHours.Value = new decimal(new int[4] { 8, 0, 0, 0 });
		resources.ApplyResources(this.txtSalaryDays, "txtSalaryDays");
		this.txtSalaryDays.Maximum = new decimal(new int[4] { 31, 0, 0, 0 });
		this.txtSalaryDays.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.txtSalaryDays.Name = "txtSalaryDays";
		this.txtSalaryDays.Value = new decimal(new int[4] { 30, 0, 0, 0 });
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		this.ultraLabel5.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		this.ultraLabel6.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		((ControlBase)this.ultraLabel6).WrapText = false;
		resources.ApplyResources(this.ultraLabel7, "ultraLabel7");
		this.ultraLabel7.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel7).Name = "ultraLabel7";
		((ControlBase)this.ultraLabel7).WrapText = false;
		resources.ApplyResources(this.ultraLabel8, "ultraLabel8");
		((System.Windows.Forms.Control)(object)this.ultraLabel8).Name = "ultraLabel8";
		((ControlBase)this.ultraLabel8).WrapText = false;
		resources.ApplyResources(this.ultraLabel9, "ultraLabel9");
		((System.Windows.Forms.Control)(object)this.ultraLabel9).Name = "ultraLabel9";
		((ControlBase)this.ultraLabel9).WrapText = false;
		resources.ApplyResources(this.chkBreakIncluded, "chkBreakIncluded");
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val15, "appearance27");
		((UltraToggleEditorBase)this.chkBreakIncluded).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.chkBreakIncluded).Name = "chkBreakIncluded";
		((UltraToggleEditorBase)this.chkBreakIncluded).CheckedChanged += new System.EventHandler(chkIsAuto_CheckedChanged);
		resources.ApplyResources(this.txtWorkExtraHourFrom, "txtWorkExtraHourFrom");
		this.txtWorkExtraHourFrom.DisplayMode = (MaskMode)1;
		this.txtWorkExtraHourFrom.EditAs = (EditAsType)8;
		this.txtWorkExtraHourFrom.InputMask = "{LOC}hh:mm";
		((System.Windows.Forms.Control)(object)this.txtWorkExtraHourFrom).Name = "txtWorkExtraHourFrom";
		this.txtWorkExtraHourFrom.NonAutoSizeHeight = 24;
		resources.ApplyResources(this.lblTo, "lblTo");
		this.lblTo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTo).Name = "lblTo";
		((ControlBase)this.lblTo).WrapText = false;
		resources.ApplyResources(this.txtWorkExtraHourTo, "txtWorkExtraHourTo");
		this.txtWorkExtraHourTo.DisplayMode = (MaskMode)1;
		this.txtWorkExtraHourTo.EditAs = (EditAsType)8;
		this.txtWorkExtraHourTo.InputMask = "{LOC}hh:mm";
		((System.Windows.Forms.Control)(object)this.txtWorkExtraHourTo).Name = "txtWorkExtraHourTo";
		this.txtWorkExtraHourTo.NonAutoSizeHeight = 24;
		resources.ApplyResources(this.ultraLabel10, "ultraLabel10");
		this.ultraLabel10.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel10).Name = "ultraLabel10";
		((ControlBase)this.ultraLabel10).WrapText = false;
		resources.ApplyResources(this.lblWorkExtraHourFrom, "lblWorkExtraHourFrom");
		this.lblWorkExtraHourFrom.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblWorkExtraHourFrom).Name = "lblWorkExtraHourFrom";
		((ControlBase)this.lblWorkExtraHourFrom).WrapText = false;
		resources.ApplyResources(this.ultraLabel11, "ultraLabel11");
		this.ultraLabel11.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel11).Name = "ultraLabel11";
		((ControlBase)this.ultraLabel11).WrapText = false;
		resources.ApplyResources(this.lblMaxMonthExtraTime, "lblMaxMonthExtraTime");
		this.lblMaxMonthExtraTime.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMaxMonthExtraTime).Name = "lblMaxMonthExtraTime";
		((ControlBase)this.lblMaxMonthExtraTime).WrapText = false;
		resources.ApplyResources(this.txtMaxMonthExtraTime, "txtMaxMonthExtraTime");
		((System.Windows.Forms.Control)(object)this.txtMaxMonthExtraTime).Name = "txtMaxMonthExtraTime";
		((System.Windows.Forms.Control)(object)this.txtMaxMonthExtraTime).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtMaxMonthExtraTime_KeyPress);
		resources.ApplyResources(this.chkEarlyCheckIncluded, "chkEarlyCheckIncluded");
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val16, "appearance28");
		((UltraToggleEditorBase)this.chkEarlyCheckIncluded).Appearance = (AppearanceBase)(object)val16;
		((System.Windows.Forms.Control)(object)this.chkEarlyCheckIncluded).Name = "chkEarlyCheckIncluded";
		((UltraToggleEditorBase)this.chkEarlyCheckIncluded).CheckedChanged += new System.EventHandler(chkIsAuto_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtMaxMonthExtraTime);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtWorkExtraHourFrom);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMaxMonthExtraTime);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblWorkExtraHourFrom);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel9);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel8);
		base.Controls.Add(this.txtSalaryDays);
		base.Controls.Add(this.txtSalaryWorkHours);
		base.Controls.Add(this.txtCalcAfter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkEarlyCheckIncluded);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkBreakIncluded);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsAuto);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblWorkExtraHourNight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtWorkExtraHourTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtWorkExtraHourNight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel11);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel10);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel7);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblWorkExtraHour);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtWorkExtraHour);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVacationExtraHour);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVacationExtraHour);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBDetails);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCopyTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPriveous);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNext);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmExtraTimeRules";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVacationExtraHour, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVacationExtraHour, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtWorkExtraHour, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblWorkExtraHour, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel7, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel10, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel5, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel11, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel6, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtWorkExtraHourNight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtWorkExtraHourTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblWorkExtraHourNight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel4, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsAuto, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkBreakIncluded, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkEarlyCheckIncluded, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex(this.txtCalcAfter, 0);
		base.Controls.SetChildIndex(this.txtSalaryWorkHours, 0);
		base.Controls.SetChildIndex(this.txtSalaryDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel8, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel9, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblWorkExtraHourFrom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMaxMonthExtraTime, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtWorkExtraHourFrom, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtMaxMonthExtraTime, 0);
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBDetails).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.UGBDetails).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtEmployees).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeEmployees).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsAuto).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCalcAfter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalaryWorkHours).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalaryDays).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkBreakIncluded).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMaxMonthExtraTime).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkEarlyCheckIncluded).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
