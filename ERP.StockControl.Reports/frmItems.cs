using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.StockControl;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;

namespace ERP.StockControl.Reports;

public class frmItems : frmReportTree2010
{
	private IContainer components = null;

	private UltraCheckEditor chkIsProductionItem;

	private UltraCheckEditor chkIsDirectItem;

	private UltraCheckEditor chkIsSalesItem;

	private UltraCheckEditor chkIsService;

	private UltraCheckEditor chkIsRecipe;

	private UltraCheckEditor chkIsItem;

	private UltraCheckEditor chkIsActive;

	private UltraCheckEditor chkIsNotActive;

	private UltraCheckEditor chkIsNotProductionItem;

	private UltraCheckEditor chkIsNotDirectItem;

	private UltraCheckEditor chkIsNotSalesItem;

	public frmItems()
	{
		dtItems = BusinessLayer.StockControl.Items.FillTree("-1", "-1", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItemsParentIDCol = "ParentID";
		TreeItemsIDCol = "ItemID";
		TreeItemsNameCol = "Name";
		TreeItemsIsMainCol = "IsMain";
		InitializeComponent();
		cboReportType.Items.Clear();
		cboReportType.Items.Add((object)1, GlobalVariables.IsArabic ? " الأصناف" : "Items");
		cboReportType.SelectedIndex = 0;
	}

	public override void FormLoad()
	{
		base.FormLoad();
		((Control)(object)chkAllBranches).Visible = false;
		clbBranches.Visible = false;
	}

	public override void FillData()
	{
		dtItems = BusinessLayer.StockControl.Items.FillTree((!((UltraToggleEditorBase)chkIsItem).Checked) ? "0" : ((((UltraToggleEditorBase)chkIsRecipe).Checked || ((UltraToggleEditorBase)chkIsService).Checked) ? "-1" : "1"), (!((UltraToggleEditorBase)chkIsRecipe).Checked) ? "0" : ((((UltraToggleEditorBase)chkIsItem).Checked || ((UltraToggleEditorBase)chkIsService).Checked) ? "-1" : "1"), (!((UltraToggleEditorBase)chkIsService).Checked) ? "0" : ((((UltraToggleEditorBase)chkIsItem).Checked || ((UltraToggleEditorBase)chkIsRecipe).Checked) ? "-1" : "1"), (!((UltraToggleEditorBase)chkIsSalesItem).Checked) ? ((!((UltraToggleEditorBase)chkIsNotSalesItem).Checked) ? "-1" : "0") : (((UltraToggleEditorBase)chkIsNotSalesItem).Checked ? "-1" : "1"), (!((UltraToggleEditorBase)chkIsActive).Checked) ? ((!((UltraToggleEditorBase)chkIsNotActive).Checked) ? "-1" : "0") : (((UltraToggleEditorBase)chkIsNotActive).Checked ? "-1" : "1"), (!((UltraToggleEditorBase)chkIsDirectItem).Checked) ? ((!((UltraToggleEditorBase)chkIsNotDirectItem).Checked) ? "-1" : "0") : (((UltraToggleEditorBase)chkIsNotDirectItem).Checked ? "-1" : "1"), (!((UltraToggleEditorBase)chkIsProductionItem).Checked) ? ((!((UltraToggleEditorBase)chkIsNotProductionItem).Checked) ? "-1" : "0") : (((UltraToggleEditorBase)chkIsNotProductionItem).Checked ? "-1" : "1"), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (dtItems != null)
		{
			TreeFunctions.FillTree(TreeItems, dtItems, TreeItemsParentIDCol, TreeItemsIDCol, TreeItemsNameCol, TreeItemsNumberCol, TreeItemsIsMainCol);
		}
	}

	public override string GetReportName()
	{
		if (((TextEditorControlBase)cboReportType).Value.ToString() == "1")
		{
			if (((UltraToggleEditorBase)chkWithLogo).Checked)
			{
				return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_SC_Items_A.rpt" : "Rep_SC_Items_E.rpt");
			}
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_SC_Items_A_nologo.rpt" : "Rep_SC_Items_E_nologo.rpt");
		}
		return base.GetReportName();
	}

	public override void ShowReport()
	{
		GetItems();
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		if (Items.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى صنف  ", "There is no choosen Item to be shown in the report, please check items to be shown in report");
			return;
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@ItemIDs", Items);
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

	public override void ReportDoubleClick(string GroupNamePath, frmReporViwer frmViewer)
	{
		if (!(GroupNamePath != ""))
		{
			return;
		}
		string text = GroupNamePath.Substring(0, GroupNamePath.IndexOf("ItemID") + 9);
		if (text.IndexOf("ItemID") < 0)
		{
			return;
		}
		string itemIDs = GroupNamePath.Replace(text, "").Replace(",", "").Replace('[', ',')
			.Replace(']', ',');
		frmItemCard frmItemCard2 = new frmItemCard();
		try
		{
			GlobalVariables.ReportDocument = new ReportDocument();
			frmItemCard2.ShowReport(Branches, itemIDs, Items2, dtpFromDate.DateTime, dtpToDate.DateTime, ((UltraToggleEditorBase)chkIsArabic).Checked, (cboReportType.SelectedIndex == 1) ? 1 : 0);
			RefreshReport(frmViewer);
		}
		catch (Exception ex)
		{
			if (ex.Message == "Load report failed.")
			{
				GlobalVariables.InformationMB.Show("مسار التقارير غير سليم \r\n برجاء مراجعة مسار التقارير من إعدادات النظام", "Invalied Reports Path \r\n Please Check Report Path from System Tools");
			}
			else
			{
				GlobalVariables.InformationMB.Show(ex.Message);
			}
		}
	}

	public void RefreshReport(frmReporViwer frmViewer)
	{
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		int currentPageNumber = frmViewer.crvReportViewer.GetCurrentPageNumber();
		GlobalVariables.ReportDocument = new ReportDocument();
		GlobalVariables.ReportDocument.Load((dtReports.Rows.Count == 0) ? GetReportName() : (GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep" + (((UltraToggleEditorBase)chkIsArabic).Checked ? "_A" : "_E") + (((UltraToggleEditorBase)chkWithLogo).Checked ? "" : "_nologo")].ToString()));
		GlobalVariables.ReportDocument.SetParameterValue("@ItemIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", text);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked);
		frmViewer.frmParent = this;
		frmViewer.ConfigureReport();
		frmViewer.BringToFront();
		frmViewer.Refresh();
		frmViewer.crvReportViewer.ShowNthPage(currentPageNumber);
	}

	public override void btnItemsSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ItemsReport((!((UltraToggleEditorBase)chkIsItem).Checked) ? "0" : ((((UltraToggleEditorBase)chkIsRecipe).Checked || ((UltraToggleEditorBase)chkIsService).Checked) ? "-1" : "1"), (!((UltraToggleEditorBase)chkIsRecipe).Checked) ? "0" : ((((UltraToggleEditorBase)chkIsItem).Checked || ((UltraToggleEditorBase)chkIsService).Checked) ? "-1" : "1"), (!((UltraToggleEditorBase)chkIsService).Checked) ? "0" : ((((UltraToggleEditorBase)chkIsItem).Checked || ((UltraToggleEditorBase)chkIsRecipe).Checked) ? "-1" : "1"), (!((UltraToggleEditorBase)chkIsSalesItem).Checked) ? ((!((UltraToggleEditorBase)chkIsNotSalesItem).Checked) ? "-1" : "0") : (((UltraToggleEditorBase)chkIsNotSalesItem).Checked ? "-1" : "1"), (!((UltraToggleEditorBase)chkIsActive).Checked) ? ((!((UltraToggleEditorBase)chkIsNotActive).Checked) ? "-1" : "0") : (((UltraToggleEditorBase)chkIsNotActive).Checked ? "-1" : "1"), (!((UltraToggleEditorBase)chkIsDirectItem).Checked) ? ((!((UltraToggleEditorBase)chkIsNotDirectItem).Checked) ? "-1" : "0") : (((UltraToggleEditorBase)chkIsNotDirectItem).Checked ? "-1" : "1"), (!((UltraToggleEditorBase)chkIsProductionItem).Checked) ? ((!((UltraToggleEditorBase)chkIsNotProductionItem).Checked) ? "-1" : "0") : (((UltraToggleEditorBase)chkIsNotProductionItem).Checked ? "-1" : "1"), "-1", "-1", "0", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems.GetNodeByKey(dtSearchResult.Rows[i][TreeItemsIDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void chk_CheckedChanged(object sender, EventArgs e)
	{
		if (!IsLoading)
		{
			FillData();
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
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected O, but got Unknown
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected O, but got Unknown
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Expected O, but got Unknown
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.Reports.frmItems));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		Appearance val10 = new Appearance();
		Appearance val11 = new Appearance();
		Appearance val12 = new Appearance();
		Appearance val13 = new Appearance();
		Appearance val14 = new Appearance();
		Appearance val15 = new Appearance();
		Appearance val16 = new Appearance();
		this.chkIsProductionItem = new UltraCheckEditor();
		this.chkIsDirectItem = new UltraCheckEditor();
		this.chkIsSalesItem = new UltraCheckEditor();
		this.chkIsService = new UltraCheckEditor();
		this.chkIsRecipe = new UltraCheckEditor();
		this.chkIsItem = new UltraCheckEditor();
		this.chkIsActive = new UltraCheckEditor();
		this.chkIsNotActive = new UltraCheckEditor();
		this.chkIsNotProductionItem = new UltraCheckEditor();
		this.chkIsNotDirectItem = new UltraCheckEditor();
		this.chkIsNotSalesItem = new UltraCheckEditor();
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
		((System.ComponentModel.ISupportInitialize)this.chkIsProductionItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsDirectItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsSalesItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsService).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsRecipe).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsNotActive).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsNotProductionItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsNotDirectItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsNotSalesItem).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.ultraLabel1, "ultraLabel1");
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		resources.ApplyResources(base.ultraLabel2, "ultraLabel2");
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance2.FontData");
		resources.ApplyResources(val, "appearance2");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(base.dtpFromDate, "dtpFromDate");
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance3.FontData");
		resources.ApplyResources(val2, "appearance3");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		base.dtpToDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpToDate.DateTime = new System.DateTime(2011, 3, 22, 23, 59, 59, 0);
		resources.ApplyResources(base.dtpToDate, "dtpToDate");
		base.dtpToDate.MaskInput = "dd/mm/yyyy";
		base.dtpToDate.Value = new System.DateTime(2011, 3, 22, 23, 59, 59, 0);
		resources.ApplyResources(base.chkAllBranches, "chkAllBranches");
		((UltraControlBase)base.chkAllBranches).UseAppStyling = false;
		resources.ApplyResources(base.chkWithLogo, "chkWithLogo");
		((UltraControlBase)base.chkWithLogo).UseAppStyling = false;
		resources.ApplyResources(base.lblReportType, "lblReportType");
		((UltraControlBase)base.lblReportType).UseAppStyling = false;
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance6.FontData");
		resources.ApplyResources(val3, "appearance6");
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
		resources.ApplyResources(base.chkIsArabic, "chkIsArabic");
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
		resources.ApplyResources(this.chkIsProductionItem, "chkIsProductionItem");
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance9.FontData");
		resources.ApplyResources(val6, "appearance9");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkIsProductionItem).Appearance = (AppearanceBase)(object)val6;
		((UltraToggleEditorBase)this.chkIsProductionItem).Checked = true;
		((UltraToggleEditorBase)this.chkIsProductionItem).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsProductionItem).Name = "chkIsProductionItem";
		((UltraToggleEditorBase)this.chkIsProductionItem).CheckedChanged += new System.EventHandler(chk_CheckedChanged);
		resources.ApplyResources(this.chkIsDirectItem, "chkIsDirectItem");
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance10.FontData");
		resources.ApplyResources(val7, "appearance10");
		((SubObjectBase)val7).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkIsDirectItem).Appearance = (AppearanceBase)(object)val7;
		((UltraToggleEditorBase)this.chkIsDirectItem).Checked = true;
		((UltraToggleEditorBase)this.chkIsDirectItem).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsDirectItem).Name = "chkIsDirectItem";
		((UltraToggleEditorBase)this.chkIsDirectItem).CheckedChanged += new System.EventHandler(chk_CheckedChanged);
		resources.ApplyResources(this.chkIsSalesItem, "chkIsSalesItem");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val8).FontData, "appearance11.FontData");
		resources.ApplyResources(val8, "appearance11");
		((SubObjectBase)val8).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkIsSalesItem).Appearance = (AppearanceBase)(object)val8;
		((UltraToggleEditorBase)this.chkIsSalesItem).Checked = true;
		((UltraToggleEditorBase)this.chkIsSalesItem).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsSalesItem).Name = "chkIsSalesItem";
		((UltraToggleEditorBase)this.chkIsSalesItem).CheckedChanged += new System.EventHandler(chk_CheckedChanged);
		resources.ApplyResources(this.chkIsService, "chkIsService");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val9).FontData, "appearance12.FontData");
		resources.ApplyResources(val9, "appearance12");
		((SubObjectBase)val9).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkIsService).Appearance = (AppearanceBase)(object)val9;
		((UltraToggleEditorBase)this.chkIsService).Checked = true;
		((UltraToggleEditorBase)this.chkIsService).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsService).Name = "chkIsService";
		((UltraToggleEditorBase)this.chkIsService).CheckedChanged += new System.EventHandler(chk_CheckedChanged);
		resources.ApplyResources(this.chkIsRecipe, "chkIsRecipe");
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val10).FontData, "appearance13.FontData");
		resources.ApplyResources(val10, "appearance13");
		((SubObjectBase)val10).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkIsRecipe).Appearance = (AppearanceBase)(object)val10;
		((UltraToggleEditorBase)this.chkIsRecipe).Checked = true;
		((UltraToggleEditorBase)this.chkIsRecipe).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsRecipe).Name = "chkIsRecipe";
		((UltraToggleEditorBase)this.chkIsRecipe).CheckedChanged += new System.EventHandler(chk_CheckedChanged);
		resources.ApplyResources(this.chkIsItem, "chkIsItem");
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val11).FontData, "appearance15.FontData");
		resources.ApplyResources(val11, "appearance15");
		((SubObjectBase)val11).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkIsItem).Appearance = (AppearanceBase)(object)val11;
		((UltraToggleEditorBase)this.chkIsItem).Checked = true;
		((UltraToggleEditorBase)this.chkIsItem).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsItem).Name = "chkIsItem";
		((UltraToggleEditorBase)this.chkIsItem).CheckedChanged += new System.EventHandler(chk_CheckedChanged);
		resources.ApplyResources(this.chkIsActive, "chkIsActive");
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val12).FontData, "appearance16.FontData");
		resources.ApplyResources(val12, "appearance16");
		((SubObjectBase)val12).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkIsActive).Appearance = (AppearanceBase)(object)val12;
		((UltraToggleEditorBase)this.chkIsActive).Checked = true;
		((UltraToggleEditorBase)this.chkIsActive).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsActive).Name = "chkIsActive";
		((UltraToggleEditorBase)this.chkIsActive).CheckedChanged += new System.EventHandler(chk_CheckedChanged);
		resources.ApplyResources(this.chkIsNotActive, "chkIsNotActive");
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val13).FontData, "appearance41.FontData");
		resources.ApplyResources(val13, "appearance41");
		((SubObjectBase)val13).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkIsNotActive).Appearance = (AppearanceBase)(object)val13;
		((UltraToggleEditorBase)this.chkIsNotActive).Checked = true;
		((UltraToggleEditorBase)this.chkIsNotActive).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsNotActive).Name = "chkIsNotActive";
		((UltraToggleEditorBase)this.chkIsNotActive).CheckedChanged += new System.EventHandler(chk_CheckedChanged);
		resources.ApplyResources(this.chkIsNotProductionItem, "chkIsNotProductionItem");
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val14).FontData, "appearance1.FontData");
		resources.ApplyResources(val14, "appearance1");
		((SubObjectBase)val14).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkIsNotProductionItem).Appearance = (AppearanceBase)(object)val14;
		((UltraToggleEditorBase)this.chkIsNotProductionItem).Checked = true;
		((UltraToggleEditorBase)this.chkIsNotProductionItem).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsNotProductionItem).Name = "chkIsNotProductionItem";
		((UltraToggleEditorBase)this.chkIsNotProductionItem).CheckedChanged += new System.EventHandler(chk_CheckedChanged);
		resources.ApplyResources(this.chkIsNotDirectItem, "chkIsNotDirectItem");
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val15).FontData, "appearance4.FontData");
		resources.ApplyResources(val15, "appearance4");
		((SubObjectBase)val15).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkIsNotDirectItem).Appearance = (AppearanceBase)(object)val15;
		((UltraToggleEditorBase)this.chkIsNotDirectItem).Checked = true;
		((UltraToggleEditorBase)this.chkIsNotDirectItem).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsNotDirectItem).Name = "chkIsNotDirectItem";
		((UltraToggleEditorBase)this.chkIsNotDirectItem).CheckedChanged += new System.EventHandler(chk_CheckedChanged);
		resources.ApplyResources(this.chkIsNotSalesItem, "chkIsNotSalesItem");
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val16).FontData, "appearance7.FontData");
		resources.ApplyResources(val16, "appearance7");
		((SubObjectBase)val16).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkIsNotSalesItem).Appearance = (AppearanceBase)(object)val16;
		((UltraToggleEditorBase)this.chkIsNotSalesItem).Checked = true;
		((UltraToggleEditorBase)this.chkIsNotSalesItem).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsNotSalesItem).Name = "chkIsNotSalesItem";
		((UltraToggleEditorBase)this.chkIsNotSalesItem).CheckedChanged += new System.EventHandler(chk_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsNotActive);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsNotProductionItem);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsNotDirectItem);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsNotSalesItem);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsActive);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsService);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsRecipe);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsItem);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsProductionItem);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsDirectItem);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsSalesItem);
		base.Name = "frmItems";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsSalesItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsDirectItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsProductionItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsRecipe, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsService, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsActive, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsNotSalesItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsNotDirectItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsNotProductionItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsNotActive, 0);
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
		((System.ComponentModel.ISupportInitialize)this.chkIsProductionItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsDirectItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsSalesItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsService).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsRecipe).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsNotActive).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsNotProductionItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsNotDirectItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsNotSalesItem).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
