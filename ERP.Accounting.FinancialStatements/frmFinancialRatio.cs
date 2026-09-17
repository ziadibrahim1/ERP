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

public class frmFinancialRatio : frmBase
{
	private string Branches;

	private string BranchesNames;

	private bool HasChanges;

	private DataTable dtReports;

	private DataTable dtFinancialRatioDetails;

	private DataTable dtBranches;

	private IContainer components = null;

	public UltraTree treeAssets;

	public UltraLabel lblAssets;

	public UltraButton btnPreview;

	public UltraButton btnClose;

	public UltraLabel lblTitle;

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

	public frmFinancialRatio()
	{
		InitializeComponent();
		treeAssets.Override.ActiveNodeAppearance.BackColor = Color.Gray;
		dtpToDate.Value = DateTime.Now.Date.AddHours(23.0).AddMinutes(59.0).AddSeconds(59.0);
		cboReportType.Items.Clear();
		cboReportType.Items.Add((object)1, GlobalVariables.IsArabic ? "طباعه علي شكل قائمه" : "Print in Statement View");
	}

	public override void PrepareData()
	{
		((TextEditorControlBase)cboReportType).Clear();
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboReportType, dtReports, "ReportID", "ReportName");
		cboReportType.SelectedIndex = 0;
		((UltraToggleEditorBase)chkIsArabic).Checked = GlobalVariables.IsArabic;
		dtFinancialRatioDetails = FinancialRatioDetails.FillTree(GlobalVariables.IsArabic ? "1" : "0");
		TreeFunctions.FillTree(treeAssets, dtFinancialRatioDetails, "ParentID", "AccountID", "AccountName", "AccountNumber", "IsMain");
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

	private void treeAssets_AfterSelect(object sender, SelectEventArgs e)
	{
		if (treeAssets.ActiveNode.Level == 2)
		{
			((Control)(object)btnAssetsUp).Enabled = treeAssets.ActiveNode.Index != 0 || treeAssets.ActiveNode.Parent.Index != 0;
			((Control)(object)btnAssetsDown).Enabled = treeAssets.ActiveNode.Index != ((DisposableObjectCollectionBase)treeAssets.ActiveNode.Parent.Nodes).Count - 1 || treeAssets.ActiveNode.Parent.Index != ((DisposableObjectCollectionBase)treeAssets.ActiveNode.Parent.Parent.Nodes).Count - 1;
		}
		else
		{
			UltraButton obj = btnAssetsDown;
			bool enabled = (((Control)(object)btnAssetsUp).Enabled = false);
			((Control)(object)obj).Enabled = enabled;
		}
	}

	private void btnAssetsUp_Click(object sender, EventArgs e)
	{
		HasChanges = true;
		UltraTreeNode activeNode = treeAssets.ActiveNode;
		if (treeAssets.ActiveNode.Index == 0)
		{
			treeAssets.ActiveNode.Reposition(treeAssets.ActiveNode.Parent.Parent.Nodes[treeAssets.ActiveNode.Parent.Index - 1].Nodes);
		}
		else
		{
			treeAssets.ActiveNode.Reposition(treeAssets.ActiveNode, (NodePosition)2);
		}
		treeAssets.ActiveNode = activeNode;
		treeAssets_AfterSelect(null, null);
	}

	private void btnAssetsDown_Click(object sender, EventArgs e)
	{
		HasChanges = true;
		UltraTreeNode activeNode = treeAssets.ActiveNode;
		if (treeAssets.ActiveNode.Index == ((DisposableObjectCollectionBase)treeAssets.ActiveNode.Parent.Nodes).Count - 1)
		{
			treeAssets.ActiveNode.Reposition(treeAssets.ActiveNode.Parent.Parent.Nodes[treeAssets.ActiveNode.Parent.Index + 1].Nodes);
		}
		else
		{
			treeAssets.ActiveNode.Reposition(treeAssets.ActiveNode, (NodePosition)3);
		}
		treeAssets.ActiveNode = activeNode;
		treeAssets_AfterSelect(null, null);
	}

