using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.HR;
using BusinessLayer.Privilege;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.HR.Payroll.Transactions;

public class frmEmployeesSalary : frmBase
{
	private DataTable dtItems;

	private DataTable dtSalaryLists;

	private DataTable dtMonths;

	private DataTable dtReports = new DataTable();

	private IContainer components = null;

	public UltraDateTimeEditor dtpToDate;

	public UltraLabel ultraLabel2;

	public UltraButton btnItemsSearch;

	public UltraTextEditor txtItems;

	public UltraTree TreeItems;

	protected internal UltraCheckEditor chkAll;

	public UltraButton btnPreview;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	private UltraComboEditor cboMonth;

	private UltraLabel lblMonth;

	public UltraComboEditor cboSalaryList;

	private UltraLabel lblSalaryList;

	protected internal UltraCheckEditor chkIsArabic;

	public UltraCheckEditor chkWithLogo;

	public frmEmployeesSalary()
	{
		InitializeComponent();
		dtSalaryLists = SalaryLists.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSalaryList, dtSalaryLists, "SalaryListID", "SalaryListName");
		dtpToDate.Value = DBNull.Value;
	}

	public void fillData()
	{
		((UltraToggleEditorBase)chkAll).Checked = false;
		TreeItems.Nodes.Clear();
		if (dtpToDate.Value != null && dtpToDate.Value != DBNull.Value && cboSalaryList.SelectedIndex > -1)
		{
			dtItems = Employees.FillListBySalaryListID(((TextEditorControlBase)cboSalaryList).Value.ToString(), dtpToDate.DateTime.ToString("MM/dd/yyyy 23:59:59"), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			if (dtItems != null)
			{
				TreeFunctions.FillTreeOneLevel(TreeItems, dtItems, "SubAccountID", "SubAccountName");
			}
		}
	}

	public string GetReportName()
	{
		if (dtReports.Rows.Count == 0)
		{
			dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		}
		if (dtReports.Rows.Count > 0)
		{
			if (((UltraToggleEditorBase)chkWithLogo).Checked)
			{
				return GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (((UltraToggleEditorBase)chkIsArabic).Checked ? "_A" : "_E")].ToString();
			}
			return GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (((UltraToggleEditorBase)chkIsArabic).Checked ? "_A_nologo" : "_E_nologo")].ToString();
		}
		if (((UltraToggleEditorBase)chkWithLogo).Checked)
		{
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_HR_EmployeesSalaryHistory_A.rpt" : "Rep_HR_EmployeesSalaryHistory_E.rpt");
		}
		return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_HR_EmployeesSalaryHistory_A_nologo.rpt" : "Rep_HR_EmployeesSalaryHistory_E_nologo.rpt");
	}

	private void btnPreview_Click(object sender, EventArgs e)
	{
		string treeCheckedNodesIDs = TreeFunctions.GetTreeCheckedNodesIDs(TreeItems);
		if (treeCheckedNodesIDs.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى موظف  ", "There is no chosen Employee, please check items ");
			return;
		}
		if (dtpToDate.Value == null && dtpToDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار التاريخ", "please Set End Date ");
			return;
		}
		if (cboMonth.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار الشهر ", "please Select Month ");
			return;
		}
		if (GlobalFunctions.GetOption("SalaryAutoGenerateJV"))
		{
			if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='SalaryExpenseAccount' ")[0]["AccountID"] == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء تحديد حساب مصروف الإجور من حسابات النظام  ", "Please Select Salary Expense Account From SystemAccounts ");
				return;
			}
			if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='AccruedSalaryAccount' ")[0]["AccountID"] == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء تحديد حساب الاجور المستحقة من حسابات النظام  ", "Please Select Accrued Salary Account From SystemAccounts ");
				return;
			}
			if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='IncomeTaxAccount' ")[0]["AccountID"] == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء تحديد حساب ضرائب الدخل من حسابات النظام  ", "Please Select Income Tax Account From SystemAccounts ");
				return;
			}
			if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='SocialInssuranceAccount' ")[0]["AccountID"] == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء تحديد حساب التأمينات الاجتماعية من حسابات النظام  ", "Please Select Social Inssurance Account From SystemAccounts ");
				return;
			}
			if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='SalaryAdvanceAccount' ")[0]["AccountID"] == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء تحديد حساب السلف  من حسابات النظام  ", "Please Select Salary Advance Account From SystemAccounts ");
				return;
			}
		}
		DataTable dataTable = new DataTable();
		Main.StartBulkTrans(FromServer: true);
		try
		{
			dataTable = Employees.CalculateSalary(treeCheckedNodesIDs, dtpToDate.DateTime.ToString("MM/dd/yyyy 23:59:59"), ((TextEditorControlBase)cboMonth).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			GlobalVariables.ReportDocument = new ReportDocument();
			GlobalVariables.ReportDocument.Load(GetReportName());
			GlobalVariables.IsRepOnlineConn = Main.IsSynchronization;
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@EmployeeSalaryHistoryIDs", dataTable.Rows[0]["EmployeeSalaryHistoryIDs"]);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked);
			GlobalVariables.ReportDocument.SetParameterValue("@EmployeeSalaryHistoryIDs", dataTable.Rows[0]["EmployeeSalaryHistoryIDs"], "Rep_HR_EmployeesAllowancesHistory");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked, "Rep_HR_EmployeesAllowancesHistory");
			GlobalVariables.ReportDocument.SetParameterValue("@EmployeeSalaryHistoryIDs", dataTable.Rows[0]["EmployeeSalaryHistoryIDs"], "Rep_HR_EmployeesDeductionsHistory");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked, "Rep_HR_EmployeesDeductionsHistory");
			GlobalVariables.ReportDocument.SetParameterValue("@EmployeeSalaryHistoryIDs", dataTable.Rows[0]["EmployeeSalaryHistoryIDs"], "Rep_HR_EmployeesMotivationsHistory");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked, "Rep_HR_EmployeesMotivationsHistory");
			frmReporViwer2.Tag = base.Tag;
			frmReporViwer2.MdiParent = base.MdiParent;
			frmReporViwer2.TopLevel = false;
			frmReporViwer2.Parent = base.Parent;
			frmReporViwer2.Width = base.Parent.Width;
			frmReporViwer2.Height = base.Parent.Height;
			frmReporViwer2.frmParent = this;
			frmReporViwer2.Show();
			frmReporViwer2.BringToFront();
			GlobalVariables.ReportDocument = null;
			Main.EndBulkTrans(FromServer: true);
		}
		catch (Exception)
		{
			Main.RollbackBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
			return;
		}
		fillData();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	private void TreeItems_AfterCheck(object sender, NodeEventArgs e)
	{
		((UltraToggleEditorBase)chkAll).CheckedChanged -= chkAll_CheckedChanged;
		SetCheckBoxAllState(TreeItems, chkAll);
		((UltraToggleEditorBase)chkAll).CheckedChanged += chkAll_CheckedChanged;
	}

	private void chkAll_CheckedChanged(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		TreeItems.AfterCheck -= new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAll).Checked, TreeItems);
		TreeItems.AfterCheck += new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
	}

	public void SetCheckBoxAllState(UltraTree tree, UltraCheckEditor CheckBox)
	{
		int num = 0;
		for (int i = 0; i < ((DisposableObjectCollectionBase)tree.Nodes).Count; i++)
		{
			if (tree.Nodes[i].CheckedState == CheckState.Unchecked || tree.Nodes[i].CheckedState == CheckState.Indeterminate)
			{
				((UltraToggleEditorBase)CheckBox).Checked = false;
			}
			else
			{
				num++;
			}
		}
		if (num == ((DisposableObjectCollectionBase)tree.Nodes).Count)
		{
			((UltraToggleEditorBase)CheckBox).Checked = true;
		}
	}

	private void txtItems_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtItems);
		dataView.RowFilter = "SubAccountName Like '%" + ((Control)(object)txtItems).Text.Trim() + "%'  OR EmployeeNo Like '" + ((Control)(object)txtItems).Text.Trim() + "%' ";
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TreeItems.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			TreeItems.ActiveNode = TreeItems.GetNodeByKey(dataView.ToTable().Rows[0]["SubAccountID"].ToString());
		}
	}

	private void btnItemsSearch_Click(object sender, EventArgs e)
	{
	}

	private void dtpToDate_ValueChanged(object sender, EventArgs e)
	{
		if (dtpToDate.Value != null && dtpToDate.Value != DBNull.Value)
		{
			dtMonths = YearsMonths.FillComboByDate(dtpToDate.DateTime.ToString("MM/dd/yyyy 23:59:59"), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboMonth, dtMonths, "YearMonthID", "YearMonthName");
		}
		else
		{
			cboMonth.Items.Clear();
			((TextEditorControlBase)cboMonth).Clear();
		}
		fillData();
	}

	private void cboSalaryList_ValueChanged(object sender, EventArgs e)
	{
		fillData();
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
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Expected O, but got Unknown
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Expected O, but got Unknown
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HR.Payroll.Transactions.frmEmployeesSalary));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Override val4 = new Override();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.dtpToDate = new UltraDateTimeEditor();
		this.ultraLabel2 = new UltraLabel();
		this.btnItemsSearch = new UltraButton();
		this.txtItems = new UltraTextEditor();
		this.TreeItems = new UltraTree();
		this.chkAll = new UltraCheckEditor();
		this.btnPreview = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.cboMonth = new UltraComboEditor();
		this.lblMonth = new UltraLabel();
		this.cboSalaryList = new UltraComboEditor();
		this.lblSalaryList = new UltraLabel();
		this.chkIsArabic = new UltraCheckEditor();
		this.chkWithLogo = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboMonth).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalaryList).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsArabic).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithLogo).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.dtpToDate, "dtpToDate");
		((System.Windows.Forms.Control)(object)this.dtpToDate).Name = "dtpToDate";
		this.dtpToDate.PromptChar = ' ';
		this.dtpToDate.ValueChanged += new System.EventHandler(dtpToDate_ValueChanged);
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val, "appearance9");
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val;
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.btnItemsSearch, "btnItemsSearch");
		((AppearanceBase)val2).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val2, "appearance10");
		((ControlBase)this.btnItemsSearch).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Name = "btnItemsSearch";
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Click += new System.EventHandler(btnItemsSearch_Click);
		resources.ApplyResources(this.txtItems, "txtItems");
		((System.Windows.Forms.Control)(object)this.txtItems).Name = "txtItems";
		((TextEditorControlBase)this.txtItems).ValueChanged += new System.EventHandler(txtItems_ValueChanged);
		resources.ApplyResources(this.TreeItems, "TreeItems");
		((AppearanceBase)val3).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val3).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val3).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints");
		((AppearanceBase)val3).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val3).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val3).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val3, "appearance3");
		this.TreeItems.Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.TreeItems).Name = "TreeItems";
		val4.NodeStyle = (NodeStyle)1;
		this.TreeItems.Override = val4;
		((UltraControlBase)this.TreeItems).UseAppStyling = false;
		this.TreeItems.AfterCheck += new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
		resources.ApplyResources(this.chkAll, "chkAll");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val5, "appearance11");
		((UltraToggleEditorBase)this.chkAll).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.chkAll).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAll).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAll).Name = "chkAll";
		((UltraControlBase)this.chkAll).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAll).CheckedChanged += new System.EventHandler(chkAll_CheckedChanged);
		resources.ApplyResources(this.btnPreview, "btnPreview");
		((System.Windows.Forms.Control)(object)this.btnPreview).Name = "btnPreview";
		((System.Windows.Forms.Control)(object)this.btnPreview).Click += new System.EventHandler(btnPreview_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val6).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val6, "appearance12");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val7).Image = resources.GetObject("appearance13.Image");
		resources.ApplyResources(val7, "appearance13");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val7;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.cboMonth, "cboMonth");
		((TextEditorControlBase)this.cboMonth).AlwaysInEditMode = true;
		this.cboMonth.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboMonth).Name = "cboMonth";
		resources.ApplyResources(this.lblMonth, "lblMonth");
		this.lblMonth.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMonth).Name = "lblMonth";
		((ControlBase)this.lblMonth).WrapText = false;
		resources.ApplyResources(this.cboSalaryList, "cboSalaryList");
		this.cboSalaryList.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSalaryList).Name = "cboSalaryList";
		((TextEditorControlBase)this.cboSalaryList).Nullable = false;
		((TextEditorControlBase)this.cboSalaryList).ValueChanged += new System.EventHandler(cboSalaryList_ValueChanged);
		resources.ApplyResources(this.lblSalaryList, "lblSalaryList");
		this.lblSalaryList.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSalaryList).Name = "lblSalaryList";
		((ControlBase)this.lblSalaryList).WrapText = false;
		resources.ApplyResources(this.chkIsArabic, "chkIsArabic");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance14");
		((UltraToggleEditorBase)this.chkIsArabic).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.chkIsArabic).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkIsArabic).BackColorInternal = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkIsArabic).Checked = true;
		((UltraToggleEditorBase)this.chkIsArabic).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsArabic).Name = "chkIsArabic";
		((UltraControlBase)this.chkIsArabic).UseAppStyling = false;
		resources.ApplyResources(this.chkWithLogo, "chkWithLogo");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance15");
		((UltraToggleEditorBase)this.chkWithLogo).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.chkWithLogo).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkWithLogo).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkWithLogo).Name = "chkWithLogo";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsArabic);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkWithLogo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSalaryList);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSalaryList);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboMonth);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMonth);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnItemsSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtItems);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.TreeItems);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAll);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPreview);
		base.Name = "frmEmployeesSalary";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPreview, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAll, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.TreeItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnItemsSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMonth, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboMonth, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSalaryList, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSalaryList, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkWithLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsArabic, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboMonth).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalaryList).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsArabic).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithLogo).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
