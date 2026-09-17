using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.StockControl;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.StockControl.Reports;

public class frmGoodReceiptNotesByStores : frmReportTree2010
{
	public DataTable dtGRNVouchers;

	public string GRNVouchers;

	public string TreeGRNVouchersParentIDCol = "ParentID";

	public string TreeGRNVoucherIDCol = "GoodReceiptNoteID";

	public string TreeGRNVoucherNameCol = "GoodReceiptNoteNo";

	public string TreeGRNVoucherNumberCol = "";

	public string TreeGRNVouchersIsMainCol = "IsMain";

	public DataTable dtSubAccounts;

	public string SubAccounts;

	public string TreeSubAccountsParentIDCol = "ParentID";

	public string TreeSubAccountsIDCol = "SubAccountID";

	public string TreeSubAccountsNameCol = "SubAccountName";

	public string TreeSubAccountsNumberCol = "ClientSupplierNo";

	public string TreeSubAccountsIsMainCol = "IsMain";

	private IContainer components = null;

	public UltraButton btnSuppliersSearch;

	public UltraTextEditor txtSuppliers;

	public UltraTree TreeSubAccounts;

	protected internal UltraCheckEditor chkAllSubAccounts;

	public UltraTextEditor txtGRN;

	public UltraTree TreeGRNVouchers;

	protected internal UltraCheckEditor chkAllGRN;

