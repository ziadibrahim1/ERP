using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using BusinessLayer.Defaults;
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

public class frmJVInquiry : frmReportTree2010
{
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

	protected internal UltraCheckEditor chkIsInternal;

	protected internal UltraCheckEditor chkExternal;

	public frmJVInquiry()
	{
		dtItems = Accounts.Select("-1", "-1", GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItemsParentIDCol = "ParentID";
		TreeItemsIDCol = "AccountID";
		TreeItemsNameCol = (GlobalVariables.IsArabic ? "AccountNameAr" : "AccountNameEn");
		TreeItemsNumberCol = "AccountNumber";
		TreeItemsIsMainCol = "IsMain";
		dtItems2 = JVDefaults.Select("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItems2IDCol = "TransTypeID";
		TreeItems2NameCol = (GlobalVariables.IsArabic ? "TransTypeNameAr" : "TransTypeNameEn");
		InitializeComponent();
		dtSubAccounts = BusinessLayer.Accounting.SubAccounts.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
	}

	public override void FormLoad()
	{
		base.FormLoad();
		if (dtItems2 != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeItems2, dtItems2, TreeItems2IDCol, TreeItems2NameCol);
		}
		if (dtSubAccounts != null)
		{
			TreeFunctions.FillTree(treeSubAccounts, dtSubAccounts, TreeSubAccountsParentIDCol, TreeSubAccountsIDCol, TreeSubAccountsNameCol, TreeSubAccountsNumberCol, TreeSubAccountsIsMainCol);
		}
		((UltraToggleEditorBase)chkAll2).Checked = true;
		((UltraToggleEditorBase)chkAll).Checked = true;
	}

	public override void ShowReport()
	{
		GetItems();
		GetBranches();
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		string val = "-1";
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
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى حساب  ", "There is no chosen account to be shown in the report, please check accounts to be shown in report");
			return;
		}
		if (SubAccounts.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى حساب تحليلي  ", "There is no chosen Subaccount to be shown in the report, please check Subaccounts to be shown in report");
			return;
		}
		if (((UltraToggleEditorBase)chkAll2).Checked)
		{
			Items2 = "-1";
		}
		else if (Items2.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى نوع حركة", "There is no chosen TransType to be shown in the report, please check TransTypes to be shown in report");
			return;
		}
		if (((UltraToggleEditorBase)chkIsInternal).Checked != ((UltraToggleEditorBase)chkExternal).Checked)
		{
			val = (((UltraToggleEditorBase)chkIsInternal).Checked ? "1" : "0");
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches);
		GlobalVariables.ReportDocument.SetParameterValue("@BranchsNames", BranchesNames);
		GlobalVariables.ReportDocument.SetParameterValue("@SubAccountIDs", SubAccounts);
		GlobalVariables.ReportDocument.SetParameterValue("@AccountIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@TransTypeIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@IsInternal", val);
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
		GlobalVariables.ReportDocument.SetParameterValue("@TransTypeIDs", Items2);
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

	public override void btnItemsSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.AccountsReport(IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems.GetNodeByKey(dtSearchResult.Rows[i][TreeItemsIDCol].ToString()).CheckedState = CheckState.Checked;
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
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_0781: Unknown result type (might be due to invalid IL or missing references)
		//IL_078b: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Accounting.Reports.frmJVInquiry));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Override val8 = new Override();
		Appearance val9 = new Appearance();
		Appearance val10 = new Appearance();
		Appearance val11 = new Appearance();
		this.btnSubAccountsSearch = new UltraButton();
		this.txtSubAccounts = new UltraTextEditor();
		this.treeSubAccounts = new UltraTree();
		this.chkAllSubAccounts = new UltraCheckEditor();
		this.chkIsInternal = new UltraCheckEditor();
		this.chkExternal = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.dtReports).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtFormSetting).BeginInit();
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
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSubAccounts).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.treeSubAccounts).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllSubAccounts).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsInternal).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkExternal).BeginInit();
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
		base.dtpFromDate.DateTime = new System.DateTime(2015, 8, 24, 0, 0, 0, 0);
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2015, 8, 24, 0, 0, 0, 0);
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
		resources.ApplyResources(val7, "appearance1");
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance1.FontData");
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
		resources.ApplyResources(val9, "appearance4");
		resources.ApplyResources(((AppearanceBase)val9).FontData, "appearance4.FontData");
		((SubObjectBase)val9).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkAllSubAccounts).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.chkAllSubAccounts).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllSubAccounts).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllSubAccounts).Name = "chkAllSubAccounts";
		((UltraControlBase)this.chkAllSubAccounts).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAllSubAccounts).CheckedChanged += new System.EventHandler(chkAllSubAccounts_CheckedChanged);
		resources.ApplyResources(this.chkIsInternal, "chkIsInternal");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance10");
		resources.ApplyResources(((AppearanceBase)val10).FontData, "appearance10.FontData");
		((SubObjectBase)val10).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkIsInternal).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.chkIsInternal).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkIsInternal).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkIsInternal).Name = "chkIsInternal";
		((UltraControlBase)this.chkIsInternal).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkIsInternal).CheckedChanged += new System.EventHandler(chkAllSubAccounts_CheckedChanged);
		resources.ApplyResources(this.chkExternal, "chkExternal");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance7");
		resources.ApplyResources(((AppearanceBase)val11).FontData, "appearance7.FontData");
		((SubObjectBase)val11).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkExternal).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.chkExternal).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkExternal).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkExternal).Name = "chkExternal";
		((UltraControlBase)this.chkExternal).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkExternal).CheckedChanged += new System.EventHandler(chkAllSubAccounts_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSubAccountsSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSubAccounts);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.treeSubAccounts);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkExternal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsInternal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllSubAccounts);
		base.Name = "frmJVInquiry";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsInternal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkExternal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.treeSubAccounts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSubAccounts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSubAccountsSearch, 0);
		((System.ComponentModel.ISupportInitialize)base.dtReports).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtFormSetting).EndInit();
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
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSubAccounts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.treeSubAccounts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllSubAccounts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsInternal).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkExternal).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
