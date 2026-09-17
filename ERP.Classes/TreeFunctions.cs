using System;
using System.Data;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.Classes;

public class TreeFunctions
{
	public static void SetAllTreeNodesCheckState(bool Checked, UltraTree tree)
	{
		CheckState checkState = (Checked ? CheckState.Checked : CheckState.Unchecked);
		for (int i = 0; i < ((DisposableObjectCollectionBase)tree.Nodes).Count; i++)
		{
			tree.Nodes[i].CheckedState = checkState;
			if (((DisposableObjectCollectionBase)tree.Nodes[i].Nodes).Count > 0)
			{
				SetAllNodeChildsCheckState(checkState, tree.Nodes[i]);
			}
		}
	}

	public static void SetAllNodeChildsCheckState(CheckState Checked, UltraTreeNode Node)
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

	public static void SetCheckBoxAllState(UltraTree tree, UltraCheckEditor CheckBox)
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

	public static void SetNodeCheckState(CheckState Checked, UltraTreeNode Node)
	{
		Node.CheckedState = Checked;
	}

	public static void SetNodeCheckState(CheckState Checked, string Key, UltraTree tree)
	{
		tree.GetNodeByKey(Key).CheckedState = Checked;
	}

	public static void SetParentCheckedState(UltraTreeNode Node)
	{
		Node.CheckedState = GetParentCheckedState(Node);
		if (Node.Parent != null)
		{
			SetParentCheckedState(Node.Parent);
		}
	}

	public static void FillTree(UltraTree TreeItem, DataTable dataTable, string ParentIDCol, string IDCol, string NameCol, string NumberCol, string IsMainCol)
	{
		try
		{
			TreeItem.Nodes.Clear();
			DataRow[] array = dataTable.Select(ParentIDCol + "  is null or " + ParentIDCol + " = 0 ");
			if (array.Length == 0)
			{
				return;
			}
			for (int i = 0; i < array.Length; i++)
			{
				AddNode(TreeItem, array[i], array[i][ParentIDCol].ToString(), IDCol, NameCol, NumberCol, IsMainCol);
				if ((bool)array[i][IsMainCol])
				{
					FillTreeChilds(TreeItem, dataTable, ParentIDCol, array[i][IDCol].ToString(), IDCol, NameCol, NumberCol, IsMainCol);
				}
			}
		}
		catch
		{
		}
	}

