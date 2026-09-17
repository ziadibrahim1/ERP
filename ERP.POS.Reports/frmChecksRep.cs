using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.POS;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.POS.Transactions;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;

namespace ERP.POS.Reports;

public class frmChecksRep : frmReportTree2010
{
	private IContainer components = null;

	public frmChecksRep()
	{
		InitializeComponent();
		TreeItems2ParentIDCol = "ParentID";
		TreeItems2IDCol = "RoomID";
		TreeItems2NameCol = "RoomName";
		TreeItems2IsMainCol = "IsMain";
		cboReportType.Items.Clear();
		cboReportType.Items.Add((object)1, GlobalVariables.IsArabic ? "شيكات بتفاصيل انواع التصنيف" : "Check Details With Item Type");
		cboReportType.Items.Add((object)2, GlobalVariables.IsArabic ? "شيكات تفصيلى" : "Check Details ");
		cboReportType.Items.Add((object)3, GlobalVariables.IsArabic ? "شيكات إجمالي" : "Check Totals");
		cboReportType.Items.Add((object)4, GlobalVariables.IsArabic ? "شيكات إجمالي مجمع بالصاله" : "Check Totals Grouped By Room");
		cboReportType.Items.Add((object)5, GlobalVariables.IsArabic ? "شيكات محذوفه" : "Deleted Checks ");
		cboReportType.Items.Add((object)6, GlobalVariables.IsArabic ? "متوسط الفرد بالشيك" : "person Average per Check");
		cboReportType.SelectedIndex = 0;
	}

	public override void FormLoad()
	{
	}

	public override void FillData()
	{
		GetBranches();
		dtItems2 = Rooms.SelectWithoutImage("-1", Branches, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (dtItems2 != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeItems2, dtItems2, TreeItems2IDCol, TreeItems2NameCol);
		}
		((UltraToggleEditorBase)chkAll2).Checked = false;
	}

	public override string GetReportName()
	{
		if (((TextEditorControlBase)cboReportType).Value.ToString() == "1")
		{
			if (((UltraToggleEditorBase)chkWithLogo).Checked)
			{
				return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_POS_Checks_SummaryByItemsType_A.rpt" : "Rep_POS_Checks_SummaryByItemsType_E.rpt");
			}
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_POS_Checks_SummaryByItemsType_A_nologo.rpt" : "Rep_POS_Checks_SummaryByItemsType_E_nologo.rpt");
		}
		if (((TextEditorControlBase)cboReportType).Value.ToString() == "2")
		{
			if (((UltraToggleEditorBase)chkWithLogo).Checked)
			{
				return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_POS_Checks_Details_A.rpt" : "Rep_POS_Checks_Details_E.rpt");
			}
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_POS_Checks_Details_A.rpt" : "Rep_POS_Checks_Details_E.rpt");
		}
		if (((TextEditorControlBase)cboReportType).Value.ToString() == "3")
		{
			if (((UltraToggleEditorBase)chkWithLogo).Checked)
			{
				return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_POS_Checks_Summary_A.rpt" : "Rep_POS_Checks_Summary_E.rpt");
			}
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_POS_Checks_Summary_A_nologo.rpt" : "Rep_POS_Checks_Summary_E_nologo.rpt");
		}
		if (((TextEditorControlBase)cboReportType).Value.ToString() == "4")
		{
			if (((UltraToggleEditorBase)chkWithLogo).Checked)
			{
				return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_POS_Checks_SummaryByRoom_A.rpt" : "Rep_POS_Checks_SummaryByRoom_E.rpt");
			}
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_POS_Checks_SummaryByRoom_A_nologo.rpt" : "Rep_POS_Checks_SummaryByRoom_E_nologo.rpt");
		}
		if (((TextEditorControlBase)cboReportType).Value.ToString() == "5")
		{
			if (((UltraToggleEditorBase)chkWithLogo).Checked)
			{
				return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_POS_Checks_Deleted_A.rpt" : "Rep_POS_Checks_Deleted_E.rpt");
			}
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_POS_Checks_Deleted_A.rpt" : "Rep_POS_Checks_Deleted_E.rpt");
		}
		if (((TextEditorControlBase)cboReportType).Value.ToString() == "6")
		{
			if (((UltraToggleEditorBase)chkWithLogo).Checked)
			{
				return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_POS_Checks_Summary_ByPersonAvg_A.rpt" : "Rep_POS_Checks_Summary_ByPersonAvg_E.rpt");
			}
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_POS_Checks_Summary_ByPersonAvg_A_nologo.rpt" : "Rep_POS_Checks_Summary_ByPersonAvg_E_nologo.rpt");
		}
		return base.GetReportName();
	}

