using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using BusinessLayer.POS;
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

public class frmSalesLns : frmReportTree2010
{
	public DataTable dtItems3;

	public string Items3;

	public string TreeItems3IDCol = "ShiftDetailID";

	public string TreeItems3NameCol = "Name";

	private IContainer components = null;

	public UltraButton btnShiftDetailsSearch;

	public UltraTextEditor txtShiftDetails;

	public UltraTree treeShiftDetails;

	protected internal UltraCheckEditor chkAllShiftDetails;

	public frmSalesLns()
	{
		dtItems = BusinessLayer.StockControl.Items.FillTree("-1", "-1", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItemsParentIDCol = "ParentID";
		TreeItemsIDCol = "ItemID";
		TreeItemsNameCol = "Name";
		TreeItemsIsMainCol = "IsMain";
		dtItems2 = SubAccounts.FillReportTreeBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItems2ParentIDCol = "ParentID";
		TreeItems2IDCol = "SubAccountID";
		TreeItems2NameCol = "SubAccountName";
		TreeItems2NumberCol = "ClientSupplierNo";
		TreeItems2IsMainCol = "IsMain";
		InitializeComponent();
		cboReportType.Items.Clear();
		cboReportType.Items.Add((object)1, GlobalVariables.IsArabic ? "مبيعات العميل مرتب بالقيمه" : "Client Sales Sorted by Value");
		cboReportType.Items.Add((object)2, GlobalVariables.IsArabic ? "مبيعات الاصناف مرتب بالقيمه" : "Items Sales Sorted by Value");
		cboReportType.Items.Add((object)3, GlobalVariables.IsArabic ? "مبيعات مجموعات الاصناف مرتب بالقيمه" : "Items Groups Sales Sorted by Value");
		cboReportType.Items.Add((object)4, GlobalVariables.IsArabic ? "مبيعات مجموعات الاصناف إجمالي مرتب بالقيمه" : "Items Groups Sales Summary Sorted by Value");
		cboReportType.SelectedIndex = 0;
	}

	public override void FillData()
	{
		GetBranches();
		dtItems3 = ShiftsDetails.FillComboByDate(Branches, dtpFromDate.DateTime.ToString(GlobalVariables.DateLongFormate), dtpToDate.DateTime.ToString(GlobalVariables.DateLongFormate), "-1");
		if (dtItems3 != null)
		{
			TreeFunctions.FillTreeOneLevel(treeShiftDetails, dtItems3, TreeItems3IDCol, TreeItems3NameCol);
		}
	}

	public override void FormLoad()
	{
		base.FormLoad();
		((UltraToggleEditorBase)chkAll2).Checked = true;
	}

	public override string GetReportName()
	{
		if (((TextEditorControlBase)cboReportType).Value.ToString() == "1")
		{
			if (((UltraToggleEditorBase)chkWithLogo).Checked)
			{
				return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_Lns_ClientSales_A.rpt" : "Rep_Lns_ClientSales_E.rpt");
			}
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_Lns_ClientSales_A_nologo.rpt" : "Rep_Lns_ClientSales_E_nologo.rpt");
		}
		if (((TextEditorControlBase)cboReportType).Value.ToString() == "2")
		{
			if (((UltraToggleEditorBase)chkWithLogo).Checked)
			{
				return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_Lns_ItemsSalesWithGroup_A.rpt" : "Rep_Lns_ItemsSalesWithGroup_E.rpt");
			}
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_Lns_ItemsSalesWithGroup_A_nologo.rpt" : "Rep_Lns_ItemsSalesWithGroup_E_nologo.rpt");
		}
		if (((TextEditorControlBase)cboReportType).Value.ToString() == "3")
		{
			if (((UltraToggleEditorBase)chkWithLogo).Checked)
			{
				return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_Lns_ItemsSalesWithGroupSummary_A.rpt" : "Rep_Lns_ItemsSalesWithGroupSummary_E.rpt");
			}
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_Lns_ItemsSalesWithGroupSummary_A_nologo.rpt" : "Rep_Lns_ItemsSalesWithGroupSummary_E_nologo.rpt");
		}
		if (((TextEditorControlBase)cboReportType).Value.ToString() == "4")
		{
			if (((UltraToggleEditorBase)chkWithLogo).Checked)
			{
				return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_Lns_ItemsSalesWithGroupSummaryTotal_A.rpt" : "Rep_Lns_ItemsSalesWithGroupSummaryTotal_E.rpt");
			}
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_Lns_ItemsSalesWithGroupSummaryTotal_A_nologo.rpt" : "Rep_Lns_ItemsSalesWithGroupSummaryTotal_E_nologo.rpt");
		}
		return base.GetReportName();
	}

	public override void ShowReport()
	{
		GetItems();
		GetBranches();
		Items3 = "";
		string text = "";
		int num = 0;
		if (((UltraToggleEditorBase)chkAllShiftDetails).Checked)
		{
			text = "All Shifts From " + dtpFromDate.DateTime.ToShortDateString() + " To " + dtpToDate.DateTime.ToShortDateString();
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)treeShiftDetails.Nodes).Count; i++)
		{
			if (treeShiftDetails.Nodes[i].CheckedState == CheckState.Checked)
			{
				num++;
				Items3 = Items3 + ((KeyedSubObjectBase)treeShiftDetails.Nodes[i]).Key + ",";
				if (!((UltraToggleEditorBase)chkAllShiftDetails).Checked && num <= 60)
				{
					text = ((num > 60) ? "" : (text + treeShiftDetails.Nodes[i].Text + " , "));
				}
			}
		}
		Items3 = "," + Items3;
		if (Items.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى صنف  ", "There is no chosen Item to be shown in the report, please check items to be shown in report");
			return;
		}
		if (Items2.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى عميل  ", "There is no chosen Clients to be shown in the report, please check Clients to be shown in report");
			return;
		}
		if (num == 0)
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى ورديه  ", "There is no chosen Shift to be shown in the report, please check Shifts to be shown in report");
			return;
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches);
		GlobalVariables.ReportDocument.SetParameterValue("@ItemIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@SubAccountIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@ShiftDetailIDs", Items3);
		GlobalVariables.ReportDocument.SetParameterValue("@ShiftNames", text);
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
		dtSearchResult = SearchFunctions.ItemsReport("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "0", IsFromServer: false);
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

	private void btnShiftDetailsSearch_Click(object sender, EventArgs e)
	{
	}

	private void treeShiftDetails_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		treeShiftDetails.AfterCheck -= new AfterNodeChangedEventHandler(treeShiftDetails_AfterCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			SetParentState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAllShiftDetails).CheckedChanged -= chkAllShiftDetails_CheckedChanged;
		SetCheckBoxAllState(treeShiftDetails, chkAllShiftDetails);
		((UltraToggleEditorBase)chkAllShiftDetails).CheckedChanged += chkAllShiftDetails_CheckedChanged;
		treeShiftDetails.AfterCheck += new AfterNodeChangedEventHandler(treeShiftDetails_AfterCheck);
	}

	private void chkAllShiftDetails_CheckedChanged(object sender, EventArgs e)
	{
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAllShiftDetails).Checked, treeShiftDetails);
	}

	private void txtShiftDetails_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtItems3);
		dataView.RowFilter = TreeItems3NameCol + " Like '%" + ((Control)(object)txtShiftDetails).Text.Trim() + "%' ";
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			treeShiftDetails.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			treeShiftDetails.ActiveNode = treeShiftDetails.GetNodeByKey(dataView.ToTable().Rows[0][TreeItems3IDCol].ToString());
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
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e2: Expected O, but got Unknown
		Appearance val = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.Reports.frmSalesLns));
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Override val9 = new Override();
		Appearance val10 = new Appearance();
		this.btnShiftDetailsSearch = new UltraButton();
		this.txtShiftDetails = new UltraTextEditor();
		this.treeShiftDetails = new UltraTree();
		this.chkAllShiftDetails = new UltraCheckEditor();
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
		((System.ComponentModel.ISupportInitialize)this.txtShiftDetails).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.treeShiftDetails).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllShiftDetails).BeginInit();
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
		resources.ApplyResources(this.btnShiftDetailsSearch, "btnShiftDetailsSearch");
		((AppearanceBase)val7).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val7, "appearance9");
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance9.FontData");
		((SubObjectBase)val7).ForceApplyResources = "|FontData";
		((ControlBase)this.btnShiftDetailsSearch).Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.btnShiftDetailsSearch).Name = "btnShiftDetailsSearch";
		((System.Windows.Forms.Control)(object)this.btnShiftDetailsSearch).Click += new System.EventHandler(btnShiftDetailsSearch_Click);
		resources.ApplyResources(this.txtShiftDetails, "txtShiftDetails");
		((System.Windows.Forms.Control)(object)this.txtShiftDetails).Name = "txtShiftDetails";
		((TextEditorControlBase)this.txtShiftDetails).ValueChanged += new System.EventHandler(txtShiftDetails_ValueChanged);
		resources.ApplyResources(this.treeShiftDetails, "treeShiftDetails");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val8).FontData, "appearance1.FontData");
		resources.ApplyResources(val8, "appearance1");
		((SubObjectBase)val8).ForceApplyResources = "FontData|";
		this.treeShiftDetails.Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.treeShiftDetails).Name = "treeShiftDetails";
		val9.NodeStyle = (NodeStyle)1;
		this.treeShiftDetails.Override = val9;
		((UltraControlBase)this.treeShiftDetails).UseAppStyling = false;
		this.treeShiftDetails.AfterCheck += new AfterNodeChangedEventHandler(treeShiftDetails_AfterCheck);
		resources.ApplyResources(this.chkAllShiftDetails, "chkAllShiftDetails");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val10).FontData, "appearance7.FontData");
		resources.ApplyResources(val10, "appearance7");
		((SubObjectBase)val10).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkAllShiftDetails).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.chkAllShiftDetails).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllShiftDetails).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllShiftDetails).Name = "chkAllShiftDetails";
		((UltraControlBase)this.chkAllShiftDetails).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAllShiftDetails).CheckedChanged += new System.EventHandler(chkAllShiftDetails_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnShiftDetailsSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtShiftDetails);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.treeShiftDetails);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllShiftDetails);
		base.Name = "frmSalesLns";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllShiftDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.treeShiftDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtShiftDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnShiftDetailsSearch, 0);
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
		((System.ComponentModel.ISupportInitialize)this.txtShiftDetails).EndInit();
		((System.ComponentModel.ISupportInitialize)this.treeShiftDetails).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllShiftDetails).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
