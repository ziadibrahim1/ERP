using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.General;
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

public class frmItemCardColorSize : frmReportTree2010
{
	public DataTable dtColors;

	public string Colors;

	public string TreeColorsParentIDCol = "ParentID";

	public string TreeColorsIDCol = "ColorID";

	public string TreeColorsNameCol = (GlobalVariables.IsArabic ? "ColorNameAr" : "ColorNameEn");

	public string TreeColorsIsMainCol = "IsMain";

	public DataTable dtItemSizes;

	public string ItemSizes;

	public string TreeItemSizesParentIDCol = "ParentID";

	public string TreeItemSizesIDCol = "ItemSizeID";

	public string TreeItemSizesNameCol = (GlobalVariables.IsArabic ? "ItemSizeNameAr" : "ItemSizeNameEn");

	public string TreeItemSizesIsMainCol = "IsMain";

	private IContainer components = null;

	public UltraButton btnColorSearch;

	public UltraTextEditor txtColors;

	public UltraTree TreeColors;

	protected internal UltraCheckEditor chkAllColors;

	protected internal UltraCheckEditor chkAllItemSizes;

	public UltraTree TreeItemSizes;

	public UltraTextEditor txtItemSizes;

	public UltraButton btnItemSizesSearch;

	public frmItemCardColorSize()
	{
		dtItems = BusinessLayer.StockControl.Items.FillTree("-1", "-1", "0", "-1", "-1", "0", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItemsParentIDCol = "ParentID";
		TreeItemsIDCol = "ItemID";
		TreeItemsNameCol = "Name";
		TreeItemsIsMainCol = "IsMain";
		dtItems2 = Stores.Select("-1", ViewAllBranches ? "-1" : GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItems2ParentIDCol = "ParentID";
		TreeItems2IDCol = "StoreID";
		TreeItems2NameCol = (GlobalVariables.IsArabic ? "StoreNameAr" : "StoreNameEn");
		TreeItems2IsMainCol = "IsMain";
		dtColors = BusinessLayer.General.Colors.Select("-1", ViewAllBranches ? "-1" : GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItemSizes = BusinessLayer.StockControl.ItemSizes.Select("-1", ViewAllBranches ? "-1" : GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		InitializeComponent();
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
		if (dtColors != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeColors, dtColors, TreeColorsIDCol, TreeColorsNameCol);
		}
		if (dtItemSizes != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeItemSizes, dtItemSizes, TreeItemSizesIDCol, TreeItemSizesNameCol);
		}
		((UltraToggleEditorBase)chkAll2).Checked = true;
		((Control)(object)chkAllBranches).Visible = false;
		clbBranches.Visible = false;
		((Control)(object)chkAllColors).Text = (GlobalVariables.IsArabic ? "كل " : "All ") + GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((Control)(object)chkAllItemSizes).Text = (GlobalVariables.IsArabic ? "كل " : "All ") + GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
	}

	public override void ShowReport()
	{
		GetItems();
		GetBranches();
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		if (((UltraToggleEditorBase)chkAllColors).Checked)
		{
			Colors = "-1";
		}
		else
		{
			Colors = TreeFunctions.GetTreeCheckedNodesIDs(TreeColors);
		}
		if (((UltraToggleEditorBase)chkAllItemSizes).Checked)
		{
			ItemSizes = "-1";
		}
		else
		{
			ItemSizes = TreeFunctions.GetTreeCheckedNodesIDs(TreeItemSizes);
		}
		if (Items.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى صنف  ", "There is no chosen Item to be shown in the report, please check items to be shown in report");
			return;
		}
		if (Items2.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى مخزن  ", "There is no chosen Store to be shown in the report, please check Stores to be shown in report");
			return;
		}
		if (Colors.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى     ", "There is no chosen   to be shown in the report, please check  to be shown in report");
			return;
		}
		if (ItemSizes.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى     ", "There is no chosen   to be shown in the report, please check  to be shown in report");
			return;
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", "-1");
		GlobalVariables.ReportDocument.SetParameterValue("@ItemIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@StoreIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@ColorIDs", Colors);
		GlobalVariables.ReportDocument.SetParameterValue("@ItemSizeIDs", ItemSizes);
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
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", "-1");
		GlobalVariables.ReportDocument.SetParameterValue("@ItemIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@StoreIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@FromDate", dtpFromDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime);
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
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", "-1");
		GlobalVariables.ReportDocument.SetParameterValue("@ItemIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@StoreIDs", Items2);
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

	private void chkAllColors_CheckedChanged(object sender, EventArgs e)
	{
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAllColors).Checked, TreeColors);
	}

	private void chkAllItemSizes_CheckedChanged(object sender, EventArgs e)
	{
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAllItemSizes).Checked, TreeItemSizes);
	}

