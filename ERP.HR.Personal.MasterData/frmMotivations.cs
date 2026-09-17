using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.HR;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.HR.Personal.MasterData;

public class frmMotivations : frmGrid
{
	private IContainer components = null;

	private UltraTextEditor txtArabicName;

	private UltraLabel lblArabicName;

	private UltraTextEditor txtEnglishName;

	private UltraLabel lblEnglishName;

	private UltraCheckEditor chkInTax;

	private UltraCheckEditor chkInInsurance;

	private UltraCheckEditor chkIsPrecent;

	private UltraTextEditor txtValue;

	private UltraLabel lblValue;

	private UltraCheckEditor chkExtraTime;

	private UltraCheckEditor chkMissions;

	private UltraTextEditor txtMinValueH;

	private UltraLabel ultraLabel1;

	private UltraCheckEditor chkLeavePermissionDays;

	private UltraCheckEditor chkVacationDays;

	private UltraTextEditor txtMaxValue;

	private UltraLabel ultraLabel2;

	private UltraCheckEditor chkPenaltyDays;

	private UltraCheckEditor chkDelayDays;

	private UltraCheckEditor chkAbsentDays;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel7;

	private UltraPanel pnlCheckType;

	private RadioButton rbIsYearly;

	private RadioButton rbIsSixMonth;

	private RadioButton rbisQuarterly;

	private RadioButton rbIsMonthly;

	private UltraCheckEditor chkInLeavePermission;

	private UltraCheckEditor chkIsSalary;

	private UltraCheckEditor chkInPenalty;

	private UltraCheckEditor chkInAbsent;

	private UltraCheckEditor chkInVacation;

