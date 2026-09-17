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

public class frmBalanceSheet : frmBase
{
	private string Branches;

	private string BranchesNames;

	private bool HasChanges;

	private DataTable dtReports;

	private DataTable dtAssets;

	private DataTable dtLiabilities;

	private DataTable dtBranches;

	private DataTable dtCurrency;

	private IContainer components = null;

	public UltraTree treeAssets;

	public UltraTree treeLiabilities;

	public UltraLabel ultraLabel1;

	public UltraLabel ultraLabel2;

	public UltraLabel lblLiabilities;

	public UltraLabel lblAssets;

	public UltraButton btnMoveToLiabilities;

	public UltraButton btnMoveToAssets;

	public UltraButton btnPreview;

	public UltraButton btnClose;

	public UltraLabel lblTitle;

	public UltraButton btnLiabilitiesDown;

	public UltraButton btnLiabilitiesUp;

	public UltraButton btnAssetsDown;

	public UltraButton btnAssetsUp;

	protected internal UltraCheckEditor chkIsArabic;

	public UltraComboEditor cboReportType;

	public UltraLabel lblReportType;

	public UltraCheckEditor chkWithLogo;

	public UltraCheckEditor chkAllBranches;

	public UltraDateTimeEditor dtpToDate;

	protected internal CheckedListBox clbBranches;

	public UltraLabel ultraLabel3;

	public UltraButton btnLoadDefault;

	public UltraButton btnUpdate;

	public UltraButton btnDelete;

	public UltraButton btnNew;

	public UltraLabel lblTitle2;

	private UltraTextEditor txtExchangeRate;

	private UltraLabel lblExchangeRate;

	private UltraLabel lblCurrency;

	private UltraComboEditor cboCurrency;

	public frmBalanceSheet()
	{
		InitializeComponent();
		treeLiabilities.Override.ActiveNodeAppearance.BackColor = Color.Gray;
		treeAssets.Override.ActiveNodeAppearance.BackColor = Color.Gray;
		dtpToDate.Value = DateTime.Now.Date.AddHours(23.0).AddMinutes(59.0).AddSeconds(59.0);
		cboReportType.Items.Clear();
		cboReportType.Items.Add((object)1, GlobalVariables.IsArabic ? " T طباعه علي شكل حرف " : "Print in T View");
		cboReportType.Items.Add((object)2, GlobalVariables.IsArabic ? "طباعه علي شكل قائمه" : "Print in Statement View");
	}

