using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Alerts;
using BusinessLayer.Privilege;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Alerts.Transactions;

public class frmUserReportsAlerts : frmDetails
{
	private DataTable dtUsers;

	private IContainer components = null;

	public frmUserReportsAlerts()
	{
		InitializeComponent();
		((Control)(object)lblHeader).Text = (GlobalVariables.IsArabic ? "إسم المستخدم" : "User");
		((Control)(object)btnNext).Visible = false;
		((Control)(object)btnPriveous).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
	}

	public override void PrepareData()
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboHeader, dtUsers, "User_ID", "UserName");
		((TextEditorControlBase)cboHeader).Value = GlobalVariables.UserID;
		((Control)(object)lblHeader).Visible = false;
		((Control)(object)cboHeader).Visible = false;
		ULGData.DoubleClickRow += new DoubleClickRowEventHandler(ULGData_DoubleClickRow);
	}

	private void ULGData_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
	{
		string empty = string.Empty;
		DateTime now = DateTime.Now;
		DateTime now2 = DateTime.Now;
		try
		{
			empty = (GlobalVariables.IsArabic ? e.Row.Cells["RepAr"].OriginalValue.ToString() : e.Row.Cells["RepEn"].OriginalValue.ToString());
			now = (DateTime)e.Row.Cells["WorkingDateFrom"].OriginalValue;
			now2 = (DateTime)e.Row.Cells["WorkingDateTo"].OriginalValue;
		}
		catch (Exception ex)
		{
			GlobalVariables.InformationMB.Show("حدث خطأ\n" + ex.Message, "Error Occurred\n" + ex.Message);
			return;
		}
		try
		{
			OpenTargetedReport(empty, now, now2);
		}
		catch (Exception ex2)
		{
			GlobalVariables.InformationMB.Show("تأكد من إسم ومسار التقرير\n" + ex2.Message, "Check the report name and path\n" + ex2.Message);
		}
	}

	private void OpenTargetedReport(string ReportName, DateTime fromdate, DateTime todate)
	{
		GlobalVariables.ReportDocument = new ReportDocument();
		GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + ReportName);
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@FromDate", fromdate);
		GlobalVariables.ReportDocument.SetParameterValue("@ToDate", todate);
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
		GlobalVariables.ReportDocument = null;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserReportAlertID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ALR_ReportID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RepAr"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RepEn"].Hidden = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReportNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "إسم التقرير" : "Report Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReportNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReportNameAr"].Hidden = !GlobalVariables.IsArabic;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReportNameAr"].CellActivation = (Activation)3;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReportNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "إسم التقرير" : "Report Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReportNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReportNameEn"].Hidden = GlobalVariables.IsArabic;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReportNameEn"].CellActivation = (Activation)3;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DescriptionAr"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DescriptionEn"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InterestedUserID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDisplayed"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsOpened"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsMarked"].Hidden = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DataResult"].Header).Caption = (GlobalVariables.IsArabic ? "عدد السجلات" : "Record Count");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DataResult"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DataResult"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DataResult"].CellActivation = (Activation)3;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkingDateFrom"].Header).Caption = (GlobalVariables.IsArabic ? "من تاريخ" : "From Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkingDateFrom"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkingDateFrom"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkingDateFrom"].CellActivation = (Activation)3;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkingDateFrom"].Format = "dd/MM/yyyy hh:mm:ss tt";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkingDateTo"].Header).Caption = (GlobalVariables.IsArabic ? "إلي تاريخ" : "To Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkingDateTo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkingDateTo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkingDateTo"].CellActivation = (Activation)3;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkingDateTo"].Format = "dd/MM/yyyy hh:mm:ss tt";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Deleted"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
	}

	public override void DisplayData()
	{
		base.DisplayData();
		DataView defaultView = UserReportsAlerts.SelectByUserID(GlobalVariables.UserID, "Null", "Null", "Null", IsFromServer: false).DefaultView;
		defaultView.Sort = "UserReportAlertID DESC";
		dtDetails = defaultView.ToTable();
		InitGrid();
	}

	public override bool ValidateData()
	{
		return true;
	}

	public override void SaveData()
	{
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
	}

	public override void btnHeaderSearch_Click(object sender, EventArgs e)
	{
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
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
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
		((System.Windows.Forms.Control)(object)base.ULGData).Location = new System.Drawing.Point(8, 38);
		((System.Windows.Forms.Control)(object)base.ULGData).Size = new System.Drawing.Size(985, 460);
		((AppearanceBase)val8).FontData.BoldAsString = "True";
		((AppearanceBase)val8).FontData.Name = "Arial";
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		((TextEditorControlBase)base.cboHeader).Appearance = (AppearanceBase)(object)val8;
		base.cboHeader.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboHeader.DropDownListAlignment = (DropDownListAlignment)2;
		((System.Windows.Forms.Control)(object)base.cboHeader).Location = new System.Drawing.Point(414, 68);
		((System.Windows.Forms.Control)(object)base.cboHeader).Size = new System.Drawing.Size(285, 24);
		((AppearanceBase)val9).FontData.BoldAsString = "True";
		((AppearanceBase)val9).FontData.Name = "Arial";
		((ControlBase)base.lblHeader).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)base.lblHeader).Location = new System.Drawing.Point(282, 72);
		((System.Windows.Forms.Control)(object)base.lblHeader).Size = new System.Drawing.Size(52, 17);
		base.ClientSize = new System.Drawing.Size(1000, 500);
		base.Name = "frmUserFormsAlerts";
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
