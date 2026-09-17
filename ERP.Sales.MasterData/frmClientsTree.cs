using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.EInvoices;
using BusinessLayer.General;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Accounting.MasterData;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;
using Infragistics.Win.UltraWinTree;

namespace ERP.Sales.MasterData;

public class frmClientsTree : frmTree2
{
	private UltraTextEditor txtPassword = new UltraTextEditor();

	private DataTable dtAccounts;

	private DataTable dtEINVSubAccountsTypes;

	private DataTable dtSubAccountType;

	private DataTable dtContacts;

	private DataTable dtBanks;

	private DataTable dtLines;

	private DataTable dtCards;

	private DataTable dtTitle;

	private DataTable dtPaymentMethod;

	private DataTable dtSubAccountClassification;

	private DataTable dtPriceType;

	private DataTable dtSubAccDetails;

	private DataTable dtCities;

	private DataTable dtAreas;

	private DataTable dtBranchs;

	private DataTable dtPosition;

	private DataTable dtSalesMan;

	private ValueList vlPosition = new ValueList();

	private string ClintSupplierID = "-1";

	private bool AddFromAnotherForm = false;

	public decimal SubAccountID = default(decimal);

	private bool UseElectronicInvoice = false;

	private IContainer components = null;

	public UltraButton btnItemsSearch;

	private UltraTextEditor txtItems;

	public UltraTree TreeAccounts;

	public UltraTabControl tabItemType;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl tabItem;

	private UltraComboEditor cboType;

	private UltraComboEditor cboTitle;

	private UltraLabel lblType;

	private UltraTextEditor txtTel;

	private UltraLabel lblClintSupplierTitle;

	private UltraTextEditor txtMobile;

	private UltraLabel lblMax;

	private UltraTextEditor txtFax;

	private UltraLabel lblReorder;

	private UltraLabel lblMin;

	private UltraTabPageControl tabService;

	private UltraTabPageControl tabRecipe;

	public UltraGrid ULGContacts;

	private UltraLabel lblAccounts;

	private UltraTextEditor txtAddress;

	private UltraLabel lblAddress;

	private UltraLabel lblBirthDate;

	private UltraDateTimeEditor dtpBirthDate;

	private UltraTextEditor txtEMail;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraTextEditor txtDiscountPercentage;

	private UltraTextEditor txtCreditLimit;

	private UltraLabel lblEMail;

	private UltraLabel lblDiscountPercentage;

	private UltraLabel lblCreditLimit;

	private UltraComboEditor cboSupplierAcc;

	private UltraComboEditor cboClientAcc;

	private UltraLabel lblSupplierAcc;

	private UltraComboEditor cboPriceType;

	private UltraComboEditor cboDefaultPaymentMethod;

	private UltraLabel lblClientAccount;

	private UltraLabel lblPriceType;

	private UltraLabel lblDefaultPaymentMethod;

	private UltraTextEditor txtNo;

	private UltraLabel ultraLabel1;

	private UltraComboEditor cboCity;

	private UltraLabel lblCity;

	private UltraTextEditor txtDiscountAfterTaxPercentage;

	private UltraLabel lblDiscountAfterTaxRatio;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem changeParentTSMenu;

	private ToolStripMenuItem setAsGroupToolStripMenuItem;

	private ToolStripMenuItem setAsSubAccountToolStripMenuItem;

	private UltraComboEditor cboArea;

	private UltraLabel lblArea;

	private UltraLabel lblBranch;

	private UltraComboEditor cboBranch;

	private UltraTabPageControl ultraTabPageControl1;

	public UltraGrid ULGCards;

	private UltraTabPageControl ultraTabPageControl2;

	private UltraLabel lblDefaultSalesMan;

	private UltraTextEditor txtLicenseNo;

	private UltraTextEditor txtTaxNo;

	private UltraLabel lblTaxNo;

	private UltraLabel lblLicenseNo;

	private UltraTextEditor txtCommercialRegistrationNo;

	private UltraTextEditor txtCompanyName;

	private UltraLabel lblCompanyName;

	private UltraComboEditor cboDefaultSalesMan;

	private UltraLabel lblCommercialRegistrationNo;

	private UltraCheckEditor chkDiscountTax;

	private UltraCheckEditor chkAddedTax;

	private UltraTabPageControl ultraTabPageControl3;

	public UltraGrid ULGBanks;

	private UltraCheckEditor chkForAllBranches;

	private UltraComboEditor cboLine;

	private UltraLabel lblLine;

	private UltraComboEditor cboClassification;

	private UltraLabel lblClassification;

	private UltraCheckEditor chkIsActive;

	private UltraDateTimeEditor dtpStopDate;

	private UltraLabel ultraLabel2;

	private UltraTextEditor txtProductionDiscountPercentage;

	private UltraLabel ultraLabel3;

	private UltraLabel lblEINVSubAccountType;

	private UltraComboEditor cboEINVSubAccountType;

	private UltraTextEditor txtBuildingNumber;

	private UltraLabel lblBuildingNumber;

