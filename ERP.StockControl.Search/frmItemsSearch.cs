using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.StockControl.Search;

public class frmItemsSearch : frmSearch
{
	private IContainer components = null;

	public frmItemsSearch()
	{
		InitializeComponent();
	}

	public frmItemsSearch(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemNumber"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemNumber"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الصنف" : "No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemNumber"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13) - GlobalVariables.ScrollWidth;
		((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemNumber"]).Tag = dtSource;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "باركود الصنف" : "BarCode");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"]).Tag = dtSource;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemNameAr"].Header).Caption = (GlobalVariables.IsArabic ? " اسم الصنف بالعربية" : "Name Ar");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "اسم الصنف بالانجليزية" : "Name En");
		if (GlobalVariables.IsArabic)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemNameAr"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
			((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemNameAr"]).Tag = dtSource;
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemNameEn"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
			((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemNameEn"]).Tag = dtSource;
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitName"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		DataView defaultView = dtSource.DefaultView.ToTable(true, "UnitName").DefaultView;
		defaultView.Sort = "UnitName";
		((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitName"]).Tag = defaultView.ToTable();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentNumber"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentNumber"].Header).Caption = (GlobalVariables.IsArabic ? "رقم المجموعة" : "Group No.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentNumber"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		defaultView = dtSource.DefaultView.ToTable(true, "ParentNumber").DefaultView;
		defaultView.Sort = "ParentNumber";
		((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentNumber"]).Tag = defaultView.ToTable();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم المجموعة" : "Group Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.12);
		defaultView = dtSource.DefaultView.ToTable(true, "ParentName").DefaultView;
		defaultView.Sort = "ParentName";
		((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentName"]).Tag = defaultView.ToTable();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsService"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsService"].Header).Caption = (GlobalVariables.IsArabic ? "خدمة" : "IsService");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsService"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsRecipe"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsRecipe"].Header).Caption = (GlobalVariables.IsArabic ? "وصفة" : "IsRecipe");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsRecipe"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorName"].Header).Caption = (GlobalVariables.IsArabic ? "اللون" : "Color");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		defaultView = dtSource.DefaultView.ToTable(true, "ColorName").DefaultView;
		defaultView.Sort = "ColorName";
		((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorName"]).Tag = defaultView.ToTable();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeName"].Header).Caption = (GlobalVariables.IsArabic ? "مقاس" : "Size");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		defaultView = dtSource.DefaultView.ToTable(true, "ItemSizeName").DefaultView;
		defaultView.Sort = "ItemSizeName";
		((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeName"]).Tag = defaultView.ToTable();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsProductionItem"].Header).Caption = (GlobalVariables.IsArabic ? "صنف إنتاج" : "Is Production");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsActive"].Header).Caption = (GlobalVariables.IsArabic ? "نشط" : "Is Active");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDirectItem"].Header).Caption = (GlobalVariables.IsArabic ? "صنف مباشر" : "Is Direct");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSalesItem"].Header).Caption = (GlobalVariables.IsArabic ? "صنف بيع" : "Sales Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EnforceBatchNo"].Header).Caption = (GlobalVariables.IsArabic ? "السريال اجبارى" : "Enforce Batch No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxName"].Header).Caption = (GlobalVariables.IsArabic ? "الضريبه" : "Tax");
		defaultView = dtSource.DefaultView.ToTable(true, "TaxName").DefaultView;
		defaultView.Sort = "TaxName";
		((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxName"]).Tag = defaultView.ToTable();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemTypeName"].Header).Caption = (GlobalVariables.IsArabic ? "النوع" : "Type");
		defaultView = dtSource.DefaultView.ToTable(true, "ItemTypeName").DefaultView;
		defaultView.Sort = "ItemTypeName";
		((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemTypeName"]).Tag = defaultView.ToTable();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemAgeGroupName"].Header).Caption = GlobalFunctions.GetFormName("frmItemsAgeGroups");
		defaultView = dtSource.DefaultView.ToTable(true, "ItemAgeGroupName").DefaultView;
		defaultView.Sort = "ItemAgeGroupName";
		((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemAgeGroupName"]).Tag = defaultView.ToTable();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemFirstClassificationName"].Header).Caption = GlobalFunctions.GetFormName("frmItemsFirstClassifications");
		defaultView = dtSource.DefaultView.ToTable(true, "ItemFirstClassificationName").DefaultView;
		defaultView.Sort = "ItemFirstClassificationName";
		((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemFirstClassificationName"]).Tag = defaultView.ToTable();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSecondClassificationName"].Header).Caption = GlobalFunctions.GetFormName("frmItemsSecondClassifications");
		defaultView = dtSource.DefaultView.ToTable(true, "ItemSecondClassificationName").DefaultView;
		defaultView.Sort = "ItemSecondClassificationName";
		((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSecondClassificationName"]).Tag = defaultView.ToTable();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemThirdClassificationName"].Header).Caption = GlobalFunctions.GetFormName("frmItemsThirdClassifications");
		defaultView = dtSource.DefaultView.ToTable(true, "ItemThirdClassificationName").DefaultView;
		defaultView.Sort = "ItemThirdClassificationName";
		((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemThirdClassificationName"]).Tag = defaultView.ToTable();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemFourthClassificationName"].Header).Caption = GlobalFunctions.GetFormName("frmItemsFourthClassifications");
		defaultView = dtSource.DefaultView.ToTable(true, "ItemFourthClassificationName").DefaultView;
		defaultView.Sort = "ItemFourthClassificationName";
		((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemFourthClassificationName"]).Tag = defaultView.ToTable();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBrandName"].Header).Caption = GlobalFunctions.GetFormName("frmItemsBrands");
		defaultView = dtSource.DefaultView.ToTable(true, "ItemBrandName").DefaultView;
		defaultView.Sort = "ItemBrandName";
		((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBrandName"]).Tag = defaultView.ToTable();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSeasonName"].Header).Caption = GlobalFunctions.GetFormName("frmItemsSeasons");
		defaultView = dtSource.DefaultView.ToTable(true, "ItemSeasonName").DefaultView;
		defaultView.Sort = "ItemSeasonName";
		((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSeasonName"]).Tag = defaultView.ToTable();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemCountryName"].Header).Caption = GlobalFunctions.GetFormName("frmItemsCountrys");
		defaultView = dtSource.DefaultView.ToTable(true, "ItemCountryName").DefaultView;
		defaultView.Sort = "ItemCountryName";
		((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemCountryName"]).Tag = defaultView.ToTable();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemPriceCategoryName"].Header).Caption = GlobalFunctions.GetFormName("frmItemsPriceCategorys");
		defaultView = dtSource.DefaultView.ToTable(true, "ItemPriceCategoryName").DefaultView;
		defaultView.Sort = "ItemPriceCategoryName";
		((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemPriceCategoryName"]).Tag = defaultView.ToTable();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemFirstMaterialName"].Header).Caption = GlobalFunctions.GetFormName("frmItemsFirstMaterials");
		defaultView = dtSource.DefaultView.ToTable(true, "ItemFirstMaterialName").DefaultView;
		defaultView.Sort = "ItemFirstMaterialName";
		((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemFirstMaterialName"]).Tag = defaultView.ToTable();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSecondMaterialName"].Header).Caption = GlobalFunctions.GetFormName("frmItemsSecondMaterials");
		defaultView = dtSource.DefaultView.ToTable(true, "ItemSecondMaterialName").DefaultView;
		defaultView.Sort = "ItemSecondMaterialName";
		((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSecondMaterialName"]).Tag = defaultView.ToTable();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemShapeName"].Header).Caption = GlobalFunctions.GetFormName("frmItemsShapes");
		defaultView = dtSource.DefaultView.ToTable(true, "ItemShapeName").DefaultView;
		defaultView.Sort = "ItemShapeName";
		((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemShapeName"]).Tag = defaultView.ToTable();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemModelName"].Header).Caption = GlobalFunctions.GetFormName("frmItemsModels");
		defaultView = dtSource.DefaultView.ToTable(true, "ItemModelName").DefaultView;
		defaultView.Sort = "ItemModelName";
		((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemModelName"]).Tag = defaultView.ToTable();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Header).Caption = (GlobalVariables.IsArabic ? "المورد" : "Supplier");
		defaultView = dtSource.DefaultView.ToTable(true, "SubAccountName").DefaultView;
		defaultView.Sort = "SubAccountName";
		((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"]).Tag = defaultView.ToTable();
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
		this.components = new System.ComponentModel.Container();
	}
}
