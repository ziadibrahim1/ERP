using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.General;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.Privilege.Transactions;

public class frmGroupsPrivileges : frmBase
{
	private string SelectedformID = "0";

	private int GroupLevel;

	private DataTable dtGroups;

	private DataTable dtBranches;

	private DataTable dtForms;

	private DataTable dtFunctions;

	private DataTable dtFormFunctions;

	private DataView dvFormFunctions;

	private DataTable dtFormReports;

	private DataView dvFormReports;

	private DataTable dtGroupForms;

	private DataTable dtGroupFormFunctions;

	private DataTable dtGroupReports;

	private DataTable dtGroupFunctions;

	private DataTable dtGroupBranches;

	private IContainer components = null;

	private UltraComboEditor cboGroups;

	private UltraLabel lblGroup;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	protected internal UltraButton btnUpdate;

	protected internal UltraButton btnClose;

	private CheckedListBox clbFormFunctions;

	private CheckedListBox clbGroupFunctions;

	public UltraCheckEditor chkAllBranches;

	protected internal CheckedListBox clbBranches;

	private UltraLabel ultraLabel3;

	public UltraLabel lblTitle;

	private CheckedListBox clbReports;

	private UltraLabel lblReports;

	public UltraTree TreeItems;

	protected internal UltraCheckEditor chkAllForms;

	protected internal UltraButton btnResetUser;

	private NumericUpDown txtDatePeriod;

	private UltraLabel lblDatePeriod2;

	private UltraLabel lblDatePeriod;

	public frmGroupsPrivileges()
	{
		InitializeComponent();
		if (GlobalVariables.UserID == "1")
		{
			GroupLevel = -1;
		}
		else
		{
			GroupLevel = Convert.ToInt32(Groups.Select(GlobalVariables.GroupID, "-1", "1", IsFromServer: true).Rows[0]["GroupLevel"]);
		}
	}

	private void frmGroupsPrivileges_Load(object sender, EventArgs e)
	{
		FillData();
	}

