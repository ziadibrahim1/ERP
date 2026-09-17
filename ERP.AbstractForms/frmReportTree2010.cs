using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.General;
using BusinessLayer.Privilege;
using CrystalDecisions.CrystalReports.Engine;
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using ERP.SystemOptions.GeneralOptions;
using ERP.Ticketing;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.AbstractForms;

public class frmReportTree2010 : frmBase
{
	public bool Online = false;

	public bool IsLoading = true;

	public DataTable dtBranches;

	public DataTable dtItems;

	public DataTable dtItems2;

	public DataTable dtReports = new DataTable();

	public DataTable dtFormSetting = new DataTable();

	public string Branches;

	public string BranchesNames;

	public string Items;

	public string Items2;

	public string TreeItemsParentIDCol;

	public string TreeItemsIDCol;

	public string TreeItemsNameCol;

	public string TreeItemsNumberCol;

	public string TreeItemsIsMainCol;

	public string TreeItems2ParentIDCol;

	public string TreeItems2IDCol;

	public string TreeItems2NameCol;

	public string TreeItems2NumberCol;

	public string TreeItems2IsMainCol;

	private IContainer components = null;

	public UltraButton btnPreview;

	public UltraLabel ultraLabel1;

	public UltraLabel ultraLabel2;

	public UltraDateTimeEditor dtpFromDate;

	public UltraDateTimeEditor dtpToDate;

	public UltraCheckEditor chkAllBranches;

	protected internal CheckedListBox clbBranches;

	public UltraCheckEditor chkWithLogo;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblReportType;

	public UltraComboEditor cboReportType;

	protected internal UltraCheckEditor chkAll;

	protected internal UltraCheckEditor chkAll2;

	public UltraTree TreeItems;

	public UltraTree TreeItems2;

	public UltraButton btnItemsSearch;

	public UltraButton btnItems2Search;

	protected internal UltraCheckEditor chkIsArabic;

	public UltraTextEditor txtItems;

	public UltraTextEditor txtItems2;

	public UltraButton btnKeyboard;

	public UltraButton btnNew;

	public UltraButton btnUpdate;

	public UltraButton btnDelete;

	public UltraLabel lblTitle2;

	public UltraComboEditor cboSetting;

	public UltraLabel lblSettingName;

	public UltraButton btnSaveSetting;

	public UltraButton btnOpenTicket;

	public frmReportTree2010()
	{
		InitializeComponent();
		base.CancelButton = (IButtonControl)btnClose;
		((UltraToggleEditorBase)chkIsArabic).Checked = GlobalVariables.IsArabic;
	}

	public virtual void GetItems()
	{
		GetBranches();
		Items = TreeFunctions.GetTreeCheckedNodesIDs(TreeItems);
		Items2 = TreeFunctions.GetTreeCheckedNodesIDs(TreeItems2);
	}

	public virtual void GetBranches()
	{
		Online = false;
		Branches = ",";
		BranchesNames = ",";
		for (int i = 0; i < clbBranches.Items.Count; i++)
		{
			if (clbBranches.GetItemChecked(i))
			{
				Branches = Branches + dtBranches.Rows[i]["BranchID"].ToString() + ",";
				BranchesNames = BranchesNames + dtBranches.Rows[i][((UltraToggleEditorBase)chkIsArabic).Checked ? "BranchNameAr" : "BranchNameEn"].ToString() + ",";
				if (clbBranches.Visible && dtBranches.Rows[i]["BranchID"].ToString() != GlobalVariables.CurrentBranchID && Main.IsSynchronization && !GlobalVariables.dtSyncConn.Rows[0]["SyncBranchID"].Equals(DBNull.Value))
				{
					Online = true;
				}
			}
		}
	}

	public virtual void FillData()
	{
	}

