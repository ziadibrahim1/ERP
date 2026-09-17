using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using BusinessLayer.CustomsClearence;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.CustomsClearence.Reports;

public class frmOperationsFollowUp : frmReportTree2010
{
	public DataTable dtPorts;

	public string Ports;

	public string TreePortsParentIDCol = "ParentID";

	public string TreePortsIDCol = "SeaPortID";

	public string TreePortsNameCol = "SeaPortName";

	public string TreePortsIsMainCol = "IsMain";

	public DataTable dtExportTypes;

	public string ExportTypes;

	public string TreeExportTypesParentIDCol = "ParentID";

	public string TreeExportTypesIDCol = "ExportTypeID";

	public string TreeExportTypesNameCol = "ExportTypeName";

	public string TreeExportTypesIsMainCol = "IsMain";

	private IContainer components = null;

	public UltraButton btnPortsSearch;

	public UltraTextEditor txtPorts;

	public UltraTree TreePorts;

	protected internal UltraCheckEditor chkAllPorts;

	public UltraButton btnExportTypesSearch;

	public UltraTextEditor txtExportTypes;

	public UltraTree TreeExportTypes;

	protected internal UltraCheckEditor chkAllExportTypes;

	public frmOperationsFollowUp()
	{
		dtItems = SubAccounts.FillReportTreeBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItemsParentIDCol = "ParentID";
		TreeItemsIDCol = "SubAccountID";
		TreeItemsNameCol = "SubAccountName";
		TreeItemsNumberCol = "ClientSupplierNo";
		TreeItemsIsMainCol = "IsMain";
		TreeItems2ParentIDCol = "ParentID";
		TreeItems2IDCol = "OperationID";
		TreeItems2NameCol = "operationNo";
		TreeItems2IsMainCol = "IsMain";
		InitializeComponent();
	}

