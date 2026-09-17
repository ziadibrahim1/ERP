using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.StockControl.Slicing;

public class frmSlicing : frmHeaderManyDetails
{
	private DataTable dtAnimalPartItems;

	private DataTable dtMeatTypeItems;

	private DataTable dtSuppliers;

	private DataTable dtBatchs;

	private DataTable dtUnitGroup;

	private DataTable dtUnits;

	private DataTable dtMeatType;

	private DataTable dtSlicingMeatDetails;

	private DataTable dtStores;

	private DataTable dtAvailableBatches;

	private DataTable dtAnimalPartDetails;

	private ValueList vlMeatType = new ValueList();

	private ValueList MeatTypeItems = new ValueList();

	private ValueList vlUnits = new ValueList();

	private DataSet ds;

	private int newID = -100000;

	private decimal UnitPrice = default(decimal);

	private IContainer components = null;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraTextEditor txtSliceWeight;

	private UltraLabel lblSliceWeight;

	private UltraLabel lblSupplier;

	private UltraComboEditor cboSupplier;

	private UltraLabel lblItem;

	private UltraComboEditor cboItem;

	private UltraLabel lblBatchNo;

	private UltraComboEditor cboBatchNo;

	private UltraLabel lblUnit;

	private UltraComboEditor cboUnit;

	private UltraLabel lblUnitGroup;

	private UltraComboEditor cboUnitGroup;

	private UltraTextEditor txtUnitPrice;

	private UltraLabel lblUnitPrice;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraTextEditor txtActualWastePercentage;

	private UltraLabel lblActualWastePercentage;

	private UltraLabel lblEstimatedWastePercentage;

	private UltraTextEditor txtEstimatedWastePercentage;

	private UltraLabel lblSourceStore;

	private UltraComboEditor cboSourceStore;

	private UltraLabel lblDestinationStore;

	private UltraComboEditor cboDestinationStore;

	public UltraButton btnRecalculate;

	private UltraCheckEditor chkIsWeight;

