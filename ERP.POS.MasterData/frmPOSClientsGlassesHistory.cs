using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Clinics;
using BusinessLayer.General;
using BusinessLayer.POS;
using BusinessLayer.Privilege;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Clinics.MasterData;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.POS.MasterData;

public class frmPOSClientsGlassesHistory : frmButtons
{
	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtDoctors;

	private DataTable dtDetails;

	private ValueList vlDoctors = new ValueList();

	private DataRow drMaster;

	private bool isLabModule = true;

	private decimal ClientID = default(decimal);

	private string ClientName = "";

	public decimal ClientsGlassesHistoryID = default(decimal);

	private bool CanAddNewDoctor = false;

	private IContainer components = null;

	public UltraTextEditor txtCode;

	public UltraButton btnSearch;

	public UltraButton btnPriveous;

	public UltraButton btnNext;

	public UltraLabel lblTitle;

	public UltraLabel lblTitle2;

	public UltraLabel lblCode;

	public UltraButton btnCopyTo;

	private UltraLabel lblR;

	private UltraTextEditor txtName;

	private UltraLabel lblName;

	private UltraLabel lblRSph;

	private UltraComboEditor cboRColorDistance;

	private UltraLabel lblReading;

	private UltraTextEditor txtRAdd;

	private UltraLabel lblDistance;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	public UltraGrid ULGData;

	private UltraLabel lblRAxis;

	private UltraLabel lblRCyl;

	private UltraTextEditor txtRAxisDistance;

	private UltraComboEditor cboRSizeDistance;

	private UltraComboEditor cboRSizeReading;

	private UltraTextEditor txtRAxisReading;

	private UltraComboEditor cboRColorReading;

	private UltraLabel lblRAdd;

	private UltraLabel lblLAdd;

	private UltraComboEditor cboLSizeReading;

	private UltraTextEditor txtLAxisReading;

	private UltraComboEditor cboLColorReading;

	private UltraComboEditor cboLSizeDistance;

	private UltraTextEditor txtLAxisDistance;

	private UltraLabel lblLCyl;

	private UltraLabel lblLAxis;

	private UltraTextEditor txtLAdd;

	private UltraLabel lblLSph;

	private UltraComboEditor cboLColorDistance;

	private UltraLabel lblL;

	private UltraLabel lblIPD;

	private UltraLabel lblIPDDistance;

	private UltraLabel lblIPDReading;

	private UltraTextEditor txtIPDDistance;

	private UltraTextEditor txtIPDReading;

	private UltraLabel lblmmReading;

	private UltraLabel lblmmDistance;

	private UltraDateTimeEditor dtpDate;

	private UltraLabel lblDate;

	private UltraLabel lblDoctor;

	private UltraComboEditor cboDoctor;

	private UltraGroupBox UGBGlassesHistory;

	public UltraButton btnLTranspose;

	public UltraButton btnRTranspose;

	public UltraButton btnDoctorAdd;

