using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Clinics;
using BusinessLayer.General;
using BusinessLayer.HR;
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

namespace ERP.Clinics.MasterData;

public class frmBioAnalysisTree : frmTree2
{
	private DataTable dtPricesTypes;

	private DataTable dtBranches;

	private DataTable dtBioAnalysisPrices;

	private DataTable dtBioAnalysisMaterials;

	private DataTable dtBioAnalysisRanges;

	private DataTable dtBioAnalysisRecipe;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private DataTable dtLabs;

	private DataTable dtItems;

	private DataTable dtGender;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtUnitGroup;

	private DataTable dtUnits;

	private DataTable dtBioAnalysis;

	private DataTable dtBioAnalysisTypes;

	private ValueList vlPricesTypes = new ValueList();

	private ValueList vlBranches = new ValueList();

	private ValueList vlBranches2 = new ValueList();

	private ValueList vlBranches3 = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlItems = new ValueList();

	private ValueList vlBioAnalysis = new ValueList();

	private ValueList vlBarCode = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlItemSizes = new ValueList();

	private ValueList vlGenders = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private IContainer components = null;

	private UltraTextEditor txtNotes;

	private UltraLabel lblReceivingBank;

	public UltraTabControl tabItemType;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl tabItem;

	private UltraTextEditor txtInternationalCode;

	private UltraLabel lblInternationalCode;

	private UltraCheckEditor chkHasAttachement;

	private UltraCheckEditor chkIsActive;

	private OpenFileDialog ofdItemPic;

	private UltraCheckEditor chkIsIndoor;

	private UltraTextEditor txtSamplesCount;

	private UltraLabel lblSamplesCount;

	private UltraPanel ultraPanel1;

	private RadioButton rbPriceForEachBranches;

	private RadioButton rbPriceForAllBranches;

	private UltraTabPageControl ultraTabPageControl1;

	public UltraGrid ULGPrices;

	private ToolStripMenuItem modifyGroupItemsToolStripMenuItem;

	private ToolStripMenuItem modifyGroupItemsToolStripMenuItem1;

	private ToolStripMenuItem modifyGroupItemsToolStripMenuItem2;

	private ToolStripMenuItem modifyGroupItemsToolStripMenuItem3;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem modifyGroupItemsToolStripMenuItem4;

	private ToolStripMenuItem deleteToolStripMenuItem;

	private UltraComboEditor cboTypes;

	private UltraLabel lblTypes;

	private UltraTextEditor txtBriefCode;

	private UltraLabel lblBriefCode;

	private ToolStripMenuItem changeParentToolStripMenuItem;

	private UltraTextEditor txtDescription;

	private UltraLabel lblDescription;

	private UltraTextEditor txtSamplesPeriod;

	private UltraLabel lblSamplesPeriod;

	private UltraLabel ultraLabel1;

	private UltraCheckEditor chkIsSalesItem;

	private UltraCheckEditor chkIsRecipe;

	private UltraTabPageControl ultraTabPageControl2;

	private UltraTabPageControl ultraTabPageControl3;

	private UltraTabPageControl ultraTabPageControl4;

	public UltraGrid ULGRanges;

	public UltraGrid ULGRecipeItems;

	public UltraGrid ULGMaterials;

	private UltraComboEditor cboUnitGroup;

	private UltraComboEditor cboUnit;

	private UltraLabel lblUnitGroup;

	private UltraLabel lblUnit;

	private UltraComboEditor cboDefaultLab;

	private UltraLabel lblDefaultLab;

	public UltraButton btnAccountSearch;

	private UltraComboEditor cboAccount;

	private UltraLabel lblAccountName;

	public UltraButton btnSubAccountSearch;

	private UltraComboEditor cboSubAccount;

	private UltraLabel lblSubAccount;

	private UltraTextEditor txtMinDeliveryDays;

	private UltraLabel lblMinDeliveryDays;

