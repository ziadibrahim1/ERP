using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Security;
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.AbstractForms;

public class frmTree2 : frmButtons
{
	public string NameCol = "";

	public string NameEnCol = "";

	public string IsMainCol = "";

	public string ItemLevelCol = "";

	public string ParentIDCol = "";

	public string AdditionalCol1 = "";

	public string AdditionalCol2 = "";

	public string AdditionalCol3 = "";

	public string AdditionalCol4 = "";

	public string LevelsTable = "";

	public string LevelsCol = "";

	public string LevelsWidthCol = "";

	public DataTable dtLevels;

	public DataTable dtChart = new DataTable();

	public int NodeLevel;

	public UltraTreeNode SelectedNode;

	public bool AllowAddRoot = true;

	public bool OrderByName = false;

	private IContainer components = null;

	public UltraTree treeChart;

	public UltraTextEditor txtCode;

	public UltraTextEditor txtName;

	public UltraLabel lblPath;

	public UltraTextEditor txtNameEn;

	public UltraLabel lblTitle;

	public UltraLabel lblTitle2;

	public UltraButton btnAddRoot;

	public UltraLabel label1;

	public UltraLabel label2;

	public UltraLabel label3;

	public UltraButton btnAttachFile;

	public frmTree2()
	{
		InitializeComponent();
		((TextEditorControlBase)txtCode).Appearance.TextHAlign = (HAlign)2;
		((TextEditorControlBase)txtName).Appearance.TextHAlign = (HAlign)2;
		((TextEditorControlBase)txtNameEn).Appearance.TextHAlign = (HAlign)2;
		((ControlBase)lblPath).Appearance.TextHAlign = (HAlign)((!GlobalVariables.IsArabic) ? 1 : 3);
	}

	public override void CallButtons(KeyEventArgs e)
	{
		base.CallButtons(e);
		if (e.KeyCode == Keys.F8 && !Adding && !Updating)
		{
			TreeSearch();
		}
	}

	public virtual void SaveCopiedData(UltraTreeNode CopiedNodes, int p)
	{
	}

	public virtual bool HasTransactionValidation()
	{
		return false;
	}

	public virtual void TreeSearch()
	{
	}

	public override void PrepareData()
	{
		if (TableName != "" && LevelsTable != "")
		{
			dtLevels = Main.SyncExecuteQuery_DataTable("TreeLevels_Select '" + LevelsCol + "','" + LevelsWidthCol + "','" + LevelsTable + "'");
			dtChart = Main.SyncExecuteQuery_DataTable("TreeData_Select '" + IDCol + "','" + NoCol + "','" + NameCol + "','" + NameEnCol + "','" + ParentIDCol + "','" + IsMainCol + "','" + ItemLevelCol + "','" + AdditionalCol1 + "','" + AdditionalCol2 + "','" + AdditionalCol3 + "','" + AdditionalCol4 + "','" + TableName + "',-1," + (OrderByName ? "1" : "0"));
			treeChart.Nodes.Clear();
			if (dtChart.Rows.Count > 0)
			{
				FillTree("0");
			}
		}
	}

	public override void btnRefreshDataClick()
	{
		if (!Adding && !Updating)
		{
			PrepareData();
		}
	}

	public override void FillData()
	{
		((Control)(object)lblPath).Text = "";
		((Control)(object)treeChart).Select();
	}

