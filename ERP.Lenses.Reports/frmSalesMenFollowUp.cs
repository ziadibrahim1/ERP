using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Accounting;
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

public class frmSalesMenFollowUp : frmReportTree2010
{
	public DataTable dtItems3;

	public string Items3;

	public string TreeItems3ParentIDCol = "ParentID";

	public string TreeItems3IDCol = "ItemID";

	public string TreeItems3NameCol = "Name";

	public string TreeItems3IsMainCol = "IsMain";

	private IContainer components = null;

	public UltraTextEditor txtLensesItems;

	public UltraTree treeLensesItems;

	protected internal UltraCheckEditor chkAllLensesItems;

	public UltraButton btnLensesItemsSearch;

	public frmSalesMenFollowUp()
	{
		dtItems = SubAccounts.FillReportTreeBySubAccountTypeIDs(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItemsParentIDCol = "ParentID";
		TreeItemsIDCol = "SubAccountID";
		TreeItemsNameCol = "SubAccountName";
		TreeItemsIsMainCol = "IsMain";
		dtItems2 = SubAccounts.FillReportTreeBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItems2ParentIDCol = "ParentID";
		TreeItems2IDCol = "SubAccountID";
		TreeItems2NameCol = "SubAccountName";
		TreeItems2NumberCol = "ClientSupplierNo";
		TreeItems2IsMainCol = "IsMain";
		InitializeComponent();
		cboReportType.Items.Clear();
	}

	public override void FormLoad()
	{
		base.FormLoad();
		dtItems3 = BusinessLayer.StockControl.Items.FillTree("-1", "-1", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (dtItems3 != null)
		{
			TreeFunctions.FillTree(treeLensesItems, dtItems3, TreeItems3ParentIDCol, TreeItems3IDCol, TreeItems3NameCol, "", TreeItems3IsMainCol);
		}
		((UltraToggleEditorBase)chkAll2).Checked = true;
	}

	public override void ShowReport()
	{
		GetItems();
		GetBranches();
		Items3 = TreeFunctions.GetTreeCheckedNodesIDs(treeLensesItems);
		if (Items.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى مندوب بيع  ", "There is no chosen Employee to be shown in the report, please check Employees to be shown in report");
			return;
		}
		if (Items2.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى عميل  ", "There is no chosen Clients to be shown in the report, please check Clients to be shown in report");
			return;
		}
		if (Items3.Equals(",") && (cboReportType.SelectedIndex <= -1 || !dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("NoItems")))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى صنف  ", "There is no chosen Item to be shown in the report, please check Items to be shown in report");
			return;
		}
		if (((UltraToggleEditorBase)chkAll).Checked)
		{
			Items = "-1";
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches);
		GlobalVariables.ReportDocument.SetParameterValue("@EmployeeSubAccountIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@ClientSubAccountIDs", Items2);
		if (cboReportType.SelectedIndex <= -1 || !dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("NoItems"))
		{
			GlobalVariables.ReportDocument.SetParameterValue("@ItemIDs", Items3);
		}
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
		dtSearchResult = SearchFunctions.EmployeesReport("-1", "-1", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems.GetNodeByKey(dtSearchResult.Rows[i][TreeItemsIDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	public override void btnItems2Search_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ClientsReport("-1", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems2.GetNodeByKey(dtSearchResult.Rows[i][TreeItems2IDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void treeLensesItems_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		treeLensesItems.AfterCheck -= new AfterNodeChangedEventHandler(treeLensesItems_AfterCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			SetParentState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAllLensesItems).CheckedChanged -= chkAllLensesItems_CheckedChanged;
		SetCheckBoxAllState(treeLensesItems, chkAllLensesItems);
		((UltraToggleEditorBase)chkAllLensesItems).CheckedChanged += chkAllLensesItems_CheckedChanged;
		treeLensesItems.AfterCheck += new AfterNodeChangedEventHandler(treeLensesItems_AfterCheck);
	}

	private void chkAllLensesItems_CheckedChanged(object sender, EventArgs e)
	{
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAllLensesItems).Checked, treeLensesItems);
	}

	private void txtLensesItems_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtItems3);
		dataView.RowFilter = TreeItems3NameCol + " Like '%" + ((Control)(object)txtLensesItems).Text.Trim() + "%' ";
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			treeLensesItems.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			treeLensesItems.ActiveNode = treeLensesItems.GetNodeByKey(dataView.ToTable().Rows[0][TreeItems3IDCol].ToString());
		}
	}

	private void btnLensesItemsSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ItemsReport("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "0", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			treeLensesItems.GetNodeByKey(dtSearchResult.Rows[i][TreeItems3IDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void cboReportType_ValueChanged(object sender, EventArgs e)
	{
		if (cboReportType.SelectedIndex > -1 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("NoItems"))
		{
			((Control)(object)treeLensesItems).Visible = false;
			((Control)(object)chkAllLensesItems).Visible = false;
			((Control)(object)txtLensesItems).Visible = false;
			((Control)(object)btnLensesItemsSearch).Visible = false;
		}
		else
		{
			((Control)(object)treeLensesItems).Visible = true;
			((Control)(object)chkAllLensesItems).Visible = true;
			((Control)(object)txtLensesItems).Visible = true;
			((Control)(object)btnLensesItemsSearch).Visible = true;
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
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_0908: Unknown result type (might be due to invalid IL or missing references)
		//IL_0912: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.Reports.frmSalesMenFollowUp));
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
		this.txtLensesItems = new UltraTextEditor();
		this.treeLensesItems = new UltraTree();
		this.chkAllLensesItems = new UltraCheckEditor();
		this.btnLensesItemsSearch = new UltraButton();
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
		((System.ComponentModel.ISupportInitialize)this.txtLensesItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.treeLensesItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllLensesItems).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.btnPreview, "btnPreview");
		((AppearanceBase)val).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints");
		((AppearanceBase)val).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val, "appearance1");
		((ControlBase)base.btnPreview).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(base.ultraLabel1, "ultraLabel1");
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		resources.ApplyResources(base.ultraLabel2, "ultraLabel2");
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		resources.ApplyResources(base.dtpFromDate, "dtpFromDate");
		((AppearanceBase)val2).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val2).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val2).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val2).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		resources.ApplyResources(val2, "appearance2");
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpFromDate.DateTime = new System.DateTime(2018, 6, 13, 0, 0, 0, 0);
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2018, 6, 13, 0, 0, 0, 0);
		resources.ApplyResources(base.dtpToDate, "dtpToDate");
		((AppearanceBase)val3).FontData.BoldAsString = resources.GetString("resource.BoldAsString2");
		((AppearanceBase)val3).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString2");
		((AppearanceBase)val3).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val3).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString2");
		((AppearanceBase)val3).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString2");
		resources.ApplyResources(val3, "appearance3");
		base.dtpToDate.Appearance = (AppearanceBase)(object)val3;
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
		((AppearanceBase)val4).FontData.BoldAsString = resources.GetString("resource.BoldAsString3");
		((AppearanceBase)val4).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString3");
		((AppearanceBase)val4).FontData.Name = resources.GetString("resource.Name2");
		((AppearanceBase)val4).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString3");
		((AppearanceBase)val4).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString3");
		resources.ApplyResources(val4, "appearance4");
		((TextEditorControlBase)base.cboReportType).Appearance = (AppearanceBase)(object)val4;
		base.cboReportType.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboReportType.DropDownListAlignment = (DropDownListAlignment)2;
		((TextEditorControlBase)base.cboReportType).ValueChanged += new System.EventHandler(cboReportType_ValueChanged);
		resources.ApplyResources(base.chkAll, "chkAll");
		resources.ApplyResources(base.chkAll2, "chkAll2");
		resources.ApplyResources(base.TreeItems, "TreeItems");
		resources.ApplyResources(base.TreeItems2, "TreeItems2");
		resources.ApplyResources(base.btnItemsSearch, "btnItemsSearch");
		resources.ApplyResources(base.btnItems2Search, "btnItems2Search");
		resources.ApplyResources(base.chkIsArabic, "chkIsArabic");
		resources.ApplyResources(base.txtItems, "txtItems");
		((AppearanceBase)val5).FontData.BoldAsString = resources.GetString("resource.BoldAsString4");
		((AppearanceBase)val5).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString4");
		((AppearanceBase)val5).FontData.Name = resources.GetString("resource.Name3");
		((AppearanceBase)val5).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString4");
		((AppearanceBase)val5).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString4");
		resources.ApplyResources(val5, "appearance5");
		((TextEditorControlBase)base.txtItems).Appearance = (AppearanceBase)(object)val5;
		resources.ApplyResources(base.txtItems2, "txtItems2");
		((AppearanceBase)val6).FontData.BoldAsString = resources.GetString("resource.BoldAsString5");
		((AppearanceBase)val6).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString5");
		((AppearanceBase)val6).FontData.Name = resources.GetString("resource.Name4");
		((AppearanceBase)val6).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString5");
		((AppearanceBase)val6).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString5");
		resources.ApplyResources(val6, "appearance6");
		((TextEditorControlBase)base.txtItems2).Appearance = (AppearanceBase)(object)val6;
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
		resources.ApplyResources(this.txtLensesItems, "txtLensesItems");
		((System.Windows.Forms.Control)(object)this.txtLensesItems).Name = "txtLensesItems";
		((TextEditorControlBase)this.txtLensesItems).ValueChanged += new System.EventHandler(txtLensesItems_ValueChanged);
		resources.ApplyResources(this.treeLensesItems, "treeLensesItems");
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance7");
		this.treeLensesItems.Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.treeLensesItems).Name = "treeLensesItems";
		val8.NodeStyle = (NodeStyle)1;
		this.treeLensesItems.Override = val8;
		((UltraControlBase)this.treeLensesItems).UseAppStyling = false;
		this.treeLensesItems.AfterCheck += new AfterNodeChangedEventHandler(treeLensesItems_AfterCheck);
		resources.ApplyResources(this.chkAllLensesItems, "chkAllLensesItems");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance8");
		((UltraToggleEditorBase)this.chkAllLensesItems).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.chkAllLensesItems).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllLensesItems).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllLensesItems).Name = "chkAllLensesItems";
		((UltraControlBase)this.chkAllLensesItems).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAllLensesItems).CheckedChanged += new System.EventHandler(chkAllLensesItems_CheckedChanged);
		resources.ApplyResources(this.btnLensesItemsSearch, "btnLensesItemsSearch");
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val10, "appearance9");
		((ControlBase)this.btnLensesItemsSearch).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.btnLensesItemsSearch).Name = "btnLensesItemsSearch";
		((System.Windows.Forms.Control)(object)this.btnLensesItemsSearch).Click += new System.EventHandler(btnLensesItemsSearch_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnLensesItemsSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtLensesItems);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.treeLensesItems);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllLensesItems);
		base.Name = "frmSalesMenFollowUp";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllLensesItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.treeLensesItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtLensesItems, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnLensesItemsSearch, 0);
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
		((System.ComponentModel.ISupportInitialize)this.txtLensesItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.treeLensesItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllLensesItems).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
