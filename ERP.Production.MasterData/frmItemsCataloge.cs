using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.General;
using BusinessLayer.Production;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.Production.MasterData;

public class frmItemsCataloge : frmHeaderManyDetails
{
	private DataTable dtItems;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtStores;

	private DataTable dtUnits;

	private DataTable dtStages;

	private DataTable dtExpenses;

	private DataTable dtItemsCatalogeStagesInputs;

	private DataTable dtItemsCatalogeStagesOutputs;

	private DataTable dtItemsCatalogeStagesOutputsExpenses;

	private ValueList vlItemsInput = new ValueList();

	private ValueList vlItemsOutput = new ValueList();

	private ValueList vlUnitInput = new ValueList();

	private ValueList vlUnitOutput = new ValueList();

	private ValueList vlStoreInput = new ValueList();

	private ValueList vlStoreOutput = new ValueList();

	private ValueList vlStages = new ValueList();

	private ValueList vlExpenses = new ValueList();

	private ValueList vlColorsInput = new ValueList();

	private ValueList vlSizesInput = new ValueList();

	private ValueList vlColorsOutput = new ValueList();

	private ValueList vlSizesOutPut = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private DataSet ds;

	private int newID = -100000;

	private IContainer components = null;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblArabicName;

	private UltraTextEditor txtArabicName;

	private UltraLabel lblEnglishName;

	private UltraTextEditor txtEnglishName;

	private UltraLabel lblSourceStore;

	private UltraComboEditor cboSourceStore;

	private UltraLabel lblDestinationStore;

	private UltraComboEditor cboDestinationStore;

	private UltraCheckEditor chkActive;

