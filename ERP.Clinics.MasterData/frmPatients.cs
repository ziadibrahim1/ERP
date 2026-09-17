using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Clinics;
using BusinessLayer.General;
using BusinessLayer.HR;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Accounting.MasterData;
using ERP.Classes;
using ERP.Properties;
using ERP.StockControl.MasterData;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.Clinics.MasterData;

public class frmPatients : frmButtons
{
	private DataTable dtTitle;

	private DataTable dtSocialStatus;

	private DataTable dtGender;

	private DataTable dtNationality;

	private DataTable dtCountries;

	private DataTable dtCities;

	private DataTable dtAreas;

	private DataTable dtBloodTypes;

	private DataTable dtDoctors;

	private DataTable dtPosition;

	private DataTable dtPriceType;

	private DataTable dtSubAccounts;

	private DataRow drMaster;

	private bool CanEditVoucherNumber = true;

	private int Patient = 0;

	public int PatientID = 0;

	private IContainer components = null;

	public UltraTextEditor txtCode;

	public UltraButton btnSearch;

	public UltraButton btnPriveous;

	public UltraButton btnNext;

	public UltraLabel lblTitle;

	public UltraLabel lblTitle2;

	public UltraLabel lblCode;

	public UltraButton btnCopyTo;

	private UltraTextEditor txtPatientNameEn;

	private UltraLabel lblPatientNameEn;

	private UltraTextEditor txtPatientNameAr;

	private UltraLabel lblPatientNameAr;

	private UltraLabel lblPatientTitle;

	private UltraComboEditor cboTitle;

	private UltraDateTimeEditor dtpBirthDate;

	private UltraLabel lblBirthDate;

	private UltraComboEditor cboSocialStatus;

	private UltraLabel lblSocialStatus;

	private UltraComboEditor cboGender;

	private UltraLabel lblGender;

	private UltraComboEditor cboNationality;

	private UltraLabel lblNationality;

	private UltraTextEditor txtPersonalIDNo;

	private UltraLabel lblPersonalIDNo;

	private UltraComboEditor cboCountry;

	private UltraLabel lblCountry;

	private UltraComboEditor cboArea;

	private UltraLabel lblArea;

	private UltraComboEditor cboCity;

	private UltraLabel lblCity;

	private UltraTextEditor txtAddress;

	private UltraLabel lblAddress;

	private UltraTextEditor txtPostalCode;

	private UltraLabel lblPostalCode;

	private UltraTextEditor txtMobile;

	private UltraLabel lblMobile;

	private UltraTextEditor txtTel;

	private UltraLabel lblTel;

	private UltraTextEditor txtEMail;

	private UltraLabel lblEMail;

	private UltraComboEditor cboBloodType;

	private UltraLabel lblBloodType;

	private UltraTextEditor txtPMI;

	private UltraLabel lblPMI;

	private UltraTextEditor txtWeight;

	private UltraLabel lblWeight;

	private UltraTextEditor txtHeight;

	private UltraLabel lblHeight;

	private UltraComboEditor cboDoctor;

	private UltraLabel lblDoctor;

	private UltraCheckEditor chkClosed;

	private UltraDateTimeEditor dtpClosedDate;

	private UltraComboEditor cboPosition;

	private UltraLabel lblPosition;

	private UltraComboEditor cboHusbandPosition;

	private UltraLabel lblHusbandPosition;

	private UltraTextEditor txtNotes;

	private UltraLabel lblNotes;

	public UltraButton btnSubAccountSearch;

	public UltraButton btnPriceTypeSearch;

	private UltraComboEditor cboPriceType;

	private UltraLabel lblPriceType;

	private UltraComboEditor cboSubAccount;

	private UltraLabel lblSubAccount;

	private UltraTextEditor txtDiscountPercentage;

	private UltraLabel lblDiscountPercentage;

	public frmPatients()
	{
		InitializeComponent();
		TableName = "CL_Patients";
		NoCol = "PatientCode";
		IDCol = "PatientID";
		DateCol = "GetDate()";
	}

	public frmPatients(int PatientID)
		: this()
	{
		Patient = PatientID;
	}

	public override void SetSecurity()
	{
		base.SetSecurity();
		UltraButton obj = btnNext;
		UltraButton obj2 = btnPriveous;
		bool flag = (((Control)(object)btnSearch).Enabled = CanSearching);
		bool enabled = (((Control)(object)obj2).Enabled = flag);
		((Control)(object)obj).Enabled = enabled;
	}