	public override void PrepareData()
	{
		((TextEditorControlBase)cboReportType).Clear();
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboReportType, dtReports, "ReportID", "ReportName");
		cboReportType.SelectedIndex = 0;
		((UltraToggleEditorBase)chkIsArabic).Checked = GlobalVariables.IsArabic;
		dtCurrency = Currency.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", GlobalVariables.IsArabic ? "CurrencyNameAr" : "CurrencyNameEn");
		cboCurrency.SelectedIndex = 0;
		((Control)(object)txtExchangeRate).Text = "1";
		UltraLabel obj = ultraLabel1;
		bool useAppStyling = (((UltraControlBase)ultraLabel2).UseAppStyling = false);
		((UltraControlBase)obj).UseAppStyling = useAppStyling;
		AppearanceBase appearance = ((ControlBase)ultraLabel1).Appearance;
		Color backColor = (((ControlBase)ultraLabel2).Appearance.BackColor = Color.DarkBlue);
		appearance.BackColor = backColor;
		AppearanceBase appearance2 = ((ControlBase)ultraLabel1).Appearance;
		backColor = (((ControlBase)ultraLabel2).Appearance.ForeColor = Color.DarkBlue);
		appearance2.ForeColor = backColor;
		dtAssets = BalanceSheet.FillTree("1", GlobalVariables.IsArabic ? "1" : "0");
		dtLiabilities = BalanceSheet.FillTree("0", GlobalVariables.IsArabic ? "1" : "0");
		TreeFunctions.FillTree(treeAssets, dtAssets, "ParentID", "AccountID", "AccountName", "AccountNumber", "IsMain");
		TreeFunctions.FillTree(treeLiabilities, dtLiabilities, "ParentID", "AccountID", "AccountName", "AccountNumber", "IsMain");
		if (!(GlobalVariables.BranchIDs != ""))
		{
			return;
		}
		dtBranches = BusinessLayer.General.Branches.Select("-1", ViewAllBranches ? "-1" : GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		Main.Fillclb(clbBranches, dtBranches, "BranchID", GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn");
		if (dtBranches.Rows.Count <= 1)
		{
			clbBranches.Visible = false;
			((Control)(object)chkAllBranches).Visible = false;
			((UltraToggleEditorBase)chkAllBranches).Checked = true;
			return;
		}
		for (int i = 0; i < dtBranches.Rows.Count; i++)
		{
			if (dtBranches.Rows[i]["BranchID"].ToString() == GlobalVariables.CurrentBranchID)
			{
				clbBranches.SetItemChecked(i, value: true);
			}
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	private void treeLiabilities_AfterSelect(object sender, SelectEventArgs e)
	{
		if (treeLiabilities.ActiveNode.Level == 1)
		{
			((Control)(object)btnLiabilitiesUp).Enabled = treeLiabilities.ActiveNode.Index != 0;
			((Control)(object)btnLiabilitiesDown).Enabled = treeLiabilities.ActiveNode.Index != ((DisposableObjectCollectionBase)treeLiabilities.ActiveNode.Parent.Nodes).Count - 1;
		}
	}

	private void treeAssets_AfterSelect(object sender, SelectEventArgs e)
	{
		if (treeAssets.ActiveNode.Level == 1)
		{
			((Control)(object)btnAssetsUp).Enabled = treeAssets.ActiveNode.Index != 0;
			((Control)(object)btnAssetsDown).Enabled = treeAssets.ActiveNode.Index != ((DisposableObjectCollectionBase)treeAssets.ActiveNode.Parent.Nodes).Count - 1;
		}
	}

	private void btnMoveToAssets_Click(object sender, EventArgs e)
	{
		HasChanges = true;
		if (treeLiabilities.ActiveNode.Level == 1)
		{
			UltraTreeNode activeNode = treeLiabilities.ActiveNode;
			treeLiabilities.ActiveNode.Remove();
			if (treeAssets.ActiveNode.Level == 0)
			{
				treeAssets.ActiveNode.Nodes.Add(activeNode);
				treeAssets.ActiveNode.ExpandAll();
			}
			else
			{
				treeAssets.ActiveNode.Parent.Nodes.Add(activeNode);
			}
		}
		else
		{
			if (treeLiabilities.ActiveNode.Level != 0)
			{
				return;
			}
			int num;
			for (num = 0; num < ((DisposableObjectCollectionBase)treeLiabilities.ActiveNode.Nodes).Count; num++)
			{
				UltraTreeNode val = treeLiabilities.ActiveNode.Nodes[num];
				treeLiabilities.ActiveNode.Nodes[num].Remove();
				num--;
				if (treeAssets.ActiveNode.Level == 0)
				{
					treeAssets.ActiveNode.Nodes.Add(val);
					treeAssets.ActiveNode.ExpandAll();
				}
				else
				{
					treeAssets.ActiveNode.Parent.Nodes.Add(val);
				}
			}
		}
	}

	private void btnMoveToLiabilities_Click(object sender, EventArgs e)
	{
		HasChanges = true;
		if (treeAssets.ActiveNode.Level == 1)
		{
			UltraTreeNode activeNode = treeAssets.ActiveNode;
			treeAssets.ActiveNode.Remove();
			if (treeLiabilities.ActiveNode.Level == 0)
			{
				treeLiabilities.ActiveNode.Nodes.Add(activeNode);
				treeLiabilities.ActiveNode.ExpandAll();
			}
			else
			{
				treeLiabilities.ActiveNode.Parent.Nodes.Add(activeNode);
			}
		}
		else
		{
			if (treeAssets.ActiveNode.Level != 0)
			{
				return;
			}
			int num;
			for (num = 0; num < ((DisposableObjectCollectionBase)treeAssets.ActiveNode.Nodes).Count; num++)
			{
				UltraTreeNode val = treeAssets.ActiveNode.Nodes[num];
				treeAssets.ActiveNode.Nodes[num].Remove();
				num--;
				if (treeLiabilities.ActiveNode.Level == 0)
				{
					treeLiabilities.ActiveNode.Nodes.Add(val);
					treeLiabilities.ActiveNode.ExpandAll();
				}
				else
				{
					treeLiabilities.ActiveNode.Parent.Nodes.Add(val);
				}
			}
		}
	}

	private void btnAssetsUp_Click(object sender, EventArgs e)
	{
		HasChanges = true;
		UltraTreeNode activeNode = treeAssets.ActiveNode;
		treeAssets.ActiveNode.Reposition(treeAssets.ActiveNode, (NodePosition)2);
		treeAssets.ActiveNode = activeNode;
		treeAssets_AfterSelect(null, null);
	}

	private void btnAssetsDown_Click(object sender, EventArgs e)
	{
		HasChanges = true;
		UltraTreeNode activeNode = treeAssets.ActiveNode;
		treeAssets.ActiveNode.Reposition(treeAssets.ActiveNode, (NodePosition)3);
		treeAssets.ActiveNode = activeNode;
		treeAssets_AfterSelect(null, null);
	}

	private void btnLiabilitiesUp_Click(object sender, EventArgs e)
	{
		HasChanges = true;
		UltraTreeNode activeNode = treeLiabilities.ActiveNode;
		treeLiabilities.ActiveNode.Reposition(treeLiabilities.ActiveNode, (NodePosition)2);
		treeLiabilities.ActiveNode = activeNode;
		treeLiabilities_AfterSelect(null, null);
	}

	private void btnLiabilitiesDown_Click(object sender, EventArgs e)
	{
		HasChanges = true;
		UltraTreeNode activeNode = treeLiabilities.ActiveNode;
		treeLiabilities.ActiveNode.Reposition(treeLiabilities.ActiveNode, (NodePosition)3);
		treeLiabilities.ActiveNode = activeNode;
		treeLiabilities_AfterSelect(null, null);
	}

	private void btnPreview_Click(object sender, EventArgs e)
	{
		if (HasChanges || dtAssets.Select("BalanceSheetID =0").Length != 0 || dtLiabilities.Select("BalanceSheetID =0").Length != 0)
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
				GlobalVariables.InformationMB.Show(" برجاء تحديث تقرير الميزانية العمومية ", "Please Update Balance Sheet Reports");
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
		BalanceSheet.Delete(GlobalVariables.UserID);
		for (int i = 0; i < ((DisposableObjectCollectionBase)treeAssets.Nodes).Count; i++)
		{
			BalanceSheet.Insert_Update("-1", ((KeyedSubObjectBase)treeAssets.Nodes[i]).Key, "Null", treeAssets.Nodes[i].Index.ToString(), "1", GlobalVariables.UserID);
			for (int j = 0; j < ((DisposableObjectCollectionBase)treeAssets.Nodes[i].Nodes).Count; j++)
			{
				BalanceSheet.Insert_Update("-1", ((KeyedSubObjectBase)treeAssets.Nodes[i].Nodes[j]).Key, ((KeyedSubObjectBase)treeAssets.Nodes[i]).Key, treeAssets.Nodes[i].Nodes[j].Index.ToString(), "1", GlobalVariables.UserID);
			}
		}
		for (int k = 0; k < ((DisposableObjectCollectionBase)treeLiabilities.Nodes).Count; k++)
		{
			BalanceSheet.Insert_Update("-1", ((KeyedSubObjectBase)treeLiabilities.Nodes[k]).Key, "Null", treeLiabilities.Nodes[k].Index.ToString(), "0", GlobalVariables.UserID);
			for (int l = 0; l < ((DisposableObjectCollectionBase)treeLiabilities.Nodes[k].Nodes).Count; l++)
			{
				BalanceSheet.Insert_Update("-1", ((KeyedSubObjectBase)treeLiabilities.Nodes[k].Nodes[l]).Key, ((KeyedSubObjectBase)treeLiabilities.Nodes[k]).Key, treeLiabilities.Nodes[k].Nodes[l].Index.ToString(), "0", GlobalVariables.UserID);
			}
		}
		HasChanges = false;
	}

	public string GetReportName()
	{
		if (((UltraToggleEditorBase)chkWithLogo).Checked)
		{
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_A_BalanceSheet_A.rpt" : "Rep_A_BalanceSheet_E.rpt");
		}
		return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_A_BalanceSheet_A_nologo.rpt" : "Rep_A_BalanceSheet_E_nologo.rpt");
	}

	public void ShowReport()
	{
		GetBranches();
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@Date", dtpToDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", text);
		GlobalVariables.ReportDocument.SetParameterValue("@BranchsNames", BranchesNames);
		GlobalVariables.ReportDocument.SetParameterValue("@CurrencyName", (cboCurrency.SelectedIndex == -1) ? "" : ((Control)(object)cboCurrency).Text);
		GlobalVariables.ReportDocument.SetParameterValue("@ExchangeRate", (((Control)(object)txtExchangeRate).Text == "" || cboCurrency.SelectedIndex == -1) ? "1" : ((Control)(object)txtExchangeRate).Text);
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches, "Rep_A_BalanceSheet_Assets");
		GlobalVariables.ReportDocument.SetParameterValue("@Approved", -1, "Rep_A_BalanceSheet_Assets");
		GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime, "Rep_A_BalanceSheet_Assets");
		GlobalVariables.ReportDocument.SetParameterValue("@IsAsset", true, "Rep_A_BalanceSheet_Assets");
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked, "Rep_A_BalanceSheet_Assets");
		GlobalVariables.ReportDocument.SetParameterValue("@ExchangeRate", (((Control)(object)txtExchangeRate).Text == "" || cboCurrency.SelectedIndex == -1) ? "1" : ((Control)(object)txtExchangeRate).Text, "Rep_A_BalanceSheet_Assets");
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches, "Rep_A_BalanceSheet_Liabilities");
		GlobalVariables.ReportDocument.SetParameterValue("@Approved", -1, "Rep_A_BalanceSheet_Liabilities");
		GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime, "Rep_A_BalanceSheet_Liabilities");
		GlobalVariables.ReportDocument.SetParameterValue("@IsAsset", false, "Rep_A_BalanceSheet_Liabilities");
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked, "Rep_A_BalanceSheet_Liabilities");
		GlobalVariables.ReportDocument.SetParameterValue("@ExchangeRate", (((Control)(object)txtExchangeRate).Text == "" || cboCurrency.SelectedIndex == -1) ? "1" : ((Control)(object)txtExchangeRate).Text, "Rep_A_BalanceSheet_Liabilities");
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
		string text = GroupNamePath.Substring(0, GroupNamePath.IndexOf("AccountID") + 9);
		if (text.IndexOf("AccountID") < 0)
		{
			return;
		}
		string text2 = GroupNamePath.Replace(text, "").Replace(",", "").Replace('[', ',')
			.Replace(']', ',');
		try
		{
			if (text2 == "," + GlobalVariables.dtSystemAccounts.Select("AccountNameEn='ProfitAndLossAccount'")[0]["AccountID"].ToString() + ",")
			{
				frmProfitAndLoss frmProfitAndLoss2 = new frmProfitAndLoss();
				GlobalVariables.ReportDocument = new ReportDocument();
				frmProfitAndLoss2.ShowReport(Branches, BranchesNames, -1, (cboCurrency.SelectedIndex == -1) ? "1" : ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "" || cboCurrency.SelectedIndex == -1) ? "1" : ((Control)(object)txtExchangeRate).Text, new DateTime(dtpToDate.DateTime.Year, 1, 1), dtpToDate.DateTime, ((UltraToggleEditorBase)chkIsArabic).Checked);
			}
			else
			{
				frmAccountsLedgerRep frmAccountsLedgerRep2 = new frmAccountsLedgerRep();
				GlobalVariables.ReportDocument = new ReportDocument();
				frmAccountsLedgerRep2.ShowReport(Branches, BranchesNames, text2, "-1", "-1", -1, new DateTime(dtpToDate.DateTime.Year, 1, 1), dtpToDate.DateTime, ((UltraToggleEditorBase)chkIsArabic).Checked);
			}
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
		GlobalVariables.ReportDocument.SetParameterValue("@Date", dtpToDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", text);
		GlobalVariables.ReportDocument.SetParameterValue("@BranchsNames", BranchesNames);
		GlobalVariables.ReportDocument.SetParameterValue("@CurrencyName", (cboCurrency.SelectedIndex == -1) ? "" : ((Control)(object)cboCurrency).Text);
		GlobalVariables.ReportDocument.SetParameterValue("@ExchangeRate", (((Control)(object)txtExchangeRate).Text == "" || cboCurrency.SelectedIndex == -1) ? "1" : ((Control)(object)txtExchangeRate).Text);
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches, "Rep_A_BalanceSheet_Assets");
		GlobalVariables.ReportDocument.SetParameterValue("@Approved", -1, "Rep_A_BalanceSheet_Assets");
		GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime, "Rep_A_BalanceSheet_Assets");
		GlobalVariables.ReportDocument.SetParameterValue("@IsAsset", true, "Rep_A_BalanceSheet_Assets");
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked, "Rep_A_BalanceSheet_Assets");
		GlobalVariables.ReportDocument.SetParameterValue("@ExchangeRate", (((Control)(object)txtExchangeRate).Text == "" || cboCurrency.SelectedIndex == -1) ? "1" : ((Control)(object)txtExchangeRate).Text, "Rep_A_BalanceSheet_Assets");
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches, "Rep_A_BalanceSheet_Liabilities");
		GlobalVariables.ReportDocument.SetParameterValue("@Approved", -1, "Rep_A_BalanceSheet_Liabilities");
		GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime, "Rep_A_BalanceSheet_Liabilities");
		GlobalVariables.ReportDocument.SetParameterValue("@IsAsset", false, "Rep_A_BalanceSheet_Liabilities");
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked, "Rep_A_BalanceSheet_Liabilities");
		GlobalVariables.ReportDocument.SetParameterValue("@ExchangeRate", (((Control)(object)txtExchangeRate).Text == "" || cboCurrency.SelectedIndex == -1) ? "1" : ((Control)(object)txtExchangeRate).Text, "Rep_A_BalanceSheet_Liabilities");
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

	private void btnLoadDefault_Click(object sender, EventArgs e)
	{
		if (HasChanges)
		{
			HasChanges = false;
		}
		else
		{
			BalanceSheet.Delete(GlobalVariables.UserID);
		}
		dtAssets = BalanceSheet.FillTree("1", GlobalVariables.IsArabic ? "1" : "0");
		dtLiabilities = BalanceSheet.FillTree("0", GlobalVariables.IsArabic ? "1" : "0");
		TreeFunctions.FillTree(treeAssets, dtAssets, "ParentID", "AccountID", "AccountName", "AccountNumber", "IsMain");
		TreeFunctions.FillTree(treeLiabilities, dtLiabilities, "ParentID", "AccountID", "AccountName", "AccountNumber", "IsMain");
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

	private void frmBalanceSheet_Load(object sender, EventArgs e)
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
				dtpToDate.MinDate = GlobalVariables.MinOpenedDate;
			}
			else
			{
				dtpToDate.MinDate = dateTime;
			}
		}
		else if (num > 0)
		{
			dtpToDate.MinDate = dateTime;
		}
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
	}

	private void cboCurrency_ValueChanged(object sender, EventArgs e)
	{
	}

	private void txtExchangeRate_KeyPress(object sender, KeyPressEventArgs e)
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
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Expected O, but got Unknown
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Expected O, but got Unknown
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Expected O, but got Unknown
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Expected O, but got Unknown
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected O, but got Unknown
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected O, but got Unknown
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Expected O, but got Unknown
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Expected O, but got Unknown
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Expected O, but got Unknown
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Expected O, but got Unknown
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Expected O, but got Unknown
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Expected O, but got Unknown
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Expected O, but got Unknown
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Expected O, but got Unknown
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Expected O, but got Unknown
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Expected O, but got Unknown
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Expected O, but got Unknown
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Expected O, but got Unknown
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Expected O, but got Unknown
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Expected O, but got Unknown
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Expected O, but got Unknown
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Expected O, but got Unknown
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Expected O, but got Unknown
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Expected O, but got Unknown
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Expected O, but got Unknown
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Expected O, but got Unknown
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Expected O, but got Unknown
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Accounting.FinancialStatements.frmBalanceSheet));
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
		this.treeAssets = new UltraTree();
		this.treeLiabilities = new UltraTree();
		this.ultraLabel1 = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.lblLiabilities = new UltraLabel();
		this.lblAssets = new UltraLabel();
		this.btnMoveToLiabilities = new UltraButton();
		this.btnMoveToAssets = new UltraButton();
		this.btnPreview = new UltraButton();
		this.btnClose = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnLiabilitiesDown = new UltraButton();
		this.btnLiabilitiesUp = new UltraButton();
		this.btnAssetsDown = new UltraButton();
		this.btnAssetsUp = new UltraButton();
		this.chkIsArabic = new UltraCheckEditor();
		this.cboReportType = new UltraComboEditor();
		this.lblReportType = new UltraLabel();
		this.chkWithLogo = new UltraCheckEditor();
		this.chkAllBranches = new UltraCheckEditor();
		this.dtpToDate = new UltraDateTimeEditor();
		this.clbBranches = new System.Windows.Forms.CheckedListBox();
		this.ultraLabel3 = new UltraLabel();
		this.btnLoadDefault = new UltraButton();
		this.btnUpdate = new UltraButton();
		this.btnDelete = new UltraButton();
		this.btnNew = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.txtExchangeRate = new UltraTextEditor();
		this.lblExchangeRate = new UltraLabel();
		this.lblCurrency = new UltraLabel();
		this.cboCurrency = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.treeAssets).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.treeLiabilities).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsArabic).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboReportType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithLogo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.treeAssets, "treeAssets");
		((System.Windows.Forms.Control)(object)this.treeAssets).AllowDrop = true;
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val, "appearance1");
		this.treeAssets.Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.treeAssets).Name = "treeAssets";
		((UltraControlBase)this.treeAssets).UseAppStyling = false;
		this.treeAssets.AfterSelect += new AfterNodeSelectEventHandler(treeAssets_AfterSelect);
		resources.ApplyResources(this.treeLiabilities, "treeLiabilities");
		((System.Windows.Forms.Control)(object)this.treeLiabilities).AllowDrop = true;
		((AppearanceBase)val2).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val2, "appearance2");
		this.treeLiabilities.Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.treeLiabilities).Name = "treeLiabilities";
		((UltraControlBase)this.treeLiabilities).UseAppStyling = false;
		this.treeLiabilities.AfterSelect += new AfterNodeSelectEventHandler(treeLiabilities_AfterSelect);
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((UltraControlBase)this.ultraLabel1).UseAppStyling = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.Black;
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((UltraControlBase)this.ultraLabel2).UseAppStyling = false;
		resources.ApplyResources(this.lblLiabilities, "lblLiabilities");
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.lblLiabilities).Appearance = (AppearanceBase)(object)val5;
		this.lblLiabilities.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLiabilities).Name = "lblLiabilities";
		((ControlBase)this.lblLiabilities).WrapText = false;
		resources.ApplyResources(this.lblAssets, "lblAssets");
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.lblAssets).Appearance = (AppearanceBase)(object)val6;
		this.lblAssets.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAssets).Name = "lblAssets";
		((ControlBase)this.lblAssets).WrapText = false;
		resources.ApplyResources(this.btnMoveToLiabilities, "btnMoveToLiabilities");
		((System.Windows.Forms.Control)(object)this.btnMoveToLiabilities).Name = "btnMoveToLiabilities";
		((System.Windows.Forms.Control)(object)this.btnMoveToLiabilities).Click += new System.EventHandler(btnMoveToLiabilities_Click);
		resources.ApplyResources(this.btnMoveToAssets, "btnMoveToAssets");
		((System.Windows.Forms.Control)(object)this.btnMoveToAssets).Name = "btnMoveToAssets";
		((System.Windows.Forms.Control)(object)this.btnMoveToAssets).Click += new System.EventHandler(btnMoveToAssets_Click);
		resources.ApplyResources(this.btnPreview, "btnPreview");
		((System.Windows.Forms.Control)(object)this.btnPreview).Name = "btnPreview";
		((System.Windows.Forms.Control)(object)this.btnPreview).Click += new System.EventHandler(btnPreview_Click);
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val7).Image = resources.GetObject("appearance7.Image");
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val7;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val8).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnLiabilitiesDown, "btnLiabilitiesDown");
		((AppearanceBase)val9).Image = resources.GetObject("appearance9.Image");
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.btnLiabilitiesDown).Appearance = (AppearanceBase)(object)val9;
		((ControlBase)this.btnLiabilitiesDown).ImageSize = new System.Drawing.Size(18, 18);
		((System.Windows.Forms.Control)(object)this.btnLiabilitiesDown).Name = "btnLiabilitiesDown";
		((System.Windows.Forms.Control)(object)this.btnLiabilitiesDown).Click += new System.EventHandler(btnLiabilitiesDown_Click);
		resources.ApplyResources(this.btnLiabilitiesUp, "btnLiabilitiesUp");
		((AppearanceBase)val10).Image = ERP.Properties.Resources.btnUP;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.btnLiabilitiesUp).Appearance = (AppearanceBase)(object)val10;
		((ControlBase)this.btnLiabilitiesUp).ImageSize = new System.Drawing.Size(18, 18);
		((System.Windows.Forms.Control)(object)this.btnLiabilitiesUp).Name = "btnLiabilitiesUp";
		((System.Windows.Forms.Control)(object)this.btnLiabilitiesUp).Click += new System.EventHandler(btnLiabilitiesUp_Click);
		resources.ApplyResources(this.btnAssetsDown, "btnAssetsDown");
		((AppearanceBase)val11).Image = resources.GetObject("appearance11.Image");
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.btnAssetsDown).Appearance = (AppearanceBase)(object)val11;
		((ControlBase)this.btnAssetsDown).ImageSize = new System.Drawing.Size(18, 18);
		((System.Windows.Forms.Control)(object)this.btnAssetsDown).Name = "btnAssetsDown";
		((System.Windows.Forms.Control)(object)this.btnAssetsDown).Click += new System.EventHandler(btnAssetsDown_Click);
		resources.ApplyResources(this.btnAssetsUp, "btnAssetsUp");
		((AppearanceBase)val12).Image = ERP.Properties.Resources.btnUP;
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.btnAssetsUp).Appearance = (AppearanceBase)(object)val12;
		((ControlBase)this.btnAssetsUp).ImageSize = new System.Drawing.Size(18, 18);
		((System.Windows.Forms.Control)(object)this.btnAssetsUp).Name = "btnAssetsUp";
		((System.Windows.Forms.Control)(object)this.btnAssetsUp).Click += new System.EventHandler(btnAssetsUp_Click);
		resources.ApplyResources(this.chkIsArabic, "chkIsArabic");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Blue;
		resources.ApplyResources(val13, "appearance13");
		((UltraToggleEditorBase)this.chkIsArabic).Appearance = (AppearanceBase)(object)val13;
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
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Blue;
		resources.ApplyResources(val14, "appearance14");
		((ControlBase)this.lblReportType).Appearance = (AppearanceBase)(object)val14;
		this.lblReportType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReportType).Name = "lblReportType";
		((ControlBase)this.lblReportType).WrapText = false;
		resources.ApplyResources(this.chkWithLogo, "chkWithLogo");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Blue;
		resources.ApplyResources(val15, "appearance15");
		((UltraToggleEditorBase)this.chkWithLogo).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.chkWithLogo).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkWithLogo).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkWithLogo).Name = "chkWithLogo";
		resources.ApplyResources(this.chkAllBranches, "chkAllBranches");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Blue;
		resources.ApplyResources(val16, "appearance16");
		((UltraToggleEditorBase)this.chkAllBranches).Appearance = (AppearanceBase)(object)val16;
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
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Blue;
		resources.ApplyResources(val17, "appearance17");
		((ControlBase)this.ultraLabel3).Appearance = (AppearanceBase)(object)val17;
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.btnLoadDefault, "btnLoadDefault");
		((System.Windows.Forms.Control)(object)this.btnLoadDefault).Name = "btnLoadDefault";
		((System.Windows.Forms.Control)(object)this.btnLoadDefault).Click += new System.EventHandler(btnLoadDefault_Click);
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
		((AppearanceBase)val18).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val18).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val18).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val18, "appearance18");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val18;
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
		((TextEditorControlBase)this.cboCurrency).ValueChanged += new System.EventHandler(cboCurrency_ValueChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnUpdate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDelete);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNew);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAssetsDown);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAssetsUp);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnLoadDefault);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsArabic);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboReportType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReportType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkWithLogo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpToDate);
		base.Controls.Add(this.clbBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnLiabilitiesDown);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnLiabilitiesUp);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPreview);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnMoveToLiabilities);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnMoveToAssets);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAssets);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLiabilities);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.treeLiabilities);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.treeAssets);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmBalanceSheet";
		base.Load += new System.EventHandler(frmBalanceSheet_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.treeAssets, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.treeLiabilities, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLiabilities, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAssets, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnMoveToAssets, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnMoveToLiabilities, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPreview, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnLiabilitiesUp, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnLiabilitiesDown, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex(this.clbBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkWithLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsArabic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnLoadDefault, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAssetsUp, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAssetsDown, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNew, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExchangeRate, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.treeAssets).EndInit();
		((System.ComponentModel.ISupportInitialize)this.treeLiabilities).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsArabic).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboReportType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithLogo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
