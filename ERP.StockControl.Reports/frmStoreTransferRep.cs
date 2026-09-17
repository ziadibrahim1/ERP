using System;
using System.ComponentModel;
using System.Windows.Forms;
using BusinessLayer.General;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.StockControl.Transactions;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;

namespace ERP.StockControl.Reports;

public class frmStoreTransferRep : frmReportTree2010
{
	private IContainer components = null;

	public frmStoreTransferRep()
	{
		dtItems2 = BusinessLayer.General.Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItems2ParentIDCol = "ParentID";
		TreeItems2IDCol = "BranchID";
		TreeItems2NameCol = "BranchName";
		TreeItems2IsMainCol = "IsMain";
		InitializeComponent();
	}

	public override void FormLoad()
	{
		if (dtItems2 != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeItems2, dtItems2, TreeItems2IDCol, TreeItems2NameCol);
		}
		((UltraToggleEditorBase)chkAll2).Checked = true;
	}

	public override string GetReportName()
	{
		if (((TextEditorControlBase)cboReportType).Value.ToString() == "1")
		{
			if (((UltraToggleEditorBase)chkWithLogo).Checked)
			{
				return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_SC_StoreTransferVouchersSummary_Deliverd_A.rpt" : "Rep_SC_StoreTransferVouchersSummary_Deliverd_E.rpt");
			}
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_SC_StoreTransferVouchersSummary_Deliverd_A_nologo.rpt" : "Rep_SC_StoreTransferVouchersSummary_Deliverd_E_nologo.rpt");
		}
		if (((TextEditorControlBase)cboReportType).Value.ToString() == "2")
		{
			if (((UltraToggleEditorBase)chkWithLogo).Checked)
			{
				return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_SC_StoreTransferVouchersSummary_NotDeliverd_A.rpt" : "Rep_SC_StoreTransferVouchersSummary_NotDeliverd_E.rpt");
			}
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_SC_StoreTransferVouchersSummary_NotDeliverd_A_nologo.rpt" : "Rep_SC_StoreTransferVouchersSummary_NotDeliverd_E_nologo.rpt");
		}
		return base.GetReportName();
	}

	public override void ShowReport()
	{
		GetBranches();
		GetItems();
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@SourceBranchIDs", Branches);
		GlobalVariables.ReportDocument.SetParameterValue("@DestinationBranchIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@FromDate", dtpFromDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", text);
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
			string text = GroupNamePath.Substring(0, GroupNamePath.IndexOf("StoreTransferVoucherID") + 22);
			if (text.IndexOf("StoreTransferVoucherID") >= 0)
			{
				string s = GroupNamePath.Replace(text, "").Replace(",", "").Replace("[", "")
					.Replace("]", "");
				frmStoreTransferVouchers frmStoreTransferVouchers2 = new frmStoreTransferVouchers(int.Parse(s));
				frmStoreTransferVouchers2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmStoreTransferVouchers2.lblTitle).Text = (GlobalVariables.IsArabic ? "اذونــات الــتــحــويــل" : "Store Transfer Vouchers");
				frmStoreTransferVouchers2.ShowDialog();
				RefreshReport(frmViewer);
			}
		}
	}

	public void RefreshReport(frmReporViwer frmViewer)
	{
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		int currentPageNumber = frmViewer.crvReportViewer.GetCurrentPageNumber();
		GlobalVariables.ReportDocument = new ReportDocument();
		GlobalVariables.ReportDocument.Load((dtReports.Rows.Count == 0) ? GetReportName() : (GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep" + (((UltraToggleEditorBase)chkIsArabic).Checked ? "_A" : "_E") + (((UltraToggleEditorBase)chkWithLogo).Checked ? "" : "_nologo")].ToString()));
		GlobalVariables.ReportDocument.SetParameterValue("@SourceBranchIDs", Branches);
		GlobalVariables.ReportDocument.SetParameterValue("@DestinationBranchIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@FromDate", dtpFromDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", text);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked);
		frmViewer.frmParent = this;
		frmViewer.ConfigureReport();
		frmViewer.BringToFront();
		frmViewer.Refresh();
		frmViewer.crvReportViewer.ShowNthPage(currentPageNumber);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.Reports.frmStoreTransferRep));
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
		((System.ComponentModel.ISupportInitialize)base.cboSetting).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtReports).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtFormSetting).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.btnPreview, "btnPreview");
		resources.ApplyResources(base.ultraLabel1, "ultraLabel1");
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		resources.ApplyResources(base.ultraLabel2, "ultraLabel2");
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		resources.ApplyResources(base.dtpFromDate, "dtpFromDate");
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance2.FontData");
		resources.ApplyResources(val, "appearance2");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val;
		base.dtpFromDate.DateTime = new System.DateTime(2018, 9, 19, 0, 0, 0, 0);
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2018, 9, 19, 0, 0, 0, 0);
		resources.ApplyResources(base.dtpToDate, "dtpToDate");
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance3.FontData");
		resources.ApplyResources(val2, "appearance3");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		base.dtpToDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpToDate.DateTime = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		base.dtpToDate.MaskInput = "dd/mm/yyyy";
		base.dtpToDate.Value = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		resources.ApplyResources(base.chkAllBranches, "chkAllBranches");
		((UltraControlBase)base.chkAllBranches).UseAppStyling = false;
		resources.ApplyResources(base.clbBranches, "clbBranches");
		resources.ApplyResources(base.chkWithLogo, "chkWithLogo");
		((UltraControlBase)base.chkWithLogo).UseAppStyling = false;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.btnClose, "btnClose");
		resources.ApplyResources(base.lblReportType, "lblReportType");
		((UltraControlBase)base.lblReportType).UseAppStyling = false;
		resources.ApplyResources(base.cboReportType, "cboReportType");
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance6.FontData");
		resources.ApplyResources(val3, "appearance6");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.cboReportType).Appearance = (AppearanceBase)(object)val3;
		base.cboReportType.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboReportType.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.chkAll, "chkAll");
		resources.ApplyResources(base.chkAll2, "chkAll2");
		resources.ApplyResources(base.TreeItems, "TreeItems");
		resources.ApplyResources(base.TreeItems2, "TreeItems2");
		resources.ApplyResources(base.btnItemsSearch, "btnItemsSearch");
		resources.ApplyResources(base.btnItems2Search, "btnItems2Search");
		resources.ApplyResources(base.chkIsArabic, "chkIsArabic");
		resources.ApplyResources(base.txtItems, "txtItems");
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance8.FontData");
		resources.ApplyResources(val4, "appearance8");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtItems).Appearance = (AppearanceBase)(object)val4;
		resources.ApplyResources(base.txtItems2, "txtItems2");
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance5.FontData");
		resources.ApplyResources(val5, "appearance5");
		((SubObjectBase)val5).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtItems2).Appearance = (AppearanceBase)(object)val5;
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.btnNew, "btnNew");
		resources.ApplyResources(base.btnUpdate, "btnUpdate");
		resources.ApplyResources(base.btnDelete, "btnDelete");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.cboSetting, "cboSetting");
		resources.ApplyResources(base.lblSettingName, "lblSettingName");
		resources.ApplyResources(base.btnSaveSetting, "btnSaveSetting");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this, "$this");
		base.Name = "frmStoreTransferRep";
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
		((System.ComponentModel.ISupportInitialize)base.cboSetting).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtReports).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtFormSetting).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