	public frmPOSClientsGlassesHistory()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		TableName = "POS_ClientsGlassesHistory";
		NoCol = "";
		IDCol = "ClientsGlassesHistoryID";
		DateCol = "GetDate()";
	}

	public frmPOSClientsGlassesHistory(bool islabmodule, decimal clientid, string clientname, decimal clientglasseshistoryid)
		: this()
	{
		isLabModule = islabmodule;
		ClientID = clientid;
		ClientName = clientname;
		ClientsGlassesHistoryID = clientglasseshistoryid;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		if (!base.DesignMode)
		{
			DataTable dt = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboTransactionBranch, dt, "BranchID", "BranchName");
		}
		if (GlobalVariables.dtForms.Select("FormFullName = 'ERP.Clinics.MasterData.frmDoctors'").Length != 0)
		{
			CanAddNewDoctor = GlobalFunctions.GetFormFunction(GlobalVariables.dtForms.Select("FormFullName = 'ERP.Clinics.MasterData.frmDoctors'")[0]["FormID"].ToString(), "Adding");
		}
		((Control)(object)txtName).Text = ClientName;
		dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboRColorDistance, dtColors, "ColorID", "ColorName");
		GlobalFunctions.FillCombo(cboRColorReading, dtColors, "ColorID", "ColorName");
		GlobalFunctions.FillCombo(cboLColorDistance, dtColors, "ColorID", "ColorName");
		GlobalFunctions.FillCombo(cboLColorReading, dtColors, "ColorID", "ColorName");
		dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboRSizeDistance, dtSizes, "ItemSizeID", "ItemSizeName");
		GlobalFunctions.FillCombo(cboRSizeReading, dtSizes, "ItemSizeID", "ItemSizeName");
		GlobalFunctions.FillCombo(cboLSizeDistance, dtSizes, "ItemSizeID", "ItemSizeName");
		GlobalFunctions.FillCombo(cboLSizeReading, dtSizes, "ItemSizeID", "ItemSizeName");
		dtDoctors = Doctors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDoctor, dtDoctors, "DoctorID", "DoctorName");
		vlDoctors.ValueListItems.Clear();
		for (int i = 0; i < dtDoctors.Rows.Count; i++)
		{
			vlDoctors.ValueListItems.Add(dtDoctors.Rows[i]["DoctorID"], dtDoctors.Rows[i]["DoctorName"].ToString());
		}
		dtDetails = (isLabModule ? ClientsGlassesHistory.SelectByClientID(ClientID.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false) : ClientsGlassesHistory.SelectBySubAccountID(ClientID.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false));
		InitGrid();
	}

	private void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = dtDetails;
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("Select"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns.Insert(0, "Select");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Select"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Select"].Header).Caption = (GlobalVariables.IsArabic ? "إختيار" : "Select");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Select"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Select"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Select"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Select"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Select"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count - 1));
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["Select"].Value = (GlobalVariables.IsArabic ? "إختيار" : "Select");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Date"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Date"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Date"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DoctorID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DoctorID"].Header).Caption = (GlobalVariables.IsArabic ? "الطبيب" : "Doctor");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DoctorID"].ValueList = (IValueList)(object)vlDoctors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DoctorID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
	}

	public override void SetControls(bool NavMode)
	{
		((Control)(object)btnAdd).Visible = NavMode;
		((Control)(object)btnUpdate).Visible = NavMode;
		((Control)(object)btnClose).Visible = NavMode;
		((Control)(object)btnOK).Visible = !NavMode;
		((Control)(object)btnSaveClose).Visible = !NavMode;
		((Control)(object)btnCancel).Visible = !NavMode;
		((Control)(object)btnDoctorAdd).Visible = CanAddNewDoctor && !NavMode;
		((Control)(object)btnRTranspose).Visible = !NavMode;
		((Control)(object)btnLTranspose).Visible = !NavMode;
		((Control)(object)txtCode).Visible = false;
		((Control)(object)btnSearch).Visible = false;
		((Control)(object)btnNext).Visible = false;
		((Control)(object)btnCopyTo).Visible = false;
		((Control)(object)btnPriveous).Visible = false;
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)lblCode).Visible = false;
		((EditorButtonControlBase)txtName).ReadOnly = true;
		((EditorButtonControlBase)cboRColorDistance).ReadOnly = NavMode;
		((EditorButtonControlBase)cboRColorReading).ReadOnly = NavMode;
		((EditorButtonControlBase)cboRSizeDistance).ReadOnly = NavMode;
		((EditorButtonControlBase)cboRSizeReading).ReadOnly = NavMode;
		((EditorButtonControlBase)txtRAxisDistance).ReadOnly = NavMode;
		((EditorButtonControlBase)txtRAxisReading).ReadOnly = NavMode;
		((EditorButtonControlBase)txtRAdd).ReadOnly = NavMode;
		((EditorButtonControlBase)cboLColorDistance).ReadOnly = NavMode;
		((EditorButtonControlBase)cboLColorReading).ReadOnly = NavMode;
		((EditorButtonControlBase)cboLSizeDistance).ReadOnly = NavMode;
		((EditorButtonControlBase)cboLSizeReading).ReadOnly = NavMode;
		((EditorButtonControlBase)txtLAxisDistance).ReadOnly = NavMode;
		((EditorButtonControlBase)txtLAxisReading).ReadOnly = NavMode;
		((EditorButtonControlBase)txtLAdd).ReadOnly = NavMode;
		((EditorButtonControlBase)txtIPDDistance).ReadOnly = NavMode;
		((EditorButtonControlBase)txtIPDReading).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboDoctor).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		cboRColorDistance.SelectedIndex = -1;
		cboRColorReading.SelectedIndex = -1;
		cboRSizeDistance.SelectedIndex = -1;
		cboRSizeReading.SelectedIndex = -1;
		((TextEditorControlBase)txtRAxisDistance).Clear();
		((TextEditorControlBase)txtRAxisReading).Clear();
		((TextEditorControlBase)txtRAdd).ValueChanged -= txtRAdd_ValueChanged;
		((TextEditorControlBase)txtRAdd).Clear();
		((TextEditorControlBase)txtRAdd).ValueChanged += txtRAdd_ValueChanged;
		cboLColorDistance.SelectedIndex = -1;
		cboLColorReading.SelectedIndex = -1;
		cboLSizeDistance.SelectedIndex = -1;
		cboLSizeReading.SelectedIndex = -1;
		((TextEditorControlBase)txtLAxisDistance).Clear();
		((TextEditorControlBase)txtLAxisReading).Clear();
		((TextEditorControlBase)txtLAdd).ValueChanged -= txtLAdd_ValueChanged;
		((TextEditorControlBase)txtLAdd).Clear();
		((TextEditorControlBase)txtLAdd).ValueChanged += txtLAdd_ValueChanged;
		((TextEditorControlBase)txtIPDDistance).Clear();
		((TextEditorControlBase)txtIPDReading).Clear();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		cboDoctor.SelectedIndex = -1;
		((TextEditorControlBase)txtNotes).Clear();
	}

	public override void FillData()
	{
		if (ClientID == -1m)
		{
			drMaster = null;
			btnAddClick();
		}
		else if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = ClientsGlassesHistory.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
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

	public void FillControls()
	{
		((TextEditorControlBase)cboTransactionBranch).Value = drMaster["BranchID"];
		ClientsGlassesHistoryID = int.Parse(drMaster["ClientsGlassesHistoryID"].ToString());
		((Control)(object)lblHistory).Text = Trans_Log.GetRowHistory(TableName, ClientsGlassesHistoryID.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		((Control)(object)txtName).Text = ClientName;
		((TextEditorControlBase)cboRColorDistance).Value = drMaster["RColorIDDistance"];
		((TextEditorControlBase)cboRColorReading).Value = drMaster["RColorIDReading"];
		((TextEditorControlBase)cboRSizeDistance).Value = drMaster["RItemSizeIDDistance"];
		((TextEditorControlBase)cboRSizeReading).Value = drMaster["RItemSizeIDReading"];
		((Control)(object)txtRAxisDistance).Text = drMaster["RAxDistance"].ToString();
		((Control)(object)txtRAxisReading).Text = drMaster["RAxReading"].ToString();
		((TextEditorControlBase)txtRAdd).ValueChanged -= txtRAdd_ValueChanged;
		((Control)(object)txtRAdd).Text = drMaster["RAdd"].ToString();
		((TextEditorControlBase)txtRAdd).ValueChanged += txtRAdd_ValueChanged;
		((TextEditorControlBase)cboLColorDistance).Value = drMaster["LColorIDDistance"];
		((TextEditorControlBase)cboLColorReading).Value = drMaster["LColorIDReading"];
		((TextEditorControlBase)cboLSizeDistance).Value = drMaster["LItemSizeIDDistance"];
		((TextEditorControlBase)cboLSizeReading).Value = drMaster["LItemSizeIDReading"];
		((Control)(object)txtLAxisDistance).Text = drMaster["LAxDistance"].ToString();
		((Control)(object)txtLAxisReading).Text = drMaster["LAxReading"].ToString();
		((TextEditorControlBase)txtLAdd).ValueChanged -= txtLAdd_ValueChanged;
		((Control)(object)txtLAdd).Text = drMaster["LAdd"].ToString();
		((TextEditorControlBase)txtLAdd).ValueChanged += txtLAdd_ValueChanged;
		((Control)(object)txtIPDDistance).Text = drMaster["IPDDistance"].ToString();
		((Control)(object)txtIPDReading).Text = drMaster["IPDReading"].ToString();
		dtpDate.Value = drMaster["Date"];
		((TextEditorControlBase)cboDoctor).Value = drMaster["DoctorID"];
		((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
	}

	public void FillGrid()
	{
		dtDetails = (isLabModule ? ClientsGlassesHistory.SelectByClientID(ClientID.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false) : ClientsGlassesHistory.SelectBySubAccountID(ClientID.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false));
		InitGrid();
	}

	public override void DisplayData()
	{
		if (drMaster != null)
		{
			FillControls();
			FillGrid();
		}
	}

	public override bool ValidateData()
	{
		if ((cboRColorDistance.SelectedIndex != -1 && cboRSizeDistance.SelectedIndex == -1) || (cboRColorDistance.SelectedIndex == -1 && cboRSizeDistance.SelectedIndex != -1))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار كلا من Sph و Cyl   لعدسة المسافات يمين " : "Please Select Both Sph and Cyl Of The Right Distance Lense ");
			if (cboRColorDistance.SelectedIndex == -1)
			{
				((TextEditorControlBase)cboRColorDistance).Focus();
				cboRColorDistance.DropDown();
			}
			if (cboRSizeDistance.SelectedIndex == -1)
			{
				((TextEditorControlBase)cboRSizeDistance).Focus();
				cboRSizeDistance.DropDown();
			}
			return false;
		}
		if ((cboLColorDistance.SelectedIndex != -1 && cboLSizeDistance.SelectedIndex == -1) || (cboLColorDistance.SelectedIndex == -1 && cboLSizeDistance.SelectedIndex != -1))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار كلا من Sph و Cyl   لعدسة المسافات يسار " : "Please Select Both Sph and Cyl Of The Left Distance Lense ");
			if (cboLColorDistance.SelectedIndex == -1)
			{
				((TextEditorControlBase)cboLColorDistance).Focus();
				cboLColorDistance.DropDown();
			}
			if (cboLSizeDistance.SelectedIndex == -1)
			{
				((TextEditorControlBase)cboLSizeDistance).Focus();
				cboLSizeDistance.DropDown();
			}
			return false;
		}
		if ((cboRColorReading.SelectedIndex != -1 && cboRSizeReading.SelectedIndex == -1) || (cboRColorReading.SelectedIndex == -1 && cboRSizeReading.SelectedIndex != -1))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار كلا من Sph و Cylلعدسة القراءه يمين " : "Please Select Both Sph and Cyl Of The Right Reading Lense ");
			if (cboRColorReading.SelectedIndex == -1)
			{
				((TextEditorControlBase)cboRColorReading).Focus();
				cboRColorReading.DropDown();
			}
			if (cboRSizeReading.SelectedIndex == -1)
			{
				((TextEditorControlBase)cboRSizeReading).Focus();
				cboRSizeReading.DropDown();
			}
			return false;
		}
		if ((cboLColorReading.SelectedIndex != -1 && cboLSizeReading.SelectedIndex == -1) || (cboLColorReading.SelectedIndex == -1 && cboLSizeReading.SelectedIndex != -1))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار كلا من Sph و Cyl لعدسة القراءه يسار " : "Please Select Both Sph and Cyl Of The Left Reading Lense ");
			if (cboLColorReading.SelectedIndex == -1)
			{
				((TextEditorControlBase)cboLColorReading).Focus();
				cboLColorReading.DropDown();
			}
			if (cboLSizeReading.SelectedIndex == -1)
			{
				((TextEditorControlBase)cboLSizeReading).Focus();
				cboLSizeReading.DropDown();
			}
			return false;
		}
		return true;
	}

	public override void AddData()
	{
		try
		{
			ClientsGlassesHistoryID = ClientsGlassesHistory.Insert_Update("-1", dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), isLabModule ? ClientID.ToString() : "Null", isLabModule ? "Null" : ClientID.ToString(), (cboDoctor.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDoctor).Value.ToString(), (cboRColorReading.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboRColorReading).Value.ToString(), (cboRSizeReading.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboRSizeReading).Value.ToString(), (((Control)(object)txtRAxisReading).Text == "") ? "Null" : ((Control)(object)txtRAxisReading).Text, (cboRColorDistance.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboRColorDistance).Value.ToString(), (cboRSizeDistance.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboRSizeDistance).Value.ToString(), (((Control)(object)txtRAxisDistance).Text == "") ? "Null" : ((Control)(object)txtRAxisDistance).Text, (((Control)(object)txtRAdd).Text == "") ? "Null" : ((Control)(object)txtRAdd).Text, (cboLColorReading.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLColorReading).Value.ToString(), (cboLSizeReading.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLSizeReading).Value.ToString(), (((Control)(object)txtLAxisReading).Text == "") ? "Null" : ((Control)(object)txtLAxisReading).Text, (cboLColorDistance.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLColorDistance).Value.ToString(), (cboLSizeDistance.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLSizeDistance).Value.ToString(), (((Control)(object)txtLAxisDistance).Text == "") ? "Null" : ((Control)(object)txtLAxisDistance).Text, (((Control)(object)txtLAdd).Text == "") ? "Null" : ((Control)(object)txtLAdd).Text, (((Control)(object)txtIPDReading).Text == "") ? "Null" : ((Control)(object)txtIPDReading).Text, (((Control)(object)txtIPDDistance).Text == "") ? "Null" : ((Control)(object)txtIPDDistance).Text, ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
			FillGrid();
		}
		catch
		{
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
		}
	}

	public override void UpdateData()
	{
		try
		{
			ClientsGlassesHistoryID = ClientsGlassesHistory.Insert_Update(drMaster["ClientsGlassesHistoryID"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), isLabModule ? ClientID.ToString() : "Null", isLabModule ? "Null" : ClientID.ToString(), (cboDoctor.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDoctor).Value.ToString(), (cboRColorReading.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboRColorReading).Value.ToString(), (cboRSizeReading.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboRSizeReading).Value.ToString(), (((Control)(object)txtRAxisReading).Text == "") ? "Null" : ((Control)(object)txtRAxisReading).Text, (cboRColorDistance.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboRColorDistance).Value.ToString(), (cboRSizeDistance.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboRSizeDistance).Value.ToString(), (((Control)(object)txtRAxisDistance).Text == "") ? "Null" : ((Control)(object)txtRAxisDistance).Text, (((Control)(object)txtRAdd).Text == "") ? "Null" : ((Control)(object)txtRAdd).Text, (cboLColorReading.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLColorReading).Value.ToString(), (cboLSizeReading.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLSizeReading).Value.ToString(), (((Control)(object)txtLAxisReading).Text == "") ? "Null" : ((Control)(object)txtLAxisReading).Text, (cboLColorDistance.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLColorDistance).Value.ToString(), (cboLSizeDistance.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLSizeDistance).Value.ToString(), (((Control)(object)txtLAxisDistance).Text == "") ? "Null" : ((Control)(object)txtLAxisDistance).Text, (((Control)(object)txtLAdd).Text == "") ? "Null" : ((Control)(object)txtLAdd).Text, (((Control)(object)txtIPDReading).Text == "") ? "Null" : ((Control)(object)txtIPDReading).Text, (((Control)(object)txtIPDDistance).Text == "") ? "Null" : ((Control)(object)txtIPDDistance).Text, ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
			FillGrid();
		}
		catch
		{
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
		}
	}

	public override void DeleteData()
	{
		ClientsGlassesHistory.Delete(drMaster["ClientsGlassesHistoryID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
		FillGrid();
	}

	public override void btnUpdateClick()
	{
		if (drMaster != null)
		{
			RowID = drMaster[IDCol].ToString();
			base.btnUpdateClick();
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
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Are You Sure You want to Delete this Data?");
		DataSaved = true;
		if (GlobalVariables.MessageBoxResult == 'Y')
		{
			DeleteData();
			if (DataSaved)
			{
				FillData();
				drMaster = null;
			}
		}
	}

	public override void btnCancelClick()
	{
		base.btnCancelClick();
		if (Adding)
		{
			drMaster = null;
		}
	}

	public override void btnOKClick()
	{
		base.btnOKClick();
	}

	public override void btnRefreshDataClick()
	{
		dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboRColorDistance, dtColors, "ColorID", "ColorName");
		GlobalFunctions.FillCombo(cboRColorReading, dtColors, "ColorID", "ColorName");
		GlobalFunctions.FillCombo(cboLColorDistance, dtColors, "ColorID", "ColorName");
		GlobalFunctions.FillCombo(cboLColorReading, dtColors, "ColorID", "ColorName");
		dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboRSizeDistance, dtSizes, "ItemSizeID", "ItemSizeName");
		GlobalFunctions.FillCombo(cboRSizeReading, dtSizes, "ItemSizeID", "ItemSizeName");
		GlobalFunctions.FillCombo(cboLColorDistance, dtSizes, "ItemSizeID", "ItemSizeName");
		GlobalFunctions.FillCombo(cboLColorReading, dtSizes, "ItemSizeID", "ItemSizeName");
		dtDoctors = Doctors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDoctor, dtDoctors, "DoctorID", "DoctorName");
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_ClickCell(object sender, ClickCellEventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && (isLabModule ? ((UltraGridBase)ULGData).ActiveRow.Cells["ClientID"].Value : ((UltraGridBase)ULGData).ActiveRow.Cells["SubAccountID"].Value) != DBNull.Value && !Adding && !Updating)
		{
			drMaster = dtDetails.Select("ClientsGlassesHistoryID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ClientsGlassesHistoryID"].Value.ToString())[0];
			FillControls();
		}
	}

	private void ULGData_ClickCellButton(object sender, CellEventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Select" && (isLabModule ? ((UltraGridBase)ULGData).ActiveRow.Cells["ClientID"].Value : ((UltraGridBase)ULGData).ActiveRow.Cells["SubAccountID"].Value) != DBNull.Value && !Adding && !Updating)
		{
			ClientsGlassesHistoryID = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ClientsGlassesHistoryID"].Value.ToString());
			Close();
		}
	}

	private void txtRAdd_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
		if (cboRColorDistance.SelectedIndex == -1 || cboRSizeDistance.SelectedIndex == -1 || ((Control)(object)txtRAxisDistance).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال بيانات النظر", "Please Enter Glasses Data");
			e.Handled = true;
		}
	}

	private void txtRAdd_ValueChanged(object sender, EventArgs e)
	{
		if (((Control)(object)txtRAdd).Text != "" && decimal.TryParse(((Control)(object)txtRAdd).Text, out var result) && decimal.TryParse(((Control)(object)cboRColorDistance).Text, out var result2))
		{
			string text = (result2 + result).ToString();
			DataRow[] array = dtColors.Select("ColorName = '" + text + "' or   ColorName = '+" + text + "'");
			if (array.Length != 0)
			{
				((TextEditorControlBase)cboRColorReading).Value = array[0]["ColorID"];
				((TextEditorControlBase)cboRSizeReading).Value = ((TextEditorControlBase)cboRSizeDistance).Value;
				((Control)(object)txtRAxisReading).Text = ((Control)(object)txtRAxisDistance).Text;
			}
		}
	}

	private void txtLAdd_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
		if (cboLColorDistance.SelectedIndex == -1 || cboLSizeDistance.SelectedIndex == -1 || ((Control)(object)txtLAxisDistance).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال بيانات النظر", "Please Enter Glasses Data");
			e.Handled = true;
		}
	}

	private void txtLAdd_ValueChanged(object sender, EventArgs e)
	{
		if (((Control)(object)txtLAdd).Text != "" && decimal.TryParse(((Control)(object)txtLAdd).Text, out var result) && decimal.TryParse(((Control)(object)cboLColorDistance).Text, out var result2))
		{
			string text = (result2 + result).ToString();
			DataRow[] array = dtColors.Select("ColorName = '" + text + "' or   ColorName = '+" + text + "'");
			if (array.Length != 0)
			{
				((TextEditorControlBase)cboLColorReading).Value = array[0]["ColorID"];
				((TextEditorControlBase)cboLSizeReading).Value = ((TextEditorControlBase)cboLSizeDistance).Value;
				((Control)(object)txtLAxisReading).Text = ((Control)(object)txtLAxisDistance).Text;
			}
		}
	}

	private void btnDoctorAdd_Click(object sender, EventArgs e)
	{
		frmDoctors frmDoctors2 = new frmDoctors((cboDoctor.SelectedIndex == -1) ? (-1) : int.Parse(((TextEditorControlBase)cboDoctor).Value.ToString()));
		frmDoctors2.StartPosition = FormStartPosition.CenterParent;
		((Control)(object)frmDoctors2.lblTitle).Text = (GlobalVariables.IsArabic ? "الاطباء" : "Doctors");
		frmDoctors2.Tag = GlobalVariables.dtForms.Select("FormFullName = 'ERP.Clinics.MasterData.frmDoctors'")[0];
		frmDoctors2.ShowDialog();
		if (frmDoctors2.DoctorID != 0m)
		{
			dtDoctors = Doctors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboDoctor, dtDoctors, "DoctorID", "DoctorName");
			vlDoctors.ValueListItems.Clear();
			for (int i = 0; i < dtDoctors.Rows.Count; i++)
			{
				vlDoctors.ValueListItems.Add(dtDoctors.Rows[i]["DoctorID"], dtDoctors.Rows[i]["DoctorName"].ToString());
			}
			((TextEditorControlBase)cboDoctor).Value = frmDoctors2.DoctorID;
		}
	}

	private void Transpose(UltraComboEditor cboColor, UltraComboEditor cboSize, UltraTextEditor txtAxis, bool IsReading)
	{
		string text = "";
		text = ((!IsReading) ? (GlobalVariables.IsArabic ? "المسافات" : "Distance") : (GlobalVariables.IsArabic ? "القراءه" : "Reading"));
		decimal result;
		bool flag = decimal.TryParse(((Control)(object)cboColor).Text.ToString(), out result);
		decimal result2;
		bool flag2 = decimal.TryParse(((Control)(object)cboSize).Text.ToString(), out result2);
		decimal result3;
		bool flag3 = decimal.TryParse(((Control)(object)txtAxis).Text.ToString(), out result3);
		if (flag && flag2 && flag3)
		{
			decimal num = result + result2;
			decimal num2 = result2 * -1m;
			DataRow[] array = dtColors.Select("ColorName = '" + num + "' or   ColorName = '+" + num + "'");
			DataRow[] array2 = dtSizes.Select("ItemSizeName = '" + num2 + "' or   ItemSizeName = '+" + num2 + "'");
			if (array.Length != 0 && array2.Length != 0)
			{
				((TextEditorControlBase)cboColor).Value = array[0]["ColorID"];
				((TextEditorControlBase)cboSize).Value = array2[0]["ItemSizeID"];
				if (result3 <= 90m)
				{
					((Control)(object)txtAxis).Text = (result3 + 90m).ToString();
				}
				else
				{
					((Control)(object)txtAxis).Text = (result3 - 90m).ToString();
				}
			}
			else
			{
				GlobalVariables.InformationMB.Show("لا يمكن تحوير عدسات " + text + " لعدم وجود عدسات مطابقه للتحوير", "Cannot Transpose " + text + " Lenses Because There Is No Valid Lenses ");
			}
		}
		else
		{
			GlobalVariables.InformationMB.Show("لا يمكن تحوير عدسات " + text, "Cannot Transpose " + text + " Lenses");
		}
	}

	private void btnRTranspose_Click(object sender, EventArgs e)
	{
		Transpose(cboRColorDistance, cboRSizeDistance, txtRAxisDistance, IsReading: false);
		Transpose(cboRColorReading, cboRSizeReading, txtRAxisReading, IsReading: true);
	}

	private void btnLTranspose_Click(object sender, EventArgs e)
	{
		Transpose(cboLColorDistance, cboLSizeDistance, txtLAxisDistance, IsReading: false);
		Transpose(cboLColorReading, cboLSizeReading, txtLAxisReading, IsReading: true);
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
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected O, but got Unknown
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Expected O, but got Unknown
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Expected O, but got Unknown
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Expected O, but got Unknown
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Expected O, but got Unknown
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Expected O, but got Unknown
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Expected O, but got Unknown
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Expected O, but got Unknown
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Expected O, but got Unknown
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Expected O, but got Unknown
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Expected O, but got Unknown
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Expected O, but got Unknown
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Expected O, but got Unknown
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Expected O, but got Unknown
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Expected O, but got Unknown
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Expected O, but got Unknown
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Expected O, but got Unknown
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Expected O, but got Unknown
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Expected O, but got Unknown
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Expected O, but got Unknown
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Expected O, but got Unknown
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Expected O, but got Unknown
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Expected O, but got Unknown
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Expected O, but got Unknown
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Expected O, but got Unknown
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Expected O, but got Unknown
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Expected O, but got Unknown
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Expected O, but got Unknown
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Expected O, but got Unknown
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Expected O, but got Unknown
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Expected O, but got Unknown
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Expected O, but got Unknown
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Expected O, but got Unknown
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Expected O, but got Unknown
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Expected O, but got Unknown
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Expected O, but got Unknown
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Expected O, but got Unknown
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Expected O, but got Unknown
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Expected O, but got Unknown
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Expected O, but got Unknown
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Expected O, but got Unknown
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Expected O, but got Unknown
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Expected O, but got Unknown
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Expected O, but got Unknown
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Expected O, but got Unknown
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Expected O, but got Unknown
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Expected O, but got Unknown
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Expected O, but got Unknown
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Expected O, but got Unknown
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Expected O, but got Unknown
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Expected O, but got Unknown
		//IL_0e18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e22: Expected O, but got Unknown
		//IL_0e30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e3a: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.MasterData.frmPOSClientsGlassesHistory));
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
		this.txtCode = new UltraTextEditor();
		this.btnSearch = new UltraButton();
		this.btnPriveous = new UltraButton();
		this.btnNext = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.lblTitle2 = new UltraLabel();
		this.lblCode = new UltraLabel();
		this.btnCopyTo = new UltraButton();
		this.lblR = new UltraLabel();
		this.txtName = new UltraTextEditor();
		this.lblName = new UltraLabel();
		this.lblRSph = new UltraLabel();
		this.cboRColorDistance = new UltraComboEditor();
		this.lblReading = new UltraLabel();
		this.txtRAdd = new UltraTextEditor();
		this.lblDistance = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.ULGData = new UltraGrid();
		this.lblRAxis = new UltraLabel();
		this.lblRCyl = new UltraLabel();
		this.txtRAxisDistance = new UltraTextEditor();
		this.cboRSizeDistance = new UltraComboEditor();
		this.cboRSizeReading = new UltraComboEditor();
		this.txtRAxisReading = new UltraTextEditor();
		this.cboRColorReading = new UltraComboEditor();
		this.lblRAdd = new UltraLabel();
		this.lblLAdd = new UltraLabel();
		this.cboLSizeReading = new UltraComboEditor();
		this.txtLAxisReading = new UltraTextEditor();
		this.cboLColorReading = new UltraComboEditor();
		this.cboLSizeDistance = new UltraComboEditor();
		this.txtLAxisDistance = new UltraTextEditor();
		this.lblLCyl = new UltraLabel();
		this.lblLAxis = new UltraLabel();
		this.txtLAdd = new UltraTextEditor();
		this.lblLSph = new UltraLabel();
		this.cboLColorDistance = new UltraComboEditor();
		this.lblL = new UltraLabel();
		this.lblIPD = new UltraLabel();
		this.lblIPDDistance = new UltraLabel();
		this.lblIPDReading = new UltraLabel();
		this.txtIPDDistance = new UltraTextEditor();
		this.txtIPDReading = new UltraTextEditor();
		this.lblmmReading = new UltraLabel();
		this.lblmmDistance = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.lblDate = new UltraLabel();
		this.lblDoctor = new UltraLabel();
		this.cboDoctor = new UltraComboEditor();
		this.UGBGlassesHistory = new UltraGroupBox();
		this.btnDoctorAdd = new UltraButton();
		this.btnLTranspose = new UltraButton();
		this.btnRTranspose = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboRColorDistance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRAdd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRAxisDistance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboRSizeDistance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboRSizeReading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRAxisReading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboRColorReading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLSizeReading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLAxisReading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLColorReading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLSizeDistance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLAxisDistance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLAdd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLColorDistance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtIPDDistance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtIPDReading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDoctor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBGlassesHistory).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).SuspendLayout();
		base.SuspendLayout();
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
		((AppearanceBase)val).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val, "appearance1");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		((AppearanceBase)val2).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val2).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val2).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val2).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val2, "appearance2");
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val2;
		base.cboTransactionBranch.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboTransactionBranch.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.txtCode, "txtCode");
		((System.Windows.Forms.Control)(object)this.txtCode).Name = "txtCode";
		resources.ApplyResources(this.btnSearch, "btnSearch");
		((AppearanceBase)val3).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.btnSearch).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.btnSearch).Name = "btnSearch";
		resources.ApplyResources(this.btnPriveous, "btnPriveous");
		((AppearanceBase)val4).Image = ERP.Properties.Resources.BarLeft;
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.btnPriveous).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.btnPriveous).Name = "btnPriveous";
		resources.ApplyResources(this.btnNext, "btnNext");
		((AppearanceBase)val5).Image = ERP.Properties.Resources.BarRight;
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.btnNext).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.btnNext).Name = "btnNext";
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val6).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val7).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblCode, "lblCode");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val8).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.lblCode).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.lblCode).Name = "lblCode";
		((UltraControlBase)this.lblCode).UseAppStyling = false;
		resources.ApplyResources(this.btnCopyTo, "btnCopyTo");
		((System.Windows.Forms.Control)(object)this.btnCopyTo).Name = "btnCopyTo";
		resources.ApplyResources(this.lblR, "lblR");
		this.lblR.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblR).Name = "lblR";
		((ControlBase)this.lblR).WrapText = false;
		resources.ApplyResources(this.txtName, "txtName");
		((System.Windows.Forms.Control)(object)this.txtName).Name = "txtName";
		resources.ApplyResources(this.lblName, "lblName");
		this.lblName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblName).Name = "lblName";
		((ControlBase)this.lblName).WrapText = false;
		resources.ApplyResources(this.lblRSph, "lblRSph");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.lblRSph).Appearance = (AppearanceBase)(object)val9;
		this.lblRSph.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRSph).Name = "lblRSph";
		((ControlBase)this.lblRSph).WrapText = false;
		resources.ApplyResources(this.cboRColorDistance, "cboRColorDistance");
		this.cboRColorDistance.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboRColorDistance).Name = "cboRColorDistance";
		resources.ApplyResources(this.lblReading, "lblReading");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.lblReading).Appearance = (AppearanceBase)(object)val10;
		this.lblReading.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReading).Name = "lblReading";
		((ControlBase)this.lblReading).WrapText = false;
		resources.ApplyResources(this.txtRAdd, "txtRAdd");
		((System.Windows.Forms.Control)(object)this.txtRAdd).Name = "txtRAdd";
		((TextEditorControlBase)this.txtRAdd).ValueChanged += new System.EventHandler(txtRAdd_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtRAdd).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtRAdd_KeyPress);
		resources.ApplyResources(this.lblDistance, "lblDistance");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.lblDistance).Appearance = (AppearanceBase)(object)val11;
		this.lblDistance.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDistance).Name = "lblDistance";
		((ControlBase)this.lblDistance).WrapText = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.lblNotes).Appearance = (AppearanceBase)(object)val12;
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.ULGData, "ULGData");
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowColMoving = (AllowColMoving)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowColSizing = (AllowColSizing)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeCell = (SelectType)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeCol = (SelectType)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeRow = (SelectType)2;
		((UltraGridBase)this.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		((UltraControlBase)this.ULGData).UseOsThemes = (DefaultableBoolean)2;
		this.ULGData.AfterEnterEditMode += new System.EventHandler(ULGData_AfterEnterEditMode);
		this.ULGData.ClickCellButton += new CellEventHandler(ULGData_ClickCellButton);
		this.ULGData.ClickCell += new ClickCellEventHandler(ULGData_ClickCell);
		resources.ApplyResources(this.lblRAxis, "lblRAxis");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val13, "appearance13");
		((ControlBase)this.lblRAxis).Appearance = (AppearanceBase)(object)val13;
		this.lblRAxis.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRAxis).Name = "lblRAxis";
		((ControlBase)this.lblRAxis).WrapText = false;
		resources.ApplyResources(this.lblRCyl, "lblRCyl");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val14, "appearance14");
		((ControlBase)this.lblRCyl).Appearance = (AppearanceBase)(object)val14;
		this.lblRCyl.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRCyl).Name = "lblRCyl";
		((ControlBase)this.lblRCyl).WrapText = false;
		resources.ApplyResources(this.txtRAxisDistance, "txtRAxisDistance");
		((System.Windows.Forms.Control)(object)this.txtRAxisDistance).Name = "txtRAxisDistance";
		resources.ApplyResources(this.cboRSizeDistance, "cboRSizeDistance");
		this.cboRSizeDistance.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboRSizeDistance).Name = "cboRSizeDistance";
		resources.ApplyResources(this.cboRSizeReading, "cboRSizeReading");
		this.cboRSizeReading.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboRSizeReading).Name = "cboRSizeReading";
		resources.ApplyResources(this.txtRAxisReading, "txtRAxisReading");
		((System.Windows.Forms.Control)(object)this.txtRAxisReading).Name = "txtRAxisReading";
		resources.ApplyResources(this.cboRColorReading, "cboRColorReading");
		this.cboRColorReading.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboRColorReading).Name = "cboRColorReading";
		resources.ApplyResources(this.lblRAdd, "lblRAdd");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val15, "appearance15");
		((ControlBase)this.lblRAdd).Appearance = (AppearanceBase)(object)val15;
		this.lblRAdd.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRAdd).Name = "lblRAdd";
		((ControlBase)this.lblRAdd).WrapText = false;
		resources.ApplyResources(this.lblLAdd, "lblLAdd");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val16, "appearance16");
		((ControlBase)this.lblLAdd).Appearance = (AppearanceBase)(object)val16;
		this.lblLAdd.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLAdd).Name = "lblLAdd";
		((ControlBase)this.lblLAdd).WrapText = false;
		resources.ApplyResources(this.cboLSizeReading, "cboLSizeReading");
		this.cboLSizeReading.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboLSizeReading).Name = "cboLSizeReading";
		resources.ApplyResources(this.txtLAxisReading, "txtLAxisReading");
		((System.Windows.Forms.Control)(object)this.txtLAxisReading).Name = "txtLAxisReading";
		resources.ApplyResources(this.cboLColorReading, "cboLColorReading");
		this.cboLColorReading.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboLColorReading).Name = "cboLColorReading";
		resources.ApplyResources(this.cboLSizeDistance, "cboLSizeDistance");
		this.cboLSizeDistance.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboLSizeDistance).Name = "cboLSizeDistance";
		resources.ApplyResources(this.txtLAxisDistance, "txtLAxisDistance");
		((System.Windows.Forms.Control)(object)this.txtLAxisDistance).Name = "txtLAxisDistance";
		resources.ApplyResources(this.lblLCyl, "lblLCyl");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val17, "appearance17");
		((ControlBase)this.lblLCyl).Appearance = (AppearanceBase)(object)val17;
		this.lblLCyl.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLCyl).Name = "lblLCyl";
		((ControlBase)this.lblLCyl).WrapText = false;
		resources.ApplyResources(this.lblLAxis, "lblLAxis");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val18).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val18, "appearance18");
		((ControlBase)this.lblLAxis).Appearance = (AppearanceBase)(object)val18;
		this.lblLAxis.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLAxis).Name = "lblLAxis";
		((ControlBase)this.lblLAxis).WrapText = false;
		resources.ApplyResources(this.txtLAdd, "txtLAdd");
		((System.Windows.Forms.Control)(object)this.txtLAdd).Name = "txtLAdd";
		((TextEditorControlBase)this.txtLAdd).ValueChanged += new System.EventHandler(txtLAdd_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtLAdd).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtLAdd_KeyPress);
		resources.ApplyResources(this.lblLSph, "lblLSph");
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val19, "appearance19");
		((ControlBase)this.lblLSph).Appearance = (AppearanceBase)(object)val19;
		this.lblLSph.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLSph).Name = "lblLSph";
		((ControlBase)this.lblLSph).WrapText = false;
		resources.ApplyResources(this.cboLColorDistance, "cboLColorDistance");
		this.cboLColorDistance.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboLColorDistance).Name = "cboLColorDistance";
		resources.ApplyResources(this.lblL, "lblL");
		this.lblL.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblL).Name = "lblL";
		((ControlBase)this.lblL).WrapText = false;
		resources.ApplyResources(this.lblIPD, "lblIPD");
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val20, "appearance20");
		((ControlBase)this.lblIPD).Appearance = (AppearanceBase)(object)val20;
		this.lblIPD.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblIPD).Name = "lblIPD";
		((ControlBase)this.lblIPD).WrapText = false;
		resources.ApplyResources(this.lblIPDDistance, "lblIPDDistance");
		((AppearanceBase)val21).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val21).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val21, "appearance21");
		((ControlBase)this.lblIPDDistance).Appearance = (AppearanceBase)(object)val21;
		this.lblIPDDistance.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblIPDDistance).Name = "lblIPDDistance";
		((ControlBase)this.lblIPDDistance).WrapText = false;
		resources.ApplyResources(this.lblIPDReading, "lblIPDReading");
		((AppearanceBase)val22).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val22).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val22, "appearance22");
		((ControlBase)this.lblIPDReading).Appearance = (AppearanceBase)(object)val22;
		this.lblIPDReading.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblIPDReading).Name = "lblIPDReading";
		((ControlBase)this.lblIPDReading).WrapText = false;
		resources.ApplyResources(this.txtIPDDistance, "txtIPDDistance");
		((System.Windows.Forms.Control)(object)this.txtIPDDistance).Name = "txtIPDDistance";
		resources.ApplyResources(this.txtIPDReading, "txtIPDReading");
		((System.Windows.Forms.Control)(object)this.txtIPDReading).Name = "txtIPDReading";
		resources.ApplyResources(this.lblmmReading, "lblmmReading");
		((AppearanceBase)val23).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val23).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val23, "appearance23");
		((ControlBase)this.lblmmReading).Appearance = (AppearanceBase)(object)val23;
		this.lblmmReading.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblmmReading).Name = "lblmmReading";
		((ControlBase)this.lblmmReading).WrapText = false;
		resources.ApplyResources(this.lblmmDistance, "lblmmDistance");
		((AppearanceBase)val24).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val24).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val24, "appearance24");
		((ControlBase)this.lblmmDistance).Appearance = (AppearanceBase)(object)val24;
		this.lblmmDistance.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblmmDistance).Name = "lblmmDistance";
		((ControlBase)this.lblmmDistance).WrapText = false;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		resources.ApplyResources(this.lblDate, "lblDate");
		((AppearanceBase)val25).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val25).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val25, "appearance25");
		((ControlBase)this.lblDate).Appearance = (AppearanceBase)(object)val25;
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.lblDoctor, "lblDoctor");
		((AppearanceBase)val26).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val26).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val26, "appearance26");
		((ControlBase)this.lblDoctor).Appearance = (AppearanceBase)(object)val26;
		this.lblDoctor.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDoctor).Name = "lblDoctor";
		((ControlBase)this.lblDoctor).WrapText = false;
		resources.ApplyResources(this.cboDoctor, "cboDoctor");
		this.cboDoctor.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboDoctor).Name = "cboDoctor";
		resources.ApplyResources(this.UGBGlassesHistory, "UGBGlassesHistory");
		this.UGBGlassesHistory.BorderStyle = (GroupBoxBorderStyle)4;
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.btnDoctorAdd);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.btnLTranspose);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.btnRTranspose);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblName);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.cboDoctor);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.txtName);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblDoctor);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblR);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.cboRColorDistance);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblRSph);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblmmDistance);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblReading);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblmmReading);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblDistance);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.txtIPDReading);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.txtRAdd);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblRAxis);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblRCyl);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.txtRAxisDistance);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.cboRSizeDistance);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.cboRColorReading);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.txtRAxisReading);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.cboRSizeReading);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblRAdd);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblL);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.cboLColorDistance);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblLSph);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.txtLAdd);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblLAxis);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblLCyl);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.txtIPDDistance);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.txtLAxisDistance);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblIPDReading);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.cboLSizeDistance);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblIPDDistance);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.cboLColorReading);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblIPD);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.txtLAxisReading);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.lblLAdd);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Controls.Add((System.Windows.Forms.Control)(object)this.cboLSizeReading);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).Name = "UGBGlassesHistory";
		resources.ApplyResources(this.btnDoctorAdd, "btnDoctorAdd");
		((AppearanceBase)val27).Image = ERP.Properties.Resources.New;
		resources.ApplyResources(val27, "appearance27");
		((ControlBase)this.btnDoctorAdd).Appearance = (AppearanceBase)(object)val27;
		((System.Windows.Forms.Control)(object)this.btnDoctorAdd).Name = "btnDoctorAdd";
		((System.Windows.Forms.Control)(object)this.btnDoctorAdd).Click += new System.EventHandler(btnDoctorAdd_Click);
		resources.ApplyResources(this.btnLTranspose, "btnLTranspose");
		((AppearanceBase)val28).Image = resources.GetObject("appearance28.Image");
		resources.ApplyResources(val28, "appearance28");
		((ControlBase)this.btnLTranspose).Appearance = (AppearanceBase)(object)val28;
		((System.Windows.Forms.Control)(object)this.btnLTranspose).Name = "btnLTranspose";
		((System.Windows.Forms.Control)(object)this.btnLTranspose).Click += new System.EventHandler(btnLTranspose_Click);
		resources.ApplyResources(this.btnRTranspose, "btnRTranspose");
		((AppearanceBase)val29).Image = resources.GetObject("appearance29.Image");
		resources.ApplyResources(val29, "appearance29");
		((ControlBase)this.btnRTranspose).Appearance = (AppearanceBase)(object)val29;
		((System.Windows.Forms.Control)(object)this.btnRTranspose).Name = "btnRTranspose";
		((System.Windows.Forms.Control)(object)this.btnRTranspose).Click += new System.EventHandler(btnRTranspose_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBGlassesHistory);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCopyTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPriveous);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNext);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmPOSClientsGlassesHistory";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBGlassesHistory, 0);
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboRColorDistance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRAdd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRAxisDistance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboRSizeDistance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboRSizeReading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRAxisReading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboRColorReading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLSizeReading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLAxisReading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLColorReading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLSizeDistance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLAxisDistance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLAdd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLColorDistance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtIPDDistance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtIPDReading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDoctor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBGlassesHistory).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.UGBGlassesHistory).PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
