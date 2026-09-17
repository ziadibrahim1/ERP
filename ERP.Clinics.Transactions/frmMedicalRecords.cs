using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Clinics;
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

namespace ERP.Clinics.Transactions;

public class frmMedicalRecords : frmHeaderManyDetails
{
	private DataTable dtPatient;

	private DataTable dtItems;

	private DataTable dtBioAnalysisList;

	private DataTable dtRadiologyList;

	private DataTable dtDoctors;

	private DataTable dtClinics;

	private DataTable dtDrugsH;

	private DataTable dtVisitsH;

	private DataTable dtBioAnalysis;

	private DataTable dtBioAnalysisH;

	private DataTable dtRadiology;

	private DataTable dtRadiologyH;

	private ValueList vlItems = new ValueList();

	private ValueList vlItems2 = new ValueList();

	private ValueList vlBioAnalysisList = new ValueList();

	private ValueList vlBioAnalysisList2 = new ValueList();

	private ValueList vlRadiologyList = new ValueList();

	private ValueList vlRadiologyList2 = new ValueList();

	private ValueList vlDoctors = new ValueList();

	private ValueList vlDoctors1 = new ValueList();

	private ValueList vlDoctors2 = new ValueList();

	private ValueList vlDoctors3 = new ValueList();

	private ValueList vlClinics = new ValueList();

	private ValueList vlClinics1 = new ValueList();

	private ValueList vlClinics2 = new ValueList();

	private ValueList vlClinics3 = new ValueList();

	private DataRow drReservation;

	private string DoctorID = "";

	private string ClinicID = "";

	private IContainer components = null;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraTabPageControl ultraTabPageControl2;

	private UltraComboEditor cboPatient;

	private UltraLabel lblPatient;

	protected internal UltraGrid ULGBioAnalysis;

	private UltraTextEditor txtAdviceNotes;

	private UltraLabel ultraLabel1;

	private UltraTextEditor txtSymptomsNotes;

	private UltraLabel ultraLabel2;

	private UltraTabPageControl ultraTabPageControl3;

	protected internal UltraGrid ULGRadiology;

	private UltraTabPageControl ultraTabPageControl4;

	protected internal UltraGrid ULGMedicalRecordsHistory;

	protected internal UltraGrid ULGBioAnalysisHistory;

	protected internal UltraGrid ULGRadiologyHistory;

	protected internal UltraGrid ULGItemsHistory;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel3;

