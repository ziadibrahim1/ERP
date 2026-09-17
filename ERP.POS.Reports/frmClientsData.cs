using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.General;
using BusinessLayer.POS;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.POS.Reports;

public class frmClientsData : frmReportTree2010
{
	public DataTable dtItems3;

	public string Areas;

	public string TreeItems3IDCol = "AreaID";

	public string TreeItems3NameCol = (GlobalVariables.IsArabic ? "AreaNameAr" : "AreaNameEn");

	private IContainer components = null;

	public UltraButton btnAreasSearch;

	public UltraTextEditor txtAreas;

	public UltraTree treeAreas;

	protected internal UltraCheckEditor chkAllAreas;

	public frmClientsData()
	{
		InitializeComponent();
		TreeItemsIDCol = "CityID";
		TreeItemsNameCol = "CityName";
		dtItems = Cities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItems2 = Clients.FillCombo("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItems2ParentIDCol = "ParentID";
		TreeItems2IDCol = "ClientID";
		TreeItems2NameCol = "ClientName";
		TreeItems2NumberCol = "ClientCode";
		TreeItems2IsMainCol = "IsMain";
	}

	public override void FormLoad()
	{
		if (dtItems != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeItems, dtItems, TreeItemsIDCol, TreeItemsNameCol);
		}
		if (dtItems2 != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeItems2, dtItems2, TreeItems2IDCol, TreeItems2NameCol);
		}
	}

	public override void FillData()
	{
		GetItems();
		((UltraToggleEditorBase)chkAllAreas).Checked = false;
		if (Items != null)
		{
			Items = TreeFunctions.GetTreeCheckedNodesIDs(TreeItems);
			if (((UltraToggleEditorBase)chkAll).Checked)
			{
				Items = "-1";
			}
			dtItems3 = BusinessLayer.General.Areas.SelectByCityIDs(Items, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		}
		if (dtItems3 != null)
		{
			TreeFunctions.FillTreeOneLevel(treeAreas, dtItems3, TreeItems3IDCol, TreeItems3NameCol);
		}
	}

	public override void ShowReport()
	{
		GetItems();
		Areas = TreeFunctions.GetTreeCheckedNodesIDs(treeAreas);
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		if (Items2.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى عميل  ", "There is no chosen Clients to be shown in the report, please check Clients to be shown in report");
			return;
		}
		if (Areas.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى منطقة  ", "There is no chosen Areas to be shown in the report, please check Area to be shown in report");
			return;
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@ClientIDs", ((UltraToggleEditorBase)chkAll).Checked ? "-1" : Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@AreaIDs", ((UltraToggleEditorBase)chkAllAreas).Checked ? "-1" : Areas);
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

	public override void btnItems2Search_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.POSClientsSearchReport("-1", "-1", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems2.GetNodeByKey(dtSearchResult.Rows[i][TreeItems2IDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	public override void Tree_AfterCheck(object sender, NodeEventArgs e)
	{
		base.Tree_AfterCheck(sender, e);
		if (!IsLoading)
		{
			FillData();
		}
	}

	public override void chkAll_CheckedChanged(object sender, EventArgs e)
	{
		base.chkAll_CheckedChanged(sender, e);
		if (!IsLoading)
		{
			FillData();
		}
	}

	private void treeAreas_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		treeAreas.AfterCheck -= new AfterNodeChangedEventHandler(treeAreas_AfterCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			SetParentState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAllAreas).CheckedChanged -= chkAllAreas_CheckedChanged;
		SetCheckBoxAllState(treeAreas, chkAllAreas);
		((UltraToggleEditorBase)chkAllAreas).CheckedChanged += chkAllAreas_CheckedChanged;
		treeAreas.AfterCheck += new AfterNodeChangedEventHandler(treeAreas_AfterCheck);
	}

	private void txtAreas_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtItems3);
		dataView.RowFilter = TreeItems3NameCol + " Like '%" + ((Control)(object)txtAreas).Text.Trim() + "%' ";
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			treeAreas.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			treeAreas.ActiveNode = treeAreas.GetNodeByKey(dataView.ToTable().Rows[0][TreeItems3IDCol].ToString());
		}
	}

	private void chkAllAreas_CheckedChanged(object sender, EventArgs e)
	{
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAllAreas).Checked, treeAreas);
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
		//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fa: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.Reports.frmClientsData));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Override val8 = new Override();
		Appearance val9 = new Appearance();
		this.btnAreasSearch = new UltraButton();
		this.txtAreas = new UltraTextEditor();
		this.treeAreas = new UltraTree();
		this.chkAllAreas = new UltraCheckEditor();
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
		((System.ComponentModel.ISupportInitialize)this.txtAreas).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.treeAreas).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllAreas).BeginInit();
		base.SuspendLayout();
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		resources.ApplyResources(base.ultraLabel1, "ultraLabel1");
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		resources.ApplyResources(base.ultraLabel2, "ultraLabel2");
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance2.FontData");
		((AppearanceBase)val).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val, "appearance2");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val;
		base.dtpFromDate.DateTime = new System.DateTime(2019, 11, 5, 0, 0, 0, 0);
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2019, 11, 5, 0, 0, 0, 0);
		resources.ApplyResources(base.dtpFromDate, "dtpFromDate");
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance3.FontData");
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val2, "appearance3");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		base.dtpToDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpToDate.DateTime = new System.DateTime(2011, 9, 8, 23, 59, 59, 0);
		base.dtpToDate.MaskInput = "dd/mm/yyyy";
		base.dtpToDate.Value = new System.DateTime(2011, 9, 8, 23, 59, 59, 0);
		resources.ApplyResources(base.dtpToDate, "dtpToDate");
		((UltraControlBase)base.chkAllBranches).UseAppStyling = false;
		resources.ApplyResources(base.chkAllBranches, "chkAllBranches");
		resources.ApplyResources(base.clbBranches, "clbBranches");
		((UltraControlBase)base.chkWithLogo).UseAppStyling = false;
		((UltraControlBase)base.lblReportType).UseAppStyling = false;
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance4.FontData");
		((AppearanceBase)val3).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val3, "appearance4");
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
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance6.FontData");
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val4, "appearance6");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtItems).Appearance = (AppearanceBase)(object)val4;
		resources.ApplyResources(base.txtItems, "txtItems");
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance5.FontData");
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val5, "appearance5");
		((SubObjectBase)val5).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtItems2).Appearance = (AppearanceBase)(object)val5;
		resources.ApplyResources(base.txtItems2, "txtItems2");
		resources.ApplyResources(this.btnAreasSearch, "btnAreasSearch");
		((AppearanceBase)val6).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val6, "appearance9");
		((SubObjectBase)val6).ForceApplyResources = "";
		((ControlBase)this.btnAreasSearch).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.btnAreasSearch).Name = "btnAreasSearch";
		resources.ApplyResources(this.txtAreas, "txtAreas");
		((System.Windows.Forms.Control)(object)this.txtAreas).Name = "txtAreas";
		((TextEditorControlBase)this.txtAreas).ValueChanged += new System.EventHandler(txtAreas_ValueChanged);
		resources.ApplyResources(this.treeAreas, "treeAreas");
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance1");
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance1.FontData");
		((SubObjectBase)val7).ForceApplyResources = "|FontData";
		this.treeAreas.Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.treeAreas).Name = "treeAreas";
		val8.NodeStyle = (NodeStyle)1;
		this.treeAreas.Override = val8;
		((UltraControlBase)this.treeAreas).UseAppStyling = false;
		this.treeAreas.AfterCheck += new AfterNodeChangedEventHandler(treeAreas_AfterCheck);
		resources.ApplyResources(this.chkAllAreas, "chkAllAreas");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance7");
		resources.ApplyResources(((AppearanceBase)val9).FontData, "appearance7.FontData");
		((SubObjectBase)val9).ForceApplyResources = "|FontData";
		((UltraToggleEditorBase)this.chkAllAreas).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.chkAllAreas).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllAreas).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllAreas).Name = "chkAllAreas";
		((UltraControlBase)this.chkAllAreas).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAllAreas).CheckedChanged += new System.EventHandler(chkAllAreas_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAreasSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAreas);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.treeAreas);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllAreas);
		base.Name = "frmClientsData";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllAreas, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.treeAreas, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAreas, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAreasSearch, 0);
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
		((System.ComponentModel.ISupportInitialize)this.txtAreas).EndInit();
		((System.ComponentModel.ISupportInitialize)this.treeAreas).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllAreas).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