	public frmMotivations()
	{
		InitializeComponent();
		TableName = "HR_Motivations";
		IDCol = "MotivationID";
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEnglishName).ReadOnly = NavMode;
		((Control)(object)chkInInsurance).Enabled = !NavMode;
		((Control)(object)chkInTax).Enabled = !NavMode;
		((Control)(object)chkIsPrecent).Enabled = !NavMode;
		((Control)(object)chkExtraTime).Enabled = !NavMode;
		((Control)(object)chkMissions).Enabled = !NavMode;
		((Control)(object)chkVacationDays).Enabled = !NavMode;
		((Control)(object)chkPenaltyDays).Enabled = !NavMode;
		((Control)(object)chkInPenalty).Enabled = !NavMode;
		((Control)(object)chkInAbsent).Enabled = !NavMode;
		((Control)(object)chkInVacation).Enabled = !NavMode;
		((Control)(object)chkDelayDays).Enabled = !NavMode;
		((Control)(object)chkAbsentDays).Enabled = !NavMode;
		((Control)(object)chkLeavePermissionDays).Enabled = !NavMode;
		((Control)(object)chkInLeavePermission).Enabled = !NavMode;
		((Control)(object)chkIsSalary).Enabled = !NavMode;
		rbIsMonthly.Enabled = !NavMode;
		rbisQuarterly.Enabled = !NavMode;
		rbIsSixMonth.Enabled = !NavMode;
		rbIsYearly.Enabled = !NavMode;
		((EditorButtonControlBase)txtValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtMinValueH).ReadOnly = NavMode;
		((EditorButtonControlBase)txtMaxValue).ReadOnly = NavMode;
		((TextEditorControlBase)txtArabicName).Focus();
	}

	public override void ClearControls()
	{
		((TextEditorControlBase)txtArabicName).Clear();
		((TextEditorControlBase)txtEnglishName).Clear();
		((UltraToggleEditorBase)chkInInsurance).Checked = true;
		((UltraToggleEditorBase)chkInTax).Checked = true;
		((UltraToggleEditorBase)chkExtraTime).Checked = false;
		((UltraToggleEditorBase)chkMissions).Checked = false;
		((UltraToggleEditorBase)chkVacationDays).Checked = false;
		((UltraToggleEditorBase)chkPenaltyDays).Checked = false;
		((UltraToggleEditorBase)chkInPenalty).Checked = false;
		((UltraToggleEditorBase)chkInAbsent).Checked = false;
		((UltraToggleEditorBase)chkInVacation).Checked = false;
		((UltraToggleEditorBase)chkDelayDays).Checked = false;
		((UltraToggleEditorBase)chkAbsentDays).Checked = false;
		((UltraToggleEditorBase)chkLeavePermissionDays).Checked = false;
		rbIsMonthly.Checked = false;
		((UltraToggleEditorBase)chkIsPrecent).Checked = false;
		((UltraToggleEditorBase)chkIsSalary).Checked = false;
		((UltraToggleEditorBase)chkInLeavePermission).Checked = false;
		((Control)(object)txtValue).Text = "0";
		((Control)(object)txtMinValueH).Text = "0";
		((Control)(object)txtMaxValue).Text = "0";
	}

	public override void FillData()
	{
		dataTable = Motivations.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dataTable;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MotivationNameAr"].Header).Caption = (GlobalVariables.IsArabic ? " اسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MotivationNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MotivationNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MotivationNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "اسم  بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MotivationNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MotivationNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.12);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsMonthly"].Header).Caption = (GlobalVariables.IsArabic ? " دورى" : "periodic");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsMonthly"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsMonthly"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["isQuarterly"].Header).Caption = (GlobalVariables.IsArabic ? "ربع سنوي" : "Quarterly");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["isQuarterly"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["isQuarterly"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSixMonth"].Header).Caption = (GlobalVariables.IsArabic ? "نصف سنوي" : "Semi Annually");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSixMonth"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSixMonth"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSalary"].Header).Caption = (GlobalVariables.IsArabic ? "المرتب" : "Salary");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSalary"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSalary"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsYearly"].Header).Caption = (GlobalVariables.IsArabic ? "سنوي" : "Annually");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsYearly"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsYearly"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExtraTime"].Header).Caption = (GlobalVariables.IsArabic ? " إضافى" : "Overtime");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExtraTime"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExtraTime"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MinValueH"].Header).Caption = (GlobalVariables.IsArabic ? "الحد الادني" : "MinValue(H)");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MinValueH"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MinValueH"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VacationDays"].Header).Caption = (GlobalVariables.IsArabic ? "اجازه" : "Vacation");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VacationDays"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VacationDays"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PenaltyDays"].Header).Caption = (GlobalVariables.IsArabic ? "جزاء" : "Penalty");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PenaltyDays"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PenaltyDays"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DelayDays"].Header).Caption = (GlobalVariables.IsArabic ? "تأخير" : "Delay");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DelayDays"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DelayDays"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AbsentDays"].Header).Caption = (GlobalVariables.IsArabic ? "غياب" : "Absent");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AbsentDays"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AbsentDays"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxValueDay"].Header).Caption = (GlobalVariables.IsArabic ? "الحد الاقصي" : "MaxValue(Days)");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxValueDay"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxValueDay"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtArabicName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["MotivationNameAr"].Value.ToString();
		((Control)(object)txtEnglishName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["MotivationNameEn"].Value.ToString();
		((UltraToggleEditorBase)chkInInsurance).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["InInsurance"].Value.ToString());
		((UltraToggleEditorBase)chkInLeavePermission).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["InLeavePermission"].Value.ToString());
		((UltraToggleEditorBase)chkIsSalary).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsSalary"].Value.ToString());
		((UltraToggleEditorBase)chkInTax).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["InTax"].Value.ToString());
		((UltraToggleEditorBase)chkExtraTime).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ExtraTime"].Value.ToString());
		((UltraToggleEditorBase)chkMissions).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Missions"].Value.ToString());
		((UltraToggleEditorBase)chkVacationDays).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["VacationDays"].Value.ToString());
		((UltraToggleEditorBase)chkPenaltyDays).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["PenaltyDays"].Value.ToString());
		((UltraToggleEditorBase)chkDelayDays).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["DelayDays"].Value.ToString());
		((UltraToggleEditorBase)chkAbsentDays).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["AbsentDays"].Value.ToString());
		((UltraToggleEditorBase)chkLeavePermissionDays).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["LeavePermissionDays"].Value.ToString());
		rbisQuarterly.Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["isQuarterly"].Value.ToString());
		rbIsSixMonth.Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsSixMonth"].Value.ToString());
		rbIsYearly.Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsYearly"].Value.ToString());
		((UltraToggleEditorBase)chkInPenalty).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["InPenalty"].Value.ToString());
		((UltraToggleEditorBase)chkInAbsent).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["InAbsent"].Value.ToString());
		((UltraToggleEditorBase)chkInVacation).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["InVacation"].Value.ToString());
		((Control)(object)txtMinValueH).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["MinValueH"].Value.ToString();
		((Control)(object)txtMaxValue).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["MaxValueDay"].Value.ToString();
		rbIsMonthly.Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsMonthly"].Value.ToString());
		((UltraToggleEditorBase)chkIsPrecent).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsPrecent"].Value.ToString());
		((Control)(object)txtValue).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["Value"].Value.ToString();
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال اسم الحافز بالعربية", "Please Enter Motivation Arabic Name");
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		Motivations.Insert_Update("-1", ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, ((UltraToggleEditorBase)chkExtraTime).Checked ? "1" : "0", ((UltraToggleEditorBase)chkMissions).Checked ? "1" : "0", (((Control)(object)txtMinValueH).Text == "") ? "0" : ((Control)(object)txtMinValueH).Text, ((UltraToggleEditorBase)chkLeavePermissionDays).Checked ? "1" : "0", ((UltraToggleEditorBase)chkVacationDays).Checked ? "1" : "0", ((UltraToggleEditorBase)chkPenaltyDays).Checked ? "1" : "0", ((UltraToggleEditorBase)chkDelayDays).Checked ? "1" : "0", ((UltraToggleEditorBase)chkAbsentDays).Checked ? "1" : "0", (((Control)(object)txtMaxValue).Text == "") ? "0" : ((Control)(object)txtMaxValue).Text, rbIsMonthly.Checked ? "1" : "0", rbisQuarterly.Checked ? "1" : "0", rbIsSixMonth.Checked ? "1" : "0", rbIsYearly.Checked ? "1" : "0", ((UltraToggleEditorBase)chkInPenalty).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInAbsent).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInVacation).Checked ? "1" : "0", "0", "0", ((UltraToggleEditorBase)chkInTax).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInInsurance).Checked ? "1" : "0", "0", ((UltraToggleEditorBase)chkInLeavePermission).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsSalary).Checked ? "1" : "0", "0", "0", ((UltraToggleEditorBase)chkIsPrecent).Checked ? "1" : "0", (((Control)(object)txtValue).Text == "") ? "0" : ((Control)(object)txtValue).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
		FillData();
	}

	public override void UpdateData()
	{
		Motivations.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["MotivationID"].Value.ToString(), ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, ((UltraToggleEditorBase)chkExtraTime).Checked ? "1" : "0", ((UltraToggleEditorBase)chkMissions).Checked ? "1" : "0", (((Control)(object)txtMinValueH).Text == "") ? "0" : ((Control)(object)txtMinValueH).Text, ((UltraToggleEditorBase)chkLeavePermissionDays).Checked ? "1" : "0", ((UltraToggleEditorBase)chkVacationDays).Checked ? "1" : "0", ((UltraToggleEditorBase)chkPenaltyDays).Checked ? "1" : "0", ((UltraToggleEditorBase)chkDelayDays).Checked ? "1" : "0", ((UltraToggleEditorBase)chkAbsentDays).Checked ? "1" : "0", (((Control)(object)txtMaxValue).Text == "") ? "0" : ((Control)(object)txtMaxValue).Text, rbIsMonthly.Checked ? "1" : "0", rbisQuarterly.Checked ? "1" : "0", rbIsSixMonth.Checked ? "1" : "0", rbIsYearly.Checked ? "1" : "0", ((UltraToggleEditorBase)chkInPenalty).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInAbsent).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInVacation).Checked ? "1" : "0", "0", "0", ((UltraToggleEditorBase)chkInTax).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInInsurance).Checked ? "1" : "0", "0", ((UltraToggleEditorBase)chkInLeavePermission).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsSalary).Checked ? "1" : "0", "0", "0", ((UltraToggleEditorBase)chkIsPrecent).Checked ? "1" : "0", (((Control)(object)txtValue).Text == "") ? "0" : ((Control)(object)txtValue).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
		FillData();
	}

	public override void DeleteData()
	{
		Motivations.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["MotivationID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void txtNumeric_KeyPress(object sender, KeyPressEventArgs e)
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
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Expected O, but got Unknown
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Expected O, but got Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Expected O, but got Unknown
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Expected O, but got Unknown
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Expected O, but got Unknown
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Expected O, but got Unknown
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Expected O, but got Unknown
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Expected O, but got Unknown
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Expected O, but got Unknown
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Expected O, but got Unknown
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Expected O, but got Unknown
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Expected O, but got Unknown
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Expected O, but got Unknown
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Expected O, but got Unknown
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Expected O, but got Unknown
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Expected O, but got Unknown
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Expected O, but got Unknown
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Expected O, but got Unknown
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HR.Personal.MasterData.frmMotivations));
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
		this.pnlCheckType = new UltraPanel();
		this.rbIsYearly = new System.Windows.Forms.RadioButton();
		this.rbIsMonthly = new System.Windows.Forms.RadioButton();
		this.rbisQuarterly = new System.Windows.Forms.RadioButton();
		this.rbIsSixMonth = new System.Windows.Forms.RadioButton();
		this.txtArabicName = new UltraTextEditor();
		this.lblArabicName = new UltraLabel();
		this.txtEnglishName = new UltraTextEditor();
		this.lblEnglishName = new UltraLabel();
		this.chkInTax = new UltraCheckEditor();
		this.chkInInsurance = new UltraCheckEditor();
		this.chkIsPrecent = new UltraCheckEditor();
		this.txtValue = new UltraTextEditor();
		this.lblValue = new UltraLabel();
		this.chkExtraTime = new UltraCheckEditor();
		this.chkMissions = new UltraCheckEditor();
		this.txtMinValueH = new UltraTextEditor();
		this.ultraLabel1 = new UltraLabel();
		this.chkLeavePermissionDays = new UltraCheckEditor();
		this.chkVacationDays = new UltraCheckEditor();
		this.txtMaxValue = new UltraTextEditor();
		this.ultraLabel2 = new UltraLabel();
		this.chkPenaltyDays = new UltraCheckEditor();
		this.chkDelayDays = new UltraCheckEditor();
		this.chkAbsentDays = new UltraCheckEditor();
		this.ultraLabel3 = new UltraLabel();
		this.ultraLabel4 = new UltraLabel();
		this.ultraLabel5 = new UltraLabel();
		this.ultraLabel6 = new UltraLabel();
		this.ultraLabel7 = new UltraLabel();
		this.chkInLeavePermission = new UltraCheckEditor();
		this.chkIsSalary = new UltraCheckEditor();
		this.chkInPenalty = new UltraCheckEditor();
		this.chkInAbsent = new UltraCheckEditor();
		this.chkInVacation = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInInsurance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsPrecent).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkExtraTime).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkMissions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMinValueH).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkLeavePermissionDays).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkVacationDays).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkPenaltyDays).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkDelayDays).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAbsentDays).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInLeavePermission).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsSalary).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInPenalty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInAbsent).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInVacation).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.ULGData, "ULGData");
		((UltraGridBase)base.ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
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
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
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
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val8, "appearance11");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance9");
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val9;
		base.cboTransactionBranch.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboTransactionBranch.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.pnlCheckType, "pnlCheckType");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val10, "appearance12");
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val10;
		resources.ApplyResources(this.pnlCheckType.ClientArea, "pnlCheckType.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsYearly);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsMonthly);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbisQuarterly);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsSixMonth);
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
		resources.ApplyResources(this.rbIsYearly, "rbIsYearly");
		this.rbIsYearly.BackColor = System.Drawing.Color.Transparent;
		this.rbIsYearly.Name = "rbIsYearly";
		this.rbIsYearly.TabStop = true;
		this.rbIsYearly.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.rbIsMonthly, "rbIsMonthly");
		this.rbIsMonthly.BackColor = System.Drawing.Color.Transparent;
		this.rbIsMonthly.Name = "rbIsMonthly";
		this.rbIsMonthly.TabStop = true;
		this.rbIsMonthly.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.rbisQuarterly, "rbisQuarterly");
		this.rbisQuarterly.BackColor = System.Drawing.Color.Transparent;
		this.rbisQuarterly.Name = "rbisQuarterly";
		this.rbisQuarterly.TabStop = true;
		this.rbisQuarterly.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.rbIsSixMonth, "rbIsSixMonth");
		this.rbIsSixMonth.BackColor = System.Drawing.Color.Transparent;
		this.rbIsSixMonth.Name = "rbIsSixMonth";
		this.rbIsSixMonth.TabStop = true;
		this.rbIsSixMonth.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.txtArabicName, "txtArabicName");
		((System.Windows.Forms.Control)(object)this.txtArabicName).Name = "txtArabicName";
		resources.ApplyResources(this.lblArabicName, "lblArabicName");
		this.lblArabicName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArabicName).Name = "lblArabicName";
		((ControlBase)this.lblArabicName).WrapText = false;
		resources.ApplyResources(this.txtEnglishName, "txtEnglishName");
		((System.Windows.Forms.Control)(object)this.txtEnglishName).Name = "txtEnglishName";
		resources.ApplyResources(this.lblEnglishName, "lblEnglishName");
		this.lblEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEnglishName).Name = "lblEnglishName";
		((ControlBase)this.lblEnglishName).WrapText = false;
		resources.ApplyResources(this.chkInTax, "chkInTax");
		((System.Windows.Forms.Control)(object)this.chkInTax).Name = "chkInTax";
		resources.ApplyResources(this.chkInInsurance, "chkInInsurance");
		((System.Windows.Forms.Control)(object)this.chkInInsurance).Name = "chkInInsurance";
		resources.ApplyResources(this.chkIsPrecent, "chkIsPrecent");
		((System.Windows.Forms.Control)(object)this.chkIsPrecent).Name = "chkIsPrecent";
		resources.ApplyResources(this.txtValue, "txtValue");
		((System.Windows.Forms.Control)(object)this.txtValue).Name = "txtValue";
		((System.Windows.Forms.Control)(object)this.txtValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumeric_KeyPress);
		resources.ApplyResources(this.lblValue, "lblValue");
		this.lblValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblValue).Name = "lblValue";
		((ControlBase)this.lblValue).WrapText = false;
		resources.ApplyResources(this.chkExtraTime, "chkExtraTime");
		((System.Windows.Forms.Control)(object)this.chkExtraTime).Name = "chkExtraTime";
		resources.ApplyResources(this.chkMissions, "chkMissions");
		((System.Windows.Forms.Control)(object)this.chkMissions).Name = "chkMissions";
		resources.ApplyResources(this.txtMinValueH, "txtMinValueH");
		((System.Windows.Forms.Control)(object)this.txtMinValueH).Name = "txtMinValueH";
		((System.Windows.Forms.Control)(object)this.txtMinValueH).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		resources.ApplyResources(this.chkLeavePermissionDays, "chkLeavePermissionDays");
		((System.Windows.Forms.Control)(object)this.chkLeavePermissionDays).Name = "chkLeavePermissionDays";
		resources.ApplyResources(this.chkVacationDays, "chkVacationDays");
		((System.Windows.Forms.Control)(object)this.chkVacationDays).Name = "chkVacationDays";
		resources.ApplyResources(this.txtMaxValue, "txtMaxValue");
		((System.Windows.Forms.Control)(object)this.txtMaxValue).Name = "txtMaxValue";
		((System.Windows.Forms.Control)(object)this.txtMaxValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumeric_KeyPress);
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.chkPenaltyDays, "chkPenaltyDays");
		((System.Windows.Forms.Control)(object)this.chkPenaltyDays).Name = "chkPenaltyDays";
		resources.ApplyResources(this.chkDelayDays, "chkDelayDays");
		((System.Windows.Forms.Control)(object)this.chkDelayDays).Name = "chkDelayDays";
		resources.ApplyResources(this.chkAbsentDays, "chkAbsentDays");
		((System.Windows.Forms.Control)(object)this.chkAbsentDays).Name = "chkAbsentDays";
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		this.ultraLabel4.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		((ControlBase)this.ultraLabel4).WrapText = false;
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
		resources.ApplyResources(this.chkInLeavePermission, "chkInLeavePermission");
		((System.Windows.Forms.Control)(object)this.chkInLeavePermission).Name = "chkInLeavePermission";
		resources.ApplyResources(this.chkIsSalary, "chkIsSalary");
		((System.Windows.Forms.Control)(object)this.chkIsSalary).Name = "chkIsSalary";
		resources.ApplyResources(this.chkInPenalty, "chkInPenalty");
		((System.Windows.Forms.Control)(object)this.chkInPenalty).Name = "chkInPenalty";
		resources.ApplyResources(this.chkInAbsent, "chkInAbsent");
		((System.Windows.Forms.Control)(object)this.chkInAbsent).Name = "chkInAbsent";
		resources.ApplyResources(this.chkInVacation, "chkInVacation");
		((System.Windows.Forms.Control)(object)this.chkInVacation).Name = "chkInVacation";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkInVacation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkInAbsent);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkInPenalty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsSalary);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkInLeavePermission);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel7);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtMaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtMinValueH);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsPrecent);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkInInsurance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAbsentDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkDelayDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkVacationDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkMissions);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkPenaltyDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkLeavePermissionDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkExtraTime);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkInTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArabicName);
		base.Name = "frmMotivations";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkInTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkExtraTime, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkLeavePermissionDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkPenaltyDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkMissions, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkVacationDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkDelayDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAbsentDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkInInsurance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsPrecent, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtMinValueH, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtMaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel5, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel7, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel6, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel4, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkInLeavePermission, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsSalary, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkInPenalty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkInAbsent, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkInVacation, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInInsurance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsPrecent).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkExtraTime).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkMissions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMinValueH).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkLeavePermissionDays).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkVacationDays).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkPenaltyDays).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkDelayDays).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAbsentDays).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInLeavePermission).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsSalary).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInPenalty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInAbsent).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInVacation).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