	public static void FillTreeOneLevel(UltraTree TreeItem, DataTable dataTable, string IDCol, string NameCol)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		TreeItem.Nodes.Clear();
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			UltraTreeNode val = new UltraTreeNode(dataTable.Rows[i][IDCol].ToString(), dataTable.Rows[i][NameCol].ToString());
			((SubObjectBase)val).Tag = false;
			TreeItem.Nodes.Add(val);
		}
	}

	public static int GetTreeOneLevelCheckedItemCount(UltraTree tree)
	{
		int num = 0;
		for (int i = 0; i < ((DisposableObjectCollectionBase)tree.Nodes).Count; i++)
		{
			if (((DisposableObjectCollectionBase)tree.Nodes[i].Nodes).Count == 0 && !Convert.ToBoolean(((SubObjectBase)tree.Nodes[i]).Tag) && tree.Nodes[i].CheckedState == CheckState.Checked)
			{
				num++;
			}
		}
		return num;
	}

	public static string GetTreeCheckedNodesIDs(UltraTree tree)
	{
		string text = "";
		for (int i = 0; i < ((DisposableObjectCollectionBase)tree.Nodes).Count; i++)
		{
			text = ((((DisposableObjectCollectionBase)tree.Nodes[i].Nodes).Count != 0 || Convert.ToBoolean(((SubObjectBase)tree.Nodes[i]).Tag) || tree.Nodes[i].CheckedState != CheckState.Checked) ? (text + GetNodeCheckedChildsIDs(tree.Nodes[i])) : (text + ((KeyedSubObjectBase)tree.Nodes[i]).Key + ","));
		}
		return "," + text;
	}

	public static string GetNodeCheckedChildsIDs(UltraTreeNode Node)
	{
		string text = "";
		for (int i = 0; i < ((DisposableObjectCollectionBase)Node.Nodes).Count; i++)
		{
			text = ((((DisposableObjectCollectionBase)Node.Nodes[i].Nodes).Count != 0 || Convert.ToBoolean(((SubObjectBase)Node.Nodes[i]).Tag) || Node.Nodes[i].CheckedState != CheckState.Checked) ? (text + GetNodeCheckedChildsIDs(Node.Nodes[i])) : (text + ((KeyedSubObjectBase)Node.Nodes[i]).Key + ","));
		}
		return text;
	}

	public static string GetTreeFirstCheckedNodeID(UltraTree tree)
	{
		string text = "";
		for (int i = 0; i < ((DisposableObjectCollectionBase)tree.Nodes).Count; i++)
		{
			if (tree.Nodes[i].CheckedState == CheckState.Checked)
			{
				return ((KeyedSubObjectBase)tree.Nodes[i]).Key;
			}
			if (((DisposableObjectCollectionBase)tree.Nodes[i].Nodes).Count > 0)
			{
				text = GetNodeFirstCheckedChildsIDs(tree.Nodes[i]);
				if (text != "")
				{
					return text;
				}
			}
		}
		return "";
	}

	public static string GetNodeFirstCheckedChildsIDs(UltraTreeNode Node)
	{
		string text = "";
		for (int i = 0; i < ((DisposableObjectCollectionBase)Node.Nodes).Count; i++)
		{
			if (Node.Nodes[i].CheckedState == CheckState.Checked)
			{
				return ((KeyedSubObjectBase)Node.Nodes[i]).Key;
			}
			if (((DisposableObjectCollectionBase)Node.Nodes[i].Nodes).Count > 0)
			{
				text = GetNodeFirstCheckedChildsIDs(Node.Nodes[i]);
				if (text != "")
				{
					return text;
				}
			}
		}
		return "";
	}

	public static CheckState GetParentCheckedState(UltraTreeNode Node)
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
		if (num == ((DisposableObjectCollectionBase)Node.Nodes).Count && ((DisposableObjectCollectionBase)Node.Nodes).Count > 0)
		{
			return CheckState.Checked;
		}
		if (num2 == ((DisposableObjectCollectionBase)Node.Nodes).Count)
		{
			return CheckState.Unchecked;
		}
		return CheckState.Indeterminate;
	}

	private static void FillTreeChilds(UltraTree tree, DataTable dtChart, string ParentIDCol, string ParentID, string IDCol, string NameCol, string NumberCol, string IsMainCol)
	{
		DataRow[] array = dtChart.Select(ParentIDCol + "=" + ParentID);
		if (array.Length == 0)
		{
			return;
		}
		for (int i = 0; i < array.Length; i++)
		{
			AddNode(tree, array[i], array[i][ParentIDCol].ToString(), IDCol, NameCol, NumberCol, IsMainCol);
			if ((bool)array[i][IsMainCol])
			{
				FillTreeChilds(tree, dtChart, ParentIDCol, array[i][IDCol].ToString(), IDCol, NameCol, NumberCol, IsMainCol);
			}
		}
	}

	private static void AddNode(UltraTree tree, DataRow dr, string ParentID, string IDCol, string NameCol, string NumberCol, string IsMainCol)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		UltraTreeNode val = new UltraTreeNode(dr[IDCol].ToString(), ((NumberCol != null && NumberCol != "") ? string.Concat(dr[NumberCol], " - ") : "") + dr[NameCol]);
		((SubObjectBase)val).Tag = dr[IsMainCol].ToString();
		try
		{
			if (ParentID == "0" || ParentID == "")
			{
				tree.Nodes.Add(val);
			}
			else
			{
				tree.GetNodeByKey(ParentID).Nodes.Add(val);
			}
		}
		catch
		{
		}
	}
}
