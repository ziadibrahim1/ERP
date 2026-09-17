using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.StockControl.MasterData;

public class frmUpdateItemsClassifications : frmBase
{
	private DataTable dtGroups;

	private DataTable dtAgeGroup;

	private DataTable dtClassification1;

	private DataTable dtClassification2;

	private DataTable dtClassification3;

	private DataTable dtClassification4;

	private DataTable dtBrand;

	private DataTable dtSeason;

	private DataTable dtCountry;

	private DataTable dtMaterial1;

	private DataTable dtMaterial2;

	private DataTable dtShape;

	private DataTable dtModel;

	private string GroupID;

	private DataRow drGroup;

	private IContainer components = null;

	public UltraLabel lblRoot;

	public UltraComboEditor cboGroup;

	public UltraButton btnSaveAndClose;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraButton btnCancel;

	public UltraButton btnSave;

	public UltraLabel lblTitle2;

	private UltraCheckEditor chkModMaterial1;

	private UltraCheckEditor chkModClass4;

	private UltraCheckEditor chkModBrand;

	private UltraCheckEditor chkModGender;

	private UltraCheckEditor chkModClass2;

	private UltraCheckEditor chkModSeason;

	private UltraCheckEditor chkModCountry;

	private UltraCheckEditor chkModClass1;

	private UltraCheckEditor chkModClass3;

	private UltraComboEditor cboBrand;

	private UltraLabel lblBrand;

	private UltraComboEditor cboAgeGroup;

	private UltraLabel lblAgeGroup;

	private UltraComboEditor cboClassification4;

	private UltraLabel lblClassification4;

	private UltraComboEditor cboClassification3;

	private UltraLabel lblClassification3;

	private UltraComboEditor cboClassification2;

	private UltraLabel lblClassification2;

	private UltraComboEditor cboClassification1;

	private UltraLabel lblClassification1;

	private UltraComboEditor cboSeason;

	private UltraLabel lblSeason;

	private UltraComboEditor cboCountry;

	private UltraLabel lblCountry;

	private UltraComboEditor cboMaterial2;

	private UltraLabel lblMaterial2;

	private UltraComboEditor cboMaterial1;

	private UltraLabel lblMaterial1;

	private UltraComboEditor cboShape;

	private UltraLabel lblShape;

	private UltraComboEditor cboModel;

	private UltraLabel lblModel;

	private UltraCheckEditor chkModShape;

	private UltraCheckEditor chkModMaterial2;

	private UltraCheckEditor chkModModel;

