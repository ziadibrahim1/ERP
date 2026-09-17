using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using BusinessLayer.CustomsClearence;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.CustomsClearence.Reports;

public class frmOperationsExpenses : frmReportTree2010
{
	public DataTable dtEmployees;

	public string Employees;

	public string TreeEmployeesParentIDCol = "ParentID";

	public string TreeEmployeesIDCol = "SubAccountID";

	public string TreeEmployeesNameCol = "Name";

	public string TreeEmployeesIsMainCol = "IsMain";

	private string CustodyAccountID = "";

	private IContainer components = null;

	public UltraButton btnportsSearch;

	public UltraTextEditor txtEmployees;

	public UltraTree TreeEmployees;

	protected internal UltraCheckEditor chkAllEmployees;

	public frmOperationsExpenses()
	{
		dtItems = SubAccounts.FillReportTreeBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItemsParentIDCol = "ParentID";
		TreeItemsIDCol = "SubAccountID";
		TreeItemsNameCol = "SubAccountName";
		TreeItemsNumberCol = "ClientSupplierNo";
		TreeItemsIsMainCol = "IsMain";
		dtItems2 = Expenses.FillTree(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItems2ParentIDCol = "ParentID";
		TreeItems2IDCol = "ExpenseID";
		TreeItems2NameCol = "Name";
		TreeItems2IsMainCol = "IsMain";
		InitializeComponent();
	}

	public override void FormLoad()
	{
		CustodyAccountID = GlobalVariables.dtSystemAccounts.Select(" AccountNameEn ='CustodyAccount' ")[0]["AccountID"].ToString();
		if (CustodyAccountID != "")
		{
			dtEmployees = SubAccounts.SelectByAccountID(CustodyAccountID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		}
		if (dtEmployees != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeEmployees, dtEmployees, TreeEmployeesIDCol, TreeEmployeesNameCol);
		}
		if (dtItems2 != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeItems2, dtItems2, TreeItems2IDCol, TreeItems2NameCol);
		}
		if (dtItems != null)
		{
			TreeFunctions.FillTree(TreeItems, dtItems, TreeItemsParentIDCol, TreeItemsIDCol, TreeItemsNameCol, TreeItemsNumberCol, TreeItemsIsMainCol);
		}
		((UltraToggleEditorBase)chkAll).Checked = true;
		((UltraToggleEditorBase)chkAll2).Checked = true;
		((UltraToggleEditorBase)chkAllEmployees).Checked = true;
	}

