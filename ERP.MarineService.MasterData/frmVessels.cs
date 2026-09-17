using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.MarineService;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.MasterData;

public class frmVessels : frmHeaderDetails
{
	private DataTable dtVesselType;

	private DataTable dtCertificationTypes;

	private DataTable dtRegisterationPlaces;

	private DataTable dtNationalities;

	private DataTable dtOwners;

	private DataTable dtCharters;

	private DataTable dtVesselsClassificationOffices;

	private ValueList vlCertificationTypes = new ValueList();

	private IContainer components = null;

	private UltraTextEditor txtVesselNameEn;

	private UltraLabel lblVesselNameEn;

	private UltraTextEditor txtVesselNameAr;

	private UltraLabel lblVesselNameAr;

	private UltraLabel lblVesselType;

	private UltraComboEditor cboVesselType;

	private UltraLabel lblNationality;

	private UltraComboEditor cboNationality;

	private UltraLabel lblRegisterationPlace;

	private UltraComboEditor cboRegisterationPlace;

	private UltraTextEditor txtRegisterationNo;

	private UltraLabel lblRegisterationNo;

	private UltraLabel lblBuiltPlace;

	private UltraTextEditor txtBuiltPlace;

	private UltraComboEditor cboBuiltYear;

	private UltraLabel lblBuiltYear;

	private UltraLabel lblIMO;

	private UltraTextEditor txtIMO;

	private UltraLabel lblCallSign;

	private UltraTextEditor txtCallSign;

	private UltraTextEditor txtFuelTankCapacity;

	private UltraLabel lblFuelTankCapacity;

	private UltraLabel lblLOA;

	private UltraTextEditor txtLOA;

	private UltraLabel lblBeam;

	private UltraTextEditor txtBeam;

	private UltraLabel lblSDWT;

	private UltraTextEditor txtSDWT;

	private UltraLabel lblGRT;

	private UltraTextEditor txtGRT;

	private UltraTextEditor txtNotes;

	private UltraLabel lblNotes;

	private UltraLabel lblNRT;

	private UltraTextEditor txtNRT;

	private UltraLabel lblOwner;

	private UltraComboEditor cboOwners;

	private UltraLabel lblCharter;

	private UltraComboEditor cboCharterers;

	private UltraLabel lblm;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private UltraLabel lblDepth;

	private UltraTextEditor txtDepth;

	private UltraLabel ultraLabel6;

	private UltraComboEditor cboVesselClassificationOffice;

	private UltraLabel lblVesselClassificationOffice;

	private UltraCheckEditor chkHasbowThruster;

	private UltraTextEditor txtBowThrusterHP;

	private UltraLabel ultraLabel5;

	private UltraTextEditor txtSternThrusterHP;

	private UltraLabel ultraLabel7;

	private UltraCheckEditor chkHasSternThruster;

	private UltraTextEditor txtISPSCertificateOnBoardNo;

	private UltraCheckEditor chkHasISPsCertificateOnBoard;

	private UltraLabel lblISPSExpirationDate;

	private UltraDateTimeEditor dtpISPSExpirationDate;

	private UltraLabel lblPresentSecurityLevel;

	private UltraComboEditor cboPresentSecurityLevel;

	private UltraCheckEditor chkContinousSynopsisRecord;

	private UltraCheckEditor chkFullInternationalShipSecurityCertificate;

