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

public class frmLabLensesSales : frmReportTree2010
{
	public DataTable dtItems3;

	public string Items3;

	public string TreeItems3IDCol = "ShiftDetailID";

	public string TreeItems3NameCol = "Name";

	private IContainer components = null;

	public UltraLabel lblCyl;

	public UltraLabel lblSph;

	public UltraComboEditor cboCyl;

	public UltraComboEditor cboSph;

	public UltraButton btnShiftDetailsSearch;

	public UltraTextEditor txtShiftDetails;

	public UltraTree treeShiftDetails;

	protected internal UltraCheckEditor chkAllShiftDetails;

	public frmLabLensesSales()
	{
		dtItems = BusinessLayer.StockControl.Items.FillTreeWithItemType("-1", "-1", "-1", "-1", "-1", "-1", "-1", "2", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItemsParentIDCol = "ParentID";
		TreeItemsIDCol = "ItemID";
		TreeItemsNameCol = "Name";
		TreeItemsIsMainCol = "IsMain";
		dtItems2 = SubAccounts.FillReportTreeBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItems2ParentIDCol = "ParentID";
		TreeItems2IDCol = "SubAccountID";
		TreeItems2NameCol = "SubAccountName";
		TreeItems2NumberCol = "SubAccountNumber";
		TreeItems2IsMainCol = "IsMain";
		InitializeComponent();
		cboCyl.SelectedIndex = 0;
		cboSph.SelectedIndex = 0;
		((Control)(object)lblSph).Text = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((Control)(object)lblCyl).Text = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
	}

	public override void FormLoad()
	{
		base.FormLoad();
		((UltraToggleEditorBase)chkAll2).Checked = true;
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
		if (cboSph.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار SPH +/-  ", "There is no chosen SPH +/-");
			((Control)(object)cboSph).Select();
			return;
		}
		if (cboCyl.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار Cyl +/-  ", "There is no chosen Cyl +/-");
			((Control)(object)cboCyl).Select();
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
		if (dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("WithXY"))
		{
			GlobalVariables.ReportDocument.SetParameterValue("@ShiftNames", text);
			GlobalVariables.ReportDocument.SetParameterValue("@PositiveX", (((TextEditorControlBase)cboReportType).Value.ToString() == "2") ? "-1" : ((Control)(object)cboSph).Text);
			GlobalVariables.ReportDocument.SetParameterValue("@PositiveY", (((TextEditorControlBase)cboReportType).Value.ToString() == "2") ? "-1" : ((Control)(object)cboCyl).Text);
		}
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
		dtSearchResult = SearchFunctions.ItemsReport("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "2", "0", IsFromServer: false);
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

	private void cboReportType_ValueChanged(object sender, EventArgs e)
	{
		UltraComboEditor obj = cboCyl;
		UltraComboEditor obj2 = cboSph;
		UltraLabel obj3 = lblCyl;
		bool flag = (((Control)(object)lblSph).Visible = cboReportType.SelectedIndex > -1 && dtReports.Rows.Count != 0 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("WithXY"));
		bool flag3 = (((Control)(object)obj3).Visible = flag);
		bool visible = (((Control)(object)obj2).Visible = flag3);
		((Control)(object)obj).Visible = visible;
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
		//IL_079f: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a9: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.Reports.frmLabLensesSales));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		ValueListItem val6 = new ValueListItem();
		ValueListItem val7 = new ValueListItem();
		ValueListItem val8 = new ValueListItem();
		ValueListItem val9 = new ValueListItem();
		Appearance val10 = new Appearance();
		Appearance val11 = new Appearance();
		Override val12 = new Override();
		Appearance val13 = new Appearance();
		this.lblCyl = new UltraLabel();
		this.lblSph = new UltraLabel();
		this.cboCyl = new UltraComboEditor();
		this.cboSph = new UltraComboEditor();
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
		((System.ComponentModel.ISupportInitialize)this.cboCyl).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSph).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtShiftDetails).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.treeShiftDetails).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllShiftDetails).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.ultraLabel1, "ultraLabel1");
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		resources.ApplyResources(base.ultraLabel2, "ultraLabel2");
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		((AppearanceBase)val).FontData.Name = resources.GetString("resource.Name");
		resources.ApplyResources(val, "appearance1");
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val;
		base.dtpFromDate.DateTime = new System.DateTime(2019, 1, 2, 0, 0, 0, 0);
		resources.ApplyResources(base.dtpFromDate, "dtpFromDate");
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2019, 1, 2, 0, 0, 0, 0);
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name1");
		resources.ApplyResources(val2, "appearance2");
		base.dtpToDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpToDate.DateTime = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		resources.ApplyResources(base.dtpToDate, "dtpToDate");
		base.dtpToDate.MaskInput = "dd/mm/yyyy";
		base.dtpToDate.Value = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		resources.ApplyResources(base.chkAllBranches, "chkAllBranches");
		((UltraControlBase)base.chkAllBranches).UseAppStyling = false;
		resources.ApplyResources(base.clbBranches, "clbBranches");
		((UltraControlBase)base.chkWithLogo).UseAppStyling = false;
		resources.ApplyResources(base.lblReportType, "lblReportType");
		((UltraControlBase)base.lblReportType).UseAppStyling = false;
		((AppearanceBase)val3).FontData.Name = resources.GetString("resource.Name2");
		((TextEditorControlBase)base.cboReportType).Appearance = (AppearanceBase)(object)val3;
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
		((AppearanceBase)val4).FontData.Name = resources.GetString("resource.Name3");
		resources.ApplyResources(val4, "appearance4");
		((TextEditorControlBase)base.txtItems).Appearance = (AppearanceBase)(object)val4;
		resources.ApplyResources(base.txtItems, "txtItems");
		((AppearanceBase)val5).FontData.Name = resources.GetString("resource.Name4");
		resources.ApplyResources(val5, "appearance5");
		((TextEditorControlBase)base.txtItems2).Appearance = (AppearanceBase)(object)val5;
		resources.ApplyResources(base.txtItems2, "txtItems2");
		resources.ApplyResources(this.lblCyl, "lblCyl");
		this.lblCyl.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCyl).Name = "lblCyl";
		resources.ApplyResources(this.lblSph, "lblSph");
		this.lblSph.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSph).Name = "lblSph";
		resources.ApplyResources(this.cboCyl, "cboCyl");
		this.cboCyl.AutoCompleteMode = (AutoCompleteMode)2;
		val6.CheckState = System.Windows.Forms.CheckState.Checked;
		val6.DataValue = "0";
		resources.ApplyResources(val6, "valueListItem2");
		((SubObjectBase)val6).ForceApplyResources = "";
		val7.DataValue = "1";
		resources.ApplyResources(val7, "valueListItem1");
		((SubObjectBase)val7).ForceApplyResources = "";
		this.cboCyl.Items.AddRange((ValueListItem[])(object)new ValueListItem[2] { val6, val7 });
		((System.Windows.Forms.Control)(object)this.cboCyl).Name = "cboCyl";
		((TextEditorControlBase)this.cboCyl).Nullable = false;
		resources.ApplyResources(this.cboSph, "cboSph");
		this.cboSph.AutoCompleteMode = (AutoCompleteMode)2;
		val8.CheckState = System.Windows.Forms.CheckState.Checked;
		val8.DataValue = "0";
		resources.ApplyResources(val8, "valueListItem3");
		((SubObjectBase)val8).ForceApplyResources = "";
		val9.DataValue = "1";
		resources.ApplyResources(val9, "valueListItem4");
		((SubObjectBase)val9).ForceApplyResources = "";
		this.cboSph.Items.AddRange((ValueListItem[])(object)new ValueListItem[2] { val8, val9 });
		((System.Windows.Forms.Control)(object)this.cboSph).Name = "cboSph";
		((TextEditorControlBase)this.cboSph).Nullable = false;
		resources.ApplyResources(this.btnShiftDetailsSearch, "btnShiftDetailsSearch");
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnShiftDetailsSearch).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.btnShiftDetailsSearch).Name = "btnShiftDetailsSearch";
		resources.ApplyResources(this.txtShiftDetails, "txtShiftDetails");
		((System.Windows.Forms.Control)(object)this.txtShiftDetails).Name = "txtShiftDetails";
		((TextEditorControlBase)this.txtShiftDetails).ValueChanged += new System.EventHandler(txtShiftDetails_ValueChanged);
		resources.ApplyResources(this.treeShiftDetails, "treeShiftDetails");
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		this.treeShiftDetails.Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.treeShiftDetails).Name = "treeShiftDetails";
		val12.NodeStyle = (NodeStyle)1;
		this.treeShiftDetails.Override = val12;
		((UltraControlBase)this.treeShiftDetails).UseAppStyling = false;
		this.treeShiftDetails.AfterCheck += new AfterNodeChangedEventHandler(treeShiftDetails_AfterCheck);
		resources.ApplyResources(this.chkAllShiftDetails, "chkAllShiftDetails");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		((UltraToggleEditorBase)this.chkAllShiftDetails).Appearance = (AppearanceBase)(object)val13;
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
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCyl);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSph);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCyl);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSph);
		base.Name = "frmLabLensesSales";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNew, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblSettingName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkAll, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkAll2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.TreeItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.TreeItems2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtItems2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnItemsSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnItems2Search, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkIsArabic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSph, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCyl, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSph, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCyl, 0);
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
		((System.ComponentModel.ISupportInitialize)this.cboCyl).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSph).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtShiftDetails).EndInit();
		((System.ComponentModel.ISupportInitialize)this.treeShiftDetails).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllShiftDetails).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