	public override void ShowReport()
	{
		GetItems();
		if (Items2.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى صالة ليتم عرضعا ", "There is no choosen Hall to be shown in the report, please check Hall to be shown in report");
			return;
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@FromDate", dtpFromDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches);
		GlobalVariables.ReportDocument.SetParameterValue("@RoomIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked);
		frmReporViwer2.Tag = base.Tag;
		frmReporViwer2.MdiParent = base.MdiParent;
		frmReporViwer2.TopLevel = false;
		frmReporViwer2.Parent = base.Parent;
		frmReporViwer2.Width = base.Parent.Width;
		frmReporViwer2.Height = base.Parent.Height;
		frmReporViwer2.frmParent = this;
		frmReporViwer2.Show();
		frmReporViwer2.BringToFront();
	}

	public override void ReportDoubleClick(string GroupNamePath, frmReporViwer frmViewer)
	{
		if (GroupNamePath != "")
		{
			string text = GroupNamePath.Substring(0, GroupNamePath.IndexOf("CheckID") + 7);
			if (text.IndexOf("CheckID") >= 0)
			{
				string rowID = GroupNamePath.Replace(text, "").Replace(",", "").Replace("[", "")
					.Replace("]", "");
				frmChecks frmChecks2 = new frmChecks();
				frmChecks2.RowID = rowID;
				frmChecks2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)lblTitle).Text = (GlobalVariables.IsArabic ? "الشيكات" : "Checks");
				frmChecks2.ShowDialog();
				RefreshReport(frmViewer);
			}
		}
	}