	public override void CallButtons(KeyEventArgs e)
	{
		base.CallButtons(e);
		if (!Adding && !Updating)
		{
			if (e.KeyCode == Keys.F8 && ((Control)(object)btnSearch).Enabled && ((Control)(object)btnSearch).Visible)
			{
				btnSearch_Click(null, null);
			}
			else if (e.KeyValue == 39 && ((Control)(object)btnNext).Enabled && ((Control)(object)btnNext).Visible)
			{
				NextData();
			}
			else if (e.KeyValue == 37 && ((Control)(object)btnPriveous).Enabled && ((Control)(object)btnPriveous).Visible)
			{
				PriveousData();
			}
			else if (e.KeyCode == Keys.F7 && ((Control)(object)btnCopyTo).Enabled && ((Control)(object)btnCopyTo).Visible)
			{
				btnCopyToClick();
			}
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)txtCode).Enabled = NavMode || CanEditVoucherNumber;
		((EditorButtonControlBase)txtCode).ReadOnly = false;
		((Control)(object)btnSearch).Visible = NavMode;
		((Control)(object)btnNext).Visible = NavMode;
		((Control)(object)btnCopyTo).Visible = NavMode;
		((Control)(object)btnPriveous).Visible = NavMode;
		((EditorButtonControlBase)txtDiscountPercentage).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPriceType).ReadOnly = NavMode || !CanModifyPriceType;
		((EditorButtonControlBase)cboSubAccount).ReadOnly = NavMode || !CanModifySubAccount;
		((Control)(object)btnPriceTypeSearch).Visible = !NavMode && CanModifyPriceType;
		((Control)(object)btnSubAccountSearch).Visible = !NavMode && CanModifySubAccount;
		((EditorButtonControlBase)txtPatientNameAr).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPatientNameEn).ReadOnly = NavMode;
		((EditorButtonControlBase)cboTitle).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpBirthDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSocialStatus).ReadOnly = NavMode;
		((EditorButtonControlBase)cboGender).ReadOnly = NavMode;
		((EditorButtonControlBase)cboNationality).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPersonalIDNo).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCountry).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCity).ReadOnly = NavMode;
		((EditorButtonControlBase)cboArea).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAddress).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPostalCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtMobile).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTel).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEMail).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBloodType).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPMI).ReadOnly = NavMode;
		((EditorButtonControlBase)txtWeight).ReadOnly = NavMode;
		((EditorButtonControlBase)txtHeight).ReadOnly = NavMode;
		((EditorButtonControlBase)cboDoctor).ReadOnly = NavMode;
		((Control)(object)chkClosed).Enabled = !NavMode;
		((EditorButtonControlBase)cboPosition).ReadOnly = NavMode;
		((EditorButtonControlBase)cboHusbandPosition).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		if (!base.DesignMode)
		{
			CanEditVoucherNumber = GlobalFunctions.GetOption("CanEditVoucherNumber");
			DataTable dt = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboTransactionBranch, dt, "BranchID", "BranchName");
		}
		dtTitle = Titles.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTitle, dtTitle, "TitleID", "TitleName");
		dtSocialStatus = SocialStatus.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSocialStatus, dtSocialStatus, "SocialStatusID", "SocialStatusName");
		dtGender = Gender.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboGender, dtGender, "GenderID", "GenderName");
		dtNationality = Nationalities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboNationality, dtNationality, "NationalityID", "NationalityName");
		dtCountries = Countries.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCountry, dtCountries, "CountryID", "CountryName");
		dtCities = Cities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCity, dtCities, "CityID", "CityName");
		dtAreas = Areas.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboArea, dtAreas, "AreaID", "AreaName");
		dtBloodTypes = BloodTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboBloodType, dtBloodTypes, "BloodTypeID", "BloodTypeName");
		dtDoctors = Doctors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboDoctor, dtDoctors, "DoctorID", "DoctorName");
		dtPosition = Positions.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboPosition, dtPosition, "PositionID", "PositionName");
		GlobalFunctions.FillCombo(cboHusbandPosition, dtPosition, "PositionID", "PositionName");
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
		dtSubAccounts = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSubAccount, dtSubAccounts, "SubAccountID", "SubAccountName");
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		((Control)(object)txtCode).Text = (Adding ? Patients.GetCode(IsFromServer: true) : "");
		((TextEditorControlBase)txtPatientNameAr).Clear();
		((TextEditorControlBase)txtPatientNameEn).Clear();
		cboTitle.SelectedIndex = -1;
		dtpBirthDate.DateTime = DateTime.Today;
		cboSocialStatus.SelectedIndex = -1;
		cboGender.SelectedIndex = -1;
		cboNationality.SelectedIndex = -1;
		((TextEditorControlBase)txtPersonalIDNo).Clear();
		cboCountry.SelectedIndex = -1;
		cboCity.SelectedIndex = -1;
		cboArea.SelectedIndex = -1;
		cboPriceType.SelectedIndex = ((dtPriceType.Rows.Count <= 0) ? (-1) : 0);
		cboSubAccount.SelectedIndex = -1;
		((TextEditorControlBase)txtAddress).Clear();
		((TextEditorControlBase)txtPostalCode).Clear();
		((Control)(object)txtDiscountPercentage).Text = "0";
		((TextEditorControlBase)txtMobile).Clear();
		((TextEditorControlBase)txtTel).Clear();
		((TextEditorControlBase)txtEMail).Clear();
		cboBloodType.SelectedIndex = -1;
		((TextEditorControlBase)txtPMI).Clear();
		((TextEditorControlBase)txtWeight).Clear();
		((TextEditorControlBase)txtHeight).Clear();
		cboDoctor.SelectedIndex = -1;
		((UltraToggleEditorBase)chkClosed).Checked = false;
		cboPosition.SelectedIndex = -1;
		cboHusbandPosition.SelectedIndex = -1;
		((TextEditorControlBase)txtNotes).Clear();
	}

	public override void FillData()
	{
		if (Patient == -1)
		{
			drMaster = null;
			btnAddClick();
			((Control)(object)btnOK).Visible = false;
		}
		else if (Patient > 0 || Patient < -1)
		{
			DataTable dataTable = Patients.Select(Patient.ToString(), GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
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
		if (RowID == "")
		{
			drMaster = null;
			DisplayData();
			return;
		}
		DataTable dataTable2 = Patients.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dataTable2.Rows.Count > 0)
		{
			drMaster = dataTable2.Rows[0];
		}
		else
		{
			drMaster = null;
		}
		DisplayData();
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (drMaster != null)
		{
			((TextEditorControlBase)cboTransactionBranch).Value = drMaster["BranchID"];
			((Control)(object)txtCode).Text = drMaster["PatientCode"].ToString();
			((Control)(object)txtPatientNameAr).Text = drMaster["PatientNameAr"].ToString();
			((Control)(object)txtPatientNameEn).Text = drMaster["PatientNameEn"].ToString();
			((TextEditorControlBase)cboTitle).Value = drMaster["TitleID"];
			((TextEditorControlBase)cboPriceType).Value = drMaster["PriceTypeID"];
			((TextEditorControlBase)cboSubAccount).Value = drMaster["SubAccountID"];
			dtpBirthDate.Value = drMaster["BirthDate"];
			((TextEditorControlBase)cboSocialStatus).Value = drMaster["SocialStatusID"];
			((TextEditorControlBase)cboGender).Value = drMaster["GenderID"];
			((TextEditorControlBase)cboNationality).Value = drMaster["NationalityID"];
			((Control)(object)txtPersonalIDNo).Text = drMaster["PersonalIDNo"].ToString();
			((Control)(object)txtDiscountPercentage).Text = drMaster["DiscountPercentage"].ToString();
			((TextEditorControlBase)cboCountry).Value = drMaster["CountryID"];
			((TextEditorControlBase)cboCity).Value = drMaster["CityID"];
			((TextEditorControlBase)cboArea).Value = drMaster["AreaID"];
			((Control)(object)txtAddress).Text = drMaster["Address"].ToString();
			((Control)(object)txtPostalCode).Text = drMaster["PostalCode"].ToString();
			((Control)(object)txtMobile).Text = drMaster["Mobile"].ToString();
			((Control)(object)txtTel).Text = drMaster["Tel"].ToString();
			((Control)(object)txtEMail).Text = drMaster["EMail"].ToString();
			((TextEditorControlBase)cboBloodType).Value = drMaster["BloodTypeID"];
			((Control)(object)txtPMI).Text = drMaster["PMIPercentage"].ToString();
			((Control)(object)txtWeight).Text = drMaster["Weight"].ToString();
			((Control)(object)txtHeight).Text = drMaster["Height"].ToString();
			((TextEditorControlBase)cboDoctor).Value = drMaster["DefaultDoctorID"];
			((UltraToggleEditorBase)chkClosed).Checked = bool.Parse(drMaster["IsClosed"].ToString());
			dtpClosedDate.Value = drMaster["ClosedDate"];
			((TextEditorControlBase)cboPosition).Value = drMaster["PositionID"];
			((TextEditorControlBase)cboHusbandPosition).Value = drMaster["HusbandPositionID"];
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
		}
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال رقم الملف", "Please Enter The Profile Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtPatientNameAr).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال الاسم بالعربية", "Please Enter The Patient Arabic Name");
			((TextEditorControlBase)txtPatientNameAr).Focus();
			return false;
		}
		if (cboGender.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار النوع", "Please Select Gender");
			((TextEditorControlBase)cboGender).Focus();
			cboGender.DropDown();
			return false;
		}
		if (Main.CheckForValue("CL_Patients", "PatientNameAr", ((Control)(object)txtPatientNameAr).Text, Adding ? "0" : drMaster["PatientNameAr"].ToString(), IsFromServer: true) > 0)
		{
			GlobalVariables.InformationMB.Show("هذا الاسم متواجد من قبل", "Patient Name Already Exists ");
			((TextEditorControlBase)txtPatientNameAr).Focus();
			return false;
		}
		if (Main.CheckForValue("CL_Patients", "PatientCode", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["PatientCode"].ToString(), IsFromServer: true) > 0)
		{
			string code = Patients.GetCode(IsFromServer: true);
			GlobalVariables.QuestionMB.Show("رقم هذا الملف متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Profile Number Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = code;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		try
		{
			PatientID = Patients.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtPatientNameAr).Text, (((Control)(object)txtPatientNameEn).Text == "") ? "Null" : ((Control)(object)txtPatientNameEn).Text, (cboTitle.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTitle).Value.ToString(), (cboGender.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboGender).Value.ToString(), (dtpBirthDate.Value == null) ? "Null" : dtpBirthDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboSocialStatus.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSocialStatus).Value.ToString(), (cboNationality.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboNationality).Value.ToString(), (((Control)(object)txtPersonalIDNo).Text == "") ? "Null" : ((Control)(object)txtPersonalIDNo).Text, (cboCountry.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCountry).Value.ToString(), (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), ((Control)(object)txtAddress).Text, ((Control)(object)txtPostalCode).Text, ((Control)(object)txtEMail).Text, ((Control)(object)txtMobile).Text, ((Control)(object)txtTel).Text, (cboBloodType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBloodType).Value.ToString(), (((Control)(object)txtPMI).Text == "") ? "Null" : ((Control)(object)txtPMI).Text, (((Control)(object)txtWeight).Text == "") ? "Null" : ((Control)(object)txtWeight).Text, (((Control)(object)txtHeight).Text == "") ? "Null" : ((Control)(object)txtHeight).Text, ((UltraToggleEditorBase)chkClosed).Checked ? "1" : "0", ((UltraToggleEditorBase)chkClosed).Checked ? dtpClosedDate.DateTime.ToString(GlobalVariables.DateLongFormate) : "Null", (cboDoctor.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDoctor).Value.ToString(), (cboPosition.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPosition).Value.ToString(), (cboHusbandPosition.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboHusbandPosition).Value.ToString(), ((Control)(object)txtNotes).Text, (((Control)(object)txtDiscountPercentage).Text == "") ? "0" : ((Control)(object)txtDiscountPercentage).Text, (cboPriceType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), (cboSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccount).Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
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
			PatientID = Patients.Insert_Update(drMaster["PatientID"].ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtPatientNameAr).Text, (((Control)(object)txtPatientNameEn).Text == "") ? "Null" : ((Control)(object)txtPatientNameEn).Text, (cboTitle.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTitle).Value.ToString(), (cboGender.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboGender).Value.ToString(), (dtpBirthDate.Value == null) ? "Null" : dtpBirthDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboSocialStatus.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSocialStatus).Value.ToString(), (cboNationality.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboNationality).Value.ToString(), (((Control)(object)txtPersonalIDNo).Text == "") ? "Null" : ((Control)(object)txtPersonalIDNo).Text, (cboCountry.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCountry).Value.ToString(), (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), ((Control)(object)txtAddress).Text, ((Control)(object)txtPostalCode).Text, ((Control)(object)txtEMail).Text, ((Control)(object)txtMobile).Text, ((Control)(object)txtTel).Text, (cboBloodType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBloodType).Value.ToString(), (((Control)(object)txtPMI).Text == "") ? "Null" : ((Control)(object)txtPMI).Text, (((Control)(object)txtWeight).Text == "") ? "Null" : ((Control)(object)txtWeight).Text, (((Control)(object)txtHeight).Text == "") ? "Null" : ((Control)(object)txtHeight).Text, ((UltraToggleEditorBase)chkClosed).Checked ? "1" : "0", ((UltraToggleEditorBase)chkClosed).Checked ? dtpClosedDate.DateTime.ToString(GlobalVariables.DateLongFormate) : "Null", (cboDoctor.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDoctor).Value.ToString(), (cboPosition.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPosition).Value.ToString(), (cboHusbandPosition.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboHusbandPosition).Value.ToString(), ((Control)(object)txtNotes).Text, (((Control)(object)txtDiscountPercentage).Text == "") ? "0" : ((Control)(object)txtDiscountPercentage).Text, (cboPriceType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), (cboSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccount).Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
		}
		catch
		{
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
		}
	}

	public override void DeleteData()
	{
		Patients.Delete(drMaster["PatientID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
	}

	public virtual void btnCopyToClick()
	{
		if (!CanAdd)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		Adding = true;
		drMaster = null;
		SetControls(NavMode: false);
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
		dtTitle = Titles.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTitle, dtTitle, "TitleID", "TitleName");
		dtSocialStatus = SocialStatus.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSocialStatus, dtSocialStatus, "SocialStatusID", "SocialStatusName");
		dtGender = Gender.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboGender, dtGender, "GenderID", "GenderName");
		dtNationality = Nationalities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboNationality, dtNationality, "NationalityID", "NationalityName");
		dtCountries = Countries.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCountry, dtCountries, "CountryID", "CountryName");
		dtCities = Cities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCity, dtCities, "CityID", "CityName");
		dtAreas = Areas.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboArea, dtAreas, "AreaID", "AreaName");
		dtBloodTypes = BloodTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboBloodType, dtBloodTypes, "BloodTypeID", "BloodTypeName");
		dtDoctors = Doctors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboDoctor, dtDoctors, "DoctorID", "DoctorName");
		dtPosition = Positions.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboPosition, dtPosition, "PositionID", "PositionName");
		GlobalFunctions.FillCombo(cboHusbandPosition, dtPosition, "PositionID", "PositionName");
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
		dtSubAccounts = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSubAccount, dtSubAccounts, "SubAccountID", "SubAccountName");
	}

	public virtual void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.PatientsSearchReport(IsFromServer: true);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["PatientID"].ToString();
			FillData();
		}
	}

	public virtual void txtCode_KeyUp(object sender, KeyEventArgs e)
	{
		if (!CanSearching || e.KeyCode != Keys.Return || TableName.Length <= 0 || NoCol.Length <= 0 || ((Control)(object)txtCode).Text.Length <= 0)
		{
			return;
		}
		if (Adding || Updating)
		{
			e.Handled = true;
			SendKeys.Send("{tab}");
			return;
		}
		DataTable comboData = Main.GetComboData(TableName, IDCol, NoCol + "=''" + ((Control)(object)txtCode).Text.Trim() + "'' And Deleted=0 Order by year(" + DateCol + ") Desc");
		if (comboData.Rows.Count > 0)
		{
			RowID = comboData.Rows[0][IDCol].ToString();
			dtSearchResult = null;
		}
		else
		{
			RowID = "";
		}
		FillData();
	}

	private void btnPriveous_Click(object sender, EventArgs e)
	{
		PriveousData();
	}

	private void btnNext_Click(object sender, EventArgs e)
	{
		NextData();
	}

	public virtual void PriveousData()
	{
		if (dtSearchResult != null && dtSearchResult.Rows.Count > 1)
		{
			if (drMaster != null)
			{
				for (int i = 0; i < dtSearchResult.Rows.Count - 1; i++)
				{
					if (dtSearchResult.Rows[i][IDCol].ToString() == drMaster[IDCol].ToString())
					{
						RowID = dtSearchResult.Rows[i + 1][IDCol].ToString();
						FillData();
						break;
					}
				}
			}
			else
			{
				RowID = dtSearchResult.Rows[dtSearchResult.Rows.Count - 1][IDCol].ToString();
				FillData();
			}
		}
		else
		{
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, "", "0");
			if (dataTable.Rows.Count > 0)
			{
				RowID = dataTable.Rows[0][IDCol].ToString();
			}
			else
			{
				RowID = "";
			}
			FillData();
		}
	}

	public virtual void NextData()
	{
		if (dtSearchResult != null && dtSearchResult.Rows.Count > 1)
		{
			if (drMaster != null)
			{
				for (int i = 1; i < dtSearchResult.Rows.Count; i++)
				{
					if (dtSearchResult.Rows[i][IDCol].ToString() == drMaster[IDCol].ToString())
					{
						RowID = dtSearchResult.Rows[i - 1][IDCol].ToString();
						FillData();
						break;
					}
				}
			}
			else
			{
				RowID = dtSearchResult.Rows[0][IDCol].ToString();
				FillData();
			}
		}
		else
		{
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, "", "1");
			if (dataTable.Rows.Count > 0)
			{
				RowID = dataTable.Rows[0][IDCol].ToString();
			}
			else
			{
				RowID = "";
			}
			FillData();
		}
	}

	private void btnCopyTo_Click(object sender, EventArgs e)
	{
		btnCopyToClick();
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void chkClosed_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)dtpClosedDate).Enabled = ((UltraToggleEditorBase)chkClosed).Checked;
	}

	private void cboCity_ValueChanged(object sender, EventArgs e)
	{
		if (cboCity.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtAreas);
			dataView.RowFilter = "CityID=" + ((TextEditorControlBase)cboCity).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboArea.DataSource = dataView;
			cboArea.DisplayMember = "AreaName";
			cboArea.ValueMember = "AreaID";
		}
	}

	private void txtPersonalIDNo_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void btnPriceTypeSearch_Click(object sender, EventArgs e)
	{
		frmPricesTypesChange frmPricesTypesChange2 = new frmPricesTypesChange(base.Name);
		frmPricesTypesChange2.WindowState = FormWindowState.Normal;
		frmPricesTypesChange2.ShowDialog();
		((TextEditorControlBase)cboPriceType).Value = ((frmPricesTypesChange2.PriceTypeID > 0) ? ((object)frmPricesTypesChange2.PriceTypeID) : ((TextEditorControlBase)cboPriceType).Value);
	}

	private void btnSubAccountSearch_Click(object sender, EventArgs e)
	{
		frmSubAccountsTreeChange frmSubAccountsTreeChange2 = new frmSubAccountsTreeChange("ERP.POS.MasterData.frmPOSClients");
		frmSubAccountsTreeChange2.WindowState = FormWindowState.Normal;
		frmSubAccountsTreeChange2.ShowDialog();
		((TextEditorControlBase)cboSubAccount).Value = ((frmSubAccountsTreeChange2.SubAccountID > 0) ? ((object)frmSubAccountsTreeChange2.SubAccountID) : ((TextEditorControlBase)cboSubAccount).Value);
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
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Expected O, but got Unknown
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Expected O, but got Unknown
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Expected O, but got Unknown
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Expected O, but got Unknown
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Expected O, but got Unknown
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Expected O, but got Unknown
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Expected O, but got Unknown
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Expected O, but got Unknown
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Expected O, but got Unknown
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Expected O, but got Unknown
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Expected O, but got Unknown
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Expected O, but got Unknown
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Expected O, but got Unknown
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Expected O, but got Unknown
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Expected O, but got Unknown
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Expected O, but got Unknown
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Expected O, but got Unknown
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Expected O, but got Unknown
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Expected O, but got Unknown
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Expected O, but got Unknown
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Expected O, but got Unknown
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Expected O, but got Unknown
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Expected O, but got Unknown
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Expected O, but got Unknown
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Expected O, but got Unknown
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Expected O, but got Unknown
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Expected O, but got Unknown
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Expected O, but got Unknown
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Expected O, but got Unknown
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Expected O, but got Unknown
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Expected O, but got Unknown
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Expected O, but got Unknown
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Expected O, but got Unknown
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Expected O, but got Unknown
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Expected O, but got Unknown
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Expected O, but got Unknown
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Expected O, but got Unknown
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Expected O, but got Unknown
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Expected O, but got Unknown
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Expected O, but got Unknown
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Expected O, but got Unknown
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Expected O, but got Unknown
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Expected O, but got Unknown
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Expected O, but got Unknown
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Expected O, but got Unknown
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Expected O, but got Unknown
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Expected O, but got Unknown
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Expected O, but got Unknown
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Clinics.MasterData.frmPatients));
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
		Appearance val30 = new Appearance();
		Appearance val31 = new Appearance();
		Appearance val32 = new Appearance();
		Appearance val33 = new Appearance();
		this.txtCode = new UltraTextEditor();
		this.btnSearch = new UltraButton();
		this.btnPriveous = new UltraButton();
		this.btnNext = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.lblTitle2 = new UltraLabel();
		this.lblCode = new UltraLabel();
		this.btnCopyTo = new UltraButton();
		this.txtPatientNameEn = new UltraTextEditor();
		this.lblPatientNameEn = new UltraLabel();
		this.txtPatientNameAr = new UltraTextEditor();
		this.lblPatientNameAr = new UltraLabel();
		this.lblPatientTitle = new UltraLabel();
		this.cboTitle = new UltraComboEditor();
		this.dtpBirthDate = new UltraDateTimeEditor();
		this.lblBirthDate = new UltraLabel();
		this.cboSocialStatus = new UltraComboEditor();
		this.lblSocialStatus = new UltraLabel();
		this.cboGender = new UltraComboEditor();
		this.lblGender = new UltraLabel();
		this.cboNationality = new UltraComboEditor();
		this.lblNationality = new UltraLabel();
		this.txtPersonalIDNo = new UltraTextEditor();
		this.lblPersonalIDNo = new UltraLabel();
		this.cboCountry = new UltraComboEditor();
		this.lblCountry = new UltraLabel();
		this.cboArea = new UltraComboEditor();
		this.lblArea = new UltraLabel();
		this.cboCity = new UltraComboEditor();
		this.lblCity = new UltraLabel();
		this.txtAddress = new UltraTextEditor();
		this.lblAddress = new UltraLabel();
		this.txtPostalCode = new UltraTextEditor();
		this.lblPostalCode = new UltraLabel();
		this.txtMobile = new UltraTextEditor();
		this.lblMobile = new UltraLabel();
		this.txtTel = new UltraTextEditor();
		this.lblTel = new UltraLabel();
		this.txtEMail = new UltraTextEditor();
		this.lblEMail = new UltraLabel();
		this.cboBloodType = new UltraComboEditor();
		this.lblBloodType = new UltraLabel();
		this.txtPMI = new UltraTextEditor();
		this.lblPMI = new UltraLabel();
		this.txtWeight = new UltraTextEditor();
		this.lblWeight = new UltraLabel();
		this.txtHeight = new UltraTextEditor();
		this.lblHeight = new UltraLabel();
		this.cboDoctor = new UltraComboEditor();
		this.lblDoctor = new UltraLabel();
		this.chkClosed = new UltraCheckEditor();
		this.dtpClosedDate = new UltraDateTimeEditor();
		this.cboPosition = new UltraComboEditor();
		this.lblPosition = new UltraLabel();
		this.cboHusbandPosition = new UltraComboEditor();
		this.lblHusbandPosition = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblNotes = new UltraLabel();
		this.btnSubAccountSearch = new UltraButton();
		this.btnPriceTypeSearch = new UltraButton();
		this.cboPriceType = new UltraComboEditor();
		this.lblPriceType = new UltraLabel();
		this.cboSubAccount = new UltraComboEditor();
		this.lblSubAccount = new UltraLabel();
		this.txtDiscountPercentage = new UltraTextEditor();
		this.lblDiscountPercentage = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPatientNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPatientNameAr).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTitle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBirthDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSocialStatus).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboGender).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboNationality).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPersonalIDNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPostalCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobile).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBloodType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPMI).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtWeight).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtHeight).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDoctor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkClosed).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpClosedDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPosition).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboHusbandPosition).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountPercentage).BeginInit();
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
		resources.ApplyResources(val, "appearance34");
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
		((System.Windows.Forms.Control)(object)this.txtCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtCode_KeyUp);
		resources.ApplyResources(this.btnSearch, "btnSearch");
		((AppearanceBase)val3).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val3, "appearance35");
		((ControlBase)this.btnSearch).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.btnSearch).Name = "btnSearch";
		((System.Windows.Forms.Control)(object)this.btnSearch).Click += new System.EventHandler(btnSearch_Click);
		resources.ApplyResources(this.btnPriveous, "btnPriveous");
		((AppearanceBase)val4).Image = ERP.Properties.Resources.BarLeft;
		resources.ApplyResources(val4, "appearance36");
		((ControlBase)this.btnPriveous).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.btnPriveous).Name = "btnPriveous";
		((System.Windows.Forms.Control)(object)this.btnPriveous).Click += new System.EventHandler(btnPriveous_Click);
		resources.ApplyResources(this.btnNext, "btnNext");
		((AppearanceBase)val5).Image = ERP.Properties.Resources.BarRight;
		resources.ApplyResources(val5, "appearance37");
		((ControlBase)this.btnNext).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.btnNext).Name = "btnNext";
		((System.Windows.Forms.Control)(object)this.btnNext).Click += new System.EventHandler(btnNext_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val6).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val6, "appearance38");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val7).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val7, "appearance39");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblCode, "lblCode");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val8).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val8, "appearance40");
		((ControlBase)this.lblCode).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.lblCode).Name = "lblCode";
		((UltraControlBase)this.lblCode).UseAppStyling = false;
		resources.ApplyResources(this.btnCopyTo, "btnCopyTo");
		((System.Windows.Forms.Control)(object)this.btnCopyTo).Name = "btnCopyTo";
		((System.Windows.Forms.Control)(object)this.btnCopyTo).Click += new System.EventHandler(btnCopyTo_Click);
		resources.ApplyResources(this.txtPatientNameEn, "txtPatientNameEn");
		((System.Windows.Forms.Control)(object)this.txtPatientNameEn).Name = "txtPatientNameEn";
		resources.ApplyResources(this.lblPatientNameEn, "lblPatientNameEn");
		this.lblPatientNameEn.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPatientNameEn).Name = "lblPatientNameEn";
		((ControlBase)this.lblPatientNameEn).WrapText = false;
		resources.ApplyResources(this.txtPatientNameAr, "txtPatientNameAr");
		((System.Windows.Forms.Control)(object)this.txtPatientNameAr).Name = "txtPatientNameAr";
		resources.ApplyResources(this.lblPatientNameAr, "lblPatientNameAr");
		this.lblPatientNameAr.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPatientNameAr).Name = "lblPatientNameAr";
		((ControlBase)this.lblPatientNameAr).WrapText = false;
		resources.ApplyResources(this.lblPatientTitle, "lblPatientTitle");
		this.lblPatientTitle.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPatientTitle).Name = "lblPatientTitle";
		((ControlBase)this.lblPatientTitle).WrapText = false;
		resources.ApplyResources(this.cboTitle, "cboTitle");
		this.cboTitle.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboTitle).Name = "cboTitle";
		resources.ApplyResources(this.dtpBirthDate, "dtpBirthDate");
		((UltraWinEditorMaskedControlBase)this.dtpBirthDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpBirthDate).Name = "dtpBirthDate";
		resources.ApplyResources(this.lblBirthDate, "lblBirthDate");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val9, "appearance41");
		((ControlBase)this.lblBirthDate).Appearance = (AppearanceBase)(object)val9;
		this.lblBirthDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBirthDate).Name = "lblBirthDate";
		((ControlBase)this.lblBirthDate).WrapText = false;
		resources.ApplyResources(this.cboSocialStatus, "cboSocialStatus");
		((System.Windows.Forms.Control)(object)this.cboSocialStatus).Name = "cboSocialStatus";
		resources.ApplyResources(this.lblSocialStatus, "lblSocialStatus");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val10, "appearance42");
		((ControlBase)this.lblSocialStatus).Appearance = (AppearanceBase)(object)val10;
		this.lblSocialStatus.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSocialStatus).Name = "lblSocialStatus";
		((ControlBase)this.lblSocialStatus).WrapText = false;
		resources.ApplyResources(this.cboGender, "cboGender");
		((System.Windows.Forms.Control)(object)this.cboGender).Name = "cboGender";
		resources.ApplyResources(this.lblGender, "lblGender");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val11, "appearance43");
		((ControlBase)this.lblGender).Appearance = (AppearanceBase)(object)val11;
		this.lblGender.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGender).Name = "lblGender";
		((ControlBase)this.lblGender).WrapText = false;
		resources.ApplyResources(this.cboNationality, "cboNationality");
		((System.Windows.Forms.Control)(object)this.cboNationality).Name = "cboNationality";
		resources.ApplyResources(this.lblNationality, "lblNationality");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val12, "appearance44");
		((ControlBase)this.lblNationality).Appearance = (AppearanceBase)(object)val12;
		this.lblNationality.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNationality).Name = "lblNationality";
		((ControlBase)this.lblNationality).WrapText = false;
		resources.ApplyResources(this.txtPersonalIDNo, "txtPersonalIDNo");
		((System.Windows.Forms.Control)(object)this.txtPersonalIDNo).Name = "txtPersonalIDNo";
		((System.Windows.Forms.Control)(object)this.txtPersonalIDNo).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPersonalIDNo_KeyPress);
		resources.ApplyResources(this.lblPersonalIDNo, "lblPersonalIDNo");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val13, "appearance45");
		((ControlBase)this.lblPersonalIDNo).Appearance = (AppearanceBase)(object)val13;
		this.lblPersonalIDNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPersonalIDNo).Name = "lblPersonalIDNo";
		((ControlBase)this.lblPersonalIDNo).WrapText = false;
		resources.ApplyResources(this.cboCountry, "cboCountry");
		((System.Windows.Forms.Control)(object)this.cboCountry).Name = "cboCountry";
		resources.ApplyResources(this.lblCountry, "lblCountry");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val14, "appearance46");
		((ControlBase)this.lblCountry).Appearance = (AppearanceBase)(object)val14;
		this.lblCountry.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCountry).Name = "lblCountry";
		((ControlBase)this.lblCountry).WrapText = false;
		resources.ApplyResources(this.cboArea, "cboArea");
		((System.Windows.Forms.Control)(object)this.cboArea).Name = "cboArea";
		resources.ApplyResources(this.lblArea, "lblArea");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val15, "appearance47");
		((ControlBase)this.lblArea).Appearance = (AppearanceBase)(object)val15;
		this.lblArea.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArea).Name = "lblArea";
		((ControlBase)this.lblArea).WrapText = false;
		resources.ApplyResources(this.cboCity, "cboCity");
		((System.Windows.Forms.Control)(object)this.cboCity).Name = "cboCity";
		((TextEditorControlBase)this.cboCity).ValueChanged += new System.EventHandler(cboCity_ValueChanged);
		resources.ApplyResources(this.lblCity, "lblCity");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val16, "appearance48");
		((ControlBase)this.lblCity).Appearance = (AppearanceBase)(object)val16;
		this.lblCity.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCity).Name = "lblCity";
		((ControlBase)this.lblCity).WrapText = false;
		resources.ApplyResources(this.txtAddress, "txtAddress");
		((System.Windows.Forms.Control)(object)this.txtAddress).Name = "txtAddress";
		resources.ApplyResources(this.lblAddress, "lblAddress");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val17, "appearance49");
		((ControlBase)this.lblAddress).Appearance = (AppearanceBase)(object)val17;
		this.lblAddress.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAddress).Name = "lblAddress";
		((ControlBase)this.lblAddress).WrapText = false;
		resources.ApplyResources(this.txtPostalCode, "txtPostalCode");
		((System.Windows.Forms.Control)(object)this.txtPostalCode).Name = "txtPostalCode";
		resources.ApplyResources(this.lblPostalCode, "lblPostalCode");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val18).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val18, "appearance50");
		((ControlBase)this.lblPostalCode).Appearance = (AppearanceBase)(object)val18;
		this.lblPostalCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPostalCode).Name = "lblPostalCode";
		((ControlBase)this.lblPostalCode).WrapText = false;
		resources.ApplyResources(this.txtMobile, "txtMobile");
		((System.Windows.Forms.Control)(object)this.txtMobile).Name = "txtMobile";
		resources.ApplyResources(this.lblMobile, "lblMobile");
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val19, "appearance51");
		((ControlBase)this.lblMobile).Appearance = (AppearanceBase)(object)val19;
		this.lblMobile.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMobile).Name = "lblMobile";
		((ControlBase)this.lblMobile).WrapText = false;
		resources.ApplyResources(this.txtTel, "txtTel");
		((System.Windows.Forms.Control)(object)this.txtTel).Name = "txtTel";
		resources.ApplyResources(this.lblTel, "lblTel");
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val20, "appearance52");
		((ControlBase)this.lblTel).Appearance = (AppearanceBase)(object)val20;
		this.lblTel.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTel).Name = "lblTel";
		((ControlBase)this.lblTel).WrapText = false;
		resources.ApplyResources(this.txtEMail, "txtEMail");
		((System.Windows.Forms.Control)(object)this.txtEMail).Name = "txtEMail";
		resources.ApplyResources(this.lblEMail, "lblEMail");
		((AppearanceBase)val21).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val21).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val21, "appearance53");
		((ControlBase)this.lblEMail).Appearance = (AppearanceBase)(object)val21;
		this.lblEMail.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEMail).Name = "lblEMail";
		((ControlBase)this.lblEMail).WrapText = false;
		resources.ApplyResources(this.cboBloodType, "cboBloodType");
		((System.Windows.Forms.Control)(object)this.cboBloodType).Name = "cboBloodType";
		resources.ApplyResources(this.lblBloodType, "lblBloodType");
		((AppearanceBase)val22).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val22).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val22, "appearance54");
		((ControlBase)this.lblBloodType).Appearance = (AppearanceBase)(object)val22;
		this.lblBloodType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBloodType).Name = "lblBloodType";
		((ControlBase)this.lblBloodType).WrapText = false;
		resources.ApplyResources(this.txtPMI, "txtPMI");
		((System.Windows.Forms.Control)(object)this.txtPMI).Name = "txtPMI";
		((System.Windows.Forms.Control)(object)this.txtPMI).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblPMI, "lblPMI");
		((AppearanceBase)val23).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val23).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val23, "appearance55");
		((ControlBase)this.lblPMI).Appearance = (AppearanceBase)(object)val23;
		this.lblPMI.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPMI).Name = "lblPMI";
		((ControlBase)this.lblPMI).WrapText = false;
		resources.ApplyResources(this.txtWeight, "txtWeight");
		((System.Windows.Forms.Control)(object)this.txtWeight).Name = "txtWeight";
		((System.Windows.Forms.Control)(object)this.txtWeight).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblWeight, "lblWeight");
		((AppearanceBase)val24).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val24).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val24, "appearance56");
		((ControlBase)this.lblWeight).Appearance = (AppearanceBase)(object)val24;
		this.lblWeight.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblWeight).Name = "lblWeight";
		((ControlBase)this.lblWeight).WrapText = false;
		resources.ApplyResources(this.txtHeight, "txtHeight");
		((System.Windows.Forms.Control)(object)this.txtHeight).Name = "txtHeight";
		((System.Windows.Forms.Control)(object)this.txtHeight).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblHeight, "lblHeight");
		((AppearanceBase)val25).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val25).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val25, "appearance57");
		((ControlBase)this.lblHeight).Appearance = (AppearanceBase)(object)val25;
		this.lblHeight.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblHeight).Name = "lblHeight";
		((ControlBase)this.lblHeight).WrapText = false;
		resources.ApplyResources(this.cboDoctor, "cboDoctor");
		((System.Windows.Forms.Control)(object)this.cboDoctor).Name = "cboDoctor";
		resources.ApplyResources(this.lblDoctor, "lblDoctor");
		((AppearanceBase)val26).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val26).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val26, "appearance58");
		((ControlBase)this.lblDoctor).Appearance = (AppearanceBase)(object)val26;
		this.lblDoctor.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDoctor).Name = "lblDoctor";
		((ControlBase)this.lblDoctor).WrapText = false;
		resources.ApplyResources(this.chkClosed, "chkClosed");
		((AppearanceBase)val27).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val27, "appearance59");
		((UltraToggleEditorBase)this.chkClosed).Appearance = (AppearanceBase)(object)val27;
		((System.Windows.Forms.Control)(object)this.chkClosed).Name = "chkClosed";
		((UltraToggleEditorBase)this.chkClosed).CheckedChanged += new System.EventHandler(chkClosed_CheckedChanged);
		resources.ApplyResources(this.dtpClosedDate, "dtpClosedDate");
		((UltraWinEditorMaskedControlBase)this.dtpClosedDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpClosedDate).Name = "dtpClosedDate";
		resources.ApplyResources(this.cboPosition, "cboPosition");
		((System.Windows.Forms.Control)(object)this.cboPosition).Name = "cboPosition";
		resources.ApplyResources(this.lblPosition, "lblPosition");
		((AppearanceBase)val28).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val28).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val28, "appearance60");
		((ControlBase)this.lblPosition).Appearance = (AppearanceBase)(object)val28;
		this.lblPosition.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPosition).Name = "lblPosition";
		((ControlBase)this.lblPosition).WrapText = false;
		resources.ApplyResources(this.cboHusbandPosition, "cboHusbandPosition");
		((System.Windows.Forms.Control)(object)this.cboHusbandPosition).Name = "cboHusbandPosition";
		resources.ApplyResources(this.lblHusbandPosition, "lblHusbandPosition");
		((AppearanceBase)val29).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val29).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val29, "appearance61");
		((ControlBase)this.lblHusbandPosition).Appearance = (AppearanceBase)(object)val29;
		this.lblHusbandPosition.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblHusbandPosition).Name = "lblHusbandPosition";
		((ControlBase)this.lblHusbandPosition).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((AppearanceBase)val30).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val30).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val30, "appearance62");
		((ControlBase)this.lblNotes).Appearance = (AppearanceBase)(object)val30;
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.btnSubAccountSearch, "btnSubAccountSearch");
		((AppearanceBase)val31).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val31, "appearance63");
		((ControlBase)this.btnSubAccountSearch).Appearance = (AppearanceBase)(object)val31;
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).Name = "btnSubAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).Click += new System.EventHandler(btnSubAccountSearch_Click);
		resources.ApplyResources(this.btnPriceTypeSearch, "btnPriceTypeSearch");
		((AppearanceBase)val32).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val32, "appearance64");
		((ControlBase)this.btnPriceTypeSearch).Appearance = (AppearanceBase)(object)val32;
		((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch).Name = "btnPriceTypeSearch";
		((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch).Click += new System.EventHandler(btnPriceTypeSearch_Click);
		resources.ApplyResources(this.cboPriceType, "cboPriceType");
		((System.Windows.Forms.Control)(object)this.cboPriceType).Name = "cboPriceType";
		resources.ApplyResources(this.lblPriceType, "lblPriceType");
		this.lblPriceType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPriceType).Name = "lblPriceType";
		((ControlBase)this.lblPriceType).WrapText = false;
		resources.ApplyResources(this.cboSubAccount, "cboSubAccount");
		((System.Windows.Forms.Control)(object)this.cboSubAccount).Name = "cboSubAccount";
		resources.ApplyResources(this.lblSubAccount, "lblSubAccount");
		this.lblSubAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSubAccount).Name = "lblSubAccount";
		((ControlBase)this.lblSubAccount).WrapText = false;
		resources.ApplyResources(this.txtDiscountPercentage, "txtDiscountPercentage");
		((System.Windows.Forms.Control)(object)this.txtDiscountPercentage).Name = "txtDiscountPercentage";
		((System.Windows.Forms.Control)(object)this.txtDiscountPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblDiscountPercentage, "lblDiscountPercentage");
		((AppearanceBase)val33).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val33).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val33, "appearance65");
		((ControlBase)this.lblDiscountPercentage).Appearance = (AppearanceBase)(object)val33;
		this.lblDiscountPercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscountPercentage).Name = "lblDiscountPercentage";
		((ControlBase)this.lblDiscountPercentage).WrapText = false;
		base.AcceptButton = (System.Windows.Forms.IButtonControl)base.btnAdd;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSubAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboHusbandPosition);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblHusbandPosition);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPosition);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPosition);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpClosedDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkClosed);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboDoctor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDoctor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtHeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblHeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPMI);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPMI);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBloodType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBloodType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEMail);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEMail);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtMobile);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMobile);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPostalCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPostalCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCountry);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCountry);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboArea);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArea);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCity);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCity);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAddress);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAddress);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPersonalIDNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPersonalIDNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboNationality);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNationality);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSocialStatus);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSocialStatus);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboGender);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGender);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpBirthDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBirthDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPatientTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPatientNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPatientNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPatientNameAr);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPatientNameAr);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCopyTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPriveous);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNext);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmPatients";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPatientNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPatientNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPatientNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPatientNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPatientTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBirthDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpBirthDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGender, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboGender, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSocialStatus, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSocialStatus, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNationality, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboNationality, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPersonalIDNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPersonalIDNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAddress, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAddress, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCity, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCity, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArea, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboArea, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCountry, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCountry, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPostalCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPostalCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMobile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtMobile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEMail, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEMail, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBloodType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBloodType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPMI, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPMI, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblHeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtHeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDoctor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboDoctor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkClosed, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpClosedDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPosition, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPosition, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblHusbandPosition, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboHusbandPosition, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSubAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountPercentage, 0);
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPatientNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPatientNameAr).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTitle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBirthDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSocialStatus).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboGender).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboNationality).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPersonalIDNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPostalCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobile).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBloodType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPMI).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtWeight).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtHeight).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDoctor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkClosed).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpClosedDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPosition).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboHusbandPosition).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountPercentage).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
