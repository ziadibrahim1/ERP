using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.EInvoices;
using BusinessLayer.General;
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
using Microsoft.Office.Interop.Excel;

namespace ERP.StockControl.MasterData;

public class frmItemsTree : frmTree2
{
	private DataTable dtAccounts;

	private DataTable dtPricesTypes;

	private DataTable dtBranches;

	private DataTable dtItemPrices;

	private DataTable dtItemsUnits;

	private DataTable dtItemsUnitsPrices;

	private DataTable dtItemStockLevels;

	private DataTable dtItems;

	private DataTable dtItemsTypes;

	private DataTable dtRecipeItems;

	private DataSet dsRecipe;

	private DataTable dtItemsAccessories;

	private DataTable dtItemsAdditionals;

	private DataTable dtUnits;

	private DataTable dtStores;

	private DataTable dtUnitGroup;

	private DataTable dtTaxes;

	private DataTable dtSuppliers;

	private DataTable dtCostCenters;

	private DataTable dtEinvSettings;

	private DataTable dtEINVItemsNamesTypes;

	private DataTable dtEINVItemsGPC;

	private DataTable dtAgeGroup;

	private DataTable dtClassification1;

	private DataTable dtClassification2;

	private DataTable dtClassification3;

	private DataTable dtClassification4;

	private DataTable dtBrand;

	private DataTable dtSeason;

	private DataTable dtCountry;

	private DataTable dtPriceCategory;

	private DataTable dtMaterial1;

	private DataTable dtMaterial2;

	private DataTable dtShape;

	private DataTable dtModel;

	private int newID = -100000;

	private bool GenerateBarCodeSerial;

	private bool UseElectronicInvoice = false;

	private bool SendingItemsByItemsTypes = false;

	private DataSet ds;

	private DataTable dtItemColorCategory;

	private DataTable dtColors;

	private DataTable dtItemSizeCategory;

	private DataTable dtSizes;

	private ValueList vlPricesTypes = new ValueList();

	private ValueList vlBranches = new ValueList();

	private ValueList vlBranches2 = new ValueList();

	private ValueList vlBranches3 = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlItemsUnits = new ValueList();

	private ValueList vlItems = new ValueList();

	private ValueList vlAccessoriesItems = new ValueList();

	private ValueList vlAdditionalsItems = new ValueList();

	private ValueList vlAccessoriesUnits = new ValueList();

	private ValueList vlAdditionalsUnits = new ValueList();

	private bool UsingBatchNoAndValidityPeriod = false;

	private bool DataDisplayed = false;

	private IContainer components = null;

	private UltraPanel pnlCheckType;

	private RadioButton rbIsRecipe;

	private RadioButton rbIsService;

	private RadioButton rbIsItem;

	private UltraTextEditor txtNotes;

	private UltraLabel lblReceivingBank;

	public UltraTabControl tabItemType;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl tabItem;

	private UltraTabPageControl tabService;

	private UltraTabPageControl tabRecipe;

	private UltraTextEditor txtBarCode;

	private UltraLabel lblBarCode;

	private UltraButton btnPic;

	private UltraComboEditor cboStores;

	private UltraLabel ultraLabel1;

	private UltraCheckEditor chkIsDirectItem;

	private UltraCheckEditor chkIsSalesItem;

	private UltraComboEditor cboUnitGroup;

	private UltraCheckEditor chkIsActive;

	private UltraComboEditor cboUnit;

	private UltraLabel lblUnitGroup;

	private UltraLabel lblUnit;

	private UltraLabel lblPic;

	private UltraPictureBox picItem;

	private OpenFileDialog ofdItemPic;

	private UltraCheckEditor chkIsProductionItem;

	private UltraCheckEditor chkEnforceBatchNo;

	private UltraTextEditor txtValidityDays;

	private UltraLabel lblValidityDays;

	private UltraPanel ultraPanel1;

	private RadioButton rbPriceForEachBranches;

	private RadioButton rbPriceForAllBranches;

	private UltraTabPageControl ultraTabPageControl1;

	public UltraButton btnServiceAccountSearch;

	private UltraComboEditor cboServiceAccount;

	private UltraLabel lblServiceAccount;

	public UltraGrid ULGRecipeItems;

	public UltraGrid ULGPrices;

	private UltraComboEditor cboTax;

	private UltraLabel lblTax;

	private UltraTabPageControl ultraTabPageControl2;

	public UltraButton btnDepartmentIssueAccount;

	private UltraComboEditor cboDepartmentIssueAccount;

	private UltraLabel lblDepartmentIssueAccount;

	public UltraButton btnPurchaseReturnsAccount;

	private UltraComboEditor cboPurchaseReturnsAccount;

	private UltraLabel lblPurchaseReturnsAccount;

	public UltraButton btnPurchaseAccount;

	private UltraComboEditor cboPurchaseAccount;

	private UltraLabel lblPurchaseAccount;

	public UltraButton btnCostOfSalesAccount;

	private UltraComboEditor cboCostOfSalesAccount;

	private UltraLabel lblCostOfSalesAccount;

	public UltraButton btnSalesReturnsAccount;

	private UltraComboEditor cboSalesReturnsAccount;

	private UltraLabel lblSalesReturnsAccount;

	public UltraButton btnSalesAccount;

	private UltraComboEditor cboSalesAccount;

	private UltraLabel lblSalesAccount;

	private ToolStripMenuItem modifyGroupItemsToolStripMenuItem;

	private ToolStripMenuItem modifyGroupItemsToolStripMenuItem1;

	private ToolStripMenuItem modifyGroupItemsToolStripMenuItem2;

	private ToolStripMenuItem modifyGroupItemsToolStripMenuItem3;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem modifyGroupItemsToolStripMenuItem4;

	private UltraCheckEditor chkCanModSalesPrice;

	private UltraTextEditor txtPrepareTime;

	private UltraLabel lblPrepareTime;

	private UltraComboEditor cboPrinterName;

	private UltraLabel lblPrinterName;

	private ToolStripMenuItem deleteToolStripMenuItem;

	private UltraComboEditor cboTypes;

	private UltraLabel lblTypes;

	public UltraTextEditor txtCyl;

	public UltraLabel lblCyl;

	public UltraTextEditor txtSph;

	public UltraLabel lblSph;

	private UltraTabPageControl ultraTabPageControl3;

	public UltraGrid ULGLevels;

	private UltraPanel ultraPanel2;

	private RadioButton rbStockLevelForEachBranch;

	private RadioButton rbStockLevelsForAllBranches;

	private UltraTabPageControl ultraTabPageControl4;

	private UltraComboEditor cboShape;

	private UltraLabel lblShape;

	private UltraComboEditor cboModel;

	private UltraLabel lblModel;

	private UltraComboEditor cboMaterial2;

	private UltraLabel lblMaterial2;

	private UltraComboEditor cboMaterial1;

	private UltraLabel lblMaterial1;

	private UltraComboEditor cboPriceCategory;

	private UltraLabel lblPriceCategory;

	private UltraComboEditor cboCountry;

	private UltraLabel lblCountry;

	private UltraComboEditor cboSeason;

	private UltraLabel lblSeason;

	private UltraComboEditor cboClassification4;

	private UltraLabel lblClassification4;

	private UltraComboEditor cboClassification3;

	private UltraLabel lblClassification3;

	private UltraComboEditor cboClassification2;

	private UltraLabel lblClassification2;

	private UltraComboEditor cboClassification1;

	private UltraLabel lblClassification1;

	private UltraComboEditor cboAgeGroup;

	private UltraLabel lblAgeGroup;

	private UltraComboEditor cboBrand;

	private UltraLabel lblBrand;

	private UltraComboEditor cboSuppliers;

	private UltraLabel lblSuplliers;

	public UltraTextEditor txtVolume;

	public UltraLabel ultraLabel4;

	public UltraTextEditor txtNetWeight;

	public UltraLabel ultraLabel3;

	public UltraTextEditor txtGrowthWeight;

	public UltraLabel ultraLabel2;

	private UltraTextEditor txtSupplierItemCode;

	private UltraLabel ultraLabel5;

	private UltraComboEditor cboColorCategory;

	private UltraComboEditor cboSizeCategory;

	private UltraLabel lblColorCategory;

	private UltraLabel lblSizeCategory;

	private UltraComboEditor cboColor;

	private UltraComboEditor cboSize;

	private UltraLabel lblColor;

	private UltraLabel lblSize;

	private UltraTabPageControl ultraTabPageControl5;

	private UltraTextEditor txtAccessoriesCount;

	private UltraLabel lblAccessoriesCount;

	public UltraGrid ULGAccessoriesItems;

	private ToolStripMenuItem changeParentToolStripMenuItem;

	private UltraPanel ultraPanel3;

	private RadioButton rbRecipeForEachBranch;

	private RadioButton rbRecipeForAllBranchs;

	private UltraCheckEditor chkUsePOSNumPad;

	private UltraTabPageControl ultraTabPageControl6;

	public UltraGrid ULGAdditionalsItems;

	private UltraCheckEditor chkHideFromReports;

	private UltraCheckEditor chkCanModPurchasePrice;

	private UltraCheckEditor chkIsUnitPrice;

	public UltraButton btnCostCenterSearch;

	private UltraComboEditor cboCostCenter;

	private UltraLabel lblCostCenter;

	private ToolStripMenuItem changeClassificationsToolStripMenuItem;

	private UltraTabPageControl ultraTabPageControl7;

	private UltraComboEditor cboEINVItemGPC;

	private UltraLabel lblEINVItemGPC;

	private UltraComboEditor cboEINVItemNameType;

	private UltraLabel lblEINVItemNameType;

	private UltraTextEditor txtEINVItemNameCode;

	private UltraLabel lblEINVItemNameCode;

	private UltraButton btnEINVExportItems;

	private UltraTabPageControl ultraTabPageControl8;

	private UltraComboEditor cboGrowthTax;

	private UltraLabel lblGrowthTax;

	private UltraComboEditor cboTableTax;

	private UltraLabel lblTableTax;

