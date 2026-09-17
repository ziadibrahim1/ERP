using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Photos;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.Photos.MasterData;

public class frmStages : frmButtons
{
	private DataTable dtUsers = new DataTable();

	private DataTable dtStagesUsers = new DataTable();

	private DataRow drMaster;

	private IContainer components = null;

	public UltraButton btnCopyTo;

	public UltraButton btnSearch;

	public UltraButton btnPriveous;

	public UltraButton btnNext;

	public UltraTextEditor txtCode;

	public UltraLabel lblCode;

	public UltraLabel lblTitle;

	public UltraLabel lblTitle2;

	private UltraTextEditor txtEnglishName;

	private UltraLabel lblEnglishName;

	private UltraTextEditor txtArabicName;

	private UltraLabel lblArabicName;

	public UltraGroupBox UGBDetails;

	public UltraTree TreeUsers;

	protected internal UltraCheckEditor chkAll;

	public UltraButton btnUsersSearch;

	public UltraTextEditor txtUsers;

	private UltraTextEditor txtIndoorFolderPath;

	private UltraLabel lblIndoorFolderPath;

	private UltraButton btnIndoorFolderPath;

	private FolderBrowserDialog fbdPath;

	private UltraTextEditor txtOutdoorFolderPath;

	private UltraLabel lblOutdoorFolderPath;

	private UltraButton btnOutdoorFolderPath;

	public frmStages()
	{
		InitializeComponent();
		TableName = "PHO_Stages";
		IDCol = "StageID";
		NoCol = "StageNo";
		DateCol = "GetDate()";
	}

