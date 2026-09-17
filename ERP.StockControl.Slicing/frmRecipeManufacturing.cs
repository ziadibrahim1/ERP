using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.POS;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.StockControl.MasterData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.StockControl.Slicing;

public class frmRecipeManufacturing : frmHeaderManyDetails
{
	private DataTable dtItems;

	private DataTable dtUnits;

	private DataTable dtStores;

	private DataTable dtBatchs;

	private DataTable dtRecipeManufacturingMaterials;

	private ValueList vlItemsManufacturing = new ValueList();

	private ValueList vlItemsMaterials = new ValueList();

	private ValueList vlUnitManufacturing = new ValueList();

	private ValueList vlUnitMaterials = new ValueList();

	private ValueList vlStoreManufacturing = new ValueList();

	private ValueList vlStoreMaterials = new ValueList();

	private ValueList vlBatchManufacturing = new ValueList();

	private ValueList vlBatchMaterials = new ValueList();

	private DataSet ds;

	private int newID = -100000;

	private bool UsingBatchNoAndValidityPeriod = false;

	private IContainer components = null;

	private UltraLabel lblDateIssue;

	private UltraDateTimeEditor dtpDateIssue;

	private UltraLabel lblDateManufacturing;

	private UltraDateTimeEditor dtpDateManufacturing;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraButton btnRecipeManufacturing;

	private UltraButton btnGetItems;

	private UltraComboEditor cboMaterialsStore;