	private void btnPreview_Click(object sender, EventArgs e)
	{
		if (HasChanges || dtFinancialRatioDetails.Select("FinancialRatioDetailID =-1").Length != 0)
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
		FinancialRatioDetails.Delete(GlobalVariables.UserID);
		for (int i = 0; i < ((DisposableObjectCollectionBase)treeAssets.Nodes).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)treeAssets.Nodes[i].Nodes).Count; j++)
			{
				for (int k = 0; k < ((DisposableObjectCollectionBase)treeAssets.Nodes[i].Nodes[j].Nodes).Count; k++)
				{
					FinancialRatioDetails.Insert_Update("-1", ((KeyedSubObjectBase)treeAssets.Nodes[i].Nodes[j].Nodes[k]).Key, ((KeyedSubObjectBase)treeAssets.Nodes[i].Nodes[j]).Key, treeAssets.Nodes[i].Nodes[j].Nodes[k].Index.ToString(), GlobalVariables.UserID);
				}
			}
		}
		HasChanges = false;
	}

	public string GetReportName()
	{
		if (((UltraToggleEditorBase)chkWithLogo).Checked)
		{
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_A_FinancialRatio_A.rpt" : "Rep_A_FinancialRatio_E.rpt");
		}
		return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_A_FinancialRatio_A_nologo.rpt" : "Rep_A_FinancialRatio_E_nologo.rpt");
	}

	public void ShowReport()
	{
		GetBranches();
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@BranchsNames", BranchesNames);
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches);
		GlobalVariables.ReportDocument.SetParameterValue("@Approved", -1);
		GlobalVariables.ReportDocument.SetParameterValue("@FromDate", new DateTime(dtpToDate.DateTime.Year, 1, 1));
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
		HasChanges = false;
		dtFinancialRatioDetails = FinancialRatioDetails.FillTree(GlobalVariables.IsArabic ? "1" : "0");
		TreeFunctions.FillTree(treeAssets, dtFinancialRatioDetails, "ParentID", "AccountID", "AccountName", "AccountNumber", "IsMain");
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

	private void frmFinancialRatio_Load(object sender, EventArgs e)
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
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Expected O, but got Unknown
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Expected O, but got Unknown
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Expected O, but got Unknown
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Expected O, but got Unknown
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Expected O, but got Unknown
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Expected O, but got Unknown
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Expected O, but got Unknown
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Expected O, but got Unknown
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Expected O, but got Unknown
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Expected O, but got Unknown
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected O, but got Unknown
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Accounting.FinancialStatements.frmFinancialRatio));
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
		this.treeAssets = new UltraTree();
		this.lblAssets = new UltraLabel();
		this.btnPreview = new UltraButton();
		this.btnClose = new UltraButton();
		this.lblTitle = new UltraLabel();
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
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.treeAssets).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsArabic).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboReportType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithLogo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).BeginInit();
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
		resources.ApplyResources(this.lblAssets, "lblAssets");
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.lblAssets).Appearance = (AppearanceBase)(object)val2;
		this.lblAssets.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAssets).Name = "lblAssets";
		((ControlBase)this.lblAssets).WrapText = false;
		resources.ApplyResources(this.btnPreview, "btnPreview");
		((System.Windows.Forms.Control)(object)this.btnPreview).Name = "btnPreview";
		((System.Windows.Forms.Control)(object)this.btnPreview).Click += new System.EventHandler(btnPreview_Click);
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance3.Image");
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnAssetsDown, "btnAssetsDown");
		((AppearanceBase)val5).Image = ERP.Properties.Resources.btnDOWN;
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.btnAssetsDown).Appearance = (AppearanceBase)(object)val5;
		((ControlBase)this.btnAssetsDown).ImageSize = new System.Drawing.Size(18, 18);
		((System.Windows.Forms.Control)(object)this.btnAssetsDown).Name = "btnAssetsDown";
		((System.Windows.Forms.Control)(object)this.btnAssetsDown).Click += new System.EventHandler(btnAssetsDown_Click);
		resources.ApplyResources(this.btnAssetsUp, "btnAssetsUp");
		((AppearanceBase)val6).Image = ERP.Properties.Resources.btnUP;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.btnAssetsUp).Appearance = (AppearanceBase)(object)val6;
		((ControlBase)this.btnAssetsUp).ImageSize = new System.Drawing.Size(18, 18);
		((System.Windows.Forms.Control)(object)this.btnAssetsUp).Name = "btnAssetsUp";
		((System.Windows.Forms.Control)(object)this.btnAssetsUp).Click += new System.EventHandler(btnAssetsUp_Click);
		resources.ApplyResources(this.chkIsArabic, "chkIsArabic");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Blue;
		resources.ApplyResources(val7, "appearance7");
		((UltraToggleEditorBase)this.chkIsArabic).Appearance = (AppearanceBase)(object)val7;
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
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Blue;
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.lblReportType).Appearance = (AppearanceBase)(object)val8;
		this.lblReportType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReportType).Name = "lblReportType";
		((ControlBase)this.lblReportType).WrapText = false;
		resources.ApplyResources(this.chkWithLogo, "chkWithLogo");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Blue;
		resources.ApplyResources(val9, "appearance9");
		((UltraToggleEditorBase)this.chkWithLogo).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.chkWithLogo).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkWithLogo).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkWithLogo).Name = "chkWithLogo";
		resources.ApplyResources(this.chkAllBranches, "chkAllBranches");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Blue;
		resources.ApplyResources(val10, "appearance10");
		((UltraToggleEditorBase)this.chkAllBranches).Appearance = (AppearanceBase)(object)val10;
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
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Blue;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.ultraLabel3).Appearance = (AppearanceBase)(object)val11;
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
		((AppearanceBase)val12).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val12).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this, "$this");
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
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPreview);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAssets);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.treeAssets);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmFinancialRatio";
		base.Load += new System.EventHandler(frmFinancialRatio_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.treeAssets, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAssets, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPreview, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
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
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.treeAssets).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsArabic).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboReportType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithLogo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