	public frmItemsTree()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Expected O, but got Unknown
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Expected O, but got Unknown
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Expected O, but got Unknown
		InitializeComponent();
		IDCol = "ItemID";
		NoCol = "ItemNumber";
		NameCol = "ItemNameAr";
		NameEnCol = "ItemNameEn";
		ParentIDCol = "ParentID";
		IsMainCol = "IsMain";
		ItemLevelCol = "LevelID";
		AdditionalCol1 = "ItemBarCode";
		TableName = "SC_Items";
		LevelsTable = "SC_ItemLevels";
		LevelsCol = "LevelID";
		LevelsWidthCol = "Width";
		AllowAddRoot = true;
		rbIsItem.Checked = true;
	}

	public override void PrepareData()
	{
		UseElectronicInvoice = GlobalFunctions.GetOption("UsingElectronicInvoice");
		GenerateBarCodeSerial = GlobalFunctions.GetOption("GenerateBarCodeSerial");
		OrderByName = GlobalFunctions.GetOption("Name-Code(ItemTree)");
		base.PrepareData();
		((Control)(object)chkEnforceBatchNo).Visible = (UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod"));
		if (UseElectronicInvoice)
		{
			dtEinvSettings = BusinessLayer.EInvoices.Settings.SelectByBranchID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			if (dtEinvSettings.Rows.Count > 0)
			{
				SendingItemsByItemsTypes = bool.Parse(dtEinvSettings.Rows[0]["SendingItemsByItemsTypes"].ToString());
			}
			dtEINVItemsGPC = ItemsGPC.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboEINVItemGPC, dtEINVItemsGPC, "EINVItemGPCID", "EINVItemGPCName");
			dtEINVItemsNamesTypes = ItemsNamesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboEINVItemNameType, dtEINVItemsNamesTypes, "EINVItemNameTypeID", "EINVItemName");
		}
		((UltraTabControlBase)tabItemType).Tabs["EInvoice"].Visible = UseElectronicInvoice && !SendingItemsByItemsTypes;
		((Control)(object)lblAgeGroup).Text = GlobalFunctions.GetFormName("frmItemsAgeGroups");
		((Control)(object)lblBrand).Text = GlobalFunctions.GetFormName("frmItemsBrands");
		((Control)(object)lblClassification1).Text = GlobalFunctions.GetFormName("frmItemsFirstClassifications");
		((Control)(object)lblClassification2).Text = GlobalFunctions.GetFormName("frmItemsSecondClassifications");
		((Control)(object)lblClassification3).Text = GlobalFunctions.GetFormName("frmItemsThirdClassifications");
		((Control)(object)lblClassification4).Text = GlobalFunctions.GetFormName("frmItemsFourthClassifications");
		((Control)(object)lblCountry).Text = GlobalFunctions.GetFormName("frmItemsCountrys");
		((Control)(object)lblMaterial1).Text = GlobalFunctions.GetFormName("frmItemsFirstMaterials");
		((Control)(object)lblMaterial2).Text = GlobalFunctions.GetFormName("frmItemsSecondMaterials");
		((Control)(object)lblModel).Text = GlobalFunctions.GetFormName("frmItemsModels");
		((Control)(object)lblPriceCategory).Text = GlobalFunctions.GetFormName("frmItemsPriceCategorys");
		((Control)(object)lblSeason).Text = GlobalFunctions.GetFormName("frmItemsSeasons");
		((Control)(object)lblShape).Text = GlobalFunctions.GetFormName("frmItemsShapes");
		((Control)(object)lblColorCategory).Text = GlobalFunctions.GetFormName("frmItemsColorCategorys");
		((Control)(object)lblColor).Text = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((Control)(object)lblSizeCategory).Text = GlobalFunctions.GetFormName("frmItemsSizeCategorys");
		((Control)(object)lblSize).Text = GlobalFunctions.GetFormName("frmItemSizes");
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboServiceAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboSalesAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboSalesReturnsAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboCostOfSalesAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboPurchaseAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboPurchaseReturnsAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboDepartmentIssueAccount, dtAccounts, "AccountID", "Name");
		dtCostCenters = CostCenters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCostCenter, dtCostCenters, "CostCenterID", "Name");
		dtTaxes = BusinessLayer.General.Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboTax, dtTaxes, "TaxID", "TaxName");
		GlobalFunctions.FillCombo(cboGrowthTax, dtTaxes, "TaxID", "TaxName");
		GlobalFunctions.FillCombo(cboTableTax, dtTaxes, "TaxID", "TaxName");
		dtItemColorCategory = ItemsColorCategorys.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboColorCategory, dtItemColorCategory, "ItemColorCategoryID", "ItemColorCategoryName");
		dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboColor, dtColors, "ColorID", "ColorName");
		dtItemSizeCategory = ItemsSizeCategorys.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSizeCategory, dtItemSizeCategory, "ItemSizeCategoryID", "ItemSizeCategoryName");
		dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSize, dtSizes, "ItemSizeID", "ItemSizeName");
		dtUnitGroup = UnitsTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboUnitGroup, dtUnitGroup, "UnitTypeID", "UnitTypeName");
		dtUnits = BusinessLayer.StockControl.Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlUnits.ValueListItems.Clear();
		vlItemsUnits.ValueListItems.Clear();
		vlAccessoriesUnits.ValueListItems.Clear();
		vlAdditionalsUnits.ValueListItems.Clear();
		for (int i = 0; i < dtUnits.Rows.Count; i++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[i]["UnitID"], dtUnits.Rows[i]["UnitName"].ToString());
			vlItemsUnits.ValueListItems.Add(dtUnits.Rows[i]["UnitID"], dtUnits.Rows[i]["UnitName"].ToString());
			vlAccessoriesUnits.ValueListItems.Add(dtUnits.Rows[i]["UnitID"], dtUnits.Rows[i]["UnitName"].ToString());
			vlAdditionalsUnits.ValueListItems.Add(dtUnits.Rows[i]["UnitID"], dtUnits.Rows[i]["UnitName"].ToString());
		}
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlItems.ValueListItems.Clear();
		vlAccessoriesItems.ValueListItems.Clear();
		vlAdditionalsItems.ValueListItems.Clear();
		for (int j = 0; j < dtItems.Rows.Count; j++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[j]["ItemID"], dtItems.Rows[j]["Name"].ToString());
			vlAccessoriesItems.ValueListItems.Add(dtItems.Rows[j]["ItemID"], dtItems.Rows[j]["Name"].ToString());
			vlAdditionalsItems.ValueListItems.Add(dtItems.Rows[j]["ItemID"], dtItems.Rows[j]["Name"].ToString());
		}
		dtStores = Stores.FillCombo("-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboStores, dtStores, "StoreID", "StoreName");
		dtPricesTypes = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlPricesTypes.ValueListItems.Clear();
		for (int k = 0; k < dtPricesTypes.Rows.Count; k++)
		{
			vlPricesTypes.ValueListItems.Add(dtPricesTypes.Rows[k]["PriceTypeID"], dtPricesTypes.Rows[k]["PriceName"].ToString());
		}
		dtBranches = Branches.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlBranches.ValueListItems.Clear();
		vlBranches2.ValueListItems.Clear();
		vlBranches3.ValueListItems.Clear();
		for (int l = 0; l < dtBranches.Rows.Count; l++)
		{
			vlBranches.ValueListItems.Add(dtBranches.Rows[l]["BranchID"], dtBranches.Rows[l][GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn"].ToString());
			vlBranches2.ValueListItems.Add(dtBranches.Rows[l]["BranchID"], dtBranches.Rows[l][GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn"].ToString());
			vlBranches3.ValueListItems.Add(dtBranches.Rows[l]["BranchID"], dtBranches.Rows[l][GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn"].ToString());
		}
		dtItemsTypes = ItemsTypes.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboTypes, dtItemsTypes, "ItemTypeID", GlobalVariables.IsArabic ? "ItemTypeNameAr" : "ItemTypeNameEn");
		dtAgeGroup = ItemsAgeGroups.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboAgeGroup, dtAgeGroup, "ItemAgeGroupID", GlobalVariables.IsArabic ? "ItemAgeGroupNameAr" : "ItemAgeGroupNameEn");
		UltraLabel obj = lblAgeGroup;
		bool visible = (((Control)(object)cboAgeGroup).Visible = dtAgeGroup.Rows.Count > 0);
		((Control)(object)obj).Visible = visible;
		dtClassification1 = ItemsFirstClassifications.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboClassification1, dtClassification1, "ItemFirstClassificationID", GlobalVariables.IsArabic ? "ItemFirstClassificationNameAr" : "ItemFirstClassificationNameEn");
		UltraLabel obj2 = lblClassification1;
		visible = (((Control)(object)cboClassification1).Visible = dtClassification1.Rows.Count > 0);
		((Control)(object)obj2).Visible = visible;
		dtClassification2 = ItemsSecondClassifications.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboClassification2, dtClassification2, "ItemSecondClassificationID", GlobalVariables.IsArabic ? "ItemSecondClassificationNameAr" : "ItemSecondClassificationNameEn");
		UltraLabel obj3 = lblClassification2;
		visible = (((Control)(object)cboClassification2).Visible = dtClassification2.Rows.Count > 0);
		((Control)(object)obj3).Visible = visible;
		dtClassification3 = ItemsThirdClassifications.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboClassification3, dtClassification3, "ItemThirdClassificationID", GlobalVariables.IsArabic ? "ItemThirdClassificationNameAr" : "ItemThirdClassificationNameEn");
		UltraLabel obj4 = lblClassification3;
		visible = (((Control)(object)cboClassification3).Visible = dtClassification3.Rows.Count > 0);
		((Control)(object)obj4).Visible = visible;
		dtClassification4 = ItemsFourthClassifications.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboClassification4, dtClassification4, "ItemFourthClassificationID", GlobalVariables.IsArabic ? "ItemFourthClassificationNameAr" : "ItemFourthClassificationNameEn");
		UltraLabel obj5 = lblClassification4;
		visible = (((Control)(object)cboClassification4).Visible = dtClassification4.Rows.Count > 0);
		((Control)(object)obj5).Visible = visible;
		dtBrand = ItemsBrands.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboBrand, dtBrand, "ItemBrandID", GlobalVariables.IsArabic ? "ItemBrandNameAr" : "ItemBrandNameEn");
		UltraLabel obj6 = lblBrand;
		visible = (((Control)(object)cboBrand).Visible = dtBrand.Rows.Count > 0);
		((Control)(object)obj6).Visible = visible;
		dtSeason = ItemsSeasons.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSeason, dtSeason, "ItemSeasonID", GlobalVariables.IsArabic ? "ItemSeasonNameAr" : "ItemSeasonNameEn");
		UltraLabel obj7 = lblSeason;
		visible = (((Control)(object)cboSeason).Visible = dtSeason.Rows.Count > 0);
		((Control)(object)obj7).Visible = visible;
		dtCountry = ItemsCountrys.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCountry, dtCountry, "ItemCountryID", GlobalVariables.IsArabic ? "ItemCountryNameAr" : "ItemCountryNameEn");
		UltraLabel obj8 = lblCountry;
		visible = (((Control)(object)cboCountry).Visible = dtCountry.Rows.Count > 0);
		((Control)(object)obj8).Visible = visible;
		dtPriceCategory = ItemsPriceCategorys.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboPriceCategory, dtPriceCategory, "ItemPriceCategoryID", GlobalVariables.IsArabic ? "ItemPriceCategoryNameAr" : "ItemPriceCategoryNameEn");
		UltraLabel obj9 = lblPriceCategory;
		visible = (((Control)(object)cboPriceCategory).Visible = dtPriceCategory.Rows.Count > 0);
		((Control)(object)obj9).Visible = visible;
		dtMaterial1 = ItemsFirstMaterials.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboMaterial1, dtMaterial1, "ItemFirstMaterialID", GlobalVariables.IsArabic ? "ItemFirstMaterialNameAr" : "ItemFirstMaterialNameEn");
		UltraLabel obj10 = lblMaterial1;
		visible = (((Control)(object)cboMaterial1).Visible = dtMaterial1.Rows.Count > 0);
		((Control)(object)obj10).Visible = visible;
		dtMaterial2 = ItemsSecondMaterials.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboMaterial2, dtMaterial2, "ItemSecondMaterialID", GlobalVariables.IsArabic ? "ItemSecondMaterialNameAr" : "ItemSecondMaterialNameEn");
		UltraLabel obj11 = lblMaterial2;
		visible = (((Control)(object)cboMaterial2).Visible = dtMaterial2.Rows.Count > 0);
		((Control)(object)obj11).Visible = visible;
		dtShape = ItemsShapes.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboShape, dtShape, "ItemShapeID", GlobalVariables.IsArabic ? "ItemShapeNameAr" : "ItemShapeNameEn");
		UltraLabel obj12 = lblShape;
		visible = (((Control)(object)cboShape).Visible = dtShape.Rows.Count > 0);
		((Control)(object)obj12).Visible = visible;
		dtModel = ItemsModels.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboModel, dtModel, "ItemModelID", GlobalVariables.IsArabic ? "ItemModelNameAr" : "ItemModelNameEn");
		UltraLabel obj13 = lblModel;
		visible = (((Control)(object)cboModel).Visible = dtModel.Rows.Count > 0);
		((Control)(object)obj13).Visible = visible;
		dtSuppliers = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.SupplierSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSuppliers, dtSuppliers, "SubAccountID", "SubAccountName");
		if (Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='POS'")[0]["Installed"]) && !Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='Lenses'")[0]["Installed"]))
		{
			foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
			{
				cboPrinterName.Items.Add((object)installedPrinter, installedPrinter);
			}
			((Control)(object)lblPrepareTime).Visible = true;
			((Control)(object)lblPrinterName).Visible = true;
			((Control)(object)txtPrepareTime).Visible = true;
			((Control)(object)cboPrinterName).Visible = true;
		}
		if (Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='POS'")[0]["Installed"]))
		{
			((UltraTabControlBase)tabItemType).Tabs["Accessories"].Visible = true;
			((UltraTabControlBase)tabItemType).Tabs["Additionals"].Visible = true;
			dtItemsAccessories = ItemsAccessories.SelectByItemID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtItemsAdditionals = ItemsAdditionals.SelectByItemID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			InitGridAccessoriesItems();
			InitGridAdditionalsItems();
		}
		UltraTextEditor obj14 = txtCyl;
		UltraTextEditor obj15 = txtSph;
		UltraLabel obj16 = lblCyl;
		bool flag14 = (((Control)(object)lblSph).Visible = Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='Lenses'")[0]["Installed"]));
		bool flag16 = (((Control)(object)obj16).Visible = flag14);
		visible = (((Control)(object)obj15).Visible = flag16);
		((Control)(object)obj14).Visible = visible;
		((Control)(object)chkUsePOSNumPad).Visible = Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='Lenses'")[0]["Installed"]) || Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='POS'")[0]["Installed"]);
		dtItemPrices = ItemsPrices.SelectByItemID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtItemsUnits = ItemsUnits.SelectByItemID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtItemsUnitsPrices = ItemsUnitsPrices.SelectByItemID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		ds = new DataSet();
		ds.Tables.Add(dtItemsUnits);
		ds.Tables.Add(dtItemsUnitsPrices);
		ds.Tables[0].TableName = "dtItemsUnits";
		ds.Tables[1].TableName = "dtItemsUnitsPrices";
		ds.Relations.Add(ds.Tables[0].Columns["ItemUnitID"], ds.Tables[1].Columns["ItemUnitID"]);
		InitGridPrices();
		dtItemStockLevels = ItemsStockLevels.SelectByItemID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGridStockLevels();
		dtRecipeItems = RecipesDetails.SelectByItemID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGRecipeItems).DataSource = dtRecipeItems;
		InitGridRecipeItems();
	}

	public override void ClearControls()
	{
		if (Adding)
		{
			DisplayData();
		}
		base.ClearControls();
		if (Adding)
		{
			((Control)(object)txtCode).Text = Items.GetCode((SelectedNode == null) ? "Null" : ((KeyedSubObjectBase)SelectedNode).Key, IsFromServer: true);
			if (GenerateBarCodeSerial)
			{
				((Control)(object)txtBarCode).Text = Items.GetBarCode(IsFromServer: true);
			}
			else
			{
				((TextEditorControlBase)txtBarCode).Clear();
			}
			((Control)(object)txtEINVItemNameCode).Text = "";
			cboEINVItemNameType.SelectedIndex = -1;
			cboEINVItemGPC.SelectedIndex = -1;
			picItem.Image = null;
			dtRecipeItems.Rows.Clear();
			FillGridPrices();
			FillGridStockLevels();
			((Control)(object)txtName).Select();
			if (NodeLevel == 0)
			{
				rbIsItem.Checked = true;
			}
		}
	}

	public override void SetControls(bool NavMode)
	{
		if (Updating && !DataDisplayed)
		{
			DisplayData();
		}
		base.SetControls(NavMode);
		bool flag = false;
		if (Updating)
		{
			flag = ItemsTransactions.HasTransaction(((KeyedSubObjectBase)SelectedNode).Key);
		}
		((EditorButtonControlBase)cboUnit).ReadOnly = NavMode || flag;
		((EditorButtonControlBase)cboUnitGroup).ReadOnly = NavMode || flag;
		((EditorButtonControlBase)cboStores).ReadOnly = NavMode;
		((EditorButtonControlBase)cboServiceAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSalesAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSalesReturnsAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCostOfSalesAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPurchaseAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPurchaseReturnsAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboDepartmentIssueAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCostCenter).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPrinterName).ReadOnly = NavMode;
		((EditorButtonControlBase)cboTypes).ReadOnly = NavMode;
		((EditorButtonControlBase)cboAgeGroup).ReadOnly = NavMode;
		((EditorButtonControlBase)cboClassification1).ReadOnly = NavMode;
		((EditorButtonControlBase)cboClassification2).ReadOnly = NavMode;
		((EditorButtonControlBase)cboClassification3).ReadOnly = NavMode;
		((EditorButtonControlBase)cboClassification4).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBrand).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSeason).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCountry).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPriceCategory).ReadOnly = NavMode;
		((EditorButtonControlBase)cboMaterial1).ReadOnly = NavMode;
		((EditorButtonControlBase)cboMaterial2).ReadOnly = NavMode;
		((EditorButtonControlBase)cboShape).ReadOnly = NavMode;
		((EditorButtonControlBase)cboModel).ReadOnly = NavMode;
		((EditorButtonControlBase)cboColorCategory).ReadOnly = NavMode;
		((EditorButtonControlBase)cboColor).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSizeCategory).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSize).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSuppliers).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBarCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtValidityDays).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPrepareTime).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSph).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCyl).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSupplierItemCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNetWeight).ReadOnly = NavMode;
		((EditorButtonControlBase)txtGrowthWeight).ReadOnly = NavMode;
		((EditorButtonControlBase)txtVolume).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAccessoriesCount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboEINVItemNameType).ReadOnly = NavMode || (flag && cboEINVItemNameType.SelectedIndex != -1);
		((EditorButtonControlBase)cboEINVItemGPC).ReadOnly = NavMode || (flag && cboEINVItemGPC.SelectedIndex != -1);
		((EditorButtonControlBase)txtEINVItemNameCode).ReadOnly = NavMode || (flag && !((Control)(object)txtEINVItemNameCode).Text.Equals(""));
		((Control)(object)btnEINVExportItems).Visible = NavMode;
		((Control)(object)txtEINVItemNameCode).Enabled = cboEINVItemNameType.SelectedIndex != 1;
		((EditorButtonControlBase)cboGrowthTax).ReadOnly = NavMode;
		((EditorButtonControlBase)cboTableTax).ReadOnly = NavMode;
		((EditorButtonControlBase)cboTax).ReadOnly = NavMode;
		((Control)(object)chkIsActive).Enabled = !NavMode;
		((Control)(object)chkHideFromReports).Enabled = !NavMode;
		((Control)(object)chkIsUnitPrice).Enabled = !NavMode;
		((Control)(object)chkIsDirectItem).Enabled = !NavMode && !flag;
		((Control)(object)chkIsSalesItem).Enabled = !NavMode && (!flag || !((UltraToggleEditorBase)chkIsSalesItem).Checked);
		((Control)(object)chkIsProductionItem).Enabled = !NavMode;
		((Control)(object)chkEnforceBatchNo).Enabled = !NavMode && !flag;
		((Control)(object)chkCanModSalesPrice).Enabled = !NavMode;
		((Control)(object)chkCanModPurchasePrice).Enabled = !NavMode;
		((Control)(object)chkUsePOSNumPad).Enabled = !NavMode;
		RadioButton radioButton = rbIsItem;
		bool enabled = (rbIsRecipe.Enabled = !NavMode && (!rbIsService.Checked || SelectedNode == null));
		radioButton.Enabled = enabled;
		rbIsService.Enabled = !NavMode && (!rbIsRecipe.Checked || SelectedNode == null) && (!rbIsItem.Checked || SelectedNode == null);
		rbPriceForAllBranches.Enabled = !NavMode;
		rbPriceForEachBranches.Enabled = !NavMode;
		rbRecipeForAllBranchs.Enabled = !NavMode;
		rbRecipeForEachBranch.Enabled = !NavMode;
		rbStockLevelsForAllBranches.Enabled = !NavMode;
		rbStockLevelForEachBranch.Enabled = !NavMode;
		((Control)(object)btnPic).Visible = !NavMode;
		((UltraGridBase)ULGRecipeItems).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		if (Updating && rbIsRecipe.Checked)
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGRecipeItems).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGRecipeItems).Rows[i].ChildBands != null)
				{
					for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGRecipeItems).Rows[i].ChildBands[0].Rows).Count; j++)
					{
						if (((UltraGridBase)ULGRecipeItems).Rows[i].ChildBands[0].Rows[j].Cells["RecipeItemID"].Value != DBNull.Value)
						{
							string unitTypeID = dtItems.Select("ItemID=" + ((UltraGridBase)ULGRecipeItems).Rows[i].ChildBands[0].Rows[j].Cells["RecipeItemID"].Value)[0]["UnitTypeID"].ToString();
							ValueList unitValueList = getUnitValueList(unitTypeID);
							((UltraGridBase)ULGRecipeItems).Rows[i].ChildBands[0].Rows[j].Cells["UnitID"].ValueList = (IValueList)(object)unitValueList;
							if (((DisposableObjectCollectionBase)unitValueList.ValueListItems).Count == 0)
							{
								((UltraGridBase)ULGRecipeItems).Rows[i].ChildBands[0].Rows[j].Cells["UnitID"].Value = DBNull.Value;
							}
						}
					}
				}
				else if (((UltraGridBase)ULGRecipeItems).Rows[i].Cells["RecipeItemID"].Value != DBNull.Value)
				{
					string unitTypeID2 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGRecipeItems).Rows[i].Cells["RecipeItemID"].Value)[0]["UnitTypeID"].ToString();
					ValueList unitValueList2 = getUnitValueList(unitTypeID2);
					((UltraGridBase)ULGRecipeItems).Rows[i].Cells["UnitID"].ValueList = (IValueList)(object)unitValueList2;
					if (((DisposableObjectCollectionBase)unitValueList2.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGRecipeItems).Rows[i].Cells["UnitID"].Value = DBNull.Value;
					}
				}
			}
		}
		((UltraGridBase)ULGAccessoriesItems).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((UltraGridBase)ULGAdditionalsItems).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		if (!Updating || !Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='POS'")[0]["Installed"]))
		{
			return;
		}
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGAccessoriesItems).Rows).Count; k++)
		{
			string unitTypeID3 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGAccessoriesItems).Rows[k].Cells["AccessoriesItemID"].Value)[0]["UnitTypeID"].ToString();
			ValueList unitValueList3 = getUnitValueList(unitTypeID3);
			((UltraGridBase)ULGAccessoriesItems).Rows[k].Cells["UnitID"].ValueList = (IValueList)(object)unitValueList3;
			if (((DisposableObjectCollectionBase)unitValueList3.ValueListItems).Count == 0)
			{
				((UltraGridBase)ULGAccessoriesItems).Rows[k].Cells["UnitID"].Value = DBNull.Value;
			}
		}
		for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGAdditionalsItems).Rows).Count; l++)
		{
			string unitTypeID4 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGAdditionalsItems).Rows[l].Cells["AdditionalItemID"].Value)[0]["UnitTypeID"].ToString();
			ValueList unitValueList4 = getUnitValueList(unitTypeID4);
			((UltraGridBase)ULGAdditionalsItems).Rows[l].Cells["UnitID"].ValueList = (IValueList)(object)unitValueList4;
			if (((DisposableObjectCollectionBase)unitValueList4.ValueListItems).Count == 0)
			{
				((UltraGridBase)ULGAdditionalsItems).Rows[l].Cells["UnitID"].Value = DBNull.Value;
			}
		}
	}

	public override void DisplayData()
	{
		DataDisplayed = true;
		base.DisplayData();
		if (SelectedNode == null)
		{
			return;
		}
		DataRow dataRow = Items.Select(((KeyedSubObjectBase)SelectedNode).Key, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true).Rows[0];
		((TextEditorControlBase)cboUnit).Value = dataRow["UnitID"];
		((TextEditorControlBase)cboUnitGroup).Value = dataRow["UnitTypeID"];
		((TextEditorControlBase)cboStores).Value = dataRow["DefaultStoreID"];
		((TextEditorControlBase)cboServiceAccount).Value = dataRow["ServiceAccountID"];
		((TextEditorControlBase)cboSalesAccount).Value = dataRow["SalesAccount"];
		((TextEditorControlBase)cboSalesReturnsAccount).Value = dataRow["SalesReturnsAccount"];
		((TextEditorControlBase)cboCostOfSalesAccount).Value = dataRow["CostOfSalesAccount"];
		((TextEditorControlBase)cboPurchaseAccount).Value = dataRow["PurchaseAccount"];
		((TextEditorControlBase)cboPurchaseReturnsAccount).Value = dataRow["PurchaseReturnsAccount"];
		((TextEditorControlBase)cboDepartmentIssueAccount).Value = dataRow["DepartmentIssueAccount"];
		((TextEditorControlBase)cboCostCenter).Value = dataRow["CostCenterID"];
		((TextEditorControlBase)cboGrowthTax).Value = dataRow["GrowthTaxID"];
		((TextEditorControlBase)cboTableTax).Value = dataRow["TableTaxID"];
		((TextEditorControlBase)cboTax).Value = dataRow["TaxID"];
		((Control)(object)cboPrinterName).Text = dataRow["POSPrinter"].ToString();
		((TextEditorControlBase)cboTypes).Value = dataRow["ItemTypeID"];
		((TextEditorControlBase)cboAgeGroup).Value = dataRow["ItemAgeGroupID"];
		((TextEditorControlBase)cboClassification1).Value = dataRow["ItemFirstClassificationID"];
		((TextEditorControlBase)cboClassification2).Value = dataRow["ItemSecondClassificationID"];
		((TextEditorControlBase)cboClassification3).Value = dataRow["ItemThirdClassificationID"];
		((TextEditorControlBase)cboClassification4).Value = dataRow["ItemFourthClassificationID"];
		((TextEditorControlBase)cboBrand).Value = dataRow["ItemBrandID"];
		((TextEditorControlBase)cboSeason).Value = dataRow["ItemSeasonID"];
		((TextEditorControlBase)cboCountry).Value = dataRow["ItemCountryID"];
		((TextEditorControlBase)cboPriceCategory).Value = dataRow["ItemPriceCategoryID"];
		((TextEditorControlBase)cboMaterial1).Value = dataRow["ItemFirstMaterialID"];
		((TextEditorControlBase)cboMaterial2).Value = dataRow["ItemSecondMaterialID"];
		((TextEditorControlBase)cboShape).Value = dataRow["ItemShapeID"];
		((TextEditorControlBase)cboModel).Value = dataRow["ItemModelID"];
		((TextEditorControlBase)cboColorCategory).Value = dataRow["ItemColorCategoryID"];
		((TextEditorControlBase)cboColor).Value = dataRow["ColorID"];
		((TextEditorControlBase)cboSizeCategory).Value = dataRow["ItemSizeCategoryID"];
		((TextEditorControlBase)cboSize).Value = dataRow["ItemSizID"];
		((TextEditorControlBase)cboSuppliers).Value = dataRow["SubAccountID"];
		((TextEditorControlBase)cboEINVItemGPC).Value = dataRow["EINVItemGPCID"];
		((TextEditorControlBase)cboEINVItemNameType).ValueChanged -= cboEINVItemNameType_ValueChanged;
		((TextEditorControlBase)cboEINVItemNameType).Value = dataRow["EINVItemNameTypeID"];
		((TextEditorControlBase)cboEINVItemNameType).ValueChanged += cboEINVItemNameType_ValueChanged;
		((Control)(object)txtEINVItemNameCode).Text = dataRow["EINVItemNameCode"].ToString();
		((UltraToggleEditorBase)chkIsActive).Checked = Convert.ToBoolean(dataRow["IsActive"]);
		((UltraToggleEditorBase)chkHideFromReports).Checked = Convert.ToBoolean(dataRow["HideFromReports"]);
		((UltraToggleEditorBase)chkIsUnitPrice).Checked = Convert.ToBoolean(dataRow["IsUnitPrice"]);
		((UltraToggleEditorBase)chkIsDirectItem).Checked = Convert.ToBoolean(dataRow["IsDirectItem"]);
		((UltraToggleEditorBase)chkIsSalesItem).Checked = Convert.ToBoolean(dataRow["IsSalesItem"]);
		((UltraToggleEditorBase)chkIsProductionItem).Checked = Convert.ToBoolean(dataRow["IsProductionItem"]);
		((UltraToggleEditorBase)chkEnforceBatchNo).Checked = Convert.ToBoolean(dataRow["EnforceBatchNo"]);
		((UltraToggleEditorBase)chkCanModSalesPrice).Checked = Convert.ToBoolean(dataRow["CanModifyPrice"]);
		((UltraToggleEditorBase)chkCanModPurchasePrice).Checked = Convert.ToBoolean(dataRow["CanModifyPurchasePrice"]);
		((UltraToggleEditorBase)chkUsePOSNumPad).Checked = !dataRow["UsePOSNumPad"].Equals(DBNull.Value) && Convert.ToBoolean(dataRow["UsePOSNumPad"]);
		rbIsItem.Checked = Convert.ToBoolean(dataRow["IsItem"]);
		rbIsRecipe.Checked = Convert.ToBoolean(dataRow["IsRecipe"]);
		rbIsService.Checked = Convert.ToBoolean(dataRow["IsService"]);
		rbPriceForAllBranches.Checked = Convert.ToBoolean(dataRow["PriceForAllBranch"]);
		rbPriceForEachBranches.Checked = !Convert.ToBoolean(dataRow["PriceForAllBranch"]);
		rbRecipeForAllBranchs.Checked = Convert.ToBoolean(dataRow["RecipeForAllBranch"]);
		rbRecipeForEachBranch.Checked = !Convert.ToBoolean(dataRow["RecipeForAllBranch"]);
		rbStockLevelsForAllBranches.Checked = !dataRow["StockLevelsForAllBranch"].Equals(DBNull.Value) && Convert.ToBoolean(dataRow["StockLevelsForAllBranch"]);
		rbStockLevelForEachBranch.Checked = dataRow["StockLevelsForAllBranch"].Equals(DBNull.Value) || !Convert.ToBoolean(dataRow["StockLevelsForAllBranch"]);
		modifyGroupItemsToolStripMenuItem4.Enabled = Convert.ToBoolean(dataRow["IsMain"]);
		deleteToolStripMenuItem.Enabled = Convert.ToBoolean(dataRow["IsMain"]);
		changeParentToolStripMenuItem.Enabled = !Convert.ToBoolean(dataRow["IsMain"]);
		((Control)(object)chkIsUnitPrice).Visible = !Convert.ToBoolean(dataRow["IsService"]);
		((Control)(object)txtBarCode).Text = dataRow["ItemBarCode"].ToString();
		((Control)(object)txtValidityDays).Text = dataRow["ValidityDays"].ToString();
		((Control)(object)txtNotes).Text = dataRow["Notes"].ToString();
		((Control)(object)txtPrepareTime).Text = dataRow["PrepareTime"].ToString();
		((Control)(object)txtSph).Text = dataRow["X"].ToString();
		((Control)(object)txtCyl).Text = dataRow["Y"].ToString();
		((Control)(object)txtSupplierItemCode).Text = dataRow["SupplierItemCode"].ToString();
		((Control)(object)txtNetWeight).Text = dataRow["NetWeight"].ToString();
		((Control)(object)txtGrowthWeight).Text = dataRow["GrowthWeight"].ToString();
		((Control)(object)txtVolume).Text = dataRow["Volume"].ToString();
		((Control)(object)txtAccessoriesCount).Text = dataRow["AccessoriesCount"].ToString();
		if (dataRow["ItemPic"] != DBNull.Value)
		{
			picItem.Image = GlobalFunctions.BinaryToImage((byte[])dataRow["ItemPic"]);
		}
		else
		{
			picItem.Image = null;
		}
		dtItemPrices = ItemsPrices.SelectByItemID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (!Main.Success)
		{
			DataDisplayed = false;
		}
		dtItemsUnits = ItemsUnits.SelectByItemID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (!Main.Success)
		{
			DataDisplayed = false;
		}
		dtItemsUnitsPrices = ItemsUnitsPrices.SelectByItemID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (!Main.Success)
		{
			DataDisplayed = false;
		}
		ds = new DataSet();
		ds.Tables.Add(dtItemsUnits);
		ds.Tables.Add(dtItemsUnitsPrices);
		ds.Tables[0].TableName = "dtItemsUnits";
		ds.Tables[1].TableName = "dtItemsUnitsPrices";
		ds.Relations.Add(ds.Tables[0].Columns["ItemUnitID"], ds.Tables[1].Columns["ItemUnitID"]);
		InitGridPrices();
		dtItemStockLevels = ItemsStockLevels.SelectByItemID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (!Main.Success)
		{
			DataDisplayed = false;
		}
		InitGridStockLevels();
		if (rbIsRecipe.Checked)
		{
			dtRecipeItems = RecipesDetails.SelectByItemID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			if (!Main.Success)
			{
				DataDisplayed = false;
			}
			if (rbRecipeForAllBranchs.Checked)
			{
				((UltraGridBase)ULGRecipeItems).DataSource = dtRecipeItems;
			}
			else
			{
				dsRecipe = new DataSet();
				dsRecipe.Tables.Add(dtBranches.Copy());
				dsRecipe.Tables.Add(dtRecipeItems.Copy());
				dsRecipe.Tables[0].TableName = "dtBranches";
				dsRecipe.Tables[1].TableName = "dtRecipeItems";
				dsRecipe.Relations.Add(dsRecipe.Tables[0].Columns["BranchID"], dsRecipe.Tables[1].Columns["BranchID"]);
				((UltraGridBase)ULGRecipeItems).DataSource = dsRecipe;
			}
		}
		else if (dtRecipeItems != null)
		{
			dtRecipeItems.Rows.Clear();
		}
		InitGridRecipeItems();
		if (Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='POS'")[0]["Installed"]))
		{
			dtItemsAccessories = ItemsAccessories.SelectByItemID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			if (!Main.Success)
			{
				DataDisplayed = false;
			}
			InitGridAccessoriesItems();
			dtItemsAdditionals = ItemsAdditionals.SelectByItemID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			if (!Main.Success)
			{
				DataDisplayed = false;
			}
			InitGridAdditionalsItems();
		}
	}

	public override bool ValidateData()
	{
		DataRow[] array = dtChart.Select(AdditionalCol1 + " = '" + ((((Control)(object)txtBarCode).Text.Trim() == "") ? GetCode() : ((Control)(object)txtBarCode).Text) + "'");
		if ((array.Length != 0 && (Adding || array[0][IDCol].ToString() != ((KeyedSubObjectBase)SelectedNode).Key)) || array.Length > 1)
		{
			GlobalVariables.InformationMB.Show("هذا الباركود موجود من قبل", "This BarCode is Already Exists..");
			return false;
		}
		if (((Control)(object)txtBarCode).Text.Trim().Contains(char.Parse(GlobalFunctions.GetDefault("BarcodeQuantitySeparator")).ToString()))
		{
			GlobalVariables.InformationMB.Show("هذا الباركود يحتوي على BarcodeQuantitySeparator ", "This BarCode Contains Barcode Quantity Separator");
			return false;
		}
		if (((Control)(object)txtBarCode).Text.Trim().Contains(char.Parse(GlobalFunctions.GetDefault("BarcodeBatchNoSeparator")).ToString()))
		{
			GlobalVariables.InformationMB.Show("هذا الباركود يحتوي على BarcodeBatchNoSeparator ", "This BarCode Contains Barcode BatchNo Separator");
			return false;
		}
		if (((Control)(object)txtBarCode).Text.Trim().Contains(char.Parse(GlobalFunctions.GetDefault("BarcodeSizeSeparator")).ToString()))
		{
			GlobalVariables.InformationMB.Show("هذا الباركود يحتوي على BarcodeSizeSeparator ", "This BarCode Contains Barcode Size Separator");
			return false;
		}
		if (((Control)(object)txtBarCode).Text.Trim().Contains(char.Parse(GlobalFunctions.GetDefault("BarcodeColorSeparator")).ToString()))
		{
			GlobalVariables.InformationMB.Show("هذا الباركود يحتوي على BarcodeColorSeparator ", "This BarCode Contains Barcode Color Separator");
			return false;
		}
		if (rbIsItem.Checked || rbIsRecipe.Checked)
		{
			if (cboUnitGroup.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار مجموعه الوحده", "Please select Unit Group");
				return false;
			}
			if (cboUnit.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار الوحده", "Please select Unit ");
				return false;
			}
			if (cboStores.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار المخزن الإفتراضي", "Please select Default Store");
				return false;
			}
			if (((UltraToggleEditorBase)chkEnforceBatchNo).Checked && ((Control)(object)txtValidityDays).Text == "")
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال عدد أيام الصلاحيه", "Please Enter Validity Days");
				return false;
			}
		}
		else if (rbIsService.Checked && cboServiceAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار حساب الخدمه", "Please select Service Account");
			return false;
		}
		if (UseElectronicInvoice)
		{
			if (!SendingItemsByItemsTypes)
			{
				if (cboEINVItemNameType.SelectedIndex == -1)
				{
					GlobalVariables.InformationMB.Show("برجاء إختيار نوع التصنيف", "Please select Type");
					((UltraTabControlBase)tabItemType).Tabs["EInvoice"].Selected = true;
					((TextEditorControlBase)cboEINVItemNameType).Focus();
					return false;
				}
				if (cboEINVItemGPC.SelectedIndex == -1)
				{
					GlobalVariables.InformationMB.Show("برجاء إختيار ال GPC", "Please select GPC");
					((UltraTabControlBase)tabItemType).Tabs["EInvoice"].Selected = true;
					((TextEditorControlBase)cboEINVItemGPC).Focus();
					return false;
				}
				if (((Control)(object)txtEINVItemNameCode).Text.Trim().Equals(""))
				{
					GlobalVariables.InformationMB.Show("برجاء إختيار كود التصنيف", "Please select Type Code");
					((UltraTabControlBase)tabItemType).Tabs["EInvoice"].Selected = true;
					((TextEditorControlBase)txtEINVItemNameCode).Focus();
					return false;
				}
				if (Main.CheckForValue("SC_Items", "EINVItemNameCode", ((Control)(object)txtEINVItemNameCode).Text, "", IsFromServer: true, string.Concat(" And  EINVItemNameTypeID = ", ((TextEditorControlBase)cboEINVItemNameType).Value, "  And ItemID <> ", Adding ? "-1" : ((KeyedSubObjectBase)SelectedNode).Key.ToString())) > 0)
				{
					GlobalVariables.InformationMB.Show(" كود التصنيف متواجد من قبل ", "Type Code Already Exist");
					((TextEditorControlBase)txtEINVItemNameCode).Focus();
					return false;
				}
			}
			else if (cboTypes.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار نوع التصنيف", "Please select Item Type");
				((TextEditorControlBase)cboTypes).Focus();
				return false;
			}
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGLevels).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء ادخال حدود المخزون   ", "Please Enter stock Levels ");
			((UltraTabControlBase)tabItemType).Tabs["Levels"].Selected = true;
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGLevels).Rows).Count; i++)
		{
			if (Convert.ToDecimal(((UltraGridBase)ULGLevels).Rows[i].Cells["MaxLevel"].Value) < Convert.ToDecimal(((UltraGridBase)ULGLevels).Rows[i].Cells["MinLevel"].Value))
			{
				GlobalVariables.InformationMB.Show("الحد الأقصي يجب ان يكون اكبر من الحد الأدني", "MaxLevel Must Be Greater Than MinLevel");
				ULGLevels.ActiveCell = ((UltraGridBase)ULGLevels).Rows[i].Cells["MaxLevel"];
				return false;
			}
		}
		string text = "," + ((Control)(object)txtBarCode).Text + ",";
		string text2 = ",";
		bool flag = true;
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGPrices).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء ادخال سعر وحدة واحده على الاقل   ", "Please Enter Unit Prices For This Item ");
			((UltraTabControlBase)tabItemType).Tabs["Prices"].Selected = true;
			return false;
		}
		if (((UltraToggleEditorBase)chkIsUnitPrice).Checked)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGPrices).Rows).Count; j++)
			{
				if (((UltraGridBase)ULGPrices).Rows[j].Cells["UnitID"].Value == DBNull.Value)
				{
					((UltraTabControlBase)tabItemType).Tabs["Prices"].Selected = true;
					GlobalVariables.InformationMB.Show("برجاء ادخال اسم الوحدة  ", "Please Enter Unit Name or Delete Row ");
					ULGPrices.ActiveCell = ((UltraGridBase)ULGPrices).Rows[j].Cells["UnitID"];
					return false;
				}
				if (((UltraGridBase)ULGPrices).Rows[j].Cells["ItemUnitBarcode"].Value == DBNull.Value || ((UltraGridBase)ULGPrices).Rows[j].Cells["ItemUnitBarcode"].Value.ToString() == "")
				{
					((UltraTabControlBase)tabItemType).Tabs["Prices"].Selected = true;
					GlobalVariables.InformationMB.Show("برجاء ادخال باركود الوحدة  ", "Please Enter Item Unit Barcode");
					ULGPrices.ActiveCell = ((UltraGridBase)ULGPrices).Rows[j].Cells["ItemUnitBarcode"];
					return false;
				}
				if (((UltraGridBase)ULGPrices).Rows[j].Cells["ItemUnitBarcode"].Value.ToString().Equals(((Control)(object)txtBarCode).Text))
				{
					((UltraTabControlBase)tabItemType).Tabs["Prices"].Selected = true;
					GlobalVariables.InformationMB.Show("لا يمكن تكرار باركود الصنف مع باركود الوحدة", "Cannot Duplicate The Item Unit Barcode With The ItemBarcode");
					ULGPrices.ActiveCell = ((UltraGridBase)ULGPrices).Rows[j].Cells["ItemUnitBarcode"];
					return false;
				}
				if (((UltraGridBase)ULGPrices).Rows[j].Cells["ItemUnitBarcode"].Value.ToString().Trim().Contains(char.Parse(GlobalFunctions.GetDefault("BarcodeQuantitySeparator")).ToString()))
				{
					((UltraTabControlBase)tabItemType).Tabs["Prices"].Selected = true;
					GlobalVariables.InformationMB.Show("هذا الباركود يحتوي على BarcodeQuantitySeparator ", "This BarCode Contains Barcode Quantity Separator");
					ULGPrices.ActiveCell = ((UltraGridBase)ULGPrices).Rows[j].Cells["ItemUnitBarcode"];
					return false;
				}
				if (((UltraGridBase)ULGPrices).Rows[j].Cells["ItemUnitBarcode"].Value.ToString().Trim().Contains(char.Parse(GlobalFunctions.GetDefault("BarcodeBatchNoSeparator")).ToString()))
				{
					((UltraTabControlBase)tabItemType).Tabs["Prices"].Selected = true;
					GlobalVariables.InformationMB.Show("هذا الباركود يحتوي على BarcodeBatchNoSeparator ", "This BarCode Contains Barcode BatchNo Separator");
					ULGPrices.ActiveCell = ((UltraGridBase)ULGPrices).Rows[j].Cells["ItemUnitBarcode"];
					return false;
				}
				if (((UltraGridBase)ULGPrices).Rows[j].Cells["ItemUnitBarcode"].Value.ToString().Trim().Contains(char.Parse(GlobalFunctions.GetDefault("BarcodeSizeSeparator")).ToString()))
				{
					((UltraTabControlBase)tabItemType).Tabs["Prices"].Selected = true;
					GlobalVariables.InformationMB.Show("هذا الباركود يحتوي على BarcodeSizeSeparator ", "This BarCode Contains Barcode Size Separator");
					ULGPrices.ActiveCell = ((UltraGridBase)ULGPrices).Rows[j].Cells["ItemUnitBarcode"];
					return false;
				}
				if (((UltraGridBase)ULGPrices).Rows[j].Cells["ItemUnitBarcode"].Value.ToString().Trim().Contains(char.Parse(GlobalFunctions.GetDefault("BarcodeColorSeparator")).ToString()))
				{
					((UltraTabControlBase)tabItemType).Tabs["Prices"].Selected = true;
					GlobalVariables.InformationMB.Show("هذا الباركود يحتوي على BarcodeColorSeparator ", "This BarCode Contains Barcode Color Separator");
					ULGPrices.ActiveCell = ((UltraGridBase)ULGPrices).Rows[j].Cells["ItemUnitBarcode"];
					return false;
				}
				text = text + ((UltraGridBase)ULGPrices).Rows[j].Cells["ItemUnitBarcode"].Value.ToString() + ",";
				text2 = text2 + ((UltraGridBase)ULGPrices).Rows[j].Cells["ItemUnitID"].Value.ToString() + ",";
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGPrices).Rows).Count; k++)
				{
					if (j != k && ((UltraGridBase)ULGPrices).Rows[j].Cells["UnitID"].Value.ToString() == ((UltraGridBase)ULGPrices).Rows[k].Cells["UnitID"].Value.ToString())
					{
						flag = false;
					}
					if (j != k && ((UltraGridBase)ULGPrices).Rows[j].Cells["ItemUnitBarcode"].Value.ToString() == ((UltraGridBase)ULGPrices).Rows[k].Cells["ItemUnitBarcode"].Value.ToString())
					{
						((UltraTabControlBase)tabItemType).Tabs["Prices"].Selected = true;
						GlobalVariables.InformationMB.Show("لا يمكن تكرار الباركود", "Cannot Duplicate The Item Unit Barcode");
						ULGPrices.ActiveCell = ((UltraGridBase)ULGPrices).Rows[j].Cells["ItemUnitBarcode"];
						return false;
					}
				}
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGPrices).Rows[j].ChildBands[0].Rows).Count; l++)
				{
					if (((UltraGridBase)ULGPrices).Rows[j].ChildBands[0].Rows[l].Cells["Price"].Value == DBNull.Value)
					{
						((UltraTabControlBase)tabItemType).Tabs["Prices"].Selected = true;
						GlobalVariables.InformationMB.Show("برجاء ادخال السعر  ", "Please Enter Price ");
						ULGPrices.ActiveCell = ((UltraGridBase)ULGPrices).Rows[j].ChildBands[0].Rows[l].Cells["Price"];
						ULGPrices.PerformAction((UltraGridAction)24);
						return false;
					}
				}
			}
		}
		else
		{
			for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGPrices).Rows).Count; m++)
			{
				if (Updating && ((UltraGridBase)ULGPrices).Rows[m].Cells["ItemID"].Value.ToString() != ((KeyedSubObjectBase)SelectedNode).Key)
				{
					GlobalVariables.InformationMB.Show("برجاء تنشيط بيانات الاسعار  ", "Please Refresh Item Prices ");
					return false;
				}
				if (((UltraGridBase)ULGPrices).Rows[m].Cells["Price"].Value == DBNull.Value)
				{
					((UltraTabControlBase)tabItemType).Tabs["Prices"].Selected = true;
					GlobalVariables.InformationMB.Show("برجاء ادخال السعر  ", "Please Enter Price ");
					ULGPrices.ActiveCell = ((UltraGridBase)ULGPrices).Rows[m].Cells["Price"];
					ULGPrices.PerformAction((UltraGridAction)24);
					return false;
				}
			}
		}
		if (text2 == ",")
		{
			text2 = "-1";
		}
		DataTable dataTable = Items.CheckForItemBarcode(Adding ? "-1" : ((KeyedSubObjectBase)SelectedNode).Key, text2, text, IsFromServer: true);
		if (dataTable.Rows.Count > 0)
		{
			((UltraTabControlBase)tabItemType).Tabs["Prices"].Selected = true;
			GlobalVariables.InformationMB.Show(dataTable.Rows[0]["ItemBarcode"].ToString() + "هذا الباركود موجود من قبل  ", "This UnitBarcode Already Exists " + dataTable.Rows[0]["ItemBarcode"].ToString());
			return false;
		}
		if (!flag)
		{
			GlobalVariables.QuestionMB.Show("يوجد تكرار في الوحدات هل توافق على التكرار؟", "There Is A Duplicate in Units Do You Want To Continue?");
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((UltraTabControlBase)tabItemType).Tabs["Prices"].Selected = true;
				return false;
			}
		}
		if (Updating && ((DisposableObjectCollectionBase)((UltraGridBase)ULGRecipeItems).Rows).Count > 0)
		{
			((UltraGridBase)ULGRecipeItems).UpdateData();
			string text3 = "";
			for (int n = 0; n < ((DisposableObjectCollectionBase)((UltraGridBase)ULGRecipeItems).Rows).Count; n++)
			{
				if (((UltraGridBase)ULGRecipeItems).Rows[n].ChildBands != null)
				{
					for (int num = 0; num < ((DisposableObjectCollectionBase)((UltraGridBase)ULGRecipeItems).Rows[n].ChildBands[0].Rows).Count; num++)
					{
						if (((UltraGridBase)ULGRecipeItems).Rows[n].ChildBands[0].Rows[num].Cells["RecipeItemID"].Value.ToString() == ((KeyedSubObjectBase)SelectedNode).Key.ToString())
						{
							GlobalVariables.InformationMB.Show("لا يمكن اختيار نفس الصنف في الوصفه", "Useing same Item Is not allowed");
							((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Recipe"];
							ULGRecipeItems.ActiveCell = ((UltraGridBase)ULGRecipeItems).Rows[n].ChildBands[0].Rows[num].Cells["RecipeItemID"];
							return false;
						}
						text3 = text3 + "," + ((UltraGridBase)ULGRecipeItems).Rows[n].ChildBands[0].Rows[num].Cells["RecipeItemID"].Value.ToString();
					}
				}
				else
				{
					if (((UltraGridBase)ULGRecipeItems).Rows[n].Cells["RecipeItemID"].Value.ToString() == ((KeyedSubObjectBase)SelectedNode).Key)
					{
						GlobalVariables.InformationMB.Show("لا يمكن اختيار نفس الصنف في الوصفه", "Useing same Item Is not allowed");
						((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Recipe"];
						ULGRecipeItems.ActiveCell = ((UltraGridBase)ULGRecipeItems).Rows[n].Cells["RecipeItemID"];
						return false;
					}
					text3 = text3 + "," + ((UltraGridBase)ULGRecipeItems).Rows[n].Cells["RecipeItemID"].Value.ToString();
				}
			}
			text3 += ",";
			if (Convert.ToBoolean(RecipesDetails.CheckForLoops(text3, ((KeyedSubObjectBase)SelectedNode).Key, IsFromServer: true).Rows[0]["HasLoops"]))
			{
				GlobalVariables.InformationMB.Show("مكونات الوصفه غير سليمه(قد تحتوي نفس الصنف في احد مكوناتها)  ", "Useing Recipes Which contains same Item Is not allowed");
				((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Recipe"];
				return false;
			}
		}
		if ((((Control)(object)txtAccessoriesCount).Text == "" || int.Parse(((Control)(object)txtAccessoriesCount).Text) <= 0) && ((DisposableObjectCollectionBase)((UltraGridBase)ULGAccessoriesItems).Rows).Count > 0)
		{
			GlobalVariables.InformationMB.Show(" برجاء إدخال عدد الملحقات", "Please  Enter Accessories Count");
			return false;
		}
		if (((Control)(object)txtAccessoriesCount).Text != "" && int.Parse(((Control)(object)txtAccessoriesCount).Text) > 0 && ((DisposableObjectCollectionBase)((UltraGridBase)ULGAccessoriesItems).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show(" برجاء إختيار أصناف الملحقات", "Please Select Accessories Items");
			return false;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGAccessoriesItems).Rows).Count > 0)
		{
			for (int num2 = 0; num2 < ((DisposableObjectCollectionBase)((UltraGridBase)ULGAccessoriesItems).Rows).Count; num2++)
			{
				if (((UltraGridBase)ULGAccessoriesItems).Rows[num2].Cells["AccessoriesItemID"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show(" برجاء إختيار أصناف الملحقات", "Please Select Accessories Items");
					((UltraTabControlBase)tabItemType).Tabs["Accessories"].Selected = true;
					return false;
				}
				if (((UltraGridBase)ULGAccessoriesItems).Rows[num2].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGAccessoriesItems).Rows[num2].Cells["Qty"].Value.ToString()) <= 0m)
				{
					GlobalVariables.InformationMB.Show(" برجاء إدخال كمية الملحق", "Please Enter Accessories Quantity");
					((UltraTabControlBase)tabItemType).Tabs["Accessories"].Selected = true;
					return false;
				}
				if (((UltraGridBase)ULGAccessoriesItems).Rows[num2].Cells["UnitID"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show(" برجاء إختيار الوحدة", "Please Select Unit");
					((UltraTabControlBase)tabItemType).Tabs["Accessories"].Selected = true;
					return false;
				}
			}
		}
		else if (((DisposableObjectCollectionBase)((UltraGridBase)ULGAdditionalsItems).Rows).Count > 0)
		{
			for (int num3 = 0; num3 < ((DisposableObjectCollectionBase)((UltraGridBase)ULGAdditionalsItems).Rows).Count; num3++)
			{
				if (((UltraGridBase)ULGAdditionalsItems).Rows[num3].Cells["AdditionalItemID"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show(" برجاء إختيار أصناف ", "Please Select Additionals Items");
					((UltraTabControlBase)tabItemType).Tabs["Additionals"].Selected = true;
					return false;
				}
				if (((UltraGridBase)ULGAdditionalsItems).Rows[num3].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGAdditionalsItems).Rows[num3].Cells["Qty"].Value.ToString()) <= 0m)
				{
					GlobalVariables.InformationMB.Show(" برجاء إدخال كمية الإضافات", "Please Enter Additionals Quantity");
					((UltraTabControlBase)tabItemType).Tabs["Additionals"].Selected = true;
					return false;
				}
				if (((UltraGridBase)ULGAdditionalsItems).Rows[num3].Cells["UnitID"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show(" برجاء إختيار الوحدة", "Please Select Unit");
					((UltraTabControlBase)tabItemType).Tabs["Additionals"].Selected = true;
					return false;
				}
			}
		}
		return base.ValidateData();
	}

	public override int TreeAddData()
	{
		int num = 0;
		Main.StartBulkTrans(FromServer: true);
		try
		{
			num = Items.Insert_Update("-1", GetCode(), ((Control)(object)txtName).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? "Null" : ((Control)(object)txtNameEn).Text.Trim(), (SelectedNode == null) ? "Null" : ((DataRow)((SubObjectBase)SelectedNode).Tag)[0].ToString(), "0", (NodeLevel + 1).ToString(), (((Control)(object)txtBarCode).Text.Trim() == "") ? GetCode() : ((Control)(object)txtBarCode).Text, (rbIsService.Checked || cboUnit.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUnit).Value.ToString(), (rbIsService.Checked || cboUnitGroup.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUnitGroup).Value.ToString(), rbStockLevelsForAllBranches.Checked ? "1" : "0", (((Control)(object)txtNotes).Text.Trim() == "") ? "Null" : ((Control)(object)txtNotes).Text, (!rbIsService.Checked && ((UltraToggleEditorBase)chkIsProductionItem).Checked) ? "1" : "0", ((UltraToggleEditorBase)chkIsActive).Checked ? "1" : "0", ((UltraToggleEditorBase)chkHideFromReports).Checked ? "1" : "0", rbIsItem.Checked ? "1" : "0", rbIsService.Checked ? "1" : "0", rbIsRecipe.Checked ? "1" : "0", (!rbIsService.Checked && ((UltraToggleEditorBase)chkIsDirectItem).Checked) ? "1" : "0", (!rbIsService.Checked && ((UltraToggleEditorBase)chkIsSalesItem).Checked) ? "1" : "0", (!rbIsService.Checked || cboServiceAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboServiceAccount).Value.ToString(), ((UltraToggleEditorBase)chkIsUnitPrice).Checked ? "1" : "0", rbPriceForAllBranches.Checked ? "1" : "0", rbRecipeForAllBranchs.Checked ? "1" : "0", (!rbIsService.Checked && ((UltraToggleEditorBase)chkEnforceBatchNo).Checked) ? "1" : "0", (rbIsService.Checked || cboStores.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboStores).Value.ToString(), (rbIsService.Checked || ((Control)(object)txtValidityDays).Text.Trim() == "") ? "Null" : ((Control)(object)txtValidityDays).Text, (cboGrowthTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboGrowthTax).Value.ToString(), (cboTableTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTableTax).Value.ToString(), (cboTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTax).Value.ToString(), (cboSalesAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesAccount).Value.ToString(), (cboSalesReturnsAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesReturnsAccount).Value.ToString(), (cboCostOfSalesAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCostOfSalesAccount).Value.ToString(), (cboPurchaseAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPurchaseAccount).Value.ToString(), (cboPurchaseReturnsAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPurchaseReturnsAccount).Value.ToString(), (cboDepartmentIssueAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDepartmentIssueAccount).Value.ToString(), (cboCostCenter.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCostCenter).Value.ToString(), (((Control)(object)txtSph).Text.Trim() == "") ? "0" : ((Control)(object)txtSph).Text, (((Control)(object)txtCyl).Text.Trim() == "") ? "0" : ((Control)(object)txtCyl).Text, (((Control)(object)txtPrepareTime).Text.Trim() == "") ? "Null" : ((Control)(object)txtPrepareTime).Text, ((UltraToggleEditorBase)chkCanModSalesPrice).Checked ? "1" : "0", ((UltraToggleEditorBase)chkCanModPurchasePrice).Checked ? "1" : "0", ((UltraToggleEditorBase)chkUsePOSNumPad).Checked ? "1" : "0", (((Control)(object)cboPrinterName).Text == "") ? "Null" : ((Control)(object)cboPrinterName).Text.ToString(), (cboTypes.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTypes).Value.ToString(), (cboAgeGroup.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAgeGroup).Value.ToString(), (cboClassification1.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClassification1).Value.ToString(), (cboClassification2.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClassification2).Value.ToString(), (cboClassification3.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClassification3).Value.ToString(), (cboClassification4.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClassification4).Value.ToString(), (cboBrand.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBrand).Value.ToString(), (cboSeason.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSeason).Value.ToString(), (cboCountry.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCountry).Value.ToString(), (cboPriceCategory.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPriceCategory).Value.ToString(), (cboMaterial1.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboMaterial1).Value.ToString(), (cboMaterial2.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboMaterial2).Value.ToString(), (cboShape.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboShape).Value.ToString(), (cboModel.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboModel).Value.ToString(), (cboColorCategory.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboColorCategory).Value.ToString(), (cboColor.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboColor).Value.ToString(), (cboSizeCategory.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSizeCategory).Value.ToString(), (cboSize.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSize).Value.ToString(), (cboSuppliers.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSuppliers).Value.ToString(), (((Control)(object)txtSupplierItemCode).Text.Trim() == "") ? "Null" : ((Control)(object)txtSupplierItemCode).Text, (((Control)(object)txtNetWeight).Text.Trim() == "") ? "0" : ((Control)(object)txtNetWeight).Text, (((Control)(object)txtGrowthWeight).Text.Trim() == "") ? "0" : ((Control)(object)txtGrowthWeight).Text, (((Control)(object)txtVolume).Text.Trim() == "") ? "0" : ((Control)(object)txtVolume).Text, (((Control)(object)txtAccessoriesCount).Text.Trim() == "") ? "0" : ((Control)(object)txtAccessoriesCount).Text, (cboEINVItemNameType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboEINVItemNameType).Value.ToString(), (((Control)(object)txtEINVItemNameCode).Text.Trim() == "") ? "Null" : ((Control)(object)txtEINVItemNameCode).Text, (cboEINVItemGPC.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboEINVItemGPC).Value.ToString(), "0", GlobalVariables.CurrentBranchID.ToString(), GlobalVariables.UserID, IsFromServer: true);
			if (picItem.Image != null)
			{
				Items.PicUpdate(num.ToString(), GlobalFunctions.ImageToBinary((Image)picItem.Image), IsFromServer: true);
			}
			dtItemStockLevels.AcceptChanges();
			for (int i = 0; i < dtItemStockLevels.Rows.Count; i++)
			{
				ItemsStockLevels.Insert_Update("-1", num.ToString(), dtItemStockLevels.Rows[i]["MinLevel"].ToString(), dtItemStockLevels.Rows[i]["ReOrderLevel"].ToString(), dtItemStockLevels.Rows[i]["MaxLevel"].ToString(), "0", rbStockLevelsForAllBranches.Checked ? "Null" : dtItemStockLevels.Rows[i]["BranchID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			}
			if (((UltraToggleEditorBase)chkIsUnitPrice).Checked)
			{
				dtItemPrices.Rows.Clear();
				dtItemsUnits.AcceptChanges();
				dtItemsUnitsPrices.AcceptChanges();
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGPrices).Rows).Count; j++)
				{
					int num2 = ItemsUnits.Insert_Update("-1", num.ToString(), ((UltraGridBase)ULGPrices).Rows[j].Cells["ItemUnitBarcode"].Value.ToString(), (((UltraGridBase)ULGPrices).Rows[j].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGPrices).Rows[j].Cells["UnitID"].Value.ToString(), "0", GlobalVariables.CurrentBranchID.ToString(), GlobalVariables.UserID, IsFromServer: true);
					for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGPrices).Rows[j].ChildBands[0].Rows).Count; k++)
					{
						ItemsUnitsPrices.Insert_Update("-1", num2.ToString(), num.ToString(), ((UltraGridBase)ULGPrices).Rows[j].ChildBands[0].Rows[k].Cells["PriceTypeID"].Value.ToString(), ((UltraGridBase)ULGPrices).Rows[j].ChildBands[0].Rows[k].Cells["Price"].Value.ToString(), "0", rbPriceForAllBranches.Checked ? "Null" : ((UltraGridBase)ULGPrices).Rows[j].ChildBands[0].Rows[k].Cells["BranchID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
					}
				}
			}
			else
			{
				dtItemsUnitsPrices.Rows.Clear();
				dtItemsUnits.Rows.Clear();
				dtItemPrices.AcceptChanges();
				for (int l = 0; l < dtItemPrices.Rows.Count; l++)
				{
					ItemsPrices.Insert_Update("-1", num.ToString(), dtItemPrices.Rows[l]["PriceTypeID"].ToString(), dtItemPrices.Rows[l]["Price"].ToString(), "0", rbPriceForAllBranches.Checked ? "Null" : dtItemPrices.Rows[l]["BranchID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
				}
			}
			dtRecipeItems.AcceptChanges();
			((UltraGridBase)ULGRecipeItems).UpdateData();
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGRecipeItems).Rows).Count > 0)
			{
				for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGRecipeItems).Rows).Count; m++)
				{
					if (((UltraGridBase)ULGRecipeItems).Rows[m].ChildBands != null)
					{
						for (int n = 0; n < ((DisposableObjectCollectionBase)((UltraGridBase)ULGRecipeItems).Rows[m].ChildBands[0].Rows).Count; n++)
						{
							((UltraGridBase)ULGRecipeItems).Rows[m].ChildBands[0].Rows[n].Cells["RecipeDetailID"].Value = -1;
							((UltraGridBase)ULGRecipeItems).Rows[m].ChildBands[0].Rows[n].Cells["BranchID"].Value = ((UltraGridBase)ULGRecipeItems).Rows[m].Cells["BranchID"].Value.ToString();
							((UltraGridBase)ULGRecipeItems).Rows[m].ChildBands[0].Rows[n].Cells["ItemID"].Value = num;
						}
					}
					else
					{
						((UltraGridBase)ULGRecipeItems).Rows[m].Cells["BranchID"].Value = DBNull.Value;
						((UltraGridBase)ULGRecipeItems).Rows[m].Cells["ItemID"].Value = num;
					}
				}
				if (((UltraGridBase)ULGRecipeItems).DataSource.GetType().Name.Equals("DataTable"))
				{
					RecipesDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGRecipeItems).DataSource, GlobalVariables.UserID, IsFromServer: true);
				}
				else
				{
					RecipesDetails.Insert_UpdateByTable(((DataSet)((UltraGridBase)ULGRecipeItems).DataSource).Tables["dtRecipeItems"], GlobalVariables.UserID, IsFromServer: true);
				}
			}
			if (dtItemsAccessories != null)
			{
				dtItemsAccessories.AcceptChanges();
				if (dtItemsAccessories != null && dtItemsAccessories.Rows.Count > 0)
				{
					for (int num3 = 0; num3 < dtItemsAccessories.Rows.Count; num3++)
					{
						dtItemsAccessories.Rows[num3]["ItemID"] = num;
					}
					ItemsAccessories.Insert_UpdateByTable(dtItemsAccessories, GlobalVariables.UserID, IsFromServer: true);
				}
			}
			if (dtItemsAdditionals != null)
			{
				dtItemsAdditionals.AcceptChanges();
				if (dtItemsAdditionals != null && dtItemsAdditionals.Rows.Count > 0)
				{
					for (int num4 = 0; num4 < dtItemsAdditionals.Rows.Count; num4++)
					{
						dtItemsAdditionals.Rows[num4]["ItemID"] = num;
					}
					ItemsAdditionals.Insert_UpdateByTable(dtItemsAdditionals, GlobalVariables.UserID, IsFromServer: true);
				}
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
			Items.Insert_Update(((KeyedSubObjectBase)SelectedNode).Key, GetCode(), ((Control)(object)txtName).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? "Null" : ((Control)(object)txtNameEn).Text.Trim(), (SelectedNode.Parent == null) ? "Null" : ((KeyedSubObjectBase)SelectedNode.Parent).Key, ((bool)((DataRow)((SubObjectBase)SelectedNode).Tag)[IsMainCol]) ? "1" : "0", NodeLevel.ToString(), (((Control)(object)txtBarCode).Text.Trim() == "") ? GetCode() : ((Control)(object)txtBarCode).Text, (rbIsService.Checked || cboUnit.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUnit).Value.ToString(), (rbIsService.Checked || cboUnitGroup.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUnitGroup).Value.ToString(), rbStockLevelsForAllBranches.Checked ? "1" : "0", (((Control)(object)txtNotes).Text.Trim() == "") ? "Null" : ((Control)(object)txtNotes).Text, (!rbIsService.Checked && ((UltraToggleEditorBase)chkIsProductionItem).Checked) ? "1" : "0", ((UltraToggleEditorBase)chkIsActive).Checked ? "1" : "0", ((UltraToggleEditorBase)chkHideFromReports).Checked ? "1" : "0", rbIsItem.Checked ? "1" : "0", rbIsService.Checked ? "1" : "0", rbIsRecipe.Checked ? "1" : "0", (!rbIsService.Checked && ((UltraToggleEditorBase)chkIsDirectItem).Checked) ? "1" : "0", (!rbIsService.Checked && ((UltraToggleEditorBase)chkIsSalesItem).Checked) ? "1" : "0", (!rbIsService.Checked || cboServiceAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboServiceAccount).Value.ToString(), ((UltraToggleEditorBase)chkIsUnitPrice).Checked ? "1" : "0", rbPriceForAllBranches.Checked ? "1" : "0", rbRecipeForAllBranchs.Checked ? "1" : "0", (!rbIsService.Checked && ((UltraToggleEditorBase)chkEnforceBatchNo).Checked) ? "1" : "0", (rbIsService.Checked || cboStores.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboStores).Value.ToString(), (rbIsService.Checked || ((Control)(object)txtValidityDays).Text.Trim() == "") ? "Null" : ((Control)(object)txtValidityDays).Text, (cboGrowthTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboGrowthTax).Value.ToString(), (cboTableTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTableTax).Value.ToString(), (cboTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTax).Value.ToString(), (cboSalesAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesAccount).Value.ToString(), (cboSalesReturnsAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesReturnsAccount).Value.ToString(), (cboCostOfSalesAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCostOfSalesAccount).Value.ToString(), (cboPurchaseAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPurchaseAccount).Value.ToString(), (cboPurchaseReturnsAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPurchaseReturnsAccount).Value.ToString(), (cboDepartmentIssueAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDepartmentIssueAccount).Value.ToString(), (cboCostCenter.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCostCenter).Value.ToString(), (((Control)(object)txtSph).Text.Trim() == "") ? "0" : ((Control)(object)txtSph).Text, (((Control)(object)txtCyl).Text.Trim() == "") ? "0" : ((Control)(object)txtCyl).Text, (((Control)(object)txtPrepareTime).Text.Trim() == "") ? "Null" : ((Control)(object)txtPrepareTime).Text, ((UltraToggleEditorBase)chkCanModSalesPrice).Checked ? "1" : "0", ((UltraToggleEditorBase)chkCanModPurchasePrice).Checked ? "1" : "0", ((UltraToggleEditorBase)chkUsePOSNumPad).Checked ? "1" : "0", (((Control)(object)cboPrinterName).Text == "") ? "Null" : ((Control)(object)cboPrinterName).Text.ToString(), (cboTypes.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTypes).Value.ToString(), (cboAgeGroup.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAgeGroup).Value.ToString(), (cboClassification1.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClassification1).Value.ToString(), (cboClassification2.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClassification2).Value.ToString(), (cboClassification3.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClassification3).Value.ToString(), (cboClassification4.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClassification4).Value.ToString(), (cboBrand.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBrand).Value.ToString(), (cboSeason.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSeason).Value.ToString(), (cboCountry.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCountry).Value.ToString(), (cboPriceCategory.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPriceCategory).Value.ToString(), (cboMaterial1.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboMaterial1).Value.ToString(), (cboMaterial2.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboMaterial2).Value.ToString(), (cboShape.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboShape).Value.ToString(), (cboModel.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboModel).Value.ToString(), (cboColorCategory.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboColorCategory).Value.ToString(), (cboColor.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboColor).Value.ToString(), (cboSizeCategory.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSizeCategory).Value.ToString(), (cboSize.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSize).Value.ToString(), (cboSuppliers.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSuppliers).Value.ToString(), (((Control)(object)txtSupplierItemCode).Text.Trim() == "") ? "Null" : ((Control)(object)txtSupplierItemCode).Text, (((Control)(object)txtNetWeight).Text.Trim() == "") ? "0" : ((Control)(object)txtNetWeight).Text, (((Control)(object)txtGrowthWeight).Text.Trim() == "") ? "0" : ((Control)(object)txtGrowthWeight).Text, (((Control)(object)txtVolume).Text.Trim() == "") ? "0" : ((Control)(object)txtVolume).Text, (((Control)(object)txtAccessoriesCount).Text.Trim() == "") ? "0" : ((Control)(object)txtAccessoriesCount).Text, (cboEINVItemNameType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboEINVItemNameType).Value.ToString(), (((Control)(object)txtEINVItemNameCode).Text.Trim() == "") ? "Null" : ((Control)(object)txtEINVItemNameCode).Text, (cboEINVItemGPC.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboEINVItemGPC).Value.ToString(), "0", GlobalVariables.CurrentBranchID.ToString(), GlobalVariables.UserID, IsFromServer: true);
			if (picItem.Image != null)
			{
				Items.PicUpdate(((KeyedSubObjectBase)SelectedNode).Key, GlobalFunctions.ImageToBinary((Image)picItem.Image), IsFromServer: true);
			}
			if (((UltraToggleEditorBase)chkIsUnitPrice).Checked)
			{
				ItemsPrices.DeleteByItemID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
				ItemsUnitsPrices.DeleteByItemID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
				ItemsUnits.DeleteByItemID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
				dtItemsUnits.AcceptChanges();
				dtItemsUnitsPrices.AcceptChanges();
				((UltraGridBase)ULGPrices).UpdateData();
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGPrices).Rows).Count; i++)
				{
					int num = ItemsUnits.Insert_Update("-1", ((KeyedSubObjectBase)SelectedNode).Key, ((UltraGridBase)ULGPrices).Rows[i].Cells["ItemUnitBarcode"].Value.ToString(), (((UltraGridBase)ULGPrices).Rows[i].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGPrices).Rows[i].Cells["UnitID"].Value.ToString(), "0", GlobalVariables.CurrentBranchID.ToString(), GlobalVariables.UserID, IsFromServer: true);
					for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGPrices).Rows[i].ChildBands[0].Rows).Count; j++)
					{
						ItemsUnitsPrices.Insert_Update("-1", num.ToString(), ((KeyedSubObjectBase)SelectedNode).Key, ((UltraGridBase)ULGPrices).Rows[i].ChildBands[0].Rows[j].Cells["PriceTypeID"].Value.ToString(), ((UltraGridBase)ULGPrices).Rows[i].ChildBands[0].Rows[j].Cells["Price"].Value.ToString(), "0", rbPriceForAllBranches.Checked ? "Null" : ((UltraGridBase)ULGPrices).Rows[i].ChildBands[0].Rows[j].Cells["BranchID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
					}
				}
			}
			else
			{
				ItemsUnitsPrices.DeleteByItemID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
				ItemsUnits.DeleteByItemID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
				dtItemPrices.AcceptChanges();
				((UltraGridBase)ULGPrices).UpdateData();
				string text = ",";
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGPrices).Rows).Count; k++)
				{
					text = text + ((UltraGridBase)ULGPrices).Rows[k].Cells["ItemPriceID"].Value.ToString() + ",";
				}
				Main.SyncDeleteForUpdate("SC_ItemsPrices", "ItemID", ((KeyedSubObjectBase)SelectedNode).Key, "ItemPriceID", text, IsFromServer: true);
				for (int l = 0; l < dtItemPrices.Rows.Count; l++)
				{
					ItemsPrices.Insert_Update(dtItemPrices.Rows[l]["ItemPriceID"].ToString(), ((KeyedSubObjectBase)SelectedNode).Key, dtItemPrices.Rows[l]["PriceTypeID"].ToString(), dtItemPrices.Rows[l]["Price"].ToString(), "0", rbPriceForAllBranches.Checked ? "Null" : dtItemPrices.Rows[l]["BranchID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
				}
			}
			dtItemStockLevels.AcceptChanges();
			string text2 = ",";
			for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGLevels).Rows).Count; m++)
			{
				text2 = text2 + ((UltraGridBase)ULGLevels).Rows[m].Cells["ItemStockLevels"].Value.ToString() + ",";
			}
			Main.SyncDeleteForUpdate("SC_ItemsStockLevels", "ItemID", ((KeyedSubObjectBase)SelectedNode).Key, "ItemStockLevels", text2, IsFromServer: true);
			for (int n = 0; n < dtItemStockLevels.Rows.Count; n++)
			{
				ItemsStockLevels.Insert_Update(dtItemStockLevels.Rows[n]["ItemStockLevels"].ToString(), ((KeyedSubObjectBase)SelectedNode).Key, dtItemStockLevels.Rows[n]["MinLevel"].ToString(), dtItemStockLevels.Rows[n]["ReOrderLevel"].ToString(), dtItemStockLevels.Rows[n]["MaxLevel"].ToString(), "0", rbStockLevelsForAllBranches.Checked ? "Null" : dtItemStockLevels.Rows[n]["BranchID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			}
			RecipesDetails.DeleteByItemID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			dtRecipeItems.AcceptChanges();
			((UltraGridBase)ULGRecipeItems).UpdateData();
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGRecipeItems).Rows).Count > 0)
			{
				for (int num2 = 0; num2 < ((DisposableObjectCollectionBase)((UltraGridBase)ULGRecipeItems).Rows).Count; num2++)
				{
					if (((UltraGridBase)ULGRecipeItems).Rows[num2].ChildBands != null)
					{
						for (int num3 = 0; num3 < ((DisposableObjectCollectionBase)((UltraGridBase)ULGRecipeItems).Rows[num2].ChildBands[0].Rows).Count; num3++)
						{
							((UltraGridBase)ULGRecipeItems).Rows[num2].ChildBands[0].Rows[num3].Cells["RecipeDetailID"].Value = -1;
							((UltraGridBase)ULGRecipeItems).Rows[num2].ChildBands[0].Rows[num3].Cells["BranchID"].Value = ((UltraGridBase)ULGRecipeItems).Rows[num2].Cells["BranchID"].Value.ToString();
							((UltraGridBase)ULGRecipeItems).Rows[num2].ChildBands[0].Rows[num3].Cells["ItemID"].Value = ((KeyedSubObjectBase)SelectedNode).Key;
						}
					}
					else
					{
						((UltraGridBase)ULGRecipeItems).Rows[num2].Cells["RecipeDetailID"].Value = -1;
						((UltraGridBase)ULGRecipeItems).Rows[num2].Cells["BranchID"].Value = DBNull.Value;
						((UltraGridBase)ULGRecipeItems).Rows[num2].Cells["ItemID"].Value = ((KeyedSubObjectBase)SelectedNode).Key;
					}
				}
				if (((UltraGridBase)ULGRecipeItems).DataSource.GetType().Name.Equals("DataTable"))
				{
					RecipesDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGRecipeItems).DataSource, GlobalVariables.UserID, IsFromServer: true);
				}
				else
				{
					RecipesDetails.Insert_UpdateByTable(((DataSet)((UltraGridBase)ULGRecipeItems).DataSource).Tables["dtRecipeItems"], GlobalVariables.UserID, IsFromServer: true);
				}
			}
			ItemsAccessories.DeleteByItemID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			if (dtItemsAccessories != null)
			{
				dtItemsAccessories.AcceptChanges();
				if (dtItemsAccessories.Rows.Count > 0)
				{
					for (int num4 = 0; num4 < dtItemsAccessories.Rows.Count; num4++)
					{
						dtItemsAccessories.Rows[num4]["ItemAccessoriesID"] = -1;
						dtItemsAccessories.Rows[num4]["ItemID"] = ((KeyedSubObjectBase)SelectedNode).Key;
						dtItemsAccessories.Rows[num4]["BranchID"] = GlobalVariables.CurrentBranchID;
					}
					ItemsAccessories.Insert_UpdateByTable(dtItemsAccessories, GlobalVariables.UserID, IsFromServer: true);
				}
			}
			ItemsAdditionals.DeleteByItemID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			if (dtItemsAdditionals != null)
			{
				dtItemsAdditionals.AcceptChanges();
				if (dtItemsAdditionals.Rows.Count > 0)
				{
					for (int num5 = 0; num5 < dtItemsAdditionals.Rows.Count; num5++)
					{
						dtItemsAdditionals.Rows[num5]["ItemAdditionalID"] = -1;
						dtItemsAdditionals.Rows[num5]["ItemID"] = ((KeyedSubObjectBase)SelectedNode).Key;
						dtItemsAdditionals.Rows[num5]["BranchID"] = GlobalVariables.CurrentBranchID;
					}
					ItemsAdditionals.Insert_UpdateByTable(dtItemsAdditionals, GlobalVariables.UserID, IsFromServer: true);
				}
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

	public override void TreeDeleteData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			ItemsAdditionals.DeleteByItemID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			ItemsAccessories.DeleteByItemID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			RecipesDetails.DeleteByItemID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			ItemsStockLevels.DeleteByItemID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			ItemsUnitsPrices.DeleteByItemID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			ItemsUnits.DeleteByItemID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			ItemsPrices.DeleteByItemID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			ItemsBatches.DeleteByItemID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			Items.Delete(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override bool HasTransactionValidation()
	{
		string text = Items.SelectRelations(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true).Rows[0]["Relations"].ToString().Replace("-", "\n");
		if (text != "")
		{
			GlobalVariables.InformationMB.Show(text, text);
			return true;
		}
		return base.HasTransactionValidation();
	}

	public override void TreeSearch()
	{
		int num = SearchFunctions.Items("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", IsFromServer: true);
		if (num != 0)
		{
			treeChart.CollapseAll();
			treeChart.ActiveNode = treeChart.GetNodeByKey(num.ToString());
			treeChart.GetNodeByKey(num.ToString()).Selected = true;
		}
	}

	private void btnPic_Click(object sender, EventArgs e)
	{
		if (ofdItemPic.ShowDialog() == DialogResult.OK)
		{
			picItem.Image = null;
			picItem.Image = ImageFunctions.ScaleImage(Image.FromFile(ofdItemPic.FileName), 100);
			picItem.ScaleImage = (ScaleImage)2;
		}
	}

	private void rbIsItem_CheckedChanged(object sender, EventArgs e)
	{
		((UltraTabControlBase)tabItemType).Tabs["Item"].Visible = rbIsItem.Checked;
		((UltraTabControlBase)tabItemType).Tabs["Levels"].Visible = rbIsItem.Checked;
		((UltraTabControlBase)tabItemType).Tabs["Classification"].Visible = rbIsItem.Checked;
		((UltraTabControlBase)tabItemType).Tabs["Accounts"].Visible = rbIsItem.Checked;
		if (rbIsItem.Checked)
		{
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Item"];
		}
	}

	private void rbIsService_CheckedChanged(object sender, EventArgs e)
	{
		((UltraTabControlBase)tabItemType).Tabs["Service"].Visible = rbIsService.Checked;
		if (rbIsService.Checked)
		{
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Service"];
		}
	}

	private void rbIsRecipe_CheckedChanged(object sender, EventArgs e)
	{
		((UltraTabControlBase)tabItemType).Tabs["Item"].Visible = rbIsRecipe.Checked;
		((UltraTabControlBase)tabItemType).Tabs["Recipe"].Visible = rbIsRecipe.Checked;
		((UltraTabControlBase)tabItemType).Tabs["Levels"].Visible = rbIsRecipe.Checked;
		((UltraTabControlBase)tabItemType).Tabs["Classification"].Visible = rbIsRecipe.Checked;
		((UltraTabControlBase)tabItemType).Tabs["Accounts"].Visible = rbIsRecipe.Checked;
		if (rbIsRecipe.Checked)
		{
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Item"];
		}
	}

	private void cboUnitGroup_ValueChanged(object sender, EventArgs e)
	{
		if (cboUnitGroup.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtUnits);
			dataView.RowFilter = "UnitTypeID=" + ((TextEditorControlBase)cboUnitGroup).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboUnit.DataSource = dataView;
			cboUnit.DisplayMember = "UnitName";
			cboUnit.ValueMember = "UnitID";
			dtItemsUnitsPrices.Rows.Clear();
			dtItemsUnits.Rows.Clear();
			vlItemsUnits = getUnitValueList(((TextEditorControlBase)cboUnitGroup).Value.ToString());
			InitGridPrices();
		}
	}

	private void rbPriceForAllBranches_CheckedChanged(object sender, EventArgs e)
	{
		FillGridPrices();
	}

	private void rbStockLevelsForAllBranches_CheckedChanged(object sender, EventArgs e)
	{
		FillGridStockLevels();
	}

	private void rbRecipeForAllBranchs_CheckedChanged(object sender, EventArgs e)
	{
		FillRecipeItems();
	}

	private void chkIsUnitPrice_CheckedChanged(object sender, EventArgs e)
	{
		FillGridPrices();
	}

	private void ULGPrices_CellListSelect(object sender, CellEventArgs e)
	{
		if (!(((KeyedSubObjectBase)e.Cell.Column).Key == "UnitID"))
		{
			return;
		}
		int num;
		if (int.Parse(((UltraGridBase)ULGPrices).ActiveRow.Cells["ItemUnitID"].Value.ToString()) != -1)
		{
			num = int.Parse(((UltraGridBase)ULGPrices).ActiveRow.Cells["ItemUnitID"].Value.ToString());
			DataRow[] array = dtItemsUnitsPrices.Select("ItemUnitID = " + num);
			DataRow[] array2 = array;
			foreach (DataRow dataRow in array2)
			{
				dataRow.Delete();
			}
		}
		else
		{
			num = ++newID;
			((UltraGridBase)ULGPrices).ActiveRow.Cells["ItemUnitID"].Value = num;
		}
		((UltraGridBase)ULGPrices).UpdateData();
		if (rbPriceForAllBranches.Checked)
		{
			for (int j = 0; j < dtPricesTypes.Rows.Count; j++)
			{
				DataRow dataRow2 = dtItemsUnitsPrices.NewRow();
				dataRow2["ItemUnitPriceID"] = -1;
				dataRow2["ItemUnitID"] = num;
				dataRow2["ItemID"] = -1;
				dataRow2["PriceTypeID"] = dtPricesTypes.Rows[j]["PriceTypeID"];
				dataRow2["Price"] = 0;
				dataRow2["Deleted"] = false;
				dataRow2["BranchID"] = DBNull.Value;
				dtItemsUnitsPrices.Rows.Add(dataRow2);
			}
			return;
		}
		for (int k = 0; k < dtBranches.Rows.Count; k++)
		{
			for (int l = 0; l < dtPricesTypes.Rows.Count; l++)
			{
				DataRow dataRow3 = dtItemsUnitsPrices.NewRow();
				dataRow3["ItemUnitPriceID"] = -1;
				dataRow3["ItemUnitID"] = num;
				dataRow3["ItemID"] = -1;
				dataRow3["PriceTypeID"] = dtPricesTypes.Rows[l]["PriceTypeID"];
				dataRow3["Price"] = 0;
				dataRow3["Deleted"] = false;
				dataRow3["BranchID"] = dtBranches.Rows[k]["BranchID"];
				dtItemsUnitsPrices.Rows.Add(dataRow3);
			}
		}
	}

	private void FillGridPrices()
	{
		if (((UltraToggleEditorBase)chkIsUnitPrice).Checked)
		{
			dtItemsUnitsPrices.Rows.Clear();
			dtItemsUnits.Rows.Clear();
		}
		else
		{
			dtItemPrices.Rows.Clear();
			if (rbPriceForAllBranches.Checked)
			{
				for (int i = 0; i < dtPricesTypes.Rows.Count; i++)
				{
					DataRow dataRow = dtItemPrices.NewRow();
					dataRow["ItemPriceID"] = -1;
					dataRow["ItemID"] = (Adding ? "-1" : ((KeyedSubObjectBase)SelectedNode).Key);
					dataRow["PriceTypeID"] = dtPricesTypes.Rows[i]["PriceTypeID"];
					dataRow["Price"] = 0;
					dataRow["Deleted"] = false;
					dataRow["BranchID"] = DBNull.Value;
					dtItemPrices.Rows.Add(dataRow);
				}
			}
			else
			{
				for (int j = 0; j < dtBranches.Rows.Count; j++)
				{
					for (int k = 0; k < dtPricesTypes.Rows.Count; k++)
					{
						DataRow dataRow2 = dtItemPrices.NewRow();
						dataRow2["ItemPriceID"] = -1;
						dataRow2["ItemID"] = (Adding ? "-1" : ((KeyedSubObjectBase)SelectedNode).Key);
						dataRow2["PriceTypeID"] = dtPricesTypes.Rows[k]["PriceTypeID"];
						dataRow2["Price"] = 0;
						dataRow2["Deleted"] = false;
						dataRow2["BranchID"] = dtBranches.Rows[j]["BranchID"];
						dtItemPrices.Rows.Add(dataRow2);
					}
				}
			}
		}
		InitGridPrices();
	}

	private void InitGridPrices()
	{
		if (((UltraToggleEditorBase)chkIsUnitPrice).Checked)
		{
			((UltraGridBase)ULGPrices).DataSource = ds;
			GlobalFunctions.PrepareGrid(ULGPrices);
			((UltraGridBase)ULGPrices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
			((UltraGridBase)ULGPrices).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
			((UltraGridBase)ULGPrices).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)1;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[1].Override.RowSelectors = (DefaultableBoolean)1;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[1].Override.AllowAddNew = (AllowAddNew)2;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[1].Override.AllowDelete = (DefaultableBoolean)2;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["ItemUnitID"].DefaultCellValue = -1;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["ItemUnitBarcode"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.35) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.35);
			((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["ItemUnitBarcode"].Header).Caption = (GlobalVariables.IsArabic ? "باركود الوحدة" : "Item Unit Barcode");
			((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["ItemUnitBarcode"].Hidden = false;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlItemsUnits;
			((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[1].Columns["PriceTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع السعر" : "Price Type");
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[1].Columns["PriceTypeID"].Hidden = false;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[1].Columns["PriceTypeID"].ValueList = (IValueList)(object)vlPricesTypes;
			((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[1].Columns["Price"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[1].Columns["Price"].Hidden = false;
			if (rbPriceForAllBranches.Checked)
			{
				((UltraGridBase)ULGPrices).DisplayLayout.Bands[1].Columns["PriceTypeID"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.4) - GlobalVariables.ScrollWidth;
				((UltraGridBase)ULGPrices).DisplayLayout.Bands[1].Columns["Price"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.4);
				((UltraGridBase)ULGPrices).DisplayLayout.Bands[1].Columns["BranchID"].Hidden = true;
				return;
			}
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[1].Columns["PriceTypeID"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.2) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[1].Columns["Price"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.2);
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[1].Columns["BranchID"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.2);
			((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[1].Columns["BranchID"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[1].Columns["BranchID"].Hidden = false;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[1].Columns["BranchID"].ValueList = (IValueList)(object)vlBranches;
		}
		else
		{
			((UltraGridBase)ULGPrices).DataSource = dtItemPrices;
			GlobalFunctions.PrepareGrid(ULGPrices);
			((UltraGridBase)ULGPrices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
			((UltraGridBase)ULGPrices).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
			((UltraGridBase)ULGPrices).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["ItemPriceID"].DefaultCellValue = -1;
			((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع السعر" : "Price Type");
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Hidden = false;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].ValueList = (IValueList)(object)vlPricesTypes;
			((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Hidden = false;
			if (rbPriceForAllBranches.Checked)
			{
				((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.5) - GlobalVariables.ScrollWidth;
				((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.5);
				((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = true;
				return;
			}
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.4) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.3);
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.3);
			((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = false;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].ValueList = (IValueList)(object)vlBranches;
		}
	}

	private void FillGridStockLevels()
	{
		dtItemStockLevels.Rows.Clear();
		if (rbStockLevelsForAllBranches.Checked)
		{
			DataRow dataRow = dtItemStockLevels.NewRow();
			dataRow["ItemStockLevels"] = -1;
			dataRow["ItemID"] = -1;
			dataRow["MinLevel"] = 0;
			dataRow["ReOrderLevel"] = 0;
			dataRow["MaxLevel"] = 0;
			dataRow["Deleted"] = false;
			dataRow["BranchID"] = DBNull.Value;
			dtItemStockLevels.Rows.Add(dataRow);
		}
		else
		{
			for (int i = 0; i < dtBranches.Rows.Count; i++)
			{
				DataRow dataRow2 = dtItemStockLevels.NewRow();
				dataRow2["ItemStockLevels"] = -1;
				dataRow2["ItemID"] = -1;
				dataRow2["MinLevel"] = 0;
				dataRow2["ReOrderLevel"] = 0;
				dataRow2["MaxLevel"] = 0;
				dataRow2["Deleted"] = false;
				dataRow2["BranchID"] = dtBranches.Rows[i]["BranchID"];
				dtItemStockLevels.Rows.Add(dataRow2);
			}
		}
		InitGridStockLevels();
	}

	private void InitGridStockLevels()
	{
		((UltraGridBase)ULGLevels).DataSource = dtItemStockLevels;
		GlobalFunctions.PrepareGrid(ULGLevels);
		((UltraGridBase)ULGLevels).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGLevels).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGLevels).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["ItemStockLevels"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MinLevel"].Header).Caption = (GlobalVariables.IsArabic ? "الأدني" : "Min");
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MinLevel"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["ReOrderLevel"].Header).Caption = (GlobalVariables.IsArabic ? "اعادة الطلب" : "Reorder");
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["ReOrderLevel"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MaxLevel"].Header).Caption = (GlobalVariables.IsArabic ? "الأقصي" : "Max");
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MaxLevel"].Hidden = false;
		if (rbStockLevelsForAllBranches.Checked)
		{
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MinLevel"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.33) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["ReOrderLevel"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.33);
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MaxLevel"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.34);
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = true;
		}
		else
		{
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MinLevel"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.25) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["ReOrderLevel"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.25);
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MaxLevel"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.25);
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["BranchID"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.25);
			((HeaderBase)((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["BranchID"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = false;
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["BranchID"].ValueList = (IValueList)(object)vlBranches2;
		}
	}

	private void FillRecipeItems()
	{
		dtRecipeItems.Rows.Clear();
		if (rbRecipeForAllBranchs.Checked)
		{
			((UltraGridBase)ULGRecipeItems).DataSource = dtRecipeItems;
		}
		else
		{
			dsRecipe = new DataSet();
			dsRecipe.Tables.Add(dtBranches.Copy());
			dsRecipe.Tables.Add(dtRecipeItems.Copy());
			dsRecipe.Tables[0].TableName = "dtBranches";
			dsRecipe.Tables[1].TableName = "dtRecipeItems";
			dsRecipe.Relations.Add(dsRecipe.Tables[0].Columns["BranchID"], dsRecipe.Tables[1].Columns["BranchID"]);
			((UltraGridBase)ULGRecipeItems).DataSource = dsRecipe;
		}
		InitGridRecipeItems();
	}

	private void InitGridRecipeItems()
	{
		GlobalFunctions.PrepareGrid(ULGRecipeItems);
		object type = ((UltraGridBase)ULGRecipeItems).DataSource.GetType();
		if (((UltraGridBase)ULGRecipeItems).DataSource.GetType().Name.Equals("DataTable"))
		{
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["RecipeDetailID"].DefaultCellValue = -1;
			((HeaderBase)((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["RecipeItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item Name");
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["RecipeItemID"].Hidden = false;
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["RecipeItemID"].Width = (int)((double)((Control)(object)ULGRecipeItems).Width * 0.4) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["RecipeItemID"].ValueList = (IValueList)(object)vlItems;
			((HeaderBase)((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكميه" : "Qty");
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGRecipeItems).Width * 0.2);
			((HeaderBase)((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحده" : "Unit");
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGRecipeItems).Width * 0.2);
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
			((HeaderBase)((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGRecipeItems).Width * 0.2);
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 0;
		}
		else
		{
			((HeaderBase)((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["BranchID"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch Name");
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = false;
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["BranchID"].Width = (int)((double)((Control)(object)ULGRecipeItems).Width * 0.1) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["BranchID"].ValueList = (IValueList)(object)vlBranches3;
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[1].Override.RowSelectors = (DefaultableBoolean)1;
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[1].Override.AllowAddNew = (AllowAddNew)6;
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[1].Columns["RecipeDetailID"].DefaultCellValue = -1;
			((HeaderBase)((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[1].Columns["RecipeItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item Name");
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[1].Columns["RecipeItemID"].Hidden = false;
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[1].Columns["RecipeItemID"].Width = (int)((double)((Control)(object)ULGRecipeItems).Width * 0.3) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[1].Columns["RecipeItemID"].ValueList = (IValueList)(object)vlItems;
			((HeaderBase)((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[1].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكميه" : "Qty");
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[1].Columns["Qty"].Hidden = false;
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[1].Columns["Qty"].Width = (int)((double)((Control)(object)ULGRecipeItems).Width * 0.2);
			((HeaderBase)((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[1].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحده" : "Unit");
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[1].Columns["UnitID"].Hidden = false;
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[1].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGRecipeItems).Width * 0.2);
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[1].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
			((HeaderBase)((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[1].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[1].Columns["Notes"].Hidden = false;
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[1].Columns["Notes"].Width = (int)((double)((Control)(object)ULGRecipeItems).Width * 0.2);
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[1].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
			((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[1].Columns["Qty"].DefaultCellValue = 0;
		}
	}

	private void InitGridAccessoriesItems()
	{
		((UltraGridBase)ULGAccessoriesItems).DataSource = dtItemsAccessories;
		GlobalFunctions.PrepareGrid(ULGAccessoriesItems);
		((UltraGridBase)ULGAccessoriesItems).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGAccessoriesItems).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGAccessoriesItems).DisplayLayout.Bands[0].Columns["ItemAccessoriesID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGAccessoriesItems).DisplayLayout.Bands[0].Columns["AccessoriesItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item Name");
		((UltraGridBase)ULGAccessoriesItems).DisplayLayout.Bands[0].Columns["AccessoriesItemID"].Hidden = false;
		((UltraGridBase)ULGAccessoriesItems).DisplayLayout.Bands[0].Columns["AccessoriesItemID"].Width = (int)((double)((Control)(object)ULGAccessoriesItems).Width * 0.6) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGAccessoriesItems).DisplayLayout.Bands[0].Columns["AccessoriesItemID"].ValueList = (IValueList)(object)vlAccessoriesItems;
		((HeaderBase)((UltraGridBase)ULGAccessoriesItems).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكميه" : "Qty");
		((UltraGridBase)ULGAccessoriesItems).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGAccessoriesItems).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGAccessoriesItems).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGAccessoriesItems).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحده" : "Unit");
		((UltraGridBase)ULGAccessoriesItems).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGAccessoriesItems).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGAccessoriesItems).Width * 0.2);
		((UltraGridBase)ULGAccessoriesItems).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlAccessoriesUnits;
		((UltraGridBase)ULGAccessoriesItems).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
	}

	private void InitGridAdditionalsItems()
	{
		((UltraGridBase)ULGAdditionalsItems).DataSource = dtItemsAdditionals;
		GlobalFunctions.PrepareGrid(ULGAdditionalsItems);
		((UltraGridBase)ULGAdditionalsItems).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGAdditionalsItems).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGAdditionalsItems).DisplayLayout.Bands[0].Columns["ItemAdditionalID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGAdditionalsItems).DisplayLayout.Bands[0].Columns["AdditionalItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item Name");
		((UltraGridBase)ULGAdditionalsItems).DisplayLayout.Bands[0].Columns["AdditionalItemID"].Hidden = false;
		((UltraGridBase)ULGAdditionalsItems).DisplayLayout.Bands[0].Columns["AdditionalItemID"].Width = (int)((double)((Control)(object)ULGAdditionalsItems).Width * 0.6) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGAdditionalsItems).DisplayLayout.Bands[0].Columns["AdditionalItemID"].ValueList = (IValueList)(object)vlAdditionalsItems;
		((HeaderBase)((UltraGridBase)ULGAdditionalsItems).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكميه" : "Qty");
		((UltraGridBase)ULGAdditionalsItems).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGAdditionalsItems).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGAdditionalsItems).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGAdditionalsItems).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحده" : "Unit");
		((UltraGridBase)ULGAdditionalsItems).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGAdditionalsItems).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGAdditionalsItems).Width * 0.2);
		((UltraGridBase)ULGAdditionalsItems).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlAdditionalsUnits;
		((UltraGridBase)ULGAdditionalsItems).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
	}

	public override void btnRefreshDataClick()
	{
		base.btnRefreshDataClick();
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboServiceAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboSalesAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboSalesReturnsAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboCostOfSalesAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboPurchaseAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboPurchaseReturnsAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboDepartmentIssueAccount, dtAccounts, "AccountID", "Name");
		if (UseElectronicInvoice)
		{
			dtEINVItemsGPC = ItemsGPC.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboEINVItemGPC, dtEINVItemsGPC, "EINVItemGPCID", "EINVItemGPCName");
			dtEINVItemsNamesTypes = ItemsNamesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboEINVItemNameType, dtEINVItemsNamesTypes, "EINVItemNameTypeID", "EINVItemName");
		}
		dtCostCenters = CostCenters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCostCenter, dtCostCenters, "CostCenterID", "Name");
		dtUnitGroup = UnitsTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		cboUnitGroup.DataSource = dtUnitGroup;
		cboUnitGroup.DisplayMember = "UnitTypeName";
		cboUnitGroup.ValueMember = "UnitTypeID";
		dtUnits = BusinessLayer.StockControl.Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlUnits.ValueListItems.Clear();
		vlItemsUnits.ValueListItems.Clear();
		vlAccessoriesUnits.ValueListItems.Clear();
		vlAdditionalsUnits.ValueListItems.Clear();
		for (int i = 0; i < dtUnits.Rows.Count; i++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[i]["UnitID"], dtUnits.Rows[i]["UnitName"].ToString());
			vlAccessoriesUnits.ValueListItems.Add(dtUnits.Rows[i]["UnitID"], dtUnits.Rows[i]["UnitName"].ToString());
			vlAdditionalsUnits.ValueListItems.Add(dtUnits.Rows[i]["UnitID"], dtUnits.Rows[i]["UnitName"].ToString());
		}
		if (Adding || Updating)
		{
			vlItemsUnits = getUnitValueList(((TextEditorControlBase)cboUnitGroup).Value.ToString());
			InitGridPrices();
		}
		else
		{
			vlItemsUnits.ValueListItems.Clear();
			for (int j = 0; j < dtUnits.Rows.Count; j++)
			{
				vlItemsUnits.ValueListItems.Add(dtUnits.Rows[j]["UnitID"], dtUnits.Rows[j]["UnitName"].ToString());
			}
		}
		dtTaxes = BusinessLayer.General.Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboTax, dtTaxes, "TaxID", "TaxName");
		GlobalFunctions.FillCombo(cboGrowthTax, dtTaxes, "TaxID", "TaxName");
		GlobalFunctions.FillCombo(cboTableTax, dtTaxes, "TaxID", "TaxName");
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlItems.ValueListItems.Clear();
		vlAccessoriesItems.ValueListItems.Clear();
		vlAdditionalsItems.ValueListItems.Clear();
		for (int k = 0; k < dtItems.Rows.Count; k++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["Name"].ToString());
			vlAccessoriesItems.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["Name"].ToString());
			vlAdditionalsItems.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["Name"].ToString());
		}
		dtStores = Stores.FillCombo("-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		cboStores.DataSource = dtStores;
		cboStores.DisplayMember = "StoreName";
		cboStores.ValueMember = "StoreID";
		dtPricesTypes = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlPricesTypes.ValueListItems.Clear();
		for (int l = 0; l < dtPricesTypes.Rows.Count; l++)
		{
			vlPricesTypes.ValueListItems.Add(dtPricesTypes.Rows[l]["PriceTypeID"], dtPricesTypes.Rows[l]["PriceName"].ToString());
		}
		dtBranches = Branches.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlBranches.ValueListItems.Clear();
		for (int m = 0; m < dtBranches.Rows.Count; m++)
		{
			vlBranches.ValueListItems.Add(dtBranches.Rows[m]["BranchID"], dtBranches.Rows[m][GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn"].ToString());
			vlBranches2.ValueListItems.Add(dtBranches.Rows[m]["BranchID"], dtBranches.Rows[m][GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn"].ToString());
		}
	}

	private void chkEnforceBatchNo_CheckedChanged(object sender, EventArgs e)
	{
		UltraTextEditor obj = txtValidityDays;
		bool visible = (((Control)(object)lblValidityDays).Visible = UsingBatchNoAndValidityPeriod && ((UltraToggleEditorBase)chkEnforceBatchNo).Checked);
		((Control)(object)obj).Visible = visible;
	}

	private void ULGAccessoriesItems_CellListSelect(object sender, CellEventArgs e)
	{
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "AccessoriesItemID")
		{
			((UltraGridBase)ULGAccessoriesItems).ActiveRow.Cells["UnitID"].Value = dtItems.Rows[vlAccessoriesItems.SelectedIndex]["UnitID"];
			string unitTypeID = dtItems.Rows[vlAccessoriesItems.SelectedIndex]["UnitTypeID"].ToString();
			e.Cell.Row.Cells["UnitID"].ValueList = (IValueList)(object)getUnitValueList(unitTypeID);
		}
	}

	private void ULGAdditionalsItems_CellListSelect(object sender, CellEventArgs e)
	{
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "AdditionalItemID")
		{
			((UltraGridBase)ULGAdditionalsItems).ActiveRow.Cells["UnitID"].Value = dtItems.Rows[vlAdditionalsItems.SelectedIndex]["UnitID"];
			string unitTypeID = dtItems.Rows[vlAdditionalsItems.SelectedIndex]["UnitTypeID"].ToString();
			e.Cell.Row.Cells["UnitID"].ValueList = (IValueList)(object)getUnitValueList(unitTypeID);
		}
	}

	private void ULGRecipeItems_CellListSelect(object sender, CellEventArgs e)
	{
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "RecipeItemID")
		{
			((UltraGridBase)ULGRecipeItems).ActiveRow.Cells["UnitID"].Value = dtItems.Rows[vlItems.SelectedIndex]["UnitID"];
			string unitTypeID = dtItems.Rows[vlItems.SelectedIndex]["UnitTypeID"].ToString();
			e.Cell.Row.Cells["UnitID"].ValueList = (IValueList)(object)getUnitValueList(unitTypeID);
		}
	}

	private ValueList getUnitValueList(string UnitTypeID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtUnits.Select("UnitTypeID=" + UnitTypeID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["UnitID"].ToString(), array[i]["UnitName"].ToString());
		}
		return val;
	}

	public override void btnPrintClick()
	{
		string val = "";
		GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_SC_Items_A_nologo.rpt" : "Rep_SC_Items_E_nologo.rpt"));
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@ItemIDs", "-1");
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
	}

	private void btnServiceAccountSearch_Click(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			int num = SearchFunctions.Accounts(IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboServiceAccount).Value = num;
			}
		}
	}

	private void btnSalesAccount_Click(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			int num = SearchFunctions.Accounts(IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboSalesAccount).Value = num;
			}
		}
	}

	private void btnSalesReturnsAccount_Click(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			int num = SearchFunctions.Accounts(IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboSalesReturnsAccount).Value = num;
			}
		}
	}

	private void btnCostOfSalesAccount_Click(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			int num = SearchFunctions.Accounts(IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboCostOfSalesAccount).Value = num;
			}
		}
	}

	private void btnPurchaseAccount_Click(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			int num = SearchFunctions.Accounts(IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboPurchaseAccount).Value = num;
			}
		}
	}

	private void ULGPrices_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void ULGPrices_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGPrices.ActiveCell != null && ((KeyedSubObjectBase)ULGPrices.ActiveCell.Column).Key == "ItemUnitBarcode" && (e.KeyChar == ' ' || e.KeyChar == '+'))
		{
			e.Handled = true;
		}
	}

	private void btnCostCenterSearch_Click(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			int num = SearchFunctions.CostCenter(IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboCostCenter).Value = num;
			}
		}
	}

	private void cboCostCenter_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.CostCenter(IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboCostCenter).Value = num;
			}
		}
	}

	private void cboEINVItemNameType_ValueChanged(object sender, EventArgs e)
	{
		if (cboEINVItemNameType.SelectedIndex == 1)
		{
			((Control)(object)txtEINVItemNameCode).Enabled = false;
			((Control)(object)txtEINVItemNameCode).Text = Items.GetEINVItemNameCode(IsFromServer: true);
		}
		else
		{
			((Control)(object)txtEINVItemNameCode).Enabled = true;
		}
	}

	private void btnEINVExportItems_Click(object sender, EventArgs e)
	{
		try
		{
			string text = "";
			DataTable dataTable = Main.ExecuteQuery_DataTable(" EINV_ItemsExportTOPortal " + (SendingItemsByItemsTypes ? "1" : "0"));
			if (dataTable == null || dataTable.Columns.Count == 0)
			{
				throw new Exception("ExportToExcel: Null or empty input table!\n");
			}
			Microsoft.Office.Interop.Excel.Application application = (Microsoft.Office.Interop.Excel.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
			application.Workbooks.Add(Type.Missing);
			_Worksheet worksheet = (dynamic)application.ActiveSheet;
			worksheet.Name = "DataEntry";
			for (int i = 0; i < dataTable.Columns.Count; i++)
			{
				worksheet.Cells[1, i + 1] = dataTable.Columns[i].ColumnName;
			}
			for (int j = 0; j < dataTable.Rows.Count; j++)
			{
				for (int k = 0; k < dataTable.Columns.Count; k++)
				{
					worksheet.Cells[j + 2, k + 1] = dataTable.Rows[j][k];
				}
			}
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				text = folderBrowserDialog.SelectedPath;
			}
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					worksheet.SaveAs(text + "\\NewCodeBulkTemplate.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
					application.Quit();
					MessageBox.Show("Excel file saved!");
					return;
				}
				catch (Exception ex)
				{
					throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n" + ex.Message);
				}
			}
			application.Visible = true;
		}
		catch (Exception ex2)
		{
			throw new Exception("ExportToExcel: \n" + ex2.Message);
		}
	}

	private void btnPurchaseReturnsAccount_Click(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			int num = SearchFunctions.Accounts(IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboPurchaseReturnsAccount).Value = num;
			}
		}
	}

	private void btnDepartmentIssueAccount_Click(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			int num = SearchFunctions.Accounts(IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboDepartmentIssueAccount).Value = num;
			}
		}
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txt_KeyPress2(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbersNegative(sender, e);
	}

	private void txtInt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void modifyGroupItemsToolStripMenuItem4_Click(object sender, EventArgs e)
	{
		if (SelectedNode != null)
		{
			frmUpdateGroupItems frmUpdateGroupItems2 = new frmUpdateGroupItems(((KeyedSubObjectBase)SelectedNode).Key);
			frmUpdateGroupItems2.ShowDialog();
		}
	}

	private void modifyGroupItemsToolStripMenuItem3_Click(object sender, EventArgs e)
	{
	}

	private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!CanDelete)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
		}
		else if (SelectedNode != null)
		{
			DeleteGroupItems(SelectedNode);
		}
	}

	public bool DeleteGroupItems(UltraTreeNode Node)
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)Node.Nodes).Count; i++)
		{
			if (DeleteGroupItems(Node.Nodes[i]))
			{
				i--;
			}
		}
		SelectedNode = Node;
		if (((DisposableObjectCollectionBase)Node.Nodes).Count > 0)
		{
			GlobalVariables.InformationMB.Show(" لايمكن حذف هذا العنصر لوجود عناصر تحته", "Cannot Delete this Node It Has Sub Nodes");
			return false;
		}
		if (HasTransactionValidation())
		{
			return false;
		}
		RowID = (((SubObjectBase)SelectedNode).Tag as DataRow)[0].ToString();
		DeleteData();
		if (Main.Success)
		{
			return true;
		}
		return false;
	}

	private void ULGLevels_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGLevels.ActiveCell.Column).Key == "BranchID")
		{
			((GridItemBase)((UltraGridBase)ULGLevels).ActiveRow).Selected = true;
		}
	}

	private void ULGPrices_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGPrices.ActiveCell.Column).Key == "PriceTypeID" || ((KeyedSubObjectBase)ULGPrices.ActiveCell.Column).Key == "BranchID")
		{
			((GridItemBase)((UltraGridBase)ULGPrices).ActiveRow).Selected = true;
		}
	}

	private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == ' ' || e.KeyChar == '+')
		{
			e.Handled = true;
		}
	}

	private void changeParentToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (SelectedNode != null)
		{
			frmUpdateParent frmUpdateParent2 = new frmUpdateParent(((KeyedSubObjectBase)SelectedNode).Key, (SelectedNode.Parent != null) ? ((KeyedSubObjectBase)SelectedNode.Parent).Key : "");
			((Control)(object)frmUpdateParent2.lblTitle).Text = (GlobalVariables.IsArabic ? "تغيير المجموعه" : "Update Group");
			frmUpdateParent2.ShowDialog();
			btnRefreshDataClick();
		}
	}

	private void changeClassificationsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (SelectedNode != null)
		{
			frmUpdateItemsClassifications frmUpdateItemsClassifications2 = new frmUpdateItemsClassifications(((KeyedSubObjectBase)SelectedNode).Key);
			((Control)(object)frmUpdateItemsClassifications2.lblTitle).Text = (GlobalVariables.IsArabic ? "تعديل تصنيفات المجموعه " : "Change Group Classifications");
			frmUpdateItemsClassifications2.ShowDialog();
			btnRefreshDataClick();
		}
	}

	private void ULGRecipeItems_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGRecipeItems.ActiveCell.Column).Key == "BranchID")
		{
			((GridItemBase)((UltraGridBase)ULGRecipeItems).ActiveRow).Selected = true;
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
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Expected O, but got Unknown
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Expected O, but got Unknown
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Expected O, but got Unknown
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Expected O, but got Unknown
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Expected O, but got Unknown
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Expected O, but got Unknown
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Expected O, but got Unknown
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Expected O, but got Unknown
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Expected O, but got Unknown
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Expected O, but got Unknown
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Expected O, but got Unknown
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Expected O, but got Unknown
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Expected O, but got Unknown
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Expected O, but got Unknown
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Expected O, but got Unknown
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Expected O, but got Unknown
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Expected O, but got Unknown
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Expected O, but got Unknown
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Expected O, but got Unknown
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Expected O, but got Unknown
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Expected O, but got Unknown
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Expected O, but got Unknown
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Expected O, but got Unknown
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Expected O, but got Unknown
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Expected O, but got Unknown
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Expected O, but got Unknown
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Expected O, but got Unknown
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Expected O, but got Unknown
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Expected O, but got Unknown
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Expected O, but got Unknown
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Expected O, but got Unknown
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Expected O, but got Unknown
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Expected O, but got Unknown
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Expected O, but got Unknown
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Expected O, but got Unknown
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Expected O, but got Unknown
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Expected O, but got Unknown
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Expected O, but got Unknown
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Expected O, but got Unknown
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Expected O, but got Unknown
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Expected O, but got Unknown
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Expected O, but got Unknown
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Expected O, but got Unknown
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Expected O, but got Unknown
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Expected O, but got Unknown
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Expected O, but got Unknown
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Expected O, but got Unknown
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Expected O, but got Unknown
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Expected O, but got Unknown
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Expected O, but got Unknown
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Expected O, but got Unknown
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Expected O, but got Unknown
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Expected O, but got Unknown
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Expected O, but got Unknown
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Expected O, but got Unknown
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Expected O, but got Unknown
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Expected O, but got Unknown
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Expected O, but got Unknown
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Expected O, but got Unknown
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Expected O, but got Unknown
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Expected O, but got Unknown
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Expected O, but got Unknown
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Expected O, but got Unknown
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Expected O, but got Unknown
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Expected O, but got Unknown
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Expected O, but got Unknown
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Expected O, but got Unknown
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Expected O, but got Unknown
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Expected O, but got Unknown
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Expected O, but got Unknown
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Expected O, but got Unknown
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Expected O, but got Unknown
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Expected O, but got Unknown
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Expected O, but got Unknown
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Expected O, but got Unknown
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Expected O, but got Unknown
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Expected O, but got Unknown
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Expected O, but got Unknown
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Expected O, but got Unknown
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Expected O, but got Unknown
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Expected O, but got Unknown
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Expected O, but got Unknown
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Expected O, but got Unknown
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Expected O, but got Unknown
		//IL_0580: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Expected O, but got Unknown
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Expected O, but got Unknown
		//IL_0596: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Expected O, but got Unknown
		//IL_05a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ab: Expected O, but got Unknown
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Expected O, but got Unknown
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d7: Expected O, but got Unknown
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e2: Expected O, but got Unknown
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Expected O, but got Unknown
		//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f8: Expected O, but got Unknown
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Expected O, but got Unknown
		//IL_0604: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Expected O, but got Unknown
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0619: Expected O, but got Unknown
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Expected O, but got Unknown
		//IL_0625: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Expected O, but got Unknown
		//IL_0630: Unknown result type (might be due to invalid IL or missing references)
		//IL_063a: Expected O, but got Unknown
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0645: Expected O, but got Unknown
		//IL_0646: Unknown result type (might be due to invalid IL or missing references)
		//IL_0650: Expected O, but got Unknown
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Expected O, but got Unknown
		//IL_065c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0666: Expected O, but got Unknown
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_0671: Expected O, but got Unknown
		//IL_0672: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Expected O, but got Unknown
		//IL_067d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0687: Expected O, but got Unknown
		//IL_0688: Unknown result type (might be due to invalid IL or missing references)
		//IL_0692: Expected O, but got Unknown
		//IL_0693: Unknown result type (might be due to invalid IL or missing references)
		//IL_069d: Expected O, but got Unknown
		//IL_069e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a8: Expected O, but got Unknown
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b3: Expected O, but got Unknown
		//IL_06ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d4: Expected O, but got Unknown
		//IL_06d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Expected O, but got Unknown
		//IL_06e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ea: Expected O, but got Unknown
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f5: Expected O, but got Unknown
		//IL_06f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0700: Expected O, but got Unknown
		//IL_0701: Unknown result type (might be due to invalid IL or missing references)
		//IL_070b: Expected O, but got Unknown
		//IL_070c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0716: Expected O, but got Unknown
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_0721: Expected O, but got Unknown
		//IL_0722: Unknown result type (might be due to invalid IL or missing references)
		//IL_072c: Expected O, but got Unknown
		//IL_072d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0737: Expected O, but got Unknown
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Expected O, but got Unknown
		//IL_0743: Unknown result type (might be due to invalid IL or missing references)
		//IL_074d: Expected O, but got Unknown
		//IL_074e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0758: Expected O, but got Unknown
		//IL_0759: Unknown result type (might be due to invalid IL or missing references)
		//IL_0763: Expected O, but got Unknown
		//IL_0764: Unknown result type (might be due to invalid IL or missing references)
		//IL_076e: Expected O, but got Unknown
		//IL_076f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0779: Expected O, but got Unknown
		//IL_077a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0784: Expected O, but got Unknown
		//IL_0785: Unknown result type (might be due to invalid IL or missing references)
		//IL_078f: Expected O, but got Unknown
		//IL_0790: Unknown result type (might be due to invalid IL or missing references)
		//IL_079a: Expected O, but got Unknown
		//IL_079b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a5: Expected O, but got Unknown
		//IL_07a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b0: Expected O, but got Unknown
		//IL_07b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bb: Expected O, but got Unknown
		//IL_07bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c6: Expected O, but got Unknown
		//IL_07c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d1: Expected O, but got Unknown
		//IL_07d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07dc: Expected O, but got Unknown
		//IL_07dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e7: Expected O, but got Unknown
		//IL_07e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f2: Expected O, but got Unknown
		//IL_07f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fd: Expected O, but got Unknown
		//IL_07fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0808: Expected O, but got Unknown
		//IL_0809: Unknown result type (might be due to invalid IL or missing references)
		//IL_0813: Expected O, but got Unknown
		//IL_0814: Unknown result type (might be due to invalid IL or missing references)
		//IL_081e: Expected O, but got Unknown
		//IL_081f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0829: Expected O, but got Unknown
		//IL_082a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0834: Expected O, but got Unknown
		//IL_0835: Unknown result type (might be due to invalid IL or missing references)
		//IL_083f: Expected O, but got Unknown
		//IL_0840: Unknown result type (might be due to invalid IL or missing references)
		//IL_084a: Expected O, but got Unknown
		//IL_084b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0855: Expected O, but got Unknown
		//IL_0856: Unknown result type (might be due to invalid IL or missing references)
		//IL_0860: Expected O, but got Unknown
		//IL_0861: Unknown result type (might be due to invalid IL or missing references)
		//IL_086b: Expected O, but got Unknown
		//IL_086c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0876: Expected O, but got Unknown
		//IL_0877: Unknown result type (might be due to invalid IL or missing references)
		//IL_0881: Expected O, but got Unknown
		//IL_0882: Unknown result type (might be due to invalid IL or missing references)
		//IL_088c: Expected O, but got Unknown
		//IL_088d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0897: Expected O, but got Unknown
		//IL_0898: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a2: Expected O, but got Unknown
		//IL_08a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ad: Expected O, but got Unknown
		//IL_08ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b8: Expected O, but got Unknown
		//IL_08b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c3: Expected O, but got Unknown
		//IL_08c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ce: Expected O, but got Unknown
		//IL_08cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d9: Expected O, but got Unknown
		//IL_08da: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e4: Expected O, but got Unknown
		//IL_08e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ef: Expected O, but got Unknown
		//IL_08f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fa: Expected O, but got Unknown
		//IL_08fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0905: Expected O, but got Unknown
		//IL_0906: Unknown result type (might be due to invalid IL or missing references)
		//IL_0910: Expected O, but got Unknown
		//IL_0911: Unknown result type (might be due to invalid IL or missing references)
		//IL_091b: Expected O, but got Unknown
		//IL_091c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0926: Expected O, but got Unknown
		//IL_0948: Unknown result type (might be due to invalid IL or missing references)
		//IL_0952: Expected O, but got Unknown
		//IL_0953: Unknown result type (might be due to invalid IL or missing references)
		//IL_095d: Expected O, but got Unknown
		//IL_095e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0968: Expected O, but got Unknown
		//IL_0969: Unknown result type (might be due to invalid IL or missing references)
		//IL_0973: Expected O, but got Unknown
		//IL_0974: Unknown result type (might be due to invalid IL or missing references)
		//IL_097e: Expected O, but got Unknown
		//IL_097f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0989: Expected O, but got Unknown
		//IL_098a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0994: Expected O, but got Unknown
		//IL_0995: Unknown result type (might be due to invalid IL or missing references)
		//IL_099f: Expected O, but got Unknown
		//IL_09a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09aa: Expected O, but got Unknown
		//IL_09ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b5: Expected O, but got Unknown
		//IL_09b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c0: Expected O, but got Unknown
		//IL_0a35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3f: Expected O, but got Unknown
		//IL_0a40: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4a: Expected O, but got Unknown
		//IL_291c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2926: Expected O, but got Unknown
		//IL_2db7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dc1: Expected O, but got Unknown
		//IL_2dcf: Unknown result type (might be due to invalid IL or missing references)
		//IL_2dd9: Expected O, but got Unknown
		//IL_4d78: Unknown result type (might be due to invalid IL or missing references)
		//IL_4d82: Expected O, but got Unknown
		//IL_5169: Unknown result type (might be due to invalid IL or missing references)
		//IL_5173: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.MasterData.frmItemsTree));
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
		Appearance val77 = new Appearance();
		Appearance val78 = new Appearance();
		Appearance val79 = new Appearance();
		Appearance val80 = new Appearance();
		Appearance val81 = new Appearance();
		Appearance val82 = new Appearance();
		Appearance val83 = new Appearance();
		Appearance val84 = new Appearance();
		Appearance val85 = new Appearance();
		Appearance val86 = new Appearance();
		Appearance val87 = new Appearance();
		Appearance val88 = new Appearance();
		Appearance val89 = new Appearance();
		Appearance val90 = new Appearance();
		Appearance val91 = new Appearance();
		Appearance val92 = new Appearance();
		Appearance val93 = new Appearance();
		Appearance val94 = new Appearance();
		Appearance val95 = new Appearance();
		Appearance val96 = new Appearance();
		Appearance val97 = new Appearance();
		Appearance val98 = new Appearance();
		Appearance val99 = new Appearance();
		Appearance val100 = new Appearance();
		Appearance val101 = new Appearance();
		Appearance val102 = new Appearance();
		Appearance val103 = new Appearance();
		Appearance val104 = new Appearance();
		Appearance val105 = new Appearance();
		Appearance val106 = new Appearance();
		Appearance val107 = new Appearance();
		Appearance val108 = new Appearance();
		UltraTab val109 = new UltraTab();
		UltraTab val110 = new UltraTab();
		UltraTab val111 = new UltraTab();
		UltraTab val112 = new UltraTab();
		UltraTab val113 = new UltraTab();
		UltraTab val114 = new UltraTab();
		UltraTab val115 = new UltraTab();
		UltraTab val116 = new UltraTab();
		UltraTab val117 = new UltraTab();
		UltraTab val118 = new UltraTab();
		UltraTab val119 = new UltraTab();
		Appearance val120 = new Appearance();
		Appearance val121 = new Appearance();
		Appearance val122 = new Appearance();
		this.tabItem = new UltraTabPageControl();
		this.chkUsePOSNumPad = new UltraCheckEditor();
		this.txtCyl = new UltraTextEditor();
		this.lblCyl = new UltraLabel();
		this.txtVolume = new UltraTextEditor();
		this.ultraLabel4 = new UltraLabel();
		this.txtNetWeight = new UltraTextEditor();
		this.ultraLabel3 = new UltraLabel();
		this.txtGrowthWeight = new UltraTextEditor();
		this.ultraLabel2 = new UltraLabel();
		this.txtSph = new UltraTextEditor();
		this.lblSph = new UltraLabel();
		this.cboColorCategory = new UltraComboEditor();
		this.cboSizeCategory = new UltraComboEditor();
		this.cboPrinterName = new UltraComboEditor();
		this.txtSupplierItemCode = new UltraTextEditor();
		this.ultraLabel5 = new UltraLabel();
		this.txtPrepareTime = new UltraTextEditor();
		this.lblPrepareTime = new UltraLabel();
		this.chkEnforceBatchNo = new UltraCheckEditor();
		this.chkIsProductionItem = new UltraCheckEditor();
		this.btnPic = new UltraButton();
		this.cboStores = new UltraComboEditor();
		this.ultraLabel1 = new UltraLabel();
		this.chkIsDirectItem = new UltraCheckEditor();
		this.chkIsSalesItem = new UltraCheckEditor();
		this.cboUnitGroup = new UltraComboEditor();
		this.cboUnit = new UltraComboEditor();
		this.lblUnitGroup = new UltraLabel();
		this.lblUnit = new UltraLabel();
		this.lblColorCategory = new UltraLabel();
		this.lblSizeCategory = new UltraLabel();
		this.lblPic = new UltraLabel();
		this.txtValidityDays = new UltraTextEditor();
		this.lblPrinterName = new UltraLabel();
		this.lblValidityDays = new UltraLabel();
		this.picItem = new UltraPictureBox();
		this.cboTypes = new UltraComboEditor();
		this.lblTypes = new UltraLabel();
		this.tabService = new UltraTabPageControl();
		this.btnServiceAccountSearch = new UltraButton();
		this.cboServiceAccount = new UltraComboEditor();
		this.lblServiceAccount = new UltraLabel();
		this.tabRecipe = new UltraTabPageControl();
		this.ultraPanel3 = new UltraPanel();
		this.rbRecipeForEachBranch = new System.Windows.Forms.RadioButton();
		this.rbRecipeForAllBranchs = new System.Windows.Forms.RadioButton();
		this.ULGRecipeItems = new UltraGrid();
		this.ultraTabPageControl1 = new UltraTabPageControl();
		this.chkIsUnitPrice = new UltraCheckEditor();
		this.ULGPrices = new UltraGrid();
		this.ultraPanel1 = new UltraPanel();
		this.rbPriceForEachBranches = new System.Windows.Forms.RadioButton();
		this.rbPriceForAllBranches = new System.Windows.Forms.RadioButton();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.btnDepartmentIssueAccount = new UltraButton();
		this.cboDepartmentIssueAccount = new UltraComboEditor();
		this.lblDepartmentIssueAccount = new UltraLabel();
		this.btnPurchaseReturnsAccount = new UltraButton();
		this.cboPurchaseReturnsAccount = new UltraComboEditor();
		this.lblPurchaseReturnsAccount = new UltraLabel();
		this.btnPurchaseAccount = new UltraButton();
		this.cboPurchaseAccount = new UltraComboEditor();
		this.lblPurchaseAccount = new UltraLabel();
		this.btnCostOfSalesAccount = new UltraButton();
		this.cboCostOfSalesAccount = new UltraComboEditor();
		this.lblCostOfSalesAccount = new UltraLabel();
		this.btnSalesReturnsAccount = new UltraButton();
		this.cboSalesReturnsAccount = new UltraComboEditor();
		this.lblSalesReturnsAccount = new UltraLabel();
		this.btnSalesAccount = new UltraButton();
		this.cboSalesAccount = new UltraComboEditor();
		this.lblSalesAccount = new UltraLabel();
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.ULGLevels = new UltraGrid();
		this.ultraPanel2 = new UltraPanel();
		this.rbStockLevelForEachBranch = new System.Windows.Forms.RadioButton();
		this.rbStockLevelsForAllBranches = new System.Windows.Forms.RadioButton();
		this.ultraTabPageControl4 = new UltraTabPageControl();
		this.cboColor = new UltraComboEditor();
		this.cboSize = new UltraComboEditor();
		this.lblColor = new UltraLabel();
		this.lblSize = new UltraLabel();
		this.cboSuppliers = new UltraComboEditor();
		this.lblSuplliers = new UltraLabel();
		this.cboShape = new UltraComboEditor();
		this.lblShape = new UltraLabel();
		this.cboModel = new UltraComboEditor();
		this.lblModel = new UltraLabel();
		this.cboMaterial2 = new UltraComboEditor();
		this.lblMaterial2 = new UltraLabel();
		this.cboMaterial1 = new UltraComboEditor();
		this.lblMaterial1 = new UltraLabel();
		this.cboPriceCategory = new UltraComboEditor();
		this.lblPriceCategory = new UltraLabel();
		this.cboCountry = new UltraComboEditor();
		this.lblCountry = new UltraLabel();
		this.cboSeason = new UltraComboEditor();
		this.lblSeason = new UltraLabel();
		this.cboClassification4 = new UltraComboEditor();
		this.lblClassification4 = new UltraLabel();
		this.cboClassification3 = new UltraComboEditor();
		this.lblClassification3 = new UltraLabel();
		this.cboClassification2 = new UltraComboEditor();
		this.lblClassification2 = new UltraLabel();
		this.cboClassification1 = new UltraComboEditor();
		this.lblClassification1 = new UltraLabel();
		this.cboAgeGroup = new UltraComboEditor();
		this.lblAgeGroup = new UltraLabel();
		this.cboBrand = new UltraComboEditor();
		this.lblBrand = new UltraLabel();
		this.ultraTabPageControl5 = new UltraTabPageControl();
		this.txtAccessoriesCount = new UltraTextEditor();
		this.lblAccessoriesCount = new UltraLabel();
		this.ULGAccessoriesItems = new UltraGrid();
		this.ultraTabPageControl6 = new UltraTabPageControl();
		this.ULGAdditionalsItems = new UltraGrid();
		this.ultraTabPageControl7 = new UltraTabPageControl();
		this.btnEINVExportItems = new UltraButton();
		this.txtEINVItemNameCode = new UltraTextEditor();
		this.lblEINVItemNameCode = new UltraLabel();
		this.cboEINVItemGPC = new UltraComboEditor();
		this.lblEINVItemGPC = new UltraLabel();
		this.cboEINVItemNameType = new UltraComboEditor();
		this.lblEINVItemNameType = new UltraLabel();
		this.ultraTabPageControl8 = new UltraTabPageControl();
		this.cboGrowthTax = new UltraComboEditor();
		this.lblGrowthTax = new UltraLabel();
		this.cboTableTax = new UltraComboEditor();
		this.lblTableTax = new UltraLabel();
		this.cboTax = new UltraComboEditor();
		this.lblTax = new UltraLabel();
		this.pnlCheckType = new UltraPanel();
		this.rbIsRecipe = new System.Windows.Forms.RadioButton();
		this.rbIsService = new System.Windows.Forms.RadioButton();
		this.rbIsItem = new System.Windows.Forms.RadioButton();
		this.btnCostCenterSearch = new UltraButton();
		this.cboCostCenter = new UltraComboEditor();
		this.lblCostCenter = new UltraLabel();
		this.chkCanModSalesPrice = new UltraCheckEditor();
		this.chkIsActive = new UltraCheckEditor();
		this.txtNotes = new UltraTextEditor();
		this.lblReceivingBank = new UltraLabel();
		this.tabItemType = new UltraTabControl();
		this.ultraTabSharedControlsPage1 = new UltraTabSharedControlsPage();
		this.txtBarCode = new UltraTextEditor();
		this.lblBarCode = new UltraLabel();
		this.ofdItemPic = new System.Windows.Forms.OpenFileDialog();
		this.modifyGroupItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.modifyGroupItemsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.modifyGroupItemsToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
		this.modifyGroupItemsToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.modifyGroupItemsToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
		this.changeClassificationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.changeParentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.chkHideFromReports = new UltraCheckEditor();
		this.chkCanModPurchasePrice = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.dtChart).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.treeChart).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtName).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabItem).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.chkUsePOSNumPad).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCyl).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVolume).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetWeight).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrowthWeight).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSph).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboColorCategory).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSizeCategory).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPrinterName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSupplierItemCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPrepareTime).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkEnforceBatchNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsProductionItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboStores).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsDirectItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsSalesItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnitGroup).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtValidityDays).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTypes).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabService).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboServiceAccount).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabRecipe).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.ultraPanel3.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.ultraPanel3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGRecipeItems).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.chkIsUnitPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGPrices).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraPanel1.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.ultraPanel1).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboDepartmentIssueAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPurchaseReturnsAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPurchaseAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCostOfSalesAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesReturnsAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesAccount).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGLevels).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraPanel2.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.ultraPanel2).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboColor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSize).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSuppliers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboShape).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboModel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaterial2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaterial1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceCategory).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSeason).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAgeGroup).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBrand).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtAccessoriesCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGAccessoriesItems).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGAdditionalsItems).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtEINVItemNameCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboEINVItemGPC).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboEINVItemNameType).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboGrowthTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTableTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboCostCenter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkCanModSalesPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tabItemType).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabItemType).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.chkHideFromReports).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkCanModPurchasePrice).BeginInit();
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
		resources.ApplyResources(this.tabItem, "tabItem");
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.chkUsePOSNumPad);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtCyl);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblCyl);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtVolume);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtNetWeight);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtGrowthWeight);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtSph);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblSph);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboColorCategory);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboSizeCategory);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboPrinterName);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtSupplierItemCode);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtPrepareTime);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblPrepareTime);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.chkEnforceBatchNo);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.chkIsProductionItem);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.btnPic);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboStores);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.chkIsDirectItem);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.chkIsSalesItem);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboUnitGroup);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboUnit);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblUnitGroup);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblUnit);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblColorCategory);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblSizeCategory);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblPic);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtValidityDays);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblPrinterName);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblValidityDays);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.picItem);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboTypes);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblTypes);
		((System.Windows.Forms.Control)(object)this.tabItem).Name = "tabItem";
		resources.ApplyResources(this.chkUsePOSNumPad, "chkUsePOSNumPad");
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val5, "appearance5");
		((UltraToggleEditorBase)this.chkUsePOSNumPad).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.chkUsePOSNumPad).Name = "chkUsePOSNumPad";
		resources.ApplyResources(this.txtCyl, "txtCyl");
		((System.Windows.Forms.Control)(object)this.txtCyl).Name = "txtCyl";
		((System.Windows.Forms.Control)(object)this.txtCyl).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress2);
		resources.ApplyResources(this.lblCyl, "lblCyl");
		this.lblCyl.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCyl).Name = "lblCyl";
		((ControlBase)this.lblCyl).WrapText = false;
		resources.ApplyResources(this.txtVolume, "txtVolume");
		((System.Windows.Forms.Control)(object)this.txtVolume).Name = "txtVolume";
		((System.Windows.Forms.Control)(object)this.txtVolume).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		this.ultraLabel4.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		((ControlBase)this.ultraLabel4).WrapText = false;
		resources.ApplyResources(this.txtNetWeight, "txtNetWeight");
		((System.Windows.Forms.Control)(object)this.txtNetWeight).Name = "txtNetWeight";
		((System.Windows.Forms.Control)(object)this.txtNetWeight).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.txtGrowthWeight, "txtGrowthWeight");
		((System.Windows.Forms.Control)(object)this.txtGrowthWeight).Name = "txtGrowthWeight";
		((System.Windows.Forms.Control)(object)this.txtGrowthWeight).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.txtSph, "txtSph");
		((System.Windows.Forms.Control)(object)this.txtSph).Name = "txtSph";
		((System.Windows.Forms.Control)(object)this.txtSph).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress2);
		resources.ApplyResources(this.lblSph, "lblSph");
		this.lblSph.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSph).Name = "lblSph";
		((ControlBase)this.lblSph).WrapText = false;
		resources.ApplyResources(this.cboColorCategory, "cboColorCategory");
		this.cboColorCategory.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboColorCategory).Name = "cboColorCategory";
		resources.ApplyResources(this.cboSizeCategory, "cboSizeCategory");
		this.cboSizeCategory.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSizeCategory).Name = "cboSizeCategory";
		resources.ApplyResources(this.cboPrinterName, "cboPrinterName");
		this.cboPrinterName.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboPrinterName).Name = "cboPrinterName";
		resources.ApplyResources(this.txtSupplierItemCode, "txtSupplierItemCode");
		((System.Windows.Forms.Control)(object)this.txtSupplierItemCode).Name = "txtSupplierItemCode";
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.ultraLabel5).Appearance = (AppearanceBase)(object)val6;
		this.ultraLabel5.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.txtPrepareTime, "txtPrepareTime");
		((System.Windows.Forms.Control)(object)this.txtPrepareTime).Name = "txtPrepareTime";
		((System.Windows.Forms.Control)(object)this.txtPrepareTime).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInt_KeyPress);
		resources.ApplyResources(this.lblPrepareTime, "lblPrepareTime");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.lblPrepareTime).Appearance = (AppearanceBase)(object)val7;
		this.lblPrepareTime.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPrepareTime).Name = "lblPrepareTime";
		((ControlBase)this.lblPrepareTime).WrapText = false;
		resources.ApplyResources(this.chkEnforceBatchNo, "chkEnforceBatchNo");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((UltraToggleEditorBase)this.chkEnforceBatchNo).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.chkEnforceBatchNo).Name = "chkEnforceBatchNo";
		((UltraToggleEditorBase)this.chkEnforceBatchNo).CheckedChanged += new System.EventHandler(chkEnforceBatchNo_CheckedChanged);
		resources.ApplyResources(this.chkIsProductionItem, "chkIsProductionItem");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance9");
		((UltraToggleEditorBase)this.chkIsProductionItem).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.chkIsProductionItem).Name = "chkIsProductionItem";
		resources.ApplyResources(this.btnPic, "btnPic");
		((System.Windows.Forms.Control)(object)this.btnPic).Name = "btnPic";
		((System.Windows.Forms.Control)(object)this.btnPic).Click += new System.EventHandler(btnPic_Click);
		resources.ApplyResources(this.cboStores, "cboStores");
		((System.Windows.Forms.Control)(object)this.cboStores).Name = "cboStores";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance10");
		((AppearanceBase)val10).TextTrimming = (TextTrimming)6;
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val10;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.chkIsDirectItem, "chkIsDirectItem");
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance11");
		((UltraToggleEditorBase)this.chkIsDirectItem).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.chkIsDirectItem).Name = "chkIsDirectItem";
		resources.ApplyResources(this.chkIsSalesItem, "chkIsSalesItem");
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance12");
		((UltraToggleEditorBase)this.chkIsSalesItem).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.chkIsSalesItem).Name = "chkIsSalesItem";
		resources.ApplyResources(this.cboUnitGroup, "cboUnitGroup");
		((System.Windows.Forms.Control)(object)this.cboUnitGroup).Name = "cboUnitGroup";
		((TextEditorControlBase)this.cboUnitGroup).ValueChanged += new System.EventHandler(cboUnitGroup_ValueChanged);
		resources.ApplyResources(this.cboUnit, "cboUnit");
		((System.Windows.Forms.Control)(object)this.cboUnit).Name = "cboUnit";
		resources.ApplyResources(this.lblUnitGroup, "lblUnitGroup");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance13");
		((ControlBase)this.lblUnitGroup).Appearance = (AppearanceBase)(object)val13;
		this.lblUnitGroup.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUnitGroup).Name = "lblUnitGroup";
		((ControlBase)this.lblUnitGroup).WrapText = false;
		resources.ApplyResources(this.lblUnit, "lblUnit");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val14, "appearance14");
		((ControlBase)this.lblUnit).Appearance = (AppearanceBase)(object)val14;
		this.lblUnit.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUnit).Name = "lblUnit";
		((ControlBase)this.lblUnit).WrapText = false;
		resources.ApplyResources(this.lblColorCategory, "lblColorCategory");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val15, "appearance15");
		((ControlBase)this.lblColorCategory).Appearance = (AppearanceBase)(object)val15;
		this.lblColorCategory.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblColorCategory).Name = "lblColorCategory";
		((ControlBase)this.lblColorCategory).WrapText = false;
		resources.ApplyResources(this.lblSizeCategory, "lblSizeCategory");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val16, "appearance16");
		((ControlBase)this.lblSizeCategory).Appearance = (AppearanceBase)(object)val16;
		this.lblSizeCategory.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSizeCategory).Name = "lblSizeCategory";
		((ControlBase)this.lblSizeCategory).WrapText = false;
		resources.ApplyResources(this.lblPic, "lblPic");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val17, "appearance17");
		((ControlBase)this.lblPic).Appearance = (AppearanceBase)(object)val17;
		this.lblPic.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPic).Name = "lblPic";
		((ControlBase)this.lblPic).WrapText = false;
		resources.ApplyResources(this.txtValidityDays, "txtValidityDays");
		((System.Windows.Forms.Control)(object)this.txtValidityDays).Name = "txtValidityDays";
		((System.Windows.Forms.Control)(object)this.txtValidityDays).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInt_KeyPress);
		resources.ApplyResources(this.lblPrinterName, "lblPrinterName");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val18).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val18, "appearance18");
		((ControlBase)this.lblPrinterName).Appearance = (AppearanceBase)(object)val18;
		this.lblPrinterName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPrinterName).Name = "lblPrinterName";
		((ControlBase)this.lblPrinterName).WrapText = false;
		resources.ApplyResources(this.lblValidityDays, "lblValidityDays");
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val19, "appearance19");
		((ControlBase)this.lblValidityDays).Appearance = (AppearanceBase)(object)val19;
		this.lblValidityDays.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblValidityDays).Name = "lblValidityDays";
		((ControlBase)this.lblValidityDays).WrapText = false;
		resources.ApplyResources(this.picItem, "picItem");
		this.picItem.BorderShadowColor = System.Drawing.Color.Empty;
		this.picItem.BorderStyle = (UIElementBorderStyle)2;
		((System.Windows.Forms.Control)(object)this.picItem).Name = "picItem";
		resources.ApplyResources(this.cboTypes, "cboTypes");
		this.cboTypes.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboTypes).Name = "cboTypes";
		resources.ApplyResources(this.lblTypes, "lblTypes");
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val20, "appearance20");
		((ControlBase)this.lblTypes).Appearance = (AppearanceBase)(object)val20;
		this.lblTypes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTypes).Name = "lblTypes";
		((ControlBase)this.lblTypes).WrapText = false;
		resources.ApplyResources(this.tabService, "tabService");
		((System.Windows.Forms.Control)(object)this.tabService).Controls.Add((System.Windows.Forms.Control)(object)this.btnServiceAccountSearch);
		((System.Windows.Forms.Control)(object)this.tabService).Controls.Add((System.Windows.Forms.Control)(object)this.cboServiceAccount);
		((System.Windows.Forms.Control)(object)this.tabService).Controls.Add((System.Windows.Forms.Control)(object)this.lblServiceAccount);
		((System.Windows.Forms.Control)(object)this.tabService).Name = "tabService";
		resources.ApplyResources(this.btnServiceAccountSearch, "btnServiceAccountSearch");
		((AppearanceBase)val21).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val21, "appearance21");
		((ControlBase)this.btnServiceAccountSearch).Appearance = (AppearanceBase)(object)val21;
		((System.Windows.Forms.Control)(object)this.btnServiceAccountSearch).Name = "btnServiceAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnServiceAccountSearch).Click += new System.EventHandler(btnServiceAccountSearch_Click);
		resources.ApplyResources(this.cboServiceAccount, "cboServiceAccount");
		this.cboServiceAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboServiceAccount).Name = "cboServiceAccount";
		resources.ApplyResources(this.lblServiceAccount, "lblServiceAccount");
		this.lblServiceAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblServiceAccount).Name = "lblServiceAccount";
		((ControlBase)this.lblServiceAccount).WrapText = false;
		resources.ApplyResources(this.tabRecipe, "tabRecipe");
		((System.Windows.Forms.Control)(object)this.tabRecipe).Controls.Add((System.Windows.Forms.Control)(object)this.ultraPanel3);
		((System.Windows.Forms.Control)(object)this.tabRecipe).Controls.Add((System.Windows.Forms.Control)(object)this.ULGRecipeItems);
		((System.Windows.Forms.Control)(object)this.tabRecipe).Name = "tabRecipe";
		resources.ApplyResources(this.ultraPanel3, "ultraPanel3");
		((AppearanceBase)val22).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val22, "appearance22");
		this.ultraPanel3.Appearance = (AppearanceBase)(object)val22;
		resources.ApplyResources(this.ultraPanel3.ClientArea, "ultraPanel3.ClientArea");
		((System.Windows.Forms.Control)(object)this.ultraPanel3.ClientArea).Controls.Add(this.rbRecipeForEachBranch);
		((System.Windows.Forms.Control)(object)this.ultraPanel3.ClientArea).Controls.Add(this.rbRecipeForAllBranchs);
		((System.Windows.Forms.Control)(object)this.ultraPanel3).Name = "ultraPanel3";
		resources.ApplyResources(this.rbRecipeForEachBranch, "rbRecipeForEachBranch");
		this.rbRecipeForEachBranch.BackColor = System.Drawing.Color.Transparent;
		this.rbRecipeForEachBranch.ForeColor = System.Drawing.Color.Navy;
		this.rbRecipeForEachBranch.Name = "rbRecipeForEachBranch";
		this.rbRecipeForEachBranch.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.rbRecipeForAllBranchs, "rbRecipeForAllBranchs");
		this.rbRecipeForAllBranchs.BackColor = System.Drawing.Color.Transparent;
		this.rbRecipeForAllBranchs.Checked = true;
		this.rbRecipeForAllBranchs.ForeColor = System.Drawing.Color.Navy;
		this.rbRecipeForAllBranchs.Name = "rbRecipeForAllBranchs";
		this.rbRecipeForAllBranchs.TabStop = true;
		this.rbRecipeForAllBranchs.UseVisualStyleBackColor = false;
		this.rbRecipeForAllBranchs.CheckedChanged += new System.EventHandler(rbRecipeForAllBranchs_CheckedChanged);
		resources.ApplyResources(this.ULGRecipeItems, "ULGRecipeItems");
		((AppearanceBase)val23).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val23).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val23).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val23).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val23, "appearance23");
		((SpecialBoxBase)((UltraGridBase)this.ULGRecipeItems).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val23;
		((AppearanceBase)val24).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val24, "appearance24");
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val24;
		((SpecialBoxBase)((UltraGridBase)this.ULGRecipeItems).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val25).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val25).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val25).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val25).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val25, "appearance25");
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val25;
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val26).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val26).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val26, "appearance26");
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val26;
		((AppearanceBase)val27).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val27).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val27, "appearance27");
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val27;
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val28).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val28, "appearance28");
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val28;
		((AppearanceBase)val29).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val29, "appearance29");
		((AppearanceBase)val29).TextTrimming = (TextTrimming)6;
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val29;
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val30).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val30).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val30).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val30).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val30).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val30, "appearance30");
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val30;
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val31).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val31).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val31, "appearance31");
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val31;
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val32).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val32, "appearance32");
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val32;
		((System.Windows.Forms.Control)(object)this.ULGRecipeItems).Name = "ULGRecipeItems";
		this.ULGRecipeItems.AfterEnterEditMode += new System.EventHandler(ULGRecipeItems_AfterEnterEditMode);
		this.ULGRecipeItems.CellListSelect += new CellEventHandler(ULGRecipeItems_CellListSelect);
		resources.ApplyResources(this.ultraTabPageControl1, "ultraTabPageControl1");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.chkIsUnitPrice);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.ULGPrices);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.ultraPanel1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Name = "ultraTabPageControl1";
		resources.ApplyResources(this.chkIsUnitPrice, "chkIsUnitPrice");
		((AppearanceBase)val33).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val33, "appearance33");
		((UltraToggleEditorBase)this.chkIsUnitPrice).Appearance = (AppearanceBase)(object)val33;
		((System.Windows.Forms.Control)(object)this.chkIsUnitPrice).Name = "chkIsUnitPrice";
		((UltraToggleEditorBase)this.chkIsUnitPrice).CheckedChanged += new System.EventHandler(chkIsUnitPrice_CheckedChanged);
		resources.ApplyResources(this.ULGPrices, "ULGPrices");
		((AppearanceBase)val34).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val34).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val34).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val34).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val34, "appearance34");
		((SpecialBoxBase)((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val34;
		((AppearanceBase)val35).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val35, "appearance35");
		((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val35;
		((SpecialBoxBase)((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val36).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val36).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val36).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val36).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val36, "appearance36");
		((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val36;
		((UltraGridBase)this.ULGPrices).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGPrices).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val37).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val37).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val37, "appearance37");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val37;
		((AppearanceBase)val38).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val38).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val38, "appearance38");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val38;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val39).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val39, "appearance39");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val39;
		((AppearanceBase)val40).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val40, "appearance40");
		((AppearanceBase)val40).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val40;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val41).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val41).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val41).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val41).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val41).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val41, "appearance41");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val41;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val42).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val42).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val42, "appearance42");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val42;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val43).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val43, "appearance43");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val43;
		((System.Windows.Forms.Control)(object)this.ULGPrices).Name = "ULGPrices";
		this.ULGPrices.AfterEnterEditMode += new System.EventHandler(ULGPrices_AfterEnterEditMode);
		this.ULGPrices.CellListSelect += new CellEventHandler(ULGPrices_CellListSelect);
		this.ULGPrices.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGPrices_BeforeRowsDeleted);
		((System.Windows.Forms.Control)(object)this.ULGPrices).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGPrices_KeyPress);
		resources.ApplyResources(this.ultraPanel1, "ultraPanel1");
		((AppearanceBase)val44).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val44, "appearance44");
		this.ultraPanel1.Appearance = (AppearanceBase)(object)val44;
		resources.ApplyResources(this.ultraPanel1.ClientArea, "ultraPanel1.ClientArea");
		((System.Windows.Forms.Control)(object)this.ultraPanel1.ClientArea).Controls.Add(this.rbPriceForEachBranches);
		((System.Windows.Forms.Control)(object)this.ultraPanel1.ClientArea).Controls.Add(this.rbPriceForAllBranches);
		((System.Windows.Forms.Control)(object)this.ultraPanel1).Name = "ultraPanel1";
		resources.ApplyResources(this.rbPriceForEachBranches, "rbPriceForEachBranches");
		this.rbPriceForEachBranches.BackColor = System.Drawing.Color.Transparent;
		this.rbPriceForEachBranches.ForeColor = System.Drawing.Color.Navy;
		this.rbPriceForEachBranches.Name = "rbPriceForEachBranches";
		this.rbPriceForEachBranches.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.rbPriceForAllBranches, "rbPriceForAllBranches");
		this.rbPriceForAllBranches.BackColor = System.Drawing.Color.Transparent;
		this.rbPriceForAllBranches.Checked = true;
		this.rbPriceForAllBranches.ForeColor = System.Drawing.Color.Navy;
		this.rbPriceForAllBranches.Name = "rbPriceForAllBranches";
		this.rbPriceForAllBranches.TabStop = true;
		this.rbPriceForAllBranches.UseVisualStyleBackColor = false;
		this.rbPriceForAllBranches.CheckedChanged += new System.EventHandler(rbPriceForAllBranches_CheckedChanged);
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.btnDepartmentIssueAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.cboDepartmentIssueAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblDepartmentIssueAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.btnPurchaseReturnsAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.cboPurchaseReturnsAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblPurchaseReturnsAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.btnPurchaseAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.cboPurchaseAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblPurchaseAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.btnCostOfSalesAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.cboCostOfSalesAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblCostOfSalesAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.btnSalesReturnsAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.cboSalesReturnsAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesReturnsAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.btnSalesAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.cboSalesAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.btnDepartmentIssueAccount, "btnDepartmentIssueAccount");
		((AppearanceBase)val45).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val45, "appearance45");
		((ControlBase)this.btnDepartmentIssueAccount).Appearance = (AppearanceBase)(object)val45;
		((System.Windows.Forms.Control)(object)this.btnDepartmentIssueAccount).Name = "btnDepartmentIssueAccount";
		((System.Windows.Forms.Control)(object)this.btnDepartmentIssueAccount).Click += new System.EventHandler(btnDepartmentIssueAccount_Click);
		resources.ApplyResources(this.cboDepartmentIssueAccount, "cboDepartmentIssueAccount");
		this.cboDepartmentIssueAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboDepartmentIssueAccount).Name = "cboDepartmentIssueAccount";
		resources.ApplyResources(this.lblDepartmentIssueAccount, "lblDepartmentIssueAccount");
		this.lblDepartmentIssueAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDepartmentIssueAccount).Name = "lblDepartmentIssueAccount";
		((ControlBase)this.lblDepartmentIssueAccount).WrapText = false;
		resources.ApplyResources(this.btnPurchaseReturnsAccount, "btnPurchaseReturnsAccount");
		((AppearanceBase)val46).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val46, "appearance46");
		((ControlBase)this.btnPurchaseReturnsAccount).Appearance = (AppearanceBase)(object)val46;
		((System.Windows.Forms.Control)(object)this.btnPurchaseReturnsAccount).Name = "btnPurchaseReturnsAccount";
		((System.Windows.Forms.Control)(object)this.btnPurchaseReturnsAccount).Click += new System.EventHandler(btnPurchaseReturnsAccount_Click);
		resources.ApplyResources(this.cboPurchaseReturnsAccount, "cboPurchaseReturnsAccount");
		this.cboPurchaseReturnsAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboPurchaseReturnsAccount).Name = "cboPurchaseReturnsAccount";
		resources.ApplyResources(this.lblPurchaseReturnsAccount, "lblPurchaseReturnsAccount");
		this.lblPurchaseReturnsAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPurchaseReturnsAccount).Name = "lblPurchaseReturnsAccount";
		((ControlBase)this.lblPurchaseReturnsAccount).WrapText = false;
		resources.ApplyResources(this.btnPurchaseAccount, "btnPurchaseAccount");
		((AppearanceBase)val47).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val47, "appearance47");
		((ControlBase)this.btnPurchaseAccount).Appearance = (AppearanceBase)(object)val47;
		((System.Windows.Forms.Control)(object)this.btnPurchaseAccount).Name = "btnPurchaseAccount";
		((System.Windows.Forms.Control)(object)this.btnPurchaseAccount).Click += new System.EventHandler(btnPurchaseAccount_Click);
		resources.ApplyResources(this.cboPurchaseAccount, "cboPurchaseAccount");
		this.cboPurchaseAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboPurchaseAccount).Name = "cboPurchaseAccount";
		resources.ApplyResources(this.lblPurchaseAccount, "lblPurchaseAccount");
		this.lblPurchaseAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPurchaseAccount).Name = "lblPurchaseAccount";
		((ControlBase)this.lblPurchaseAccount).WrapText = false;
		resources.ApplyResources(this.btnCostOfSalesAccount, "btnCostOfSalesAccount");
		((AppearanceBase)val48).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val48, "appearance48");
		((ControlBase)this.btnCostOfSalesAccount).Appearance = (AppearanceBase)(object)val48;
		((System.Windows.Forms.Control)(object)this.btnCostOfSalesAccount).Name = "btnCostOfSalesAccount";
		((System.Windows.Forms.Control)(object)this.btnCostOfSalesAccount).Click += new System.EventHandler(btnCostOfSalesAccount_Click);
		resources.ApplyResources(this.cboCostOfSalesAccount, "cboCostOfSalesAccount");
		this.cboCostOfSalesAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboCostOfSalesAccount).Name = "cboCostOfSalesAccount";
		resources.ApplyResources(this.lblCostOfSalesAccount, "lblCostOfSalesAccount");
		this.lblCostOfSalesAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCostOfSalesAccount).Name = "lblCostOfSalesAccount";
		((ControlBase)this.lblCostOfSalesAccount).WrapText = false;
		resources.ApplyResources(this.btnSalesReturnsAccount, "btnSalesReturnsAccount");
		((AppearanceBase)val49).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val49, "appearance49");
		((ControlBase)this.btnSalesReturnsAccount).Appearance = (AppearanceBase)(object)val49;
		((System.Windows.Forms.Control)(object)this.btnSalesReturnsAccount).Name = "btnSalesReturnsAccount";
		((System.Windows.Forms.Control)(object)this.btnSalesReturnsAccount).Click += new System.EventHandler(btnSalesReturnsAccount_Click);
		resources.ApplyResources(this.cboSalesReturnsAccount, "cboSalesReturnsAccount");
		this.cboSalesReturnsAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSalesReturnsAccount).Name = "cboSalesReturnsAccount";
		resources.ApplyResources(this.lblSalesReturnsAccount, "lblSalesReturnsAccount");
		this.lblSalesReturnsAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSalesReturnsAccount).Name = "lblSalesReturnsAccount";
		((ControlBase)this.lblSalesReturnsAccount).WrapText = false;
		resources.ApplyResources(this.btnSalesAccount, "btnSalesAccount");
		((AppearanceBase)val50).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val50, "appearance50");
		((ControlBase)this.btnSalesAccount).Appearance = (AppearanceBase)(object)val50;
		((System.Windows.Forms.Control)(object)this.btnSalesAccount).Name = "btnSalesAccount";
		((System.Windows.Forms.Control)(object)this.btnSalesAccount).Click += new System.EventHandler(btnSalesAccount_Click);
		resources.ApplyResources(this.cboSalesAccount, "cboSalesAccount");
		this.cboSalesAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSalesAccount).Name = "cboSalesAccount";
		resources.ApplyResources(this.lblSalesAccount, "lblSalesAccount");
		this.lblSalesAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSalesAccount).Name = "lblSalesAccount";
		((ControlBase)this.lblSalesAccount).WrapText = false;
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ULGLevels);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ultraPanel2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		resources.ApplyResources(this.ULGLevels, "ULGLevels");
		((AppearanceBase)val51).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val51).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val51).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val51).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val51, "appearance51");
		((SpecialBoxBase)((UltraGridBase)this.ULGLevels).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val51;
		((AppearanceBase)val52).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val52, "appearance52");
		((UltraGridBase)this.ULGLevels).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val52;
		((SpecialBoxBase)((UltraGridBase)this.ULGLevels).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val53).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val53).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val53).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val53).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val53, "appearance53");
		((UltraGridBase)this.ULGLevels).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val53;
		((UltraGridBase)this.ULGLevels).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGLevels).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val54).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val54).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val54, "appearance54");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val54;
		((AppearanceBase)val55).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val55).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val55, "appearance55");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val55;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val56).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val56, "appearance56");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val56;
		((AppearanceBase)val57).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val57, "appearance57");
		((AppearanceBase)val57).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val57;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val58).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val58).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val58).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val58).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val58).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val58, "appearance58");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val58;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val59).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val59).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val59, "appearance59");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val59;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val60).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val60, "appearance60");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val60;
		((System.Windows.Forms.Control)(object)this.ULGLevels).Name = "ULGLevels";
		this.ULGLevels.AfterEnterEditMode += new System.EventHandler(ULGLevels_AfterEnterEditMode);
		resources.ApplyResources(this.ultraPanel2, "ultraPanel2");
		((AppearanceBase)val61).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val61, "appearance61");
		this.ultraPanel2.Appearance = (AppearanceBase)(object)val61;
		resources.ApplyResources(this.ultraPanel2.ClientArea, "ultraPanel2.ClientArea");
		((System.Windows.Forms.Control)(object)this.ultraPanel2.ClientArea).Controls.Add(this.rbStockLevelForEachBranch);
		((System.Windows.Forms.Control)(object)this.ultraPanel2.ClientArea).Controls.Add(this.rbStockLevelsForAllBranches);
		((System.Windows.Forms.Control)(object)this.ultraPanel2).Name = "ultraPanel2";
		resources.ApplyResources(this.rbStockLevelForEachBranch, "rbStockLevelForEachBranch");
		this.rbStockLevelForEachBranch.BackColor = System.Drawing.Color.Transparent;
		this.rbStockLevelForEachBranch.ForeColor = System.Drawing.Color.Navy;
		this.rbStockLevelForEachBranch.Name = "rbStockLevelForEachBranch";
		this.rbStockLevelForEachBranch.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.rbStockLevelsForAllBranches, "rbStockLevelsForAllBranches");
		this.rbStockLevelsForAllBranches.BackColor = System.Drawing.Color.Transparent;
		this.rbStockLevelsForAllBranches.Checked = true;
		this.rbStockLevelsForAllBranches.ForeColor = System.Drawing.Color.Navy;
		this.rbStockLevelsForAllBranches.Name = "rbStockLevelsForAllBranches";
		this.rbStockLevelsForAllBranches.TabStop = true;
		this.rbStockLevelsForAllBranches.UseVisualStyleBackColor = false;
		this.rbStockLevelsForAllBranches.CheckedChanged += new System.EventHandler(rbStockLevelsForAllBranches_CheckedChanged);
		resources.ApplyResources(this.ultraTabPageControl4, "ultraTabPageControl4");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboColor);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboSize);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblColor);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblSize);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboSuppliers);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblSuplliers);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboShape);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblShape);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboModel);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblModel);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboMaterial2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblMaterial2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboMaterial1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblMaterial1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboPriceCategory);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblPriceCategory);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboCountry);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblCountry);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboSeason);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblSeason);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboClassification4);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblClassification4);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboClassification3);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblClassification3);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboClassification2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblClassification2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboClassification1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblClassification1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboAgeGroup);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblAgeGroup);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.cboBrand);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblBrand);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Name = "ultraTabPageControl4";
		resources.ApplyResources(this.cboColor, "cboColor");
		this.cboColor.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboColor).Name = "cboColor";
		resources.ApplyResources(this.cboSize, "cboSize");
		this.cboSize.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSize).Name = "cboSize";
		resources.ApplyResources(this.lblColor, "lblColor");
		((AppearanceBase)val62).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val62).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val62, "appearance62");
		((ControlBase)this.lblColor).Appearance = (AppearanceBase)(object)val62;
		this.lblColor.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblColor).Name = "lblColor";
		((ControlBase)this.lblColor).WrapText = false;
		resources.ApplyResources(this.lblSize, "lblSize");
		((AppearanceBase)val63).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val63).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val63, "appearance63");
		((ControlBase)this.lblSize).Appearance = (AppearanceBase)(object)val63;
		this.lblSize.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSize).Name = "lblSize";
		((ControlBase)this.lblSize).WrapText = false;
		resources.ApplyResources(this.cboSuppliers, "cboSuppliers");
		this.cboSuppliers.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSuppliers).Name = "cboSuppliers";
		resources.ApplyResources(this.lblSuplliers, "lblSuplliers");
		((AppearanceBase)val64).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val64).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val64, "appearance64");
		((ControlBase)this.lblSuplliers).Appearance = (AppearanceBase)(object)val64;
		this.lblSuplliers.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSuplliers).Name = "lblSuplliers";
		((ControlBase)this.lblSuplliers).WrapText = false;
		resources.ApplyResources(this.cboShape, "cboShape");
		this.cboShape.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboShape).Name = "cboShape";
		resources.ApplyResources(this.lblShape, "lblShape");
		((AppearanceBase)val65).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val65).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val65, "appearance65");
		((ControlBase)this.lblShape).Appearance = (AppearanceBase)(object)val65;
		this.lblShape.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblShape).Name = "lblShape";
		((ControlBase)this.lblShape).WrapText = false;
		resources.ApplyResources(this.cboModel, "cboModel");
		this.cboModel.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboModel).Name = "cboModel";
		resources.ApplyResources(this.lblModel, "lblModel");
		((AppearanceBase)val66).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val66).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val66, "appearance66");
		((ControlBase)this.lblModel).Appearance = (AppearanceBase)(object)val66;
		this.lblModel.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblModel).Name = "lblModel";
		((ControlBase)this.lblModel).WrapText = false;
		resources.ApplyResources(this.cboMaterial2, "cboMaterial2");
		this.cboMaterial2.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboMaterial2).Name = "cboMaterial2";
		resources.ApplyResources(this.lblMaterial2, "lblMaterial2");
		((AppearanceBase)val67).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val67).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val67, "appearance67");
		((ControlBase)this.lblMaterial2).Appearance = (AppearanceBase)(object)val67;
		this.lblMaterial2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMaterial2).Name = "lblMaterial2";
		((ControlBase)this.lblMaterial2).WrapText = false;
		resources.ApplyResources(this.cboMaterial1, "cboMaterial1");
		this.cboMaterial1.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboMaterial1).Name = "cboMaterial1";
		resources.ApplyResources(this.lblMaterial1, "lblMaterial1");
		((AppearanceBase)val68).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val68).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val68, "appearance68");
		((ControlBase)this.lblMaterial1).Appearance = (AppearanceBase)(object)val68;
		this.lblMaterial1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMaterial1).Name = "lblMaterial1";
		((ControlBase)this.lblMaterial1).WrapText = false;
		resources.ApplyResources(this.cboPriceCategory, "cboPriceCategory");
		this.cboPriceCategory.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboPriceCategory).Name = "cboPriceCategory";
		resources.ApplyResources(this.lblPriceCategory, "lblPriceCategory");
		((AppearanceBase)val69).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val69).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val69, "appearance69");
		((ControlBase)this.lblPriceCategory).Appearance = (AppearanceBase)(object)val69;
		this.lblPriceCategory.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPriceCategory).Name = "lblPriceCategory";
		((ControlBase)this.lblPriceCategory).WrapText = false;
		resources.ApplyResources(this.cboCountry, "cboCountry");
		this.cboCountry.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCountry).Name = "cboCountry";
		resources.ApplyResources(this.lblCountry, "lblCountry");
		((AppearanceBase)val70).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val70).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val70, "appearance70");
		((ControlBase)this.lblCountry).Appearance = (AppearanceBase)(object)val70;
		this.lblCountry.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCountry).Name = "lblCountry";
		((ControlBase)this.lblCountry).WrapText = false;
		resources.ApplyResources(this.cboSeason, "cboSeason");
		this.cboSeason.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSeason).Name = "cboSeason";
		resources.ApplyResources(this.lblSeason, "lblSeason");
		((AppearanceBase)val71).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val71).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val71, "appearance71");
		((ControlBase)this.lblSeason).Appearance = (AppearanceBase)(object)val71;
		this.lblSeason.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSeason).Name = "lblSeason";
		((ControlBase)this.lblSeason).WrapText = false;
		resources.ApplyResources(this.cboClassification4, "cboClassification4");
		this.cboClassification4.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClassification4).Name = "cboClassification4";
		resources.ApplyResources(this.lblClassification4, "lblClassification4");
		((AppearanceBase)val72).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val72).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val72, "appearance72");
		((ControlBase)this.lblClassification4).Appearance = (AppearanceBase)(object)val72;
		this.lblClassification4.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClassification4).Name = "lblClassification4";
		((ControlBase)this.lblClassification4).WrapText = false;
		resources.ApplyResources(this.cboClassification3, "cboClassification3");
		this.cboClassification3.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClassification3).Name = "cboClassification3";
		resources.ApplyResources(this.lblClassification3, "lblClassification3");
		((AppearanceBase)val73).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val73).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val73, "appearance73");
		((ControlBase)this.lblClassification3).Appearance = (AppearanceBase)(object)val73;
		this.lblClassification3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClassification3).Name = "lblClassification3";
		((ControlBase)this.lblClassification3).WrapText = false;
		resources.ApplyResources(this.cboClassification2, "cboClassification2");
		this.cboClassification2.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClassification2).Name = "cboClassification2";
		resources.ApplyResources(this.lblClassification2, "lblClassification2");
		((AppearanceBase)val74).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val74).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val74, "appearance74");
		((ControlBase)this.lblClassification2).Appearance = (AppearanceBase)(object)val74;
		this.lblClassification2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClassification2).Name = "lblClassification2";
		((ControlBase)this.lblClassification2).WrapText = false;
		resources.ApplyResources(this.cboClassification1, "cboClassification1");
		this.cboClassification1.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClassification1).Name = "cboClassification1";
		resources.ApplyResources(this.lblClassification1, "lblClassification1");
		((AppearanceBase)val75).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val75).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val75, "appearance75");
		((ControlBase)this.lblClassification1).Appearance = (AppearanceBase)(object)val75;
		this.lblClassification1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClassification1).Name = "lblClassification1";
		((ControlBase)this.lblClassification1).WrapText = false;
		resources.ApplyResources(this.cboAgeGroup, "cboAgeGroup");
		this.cboAgeGroup.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboAgeGroup).Name = "cboAgeGroup";
		resources.ApplyResources(this.lblAgeGroup, "lblAgeGroup");
		((AppearanceBase)val76).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val76).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val76, "appearance76");
		((ControlBase)this.lblAgeGroup).Appearance = (AppearanceBase)(object)val76;
		this.lblAgeGroup.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAgeGroup).Name = "lblAgeGroup";
		((ControlBase)this.lblAgeGroup).WrapText = false;
		resources.ApplyResources(this.cboBrand, "cboBrand");
		this.cboBrand.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboBrand).Name = "cboBrand";
		resources.ApplyResources(this.lblBrand, "lblBrand");
		((AppearanceBase)val77).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val77).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val77, "appearance77");
		((ControlBase)this.lblBrand).Appearance = (AppearanceBase)(object)val77;
		this.lblBrand.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBrand).Name = "lblBrand";
		((ControlBase)this.lblBrand).WrapText = false;
		resources.ApplyResources(this.ultraTabPageControl5, "ultraTabPageControl5");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.txtAccessoriesCount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.lblAccessoriesCount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Controls.Add((System.Windows.Forms.Control)(object)this.ULGAccessoriesItems);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).Name = "ultraTabPageControl5";
		resources.ApplyResources(this.txtAccessoriesCount, "txtAccessoriesCount");
		((System.Windows.Forms.Control)(object)this.txtAccessoriesCount).Name = "txtAccessoriesCount";
		((System.Windows.Forms.Control)(object)this.txtAccessoriesCount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInt_KeyPress);
		resources.ApplyResources(this.lblAccessoriesCount, "lblAccessoriesCount");
		((AppearanceBase)val78).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val78).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val78, "appearance78");
		((ControlBase)this.lblAccessoriesCount).Appearance = (AppearanceBase)(object)val78;
		this.lblAccessoriesCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAccessoriesCount).Name = "lblAccessoriesCount";
		((ControlBase)this.lblAccessoriesCount).WrapText = false;
		resources.ApplyResources(this.ULGAccessoriesItems, "ULGAccessoriesItems");
		((AppearanceBase)val79).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val79).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val79).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val79).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val79, "appearance79");
		((SpecialBoxBase)((UltraGridBase)this.ULGAccessoriesItems).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val79;
		((AppearanceBase)val80).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val80, "appearance80");
		((UltraGridBase)this.ULGAccessoriesItems).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val80;
		((SpecialBoxBase)((UltraGridBase)this.ULGAccessoriesItems).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val81).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val81).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val81).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val81).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val81, "appearance81");
		((UltraGridBase)this.ULGAccessoriesItems).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val81;
		((UltraGridBase)this.ULGAccessoriesItems).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGAccessoriesItems).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val82).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val82).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val82, "appearance82");
		((UltraGridBase)this.ULGAccessoriesItems).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val82;
		((AppearanceBase)val83).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val83).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val83, "appearance83");
		((UltraGridBase)this.ULGAccessoriesItems).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val83;
		((UltraGridBase)this.ULGAccessoriesItems).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGAccessoriesItems).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val84).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val84, "appearance84");
		((UltraGridBase)this.ULGAccessoriesItems).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val84;
		((AppearanceBase)val85).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val85, "appearance85");
		((AppearanceBase)val85).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGAccessoriesItems).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val85;
		((UltraGridBase)this.ULGAccessoriesItems).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGAccessoriesItems).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val86).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val86).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val86).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val86).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val86).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val86, "appearance86");
		((UltraGridBase)this.ULGAccessoriesItems).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val86;
		((UltraGridBase)this.ULGAccessoriesItems).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGAccessoriesItems).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val87).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val87).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val87, "appearance87");
		((UltraGridBase)this.ULGAccessoriesItems).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val87;
		((UltraGridBase)this.ULGAccessoriesItems).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val88).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val88, "appearance88");
		((UltraGridBase)this.ULGAccessoriesItems).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val88;
		((System.Windows.Forms.Control)(object)this.ULGAccessoriesItems).Name = "ULGAccessoriesItems";
		this.ULGAccessoriesItems.CellListSelect += new CellEventHandler(ULGAccessoriesItems_CellListSelect);
		resources.ApplyResources(this.ultraTabPageControl6, "ultraTabPageControl6");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Controls.Add((System.Windows.Forms.Control)(object)this.ULGAdditionalsItems);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).Name = "ultraTabPageControl6";
		resources.ApplyResources(this.ULGAdditionalsItems, "ULGAdditionalsItems");
		((AppearanceBase)val89).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val89).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val89).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val89).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val89, "appearance89");
		((SpecialBoxBase)((UltraGridBase)this.ULGAdditionalsItems).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val89;
		((AppearanceBase)val90).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val90, "appearance90");
		((UltraGridBase)this.ULGAdditionalsItems).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val90;
		((SpecialBoxBase)((UltraGridBase)this.ULGAdditionalsItems).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val91).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val91).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val91).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val91).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val91, "appearance91");
		((UltraGridBase)this.ULGAdditionalsItems).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val91;
		((UltraGridBase)this.ULGAdditionalsItems).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGAdditionalsItems).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val92).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val92).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val92, "appearance92");
		((UltraGridBase)this.ULGAdditionalsItems).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val92;
		((AppearanceBase)val93).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val93).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val93, "appearance93");
		((UltraGridBase)this.ULGAdditionalsItems).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val93;
		((UltraGridBase)this.ULGAdditionalsItems).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGAdditionalsItems).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val94).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val94, "appearance94");
		((UltraGridBase)this.ULGAdditionalsItems).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val94;
		((AppearanceBase)val95).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val95, "appearance95");
		((AppearanceBase)val95).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGAdditionalsItems).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val95;
		((UltraGridBase)this.ULGAdditionalsItems).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGAdditionalsItems).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val96).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val96).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val96).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val96).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val96).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val96, "appearance96");
		((UltraGridBase)this.ULGAdditionalsItems).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val96;
		((UltraGridBase)this.ULGAdditionalsItems).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGAdditionalsItems).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val97).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val97).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val97, "appearance97");
		((UltraGridBase)this.ULGAdditionalsItems).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val97;
		((UltraGridBase)this.ULGAdditionalsItems).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val98).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val98, "appearance98");
		((UltraGridBase)this.ULGAdditionalsItems).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val98;
		((System.Windows.Forms.Control)(object)this.ULGAdditionalsItems).Name = "ULGAdditionalsItems";
		this.ULGAdditionalsItems.CellListSelect += new CellEventHandler(ULGAdditionalsItems_CellListSelect);
		resources.ApplyResources(this.ultraTabPageControl7, "ultraTabPageControl7");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.btnEINVExportItems);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.txtEINVItemNameCode);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblEINVItemNameCode);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.cboEINVItemGPC);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblEINVItemGPC);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.cboEINVItemNameType);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.lblEINVItemNameType);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Name = "ultraTabPageControl7";
		resources.ApplyResources(this.btnEINVExportItems, "btnEINVExportItems");
		((System.Windows.Forms.Control)(object)this.btnEINVExportItems).Name = "btnEINVExportItems";
		((System.Windows.Forms.Control)(object)this.btnEINVExportItems).Click += new System.EventHandler(btnEINVExportItems_Click);
		resources.ApplyResources(this.txtEINVItemNameCode, "txtEINVItemNameCode");
		((System.Windows.Forms.Control)(object)this.txtEINVItemNameCode).Name = "txtEINVItemNameCode";
		resources.ApplyResources(this.lblEINVItemNameCode, "lblEINVItemNameCode");
		((AppearanceBase)val99).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val99).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val99, "appearance99");
		((ControlBase)this.lblEINVItemNameCode).Appearance = (AppearanceBase)(object)val99;
		this.lblEINVItemNameCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEINVItemNameCode).Name = "lblEINVItemNameCode";
		((ControlBase)this.lblEINVItemNameCode).WrapText = false;
		resources.ApplyResources(this.cboEINVItemGPC, "cboEINVItemGPC");
		this.cboEINVItemGPC.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboEINVItemGPC).Name = "cboEINVItemGPC";
		resources.ApplyResources(this.lblEINVItemGPC, "lblEINVItemGPC");
		this.lblEINVItemGPC.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEINVItemGPC).Name = "lblEINVItemGPC";
		((ControlBase)this.lblEINVItemGPC).WrapText = false;
		resources.ApplyResources(this.cboEINVItemNameType, "cboEINVItemNameType");
		this.cboEINVItemNameType.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboEINVItemNameType).Name = "cboEINVItemNameType";
		((TextEditorControlBase)this.cboEINVItemNameType).ValueChanged += new System.EventHandler(cboEINVItemNameType_ValueChanged);
		resources.ApplyResources(this.lblEINVItemNameType, "lblEINVItemNameType");
		this.lblEINVItemNameType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEINVItemNameType).Name = "lblEINVItemNameType";
		((ControlBase)this.lblEINVItemNameType).WrapText = false;
		resources.ApplyResources(this.ultraTabPageControl8, "ultraTabPageControl8");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).Controls.Add((System.Windows.Forms.Control)(object)this.cboGrowthTax);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).Controls.Add((System.Windows.Forms.Control)(object)this.lblGrowthTax);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).Controls.Add((System.Windows.Forms.Control)(object)this.cboTableTax);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).Controls.Add((System.Windows.Forms.Control)(object)this.lblTableTax);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).Controls.Add((System.Windows.Forms.Control)(object)this.cboTax);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).Controls.Add((System.Windows.Forms.Control)(object)this.lblTax);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).Name = "ultraTabPageControl8";
		resources.ApplyResources(this.cboGrowthTax, "cboGrowthTax");
		((System.Windows.Forms.Control)(object)this.cboGrowthTax).Name = "cboGrowthTax";
		resources.ApplyResources(this.lblGrowthTax, "lblGrowthTax");
		((AppearanceBase)val100).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val100).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val100, "appearance100");
		((ControlBase)this.lblGrowthTax).Appearance = (AppearanceBase)(object)val100;
		this.lblGrowthTax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGrowthTax).Name = "lblGrowthTax";
		((ControlBase)this.lblGrowthTax).WrapText = false;
		resources.ApplyResources(this.cboTableTax, "cboTableTax");
		((System.Windows.Forms.Control)(object)this.cboTableTax).Name = "cboTableTax";
		resources.ApplyResources(this.lblTableTax, "lblTableTax");
		((AppearanceBase)val101).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val101).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val101, "appearance101");
		((ControlBase)this.lblTableTax).Appearance = (AppearanceBase)(object)val101;
		this.lblTableTax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTableTax).Name = "lblTableTax";
		((ControlBase)this.lblTableTax).WrapText = false;
		resources.ApplyResources(this.cboTax, "cboTax");
		((System.Windows.Forms.Control)(object)this.cboTax).Name = "cboTax";
		resources.ApplyResources(this.lblTax, "lblTax");
		((AppearanceBase)val102).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val102).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val102, "appearance102");
		((ControlBase)this.lblTax).Appearance = (AppearanceBase)(object)val102;
		this.lblTax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTax).Name = "lblTax";
		((ControlBase)this.lblTax).WrapText = false;
		resources.ApplyResources(this.pnlCheckType, "pnlCheckType");
		((AppearanceBase)val103).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val103, "appearance103");
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val103;
		resources.ApplyResources(this.pnlCheckType.ClientArea, "pnlCheckType.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsRecipe);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsService);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsItem);
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
		resources.ApplyResources(this.rbIsRecipe, "rbIsRecipe");
		this.rbIsRecipe.BackColor = System.Drawing.Color.Transparent;
		this.rbIsRecipe.ForeColor = System.Drawing.Color.Navy;
		this.rbIsRecipe.Name = "rbIsRecipe";
		this.rbIsRecipe.UseVisualStyleBackColor = false;
		this.rbIsRecipe.CheckedChanged += new System.EventHandler(rbIsRecipe_CheckedChanged);
		resources.ApplyResources(this.rbIsService, "rbIsService");
		this.rbIsService.BackColor = System.Drawing.Color.Transparent;
		this.rbIsService.ForeColor = System.Drawing.Color.Navy;
		this.rbIsService.Name = "rbIsService";
		this.rbIsService.UseVisualStyleBackColor = false;
		this.rbIsService.CheckedChanged += new System.EventHandler(rbIsService_CheckedChanged);
		resources.ApplyResources(this.rbIsItem, "rbIsItem");
		this.rbIsItem.BackColor = System.Drawing.Color.Transparent;
		this.rbIsItem.ForeColor = System.Drawing.Color.Navy;
		this.rbIsItem.Name = "rbIsItem";
		this.rbIsItem.UseVisualStyleBackColor = false;
		this.rbIsItem.CheckedChanged += new System.EventHandler(rbIsItem_CheckedChanged);
		resources.ApplyResources(this.btnCostCenterSearch, "btnCostCenterSearch");
		((AppearanceBase)val104).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val104, "appearance104");
		((ControlBase)this.btnCostCenterSearch).Appearance = (AppearanceBase)(object)val104;
		((System.Windows.Forms.Control)(object)this.btnCostCenterSearch).Name = "btnCostCenterSearch";
		((System.Windows.Forms.Control)(object)this.btnCostCenterSearch).Click += new System.EventHandler(btnCostCenterSearch_Click);
		resources.ApplyResources(this.cboCostCenter, "cboCostCenter");
		this.cboCostCenter.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboCostCenter).Name = "cboCostCenter";
		resources.ApplyResources(this.lblCostCenter, "lblCostCenter");
		this.lblCostCenter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCostCenter).Name = "lblCostCenter";
		((ControlBase)this.lblCostCenter).WrapText = false;
		resources.ApplyResources(this.chkCanModSalesPrice, "chkCanModSalesPrice");
		((AppearanceBase)val105).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val105, "appearance105");
		((UltraToggleEditorBase)this.chkCanModSalesPrice).Appearance = (AppearanceBase)(object)val105;
		((System.Windows.Forms.Control)(object)this.chkCanModSalesPrice).Name = "chkCanModSalesPrice";
		resources.ApplyResources(this.chkIsActive, "chkIsActive");
		((AppearanceBase)val106).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val106, "appearance106");
		((UltraToggleEditorBase)this.chkIsActive).Appearance = (AppearanceBase)(object)val106;
		((UltraToggleEditorBase)this.chkIsActive).Checked = true;
		((UltraToggleEditorBase)this.chkIsActive).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsActive).Name = "chkIsActive";
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblReceivingBank, "lblReceivingBank");
		((AppearanceBase)val107).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val107).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val107, "appearance107");
		((ControlBase)this.lblReceivingBank).Appearance = (AppearanceBase)(object)val107;
		this.lblReceivingBank.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReceivingBank).Name = "lblReceivingBank";
		((ControlBase)this.lblReceivingBank).WrapText = false;
		resources.ApplyResources(this.tabItemType, "tabItemType");
		((AppearanceBase)val108).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val108, "appearance108");
		((AppearanceBase)val108).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.tabItemType).Appearance = (AppearanceBase)(object)val108;
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.tabItem);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.tabService);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.tabRecipe);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl1);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl4);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl5);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl6);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl7);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl8);
		((System.Windows.Forms.Control)(object)this.tabItemType).Name = "tabItemType";
		((UltraTabControlBase)this.tabItemType).SharedControlsPage = this.ultraTabSharedControlsPage1;
		((UltraTabControlBase)this.tabItemType).TabOrientation = (TabOrientation)2;
		((KeyedSubObjectBase)val109).Key = "Item";
		val109.TabPage = this.tabItem;
		resources.ApplyResources(val109, "ultraTab2");
		val109.Visible = false;
		((SubObjectBase)val109).ForceApplyResources = "";
		((KeyedSubObjectBase)val110).Key = "Service";
		val110.TabPage = this.tabService;
		resources.ApplyResources(val110, "ultraTab1");
		val110.Visible = false;
		((SubObjectBase)val110).ForceApplyResources = "";
		((KeyedSubObjectBase)val111).Key = "Recipe";
		val111.TabPage = this.tabRecipe;
		resources.ApplyResources(val111, "ultraTab3");
		val111.Visible = false;
		((SubObjectBase)val111).ForceApplyResources = "";
		((KeyedSubObjectBase)val112).Key = "Prices";
		val112.TabPage = this.ultraTabPageControl1;
		resources.ApplyResources(val112, "ultraTab4");
		((SubObjectBase)val112).ForceApplyResources = "";
		((KeyedSubObjectBase)val113).Key = "Accounts";
		val113.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val113, "ultraTab5");
		((SubObjectBase)val113).ForceApplyResources = "";
		((KeyedSubObjectBase)val114).Key = "Levels";
		val114.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val114, "ultraTab6");
		val114.Visible = false;
		((SubObjectBase)val114).ForceApplyResources = "";
		((KeyedSubObjectBase)val115).Key = "Classification";
		val115.TabPage = this.ultraTabPageControl4;
		resources.ApplyResources(val115, "ultraTab7");
		((SubObjectBase)val115).ForceApplyResources = "";
		((KeyedSubObjectBase)val116).Key = "Accessories";
		val116.TabPage = this.ultraTabPageControl5;
		resources.ApplyResources(val116, "ultraTab8");
		val116.Visible = false;
		((SubObjectBase)val116).ForceApplyResources = "";
		((KeyedSubObjectBase)val117).Key = "Additionals";
		val117.TabPage = this.ultraTabPageControl6;
		resources.ApplyResources(val117, "ultraTab9");
		val117.Visible = false;
		((SubObjectBase)val117).ForceApplyResources = "";
		((KeyedSubObjectBase)val118).Key = "EInvoice";
		val118.TabPage = this.ultraTabPageControl7;
		resources.ApplyResources(val118, "ultraTab10");
		val118.Visible = false;
		((SubObjectBase)val118).ForceApplyResources = "";
		((KeyedSubObjectBase)val119).Key = "Taxes";
		val119.TabPage = this.ultraTabPageControl8;
		resources.ApplyResources(val119, "ultraTab11");
		((SubObjectBase)val119).ForceApplyResources = "";
		((UltraTabControlBase)this.tabItemType).Tabs.AddRange((UltraTab[])(object)new UltraTab[11]
		{
			val109, val110, val111, val112, val113, val114, val115, val116, val117, val118,
			val119
		});
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		resources.ApplyResources(this.txtBarCode, "txtBarCode");
		((System.Windows.Forms.Control)(object)this.txtBarCode).Name = "txtBarCode";
		((System.Windows.Forms.Control)(object)this.txtBarCode).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtBarCode_KeyPress);
		resources.ApplyResources(this.lblBarCode, "lblBarCode");
		((AppearanceBase)val120).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val120).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val120, "appearance109");
		((ControlBase)this.lblBarCode).Appearance = (AppearanceBase)(object)val120;
		this.lblBarCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBarCode).Name = "lblBarCode";
		((ControlBase)this.lblBarCode).WrapText = false;
		this.ofdItemPic.FileName = "openFileDialog1";
		resources.ApplyResources(this.ofdItemPic, "ofdItemPic");
		resources.ApplyResources(this.modifyGroupItemsToolStripMenuItem, "modifyGroupItemsToolStripMenuItem");
		this.modifyGroupItemsToolStripMenuItem.Name = "modifyGroupItemsToolStripMenuItem";
		resources.ApplyResources(this.modifyGroupItemsToolStripMenuItem1, "modifyGroupItemsToolStripMenuItem1");
		this.modifyGroupItemsToolStripMenuItem1.Name = "modifyGroupItemsToolStripMenuItem1";
		resources.ApplyResources(this.modifyGroupItemsToolStripMenuItem2, "modifyGroupItemsToolStripMenuItem2");
		this.modifyGroupItemsToolStripMenuItem2.Name = "modifyGroupItemsToolStripMenuItem2";
		resources.ApplyResources(this.modifyGroupItemsToolStripMenuItem3, "modifyGroupItemsToolStripMenuItem3");
		this.modifyGroupItemsToolStripMenuItem3.Name = "modifyGroupItemsToolStripMenuItem3";
		this.modifyGroupItemsToolStripMenuItem3.Click += new System.EventHandler(modifyGroupItemsToolStripMenuItem3_Click);
		resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
		this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(18, 18);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.modifyGroupItemsToolStripMenuItem4, this.changeClassificationsToolStripMenuItem, this.deleteToolStripMenuItem, this.changeParentToolStripMenuItem });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.ShowImageMargin = false;
		resources.ApplyResources(this.modifyGroupItemsToolStripMenuItem4, "modifyGroupItemsToolStripMenuItem4");
		this.modifyGroupItemsToolStripMenuItem4.Name = "modifyGroupItemsToolStripMenuItem4";
		this.modifyGroupItemsToolStripMenuItem4.Click += new System.EventHandler(modifyGroupItemsToolStripMenuItem4_Click);
		resources.ApplyResources(this.changeClassificationsToolStripMenuItem, "changeClassificationsToolStripMenuItem");
		this.changeClassificationsToolStripMenuItem.Name = "changeClassificationsToolStripMenuItem";
		this.changeClassificationsToolStripMenuItem.Click += new System.EventHandler(changeClassificationsToolStripMenuItem_Click);
		resources.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
		this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
		this.deleteToolStripMenuItem.Click += new System.EventHandler(deleteToolStripMenuItem_Click);
		resources.ApplyResources(this.changeParentToolStripMenuItem, "changeParentToolStripMenuItem");
		this.changeParentToolStripMenuItem.Name = "changeParentToolStripMenuItem";
		this.changeParentToolStripMenuItem.Click += new System.EventHandler(changeParentToolStripMenuItem_Click);
		resources.ApplyResources(this.chkHideFromReports, "chkHideFromReports");
		((AppearanceBase)val121).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val121, "appearance110");
		((UltraToggleEditorBase)this.chkHideFromReports).Appearance = (AppearanceBase)(object)val121;
		((System.Windows.Forms.Control)(object)this.chkHideFromReports).Name = "chkHideFromReports";
		resources.ApplyResources(this.chkCanModPurchasePrice, "chkCanModPurchasePrice");
		((AppearanceBase)val122).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val122, "appearance111");
		((UltraToggleEditorBase)this.chkCanModPurchasePrice).Appearance = (AppearanceBase)(object)val122;
		((System.Windows.Forms.Control)(object)this.chkCanModPurchasePrice).Name = "chkCanModPurchasePrice";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCostCenterSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCostCenter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCostCenter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.tabItemType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReceivingBank);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkHideFromReports);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsActive);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkCanModPurchasePrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkCanModSalesPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBarCode);
		base.Name = "frmItemsTree";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkCanModSalesPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkCanModPurchasePrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsActive, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkHideFromReports, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReceivingBank, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.tabItemType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.label2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.label1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.label3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.treeChart, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCostCenter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAddRoot, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCostCenter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCostCenterSearch, 0);
		((System.ComponentModel.ISupportInitialize)base.dtChart).EndInit();
		((System.ComponentModel.ISupportInitialize)base.treeChart).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtName).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.tabItem).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.tabItem).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.chkUsePOSNumPad).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCyl).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVolume).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetWeight).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrowthWeight).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSph).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboColorCategory).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSizeCategory).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPrinterName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSupplierItemCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPrepareTime).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkEnforceBatchNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsProductionItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboStores).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsDirectItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsSalesItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnitGroup).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtValidityDays).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTypes).EndInit();
		((System.Windows.Forms.Control)(object)this.tabService).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.tabService).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cboServiceAccount).EndInit();
		((System.Windows.Forms.Control)(object)this.tabRecipe).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraPanel3.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraPanel3.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.ultraPanel3).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGRecipeItems).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.chkIsUnitPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGPrices).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraPanel1.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraPanel1.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.ultraPanel1).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cboDepartmentIssueAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPurchaseReturnsAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPurchaseAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCostOfSalesAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesReturnsAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesAccount).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGLevels).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraPanel2.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraPanel2.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.ultraPanel2).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cboColor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSize).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSuppliers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboShape).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboModel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaterial2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaterial1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceCategory).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSeason).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAgeGroup).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBrand).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl5).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtAccessoriesCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGAccessoriesItems).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl6).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGAdditionalsItems).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtEINVItemNameCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboEINVItemGPC).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboEINVItemNameType).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cboGrowthTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTableTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.cboCostCenter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkCanModSalesPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tabItemType).EndInit();
		((System.Windows.Forms.Control)(object)this.tabItemType).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.chkHideFromReports).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkCanModPurchasePrice).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