	public override void FormLoad()
	{
		base.FormLoad();
		dtPorts = SeaPorts.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtExportTypes = ExportsTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (dtPorts != null)
		{
			TreeFunctions.FillTreeOneLevel(TreePorts, dtPorts, TreePortsIDCol, TreePortsNameCol);
		}
		if (dtExportTypes != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeExportTypes, dtExportTypes, TreeExportTypesIDCol, TreeExportTypesNameCol);
		}
		((UltraToggleEditorBase)chkAllPorts).Checked = true;
		((UltraToggleEditorBase)chkAllExportTypes).Checked = true;
		((UltraToggleEditorBase)chkAll).Checked = true;
	}

	public override void FillData()
	{
		GetBranches();
		Items = TreeFunctions.GetTreeCheckedNodesIDs(TreeItems);
		dtItems2 = Operations.FillRepTree(Branches, dtpFromDate.DateTime.Date.ToString(GlobalVariables.DateShortFormate), dtpToDate.DateTime.ToString("MM/dd/yyyy 23:59:59"), GlobalVariables.IsArabic ? "1" : "0");
		if (dtItems2 != null)
		{
			TreeFunctions.FillTree(TreeItems2, dtItems2, TreeItems2ParentIDCol, TreeItems2IDCol, TreeItems2NameCol, TreeItems2NumberCol, TreeItems2IsMainCol);
		}
	}

	public override void ShowReport()
	{
		GetItems();
		if (((UltraToggleEditorBase)chkAllPorts).Checked)
		{
			Ports = "-1";
		}
		else
		{
			Ports = TreeFunctions.GetTreeCheckedNodesIDs(TreePorts);
		}
		if (((UltraToggleEditorBase)chkAllExportTypes).Checked)
		{
			ExportTypes = "-1";
		}
		else
		{
			ExportTypes = TreeFunctions.GetTreeCheckedNodesIDs(TreeExportTypes);
		}
		if (Items.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى عميل ليتم عرضه ", "There is no chosen Client to be shown in the report, please check Clients to be shown in report");
			return;
		}
		if (Items2.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى عملية ليتم عرضها ", "There is no chosen Operation to be shown in the report, please check Operations to be shown in report");
			return;
		}
		if (Ports.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى ميناء ", "There is no chosen SeaPort to be shown in the report, please check SeaPorts to be shown in report");
			return;
		}
		if (ExportTypes.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى نظام للشحن ", "There is no chosen Export Type to be shown in the report, please check Export Types to be shown in report");
			return;
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@OperationIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@SubAccountIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@SeaPortIDs", Ports);
		GlobalVariables.ReportDocument.SetParameterValue("@ExportTypeIDs", ExportTypes);
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

	public override void btnItems2Search_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.CSTOperationsReport(Branches, dtpFromDate.DateTime, dtpToDate.DateTime, "-1", "0");
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems2.GetNodeByKey(dtSearchResult.Rows[i][TreeItems2IDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	public override void btnItemsSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ClientsReport("-1", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems.GetNodeByKey(dtSearchResult.Rows[i][TreeItemsIDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void btnPortsSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.CSTSeaPortsSearchReport(IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreePorts.GetNodeByKey(dtSearchResult.Rows[i][TreePortsIDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void btnExportTypesSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.CSTExportTypesSearchReport(IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreePorts.GetNodeByKey(dtSearchResult.Rows[i][TreeExportTypesIDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void txtPorts_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtPorts);
		dataView.RowFilter = TreePortsNameCol + " Like '%" + ((Control)(object)txtPorts).Text.Trim() + "%' " + ((TreePortsNameCol != null && TreePortsNameCol != "") ? (" OR " + TreePortsNameCol + " Like '" + ((Control)(object)txtPorts).Text.Trim() + "%' ") : "");
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TreePorts.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			TreePorts.ActiveNode = TreePorts.GetNodeByKey(dataView.ToTable().Rows[0][TreePortsIDCol].ToString());
		}
	}

	private void txtExportTypes_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtExportTypes);
		dataView.RowFilter = TreeExportTypesNameCol + " Like '%" + ((Control)(object)txtExportTypes).Text.Trim() + "%' " + ((TreeExportTypesNameCol != null && TreeExportTypesNameCol != "") ? (" OR " + TreeExportTypesNameCol + " Like '" + ((Control)(object)txtExportTypes).Text.Trim() + "%' ") : "");
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TreeExportTypes.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			TreeExportTypes.ActiveNode = TreeExportTypes.GetNodeByKey(dataView.ToTable().Rows[0][TreeExportTypesIDCol].ToString());
		}
	}

	private void chkAllPorts_CheckedChanged(object sender, EventArgs e)
	{
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAllPorts).Checked, TreePorts);
	}

	private void chkAllExportTypes_CheckedChanged(object sender, EventArgs e)
	{
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAllExportTypes).Checked, TreeExportTypes);
	}

	private void TreePorts_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		TreePorts.AfterCheck -= new AfterNodeChangedEventHandler(TreePorts_AfterCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			SetParentState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAllPorts).CheckedChanged -= chkAllPorts_CheckedChanged;
		SetCheckBoxAllState(TreePorts, chkAllPorts);
		((UltraToggleEditorBase)chkAllPorts).CheckedChanged += chkAllPorts_CheckedChanged;
		TreePorts.AfterCheck += new AfterNodeChangedEventHandler(TreePorts_AfterCheck);
	}

	private void TreeExportTypes_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		TreeExportTypes.AfterCheck -= new AfterNodeChangedEventHandler(TreeExportTypes_AfterCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			SetParentState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAllExportTypes).CheckedChanged -= chkAllExportTypes_CheckedChanged;
		SetCheckBoxAllState(TreeExportTypes, chkAllExportTypes);
		((UltraToggleEditorBase)chkAllExportTypes).CheckedChanged += chkAllExportTypes_CheckedChanged;
		TreeExportTypes.AfterCheck += new AfterNodeChangedEventHandler(TreeExportTypes_AfterCheck);
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
		//IL_0601: Unknown result type (might be due to invalid IL or missing references)
		//IL_060b: Expected O, but got Unknown
		//IL_0828: Unknown result type (might be due to invalid IL or missing references)
		//IL_0832: Expected O, but got Unknown
		Appearance val = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.CustomsClearence.Reports.frmOperationsFollowUp));
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
		this.btnPortsSearch = new UltraButton();
		this.txtPorts = new UltraTextEditor();
		this.TreePorts = new UltraTree();
		this.chkAllPorts = new UltraCheckEditor();
		this.btnExportTypesSearch = new UltraButton();
		this.txtExportTypes = new UltraTextEditor();
		this.TreeExportTypes = new UltraTree();
		this.chkAllExportTypes = new UltraCheckEditor();
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
		((System.ComponentModel.ISupportInitialize)this.txtPorts).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreePorts).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllPorts).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExportTypes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeExportTypes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllExportTypes).BeginInit();
		base.SuspendLayout();
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance2.FontData");
		resources.ApplyResources(val, "appearance2");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val;
		base.dtpFromDate.DateTime = new System.DateTime(2019, 1, 28, 0, 0, 0, 0);
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2019, 1, 28, 0, 0, 0, 0);
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
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance11.FontData");
		resources.ApplyResources(val3, "appearance11");
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
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance4.FontData");
		resources.ApplyResources(val4, "appearance4");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtItems).Appearance = (AppearanceBase)(object)val4;
		resources.ApplyResources(base.txtItems, "txtItems");
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance5.FontData");
		resources.ApplyResources(val5, "appearance5");
		((SubObjectBase)val5).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtItems2).Appearance = (AppearanceBase)(object)val5;
		resources.ApplyResources(base.txtItems2, "txtItems2");
		resources.ApplyResources(this.btnPortsSearch, "btnPortsSearch");
		((AppearanceBase)val6).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val6, "appearance12");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance12.FontData");
		((SubObjectBase)val6).ForceApplyResources = "|FontData";
		((ControlBase)this.btnPortsSearch).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.btnPortsSearch).Name = "btnPortsSearch";
		((System.Windows.Forms.Control)(object)this.btnPortsSearch).Click += new System.EventHandler(btnPortsSearch_Click);
		resources.ApplyResources(this.txtPorts, "txtPorts");
		((System.Windows.Forms.Control)(object)this.txtPorts).Name = "txtPorts";
		((TextEditorControlBase)this.txtPorts).ValueChanged += new System.EventHandler(txtPorts_ValueChanged);
		resources.ApplyResources(this.TreePorts, "TreePorts");
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance6.FontData");
		resources.ApplyResources(val7, "appearance6");
		((SubObjectBase)val7).ForceApplyResources = "FontData|";
		this.TreePorts.Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.TreePorts).Name = "TreePorts";
		val8.NodeStyle = (NodeStyle)1;
		this.TreePorts.Override = val8;
		((UltraControlBase)this.TreePorts).UseAppStyling = false;
		this.TreePorts.AfterCheck += new AfterNodeChangedEventHandler(TreePorts_AfterCheck);
		resources.ApplyResources(this.chkAllPorts, "chkAllPorts");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val9).FontData, "appearance10.FontData");
		resources.ApplyResources(val9, "appearance10");
		((SubObjectBase)val9).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkAllPorts).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.chkAllPorts).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllPorts).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllPorts).Name = "chkAllPorts";
		((UltraControlBase)this.chkAllPorts).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAllPorts).CheckedChanged += new System.EventHandler(chkAllPorts_CheckedChanged);
		resources.ApplyResources(this.btnExportTypesSearch, "btnExportTypesSearch");
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val10, "appearance9");
		resources.ApplyResources(((AppearanceBase)val10).FontData, "appearance9.FontData");
		((SubObjectBase)val10).ForceApplyResources = "|FontData";
		((ControlBase)this.btnExportTypesSearch).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.btnExportTypesSearch).Name = "btnExportTypesSearch";
		((System.Windows.Forms.Control)(object)this.btnExportTypesSearch).Click += new System.EventHandler(btnExportTypesSearch_Click);
		resources.ApplyResources(this.txtExportTypes, "txtExportTypes");
		((System.Windows.Forms.Control)(object)this.txtExportTypes).Name = "txtExportTypes";
		((TextEditorControlBase)this.txtExportTypes).ValueChanged += new System.EventHandler(txtExportTypes_ValueChanged);
		resources.ApplyResources(this.TreeExportTypes, "TreeExportTypes");
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val11).FontData, "appearance1.FontData");
		resources.ApplyResources(val11, "appearance1");
		((SubObjectBase)val11).ForceApplyResources = "FontData|";
		this.TreeExportTypes.Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.TreeExportTypes).Name = "TreeExportTypes";
		val12.NodeStyle = (NodeStyle)1;
		this.TreeExportTypes.Override = val12;
		((UltraControlBase)this.TreeExportTypes).UseAppStyling = false;
		this.TreeExportTypes.AfterCheck += new AfterNodeChangedEventHandler(TreeExportTypes_AfterCheck);
		resources.ApplyResources(this.chkAllExportTypes, "chkAllExportTypes");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val13).FontData, "appearance7.FontData");
		resources.ApplyResources(val13, "appearance7");
		((SubObjectBase)val13).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkAllExportTypes).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.chkAllExportTypes).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllExportTypes).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllExportTypes).Name = "chkAllExportTypes";
		((UltraControlBase)this.chkAllExportTypes).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAllExportTypes).CheckedChanged += new System.EventHandler(chkAllExportTypes_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnExportTypesSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExportTypes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.TreeExportTypes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllExportTypes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPortsSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPorts);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.TreePorts);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllPorts);
		base.Name = "frmOperationsFollowUp";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllPorts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.TreePorts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPorts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPortsSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllExportTypes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.TreeExportTypes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExportTypes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnExportTypesSearch, 0);
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
		((System.ComponentModel.ISupportInitialize)this.txtPorts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreePorts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllPorts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExportTypes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeExportTypes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllExportTypes).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
