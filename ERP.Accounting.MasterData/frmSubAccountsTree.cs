using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.EInvoices;
using BusinessLayer.General;
using BusinessLayer.HR;
using BusinessLayer.Privilege;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
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

namespace ERP.Accounting.MasterData;

public class frmSubAccountsTree : frmTree2
{
	private UltraTextEditor txtPassword = new UltraTextEditor();

	private DataTable dtAccounts;

	private DataTable dtStores;

	private DataTable dtEINVSubAccountsTypes;

	private DataTable dtSubAccountType;

	private DataTable dtContacts;

	private DataTable dtReports;

	private DataTable dtSubAccountClassification;

	private DataTable dtBanks;

	private DataTable dtLines;

	private DataTable dtCards;

	private DataTable dtTitle;

	private DataTable dtPaymentMethod;

	private DataTable dtPriceType;

	private DataTable dtSubAccDetails;

	private DataTable dtCities;

	private DataTable dtAreas;

	private DataTable dtBranchs;

	private DataTable dtPosition;

	private DataTable dtSalesMan;

	private ValueList vlPosition = new ValueList();

	private string ClintSupplierID = "-1";

	private string EmployeeID = "-1";

	private bool UseElectronicInvoice = false;

	private IContainer components = null;

	public UltraButton btnItemsSearch;

	private UltraTextEditor txtItems;

	public UltraTree TreeAccounts;

	public UltraTabControl tabItemType;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraComboEditor cboType;

	private UltraLabel lblType;

	private UltraTabPageControl tabService;

	private UltraTabPageControl tabRecipe;

	public UltraGrid ULGContacts;

	private UltraLabel lblAccounts;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem changeParentTSMenu;

	private ToolStripMenuItem setAsGroupToolStripMenuItem;

	private ToolStripMenuItem setAsSubAccountToolStripMenuItem;

	private UltraTabPageControl ultraTabPageControl1;

	public UltraGrid ULGCards;

	private UltraLabel lblBranch;

	private UltraComboEditor cboBranch;

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

	private UltraTabPageControl ultraTabPageControl3;

	public UltraGrid ULGBanks;

	private UltraTabPageControl ultraTabPageControl4;

	private UltraLabel ultraLabel28;

	private UltraComboEditor cboSalesManDefaultStore;

	private UltraCheckEditor chkIsSalesMan;

	private UltraTabPageControl tabItem;

	private UltraCheckEditor chkIsActive;

	private UltraDateTimeEditor dtpStopDate;

	private UltraLabel ultraLabel2;

	private UltraComboEditor cboClassification;

	private UltraLabel lblClassification;

	private UltraComboEditor cboLine;

	private UltraLabel lblLine;

	private UltraCheckEditor chkDiscountTax;

	private UltraCheckEditor chkForAllBranches;

	private UltraCheckEditor chkAddedTax;

	private UltraTextEditor txtDiscountAfterTaxPercentage;

	private UltraLabel ultraLabel3;

	private UltraLabel lblDiscountAfterTaxRatio;

	private UltraComboEditor cboArea;

	private UltraLabel lblArea;

	private UltraComboEditor cboCity;

	private UltraLabel lblCity;

	private UltraTextEditor txtNo;

	private UltraLabel ultraLabel1;

	private UltraDateTimeEditor dtpBirthDate;

	private UltraLabel lblBirthDate;

	private UltraComboEditor cboSupplierAcc;

	private UltraComboEditor cboClientAcc;

	private UltraLabel lblSupplierAcc;

	private UltraComboEditor cboPriceType;

	private UltraComboEditor cboDefaultPaymentMethod;

	private UltraLabel lblClientAccount;

	private UltraLabel lblPriceType;

	private UltraComboEditor cboTitle;

	private UltraLabel lblDefaultPaymentMethod;

	private UltraTextEditor txtTel;

	private UltraLabel lblClintSupplierTitle;

	private UltraTextEditor txtNotes;

	private UltraTextEditor txtDiscountPercentage;

	private UltraTextEditor txtCreditLimit;

	private UltraTextEditor txtEMail;

	private UltraTextEditor txtMobile;

	private UltraLabel lblMax;

	private UltraTextEditor txtAddress;

	private UltraLabel lblEMail;

	private UltraLabel lblDiscountPercentage;

	private UltraTextEditor txtFax;

	private UltraLabel lblCreditLimit;

	private UltraLabel lblAddress;

	private UltraLabel lblNotes;

	private UltraLabel lblReorder;

	private UltraLabel lblMin;

	private UltraTextEditor txtLensesProductionDiscountPercentage;

	private UltraLabel lblEINVSubAccountType;

	private UltraComboEditor cboEINVSubAccountType;

	private UltraTextEditor txtBuildingNumber;

	private UltraLabel lblBuildingNumber;

	private UltraCheckEditor chkAllBranchesSalesMen;

