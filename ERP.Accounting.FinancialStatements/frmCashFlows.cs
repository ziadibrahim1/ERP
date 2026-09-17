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
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.Accounting.FinancialStatements;

public class frmCashFlows : frmBase
{
	private string Branches;

	private string BranchesNames;

	private bool HasChanges;

	private DataTable dtReports;

	private DataTable dtCashFlows;

	private DataTable dtAccounts;

	private DataTable dtBranches;

	private DataTable dtCurrency;

	private IContainer components = null;

	public UltraTree treeCashFlows;

	public UltraTree treeAccounts;

	public UltraLabel ultraLabel1;

	public UltraLabel ultraLabel2;

	public UltraLabel lblAccounts;

	public UltraLabel lblCashFlows;

	public UltraButton btnMoveToAccounts;

	public UltraButton btnMoveToCashFlows;

	public UltraButton btnPreview;

	public UltraButton btnClose;

	public UltraLabel lblTitle;

	public UltraButton btnAccountsDown;

	public UltraButton btnAccountsUp;

	public UltraButton btnCashFlowsDown;

	public UltraButton btnCashFlowsUp;

	protected internal UltraCheckEditor chkIsArabic;

	public UltraComboEditor cboReportType;

	public UltraLabel lblReportType;

	public UltraCheckEditor chkWithLogo;

	public UltraCheckEditor chkAllBranches;

	protected internal CheckedListBox clbBranches;

	public UltraButton btnLoadDefault;

	public UltraButton btnUpdate;

	public UltraButton btnDelete;

	public UltraButton btnNew;

	public UltraLabel lblTitle2;

	private UltraTextEditor txtExchangeRate;

	private UltraLabel lblExchangeRate;

	private UltraLabel lblCurrency;

	private UltraComboEditor cboCurrency;

	public UltraDateTimeEditor dtpFromDate;

	public UltraLabel ultraLabel3;

	public UltraLabel ultraLabel4;

	public UltraDateTimeEditor dtpToDate;

	public frmCashFlows()
	{
		InitializeComponent();
		treeAccounts.Override.ActiveNodeAppearance.BackColor = Color.Gray;
		treeCashFlows.Override.ActiveNodeAppearance.BackColor = Color.Gray;
		dtpToDate.Value = DateTime.Now.Date.AddHours(23.0).AddMinutes(59.0).AddSeconds(59.0);
		cboReportType.Items.Clear();
	}

	public override void PrepareData()
	{
		((TextEditorControlBase)cboReportType).Clear();
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboReportType, dtReports, "ReportID", "ReportName");
		cboReportType.SelectedIndex = 0;
		((UltraToggleEditorBase)chkIsArabic).Checked = GlobalVariables.IsArabic;
		UltraLabel obj = ultraLabel1;
		bool useAppStyling = (((UltraControlBase)ultraLabel2).UseAppStyling = false);
		((UltraControlBase)obj).UseAppStyling = useAppStyling;
		AppearanceBase appearance = ((ControlBase)ultraLabel1).Appearance;
		Color backColor = (((ControlBase)ultraLabel2).Appearance.BackColor = Color.DarkBlue);
		appearance.BackColor = backColor;
		AppearanceBase appearance2 = ((ControlBase)ultraLabel1).Appearance;
		backColor = (((ControlBase)ultraLabel2).Appearance.ForeColor = Color.DarkBlue);
		appearance2.ForeColor = backColor;
		dtCashFlows = CashFlowsDetails.FillTree(GlobalVariables.IsArabic ? "1" : "0");
		dtAccounts = CashFlowsDetails.AccountsFillTree(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0");
		TreeFunctions.FillTree(treeCashFlows, dtCashFlows, "ParentID", "ItemID", "ItemName", "ItemNumber", "IsMain");
		TreeFunctions.FillTree(treeAccounts, dtAccounts, "ParentID", "AccountID", "AccountName", "AccountNumber", "IsMain");
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
		if (treeAccounts.ActiveNode.Level == 1)
		{
			((Control)(object)btnAccountsUp).Enabled = treeAccounts.ActiveNode.Index != 0;
			((Control)(object)btnAccountsDown).Enabled = treeAccounts.ActiveNode.Index != ((DisposableObjectCollectionBase)treeAccounts.ActiveNode.Parent.Nodes).Count - 1;
		}
	}

