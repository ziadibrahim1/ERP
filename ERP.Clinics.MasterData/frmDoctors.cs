using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Clinics;
using BusinessLayer.General;
using BusinessLayer.HR;
using BusinessLayer.Privilege;
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

namespace ERP.Clinics.MasterData;

public class frmDoctors : frmHeaderManyDetails
{
	private DataTable dtDoctorsTypes;

	private DataTable dtSpecializations;

	private DataTable dtSpecializationsDetails;

	private DataTable dtGender;

	private DataTable dtSocialStatus;

	private DataTable dtNationality;

	private DataTable dtCountry;

	private DataTable dtCity;

	private DataTable dtArea;

	private DataTable dtUsers;

	private DataTable dtPriceType;

	private DataTable dtCertificates;

	private DataTable dtProcedures;

	private DataTable dtProceduresPercentages;

	private ValueList vlProcedures = new ValueList();

	private decimal Doctor = default(decimal);

	public decimal DoctorID = default(decimal);

	public string DoctorName = "";

	private IContainer components = null;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraTextEditor txtEnglishName;

	private UltraLabel lblEnglishName;

	private UltraTextEditor txtArabicName;

	private UltraLabel lblArabicName;

	private UltraTabPageControl ultraTabPageControl2;

	private UltraComboEditor cboDoctorType;

	private UltraLabel lblDoctorType;

	private UltraLabel lblGender;

	private UltraComboEditor cboGender;

	private UltraLabel lblBirthDate;

	private UltraDateTimeEditor dtpBirthDate;

	private UltraLabel lblNationality;

	private UltraComboEditor cboNationality;

	private UltraLabel lblSocialStatus;

	private UltraComboEditor cboSocialStatus;

	private UltraLabel lblPersonalIDNo;

	private UltraTextEditor txtPersonalIDNo;

	private UltraLabel lblCountry;

	private UltraComboEditor cboCountry;

	private UltraLabel lblCity;

	private UltraComboEditor cboCity;

	private UltraLabel lblArea;

	private UltraComboEditor cboArea;

	private UltraLabel lblAddress;

	private UltraTextEditor txtAddress;

	private UltraLabel lblPostalCode;

	private UltraTextEditor txtPostalCode;

	private UltraLabel lblEMail;

	private UltraTextEditor txtEMail;

	private UltraLabel lblMobile;

	private UltraTextEditor txtMobile;

	private UltraLabel lblTel;

	private UltraTextEditor txtTel;

	private UltraLabel lblFees;

	private UltraTextEditor txtFees;

	private UltraLabel lblConsultingFees;

	private UltraTextEditor txtConsultingFees;

	private UltraLabel lblConsultingPeriod;

	private UltraTextEditor txtConsultingPeriod;

	private UltraLabel lblConsultingCount;

	private UltraTextEditor txtConsultingCount;

	private UltraCheckEditor chkIsActive;

	protected internal UltraGrid ULGCertificates;

	private UltraLabel lblUser;

	private UltraComboEditor cboUser;

	public UltraButton btnPriceTypeSearch;

	private UltraComboEditor cboPriceType;

	private UltraLabel lblPriceType;

	private UltraCheckEditor chkIsExtended;

	private UltraLabel lblExtendedFees;

	private UltraTextEditor txtExtendedFees;

	private UltraComboEditor cboSpecialization;

	private UltraLabel lblSpecialization;

	private UltraComboEditor cboSpecializationDetail;

	private UltraLabel lblSpecializationDetail;

	private UltraTabPageControl ultraTabPageControl3;

	protected internal UltraGrid ULGProceduresPercentages;

	private UltraTextEditor txtMonthlyFees;

	private UltraLabel lblMonthlyFees;

	private UltraTextEditor txtPeriodFixedValue;

	private UltraLabel lblPeriodFixedValue;

	private UltraTextEditor txtPeriodMinimumValue;

	private UltraLabel lblPeriodMinimumValue;

	private UltraTextEditor txtProcedureFeesPercentage;

	private UltraLabel lblProcedureFeesPercentage;

	private UltraTextEditor txtVisitFeesPercentage;

	private UltraLabel lblVisitFeesPercentage;

