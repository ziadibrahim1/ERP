using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Constructions;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.Constructions.MasterData;

public class frmBuildings : frmHeaderManyDetails
{
	private DataTable dtProjects;

	private DataTable dtUnitTypes;

	private DataTable dtbuildingUnits;

	private DataTable dtFloors;

	private ValueList vlUnitType = new ValueList();

	private bool HasContract = false;

	private IContainer components = null;

	private UltraLabel lblProject;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraComboEditor cboProject;

	private UltraTabPageControl ultraTabPageControl2;

	protected internal UltraGrid ULGDataUnits;

	private UltraLabel lblNameEn;

	private UltraTextEditor txtNameEn;

	private UltraLabel lblNameAr;

	private UltraTextEditor txtNameAr;

	private UltraLabel lblSize;

	private UltraTextEditor txtSize;

	private UltraLabel lblSampleCount;

	private UltraTextEditor txtSampleCount;

	private UltraLabel lblFloorCount;

	private UltraTextEditor txtFloorCount;

	private UltraLabel lblBalanceCount;

	private UltraTextEditor txtBalanceCount;

	private UltraLabel lblStoresCount;

	private UltraTextEditor txtStoreCount;

	private UltraLabel lblParkingCount;

	private UltraTextEditor txtParkingCount;

	private UltraLabel lblVillaCount;

	private UltraTextEditor txtVillaCount;

