using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using BusinessLayer.MarineService;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.MarineService.Reports;

public class frmServicesSalesFollowUp : frmReportTree2010
{
	public DataTable dtVessels;

	public string Vessels;

	public string TreeVesselsParentIDCol = "ParentID";

	public string TreeVesselsIDCol = "VesselID";

	public string TreeVesselsNameCol = "VesselName";

	public string TreeVesselsIsMainCol = "IsMain";

	private IContainer components = null;

	public UltraButton btnVesselsSearch;

	public UltraTextEditor txtVessels;

	public UltraTree TreeVessels;

	protected internal UltraCheckEditor chkAllVessels;

	public frmServicesSalesFollowUp()
	{
		dtItems = Services.FillTree(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItemsParentIDCol = "ParentID";
		TreeItemsIDCol = "ServiceID";
		TreeItemsNameCol = "Name";
		TreeItemsIsMainCol = "IsMain";
		dtItems2 = SubAccounts.FillReportTreeBySubAccountTypeIDs(GlobalVariables.OwnerSubAccountTypeIDs + GlobalVariables.CharterSubAccountTypeIDs.Remove(0, 1) + GlobalVariables.AgentSubAccountTypeIDs.Remove(0, 1) + GlobalVariables.CaptainSubAccountTypeIDs.Remove(0, 1) + GlobalVariables.SeaManSubAccountTypeIDs.Remove(0, 1), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItems2ParentIDCol = "ParentID";
		TreeItems2IDCol = "SubAccountID";
		TreeItems2NameCol = "SubAccountName";
		TreeItems2NumberCol = "ClientSupplierNo";
		TreeItems2IsMainCol = "IsMain";
		InitializeComponent();
	}

	public override void FormLoad()
	{
		base.FormLoad();
		dtVessels = BusinessLayer.MarineService.Vessels.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (dtVessels != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeVessels, dtVessels, TreeVesselsIDCol, TreeVesselsNameCol);
		}
		((UltraToggleEditorBase)chkAll).Checked = true;
		((UltraToggleEditorBase)chkAll2).Checked = true;
		((UltraToggleEditorBase)chkAllVessels).Checked = true;
	}

	public override string GetReportName()
	{
		return base.GetReportName();
	}

	public override void ShowReport()
	{
		GetItems();
		GetBranches();
		if (((UltraToggleEditorBase)chkAllVessels).Checked)
		{
			Vessels = "-1";
		}
		else
		{
			Vessels = TreeFunctions.GetTreeCheckedNodesIDs(TreeVessels);
		}
		if (Items.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى خدمة  ", "There is no chosen Service to be shown in the report, please check Services to be shown in report");
			return;
		}
		if (Items2.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى عميل  ", "There is no chosen Clients to be shown in the report, please check Clients to be shown in report");
			return;
		}
		if (Vessels.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى باخرة ", "There is no chosen Vessel to be shown in the report, please check Vessels to be shown in report");
			return;
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches);
		GlobalVariables.ReportDocument.SetParameterValue("@ServiceIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@SubAccountIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@VesselIDs", Vessels);
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
		dtSearchResult = SearchFunctions.ServicesSearchReport(IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems.GetNodeByKey(dtSearchResult.Rows[i][TreeItemsIDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	public override void btnItems2Search_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.MS_ClientsReport("-1", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems2.GetNodeByKey(dtSearchResult.Rows[i][TreeItems2IDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void TreeVessels_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		TreeVessels.AfterCheck -= new AfterNodeChangedEventHandler(TreeVessels_AfterCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			SetParentState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAllVessels).CheckedChanged -= chkAllVessels_CheckedChanged;
		SetCheckBoxAllState(TreeVessels, chkAllVessels);
		((UltraToggleEditorBase)chkAllVessels).CheckedChanged += chkAllVessels_CheckedChanged;
		TreeVessels.AfterCheck += new AfterNodeChangedEventHandler(TreeVessels_AfterCheck);
	}

	private void chkAllVessels_CheckedChanged(object sender, EventArgs e)
	{
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAllVessels).Checked, TreeVessels);
	}

	private void txtVessels_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtVessels);
		dataView.RowFilter = TreeVesselsNameCol + " Like '%" + ((Control)(object)txtVessels).Text.Trim() + "%' " + ((TreeVesselsNameCol != null && TreeVesselsNameCol != "") ? (" OR " + TreeVesselsNameCol + " Like '" + ((Control)(object)txtVessels).Text.Trim() + "%' ") : "");
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TreeVessels.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			TreeVessels.ActiveNode = TreeVessels.GetNodeByKey(dataView.ToTable().Rows[0][TreeVesselsIDCol].ToString());
		}
	}

	private void btnVesselsSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.VesselsSearchReport(IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeVessels.GetNodeByKey(dtSearchResult.Rows[i][TreeVesselsIDCol].ToString()).CheckedState = CheckState.Checked;
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
		//IL_0788: Unknown result type (might be due to invalid IL or missing references)
		//IL_0792: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Reports.frmServicesSalesFollowUp));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Override val9 = new Override();
		Appearance val10 = new Appearance();
		this.btnVesselsSearch = new UltraButton();
		this.txtVessels = new UltraTextEditor();
		this.TreeVessels = new UltraTree();
		this.chkAllVessels = new UltraCheckEditor();
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
		((System.ComponentModel.ISupportInitialize)this.txtVessels).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeVessels).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllVessels).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.btnPreview, "btnPreview");
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance4.FontData");
		resources.ApplyResources(val, "appearance4");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		((ControlBase)base.btnPreview).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(base.ultraLabel1, "ultraLabel1");
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		resources.ApplyResources(base.ultraLabel2, "ultraLabel2");
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		resources.ApplyResources(base.dtpFromDate, "dtpFromDate");
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance2.FontData");
		resources.ApplyResources(val2, "appearance2");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpFromDate.DateTime = new System.DateTime(2017, 4, 10, 0, 0, 0, 0);
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2017, 4, 10, 0, 0, 0, 0);
		resources.ApplyResources(base.dtpToDate, "dtpToDate");
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance3.FontData");
		resources.ApplyResources(val3, "appearance3");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
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
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance6.FontData");
		resources.ApplyResources(val4, "appearance6");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.cboReportType).Appearance = (AppearanceBase)(object)val4;
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
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance8.FontData");
		resources.ApplyResources(val5, "appearance8");
		((SubObjectBase)val5).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtItems).Appearance = (AppearanceBase)(object)val5;
		resources.ApplyResources(base.txtItems2, "txtItems2");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance5.FontData");
		resources.ApplyResources(val6, "appearance5");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
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
		resources.ApplyResources(this.btnVesselsSearch, "btnVesselsSearch");
		((AppearanceBase)val7).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val7, "appearance9");
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance9.FontData");
		((SubObjectBase)val7).ForceApplyResources = "|FontData";
		((ControlBase)this.btnVesselsSearch).Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.btnVesselsSearch).Name = "btnVesselsSearch";
		((System.Windows.Forms.Control)(object)this.btnVesselsSearch).Click += new System.EventHandler(btnVesselsSearch_Click);
		resources.ApplyResources(this.txtVessels, "txtVessels");
		((System.Windows.Forms.Control)(object)this.txtVessels).Name = "txtVessels";
		((TextEditorControlBase)this.txtVessels).ValueChanged += new System.EventHandler(txtVessels_ValueChanged);
		resources.ApplyResources(this.TreeVessels, "TreeVessels");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance1");
		resources.ApplyResources(((AppearanceBase)val8).FontData, "appearance1.FontData");
		((SubObjectBase)val8).ForceApplyResources = "FontData|";
		this.TreeVessels.Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.TreeVessels).Name = "TreeVessels";
		val9.NodeStyle = (NodeStyle)1;
		this.TreeVessels.Override = val9;
		((UltraControlBase)this.TreeVessels).UseAppStyling = false;
		this.TreeVessels.AfterCheck += new AfterNodeChangedEventHandler(TreeVessels_AfterCheck);
		resources.ApplyResources(this.chkAllVessels, "chkAllVessels");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance7");
		resources.ApplyResources(((AppearanceBase)val10).FontData, "appearance7.FontData");
		((SubObjectBase)val10).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkAllVessels).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.chkAllVessels).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllVessels).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllVessels).Name = "chkAllVessels";
		((UltraControlBase)this.chkAllVessels).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAllVessels).CheckedChanged += new System.EventHandler(chkAllVessels_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnVesselsSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVessels);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.TreeVessels);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllVessels);
		base.Name = "frmServicesSalesFollowUp";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllVessels, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.TreeVessels, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVessels, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnVesselsSearch, 0);
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
		((System.ComponentModel.ISupportInitialize)this.txtVessels).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeVessels).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllVessels).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
