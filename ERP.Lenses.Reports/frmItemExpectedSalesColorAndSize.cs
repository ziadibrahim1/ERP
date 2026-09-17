using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.General;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.Lenses.Reports;

public class frmItemExpectedSalesColorAndSize : frmReportTree2010
{
	public DataTable dtItemSizes;

	public string ItemSizes;

	public string TreeItemSizesParentIDCol = "ParentID";

	public string TreeItemSizesIDCol = "ItemSizeID";

	public string TreeItemSizesNameCol = (GlobalVariables.IsArabic ? "ItemSizeNameAr" : "ItemSizeNameEn");

	public string TreeItemSizesIsMainCol = "IsMain";

	private string colorDisplayName;

	private string sizeDisplayName;

	private IContainer components = null;

	private UltraTextEditor txtMonthCount;

	private UltraLabel lblMonthCount;

	public UltraButton btnItemSizesSearch;

	public UltraTextEditor txtItemSizes;

	public UltraTree TreeItemSizes;

	protected internal UltraCheckEditor chkAllItemSizes;

	public frmItemExpectedSalesColorAndSize()
	{
		dtItems = BusinessLayer.StockControl.Items.FillTree("-1", "-1", "0", "-1", "-1", "0", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItemsParentIDCol = "ParentID";
		TreeItemsIDCol = "ItemID";
		TreeItemsNameCol = "Name";
		TreeItemsIsMainCol = "IsMain";
		TreeItems2ParentIDCol = "ParentID";
		TreeItems2IDCol = "ColorID";
		TreeItems2NameCol = (GlobalVariables.IsArabic ? "ColorNameAr" : "ColorNameEn");
		TreeItems2IsMainCol = "IsMain";
		dtItemSizes = BusinessLayer.StockControl.ItemSizes.Select("-1", ViewAllBranches ? "-1" : GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		colorDisplayName = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		sizeDisplayName = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		InitializeComponent();
	}

	public override void FormLoad()
	{
		if (dtItems != null)
		{
			TreeFunctions.FillTree(TreeItems, dtItems, TreeItemsParentIDCol, TreeItemsIDCol, TreeItemsNameCol, TreeItemsNumberCol, TreeItemsIsMainCol);
		}
		dtItems2 = Colors.Select("-1", ViewAllBranches ? "-1" : GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (dtItems2 != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeItems2, dtItems2, TreeItems2IDCol, TreeItems2NameCol);
		}
		if (dtItemSizes != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeItemSizes, dtItemSizes, TreeItemSizesIDCol, TreeItemSizesNameCol);
		}
		((Control)(object)chkAll2).Text = (GlobalVariables.IsArabic ? "كل " : "All ") + colorDisplayName;
		((Control)(object)chkAllItemSizes).Text = (GlobalVariables.IsArabic ? "كل " : "All ") + sizeDisplayName;
	}

	public override void ShowReport()
	{
		GetItems();
		GetBranches();
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
			GlobalVariables.InformationMB.Show(" لم تقم باختيار اى " + colorDisplayName, "There is no chosen " + colorDisplayName + " to be shown in the report, please check " + colorDisplayName + " to be shown in report");
			return;
		}
		if (ItemSizes.Equals(","))
		{
			GlobalVariables.InformationMB.Show(" لم تقم باختيار اى " + sizeDisplayName, "There is no chosen " + sizeDisplayName + " to be shown in the report, please check" + sizeDisplayName + "to be shown in report");
			return;
		}
		if (((Control)(object)txtMonthCount).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال عدد الشهور المطلوبه", "Please Enter Month Count");
			return;
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches);
		GlobalVariables.ReportDocument.SetParameterValue("@ItemIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@ColorIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@ItemSizeIDs", ItemSizes);
		GlobalVariables.ReportDocument.SetParameterValue("@MonthCount", ((Control)(object)txtMonthCount).Text);
		GlobalVariables.ReportDocument.SetParameterValue("@FromDate", dtpFromDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime);
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
	}