	public frmStages(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void SetSecurity()
	{
		base.SetSecurity();
		UltraButton obj = btnNext;
		UltraButton obj2 = btnPriveous;
		bool flag = (((Control)(object)btnSearch).Enabled = CanSearching);
		bool enabled = (((Control)(object)obj2).Enabled = flag);
		((Control)(object)obj).Enabled = enabled;
	}

	public override void CallButtons(KeyEventArgs e)
	{
		base.CallButtons(e);
		if (!Adding && !Updating)
		{
			if (e.KeyCode == Keys.F8 && ((Control)(object)btnSearch).Enabled && ((Control)(object)btnSearch).Visible)
			{
				btnSearch_Click(null, null);
			}
			else if (e.KeyValue == 39 && ((Control)(object)btnNext).Enabled && ((Control)(object)btnNext).Visible)
			{
				NextData();
			}
			else if (e.KeyValue == 37 && ((Control)(object)btnPriveous).Enabled && ((Control)(object)btnPriveous).Visible)
			{
				PriveousData();
			}
			else if (e.KeyCode == Keys.F7 && ((Control)(object)btnCopyTo).Enabled && ((Control)(object)btnCopyTo).Visible)
			{
				btnCopyToClick();
			}
		}
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtUsers = Users.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dtUsers != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeUsers, dtUsers, "User_ID", GlobalVariables.IsArabic ? "UserNameAr" : "UserNameEn");
		}
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = Stages.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			if (dataTable.Rows.Count > 0)
			{
				drMaster = dataTable.Rows[0];
			}
			else
			{
				drMaster = null;
			}
		}
		DisplayData();
	}

	public override void DisplayData()
	{
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Expected O, but got Unknown
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Expected O, but got Unknown
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Expected O, but got Unknown
		if (drMaster != null)
		{
			((Control)(object)txtCode).Text = drMaster["StageNo"].ToString();
			((Control)(object)txtArabicName).Text = drMaster["StageNameAr"].ToString();
			((Control)(object)txtEnglishName).Text = drMaster["StageNameEn"].ToString();
			((Control)(object)txtIndoorFolderPath).Text = drMaster["FolderPath"].ToString();
			((Control)(object)txtOutdoorFolderPath).Text = drMaster["OutdoorFolderPath"].ToString();
			dtStagesUsers = StagesUsers.SelectByStageID(drMaster["StageID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			((UltraToggleEditorBase)chkAll).Checked = false;
			TreeUsers.AfterCheck -= new AfterNodeChangedEventHandler(TreeUsers_AfterCheck);
			TreeUsers.BeforeCheck -= new BeforeCheckEventHandler(TreeUsers_BeforeCheck);
			TreeFunctions.SetAllTreeNodesCheckState(Checked: false, TreeUsers);
			SetCheckedSubAccounts(dtStagesUsers);
			TreeUsers.AfterCheck += new AfterNodeChangedEventHandler(TreeUsers_AfterCheck);
			TreeUsers.BeforeCheck -= new BeforeCheckEventHandler(TreeUsers_BeforeCheck);
		}
		else
		{
			ClearControls();
		}
		((TextEditorControlBase)txtCode).Focus();
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)txtArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEnglishName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtIndoorFolderPath).ReadOnly = NavMode;
		((EditorButtonControlBase)txtOutdoorFolderPath).ReadOnly = NavMode;
		((Control)(object)btnIndoorFolderPath).Visible = !NavMode;
		((Control)(object)btnOutdoorFolderPath).Visible = !NavMode;
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnUsersSearch).Visible = false;
		((Control)(object)chkAll).Enabled = !NavMode;
		((TextEditorControlBase)txtArabicName).Focus();
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtArabicName).Clear();
		((TextEditorControlBase)txtEnglishName).Clear();
		((TextEditorControlBase)txtIndoorFolderPath).Clear();
		((TextEditorControlBase)txtOutdoorFolderPath).Clear();
		((Control)(object)txtCode).Text = (Adding ? Stages.GetCode(IsFromServer: true) : "");
		TreeFunctions.SetAllTreeNodesCheckState(Checked: false, TreeUsers);
		((Control)(object)txtUsers).Text = "";
		((UltraToggleEditorBase)chkAll).Checked = false;
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال كود المرحلة", "Please Enter Stage Code");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال إسم المرحلة بالعربية", "Please Enter Stage Arabic Name");
			((TextEditorControlBase)txtArabicName).Focus();
			return false;
		}
		if (((Control)(object)txtIndoorFolderPath).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال مسار الملف داخلى  ", "Please Enter Indoor Folder Path");
			((TextEditorControlBase)txtIndoorFolderPath).Focus();
			return false;
		}
		if (((Control)(object)txtOutdoorFolderPath).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال مسار الملف خارجى  ", "Please Enter Outdoor Folder Path");
			((TextEditorControlBase)txtOutdoorFolderPath).Focus();
			return false;
		}
		if (Main.CheckForValue("PHO_Stages", "StageNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["StageNo"].ToString(), IsFromServer: true) > 0)
		{
			string code = Stages.GetCode(IsFromServer: true);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Voucher Number Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = code;
		}
		return base.ValidateData();
	}

	public string InsertUsers(string StageID)
	{
		string text = "";
		for (int i = 0; i < ((DisposableObjectCollectionBase)TreeUsers.Nodes).Count; i++)
		{
			if (TreeUsers.Nodes[i].CheckedState == CheckState.Checked)
			{
				text = text + " EXEC PHO_StagesUsers_Insert_Update -1," + ((KeyedSubObjectBase)TreeUsers.Nodes[i]).Key + "," + StageID + ",0," + GlobalVariables.CurrentBranchID + "," + GlobalVariables.UserID + "; ";
			}
		}
		return text;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			string text = InsertUsers(Stages.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, ((Control)(object)txtIndoorFolderPath).Text, ((Control)(object)txtOutdoorFolderPath).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true).ToString());
			if (text != "")
			{
				Main.SyncExecuteQuery_DataTable_Trans(text);
			}
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void UpdateData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = Stages.Insert_Update(drMaster["StageID"].ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, ((Control)(object)txtIndoorFolderPath).Text, ((Control)(object)txtOutdoorFolderPath).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			StagesUsers.DeleteByStageID(num.ToString(), GlobalVariables.UserID, IsFromServer: true);
			string text = InsertUsers(num.ToString());
			if (text != "")
			{
				Main.SyncExecuteQuery_DataTable_Trans(text);
			}
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			StagesUsers.DeleteByStageID(drMaster["StageID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			Stages.Delete(drMaster["StageID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحذف  " : "Error Occured");
		}
	}

	public override void btnPrintClick()
	{
	}

	public virtual void btnCopyToClick()
	{
		if (!CanAdd)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		Adding = true;
		drMaster = null;
		SetControls(NavMode: false);
	}

	public override void btnUpdateClick()
	{
		if (drMaster != null)
		{
			RowID = drMaster[IDCol].ToString();
			base.btnUpdateClick();
		}
	}

	public override void btnDeleteClick()
	{
		if (drMaster == null)
		{
			return;
		}
		RowID = drMaster[IDCol].ToString();
		if (!CanDelete)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		if (!CanModifyOtherBranch)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفرع آخر", "Cannot Update This Transaction Because It Related to Another Branch ");
			return;
		}
		if (ClosedPeriod)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Delete This Transaction Because It Related to ClosedPeriod ");
			return;
		}
		if (RowID == "1")
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه المرحلة", " Cannot Delete This Stage");
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Are You Sure You want to Delete this Data?");
		DataSaved = true;
		if (GlobalVariables.MessageBoxResult == 'Y')
		{
			DeleteData();
			if (DataSaved)
			{
				FillData();
				drMaster = null;
			}
		}
	}

	public override void btnCancelClick()
	{
		base.btnCancelClick();
		if (Adding)
		{
			drMaster = null;
		}
	}

	private void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.StagesSearchReport(IsFromServer: true);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["StageID"].ToString();
			FillData();
		}
	}

	private void btnPriveous_Click(object sender, EventArgs e)
	{
		PriveousData();
	}

	private void btnNext_Click(object sender, EventArgs e)
	{
		NextData();
	}

	public virtual void PriveousData()
	{
		if (dtSearchResult != null && dtSearchResult.Rows.Count > 1)
		{
			if (drMaster != null)
			{
				for (int i = 0; i < dtSearchResult.Rows.Count - 1; i++)
				{
					if (dtSearchResult.Rows[i][IDCol].ToString() == drMaster[IDCol].ToString())
					{
						RowID = dtSearchResult.Rows[i + 1][IDCol].ToString();
						FillData();
						break;
					}
				}
			}
			else
			{
				RowID = dtSearchResult.Rows[dtSearchResult.Rows.Count - 1][IDCol].ToString();
				FillData();
			}
		}
		else
		{
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, "", "0");
			if (dataTable.Rows.Count > 0)
			{
				RowID = dataTable.Rows[0][IDCol].ToString();
			}
			else
			{
				RowID = "";
			}
			FillData();
		}
	}

	public virtual void NextData()
	{
		if (dtSearchResult != null && dtSearchResult.Rows.Count > 1)
		{
			if (drMaster != null)
			{
				for (int i = 1; i < dtSearchResult.Rows.Count; i++)
				{
					if (dtSearchResult.Rows[i][IDCol].ToString() == drMaster[IDCol].ToString())
					{
						RowID = dtSearchResult.Rows[i - 1][IDCol].ToString();
						FillData();
						break;
					}
				}
			}
			else
			{
				RowID = dtSearchResult.Rows[0][IDCol].ToString();
				FillData();
			}
		}
		else
		{
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, "", "1");
			if (dataTable.Rows.Count > 0)
			{
				RowID = dataTable.Rows[0][IDCol].ToString();
			}
			else
			{
				RowID = "";
			}
			FillData();
		}
	}

	private void btnCopyTo_Click(object sender, EventArgs e)
	{
		btnCopyToClick();
	}

	private void txtCode_KeyUp(object sender, KeyEventArgs e)
	{
		if (!CanSearching || e.KeyCode != Keys.Return || TableName.Length <= 0 || NoCol.Length <= 0 || ((Control)(object)txtCode).Text.Length <= 0)
		{
			return;
		}
		if (Adding || Updating)
		{
			e.Handled = true;
			SendKeys.Send("{tab}");
			return;
		}
		DataTable comboData = Main.GetComboData(TableName, IDCol, NoCol + "=''" + ((Control)(object)txtCode).Text.Trim() + "'' And Deleted=0");
		if (comboData.Rows.Count > 0)
		{
			RowID = comboData.Rows[0][IDCol].ToString();
			dtSearchResult = null;
		}
		else
		{
			RowID = "";
		}
		FillData();
	}

	public override void btnRefreshDataClick()
	{
		dtUsers = Users.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dtUsers != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeUsers, dtUsers, "User_ID", GlobalVariables.IsArabic ? "UserNameAr" : "UserNameEn");
		}
	}

	private void TreeUsers_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		TreeUsers.AfterCheck -= new AfterNodeChangedEventHandler(TreeUsers_AfterCheck);
		TreeUsers.BeforeCheck -= new BeforeCheckEventHandler(TreeUsers_BeforeCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			TreeFunctions.SetParentCheckedState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAll).CheckedChanged -= chkAll_CheckedChanged;
		SetCheckBoxAllState(TreeUsers, chkAll);
		((UltraToggleEditorBase)chkAll).CheckedChanged += chkAll_CheckedChanged;
		TreeUsers.AfterCheck += new AfterNodeChangedEventHandler(TreeUsers_AfterCheck);
		TreeUsers.BeforeCheck += new BeforeCheckEventHandler(TreeUsers_BeforeCheck);
	}

	private void TreeUsers_BeforeCheck(object sender, BeforeCheckEventArgs e)
	{
		if (!Adding && !Updating)
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void chkAll_CheckedChanged(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		TreeUsers.AfterCheck -= new AfterNodeChangedEventHandler(TreeUsers_AfterCheck);
		TreeUsers.BeforeCheck -= new BeforeCheckEventHandler(TreeUsers_BeforeCheck);
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAll).Checked, TreeUsers);
		TreeUsers.AfterCheck += new AfterNodeChangedEventHandler(TreeUsers_AfterCheck);
		TreeUsers.BeforeCheck += new BeforeCheckEventHandler(TreeUsers_BeforeCheck);
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

	public void SetCheckedSubAccounts(DataTable dtStagesUsers)
	{
		for (int i = 0; i < dtStagesUsers.Rows.Count; i++)
		{
			UltraTreeNode nodeByKey = TreeUsers.GetNodeByKey(dtStagesUsers.Rows[i]["User_ID"].ToString());
			nodeByKey.CheckedState = CheckState.Checked;
			((UltraControlBase)TreeUsers).Update();
		}
		((UltraToggleEditorBase)chkAll).CheckedChanged -= chkAll_CheckedChanged;
		SetCheckBoxAllState(TreeUsers, chkAll);
		((UltraToggleEditorBase)chkAll).CheckedChanged += chkAll_CheckedChanged;
	}

	private void txtUsers_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtUsers);
		dataView.RowFilter = (GlobalVariables.IsArabic ? "UserNameAr" : "UserNameEn") + "  Like '%" + ((Control)(object)txtUsers).Text.Trim() + "%' ";
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TreeUsers.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			TreeUsers.ActiveNode = TreeUsers.GetNodeByKey(dataView.ToTable().Rows[0]["User_ID"].ToString());
		}
	}

	private void btnIndoorFolderPath_Click(object sender, EventArgs e)
	{
		if (fbdPath.ShowDialog() == DialogResult.OK)
		{
			((Control)(object)txtIndoorFolderPath).Text = fbdPath.SelectedPath + "\\";
		}
	}

	private void btnOutdoorFolderPath_Click(object sender, EventArgs e)
	{
		if (fbdPath.ShowDialog() == DialogResult.OK)
		{
			((Control)(object)txtOutdoorFolderPath).Text = fbdPath.SelectedPath + "\\";
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
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Expected O, but got Unknown
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Expected O, but got Unknown
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Expected O, but got Unknown
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Expected O, but got Unknown
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Expected O, but got Unknown
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Expected O, but got Unknown
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Expected O, but got Unknown
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Expected O, but got Unknown
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Expected O, but got Unknown
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Expected O, but got Unknown
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Expected O, but got Unknown
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Expected O, but got Unknown
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Expected O, but got Unknown
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Expected O, but got Unknown
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Expected O, but got Unknown
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Expected O, but got Unknown
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Expected O, but got Unknown
		//IL_0a05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0f: Expected O, but got Unknown
		//IL_0a1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a27: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Photos.MasterData.frmStages));
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
		Appearance val13 = new Appearance();
		Appearance val14 = new Appearance();
		this.btnCopyTo = new UltraButton();
		this.btnSearch = new UltraButton();
		this.btnPriveous = new UltraButton();
		this.btnNext = new UltraButton();
		this.txtCode = new UltraTextEditor();
		this.lblCode = new UltraLabel();
		this.lblTitle = new UltraLabel();
		this.lblTitle2 = new UltraLabel();
		this.txtEnglishName = new UltraTextEditor();
		this.lblEnglishName = new UltraLabel();
		this.txtArabicName = new UltraTextEditor();
		this.lblArabicName = new UltraLabel();
		this.UGBDetails = new UltraGroupBox();
		this.btnUsersSearch = new UltraButton();
		this.txtUsers = new UltraTextEditor();
		this.TreeUsers = new UltraTree();
		this.chkAll = new UltraCheckEditor();
		this.txtIndoorFolderPath = new UltraTextEditor();
		this.lblIndoorFolderPath = new UltraLabel();
		this.btnIndoorFolderPath = new UltraButton();
		this.fbdPath = new System.Windows.Forms.FolderBrowserDialog();
		this.txtOutdoorFolderPath = new UltraTextEditor();
		this.lblOutdoorFolderPath = new UltraLabel();
		this.btnOutdoorFolderPath = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtUsers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeUsers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtIndoorFolderPath).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOutdoorFolderPath).BeginInit();
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
		resources.ApplyResources(val, "appearance14");
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
		resources.ApplyResources(this.btnCopyTo, "btnCopyTo");
		((System.Windows.Forms.Control)(object)this.btnCopyTo).Name = "btnCopyTo";
		((System.Windows.Forms.Control)(object)this.btnCopyTo).Click += new System.EventHandler(btnCopyTo_Click);
		resources.ApplyResources(this.btnSearch, "btnSearch");
		((AppearanceBase)val3).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val3, "appearance15");
		((ControlBase)this.btnSearch).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.btnSearch).Name = "btnSearch";
		((System.Windows.Forms.Control)(object)this.btnSearch).Click += new System.EventHandler(btnSearch_Click);
		resources.ApplyResources(this.btnPriveous, "btnPriveous");
		((AppearanceBase)val4).Image = ERP.Properties.Resources.BarLeft;
		resources.ApplyResources(val4, "appearance16");
		((ControlBase)this.btnPriveous).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.btnPriveous).Name = "btnPriveous";
		((System.Windows.Forms.Control)(object)this.btnPriveous).Click += new System.EventHandler(btnPriveous_Click);
		resources.ApplyResources(this.btnNext, "btnNext");
		((AppearanceBase)val5).Image = ERP.Properties.Resources.BarRight;
		resources.ApplyResources(val5, "appearance17");
		((ControlBase)this.btnNext).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.btnNext).Name = "btnNext";
		((System.Windows.Forms.Control)(object)this.btnNext).Click += new System.EventHandler(btnNext_Click);
		resources.ApplyResources(this.txtCode, "txtCode");
		((System.Windows.Forms.Control)(object)this.txtCode).Name = "txtCode";
		((System.Windows.Forms.Control)(object)this.txtCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtCode_KeyUp);
		resources.ApplyResources(this.lblCode, "lblCode");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val6).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val6, "appearance18");
		((ControlBase)this.lblCode).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.lblCode).Name = "lblCode";
		((UltraControlBase)this.lblCode).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val7).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val7, "appearance19");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val8).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val8, "appearance20");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.txtEnglishName, "txtEnglishName");
		((System.Windows.Forms.Control)(object)this.txtEnglishName).Name = "txtEnglishName";
		resources.ApplyResources(this.lblEnglishName, "lblEnglishName");
		this.lblEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEnglishName).Name = "lblEnglishName";
		((ControlBase)this.lblEnglishName).WrapText = false;
		resources.ApplyResources(this.txtArabicName, "txtArabicName");
		((System.Windows.Forms.Control)(object)this.txtArabicName).Name = "txtArabicName";
		resources.ApplyResources(this.lblArabicName, "lblArabicName");
		this.lblArabicName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArabicName).Name = "lblArabicName";
		((ControlBase)this.lblArabicName).WrapText = false;
		resources.ApplyResources(this.UGBDetails, "UGBDetails");
		this.UGBDetails.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.btnUsersSearch);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtUsers);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.TreeUsers);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.chkAll);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Name = "UGBDetails";
		resources.ApplyResources(this.btnUsersSearch, "btnUsersSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance21");
		((ControlBase)this.btnUsersSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnUsersSearch).Name = "btnUsersSearch";
		resources.ApplyResources(this.txtUsers, "txtUsers");
		((System.Windows.Forms.Control)(object)this.txtUsers).Name = "txtUsers";
		((TextEditorControlBase)this.txtUsers).ValueChanged += new System.EventHandler(txtUsers_ValueChanged);
		resources.ApplyResources(this.TreeUsers, "TreeUsers");
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance10");
		this.TreeUsers.Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.TreeUsers).Name = "TreeUsers";
		val11.NodeStyle = (NodeStyle)1;
		this.TreeUsers.Override = val11;
		((UltraControlBase)this.TreeUsers).UseAppStyling = false;
		this.TreeUsers.AfterCheck += new AfterNodeChangedEventHandler(TreeUsers_AfterCheck);
		this.TreeUsers.BeforeCheck += new BeforeCheckEventHandler(TreeUsers_BeforeCheck);
		resources.ApplyResources(this.chkAll, "chkAll");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance22");
		((UltraToggleEditorBase)this.chkAll).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.chkAll).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAll).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAll).Name = "chkAll";
		((UltraControlBase)this.chkAll).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAll).CheckedChanged += new System.EventHandler(chkAll_CheckedChanged);
		resources.ApplyResources(this.txtIndoorFolderPath, "txtIndoorFolderPath");
		((System.Windows.Forms.Control)(object)this.txtIndoorFolderPath).Name = "txtIndoorFolderPath";
		resources.ApplyResources(this.lblIndoorFolderPath, "lblIndoorFolderPath");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val13, "appearance23");
		((ControlBase)this.lblIndoorFolderPath).Appearance = (AppearanceBase)(object)val13;
		this.lblIndoorFolderPath.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblIndoorFolderPath).Name = "lblIndoorFolderPath";
		((ControlBase)this.lblIndoorFolderPath).WrapText = false;
		resources.ApplyResources(this.btnIndoorFolderPath, "btnIndoorFolderPath");
		((System.Windows.Forms.Control)(object)this.btnIndoorFolderPath).Name = "btnIndoorFolderPath";
		((System.Windows.Forms.Control)(object)this.btnIndoorFolderPath).Click += new System.EventHandler(btnIndoorFolderPath_Click);
		resources.ApplyResources(this.fbdPath, "fbdPath");
		resources.ApplyResources(this.txtOutdoorFolderPath, "txtOutdoorFolderPath");
		((System.Windows.Forms.Control)(object)this.txtOutdoorFolderPath).Name = "txtOutdoorFolderPath";
		resources.ApplyResources(this.lblOutdoorFolderPath, "lblOutdoorFolderPath");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val14, "appearance24");
		((ControlBase)this.lblOutdoorFolderPath).Appearance = (AppearanceBase)(object)val14;
		this.lblOutdoorFolderPath.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOutdoorFolderPath).Name = "lblOutdoorFolderPath";
		((ControlBase)this.lblOutdoorFolderPath).WrapText = false;
		resources.ApplyResources(this.btnOutdoorFolderPath, "btnOutdoorFolderPath");
		((System.Windows.Forms.Control)(object)this.btnOutdoorFolderPath).Name = "btnOutdoorFolderPath";
		((System.Windows.Forms.Control)(object)this.btnOutdoorFolderPath).Click += new System.EventHandler(btnOutdoorFolderPath_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtOutdoorFolderPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOutdoorFolderPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOutdoorFolderPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtIndoorFolderPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblIndoorFolderPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnIndoorFolderPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBDetails);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCopyTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPriveous);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNext);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmStages";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnIndoorFolderPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblIndoorFolderPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtIndoorFolderPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOutdoorFolderPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOutdoorFolderPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtOutdoorFolderPath, 0);
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBDetails).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.UGBDetails).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtUsers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeUsers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtIndoorFolderPath).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOutdoorFolderPath).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
