using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
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

namespace ERP.POS.Reports;

public class frmSalesFollowUp : frmReportTree2010
{
	public DataTable dtItems3;

	public string Items3;

	public string TreeItems3IDCol = "ShiftDetailID";

	public string TreeItems3NameCol = "Name";

	public DataTable dtHalls;

	public string Halls;

	public string TreeHallsParentIDCol = "ParentID";

	public string TreeHallsIDCol = "RoomID";

	public string TreeHallsNameCol = "RoomName";

	public string TreeHallsIsMainCol = "IsMain";

	private IContainer components = null;

	public UltraButton btnShiftDetailsSearch;

	public UltraTextEditor txtShiftDetails;

	public UltraTree treeShiftDetails;

	protected internal UltraCheckEditor chkAllShiftDetails;

	public UltraButton btnHallsSearch;

	public UltraTextEditor txtHalls;

	public UltraTree treeHalls;

	protected internal UltraCheckEditor chkHalls;

	public frmSalesFollowUp()
	{
		TreeItemsParentIDCol = "ParentID";
		TreeItemsIDCol = "ItemID";
		TreeItemsNameCol = "Name";
		TreeItemsIsMainCol = "IsMain";
		TreeItems2ParentIDCol = "ParentID";
		TreeItems2IDCol = "ClientID";
		TreeItems2NameCol = "ClientName";
		TreeItems2NumberCol = "ClientCode";
		TreeItems2IsMainCol = "IsMain";
		InitializeComponent();
		dtItems = BusinessLayer.StockControl.Items.FillTree("-1", "-1", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItems2 = Clients.FillCombo("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		cboReportType.Items.Clear();
		cboReportType.Items.Add((object)1, GlobalVariables.IsArabic ? "مبيعات عميل" : "Client Sales");
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
	}

	public override void FillData()
	{
		GetBranches();
		dtItems3 = ShiftsDetails.FillComboByDate(Branches, dtpFromDate.DateTime.ToString(GlobalVariables.DateLongFormate), dtpToDate.DateTime.ToString(GlobalVariables.DateLongFormate), "-1");
		if (dtItems3 != null)
		{
			TreeFunctions.FillTreeOneLevel(treeShiftDetails, dtItems3, TreeItems3IDCol, TreeItems3NameCol);
		}
		dtHalls = Rooms.SelectWithoutImage("-1", Branches, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (dtHalls != null)
		{
			TreeFunctions.FillTreeOneLevel(treeHalls, dtHalls, TreeHallsIDCol, TreeHallsNameCol);
		}
	}

	public override string GetReportName()
	{
		if (((TextEditorControlBase)cboReportType).Value.ToString() == "1")
		{
			if (((UltraToggleEditorBase)chkWithLogo).Checked)
			{
				return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_POS_ClientSalesDetails_A.rpt" : "Rep_POS_ClientSalesDetails_E.rpt");
			}
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_POS_ClientSalesDetails_A_nologo.rpt" : "Rep_POS_ClientSalesDetails_E_nologo.rpt");
		}
		return base.GetReportName();
	}

	public override void ShowReport()
	{
		GetItems();
		GetBranches();
		Halls = TreeFunctions.GetTreeCheckedNodesIDs(treeHalls);
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
		if (Halls.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى صاله  ", "There is no chosen Hall to be shown in the report, please check Halls to be shown in report");
			return;
		}
		if (num == 0)
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى ورديه  ", "There is no chosen Shift to be shown in the report, please check Shifts to be shown in report");
			return;
		}
		if (((UltraToggleEditorBase)chkAll2).Checked)
		{
			Items2 = "-1";
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches);
		GlobalVariables.ReportDocument.SetParameterValue("@ItemIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@ClientIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@ShiftDetailIDs", Items3);
		GlobalVariables.ReportDocument.SetParameterValue("@RoomIDs", Halls);
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
		dtSearchResult = SearchFunctions.POSClientsSearchReport("-1", "-1", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems2.GetNodeByKey(dtSearchResult.Rows[i][TreeItems2IDCol].ToString()).CheckedState = CheckState.Checked;
		}
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

	private void chkAllShiftDetails_CheckedChanged(object sender, EventArgs e)
	{
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAllShiftDetails).Checked, treeShiftDetails);
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

	private void chkHalls_CheckedChanged(object sender, EventArgs e)
	{
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkHalls).Checked, treeHalls);
	}