	public frmSlicing()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		InitializeComponent();
		TableName = "SCR_Slicing";
		IDCol = "SlicingID";
		NoCol = "SlicingNo";
		DateCol = "SlicingDate";
	}

	public frmSlicing(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		dtAnimalPartItems = AnimalParts.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboItem, dtAnimalPartItems, "ItemID", "Name");
		dtMeatTypeItems = MeatTypesDetails.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		MeatTypeItems.ValueListItems.Clear();
		for (int i = 0; i < dtMeatTypeItems.Rows.Count; i++)
		{
			MeatTypeItems.ValueListItems.Add(dtMeatTypeItems.Rows[i]["ItemID"], dtMeatTypeItems.Rows[i]["Name"].ToString());
		}
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSourceStore, dtStores, "StoreID", "StoreName");
		GlobalFunctions.FillCombo(cboDestinationStore, dtStores, "StoreID", "StoreName");
		dtSuppliers = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.SupplierSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSupplier, dtSuppliers, "SubAccountID", "SubAccountName");
		dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
		GlobalFunctions.FillCombo(cboBatchNo, dtBatchs, "BatchID", "BatchName");
		dtUnitGroup = UnitsTypes.FillCombo("-1", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboUnitGroup, dtUnitGroup, "UnitTypeID", "UnitTypeName");
		dtUnits = Units.FillComboByWeight("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboUnit, dtUnits, "UnitID", "UnitName");
		vlUnits.ValueListItems.Clear();
		for (int j = 0; j < dtUnits.Rows.Count; j++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[j]["UnitID"], dtUnits.Rows[j]["UnitName"].ToString());
		}
		dtMeatType = MeatTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlMeatType.ValueListItems.Clear();
		for (int k = 0; k < dtMeatType.Rows.Count; k++)
		{
			vlMeatType.ValueListItems.Add(dtMeatType.Rows[k]["MeatTypeID"], dtMeatType.Rows[k]["MeatTypeName"].ToString());
		}
		dtDetails = SlicingMeat.SelectBySlicingID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtSlicingMeatDetails = SlicingMeatDetails.SelectBySlicingID("0", GlobalVariables.IsArabic ? "1" : "0");
		ds = new DataSet();
		ds.Tables.Add(dtDetails);
		ds.Tables.Add(dtSlicingMeatDetails);
		ds.Tables[0].TableName = "dtDetails";
		ds.Tables[1].TableName = "dtSlicingMeatDetails";
		ds.Relations.Add(ds.Tables[0].Columns["SlicingMeatID"], ds.Tables[1].Columns["SlicingMeatID"]);
		((UltraGridBase)ULGData).DataSource = ds;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MeatTypeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.18) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EstimatedPercentage"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualPercentage"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MeatTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع اللحم" : "Meat Type");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EstimatedPercentage"].Header).Caption = (GlobalVariables.IsArabic ? "النسبة المقدرة" : "Estimated Percentage");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualPercentage"].Header).Caption = (GlobalVariables.IsArabic ? "االنسبة الفعلية" : "Actual Percentage");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحدة" : "Unit Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الاجمالى" : "Total");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MeatTypeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EstimatedPercentage"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualPercentage"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MeatTypeID"].ValueList = (IValueList)(object)vlMeatType;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EstimatedPercentage"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualPercentage"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].ValueList = (IValueList)(object)MeatTypeItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
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
			DataTable dataTable = BusinessLayer.StockControl.Slicing.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Expected O, but got Unknown
		base.DisplayData();
		if (drMaster != null)
		{
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			((TextEditorControlBase)cboItem).ValueChanged -= cboItem_ValueChanged;
			((TextEditorControlBase)cboBatchNo).ValueChanged -= cboBatchNo_ValueChanged;
			((TextEditorControlBase)cboUnit).ValueChanged -= cboUnit_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((TextEditorControlBase)txtSliceWeight).ValueChanged -= txtSliceWeight_ValueChanged;
			((UltraToggleEditorBase)chkIsWeight).CheckedChanged -= chkIsWeight_CheckedChanged;
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["SlicingNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["SlicingDate"];
			((UltraToggleEditorBase)chkIsWeight).Checked = bool.Parse(drMaster["IsWeight"].ToString());
			((Control)(object)txtSliceWeight).Text = drMaster["SlicingWeight"].ToString();
			((TextEditorControlBase)cboSourceStore).Value = drMaster["SourceStoreID"];
			((TextEditorControlBase)cboDestinationStore).Value = drMaster["DestinationStoreID"];
			((TextEditorControlBase)cboItem).Value = drMaster["ItemID"];
			((TextEditorControlBase)cboBatchNo).Value = drMaster["BatchID"];
			((TextEditorControlBase)cboUnitGroup).Value = drMaster["UnitTypeID"];
			((TextEditorControlBase)cboUnit).Value = drMaster["UnitID"];
			((Control)(object)txtUnitPrice).Text = drMaster["UnitPrice"].ToString();
			((TextEditorControlBase)cboSupplier).Value = drMaster["SubAccountID"];
			((Control)(object)txtEstimatedWastePercentage).Text = drMaster["EstimatedWastePercentage"].ToString();
			((Control)(object)txtActualWastePercentage).Text = drMaster["ActualWastePercentage"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			if (((UltraToggleEditorBase)chkIsWeight).Checked)
			{
				UnitPrice = decimal.Parse((((Control)(object)txtSliceWeight).Text == "") ? "0" : ((Control)(object)txtSliceWeight).Text) / decimal.Parse(((Control)(object)txtUnitPrice).Text);
			}
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = SlicingMeat.SelectBySlicingID(drMaster["SlicingID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtSlicingMeatDetails = SlicingMeatDetails.SelectBySlicingID(drMaster["SlicingID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			ds = new DataSet();
			ds.Tables.Add(dtDetails);
			ds.Tables.Add(dtSlicingMeatDetails);
			ds.Tables[0].TableName = "dtDetails";
			ds.Tables[1].TableName = "dtSlicingMeatDetails";
			ds.Relations.Add(ds.Tables[0].Columns["SlicingMeatID"], ds.Tables[1].Columns["SlicingMeatID"]);
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
			dtpDate.ValueChanged += dtpDate_ValueChanged;
			((TextEditorControlBase)cboItem).ValueChanged += cboItem_ValueChanged;
			((TextEditorControlBase)cboBatchNo).ValueChanged += cboBatchNo_ValueChanged;
			((TextEditorControlBase)cboUnit).ValueChanged += cboUnit_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			((TextEditorControlBase)txtSliceWeight).ValueChanged += txtSliceWeight_ValueChanged;
			((UltraToggleEditorBase)chkIsWeight).CheckedChanged += chkIsWeight_CheckedChanged;
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
		((EditorButtonControlBase)txtSliceWeight).ReadOnly = NavMode;
		((EditorButtonControlBase)cboItem).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBatchNo).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSourceStore).ReadOnly = true;
		((EditorButtonControlBase)cboDestinationStore).ReadOnly = NavMode;
		((EditorButtonControlBase)cboUnitGroup).ReadOnly = true;
		((EditorButtonControlBase)cboUnit).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSupplier).ReadOnly = true;
		((EditorButtonControlBase)txtUnitPrice).ReadOnly = true;
		((Control)(object)chkIsWeight).Enabled = !NavMode;
		((EditorButtonControlBase)txtEstimatedWastePercentage).ReadOnly = true;
		((EditorButtonControlBase)txtActualWastePercentage).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)btnRecalculate).Visible = Updating;
		int num = 0;
		if (cboItem.SelectedIndex > -1)
		{
			num = int.Parse(((TextEditorControlBase)cboItem).Value.ToString());
		}
		if (Updating)
		{
			DataView dataView = new DataView(dtAnimalPartItems);
			dataView.RowFilter = (((UltraToggleEditorBase)chkIsWeight).Checked ? " IsWeight =  1" : " IsWeight =  0");
			GlobalFunctions.FillCombo(cboItem, dataView.ToTable(), "ItemID", "Name");
		}
		if (NavMode)
		{
			dtAnimalPartItems = AnimalParts.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboItem, dtAnimalPartItems, "ItemID", "Name");
		}
		((TextEditorControlBase)cboItem).ValueChanged -= cboItem_ValueChanged;
		if (num > 0)
		{
			((TextEditorControlBase)cboItem).Value = num;
		}
		((TextEditorControlBase)cboItem).ValueChanged += cboItem_ValueChanged;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? BusinessLayer.StockControl.Slicing.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		((TextEditorControlBase)cboItem).ValueChanged -= cboItem_ValueChanged;
		((TextEditorControlBase)cboBatchNo).ValueChanged -= cboBatchNo_ValueChanged;
		((TextEditorControlBase)cboUnit).ValueChanged -= cboUnit_ValueChanged;
		((TextEditorControlBase)txtSliceWeight).ValueChanged -= txtSliceWeight_ValueChanged;
		((UltraToggleEditorBase)chkIsWeight).CheckedChanged -= chkIsWeight_CheckedChanged;
		((UltraToggleEditorBase)chkIsWeight).Checked = false;
		DataView dataView = new DataView(dtAnimalPartItems);
		dataView.RowFilter = (((UltraToggleEditorBase)chkIsWeight).Checked ? " IsWeight =  1" : " IsWeight =  0");
		GlobalFunctions.FillCombo(cboItem, dataView.ToTable(), "ItemID", "Name");
		((UltraToggleEditorBase)chkIsWeight).CheckedChanged += chkIsWeight_CheckedChanged;
		((Control)(object)txtSliceWeight).Text = "0";
		cboItem.SelectedIndex = -1;
		cboBatchNo.SelectedIndex = -1;
		cboSourceStore.SelectedIndex = -1;
		cboDestinationStore.SelectedIndex = -1;
		cboUnitGroup.SelectedIndex = ((((DisposableObjectCollectionBase)cboUnitGroup.Items).Count <= 0) ? (-1) : 0);
		cboUnit.SelectedIndex = -1;
		cboSupplier.SelectedIndex = -1;
		((Control)(object)txtUnitPrice).Text = "0";
		((Control)(object)txtEstimatedWastePercentage).Text = "0";
		((Control)(object)txtActualWastePercentage).Text = "0";
		((TextEditorControlBase)txtNotes).Clear();
		dtpDate.ValueChanged += dtpDate_ValueChanged;
		((TextEditorControlBase)cboItem).ValueChanged += cboItem_ValueChanged;
		((TextEditorControlBase)cboBatchNo).ValueChanged += cboBatchNo_ValueChanged;
		((TextEditorControlBase)cboUnit).ValueChanged += cboUnit_ValueChanged;
		((TextEditorControlBase)txtSliceWeight).ValueChanged += txtSliceWeight_ValueChanged;
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
		if (((Control)(object)txtSliceWeight).Text.Trim() == "" || decimal.Parse(((Control)(object)txtSliceWeight).Text) <= 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال وزن الشريحة" : "Please Enter The Slice Weight");
			((TextEditorControlBase)txtSliceWeight).Focus();
			return false;
		}
		if (cboItem.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار صنف" : "Please Select Item");
			((TextEditorControlBase)cboItem).Focus();
			cboItem.DropDown();
			return false;
		}
		if (cboBatchNo.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار رقم التشغيلة" : "Please Select Batch No");
			((TextEditorControlBase)cboBatchNo).Focus();
			cboBatchNo.DropDown();
			return false;
		}
		if (cboSourceStore.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار من مخزن" : "Please Select Source Store");
			((TextEditorControlBase)cboSourceStore).Focus();
			cboSourceStore.DropDown();
			return false;
		}
		if (cboDestinationStore.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الى مخزن" : "Please Select To Store");
			((TextEditorControlBase)cboDestinationStore).Focus();
			cboDestinationStore.DropDown();
			return false;
		}
		if (cboUnitGroup.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار مجموعة الوحدة" : "Please Select Unit Group");
			((TextEditorControlBase)cboUnitGroup).Focus();
			cboUnitGroup.DropDown();
			return false;
		}
		if (cboUnit.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الوحدة" : "Please Select Unit");
			((TextEditorControlBase)cboUnit).Focus();
			cboUnit.DropDown();
			return false;
		}
		if (((Control)(object)txtUnitPrice).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال سعر الوحدة" : "Please Enter Unit Price");
			((TextEditorControlBase)txtUnitPrice).Focus();
			return false;
		}
		if (((Control)(object)txtEstimatedWastePercentage).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال نسبة الهالك المتوقعة" : "Please Enter Estimated Waste Percentage");
			((TextEditorControlBase)txtEstimatedWastePercentage).Focus();
			return false;
		}
		if (((Control)(object)txtActualWastePercentage).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال نسبة الهالك الفعلية" : "Please Enter Actual Waste Percentage");
			((TextEditorControlBase)txtActualWastePercentage).Focus();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("SCR_Slicing", "SlicingNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["SlicingNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = BusinessLayer.StockControl.Slicing.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
			if (bool.Parse(dtMeatType.Select(" MeatTypeID = " + ((UltraGridBase)ULGData).Rows[i].Cells["MeatTypeID"].Value.ToString())[0]["MarketPrice"].ToString()) && decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء برجاء إدخال سعر الوحدة  ", "Please Enter unit price");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"];
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("برجاء إختيار إسم الصنف  ", "Please Select Item Name ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Qty"].Value.ToString()) <= 0m)
				{
					GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Qty"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitID"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("برجاء إختيار إسم الوحدة  ", "Please Select Unit Name");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitID"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; k++)
				{
					if (k != j && ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["ItemID"].Value.ToString())
					{
						GlobalVariables.InformationMB.Show("لا يمكن تكرار الصنف ", "Cannot Duplicate The Same Item ");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"];
						return false;
					}
				}
			}
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='SlicingDiffAccount' ")[0]["AccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب فروق التشريح من حسابات النظام  ", "Please Select Slicing Diff Account From SystemAccounts ");
			return false;
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = BusinessLayer.StockControl.Slicing.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkIsWeight).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsWeight).Checked ? ((Control)(object)txtSliceWeight).Text : "1", ((Control)(object)txtSliceWeight).Text, ((TextEditorControlBase)cboSourceStore).Value.ToString(), ((TextEditorControlBase)cboDestinationStore).Value.ToString(), ((TextEditorControlBase)cboItem).Value.ToString(), ((TextEditorControlBase)cboBatchNo).Value.ToString(), ((TextEditorControlBase)cboUnitGroup).Value.ToString(), ((TextEditorControlBase)cboUnit).Value.ToString(), ((Control)(object)txtUnitPrice).Text, (cboSupplier.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSupplier).Value.ToString(), (((Control)(object)txtEstimatedWastePercentage).Text == "") ? "Null" : ((Control)(object)txtEstimatedWastePercentage).Text, (((Control)(object)txtActualWastePercentage).Text == "") ? "Null" : ((Control)(object)txtActualWastePercentage).Text, ((Control)(object)txtNotes).Text, "0", "Null", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				int num2 = SlicingMeat.Insert_Update("-1", num.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["MeatTypeID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["EstimatedPercentage"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["ActualPercentage"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
				{
					SlicingMeatDetails.Insert_Update("-1", num.ToString(), num2.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value.ToString(), bool.Parse(dtMeatTypeItems.Select("ItemID =" + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) ? ((TextEditorControlBase)cboBatchNo).Value.ToString() : "Null", ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Qty"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitID"].Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
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
			int num = BusinessLayer.StockControl.Slicing.Insert_Update(drMaster["SlicingID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkIsWeight).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsWeight).Checked ? ((Control)(object)txtSliceWeight).Text : "1", ((Control)(object)txtSliceWeight).Text, ((TextEditorControlBase)cboSourceStore).Value.ToString(), ((TextEditorControlBase)cboDestinationStore).Value.ToString(), ((TextEditorControlBase)cboItem).Value.ToString(), ((TextEditorControlBase)cboBatchNo).Value.ToString(), ((TextEditorControlBase)cboUnitGroup).Value.ToString(), ((TextEditorControlBase)cboUnit).Value.ToString(), ((Control)(object)txtUnitPrice).Text, (cboSupplier.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSupplier).Value.ToString(), (((Control)(object)txtEstimatedWastePercentage).Text == "") ? "Null" : ((Control)(object)txtEstimatedWastePercentage).Text, (((Control)(object)txtActualWastePercentage).Text == "") ? "Null" : ((Control)(object)txtActualWastePercentage).Text, ((Control)(object)txtNotes).Text, bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", (drMaster["StockControlJVID"] == DBNull.Value) ? "Null" : drMaster["StockControlJVID"].ToString(), "1", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			SlicingMeatDetails.DeleteBySlicingID(num.ToString(), GlobalVariables.UserID);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["SlicingMeatID"].Value.ToString() + ",";
			}
			Main.DeleteForUpdate("SCR_SlicingMeat", "SlicingID", drMaster["SlicingID"].ToString(), "SlicingMeatID", text);
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				int num2 = SlicingMeat.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[j].Cells["SlicingMeatID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[j].Cells["SlicingMeatID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[j].Cells["SlicingMeatID"].Value.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["MeatTypeID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["EstimatedPercentage"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["ActualPercentage"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows).Count; k++)
				{
					SlicingMeatDetails.Insert_Update("-1", num.ToString(), num2.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ItemID"].Value.ToString(), bool.Parse(dtMeatTypeItems.Select("ItemID =" + ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) ? ((TextEditorControlBase)cboBatchNo).Value.ToString() : "Null", ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["Qty"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["UnitID"].Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
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
			if (drMaster["StockControlJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["StockControlJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			SlicingMeatDetails.DeleteVirtualBySlicingID(drMaster["SlicingID"].ToString(), GlobalVariables.UserID);
			SlicingMeat.DeleteVirtualBySlicingID(drMaster["SlicingID"].ToString(), GlobalVariables.UserID);
			BusinessLayer.StockControl.Slicing.DeleteVirtual(drMaster["SlicingID"].ToString(), GlobalVariables.UserID);
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

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.SlicingReport(-1);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["SlicingID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		dtAnimalPartItems = AnimalParts.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboItem, dtAnimalPartItems, "ItemID", "Name");
		dtMeatTypeItems = MeatTypesDetails.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		MeatTypeItems.ValueListItems.Clear();
		for (int i = 0; i < dtMeatTypeItems.Rows.Count; i++)
		{
			MeatTypeItems.ValueListItems.Add(dtMeatTypeItems.Rows[i]["ItemID"], dtMeatTypeItems.Rows[i]["Name"].ToString());
		}
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSourceStore, dtStores, "StoreID", "StoreName");
		GlobalFunctions.FillCombo(cboDestinationStore, dtStores, "StoreID", "StoreName");
		dtSuppliers = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.SupplierSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSupplier, dtSuppliers, "SubAccountID", "SubAccountName");
		dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
		GlobalFunctions.FillCombo(cboBatchNo, dtBatchs, "BatchID", "BatchName");
		dtUnitGroup = UnitsTypes.FillCombo("-1", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboUnitGroup, dtUnitGroup, "UnitTypeID", "UnitTypeName");
		dtUnits = Units.FillComboByWeight("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboUnit, dtUnits, "UnitID", "UnitName");
		vlUnits.ValueListItems.Clear();
		for (int j = 0; j < dtUnits.Rows.Count; j++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[j]["UnitID"], dtUnits.Rows[j]["UnitName"].ToString());
		}
		dtMeatType = MeatTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlMeatType.ValueListItems.Clear();
		for (int k = 0; k < dtMeatType.Rows.Count; k++)
		{
			vlMeatType.ValueListItems.Add(dtMeatType.Rows[k]["MeatTypeID"], dtMeatType.Rows[k]["MeatTypeName"].ToString());
		}
	}

	private void cboItem_ValueChanged(object sender, EventArgs e)
	{
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Expected O, but got Unknown
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Expected O, but got Unknown
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Expected O, but got Unknown
		if (cboItem.SelectedIndex > -1)
		{
			dtAvailableBatches = BusinessLayer.StockControl.Slicing.AvailableBatches(((TextEditorControlBase)cboItem).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate));
			GlobalFunctions.FillCombo(cboBatchNo, dtAvailableBatches, "BatchID", "BatchName");
			((Control)(object)txtEstimatedWastePercentage).Text = dtAnimalPartItems.Select(" ItemID = " + ((TextEditorControlBase)cboItem).Value.ToString())[0]["WastePercentage"].ToString();
			dtAnimalPartDetails = AnimalPartsDetails.SelectByItemID(((TextEditorControlBase)cboItem).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			((DataSet)((UltraGridBase)ULGData).DataSource).Tables[1].Rows.Clear();
			((DataSet)((UltraGridBase)ULGData).DataSource).Tables[0].Rows.Clear();
			ULGData.InitializeRow -= new InitializeRowEventHandler(ULGData_InitializeRow);
			((Control)(object)ULGData).Enter -= ULGData_Enter;
			for (int i = 0; i < dtAnimalPartDetails.Rows.Count; i++)
			{
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).ActiveRow.Cells["MeatTypeID"].Value = dtAnimalPartDetails.Rows[i]["MeatTypeID"];
				((UltraGridBase)ULGData).ActiveRow.Cells["EstimatedPercentage"].Value = dtAnimalPartDetails.Rows[i]["Percentage"];
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
			ULGData.InitializeRow += new InitializeRowEventHandler(ULGData_InitializeRow);
			((Control)(object)ULGData).Enter += ULGData_Enter;
		}
	}

	public decimal GetMeatTypePrice(string MeatTypeID)
	{
		decimal result = default(decimal);
		if (bool.Parse(dtMeatType.Select(" MeatTypeID = " + MeatTypeID)[0]["WithoutPrice"].ToString()))
		{
			result = default(decimal);
		}
		else if (bool.Parse(dtMeatType.Select(" MeatTypeID = " + MeatTypeID)[0]["MarketPrice"].ToString()))
		{
			result = default(decimal);
		}
		else if (bool.Parse(dtMeatType.Select(" MeatTypeID = " + MeatTypeID)[0]["Integral"].ToString()))
		{
			result = default(decimal);
		}
		else if (bool.Parse(dtMeatType.Select(" MeatTypeID = " + MeatTypeID)[0]["IsPercentage"].ToString()) && ((Control)(object)txtSliceWeight).Text != "" && decimal.Parse(((Control)(object)txtSliceWeight).Text) > 0m)
		{
			return decimal.Parse(dtMeatType.Select(" MeatTypeID = " + MeatTypeID.ToString())[0]["PercentageValue"].ToString()) * decimal.Parse(((Control)(object)txtUnitPrice).Text) / decimal.Parse(((Control)(object)txtSliceWeight).Text) / 100m;
		}
		return result;
	}

	public override void btnPrintClick()
	{
		if (RowID != "")
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_SCR_Slicing_A.rpt" : "Rep_SCR_Slicing_E.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@SlicingIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	private void ULGData_InitializeRow(object sender, InitializeRowEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		ULGData.InitializeRow -= new InitializeRowEventHandler(ULGData_InitializeRow);
		if (((GridItemBase)e.Row).Band.Index == 1 && e.Row.ParentRow.Cells["MeatTypeID"].Value != DBNull.Value)
		{
			e.Row.Cells["ItemID"].ValueList = (IValueList)(object)getItemValueList(int.Parse(e.Row.ParentRow.Cells["MeatTypeID"].Value.ToString()));
		}
		ULGData.InitializeRow += new InitializeRowEventHandler(ULGData_InitializeRow);
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
		{
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "MeatTypeID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "EstimatedPercentage" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ActualPercentage" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice")
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" && ((UltraGridBase)ULGData).ActiveRow.Cells["MeatTypeID"].Value != DBNull.Value && !bool.Parse(dtMeatType.Select(" MeatTypeID = " + ((UltraGridBase)ULGData).ActiveRow.Cells["MeatTypeID"].Value.ToString())[0]["MarketPrice"].ToString()))
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			else if (((UltraGridBase)ULGData).ActiveRow.Cells["MeatTypeID"].Value == DBNull.Value)
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
		}
		else if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1 && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((GridItemBase)e.Row).Band.Index == 0)
		{
			e.Row.Cells["SlicingMeatID"].Value = ++newID;
		}
		else if (((GridItemBase)e.Row).Band.Index == 1)
		{
			e.Row.Cells["SlicingMeatDetailID"].Value = ++newID;
			e.Row.Cells["UnitID"].Value = ((cboUnit.SelectedIndex == -1) ? DBNull.Value : ((TextEditorControlBase)cboUnit).Value);
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).UpdateData();
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1)
		{
			if (((KeyedSubObjectBase)e.Cell.Column).Key == "Qty" || ((KeyedSubObjectBase)e.Cell.Column).Key == "UnitPrice")
			{
				CalculateParentRow(((UltraGridBase)ULGData).ActiveRow.ParentRow);
				CalculateIntegralAndActualWaste();
			}
		}
		else if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0 && ((KeyedSubObjectBase)e.Cell.Column).Key == "UnitPrice")
		{
			CalculateParentRow(((UltraGridBase)ULGData).ActiveRow);
			CalculateIntegralAndActualWaste();
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void chkIsWeight_CheckedChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtAnimalPartItems);
		dataView.RowFilter = (((UltraToggleEditorBase)chkIsWeight).Checked ? " IsWeight =  1" : " IsWeight =  0");
		GlobalFunctions.FillCombo(cboItem, dataView.ToTable(), "ItemID", "Name");
	}

	private ValueList getItemValueList(int MeatTypeID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtMeatTypeItems.Select("MeattypeID=" + MeatTypeID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["ItemID"].ToString(), array[i]["Name"].ToString());
		}
		return val;
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (cboItem.SelectedIndex > -1)
		{
			dtAvailableBatches = BusinessLayer.StockControl.Slicing.AvailableBatches(((TextEditorControlBase)cboItem).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate));
			GlobalFunctions.FillCombo(cboBatchNo, dtAvailableBatches, "BatchID", "BatchName");
			((Control)(object)txtEstimatedWastePercentage).Text = dtAnimalPartItems.Select(" ItemID = " + ((TextEditorControlBase)cboItem).Value.ToString())[0]["WastePercentage"].ToString();
		}
		if (Adding)
		{
			((Control)(object)txtCode).Text = BusinessLayer.StockControl.Slicing.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void cboBatchNo_ValueChanged(object sender, EventArgs e)
	{
		if (cboBatchNo.SelectedIndex <= -1 || dtAvailableBatches.Rows.Count <= 0)
		{
			return;
		}
		UnitPrice = decimal.Parse(dtAvailableBatches.Rows[cboBatchNo.SelectedIndex]["UnitPrice"].ToString());
		((TextEditorControlBase)cboSourceStore).Value = dtAvailableBatches.Rows[cboBatchNo.SelectedIndex]["StoreID"];
		((TextEditorControlBase)cboSupplier).Value = dtAvailableBatches.Rows[cboBatchNo.SelectedIndex]["SubAccountID"];
		((Control)(object)txtUnitPrice).Text = (((UltraToggleEditorBase)chkIsWeight).Checked ? (UnitPrice * decimal.Parse(((Control)(object)txtSliceWeight).Text)).ToString() : dtAvailableBatches.Rows[cboBatchNo.SelectedIndex]["UnitPrice"].ToString());
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["MeatTypeID"].Value != DBNull.Value && decimal.Parse(((Control)(object)txtUnitPrice).Text) > 0m)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = GetMeatTypePrice(((UltraGridBase)ULGData).Rows[i].Cells["MeatTypeID"].Value.ToString());
			}
		}
	}

	public void CalculateParentRow(UltraGridRow ParentRow)
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)ParentRow.ChildBands[0].Rows).Count; i++)
		{
			num += decimal.Parse(ParentRow.ChildBands[0].Rows[i].Cells["Qty"].Value.ToString());
		}
		ParentRow.Cells["TotalPrice"].Value = num * decimal.Parse(ParentRow.Cells["UnitPrice"].Value.ToString());
		if (((Control)(object)txtSliceWeight).Text != "" && decimal.Parse(((Control)(object)txtSliceWeight).Text) > 0m)
		{
			ParentRow.Cells["ActualPercentage"].Value = num / decimal.Parse(((Control)(object)txtSliceWeight).Text) * 100m;
		}
	}

	public void CalculateIntegralAndActualWaste()
	{
		if (!(((Control)(object)txtSliceWeight).Text != "") || !(decimal.Parse(((Control)(object)txtSliceWeight).Text) > 0m) || !(((Control)(object)txtUnitPrice).Text != "") || !(decimal.Parse(((Control)(object)txtUnitPrice).Text) > 0m))
		{
			return;
		}
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		decimal num3 = default(decimal);
		decimal num4 = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (!bool.Parse(dtMeatType.Select(" MeatTypeID = " + ((UltraGridBase)ULGData).Rows[i].Cells["MeatTypeID"].Value.ToString())[0]["Integral"].ToString()))
			{
				num3 += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
			}
			num2 += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ActualPercentage"].Value.ToString());
		}
		num = decimal.Parse(((Control)(object)txtUnitPrice).Text);
		((Control)(object)txtActualWastePercentage).Text = (100m - num2).ToString();
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
		{
			if (bool.Parse(dtMeatType.Select(" MeatTypeID = " + ((UltraGridBase)ULGData).Rows[j].Cells["MeatTypeID"].Value.ToString())[0]["Integral"].ToString()))
			{
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows).Count; k++)
				{
					num4 += decimal.Parse(((UltraGridBase)ULGData).Rows[j].ChildBands[0].Rows[k].Cells["Qty"].Value.ToString());
				}
				((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value = num - num3;
				if (num4 > 0m)
				{
					((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value.ToString()) / num4;
				}
			}
		}
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void cboUnit_ValueChanged(object sender, EventArgs e)
	{
		if (cboUnit.SelectedIndex <= -1)
		{
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitID"].Value = ((TextEditorControlBase)cboUnit).Value;
			}
		}
	}

	private void txtSliceWeight_ValueChanged(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((Control)(object)txtUnitPrice).Text = (((UltraToggleEditorBase)chkIsWeight).Checked ? (UnitPrice * decimal.Parse((((Control)(object)txtSliceWeight).Text == "") ? "0" : ((Control)(object)txtSliceWeight).Text)).ToString() : ((Control)(object)txtUnitPrice).Text);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = GetMeatTypePrice(((UltraGridBase)ULGData).Rows[i].Cells["MeatTypeID"].Value.ToString());
			CalculateParentRow(((UltraGridBase)ULGData).Rows[i]);
		}
		CalculateIntegralAndActualWaste();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = GetMeatTypePrice(((UltraGridBase)ULGData).Rows[i].Cells["MeatTypeID"].Value.ToString());
			CalculateParentRow(((UltraGridBase)ULGData).Rows[i]);
		}
		CalculateIntegralAndActualWaste();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void btnRecalculate_Click(object sender, EventArgs e)
	{
		if (cboBatchNo.SelectedIndex <= -1 || cboItem.SelectedIndex <= -1)
		{
			return;
		}
		DataTable dataTable = BusinessLayer.StockControl.Slicing.SelectBatchesData(((TextEditorControlBase)cboItem).Value.ToString(), ((TextEditorControlBase)cboBatchNo).Value.ToString());
		if (dataTable.Rows.Count <= 0)
		{
			return;
		}
		UnitPrice = decimal.Parse(dataTable.Rows[0]["UnitPrice"].ToString());
		((TextEditorControlBase)cboSourceStore).Value = dataTable.Rows[0]["StoreID"];
		((TextEditorControlBase)cboSupplier).Value = dataTable.Rows[0]["SubAccountID"];
		((Control)(object)txtUnitPrice).Text = (((UltraToggleEditorBase)chkIsWeight).Checked ? (UnitPrice * decimal.Parse((((Control)(object)txtSliceWeight).Text == "") ? "0" : ((Control)(object)txtSliceWeight).Text)).ToString() : dataTable.Rows[0]["UnitPrice"].ToString());
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["MeatTypeID"].Value != DBNull.Value && decimal.Parse(((Control)(object)txtUnitPrice).Text) > 0m)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value = GetMeatTypePrice(((UltraGridBase)ULGData).Rows[i].Cells["MeatTypeID"].Value.ToString());
				CalculateParentRow(((UltraGridBase)ULGData).Rows[i]);
			}
		}
		CalculateIntegralAndActualWaste();
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
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Expected O, but got Unknown
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Expected O, but got Unknown
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Expected O, but got Unknown
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Expected O, but got Unknown
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Expected O, but got Unknown
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Expected O, but got Unknown
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Expected O, but got Unknown
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Expected O, but got Unknown
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Expected O, but got Unknown
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Expected O, but got Unknown
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Expected O, but got Unknown
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Expected O, but got Unknown
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Expected O, but got Unknown
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a3: Expected O, but got Unknown
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Expected O, but got Unknown
		//IL_05e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05eb: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.Slicing.frmSlicing));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.txtSliceWeight = new UltraTextEditor();
		this.lblSliceWeight = new UltraLabel();
		this.lblSupplier = new UltraLabel();
		this.cboSupplier = new UltraComboEditor();
		this.lblItem = new UltraLabel();
		this.cboItem = new UltraComboEditor();
		this.lblBatchNo = new UltraLabel();
		this.cboBatchNo = new UltraComboEditor();
		this.lblUnit = new UltraLabel();
		this.cboUnit = new UltraComboEditor();
		this.lblUnitGroup = new UltraLabel();
		this.cboUnitGroup = new UltraComboEditor();
		this.txtUnitPrice = new UltraTextEditor();
		this.lblUnitPrice = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.txtActualWastePercentage = new UltraTextEditor();
		this.lblActualWastePercentage = new UltraLabel();
		this.lblEstimatedWastePercentage = new UltraLabel();
		this.txtEstimatedWastePercentage = new UltraTextEditor();
		this.lblSourceStore = new UltraLabel();
		this.cboSourceStore = new UltraComboEditor();
		this.lblDestinationStore = new UltraLabel();
		this.cboDestinationStore = new UltraComboEditor();
		this.btnRecalculate = new UltraButton();
		this.chkIsWeight = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSliceWeight).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSupplier).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBatchNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnitGroup).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUnitPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtActualWastePercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEstimatedWastePercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSourceStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDestinationStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsWeight).BeginInit();
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
		base.ULGData.InitializeRow += new InitializeRowEventHandler(ULGData_InitializeRow);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
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
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.txtSliceWeight, "txtSliceWeight");
		((System.Windows.Forms.Control)(object)this.txtSliceWeight).Name = "txtSliceWeight";
		((TextEditorControlBase)this.txtSliceWeight).ValueChanged += new System.EventHandler(txtSliceWeight_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtSliceWeight).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblSliceWeight, "lblSliceWeight");
		this.lblSliceWeight.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSliceWeight).Name = "lblSliceWeight";
		((ControlBase)this.lblSliceWeight).WrapText = false;
		resources.ApplyResources(this.lblSupplier, "lblSupplier");
		this.lblSupplier.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSupplier).Name = "lblSupplier";
		((ControlBase)this.lblSupplier).WrapText = false;
		resources.ApplyResources(this.cboSupplier, "cboSupplier");
		((TextEditorControlBase)this.cboSupplier).AlwaysInEditMode = true;
		this.cboSupplier.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSupplier).Name = "cboSupplier";
		resources.ApplyResources(this.lblItem, "lblItem");
		this.lblItem.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblItem).Name = "lblItem";
		((ControlBase)this.lblItem).WrapText = false;
		resources.ApplyResources(this.cboItem, "cboItem");
		((TextEditorControlBase)this.cboItem).AlwaysInEditMode = true;
		this.cboItem.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboItem).Name = "cboItem";
		((TextEditorControlBase)this.cboItem).ValueChanged += new System.EventHandler(cboItem_ValueChanged);
		resources.ApplyResources(this.lblBatchNo, "lblBatchNo");
		this.lblBatchNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBatchNo).Name = "lblBatchNo";
		((ControlBase)this.lblBatchNo).WrapText = false;
		resources.ApplyResources(this.cboBatchNo, "cboBatchNo");
		((TextEditorControlBase)this.cboBatchNo).AlwaysInEditMode = true;
		this.cboBatchNo.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboBatchNo).Name = "cboBatchNo";
		((TextEditorControlBase)this.cboBatchNo).ValueChanged += new System.EventHandler(cboBatchNo_ValueChanged);
		resources.ApplyResources(this.lblUnit, "lblUnit");
		this.lblUnit.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUnit).Name = "lblUnit";
		((ControlBase)this.lblUnit).WrapText = false;
		resources.ApplyResources(this.cboUnit, "cboUnit");
		((TextEditorControlBase)this.cboUnit).AlwaysInEditMode = true;
		this.cboUnit.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboUnit).Name = "cboUnit";
		((TextEditorControlBase)this.cboUnit).ValueChanged += new System.EventHandler(cboUnit_ValueChanged);
		resources.ApplyResources(this.lblUnitGroup, "lblUnitGroup");
		this.lblUnitGroup.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUnitGroup).Name = "lblUnitGroup";
		((ControlBase)this.lblUnitGroup).WrapText = false;
		resources.ApplyResources(this.cboUnitGroup, "cboUnitGroup");
		((TextEditorControlBase)this.cboUnitGroup).AlwaysInEditMode = true;
		this.cboUnitGroup.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboUnitGroup).Name = "cboUnitGroup";
		resources.ApplyResources(this.txtUnitPrice, "txtUnitPrice");
		((System.Windows.Forms.Control)(object)this.txtUnitPrice).Name = "txtUnitPrice";
		resources.ApplyResources(this.lblUnitPrice, "lblUnitPrice");
		this.lblUnitPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUnitPrice).Name = "lblUnitPrice";
		((ControlBase)this.lblUnitPrice).WrapText = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.txtActualWastePercentage, "txtActualWastePercentage");
		((System.Windows.Forms.Control)(object)this.txtActualWastePercentage).Name = "txtActualWastePercentage";
		((System.Windows.Forms.Control)(object)this.txtActualWastePercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblActualWastePercentage, "lblActualWastePercentage");
		this.lblActualWastePercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblActualWastePercentage).Name = "lblActualWastePercentage";
		((ControlBase)this.lblActualWastePercentage).WrapText = false;
		resources.ApplyResources(this.lblEstimatedWastePercentage, "lblEstimatedWastePercentage");
		this.lblEstimatedWastePercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEstimatedWastePercentage).Name = "lblEstimatedWastePercentage";
		((ControlBase)this.lblEstimatedWastePercentage).WrapText = false;
		resources.ApplyResources(this.txtEstimatedWastePercentage, "txtEstimatedWastePercentage");
		((System.Windows.Forms.Control)(object)this.txtEstimatedWastePercentage).Name = "txtEstimatedWastePercentage";
		resources.ApplyResources(this.lblSourceStore, "lblSourceStore");
		this.lblSourceStore.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSourceStore).Name = "lblSourceStore";
		((ControlBase)this.lblSourceStore).WrapText = false;
		resources.ApplyResources(this.cboSourceStore, "cboSourceStore");
		((TextEditorControlBase)this.cboSourceStore).AlwaysInEditMode = true;
		this.cboSourceStore.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSourceStore).Name = "cboSourceStore";
		resources.ApplyResources(this.lblDestinationStore, "lblDestinationStore");
		this.lblDestinationStore.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDestinationStore).Name = "lblDestinationStore";
		((ControlBase)this.lblDestinationStore).WrapText = false;
		resources.ApplyResources(this.cboDestinationStore, "cboDestinationStore");
		((TextEditorControlBase)this.cboDestinationStore).AlwaysInEditMode = true;
		this.cboDestinationStore.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboDestinationStore).Name = "cboDestinationStore";
		resources.ApplyResources(this.btnRecalculate, "btnRecalculate");
		((AppearanceBase)val9).Image = resources.GetObject("appearance10.Image");
		resources.ApplyResources(val9, "appearance10");
		((ControlBase)this.btnRecalculate).Appearance = (AppearanceBase)(object)val9;
		((ControlBase)this.btnRecalculate).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnRecalculate).Name = "btnRecalculate";
		((System.Windows.Forms.Control)(object)this.btnRecalculate).Click += new System.EventHandler(btnRecalculate_Click);
		resources.ApplyResources(this.chkIsWeight, "chkIsWeight");
		((System.Windows.Forms.Control)(object)this.chkIsWeight).Name = "chkIsWeight";
		((UltraToggleEditorBase)this.chkIsWeight).CheckedChanged += new System.EventHandler(chkIsWeight_CheckedChanged);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnRecalculate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSourceStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSourceStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDestinationStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboDestinationStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEstimatedWastePercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtActualWastePercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblActualWastePercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEstimatedWastePercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtUnitPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUnitPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUnitGroup);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboUnitGroup);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUnit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboUnit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBatchNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBatchNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblItem);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboItem);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSupplier);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSupplier);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSliceWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSliceWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmSlicing";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSliceWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSliceWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSupplier, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSupplier, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBatchNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBatchNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboUnit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUnit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboUnitGroup, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUnitGroup, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUnitPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtUnitPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEstimatedWastePercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblActualWastePercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtActualWastePercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEstimatedWastePercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboDestinationStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDestinationStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSourceStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSourceStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnRecalculate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsWeight, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSliceWeight).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSupplier).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBatchNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnitGroup).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUnitPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtActualWastePercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEstimatedWastePercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSourceStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDestinationStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsWeight).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