	public override void ShowReport()
	{
		GetItems();
		if (((UltraToggleEditorBase)chkAllEmployees).Checked)
		{
			Employees = "-1";
		}
		else
		{
			Employees = TreeFunctions.GetTreeCheckedNodesIDs(TreeEmployees);
		}
		if (Items.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى عميل ليتم عرضه ", "There is no chosen Client to be shown in the report, please check Clients to be shown in report");
			return;
		}
		if (Items2.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى مصروف  ", "There is no chosen Expense to be shown in the report, please check Expenses to be shown in report");
			return;
		}
		if (Employees.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى موظف ", "There is no chosen Employee to be shown in the report, please check Employees to be shown in report");
			return;
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@ExpenseIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@SubAccountIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@EmployeeSubAccountIDs", Employees);
		GlobalVariables.ReportDocument.SetParameterValue("@FromDate", dtpFromDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime);
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

	public override void btnItems2Search_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.CST_ExpensesSearchReport(IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems2.GetNodeByKey(dtSearchResult.Rows[i][TreeItems2IDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	public override void btnItemsSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ClientsReport("-1", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems.GetNodeByKey(dtSearchResult.Rows[i][TreeItemsIDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void txtEmployee_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtEmployees);
		dataView.RowFilter = TreeEmployeesNameCol + " Like '%" + ((Control)(object)txtEmployees).Text.Trim() + "%' " + ((TreeEmployeesNameCol != null && TreeEmployeesNameCol != "") ? (" OR " + TreeEmployeesNameCol + " Like '" + ((Control)(object)txtEmployees).Text.Trim() + "%' ") : "");
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TreeEmployees.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			TreeEmployees.ActiveNode = TreeEmployees.GetNodeByKey(dataView.ToTable().Rows[0][TreeEmployeesIDCol].ToString());
		}
	}

	private void chkAllEmployee_CheckedChanged(object sender, EventArgs e)
	{
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAllEmployees).Checked, TreeEmployees);
	}

	private void btnEmployeeSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.SubAccountsReport(CustodyAccountID, IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeEmployees.GetNodeByKey(dtSearchResult.Rows[i][TreeEmployeesIDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void TreeEmployee_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		TreeEmployees.AfterCheck -= new AfterNodeChangedEventHandler(TreeEmployee_AfterCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			SetParentState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAllEmployees).CheckedChanged -= chkAllEmployee_CheckedChanged;
		SetCheckBoxAllState(TreeEmployees, chkAllEmployees);
		((UltraToggleEditorBase)chkAllEmployees).CheckedChanged += chkAllEmployee_CheckedChanged;
		TreeEmployees.AfterCheck += new AfterNodeChangedEventHandler(TreeEmployee_AfterCheck);
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
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
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Expected O, but got Unknown
		Appearance val = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.CustomsClearence.Reports.frmOperationsExpenses));
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Override val8 = new Override();
		Appearance val9 = new Appearance();
		this.btnportsSearch = new UltraButton();
		this.txtEmployees = new UltraTextEditor();
		this.TreeEmployees = new UltraTree();
		this.chkAllEmployees = new UltraCheckEditor();
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
		((System.ComponentModel.ISupportInitialize)this.txtEmployees).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeEmployees).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllEmployees).BeginInit();
		base.SuspendLayout();
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		((AppearanceBase)val).FontData.Name = resources.GetString("resource.Name");
		resources.ApplyResources(val, "appearance1");
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val;
		base.dtpFromDate.DateTime = new System.DateTime(2019, 1, 28, 0, 0, 0, 0);
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2019, 1, 28, 0, 0, 0, 0);
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name1");
		resources.ApplyResources(val2, "appearance2");
		base.dtpToDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpToDate.DateTime = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		base.dtpToDate.MaskInput = "dd/mm/yyyy";
		base.dtpToDate.Value = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		resources.ApplyResources(base.chkAllBranches, "chkAllBranches");
		((UltraControlBase)base.chkAllBranches).UseAppStyling = false;
		((UltraControlBase)base.chkWithLogo).UseAppStyling = false;
		((UltraControlBase)base.lblReportType).UseAppStyling = false;
		((AppearanceBase)val3).FontData.Name = resources.GetString("resource.Name2");
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
		((AppearanceBase)val4).FontData.Name = resources.GetString("resource.Name3");
		resources.ApplyResources(val4, "appearance4");
		((TextEditorControlBase)base.txtItems).Appearance = (AppearanceBase)(object)val4;
		resources.ApplyResources(base.txtItems, "txtItems");
		((AppearanceBase)val5).FontData.Name = resources.GetString("resource.Name4");
		resources.ApplyResources(val5, "appearance5");
		((TextEditorControlBase)base.txtItems2).Appearance = (AppearanceBase)(object)val5;
		resources.ApplyResources(base.txtItems2, "txtItems2");
		resources.ApplyResources(this.btnportsSearch, "btnportsSearch");
		((AppearanceBase)val6).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnportsSearch).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.btnportsSearch).Name = "btnportsSearch";
		((System.Windows.Forms.Control)(object)this.btnportsSearch).Click += new System.EventHandler(btnEmployeeSearch_Click);
		resources.ApplyResources(this.txtEmployees, "txtEmployees");
		((System.Windows.Forms.Control)(object)this.txtEmployees).Name = "txtEmployees";
		((TextEditorControlBase)this.txtEmployees).ValueChanged += new System.EventHandler(txtEmployee_ValueChanged);
		resources.ApplyResources(this.TreeEmployees, "TreeEmployees");
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		this.TreeEmployees.Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.TreeEmployees).Name = "TreeEmployees";
		val8.NodeStyle = (NodeStyle)1;
		this.TreeEmployees.Override = val8;
		((UltraControlBase)this.TreeEmployees).UseAppStyling = false;
		this.TreeEmployees.AfterCheck += new AfterNodeChangedEventHandler(TreeEmployee_AfterCheck);
		resources.ApplyResources(this.chkAllEmployees, "chkAllEmployees");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		((UltraToggleEditorBase)this.chkAllEmployees).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.chkAllEmployees).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllEmployees).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllEmployees).Name = "chkAllEmployees";
		((UltraControlBase)this.chkAllEmployees).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAllEmployees).CheckedChanged += new System.EventHandler(chkAllEmployee_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnportsSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEmployees);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.TreeEmployees);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllEmployees);
		base.Name = "frmOperationsExpenses";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllEmployees, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.TreeEmployees, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEmployees, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnportsSearch, 0);
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
		((System.ComponentModel.ISupportInitialize)this.txtEmployees).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeEmployees).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllEmployees).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