	public frmBioAnalysisTree()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		InitializeComponent();
		IDCol = "BioAnalysisID";
		NoCol = "BioAnalysisNumber";
		NameCol = "BioAnalysisNameAr";
		NameEnCol = "BioAnalysisNameEn";
		ParentIDCol = "ParentID";
		IsMainCol = "IsMain";
		ItemLevelCol = "LevelID";
		AdditionalCol1 = "InternationalCode";
		TableName = "CL_BioAnalysis";
		LevelsTable = "CL_BioAnalysis_Levels";
		LevelsCol = "LevelID";
		LevelsWidthCol = "Width";
		AllowAddRoot = true;
	}

	public override void PrepareData()
	{
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		OrderByName = GlobalFunctions.GetOption("Name-Code(ItemTree)");
		base.PrepareData();
		dtLabs = Labs.FillCombo("-1", "1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboDefaultLab, dtLabs, "LabID", "LabName");
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboAccount, dtAccounts, "AccountID", "Name");
		dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSubAccount, dtSubAccounts, "SubAccountID", "Name");
		dtBioAnalysis = BioAnalysis.FillCombo("0", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlBioAnalysis.ValueListItems.Clear();
		for (int i = 0; i < dtBioAnalysis.Rows.Count; i++)
		{
			vlBioAnalysis.ValueListItems.Add(dtBioAnalysis.Rows[i]["BioAnalysisID"], dtBioAnalysis.Rows[i]["Name"].ToString());
		}
		dtItems = Items.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlItems.ValueListItems.Clear();
		vlBarCode.ValueListItems.Clear();
		for (int j = 0; j < dtItems.Rows.Count; j++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[j]["ItemID"], dtItems.Rows[j]["Name"].ToString());
			vlBarCode.ValueListItems.Add(dtItems.Rows[j]["ItemID"], dtItems.Rows[j]["ItemBarCode"].ToString());
		}
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
		dtGender = Gender.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlGenders.ValueListItems.Clear();
		for (int m = 0; m < dtGender.Rows.Count; m++)
		{
			vlGenders.ValueListItems.Add(dtGender.Rows[m]["GenderID"], dtGender.Rows[m]["GenderName"].ToString());
		}
		dtUnitGroup = UnitsTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboUnitGroup, dtUnitGroup, "UnitTypeID", "UnitTypeName");
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboUnit, dtUnits, "UnitID", "UnitName");
		vlUnits.ValueListItems.Clear();
		for (int n = 0; n < dtUnits.Rows.Count; n++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[n]["UnitID"], dtUnits.Rows[n]["UnitName"].ToString());
		}
		if (UsingColors)
		{
			dtItemsColorCategorysDetails = ItemsColorCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			vlColors.ValueListItems.Clear();
			for (int num = 0; num < dtColors.Rows.Count; num++)
			{
				vlColors.ValueListItems.Add(dtColors.Rows[num]["ColorID"], dtColors.Rows[num]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtItemsSizeCategorysDetails = ItemsSizeCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			vlItemSizes.ValueListItems.Clear();
			for (int num2 = 0; num2 < dtSizes.Rows.Count; num2++)
			{
				vlItemSizes.ValueListItems.Add(dtSizes.Rows[num2]["ItemSizeID"], dtSizes.Rows[num2]["ItemSizeName"].ToString());
			}
		}
		dtBioAnalysisTypes = BioAnalysisTypes.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboTypes, dtBioAnalysisTypes, "BioAnalysisTypeID", GlobalVariables.IsArabic ? "BioAnalysisTypeNameAr" : "BioAnalysisTypeNameEn");
		dtBioAnalysisPrices = BioAnalysisPrices.SelectByBioAnalysisID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGridPrices();
		dtBioAnalysisMaterials = BioAnalysisMaterials.SelectByBioAnalysisID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGridMaterials();
		dtBioAnalysisRanges = BioAnalysisRanges.SelectByBioAnalysisID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGridRanges();
		dtBioAnalysisRecipe = BioAnalysisRecipes.SelectByBioAnalysisID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
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
			((Control)(object)txtCode).Text = BioAnalysis.GetCode((SelectedNode == null) ? "Null" : ((KeyedSubObjectBase)SelectedNode).Key, IsFromServer: true);
			dtBioAnalysisPrices.Rows.Clear();
			dtBioAnalysisRecipe.Rows.Clear();
			dtBioAnalysisRanges.Rows.Clear();
			dtBioAnalysisMaterials.Rows.Clear();
			((TextEditorControlBase)txtInternationalCode).Clear();
			((TextEditorControlBase)txtBriefCode).Clear();
			((TextEditorControlBase)txtDescription).Clear();
			((TextEditorControlBase)txtNotes).Clear();
			((TextEditorControlBase)txtSamplesCount).Clear();
			((TextEditorControlBase)txtSamplesPeriod).Clear();
			FillGridPrices();
			((Control)(object)txtName).Select();
			((UltraToggleEditorBase)chkHasAttachement).Checked = false;
			cboUnit.SelectedIndex = -1;
			((Control)(object)txtMinDeliveryDays).Text = "0";
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)txtInternationalCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSamplesCount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBriefCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDescription).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSamplesPeriod).ReadOnly = NavMode;
		((Control)(object)chkIsActive).Enabled = !NavMode;
		((Control)(object)chkIsSalesItem).Enabled = !NavMode;
		((Control)(object)chkIsRecipe).Enabled = !NavMode;
		((Control)(object)chkIsIndoor).Enabled = !NavMode;
		((Control)(object)chkHasAttachement).Enabled = !NavMode;
		((EditorButtonControlBase)cboTypes).ReadOnly = NavMode;
		rbPriceForAllBranches.Enabled = !NavMode;
		rbPriceForEachBranches.Enabled = !NavMode;
		((EditorButtonControlBase)cboUnitGroup).ReadOnly = NavMode;
		((EditorButtonControlBase)cboUnit).ReadOnly = NavMode;
		((EditorButtonControlBase)cboDefaultLab).ReadOnly = NavMode;
		((EditorButtonControlBase)cboAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSubAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtMinDeliveryDays).ReadOnly = NavMode;
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (SelectedNode != null)
		{
			DataRow dataRow = BioAnalysis.Select(((KeyedSubObjectBase)SelectedNode).Key, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true).Rows[0];
			((TextEditorControlBase)cboTypes).Value = dataRow["BioAnalysisTypeID"];
			((UltraToggleEditorBase)chkIsActive).Checked = Convert.ToBoolean(dataRow["IsActive"]);
			((UltraToggleEditorBase)chkIsIndoor).Checked = Convert.ToBoolean(dataRow["IsIndoor"]);
			((UltraToggleEditorBase)chkIsSalesItem).Checked = Convert.ToBoolean(dataRow["IsSalesItem"]);
			((UltraToggleEditorBase)chkIsRecipe).Checked = Convert.ToBoolean(dataRow["IsRecipe"]);
			((UltraToggleEditorBase)chkHasAttachement).Checked = Convert.ToBoolean(dataRow["HasAttachement"]);
			rbPriceForAllBranches.Checked = Convert.ToBoolean(dataRow["PriceForAllBranch"]);
			rbPriceForEachBranches.Checked = !Convert.ToBoolean(dataRow["PriceForAllBranch"]);
			modifyGroupItemsToolStripMenuItem4.Enabled = Convert.ToBoolean(dataRow["IsMain"]);
			deleteToolStripMenuItem.Enabled = Convert.ToBoolean(dataRow["IsMain"]);
			changeParentToolStripMenuItem.Enabled = !Convert.ToBoolean(dataRow["IsMain"]);
			((Control)(object)txtInternationalCode).Text = dataRow["InternationalCode"].ToString();
			((Control)(object)txtNotes).Text = dataRow["Notes"].ToString();
			((Control)(object)txtDescription).Text = dataRow["Description"].ToString();
			((Control)(object)txtBriefCode).Text = dataRow["BriefCode"].ToString();
			((Control)(object)txtSamplesCount).Text = dataRow["SamplesCount"].ToString();
			((Control)(object)txtSamplesPeriod).Text = dataRow["SamplesPeriod"].ToString();
			((Control)(object)txtMinDeliveryDays).Text = dataRow["MinDeliveryDays"].ToString();
			((TextEditorControlBase)cboDefaultLab).Value = dataRow["DefaultLabID"].ToString();
			((TextEditorControlBase)cboTypes).Value = dataRow["BioAnalysisTypeID"].ToString();
			((TextEditorControlBase)cboUnitGroup).Value = dataRow["BioAnalysisUnitTypeID"].ToString();
			((TextEditorControlBase)cboUnit).Value = dataRow["BioAnalysisUnitID"].ToString();
			((TextEditorControlBase)cboAccount).Value = dataRow["SalesAccountID"].ToString();
			((TextEditorControlBase)cboSubAccount).Value = dataRow["SalesSubAccountID"].ToString();
			dtBioAnalysisPrices = BioAnalysisPrices.SelectByBioAnalysisID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			InitGridPrices();
			if (((UltraToggleEditorBase)chkIsRecipe).Checked)
			{
				dtBioAnalysisRecipe = BioAnalysisRecipes.SelectByBioAnalysisID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
				((UltraGridBase)ULGRecipeItems).DataSource = dtBioAnalysisRecipe;
			}
			else
			{
				dtBioAnalysisRecipe.Rows.Clear();
			}
			InitGridRecipeItems();
			dtBioAnalysisMaterials = BioAnalysisMaterials.SelectByBioAnalysisID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			InitGridMaterials();
			dtBioAnalysisRanges = BioAnalysisRanges.SelectByBioAnalysisID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			InitGridRanges();
		}
	}

	public override bool ValidateData()
	{
		DataRow[] array = dtChart.Select(AdditionalCol1 + " = '" + ((((Control)(object)txtInternationalCode).Text.Trim() == "") ? GetCode() : ((Control)(object)txtInternationalCode).Text) + "'");
		if ((array.Length != 0 && (Adding || array[0][IDCol].ToString() != ((KeyedSubObjectBase)SelectedNode).Key)) || array.Length > 1)
		{
			GlobalVariables.InformationMB.Show("هذا الباركود موجود من قبل", "This BarCode is Already Exists..");
			return false;
		}
		if (cboAccount.SelectedIndex > -1 && cboSubAccount.SelectedIndex == -1 && dtSubAccounts.Select("AccountID = " + ((TextEditorControlBase)cboAccount).Value.ToString()).Length != 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار الحساب التحليلي ", "Please select SubAccount ");
			((TextEditorControlBase)cboSubAccount).Focus();
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGRecipeItems).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGRecipeItems).Rows[i].Cells["RecipeBioAnalysisID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("يجب اختيار مكون للوصفه", "Choose a BioAnalysis");
				((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Recipe"];
				ULGRecipeItems.ActiveCell = ((UltraGridBase)ULGRecipeItems).Rows[i].Cells["RecipeBioAnalysisID"];
				return false;
			}
			if (((UltraGridBase)ULGRecipeItems).Rows[i].Cells["RecipeBioAnalysisID"].Value.ToString() == ((KeyedSubObjectBase)SelectedNode).Key)
			{
				GlobalVariables.InformationMB.Show("لا يمكن اختيار نفس التحليل في الوصفه", "Useing same BioAnalysis Is not allowed");
				((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Recipe"];
				ULGRecipeItems.ActiveCell = ((UltraGridBase)ULGRecipeItems).Rows[i].Cells["RecipeBioAnalysisID"];
				return false;
			}
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGRanges).Rows).Count; j++)
		{
			if (((UltraGridBase)ULGRanges).Rows[j].Cells["FromAge"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show(" برجاء إدخال العمر", "Please Enter From Age");
				((UltraTabControlBase)tabItemType).Tabs["Ranges"].Selected = true;
				return false;
			}
			if (((UltraGridBase)ULGRanges).Rows[j].Cells["ToAge"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show(" برجاء إدخال العمر", "Please Enter To Age");
				((UltraTabControlBase)tabItemType).Tabs["Ranges"].Selected = true;
				return false;
			}
			if (((UltraGridBase)ULGRanges).Rows[j].Cells["FromRange"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show(" برجاء إدخال النتيجه", "Please Enter From Range");
				((UltraTabControlBase)tabItemType).Tabs["Ranges"].Selected = true;
				return false;
			}
			if (((UltraGridBase)ULGRanges).Rows[j].Cells["ToRange"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show(" برجاء إدخال النتيجه", "Please Enter To Range");
				((UltraTabControlBase)tabItemType).Tabs["Ranges"].Selected = true;
				return false;
			}
		}
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGPrices).Rows).Count; k++)
		{
			if (((UltraGridBase)ULGPrices).Rows[k].Cells["Price"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show(" برجاء إدخال السعر", "Please Enter Price");
				((UltraTabControlBase)tabItemType).Tabs["Prices"].Selected = true;
				return false;
			}
		}
		for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGMaterials).Rows).Count; l++)
		{
			if (((UltraGridBase)ULGMaterials).Rows[l].Cells["ItemID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("يجب اختيار الصنف ", "Choose Item");
				((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Materials"];
				ULGMaterials.ActiveCell = ((UltraGridBase)ULGMaterials).Rows[l].Cells["ItemID"];
				return false;
			}
			if (((UltraGridBase)ULGMaterials).Rows[l].Cells["UnitID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("يجب اختيار وحدة ", "Choose Unit");
				((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Materials"];
				ULGMaterials.ActiveCell = ((UltraGridBase)ULGMaterials).Rows[l].Cells["UnitID"];
				return false;
			}
		}
		return base.ValidateData();
	}

	public override int TreeAddData()
	{
		int result = 0;
		Main.StartBulkTrans(FromServer: true);
		try
		{
			result = BioAnalysis.Insert_Update("-1", GetCode(), ((Control)(object)txtName).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? ((Control)(object)txtName).Text.Trim() : ((Control)(object)txtNameEn).Text.Trim(), (SelectedNode == null) ? "Null" : ((DataRow)((SubObjectBase)SelectedNode).Tag)[0].ToString(), "0", (NodeLevel + 1).ToString(), ((UltraToggleEditorBase)chkIsSalesItem).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsRecipe).Checked ? "1" : "0", (((Control)(object)txtInternationalCode).Text.Trim() == "") ? GetCode() : ((Control)(object)txtInternationalCode).Text, (((Control)(object)txtBriefCode).Text.Trim() == "") ? "Null" : ((Control)(object)txtBriefCode).Text, (((Control)(object)txtDescription).Text.Trim() == "") ? "Null" : ((Control)(object)txtDescription).Text, (cboTypes.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTypes).Value.ToString(), (cboUnitGroup.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUnitGroup).Value.ToString(), (cboUnit.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUnit).Value.ToString(), (cboDefaultLab.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDefaultLab).Value.ToString(), (((Control)(object)txtMinDeliveryDays).Text.Trim() == "") ? "0" : ((Control)(object)txtMinDeliveryDays).Text, (((Control)(object)txtSamplesCount).Text.Trim() == "") ? "0" : ((Control)(object)txtSamplesCount).Text, (((Control)(object)txtSamplesPeriod).Text.Trim() == "") ? "0" : ((Control)(object)txtSamplesPeriod).Text, ((UltraToggleEditorBase)chkIsIndoor).Checked ? "1" : "0", ((UltraToggleEditorBase)chkHasAttachement).Checked ? "1" : "0", rbPriceForAllBranches.Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsActive).Checked ? "1" : "0", (cboAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAccount).Value.ToString(), (cboSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccount).Value.ToString(), (((Control)(object)txtNotes).Text.Trim() == "") ? "Null" : ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID.ToString(), GlobalVariables.UserID, IsFromServer: true);
			dtBioAnalysisPrices.AcceptChanges();
			for (int i = 0; i < dtBioAnalysisPrices.Rows.Count; i++)
			{
				BioAnalysisPrices.Insert_Update("-1", result.ToString(), dtBioAnalysisPrices.Rows[i]["PriceTypeID"].ToString(), dtBioAnalysisPrices.Rows[i]["Price"].ToString(), "0", rbPriceForAllBranches.Checked ? "Null" : dtBioAnalysisPrices.Rows[i]["BranchID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			}
			dtBioAnalysisRanges.AcceptChanges();
			for (int j = 0; j < dtBioAnalysisRanges.Rows.Count; j++)
			{
				BioAnalysisRanges.Insert_Update("-1", result.ToString(), (dtBioAnalysisRanges.Rows[j]["GenderID"] == DBNull.Value) ? "Null" : dtBioAnalysisRanges.Rows[j]["GenderID"].ToString(), dtBioAnalysisRanges.Rows[j]["FromAge"].ToString(), dtBioAnalysisRanges.Rows[j]["ToAge"].ToString(), dtBioAnalysisRanges.Rows[j]["FromRange"].ToString(), dtBioAnalysisRanges.Rows[j]["ToRange"].ToString(), dtBioAnalysisRanges.Rows[j]["RangeDescription"].ToString(), "0", GlobalVariables.CurrentBranchID.ToString(), GlobalVariables.UserID, IsFromServer: true);
			}
			dtBioAnalysisRecipe.AcceptChanges();
			for (int k = 0; k < dtBioAnalysisRecipe.Rows.Count; k++)
			{
				BioAnalysisRecipes.Insert_Update("-1", result.ToString(), (dtBioAnalysisRecipe.Rows[k]["RecipeBioAnalysisID"] == DBNull.Value) ? "Null" : dtBioAnalysisRecipe.Rows[k]["RecipeBioAnalysisID"].ToString(), dtBioAnalysisRecipe.Rows[k]["Notes"].ToString(), "0", GlobalVariables.CurrentBranchID.ToString(), GlobalVariables.UserID, IsFromServer: true);
			}
			dtBioAnalysisMaterials.AcceptChanges();
			for (int l = 0; l < dtBioAnalysisMaterials.Rows.Count; l++)
			{
				BioAnalysisMaterials.Insert_Update("-1", result.ToString(), (dtBioAnalysisMaterials.Rows[l]["ItemID"] == DBNull.Value) ? "Null" : dtBioAnalysisMaterials.Rows[l]["ItemID"].ToString(), (dtBioAnalysisMaterials.Rows[l]["ColorID"] == DBNull.Value) ? "1" : dtBioAnalysisMaterials.Rows[l]["ColorID"].ToString(), (dtBioAnalysisMaterials.Rows[l]["ItemSizeID"] == DBNull.Value) ? "1" : dtBioAnalysisMaterials.Rows[l]["ItemSizeID"].ToString(), dtBioAnalysisMaterials.Rows[l]["Qty"].ToString(), dtBioAnalysisMaterials.Rows[l]["UnitID"].ToString(), dtBioAnalysisMaterials.Rows[l]["Notes"].ToString(), "0", GlobalVariables.CurrentBranchID.ToString(), GlobalVariables.UserID, IsFromServer: true);
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
		return result;
	}

	public override void TreeUpdateData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			BioAnalysis.Insert_Update(((KeyedSubObjectBase)SelectedNode).Key, GetCode(), ((Control)(object)txtName).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? "Null" : ((Control)(object)txtNameEn).Text.Trim(), (SelectedNode.Parent == null) ? "Null" : ((KeyedSubObjectBase)SelectedNode.Parent).Key, ((bool)((DataRow)((SubObjectBase)SelectedNode).Tag)[IsMainCol]) ? "1" : "0", (NodeLevel + 1).ToString(), ((UltraToggleEditorBase)chkIsSalesItem).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsRecipe).Checked ? "1" : "0", (((Control)(object)txtInternationalCode).Text.Trim() == "") ? GetCode() : ((Control)(object)txtInternationalCode).Text, (((Control)(object)txtBriefCode).Text.Trim() == "") ? "Null" : ((Control)(object)txtBriefCode).Text, (((Control)(object)txtDescription).Text.Trim() == "") ? "Null" : ((Control)(object)txtDescription).Text, (cboTypes.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTypes).Value.ToString(), (cboUnitGroup.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUnitGroup).Value.ToString(), (cboUnit.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUnit).Value.ToString(), (cboDefaultLab.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDefaultLab).Value.ToString(), (((Control)(object)txtMinDeliveryDays).Text.Trim() == "") ? "0" : ((Control)(object)txtMinDeliveryDays).Text, (((Control)(object)txtSamplesCount).Text.Trim() == "") ? "0" : ((Control)(object)txtSamplesCount).Text, (((Control)(object)txtSamplesPeriod).Text.Trim() == "") ? "0" : ((Control)(object)txtSamplesPeriod).Text, ((UltraToggleEditorBase)chkIsIndoor).Checked ? "1" : "0", ((UltraToggleEditorBase)chkHasAttachement).Checked ? "1" : "0", rbPriceForAllBranches.Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsActive).Checked ? "1" : "0", (cboAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAccount).Value.ToString(), (cboSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccount).Value.ToString(), (((Control)(object)txtNotes).Text.Trim() == "") ? "Null" : ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID.ToString(), GlobalVariables.UserID, IsFromServer: true);
			BioAnalysisPrices.DeleteByBioAnalysisID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			dtBioAnalysisPrices.AcceptChanges();
			for (int i = 0; i < dtBioAnalysisPrices.Rows.Count; i++)
			{
				BioAnalysisPrices.Insert_Update("-1", ((KeyedSubObjectBase)SelectedNode).Key, dtBioAnalysisPrices.Rows[i]["PriceTypeID"].ToString(), dtBioAnalysisPrices.Rows[i]["Price"].ToString(), "0", rbPriceForAllBranches.Checked ? "Null" : dtBioAnalysisPrices.Rows[i]["BranchID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			}
			dtBioAnalysisRanges.AcceptChanges();
			BioAnalysisRanges.DeleteByBioAnalysisID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			if (dtBioAnalysisRanges.Rows.Count > 0)
			{
				for (int j = 0; j < dtBioAnalysisRanges.Rows.Count; j++)
				{
					dtBioAnalysisRanges.Rows[j]["BioAnalysisID"] = ((KeyedSubObjectBase)SelectedNode).Key;
					dtBioAnalysisRanges.Rows[j]["BioAnalysisRangeID"] = -1;
					dtBioAnalysisRanges.Rows[j]["BranchID"] = GlobalVariables.CurrentBranchID;
				}
				BioAnalysisRanges.Insert_UpdateByTable(dtBioAnalysisRanges, GlobalVariables.UserID, IsFromServer: true);
			}
			dtBioAnalysisRecipe.AcceptChanges();
			BioAnalysisRecipes.DeleteByBioAnalysisID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			if (dtBioAnalysisRecipe.Rows.Count > 0)
			{
				for (int k = 0; k < dtBioAnalysisRecipe.Rows.Count; k++)
				{
					dtBioAnalysisRecipe.Rows[k]["BioAnalysisID"] = ((KeyedSubObjectBase)SelectedNode).Key;
					dtBioAnalysisRecipe.Rows[k]["BioAnalysisRecipeID"] = -1;
					dtBioAnalysisRecipe.Rows[k]["BranchID"] = GlobalVariables.CurrentBranchID;
				}
				BioAnalysisRecipes.Insert_UpdateByTable(dtBioAnalysisRecipe, GlobalVariables.UserID, IsFromServer: true);
			}
			dtBioAnalysisMaterials.AcceptChanges();
			BioAnalysisMaterials.DeleteByBioAnalysisID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			if (dtBioAnalysisMaterials.Rows.Count > 0)
			{
				for (int l = 0; l < dtBioAnalysisMaterials.Rows.Count; l++)
				{
					dtBioAnalysisMaterials.Rows[l]["BioAnalysisID"] = ((KeyedSubObjectBase)SelectedNode).Key;
					dtBioAnalysisMaterials.Rows[l]["BioAnalysisMaterialID"] = -1;
					dtBioAnalysisMaterials.Rows[l]["BranchID"] = GlobalVariables.CurrentBranchID;
					if (dtBioAnalysisMaterials.Rows[l]["ColorID"] == DBNull.Value)
					{
						dtBioAnalysisMaterials.Rows[l]["ColorID"] = "1";
					}
					if (dtBioAnalysisMaterials.Rows[l]["ItemSizeID"] == DBNull.Value)
					{
						dtBioAnalysisMaterials.Rows[l]["ItemSizeID"] = "1";
					}
				}
				BioAnalysisMaterials.Insert_UpdateByTable(dtBioAnalysisMaterials, GlobalVariables.UserID, IsFromServer: true);
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
			BioAnalysisMaterials.DeleteByBioAnalysisID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			BioAnalysisRecipes.DeleteByBioAnalysisID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			BioAnalysisRanges.DeleteByBioAnalysisID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			BioAnalysisPrices.DeleteByBioAnalysisID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			BioAnalysis.Delete(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
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
		string text = BioAnalysis.SelectRelations(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true).Rows[0]["Relations"].ToString().Replace("-", "\n");
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

	private void rbPriceForAllBranches_CheckedChanged(object sender, EventArgs e)
	{
		FillGridPrices();
	}

	private void FillGridPrices()
	{
		dtBioAnalysisPrices.Rows.Clear();
		if (rbPriceForAllBranches.Checked)
		{
			for (int i = 0; i < dtPricesTypes.Rows.Count; i++)
			{
				DataRow dataRow = dtBioAnalysisPrices.NewRow();
				dataRow["BioAnalysisPriceID"] = -1;
				dataRow["BioAnalysisID"] = -1;
				dataRow["PriceTypeID"] = dtPricesTypes.Rows[i]["PriceTypeID"];
				dataRow["Price"] = 0;
				dataRow["Deleted"] = false;
				dataRow["BranchID"] = DBNull.Value;
				dtBioAnalysisPrices.Rows.Add(dataRow);
			}
		}
		else
		{
			for (int j = 0; j < dtBranches.Rows.Count; j++)
			{
				for (int k = 0; k < dtPricesTypes.Rows.Count; k++)
				{
					DataRow dataRow2 = dtBioAnalysisPrices.NewRow();
					dataRow2["BioAnalysisPriceID"] = -1;
					dataRow2["BioAnalysisID"] = -1;
					dataRow2["PriceTypeID"] = dtPricesTypes.Rows[k]["PriceTypeID"];
					dataRow2["Price"] = 0;
					dataRow2["Deleted"] = false;
					dataRow2["BranchID"] = dtBranches.Rows[j]["BranchID"];
					dtBioAnalysisPrices.Rows.Add(dataRow2);
				}
			}
		}
		InitGridPrices();
	}

	private void InitGridPrices()
	{
		((UltraGridBase)ULGPrices).DataSource = dtBioAnalysisPrices;
		GlobalFunctions.PrepareGrid(ULGPrices);
		((UltraGridBase)ULGPrices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGPrices).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGPrices).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BioAnalysisPriceID"].DefaultCellValue = -1;
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

	private void InitGridMaterials()
	{
		((UltraGridBase)ULGMaterials).DataSource = dtBioAnalysisMaterials;
		GlobalFunctions.PrepareGrid(ULGMaterials);
		((UltraGridBase)ULGMaterials).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGMaterials).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGMaterials).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)1;
		((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGMaterials).Width * 0.05);
		((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGMaterials).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["ColorID"].Hidden = !UsingColors;
		((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["ItemSizeID"].Hidden = !UsingSizes;
		((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["ColorID"].ValueList = (IValueList)(object)vlColors;
		((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlItemSizes;
		((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["ColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["ItemSizeID"].DefaultCellValue = 1;
		((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["BioAnalysisMaterialID"].DefaultCellValue = -1;
		((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = GlobalVariables.CurrentBranchID;
		((HeaderBase)((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGMaterials).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGMaterials).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGMaterials).Width * 0.3) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGMaterials).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGMaterials).Width * 0.2);
	}

	private void InitGridRecipeItems()
	{
		((UltraGridBase)ULGRecipeItems).DataSource = dtBioAnalysisRecipe;
		GlobalFunctions.PrepareGrid(ULGRecipeItems);
		((UltraGridBase)ULGRecipeItems).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGRecipeItems).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGRecipeItems).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)1;
		((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["BioAnalysisRecipeID"].DefaultCellValue = -1;
		((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = GlobalVariables.CurrentBranchID;
		((HeaderBase)((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["RecipeBioAnalysisID"].Header).Caption = (GlobalVariables.IsArabic ? "التحليل" : "BioAnalysis");
		((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["RecipeBioAnalysisID"].Hidden = false;
		((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["RecipeBioAnalysisID"].ValueList = (IValueList)(object)vlBioAnalysis;
		((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["RecipeBioAnalysisID"].Width = (int)((double)((Control)(object)ULGRecipeItems).Width * 0.5) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGRecipeItems).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGRecipeItems).Width * 0.5);
	}

	private void InitGridRanges()
	{
		((UltraGridBase)ULGRanges).DataSource = dtBioAnalysisRanges;
		GlobalFunctions.PrepareGrid(ULGRanges);
		((UltraGridBase)ULGRanges).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGRanges).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGRanges).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)1;
		((UltraGridBase)ULGRanges).DisplayLayout.Bands[0].Columns["BioAnalysisRangeID"].DefaultCellValue = -1;
		((UltraGridBase)ULGRanges).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = GlobalVariables.CurrentBranchID;
		((HeaderBase)((UltraGridBase)ULGRanges).DisplayLayout.Bands[0].Columns["GenderID"].Header).Caption = (GlobalVariables.IsArabic ? "النوع" : "Gender");
		((UltraGridBase)ULGRanges).DisplayLayout.Bands[0].Columns["GenderID"].Hidden = false;
		((UltraGridBase)ULGRanges).DisplayLayout.Bands[0].Columns["GenderID"].ValueList = (IValueList)(object)vlGenders;
		((UltraGridBase)ULGRanges).DisplayLayout.Bands[0].Columns["GenderID"].Width = (int)((double)((Control)(object)ULGRanges).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGRanges).DisplayLayout.Bands[0].Columns["FromAge"].Header).Caption = (GlobalVariables.IsArabic ? "من سن" : "From Age");
		((UltraGridBase)ULGRanges).DisplayLayout.Bands[0].Columns["FromAge"].Hidden = false;
		((UltraGridBase)ULGRanges).DisplayLayout.Bands[0].Columns["FromAge"].Width = (int)((double)((Control)(object)ULGRanges).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGRanges).DisplayLayout.Bands[0].Columns["ToAge"].Header).Caption = (GlobalVariables.IsArabic ? "الى سن" : "To Age");
		((UltraGridBase)ULGRanges).DisplayLayout.Bands[0].Columns["ToAge"].Hidden = false;
		((UltraGridBase)ULGRanges).DisplayLayout.Bands[0].Columns["ToAge"].Width = (int)((double)((Control)(object)ULGRanges).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGRanges).DisplayLayout.Bands[0].Columns["FromRange"].Header).Caption = (GlobalVariables.IsArabic ? "من" : "From Range");
		((UltraGridBase)ULGRanges).DisplayLayout.Bands[0].Columns["FromRange"].Hidden = false;
		((UltraGridBase)ULGRanges).DisplayLayout.Bands[0].Columns["FromRange"].Width = (int)((double)((Control)(object)ULGRanges).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGRanges).DisplayLayout.Bands[0].Columns["ToRange"].Header).Caption = (GlobalVariables.IsArabic ? "الى" : "To Range");
		((UltraGridBase)ULGRanges).DisplayLayout.Bands[0].Columns["ToRange"].Hidden = false;
		((UltraGridBase)ULGRanges).DisplayLayout.Bands[0].Columns["ToRange"].Width = (int)((double)((Control)(object)ULGRanges).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGRanges).DisplayLayout.Bands[0].Columns["RangeDescription"].Header).Caption = (GlobalVariables.IsArabic ? "الوصف" : "Range Description");
		((UltraGridBase)ULGRanges).DisplayLayout.Bands[0].Columns["RangeDescription"].Hidden = false;
		((UltraGridBase)ULGRanges).DisplayLayout.Bands[0].Columns["RangeDescription"].Width = (int)((double)((Control)(object)ULGRanges).Width * 0.4);
		((UltraGridBase)ULGRanges).DisplayLayout.Bands[0].Columns["ToAge"].DefaultCellValue = 0;
		((UltraGridBase)ULGRanges).DisplayLayout.Bands[0].Columns["FromAge"].DefaultCellValue = 0;
	}

	public override void btnRefreshDataClick()
	{
		base.btnRefreshDataClick();
		dtLabs = Labs.FillCombo("-1", "1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboDefaultLab, dtLabs, "LabID", "LabName");
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboAccount, dtAccounts, "AccountID", "Name");
		dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSubAccount, dtSubAccounts, "SubAccountID", "Name");
		dtBioAnalysis = BioAnalysis.FillCombo("1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlBioAnalysis.ValueListItems.Clear();
		for (int i = 0; i < dtBioAnalysis.Rows.Count; i++)
		{
			vlBioAnalysis.ValueListItems.Add(dtBioAnalysis.Rows[i]["BioAnalysisID"], dtBioAnalysis.Rows[i]["Name"].ToString());
		}
		dtItems = Items.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlItems.ValueListItems.Clear();
		vlBarCode.ValueListItems.Clear();
		for (int j = 0; j < dtItems.Rows.Count; j++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[j]["ItemID"], dtItems.Rows[j]["Name"].ToString());
			vlBarCode.ValueListItems.Add(dtItems.Rows[j]["ItemID"], dtItems.Rows[j]["ItemBarCode"].ToString());
		}
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
		dtGender = Gender.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlGenders.ValueListItems.Clear();
		for (int m = 0; m < dtGender.Rows.Count; m++)
		{
			vlGenders.ValueListItems.Add(dtGender.Rows[m]["GenderID"], dtGender.Rows[m]["GenderName"].ToString());
		}
		dtUnitGroup = UnitsTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboUnitGroup, dtUnitGroup, "UnitTypeID", "UnitTypeName");
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboUnit, dtUnits, "UnitID", "UnitName");
		vlUnits.ValueListItems.Clear();
		for (int n = 0; n < dtUnits.Rows.Count; n++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[n]["UnitID"], dtUnits.Rows[n]["UnitName"].ToString());
		}
		if (UsingColors)
		{
			dtItemsColorCategorysDetails = ItemsColorCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			vlColors.ValueListItems.Clear();
			for (int num = 0; num < dtColors.Rows.Count; num++)
			{
				vlColors.ValueListItems.Add(dtColors.Rows[num]["ColorID"], dtColors.Rows[num]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtItemsSizeCategorysDetails = ItemsSizeCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			vlItemSizes.ValueListItems.Clear();
			for (int num2 = 0; num2 < dtSizes.Rows.Count; num2++)
			{
				vlItemSizes.ValueListItems.Add(dtSizes.Rows[num2]["ItemSizeID"], dtSizes.Rows[num2]["ItemSizeName"].ToString());
			}
		}
		dtBioAnalysisTypes = BioAnalysisTypes.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboTypes, dtBioAnalysisTypes, "BioAnalysisTypeID", GlobalVariables.IsArabic ? "BioAnalysisTypeNameAr" : "BioAnalysisTypeNameEn");
	}

	public override void btnPrintClick()
	{
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
			frmUpdateGroupBioAnalysis frmUpdateGroupBioAnalysis2 = new frmUpdateGroupBioAnalysis(((KeyedSubObjectBase)SelectedNode).Key);
			frmUpdateGroupBioAnalysis2.ShowDialog();
		}
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

	private void cboAccount_ValueChanged(object sender, EventArgs e)
	{
		if (cboAccount.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtSubAccounts);
			dataView.RowFilter = "AccountID=" + ((TextEditorControlBase)cboAccount).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboSubAccount.DataSource = dataView;
		}
	}

	private void btnAccountSearch_Click(object sender, EventArgs e)
	{
		((TextEditorControlBase)cboAccount).Value = SearchFunctions.Accounts(IsFromServer: true);
	}

	private void btnSubAccountSearch_Click(object sender, EventArgs e)
	{
		if (cboAccount.SelectedIndex != -1)
		{
			int num = SearchFunctions.SubAccounts(((TextEditorControlBase)cboAccount).Value.ToString(), IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboSubAccount).Value = num;
			}
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
		}
	}

	private void chkIsRecipe_CheckedChanged(object sender, EventArgs e)
	{
		((UltraTabControlBase)tabItemType).Tabs["Recipe"].Visible = ((UltraToggleEditorBase)chkIsRecipe).Checked;
	}

	private void ULGPrices_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGPrices.ActiveCell != null && ((KeyedSubObjectBase)ULGPrices.ActiveCell.Column).Key == "Price")
		{
			GlobalFunctions.CheckForNumbers(ULGPrices.ActiveCell, e);
		}
	}

	private void ULGRanges_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGRanges.ActiveCell != null && (((KeyedSubObjectBase)ULGRanges.ActiveCell.Column).Key == "FromAge" || ((KeyedSubObjectBase)ULGRanges.ActiveCell.Column).Key == "ToAge" || ((KeyedSubObjectBase)ULGRanges.ActiveCell.Column).Key == "ToRange" || ((KeyedSubObjectBase)ULGRanges.ActiveCell.Column).Key == "FromRange"))
		{
			GlobalFunctions.CheckForNumbers(ULGRanges.ActiveCell, e);
		}
	}

	private void ULGMaterials_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGMaterials.ActiveCell != null && ((KeyedSubObjectBase)ULGMaterials.ActiveCell.Column).Key == "Qty")
		{
			GlobalFunctions.CheckForNumbers(ULGMaterials.ActiveCell, e);
		}
	}

	private ValueList getUnitsValueList(int UnitTypeID)
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

	private ValueList getColorsValueList(int ItemColorCategoryID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtItemsColorCategorysDetails.Select("ItemColorCategoryID=" + ItemColorCategoryID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["ColorID"].ToString(), dtColors.Select("ColorID =" + array[i]["ColorID"].ToString())[0]["ColorName"].ToString());
		}
		return val;
	}

	private ValueList getSizesValueList(int ItemSizeCategoryID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtItemsSizeCategorysDetails.Select("ItemSizeCategoryID=" + ItemSizeCategoryID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["ItemSizeID"].ToString(), dtSizes.Select("ItemSizeID =" + array[i]["ItemSizeID"].ToString())[0]["ItemSizeName"].ToString());
		}
		return val;
	}

	private void ULGMaterials_CellListSelect(object sender, CellEventArgs e)
	{
		if ((((KeyedSubObjectBase)e.Cell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID") && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGMaterials).UpdateData();
			DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			UltraGridCell obj = ((UltraGridBase)ULGMaterials).ActiveRow.Cells["ItemBarCode"];
			object value = (((UltraGridBase)ULGMaterials).ActiveRow.Cells["ItemID"].Value = e.Cell.Value);
			obj.Value = value;
			((UltraGridBase)ULGMaterials).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			int num = ((((UltraGridBase)ULGMaterials).ActiveRow.Cells["UnitID"].Value != DBNull.Value) ? int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGMaterials).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()) : 0);
			if (num != 0)
			{
				e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
				e.Cell.Row.Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(num);
				((UltraGridBase)ULGMaterials).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			}
			else
			{
				e.Cell.Row.Cells["UnitID"].ValueList = null;
				e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
			}
			if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
			{
				e.Cell.Row.Cells["ColorID"].Value = DBNull.Value;
				e.Cell.Row.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
			}
			else
			{
				e.Cell.Row.Cells["ColorID"].Value = 1;
				e.Cell.Row.Cells["ColorID"].ValueList = null;
			}
			if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
			{
				e.Cell.Row.Cells["ItemSizeID"].Value = DBNull.Value;
				e.Cell.Row.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
			}
			else
			{
				e.Cell.Row.Cells["ItemSizeID"].Value = 1;
				e.Cell.Row.Cells["ItemSizeID"].ValueList = null;
			}
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
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Expected O, but got Unknown
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Expected O, but got Unknown
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Expected O, but got Unknown
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Expected O, but got Unknown
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Expected O, but got Unknown
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Expected O, but got Unknown
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Expected O, but got Unknown
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Expected O, but got Unknown
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Expected O, but got Unknown
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Expected O, but got Unknown
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Expected O, but got Unknown
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Expected O, but got Unknown
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Expected O, but got Unknown
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Expected O, but got Unknown
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Expected O, but got Unknown
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Expected O, but got Unknown
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Expected O, but got Unknown
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Expected O, but got Unknown
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Expected O, but got Unknown
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Expected O, but got Unknown
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Expected O, but got Unknown
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Expected O, but got Unknown
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Expected O, but got Unknown
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Expected O, but got Unknown
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Expected O, but got Unknown
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Expected O, but got Unknown
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Expected O, but got Unknown
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Expected O, but got Unknown
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Expected O, but got Unknown
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Expected O, but got Unknown
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Expected O, but got Unknown
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Expected O, but got Unknown
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Expected O, but got Unknown
		//IL_279f: Unknown result type (might be due to invalid IL or missing references)
		//IL_27a9: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Clinics.MasterData.frmBioAnalysisTree));
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
		UltraTab val65 = new UltraTab();
		UltraTab val66 = new UltraTab();
		UltraTab val67 = new UltraTab();
		UltraTab val68 = new UltraTab();
		UltraTab val69 = new UltraTab();
		Appearance val70 = new Appearance();
		Appearance val71 = new Appearance();
		Appearance val72 = new Appearance();
		this.tabItem = new UltraTabPageControl();
		this.btnSubAccountSearch = new UltraButton();
		this.cboSubAccount = new UltraComboEditor();
		this.lblSubAccount = new UltraLabel();
		this.btnAccountSearch = new UltraButton();
		this.cboAccount = new UltraComboEditor();
		this.lblAccountName = new UltraLabel();
		this.cboUnitGroup = new UltraComboEditor();
		this.cboUnit = new UltraComboEditor();
		this.lblUnitGroup = new UltraLabel();
		this.lblUnit = new UltraLabel();
		this.txtBriefCode = new UltraTextEditor();
		this.lblBriefCode = new UltraLabel();
		this.chkIsIndoor = new UltraCheckEditor();
		this.chkHasAttachement = new UltraCheckEditor();
		this.txtSamplesPeriod = new UltraTextEditor();
		this.txtMinDeliveryDays = new UltraTextEditor();
		this.txtSamplesCount = new UltraTextEditor();
		this.ultraLabel1 = new UltraLabel();
		this.lblMinDeliveryDays = new UltraLabel();
		this.lblSamplesPeriod = new UltraLabel();
		this.lblSamplesCount = new UltraLabel();
		this.cboDefaultLab = new UltraComboEditor();
		this.lblDefaultLab = new UltraLabel();
		this.cboTypes = new UltraComboEditor();
		this.lblTypes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblReceivingBank = new UltraLabel();
		this.ultraTabPageControl1 = new UltraTabPageControl();
		this.ULGPrices = new UltraGrid();
		this.ultraPanel1 = new UltraPanel();
		this.rbPriceForEachBranches = new System.Windows.Forms.RadioButton();
		this.rbPriceForAllBranches = new System.Windows.Forms.RadioButton();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGRanges = new UltraGrid();
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.ULGRecipeItems = new UltraGrid();
		this.ultraTabPageControl4 = new UltraTabPageControl();
		this.ULGMaterials = new UltraGrid();
		this.txtDescription = new UltraTextEditor();
		this.lblDescription = new UltraLabel();
		this.chkIsActive = new UltraCheckEditor();
		this.tabItemType = new UltraTabControl();
		this.ultraTabSharedControlsPage1 = new UltraTabSharedControlsPage();
		this.txtInternationalCode = new UltraTextEditor();
		this.lblInternationalCode = new UltraLabel();
		this.ofdItemPic = new System.Windows.Forms.OpenFileDialog();
		this.modifyGroupItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.modifyGroupItemsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.modifyGroupItemsToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
		this.modifyGroupItemsToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.modifyGroupItemsToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
		this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.changeParentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.chkIsSalesItem = new UltraCheckEditor();
		this.chkIsRecipe = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.dtChart).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.treeChart).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtName).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabItem).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnitGroup).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBriefCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsIndoor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkHasAttachement).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSamplesPeriod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMinDeliveryDays).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSamplesCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultLab).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTypes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGPrices).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraPanel1.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.ultraPanel1).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGRanges).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGRecipeItems).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGMaterials).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDescription).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tabItemType).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabItemType).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtInternationalCode).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.chkIsSalesItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsRecipe).BeginInit();
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
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.btnSubAccountSearch);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboSubAccount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblSubAccount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.btnAccountSearch);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboAccount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblAccountName);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboUnitGroup);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboUnit);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblUnitGroup);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblUnit);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtBriefCode);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblBriefCode);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.chkIsIndoor);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.chkHasAttachement);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtSamplesPeriod);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtMinDeliveryDays);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtSamplesCount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblMinDeliveryDays);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblSamplesPeriod);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblSamplesCount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboDefaultLab);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblDefaultLab);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboTypes);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblTypes);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblReceivingBank);
		((System.Windows.Forms.Control)(object)this.tabItem).Name = "tabItem";
		resources.ApplyResources(this.btnSubAccountSearch, "btnSubAccountSearch");
		((AppearanceBase)val5).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.btnSubAccountSearch).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).Name = "btnSubAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).Click += new System.EventHandler(btnSubAccountSearch_Click);
		resources.ApplyResources(this.cboSubAccount, "cboSubAccount");
		this.cboSubAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSubAccount).Name = "cboSubAccount";
		resources.ApplyResources(this.lblSubAccount, "lblSubAccount");
		this.lblSubAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSubAccount).Name = "lblSubAccount";
		((ControlBase)this.lblSubAccount).WrapText = false;
		resources.ApplyResources(this.btnAccountSearch, "btnAccountSearch");
		((AppearanceBase)val6).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.btnAccountSearch).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.btnAccountSearch).Name = "btnAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnAccountSearch).Click += new System.EventHandler(btnAccountSearch_Click);
		resources.ApplyResources(this.cboAccount, "cboAccount");
		this.cboAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboAccount).Name = "cboAccount";
		((TextEditorControlBase)this.cboAccount).ValueChanged += new System.EventHandler(cboAccount_ValueChanged);
		resources.ApplyResources(this.lblAccountName, "lblAccountName");
		this.lblAccountName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAccountName).Name = "lblAccountName";
		((ControlBase)this.lblAccountName).WrapText = false;
		resources.ApplyResources(this.cboUnitGroup, "cboUnitGroup");
		((System.Windows.Forms.Control)(object)this.cboUnitGroup).Name = "cboUnitGroup";
		((TextEditorControlBase)this.cboUnitGroup).ValueChanged += new System.EventHandler(cboUnitGroup_ValueChanged);
		resources.ApplyResources(this.cboUnit, "cboUnit");
		((System.Windows.Forms.Control)(object)this.cboUnit).Name = "cboUnit";
		resources.ApplyResources(this.lblUnitGroup, "lblUnitGroup");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.lblUnitGroup).Appearance = (AppearanceBase)(object)val7;
		this.lblUnitGroup.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUnitGroup).Name = "lblUnitGroup";
		((ControlBase)this.lblUnitGroup).WrapText = false;
		resources.ApplyResources(this.lblUnit, "lblUnit");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.lblUnit).Appearance = (AppearanceBase)(object)val8;
		this.lblUnit.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUnit).Name = "lblUnit";
		((ControlBase)this.lblUnit).WrapText = false;
		resources.ApplyResources(this.txtBriefCode, "txtBriefCode");
		((System.Windows.Forms.Control)(object)this.txtBriefCode).Name = "txtBriefCode";
		resources.ApplyResources(this.lblBriefCode, "lblBriefCode");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.lblBriefCode).Appearance = (AppearanceBase)(object)val9;
		this.lblBriefCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBriefCode).Name = "lblBriefCode";
		((ControlBase)this.lblBriefCode).WrapText = false;
		resources.ApplyResources(this.chkIsIndoor, "chkIsIndoor");
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance10");
		((UltraToggleEditorBase)this.chkIsIndoor).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.chkIsIndoor).Name = "chkIsIndoor";
		resources.ApplyResources(this.chkHasAttachement, "chkHasAttachement");
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance11");
		((UltraToggleEditorBase)this.chkHasAttachement).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.chkHasAttachement).Name = "chkHasAttachement";
		resources.ApplyResources(this.txtSamplesPeriod, "txtSamplesPeriod");
		((System.Windows.Forms.Control)(object)this.txtSamplesPeriod).Name = "txtSamplesPeriod";
		((System.Windows.Forms.Control)(object)this.txtSamplesPeriod).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInt_KeyPress);
		resources.ApplyResources(this.txtMinDeliveryDays, "txtMinDeliveryDays");
		((System.Windows.Forms.Control)(object)this.txtMinDeliveryDays).Name = "txtMinDeliveryDays";
		((System.Windows.Forms.Control)(object)this.txtMinDeliveryDays).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInt_KeyPress);
		resources.ApplyResources(this.txtSamplesCount, "txtSamplesCount");
		((System.Windows.Forms.Control)(object)this.txtSamplesCount).Name = "txtSamplesCount";
		((System.Windows.Forms.Control)(object)this.txtSamplesCount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInt_KeyPress);
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val12;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.lblMinDeliveryDays, "lblMinDeliveryDays");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance13");
		((ControlBase)this.lblMinDeliveryDays).Appearance = (AppearanceBase)(object)val13;
		this.lblMinDeliveryDays.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMinDeliveryDays).Name = "lblMinDeliveryDays";
		((ControlBase)this.lblMinDeliveryDays).WrapText = false;
		resources.ApplyResources(this.lblSamplesPeriod, "lblSamplesPeriod");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val14, "appearance14");
		((ControlBase)this.lblSamplesPeriod).Appearance = (AppearanceBase)(object)val14;
		this.lblSamplesPeriod.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSamplesPeriod).Name = "lblSamplesPeriod";
		((ControlBase)this.lblSamplesPeriod).WrapText = false;
		resources.ApplyResources(this.lblSamplesCount, "lblSamplesCount");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val15, "appearance15");
		((ControlBase)this.lblSamplesCount).Appearance = (AppearanceBase)(object)val15;
		this.lblSamplesCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSamplesCount).Name = "lblSamplesCount";
		((ControlBase)this.lblSamplesCount).WrapText = false;
		resources.ApplyResources(this.cboDefaultLab, "cboDefaultLab");
		this.cboDefaultLab.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboDefaultLab).Name = "cboDefaultLab";
		resources.ApplyResources(this.lblDefaultLab, "lblDefaultLab");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val16, "appearance16");
		((ControlBase)this.lblDefaultLab).Appearance = (AppearanceBase)(object)val16;
		this.lblDefaultLab.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDefaultLab).Name = "lblDefaultLab";
		((ControlBase)this.lblDefaultLab).WrapText = false;
		resources.ApplyResources(this.cboTypes, "cboTypes");
		this.cboTypes.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboTypes).Name = "cboTypes";
		resources.ApplyResources(this.lblTypes, "lblTypes");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val17, "appearance17");
		((ControlBase)this.lblTypes).Appearance = (AppearanceBase)(object)val17;
		this.lblTypes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTypes).Name = "lblTypes";
		((ControlBase)this.lblTypes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblReceivingBank, "lblReceivingBank");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val18).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val18, "appearance18");
		((ControlBase)this.lblReceivingBank).Appearance = (AppearanceBase)(object)val18;
		this.lblReceivingBank.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReceivingBank).Name = "lblReceivingBank";
		((ControlBase)this.lblReceivingBank).WrapText = false;
		resources.ApplyResources(this.ultraTabPageControl1, "ultraTabPageControl1");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.ULGPrices);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.ultraPanel1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Name = "ultraTabPageControl1";
		resources.ApplyResources(this.ULGPrices, "ULGPrices");
		((AppearanceBase)val19).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val19).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val19).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val19).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val19, "appearance19");
		((SpecialBoxBase)((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val19;
		((AppearanceBase)val20).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val20, "appearance20");
		((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val20;
		((SpecialBoxBase)((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val21).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val21).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val21).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val21).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val21, "appearance21");
		((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val21;
		((UltraGridBase)this.ULGPrices).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGPrices).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val22).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val22).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val22, "appearance22");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val22;
		((AppearanceBase)val23).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val23).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val23, "appearance23");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val23;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val24).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val24, "appearance24");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val24;
		((AppearanceBase)val25).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val25, "appearance25");
		((AppearanceBase)val25).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val25;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val26).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val26).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val26).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val26).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val26).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val26, "appearance26");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val26;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val27).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val27).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val27, "appearance27");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val27;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val28).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val28, "appearance28");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val28;
		((System.Windows.Forms.Control)(object)this.ULGPrices).Name = "ULGPrices";
		this.ULGPrices.AfterEnterEditMode += new System.EventHandler(ULGPrices_AfterEnterEditMode);
		((System.Windows.Forms.Control)(object)this.ULGPrices).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGPrices_KeyPress);
		resources.ApplyResources(this.ultraPanel1, "ultraPanel1");
		((AppearanceBase)val29).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val29, "appearance29");
		this.ultraPanel1.Appearance = (AppearanceBase)(object)val29;
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
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGRanges);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.ULGRanges, "ULGRanges");
		((AppearanceBase)val30).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val30).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val30).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val30).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val30, "appearance30");
		((SpecialBoxBase)((UltraGridBase)this.ULGRanges).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val30;
		((AppearanceBase)val31).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val31, "appearance31");
		((UltraGridBase)this.ULGRanges).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val31;
		((SpecialBoxBase)((UltraGridBase)this.ULGRanges).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val32).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val32).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val32).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val32).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val32, "appearance32");
		((UltraGridBase)this.ULGRanges).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val32;
		((UltraGridBase)this.ULGRanges).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGRanges).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val33).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val33).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val33, "appearance33");
		((UltraGridBase)this.ULGRanges).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val33;
		((AppearanceBase)val34).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val34).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val34, "appearance34");
		((UltraGridBase)this.ULGRanges).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val34;
		((UltraGridBase)this.ULGRanges).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGRanges).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val35).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val35, "appearance35");
		((UltraGridBase)this.ULGRanges).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val35;
		((AppearanceBase)val36).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val36, "appearance36");
		((AppearanceBase)val36).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGRanges).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val36;
		((UltraGridBase)this.ULGRanges).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGRanges).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val37).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val37).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val37).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val37).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val37).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val37, "appearance37");
		((UltraGridBase)this.ULGRanges).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val37;
		((UltraGridBase)this.ULGRanges).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGRanges).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val38).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val38).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val38, "appearance38");
		((UltraGridBase)this.ULGRanges).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val38;
		((UltraGridBase)this.ULGRanges).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val39).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val39, "appearance39");
		((UltraGridBase)this.ULGRanges).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val39;
		((System.Windows.Forms.Control)(object)this.ULGRanges).Name = "ULGRanges";
		((System.Windows.Forms.Control)(object)this.ULGRanges).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGRanges_KeyPress);
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ULGRecipeItems);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		resources.ApplyResources(this.ULGRecipeItems, "ULGRecipeItems");
		((AppearanceBase)val40).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val40).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val40).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val40).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val40, "appearance40");
		((SpecialBoxBase)((UltraGridBase)this.ULGRecipeItems).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val40;
		((AppearanceBase)val41).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val41, "appearance41");
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val41;
		((SpecialBoxBase)((UltraGridBase)this.ULGRecipeItems).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val42).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val42).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val42).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val42).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val42, "appearance42");
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val42;
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val43).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val43).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val43, "appearance43");
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val43;
		((AppearanceBase)val44).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val44).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val44, "appearance44");
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val44;
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val45).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val45, "appearance45");
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val45;
		((AppearanceBase)val46).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val46, "appearance46");
		((AppearanceBase)val46).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val46;
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val47).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val47).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val47).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val47).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val47).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val47, "appearance47");
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val47;
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val48).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val48).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val48, "appearance48");
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val48;
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val49).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val49, "appearance49");
		((UltraGridBase)this.ULGRecipeItems).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val49;
		((System.Windows.Forms.Control)(object)this.ULGRecipeItems).Name = "ULGRecipeItems";
		resources.ApplyResources(this.ultraTabPageControl4, "ultraTabPageControl4");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ULGMaterials);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Name = "ultraTabPageControl4";
		resources.ApplyResources(this.ULGMaterials, "ULGMaterials");
		((AppearanceBase)val50).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val50).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val50).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val50).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val50, "appearance50");
		((SpecialBoxBase)((UltraGridBase)this.ULGMaterials).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val50;
		((AppearanceBase)val51).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val51, "appearance51");
		((UltraGridBase)this.ULGMaterials).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val51;
		((SpecialBoxBase)((UltraGridBase)this.ULGMaterials).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val52).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val52).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val52).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val52).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val52, "appearance52");
		((UltraGridBase)this.ULGMaterials).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val52;
		((UltraGridBase)this.ULGMaterials).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGMaterials).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val53).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val53).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val53, "appearance53");
		((UltraGridBase)this.ULGMaterials).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val53;
		((AppearanceBase)val54).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val54).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val54, "appearance54");
		((UltraGridBase)this.ULGMaterials).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val54;
		((UltraGridBase)this.ULGMaterials).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGMaterials).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val55).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val55, "appearance55");
		((UltraGridBase)this.ULGMaterials).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val55;
		((AppearanceBase)val56).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val56, "appearance56");
		((AppearanceBase)val56).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGMaterials).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val56;
		((UltraGridBase)this.ULGMaterials).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGMaterials).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val57).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val57).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val57).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val57).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val57).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val57, "appearance57");
		((UltraGridBase)this.ULGMaterials).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val57;
		((UltraGridBase)this.ULGMaterials).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGMaterials).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val58).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val58).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val58, "appearance58");
		((UltraGridBase)this.ULGMaterials).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val58;
		((UltraGridBase)this.ULGMaterials).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val59).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val59, "appearance59");
		((UltraGridBase)this.ULGMaterials).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val59;
		((System.Windows.Forms.Control)(object)this.ULGMaterials).Name = "ULGMaterials";
		this.ULGMaterials.CellListSelect += new CellEventHandler(ULGMaterials_CellListSelect);
		((System.Windows.Forms.Control)(object)this.ULGMaterials).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGMaterials_KeyPress);
		resources.ApplyResources(this.txtDescription, "txtDescription");
		((System.Windows.Forms.Control)(object)this.txtDescription).Name = "txtDescription";
		resources.ApplyResources(this.lblDescription, "lblDescription");
		((AppearanceBase)val60).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val60).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val60, "appearance60");
		((ControlBase)this.lblDescription).Appearance = (AppearanceBase)(object)val60;
		this.lblDescription.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDescription).Name = "lblDescription";
		((ControlBase)this.lblDescription).WrapText = false;
		resources.ApplyResources(this.chkIsActive, "chkIsActive");
		((AppearanceBase)val61).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val61, "appearance61");
		((UltraToggleEditorBase)this.chkIsActive).Appearance = (AppearanceBase)(object)val61;
		((UltraToggleEditorBase)this.chkIsActive).Checked = true;
		((UltraToggleEditorBase)this.chkIsActive).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsActive).Name = "chkIsActive";
		resources.ApplyResources(this.tabItemType, "tabItemType");
		((AppearanceBase)val62).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val62, "appearance62");
		((AppearanceBase)val62).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.tabItemType).Appearance = (AppearanceBase)(object)val62;
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.tabItem);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl1);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl4);
		((System.Windows.Forms.Control)(object)this.tabItemType).Name = "tabItemType";
		((UltraTabControlBase)this.tabItemType).SharedControlsPage = this.ultraTabSharedControlsPage1;
		resources.ApplyResources(val63, "appearance63");
		((UltraTabControlBase)this.tabItemType).TabHeaderAreaAppearance = (AppearanceBase)(object)val63;
		resources.ApplyResources(val64, "appearance64");
		((UltraTabControlBase)this.tabItemType).TabListButtonAppearance = (AppearanceBase)(object)val64;
		((UltraTabControlBase)this.tabItemType).TabOrientation = (TabOrientation)1;
		((KeyedSubObjectBase)val65).Key = "Item";
		val65.TabPage = this.tabItem;
		resources.ApplyResources(val65, "ultraTab2");
		((SubObjectBase)val65).ForceApplyResources = "";
		((KeyedSubObjectBase)val66).Key = "Prices";
		val66.TabPage = this.ultraTabPageControl1;
		resources.ApplyResources(val66, "ultraTab4");
		((SubObjectBase)val66).ForceApplyResources = "";
		((KeyedSubObjectBase)val67).Key = "Ranges";
		val67.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val67, "ultraTab1");
		((SubObjectBase)val67).ForceApplyResources = "";
		((KeyedSubObjectBase)val68).Key = "Recipe";
		val68.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val68, "ultraTab3");
		val68.Visible = false;
		((SubObjectBase)val68).ForceApplyResources = "";
		((KeyedSubObjectBase)val69).Key = "Materials";
		val69.TabPage = this.ultraTabPageControl4;
		resources.ApplyResources(val69, "ultraTab5");
		((SubObjectBase)val69).ForceApplyResources = "";
		((UltraTabControlBase)this.tabItemType).Tabs.AddRange((UltraTab[])(object)new UltraTab[5] { val65, val66, val67, val68, val69 });
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		resources.ApplyResources(this.txtInternationalCode, "txtInternationalCode");
		((System.Windows.Forms.Control)(object)this.txtInternationalCode).Name = "txtInternationalCode";
		((System.Windows.Forms.Control)(object)this.txtInternationalCode).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtBarCode_KeyPress);
		resources.ApplyResources(this.lblInternationalCode, "lblInternationalCode");
		((AppearanceBase)val70).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val70).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val70, "appearance65");
		((ControlBase)this.lblInternationalCode).Appearance = (AppearanceBase)(object)val70;
		this.lblInternationalCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInternationalCode).Name = "lblInternationalCode";
		((ControlBase)this.lblInternationalCode).WrapText = false;
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
		resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.modifyGroupItemsToolStripMenuItem4, this.deleteToolStripMenuItem, this.changeParentToolStripMenuItem });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.ShowImageMargin = false;
		resources.ApplyResources(this.modifyGroupItemsToolStripMenuItem4, "modifyGroupItemsToolStripMenuItem4");
		this.modifyGroupItemsToolStripMenuItem4.Name = "modifyGroupItemsToolStripMenuItem4";
		this.modifyGroupItemsToolStripMenuItem4.Click += new System.EventHandler(modifyGroupItemsToolStripMenuItem4_Click);
		resources.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
		this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
		this.deleteToolStripMenuItem.Click += new System.EventHandler(deleteToolStripMenuItem_Click);
		resources.ApplyResources(this.changeParentToolStripMenuItem, "changeParentToolStripMenuItem");
		this.changeParentToolStripMenuItem.Name = "changeParentToolStripMenuItem";
		this.changeParentToolStripMenuItem.Click += new System.EventHandler(changeParentToolStripMenuItem_Click);
		resources.ApplyResources(this.chkIsSalesItem, "chkIsSalesItem");
		((AppearanceBase)val71).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val71, "appearance66");
		((UltraToggleEditorBase)this.chkIsSalesItem).Appearance = (AppearanceBase)(object)val71;
		((System.Windows.Forms.Control)(object)this.chkIsSalesItem).Name = "chkIsSalesItem";
		resources.ApplyResources(this.chkIsRecipe, "chkIsRecipe");
		((AppearanceBase)val72).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val72, "appearance67");
		((UltraToggleEditorBase)this.chkIsRecipe).Appearance = (AppearanceBase)(object)val72;
		((System.Windows.Forms.Control)(object)this.chkIsRecipe).Name = "chkIsRecipe";
		((UltraToggleEditorBase)this.chkIsRecipe).CheckedChanged += new System.EventHandler(chkIsRecipe_CheckedChanged);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.tabItemType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsActive);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInternationalCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDescription);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtInternationalCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDescription);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsSalesItem);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsRecipe);
		base.Name = "frmBioAnalysisTree";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsRecipe, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsSalesItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDescription, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtInternationalCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDescription, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInternationalCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsActive, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.tabItemType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
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
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnitGroup).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBriefCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsIndoor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkHasAttachement).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSamplesPeriod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMinDeliveryDays).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSamplesCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultLab).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTypes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGPrices).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraPanel1.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraPanel1.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.ultraPanel1).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGRanges).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGRecipeItems).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGMaterials).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDescription).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tabItemType).EndInit();
		((System.Windows.Forms.Control)(object)this.tabItemType).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtInternationalCode).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.chkIsSalesItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsRecipe).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