	public override void DisplayData()
	{
		if (SelectedNode != null)
		{
			RowID = ((KeyedSubObjectBase)SelectedNode).Key;
		}
		base.DisplayData();
		if (SelectedNode != null)
		{
			NodeLevel = SelectedNode.Level + 1;
			((Control)(object)txtCode).Text = ((DataRow)((SubObjectBase)SelectedNode).Tag)[NoCol].ToString();
			((Control)(object)txtName).Text = ((DataRow)((SubObjectBase)SelectedNode).Tag)[NameCol].ToString();
			((Control)(object)txtNameEn).Text = ((DataRow)((SubObjectBase)SelectedNode).Tag)[NameEnCol].ToString();
			HighlightNodes(SelectedNode);
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)txtCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNameEn).ReadOnly = NavMode;
		((Control)(object)treeChart).Enabled = NavMode;
		((Control)(object)btnAddRoot).Visible = NavMode && AllowAddRoot;
		((Control)(object)btnRefreshData).Visible = true;
		((Control)(object)btnAttachFile).Visible = !Adding;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((Control)(object)txtCode).Text = "";
		((Control)(object)txtName).Text = "";
		((Control)(object)txtNameEn).Text = "";
	}

	public virtual int LevelStart(int Level)
	{
		return Convert.ToInt32(dtLevels.Select("Level=" + Level)[0]["Start"]);
	}

	public virtual int LevelEnd(int Level)
	{
		return Convert.ToInt32(dtLevels.Select("Level=" + Level)[0]["End"]);
	}

	public virtual int LevelWidth(int Level)
	{
		return Convert.ToInt32(dtLevels.Select("Level=" + Level)[0]["Width"]);
	}

	public virtual string GetCode()
	{
		if (NodeLevel != 0)
		{
			string text = "";
			int startIndex = LevelStart(NodeLevel + (Adding ? 1 : 0));
			text = ((DataRow)((SubObjectBase)SelectedNode).Tag)[NoCol].ToString();
			if (Updating)
			{
				text = text.Remove(startIndex);
			}
			return text.Insert(startIndex, ((Control)(object)txtCode).Text);
		}
		return ((Control)(object)txtCode).Text;
	}

	public override void btnAddClick()
	{
		if (((DisposableObjectCollectionBase)treeChart.Nodes).Count == 0 || ((DisposableObjectCollectionBase)treeChart.SelectedNodes).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار نقطة من الدليل", "Select a node from the tree first");
		}
		else if (dtLevels.Rows.Count <= NodeLevel)
		{
			GlobalVariables.InformationMB.Show("لا يمكن تجاوز المستوى الأقصى للدليل", "Cannot Exceed Max Chart Level..");
		}
		else if (!HasTransactionValidation())
		{
			base.btnAddClick();
			((TextEditorControlBase)txtCode).MaxLength = LevelWidth(NodeLevel + 1);
			((Control)(object)treeChart).Enabled = false;
		}
	}

	private void btnAddRoot_Click(object sender, EventArgs e)
	{
		NodeLevel = 0;
		treeChart.SelectedNodes.Clear();
		SelectedNode = null;
		base.btnAddClick();
		((TextEditorControlBase)txtCode).MaxLength = LevelWidth(NodeLevel + 1);
		((Control)(object)treeChart).Enabled = false;
	}

	public override void btnUpdateClick()
	{
		if (((DisposableObjectCollectionBase)treeChart.SelectedNodes).Count == 0)
		{
			GlobalVariables.InformationMB.Show("اختر عنصر من الشجره اولا", "Select a node from the tree first");
			return;
		}
		RowID = ((DataRow)((SubObjectBase)SelectedNode).Tag)[IDCol].ToString();
		base.btnUpdateClick();
		string text = ((DataRow)((SubObjectBase)SelectedNode).Tag)[NoCol].ToString();
		((Control)(object)txtCode).Text = text.Substring(LevelStart(NodeLevel), LevelWidth(NodeLevel));
		((TextEditorControlBase)txtCode).MaxLength = LevelWidth(NodeLevel);
		((Control)(object)treeChart).Enabled = false;
	}

	public override void btnDeleteClick()
	{
		if (((DisposableObjectCollectionBase)treeChart.SelectedNodes).Count == 0)
		{
			GlobalVariables.InformationMB.Show("اختر عنصر من الشجره اولا", "Select a node from the tree first");
		}
		else if (((DisposableObjectCollectionBase)SelectedNode.Nodes).Count > 0)
		{
			GlobalVariables.InformationMB.Show(" لايمكن حذف هذا العنصر لوجود عناصر تحته", "Cannot Delete this Node It Has Sub Nodes");
		}
		else if (!HasTransactionValidation())
		{
			RowID = (((SubObjectBase)SelectedNode).Tag as DataRow)[0].ToString();
			base.btnDeleteClick();
			ClearControls();
		}
	}

	public virtual void treeChart_AfterSelect(object sender, SelectEventArgs e)
	{
		if (((DisposableObjectCollectionBase)treeChart.SelectedNodes).Count > 0 && ((SubObjectBase)treeChart.SelectedNodes[0]).Tag != null)
		{
			SelectedNode = e.NewSelections[0];
			DisplayData();
		}
	}

	public override void btnOKClick()
	{
		if (!ValidateData())
		{
			return;
		}
		DataSaved = true;
		if (Adding)
		{
			AddData();
			if (DataSaved)
			{
				if (NodeLevel == 0)
				{
					btnAddRoot_Click(null, null);
				}
				else
				{
					btnAddClick();
				}
			}
			return;
		}
		UpdateData();
		if (DataSaved)
		{
			Updating = false;
			SetControls(NavMode: true);
			if (RowID != "" && TableName != "")
			{
				UsersTransactions.DeleteByRowID(TableName, RowID);
			}
			DisplayData();
		}
	}

	public override void btnCancelClick()
	{
		base.btnCancelClick();
		((Control)(object)treeChart).Enabled = true;
		if (((DisposableObjectCollectionBase)treeChart.Nodes).Count > 0 && SelectedNode != null)
		{
			treeChart.ActiveNode = SelectedNode;
		}
		DisplayData();
	}

	public override void AddData()
	{
		int num = TreeAddData();
		if (DataSaved)
		{
			DataTable dataTable = Main.SyncExecuteQuery_DataTable("TreeData_Select '" + IDCol + "','" + NoCol + "','" + NameCol + "','" + NameEnCol + "','" + ParentIDCol + "','" + IsMainCol + "','" + ItemLevelCol + "','" + AdditionalCol1 + "','" + AdditionalCol2 + "','" + AdditionalCol3 + "','" + AdditionalCol4 + "','" + TableName + "'," + num);
			dtChart.ImportRow(dataTable.Rows[0]);
			AddNode(dataTable.Rows[0], (NodeLevel == 0) ? "0" : ((KeyedSubObjectBase)SelectedNode).Key);
			if (NodeLevel != 0 && ((DisposableObjectCollectionBase)SelectedNode.Nodes).Count == 1)
			{
				Main.SyncExecuteNonQuery("Update " + TableName + " set " + IsMainCol + " = 1 where " + IDCol + " = " + ((KeyedSubObjectBase)SelectedNode).Key);
				DataRow dataRow = dtChart.Select(IDCol + "=" + ((KeyedSubObjectBase)SelectedNode).Key)[0];
				dataRow[IsMainCol] = 1;
				((SubObjectBase)SelectedNode).Tag = dataRow;
				SelectedNode.Override.NodeAppearance.Image = Resources.folderfortree;
				SelectedNode.ExpandAll();
			}
		}
	}

	public override void UpdateData()
	{
		DataRow dataRow = dtChart.Select(IDCol + "=" + ((KeyedSubObjectBase)SelectedNode).Key)[0];
		TreeUpdateData();
		if (!DataSaved)
		{
			return;
		}
		DataRow dataRow2 = Main.SyncExecuteQuery_DataTable("TreeData_Select '" + IDCol + "','" + NoCol + "','" + NameCol + "','" + NameEnCol + "','" + ParentIDCol + "','" + IsMainCol + "','" + ItemLevelCol + "','" + AdditionalCol1 + "','" + AdditionalCol2 + "','" + AdditionalCol3 + "','" + AdditionalCol4 + "','" + TableName + "'," + ((KeyedSubObjectBase)SelectedNode).Key).Rows[0];
		if (dataRow[NoCol].ToString() != dataRow2[NoCol].ToString())
		{
			Main.SyncExecuteQuery_DataTable(string.Concat("update ", TableName, " set ", NoCol, " = '", dataRow2[NoCol], "' + SUBSTRING(", NoCol, ", ", dataRow[NoCol].ToString().Length + 1, ", LEN(", NoCol, ")) Where ", NoCol, " Like '", dataRow[NoCol], "%'"));
			DataRow[] array = dtChart.Select(string.Concat(NoCol, " Like '", dataRow[NoCol], "%'"));
			for (int i = 0; i < array.Length; i++)
			{
				array[i][NoCol] = string.Concat(dataRow2[NoCol], array[i][NoCol].ToString().Remove(0, dataRow[NoCol].ToString().Length));
				treeChart.GetNodeByKey(array[i][IDCol].ToString()).Text = array[i][NoCol].ToString() + "  " + array[i][GlobalVariables.IsArabic ? NameCol : NameEnCol].ToString();
			}
		}
		dataRow.Delete();
		dtChart.ImportRow(dataRow2);
		((SubObjectBase)SelectedNode).Tag = dataRow2;
		SelectedNode.Text = dataRow2[NoCol].ToString() + "  " + dataRow2[GlobalVariables.IsArabic ? NameCol : NameEnCol].ToString();
	}

	public override void DeleteData()
	{
		TreeDeleteData();
		if (Main.Success)
		{
			dtChart.Select(IDCol + "=" + ((KeyedSubObjectBase)SelectedNode).Key)[0].Delete();
			SelectedNode.Remove();
		}
	}

	public virtual int TreeAddData()
	{
		return 0;
	}

	public virtual void TreeUpdateData()
	{
	}

	public virtual void TreeDeleteData()
	{
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل الكود", "Enter a valid Node Code..");
			return false;
		}
		if (((Control)(object)txtName).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل الاسم", "Enter a valid Node Name..");
			return false;
		}
		DataRow[] array = dtChart.Select(NoCol + " = '" + GetCode() + "'");
		if (array.Length != 0 && (Adding || array[0][IDCol].ToString() != ((KeyedSubObjectBase)SelectedNode).Key))
		{
			GlobalVariables.InformationMB.Show("هذا الكود موجود من قبل", "This Code is Already Exists..");
			return false;
		}
		array = dtChart.Select(NameCol + " = '" + ((Control)(object)txtName).Text + "'");
		if (array.Length != 0 && (Adding || array[0][IDCol].ToString() != ((KeyedSubObjectBase)SelectedNode).Key))
		{
			GlobalVariables.InformationMB.Show("هذا الاسم موجود من قبل", "This Name is Already Exists..");
			return false;
		}
		return true;
	}

	public virtual void FillTree(string ParentID)
	{
		DataRow[] array = dtChart.Select(ParentIDCol + "=" + ParentID);
		if (array.Length == 0)
		{
			return;
		}
		for (int i = 0; i < array.Length; i++)
		{
			AddNode(array[i], ParentID);
			if ((bool)array[i][IsMainCol])
			{
				FillTree(array[i][IDCol].ToString());
			}
		}
	}

	public virtual void AddNode(DataRow dr, string ParentID)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Expected O, but got Unknown
		UltraTreeNode val = new UltraTreeNode(dr[IDCol].ToString(), dr[NoCol].ToString() + "  " + dr[GlobalVariables.IsArabic ? NameCol : NameEnCol].ToString());
		((SubObjectBase)val).Tag = dr;
		if ((bool)dr[IsMainCol])
		{
			val.Override.NodeAppearance.Image = Resources.folderfortree;
		}
		if (ParentID == "0")
		{
			treeChart.Nodes.Add(val);
		}
		else
		{
			treeChart.GetNodeByKey(ParentID).Nodes.Add(val);
		}
	}

	public virtual void HighlightNodes(UltraTreeNode node)
	{
		node.Override.NodeAppearance.BackColor = Color.RoyalBlue;
		node.Override.NodeAppearance.ForeColor = Color.White;
		if (node.Parent != null)
		{
			HighlightNodes(node.Parent);
		}
	}

	public virtual void ResetHighlighted(UltraTreeNode node)
	{
		node.Override.NodeAppearance.BackColor = treeChart.Appearance.BackColor;
		node.Override.NodeAppearance.ForeColor = treeChart.Appearance.ForeColor;
		if (node.Parent != null)
		{
			ResetHighlighted(node.Parent);
		}
	}

	private void treeChart_BeforeSelect(object sender, BeforeSelectEventArgs e)
	{
		if (((DisposableObjectCollectionBase)treeChart.SelectedNodes).Count > 0)
		{
			ResetHighlighted(treeChart.SelectedNodes[0]);
		}
	}

	public virtual void GetNodePath(UltraTreeNode node)
	{
		if (node.Parent != null)
		{
			GetNodePath(node.Parent);
		}
		UltraLabel obj = lblPath;
		((Control)(object)obj).Text = ((Control)(object)obj).Text + "   \n  " + ((DataRow)((SubObjectBase)node).Tag)[GlobalVariables.IsArabic ? NameCol : NameEnCol].ToString();
	}

	public virtual void cmCopyPast_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
	{
	}

	public virtual void txtCode_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	public virtual void txtCode_Leave(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			int num = LevelWidth(NodeLevel + (Adding ? 1 : 0));
			while (((Control)(object)txtCode).Text.Length < num)
			{
				((Control)(object)txtCode).Text = ((Control)(object)txtCode).Text.Insert(0, "0");
			}
		}
	}

	private void treeChart_KeyUp(object sender, KeyEventArgs e)
	{
		if (((DisposableObjectCollectionBase)treeChart.SelectedNodes).Count > 0 && e.KeyCode == Keys.Delete)
		{
			btnDeleteClick();
		}
	}

	private void txtName_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\'')
		{
			e.Handled = true;
		}
	}

	private void btnAttachFile_Click(object sender, EventArgs e)
	{
		btnAttachFileClick();
	}

	public virtual void btnAttachFileClick()
	{
		if (RowID != "")
		{
			DataRow dataRow = null;
			if (base.Tag != null)
			{
				dataRow = (DataRow)base.Tag;
			}
			else if (GlobalVariables.dtForms.Select("FormFullName = '" + GetType().Namespace + "." + GetType().Name + "'").Length != 0)
			{
				dataRow = GlobalVariables.dtForms.Select("FormFullName = '" + GetType().Namespace + "." + GetType().Name + "'")[0];
			}
			bool option = GlobalFunctions.GetOption("ArchivingInEnglish");
			string text = dataRow[option ? "FormNameEn" : "FormNameAr"].ToString();
			string text2 = GlobalVariables.dtForms.Select("formID=" + dataRow["ParentID"])[0]["ParentID"].ToString();
			string text3 = GlobalVariables.dtForms.Select("formID=" + text2)[0][option ? "FormNameEn" : "FormNameAr"].ToString();
			string text4 = text3 + "\\" + text + "\\";
			string path = text4 + ((Control)(object)txtCode).Text.Replace('\\', '-').Replace('/', '-').Replace('*', '-')
				.Replace('?', '-')
				.Replace('؟', '-')
				.Replace(':', '-')
				.Replace('<', '-')
				.Replace('>', '-')
				.Replace('"', '-') + "\\";
			frmAttachFiles frmAttachFiles2 = new frmAttachFiles(path, text4, RowID, ((Control)(object)btnUpdate).Enabled, ((Control)(object)btnDelete).Enabled);
			frmAttachFiles2.ShowDialog();
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
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Expected O, but got Unknown
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Expected O, but got Unknown
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Expected O, but got Unknown
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.AbstractForms.frmTree2));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		this.treeChart = new UltraTree();
		this.txtCode = new UltraTextEditor();
		this.txtName = new UltraTextEditor();
		this.lblPath = new UltraLabel();
		this.txtNameEn = new UltraTextEditor();
		this.btnAddRoot = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.lblTitle2 = new UltraLabel();
		this.label1 = new UltraLabel();
		this.label2 = new UltraLabel();
		this.label3 = new UltraLabel();
		this.btnAttachFile = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.treeChart).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.btnAdd, "btnAdd");
		resources.ApplyResources(base.btnUpdate, "btnUpdate");
		resources.ApplyResources(base.btnDelete, "btnDelete");
		resources.ApplyResources(base.btnPrint, "btnPrint");
		resources.ApplyResources(base.btnOK, "btnOK");
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		resources.ApplyResources(base.btnClose, "btnClose");
		resources.ApplyResources(base.btnSaveClose, "btnSaveClose");
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.lblHistory, "lblHistory");
		((AppearanceBase)val).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val, "appearance1");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		((AppearanceBase)val2).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val2).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val2).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val2).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val2, "appearance2");
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val2;
		base.cboTransactionBranch.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboTransactionBranch.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.treeChart, "treeChart");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val3).FontData.BoldAsString = resources.GetString("resource.BoldAsString2");
		((AppearanceBase)val3).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString2");
		((AppearanceBase)val3).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints");
		((AppearanceBase)val3).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString2");
		((AppearanceBase)val3).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString2");
		((AppearanceBase)val3).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val3, "appearance3");
		this.treeChart.Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.treeChart).Name = "treeChart";
		((UltraControlBase)this.treeChart).UseAppStyling = false;
		this.treeChart.AfterSelect += new AfterNodeSelectEventHandler(treeChart_AfterSelect);
		this.treeChart.BeforeSelect += new BeforeNodeSelectEventHandler(treeChart_BeforeSelect);
		((System.Windows.Forms.Control)(object)this.treeChart).KeyUp += new System.Windows.Forms.KeyEventHandler(treeChart_KeyUp);
		resources.ApplyResources(this.txtCode, "txtCode");
		((System.Windows.Forms.Control)(object)this.txtCode).Name = "txtCode";
		((System.Windows.Forms.Control)(object)this.txtCode).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCode_KeyPress);
		((System.Windows.Forms.Control)(object)this.txtCode).Leave += new System.EventHandler(txtCode_Leave);
		resources.ApplyResources(this.txtName, "txtName");
		((System.Windows.Forms.Control)(object)this.txtName).Name = "txtName";
		((System.Windows.Forms.Control)(object)this.txtName).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtName_KeyPress);
		resources.ApplyResources(this.lblPath, "lblPath");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val4).FontData.BoldAsString = resources.GetString("resource.BoldAsString3");
		((AppearanceBase)val4).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString3");
		((AppearanceBase)val4).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString3");
		((AppearanceBase)val4).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString3");
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Maroon;
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.lblPath).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.lblPath).Name = "lblPath";
		((UltraControlBase)this.lblPath).UseAppStyling = false;
		resources.ApplyResources(this.txtNameEn, "txtNameEn");
		((System.Windows.Forms.Control)(object)this.txtNameEn).Name = "txtNameEn";
		((System.Windows.Forms.Control)(object)this.txtNameEn).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtName_KeyPress);
		resources.ApplyResources(this.btnAddRoot, "btnAddRoot");
		((System.Windows.Forms.Control)(object)this.btnAddRoot).Name = "btnAddRoot";
		((System.Windows.Forms.Control)(object)this.btnAddRoot).Click += new System.EventHandler(btnAddRoot_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(34, 62, 110);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(79, 124, 165);
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val6).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.label1, "label1");
		this.label1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.label1).Name = "label1";
		resources.ApplyResources(this.label2, "label2");
		this.label2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.label2).Name = "label2";
		resources.ApplyResources(this.label3, "label3");
		this.label3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.label3).Name = "label3";
		resources.ApplyResources(this.btnAttachFile, "btnAttachFile");
		((AppearanceBase)val7).Image = ERP.Properties.Resources.Attach;
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.btnAttachFile).Appearance = (AppearanceBase)(object)val7;
		((ControlBase)this.btnAttachFile).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnAttachFile).Name = "btnAttachFile";
		((System.Windows.Forms.Control)(object)this.btnAttachFile).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnAttachFile).Click += new System.EventHandler(btnAttachFile_Click);
		base.AcceptButton = (System.Windows.Forms.IButtonControl)base.btnAdd;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAttachFile);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.label3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.label2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.label1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAddRoot);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.treeChart);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmTree2";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.treeChart, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAddRoot, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.label1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.label2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.label3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAttachFile, 0);
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.treeChart).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
