using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.FixedAssets;
using BusinessLayer.HR;
using BusinessLayer.Privilege;
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

namespace ERP.FixedAssets.MasterData;

public class frmAssetsTree : frmTree2
{
	private bool CanModAdministrativeStructure = false;

	private DataTable dtAccounts;

	private DataTable dtReports;

	private DataTable dtAssetsDepreciationTypes;

	private DataTable dtAssetState;

	private DataTable dtAssetsLocations;

	private DataTable dtDirectManager;

	private DataTable dtCustodySubAccount;

	private DataTable dtSubAccDetails;

	private IContainer components = null;

	public UltraTabControl tabItemType;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl tabItem;

	private UltraComboEditor cboAssetState;

	private UltraLabel lblAssetState;

	private UltraLabel lblAcquisitionDate;

	private UltraDateTimeEditor dtpAcquisitionDate;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraComboEditor cboAssetDepreciationType;

	private UltraLabel lblAssetDepreciationType;

	private OpenFileDialog ofdItemPic;

	private UltraDateTimeEditor dtpScrapDate;

	private UltraLabel lblScrapDate;

	private UltraCheckEditor chkHasTransAction;

	private UltraTextEditor txtAssetBarCode;

	private UltraLabel lblAssetBarcode;

	private UltraDateTimeEditor dtpSalesDate;

	private UltraLabel lblSalesDate;

	private UltraTabPageControl ultraTabPageControl6;

	private UltraComboEditor cboAssetAccount;

	private UltraLabel lblAssetAccount;

	private UltraComboEditor cboAssetAccumulatedDepreciationAccount;

	private UltraLabel lblAssetAccumulatedDepreciationAccount;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem changeParentTSMenu;

	private ToolStripMenuItem setAsGroupToolStripMenuItem;

	private ToolStripMenuItem setAsSubAccountToolStripMenuItem;

	private ToolStripMenuItem modifyGroupEmployeesToolStripMenuItem;

	private UltraDateTimeEditor dtpStartDepreciationDate;

	private UltraLabel lblStartDepreciationDate;

	private UltraTextEditor txtScrapValue;

	private UltraLabel lblScrapValue;

	private UltraTextEditor txtDepreciationPeriodCount;

	private UltraLabel lblDepreciationPeriodCount;

	private UltraTextEditor txtDepreciationPercentage;

	private UltraLabel lblDepreciationPercentage;

	public UltraButton btnItemsSearch;

	private UltraTextEditor txtItems;

	private UltraLabel lblAccounts;

	public UltraTree TreeAccounts;

	private UltraComboEditor cboCustodySubAccount;

	private UltraLabel lblCustodySubAccount;

	private UltraComboEditor cboAssetDepreciationExpenseAccount;

	private UltraLabel lblAssetDepreciationExpenseAccount;

	private UltraComboEditor cboAssetInvestmentAccount;

	private UltraLabel lblAssetInvestmentAccount;

	private UltraComboEditor cboAssetSalesAccount;

	private UltraLabel lblAssetSalesAccount;

	public UltraTree treeAssetLocation;

	private UltraLabel lblAdministrativeStructure;

	public frmAssetsTree()
	{
		InitializeComponent();
		IDCol = "SubAccountID";
		NoCol = "SubAccountNumber";
		NameCol = "SubAccountNameAr";
		NameEnCol = "SubAccountNameEn";
		ParentIDCol = "ParentID";
		IsMainCol = "IsMain";
		AdditionalCol1 = "SubAccountTypeID";
		ItemLevelCol = "LevelID";
		TableName = "A_SubAccounts";
		LevelsTable = "A_SubAccounts_Levels";
		LevelsCol = "LevelID";
		LevelsWidthCol = "Width";
		AllowAddRoot = false;
	}