	public frmBuildings()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		TableName = "Con_Buildings";
		IDCol = "BuildingID";
		NoCol = "BuildingCode";
		DateCol = "GetDate()";
	}

	public frmBuildings(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtProjects = Projects.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboProject, dtProjects, "ProjectID", "ProjectName");
		dtFloors = Floors.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtUnitTypes = UnitsTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlUnitType.ValueListItems.Clear();
		for (int i = 0; i < dtUnitTypes.Rows.Count; i++)
		{
			vlUnitType.ValueListItems.Add(dtUnitTypes.Rows[i]["UnitTypeID"], dtUnitTypes.Rows[i]["UnitTypeName"].ToString());
		}
		dtDetails = BuildingsSamples.SelectByBuildingID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtbuildingUnits = BuildingsUnits.SelectByBuildingIDAndUnitTypeIDs("0", ",2,3,4,5,", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGDataUnits).DataSource = dtbuildingUnits;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		GlobalFunctions.PrepareGrid(ULGDataUnits);
		((UltraGridBase)ULGDataUnits).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGDataUnits).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BuildingSampleID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BuildingSampleCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Size"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Price"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RoomsCount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BuildingSampleCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود" : "Code");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Size"].Header).Caption = (GlobalVariables.IsArabic ? "مساحة" : "Size");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Price"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RoomsCount"].Header).Caption = (GlobalVariables.IsArabic ? "عدد الغرف" : "Rooms Count");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BuildingSampleCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Size"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Price"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RoomsCount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Price"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Size"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RoomsCount"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataUnits).DisplayLayout.Bands[0].Columns["BuildingUnitID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataUnits).DisplayLayout.Bands[0].Columns["BuildingUnitCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataUnits).DisplayLayout.Bands[0].Columns["UnitTypeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGDataUnits).DisplayLayout.Bands[0].Columns["Size"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataUnits).DisplayLayout.Bands[0].Columns["Price"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGDataUnits).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5);
		((HeaderBase)((UltraGridBase)ULGDataUnits).DisplayLayout.Bands[0].Columns["BuildingUnitCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود" : "Code");
		((HeaderBase)((UltraGridBase)ULGDataUnits).DisplayLayout.Bands[0].Columns["UnitTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الوحدة" : "Unit Type");
		((HeaderBase)((UltraGridBase)ULGDataUnits).DisplayLayout.Bands[0].Columns["Size"].Header).Caption = (GlobalVariables.IsArabic ? "مساحة" : "Size");
		((HeaderBase)((UltraGridBase)ULGDataUnits).DisplayLayout.Bands[0].Columns["Price"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
		((HeaderBase)((UltraGridBase)ULGDataUnits).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGDataUnits).DisplayLayout.Bands[0].Columns["BuildingUnitCode"].Hidden = false;
		((UltraGridBase)ULGDataUnits).DisplayLayout.Bands[0].Columns["UnitTypeID"].Hidden = false;
		((UltraGridBase)ULGDataUnits).DisplayLayout.Bands[0].Columns["Size"].Hidden = false;
		((UltraGridBase)ULGDataUnits).DisplayLayout.Bands[0].Columns["Price"].Hidden = false;
		((UltraGridBase)ULGDataUnits).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataUnits).DisplayLayout.Bands[0].Columns["UnitTypeID"].ValueList = (IValueList)(object)vlUnitType;
		((UltraGridBase)ULGDataUnits).DisplayLayout.Bands[0].Columns["Price"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataUnits).DisplayLayout.Bands[0].Columns["Size"].DefaultCellValue = 0;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = Buildings.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
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
			((Control)(object)txtCode).Text = drMaster["BuildingCode"].ToString();
			((Control)(object)txtNameAr).Text = drMaster["buildingNameAr"].ToString();
			((Control)(object)txtNameEn).Text = drMaster["buildingNameEn"].ToString();
			((TextEditorControlBase)cboProject).Value = drMaster["ProjectID"];
			((Control)(object)txtSize).Text = drMaster["Size"].ToString();
			((TextEditorControlBase)txtSampleCount).ValueChanged -= txtSampleCount_ValueChanged;
			((Control)(object)txtSampleCount).Text = drMaster["SamplesCount"].ToString();
			((TextEditorControlBase)txtSampleCount).ValueChanged += txtSampleCount_ValueChanged;
			((TextEditorControlBase)txtParkingCount).ValueChanged -= txtParkingCount_ValueChanged;
			((Control)(object)txtParkingCount).Text = drMaster["ParkingCount"].ToString();
			((TextEditorControlBase)txtParkingCount).ValueChanged += txtParkingCount_ValueChanged;
			((TextEditorControlBase)txtStoreCount).ValueChanged -= txtStoreCount_ValueChanged;
			((Control)(object)txtStoreCount).Text = drMaster["StoresCount"].ToString();
			((TextEditorControlBase)txtStoreCount).ValueChanged += txtStoreCount_ValueChanged;
			((TextEditorControlBase)txtBalanceCount).ValueChanged -= txtBalanceCount_ValueChanged;
			((Control)(object)txtBalanceCount).Text = drMaster["BalancesCount"].ToString();
			((TextEditorControlBase)txtBalanceCount).ValueChanged += txtBalanceCount_ValueChanged;
			((TextEditorControlBase)txtVillaCount).ValueChanged -= txtVillaCount_ValueChanged;
			((Control)(object)txtVillaCount).Text = drMaster["VillaCount"].ToString();
			((TextEditorControlBase)txtVillaCount).ValueChanged += txtVillaCount_ValueChanged;
			((Control)(object)txtFloorCount).Text = drMaster["FloorCount"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			HasContract = Buildings.CheckContract(drMaster["BuildingID"].ToString(), IsFromServer: true);
			dtDetails = BuildingsSamples.SelectByBuildingID(drMaster["BuildingID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtbuildingUnits = BuildingsUnits.SelectByBuildingIDAndUnitTypeIDs(drMaster["BuildingID"].ToString(), ",2,3,4,5,", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			((UltraGridBase)ULGData).DataSource = dtDetails;
			((UltraGridBase)ULGDataUnits).DataSource = dtbuildingUnits;
			InitGrid();
		}
		else
		{
			ClearControls();
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)txtCode).ReadOnly = Adding || Updating;
		((EditorButtonControlBase)txtNameAr).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNameEn).ReadOnly = NavMode;
		((EditorButtonControlBase)cboProject).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSize).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSampleCount).ReadOnly = NavMode || HasContract;
		((EditorButtonControlBase)txtParkingCount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBalanceCount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtVillaCount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtFloorCount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((Control)(object)txtCode).Text = (Adding ? Buildings.GetCode(IsFromServer: true) : "");
		((TextEditorControlBase)txtNameAr).Clear();
		((TextEditorControlBase)txtNameEn).Clear();
		cboProject.SelectedIndex = -1;
		((Control)(object)txtSize).Text = "0";
		((TextEditorControlBase)txtSampleCount).ValueChanged -= txtSampleCount_ValueChanged;
		((Control)(object)txtSampleCount).Text = "0";
		((TextEditorControlBase)txtSampleCount).ValueChanged += txtSampleCount_ValueChanged;
		((TextEditorControlBase)txtParkingCount).ValueChanged -= txtParkingCount_ValueChanged;
		((Control)(object)txtParkingCount).Text = "0";
		((TextEditorControlBase)txtParkingCount).ValueChanged += txtParkingCount_ValueChanged;
		((TextEditorControlBase)txtStoreCount).ValueChanged -= txtStoreCount_ValueChanged;
		((Control)(object)txtStoreCount).Text = "0";
		((TextEditorControlBase)txtStoreCount).ValueChanged += txtStoreCount_ValueChanged;
		((TextEditorControlBase)txtBalanceCount).ValueChanged -= txtBalanceCount_ValueChanged;
		((Control)(object)txtBalanceCount).Text = "0";
		((TextEditorControlBase)txtBalanceCount).ValueChanged += txtBalanceCount_ValueChanged;
		((TextEditorControlBase)txtVillaCount).ValueChanged -= txtVillaCount_ValueChanged;
		((Control)(object)txtVillaCount).Text = "0";
		((TextEditorControlBase)txtVillaCount).ValueChanged += txtVillaCount_ValueChanged;
		((Control)(object)txtFloorCount).Text = "0";
		((TextEditorControlBase)txtNotes).Clear();
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataUnits).Rows).Count > 0)
		{
			((DataTable)((UltraGridBase)ULGDataUnits).DataSource).Rows.Clear();
		}
		HasContract = false;
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم المبنى" : "Please Enter The Building Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtSampleCount).Text.Trim() == "" || int.Parse(((Control)(object)txtSampleCount).Text) <= 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال عدد النماذج" : "Please Enter Samples Count");
			((TextEditorControlBase)txtSampleCount).Focus();
			return false;
		}
		if (((Control)(object)txtFloorCount).Text.Trim() == "" || int.Parse(((Control)(object)txtFloorCount).Text) <= 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال عدد الادوار" : "Please Enter Floor Count");
			((TextEditorControlBase)txtFloorCount).Focus();
			return false;
		}
		if (cboProject.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار المشروع" : "Please Select Project");
			((TextEditorControlBase)cboProject).Focus();
			return false;
		}
		if (Main.CheckForValue("Con_Buildings", "BuildingCode", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["BuildingCode"].ToString(), IsFromServer: true) > 0)
		{
			string code = Buildings.GetCode(IsFromServer: true);
			GlobalVariables.QuestionMB.Show("رقم هذا المبنى متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Voucher Number Already Exists It Will Be Saved With No. : " + code);
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
			((UltraTabControlBase)UTCDetails).Tabs[0].Selected = true;
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["Size"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Size"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال  المساحة  ", "Please Enter Size");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Size"];
				((UltraTabControlBase)UTCDetails).Tabs[0].Selected = true;
				((UltraGridBase)ULGData).Rows[i].Cells["Size"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["Price"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Price"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال  السعر  ", "Please Enter Price");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Price"];
				((UltraTabControlBase)UTCDetails).Tabs[0].Selected = true;
				((UltraGridBase)ULGData).Rows[i].Cells["Price"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["RoomsCount"].Value == DBNull.Value || int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["RoomsCount"].Value.ToString()) <= 0)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال  عدد الغرف  ", "Please Enter Rooms Count");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["RoomsCount"];
				((UltraTabControlBase)UTCDetails).Tabs[0].Selected = true;
				((UltraGridBase)ULGData).Rows[i].Cells["RoomsCount"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataUnits).Rows).Count; j++)
		{
			if (((UltraGridBase)ULGDataUnits).Rows[j].Cells["Size"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGDataUnits).Rows[j].Cells["Size"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال  المساحة  ", "Please Enter Size");
				ULGDataUnits.ActiveCell = ((UltraGridBase)ULGDataUnits).Rows[j].Cells["Size"];
				((UltraTabControlBase)UTCDetails).Tabs[1].Selected = true;
				((UltraGridBase)ULGDataUnits).Rows[j].Cells["Size"].DroppedDown = true;
				return false;
			}
			if (((UltraGridBase)ULGDataUnits).Rows[j].Cells["Price"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGDataUnits).Rows[j].Cells["Price"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال  السعر  ", "Please Enter Price");
				ULGDataUnits.ActiveCell = ((UltraGridBase)ULGDataUnits).Rows[j].Cells["Price"];
				((UltraTabControlBase)UTCDetails).Tabs[1].Selected = true;
				((UltraGridBase)ULGDataUnits).Rows[j].Cells["Price"].DroppedDown = true;
				return false;
			}
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = Buildings.Insert_Update("-1", ((Control)(object)txtCode).Text, (((Control)(object)txtNameAr).Text == "") ? "Null" : ((Control)(object)txtNameAr).Text, (((Control)(object)txtNameEn).Text == "") ? "Null" : ((Control)(object)txtNameEn).Text, (cboProject.SelectedIndex > -1) ? ((TextEditorControlBase)cboProject).Value.ToString() : "Null", (((Control)(object)txtSize).Text == "") ? "0" : ((Control)(object)txtSize).Text, (((Control)(object)txtSampleCount).Text == "") ? "0" : ((Control)(object)txtSampleCount).Text, (((Control)(object)txtParkingCount).Text == "") ? "0" : ((Control)(object)txtParkingCount).Text, (((Control)(object)txtStoreCount).Text == "") ? "0" : ((Control)(object)txtStoreCount).Text, (((Control)(object)txtBalanceCount).Text == "") ? "0" : ((Control)(object)txtBalanceCount).Text, (((Control)(object)txtFloorCount).Text == "") ? "0" : ((Control)(object)txtFloorCount).Text, (((Control)(object)txtVillaCount).Text == "") ? "0" : ((Control)(object)txtVillaCount).Text, ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["BuildingSampleID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["BuildingID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataUnits).Rows).Count; j++)
			{
				((UltraGridBase)ULGDataUnits).Rows[j].Cells["BuildingUnitID"].Value = -1;
				((UltraGridBase)ULGDataUnits).Rows[j].Cells["BuildingID"].Value = num;
				((UltraGridBase)ULGDataUnits).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			BuildingsSamples.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataUnits).Rows).Count > 0)
			{
				BuildingsUnits.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataUnits).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			BuildingsUnits.CreateUnits(num.ToString(), ((Control)(object)txtSampleCount).Text, ((Control)(object)txtFloorCount).Text, GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
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
			int num = Buildings.Insert_Update(drMaster["BuildingID"].ToString(), ((Control)(object)txtCode).Text, (((Control)(object)txtNameAr).Text == "") ? "Null" : ((Control)(object)txtNameAr).Text, (((Control)(object)txtNameEn).Text == "") ? "Null" : ((Control)(object)txtNameEn).Text, (cboProject.SelectedIndex > -1) ? ((TextEditorControlBase)cboProject).Value.ToString() : "Null", (((Control)(object)txtSize).Text == "") ? "0" : ((Control)(object)txtSize).Text, (((Control)(object)txtSampleCount).Text == "") ? "0" : ((Control)(object)txtSampleCount).Text, (((Control)(object)txtParkingCount).Text == "") ? "0" : ((Control)(object)txtParkingCount).Text, (((Control)(object)txtStoreCount).Text == "") ? "0" : ((Control)(object)txtStoreCount).Text, (((Control)(object)txtBalanceCount).Text == "") ? "0" : ((Control)(object)txtBalanceCount).Text, (((Control)(object)txtFloorCount).Text == "") ? "0" : ((Control)(object)txtFloorCount).Text, (((Control)(object)txtVillaCount).Text == "") ? "0" : ((Control)(object)txtVillaCount).Text, ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			if (!HasContract)
			{
				BuildingsUnits.DeleteByBuildingID(num.ToString(), GlobalVariables.UserID, IsFromServer: true);
				BuildingsSamples.DeleteByBuildingID(num.ToString(), GlobalVariables.UserID, IsFromServer: true);
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["BuildingSampleID"].Value = -1;
					((UltraGridBase)ULGData).Rows[i].Cells["BuildingID"].Value = num;
					((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				}
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataUnits).Rows).Count; j++)
				{
					((UltraGridBase)ULGDataUnits).Rows[j].Cells["BuildingUnitID"].Value = -1;
					((UltraGridBase)ULGDataUnits).Rows[j].Cells["BuildingID"].Value = num;
					((UltraGridBase)ULGDataUnits).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				}
				BuildingsSamples.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
				if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataUnits).Rows).Count > 0)
				{
					BuildingsUnits.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataUnits).DataSource, GlobalVariables.UserID, IsFromServer: true);
				}
				BuildingsUnits.CreateUnits(num.ToString(), ((Control)(object)txtSampleCount).Text, ((Control)(object)txtFloorCount).Text, GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			}
			else
			{
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
				{
					BuildingsUnits.UpdatePriceAndSize(BuildingsSamples.Insert_Update(((UltraGridBase)ULGData).Rows[k].Cells["BuildingSampleID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].Cells["BuildingSampleCode"].Value.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[k].Cells["Size"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].Cells["Price"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].Cells["RoomsCount"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true).ToString(), ((UltraGridBase)ULGData).Rows[k].Cells["Size"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].Cells["Price"].Value.ToString(), IsFromServer: true);
				}
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataUnits).Rows).Count; l++)
				{
					((UltraGridBase)ULGDataUnits).Rows[l].Cells["BuildingID"].Value = num;
					((UltraGridBase)ULGDataUnits).Rows[l].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				}
				if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataUnits).Rows).Count > 0)
				{
					BuildingsUnits.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataUnits).DataSource, GlobalVariables.UserID, IsFromServer: true);
				}
				BuildingsUnits.CreateUnits(num.ToString(), "0", (int.Parse(((Control)(object)txtFloorCount).Text) - int.Parse(drMaster["FloorCount"].ToString())).ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
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
			if (!HasContract)
			{
				BuildingsUnits.DeleteByBuildingID(drMaster["BuildingID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
				BuildingsSamples.DeleteByBuildingID(drMaster["BuildingID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
				Buildings.Delete(drMaster["BuildingID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
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

	public override void btnPrintClick()
	{
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.BuildingsSearchReport(IsFromServer: true);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["BuildingID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtProjects = Projects.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboProject, dtProjects, "ProjectID", "ProjectName");
		dtFloors = Floors.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtUnitTypes = UnitsTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlUnitType.ValueListItems.Clear();
		for (int i = 0; i < dtUnitTypes.Rows.Count; i++)
		{
			vlUnitType.ValueListItems.Add(dtUnitTypes.Rows[i]["UnitTypeID"], dtUnitTypes.Rows[i]["UnitTypeName"].ToString());
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BuildingSampleCode")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null)
		{
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Price" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Size")
			{
				GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "RoomsCount")
			{
				GlobalFunctions.CheckForIntegers(ULGData.ActiveCell, e);
			}
		}
	}

	private void ULGDataUnits_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGDataUnits.ActiveCell != null && (((KeyedSubObjectBase)ULGDataUnits.ActiveCell.Column).Key == "Price" || ((KeyedSubObjectBase)ULGDataUnits.ActiveCell.Column).Key == "Size"))
		{
			GlobalFunctions.CheckForNumbers(ULGDataUnits.ActiveCell, e);
		}
	}

	private void ULGDataUnits_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGDataUnits.ActiveCell.Column).Key == "BuildingUnitCode" || ((KeyedSubObjectBase)ULGDataUnits.ActiveCell.Column).Key == "UnitTypeID")
		{
			((GridItemBase)((UltraGridBase)ULGDataUnits).ActiveRow).Selected = true;
		}
	}

	private void txtSampleCount_ValueChanged(object sender, EventArgs e)
	{
		if (!HasContract)
		{
			dtDetails.Rows.Clear();
		}
		if (((Control)(object)txtSampleCount).Text.Length > 2)
		{
			((Control)(object)txtSampleCount).Text = "99";
		}
		if (!(((Control)(object)txtSampleCount).Text != "") || int.Parse(((Control)(object)txtSampleCount).Text) <= ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count)
		{
			return;
		}
		int num = 0;
		int result = 0;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			int.TryParse(((UltraGridBase)ULGData).Rows[i].Cells["BuildingSampleCode"].Value.ToString(), out result);
			if (result > num)
			{
				num = int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["BuildingSampleCode"].Value.ToString());
			}
		}
		int num2 = int.Parse(((Control)(object)txtSampleCount).Text) - ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count;
		int num3 = 2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
		for (int j = 0; j < num2; j++)
		{
			num++;
			DataRow dataRow = dtDetails.NewRow();
			dataRow["BuildingSampleID"] = -1;
			dataRow["BuildingSampleCode"] = new string('0', num3 - (j + 1).ToString().Length) + (j + 1);
			dataRow["Size"] = 0;
			dataRow["Price"] = 0;
			dataRow["RoomsCount"] = 0;
			dtDetails.Rows.Add(dataRow);
		}
	}

	private void txtSampleCount_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void txtSampleCount_Leave(object sender, EventArgs e)
	{
		((Control)(object)txtSampleCount).Text = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count.ToString();
	}

	private void txtParkingCount_ValueChanged(object sender, EventArgs e)
	{
		if (HasContract && Updating && ((Control)(object)txtParkingCount).Text != "" && drMaster != null && int.Parse(((Control)(object)txtParkingCount).Text) < int.Parse(drMaster["ParkingCount"].ToString()))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا يمكن تقليل عدد الجراجات تم التعامل على المبنى" : "Cannot Decrease Parking Count Building Havr Reserved");
			((Control)(object)txtParkingCount).Text = drMaster["ParkingCount"].ToString();
			((TextEditorControlBase)txtParkingCount).Focus();
			return;
		}
		if (!HasContract)
		{
			for (int i = 0; i < dtbuildingUnits.Rows.Count; i++)
			{
				if (int.Parse(dtbuildingUnits.Rows[i]["UnitTypeID"].ToString()) == 3)
				{
					dtbuildingUnits.Rows[i].Delete();
					dtbuildingUnits.AcceptChanges();
					i--;
				}
			}
		}
		if (((Control)(object)txtParkingCount).Text.Length > 2)
		{
			((Control)(object)txtParkingCount).Text = "99";
		}
		if (!(((Control)(object)txtParkingCount).Text != "") || int.Parse(((Control)(object)txtParkingCount).Text) <= dtbuildingUnits.Select(" UnitTypeID=3").Length)
		{
			return;
		}
		int num = 0;
		int result = 0;
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataUnits).Rows).Count; j++)
		{
			if (((UltraGridBase)ULGDataUnits).Rows[j].Cells["UnitTypeID"].Value.ToString() == "3")
			{
				int.TryParse(((UltraGridBase)ULGDataUnits).Rows[j].Cells["BuildingUnitCode"].Value.ToString().Substring(4), out result);
				if (result > num)
				{
					num = result;
				}
			}
		}
		int num2 = int.Parse(((Control)(object)txtParkingCount).Text) - dtbuildingUnits.Select(" UnitTypeID=3").Length;
		int num3 = 2;
		string text = ((Control)(object)txtCode).Text + dtFloors.Select("UnitTypeID=3")[0]["FloorCode"].ToString();
		((UltraGridBase)ULGDataUnits).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
		for (int k = 0; k < num2; k++)
		{
			num++;
			DataRow dataRow = dtbuildingUnits.NewRow();
			dataRow["BuildingUnitID"] = -1;
			dataRow["BuildingUnitCode"] = text + new string('0', num3 - num.ToString().Length) + num;
			dataRow["Size"] = 0;
			dataRow["Price"] = 0;
			dataRow["UnitTypeID"] = 3;
			dataRow["FloorID"] = dtFloors.Select("UnitTypeID=3")[0]["FloorID"];
			dtbuildingUnits.Rows.Add(dataRow);
		}
	}

	private void txtParkingCount_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void txtParkingCount_Leave(object sender, EventArgs e)
	{
		((Control)(object)txtParkingCount).Text = dtbuildingUnits.Select(" UnitTypeID=3").Length.ToString();
	}

	private void txtStoreCount_ValueChanged(object sender, EventArgs e)
	{
		if (HasContract && Updating && ((Control)(object)txtStoreCount).Text != "" && drMaster != null && int.Parse(((Control)(object)txtStoreCount).Text) < int.Parse(drMaster["StoresCount"].ToString()))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا يمكن تقليل عدد المحلات تم التعامل على المبنى" : "Cannot Decrease Stores Count Building Have Reserved");
			((Control)(object)txtStoreCount).Text = drMaster["StoresCount"].ToString();
			((TextEditorControlBase)txtStoreCount).Focus();
			return;
		}
		if (!HasContract)
		{
			for (int i = 0; i < dtbuildingUnits.Rows.Count; i++)
			{
				if (int.Parse(dtbuildingUnits.Rows[i]["UnitTypeID"].ToString()) == 2)
				{
					dtbuildingUnits.Rows[i].Delete();
					dtbuildingUnits.AcceptChanges();
					i--;
				}
			}
		}
		if (((Control)(object)txtStoreCount).Text.Length > 2)
		{
			((Control)(object)txtStoreCount).Text = "99";
		}
		if (!(((Control)(object)txtStoreCount).Text != "") || int.Parse(((Control)(object)txtStoreCount).Text) <= dtbuildingUnits.Select(" UnitTypeID=2").Length)
		{
			return;
		}
		int num = 0;
		int result = 0;
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataUnits).Rows).Count; j++)
		{
			if (((UltraGridBase)ULGDataUnits).Rows[j].Cells["UnitTypeID"].Value.ToString() == "2")
			{
				int.TryParse(((UltraGridBase)ULGDataUnits).Rows[j].Cells["BuildingUnitCode"].Value.ToString().Substring(4), out result);
				if (result > num)
				{
					num = result;
				}
			}
		}
		int num2 = int.Parse(((Control)(object)txtStoreCount).Text) - dtbuildingUnits.Select(" UnitTypeID=2").Length;
		int num3 = 2;
		string text = ((Control)(object)txtCode).Text + dtFloors.Select("UnitTypeID=2")[0]["FloorCode"].ToString();
		((UltraGridBase)ULGDataUnits).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
		for (int k = 0; k < num2; k++)
		{
			num++;
			DataRow dataRow = dtbuildingUnits.NewRow();
			dataRow["BuildingUnitID"] = -1;
			dataRow["BuildingUnitCode"] = text + new string('0', num3 - num.ToString().Length) + num;
			dataRow["Size"] = 0;
			dataRow["Price"] = 0;
			dataRow["UnitTypeID"] = 2;
			dataRow["FloorID"] = dtFloors.Select("UnitTypeID=2")[0]["FloorID"];
			dtbuildingUnits.Rows.Add(dataRow);
		}
	}

	private void txtStoreCount_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void txtStoreCount_Leave(object sender, EventArgs e)
	{
		((Control)(object)txtStoreCount).Text = dtbuildingUnits.Select(" UnitTypeID=2").Length.ToString();
	}

	private void txtBalanceCount_ValueChanged(object sender, EventArgs e)
	{
		if (HasContract && Updating && ((Control)(object)txtBalanceCount).Text != "" && drMaster != null && int.Parse(((Control)(object)txtBalanceCount).Text) < int.Parse(drMaster["BalancesCount"].ToString()))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا يمكن تقليل عدد الميزانين  تم التعامل على المبنى" : "Cannot Decrease Balances Count Building Have Reserved");
			((Control)(object)txtBalanceCount).Text = drMaster["BalancesCount"].ToString();
			((TextEditorControlBase)txtBalanceCount).Focus();
			return;
		}
		if (!HasContract)
		{
			for (int i = 0; i < dtbuildingUnits.Rows.Count; i++)
			{
				if (int.Parse(dtbuildingUnits.Rows[i]["UnitTypeID"].ToString()) == 4)
				{
					dtbuildingUnits.Rows[i].Delete();
					dtbuildingUnits.AcceptChanges();
					i--;
				}
			}
		}
		if (((Control)(object)txtBalanceCount).Text.Length > 2)
		{
			((Control)(object)txtBalanceCount).Text = "99";
		}
		if (!(((Control)(object)txtBalanceCount).Text != "") || int.Parse(((Control)(object)txtBalanceCount).Text) <= dtbuildingUnits.Select(" UnitTypeID=4").Length)
		{
			return;
		}
		int num = 0;
		int result = 0;
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataUnits).Rows).Count; j++)
		{
			if (((UltraGridBase)ULGDataUnits).Rows[j].Cells["UnitTypeID"].Value.ToString() == "4")
			{
				int.TryParse(((UltraGridBase)ULGDataUnits).Rows[j].Cells["BuildingUnitCode"].Value.ToString().Substring(4), out result);
				if (result > num)
				{
					num = result;
				}
			}
		}
		int num2 = int.Parse(((Control)(object)txtBalanceCount).Text) - dtbuildingUnits.Select(" UnitTypeID=4").Length;
		int num3 = 2;
		string text = ((Control)(object)txtCode).Text + dtFloors.Select("UnitTypeID=4")[0]["FloorCode"].ToString();
		((UltraGridBase)ULGDataUnits).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
		for (int k = 0; k < num2; k++)
		{
			num++;
			DataRow dataRow = dtbuildingUnits.NewRow();
			dataRow["BuildingUnitID"] = -1;
			dataRow["BuildingUnitCode"] = text + new string('0', num3 - num.ToString().Length) + num;
			dataRow["Size"] = 0;
			dataRow["Price"] = 0;
			dataRow["UnitTypeID"] = 4;
			dataRow["FloorID"] = dtFloors.Select("UnitTypeID=4")[0]["FloorID"];
			dtbuildingUnits.Rows.Add(dataRow);
		}
	}

	private void txtBalanceCount_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void txtBalanceCount_Leave(object sender, EventArgs e)
	{
		((Control)(object)txtBalanceCount).Text = dtbuildingUnits.Select(" UnitTypeID=4").Length.ToString();
	}

	private void txtFloorCount_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void txtFloorCount_ValueChanged(object sender, EventArgs e)
	{
		if (HasContract && Updating && ((Control)(object)txtFloorCount).Text != "" && drMaster != null && int.Parse(((Control)(object)txtFloorCount).Text) < int.Parse(drMaster["FloorCount"].ToString()))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا يمكن تقليل عدد الادوار  تم التعامل على المبنى" : "Cannot Decrease Floor Count Building Have Reserved");
			((Control)(object)txtFloorCount).Text = drMaster["FloorCount"].ToString();
			((TextEditorControlBase)txtFloorCount).Focus();
		}
	}

	private void txtSize_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtVillaCount_ValueChanged(object sender, EventArgs e)
	{
		if (HasContract && Updating && ((Control)(object)txtVillaCount).Text != "" && drMaster != null && int.Parse(((Control)(object)txtVillaCount).Text) < int.Parse((drMaster["VillaCount"] == DBNull.Value) ? "0" : drMaster["VillaCount"].ToString()))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا يمكن تقليل عدد الفيلات  تم التعامل على المبنى" : "Cannot Decrease Villa Count Building Have Reserved");
			((Control)(object)txtVillaCount).Text = drMaster["VillaCount"].ToString();
			((TextEditorControlBase)txtVillaCount).Focus();
			return;
		}
		if (!HasContract)
		{
			for (int i = 0; i < dtbuildingUnits.Rows.Count; i++)
			{
				if (int.Parse(dtbuildingUnits.Rows[i]["UnitTypeID"].ToString()) == 5)
				{
					dtbuildingUnits.Rows[i].Delete();
					dtbuildingUnits.AcceptChanges();
					i--;
				}
			}
		}
		if (((Control)(object)txtVillaCount).Text.Length > 2)
		{
			((Control)(object)txtVillaCount).Text = "99";
		}
		if (!(((Control)(object)txtVillaCount).Text != "") || int.Parse(((Control)(object)txtVillaCount).Text) <= dtbuildingUnits.Select(" UnitTypeID=5").Length)
		{
			return;
		}
		int num = 0;
		int result = 0;
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataUnits).Rows).Count; j++)
		{
			if (((UltraGridBase)ULGDataUnits).Rows[j].Cells["UnitTypeID"].Value.ToString() == "5")
			{
				int.TryParse(((UltraGridBase)ULGDataUnits).Rows[j].Cells["BuildingUnitCode"].Value.ToString().Substring(4), out result);
				if (result > num)
				{
					num = result;
				}
			}
		}
		int num2 = int.Parse(((Control)(object)txtVillaCount).Text) - dtbuildingUnits.Select(" UnitTypeID=5").Length;
		int num3 = 2;
		string text = ((Control)(object)txtCode).Text + dtFloors.Select("UnitTypeID=5")[0]["FloorCode"].ToString();
		((UltraGridBase)ULGDataUnits).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
		for (int k = 0; k < num2; k++)
		{
			num++;
			DataRow dataRow = dtbuildingUnits.NewRow();
			dataRow["BuildingUnitID"] = -1;
			dataRow["BuildingUnitCode"] = text + new string('0', num3 - num.ToString().Length) + num;
			dataRow["Size"] = 0;
			dataRow["Price"] = 0;
			dataRow["UnitTypeID"] = 5;
			dataRow["FloorID"] = dtFloors.Select("UnitTypeID=5")[0]["FloorID"];
			dtbuildingUnits.Rows.Add(dataRow);
		}
	}

	private void txtVillaCount_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void txtVillaCount_Leave(object sender, EventArgs e)
	{
		((Control)(object)txtVillaCount).Text = dtbuildingUnits.Select(" UnitTypeID=5").Length.ToString();
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
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Expected O, but got Unknown
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Expected O, but got Unknown
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Expected O, but got Unknown
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Expected O, but got Unknown
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Expected O, but got Unknown
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Expected O, but got Unknown
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Expected O, but got Unknown
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Expected O, but got Unknown
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Expected O, but got Unknown
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Expected O, but got Unknown
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Expected O, but got Unknown
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Expected O, but got Unknown
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Expected O, but got Unknown
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Expected O, but got Unknown
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Expected O, but got Unknown
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Expected O, but got Unknown
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Expected O, but got Unknown
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Constructions.MasterData.frmBuildings));
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
		Appearance val16 = new Appearance();
		Appearance val17 = new Appearance();
		Appearance val18 = new Appearance();
		Appearance val19 = new Appearance();
		Appearance val20 = new Appearance();
		Appearance val21 = new Appearance();
		Appearance val22 = new Appearance();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGDataUnits = new UltraGrid();
		this.lblProject = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.cboProject = new UltraComboEditor();
		this.lblNameEn = new UltraLabel();
		this.txtNameEn = new UltraTextEditor();
		this.lblNameAr = new UltraLabel();
		this.txtNameAr = new UltraTextEditor();
		this.lblSize = new UltraLabel();
		this.txtSize = new UltraTextEditor();
		this.lblSampleCount = new UltraLabel();
		this.txtSampleCount = new UltraTextEditor();
		this.lblFloorCount = new UltraLabel();
		this.txtFloorCount = new UltraTextEditor();
		this.lblBalanceCount = new UltraLabel();
		this.txtBalanceCount = new UltraTextEditor();
		this.lblStoresCount = new UltraLabel();
		this.txtStoreCount = new UltraTextEditor();
		this.lblParkingCount = new UltraLabel();
		this.txtParkingCount = new UltraTextEditor();
		this.lblVillaCount = new UltraLabel();
		this.txtVillaCount = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataUnits).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboProject).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSize).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSampleCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFloorCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBalanceCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtStoreCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtParkingCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVillaCount).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		val.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val, "ultraTab1");
		((SubObjectBase)val).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[1] { val });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
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
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataUnits);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.ULGDataUnits, "ULGDataUnits");
		((UltraGridBase)this.ULGDataUnits).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataUnits).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataUnits).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val10, "appearance1");
		((UltraGridBase)this.ULGDataUnits).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGDataUnits).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val11).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val11).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val11).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val11, "appearance2");
		((AppearanceBase)val11).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataUnits).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGDataUnits).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val12).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val12, "appearance3");
		((UltraGridBase)this.ULGDataUnits).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val12;
		((AppearanceBase)val13).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val13).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val13, "appearance4");
		((UltraGridBase)this.ULGDataUnits).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGDataUnits).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataUnits).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val14).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val14).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val14, "appearance5");
		((UltraGridBase)this.ULGDataUnits).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGDataUnits).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataUnits).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataUnits).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataUnits).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataUnits).Name = "ULGDataUnits";
		((UltraControlBase)this.ULGDataUnits).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataUnits.AfterEnterEditMode += new System.EventHandler(ULGDataUnits_AfterEnterEditMode);
		((System.Windows.Forms.Control)(object)this.ULGDataUnits).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGDataUnits_KeyPress);
		resources.ApplyResources(this.lblProject, "lblProject");
		this.lblProject.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblProject).Name = "lblProject";
		((ControlBase)this.lblProject).WrapText = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.cboProject, "cboProject");
		((TextEditorControlBase)this.cboProject).AlwaysInEditMode = true;
		this.cboProject.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboProject).Name = "cboProject";
		resources.ApplyResources(this.lblNameEn, "lblNameEn");
		this.lblNameEn.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNameEn).Name = "lblNameEn";
		((ControlBase)this.lblNameEn).WrapText = false;
		resources.ApplyResources(this.txtNameEn, "txtNameEn");
		resources.ApplyResources(val15, "appearance14");
		((TextEditorControlBase)this.txtNameEn).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.txtNameEn).Name = "txtNameEn";
		resources.ApplyResources(this.lblNameAr, "lblNameAr");
		this.lblNameAr.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNameAr).Name = "lblNameAr";
		((ControlBase)this.lblNameAr).WrapText = false;
		resources.ApplyResources(this.txtNameAr, "txtNameAr");
		((System.Windows.Forms.Control)(object)this.txtNameAr).Name = "txtNameAr";
		resources.ApplyResources(this.lblSize, "lblSize");
		this.lblSize.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSize).Name = "lblSize";
		((ControlBase)this.lblSize).WrapText = false;
		resources.ApplyResources(this.txtSize, "txtSize");
		resources.ApplyResources(val16, "appearance15");
		((TextEditorControlBase)this.txtSize).Appearance = (AppearanceBase)(object)val16;
		((System.Windows.Forms.Control)(object)this.txtSize).Name = "txtSize";
		((System.Windows.Forms.Control)(object)this.txtSize).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSize_KeyPress);
		resources.ApplyResources(this.lblSampleCount, "lblSampleCount");
		this.lblSampleCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSampleCount).Name = "lblSampleCount";
		((ControlBase)this.lblSampleCount).WrapText = false;
		resources.ApplyResources(this.txtSampleCount, "txtSampleCount");
		resources.ApplyResources(val17, "appearance16");
		((TextEditorControlBase)this.txtSampleCount).Appearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.txtSampleCount).Name = "txtSampleCount";
		((TextEditorControlBase)this.txtSampleCount).ValueChanged += new System.EventHandler(txtSampleCount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtSampleCount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSampleCount_KeyPress);
		((System.Windows.Forms.Control)(object)this.txtSampleCount).Leave += new System.EventHandler(txtSampleCount_Leave);
		resources.ApplyResources(this.lblFloorCount, "lblFloorCount");
		this.lblFloorCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFloorCount).Name = "lblFloorCount";
		((ControlBase)this.lblFloorCount).WrapText = false;
		resources.ApplyResources(this.txtFloorCount, "txtFloorCount");
		resources.ApplyResources(val18, "appearance17");
		((TextEditorControlBase)this.txtFloorCount).Appearance = (AppearanceBase)(object)val18;
		((System.Windows.Forms.Control)(object)this.txtFloorCount).Name = "txtFloorCount";
		((TextEditorControlBase)this.txtFloorCount).ValueChanged += new System.EventHandler(txtFloorCount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtFloorCount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtFloorCount_KeyPress);
		resources.ApplyResources(this.lblBalanceCount, "lblBalanceCount");
		this.lblBalanceCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBalanceCount).Name = "lblBalanceCount";
		((ControlBase)this.lblBalanceCount).WrapText = false;
		resources.ApplyResources(this.txtBalanceCount, "txtBalanceCount");
		resources.ApplyResources(val19, "appearance18");
		((TextEditorControlBase)this.txtBalanceCount).Appearance = (AppearanceBase)(object)val19;
		((System.Windows.Forms.Control)(object)this.txtBalanceCount).Name = "txtBalanceCount";
		((TextEditorControlBase)this.txtBalanceCount).ValueChanged += new System.EventHandler(txtBalanceCount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtBalanceCount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtBalanceCount_KeyPress);
		((System.Windows.Forms.Control)(object)this.txtBalanceCount).Leave += new System.EventHandler(txtBalanceCount_Leave);
		resources.ApplyResources(this.lblStoresCount, "lblStoresCount");
		this.lblStoresCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStoresCount).Name = "lblStoresCount";
		((ControlBase)this.lblStoresCount).WrapText = false;
		resources.ApplyResources(this.txtStoreCount, "txtStoreCount");
		resources.ApplyResources(val20, "appearance19");
		((TextEditorControlBase)this.txtStoreCount).Appearance = (AppearanceBase)(object)val20;
		((System.Windows.Forms.Control)(object)this.txtStoreCount).Name = "txtStoreCount";
		((TextEditorControlBase)this.txtStoreCount).ValueChanged += new System.EventHandler(txtStoreCount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtStoreCount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtStoreCount_KeyPress);
		((System.Windows.Forms.Control)(object)this.txtStoreCount).Leave += new System.EventHandler(txtStoreCount_Leave);
		resources.ApplyResources(this.lblParkingCount, "lblParkingCount");
		this.lblParkingCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblParkingCount).Name = "lblParkingCount";
		((ControlBase)this.lblParkingCount).WrapText = false;
		resources.ApplyResources(this.txtParkingCount, "txtParkingCount");
		resources.ApplyResources(val21, "appearance20");
		((TextEditorControlBase)this.txtParkingCount).Appearance = (AppearanceBase)(object)val21;
		((System.Windows.Forms.Control)(object)this.txtParkingCount).Name = "txtParkingCount";
		((TextEditorControlBase)this.txtParkingCount).ValueChanged += new System.EventHandler(txtParkingCount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtParkingCount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtParkingCount_KeyPress);
		((System.Windows.Forms.Control)(object)this.txtParkingCount).Leave += new System.EventHandler(txtParkingCount_Leave);
		resources.ApplyResources(this.lblVillaCount, "lblVillaCount");
		this.lblVillaCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVillaCount).Name = "lblVillaCount";
		((ControlBase)this.lblVillaCount).WrapText = false;
		resources.ApplyResources(this.txtVillaCount, "txtVillaCount");
		resources.ApplyResources(val22, "appearance21");
		((TextEditorControlBase)this.txtVillaCount).Appearance = (AppearanceBase)(object)val22;
		((System.Windows.Forms.Control)(object)this.txtVillaCount).Name = "txtVillaCount";
		((TextEditorControlBase)this.txtVillaCount).ValueChanged += new System.EventHandler(txtVillaCount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtVillaCount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtVillaCount_KeyPress);
		((System.Windows.Forms.Control)(object)this.txtVillaCount).Leave += new System.EventHandler(txtVillaCount_Leave);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVillaCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVillaCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBalanceCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBalanceCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStoresCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtStoreCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblParkingCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtParkingCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFloorCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtFloorCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSampleCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSampleCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSize);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSize);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNameAr);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNameAr);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblProject);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboProject);
		base.Name = "frmBuildings";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UTCDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboProject, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblProject, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSize, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSize, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSampleCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSampleCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtFloorCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFloorCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtParkingCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblParkingCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtStoreCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStoresCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBalanceCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBalanceCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVillaCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVillaCount, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataUnits).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboProject).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSize).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSampleCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFloorCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBalanceCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtStoreCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtParkingCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVillaCount).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
