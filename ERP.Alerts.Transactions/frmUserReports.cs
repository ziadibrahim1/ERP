using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Alerts;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Alerts.Transactions;

public class frmUserReports : frmDetails
{
	private DataTable dtUsers;

	private IContainer components = null;

	public frmUserReports()
	{
		InitializeComponent();
		((Control)(object)lblHeader).Text = (GlobalVariables.IsArabic ? "إسم المستخدم" : "User");
		((Control)(object)btnNext).Visible = false;
		((Control)(object)btnPriveous).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
	}

	public override void PrepareData()
	{
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboHeader, dtUsers, "User_ID", "UserName");
		((TextEditorControlBase)cboHeader).Value = GlobalVariables.UserID;
		((Control)(object)lblHeader).Visible = false;
		((Control)(object)cboHeader).Visible = false;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ALR_UserReportID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ALR_ReportID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReportName"].Hidden = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Rep"].Header).Caption = (GlobalVariables.IsArabic ? "إسم التقرير" : "Report Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Rep"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Rep"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Rep"].CellActivation = (Activation)3;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Description"].Header).Caption = (GlobalVariables.IsArabic ? "الوصف" : "Description");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Description"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Description"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Description"].CellActivation = (Activation)3;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MinPeriodHours"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChkQryProcName"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InterestedUserID"].Hidden = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NotificationRequired"].Header).Caption = (GlobalVariables.IsArabic ? "الإشعار" : "Notification Required");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NotificationRequired"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NotificationRequired"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Deleted"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
	}

	public override void DisplayData()
	{
		base.DisplayData();
		dtDetails = UserReports.SelectByUserID(((TextEditorControlBase)cboHeader).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		InitGrid();
	}

	public override bool ValidateData()
	{
		return true;
	}

	public override void SaveData()
	{
		try
		{
			foreach (DataRow row in (((UltraGridBase)ULGData).DataSource as DataTable).Rows)
			{
				UserReports.Insert_Update(row["ALR_UserReportID"].ToString(), row["ALR_ReportID"].ToString(), ((TextEditorControlBase)cboHeader).Value.ToString(), Convert.ToInt16(Convert.ToBoolean(row["NotificationRequired"])).ToString(), "1", "0", GlobalVariables.UserID, IsFromServer: false);
			}
			dtDetails = UserReports.SelectByUserID(((TextEditorControlBase)cboHeader).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			InitGrid();
		}
		catch
		{
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
			SaveError = true;
		}
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
		base.Name = "frmUserForms";
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
