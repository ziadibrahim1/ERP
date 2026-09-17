using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.HR;
using BusinessLayer.Production;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using ERP.StockControl.MasterData;
using ERP.StockControl.Reports;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.Production.Transactions;

public class frmProductions : frmHeaderManyDetails
{
	private DataTable dtProductionRequest;

	private DataTable dtLines;

	private DataTable dtItemCataloge;

	private DataTable dtItems;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtBatchs;

	private DataTable dtStores;

	private DataTable dtUnits;

	private DataTable dtStages;

	private DataTable dtExpenses;

	private DataTable dtEmployees;

	private DataTable dtProductionStagesInputs;

	private DataTable dtProductionStagesOutputs;

	private DataTable dtProductionStagesOutputsExpenses;

	private DataTable dtProductionsFeesDetailsEmployees;

	private DataTable dtRequestItemWithQty;

	private DataTable dtRequestCataloges;

	private DataTable dtOriginalInputItems;

	private DataTable dtOriginalOutputItems;

	private DataTable dtOriginalOutputItemsExpenses;

	private ValueList vlItemsInput = new ValueList();

	private ValueList vlItemsOutput = new ValueList();

	private ValueList vlBatchsInput = new ValueList();

	private ValueList vlBatchsOutput = new ValueList();

	private ValueList vlUnitInput = new ValueList();

	private ValueList vlUnitOutput = new ValueList();

	private ValueList vlStoreInput = new ValueList();

	private ValueList vlStoreOutput = new ValueList();

	private ValueList vlStages = new ValueList();

	private ValueList vlExpenses = new ValueList();

	private ValueList vlEmployees = new ValueList();

	private ValueList vlColorsInput = new ValueList();

	private ValueList vlSizesInput = new ValueList();

	private ValueList vlColorsOutput = new ValueList();

	private ValueList vlSizesOutPut = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private bool UsingBatchNoAndValidityPeriod = false;

	private bool AnyStageApproved = false;

	private decimal QtyBeforeUpdate = 1m;

	private DataSet ds;

	private int newID = -100000;

	private IContainer components = null;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblBatchNo;

	private UltraTextEditor txtBatchNo;

	private UltraLabel lblRequestQty;

	private UltraTextEditor txtRequestQty;

	private UltraLabel lblSourceStore;

	private UltraComboEditor cboSourceStore;

	private UltraLabel lblDestinationStore;

	private UltraComboEditor cboDestinationStore;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraPanel pnlCheckType;

	private RadioButton rbIsProductionRequest;

	private RadioButton rbIsDirect;

	private UltraLabel lblProductionStartDate;

	private UltraDateTimeEditor dtpProductionStartDate;

	private UltraLabel lblProductionEndDate;

	private UltraDateTimeEditor dtpProductionEndDate;

	private UltraLabel lblProductionRequest;

	private UltraComboEditor cboProductionRequest;

	private UltraLabel lblRequestItemID;

	private UltraComboEditor cboRequestItem;

	private UltraLabel lblLine;

	private UltraComboEditor cboLine;

	private UltraLabel lblItemCataloge;

	private UltraComboEditor cboItemCataloge;

	private UltraLabel lblQty;

	private UltraTextEditor txtQty;

	private UltraComboEditor cboUnitName;

	public UltraButton btnRequestNoSearch;

	public UltraButton btnPrintBarCode;

	public UltraButton btnItemCatalogSearch;

