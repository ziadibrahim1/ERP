using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Privilege;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.SystemOptions.GeneralData;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.Accounting.Reports;

public class frmUserSubAccountLedger : frmBase
{
	private string AccountIDs;

	private bool UseCurrency;

	private bool UseCostCenters;

	private DataTable dtReports;

	private DataTable dtSubAccounts;

	private DataTable dtAccounts;

	private IContainer components = null;

	public UltraComboEditor cboSubAccount;

	public UltraLabel lblSubAccount;

	public UltraButton btnPreview;

	public UltraComboEditor cboReportType;

	public UltraLabel lblReportType;

	protected internal UltraCheckEditor chkIsArabic;

	public UltraCheckEditor chkWithLogo;

	public UltraDateTimeEditor dtpToDate;

	public UltraDateTimeEditor dtpFromDate;

	public UltraLabel ultraLabel2;

	public UltraLabel ultraLabel1;

	public UltraButton btnClose;

	public UltraLabel lblTitle;

	public UltraCheckEditor chkAllAccounts;

	protected internal CheckedListBox clbAccounts;

	public UltraButton btnUpdate;

	public UltraButton btnDelete;

	public UltraButton btnNew;

	public UltraLabel lblTitle2;

	public frmUserSubAccountLedger()
	{
		InitializeComponent();
		UseCurrency = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from Currency ").Rows[0][0].ToString()) > 1;
		UseCostCenters = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as CC From A_CostCenters Where IsMain = 0").Rows[0][0].ToString()) > 0;
		DataTable dataTable = Users.Select(GlobalVariables.UserID, "-1", "0", IsFromServer: false);
		((TextEditorControlBase)cboSubAccount).Value = dataTable.Rows[0]["SubAccountID"];
		if (!((TextEditorControlBase)cboSubAccount).Value.Equals(""))
		{
			dtAccounts = SubAccounts_Details.SelectBySubAccountIDWithAccountName(((TextEditorControlBase)cboSubAccount).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			Main.Fillclb(clbAccounts, dtAccounts, "AccountID", "AccountName");
		}
		else
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء ربط المستخدم بحساب الموظف" : "This User Has No Employee Account");
		}
		((UltraToggleEditorBase)chkAllAccounts).Checked = true;
		cboReportType.Items.Clear();
		cboReportType.Items.Add((object)1, GlobalVariables.IsArabic ? "طباعه كل التفاصيل بالعمله المحليه" : "Print With All Details With Local Currency");
		if (UseCostCenters)
		{
			cboReportType.Items.Add((object)2, GlobalVariables.IsArabic ? "طباعه مجمع بمراكز التكلفه بالعمله المحليه" : "Print Grouped By Cost Center With Local Currency");
			cboReportType.Items.Add((object)3, GlobalVariables.IsArabic ? "طباعه مجمع بمراكز التكلفه" : "Print Grouped By Cost Center");
		}
		if (UseCurrency)
		{
			cboReportType.Items.Add((object)4, GlobalVariables.IsArabic ? "طباعه كل التفاصيل" : "Print With All Details");
			cboReportType.Items.Add((object)5, GlobalVariables.IsArabic ? "طباعه مجمع بالعمله" : "Print Grouped By Cost Center");
		}
	}