	private void treeAssets_AfterSelect(object sender, SelectEventArgs e)
	{
		if (treeCashFlows.ActiveNode.Level == 1)
		{
			((Control)(object)btnCashFlowsUp).Enabled = treeCashFlows.ActiveNode.Index != 0;
			((Control)(object)btnCashFlowsDown).Enabled = treeCashFlows.ActiveNode.Index != ((DisposableObjectCollectionBase)treeCashFlows.ActiveNode.Parent.Nodes).Count - 1;
		}
	}

	private void btnMoveToCashFlows_Click(object sender, EventArgs e)
	{
		HasChanges = true;
		if (treeAccounts.ActiveNode.Level == 1)
		{
			UltraTreeNode activeNode = treeAccounts.ActiveNode;
			treeAccounts.ActiveNode.Remove();
			if (treeCashFlows.ActiveNode.Level == 0)
			{
				treeCashFlows.ActiveNode.Nodes.Add(activeNode);
				treeCashFlows.ActiveNode.ExpandAll();
			}
			else
			{
				treeCashFlows.ActiveNode.Parent.Nodes.Add(activeNode);
			}
		}
		else
		{
			if (treeAccounts.ActiveNode.Level != 0)
			{
				return;
			}
			int num;
			for (num = 0; num < ((DisposableObjectCollectionBase)treeAccounts.ActiveNode.Nodes).Count; num++)
			{
				UltraTreeNode val = treeAccounts.ActiveNode.Nodes[num];
				treeAccounts.ActiveNode.Nodes[num].Remove();
				num--;
				if (treeCashFlows.ActiveNode.Level == 0)
				{
					treeCashFlows.ActiveNode.Nodes.Add(val);
					treeCashFlows.ActiveNode.ExpandAll();
				}
				else
				{
					treeCashFlows.ActiveNode.Parent.Nodes.Add(val);
				}
			}
		}
	}

	private void btnMoveToAccounts_Click(object sender, EventArgs e)
	{
		HasChanges = true;
		if (treeCashFlows.ActiveNode.Level == 1)
		{
			UltraTreeNode activeNode = treeCashFlows.ActiveNode;
			treeCashFlows.ActiveNode.Remove();
			if (treeAccounts.ActiveNode.Level == 0)
			{
				treeAccounts.ActiveNode.Nodes.Add(activeNode);
				treeAccounts.ActiveNode.ExpandAll();
			}
			else
			{
				treeAccounts.ActiveNode.Parent.Nodes.Add(activeNode);
			}
		}
		else
		{
			if (treeCashFlows.ActiveNode.Level != 0)
			{
				return;
			}
			int num;
			for (num = 0; num < ((DisposableObjectCollectionBase)treeCashFlows.ActiveNode.Nodes).Count; num++)
			{
				UltraTreeNode val = treeCashFlows.ActiveNode.Nodes[num];
				treeCashFlows.ActiveNode.Nodes[num].Remove();
				num--;
				if (treeAccounts.ActiveNode.Level == 0)
				{
					treeAccounts.ActiveNode.Nodes.Add(val);
					treeAccounts.ActiveNode.ExpandAll();
				}
				else
				{
					treeAccounts.ActiveNode.Parent.Nodes.Add(val);
				}
			}
		}
	}

	private void btnAssetsUp_Click(object sender, EventArgs e)
	{
		HasChanges = true;
		UltraTreeNode activeNode = treeCashFlows.ActiveNode;
		treeCashFlows.ActiveNode.Reposition(treeCashFlows.ActiveNode, (NodePosition)2);
		treeCashFlows.ActiveNode = activeNode;
		treeAssets_AfterSelect(null, null);
	}

