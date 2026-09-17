using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.Accounting.Reports;

public class frmSubAccountsTrialBalanceRep : frmReportTree2010
{
	private DataTable dtAccountlevels;

	public DataTable dtSubAccounts;

	public string SubAccounts;

	public string TreeSubAccountsParentIDCol = "ParentID";

	public string TreeSubAccountsIDCol = "SubAccountID";

	public string TreeSubAccountsNameCol = (GlobalVariables.IsArabic ? "SubAccountNameAr" : "SubAccountNameEn");

	public string TreeSubAccountsNumberCol = "SubAccountNumber";

	public string TreeSubAccountsIsMainCol = "IsMain";

	private IContainer components = null;

	public UltraButton btnSubAccountsSearch;

	public UltraTextEditor txtSubAccounts;

	public UltraTree treeSubAccounts;

	protected internal UltraCheckEditor chkAllSubAccounts;

	private UltraComboEditor cboToLevel;

	private UltraLabel lblToLevel;

	public frmSubAccountsTrialBalanceRep()
	{
		dtItems = Accounts.Select("-1", "-1", GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItemsParentIDCol = "ParentID";
		TreeItemsIDCol = "AccountID";
		TreeItemsNameCol = (GlobalVariables.IsArabic ? "AccountNameAr" : "AccountNameEn");
		TreeItemsNumberCol = "AccountNumber";
		TreeItemsIsMainCol = "IsMain";
		dtItems2 = CostCenters.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItems2ParentIDCol = "ParentID";
		TreeItems2IDCol = "CostCenterID";
		TreeItems2NameCol = (GlobalVariables.IsArabic ? "CostCenterNameAr" : "CostCenterNameEn");
		TreeItems2NumberCol = "CostCenterNumber";
		TreeItems2IsMainCol = "IsMain";
		InitializeComponent();
		dtSubAccounts = BusinessLayer.Accounting.SubAccounts.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		cboReportType.Items.Clear();
		cboReportType.Items.Add((object)1, GlobalVariables.IsArabic ? "طباعه التفاصيل مدين ودائن" : "Print With All Details With Debit And Credit");
		cboReportType.Items.Add((object)2, GlobalVariables.IsArabic ? "طباعه رصيد ختامي" : "Print Final Balance");
		cboReportType.Items.Add((object)3, GlobalVariables.IsArabic ? "طباعه الحسابات والحسابات التحليليه معا" : "Print Accounts With SubAccounts");
		cboReportType.Items.Add((object)4, GlobalVariables.IsArabic ? "طباعه بالمستوى" : "Print With Levels");
	}

	public override void FormLoad()
	{
		base.FormLoad();
		if (dtSubAccounts != null)
		{
			TreeFunctions.FillTree(treeSubAccounts, dtSubAccounts, TreeSubAccountsParentIDCol, TreeSubAccountsIDCol, TreeSubAccountsNameCol, TreeSubAccountsNumberCol, TreeSubAccountsIsMainCol);
		}
		((UltraToggleEditorBase)chkAll2).Checked = true;
		((UltraToggleEditorBase)chkAll).Checked = true;
		dtAccountlevels = AccountLevels.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboToLevel, dtAccountlevels, "LevelID", "LevelID");
	}

	public override string GetReportName()
	{
		if (((UltraToggleEditorBase)chkWithLogo).Checked)
		{
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_A_SubAccountsTrialBalanceDetails_A.rpt" : "Rep_A_SubAccountsTrialBalanceDetails_E.rpt");
		}
		return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_A_SubAccountsTrialBalanceDetails_A_nologo.rpt" : "Rep_A_SubAccountsTrialBalanceDetails_E_nologo.rpt");
	}