	private UltraComboEditor cboRecipeStore;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	public frmRecipeManufacturing()
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
		InitializeComponent();
		TableName = "SCR_RecipeManufacturing";
		IDCol = "RecipeManufacturingID";
		NoCol = "RecipeManufacturingNo";
		DateCol = "RecipeManufacturingDate";
	}

	public frmRecipeManufacturing(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDateManufacturing.MaskInput = "dd/mm/yyyy hh:mm tt";
		dtpDateIssue.MaskInput = "dd/mm/yyyy hh:mm tt";
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchManufacturing.ValueListItems.Clear();
			vlBatchMaterials.ValueListItems.Clear();
			for (int i = 0; i < dtBatchs.Rows.Count; i++)
			{
				vlBatchManufacturing.ValueListItems.Add(dtBatchs.Rows[i]["BatchID"], dtBatchs.Rows[i]["BatchName"].ToString());
				vlBatchMaterials.ValueListItems.Add(dtBatchs.Rows[i]["BatchID"], dtBatchs.Rows[i]["BatchName"].ToString());
			}
		}
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "1", "0", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		DataView dataView = new DataView(dtItems);
		dataView.RowFilter = " IsRecipe=1 ";
		DataTable dataTable = dataView.ToTable();
		vlItemsMaterials.ValueListItems.Clear();
		vlItemsManufacturing.ValueListItems.Clear();
		for (int j = 0; j < dtItems.Rows.Count; j++)
		{
			vlItemsMaterials.ValueListItems.Add(dtItems.Rows[j]["ItemID"], dtItems.Rows[j]["Name"].ToString());
		}
		for (int k = 0; k < dataTable.Rows.Count; k++)
		{
			vlItemsManufacturing.ValueListItems.Add(dataTable.Rows[k]["ItemID"], dataTable.Rows[k]["Name"].ToString());
		}
		dtStores = Stores.FillCombo("-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboRecipeStore, dtStores, "StoreID", "StoreName");
		GlobalFunctions.FillCombo(cboMaterialsStore, dtStores, "StoreID", "StoreName");
		vlStoreManufacturing.ValueListItems.Clear();
		vlStoreMaterials.ValueListItems.Clear();
		for (int l = 0; l < dtStores.Rows.Count; l++)
		{
			vlStoreManufacturing.ValueListItems.Add(dtStores.Rows[l]["StoreID"], dtStores.Rows[l]["StoreName"].ToString());
			vlStoreMaterials.ValueListItems.Add(dtStores.Rows[l]["StoreID"], dtStores.Rows[l]["StoreName"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnitManufacturing.ValueListItems.Clear();
		vlUnitMaterials.ValueListItems.Clear();
		for (int m = 0; m < dtUnits.Rows.Count; m++)
		{
			vlUnitManufacturing.ValueListItems.Add(dtUnits.Rows[m]["UnitID"], dtUnits.Rows[m]["UnitName"].ToString());
			vlUnitMaterials.ValueListItems.Add(dtUnits.Rows[m]["UnitID"], dtUnits.Rows[m]["UnitName"].ToString());
		}
		dtDetails = RecipeManufacturingDetails.SelectByRecipeManufacturingID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtRecipeManufacturingMaterials = RecipeManufacturingMaterials.SelectByRecipeManufacturingID("0", GlobalVariables.IsArabic ? "1" : "0");
		ds = new DataSet();
		ds.Tables.Add(dtDetails);
		ds.Tables.Add(dtRecipeManufacturingMaterials);
		ds.Tables[0].TableName = "dtDetails";
		ds.Tables[1].TableName = "dtRecipeManufacturingMaterials";
		ds.Relations.Add(ds.Tables[0].Columns["RecipeManufacturingDetailID"], ds.Tables[1].Columns["RecipeManufacturingDetailID"]);
		((UltraGridBase)ULGData).DataSource = ds;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		if (UsingBatchNoAndValidityPeriod)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "رقم التشغيلة" : "Batch No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].ValueList = (IValueList)(object)vlBatchManufacturing;
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = true;
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.18) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "مخزن" : "Store");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItemsManufacturing;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnitManufacturing;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].ValueList = (IValueList)(object)vlStoreManufacturing;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 0;
		if (UsingBatchNoAndValidityPeriod)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BatchID"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "رقم التشغيلة" : "Batch No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BatchID"].ValueList = (IValueList)(object)vlBatchMaterials;
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = true;
		}
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "مخزن" : "Store");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].ValueList = (IValueList)(object)vlItemsMaterials;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].ValueList = (IValueList)(object)vlUnitMaterials;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["StoreID"].ValueList = (IValueList)(object)vlStoreMaterials;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = RecipeManufacturing.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
			dtpDateIssue.ValueChanged -= dtpDateIssue_ValueChanged;
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["RecipeManufacturingNo"].ToString();
			dtpDateManufacturing.Value = (DateTime)drMaster["RecipeManufacturingDate"];
			dtpDateIssue.Value = (DateTime)drMaster["RecipeMaterialsIssueDate"];
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			dtpDateIssue.ValueChanged += dtpDateIssue_ValueChanged;
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDateManufacturing.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = RecipeManufacturingDetails.SelectByRecipeManufacturingID(drMaster["RecipeManufacturingID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtRecipeManufacturingMaterials = RecipeManufacturingMaterials.SelectByRecipeManufacturingID(drMaster["RecipeManufacturingID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			ds = new DataSet();
			ds.Tables.Add(dtDetails);
			ds.Tables.Add(dtRecipeManufacturingMaterials);
			ds.Tables[0].TableName = "dtDetails";
			ds.Tables[1].TableName = "dtRecipeManufacturingMaterials";
			ds.Relations.Add(ds.Tables[0].Columns["RecipeManufacturingDetailID"], ds.Tables[1].Columns["RecipeManufacturingDetailID"]);
			((UltraGridBase)ULGData).DataSource = ds;
			InitGrid();
			if (drMaster["Approved"].Equals(true) || drMaster["IsInternal"].Equals(true))
			{
				((Control)(object)btnDelete).Enabled = false;
				((Control)(object)btnUpdate).Enabled = false;
			}
			else
			{
				((Control)(object)btnDelete).Enabled = true;
				((Control)(object)btnUpdate).Enabled = true;
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
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)btnRecipeManufacturing).Visible = NavMode;
		((Control)(object)btnGetItems).Visible = NavMode;
		if (Adding || Updating)
		{
			DataView dataView = new DataView(dtStores);
			dataView.RowFilter = " BranchID = " + GlobalVariables.CurrentBranchID + " And Locked = 0";
			DataTable dataTable = dataView.ToTable();
			GlobalFunctions.FillCombo(cboRecipeStore, dataTable, "StoreID", "StoreName");
			GlobalFunctions.FillCombo(cboMaterialsStore, dataTable, "StoreID", "StoreName");
			vlStoreManufacturing.ValueListItems.Clear();
			vlStoreMaterials.ValueListItems.Clear();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				vlStoreManufacturing.ValueListItems.Add(dataTable.Rows[i]["StoreID"], dataTable.Rows[i]["StoreName"].ToString());
				vlStoreMaterials.ValueListItems.Add(dataTable.Rows[i]["StoreID"], dataTable.Rows[i]["StoreName"].ToString());
			}
		}
		else
		{
			GlobalFunctions.FillCombo(cboRecipeStore, dtStores, "StoreID", "StoreName");
			GlobalFunctions.FillCombo(cboMaterialsStore, dtStores, "StoreID", "StoreName");
			vlStoreManufacturing.ValueListItems.Clear();
			vlStoreMaterials.ValueListItems.Clear();
			for (int j = 0; j < dtStores.Rows.Count; j++)
			{
				vlStoreManufacturing.ValueListItems.Add(dtStores.Rows[j]["StoreID"], dtStores.Rows[j]["StoreName"].ToString());
				vlStoreMaterials.ValueListItems.Add(dtStores.Rows[j]["StoreID"], dtStores.Rows[j]["StoreName"].ToString());
			}
		}
		if (!Updating)
		{
			return;
		}
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
		{
			if (((UltraGridBase)ULGData).Rows[k].Cells["ItemID"].Value != DBNull.Value)
			{
				int itemID = int.Parse(((UltraGridBase)ULGData).Rows[k].Cells["ItemID"].Value.ToString());
				if (UsingBatchNoAndValidityPeriod)
				{
					ValueList batchsValueList = getBatchsValueList(itemID);
					((UltraGridBase)ULGData).Rows[k].Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
					if (((DisposableObjectCollectionBase)batchsValueList.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGData).Rows[k].Cells["BatchID"].Value = DBNull.Value;
					}
				}
			}
			for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows).Count; l++)
			{
				int itemID2 = int.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["ItemID"].Value.ToString());
				if (UsingBatchNoAndValidityPeriod)
				{
					ValueList batchsValueList2 = getBatchsValueList(itemID2);
					((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList2;
					if (((DisposableObjectCollectionBase)batchsValueList2.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["BatchID"].Value = DBNull.Value;
					}
				}
			}
		}
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		dtpDateManufacturing.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? RecipeManufacturing.GetCodeByBranchID(dtpDateManufacturing.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		dtpDateIssue.ValueChanged -= dtpDateIssue_ValueChanged;
		dtpDateIssue.DateTime = GlobalFunctions.GetServerDateTimeNow();
		dtpDateIssue.ValueChanged += dtpDateIssue_ValueChanged;
		((TextEditorControlBase)txtNotes).Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[1].Rows.Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[0].Rows.Clear();
	}

	public override bool ValidateData()
	{
		if (dtpDateManufacturing.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ التصنيع" : "Please Enter The Manufacturing Date");
			((Control)(object)dtpDateManufacturing).Focus();
			dtpDateManufacturing.DropDown();
			return false;
		}
		if (dtpDateIssue.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ الصرف" : "Please Enter The issue Date");
			((Control)(object)dtpDateIssue).Focus();
			dtpDateIssue.DropDown();
			return false;
		}
		if (!FiscalYear.ChkForConfirmedFiscalYear(dtpDateManufacturing.DateTime.ToString(GlobalVariables.DateShortFormate), IsFromServer: false))
		{
			GlobalVariables.InformationMB.Show("السنة المالية غير معتمدة", "The Fiscal Year is not confirmed..");
			return false;
		}
		if (FiscalYear.ChkForClosingFsicalPeriod(dtpDateManufacturing.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false))
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
		if (Main.CheckForValueByBranchIDAndFiscalYearID("dbo.SCR_RecipeManufacturing", "RecipeManufacturingNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["RecipeManufacturingNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDateManufacturing.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = RecipeManufacturing.GetCodeByBranchID(dtpDateManufacturing.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
			if (((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار إسم الصنف  ", "Please Select Item Name ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Qty"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار إسم الوحدة  ", "Please Select Unit Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار المخزن  ", "Please Select Store");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			if (UsingBatchNoAndValidityPeriod && bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) && ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء اختيار سريل", "Please choose Batch No");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["BatchID"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show(" لا يمكن تكرار الصنف مع نفس التشغيلة ", "Cannot Duplicate The Same Item With the Batch No ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
					return false;
				}
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count == 0)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا الصنف", "Please insert details for this Item");
				return false;
			}
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; k++)
			{
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["ItemID"].Value == ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value)
				{
					GlobalVariables.InformationMB.Show("الصنف لايمكن تصنيعه من نفس الصنف  ", "Item Cannot Manufactured with the same Item ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["ItemID"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["ItemID"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("برجاء إختيار إسم الصنف  ", "Please Select Item Name ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["ItemID"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["Qty"].Value.ToString()) <= 0m)
				{
					GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["Qty"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["UnitID"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("برجاء إختيار إسم الوحدة  ", "Please Select Unit Name");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["UnitID"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["StoreID"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("برجاء إختيار المخزن  ", "Please Select Store");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["StoreID"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
				if (UsingBatchNoAndValidityPeriod && bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) && ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["BatchID"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء اختيار سريل", "Please choose Batch No");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["BatchID"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; l++)
				{
					if (l != k && ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["ItemID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[l].Cells["ItemID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["BatchID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[l].Cells["BatchID"].Value.ToString())
					{
						GlobalVariables.InformationMB.Show(" لا يمكن تكرار الصنف مع نفس التشغيلة ", "Cannot Duplicate The Same Item With the Batch No ");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["ItemID"];
						return false;
					}
				}
			}
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = RecipeManufacturing.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDateManufacturing.DateTime.ToString(GlobalVariables.DateLongFormate), dtpDateIssue.DateTime.ToString(GlobalVariables.DateLongFormate), "0", ((Control)(object)txtNotes).Text, "0", "Null", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				int num2 = RecipeManufacturingDetails.Insert_Update("-1", num.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString(), dtpDateManufacturing.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
				{
					RecipeManufacturingMaterials.Insert_Update("-1", num.ToString(), num2.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BatchID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BatchID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Qty"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["StoreID"].Value.ToString(), dtpDateIssue.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
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
			int num = RecipeManufacturing.Insert_Update(drMaster["RecipeManufacturingID"].ToString(), ((Control)(object)txtCode).Text, dtpDateManufacturing.DateTime.ToString(GlobalVariables.DateLongFormate), dtpDateIssue.DateTime.ToString(GlobalVariables.DateLongFormate), "0", ((Control)(object)txtNotes).Text, bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", (drMaster["StockControlJVID"] == DBNull.Value) ? "Null" : drMaster["StockControlJVID"].ToString(), "1", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			RecipeManufacturingMaterials.DeleteByRecipeManufacturingID(num.ToString(), GlobalVariables.UserID);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["RecipeManufacturingDetailID"].Value.ToString() + ",";
			}
			Main.DeleteForUpdate("SCR_RecipeManufacturingDetails", "RecipeManufacturingID", drMaster["RecipeManufacturingID"].ToString(), "RecipeManufacturingDetailID", text);
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				int num2 = RecipeManufacturingDetails.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[j].Cells["RecipeManufacturingDetailID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[j].Cells["RecipeManufacturingDetailID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[j].Cells["RecipeManufacturingDetailID"].Value.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["BatchID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["BatchID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["UnitID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["StoreID"].Value.ToString(), dtpDateManufacturing.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows).Count; k++)
				{
					RecipeManufacturingMaterials.Insert_Update("-1", num.ToString(), num2.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["BatchID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["BatchID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["Qty"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["UnitID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["StoreID"].Value.ToString(), dtpDateIssue.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				}
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
			RecipeManufacturingMaterials.DeleteVirtualByRecipeManufacturingID(drMaster["RecipeManufacturingID"].ToString(), GlobalVariables.UserID);
			RecipeManufacturingDetails.DeleteVirtualByRecipeManufacturingID(drMaster["RecipeManufacturingID"].ToString(), GlobalVariables.UserID);
			RecipeManufacturing.DeleteVirtual(drMaster["RecipeManufacturingID"].ToString(), GlobalVariables.UserID);
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

	public override void btnPrintClick()
	{
		if (RowID != "")
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_SCR_RecipeManufacturing_A.rpt" : "Rep_SCR_RecipeManufacturing_E.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@RecipeManufacturingIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.RecipeManufacturingReport(-1);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["RecipeManufacturingID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtpDateManufacturing.MaskInput = "dd/mm/yyyy hh:mm tt";
		dtpDateIssue.MaskInput = "dd/mm/yyyy hh:mm tt";
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchManufacturing.ValueListItems.Clear();
			vlBatchMaterials.ValueListItems.Clear();
			for (int i = 0; i < dtBatchs.Rows.Count; i++)
			{
				vlBatchManufacturing.ValueListItems.Add(dtBatchs.Rows[i]["BatchID"], dtBatchs.Rows[i]["BatchName"].ToString());
				vlBatchMaterials.ValueListItems.Add(dtBatchs.Rows[i]["BatchID"], dtBatchs.Rows[i]["BatchName"].ToString());
			}
		}
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "1", "0", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		DataView dataView = new DataView(dtItems);
		dataView.RowFilter = " IsRecipe=1 ";
		DataTable dataTable = dataView.ToTable();
		vlItemsMaterials.ValueListItems.Clear();
		vlItemsManufacturing.ValueListItems.Clear();
		for (int j = 0; j < dtItems.Rows.Count; j++)
		{
			vlItemsMaterials.ValueListItems.Add(dtItems.Rows[j]["ItemID"], dtItems.Rows[j]["Name"].ToString());
		}
		for (int k = 0; k < dataTable.Rows.Count; k++)
		{
			vlItemsManufacturing.ValueListItems.Add(dataTable.Rows[k]["ItemID"], dataTable.Rows[k]["Name"].ToString());
		}
		dtStores = Stores.FillCombo("-1", "-1", "," + GlobalVariables.CurrentBranchID + ",", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlStoreManufacturing.ValueListItems.Clear();
		vlStoreMaterials.ValueListItems.Clear();
		for (int l = 0; l < dtStores.Rows.Count; l++)
		{
			vlStoreManufacturing.ValueListItems.Add(dtStores.Rows[l]["StoreID"], dtStores.Rows[l]["StoreName"].ToString());
			vlStoreMaterials.ValueListItems.Add(dtStores.Rows[l]["StoreID"], dtStores.Rows[l]["StoreName"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnitManufacturing.ValueListItems.Clear();
		vlUnitMaterials.ValueListItems.Clear();
		for (int m = 0; m < dtUnits.Rows.Count; m++)
		{
			vlUnitManufacturing.ValueListItems.Add(dtUnits.Rows[m]["UnitID"], dtUnits.Rows[m]["UnitName"].ToString());
			vlUnitMaterials.ValueListItems.Add(dtUnits.Rows[m]["UnitID"], dtUnits.Rows[m]["UnitName"].ToString());
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

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Expected O, but got Unknown
		//IL_05b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dtItems.Select(" ItemID= " + e.Cell.Value)[0]["UnitID"];
			if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["StoreID"].Value;
			}
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
		}
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "BatchID")
		{
			int num2 = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtBatchs.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"].ToString()) : 0);
			if (num2 != 0)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = num2;
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dtItems.Select(" ItemID= " + num2)[0]["UnitID"];
				if (((GridItemBase)e.Cell).Band.Index == 1 && e.Cell.Row.ParentRow.Cells["StoreID"].Value != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = e.Cell.Row.ParentRow.Cells["StoreID"].Value;
				}
			}
		}
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0 && ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "StoreID") && ULGData.ActiveCell.Value != DBNull.Value && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value != DBNull.Value && ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value != DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > 0m)
		{
			GetItemMaterials(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString(), ((UltraGridBase)ULGData).ActiveRow);
		}
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1 && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "StoreID")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F6)
		{
			if ((Adding || Updating) && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
			{
				frmAddItemsSerial frmAddItemsSerial2 = new frmAddItemsSerial(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
				frmAddItemsSerial2.WindowState = FormWindowState.Normal;
				frmAddItemsSerial2.ShowDialog();
				if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
				{
					dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
					vlBatchManufacturing.ValueListItems.Clear();
					for (int i = 0; i < dtBatchs.Rows.Count; i++)
					{
						vlBatchManufacturing.ValueListItems.Add(dtBatchs.Rows[i]["BatchID"], dtBatchs.Rows[i]["BatchName"].ToString());
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = frmAddItemsSerial2.BatchID;
				}
			}
			e.Handled = true;
		}
		else
		{
			if (e.KeyCode != Keys.F8)
			{
				return;
			}
			if (Adding || Updating)
			{
				if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID")
				{
					int num = (Adding ? SearchFunctions.Items("-1", (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0) ? "1" : "-1", "0", "-1", "1", "0", "-1", "-1", "-1", "-1", IsFromServer: false) : SearchFunctions.Items("-1", (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0) ? "1" : "-1", "0", "-1", "-1", "0", "-1", "-1", "-1", "-1", IsFromServer: false));
					if (num != 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = num;
					}
				}
				else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select(" ItemID = '" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString() + "'")[0]["EnforceBatchNo"].ToString()))
				{
					int num2 = SearchFunctions.ItemsBatchesSearch(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()), 0, IsFromServer: false);
					if (num2 != 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = num2;
					}
				}
			}
			e.Handled = true;
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty")
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((GridItemBase)e.Row).Band.Index == 0)
		{
			e.Row.Cells["RecipeManufacturingDetailID"].Value = ++newID;
		}
		else if (((GridItemBase)e.Row).Band.Index == 1)
		{
			e.Row.Cells["RecipeManufacturingMaterialID"].Value = ++newID;
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0 && ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "StoreID") && ULGData.ActiveCell.Value != DBNull.Value && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value != DBNull.Value && ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value != DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > 0m)
		{
			GetItemMaterials(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString(), ((UltraGridBase)ULGData).ActiveRow);
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void btnGetItems_Click(object sender, EventArgs e)
	{
		if (dtpDateManufacturing.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ التصنيع" : "Please Enter The Manufacturing Date");
			((Control)(object)dtpDateManufacturing).Focus();
			dtpDateManufacturing.DropDown();
			return;
		}
		if (dtpDateIssue.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ الصرف" : "Please Enter The issue Date");
			((Control)(object)dtpDateIssue).Focus();
			dtpDateIssue.DropDown();
			return;
		}
		if (!FiscalYear.ChkForConfirmedFiscalYear(dtpDateManufacturing.DateTime.ToString(GlobalVariables.DateShortFormate), IsFromServer: false))
		{
			GlobalVariables.InformationMB.Show("السنة المالية غير معتمدة", "The Fiscal Year is not confirmed..");
			return;
		}
		if (FiscalYear.ChkForClosingFsicalPeriod(dtpDateManufacturing.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false))
		{
			GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ يقع في فتره ماليه مغلقة", "The Date you choosed\n\r exists in closed fisical period");
			return;
		}
		if (cboMaterialsStore.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار مخزن الخامات" : "Please Select Materials Store");
			((TextEditorControlBase)cboMaterialsStore).Focus();
			cboMaterialsStore.DropDown();
			return;
		}
		if (cboRecipeStore.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار مخزن المنتج" : "Please Select Recipe Store");
			((TextEditorControlBase)cboRecipeStore).Focus();
			cboRecipeStore.DropDown();
			return;
		}
		DataTable dataTable = SearchFunctions.ItemsReport("0", "1", "0", "-1", "1", "0", "-1", "-1", "-1", "-1", IsFromServer: false);
		string text = ",";
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			text = text + dataTable.Rows[i]["ItemID"].ToString() + ",";
		}
		if (!(text != ","))
		{
			return;
		}
		Main.StartBulkTrans(FromServer: false);
		try
		{
			DataTable dataTable2 = ShiftsDetails.InsertByItemIDs(dtpDateManufacturing.DateTime.ToString(GlobalVariables.DateLongFormate), dtpDateIssue.DateTime.ToString(GlobalVariables.DateLongFormate), text, ((TextEditorControlBase)cboRecipeStore).Value.ToString(), ((TextEditorControlBase)cboMaterialsStore).Value.ToString(), GlobalVariables.CurrentBranchID);
			Main.EndBulkTrans(FromServer: false);
			RowID = dataTable2.Rows[0]["ID"].ToString();
			FillData();
			ItemsTransactions.ManageInThread();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void dtpDateIssue_ValueChanged(object sender, EventArgs e)
	{
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Expected O, but got Unknown
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Expected O, but got Unknown
		if (dtpDateIssue.Value == DBNull.Value)
		{
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value != DBNull.Value && ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value != DBNull.Value && ((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value != DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) > 0m)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				GetItemMaterials(((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i]);
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
	}

	public void GetItemMaterials(string ItemID, string StoreID, string Qty, UltraGridRow ParentRow)
	{
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Expected O, but got Unknown
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Expected O, but got Unknown
		DataTable dataTable = new DataTable();
		dataTable = RecipeManufacturing.GetItemsMaterials(dtpDateIssue.DateTime.ToString(GlobalVariables.DateLongFormate), ItemID, StoreID, Qty, GlobalVariables.CurrentBranchID);
		ds.AcceptChanges();
		for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
		{
			if (ds.Tables[1].Rows[i]["RecipeManufacturingDetailID"].ToString() == ParentRow.Cells["RecipeManufacturingDetailID"].Value.ToString())
			{
				ds.Tables[1].Rows[i].Delete();
				i--;
				ds.AcceptChanges();
			}
		}
		ds.AcceptChanges();
		ULGData.AfterRowInsert -= new RowEventHandler(ULGData_AfterRowInsert);
		((Control)(object)ULGData).Enter -= ULGData_Enter;
		for (int j = 0; j < dataTable.Rows.Count; j++)
		{
			DataRow dataRow = ds.Tables[1].NewRow();
			dataRow["RecipeManufacturingDetailID"] = ParentRow.Cells["RecipeManufacturingDetailID"].Value.ToString();
			dataRow["ItemID"] = dataTable.Rows[j]["ItemID"];
			dataRow["BatchID"] = dataTable.Rows[j]["BatchID"];
			dataRow["Qty"] = dataTable.Rows[j]["Qty"].ToString();
			dataRow["UnitID"] = dataTable.Rows[j]["UnitID"];
			dataRow["StoreID"] = ParentRow.Cells["StoreID"].Value;
			ds.Tables[1].Rows.Add(dataRow);
		}
		ds.AcceptChanges();
		for (int k = 0; k < ((DisposableObjectCollectionBase)ParentRow.ChildBands[0].Rows).Count; k++)
		{
			if (UsingBatchNoAndValidityPeriod)
			{
				ValueList batchsValueList = getBatchsValueList(int.Parse(ParentRow.ChildBands[0].Rows[k].Cells["ItemID"].Value.ToString()));
				ParentRow.ChildBands[0].Rows[k].Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
				if (((DisposableObjectCollectionBase)batchsValueList.ValueListItems).Count == 0)
				{
					ParentRow.ChildBands[0].Rows[k].Cells["BatchID"].Value = DBNull.Value;
				}
			}
		}
		((Control)(object)ULGData).Enter += ULGData_Enter;
		ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
	}

	private void btnRecipeManufacturing_Click(object sender, EventArgs e)
	{
		DataTable dataTable = ShiftsDetails.SelectNotClosed(GlobalVariables.CurrentBranchID);
		if (dataTable.Rows.Count != 1)
		{
			GlobalVariables.InformationMB.Show("برجاء فتح وردية اولا", "Please open Shift First");
			return;
		}
		ShiftsDetails.InsertRecipe(DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()).ToString(GlobalVariables.DateLongFormate), GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID);
		GlobalVariables.InformationMB.Show("تمت العملية بنجاح", "Operation Made Successfuly");
	}

	private void dtpDateManufacturing_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = RecipeManufacturing.GetCodeByBranchID(dtpDateManufacturing.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Expected O, but got Unknown
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Expected O, but got Unknown
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Expected O, but got Unknown
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Expected O, but got Unknown
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.Slicing.frmRecipeManufacturing));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		this.lblDateIssue = new UltraLabel();
		this.dtpDateIssue = new UltraDateTimeEditor();
		this.lblDateManufacturing = new UltraLabel();
		this.dtpDateManufacturing = new UltraDateTimeEditor();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.btnRecipeManufacturing = new UltraButton();
		this.btnGetItems = new UltraButton();
		this.cboMaterialsStore = new UltraComboEditor();
		this.cboRecipeStore = new UltraComboEditor();
		this.ultraLabel1 = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDateIssue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDateManufacturing).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaterialsStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboRecipeStore).BeginInit();
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
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
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
		resources.ApplyResources(this.lblDateIssue, "lblDateIssue");
		this.lblDateIssue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDateIssue).Name = "lblDateIssue";
		((ControlBase)this.lblDateIssue).WrapText = false;
		resources.ApplyResources(this.dtpDateIssue, "dtpDateIssue");
		((UltraWinEditorMaskedControlBase)this.dtpDateIssue).AlwaysInEditMode = true;
		this.dtpDateIssue.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDateIssue).Name = "dtpDateIssue";
		this.dtpDateIssue.ValueChanged += new System.EventHandler(dtpDateIssue_ValueChanged);
		resources.ApplyResources(this.lblDateManufacturing, "lblDateManufacturing");
		this.lblDateManufacturing.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDateManufacturing).Name = "lblDateManufacturing";
		((ControlBase)this.lblDateManufacturing).WrapText = false;
		resources.ApplyResources(this.dtpDateManufacturing, "dtpDateManufacturing");
		((UltraWinEditorMaskedControlBase)this.dtpDateManufacturing).AlwaysInEditMode = true;
		this.dtpDateManufacturing.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDateManufacturing).Name = "dtpDateManufacturing";
		this.dtpDateManufacturing.ValueChanged += new System.EventHandler(dtpDateManufacturing_ValueChanged);
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.btnRecipeManufacturing, "btnRecipeManufacturing");
		((System.Windows.Forms.Control)(object)this.btnRecipeManufacturing).Name = "btnRecipeManufacturing";
		((System.Windows.Forms.Control)(object)this.btnRecipeManufacturing).Click += new System.EventHandler(btnRecipeManufacturing_Click);
		resources.ApplyResources(this.btnGetItems, "btnGetItems");
		((System.Windows.Forms.Control)(object)this.btnGetItems).Name = "btnGetItems";
		((ControlBase)this.btnGetItems).WrapText = false;
		((System.Windows.Forms.Control)(object)this.btnGetItems).Click += new System.EventHandler(btnGetItems_Click);
		resources.ApplyResources(this.cboMaterialsStore, "cboMaterialsStore");
		((TextEditorControlBase)this.cboMaterialsStore).AlwaysInEditMode = true;
		this.cboMaterialsStore.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboMaterialsStore).Name = "cboMaterialsStore";
		resources.ApplyResources(this.cboRecipeStore, "cboRecipeStore");
		((TextEditorControlBase)this.cboRecipeStore).AlwaysInEditMode = true;
		this.cboRecipeStore.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboRecipeStore).Name = "cboRecipeStore";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboRecipeStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboMaterialsStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnGetItems);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnRecipeManufacturing);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDateIssue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDateIssue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDateManufacturing);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDateManufacturing);
		base.Name = "frmRecipeManufacturing";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDateManufacturing, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDateManufacturing, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDateIssue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDateIssue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnRecipeManufacturing, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnGetItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboMaterialsStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboRecipeStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDateIssue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDateManufacturing).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaterialsStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboRecipeStore).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
