using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Accounting.Transactions;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;

namespace ERP.Accounting.Reports;

public class frmAccountsLedgerRep : frmReportTree2010
{
	public DataTable dtItems3;

	public string Items3;

	private bool UseCurrency;

	private bool UseCostCenters;

	private IContainer components = null;

	public UltraCheckEditor chkAllCurrency;

	protected internal CheckedListBox clbCurrency;

	public frmAccountsLedgerRep()
	{
		UseCurrency = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from Currency ").Rows[0][0].ToString()) > 1;
		UseCostCenters = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as CC From A_CostCenters Where IsMain = 0").Rows[0][0].ToString()) > 0;
		dtItems = Accounts.Select("-1", "-1", GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItemsParentIDCol = "ParentID";
		TreeItemsIDCol = "AccountID";
		TreeItemsNameCol = (GlobalVariables.IsArabic ? "AccountNameAr" : "AccountNameEn");
		TreeItemsNumberCol = "AccountNumber";
		TreeItemsIsMainCol = "IsMain";
		dtItems2 = CostCenters.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItems2ParentIDCol = "ParentID";
		TreeItems2IDCol = "CostCenterID";
		TreeItems2NameCol = (GlobalVariables.IsArabic ? "CostCenterNameAr" : "CostCenterNameEn");
		TreeItems2NumberCol = "CostCenterNumber";
		TreeItems2IsMainCol = "IsMain";
		InitializeComponent();
		cboReportType.Items.Clear();
		cboReportType.Items.Add((object)1, GlobalVariables.IsArabic ? "طباعه كل التفاصيل بالعمله المحليه" : "Print With All Details With Local Currency");
		cboReportType.Items.Add((object)6, GlobalVariables.IsArabic ? "طباعه مجمع باليوم تفصيلي" : "Print Grouped By Day With Details");
		cboReportType.Items.Add((object)7, GlobalVariables.IsArabic ? "طباعه مجمع باليوم إجمالي" : "Print Grouped By Day");
		if (UseCostCenters)
		{
			cboReportType.Items.Add((object)2, GlobalVariables.IsArabic ? "طباعه مجمع بمراكز التكلفه بالعمله المحليه" : "Print Grouped By Cost Center With Local Currency");
			cboReportType.Items.Add((object)3, GlobalVariables.IsArabic ? "طباعه مجمع بمراكز التكلفه" : "Print Grouped By Cost Center");
		}
		if (UseCurrency)
		{
			cboReportType.Items.Add((object)4, GlobalVariables.IsArabic ? "طباعه كل التفاصيل" : "Print With All Details");
			cboReportType.Items.Add((object)5, GlobalVariables.IsArabic ? "طباعه مجمع بالعمله" : "Print Grouped By Currency");
		}
	}

	public override void FormLoad()
	{
		base.FormLoad();
		dtItems3 = Currency.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		Main.Fillclb(clbCurrency, dtItems3, "CurrencyID", GlobalVariables.IsArabic ? "CurrencyNameAr" : "CurrencyNameEn");
		if (dtItems3.Rows.Count <= 1)
		{
			clbCurrency.Visible = false;
			((Control)(object)chkAllCurrency).Visible = false;
			((UltraToggleEditorBase)chkAllCurrency).Checked = true;
		}
		else
		{
			for (int i = 0; i < dtItems3.Rows.Count; i++)
			{
				clbCurrency.SetItemChecked(i, value: true);
			}
		}
		((UltraToggleEditorBase)chkAll2).Checked = true;
		((UltraToggleEditorBase)chkAllCurrency).Checked = true;
	}

	public override string GetReportName()
	{
		if (((UltraToggleEditorBase)chkWithLogo).Checked)
		{
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_A_AccountsLedgerLocalCurrency_A.rpt" : "Rep_A_AccountsLedgerLocalCurrency_E.rpt");
		}
		return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_A_AccountsLedgerLocalCurrency_A_nologo.rpt" : "Rep_A_AccountsLedgerLocalCurrency_E_nologo.rpt");
	}

	public override void ShowReport()
	{
		GetItems();
		GetBranches();
		Items3 = ",";
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		for (int i = 0; i < clbCurrency.Items.Count; i++)
		{
			if (clbCurrency.GetItemChecked(i))
			{
				Items3 = Items3 + dtItems3.Rows[i]["CurrencyID"].ToString() + ",";
			}
		}
		if (Items.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى حساب ليتم عرضه ", "There is no choosen account to be shown in the report, please check items to be shown in report");
			return;
		}
		if (((UltraToggleEditorBase)chkAll2).Checked || dtItems2.Select(TreeItems2IsMainCol + " = 0").Length == 0)
		{
			Items2 = "-1";
		}
		else if (Items2.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى مركز تكلفه ليتم عرضه ", "There is no choosen Cost Center to be shown in the report, please check items to be shown in report");
			return;
		}
		if (Items3.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى عمله ليتم عرضه ", "There is no choosen Currency to be shown in the report, please check items to be shown in report");
			return;
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches);
		GlobalVariables.ReportDocument.SetParameterValue("@BranchsNames", BranchesNames);
		GlobalVariables.ReportDocument.SetParameterValue("@AccountIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@CostCenterIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@CurrencyIDs", Items3);
		GlobalVariables.ReportDocument.SetParameterValue("@Approved", "-1");
		GlobalVariables.ReportDocument.SetParameterValue("@FromDate", dtpFromDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime);
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

	public void ShowReport(string BranchIDs, string Branches_Names, string AccountIDs, string CostCenterIDs, string CurrencyIDs, int Approved, DateTime FromDate, DateTime ToDate, bool IsArabic)
	{
		Branches = BranchIDs;
		BranchesNames = Branches_Names;
		Items = AccountIDs;
		Items2 = CostCenterIDs;
		Items3 = CurrencyIDs;
		dtpFromDate.DateTime = FromDate;
		dtpToDate.DateTime = ToDate;
		((UltraToggleEditorBase)chkIsArabic).Checked = IsArabic;
		cboReportType.SelectedIndex = 0;
		GlobalVariables.ReportDocument.Load((dtReports.Rows.Count == 0) ? GetReportName() : (GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep" + (((UltraToggleEditorBase)chkIsArabic).Checked ? "_A" : "_E") + (((UltraToggleEditorBase)chkWithLogo).Checked ? "" : "_nologo")].ToString()));
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", BranchIDs);
		GlobalVariables.ReportDocument.SetParameterValue("@BranchsNames", BranchesNames);
		GlobalVariables.ReportDocument.SetParameterValue("@AccountIDs", AccountIDs);
		GlobalVariables.ReportDocument.SetParameterValue("@CostCenterIDs", CostCenterIDs);
		GlobalVariables.ReportDocument.SetParameterValue("@CurrencyIDs", CurrencyIDs);
		GlobalVariables.ReportDocument.SetParameterValue("@Approved", Approved);
		GlobalVariables.ReportDocument.SetParameterValue("@FromDate", FromDate);
		GlobalVariables.ReportDocument.SetParameterValue("@ToDate", ToDate);
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", text);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", IsArabic);
		frmReporViwer2.frmParent = this;
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
	}

	public override void ReportDoubleClick(string GroupNamePath, frmReporViwer frmViewer)
	{
		if (GroupNamePath != "")
		{
			string text = GroupNamePath.Substring(0, GroupNamePath.IndexOf("JVID") + 4);
			if (text.IndexOf("JVID") >= 0)
			{
				string s = GroupNamePath.Replace(text, "").Replace(",", "").Replace("[", "")
					.Replace("]", "");
				frmJV frmJV2 = new frmJV(int.Parse(s));
				frmJV2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmJV2.lblTitle).Text = (GlobalVariables.IsArabic ? "قيود اليومية" : "JV");
				frmJV2.ShowDialog();
				RefreshReport(frmViewer);
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
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches);
		GlobalVariables.ReportDocument.SetParameterValue("@BranchsNames", BranchesNames);
		GlobalVariables.ReportDocument.SetParameterValue("@AccountIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@CostCenterIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@CurrencyIDs", Items3);
		GlobalVariables.ReportDocument.SetParameterValue("@Approved", "-1");
		GlobalVariables.ReportDocument.SetParameterValue("@FromDate", dtpFromDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime);
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
		dtSearchResult = SearchFunctions.AccountsReport(IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems.GetNodeByKey(dtSearchResult.Rows[i][TreeItemsIDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	public override void btnItems2Search_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.CostCenterReport(IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems2.GetNodeByKey(dtSearchResult.Rows[i][TreeItems2IDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void chkAllCurrency_CheckedChanged(object sender, EventArgs e)
	{
		for (int i = 0; i < clbCurrency.Items.Count; i++)
		{
			clbCurrency.SetItemChecked(i, ((UltraToggleEditorBase)chkAllCurrency).Checked);
		}
	}

	private void clbCurrency_SelectedValueChanged(object sender, EventArgs e)
	{
		((UltraToggleEditorBase)chkAllCurrency).CheckedChanged -= chkAllCurrency_CheckedChanged;
		((UltraToggleEditorBase)chkAllCurrency).Checked = clbCurrency.CheckedItems.Count == clbCurrency.Items.Count && clbCurrency.Items.Count > 0;
		((UltraToggleEditorBase)chkAllCurrency).CheckedChanged += chkAllCurrency_CheckedChanged;
	}

	public override void SaveSetting()
	{
		base.SaveSetting();
	}

	public override void DisplaySetting(DataRow drSetting)
	{
		base.DisplaySetting(drSetting);
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
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Accounting.Reports.frmAccountsLedgerRep));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		this.chkAllCurrency = new UltraCheckEditor();
		this.clbCurrency = new System.Windows.Forms.CheckedListBox();
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
		((System.ComponentModel.ISupportInitialize)base.dtReports).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtFormSetting).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllCurrency).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.ultraLabel1, "ultraLabel1");
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		resources.ApplyResources(base.ultraLabel2, "ultraLabel2");
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance2.FontData");
		resources.ApplyResources(val, "appearance2");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val;
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		resources.ApplyResources(base.dtpFromDate, "dtpFromDate");
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance3.FontData");
		resources.ApplyResources(val2, "appearance3");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		base.dtpToDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpToDate.DateTime = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		base.dtpToDate.MaskInput = "dd/mm/yyyy";
		resources.ApplyResources(base.dtpToDate, "dtpToDate");
		base.dtpToDate.Value = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
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
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance1.FontData");
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		((SubObjectBase)val6).ForceApplyResources = "FontData";
		((TextEditorControlBase)base.cboSetting).Appearance = (AppearanceBase)(object)val6;
		base.cboSetting.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboSetting.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.cboSetting, "cboSetting");
		resources.ApplyResources(base.lblSettingName, "ultraLabel3");
		resources.ApplyResources(base.btnSaveSetting, "btnSaveSetting");
		resources.ApplyResources(this.chkAllCurrency, "chkAllCurrency");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Blue;
		resources.ApplyResources(val7, "appearance4");
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance4.FontData");
		((SubObjectBase)val7).ForceApplyResources = "|FontData";
		((UltraToggleEditorBase)this.chkAllCurrency).Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.chkAllCurrency).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllCurrency).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllCurrency).Name = "chkAllCurrency";
		((UltraToggleEditorBase)this.chkAllCurrency).CheckedChanged += new System.EventHandler(chkAllCurrency_CheckedChanged);
		resources.ApplyResources(this.clbCurrency, "clbCurrency");
		this.clbCurrency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.clbCurrency.CheckOnClick = true;
		this.clbCurrency.Name = "clbCurrency";
		this.clbCurrency.SelectedValueChanged += new System.EventHandler(clbCurrency_SelectedValueChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllCurrency);
		base.Controls.Add(this.clbCurrency);
		base.Name = "frmAccountsLedgerRep";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblSettingName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNew, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
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
		base.Controls.SetChildIndex(this.clbCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllCurrency, 0);
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
		((System.ComponentModel.ISupportInitialize)base.dtReports).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtFormSetting).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllCurrency).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
