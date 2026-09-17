using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.Privilege;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Accounting.Reports;
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.Accounting.FinancialStatements;

public class frmProfitAndLoss : frmBase
{
	private string Branches;

	private string BranchesNames;

	public DataTable dtCostCenters;

	public string CostCenters;

	public string TreeCostCentersParentIDCol = "ParentID";

	public string TreeCostCentersIDCol = "CostCenterID";

	public string TreeCostCentersNameCol = (GlobalVariables.IsArabic ? "CostCenterNameAr" : "CostCenterNameEn");

	public string TreeCostCentersNumberCol = "CostCenterNumber";

	public string TreeCostCentersIsMainCol = "IsMain";

	private bool HasChanges;

	private DataTable dtReports = new DataTable();

	private DataTable dtExpenses;

	private DataTable dtRevenues;

	private DataTable dtBranches;

	private DataTable dtCurrency;

	private IContainer components = null;

	public UltraTree treeExpenses;

	public UltraTree treeRevenues;

	public UltraLabel lblRevenues;

	public UltraLabel lblExpenses;

	public UltraButton btnPreview;

	public UltraButton btnClose;

	public UltraLabel lblTitle;

	public UltraButton btnRevenuesDown;

	public UltraButton btnRevenuesUp;

	public UltraButton btnExpensesDown;

	public UltraButton btnExpensesUp;

	protected internal UltraCheckEditor chkIsArabic;

	public UltraComboEditor cboReportType;

	public UltraLabel lblReportType;

	public UltraCheckEditor chkWithLogo;

	public UltraCheckEditor chkAllBranches;

	public UltraDateTimeEditor dtpToDate;

	protected internal CheckedListBox clbBranches;

	public UltraDateTimeEditor dtpFromDate;

	public UltraLabel ultraLabel2;

	public UltraLabel ultraLabel1;

	public UltraButton btnUpdate;

	public UltraButton btnDelete;

	public UltraButton btnNew;

	public UltraLabel lblTitle2;

	private UltraTextEditor txtExchangeRate;

	private UltraLabel lblExchangeRate;

	private UltraLabel lblCurrency;

	private UltraComboEditor cboCurrency;

	public UltraButton btnCostCentersSearch;

	public UltraTextEditor txtCostCenters;

	public UltraTree treeCostCenters;

	protected internal UltraCheckEditor chkAllCostCenters;

	public frmProfitAndLoss()
	{
		InitializeComponent();
		treeRevenues.Override.ActiveNodeAppearance.BackColor = Color.Gray;
		treeExpenses.Override.ActiveNodeAppearance.BackColor = Color.Gray;
		dtpToDate.Value = DateTime.Now.Date.AddHours(23.0).AddMinutes(59.0).AddSeconds(59.0);
		dtCurrency = Currency.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", GlobalVariables.IsArabic ? "CurrencyNameAr" : "CurrencyNameEn");
		cboCurrency.SelectedIndex = 0;
		((Control)(object)txtExchangeRate).Text = "1";
		cboReportType.Items.Clear();
		cboReportType.Items.Add((object)1, GlobalVariables.IsArabic ? " T طباعه علي شكل حرف " : "Print in T View");
		cboReportType.Items.Add((object)2, GlobalVariables.IsArabic ? "طباعه علي شكل قائمه" : "Print in Statement View");
		cboReportType.Items.Add((object)3, GlobalVariables.IsArabic ? "طباعه علي شكل قائمه مجمع بمركز التكلفه" : "Print in Statement View Grouped By Cost Center");
	}

