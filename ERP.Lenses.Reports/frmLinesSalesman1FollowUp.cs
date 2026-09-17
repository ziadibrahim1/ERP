using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.Lenses.Reports;

public class frmLinesSalesman1FollowUp : frmReportTree2010
{
	public DataTable dtItems3;

	public string Items3;

	public string TreeItems3IDCol = "LineID";

	public string TreeItems3NameCol = "LineName";

	private IContainer components = null;

	public UltraTextEditor txtLines;

	public UltraTree treeLines;

	protected internal UltraCheckEditor chkAllLines;

	public UltraButton btnLinesSearch;

	public frmLinesSalesman1FollowUp()
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
		dtItems3 = Lines.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (dtItems3 != null)
		{
			TreeFunctions.FillTreeOneLevel(treeLines, dtItems3, TreeItems3IDCol, TreeItems3NameCol);
		}
		((UltraToggleEditorBase)chkAll2).Checked = true;
	}

	public override void ShowReport()
	{
		GetItems();
		GetBranches();
		Items3 = "";
		for (int i = 0; i < ((DisposableObjectCollectionBase)treeLines.Nodes).Count; i++)
		{
			if (treeLines.Nodes[i].CheckedState == CheckState.Checked)
			{
				Items3 = Items3 + ((KeyedSubObjectBase)treeLines.Nodes[i]).Key + ",";
			}
		}
		Items3 = "," + Items3;
		if (Items.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى مندوب بيع  ", "There is no chosen Employee to be shown in the report, please check Employees to be shown in report");
			return;
		}
		if (Items2.Equals(",") && (cboReportType.SelectedIndex <= -1 || !dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("G_Lines")))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى عميل  ", "There is no chosen Clients to be shown in the report, please check Clients to be shown in report");
			return;
		}
		if (Items3.Equals(",") && (cboReportType.SelectedIndex <= -1 || !dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("DefaultSalesman")))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى خط  ", "There is no chosen Line to be shown in the report, please check Lines to be shown in report");
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
		GlobalVariables.ReportDocument.SetParameterValue("@LineIDs", Items3);
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

	private void treeLines_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		treeLines.AfterCheck -= new AfterNodeChangedEventHandler(treeLines_AfterCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			SetParentState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAllLines).CheckedChanged -= chkAllLines_CheckedChanged;
		SetCheckBoxAllState(treeLines, chkAllLines);
		((UltraToggleEditorBase)chkAllLines).CheckedChanged += chkAllLines_CheckedChanged;
		treeLines.AfterCheck += new AfterNodeChangedEventHandler(treeLines_AfterCheck);
	}

	private void chkAllLines_CheckedChanged(object sender, EventArgs e)
	{
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAllLines).Checked, treeLines);
	}

	private void txtLines_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtItems3);
		dataView.RowFilter = TreeItems3NameCol + " Like '%" + ((Control)(object)txtLines).Text.Trim() + "%' ";
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			treeLines.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			treeLines.ActiveNode = treeLines.GetNodeByKey(dataView.ToTable().Rows[0][TreeItems3IDCol].ToString());
		}
	}

	private void btnLinesSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.GLinesReport(IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			treeLines.GetNodeByKey(dtSearchResult.Rows[i][TreeItems3IDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void cboReportType_ValueChanged(object sender, EventArgs e)
	{
		if (cboReportType.SelectedIndex > -1 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("G_Lines"))
		{
			clbBranches.Visible = false;
			((Control)(object)chkAllBranches).Visible = false;
			((Control)(object)chkAll2).Visible = false;
			((Control)(object)TreeItems2).Visible = false;
			((Control)(object)txtItems2).Visible = false;
			((Control)(object)btnItems2Search).Visible = false;
			((Control)(object)chkAllLines).Visible = true;
			((Control)(object)treeLines).Visible = true;
			((Control)(object)txtLines).Visible = true;
			((Control)(object)btnLinesSearch).Visible = true;
		}
		else if (cboReportType.SelectedIndex > -1 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("DefaultSalesman"))
		{
			clbBranches.Visible = true;
			((Control)(object)chkAllBranches).Visible = true;
			((Control)(object)chkAll2).Visible = true;
			((Control)(object)TreeItems2).Visible = true;
			((Control)(object)txtItems2).Visible = true;
			((Control)(object)btnItems2Search).Visible = true;
			((Control)(object)chkAllLines).Visible = false;
			((Control)(object)treeLines).Visible = false;
			((Control)(object)txtLines).Visible = false;
			((Control)(object)btnLinesSearch).Visible = false;
		}
		else
		{
			clbBranches.Visible = true;
			((Control)(object)chkAllBranches).Visible = true;
			((Control)(object)chkAll2).Visible = true;
			((Control)(object)TreeItems2).Visible = true;
			((Control)(object)txtItems2).Visible = true;
			((Control)(object)btnItems2Search).Visible = true;
			((Control)(object)chkAllLines).Visible = true;
			((Control)(object)treeLines).Visible = true;
			((Control)(object)txtLines).Visible = true;
			((Control)(object)btnLinesSearch).Visible = true;
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
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Expected O, but got Unknown
		Appearance val = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.Reports.frmLinesSalesman1FollowUp));
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Override val8 = new Override();
		Appearance val9 = new Appearance();
		Appearance val10 = new Appearance();
		this.txtLines = new UltraTextEditor();
		this.treeLines = new UltraTree();
		this.chkAllLines = new UltraCheckEditor();
		this.btnLinesSearch = new UltraButton();
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
		((System.ComponentModel.ISupportInitialize)this.txtLines).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.treeLines).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllLines).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance4.FontData");
		resources.ApplyResources(val, "appearance4");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		((ControlBase)base.btnPreview).Appearance = (AppearanceBase)(object)val;
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance2.FontData");
		resources.ApplyResources(val2, "appearance2");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpFromDate.DateTime = new System.DateTime(2018, 6, 13, 0, 0, 0, 0);
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2018, 6, 13, 0, 0, 0, 0);
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance3.FontData");
		resources.ApplyResources(val3, "appearance3");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
		base.dtpToDate.Appearance = (AppearanceBase)(object)val3;
		base.dtpToDate.DateTime = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		base.dtpToDate.MaskInput = "dd/mm/yyyy";
		base.dtpToDate.Value = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		resources.ApplyResources(base.chkAllBranches, "chkAllBranches");
		((UltraControlBase)base.chkAllBranches).UseAppStyling = false;
		((UltraControlBase)base.chkWithLogo).UseAppStyling = false;
		((UltraControlBase)base.lblReportType).UseAppStyling = false;
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance6.FontData");
		resources.ApplyResources(val4, "appearance6");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.cboReportType).Appearance = (AppearanceBase)(object)val4;
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
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance8.FontData");
		resources.ApplyResources(val5, "appearance8");
		((SubObjectBase)val5).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtItems).Appearance = (AppearanceBase)(object)val5;
		resources.ApplyResources(base.txtItems, "txtItems");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance5.FontData");
		resources.ApplyResources(val6, "appearance5");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtItems2).Appearance = (AppearanceBase)(object)val6;
		resources.ApplyResources(base.txtItems2, "txtItems2");
		resources.ApplyResources(this.txtLines, "txtLines");
		((System.Windows.Forms.Control)(object)this.txtLines).Name = "txtLines";
		((TextEditorControlBase)this.txtLines).ValueChanged += new System.EventHandler(txtLines_ValueChanged);
		resources.ApplyResources(this.treeLines, "treeLines");
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance10.FontData");
		resources.ApplyResources(val7, "appearance10");
		((SubObjectBase)val7).ForceApplyResources = "FontData|";
		this.treeLines.Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.treeLines).Name = "treeLines";
		val8.NodeStyle = (NodeStyle)1;
		this.treeLines.Override = val8;
		((UltraControlBase)this.treeLines).UseAppStyling = false;
		this.treeLines.AfterCheck += new AfterNodeChangedEventHandler(treeLines_AfterCheck);
		resources.ApplyResources(this.chkAllLines, "chkAllLines");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val9).FontData, "appearance7.FontData");
		resources.ApplyResources(val9, "appearance7");
		((SubObjectBase)val9).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkAllLines).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.chkAllLines).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllLines).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllLines).Name = "chkAllLines";
		((UltraControlBase)this.chkAllLines).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAllLines).CheckedChanged += new System.EventHandler(chkAllLines_CheckedChanged);
		resources.ApplyResources(this.btnLinesSearch, "btnLinesSearch");
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val10, "appearance9");
		resources.ApplyResources(((AppearanceBase)val10).FontData, "appearance9.FontData");
		((SubObjectBase)val10).ForceApplyResources = "|FontData";
		((ControlBase)this.btnLinesSearch).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.btnLinesSearch).Name = "btnLinesSearch";
		((System.Windows.Forms.Control)(object)this.btnLinesSearch).Click += new System.EventHandler(btnLinesSearch_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnLinesSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtLines);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.treeLines);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllLines);
		base.Name = "frmLinesSalesman1FollowUp";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllLines, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.treeLines, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtLines, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnLinesSearch, 0);
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
		((System.ComponentModel.ISupportInitialize)this.txtLines).EndInit();
		((System.ComponentModel.ISupportInitialize)this.treeLines).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllLines).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
