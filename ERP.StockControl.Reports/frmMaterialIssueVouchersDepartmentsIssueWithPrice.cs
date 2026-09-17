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

public class frmMaterialIssueVouchersDepartmentsIssueWithPrice : frmReportTree2010
{
	public DataTable dtMaterialIssueVouchers;

	public string MaterialIssueVouchers;

	public string TreeMaterialIssueVouchersParentIDCol = "ParentID";

	public string TreeMaterialIssueVoucherIDCol = "MaterialIssueVoucherID";

	public string TreeMaterialIssueVoucherNameCol = "MaterialIssueVoucherNo";

	public string TreeMaterialIssueVoucherNumberCol = "";

	public string TreeMaterialIssueVouchersIsMainCol = "IsMain";

	private IContainer components = null;

	public UltraButton btnMIVSearch;

	public UltraTextEditor txtMIV;

	public UltraTree TreeMaterialIssueVouchers;

	protected internal UltraCheckEditor chkAllMIV;

	public frmMaterialIssueVouchersDepartmentsIssueWithPrice()
	{
		dtItems = BusinessLayer.StockControl.Items.FillTree("-1", "-1", "0", "-1", "-1", "0", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItemsParentIDCol = "ParentID";
		TreeItemsIDCol = "ItemID";
		TreeItemsNameCol = "Name";
		TreeItemsIsMainCol = "IsMain";
		dtMaterialIssueVouchers = BusinessLayer.StockControl.MaterialIssueVouchers.FillRepTree("-1", "-1", dtpFromDate.DateTime.Date.ToString(GlobalVariables.DateShortFormate), dtpToDate.DateTime.ToString("MM/dd/yyyy 23:59:59"));
		dtItems2 = Departments.Select("-1", ViewAllBranches ? "-1" : GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItems2ParentIDCol = "ParentID";
		TreeItems2IDCol = "DepartmentID";
		TreeItems2NameCol = (GlobalVariables.IsArabic ? "DepartmentNameAr" : "DepartmentNameEn");
		TreeItems2IsMainCol = "IsMain";
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
		if (dtMaterialIssueVouchers != null)
		{
			TreeFunctions.FillTree(TreeMaterialIssueVouchers, dtMaterialIssueVouchers, TreeMaterialIssueVouchersParentIDCol, TreeMaterialIssueVoucherIDCol, TreeMaterialIssueVoucherNameCol, TreeMaterialIssueVoucherNumberCol, TreeMaterialIssueVouchersIsMainCol);
		}
		((UltraToggleEditorBase)chkAll).Checked = true;
		((UltraToggleEditorBase)chkAllMIV).Checked = false;
		((UltraToggleEditorBase)chkAll2).Checked = true;
		((Control)(object)chkAllBranches).Visible = false;
		clbBranches.Visible = false;
	}

	public override void FillData()
	{
		GetItems();
		dtMaterialIssueVouchers = BusinessLayer.StockControl.MaterialIssueVouchers.FillRepTree("-1", Items2, dtpFromDate.DateTime.Date.ToString(GlobalVariables.DateShortFormate), dtpToDate.DateTime.ToString("MM/dd/yyyy 23:59:59"));
		if (dtMaterialIssueVouchers != null)
		{
			TreeFunctions.FillTree(TreeMaterialIssueVouchers, dtMaterialIssueVouchers, TreeMaterialIssueVouchersParentIDCol, TreeMaterialIssueVoucherIDCol, TreeMaterialIssueVoucherNameCol, TreeMaterialIssueVoucherNumberCol, TreeMaterialIssueVouchersIsMainCol);
		}
	}

	public override void ShowReport()
	{
		GetItems();
		GetBranches();
		MaterialIssueVouchers = TreeFunctions.GetTreeCheckedNodesIDs(TreeMaterialIssueVouchers);
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		if (Items.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى صنف  ", "There is no chosen Item to be shown in the report, please check items to be shown in report");
			return;
		}
		if (MaterialIssueVouchers.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى إذن صرف   ", "There is no choosen Material Issue Voucher to be shown in the report, please check items to be shown in report");
			return;
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", "-1");
		GlobalVariables.ReportDocument.SetParameterValue("@ItemIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@MaterialIssueVoucherIDs", MaterialIssueVouchers);
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

	public void RefreshReport(frmReporViwer frmViewer)
	{
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		int currentPageNumber = frmViewer.crvReportViewer.GetCurrentPageNumber();
		GlobalVariables.ReportDocument = new ReportDocument();
		GlobalVariables.ReportDocument.Load((dtReports.Rows.Count == 0) ? GetReportName() : (GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep" + (((UltraToggleEditorBase)chkIsArabic).Checked ? "_A" : "_E") + (((UltraToggleEditorBase)chkWithLogo).Checked ? "" : "_nologo")].ToString()));
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", "-1");
		GlobalVariables.ReportDocument.SetParameterValue("@ItemIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@MaterialIssueVoucherIDs", MaterialIssueVouchers);
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
		dtSearchResult = SearchFunctions.DepartmentsReport(IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems2.GetNodeByKey(dtSearchResult.Rows[i][TreeItems2IDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void chkAllSubAccounts_CheckedChanged(object sender, EventArgs e)
	{
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAllMIV).Checked, TreeMaterialIssueVouchers);
	}

	private void treeSubAccounts_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		TreeMaterialIssueVouchers.AfterCheck -= new AfterNodeChangedEventHandler(treeSubAccounts_AfterCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			SetParentState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAllMIV).CheckedChanged -= chkAllSubAccounts_CheckedChanged;
		SetCheckBoxAllState(TreeMaterialIssueVouchers, chkAllMIV);
		((UltraToggleEditorBase)chkAllMIV).CheckedChanged += chkAllSubAccounts_CheckedChanged;
		TreeMaterialIssueVouchers.AfterCheck += new AfterNodeChangedEventHandler(treeSubAccounts_AfterCheck);
	}

	public override void Tree2_AfterCheck(object sender, NodeEventArgs e)
	{
		base.Tree2_AfterCheck(sender, e);
		if (!IsLoading)
		{
			FillData();
		}
	}

	private void btnMIVSearch_Click(object sender, EventArgs e)
	{
	}

	private void txtMIV_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtMaterialIssueVouchers);
		dataView.RowFilter = TreeMaterialIssueVoucherNameCol + " Like '%" + ((Control)(object)txtMIV).Text.Trim() + "%' " + ((TreeMaterialIssueVoucherNumberCol != null && TreeMaterialIssueVoucherNumberCol != "") ? (" OR " + TreeMaterialIssueVoucherNumberCol + " Like '" + ((Control)(object)txtMIV).Text.Trim() + "%' ") : "");
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TreeMaterialIssueVouchers.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			TreeMaterialIssueVouchers.ActiveNode = TreeMaterialIssueVouchers.GetNodeByKey(dataView.ToTable().Rows[0][TreeMaterialIssueVoucherIDCol].ToString());
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
		//IL_0583: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Expected O, but got Unknown
		Appearance val = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.Reports.frmMaterialIssueVouchersDepartmentsIssueWithPrice));
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Override val8 = new Override();
		Appearance val9 = new Appearance();
		this.btnMIVSearch = new UltraButton();
		this.txtMIV = new UltraTextEditor();
		this.TreeMaterialIssueVouchers = new UltraTree();
		this.chkAllMIV = new UltraCheckEditor();
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
		((System.ComponentModel.ISupportInitialize)this.txtMIV).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeMaterialIssueVouchers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllMIV).BeginInit();
		base.SuspendLayout();
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance2.FontData");
		resources.ApplyResources(val, "appearance2");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val;
		base.dtpFromDate.DateTime = new System.DateTime(2017, 1, 29, 0, 0, 0, 0);
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2017, 1, 29, 0, 0, 0, 0);
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance3.FontData");
		resources.ApplyResources(val2, "appearance3");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		base.dtpToDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpToDate.DateTime = new System.DateTime(2017, 1, 29, 23, 59, 59, 0);
		base.dtpToDate.MaskInput = "dd/mm/yyyy";
		base.dtpToDate.Value = new System.DateTime(2017, 1, 29, 23, 59, 59, 0);
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
		resources.ApplyResources(this.btnMIVSearch, "btnMIVSearch");
		((AppearanceBase)val6).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val6, "appearance9");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance9.FontData");
		((SubObjectBase)val6).ForceApplyResources = "|FontData";
		((ControlBase)this.btnMIVSearch).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.btnMIVSearch).Name = "btnMIVSearch";
		((System.Windows.Forms.Control)(object)this.btnMIVSearch).Click += new System.EventHandler(btnMIVSearch_Click);
		resources.ApplyResources(this.txtMIV, "txtMIV");
		((System.Windows.Forms.Control)(object)this.txtMIV).Name = "txtMIV";
		((TextEditorControlBase)this.txtMIV).ValueChanged += new System.EventHandler(txtMIV_ValueChanged);
		resources.ApplyResources(this.TreeMaterialIssueVouchers, "TreeMaterialIssueVouchers");
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance1.FontData");
		resources.ApplyResources(val7, "appearance1");
		((SubObjectBase)val7).ForceApplyResources = "FontData|";
		this.TreeMaterialIssueVouchers.Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.TreeMaterialIssueVouchers).Name = "TreeMaterialIssueVouchers";
		val8.NodeStyle = (NodeStyle)1;
		this.TreeMaterialIssueVouchers.Override = val8;
		((UltraControlBase)this.TreeMaterialIssueVouchers).UseAppStyling = false;
		this.TreeMaterialIssueVouchers.AfterCheck += new AfterNodeChangedEventHandler(treeSubAccounts_AfterCheck);
		resources.ApplyResources(this.chkAllMIV, "chkAllMIV");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val9).FontData, "appearance7.FontData");
		resources.ApplyResources(val9, "appearance7");
		((SubObjectBase)val9).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkAllMIV).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.chkAllMIV).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllMIV).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllMIV).Name = "chkAllMIV";
		((UltraControlBase)this.chkAllMIV).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAllMIV).CheckedChanged += new System.EventHandler(chkAllSubAccounts_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnMIVSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtMIV);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.TreeMaterialIssueVouchers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllMIV);
		base.Name = "frmMaterialIssueVouchersDepartmentsIssueWithPrice";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllMIV, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.TreeMaterialIssueVouchers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtMIV, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnMIVSearch, 0);
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
		((System.ComponentModel.ISupportInitialize)this.txtMIV).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeMaterialIssueVouchers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllMIV).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