	private void FillData()
	{
		dtFunctions = Functions.SelectByUserID(GlobalVariables.UserID, "0", IsFromServer: true);
		clbGroupFunctions.DataSource = dtFunctions;
		clbGroupFunctions.DisplayMember = (GlobalVariables.IsArabic ? "FunctionNameAr" : "FunctionNameEn");
		dtForms = Forms.SelectByUserID(GlobalVariables.UserID, IsFromServer: true);
		dtFormFunctions = FormsFunctions.SelectByUserID("-1", GlobalVariables.UserID, IsFromServer: true);
		dvFormFunctions = new DataView(dtFormFunctions);
		dtFormReports = BusinessLayer.Privilege.Reports.SelectByUserID(GlobalVariables.UserID, IsFromServer: true);
		dvFormReports = new DataView(dtFormReports);
		DrowTree();
		dtGroups = Groups.SelectByLevel(GroupLevel.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboGroups, dtGroups, "GroupID", GlobalVariables.IsArabic ? "GroupNameAr" : "GroupNameEn");
		dtBranches = Branches.Select("-1", GlobalVariables.BranchIDs, "1", IsFromServer: true);
		clbBranches.DataSource = dtBranches;
		clbBranches.DisplayMember = (GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn");
	}

	private void Tree_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Expected O, but got Unknown
		TreeItems.AfterCheck -= new AfterNodeChangedEventHandler(Tree_AfterCheck);
		UpdateNode(e.TreeNode);
		e.TreeNode.Selected = true;
		SetFormDetailsChecked(((KeyedSubObjectBase)e.TreeNode).Key);
		if (((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count > 0)
		{
			SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			SetParentState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAllForms).CheckedChanged -= chkAllForms_CheckedChanged;
		TreeFunctions.SetCheckBoxAllState(TreeItems, chkAllForms);
		((UltraToggleEditorBase)chkAllForms).CheckedChanged += chkAllForms_CheckedChanged;
		TreeItems.AfterCheck += new AfterNodeChangedEventHandler(Tree_AfterCheck);
	}

	private void TreeItems_AfterSelect(object sender, SelectEventArgs e)
	{
		UltraTreeNode nodeByKey = TreeItems.GetNodeByKey(SelectedformID);
		if (nodeByKey != null)
		{
			DataRow[] array = dtGroupForms.Select("FormID=" + ((KeyedSubObjectBase)nodeByKey).Key);
			if (array.Length != 0)
			{
				array[0]["GroupFormID"] = -1;
				array[0]["PeriodDays"] = txtDatePeriod.Value;
			}
			((SubObjectBase)nodeByKey).Tag = txtDatePeriod.Value;
		}
		SelectedformID = "0";
		if (((DisposableObjectCollectionBase)e.NewSelections).Count != 0)
		{
			SelectedformID = ((KeyedSubObjectBase)e.NewSelections[0]).Key;
			txtDatePeriod.Value = Convert.ToInt32(((SubObjectBase)e.NewSelections[0]).Tag);
		}
		SetFormDetailsChecked(SelectedformID);
	}

	private void SetFormDetailsChecked(string formID)
	{
		dvFormFunctions.RowFilter = "FormID=" + formID;
		DataTable dataTable = (DataTable)(clbFormFunctions.DataSource = dvFormFunctions.ToTable());
		clbFormFunctions.DisplayMember = (GlobalVariables.IsArabic ? "FunctionNameAr" : "FunctionNameEn");
		NumericUpDown numericUpDown = txtDatePeriod;
		UltraLabel obj = lblDatePeriod;
		bool flag = (((Control)(object)lblDatePeriod2).Visible = dataTable.Select("FunctionID=39").Length != 0);
		bool visible = (((Control)(object)obj).Visible = flag);
		numericUpDown.Visible = visible;
		dvFormReports.RowFilter = "FormID=" + formID;
		DataTable dataTable3 = (DataTable)(clbReports.DataSource = dvFormReports.ToTable());
		clbReports.DisplayMember = (GlobalVariables.IsArabic ? "ReportNameAr" : "ReportNameEn");
		if (cboGroups.SelectedIndex > -1)
		{
			for (int i = 0; i < clbFormFunctions.Items.Count; i++)
			{
				clbFormFunctions.SetItemChecked(i, dtGroupFormFunctions.Select("FormFunctionID = " + dataTable.Rows[i]["FormFunctionID"].ToString()).Length != 0);
			}
			for (int j = 0; j < clbReports.Items.Count; j++)
			{
				clbReports.SetItemChecked(j, dtGroupReports.Select("ReportID = " + dataTable3.Rows[j]["ReportID"].ToString()).Length != 0);
			}
		}
	}

	private void cboGroups_SelectionChanged(object sender, EventArgs e)
	{
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected O, but got Unknown
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Expected O, but got Unknown
		SelectedformID = "0";
		if (((TextEditorControlBase)cboGroups).Value == null)
		{
			return;
		}
		dtGroupForms = GroupsForms.SelectByGroupID(((TextEditorControlBase)cboGroups).Value.ToString(), "0", IsFromServer: true);
		dtGroupFormFunctions = GroupsFormsFunctions.SelectByGroupID(((TextEditorControlBase)cboGroups).Value.ToString(), "0", IsFromServer: true);
		dtGroupReports = GroupsReports.SelectByGroupID(((TextEditorControlBase)cboGroups).Value.ToString(), "0", IsFromServer: true);
		dtGroupFunctions = GroupsFunctions.SelectByGroupID(((TextEditorControlBase)cboGroups).Value.ToString(), "0", IsFromServer: true);
		dtGroupBranches = GroupsBranches.SelectByGroupID(((TextEditorControlBase)cboGroups).Value.ToString(), "0", IsFromServer: true);
		TreeItems.SelectedNodes.Clear();
		TreeItems.AfterCheck -= new AfterNodeChangedEventHandler(Tree_AfterCheck);
		NodeEnumerator enumerator = TreeItems.Nodes.GetEnumerator();
		try
		{
			while (((DisposableObjectEnumeratorBase)enumerator).MoveNext())
			{
				UltraTreeNode current = enumerator.Current;
				NodeEnumerator enumerator2 = current.Nodes.GetEnumerator();
				try
				{
					while (((DisposableObjectEnumeratorBase)enumerator2).MoveNext())
					{
						UltraTreeNode current2 = enumerator2.Current;
						NodeEnumerator enumerator3 = current2.Nodes.GetEnumerator();
						try
						{
							while (((DisposableObjectEnumeratorBase)enumerator3).MoveNext())
							{
								UltraTreeNode current3 = enumerator3.Current;
								DataRow[] array = dtGroupForms.Select("formID=" + ((KeyedSubObjectBase)current3).Key);
								current3.CheckedState = ((array.Length != 0) ? CheckState.Checked : CheckState.Unchecked);
								((SubObjectBase)current3).Tag = ((array.Length != 0) ? array[0]["PeriodDays"] : ((object)0));
							}
						}
						finally
						{
							if (enumerator3 is IDisposable disposable)
							{
								disposable.Dispose();
							}
						}
						if (((DisposableObjectCollectionBase)current2.Nodes).Count > 0)
						{
							TreeFunctions.SetParentCheckedState(current2);
						}
						else
						{
							current2.CheckedState = ((dtGroupForms.Select("formID=" + ((KeyedSubObjectBase)current2).Key).Length != 0) ? CheckState.Checked : CheckState.Unchecked);
						}
					}
				}
				finally
				{
					if (enumerator2 is IDisposable disposable2)
					{
						disposable2.Dispose();
					}
				}
			}
		}
		finally
		{
			if (enumerator is IDisposable disposable3)
			{
				disposable3.Dispose();
			}
		}
		TreeItems.AfterCheck += new AfterNodeChangedEventHandler(Tree_AfterCheck);
		for (int i = 0; i < clbGroupFunctions.Items.Count; i++)
		{
			clbGroupFunctions.SetItemChecked(i, dtGroupFunctions.Select("FunctionID = " + dtFunctions.Rows[i]["FunctionID"].ToString()).Length != 0);
		}
		for (int j = 0; j < clbBranches.Items.Count; j++)
		{
			clbBranches.SetItemChecked(j, dtGroupBranches.Select("BranchID = " + dtBranches.Rows[j]["BranchID"].ToString()).Length != 0);
		}
		clbBranches_SelectedValueChanged(null, null);
	}

	private void btnUpdate_Click(object sender, EventArgs e)
	{
		UltraTreeNode nodeByKey = TreeItems.GetNodeByKey(SelectedformID);
		if (nodeByKey != null)
		{
			DataRow[] array = dtGroupForms.Select("FormID=" + ((KeyedSubObjectBase)nodeByKey).Key);
			if (array.Length != 0)
			{
				array[0]["GroupFormID"] = -1;
				array[0]["PeriodDays"] = txtDatePeriod.Value;
			}
			((SubObjectBase)nodeByKey).Tag = txtDatePeriod.Value;
		}
		if (clbBranches.CheckedItems.Count == 0)
		{
			GlobalVariables.InformationMB.Show(" برجاء إختيار فرع واحد على الاقل للمجموعة", "Please Select one Branch At Least For This Group");
			return;
		}
		Main.StartBulkTrans(FromServer: true);
		try
		{
			string text2;
			string text3;
			string text = (text2 = (text3 = ","));
			for (int i = 0; i < dtGroupFormFunctions.Rows.Count; i++)
			{
				if (dtGroupFormFunctions.Rows[i]["GroupFormFunctionID"].ToString() != "-1")
				{
					text = string.Concat(text, dtGroupFormFunctions.Rows[i]["GroupFormFunctionID"], ",");
				}
			}
			for (int j = 0; j < dtGroupReports.Rows.Count; j++)
			{
				if (dtGroupReports.Rows[j]["GroupReportID"].ToString() != "-1")
				{
					text2 = string.Concat(text2, dtGroupReports.Rows[j]["GroupReportID"], ",");
				}
			}
			for (int k = 0; k < dtGroupForms.Rows.Count; k++)
			{
				if (dtGroupForms.Rows[k]["GroupFormID"].ToString() != "-1")
				{
					text3 = string.Concat(text3, dtGroupForms.Rows[k]["GroupFormID"], ",");
				}
			}
			Main.SyncDeleteForUpdate("Prv_GroupsFormsFunctions", "GroupID", ((TextEditorControlBase)cboGroups).Value.ToString(), "GroupFormFunctionID", text, IsFromServer: true);
			Main.SyncDeleteForUpdate("Prv_GroupsReports", "GroupID", ((TextEditorControlBase)cboGroups).Value.ToString(), "GroupReportID", text2, IsFromServer: true);
			Main.SyncDeleteForUpdate("Prv_GroupsForms", "GroupID", ((TextEditorControlBase)cboGroups).Value.ToString(), "GroupFormID", text3, IsFromServer: true);
			DataView dataView = new DataView(dtGroupForms);
			dataView.RowFilter = "GroupFormID=-1";
			DataTable dataTable = dataView.ToTable();
			if (dataTable.Rows.Count > 0)
			{
				GroupsForms.Insert_UpdateByTable(dataTable, GlobalVariables.UserID, IsFromServer: true);
			}
			dataView = new DataView(dtGroupReports);
			dataView.RowFilter = "GroupReportID=-1";
			dataTable = dataView.ToTable();
			if (dataTable.Rows.Count > 0)
			{
				GroupsReports.Insert_UpdateByTable(dataTable, GlobalVariables.UserID, IsFromServer: true);
			}
			dataView = new DataView(dtGroupFormFunctions);
			dataView.RowFilter = "GroupFormFunctionID=-1";
			dataTable = dataView.ToTable();
			if (dataTable.Rows.Count > 0)
			{
				GroupsFormsFunctions.Insert_UpdateByTable(dataTable, GlobalVariables.UserID, IsFromServer: true);
			}
			GroupsFunctions.DeleteByGroupID(((TextEditorControlBase)cboGroups).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			GroupsBranches.DeleteByGroupID(((TextEditorControlBase)cboGroups).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			for (int l = 0; l < clbGroupFunctions.Items.Count; l++)
			{
				if (clbGroupFunctions.GetItemChecked(l))
				{
					GroupsFunctions.Insert_Update("-1", ((TextEditorControlBase)cboGroups).Value.ToString(), dtFunctions.Rows[l]["FunctionID"].ToString(), GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID, IsFromServer: true);
				}
			}
			for (int m = 0; m < clbBranches.Items.Count; m++)
			{
				if (clbBranches.GetItemChecked(m))
				{
					GroupsBranches.Insert_Update("-1", ((TextEditorControlBase)cboGroups).Value.ToString(), dtBranches.Rows[m]["BranchID"].ToString(), "0", GlobalVariables.UserID, IsFromServer: true);
				}
			}
			Main.EndBulkTrans(FromServer: true);
			cboGroups_SelectionChanged(null, null);
			TreeItems.SelectedNodes.Clear();
			GlobalVariables.InformationMB.Show("تم حفظ التعديلات بنجاح", "Changes were saved Successfully");
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	private void chkAllForms_CheckedChanged(object sender, EventArgs e)
	{
		CheckState checkedState = (((UltraToggleEditorBase)chkAllForms).Checked ? CheckState.Checked : CheckState.Unchecked);
		NodeEnumerator enumerator = TreeItems.Nodes.GetEnumerator();
		try
		{
			while (((DisposableObjectEnumeratorBase)enumerator).MoveNext())
			{
				UltraTreeNode current = enumerator.Current;
				current.CheckedState = checkedState;
			}
		}
		finally
		{
			if (enumerator is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}
	}

	private void chkAllBranches_CheckedChanged(object sender, EventArgs e)
	{
		clbBranches.SelectedValueChanged -= clbBranches_SelectedValueChanged;
		for (int i = 0; i < clbBranches.Items.Count; i++)
		{
			clbBranches.SetItemChecked(i, ((UltraToggleEditorBase)chkAllBranches).Checked);
		}
		clbBranches.SelectedValueChanged += clbBranches_SelectedValueChanged;
	}

	private void clbBranches_SelectedValueChanged(object sender, EventArgs e)
	{
		((UltraToggleEditorBase)chkAllBranches).CheckedChanged -= chkAllBranches_CheckedChanged;
		((UltraToggleEditorBase)chkAllBranches).Checked = clbBranches.CheckedItems.Count == clbBranches.Items.Count && clbBranches.Items.Count > 0;
		((UltraToggleEditorBase)chkAllBranches).CheckedChanged += chkAllBranches_CheckedChanged;
	}

	private void clbReports_MouseUp(object sender, MouseEventArgs e)
	{
		if (((DisposableObjectCollectionBase)TreeItems.SelectedNodes).Count > 0)
		{
			UpdateNodeReports(TreeItems.SelectedNodes[0]);
		}
		else if (clbReports.DataSource != null)
		{
			((DataTable)clbReports.DataSource).Rows.Clear();
		}
	}

	private void clbFormFunctions_MouseUp(object sender, MouseEventArgs e)
	{
		if (((DisposableObjectCollectionBase)TreeItems.SelectedNodes).Count > 0)
		{
			UpdateNodePriveliges(TreeItems.SelectedNodes[0]);
		}
		else if (clbFormFunctions.DataSource != null)
		{
			((DataTable)clbFormFunctions.DataSource).Rows.Clear();
		}
	}

	private void DrowTree()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		TreeItems.Nodes.Clear();
		DataRow[] array = dtForms.Select("ParentID is null");
		DataRow[] array2 = array;
		foreach (DataRow dataRow in array2)
		{
			UltraTreeNode val = new UltraTreeNode();
			((KeyedSubObjectBase)val).Key = dataRow["FormID"].ToString();
			val.Text = (GlobalVariables.IsArabic ? dataRow["FormNameAr"].ToString() : dataRow["FormNameEn"].ToString());
			((SubObjectBase)val).Tag = 0;
			TreeItems.Nodes.Add(val);
			DrowChilds(val);
		}
	}

	private void DrowChilds(UltraTreeNode tn)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		DataRow[] array = dtForms.Select("ParentID = " + ((KeyedSubObjectBase)tn).Key);
		if (array.Length != 0)
		{
			DataRow[] array2 = array;
			foreach (DataRow dataRow in array2)
			{
				UltraTreeNode val = new UltraTreeNode();
				((KeyedSubObjectBase)val).Key = dataRow["FormID"].ToString();
				val.Text = (GlobalVariables.IsArabic ? dataRow["FormNameAr"].ToString() : dataRow["FormNameEn"].ToString());
				((SubObjectBase)val).Tag = 0;
				tn.Nodes.Add(val);
				DrowChilds(val);
			}
		}
	}

	private void SetAllNodeChildsCheckState(CheckState Checked, UltraTreeNode Node)
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)Node.Nodes).Count; i++)
		{
			SetNodeCheckState(Checked, Node.Nodes[i]);
			if (((DisposableObjectCollectionBase)Node.Nodes[i].Nodes).Count > 0)
			{
				SetAllNodeChildsCheckState(Checked, Node.Nodes[i]);
			}
		}
	}

	private void SetNodeCheckState(CheckState Checked, UltraTreeNode Node)
	{
		Node.CheckedState = Checked;
		UpdateNode(Node);
	}

	public void SetParentState(UltraTreeNode Node)
	{
		Node.CheckedState = TreeFunctions.GetParentCheckedState(Node);
		UpdateNode(Node);
		if (Node.Parent != null)
		{
			SetParentState(Node.Parent);
		}
	}

	private void UpdateNode(UltraTreeNode n)
	{
		if (cboGroups.SelectedIndex == -1)
		{
			return;
		}
		DataRow[] array = dtGroupForms.Select("FormID=" + ((KeyedSubObjectBase)n).Key);
		if (n.CheckedState != CheckState.Unchecked)
		{
			DataRow[] array2 = dtFormFunctions.Select("FormID=" + ((KeyedSubObjectBase)n).Key);
			DataRow[] array3 = dtFormReports.Select("FormID=" + ((KeyedSubObjectBase)n).Key);
			DataRow[] array4 = array2;
			foreach (DataRow dataRow in array4)
			{
				if (dtGroupFormFunctions.Select("FormFunctionID=" + dataRow["FormFunctionID"]).Length == 0)
				{
					DataRow dataRow2 = dtGroupFormFunctions.NewRow();
					dataRow2["GroupFormFunctionID"] = -1;
					dataRow2["FormFunctionID"] = dataRow["FormFunctionID"];
					dataRow2["GroupID"] = ((cboGroups.SelectedIndex > -1) ? ((TextEditorControlBase)cboGroups).Value.ToString() : "");
					dataRow2["Deleted"] = false;
					dataRow2["BranchID"] = GlobalVariables.CurrentBranchID;
					dtGroupFormFunctions.Rows.Add(dataRow2);
				}
			}
			DataRow[] array5 = array3;
			foreach (DataRow dataRow3 in array5)
			{
				if (dtGroupReports.Select("ReportID=" + dataRow3["ReportID"]).Length == 0)
				{
					DataRow dataRow4 = dtGroupReports.NewRow();
					dataRow4["GroupReportID"] = -1;
					dataRow4["ReportID"] = dataRow3["ReportID"];
					dataRow4["GroupID"] = ((cboGroups.SelectedIndex > -1) ? ((TextEditorControlBase)cboGroups).Value.ToString() : "");
					dataRow4["Deleted"] = false;
					dataRow4["BranchID"] = GlobalVariables.CurrentBranchID;
					dtGroupReports.Rows.Add(dataRow4);
				}
			}
		}
		else
		{
			DataRow[] array6 = dtFormFunctions.Select("FormID=" + ((KeyedSubObjectBase)n).Key);
			DataRow[] array7 = dtFormReports.Select("FormID=" + ((KeyedSubObjectBase)n).Key);
			DataRow[] array8 = array6;
			foreach (DataRow dataRow5 in array8)
			{
				if (dtGroupFormFunctions.Select("FormFunctionID=" + dataRow5["FormFunctionID"]).Length != 0)
				{
					dtGroupFormFunctions.Rows.Remove(dtGroupFormFunctions.Select("FormFunctionID=" + dataRow5["FormFunctionID"])[0]);
				}
			}
			DataRow[] array9 = array7;
			foreach (DataRow dataRow6 in array9)
			{
				if (dtGroupReports.Select("ReportID=" + dataRow6["ReportID"]).Length != 0)
				{
					dtGroupReports.Rows.Remove(dtGroupReports.Select("ReportID=" + dataRow6["ReportID"])[0]);
				}
			}
		}
		if (n.CheckedState != CheckState.Unchecked)
		{
			if (array.Length == 0)
			{
				DataRow dataRow7 = dtGroupForms.NewRow();
				dataRow7["GroupFormID"] = -1;
				dataRow7["GroupID"] = ((cboGroups.SelectedIndex > -1) ? ((TextEditorControlBase)cboGroups).Value.ToString() : "");
				dataRow7["FormID"] = ((KeyedSubObjectBase)n).Key;
				dataRow7["PeriodDays"] = ((SubObjectBase)n).Tag;
				dataRow7["Deleted"] = false;
				dataRow7["BranchID"] = GlobalVariables.CurrentBranchID;
				dtGroupForms.Rows.Add(dataRow7);
			}
		}
		else if (array.Length != 0)
		{
			dtGroupForms.Rows.Remove(dtGroupForms.Select("FormID=" + ((KeyedSubObjectBase)n).Key)[0]);
		}
	}

	private void UpdateNodePriveliges(UltraTreeNode n)
	{
		if (cboGroups.SelectedIndex == -1)
		{
			return;
		}
		for (int i = 0; i < clbFormFunctions.Items.Count; i++)
		{
			DataRow dataRow = ((DataTable)clbFormFunctions.DataSource).Rows[i];
			DataRow[] array = dtGroupFormFunctions.Select("FormFunctionID=" + dataRow["FormFunctionID"]);
			if (clbFormFunctions.GetItemChecked(i))
			{
				if (array.Length == 0)
				{
					DataRow dataRow2 = dtGroupFormFunctions.NewRow();
					dataRow2["GroupFormFunctionID"] = -1;
					dataRow2["FormFunctionID"] = dataRow["FormFunctionID"];
					dataRow2["GroupID"] = ((cboGroups.SelectedIndex > -1) ? ((TextEditorControlBase)cboGroups).Value.ToString() : "");
					dataRow2["Deleted"] = false;
					dataRow2["BranchID"] = GlobalVariables.CurrentBranchID;
					dtGroupFormFunctions.Rows.Add(dataRow2);
				}
			}
			else if (array.Length != 0)
			{
				dtGroupFormFunctions.Rows.Remove(array[0]);
			}
		}
	}

	private void UpdateNodeReports(UltraTreeNode n)
	{
		if (cboGroups.SelectedIndex == -1)
		{
			return;
		}
		for (int i = 0; i < clbReports.Items.Count; i++)
		{
			string text = ((DataTable)clbReports.DataSource).Rows[i]["ReportID"].ToString();
			DataRow[] array = dtGroupReports.Select("ReportID=" + text);
			if (clbReports.GetItemChecked(i))
			{
				if (array.Length == 0)
				{
					DataRow dataRow = dtGroupReports.NewRow();
					dataRow["GroupReportID"] = -1;
					dataRow["GroupID"] = ((cboGroups.SelectedIndex > -1) ? ((TextEditorControlBase)cboGroups).Value.ToString() : "");
					dataRow["ReportID"] = text;
					dataRow["Deleted"] = false;
					dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
					dtGroupReports.Rows.Add(dataRow);
				}
			}
			else if (array.Length != 0)
			{
				dtGroupReports.Rows.Remove(array[0]);
			}
		}
	}

	private void btnResetUser_Click(object sender, EventArgs e)
	{
		if (cboGroups.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(" برجاء إختيار المجموعة", "Please Select Group");
			return;
		}
		GlobalVariables.QuestionMB.Show("ملحوظة :- سوف تتم إعادة ضبط صلاحيات كل مستخدمي هذه المجموعة\r\nهل تريد الحفظ ؟", "Note: All Privileges of Group's Users will be Overwritten.\r\nDo you want to save ?");
		if (GlobalVariables.MessageBoxResult == 'Y')
		{
			Main.StartBulkTrans(FromServer: true);
			try
			{
				Groups.ResetUserPrivileges(((TextEditorControlBase)cboGroups).Value.ToString(), "-1", GlobalVariables.CurrentBranchID, IsFromServer: true);
				Main.EndBulkTrans(FromServer: true);
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: true);
				GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
				return;
			}
			GlobalFunctions.LoadUserPrivileges();
			GlobalVariables.InformationMB.Show("تم حفظ التعديلات بنجاح", "Changes were saved Successfully");
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
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Expected O, but got Unknown
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Expected O, but got Unknown
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected O, but got Unknown
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Expected O, but got Unknown
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Expected O, but got Unknown
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Expected O, but got Unknown
		//IL_0711: Unknown result type (might be due to invalid IL or missing references)
		//IL_071b: Expected O, but got Unknown
		//IL_0729: Unknown result type (might be due to invalid IL or missing references)
		//IL_0733: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Privilege.Transactions.frmGroupsPrivileges));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Override val4 = new Override();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		this.cboGroups = new UltraComboEditor();
		this.lblGroup = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.btnUpdate = new UltraButton();
		this.btnClose = new UltraButton();
		this.clbFormFunctions = new System.Windows.Forms.CheckedListBox();
		this.clbGroupFunctions = new System.Windows.Forms.CheckedListBox();
		this.chkAllBranches = new UltraCheckEditor();
		this.clbBranches = new System.Windows.Forms.CheckedListBox();
		this.ultraLabel3 = new UltraLabel();
		this.lblTitle = new UltraLabel();
		this.clbReports = new System.Windows.Forms.CheckedListBox();
		this.lblReports = new UltraLabel();
		this.TreeItems = new UltraTree();
		this.chkAllForms = new UltraCheckEditor();
		this.btnResetUser = new UltraButton();
		this.txtDatePeriod = new System.Windows.Forms.NumericUpDown();
		this.lblDatePeriod2 = new UltraLabel();
		this.lblDatePeriod = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboGroups).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllForms).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDatePeriod).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.cboGroups, "cboGroups");
		((System.Windows.Forms.Control)(object)this.cboGroups).Name = "cboGroups";
		this.cboGroups.SelectionChanged += new System.EventHandler(cboGroups_SelectionChanged);
		resources.ApplyResources(this.lblGroup, "lblGroup");
		this.lblGroup.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGroup).Name = "lblGroup";
		((ControlBase)this.lblGroup).WrapText = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.btnUpdate, "btnUpdate");
		((System.Windows.Forms.Control)(object)this.btnUpdate).Name = "btnUpdate";
		((System.Windows.Forms.Control)(object)this.btnUpdate).Click += new System.EventHandler(btnUpdate_Click);
		resources.ApplyResources(this.btnClose, "btnClose");
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.clbFormFunctions, "clbFormFunctions");
		this.clbFormFunctions.CheckOnClick = true;
		this.clbFormFunctions.ForeColor = System.Drawing.Color.Navy;
		this.clbFormFunctions.FormattingEnabled = true;
		this.clbFormFunctions.Name = "clbFormFunctions";
		this.clbFormFunctions.MouseUp += new System.Windows.Forms.MouseEventHandler(clbFormFunctions_MouseUp);
		resources.ApplyResources(this.clbGroupFunctions, "clbGroupFunctions");
		this.clbGroupFunctions.CheckOnClick = true;
		this.clbGroupFunctions.ForeColor = System.Drawing.Color.Navy;
		this.clbGroupFunctions.FormattingEnabled = true;
		this.clbGroupFunctions.Name = "clbGroupFunctions";
		resources.ApplyResources(this.chkAllBranches, "chkAllBranches");
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val).ForeColor = System.Drawing.Color.Blue;
		resources.ApplyResources(val, "appearance6");
		((UltraToggleEditorBase)this.chkAllBranches).Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.chkAllBranches).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllBranches).BackColorInternal = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllBranches).CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		((System.Windows.Forms.Control)(object)this.chkAllBranches).Name = "chkAllBranches";
		((UltraToggleEditorBase)this.chkAllBranches).CheckedChanged += new System.EventHandler(chkAllBranches_CheckedChanged);
		resources.ApplyResources(this.clbBranches, "clbBranches");
		this.clbBranches.CheckOnClick = true;
		this.clbBranches.ForeColor = System.Drawing.Color.Navy;
		this.clbBranches.MultiColumn = true;
		this.clbBranches.Name = "clbBranches";
		this.clbBranches.SelectedValueChanged += new System.EventHandler(clbBranches_SelectedValueChanged);
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance7");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.clbReports, "clbReports");
		this.clbReports.CheckOnClick = true;
		this.clbReports.ForeColor = System.Drawing.Color.Navy;
		this.clbReports.FormattingEnabled = true;
		this.clbReports.Name = "clbReports";
		this.clbReports.MouseUp += new System.Windows.Forms.MouseEventHandler(clbReports_MouseUp);
		resources.ApplyResources(this.lblReports, "lblReports");
		this.lblReports.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReports).Name = "lblReports";
		((ControlBase)this.lblReports).WrapText = false;
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
		this.TreeItems.AfterCheck += new AfterNodeChangedEventHandler(Tree_AfterCheck);
		this.TreeItems.AfterSelect += new AfterNodeSelectEventHandler(TreeItems_AfterSelect);
		resources.ApplyResources(this.chkAllForms, "chkAllForms");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val5, "appearance8");
		((UltraToggleEditorBase)this.chkAllForms).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.chkAllForms).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllForms).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllForms).Name = "chkAllForms";
		((UltraControlBase)this.chkAllForms).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAllForms).CheckedChanged += new System.EventHandler(chkAllForms_CheckedChanged);
		resources.ApplyResources(this.btnResetUser, "btnResetUser");
		((UltraButtonBase)this.btnResetUser).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((System.Windows.Forms.Control)(object)this.btnResetUser).Name = "btnResetUser";
		((System.Windows.Forms.Control)(object)this.btnResetUser).Click += new System.EventHandler(btnResetUser_Click);
		resources.ApplyResources(this.txtDatePeriod, "txtDatePeriod");
		this.txtDatePeriod.Maximum = new decimal(new int[4] { 365, 0, 0, 0 });
		this.txtDatePeriod.Name = "txtDatePeriod";
		resources.ApplyResources(this.lblDatePeriod2, "lblDatePeriod2");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val6, "appearance9");
		((ControlBase)this.lblDatePeriod2).Appearance = (AppearanceBase)(object)val6;
		this.lblDatePeriod2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDatePeriod2).Name = "lblDatePeriod2";
		((ControlBase)this.lblDatePeriod2).WrapText = false;
		resources.ApplyResources(this.lblDatePeriod, "lblDatePeriod");
		this.lblDatePeriod.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDatePeriod).Name = "lblDatePeriod";
		((ControlBase)this.lblDatePeriod).WrapText = false;
		resources.ApplyResources(this, "$this");
		base.CancelButton = (System.Windows.Forms.IButtonControl)this.btnClose;
		base.Controls.Add(this.txtDatePeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDatePeriod2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDatePeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnResetUser);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllForms);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.TreeItems);
		base.Controls.Add(this.clbReports);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReports);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllBranches);
		base.Controls.Add(this.clbBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add(this.clbGroupFunctions);
		base.Controls.Add(this.clbFormFunctions);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnUpdate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGroup);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboGroups);
		base.Name = "frmGroupsPrivileges";
		base.Load += new System.EventHandler(frmGroupsPrivileges_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboGroups, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGroup, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnUpdate, 0);
		base.Controls.SetChildIndex(this.clbFormFunctions, 0);
		base.Controls.SetChildIndex(this.clbGroupFunctions, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex(this.clbBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReports, 0);
		base.Controls.SetChildIndex(this.clbReports, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.TreeItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllForms, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnResetUser, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDatePeriod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDatePeriod2, 0);
		base.Controls.SetChildIndex(this.txtDatePeriod, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboGroups).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllForms).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDatePeriod).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
