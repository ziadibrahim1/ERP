using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.MarineService;
using ERP.AbstractForms;
using ERP.Accounting.MasterData;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;
using Infragistics.Win.UltraWinTree;

namespace ERP.MarineService.MasterData;

public class frmChartersTree : frmTree2
{
	private DataTable dtAccounts;

	private DataTable dtSubAccountType;

	private DataTable dtTitle;

	private DataTable dtSubAccDetails;

	private DataTable dtCities;

	private DataTable dtAreas;

	private DataTable dtBranchs;

	private DataTable dtNationality;

	private DataTable dtCountries;

	private ValueList vlPosition = new ValueList();

	private string CharterID = "-1";

	private IContainer components = null;

	public UltraButton btnItemsSearch;

	private UltraTextEditor txtItems;

	public UltraTree TreeAccounts;

	public UltraTabControl tabItemType;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl tabItem;

	private UltraComboEditor cboType;

	private UltraLabel lblType;

	private UltraTextEditor txtFax;

	private UltraTabPageControl tabService;

	private UltraLabel lblAccounts;

	private UltraLabel lblBirthDate;

	private UltraDateTimeEditor dtpBirthDate;

	private UltraTextEditor txtNo;

	private UltraLabel ultraLabel1;

	private UltraComboEditor cboCity;

	private UltraLabel lblCity;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem changeParentTSMenu;

	private ToolStripMenuItem setAsGroupToolStripMenuItem;

	private ToolStripMenuItem setAsSubAccountToolStripMenuItem;

	private UltraLabel lblArea;

	private UltraLabel lblBranch;

	private UltraComboEditor cboBranch;

	private UltraLabel lblAddress;

	private UltraTextEditor txtAddress;

	private UltraComboEditor cboArea;

	private UltraTextEditor txtNotes;

	private UltraLabel lblNotes;

	private UltraLabel lblMin;

	private UltraLabel lblReorder;

	private UltraLabel lblEMail;

	private UltraLabel lblMax;

	private UltraTextEditor txtMobile;

	private UltraTextEditor txtEMail;

	private UltraLabel lblClintSupplierTitle;

	private UltraTextEditor txtTel;

	private UltraComboEditor cboTitle;

	private UltraComboEditor cboCountry;

	private UltraLabel lblCountry;

	private UltraComboEditor cboNationality;

	private UltraLabel lblNationality;

	private UltraLabel lblMobile2;

	private UltraTextEditor txtMobile2;

	private UltraComboEditor cboDefaultAcc;

	private UltraLabel lblClientAccount;

	private UltraLabel lblPassportNo;

	private UltraTextEditor txtPassportNo;

	private UltraLabel lblCDCExpireDate;

	private UltraDateTimeEditor dtpCDCExpireDate;

	private UltraLabel lblCDCIssueDate;

	private UltraDateTimeEditor dtpCDCIssueDate;

	private UltraLabel lblCDCNo;

	private UltraTextEditor txtCDCNo;

	private UltraLabel lblPassportExpireDate;

	private UltraDateTimeEditor dtpPassportExpireDate;

	private UltraLabel lblPassportIssueDate;

	private UltraDateTimeEditor dtpPassportIssueDate;

	private UltraLabel lblIDNo;

	private UltraTextEditor txtIDNo;

	private UltraLabel lblIDIssueDate;

	private UltraDateTimeEditor dtpIDIssueDate;

	private UltraTextEditor txtTaxNo;

	private UltraLabel lblTaxNo;

