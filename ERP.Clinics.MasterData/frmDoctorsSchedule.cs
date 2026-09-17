using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Clinics;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Clinics.MasterData;

public class frmDoctorsSchedule : frmHeaderDetails
{
	private DataTable dtDoctors;

	private DataTable dtClinics;

	private DataTable dtScheduleDetailsPeriods;

	private ValueList vlClinics = new ValueList();

	private int newID = -100000;

	private DataSet ds;

	private IContainer components = null;

	private UltraNumericEditor UNWeeksCount;

	private UltraLabel lblWeeksCount;

	public UltraButton btnPatientSearch;

	private UltraLabel lblDoctor;

	private UltraComboEditor cboDoctor;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	public UltraButton btnGenerate;

	public frmDoctorsSchedule()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		TableName = "CL_DoctorsSchedule";
		IDCol = "DoctorScheduleID";
		NoCol = "DoctorScheduleCode";
		DateCol = "GetDate()";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtDoctors = Doctors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDoctor, dtDoctors, "DoctorID", "DoctorName");
		dtClinics = BusinessLayer.Clinics.Clinics.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlClinics.ValueListItems.Clear();
		for (int i = 0; i < dtClinics.Rows.Count; i++)
		{
			vlClinics.ValueListItems.Add(dtClinics.Rows[i]["ClinicID"], dtClinics.Rows[i]["ClinicName"].ToString());
		}
		dtDetails = DoctorsScheduleDetails.SelectByDoctorScheduleID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtScheduleDetailsPeriods = DoctorsScheduleDetailsPeriods.SelectByDoctorScheduleID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		ds = new DataSet();
		ds.Tables.Add(dtDetails);
		ds.Tables.Add(dtScheduleDetailsPeriods);
		ds.Tables[0].TableName = "dtDetails";
		ds.Tables[1].TableName = "dtScheduleDetailsPeriods";
		ds.Relations.Add(ds.Tables[0].Columns["DoctorScheduleDetailID"], ds.Tables[1].Columns["DoctorScheduleDetailID"]);
		((UltraGridBase)ULGData).DataSource = ds;
		InitGrid();
	}

	public override void InitGrid()
	{
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Override.AllowAddNew = (AllowAddNew)6;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DayNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "اليوم" : "Day");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DayNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DayNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DayNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "-" : "-");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DayNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DayNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["DoctorScheduleDetailPeriodID"].DefaultCellValue = "-1";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["FromTime"].Header).Caption = (GlobalVariables.IsArabic ? "من" : "From");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ToTime"].Header).Caption = (GlobalVariables.IsArabic ? "الي" : "To");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ClinicID"].Header).Caption = (GlobalVariables.IsArabic ? "العيادة" : "Clinic");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["MaxDiagnoseCount"].Header).Caption = (GlobalVariables.IsArabic ? "أقصى عدد للحالات" : "Max Diagnose Count");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["FromTime"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ToTime"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ClinicID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["MaxDiagnoseCount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["FromTime"].MaskInput = "hh:mm tt";
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ToTime"].MaskInput = "hh:mm tt";
		((UltraDateTimeEditor)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["FromTime"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
		((UltraDateTimeEditor)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["FromTime"].EditorComponent).DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		((UltraDateTimeEditor)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ToTime"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
		((UltraDateTimeEditor)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ToTime"].EditorComponent).DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ClinicID"].ValueList = (IValueList)(object)vlClinics;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["MaxDiagnoseCount"].DefaultCellValue = 0;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = DoctorsSchedule.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
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
		if (drMaster != null)
		{
			((Control)(object)txtCode).Text = drMaster["DoctorScheduleCode"].ToString();
			((TextEditorControlBase)cboDoctor).ValueChanged -= cboDoctor_ValueChanged;
			((UltraNumericEditorBase)UNWeeksCount).ValueChanged -= UNWeeksCount_ValueChanged;
			((TextEditorControlBase)cboDoctor).Value = drMaster["DoctorID"];
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)UNWeeksCount).Text = drMaster["WeeksCount"].ToString();
			((TextEditorControlBase)cboDoctor).ValueChanged += cboDoctor_ValueChanged;
			((UltraNumericEditorBase)UNWeeksCount).ValueChanged += UNWeeksCount_ValueChanged;
			dtDetails = DoctorsScheduleDetails.SelectByDoctorScheduleID(drMaster["DoctorScheduleID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtScheduleDetailsPeriods = DoctorsScheduleDetailsPeriods.SelectByDoctorScheduleID(drMaster["DoctorScheduleID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			ds = new DataSet();
			ds.Tables.Add(dtDetails);
			ds.Tables.Add(dtScheduleDetailsPeriods);
			ds.Tables[0].TableName = "dtDetails";
			ds.Tables[1].TableName = "dtScheduleDetailsPeriods";
			ds.Relations.Add(ds.Tables[0].Columns["DoctorScheduleDetailID"], ds.Tables[1].Columns["DoctorScheduleDetailID"]);
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
		((Control)(object)btnGenerate).Visible = NavMode;
		((EditorButtonControlBase)cboDoctor).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)UNWeeksCount).ReadOnly = NavMode;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		((Control)(object)txtCode).Text = (Adding ? DoctorsSchedule.GetCode(IsFromServer: true) : "");
		((TextEditorControlBase)cboDoctor).ValueChanged -= cboDoctor_ValueChanged;
		((UltraNumericEditorBase)UNWeeksCount).ValueChanged -= UNWeeksCount_ValueChanged;
		cboDoctor.SelectedIndex = -1;
		((TextEditorControlBase)txtNotes).Clear();
		UNWeeksCount.Value = "1";
		((UltraNumericEditorBase)UNWeeksCount).ValueChanged += UNWeeksCount_ValueChanged;
		((TextEditorControlBase)cboDoctor).ValueChanged += cboDoctor_ValueChanged;
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[1].Rows.Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[0].Rows.Clear();
		newID = -100000;
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال الكود" : "Please Enter The Code");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (cboDoctor.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الطبيب" : "Please Select The Doctor");
			((TextEditorControlBase)cboDoctor).Focus();
			cboDoctor.DropDown();
			return false;
		}
		if (cboDoctor.SelectedIndex > -1 && Adding && Doctors.HasSchedule(((TextEditorControlBase)cboDoctor).Value.ToString(), IsFromServer: false))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "يوجد جدول للمواعيد لهذا الطبيب" : "This Doctor Already Has A Schedule");
			((TextEditorControlBase)cboDoctor).Focus();
			cboDoctor.DropDown();
			return false;
		}
		if (UNWeeksCount.Value == null || ((Control)(object)UNWeeksCount).Text == "" || int.Parse(((Control)(object)UNWeeksCount).Text) < 1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال عدد الأسابيع" : "Please Enter Weeks Count");
			((Control)(object)UNWeeksCount).Focus();
			return false;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تفاصيل للجدول الزمني" : "Please Insert Details To The Schedule ");
			return false;
		}
		if (Main.CheckForValue("CL_DoctorsSchedule", "DoctorScheduleCode", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["DoctorScheduleCode"].ToString(), IsFromServer: true) > 0)
		{
			string code = DoctorsSchedule.GetCode(IsFromServer: true);
			GlobalVariables.QuestionMB.Show("رقم هذا الجدول متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Schedule Code Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = code;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["FromTime"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("برجاء إدخال وقت البداية", "Please Insert From Time");
					((GridItemBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["FromTime"]).Selected = true;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ToTime"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("برجاء إدخال وقت النهاية", "Please Insert To Time");
					((GridItemBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ToTime"]).Selected = true;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ClinicID"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء ادخال اسم العيادة  ", "Please Enter Clinic Name");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ClinicID"];
					((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ClinicID"].DroppedDown = true;
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["MaxDiagnoseCount"].Value == DBNull.Value || int.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["MaxDiagnoseCount"].Value.ToString()) < 1)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء ادخال أقصى عدد للحالات  ", "Please Enter Max Diagnose Count ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["MaxDiagnoseCount"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; k++)
				{
					if (k != j && ((DateTime.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["FromTime"].Value.ToString()) >= DateTime.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["FromTime"].Value.ToString()) && DateTime.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["FromTime"].Value.ToString()) <= DateTime.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["ToTime"].Value.ToString())) || (DateTime.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ToTime"].Value.ToString()) >= DateTime.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["FromTime"].Value.ToString()) && DateTime.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ToTime"].Value.ToString()) <= DateTime.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["ToTime"].Value.ToString())) || (DateTime.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["FromTime"].Value.ToString()) <= DateTime.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["FromTime"].Value.ToString()) && DateTime.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ToTime"].Value.ToString()) >= DateTime.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k].Cells["ToTime"].Value.ToString()))))
					{
						GlobalVariables.InformationMB.Show("هذا الوقت واقع فى فترة من قبل", "this Time in Another Period");
						((GridItemBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[k]).Selected = true;
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
			int num = DoctorsSchedule.Insert_Update("-1", ((Control)(object)txtCode).Text, (cboDoctor.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDoctor).Value.ToString(), UNWeeksCount.Value.ToString(), ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				int num2 = DoctorsScheduleDetails.Insert_Update("-1", num.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["DayNameAr"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["DayNameEn"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["PlanOrder"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
				{
					DoctorsScheduleDetailsPeriods.Insert_Update("-1", num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["FromTime"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ToTime"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ClinicID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ClinicID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["MaxDiagnoseCount"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				}
			}
			Main.EndBulkTrans(FromServer: true);
			RowID = num.ToString();
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
			int num = DoctorsSchedule.Insert_Update(drMaster["DoctorScheduleID"].ToString(), ((Control)(object)txtCode).Text, (cboDoctor.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDoctor).Value.ToString(), UNWeeksCount.Value.ToString(), ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			string text = ",";
			string text2 = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["DoctorScheduleDetailID"].Value.ToString() + ",";
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
				{
					text2 = text2 + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["DoctorScheduleDetailPeriodID"].Value.ToString() + ",";
				}
			}
			Main.DeleteForUpdate("CL_DoctorsScheduleDetailsPeriods", "DoctorScheduleID", drMaster["DoctorScheduleID"].ToString(), "DoctorScheduleDetailPeriodID", text2);
			Main.DeleteForUpdate("CL_DoctorsScheduleDetails", "DoctorScheduleID", drMaster["DoctorScheduleID"].ToString(), "DoctorScheduleDetailID", text);
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
			{
				int num2 = DoctorsScheduleDetails.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[k].Cells["DoctorScheduleDetailID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[k].Cells["DoctorScheduleDetailID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[k].Cells["DoctorScheduleDetailID"].Value.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[k].Cells["DayNameAr"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].Cells["DayNameEn"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].Cells["PlanOrder"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows).Count; l++)
				{
					DoctorsScheduleDetailsPeriods.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["DoctorScheduleDetailPeriodID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["DoctorScheduleDetailPeriodID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["DoctorScheduleDetailPeriodID"].Value.ToString(), num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["FromTime"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["ToTime"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["ClinicID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["ClinicID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["MaxDiagnoseCount"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
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
		base.DeleteData();
		Main.StartBulkTrans(FromServer: true);
		try
		{
			DoctorsScheduleDetailsPeriods.DeleteByDoctorScheduleID(drMaster["DoctorScheduleID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			DoctorsScheduleDetails.DeleteByDoctorScheduleID(drMaster["DoctorScheduleID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			DoctorsSchedule.Delete(drMaster["DoctorScheduleID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public void fillDetails(int Count)
	{
		int num = 0;
		if (dtDetails.Rows.Count > 0)
		{
			num = Convert.ToInt32(dtDetails.Compute("max(PlanOrder) ", ""));
		}
		for (int i = 0; i < Count; i++)
		{
			DataRow dataRow = dtDetails.NewRow();
			dataRow["DoctorScheduleID"] = "-1";
			dataRow["DoctorScheduleDetailID"] = ++newID;
			dataRow["DayNameAr"] = "السبت";
			dataRow["DayNameEn"] = "Saturday";
			dataRow["PlanOrder"] = ++num;
			dataRow["Deleted"] = false;
			dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
			dtDetails.Rows.Add(dataRow);
			dataRow = dtDetails.NewRow();
			dataRow["DoctorScheduleID"] = "-1";
			dataRow["DoctorScheduleDetailID"] = ++newID;
			dataRow["DayNameAr"] = "الاحد";
			dataRow["DayNameEn"] = "Sunday";
			dataRow["PlanOrder"] = ++num;
			dataRow["Deleted"] = false;
			dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
			dtDetails.Rows.Add(dataRow);
			dataRow = dtDetails.NewRow();
			dataRow["DoctorScheduleID"] = "-1";
			dataRow["DoctorScheduleDetailID"] = ++newID;
			dataRow["DayNameAr"] = "الاثنين";
			dataRow["DayNameEn"] = "Monday";
			dataRow["PlanOrder"] = ++num;
			dataRow["Deleted"] = false;
			dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
			dtDetails.Rows.Add(dataRow);
			dataRow = dtDetails.NewRow();
			dataRow["DoctorScheduleID"] = "-1";
			dataRow["DoctorScheduleDetailID"] = ++newID;
			dataRow["DayNameAr"] = "الثلاثاء";
			dataRow["DayNameEn"] = "Tuesday";
			dataRow["PlanOrder"] = ++num;
			dataRow["Deleted"] = false;
			dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
			dtDetails.Rows.Add(dataRow);
			dataRow = dtDetails.NewRow();
			dataRow["DoctorScheduleID"] = "-1";
			dataRow["DoctorScheduleDetailID"] = ++newID;
			dataRow["DayNameAr"] = "الاربعاء";
			dataRow["DayNameEn"] = "Wednesday";
			dataRow["PlanOrder"] = ++num;
			dataRow["Deleted"] = false;
			dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
			dtDetails.Rows.Add(dataRow);
			dataRow = dtDetails.NewRow();
			dataRow["DoctorScheduleID"] = "-1";
			dataRow["DoctorScheduleDetailID"] = ++newID;
			dataRow["DayNameAr"] = "الخميس";
			dataRow["DayNameEn"] = "Thursday";
			dataRow["PlanOrder"] = ++num;
			dataRow["Deleted"] = false;
			dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
			dtDetails.Rows.Add(dataRow);
			dataRow = dtDetails.NewRow();
			dataRow["DoctorScheduleID"] = "-1";
			dataRow["DoctorScheduleDetailID"] = ++newID;
			dataRow["DayNameAr"] = "الجمعة";
			dataRow["DayNameEn"] = "Friday";
			dataRow["PlanOrder"] = ++num;
			dataRow["Deleted"] = false;
			dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
			dtDetails.Rows.Add(dataRow);
		}
	}

	public override void btnRefreshDataClick()
	{
		dtDoctors = Doctors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDoctor, dtDoctors, "DoctorID", "DoctorName");
		dtClinics = BusinessLayer.Clinics.Clinics.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlClinics.ValueListItems.Clear();
		for (int i = 0; i < dtClinics.Rows.Count; i++)
		{
			vlClinics.ValueListItems.Add(dtClinics.Rows[i]["ClinicID"], dtClinics.Rows[i]["ClinicName"].ToString());
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.DoctorsScheduleSearchReport(IsFromServer: true);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["DoctorScheduleID"].ToString();
			FillData();
		}
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		if (((GridItemBase)e.Row).Band.Index == 0)
		{
			e.Row.Cells["DoctorScheduleDetailID"].Value = ++newID;
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((GridItemBase)ULGData.ActiveCell).Band.Index == 0)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void btnPatientSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.DoctorsSearch(IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboDoctor).Value = num;
		}
	}

	private void cboDoctor_ValueChanged(object sender, EventArgs e)
	{
		if (cboDoctor.SelectedIndex > -1)
		{
			if (!Doctors.HasSchedule(((TextEditorControlBase)cboDoctor).Value.ToString(), IsFromServer: false))
			{
				((DataSet)((UltraGridBase)ULGData).DataSource).Tables[1].Rows.Clear();
				((DataSet)((UltraGridBase)ULGData).DataSource).Tables[0].Rows.Clear();
				dtDetails = null;
				dtDetails = DoctorsScheduleDetails.SelectByDoctorScheduleID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
				fillDetails(int.Parse(UNWeeksCount.Value.ToString()));
				dtScheduleDetailsPeriods = DoctorsScheduleDetailsPeriods.SelectByDoctorScheduleID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
				ds = new DataSet();
				ds.Tables.Add(dtDetails);
				ds.Tables.Add(dtScheduleDetailsPeriods);
				ds.Tables[0].TableName = "dtDetails";
				ds.Tables[1].TableName = "dtScheduleDetailsPeriods";
				ds.Relations.Add(ds.Tables[0].Columns["DoctorScheduleDetailID"], ds.Tables[1].Columns["DoctorScheduleDetailID"]);
				((UltraGridBase)ULGData).DataSource = ds;
				InitGrid();
			}
			else
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "يوجد جدول للمواعيد لهذا الطبيب" : "This Doctor Already Has A Schedule");
			}
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "MaxDiagnoseCount" && ULGData.ActiveCell.Value == DBNull.Value)
		{
			ULGData.ActiveCell.Value = 0;
		}
	}

	private void UNWeeksCount_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((DataSet)((UltraGridBase)ULGData).DataSource).Tables[1].Rows.Clear();
			((DataSet)((UltraGridBase)ULGData).DataSource).Tables[0].Rows.Clear();
			dtDetails = null;
			dtDetails = DoctorsScheduleDetails.SelectByDoctorScheduleID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			fillDetails(int.Parse(UNWeeksCount.Value.ToString()));
			dtScheduleDetailsPeriods = DoctorsScheduleDetailsPeriods.SelectByDoctorScheduleID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			ds = new DataSet();
			ds.Tables.Add(dtDetails);
			ds.Tables.Add(dtScheduleDetailsPeriods);
			ds.Tables[0].TableName = "dtDetails";
			ds.Tables[1].TableName = "dtScheduleDetailsPeriods";
			ds.Relations.Add(ds.Tables[0].Columns["DoctorScheduleDetailID"], ds.Tables[1].Columns["DoctorScheduleDetailID"]);
			((UltraGridBase)ULGData).DataSource = ds;
			InitGrid();
		}
		else if (Updating)
		{
			int num = int.Parse(UNWeeksCount.Value.ToString()) - int.Parse(drMaster["WeeksCount"].ToString());
			if (num > 0)
			{
				DataTable dataTable = dtDetails.Copy();
				dtDetails = DoctorsScheduleDetails.SelectByDoctorScheduleID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
				dtDetails = dataTable.Copy();
				fillDetails(num);
				dataTable = dtScheduleDetailsPeriods.Copy();
				dtScheduleDetailsPeriods = DoctorsScheduleDetailsPeriods.SelectByDoctorScheduleID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
				dtScheduleDetailsPeriods = dataTable.Copy();
				ds = new DataSet();
				ds.Tables.Add(dtDetails);
				ds.Tables.Add(dtScheduleDetailsPeriods);
				ds.Tables[0].TableName = "dtDetails";
				ds.Tables[1].TableName = "dtScheduleDetailsPeriods";
				ds.Relations.Add(ds.Tables[0].Columns["DoctorScheduleDetailID"], ds.Tables[1].Columns["DoctorScheduleDetailID"]);
				((UltraGridBase)ULGData).DataSource = ds;
				InitGrid();
			}
			else if (num < 0)
			{
				((DataSet)((UltraGridBase)ULGData).DataSource).Tables[1].Rows.Clear();
				((DataSet)((UltraGridBase)ULGData).DataSource).Tables[0].Rows.Clear();
				dtDetails = null;
				dtDetails = DoctorsScheduleDetails.SelectByDoctorScheduleID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
				fillDetails(int.Parse(UNWeeksCount.Value.ToString()));
				dtScheduleDetailsPeriods = DoctorsScheduleDetailsPeriods.SelectByDoctorScheduleID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
				ds = new DataSet();
				ds.Tables.Add(dtDetails);
				ds.Tables.Add(dtScheduleDetailsPeriods);
				ds.Tables[0].TableName = "dtDetails";
				ds.Tables[1].TableName = "dtScheduleDetailsPeriods";
				ds.Relations.Add(ds.Tables[0].Columns["DoctorScheduleDetailID"], ds.Tables[1].Columns["DoctorScheduleDetailID"]);
				((UltraGridBase)ULGData).DataSource = ds;
				InitGrid();
			}
		}
	}

	private void btnGenerate_Click(object sender, EventArgs e)
	{
		if (drMaster != null)
		{
			frmDoctorsSchedule_Generate frmDoctorsSchedule_Generate2 = new frmDoctorsSchedule_Generate(int.Parse(drMaster["DoctorScheduleID"].ToString()), Convert.ToInt32(((TextEditorControlBase)cboDoctor).Value), Convert.ToInt32(UNWeeksCount.Value));
			frmDoctorsSchedule_Generate2.ShowDialog();
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
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفرع آخر", "Cannot Update This Transaction Because It Related to Another Branch ");
			return;
		}
		if (ClosedPeriod)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Delete This Transaction Because It Related to ClosedPeriod ");
			return;
		}
		if (int.Parse(Main.ExecuteQuery_DataTable(" Select IsNull(COUNT(*),0) RowsCount From CL_ClinicsSchedule Where Deleted = 0 And DoctorScheduleDetailPeriodID in (Select DoctorScheduleDetailPeriodID From CL_DoctorsScheduleDetailsPeriods Where DoctorScheduleID =  " + drMaster["DoctorScheduleID"].ToString() + " And Deleted = 0 ) ").Rows[0][0].ToString()) > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لوجود جدول زمني ", "Cannot Delete This Transaction Because It Has A Clinic Schedule");
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
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Expected O, but got Unknown
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Clinics.MasterData.frmDoctorsSchedule));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.UNWeeksCount = new UltraNumericEditor();
		this.lblWeeksCount = new UltraLabel();
		this.btnPatientSearch = new UltraButton();
		this.lblDoctor = new UltraLabel();
		this.cboDoctor = new UltraComboEditor();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.btnGenerate = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UNWeeksCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDoctor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.UGBDetails, "UGBDetails");
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
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.btnCopyTo, "btnCopyTo");
		resources.ApplyResources(base.btnSetting, "btnSetting");
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
		resources.ApplyResources(this.UNWeeksCount, "UNWeeksCount");
		((UltraNumericEditorBase)this.UNWeeksCount).FormatString = "";
		this.UNWeeksCount.MaxValue = 4;
		this.UNWeeksCount.MinValue = 1;
		((System.Windows.Forms.Control)(object)this.UNWeeksCount).Name = "UNWeeksCount";
		((UltraNumericEditorBase)this.UNWeeksCount).PromptChar = ' ';
		((UltraNumericEditorBase)this.UNWeeksCount).SpinButtonDisplayStyle = (ButtonDisplayStyle)1;
		this.UNWeeksCount.SpinIncrement = 1;
		this.UNWeeksCount.Value = 1;
		((UltraNumericEditorBase)this.UNWeeksCount).ValueChanged += new System.EventHandler(UNWeeksCount_ValueChanged);
		resources.ApplyResources(this.lblWeeksCount, "lblWeeksCount");
		this.lblWeeksCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblWeeksCount).Name = "lblWeeksCount";
		((ControlBase)this.lblWeeksCount).WrapText = false;
		resources.ApplyResources(this.btnPatientSearch, "btnPatientSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance10");
		((ControlBase)this.btnPatientSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnPatientSearch).Name = "btnPatientSearch";
		((System.Windows.Forms.Control)(object)this.btnPatientSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnPatientSearch).Click += new System.EventHandler(btnPatientSearch_Click);
		resources.ApplyResources(this.lblDoctor, "lblDoctor");
		this.lblDoctor.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDoctor).Name = "lblDoctor";
		((ControlBase)this.lblDoctor).WrapText = false;
		resources.ApplyResources(this.cboDoctor, "cboDoctor");
		((TextEditorControlBase)this.cboDoctor).AlwaysInEditMode = true;
		this.cboDoctor.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboDoctor).Name = "cboDoctor";
		((TextEditorControlBase)this.cboDoctor).ValueChanged += new System.EventHandler(cboDoctor_ValueChanged);
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.btnGenerate, "btnGenerate");
		((System.Windows.Forms.Control)(object)this.btnGenerate).Name = "btnGenerate";
		((System.Windows.Forms.Control)(object)this.btnGenerate).Click += new System.EventHandler(btnGenerate_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnGenerate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDoctor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboDoctor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPatientSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UNWeeksCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblWeeksCount);
		base.Name = "frmDoctorsSchedule";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblWeeksCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UNWeeksCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPatientSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboDoctor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDoctor, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UGBDetails, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnGenerate, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UNWeeksCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDoctor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