	public frmSubAccountsTree()
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
		AllowAddRoot = true;
	}

	public override void PrepareData()
	{
		base.PrepareData();
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
		dtSubAccountType = SuAccountsTypes.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0");
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
		dtPriceType = PricesTypes.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", GlobalVariables.IsArabic ? "PriceNameAr" : "PriceNameEn");
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSalesManDefaultStore, dtStores, "StoreID", "StoreName");
		dtSubAccountClassification = SubAccounts_Classifications.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboClassification, dtSubAccountClassification, "SubAccountClassificationID", "SubAccountClassificationName");
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtPosition = Positions.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
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
	}

	public override void ClearControls()
	{
		base.ClearControls();
		if (Adding)
		{
			((Control)(object)txtCode).Text = SubAccounts.GetCode((SelectedNode == null) ? "Null" : ((KeyedSubObjectBase)SelectedNode).Key, IsFromServer: true);
			((Control)(object)txtNo).Text = SubAccountsClientSupplier.GetCode(IsFromServer: true);
			((Control)(object)txtName).Select();
			((UltraToggleEditorBase)chkIsSalesMan).Checked = false;
		}
		dtContacts.Rows.Clear();
		dtBanks.Rows.Clear();
		dtCards.Rows.Clear();
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
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
		((EditorButtonControlBase)txtDiscountPercentage).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscountAfterTaxPercentage).ReadOnly = NavMode;
		((EditorButtonControlBase)txtLensesProductionDiscountPercentage).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNo).ReadOnly = NavMode;
		((EditorButtonControlBase)cboDefaultPaymentMethod).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCity).ReadOnly = NavMode;
		((EditorButtonControlBase)cboArea).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBranch).ReadOnly = NavMode || flag;
		((EditorButtonControlBase)cboClientAcc).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSupplierAcc).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPriceType).ReadOnly = NavMode;
		((EditorButtonControlBase)cboLine).ReadOnly = NavMode;
		((Control)(object)chkAddedTax).Enabled = !NavMode;
		((Control)(object)chkDiscountTax).Enabled = !NavMode;
		((Control)(object)chkForAllBranches).Enabled = !(NavMode || flag);
		((Control)(object)chkAllBranchesSalesMen).Enabled = !(NavMode || flag);
		((Control)(object)chkIsActive).Enabled = !NavMode;
		((EditorButtonControlBase)cboClassification).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCompanyName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCommercialRegistrationNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTaxNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtLicenseNo).ReadOnly = NavMode;
		((EditorButtonControlBase)cboDefaultSalesMan).ReadOnly = NavMode;
		((EditorButtonControlBase)cboEINVSubAccountType).ReadOnly = NavMode;
		((Control)(object)cboEINVSubAccountType).Visible = UseElectronicInvoice;
		((Control)(object)lblEINVSubAccountType).Visible = UseElectronicInvoice;
		((EditorButtonControlBase)cboType).ReadOnly = NavMode;
		((Control)(object)chkIsSalesMan).Enabled = !NavMode;
		((EditorButtonControlBase)cboSalesManDefaultStore).ReadOnly = NavMode;
		if (Adding || Updating)
		{
			if (cboBranch.SelectedIndex > -1)
			{
				object value = ((TextEditorControlBase)cboSalesManDefaultStore).Value;
				DataView dataView = new DataView(dtStores);
				dataView.RowFilter = " Locked =0  And BranchID=" + ((TextEditorControlBase)cboBranch).Value.ToString();
				DataTable dt = dataView.ToTable();
				GlobalFunctions.FillCombo(cboSalesManDefaultStore, dt, "StoreID", "StoreName");
				((TextEditorControlBase)cboSalesManDefaultStore).Value = value;
			}
			else
			{
				cboSalesManDefaultStore.DataSource = null;
			}
		}
		else
		{
			object value2 = ((TextEditorControlBase)cboSalesManDefaultStore).Value;
			GlobalFunctions.FillCombo(cboSalesManDefaultStore, dtStores, "StoreID", "StoreName");
			((TextEditorControlBase)cboSalesManDefaultStore).Value = value2;
		}
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
		EmployeeID = "-1";
		base.DisplayData();
		if (SelectedNode != null)
		{
			((UltraToggleEditorBase)chkForAllBranches).CheckedChanged -= chkForAllBranches_CheckedChanged;
			((UltraToggleEditorBase)chkAllBranchesSalesMen).CheckedChanged -= chkAllBranchesSalesMen_CheckedChanged;
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
			((TextEditorControlBase)txtLensesProductionDiscountPercentage).Clear();
			((TextEditorControlBase)txtNotes).Clear();
			((TextEditorControlBase)txtNo).Clear();
			((TextEditorControlBase)cboDefaultPaymentMethod).Clear();
			((TextEditorControlBase)cboCity).Clear();
			((TextEditorControlBase)cboArea).Clear();
			((TextEditorControlBase)cboClientAcc).Clear();
			((TextEditorControlBase)cboSupplierAcc).Clear();
			((TextEditorControlBase)cboPriceType).Clear();
			((TextEditorControlBase)cboLine).Clear();
			((UltraToggleEditorBase)chkAddedTax).Checked = false;
			((UltraToggleEditorBase)chkIsActive).Checked = false;
			((UltraToggleEditorBase)chkDiscountTax).Checked = false;
			((UltraToggleEditorBase)chkForAllBranches).Checked = true;
			((UltraToggleEditorBase)chkAllBranchesSalesMen).Checked = true;
			((TextEditorControlBase)cboClassification).Clear();
			((TextEditorControlBase)txtCompanyName).Clear();
			((TextEditorControlBase)txtCommercialRegistrationNo).Clear();
			((TextEditorControlBase)txtTaxNo).Clear();
			((TextEditorControlBase)txtLicenseNo).Clear();
			((TextEditorControlBase)cboDefaultSalesMan).Clear();
			((TextEditorControlBase)cboEINVSubAccountType).Clear();
			((TextEditorControlBase)cboType).Value = ((DataRow)((SubObjectBase)SelectedNode).Tag)[AdditionalCol1].ToString();
			((TextEditorControlBase)cboBranch).ValueChanged -= cboBranch_ValueChanged;
			((TextEditorControlBase)cboBranch).Value = ((DataRow)((SubObjectBase)SelectedNode).Tag)[AdditionalCol2].ToString();
			((TextEditorControlBase)cboBranch).ValueChanged += cboBranch_ValueChanged;
			((UltraToggleEditorBase)chkForAllBranches).Checked = Convert.ToBoolean(((DataRow)((SubObjectBase)SelectedNode).Tag)[AdditionalCol3]);
			((UltraToggleEditorBase)chkAllBranchesSalesMen).Checked = Convert.ToBoolean(((DataRow)((SubObjectBase)SelectedNode).Tag)[AdditionalCol3]);
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
			if (cboType.SelectedIndex == 2 || cboType.SelectedIndex == 3 || cboType.SelectedIndex == 4 || cboType.SelectedIndex == 6)
			{
				DisplayClientSupplierData();
			}
			if (cboType.SelectedIndex == 1 || cboType.SelectedIndex == 6)
			{
				DisplayEmployeeData();
			}
			((UltraToggleEditorBase)chkForAllBranches).CheckedChanged += chkForAllBranches_CheckedChanged;
			((UltraToggleEditorBase)chkAllBranchesSalesMen).CheckedChanged += chkAllBranchesSalesMen_CheckedChanged;
		}
	}

	public void DisplayEmployeeData()
	{
		EmployeeID = "-1";
		DataTable dataTable = Employees.SelectBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.Rows[0];
			EmployeeID = dataRow["EmployeeID"].ToString();
			((TextEditorControlBase)cboSalesManDefaultStore).Value = dataRow["SalesManDefaultStoreID"].ToString();
			((UltraToggleEditorBase)chkIsSalesMan).Checked = bool.Parse(dataRow["IsSalesMan"].ToString());
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
			((Control)(object)txtLensesProductionDiscountPercentage).Text = dataRow["BlanksProductionDiscountPercentage"].ToString();
			((TextEditorControlBase)cboDefaultPaymentMethod).Value = dataRow["DefaultPaymentMethodID"].ToString();
			((TextEditorControlBase)cboCity).Value = dataRow["CityID"].ToString();
			((TextEditorControlBase)cboArea).Value = dataRow["AreaID"].ToString();
			((TextEditorControlBase)cboClientAcc).Value = dataRow["DefaultClientAccountID"].ToString();
			((TextEditorControlBase)cboClassification).Value = dataRow["SubAccountClassificationID"].ToString();
			((TextEditorControlBase)cboSupplierAcc).Value = dataRow["DefaultSupplierAccountID"].ToString();
			((TextEditorControlBase)cboPriceType).Value = dataRow["PriceTypeID"].ToString();
			((TextEditorControlBase)cboLine).Value = dataRow["LineID"].ToString();
			((UltraToggleEditorBase)chkAddedTax).Checked = bool.Parse(dataRow["IsAddedTax"].ToString());
			((UltraToggleEditorBase)chkIsActive).Checked = bool.Parse(dataRow["IsActive"].ToString());
			((UltraToggleEditorBase)chkDiscountTax).Checked = bool.Parse(dataRow["IsDiscountTax"].ToString());
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
			if (cboType.SelectedIndex == 2 || cboType.SelectedIndex == 3 || cboType.SelectedIndex == 4 || cboType.SelectedIndex == 6)
			{
				SubAccountsClientSupplier.Insert_Update("-1", (((Control)(object)txtNo).Text == "") ? GetCode() : ((Control)(object)txtNo).Text, (cboTitle.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTitle).Value.ToString(), num.ToString(), dtpBirthDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), ((Control)(object)txtBuildingNumber).Text, ((Control)(object)txtAddress).Text, ((Control)(object)txtTel).Text, ((Control)(object)txtFax).Text, ((Control)(object)txtMobile).Text, ((Control)(object)txtEMail).Text, (cboLine.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLine).Value.ToString(), (((Control)(object)txtCreditLimit).Text == "") ? "Null" : ((Control)(object)txtCreditLimit).Text, (((Control)(object)txtDiscountPercentage).Text == "") ? "Null" : ((Control)(object)txtDiscountPercentage).Text, (((Control)(object)txtDiscountAfterTaxPercentage).Text == "") ? "Null" : ((Control)(object)txtDiscountAfterTaxPercentage).Text, (((Control)(object)txtLensesProductionDiscountPercentage).Text == "") ? "Null" : ((Control)(object)txtLensesProductionDiscountPercentage).Text, (cboDefaultPaymentMethod.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDefaultPaymentMethod).Value.ToString(), (cboClientAcc.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClientAcc).Value.ToString(), (cboSupplierAcc.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSupplierAcc).Value.ToString(), (cboPriceType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), (cboClassification.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClassification).Value.ToString(), ((Control)(object)txtCompanyName).Text, ((Control)(object)txtCommercialRegistrationNo).Text, (cboEINVSubAccountType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboEINVSubAccountType).Value.ToString(), ((Control)(object)txtTaxNo).Text.Trim(), ((Control)(object)txtLicenseNo).Text, (cboDefaultSalesMan.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDefaultSalesMan).Value.ToString(), ((UltraToggleEditorBase)chkDiscountTax).Checked ? "1" : "0", ((UltraToggleEditorBase)chkAddedTax).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsActive).Checked ? "1" : "0", (dtpStopDate.Value == null) ? "Null" : dtpStopDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtNotes).Text, "0", (cboBranch.SelectedIndex == -1) ? GlobalVariables.CurrentBranchID : ((TextEditorControlBase)cboBranch).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
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
			}
			if (cboType.SelectedIndex == 1 || cboType.SelectedIndex == 6)
			{
				Employees.Insert_Update(GetCode(), "Null", num.ToString(), ((UltraToggleEditorBase)chkIsSalesMan).Checked ? "1" : "0", (!((UltraToggleEditorBase)chkIsSalesMan).Checked) ? "Null" : ((cboSalesManDefaultStore.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesManDefaultStore).Value.ToString()), "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "1", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "0", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "0", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "1", "0", "0", "Null", "Null", "Null", "Null", "Null", "Null", "0", "Null", "0", "Null", "0", "0", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "0", (cboBranch.SelectedIndex == -1) ? GlobalVariables.CurrentBranchID : ((TextEditorControlBase)cboBranch).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
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
			if (cboType.SelectedIndex == 2 || cboType.SelectedIndex == 3 || cboType.SelectedIndex == 4 || cboType.SelectedIndex == 6)
			{
				SubAccountsClientSupplier.Insert_Update(ClintSupplierID, (((Control)(object)txtNo).Text == "") ? GetCode() : ((Control)(object)txtNo).Text, (cboTitle.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTitle).Value.ToString(), ((KeyedSubObjectBase)SelectedNode).Key, dtpBirthDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), ((Control)(object)txtBuildingNumber).Text, ((Control)(object)txtAddress).Text, ((Control)(object)txtTel).Text, ((Control)(object)txtFax).Text, ((Control)(object)txtMobile).Text, ((Control)(object)txtEMail).Text, (cboLine.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLine).Value.ToString(), (((Control)(object)txtCreditLimit).Text == "") ? "Null" : ((Control)(object)txtCreditLimit).Text, (((Control)(object)txtDiscountPercentage).Text == "") ? "Null" : ((Control)(object)txtDiscountPercentage).Text, (((Control)(object)txtDiscountAfterTaxPercentage).Text == "") ? "Null" : ((Control)(object)txtDiscountAfterTaxPercentage).Text, (((Control)(object)txtLensesProductionDiscountPercentage).Text == "") ? "Null" : ((Control)(object)txtLensesProductionDiscountPercentage).Text, (cboDefaultPaymentMethod.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDefaultPaymentMethod).Value.ToString(), (cboClientAcc.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClientAcc).Value.ToString(), (cboSupplierAcc.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSupplierAcc).Value.ToString(), (cboPriceType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), (cboClassification.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClassification).Value.ToString(), ((Control)(object)txtCompanyName).Text, ((Control)(object)txtCommercialRegistrationNo).Text, (cboEINVSubAccountType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboEINVSubAccountType).Value.ToString(), ((Control)(object)txtTaxNo).Text.Trim(), ((Control)(object)txtLicenseNo).Text, (cboDefaultSalesMan.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDefaultSalesMan).Value.ToString(), ((UltraToggleEditorBase)chkDiscountTax).Checked ? "1" : "0", ((UltraToggleEditorBase)chkAddedTax).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsActive).Checked ? "1" : "0", (dtpStopDate.Value == null) ? "Null" : dtpStopDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtNotes).Text, "0", (cboBranch.SelectedIndex == -1) ? GlobalVariables.CurrentBranchID : ((TextEditorControlBase)cboBranch).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
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
			}
			if (cboType.SelectedIndex == 1 || cboType.SelectedIndex == 6)
			{
				Employees.Insert_Update2(GetCode(), ((KeyedSubObjectBase)SelectedNode).Key, ((UltraToggleEditorBase)chkIsSalesMan).Checked ? "1" : "0", (!((UltraToggleEditorBase)chkIsSalesMan).Checked) ? "Null" : ((cboSalesManDefaultStore.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesManDefaultStore).Value.ToString()), (cboBranch.SelectedIndex == -1) ? GlobalVariables.CurrentBranchID : ((TextEditorControlBase)cboBranch).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
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
			SubAccountsClientSupplierBanks.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			SubAccountsClientSupplierContacts.DeleteBySubAccountID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
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

	private void ULGCards_InitializeLayout(object sender, InitializeLayoutEventArgs e)
	{
	}

	private void chkAllBranchesSalesMen_CheckedChanged(object sender, EventArgs e)
	{
		((UltraToggleEditorBase)chkForAllBranches).Checked = ((UltraToggleEditorBase)chkAllBranchesSalesMen).Checked;
	}

	private void chkForAllBranches_CheckedChanged(object sender, EventArgs e)
	{
		((UltraToggleEditorBase)chkAllBranchesSalesMen).Checked = ((UltraToggleEditorBase)chkForAllBranches).Checked;
	}

	private void chkIsSalesMan_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboSalesManDefaultStore).Enabled = ((UltraToggleEditorBase)chkIsSalesMan).Checked;
	}

	private void cboBranch_ValueChanged(object sender, EventArgs e)
	{
		if (cboBranch.SelectedIndex != -1)
		{
			object value = ((TextEditorControlBase)cboSalesManDefaultStore).Value;
			DataView dataView = new DataView(dtStores);
			dataView.RowFilter = " Locked =0  And BranchID=" + ((TextEditorControlBase)cboBranch).Value.ToString();
			DataTable dt = dataView.ToTable();
			GlobalFunctions.FillCombo(cboSalesManDefaultStore, dt, "StoreID", "StoreName");
			((TextEditorControlBase)cboSalesManDefaultStore).Value = value;
		}
		else
		{
			object value2 = ((TextEditorControlBase)cboSalesManDefaultStore).Value;
			GlobalFunctions.FillCombo(cboSalesManDefaultStore, dtStores, "StoreID", "StoreName");
			((TextEditorControlBase)cboSalesManDefaultStore).Value = value2;
		}
	}

	public override bool ValidateData()
	{
		dtContacts.AcceptChanges();
		dtBanks.AcceptChanges();
		dtCards.AcceptChanges();
		if (cboSupplierAcc.SelectedIndex == -1 && (cboType.SelectedIndex == 2 || cboType.SelectedIndex == 4))
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل الحساب الإفتراضي للمورد", "Enter Default Supplier Account");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["ClientSupplier"];
			((TextEditorControlBase)cboSupplierAcc).Focus();
			return false;
		}
		if (cboClientAcc.SelectedIndex == -1 && (cboType.SelectedIndex == 3 || cboType.SelectedIndex == 4 || cboType.SelectedIndex == 6))
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل الحساب الإفتراضي للعميل", "Enter Default Client Account");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["ClientSupplier"];
			((TextEditorControlBase)cboClientAcc).Focus();
			return false;
		}
		if (cboType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك نوع الحساب ", "Enter SubAccount Type");
			((TextEditorControlBase)cboType).Focus();
			return false;
		}
		if (UseElectronicInvoice && (cboType.SelectedIndex == 2 || cboType.SelectedIndex == 3 || cboType.SelectedIndex == 4 || cboType.SelectedIndex == 6))
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
		if (((Control)(object)cboBranch).Visible && cboBranch.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك الفرع ", "Enter Branch");
			((TextEditorControlBase)cboBranch).Focus();
			return false;
		}
		if (!((UltraToggleEditorBase)chkIsActive).Checked && dtpStopDate.Value == null && (cboType.SelectedIndex == 2 || cboType.SelectedIndex == 3 || cboType.SelectedIndex == 4 || cboType.SelectedIndex == 6))
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل تاريخ التوقف ", "Please Enter Stop Date");
			((Control)(object)dtpStopDate).Focus();
			return false;
		}
		if (cboType.SelectedIndex == 2 || cboType.SelectedIndex == 3 || cboType.SelectedIndex == 4 || cboType.SelectedIndex == 6)
		{
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
						GlobalVariables.InformationMB.Show("من فضلك ادخل ادخل إسم  البنك", "Enter Bank Name");
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
		}
		if (cboType.SelectedIndex == 1 || cboType.SelectedIndex == 6)
		{
			if (((Control)(object)txtCode).Text == "")
			{
				((Control)(object)txtCode).Text = GetCode();
			}
			if (Employees.Check_Code(Adding ? "0" : ((KeyedSubObjectBase)SelectedNode).Key, ((Control)(object)txtCode).Text, IsFromServer: true))
			{
				string code2 = Employees.GetCode(IsFromServer: true);
				GlobalVariables.QuestionMB.Show("رقم هذا الموظف متواجد من قبل \n سوف يتم الحفظ برقم " + code2, "The Employee Number Already Exists It Will Be Saved With No. : " + code2);
				if (GlobalVariables.MessageBoxResult != 'Y')
				{
					((TextEditorControlBase)txtCode).Focus();
					return false;
				}
				((Control)(object)txtCode).Text = code2;
			}
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
		int num = SearchFunctions.SubAccounts("-1", IsFromServer: true);
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
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_A_SubAccounts_A.rpt" : "Rep_A_SubAccounts_E.rpt"));
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
	}

	private void cboType_ValueChanged(object sender, EventArgs e)
	{
		UltraLabel obj = lblBranch;
		bool visible = (((Control)(object)cboBranch).Visible = cboType.SelectedIndex == 1 || cboType.SelectedIndex == 2 || cboType.SelectedIndex == 3 || cboType.SelectedIndex == 4 || cboType.SelectedIndex == 6);
		((Control)(object)obj).Visible = visible;
		((UltraTabControlBase)tabItemType).Tabs["EmployeeDetails"].Visible = cboType.SelectedIndex == 1 || cboType.SelectedIndex == 6;
		UltraTab obj2 = ((UltraTabControlBase)tabItemType).Tabs["ClientSupplier"];
		UltraTab obj3 = ((UltraTabControlBase)tabItemType).Tabs["Contacts"];
		UltraTab obj4 = ((UltraTabControlBase)tabItemType).Tabs["Banks"];
		UltraTab obj5 = ((UltraTabControlBase)tabItemType).Tabs["Cards"];
		bool flag2 = (((UltraTabControlBase)tabItemType).Tabs["Company"].Visible = cboType.SelectedIndex == 2 || cboType.SelectedIndex == 3 || cboType.SelectedIndex == 4 || cboType.SelectedIndex == 6);
		bool flag4 = (obj5.Visible = flag2);
		bool flag6 = (obj4.Visible = flag4);
		visible = (obj3.Visible = flag6);
		obj2.Visible = visible;
		UltraLabel obj6 = lblSupplierAcc;
		visible = (((Control)(object)cboSupplierAcc).Visible = cboType.SelectedIndex == 2 || cboType.SelectedIndex == 4);
		((Control)(object)obj6).Visible = visible;
		UltraComboEditor obj7 = cboLine;
		UltraLabel obj8 = lblLine;
		UltraLabel obj9 = lblClientAccount;
		flag4 = (((Control)(object)cboClientAcc).Visible = cboType.SelectedIndex == 3 || cboType.SelectedIndex == 4 || cboType.SelectedIndex == 6);
		flag6 = (((Control)(object)obj9).Visible = flag4);
		visible = (((Control)(object)obj8).Visible = flag6);
		((Control)(object)obj7).Visible = visible;
		if (cboType.SelectedIndex == 2 || cboType.SelectedIndex == 3 || cboType.SelectedIndex == 4 || cboType.SelectedIndex == 6)
		{
			DisplayClientSupplierData();
		}
		if (cboType.SelectedIndex == 1 || cboType.SelectedIndex == 6)
		{
			DisplayEmployeeData();
		}
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
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected O, but got Unknown
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Expected O, but got Unknown
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected O, but got Unknown
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Expected O, but got Unknown
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Expected O, but got Unknown
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Expected O, but got Unknown
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Expected O, but got Unknown
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Expected O, but got Unknown
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Expected O, but got Unknown
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Expected O, but got Unknown
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Expected O, but got Unknown
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Expected O, but got Unknown
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Expected O, but got Unknown
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Expected O, but got Unknown
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Expected O, but got Unknown
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Expected O, but got Unknown
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Expected O, but got Unknown
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Expected O, but got Unknown
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Expected O, but got Unknown
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Expected O, but got Unknown
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Expected O, but got Unknown
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Expected O, but got Unknown
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Expected O, but got Unknown
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Expected O, but got Unknown
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Expected O, but got Unknown
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Expected O, but got Unknown
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Expected O, but got Unknown
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Expected O, but got Unknown
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Expected O, but got Unknown
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Expected O, but got Unknown
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Expected O, but got Unknown
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Expected O, but got Unknown
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Expected O, but got Unknown
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Expected O, but got Unknown
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Expected O, but got Unknown
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Expected O, but got Unknown
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Expected O, but got Unknown
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Expected O, but got Unknown
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Expected O, but got Unknown
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Expected O, but got Unknown
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Expected O, but got Unknown
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Expected O, but got Unknown
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Expected O, but got Unknown
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Expected O, but got Unknown
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Expected O, but got Unknown
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Expected O, but got Unknown
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Expected O, but got Unknown
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Expected O, but got Unknown
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Expected O, but got Unknown
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Expected O, but got Unknown
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Expected O, but got Unknown
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Expected O, but got Unknown
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Expected O, but got Unknown
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Expected O, but got Unknown
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Expected O, but got Unknown
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Expected O, but got Unknown
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Expected O, but got Unknown
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Expected O, but got Unknown
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Expected O, but got Unknown
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Expected O, but got Unknown
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Expected O, but got Unknown
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Expected O, but got Unknown
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Expected O, but got Unknown
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Expected O, but got Unknown
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Expected O, but got Unknown
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Expected O, but got Unknown
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Expected O, but got Unknown
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Expected O, but got Unknown
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Expected O, but got Unknown
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Expected O, but got Unknown
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Expected O, but got Unknown
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Expected O, but got Unknown
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Expected O, but got Unknown
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Expected O, but got Unknown
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Expected O, but got Unknown
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Expected O, but got Unknown
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Expected O, but got Unknown
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Expected O, but got Unknown
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Expected O, but got Unknown
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Expected O, but got Unknown
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Expected O, but got Unknown
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Expected O, but got Unknown
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Expected O, but got Unknown
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Expected O, but got Unknown
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Expected O, but got Unknown
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Expected O, but got Unknown
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Expected O, but got Unknown
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Expected O, but got Unknown
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Expected O, but got Unknown
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Expected O, but got Unknown
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Expected O, but got Unknown
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Expected O, but got Unknown
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Expected O, but got Unknown
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Expected O, but got Unknown
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Expected O, but got Unknown
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Expected O, but got Unknown
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Expected O, but got Unknown
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Expected O, but got Unknown
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Expected O, but got Unknown
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Expected O, but got Unknown
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Expected O, but got Unknown
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Expected O, but got Unknown
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Expected O, but got Unknown
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Expected O, but got Unknown
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Expected O, but got Unknown
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Expected O, but got Unknown
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Expected O, but got Unknown
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Expected O, but got Unknown
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Expected O, but got Unknown
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Expected O, but got Unknown
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Expected O, but got Unknown
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Expected O, but got Unknown
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Expected O, but got Unknown
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Expected O, but got Unknown
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Expected O, but got Unknown
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Expected O, but got Unknown
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Expected O, but got Unknown
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Expected O, but got Unknown
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Expected O, but got Unknown
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Expected O, but got Unknown
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Expected O, but got Unknown
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Expected O, but got Unknown
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Expected O, but got Unknown
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Expected O, but got Unknown
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Expected O, but got Unknown
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Expected O, but got Unknown
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Expected O, but got Unknown
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Expected O, but got Unknown
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Expected O, but got Unknown
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		//IL_059e: Expected O, but got Unknown
		//IL_059f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a9: Expected O, but got Unknown
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Expected O, but got Unknown
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Expected O, but got Unknown
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Expected O, but got Unknown
		//IL_05cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d5: Expected O, but got Unknown
		//IL_05d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e0: Expected O, but got Unknown
		//IL_05e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05eb: Expected O, but got Unknown
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f6: Expected O, but got Unknown
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Expected O, but got Unknown
		//IL_0634: Unknown result type (might be due to invalid IL or missing references)
		//IL_063e: Expected O, but got Unknown
		//IL_063f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0649: Expected O, but got Unknown
		//IL_0ec5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ecf: Expected O, but got Unknown
		//IL_2d25: Unknown result type (might be due to invalid IL or missing references)
		//IL_2d2f: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Accounting.MasterData.frmSubAccountsTree));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Override val5 = new Override();
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
		Appearance val69 = new Appearance();
		Appearance val70 = new Appearance();
		Appearance val71 = new Appearance();
		Appearance val72 = new Appearance();
		Appearance val73 = new Appearance();
		Appearance val74 = new Appearance();
		Appearance val75 = new Appearance();
		Appearance val76 = new Appearance();
		UltraTab val77 = new UltraTab();
		UltraTab val78 = new UltraTab();
		UltraTab val79 = new UltraTab();
		UltraTab val80 = new UltraTab();
		UltraTab val81 = new UltraTab();
		UltraTab val82 = new UltraTab();
		UltraTab val83 = new UltraTab();
		Appearance val84 = new Appearance();
		this.tabService = new UltraTabPageControl();
		this.TreeAccounts = new UltraTree();
		this.lblAccounts = new UltraLabel();
		this.txtItems = new UltraTextEditor();
		this.btnItemsSearch = new UltraButton();
		this.tabItem = new UltraTabPageControl();
		this.chkIsActive = new UltraCheckEditor();
		this.dtpStopDate = new UltraDateTimeEditor();
		this.ultraLabel2 = new UltraLabel();
		this.cboClassification = new UltraComboEditor();
		this.lblClassification = new UltraLabel();
		this.cboLine = new UltraComboEditor();
		this.lblLine = new UltraLabel();
		this.chkDiscountTax = new UltraCheckEditor();
		this.chkForAllBranches = new UltraCheckEditor();
		this.chkAddedTax = new UltraCheckEditor();
		this.txtBuildingNumber = new UltraTextEditor();
		this.txtLensesProductionDiscountPercentage = new UltraTextEditor();
		this.lblBuildingNumber = new UltraLabel();
		this.txtDiscountAfterTaxPercentage = new UltraTextEditor();
		this.ultraLabel3 = new UltraLabel();
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
		this.cboDefaultPaymentMethod = new UltraComboEditor();
		this.lblClientAccount = new UltraLabel();
		this.lblPriceType = new UltraLabel();
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
		this.ultraTabPageControl4 = new UltraTabPageControl();
		this.chkAllBranchesSalesMen = new UltraCheckEditor();
		this.ultraLabel28 = new UltraLabel();
		this.cboSalesManDefaultStore = new UltraComboEditor();
		this.chkIsSalesMan = new UltraCheckEditor();
		this.tabRecipe = new UltraTabPageControl();
		this.ULGContacts = new UltraGrid();
		this.ultraTabPageControl1 = new UltraTabPageControl();
		this.ULGCards = new UltraGrid();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.lblEINVSubAccountType = new UltraLabel();
		this.lblDefaultSalesMan = new UltraLabel();
		this.txtLicenseNo = new UltraTextEditor();
		this.txtTaxNo = new UltraTextEditor();
		this.lblTaxNo = new UltraLabel();
		this.lblLicenseNo = new UltraLabel();
		this.txtCommercialRegistrationNo = new UltraTextEditor();
		this.txtCompanyName = new UltraTextEditor();
		this.lblCompanyName = new UltraLabel();
		this.cboEINVSubAccountType = new UltraComboEditor();
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
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpStopDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLine).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkDiscountTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkForAllBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAddedTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBuildingNumber).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLensesProductionDiscountPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountAfterTaxPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBirthDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSupplierAcc).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClientAcc).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).BeginInit();
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
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.chkAllBranchesSalesMen).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesManDefaultStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsSalesMan).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabRecipe).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGContacts).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGCards).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtLicenseNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCommercialRegistrationNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompanyName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboEINVSubAccountType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultSalesMan).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGBanks).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tabItemType).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabItemType).SuspendLayout();
		this.contextMenuStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboBranch).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.treeChart, "treeChart");
		((System.Windows.Forms.Control)(object)base.treeChart).ContextMenuStrip = this.contextMenuStrip1;
		resources.ApplyResources(base.txtCode, "txtCode");
		((EditorButtonControlBase)base.txtCode).ReadOnly = true;
		resources.ApplyResources(base.txtName, "txtName");
		((EditorButtonControlBase)base.txtName).ReadOnly = true;
		resources.ApplyResources(base.lblPath, "lblPath");
		resources.ApplyResources(base.txtNameEn, "txtNameEn");
		((EditorButtonControlBase)base.txtNameEn).ReadOnly = true;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnAddRoot, "btnAddRoot");
		resources.ApplyResources(base.label1, "label1");
		((AppearanceBase)val).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints");
		((AppearanceBase)val).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val, "appearance1");
		((ControlBase)base.label1).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(base.label2, "label2");
		((AppearanceBase)val2).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val2).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val2).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints1");
		((AppearanceBase)val2).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val2).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)base.label2).Appearance = (AppearanceBase)(object)val2;
		resources.ApplyResources(base.label3, "label3");
		((AppearanceBase)val3).FontData.BoldAsString = resources.GetString("resource.BoldAsString2");
		((AppearanceBase)val3).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString2");
		((AppearanceBase)val3).FontData.Name = resources.GetString("resource.Name2");
		((AppearanceBase)val3).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints2");
		((AppearanceBase)val3).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString2");
		((AppearanceBase)val3).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString2");
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)base.label3).Appearance = (AppearanceBase)(object)val3;
		resources.ApplyResources(base.btnAttachFile, "btnAttachFile");
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
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		((AppearanceBase)val4).FontData.BoldAsString = resources.GetString("resource.BoldAsString3");
		((AppearanceBase)val4).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString3");
		((AppearanceBase)val4).FontData.Name = resources.GetString("resource.Name3");
		((AppearanceBase)val4).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString3");
		((AppearanceBase)val4).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString3");
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val4, "appearance4");
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val4;
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.tabService, "tabService");
		((System.Windows.Forms.Control)(object)this.tabService).Controls.Add((System.Windows.Forms.Control)(object)this.TreeAccounts);
		((System.Windows.Forms.Control)(object)this.tabService).Controls.Add((System.Windows.Forms.Control)(object)this.lblAccounts);
		((System.Windows.Forms.Control)(object)this.tabService).Controls.Add((System.Windows.Forms.Control)(object)this.txtItems);
		((System.Windows.Forms.Control)(object)this.tabService).Controls.Add((System.Windows.Forms.Control)(object)this.btnItemsSearch);
		((System.Windows.Forms.Control)(object)this.tabService).Name = "tabService";
		resources.ApplyResources(this.TreeAccounts, "TreeAccounts");
		((System.Windows.Forms.Control)(object)this.TreeAccounts).Name = "TreeAccounts";
		val5.NodeStyle = (NodeStyle)1;
		this.TreeAccounts.Override = val5;
		this.TreeAccounts.AfterCheck += new AfterNodeChangedEventHandler(Tree_AfterCheck);
		resources.ApplyResources(this.lblAccounts, "lblAccounts");
		((System.Windows.Forms.Control)(object)this.lblAccounts).Name = "lblAccounts";
		resources.ApplyResources(this.txtItems, "txtItems");
		((System.Windows.Forms.Control)(object)this.txtItems).Name = "txtItems";
		((TextEditorControlBase)this.txtItems).ValueChanged += new System.EventHandler(txtItems_ValueChanged);
		resources.ApplyResources(this.btnItemsSearch, "btnItemsSearch");
		((AppearanceBase)val6).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val6, "appearance5");
		((ControlBase)this.btnItemsSearch).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Name = "btnItemsSearch";
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Click += new System.EventHandler(btnItemsSearch_Click);
		resources.ApplyResources(this.tabItem, "tabItem");
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.chkIsActive);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.dtpStopDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboClassification);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblClassification);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboLine);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblLine);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.chkDiscountTax);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.chkForAllBranches);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.chkAddedTax);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtBuildingNumber);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtLensesProductionDiscountPercentage);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblBuildingNumber);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountAfterTaxPercentage);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
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
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboDefaultPaymentMethod);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblClientAccount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblPriceType);
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
		((System.Windows.Forms.Control)(object)this.tabItem).Name = "tabItem";
		resources.ApplyResources(this.chkIsActive, "chkIsActive");
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance6");
		((UltraToggleEditorBase)this.chkIsActive).Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.chkIsActive).Name = "chkIsActive";
		resources.ApplyResources(this.dtpStopDate, "dtpStopDate");
		((UltraWinEditorMaskedControlBase)this.dtpStopDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpStopDate).Name = "dtpStopDate";
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance7");
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val8;
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.cboClassification, "cboClassification");
		((System.Windows.Forms.Control)(object)this.cboClassification).Name = "cboClassification";
		resources.ApplyResources(this.lblClassification, "lblClassification");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance8");
		((ControlBase)this.lblClassification).Appearance = (AppearanceBase)(object)val9;
		this.lblClassification.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClassification).Name = "lblClassification";
		((ControlBase)this.lblClassification).WrapText = false;
		resources.ApplyResources(this.cboLine, "cboLine");
		((System.Windows.Forms.Control)(object)this.cboLine).Name = "cboLine";
		resources.ApplyResources(this.lblLine, "lblLine");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance9");
		((ControlBase)this.lblLine).Appearance = (AppearanceBase)(object)val10;
		this.lblLine.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLine).Name = "lblLine";
		((ControlBase)this.lblLine).WrapText = false;
		resources.ApplyResources(this.chkDiscountTax, "chkDiscountTax");
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance10");
		((UltraToggleEditorBase)this.chkDiscountTax).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.chkDiscountTax).Name = "chkDiscountTax";
		resources.ApplyResources(this.chkForAllBranches, "chkForAllBranches");
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance11");
		((UltraToggleEditorBase)this.chkForAllBranches).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.chkForAllBranches).Name = "chkForAllBranches";
		((UltraToggleEditorBase)this.chkForAllBranches).CheckedChanged += new System.EventHandler(chkForAllBranches_CheckedChanged);
		resources.ApplyResources(this.chkAddedTax, "chkAddedTax");
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance12");
		((UltraToggleEditorBase)this.chkAddedTax).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.chkAddedTax).Name = "chkAddedTax";
		resources.ApplyResources(this.txtBuildingNumber, "txtBuildingNumber");
		((System.Windows.Forms.Control)(object)this.txtBuildingNumber).Name = "txtBuildingNumber";
		resources.ApplyResources(this.txtLensesProductionDiscountPercentage, "txtLensesProductionDiscountPercentage");
		((System.Windows.Forms.Control)(object)this.txtLensesProductionDiscountPercentage).Name = "txtLensesProductionDiscountPercentage";
		resources.ApplyResources(this.lblBuildingNumber, "lblBuildingNumber");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val14, "appearance13");
		((ControlBase)this.lblBuildingNumber).Appearance = (AppearanceBase)(object)val14;
		this.lblBuildingNumber.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBuildingNumber).Name = "lblBuildingNumber";
		((ControlBase)this.lblBuildingNumber).WrapText = false;
		resources.ApplyResources(this.txtDiscountAfterTaxPercentage, "txtDiscountAfterTaxPercentage");
		((System.Windows.Forms.Control)(object)this.txtDiscountAfterTaxPercentage).Name = "txtDiscountAfterTaxPercentage";
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val15, "appearance14");
		((ControlBase)this.ultraLabel3).Appearance = (AppearanceBase)(object)val15;
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.lblDiscountAfterTaxRatio, "lblDiscountAfterTaxRatio");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val16, "appearance15");
		((ControlBase)this.lblDiscountAfterTaxRatio).Appearance = (AppearanceBase)(object)val16;
		this.lblDiscountAfterTaxRatio.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscountAfterTaxRatio).Name = "lblDiscountAfterTaxRatio";
		((ControlBase)this.lblDiscountAfterTaxRatio).WrapText = false;
		resources.ApplyResources(this.cboArea, "cboArea");
		((System.Windows.Forms.Control)(object)this.cboArea).Name = "cboArea";
		resources.ApplyResources(this.lblArea, "lblArea");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val17, "appearance16");
		((ControlBase)this.lblArea).Appearance = (AppearanceBase)(object)val17;
		this.lblArea.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArea).Name = "lblArea";
		((ControlBase)this.lblArea).WrapText = false;
		resources.ApplyResources(this.cboCity, "cboCity");
		((System.Windows.Forms.Control)(object)this.cboCity).Name = "cboCity";
		((TextEditorControlBase)this.cboCity).ValueChanged += new System.EventHandler(cboCity_ValueChanged);
		resources.ApplyResources(this.lblCity, "lblCity");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val18).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val18, "appearance17");
		((ControlBase)this.lblCity).Appearance = (AppearanceBase)(object)val18;
		this.lblCity.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCity).Name = "lblCity";
		((ControlBase)this.lblCity).WrapText = false;
		resources.ApplyResources(this.txtNo, "txtNo");
		((System.Windows.Forms.Control)(object)this.txtNo).Name = "txtNo";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val19, "appearance18");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val19;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.dtpBirthDate, "dtpBirthDate");
		((UltraWinEditorMaskedControlBase)this.dtpBirthDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpBirthDate).Name = "dtpBirthDate";
		resources.ApplyResources(this.lblBirthDate, "lblBirthDate");
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val20, "appearance19");
		((ControlBase)this.lblBirthDate).Appearance = (AppearanceBase)(object)val20;
		this.lblBirthDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBirthDate).Name = "lblBirthDate";
		((ControlBase)this.lblBirthDate).WrapText = false;
		resources.ApplyResources(this.cboSupplierAcc, "cboSupplierAcc");
		((System.Windows.Forms.Control)(object)this.cboSupplierAcc).Name = "cboSupplierAcc";
		resources.ApplyResources(this.cboClientAcc, "cboClientAcc");
		((System.Windows.Forms.Control)(object)this.cboClientAcc).Name = "cboClientAcc";
		resources.ApplyResources(this.lblSupplierAcc, "lblSupplierAcc");
		((AppearanceBase)val21).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val21).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val21, "appearance20");
		((ControlBase)this.lblSupplierAcc).Appearance = (AppearanceBase)(object)val21;
		this.lblSupplierAcc.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSupplierAcc).Name = "lblSupplierAcc";
		((ControlBase)this.lblSupplierAcc).WrapText = false;
		resources.ApplyResources(this.cboPriceType, "cboPriceType");
		((System.Windows.Forms.Control)(object)this.cboPriceType).Name = "cboPriceType";
		resources.ApplyResources(this.cboDefaultPaymentMethod, "cboDefaultPaymentMethod");
		((System.Windows.Forms.Control)(object)this.cboDefaultPaymentMethod).Name = "cboDefaultPaymentMethod";
		resources.ApplyResources(this.lblClientAccount, "lblClientAccount");
		((AppearanceBase)val22).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val22).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val22, "appearance21");
		((ControlBase)this.lblClientAccount).Appearance = (AppearanceBase)(object)val22;
		this.lblClientAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClientAccount).Name = "lblClientAccount";
		((ControlBase)this.lblClientAccount).WrapText = false;
		resources.ApplyResources(this.lblPriceType, "lblPriceType");
		((AppearanceBase)val23).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val23).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val23, "appearance22");
		((ControlBase)this.lblPriceType).Appearance = (AppearanceBase)(object)val23;
		this.lblPriceType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPriceType).Name = "lblPriceType";
		((ControlBase)this.lblPriceType).WrapText = false;
		resources.ApplyResources(this.cboTitle, "cboTitle");
		((System.Windows.Forms.Control)(object)this.cboTitle).Name = "cboTitle";
		resources.ApplyResources(this.lblDefaultPaymentMethod, "lblDefaultPaymentMethod");
		((AppearanceBase)val24).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val24).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val24, "appearance23");
		((ControlBase)this.lblDefaultPaymentMethod).Appearance = (AppearanceBase)(object)val24;
		this.lblDefaultPaymentMethod.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDefaultPaymentMethod).Name = "lblDefaultPaymentMethod";
		((ControlBase)this.lblDefaultPaymentMethod).WrapText = false;
		resources.ApplyResources(this.txtTel, "txtTel");
		((System.Windows.Forms.Control)(object)this.txtTel).Name = "txtTel";
		resources.ApplyResources(this.lblClintSupplierTitle, "lblClintSupplierTitle");
		((AppearanceBase)val25).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val25).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val25, "appearance24");
		((ControlBase)this.lblClintSupplierTitle).Appearance = (AppearanceBase)(object)val25;
		this.lblClintSupplierTitle.AutoEllipsis = false;
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
		resources.ApplyResources(this.lblMax, "lblMax");
		((AppearanceBase)val26).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val26).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val26, "appearance25");
		((ControlBase)this.lblMax).Appearance = (AppearanceBase)(object)val26;
		this.lblMax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMax).Name = "lblMax";
		((ControlBase)this.lblMax).WrapText = false;
		resources.ApplyResources(this.txtAddress, "txtAddress");
		((System.Windows.Forms.Control)(object)this.txtAddress).Name = "txtAddress";
		resources.ApplyResources(this.lblEMail, "lblEMail");
		((AppearanceBase)val27).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val27).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val27, "appearance26");
		((ControlBase)this.lblEMail).Appearance = (AppearanceBase)(object)val27;
		this.lblEMail.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEMail).Name = "lblEMail";
		((ControlBase)this.lblEMail).WrapText = false;
		resources.ApplyResources(this.lblDiscountPercentage, "lblDiscountPercentage");
		((AppearanceBase)val28).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val28).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val28, "appearance27");
		((ControlBase)this.lblDiscountPercentage).Appearance = (AppearanceBase)(object)val28;
		this.lblDiscountPercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscountPercentage).Name = "lblDiscountPercentage";
		((ControlBase)this.lblDiscountPercentage).WrapText = false;
		resources.ApplyResources(this.txtFax, "txtFax");
		((System.Windows.Forms.Control)(object)this.txtFax).Name = "txtFax";
		resources.ApplyResources(this.lblCreditLimit, "lblCreditLimit");
		((AppearanceBase)val29).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val29).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val29, "appearance28");
		((ControlBase)this.lblCreditLimit).Appearance = (AppearanceBase)(object)val29;
		this.lblCreditLimit.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCreditLimit).Name = "lblCreditLimit";
		((ControlBase)this.lblCreditLimit).WrapText = false;
		resources.ApplyResources(this.lblAddress, "lblAddress");
		((AppearanceBase)val30).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val30).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val30, "appearance29");
		((ControlBase)this.lblAddress).Appearance = (AppearanceBase)(object)val30;
		this.lblAddress.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAddress).Name = "lblAddress";
		((ControlBase)this.lblAddress).WrapText = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((AppearanceBase)val31).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val31).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val31, "appearance30");
		((ControlBase)this.lblNotes).Appearance = (AppearanceBase)(object)val31;
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.lblReorder, "lblReorder");
		((AppearanceBase)val32).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val32).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val32, "appearance31");
		((ControlBase)this.lblReorder).Appearance = (AppearanceBase)(object)val32;
		this.lblReorder.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReorder).Name = "lblReorder";
		((ControlBase)this.lblReorder).WrapText = false;
		resources.ApplyResources(this.lblMin, "lblMin");
		((AppearanceBase)val33).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val33).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val33, "appearance32");
		((ControlBase)this.lblMin).Appearance = (AppearanceBase)(object)val33;
		this.lblMin.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMin).Name = "lblMin";
		((ControlBase)this.lblMin).WrapText = false;
		resources.ApplyResources(this.ultraTabPageControl4, "ultraTabPageControl4");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.chkAllBranchesSalesMen);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel28);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboSalesManDefaultStore);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.chkIsSalesMan);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Name = "ultraTabPageControl4";
		resources.ApplyResources(this.chkAllBranchesSalesMen, "chkAllBranchesSalesMen");
		((AppearanceBase)val34).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val34, "appearance33");
		((UltraToggleEditorBase)this.chkAllBranchesSalesMen).Appearance = (AppearanceBase)(object)val34;
		((System.Windows.Forms.Control)(object)this.chkAllBranchesSalesMen).Name = "chkAllBranchesSalesMen";
		((UltraToggleEditorBase)this.chkAllBranchesSalesMen).CheckedChanged += new System.EventHandler(chkAllBranchesSalesMen_CheckedChanged);
		resources.ApplyResources(this.ultraLabel28, "ultraLabel28");
		((AppearanceBase)val35).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val35).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val35, "appearance34");
		((ControlBase)this.ultraLabel28).Appearance = (AppearanceBase)(object)val35;
		this.ultraLabel28.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel28).Name = "ultraLabel28";
		((ControlBase)this.ultraLabel28).WrapText = false;
		resources.ApplyResources(this.cboSalesManDefaultStore, "cboSalesManDefaultStore");
		((TextEditorControlBase)this.cboSalesManDefaultStore).AlwaysInEditMode = true;
		this.cboSalesManDefaultStore.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSalesManDefaultStore).Name = "cboSalesManDefaultStore";
		resources.ApplyResources(this.chkIsSalesMan, "chkIsSalesMan");
		((AppearanceBase)val36).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val36, "appearance35");
		((UltraToggleEditorBase)this.chkIsSalesMan).Appearance = (AppearanceBase)(object)val36;
		((System.Windows.Forms.Control)(object)this.chkIsSalesMan).Name = "chkIsSalesMan";
		((UltraToggleEditorBase)this.chkIsSalesMan).CheckedChanged += new System.EventHandler(chkIsSalesMan_CheckedChanged);
		resources.ApplyResources(this.tabRecipe, "tabRecipe");
		((System.Windows.Forms.Control)(object)this.tabRecipe).Controls.Add((System.Windows.Forms.Control)(object)this.ULGContacts);
		((System.Windows.Forms.Control)(object)this.tabRecipe).Name = "tabRecipe";
		resources.ApplyResources(this.ULGContacts, "ULGContacts");
		((AppearanceBase)val37).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val37).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val37).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val37).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val37, "appearance36");
		((SpecialBoxBase)((UltraGridBase)this.ULGContacts).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val37;
		((AppearanceBase)val38).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val38, "appearance37");
		((UltraGridBase)this.ULGContacts).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val38;
		((SpecialBoxBase)((UltraGridBase)this.ULGContacts).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val39).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val39).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val39).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val39).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val39, "appearance38");
		((UltraGridBase)this.ULGContacts).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val39;
		((UltraGridBase)this.ULGContacts).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGContacts).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val40).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val40).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val40, "appearance39");
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val40;
		((AppearanceBase)val41).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val41).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val41, "appearance40");
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val41;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val42).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val42, "appearance41");
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val42;
		((AppearanceBase)val43).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val43, "appearance42");
		((AppearanceBase)val43).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val43;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val44).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val44).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val44).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val44).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val44).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val44, "appearance43");
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val44;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val45).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val45).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val45, "appearance44");
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val45;
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val46).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val46, "appearance45");
		((UltraGridBase)this.ULGContacts).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val46;
		((System.Windows.Forms.Control)(object)this.ULGContacts).Name = "ULGContacts";
		this.ULGContacts.AfterEnterEditMode += new System.EventHandler(ULGContacts_AfterEnterEditMode);
		resources.ApplyResources(this.ultraTabPageControl1, "ultraTabPageControl1");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.ULGCards);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Name = "ultraTabPageControl1";
		resources.ApplyResources(this.ULGCards, "ULGCards");
		((AppearanceBase)val47).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val47).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val47).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val47).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val47, "appearance46");
		((SpecialBoxBase)((UltraGridBase)this.ULGCards).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val47;
		((AppearanceBase)val48).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val48, "appearance47");
		((UltraGridBase)this.ULGCards).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val48;
		((SpecialBoxBase)((UltraGridBase)this.ULGCards).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val49).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val49).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val49).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val49).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val49, "appearance48");
		((UltraGridBase)this.ULGCards).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val49;
		((UltraGridBase)this.ULGCards).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGCards).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val50).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val50).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val50, "appearance49");
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val50;
		((AppearanceBase)val51).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val51).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val51, "appearance50");
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val51;
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val52).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val52, "appearance51");
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val52;
		((AppearanceBase)val53).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val53, "appearance52");
		((AppearanceBase)val53).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val53;
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val54).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val54).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val54).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val54).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val54).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val54, "appearance53");
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val54;
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val55).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val55).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val55, "appearance54");
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val55;
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val56).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val56, "appearance55");
		((UltraGridBase)this.ULGCards).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val56;
		((System.Windows.Forms.Control)(object)this.ULGCards).Name = "ULGCards";
		this.ULGCards.InitializeLayout += new InitializeLayoutEventHandler(ULGCards_InitializeLayout);
		this.ULGCards.AfterEnterEditMode += new System.EventHandler(ULGCards_AfterEnterEditMode);
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblEINVSubAccountType);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblDefaultSalesMan);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.txtLicenseNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblLicenseNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.txtCommercialRegistrationNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.txtCompanyName);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblCompanyName);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.cboEINVSubAccountType);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.cboDefaultSalesMan);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblCommercialRegistrationNo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.lblEINVSubAccountType, "lblEINVSubAccountType");
		((AppearanceBase)val57).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val57).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val57, "appearance56");
		((ControlBase)this.lblEINVSubAccountType).Appearance = (AppearanceBase)(object)val57;
		this.lblEINVSubAccountType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEINVSubAccountType).Name = "lblEINVSubAccountType";
		((ControlBase)this.lblEINVSubAccountType).WrapText = false;
		resources.ApplyResources(this.lblDefaultSalesMan, "lblDefaultSalesMan");
		((AppearanceBase)val58).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val58).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val58, "appearance57");
		((ControlBase)this.lblDefaultSalesMan).Appearance = (AppearanceBase)(object)val58;
		this.lblDefaultSalesMan.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDefaultSalesMan).Name = "lblDefaultSalesMan";
		((ControlBase)this.lblDefaultSalesMan).WrapText = false;
		resources.ApplyResources(this.txtLicenseNo, "txtLicenseNo");
		((System.Windows.Forms.Control)(object)this.txtLicenseNo).Name = "txtLicenseNo";
		resources.ApplyResources(this.txtTaxNo, "txtTaxNo");
		((System.Windows.Forms.Control)(object)this.txtTaxNo).Name = "txtTaxNo";
		resources.ApplyResources(this.lblTaxNo, "lblTaxNo");
		((AppearanceBase)val59).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val59).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val59, "appearance58");
		((ControlBase)this.lblTaxNo).Appearance = (AppearanceBase)(object)val59;
		this.lblTaxNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaxNo).Name = "lblTaxNo";
		((ControlBase)this.lblTaxNo).WrapText = false;
		resources.ApplyResources(this.lblLicenseNo, "lblLicenseNo");
		((AppearanceBase)val60).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val60).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val60, "appearance59");
		((ControlBase)this.lblLicenseNo).Appearance = (AppearanceBase)(object)val60;
		this.lblLicenseNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLicenseNo).Name = "lblLicenseNo";
		((ControlBase)this.lblLicenseNo).WrapText = false;
		resources.ApplyResources(this.txtCommercialRegistrationNo, "txtCommercialRegistrationNo");
		((System.Windows.Forms.Control)(object)this.txtCommercialRegistrationNo).Name = "txtCommercialRegistrationNo";
		resources.ApplyResources(this.txtCompanyName, "txtCompanyName");
		((System.Windows.Forms.Control)(object)this.txtCompanyName).Name = "txtCompanyName";
		resources.ApplyResources(this.lblCompanyName, "lblCompanyName");
		((AppearanceBase)val61).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val61).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val61, "appearance60");
		((ControlBase)this.lblCompanyName).Appearance = (AppearanceBase)(object)val61;
		this.lblCompanyName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCompanyName).Name = "lblCompanyName";
		((ControlBase)this.lblCompanyName).WrapText = false;
		resources.ApplyResources(this.cboEINVSubAccountType, "cboEINVSubAccountType");
		((System.Windows.Forms.Control)(object)this.cboEINVSubAccountType).Name = "cboEINVSubAccountType";
		resources.ApplyResources(this.cboDefaultSalesMan, "cboDefaultSalesMan");
		((System.Windows.Forms.Control)(object)this.cboDefaultSalesMan).Name = "cboDefaultSalesMan";
		resources.ApplyResources(this.lblCommercialRegistrationNo, "lblCommercialRegistrationNo");
		((AppearanceBase)val62).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val62).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val62, "appearance61");
		((ControlBase)this.lblCommercialRegistrationNo).Appearance = (AppearanceBase)(object)val62;
		this.lblCommercialRegistrationNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCommercialRegistrationNo).Name = "lblCommercialRegistrationNo";
		((ControlBase)this.lblCommercialRegistrationNo).WrapText = false;
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ULGBanks);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		resources.ApplyResources(this.ULGBanks, "ULGBanks");
		((AppearanceBase)val63).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val63).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val63).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val63).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val63, "appearance62");
		((SpecialBoxBase)((UltraGridBase)this.ULGBanks).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val63;
		((AppearanceBase)val64).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val64, "appearance63");
		((UltraGridBase)this.ULGBanks).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val64;
		((SpecialBoxBase)((UltraGridBase)this.ULGBanks).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val65).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val65).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val65).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val65).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val65, "appearance64");
		((UltraGridBase)this.ULGBanks).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val65;
		((UltraGridBase)this.ULGBanks).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGBanks).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val66).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val66).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val66, "appearance65");
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val66;
		((AppearanceBase)val67).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val67).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val67, "appearance66");
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val67;
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val68).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val68, "appearance67");
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val68;
		((AppearanceBase)val69).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val69, "appearance68");
		((AppearanceBase)val69).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val69;
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val70).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val70).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val70).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val70).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val70).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val70, "appearance69");
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val70;
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val71).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val71).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val71, "appearance70");
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val71;
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val72).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val72, "appearance71");
		((UltraGridBase)this.ULGBanks).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val72;
		((System.Windows.Forms.Control)(object)this.ULGBanks).Name = "ULGBanks";
		this.ULGBanks.AfterEnterEditMode += new System.EventHandler(ULGBanks_AfterEnterEditMode);
		resources.ApplyResources(this.cboType, "cboType");
		((System.Windows.Forms.Control)(object)this.cboType).Name = "cboType";
		((TextEditorControlBase)this.cboType).ValueChanged += new System.EventHandler(cboType_ValueChanged);
		resources.ApplyResources(this.lblType, "lblType");
		((AppearanceBase)val73).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val73).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val73, "appearance72");
		((ControlBase)this.lblType).Appearance = (AppearanceBase)(object)val73;
		this.lblType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblType).Name = "lblType";
		((ControlBase)this.lblType).WrapText = false;
		resources.ApplyResources(this.tabItemType, "tabItemType");
		((AppearanceBase)val74).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val74, "appearance73");
		((AppearanceBase)val74).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.tabItemType).Appearance = (AppearanceBase)(object)val74;
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.tabItem);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.tabService);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.tabRecipe);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl1);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl4);
		((System.Windows.Forms.Control)(object)this.tabItemType).Name = "tabItemType";
		((UltraTabControlBase)this.tabItemType).SharedControlsPage = this.ultraTabSharedControlsPage1;
		resources.ApplyResources(val75, "appearance74");
		((UltraTabControlBase)this.tabItemType).TabHeaderAreaAppearance = (AppearanceBase)(object)val75;
		resources.ApplyResources(val76, "appearance75");
		((UltraTabControlBase)this.tabItemType).TabListButtonAppearance = (AppearanceBase)(object)val76;
		((UltraTabControlBase)this.tabItemType).TabOrientation = (TabOrientation)2;
		((KeyedSubObjectBase)val77).Key = "Accounts";
		val77.TabPage = this.tabService;
		resources.ApplyResources(val77, "ultraTab1");
		((SubObjectBase)val77).ForceApplyResources = "";
		((KeyedSubObjectBase)val78).Key = "ClientSupplier";
		val78.TabPage = this.tabItem;
		resources.ApplyResources(val78, "ultraTab2");
		val78.Visible = false;
		((SubObjectBase)val78).ForceApplyResources = "";
		((KeyedSubObjectBase)val79).Key = "EmployeeDetails";
		val79.TabPage = this.ultraTabPageControl4;
		resources.ApplyResources(val79, "ultraTab7");
		val79.Visible = false;
		((SubObjectBase)val79).ForceApplyResources = "";
		((KeyedSubObjectBase)val80).Key = "Contacts";
		val80.TabPage = this.tabRecipe;
		resources.ApplyResources(val80, "ultraTab3");
		val80.Visible = false;
		((SubObjectBase)val80).ForceApplyResources = "";
		((KeyedSubObjectBase)val81).Key = "Cards";
		val81.TabPage = this.ultraTabPageControl1;
		resources.ApplyResources(val81, "ultraTab4");
		val81.Visible = false;
		((SubObjectBase)val81).ForceApplyResources = "";
		((KeyedSubObjectBase)val82).Key = "Company";
		val82.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val82, "ultraTab5");
		val82.Visible = false;
		((SubObjectBase)val82).ForceApplyResources = "";
		((KeyedSubObjectBase)val83).Key = "Banks";
		val83.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val83, "ultraTab6");
		val83.Visible = false;
		((SubObjectBase)val83).ForceApplyResources = "";
		((UltraTabControlBase)this.tabItemType).Tabs.AddRange((UltraTab[])(object)new UltraTab[7] { val77, val78, val79, val80, val81, val82, val83 });
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
		this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(18, 18);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.changeParentTSMenu, this.setAsGroupToolStripMenuItem, this.setAsSubAccountToolStripMenuItem });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.ShowImageMargin = false;
		resources.ApplyResources(this.changeParentTSMenu, "changeParentTSMenu");
		this.changeParentTSMenu.Name = "changeParentTSMenu";
		this.changeParentTSMenu.Click += new System.EventHandler(changeParentTSMenu_Click);
		resources.ApplyResources(this.setAsGroupToolStripMenuItem, "setAsGroupToolStripMenuItem");
		this.setAsGroupToolStripMenuItem.Name = "setAsGroupToolStripMenuItem";
		this.setAsGroupToolStripMenuItem.Click += new System.EventHandler(setAsGroupToolStripMenuItem_Click);
		resources.ApplyResources(this.setAsSubAccountToolStripMenuItem, "setAsSubAccountToolStripMenuItem");
		this.setAsSubAccountToolStripMenuItem.Name = "setAsSubAccountToolStripMenuItem";
		this.setAsSubAccountToolStripMenuItem.Click += new System.EventHandler(setAsSubAccountToolStripMenuItem_Click);
		resources.ApplyResources(this.lblBranch, "lblBranch");
		((AppearanceBase)val84).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val84).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val84, "appearance76");
		((ControlBase)this.lblBranch).Appearance = (AppearanceBase)(object)val84;
		this.lblBranch.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBranch).Name = "lblBranch";
		((ControlBase)this.lblBranch).WrapText = false;
		resources.ApplyResources(this.cboBranch, "cboBranch");
		((System.Windows.Forms.Control)(object)this.cboBranch).Name = "cboBranch";
		((TextEditorControlBase)this.cboBranch).ValueChanged += new System.EventHandler(cboBranch_ValueChanged);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.tabItemType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblType);
		base.Name = "frmSubAccountsTree";
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
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpStopDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLine).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkDiscountTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkForAllBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAddedTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBuildingNumber).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLensesProductionDiscountPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountAfterTaxPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBirthDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSupplierAcc).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClientAcc).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).EndInit();
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
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.chkAllBranchesSalesMen).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesManDefaultStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsSalesMan).EndInit();
		((System.Windows.Forms.Control)(object)this.tabRecipe).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGContacts).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGCards).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtLicenseNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCommercialRegistrationNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompanyName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboEINVSubAccountType).EndInit();
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