	public frmChartersTree()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		IDCol = "SubAccountID";
		NoCol = "SubAccountNumber";
		NameCol = "SubAccountNameAr";
		NameEnCol = "SubAccountNameEn";
		ParentIDCol = "ParentID";
		IsMainCol = "IsMain";
		AdditionalCol1 = "SubAccountTypeID";
		AdditionalCol2 = "BranchID";
		ItemLevelCol = "LevelID";
		TableName = "A_SubAccounts";
		LevelsTable = "A_SubAccounts_Levels";
		LevelsCol = "LevelID";
		LevelsWidthCol = "Width";
		AllowAddRoot = false;
	}

	public override void PrepareData()
	{
		if (TableName != "" && LevelsTable != "")
		{
			dtLevels = Main.SyncExecuteQuery_DataTable("TreeLevels_Select '" + LevelsCol + "','" + LevelsWidthCol + "','" + LevelsTable + "'");
			dtChart = SubAccounts.FillTreeBySubAccountTypeIDs(GlobalVariables.CharterSubAccountTypeIDs, "-1", IsFromServer: true);
			treeChart.Nodes.Clear();
			if (dtChart.Rows.Count > 0)
			{
				FillTree("0");
			}
		}
		dtAccounts = Accounts.Select("-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		TreeFunctions.FillTree(TreeAccounts, dtAccounts, "ParentID", "AccountID", GlobalVariables.IsArabic ? "AccountNameAr" : "AccountNameEn", "AccountNumber", "IsMain");
		dtSubAccountType = SuAccountsTypes.SelectBySuAccountsTypeIDs(GlobalVariables.CharterSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0");
		GlobalFunctions.FillCombo(cboType, dtSubAccountType, "SubAccountTypeID", GlobalVariables.IsArabic ? "SubAccountTypeNameAr" : "SubAccountTypeNameEn");
		dtTitle = Titles.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboTitle, dtTitle, "TitleID", GlobalVariables.IsArabic ? "TitleNameAr" : "TitleNameEn");
		dtCities = Cities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCity, dtCities, "CityID", "CityName");
		dtAreas = Areas.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboArea, dtAreas, "AreaID", "AreaName");
		dtBranchs = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboBranch, dtBranchs, "BranchID", "BranchName");
		dtCountries = Countries.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCountry, dtCountries, "CountryID", "CountryName");
		dtNationality = Nationalities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboNationality, dtNationality, "NationalityID", "NationalityName");
		dtSubAccDetails = SubAccounts_Details.SelectBySubAccountIDWithAccountName("0", "1", IsFromServer: true);
	}

	public override void ClearControls()
	{
		base.ClearControls();
		if (Adding)
		{
			((Control)(object)txtCode).Text = SubAccounts.GetCode((SelectedNode == null) ? "Null" : ((KeyedSubObjectBase)SelectedNode).Key, IsFromServer: true);
			((Control)(object)txtNo).Text = Charters.GetCode(IsFromServer: true);
			((TextEditorControlBase)cboType).Value = 10;
			((Control)(object)txtName).Select();
		}
		((TextEditorControlBase)txtFax).Clear();
		((TextEditorControlBase)txtAddress).Clear();
		((TextEditorControlBase)txtEMail).Clear();
		((TextEditorControlBase)txtMobile).Clear();
		((TextEditorControlBase)txtTel).Clear();
		((TextEditorControlBase)txtMobile2).Clear();
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)txtPassportNo).Clear();
		((TextEditorControlBase)txtCDCNo).Clear();
		((TextEditorControlBase)txtIDNo).Clear();
		((TextEditorControlBase)txtTaxNo).Clear();
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)cboTitle).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpBirthDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAddress).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTel).ReadOnly = NavMode;
		((EditorButtonControlBase)txtFax).ReadOnly = NavMode;
		((EditorButtonControlBase)txtMobile).ReadOnly = NavMode;
		((EditorButtonControlBase)txtMobile2).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPassportNo).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpPassportIssueDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpPassportExpireDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpIDIssueDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCDCNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtIDNo).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpCDCIssueDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpCDCExpireDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEMail).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTaxNo).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCountry).ReadOnly = NavMode;
		((EditorButtonControlBase)cboNationality).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCity).ReadOnly = NavMode;
		((EditorButtonControlBase)cboArea).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBranch).ReadOnly = NavMode;
		((EditorButtonControlBase)cboDefaultAcc).ReadOnly = NavMode;
		((Control)(object)TreeAccounts).Visible = !NavMode;
		((Control)(object)txtItems).Visible = !NavMode;
		((Control)(object)btnItemsSearch).Visible = !NavMode;
		((Control)(object)lblAccounts).Visible = NavMode;
		if (SelectedNode != null && (Adding || Updating))
		{
			TreeAccounts.CollapseAll();
			TreeFunctions.SetAllTreeNodesCheckState(Checked: false, TreeAccounts);
			DataTable dataTable = SubAccounts_Details.SelectBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, "1", IsFromServer: true);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				TreeFunctions.SetNodeCheckState(CheckState.Checked, dataTable.Rows[i]["AccountID"].ToString(), TreeAccounts);
			}
		}
	}

	public override void DisplayData()
	{
		CharterID = "-1";
		base.DisplayData();
		if (SelectedNode != null)
		{
			cboTitle.SelectedIndex = -1;
			dtpBirthDate.DateTime = DateTime.Now;
			dtpPassportIssueDate.DateTime = DateTime.Now;
			dtpPassportExpireDate.DateTime = DateTime.Now;
			dtpCDCIssueDate.DateTime = DateTime.Now;
			dtpCDCExpireDate.DateTime = DateTime.Now;
			dtpIDIssueDate.DateTime = DateTime.Now;
			((TextEditorControlBase)txtAddress).Clear();
			((TextEditorControlBase)txtTel).Clear();
			((TextEditorControlBase)txtFax).Clear();
			((TextEditorControlBase)txtMobile).Clear();
			((TextEditorControlBase)txtMobile2).Clear();
			((TextEditorControlBase)txtEMail).Clear();
			((TextEditorControlBase)txtNotes).Clear();
			((TextEditorControlBase)txtNo).Clear();
			((TextEditorControlBase)txtTaxNo).Clear();
			((TextEditorControlBase)txtPassportNo).Clear();
			((TextEditorControlBase)txtCDCNo).Clear();
			((TextEditorControlBase)txtIDNo).Clear();
			((TextEditorControlBase)cboCity).Clear();
			((TextEditorControlBase)cboArea).Clear();
			((TextEditorControlBase)cboCountry).Clear();
			((TextEditorControlBase)cboNationality).Clear();
			((TextEditorControlBase)cboDefaultAcc).Clear();
			((TextEditorControlBase)cboType).Value = ((DataRow)((SubObjectBase)SelectedNode).Tag)[AdditionalCol1].ToString();
			((TextEditorControlBase)cboBranch).Value = ((DataRow)((SubObjectBase)SelectedNode).Tag)[AdditionalCol2].ToString();
			changeParentTSMenu.Enabled = !Convert.ToBoolean(((DataRow)((SubObjectBase)SelectedNode).Tag)[IsMainCol]);
			setAsGroupToolStripMenuItem.Enabled = !Convert.ToBoolean(((DataRow)((SubObjectBase)SelectedNode).Tag)[IsMainCol]);
			setAsSubAccountToolStripMenuItem.Enabled = ((DisposableObjectCollectionBase)SelectedNode.Nodes).Count == 0 && Convert.ToBoolean(((DataRow)((SubObjectBase)SelectedNode).Tag)[IsMainCol]);
			((Control)(object)lblAccounts).Text = "";
			dtSubAccDetails = SubAccounts_Details.SelectBySubAccountIDWithAccountName(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			for (int i = 0; i < dtSubAccDetails.Rows.Count; i++)
			{
				UltraLabel obj = lblAccounts;
				((Control)(object)obj).Text = string.Concat(((Control)(object)obj).Text, dtSubAccDetails.Rows[i]["AccountName"], " \n");
			}
			GlobalFunctions.FillCombo(cboDefaultAcc, dtSubAccDetails, "AccountID", "AccountName");
			if (cboType.SelectedIndex == 0)
			{
				DisplayCharterData();
			}
		}
	}

	public void DisplayCharterData()
	{
		CharterID = "-1";
		DataTable dataTable = Charters.SelectBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.Rows[0];
			CharterID = dataRow["CharterID"].ToString();
			((TextEditorControlBase)cboTitle).Value = dataRow["TitleID"];
			dtpBirthDate.Value = dataRow["BirthDate"];
			((Control)(object)txtNo).Text = dataRow["CharterNo"].ToString();
			((Control)(object)txtAddress).Text = dataRow["Address"].ToString();
			((Control)(object)txtTel).Text = dataRow["Tel"].ToString();
			((Control)(object)txtFax).Text = dataRow["Fax"].ToString();
			((Control)(object)txtMobile).Text = dataRow["Mobile"].ToString();
			((Control)(object)txtMobile2).Text = dataRow["Mobile2"].ToString();
			((Control)(object)txtEMail).Text = dataRow["EMail"].ToString();
			((TextEditorControlBase)cboCountry).Value = dataRow["CountryID"].ToString();
			((TextEditorControlBase)cboNationality).Value = dataRow["NationalityID"].ToString();
			((TextEditorControlBase)cboCity).Value = dataRow["CityID"].ToString();
			((TextEditorControlBase)cboArea).Value = dataRow["AreaID"].ToString();
			((TextEditorControlBase)cboDefaultAcc).Value = dataRow["DefaultAccountID"].ToString();
			((Control)(object)txtNotes).Text = dataRow["Notes"].ToString();
			((Control)(object)txtPassportNo).Text = dataRow["PassportNo"].ToString();
			dtpPassportIssueDate.Value = dataRow["PassportIssueDate"];
			dtpPassportExpireDate.Value = dataRow["PassportExpireDate"];
			((Control)(object)txtCDCNo).Text = dataRow["CDCNo"].ToString();
			dtpCDCIssueDate.Value = dataRow["CDCIssueDate"];
			dtpCDCExpireDate.Value = dataRow["CDCExpireDate"];
			((Control)(object)txtIDNo).Text = dataRow["IDNo"].ToString();
			((Control)(object)txtTaxNo).Text = dataRow["TaxNo"].ToString();
			dtpIDIssueDate.Value = dataRow["IDIssueDate"];
		}
	}

	public override int TreeAddData()
	{
		int result = 0;
		Main.StartBulkTrans(FromServer: true);
		try
		{
			result = SubAccounts.Insert_Update("-1", GetCode(), ((Control)(object)txtName).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? "Null" : ((Control)(object)txtNameEn).Text.Trim(), (SelectedNode == null) ? "Null" : ((DataRow)((SubObjectBase)SelectedNode).Tag)[0].ToString(), "0", (NodeLevel + 1).ToString(), ((TextEditorControlBase)cboType).Value.ToString(), "1", "0", (cboBranch.SelectedIndex == -1) ? GlobalVariables.CurrentBranchID : ((TextEditorControlBase)cboBranch).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			SubAccounts_Details.Insert_UpdateByAccIDs(result.ToString(), TreeFunctions.GetTreeCheckedNodesIDs(TreeAccounts), "0", GlobalVariables.CurrentBranchID, IsFromServer: true);
			Charters.Insert_Update("-1", (((Control)(object)txtNo).Text == "") ? GetCode() : ((Control)(object)txtNo).Text, result.ToString(), (cboTitle.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTitle).Value.ToString(), dtpBirthDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboCountry.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCountry).Value.ToString(), (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), (cboNationality.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboNationality).Value.ToString(), ((Control)(object)txtPassportNo).Text, dtpPassportIssueDate.DateTime.ToString(GlobalVariables.DateShortFormate), dtpPassportExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtCDCNo).Text, dtpCDCIssueDate.DateTime.ToString(GlobalVariables.DateShortFormate), dtpCDCExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtIDNo).Text, dtpIDIssueDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtAddress).Text, ((Control)(object)txtTel).Text, ((Control)(object)txtFax).Text, ((Control)(object)txtMobile).Text, ((Control)(object)txtMobile2).Text, ((Control)(object)txtEMail).Text, (cboDefaultAcc.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDefaultAcc).Value.ToString(), ((Control)(object)txtTaxNo).Text, ((Control)(object)txtNotes).Text, "0", (cboBranch.SelectedIndex == -1) ? GlobalVariables.CurrentBranchID : ((TextEditorControlBase)cboBranch).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
			ClearControls();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
		return result;
	}

	public override void TreeUpdateData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			SubAccounts.Insert_Update(((KeyedSubObjectBase)SelectedNode).Key, GetCode(), ((Control)(object)txtName).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? "Null" : ((Control)(object)txtNameEn).Text.Trim(), (SelectedNode.Parent == null) ? "Null" : ((KeyedSubObjectBase)SelectedNode.Parent).Key, ((bool)((DataRow)((SubObjectBase)SelectedNode).Tag)[IsMainCol]) ? "1" : "0", NodeLevel.ToString(), ((TextEditorControlBase)cboType).Value.ToString(), "1", "0", (cboBranch.SelectedIndex == -1) ? GlobalVariables.CurrentBranchID : ((TextEditorControlBase)cboBranch).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			SubAccounts_Details.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			string treeCheckedNodesIDs = TreeFunctions.GetTreeCheckedNodesIDs(TreeAccounts);
			SubAccounts_Details.Insert_UpdateByAccIDs(((KeyedSubObjectBase)SelectedNode).Key, treeCheckedNodesIDs, "0", GlobalVariables.CurrentBranchID, IsFromServer: true);
			if (((DisposableObjectCollectionBase)SelectedNode.Nodes).Count > 0)
			{
				GlobalVariables.QuestionMB.Show("هل تريد ربط كل محتويات المجموعه بنفس الحسابات؟", "Would you like to Bind All Group Contents With The Same Accounts?");
				if (GlobalVariables.MessageBoxResult == 'Y')
				{
					SaveAccountsToChildNades(SelectedNode, treeCheckedNodesIDs);
				}
			}
			Charters.Insert_Update(CharterID, (((Control)(object)txtNo).Text == "") ? GetCode() : ((Control)(object)txtNo).Text, ((KeyedSubObjectBase)SelectedNode).Key, (cboTitle.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTitle).Value.ToString(), dtpBirthDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboCountry.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCountry).Value.ToString(), (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), (cboNationality.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboNationality).Value.ToString(), ((Control)(object)txtPassportNo).Text, dtpPassportIssueDate.DateTime.ToString(GlobalVariables.DateShortFormate), dtpPassportExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtCDCNo).Text, dtpCDCIssueDate.DateTime.ToString(GlobalVariables.DateShortFormate), dtpCDCExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtIDNo).Text, dtpIDIssueDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtAddress).Text, ((Control)(object)txtTel).Text, ((Control)(object)txtFax).Text, ((Control)(object)txtMobile).Text, ((Control)(object)txtMobile2).Text, ((Control)(object)txtEMail).Text, (cboDefaultAcc.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDefaultAcc).Value.ToString(), ((Control)(object)txtTaxNo).Text, ((Control)(object)txtNotes).Text, "0", (cboBranch.SelectedIndex == -1) ? GlobalVariables.CurrentBranchID : ((TextEditorControlBase)cboBranch).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
			ClearControls();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void SaveAccountsToChildNades(UltraTreeNode Node, string AccountIDs)
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)Node.Nodes).Count; i++)
		{
			SubAccounts_Details.DeleteBySubAccountID(((KeyedSubObjectBase)Node.Nodes[i]).Key, GlobalVariables.UserID, IsFromServer: true);
			SubAccounts_Details.Insert_UpdateByAccIDs(((KeyedSubObjectBase)Node.Nodes[i]).Key, AccountIDs, "0", GlobalVariables.CurrentBranchID, IsFromServer: true);
			if (((DisposableObjectCollectionBase)Node.Nodes[i].Nodes).Count > 0)
			{
				SaveAccountsToChildNades(Node.Nodes[i], AccountIDs);
			}
		}
	}

	public override void TreeDeleteData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			Charters.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			SubAccounts_Details.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			SubAccounts.Delete(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override bool ValidateData()
	{
		if (cboDefaultAcc.SelectedIndex == -1 && cboType.SelectedIndex == 0)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل الحساب الإفتراضي للمستأجر", "Enter Default Charterer Account");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Charter"];
			((TextEditorControlBase)cboDefaultAcc).Focus();
			return false;
		}
		if (cboType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك نوع الحساب ", "Enter SubAccount Type");
			((TextEditorControlBase)cboType).Focus();
			return false;
		}
		if (((Control)(object)cboBranch).Visible && cboBranch.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك الفرع ", "Enter Branch");
			((TextEditorControlBase)cboBranch).Focus();
			return false;
		}
		return base.ValidateData();
	}

	public override bool HasTransactionValidation()
	{
		string text = SubAccounts.SelectRelations(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true).Rows[0]["Relations"].ToString().Replace("-", "\n");
		if (text != "")
		{
			GlobalVariables.InformationMB.Show(text, text);
			return true;
		}
		return base.HasTransactionValidation();
	}

	private void Tree_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Expected O, but got Unknown
		((UltraTree)sender).AfterCheck -= new AfterNodeChangedEventHandler(Tree_AfterCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			TreeFunctions.SetParentCheckedState(e.TreeNode.Parent);
		}
		string treeCheckedNodesIDs = TreeFunctions.GetTreeCheckedNodesIDs(TreeAccounts);
		if (treeCheckedNodesIDs != ",")
		{
			string[] array = treeCheckedNodesIDs.TrimEnd(',').TrimStart(',').Split(',');
			dtSubAccDetails.Rows.Clear();
			for (int j = 0; j < array.Length; j++)
			{
				DataRow dataRow = dtSubAccDetails.NewRow();
				DataRow dataRow2 = dtAccounts.Select("AccountID=" + array[j])[0];
				dataRow["AccountID"] = dataRow2["AccountID"];
				dataRow["AccountName"] = dataRow2[GlobalVariables.IsArabic ? "AccountNameAr" : "AccountNameEn"];
				dtSubAccDetails.Rows.Add(dataRow);
			}
			GlobalFunctions.FillCombo(cboDefaultAcc, dtSubAccDetails, "AccountID", "AccountName");
		}
		((UltraTree)sender).AfterCheck += new AfterNodeChangedEventHandler(Tree_AfterCheck);
	}

	private void txtItems_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtAccounts);
		dataView.RowFilter = (GlobalVariables.IsArabic ? "AccountNameAr" : "AccountNameEn") + " Like '%" + ((Control)(object)txtItems).Text.Trim() + "%' ";
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TreeAccounts.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			TreeAccounts.ActiveNode = TreeAccounts.GetNodeByKey(dataView.ToTable().Rows[0]["AccountID"].ToString());
		}
	}

	public override void TreeSearch()
	{
		int num = SearchFunctions.ChartersSearch("-1", IsFromServer: true);
		if (num != 0)
		{
			treeChart.CollapseAll();
			treeChart.ActiveNode = treeChart.GetNodeByKey(num.ToString());
			treeChart.GetNodeByKey(num.ToString()).Selected = true;
		}
	}

	private void btnItemsSearch_Click(object sender, EventArgs e)
	{
		DataTable dataTable = SearchFunctions.AccountsReport(IsFromServer: true);
		if (dataTable.Rows.Count > 0)
		{
			TreeAccounts.CollapseAll();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				TreeAccounts.GetNodeByKey(dataTable.Rows[i]["AccountID"].ToString()).CheckedState = CheckState.Checked;
			}
		}
	}

	public override void btnPrintClick()
	{
		GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_MS_Charters_A.rpt" : "Rep_MS_Charters_E.rpt"));
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
	}

	private void cboType_ValueChanged(object sender, EventArgs e)
	{
	}

	private void cboCity_ValueChanged(object sender, EventArgs e)
	{
		if (cboCity.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtAreas);
			dataView.RowFilter = "CityID=" + ((TextEditorControlBase)cboCity).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboArea.DataSource = dataView;
			cboArea.DisplayMember = "AreaName";
			cboArea.ValueMember = "AreaID";
		}
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void changeParentTSMenu_Click(object sender, EventArgs e)
	{
		if (SelectedNode != null)
		{
			ERP.Accounting.MasterData.frmUpdateParent frmUpdateParent2 = new ERP.Accounting.MasterData.frmUpdateParent(((KeyedSubObjectBase)SelectedNode).Key, (SelectedNode.Parent != null) ? ((KeyedSubObjectBase)SelectedNode.Parent).Key : "");
			((Control)(object)frmUpdateParent2.lblTitle).Text = (GlobalVariables.IsArabic ? "تغيير المجموعه" : "Update Group");
			frmUpdateParent2.ShowDialog();
			btnRefreshDataClick();
		}
	}

	private void setAsGroupToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!HasTransactionValidation())
		{
			SubAccounts.UpdateIsMain(((KeyedSubObjectBase)SelectedNode).Key, "1", GlobalVariables.UserID, IsFromServer: true);
			DataRow dataRow = dtChart.Select(IDCol + "=" + ((KeyedSubObjectBase)SelectedNode).Key)[0];
			dataRow[IsMainCol] = 1;
			((SubObjectBase)SelectedNode).Tag = dataRow;
			SelectedNode.Override.NodeAppearance.Image = Resources.folderfortree;
			SelectedNode.ExpandAll();
			changeParentTSMenu.Enabled = false;
			setAsGroupToolStripMenuItem.Enabled = false;
			setAsSubAccountToolStripMenuItem.Enabled = true;
		}
	}

	private void setAsSubAccountToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (((DisposableObjectCollectionBase)SelectedNode.Nodes).Count > 0)
		{
			GlobalVariables.InformationMB.Show(" لايمكن تحويله لحساب تحليلي لوجود عناصر تحته", "Cannot set this Node as SubAccount It Has Sub Nodes");
			return;
		}
		SubAccounts.UpdateIsMain(((KeyedSubObjectBase)SelectedNode).Key, "0", GlobalVariables.UserID, IsFromServer: true);
		DataRow dataRow = dtChart.Select(IDCol + "=" + ((KeyedSubObjectBase)SelectedNode).Key)[0];
		dataRow[IsMainCol] = 0;
		((SubObjectBase)SelectedNode).Tag = dataRow;
		SelectedNode.Override.NodeAppearance.Image = null;
		SelectedNode.ExpandAll();
		changeParentTSMenu.Enabled = true;
		setAsGroupToolStripMenuItem.Enabled = true;
		setAsSubAccountToolStripMenuItem.Enabled = false;
	}

	private void cboCountry_ValueChanged(object sender, EventArgs e)
	{
		if (cboCountry.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtCities);
			dataView.RowFilter = "CountryID=" + ((TextEditorControlBase)cboCountry).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboCity.DataSource = dataView;
			cboCity.DisplayMember = "CityName";
			cboCity.ValueMember = "CityID";
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
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Expected O, but got Unknown
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Expected O, but got Unknown
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Expected O, but got Unknown
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected O, but got Unknown
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Expected O, but got Unknown
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Expected O, but got Unknown
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Expected O, but got Unknown
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Expected O, but got Unknown
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Expected O, but got Unknown
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Expected O, but got Unknown
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Expected O, but got Unknown
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Expected O, but got Unknown
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Expected O, but got Unknown
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Expected O, but got Unknown
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Expected O, but got Unknown
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Expected O, but got Unknown
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Expected O, but got Unknown
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Expected O, but got Unknown
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Expected O, but got Unknown
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Expected O, but got Unknown
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Expected O, but got Unknown
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Expected O, but got Unknown
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Expected O, but got Unknown
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Expected O, but got Unknown
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Expected O, but got Unknown
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Expected O, but got Unknown
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Expected O, but got Unknown
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Expected O, but got Unknown
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Expected O, but got Unknown
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Expected O, but got Unknown
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Expected O, but got Unknown
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Expected O, but got Unknown
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Expected O, but got Unknown
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Expected O, but got Unknown
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Expected O, but got Unknown
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Expected O, but got Unknown
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Expected O, but got Unknown
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Expected O, but got Unknown
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Expected O, but got Unknown
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Expected O, but got Unknown
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Expected O, but got Unknown
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Expected O, but got Unknown
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Expected O, but got Unknown
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Expected O, but got Unknown
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Expected O, but got Unknown
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Expected O, but got Unknown
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Expected O, but got Unknown
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Expected O, but got Unknown
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Expected O, but got Unknown
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Expected O, but got Unknown
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Expected O, but got Unknown
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Expected O, but got Unknown
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Expected O, but got Unknown
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Expected O, but got Unknown
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Expected O, but got Unknown
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Expected O, but got Unknown
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Expected O, but got Unknown
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Expected O, but got Unknown
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Expected O, but got Unknown
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Expected O, but got Unknown
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Expected O, but got Unknown
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Expected O, but got Unknown
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Expected O, but got Unknown
		//IL_1a1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a28: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
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
		Appearance val13 = new Appearance();
		Appearance val14 = new Appearance();
		Appearance val15 = new Appearance();
		Appearance val16 = new Appearance();
		Appearance val17 = new Appearance();
		Appearance val18 = new Appearance();
		Appearance val19 = new Appearance();
		Appearance val20 = new Appearance();
		Appearance val21 = new Appearance();
		Appearance val22 = new Appearance();
		Appearance val23 = new Appearance();
		Appearance val24 = new Appearance();
		Appearance val25 = new Appearance();
		Override val26 = new Override();
		Appearance val27 = new Appearance();
		Appearance val28 = new Appearance();
		Appearance val29 = new Appearance();
		UltraTab val30 = new UltraTab();
		UltraTab val31 = new UltraTab();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.MasterData.frmChartersTree));
		Appearance val32 = new Appearance();
		this.tabItem = new UltraTabPageControl();
		this.txtTaxNo = new UltraTextEditor();
		this.lblTaxNo = new UltraLabel();
		this.lblIDIssueDate = new UltraLabel();
		this.dtpIDIssueDate = new UltraDateTimeEditor();
		this.lblIDNo = new UltraLabel();
		this.txtIDNo = new UltraTextEditor();
		this.lblCDCExpireDate = new UltraLabel();
		this.dtpCDCExpireDate = new UltraDateTimeEditor();
		this.lblCDCIssueDate = new UltraLabel();
		this.dtpCDCIssueDate = new UltraDateTimeEditor();
		this.lblCDCNo = new UltraLabel();
		this.txtCDCNo = new UltraTextEditor();
		this.lblPassportExpireDate = new UltraLabel();
		this.dtpPassportExpireDate = new UltraDateTimeEditor();
		this.lblPassportIssueDate = new UltraLabel();
		this.dtpPassportIssueDate = new UltraDateTimeEditor();
		this.lblPassportNo = new UltraLabel();
		this.txtPassportNo = new UltraTextEditor();
		this.cboDefaultAcc = new UltraComboEditor();
		this.lblClientAccount = new UltraLabel();
		this.cboNationality = new UltraComboEditor();
		this.txtNo = new UltraTextEditor();
		this.lblNationality = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.cboCountry = new UltraComboEditor();
		this.lblBirthDate = new UltraLabel();
		this.lblCountry = new UltraLabel();
		this.lblClintSupplierTitle = new UltraLabel();
		this.cboTitle = new UltraComboEditor();
		this.lblMobile2 = new UltraLabel();
		this.lblReorder = new UltraLabel();
		this.txtMobile2 = new UltraTextEditor();
		this.cboArea = new UltraComboEditor();
		this.txtMobile = new UltraTextEditor();
		this.lblArea = new UltraLabel();
		this.lblMin = new UltraLabel();
		this.txtFax = new UltraTextEditor();
		this.cboCity = new UltraComboEditor();
		this.lblMax = new UltraLabel();
		this.txtTel = new UltraTextEditor();
		this.lblEMail = new UltraLabel();
		this.lblCity = new UltraLabel();
		this.txtEMail = new UltraTextEditor();
		this.dtpBirthDate = new UltraDateTimeEditor();
		this.lblAddress = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.txtAddress = new UltraTextEditor();
		this.tabService = new UltraTabPageControl();
		this.TreeAccounts = new UltraTree();
		this.lblAccounts = new UltraLabel();
		this.txtItems = new UltraTextEditor();
		this.btnItemsSearch = new UltraButton();
		this.cboType = new UltraComboEditor();
		this.lblType = new UltraLabel();
		this.tabItemType = new UltraTabControl();
		this.ultraTabSharedControlsPage1 = new UltraTabSharedControlsPage();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.changeParentTSMenu = new System.Windows.Forms.ToolStripMenuItem();
		this.setAsGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.setAsSubAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.lblBranch = new UltraLabel();
		this.cboBranch = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.dtChart).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.treeChart).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtName).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabItem).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtTaxNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpIDIssueDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtIDNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCDCExpireDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCDCIssueDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCDCNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpPassportExpireDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpPassportIssueDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassportNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultAcc).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboNationality).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTitle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobile2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobile).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBirthDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabService).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TreeAccounts).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tabItemType).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabItemType).SuspendLayout();
		this.contextMenuStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboBranch).BeginInit();
		base.SuspendLayout();
		((System.Windows.Forms.Control)(object)base.treeChart).ContextMenuStrip = this.contextMenuStrip1;
		resources.ApplyResources(base.treeChart, "treeChart");
		resources.ApplyResources(base.txtCode, "txtCode");
		((EditorButtonControlBase)base.txtCode).ReadOnly = true;
		resources.ApplyResources(base.txtName, "txtName");
		((EditorButtonControlBase)base.txtName).ReadOnly = true;
		resources.ApplyResources(base.lblPath, "lblPath");
		resources.ApplyResources(base.txtNameEn, "txtNameEn");
		((EditorButtonControlBase)base.txtNameEn).ReadOnly = true;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.label1, "label1");
		((ControlBase)base.label1).WrapText = false;
		resources.ApplyResources(base.label2, "label2");
		((ControlBase)base.label2).WrapText = false;
		resources.ApplyResources(base.label3, "label3");
		((ControlBase)base.label3).WrapText = false;
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		resources.ApplyResources(base.lblHistory, "lblHistory");
		((AppearanceBase)val).ForeColor = System.Drawing.Color.Navy;
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxNo);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxNo);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblIDIssueDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.dtpIDIssueDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblIDNo);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtIDNo);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblCDCExpireDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.dtpCDCExpireDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblCDCIssueDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.dtpCDCIssueDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblCDCNo);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtCDCNo);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblPassportExpireDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.dtpPassportExpireDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblPassportIssueDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.dtpPassportIssueDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblPassportNo);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtPassportNo);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboDefaultAcc);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblClientAccount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboNationality);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtNo);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblNationality);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboCountry);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblBirthDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblCountry);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblClintSupplierTitle);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboTitle);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblMobile2);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblReorder);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtMobile2);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboArea);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtMobile);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblArea);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblMin);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtFax);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboCity);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblMax);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtTel);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblEMail);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblCity);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtEMail);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.dtpBirthDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblAddress);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtAddress);
		resources.ApplyResources(this.tabItem, "tabItem");
		((System.Windows.Forms.Control)(object)this.tabItem).Name = "tabItem";
		resources.ApplyResources(this.txtTaxNo, "txtTaxNo");
		((System.Windows.Forms.Control)(object)this.txtTaxNo).Name = "txtTaxNo";
		((AppearanceBase)val2).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblTaxNo).Appearance = (AppearanceBase)(object)val2;
		this.lblTaxNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblTaxNo, "lblTaxNo");
		((System.Windows.Forms.Control)(object)this.lblTaxNo).Name = "lblTaxNo";
		((ControlBase)this.lblTaxNo).WrapText = false;
		((AppearanceBase)val3).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val3).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblIDIssueDate).Appearance = (AppearanceBase)(object)val3;
		this.lblIDIssueDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblIDIssueDate, "lblIDIssueDate");
		((System.Windows.Forms.Control)(object)this.lblIDIssueDate).Name = "lblIDIssueDate";
		((ControlBase)this.lblIDIssueDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpIDIssueDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpIDIssueDate, "dtpIDIssueDate");
		((System.Windows.Forms.Control)(object)this.dtpIDIssueDate).Name = "dtpIDIssueDate";
		((AppearanceBase)val4).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblIDNo).Appearance = (AppearanceBase)(object)val4;
		this.lblIDNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblIDNo, "lblIDNo");
		((System.Windows.Forms.Control)(object)this.lblIDNo).Name = "lblIDNo";
		((ControlBase)this.lblIDNo).WrapText = false;
		resources.ApplyResources(this.txtIDNo, "txtIDNo");
		((System.Windows.Forms.Control)(object)this.txtIDNo).Name = "txtIDNo";
		((AppearanceBase)val5).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblCDCExpireDate).Appearance = (AppearanceBase)(object)val5;
		this.lblCDCExpireDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblCDCExpireDate, "lblCDCExpireDate");
		((System.Windows.Forms.Control)(object)this.lblCDCExpireDate).Name = "lblCDCExpireDate";
		((ControlBase)this.lblCDCExpireDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpCDCExpireDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpCDCExpireDate, "dtpCDCExpireDate");
		((System.Windows.Forms.Control)(object)this.dtpCDCExpireDate).Name = "dtpCDCExpireDate";
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblCDCIssueDate).Appearance = (AppearanceBase)(object)val6;
		this.lblCDCIssueDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblCDCIssueDate, "lblCDCIssueDate");
		((System.Windows.Forms.Control)(object)this.lblCDCIssueDate).Name = "lblCDCIssueDate";
		((ControlBase)this.lblCDCIssueDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpCDCIssueDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpCDCIssueDate, "dtpCDCIssueDate");
		((System.Windows.Forms.Control)(object)this.dtpCDCIssueDate).Name = "dtpCDCIssueDate";
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblCDCNo).Appearance = (AppearanceBase)(object)val7;
		this.lblCDCNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblCDCNo, "lblCDCNo");
		((System.Windows.Forms.Control)(object)this.lblCDCNo).Name = "lblCDCNo";
		((ControlBase)this.lblCDCNo).WrapText = false;
		resources.ApplyResources(this.txtCDCNo, "txtCDCNo");
		((System.Windows.Forms.Control)(object)this.txtCDCNo).Name = "txtCDCNo";
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblPassportExpireDate).Appearance = (AppearanceBase)(object)val8;
		this.lblPassportExpireDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblPassportExpireDate, "lblPassportExpireDate");
		((System.Windows.Forms.Control)(object)this.lblPassportExpireDate).Name = "lblPassportExpireDate";
		((ControlBase)this.lblPassportExpireDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpPassportExpireDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpPassportExpireDate, "dtpPassportExpireDate");
		((System.Windows.Forms.Control)(object)this.dtpPassportExpireDate).Name = "dtpPassportExpireDate";
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblPassportIssueDate).Appearance = (AppearanceBase)(object)val9;
		this.lblPassportIssueDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblPassportIssueDate, "lblPassportIssueDate");
		((System.Windows.Forms.Control)(object)this.lblPassportIssueDate).Name = "lblPassportIssueDate";
		((ControlBase)this.lblPassportIssueDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpPassportIssueDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpPassportIssueDate, "dtpPassportIssueDate");
		((System.Windows.Forms.Control)(object)this.dtpPassportIssueDate).Name = "dtpPassportIssueDate";
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblPassportNo).Appearance = (AppearanceBase)(object)val10;
		this.lblPassportNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblPassportNo, "lblPassportNo");
		((System.Windows.Forms.Control)(object)this.lblPassportNo).Name = "lblPassportNo";
		((ControlBase)this.lblPassportNo).WrapText = false;
		resources.ApplyResources(this.txtPassportNo, "txtPassportNo");
		((System.Windows.Forms.Control)(object)this.txtPassportNo).Name = "txtPassportNo";
		resources.ApplyResources(this.cboDefaultAcc, "cboDefaultAcc");
		((System.Windows.Forms.Control)(object)this.cboDefaultAcc).Name = "cboDefaultAcc";
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblClientAccount).Appearance = (AppearanceBase)(object)val11;
		this.lblClientAccount.AutoEllipsis = false;
		resources.ApplyResources(this.lblClientAccount, "lblClientAccount");
		((System.Windows.Forms.Control)(object)this.lblClientAccount).Name = "lblClientAccount";
		((ControlBase)this.lblClientAccount).WrapText = false;
		this.cboNationality.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboNationality, "cboNationality");
		((System.Windows.Forms.Control)(object)this.cboNationality).Name = "cboNationality";
		resources.ApplyResources(this.txtNo, "txtNo");
		((System.Windows.Forms.Control)(object)this.txtNo).Name = "txtNo";
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblNationality).Appearance = (AppearanceBase)(object)val12;
		this.lblNationality.AutoEllipsis = false;
		resources.ApplyResources(this.lblNationality, "lblNationality");
		((System.Windows.Forms.Control)(object)this.lblNationality).Name = "lblNationality";
		((ControlBase)this.lblNationality).WrapText = false;
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val13;
		this.ultraLabel1.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		this.cboCountry.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboCountry, "cboCountry");
		((System.Windows.Forms.Control)(object)this.cboCountry).Name = "cboCountry";
		((TextEditorControlBase)this.cboCountry).ValueChanged += new System.EventHandler(cboCountry_ValueChanged);
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblBirthDate).Appearance = (AppearanceBase)(object)val14;
		this.lblBirthDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblBirthDate, "lblBirthDate");
		((System.Windows.Forms.Control)(object)this.lblBirthDate).Name = "lblBirthDate";
		((ControlBase)this.lblBirthDate).WrapText = false;
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblCountry).Appearance = (AppearanceBase)(object)val15;
		this.lblCountry.AutoEllipsis = false;
		resources.ApplyResources(this.lblCountry, "lblCountry");
		((System.Windows.Forms.Control)(object)this.lblCountry).Name = "lblCountry";
		((ControlBase)this.lblCountry).WrapText = false;
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblClintSupplierTitle).Appearance = (AppearanceBase)(object)val16;
		this.lblClintSupplierTitle.AutoEllipsis = false;
		resources.ApplyResources(this.lblClintSupplierTitle, "lblClintSupplierTitle");
		((System.Windows.Forms.Control)(object)this.lblClintSupplierTitle).Name = "lblClintSupplierTitle";
		((ControlBase)this.lblClintSupplierTitle).WrapText = false;
		this.cboTitle.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboTitle, "cboTitle");
		((System.Windows.Forms.Control)(object)this.cboTitle).Name = "cboTitle";
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblMobile2).Appearance = (AppearanceBase)(object)val17;
		this.lblMobile2.AutoEllipsis = false;
		resources.ApplyResources(this.lblMobile2, "lblMobile2");
		((System.Windows.Forms.Control)(object)this.lblMobile2).Name = "lblMobile2";
		((ControlBase)this.lblMobile2).WrapText = false;
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val18).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblReorder).Appearance = (AppearanceBase)(object)val18;
		this.lblReorder.AutoEllipsis = false;
		resources.ApplyResources(this.lblReorder, "lblReorder");
		((System.Windows.Forms.Control)(object)this.lblReorder).Name = "lblReorder";
		((ControlBase)this.lblReorder).WrapText = false;
		resources.ApplyResources(this.txtMobile2, "txtMobile2");
		((System.Windows.Forms.Control)(object)this.txtMobile2).Name = "txtMobile2";
		this.cboArea.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboArea, "cboArea");
		((System.Windows.Forms.Control)(object)this.cboArea).Name = "cboArea";
		resources.ApplyResources(this.txtMobile, "txtMobile");
		((System.Windows.Forms.Control)(object)this.txtMobile).Name = "txtMobile";
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblArea).Appearance = (AppearanceBase)(object)val19;
		this.lblArea.AutoEllipsis = false;
		resources.ApplyResources(this.lblArea, "lblArea");
		((System.Windows.Forms.Control)(object)this.lblArea).Name = "lblArea";
		((ControlBase)this.lblArea).WrapText = false;
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblMin).Appearance = (AppearanceBase)(object)val20;
		this.lblMin.AutoEllipsis = false;
		resources.ApplyResources(this.lblMin, "lblMin");
		((System.Windows.Forms.Control)(object)this.lblMin).Name = "lblMin";
		((ControlBase)this.lblMin).WrapText = false;
		resources.ApplyResources(this.txtFax, "txtFax");
		((System.Windows.Forms.Control)(object)this.txtFax).Name = "txtFax";
		this.cboCity.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboCity, "cboCity");
		((System.Windows.Forms.Control)(object)this.cboCity).Name = "cboCity";
		((TextEditorControlBase)this.cboCity).ValueChanged += new System.EventHandler(cboCity_ValueChanged);
		((AppearanceBase)val21).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val21).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblMax).Appearance = (AppearanceBase)(object)val21;
		this.lblMax.AutoEllipsis = false;
		resources.ApplyResources(this.lblMax, "lblMax");
		((System.Windows.Forms.Control)(object)this.lblMax).Name = "lblMax";
		((ControlBase)this.lblMax).WrapText = false;
		resources.ApplyResources(this.txtTel, "txtTel");
		((System.Windows.Forms.Control)(object)this.txtTel).Name = "txtTel";
		((AppearanceBase)val22).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val22).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblEMail).Appearance = (AppearanceBase)(object)val22;
		this.lblEMail.AutoEllipsis = false;
		resources.ApplyResources(this.lblEMail, "lblEMail");
		((System.Windows.Forms.Control)(object)this.lblEMail).Name = "lblEMail";
		((ControlBase)this.lblEMail).WrapText = false;
		((AppearanceBase)val23).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val23).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblCity).Appearance = (AppearanceBase)(object)val23;
		this.lblCity.AutoEllipsis = false;
		resources.ApplyResources(this.lblCity, "lblCity");
		((System.Windows.Forms.Control)(object)this.lblCity).Name = "lblCity";
		((ControlBase)this.lblCity).WrapText = false;
		resources.ApplyResources(this.txtEMail, "txtEMail");
		((System.Windows.Forms.Control)(object)this.txtEMail).Name = "txtEMail";
		((UltraWinEditorMaskedControlBase)this.dtpBirthDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpBirthDate, "dtpBirthDate");
		((System.Windows.Forms.Control)(object)this.dtpBirthDate).Name = "dtpBirthDate";
		((AppearanceBase)val24).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val24).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblAddress).Appearance = (AppearanceBase)(object)val24;
		this.lblAddress.AutoEllipsis = false;
		resources.ApplyResources(this.lblAddress, "lblAddress");
		((System.Windows.Forms.Control)(object)this.lblAddress).Name = "lblAddress";
		((ControlBase)this.lblAddress).WrapText = false;
		((AppearanceBase)val25).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val25).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblNotes).Appearance = (AppearanceBase)(object)val25;
		this.lblNotes.AutoEllipsis = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.txtAddress, "txtAddress");
		((System.Windows.Forms.Control)(object)this.txtAddress).Name = "txtAddress";
		((System.Windows.Forms.Control)(object)this.tabService).Controls.Add((System.Windows.Forms.Control)(object)this.TreeAccounts);
		((System.Windows.Forms.Control)(object)this.tabService).Controls.Add((System.Windows.Forms.Control)(object)this.lblAccounts);
		((System.Windows.Forms.Control)(object)this.tabService).Controls.Add((System.Windows.Forms.Control)(object)this.txtItems);
		((System.Windows.Forms.Control)(object)this.tabService).Controls.Add((System.Windows.Forms.Control)(object)this.btnItemsSearch);
		resources.ApplyResources(this.tabService, "tabService");
		((System.Windows.Forms.Control)(object)this.tabService).Name = "tabService";
		resources.ApplyResources(this.TreeAccounts, "TreeAccounts");
		((System.Windows.Forms.Control)(object)this.TreeAccounts).Name = "TreeAccounts";
		val26.NodeStyle = (NodeStyle)1;
		this.TreeAccounts.Override = val26;
		this.TreeAccounts.AfterCheck += new AfterNodeChangedEventHandler(Tree_AfterCheck);
		resources.ApplyResources(this.lblAccounts, "lblAccounts");
		((System.Windows.Forms.Control)(object)this.lblAccounts).Name = "lblAccounts";
		resources.ApplyResources(this.txtItems, "txtItems");
		((System.Windows.Forms.Control)(object)this.txtItems).Name = "txtItems";
		((TextEditorControlBase)this.txtItems).ValueChanged += new System.EventHandler(txtItems_ValueChanged);
		resources.ApplyResources(this.btnItemsSearch, "btnItemsSearch");
		((AppearanceBase)val27).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnItemsSearch).Appearance = (AppearanceBase)(object)val27;
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Name = "btnItemsSearch";
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Click += new System.EventHandler(btnItemsSearch_Click);
		resources.ApplyResources(this.cboType, "cboType");
		((System.Windows.Forms.Control)(object)this.cboType).Name = "cboType";
		((TextEditorControlBase)this.cboType).ValueChanged += new System.EventHandler(cboType_ValueChanged);
		((AppearanceBase)val28).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val28).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblType).Appearance = (AppearanceBase)(object)val28;
		this.lblType.AutoEllipsis = false;
		resources.ApplyResources(this.lblType, "lblType");
		((System.Windows.Forms.Control)(object)this.lblType).Name = "lblType";
		((ControlBase)this.lblType).WrapText = false;
		resources.ApplyResources(this.tabItemType, "tabItemType");
		((AppearanceBase)val29).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val29).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.tabItemType).Appearance = (AppearanceBase)(object)val29;
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.tabItem);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.tabService);
		((System.Windows.Forms.Control)(object)this.tabItemType).Name = "tabItemType";
		((UltraTabControlBase)this.tabItemType).SharedControlsPage = this.ultraTabSharedControlsPage1;
		((UltraTabControlBase)this.tabItemType).TabOrientation = (TabOrientation)2;
		((KeyedSubObjectBase)val30).Key = "Charter";
		val30.TabPage = this.tabItem;
		resources.ApplyResources(val30, "ultraTab2");
		((SubObjectBase)val30).ForceApplyResources = "";
		((KeyedSubObjectBase)val31).Key = "Accounts";
		val31.TabPage = this.tabService;
		resources.ApplyResources(val31, "ultraTab1");
		((SubObjectBase)val31).ForceApplyResources = "";
		((UltraTabControlBase)this.tabItemType).Tabs.AddRange((UltraTab[])(object)new UltraTab[2] { val30, val31 });
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.changeParentTSMenu, this.setAsGroupToolStripMenuItem, this.setAsSubAccountToolStripMenuItem });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.ShowImageMargin = false;
		resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
		this.changeParentTSMenu.Name = "changeParentTSMenu";
		resources.ApplyResources(this.changeParentTSMenu, "changeParentTSMenu");
		this.changeParentTSMenu.Click += new System.EventHandler(changeParentTSMenu_Click);
		this.setAsGroupToolStripMenuItem.Name = "setAsGroupToolStripMenuItem";
		resources.ApplyResources(this.setAsGroupToolStripMenuItem, "setAsGroupToolStripMenuItem");
		this.setAsGroupToolStripMenuItem.Click += new System.EventHandler(setAsGroupToolStripMenuItem_Click);
		this.setAsSubAccountToolStripMenuItem.Name = "setAsSubAccountToolStripMenuItem";
		resources.ApplyResources(this.setAsSubAccountToolStripMenuItem, "setAsSubAccountToolStripMenuItem");
		this.setAsSubAccountToolStripMenuItem.Click += new System.EventHandler(setAsSubAccountToolStripMenuItem_Click);
		((AppearanceBase)val32).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val32).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblBranch).Appearance = (AppearanceBase)(object)val32;
		this.lblBranch.AutoEllipsis = false;
		resources.ApplyResources(this.lblBranch, "lblBranch");
		((System.Windows.Forms.Control)(object)this.lblBranch).Name = "lblBranch";
		((ControlBase)this.lblBranch).WrapText = false;
		resources.ApplyResources(this.cboBranch, "cboBranch");
		((System.Windows.Forms.Control)(object)this.cboBranch).Name = "cboBranch";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.tabItemType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblType);
		base.Name = "frmChartersTree";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.tabItemType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.treeChart, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.label1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.label2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.label3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAddRoot, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		((System.ComponentModel.ISupportInitialize)base.dtChart).EndInit();
		((System.ComponentModel.ISupportInitialize)base.treeChart).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtName).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.tabItem).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.tabItem).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtTaxNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpIDIssueDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtIDNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCDCExpireDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCDCIssueDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCDCNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpPassportExpireDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpPassportIssueDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassportNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultAcc).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboNationality).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTitle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobile2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobile).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBirthDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).EndInit();
		((System.Windows.Forms.Control)(object)this.tabService).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.tabService).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TreeAccounts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tabItemType).EndInit();
		((System.Windows.Forms.Control)(object)this.tabItemType).ResumeLayout(false);
		this.contextMenuStrip1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.cboBranch).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
