using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Lenses.MasterData;

public class frmSizeItemsGroups : frmBase
{
	private DataTable dtItemsTypes;

	private DataTable dtColors;

	private DataTable dtSizeCategory;

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

	private DataTable dtSuppliers;

	private DataTable dtUnitGroup;

	private DataTable dtUnits;

	private DataTable dtStores;

	private DataTable dtRoots;

	private DataTable dtGroups;

	private DataTable dtItemPrices;

	private DataTable dtItemStockLevels;

	private DataTable dtPricesTypes;

	private DataTable dtBranches;

	private ValueList vlPricesTypes = new ValueList();

	private ValueList vlBranches = new ValueList();

	private ValueList vlBranches2 = new ValueList();

	private IContainer components = null;

	public UltraLabel lblRoot;

	public UltraComboEditor cboRoot;

	public UltraComboEditor cboGroup;

	public UltraLabel lblGroup;

	public UltraLabel lblUnitGroup;

	public UltraComboEditor cboUnitGroup;

	public UltraLabel lblUnit;

	public UltraComboEditor cboUnit;

	public UltraComboEditor cboStore;

	public UltraLabel lblStore;

	public UltraButton btnSaveAndClose;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraButton btnCancel;

	public UltraButton btnSave;

	public UltraLabel lblTitle2;

	public UltraGrid ULGPrices;

	private UltraPanel ultraPanel1;

	private RadioButton rbPriceForEachBranches;

	private RadioButton rbPriceForAllBranches;

	public UltraGrid ULGLevels;

	private UltraPanel ultraPanel2;

	private RadioButton rbStockLevelForEachBranch;

	private RadioButton rbStockLevelsForAllBranches;

	public UltraLabel ultraLabel5;

	public UltraLabel ultraLabel6;

	private UltraComboEditor cboShape;

	private UltraLabel lblShape;

	private UltraComboEditor cboModel;

	private UltraLabel lblModel;

	private UltraComboEditor cboMaterial2;

	private UltraLabel lblMaterial2;

	private UltraComboEditor cboSizeCategory;

	private UltraLabel lblSizeCategory;

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

	private UltraComboEditor cboTypes;

	private UltraLabel lblTypes;

	protected internal CheckedListBox clbColors;

	protected internal CheckedListBox clbMaterials;

	public UltraLabel lblMaterial1;

	public UltraLabel lblColors;

	private UltraComboEditor cboSuppliers;

	private UltraLabel lblSuplliers;