	public virtual void DisplaySetting(DataRow drSetting)
	{
		((UltraToggleEditorBase)chkIsArabic).Checked = !drSetting["bit1"].Equals(false);
		((UltraToggleEditorBase)chkWithLogo).Checked = drSetting["bit2"].Equals(true);
		string text = drSetting["Items5"].ToString();
		for (int i = 0; i < dtBranches.Rows.Count; i++)
		{
			clbBranches.SetItemChecked(i, text.IndexOf(string.Concat(",", dtBranches.Rows[i]["BranchID"], ",")) > -1);
		}
		if (drSetting["Items1"].Equals("-1"))
		{
			((UltraToggleEditorBase)chkAll).Checked = true;
		}
		else
		{
			TreeFunctions.SetAllTreeNodesCheckState(Checked: false, TreeItems);
			string[] array = drSetting["Items1"].ToString().Split(',');
			for (int j = 0; j < array.Length; j++)
			{
				if (TreeItems.GetNodeByKey(array[j]) != null)
				{
					TreeItems.GetNodeByKey(array[j]).CheckedState = CheckState.Checked;
				}
			}
		}
		if (drSetting["Items2"].Equals("-1"))
		{
			((UltraToggleEditorBase)chkAll2).Checked = true;
			return;
		}
		TreeFunctions.SetAllTreeNodesCheckState(Checked: false, TreeItems2);
		string[] array2 = drSetting["Items2"].ToString().Split(',');
		for (int k = 0; k < array2.Length; k++)
		{
			if (TreeItems2.GetNodeByKey(array2[k]) != null)
			{
				TreeItems2.GetNodeByKey(array2[k]).CheckedState = CheckState.Checked;
			}
		}
	}