	public override void PrepareData()
	{
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (TableName != "" && LevelsTable != "")
		{
			dtLevels = Main.SyncExecuteQuery_DataTable("TreeLevels_Select '" + LevelsCol + "','" + LevelsWidthCol + "','" + LevelsTable + "'");
			dtChart = SubAccounts.FillTreeBySubAccountTypeIDs(GlobalVariables.AssetSubAccountTypeIDs, GlobalVariables.BranchIDs, IsFromServer: true);
			treeChart.Nodes.Clear();
			if (dtChart.Rows.Count > 0)
			{
				FillTree("0");
			}
		}
		dtCustodySubAccount = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCustodySubAccount, dtCustodySubAccount, "SubAccountID", "SubAccountName");
		dtAccounts = Accounts.Select("-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		TreeFunctions.FillTree(TreeAccounts, dtAccounts, "ParentID", "AccountID", GlobalVariables.IsArabic ? "AccountNameAr" : "AccountNameEn", "AccountNumber", "IsMain");
		dtAssetsDepreciationTypes = AssetsDepreciationTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboAssetDepreciationType, dtAssetsDepreciationTypes, "AssetDepreciationTypeID", "AssetDepreciationTypeName");
		dtAssetState = AssetsState.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboAssetState, dtAssetState, "AssetStateID", "AssetStateName");
		dtAssetsLocations = AssetsLocations.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		TreeFunctions.FillTree(treeAssetLocation, dtAssetsLocations, "ParentID", "AssetLocationID", GlobalVariables.IsArabic ? "AssetLocationNameAr" : "AssetLocationNameEn", "AssetLocationNumber", "IsMain");
		DataView dataView = new DataView(dtChart);
		dataView.RowFilter = "IsMain=0";
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		dtDirectManager = dataView.ToTable();
		dtSubAccDetails = SubAccounts_Details.SelectBySubAccountIDWithAccountName("0", "1", IsFromServer: true);
	}

	public override void ClearControls()
	{
		base.ClearControls();
		if (Adding)
		{
			treeAssetLocation.CollapseAll();
			TreeFunctions.SetAllTreeNodesCheckState(Checked: false, treeAssetLocation);
			((Control)(object)txtCode).Text = SubAccounts.GetCode((SelectedNode == null) ? "Null" : ((KeyedSubObjectBase)SelectedNode).Key, IsFromServer: true);
			((Control)(object)txtAssetBarCode).Text = Assets.GetCode(IsFromServer: true);
			dtpSalesDate.Value = DBNull.Value;
			dtpScrapDate.Value = DBNull.Value;
			dtpAcquisitionDate.Value = DBNull.Value;
			dtpStartDepreciationDate.Value = DBNull.Value;
			cboAssetAccount.SelectedIndex = -1;
			cboAssetAccumulatedDepreciationAccount.SelectedIndex = -1;
			cboAssetDepreciationExpenseAccount.SelectedIndex = -1;
			cboAssetDepreciationType.SelectedIndex = -1;
			cboAssetInvestmentAccount.SelectedIndex = -1;
			cboAssetSalesAccount.SelectedIndex = -1;
			cboAssetState.SelectedIndex = -1;
			cboCustodySubAccount.SelectedIndex = -1;
			cboTransactionBranch.SelectedIndex = -1;
			((TextEditorControlBase)txtDepreciationPercentage).Clear();
			((TextEditorControlBase)txtDepreciationPeriodCount).Clear();
			((TextEditorControlBase)txtNotes).Clear();
			((Control)(object)txtScrapValue).Text = "1";
			((Control)(object)txtName).Select();
		}
	}

	public override void SetControls(bool NavMode)
	{
		CanModAdministrativeStructure = TreeFunctions.GetTreeFirstCheckedNodeID(treeAssetLocation) == "";
		base.SetControls(NavMode);
		((EditorButtonControlBase)dtpAcquisitionDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpStartDepreciationDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpScrapDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpSalesDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAssetBarCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDepreciationPercentage).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDepreciationPeriodCount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtScrapValue).ReadOnly = NavMode;
		((EditorButtonControlBase)cboAssetDepreciationType).ReadOnly = NavMode;
		((EditorButtonControlBase)cboAssetState).ReadOnly = NavMode;
		((EditorButtonControlBase)cboAssetAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboAssetAccumulatedDepreciationAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboAssetDepreciationExpenseAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboAssetInvestmentAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboAssetSalesAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboAssetState).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCustodySubAccount).ReadOnly = NavMode;
		((Control)(object)chkHasTransAction).Enabled = !NavMode;
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
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Expected O, but got Unknown
		base.DisplayData();
		if (SelectedNode == null)
		{
			return;
		}
		treeAssetLocation.BeforeCheck -= new BeforeCheckEventHandler(treeAssetLocation_BeforeCheck);
		TreeFunctions.SetAllTreeNodesCheckState(Checked: false, treeAssetLocation);
		dtpAcquisitionDate.Value = DBNull.Value;
		dtpStartDepreciationDate.Value = DBNull.Value;
		dtpSalesDate.Value = DBNull.Value;
		((TextEditorControlBase)txtAssetBarCode).Clear();
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)cboAssetDepreciationType).Clear();
		((TextEditorControlBase)cboAssetState).Clear();
		((TextEditorControlBase)cboAssetAccount).Clear();
		((TextEditorControlBase)cboAssetAccumulatedDepreciationAccount).Clear();
		((UltraToggleEditorBase)chkHasTransAction).Checked = false;
		changeParentTSMenu.Enabled = !Convert.ToBoolean(((DataRow)((SubObjectBase)SelectedNode).Tag)[IsMainCol]);
		setAsGroupToolStripMenuItem.Enabled = !Convert.ToBoolean(((DataRow)((SubObjectBase)SelectedNode).Tag)[IsMainCol]);
		modifyGroupEmployeesToolStripMenuItem.Enabled = Convert.ToBoolean(((DataRow)((SubObjectBase)SelectedNode).Tag)[IsMainCol]);
		setAsSubAccountToolStripMenuItem.Enabled = ((DisposableObjectCollectionBase)SelectedNode.Nodes).Count == 0 && Convert.ToBoolean(((DataRow)((SubObjectBase)SelectedNode).Tag)[IsMainCol]);
		((Control)(object)lblAccounts).Text = "";
		dtSubAccDetails = SubAccounts_Details.SelectBySubAccountIDWithAccountName(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		for (int i = 0; i < dtSubAccDetails.Rows.Count; i++)
		{
			UltraLabel obj = lblAccounts;
			((Control)(object)obj).Text = string.Concat(((Control)(object)obj).Text, dtSubAccDetails.Rows[i]["AccountName"], " \n");
		}
		treeAssetLocation.CollapseAll();
		GlobalFunctions.FillCombo(cboAssetAccount, dtSubAccDetails, "AccountID", "AccountName");
		GlobalFunctions.FillCombo(cboAssetAccumulatedDepreciationAccount, dtSubAccDetails, "AccountID", "AccountName");
		GlobalFunctions.FillCombo(cboAssetSalesAccount, dtSubAccDetails, "AccountID", "AccountName");
		GlobalFunctions.FillCombo(cboAssetInvestmentAccount, dtSubAccDetails, "AccountID", "AccountName");
		GlobalFunctions.FillCombo(cboAssetDepreciationExpenseAccount, dtSubAccDetails, "AccountID", "AccountName");
		DataTable dataTable = Assets.SelectBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.Rows[0];
			if (dataRow["AssetLocationID"] != DBNull.Value)
			{
				treeAssetLocation.GetNodeByKey(dataRow["AssetLocationID"].ToString()).CheckedState = CheckState.Checked;
				treeAssetLocation.ActiveNode = treeAssetLocation.GetNodeByKey(dataRow["AssetLocationID"].ToString());
			}
			dtpAcquisitionDate.Value = dataRow["AcquisitionDate"];
			dtpStartDepreciationDate.Value = dataRow["StartDepreciationDate"];
			dtpScrapDate.Value = dataRow["ScrapDate"];
			dtpSalesDate.Value = dataRow["SalesDate"];
			((Control)(object)txtAssetBarCode).Text = dataRow["AssetBarCode"].ToString();
			((Control)(object)txtNotes).Text = dataRow["Notes"].ToString();
			((Control)(object)txtDepreciationPercentage).Text = dataRow["DepreciationPercentage"].ToString();
			((Control)(object)txtDepreciationPeriodCount).Text = dataRow["DepreciationPeriodCount"].ToString();
			((Control)(object)txtScrapValue).Text = dataRow["ScrapValue"].ToString();
			((TextEditorControlBase)cboAssetDepreciationType).Value = dataRow["AssetDepreciationTypeID"];
			((TextEditorControlBase)cboAssetState).Value = dataRow["AssetStateID"];
			((TextEditorControlBase)cboAssetAccount).Value = dataRow["AssetAccountID"];
			((TextEditorControlBase)cboAssetAccumulatedDepreciationAccount).Value = dataRow["AssetAccumulatedDepreciationAccountID"];
			((TextEditorControlBase)cboAssetDepreciationExpenseAccount).Value = dataRow["AssetDepreciationExpenseAccountID"];
			((TextEditorControlBase)cboAssetInvestmentAccount).Value = dataRow["AssetInvestmentAccountID"];
			((TextEditorControlBase)cboAssetSalesAccount).Value = dataRow["AssetSalesAccountID"];
			((TextEditorControlBase)cboCustodySubAccount).Value = dataRow["CustodySubAccountID"];
			((UltraToggleEditorBase)chkHasTransAction).Checked = !dataRow["HasTransAction"].Equals(DBNull.Value) && Convert.ToBoolean(dataRow["HasTransAction"]);
		}
		treeAssetLocation.BeforeCheck += new BeforeCheckEventHandler(treeAssetLocation_BeforeCheck);
	}

	public override int TreeAddData()
	{
		int result = 0;
		Main.StartBulkTrans(FromServer: true);
		try
		{
			result = SubAccounts.Insert_Update("-1", GetCode(), ((Control)(object)txtName).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? "Null" : ((Control)(object)txtNameEn).Text.Trim(), (SelectedNode == null) ? "Null" : ((DataRow)((SubObjectBase)SelectedNode).Tag)[0].ToString(), "0", (NodeLevel + 1).ToString(), "1", "1", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			SubAccounts_Details.Insert_UpdateByAccIDs(result.ToString(), TreeFunctions.GetTreeCheckedNodesIDs(TreeAccounts), "0", GlobalVariables.CurrentBranchID, IsFromServer: true);
			string treeFirstCheckedNodeID = TreeFunctions.GetTreeFirstCheckedNodeID(treeAssetLocation);
			Assets.Insert_Update((((Control)(object)txtAssetBarCode).Text == "") ? GetCode() : ((Control)(object)txtAssetBarCode).Text, result.ToString(), (dtpAcquisitionDate.Value == null) ? "Null" : dtpAcquisitionDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpStartDepreciationDate.Value == null) ? "Null" : dtpStartDepreciationDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboAssetState.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAssetState).Value.ToString(), (((Control)(object)txtScrapValue).Text == "") ? "0" : ((Control)(object)txtScrapValue).Text, (dtpScrapDate.Value == null) ? "Null" : dtpScrapDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpSalesDate.Value == null) ? "Null" : dtpSalesDate.DateTime.ToString(GlobalVariables.DateShortFormate), (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, (cboAssetAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAssetAccount).Value.ToString(), (cboAssetAccumulatedDepreciationAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAssetAccumulatedDepreciationAccount).Value.ToString(), (cboAssetDepreciationExpenseAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAssetDepreciationExpenseAccount).Value.ToString(), (cboAssetSalesAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAssetSalesAccount).Value.ToString(), (cboAssetInvestmentAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAssetInvestmentAccount).Value.ToString(), (treeFirstCheckedNodeID == "") ? "Null" : treeFirstCheckedNodeID, (cboCustodySubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCustodySubAccount).Value.ToString(), (cboAssetDepreciationType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAssetDepreciationType).Value.ToString(), (((Control)(object)txtDepreciationPercentage).Text == "") ? "0" : ((Control)(object)txtDepreciationPercentage).Text, (((Control)(object)txtDepreciationPeriodCount).Text == "") ? "0" : ((Control)(object)txtDepreciationPeriodCount).Text, ((UltraToggleEditorBase)chkHasTransAction).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
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
			SubAccounts.Insert_Update(((KeyedSubObjectBase)SelectedNode).Key, GetCode(), ((Control)(object)txtName).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? "Null" : ((Control)(object)txtNameEn).Text.Trim(), (SelectedNode.Parent == null) ? "Null" : ((KeyedSubObjectBase)SelectedNode.Parent).Key, ((bool)((DataRow)((SubObjectBase)SelectedNode).Tag)[IsMainCol]) ? "1" : "0", NodeLevel.ToString(), ((DataRow)((SubObjectBase)SelectedNode).Tag)[AdditionalCol1].ToString(), "1", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			SubAccounts_Details.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			string treeCheckedNodesIDs = TreeFunctions.GetTreeCheckedNodesIDs(TreeAccounts);
			SubAccounts_Details.Insert_UpdateByAccIDs(((KeyedSubObjectBase)SelectedNode).Key, treeCheckedNodesIDs, "0", GlobalVariables.CurrentBranchID, IsFromServer: true);
			string treeFirstCheckedNodeID = TreeFunctions.GetTreeFirstCheckedNodeID(treeAssetLocation);
			if (((DisposableObjectCollectionBase)SelectedNode.Nodes).Count > 0)
			{
				GlobalVariables.QuestionMB.Show("هل تريد ربط كل محتويات المجموعه بنفس الحسابات؟", "Would you like to Bind All Group Contents With The Same Accounts?");
				if (GlobalVariables.MessageBoxResult == 'Y')
				{
					SaveAccountsToChildNades(SelectedNode, treeCheckedNodesIDs);
				}
			}
			Assets.Insert_Update((((Control)(object)txtAssetBarCode).Text == "") ? GetCode() : ((Control)(object)txtAssetBarCode).Text, ((KeyedSubObjectBase)SelectedNode).Key, (dtpAcquisitionDate.Value == null) ? "Null" : dtpAcquisitionDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpStartDepreciationDate.Value == null) ? "Null" : dtpStartDepreciationDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboAssetState.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAssetState).Value.ToString(), (((Control)(object)txtScrapValue).Text == "") ? "0" : ((Control)(object)txtScrapValue).Text, (dtpScrapDate.Value == null) ? "Null" : dtpScrapDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpSalesDate.Value == null) ? "Null" : dtpSalesDate.DateTime.ToString(GlobalVariables.DateShortFormate), (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, (cboAssetAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAssetAccount).Value.ToString(), (cboAssetAccumulatedDepreciationAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAssetAccumulatedDepreciationAccount).Value.ToString(), (cboAssetDepreciationExpenseAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAssetDepreciationExpenseAccount).Value.ToString(), (cboAssetSalesAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAssetSalesAccount).Value.ToString(), (cboAssetInvestmentAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAssetInvestmentAccount).Value.ToString(), (treeFirstCheckedNodeID == "") ? "Null" : treeFirstCheckedNodeID, (cboCustodySubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCustodySubAccount).Value.ToString(), (cboAssetDepreciationType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAssetDepreciationType).Value.ToString(), (((Control)(object)txtDepreciationPercentage).Text == "") ? "0" : ((Control)(object)txtDepreciationPercentage).Text, (((Control)(object)txtDepreciationPeriodCount).Text == "") ? "0" : ((Control)(object)txtDepreciationPeriodCount).Text, ((UltraToggleEditorBase)chkHasTransAction).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
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
			Assets.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
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
		if (cboAssetState.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل حالة الأصل ", "Enter Asset State");
			((TextEditorControlBase)cboAssetState).Focus();
			cboAssetState.DropDown();
			return false;
		}
		if (cboAssetDepreciationType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل نوع الاهلاك   ", "Enter Asset Depreciation Type");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Details"];
			((TextEditorControlBase)cboAssetDepreciationType).Focus();
			cboAssetDepreciationType.DropDown();
			return false;
		}
		if (dtpAcquisitionDate.Value == null)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل تاريخ الإقتناء", "Please Select The Acquisition Date");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Details"];
			((Control)(object)dtpAcquisitionDate).Focus();
			return false;
		}
		if (dtpStartDepreciationDate.Value == null)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل تاريخ بدء الإهلاك", "Please Select The Start Depreciation Date");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Details"];
			((Control)(object)dtpStartDepreciationDate).Focus();
			return false;
		}
		if (((Control)(object)txtScrapValue).Text == "" || decimal.Parse(((Control)(object)txtScrapValue).Text) < 1m)
		{
			GlobalVariables.InformationMB.Show("سعر التخريد لابد ان يكون اكبر من الواحد", "SCrap Value Must Be Greater Than One");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Details"];
			((TextEditorControlBase)txtScrapValue).Focus();
			return false;
		}
		if (TreeFunctions.GetTreeCheckedNodesIDs(treeAssetLocation).Equals(","))
		{
			GlobalVariables.InformationMB.Show("من فضلك اختر موقع الأصل  ", "Enter Asset Location");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Details"];
			return false;
		}
		if (cboAssetAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل حساب الأصل  ", "Enter Asset Account");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Accounts"];
			((TextEditorControlBase)cboAssetAccount).Focus();
			cboAssetAccount.DropDown();
			return false;
		}
		if (cboAssetAccumulatedDepreciationAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل حساب مجمع إهلاك الأصل  ", "Enter Asset Accumulated Depreciation Account");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Accounts"];
			((TextEditorControlBase)cboAssetAccumulatedDepreciationAccount).Focus();
			cboAssetAccumulatedDepreciationAccount.DropDown();
			return false;
		}
		if (cboAssetDepreciationExpenseAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل حساب مصروف اهلاك الأصل  ", "Enter Asset Depreciation Expense Account");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Accounts"];
			((TextEditorControlBase)cboAssetDepreciationExpenseAccount).Focus();
			cboAssetDepreciationExpenseAccount.DropDown();
			return false;
		}
		if (cboAssetInvestmentAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل حساب إستثمار الأصل  ", "Enter Asset Investment Account");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Accounts"];
			((TextEditorControlBase)cboAssetInvestmentAccount).Focus();
			cboAssetInvestmentAccount.DropDown();
			return false;
		}
		if (cboAssetSalesAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل حساب مبيعات الأصل  ", "Enter Asset Sales Account");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Accounts"];
			((TextEditorControlBase)cboAssetSalesAccount).Focus();
			cboAssetSalesAccount.DropDown();
			return false;
		}
		if (((TextEditorControlBase)cboAssetAccumulatedDepreciationAccount).Value.Equals(((TextEditorControlBase)cboAssetAccount).Value))
		{
			GlobalVariables.InformationMB.Show("من فضلك اختر حسابين مختلفين لحساب الا\u064bصل و حساب مجمع الاهلاك  ", "Please Choose Different Accounts For Accumulated Depreciation and Asset Accounts");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Accounts"];
			((TextEditorControlBase)cboAssetAccumulatedDepreciationAccount).Focus();
			cboAssetAccumulatedDepreciationAccount.DropDown();
			return false;
		}
		if (((Control)(object)txtAssetBarCode).Text == "")
		{
			((Control)(object)txtAssetBarCode).Text = GetCode();
		}
		if (Assets.Check_Code(Adding ? "0" : ((KeyedSubObjectBase)SelectedNode).Key, ((Control)(object)txtAssetBarCode).Text))
		{
			string code = Assets.GetCode(IsFromServer: true);
			GlobalVariables.QuestionMB.Show("رقم هذا الأصل متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Asset Barcode Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtAssetBarCode).Focus();
				return false;
			}
			((Control)(object)txtAssetBarCode).Text = code;
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
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Expected O, but got Unknown
		TreeAccounts.AfterCheck -= new AfterNodeChangedEventHandler(Tree_AfterCheck);
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
				dataRow["AccountTypeID"] = dataRow2["AccountTypeID"];
				dtSubAccDetails.Rows.Add(dataRow);
			}
			GlobalFunctions.FillCombo(cboAssetAccount, dtSubAccDetails, "AccountID", "AccountName");
			GlobalFunctions.FillCombo(cboAssetAccumulatedDepreciationAccount, dtSubAccDetails, "AccountID", "AccountName");
			DataView dataView = new DataView(dtSubAccDetails);
			dataView.RowFilter = "AccountTypeID = 3";
			GlobalFunctions.FillCombo(cboAssetDepreciationExpenseAccount, dataView.ToTable(), "AccountID", "AccountName");
			GlobalFunctions.FillCombo(cboAssetInvestmentAccount, dtSubAccDetails, "AccountID", "AccountName");
			GlobalFunctions.FillCombo(cboAssetSalesAccount, dtSubAccDetails, "AccountID", "AccountName");
		}
		TreeAccounts.AfterCheck += new AfterNodeChangedEventHandler(Tree_AfterCheck);
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
		int num = SearchFunctions.Assets("-1", IsFromServer: true);
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
		string val = "";
		if (dtReports.Rows.Count > 0)
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
			val = dtReports.Rows[0]["isoCode"].ToString();
		}
		else
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_AST_Assets_A.rpt" : "Rep_AST_Assets_E.rpt"));
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txt2_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbersNegative(sender, e);
	}

	private void treeAssetLocation_BeforeCheck(object sender, BeforeCheckEventArgs e)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		if ((!Adding && !Updating) || (Updating && !CanModAdministrativeStructure))
		{
			((CancelEventArgs)(object)e).Cancel = true;
			return;
		}
		treeAssetLocation.BeforeCheck -= new BeforeCheckEventHandler(treeAssetLocation_BeforeCheck);
		TreeFunctions.SetAllTreeNodesCheckState(Checked: false, treeAssetLocation);
		treeAssetLocation.BeforeCheck += new BeforeCheckEventHandler(treeAssetLocation_BeforeCheck);
	}

	private void changeParentTSMenu_Click(object sender, EventArgs e)
	{
		if (SelectedNode != null)
		{
			frmUpdateParent frmUpdateParent2 = new frmUpdateParent(((KeyedSubObjectBase)SelectedNode).Key, (SelectedNode.Parent != null) ? ((KeyedSubObjectBase)SelectedNode.Parent).Key : "");
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

	private void modifyGroupEmployeesToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (SelectedNode == null)
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
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Expected O, but got Unknown
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Expected O, but got Unknown
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Expected O, but got Unknown
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Expected O, but got Unknown
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Expected O, but got Unknown
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Expected O, but got Unknown
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Expected O, but got Unknown
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Expected O, but got Unknown
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Expected O, but got Unknown
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Expected O, but got Unknown
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Expected O, but got Unknown
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Expected O, but got Unknown
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Expected O, but got Unknown
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Expected O, but got Unknown
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Expected O, but got Unknown
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Expected O, but got Unknown
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Expected O, but got Unknown
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Expected O, but got Unknown
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Expected O, but got Unknown
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Expected O, but got Unknown
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Expected O, but got Unknown
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Expected O, but got Unknown
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Expected O, but got Unknown
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Expected O, but got Unknown
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Expected O, but got Unknown
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Expected O, but got Unknown
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Expected O, but got Unknown
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Expected O, but got Unknown
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Expected O, but got Unknown
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Expected O, but got Unknown
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Expected O, but got Unknown
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Expected O, but got Unknown
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Expected O, but got Unknown
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Expected O, but got Unknown
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Expected O, but got Unknown
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Expected O, but got Unknown
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Expected O, but got Unknown
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Expected O, but got Unknown
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Expected O, but got Unknown
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Expected O, but got Unknown
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Expected O, but got Unknown
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Expected O, but got Unknown
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Expected O, but got Unknown
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Expected O, but got Unknown
		//IL_197c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1986: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Override val6 = new Override();
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
		UltraTab val26 = new UltraTab();
		UltraTab val27 = new UltraTab();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.FixedAssets.MasterData.frmAssetsTree));
		Override val28 = new Override();
		this.tabItem = new UltraTabPageControl();
		this.lblAdministrativeStructure = new UltraLabel();
		this.treeAssetLocation = new UltraTree();
		this.cboCustodySubAccount = new UltraComboEditor();
		this.lblCustodySubAccount = new UltraLabel();
		this.txtDepreciationPeriodCount = new UltraTextEditor();
		this.lblDepreciationPeriodCount = new UltraLabel();
		this.txtDepreciationPercentage = new UltraTextEditor();
		this.lblDepreciationPercentage = new UltraLabel();
		this.txtScrapValue = new UltraTextEditor();
		this.lblScrapValue = new UltraLabel();
		this.dtpStartDepreciationDate = new UltraDateTimeEditor();
		this.lblStartDepreciationDate = new UltraLabel();
		this.chkHasTransAction = new UltraCheckEditor();
		this.dtpSalesDate = new UltraDateTimeEditor();
		this.dtpScrapDate = new UltraDateTimeEditor();
		this.lblSalesDate = new UltraLabel();
		this.lblScrapDate = new UltraLabel();
		this.dtpAcquisitionDate = new UltraDateTimeEditor();
		this.lblAcquisitionDate = new UltraLabel();
		this.cboAssetDepreciationType = new UltraComboEditor();
		this.lblAssetDepreciationType = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblNotes = new UltraLabel();
		this.ultraTabPageControl6 = new UltraTabPageControl();
		this.cboAssetInvestmentAccount = new UltraComboEditor();
		this.cboAssetDepreciationExpenseAccount = new UltraComboEditor();
		this.lblAssetInvestmentAccount = new UltraLabel();
		this.lblAssetDepreciationExpenseAccount = new UltraLabel();
		this.cboAssetAccount = new UltraComboEditor();
		this.lblAssetAccount = new UltraLabel();
		this.cboAssetSalesAccount = new UltraComboEditor();
		this.lblAssetSalesAccount = new UltraLabel();
		this.cboAssetAccumulatedDepreciationAccount = new UltraComboEditor();
		this.lblAssetAccumulatedDepreciationAccount = new UltraLabel();
		this.txtAssetBarCode = new UltraTextEditor();
		this.lblAssetBarcode = new UltraLabel();
		this.cboAssetState = new UltraComboEditor();
		this.lblAssetState = new UltraLabel();
		this.tabItemType = new UltraTabControl();
		this.ultraTabSharedControlsPage1 = new UltraTabSharedControlsPage();
		this.ofdItemPic = new System.Windows.Forms.OpenFileDialog();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.changeParentTSMenu = new System.Windows.Forms.ToolStripMenuItem();
		this.setAsGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.setAsSubAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.modifyGroupEmployeesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.btnItemsSearch = new UltraButton();
		this.txtItems = new UltraTextEditor();
		this.lblAccounts = new UltraLabel();
		this.TreeAccounts = new UltraTree();
		((System.ComponentModel.ISupportInitialize)base.dtChart).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.treeChart).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtName).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabItem).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.treeAssetLocation).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCustodySubAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDepreciationPeriodCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDepreciationPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtScrapValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpStartDepreciationDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkHasTransAction).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpSalesDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpScrapDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpAcquisitionDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAssetDepreciationType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboAssetInvestmentAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAssetDepreciationExpenseAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAssetAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAssetSalesAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAssetAccumulatedDepreciationAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAssetBarCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAssetState).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tabItemType).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabItemType).SuspendLayout();
		this.contextMenuStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeAccounts).BeginInit();
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
		((AppearanceBase)val).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints");
		((ControlBase)base.label1).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(base.label1, "label1");
		((AppearanceBase)val2).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val2).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints1");
		((ControlBase)base.label2).Appearance = (AppearanceBase)(object)val2;
		resources.ApplyResources(base.label2, "label2");
		((AppearanceBase)val3).FontData.BoldAsString = resources.GetString("resource.BoldAsString2");
		((AppearanceBase)val3).FontData.Name = resources.GetString("resource.Name2");
		((AppearanceBase)val3).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints2");
		((ControlBase)base.label3).Appearance = (AppearanceBase)(object)val3;
		resources.ApplyResources(base.label3, "label3");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		((AppearanceBase)val4).FontData.BoldAsString = resources.GetString("resource.BoldAsString3");
		((AppearanceBase)val4).FontData.Name = resources.GetString("resource.Name3");
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Navy;
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val4;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblAdministrativeStructure);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.treeAssetLocation);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboCustodySubAccount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblCustodySubAccount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtDepreciationPeriodCount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblDepreciationPeriodCount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtDepreciationPercentage);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblDepreciationPercentage);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtScrapValue);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblScrapValue);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.dtpStartDepreciationDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblStartDepreciationDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.chkHasTransAction);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.dtpSalesDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.dtpScrapDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblScrapDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.dtpAcquisitionDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblAcquisitionDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboAssetDepreciationType);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblAssetDepreciationType);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		resources.ApplyResources(this.tabItem, "tabItem");
		((System.Windows.Forms.Control)(object)this.tabItem).Name = "tabItem";
		((AppearanceBase)val5).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblAdministrativeStructure).Appearance = (AppearanceBase)(object)val5;
		this.lblAdministrativeStructure.AutoEllipsis = false;
		resources.ApplyResources(this.lblAdministrativeStructure, "lblAdministrativeStructure");
		((System.Windows.Forms.Control)(object)this.lblAdministrativeStructure).Name = "lblAdministrativeStructure";
		((ControlBase)this.lblAdministrativeStructure).WrapText = false;
		resources.ApplyResources(this.treeAssetLocation, "treeAssetLocation");
		((System.Windows.Forms.Control)(object)this.treeAssetLocation).Name = "treeAssetLocation";
		val6.NodeStyle = (NodeStyle)1;
		this.treeAssetLocation.Override = val6;
		resources.ApplyResources(this.cboCustodySubAccount, "cboCustodySubAccount");
		((System.Windows.Forms.Control)(object)this.cboCustodySubAccount).Name = "cboCustodySubAccount";
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblCustodySubAccount).Appearance = (AppearanceBase)(object)val7;
		this.lblCustodySubAccount.AutoEllipsis = false;
		resources.ApplyResources(this.lblCustodySubAccount, "lblCustodySubAccount");
		((System.Windows.Forms.Control)(object)this.lblCustodySubAccount).Name = "lblCustodySubAccount";
		((ControlBase)this.lblCustodySubAccount).WrapText = false;
		resources.ApplyResources(this.txtDepreciationPeriodCount, "txtDepreciationPeriodCount");
		((System.Windows.Forms.Control)(object)this.txtDepreciationPeriodCount).Name = "txtDepreciationPeriodCount";
		resources.ApplyResources(this.lblDepreciationPeriodCount, "lblDepreciationPeriodCount");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblDepreciationPeriodCount).Appearance = (AppearanceBase)(object)val8;
		this.lblDepreciationPeriodCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDepreciationPeriodCount).Name = "lblDepreciationPeriodCount";
		((ControlBase)this.lblDepreciationPeriodCount).WrapText = false;
		resources.ApplyResources(this.txtDepreciationPercentage, "txtDepreciationPercentage");
		((System.Windows.Forms.Control)(object)this.txtDepreciationPercentage).Name = "txtDepreciationPercentage";
		((System.Windows.Forms.Control)(object)this.txtDepreciationPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt2_KeyPress);
		resources.ApplyResources(this.lblDepreciationPercentage, "lblDepreciationPercentage");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblDepreciationPercentage).Appearance = (AppearanceBase)(object)val9;
		this.lblDepreciationPercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDepreciationPercentage).Name = "lblDepreciationPercentage";
		((ControlBase)this.lblDepreciationPercentage).WrapText = false;
		resources.ApplyResources(this.txtScrapValue, "txtScrapValue");
		((System.Windows.Forms.Control)(object)this.txtScrapValue).Name = "txtScrapValue";
		((System.Windows.Forms.Control)(object)this.txtScrapValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt2_KeyPress);
		resources.ApplyResources(this.lblScrapValue, "lblScrapValue");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblScrapValue).Appearance = (AppearanceBase)(object)val10;
		this.lblScrapValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblScrapValue).Name = "lblScrapValue";
		((ControlBase)this.lblScrapValue).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpStartDepreciationDate).AlwaysInEditMode = true;
		this.dtpStartDepreciationDate.DateTime = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		resources.ApplyResources(this.dtpStartDepreciationDate, "dtpStartDepreciationDate");
		((System.Windows.Forms.Control)(object)this.dtpStartDepreciationDate).Name = "dtpStartDepreciationDate";
		this.dtpStartDepreciationDate.Value = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblStartDepreciationDate).Appearance = (AppearanceBase)(object)val11;
		this.lblStartDepreciationDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblStartDepreciationDate, "lblStartDepreciationDate");
		((System.Windows.Forms.Control)(object)this.lblStartDepreciationDate).Name = "lblStartDepreciationDate";
		((ControlBase)this.lblStartDepreciationDate).WrapText = false;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		((UltraToggleEditorBase)this.chkHasTransAction).Appearance = (AppearanceBase)(object)val12;
		resources.ApplyResources(this.chkHasTransAction, "chkHasTransAction");
		((System.Windows.Forms.Control)(object)this.chkHasTransAction).Name = "chkHasTransAction";
		((UltraWinEditorMaskedControlBase)this.dtpSalesDate).AlwaysInEditMode = true;
		this.dtpSalesDate.DateTime = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		resources.ApplyResources(this.dtpSalesDate, "dtpSalesDate");
		((System.Windows.Forms.Control)(object)this.dtpSalesDate).Name = "dtpSalesDate";
		this.dtpSalesDate.Value = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		((UltraWinEditorMaskedControlBase)this.dtpScrapDate).AlwaysInEditMode = true;
		this.dtpScrapDate.DateTime = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		resources.ApplyResources(this.dtpScrapDate, "dtpScrapDate");
		((System.Windows.Forms.Control)(object)this.dtpScrapDate).Name = "dtpScrapDate";
		this.dtpScrapDate.Value = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblSalesDate).Appearance = (AppearanceBase)(object)val13;
		this.lblSalesDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblSalesDate, "lblSalesDate");
		((System.Windows.Forms.Control)(object)this.lblSalesDate).Name = "lblSalesDate";
		((ControlBase)this.lblSalesDate).WrapText = false;
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblScrapDate).Appearance = (AppearanceBase)(object)val14;
		this.lblScrapDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblScrapDate, "lblScrapDate");
		((System.Windows.Forms.Control)(object)this.lblScrapDate).Name = "lblScrapDate";
		((ControlBase)this.lblScrapDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpAcquisitionDate).AlwaysInEditMode = true;
		this.dtpAcquisitionDate.DateTime = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		resources.ApplyResources(this.dtpAcquisitionDate, "dtpAcquisitionDate");
		((System.Windows.Forms.Control)(object)this.dtpAcquisitionDate).Name = "dtpAcquisitionDate";
		this.dtpAcquisitionDate.Value = new System.DateTime(2016, 12, 12, 0, 0, 0, 0);
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblAcquisitionDate).Appearance = (AppearanceBase)(object)val15;
		this.lblAcquisitionDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblAcquisitionDate, "lblAcquisitionDate");
		((System.Windows.Forms.Control)(object)this.lblAcquisitionDate).Name = "lblAcquisitionDate";
		((ControlBase)this.lblAcquisitionDate).WrapText = false;
		resources.ApplyResources(this.cboAssetDepreciationType, "cboAssetDepreciationType");
		((System.Windows.Forms.Control)(object)this.cboAssetDepreciationType).Name = "cboAssetDepreciationType";
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblAssetDepreciationType).Appearance = (AppearanceBase)(object)val16;
		this.lblAssetDepreciationType.AutoEllipsis = false;
		resources.ApplyResources(this.lblAssetDepreciationType, "lblAssetDepreciationType");
		((System.Windows.Forms.Control)(object)this.lblAssetDepreciationType).Name = "lblAssetDepreciationType";
		((ControlBase)this.lblAssetDepreciationType).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblNotes).Appearance = (AppearanceBase)(object)val17;
		this.lblNotes.AutoEllipsis = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.cboAssetInvestmentAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.cboAssetDepreciationExpenseAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.lblAssetInvestmentAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.lblAssetDepreciationExpenseAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.cboAssetAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.lblAssetAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.cboAssetSalesAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.lblAssetSalesAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.cboAssetAccumulatedDepreciationAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.lblAssetAccumulatedDepreciationAccount);
		resources.ApplyResources(this.ultraTabPageControl6, "ultraTabPageControl6");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Name = "ultraTabPageControl6";
		resources.ApplyResources(this.cboAssetInvestmentAccount, "cboAssetInvestmentAccount");
		((System.Windows.Forms.Control)(object)this.cboAssetInvestmentAccount).Name = "cboAssetInvestmentAccount";
		resources.ApplyResources(this.cboAssetDepreciationExpenseAccount, "cboAssetDepreciationExpenseAccount");
		((System.Windows.Forms.Control)(object)this.cboAssetDepreciationExpenseAccount).Name = "cboAssetDepreciationExpenseAccount";
		resources.ApplyResources(this.lblAssetInvestmentAccount, "lblAssetInvestmentAccount");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val18).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblAssetInvestmentAccount).Appearance = (AppearanceBase)(object)val18;
		this.lblAssetInvestmentAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAssetInvestmentAccount).Name = "lblAssetInvestmentAccount";
		((ControlBase)this.lblAssetInvestmentAccount).WrapText = false;
		resources.ApplyResources(this.lblAssetDepreciationExpenseAccount, "lblAssetDepreciationExpenseAccount");
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblAssetDepreciationExpenseAccount).Appearance = (AppearanceBase)(object)val19;
		this.lblAssetDepreciationExpenseAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAssetDepreciationExpenseAccount).Name = "lblAssetDepreciationExpenseAccount";
		((ControlBase)this.lblAssetDepreciationExpenseAccount).WrapText = false;
		resources.ApplyResources(this.cboAssetAccount, "cboAssetAccount");
		((System.Windows.Forms.Control)(object)this.cboAssetAccount).Name = "cboAssetAccount";
		resources.ApplyResources(this.lblAssetAccount, "lblAssetAccount");
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblAssetAccount).Appearance = (AppearanceBase)(object)val20;
		this.lblAssetAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAssetAccount).Name = "lblAssetAccount";
		((ControlBase)this.lblAssetAccount).WrapText = false;
		resources.ApplyResources(this.cboAssetSalesAccount, "cboAssetSalesAccount");
		((System.Windows.Forms.Control)(object)this.cboAssetSalesAccount).Name = "cboAssetSalesAccount";
		resources.ApplyResources(this.lblAssetSalesAccount, "lblAssetSalesAccount");
		((AppearanceBase)val21).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val21).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblAssetSalesAccount).Appearance = (AppearanceBase)(object)val21;
		this.lblAssetSalesAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAssetSalesAccount).Name = "lblAssetSalesAccount";
		((ControlBase)this.lblAssetSalesAccount).WrapText = false;
		resources.ApplyResources(this.cboAssetAccumulatedDepreciationAccount, "cboAssetAccumulatedDepreciationAccount");
		((System.Windows.Forms.Control)(object)this.cboAssetAccumulatedDepreciationAccount).Name = "cboAssetAccumulatedDepreciationAccount";
		resources.ApplyResources(this.lblAssetAccumulatedDepreciationAccount, "lblAssetAccumulatedDepreciationAccount");
		((AppearanceBase)val22).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val22).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblAssetAccumulatedDepreciationAccount).Appearance = (AppearanceBase)(object)val22;
		this.lblAssetAccumulatedDepreciationAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAssetAccumulatedDepreciationAccount).Name = "lblAssetAccumulatedDepreciationAccount";
		((ControlBase)this.lblAssetAccumulatedDepreciationAccount).WrapText = false;
		resources.ApplyResources(this.txtAssetBarCode, "txtAssetBarCode");
		((System.Windows.Forms.Control)(object)this.txtAssetBarCode).Name = "txtAssetBarCode";
		((AppearanceBase)val23).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val23).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblAssetBarcode).Appearance = (AppearanceBase)(object)val23;
		this.lblAssetBarcode.AutoEllipsis = false;
		resources.ApplyResources(this.lblAssetBarcode, "lblAssetBarcode");
		((System.Windows.Forms.Control)(object)this.lblAssetBarcode).Name = "lblAssetBarcode";
		((ControlBase)this.lblAssetBarcode).WrapText = false;
		resources.ApplyResources(this.cboAssetState, "cboAssetState");
		((System.Windows.Forms.Control)(object)this.cboAssetState).Name = "cboAssetState";
		((AppearanceBase)val24).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val24).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblAssetState).Appearance = (AppearanceBase)(object)val24;
		this.lblAssetState.AutoEllipsis = false;
		resources.ApplyResources(this.lblAssetState, "lblAssetState");
		((System.Windows.Forms.Control)(object)this.lblAssetState).Name = "lblAssetState";
		((ControlBase)this.lblAssetState).WrapText = false;
		resources.ApplyResources(this.tabItemType, "tabItemType");
		((AppearanceBase)val25).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val25).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.tabItemType).Appearance = (AppearanceBase)(object)val25;
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.tabItem);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl6);
		((System.Windows.Forms.Control)(object)this.tabItemType).Name = "tabItemType";
		((UltraTabControlBase)this.tabItemType).SharedControlsPage = this.ultraTabSharedControlsPage1;
		((UltraTabControlBase)this.tabItemType).TabOrientation = (TabOrientation)2;
		((KeyedSubObjectBase)val26).Key = "Details";
		val26.TabPage = this.tabItem;
		resources.ApplyResources(val26, "ultraTab1");
		((SubObjectBase)val26).ForceApplyResources = "";
		((KeyedSubObjectBase)val27).Key = "Accounts";
		val27.TabPage = this.ultraTabPageControl6;
		resources.ApplyResources(val27, "ultraTab3");
		((SubObjectBase)val27).ForceApplyResources = "";
		((UltraTabControlBase)this.tabItemType).Tabs.AddRange((UltraTab[])(object)new UltraTab[2] { val26, val27 });
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		this.ofdItemPic.FileName = "openFileDialog1";
		resources.ApplyResources(this.ofdItemPic, "ofdItemPic");
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.changeParentTSMenu, this.setAsGroupToolStripMenuItem, this.setAsSubAccountToolStripMenuItem, this.modifyGroupEmployeesToolStripMenuItem });
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
		this.modifyGroupEmployeesToolStripMenuItem.Name = "modifyGroupEmployeesToolStripMenuItem";
		resources.ApplyResources(this.modifyGroupEmployeesToolStripMenuItem, "modifyGroupEmployeesToolStripMenuItem");
		this.modifyGroupEmployeesToolStripMenuItem.Click += new System.EventHandler(modifyGroupEmployeesToolStripMenuItem_Click);
		resources.ApplyResources(this.btnItemsSearch, "btnItemsSearch");
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Name = "btnItemsSearch";
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Click += new System.EventHandler(btnItemsSearch_Click);
		resources.ApplyResources(this.txtItems, "txtItems");
		((System.Windows.Forms.Control)(object)this.txtItems).Name = "txtItems";
		((TextEditorControlBase)this.txtItems).ValueChanged += new System.EventHandler(txtItems_ValueChanged);
		resources.ApplyResources(this.lblAccounts, "lblAccounts");
		((System.Windows.Forms.Control)(object)this.lblAccounts).Name = "lblAccounts";
		resources.ApplyResources(this.TreeAccounts, "TreeAccounts");
		((System.Windows.Forms.Control)(object)this.TreeAccounts).Name = "TreeAccounts";
		val28.NodeStyle = (NodeStyle)1;
		this.TreeAccounts.Override = val28;
		this.TreeAccounts.AfterCheck += new AfterNodeChangedEventHandler(Tree_AfterCheck);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAssetBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAssetBarcode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.tabItemType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboAssetState);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAssetState);
		base.Name = "frmAssetsTree";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAssetState, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboAssetState, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.tabItemType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAssetBarcode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAssetBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblPath, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		((System.ComponentModel.ISupportInitialize)base.dtChart).EndInit();
		((System.ComponentModel.ISupportInitialize)base.treeChart).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtName).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.tabItem).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.tabItem).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.treeAssetLocation).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCustodySubAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDepreciationPeriodCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDepreciationPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtScrapValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpStartDepreciationDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkHasTransAction).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpSalesDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpScrapDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpAcquisitionDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAssetDepreciationType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cboAssetInvestmentAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAssetDepreciationExpenseAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAssetAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAssetSalesAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAssetAccumulatedDepreciationAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAssetBarCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAssetState).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tabItemType).EndInit();
		((System.Windows.Forms.Control)(object)this.tabItemType).ResumeLayout(false);
		this.contextMenuStrip1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeAccounts).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