	private void chkModBrand_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboBrand).Enabled = ((UltraToggleEditorBase)chkModBrand).Checked;
	}

	private void chkModGender_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboAgeGroup).Enabled = ((UltraToggleEditorBase)chkModGender).Checked;
	}

	private void chkModClass1_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboClassification1).Enabled = ((UltraToggleEditorBase)chkModClass1).Checked;
	}

	private void chkModClass2_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboClassification2).Enabled = ((UltraToggleEditorBase)chkModClass2).Checked;
	}

	private void chkModClass3_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboClassification3).Enabled = ((UltraToggleEditorBase)chkModClass3).Checked;
	}

	private void chkModClass4_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboClassification4).Enabled = ((UltraToggleEditorBase)chkModClass4).Checked;
	}

	private void chkModSeason_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboSeason).Enabled = ((UltraToggleEditorBase)chkModSeason).Checked;
	}

	private void chkModCountry_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboCountry).Enabled = ((UltraToggleEditorBase)chkModCountry).Checked;
	}

	private void chkModMaterial1_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboMaterial1).Enabled = ((UltraToggleEditorBase)chkModMaterial1).Checked;
	}

	private void chkModMaterial2_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboMaterial2).Enabled = ((UltraToggleEditorBase)chkModMaterial2).Checked;
	}

	private void chkModModel_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboModel).Enabled = ((UltraToggleEditorBase)chkModModel).Checked;
	}

	private void chkModShape_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboShape).Enabled = ((UltraToggleEditorBase)chkModShape).Checked;
	}

	private void frmUpdateItemsClassifications_Load(object sender, EventArgs e)
	{
	}

	private void lblAgeGroup_Click(object sender, EventArgs e)
	{
	}

	public frmUpdateItemsClassifications(string groupID)
	{
		GroupID = groupID;
		InitializeComponent();
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
		((Control)(object)lblSeason).Text = GlobalFunctions.GetFormName("frmItemsSeasons");
		((Control)(object)lblShape).Text = GlobalFunctions.GetFormName("frmItemsShapes");
		dtGroups = Items.FillGroups("-1", "-1", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboGroup, dtGroups, "ItemID", "Name");
		dtAgeGroup = ItemsAgeGroups.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboAgeGroup, dtAgeGroup, "ItemAgeGroupID", GlobalVariables.IsArabic ? "ItemAgeGroupNameAr" : "ItemAgeGroupNameEn");
		UltraCheckEditor obj = chkModGender;
		UltraLabel obj2 = lblAgeGroup;
		bool flag = (((Control)(object)cboAgeGroup).Visible = dtAgeGroup.Rows.Count > 0);
		bool visible = (((Control)(object)obj2).Visible = flag);
		((Control)(object)obj).Visible = visible;
		dtClassification1 = ItemsFirstClassifications.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboClassification1, dtClassification1, "ItemFirstClassificationID", GlobalVariables.IsArabic ? "ItemFirstClassificationNameAr" : "ItemFirstClassificationNameEn");
		UltraCheckEditor obj3 = chkModClass1;
		UltraLabel obj4 = lblClassification1;
		flag = (((Control)(object)cboClassification1).Visible = dtClassification1.Rows.Count > 0);
		visible = (((Control)(object)obj4).Visible = flag);
		((Control)(object)obj3).Visible = visible;
		dtClassification2 = ItemsSecondClassifications.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboClassification2, dtClassification2, "ItemSecondClassificationID", GlobalVariables.IsArabic ? "ItemSecondClassificationNameAr" : "ItemSecondClassificationNameEn");
		UltraCheckEditor obj5 = chkModClass2;
		UltraLabel obj6 = lblClassification2;
		flag = (((Control)(object)cboClassification2).Visible = dtClassification2.Rows.Count > 0);
		visible = (((Control)(object)obj6).Visible = flag);
		((Control)(object)obj5).Visible = visible;
		dtClassification3 = ItemsThirdClassifications.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboClassification3, dtClassification3, "ItemThirdClassificationID", GlobalVariables.IsArabic ? "ItemThirdClassificationNameAr" : "ItemThirdClassificationNameEn");
		UltraCheckEditor obj7 = chkModClass3;
		UltraLabel obj8 = lblClassification3;
		flag = (((Control)(object)cboClassification3).Visible = dtClassification3.Rows.Count > 0);
		visible = (((Control)(object)obj8).Visible = flag);
		((Control)(object)obj7).Visible = visible;
		dtClassification4 = ItemsFourthClassifications.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboClassification4, dtClassification4, "ItemFourthClassificationID", GlobalVariables.IsArabic ? "ItemFourthClassificationNameAr" : "ItemFourthClassificationNameEn");
		UltraCheckEditor obj9 = chkModClass4;
		UltraLabel obj10 = lblClassification4;
		flag = (((Control)(object)cboClassification4).Visible = dtClassification4.Rows.Count > 0);
		visible = (((Control)(object)obj10).Visible = flag);
		((Control)(object)obj9).Visible = visible;
		dtBrand = ItemsBrands.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboBrand, dtBrand, "ItemBrandID", GlobalVariables.IsArabic ? "ItemBrandNameAr" : "ItemBrandNameEn");
		UltraCheckEditor obj11 = chkModBrand;
		UltraLabel obj12 = lblBrand;
		flag = (((Control)(object)cboBrand).Visible = dtBrand.Rows.Count > 0);
		visible = (((Control)(object)obj12).Visible = flag);
		((Control)(object)obj11).Visible = visible;
		dtSeason = ItemsSeasons.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSeason, dtSeason, "ItemSeasonID", GlobalVariables.IsArabic ? "ItemSeasonNameAr" : "ItemSeasonNameEn");
		UltraCheckEditor obj13 = chkModSeason;
		UltraLabel obj14 = lblSeason;
		flag = (((Control)(object)cboSeason).Visible = dtSeason.Rows.Count > 0);
		visible = (((Control)(object)obj14).Visible = flag);
		((Control)(object)obj13).Visible = visible;
		dtCountry = ItemsCountrys.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCountry, dtCountry, "ItemCountryID", GlobalVariables.IsArabic ? "ItemCountryNameAr" : "ItemCountryNameEn");
		UltraCheckEditor obj15 = chkModCountry;
		UltraLabel obj16 = lblCountry;
		flag = (((Control)(object)cboCountry).Visible = dtCountry.Rows.Count > 0);
		visible = (((Control)(object)obj16).Visible = flag);
		((Control)(object)obj15).Visible = visible;
		dtMaterial1 = ItemsFirstMaterials.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboMaterial1, dtMaterial1, "ItemFirstMaterialID", GlobalVariables.IsArabic ? "ItemFirstMaterialNameAr" : "ItemFirstMaterialNameEn");
		UltraCheckEditor obj17 = chkModMaterial1;
		UltraLabel obj18 = lblMaterial1;
		flag = (((Control)(object)cboMaterial1).Visible = dtMaterial1.Rows.Count > 0);
		visible = (((Control)(object)obj18).Visible = flag);
		((Control)(object)obj17).Visible = visible;
		dtMaterial2 = ItemsSecondMaterials.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboMaterial2, dtMaterial2, "ItemSecondMaterialID", GlobalVariables.IsArabic ? "ItemSecondMaterialNameAr" : "ItemSecondMaterialNameEn");
		UltraCheckEditor obj19 = chkModMaterial2;
		UltraLabel obj20 = lblMaterial2;
		flag = (((Control)(object)cboMaterial2).Visible = dtMaterial2.Rows.Count > 0);
		visible = (((Control)(object)obj20).Visible = flag);
		((Control)(object)obj19).Visible = visible;
		dtShape = ItemsShapes.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboShape, dtShape, "ItemShapeID", GlobalVariables.IsArabic ? "ItemShapeNameAr" : "ItemShapeNameEn");
		UltraCheckEditor obj21 = chkModShape;
		UltraLabel obj22 = lblShape;
		flag = (((Control)(object)cboShape).Visible = dtShape.Rows.Count > 0);
		visible = (((Control)(object)obj22).Visible = flag);
		((Control)(object)obj21).Visible = visible;
		dtModel = ItemsModels.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboModel, dtModel, "ItemModelID", GlobalVariables.IsArabic ? "ItemModelNameAr" : "ItemModelNameEn");
		UltraCheckEditor obj23 = chkModModel;
		UltraLabel obj24 = lblModel;
		flag = (((Control)(object)cboModel).Visible = dtModel.Rows.Count > 0);
		visible = (((Control)(object)obj24).Visible = flag);
		((Control)(object)obj23).Visible = visible;
		DisplayData();
		((Control)(object)btnSave).Enabled = true;
		((Control)(object)btnSaveAndClose).Enabled = true;
		((Control)(object)btnCancel).Enabled = true;
	}

	public void DisplayData()
	{
		drGroup = Items.Select(GroupID, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true).Rows[0];
		((TextEditorControlBase)cboGroup).Value = GroupID;
		((TextEditorControlBase)cboAgeGroup).Value = drGroup["ItemAgeGroupID"];
		((TextEditorControlBase)cboClassification1).Value = drGroup["ItemFirstClassificationID"];
		((TextEditorControlBase)cboClassification2).Value = drGroup["ItemSecondClassificationID"];
		((TextEditorControlBase)cboClassification3).Value = drGroup["ItemThirdClassificationID"];
		((TextEditorControlBase)cboClassification4).Value = drGroup["ItemFourthClassificationID"];
		((TextEditorControlBase)cboBrand).Value = drGroup["ItemBrandID"];
		((TextEditorControlBase)cboSeason).Value = drGroup["ItemSeasonID"];
		((TextEditorControlBase)cboCountry).Value = drGroup["ItemCountryID"];
		((TextEditorControlBase)cboMaterial1).Value = drGroup["ItemFirstMaterialID"];
		((TextEditorControlBase)cboMaterial2).Value = drGroup["ItemSecondMaterialID"];
		((TextEditorControlBase)cboShape).Value = drGroup["ItemShapeID"];
		((TextEditorControlBase)cboModel).Value = drGroup["ItemModelID"];
	}

	public bool ValidateData()
	{
		return true;
	}

	public void Save()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			DataTable dataTable = Items.UpdateGroupItemsClassifications(((TextEditorControlBase)cboGroup).Value.ToString(), (!((UltraToggleEditorBase)chkModGender).Checked) ? "-1" : ((TextEditorControlBase)cboAgeGroup).Value.ToString(), (!((UltraToggleEditorBase)chkModClass1).Checked) ? "-1" : ((TextEditorControlBase)cboClassification1).Value.ToString(), (!((UltraToggleEditorBase)chkModClass2).Checked) ? "-1" : ((TextEditorControlBase)cboClassification2).Value.ToString(), (!((UltraToggleEditorBase)chkModClass3).Checked) ? "-1" : ((TextEditorControlBase)cboClassification3).Value.ToString(), (!((UltraToggleEditorBase)chkModClass4).Checked) ? "-1" : ((TextEditorControlBase)cboClassification4).Value.ToString(), (!((UltraToggleEditorBase)chkModBrand).Checked) ? "-1" : ((TextEditorControlBase)cboBrand).Value.ToString(), (!((UltraToggleEditorBase)chkModSeason).Checked) ? "-1" : ((TextEditorControlBase)cboSeason).Value.ToString(), (!((UltraToggleEditorBase)chkModCountry).Checked) ? "-1" : ((TextEditorControlBase)cboCountry).Value.ToString(), (!((UltraToggleEditorBase)chkModMaterial1).Checked) ? "-1" : ((TextEditorControlBase)cboMaterial1).Value.ToString(), (!((UltraToggleEditorBase)chkModMaterial2).Checked) ? "-1" : ((TextEditorControlBase)cboMaterial2).Value.ToString(), (!((UltraToggleEditorBase)chkModShape).Checked) ? "-1" : ((TextEditorControlBase)cboShape).Value.ToString(), (!((UltraToggleEditorBase)chkModModel).Checked) ? "-1" : ((TextEditorControlBase)cboModel).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show("تم الحفظ بنجاح", "Items Saved");
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void cboRoot_ValueChanged(object sender, EventArgs e)
	{
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
		Dispose();
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
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected O, but got Unknown
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Expected O, but got Unknown
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Expected O, but got Unknown
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Expected O, but got Unknown
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Expected O, but got Unknown
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Expected O, but got Unknown
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Expected O, but got Unknown
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Expected O, but got Unknown
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Expected O, but got Unknown
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Expected O, but got Unknown
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Expected O, but got Unknown
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Expected O, but got Unknown
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Expected O, but got Unknown
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Expected O, but got Unknown
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.MasterData.frmUpdateItemsClassifications));
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
		this.lblRoot = new UltraLabel();
		this.cboGroup = new UltraComboEditor();
		this.btnSaveAndClose = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.btnCancel = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.chkModMaterial1 = new UltraCheckEditor();
		this.chkModClass4 = new UltraCheckEditor();
		this.chkModBrand = new UltraCheckEditor();
		this.chkModGender = new UltraCheckEditor();
		this.chkModClass2 = new UltraCheckEditor();
		this.chkModSeason = new UltraCheckEditor();
		this.chkModCountry = new UltraCheckEditor();
		this.chkModClass1 = new UltraCheckEditor();
		this.chkModClass3 = new UltraCheckEditor();
		this.cboBrand = new UltraComboEditor();
		this.lblBrand = new UltraLabel();
		this.cboAgeGroup = new UltraComboEditor();
		this.lblAgeGroup = new UltraLabel();
		this.cboClassification4 = new UltraComboEditor();
		this.lblClassification4 = new UltraLabel();
		this.cboClassification3 = new UltraComboEditor();
		this.lblClassification3 = new UltraLabel();
		this.cboClassification2 = new UltraComboEditor();
		this.lblClassification2 = new UltraLabel();
		this.cboClassification1 = new UltraComboEditor();
		this.lblClassification1 = new UltraLabel();
		this.cboSeason = new UltraComboEditor();
		this.lblSeason = new UltraLabel();
		this.cboCountry = new UltraComboEditor();
		this.lblCountry = new UltraLabel();
		this.cboMaterial2 = new UltraComboEditor();
		this.lblMaterial2 = new UltraLabel();
		this.cboMaterial1 = new UltraComboEditor();
		this.lblMaterial1 = new UltraLabel();
		this.cboShape = new UltraComboEditor();
		this.lblShape = new UltraLabel();
		this.cboModel = new UltraComboEditor();
		this.lblModel = new UltraLabel();
		this.chkModShape = new UltraCheckEditor();
		this.chkModMaterial2 = new UltraCheckEditor();
		this.chkModModel = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboGroup).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModMaterial1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModClass4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModBrand).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModGender).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModClass2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModSeason).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModCountry).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModClass1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModClass3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBrand).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAgeGroup).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSeason).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaterial2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaterial1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboShape).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboModel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModShape).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModMaterial2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModModel).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblRoot, "lblRoot");
		this.lblRoot.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRoot).Name = "lblRoot";
		((ControlBase)this.lblRoot).WrapText = false;
		resources.ApplyResources(this.cboGroup, "cboGroup");
		this.cboGroup.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboGroup).Name = "cboGroup";
		((TextEditorControlBase)this.cboGroup).Nullable = false;
		((TextEditorControlBase)this.cboGroup).ValueChanged += new System.EventHandler(cboRoot_ValueChanged);
		resources.ApplyResources(this.btnSaveAndClose, "btnSaveAndClose");
		((AppearanceBase)val).Image = resources.GetObject("appearance31.Image");
		resources.ApplyResources(val, "appearance31");
		((ControlBase)this.btnSaveAndClose).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnSaveAndClose).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSaveAndClose).Name = "btnSaveAndClose";
		((System.Windows.Forms.Control)(object)this.btnSaveAndClose).Click += new System.EventHandler(btnSaveAndClose_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance32");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance33.Image");
		resources.ApplyResources(val3, "appearance33");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((AppearanceBase)val4).Image = resources.GetObject("appearance34.Image");
		resources.ApplyResources(val4, "appearance34");
		((ControlBase)this.btnCancel).Appearance = (AppearanceBase)(object)val4;
		((ControlBase)this.btnCancel).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val5).Image = resources.GetObject("appearance35.Image");
		resources.ApplyResources(val5, "appearance35");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val5;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val6).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val6, "appearance36");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.chkModMaterial1, "chkModMaterial1");
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance37");
		((UltraToggleEditorBase)this.chkModMaterial1).Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.chkModMaterial1).Name = "chkModMaterial1";
		((UltraToggleEditorBase)this.chkModMaterial1).CheckedChanged += new System.EventHandler(chkModMaterial1_CheckedChanged);
		resources.ApplyResources(this.chkModClass4, "chkModClass4");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance38");
		((UltraToggleEditorBase)this.chkModClass4).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.chkModClass4).Name = "chkModClass4";
		((UltraToggleEditorBase)this.chkModClass4).CheckedChanged += new System.EventHandler(chkModClass4_CheckedChanged);
		resources.ApplyResources(this.chkModBrand, "chkModBrand");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance39");
		((UltraToggleEditorBase)this.chkModBrand).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.chkModBrand).Name = "chkModBrand";
		((UltraToggleEditorBase)this.chkModBrand).CheckedChanged += new System.EventHandler(chkModBrand_CheckedChanged);
		resources.ApplyResources(this.chkModGender, "chkModGender");
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance40");
		((UltraToggleEditorBase)this.chkModGender).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.chkModGender).Name = "chkModGender";
		((UltraToggleEditorBase)this.chkModGender).CheckedChanged += new System.EventHandler(chkModGender_CheckedChanged);
		resources.ApplyResources(this.chkModClass2, "chkModClass2");
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance41");
		((UltraToggleEditorBase)this.chkModClass2).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.chkModClass2).Name = "chkModClass2";
		((UltraToggleEditorBase)this.chkModClass2).CheckedChanged += new System.EventHandler(chkModClass2_CheckedChanged);
		resources.ApplyResources(this.chkModSeason, "chkModSeason");
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance42");
		((UltraToggleEditorBase)this.chkModSeason).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.chkModSeason).Name = "chkModSeason";
		((UltraToggleEditorBase)this.chkModSeason).CheckedChanged += new System.EventHandler(chkModSeason_CheckedChanged);
		resources.ApplyResources(this.chkModCountry, "chkModCountry");
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance43");
		((UltraToggleEditorBase)this.chkModCountry).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.chkModCountry).Name = "chkModCountry";
		((UltraToggleEditorBase)this.chkModCountry).CheckedChanged += new System.EventHandler(chkModCountry_CheckedChanged);
		resources.ApplyResources(this.chkModClass1, "chkModClass1");
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val14, "appearance44");
		((UltraToggleEditorBase)this.chkModClass1).Appearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.chkModClass1).Name = "chkModClass1";
		((UltraToggleEditorBase)this.chkModClass1).CheckedChanged += new System.EventHandler(chkModClass1_CheckedChanged);
		resources.ApplyResources(this.chkModClass3, "chkModClass3");
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val15, "appearance45");
		((UltraToggleEditorBase)this.chkModClass3).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.chkModClass3).Name = "chkModClass3";
		((UltraToggleEditorBase)this.chkModClass3).CheckedChanged += new System.EventHandler(chkModClass3_CheckedChanged);
		resources.ApplyResources(this.cboBrand, "cboBrand");
		this.cboBrand.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboBrand).Name = "cboBrand";
		resources.ApplyResources(this.lblBrand, "lblBrand");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val16, "appearance46");
		((ControlBase)this.lblBrand).Appearance = (AppearanceBase)(object)val16;
		this.lblBrand.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBrand).Name = "lblBrand";
		((ControlBase)this.lblBrand).WrapText = false;
		resources.ApplyResources(this.cboAgeGroup, "cboAgeGroup");
		this.cboAgeGroup.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboAgeGroup).Name = "cboAgeGroup";
		resources.ApplyResources(this.lblAgeGroup, "lblAgeGroup");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Navy;
		((AppearanceBase)val17).Image = resources.GetObject("appearance47.Image");
		resources.ApplyResources(val17, "appearance47");
		((ControlBase)this.lblAgeGroup).Appearance = (AppearanceBase)(object)val17;
		this.lblAgeGroup.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAgeGroup).Name = "lblAgeGroup";
		((ControlBase)this.lblAgeGroup).WrapText = false;
		((System.Windows.Forms.Control)(object)this.lblAgeGroup).Click += new System.EventHandler(lblAgeGroup_Click);
		resources.ApplyResources(this.cboClassification4, "cboClassification4");
		this.cboClassification4.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClassification4).Name = "cboClassification4";
		resources.ApplyResources(this.lblClassification4, "lblClassification4");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val18).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val18, "appearance48");
		((ControlBase)this.lblClassification4).Appearance = (AppearanceBase)(object)val18;
		this.lblClassification4.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClassification4).Name = "lblClassification4";
		((ControlBase)this.lblClassification4).WrapText = false;
		resources.ApplyResources(this.cboClassification3, "cboClassification3");
		this.cboClassification3.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClassification3).Name = "cboClassification3";
		resources.ApplyResources(this.lblClassification3, "lblClassification3");
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		((AppearanceBase)val19).Image = resources.GetObject("appearance49.Image");
		resources.ApplyResources(val19, "appearance49");
		((ControlBase)this.lblClassification3).Appearance = (AppearanceBase)(object)val19;
		this.lblClassification3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClassification3).Name = "lblClassification3";
		((ControlBase)this.lblClassification3).WrapText = false;
		resources.ApplyResources(this.cboClassification2, "cboClassification2");
		this.cboClassification2.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClassification2).Name = "cboClassification2";
		resources.ApplyResources(this.lblClassification2, "lblClassification2");
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val20, "appearance50");
		((ControlBase)this.lblClassification2).Appearance = (AppearanceBase)(object)val20;
		this.lblClassification2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClassification2).Name = "lblClassification2";
		((ControlBase)this.lblClassification2).WrapText = false;
		resources.ApplyResources(this.cboClassification1, "cboClassification1");
		this.cboClassification1.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClassification1).Name = "cboClassification1";
		resources.ApplyResources(this.lblClassification1, "lblClassification1");
		((AppearanceBase)val21).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val21).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val21, "appearance51");
		((ControlBase)this.lblClassification1).Appearance = (AppearanceBase)(object)val21;
		this.lblClassification1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClassification1).Name = "lblClassification1";
		((ControlBase)this.lblClassification1).WrapText = false;
		resources.ApplyResources(this.cboSeason, "cboSeason");
		this.cboSeason.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSeason).Name = "cboSeason";
		resources.ApplyResources(this.lblSeason, "lblSeason");
		((AppearanceBase)val22).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val22).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val22, "appearance52");
		((ControlBase)this.lblSeason).Appearance = (AppearanceBase)(object)val22;
		this.lblSeason.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSeason).Name = "lblSeason";
		((ControlBase)this.lblSeason).WrapText = false;
		resources.ApplyResources(this.cboCountry, "cboCountry");
		this.cboCountry.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCountry).Name = "cboCountry";
		resources.ApplyResources(this.lblCountry, "lblCountry");
		((AppearanceBase)val23).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val23).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val23, "appearance53");
		((ControlBase)this.lblCountry).Appearance = (AppearanceBase)(object)val23;
		this.lblCountry.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCountry).Name = "lblCountry";
		((ControlBase)this.lblCountry).WrapText = false;
		resources.ApplyResources(this.cboMaterial2, "cboMaterial2");
		this.cboMaterial2.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboMaterial2).Name = "cboMaterial2";
		resources.ApplyResources(this.lblMaterial2, "lblMaterial2");
		((AppearanceBase)val24).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val24).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val24, "appearance54");
		((ControlBase)this.lblMaterial2).Appearance = (AppearanceBase)(object)val24;
		this.lblMaterial2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMaterial2).Name = "lblMaterial2";
		((ControlBase)this.lblMaterial2).WrapText = false;
		resources.ApplyResources(this.cboMaterial1, "cboMaterial1");
		this.cboMaterial1.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboMaterial1).Name = "cboMaterial1";
		resources.ApplyResources(this.lblMaterial1, "lblMaterial1");
		((AppearanceBase)val25).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val25).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val25, "appearance55");
		((ControlBase)this.lblMaterial1).Appearance = (AppearanceBase)(object)val25;
		this.lblMaterial1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMaterial1).Name = "lblMaterial1";
		((ControlBase)this.lblMaterial1).WrapText = false;
		resources.ApplyResources(this.cboShape, "cboShape");
		this.cboShape.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboShape).Name = "cboShape";
		resources.ApplyResources(this.lblShape, "lblShape");
		((AppearanceBase)val26).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val26).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val26, "appearance56");
		((ControlBase)this.lblShape).Appearance = (AppearanceBase)(object)val26;
		this.lblShape.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblShape).Name = "lblShape";
		((ControlBase)this.lblShape).WrapText = false;
		resources.ApplyResources(this.cboModel, "cboModel");
		this.cboModel.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboModel).Name = "cboModel";
		resources.ApplyResources(this.lblModel, "lblModel");
		((AppearanceBase)val27).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val27).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val27, "appearance57");
		((ControlBase)this.lblModel).Appearance = (AppearanceBase)(object)val27;
		this.lblModel.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblModel).Name = "lblModel";
		((ControlBase)this.lblModel).WrapText = false;
		resources.ApplyResources(this.chkModShape, "chkModShape");
		((AppearanceBase)val28).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val28, "appearance58");
		((UltraToggleEditorBase)this.chkModShape).Appearance = (AppearanceBase)(object)val28;
		((System.Windows.Forms.Control)(object)this.chkModShape).Name = "chkModShape";
		((UltraToggleEditorBase)this.chkModShape).CheckedChanged += new System.EventHandler(chkModShape_CheckedChanged);
		resources.ApplyResources(this.chkModMaterial2, "chkModMaterial2");
		((AppearanceBase)val29).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val29, "appearance59");
		((UltraToggleEditorBase)this.chkModMaterial2).Appearance = (AppearanceBase)(object)val29;
		((System.Windows.Forms.Control)(object)this.chkModMaterial2).Name = "chkModMaterial2";
		((UltraToggleEditorBase)this.chkModMaterial2).CheckedChanged += new System.EventHandler(chkModMaterial2_CheckedChanged);
		resources.ApplyResources(this.chkModModel, "chkModModel");
		((AppearanceBase)val30).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val30, "appearance60");
		((UltraToggleEditorBase)this.chkModModel).Appearance = (AppearanceBase)(object)val30;
		((System.Windows.Forms.Control)(object)this.chkModModel).Name = "chkModModel";
		((UltraToggleEditorBase)this.chkModModel).CheckedChanged += new System.EventHandler(chkModModel_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboShape);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblShape);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboModel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblModel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboMaterial2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMaterial2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboMaterial1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMaterial1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCountry);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCountry);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSeason);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSeason);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClassification2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClassification2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClassification1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClassification1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClassification4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClassification4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClassification3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClassification3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboAgeGroup);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAgeGroup);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBrand);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBrand);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModClass3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModClass2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModGender);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModClass1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModBrand);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModModel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModCountry);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModMaterial2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModSeason);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModClass4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModShape);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModMaterial1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSaveAndClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRoot);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboGroup);
		base.Name = "frmUpdateItemsClassifications";
		base.Load += new System.EventHandler(frmUpdateItemsClassifications_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboGroup, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRoot, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSaveAndClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModMaterial1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModShape, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModClass4, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModSeason, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModMaterial2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModCountry, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModModel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModBrand, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModClass1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModGender, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModClass2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModClass3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBrand, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBrand, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAgeGroup, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboAgeGroup, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClassification3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClassification3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClassification4, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClassification4, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClassification1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClassification1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClassification2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClassification2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSeason, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSeason, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCountry, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCountry, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMaterial1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboMaterial1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMaterial2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboMaterial2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblModel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboModel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblShape, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboShape, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboGroup).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModMaterial1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModClass4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModBrand).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModGender).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModClass2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModSeason).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModCountry).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModClass1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModClass3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBrand).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAgeGroup).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClassification1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSeason).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaterial2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaterial1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboShape).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboModel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModShape).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModMaterial2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModModel).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