	public frmMedicalRecords()
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
		InitializeComponent();
		TableName = "CL_MedicalRecords";
		IDCol = "MedicalRecordID";
		NoCol = "MedicalRecordCode";
		DateCol = "MedicalRecordDate";
	}

	public frmMedicalRecords(DataRow dr_Reservation, string Doctor_ID, string Clinic_ID)
		: this()
	{
		drReservation = dr_Reservation;
		DoctorID = Doctor_ID;
		ClinicID = Clinic_ID;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtPatient = Patients.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPatient, dtPatient, "PatientID", "PatientName");
		dtItems = Items.FillCombo("1", "0", "0", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		for (int i = 0; i < dtItems.Rows.Count; i++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[i]["ItemID"], dtItems.Rows[i]["Name"].ToString());
			vlItems2.ValueListItems.Add(dtItems.Rows[i]["ItemID"], dtItems.Rows[i]["Name"].ToString());
		}
		dtBioAnalysisList = BioAnalysis.FillCombo("-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		for (int j = 0; j < dtBioAnalysisList.Rows.Count; j++)
		{
			vlBioAnalysisList.ValueListItems.Add(dtBioAnalysisList.Rows[j]["BioAnalysisID"], dtBioAnalysisList.Rows[j]["Name"].ToString());
			vlBioAnalysisList2.ValueListItems.Add(dtBioAnalysisList.Rows[j]["BioAnalysisID"], dtBioAnalysisList.Rows[j]["Name"].ToString());
		}
		dtRadiologyList = Radiology.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		for (int k = 0; k < dtRadiologyList.Rows.Count; k++)
		{
			vlRadiologyList.ValueListItems.Add(dtRadiologyList.Rows[k]["RadiologyID"], dtRadiologyList.Rows[k]["RadiologyName"].ToString());
			vlRadiologyList2.ValueListItems.Add(dtRadiologyList.Rows[k]["RadiologyID"], dtRadiologyList.Rows[k]["RadiologyName"].ToString());
		}
		dtDoctors = Doctors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		for (int l = 0; l < dtDoctors.Rows.Count; l++)
		{
			vlDoctors.ValueListItems.Add(dtDoctors.Rows[l]["DoctorID"], dtDoctors.Rows[l]["DoctorName"].ToString());
			vlDoctors1.ValueListItems.Add(dtDoctors.Rows[l]["DoctorID"], dtDoctors.Rows[l]["DoctorName"].ToString());
			vlDoctors2.ValueListItems.Add(dtDoctors.Rows[l]["DoctorID"], dtDoctors.Rows[l]["DoctorName"].ToString());
			vlDoctors3.ValueListItems.Add(dtDoctors.Rows[l]["DoctorID"], dtDoctors.Rows[l]["DoctorName"].ToString());
		}
		dtClinics = BusinessLayer.Clinics.Clinics.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		for (int m = 0; m < dtClinics.Rows.Count; m++)
		{
			vlClinics.ValueListItems.Add(dtClinics.Rows[m]["ClinicID"], dtClinics.Rows[m]["ClinicName"].ToString());
			vlClinics1.ValueListItems.Add(dtClinics.Rows[m]["ClinicID"], dtClinics.Rows[m]["ClinicName"].ToString());
			vlClinics2.ValueListItems.Add(dtClinics.Rows[m]["ClinicID"], dtClinics.Rows[m]["ClinicName"].ToString());
			vlClinics3.ValueListItems.Add(dtClinics.Rows[m]["ClinicID"], dtClinics.Rows[m]["ClinicName"].ToString());
		}
		dtDetails = MedicalRecordsItems.SelectByMedicalRecordID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtDrugsH = MedicalRecordsItems.SelectByPatientID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtBioAnalysis = MedicalRecordsBioAnalysis.SelectByMedicalRecordID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtBioAnalysisH = MedicalRecordsBioAnalysis.SelectByPatientID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtRadiology = MedicalRecordsRadiology.SelectByMedicalRecordID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtRadiologyH = MedicalRecordsRadiology.SelectByPatientID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtVisitsH = MedicalRecords.SelectByPatientID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGItemsHistory).DataSource = dtDrugsH;
		((UltraGridBase)ULGBioAnalysis).DataSource = dtBioAnalysis;
		((UltraGridBase)ULGBioAnalysisHistory).DataSource = dtBioAnalysisH;
		((UltraGridBase)ULGRadiology).DataSource = dtRadiology;
		((UltraGridBase)ULGRadiologyHistory).DataSource = dtRadiologyH;
		((UltraGridBase)ULGMedicalRecordsHistory).DataSource = dtVisitsH;
		InitGrid();
		InitGridItemsHistory();
		InitGridBioAnalysis();
		InitGridBioAnalysisHistory();
		InitGridRadiology();
		InitGridRadiologyHistory();
		InitGridMedicalRecordsHistory();
		((UltraTabControlBase)UTCDetails).Tabs["Details"].Text = (GlobalVariables.IsArabic ? "أدويه" : "Drugs");
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MedicalRecordItemID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم" : "Drug Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.7);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
	}

	public void InitGridItemsHistory()
	{
		GlobalFunctions.PrepareGrid(ULGItemsHistory);
		((UltraGridBase)ULGItemsHistory).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGItemsHistory).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGItemsHistory).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGItemsHistory).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)ULGItemsHistory).DisplayLayout.Bands[0].Columns["MedicalRecordDate"].Width = (int)((double)((Control)(object)ULGItemsHistory).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGItemsHistory).DisplayLayout.Bands[0].Columns["MedicalRecordDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGItemsHistory).DisplayLayout.Bands[0].Columns["MedicalRecordDate"].Hidden = false;
		((UltraGridBase)ULGItemsHistory).DisplayLayout.Bands[0].Columns["MedicalRecordDate"].MaskInput = "dd/mm/yyyy";
		((UltraGridBase)ULGItemsHistory).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGItemsHistory).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGItemsHistory).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم" : "Drug Name");
		((UltraGridBase)ULGItemsHistory).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGItemsHistory).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems2;
		((UltraGridBase)ULGItemsHistory).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGItemsHistory).Width * 0.45);
		((HeaderBase)((UltraGridBase)ULGItemsHistory).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGItemsHistory).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGItemsHistory).DisplayLayout.Bands[0].Columns["DoctorID"].Width = (int)((double)((Control)(object)ULGItemsHistory).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGItemsHistory).DisplayLayout.Bands[0].Columns["DoctorID"].Header).Caption = (GlobalVariables.IsArabic ? "الطبيب" : "Doctor");
		((UltraGridBase)ULGItemsHistory).DisplayLayout.Bands[0].Columns["DoctorID"].Hidden = false;
		((UltraGridBase)ULGItemsHistory).DisplayLayout.Bands[0].Columns["DoctorID"].ValueList = (IValueList)(object)vlDoctors2;
		((UltraGridBase)ULGItemsHistory).DisplayLayout.Bands[0].Columns["ClinicID"].Width = (int)((double)((Control)(object)ULGItemsHistory).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGItemsHistory).DisplayLayout.Bands[0].Columns["ClinicID"].Header).Caption = (GlobalVariables.IsArabic ? "العياده" : "Clinic");
		((UltraGridBase)ULGItemsHistory).DisplayLayout.Bands[0].Columns["ClinicID"].Hidden = false;
		((UltraGridBase)ULGItemsHistory).DisplayLayout.Bands[0].Columns["ClinicID"].ValueList = (IValueList)(object)vlClinics2;
	}

	public void InitGridBioAnalysis()
	{
		GlobalFunctions.PrepareGrid(ULGBioAnalysis);
		((UltraGridBase)ULGBioAnalysis).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGBioAnalysis).DisplayLayout.Bands[0].Columns["MedicalRecordBioAnalysisID"].DefaultCellValue = -1;
		((UltraGridBase)ULGBioAnalysis).DisplayLayout.Bands[0].Columns["BioAnalysisID"].Width = (int)((double)((Control)(object)ULGBioAnalysis).Width * 0.3) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGBioAnalysis).DisplayLayout.Bands[0].Columns["BioAnalysisID"].Header).Caption = (GlobalVariables.IsArabic ? "التحليل" : "Bioanalysis");
		((UltraGridBase)ULGBioAnalysis).DisplayLayout.Bands[0].Columns["BioAnalysisID"].Hidden = false;
		((UltraGridBase)ULGBioAnalysis).DisplayLayout.Bands[0].Columns["BioAnalysisID"].ValueList = (IValueList)(object)vlBioAnalysisList;
		((UltraGridBase)ULGBioAnalysis).DisplayLayout.Bands[0].Columns["Result"].Width = (int)((double)((Control)(object)ULGBioAnalysis).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGBioAnalysis).DisplayLayout.Bands[0].Columns["Result"].Header).Caption = (GlobalVariables.IsArabic ? "النتيجه" : "Result");
		((UltraGridBase)ULGBioAnalysis).DisplayLayout.Bands[0].Columns["Result"].Hidden = false;
		((UltraGridBase)ULGBioAnalysis).DisplayLayout.Bands[0].Columns["ResultDate"].Width = (int)((double)((Control)(object)ULGBioAnalysis).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGBioAnalysis).DisplayLayout.Bands[0].Columns["ResultDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Result Date");
		((UltraGridBase)ULGBioAnalysis).DisplayLayout.Bands[0].Columns["ResultDate"].Hidden = false;
		((UltraGridBase)ULGBioAnalysis).DisplayLayout.Bands[0].Columns["ResultDate"].MaskInput = "dd/mm/yyyy";
		((UltraGridBase)ULGBioAnalysis).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGBioAnalysis).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGBioAnalysis).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الملاحظات" : "Notes");
		((UltraGridBase)ULGBioAnalysis).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
	}

	public void InitGridBioAnalysisHistory()
	{
		GlobalFunctions.PrepareGrid(ULGBioAnalysisHistory);
		((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Bands[0].Columns["BioAnalysisID"].Width = (int)((double)((Control)(object)ULGBioAnalysisHistory).Width * 0.15) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Bands[0].Columns["BioAnalysisID"].Header).Caption = (GlobalVariables.IsArabic ? "التحليل" : "Bioanalysis");
		((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Bands[0].Columns["BioAnalysisID"].Hidden = false;
		((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Bands[0].Columns["BioAnalysisID"].ValueList = (IValueList)(object)vlBioAnalysisList2;
		((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Bands[0].Columns["Result"].Width = (int)((double)((Control)(object)ULGBioAnalysisHistory).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Bands[0].Columns["Result"].Header).Caption = (GlobalVariables.IsArabic ? "النتيجه" : "Result");
		((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Bands[0].Columns["Result"].Hidden = false;
		((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Bands[0].Columns["ResultDate"].Width = (int)((double)((Control)(object)ULGBioAnalysisHistory).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Bands[0].Columns["ResultDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Result Date");
		((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Bands[0].Columns["ResultDate"].Hidden = false;
		((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Bands[0].Columns["ResultDate"].MaskInput = "dd/mm/yyyy";
		((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGBioAnalysisHistory).Width * 0.25);
		((HeaderBase)((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الملاحظات" : "Notes");
		((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Bands[0].Columns["DoctorID"].Width = (int)((double)((Control)(object)ULGBioAnalysisHistory).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Bands[0].Columns["DoctorID"].Header).Caption = (GlobalVariables.IsArabic ? "الطبيب" : "Doctor");
		((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Bands[0].Columns["DoctorID"].Hidden = false;
		((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Bands[0].Columns["DoctorID"].ValueList = (IValueList)(object)vlDoctors;
		((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Bands[0].Columns["ClinicID"].Width = (int)((double)((Control)(object)ULGBioAnalysisHistory).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Bands[0].Columns["ClinicID"].Header).Caption = (GlobalVariables.IsArabic ? "العياده" : "Clinic");
		((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Bands[0].Columns["ClinicID"].Hidden = false;
		((UltraGridBase)ULGBioAnalysisHistory).DisplayLayout.Bands[0].Columns["ClinicID"].ValueList = (IValueList)(object)vlClinics;
	}

	public void InitGridRadiology()
	{
		GlobalFunctions.PrepareGrid(ULGRadiology);
		((UltraGridBase)ULGRadiology).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGRadiology).DisplayLayout.Bands[0].Columns["MedicalRecordRadiologyID"].DefaultCellValue = -1;
		((UltraGridBase)ULGRadiology).DisplayLayout.Bands[0].Columns["RadiologyID"].Width = (int)((double)((Control)(object)ULGRadiology).Width * 0.3) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGRadiology).DisplayLayout.Bands[0].Columns["RadiologyID"].Header).Caption = (GlobalVariables.IsArabic ? "الاشعه" : "Radiology");
		((UltraGridBase)ULGRadiology).DisplayLayout.Bands[0].Columns["RadiologyID"].Hidden = false;
		((UltraGridBase)ULGRadiology).DisplayLayout.Bands[0].Columns["RadiologyID"].ValueList = (IValueList)(object)vlRadiologyList;
		((UltraGridBase)ULGRadiology).DisplayLayout.Bands[0].Columns["Result"].Width = (int)((double)((Control)(object)ULGRadiology).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGRadiology).DisplayLayout.Bands[0].Columns["Result"].Header).Caption = (GlobalVariables.IsArabic ? "النتيجه" : "Result");
		((UltraGridBase)ULGRadiology).DisplayLayout.Bands[0].Columns["Result"].Hidden = false;
		((UltraGridBase)ULGRadiology).DisplayLayout.Bands[0].Columns["ResultDate"].Width = (int)((double)((Control)(object)ULGRadiology).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGRadiology).DisplayLayout.Bands[0].Columns["ResultDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Result Date");
		((UltraGridBase)ULGRadiology).DisplayLayout.Bands[0].Columns["ResultDate"].Hidden = false;
		((UltraGridBase)ULGRadiology).DisplayLayout.Bands[0].Columns["ResultDate"].MaskInput = "dd/mm/yyyy";
		((UltraGridBase)ULGRadiology).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGRadiology).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGRadiology).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الملاحظات" : "Notes");
		((UltraGridBase)ULGRadiology).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
	}

	public void InitGridRadiologyHistory()
	{
		GlobalFunctions.PrepareGrid(ULGRadiologyHistory);
		((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Bands[0].Columns["RadiologyID"].Width = (int)((double)((Control)(object)ULGRadiologyHistory).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Bands[0].Columns["RadiologyID"].Header).Caption = (GlobalVariables.IsArabic ? "الاشعه" : "Radiology");
		((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Bands[0].Columns["RadiologyID"].Hidden = false;
		((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Bands[0].Columns["RadiologyID"].ValueList = (IValueList)(object)vlRadiologyList2;
		((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Bands[0].Columns["Result"].Width = (int)((double)((Control)(object)ULGRadiologyHistory).Width * 0.25);
		((HeaderBase)((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Bands[0].Columns["Result"].Header).Caption = (GlobalVariables.IsArabic ? "النتيجه" : "Result");
		((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Bands[0].Columns["Result"].Hidden = false;
		((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Bands[0].Columns["ResultDate"].Width = (int)((double)((Control)(object)ULGRadiologyHistory).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Bands[0].Columns["ResultDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Result Date");
		((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Bands[0].Columns["ResultDate"].Hidden = false;
		((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Bands[0].Columns["ResultDate"].MaskInput = "dd/mm/yyyy";
		((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGRadiologyHistory).Width * 0.25);
		((HeaderBase)((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الملاحظات" : "Notes");
		((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Bands[0].Columns["DoctorID"].Width = (int)((double)((Control)(object)ULGRadiologyHistory).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Bands[0].Columns["DoctorID"].Header).Caption = (GlobalVariables.IsArabic ? "الطبيب" : "Doctor");
		((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Bands[0].Columns["DoctorID"].Hidden = false;
		((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Bands[0].Columns["DoctorID"].ValueList = (IValueList)(object)vlDoctors1;
		((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Bands[0].Columns["ClinicID"].Width = (int)((double)((Control)(object)ULGRadiologyHistory).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Bands[0].Columns["ClinicID"].Header).Caption = (GlobalVariables.IsArabic ? "العياده" : "Clinic");
		((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Bands[0].Columns["ClinicID"].Hidden = false;
		((UltraGridBase)ULGRadiologyHistory).DisplayLayout.Bands[0].Columns["ClinicID"].ValueList = (IValueList)(object)vlClinics1;
	}

	public void InitGridMedicalRecordsHistory()
	{
		GlobalFunctions.PrepareGrid(ULGMedicalRecordsHistory);
		((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Bands[0].Columns["MedicalRecordDate"].Width = (int)((double)((Control)(object)ULGMedicalRecordsHistory).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Bands[0].Columns["MedicalRecordDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Bands[0].Columns["MedicalRecordDate"].Hidden = false;
		((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Bands[0].Columns["MedicalRecordDate"].MaskInput = "dd/mm/yyyy";
		((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Bands[0].Columns["SymptomsNotes"].Width = (int)((double)((Control)(object)ULGMedicalRecordsHistory).Width * 0.25) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Bands[0].Columns["SymptomsNotes"].Header).Caption = (GlobalVariables.IsArabic ? "الاعراض" : "Symptoms");
		((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Bands[0].Columns["SymptomsNotes"].Hidden = false;
		((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Bands[0].Columns["AdviceNotes"].Width = (int)((double)((Control)(object)ULGMedicalRecordsHistory).Width * 0.25);
		((HeaderBase)((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Bands[0].Columns["AdviceNotes"].Header).Caption = (GlobalVariables.IsArabic ? "التشخيص" : "Diagnose");
		((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Bands[0].Columns["AdviceNotes"].Hidden = false;
		((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGMedicalRecordsHistory).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الملاحظات" : "Notes");
		((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Bands[0].Columns["DoctorID"].Width = (int)((double)((Control)(object)ULGMedicalRecordsHistory).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Bands[0].Columns["DoctorID"].Header).Caption = (GlobalVariables.IsArabic ? "الطبيب" : "Doctor");
		((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Bands[0].Columns["DoctorID"].Hidden = false;
		((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Bands[0].Columns["DoctorID"].ValueList = (IValueList)(object)vlDoctors3;
		((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Bands[0].Columns["ClinicID"].Width = (int)((double)((Control)(object)ULGMedicalRecordsHistory).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Bands[0].Columns["ClinicID"].Header).Caption = (GlobalVariables.IsArabic ? "العياده" : "Clinic");
		((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Bands[0].Columns["ClinicID"].Hidden = false;
		((UltraGridBase)ULGMedicalRecordsHistory).DisplayLayout.Bands[0].Columns["ClinicID"].ValueList = (IValueList)(object)vlClinics3;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = MedicalRecords.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0");
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
			((Control)(object)txtCode).Text = drMaster["MedicalRecordCode"].ToString();
			((Control)(object)txtSymptomsNotes).Text = drMaster["SymptomsNotes"].ToString();
			((Control)(object)txtAdviceNotes).Text = drMaster["AdviceNotes"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((TextEditorControlBase)cboPatient).Value = drMaster["PatientID"].ToString();
			dtDetails = MedicalRecordsItems.SelectByMedicalRecordID(drMaster["MedicalRecordID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtDrugsH = MedicalRecordsItems.SelectByPatientID(drMaster["PatientID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtBioAnalysis = MedicalRecordsBioAnalysis.SelectByMedicalRecordID(drMaster["MedicalRecordID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtBioAnalysisH = MedicalRecordsBioAnalysis.SelectByPatientID(drMaster["PatientID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtRadiology = MedicalRecordsRadiology.SelectByMedicalRecordID(drMaster["MedicalRecordID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtRadiologyH = MedicalRecordsRadiology.SelectByPatientID(drMaster["PatientID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtVisitsH = MedicalRecords.SelectByPatientID(drMaster["PatientID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			((UltraGridBase)ULGItemsHistory).DataSource = dtDrugsH;
			((UltraGridBase)ULGBioAnalysis).DataSource = dtBioAnalysis;
			((UltraGridBase)ULGBioAnalysisHistory).DataSource = dtBioAnalysisH;
			((UltraGridBase)ULGRadiology).DataSource = dtRadiology;
			((UltraGridBase)ULGRadiologyHistory).DataSource = dtRadiologyH;
			((UltraGridBase)ULGMedicalRecordsHistory).DataSource = dtVisitsH;
			InitGrid();
			InitGridItemsHistory();
			InitGridBioAnalysis();
			InitGridBioAnalysisHistory();
			InitGridRadiology();
			InitGridRadiologyHistory();
			InitGridMedicalRecordsHistory();
		}
		else if (drReservation != null)
		{
			dtDetails.Rows.Clear();
			dtDrugsH = MedicalRecordsItems.SelectByPatientID(drReservation["PatientID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtBioAnalysis.Rows.Clear();
			dtBioAnalysisH = MedicalRecordsBioAnalysis.SelectByPatientID(drReservation["PatientID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtRadiology.Rows.Clear();
			dtRadiologyH = MedicalRecordsRadiology.SelectByPatientID(drReservation["PatientID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtVisitsH = MedicalRecords.SelectByPatientID(drReservation["PatientID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			((UltraGridBase)ULGItemsHistory).DataSource = dtDrugsH;
			((UltraGridBase)ULGBioAnalysis).DataSource = dtBioAnalysis;
			((UltraGridBase)ULGBioAnalysisHistory).DataSource = dtBioAnalysisH;
			((UltraGridBase)ULGRadiology).DataSource = dtRadiology;
			((UltraGridBase)ULGRadiologyHistory).DataSource = dtRadiologyH;
			((UltraGridBase)ULGMedicalRecordsHistory).DataSource = dtVisitsH;
			InitGrid();
			InitGridItemsHistory();
			InitGridBioAnalysis();
			InitGridBioAnalysisHistory();
			InitGridRadiology();
			InitGridRadiologyHistory();
			InitGridMedicalRecordsHistory();
			btnAddClick();
			((TextEditorControlBase)cboPatient).Value = drReservation["PatientID"];
			((Control)(object)btnAdd).Visible = false;
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
		((EditorButtonControlBase)txtSymptomsNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAdviceNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPatient).ReadOnly = true;
		((Control)(object)btnOK).Visible = Updating;
		((UltraGridBase)ULGBioAnalysis).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGBioAnalysis).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((UltraGridBase)ULGRadiology).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGRadiology).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
	}

	public override void ClearControls()
	{
		base.ClearControls();
		if (((UltraGridBase)ULGBioAnalysis).DataSource is DataTable && ((DisposableObjectCollectionBase)((UltraGridBase)ULGBioAnalysis).Rows).Count > 0)
		{
			((DataTable)((UltraGridBase)ULGBioAnalysis).DataSource).Rows.Clear();
		}
		((TextEditorControlBase)txtCode).Clear();
		((Control)(object)txtCode).Text = (Adding ? MedicalRecords.GetCode() : "");
		((TextEditorControlBase)txtSymptomsNotes).Clear();
		((TextEditorControlBase)txtAdviceNotes).Clear();
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)cboPatient).Clear();
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال الرقم " : "Please Enter The Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (cboPatient.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء اختيار المريض " : "Please select Patient");
			((TextEditorControlBase)cboPatient).Focus();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("CL_MedicalRecords", "MedicalRecordCode", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["MedicalRecordCode"].ToString(), GlobalVariables.CurrentBranchID, DateCol, DateTime.Now.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string code = MedicalRecords.GetCode();
			GlobalVariables.QuestionMB.Show("الرقم متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Number Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = code;
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = MedicalRecords.Insert_Update("-1", ((Control)(object)txtCode).Text, drReservation["ReservationID"].ToString(), (cboPatient.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPatient).Value.ToString(), DoctorID, ClinicID, (((Control)(object)txtSymptomsNotes).Text == "") ? "Null" : ((Control)(object)txtSymptomsNotes).Text, (((Control)(object)txtAdviceNotes).Text == "") ? "Null" : ((Control)(object)txtAdviceNotes).Text, (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Reservations.SetClosed(drReservation["ReservationID"].ToString(), GlobalVariables.UserID);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["MedicalRecordItemID"].Value = "-1";
				((UltraGridBase)ULGData).Rows[i].Cells["MedicalRecordID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				MedicalRecordsItems.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGBioAnalysis).Rows).Count; j++)
			{
				((UltraGridBase)ULGBioAnalysis).Rows[j].Cells["MedicalRecordBioAnalysisID"].Value = "-1";
				((UltraGridBase)ULGBioAnalysis).Rows[j].Cells["MedicalRecordID"].Value = num;
				((UltraGridBase)ULGBioAnalysis).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGBioAnalysis).Rows).Count > 0)
			{
				MedicalRecordsBioAnalysis.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGBioAnalysis).DataSource, GlobalVariables.UserID);
			}
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGRadiology).Rows).Count; k++)
			{
				((UltraGridBase)ULGRadiology).Rows[k].Cells["MedicalRecordRadiologyID"].Value = "-1";
				((UltraGridBase)ULGRadiology).Rows[k].Cells["MedicalRecordID"].Value = num;
				((UltraGridBase)ULGRadiology).Rows[k].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGRadiology).Rows).Count > 0)
			{
				MedicalRecordsRadiology.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGRadiology).DataSource, GlobalVariables.UserID);
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGBioAnalysisHistory).Rows).Count > 0)
			{
				MedicalRecordsBioAnalysis.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGBioAnalysisHistory).DataSource, GlobalVariables.UserID);
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGRadiologyHistory).Rows).Count > 0)
			{
				MedicalRecordsRadiology.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGRadiologyHistory).DataSource, GlobalVariables.UserID);
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
			int num = MedicalRecords.Insert_Update(drMaster["MedicalRecordID"].ToString(), ((Control)(object)txtCode).Text, drMaster["ReservationID"].ToString(), (cboPatient.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPatient).Value.ToString(), drMaster["DoctorID"].ToString(), drMaster["ClinicID"].ToString(), (((Control)(object)txtSymptomsNotes).Text == "") ? "Null" : ((Control)(object)txtSymptomsNotes).Text, (((Control)(object)txtAdviceNotes).Text == "") ? "Null" : ((Control)(object)txtAdviceNotes).Text, (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["MedicalRecordItemID"].Value.ToString() + ",";
				((UltraGridBase)ULGData).Rows[i].Cells["MedicalRecordID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			Main.SyncDeleteForUpdate("CL_MedicalRecordsItems", "MedicalRecordID", drMaster["MedicalRecordID"].ToString(), "MedicalRecordItemID", text, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				MedicalRecordsItems.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			}
			text = ",";
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGBioAnalysis).Rows).Count; j++)
			{
				text = text + ((UltraGridBase)ULGBioAnalysis).Rows[j].Cells["MedicalRecordBioAnalysisID"].Value.ToString() + ",";
				((UltraGridBase)ULGBioAnalysis).Rows[j].Cells["MedicalRecordID"].Value = num;
				((UltraGridBase)ULGBioAnalysis).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			Main.SyncDeleteForUpdate("CL_MedicalRecordsBioAnalysis", "MedicalRecordID", drMaster["MedicalRecordID"].ToString(), "MedicalRecordBioAnalysisID", text, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGBioAnalysis).Rows).Count > 0)
			{
				MedicalRecordsBioAnalysis.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGBioAnalysis).DataSource, GlobalVariables.UserID);
			}
			text = ",";
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGRadiology).Rows).Count; k++)
			{
				text = text + ((UltraGridBase)ULGRadiology).Rows[k].Cells["MedicalRecordRadiologyID"].Value.ToString() + ",";
				((UltraGridBase)ULGRadiology).Rows[k].Cells["MedicalRecordID"].Value = num;
				((UltraGridBase)ULGRadiology).Rows[k].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			Main.SyncDeleteForUpdate("CL_MedicalRecordsRadiology", "MedicalRecordID", drMaster["MedicalRecordID"].ToString(), "MedicalRecordRadiologyID", text, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGRadiology).Rows).Count > 0)
			{
				MedicalRecordsRadiology.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGRadiology).DataSource, GlobalVariables.UserID);
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGBioAnalysisHistory).Rows).Count > 0)
			{
				MedicalRecordsBioAnalysis.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGBioAnalysisHistory).DataSource, GlobalVariables.UserID);
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGRadiologyHistory).Rows).Count > 0)
			{
				MedicalRecordsRadiology.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGRadiologyHistory).DataSource, GlobalVariables.UserID);
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
			MedicalRecordsBioAnalysis.DeleteByMedicalRecordID(drMaster["MedicalRecordID"].ToString(), GlobalVariables.UserID);
			MedicalRecordsItems.DeleteByMedicalRecordID(drMaster["MedicalRecordID"].ToString(), GlobalVariables.UserID);
			MedicalRecordsRadiology.DeleteByMedicalRecordID(drMaster["MedicalRecordID"].ToString(), GlobalVariables.UserID);
			MedicalRecords.Delete(drMaster["MedicalRecordID"].ToString(), GlobalVariables.UserID);
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
		dtSearchResult = SearchFunctions.MedicalRecordsSearchReport(0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["MedicalRecordID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtPatient = Patients.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPatient, dtPatient, "PatientID", "PatientName");
		dtItems = Items.FillCombo("1", "0", "0", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		for (int i = 0; i < dtItems.Rows.Count; i++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[i]["ItemID"], dtItems.Rows[i]["Name"].ToString());
			vlItems2.ValueListItems.Add(dtItems.Rows[i]["ItemID"], dtItems.Rows[i]["Name"].ToString());
		}
		dtBioAnalysisList = BioAnalysis.FillCombo("-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		for (int j = 0; j < dtBioAnalysisList.Rows.Count; j++)
		{
			vlBioAnalysisList.ValueListItems.Add(dtBioAnalysisList.Rows[j]["BioAnalysisID"], dtBioAnalysisList.Rows[j]["Name"].ToString());
			vlBioAnalysisList2.ValueListItems.Add(dtBioAnalysisList.Rows[j]["BioAnalysisID"], dtBioAnalysisList.Rows[j]["Name"].ToString());
		}
		dtRadiologyList = Radiology.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		for (int k = 0; k < dtRadiologyList.Rows.Count; k++)
		{
			vlRadiologyList.ValueListItems.Add(dtRadiologyList.Rows[k]["RadiologyID"], dtRadiologyList.Rows[k]["RadiologyName"].ToString());
			vlRadiologyList2.ValueListItems.Add(dtRadiologyList.Rows[k]["RadiologyID"], dtRadiologyList.Rows[k]["RadiologyName"].ToString());
		}
		dtDoctors = Doctors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		for (int l = 0; l < dtDoctors.Rows.Count; l++)
		{
			vlDoctors.ValueListItems.Add(dtDoctors.Rows[l]["DoctorID"], dtDoctors.Rows[l]["DoctorName"].ToString());
			vlDoctors1.ValueListItems.Add(dtDoctors.Rows[l]["DoctorID"], dtDoctors.Rows[l]["DoctorName"].ToString());
			vlDoctors2.ValueListItems.Add(dtDoctors.Rows[l]["DoctorID"], dtDoctors.Rows[l]["DoctorName"].ToString());
			vlDoctors3.ValueListItems.Add(dtDoctors.Rows[l]["DoctorID"], dtDoctors.Rows[l]["DoctorName"].ToString());
		}
		dtClinics = BusinessLayer.Clinics.Clinics.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		for (int m = 0; m < dtClinics.Rows.Count; m++)
		{
			vlClinics.ValueListItems.Add(dtClinics.Rows[m]["ClinicID"], dtClinics.Rows[m]["ClinicName"].ToString());
			vlClinics1.ValueListItems.Add(dtClinics.Rows[m]["ClinicID"], dtClinics.Rows[m]["ClinicName"].ToString());
			vlClinics2.ValueListItems.Add(dtClinics.Rows[m]["ClinicID"], dtClinics.Rows[m]["ClinicName"].ToString());
			vlClinics3.ValueListItems.Add(dtClinics.Rows[m]["ClinicID"], dtClinics.Rows[m]["ClinicName"].ToString());
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void ULGBioAnalysis_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGBioAnalysis).ActiveRow).Selected = true;
		}
	}

	private void ULGRadiology_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGRadiology).ActiveRow).Selected = true;
		}
	}

	private void ULGItemsHistory_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGItemsHistory).ActiveRow).Selected = true;
	}

	private void ULGBioAnalysisHistory_AfterEnterEditMode(object sender, EventArgs e)
	{
		if ((!Adding && !Updating) || (((KeyedSubObjectBase)ULGBioAnalysisHistory.ActiveCell.Column).Key != "Result" && ((KeyedSubObjectBase)ULGBioAnalysisHistory.ActiveCell.Column).Key != "ResultDate" && ((KeyedSubObjectBase)ULGBioAnalysisHistory.ActiveCell.Column).Key != "Notes"))
		{
			((GridItemBase)((UltraGridBase)ULGBioAnalysisHistory).ActiveRow).Selected = true;
		}
	}

	private void ULGRadiologyHistory_AfterEnterEditMode(object sender, EventArgs e)
	{
		if ((!Adding && !Updating) || (((KeyedSubObjectBase)ULGRadiologyHistory.ActiveCell.Column).Key != "Result" && ((KeyedSubObjectBase)ULGRadiologyHistory.ActiveCell.Column).Key != "ResultDate" && ((KeyedSubObjectBase)ULGRadiologyHistory.ActiveCell.Column).Key != "Notes"))
		{
			((GridItemBase)((UltraGridBase)ULGRadiologyHistory).ActiveRow).Selected = true;
		}
	}

	private void ULGMedicalRecordsHistory_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGMedicalRecordsHistory).ActiveRow).Selected = true;
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
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Expected O, but got Unknown
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Expected O, but got Unknown
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Expected O, but got Unknown
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Expected O, but got Unknown
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Expected O, but got Unknown
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Expected O, but got Unknown
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Expected O, but got Unknown
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Expected O, but got Unknown
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Expected O, but got Unknown
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Expected O, but got Unknown
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Expected O, but got Unknown
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Expected O, but got Unknown
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Expected O, but got Unknown
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Clinics.Transactions.frmMedicalRecords));
		UltraTab val = new UltraTab();
		UltraTab val2 = new UltraTab();
		UltraTab val3 = new UltraTab();
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
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ultraLabel4 = new UltraLabel();
		this.ULGBioAnalysisHistory = new UltraGrid();
		this.ULGBioAnalysis = new UltraGrid();
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.ultraLabel5 = new UltraLabel();
		this.ULGRadiologyHistory = new UltraGrid();
		this.ULGRadiology = new UltraGrid();
		this.ultraTabPageControl4 = new UltraTabPageControl();
		this.ULGMedicalRecordsHistory = new UltraGrid();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.cboPatient = new UltraComboEditor();
		this.lblPatient = new UltraLabel();
		this.txtAdviceNotes = new UltraTextEditor();
		this.ultraLabel1 = new UltraLabel();
		this.txtSymptomsNotes = new UltraTextEditor();
		this.ultraLabel2 = new UltraLabel();
		this.ULGItemsHistory = new UltraGrid();
		this.ultraLabel3 = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGBioAnalysisHistory).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGBioAnalysis).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGRadiologyHistory).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGRadiology).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGMedicalRecordsHistory).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPatient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAdviceNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSymptomsNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGItemsHistory).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl4);
		((UltraTabControlBase)base.UTCDetails).TabOrientation = (TabOrientation)1;
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		((KeyedSubObjectBase)val).Key = "BioAnalysis";
		val.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val, "ultraTab1");
		((SubObjectBase)val).ForceApplyResources = "";
		((KeyedSubObjectBase)val2).Key = "Radiology";
		val2.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val2, "ultraTab2");
		((SubObjectBase)val2).ForceApplyResources = "";
		((KeyedSubObjectBase)val3).Key = "VisitsHistory";
		val3.TabPage = this.ultraTabPageControl4;
		resources.ApplyResources(val3, "ultraTab3");
		((SubObjectBase)val3).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[3] { val, val2, val3 });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl4, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl3, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
		resources.ApplyResources(base.ULGData, "ULGData");
		((UltraGridBase)base.ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val4, "appearance31");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val5, "appearance32");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val6, "appearance33");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val6;
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val7, "appearance34");
		((AppearanceBase)val7).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val8).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val8).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val8).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val8, "appearance35");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val9).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val9, "appearance36");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val10, "appearance37");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.ULGItemsHistory);
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGItemsHistory, 0);
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		resources.ApplyResources(base.btnSetting, "btnSetting");
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val11).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val11).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val11).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val11).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val11).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance38");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val11;
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
		((AppearanceBase)val12).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val12).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val12).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val12).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val12).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		resources.ApplyResources(val12, "appearance39");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val12;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGBioAnalysisHistory);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGBioAnalysis);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		this.ultraLabel4.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		resources.ApplyResources(this.ULGBioAnalysisHistory, "ULGBioAnalysisHistory");
		((UltraGridBase)this.ULGBioAnalysisHistory).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGBioAnalysisHistory).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGBioAnalysisHistory).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val13, "appearance1");
		((UltraGridBase)this.ULGBioAnalysisHistory).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGBioAnalysisHistory).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val14).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val14).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val14, "appearance2");
		((AppearanceBase)val14).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGBioAnalysisHistory).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGBioAnalysisHistory).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val15).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val15, "appearance3");
		((UltraGridBase)this.ULGBioAnalysisHistory).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val15;
		((AppearanceBase)val16).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val16).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val16).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val16, "appearance4");
		((UltraGridBase)this.ULGBioAnalysisHistory).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val16;
		((UltraGridBase)this.ULGBioAnalysisHistory).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGBioAnalysisHistory).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val17).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val17).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val17).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val17, "appearance5");
		((UltraGridBase)this.ULGBioAnalysisHistory).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val17;
		((UltraGridBase)this.ULGBioAnalysisHistory).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGBioAnalysisHistory).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGBioAnalysisHistory).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGBioAnalysisHistory).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGBioAnalysisHistory).Name = "ULGBioAnalysisHistory";
		((UltraControlBase)this.ULGBioAnalysisHistory).UseFlatMode = (DefaultableBoolean)1;
		this.ULGBioAnalysisHistory.AfterEnterEditMode += new System.EventHandler(ULGBioAnalysisHistory_AfterEnterEditMode);
		resources.ApplyResources(this.ULGBioAnalysis, "ULGBioAnalysis");
		((UltraGridBase)this.ULGBioAnalysis).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGBioAnalysis).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGBioAnalysis).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val18, "appearance6");
		((UltraGridBase)this.ULGBioAnalysis).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val18;
		((UltraGridBase)this.ULGBioAnalysis).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val19).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val19).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val19).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val19, "appearance7");
		((AppearanceBase)val19).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGBioAnalysis).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val19;
		((UltraGridBase)this.ULGBioAnalysis).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val20).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val20, "appearance8");
		((UltraGridBase)this.ULGBioAnalysis).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val20;
		((AppearanceBase)val21).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val21).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val21).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val21, "appearance9");
		((UltraGridBase)this.ULGBioAnalysis).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val21;
		((UltraGridBase)this.ULGBioAnalysis).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGBioAnalysis).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val22).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val22).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val22).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val22).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val22, "appearance10");
		((UltraGridBase)this.ULGBioAnalysis).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val22;
		((UltraGridBase)this.ULGBioAnalysis).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGBioAnalysis).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGBioAnalysis).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGBioAnalysis).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGBioAnalysis).Name = "ULGBioAnalysis";
		((UltraControlBase)this.ULGBioAnalysis).UseFlatMode = (DefaultableBoolean)1;
		this.ULGBioAnalysis.AfterEnterEditMode += new System.EventHandler(ULGBioAnalysis_AfterEnterEditMode);
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ULGRadiologyHistory);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ULGRadiology);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		this.ultraLabel5.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		resources.ApplyResources(this.ULGRadiologyHistory, "ULGRadiologyHistory");
		((UltraGridBase)this.ULGRadiologyHistory).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGRadiologyHistory).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGRadiologyHistory).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val23).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val23, "appearance11");
		((UltraGridBase)this.ULGRadiologyHistory).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val23;
		((UltraGridBase)this.ULGRadiologyHistory).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val24).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val24).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val24).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val24, "appearance12");
		((AppearanceBase)val24).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGRadiologyHistory).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val24;
		((UltraGridBase)this.ULGRadiologyHistory).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val25).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val25, "appearance13");
		((UltraGridBase)this.ULGRadiologyHistory).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val25;
		((AppearanceBase)val26).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val26).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val26).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val26, "appearance14");
		((UltraGridBase)this.ULGRadiologyHistory).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val26;
		((UltraGridBase)this.ULGRadiologyHistory).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGRadiologyHistory).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val27).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val27).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val27).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val27).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val27, "appearance15");
		((UltraGridBase)this.ULGRadiologyHistory).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val27;
		((UltraGridBase)this.ULGRadiologyHistory).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGRadiologyHistory).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGRadiologyHistory).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGRadiologyHistory).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGRadiologyHistory).Name = "ULGRadiologyHistory";
		((UltraControlBase)this.ULGRadiologyHistory).UseFlatMode = (DefaultableBoolean)1;
		this.ULGRadiologyHistory.AfterEnterEditMode += new System.EventHandler(ULGRadiologyHistory_AfterEnterEditMode);
		resources.ApplyResources(this.ULGRadiology, "ULGRadiology");
		((UltraGridBase)this.ULGRadiology).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGRadiology).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGRadiology).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val28).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val28, "appearance16");
		((UltraGridBase)this.ULGRadiology).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val28;
		((UltraGridBase)this.ULGRadiology).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val29).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val29).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val29).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val29, "appearance17");
		((AppearanceBase)val29).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGRadiology).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val29;
		((UltraGridBase)this.ULGRadiology).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val30).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val30, "appearance18");
		((UltraGridBase)this.ULGRadiology).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val30;
		((AppearanceBase)val31).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val31).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val31).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val31, "appearance19");
		((UltraGridBase)this.ULGRadiology).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val31;
		((UltraGridBase)this.ULGRadiology).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGRadiology).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val32).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val32).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val32).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val32).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val32, "appearance20");
		((UltraGridBase)this.ULGRadiology).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val32;
		((UltraGridBase)this.ULGRadiology).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGRadiology).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGRadiology).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGRadiology).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGRadiology).Name = "ULGRadiology";
		((UltraControlBase)this.ULGRadiology).UseFlatMode = (DefaultableBoolean)1;
		this.ULGRadiology.AfterEnterEditMode += new System.EventHandler(ULGRadiology_AfterEnterEditMode);
		resources.ApplyResources(this.ultraTabPageControl4, "ultraTabPageControl4");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ULGMedicalRecordsHistory);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Name = "ultraTabPageControl4";
		resources.ApplyResources(this.ULGMedicalRecordsHistory, "ULGMedicalRecordsHistory");
		((UltraGridBase)this.ULGMedicalRecordsHistory).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGMedicalRecordsHistory).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGMedicalRecordsHistory).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val33).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val33, "appearance21");
		((UltraGridBase)this.ULGMedicalRecordsHistory).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val33;
		((UltraGridBase)this.ULGMedicalRecordsHistory).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val34).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val34).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val34).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val34, "appearance22");
		((AppearanceBase)val34).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGMedicalRecordsHistory).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val34;
		((UltraGridBase)this.ULGMedicalRecordsHistory).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val35).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val35, "appearance23");
		((UltraGridBase)this.ULGMedicalRecordsHistory).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val35;
		((AppearanceBase)val36).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val36).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val36).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val36, "appearance24");
		((UltraGridBase)this.ULGMedicalRecordsHistory).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val36;
		((UltraGridBase)this.ULGMedicalRecordsHistory).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGMedicalRecordsHistory).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val37).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val37).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val37).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val37).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val37, "appearance25");
		((UltraGridBase)this.ULGMedicalRecordsHistory).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val37;
		((UltraGridBase)this.ULGMedicalRecordsHistory).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGMedicalRecordsHistory).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGMedicalRecordsHistory).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGMedicalRecordsHistory).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGMedicalRecordsHistory).Name = "ULGMedicalRecordsHistory";
		((UltraControlBase)this.ULGMedicalRecordsHistory).UseFlatMode = (DefaultableBoolean)1;
		this.ULGMedicalRecordsHistory.AfterEnterEditMode += new System.EventHandler(ULGMedicalRecordsHistory_AfterEnterEditMode);
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.cboPatient, "cboPatient");
		((TextEditorControlBase)this.cboPatient).AlwaysInEditMode = true;
		this.cboPatient.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboPatient).Name = "cboPatient";
		resources.ApplyResources(this.lblPatient, "lblPatient");
		this.lblPatient.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPatient).Name = "lblPatient";
		((ControlBase)this.lblPatient).WrapText = false;
		resources.ApplyResources(this.txtAdviceNotes, "txtAdviceNotes");
		((System.Windows.Forms.Control)(object)this.txtAdviceNotes).Name = "txtAdviceNotes";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.txtSymptomsNotes, "txtSymptomsNotes");
		((System.Windows.Forms.Control)(object)this.txtSymptomsNotes).Name = "txtSymptomsNotes";
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.ULGItemsHistory, "ULGItemsHistory");
		((UltraGridBase)this.ULGItemsHistory).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGItemsHistory).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGItemsHistory).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val38).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val38, "appearance26");
		((UltraGridBase)this.ULGItemsHistory).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val38;
		((UltraGridBase)this.ULGItemsHistory).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val39).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val39).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val39).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val39, "appearance27");
		((AppearanceBase)val39).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGItemsHistory).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val39;
		((UltraGridBase)this.ULGItemsHistory).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val40).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val40, "appearance28");
		((UltraGridBase)this.ULGItemsHistory).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val40;
		((AppearanceBase)val41).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val41).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val41).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val41, "appearance29");
		((UltraGridBase)this.ULGItemsHistory).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val41;
		((UltraGridBase)this.ULGItemsHistory).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGItemsHistory).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val42).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val42).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val42).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val42).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val42, "appearance30");
		((UltraGridBase)this.ULGItemsHistory).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val42;
		((UltraGridBase)this.ULGItemsHistory).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGItemsHistory).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGItemsHistory).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGItemsHistory).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGItemsHistory).Name = "ULGItemsHistory";
		((UltraControlBase)this.ULGItemsHistory).UseFlatMode = (DefaultableBoolean)1;
		this.ULGItemsHistory.AfterEnterEditMode += new System.EventHandler(ULGItemsHistory_AfterEnterEditMode);
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPatient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPatient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSymptomsNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAdviceNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Name = "frmMedicalRecords";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAdviceNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSymptomsNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPatient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPatient, 0);
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
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).PerformLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGBioAnalysisHistory).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGBioAnalysis).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGRadiologyHistory).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGRadiology).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGMedicalRecordsHistory).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPatient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAdviceNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSymptomsNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGItemsHistory).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