	public frmSizeItemsGroups()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		InitializeComponent();
	}

	public override void CallButtons(KeyEventArgs e)
	{
		base.CallButtons(e);
		if (e.KeyCode == Keys.F1 && ((Control)(object)btnSave).Enabled && ((Control)(object)btnSave).Visible)
		{
			btnSave_Click(null, null);
		}
	}

	public override void PrepareData()
	{
		base.PrepareData();
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
		dtRoots = Items.FillGroups("-1", ",2,", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboRoot, dtRoots, "ItemID", "Name");
		dtUnitGroup = UnitsTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboUnitGroup, dtUnitGroup, "UnitTypeID", "UnitTypeName");
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtStores = Stores.FillCombo("-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboStore, dtStores, "StoreID", "StoreName");
		dtSizeCategory = ItemsSizeCategorys.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSizeCategory, dtSizeCategory, "ItemSizeCategoryID", "ItemSizeCategoryName");
		dtItemsTypes = ItemsTypes.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboTypes, dtItemsTypes, "ItemTypeID", GlobalVariables.IsArabic ? "ItemTypeNameAr" : "ItemTypeNameEn");
		dtAgeGroup = ItemsAgeGroups.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboAgeGroup, dtAgeGroup, "ItemAgeGroupID", GlobalVariables.IsArabic ? "ItemAgeGroupNameAr" : "ItemAgeGroupNameEn");
		dtClassification1 = ItemsFirstClassifications.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboClassification1, dtClassification1, "ItemFirstClassificationID", GlobalVariables.IsArabic ? "ItemFirstClassificationNameAr" : "ItemFirstClassificationNameEn");
		dtClassification2 = ItemsSecondClassifications.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboClassification2, dtClassification2, "ItemSecondClassificationID", GlobalVariables.IsArabic ? "ItemSecondClassificationNameAr" : "ItemSecondClassificationNameEn");
		dtClassification3 = ItemsThirdClassifications.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboClassification3, dtClassification3, "ItemThirdClassificationID", GlobalVariables.IsArabic ? "ItemThirdClassificationNameAr" : "ItemThirdClassificationNameEn");
		dtClassification4 = ItemsFourthClassifications.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboClassification4, dtClassification4, "ItemFourthClassificationID", GlobalVariables.IsArabic ? "ItemFourthClassificationNameAr" : "ItemFourthClassificationNameEn");
		dtBrand = ItemsBrands.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboBrand, dtBrand, "ItemBrandID", GlobalVariables.IsArabic ? "ItemBrandNameAr" : "ItemBrandNameEn");
		dtSeason = ItemsSeasons.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSeason, dtSeason, "ItemSeasonID", GlobalVariables.IsArabic ? "ItemSeasonNameAr" : "ItemSeasonNameEn");
		dtCountry = ItemsCountrys.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCountry, dtCountry, "ItemCountryID", GlobalVariables.IsArabic ? "ItemCountryNameAr" : "ItemCountryNameEn");
		dtPriceCategory = ItemsPriceCategorys.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboPriceCategory, dtPriceCategory, "ItemPriceCategoryID", GlobalVariables.IsArabic ? "ItemPriceCategoryNameAr" : "ItemPriceCategoryNameEn");
		dtMaterial2 = ItemsSecondMaterials.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboMaterial2, dtMaterial2, "ItemSecondMaterialID", GlobalVariables.IsArabic ? "ItemSecondMaterialNameAr" : "ItemSecondMaterialNameEn");
		dtShape = ItemsShapes.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboShape, dtShape, "ItemShapeID", GlobalVariables.IsArabic ? "ItemShapeNameAr" : "ItemShapeNameEn");
		dtModel = ItemsModels.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboModel, dtModel, "ItemModelID", GlobalVariables.IsArabic ? "ItemModelNameAr" : "ItemModelNameEn");
		dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		Main.Fillclb(clbColors, dtColors, "ColorID", "ColorCode");
		dtMaterial1 = ItemsFirstMaterials.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		Main.Fillclb(clbMaterials, dtMaterial1, "ItemFirstMaterialID", GlobalVariables.IsArabic ? "ItemFirstMaterialNameAr" : "ItemFirstMaterialNameEn");
		dtBranches = Branches.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlBranches.ValueListItems.Clear();
		vlBranches2.ValueListItems.Clear();
		for (int i = 0; i < dtBranches.Rows.Count; i++)
		{
			vlBranches.ValueListItems.Add(dtBranches.Rows[i]["BranchID"], dtBranches.Rows[i][GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn"].ToString());
			vlBranches2.ValueListItems.Add(dtBranches.Rows[i]["BranchID"], dtBranches.Rows[i][GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn"].ToString());
		}
		dtPricesTypes = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlPricesTypes.ValueListItems.Clear();
		for (int j = 0; j < dtPricesTypes.Rows.Count; j++)
		{
			vlPricesTypes.ValueListItems.Add(dtPricesTypes.Rows[j]["PriceTypeID"], dtPricesTypes.Rows[j]["PriceName"].ToString());
		}
		dtSuppliers = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.SupplierSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSuppliers, dtSuppliers, "SubAccountID", "SubAccountName");
		dtItemPrices = ItemsPrices.SelectByItemID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGridPrices();
		dtItemStockLevels = ItemsStockLevels.SelectByItemID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGridStockLevels();
		((Control)(object)btnSave).Enabled = true;
		((Control)(object)btnSaveAndClose).Enabled = true;
		((Control)(object)btnCancel).Enabled = true;
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
		}
	}

	public bool ValidateData()
	{
		if (cboUnitGroup.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار مجموعه الوحده", "Please select Unit Group");
			((TextEditorControlBase)cboUnitGroup).Focus();
			return false;
		}
		if (cboUnit.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار الوحده", "Please select Unit ");
			((TextEditorControlBase)cboUnit).Focus();
			return false;
		}
		if (cboStore.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار المخزن الإفتراضي", "Please select Default Store");
			((TextEditorControlBase)cboStore).Focus();
			return false;
		}
		if (cboRoot.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار المجموعه الإفتراضيه", "Please select Root");
			((TextEditorControlBase)cboRoot).Focus();
			return false;
		}
		if (((Control)(object)cboGroup).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال اسم المجموعه", "Please Enter Group Name");
			((TextEditorControlBase)cboGroup).Focus();
			return false;
		}
		if (cboSizeCategory.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار المقاس", "Please select Size Category ");
			((TextEditorControlBase)cboSizeCategory).Focus();
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
		return true;
	}

	public void Save()
	{
		GlobalVariables.QuestionMB.Show("هل تريد الحفظ ؟", "Do You Want To save ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			return;
		}
		string text = ",";
		for (int i = 0; i < clbColors.Items.Count; i++)
		{
			if (clbColors.GetItemChecked(i))
			{
				text = text + dtColors.Rows[i]["ColorID"].ToString() + ",";
			}
		}
		string text2 = ",";
		for (int j = 0; j < clbMaterials.Items.Count; j++)
		{
			if (clbMaterials.GetItemChecked(j))
			{
				text2 = text2 + dtMaterial1.Rows[j]["ItemFirstMaterialID"].ToString() + ",";
			}
		}
		DataTable dataTable = Items.LensesGenerateItemsSizeRange(((Control)(object)cboGroup).Text, ((TextEditorControlBase)cboRoot).Value.ToString(), ((TextEditorControlBase)cboUnit).Value.ToString(), ((TextEditorControlBase)cboUnitGroup).Value.ToString(), rbStockLevelsForAllBranches.Checked ? "1" : "0", ((TextEditorControlBase)cboStore).Value.ToString(), (cboTypes.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTypes).Value.ToString(), (cboAgeGroup.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAgeGroup).Value.ToString(), (cboClassification1.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClassification1).Value.ToString(), (cboClassification2.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClassification2).Value.ToString(), (cboClassification3.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClassification3).Value.ToString(), (cboClassification4.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClassification4).Value.ToString(), (cboBrand.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBrand).Value.ToString(), (cboSeason.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSeason).Value.ToString(), (cboCountry.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCountry).Value.ToString(), (cboPriceCategory.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPriceCategory).Value.ToString(), (cboMaterial2.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboMaterial2).Value.ToString(), (cboShape.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboShape).Value.ToString(), (cboModel.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboModel).Value.ToString(), (cboSizeCategory.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSizeCategory).Value.ToString(), (cboSuppliers.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSuppliers).Value.ToString(), text, text2, rbPriceForAllBranches.Checked ? "1" : "0", GlobalVariables.CurrentBranchID.ToString(), GlobalVariables.UserID, IsFromServer: true);
		string text3 = "";
		for (int k = 0; k < dataTable.Rows.Count; k++)
		{
			for (int l = 0; l < dtItemPrices.Rows.Count; l++)
			{
				text3 += " Exec SC_ItemsPrices_Insert_Update ";
				text3 += "-1,";
				text3 = text3 + dataTable.Rows[k]["ItemID"].ToString() + ",";
				text3 = text3 + dtItemPrices.Rows[l]["PriceTypeID"].ToString() + ",";
				text3 = text3 + dtItemPrices.Rows[l]["Price"].ToString() + ",";
				text3 += "0,";
				text3 += (rbPriceForAllBranches.Checked ? "Null," : (dtItemPrices.Rows[l]["BranchID"].ToString() + ","));
				text3 = text3 + GlobalVariables.UserID + "; ";
			}
		}
		for (int m = 0; m < dataTable.Rows.Count; m++)
		{
			for (int n = 0; n < dtItemStockLevels.Rows.Count; n++)
			{
				text3 += " Exec SC_ItemsStockLevels_Insert_Update ";
				text3 += "-1,";
				text3 = text3 + dataTable.Rows[m]["ItemID"].ToString() + ",";
				text3 = text3 + dtItemStockLevels.Rows[n]["MinLevel"].ToString() + ",";
				text3 = text3 + dtItemStockLevels.Rows[n]["ReOrderLevel"].ToString() + ",";
				text3 = text3 + dtItemStockLevels.Rows[n]["MaxLevel"].ToString() + ",";
				text3 += "0,";
				text3 += (rbStockLevelsForAllBranches.Checked ? "Null," : (dtItemStockLevels.Rows[n]["BranchID"].ToString() + ","));
				text3 = text3 + GlobalVariables.UserID + "; ";
			}
		}
		if (text3 != "")
		{
			Main.SyncExecuteNonQuery(text3);
		}
		GlobalVariables.InformationMB.Show("تم الحفظ بنجاح", "Items Saved");
		cboRoot_ValueChanged(null, null);
		SelectAllListBoxItems(Checked: false, clbColors);
		SelectAllListBoxItems(Checked: false, clbMaterials);
		((TextEditorControlBase)cboGroup).Focus();
		((TextEditorControlBase)cboGroup).SelectAll();
	}

	public void SelectAllListBoxItems(bool Checked, CheckedListBox lst)
	{
		for (int i = 0; i < lst.Items.Count; i++)
		{
			lst.SetItemChecked(i, Checked);
		}
	}

	private void cboRoot_ValueChanged(object sender, EventArgs e)
	{
		if (cboRoot.SelectedIndex != -1)
		{
			dtGroups = Items.FillGroups(((TextEditorControlBase)cboRoot).Value.ToString(), ",3,", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboGroup, dtGroups, "ItemID", "Name");
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

	private void FillGridPrices()
	{
		dtItemPrices.Rows.Clear();
		if (rbPriceForAllBranches.Checked)
		{
			for (int i = 0; i < dtPricesTypes.Rows.Count; i++)
			{
				DataRow dataRow = dtItemPrices.NewRow();
				dataRow["ItemPriceID"] = -1;
				dataRow["ItemID"] = -1;
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
					dataRow2["ItemID"] = -1;
					dataRow2["PriceTypeID"] = dtPricesTypes.Rows[k]["PriceTypeID"];
					dataRow2["Price"] = 0;
					dataRow2["Deleted"] = false;
					dataRow2["BranchID"] = dtBranches.Rows[j]["BranchID"];
					dtItemPrices.Rows.Add(dataRow2);
				}
			}
		}
		InitGridPrices();
	}

	private void InitGridPrices()
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
		((HeaderBase)((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["ReOrderLevel"].Header).Caption = (GlobalVariables.IsArabic ? "اعدة الطلب" : "Reorder");
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

	private void btnClose_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (ValidateData())
		{
			Save();
		}
	}

	private void btnSaveAndClose_Click(object sender, EventArgs e)
	{
		if (ValidateData())
		{
			Save();
			Dispose();
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void ULGPrices_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGPrices.ActiveCell.Column).Key != "Price")
		{
			((GridItemBase)((UltraGridBase)ULGPrices).ActiveRow).Selected = true;
		}
	}

	private void ULGLevels_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGLevels.ActiveCell.Column).Key == "BranchID")
		{
			((GridItemBase)((UltraGridBase)ULGLevels).ActiveRow).Selected = true;
		}
	}

	private void ULGLevels_Enter(object sender, EventArgs e)
	{
		int num = -1;
		for (int num2 = ((DisposableObjectCollectionBase)((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns).Count - 1; num2 >= 0; num2--)
		{
			if (num == -1 && !((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns[num2].Hidden)
			{
				num = num2;
				break;
			}
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGLevels).Rows).Count > 0)
		{
			((UltraGridBase)ULGLevels).Rows[0].Cells[num].Activate();
			ULGLevels.PerformAction((UltraGridAction)24);
		}
	}

	private void ULGPrices_Enter(object sender, EventArgs e)
	{
		int num = -1;
		for (int num2 = ((DisposableObjectCollectionBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns).Count - 1; num2 >= 0; num2--)
		{
			if (num == -1 && !((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns[num2].Hidden)
			{
				num = num2;
				break;
			}
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGPrices).Rows).Count > 0)
		{
			((UltraGridBase)ULGPrices).Rows[0].Cells[num].Activate();
			ULGPrices.PerformAction((UltraGridAction)24);
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
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected O, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Expected O, but got Unknown
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Expected O, but got Unknown
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Expected O, but got Unknown
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Expected O, but got Unknown
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Expected O, but got Unknown
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Expected O, but got Unknown
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Expected O, but got Unknown
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Expected O, but got Unknown
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Expected O, but got Unknown
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Expected O, but got Unknown
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Expected O, but got Unknown
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Expected O, but got Unknown
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Expected O, but got Unknown
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Expected O, but got Unknown
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Expected O, but got Unknown
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Expected O, but got Unknown
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Expected O, but got Unknown
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Expected O, but got Unknown
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Expected O, but got Unknown
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Expected O, but got Unknown
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Expected O, but got Unknown
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Expected O, but got Unknown
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Expected O, but got Unknown
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Expected O, but got Unknown
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Expected O, but got Unknown
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Expected O, but got Unknown
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Expected O, but got Unknown
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Expected O, but got Unknown
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Expected O, but got Unknown
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Expected O, but got Unknown
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Expected O, but got Unknown
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Expected O, but got Unknown
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Expected O, but got Unknown
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Expected O, but got Unknown
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Expected O, but got Unknown
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Expected O, but got Unknown
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Expected O, but got Unknown
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Expected O, but got Unknown
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Expected O, but got Unknown
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Expected O, but got Unknown
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Expected O, but got Unknown
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Expected O, but got Unknown
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Expected O, but got Unknown
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Expected O, but got Unknown
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Expected O, but got Unknown
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Expected O, but got Unknown
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Expected O, but got Unknown
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Expected O, but got Unknown
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Expected O, but got Unknown
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Expected O, but got Unknown
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Expected O, but got Unknown
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Expected O, but got Unknown
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Expected O, but got Unknown
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Expected O, but got Unknown
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Expected O, but got Unknown
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Expected O, but got Unknown
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Expected O, but got Unknown
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Expected O, but got Unknown
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Expected O, but got Unknown
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Expected O, but got Unknown
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Expected O, but got Unknown
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.MasterData.frmSizeItemsGroups));
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
		this.ultraPanel1 = new UltraPanel();
		this.rbPriceForEachBranches = new System.Windows.Forms.RadioButton();
		this.rbPriceForAllBranches = new System.Windows.Forms.RadioButton();
		this.ultraPanel2 = new UltraPanel();
		this.rbStockLevelForEachBranch = new System.Windows.Forms.RadioButton();
		this.rbStockLevelsForAllBranches = new System.Windows.Forms.RadioButton();
		this.lblRoot = new UltraLabel();
		this.cboRoot = new UltraComboEditor();
		this.cboGroup = new UltraComboEditor();
		this.lblGroup = new UltraLabel();
		this.lblUnitGroup = new UltraLabel();
		this.cboUnitGroup = new UltraComboEditor();
		this.lblUnit = new UltraLabel();
		this.cboUnit = new UltraComboEditor();
		this.cboStore = new UltraComboEditor();
		this.lblStore = new UltraLabel();
		this.btnSaveAndClose = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.btnCancel = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.ULGPrices = new UltraGrid();
		this.ULGLevels = new UltraGrid();
		this.ultraLabel5 = new UltraLabel();
		this.ultraLabel6 = new UltraLabel();
		this.cboShape = new UltraComboEditor();
		this.lblShape = new UltraLabel();
		this.cboModel = new UltraComboEditor();
		this.lblModel = new UltraLabel();
		this.cboMaterial2 = new UltraComboEditor();
		this.lblMaterial2 = new UltraLabel();
		this.cboSizeCategory = new UltraComboEditor();
		this.lblSizeCategory = new UltraLabel();
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
		this.cboTypes = new UltraComboEditor();
		this.lblTypes = new UltraLabel();
		this.clbColors = new System.Windows.Forms.CheckedListBox();
		this.clbMaterials = new System.Windows.Forms.CheckedListBox();
		this.lblMaterial1 = new UltraLabel();
		this.lblColors = new UltraLabel();
		this.cboSuppliers = new UltraComboEditor();
		this.lblSuplliers = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraPanel1.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.ultraPanel1).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.ultraPanel2.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.ultraPanel2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboRoot).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboGroup).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnitGroup).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGPrices).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGLevels).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboShape).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboModel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaterial2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSizeCategory).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceCategory).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSeason).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAgeGroup).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBrand).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTypes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSuppliers).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ultraPanel1, "ultraPanel1");
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val, "appearance1");
		this.ultraPanel1.Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(this.ultraPanel1.ClientArea, "ultraPanel1.ClientArea");
		((System.Windows.Forms.Control)(object)this.ultraPanel1.ClientArea).Controls.Add(this.rbPriceForEachBranches);
		((System.Windows.Forms.Control)(object)this.ultraPanel1.ClientArea).Controls.Add(this.rbPriceForAllBranches);
		((System.Windows.Forms.Control)(object)this.ultraPanel1).Name = "ultraPanel1";
		((System.Windows.Forms.Control)(object)this.ultraPanel1).TabStop = false;
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
		resources.ApplyResources(this.ultraPanel2, "ultraPanel2");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val2, "appearance2");
		this.ultraPanel2.Appearance = (AppearanceBase)(object)val2;
		resources.ApplyResources(this.ultraPanel2.ClientArea, "ultraPanel2.ClientArea");
		((System.Windows.Forms.Control)(object)this.ultraPanel2.ClientArea).Controls.Add(this.rbStockLevelForEachBranch);
		((System.Windows.Forms.Control)(object)this.ultraPanel2.ClientArea).Controls.Add(this.rbStockLevelsForAllBranches);
		((System.Windows.Forms.Control)(object)this.ultraPanel2).Name = "ultraPanel2";
		((System.Windows.Forms.Control)(object)this.ultraPanel2).TabStop = false;
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
		resources.ApplyResources(this.lblRoot, "lblRoot");
		((AppearanceBase)val3).Image = resources.GetObject("appearance3.Image");
		resources.ApplyResources(val3, "appearance3");
		((AppearanceBase)val3).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblRoot).Appearance = (AppearanceBase)(object)val3;
		this.lblRoot.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRoot).Name = "lblRoot";
		((ControlBase)this.lblRoot).WrapText = false;
		resources.ApplyResources(this.cboRoot, "cboRoot");
		this.cboRoot.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboRoot).Name = "cboRoot";
		((TextEditorControlBase)this.cboRoot).Nullable = false;
		((TextEditorControlBase)this.cboRoot).ValueChanged += new System.EventHandler(cboRoot_ValueChanged);
		resources.ApplyResources(this.cboGroup, "cboGroup");
		this.cboGroup.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboGroup).Name = "cboGroup";
		((TextEditorControlBase)this.cboGroup).Nullable = false;
		resources.ApplyResources(this.lblGroup, "lblGroup");
		resources.ApplyResources(val4, "appearance4");
		((AppearanceBase)val4).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblGroup).Appearance = (AppearanceBase)(object)val4;
		this.lblGroup.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGroup).Name = "lblGroup";
		((ControlBase)this.lblGroup).WrapText = false;
		resources.ApplyResources(this.lblUnitGroup, "lblUnitGroup");
		((AppearanceBase)val5).Image = resources.GetObject("appearance5.Image");
		resources.ApplyResources(val5, "appearance5");
		((AppearanceBase)val5).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblUnitGroup).Appearance = (AppearanceBase)(object)val5;
		this.lblUnitGroup.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUnitGroup).Name = "lblUnitGroup";
		((ControlBase)this.lblUnitGroup).WrapText = false;
		resources.ApplyResources(this.cboUnitGroup, "cboUnitGroup");
		this.cboUnitGroup.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboUnitGroup).Name = "cboUnitGroup";
		((TextEditorControlBase)this.cboUnitGroup).Nullable = false;
		((TextEditorControlBase)this.cboUnitGroup).ValueChanged += new System.EventHandler(cboUnitGroup_ValueChanged);
		resources.ApplyResources(this.lblUnit, "lblUnit");
		((AppearanceBase)val6).Image = resources.GetObject("appearance6.Image");
		resources.ApplyResources(val6, "appearance6");
		((AppearanceBase)val6).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblUnit).Appearance = (AppearanceBase)(object)val6;
		this.lblUnit.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUnit).Name = "lblUnit";
		((ControlBase)this.lblUnit).WrapText = false;
		resources.ApplyResources(this.cboUnit, "cboUnit");
		this.cboUnit.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboUnit).Name = "cboUnit";
		((TextEditorControlBase)this.cboUnit).Nullable = false;
		resources.ApplyResources(this.cboStore, "cboStore");
		this.cboStore.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboStore).Name = "cboStore";
		((TextEditorControlBase)this.cboStore).Nullable = false;
		resources.ApplyResources(this.lblStore, "lblStore");
		((AppearanceBase)val7).Image = resources.GetObject("appearance7.Image");
		resources.ApplyResources(val7, "appearance7");
		((AppearanceBase)val7).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblStore).Appearance = (AppearanceBase)(object)val7;
		this.lblStore.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStore).Name = "lblStore";
		((ControlBase)this.lblStore).WrapText = false;
		resources.ApplyResources(this.btnSaveAndClose, "btnSaveAndClose");
		((AppearanceBase)val8).Image = resources.GetObject("appearance8.Image");
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.btnSaveAndClose).Appearance = (AppearanceBase)(object)val8;
		((ControlBase)this.btnSaveAndClose).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSaveAndClose).Name = "btnSaveAndClose";
		((System.Windows.Forms.Control)(object)this.btnSaveAndClose).Click += new System.EventHandler(btnSaveAndClose_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val9).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val9).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val10).Image = resources.GetObject("appearance10.Image");
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val10;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((AppearanceBase)val11).Image = resources.GetObject("appearance11.Image");
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.btnCancel).Appearance = (AppearanceBase)(object)val11;
		((ControlBase)this.btnCancel).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val12).Image = resources.GetObject("appearance12.Image");
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val12;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val13).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val13, "appearance13");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.ULGPrices, "ULGPrices");
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val14).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val14).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val14, "appearance14");
		((SpecialBoxBase)((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val14;
		((AppearanceBase)val15).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val15, "appearance15");
		((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val15;
		((SpecialBoxBase)((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val16).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val16).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val16).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val16).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val16, "appearance16");
		((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val16;
		((UltraGridBase)this.ULGPrices).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGPrices).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val17).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val17).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val17, "appearance17");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val17;
		((AppearanceBase)val18).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val18).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val18, "appearance18");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val18;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val19).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val19, "appearance19");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val19;
		((AppearanceBase)val20).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val20, "appearance20");
		((AppearanceBase)val20).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val20;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val21).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val21).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val21).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val21).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val21).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val21, "appearance21");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val21;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val22).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val22).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val22, "appearance22");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val22;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val23).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val23, "appearance23");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val23;
		((System.Windows.Forms.Control)(object)this.ULGPrices).Name = "ULGPrices";
		this.ULGPrices.AfterEnterEditMode += new System.EventHandler(ULGPrices_AfterEnterEditMode);
		((System.Windows.Forms.Control)(object)this.ULGPrices).Enter += new System.EventHandler(ULGPrices_Enter);
		resources.ApplyResources(this.ULGLevels, "ULGLevels");
		((AppearanceBase)val24).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val24).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val24).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val24).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val24, "appearance24");
		((SpecialBoxBase)((UltraGridBase)this.ULGLevels).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val24;
		((AppearanceBase)val25).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val25, "appearance25");
		((UltraGridBase)this.ULGLevels).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val25;
		((SpecialBoxBase)((UltraGridBase)this.ULGLevels).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val26).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val26).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val26).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val26).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val26, "appearance26");
		((UltraGridBase)this.ULGLevels).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val26;
		((UltraGridBase)this.ULGLevels).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGLevels).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val27).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val27).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val27, "appearance27");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val27;
		((AppearanceBase)val28).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val28).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val28, "appearance28");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val28;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val29).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val29, "appearance29");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val29;
		((AppearanceBase)val30).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val30, "appearance30");
		((AppearanceBase)val30).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val30;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val31).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val31).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val31).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val31).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val31).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val31, "appearance31");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val31;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val32).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val32).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val32, "appearance32");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val32;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val33).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val33, "appearance33");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val33;
		((System.Windows.Forms.Control)(object)this.ULGLevels).Name = "ULGLevels";
		this.ULGLevels.AfterEnterEditMode += new System.EventHandler(ULGLevels_AfterEnterEditMode);
		((System.Windows.Forms.Control)(object)this.ULGLevels).Enter += new System.EventHandler(ULGLevels_Enter);
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		resources.ApplyResources(val34, "appearance34");
		((AppearanceBase)val34).TextTrimming = (TextTrimming)6;
		((ControlBase)this.ultraLabel5).Appearance = (AppearanceBase)(object)val34;
		this.ultraLabel5.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		resources.ApplyResources(val35, "appearance35");
		((AppearanceBase)val35).TextTrimming = (TextTrimming)6;
		((ControlBase)this.ultraLabel6).Appearance = (AppearanceBase)(object)val35;
		this.ultraLabel6.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		((ControlBase)this.ultraLabel6).WrapText = false;
		resources.ApplyResources(this.cboShape, "cboShape");
		this.cboShape.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboShape).Name = "cboShape";
		resources.ApplyResources(this.lblShape, "lblShape");
		((AppearanceBase)val36).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val36).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val36, "appearance36");
		((AppearanceBase)val36).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblShape).Appearance = (AppearanceBase)(object)val36;
		this.lblShape.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblShape).Name = "lblShape";
		((ControlBase)this.lblShape).WrapText = false;
		resources.ApplyResources(this.cboModel, "cboModel");
		this.cboModel.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboModel).Name = "cboModel";
		resources.ApplyResources(this.lblModel, "lblModel");
		((AppearanceBase)val37).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val37).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val37, "appearance37");
		((AppearanceBase)val37).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblModel).Appearance = (AppearanceBase)(object)val37;
		this.lblModel.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblModel).Name = "lblModel";
		((ControlBase)this.lblModel).WrapText = false;
		resources.ApplyResources(this.cboMaterial2, "cboMaterial2");
		this.cboMaterial2.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboMaterial2).Name = "cboMaterial2";
		resources.ApplyResources(this.lblMaterial2, "lblMaterial2");
		((AppearanceBase)val38).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val38).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val38, "appearance38");
		((AppearanceBase)val38).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblMaterial2).Appearance = (AppearanceBase)(object)val38;
		this.lblMaterial2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMaterial2).Name = "lblMaterial2";
		((ControlBase)this.lblMaterial2).WrapText = false;
		resources.ApplyResources(this.cboSizeCategory, "cboSizeCategory");
		this.cboSizeCategory.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSizeCategory).Name = "cboSizeCategory";
		resources.ApplyResources(this.lblSizeCategory, "lblSizeCategory");
		((AppearanceBase)val39).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val39).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val39, "appearance39");
		((AppearanceBase)val39).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblSizeCategory).Appearance = (AppearanceBase)(object)val39;
		this.lblSizeCategory.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSizeCategory).Name = "lblSizeCategory";
		((ControlBase)this.lblSizeCategory).WrapText = false;
		resources.ApplyResources(this.cboPriceCategory, "cboPriceCategory");
		this.cboPriceCategory.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboPriceCategory).Name = "cboPriceCategory";
		resources.ApplyResources(this.lblPriceCategory, "lblPriceCategory");
		((AppearanceBase)val40).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val40).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val40, "appearance40");
		((AppearanceBase)val40).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblPriceCategory).Appearance = (AppearanceBase)(object)val40;
		this.lblPriceCategory.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPriceCategory).Name = "lblPriceCategory";
		((ControlBase)this.lblPriceCategory).WrapText = false;
		resources.ApplyResources(this.cboCountry, "cboCountry");
		this.cboCountry.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboCountry).Name = "cboCountry";
		resources.ApplyResources(this.lblCountry, "lblCountry");
		((AppearanceBase)val41).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val41).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val41, "appearance41");
		((AppearanceBase)val41).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblCountry).Appearance = (AppearanceBase)(object)val41;
		this.lblCountry.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCountry).Name = "lblCountry";
		((ControlBase)this.lblCountry).WrapText = false;
		resources.ApplyResources(this.cboSeason, "cboSeason");
		this.cboSeason.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSeason).Name = "cboSeason";
		resources.ApplyResources(this.lblSeason, "lblSeason");
		((AppearanceBase)val42).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val42).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val42, "appearance42");
		((AppearanceBase)val42).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblSeason).Appearance = (AppearanceBase)(object)val42;
		this.lblSeason.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSeason).Name = "lblSeason";
		((ControlBase)this.lblSeason).WrapText = false;
		resources.ApplyResources(this.cboClassification4, "cboClassification4");
		this.cboClassification4.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboClassification4).Name = "cboClassification4";
		resources.ApplyResources(this.lblClassification4, "lblClassification4");
		((AppearanceBase)val43).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val43).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val43, "appearance43");
		((AppearanceBase)val43).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblClassification4).Appearance = (AppearanceBase)(object)val43;
		this.lblClassification4.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClassification4).Name = "lblClassification4";
		((ControlBase)this.lblClassification4).WrapText = false;
		resources.ApplyResources(this.cboClassification3, "cboClassification3");
		this.cboClassification3.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboClassification3).Name = "cboClassification3";
		resources.ApplyResources(this.lblClassification3, "lblClassification3");
		((AppearanceBase)val44).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val44).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val44, "appearance44");
		((AppearanceBase)val44).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblClassification3).Appearance = (AppearanceBase)(object)val44;
		this.lblClassification3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClassification3).Name = "lblClassification3";
		((ControlBase)this.lblClassification3).WrapText = false;
		resources.ApplyResources(this.cboClassification2, "cboClassification2");
		this.cboClassification2.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboClassification2).Name = "cboClassification2";
		resources.ApplyResources(this.lblClassification2, "lblClassification2");
		((AppearanceBase)val45).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val45).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val45, "appearance45");
		((AppearanceBase)val45).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblClassification2).Appearance = (AppearanceBase)(object)val45;
		this.lblClassification2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClassification2).Name = "lblClassification2";
		((ControlBase)this.lblClassification2).WrapText = false;
		resources.ApplyResources(this.cboClassification1, "cboClassification1");
		this.cboClassification1.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboClassification1).Name = "cboClassification1";
		resources.ApplyResources(this.lblClassification1, "lblClassification1");
		((AppearanceBase)val46).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val46).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val46, "appearance46");
		((AppearanceBase)val46).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblClassification1).Appearance = (AppearanceBase)(object)val46;
		this.lblClassification1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClassification1).Name = "lblClassification1";
		((ControlBase)this.lblClassification1).WrapText = false;
		resources.ApplyResources(this.cboAgeGroup, "cboAgeGroup");
		this.cboAgeGroup.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboAgeGroup).Name = "cboAgeGroup";
		resources.ApplyResources(this.lblAgeGroup, "lblAgeGroup");
		((AppearanceBase)val47).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val47).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val47, "appearance47");
		((AppearanceBase)val47).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblAgeGroup).Appearance = (AppearanceBase)(object)val47;
		this.lblAgeGroup.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAgeGroup).Name = "lblAgeGroup";
		((ControlBase)this.lblAgeGroup).WrapText = false;
		resources.ApplyResources(this.cboBrand, "cboBrand");
		this.cboBrand.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboBrand).Name = "cboBrand";
		resources.ApplyResources(this.lblBrand, "lblBrand");
		((AppearanceBase)val48).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val48).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val48, "appearance48");
		((AppearanceBase)val48).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblBrand).Appearance = (AppearanceBase)(object)val48;
		this.lblBrand.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBrand).Name = "lblBrand";
		((ControlBase)this.lblBrand).WrapText = false;
		resources.ApplyResources(this.cboTypes, "cboTypes");
		this.cboTypes.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboTypes).Name = "cboTypes";
		resources.ApplyResources(this.lblTypes, "lblTypes");
		((AppearanceBase)val49).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val49).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val49, "appearance49");
		((AppearanceBase)val49).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblTypes).Appearance = (AppearanceBase)(object)val49;
		this.lblTypes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTypes).Name = "lblTypes";
		((ControlBase)this.lblTypes).WrapText = false;
		resources.ApplyResources(this.clbColors, "clbColors");
		this.clbColors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.clbColors.CheckOnClick = true;
		this.clbColors.MultiColumn = true;
		this.clbColors.Name = "clbColors";
		resources.ApplyResources(this.clbMaterials, "clbMaterials");
		this.clbMaterials.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.clbMaterials.CheckOnClick = true;
		this.clbMaterials.MultiColumn = true;
		this.clbMaterials.Name = "clbMaterials";
		resources.ApplyResources(this.lblMaterial1, "lblMaterial1");
		resources.ApplyResources(val50, "appearance50");
		((AppearanceBase)val50).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblMaterial1).Appearance = (AppearanceBase)(object)val50;
		this.lblMaterial1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMaterial1).Name = "lblMaterial1";
		((ControlBase)this.lblMaterial1).WrapText = false;
		resources.ApplyResources(this.lblColors, "lblColors");
		resources.ApplyResources(val51, "appearance51");
		((AppearanceBase)val51).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblColors).Appearance = (AppearanceBase)(object)val51;
		this.lblColors.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblColors).Name = "lblColors";
		((ControlBase)this.lblColors).WrapText = false;
		resources.ApplyResources(this.cboSuppliers, "cboSuppliers");
		this.cboSuppliers.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSuppliers).Name = "cboSuppliers";
		resources.ApplyResources(this.lblSuplliers, "lblSuplliers");
		((AppearanceBase)val52).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val52).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val52, "appearance52");
		((AppearanceBase)val52).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblSuplliers).Appearance = (AppearanceBase)(object)val52;
		this.lblSuplliers.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSuplliers).Name = "lblSuplliers";
		((ControlBase)this.lblSuplliers).WrapText = false;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSuppliers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSuplliers);
		base.Controls.Add(this.clbMaterials);
		base.Controls.Add(this.clbColors);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTypes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTypes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboShape);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblShape);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboModel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblModel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboMaterial2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMaterial2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSizeCategory);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSizeCategory);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPriceCategory);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPriceCategory);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCountry);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCountry);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSeason);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSeason);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClassification4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClassification4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClassification3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClassification3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClassification2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClassification2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClassification1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClassification1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboAgeGroup);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAgeGroup);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBrand);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBrand);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGLevels);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraPanel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGPrices);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraPanel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSaveAndClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblColors);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMaterial1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUnit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboUnit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUnitGroup);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboUnitGroup);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGroup);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRoot);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboGroup);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboRoot);
		base.Name = "frmSizeItemsGroups";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboRoot, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboGroup, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRoot, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGroup, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboUnitGroup, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUnitGroup, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboUnit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUnit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel5, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMaterial1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel6, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblColors, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSaveAndClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraPanel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGPrices, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraPanel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGLevels, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBrand, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBrand, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAgeGroup, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboAgeGroup, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClassification1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClassification1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClassification2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClassification2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClassification3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClassification3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClassification4, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClassification4, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSeason, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSeason, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCountry, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCountry, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPriceCategory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPriceCategory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSizeCategory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSizeCategory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMaterial2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboMaterial2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblModel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboModel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblShape, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboShape, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTypes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTypes, 0);
		base.Controls.SetChildIndex(this.clbColors, 0);
		base.Controls.SetChildIndex(this.clbMaterials, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSuplliers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSuppliers, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraPanel1.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraPanel1.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.ultraPanel1).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraPanel2.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraPanel2.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.ultraPanel2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.cboRoot).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboGroup).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnitGroup).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGPrices).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGLevels).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboShape).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboModel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaterial2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSizeCategory).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceCategory).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSeason).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAgeGroup).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBrand).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTypes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSuppliers).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