	public frmClientsTree(bool _AddFromAnotherForm)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
		txtPassword.PasswordChar = '*';
		IDCol = "SubAccountID";
		NoCol = "SubAccountNumber";
		NameCol = "SubAccountNameAr";
		NameEnCol = "SubAccountNameEn";
		ParentIDCol = "ParentID";
		IsMainCol = "IsMain";
		AdditionalCol1 = "SubAccountTypeID";
		AdditionalCol2 = "BranchID";
		AdditionalCol3 = "ForAllBranches";
		ItemLevelCol = "LevelID";
		TableName = "A_SubAccounts";
		LevelsTable = "A_SubAccounts_Levels";
		LevelsCol = "LevelID";
		LevelsWidthCol = "Width";
		AddFromAnotherForm = _AddFromAnotherForm;
		AllowAddRoot = false;
	}

	public frmClientsTree()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
		txtPassword.PasswordChar = '*';
		IDCol = "SubAccountID";
		NoCol = "SubAccountNumber";
		NameCol = "SubAccountNameAr";
		NameEnCol = "SubAccountNameEn";
		ParentIDCol = "ParentID";
		IsMainCol = "IsMain";
		AdditionalCol1 = "SubAccountTypeID";
		AdditionalCol2 = "BranchID";
		AdditionalCol3 = "ForAllBranches";
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
			dtChart = SubAccounts.FillTreeBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, GlobalVariables.BranchIDs, IsFromServer: true);
			treeChart.Nodes.Clear();
			if (dtChart.Rows.Count > 0)
			{
				FillTree("0");
			}
		}
		UseElectronicInvoice = GlobalFunctions.GetOption("UsingElectronicInvoice");
		if (UseElectronicInvoice)
		{
			dtEINVSubAccountsTypes = SubAccountsTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboEINVSubAccountType, dtEINVSubAccountsTypes, "EINVSubAccountTypeID", "EINVSubAccountTypeName");
		}
		dtLines = Lines.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboLine, dtLines, "LineID", "LineName");
		dtAccounts = Accounts.Select("-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		TreeFunctions.FillTree(TreeAccounts, dtAccounts, "ParentID", "AccountID", GlobalVariables.IsArabic ? "AccountNameAr" : "AccountNameEn", "AccountNumber", "IsMain");
		dtSubAccountType = SuAccountsTypes.SelectBySuAccountsTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0");
		GlobalFunctions.FillCombo(cboType, dtSubAccountType, "SubAccountTypeID", GlobalVariables.IsArabic ? "SubAccountTypeNameAr" : "SubAccountTypeNameEn");
		dtTitle = Titles.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboTitle, dtTitle, "TitleID", GlobalVariables.IsArabic ? "TitleNameAr" : "TitleNameEn");
		dtCities = Cities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCity, dtCities, "CityID", "CityName");
		dtAreas = Areas.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboArea, dtAreas, "AreaID", "AreaName");
		dtBranchs = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboBranch, dtBranchs, "BranchID", "BranchName");
		dtPaymentMethod = PaymentMethods.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboDefaultPaymentMethod, dtPaymentMethod, "PaymentMethodID", GlobalVariables.IsArabic ? "PaymentMethodNameAr" : "PaymentMethodNameEn");
		dtSubAccountClassification = SubAccounts_Classifications.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboClassification, dtSubAccountClassification, "SubAccountClassificationID", "SubAccountClassificationName");
		dtPriceType = PricesTypes.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", GlobalVariables.IsArabic ? "PriceNameAr" : "PriceNameEn");
		dtPosition = Positions.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlPosition.ValueListItems.Clear();
		for (int i = 0; i < dtPosition.Rows.Count; i++)
		{
			vlPosition.ValueListItems.Add(dtPosition.Rows[i]["PositionID"], dtPosition.Rows[i]["PositionName"].ToString());
		}
		dtSalesMan = SubAccounts.SelectBySubAccountTypeIDs(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboDefaultSalesMan, dtSalesMan, "SubAccountID", "SubAccountName");
		dtSubAccDetails = SubAccounts_Details.SelectBySubAccountIDWithAccountName("0", "1", IsFromServer: true);
		dtContacts = SubAccountsClientSupplierContacts.SelectBySubAccountID("0", "1", IsFromServer: true);
		InitGridContacts();
		dtBanks = SubAccountsClientSupplierBanks.SelectBySubAccountID("0", "1", IsFromServer: true);
		InitGridBanks();
		dtCards = SubAccountsClientCards.SelectBySubAccountID("0", "1", IsFromServer: true);
		InitGridCards();
		((Control)(object)TreeAccounts).Enabled = false;
		((EditorButtonControlBase)cboSupplierAcc).ReadOnly = true;
		((EditorButtonControlBase)cboClientAcc).ReadOnly = true;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		if (Adding)
		{
			((Control)(object)txtCode).Text = SubAccounts.GetCode((SelectedNode == null) ? "Null" : ((KeyedSubObjectBase)SelectedNode).Key, IsFromServer: true);
			((Control)(object)txtNo).Text = SubAccountsClientSupplier.GetCode(IsFromServer: true);
			((TextEditorControlBase)cboType).Value = 4;
			((Control)(object)txtName).Select();
		}
		dtContacts.Rows.Clear();
		dtBanks.Rows.Clear();
		dtCards.Rows.Clear();
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		if (AddFromAnotherForm)
		{
			((Control)(object)btnOK).Visible = false;
		}
		bool flag = false;
		if (Updating)
		{
			flag = SubAccounts.UpdateBranchCheck(((KeyedSubObjectBase)SelectedNode).Key);
		}
		((EditorButtonControlBase)cboTitle).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpBirthDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpStopDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAddress).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBuildingNumber).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTel).ReadOnly = NavMode;
		((EditorButtonControlBase)txtFax).ReadOnly = NavMode;
		((EditorButtonControlBase)txtMobile).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEMail).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCreditLimit).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNo).ReadOnly = NavMode;
		((EditorButtonControlBase)cboDefaultPaymentMethod).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCity).ReadOnly = NavMode;
		((EditorButtonControlBase)cboArea).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBranch).ReadOnly = NavMode || flag;
		((EditorButtonControlBase)cboPriceType).ReadOnly = NavMode || !CanModifyPriceType;
		((EditorButtonControlBase)txtDiscountPercentage).ReadOnly = NavMode || !CanModifyPriceType;
		((EditorButtonControlBase)txtDiscountAfterTaxPercentage).ReadOnly = NavMode || !CanModifyPriceType;
		((EditorButtonControlBase)txtProductionDiscountPercentage).ReadOnly = NavMode || !CanModifyPriceType;
		((EditorButtonControlBase)cboLine).ReadOnly = NavMode;
		((Control)(object)chkAddedTax).Enabled = !NavMode;
		((Control)(object)chkIsActive).Enabled = !NavMode;
		((Control)(object)chkDiscountTax).Enabled = !NavMode;
		((Control)(object)chkForAllBranches).Enabled = !NavMode;
		((EditorButtonControlBase)cboClassification).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCompanyName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCommercialRegistrationNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTaxNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtLicenseNo).ReadOnly = NavMode;
		((EditorButtonControlBase)cboDefaultSalesMan).ReadOnly = NavMode;
		((EditorButtonControlBase)cboEINVSubAccountType).ReadOnly = NavMode;
		((Control)(object)cboEINVSubAccountType).Visible = UseElectronicInvoice;
		((Control)(object)lblEINVSubAccountType).Visible = UseElectronicInvoice;
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
		((UltraGridBase)ULGContacts).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((UltraGridBase)ULGBanks).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((UltraGridBase)ULGCards).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
	}

	public override void DisplayData()
	{
		ClintSupplierID = "-1";
		base.DisplayData();
		if (SelectedNode != null)
		{
			cboTitle.SelectedIndex = -1;
			dtpBirthDate.DateTime = DateTime.Now;
			dtpStopDate.Value = null;
			((TextEditorControlBase)txtAddress).Clear();
			((TextEditorControlBase)txtBuildingNumber).Clear();
			((TextEditorControlBase)txtTel).Clear();
			((TextEditorControlBase)txtFax).Clear();
			((TextEditorControlBase)txtMobile).Clear();
			((TextEditorControlBase)txtEMail).Clear();
			((TextEditorControlBase)txtCreditLimit).Clear();
			((TextEditorControlBase)txtDiscountPercentage).Clear();
			((TextEditorControlBase)txtDiscountAfterTaxPercentage).Clear();
			((TextEditorControlBase)txtProductionDiscountPercentage).Clear();
			((TextEditorControlBase)txtNotes).Clear();
			((TextEditorControlBase)txtNo).Clear();
			((TextEditorControlBase)cboDefaultPaymentMethod).Clear();
			((TextEditorControlBase)cboCity).Clear();
			((TextEditorControlBase)cboArea).Clear();
			((TextEditorControlBase)cboClientAcc).Clear();
			((TextEditorControlBase)cboSupplierAcc).Clear();
			((TextEditorControlBase)cboPriceType).Clear();
			((TextEditorControlBase)cboLine).Clear();
			((TextEditorControlBase)cboClassification).Clear();
			((UltraToggleEditorBase)chkAddedTax).Checked = false;
			((UltraToggleEditorBase)chkDiscountTax).Checked = false;
			((UltraToggleEditorBase)chkForAllBranches).Checked = true;
			((UltraToggleEditorBase)chkIsActive).Checked = false;
			((TextEditorControlBase)txtCompanyName).Clear();
			((TextEditorControlBase)txtCommercialRegistrationNo).Clear();
			((TextEditorControlBase)txtTaxNo).Clear();
			((TextEditorControlBase)txtLicenseNo).Clear();
			((TextEditorControlBase)cboDefaultSalesMan).Clear();
			((TextEditorControlBase)cboEINVSubAccountType).Clear();
			((TextEditorControlBase)cboType).Value = ((DataRow)((SubObjectBase)SelectedNode).Tag)[AdditionalCol1].ToString();
			((TextEditorControlBase)cboBranch).Value = ((DataRow)((SubObjectBase)SelectedNode).Tag)[AdditionalCol2].ToString();
			((UltraToggleEditorBase)chkForAllBranches).Checked = Convert.ToBoolean(((DataRow)((SubObjectBase)SelectedNode).Tag)[AdditionalCol3]);
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
			GlobalFunctions.FillCombo(cboClientAcc, dtSubAccDetails, "AccountID", "AccountName");
			GlobalFunctions.FillCombo(cboSupplierAcc, dtSubAccDetails, "AccountID", "AccountName");
			DisplayClientSupplierData();
		}
	}

	public void DisplayClientSupplierData()
	{
		ClintSupplierID = "-1";
		DataTable dataTable = SubAccountsClientSupplier.SelectBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.Rows[0];
			ClintSupplierID = dataRow["ClientSupplierID"].ToString();
			((TextEditorControlBase)cboTitle).Value = dataRow["TitleID"];
			dtpBirthDate.Value = dataRow["BirthDate"];
			dtpStopDate.Value = dataRow["StopDate"];
			((Control)(object)txtNo).Text = dataRow["ClientSupplierNo"].ToString();
			((Control)(object)txtAddress).Text = dataRow["Address"].ToString();
			((Control)(object)txtBuildingNumber).Text = dataRow["BuildingNumber"].ToString();
			((Control)(object)txtTel).Text = dataRow["Tel"].ToString();
			((Control)(object)txtFax).Text = dataRow["Fax"].ToString();
			((Control)(object)txtMobile).Text = dataRow["Mobile"].ToString();
			((Control)(object)txtEMail).Text = dataRow["EMail"].ToString();
			((Control)(object)txtCreditLimit).Text = dataRow["CreditLimit"].ToString();
			((Control)(object)txtDiscountPercentage).Text = dataRow["DiscountPercentage"].ToString();
			((Control)(object)txtDiscountAfterTaxPercentage).Text = dataRow["DiscountPercentageAfterTax"].ToString();
			((Control)(object)txtProductionDiscountPercentage).Text = dataRow["BlanksProductionDiscountPercentage"].ToString();
			((TextEditorControlBase)cboDefaultPaymentMethod).Value = dataRow["DefaultPaymentMethodID"].ToString();
			((TextEditorControlBase)cboCity).Value = dataRow["CityID"].ToString();
			((TextEditorControlBase)cboArea).Value = dataRow["AreaID"].ToString();
			((TextEditorControlBase)cboClientAcc).Value = dataRow["DefaultClientAccountID"].ToString();
			((TextEditorControlBase)cboSupplierAcc).Value = dataRow["DefaultSupplierAccountID"].ToString();
			((TextEditorControlBase)cboPriceType).Value = dataRow["PriceTypeID"].ToString();
			((TextEditorControlBase)cboLine).Value = dataRow["LineID"].ToString();
			((UltraToggleEditorBase)chkAddedTax).Checked = bool.Parse(dataRow["IsAddedTax"].ToString());
			((UltraToggleEditorBase)chkIsActive).Checked = bool.Parse(dataRow["IsActive"].ToString());
			((UltraToggleEditorBase)chkDiscountTax).Checked = bool.Parse(dataRow["IsDiscountTax"].ToString());
			((TextEditorControlBase)cboClassification).Value = dataRow["SubAccountClassificationID"].ToString();
			((Control)(object)txtCompanyName).Text = dataRow["CompanyName"].ToString();
			((Control)(object)txtCommercialRegistrationNo).Text = dataRow["CommercialRegistrationNo"].ToString();
			((Control)(object)txtTaxNo).Text = dataRow["TaxNo"].ToString();
			((Control)(object)txtLicenseNo).Text = dataRow["LicenseNo"].ToString();
			((TextEditorControlBase)cboDefaultSalesMan).Value = dataRow["EmployeeID"];
			((TextEditorControlBase)cboEINVSubAccountType).Value = dataRow["EINVSubAccountTypeID"];
			((Control)(object)txtNotes).Text = dataRow["Notes"].ToString();
			dtContacts = SubAccountsClientSupplierContacts.SelectBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, "1", IsFromServer: true);
			InitGridContacts();
			dtBanks = SubAccountsClientSupplierBanks.SelectBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, "1", IsFromServer: true);
			InitGridBanks();
			dtCards = SubAccountsClientCards.SelectBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, "1", IsFromServer: true);
			InitGridCards();
		}
	}

	private void InitGridCards()
	{
		((UltraGridBase)ULGCards).DataSource = dtCards;
		GlobalFunctions.PrepareGrid(ULGCards);
		((UltraGridBase)ULGCards).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGCards).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGCards).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGCards).DisplayLayout.Bands[0].Columns["SubAccountClientCardID"].DefaultCellValue = -1;
		((UltraGridBase)ULGCards).DisplayLayout.Bands[0].Columns["IsActive"].DefaultCellValue = true;
		((UltraGridBase)ULGCards).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = GlobalVariables.CurrentBranchID;
		((UltraGridBase)ULGCards).DisplayLayout.Bands[0].Columns["InsertDate"].DefaultCellValue = DateTime.Now;
		((HeaderBase)((UltraGridBase)ULGCards).DisplayLayout.Bands[0].Columns["CardCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود" : "Code");
		((UltraGridBase)ULGCards).DisplayLayout.Bands[0].Columns["CardCode"].Hidden = false;
		((UltraGridBase)ULGCards).DisplayLayout.Bands[0].Columns["CardCode"].EditorComponent = (Component)(object)txtPassword;
		((UltraGridBase)ULGCards).DisplayLayout.Bands[0].Columns["CardCode"].Width = (int)((double)((Control)(object)ULGCards).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGCards).DisplayLayout.Bands[0].Columns["IsActive"].Header).Caption = (GlobalVariables.IsArabic ? "نشط" : "Active");
		((UltraGridBase)ULGCards).DisplayLayout.Bands[0].Columns["IsActive"].Hidden = false;
		((UltraGridBase)ULGCards).DisplayLayout.Bands[0].Columns["IsActive"].Width = (int)((double)((Control)(object)ULGCards).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGCards).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGCards).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGCards).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGCards).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGCards).DisplayLayout.Bands[0].Columns["InsertDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGCards).DisplayLayout.Bands[0].Columns["InsertDate"].Hidden = false;
		((UltraGridBase)ULGCards).DisplayLayout.Bands[0].Columns["InsertDate"].Width = (int)((double)((Control)(object)ULGCards).Width * 0.2);
	}

	private void InitGridContacts()
	{
		((UltraGridBase)ULGContacts).DataSource = dtContacts;
		GlobalFunctions.PrepareGrid(ULGContacts);
		((UltraGridBase)ULGContacts).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGContacts).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ClientSupplierContactID"].DefaultCellValue = -1;
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = GlobalVariables.CurrentBranchID;
		((HeaderBase)((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactNo"].Header).Caption = (GlobalVariables.IsArabic ? "كود" : "Code");
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactNo"].Hidden = false;
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactNo"].Width = (int)((double)((Control)(object)ULGContacts).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactName"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم" : "Name");
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactName"].Hidden = false;
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactName"].Width = (int)((double)((Control)(object)ULGContacts).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["PositionID"].Header).Caption = (GlobalVariables.IsArabic ? "الوظيفة" : "Position");
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["PositionID"].Hidden = false;
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["PositionID"].ValueList = (IValueList)(object)vlPosition;
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["PositionID"].Width = (int)((double)((Control)(object)ULGContacts).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["BirthDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الميلاد" : "BirthDate");
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["BirthDate"].Hidden = false;
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["BirthDate"].Width = (int)((double)((Control)(object)ULGContacts).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactAddress"].Header).Caption = (GlobalVariables.IsArabic ? "العنوان" : "Address");
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactAddress"].Hidden = false;
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactAddress"].Width = (int)((double)((Control)(object)ULGContacts).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactTel"].Header).Caption = (GlobalVariables.IsArabic ? "تليفون" : "Tel");
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactTel"].Hidden = false;
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactTel"].Width = (int)((double)((Control)(object)ULGContacts).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactFax"].Header).Caption = (GlobalVariables.IsArabic ? "فاكس" : "Fax");
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactFax"].Hidden = false;
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactFax"].Width = (int)((double)((Control)(object)ULGContacts).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactMobile"].Header).Caption = (GlobalVariables.IsArabic ? "محمول" : "Mobile");
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactMobile"].Hidden = false;
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactMobile"].Width = (int)((double)((Control)(object)ULGContacts).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactEMail"].Header).Caption = (GlobalVariables.IsArabic ? "بريد إلكتروني" : "EMail");
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactEMail"].Hidden = false;
		((UltraGridBase)ULGContacts).DisplayLayout.Bands[0].Columns["ContactEMail"].Width = (int)((double)((Control)(object)ULGContacts).Width * 0.15);
	}

	private void InitGridBanks()
	{
		((UltraGridBase)ULGBanks).DataSource = dtBanks;
		GlobalFunctions.PrepareGrid(ULGBanks);
		((UltraGridBase)ULGBanks).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGBanks).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGBanks).DisplayLayout.Bands[0].Columns["ClientSupplierBankID"].DefaultCellValue = -1;
		((UltraGridBase)ULGBanks).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = GlobalVariables.CurrentBranchID;
		((HeaderBase)((UltraGridBase)ULGBanks).DisplayLayout.Bands[0].Columns["BankName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم البنك" : "Bank Name");
		((UltraGridBase)ULGBanks).DisplayLayout.Bands[0].Columns["BankName"].Hidden = false;
		((UltraGridBase)ULGBanks).DisplayLayout.Bands[0].Columns["BankName"].Width = (int)((double)((Control)(object)ULGBanks).Width * 0.25) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGBanks).DisplayLayout.Bands[0].Columns["Address"].Header).Caption = (GlobalVariables.IsArabic ? "العنوان" : "Address");
		((UltraGridBase)ULGBanks).DisplayLayout.Bands[0].Columns["Address"].Hidden = false;
		((UltraGridBase)ULGBanks).DisplayLayout.Bands[0].Columns["Address"].Width = (int)((double)((Control)(object)ULGBanks).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGBanks).DisplayLayout.Bands[0].Columns["BranchCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود الفرع" : "Branch Code");
		((UltraGridBase)ULGBanks).DisplayLayout.Bands[0].Columns["BranchCode"].Hidden = false;
		((UltraGridBase)ULGBanks).DisplayLayout.Bands[0].Columns["BranchCode"].Width = (int)((double)((Control)(object)ULGBanks).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGBanks).DisplayLayout.Bands[0].Columns["AccountNumber"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الحساب" : "Account Number");
		((UltraGridBase)ULGBanks).DisplayLayout.Bands[0].Columns["AccountNumber"].Hidden = false;
		((UltraGridBase)ULGBanks).DisplayLayout.Bands[0].Columns["AccountNumber"].Width = (int)((double)((Control)(object)ULGBanks).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGBanks).DisplayLayout.Bands[0].Columns["AccountName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم الحساب" : "Account Name");
		((UltraGridBase)ULGBanks).DisplayLayout.Bands[0].Columns["AccountName"].Hidden = false;
		((UltraGridBase)ULGBanks).DisplayLayout.Bands[0].Columns["AccountName"].Width = (int)((double)((Control)(object)ULGBanks).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGBanks).DisplayLayout.Bands[0].Columns["SwiftCode"].Header).Caption = (GlobalVariables.IsArabic ? "Swift Code" : "Swift Code");
		((UltraGridBase)ULGBanks).DisplayLayout.Bands[0].Columns["SwiftCode"].Hidden = false;
		((UltraGridBase)ULGBanks).DisplayLayout.Bands[0].Columns["SwiftCode"].Width = (int)((double)((Control)(object)ULGBanks).Width * 0.15);
	}

	public override int TreeAddData()
	{
		int num = 0;
		Main.StartBulkTrans(FromServer: true);
		try
		{
			num = SubAccounts.Insert_Update("-1", GetCode(), ((Control)(object)txtName).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? "Null" : ((Control)(object)txtNameEn).Text.Trim(), (SelectedNode == null) ? "Null" : ((DataRow)((SubObjectBase)SelectedNode).Tag)[0].ToString(), "0", (NodeLevel + 1).ToString(), ((TextEditorControlBase)cboType).Value.ToString(), ((UltraToggleEditorBase)chkForAllBranches).Checked ? "1" : "0", "0", (cboBranch.SelectedIndex == -1) ? GlobalVariables.CurrentBranchID : ((TextEditorControlBase)cboBranch).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			SubAccounts_Details.Insert_UpdateByAccIDs(num.ToString(), TreeFunctions.GetTreeCheckedNodesIDs(TreeAccounts), "0", GlobalVariables.CurrentBranchID, IsFromServer: true);
			SubAccountsClientSupplier.Insert_Update("-1", (((Control)(object)txtNo).Text == "") ? GetCode() : ((Control)(object)txtNo).Text, (cboTitle.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTitle).Value.ToString(), num.ToString(), dtpBirthDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), ((Control)(object)txtBuildingNumber).Text, ((Control)(object)txtAddress).Text, ((Control)(object)txtTel).Text, ((Control)(object)txtFax).Text, ((Control)(object)txtMobile).Text, ((Control)(object)txtEMail).Text, (cboLine.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLine).Value.ToString(), (((Control)(object)txtCreditLimit).Text == "") ? "Null" : ((Control)(object)txtCreditLimit).Text, (((Control)(object)txtDiscountPercentage).Text == "") ? "Null" : ((Control)(object)txtDiscountPercentage).Text, (((Control)(object)txtDiscountAfterTaxPercentage).Text == "") ? "Null" : ((Control)(object)txtDiscountAfterTaxPercentage).Text, (((Control)(object)txtProductionDiscountPercentage).Text == "") ? "Null" : ((Control)(object)txtProductionDiscountPercentage).Text, (cboDefaultPaymentMethod.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDefaultPaymentMethod).Value.ToString(), (cboClientAcc.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClientAcc).Value.ToString(), (cboSupplierAcc.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSupplierAcc).Value.ToString(), (cboPriceType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), (cboClassification.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClassification).Value.ToString(), ((Control)(object)txtCompanyName).Text, ((Control)(object)txtCommercialRegistrationNo).Text, (cboEINVSubAccountType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboEINVSubAccountType).Value.ToString(), ((Control)(object)txtTaxNo).Text.Trim(), ((Control)(object)txtLicenseNo).Text, (cboDefaultSalesMan.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDefaultSalesMan).Value.ToString(), ((UltraToggleEditorBase)chkDiscountTax).Checked ? "1" : "0", ((UltraToggleEditorBase)chkAddedTax).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsActive).Checked ? "1" : "0", (dtpStopDate.Value == null) ? "Null" : dtpStopDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtNotes).Text, "0", (cboBranch.SelectedIndex == -1) ? GlobalVariables.CurrentBranchID : ((TextEditorControlBase)cboBranch).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			if (dtContacts.Rows.Count > 0)
			{
				for (int i = 0; i < dtContacts.Rows.Count; i++)
				{
					dtContacts.Rows[i]["SubAccountID"] = num;
				}
				SubAccountsClientSupplierContacts.Insert_UpdateByTable(dtContacts, GlobalVariables.UserID, IsFromServer: true);
			}
			if (dtBanks.Rows.Count > 0)
			{
				for (int j = 0; j < dtBanks.Rows.Count; j++)
				{
					dtBanks.Rows[j]["SubAccountID"] = num;
				}
				SubAccountsClientSupplierBanks.Insert_UpdateByTable(dtBanks, GlobalVariables.UserID, IsFromServer: true);
			}
			if (dtCards.Rows.Count > 0)
			{
				for (int k = 0; k < dtCards.Rows.Count; k++)
				{
					dtCards.Rows[k]["SubAccountID"] = num;
				}
				SubAccountsClientCards.Insert_UpdateByTable(dtCards, GlobalVariables.UserID, IsFromServer: true);
			}
			SubAccountID = num;
			Main.EndBulkTrans(FromServer: true);
			ClearControls();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
		return num;
	}

	public override void TreeUpdateData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			SubAccounts.Insert_Update(((KeyedSubObjectBase)SelectedNode).Key, GetCode(), ((Control)(object)txtName).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? "Null" : ((Control)(object)txtNameEn).Text.Trim(), (SelectedNode.Parent == null) ? "Null" : ((KeyedSubObjectBase)SelectedNode.Parent).Key, ((bool)((DataRow)((SubObjectBase)SelectedNode).Tag)[IsMainCol]) ? "1" : "0", NodeLevel.ToString(), ((TextEditorControlBase)cboType).Value.ToString(), ((UltraToggleEditorBase)chkForAllBranches).Checked ? "1" : "0", "0", (cboBranch.SelectedIndex == -1) ? GlobalVariables.CurrentBranchID : ((TextEditorControlBase)cboBranch).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
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
			SubAccountsClientSupplier.Insert_Update(ClintSupplierID, (((Control)(object)txtNo).Text == "") ? GetCode() : ((Control)(object)txtNo).Text, (cboTitle.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTitle).Value.ToString(), ((KeyedSubObjectBase)SelectedNode).Key, dtpBirthDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), ((Control)(object)txtBuildingNumber).Text, ((Control)(object)txtAddress).Text, ((Control)(object)txtTel).Text, ((Control)(object)txtFax).Text, ((Control)(object)txtMobile).Text, ((Control)(object)txtEMail).Text, (cboLine.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLine).Value.ToString(), (((Control)(object)txtCreditLimit).Text == "") ? "Null" : ((Control)(object)txtCreditLimit).Text, (((Control)(object)txtDiscountPercentage).Text == "") ? "Null" : ((Control)(object)txtDiscountPercentage).Text, (((Control)(object)txtDiscountAfterTaxPercentage).Text == "") ? "Null" : ((Control)(object)txtDiscountAfterTaxPercentage).Text, (((Control)(object)txtProductionDiscountPercentage).Text == "") ? "Null" : ((Control)(object)txtProductionDiscountPercentage).Text, (cboDefaultPaymentMethod.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDefaultPaymentMethod).Value.ToString(), (cboClientAcc.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClientAcc).Value.ToString(), (cboSupplierAcc.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSupplierAcc).Value.ToString(), (cboPriceType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), (cboClassification.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClassification).Value.ToString(), ((Control)(object)txtCompanyName).Text, ((Control)(object)txtCommercialRegistrationNo).Text, (cboEINVSubAccountType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboEINVSubAccountType).Value.ToString(), ((Control)(object)txtTaxNo).Text.Trim(), ((Control)(object)txtLicenseNo).Text, (cboDefaultSalesMan.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDefaultSalesMan).Value.ToString(), ((UltraToggleEditorBase)chkDiscountTax).Checked ? "1" : "0", ((UltraToggleEditorBase)chkAddedTax).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsActive).Checked ? "1" : "0", (dtpStopDate.Value == null) ? "Null" : dtpStopDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtNotes).Text, "0", (cboBranch.SelectedIndex == -1) ? GlobalVariables.CurrentBranchID : ((TextEditorControlBase)cboBranch).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGContacts).Rows).Count; i++)
			{
				text = text + ((UltraGridBase)ULGContacts).Rows[i].Cells["ClientSupplierContactID"].Value.ToString() + ",";
				((UltraGridBase)ULGContacts).Rows[i].Cells["SubAccountID"].Value = ((KeyedSubObjectBase)SelectedNode).Key;
				((UltraGridBase)ULGContacts).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			Main.SyncDeleteForUpdate("A_SubAccountsClientSupplierContacts", "SubAccountID", ((KeyedSubObjectBase)SelectedNode).Key, "ClientSupplierContactID", text, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGContacts).Rows).Count > 0)
			{
				SubAccountsClientSupplierContacts.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGContacts).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			string text2 = ",";
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGBanks).Rows).Count; j++)
			{
				text2 = text2 + ((UltraGridBase)ULGBanks).Rows[j].Cells["ClientSupplierBankID"].Value.ToString() + ",";
				((UltraGridBase)ULGBanks).Rows[j].Cells["SubAccountID"].Value = ((KeyedSubObjectBase)SelectedNode).Key;
				((UltraGridBase)ULGBanks).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			Main.SyncDeleteForUpdate("A_SubAccountsClientSupplierBanks", "SubAccountID", ((KeyedSubObjectBase)SelectedNode).Key, "ClientSupplierBankID", text2, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGBanks).Rows).Count > 0)
			{
				SubAccountsClientSupplierBanks.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGBanks).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			SubAccountsClientCards.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			if (dtCards.Rows.Count > 0)
			{
				for (int k = 0; k < dtCards.Rows.Count; k++)
				{
					dtCards.Rows[k]["SubAccountID"] = ((KeyedSubObjectBase)SelectedNode).Key;
					dtCards.Rows[k]["SubAccountClientCardID"] = -1;
				}
				SubAccountsClientCards.Insert_UpdateByTable(dtCards, GlobalVariables.UserID, IsFromServer: true);
			}
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
			SubAccountsClientCards.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			SubAccountsClientSupplierContacts.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			SubAccountsClientSupplierBanks.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			SubAccountsClientSupplier.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
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
		dtContacts.AcceptChanges();
		dtBanks.AcceptChanges();
		dtCards.AcceptChanges();
		if (cboType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك نوع الحساب ", "Enter SubAccount Type");
			((TextEditorControlBase)cboType).Focus();
			return false;
		}
		if (cboSupplierAcc.SelectedIndex == -1 && (((TextEditorControlBase)cboType).Value.ToString() == "3" || ((TextEditorControlBase)cboType).Value.ToString() == "5"))
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل الحساب الإفتراضي للمورد", "Enter Default Supplier Account");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["ClientSupplier"];
			((TextEditorControlBase)cboSupplierAcc).Focus();
			return false;
		}
		if (cboClientAcc.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل الحساب الإفتراضي للعميل", "Enter Default Client Account");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["ClientSupplier"];
			((TextEditorControlBase)cboClientAcc).Focus();
			return false;
		}
		if (((Control)(object)cboBranch).Visible && cboBranch.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك الفرع ", "Enter Branch");
			((TextEditorControlBase)cboBranch).Focus();
			return false;
		}
		if (!((UltraToggleEditorBase)chkIsActive).Checked && dtpStopDate.Value == null)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل تاريخ التوقف ", "Please Enter Stop Date");
			((Control)(object)dtpStopDate).Focus();
			return false;
		}
		if (UseElectronicInvoice)
		{
			if (cboEINVSubAccountType.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("من فضلك ادخل نوع الممول", "Please Select Type");
				((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Company"];
				((TextEditorControlBase)cboEINVSubAccountType).Focus();
				return false;
			}
			if (cboEINVSubAccountType.SelectedIndex == 0 && ((Control)(object)txtTaxNo).Text.Trim().Length != 9)
			{
				GlobalVariables.InformationMB.Show("من فضلك ادخل رقم ضريبي صحيح", "Please Enter A Valid Tax Number");
				((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Company"];
				((TextEditorControlBase)txtTaxNo).Focus();
				return false;
			}
			if (cboEINVSubAccountType.SelectedIndex == 1 && ((Control)(object)txtTaxNo).Text.Trim().Length > 0 && ((Control)(object)txtTaxNo).Text.Trim().Length != 14)
			{
				GlobalVariables.InformationMB.Show("من فضلك ادخل رقم ضريبي صحيح", "Please Enter A Valid Tax Number");
				((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Company"];
				((TextEditorControlBase)txtTaxNo).Focus();
				return false;
			}
			if (cboEINVSubAccountType.SelectedIndex == 0)
			{
				if (cboCity.SelectedIndex == -1)
				{
					GlobalVariables.InformationMB.Show("من فضلك ادخل المحافظة", "Please Select City");
					((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["ClientSupplier"];
					((TextEditorControlBase)cboCity).Focus();
					return false;
				}
				if (cboArea.SelectedIndex == -1)
				{
					GlobalVariables.InformationMB.Show("من فضلك ادخل المنطقة", "Please Select Area");
					((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["ClientSupplier"];
					((TextEditorControlBase)cboArea).Focus();
					return false;
				}
				if (((Control)(object)txtAddress).Text.Trim().Equals(""))
				{
					GlobalVariables.InformationMB.Show("من فضلك ادخل العنوان", "Please Enter Address");
					((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["ClientSupplier"];
					((TextEditorControlBase)txtAddress).Focus();
					return false;
				}
				if (((Control)(object)txtBuildingNumber).Text.Trim().Equals(""))
				{
					GlobalVariables.InformationMB.Show("من فضلك ادخل رقم المبنى", "Please Enter The Building Number");
					((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["ClientSupplier"];
					((TextEditorControlBase)txtBuildingNumber).Focus();
					return false;
				}
			}
		}
		if (((DataTable)((UltraGridBase)ULGContacts).DataSource).Rows.Count > 0)
		{
			for (int i = 0; i < ((DataTable)((UltraGridBase)ULGContacts).DataSource).Rows.Count; i++)
			{
				if (((UltraGridBase)ULGContacts).Rows[i].Cells["ContactNo"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("من فضلك ادخل كود جهه الإتصال", "Enter Contact No");
					ULGContacts.ActiveCell = ((UltraGridBase)ULGContacts).Rows[i].Cells["ContactNo"];
					return false;
				}
				if (((UltraGridBase)ULGContacts).Rows[i].Cells["ContactName"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("من فضلك ادخل ادخل إسم جهه الإتصال", "Enter Contact Name");
					ULGContacts.ActiveCell = ((UltraGridBase)ULGContacts).Rows[i].Cells["ContactName"];
					return false;
				}
			}
		}
		if (((DataTable)((UltraGridBase)ULGBanks).DataSource).Rows.Count > 0)
		{
			for (int j = 0; j < ((DataTable)((UltraGridBase)ULGBanks).DataSource).Rows.Count; j++)
			{
				if (((UltraGridBase)ULGBanks).Rows[j].Cells["BankName"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("من فضلك ادخل ادخل إسم البنك", "Enter Bank Name");
					((UltraTabControlBase)tabItemType).Tabs["Banks"].Selected = true;
					ULGBanks.ActiveCell = ((UltraGridBase)ULGBanks).Rows[j].Cells["BankName"];
					return false;
				}
			}
		}
		if (((DataTable)((UltraGridBase)ULGCards).DataSource).Rows.Count > 0)
		{
			for (int k = 0; k < ((DataTable)((UltraGridBase)ULGCards).DataSource).Rows.Count; k++)
			{
				if (((UltraGridBase)ULGCards).Rows[k].Cells["CardCode"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("من فضلك ادخل كود البطاقه", "Enter Card Code");
					ULGCards.ActiveCell = ((UltraGridBase)ULGCards).Rows[k].Cells["CardCode"];
					return false;
				}
			}
		}
		if (((Control)(object)txtNo).Text == "")
		{
			((Control)(object)txtNo).Text = GetCode();
		}
		if (SubAccountsClientSupplier.Check_Code(Adding ? "0" : ((KeyedSubObjectBase)SelectedNode).Key, ((Control)(object)txtNo).Text))
		{
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["ClientSupplier"];
			string code = SubAccountsClientSupplier.GetCode(IsFromServer: true);
			GlobalVariables.QuestionMB.Show("رقم هذا العميل/المورد متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Client/Supplier Number Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtNo).Focus();
				return false;
			}
			((Control)(object)txtNo).Text = code;
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
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Expected O, but got Unknown
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
			GlobalFunctions.FillCombo(cboClientAcc, dtSubAccDetails, "AccountID", "AccountName");
			GlobalFunctions.FillCombo(cboSupplierAcc, dtSubAccDetails, "AccountID", "AccountName");
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
		int num = SearchFunctions.Clients("-1", "-1", IsFromServer: true);
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
		GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_A_SubAccounts_A.rpt" : "Rep_A_SubAccounts_E.rpt"));
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
	}

	private void cboType_ValueChanged(object sender, EventArgs e)
	{
		UltraLabel obj = lblSupplierAcc;
		bool visible = (((Control)(object)cboSupplierAcc).Visible = cboType.SelectedIndex > -1 && (((TextEditorControlBase)cboType).Value.ToString() == "3" || ((TextEditorControlBase)cboType).Value.ToString() == "5"));
		((Control)(object)obj).Visible = visible;
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

	private void ULGCards_AfterEnterEditMode(object sender, EventArgs e)
	{
		if ((!Adding && !Updating) || ((KeyedSubObjectBase)ULGCards.ActiveCell.Column).Key == "InsertDate" || (((KeyedSubObjectBase)ULGCards.ActiveCell.Column).Key == "CardCode" && !((UltraGridBase)ULGCards).ActiveRow.Cells["SubAccountClientCardID"].Value.Equals(-1)))
		{
			((GridItemBase)((UltraGridBase)ULGCards).ActiveRow).Selected = true;
		}
	}

	private void ULGContacts_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGContacts).ActiveRow).Selected = true;
		}
	}

	private void ULGBanks_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGBanks).ActiveRow).Selected = true;
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
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected O, but got Unknown
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Expected O, but got Unknown
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Expected O, but got Unknown
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Expected O, but got Unknown
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Expected O, but got Unknown
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Expected O, but got Unknown
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Expected O, but got Unknown
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Expected O, but got Unknown
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Expected O, but got Unknown
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Expected O, but got Unknown
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Expected O, but got Unknown
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Expected O, but got Unknown
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Expected O, but got Unknown
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Expected O, but got Unknown
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Expected O, but got Unknown
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Expected O, but got Unknown
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Expected O, but got Unknown
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Expected O, but got Unknown
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Expected O, but got Unknown
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Expected O, but got Unknown
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Expected O, but got Unknown
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Expected O, but got Unknown
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Expected O, but got Unknown
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Expected O, but got Unknown
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Expected O, but got Unknown
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Expected O, but got Unknown
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Expected O, but got Unknown
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Expected O, but got Unknown
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Expected O, but got Unknown
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Expected O, but got Unknown
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Expected O, but got Unknown
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Expected O, but got Unknown
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Expected O, but got Unknown
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Expected O, but got Unknown
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Expected O, but got Unknown
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Expected O, but got Unknown
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Expected O, but got Unknown
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Expected O, but got Unknown
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Expected O, but got Unknown
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Expected O, but got Unknown
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Expected O, but got Unknown
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Expected O, but got Unknown
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Expected O, but got Unknown
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Expected O, but got Unknown
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Expected O, but got Unknown
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Expected O, but got Unknown
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Expected O, but got Unknown
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Expected O, but got Unknown
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Expected O, but got Unknown
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Expected O, but got Unknown
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Expected O, but got Unknown
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Expected O, but got Unknown
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Expected O, but got Unknown
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Expected O, but got Unknown
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Expected O, but got Unknown
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Expected O, but got Unknown
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Expected O, but got Unknown
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Expected O, but got Unknown
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Expected O, but got Unknown
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Expected O, but got Unknown
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Expected O, but got Unknown
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Expected O, but got Unknown
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Expected O, but got Unknown
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Expected O, but got Unknown
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Expected O, but got Unknown
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Expected O, but got Unknown
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Expected O, but got Unknown
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Expected O, but got Unknown
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Expected O, but got Unknown
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Expected O, but got Unknown
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Expected O, but got Unknown
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Expected O, but got Unknown
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Expected O, but got Unknown
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Expected O, but got Unknown
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Expected O, but got Unknown
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Expected O, but got Unknown
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Expected O, but got Unknown
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Expected O, but got Unknown
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Expected O, but got Unknown
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Expected O, but got Unknown
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Expected O, but got Unknown
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Expected O, but got Unknown
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Expected O, but got Unknown
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Expected O, but got Unknown
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Expected O, but got Unknown
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Expected O, but got Unknown
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Expected O, but got Unknown
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Expected O, but got Unknown
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Expected O, but got Unknown
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Expected O, but got Unknown
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Expected O, but got Unknown
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Expected O, but got Unknown
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Expected O, but got Unknown
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Expected O, but got Unknown
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Expected O, but got Unknown
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Expected O, but got Unknown
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Expected O, but got Unknown
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Expected O, but got Unknown
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Expected O, but got Unknown
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Expected O, but got Unknown
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Expected O, but got Unknown
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Expected O, but got Unknown
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Expected O, but got Unknown
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Expected O, but got Unknown
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Expected O, but got Unknown
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Expected O, but got Unknown
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Expected O, but got Unknown
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Expected O, but got Unknown
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Expected O, but got Unknown
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Expected O, but got Unknown
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Expected O, but got Unknown
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Expected O, but got Unknown
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Expected O, but got Unknown
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Expected O, but got Unknown
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Expected O, but got Unknown
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Expected O, but got Unknown
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Expected O, but got Unknown
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Expected O, but got Unknown
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_0580: Expected O, but got Unknown
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Expected O, but got Unknown
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Expected O, but got Unknown
		//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Expected O, but got Unknown
		//IL_0a63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6d: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		Appearance val = new Appearance();
		Override val2 = new Override();
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
		Appearance val26 = new Appearance();
		Appearance val27 = new Appearance();
		Appearance val28 = new Appearance();
		Appearance val29 = new Appearance();
		Appearance val30 = new Appearance();
		Appearance val31 = new Appearance();
		Appearance val32 = new Appearance();
		Appearance val33 = new Appearance();
		Appearance val34 = new Appearance();
		Appearance val35 = new Appearance();
		Appearance val36 = new Appearance();
		Appearance val37 = new Appearance();
		Appearance val38 = new Appearance();
		Appearance val39 = new Appearance();
		Appearance val40 = new Appearance();
		Appearance val41 = new Appearance();
		Appearance val42 = new Appearance();
		Appearance val43 = new Appearance();
		Appearance val44 = new Appearance();
		Appearance val45 = new Appearance();
		Appearance val46 = new Appearance();
		Appearance val47 = new Appearance();
		Appearance val48 = new Appearance();
		Appearance val49 = new Appearance();
		Appearance val50 = new Appearance();
		Appearance val51 = new Appearance();
		Appearance val52 = new Appearance();
		Appearance val53 = new Appearance();
		Appearance val54 = new Appearance();
		Appearance val55 = new Appearance();
		Appearance val56 = new Appearance();
		Appearance val57 = new Appearance();
		Appearance val58 = new Appearance();
		Appearance val59 = new Appearance();
		Appearance val60 = new Appearance();
		Appearance val61 = new Appearance();
		Appearance val62 = new Appearance();
		Appearance val63 = new Appearance();
		Appearance val64 = new Appearance();
		Appearance val65 = new Appearance();
		Appearance val66 = new Appearance();
		Appearance val67 = new Appearance();
		Appearance val68 = new Appearance();
		UltraTab val69 = new UltraTab();
		UltraTab val70 = new UltraTab();
		UltraTab val71 = new UltraTab();
		UltraTab val72 = new UltraTab();
		UltraTab val73 = new UltraTab();
		UltraTab val74 = new UltraTab();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Sales.MasterData.frmClientsTree));
		Appearance val75 = new Appearance();
		this.tabService = new UltraTabPageControl();
		this.TreeAccounts = new UltraTree();
		this.lblAccounts = new UltraLabel();
		this.txtItems = new UltraTextEditor();
		this.btnItemsSearch = new UltraButton();
		this.tabItem = new UltraTabPageControl();
		this.txtBuildingNumber = new UltraTextEditor();
		this.lblBuildingNumber = new UltraLabel();
		this.chkIsActive = new UltraCheckEditor();
		this.dtpStopDate = new UltraDateTimeEditor();
		this.ultraLabel2 = new UltraLabel();
		this.cboClassification = new UltraComboEditor();
		this.lblClassification = new UltraLabel();
		this.chkForAllBranches = new UltraCheckEditor();
		this.chkDiscountTax = new UltraCheckEditor();
		this.chkAddedTax = new UltraCheckEditor();
		this.txtProductionDiscountPercentage = new UltraTextEditor();
		this.ultraLabel3 = new UltraLabel();
		this.txtDiscountAfterTaxPercentage = new UltraTextEditor();
		this.lblDiscountAfterTaxRatio = new UltraLabel();
		this.cboArea = new UltraComboEditor();
		this.lblArea = new UltraLabel();
		this.cboCity = new UltraComboEditor();
		this.lblCity = new UltraLabel();
		this.txtNo = new UltraTextEditor();
		this.ultraLabel1 = new UltraLabel();
		this.dtpBirthDate = new UltraDateTimeEditor();
		this.lblBirthDate = new UltraLabel();
		this.cboSupplierAcc = new UltraComboEditor();
		this.cboClientAcc = new UltraComboEditor();
		this.lblSupplierAcc = new UltraLabel();
		this.cboPriceType = new UltraComboEditor();
		this.cboLine = new UltraComboEditor();
		this.cboDefaultPaymentMethod = new UltraComboEditor();
		this.lblClientAccount = new UltraLabel();
		this.lblPriceType = new UltraLabel();
		this.lblLine = new UltraLabel();
		this.cboTitle = new UltraComboEditor();
		this.lblDefaultPaymentMethod = new UltraLabel();
		this.txtTel = new UltraTextEditor();
		this.lblClintSupplierTitle = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.txtDiscountPercentage = new UltraTextEditor();
		this.txtCreditLimit = new UltraTextEditor();
		this.txtEMail = new UltraTextEditor();
		this.txtMobile = new UltraTextEditor();
		this.lblMax = new UltraLabel();
		this.txtAddress = new UltraTextEditor();
		this.lblEMail = new UltraLabel();
		this.lblDiscountPercentage = new UltraLabel();
		this.txtFax = new UltraTextEditor();
		this.lblCreditLimit = new UltraLabel();
		this.lblAddress = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.lblReorder = new UltraLabel();
		this.lblMin = new UltraLabel();
		this.tabRecipe = new UltraTabPageControl();
		this.ULGContacts = new UltraGrid();
		this.ultraTabPageControl1 = new UltraTabPageControl();
		this.ULGCards = new UltraGrid();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.lblEINVSubAccountType = new UltraLabel();
		this.cboEINVSubAccountType = new UltraComboEditor();
		this.lblDefaultSalesMan = new UltraLabel();
		this.txtLicenseNo = new UltraTextEditor();
		this.txtTaxNo = new UltraTextEditor();
		this.lblTaxNo = new UltraLabel();
		this.lblLicenseNo = new UltraLabel();
		this.txtCommercialRegistrationNo = new UltraTextEditor();
		this.txtCompanyName = new UltraTextEditor();
		this.lblCompanyName = new UltraLabel();
		this.cboDefaultSalesMan = new UltraComboEditor();
		this.lblCommercialRegistrationNo = new UltraLabel();
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.ULGBanks = new UltraGrid();
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
		((System.Windows.Forms.Control)(object)this.tabService).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.TreeAccounts).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtItems).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabItem).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtBuildingNumber).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpStopDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkForAllBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkDiscountTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAddedTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtProductionDiscountPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountAfterTaxPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBirthDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSupplierAcc).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClientAcc).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLine).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultPaymentMethod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTitle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCreditLimit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobile).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFax).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabRecipe).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGContacts).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGCards).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboEINVSubAccountType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLicenseNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCommercialRegistrationNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompanyName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultSalesMan).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGBanks).BeginInit();
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
		resources.ApplyResources(base.label2, "label2");
		resources.ApplyResources(base.label3, "label3");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		((AppearanceBase)val).ForeColor = System.Drawing.Color.Navy;
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		((System.Windows.Forms.Control)(object)this.tabService).Controls.Add((System.Windows.Forms.Control)(object)this.TreeAccounts);
		((System.Windows.Forms.Control)(object)this.tabService).Controls.Add((System.Windows.Forms.Control)(object)this.lblAccounts);
		((System.Windows.Forms.Control)(object)this.tabService).Controls.Add((System.Windows.Forms.Control)(object)this.txtItems);
		((System.Windows.Forms.Control)(object)this.tabService).Controls.Add((System.Windows.Forms.Control)(object)this.btnItemsSearch);
		resources.ApplyResources(this.tabService, "tabService");
		((System.Windows.Forms.Control)(object)this.tabService).Name = "tabService";
		resources.ApplyResources(this.TreeAccounts, "TreeAccounts");
		((System.Windows.Forms.Control)(object)this.TreeAccounts).Name = "TreeAccounts";
		val2.NodeStyle = (NodeStyle)1;
		this.TreeAccounts.Override = val2;
		this.TreeAccounts.AfterCheck += new AfterNodeChangedEventHandler(Tree_AfterCheck);
		resources.ApplyResources(this.lblAccounts, "lblAccounts");
		((System.Windows.Forms.Control)(object)this.lblAccounts).Name = "lblAccounts";
		resources.ApplyResources(this.txtItems, "txtItems");
		((System.Windows.Forms.Control)(object)this.txtItems).Name = "txtItems";
		((TextEditorControlBase)this.txtItems).ValueChanged += new System.EventHandler(txtItems_ValueChanged);
		resources.ApplyResources(this.btnItemsSearch, "btnItemsSearch");
		((AppearanceBase)val3).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnItemsSearch).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Name = "btnItemsSearch";
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Click += new System.EventHandler(btnItemsSearch_Click);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtBuildingNumber);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblBuildingNumber);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.chkIsActive);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.dtpStopDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboClassification);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblClassification);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.chkForAllBranches);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.chkDiscountTax);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.chkAddedTax);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtProductionDiscountPercentage);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountAfterTaxPercentage);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountAfterTaxRatio);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboArea);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblArea);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboCity);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblCity);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtNo);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.dtpBirthDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblBirthDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboSupplierAcc);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboClientAcc);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblSupplierAcc);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboPriceType);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboLine);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboDefaultPaymentMethod);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblClientAccount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblPriceType);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblLine);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboTitle);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblDefaultPaymentMethod);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtTel);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblClintSupplierTitle);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountPercentage);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtCreditLimit);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtEMail);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtMobile);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblMax);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtAddress);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblEMail);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountPercentage);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtFax);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblCreditLimit);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblAddress);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblReorder);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblMin);
		resources.ApplyResources(this.tabItem, "tabItem");
		((System.Windows.Forms.Control)(object)this.tabItem).Name = "tabItem";
		resources.ApplyResources(this.txtBuildingNumber, "txtBuildingNumber");
		((System.Windows.Forms.Control)(object)this.txtBuildingNumber).Name = "txtBuildingNumber";
		((AppearanceBase)val4).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblBuildingNumber).Appearance = (AppearanceBase)(object)val4;
		this.lblBuildingNumber.AutoEllipsis = false;
		resources.ApplyResources(this.lblBuildingNumber, "lblBuildingNumber");
		((System.Windows.Forms.Control)(object)this.lblBuildingNumber).Name = "lblBuildingNumber";
		((ControlBase)this.lblBuildingNumber).WrapText = false;
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Navy;
		((UltraToggleEditorBase)this.chkIsActive).Appearance = (AppearanceBase)(object)val5;
		resources.ApplyResources(this.chkIsActive, "chkIsActive");
		((System.Windows.Forms.Control)(object)this.chkIsActive).Name = "chkIsActive";
		((UltraWinEditorMaskedControlBase)this.dtpStopDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpStopDate, "dtpStopDate");
		((System.Windows.Forms.Control)(object)this.dtpStopDate).Name = "dtpStopDate";
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val6;
		this.ultraLabel2.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.cboClassification, "cboClassification");
		((System.Windows.Forms.Control)(object)this.cboClassification).Name = "cboClassification";
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblClassification).Appearance = (AppearanceBase)(object)val7;
		this.lblClassification.AutoEllipsis = false;
		resources.ApplyResources(this.lblClassification, "lblClassification");
		((System.Windows.Forms.Control)(object)this.lblClassification).Name = "lblClassification";
		((ControlBase)this.lblClassification).WrapText = false;
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		((UltraToggleEditorBase)this.chkForAllBranches).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(this.chkForAllBranches, "chkForAllBranches");
		((System.Windows.Forms.Control)(object)this.chkForAllBranches).Name = "chkForAllBranches";
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		((UltraToggleEditorBase)this.chkDiscountTax).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(this.chkDiscountTax, "chkDiscountTax");
		((System.Windows.Forms.Control)(object)this.chkDiscountTax).Name = "chkDiscountTax";
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		((UltraToggleEditorBase)this.chkAddedTax).Appearance = (AppearanceBase)(object)val10;
		resources.ApplyResources(this.chkAddedTax, "chkAddedTax");
		((System.Windows.Forms.Control)(object)this.chkAddedTax).Name = "chkAddedTax";
		resources.ApplyResources(this.txtProductionDiscountPercentage, "txtProductionDiscountPercentage");
		((System.Windows.Forms.Control)(object)this.txtProductionDiscountPercentage).Name = "txtProductionDiscountPercentage";
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.ultraLabel3).Appearance = (AppearanceBase)(object)val11;
		this.ultraLabel3.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.txtDiscountAfterTaxPercentage, "txtDiscountAfterTaxPercentage");
		((System.Windows.Forms.Control)(object)this.txtDiscountAfterTaxPercentage).Name = "txtDiscountAfterTaxPercentage";
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblDiscountAfterTaxRatio).Appearance = (AppearanceBase)(object)val12;
		this.lblDiscountAfterTaxRatio.AutoEllipsis = false;
		resources.ApplyResources(this.lblDiscountAfterTaxRatio, "lblDiscountAfterTaxRatio");
		((System.Windows.Forms.Control)(object)this.lblDiscountAfterTaxRatio).Name = "lblDiscountAfterTaxRatio";
		((ControlBase)this.lblDiscountAfterTaxRatio).WrapText = false;
		resources.ApplyResources(this.cboArea, "cboArea");
		((System.Windows.Forms.Control)(object)this.cboArea).Name = "cboArea";
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblArea).Appearance = (AppearanceBase)(object)val13;
		this.lblArea.AutoEllipsis = false;
		resources.ApplyResources(this.lblArea, "lblArea");
		((System.Windows.Forms.Control)(object)this.lblArea).Name = "lblArea";
		((ControlBase)this.lblArea).WrapText = false;
		resources.ApplyResources(this.cboCity, "cboCity");
		((System.Windows.Forms.Control)(object)this.cboCity).Name = "cboCity";
		((TextEditorControlBase)this.cboCity).ValueChanged += new System.EventHandler(cboCity_ValueChanged);
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblCity).Appearance = (AppearanceBase)(object)val14;
		this.lblCity.AutoEllipsis = false;
		resources.ApplyResources(this.lblCity, "lblCity");
		((System.Windows.Forms.Control)(object)this.lblCity).Name = "lblCity";
		((ControlBase)this.lblCity).WrapText = false;
		resources.ApplyResources(this.txtNo, "txtNo");
		((System.Windows.Forms.Control)(object)this.txtNo).Name = "txtNo";
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val15;
		this.ultraLabel1.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpBirthDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpBirthDate, "dtpBirthDate");
		((System.Windows.Forms.Control)(object)this.dtpBirthDate).Name = "dtpBirthDate";
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblBirthDate).Appearance = (AppearanceBase)(object)val16;
		this.lblBirthDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblBirthDate, "lblBirthDate");
		((System.Windows.Forms.Control)(object)this.lblBirthDate).Name = "lblBirthDate";
		((ControlBase)this.lblBirthDate).WrapText = false;
		resources.ApplyResources(this.cboSupplierAcc, "cboSupplierAcc");
		((System.Windows.Forms.Control)(object)this.cboSupplierAcc).Name = "cboSupplierAcc";
		resources.ApplyResources(this.cboClientAcc, "cboClientAcc");
		((System.Windows.Forms.Control)(object)this.cboClientAcc).Name = "cboClientAcc";
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblSupplierAcc).Appearance = (AppearanceBase)(object)val17;
		this.lblSupplierAcc.AutoEllipsis = false;
		resources.ApplyResources(this.lblSupplierAcc, "lblSupplierAcc");
		((System.Windows.Forms.Control)(object)this.lblSupplierAcc).Name = "lblSupplierAcc";
		((ControlBase)this.lblSupplierAcc).WrapText = false;
		resources.ApplyResources(this.cboPriceType, "cboPriceType");
		((System.Windows.Forms.Control)(object)this.cboPriceType).Name = "cboPriceType";
		resources.ApplyResources(this.cboLine, "cboLine");
		((System.Windows.Forms.Control)(object)this.cboLine).Name = "cboLine";
		resources.ApplyResources(this.cboDefaultPaymentMethod, "cboDefaultPaymentMethod");
		((System.Windows.Forms.Control)(object)this.cboDefaultPaymentMethod).Name = "cboDefaultPaymentMethod";
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val18).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblClientAccount).Appearance = (AppearanceBase)(object)val18;
		this.lblClientAccount.AutoEllipsis = false;
		resources.ApplyResources(this.lblClientAccount, "lblClientAccount");
		((System.Windows.Forms.Control)(object)this.lblClientAccount).Name = "lblClientAccount";
		((ControlBase)this.lblClientAccount).WrapText = false;
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblPriceType).Appearance = (AppearanceBase)(object)val19;
		this.lblPriceType.AutoEllipsis = false;
		resources.ApplyResources(this.lblPriceType, "lblPriceType");
		((System.Windows.Forms.Control)(object)this.lblPriceType).Name = "lblPriceType";
		((ControlBase)this.lblPriceType).WrapText = false;
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val20, "appearance19");
		((ControlBase)this.lblLine).Appearance = (AppearanceBase)(object)val20;
		this.lblLine.AutoEllipsis = false;
		resources.ApplyResources(this.lblLine, "lblLine");
		((System.Windows.Forms.Control)(object)this.lblLine).Name = "lblLine";
		((ControlBase)this.lblLine).WrapText = false;
		resources.ApplyResources(this.cboTitle, "cboTitle");
		((System.Windows.Forms.Control)(object)this.cboTitle).Name = "cboTitle";
		((AppearanceBase)val21).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val21).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val21, "appearance20");
		((ControlBase)this.lblDefaultPaymentMethod).Appearance = (AppearanceBase)(object)val21;
		this.lblDefaultPaymentMethod.AutoEllipsis = false;
		resources.ApplyResources(this.lblDefaultPaymentMethod, "lblDefaultPaymentMethod");
		((System.Windows.Forms.Control)(object)this.lblDefaultPaymentMethod).Name = "lblDefaultPaymentMethod";
		((ControlBase)this.lblDefaultPaymentMethod).WrapText = false;
		resources.ApplyResources(this.txtTel, "txtTel");
		((System.Windows.Forms.Control)(object)this.txtTel).Name = "txtTel";
		((AppearanceBase)val22).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val22).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblClintSupplierTitle).Appearance = (AppearanceBase)(object)val22;
		this.lblClintSupplierTitle.AutoEllipsis = false;
		resources.ApplyResources(this.lblClintSupplierTitle, "lblClintSupplierTitle");
		((System.Windows.Forms.Control)(object)this.lblClintSupplierTitle).Name = "lblClintSupplierTitle";
		((ControlBase)this.lblClintSupplierTitle).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.txtDiscountPercentage, "txtDiscountPercentage");
		((System.Windows.Forms.Control)(object)this.txtDiscountPercentage).Name = "txtDiscountPercentage";
		((System.Windows.Forms.Control)(object)this.txtDiscountPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtCreditLimit, "txtCreditLimit");
		((System.Windows.Forms.Control)(object)this.txtCreditLimit).Name = "txtCreditLimit";
		((System.Windows.Forms.Control)(object)this.txtCreditLimit).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtEMail, "txtEMail");
		((System.Windows.Forms.Control)(object)this.txtEMail).Name = "txtEMail";
		resources.ApplyResources(this.txtMobile, "txtMobile");
		((System.Windows.Forms.Control)(object)this.txtMobile).Name = "txtMobile";
		((AppearanceBase)val23).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val23).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblMax).Appearance = (AppearanceBase)(object)val23;
		this.lblMax.AutoEllipsis = false;
		resources.ApplyResources(this.lblMax, "lblMax");
		((System.Windows.Forms.Control)(object)this.lblMax).Name = "lblMax";
		((ControlBase)this.lblMax).WrapText = false;
		resources.ApplyResources(this.txtAddress, "txtAddress");
		((System.Windows.Forms.Control)(object)this.txtAddress).Name = "txtAddress";
		((AppearanceBase)val24).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val24).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblEMail).Appearance = (AppearanceBase)(object)val24;
		this.lblEMail.AutoEllipsis = false;
		resources.ApplyResources(this.lblEMail, "lblEMail");
		((System.Windows.Forms.Control)(object)this.lblEMail).Name = "lblEMail";
		((ControlBase)this.lblEMail).WrapText = false;
		((AppearanceBase)val25).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val25).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblDiscountPercentage).Appearance = (AppearanceBase)(object)val25;
		this.lblDiscountPercentage.AutoEllipsis = false;
		resources.ApplyResources(this.lblDiscountPercentage, "lblDiscountPercentage");
		((System.Windows.Forms.Control)(object)this.lblDiscountPercentage).Name = "lblDiscountPercentage";
		((ControlBase)this.lblDiscountPercentage).WrapText = false;
		resources.ApplyResources(this.txtFax, "txtFax");
		((System.Windows.Forms.Control)(object)this.txtFax).Name = "txtFax";
		((AppearanceBase)val26).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val26).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblCreditLimit).Appearance = (AppearanceBase)(object)val26;
		this.lblCreditLimit.AutoEllipsis = false;
		resources.ApplyResources(this.lblCreditLimit, "lblCreditLimit");
		((System.Windows.Forms.Control)(object)this.lblCreditLimit).Name = "lblCreditLimit";
		((ControlBase)this.lblCreditLimit).WrapText = false;
		((AppearanceBase)val27).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val27).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblAddress).Appearance = (AppearanceBase)(object)val27;
		this.lblAddress.AutoEllipsis = false;
		resources.ApplyResources(this.lblAddress, "lblAddress");
		((System.Windows.Forms.Control)(object)this.lblAddress).Name = "lblAddress";
		((ControlBase)this.lblAddress).WrapText = false;
		((AppearanceBase)val28).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val28).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblNotes).Appearance = (AppearanceBase)(object)val28;
		this.lblNotes.AutoEllipsis = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		((AppearanceBase)val29).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val29).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblReorder).Appearance = (AppearanceBase)(object)val29;
		this.lblReorder.AutoEllipsis = false;
		resources.ApplyResources(this.lblReorder, "lblReorder");
		((System.Windows.Forms.Control)(object)this.lblReorder).Name = "lblReorder";
		((ControlBase)this.lblReorder).WrapText = false;
		((AppearanceBase)val30).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val30).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblMin).Appearance = (AppearanceBase)(object)val30;
		this.lblMin.AutoEllipsis = false;
		resources.ApplyResources(this.lblMin, "lblMin");
		((System.Windows.Forms.Control)(object)this.lblMin).Name = "lblMin";
		((ControlBase)this.lblMin).WrapText = false;
		((System.Windows.Forms.Control)(object)this.tabRecipe).Controls.Add((System.Windows.Forms.Control)(object)this.ULGContacts);
		resources.ApplyResources(this.tabRecipe, "tabRecipe");
		((System.Windows.Forms.Control)(object)this.tabRecipe).Name = "tabRecipe";
		resources.ApplyResources(this.ULGContacts, "ULGContacts");
		((AppearanceBase)val31).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val31).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val31).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val31).BorderColor = System.Drawing.SystemColors.Window;
		((SpecialBoxBase)((UltraGridBase)this.ULGContacts).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val31;
		((AppearanceBase)val32).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGContacts).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val32;
		((SpecialBoxBase)((UltraGridBase)this.ULGContacts).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val33).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val33).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val33).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val33).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGContacts).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val33;
		((UltraGridBase)this.ULGContacts).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGContacts).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val34).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val34).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val34;
		((AppearanceBase)val35).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val35).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val35;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val36).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val36;
		((AppearanceBase)val37).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val37).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val37;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val38).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val38).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val38).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val38).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val38).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val38;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val39).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val39).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val39;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val40).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val40;
		((System.Windows.Forms.Control)(object)this.ULGContacts).Name = "ULGContacts";
		this.ULGContacts.AfterEnterEditMode += new System.EventHandler(ULGContacts_AfterEnterEditMode);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.ULGCards);
		resources.ApplyResources(this.ultraTabPageControl1, "ultraTabPageControl1");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Name = "ultraTabPageControl1";
		resources.ApplyResources(this.ULGCards, "ULGCards");
		((AppearanceBase)val41).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val41).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val41).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val41).BorderColor = System.Drawing.SystemColors.Window;
		((SpecialBoxBase)((UltraGridBase)this.ULGCards).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val41;
		((AppearanceBase)val42).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGCards).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val42;
		((SpecialBoxBase)((UltraGridBase)this.ULGCards).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val43).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val43).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val43).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val43).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGCards).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val43;
		((UltraGridBase)this.ULGCards).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGCards).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val44).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val44).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val44;
		((AppearanceBase)val45).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val45).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val45;
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val46).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val46;
		((AppearanceBase)val47).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val47).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val47;
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val48).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val48).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val48).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val48).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val48).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val48;
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val49).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val49).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val49;
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val50).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val50;
		((System.Windows.Forms.Control)(object)this.ULGCards).Name = "ULGCards";
		this.ULGCards.AfterEnterEditMode += new System.EventHandler(ULGCards_AfterEnterEditMode);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblEINVSubAccountType);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.cboEINVSubAccountType);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblDefaultSalesMan);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.txtLicenseNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblLicenseNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.txtCommercialRegistrationNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.txtCompanyName);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblCompanyName);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.cboDefaultSalesMan);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblCommercialRegistrationNo);
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		((AppearanceBase)val51).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val51).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblEINVSubAccountType).Appearance = (AppearanceBase)(object)val51;
		this.lblEINVSubAccountType.AutoEllipsis = false;
		resources.ApplyResources(this.lblEINVSubAccountType, "lblEINVSubAccountType");
		((System.Windows.Forms.Control)(object)this.lblEINVSubAccountType).Name = "lblEINVSubAccountType";
		((ControlBase)this.lblEINVSubAccountType).WrapText = false;
		resources.ApplyResources(this.cboEINVSubAccountType, "cboEINVSubAccountType");
		((System.Windows.Forms.Control)(object)this.cboEINVSubAccountType).Name = "cboEINVSubAccountType";
		((AppearanceBase)val52).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val52).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblDefaultSalesMan).Appearance = (AppearanceBase)(object)val52;
		this.lblDefaultSalesMan.AutoEllipsis = false;
		resources.ApplyResources(this.lblDefaultSalesMan, "lblDefaultSalesMan");
		((System.Windows.Forms.Control)(object)this.lblDefaultSalesMan).Name = "lblDefaultSalesMan";
		((ControlBase)this.lblDefaultSalesMan).WrapText = false;
		resources.ApplyResources(this.txtLicenseNo, "txtLicenseNo");
		((System.Windows.Forms.Control)(object)this.txtLicenseNo).Name = "txtLicenseNo";
		resources.ApplyResources(this.txtTaxNo, "txtTaxNo");
		((System.Windows.Forms.Control)(object)this.txtTaxNo).Name = "txtTaxNo";
		((AppearanceBase)val53).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val53).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblTaxNo).Appearance = (AppearanceBase)(object)val53;
		this.lblTaxNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblTaxNo, "lblTaxNo");
		((System.Windows.Forms.Control)(object)this.lblTaxNo).Name = "lblTaxNo";
		((ControlBase)this.lblTaxNo).WrapText = false;
		((AppearanceBase)val54).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val54).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblLicenseNo).Appearance = (AppearanceBase)(object)val54;
		this.lblLicenseNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblLicenseNo, "lblLicenseNo");
		((System.Windows.Forms.Control)(object)this.lblLicenseNo).Name = "lblLicenseNo";
		((ControlBase)this.lblLicenseNo).WrapText = false;
		resources.ApplyResources(this.txtCommercialRegistrationNo, "txtCommercialRegistrationNo");
		((System.Windows.Forms.Control)(object)this.txtCommercialRegistrationNo).Name = "txtCommercialRegistrationNo";
		resources.ApplyResources(this.txtCompanyName, "txtCompanyName");
		((System.Windows.Forms.Control)(object)this.txtCompanyName).Name = "txtCompanyName";
		((AppearanceBase)val55).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val55).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblCompanyName).Appearance = (AppearanceBase)(object)val55;
		this.lblCompanyName.AutoEllipsis = false;
		resources.ApplyResources(this.lblCompanyName, "lblCompanyName");
		((System.Windows.Forms.Control)(object)this.lblCompanyName).Name = "lblCompanyName";
		((ControlBase)this.lblCompanyName).WrapText = false;
		resources.ApplyResources(this.cboDefaultSalesMan, "cboDefaultSalesMan");
		((System.Windows.Forms.Control)(object)this.cboDefaultSalesMan).Name = "cboDefaultSalesMan";
		((AppearanceBase)val56).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val56).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblCommercialRegistrationNo).Appearance = (AppearanceBase)(object)val56;
		this.lblCommercialRegistrationNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblCommercialRegistrationNo, "lblCommercialRegistrationNo");
		((System.Windows.Forms.Control)(object)this.lblCommercialRegistrationNo).Name = "lblCommercialRegistrationNo";
		((ControlBase)this.lblCommercialRegistrationNo).WrapText = false;
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ULGBanks);
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		resources.ApplyResources(this.ULGBanks, "ULGBanks");
		((AppearanceBase)val57).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val57).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val57).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val57).BorderColor = System.Drawing.SystemColors.Window;
		((SpecialBoxBase)((UltraGridBase)this.ULGBanks).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val57;
		((AppearanceBase)val58).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGBanks).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val58;
		((SpecialBoxBase)((UltraGridBase)this.ULGBanks).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val59).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val59).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val59).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val59).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGBanks).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val59;
		((UltraGridBase)this.ULGBanks).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGBanks).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val60).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val60).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val60;
		((AppearanceBase)val61).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val61).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val61;
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val62).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val62;
		((AppearanceBase)val63).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val63).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val63;
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val64).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val64).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val64).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val64).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val64).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val64;
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val65).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val65).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val65;
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val66).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val66;
		((System.Windows.Forms.Control)(object)this.ULGBanks).Name = "ULGBanks";
		this.ULGBanks.AfterEnterEditMode += new System.EventHandler(ULGBanks_AfterEnterEditMode);
		resources.ApplyResources(this.cboType, "cboType");
		((System.Windows.Forms.Control)(object)this.cboType).Name = "cboType";
		((TextEditorControlBase)this.cboType).ValueChanged += new System.EventHandler(cboType_ValueChanged);
		((AppearanceBase)val67).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val67).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblType).Appearance = (AppearanceBase)(object)val67;
		this.lblType.AutoEllipsis = false;
		resources.ApplyResources(this.lblType, "lblType");
		((System.Windows.Forms.Control)(object)this.lblType).Name = "lblType";
		((ControlBase)this.lblType).WrapText = false;
		resources.ApplyResources(this.tabItemType, "tabItemType");
		((AppearanceBase)val68).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val68).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.tabItemType).Appearance = (AppearanceBase)(object)val68;
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.tabItem);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.tabService);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.tabRecipe);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl1);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((System.Windows.Forms.Control)(object)this.tabItemType).Name = "tabItemType";
		((UltraTabControlBase)this.tabItemType).SharedControlsPage = this.ultraTabSharedControlsPage1;
		((UltraTabControlBase)this.tabItemType).TabOrientation = (TabOrientation)1;
		((KeyedSubObjectBase)val69).Key = "Accounts";
		val69.TabPage = this.tabService;
		resources.ApplyResources(val69, "ultraTab1");
		((SubObjectBase)val69).ForceApplyResources = "";
		((KeyedSubObjectBase)val70).Key = "ClientSupplier";
		val70.TabPage = this.tabItem;
		resources.ApplyResources(val70, "ultraTab2");
		((SubObjectBase)val70).ForceApplyResources = "";
		((KeyedSubObjectBase)val71).Key = "Contacts";
		val71.TabPage = this.tabRecipe;
		resources.ApplyResources(val71, "ultraTab3");
		((SubObjectBase)val71).ForceApplyResources = "";
		((KeyedSubObjectBase)val72).Key = "Cards";
		val72.TabPage = this.ultraTabPageControl1;
		resources.ApplyResources(val72, "ultraTab4");
		((SubObjectBase)val72).ForceApplyResources = "";
		((KeyedSubObjectBase)val73).Key = "Company";
		val73.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val73, "ultraTab5");
		((SubObjectBase)val73).ForceApplyResources = "";
		((KeyedSubObjectBase)val74).Key = "Banks";
		val74.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val74, "ultraTab6");
		((SubObjectBase)val74).ForceApplyResources = "";
		((UltraTabControlBase)this.tabItemType).Tabs.AddRange((UltraTab[])(object)new UltraTab[6] { val69, val70, val71, val72, val73, val74 });
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
		((AppearanceBase)val75).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val75).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblBranch).Appearance = (AppearanceBase)(object)val75;
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
		base.Name = "frmClientsTree";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.label3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAddRoot, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.tabItemType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		((System.ComponentModel.ISupportInitialize)base.dtChart).EndInit();
		((System.ComponentModel.ISupportInitialize)base.treeChart).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtName).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.tabService).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.tabService).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.TreeAccounts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtItems).EndInit();
		((System.Windows.Forms.Control)(object)this.tabItem).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.tabItem).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtBuildingNumber).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpStopDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkForAllBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkDiscountTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAddedTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtProductionDiscountPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountAfterTaxPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBirthDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSupplierAcc).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClientAcc).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLine).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultPaymentMethod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTitle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCreditLimit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobile).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFax).EndInit();
		((System.Windows.Forms.Control)(object)this.tabRecipe).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGContacts).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGCards).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cboEINVSubAccountType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLicenseNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCommercialRegistrationNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompanyName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultSalesMan).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGBanks).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tabItemType).EndInit();
		((System.Windows.Forms.Control)(object)this.tabItemType).ResumeLayout(false);
		this.contextMenuStrip1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.cboBranch).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
