using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.General;
using BusinessLayer.HR;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.HR.Personal.MasterData;

public class frmSocialInssuranceOffices : frmGrid
{
	private DataTable dtCities;

	private DataTable dtAreas;

	private DataTable dtReports;

	private IContainer components = null;

	private UltraLabel lblEnglishName;

	private UltraTextEditor txtEnglishName;

	private UltraLabel lblArabicName;

	private UltraTextEditor txtArabicName;

	private UltraLabel lblCode;

	private UltraTextEditor txtCode;

	private UltraTextEditor txtCompanyNo;

	private UltraLabel lblCompanyNo;

	private UltraTextEditor txtArea;

	private UltraLabel ultraLabel2;

	private UltraTextEditor txtCompanyName;

	private UltraLabel lblCompanyName;

	private UltraTextEditor txtOwner;

	private UltraTextEditor txtManager;

	private UltraTextEditor txtAdjective;

	private UltraLabel lblOwner;

	private UltraLabel lblManager;

	private UltraLabel lblAdjective;

	private UltraTextEditor txtAddress;

	private UltraTextEditor txtBuildingNo;

	private UltraLabel lblAddress;

	private UltraLabel lblBuildingNo;

	private UltraComboEditor cboArea;

	private UltraLabel lblArea;

	private UltraComboEditor cboCity;

	private UltraLabel lblCity;

	private UltraTextEditor txtVillage;

	private UltraLabel lblVillage;

	public frmSocialInssuranceOffices()
	{
		InitializeComponent();
		TableName = "HR_SocialInssuranceOffices";
		IDCol = "SocialInssuranceOfficeID";
	}

