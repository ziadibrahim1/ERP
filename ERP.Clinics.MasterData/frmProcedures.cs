using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Clinics;
using BusinessLayer.General;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;

namespace ERP.Clinics.MasterData;

public class frmProcedures : frmHeaderManyDetails
{
	private DataTable dtProcedurePrices;

	private DataTable dtProcedureStepItems;

	private DataTable dtSteps;

	private DataTable dtSpecializations;

	private DataTable dtUnits;

	private DataTable dtPriceTypes;

	private DataTable dtItems;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtBatchs;

	private DataTable dtStores;

	private ValueList vlPriceTypes = new ValueList();

	private ValueList vlItems = new ValueList();

	private ValueList vlBarCode = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlBatchs = new ValueList();

	private ValueList vlStores = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private ValueList vlSteps = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private bool UsingBatchNoAndValidityPeriod = false;

	private bool AutomaticlyAddItemTaxToSalesInvoice = false;

	private DataSet ds;

	private int newID = -100000;

	private IContainer components = null;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblSpecializations;

	private UltraComboEditor cboSpecializations;

	private UltraTabPageControl ultraTabPageControl3;

	protected internal UltraGrid ULGDataPrices;

	private UltraTextEditor txtNameEn;

	private UltraLabel lblVesselNameEn;

	private UltraTextEditor txtNameAr;

	private UltraLabel lblVesselNameAr;

	private UltraCheckEditor chkCanModPrice;