	public frmVessels()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		TableName = "MS_Vessels";
		IDCol = "VesselID";
		NoCol = "VesselCode";
		DateCol = "GetDate()";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtVesselType = VesselsTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboVesselType, dtVesselType, "VesselTypeID", "VesselTypeName");
		dtVesselsClassificationOffices = VesselsClassificationOffices.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboVesselClassificationOffice, dtVesselsClassificationOffices, "VesselClassificationOfficeID", "VesselClassificationOfficeName");
		dtRegisterationPlaces = RegisterationPlaces.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboRegisterationPlace, dtRegisterationPlaces, "RegisterationPlaceID", "RegisterationPlaceName");
		dtNationalities = Nationalities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboNationality, dtNationalities, "NationalityID", "NationalityName");
		dtOwners = Owners.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboOwners, dtOwners, "SubAccountID", "OwnerName");
		dtCharters = Charters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCharterers, dtCharters, "SubAccountID", "CharterName");
		cboPresentSecurityLevel.Items.Add((object)"1", "1");
		cboPresentSecurityLevel.Items.Add((object)"2", "2");
		cboPresentSecurityLevel.Items.Add((object)"3", "3");
		dtCertificationTypes = CertificationsTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlCertificationTypes.ValueListItems.Clear();
		for (int i = 0; i < dtCertificationTypes.Rows.Count; i++)
		{
			vlCertificationTypes.ValueListItems.Add(dtCertificationTypes.Rows[i]["CertificationTypeID"], dtCertificationTypes.Rows[i]["CertificationTypeName"].ToString());
		}
		dtDetails = VesselsCertifications.SelectByVesselID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		for (int num = DateTime.Now.Year; num >= 1950; num--)
		{
			cboBuiltYear.Items.Add((object)num.ToString(), num.ToString());
		}
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VesselCertificationID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CertificationTypeID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CertificationTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "اسم الشهادة" : "Certificate Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CertificationTypeID"].ValueList = (IValueList)(object)vlCertificationTypes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CertificationTypeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidToDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidToDate"].Header).Caption = (GlobalVariables.IsArabic ? "ساري حتي" : "Valid To");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidToDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = Vessels.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
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
			((Control)(object)txtCode).Text = drMaster["VesselCode"].ToString();
			((TextEditorControlBase)cboVesselType).Value = drMaster["VesselTypeID"];
			((TextEditorControlBase)cboBuiltYear).Value = drMaster["BuiltYear"];
			((TextEditorControlBase)cboNationality).Value = drMaster["NationalityID"];
			((TextEditorControlBase)cboRegisterationPlace).Value = drMaster["RegisterationPlaceID"];
			((TextEditorControlBase)cboOwners).Value = drMaster["OwnerSubAccountID"];
			((TextEditorControlBase)cboCharterers).Value = drMaster["ClientSubAccountID"];
			((Control)(object)txtVesselNameAr).Text = drMaster["VesselNameAr"].ToString();
			((Control)(object)txtVesselNameEn).Text = drMaster["VesselNameEn"].ToString();
			((Control)(object)txtBeam).Text = drMaster["Beam"].ToString();
			((Control)(object)txtBuiltPlace).Text = drMaster["BuiltPlace"].ToString();
			((Control)(object)txtCallSign).Text = drMaster["CallSign"].ToString();
			((TextEditorControlBase)cboVesselClassificationOffice).Value = drMaster["VesselClassificationOfficeID"];
			((UltraToggleEditorBase)chkHasbowThruster).Checked = bool.Parse(drMaster["HasBowThruster"].ToString());
			((Control)(object)txtBowThrusterHP).Text = drMaster["BowThrusterHP"].ToString();
			((UltraToggleEditorBase)chkHasSternThruster).Checked = bool.Parse(drMaster["HasSternThruster"].ToString());
			((Control)(object)txtSternThrusterHP).Text = drMaster["SternThrusterHP"].ToString();
			((UltraToggleEditorBase)chkHasISPsCertificateOnBoard).Checked = bool.Parse(drMaster["HasISPSCertificateOnBoard"].ToString());
			((Control)(object)txtISPSCertificateOnBoardNo).Text = drMaster["CertificateNo"].ToString();
			dtpISPSExpirationDate.Value = drMaster["CertificateExpirationDate"];
			((TextEditorControlBase)cboPresentSecurityLevel).Value = drMaster["PresentSecurityLevelOnBoardLevel"];
			((UltraToggleEditorBase)chkContinousSynopsisRecord).Checked = bool.Parse(drMaster["HasContinousSynopsisRecord"].ToString());
			((UltraToggleEditorBase)chkFullInternationalShipSecurityCertificate).Checked = bool.Parse(drMaster["IsFullInternationalShipSecurityCertificate"].ToString());
			((Control)(object)txtFuelTankCapacity).Text = drMaster["FuelTankCapacity"].ToString();
			((Control)(object)txtGRT).Text = drMaster["GRT"].ToString();
			((Control)(object)txtIMO).Text = drMaster["IMO"].ToString();
			((Control)(object)txtLOA).Text = drMaster["LOA"].ToString();
			((Control)(object)txtDepth).Text = drMaster["Depth"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtNRT).Text = drMaster["NRT"].ToString();
			((Control)(object)txtRegisterationNo).Text = drMaster["RegisterationNo"].ToString();
			((Control)(object)txtSDWT).Text = drMaster["SDWT"].ToString();
			dtDetails = VesselsCertifications.SelectByVesselID(drMaster["VesselID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			((UltraGridBase)ULGData).DataSource = dtDetails;
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
		((EditorButtonControlBase)cboVesselType).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBuiltYear).ReadOnly = NavMode;
		((EditorButtonControlBase)cboNationality).ReadOnly = NavMode;
		((EditorButtonControlBase)cboRegisterationPlace).ReadOnly = NavMode;
		((EditorButtonControlBase)cboOwners).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCharterers).ReadOnly = NavMode;
		((EditorButtonControlBase)txtVesselNameAr).ReadOnly = NavMode;
		((EditorButtonControlBase)txtVesselNameEn).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBeam).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBuiltPlace).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCallSign).ReadOnly = NavMode;
		((EditorButtonControlBase)cboVesselClassificationOffice).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBowThrusterHP).ReadOnly = NavMode;
		((Control)(object)chkHasbowThruster).Enabled = !NavMode;
		((EditorButtonControlBase)txtSternThrusterHP).ReadOnly = NavMode;
		((Control)(object)chkHasSternThruster).Enabled = !NavMode;
		((EditorButtonControlBase)txtISPSCertificateOnBoardNo).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpISPSExpirationDate).ReadOnly = NavMode;
		((Control)(object)chkHasISPsCertificateOnBoard).Enabled = !NavMode;
		((EditorButtonControlBase)cboPresentSecurityLevel).ReadOnly = NavMode;
		((Control)(object)chkContinousSynopsisRecord).Enabled = !NavMode;
		((Control)(object)chkFullInternationalShipSecurityCertificate).Enabled = !NavMode;
		((EditorButtonControlBase)txtFuelTankCapacity).ReadOnly = NavMode;
		((EditorButtonControlBase)txtGRT).ReadOnly = NavMode;
		((EditorButtonControlBase)txtIMO).ReadOnly = NavMode;
		((EditorButtonControlBase)txtLOA).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDepth).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNRT).ReadOnly = NavMode;
		((EditorButtonControlBase)txtRegisterationNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSDWT).ReadOnly = NavMode;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		((Control)(object)txtCode).Text = (Adding ? Vessels.GetCode(IsFromServer: true) : "");
		cboVesselType.SelectedIndex = -1;
		cboBuiltYear.SelectedIndex = -1;
		cboRegisterationPlace.SelectedIndex = -1;
		cboNationality.SelectedIndex = -1;
		cboOwners.SelectedIndex = -1;
		cboCharterers.SelectedIndex = -1;
		((TextEditorControlBase)txtVesselNameAr).Clear();
		((TextEditorControlBase)txtVesselNameEn).Clear();
		((TextEditorControlBase)txtBeam).Clear();
		((TextEditorControlBase)txtDepth).Clear();
		((TextEditorControlBase)txtBuiltPlace).Clear();
		((TextEditorControlBase)txtCallSign).Clear();
		cboVesselClassificationOffice.SelectedIndex = -1;
		((UltraToggleEditorBase)chkHasbowThruster).Checked = false;
		((TextEditorControlBase)txtBowThrusterHP).Clear();
		((UltraToggleEditorBase)chkHasSternThruster).Checked = false;
		((TextEditorControlBase)txtSternThrusterHP).Clear();
		((UltraToggleEditorBase)chkHasISPsCertificateOnBoard).Checked = false;
		((TextEditorControlBase)txtISPSCertificateOnBoardNo).Clear();
		dtpISPSExpirationDate.Value = DBNull.Value;
		cboPresentSecurityLevel.SelectedIndex = -1;
		((UltraToggleEditorBase)chkContinousSynopsisRecord).Checked = false;
		((UltraToggleEditorBase)chkFullInternationalShipSecurityCertificate).Checked = false;
		((TextEditorControlBase)txtFuelTankCapacity).Clear();
		((TextEditorControlBase)txtGRT).Clear();
		((TextEditorControlBase)txtIMO).Clear();
		((TextEditorControlBase)txtLOA).Clear();
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)txtNRT).Clear();
		((TextEditorControlBase)txtRegisterationNo).Clear();
		((TextEditorControlBase)txtSDWT).Clear();
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال الكود" : "Please Enter The Code");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (cboVesselType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار نوع السفينه" : "Please Select Vessel Type");
			((TextEditorControlBase)cboVesselType).Focus();
			cboVesselType.DropDown();
			return false;
		}
		if (cboOwners.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار المالك" : "Please Select Owner");
			((TextEditorControlBase)cboOwners).Focus();
			cboOwners.DropDown();
			return false;
		}
		if (((Control)(object)txtVesselNameAr).Text == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال إسم السفينه" : "Please  Enter Vessel Name");
			((TextEditorControlBase)txtVesselNameAr).Focus();
			return false;
		}
		if (((Control)(object)txtRegisterationNo).Text == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم تسجيل السفينه" : "Please Enter Registration No.");
			((TextEditorControlBase)txtRegisterationNo).Focus();
			return false;
		}
		if (Main.CheckForValue("MS_Vessels", "VesselCode", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["VesselCode"].ToString(), IsFromServer: true) > 0)
		{
			string code = Vessels.GetCode(IsFromServer: true);
			GlobalVariables.QuestionMB.Show("رقم هذه السفينة متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Vessel code Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = code;
		}
		if (Main.CheckForValue("MS_Vessels", "VesselNameAr", ((Control)(object)txtVesselNameAr).Text, Updating ? drMaster["VesselNameAr"].ToString() : "", IsFromServer: true) > 0)
		{
			GlobalVariables.InformationMB.Show(" إسم السفينة بالعربية متواجد من قبل ", "Vessel Arabic Name Already Exist");
			((TextEditorControlBase)txtVesselNameAr).Focus();
			return false;
		}
		if (Main.CheckForValue("MS_Vessels", "RegisterationNo", ((Control)(object)txtRegisterationNo).Text, Updating ? drMaster["RegisterationNo"].ToString() : "", IsFromServer: true) > 0)
		{
			GlobalVariables.InformationMB.Show("رقم التسجيل متواجد من قبل", "Registration No. Already Exist");
			((TextEditorControlBase)txtRegisterationNo).Focus();
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGData).Rows[i].Cells["CertificationTypeID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["CertificationTypeID"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "إسم الشهادة متواجد من قبل" : "Certification Type Already Exist");
					return false;
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
			int num = Vessels.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtVesselNameAr).Text, (((Control)(object)txtVesselNameEn).Text == "") ? "Null" : ((Control)(object)txtVesselNameEn).Text, (cboNationality.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboNationality).Value.ToString(), (cboRegisterationPlace.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboRegisterationPlace).Value.ToString(), (((Control)(object)txtRegisterationNo).Text == "") ? "Null" : ((Control)(object)txtRegisterationNo).Text, (cboVesselType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboVesselType).Value.ToString(), (((Control)(object)txtBuiltPlace).Text == "") ? "Null" : ((Control)(object)txtBuiltPlace).Text, (cboBuiltYear.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBuiltYear).Value.ToString(), (((Control)(object)txtIMO).Text == "") ? "Null" : ((Control)(object)txtIMO).Text, (((Control)(object)txtCallSign).Text == "") ? "Null" : ((Control)(object)txtCallSign).Text, (cboVesselClassificationOffice.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboVesselClassificationOffice).Value.ToString(), ((UltraToggleEditorBase)chkHasbowThruster).Checked ? "1" : "0", (((Control)(object)txtBowThrusterHP).Text == "") ? "Null" : ((Control)(object)txtBowThrusterHP).Text, ((UltraToggleEditorBase)chkHasSternThruster).Checked ? "1" : "0", (((Control)(object)txtSternThrusterHP).Text == "") ? "Null" : ((Control)(object)txtSternThrusterHP).Text, ((UltraToggleEditorBase)chkHasISPsCertificateOnBoard).Checked ? "1" : "0", (((Control)(object)txtISPSCertificateOnBoardNo).Text == "") ? "Null" : ((Control)(object)txtISPSCertificateOnBoardNo).Text, (dtpISPSExpirationDate.Value == null) ? "Null" : dtpISPSExpirationDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboPresentSecurityLevel.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPresentSecurityLevel).Value.ToString(), ((UltraToggleEditorBase)chkContinousSynopsisRecord).Checked ? "1" : "0", ((UltraToggleEditorBase)chkFullInternationalShipSecurityCertificate).Checked ? "1" : "0", (((Control)(object)txtFuelTankCapacity).Text == "") ? "0" : ((Control)(object)txtFuelTankCapacity).Text, (((Control)(object)txtLOA).Text == "") ? "0" : ((Control)(object)txtLOA).Text, (((Control)(object)txtBeam).Text == "") ? "0" : ((Control)(object)txtBeam).Text, (((Control)(object)txtDepth).Text == "") ? "0" : ((Control)(object)txtDepth).Text, (((Control)(object)txtSDWT).Text == "") ? "0" : ((Control)(object)txtSDWT).Text, (((Control)(object)txtGRT).Text == "") ? "0" : ((Control)(object)txtGRT).Text, (((Control)(object)txtNRT).Text == "") ? "0" : ((Control)(object)txtNRT).Text, (cboOwners.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOwners).Value.ToString(), (cboCharterers.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCharterers).Value.ToString(), (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["VesselCertificationID"].Value = "-1";
				((UltraGridBase)ULGData).Rows[i].Cells["VesselID"].Value = num.ToString();
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				VesselsCertifications.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
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
			int num = Vessels.Insert_Update(drMaster["VesselID"].ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtVesselNameAr).Text, (((Control)(object)txtVesselNameEn).Text == "") ? "Null" : ((Control)(object)txtVesselNameEn).Text, (cboNationality.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboNationality).Value.ToString(), (cboRegisterationPlace.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboRegisterationPlace).Value.ToString(), (((Control)(object)txtRegisterationNo).Text == "") ? "Null" : ((Control)(object)txtRegisterationNo).Text, (cboVesselType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboVesselType).Value.ToString(), (((Control)(object)txtBuiltPlace).Text == "") ? "Null" : ((Control)(object)txtBuiltPlace).Text, (cboBuiltYear.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBuiltYear).Value.ToString(), (((Control)(object)txtIMO).Text == "") ? "Null" : ((Control)(object)txtIMO).Text, (((Control)(object)txtCallSign).Text == "") ? "Null" : ((Control)(object)txtCallSign).Text, (cboVesselClassificationOffice.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboVesselClassificationOffice).Value.ToString(), ((UltraToggleEditorBase)chkHasbowThruster).Checked ? "1" : "0", (((Control)(object)txtBowThrusterHP).Text == "") ? "Null" : ((Control)(object)txtBowThrusterHP).Text, ((UltraToggleEditorBase)chkHasSternThruster).Checked ? "1" : "0", (((Control)(object)txtSternThrusterHP).Text == "") ? "Null" : ((Control)(object)txtSternThrusterHP).Text, ((UltraToggleEditorBase)chkHasISPsCertificateOnBoard).Checked ? "1" : "0", (((Control)(object)txtISPSCertificateOnBoardNo).Text == "") ? "Null" : ((Control)(object)txtISPSCertificateOnBoardNo).Text, (dtpISPSExpirationDate.Value == null) ? "Null" : dtpISPSExpirationDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboPresentSecurityLevel.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPresentSecurityLevel).Value.ToString(), ((UltraToggleEditorBase)chkContinousSynopsisRecord).Checked ? "1" : "0", ((UltraToggleEditorBase)chkFullInternationalShipSecurityCertificate).Checked ? "1" : "0", (((Control)(object)txtFuelTankCapacity).Text == "") ? "0" : ((Control)(object)txtFuelTankCapacity).Text, (((Control)(object)txtLOA).Text == "") ? "0" : ((Control)(object)txtLOA).Text, (((Control)(object)txtBeam).Text == "") ? "0" : ((Control)(object)txtBeam).Text, (((Control)(object)txtDepth).Text == "") ? "0" : ((Control)(object)txtDepth).Text, (((Control)(object)txtSDWT).Text == "") ? "0" : ((Control)(object)txtSDWT).Text, (((Control)(object)txtGRT).Text == "") ? "0" : ((Control)(object)txtGRT).Text, (((Control)(object)txtNRT).Text == "") ? "0" : ((Control)(object)txtNRT).Text, (cboOwners.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOwners).Value.ToString(), (cboCharterers.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCharterers).Value.ToString(), (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["VesselCertificationID"].Value.ToString() + ",";
				((UltraGridBase)ULGData).Rows[i].Cells["VesselID"].Value = num.ToString();
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			Main.SyncDeleteForUpdate("MS_VesselsCertifications", "VesselID", num.ToString(), "VesselCertificationID", text, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				VesselsCertifications.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
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
			VesselsCertifications.DeleteByVesselID(drMaster["VesselID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			Vessels.Delete(drMaster["VesselID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnRefreshDataClick()
	{
		dtVesselsClassificationOffices = VesselsClassificationOffices.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboVesselClassificationOffice, dtVesselsClassificationOffices, "VesselClassificationOfficeID", "VesselClassificationOfficeName");
		dtVesselType = VesselsTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboVesselType, dtVesselType, "VesselTypeID", "VesselTypeName");
		dtRegisterationPlaces = RegisterationPlaces.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboRegisterationPlace, dtRegisterationPlaces, "RegisterationPlaceID", "RegisterationPlaceName");
		dtNationalities = Nationalities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboNationality, dtNationalities, "NationalityID", "NationalityName");
		dtOwners = Owners.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboOwners, dtOwners, "SubAccountID", "OwnerName");
		dtCharters = Charters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCharterers, dtCharters, "SubAccountID", "CharterName");
		cboPresentSecurityLevel.Items.Add((object)"1", "1");
		cboPresentSecurityLevel.Items.Add((object)"2", "2");
		cboPresentSecurityLevel.Items.Add((object)"3", "3");
	}

	public override void btnPrintClick()
	{
		if (!(RowID != ""))
		{
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.VesselsSearchReport(IsFromServer: true);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["VesselID"].ToString();
			FillData();
		}
	}

	private void txtBeam_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtLOA_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtFuelTankCapacity_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtGRT_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtNRT_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtSDWT_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtDepth_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void chkHasbowThruster_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)txtBowThrusterHP).Enabled = ((UltraToggleEditorBase)chkHasbowThruster).Checked;
	}

	private void chkHasSternThruster_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)txtSternThrusterHP).Enabled = ((UltraToggleEditorBase)chkHasSternThruster).Checked;
	}

	private void chkHasISPsCertificateOnBoard_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)txtISPSCertificateOnBoardNo).Enabled = ((UltraToggleEditorBase)chkHasISPsCertificateOnBoard).Checked;
		((Control)(object)dtpISPSExpirationDate).Enabled = ((UltraToggleEditorBase)chkHasISPsCertificateOnBoard).Checked;
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
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected O, but got Unknown
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected O, but got Unknown
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Expected O, but got Unknown
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Expected O, but got Unknown
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Expected O, but got Unknown
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Expected O, but got Unknown
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Expected O, but got Unknown
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Expected O, but got Unknown
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Expected O, but got Unknown
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Expected O, but got Unknown
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Expected O, but got Unknown
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Expected O, but got Unknown
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Expected O, but got Unknown
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Expected O, but got Unknown
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Expected O, but got Unknown
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Expected O, but got Unknown
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Expected O, but got Unknown
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Expected O, but got Unknown
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Expected O, but got Unknown
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Expected O, but got Unknown
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Expected O, but got Unknown
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Expected O, but got Unknown
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Expected O, but got Unknown
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Expected O, but got Unknown
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Expected O, but got Unknown
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Expected O, but got Unknown
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Expected O, but got Unknown
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Expected O, but got Unknown
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Expected O, but got Unknown
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Expected O, but got Unknown
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Expected O, but got Unknown
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Expected O, but got Unknown
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Expected O, but got Unknown
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Expected O, but got Unknown
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Expected O, but got Unknown
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Expected O, but got Unknown
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Expected O, but got Unknown
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Expected O, but got Unknown
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Expected O, but got Unknown
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Expected O, but got Unknown
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Expected O, but got Unknown
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Expected O, but got Unknown
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.MasterData.frmVessels));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		this.txtVesselNameEn = new UltraTextEditor();
		this.lblVesselNameEn = new UltraLabel();
		this.txtVesselNameAr = new UltraTextEditor();
		this.lblVesselNameAr = new UltraLabel();
		this.lblVesselType = new UltraLabel();
		this.cboVesselType = new UltraComboEditor();
		this.lblNationality = new UltraLabel();
		this.cboNationality = new UltraComboEditor();
		this.lblRegisterationPlace = new UltraLabel();
		this.cboRegisterationPlace = new UltraComboEditor();
		this.txtRegisterationNo = new UltraTextEditor();
		this.lblRegisterationNo = new UltraLabel();
		this.lblBuiltPlace = new UltraLabel();
		this.txtBuiltPlace = new UltraTextEditor();
		this.cboBuiltYear = new UltraComboEditor();
		this.lblBuiltYear = new UltraLabel();
		this.lblIMO = new UltraLabel();
		this.txtIMO = new UltraTextEditor();
		this.lblCallSign = new UltraLabel();
		this.txtCallSign = new UltraTextEditor();
		this.txtFuelTankCapacity = new UltraTextEditor();
		this.lblFuelTankCapacity = new UltraLabel();
		this.lblLOA = new UltraLabel();
		this.txtLOA = new UltraTextEditor();
		this.lblBeam = new UltraLabel();
		this.txtBeam = new UltraTextEditor();
		this.lblSDWT = new UltraLabel();
		this.txtSDWT = new UltraTextEditor();
		this.lblGRT = new UltraLabel();
		this.txtGRT = new UltraTextEditor();
		this.txtNotes = new UltraTextEditor();
		this.lblNotes = new UltraLabel();
		this.lblNRT = new UltraLabel();
		this.txtNRT = new UltraTextEditor();
		this.lblOwner = new UltraLabel();
		this.cboOwners = new UltraComboEditor();
		this.lblCharter = new UltraLabel();
		this.cboCharterers = new UltraComboEditor();
		this.lblm = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.ultraLabel3 = new UltraLabel();
		this.ultraLabel4 = new UltraLabel();
		this.lblDepth = new UltraLabel();
		this.txtDepth = new UltraTextEditor();
		this.ultraLabel6 = new UltraLabel();
		this.cboVesselClassificationOffice = new UltraComboEditor();
		this.lblVesselClassificationOffice = new UltraLabel();
		this.chkHasbowThruster = new UltraCheckEditor();
		this.txtBowThrusterHP = new UltraTextEditor();
		this.ultraLabel5 = new UltraLabel();
		this.txtSternThrusterHP = new UltraTextEditor();
		this.ultraLabel7 = new UltraLabel();
		this.chkHasSternThruster = new UltraCheckEditor();
		this.txtISPSCertificateOnBoardNo = new UltraTextEditor();
		this.chkHasISPsCertificateOnBoard = new UltraCheckEditor();
		this.lblISPSExpirationDate = new UltraLabel();
		this.dtpISPSExpirationDate = new UltraDateTimeEditor();
		this.lblPresentSecurityLevel = new UltraLabel();
		this.cboPresentSecurityLevel = new UltraComboEditor();
		this.chkContinousSynopsisRecord = new UltraCheckEditor();
		this.chkFullInternationalShipSecurityCertificate = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVesselNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVesselNameAr).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVesselType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboNationality).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboRegisterationPlace).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRegisterationNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBuiltPlace).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBuiltYear).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtIMO).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCallSign).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFuelTankCapacity).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLOA).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBeam).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSDWT).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGRT).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNRT).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboOwners).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCharterers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDepth).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVesselClassificationOffice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkHasbowThruster).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBowThrusterHP).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSternThrusterHP).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkHasSternThruster).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtISPSCertificateOnBoardNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkHasISPsCertificateOnBoard).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpISPSExpirationDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPresentSecurityLevel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkContinousSynopsisRecord).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkFullInternationalShipSecurityCertificate).BeginInit();
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
		resources.ApplyResources(this.txtVesselNameEn, "txtVesselNameEn");
		((System.Windows.Forms.Control)(object)this.txtVesselNameEn).Name = "txtVesselNameEn";
		resources.ApplyResources(this.lblVesselNameEn, "lblVesselNameEn");
		this.lblVesselNameEn.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVesselNameEn).Name = "lblVesselNameEn";
		((ControlBase)this.lblVesselNameEn).WrapText = false;
		resources.ApplyResources(this.txtVesselNameAr, "txtVesselNameAr");
		((System.Windows.Forms.Control)(object)this.txtVesselNameAr).Name = "txtVesselNameAr";
		resources.ApplyResources(this.lblVesselNameAr, "lblVesselNameAr");
		this.lblVesselNameAr.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVesselNameAr).Name = "lblVesselNameAr";
		((ControlBase)this.lblVesselNameAr).WrapText = false;
		resources.ApplyResources(this.lblVesselType, "lblVesselType");
		this.lblVesselType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVesselType).Name = "lblVesselType";
		((ControlBase)this.lblVesselType).WrapText = false;
		resources.ApplyResources(this.cboVesselType, "cboVesselType");
		((TextEditorControlBase)this.cboVesselType).AlwaysInEditMode = true;
		this.cboVesselType.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboVesselType).Name = "cboVesselType";
		resources.ApplyResources(this.lblNationality, "lblNationality");
		this.lblNationality.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNationality).Name = "lblNationality";
		((ControlBase)this.lblNationality).WrapText = false;
		resources.ApplyResources(this.cboNationality, "cboNationality");
		((TextEditorControlBase)this.cboNationality).AlwaysInEditMode = true;
		this.cboNationality.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboNationality).Name = "cboNationality";
		resources.ApplyResources(this.lblRegisterationPlace, "lblRegisterationPlace");
		this.lblRegisterationPlace.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRegisterationPlace).Name = "lblRegisterationPlace";
		((ControlBase)this.lblRegisterationPlace).WrapText = false;
		resources.ApplyResources(this.cboRegisterationPlace, "cboRegisterationPlace");
		((TextEditorControlBase)this.cboRegisterationPlace).AlwaysInEditMode = true;
		this.cboRegisterationPlace.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboRegisterationPlace).Name = "cboRegisterationPlace";
		resources.ApplyResources(this.txtRegisterationNo, "txtRegisterationNo");
		((System.Windows.Forms.Control)(object)this.txtRegisterationNo).Name = "txtRegisterationNo";
		resources.ApplyResources(this.lblRegisterationNo, "lblRegisterationNo");
		this.lblRegisterationNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRegisterationNo).Name = "lblRegisterationNo";
		((ControlBase)this.lblRegisterationNo).WrapText = false;
		resources.ApplyResources(this.lblBuiltPlace, "lblBuiltPlace");
		this.lblBuiltPlace.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBuiltPlace).Name = "lblBuiltPlace";
		((ControlBase)this.lblBuiltPlace).WrapText = false;
		resources.ApplyResources(this.txtBuiltPlace, "txtBuiltPlace");
		((System.Windows.Forms.Control)(object)this.txtBuiltPlace).Name = "txtBuiltPlace";
		resources.ApplyResources(this.cboBuiltYear, "cboBuiltYear");
		((System.Windows.Forms.Control)(object)this.cboBuiltYear).Name = "cboBuiltYear";
		resources.ApplyResources(this.lblBuiltYear, "lblBuiltYear");
		this.lblBuiltYear.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBuiltYear).Name = "lblBuiltYear";
		((ControlBase)this.lblBuiltYear).WrapText = false;
		resources.ApplyResources(this.lblIMO, "lblIMO");
		this.lblIMO.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblIMO).Name = "lblIMO";
		((ControlBase)this.lblIMO).WrapText = false;
		resources.ApplyResources(this.txtIMO, "txtIMO");
		((System.Windows.Forms.Control)(object)this.txtIMO).Name = "txtIMO";
		resources.ApplyResources(this.lblCallSign, "lblCallSign");
		this.lblCallSign.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCallSign).Name = "lblCallSign";
		((ControlBase)this.lblCallSign).WrapText = false;
		resources.ApplyResources(this.txtCallSign, "txtCallSign");
		((System.Windows.Forms.Control)(object)this.txtCallSign).Name = "txtCallSign";
		resources.ApplyResources(this.txtFuelTankCapacity, "txtFuelTankCapacity");
		((System.Windows.Forms.Control)(object)this.txtFuelTankCapacity).Name = "txtFuelTankCapacity";
		((System.Windows.Forms.Control)(object)this.txtFuelTankCapacity).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtFuelTankCapacity_KeyPress);
		resources.ApplyResources(this.lblFuelTankCapacity, "lblFuelTankCapacity");
		this.lblFuelTankCapacity.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFuelTankCapacity).Name = "lblFuelTankCapacity";
		((ControlBase)this.lblFuelTankCapacity).WrapText = false;
		resources.ApplyResources(this.lblLOA, "lblLOA");
		this.lblLOA.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLOA).Name = "lblLOA";
		((ControlBase)this.lblLOA).WrapText = false;
		resources.ApplyResources(this.txtLOA, "txtLOA");
		((System.Windows.Forms.Control)(object)this.txtLOA).Name = "txtLOA";
		((System.Windows.Forms.Control)(object)this.txtLOA).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtLOA_KeyPress);
		resources.ApplyResources(this.lblBeam, "lblBeam");
		this.lblBeam.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBeam).Name = "lblBeam";
		((ControlBase)this.lblBeam).WrapText = false;
		resources.ApplyResources(this.txtBeam, "txtBeam");
		((System.Windows.Forms.Control)(object)this.txtBeam).Name = "txtBeam";
		((System.Windows.Forms.Control)(object)this.txtBeam).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtBeam_KeyPress);
		resources.ApplyResources(this.lblSDWT, "lblSDWT");
		this.lblSDWT.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSDWT).Name = "lblSDWT";
		((ControlBase)this.lblSDWT).WrapText = false;
		resources.ApplyResources(this.txtSDWT, "txtSDWT");
		((System.Windows.Forms.Control)(object)this.txtSDWT).Name = "txtSDWT";
		((System.Windows.Forms.Control)(object)this.txtSDWT).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSDWT_KeyPress);
		resources.ApplyResources(this.lblGRT, "lblGRT");
		this.lblGRT.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGRT).Name = "lblGRT";
		((ControlBase)this.lblGRT).WrapText = false;
		resources.ApplyResources(this.txtGRT, "txtGRT");
		((System.Windows.Forms.Control)(object)this.txtGRT).Name = "txtGRT";
		((System.Windows.Forms.Control)(object)this.txtGRT).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtGRT_KeyPress);
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.lblNRT, "lblNRT");
		this.lblNRT.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNRT).Name = "lblNRT";
		((ControlBase)this.lblNRT).WrapText = false;
		resources.ApplyResources(this.txtNRT, "txtNRT");
		((System.Windows.Forms.Control)(object)this.txtNRT).Name = "txtNRT";
		((System.Windows.Forms.Control)(object)this.txtNRT).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNRT_KeyPress);
		resources.ApplyResources(this.lblOwner, "lblOwner");
		this.lblOwner.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOwner).Name = "lblOwner";
		((ControlBase)this.lblOwner).WrapText = false;
		resources.ApplyResources(this.cboOwners, "cboOwners");
		((TextEditorControlBase)this.cboOwners).AlwaysInEditMode = true;
		this.cboOwners.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboOwners).Name = "cboOwners";
		resources.ApplyResources(this.lblCharter, "lblCharter");
		this.lblCharter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCharter).Name = "lblCharter";
		((ControlBase)this.lblCharter).WrapText = false;
		resources.ApplyResources(this.cboCharterers, "cboCharterers");
		((TextEditorControlBase)this.cboCharterers).AlwaysInEditMode = true;
		this.cboCharterers.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCharterers).Name = "cboCharterers";
		resources.ApplyResources(this.lblm, "lblm");
		this.lblm.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblm).Name = "lblm";
		((ControlBase)this.lblm).WrapText = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		this.ultraLabel4.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		((ControlBase)this.ultraLabel4).WrapText = false;
		resources.ApplyResources(this.lblDepth, "lblDepth");
		this.lblDepth.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDepth).Name = "lblDepth";
		((ControlBase)this.lblDepth).WrapText = false;
		resources.ApplyResources(this.txtDepth, "txtDepth");
		((System.Windows.Forms.Control)(object)this.txtDepth).Name = "txtDepth";
		((System.Windows.Forms.Control)(object)this.txtDepth).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDepth_KeyPress);
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		this.ultraLabel6.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		((ControlBase)this.ultraLabel6).WrapText = false;
		resources.ApplyResources(this.cboVesselClassificationOffice, "cboVesselClassificationOffice");
		((System.Windows.Forms.Control)(object)this.cboVesselClassificationOffice).Name = "cboVesselClassificationOffice";
		resources.ApplyResources(this.lblVesselClassificationOffice, "lblVesselClassificationOffice");
		this.lblVesselClassificationOffice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVesselClassificationOffice).Name = "lblVesselClassificationOffice";
		((ControlBase)this.lblVesselClassificationOffice).WrapText = false;
		resources.ApplyResources(this.chkHasbowThruster, "chkHasbowThruster");
		((System.Windows.Forms.Control)(object)this.chkHasbowThruster).Name = "chkHasbowThruster";
		((UltraToggleEditorBase)this.chkHasbowThruster).CheckedChanged += new System.EventHandler(chkHasbowThruster_CheckedChanged);
		resources.ApplyResources(this.txtBowThrusterHP, "txtBowThrusterHP");
		((System.Windows.Forms.Control)(object)this.txtBowThrusterHP).Name = "txtBowThrusterHP";
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		this.ultraLabel5.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.txtSternThrusterHP, "txtSternThrusterHP");
		((System.Windows.Forms.Control)(object)this.txtSternThrusterHP).Name = "txtSternThrusterHP";
		resources.ApplyResources(this.ultraLabel7, "ultraLabel7");
		this.ultraLabel7.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel7).Name = "ultraLabel7";
		((ControlBase)this.ultraLabel7).WrapText = false;
		resources.ApplyResources(this.chkHasSternThruster, "chkHasSternThruster");
		((System.Windows.Forms.Control)(object)this.chkHasSternThruster).Name = "chkHasSternThruster";
		((UltraToggleEditorBase)this.chkHasSternThruster).CheckedChanged += new System.EventHandler(chkHasSternThruster_CheckedChanged);
		resources.ApplyResources(this.txtISPSCertificateOnBoardNo, "txtISPSCertificateOnBoardNo");
		((System.Windows.Forms.Control)(object)this.txtISPSCertificateOnBoardNo).Name = "txtISPSCertificateOnBoardNo";
		resources.ApplyResources(this.chkHasISPsCertificateOnBoard, "chkHasISPsCertificateOnBoard");
		((System.Windows.Forms.Control)(object)this.chkHasISPsCertificateOnBoard).Name = "chkHasISPsCertificateOnBoard";
		((UltraToggleEditorBase)this.chkHasISPsCertificateOnBoard).CheckedChanged += new System.EventHandler(chkHasISPsCertificateOnBoard_CheckedChanged);
		resources.ApplyResources(this.lblISPSExpirationDate, "lblISPSExpirationDate");
		this.lblISPSExpirationDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblISPSExpirationDate).Name = "lblISPSExpirationDate";
		((ControlBase)this.lblISPSExpirationDate).WrapText = false;
		resources.ApplyResources(this.dtpISPSExpirationDate, "dtpISPSExpirationDate");
		((UltraWinEditorMaskedControlBase)this.dtpISPSExpirationDate).AlwaysInEditMode = true;
		this.dtpISPSExpirationDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpISPSExpirationDate).Name = "dtpISPSExpirationDate";
		resources.ApplyResources(this.lblPresentSecurityLevel, "lblPresentSecurityLevel");
		this.lblPresentSecurityLevel.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPresentSecurityLevel).Name = "lblPresentSecurityLevel";
		((ControlBase)this.lblPresentSecurityLevel).WrapText = false;
		resources.ApplyResources(this.cboPresentSecurityLevel, "cboPresentSecurityLevel");
		((TextEditorControlBase)this.cboPresentSecurityLevel).AlwaysInEditMode = true;
		this.cboPresentSecurityLevel.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboPresentSecurityLevel).Name = "cboPresentSecurityLevel";
		resources.ApplyResources(this.chkContinousSynopsisRecord, "chkContinousSynopsisRecord");
		((System.Windows.Forms.Control)(object)this.chkContinousSynopsisRecord).Name = "chkContinousSynopsisRecord";
		resources.ApplyResources(this.chkFullInternationalShipSecurityCertificate, "chkFullInternationalShipSecurityCertificate");
		((System.Windows.Forms.Control)(object)this.chkFullInternationalShipSecurityCertificate).Name = "chkFullInternationalShipSecurityCertificate";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpISPSExpirationDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblISPSExpirationDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkFullInternationalShipSecurityCertificate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkContinousSynopsisRecord);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkHasISPsCertificateOnBoard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkHasSternThruster);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkHasbowThruster);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel7);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblm);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCharter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCharterers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOwner);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPresentSecurityLevel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboOwners);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNRT);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNRT);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGRT);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGRT);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtISPSCertificateOnBoardNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSDWT);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSternThrusterHP);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSDWT);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBowThrusterHP);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDepth);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBeam);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDepth);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBeam);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtLOA);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLOA);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtFuelTankCapacity);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFuelTankCapacity);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVesselClassificationOffice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboVesselClassificationOffice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBuiltYear);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBuiltYear);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCallSign);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtIMO);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBuiltPlace);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRegisterationNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPresentSecurityLevel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCallSign);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblIMO);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBuiltPlace);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRegisterationNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRegisterationPlace);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboRegisterationPlace);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNationality);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboNationality);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVesselType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboVesselType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVesselNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVesselNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVesselNameAr);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVesselNameAr);
		base.Name = "frmVessels";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVesselNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVesselNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVesselNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVesselNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboVesselType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVesselType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboNationality, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNationality, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboRegisterationPlace, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRegisterationPlace, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRegisterationNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBuiltPlace, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblIMO, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCallSign, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPresentSecurityLevel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRegisterationNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBuiltPlace, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtIMO, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCallSign, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBuiltYear, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBuiltYear, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboVesselClassificationOffice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVesselClassificationOffice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFuelTankCapacity, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtFuelTankCapacity, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLOA, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtLOA, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBeam, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDepth, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBeam, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDepth, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBowThrusterHP, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSDWT, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSternThrusterHP, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSDWT, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtISPSCertificateOnBoardNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGRT, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGRT, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNRT, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNRT, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboOwners, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPresentSecurityLevel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOwner, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCharterers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCharter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblm, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel6, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel5, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel7, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel4, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkHasbowThruster, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkHasSternThruster, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkHasISPsCertificateOnBoard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkContinousSynopsisRecord, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkFullInternationalShipSecurityCertificate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblISPSExpirationDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpISPSExpirationDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
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
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVesselNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVesselNameAr).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVesselType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboNationality).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboRegisterationPlace).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRegisterationNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBuiltPlace).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBuiltYear).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtIMO).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCallSign).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFuelTankCapacity).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLOA).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBeam).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSDWT).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGRT).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNRT).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboOwners).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCharterers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDepth).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVesselClassificationOffice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkHasbowThruster).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBowThrusterHP).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSternThrusterHP).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkHasSternThruster).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtISPSCertificateOnBoardNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkHasISPsCertificateOnBoard).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpISPSExpirationDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPresentSecurityLevel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkContinousSynopsisRecord).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkFullInternationalShipSecurityCertificate).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