	public override void PrepareData()
	{
		dtCities = Cities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCity, dtCities, "CityID", "CityName");
		dtAreas = Areas.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboArea, dtAreas, "AreaID", "AreaName");
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
	}

	public override void btnRefreshDataClick()
	{
		dtCities = Cities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCity, dtCities, "CityID", "CityName");
		dtAreas = Areas.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboArea, dtAreas, "AreaID", "AreaName");
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEnglishName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAddress).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAdjective).ReadOnly = NavMode;
		((EditorButtonControlBase)txtArea).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBuildingNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtVillage).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCompanyName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCompanyNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtManager).ReadOnly = NavMode;
		((EditorButtonControlBase)txtOwner).ReadOnly = NavMode;
		((EditorButtonControlBase)cboArea).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCity).ReadOnly = NavMode;
		((TextEditorControlBase)txtCode).Focus();
	}

	public override void ClearControls()
	{
		((TextEditorControlBase)txtArabicName).Clear();
		((TextEditorControlBase)txtEnglishName).Clear();
		((TextEditorControlBase)txtAddress).Clear();
		((TextEditorControlBase)txtAdjective).Clear();
		((TextEditorControlBase)txtArea).Clear();
		((TextEditorControlBase)txtBuildingNo).Clear();
		((TextEditorControlBase)txtVillage).Clear();
		((TextEditorControlBase)txtCode).Clear();
		((TextEditorControlBase)txtCompanyName).Clear();
		((TextEditorControlBase)txtCompanyNo).Clear();
		((TextEditorControlBase)txtManager).Clear();
		((TextEditorControlBase)txtOwner).Clear();
		cboCity.SelectedIndex = -1;
		cboArea.SelectedIndex = -1;
		((Control)(object)txtCode).Text = (Adding ? SocialInssuranceOffices.GetCode(IsFromServer: true) : "");
	}

	public override void FillData()
	{
		dataTable = SocialInssuranceOffices.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
	}

	public override void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = dataTable;
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialInssuranceOfficeID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialInssuranceOfficeCode"].Header).Caption = (GlobalVariables.IsArabic ? "الكود" : "Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialInssuranceOfficeCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialInssuranceOfficeCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialInssuranceOfficeNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialInssuranceOfficeNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialInssuranceOfficeNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.09) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialInssuranceOfficeNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialInssuranceOfficeNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialInssuranceOfficeNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم المنشأة" : "Company No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Area"].Header).Caption = (GlobalVariables.IsArabic ? "المنطقة" : "Area");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Area"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Area"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم المنشأة" : "Company Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Owner"].Header).Caption = (GlobalVariables.IsArabic ? "المالك" : "Owner");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Owner"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Owner"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Manager"].Header).Caption = (GlobalVariables.IsArabic ? "المدير" : "Manager");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Manager"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Manager"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Adjective"].Header).Caption = (GlobalVariables.IsArabic ? "الصفة" : "Adjective");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Adjective"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Adjective"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Address"].Header).Caption = (GlobalVariables.IsArabic ? "العنوان" : "Address");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Address"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Address"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BuildingNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم العقار" : "Building No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BuildingNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BuildingNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Village"].Header).Caption = (GlobalVariables.IsArabic ? "الشياخة/القرية" : "Village");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Village"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Village"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityID"].Header).Caption = (GlobalVariables.IsArabic ? "المحافظة" : "City");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AreaID"].Header).Caption = (GlobalVariables.IsArabic ? "القسم /الشياخة" : "Area");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AreaID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AreaID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtCode).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["SocialInssuranceOfficeCode"].Value.ToString();
		((Control)(object)txtArabicName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["SocialInssuranceOfficeNameAr"].Value.ToString();
		((Control)(object)txtEnglishName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["SocialInssuranceOfficeNameEn"].Value.ToString();
		((Control)(object)txtCompanyNo).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["CompanyNo"].Value.ToString();
		((Control)(object)txtArea).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["Area"].Value.ToString();
		((Control)(object)txtCompanyName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["CompanyName"].Value.ToString();
		((Control)(object)txtOwner).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["Owner"].Value.ToString();
		((Control)(object)txtManager).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["Manager"].Value.ToString();
		((Control)(object)txtAdjective).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["Adjective"].Value.ToString();
		((Control)(object)txtAddress).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["Address"].Value.ToString();
		((Control)(object)txtBuildingNo).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["BuildingNo"].Value.ToString();
		((Control)(object)txtVillage).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["Village"].Value.ToString();
		((TextEditorControlBase)cboCity).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["CityID"].Value;
		((TextEditorControlBase)cboArea).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["AreaID"].Value;
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال كود مكتب العمل ", "Please Insert Social Inssurance Office Code");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال الاسم بالعربية", "Please Insert Social Inssurance Office Arabic Name");
			((TextEditorControlBase)txtArabicName).Focus();
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		SocialInssuranceOffices.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, (((Control)(object)txtCompanyNo).Text == "") ? "Null" : ((Control)(object)txtCompanyNo).Text, (((Control)(object)txtArea).Text == "") ? "Null" : ((Control)(object)txtArea).Text, (((Control)(object)txtCompanyName).Text == "") ? "Null" : ((Control)(object)txtCompanyName).Text, (((Control)(object)txtOwner).Text == "") ? "Null" : ((Control)(object)txtOwner).Text, (((Control)(object)txtManager).Text == "") ? "Null" : ((Control)(object)txtManager).Text, (((Control)(object)txtAdjective).Text == "") ? "Null" : ((Control)(object)txtAdjective).Text, (((Control)(object)txtAddress).Text == "") ? "Null" : ((Control)(object)txtAddress).Text, (((Control)(object)txtBuildingNo).Text == "") ? "Null" : ((Control)(object)txtBuildingNo).Text, (((Control)(object)txtVillage).Text == "") ? "Null" : ((Control)(object)txtVillage).Text, (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void UpdateData()
	{
		SocialInssuranceOffices.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["SocialInssuranceOfficeID"].Value.ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, (((Control)(object)txtCompanyNo).Text == "") ? "Null" : ((Control)(object)txtCompanyNo).Text, (((Control)(object)txtArea).Text == "") ? "Null" : ((Control)(object)txtArea).Text, (((Control)(object)txtCompanyName).Text == "") ? "Null" : ((Control)(object)txtCompanyName).Text, (((Control)(object)txtOwner).Text == "") ? "Null" : ((Control)(object)txtOwner).Text, (((Control)(object)txtManager).Text == "") ? "Null" : ((Control)(object)txtManager).Text, (((Control)(object)txtAdjective).Text == "") ? "Null" : ((Control)(object)txtAdjective).Text, (((Control)(object)txtAddress).Text == "") ? "Null" : ((Control)(object)txtAddress).Text, (((Control)(object)txtBuildingNo).Text == "") ? "Null" : ((Control)(object)txtBuildingNo).Text, (((Control)(object)txtVillage).Text == "") ? "Null" : ((Control)(object)txtVillage).Text, (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString().Equals("True") ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void DeleteData()
	{
		SocialInssuranceOffices.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["SocialInssuranceOfficeID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
		ClearControls();
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

	public override void btnPrintClick()
	{
		string val = "";
		if (dtReports.Rows.Count > 0)
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
			val = dtReports.Rows[0]["isoCode"].ToString();
		}
		else
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_HR_SocialInssuranceOffices_A.rpt" : "Rep_HR_SocialInssuranceOffices_E.rpt"));
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
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
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HR.Personal.MasterData.frmSocialInssuranceOffices));
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
		this.lblEnglishName = new UltraLabel();
		this.txtEnglishName = new UltraTextEditor();
		this.lblArabicName = new UltraLabel();
		this.txtArabicName = new UltraTextEditor();
		this.lblCode = new UltraLabel();
		this.txtCode = new UltraTextEditor();
		this.txtCompanyNo = new UltraTextEditor();
		this.lblCompanyNo = new UltraLabel();
		this.txtArea = new UltraTextEditor();
		this.ultraLabel2 = new UltraLabel();
		this.txtCompanyName = new UltraTextEditor();
		this.lblCompanyName = new UltraLabel();
		this.txtOwner = new UltraTextEditor();
		this.txtManager = new UltraTextEditor();
		this.txtAdjective = new UltraTextEditor();
		this.lblOwner = new UltraLabel();
		this.lblManager = new UltraLabel();
		this.lblAdjective = new UltraLabel();
		this.txtAddress = new UltraTextEditor();
		this.txtBuildingNo = new UltraTextEditor();
		this.lblAddress = new UltraLabel();
		this.lblBuildingNo = new UltraLabel();
		this.cboArea = new UltraComboEditor();
		this.lblArea = new UltraLabel();
		this.cboCity = new UltraComboEditor();
		this.lblCity = new UltraLabel();
		this.txtVillage = new UltraTextEditor();
		this.lblVillage = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompanyNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArea).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompanyName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOwner).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtManager).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAdjective).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBuildingNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVillage).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.ULGData, "ULGData");
		((UltraGridBase)base.ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
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
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
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
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val8, "appearance13");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance9");
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val9;
		base.cboTransactionBranch.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboTransactionBranch.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblEnglishName, "lblEnglishName");
		this.lblEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEnglishName).Name = "lblEnglishName";
		((ControlBase)this.lblEnglishName).WrapText = false;
		resources.ApplyResources(this.txtEnglishName, "txtEnglishName");
		resources.ApplyResources(val10, "appearance10");
		((TextEditorControlBase)this.txtEnglishName).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.txtEnglishName).Name = "txtEnglishName";
		resources.ApplyResources(this.lblArabicName, "lblArabicName");
		this.lblArabicName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArabicName).Name = "lblArabicName";
		((ControlBase)this.lblArabicName).WrapText = false;
		resources.ApplyResources(this.txtArabicName, "txtArabicName");
		((System.Windows.Forms.Control)(object)this.txtArabicName).Name = "txtArabicName";
		resources.ApplyResources(this.lblCode, "lblCode");
		this.lblCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCode).Name = "lblCode";
		((ControlBase)this.lblCode).WrapText = false;
		resources.ApplyResources(this.txtCode, "txtCode");
		((System.Windows.Forms.Control)(object)this.txtCode).Name = "txtCode";
		resources.ApplyResources(this.txtCompanyNo, "txtCompanyNo");
		((System.Windows.Forms.Control)(object)this.txtCompanyNo).Name = "txtCompanyNo";
		resources.ApplyResources(this.lblCompanyNo, "lblCompanyNo");
		this.lblCompanyNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCompanyNo).Name = "lblCompanyNo";
		((ControlBase)this.lblCompanyNo).WrapText = false;
		resources.ApplyResources(this.txtArea, "txtArea");
		((System.Windows.Forms.Control)(object)this.txtArea).Name = "txtArea";
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.txtCompanyName, "txtCompanyName");
		((System.Windows.Forms.Control)(object)this.txtCompanyName).Name = "txtCompanyName";
		resources.ApplyResources(this.lblCompanyName, "lblCompanyName");
		this.lblCompanyName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCompanyName).Name = "lblCompanyName";
		((ControlBase)this.lblCompanyName).WrapText = false;
		resources.ApplyResources(this.txtOwner, "txtOwner");
		((System.Windows.Forms.Control)(object)this.txtOwner).Name = "txtOwner";
		resources.ApplyResources(this.txtManager, "txtManager");
		((System.Windows.Forms.Control)(object)this.txtManager).Name = "txtManager";
		resources.ApplyResources(this.txtAdjective, "txtAdjective");
		((System.Windows.Forms.Control)(object)this.txtAdjective).Name = "txtAdjective";
		resources.ApplyResources(this.lblOwner, "lblOwner");
		this.lblOwner.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOwner).Name = "lblOwner";
		((ControlBase)this.lblOwner).WrapText = false;
		resources.ApplyResources(this.lblManager, "lblManager");
		this.lblManager.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblManager).Name = "lblManager";
		((ControlBase)this.lblManager).WrapText = false;
		resources.ApplyResources(this.lblAdjective, "lblAdjective");
		this.lblAdjective.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAdjective).Name = "lblAdjective";
		((ControlBase)this.lblAdjective).WrapText = false;
		resources.ApplyResources(this.txtAddress, "txtAddress");
		((System.Windows.Forms.Control)(object)this.txtAddress).Name = "txtAddress";
		resources.ApplyResources(this.txtBuildingNo, "txtBuildingNo");
		((System.Windows.Forms.Control)(object)this.txtBuildingNo).Name = "txtBuildingNo";
		resources.ApplyResources(this.lblAddress, "lblAddress");
		this.lblAddress.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAddress).Name = "lblAddress";
		((ControlBase)this.lblAddress).WrapText = false;
		resources.ApplyResources(this.lblBuildingNo, "lblBuildingNo");
		this.lblBuildingNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBuildingNo).Name = "lblBuildingNo";
		((ControlBase)this.lblBuildingNo).WrapText = false;
		resources.ApplyResources(this.cboArea, "cboArea");
		((System.Windows.Forms.Control)(object)this.cboArea).Name = "cboArea";
		resources.ApplyResources(this.lblArea, "lblArea");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val11, "appearance14");
		((ControlBase)this.lblArea).Appearance = (AppearanceBase)(object)val11;
		this.lblArea.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArea).Name = "lblArea";
		((ControlBase)this.lblArea).WrapText = false;
		resources.ApplyResources(this.cboCity, "cboCity");
		((System.Windows.Forms.Control)(object)this.cboCity).Name = "cboCity";
		((TextEditorControlBase)this.cboCity).ValueChanged += new System.EventHandler(cboCity_ValueChanged);
		resources.ApplyResources(this.lblCity, "lblCity");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val12, "appearance15");
		((ControlBase)this.lblCity).Appearance = (AppearanceBase)(object)val12;
		this.lblCity.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCity).Name = "lblCity";
		((ControlBase)this.lblCity).WrapText = false;
		resources.ApplyResources(this.txtVillage, "txtVillage");
		((System.Windows.Forms.Control)(object)this.txtVillage).Name = "txtVillage";
		resources.ApplyResources(this.lblVillage, "lblVillage");
		this.lblVillage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVillage).Name = "lblVillage";
		((ControlBase)this.lblVillage).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboArea);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArea);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCity);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCity);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAdjective);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCompanyName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVillage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBuildingNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblManager);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAddress);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOwner);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCompanyNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAdjective);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVillage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBuildingNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtManager);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCompanyName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAddress);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtOwner);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArea);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCompanyNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArabicName);
		base.Name = "frmSocialInssuranceOffices";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCompanyNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArea, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtOwner, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAddress, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCompanyName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtManager, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBuildingNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVillage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAdjective, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCompanyNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOwner, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAddress, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblManager, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBuildingNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVillage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCompanyName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAdjective, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCity, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCity, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArea, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboArea, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompanyNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArea).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompanyName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOwner).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtManager).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAdjective).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBuildingNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVillage).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