	public virtual void SaveSetting()
	{
		frmFormSettingName frmFormSettingName2 = new frmFormSettingName((cboSetting.SelectedIndex > -1) ? ((TextEditorControlBase)cboSetting).Value.ToString() : "0", dtFormSetting, ((DataRow)base.Tag)["FormID"].ToString());
		frmFormSettingName2.ShowDialog();
		dtFormSetting = FormSetting.FillCombo(((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.UserID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSetting, dtFormSetting, "FormSettingID", "SettingName");
		GetItems();
		GetBranches();
		int settingID = frmFormSettingName2.SettingID;
		FormSetting.UpdateParameters(settingID.ToString(), Items, Items2, "Null", "Null", Branches, "Null", "Null", "Null", "Null", "Null", "Null", ((UltraToggleEditorBase)chkIsArabic).Checked ? "1" : "0", ((UltraToggleEditorBase)chkWithLogo).Checked ? "1" : "0", "Null", "Null", GlobalVariables.UserID, IsFromServer: false);
	}

	public virtual void ShowReport()
	{
	}

	public virtual void NewReport()
	{
		frmUserReportName frmUserReportName2 = new frmUserReportName();
		frmUserReportName2.WindowState = FormWindowState.Normal;
		frmUserReportName2.ShowDialog();
		if (!frmUserReportName2.Cancel)
		{
			int num = Reports.InsertNewUserDesign(((DataRow)base.Tag)["FormID"].ToString(), frmUserReportName2.ArName, frmUserReportName2.EnName, dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].ToString(), GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID, IsFromServer: true);
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

	public virtual void UpdateReport()
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

	public virtual void DeleteReport()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			UsersReports.DeleteByReportID(((TextEditorControlBase)cboReportType).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			GroupsReports.DeleteByReportID(((TextEditorControlBase)cboReportType).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			Reports.Delete(((TextEditorControlBase)cboReportType).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
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

	public virtual string GetReportName()
	{
		return "";
	}

	public virtual void FormLoad()
	{
		if (dtItems != null)
		{
			TreeFunctions.FillTree(TreeItems, dtItems, TreeItemsParentIDCol, TreeItemsIDCol, TreeItemsNameCol, TreeItemsNumberCol, TreeItemsIsMainCol);
		}
		if (dtItems2 != null)
		{
			TreeFunctions.FillTree(TreeItems2, dtItems2, TreeItems2ParentIDCol, TreeItems2IDCol, TreeItems2NameCol, TreeItems2NumberCol, TreeItems2IsMainCol);
		}
	}

	public void frmReport_Load(object sender, EventArgs e)
	{
		dtpFromDate.Value = DateTime.Now.Date;
		dtpToDate.Value = DateTime.Now.Date.AddHours(23.0).AddMinutes(59.0).AddSeconds(59.0);
		if (GlobalVariables.BranchIDs != "" || ViewAllBranches)
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
		FormLoad();
		if (!base.DesignMode)
		{
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
			dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			if (dtReports.Rows.Count > 0)
			{
				((TextEditorControlBase)cboReportType).Clear();
				GlobalFunctions.FillCombo(cboReportType, dtReports, "ReportID", "ReportName");
				cboReportType.SelectedIndex = 0;
			}
			dtFormSetting = FormSetting.FillCombo(((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.UserID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboSetting, dtFormSetting, "FormSettingID", "SettingName");
		}
		IsLoading = false;
		FillData();
	}

	public virtual void btnPreview_Click(object sender, EventArgs e)
	{
		try
		{
			GlobalVariables.ReportDocument = new ReportDocument();
			GlobalVariables.ReportDocument.Load((dtReports.Rows.Count == 0) ? GetReportName() : (GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep" + (((UltraToggleEditorBase)chkIsArabic).Checked ? "_A" : "_E") + (((UltraToggleEditorBase)chkWithLogo).Checked ? "" : "_nologo")].ToString()));
			GetBranches();
			GlobalVariables.IsRepOnlineConn = Online;
			ShowReport();
			GlobalVariables.ReportDocument = null;
		}
		catch (Exception ex)
		{
			if (ex.Message == "Load report failed.")
			{
				GlobalVariables.QuestionMB.Show("مسار التقارير غير سليم \r\n  هل تريد تغيير المسار الإفتراضي ؟?", "Invalid Reports Path  \r\n Do you like to Change Default Path ?");
				if (GlobalVariables.MessageBoxResult == 'Y')
				{
					frmReportDefultPath frmReportDefultPath2 = new frmReportDefultPath();
					frmReportDefultPath2.ShowDialog();
				}
			}
			else
			{
				GlobalVariables.InformationMB.Show(ex.Message);
			}
		}
	}

	public void btnClose_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	public virtual void dtpFromDate_ValueChanged(object sender, EventArgs e)
	{
		if (!IsLoading)
		{
			FillData();
		}
	}

	public virtual void dtpToDate_ValueChanged(object sender, EventArgs e)
	{
		if (!IsLoading)
		{
			FillData();
		}
	}

	public virtual void SelectAllListBoxItems(bool Checked, CheckedListBox lst)
	{
		for (int i = 0; i < lst.Items.Count; i++)
		{
			lst.SetItemChecked(i, Checked);
		}
	}

	private void clbBranches_SelectedValueChanged(object sender, EventArgs e)
	{
		((UltraToggleEditorBase)chkAllBranches).CheckedChanged -= chkAllBranches_CheckedChanged;
		((UltraToggleEditorBase)chkAllBranches).Checked = clbBranches.CheckedItems.Count == clbBranches.Items.Count && clbBranches.Items.Count > 0;
		((UltraToggleEditorBase)chkAllBranches).CheckedChanged += chkAllBranches_CheckedChanged;
		if (!IsLoading)
		{
			FillData();
		}
	}

	public virtual void Tree_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected O, but got Unknown
		TreeItems.AfterCheck -= new AfterNodeChangedEventHandler(Tree_AfterCheck);
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
		TreeItems.AfterCheck += new AfterNodeChangedEventHandler(Tree_AfterCheck);
	}

	public virtual void Tree2_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected O, but got Unknown
		TreeItems2.AfterCheck -= new AfterNodeChangedEventHandler(Tree2_AfterCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			SetParentState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAll2).CheckedChanged -= chkAll2_CheckedChanged;
		SetCheckBoxAllState(TreeItems2, chkAll2);
		((UltraToggleEditorBase)chkAll2).CheckedChanged += chkAll2_CheckedChanged;
		TreeItems2.AfterCheck += new AfterNodeChangedEventHandler(Tree2_AfterCheck);
	}

	public virtual void chkAllBranches_CheckedChanged(object sender, EventArgs e)
	{
		SelectAllListBoxItems(((UltraToggleEditorBase)chkAllBranches).Checked, clbBranches);
		if (!IsLoading)
		{
			FillData();
		}
	}

	public virtual void chkAll_CheckedChanged(object sender, EventArgs e)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		TreeItems.AfterCheck -= new AfterNodeChangedEventHandler(Tree_AfterCheck);
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAll).Checked, TreeItems);
		TreeItems.AfterCheck += new AfterNodeChangedEventHandler(Tree_AfterCheck);
	}

	public virtual void chkAll2_CheckedChanged(object sender, EventArgs e)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		TreeItems2.AfterCheck -= new AfterNodeChangedEventHandler(Tree2_AfterCheck);
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAll2).Checked, TreeItems2);
		TreeItems2.AfterCheck += new AfterNodeChangedEventHandler(Tree2_AfterCheck);
	}

	public virtual void SetCheckBoxAllState(UltraTree tree, UltraCheckEditor CheckBox)
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

	public virtual void btnItems2Search_Click(object sender, EventArgs e)
	{
	}

	public virtual void btnItemsSearch_Click(object sender, EventArgs e)
	{
	}

	public virtual void txtItems_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtItems);
		dataView.RowFilter = TreeItemsNameCol + " Like '%" + ((Control)(object)txtItems).Text.Trim() + "%' " + ((TreeItemsNumberCol != null && TreeItemsNumberCol != "") ? (" OR " + TreeItemsNumberCol + " Like '" + ((Control)(object)txtItems).Text.Trim() + "%' ") : "");
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TreeItems.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			TreeItems.ActiveNode = TreeItems.GetNodeByKey(dataView.ToTable().Rows[0][TreeItemsIDCol].ToString());
		}
	}

	public virtual void txtItems2_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtItems2);
		dataView.RowFilter = TreeItems2NameCol + " Like '%" + ((Control)(object)txtItems2).Text.Trim() + "%' " + ((TreeItems2NumberCol != null && TreeItems2NumberCol != "") ? (" OR " + TreeItems2NumberCol + " Like '" + ((Control)(object)txtItems2).Text.Trim() + "%' ") : "");
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TreeItems2.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			TreeItems2.ActiveNode = TreeItems2.GetNodeByKey(dataView.ToTable().Rows[0][TreeItems2IDCol].ToString());
		}
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
	}

	private void btnNew_Click(object sender, EventArgs e)
	{
		NewReport();
	}

	private void btnUpdate_Click(object sender, EventArgs e)
	{
		UpdateReport();
	}

	private void btnDelete_Click(object sender, EventArgs e)
	{
		DeleteReport();
	}

	private void cboReportType_ValueChanged(object sender, EventArgs e)
	{
		if (dtReports != null && dtReports.Rows.Count > 0 && cboReportType.SelectedIndex > -1)
		{
			((Control)(object)btnUpdate).Enabled = GlobalVariables.UserID == "1" || !Convert.ToBoolean(dtReports.Rows[cboReportType.SelectedIndex]["ReadOnly"]);
			((Control)(object)btnDelete).Enabled = !Convert.ToBoolean(dtReports.Rows[cboReportType.SelectedIndex]["ReadOnly"]);
		}
	}

	private void cboSetting_ValueChanged(object sender, EventArgs e)
	{
		if (cboSetting.SelectedIndex > -1)
		{
			DataRow drSetting = FormSetting.Select(((TextEditorControlBase)cboSetting).Value.ToString(), "-1", "0", IsFromServer: false).Rows[0];
			DisplaySetting(drSetting);
		}
	}

	private void btnSaveSetting_Click(object sender, EventArgs e)
	{
		SaveSetting();
	}

	private void btnOpenTicket_Click(object sender, EventArgs e)
	{
		try
		{
			Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
			frmSupportingTickets frmSupportingTickets2 = new frmSupportingTickets(isError: false, isMessage: false, isFormQst: true, base.Name, "Question on form : " + base.Name, bitmap);
			frmSupportingTickets2.StartPosition = FormStartPosition.CenterScreen;
			((Control)(object)frmSupportingTickets2.lblTitle).Text = (GlobalVariables.IsArabic ? "طلب دعم" : "Supporting Tickets");
			frmSupportingTickets2.Tag = GlobalVariables.dtAllForms.Select("Form = 'ERP.Ticketing.frmSupportingTickets'")[0];
			frmSupportingTickets2.ShowDialog();
		}
		catch
		{
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
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
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
		//IL_0a73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7d: Expected O, but got Unknown
		//IL_0b79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b83: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.AbstractForms.frmReportTree2010));
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
		Override val11 = new Override();
		Appearance val12 = new Appearance();
		Override val13 = new Override();
		Appearance val14 = new Appearance();
		Appearance val15 = new Appearance();
		Appearance val16 = new Appearance();
		Appearance val17 = new Appearance();
		Appearance val18 = new Appearance();
		Appearance val19 = new Appearance();
		this.ultraLabel1 = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.clbBranches = new System.Windows.Forms.CheckedListBox();
		this.dtpFromDate = new UltraDateTimeEditor();
		this.dtpToDate = new UltraDateTimeEditor();
		this.chkAllBranches = new UltraCheckEditor();
		this.btnPreview = new UltraButton();
		this.chkWithLogo = new UltraCheckEditor();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblReportType = new UltraLabel();
		this.cboReportType = new UltraComboEditor();
		this.chkAll = new UltraCheckEditor();
		this.chkAll2 = new UltraCheckEditor();
		this.TreeItems = new UltraTree();
		this.TreeItems2 = new UltraTree();
		this.txtItems = new UltraTextEditor();
		this.txtItems2 = new UltraTextEditor();
		this.btnItemsSearch = new UltraButton();
		this.btnItems2Search = new UltraButton();
		this.chkIsArabic = new UltraCheckEditor();
		this.btnKeyboard = new UltraButton();
		this.btnNew = new UltraButton();
		this.btnUpdate = new UltraButton();
		this.btnDelete = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.cboSetting = new UltraComboEditor();
		this.lblSettingName = new UltraLabel();
		this.btnSaveSetting = new UltraButton();
		this.btnOpenTicket = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithLogo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboReportType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItems2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtItems2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsArabic).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSetting).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val2;
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.clbBranches, "clbBranches");
		this.clbBranches.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.clbBranches.CheckOnClick = true;
		this.clbBranches.MultiColumn = true;
		this.clbBranches.Name = "clbBranches";
		this.clbBranches.SelectedValueChanged += new System.EventHandler(clbBranches_SelectedValueChanged);
		resources.ApplyResources(this.dtpFromDate, "dtpFromDate");
		this.dtpFromDate.DateTime = new System.DateTime(2013, 7, 16, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpFromDate).Name = "dtpFromDate";
		this.dtpFromDate.PromptChar = ' ';
		this.dtpFromDate.Value = new System.DateTime(2013, 7, 16, 0, 0, 0, 0);
		this.dtpFromDate.ValueChanged += new System.EventHandler(dtpFromDate_ValueChanged);
		resources.ApplyResources(this.dtpToDate, "dtpToDate");
		this.dtpToDate.DateTime = new System.DateTime(2013, 7, 16, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpToDate).Name = "dtpToDate";
		this.dtpToDate.PromptChar = ' ';
		this.dtpToDate.Value = new System.DateTime(2013, 7, 16, 0, 0, 0, 0);
		this.dtpToDate.ValueChanged += new System.EventHandler(dtpToDate_ValueChanged);
		resources.ApplyResources(this.chkAllBranches, "chkAllBranches");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val3).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val3, "appearance3");
		((UltraToggleEditorBase)this.chkAllBranches).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.chkAllBranches).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllBranches).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllBranches).Name = "chkAllBranches";
		((UltraToggleEditorBase)this.chkAllBranches).CheckedChanged += new System.EventHandler(chkAllBranches_CheckedChanged);
		resources.ApplyResources(this.btnPreview, "btnPreview");
		((System.Windows.Forms.Control)(object)this.btnPreview).Name = "btnPreview";
		((System.Windows.Forms.Control)(object)this.btnPreview).Click += new System.EventHandler(btnPreview_Click);
		resources.ApplyResources(this.chkWithLogo, "chkWithLogo");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val4, "appearance4");
		((UltraToggleEditorBase)this.chkWithLogo).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.chkWithLogo).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkWithLogo).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkWithLogo).Name = "chkWithLogo";
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)7;
		((AppearanceBase)val5).Image = resources.GetObject("appearance5.Image");
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val6).Image = resources.GetObject("appearance6.Image");
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val6;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblReportType, "lblReportType");
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.lblReportType).Appearance = (AppearanceBase)(object)val7;
		this.lblReportType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReportType).Name = "lblReportType";
		((ControlBase)this.lblReportType).WrapText = false;
		resources.ApplyResources(this.cboReportType, "cboReportType");
		this.cboReportType.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboReportType).Name = "cboReportType";
		((TextEditorControlBase)this.cboReportType).Nullable = false;
		((TextEditorControlBase)this.cboReportType).ValueChanged += new System.EventHandler(cboReportType_ValueChanged);
		resources.ApplyResources(this.chkAll, "chkAll");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((UltraToggleEditorBase)this.chkAll).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.chkAll).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAll).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAll).Name = "chkAll";
		((UltraControlBase)this.chkAll).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAll).CheckedChanged += new System.EventHandler(chkAll_CheckedChanged);
		resources.ApplyResources(this.chkAll2, "chkAll2");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance9");
		((UltraToggleEditorBase)this.chkAll2).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.chkAll2).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAll2).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAll2).Name = "chkAll2";
		((UltraControlBase)this.chkAll2).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAll2).CheckedChanged += new System.EventHandler(chkAll2_CheckedChanged);
		resources.ApplyResources(this.TreeItems, "TreeItems");
		((AppearanceBase)val10).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val10).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val10).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints");
		((AppearanceBase)val10).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val10).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance10");
		this.TreeItems.Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.TreeItems).Name = "TreeItems";
		val11.NodeStyle = (NodeStyle)1;
		this.TreeItems.Override = val11;
		((UltraControlBase)this.TreeItems).UseAppStyling = false;
		this.TreeItems.AfterCheck += new AfterNodeChangedEventHandler(Tree_AfterCheck);
		resources.ApplyResources(this.TreeItems2, "TreeItems2");
		((AppearanceBase)val12).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val12).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val12).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints1");
		((AppearanceBase)val12).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val12).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance11");
		this.TreeItems2.Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.TreeItems2).Name = "TreeItems2";
		val13.NodeStyle = (NodeStyle)1;
		this.TreeItems2.Override = val13;
		((UltraControlBase)this.TreeItems2).UseAppStyling = false;
		this.TreeItems2.AfterCheck += new AfterNodeChangedEventHandler(Tree2_AfterCheck);
		resources.ApplyResources(this.txtItems, "txtItems");
		((System.Windows.Forms.Control)(object)this.txtItems).Name = "txtItems";
		((TextEditorControlBase)this.txtItems).ValueChanged += new System.EventHandler(txtItems_ValueChanged);
		resources.ApplyResources(this.txtItems2, "txtItems2");
		((System.Windows.Forms.Control)(object)this.txtItems2).Name = "txtItems2";
		((TextEditorControlBase)this.txtItems2).ValueChanged += new System.EventHandler(txtItems2_ValueChanged);
		resources.ApplyResources(this.btnItemsSearch, "btnItemsSearch");
		((AppearanceBase)val14).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val14, "appearance12");
		((ControlBase)this.btnItemsSearch).Appearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Name = "btnItemsSearch";
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Click += new System.EventHandler(btnItemsSearch_Click);
		resources.ApplyResources(this.btnItems2Search, "btnItems2Search");
		((AppearanceBase)val15).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val15, "appearance13");
		((ControlBase)this.btnItems2Search).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.btnItems2Search).Name = "btnItems2Search";
		((System.Windows.Forms.Control)(object)this.btnItems2Search).Click += new System.EventHandler(btnItems2Search_Click);
		resources.ApplyResources(this.chkIsArabic, "chkIsArabic");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val16, "appearance14");
		((UltraToggleEditorBase)this.chkIsArabic).Appearance = (AppearanceBase)(object)val16;
		((System.Windows.Forms.Control)(object)this.chkIsArabic).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkIsArabic).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkIsArabic).Name = "chkIsArabic";
		((UltraControlBase)this.chkIsArabic).UseAppStyling = false;
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val17).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val17, "appearance15");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val17;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.btnNew, "btnNew");
		((System.Windows.Forms.Control)(object)this.btnNew).Name = "btnNew";
		((System.Windows.Forms.Control)(object)this.btnNew).Click += new System.EventHandler(btnNew_Click);
		resources.ApplyResources(this.btnUpdate, "btnUpdate");
		((System.Windows.Forms.Control)(object)this.btnUpdate).Name = "btnUpdate";
		((System.Windows.Forms.Control)(object)this.btnUpdate).Click += new System.EventHandler(btnUpdate_Click);
		resources.ApplyResources(this.btnDelete, "btnDelete");
		((System.Windows.Forms.Control)(object)this.btnDelete).Name = "btnDelete";
		((System.Windows.Forms.Control)(object)this.btnDelete).Click += new System.EventHandler(btnDelete_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val18).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val18).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val18, "appearance16");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val18;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.cboSetting, "cboSetting");
		this.cboSetting.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSetting).Name = "cboSetting";
		((TextEditorControlBase)this.cboSetting).Nullable = false;
		((TextEditorControlBase)this.cboSetting).ValueChanged += new System.EventHandler(cboSetting_ValueChanged);
		resources.ApplyResources(this.lblSettingName, "lblSettingName");
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val19, "appearance17");
		((ControlBase)this.lblSettingName).Appearance = (AppearanceBase)(object)val19;
		this.lblSettingName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSettingName).Name = "lblSettingName";
		((ControlBase)this.lblSettingName).WrapText = false;
		resources.ApplyResources(this.btnSaveSetting, "btnSaveSetting");
		((System.Windows.Forms.Control)(object)this.btnSaveSetting).Name = "btnSaveSetting";
		((System.Windows.Forms.Control)(object)this.btnSaveSetting).Click += new System.EventHandler(btnSaveSetting_Click);
		resources.ApplyResources(this.btnOpenTicket, "btnOpenTicket");
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Name = "btnOpenTicket";
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Click += new System.EventHandler(btnOpenTicket_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOpenTicket);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSetting);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSettingName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnUpdate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDelete);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNew);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsArabic);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnItems2Search);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnItemsSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtItems2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtItems);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.TreeItems2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.TreeItems);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAll2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAll);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboReportType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReportType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSaveSetting);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPreview);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkWithLogo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFromDate);
		base.Controls.Add(this.clbBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmReportTree2010";
		base.Load += new System.EventHandler(frmReport_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex(this.clbBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkWithLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPreview, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSaveSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAll, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAll2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.TreeItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.TreeItems2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtItems2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnItemsSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnItems2Search, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsArabic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNew, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSettingName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOpenTicket, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithLogo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboReportType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItems2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtItems2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsArabic).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSetting).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