	public override void PrepareData()
	{
		((TextEditorControlBase)cboReportType).Clear();
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboReportType, dtReports, "ReportID", "ReportName");
		cboReportType.SelectedIndex = 0;
		dtSubAccounts = SubAccounts.SelectBySubAccountTypeIDs(",2,7,", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSubAccount, dtSubAccounts, "SubAccountID", "SubAccountName");
	}

	private void btnPreview_Click(object sender, EventArgs e)
	{
		AccountIDs = ",";
		for (int i = 0; i < clbAccounts.Items.Count; i++)
		{
			if (clbAccounts.GetItemChecked(i))
			{
				AccountIDs = AccountIDs + dtAccounts.Rows[i]["AccountID"].ToString() + ",";
			}
		}
		if (AccountIDs == ",")
		{
			GlobalVariables.InformationMB.Show("برجاء اختيار حساب واحد علي الاقل", "Please Select one Account At Least");
			return;
		}
		try
		{
			GlobalVariables.ReportDocument = new ReportDocument();
			string fileName = ((dtReports.Rows.Count == 0) ? GetReportName() : (GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep" + (((UltraToggleEditorBase)chkIsArabic).Checked ? "_A" : "_E") + (((UltraToggleEditorBase)chkWithLogo).Checked ? "" : "_nologo")].ToString()));
			FileInfo fileInfo = new FileInfo(fileName);
			DateTime lastWriteTime = fileInfo.LastWriteTime;
			if (fileInfo.Exists && lastWriteTime < new DateTime(2022, 2, 1))
			{
				GlobalVariables.InformationMB.Show(" برجاء تحديث تقرير الحسابات التحليلية ", "Please Update SubAccount Ledger Reports");
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

	public string GetReportName()
	{
		if (((UltraToggleEditorBase)chkWithLogo).Checked)
		{
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_A_SubAccountsLedgerLocalCurrency_A.rpt" : "Rep_A_SubAccountsLedgerLocalCurrency_E.rpt");
		}
		return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_A_SubAccountsLedgerLocalCurrency_A_nologo.rpt" : "Rep_A_SubAccountsLedgerLocalCurrency_E_nologo.rpt");
	}

	public void ShowReport()
	{
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", "-1");
		GlobalVariables.ReportDocument.SetParameterValue("@BranchsNames", "");
		GlobalVariables.ReportDocument.SetParameterValue("@SubAccountIDs", string.Concat(",", ((TextEditorControlBase)cboSubAccount).Value, ","));
		GlobalVariables.ReportDocument.SetParameterValue("@AccountIDs", AccountIDs);
		GlobalVariables.ReportDocument.SetParameterValue("@CostCenterIDs", "-1");
		GlobalVariables.ReportDocument.SetParameterValue("@CurrencyIDs", "-1");
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

	private void btnClose_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	private void clbAccounts_SelectedValueChanged(object sender, EventArgs e)
	{
		((UltraToggleEditorBase)chkAllAccounts).CheckedChanged -= chkAllAccounts_CheckedChanged;
		((UltraToggleEditorBase)chkAllAccounts).Checked = clbAccounts.CheckedItems.Count == clbAccounts.Items.Count && clbAccounts.Items.Count > 0;
		((UltraToggleEditorBase)chkAllAccounts).CheckedChanged += chkAllAccounts_CheckedChanged;
	}

	public virtual void chkAllAccounts_CheckedChanged(object sender, EventArgs e)
	{
		for (int i = 0; i < clbAccounts.Items.Count; i++)
		{
			clbAccounts.SetItemChecked(i, ((UltraToggleEditorBase)chkAllAccounts).Checked);
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
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Accounting.Reports.frmUserSubAccountLedger));
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
		this.cboSubAccount = new UltraComboEditor();
		this.lblSubAccount = new UltraLabel();
		this.btnPreview = new UltraButton();
		this.cboReportType = new UltraComboEditor();
		this.lblReportType = new UltraLabel();
		this.chkIsArabic = new UltraCheckEditor();
		this.chkWithLogo = new UltraCheckEditor();
		this.dtpToDate = new UltraDateTimeEditor();
		this.dtpFromDate = new UltraDateTimeEditor();
		this.ultraLabel2 = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.chkAllAccounts = new UltraCheckEditor();
		this.clbAccounts = new System.Windows.Forms.CheckedListBox();
		this.btnUpdate = new UltraButton();
		this.btnDelete = new UltraButton();
		this.btnNew = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboReportType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsArabic).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithLogo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllAccounts).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.cboSubAccount, "cboSubAccount");
		this.cboSubAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSubAccount).Name = "cboSubAccount";
		((TextEditorControlBase)this.cboSubAccount).Nullable = false;
		((EditorButtonControlBase)this.cboSubAccount).ReadOnly = true;
		((AppearanceBase)val).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblSubAccount).Appearance = (AppearanceBase)(object)val;
		this.lblSubAccount.AutoEllipsis = false;
		resources.ApplyResources(this.lblSubAccount, "lblSubAccount");
		((System.Windows.Forms.Control)(object)this.lblSubAccount).Name = "lblSubAccount";
		((ControlBase)this.lblSubAccount).WrapText = false;
		resources.ApplyResources(this.btnPreview, "btnPreview");
		((System.Windows.Forms.Control)(object)this.btnPreview).Name = "btnPreview";
		((System.Windows.Forms.Control)(object)this.btnPreview).Click += new System.EventHandler(btnPreview_Click);
		resources.ApplyResources(this.cboReportType, "cboReportType");
		this.cboReportType.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboReportType).Name = "cboReportType";
		((TextEditorControlBase)this.cboReportType).Nullable = false;
		((TextEditorControlBase)this.cboReportType).ValueChanged += new System.EventHandler(cboReportType_ValueChanged);
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblReportType).Appearance = (AppearanceBase)(object)val2;
		resources.ApplyResources(this.lblReportType, "lblReportType");
		((System.Windows.Forms.Control)(object)this.lblReportType).Name = "lblReportType";
		((AppearanceBase)val3).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val3).ForeColor = System.Drawing.Color.Navy;
		((UltraToggleEditorBase)this.chkIsArabic).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.chkIsArabic).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkIsArabic).BackColorInternal = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkIsArabic).Checked = true;
		((UltraToggleEditorBase)this.chkIsArabic).CheckState = System.Windows.Forms.CheckState.Checked;
		resources.ApplyResources(this.chkIsArabic, "chkIsArabic");
		((System.Windows.Forms.Control)(object)this.chkIsArabic).Name = "chkIsArabic";
		((UltraControlBase)this.chkIsArabic).UseAppStyling = false;
		((AppearanceBase)val4).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Navy;
		((UltraToggleEditorBase)this.chkWithLogo).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.chkWithLogo).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkWithLogo).BackColorInternal = System.Drawing.Color.Transparent;
		resources.ApplyResources(this.chkWithLogo, "chkWithLogo");
		((System.Windows.Forms.Control)(object)this.chkWithLogo).Name = "chkWithLogo";
		resources.ApplyResources(this.dtpToDate, "dtpToDate");
		((System.Windows.Forms.Control)(object)this.dtpToDate).Name = "dtpToDate";
		this.dtpToDate.PromptChar = ' ';
		resources.ApplyResources(this.dtpFromDate, "dtpFromDate");
		((System.Windows.Forms.Control)(object)this.dtpFromDate).Name = "dtpFromDate";
		this.dtpFromDate.PromptChar = ' ';
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val7).Image = resources.GetObject("appearance7.Image");
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
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		((UltraToggleEditorBase)this.chkAllAccounts).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(this.chkAllAccounts, "chkAllAccounts");
		((System.Windows.Forms.Control)(object)this.chkAllAccounts).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllAccounts).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllAccounts).Name = "chkAllAccounts";
		((UltraToggleEditorBase)this.chkAllAccounts).CheckedChanged += new System.EventHandler(chkAllAccounts_CheckedChanged);
		resources.ApplyResources(this.clbAccounts, "clbAccounts");
		this.clbAccounts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.clbAccounts.CheckOnClick = true;
		this.clbAccounts.Name = "clbAccounts";
		this.clbAccounts.SelectedValueChanged += new System.EventHandler(clbAccounts_SelectedValueChanged);
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
		((AppearanceBase)val10).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val10).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val10).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnUpdate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDelete);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNew);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllAccounts);
		base.Controls.Add(this.clbAccounts);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsArabic);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkWithLogo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboReportType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReportType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPreview);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmUserSubAccountLedger";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPreview, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkWithLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsArabic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex(this.clbAccounts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllAccounts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNew, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnUpdate, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboReportType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsArabic).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithLogo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllAccounts).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