	public frmGoodReceiptNotesByStores()
	{
		dtItems = BusinessLayer.StockControl.Items.FillTree("-1", "-1", "0", "-1", "-1", "0", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItemsParentIDCol = "ParentID";
		TreeItemsIDCol = "ItemID";
		TreeItemsNameCol = "Name";
		TreeItemsIsMainCol = "IsMain";
		dtSubAccounts = BusinessLayer.Accounting.SubAccounts.FillReportTreeBySubAccountTypeIDs(GlobalVariables.SupplierSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtGRNVouchers = GoodReceiptNotes.FillRepTree("-1", "-1", "-1", dtpFromDate.DateTime.Date.ToString(GlobalVariables.DateShortFormate), dtpToDate.DateTime.ToString("MM/dd/yyyy 23:59:59"));
		dtItems2 = Stores.Select("-1", ViewAllBranches ? "-1" : GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItems2ParentIDCol = "ParentID";
		TreeItems2IDCol = "StoreID";
		TreeItems2NameCol = (GlobalVariables.IsArabic ? "StoreNameAr" : "StoreNameEn");
		TreeItems2IsMainCol = "IsMain";
		InitializeComponent();
		cboReportType.Items.Clear();
		cboReportType.Items.Add((object)1, GlobalVariables.IsArabic ? "حركات الأصناف" : "Item Transaction");
		if (GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod"))
		{
			cboReportType.Items.Add((object)2, GlobalVariables.IsArabic ? "حركات الأصناف لكل تشغيله" : "Item Transaction For Each Batch No.");
		}
		cboReportType.SelectedIndex = 0;
	}

	public override void FormLoad()
	{
		if (dtItems != null)
		{
			TreeFunctions.FillTree(TreeItems, dtItems, TreeItemsParentIDCol, TreeItemsIDCol, TreeItemsNameCol, TreeItemsNumberCol, TreeItemsIsMainCol);
		}
		if (dtItems2 != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeItems2, dtItems2, TreeItems2IDCol, TreeItems2NameCol);
		}
		if (dtSubAccounts != null)
		{
			TreeFunctions.FillTree(TreeSubAccounts, dtSubAccounts, TreeSubAccountsParentIDCol, TreeSubAccountsIDCol, TreeSubAccountsNameCol, TreeSubAccountsNumberCol, TreeSubAccountsIsMainCol);
		}
		if (dtGRNVouchers != null)
		{
			TreeFunctions.FillTree(TreeGRNVouchers, dtGRNVouchers, TreeGRNVouchersParentIDCol, TreeGRNVoucherIDCol, TreeGRNVoucherNameCol, TreeGRNVoucherNumberCol, TreeGRNVouchersIsMainCol);
		}
		((UltraToggleEditorBase)chkAllGRN).Checked = false;
		((UltraToggleEditorBase)chkAllSubAccounts).Checked = true;
		((UltraToggleEditorBase)chkAll2).Checked = true;
		((Control)(object)chkAllBranches).Visible = false;
		clbBranches.Visible = false;
	}

	public override void FillData()
	{
		if (((UltraToggleEditorBase)chkAllSubAccounts).Checked)
		{
			SubAccounts = "-1";
		}
		else
		{
			SubAccounts = TreeFunctions.GetTreeCheckedNodesIDs(TreeSubAccounts);
		}
		GetItems();
		dtGRNVouchers = GoodReceiptNotes.FillRepTree("-1", SubAccounts, Items2, dtpFromDate.DateTime.Date.ToString(GlobalVariables.DateShortFormate), dtpToDate.DateTime.ToString("MM/dd/yyyy 23:59:59"));
		if (dtGRNVouchers != null)
		{
			TreeFunctions.FillTree(TreeGRNVouchers, dtGRNVouchers, TreeGRNVouchersParentIDCol, TreeGRNVoucherIDCol, TreeGRNVoucherNameCol, TreeGRNVoucherNumberCol, TreeGRNVouchersIsMainCol);
		}
	}

	public override void ShowReport()
	{
		GetItems();
		GetBranches();
		GRNVouchers = TreeFunctions.GetTreeCheckedNodesIDs(TreeGRNVouchers);
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		if (Items.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى صنف  ", "There is no choosen Item to be shown in the report, please check items to be shown in report");
			return;
		}
		if (GRNVouchers.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى إذن صرف   ", "There is no choosen Good Receipt Note to be shown in the report, please check items to be shown in report");
			return;
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@GoodReceiptNoteIDs", GRNVouchers);
		GlobalVariables.ReportDocument.SetParameterValue("@ItemIDs", Items);
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

	public override void GetBranches()
	{
		Online = false;
		Branches = ",";
		for (int i = 0; i < ((DisposableObjectCollectionBase)TreeItems2.Nodes).Count; i++)
		{
			if (TreeItems2.Nodes[i].CheckedState == CheckState.Checked)
			{
				Branches = Branches + dtItems2.Rows[i]["BranchID"].ToString() + ",";
				if (dtItems2.Rows[i]["BranchID"].ToString() != GlobalVariables.CurrentBranchID && Main.IsSynchronization && !GlobalVariables.dtSyncConn.Rows[0]["SyncBranchID"].Equals(DBNull.Value))
				{
					Online = true;
				}
			}
		}
	}

	public void ShowReport(string BranchIDs, string ItemIDs, string StoreIDs, DateTime FromDate, DateTime ToDate, bool IsArabic, int cboindex)
	{
		Branches = BranchIDs;
		Items = ItemIDs;
		Items2 = StoreIDs;
		dtpFromDate.DateTime = FromDate;
		dtpToDate.DateTime = ToDate;
		((UltraToggleEditorBase)chkIsArabic).Checked = IsArabic;
		cboReportType.SelectedIndex = cboindex;
		GlobalVariables.ReportDocument.Load((dtReports.Rows.Count == 0) ? GetReportName() : (GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep" + (((UltraToggleEditorBase)chkIsArabic).Checked ? "_A" : "_E") + (((UltraToggleEditorBase)chkWithLogo).Checked ? "" : "_nologo")].ToString()));
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@GoodReceiptNoteIDs", GRNVouchers);
		GlobalVariables.ReportDocument.SetParameterValue("@ItemIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", text);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked);
		frmReporViwer2.frmParent = this;
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
	}

	public override void ReportDoubleClick(string GroupNamePath, frmReporViwer frmViewer)
	{
		if (GroupNamePath != "")
		{
			string text = GroupNamePath.Substring(0, GroupNamePath.IndexOf("ItemTransactionID") + 17);
			if (text.IndexOf("ItemTransactionID") >= 0)
			{
				string s = GroupNamePath.Replace(text, "").Replace(",", "").Replace("[", "")
					.Replace("]", "");
				GlobalFunctions.SC_OpenTransactionForms(Convert.ToInt32(decimal.Parse(s)));
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
		GlobalVariables.ReportDocument.SetParameterValue("@GoodReceiptNoteIDs", GRNVouchers);
		GlobalVariables.ReportDocument.SetParameterValue("@ItemIDs", Items);
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
		dtSearchResult = SearchFunctions.ItemsReport("-1", "-1", "0", "-1", "-1", "0", "-1", "-1", "-1", "0", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems.GetNodeByKey(dtSearchResult.Rows[i][TreeItemsIDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	public override void btnItems2Search_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.StoresReport(IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems2.GetNodeByKey(dtSearchResult.Rows[i][TreeItems2IDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void btnSuppliersSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.SuppliersReport(IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeSubAccounts.GetNodeByKey(dtSearchResult.Rows[i][TreeSubAccountsIDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void chkAllSubAccounts_CheckedChanged(object sender, EventArgs e)
	{
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAllSubAccounts).Checked, TreeSubAccounts);
	}

	private void treeSubAccounts_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		TreeSubAccounts.AfterCheck -= new AfterNodeChangedEventHandler(treeSubAccounts_AfterCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			SetParentState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAllSubAccounts).CheckedChanged -= chkAllSubAccounts_CheckedChanged;
		SetCheckBoxAllState(TreeSubAccounts, chkAllSubAccounts);
		((UltraToggleEditorBase)chkAllSubAccounts).CheckedChanged += chkAllSubAccounts_CheckedChanged;
		TreeSubAccounts.AfterCheck += new AfterNodeChangedEventHandler(treeSubAccounts_AfterCheck);
		if (!IsLoading)
		{
			FillData();
		}
	}

	private void chkAllGRN_CheckedChanged(object sender, EventArgs e)
	{
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAllGRN).Checked, TreeGRNVouchers);
	}

	private void TreeGRNVouchers_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		TreeGRNVouchers.AfterCheck -= new AfterNodeChangedEventHandler(TreeGRNVouchers_AfterCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			SetParentState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAllGRN).CheckedChanged -= chkAllGRN_CheckedChanged;
		SetCheckBoxAllState(TreeGRNVouchers, chkAllGRN);
		((UltraToggleEditorBase)chkAllGRN).CheckedChanged += chkAllGRN_CheckedChanged;
		TreeGRNVouchers.AfterCheck += new AfterNodeChangedEventHandler(TreeGRNVouchers_AfterCheck);
	}

	private void txtGRN_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtGRNVouchers);
		dataView.RowFilter = TreeGRNVoucherNameCol + " Like '%" + ((Control)(object)txtGRN).Text.Trim() + "%' " + ((TreeGRNVoucherNumberCol != null && TreeGRNVoucherNumberCol != "") ? (" OR " + TreeGRNVoucherNumberCol + " Like '" + ((Control)(object)txtGRN).Text.Trim() + "%' ") : "");
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TreeGRNVouchers.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			TreeGRNVouchers.ActiveNode = TreeGRNVouchers.GetNodeByKey(dataView.ToTable().Rows[0][TreeGRNVoucherIDCol].ToString());
		}
	}

	private void btnGRNSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.GoodReceiptNotesReportBySupplierID("-1");
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeGRNVouchers.GetNodeByKey(dtSearchResult.Rows[i][TreeGRNVoucherIDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void TreeItems2_AfterCheck(object sender, NodeEventArgs e)
	{
		if (!IsLoading)
		{
			FillData();
		}
	}

	private void txtSuppliers_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtSubAccounts);
		dataView.RowFilter = TreeSubAccountsNameCol + " Like '%" + ((Control)(object)txtSuppliers).Text.Trim() + "%' " + ((TreeSubAccountsNumberCol != null && TreeSubAccountsNumberCol != "") ? (" OR " + TreeSubAccountsNumberCol + " Like '" + ((Control)(object)txtSuppliers).Text.Trim() + "%' ") : "");
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TreeSubAccounts.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			TreeSubAccounts.ActiveNode = TreeSubAccounts.GetNodeByKey(dataView.ToTable().Rows[0][TreeSubAccountsIDCol].ToString());
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
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Expected O, but got Unknown
		//IL_05f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fd: Expected O, but got Unknown
		//IL_0796: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a0: Expected O, but got Unknown
		Appearance val = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.Reports.frmGoodReceiptNotesByStores));
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Override val8 = new Override();
		Appearance val9 = new Appearance();
		Appearance val10 = new Appearance();
		Override val11 = new Override();
		Appearance val12 = new Appearance();
		this.btnSuppliersSearch = new UltraButton();
		this.txtSuppliers = new UltraTextEditor();
		this.TreeSubAccounts = new UltraTree();
		this.chkAllSubAccounts = new UltraCheckEditor();
		this.txtGRN = new UltraTextEditor();
		this.TreeGRNVouchers = new UltraTree();
		this.chkAllGRN = new UltraCheckEditor();
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
		((System.ComponentModel.ISupportInitialize)this.txtSuppliers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeSubAccounts).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllSubAccounts).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGRN).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeGRNVouchers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllGRN).BeginInit();
		base.SuspendLayout();
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance2.FontData");
		resources.ApplyResources(val, "appearance2");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val;
		base.dtpFromDate.DateTime = new System.DateTime(2016, 8, 2, 0, 0, 0, 0);
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2016, 8, 2, 0, 0, 0, 0);
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance3.FontData");
		resources.ApplyResources(val2, "appearance3");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		base.dtpToDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpToDate.DateTime = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		base.dtpToDate.MaskInput = "dd/mm/yyyy";
		base.dtpToDate.Value = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
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
		resources.ApplyResources(base.chkAll, "chkAll");
		resources.ApplyResources(base.chkAll2, "chkAll2");
		resources.ApplyResources(base.TreeItems, "TreeItems");
		resources.ApplyResources(base.TreeItems2, "TreeItems2");
		base.TreeItems2.AfterCheck += new AfterNodeChangedEventHandler(TreeItems2_AfterCheck);
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
		resources.ApplyResources(this.btnSuppliersSearch, "btnSuppliersSearch");
		((AppearanceBase)val6).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val6, "appearance4");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance4.FontData");
		((SubObjectBase)val6).ForceApplyResources = "|FontData";
		((ControlBase)this.btnSuppliersSearch).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.btnSuppliersSearch).Name = "btnSuppliersSearch";
		((System.Windows.Forms.Control)(object)this.btnSuppliersSearch).Click += new System.EventHandler(btnSuppliersSearch_Click);
		resources.ApplyResources(this.txtSuppliers, "txtSuppliers");
		((System.Windows.Forms.Control)(object)this.txtSuppliers).Name = "txtSuppliers";
		((TextEditorControlBase)this.txtSuppliers).ValueChanged += new System.EventHandler(txtSuppliers_ValueChanged);
		resources.ApplyResources(this.TreeSubAccounts, "TreeSubAccounts");
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance11.FontData");
		resources.ApplyResources(val7, "appearance11");
		((SubObjectBase)val7).ForceApplyResources = "FontData|";
		this.TreeSubAccounts.Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.TreeSubAccounts).Name = "TreeSubAccounts";
		val8.NodeStyle = (NodeStyle)1;
		this.TreeSubAccounts.Override = val8;
		((UltraControlBase)this.TreeSubAccounts).UseAppStyling = false;
		this.TreeSubAccounts.AfterCheck += new AfterNodeChangedEventHandler(treeSubAccounts_AfterCheck);
		resources.ApplyResources(this.chkAllSubAccounts, "chkAllSubAccounts");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val9).FontData, "appearance10.FontData");
		resources.ApplyResources(val9, "appearance10");
		((SubObjectBase)val9).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkAllSubAccounts).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.chkAllSubAccounts).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllSubAccounts).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllSubAccounts).Name = "chkAllSubAccounts";
		((UltraControlBase)this.chkAllSubAccounts).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAllSubAccounts).CheckedChanged += new System.EventHandler(chkAllSubAccounts_CheckedChanged);
		resources.ApplyResources(this.txtGRN, "txtGRN");
		((System.Windows.Forms.Control)(object)this.txtGRN).Name = "txtGRN";
		((TextEditorControlBase)this.txtGRN).ValueChanged += new System.EventHandler(txtGRN_ValueChanged);
		resources.ApplyResources(this.TreeGRNVouchers, "TreeGRNVouchers");
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val10).FontData, "appearance1.FontData");
		resources.ApplyResources(val10, "appearance1");
		((SubObjectBase)val10).ForceApplyResources = "FontData|";
		this.TreeGRNVouchers.Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.TreeGRNVouchers).Name = "TreeGRNVouchers";
		val11.NodeStyle = (NodeStyle)1;
		this.TreeGRNVouchers.Override = val11;
		((UltraControlBase)this.TreeGRNVouchers).UseAppStyling = false;
		this.TreeGRNVouchers.AfterCheck += new AfterNodeChangedEventHandler(TreeGRNVouchers_AfterCheck);
		resources.ApplyResources(this.chkAllGRN, "chkAllGRN");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val12).FontData, "appearance7.FontData");
		resources.ApplyResources(val12, "appearance7");
		((SubObjectBase)val12).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkAllGRN).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.chkAllGRN).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllGRN).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllGRN).Name = "chkAllGRN";
		((UltraControlBase)this.chkAllGRN).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAllGRN).CheckedChanged += new System.EventHandler(chkAllGRN_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGRN);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.TreeGRNVouchers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllGRN);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSuppliersSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSuppliers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.TreeSubAccounts);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllSubAccounts);
		base.Name = "frmGoodReceiptNotesByStores";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraLabel2, 0);
		base.Controls.SetChildIndex(base.clbBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.dtpFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.dtpToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkAllBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkWithLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPreview, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkAll, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkAll2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.TreeItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.TreeItems2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtItems2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnItemsSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnItems2Search, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkIsArabic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNew, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblSettingName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllSubAccounts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.TreeSubAccounts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSuppliers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSuppliersSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllGRN, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.TreeGRNVouchers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGRN, 0);
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
		((System.ComponentModel.ISupportInitialize)this.txtSuppliers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeSubAccounts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllSubAccounts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGRN).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeGRNVouchers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllGRN).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