	private void txtMonthCount_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void chkAllItemSizes_CheckedChanged(object sender, EventArgs e)
	{
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAllItemSizes).Checked, TreeItemSizes);
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
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.Reports.frmItemExpectedSalesColorAndSize));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Override val8 = new Override();
		Appearance val9 = new Appearance();
		this.txtMonthCount = new UltraTextEditor();
		this.lblMonthCount = new UltraLabel();
		this.btnItemSizesSearch = new UltraButton();
		this.txtItemSizes = new UltraTextEditor();
		this.TreeItemSizes = new UltraTree();
		this.chkAllItemSizes = new UltraCheckEditor();
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
		((System.ComponentModel.ISupportInitialize)this.txtMonthCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtItemSizes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItemSizes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllItemSizes).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.ultraLabel1, "ultraLabel1");
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		((AppearanceBase)val).FontData.Name = resources.GetString("resource.Name");
		resources.ApplyResources(val, "appearance1");
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val;
		base.dtpFromDate.DateTime = new System.DateTime(2021, 3, 30, 0, 0, 0, 0);
		resources.ApplyResources(base.dtpFromDate, "dtpFromDate");
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2021, 3, 30, 0, 0, 0, 0);
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name1");
		resources.ApplyResources(val2, "appearance2");
		base.dtpToDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpToDate.DateTime = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		base.dtpToDate.MaskInput = "dd/mm/yyyy";
		base.dtpToDate.Value = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		((UltraControlBase)base.chkAllBranches).UseAppStyling = false;
		resources.ApplyResources(base.clbBranches, "clbBranches");
		((UltraControlBase)base.chkWithLogo).UseAppStyling = false;
		((UltraControlBase)base.lblReportType).UseAppStyling = false;
		((AppearanceBase)val3).FontData.Name = resources.GetString("resource.Name2");
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
		((AppearanceBase)val4).FontData.Name = resources.GetString("resource.Name3");
		resources.ApplyResources(val4, "appearance4");
		((TextEditorControlBase)base.txtItems).Appearance = (AppearanceBase)(object)val4;
		resources.ApplyResources(base.txtItems, "txtItems");
		((AppearanceBase)val5).FontData.Name = resources.GetString("resource.Name4");
		resources.ApplyResources(val5, "appearance5");
		((TextEditorControlBase)base.txtItems2).Appearance = (AppearanceBase)(object)val5;
		resources.ApplyResources(base.txtItems2, "txtItems2");
		resources.ApplyResources(this.txtMonthCount, "txtMonthCount");
		((System.Windows.Forms.Control)(object)this.txtMonthCount).Name = "txtMonthCount";
		((System.Windows.Forms.Control)(object)this.txtMonthCount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtMonthCount_KeyPress);
		resources.ApplyResources(this.lblMonthCount, "lblMonthCount");
		this.lblMonthCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMonthCount).Name = "lblMonthCount";
		resources.ApplyResources(this.btnItemSizesSearch, "btnItemSizesSearch");
		((AppearanceBase)val6).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnItemSizesSearch).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.btnItemSizesSearch).Name = "btnItemSizesSearch";
		resources.ApplyResources(this.txtItemSizes, "txtItemSizes");
		((System.Windows.Forms.Control)(object)this.txtItemSizes).Name = "txtItemSizes";
		((TextEditorControlBase)this.txtItemSizes).ValueChanged += new System.EventHandler(txtItemSizes_ValueChanged);
		resources.ApplyResources(this.TreeItemSizes, "TreeItemSizes");
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		this.TreeItemSizes.Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.TreeItemSizes).Name = "TreeItemSizes";
		val8.NodeStyle = (NodeStyle)1;
		this.TreeItemSizes.Override = val8;
		((UltraControlBase)this.TreeItemSizes).UseAppStyling = false;
		this.TreeItemSizes.AfterCheck += new AfterNodeChangedEventHandler(TreeItemSizes_AfterCheck);
		resources.ApplyResources(this.chkAllItemSizes, "chkAllItemSizes");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		((UltraToggleEditorBase)this.chkAllItemSizes).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.chkAllItemSizes).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllItemSizes).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllItemSizes).Name = "chkAllItemSizes";
		((UltraControlBase)this.chkAllItemSizes).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAllItemSizes).CheckedChanged += new System.EventHandler(chkAllItemSizes_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnItemSizesSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtItemSizes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.TreeItemSizes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllItemSizes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtMonthCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMonthCount);
		base.Name = "frmItemExpectedSalesColorAndSize";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNew, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblSettingName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkAll, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnItemsSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.TreeItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnItems2Search, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtItems2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkAll2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.TreeItems2, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkIsArabic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMonthCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtMonthCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllItemSizes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.TreeItemSizes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtItemSizes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnItemSizesSearch, 0);
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
		((System.ComponentModel.ISupportInitialize)this.txtMonthCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtItemSizes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItemSizes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllItemSizes).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
