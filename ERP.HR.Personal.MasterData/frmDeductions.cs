using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.HR;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.HR.Personal.MasterData;

public class frmDeductions : frmGrid
{
	private DataTable dtReports;

	private IContainer components = null;

	private UltraTextEditor txtArabicName;

	private UltraLabel lblArabicName;

	private UltraTextEditor txtEnglishName;

	private UltraLabel lblEnglishName;

	private UltraCheckEditor chkIsMonthly;

	private UltraCheckEditor chkInTax;

	private UltraCheckEditor chkInLatency;

	private UltraCheckEditor chkInOvertime;

	private UltraCheckEditor chkInVacation;

	private UltraCheckEditor chkInPenalty;

	private UltraCheckEditor chkInInsurance;

	private UltraCheckEditor chkInBonus;

	private UltraCheckEditor chkInAbsent;

	private UltraCheckEditor chkInServicePrecent;

	private UltraCheckEditor chkIsPrecent;

	private UltraTextEditor txtValue;

	private UltraLabel lblValue;

	private UltraCheckEditor chkInLeavePermission;

	public frmDeductions()
	{
		InitializeComponent();
		TableName = "HR_Deductions";
		IDCol = "DeductionID";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEnglishName).ReadOnly = NavMode;
		((Control)(object)chkInAbsent).Enabled = !NavMode;
		((Control)(object)chkInBonus).Enabled = !NavMode;
		((Control)(object)chkInLeavePermission).Enabled = !NavMode;
		((Control)(object)chkInInsurance).Enabled = !NavMode;
		((Control)(object)chkInLatency).Enabled = !NavMode;
		((Control)(object)chkInOvertime).Enabled = !NavMode;
		((Control)(object)chkInPenalty).Enabled = !NavMode;
		((Control)(object)chkInTax).Enabled = !NavMode;
		((Control)(object)chkInVacation).Enabled = !NavMode;
		((Control)(object)chkIsMonthly).Enabled = !NavMode;
		((Control)(object)chkInServicePrecent).Enabled = !NavMode;
		((Control)(object)chkIsPrecent).Enabled = !NavMode;
		((EditorButtonControlBase)txtValue).ReadOnly = NavMode;
		((TextEditorControlBase)txtArabicName).Focus();
	}

	public override void ClearControls()
	{
		((TextEditorControlBase)txtArabicName).Clear();
		((TextEditorControlBase)txtEnglishName).Clear();
		((UltraToggleEditorBase)chkInAbsent).Checked = false;
		((UltraToggleEditorBase)chkInBonus).Checked = false;
		((UltraToggleEditorBase)chkInLeavePermission).Checked = false;
		((UltraToggleEditorBase)chkInInsurance).Checked = false;
		((UltraToggleEditorBase)chkInLatency).Checked = false;
		((UltraToggleEditorBase)chkInOvertime).Checked = false;
		((UltraToggleEditorBase)chkInPenalty).Checked = false;
		((UltraToggleEditorBase)chkInTax).Checked = false;
		((UltraToggleEditorBase)chkInVacation).Checked = false;
		((UltraToggleEditorBase)chkIsMonthly).Checked = false;
		((UltraToggleEditorBase)chkInServicePrecent).Checked = false;
		((UltraToggleEditorBase)chkIsPrecent).Checked = false;
		((Control)(object)txtValue).Text = "0";
	}

	public override void FillData()
	{
		dataTable = Deductions.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dataTable;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeductionNameAr"].Header).Caption = (GlobalVariables.IsArabic ? " اسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeductionNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeductionNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeductionNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "اسم  بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeductionNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeductionNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsMonthly"].Header).Caption = (GlobalVariables.IsArabic ? " دورى" : "periodic");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsMonthly"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsMonthly"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InPenalty"].Header).Caption = (GlobalVariables.IsArabic ? "جزاء" : "Penalty");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InPenalty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InPenalty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InAbsent"].Header).Caption = (GlobalVariables.IsArabic ? " غياب" : "Absence");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InAbsent"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InAbsent"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InVacation"].Header).Caption = (GlobalVariables.IsArabic ? "اجازة" : "Vacation");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InVacation"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InVacation"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InOvertime"].Header).Caption = (GlobalVariables.IsArabic ? " إضافى" : "Overtime");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InOvertime"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InOvertime"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InLatency"].Header).Caption = (GlobalVariables.IsArabic ? "تأخير" : "Lateness");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InLatency"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InLatency"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InTax"].Header).Caption = (GlobalVariables.IsArabic ? " ضريبة" : "Tax");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InTax"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InTax"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InInsurance"].Header).Caption = (GlobalVariables.IsArabic ? "تأمينات" : "Insurance");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InInsurance"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InInsurance"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InBonus"].Header).Caption = (GlobalVariables.IsArabic ? "مكافأة" : "Bonus");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InBonus"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InBonus"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InLeavePermission"].Header).Caption = (GlobalVariables.IsArabic ? "الأذونات" : "Leave Permission");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InLeavePermission"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InLeavePermission"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InServicePrecent"].Header).Caption = (GlobalVariables.IsArabic ? "رسم الخدمة" : "Service%");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InServicePrecent"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InServicePrecent"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Value"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة" : "Value");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Value"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Value"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtArabicName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["DeductionNameAr"].Value.ToString();
		((Control)(object)txtEnglishName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["DeductionNameEn"].Value.ToString();
		((UltraToggleEditorBase)chkInAbsent).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["InAbsent"].Value.ToString());
		((UltraToggleEditorBase)chkInBonus).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["InBonus"].Value.ToString());
		((UltraToggleEditorBase)chkInLeavePermission).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["InLeavePermission"].Value.ToString());
		((UltraToggleEditorBase)chkInInsurance).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["InInsurance"].Value.ToString());
		((UltraToggleEditorBase)chkInLatency).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["InLatency"].Value.ToString());
		((UltraToggleEditorBase)chkInOvertime).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["InOvertime"].Value.ToString());
		((UltraToggleEditorBase)chkInPenalty).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["InPenalty"].Value.ToString());
		((UltraToggleEditorBase)chkInTax).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["InTax"].Value.ToString());
		((UltraToggleEditorBase)chkInVacation).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["InVacation"].Value.ToString());
		((UltraToggleEditorBase)chkIsMonthly).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsMonthly"].Value.ToString());
		((UltraToggleEditorBase)chkInServicePrecent).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["InServicePrecent"].Value.ToString());
		((UltraToggleEditorBase)chkIsPrecent).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsPrecent"].Value.ToString());
		((Control)(object)txtValue).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["Value"].Value.ToString();
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال اسم الاستقطاع بالعربية", "Please Enter Deduction Arabic Name");
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		Deductions.Insert_Update("-1", ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, ((UltraToggleEditorBase)chkIsMonthly).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInPenalty).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInAbsent).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInVacation).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInOvertime).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInLatency).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInTax).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInInsurance).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInBonus).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInLeavePermission).Checked ? "1" : "0", "0", ((UltraToggleEditorBase)chkInServicePrecent).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsPrecent).Checked ? "1" : "0", (((Control)(object)txtValue).Text == "") ? "0" : ((Control)(object)txtValue).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
		FillData();
	}

	public override void UpdateData()
	{
		Deductions.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["DeductionID"].Value.ToString(), ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, ((UltraToggleEditorBase)chkIsMonthly).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInPenalty).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInAbsent).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInVacation).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInOvertime).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInLatency).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInTax).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInInsurance).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInBonus).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInLeavePermission).Checked ? "1" : "0", "0", ((UltraToggleEditorBase)chkInServicePrecent).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsPrecent).Checked ? "1" : "0", (((Control)(object)txtValue).Text == "") ? "0" : ((Control)(object)txtValue).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
		FillData();
	}

	public override void DeleteData()
	{
		Deductions.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["DeductionID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
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
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_HR_Deductions_A.rpt" : "Rep_HR_Deductions_E.rpt"));
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HR.Personal.MasterData.frmDeductions));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.txtArabicName = new UltraTextEditor();
		this.lblArabicName = new UltraLabel();
		this.txtEnglishName = new UltraTextEditor();
		this.lblEnglishName = new UltraLabel();
		this.chkIsMonthly = new UltraCheckEditor();
		this.chkInTax = new UltraCheckEditor();
		this.chkInLatency = new UltraCheckEditor();
		this.chkInOvertime = new UltraCheckEditor();
		this.chkInVacation = new UltraCheckEditor();
		this.chkInPenalty = new UltraCheckEditor();
		this.chkInInsurance = new UltraCheckEditor();
		this.chkInBonus = new UltraCheckEditor();
		this.chkInAbsent = new UltraCheckEditor();
		this.chkInServicePrecent = new UltraCheckEditor();
		this.chkIsPrecent = new UltraCheckEditor();
		this.txtValue = new UltraTextEditor();
		this.lblValue = new UltraLabel();
		this.chkInLeavePermission = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsMonthly).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInLatency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInOvertime).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInVacation).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInPenalty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInInsurance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInBonus).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInAbsent).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInServicePrecent).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsPrecent).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInLeavePermission).BeginInit();
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
		resources.ApplyResources(val8, "appearance10");
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
		resources.ApplyResources(this.chkIsMonthly, "chkIsMonthly");
		((System.Windows.Forms.Control)(object)this.chkIsMonthly).Name = "chkIsMonthly";
		resources.ApplyResources(this.chkInTax, "chkInTax");
		((System.Windows.Forms.Control)(object)this.chkInTax).Name = "chkInTax";
		resources.ApplyResources(this.chkInLatency, "chkInLatency");
		((System.Windows.Forms.Control)(object)this.chkInLatency).Name = "chkInLatency";
		resources.ApplyResources(this.chkInOvertime, "chkInOvertime");
		((System.Windows.Forms.Control)(object)this.chkInOvertime).Name = "chkInOvertime";
		resources.ApplyResources(this.chkInVacation, "chkInVacation");
		((System.Windows.Forms.Control)(object)this.chkInVacation).Name = "chkInVacation";
		resources.ApplyResources(this.chkInPenalty, "chkInPenalty");
		((System.Windows.Forms.Control)(object)this.chkInPenalty).Name = "chkInPenalty";
		resources.ApplyResources(this.chkInInsurance, "chkInInsurance");
		((System.Windows.Forms.Control)(object)this.chkInInsurance).Name = "chkInInsurance";
		resources.ApplyResources(this.chkInBonus, "chkInBonus");
		((System.Windows.Forms.Control)(object)this.chkInBonus).Name = "chkInBonus";
		resources.ApplyResources(this.chkInAbsent, "chkInAbsent");
		((System.Windows.Forms.Control)(object)this.chkInAbsent).Name = "chkInAbsent";
		resources.ApplyResources(this.chkInServicePrecent, "chkInServicePrecent");
		((System.Windows.Forms.Control)(object)this.chkInServicePrecent).Name = "chkInServicePrecent";
		resources.ApplyResources(this.chkIsPrecent, "chkIsPrecent");
		((System.Windows.Forms.Control)(object)this.chkIsPrecent).Name = "chkIsPrecent";
		resources.ApplyResources(this.txtValue, "txtValue");
		((System.Windows.Forms.Control)(object)this.txtValue).Name = "txtValue";
		resources.ApplyResources(this.lblValue, "lblValue");
		this.lblValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblValue).Name = "lblValue";
		((ControlBase)this.lblValue).WrapText = false;
		resources.ApplyResources(this.chkInLeavePermission, "chkInLeavePermission");
		((System.Windows.Forms.Control)(object)this.chkInLeavePermission).Name = "chkInLeavePermission";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkInLeavePermission);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsPrecent);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkInServicePrecent);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkInAbsent);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkInInsurance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkInBonus);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkInPenalty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkInVacation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkInOvertime);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkInLatency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkInTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsMonthly);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArabicName);
		base.Name = "frmDeductions";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsMonthly, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkInTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkInLatency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkInOvertime, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkInVacation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkInPenalty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkInBonus, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkInInsurance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkInAbsent, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkInServicePrecent, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsPrecent, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkInLeavePermission, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsMonthly).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInLatency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInOvertime).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInVacation).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInPenalty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInInsurance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInBonus).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInAbsent).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInServicePrecent).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsPrecent).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInLeavePermission).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