	public override void ShowReport()
	{
		GetItems();
		GetBranches();
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		if (((UltraToggleEditorBase)chkAllSubAccounts).Checked)
		{
			SubAccounts = "-1";
		}
		else
		{
			SubAccounts = TreeFunctions.GetTreeCheckedNodesIDs(treeSubAccounts);
		}
		if (Items.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى حساب  ", "There is no chosen account to be shown in the report, please check items to be shown in report");
			return;
		}
		if (SubAccounts.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى حساب تحليلي  ", "There is no chosen account to be shown in the report, please check items to be shown in report");
			return;
		}
		if (((UltraToggleEditorBase)chkAll2).Checked || dtItems2.Select(TreeItems2IsMainCol + " = 0").Length == 0)
		{
			Items2 = "-1";
		}
		else
		{
			if (Items2.Equals(","))
			{
				GlobalVariables.InformationMB.Show("لم تقم باختيار اى مركز تكلفه  ", "There is no chosen Cost Center to be shown in the report, please check items to be shown in report");
				return;
			}
			if (cboToLevel.SelectedIndex == -1 && cboReportType.SelectedIndex > -1 && dtReports != null && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("Levels"))
			{
				GlobalVariables.InformationMB.Show("لم تقم باختيار اى مستوى  ", "There is no chosen Level to be shown in the report, please Select Level to be shown in report");
				return;
			}
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches);
		GlobalVariables.ReportDocument.SetParameterValue("@BranchsNames", BranchesNames);
		GlobalVariables.ReportDocument.SetParameterValue("@SubAccountIDs", SubAccounts);
		GlobalVariables.ReportDocument.SetParameterValue("@AccountIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@CostCenterIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@Approved", "-1");
		GlobalVariables.ReportDocument.SetParameterValue("@FromDate", dtpFromDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", text);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked);
		GlobalVariables.ReportDocument.SetParameterValue("@Suppress", 1);
		if (cboReportType.SelectedIndex > -1 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("Levels"))
		{
			GlobalVariables.ReportDocument.SetParameterValue("@ToLevelID", ((TextEditorControlBase)cboToLevel).Value.ToString());
		}
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
		if (!(GroupNamePath != ""))
		{
			return;
		}
		string text = GroupNamePath.Substring(0, GroupNamePath.IndexOf("SubAccountID") + 12);
		if (text.IndexOf("SubAccountID") < 0)
		{
			return;
		}
		string subAccountIDs = GroupNamePath.Replace(text, "").Replace(",", "").Replace('[', ',')
			.Replace(']', ',');
		frmSubAccountsLedgerRep frmSubAccountsLedgerRep2 = new frmSubAccountsLedgerRep();
		try
		{
			GlobalVariables.ReportDocument = new ReportDocument();
			frmSubAccountsLedgerRep2.ShowReport(Branches, BranchesNames, subAccountIDs, Items, Items2, "-1", -1, dtpFromDate.DateTime, dtpToDate.DateTime, ((UltraToggleEditorBase)chkIsArabic).Checked);
			RefreshReport(frmViewer);
		}
		catch (Exception ex)
		{
			if (ex.Message == "Load report failed.")
			{
				GlobalVariables.InformationMB.Show("مسار التقارير غير سليم \r\n برجاء مراجعة مسار التقارير من إعدادات النظام", "Invalied Reports Path \r\n Please Check Report Path from System Tools");
			}
			else
			{
				GlobalVariables.InformationMB.Show(ex.Message);
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
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches);
		GlobalVariables.ReportDocument.SetParameterValue("@BranchsNames", BranchesNames);
		GlobalVariables.ReportDocument.SetParameterValue("@SubAccountIDs", SubAccounts);
		GlobalVariables.ReportDocument.SetParameterValue("@AccountIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@CostCenterIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@Approved", "-1");
		GlobalVariables.ReportDocument.SetParameterValue("@FromDate", dtpFromDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", text);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked);
		GlobalVariables.ReportDocument.SetParameterValue("@Suppress", 1);
		if (cboReportType.SelectedIndex > -1 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("Levels"))
		{
			GlobalVariables.ReportDocument.SetParameterValue("@ToLevelID", ((TextEditorControlBase)cboToLevel).Value.ToString());
		}
		frmViewer.frmParent = this;
		frmViewer.ConfigureReport();
		frmViewer.BringToFront();
		frmViewer.Refresh();
		frmViewer.crvReportViewer.ShowNthPage(currentPageNumber);
	}

	public override void btnItemsSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.AccountsReport(IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems.GetNodeByKey(dtSearchResult.Rows[i][TreeItemsIDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	public override void btnItems2Search_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.CostCenterReport(IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems2.GetNodeByKey(dtSearchResult.Rows[i][TreeItems2IDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void btnSubAccountsSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.SubAccountsReport("-1", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			treeSubAccounts.GetNodeByKey(dtSearchResult.Rows[i][TreeSubAccountsIDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void txtSubAccounts_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtSubAccounts);
		dataView.RowFilter = TreeSubAccountsNameCol + " Like '%" + ((Control)(object)txtSubAccounts).Text.Trim() + "%' " + ((TreeSubAccountsNumberCol != null && TreeSubAccountsNumberCol != "") ? (" OR " + TreeSubAccountsNumberCol + " Like '" + ((Control)(object)txtSubAccounts).Text.Trim() + "%' ") : "");
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			treeSubAccounts.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			treeSubAccounts.ActiveNode = treeSubAccounts.GetNodeByKey(dataView.ToTable().Rows[0][TreeSubAccountsIDCol].ToString());
		}
	}

	private void chkAllSubAccounts_CheckedChanged(object sender, EventArgs e)
	{
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAllSubAccounts).Checked, treeSubAccounts);
	}

	private void treeSubAccounts_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		treeSubAccounts.AfterCheck -= new AfterNodeChangedEventHandler(treeSubAccounts_AfterCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			SetParentState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAllSubAccounts).CheckedChanged -= chkAllSubAccounts_CheckedChanged;
		SetCheckBoxAllState(treeSubAccounts, chkAllSubAccounts);
		((UltraToggleEditorBase)chkAllSubAccounts).CheckedChanged += chkAllSubAccounts_CheckedChanged;
		treeSubAccounts.AfterCheck += new AfterNodeChangedEventHandler(treeSubAccounts_AfterCheck);
	}

	private void cboReportType_ValueChanged(object sender, EventArgs e)
	{
		if (cboReportType.SelectedIndex > -1 && dtReports.Rows.Count > 0)
		{
			UltraLabel obj = lblToLevel;
			bool visible = (((Control)(object)cboToLevel).Visible = dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("Levels"));
			((Control)(object)obj).Visible = visible;
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
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Expected O, but got Unknown
		Appearance val = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Accounting.Reports.frmSubAccountsTrialBalanceRep));
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Override val8 = new Override();
		Appearance val9 = new Appearance();
		this.btnSubAccountsSearch = new UltraButton();
		this.txtSubAccounts = new UltraTextEditor();
		this.treeSubAccounts = new UltraTree();
		this.chkAllSubAccounts = new UltraCheckEditor();
		this.cboToLevel = new UltraComboEditor();
		this.lblToLevel = new UltraLabel();
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
		((System.ComponentModel.ISupportInitialize)this.txtSubAccounts).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.treeSubAccounts).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllSubAccounts).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboToLevel).BeginInit();
		base.SuspendLayout();
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance2.FontData");
		resources.ApplyResources(val, "appearance2");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val;
		base.dtpFromDate.DateTime = new System.DateTime(2015, 8, 24, 0, 0, 0, 0);
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2015, 8, 24, 0, 0, 0, 0);
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance3.FontData");
		resources.ApplyResources(val2, "appearance3");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		base.dtpToDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpToDate.DateTime = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		base.dtpToDate.MaskInput = "dd/mm/yyyy";
		base.dtpToDate.Value = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		resources.ApplyResources(base.chkAllBranches, "chkAllBranches");
		((UltraControlBase)base.chkAllBranches).UseAppStyling = false;
		((UltraControlBase)base.chkWithLogo).UseAppStyling = false;
		((UltraControlBase)base.lblReportType).UseAppStyling = false;
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance6.FontData");
		resources.ApplyResources(val3, "appearance6");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.cboReportType).Appearance = (AppearanceBase)(object)val3;
		base.cboReportType.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboReportType.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.cboReportType, "cboReportType");
		((TextEditorControlBase)base.cboReportType).ValueChanged += new System.EventHandler(cboReportType_ValueChanged);
		resources.ApplyResources(base.chkAll, "chkAll");
		resources.ApplyResources(base.chkAll2, "chkAll2");
		resources.ApplyResources(base.TreeItems, "TreeItems");
		resources.ApplyResources(base.TreeItems2, "TreeItems2");
		resources.ApplyResources(base.btnItemsSearch, "btnItemsSearch");
		resources.ApplyResources(base.btnItems2Search, "btnItems2Search");
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance8.FontData");
		resources.ApplyResources(val4, "appearance8");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtItems).Appearance = (AppearanceBase)(object)val4;
		resources.ApplyResources(base.txtItems, "txtItems");
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance5.FontData");
		resources.ApplyResources(val5, "appearance5");
		((SubObjectBase)val5).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtItems2).Appearance = (AppearanceBase)(object)val5;
		resources.ApplyResources(base.txtItems2, "txtItems2");
		resources.ApplyResources(this.btnSubAccountsSearch, "btnSubAccountsSearch");
		((AppearanceBase)val6).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val6, "appearance9");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance9.FontData");
		((SubObjectBase)val6).ForceApplyResources = "|FontData";
		((ControlBase)this.btnSubAccountsSearch).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.btnSubAccountsSearch).Name = "btnSubAccountsSearch";
		((System.Windows.Forms.Control)(object)this.btnSubAccountsSearch).Click += new System.EventHandler(btnSubAccountsSearch_Click);
		resources.ApplyResources(this.txtSubAccounts, "txtSubAccounts");
		((System.Windows.Forms.Control)(object)this.txtSubAccounts).Name = "txtSubAccounts";
		((TextEditorControlBase)this.txtSubAccounts).ValueChanged += new System.EventHandler(txtSubAccounts_ValueChanged);
		resources.ApplyResources(this.treeSubAccounts, "treeSubAccounts");
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance1.FontData");
		resources.ApplyResources(val7, "appearance1");
		((SubObjectBase)val7).ForceApplyResources = "FontData|";
		this.treeSubAccounts.Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.treeSubAccounts).Name = "treeSubAccounts";
		val8.NodeStyle = (NodeStyle)1;
		this.treeSubAccounts.Override = val8;
		((UltraControlBase)this.treeSubAccounts).UseAppStyling = false;
		this.treeSubAccounts.AfterCheck += new AfterNodeChangedEventHandler(treeSubAccounts_AfterCheck);
		resources.ApplyResources(this.chkAllSubAccounts, "chkAllSubAccounts");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val9).FontData, "appearance7.FontData");
		resources.ApplyResources(val9, "appearance7");
		((SubObjectBase)val9).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkAllSubAccounts).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.chkAllSubAccounts).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllSubAccounts).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllSubAccounts).Name = "chkAllSubAccounts";
		((UltraControlBase)this.chkAllSubAccounts).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAllSubAccounts).CheckedChanged += new System.EventHandler(chkAllSubAccounts_CheckedChanged);
		((TextEditorControlBase)this.cboToLevel).AlwaysInEditMode = true;
		resources.ApplyResources(this.cboToLevel, "cboToLevel");
		this.cboToLevel.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboToLevel).Name = "cboToLevel";
		resources.ApplyResources(this.lblToLevel, "lblToLevel");
		((System.Windows.Forms.Control)(object)this.lblToLevel).Name = "lblToLevel";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboToLevel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblToLevel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSubAccountsSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSubAccounts);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.treeSubAccounts);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllSubAccounts);
		base.Name = "frmSubAccountsTrialBalanceRep";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblSettingName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNew, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraLabel2, 0);
		base.Controls.SetChildIndex(base.clbBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.dtpFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.dtpToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkAllBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkWithLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPreview, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkAll, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkAll2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.TreeItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.TreeItems2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtItems2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnItemsSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnItems2Search, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkIsArabic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllSubAccounts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.treeSubAccounts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSubAccounts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSubAccountsSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblToLevel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboToLevel, 0);
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
		((System.ComponentModel.ISupportInitialize)this.txtSubAccounts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.treeSubAccounts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllSubAccounts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboToLevel).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