	public void RefreshReport(frmReporViwer frmViewer)
	{
		int currentPageNumber = frmViewer.crvReportViewer.GetCurrentPageNumber();
		GlobalVariables.ReportDocument = new ReportDocument();
		GlobalVariables.ReportDocument.Load((dtReports.Rows.Count == 0) ? GetReportName() : (GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep" + (((UltraToggleEditorBase)chkIsArabic).Checked ? "_A" : "_E") + (((UltraToggleEditorBase)chkWithLogo).Checked ? "" : "_nologo")].ToString()));
		GlobalVariables.ReportDocument.SetParameterValue("@FromDate", dtpFromDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches);
		GlobalVariables.ReportDocument.SetParameterValue("@RoomIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked);
		frmViewer.frmParent = this;
		frmViewer.ConfigureReport();
		frmViewer.BringToFront();
		frmViewer.Refresh();
		frmViewer.crvReportViewer.ShowNthPage(currentPageNumber);
	}

	public override void btnItems2Search_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.RoomsReport(Branches, IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems2.GetNodeByKey(dtSearchResult.Rows[i]["RoomID"].ToString()).CheckedState = CheckState.Checked;
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
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		((System.ComponentModel.ISupportInitialize)base.dtpFromDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtpToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.chkAllBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.chkWithLogo).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboReportType).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.chkAll).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.chkAll2).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.TreeItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.TreeItems2).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.chkIsArabic).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtItems2).BeginInit();
		base.SuspendLayout();
		((System.Windows.Forms.Control)(object)base.ultraLabel1).Size = new System.Drawing.Size(39, 17);
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)base.ultraLabel2).Size = new System.Drawing.Size(22, 17);
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		((AppearanceBase)val).FontData.Name = "Tahoma";
		((AppearanceBase)val).ForeColor = System.Drawing.Color.Navy;
		((AppearanceBase)val).TextHAlignAsString = "Center";
		((AppearanceBase)val).TextVAlignAsString = "Middle";
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val;
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		((System.Windows.Forms.Control)(object)base.dtpFromDate).Size = new System.Drawing.Size(120, 24);
		((AppearanceBase)val2).FontData.Name = "Tahoma";
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		((AppearanceBase)val2).TextHAlignAsString = "Center";
		((AppearanceBase)val2).TextVAlignAsString = "Middle";
		base.dtpToDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpToDate.DateTime = new System.DateTime(2011, 9, 8, 23, 59, 59, 0);
		base.dtpToDate.MaskInput = "dd/mm/yyyy";
		((System.Windows.Forms.Control)(object)base.dtpToDate).Size = new System.Drawing.Size(120, 24);
		base.dtpToDate.Value = new System.DateTime(2011, 9, 8, 23, 59, 59, 0);
		((System.Windows.Forms.Control)(object)base.chkAllBranches).Size = new System.Drawing.Size(49, 20);
		((UltraControlBase)base.chkAllBranches).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)base.chkWithLogo).Size = new System.Drawing.Size(89, 20);
		((UltraControlBase)base.chkWithLogo).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)base.lblReportType).Size = new System.Drawing.Size(85, 17);
		((UltraControlBase)base.lblReportType).UseAppStyling = false;
		((AppearanceBase)val3).FontData.Name = "Tahoma";
		((AppearanceBase)val3).ForeColor = System.Drawing.Color.Navy;
		((TextEditorControlBase)base.cboReportType).Appearance = (AppearanceBase)(object)val3;
		base.cboReportType.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboReportType.DropDownListAlignment = (DropDownListAlignment)2;
		((System.Windows.Forms.Control)(object)base.cboReportType).Size = new System.Drawing.Size(362, 24);
		((System.Windows.Forms.Control)(object)base.chkAll).Location = new System.Drawing.Point(42, 120);
		((System.Windows.Forms.Control)(object)base.chkAll).Size = new System.Drawing.Size(49, 20);
		((System.Windows.Forms.Control)(object)base.chkAll).Visible = false;
		((System.Windows.Forms.Control)(object)base.chkAll2).Location = new System.Drawing.Point(476, 121);
		((System.Windows.Forms.Control)(object)base.chkAll2).Size = new System.Drawing.Size(49, 20);
		((System.Windows.Forms.Control)(object)base.TreeItems).Location = new System.Drawing.Point(12, 146);
		((System.Windows.Forms.Control)(object)base.TreeItems).Size = new System.Drawing.Size(134, 277);
		((System.Windows.Forms.Control)(object)base.TreeItems).Visible = false;
		((System.Windows.Forms.Control)(object)base.TreeItems2).Location = new System.Drawing.Point(255, 146);
		((System.Windows.Forms.Control)(object)base.TreeItems2).Size = new System.Drawing.Size(489, 278);
		((System.Windows.Forms.Control)(object)base.btnItemsSearch).Location = new System.Drawing.Point(119, 427);
		((System.Windows.Forms.Control)(object)base.btnItemsSearch).Visible = false;
		((System.Windows.Forms.Control)(object)base.btnItems2Search).Location = new System.Drawing.Point(717, 428);
		((System.Windows.Forms.Control)(object)base.chkIsArabic).Size = new System.Drawing.Size(65, 20);
		((AppearanceBase)val4).FontData.Name = "Tahoma";
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Navy;
		((AppearanceBase)val4).TextHAlignAsString = "Center";
		((AppearanceBase)val4).TextVAlignAsString = "Middle";
		((TextEditorControlBase)base.txtItems).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)base.txtItems).Location = new System.Drawing.Point(12, 429);
		((System.Windows.Forms.Control)(object)base.txtItems).Size = new System.Drawing.Size(101, 25);
		((System.Windows.Forms.Control)(object)base.txtItems).Visible = false;
		((AppearanceBase)val5).FontData.Name = "Tahoma";
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Navy;
		((AppearanceBase)val5).TextHAlignAsString = "Center";
		((AppearanceBase)val5).TextVAlignAsString = "Middle";
		((TextEditorControlBase)base.txtItems2).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)base.txtItems2).Location = new System.Drawing.Point(255, 429);
		((System.Windows.Forms.Control)(object)base.txtItems2).Size = new System.Drawing.Size(460, 25);
		base.ClientSize = new System.Drawing.Size(1000, 500);
		base.Name = "frmChecksRep";
		((System.ComponentModel.ISupportInitialize)base.dtpFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtpToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)base.chkAllBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)base.chkWithLogo).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboReportType).EndInit();
		((System.ComponentModel.ISupportInitialize)base.chkAll).EndInit();
		((System.ComponentModel.ISupportInitialize)base.chkAll2).EndInit();
		((System.ComponentModel.ISupportInitialize)base.TreeItems).EndInit();
		((System.ComponentModel.ISupportInitialize)base.TreeItems2).EndInit();
		((System.ComponentModel.ISupportInitialize)base.chkIsArabic).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtItems).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtItems2).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