	public frmDoctors()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		TableName = "CL_Doctors";
		IDCol = "DoctorID";
		NoCol = "DoctorCode";
		DateCol = "GetDate()";
	}

	public frmDoctors(int ID)
		: this()
	{
		RowID = ID.ToString();
		Doctor = ID;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtSpecializations = Specializations.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSpecialization, dtSpecializations, "SpecializationID", "SpecializationName");
		dtSpecializationsDetails = SpecializationsDetails.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSpecializationDetail, dtSpecializationsDetails, "SpecializationDetailID", "SpecializationDetailName");
		dtDoctorsTypes = DoctorsTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDoctorType, dtDoctorsTypes, "DoctorTypeID", "DoctorTypeName");
		dtProcedures = Procedures.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlProcedures.ValueListItems.Clear();
		for (int i = 0; i < dtProcedures.Rows.Count; i++)
		{
			vlProcedures.ValueListItems.Add(dtProcedures.Rows[i]["ProcedureID"], dtProcedures.Rows[i]["ProcedureName"].ToString());
		}
		dtGender = Gender.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboGender, dtGender, "GenderID", "GenderName");
		dtSocialStatus = SocialStatus.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSocialStatus, dtSocialStatus, "SocialStatusID", "SocialStatusName");
		dtNationality = Nationalities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboNationality, dtNationality, "NationalityID", "NationalityName");
		dtCountry = Countries.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCountry, dtCountry, "CountryID", "CountryName");
		dtCity = Cities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCity, dtCity, "CityID", "CityName");
		dtArea = Areas.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboArea, dtArea, "AreaID", "AreaName");
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboUser, dtUsers, "UserID", "UserName");
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
		dtDetails = DoctorsContacts.SelectByDoctorID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtCertificates = DoctorsCertificates.SelectByDoctorID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtProceduresPercentages = DoctorsProceduresPercentages.SelectByDoctorID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGCertificates).DataSource = dtCertificates;
		((UltraGridBase)ULGProceduresPercentages).DataSource = dtProceduresPercentages;
		InitGrid();
		InitGridCertificates();
		InitGridProceduresPercentages();
		((UltraTabControlBase)UTCDetails).Tabs["Details"].Text = (GlobalVariables.IsArabic ? "جهات الإتصال" : "Contacts");
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DoctorContactID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContactName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContactAddress"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContactTel"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContactMobile"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContactEMail"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContactName"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم" : "Name");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContactAddress"].Header).Caption = (GlobalVariables.IsArabic ? "العنوان" : "Address");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContactTel"].Header).Caption = (GlobalVariables.IsArabic ? "تليفون" : "Tel");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContactMobile"].Header).Caption = (GlobalVariables.IsArabic ? "محمول" : "Mobile");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContactEMail"].Header).Caption = (GlobalVariables.IsArabic ? "بريد إليكتروني" : "EMail");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContactName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContactAddress"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContactTel"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContactMobile"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContactEMail"].Hidden = false;
	}

	public void InitGridCertificates()
	{
		GlobalFunctions.PrepareGrid(ULGCertificates);
		((UltraGridBase)ULGCertificates).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGCertificates).DisplayLayout.Bands[0].Columns["DoctorCertificateID"].DefaultCellValue = -1;
		((UltraGridBase)ULGCertificates).DisplayLayout.Bands[0].Columns["CertificateNameAr"].Width = (int)((double)((Control)(object)ULGCertificates).Width * 0.4) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGCertificates).DisplayLayout.Bands[0].Columns["CertificateNameEn"].Width = (int)((double)((Control)(object)ULGCertificates).Width * 0.4);
		((UltraGridBase)ULGCertificates).DisplayLayout.Bands[0].Columns["CertificateDate"].Width = (int)((double)((Control)(object)ULGCertificates).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGCertificates).DisplayLayout.Bands[0].Columns["CertificateNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم بالعربيه" : "Name Ar");
		((HeaderBase)((UltraGridBase)ULGCertificates).DisplayLayout.Bands[0].Columns["CertificateNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم بالإنجليزيه" : "Name En");
		((HeaderBase)((UltraGridBase)ULGCertificates).DisplayLayout.Bands[0].Columns["CertificateDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGCertificates).DisplayLayout.Bands[0].Columns["CertificateNameAr"].Hidden = false;
		((UltraGridBase)ULGCertificates).DisplayLayout.Bands[0].Columns["CertificateNameEn"].Hidden = false;
		((UltraGridBase)ULGCertificates).DisplayLayout.Bands[0].Columns["CertificateDate"].Hidden = false;
	}

	public void InitGridProceduresPercentages()
	{
		GlobalFunctions.PrepareGrid(ULGProceduresPercentages);
		((UltraGridBase)ULGProceduresPercentages).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGProceduresPercentages).DisplayLayout.Bands[0].Columns["DoctorProcedurePercentageID"].DefaultCellValue = -1;
		((UltraGridBase)ULGProceduresPercentages).DisplayLayout.Bands[0].Columns["ProcedureID"].Width = (int)((double)((Control)(object)ULGProceduresPercentages).Width * 0.8) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGProceduresPercentages).DisplayLayout.Bands[0].Columns["Percentage"].Width = (int)((double)((Control)(object)ULGProceduresPercentages).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGProceduresPercentages).DisplayLayout.Bands[0].Columns["ProcedureID"].Header).Caption = (GlobalVariables.IsArabic ? "الاجراء" : "Procedure");
		((HeaderBase)((UltraGridBase)ULGProceduresPercentages).DisplayLayout.Bands[0].Columns["Percentage"].Header).Caption = (GlobalVariables.IsArabic ? "النسبة" : "Percentage");
		((UltraGridBase)ULGProceduresPercentages).DisplayLayout.Bands[0].Columns["ProcedureID"].Hidden = false;
		((UltraGridBase)ULGProceduresPercentages).DisplayLayout.Bands[0].Columns["Percentage"].Hidden = false;
		((UltraGridBase)ULGProceduresPercentages).DisplayLayout.Bands[0].Columns["ProcedureID"].ValueList = (IValueList)(object)vlProcedures;
		((UltraGridBase)ULGProceduresPercentages).DisplayLayout.Bands[0].Columns["Percentage"].DefaultCellValue = 0;
	}

	public override void FillData()
	{
		if (Doctor == -1m)
		{
			drMaster = null;
			btnAddClick();
			((Control)(object)btnOK).Visible = false;
		}
		else if (Doctor > 0m || Doctor < -1m)
		{
			DataTable dataTable = Doctors.Select(Doctor.ToString(), GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			if (dataTable.Rows.Count > 0)
			{
				drMaster = dataTable.Rows[0];
			}
			else
			{
				drMaster = null;
			}
			btnUpdateClick();
			((Control)(object)btnOK).Visible = false;
		}
		else if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable2 = Doctors.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			if (dataTable2.Rows.Count > 0)
			{
				drMaster = dataTable2.Rows[0];
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
			((Control)(object)txtCode).Text = drMaster["DoctorCode"].ToString();
			((Control)(object)txtArabicName).Text = drMaster["DoctorNameAr"].ToString();
			((Control)(object)txtEnglishName).Text = drMaster["DoctorNameEn"].ToString();
			((Control)(object)txtAddress).Text = drMaster["Address"].ToString();
			((Control)(object)txtConsultingCount).Text = drMaster["ConsultingCount"].ToString();
			((Control)(object)txtConsultingFees).Text = drMaster["ConsultingFees"].ToString();
			((Control)(object)txtConsultingPeriod).Text = drMaster["ConsultingPeriod"].ToString();
			((Control)(object)txtEMail).Text = drMaster["EMail"].ToString();
			((Control)(object)txtFees).Text = drMaster["Fees"].ToString();
			((Control)(object)txtMobile).Text = drMaster["Mobile"].ToString();
			((Control)(object)txtPersonalIDNo).Text = drMaster["PersonalIDNo"].ToString();
			((Control)(object)txtPostalCode).Text = drMaster["PostalCode"].ToString();
			((Control)(object)txtTel).Text = drMaster["Tel"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((TextEditorControlBase)cboArea).Value = drMaster["AreaID"].ToString();
			((TextEditorControlBase)cboCity).Value = drMaster["CityID"].ToString();
			((TextEditorControlBase)cboCountry).Value = drMaster["CountryID"].ToString();
			((TextEditorControlBase)cboDoctorType).Value = drMaster["DoctorTypeID"].ToString();
			((TextEditorControlBase)cboSpecialization).Value = drMaster["SpecializationID"].ToString();
			((TextEditorControlBase)cboSpecializationDetail).Value = drMaster["SpecializationDetailID"].ToString();
			((TextEditorControlBase)cboGender).Value = drMaster["GenderID"].ToString();
			((TextEditorControlBase)cboPriceType).Value = drMaster["PriceTypeID"];
			((TextEditorControlBase)cboNationality).Value = drMaster["NationalityID"].ToString();
			((TextEditorControlBase)cboSocialStatus).Value = drMaster["SocialStatusID"].ToString();
			((UltraToggleEditorBase)chkIsExtended).Checked = !drMaster["IsExtended"].Equals(DBNull.Value) && Convert.ToBoolean(drMaster["IsExtended"]);
			((Control)(object)txtExtendedFees).Text = drMaster["ExtendedFees"].ToString();
			((TextEditorControlBase)cboPriceType).Value = drMaster["PriceTypeID"];
			((Control)(object)txtVisitFeesPercentage).Text = drMaster["VisitFeesPercentage"].ToString();
			((Control)(object)txtProcedureFeesPercentage).Text = drMaster["ProcedureFeesPercentage"].ToString();
			((Control)(object)txtPeriodMinimumValue).Text = drMaster["PeriodMinimumValue"].ToString();
			((Control)(object)txtPeriodFixedValue).Text = drMaster["PeriodFixedValue"].ToString();
			((Control)(object)txtMonthlyFees).Text = drMaster["MonthlyFees"].ToString();
			((TextEditorControlBase)cboUser).Value = drMaster["DoctorUserID"].ToString();
			((UltraToggleEditorBase)chkIsActive).Checked = !drMaster["IsActive"].Equals(DBNull.Value) && Convert.ToBoolean(drMaster["IsActive"]);
			dtpBirthDate.Value = drMaster["BirthDate"];
			dtDetails = DoctorsContacts.SelectByDoctorID(drMaster["DoctorID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			dtCertificates = DoctorsCertificates.SelectByDoctorID(drMaster["DoctorID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			dtProceduresPercentages = DoctorsProceduresPercentages.SelectByDoctorID(drMaster["DoctorID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			((UltraGridBase)ULGData).DataSource = dtDetails;
			((UltraGridBase)ULGCertificates).DataSource = dtCertificates;
			((UltraGridBase)ULGProceduresPercentages).DataSource = dtProceduresPercentages;
			InitGrid();
			InitGridCertificates();
			InitGridProceduresPercentages();
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
		((EditorButtonControlBase)txtAddress).ReadOnly = NavMode;
		((EditorButtonControlBase)txtConsultingCount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtConsultingFees).ReadOnly = NavMode;
		((EditorButtonControlBase)txtConsultingPeriod).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEMail).ReadOnly = NavMode;
		((EditorButtonControlBase)txtFees).ReadOnly = NavMode;
		((EditorButtonControlBase)txtMobile).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPersonalIDNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPostalCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTel).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)cboArea).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCity).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCountry).ReadOnly = NavMode;
		((EditorButtonControlBase)cboDoctorType).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSpecialization).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSpecializationDetail).ReadOnly = NavMode;
		((EditorButtonControlBase)cboGender).ReadOnly = NavMode;
		((EditorButtonControlBase)cboNationality).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSocialStatus).ReadOnly = NavMode;
		((EditorButtonControlBase)cboUser).ReadOnly = NavMode;
		((Control)(object)chkIsActive).Enabled = !NavMode;
		((EditorButtonControlBase)cboPriceType).ReadOnly = NavMode || !CanModifyPriceType;
		((Control)(object)btnPriceTypeSearch).Visible = !NavMode && CanModifyPriceType;
		((EditorButtonControlBase)txtExtendedFees).ReadOnly = NavMode;
		((Control)(object)chkIsExtended).Enabled = !NavMode;
		((EditorButtonControlBase)txtVisitFeesPercentage).ReadOnly = NavMode;
		((EditorButtonControlBase)txtProcedureFeesPercentage).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPeriodMinimumValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPeriodFixedValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtMonthlyFees).ReadOnly = NavMode;
		dtpBirthDate.DateTime = DateTime.Now;
		((UltraGridBase)ULGCertificates).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGCertificates).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((UltraGridBase)ULGProceduresPercentages).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGProceduresPercentages).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
	}

	public override void ClearControls()
	{
		base.ClearControls();
		if (((UltraGridBase)ULGCertificates).DataSource is DataTable && ((DisposableObjectCollectionBase)((UltraGridBase)ULGCertificates).Rows).Count > 0)
		{
			((DataTable)((UltraGridBase)ULGCertificates).DataSource).Rows.Clear();
		}
		if (((UltraGridBase)ULGProceduresPercentages).DataSource is DataTable && ((DisposableObjectCollectionBase)((UltraGridBase)ULGProceduresPercentages).Rows).Count > 0)
		{
			((DataTable)((UltraGridBase)ULGProceduresPercentages).DataSource).Rows.Clear();
		}
		((TextEditorControlBase)txtCode).Clear();
		((Control)(object)txtCode).Text = (Adding ? Doctors.GetCode(IsFromServer: false) : "");
		((TextEditorControlBase)txtArabicName).Clear();
		((TextEditorControlBase)txtEnglishName).Clear();
		((TextEditorControlBase)txtAddress).Clear();
		((TextEditorControlBase)txtConsultingCount).Clear();
		((TextEditorControlBase)txtConsultingFees).Clear();
		((TextEditorControlBase)txtConsultingPeriod).Clear();
		((TextEditorControlBase)txtEMail).Clear();
		((TextEditorControlBase)txtFees).Clear();
		((TextEditorControlBase)txtMobile).Clear();
		((TextEditorControlBase)txtPersonalIDNo).Clear();
		((TextEditorControlBase)txtPostalCode).Clear();
		((TextEditorControlBase)txtTel).Clear();
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)cboArea).Clear();
		((TextEditorControlBase)cboCity).Clear();
		((TextEditorControlBase)cboCountry).Clear();
		cboDoctorType.SelectedIndex = -1;
		((TextEditorControlBase)cboSpecialization).ValueChanged -= cboSpecialization_ValueChanged;
		cboSpecialization.SelectedIndex = -1;
		((TextEditorControlBase)cboSpecialization).ValueChanged += cboSpecialization_ValueChanged;
		cboSpecializationDetail.SelectedIndex = -1;
		((TextEditorControlBase)cboGender).Clear();
		((TextEditorControlBase)cboNationality).Clear();
		((TextEditorControlBase)cboSocialStatus).Clear();
		((TextEditorControlBase)cboUser).Clear();
		cboPriceType.SelectedIndex = ((dtPriceType.Rows.Count <= 0) ? (-1) : 0);
		((UltraToggleEditorBase)chkIsActive).Checked = true;
		((UltraToggleEditorBase)chkIsExtended).Checked = false;
		((Control)(object)txtExtendedFees).Text = "0";
		((Control)(object)txtVisitFeesPercentage).Text = "0";
		((Control)(object)txtProcedureFeesPercentage).Text = "0";
		((Control)(object)txtPeriodMinimumValue).Text = "0";
		((Control)(object)txtPeriodFixedValue).Text = "0";
		((Control)(object)txtMonthlyFees).Text = "0";
		dtpBirthDate.DateTime = DateTime.Now;
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال الرقم " : "Please Enter The Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال الاسم بالعربية", "Please Enter Arabic Name");
			((TextEditorControlBase)txtArabicName).Focus();
			return false;
		}
		if (cboUser.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال المستخدم", "Please Enter User");
			((TextEditorControlBase)cboUser).Focus();
			cboUser.DropDown();
			return false;
		}
		if (Main.CheckForValue("CL_Doctors", "DoctorCode", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["DoctorCode"].ToString(), IsFromServer: false) > 0)
		{
			string code = Doctors.GetCode(IsFromServer: false);
			GlobalVariables.QuestionMB.Show("الرقم متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Number Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = code;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGProceduresPercentages).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGProceduresPercentages).Rows[i].Cells["ProcedureID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الاجراء  ", "Please Enter Procedure Name");
				ULGProceduresPercentages.ActiveCell = ((UltraGridBase)ULGProceduresPercentages).Rows[i].Cells["ProcedureID"];
				return false;
			}
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			DoctorID = Doctors.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, (cboDoctorType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDoctorType).Value.ToString(), (cboSpecialization.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSpecialization).Value.ToString(), (cboSpecializationDetail.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSpecializationDetail).Value.ToString(), (cboGender.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboGender).Value.ToString(), dtpBirthDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboSocialStatus.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSocialStatus).Value.ToString(), (cboNationality.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboNationality).Value.ToString(), (((Control)(object)txtPersonalIDNo).Text == "") ? "Null" : ((Control)(object)txtPersonalIDNo).Text, (cboCountry.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCountry).Value.ToString(), (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), (((Control)(object)txtAddress).Text == "") ? "Null" : ((Control)(object)txtAddress).Text, (((Control)(object)txtPostalCode).Text == "") ? "Null" : ((Control)(object)txtPostalCode).Text, (((Control)(object)txtEMail).Text == "") ? "Null" : ((Control)(object)txtEMail).Text, (((Control)(object)txtMobile).Text == "") ? "Null" : ((Control)(object)txtMobile).Text, (((Control)(object)txtTel).Text == "") ? "Null" : ((Control)(object)txtTel).Text, (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, (((Control)(object)txtFees).Text == "") ? "0" : ((Control)(object)txtFees).Text, (((Control)(object)txtConsultingFees).Text == "") ? "0" : ((Control)(object)txtConsultingFees).Text, (((Control)(object)txtConsultingPeriod).Text == "") ? "0" : ((Control)(object)txtConsultingPeriod).Text, (((Control)(object)txtConsultingCount).Text == "") ? "0" : ((Control)(object)txtConsultingCount).Text, ((UltraToggleEditorBase)chkIsExtended).Checked ? "1" : "0", (!((UltraToggleEditorBase)chkIsExtended).Checked) ? "0" : ((((Control)(object)txtExtendedFees).Text == "") ? "0" : ((Control)(object)txtExtendedFees).Text), (cboPriceType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), ((UltraToggleEditorBase)chkIsActive).Checked ? "1" : "0", (((Control)(object)txtVisitFeesPercentage).Text == "") ? "0" : ((Control)(object)txtVisitFeesPercentage).Text, (((Control)(object)txtProcedureFeesPercentage).Text == "") ? "0" : ((Control)(object)txtProcedureFeesPercentage).Text, (((Control)(object)txtPeriodMinimumValue).Text == "") ? "0" : ((Control)(object)txtPeriodMinimumValue).Text, (((Control)(object)txtPeriodFixedValue).Text == "") ? "0" : ((Control)(object)txtPeriodFixedValue).Text, (((Control)(object)txtMonthlyFees).Text == "") ? "0" : ((Control)(object)txtMonthlyFees).Text, (cboUser.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUser).Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["DoctorContactID"].Value = "-1";
				((UltraGridBase)ULGData).Rows[i].Cells["DoctorID"].Value = DoctorID;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				DoctorsContacts.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: false);
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGCertificates).Rows).Count; j++)
			{
				((UltraGridBase)ULGCertificates).Rows[j].Cells["DoctorCertificateID"].Value = "-1";
				((UltraGridBase)ULGCertificates).Rows[j].Cells["DoctorID"].Value = DoctorID;
				((UltraGridBase)ULGCertificates).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGCertificates).Rows).Count > 0)
			{
				DoctorsCertificates.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGCertificates).DataSource, GlobalVariables.UserID, IsFromServer: false);
			}
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGProceduresPercentages).Rows).Count; k++)
			{
				((UltraGridBase)ULGProceduresPercentages).Rows[k].Cells["DoctorProcedurePercentageID"].Value = "-1";
				((UltraGridBase)ULGProceduresPercentages).Rows[k].Cells["DoctorID"].Value = DoctorID;
				((UltraGridBase)ULGProceduresPercentages).Rows[k].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGProceduresPercentages).Rows).Count > 0)
			{
				DoctorsProceduresPercentages.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGProceduresPercentages).DataSource, GlobalVariables.UserID, IsFromServer: false);
			}
			Main.EndBulkTrans(FromServer: false);
			DoctorName = (GlobalVariables.IsArabic ? ((Control)(object)txtArabicName).Text : ((Control)(object)txtEnglishName).Text);
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
			DoctorID = Doctors.Insert_Update(drMaster["DoctorID"].ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, (cboDoctorType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDoctorType).Value.ToString(), (cboSpecialization.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSpecialization).Value.ToString(), (cboSpecializationDetail.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSpecializationDetail).Value.ToString(), (cboGender.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboGender).Value.ToString(), dtpBirthDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboSocialStatus.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSocialStatus).Value.ToString(), (cboNationality.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboNationality).Value.ToString(), (((Control)(object)txtPersonalIDNo).Text == "") ? "Null" : ((Control)(object)txtPersonalIDNo).Text, (cboCountry.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCountry).Value.ToString(), (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), (((Control)(object)txtAddress).Text == "") ? "Null" : ((Control)(object)txtAddress).Text, (((Control)(object)txtPostalCode).Text == "") ? "Null" : ((Control)(object)txtPostalCode).Text, (((Control)(object)txtEMail).Text == "") ? "Null" : ((Control)(object)txtEMail).Text, (((Control)(object)txtMobile).Text == "") ? "Null" : ((Control)(object)txtMobile).Text, (((Control)(object)txtTel).Text == "") ? "Null" : ((Control)(object)txtTel).Text, (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, (((Control)(object)txtFees).Text == "") ? "0" : ((Control)(object)txtFees).Text, (((Control)(object)txtConsultingFees).Text == "") ? "0" : ((Control)(object)txtConsultingFees).Text, (((Control)(object)txtConsultingPeriod).Text == "") ? "0" : ((Control)(object)txtConsultingPeriod).Text, (((Control)(object)txtConsultingCount).Text == "") ? "0" : ((Control)(object)txtConsultingCount).Text, ((UltraToggleEditorBase)chkIsExtended).Checked ? "1" : "0", (!((UltraToggleEditorBase)chkIsExtended).Checked) ? "0" : ((((Control)(object)txtExtendedFees).Text == "") ? "0" : ((Control)(object)txtExtendedFees).Text), (cboPriceType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), ((UltraToggleEditorBase)chkIsActive).Checked ? "1" : "0", (((Control)(object)txtVisitFeesPercentage).Text == "") ? "0" : ((Control)(object)txtVisitFeesPercentage).Text, (((Control)(object)txtProcedureFeesPercentage).Text == "") ? "0" : ((Control)(object)txtProcedureFeesPercentage).Text, (((Control)(object)txtPeriodMinimumValue).Text == "") ? "0" : ((Control)(object)txtPeriodMinimumValue).Text, (((Control)(object)txtPeriodFixedValue).Text == "") ? "0" : ((Control)(object)txtPeriodFixedValue).Text, (((Control)(object)txtMonthlyFees).Text == "") ? "0" : ((Control)(object)txtMonthlyFees).Text, (cboUser.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUser).Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["DoctorContactID"].Value.ToString() + ",";
				((UltraGridBase)ULGData).Rows[i].Cells["DoctorID"].Value = DoctorID;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			Main.SyncDeleteForUpdate("CL_DoctorsContacts", "DoctorID", drMaster["DoctorID"].ToString(), "DoctorContactID", text, IsFromServer: false);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				DoctorsContacts.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: false);
			}
			text = ",";
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGCertificates).Rows).Count; j++)
			{
				text = text + ((UltraGridBase)ULGCertificates).Rows[j].Cells["DoctorCertificateID"].Value.ToString() + ",";
				((UltraGridBase)ULGCertificates).Rows[j].Cells["DoctorID"].Value = DoctorID;
				((UltraGridBase)ULGCertificates).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			Main.SyncDeleteForUpdate("CL_DoctorsCertificates", "DoctorID", drMaster["DoctorID"].ToString(), "DoctorCertificateID", text, IsFromServer: false);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGCertificates).Rows).Count > 0)
			{
				DoctorsCertificates.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGCertificates).DataSource, GlobalVariables.UserID, IsFromServer: false);
			}
			text = ",";
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGProceduresPercentages).Rows).Count; k++)
			{
				text = text + ((UltraGridBase)ULGProceduresPercentages).Rows[k].Cells["DoctorProcedurePercentageID"].Value.ToString() + ",";
				((UltraGridBase)ULGProceduresPercentages).Rows[k].Cells["DoctorID"].Value = DoctorID;
				((UltraGridBase)ULGProceduresPercentages).Rows[k].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			Main.SyncDeleteForUpdate("CL_DoctorsProceduresPercentages", "DoctorID", drMaster["DoctorID"].ToString(), "DoctorProcedurePercentageID", text, IsFromServer: false);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGProceduresPercentages).Rows).Count > 0)
			{
				DoctorsProceduresPercentages.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGProceduresPercentages).DataSource, GlobalVariables.UserID, IsFromServer: false);
			}
			Main.EndBulkTrans(FromServer: false);
			DoctorName = (GlobalVariables.IsArabic ? ((Control)(object)txtArabicName).Text : ((Control)(object)txtEnglishName).Text);
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
			DoctorsContacts.DeleteByDoctorID(drMaster["DoctorID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			DoctorsCertificates.DeleteByDoctorID(drMaster["DoctorID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			DoctorsProceduresPercentages.DeleteByDoctorID(drMaster["DoctorID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			DoctorsSchedules.DeleteByDoctorID(drMaster["DoctorID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			Doctors.Delete(drMaster["DoctorID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			Main.EndBulkTrans(FromServer: false);
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
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.DoctorsSearchReport(IsFromServer: false);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["DoctorID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtSpecializations = Specializations.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSpecialization, dtSpecializations, "SpecializationID", "SpecializationName");
		dtSpecializationsDetails = SpecializationsDetails.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSpecializationDetail, dtSpecializationsDetails, "SpecializationDetailID", "SpecializationDetailName");
		dtDoctorsTypes = DoctorsTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDoctorType, dtDoctorsTypes, "DoctorTypeID", "DoctorTypeName");
		dtGender = Gender.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboGender, dtGender, "GenderID", "GenderName");
		dtSocialStatus = SocialStatus.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSocialStatus, dtSocialStatus, "SocialStatusID", "SocialStatusName");
		dtNationality = Nationalities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboNationality, dtNationality, "NationalityID", "NationalityName");
		dtCountry = Countries.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCountry, dtCountry, "CountryID", "CountryName");
		dtCity = Cities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCity, dtCity, "CityID", "CityName");
		dtArea = Areas.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboArea, dtArea, "AreaID", "AreaName");
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboUser, dtUsers, "UserID", "UserName");
		dtProcedures = Procedures.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlProcedures.ValueListItems.Clear();
		for (int i = 0; i < dtProcedures.Rows.Count; i++)
		{
			vlProcedures.ValueListItems.Add(dtProcedures.Rows[i]["ProcedureID"], dtProcedures.Rows[i]["ProcedureName"].ToString());
		}
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void ULGCertificates_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGCertificates).ActiveRow).Selected = true;
		}
	}

	private void cboCountry_ValueChanged(object sender, EventArgs e)
	{
		if (cboCountry.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtCity);
			dataView.RowFilter = "CountryID=" + ((TextEditorControlBase)cboCountry).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboCity.DataSource = dataView;
			cboCity.DisplayMember = "CityName";
			cboCity.ValueMember = "CityID";
		}
	}

	private void cboCity_ValueChanged(object sender, EventArgs e)
	{
		if (cboCity.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtArea);
			dataView.RowFilter = "CityID=" + ((TextEditorControlBase)cboCity).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboArea.DataSource = dataView;
			cboArea.DisplayMember = "AreaName";
			cboArea.ValueMember = "AreaID";
		}
	}

	private void txtInt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void txtNumper_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void chkIsExtended_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)txtExtendedFees).Enabled = ((UltraToggleEditorBase)chkIsExtended).Checked;
	}

	private void cboSpecialization_ValueChanged(object sender, EventArgs e)
	{
		if (cboSpecialization.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtSpecializationsDetails);
			dataView.RowFilter = "SpecializationID =" + ((TextEditorControlBase)cboSpecialization).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboSpecializationDetail.DataSource = dataView;
			cboSpecializationDetail.DisplayMember = "SpecializationDetailName";
			cboSpecializationDetail.ValueMember = "SpecializationDetailID";
			cboSpecializationDetail.SelectedIndex = -1;
		}
	}

	private void ULGProceduresPercentages_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (((KeyedSubObjectBase)ULGProceduresPercentages.ActiveCell.Column).Key == "Percentage")
		{
			GlobalFunctions.CheckForNumbers(ULGProceduresPercentages.ActiveCell, e);
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
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Expected O, but got Unknown
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Expected O, but got Unknown
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Expected O, but got Unknown
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Expected O, but got Unknown
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Expected O, but got Unknown
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Expected O, but got Unknown
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Expected O, but got Unknown
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Expected O, but got Unknown
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Expected O, but got Unknown
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Expected O, but got Unknown
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected O, but got Unknown
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Expected O, but got Unknown
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Expected O, but got Unknown
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Expected O, but got Unknown
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Expected O, but got Unknown
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Expected O, but got Unknown
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Expected O, but got Unknown
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Expected O, but got Unknown
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Expected O, but got Unknown
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Expected O, but got Unknown
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Expected O, but got Unknown
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Expected O, but got Unknown
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Expected O, but got Unknown
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Expected O, but got Unknown
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Expected O, but got Unknown
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Expected O, but got Unknown
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Expected O, but got Unknown
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Expected O, but got Unknown
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Expected O, but got Unknown
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Expected O, but got Unknown
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Expected O, but got Unknown
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Expected O, but got Unknown
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Expected O, but got Unknown
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Expected O, but got Unknown
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Expected O, but got Unknown
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Expected O, but got Unknown
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Expected O, but got Unknown
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Expected O, but got Unknown
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Expected O, but got Unknown
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Expected O, but got Unknown
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Expected O, but got Unknown
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Expected O, but got Unknown
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Expected O, but got Unknown
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Expected O, but got Unknown
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Expected O, but got Unknown
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Expected O, but got Unknown
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Expected O, but got Unknown
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Expected O, but got Unknown
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Expected O, but got Unknown
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Expected O, but got Unknown
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Expected O, but got Unknown
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Expected O, but got Unknown
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Expected O, but got Unknown
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Expected O, but got Unknown
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Expected O, but got Unknown
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Expected O, but got Unknown
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Expected O, but got Unknown
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Expected O, but got Unknown
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Expected O, but got Unknown
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Expected O, but got Unknown
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Expected O, but got Unknown
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Expected O, but got Unknown
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Expected O, but got Unknown
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Expected O, but got Unknown
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Expected O, but got Unknown
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Clinics.MasterData.frmDoctors));
		UltraTab val = new UltraTab();
		UltraTab val2 = new UltraTab();
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
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGCertificates = new UltraGrid();
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.ULGProceduresPercentages = new UltraGrid();
		this.txtMonthlyFees = new UltraTextEditor();
		this.lblMonthlyFees = new UltraLabel();
		this.txtPeriodFixedValue = new UltraTextEditor();
		this.lblPeriodFixedValue = new UltraLabel();
		this.txtPeriodMinimumValue = new UltraTextEditor();
		this.lblPeriodMinimumValue = new UltraLabel();
		this.txtProcedureFeesPercentage = new UltraTextEditor();
		this.lblProcedureFeesPercentage = new UltraLabel();
		this.txtVisitFeesPercentage = new UltraTextEditor();
		this.lblVisitFeesPercentage = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.txtEnglishName = new UltraTextEditor();
		this.lblEnglishName = new UltraLabel();
		this.txtArabicName = new UltraTextEditor();
		this.lblArabicName = new UltraLabel();
		this.cboDoctorType = new UltraComboEditor();
		this.lblDoctorType = new UltraLabel();
		this.lblGender = new UltraLabel();
		this.cboGender = new UltraComboEditor();
		this.lblBirthDate = new UltraLabel();
		this.dtpBirthDate = new UltraDateTimeEditor();
		this.lblNationality = new UltraLabel();
		this.cboNationality = new UltraComboEditor();
		this.lblSocialStatus = new UltraLabel();
		this.cboSocialStatus = new UltraComboEditor();
		this.lblPersonalIDNo = new UltraLabel();
		this.txtPersonalIDNo = new UltraTextEditor();
		this.lblCountry = new UltraLabel();
		this.cboCountry = new UltraComboEditor();
		this.lblCity = new UltraLabel();
		this.cboCity = new UltraComboEditor();
		this.lblArea = new UltraLabel();
		this.cboArea = new UltraComboEditor();
		this.lblAddress = new UltraLabel();
		this.txtAddress = new UltraTextEditor();
		this.lblPostalCode = new UltraLabel();
		this.txtPostalCode = new UltraTextEditor();
		this.lblEMail = new UltraLabel();
		this.txtEMail = new UltraTextEditor();
		this.lblMobile = new UltraLabel();
		this.txtMobile = new UltraTextEditor();
		this.lblTel = new UltraLabel();
		this.txtTel = new UltraTextEditor();
		this.lblFees = new UltraLabel();
		this.txtFees = new UltraTextEditor();
		this.lblConsultingFees = new UltraLabel();
		this.txtConsultingFees = new UltraTextEditor();
		this.lblConsultingPeriod = new UltraLabel();
		this.txtConsultingPeriod = new UltraTextEditor();
		this.lblConsultingCount = new UltraLabel();
		this.txtConsultingCount = new UltraTextEditor();
		this.chkIsActive = new UltraCheckEditor();
		this.lblUser = new UltraLabel();
		this.cboUser = new UltraComboEditor();
		this.btnPriceTypeSearch = new UltraButton();
		this.cboPriceType = new UltraComboEditor();
		this.lblPriceType = new UltraLabel();
		this.chkIsExtended = new UltraCheckEditor();
		this.lblExtendedFees = new UltraLabel();
		this.txtExtendedFees = new UltraTextEditor();
		this.cboSpecialization = new UltraComboEditor();
		this.lblSpecialization = new UltraLabel();
		this.cboSpecializationDetail = new UltraComboEditor();
		this.lblSpecializationDetail = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGCertificates).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGProceduresPercentages).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMonthlyFees).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPeriodFixedValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPeriodMinimumValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtProcedureFeesPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisitFeesPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDoctorType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboGender).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBirthDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboNationality).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSocialStatus).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPersonalIDNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPostalCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobile).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFees).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtConsultingFees).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtConsultingPeriod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtConsultingCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboUser).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsExtended).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExtendedFees).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSpecialization).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSpecializationDetail).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((UltraTabControlBase)base.UTCDetails).TabOrientation = (TabOrientation)1;
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		((KeyedSubObjectBase)val).Key = "Certificates";
		val.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val, "ultraTab1");
		((SubObjectBase)val).ForceApplyResources = "";
		((KeyedSubObjectBase)val2).Key = "Contracts";
		val2.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val2, "ultraTab2");
		((SubObjectBase)val2).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[2] { val, val2 });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl3, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
		resources.ApplyResources(base.ULGData, "ULGData");
		((UltraGridBase)base.ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val3, "appearance11");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val4, "appearance12");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val5, "appearance13");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val6, "appearance14");
		((AppearanceBase)val6).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val7).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val7).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val7).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val7, "appearance15");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val8).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val8, "appearance16");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val9, "appearance17");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		resources.ApplyResources(base.btnSetting, "btnSetting");
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val10).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val10).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val10).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val10).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val10).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance18");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val10;
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
		((AppearanceBase)val11).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val11).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val11).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val11).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val11).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		resources.ApplyResources(val11, "appearance22");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val11;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGCertificates);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.ULGCertificates, "ULGCertificates");
		((UltraGridBase)this.ULGCertificates).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGCertificates).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGCertificates).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val12, "appearance1");
		((UltraGridBase)this.ULGCertificates).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGCertificates).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val13).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val13).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val13, "appearance2");
		((AppearanceBase)val13).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGCertificates).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGCertificates).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val14).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val14, "appearance3");
		((UltraGridBase)this.ULGCertificates).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val14;
		((AppearanceBase)val15).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val15).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val15).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val15, "appearance4");
		((UltraGridBase)this.ULGCertificates).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGCertificates).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGCertificates).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val16).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val16).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val16).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val16, "appearance5");
		((UltraGridBase)this.ULGCertificates).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val16;
		((UltraGridBase)this.ULGCertificates).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGCertificates).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGCertificates).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGCertificates).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGCertificates).Name = "ULGCertificates";
		((UltraControlBase)this.ULGCertificates).UseFlatMode = (DefaultableBoolean)1;
		this.ULGCertificates.AfterEnterEditMode += new System.EventHandler(ULGCertificates_AfterEnterEditMode);
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ULGProceduresPercentages);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.txtMonthlyFees);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblMonthlyFees);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.txtPeriodFixedValue);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblPeriodFixedValue);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.txtPeriodMinimumValue);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblPeriodMinimumValue);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.txtProcedureFeesPercentage);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblProcedureFeesPercentage);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.txtVisitFeesPercentage);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblVisitFeesPercentage);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		resources.ApplyResources(this.ULGProceduresPercentages, "ULGProceduresPercentages");
		((UltraGridBase)this.ULGProceduresPercentages).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGProceduresPercentages).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGProceduresPercentages).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val17, "appearance6");
		((UltraGridBase)this.ULGProceduresPercentages).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val17;
		((UltraGridBase)this.ULGProceduresPercentages).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val18).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val18).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val18).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val18, "appearance7");
		((AppearanceBase)val18).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGProceduresPercentages).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val18;
		((UltraGridBase)this.ULGProceduresPercentages).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val19).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val19, "appearance8");
		((UltraGridBase)this.ULGProceduresPercentages).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val19;
		((AppearanceBase)val20).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val20).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val20).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val20, "appearance9");
		((UltraGridBase)this.ULGProceduresPercentages).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val20;
		((UltraGridBase)this.ULGProceduresPercentages).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGProceduresPercentages).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val21).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val21).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val21).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val21).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val21, "appearance10");
		((UltraGridBase)this.ULGProceduresPercentages).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val21;
		((UltraGridBase)this.ULGProceduresPercentages).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGProceduresPercentages).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGProceduresPercentages).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGProceduresPercentages).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGProceduresPercentages).Name = "ULGProceduresPercentages";
		((UltraControlBase)this.ULGProceduresPercentages).UseFlatMode = (DefaultableBoolean)1;
		((System.Windows.Forms.Control)(object)this.ULGProceduresPercentages).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGProceduresPercentages_KeyPress);
		resources.ApplyResources(this.txtMonthlyFees, "txtMonthlyFees");
		((System.Windows.Forms.Control)(object)this.txtMonthlyFees).Name = "txtMonthlyFees";
		((System.Windows.Forms.Control)(object)this.txtMonthlyFees).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumper_KeyPress);
		resources.ApplyResources(this.lblMonthlyFees, "lblMonthlyFees");
		this.lblMonthlyFees.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMonthlyFees).Name = "lblMonthlyFees";
		resources.ApplyResources(this.txtPeriodFixedValue, "txtPeriodFixedValue");
		((System.Windows.Forms.Control)(object)this.txtPeriodFixedValue).Name = "txtPeriodFixedValue";
		((System.Windows.Forms.Control)(object)this.txtPeriodFixedValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumper_KeyPress);
		resources.ApplyResources(this.lblPeriodFixedValue, "lblPeriodFixedValue");
		this.lblPeriodFixedValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPeriodFixedValue).Name = "lblPeriodFixedValue";
		resources.ApplyResources(this.txtPeriodMinimumValue, "txtPeriodMinimumValue");
		((System.Windows.Forms.Control)(object)this.txtPeriodMinimumValue).Name = "txtPeriodMinimumValue";
		((System.Windows.Forms.Control)(object)this.txtPeriodMinimumValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumper_KeyPress);
		resources.ApplyResources(this.lblPeriodMinimumValue, "lblPeriodMinimumValue");
		this.lblPeriodMinimumValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPeriodMinimumValue).Name = "lblPeriodMinimumValue";
		resources.ApplyResources(this.txtProcedureFeesPercentage, "txtProcedureFeesPercentage");
		((System.Windows.Forms.Control)(object)this.txtProcedureFeesPercentage).Name = "txtProcedureFeesPercentage";
		((System.Windows.Forms.Control)(object)this.txtProcedureFeesPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumper_KeyPress);
		resources.ApplyResources(this.lblProcedureFeesPercentage, "lblProcedureFeesPercentage");
		this.lblProcedureFeesPercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblProcedureFeesPercentage).Name = "lblProcedureFeesPercentage";
		resources.ApplyResources(this.txtVisitFeesPercentage, "txtVisitFeesPercentage");
		((System.Windows.Forms.Control)(object)this.txtVisitFeesPercentage).Name = "txtVisitFeesPercentage";
		((System.Windows.Forms.Control)(object)this.txtVisitFeesPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumper_KeyPress);
		resources.ApplyResources(this.lblVisitFeesPercentage, "lblVisitFeesPercentage");
		this.lblVisitFeesPercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisitFeesPercentage).Name = "lblVisitFeesPercentage";
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.txtEnglishName, "txtEnglishName");
		((System.Windows.Forms.Control)(object)this.txtEnglishName).Name = "txtEnglishName";
		resources.ApplyResources(this.lblEnglishName, "lblEnglishName");
		this.lblEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEnglishName).Name = "lblEnglishName";
		((ControlBase)this.lblEnglishName).WrapText = false;
		resources.ApplyResources(this.txtArabicName, "txtArabicName");
		((System.Windows.Forms.Control)(object)this.txtArabicName).Name = "txtArabicName";
		resources.ApplyResources(this.lblArabicName, "lblArabicName");
		this.lblArabicName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArabicName).Name = "lblArabicName";
		((ControlBase)this.lblArabicName).WrapText = false;
		resources.ApplyResources(this.cboDoctorType, "cboDoctorType");
		((TextEditorControlBase)this.cboDoctorType).AlwaysInEditMode = true;
		this.cboDoctorType.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboDoctorType).Name = "cboDoctorType";
		resources.ApplyResources(this.lblDoctorType, "lblDoctorType");
		this.lblDoctorType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDoctorType).Name = "lblDoctorType";
		((ControlBase)this.lblDoctorType).WrapText = false;
		resources.ApplyResources(this.lblGender, "lblGender");
		this.lblGender.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGender).Name = "lblGender";
		((ControlBase)this.lblGender).WrapText = false;
		resources.ApplyResources(this.cboGender, "cboGender");
		((TextEditorControlBase)this.cboGender).AlwaysInEditMode = true;
		this.cboGender.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboGender).Name = "cboGender";
		resources.ApplyResources(this.lblBirthDate, "lblBirthDate");
		this.lblBirthDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBirthDate).Name = "lblBirthDate";
		((ControlBase)this.lblBirthDate).WrapText = false;
		resources.ApplyResources(this.dtpBirthDate, "dtpBirthDate");
		((UltraWinEditorMaskedControlBase)this.dtpBirthDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpBirthDate).Name = "dtpBirthDate";
		resources.ApplyResources(this.lblNationality, "lblNationality");
		this.lblNationality.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNationality).Name = "lblNationality";
		((ControlBase)this.lblNationality).WrapText = false;
		resources.ApplyResources(this.cboNationality, "cboNationality");
		((TextEditorControlBase)this.cboNationality).AlwaysInEditMode = true;
		this.cboNationality.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboNationality).Name = "cboNationality";
		resources.ApplyResources(this.lblSocialStatus, "lblSocialStatus");
		this.lblSocialStatus.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSocialStatus).Name = "lblSocialStatus";
		((ControlBase)this.lblSocialStatus).WrapText = false;
		resources.ApplyResources(this.cboSocialStatus, "cboSocialStatus");
		((TextEditorControlBase)this.cboSocialStatus).AlwaysInEditMode = true;
		this.cboSocialStatus.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSocialStatus).Name = "cboSocialStatus";
		resources.ApplyResources(this.lblPersonalIDNo, "lblPersonalIDNo");
		this.lblPersonalIDNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPersonalIDNo).Name = "lblPersonalIDNo";
		((ControlBase)this.lblPersonalIDNo).WrapText = false;
		resources.ApplyResources(this.txtPersonalIDNo, "txtPersonalIDNo");
		((TextEditorControlBase)this.txtPersonalIDNo).MaxLength = 14;
		((System.Windows.Forms.Control)(object)this.txtPersonalIDNo).Name = "txtPersonalIDNo";
		((System.Windows.Forms.Control)(object)this.txtPersonalIDNo).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInt_KeyPress);
		resources.ApplyResources(this.lblCountry, "lblCountry");
		this.lblCountry.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCountry).Name = "lblCountry";
		((ControlBase)this.lblCountry).WrapText = false;
		resources.ApplyResources(this.cboCountry, "cboCountry");
		((TextEditorControlBase)this.cboCountry).AlwaysInEditMode = true;
		this.cboCountry.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCountry).Name = "cboCountry";
		((TextEditorControlBase)this.cboCountry).ValueChanged += new System.EventHandler(cboCountry_ValueChanged);
		resources.ApplyResources(this.lblCity, "lblCity");
		this.lblCity.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCity).Name = "lblCity";
		((ControlBase)this.lblCity).WrapText = false;
		resources.ApplyResources(this.cboCity, "cboCity");
		((TextEditorControlBase)this.cboCity).AlwaysInEditMode = true;
		this.cboCity.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCity).Name = "cboCity";
		((TextEditorControlBase)this.cboCity).ValueChanged += new System.EventHandler(cboCity_ValueChanged);
		resources.ApplyResources(this.lblArea, "lblArea");
		this.lblArea.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArea).Name = "lblArea";
		((ControlBase)this.lblArea).WrapText = false;
		resources.ApplyResources(this.cboArea, "cboArea");
		((TextEditorControlBase)this.cboArea).AlwaysInEditMode = true;
		this.cboArea.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboArea).Name = "cboArea";
		resources.ApplyResources(this.lblAddress, "lblAddress");
		this.lblAddress.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAddress).Name = "lblAddress";
		((ControlBase)this.lblAddress).WrapText = false;
		resources.ApplyResources(this.txtAddress, "txtAddress");
		((System.Windows.Forms.Control)(object)this.txtAddress).Name = "txtAddress";
		resources.ApplyResources(this.lblPostalCode, "lblPostalCode");
		this.lblPostalCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPostalCode).Name = "lblPostalCode";
		((ControlBase)this.lblPostalCode).WrapText = false;
		resources.ApplyResources(this.txtPostalCode, "txtPostalCode");
		((System.Windows.Forms.Control)(object)this.txtPostalCode).Name = "txtPostalCode";
		resources.ApplyResources(this.lblEMail, "lblEMail");
		this.lblEMail.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEMail).Name = "lblEMail";
		((ControlBase)this.lblEMail).WrapText = false;
		resources.ApplyResources(this.txtEMail, "txtEMail");
		((System.Windows.Forms.Control)(object)this.txtEMail).Name = "txtEMail";
		resources.ApplyResources(this.lblMobile, "lblMobile");
		this.lblMobile.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMobile).Name = "lblMobile";
		((ControlBase)this.lblMobile).WrapText = false;
		resources.ApplyResources(this.txtMobile, "txtMobile");
		((System.Windows.Forms.Control)(object)this.txtMobile).Name = "txtMobile";
		resources.ApplyResources(this.lblTel, "lblTel");
		this.lblTel.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTel).Name = "lblTel";
		((ControlBase)this.lblTel).WrapText = false;
		resources.ApplyResources(this.txtTel, "txtTel");
		((System.Windows.Forms.Control)(object)this.txtTel).Name = "txtTel";
		resources.ApplyResources(this.lblFees, "lblFees");
		this.lblFees.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFees).Name = "lblFees";
		((ControlBase)this.lblFees).WrapText = false;
		resources.ApplyResources(this.txtFees, "txtFees");
		((System.Windows.Forms.Control)(object)this.txtFees).Name = "txtFees";
		((System.Windows.Forms.Control)(object)this.txtFees).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumper_KeyPress);
		resources.ApplyResources(this.lblConsultingFees, "lblConsultingFees");
		this.lblConsultingFees.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblConsultingFees).Name = "lblConsultingFees";
		((ControlBase)this.lblConsultingFees).WrapText = false;
		resources.ApplyResources(this.txtConsultingFees, "txtConsultingFees");
		((System.Windows.Forms.Control)(object)this.txtConsultingFees).Name = "txtConsultingFees";
		((System.Windows.Forms.Control)(object)this.txtConsultingFees).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumper_KeyPress);
		resources.ApplyResources(this.lblConsultingPeriod, "lblConsultingPeriod");
		this.lblConsultingPeriod.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblConsultingPeriod).Name = "lblConsultingPeriod";
		((ControlBase)this.lblConsultingPeriod).WrapText = false;
		resources.ApplyResources(this.txtConsultingPeriod, "txtConsultingPeriod");
		((System.Windows.Forms.Control)(object)this.txtConsultingPeriod).Name = "txtConsultingPeriod";
		((System.Windows.Forms.Control)(object)this.txtConsultingPeriod).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInt_KeyPress);
		resources.ApplyResources(this.lblConsultingCount, "lblConsultingCount");
		this.lblConsultingCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblConsultingCount).Name = "lblConsultingCount";
		((ControlBase)this.lblConsultingCount).WrapText = false;
		resources.ApplyResources(this.txtConsultingCount, "txtConsultingCount");
		((System.Windows.Forms.Control)(object)this.txtConsultingCount).Name = "txtConsultingCount";
		((System.Windows.Forms.Control)(object)this.txtConsultingCount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInt_KeyPress);
		resources.ApplyResources(this.chkIsActive, "chkIsActive");
		((System.Windows.Forms.Control)(object)this.chkIsActive).Name = "chkIsActive";
		resources.ApplyResources(this.lblUser, "lblUser");
		this.lblUser.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUser).Name = "lblUser";
		((ControlBase)this.lblUser).WrapText = false;
		resources.ApplyResources(this.cboUser, "cboUser");
		((TextEditorControlBase)this.cboUser).AlwaysInEditMode = true;
		this.cboUser.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboUser).Name = "cboUser";
		resources.ApplyResources(this.btnPriceTypeSearch, "btnPriceTypeSearch");
		((AppearanceBase)val22).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val22, "appearance23");
		((ControlBase)this.btnPriceTypeSearch).Appearance = (AppearanceBase)(object)val22;
		((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch).Name = "btnPriceTypeSearch";
		((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch).TabStop = false;
		resources.ApplyResources(this.cboPriceType, "cboPriceType");
		((System.Windows.Forms.Control)(object)this.cboPriceType).Name = "cboPriceType";
		resources.ApplyResources(this.lblPriceType, "lblPriceType");
		this.lblPriceType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPriceType).Name = "lblPriceType";
		((ControlBase)this.lblPriceType).WrapText = false;
		resources.ApplyResources(this.chkIsExtended, "chkIsExtended");
		((System.Windows.Forms.Control)(object)this.chkIsExtended).Name = "chkIsExtended";
		((UltraToggleEditorBase)this.chkIsExtended).CheckedChanged += new System.EventHandler(chkIsExtended_CheckedChanged);
		resources.ApplyResources(this.lblExtendedFees, "lblExtendedFees");
		this.lblExtendedFees.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExtendedFees).Name = "lblExtendedFees";
		((ControlBase)this.lblExtendedFees).WrapText = false;
		resources.ApplyResources(this.txtExtendedFees, "txtExtendedFees");
		((System.Windows.Forms.Control)(object)this.txtExtendedFees).Name = "txtExtendedFees";
		((System.Windows.Forms.Control)(object)this.txtExtendedFees).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumper_KeyPress);
		resources.ApplyResources(this.cboSpecialization, "cboSpecialization");
		this.cboSpecialization.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSpecialization).Name = "cboSpecialization";
		((TextEditorControlBase)this.cboSpecialization).ValueChanged += new System.EventHandler(cboSpecialization_ValueChanged);
		resources.ApplyResources(this.lblSpecialization, "lblSpecialization");
		((AppearanceBase)val23).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val23, "appearance24");
		((ControlBase)this.lblSpecialization).Appearance = (AppearanceBase)(object)val23;
		this.lblSpecialization.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSpecialization).Name = "lblSpecialization";
		((ControlBase)this.lblSpecialization).WrapText = false;
		resources.ApplyResources(this.cboSpecializationDetail, "cboSpecializationDetail");
		this.cboSpecializationDetail.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSpecializationDetail).Name = "cboSpecializationDetail";
		resources.ApplyResources(this.lblSpecializationDetail, "lblSpecializationDetail");
		this.lblSpecializationDetail.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSpecializationDetail).Name = "lblSpecializationDetail";
		((ControlBase)this.lblSpecializationDetail).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSpecialization);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSpecialization);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSpecializationDetail);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSpecializationDetail);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsExtended);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsActive);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBirthDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpBirthDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboArea);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArea);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCity);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCity);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCountry);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCountry);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSocialStatus);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSocialStatus);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboNationality);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNationality);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboUser);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUser);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboGender);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGender);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboDoctorType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDoctorType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtConsultingCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtConsultingPeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblConsultingCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExtendedFees);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtConsultingFees);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblConsultingPeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtFees);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExtendedFees);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblConsultingFees);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFees);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtMobile);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEMail);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMobile);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPostalCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEMail);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAddress);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPostalCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAddress);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPersonalIDNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPersonalIDNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArabicName);
		base.Name = "frmDoctors";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPersonalIDNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPersonalIDNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAddress, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPostalCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAddress, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEMail, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPostalCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMobile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEMail, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtMobile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFees, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblConsultingFees, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExtendedFees, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtFees, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblConsultingPeriod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtConsultingFees, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExtendedFees, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblConsultingCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtConsultingPeriod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtConsultingCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDoctorType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboDoctorType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGender, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboGender, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUser, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboUser, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNationality, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboNationality, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSocialStatus, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSocialStatus, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCountry, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCountry, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCity, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCity, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArea, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboArea, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpBirthDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBirthDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsActive, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsExtended, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSpecializationDetail, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSpecializationDetail, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSpecialization, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSpecialization, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGCertificates).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGProceduresPercentages).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMonthlyFees).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPeriodFixedValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPeriodMinimumValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtProcedureFeesPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisitFeesPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDoctorType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboGender).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBirthDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboNationality).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSocialStatus).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPersonalIDNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPostalCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobile).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFees).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtConsultingFees).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtConsultingPeriod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtConsultingCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboUser).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsExtended).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExtendedFees).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSpecialization).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSpecializationDetail).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