	public frmProductions()
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
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		InitializeComponent();
		TableName = "Pro_Productions";
		IDCol = "ProductionID";
		NoCol = "ProductionNo";
		DateCol = "ProductionDate";
	}

	public frmProductions(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtpProductionStartDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtpProductionEndDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
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
		dtProductionRequest = ProductionRequests.FillCombo(GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
		GlobalFunctions.FillCombo(cboProductionRequest, dtProductionRequest, "ProductionRequestID", "ProductionRequestNo");
		dtLines = BusinessLayer.Production.Lines.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboLine, dtLines, "LineID", "LineName");
		dtItemCataloge = ItemsCataloge.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboItemCataloge, dtItemCataloge, "ItemCatalogeID", "ItemCatalogeName");
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "1", "0", "1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItemsInput.ValueListItems.Clear();
		vlItemsOutput.ValueListItems.Clear();
		for (int k = 0; k < dtItems.Rows.Count; k++)
		{
			vlItemsInput.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["Name"].ToString());
			vlItemsOutput.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["Name"].ToString());
		}
		GlobalFunctions.FillCombo(cboRequestItem, dtItems, "ItemID", "Name");
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchsInput.ValueListItems.Clear();
			vlBatchsOutput.ValueListItems.Clear();
			for (int l = 0; l < dtBatchs.Rows.Count; l++)
			{
				vlBatchsInput.ValueListItems.Add(dtBatchs.Rows[l]["BatchID"], dtBatchs.Rows[l]["BatchName"].ToString());
				vlBatchsOutput.ValueListItems.Add(dtBatchs.Rows[l]["BatchID"], dtBatchs.Rows[l]["BatchName"].ToString());
			}
		}
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSourceStore, dtStores, "StoreID", "StoreName");
		GlobalFunctions.FillCombo(cboDestinationStore, dtStores, "StoreID", "StoreName");
		vlStoreInput.ValueListItems.Clear();
		vlStoreOutput.ValueListItems.Clear();
		for (int m = 0; m < dtStores.Rows.Count; m++)
		{
			vlStoreInput.ValueListItems.Add(dtStores.Rows[m]["StoreID"], dtStores.Rows[m]["StoreName"].ToString());
			vlStoreOutput.ValueListItems.Add(dtStores.Rows[m]["StoreID"], dtStores.Rows[m]["StoreName"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboUnitName, dtUnits, "UnitID", "UnitName");
		vlUnitInput.ValueListItems.Clear();
		vlUnitOutput.ValueListItems.Clear();
		for (int n = 0; n < dtUnits.Rows.Count; n++)
		{
			vlUnitInput.ValueListItems.Add(dtUnits.Rows[n]["UnitID"], dtUnits.Rows[n]["UnitName"].ToString());
			vlUnitOutput.ValueListItems.Add(dtUnits.Rows[n]["UnitID"], dtUnits.Rows[n]["UnitName"].ToString());
		}
		dtStages = Stages.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlStages.ValueListItems.Clear();
		for (int num = 0; num < dtStages.Rows.Count; num++)
		{
			vlStages.ValueListItems.Add(dtStages.Rows[num]["StageID"], dtStages.Rows[num]["StageName"].ToString());
		}
		dtExpenses = Expenses.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlExpenses.ValueListItems.Clear();
		for (int num2 = 0; num2 < dtExpenses.Rows.Count; num2++)
		{
			vlExpenses.ValueListItems.Add(dtExpenses.Rows[num2]["ExpenseID"], dtExpenses.Rows[num2]["ExpenseName"].ToString());
		}
		dtEmployees = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlEmployees.ValueListItems.Clear();
		for (int num3 = 0; num3 < dtEmployees.Rows.Count; num3++)
		{
			vlEmployees.ValueListItems.Add(dtEmployees.Rows[num3]["SubAccountID"], dtEmployees.Rows[num3]["SubAccountName"].ToString());
		}
		dtDetails = ProductionsStages.SelectByProductionID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtProductionStagesInputs = ProductionsStagesInputs.SelectByProductionID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtProductionStagesOutputs = ProductionsStagesOutputs.SelectByProductionID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtProductionStagesOutputsExpenses = ProductionsStagesOutputsExpenses.SelectByProductionID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtProductionsFeesDetailsEmployees = ProductionsFeesDetailsEmployees.SelectByProductionID("0", GlobalVariables.IsArabic ? "1" : "0");
		ds = new DataSet();
		ds.Tables.Add(dtDetails);
		ds.Tables.Add(dtProductionStagesInputs);
		ds.Tables.Add(dtProductionStagesOutputs);
		ds.Tables.Add(dtProductionStagesOutputsExpenses);
		ds.Tables.Add(dtProductionsFeesDetailsEmployees);
		ds.Tables[0].TableName = "dtDetails";
		ds.Tables[1].TableName = "dtProductionStagesInputs";
		ds.Tables[2].TableName = "dtProductionStagesOutputs";
		ds.Tables[3].TableName = "dtProductionStagesOutputsExpenses";
		ds.Tables[4].TableName = "dtProductionsFeesDetailsEmployees";
		ds.Relations.Add(ds.Tables[0].Columns["ProductionStageID"], ds.Tables[1].Columns["ProductionStageID"]);
		ds.Relations.Add(ds.Tables[0].Columns["ProductionStageID"], ds.Tables[2].Columns["ProductionStageID"]);
		ds.Relations.Add(ds.Tables[0].Columns["ProductionStageID"], ds.Tables[4].Columns["ProductionStageID"]);
		ds.Relations.Add(ds.Tables[2].Columns["ProductionStageOutputID"], ds.Tables[3].Columns["ProductionStageOutputID"]);
		((UltraGridBase)ULGData).DataSource = ds;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].HeaderVisible = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Header).Caption = (GlobalVariables.IsArabic ? "الاصناف الداخله في المرحله" : "Input Items");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Header).Appearance.BackColor = Color.LightSkyBlue;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Header).Appearance.BackColor2 = Color.LightYellow;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Header).Appearance.BackGradientStyle = (GradientStyle)3;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Header).Appearance.ForeColor = Color.Blue;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].HeaderVisible = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Header).Caption = (GlobalVariables.IsArabic ? "الاصناف الناتجه من المرحله" : "Output Items");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Header).Appearance.BackColor = Color.AliceBlue;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Header).Appearance.BackColor2 = Color.CornflowerBlue;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Header).Appearance.BackGradientStyle = (GradientStyle)3;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Header).Appearance.ForeColor = Color.Blue;
		((UltraGridBase)ULGData).DisplayLayout.Bands[4].HeaderVisible = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[4].Header).Caption = (GlobalVariables.IsArabic ? "القائمين بالعمل" : "Employees");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[4].Header).Appearance.BackColor = Color.Azure;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[4].Header).Appearance.BackColor2 = Color.CornflowerBlue;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[4].Header).Appearance.BackGradientStyle = (GradientStyle)3;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[4].Header).Appearance.ForeColor = Color.Blue;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StageID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitFees"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompletePercentage"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StageID"].Header).Caption = (GlobalVariables.IsArabic ? "المرحلة" : "Stage");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitFees"].Header).Caption = (GlobalVariables.IsArabic ? "أجر الوحدة" : "Unit Fees");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompletePercentage"].Header).Caption = (GlobalVariables.IsArabic ? "نسبة التمام" : "Complete Percentage");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StageID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitFees"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompletePercentage"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StageID"].ValueList = (IValueList)(object)vlStages;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitFees"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Deleted"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompletePercentage"].DefaultCellValue = 0;
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
		if (UsingBatchNoAndValidityPeriod)
		{
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "رقم التشغيلة" : "Batch No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BatchID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BatchID"].ValueList = (IValueList)(object)vlBatchsInput;
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["EstimatedQty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية المقدرة" : "Estimated Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ActualQty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "مخزن" : "Store");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["EstimatedQty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ActualQty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].ValueList = (IValueList)(object)vlItemsInput;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].ValueList = (IValueList)(object)vlUnitInput;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["StoreID"].ValueList = (IValueList)(object)vlStoreInput;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ActualQty"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["EstimatedQty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ActualQty"].Format = GlobalVariables.QtyDecimals;
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
		if (UsingBatchNoAndValidityPeriod)
		{
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "رقم التشغيلة" : "Batch No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["BatchID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["BatchID"].ValueList = (IValueList)(object)vlBatchsOutput;
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["EstimatedQty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية المقدرة" : "Estimated Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ActualQty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["CostAllocationRate"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة البيعية" : "Sales Value");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "مخزن" : "Store");
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["EstimatedQty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ActualQty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["CostAllocationRate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ItemID"].ValueList = (IValueList)(object)vlItemsOutput;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["UnitID"].ValueList = (IValueList)(object)vlUnitOutput;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["StoreID"].ValueList = (IValueList)(object)vlStoreOutput;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ActualQty"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["CostAllocationRate"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["EstimatedQty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ActualQty"].Format = GlobalVariables.QtyDecimals;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[3].Columns["ExpenseID"].Header).Caption = (GlobalVariables.IsArabic ? "المصروف" : "Expense");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[3].Columns["EstimatedValue"].Header).Caption = (GlobalVariables.IsArabic ? " القيمة المقدرة" : "Estimated Value");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[3].Columns["ActualValue"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة" : "Value");
		((UltraGridBase)ULGData).DisplayLayout.Bands[3].Columns["ExpenseID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[3].Columns["ActualValue"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[3].Columns["EstimatedValue"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[3].Columns["ExpenseID"].ValueList = (IValueList)(object)vlExpenses;
		((UltraGridBase)ULGData).DisplayLayout.Bands[3].Columns["ActualValue"].DefaultCellValue = 0;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[4].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الموظف" : "Employee");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[4].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[4].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[4].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[4].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[4].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[4].Columns["SubAccountID"].ValueList = (IValueList)(object)vlEmployees;
		((UltraGridBase)ULGData).DisplayLayout.Bands[4].Columns["Qty"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[4].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = Productions.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
			((Control)(object)txtCode).Text = drMaster["ProductionNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["ProductionDate"];
			rbIsDirect.Checked = bool.Parse(drMaster["IsDirect"].ToString());
			rbIsProductionRequest.Checked = bool.Parse(drMaster["IsProductionRequest"].ToString());
			((Control)(object)txtBatchNo).Text = drMaster["BatchNo"].ToString();
			dtpProductionStartDate.Value = (DateTime)drMaster["ProductionStartDate"];
			dtpProductionEndDate.Value = (DateTime)drMaster["ProductionEndDate"];
			((TextEditorControlBase)cboProductionRequest).ValueChanged -= cboProductionRequest_ValueChanged;
			((TextEditorControlBase)cboProductionRequest).Value = drMaster["ProductionRequestID"];
			((TextEditorControlBase)cboProductionRequest).ValueChanged += cboProductionRequest_ValueChanged;
			((TextEditorControlBase)cboRequestItem).ValueChanged -= cboRequestItem_ValueChanged;
			((TextEditorControlBase)cboRequestItem).Value = drMaster["RequestItemID"];
			((TextEditorControlBase)cboRequestItem).ValueChanged += cboRequestItem_ValueChanged;
			((Control)(object)txtRequestQty).Text = drMaster["RequestQty"].ToString();
			((TextEditorControlBase)cboUnitName).Value = drMaster["RequestUnitID"];
			((TextEditorControlBase)cboItemCataloge).ValueChanged -= cboItemCataloge_ValueChanged;
			((TextEditorControlBase)cboItemCataloge).Value = drMaster["ItemCatalogeID"];
			((TextEditorControlBase)cboItemCataloge).ValueChanged += cboItemCataloge_ValueChanged;
			((TextEditorControlBase)cboLine).Value = drMaster["LineID"];
			((TextEditorControlBase)txtQty).ValueChanged -= txtQty_ValueChanged;
			((Control)(object)txtQty).Text = drMaster["Qty"].ToString();
			((TextEditorControlBase)txtQty).ValueChanged += txtQty_ValueChanged;
			QtyBeforeUpdate = decimal.Parse(((Control)(object)txtQty).Text);
			((TextEditorControlBase)cboSourceStore).ValueChanged -= cboSourceStore_ValueChanged;
			((TextEditorControlBase)cboDestinationStore).ValueChanged -= cboDestinationStore_ValueChanged;
			((TextEditorControlBase)cboSourceStore).Value = drMaster["InputStoreID"];
			((TextEditorControlBase)cboDestinationStore).Value = drMaster["OutputStoreID"];
			((TextEditorControlBase)cboSourceStore).ValueChanged += cboSourceStore_ValueChanged;
			((TextEditorControlBase)cboDestinationStore).ValueChanged += cboDestinationStore_ValueChanged;
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = ProductionsStages.SelectByProductionID(drMaster["ProductionID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtProductionStagesInputs = ProductionsStagesInputs.SelectByProductionID(drMaster["ProductionID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtProductionStagesOutputs = ProductionsStagesOutputs.SelectByProductionID(drMaster["ProductionID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtProductionStagesOutputsExpenses = ProductionsStagesOutputsExpenses.SelectByProductionID(drMaster["ProductionID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtProductionsFeesDetailsEmployees = ProductionsFeesDetailsEmployees.SelectByProductionID(drMaster["ProductionID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtOriginalInputItems = dtProductionStagesInputs.Copy();
			dtOriginalOutputItems = dtProductionStagesOutputs.Copy();
			dtOriginalOutputItemsExpenses = dtProductionStagesOutputsExpenses.Copy();
			ds = new DataSet();
			ds.Tables.Add(dtDetails);
			ds.Tables.Add(dtProductionStagesInputs);
			ds.Tables.Add(dtProductionStagesOutputs);
			ds.Tables.Add(dtProductionStagesOutputsExpenses);
			ds.Tables.Add(dtProductionsFeesDetailsEmployees);
			ds.Tables[0].TableName = "dtDetails";
			ds.Tables[1].TableName = "dtProductionStagesInputs";
			ds.Tables[2].TableName = "dtProductionStagesOutputs";
			ds.Tables[3].TableName = "dtProductionStagesOutputsExpenses";
			ds.Tables[4].TableName = "dtProductionsFeesDetailsEmployees";
			ds.Relations.Add(ds.Tables[0].Columns["ProductionStageID"], ds.Tables[1].Columns["ProductionStageID"]);
			ds.Relations.Add(ds.Tables[0].Columns["ProductionStageID"], ds.Tables[2].Columns["ProductionStageID"]);
			ds.Relations.Add(ds.Tables[0].Columns["ProductionStageID"], ds.Tables[4].Columns["ProductionStageID"]);
			ds.Relations.Add(ds.Tables[2].Columns["ProductionStageOutputID"], ds.Tables[3].Columns["ProductionStageOutputID"]);
			((UltraGridBase)ULGData).DataSource = ds;
			InitGrid();
			if (drMaster["Approved"].Equals(true))
			{
				((Control)(object)btnDelete).Enabled = false;
				((Control)(object)btnUpdate).Enabled = false;
			}
			else
			{
				((Control)(object)btnDelete).Enabled = true;
				((Control)(object)btnUpdate).Enabled = true;
			}
			AnyStageApproved = ((dtDetails.Select("Approved=1 Or CompletePercentage>0").Length != 0) ? true : false);
			if (AnyStageApproved)
			{
				((Control)(object)btnDelete).Enabled = false;
			}
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
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		rbIsDirect.Enabled = !NavMode && !AnyStageApproved;
		rbIsProductionRequest.Enabled = !NavMode && !AnyStageApproved;
		((EditorButtonControlBase)txtBatchNo).ReadOnly = NavMode || !CanUpdateProductionBatchNo;
		((EditorButtonControlBase)dtpProductionStartDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpProductionEndDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboProductionRequest).ReadOnly = NavMode;
		((EditorButtonControlBase)cboRequestItem).ReadOnly = NavMode || AnyStageApproved;
		((EditorButtonControlBase)txtRequestQty).ReadOnly = true;
		((EditorButtonControlBase)cboUnitName).ReadOnly = true;
		((EditorButtonControlBase)cboItemCataloge).ReadOnly = NavMode || AnyStageApproved;
		((Control)(object)btnItemCatalogSearch).Visible = !NavMode && !AnyStageApproved;
		((EditorButtonControlBase)cboLine).ReadOnly = NavMode;
		((EditorButtonControlBase)txtQty).ReadOnly = NavMode || AnyStageApproved;
		((EditorButtonControlBase)cboSourceStore).ReadOnly = NavMode;
		((EditorButtonControlBase)cboDestinationStore).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)btnPrintBarCode).Visible = NavMode;
		if (Adding || Updating)
		{
			int num = 0;
			int num2 = 0;
			if (cboSourceStore.SelectedIndex > -1)
			{
				num = int.Parse(((TextEditorControlBase)cboSourceStore).Value.ToString());
			}
			if (cboDestinationStore.SelectedIndex > -1)
			{
				num2 = int.Parse(((TextEditorControlBase)cboDestinationStore).Value.ToString());
			}
			((TextEditorControlBase)cboSourceStore).ValueChanged -= cboSourceStore_ValueChanged;
			((TextEditorControlBase)cboDestinationStore).ValueChanged -= cboDestinationStore_ValueChanged;
			if (Adding || Updating)
			{
				DataView dataView = new DataView(dtStores);
				dataView.RowFilter = " BranchID=" + GlobalVariables.CurrentBranchID;
				DataTable dataTable = dataView.ToTable();
				vlStoreInput.ValueListItems.Clear();
				vlStoreOutput.ValueListItems.Clear();
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					vlStoreInput.ValueListItems.Add(dataTable.Rows[i]["StoreID"], dataTable.Rows[i]["StoreName"].ToString());
					vlStoreOutput.ValueListItems.Add(dataTable.Rows[i]["StoreID"], dataTable.Rows[i]["StoreName"].ToString());
				}
				GlobalFunctions.FillCombo(cboSourceStore, dataTable, "StoreID", "StoreName");
				GlobalFunctions.FillCombo(cboDestinationStore, dataTable, "StoreID", "StoreName");
			}
			else
			{
				GlobalFunctions.FillCombo(cboSourceStore, dtStores, "StoreID", "StoreName");
				GlobalFunctions.FillCombo(cboDestinationStore, dtStores, "StoreID", "StoreName");
			}
			if (num > 0)
			{
				((TextEditorControlBase)cboSourceStore).Value = num;
			}
			if (num2 > 0)
			{
				((TextEditorControlBase)cboDestinationStore).Value = num2;
			}
			((TextEditorControlBase)cboSourceStore).ValueChanged += cboSourceStore_ValueChanged;
			((TextEditorControlBase)cboDestinationStore).ValueChanged += cboDestinationStore_ValueChanged;
		}
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		if (cboRequestItem.SelectedIndex > -1)
		{
			num3 = int.Parse(((TextEditorControlBase)cboRequestItem).Value.ToString());
		}
		if (cboItemCataloge.SelectedIndex > -1)
		{
			num4 = int.Parse(((TextEditorControlBase)cboItemCataloge).Value.ToString());
		}
		if (cboProductionRequest.SelectedIndex > -1)
		{
			num5 = int.Parse(((TextEditorControlBase)cboProductionRequest).Value.ToString());
		}
		if (Updating)
		{
			if (rbIsProductionRequest.Checked)
			{
				if (cboProductionRequest.SelectedIndex > -1)
				{
					dtRequestItemWithQty = Items.FillComboByProductionRequestID(drMaster["ProductionID"].ToString(), ((TextEditorControlBase)cboProductionRequest).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
					GlobalFunctions.FillCombo(cboRequestItem, dtRequestItemWithQty, "ItemID", "Name");
					((TextEditorControlBase)cboRequestItem).ValueChanged -= cboRequestItem_ValueChanged;
					((TextEditorControlBase)cboRequestItem).Value = num3;
					((TextEditorControlBase)cboRequestItem).ValueChanged += cboRequestItem_ValueChanged;
				}
				if (cboRequestItem.SelectedIndex > -1)
				{
					dtRequestCataloges = ItemsCataloge.FillComboByOutputItemID("," + ((TextEditorControlBase)cboRequestItem).Value.ToString() + ",", Adding ? "1" : "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
					GlobalFunctions.FillCombo(cboItemCataloge, dtRequestCataloges, "ItemCatalogeID", "ItemCatalogeName");
				}
			}
		}
		else if (Adding)
		{
			DataView dataView2 = new DataView(dtProductionRequest);
			dataView2.RowFilter = " IsDelivered =0  And BranchID=" + GlobalVariables.CurrentBranchID;
			DataTable dt = dataView2.ToTable();
			GlobalFunctions.FillCombo(cboProductionRequest, dt, "ProductionRequestID", "ProductionRequestNo");
			DataView dataView3 = new DataView(dtItemCataloge);
			dataView3.RowFilter = "IsActive=1";
			GlobalFunctions.FillCombo(cboItemCataloge, dataView3.ToTable(), "ItemCatalogeID", "ItemCatalogeName");
		}
		else
		{
			GlobalFunctions.FillCombo(cboProductionRequest, dtProductionRequest, "ProductionRequestID", "ProductionRequestNo");
			GlobalFunctions.FillCombo(cboRequestItem, dtItems, "ItemID", "Name");
			GlobalFunctions.FillCombo(cboItemCataloge, dtItemCataloge, "ItemCatalogeID", "ItemCatalogeName");
		}
		((TextEditorControlBase)cboRequestItem).ValueChanged -= cboRequestItem_ValueChanged;
		((TextEditorControlBase)cboItemCataloge).ValueChanged -= cboItemCataloge_ValueChanged;
		((TextEditorControlBase)cboProductionRequest).ValueChanged -= cboProductionRequest_ValueChanged;
		((TextEditorControlBase)cboRequestItem).Value = num3;
		((TextEditorControlBase)cboItemCataloge).Value = num4;
		((TextEditorControlBase)cboProductionRequest).Value = num5;
		((TextEditorControlBase)cboRequestItem).ValueChanged += cboRequestItem_ValueChanged;
		((TextEditorControlBase)cboItemCataloge).ValueChanged += cboItemCataloge_ValueChanged;
		((TextEditorControlBase)cboProductionRequest).ValueChanged += cboProductionRequest_ValueChanged;
		if (Updating)
		{
			SetBatchsAndUnitsColorsAndSizesForItems();
		}
	}

	public override void ClearControls()
	{
		base.ClearControls();
		UltraDateTimeEditor obj = dtpDate;
		UltraDateTimeEditor obj2 = dtpProductionStartDate;
		DateTime dateTime = (dtpProductionEndDate.DateTime = GlobalFunctions.GetServerDateTimeNow());
		DateTime dateTime2 = (obj2.DateTime = dateTime);
		obj.DateTime = dateTime2;
		((Control)(object)txtCode).Text = (Adding ? Productions.GetCodeByBranchID((cboLine.SelectedIndex == -1) ? "0" : ((TextEditorControlBase)cboLine).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((Control)(object)txtBatchNo).Text = (Adding ? Productions.BatchNoGetCode() : "");
		rbIsDirect.Checked = true;
		rbIsProductionRequest.Checked = false;
		((TextEditorControlBase)cboProductionRequest).ValueChanged -= cboProductionRequest_ValueChanged;
		cboProductionRequest.SelectedIndex = -1;
		((TextEditorControlBase)cboProductionRequest).ValueChanged += cboProductionRequest_ValueChanged;
		((TextEditorControlBase)cboRequestItem).ValueChanged -= cboRequestItem_ValueChanged;
		cboRequestItem.SelectedIndex = -1;
		((TextEditorControlBase)cboRequestItem).ValueChanged += cboRequestItem_ValueChanged;
		((TextEditorControlBase)txtRequestQty).Clear();
		cboUnitName.SelectedIndex = -1;
		((TextEditorControlBase)cboItemCataloge).ValueChanged -= cboItemCataloge_ValueChanged;
		cboItemCataloge.SelectedIndex = -1;
		((TextEditorControlBase)cboItemCataloge).ValueChanged += cboItemCataloge_ValueChanged;
		cboLine.SelectedIndex = -1;
		((TextEditorControlBase)txtQty).ValueChanged -= txtQty_ValueChanged;
		((TextEditorControlBase)txtQty).Clear();
		((TextEditorControlBase)txtQty).ValueChanged += txtQty_ValueChanged;
		QtyBeforeUpdate = 1m;
		AnyStageApproved = false;
		((TextEditorControlBase)cboSourceStore).ValueChanged -= cboSourceStore_ValueChanged;
		((TextEditorControlBase)cboDestinationStore).ValueChanged -= cboDestinationStore_ValueChanged;
		cboSourceStore.SelectedIndex = -1;
		cboDestinationStore.SelectedIndex = -1;
		((TextEditorControlBase)cboSourceStore).ValueChanged += cboSourceStore_ValueChanged;
		((TextEditorControlBase)cboDestinationStore).ValueChanged += cboDestinationStore_ValueChanged;
		((TextEditorControlBase)txtNotes).Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[4].Rows.Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[3].Rows.Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[2].Rows.Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[1].Rows.Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[0].Rows.Clear();
	}

	public override bool ValidateData()
	{
		if (dtpDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ الأذن" : "Please Enter The Voucher Date");
			((Control)(object)dtpDate).Focus();
			dtpDate.DropDown();
			return false;
		}
		if (!FiscalYear.ChkForConfirmedFiscalYear(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), IsFromServer: false))
		{
			GlobalVariables.InformationMB.Show("السنة المالية غير معتمدة", "The Fiscal Year is not confirmed..");
			return false;
		}
		if (FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false))
		{
			GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ يقع في فتره ماليه مغلقة", "The Date you choosed\n\r exists in closed fisical period");
			return false;
		}
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم  الأذن" : "Please Enter The Voucher Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtBatchNo).Text == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم التشغيلة" : "Please Enter The Batch No");
			((TextEditorControlBase)txtBatchNo).Focus();
			return false;
		}
		if (dtpProductionStartDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ بداية الانتاج" : "Please Enter Production Start Date");
			((Control)(object)dtpProductionStartDate).Focus();
			dtpProductionStartDate.DropDown();
			return false;
		}
		if (dtpProductionEndDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ نهاية الانتاج" : "Please Enter Production End Date");
			((Control)(object)dtpProductionEndDate).Focus();
			dtpProductionEndDate.DropDown();
			return false;
		}
		if (rbIsProductionRequest.Checked)
		{
			if (cboProductionRequest.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار رقم الطلبية" : "Please Select Request No");
				((TextEditorControlBase)cboProductionRequest).Focus();
				cboProductionRequest.DropDown();
				return false;
			}
			if (cboRequestItem.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الصنف" : "Please Select  Item");
				((TextEditorControlBase)cboRequestItem).Focus();
				cboRequestItem.DropDown();
				return false;
			}
		}
		if (cboLine.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار خط الانتاج" : "Please Select Production line");
			((TextEditorControlBase)cboLine).Focus();
			cboLine.DropDown();
			return false;
		}
		if (cboItemCataloge.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار وصفة الانتاج" : "Please Select Production Recepie");
			((TextEditorControlBase)cboItemCataloge).Focus();
			cboItemCataloge.DropDown();
			return false;
		}
		if (((Control)(object)txtQty).Text == "" || decimal.Parse(((Control)(object)txtQty).Text) <= 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال الكمية" : "Please Enter Quantity");
			((TextEditorControlBase)txtQty).Focus();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("Pro_Productions", "ProductionNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["ProductionNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = Productions.GetCodeByBranchID((cboLine.SelectedIndex == -1) ? "0" : ((TextEditorControlBase)cboLine).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Voucher Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByBranchID;
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
			if (((UltraGridBase)ULGData).Rows[i].Cells["UnitFees"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إدخال سعر الوحدة  ", "Please Enter Unit Price ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitFees"];
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
				if (UsingColors && (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ColorID"].Value == DBNull.Value || dtColors.Select("ColorID=" + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ColorID"].Value.ToString()).Length == 0))
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء إختيار اللون  ", "Please Select Color");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ColorID"];
					((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ColorID"].DroppedDown = true;
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (UsingSizes && (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemSizeID"].Value == DBNull.Value || dtSizes.Select("ItemSizeID=" + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemSizeID"].Value.ToString()).Length == 0))
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء إختيار المقاس  ", "Please Select Size");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemSizeID"];
					((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemSizeID"].DroppedDown = true;
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ActualQty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ActualQty"].Value.ToString()) <= 0m)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ActualQty"];
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
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["StoreID"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء إختيار المخزن  ", "Please Select Store Name");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["StoreID"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (UsingBatchNoAndValidityPeriod)
				{
					if (bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) && dtProductionStagesOutputs.Select(" ItemID = " + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value.ToString()).Length != 0 && ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BatchID"].Value == DBNull.Value && dtBatchs.Select(" ItemID is null and BatchName = '" + ((Control)(object)txtBatchNo).Text + "'").Length != 0)
					{
						((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BatchID"].Value = dtBatchs.Select(" ItemID is null and BatchName = '" + ((Control)(object)txtBatchNo).Text + "'")[0]["BatchID"];
					}
					if (bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) && ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BatchID"].Value == DBNull.Value)
					{
						((Control)(object)ULGData).Enter -= ULGData_Enter;
						GlobalVariables.InformationMB.Show("برجاء اختيار سريل", "Please choose Batch No");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BatchID"];
						ULGData.PerformAction((UltraGridAction)24);
						((Control)(object)ULGData).Enter += ULGData_Enter;
						return false;
					}
				}
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; k++)
				{
					if (k != j && ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["ItemID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BatchID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["BatchID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["StoreID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["StoreID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ColorID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["ColorID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemSizeID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["ItemSizeID"].Value.ToString())
					{
						GlobalVariables.InformationMB.Show(" لا يمكن تكرار الصنف فى نفس المرحلة مع ", "Cannot Duplicate The Same Item With the Same Stage ");
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
				if (UsingColors && (((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["ColorID"].Value == DBNull.Value || dtColors.Select("ColorID=" + ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["ColorID"].Value.ToString()).Length == 0))
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء إختيار اللون  ", "Please Select Color");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["ColorID"];
					((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["ColorID"].DroppedDown = true;
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (UsingSizes && (((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["ItemSizeID"].Value == DBNull.Value || dtSizes.Select("ItemSizeID=" + ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["ItemSizeID"].Value.ToString()).Length == 0))
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء إختيار المقاس  ", "Please Select Size");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["ItemSizeID"];
					((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["ItemSizeID"].DroppedDown = true;
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["ActualQty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["ActualQty"].Value.ToString()) <= 0m)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["ActualQty"];
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
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["StoreID"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء إختيار إسم المخزن  ", "Please Select Store Name");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["StoreID"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (UsingBatchNoAndValidityPeriod)
				{
					if (bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) && ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["BatchID"].Value == DBNull.Value && dtBatchs.Select(" ItemID is null and  BatchName = '" + ((Control)(object)txtBatchNo).Text + "'").Length != 0)
					{
						((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["BatchID"].Value = dtBatchs.Select(" ItemID is null and BatchName = '" + ((Control)(object)txtBatchNo).Text + "'")[0]["BatchID"];
					}
					if (bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) && ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["BatchID"].Value == DBNull.Value)
					{
						((Control)(object)ULGData).Enter -= ULGData_Enter;
						GlobalVariables.InformationMB.Show("برجاء اختيار سريل", "Please choose Batch No");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["BatchID"];
						ULGData.PerformAction((UltraGridAction)24);
						((Control)(object)ULGData).Enter += ULGData_Enter;
						return false;
					}
				}
				for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows).Count; m++)
				{
					if (m != l && ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["ItemID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[m].Cells["ItemID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["BatchID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[m].Cells["BatchID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["StoreID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[m].Cells["StoreID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["ColorID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[m].Cells["ColorID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].Cells["ItemSizeID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[m].Cells["ItemSizeID"].Value.ToString())
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
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].ChildBands[0].Rows[n].Cells["ActualValue"].Value == DBNull.Value)
					{
						((Control)(object)ULGData).Enter -= ULGData_Enter;
						GlobalVariables.InformationMB.Show("برجاء إدخال القيمة  ", "Please Insert Value");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[l].ChildBands[0].Rows[n].Cells["ActualValue"];
						ULGData.PerformAction((UltraGridAction)24);
						((Control)(object)ULGData).Enter += ULGData_Enter;
						return false;
					}
				}
			}
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='ProductionDiffAccount' ")[0]["AccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب فروق الانتاج من حسابات النظام  ", "Please Select Production Diff Account From SystemAccounts ");
			return false;
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='ProductionFeesAccount' ")[0]["AccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب اجور الانتاج من حسابات النظام  ", "Please Select Production Fees Account From SystemAccounts ");
			return false;
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = Productions.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), rbIsDirect.Checked ? "1" : "0", rbIsProductionRequest.Checked ? "1" : "0", ((Control)(object)txtBatchNo).Text, dtpProductionStartDate.DateTime.ToString(GlobalVariables.DateLongFormate), dtpProductionEndDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboProductionRequest.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboProductionRequest).Value.ToString(), (cboRequestItem.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboRequestItem).Value.ToString(), (((Control)(object)txtRequestQty).Text == "") ? "0" : ((Control)(object)txtRequestQty).Text, (cboUnitName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUnitName).Value.ToString(), (cboItemCataloge.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboItemCataloge).Value.ToString(), (cboLine.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLine).Value.ToString(), ((Control)(object)txtQty).Text, (cboSourceStore.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSourceStore).Value.ToString(), (cboDestinationStore.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDestinationStore).Value.ToString(), ((Control)(object)txtNotes).Text, "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				int num2 = ProductionsStages.Insert_Update("-1", num.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["StageID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["UnitFees"].Value.ToString(), "0", "0", "Null", "0", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
				{
					ProductionsStagesInputs.Insert_Update("-1", num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BatchID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BatchID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["EstimatedQty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["EstimatedQty"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ActualQty"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["StoreID"].Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				}
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows).Count; k++)
				{
					int num3 = ProductionsStagesOutputs.Insert_Update("-1", num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["BatchID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["BatchID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["EstimatedQty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["EstimatedQty"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["ActualQty"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["CostAllocationRate"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["UnitID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["StoreID"].Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].ChildBands[0].Rows).Count; l++)
					{
						ProductionsStagesOutputsExpenses.Insert_Update("-1", num3.ToString(), num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].ChildBands[0].Rows[l].Cells["ExpenseID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].ChildBands[0].Rows[l].Cells["EstimatedValue"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].ChildBands[0].Rows[l].Cells["ActualValue"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					}
				}
			}
			Main.EndBulkTrans(FromServer: false);
			RowID = num.ToString();
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
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = Productions.Insert_Update(drMaster["ProductionID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), rbIsDirect.Checked ? "1" : "0", rbIsProductionRequest.Checked ? "1" : "0", ((Control)(object)txtBatchNo).Text, dtpProductionStartDate.DateTime.ToString(GlobalVariables.DateLongFormate), dtpProductionEndDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboProductionRequest.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboProductionRequest).Value.ToString(), (cboRequestItem.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboRequestItem).Value.ToString(), (((Control)(object)txtRequestQty).Text == "") ? "0" : ((Control)(object)txtRequestQty).Text, (cboUnitName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUnitName).Value.ToString(), (cboItemCataloge.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboItemCataloge).Value.ToString(), (cboLine.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLine).Value.ToString(), ((Control)(object)txtQty).Text, (cboSourceStore.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSourceStore).Value.ToString(), (cboDestinationStore.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDestinationStore).Value.ToString(), ((Control)(object)txtNotes).Text, bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ProductionsStagesInputs.DeleteByProductionID(num.ToString(), GlobalVariables.UserID);
			ProductionsStagesEmployees.DeleteByProductionID(num.ToString(), GlobalVariables.UserID);
			ProductionsStagesOutputsExpenses.DeleteByProductionID(num.ToString(), GlobalVariables.UserID);
			ProductionsStagesOutputs.DeleteByProductionID(num.ToString(), GlobalVariables.UserID);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["ProductionStageID"].Value.ToString() + ",";
			}
			Main.DeleteForUpdate("Pro_ProductionsStages", "ProductionID", drMaster["ProductionID"].ToString(), "ProductionStageID", text);
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				int num2 = ProductionsStages.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ProductionStageID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ProductionStageID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[j].Cells["ProductionStageID"].Value.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["StageID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["UnitFees"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["CompletePercentage"].Value.ToString(), bool.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Approved"].Value.ToString()) ? "1" : "0", (((UltraGridBase)ULGData).Rows[j].Cells["ApprovedDate"].Value == DBNull.Value) ? "Null" : DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ApprovedDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate), "1", "1", bool.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Deleted"].Value.ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows).Count; k++)
				{
					ProductionsStagesInputs.Insert_Update("-1", num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["BatchID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["BatchID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["EstimatedQty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["EstimatedQty"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ActualQty"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["UnitID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["StoreID"].Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				}
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows).Count; l++)
				{
					int num3 = ProductionsStagesOutputs.Insert_Update("-1", num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].Cells["BatchID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].Cells["BatchID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].Cells["EstimatedQty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].Cells["EstimatedQty"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].Cells["ActualQty"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].Cells["CostAllocationRate"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].Cells["UnitID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].Cells["StoreID"].Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].ChildBands[0].Rows).Count; m++)
					{
						ProductionsStagesOutputsExpenses.Insert_Update("-1", num3.ToString(), num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].ChildBands[0].Rows[m].Cells["ExpenseID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].ChildBands[0].Rows[m].Cells["EstimatedValue"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[1].Rows[l].ChildBands[0].Rows[m].Cells["ActualValue"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					}
				}
			}
			ItemsTransactions.ManagementInsertUpdateDelete();
			ItemsTransactions.RecalculateCurrentQtyOnly();
			string text2 = Productions.AllowedQty_Message(num.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			if (text2 != "")
			{
				GlobalVariables.InformationMB.Show(text2);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
			}
			else
			{
				Main.EndBulkTrans(FromServer: false);
				ItemsTransactions.ManageInThread();
			}
		}
		catch (Exception)
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnDeleteClick()
	{
		if (drMaster == null)
		{
			return;
		}
		RowID = drMaster[IDCol].ToString();
		if (!CanDelete)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		if (!CanModifyOtherBranch)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفرع آخر", "Cannot Delete This Transaction Because It Related to Another Branch ");
			return;
		}
		if (ClosedPeriod)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Delete This Transaction Because It Related to ClosedPeriod ");
			return;
		}
		if (ProductionsFeesDetailsEmployees.SelectByProductionID(drMaster["ProductionID"].ToString(), GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء حذف الموظفين من اجور الانتاج  " : "PLease Delete Employees From Production Fees");
			return;
		}
		if (int.Parse(Main.ExecuteQuery_DataTable(" Select Count(*) AS Counter From Pro_ProductionsStages Where Deleted=0 AND CompletePercentage >0 AND ProductionID = " + drMaster["ProductionID"].ToString()).Rows[0]["Counter"].ToString()) > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لانه تمت إعتماد مراحل منها", "Cannot Delete This Transaction Because There Are Production Stages Approved ");
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Are You Sure You want to Delete this Data?");
		if (GlobalVariables.MessageBoxResult == 'Y')
		{
			DataSaved = true;
			DeleteData();
			if (DataSaved)
			{
				FillData();
				drMaster = null;
			}
		}
	}

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			ProductionsStagesOutputsExpenses.DeleteVirtualByProductionID(drMaster["ProductionID"].ToString(), GlobalVariables.UserID);
			ProductionsStagesOutputs.DeleteVirtualByProductionID(drMaster["ProductionID"].ToString(), GlobalVariables.UserID);
			ProductionsStagesInputs.DeleteVirtualByProductionID(drMaster["ProductionID"].ToString(), GlobalVariables.UserID);
			ProductionsStages.DeleteVirtualByProductionID(drMaster["ProductionID"].ToString(), GlobalVariables.UserID);
			ProductionsStagesApprove.DeleteVirtualByProductionID(drMaster["ProductionID"].ToString(), GlobalVariables.UserID);
			Productions.DeleteVirtual(drMaster["ProductionID"].ToString(), GlobalVariables.UserID);
			DataTable dataTable = ProductionsStagesApprove.SelectByProductionID(drMaster["ProductionID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				if (dataTable.Rows[i]["StockControlJVID"] != DBNull.Value)
				{
					JV.DeleteVirtual(dataTable.Rows[i]["StockControlJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
				}
			}
			ItemsTransactions.ManagementInsertUpdateDelete();
			ItemsTransactions.RecalculateCurrentQtyOnly();
			string text = Productions.AllowedQty_Message(drMaster["ProductionID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			if (text != "")
			{
				GlobalVariables.InformationMB.Show(text);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
			}
			else
			{
				Main.EndBulkTrans(FromServer: false);
				ItemsTransactions.ManageInThread();
			}
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnPrintClick()
	{
		string val = "";
		if (RowID != "")
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Pro_Productions_A.rpt" : "Rep_Pro_Productions_E.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@ProductionIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			GlobalVariables.ReportDocument.SetParameterValue("@ProductionIDs", "," + RowID + ",", "Rep_Pro_ProductionsInput");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0", "Rep_Pro_ProductionsInput");
			GlobalVariables.ReportDocument.SetParameterValue("@ProductionIDs", "," + RowID + ",", "Rep_Pro_ProductionsEmployees");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0", "Rep_Pro_ProductionsEmployees");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ProductionsReport(-1, 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["ProductionID"].ToString();
			FillData();
		}
	}

	private void btnItemCatalogSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.ItemsCataloge(IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboItemCataloge).Value = num;
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
		dtProductionRequest = ProductionRequests.FillCombo(GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
		GlobalFunctions.FillCombo(cboProductionRequest, dtProductionRequest, "ProductionRequestID", "ProductionRequestNo");
		dtLines = BusinessLayer.Production.Lines.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboLine, dtLines, "LineID", "LineName");
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "1", "0", "1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItemsInput.ValueListItems.Clear();
		vlItemsOutput.ValueListItems.Clear();
		for (int k = 0; k < dtItems.Rows.Count; k++)
		{
			vlItemsInput.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["Name"].ToString());
			vlItemsOutput.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["Name"].ToString());
		}
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchsInput.ValueListItems.Clear();
			vlBatchsOutput.ValueListItems.Clear();
			for (int l = 0; l < dtBatchs.Rows.Count; l++)
			{
				vlBatchsInput.ValueListItems.Add(dtBatchs.Rows[l]["BatchID"], dtBatchs.Rows[l]["BatchName"].ToString());
				vlBatchsOutput.ValueListItems.Add(dtBatchs.Rows[l]["BatchID"], dtBatchs.Rows[l]["BatchName"].ToString());
			}
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboUnitName, dtUnits, "UnitID", "UnitName");
		vlUnitInput.ValueListItems.Clear();
		vlUnitOutput.ValueListItems.Clear();
		for (int m = 0; m < dtUnits.Rows.Count; m++)
		{
			vlUnitInput.ValueListItems.Add(dtUnits.Rows[m]["UnitID"], dtUnits.Rows[m]["UnitName"].ToString());
			vlUnitOutput.ValueListItems.Add(dtUnits.Rows[m]["UnitID"], dtUnits.Rows[m]["UnitName"].ToString());
		}
		dtStages = Stages.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlStages.ValueListItems.Clear();
		for (int n = 0; n < dtStages.Rows.Count; n++)
		{
			vlStages.ValueListItems.Add(dtStages.Rows[n]["StageID"], dtStages.Rows[n]["StageName"].ToString());
		}
		dtExpenses = Expenses.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlExpenses.ValueListItems.Clear();
		for (int num = 0; num < dtExpenses.Rows.Count; num++)
		{
			vlExpenses.ValueListItems.Add(dtExpenses.Rows[num]["ExpenseID"], dtExpenses.Rows[num]["ExpenseName"].ToString());
		}
		dtEmployees = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlEmployees.ValueListItems.Clear();
		for (int num2 = 0; num2 < dtEmployees.Rows.Count; num2++)
		{
			vlEmployees.ValueListItems.Add(dtEmployees.Rows[num2]["SubAccountID"], dtEmployees.Rows[num2]["SubAccountName"].ToString());
		}
		if (cboProductionRequest.SelectedIndex > -1)
		{
			dtRequestItemWithQty = Items.FillComboByProductionRequestID(Adding ? "-1" : drMaster["ProductionID"].ToString(), ((TextEditorControlBase)cboProductionRequest).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboRequestItem, dtRequestItemWithQty, "ItemID", "Name");
		}
		else
		{
			GlobalFunctions.FillCombo(cboRequestItem, dtItems, "ItemID", "Name");
		}
		if (cboRequestItem.SelectedIndex > -1)
		{
			dtRequestCataloges = ItemsCataloge.FillComboByOutputItemID("," + ((TextEditorControlBase)cboRequestItem).Value.ToString() + ",", Adding ? "1" : "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboItemCataloge, dtRequestCataloges, "ItemCatalogeID", "ItemCatalogeName");
		}
		else
		{
			dtItemCataloge = ItemsCataloge.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboItemCataloge, dtItemCataloge, "ItemCatalogeID", "ItemCatalogeName");
		}
		SetBatchsAndUnitsColorsAndSizesForItems();
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		bool flag = false;
		for (int i = 0; i < e.Rows.Length; i++)
		{
			if (((GridItemBase)e.Rows[i]).Band.Index == 0)
			{
				if (bool.Parse(e.Rows[i].Cells["Approved"].Value.ToString()))
				{
					flag = true;
				}
			}
			else if (((GridItemBase)e.Rows[i]).Band.Index == 1)
			{
				if (bool.Parse(e.Rows[i].ParentRow.Cells["Approved"].Value.ToString()))
				{
					flag = true;
				}
			}
			else if (((GridItemBase)e.Rows[i]).Band.Index == 2)
			{
				if (bool.Parse(e.Rows[i].ParentRow.Cells["Approved"].Value.ToString()))
				{
					flag = true;
				}
			}
			else if (((GridItemBase)e.Rows[i]).Band.Index == 3)
			{
				if (bool.Parse(e.Rows[i].ParentRow.ParentRow.Cells["Approved"].Value.ToString()))
				{
					flag = true;
				}
			}
			else if (((GridItemBase)e.Rows[i]).Band.Index == 4 && bool.Parse(e.Rows[i].ParentRow.Cells["Approved"].Value.ToString()))
			{
				flag = true;
			}
		}
		if (flag)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن حذف هذا الصنف لانه تم إعتماد المرحلة" : "Cannot Delete This Item Because Stage Approved");
			e.DisplayPromptMsg = false;
			((CancelEventArgs)(object)e).Cancel = true;
		}
		else
		{
			base.ULGData_BeforeRowsDeleted(sender, e);
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitFees" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ActualQty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CostAllocationRate" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ActualValue"))
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		if (((GridItemBase)e.Row).Band.Index == 0)
		{
			e.Row.Cells["ProductionStageID"].Value = ++newID;
		}
		else if (((GridItemBase)e.Row).Band.Index == 1)
		{
			e.Row.Cells["ProductionStageInputID"].Value = ++newID;
		}
		else if (((GridItemBase)e.Row).Band.Index == 2)
		{
			e.Row.Cells["ProductionStageOutputID"].Value = ++newID;
		}
		else if (((GridItemBase)e.Row).Band.Index == 3)
		{
			e.Row.Cells["ProductionStageOutputExpenseID"].Value = ++newID;
		}
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
		if (e.KeyCode == Keys.F6)
		{
			if (Adding || Updating)
			{
				if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
				{
					frmAddItemsSerial frmAddItemsSerial2 = new frmAddItemsSerial(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
					frmAddItemsSerial2.WindowState = FormWindowState.Normal;
					frmAddItemsSerial2.ShowDialog();
					dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
					vlBatchsInput.ValueListItems.Clear();
					vlBatchsOutput.ValueListItems.Clear();
					for (int i = 0; i < dtBatchs.Rows.Count; i++)
					{
						vlBatchsInput.ValueListItems.Add(dtBatchs.Rows[i]["BatchID"], dtBatchs.Rows[i]["BatchName"].ToString());
						vlBatchsOutput.ValueListItems.Add(dtBatchs.Rows[i]["BatchID"], dtBatchs.Rows[i]["BatchName"].ToString());
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = frmAddItemsSerial2.BatchID;
				}
				else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ActualQty" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value)
				{
					frmQuantityMultiUnit frmQuantityMultiUnit2 = new frmQuantityMultiUnit(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString()), int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()), isreadonlyunitcolumn: true);
					frmQuantityMultiUnit2.WindowState = FormWindowState.Normal;
					frmQuantityMultiUnit2.ShowDialog();
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = ((frmQuantityMultiUnit2.UnitID > 0) ? ((object)frmQuantityMultiUnit2.UnitID) : ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value);
					((UltraGridBase)ULGData).ActiveRow.Cells["ActualQty"].Value = ((frmQuantityMultiUnit2.Qty > 0m) ? ((object)frmQuantityMultiUnit2.Qty) : ((UltraGridBase)ULGData).ActiveRow.Cells["ActualQty"].Value);
				}
			}
			e.Handled = true;
		}
		else if (e.KeyCode == Keys.F9)
		{
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemSizeID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ColorID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID") && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value)
			{
				DataTable dataTable = Items.Select(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
				frmImageViewer frmImageViewer2 = new frmImageViewer((dataTable.Rows[0]["ItemPic"] == DBNull.Value) ? null : ImageFunctions.BinaryToImage((byte[])dataTable.Rows[0]["ItemPic"]), GlobalVariables.IsArabic ? dataTable.Rows[0]["ItemNameAr"].ToString() : dataTable.Rows[0]["ItemNameEn"].ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value == DBNull.Value) ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate));
				frmImageViewer2.WindowState = FormWindowState.Normal;
				frmImageViewer2.ShowDialog();
			}
			e.Handled = true;
		}
	}

	public void SetBatchsAndUnitsColorsAndSizesForItems()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value == DBNull.Value)
				{
					continue;
				}
				DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value.ToString())[0];
				if (UsingBatchNoAndValidityPeriod)
				{
					ValueList batchsValueList = getBatchsValueList(int.Parse(dataRow["ItemID"].ToString()));
					((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
					if (((DisposableObjectCollectionBase)batchsValueList.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BatchID"].Value = DBNull.Value;
					}
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
				if (UsingBatchNoAndValidityPeriod)
				{
					ValueList batchsValueList2 = getBatchsValueList(int.Parse(dataRow2["ItemID"].ToString()));
					((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList2;
					if (((DisposableObjectCollectionBase)batchsValueList2.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["BatchID"].Value = DBNull.Value;
					}
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
				((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[j].Cells["StoreID"].Value = ((TextEditorControlBase)cboDestinationStore).Value;
			}
		}
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		((UltraGridBase)ULGData).UpdateData();
		if ((((GridItemBase)e.Cell).Band.Index == 1 || ((GridItemBase)e.Cell).Band.Index == 2) && ((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0 && e.Cell.Value != DBNull.Value)
		{
			DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((((GridItemBase)e.Cell).Band.Index != 1) ? ((cboDestinationStore.SelectedIndex > -1) ? ((TextEditorControlBase)cboDestinationStore).Value : DBNull.Value) : ((cboSourceStore.SelectedIndex > -1) ? ((TextEditorControlBase)cboSourceStore).Value : DBNull.Value));
			if (UsingBatchNoAndValidityPeriod)
			{
				int num = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtItems.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"].ToString()) : 0);
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
			if (Updating && bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Approved"].Value.ToString()))
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			else if (Updating && ProductionsFeesDetailsEmployees.SelectByProductionID(drMaster["ProductionID"].ToString(), GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			else if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CompletePercentage")
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
		}
		else if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1)
		{
			if (Updating && bool.Parse(((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["Approved"].Value.ToString()))
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			else if (Convert.ToDecimal(((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["CompletePercentage"].Value) > 0m && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "ActualQty")
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			else if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "EstimatedQty")
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			else if (ULGData.ActiveCell != null && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			else if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID")
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
		}
		else if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 2)
		{
			if (Updating && bool.Parse(((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["Approved"].Value.ToString()))
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			else if (Convert.ToDecimal(((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["CompletePercentage"].Value) > 0m && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "ActualQty")
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			else if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "EstimatedQty")
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			else if (ULGData.ActiveCell != null && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			else if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID")
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
		}
		else if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 3)
		{
			if (Updating && bool.Parse(((UltraGridBase)ULGData).ActiveRow.ParentRow.ParentRow.Cells["Approved"].Value.ToString()))
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			else if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "EstimatedValue")
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
		}
		else if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 4)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void cboItemCataloge_ValueChanged(object sender, EventArgs e)
	{
		if (cboItemCataloge.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboSourceStore).ValueChanged -= cboSourceStore_ValueChanged;
			((TextEditorControlBase)cboSourceStore).Value = dtItemCataloge.Select(" ItemCatalogeID= " + ((TextEditorControlBase)cboItemCataloge).Value.ToString())[0]["InputStoreID"];
			((TextEditorControlBase)cboSourceStore).ValueChanged += cboSourceStore_ValueChanged;
			((TextEditorControlBase)cboDestinationStore).ValueChanged -= cboDestinationStore_ValueChanged;
			((TextEditorControlBase)cboDestinationStore).Value = dtItemCataloge.Select(" ItemCatalogeID= " + ((TextEditorControlBase)cboItemCataloge).Value.ToString())[0]["OutPutStoreID"];
			((TextEditorControlBase)cboDestinationStore).ValueChanged += cboDestinationStore_ValueChanged;
			((TextEditorControlBase)txtQty).ValueChanged -= txtQty_ValueChanged;
			((Control)(object)txtQty).Text = "1";
			if (rbIsProductionRequest.Checked && cboRequestItem.SelectedIndex > -1)
			{
				((Control)(object)txtQty).Text = (decimal.Parse(((Control)(object)txtRequestQty).Text) / decimal.Parse(dtRequestCataloges.Select(" ItemCatalogeID= " + ((TextEditorControlBase)cboItemCataloge).Value.ToString())[0]["Qty"].ToString())).ToString();
			}
			((TextEditorControlBase)txtQty).ValueChanged += txtQty_ValueChanged;
			QtyBeforeUpdate = decimal.Parse(((Control)(object)txtQty).Text);
			dtDetails = ItemsCatalogeStages.SelectByItemCatalogeID_Production(((TextEditorControlBase)cboItemCataloge).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			dtProductionStagesInputs = ItemsCatalogeStagesInputs.SelectByItemCatalogeID_Production(((TextEditorControlBase)cboItemCataloge).Value.ToString(), (((Control)(object)txtQty).Text == "") ? "1" : ((Control)(object)txtQty).Text, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			dtProductionStagesOutputs = ItemsCatalogeStagesOutputs.SelectByItemCatalogeID_Production(((TextEditorControlBase)cboItemCataloge).Value.ToString(), (((Control)(object)txtQty).Text == "") ? "1" : ((Control)(object)txtQty).Text, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			dtProductionStagesOutputsExpenses = ItemsCatalogeStagesOutputsExpenses.SelectByItemCatalogeID_Production(((TextEditorControlBase)cboItemCataloge).Value.ToString(), (((Control)(object)txtQty).Text == "") ? "1" : ((Control)(object)txtQty).Text, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			dtProductionsFeesDetailsEmployees = ProductionsFeesDetailsEmployees.SelectByProductionID("0", GlobalVariables.IsArabic ? "1" : "0");
			dtOriginalInputItems = dtProductionStagesInputs.Copy();
			dtOriginalOutputItems = dtProductionStagesOutputs.Copy();
			dtOriginalOutputItemsExpenses = dtProductionStagesOutputsExpenses.Copy();
			ds = new DataSet();
			ds.Tables.Add(dtDetails);
			ds.Tables.Add(dtProductionStagesInputs);
			ds.Tables.Add(dtProductionStagesOutputs);
			ds.Tables.Add(dtProductionStagesOutputsExpenses);
			ds.Tables.Add(dtProductionsFeesDetailsEmployees);
			ds.Tables[0].TableName = "dtDetails";
			ds.Tables[1].TableName = "dtProductionStagesInputs";
			ds.Tables[2].TableName = "dtProductionStagesOutputs";
			ds.Tables[3].TableName = "dtProductionStagesOutputsExpenses";
			ds.Tables[4].TableName = "dtProductionsFeesDetailsEmployees";
			ds.Relations.Add(ds.Tables[0].Columns["ProductionStageID"], ds.Tables[1].Columns["ProductionStageID"]);
			ds.Relations.Add(ds.Tables[0].Columns["ProductionStageID"], ds.Tables[2].Columns["ProductionStageID"]);
			ds.Relations.Add(ds.Tables[0].Columns["ProductionStageID"], ds.Tables[4].Columns["ProductionStageID"]);
			ds.Relations.Add(ds.Tables[2].Columns["ProductionStageOutputID"], ds.Tables[3].Columns["ProductionStageOutputID"]);
			((UltraGridBase)ULGData).DataSource = ds;
			InitGrid();
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
			SetBatchsAndUnitsColorsAndSizesForItems();
		}
	}

	private void rbIsDirect_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)lblProductionRequest).Visible = !rbIsDirect.Checked;
		((Control)(object)cboProductionRequest).Visible = !rbIsDirect.Checked;
		((Control)(object)lblRequestItemID).Visible = !rbIsDirect.Checked;
		((Control)(object)cboRequestItem).Visible = !rbIsDirect.Checked;
		((Control)(object)lblRequestQty).Visible = !rbIsDirect.Checked;
		((Control)(object)txtRequestQty).Visible = !rbIsDirect.Checked;
		((Control)(object)cboUnitName).Visible = !rbIsDirect.Checked;
		((Control)(object)btnRequestNoSearch).Visible = !rbIsDirect.Checked;
		if ((Adding || Updating) && rbIsDirect.Checked)
		{
			((TextEditorControlBase)cboProductionRequest).ValueChanged -= cboProductionRequest_ValueChanged;
			cboProductionRequest.SelectedIndex = -1;
			((TextEditorControlBase)cboProductionRequest).ValueChanged += cboProductionRequest_ValueChanged;
			((TextEditorControlBase)cboRequestItem).ValueChanged -= cboRequestItem_ValueChanged;
			cboRequestItem.SelectedIndex = -1;
			((TextEditorControlBase)cboRequestItem).ValueChanged += cboRequestItem_ValueChanged;
			((Control)(object)txtRequestQty).Text = "0";
			cboUnitName.SelectedIndex = -1;
			GlobalFunctions.FillCombo(cboRequestItem, dtItems, "ItemID", "Name");
			DataView dataView = new DataView(dtItemCataloge);
			dataView.RowFilter = "IsActive=1";
			GlobalFunctions.FillCombo(cboItemCataloge, dataView.ToTable(), "ItemCatalogeID", "ItemCatalogeName");
			((DataSet)((UltraGridBase)ULGData).DataSource).Tables[4].Rows.Clear();
			((DataSet)((UltraGridBase)ULGData).DataSource).Tables[3].Rows.Clear();
			((DataSet)((UltraGridBase)ULGData).DataSource).Tables[2].Rows.Clear();
			((DataSet)((UltraGridBase)ULGData).DataSource).Tables[1].Rows.Clear();
			((DataSet)((UltraGridBase)ULGData).DataSource).Tables[0].Rows.Clear();
		}
	}

	private void cboProductionRequest_ValueChanged(object sender, EventArgs e)
	{
		if (cboProductionRequest.SelectedIndex > -1)
		{
			dtRequestItemWithQty = Items.FillComboByProductionRequestID(Adding ? "-1" : drMaster["ProductionID"].ToString(), ((TextEditorControlBase)cboProductionRequest).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboRequestItem, dtRequestItemWithQty, "ItemID", "Name");
		}
	}

	private void cboRequestItem_ValueChanged(object sender, EventArgs e)
	{
		if (cboRequestItem.SelectedIndex > -1 && cboProductionRequest.SelectedIndex > -1)
		{
			dtRequestCataloges = ItemsCataloge.FillComboByOutputItemID("," + ((TextEditorControlBase)cboRequestItem).Value.ToString() + ",", Adding ? "1" : "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboItemCataloge, dtRequestCataloges, "ItemCatalogeID", "ItemCatalogeName");
			((Control)(object)txtRequestQty).Text = ((decimal.Parse(dtRequestItemWithQty.Select(" ItemID= " + ((TextEditorControlBase)cboRequestItem).Value.ToString())[0]["Qty"].ToString()) < 0m) ? "0" : dtRequestItemWithQty.Select(" ItemID= " + ((TextEditorControlBase)cboRequestItem).Value.ToString())[0]["Qty"].ToString());
			((TextEditorControlBase)cboUnitName).Value = dtRequestItemWithQty.Select(" ItemID= " + ((TextEditorControlBase)cboRequestItem).Value.ToString())[0]["UnitID"];
			((DataSet)((UltraGridBase)ULGData).DataSource).Tables[4].Rows.Clear();
			((DataSet)((UltraGridBase)ULGData).DataSource).Tables[3].Rows.Clear();
			((DataSet)((UltraGridBase)ULGData).DataSource).Tables[2].Rows.Clear();
			((DataSet)((UltraGridBase)ULGData).DataSource).Tables[1].Rows.Clear();
			((DataSet)((UltraGridBase)ULGData).DataSource).Tables[0].Rows.Clear();
		}
	}

	private void txtQty_ValueChanged(object sender, EventArgs e)
	{
		if (!(((Control)(object)txtQty).Text != ".") || !(((Control)(object)txtQty).Text != ""))
		{
			return;
		}
		decimal num = decimal.Parse((((Control)(object)txtQty).Text == "") ? "1" : ((Control)(object)txtQty).Text) / QtyBeforeUpdate;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["EstimatedQty"].Value = decimal.Parse(dtOriginalInputItems.Select(" ProductionStageInputID = " + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ProductionStageInputID"].Value.ToString())[0]["EstimatedQty"].ToString()) * num;
				((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ActualQty"].Value = decimal.Parse(dtOriginalInputItems.Select(" ProductionStageInputID = " + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ProductionStageInputID"].Value.ToString())[0]["ActualQty"].ToString()) * num;
			}
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows).Count; k++)
			{
				((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["EstimatedQty"].Value = decimal.Parse(dtOriginalOutputItems.Select(" ProductionStageOutputID = " + ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["ProductionStageOutputID"].Value.ToString())[0]["EstimatedQty"].ToString()) * num;
				((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["ActualQty"].Value = decimal.Parse(dtOriginalOutputItems.Select(" ProductionStageOutputID = " + ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].Cells["ProductionStageOutputID"].Value.ToString())[0]["ActualQty"].ToString()) * num;
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].ChildBands[0].Rows).Count; l++)
				{
					((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].ChildBands[0].Rows[l].Cells["ActualValue"].Value = decimal.Parse(dtOriginalOutputItemsExpenses.Select(" ProductionStageOutputExpenseID = " + ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].ChildBands[0].Rows[l].Cells["ProductionStageOutputExpenseID"].Value.ToString())[0]["ActualValue"].ToString()) * num;
					((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].ChildBands[0].Rows[l].Cells["EstimatedValue"].Value = decimal.Parse(dtOriginalOutputItemsExpenses.Select(" ProductionStageOutputExpenseID = " + ((UltraGridBase)ULGData).Rows[i].ChildBands[1].Rows[k].ChildBands[0].Rows[l].Cells["ProductionStageOutputExpenseID"].Value.ToString())[0]["EstimatedValue"].ToString()) * num;
				}
			}
		}
	}

	private void btnRequestNoSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.ProductionRequests(GlobalVariables.BranchIDs, 1, 0);
		if (num != 0)
		{
			((TextEditorControlBase)cboProductionRequest).Value = num;
		}
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = Productions.GetCodeByBranchID((cboLine.SelectedIndex == -1) ? "0" : ((TextEditorControlBase)cboLine).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void btnPrintBarCode_Click(object sender, EventArgs e)
	{
		if (!(RowID == "") && dtDetails != null)
		{
			DataTable details = ProductionsStagesOutputs.SelectOutputItems_ByProductionID(RowID);
			frmGenerateBarCode frmGenerateBarCode2 = new frmGenerateBarCode(details);
			frmGenerateBarCode2.WindowState = FormWindowState.Normal;
			((Control)(object)frmGenerateBarCode2.lblTitle).Text = (GlobalVariables.IsArabic ? "طباعة باركود" : "BarCode");
			frmGenerateBarCode2.ShowDialog();
		}
	}

	private void cboLine_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = Productions.GetCodeByBranchID((cboLine.SelectedIndex == -1) ? "0" : ((TextEditorControlBase)cboLine).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected O, but got Unknown
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected O, but got Unknown
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected O, but got Unknown
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Expected O, but got Unknown
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected O, but got Unknown
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Expected O, but got Unknown
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Expected O, but got Unknown
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Expected O, but got Unknown
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Expected O, but got Unknown
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Expected O, but got Unknown
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Expected O, but got Unknown
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Expected O, but got Unknown
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Expected O, but got Unknown
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Expected O, but got Unknown
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Expected O, but got Unknown
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Expected O, but got Unknown
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Expected O, but got Unknown
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Expected O, but got Unknown
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Expected O, but got Unknown
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Expected O, but got Unknown
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Expected O, but got Unknown
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Expected O, but got Unknown
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Expected O, but got Unknown
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Expected O, but got Unknown
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Expected O, but got Unknown
		//IL_0621: Unknown result type (might be due to invalid IL or missing references)
		//IL_062b: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Production.Transactions.frmProductions));
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
		this.pnlCheckType = new UltraPanel();
		this.rbIsProductionRequest = new System.Windows.Forms.RadioButton();
		this.rbIsDirect = new System.Windows.Forms.RadioButton();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblBatchNo = new UltraLabel();
		this.txtBatchNo = new UltraTextEditor();
		this.lblRequestQty = new UltraLabel();
		this.txtRequestQty = new UltraTextEditor();
		this.lblSourceStore = new UltraLabel();
		this.cboSourceStore = new UltraComboEditor();
		this.lblDestinationStore = new UltraLabel();
		this.cboDestinationStore = new UltraComboEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.lblProductionStartDate = new UltraLabel();
		this.dtpProductionStartDate = new UltraDateTimeEditor();
		this.lblProductionEndDate = new UltraLabel();
		this.dtpProductionEndDate = new UltraDateTimeEditor();
		this.lblProductionRequest = new UltraLabel();
		this.cboProductionRequest = new UltraComboEditor();
		this.lblRequestItemID = new UltraLabel();
		this.cboRequestItem = new UltraComboEditor();
		this.lblLine = new UltraLabel();
		this.cboLine = new UltraComboEditor();
		this.lblItemCataloge = new UltraLabel();
		this.cboItemCataloge = new UltraComboEditor();
		this.lblQty = new UltraLabel();
		this.txtQty = new UltraTextEditor();
		this.cboUnitName = new UltraComboEditor();
		this.btnRequestNoSearch = new UltraButton();
		this.btnPrintBarCode = new UltraButton();
		this.btnItemCatalogSearch = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBatchNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRequestQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSourceStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDestinationStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpProductionStartDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpProductionEndDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboProductionRequest).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboRequestItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLine).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboItemCataloge).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnitName).BeginInit();
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
		resources.ApplyResources(this.pnlCheckType, "pnlCheckType");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val9, "appearance14");
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(this.pnlCheckType.ClientArea, "pnlCheckType.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsProductionRequest);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsDirect);
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
		resources.ApplyResources(this.rbIsProductionRequest, "rbIsProductionRequest");
		this.rbIsProductionRequest.BackColor = System.Drawing.Color.Transparent;
		this.rbIsProductionRequest.Name = "rbIsProductionRequest";
		this.rbIsProductionRequest.TabStop = true;
		this.rbIsProductionRequest.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.rbIsDirect, "rbIsDirect");
		this.rbIsDirect.BackColor = System.Drawing.Color.Transparent;
		this.rbIsDirect.Name = "rbIsDirect";
		this.rbIsDirect.TabStop = true;
		this.rbIsDirect.UseVisualStyleBackColor = false;
		this.rbIsDirect.CheckedChanged += new System.EventHandler(rbIsDirect_CheckedChanged);
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblBatchNo, "lblBatchNo");
		this.lblBatchNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBatchNo).Name = "lblBatchNo";
		((ControlBase)this.lblBatchNo).WrapText = false;
		resources.ApplyResources(this.txtBatchNo, "txtBatchNo");
		((System.Windows.Forms.Control)(object)this.txtBatchNo).Name = "txtBatchNo";
		resources.ApplyResources(this.lblRequestQty, "lblRequestQty");
		this.lblRequestQty.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRequestQty).Name = "lblRequestQty";
		((ControlBase)this.lblRequestQty).WrapText = false;
		resources.ApplyResources(this.txtRequestQty, "txtRequestQty");
		resources.ApplyResources(val10, "appearance9");
		((TextEditorControlBase)this.txtRequestQty).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.txtRequestQty).Name = "txtRequestQty";
		((System.Windows.Forms.Control)(object)this.txtRequestQty).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
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
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.lblProductionStartDate, "lblProductionStartDate");
		this.lblProductionStartDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblProductionStartDate).Name = "lblProductionStartDate";
		((ControlBase)this.lblProductionStartDate).WrapText = false;
		resources.ApplyResources(this.dtpProductionStartDate, "dtpProductionStartDate");
		((UltraWinEditorMaskedControlBase)this.dtpProductionStartDate).AlwaysInEditMode = true;
		this.dtpProductionStartDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpProductionStartDate).Name = "dtpProductionStartDate";
		resources.ApplyResources(this.lblProductionEndDate, "lblProductionEndDate");
		this.lblProductionEndDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblProductionEndDate).Name = "lblProductionEndDate";
		((ControlBase)this.lblProductionEndDate).WrapText = false;
		resources.ApplyResources(this.dtpProductionEndDate, "dtpProductionEndDate");
		((UltraWinEditorMaskedControlBase)this.dtpProductionEndDate).AlwaysInEditMode = true;
		this.dtpProductionEndDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpProductionEndDate).Name = "dtpProductionEndDate";
		resources.ApplyResources(this.lblProductionRequest, "lblProductionRequest");
		this.lblProductionRequest.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblProductionRequest).Name = "lblProductionRequest";
		((ControlBase)this.lblProductionRequest).WrapText = false;
		resources.ApplyResources(this.cboProductionRequest, "cboProductionRequest");
		((TextEditorControlBase)this.cboProductionRequest).AlwaysInEditMode = true;
		this.cboProductionRequest.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboProductionRequest).Name = "cboProductionRequest";
		((TextEditorControlBase)this.cboProductionRequest).ValueChanged += new System.EventHandler(cboProductionRequest_ValueChanged);
		resources.ApplyResources(this.lblRequestItemID, "lblRequestItemID");
		this.lblRequestItemID.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRequestItemID).Name = "lblRequestItemID";
		((ControlBase)this.lblRequestItemID).WrapText = false;
		resources.ApplyResources(this.cboRequestItem, "cboRequestItem");
		((TextEditorControlBase)this.cboRequestItem).AlwaysInEditMode = true;
		this.cboRequestItem.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboRequestItem).Name = "cboRequestItem";
		((TextEditorControlBase)this.cboRequestItem).ValueChanged += new System.EventHandler(cboRequestItem_ValueChanged);
		resources.ApplyResources(this.lblLine, "lblLine");
		this.lblLine.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLine).Name = "lblLine";
		((ControlBase)this.lblLine).WrapText = false;
		resources.ApplyResources(this.cboLine, "cboLine");
		((TextEditorControlBase)this.cboLine).AlwaysInEditMode = true;
		this.cboLine.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboLine).Name = "cboLine";
		((TextEditorControlBase)this.cboLine).ValueChanged += new System.EventHandler(cboLine_ValueChanged);
		resources.ApplyResources(this.lblItemCataloge, "lblItemCataloge");
		this.lblItemCataloge.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblItemCataloge).Name = "lblItemCataloge";
		((ControlBase)this.lblItemCataloge).WrapText = false;
		resources.ApplyResources(this.cboItemCataloge, "cboItemCataloge");
		((TextEditorControlBase)this.cboItemCataloge).AlwaysInEditMode = true;
		this.cboItemCataloge.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboItemCataloge).Name = "cboItemCataloge";
		((TextEditorControlBase)this.cboItemCataloge).ValueChanged += new System.EventHandler(cboItemCataloge_ValueChanged);
		resources.ApplyResources(this.lblQty, "lblQty");
		this.lblQty.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblQty).Name = "lblQty";
		((ControlBase)this.lblQty).WrapText = false;
		resources.ApplyResources(this.txtQty, "txtQty");
		resources.ApplyResources(val11, "appearance10");
		((TextEditorControlBase)this.txtQty).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.txtQty).Name = "txtQty";
		((TextEditorControlBase)this.txtQty).ValueChanged += new System.EventHandler(txtQty_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtQty).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.cboUnitName, "cboUnitName");
		((TextEditorControlBase)this.cboUnitName).AlwaysInEditMode = true;
		this.cboUnitName.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboUnitName).Name = "cboUnitName";
		resources.ApplyResources(this.btnRequestNoSearch, "btnRequestNoSearch");
		((AppearanceBase)val12).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val12, "appearance11");
		((ControlBase)this.btnRequestNoSearch).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.btnRequestNoSearch).Name = "btnRequestNoSearch";
		((System.Windows.Forms.Control)(object)this.btnRequestNoSearch).Click += new System.EventHandler(btnRequestNoSearch_Click);
		((UltraButtonBase)this.btnPrintBarCode).AcceptsFocus = false;
		resources.ApplyResources(this.btnPrintBarCode, "btnPrintBarCode");
		((System.Windows.Forms.Control)(object)this.btnPrintBarCode).Name = "btnPrintBarCode";
		((System.Windows.Forms.Control)(object)this.btnPrintBarCode).Click += new System.EventHandler(btnPrintBarCode_Click);
		resources.ApplyResources(this.btnItemCatalogSearch, "btnItemCatalogSearch");
		((AppearanceBase)val13).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val13, "appearance15");
		((ControlBase)this.btnItemCatalogSearch).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.btnItemCatalogSearch).Name = "btnItemCatalogSearch";
		((System.Windows.Forms.Control)(object)this.btnItemCatalogSearch).Click += new System.EventHandler(btnItemCatalogSearch_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPrintBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnItemCatalogSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnRequestNoSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboUnitName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblItemCataloge);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboItemCataloge);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLine);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboLine);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRequestItemID);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboRequestItem);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblProductionRequest);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboProductionRequest);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblProductionEndDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpProductionEndDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblProductionStartDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpProductionStartDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSourceStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSourceStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDestinationStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboDestinationStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRequestQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRequestQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBatchNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBatchNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Name = "frmProductions";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBatchNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBatchNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRequestQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRequestQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboDestinationStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDestinationStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSourceStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSourceStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpProductionStartDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblProductionStartDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpProductionEndDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblProductionEndDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboProductionRequest, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblProductionRequest, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboRequestItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRequestItemID, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboLine, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLine, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboItemCataloge, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblItemCataloge, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboUnitName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnRequestNoSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnItemCatalogSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPrintBarCode, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBatchNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRequestQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSourceStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDestinationStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpProductionStartDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpProductionEndDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboProductionRequest).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboRequestItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLine).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboItemCataloge).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnitName).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