	private void btnAssetsDown_Click(object sender, EventArgs e)
	{
		HasChanges = true;
		UltraTreeNode activeNode = treeCashFlows.ActiveNode;
		treeCashFlows.ActiveNode.Reposition(treeCashFlows.ActiveNode, (NodePosition)3);
		treeCashFlows.ActiveNode = activeNode;
		treeAssets_AfterSelect(null, null);
	}

	private void btnLiabilitiesUp_Click(object sender, EventArgs e)
	{
		HasChanges = true;
		UltraTreeNode activeNode = treeAccounts.ActiveNode;
		treeAccounts.ActiveNode.Reposition(treeAccounts.ActiveNode, (NodePosition)2);
		treeAccounts.ActiveNode = activeNode;
		treeLiabilities_AfterSelect(null, null);
	}

	private void btnLiabilitiesDown_Click(object sender, EventArgs e)
	{
		HasChanges = true;
		UltraTreeNode activeNode = treeAccounts.ActiveNode;
		treeAccounts.ActiveNode.Reposition(treeAccounts.ActiveNode, (NodePosition)3);
		treeAccounts.ActiveNode = activeNode;
		treeLiabilities_AfterSelect(null, null);
	}

	private void btnPreview_Click(object sender, EventArgs e)
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
		try
		{
			GlobalVariables.ReportDocument = new ReportDocument();
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
		CashFlowsDetails.Delete(GlobalVariables.UserID);
		for (int i = 0; i < ((DisposableObjectCollectionBase)treeCashFlows.Nodes).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)treeCashFlows.Nodes[i].Nodes).Count; j++)
			{
				CashFlowsDetails.Insert_Update("-1", ((KeyedSubObjectBase)treeCashFlows.Nodes[i]).Key, ((KeyedSubObjectBase)treeCashFlows.Nodes[i].Nodes[j]).Key, GlobalVariables.UserID);
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
		GlobalVariables.ReportDocument.SetParameterValue("@FromDate", dtpFromDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", text);
		GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches);
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
			CashFlowsDetails.Delete(GlobalVariables.UserID);
		}
		dtCashFlows = CashFlowsDetails.FillTree(GlobalVariables.IsArabic ? "1" : "0");
		dtAccounts = CashFlowsDetails.AccountsFillTree(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0");
		TreeFunctions.FillTree(treeCashFlows, dtCashFlows, "ParentID", "ItemID", "ItemName", "ItemNumber", "IsMain");
		TreeFunctions.FillTree(treeAccounts, dtAccounts, "ParentID", "AccountID", "AccountName", "AccountNumber", "IsMain");
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

	private void frmCashFlows_Load(object sender, EventArgs e)
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
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected O, but got Unknown
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected O, but got Unknown
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Expected O, but got Unknown
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Expected O, but got Unknown
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Expected O, but got Unknown
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Expected O, but got Unknown
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Expected O, but got Unknown
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Expected O, but got Unknown
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Expected O, but got Unknown
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Expected O, but got Unknown
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Expected O, but got Unknown
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Expected O, but got Unknown
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Expected O, but got Unknown
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Expected O, but got Unknown
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Expected O, but got Unknown
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Expected O, but got Unknown
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Expected O, but got Unknown
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Expected O, but got Unknown
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Expected O, but got Unknown
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Expected O, but got Unknown
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Expected O, but got Unknown
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Expected O, but got Unknown
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Expected O, but got Unknown
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Accounting.FinancialStatements.frmCashFlows));
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
		this.treeCashFlows = new UltraTree();
		this.treeAccounts = new UltraTree();
		this.ultraLabel1 = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.lblAccounts = new UltraLabel();
		this.lblCashFlows = new UltraLabel();
		this.btnMoveToAccounts = new UltraButton();
		this.btnMoveToCashFlows = new UltraButton();
		this.btnPreview = new UltraButton();
		this.btnClose = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnAccountsDown = new UltraButton();
		this.btnAccountsUp = new UltraButton();
		this.btnCashFlowsDown = new UltraButton();
		this.btnCashFlowsUp = new UltraButton();
		this.chkIsArabic = new UltraCheckEditor();
		this.cboReportType = new UltraComboEditor();
		this.lblReportType = new UltraLabel();
		this.chkWithLogo = new UltraCheckEditor();
		this.chkAllBranches = new UltraCheckEditor();
		this.clbBranches = new System.Windows.Forms.CheckedListBox();
		this.btnLoadDefault = new UltraButton();
		this.btnUpdate = new UltraButton();
		this.btnDelete = new UltraButton();
		this.btnNew = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.txtExchangeRate = new UltraTextEditor();
		this.lblExchangeRate = new UltraLabel();
		this.lblCurrency = new UltraLabel();
		this.cboCurrency = new UltraComboEditor();
		this.dtpFromDate = new UltraDateTimeEditor();
		this.ultraLabel3 = new UltraLabel();
		this.ultraLabel4 = new UltraLabel();
		this.dtpToDate = new UltraDateTimeEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.treeCashFlows).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.treeAccounts).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsArabic).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboReportType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithLogo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.treeCashFlows, "treeCashFlows");
		((System.Windows.Forms.Control)(object)this.treeCashFlows).AllowDrop = true;
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val, "appearance1");
		this.treeCashFlows.Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.treeCashFlows).Name = "treeCashFlows";
		((UltraControlBase)this.treeCashFlows).UseAppStyling = false;
		this.treeCashFlows.AfterSelect += new AfterNodeSelectEventHandler(treeAssets_AfterSelect);
		resources.ApplyResources(this.treeAccounts, "treeAccounts");
		((System.Windows.Forms.Control)(object)this.treeAccounts).AllowDrop = true;
		((AppearanceBase)val2).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val2, "appearance2");
		this.treeAccounts.Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.treeAccounts).Name = "treeAccounts";
		((UltraControlBase)this.treeAccounts).UseAppStyling = false;
		this.treeAccounts.AfterSelect += new AfterNodeSelectEventHandler(treeLiabilities_AfterSelect);
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
		resources.ApplyResources(this.lblAccounts, "lblAccounts");
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.lblAccounts).Appearance = (AppearanceBase)(object)val5;
		this.lblAccounts.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAccounts).Name = "lblAccounts";
		((ControlBase)this.lblAccounts).WrapText = false;
		resources.ApplyResources(this.lblCashFlows, "lblCashFlows");
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.lblCashFlows).Appearance = (AppearanceBase)(object)val6;
		this.lblCashFlows.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCashFlows).Name = "lblCashFlows";
		((ControlBase)this.lblCashFlows).WrapText = false;
		resources.ApplyResources(this.btnMoveToAccounts, "btnMoveToAccounts");
		((System.Windows.Forms.Control)(object)this.btnMoveToAccounts).Name = "btnMoveToAccounts";
		((System.Windows.Forms.Control)(object)this.btnMoveToAccounts).Click += new System.EventHandler(btnMoveToAccounts_Click);
		resources.ApplyResources(this.btnMoveToCashFlows, "btnMoveToCashFlows");
		((System.Windows.Forms.Control)(object)this.btnMoveToCashFlows).Name = "btnMoveToCashFlows";
		((System.Windows.Forms.Control)(object)this.btnMoveToCashFlows).Click += new System.EventHandler(btnMoveToCashFlows_Click);
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
		resources.ApplyResources(this.btnAccountsDown, "btnAccountsDown");
		((AppearanceBase)val9).Image = resources.GetObject("appearance9.Image");
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.btnAccountsDown).Appearance = (AppearanceBase)(object)val9;
		((ControlBase)this.btnAccountsDown).ImageSize = new System.Drawing.Size(18, 18);
		((System.Windows.Forms.Control)(object)this.btnAccountsDown).Name = "btnAccountsDown";
		((System.Windows.Forms.Control)(object)this.btnAccountsDown).Click += new System.EventHandler(btnLiabilitiesDown_Click);
		resources.ApplyResources(this.btnAccountsUp, "btnAccountsUp");
		((AppearanceBase)val10).Image = ERP.Properties.Resources.btnUP;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.btnAccountsUp).Appearance = (AppearanceBase)(object)val10;
		((ControlBase)this.btnAccountsUp).ImageSize = new System.Drawing.Size(18, 18);
		((System.Windows.Forms.Control)(object)this.btnAccountsUp).Name = "btnAccountsUp";
		((System.Windows.Forms.Control)(object)this.btnAccountsUp).Click += new System.EventHandler(btnLiabilitiesUp_Click);
		resources.ApplyResources(this.btnCashFlowsDown, "btnCashFlowsDown");
		((AppearanceBase)val11).Image = resources.GetObject("appearance11.Image");
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.btnCashFlowsDown).Appearance = (AppearanceBase)(object)val11;
		((ControlBase)this.btnCashFlowsDown).ImageSize = new System.Drawing.Size(18, 18);
		((System.Windows.Forms.Control)(object)this.btnCashFlowsDown).Name = "btnCashFlowsDown";
		((System.Windows.Forms.Control)(object)this.btnCashFlowsDown).Click += new System.EventHandler(btnAssetsDown_Click);
		resources.ApplyResources(this.btnCashFlowsUp, "btnCashFlowsUp");
		((AppearanceBase)val12).Image = ERP.Properties.Resources.btnUP;
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.btnCashFlowsUp).Appearance = (AppearanceBase)(object)val12;
		((ControlBase)this.btnCashFlowsUp).ImageSize = new System.Drawing.Size(18, 18);
		((System.Windows.Forms.Control)(object)this.btnCashFlowsUp).Name = "btnCashFlowsUp";
		((System.Windows.Forms.Control)(object)this.btnCashFlowsUp).Click += new System.EventHandler(btnAssetsUp_Click);
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
		resources.ApplyResources(this.clbBranches, "clbBranches");
		this.clbBranches.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.clbBranches.CheckOnClick = true;
		this.clbBranches.Name = "clbBranches";
		this.clbBranches.SelectedValueChanged += new System.EventHandler(clbBranches_SelectedValueChanged);
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
		resources.ApplyResources(this.dtpFromDate, "dtpFromDate");
		((System.Windows.Forms.Control)(object)this.dtpFromDate).Name = "dtpFromDate";
		this.dtpFromDate.PromptChar = ' ';
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		((AppearanceBase)val18).ForeColor = System.Drawing.Color.Blue;
		resources.ApplyResources(val18, "appearance18");
		((ControlBase)this.ultraLabel3).Appearance = (AppearanceBase)(object)val18;
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Blue;
		resources.ApplyResources(val19, "appearance19");
		((ControlBase)this.ultraLabel4).Appearance = (AppearanceBase)(object)val19;
		this.ultraLabel4.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		((ControlBase)this.ultraLabel4).WrapText = false;
		resources.ApplyResources(this.dtpToDate, "dtpToDate");
		((System.Windows.Forms.Control)(object)this.dtpToDate).Name = "dtpToDate";
		this.dtpToDate.PromptChar = ' ';
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnUpdate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDelete);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNew);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCashFlowsDown);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCashFlowsUp);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnLoadDefault);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsArabic);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboReportType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReportType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkWithLogo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllBranches);
		base.Controls.Add(this.clbBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAccountsDown);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAccountsUp);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPreview);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnMoveToAccounts);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnMoveToCashFlows);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCashFlows);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAccounts);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.treeAccounts);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.treeCashFlows);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmCashFlows";
		base.Load += new System.EventHandler(frmCashFlows_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.treeCashFlows, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.treeAccounts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAccounts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCashFlows, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnMoveToCashFlows, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnMoveToAccounts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPreview, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAccountsUp, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAccountsDown, 0);
		base.Controls.SetChildIndex(this.clbBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkWithLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsArabic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnLoadDefault, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCashFlowsUp, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCashFlowsDown, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNew, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel4, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpFromDate, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.treeCashFlows).EndInit();
		((System.ComponentModel.ISupportInitialize)this.treeAccounts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsArabic).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboReportType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithLogo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