	public frmProcedures()
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
		InitializeComponent();
		TableName = "CL_Procedures";
		IDCol = "ProcedureID";
		NoCol = "ProcedureCode";
		DateCol = "GetDate()";
	}

	public frmProcedures(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtSpecializations = Specializations.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSpecializations, dtSpecializations, "SpecializationID", "SpecializationName");
		dtSteps = Steps.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlSteps.ValueListItems.Clear();
		for (int i = 0; i < dtSteps.Rows.Count; i++)
		{
			vlSteps.ValueListItems.Add(dtSteps.Rows[i]["StepID"], dtSteps.Rows[i]["StepName"].ToString());
		}
		dtPriceTypes = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlPriceTypes.ValueListItems.Clear();
		for (int j = 0; j < dtPriceTypes.Rows.Count; j++)
		{
			vlPriceTypes.ValueListItems.Add(dtPriceTypes.Rows[j]["PriceTypeID"], dtPriceTypes.Rows[j]["PriceName"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int k = 0; k < dtUnits.Rows.Count; k++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[k]["UnitID"], dtUnits.Rows[k]["UnitName"].ToString());
		}
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		if (UsingColors)
		{
			dtItemsColorCategorysDetails = ItemsColorCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlColors.ValueListItems.Clear();
			for (int l = 0; l < dtColors.Rows.Count; l++)
			{
				vlColors.ValueListItems.Add(dtColors.Rows[l]["ColorID"], dtColors.Rows[l]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtItemsSizeCategorysDetails = ItemsSizeCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSizes.ValueListItems.Clear();
			for (int m = 0; m < dtSizes.Rows.Count; m++)
			{
				vlSizes.ValueListItems.Add(dtSizes.Rows[m]["ItemSizeID"], dtSizes.Rows[m]["ItemSizeName"].ToString());
			}
		}
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		AutomaticlyAddItemTaxToSalesInvoice = GlobalFunctions.GetOption("AutomaticlyAddItemTaxToSalesInvoice");
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchs.ValueListItems.Clear();
			for (int n = 0; n < dtBatchs.Rows.Count; n++)
			{
				vlBatchs.ValueListItems.Add(dtBatchs.Rows[n]["BatchID"], dtBatchs.Rows[n]["BatchName"].ToString());
			}
		}
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlStores.ValueListItems.Clear();
		for (int num = 0; num < dtStores.Rows.Count; num++)
		{
			vlStores.ValueListItems.Add(dtStores.Rows[num]["StoreID"], dtStores.Rows[num]["StoreName"].ToString());
		}
		dtItems = Items.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		vlBarCode.ValueListItems.Clear();
		for (int num2 = 0; num2 < dtItems.Rows.Count; num2++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[num2]["ItemID"], dtItems.Rows[num2]["Name"].ToString());
			vlBarCode.ValueListItems.Add(dtItems.Rows[num2]["ItemID"], dtItems.Rows[num2]["ItemBarCode"].ToString());
		}
		dtDetails = ProceduresSteps.SelectByProcedureID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtProcedureStepItems = ProceduresStepsItems.SelectByProcedureID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtProcedurePrices = ProceduresPrices.SelectByProcedureID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		ds = new DataSet();
		ds.Tables.Add(dtDetails);
		ds.Tables.Add(dtProcedureStepItems);
		ds.Tables[0].TableName = "dtDetails";
		ds.Tables[1].TableName = "dtProcedureStepItems";
		ds.Relations.Add(ds.Tables[0].Columns["ProcedureStepID"], ds.Tables[1].Columns["ProcedureStepID"]);
		((UltraGridBase)ULGData).DataSource = ds;
		((UltraGridBase)ULGDataPrices).DataSource = dtProcedurePrices;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		GlobalFunctions.PrepareGrid(ULGDataPrices);
		((UltraGridBase)ULGDataPrices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProcedureStepID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StepID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PricePercentage"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpectedDelay"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Description"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StepID"].Header).Caption = (GlobalVariables.IsArabic ? "المرحلة" : "Step");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PricePercentage"].Header).Caption = (GlobalVariables.IsArabic ? "النسبة المئوية للسعر" : "Price Percentage");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpectedDelay"].Header).Caption = (GlobalVariables.IsArabic ? "التأخير المتوقع" : "Expected Delay");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Description"].Header).Caption = (GlobalVariables.IsArabic ? "الوصف" : "Description");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StepID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PricePercentage"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpectedDelay"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Description"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StepID"].ValueList = (IValueList)(object)vlSteps;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PricePercentage"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpectedDelay"].DefaultCellValue = 0;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ColorID"].Hidden = !UsingColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemSizeID"].Hidden = !UsingSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ColorID"].ValueList = (IValueList)(object)vlColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemSizeID"].DefaultCellValue = 1;
		if (UsingBatchNoAndValidityPeriod)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BatchID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BatchID"].ValueList = (IValueList)(object)vlBatchs;
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BatchID"].Hidden = true;
		}
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "Batch No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "BarCode");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "مخزن" : "Store");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemBarCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemBarCode"].ValueList = (IValueList)(object)vlBarCode;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["StoreID"].ValueList = (IValueList)(object)vlStores;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGDataPrices).DisplayLayout.Bands[0].Columns["ProcedurePriceID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.7) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataPrices).DisplayLayout.Bands[0].Columns["Price"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGDataPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع السعر" : "Price Type");
		((HeaderBase)((UltraGridBase)ULGDataPrices).DisplayLayout.Bands[0].Columns["Price"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
		((UltraGridBase)ULGDataPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Hidden = false;
		((UltraGridBase)ULGDataPrices).DisplayLayout.Bands[0].Columns["Price"].Hidden = false;
		((UltraGridBase)ULGDataPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].ValueList = (IValueList)(object)vlPriceTypes;
		((UltraGridBase)ULGDataPrices).DisplayLayout.Bands[0].Columns["Price"].DefaultCellValue = 0;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = Procedures.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
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
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["ProcedureCode"].ToString();
			((Control)(object)txtNameAr).Text = drMaster["ProcedureNameAr"].ToString();
			((Control)(object)txtNameEn).Text = drMaster["ProcedureNameEn"].ToString();
			((TextEditorControlBase)cboSpecializations).Value = drMaster["SpecializationID"];
			((UltraToggleEditorBase)chkCanModPrice).Checked = Convert.ToBoolean(drMaster["CanModifyPrice"]);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			dtDetails = ProceduresSteps.SelectByProcedureID(drMaster["ProcedureID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtProcedureStepItems = ProceduresStepsItems.SelectByProcedureID(drMaster["ProcedureID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtProcedurePrices = ProceduresPrices.SelectByProcedureID(drMaster["ProcedureID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			ds = new DataSet();
			ds.Tables.Add(dtDetails);
			ds.Tables.Add(dtProcedureStepItems);
			ds.Tables[0].TableName = "dtDetails";
			ds.Tables[1].TableName = "dtProcedureStepItems";
			ds.Relations.Add(ds.Tables[0].Columns["ProcedureStepID"], ds.Tables[1].Columns["ProcedureStepID"]);
			((UltraGridBase)ULGData).DataSource = ds;
			((UltraGridBase)ULGDataPrices).DataSource = dtProcedurePrices;
			InitGrid();
		}
		else
		{
			ClearControls();
		}
		((TextEditorControlBase)txtCode).Focus();
	}

	private ValueList getBatchsValueList(int ItemID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtBatchs.Select("ItemID is null or ItemID=" + ItemID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["BatchID"].ToString(), array[i]["BatchName"].ToString());
		}
		return val;
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

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)txtNameAr).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNameEn).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSpecializations).ReadOnly = NavMode;
		((Control)(object)chkCanModPrice).Enabled = !NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		if (Adding || Updating)
		{
			DataView dataView = new DataView(dtStores);
			dataView.RowFilter = " Locked =0  And BranchID=" + GlobalVariables.CurrentBranchID;
			DataTable dataTable = dataView.ToTable();
			vlStores.ValueListItems.Clear();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				vlStores.ValueListItems.Add(dataTable.Rows[i]["StoreID"], dataTable.Rows[i]["StoreName"].ToString());
			}
		}
		else
		{
			vlStores.ValueListItems.Clear();
			for (int j = 0; j < dtStores.Rows.Count; j++)
			{
				vlStores.ValueListItems.Add(dtStores.Rows[j]["StoreID"], dtStores.Rows[j]["StoreName"].ToString());
			}
		}
		if (Adding)
		{
			DataView dataView2 = new DataView(dtItems);
			dataView2.RowFilter = " IsActive =1 ";
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			DataTable dataTable2 = dataView2.ToTable();
			for (int k = 0; k < dataTable2.Rows.Count; k++)
			{
				vlItems.ValueListItems.Add(dataTable2.Rows[k]["ItemID"], dataTable2.Rows[k]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dataTable2.Rows[k]["ItemID"], dataTable2.Rows[k]["ItemBarCode"].ToString());
			}
		}
		else
		{
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int l = 0; l < dtItems.Rows.Count; l++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[l]["ItemID"], dtItems.Rows[l]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dtItems.Rows[l]["ItemID"], dtItems.Rows[l]["ItemBarCode"].ToString());
			}
		}
		if (Updating)
		{
			for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; m++)
			{
				for (int n = 0; n < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[m].ChildBands[0].Rows).Count; n++)
				{
					if (((UltraGridBase)ULGData).Rows[m].ChildBands[0].Rows[n].Cells["ItemID"].Value == DBNull.Value)
					{
						continue;
					}
					DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).Rows[m].ChildBands[0].Rows[n].Cells["ItemID"].Value.ToString())[0];
					if (UsingBatchNoAndValidityPeriod)
					{
						ValueList batchsValueList = getBatchsValueList(int.Parse(dataRow["ItemID"].ToString()));
						((UltraGridBase)ULGData).Rows[m].ChildBands[0].Rows[n].Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
						if (((DisposableObjectCollectionBase)batchsValueList.ValueListItems).Count == 0)
						{
							((UltraGridBase)ULGData).Rows[m].ChildBands[0].Rows[n].Cells["BatchID"].Value = DBNull.Value;
						}
					}
					if (!bool.Parse(dataRow["IsService"].ToString()))
					{
						int unitTypeID = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).Rows[m].ChildBands[0].Rows[n].Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
						ValueList unitsValueList = getUnitsValueList(unitTypeID);
						((UltraGridBase)ULGData).Rows[m].ChildBands[0].Rows[n].Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
						if (((DisposableObjectCollectionBase)unitsValueList.ValueListItems).Count == 0)
						{
							((UltraGridBase)ULGData).Rows[m].ChildBands[0].Rows[n].Cells["UnitID"].Value = DBNull.Value;
						}
					}
					if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).Rows[m].ChildBands[0].Rows[n].Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
						if (((UltraGridBase)ULGData).Rows[m].ChildBands[0].Rows[n].Cells["ColorID"].ValueList.ItemCount == 0)
						{
							((UltraGridBase)ULGData).Rows[m].ChildBands[0].Rows[n].Cells["ColorID"].Value = DBNull.Value;
						}
					}
					if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).Rows[m].ChildBands[0].Rows[n].Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
						if (((UltraGridBase)ULGData).Rows[m].ChildBands[0].Rows[n].Cells["ItemSizeID"].ValueList.ItemCount == 0)
						{
							((UltraGridBase)ULGData).Rows[m].ChildBands[0].Rows[n].Cells["ItemSizeID"].Value = DBNull.Value;
						}
					}
				}
			}
		}
		((UltraGridBase)ULGDataPrices).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGDataPrices).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		((Control)(object)txtCode).Text = (Adding ? Procedures.GetCode(IsFromServer: true) : "");
		((TextEditorControlBase)txtNameAr).Clear();
		((TextEditorControlBase)txtNameEn).Clear();
		cboSpecializations.SelectedIndex = -1;
		((TextEditorControlBase)txtNotes).Clear();
		FillGridPrices();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[1].Rows.Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[0].Rows.Clear();
	}

	private void FillGridPrices()
	{
		dtProcedurePrices.Rows.Clear();
		for (int i = 0; i < dtPriceTypes.Rows.Count; i++)
		{
			DataRow dataRow = dtProcedurePrices.NewRow();
			dataRow["ProcedurePriceID"] = -1;
			dataRow["ProcedureID"] = -1;
			dataRow["PriceTypeID"] = dtPriceTypes.Rows[i]["PriceTypeID"];
			dataRow["Price"] = 0;
			dataRow["Deleted"] = false;
			dataRow["BranchID"] = DBNull.Value;
			dtProcedurePrices.Rows.Add(dataRow);
		}
		InitGrid();
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم  الأذن" : "Please Enter The Voucher Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtNameAr).Text == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال إسم الإجراء" : "Please Enter Procedure Name");
			((TextEditorControlBase)txtNameAr).Focus();
			return false;
		}
		if (cboSpecializations.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار التخصص" : "Please Select A Specialization");
			((TextEditorControlBase)cboSpecializations).Focus();
			cboSpecializations.DropDown();
			return false;
		}
		if (Main.CheckForValue("CL_Procedures", "ProcedureNameAr", ((Control)(object)txtNameAr).Text.Trim(), Adding ? "" : drMaster["ProcedureNameAr"].ToString(), IsFromServer: true) > 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "هذا الاجراء موجود من قبل" : "This Procedure Name Already Exists");
			((TextEditorControlBase)txtNameAr).Focus();
			return false;
		}
		if (Main.CheckForValue("CL_Procedures", "ProcedureCode", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["ProcedureCode"].ToString(), IsFromServer: true) > 0)
		{
			string code = Procedures.GetCode(IsFromServer: true);
			GlobalVariables.QuestionMB.Show("رقم هذا الإجراء متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Procedure Code Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = code;
		}
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["StepID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم المرحلة  ", "Please Enter Step Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["StepID"];
				((UltraGridBase)ULGData).Rows[i].Cells["StepID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["PricePercentage"].Value.ToString());
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGData).Rows[i].Cells["StepID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["StepID"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show("لا يمكن تكرار المرحلة ", "Cannot Duplicate The Same Step");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["StepID"];
					return false;
				}
			}
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; k++)
			{
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["ItemID"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء إختيار الصنف  ", "Please Select Item ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["ItemID"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["UnitID"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء إختيار الوحدة  ", "Please Select Unit ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["UnitID"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["Qty"].Value.ToString()) <= 0m)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء ادخال الكمية   ", "Please Enter Qty");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["Qty"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; l++)
				{
					if (k != l && ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["ItemID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[l].Cells["ItemID"].Value.ToString())
					{
						((Control)(object)ULGData).Enter -= ULGData_Enter;
						GlobalVariables.InformationMB.Show("لا يمكن تكرار الصنف لنفس المرحلة ", "Cannot Duplicate The Same Item");
						((UltraGridBase)ULGData).ActiveRow = ((UltraGridBase)ULGData).Rows[i];
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["ItemID"];
						((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["ItemID"].DroppedDown = true;
						ULGData.PerformAction((UltraGridAction)24);
						((Control)(object)ULGData).Enter += ULGData_Enter;
						return false;
					}
				}
			}
		}
		if (num < 100m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "مجموع النسبة المئوية لهذه المراحل لا يساوي 100% " : "These Steps Total Price Percentage Does not Equal 100% ");
			return false;
		}
		for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataPrices).Rows).Count; m++)
		{
			if (((UltraGridBase)ULGDataPrices).Rows[m].Cells["PriceTypeID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار نوع السعر  ", "Please Select Price Type");
				ULGDataPrices.ActiveCell = ((UltraGridBase)ULGDataPrices).Rows[m].Cells["PriceTypeID"];
				((UltraTabControlBase)UTCDetails).Tabs["Prices"].Selected = true;
				ULGDataPrices.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
		}
		return true;
	}

	public override void AddData()
	{
		string text = ((Control)(object)txtCode).Text;
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = Procedures.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtNameAr).Text, (((Control)(object)txtNameEn).Text == "") ? ((Control)(object)txtNameAr).Text : ((Control)(object)txtNameEn).Text, (cboSpecializations.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSpecializations).Value.ToString(), ((UltraToggleEditorBase)chkCanModPrice).Checked ? "1" : "0", ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				int num2 = ProceduresSteps.Insert_Update("-1", num.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["StepID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["Description"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["Description"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["PricePercentage"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["ExpectedDelay"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
				{
					ProceduresStepsItems.Insert_Update("-1", num.ToString(), num2.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BatchID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BatchID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Qty"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["StoreID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["StoreID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				}
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataPrices).Rows).Count > 0)
			{
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataPrices).Rows).Count; k++)
				{
					((UltraGridBase)ULGDataPrices).Rows[k].Cells["ProcedurePriceID"].Value = -1;
					((UltraGridBase)ULGDataPrices).Rows[k].Cells["ProcedureID"].Value = num;
					((UltraGridBase)ULGDataPrices).Rows[k].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				}
				ProceduresPrices.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataPrices).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			Main.EndBulkTrans(FromServer: false);
			ItemsTransactions.ManageInThread();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void UpdateData()
	{
		string text = ((Control)(object)txtCode).Text;
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = Procedures.Insert_Update(drMaster["ProcedureID"].ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtNameAr).Text, (((Control)(object)txtNameEn).Text == "") ? "Null" : ((Control)(object)txtNameEn).Text, (cboSpecializations.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSpecializations).Value.ToString(), ((UltraToggleEditorBase)chkCanModPrice).Checked ? "1" : "0", ((Control)(object)txtNotes).Text, bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			string text2 = ",";
			string text3 = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				text2 = text2 + ((UltraGridBase)ULGData).Rows[i].Cells["ProcedureStepID"].Value.ToString() + ",";
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
				{
					text3 = text3 + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ProcedureStepItemID"].Value.ToString() + ",";
				}
			}
			Main.DeleteForUpdate("CL_ProceduresStepsItems", "ProcedureID", drMaster["ProcedureID"].ToString(), "ProcedureStepItemID", text3);
			Main.DeleteForUpdate("CL_ProceduresSteps", "ProcedureID", drMaster["ProcedureID"].ToString(), "ProcedureStepID", text2);
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
			{
				int num2 = ProceduresSteps.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[k].Cells["ProcedureStepID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[k].Cells["ProcedureStepID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[k].Cells["ProcedureStepID"].Value.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[k].Cells["StepID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["Description"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].Cells["Description"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].Cells["PricePercentage"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].Cells["ExpectedDelay"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows).Count; l++)
				{
					ProceduresStepsItems.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["ProcedureStepItemID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["ProcedureStepItemID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["ProcedureStepItemID"].Value.ToString(), num.ToString(), num2.ToString(), ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["BatchID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["BatchID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["Qty"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["UnitID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["StoreID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["StoreID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				}
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataPrices).Rows).Count > 0)
			{
				string text4 = ",";
				for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataPrices).Rows).Count; m++)
				{
					((UltraGridBase)ULGDataPrices).Rows[m].Cells["ProcedureID"].Value = num;
					((UltraGridBase)ULGDataPrices).Rows[m].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
					text4 = text4 + ((UltraGridBase)ULGDataPrices).Rows[m].Cells["ProcedurePriceID"].Value.ToString() + ",";
				}
				Main.DeleteForUpdate("CL_ProceduresPrices", "ProcedureID", drMaster["ProcedureID"].ToString(), "ProcedurePriceID", text4);
				ProceduresPrices.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataPrices).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			Main.EndBulkTrans(FromServer: false);
			ItemsTransactions.ManageInThread();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			Procedures.DeleteVirtual(drMaster["ProcedureID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			ProceduresSteps.DeleteVirtualByProcedureID(drMaster["ProcedureID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			ProceduresStepsItems.DeleteVirtualByProcedureID(drMaster["ProcedureID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			ProceduresPrices.DeleteVirtualByProcedureID(drMaster["ProcedureID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ProceduresSearchReport(IsFromServer: true);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["ProcedureID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		object value = ((TextEditorControlBase)cboSpecializations).Value;
		dtSpecializations = Specializations.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSpecializations, dtSpecializations, "SpecializationID", "SpecializationName");
		dtSteps = Steps.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlSteps.ValueListItems.Clear();
		for (int i = 0; i < dtSteps.Rows.Count; i++)
		{
			vlSteps.ValueListItems.Add(dtSteps.Rows[i]["StepID"], dtSteps.Rows[i]["StepName"].ToString());
		}
		dtPriceTypes = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlPriceTypes.ValueListItems.Clear();
		for (int j = 0; j < dtPriceTypes.Rows.Count; j++)
		{
			vlPriceTypes.ValueListItems.Add(dtPriceTypes.Rows[j]["PriceTypeID"], dtPriceTypes.Rows[j]["PriceName"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		if (UsingColors)
		{
			dtItemsColorCategorysDetails = ItemsColorCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlColors.ValueListItems.Clear();
			for (int k = 0; k < dtColors.Rows.Count; k++)
			{
				vlColors.ValueListItems.Add(dtColors.Rows[k]["ColorID"], dtColors.Rows[k]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtItemsSizeCategorysDetails = ItemsSizeCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSizes.ValueListItems.Clear();
			for (int l = 0; l < dtSizes.Rows.Count; l++)
			{
				vlSizes.ValueListItems.Add(dtSizes.Rows[l]["ItemSizeID"], dtSizes.Rows[l]["ItemSizeName"].ToString());
			}
		}
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		AutomaticlyAddItemTaxToSalesInvoice = GlobalFunctions.GetOption("AutomaticlyAddItemTaxToSalesInvoice");
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchs.ValueListItems.Clear();
			for (int m = 0; m < dtBatchs.Rows.Count; m++)
			{
				vlBatchs.ValueListItems.Add(dtBatchs.Rows[m]["BatchID"], dtBatchs.Rows[m]["BatchName"].ToString());
			}
		}
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItems = Items.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		DataView dataView = new DataView(dtStores);
		dataView.RowFilter = " Locked =0 And BranchID=" + GlobalVariables.CurrentBranchID;
		DataTable dataTable = dataView.ToTable();
		vlStores.ValueListItems.Clear();
		for (int n = 0; n < dataTable.Rows.Count; n++)
		{
			vlStores.ValueListItems.Add(dataTable.Rows[n]["StoreID"], dataTable.Rows[n]["StoreName"].ToString());
		}
		if (Adding)
		{
			DataView dataView2 = new DataView(dtItems);
			dataView2.RowFilter = " IsActive =1 ";
			DataTable dataTable2 = dataView2.ToTable();
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int num = 0; num < dataTable2.Rows.Count; num++)
			{
				vlItems.ValueListItems.Add(dataTable2.Rows[num]["ItemID"], dataTable2.Rows[num]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dataTable2.Rows[num]["ItemID"], dataTable2.Rows[num]["ItemBarCode"].ToString());
			}
		}
		else
		{
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int num2 = 0; num2 < dtItems.Rows.Count; num2++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[num2]["ItemID"], dtItems.Rows[num2]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dtItems.Rows[num2]["ItemID"], dtItems.Rows[num2]["ItemBarCode"].ToString());
			}
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int num3 = 0; num3 < dtUnits.Rows.Count; num3++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[num3]["UnitID"], dtUnits.Rows[num3]["UnitName"].ToString());
		}
		((TextEditorControlBase)cboSpecializations).Value = value;
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		((Control)(object)ULGData).Enter -= ULGData_Enter;
		if (((GridItemBase)e.Row).Band.Index == 0)
		{
			e.Row.Cells["ProcedureStepID"].Value = ++newID;
		}
		else if (((GridItemBase)e.Row).Band.Index == 1)
		{
			e.Row.Cells["ProcedureStepItemID"].Value = ++newID;
		}
		((Control)(object)ULGData).Enter += ULGData_Enter;
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		((UltraGridBase)ULGData).UpdateData();
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1 && ULGData.ActiveCell != null && (((KeyedSubObjectBase)e.Cell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID") && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"];
			object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = e.Cell.Value);
			obj.Value = value;
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			int num = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtItems.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"].ToString()) : 0);
			if (UsingBatchNoAndValidityPeriod)
			{
				if (num != 0)
				{
					e.Cell.Row.Cells["BatchID"].Value = DBNull.Value;
					e.Cell.Row.Cells["BatchID"].ValueList = (IValueList)(object)getBatchsValueList(num);
				}
				else
				{
					e.Cell.Row.Cells["BatchID"].ValueList = null;
					e.Cell.Row.Cells["BatchID"].Value = DBNull.Value;
				}
			}
			int num2 = ((((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value != DBNull.Value) ? int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()) : 0);
			if (num2 != 0)
			{
				e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
				e.Cell.Row.Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(num2);
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
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
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
		{
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value == DBNull.Value)
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "StoreID") && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty")
		{
			GlobalFunctions.CheckForIntegers(ULGData.ActiveCell, e);
		}
	}

	private void ULGDataServices_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Price")
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGDataPrices_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (ULGDataPrices.ActiveCell != null && (((KeyedSubObjectBase)ULGDataPrices.ActiveCell.Column).Key != "Price" || (!Adding && !Updating)))
		{
			((GridItemBase)((UltraGridBase)ULGDataPrices).ActiveRow).Selected = true;
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
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Expected O, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected O, but got Unknown
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Expected O, but got Unknown
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected O, but got Unknown
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Expected O, but got Unknown
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Expected O, but got Unknown
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Expected O, but got Unknown
		//IL_0558: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Expected O, but got Unknown
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Clinics.MasterData.frmProcedures));
		UltraTab val = new UltraTab();
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
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.ULGDataPrices = new UltraGrid();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblSpecializations = new UltraLabel();
		this.cboSpecializations = new UltraComboEditor();
		this.txtNameEn = new UltraTextEditor();
		this.lblVesselNameEn = new UltraLabel();
		this.txtNameAr = new UltraTextEditor();
		this.lblVesselNameAr = new UltraLabel();
		this.chkCanModPrice = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataPrices).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSpecializations).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkCanModPrice).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((UltraTabControlBase)base.UTCDetails).TabOrientation = (TabOrientation)1;
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		((KeyedSubObjectBase)val).Key = "Prices";
		val.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val, "ultraTab2");
		((SubObjectBase)val).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[1] { val });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl3, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		resources.ApplyResources(base.ULGData, "ULGData");
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val2, "appearance6");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val3, "appearance7");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val4, "appearance8");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val5, "appearance9");
		((AppearanceBase)val5).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val6).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val6).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val6).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val6, "appearance10");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val7, "appearance11");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val8, "appearance12");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val8;
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		resources.ApplyResources(base.btnSetting, "btnSetting");
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance13");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val9;
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
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataPrices);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		resources.ApplyResources(this.ULGDataPrices, "ULGDataPrices");
		((UltraGridBase)this.ULGDataPrices).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataPrices).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataPrices).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val10, "appearance1");
		((UltraGridBase)this.ULGDataPrices).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGDataPrices).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val11).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val11).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val11).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val11, "appearance2");
		((AppearanceBase)val11).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataPrices).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGDataPrices).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val12).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val12, "appearance3");
		((UltraGridBase)this.ULGDataPrices).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val12;
		((AppearanceBase)val13).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val13).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val13, "appearance4");
		((UltraGridBase)this.ULGDataPrices).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGDataPrices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataPrices).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val14).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val14).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val14, "appearance5");
		((UltraGridBase)this.ULGDataPrices).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGDataPrices).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataPrices).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataPrices).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataPrices).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataPrices).Name = "ULGDataPrices";
		((UltraControlBase)this.ULGDataPrices).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataPrices.AfterEnterEditMode += new System.EventHandler(ULGDataPrices_AfterEnterEditMode);
		((System.Windows.Forms.Control)(object)this.ULGDataPrices).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGDataServices_KeyPress);
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblSpecializations, "lblSpecializations");
		this.lblSpecializations.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSpecializations).Name = "lblSpecializations";
		((ControlBase)this.lblSpecializations).WrapText = false;
		resources.ApplyResources(this.cboSpecializations, "cboSpecializations");
		((TextEditorControlBase)this.cboSpecializations).AlwaysInEditMode = true;
		this.cboSpecializations.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSpecializations).Name = "cboSpecializations";
		resources.ApplyResources(this.txtNameEn, "txtNameEn");
		((System.Windows.Forms.Control)(object)this.txtNameEn).Name = "txtNameEn";
		resources.ApplyResources(this.lblVesselNameEn, "lblVesselNameEn");
		this.lblVesselNameEn.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVesselNameEn).Name = "lblVesselNameEn";
		((ControlBase)this.lblVesselNameEn).WrapText = false;
		resources.ApplyResources(this.txtNameAr, "txtNameAr");
		((System.Windows.Forms.Control)(object)this.txtNameAr).Name = "txtNameAr";
		resources.ApplyResources(this.lblVesselNameAr, "lblVesselNameAr");
		this.lblVesselNameAr.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVesselNameAr).Name = "lblVesselNameAr";
		((ControlBase)this.lblVesselNameAr).WrapText = false;
		resources.ApplyResources(this.chkCanModPrice, "chkCanModPrice");
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val15, "appearance15");
		((UltraToggleEditorBase)this.chkCanModPrice).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.chkCanModPrice).Name = "chkCanModPrice";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkCanModPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVesselNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNameAr);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVesselNameAr);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSpecializations);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSpecializations);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Name = "frmProcedures";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSpecializations, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSpecializations, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UTCDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVesselNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVesselNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkCanModPrice, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataPrices).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSpecializations).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkCanModPrice).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