	private void txtColors_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtColors);
		dataView.RowFilter = TreeColorsNameCol + " Like '%" + ((Control)(object)txtColors).Text.Trim() + "%' " + ((TreeColorsNameCol != null && TreeColorsNameCol != "") ? (" OR " + TreeColorsNameCol + " Like '" + ((Control)(object)txtColors).Text.Trim() + "%' ") : "");
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TreeColors.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			TreeColors.ActiveNode = TreeColors.GetNodeByKey(dataView.ToTable().Rows[0][TreeColorsIDCol].ToString());
		}
	}

	private void txtItemSizes_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtItemSizes);
		dataView.RowFilter = TreeItemSizesNameCol + " Like '%" + ((Control)(object)txtItemSizes).Text.Trim() + "%' " + ((TreeItemSizesNameCol != null && TreeItemSizesNameCol != "") ? (" OR " + TreeItemSizesNameCol + " Like '" + ((Control)(object)txtItemSizes).Text.Trim() + "%' ") : "");
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TreeItemSizes.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			TreeItemSizes.ActiveNode = TreeItemSizes.GetNodeByKey(dataView.ToTable().Rows[0][TreeItemSizesIDCol].ToString());
		}
	}

	private void TreeColors_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		TreeColors.AfterCheck -= new AfterNodeChangedEventHandler(TreeColors_AfterCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			SetParentState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAllColors).CheckedChanged -= chkAllColors_CheckedChanged;
		SetCheckBoxAllState(TreeColors, chkAllColors);
		((UltraToggleEditorBase)chkAllColors).CheckedChanged += chkAllColors_CheckedChanged;
		TreeColors.AfterCheck += new AfterNodeChangedEventHandler(TreeColors_AfterCheck);
	}

	private void TreeItemSizes_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		TreeItemSizes.AfterCheck -= new AfterNodeChangedEventHandler(TreeItemSizes_AfterCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			SetParentState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAllItemSizes).CheckedChanged -= chkAllItemSizes_CheckedChanged;
		SetCheckBoxAllState(TreeItemSizes, chkAllItemSizes);
		((UltraToggleEditorBase)chkAllItemSizes).CheckedChanged += chkAllItemSizes_CheckedChanged;
		TreeItemSizes.AfterCheck += new AfterNodeChangedEventHandler(TreeItemSizes_AfterCheck);
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
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected O, but got Unknown
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected O, but got Unknown
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e1: Expected O, but got Unknown
		//IL_07ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0809: Expected O, but got Unknown
		Appearance val = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.Reports.frmItemCardColorSize));
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
		Override val12 = new Override();
		Appearance val13 = new Appearance();
		this.btnColorSearch = new UltraButton();
		this.txtColors = new UltraTextEditor();
		this.TreeColors = new UltraTree();
		this.chkAllColors = new UltraCheckEditor();
		this.chkAllItemSizes = new UltraCheckEditor();
		this.TreeItemSizes = new UltraTree();
		this.txtItemSizes = new UltraTextEditor();
		this.btnItemSizesSearch = new UltraButton();
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
		((System.ComponentModel.ISupportInitialize)this.txtColors).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeColors).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllColors).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllItemSizes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItemSizes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtItemSizes).BeginInit();
		base.SuspendLayout();
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance2.FontData");
		resources.ApplyResources(val, "appearance2");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val;
		base.dtpFromDate.DateTime = new System.DateTime(2020, 5, 14, 0, 0, 0, 0);
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2020, 5, 14, 0, 0, 0, 0);
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
		resources.ApplyResources(this.btnColorSearch, "btnColorSearch");
		((AppearanceBase)val6).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val6, "appearance12");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance12.FontData");
		((SubObjectBase)val6).ForceApplyResources = "|FontData";
		((ControlBase)this.btnColorSearch).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.btnColorSearch).Name = "btnColorSearch";
		resources.ApplyResources(this.txtColors, "txtColors");
		((System.Windows.Forms.Control)(object)this.txtColors).Name = "txtColors";
		((TextEditorControlBase)this.txtColors).ValueChanged += new System.EventHandler(txtColors_ValueChanged);
		resources.ApplyResources(this.TreeColors, "TreeColors");
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance10.FontData");
		resources.ApplyResources(val7, "appearance10");
		((SubObjectBase)val7).ForceApplyResources = "FontData|";
		this.TreeColors.Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.TreeColors).Name = "TreeColors";
		val8.NodeStyle = (NodeStyle)1;
		this.TreeColors.Override = val8;
		((UltraControlBase)this.TreeColors).UseAppStyling = false;
		this.TreeColors.AfterCheck += new AfterNodeChangedEventHandler(TreeColors_AfterCheck);
		resources.ApplyResources(this.chkAllColors, "chkAllColors");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val9).FontData, "appearance4.FontData");
		resources.ApplyResources(val9, "appearance4");
		((SubObjectBase)val9).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkAllColors).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.chkAllColors).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllColors).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllColors).Name = "chkAllColors";
		((UltraControlBase)this.chkAllColors).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAllColors).CheckedChanged += new System.EventHandler(chkAllColors_CheckedChanged);
		resources.ApplyResources(this.chkAllItemSizes, "chkAllItemSizes");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val10).FontData, "appearance9.FontData");
		resources.ApplyResources(val10, "appearance9");
		((SubObjectBase)val10).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkAllItemSizes).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.chkAllItemSizes).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllItemSizes).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllItemSizes).Name = "chkAllItemSizes";
		((UltraControlBase)this.chkAllItemSizes).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAllItemSizes).CheckedChanged += new System.EventHandler(chkAllItemSizes_CheckedChanged);
		resources.ApplyResources(this.TreeItemSizes, "TreeItemSizes");
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val11).FontData, "appearance11.FontData");
		resources.ApplyResources(val11, "appearance11");
		((SubObjectBase)val11).ForceApplyResources = "FontData|";
		this.TreeItemSizes.Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.TreeItemSizes).Name = "TreeItemSizes";
		val12.NodeStyle = (NodeStyle)1;
		this.TreeItemSizes.Override = val12;
		((UltraControlBase)this.TreeItemSizes).UseAppStyling = false;
		this.TreeItemSizes.AfterCheck += new AfterNodeChangedEventHandler(TreeItemSizes_AfterCheck);
		resources.ApplyResources(this.txtItemSizes, "txtItemSizes");
		((System.Windows.Forms.Control)(object)this.txtItemSizes).Name = "txtItemSizes";
		((TextEditorControlBase)this.txtItemSizes).ValueChanged += new System.EventHandler(txtItemSizes_ValueChanged);
		resources.ApplyResources(this.btnItemSizesSearch, "btnItemSizesSearch");
		((AppearanceBase)val13).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val13, "appearance13");
		resources.ApplyResources(((AppearanceBase)val13).FontData, "appearance13.FontData");
		((SubObjectBase)val13).ForceApplyResources = "|FontData";
		((ControlBase)this.btnItemSizesSearch).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.btnItemSizesSearch).Name = "btnItemSizesSearch";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnItemSizesSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnColorSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtItemSizes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtColors);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.TreeItemSizes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllItemSizes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.TreeColors);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllColors);
		base.Name = "frmItemCardColorSize";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllColors, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.TreeColors, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllItemSizes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.TreeItemSizes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtColors, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtItemSizes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnColorSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnItemSizesSearch, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
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
		((System.ComponentModel.ISupportInitialize)this.txtColors).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeColors).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllColors).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllItemSizes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItemSizes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtItemSizes).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
