using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using BusinessLayer.HR;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.HR.Attendance.Transactions;

public class frmEmployeesAttendance : frmBase
{
	private DataTable dtItems;

	private DataTable dtEmpAtt;

	private DataTable dtEmployees;

	private IContainer components = null;

	public UltraDateTimeEditor dtpToDate;

	public UltraDateTimeEditor dtpFromDate;

	public UltraLabel ultraLabel2;

	public UltraLabel ultraLabel1;

	public UltraButton btnItemsSearch;

	public UltraTextEditor txtItems;

	public UltraTree TreeItems;

	protected internal UltraCheckEditor chkAll;

	public UltraButton btnPreview;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public frmEmployeesAttendance()
	{
		InitializeComponent();
		dtItems = SubAccounts.FillReportTreeEmployeesBySubAccountTypeIDs(GlobalVariables.EmployeeSubAccountTypeIDs, "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dtItems != null)
		{
			TreeFunctions.FillTree(TreeItems, dtItems, "ParentID", "SubAccountID", "SubAccountName", "SubAccountNumber", "IsMain");
		}
		dtEmployees = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, "-1", "1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
	}

	private void btnPreview_Click(object sender, EventArgs e)
	{
		string treeCheckedNodesIDs = TreeFunctions.GetTreeCheckedNodesIDs(TreeItems);
		if (treeCheckedNodesIDs.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى موظف  ", "There is no chosen Employee, please check items ");
			return;
		}
		dtEmpAtt = EmployeesAttendance.SelectByEmployeeIDsPeriods(treeCheckedNodesIDs, dtpFromDate.DateTime.ToString(GlobalVariables.DateShortFormate), dtpToDate.DateTime.ToString("MM/dd/yyyy 23:59:59"), GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		frmEmployeesAttendanceDetails frmEmployeesAttendanceDetails2 = new frmEmployeesAttendanceDetails(dtEmpAtt, dtEmployees);
		frmEmployeesAttendanceDetails2.EmpIDs = treeCheckedNodesIDs;
		frmEmployeesAttendanceDetails2.FromDate = dtpFromDate.DateTime;
		frmEmployeesAttendanceDetails2.ToDate = dtpToDate.DateTime;
		frmEmployeesAttendanceDetails2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Width);
		frmEmployeesAttendanceDetails2.Location = new Point(0, 0);
		((Control)(object)frmEmployeesAttendanceDetails2.lblTitle).Text = ((Control)(object)lblTitle).Text;
		frmEmployeesAttendanceDetails2.CanUpdate = CanUpdate;
		frmEmployeesAttendanceDetails2.ShowDialog();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	private void TreeItems_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		TreeItems.AfterCheck -= new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			SetParentState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAll).CheckedChanged -= chkAll_CheckedChanged;
		SetCheckBoxAllState(TreeItems, chkAll);
		((UltraToggleEditorBase)chkAll).CheckedChanged += chkAll_CheckedChanged;
		TreeItems.AfterCheck += new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
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

	public virtual void SetParentState(UltraTreeNode Node)
	{
		Node.CheckedState = CheckParentState(Node);
		if (Node.Parent != null)
		{
			SetParentState(Node.Parent);
		}
	}

	public virtual CheckState CheckParentState(UltraTreeNode Node)
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < ((DisposableObjectCollectionBase)Node.Nodes).Count; i++)
		{
			if (Node.Nodes[i].CheckedState == CheckState.Checked)
			{
				num++;
			}
			else if (Node.Nodes[i].CheckedState == CheckState.Unchecked)
			{
				num2++;
			}
		}
		if (num == ((DisposableObjectCollectionBase)Node.Nodes).Count)
		{
			return CheckState.Checked;
		}
		if (num2 == ((DisposableObjectCollectionBase)Node.Nodes).Count)
		{
			return CheckState.Unchecked;
		}
		return CheckState.Indeterminate;
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
		dataView.RowFilter = "SubAccountName Like '%" + ((Control)(object)txtItems).Text.Trim() + "%'  OR SubAccountNumber Like '" + ((Control)(object)txtItems).Text.Trim() + "%' ";
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TreeItems.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			TreeItems.ActiveNode = TreeItems.GetNodeByKey(dataView.ToTable().Rows[0]["SubAccountID"].ToString());
		}
	}

	private void btnItemsSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.EmployeesReport("1", "-1", IsFromServer: true);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems.GetNodeByKey(dtSearchResult.Rows[i]["SubAccountID"].ToString()).CheckedState = CheckState.Checked;
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
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Expected O, but got Unknown
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HR.Attendance.Transactions.frmEmployeesAttendance));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Override val5 = new Override();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		this.dtpToDate = new UltraDateTimeEditor();
		this.dtpFromDate = new UltraDateTimeEditor();
		this.ultraLabel2 = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.btnItemsSearch = new UltraButton();
		this.txtItems = new UltraTextEditor();
		this.TreeItems = new UltraTree();
		this.chkAll = new UltraCheckEditor();
		this.btnPreview = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.dtpToDate, "dtpToDate");
		((System.Windows.Forms.Control)(object)this.dtpToDate).Name = "dtpToDate";
		this.dtpToDate.PromptChar = ' ';
		resources.ApplyResources(this.dtpFromDate, "dtpFromDate");
		((System.Windows.Forms.Control)(object)this.dtpFromDate).Name = "dtpFromDate";
		this.dtpFromDate.PromptChar = ' ';
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val;
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val2;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.btnItemsSearch, "btnItemsSearch");
		((AppearanceBase)val3).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnItemsSearch).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Name = "btnItemsSearch";
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Click += new System.EventHandler(btnItemsSearch_Click);
		resources.ApplyResources(this.txtItems, "txtItems");
		((System.Windows.Forms.Control)(object)this.txtItems).Name = "txtItems";
		((TextEditorControlBase)this.txtItems).ValueChanged += new System.EventHandler(txtItems_ValueChanged);
		resources.ApplyResources(this.TreeItems, "TreeItems");
		((AppearanceBase)val4).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val4).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints");
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Navy;
		this.TreeItems.Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.TreeItems).Name = "TreeItems";
		val5.NodeStyle = (NodeStyle)1;
		this.TreeItems.Override = val5;
		((UltraControlBase)this.TreeItems).UseAppStyling = false;
		this.TreeItems.AfterCheck += new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		((UltraToggleEditorBase)this.chkAll).Appearance = (AppearanceBase)(object)val6;
		resources.ApplyResources(this.chkAll, "chkAll");
		((System.Windows.Forms.Control)(object)this.chkAll).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAll).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAll).Name = "chkAll";
		((UltraControlBase)this.chkAll).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAll).CheckedChanged += new System.EventHandler(chkAll_CheckedChanged);
		resources.ApplyResources(this.btnPreview, "btnPreview");
		((System.Windows.Forms.Control)(object)this.btnPreview).Name = "btnPreview";
		((System.Windows.Forms.Control)(object)this.btnPreview).Click += new System.EventHandler(btnPreview_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val7).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val7, "appearance6");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val8).Image = resources.GetObject("appearance7.Image");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val8;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnItemsSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtItems);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.TreeItems);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAll);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPreview);
		base.Name = "frmEmployeesAttendance";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPreview, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAll, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.TreeItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnItemsSearch, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