	public frmItemsCataloge()
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
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		InitializeComponent();
		TableName = "Pro_ItemsCataloge";
		IDCol = "ItemCatalogeID";
		NoCol = "ItemCatalogeCode";
		DateCol = "GetDate()";
	}

	public frmItemsCataloge(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		if (UsingColors)
		{
			dtItemsColorCategorysDetails = ItemsColorCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlColorsInput.ValueListItems.Clear();
			vlColorsOutput.ValueListItems.Clear();
			for (int i = 0; i < dtColors.Rows.Count; i++)
			{
				vlColorsInput.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
				vlColorsOutput.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtItemsSizeCategorysDetails = ItemsSizeCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSizesInput.ValueListItems.Clear();
			vlSizesOutPut.ValueListItems.Clear();
			for (int j = 0; j < dtSizes.Rows.Count; j++)
			{
				vlSizesInput.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
				vlSizesOutPut.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
			}
		}
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "1", "0", "1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlItemsInput.ValueListItems.Clear();
		vlItemsOutput.ValueListItems.Clear();
		for (int k = 0; k < dtItems.Rows.Count; k++)
		{
			vlItemsInput.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["Name"].ToString());
			vlItemsOutput.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["Name"].ToString());
		}
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSourceStore, dtStores, "StoreID", "StoreName");
		GlobalFunctions.FillCombo(cboDestinationStore, dtStores, "StoreID", "StoreName");
		vlStoreInput.ValueListItems.Clear();
		vlStoreOutput.ValueListItems.Clear();
		for (int l = 0; l < dtStores.Rows.Count; l++)
		{
			vlStoreInput.ValueListItems.Add(dtStores.Rows[l]["StoreID"], dtStores.Rows[l]["StoreName"].ToString());
			vlStoreOutput.ValueListItems.Add(dtStores.Rows[l]["StoreID"], dtStores.Rows[l]["StoreName"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlUnitInput.ValueListItems.Clear();
		vlUnitOutput.ValueListItems.Clear();
		for (int m = 0; m < dtUnits.Rows.Count; m++)
		{
			vlUnitInput.ValueListItems.Add(dtUnits.Rows[m]["UnitID"], dtUnits.Rows[m]["UnitName"].ToString());
			vlUnitOutput.ValueListItems.Add(dtUnits.Rows[m]["UnitID"], dtUnits.Rows[m]["UnitName"].ToString());
		}
		dtStages = Stages.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlStages.ValueListItems.Clear();
		for (int n = 0; n < dtStages.Rows.Count; n++)
		{
			vlStages.ValueListItems.Add(dtStages.Rows[n]["StageID"], dtStages.Rows[n]["StageName"].ToString());
		}
		dtExpenses = Expenses.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlExpenses.ValueListItems.Clear();
		for (int num = 0; num < dtExpenses.Rows.Count; num++)
		{
			vlExpenses.ValueListItems.Add(dtExpenses.Rows[num]["ExpenseID"], dtExpenses.Rows[num]["ExpenseName"].ToString());
		}
		dtDetails = ItemsCatalogeStages.SelectByItemCatalogeID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtItemsCatalogeStagesInputs = ItemsCatalogeStagesInputs.SelectByItemCatalogeID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtItemsCatalogeStagesOutputs = ItemsCatalogeStagesOutputs.SelectByItemCatalogeID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtItemsCatalogeStagesOutputsExpenses = ItemsCatalogeStagesOutputsExpenses.SelectByItemCatalogeID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		ds = new DataSet();
		ds.Tables.Add(dtDetails);
		ds.Tables.Add(dtItemsCatalogeStagesInputs);
		ds.Tables.Add(dtItemsCatalogeStagesOutputs);
		ds.Tables.Add(dtItemsCatalogeStagesOutputsExpenses);
		ds.Tables[0].TableName = "dtDetails";
		ds.Tables[1].TableName = "dtItemsCatalogeStagesInputs";
		ds.Tables[2].TableName = "dtItemsCatalogeStagesOutputs";
		ds.Tables[3].TableName = "dtItemsCatalogeStagesOutputsExpenses";
		ds.Relations.Add(ds.Tables[0].Columns["ItemCatalogeStageID"], ds.Tables[1].Columns["ItemCatalogeStageID"]);
		ds.Relations.Add(ds.Tables[0].Columns["ItemCatalogeStageID"], ds.Tables[2].Columns["ItemCatalogeStageID"]);
		ds.Relations.Add(ds.Tables[2].Columns["ItemCatalogeStageOutputID"], ds.Tables[3].Columns["ItemCatalogeStageOutputID"]);
		((UltraGridBase)ULGData).DataSource = ds;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].HeaderVisible = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Header).Caption = "الاصناف الداخله في المرحله";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Header).Appearance.BackColor = Color.LightSkyBlue;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Header).Appearance.BackColor2 = Color.LightYellow;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Header).Appearance.BackGradientStyle = (GradientStyle)3;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Header).Appearance.ForeColor = Color.Blue;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].HeaderVisible = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Header).Caption = "الاصناف الناتجه من المرحله";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Header).Appearance.BackColor = Color.AliceBlue;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Header).Appearance.BackColor2 = Color.CornflowerBlue;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Header).Appearance.BackGradientStyle = (GradientStyle)3;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Header).Appearance.ForeColor = Color.Blue;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StageID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitFees"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StageID"].Header).Caption = (GlobalVariables.IsArabic ? "المرحلة" : "Stage");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitFees"].Header).Caption = (GlobalVariables.IsArabic ? "أجر الوحدة" : "Unit Fees");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StageID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitFees"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StageID"].ValueList = (IValueList)(object)vlStages;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitFees"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ColorID"].Hidden = !UsingColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemSizeID"].Hidden = !UsingSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ColorID"].ValueList = (IValueList)(object)vlColorsInput;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlSizesInput;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemSizeID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "مخزن" : "Store");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].ValueList = (IValueList)(object)vlItemsInput;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].ValueList = (IValueList)(object)vlUnitInput;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["StoreID"].ValueList = (IValueList)(object)vlStoreInput;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ColorID"].Hidden = !UsingColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemSizeID"].Hidden = !UsingSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ColorID"].ValueList = (IValueList)(object)vlColorsOutput;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlSizesOutPut;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemSizeID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["CostAllocationRate"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة البيعية" : "Sales Value");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "مخزن" : "Store");
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["CostAllocationRate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemID"].ValueList = (IValueList)(object)vlItemsOutput;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["UnitID"].ValueList = (IValueList)(object)vlUnitOutput;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["StoreID"].ValueList = (IValueList)(object)vlStoreOutput;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["Qty"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["CostAllocationRate"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[3].Columns["ExpenseID"].Header).Caption = (GlobalVariables.IsArabic ? "المصروف" : "Expense");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[3].Columns["Value"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة" : "Value");
		((UltraGridBase)ULGData).DisplayLayout.Bands[3].Columns["ExpenseID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[3].Columns["Value"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[3].Columns["ExpenseID"].ValueList = (IValueList)(object)vlExpenses;
		((UltraGridBase)ULGData).DisplayLayout.Bands[3].Columns["Value"].DefaultCellValue = 0;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = ItemsCataloge.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
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
		base.DisplayData();
		if (drMaster != null)
		{
			((Control)(object)txtCode).Text = drMaster["ItemCatalogeCode"].ToString();
			((Control)(object)txtArabicName).Text = drMaster["ItemCatalogeNameAr"].ToString();
			((Control)(object)txtEnglishName).Text = drMaster["ItemCatalogeNameEn"].ToString();
			((TextEditorControlBase)cboSourceStore).ValueChanged -= cboSourceStore_ValueChanged;
			((TextEditorControlBase)cboDestinationStore).ValueChanged -= cboDestinationStore_ValueChanged;
			((TextEditorControlBase)cboSourceStore).Value = drMaster["InputStoreID"];
			((TextEditorControlBase)cboDestinationStore).Value = drMaster["OutputStoreID"];
			((TextEditorControlBase)cboSourceStore).ValueChanged += cboSourceStore_ValueChanged;
			((TextEditorControlBase)cboDestinationStore).ValueChanged += cboDestinationStore_ValueChanged;
			((UltraToggleEditorBase)chkActive).Checked = bool.Parse(drMaster["IsActive"].ToString());
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			dtDetails = ItemsCatalogeStages.SelectByItemCatalogeID(drMaster["ItemCatalogeID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtItemsCatalogeStagesInputs = ItemsCatalogeStagesInputs.SelectByItemCatalogeID(drMaster["ItemCatalogeID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtItemsCatalogeStagesOutputs = ItemsCatalogeStagesOutputs.SelectByItemCatalogeID(drMaster["ItemCatalogeID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtItemsCatalogeStagesOutputsExpenses = ItemsCatalogeStagesOutputsExpenses.SelectByItemCatalogeID(drMaster["ItemCatalogeID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			ds = new DataSet();
			ds.Tables.Add(dtDetails);
			ds.Tables.Add(dtItemsCatalogeStagesInputs);
			ds.Tables.Add(dtItemsCatalogeStagesOutputs);
			ds.Tables.Add(dtItemsCatalogeStagesOutputsExpenses);
			ds.Tables[0].TableName = "dtDetails";
			ds.Tables[1].TableName = "dtItemsCatalogeStagesInputs";
			ds.Tables[2].TableName = "dtItemsCatalogeStagesOutputs";
			ds.Tables[3].TableName = "dtItemsCatalogeStagesOutputsExpenses";
			ds.Relations.Add(ds.Tables[0].Columns["ItemCatalogeStageID"], ds.Tables[1].Columns["ItemCatalogeStageID"]);
			ds.Relations.Add(ds.Tables[0].Columns["ItemCatalogeStageID"], ds.Tables[2].Columns["ItemCatalogeStageID"]);
			ds.Relations.Add(ds.Tables[2].Columns["ItemCatalogeStageOutputID"], ds.Tables[3].Columns["ItemCatalogeStageOutputID"]);
			((UltraGridBase)ULGData).DataSource = ds;
			InitGrid();
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
		((EditorButtonControlBase)cboSourceStore).ReadOnly = NavMode;
		((EditorButtonControlBase)cboDestinationStore).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)chkActive).Enabled = !NavMode;
		if (!Updating)
		{
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value == DBNull.Value)
				{
					continue;
				}
				DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value.ToString())[0];
				int unitTypeID = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
				ValueList unitsValueList = getUnitsValueList(unitTypeID);
				((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
				if (((DisposableObjectCollectionBase)unitsValueList.ValueListItems).Count == 0)
				{
					((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitID"].Value = DBNull.Value;
				}
				if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ColorID"].ValueList.ItemCount == 0)
					{
						((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ColorID"].Value = DBNull.Value;
					}
				}
				if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemSizeID"].ValueList.ItemCount == 0)
					{
						((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemSizeID"].Value = DBNull.Value;
					}
				}
			}
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows).Count; k++)
			{
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["ItemID"].Value == DBNull.Value)
				{
					continue;
				}
				DataRow dataRow2 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["ItemID"].Value.ToString())[0];
				int unitTypeID2 = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
				ValueList unitsValueList2 = getUnitsValueList(unitTypeID2);
				((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList2;
				if (((DisposableObjectCollectionBase)unitsValueList2.ValueListItems).Count == 0)
				{
					((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["UnitID"].Value = DBNull.Value;
				}
				if (UsingColors && dataRow2["ItemColorCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow2["ItemColorCategoryID"].ToString()));
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["ColorID"].ValueList.ItemCount == 0)
					{
						((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["ColorID"].Value = DBNull.Value;
					}
				}
				if (UsingSizes && dataRow2["ItemSizeCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow2["ItemSizeCategoryID"].ToString()));
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["ItemSizeID"].ValueList.ItemCount == 0)
					{
						((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["ItemSizeID"].Value = DBNull.Value;
					}
				}
			}
		}
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((Control)(object)txtCode).Text = (Adding ? ItemsCataloge.GetCode(IsFromServer: true) : "");
		((TextEditorControlBase)txtArabicName).Clear();
		((TextEditorControlBase)txtEnglishName).Clear();
		((TextEditorControlBase)cboSourceStore).ValueChanged -= cboSourceStore_ValueChanged;
		((TextEditorControlBase)cboDestinationStore).ValueChanged -= cboDestinationStore_ValueChanged;
		cboDestinationStore.SelectedIndex = -1;
		cboSourceStore.SelectedIndex = -1;
		((TextEditorControlBase)cboSourceStore).ValueChanged += cboSourceStore_ValueChanged;
		((TextEditorControlBase)cboDestinationStore).ValueChanged += cboDestinationStore_ValueChanged;
		((TextEditorControlBase)txtNotes).Clear();
		((UltraToggleEditorBase)chkActive).Checked = false;
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[3].Rows.Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[2].Rows.Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[1].Rows.Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[0].Rows.Clear();
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال الاسم بالعربية" : "Please Enter The Arabic Name");
			((TextEditorControlBase)txtArabicName).Focus();
			return false;
		}
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم  الأذن" : "Please Enter The Voucher Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (Main.CheckForValue("Pro_ItemsCataloge", "ItemCatalogeCode", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["ItemCatalogeCode"].ToString(), IsFromServer: true) > 0)
		{
			string code = ItemsCataloge.GetCode(IsFromServer: true);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Voucher Number Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = code;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا الإذن", "Please insert details for this Voucher");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["StageID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار إسم المرحلة  ", "Please Select Stage Name ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["StageID"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count == 0)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل المدخلات لهذا الصنف", "Please insert Input details for this Item");
				return false;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows).Count == 0)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل المخرجات لهذا الصنف", "Please insert output details for this Item");
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء إختيار إسم الصنف  ", "Please Select Item Name ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Qty"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Qty"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitID"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء إختيار إسم الوحدة  ", "Please Select Unit Name");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitID"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; k++)
				{
					if (k != j && ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["ItemID"].Value.ToString())
					{
						GlobalVariables.InformationMB.Show(" لا يمكن تكرار الصنف فى نفس المرحلة ", "Cannot Duplicate The Same Item With the Same Stage ");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"];
						return false;
					}
				}
			}
			for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows).Count; l++)
			{
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["ItemID"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء إختيار إسم الصنف  ", "Please Select Item Name ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["ItemID"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["Qty"].Value.ToString()) <= 0m)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["Qty"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["UnitID"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء إختيار إسم الوحدة  ", "Please Select Unit Name");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["UnitID"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows).Count; m++)
				{
					if (m != l && ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["ItemID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[m].Cells["ItemID"].Value.ToString())
					{
						GlobalVariables.InformationMB.Show(" لا يمكن تكرار الصنف فى نفس المرحلة ", "Cannot Duplicate The Same Item With the Same Stage ");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[l].Cells["ItemID"];
						return false;
					}
				}
				for (int n = 0; n < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].ChildBands[0].Rows).Count; n++)
				{
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].ChildBands[0].Rows[n].Cells["ExpenseID"].Value == DBNull.Value)
					{
						((Control)(object)ULGData).Enter -= ULGData_Enter;
						GlobalVariables.InformationMB.Show("برجاء إختيار إسم المصروف  ", "Please Select Expense Name");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].ChildBands[0].Rows[n].Cells["ExpenseID"];
						ULGData.PerformAction((UltraGridAction)24);
						((Control)(object)ULGData).Enter += ULGData_Enter;
						return false;
					}
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].ChildBands[0].Rows[n].Cells["Value"].Value == DBNull.Value)
					{
						((Control)(object)ULGData).Enter -= ULGData_Enter;
						GlobalVariables.InformationMB.Show("برجاء إدخال القيمة  ", "Please Insert Value");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].ChildBands[0].Rows[n].Cells["Value"];
						ULGData.PerformAction((UltraGridAction)24);
						((Control)(object)ULGData).Enter += ULGData_Enter;
						return false;
					}
				}
			}
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = ItemsCataloge.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? ((Control)(object)txtArabicName).Text : ((Control)(object)txtEnglishName).Text, (cboSourceStore.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSourceStore).Value.ToString(), (cboDestinationStore.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDestinationStore).Value.ToString(), ((UltraToggleEditorBase)chkActive).Checked ? "1" : "0", ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				int num2 = ItemsCatalogeStages.Insert_Update("-1", num.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["StageID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["UnitFees"].Value == DBNull.Value || ((UltraGridBase)ULGData).Rows[i].Cells["UnitFees"].Value.ToString() == "") ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["UnitFees"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
				{
					ItemsCatalogeStagesInputs.Insert_Update("-1", num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemSizeID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Qty"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["StoreID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["StoreID"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				}
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows).Count; k++)
				{
					int num3 = ItemsCatalogeStagesOutputs.Insert_Update("-1", num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["ItemSizeID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["Qty"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["CostAllocationRate"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["UnitID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["StoreID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["StoreID"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
					for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].ChildBands[0].Rows).Count; l++)
					{
						ItemsCatalogeStagesOutputsExpenses.Insert_Update("-1", num3.ToString(), num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].ChildBands[0].Rows[l].Cells["ExpenseID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].ChildBands[0].Rows[l].Cells["Value"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
					}
				}
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
			int num = ItemsCataloge.Insert_Update(drMaster["ItemCatalogeID"].ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? ((Control)(object)txtArabicName).Text : ((Control)(object)txtEnglishName).Text, (cboSourceStore.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSourceStore).Value.ToString(), (cboDestinationStore.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDestinationStore).Value.ToString(), ((UltraToggleEditorBase)chkActive).Checked ? "1" : "0", ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			ItemsCatalogeStagesInputs.DeleteByItemCatalogeID(num.ToString(), GlobalVariables.UserID, IsFromServer: true);
			ItemsCatalogeStagesOutputsExpenses.DeleteByItemCatalogeID(num.ToString(), GlobalVariables.UserID, IsFromServer: true);
			ItemsCatalogeStagesOutputs.DeleteByItemCatalogeID(num.ToString(), GlobalVariables.UserID, IsFromServer: true);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["ItemCatalogeStageID"].Value.ToString() + ",";
			}
			Main.SyncDeleteForUpdate("Pro_ItemsCatalogeStages", "ItemCatalogeID", drMaster["ItemCatalogeID"].ToString(), "ItemCatalogeStageID", text, IsFromServer: true);
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				int num2 = ItemsCatalogeStages.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ItemCatalogeStageID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ItemCatalogeStageID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[j].Cells["ItemCatalogeStageID"].Value.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["StageID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["UnitFees"].Value == DBNull.Value || ((UltraGridBase)ULGData).Rows[j].Cells["UnitFees"].Value.ToString() == "") ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["UnitFees"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows).Count; k++)
				{
					ItemsCatalogeStagesInputs.Insert_Update("-1", num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ItemSizeID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["Qty"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["UnitID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["StoreID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["StoreID"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				}
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows).Count; l++)
				{
					int num3 = ItemsCatalogeStagesOutputs.Insert_Update("-1", num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].Cells["ItemSizeID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].Cells["Qty"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].Cells["CostAllocationRate"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].Cells["UnitID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].Cells["StoreID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].Cells["StoreID"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
					for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].ChildBands[0].Rows).Count; m++)
					{
						ItemsCatalogeStagesOutputsExpenses.Insert_Update("-1", num3.ToString(), num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].ChildBands[0].Rows[m].Cells["ExpenseID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].ChildBands[0].Rows[m].Cells["Value"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
					}
				}
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
			ItemsCatalogeStagesOutputsExpenses.DeleteByItemCatalogeID(drMaster["ItemCatalogeID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			ItemsCatalogeStagesOutputs.DeleteByItemCatalogeID(drMaster["ItemCatalogeID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			ItemsCatalogeStagesInputs.DeleteByItemCatalogeID(drMaster["ItemCatalogeID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			ItemsCatalogeStages.DeleteByItemCatalogeID(drMaster["ItemCatalogeID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			ItemsCataloge.Delete(drMaster["ItemCatalogeID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnPrintClick()
	{
		if (RowID != "")
		{
			string val = "";
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Pro_ItemsCataloge_A.rpt" : "Rep_Pro_ItemsCataloge_E.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@ItemCatalogeIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			GlobalVariables.ReportDocument.SetParameterValue("@ItemCatalogeIDs", "," + RowID + ",", "Rep_Pro_ItemsCatalogeInput");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0", "Rep_Pro_ItemsCatalogeInput");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ItemsCatalogeReport(IsFromServer: true);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["ItemCatalogeID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		if (UsingColors)
		{
			dtItemsColorCategorysDetails = ItemsColorCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlColorsInput.ValueListItems.Clear();
			vlColorsOutput.ValueListItems.Clear();
			for (int i = 0; i < dtColors.Rows.Count; i++)
			{
				vlColorsInput.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
				vlColorsOutput.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtItemsSizeCategorysDetails = ItemsSizeCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSizesInput.ValueListItems.Clear();
			vlSizesOutPut.ValueListItems.Clear();
			for (int j = 0; j < dtSizes.Rows.Count; j++)
			{
				vlSizesInput.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
				vlSizesOutPut.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
			}
		}
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "1", "0", "1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlItemsInput.ValueListItems.Clear();
		vlItemsOutput.ValueListItems.Clear();
		for (int k = 0; k < dtItems.Rows.Count; k++)
		{
			vlItemsInput.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["Name"].ToString());
			vlItemsOutput.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["Name"].ToString());
		}
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSourceStore, dtStores, "StoreID", "StoreName");
		GlobalFunctions.FillCombo(cboDestinationStore, dtStores, "StoreID", "StoreName");
		vlStoreInput.ValueListItems.Clear();
		vlStoreOutput.ValueListItems.Clear();
		for (int l = 0; l < dtStores.Rows.Count; l++)
		{
			vlStoreInput.ValueListItems.Add(dtStores.Rows[l]["StoreID"], dtStores.Rows[l]["StoreName"].ToString());
			vlStoreOutput.ValueListItems.Add(dtStores.Rows[l]["StoreID"], dtStores.Rows[l]["StoreName"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlUnitInput.ValueListItems.Clear();
		vlUnitOutput.ValueListItems.Clear();
		for (int m = 0; m < dtUnits.Rows.Count; m++)
		{
			vlUnitInput.ValueListItems.Add(dtUnits.Rows[m]["UnitID"], dtUnits.Rows[m]["UnitName"].ToString());
			vlUnitOutput.ValueListItems.Add(dtUnits.Rows[m]["UnitID"], dtUnits.Rows[m]["UnitName"].ToString());
		}
		dtStages = Stages.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlStages.ValueListItems.Clear();
		for (int n = 0; n < dtStages.Rows.Count; n++)
		{
			vlStages.ValueListItems.Add(dtStages.Rows[n]["StageID"], dtStages.Rows[n]["StageName"].ToString());
		}
		dtExpenses = Expenses.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlExpenses.ValueListItems.Clear();
		for (int num = 0; num < dtExpenses.Rows.Count; num++)
		{
			vlExpenses.ValueListItems.Add(dtExpenses.Rows[num]["ExpenseID"], dtExpenses.Rows[num]["ExpenseName"].ToString());
		}
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		((UltraGridBase)ULGData).UpdateData();
		if (((GridItemBase)e.Cell).Band.Index == 0 && ((KeyedSubObjectBase)e.Cell.Column).Key == "StageID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			if (e.Cell.Value != DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitFees"].Value = dtStages.Select(" StageID= " + e.Cell.Value.ToString())[0]["DefaultUnitFees"];
			}
		}
		else if ((((GridItemBase)e.Cell).Band.Index == 1 || ((GridItemBase)e.Cell).Band.Index == 2) && ((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0 && e.Cell.Value != DBNull.Value)
		{
			DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			int num = ((((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value != DBNull.Value) ? int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()) : 0);
			if (num != 0)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = DBNull.Value;
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(num);
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			}
			else
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = null;
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = DBNull.Value;
			}
			if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
			}
			else
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = 1;
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = null;
			}
			if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
			}
			else
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = 1;
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = null;
			}
		}
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
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

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F8)
		{
			if ((Adding || Updating) && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID")
			{
				int num = (Adding ? SearchFunctions.Items("-1", "-1", "0", "-1", "1", "0", "1", "-1", "-1", "-1", IsFromServer: false) : SearchFunctions.Items("-1", (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0) ? "1" : "-1", "0", "-1", "-1", "0", "1", "-1", "-1", "-1", IsFromServer: false));
				if (num != 0)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = num;
				}
			}
			e.Handled = true;
		}
		else if (e.KeyCode == Keys.F9)
		{
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemSizeID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ColorID") && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value)
			{
				DataTable dataTable = Items.Select(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
				frmImageViewer frmImageViewer2 = new frmImageViewer((dataTable.Rows[0]["ItemPic"] == DBNull.Value) ? null : ImageFunctions.BinaryToImage((byte[])dataTable.Rows[0]["ItemPic"]), GlobalVariables.IsArabic ? dataTable.Rows[0]["ItemNameAr"].ToString() : dataTable.Rows[0]["ItemNameEn"].ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value.ToString(), "-1", (((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value.ToString(), GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate));
				frmImageViewer2.WindowState = FormWindowState.Normal;
				frmImageViewer2.ShowDialog();
			}
			e.Handled = true;
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitFees" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CostAllocationRate" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Value"))
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		if (((GridItemBase)e.Row).Band.Index == 0)
		{
			e.Row.Cells["ItemCatalogeStageID"].Value = ++newID;
		}
		else if (((GridItemBase)e.Row).Band.Index == 1)
		{
			e.Row.Cells["ItemCatalogeStageInputID"].Value = ++newID;
			e.Row.Cells["StoreID"].Value = ((cboSourceStore.SelectedIndex == -1) ? DBNull.Value : ((TextEditorControlBase)cboSourceStore).Value);
		}
		else if (((GridItemBase)e.Row).Band.Index == 2)
		{
			e.Row.Cells["ItemCatalogeStageOutputID"].Value = ++newID;
			e.Row.Cells["StoreID"].Value = ((cboDestinationStore.SelectedIndex == -1) ? DBNull.Value : ((TextEditorControlBase)cboDestinationStore).Value);
		}
		else if (((GridItemBase)e.Row).Band.Index == 3)
		{
			e.Row.Cells["ItemCatalogeStageOutputExpenseID"].Value = ++newID;
		}
	}

	private void cboSourceStore_ValueChanged(object sender, EventArgs e)
	{
		if (cboSourceStore.SelectedIndex <= -1)
		{
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["StoreID"].Value = ((TextEditorControlBase)cboSourceStore).Value;
			}
		}
	}

	private void cboDestinationStore_ValueChanged(object sender, EventArgs e)
	{
		if (cboDestinationStore.SelectedIndex <= -1)
		{
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows).Count; j++)
			{
				((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[j].Cells["StoreID"].Value = ((TextEditorControlBase)cboSourceStore).Value;
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
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Expected O, but got Unknown
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Expected O, but got Unknown
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Production.MasterData.frmItemsCataloge));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblArabicName = new UltraLabel();
		this.txtArabicName = new UltraTextEditor();
		this.lblEnglishName = new UltraLabel();
		this.txtEnglishName = new UltraTextEditor();
		this.lblSourceStore = new UltraLabel();
		this.cboSourceStore = new UltraComboEditor();
		this.lblDestinationStore = new UltraLabel();
		this.cboDestinationStore = new UltraComboEditor();
		this.chkActive = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSourceStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDestinationStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkActive).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		resources.ApplyResources(base.ULGData, "ULGData");
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val, "appearance1");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val2, "appearance2");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val3, "appearance3");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val4, "appearance4");
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		resources.ApplyResources(base.btnSetting, "btnSetting");
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.btnSearch, "btnSearch");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.btnCopyTo, "btnCopyTo");
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
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblArabicName, "lblArabicName");
		this.lblArabicName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArabicName).Name = "lblArabicName";
		((ControlBase)this.lblArabicName).WrapText = false;
		resources.ApplyResources(this.txtArabicName, "txtArabicName");
		((System.Windows.Forms.Control)(object)this.txtArabicName).Name = "txtArabicName";
		resources.ApplyResources(this.lblEnglishName, "lblEnglishName");
		this.lblEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEnglishName).Name = "lblEnglishName";
		((ControlBase)this.lblEnglishName).WrapText = false;
		resources.ApplyResources(this.txtEnglishName, "txtEnglishName");
		resources.ApplyResources(val9, "appearance9");
		((TextEditorControlBase)this.txtEnglishName).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.txtEnglishName).Name = "txtEnglishName";
		resources.ApplyResources(this.lblSourceStore, "lblSourceStore");
		this.lblSourceStore.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSourceStore).Name = "lblSourceStore";
		((ControlBase)this.lblSourceStore).WrapText = false;
		resources.ApplyResources(this.cboSourceStore, "cboSourceStore");
		((TextEditorControlBase)this.cboSourceStore).AlwaysInEditMode = true;
		this.cboSourceStore.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSourceStore).Name = "cboSourceStore";
		((TextEditorControlBase)this.cboSourceStore).ValueChanged += new System.EventHandler(cboSourceStore_ValueChanged);
		resources.ApplyResources(this.lblDestinationStore, "lblDestinationStore");
		this.lblDestinationStore.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDestinationStore).Name = "lblDestinationStore";
		((ControlBase)this.lblDestinationStore).WrapText = false;
		resources.ApplyResources(this.cboDestinationStore, "cboDestinationStore");
		((TextEditorControlBase)this.cboDestinationStore).AlwaysInEditMode = true;
		this.cboDestinationStore.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboDestinationStore).Name = "cboDestinationStore";
		((TextEditorControlBase)this.cboDestinationStore).ValueChanged += new System.EventHandler(cboDestinationStore_ValueChanged);
		resources.ApplyResources(this.chkActive, "chkActive");
		((System.Windows.Forms.Control)(object)this.chkActive).Name = "chkActive";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkActive);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSourceStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSourceStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDestinationStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboDestinationStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Name = "frmItemsCataloge";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UTCDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboDestinationStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDestinationStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSourceStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSourceStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkActive, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSourceStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDestinationStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkActive).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
