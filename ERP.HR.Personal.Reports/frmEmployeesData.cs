using System;
using System.ComponentModel;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.HR.Personal.Reports;

public class frmEmployeesData : frmReportTree2010
{
	private IContainer components = null;

	private UltraComboEditor cboMonth;

	private UltraLabel lblBDMonth;

	public UltraLabel lblAgeTo;

	public UltraLabel lblAgefrom;

	public UltraTextEditor txtAgeTo;

	public UltraTextEditor txtAgefrom;

	public frmEmployeesData()
	{
		InitializeComponent();
		dtItems = SubAccounts.FillReportTreeEmployeesBySubAccountTypeIDs(GlobalVariables.EmployeeSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		TreeItemsParentIDCol = "ParentID";
		TreeItemsIDCol = "SubAccountID";
		TreeItemsNameCol = "SubAccountName";
		TreeItemsNumberCol = "EmployeeNo";
		TreeItemsIsMainCol = "IsMain";
		dtItems2 = Areas.FillTree(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		TreeItems2ParentIDCol = "ParentID";
		TreeItems2IDCol = "ItemID";
		TreeItems2NameCol = "ItemName";
		TreeItems2NumberCol = "ItemNumber";
		TreeItems2IsMainCol = "IsMain";
		cboReportType.Items.Clear();
		cboReportType.Items.Add((object)1, GlobalVariables.IsArabic ? "طباعه كل التفاصيل" : "Print With All Details");
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
			TreeFunctions.FillTree(TreeItems2, dtItems2, TreeItems2ParentIDCol, TreeItems2IDCol, TreeItems2NameCol, TreeItems2NumberCol, TreeItems2IsMainCol);
		}
		((Control)(object)chkAllBranches).Visible = false;
		clbBranches.Visible = false;
	}

	public override void ShowReport()
	{
		GetItems();
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		if (dtReports.Rows.Count != 0 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("Area"))
		{
			if (Items2.Equals(","))
			{
				GlobalVariables.InformationMB.Show("لم تقم باختيار اى منطقة  ", "There is no chosen Area to be shown in the report, please check Area to be shown in report");
				return;
			}
			Items = "-1";
		}
		else if (Items.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى موظف  ", "There is no chosen Employee to be shown in the report, please check items to be shown in report");
			return;
		}
		if (dtReports.Rows.Count != 0 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("Birthdate") && cboMonth.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار الشهر ", "please Select Month ");
			return;
		}
		if (dtReports.Rows.Count != 0 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("ages") && (((Control)(object)txtAgefrom).Text == "" || ((Control)(object)txtAgeTo).Text == ""))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار الشهر ", "please Select Month ");
			return;
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@SubAccountIDs", Items);
		if (dtReports.Rows.Count != 0 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("Birthdate"))
		{
			GlobalVariables.ReportDocument.SetParameterValue("@Month", ((TextEditorControlBase)cboMonth).Value.ToString());
		}
		if (dtReports.Rows.Count != 0 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("Area"))
		{
			Items2 = (((UltraToggleEditorBase)chkAll2).Checked ? "-1" : Items2);
			GlobalVariables.ReportDocument.SetParameterValue("@AreaIDs", Items2);
		}
		if (dtReports.Rows.Count != 0 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("ages"))
		{
			GlobalVariables.ReportDocument.SetParameterValue("@FromAge", ((TextEditorControlBase)txtAgefrom).Value.ToString());
			GlobalVariables.ReportDocument.SetParameterValue("@ToAge", ((TextEditorControlBase)txtAgeTo).Value.ToString());
		}
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

	public override void btnItemsSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.EmployeesReport("-1", "-1", IsFromServer: true);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems.GetNodeByKey(dtSearchResult.Rows[i][TreeItemsIDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void cboReportType_ValueChanged(object sender, EventArgs e)
	{
		if (cboReportType.SelectedIndex > -1 && dtReports.Rows.Count > 0 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("Birthdate"))
		{
			((Control)(object)lblBDMonth).Visible = true;
			((Control)(object)cboMonth).Visible = true;
		}
		else
		{
			((Control)(object)lblBDMonth).Visible = false;
			((Control)(object)cboMonth).Visible = false;
		}
		if (cboReportType.SelectedIndex > -1 && dtReports.Rows.Count > 0 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("ages"))
		{
			((Control)(object)lblAgefrom).Visible = true;
			((Control)(object)lblAgeTo).Visible = true;
			((Control)(object)txtAgefrom).Visible = true;
			((Control)(object)txtAgeTo).Visible = true;
		}
		else
		{
			((Control)(object)lblAgefrom).Visible = false;
			((Control)(object)lblAgeTo).Visible = false;
			((Control)(object)txtAgefrom).Visible = false;
			((Control)(object)txtAgeTo).Visible = false;
		}
		if (cboReportType.SelectedIndex > -1 && dtReports.Rows.Count > 0)
		{
			if (dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("Area"))
			{
				UltraCheckEditor obj = chkAll2;
				UltraTree treeItems = TreeItems2;
				bool flag = (((Control)(object)txtItems2).Visible = true);
				bool visible = (((Control)(object)treeItems).Visible = flag);
				((Control)(object)obj).Visible = visible;
				UltraCheckEditor obj2 = chkAll;
				UltraTree treeItems2 = TreeItems;
				UltraTextEditor obj3 = txtItems;
				bool flag4 = (((Control)(object)btnItemsSearch).Visible = false);
				flag = (((Control)(object)obj3).Visible = flag4);
				visible = (((Control)(object)treeItems2).Visible = flag);
				((Control)(object)obj2).Visible = visible;
			}
			else
			{
				UltraCheckEditor obj4 = chkAll2;
				UltraTree treeItems3 = TreeItems2;
				bool flag = (((Control)(object)txtItems2).Visible = false);
				bool visible = (((Control)(object)treeItems3).Visible = flag);
				((Control)(object)obj4).Visible = visible;
				UltraCheckEditor obj5 = chkAll;
				UltraTree treeItems4 = TreeItems;
				UltraTextEditor obj6 = txtItems;
				bool flag4 = (((Control)(object)btnItemsSearch).Visible = true);
				flag = (((Control)(object)obj6).Visible = flag4);
				visible = (((Control)(object)treeItems4).Visible = flag);
				((Control)(object)obj5).Visible = visible;
			}
		}
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
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
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Expected O, but got Unknown
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HR.Personal.Reports.frmEmployeesData));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		ValueListItem val6 = new ValueListItem();
		ValueListItem val7 = new ValueListItem();
		ValueListItem val8 = new ValueListItem();
		ValueListItem val9 = new ValueListItem();
		ValueListItem val10 = new ValueListItem();
		ValueListItem val11 = new ValueListItem();
		ValueListItem val12 = new ValueListItem();
		ValueListItem val13 = new ValueListItem();
		ValueListItem val14 = new ValueListItem();
		ValueListItem val15 = new ValueListItem();
		ValueListItem val16 = new ValueListItem();
		ValueListItem val17 = new ValueListItem();
		this.cboMonth = new UltraComboEditor();
		this.lblBDMonth = new UltraLabel();
		this.lblAgeTo = new UltraLabel();
		this.lblAgefrom = new UltraLabel();
		this.txtAgeTo = new UltraTextEditor();
		this.txtAgefrom = new UltraTextEditor();
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
		((System.ComponentModel.ISupportInitialize)this.cboMonth).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAgeTo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAgefrom).BeginInit();
		base.SuspendLayout();
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		resources.ApplyResources(base.ultraLabel1, "ultraLabel1");
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		resources.ApplyResources(base.ultraLabel2, "ultraLabel2");
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance2.FontData");
		resources.ApplyResources(val, "appearance2");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val;
		base.dtpFromDate.DateTime = new System.DateTime(2016, 3, 29, 0, 0, 0, 0);
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2016, 3, 29, 0, 0, 0, 0);
		resources.ApplyResources(base.dtpFromDate, "dtpFromDate");
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance3.FontData");
		resources.ApplyResources(val2, "appearance3");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		base.dtpToDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpToDate.DateTime = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		base.dtpToDate.MaskInput = "dd/mm/yyyy";
		base.dtpToDate.Value = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		resources.ApplyResources(base.dtpToDate, "dtpToDate");
		resources.ApplyResources(base.chkAllBranches, "chkAllBranches");
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
		((TextEditorControlBase)base.cboReportType).ValueChanged += new System.EventHandler(cboReportType_ValueChanged);
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
		((TextEditorControlBase)this.cboMonth).AlwaysInEditMode = true;
		resources.ApplyResources(this.cboMonth, "cboMonth");
		this.cboMonth.AutoCompleteMode = (AutoCompleteMode)4;
		val6.DataValue = (byte)1;
		resources.ApplyResources(val6, "valueListItem1");
		((SubObjectBase)val6).ForceApplyResources = "";
		val7.DataValue = "2";
		resources.ApplyResources(val7, "valueListItem2");
		((SubObjectBase)val7).ForceApplyResources = "";
		val8.DataValue = "3";
		resources.ApplyResources(val8, "valueListItem3");
		((SubObjectBase)val8).ForceApplyResources = "";
		val9.DataValue = "4";
		resources.ApplyResources(val9, "valueListItem4");
		((SubObjectBase)val9).ForceApplyResources = "";
		val10.DataValue = "5";
		resources.ApplyResources(val10, "valueListItem5");
		((SubObjectBase)val10).ForceApplyResources = "";
		val11.DataValue = "6";
		resources.ApplyResources(val11, "valueListItem6");
		((SubObjectBase)val11).ForceApplyResources = "";
		val12.DataValue = "7";
		resources.ApplyResources(val12, "valueListItem7");
		((SubObjectBase)val12).ForceApplyResources = "";
		val13.DataValue = "8";
		resources.ApplyResources(val13, "valueListItem8");
		((SubObjectBase)val13).ForceApplyResources = "";
		val14.DataValue = "9";
		resources.ApplyResources(val14, "valueListItem9");
		((SubObjectBase)val14).ForceApplyResources = "";
		val15.DataValue = "10";
		resources.ApplyResources(val15, "valueListItem10");
		((SubObjectBase)val15).ForceApplyResources = "";
		val16.DataValue = "11";
		resources.ApplyResources(val16, "valueListItem11");
		((SubObjectBase)val16).ForceApplyResources = "";
		val17.DataValue = "12";
		resources.ApplyResources(val17, "valueListItem12");
		((SubObjectBase)val17).ForceApplyResources = "";
		this.cboMonth.Items.AddRange((ValueListItem[])(object)new ValueListItem[12]
		{
			val6, val7, val8, val9, val10, val11, val12, val13, val14, val15,
			val16, val17
		});
		((System.Windows.Forms.Control)(object)this.cboMonth).Name = "cboMonth";
		resources.ApplyResources(this.lblBDMonth, "lblBDMonth");
		((System.Windows.Forms.Control)(object)this.lblBDMonth).Name = "lblBDMonth";
		resources.ApplyResources(this.lblAgeTo, "lblAgeTo");
		((System.Windows.Forms.Control)(object)this.lblAgeTo).Name = "lblAgeTo";
		resources.ApplyResources(this.lblAgefrom, "lblAgefrom");
		((System.Windows.Forms.Control)(object)this.lblAgefrom).Name = "lblAgefrom";
		resources.ApplyResources(this.txtAgeTo, "txtAgeTo");
		((System.Windows.Forms.Control)(object)this.txtAgeTo).Name = "txtAgeTo";
		((System.Windows.Forms.Control)(object)this.txtAgeTo).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtAgefrom, "txtAgefrom");
		((System.Windows.Forms.Control)(object)this.txtAgefrom).Name = "txtAgefrom";
		((System.Windows.Forms.Control)(object)this.txtAgefrom).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAgeTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAgefrom);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAgeTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAgefrom);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboMonth);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBDMonth);
		base.Name = "frmEmployeesData";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBDMonth, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboMonth, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAgefrom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAgeTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAgefrom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAgeTo, 0);
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
		((System.ComponentModel.ISupportInitialize)this.cboMonth).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAgeTo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAgefrom).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