	public override void PrepareData()
	{
		((TextEditorControlBase)cboReportType).Clear();
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboReportType, dtReports, "ReportID", "ReportName");
		cboReportType.SelectedIndex = 0;
		((UltraToggleEditorBase)chkIsArabic).Checked = GlobalVariables.IsArabic;
		dtCostCenters = BusinessLayer.Accounting.CostCenters.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtExpenses = ProfitAndLose.ExpensesFillTree("1", GlobalVariables.IsArabic ? "1" : "0");
		dtRevenues = ProfitAndLose.RevenuesFillTree("0", GlobalVariables.IsArabic ? "1" : "0");
		TreeFunctions.FillTree(treeExpenses, dtExpenses, "ParentID", "AccountID", "AccountName", "", "IsMain");
		TreeFunctions.FillTree(treeRevenues, dtRevenues, "ParentID", "AccountID", "AccountName", "", "IsMain");
		if (dtCostCenters != null)
		{
			TreeFunctions.FillTree(treeCostCenters, dtCostCenters, TreeCostCentersParentIDCol, TreeCostCentersIDCol, TreeCostCentersNameCol, TreeCostCentersNumberCol, TreeCostCentersIsMainCol);
		}
		if (GlobalVariables.BranchIDs != "")
		{
			dtBranches = BusinessLayer.General.Branches.Select("-1", ViewAllBranches ? "-1" : GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			Main.Fillclb(clbBranches, dtBranches, "BranchID", GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn");
			if (dtBranches.Rows.Count <= 1)
			{
				clbBranches.Visible = false;
				((Control)(object)chkAllBranches).Visible = false;
				((UltraToggleEditorBase)chkAllBranches).Checked = true;
			}
			else
			{
				for (int i = 0; i < dtBranches.Rows.Count; i++)
				{
					if (dtBranches.Rows[i]["BranchID"].ToString() == GlobalVariables.CurrentBranchID)
					{
						clbBranches.SetItemChecked(i, value: true);
					}
				}
			}
		}
		((Control)(object)btnRevenuesUp).Enabled = false;
		((Control)(object)btnRevenuesDown).Enabled = false;
		((Control)(object)btnExpensesUp).Enabled = false;
		((Control)(object)btnExpensesDown).Enabled = false;
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	private void treeRevenues_AfterSelect(object sender, SelectEventArgs e)
	{
		((Control)(object)btnRevenuesUp).Enabled = treeRevenues.ActiveNode.Level == 1;
		((Control)(object)btnRevenuesDown).Enabled = treeRevenues.ActiveNode.Level == 1;
	}

	private void treeExpenses_AfterSelect(object sender, SelectEventArgs e)
	{
		((Control)(object)btnExpensesUp).Enabled = treeExpenses.ActiveNode.Level == 1;
		((Control)(object)btnExpensesDown).Enabled = treeExpenses.ActiveNode.Level == 1;
	}

	private void btnExpensesUp_Click(object sender, EventArgs e)
	{
		HasChanges = true;
		UltraTreeNode activeNode = treeExpenses.ActiveNode;
		if (activeNode.Index == 0 && activeNode.Parent.Index == 1)
		{
			treeExpenses.ActiveNode.Reposition(treeExpenses.Nodes[0].Nodes, ((DisposableObjectCollectionBase)treeExpenses.Nodes[0].Nodes).Count);
		}
		else
		{
			treeExpenses.ActiveNode.Reposition(treeExpenses.ActiveNode, (NodePosition)2);
		}
		treeExpenses.ActiveNode = activeNode;
	}

	private void btnExpensesDown_Click(object sender, EventArgs e)
	{
		HasChanges = true;
		UltraTreeNode activeNode = treeExpenses.ActiveNode;
		if (activeNode.Index == ((DisposableObjectCollectionBase)activeNode.Parent.Nodes).Count - 1 && activeNode.Parent.Index == 0)
		{
			treeExpenses.ActiveNode.Reposition(treeExpenses.Nodes[1].Nodes, 0);
		}
		else
		{
			treeExpenses.ActiveNode.Reposition(treeExpenses.ActiveNode, (NodePosition)3);
		}
		treeExpenses.ActiveNode = activeNode;
	}

	private void btnRevenuesUp_Click(object sender, EventArgs e)
	{
		HasChanges = true;
		UltraTreeNode activeNode = treeRevenues.ActiveNode;
		if (activeNode.Index == 0 && activeNode.Parent.Index == 1)
		{
			treeRevenues.ActiveNode.Reposition(treeRevenues.Nodes[0].Nodes, ((DisposableObjectCollectionBase)treeRevenues.Nodes[0].Nodes).Count);
		}
		else
		{
			treeRevenues.ActiveNode.Reposition(treeRevenues.ActiveNode, (NodePosition)2);
		}
		treeRevenues.ActiveNode = activeNode;
	}

	private void btnRevenuesDown_Click(object sender, EventArgs e)
	{
		HasChanges = true;
		UltraTreeNode activeNode = treeRevenues.ActiveNode;
		if (activeNode.Index == ((DisposableObjectCollectionBase)activeNode.Parent.Nodes).Count - 1 && activeNode.Parent.Index == 0)
		{
			treeRevenues.ActiveNode.Reposition(treeRevenues.Nodes[1].Nodes, 0);
		}
		else
		{
			treeRevenues.ActiveNode.Reposition(treeRevenues.ActiveNode, (NodePosition)3);
		}
		treeRevenues.ActiveNode = activeNode;
	}

	private void btnPreview_Click(object sender, EventArgs e)
	{
		if (HasChanges || dtExpenses.Select("ProfitAndLossID =0").Length != 0 || dtRevenues.Select("ProfitAndLossID =0").Length != 0)
		{
			Main.StartBulkTrans(FromServer: false);
			try
			{
				Save();
				Main.EndBulkTrans(FromServer: false);
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: false);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
				return;
			}
		}
		try
		{
			GlobalVariables.ReportDocument = new ReportDocument();
			string fileName = ((dtReports.Rows.Count == 0) ? GetReportName() : (GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep" + (((UltraToggleEditorBase)chkIsArabic).Checked ? "_A" : "_E") + (((UltraToggleEditorBase)chkWithLogo).Checked ? "" : "_nologo")].ToString()));
			FileInfo fileInfo = new FileInfo(fileName);
			DateTime lastWriteTime = fileInfo.LastWriteTime;
			if (fileInfo.Exists && lastWriteTime < new DateTime(2022, 2, 1))
			{
				GlobalVariables.InformationMB.Show(" برجاء تحديث تقرير حساب الارباح و الخسائر ", "Please Update Profit And loss Reports");
				return;
			}
			GlobalVariables.ReportDocument.Load((dtReports.Rows.Count == 0) ? GetReportName() : (GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep" + (((UltraToggleEditorBase)chkIsArabic).Checked ? "_A" : "_E") + (((UltraToggleEditorBase)chkWithLogo).Checked ? "" : "_nologo")].ToString()));
			ShowReport();
			GlobalVariables.ReportDocument = null;
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

	public void Save()
	{
		ProfitAndLose.Delete(GlobalVariables.UserID);
		for (int i = 0; i < ((DisposableObjectCollectionBase)treeExpenses.Nodes[0].Nodes).Count; i++)
		{
			ProfitAndLose.Insert_Update("-1", ((KeyedSubObjectBase)treeExpenses.Nodes[0].Nodes[i]).Key, treeExpenses.Nodes[0].Nodes[i].Index.ToString(), "1", "0", GlobalVariables.UserID);
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)treeExpenses.Nodes[1].Nodes).Count; j++)
		{
			ProfitAndLose.Insert_Update("-1", ((KeyedSubObjectBase)treeExpenses.Nodes[1].Nodes[j]).Key, treeExpenses.Nodes[1].Nodes[j].Index.ToString(), "1", "1", GlobalVariables.UserID);
		}
		for (int k = 0; k < ((DisposableObjectCollectionBase)treeRevenues.Nodes[0].Nodes).Count; k++)
		{
			ProfitAndLose.Insert_Update("-1", ((KeyedSubObjectBase)treeRevenues.Nodes[0].Nodes[k]).Key, treeRevenues.Nodes[0].Nodes[k].Index.ToString(), "0", "0", GlobalVariables.UserID);
		}
		for (int l = 0; l < ((DisposableObjectCollectionBase)treeRevenues.Nodes[1].Nodes).Count; l++)
		{
			ProfitAndLose.Insert_Update("-1", ((KeyedSubObjectBase)treeRevenues.Nodes[1].Nodes[l]).Key, treeRevenues.Nodes[1].Nodes[l].Index.ToString(), "0", "1", GlobalVariables.UserID);
		}
		HasChanges = false;
	}

	public string GetReportName()
	{
		if (((UltraToggleEditorBase)chkWithLogo).Checked)
		{
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_A_ProfitAndLossStatement_A.rpt" : "Rep_A_ProfitAndLossStatement_E.rpt");
		}
		return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_A_ProfitAndLossStatement_A_nologo.rpt" : "Rep_A_ProfitAndLossStatement_E_nologo.rpt");
	}

	public void ShowReport()
	{
		GetBranches();
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@FromDate", dtpFromDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", text);
		GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@BranchsNames", BranchesNames);
		GlobalVariables.ReportDocument.SetParameterValue("@CurrencyName", (cboCurrency.SelectedIndex == -1) ? "" : ((Control)(object)cboCurrency).Text);
		GlobalVariables.ReportDocument.SetParameterValue("@ExchangeRate", (((Control)(object)txtExchangeRate).Text == "" || cboCurrency.SelectedIndex == -1) ? "1" : ((Control)(object)txtExchangeRate).Text);
		if (cboReportType.SelectedIndex > -1 && dtReports.Rows.Count != 0 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("T"))
		{
			GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches, "Rep_A_ProfitAndLoss_Expenses");
			GlobalVariables.ReportDocument.SetParameterValue("@Approved", -1, "Rep_A_ProfitAndLoss_Expenses");
			GlobalVariables.ReportDocument.SetParameterValue("@FromDate", dtpFromDate.DateTime, "Rep_A_ProfitAndLoss_Expenses");
			GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime, "Rep_A_ProfitAndLoss_Expenses");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked, "Rep_A_ProfitAndLoss_Expenses");
			GlobalVariables.ReportDocument.SetParameterValue("@ExchangeRate", (((Control)(object)txtExchangeRate).Text == "" || cboCurrency.SelectedIndex == -1) ? "1" : ((Control)(object)txtExchangeRate).Text, "Rep_A_ProfitAndLoss_Expenses");
			GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches, "Rep_A_ProfitAndLoss_Revenues");
			GlobalVariables.ReportDocument.SetParameterValue("@Approved", -1, "Rep_A_ProfitAndLoss_Revenues");
			GlobalVariables.ReportDocument.SetParameterValue("@FromDate", dtpFromDate.DateTime, "Rep_A_ProfitAndLoss_Revenues");
			GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime, "Rep_A_ProfitAndLoss_Revenues");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked, "Rep_A_ProfitAndLoss_Revenues");
			GlobalVariables.ReportDocument.SetParameterValue("@ExchangeRate", (((Control)(object)txtExchangeRate).Text == "" || cboCurrency.SelectedIndex == -1) ? "1" : ((Control)(object)txtExchangeRate).Text, "Rep_A_ProfitAndLoss_Revenues");
		}
		else if (cboReportType.SelectedIndex > -1 && dtReports.Rows.Count != 0 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("Statement"))
		{
			GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches);
			GlobalVariables.ReportDocument.SetParameterValue("@Approved", -1);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked);
		}
		else if (cboReportType.SelectedIndex > -1 && dtReports.Rows.Count != 0 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("CostCenter"))
		{
			if (((UltraToggleEditorBase)chkAllCostCenters).Checked)
			{
				CostCenters = "-1";
			}
			else
			{
				CostCenters = TreeFunctions.GetTreeCheckedNodesIDs(treeCostCenters);
			}
			if (CostCenters.Equals(","))
			{
				GlobalVariables.InformationMB.Show("لم تقم باختيار اى مركز تكلفه  ", "There is no choosen Cost Center to be shown in the report, please check items to be shown in report");
				return;
			}
			GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches);
			GlobalVariables.ReportDocument.SetParameterValue("@CostCenterIDs", CostCenters);
			GlobalVariables.ReportDocument.SetParameterValue("@Approved", -1);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked);
		}
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

	public void ShowReport(string BranchIDs, string Branches_Names, int Approved, string CurrencyID, string ExchangeRate, DateTime FromDate, DateTime ToDate, bool IsArabic)
	{
		Branches = BranchIDs;
		BranchesNames = Branches_Names;
		dtpFromDate.DateTime = FromDate;
		dtpToDate.DateTime = ToDate;
		((UltraToggleEditorBase)chkIsArabic).Checked = IsArabic;
		cboReportType.SelectedIndex = 1;
		((TextEditorControlBase)cboCurrency).Value = CurrencyID;
		((Control)(object)txtExchangeRate).Text = ExchangeRate;
		GlobalVariables.ReportDocument.Load((dtReports.Rows.Count == 0) ? GetReportName() : (GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep" + (((UltraToggleEditorBase)chkIsArabic).Checked ? "_A" : "_E") + (((UltraToggleEditorBase)chkWithLogo).Checked ? "" : "_nologo")].ToString()));
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches);
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", text);
		GlobalVariables.ReportDocument.SetParameterValue("@BranchsNames", BranchesNames);
		GlobalVariables.ReportDocument.SetParameterValue("@FromDate", dtpFromDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@Approved", Approved);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked);
		GlobalVariables.ReportDocument.SetParameterValue("@CurrencyName", (cboCurrency.SelectedIndex == -1) ? "" : ((Control)(object)cboCurrency).Text);
		GlobalVariables.ReportDocument.SetParameterValue("@ExchangeRate", (((Control)(object)txtExchangeRate).Text == "" || cboCurrency.SelectedIndex == -1) ? "1" : ((Control)(object)txtExchangeRate).Text);
		frmReporViwer2.frmParent = this;
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
	}

	public override void ReportDoubleClick(string GroupNamePath, frmReporViwer frmViewer)
	{
		if (!(GroupNamePath != ""))
		{
			return;
		}
		string text = GroupNamePath.Substring(0, GroupNamePath.IndexOf("AccountID") + 9);
		if (text.IndexOf("AccountID") < 0)
		{
			return;
		}
		string accountIDs = GroupNamePath.Replace(text, "").Replace(",", "").Replace('[', ',')
			.Replace(']', ',');
		frmAccountsLedgerRep frmAccountsLedgerRep2 = new frmAccountsLedgerRep();
		try
		{
			GlobalVariables.ReportDocument = new ReportDocument();
			frmAccountsLedgerRep2.ShowReport(Branches, BranchesNames, accountIDs, "-1", "-1", -1, new DateTime(dtpToDate.DateTime.Year, 1, 1), dtpToDate.DateTime, ((UltraToggleEditorBase)chkIsArabic).Checked);
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
		GlobalVariables.ReportDocument.SetParameterValue("@FromDate", dtpFromDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", text);
		GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@BranchsNames", BranchesNames);
		GlobalVariables.ReportDocument.SetParameterValue("@CurrencyName", (cboCurrency.SelectedIndex == -1) ? "" : ((Control)(object)cboCurrency).Text);
		GlobalVariables.ReportDocument.SetParameterValue("@ExchangeRate", (((Control)(object)txtExchangeRate).Text == "" || cboCurrency.SelectedIndex == -1) ? "1" : ((Control)(object)txtExchangeRate).Text);
		if (cboReportType.SelectedIndex > -1 && ((dtReports.Rows.Count == 0 && ((TextEditorControlBase)cboReportType).Value.ToString() == "1") || (dtReports.Rows.Count != 0 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("T"))))
		{
			GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches, "Rep_A_ProfitAndLoss_Expenses");
			GlobalVariables.ReportDocument.SetParameterValue("@Approved", -1, "Rep_A_ProfitAndLoss_Expenses");
			GlobalVariables.ReportDocument.SetParameterValue("@FromDate", dtpFromDate.DateTime, "Rep_A_ProfitAndLoss_Expenses");
			GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime, "Rep_A_ProfitAndLoss_Expenses");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked, "Rep_A_ProfitAndLoss_Expenses");
			GlobalVariables.ReportDocument.SetParameterValue("@ExchangeRate", (((Control)(object)txtExchangeRate).Text == "" || cboCurrency.SelectedIndex == -1) ? "1" : ((Control)(object)txtExchangeRate).Text, "Rep_A_ProfitAndLoss_Expenses");
			GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches, "Rep_A_ProfitAndLoss_Revenues");
			GlobalVariables.ReportDocument.SetParameterValue("@Approved", -1, "Rep_A_ProfitAndLoss_Revenues");
			GlobalVariables.ReportDocument.SetParameterValue("@FromDate", dtpFromDate.DateTime, "Rep_A_ProfitAndLoss_Revenues");
			GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime, "Rep_A_ProfitAndLoss_Revenues");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked, "Rep_A_ProfitAndLoss_Revenues");
			GlobalVariables.ReportDocument.SetParameterValue("@ExchangeRate", (((Control)(object)txtExchangeRate).Text == "" || cboCurrency.SelectedIndex == -1) ? "1" : ((Control)(object)txtExchangeRate).Text, "Rep_A_ProfitAndLoss_Revenues");
		}
		else if (cboReportType.SelectedIndex > -1 && ((dtReports.Rows.Count == 0 && ((TextEditorControlBase)cboReportType).Value.ToString() == "2") || (dtReports.Rows.Count != 0 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("Statement"))))
		{
			GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches);
			GlobalVariables.ReportDocument.SetParameterValue("@Approved", -1);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked);
		}
		else if (cboReportType.SelectedIndex > -1 && ((dtReports.Rows.Count == 0 && ((TextEditorControlBase)cboReportType).Value.ToString() == "3") || (dtReports.Rows.Count != 0 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("CostCenter"))))
		{
			if (((UltraToggleEditorBase)chkAllCostCenters).Checked)
			{
				CostCenters = "-1";
			}
			else
			{
				CostCenters = TreeFunctions.GetTreeCheckedNodesIDs(treeCostCenters);
			}
			if (CostCenters.Equals(","))
			{
				GlobalVariables.InformationMB.Show("لم تقم باختيار اى مركز تكلفه  ", "There is no choosen Cost Center to be shown in the report, please check items to be shown in report");
				return;
			}
			GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches);
			GlobalVariables.ReportDocument.SetParameterValue("@CostCenterIDs", CostCenters);
			GlobalVariables.ReportDocument.SetParameterValue("@Approved", -1);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked);
		}
		frmViewer.frmParent = this;
		frmViewer.ConfigureReport();
		frmViewer.BringToFront();
		frmViewer.Refresh();
		frmViewer.crvReportViewer.ShowNthPage(currentPageNumber);
	}

	private void clbBranches_SelectedValueChanged(object sender, EventArgs e)
	{
		((UltraToggleEditorBase)chkAllBranches).CheckedChanged -= chkAllBranches_CheckedChanged;
		((UltraToggleEditorBase)chkAllBranches).Checked = clbBranches.CheckedItems.Count == clbBranches.Items.Count && clbBranches.Items.Count > 0;
		((UltraToggleEditorBase)chkAllBranches).CheckedChanged += chkAllBranches_CheckedChanged;
	}

	private void chkAllBranches_CheckedChanged(object sender, EventArgs e)
	{
		SelectAllListBoxItems(((UltraToggleEditorBase)chkAllBranches).Checked, clbBranches);
	}

	public virtual void SelectAllListBoxItems(bool Checked, CheckedListBox lst)
	{
		for (int i = 0; i < lst.Items.Count; i++)
		{
			lst.SetItemChecked(i, Checked);
		}
	}

	public virtual void GetBranches()
	{
		Branches = ",";
		BranchesNames = ",";
		for (int i = 0; i < clbBranches.Items.Count; i++)
		{
			if (clbBranches.GetItemChecked(i))
			{
				Branches = Branches + dtBranches.Rows[i]["BranchID"].ToString() + ",";
				BranchesNames = BranchesNames + dtBranches.Rows[i][((UltraToggleEditorBase)chkIsArabic).Checked ? "BranchNameAr" : "BranchNameEn"].ToString() + ",";
			}
		}
	}

	private void btnNew_Click(object sender, EventArgs e)
	{
		frmUserReportName frmUserReportName2 = new frmUserReportName();
		frmUserReportName2.WindowState = FormWindowState.Normal;
		frmUserReportName2.ShowDialog();
		if (!frmUserReportName2.Cancel)
		{
			int num = BusinessLayer.Privilege.Reports.InsertNewUserDesign(((DataRow)base.Tag)["FormID"].ToString(), frmUserReportName2.ArName, frmUserReportName2.EnName, dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].ToString(), GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID, IsFromServer: true);
			UsersReports.Insert_Update("-1", GlobalVariables.UserID, num.ToString(), GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID, IsFromServer: true);
			frmUserReportName2.Dispose();
			string sourceFileName = GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_A"].ToString();
			string sourceFileName2 = GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_A_nologo"].ToString();
			string sourceFileName3 = GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_E"].ToString();
			string sourceFileName4 = GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_E_nologo"].ToString();
			string text = GlobalVariables.ReportsPath + "Div\\Rep" + num + "_A.rpt";
			string text2 = GlobalVariables.ReportsPath + "Div\\Rep" + num + "_A_nologo.rpt";
			string text3 = GlobalVariables.ReportsPath + "Div\\Rep" + num + "_E.rpt";
			string text4 = GlobalVariables.ReportsPath + "Div\\Rep" + num + "_E_nologo.rpt";
			File.Copy(sourceFileName, text);
			File.Copy(sourceFileName2, text2);
			File.Copy(sourceFileName3, text3);
			File.Copy(sourceFileName4, text4);
			Process.Start(text4);
			Process.Start(text3);
			Process.Start(text2);
			Process.Start(text);
			dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboReportType, dtReports, "ReportID", "ReportName");
		}
	}

	private void btnUpdate_Click(object sender, EventArgs e)
	{
		string fileName = GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_A"].ToString();
		string fileName2 = GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_A_nologo"].ToString();
		string fileName3 = GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_E"].ToString();
		string fileName4 = GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_E_nologo"].ToString();
		Process.Start(fileName4);
		Process.Start(fileName3);
		Process.Start(fileName2);
		Process.Start(fileName);
	}

	private void btnDelete_Click(object sender, EventArgs e)
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			UsersReports.DeleteByReportID(((TextEditorControlBase)cboReportType).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			GroupsReports.DeleteByReportID(((TextEditorControlBase)cboReportType).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			BusinessLayer.Privilege.Reports.Delete(((TextEditorControlBase)cboReportType).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			File.Delete(GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_A"]);
			File.Delete(GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_A_nologo"]);
			File.Delete(GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_E"]);
			File.Delete(GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_E_nologo"]);
			((TextEditorControlBase)cboReportType).Clear();
			dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboReportType, dtReports, "ReportID", "ReportName");
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void cboReportType_ValueChanged(object sender, EventArgs e)
	{
		if (dtReports != null && dtReports.Rows.Count > 0 && cboReportType.SelectedIndex > -1)
		{
			((Control)(object)btnUpdate).Enabled = GlobalVariables.UserID == "1" || !Convert.ToBoolean(dtReports.Rows[cboReportType.SelectedIndex]["ReadOnly"]);
			((Control)(object)btnDelete).Enabled = !Convert.ToBoolean(dtReports.Rows[cboReportType.SelectedIndex]["ReadOnly"]);
		}
		UltraCheckEditor obj = chkAllCostCenters;
		UltraTree obj2 = treeCostCenters;
		UltraTextEditor obj3 = txtCostCenters;
		bool flag = (((Control)(object)btnCostCentersSearch).Visible = cboReportType.SelectedIndex > -1 && dtReports.Rows.Count != 0 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("CostCenter"));
		bool flag3 = (((Control)(object)obj3).Visible = flag);
		bool visible = (((Control)(object)obj2).Visible = flag3);
		((Control)(object)obj).Visible = visible;
	}

	private void txtExchangeRate_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void frmProfitAndLoss_Load(object sender, EventArgs e)
	{
		if (base.DesignMode)
		{
			return;
		}
		int num = Convert.ToInt32(((DataRow)base.Tag)["PeriodDays"]);
		DateTime dateTime = GlobalFunctions.GetServerDateTimeNow().AddDays(-num);
		if (!GlobalVariables.SeeingClosedYears)
		{
			if (num == 0 || dateTime < GlobalVariables.MinOpenedDate)
			{
				dtpFromDate.MinDate = GlobalVariables.MinOpenedDate;
			}
			else
			{
				dtpFromDate.MinDate = dateTime;
			}
		}
		else if (num > 0)
		{
			dtpFromDate.MinDate = dateTime;
		}
	}

	private void btnCostCentersSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.CostCenterReport(IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			treeCostCenters.GetNodeByKey(dtSearchResult.Rows[i][TreeCostCentersIDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void chkAllCostCenters_CheckedChanged(object sender, EventArgs e)
	{
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAllCostCenters).Checked, treeCostCenters);
	}

	private void txtCostCenters_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtCostCenters);
		dataView.RowFilter = TreeCostCentersNameCol + " Like '%" + ((Control)(object)txtCostCenters).Text.Trim() + "%' " + ((TreeCostCentersNumberCol != null && TreeCostCentersNumberCol != "") ? (" OR " + TreeCostCentersNumberCol + " Like '" + ((Control)(object)txtCostCenters).Text.Trim() + "%' ") : "");
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			treeCostCenters.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			treeCostCenters.ActiveNode = treeCostCenters.GetNodeByKey(dataView.ToTable().Rows[0][TreeCostCentersIDCol].ToString());
		}
	}

	private void treeCostCenters_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected O, but got Unknown
		treeCostCenters.AfterCheck -= new AfterNodeChangedEventHandler(treeCostCenters_AfterCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			TreeFunctions.SetParentCheckedState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAllCostCenters).CheckedChanged -= chkAllCostCenters_CheckedChanged;
		TreeFunctions.SetCheckBoxAllState(treeCostCenters, chkAllCostCenters);
		((UltraToggleEditorBase)chkAllCostCenters).CheckedChanged += chkAllCostCenters_CheckedChanged;
		treeCostCenters.AfterCheck += new AfterNodeChangedEventHandler(treeCostCenters_AfterCheck);
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
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Expected O, but got Unknown
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Expected O, but got Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Expected O, but got Unknown
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Expected O, but got Unknown
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Expected O, but got Unknown
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Expected O, but got Unknown
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Expected O, but got Unknown
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Expected O, but got Unknown
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Expected O, but got Unknown
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Expected O, but got Unknown
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Expected O, but got Unknown
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Expected O, but got Unknown
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Expected O, but got Unknown
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Expected O, but got Unknown
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Expected O, but got Unknown
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Expected O, but got Unknown
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Expected O, but got Unknown
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Expected O, but got Unknown
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Expected O, but got Unknown
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Expected O, but got Unknown
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Expected O, but got Unknown
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Expected O, but got Unknown
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Expected O, but got Unknown
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Expected O, but got Unknown
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Expected O, but got Unknown
		//IL_0f32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f3c: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Accounting.FinancialStatements.frmProfitAndLoss));
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
		Appearance val17 = new Appearance();
		Appearance val18 = new Appearance();
		Appearance val19 = new Appearance();
		Override val20 = new Override();
		Appearance val21 = new Appearance();
		this.treeExpenses = new UltraTree();
		this.treeRevenues = new UltraTree();
		this.lblRevenues = new UltraLabel();
		this.lblExpenses = new UltraLabel();
		this.btnPreview = new UltraButton();
		this.btnClose = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnRevenuesDown = new UltraButton();
		this.btnRevenuesUp = new UltraButton();
		this.btnExpensesDown = new UltraButton();
		this.btnExpensesUp = new UltraButton();
		this.chkIsArabic = new UltraCheckEditor();
		this.cboReportType = new UltraComboEditor();
		this.lblReportType = new UltraLabel();
		this.chkWithLogo = new UltraCheckEditor();
		this.chkAllBranches = new UltraCheckEditor();
		this.dtpToDate = new UltraDateTimeEditor();
		this.clbBranches = new System.Windows.Forms.CheckedListBox();
		this.dtpFromDate = new UltraDateTimeEditor();
		this.ultraLabel2 = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.btnUpdate = new UltraButton();
		this.btnDelete = new UltraButton();
		this.btnNew = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.txtExchangeRate = new UltraTextEditor();
		this.lblExchangeRate = new UltraLabel();
		this.lblCurrency = new UltraLabel();
		this.cboCurrency = new UltraComboEditor();
		this.btnCostCentersSearch = new UltraButton();
		this.txtCostCenters = new UltraTextEditor();
		this.treeCostCenters = new UltraTree();
		this.chkAllCostCenters = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.treeExpenses).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.treeRevenues).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsArabic).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboReportType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithLogo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCostCenters).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.treeCostCenters).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllCostCenters).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.treeExpenses, "treeExpenses");
		((System.Windows.Forms.Control)(object)this.treeExpenses).AllowDrop = true;
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val, "appearance1");
		this.treeExpenses.Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.treeExpenses).Name = "treeExpenses";
		((UltraControlBase)this.treeExpenses).UseAppStyling = false;
		this.treeExpenses.AfterSelect += new AfterNodeSelectEventHandler(treeExpenses_AfterSelect);
		resources.ApplyResources(this.treeRevenues, "treeRevenues");
		((System.Windows.Forms.Control)(object)this.treeRevenues).AllowDrop = true;
		((AppearanceBase)val2).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val2, "appearance2");
		this.treeRevenues.Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.treeRevenues).Name = "treeRevenues";
		((UltraControlBase)this.treeRevenues).UseAppStyling = false;
		this.treeRevenues.AfterSelect += new AfterNodeSelectEventHandler(treeRevenues_AfterSelect);
		resources.ApplyResources(this.lblRevenues, "lblRevenues");
		((AppearanceBase)val3).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.lblRevenues).Appearance = (AppearanceBase)(object)val3;
		this.lblRevenues.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRevenues).Name = "lblRevenues";
		((ControlBase)this.lblRevenues).WrapText = false;
		resources.ApplyResources(this.lblExpenses, "lblExpenses");
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.lblExpenses).Appearance = (AppearanceBase)(object)val4;
		this.lblExpenses.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExpenses).Name = "lblExpenses";
		((ControlBase)this.lblExpenses).WrapText = false;
		resources.ApplyResources(this.btnPreview, "btnPreview");
		((System.Windows.Forms.Control)(object)this.btnPreview).Name = "btnPreview";
		((System.Windows.Forms.Control)(object)this.btnPreview).Click += new System.EventHandler(btnPreview_Click);
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val5).Image = resources.GetObject("appearance5.Image");
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val5;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val6).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnRevenuesDown, "btnRevenuesDown");
		((AppearanceBase)val7).Image = resources.GetObject("appearance7.Image");
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.btnRevenuesDown).Appearance = (AppearanceBase)(object)val7;
		((ControlBase)this.btnRevenuesDown).ImageSize = new System.Drawing.Size(18, 18);
		((System.Windows.Forms.Control)(object)this.btnRevenuesDown).Name = "btnRevenuesDown";
		((System.Windows.Forms.Control)(object)this.btnRevenuesDown).Click += new System.EventHandler(btnRevenuesDown_Click);
		resources.ApplyResources(this.btnRevenuesUp, "btnRevenuesUp");
		((AppearanceBase)val8).Image = ERP.Properties.Resources.btnUP;
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.btnRevenuesUp).Appearance = (AppearanceBase)(object)val8;
		((ControlBase)this.btnRevenuesUp).ImageSize = new System.Drawing.Size(18, 18);
		((System.Windows.Forms.Control)(object)this.btnRevenuesUp).Name = "btnRevenuesUp";
		((System.Windows.Forms.Control)(object)this.btnRevenuesUp).Click += new System.EventHandler(btnRevenuesUp_Click);
		resources.ApplyResources(this.btnExpensesDown, "btnExpensesDown");
		((AppearanceBase)val9).Image = resources.GetObject("appearance9.Image");
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.btnExpensesDown).Appearance = (AppearanceBase)(object)val9;
		((ControlBase)this.btnExpensesDown).ImageSize = new System.Drawing.Size(18, 18);
		((System.Windows.Forms.Control)(object)this.btnExpensesDown).Name = "btnExpensesDown";
		((System.Windows.Forms.Control)(object)this.btnExpensesDown).Click += new System.EventHandler(btnExpensesDown_Click);
		resources.ApplyResources(this.btnExpensesUp, "btnExpensesUp");
		((AppearanceBase)val10).Image = ERP.Properties.Resources.btnUP;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.btnExpensesUp).Appearance = (AppearanceBase)(object)val10;
		((ControlBase)this.btnExpensesUp).ImageSize = new System.Drawing.Size(18, 18);
		((System.Windows.Forms.Control)(object)this.btnExpensesUp).Name = "btnExpensesUp";
		((System.Windows.Forms.Control)(object)this.btnExpensesUp).Click += new System.EventHandler(btnExpensesUp_Click);
		resources.ApplyResources(this.chkIsArabic, "chkIsArabic");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Blue;
		resources.ApplyResources(val11, "appearance11");
		((UltraToggleEditorBase)this.chkIsArabic).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.chkIsArabic).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkIsArabic).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkIsArabic).Name = "chkIsArabic";
		((UltraControlBase)this.chkIsArabic).UseAppStyling = false;
		resources.ApplyResources(this.cboReportType, "cboReportType");
		this.cboReportType.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboReportType).Name = "cboReportType";
		((TextEditorControlBase)this.cboReportType).Nullable = false;
		((TextEditorControlBase)this.cboReportType).ValueChanged += new System.EventHandler(cboReportType_ValueChanged);
		resources.ApplyResources(this.lblReportType, "lblReportType");
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Blue;
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.lblReportType).Appearance = (AppearanceBase)(object)val12;
		this.lblReportType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReportType).Name = "lblReportType";
		((ControlBase)this.lblReportType).WrapText = false;
		resources.ApplyResources(this.chkWithLogo, "chkWithLogo");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Blue;
		resources.ApplyResources(val13, "appearance13");
		((UltraToggleEditorBase)this.chkWithLogo).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.chkWithLogo).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkWithLogo).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkWithLogo).Name = "chkWithLogo";
		resources.ApplyResources(this.chkAllBranches, "chkAllBranches");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Blue;
		resources.ApplyResources(val14, "appearance14");
		((UltraToggleEditorBase)this.chkAllBranches).Appearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.chkAllBranches).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllBranches).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllBranches).Name = "chkAllBranches";
		((UltraToggleEditorBase)this.chkAllBranches).CheckedChanged += new System.EventHandler(chkAllBranches_CheckedChanged);
		resources.ApplyResources(this.dtpToDate, "dtpToDate");
		((System.Windows.Forms.Control)(object)this.dtpToDate).Name = "dtpToDate";
		this.dtpToDate.PromptChar = ' ';
		resources.ApplyResources(this.clbBranches, "clbBranches");
		this.clbBranches.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.clbBranches.CheckOnClick = true;
		this.clbBranches.Name = "clbBranches";
		this.clbBranches.SelectedValueChanged += new System.EventHandler(clbBranches_SelectedValueChanged);
		resources.ApplyResources(this.dtpFromDate, "dtpFromDate");
		((System.Windows.Forms.Control)(object)this.dtpFromDate).Name = "dtpFromDate";
		this.dtpFromDate.PromptChar = ' ';
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Blue;
		resources.ApplyResources(val15, "appearance15");
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val15;
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Blue;
		resources.ApplyResources(val16, "appearance16");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val16;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.btnUpdate, "btnUpdate");
		((System.Windows.Forms.Control)(object)this.btnUpdate).Name = "btnUpdate";
		((System.Windows.Forms.Control)(object)this.btnUpdate).Click += new System.EventHandler(btnUpdate_Click);
		resources.ApplyResources(this.btnDelete, "btnDelete");
		((System.Windows.Forms.Control)(object)this.btnDelete).Name = "btnDelete";
		((System.Windows.Forms.Control)(object)this.btnDelete).Click += new System.EventHandler(btnDelete_Click);
		resources.ApplyResources(this.btnNew, "btnNew");
		((System.Windows.Forms.Control)(object)this.btnNew).Name = "btnNew";
		((System.Windows.Forms.Control)(object)this.btnNew).Click += new System.EventHandler(btnNew_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val17).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val17).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val17, "appearance17");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.txtExchangeRate, "txtExchangeRate");
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).Name = "txtExchangeRate";
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtExchangeRate_KeyPress);
		resources.ApplyResources(this.lblExchangeRate, "lblExchangeRate");
		this.lblExchangeRate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExchangeRate).Name = "lblExchangeRate";
		((ControlBase)this.lblExchangeRate).WrapText = false;
		resources.ApplyResources(this.lblCurrency, "lblCurrency");
		this.lblCurrency.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCurrency).Name = "lblCurrency";
		((ControlBase)this.lblCurrency).WrapText = false;
		resources.ApplyResources(this.cboCurrency, "cboCurrency");
		((TextEditorControlBase)this.cboCurrency).AlwaysInEditMode = true;
		this.cboCurrency.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCurrency).Name = "cboCurrency";
		resources.ApplyResources(this.btnCostCentersSearch, "btnCostCentersSearch");
		((AppearanceBase)val18).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val18, "appearance18");
		((ControlBase)this.btnCostCentersSearch).Appearance = (AppearanceBase)(object)val18;
		((System.Windows.Forms.Control)(object)this.btnCostCentersSearch).Name = "btnCostCentersSearch";
		((System.Windows.Forms.Control)(object)this.btnCostCentersSearch).Click += new System.EventHandler(btnCostCentersSearch_Click);
		resources.ApplyResources(this.txtCostCenters, "txtCostCenters");
		((System.Windows.Forms.Control)(object)this.txtCostCenters).Name = "txtCostCenters";
		((TextEditorControlBase)this.txtCostCenters).ValueChanged += new System.EventHandler(txtCostCenters_ValueChanged);
		resources.ApplyResources(this.treeCostCenters, "treeCostCenters");
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val19, "appearance19");
		this.treeCostCenters.Appearance = (AppearanceBase)(object)val19;
		((System.Windows.Forms.Control)(object)this.treeCostCenters).Name = "treeCostCenters";
		val20.NodeStyle = (NodeStyle)1;
		this.treeCostCenters.Override = val20;
		((UltraControlBase)this.treeCostCenters).UseAppStyling = false;
		this.treeCostCenters.AfterCheck += new AfterNodeChangedEventHandler(treeCostCenters_AfterCheck);
		resources.ApplyResources(this.chkAllCostCenters, "chkAllCostCenters");
		((AppearanceBase)val21).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val21).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val21, "appearance20");
		((UltraToggleEditorBase)this.chkAllCostCenters).Appearance = (AppearanceBase)(object)val21;
		((System.Windows.Forms.Control)(object)this.chkAllCostCenters).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllCostCenters).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllCostCenters).Name = "chkAllCostCenters";
		((UltraControlBase)this.chkAllCostCenters).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAllCostCenters).CheckedChanged += new System.EventHandler(chkAllCostCenters_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCostCentersSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCostCenters);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.treeCostCenters);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllCostCenters);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnUpdate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDelete);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNew);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnExpensesDown);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnExpensesUp);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsArabic);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboReportType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReportType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkWithLogo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpToDate);
		base.Controls.Add(this.clbBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnRevenuesDown);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnRevenuesUp);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPreview);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExpenses);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRevenues);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.treeRevenues);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.treeExpenses);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmProfitAndLoss";
		base.Load += new System.EventHandler(frmProfitAndLoss_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.treeExpenses, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.treeRevenues, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRevenues, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExpenses, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPreview, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnRevenuesUp, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnRevenuesDown, 0);
		base.Controls.SetChildIndex(this.clbBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkWithLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsArabic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnExpensesUp, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnExpensesDown, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNew, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllCostCenters, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.treeCostCenters, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCostCenters, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCostCentersSearch, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.treeExpenses).EndInit();
		((System.ComponentModel.ISupportInitialize)this.treeRevenues).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsArabic).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboReportType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithLogo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCostCenters).EndInit();
		((System.ComponentModel.ISupportInitialize)this.treeCostCenters).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllCostCenters).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