	private void txtHalls_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtHalls);
		dataView.RowFilter = TreeHallsNameCol + " Like '%" + ((Control)(object)txtHalls).Text.Trim() + "%' ";
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			treeHalls.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			treeHalls.ActiveNode = treeHalls.GetNodeByKey(dataView.ToTable().Rows[0][TreeHallsIDCol].ToString());
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
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Expected O, but got Unknown
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Expected O, but got Unknown
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Expected O, but got Unknown
		//IL_062c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Expected O, but got Unknown
		Appearance val = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.Reports.frmSalesFollowUp));
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Override val9 = new Override();
		Appearance val10 = new Appearance();
		Appearance val11 = new Appearance();
		Appearance val12 = new Appearance();
		Override val13 = new Override();
		Appearance val14 = new Appearance();
		this.btnShiftDetailsSearch = new UltraButton();
		this.txtShiftDetails = new UltraTextEditor();
		this.treeShiftDetails = new UltraTree();
		this.chkAllShiftDetails = new UltraCheckEditor();
		this.btnHallsSearch = new UltraButton();
		this.txtHalls = new UltraTextEditor();
		this.treeHalls = new UltraTree();
		this.chkHalls = new UltraCheckEditor();
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
		((System.ComponentModel.ISupportInitialize)this.txtHalls).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.treeHalls).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkHalls).BeginInit();
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
		base.dtpFromDate.DateTime = new System.DateTime(2015, 10, 5, 0, 0, 0, 0);
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2015, 10, 5, 0, 0, 0, 0);
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
		resources.ApplyResources(val7, "appearance10");
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance10.FontData");
		((SubObjectBase)val7).ForceApplyResources = "|FontData";
		((ControlBase)this.btnShiftDetailsSearch).Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.btnShiftDetailsSearch).Name = "btnShiftDetailsSearch";
		resources.ApplyResources(this.txtShiftDetails, "txtShiftDetails");
		((System.Windows.Forms.Control)(object)this.txtShiftDetails).Name = "txtShiftDetails";
		((TextEditorControlBase)this.txtShiftDetails).ValueChanged += new System.EventHandler(txtShiftDetails_ValueChanged);
		resources.ApplyResources(this.treeShiftDetails, "treeShiftDetails");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val8).FontData, "appearance12.FontData");
		resources.ApplyResources(val8, "appearance12");
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
		resources.ApplyResources(((AppearanceBase)val10).FontData, "appearance11.FontData");
		resources.ApplyResources(val10, "appearance11");
		((SubObjectBase)val10).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkAllShiftDetails).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.chkAllShiftDetails).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllShiftDetails).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllShiftDetails).Name = "chkAllShiftDetails";
		((UltraControlBase)this.chkAllShiftDetails).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAllShiftDetails).CheckedChanged += new System.EventHandler(chkAllShiftDetails_CheckedChanged);
		resources.ApplyResources(this.btnHallsSearch, "btnHallsSearch");
		((AppearanceBase)val11).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val11, "appearance9");
		resources.ApplyResources(((AppearanceBase)val11).FontData, "appearance9.FontData");
		((SubObjectBase)val11).ForceApplyResources = "|FontData";
		((ControlBase)this.btnHallsSearch).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.btnHallsSearch).Name = "btnHallsSearch";
		resources.ApplyResources(this.txtHalls, "txtHalls");
		((System.Windows.Forms.Control)(object)this.txtHalls).Name = "txtHalls";
		((TextEditorControlBase)this.txtHalls).ValueChanged += new System.EventHandler(txtHalls_ValueChanged);
		resources.ApplyResources(this.treeHalls, "treeHalls");
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val12).FontData, "appearance1.FontData");
		resources.ApplyResources(val12, "appearance1");
		((SubObjectBase)val12).ForceApplyResources = "FontData|";
		this.treeHalls.Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.treeHalls).Name = "treeHalls";
		val13.NodeStyle = (NodeStyle)1;
		this.treeHalls.Override = val13;
		((UltraControlBase)this.treeHalls).UseAppStyling = false;
		resources.ApplyResources(this.chkHalls, "chkHalls");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val14).FontData, "appearance7.FontData");
		resources.ApplyResources(val14, "appearance7");
		((SubObjectBase)val14).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkHalls).Appearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.chkHalls).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkHalls).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkHalls).Name = "chkHalls";
		((UltraControlBase)this.chkHalls).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkHalls).CheckedChanged += new System.EventHandler(chkHalls_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnHallsSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtHalls);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.treeHalls);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkHalls);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnShiftDetailsSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtShiftDetails);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.treeShiftDetails);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllShiftDetails);
		base.Name = "frmSalesFollowUp";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllShiftDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.treeShiftDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtShiftDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnShiftDetailsSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkHalls, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.treeHalls, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtHalls, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnHallsSearch, 0);
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
		((System.ComponentModel.ISupportInitialize)this.txtHalls).EndInit();
		((System.ComponentModel.ISupportInitialize)this.treeHalls).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkHalls).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
