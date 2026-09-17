using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using BusinessLayer.EInvoices;
using BusinessLayer.General;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SystemOptions.GeneralData;

public class frmBranches : frmGrid
{
	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private DataTable dtCities;

	private DataTable dtAreas;

	private DataTable dtEINVTaxActivityTypes;

	private ValueList vlAccounts = new ValueList();

	private ValueList vlCities = new ValueList();

	private ValueList vlSubAccounts = new ValueList();

	private ValueList vlAreas = new ValueList();

	private ValueList vlEINVTaxActivityTypes = new ValueList();

	private bool UsingElectronicInvoice = false;

	private IContainer components = null;

	private UltraLabel lblBranchEnglishName;

	private UltraTextEditor txtBranchEnglishName;

	private UltraLabel lblBranchArabicName;

	private UltraTextEditor txtBranchArabicName;

	private UltraLabel lblBranchCode;

	private UltraTextEditor txtBranchCode;

	private UltraCheckEditor chkIsMainBranch;

	public UltraButton btnBranchSubAccountSearch;

	private UltraComboEditor cboBranchSubAccount;

	private UltraLabel lblBranchSubAccount;

	public UltraButton btnBranchAccountSearch;

	private UltraComboEditor cboBranchAccount;

	private UltraLabel lblAccountName;

	private UltraTextEditor txtTaxNo;

	private UltraLabel lblTaxNo;

	private UltraTextEditor txtCommercialRegistrationNo;

	private UltraLabel lblCommercialRegistrationNo;

	private UltraTextEditor txtLicenseNo;

	private UltraLabel lblLicenseNo;

	private UltraComboEditor cboCity;

	private UltraLabel lblCity;

	private UltraTextEditor txtAddress;

	private UltraLabel lblAddress;

	private UltraTextEditor txtTel;

	private UltraTextEditor txtEMail;

	private UltraLabel lblMax;

	private UltraLabel lblEMail;

	private UltraTextEditor txtFax;

	private UltraLabel lblMin;

	private UltraLabel lblTaxFileNo;

	private UltraTextEditor txtTaxFileNo;

	private UltraLabel lblTaxOfficeName;

	private UltraTextEditor txtTaxOfficeName;

	private UltraLabel lblEINVBranchCode;

	private UltraTextEditor txtEINVBranchCode;

	private UltraTextEditor txtBuildingNumber;

	private UltraLabel lblBuildingNumber;

	private UltraComboEditor cboArea;

	private UltraLabel lblArea;

	private UltraComboEditor cboEINVTaxActivityType;

	private UltraLabel lblEINVTaxActivityType;

	public frmBranches()
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
		InitializeComponent();
		TableName = "G_Branches";
		IDCol = "BranchID";
	}

	public override void PrepareData()
	{
		UsingElectronicInvoice = GlobalFunctions.GetOption("UsingElectronicInvoice");
		dtCities = Cities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCity, dtCities, "CityID", "CityName");
		vlCities.ValueListItems.Clear();
		for (int i = 0; i < dtCities.Rows.Count; i++)
		{
			vlCities.ValueListItems.Add((object)dtCities.Rows[i]["CityID"].ToString(), dtCities.Rows[i]["CityName"].ToString());
		}
		dtAreas = Areas.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboArea, dtAreas, "AreaID", "AreaName");
		vlAreas.ValueListItems.Clear();
		for (int j = 0; j < dtAreas.Rows.Count; j++)
		{
			vlAreas.ValueListItems.Add((object)dtAreas.Rows[j]["AreaID"].ToString(), dtAreas.Rows[j]["AreaName"].ToString());
		}
		if (UsingElectronicInvoice)
		{
			dtEINVTaxActivityTypes = TaxsActivitiesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboEINVTaxActivityType, dtEINVTaxActivityTypes, "EINVTaxActivityTypeID", "EINVTaxActivityTypeName");
			vlEINVTaxActivityTypes.ValueListItems.Clear();
			for (int k = 0; k < dtEINVTaxActivityTypes.Rows.Count; k++)
			{
				vlEINVTaxActivityTypes.ValueListItems.Add((object)dtEINVTaxActivityTypes.Rows[k]["EINVTaxActivityTypeID"].ToString(), dtEINVTaxActivityTypes.Rows[k]["EINVTaxActivityTypeName"].ToString());
			}
		}
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboBranchAccount, dtAccounts, "AccountID", "Name");
		vlAccounts.ValueListItems.Clear();
		for (int l = 0; l < dtAccounts.Rows.Count; l++)
		{
			vlAccounts.ValueListItems.Add((object)dtAccounts.Rows[l]["AccountID"].ToString(), dtAccounts.Rows[l]["Name"].ToString());
		}
		dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboBranchSubAccount, dtSubAccounts, "SubAccountID", "Name");
		vlSubAccounts.ValueListItems.Clear();
		for (int m = 0; m < dtSubAccounts.Rows.Count; m++)
		{
			vlSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[m]["SubAccountID"], dtSubAccounts.Rows[m]["Name"].ToString());
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtBranchArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBranchEnglishName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBranchCode).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBranchAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBranchSubAccount).ReadOnly = NavMode;
		((Control)(object)chkIsMainBranch).Enabled = !NavMode;
		((EditorButtonControlBase)txtCommercialRegistrationNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTaxNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTaxFileNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTaxOfficeName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtLicenseNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAddress).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTel).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEMail).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEINVBranchCode).ReadOnly = NavMode;
		((EditorButtonControlBase)cboEINVTaxActivityType).ReadOnly = NavMode;
		UltraTextEditor obj = txtEINVBranchCode;
		UltraLabel obj2 = lblEINVBranchCode;
		UltraLabel obj3 = lblEINVTaxActivityType;
		bool flag = (((Control)(object)cboEINVTaxActivityType).Visible = UsingElectronicInvoice);
		bool flag2 = (((Control)(object)obj3).Visible = flag);
		bool visible = (((Control)(object)obj2).Visible = flag2);
		((Control)(object)obj).Visible = visible;
		((EditorButtonControlBase)txtFax).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCity).ReadOnly = NavMode;
		((EditorButtonControlBase)cboArea).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBuildingNumber).ReadOnly = NavMode;
		((Control)(object)btnBranchAccountSearch).Visible = !NavMode;
		((Control)(object)btnBranchSubAccountSearch).Visible = !NavMode;
		((TextEditorControlBase)txtBranchCode).Focus();
	}

	public override void ClearControls()
	{
		((TextEditorControlBase)txtBranchArabicName).Clear();
		((TextEditorControlBase)txtBranchEnglishName).Clear();
		((TextEditorControlBase)txtBranchCode).Clear();
		((UltraToggleEditorBase)chkIsMainBranch).Checked = false;
		((TextEditorControlBase)txtCommercialRegistrationNo).Clear();
		((TextEditorControlBase)txtTaxNo).Clear();
		((TextEditorControlBase)txtTaxFileNo).Clear();
		((TextEditorControlBase)txtTaxOfficeName).Clear();
		((TextEditorControlBase)txtLicenseNo).Clear();
		((TextEditorControlBase)txtAddress).Clear();
		((TextEditorControlBase)txtTel).Clear();
		((TextEditorControlBase)txtEMail).Clear();
		((TextEditorControlBase)txtEINVBranchCode).Clear();
		((TextEditorControlBase)txtFax).Clear();
		cboCity.SelectedIndex = -1;
		cboArea.SelectedIndex = -1;
		((TextEditorControlBase)txtBuildingNumber).Clear();
		cboBranchAccount.SelectedIndex = -1;
		cboBranchSubAccount.SelectedIndex = -1;
		cboEINVTaxActivityType.SelectedIndex = -1;
	}

	public override void FillData()
	{
		dataTable = Branches.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
	}

	public override void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = dataTable;
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchCode"].Header).Caption = (GlobalVariables.IsArabic ? "الكود" : "Branch Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchNameAr"].Width = (UsingElectronicInvoice ? ((int)((double)((Control)(object)ULGData).Width * 0.05)) : ((int)((double)((Control)(object)ULGData).Width * 0.1))) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchNameEn"].Width = (UsingElectronicInvoice ? ((int)((double)((Control)(object)ULGData).Width * 0.05)) : ((int)((double)((Control)(object)ULGData).Width * 0.1)));
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CommercialRegistrationNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم السجل التجارى" : "Commercial Registration No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CommercialRegistrationNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CommercialRegistrationNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxNo"].Header).Caption = (GlobalVariables.IsArabic ? "الرقم التسحيل الضريبى" : "Tax No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxFileNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الملف الضريبي" : "Tax File No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxFileNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxFileNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxOfficeName"].Header).Caption = (GlobalVariables.IsArabic ? "مأمورية الضرائب" : "Tax Office");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxOfficeName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxOfficeName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LicenseNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الترخيص" : "License No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LicenseNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LicenseNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Phone"].Header).Caption = (GlobalVariables.IsArabic ? "التليفون" : "Phone");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Phone"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Phone"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Fax"].Header).Caption = (GlobalVariables.IsArabic ? "الفاكس" : "Fax");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Fax"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Fax"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Address"].Header).Caption = (GlobalVariables.IsArabic ? "العنوان" : "Address");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Address"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Address"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Email"].Header).Caption = (GlobalVariables.IsArabic ? "البريد الالكتروني" : "Email");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Email"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Email"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVBranchCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود الفاتورة" : "Invoice Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVBranchCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVBranchCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVBranchCode"].Hidden = !UsingElectronicInvoice;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityID"].Header).Caption = (GlobalVariables.IsArabic ? "المدينة" : "City");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityID"].ValueList = (IValueList)(object)vlCities;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "حساب الفرع" : "Branch Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchAccountID"].ValueList = (IValueList)(object)vlAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchSubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب التحليلى " : "SubAccount");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchSubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchSubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchSubAccountID"].ValueList = (IValueList)(object)vlSubAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsMainBranch"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع الرئيسي" : "Main Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsMainBranch"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsMainBranch"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AreaID"].Header).Caption = (GlobalVariables.IsArabic ? "المنطقة" : "Area");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AreaID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AreaID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AreaID"].ValueList = (IValueList)(object)vlAreas;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BuildingNumber"].Header).Caption = (GlobalVariables.IsArabic ? "رقم المبنى" : "Building Number");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BuildingNumber"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BuildingNumber"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVTaxActivityTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع النشاط" : "Activity Type");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVTaxActivityTypeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVTaxActivityTypeID"].Hidden = !UsingElectronicInvoice;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVTaxActivityTypeID"].ValueList = (IValueList)(object)vlEINVTaxActivityTypes;
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtBranchCode).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["BranchCode"].Value.ToString();
		((Control)(object)txtBranchArabicName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["BranchNameAr"].Value.ToString();
		((Control)(object)txtBranchEnglishName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["BranchNameEn"].Value.ToString();
		((UltraToggleEditorBase)chkIsMainBranch).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsMainBranch"].Value.ToString());
		((Control)(object)txtCommercialRegistrationNo).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["CommercialRegistrationNo"].Value.ToString();
		((Control)(object)txtTaxNo).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["TaxNo"].Value.ToString();
		((Control)(object)txtTaxFileNo).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["TaxFileNo"].Value.ToString();
		((Control)(object)txtTaxOfficeName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["TaxOfficeName"].Value.ToString();
		((Control)(object)txtLicenseNo).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["LicenseNo"].Value.ToString();
		((TextEditorControlBase)cboBranchAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["BranchAccountID"].Value;
		((TextEditorControlBase)cboBranchSubAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["BranchSubAccountID"].Value;
		((TextEditorControlBase)cboEINVTaxActivityType).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["EINVTaxActivityTypeID"].Value;
		((Control)(object)txtAddress).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["Address"].Value.ToString();
		((Control)(object)txtFax).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["Fax"].Value.ToString();
		((Control)(object)txtTel).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["Phone"].Value.ToString();
		((Control)(object)txtEMail).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["Email"].Value.ToString();
		((Control)(object)txtEINVBranchCode).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["EINVBranchCode"].Value.ToString();
		((TextEditorControlBase)cboCity).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["CityID"].Value;
		((TextEditorControlBase)cboArea).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["AreaID"].Value;
		((Control)(object)txtBuildingNumber).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["BuildingNumber"].Value.ToString();
	}

	public override bool ValidateData()
	{
		if (((UltraToggleEditorBase)chkIsMainBranch).Checked && dataTable.Select("IsMainBranch=1 And BranchID <>" + (Adding ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["BranchID"].Value.ToString())).Length != 0)
		{
			GlobalVariables.InformationMB.Show("يجب ان يكون هناك فرع رئيسي واحد ", "You Should Have Only One Main Branch");
			((Control)(object)chkIsMainBranch).Focus();
			return false;
		}
		if (((Control)(object)txtBranchCode).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال كود الفرع ", "Please Insert Branch Code");
			((TextEditorControlBase)txtBranchCode).Focus();
			return false;
		}
		if (((Control)(object)txtBranchArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال إسم الفرع بالعربية", "Please Insert Branch Arabic Name");
			((TextEditorControlBase)txtBranchArabicName).Focus();
			return false;
		}
		if (UsingElectronicInvoice)
		{
			if (((Control)(object)txtTaxNo).Text.Trim().Equals(""))
			{
				GlobalVariables.InformationMB.Show("من فضلك ادخل الرقم الضريبي", "Please Enter Tax Number");
				((TextEditorControlBase)txtTaxNo).Focus();
				return false;
			}
			if (((Control)(object)txtTaxNo).Text.Trim().Length != 9)
			{
				GlobalVariables.InformationMB.Show("من فضلك ادخل رقم ضريبي صحيح", "Please Enter A Valid Tax Number");
				((TextEditorControlBase)txtTaxNo).Focus();
				return false;
			}
			if (((Control)(object)txtEINVBranchCode).Text.Trim().Equals(""))
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال كود الفاتورة الالكترونية", "Please Insert E-Invoice Code");
				((TextEditorControlBase)txtEINVBranchCode).Focus();
				return false;
			}
			if (cboEINVTaxActivityType.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار نوع النشاط", "Please Select Activity Type");
				((TextEditorControlBase)cboEINVTaxActivityType).Focus();
				cboEINVTaxActivityType.DropDown();
				return false;
			}
			if (cboCity.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار المحافظة", "Please Select City");
				((TextEditorControlBase)cboCity).Focus();
				cboCity.DropDown();
				return false;
			}
			if (cboArea.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار المنطقة ", "Please Select Area");
				((TextEditorControlBase)cboArea).Focus();
				cboArea.DropDown();
				return false;
			}
			if (((Control)(object)txtBuildingNumber).Text.Trim().Equals(""))
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال رقم المبنى", "Please Insert Building Number");
				((TextEditorControlBase)txtBuildingNumber).Focus();
				return false;
			}
		}
		if (cboBranchAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار حساب الفرع", "Please Select Branch Account");
			((TextEditorControlBase)cboBranchAccount).Focus();
			cboBranchAccount.DropDown();
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		int num = Branches.Insert_Update("-1", ((Control)(object)txtBranchCode).Text, ((Control)(object)txtBranchArabicName).Text, (((Control)(object)txtBranchEnglishName).Text == "") ? "Null" : ((Control)(object)txtBranchEnglishName).Text, (cboBranchAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBranchAccount).Value.ToString(), (cboBranchSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBranchSubAccount).Value.ToString(), ((UltraToggleEditorBase)chkIsMainBranch).Checked ? "1" : "0", (((Control)(object)txtCommercialRegistrationNo).Text == "") ? "Null" : ((Control)(object)txtCommercialRegistrationNo).Text, (((Control)(object)txtTaxNo).Text == "") ? "Null" : ((Control)(object)txtTaxNo).Text, (((Control)(object)txtTaxFileNo).Text == "") ? "Null" : ((Control)(object)txtTaxFileNo).Text, (((Control)(object)txtTaxOfficeName).Text == "") ? "Null" : ((Control)(object)txtTaxOfficeName).Text, (((Control)(object)txtLicenseNo).Text == "") ? "Null" : ((Control)(object)txtLicenseNo).Text, (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), (((Control)(object)txtBuildingNumber).Text == "") ? "Null" : ((Control)(object)txtBuildingNumber).Text, (((Control)(object)txtAddress).Text == "") ? "Null" : ((Control)(object)txtAddress).Text, (((Control)(object)txtTel).Text == "") ? "Null" : ((Control)(object)txtTel).Text, (((Control)(object)txtFax).Text == "") ? "Null" : ((Control)(object)txtFax).Text, (((Control)(object)txtEMail).Text == "") ? "Null" : ((Control)(object)txtEMail).Text, (((Control)(object)txtEINVBranchCode).Text == "") ? "Null" : ((Control)(object)txtEINVBranchCode).Text, (cboEINVTaxActivityType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboEINVTaxActivityType).Value.ToString(), "0", GlobalVariables.UserID, IsFromServer: true);
	}

	public override void UpdateData()
	{
		Branches.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["BranchID"].Value.ToString(), ((Control)(object)txtBranchCode).Text, ((Control)(object)txtBranchArabicName).Text, (((Control)(object)txtBranchEnglishName).Text == "") ? "Null" : ((Control)(object)txtBranchEnglishName).Text, (cboBranchAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBranchAccount).Value.ToString(), (cboBranchSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBranchSubAccount).Value.ToString(), ((UltraToggleEditorBase)chkIsMainBranch).Checked ? "1" : "0", (((Control)(object)txtCommercialRegistrationNo).Text == "") ? "Null" : ((Control)(object)txtCommercialRegistrationNo).Text, (((Control)(object)txtTaxNo).Text == "") ? "Null" : ((Control)(object)txtTaxNo).Text, (((Control)(object)txtTaxFileNo).Text == "") ? "Null" : ((Control)(object)txtTaxFileNo).Text, (((Control)(object)txtTaxOfficeName).Text == "") ? "Null" : ((Control)(object)txtTaxOfficeName).Text, (((Control)(object)txtLicenseNo).Text == "") ? "Null" : ((Control)(object)txtLicenseNo).Text, (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), (((Control)(object)txtBuildingNumber).Text == "") ? "Null" : ((Control)(object)txtBuildingNumber).Text, (((Control)(object)txtAddress).Text == "") ? "Null" : ((Control)(object)txtAddress).Text, (((Control)(object)txtTel).Text == "") ? "Null" : ((Control)(object)txtTel).Text, (((Control)(object)txtFax).Text == "") ? "Null" : ((Control)(object)txtFax).Text, (((Control)(object)txtEMail).Text == "") ? "Null" : ((Control)(object)txtEMail).Text, (((Control)(object)txtEINVBranchCode).Text == "") ? "Null" : ((Control)(object)txtEINVBranchCode).Text, (cboEINVTaxActivityType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboEINVTaxActivityType).Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString().Equals("True") ? "1" : "0", GlobalVariables.UserID, IsFromServer: true);
	}

	public override void DeleteData()
	{
		Branches.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["BranchID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
		ClearControls();
	}

	private void cboBranchAccount_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			((TextEditorControlBase)cboBranchAccount).Value = SearchFunctions.Accounts(IsFromServer: true);
		}
	}

	private void btnBranchAccountSearch_Click(object sender, EventArgs e)
	{
		((TextEditorControlBase)cboBranchAccount).Value = SearchFunctions.Accounts(IsFromServer: true);
	}

	private void cboBranchAccount_ValueChanged(object sender, EventArgs e)
	{
		if (cboBranchAccount.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtSubAccounts);
			dataView.RowFilter = "AccountID=" + ((TextEditorControlBase)cboBranchAccount).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboBranchSubAccount.DataSource = dataView;
		}
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

	private void btnBranchSubAccountSearch_Click(object sender, EventArgs e)
	{
		if (cboBranchAccount.SelectedIndex != -1)
		{
			int num = SearchFunctions.SubAccounts(((TextEditorControlBase)cboBranchAccount).Value.ToString(), IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboBranchSubAccount).Value = num;
			}
		}
	}

	private void cboBranchSubAccount_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119 && cboBranchAccount.SelectedIndex != -1)
		{
			int num = SearchFunctions.SubAccounts(((TextEditorControlBase)cboBranchAccount).Value.ToString(), IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboBranchSubAccount).Value = num;
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
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Expected O, but got Unknown
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Expected O, but got Unknown
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Expected O, but got Unknown
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Expected O, but got Unknown
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Expected O, but got Unknown
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Expected O, but got Unknown
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Expected O, but got Unknown
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Expected O, but got Unknown
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Expected O, but got Unknown
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Expected O, but got Unknown
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Expected O, but got Unknown
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Expected O, but got Unknown
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Expected O, but got Unknown
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Expected O, but got Unknown
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Expected O, but got Unknown
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Expected O, but got Unknown
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Expected O, but got Unknown
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Expected O, but got Unknown
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Expected O, but got Unknown
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Expected O, but got Unknown
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Expected O, but got Unknown
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Expected O, but got Unknown
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Expected O, but got Unknown
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Expected O, but got Unknown
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Expected O, but got Unknown
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Expected O, but got Unknown
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Expected O, but got Unknown
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Expected O, but got Unknown
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Expected O, but got Unknown
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Expected O, but got Unknown
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Expected O, but got Unknown
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Expected O, but got Unknown
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Expected O, but got Unknown
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Expected O, but got Unknown
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Expected O, but got Unknown
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Expected O, but got Unknown
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Expected O, but got Unknown
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmBranches));
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
		this.lblBranchEnglishName = new UltraLabel();
		this.txtBranchEnglishName = new UltraTextEditor();
		this.lblBranchArabicName = new UltraLabel();
		this.txtBranchArabicName = new UltraTextEditor();
		this.lblBranchCode = new UltraLabel();
		this.txtBranchCode = new UltraTextEditor();
		this.chkIsMainBranch = new UltraCheckEditor();
		this.btnBranchSubAccountSearch = new UltraButton();
		this.cboBranchSubAccount = new UltraComboEditor();
		this.lblBranchSubAccount = new UltraLabel();
		this.btnBranchAccountSearch = new UltraButton();
		this.cboBranchAccount = new UltraComboEditor();
		this.lblAccountName = new UltraLabel();
		this.txtTaxNo = new UltraTextEditor();
		this.lblTaxNo = new UltraLabel();
		this.txtCommercialRegistrationNo = new UltraTextEditor();
		this.lblCommercialRegistrationNo = new UltraLabel();
		this.txtLicenseNo = new UltraTextEditor();
		this.lblLicenseNo = new UltraLabel();
		this.cboCity = new UltraComboEditor();
		this.lblCity = new UltraLabel();
		this.txtAddress = new UltraTextEditor();
		this.lblAddress = new UltraLabel();
		this.txtTel = new UltraTextEditor();
		this.txtEMail = new UltraTextEditor();
		this.lblMax = new UltraLabel();
		this.lblEMail = new UltraLabel();
		this.txtFax = new UltraTextEditor();
		this.lblMin = new UltraLabel();
		this.lblTaxFileNo = new UltraLabel();
		this.txtTaxFileNo = new UltraTextEditor();
		this.lblTaxOfficeName = new UltraLabel();
		this.txtTaxOfficeName = new UltraTextEditor();
		this.lblEINVBranchCode = new UltraLabel();
		this.txtEINVBranchCode = new UltraTextEditor();
		this.txtBuildingNumber = new UltraTextEditor();
		this.lblBuildingNumber = new UltraLabel();
		this.cboArea = new UltraComboEditor();
		this.lblArea = new UltraLabel();
		this.cboEINVTaxActivityType = new UltraComboEditor();
		this.lblEINVTaxActivityType = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBranchEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBranchArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBranchCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsMainBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranchSubAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranchAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCommercialRegistrationNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLicenseNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxFileNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxOfficeName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEINVBranchCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBuildingNumber).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboEINVTaxActivityType).BeginInit();
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
		resources.ApplyResources(val8, "appearance8");
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
		resources.ApplyResources(this.lblBranchEnglishName, "lblBranchEnglishName");
		this.lblBranchEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBranchEnglishName).Name = "lblBranchEnglishName";
		((ControlBase)this.lblBranchEnglishName).WrapText = false;
		resources.ApplyResources(this.txtBranchEnglishName, "txtBranchEnglishName");
		resources.ApplyResources(val10, "appearance10");
		((TextEditorControlBase)this.txtBranchEnglishName).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.txtBranchEnglishName).Name = "txtBranchEnglishName";
		resources.ApplyResources(this.lblBranchArabicName, "lblBranchArabicName");
		this.lblBranchArabicName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBranchArabicName).Name = "lblBranchArabicName";
		((ControlBase)this.lblBranchArabicName).WrapText = false;
		resources.ApplyResources(this.txtBranchArabicName, "txtBranchArabicName");
		((System.Windows.Forms.Control)(object)this.txtBranchArabicName).Name = "txtBranchArabicName";
		resources.ApplyResources(this.lblBranchCode, "lblBranchCode");
		this.lblBranchCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBranchCode).Name = "lblBranchCode";
		((ControlBase)this.lblBranchCode).WrapText = false;
		resources.ApplyResources(this.txtBranchCode, "txtBranchCode");
		((System.Windows.Forms.Control)(object)this.txtBranchCode).Name = "txtBranchCode";
		resources.ApplyResources(this.chkIsMainBranch, "chkIsMainBranch");
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance11");
		((UltraToggleEditorBase)this.chkIsMainBranch).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.chkIsMainBranch).Name = "chkIsMainBranch";
		resources.ApplyResources(this.btnBranchSubAccountSearch, "btnBranchSubAccountSearch");
		((AppearanceBase)val12).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.btnBranchSubAccountSearch).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.btnBranchSubAccountSearch).Name = "btnBranchSubAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnBranchSubAccountSearch).Click += new System.EventHandler(btnBranchSubAccountSearch_Click);
		resources.ApplyResources(this.cboBranchSubAccount, "cboBranchSubAccount");
		this.cboBranchSubAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboBranchSubAccount).Name = "cboBranchSubAccount";
		((System.Windows.Forms.Control)(object)this.cboBranchSubAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(cboBranchSubAccount_KeyDown);
		resources.ApplyResources(this.lblBranchSubAccount, "lblBranchSubAccount");
		this.lblBranchSubAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBranchSubAccount).Name = "lblBranchSubAccount";
		((ControlBase)this.lblBranchSubAccount).WrapText = false;
		resources.ApplyResources(this.btnBranchAccountSearch, "btnBranchAccountSearch");
		((AppearanceBase)val13).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val13, "appearance13");
		((ControlBase)this.btnBranchAccountSearch).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.btnBranchAccountSearch).Name = "btnBranchAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnBranchAccountSearch).Click += new System.EventHandler(btnBranchAccountSearch_Click);
		resources.ApplyResources(this.cboBranchAccount, "cboBranchAccount");
		this.cboBranchAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboBranchAccount).Name = "cboBranchAccount";
		((TextEditorControlBase)this.cboBranchAccount).ValueChanged += new System.EventHandler(cboBranchAccount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboBranchAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(cboBranchAccount_KeyDown);
		resources.ApplyResources(this.lblAccountName, "lblAccountName");
		this.lblAccountName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAccountName).Name = "lblAccountName";
		((ControlBase)this.lblAccountName).WrapText = false;
		resources.ApplyResources(this.txtTaxNo, "txtTaxNo");
		((System.Windows.Forms.Control)(object)this.txtTaxNo).Name = "txtTaxNo";
		resources.ApplyResources(this.lblTaxNo, "lblTaxNo");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val14, "appearance14");
		((ControlBase)this.lblTaxNo).Appearance = (AppearanceBase)(object)val14;
		this.lblTaxNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaxNo).Name = "lblTaxNo";
		((ControlBase)this.lblTaxNo).WrapText = false;
		resources.ApplyResources(this.txtCommercialRegistrationNo, "txtCommercialRegistrationNo");
		((System.Windows.Forms.Control)(object)this.txtCommercialRegistrationNo).Name = "txtCommercialRegistrationNo";
		resources.ApplyResources(this.lblCommercialRegistrationNo, "lblCommercialRegistrationNo");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val15, "appearance15");
		((ControlBase)this.lblCommercialRegistrationNo).Appearance = (AppearanceBase)(object)val15;
		this.lblCommercialRegistrationNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCommercialRegistrationNo).Name = "lblCommercialRegistrationNo";
		((ControlBase)this.lblCommercialRegistrationNo).WrapText = false;
		resources.ApplyResources(this.txtLicenseNo, "txtLicenseNo");
		((System.Windows.Forms.Control)(object)this.txtLicenseNo).Name = "txtLicenseNo";
		resources.ApplyResources(this.lblLicenseNo, "lblLicenseNo");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val16, "appearance16");
		((ControlBase)this.lblLicenseNo).Appearance = (AppearanceBase)(object)val16;
		this.lblLicenseNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLicenseNo).Name = "lblLicenseNo";
		((ControlBase)this.lblLicenseNo).WrapText = false;
		resources.ApplyResources(this.cboCity, "cboCity");
		((System.Windows.Forms.Control)(object)this.cboCity).Name = "cboCity";
		((TextEditorControlBase)this.cboCity).ValueChanged += new System.EventHandler(cboCity_ValueChanged);
		resources.ApplyResources(this.lblCity, "lblCity");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val17, "appearance17");
		((ControlBase)this.lblCity).Appearance = (AppearanceBase)(object)val17;
		this.lblCity.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCity).Name = "lblCity";
		((ControlBase)this.lblCity).WrapText = false;
		resources.ApplyResources(this.txtAddress, "txtAddress");
		((System.Windows.Forms.Control)(object)this.txtAddress).Name = "txtAddress";
		resources.ApplyResources(this.lblAddress, "lblAddress");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val18).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val18, "appearance18");
		((ControlBase)this.lblAddress).Appearance = (AppearanceBase)(object)val18;
		this.lblAddress.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAddress).Name = "lblAddress";
		((ControlBase)this.lblAddress).WrapText = false;
		resources.ApplyResources(this.txtTel, "txtTel");
		((System.Windows.Forms.Control)(object)this.txtTel).Name = "txtTel";
		resources.ApplyResources(this.txtEMail, "txtEMail");
		((System.Windows.Forms.Control)(object)this.txtEMail).Name = "txtEMail";
		resources.ApplyResources(this.lblMax, "lblMax");
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val19, "appearance19");
		((ControlBase)this.lblMax).Appearance = (AppearanceBase)(object)val19;
		this.lblMax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMax).Name = "lblMax";
		((ControlBase)this.lblMax).WrapText = false;
		resources.ApplyResources(this.lblEMail, "lblEMail");
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val20, "appearance20");
		((ControlBase)this.lblEMail).Appearance = (AppearanceBase)(object)val20;
		this.lblEMail.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEMail).Name = "lblEMail";
		((ControlBase)this.lblEMail).WrapText = false;
		resources.ApplyResources(this.txtFax, "txtFax");
		((System.Windows.Forms.Control)(object)this.txtFax).Name = "txtFax";
		resources.ApplyResources(this.lblMin, "lblMin");
		((AppearanceBase)val21).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val21).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val21, "appearance21");
		((ControlBase)this.lblMin).Appearance = (AppearanceBase)(object)val21;
		this.lblMin.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMin).Name = "lblMin";
		((ControlBase)this.lblMin).WrapText = false;
		resources.ApplyResources(this.lblTaxFileNo, "lblTaxFileNo");
		((AppearanceBase)val22).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val22).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val22, "appearance22");
		((ControlBase)this.lblTaxFileNo).Appearance = (AppearanceBase)(object)val22;
		this.lblTaxFileNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaxFileNo).Name = "lblTaxFileNo";
		((ControlBase)this.lblTaxFileNo).WrapText = false;
		resources.ApplyResources(this.txtTaxFileNo, "txtTaxFileNo");
		((System.Windows.Forms.Control)(object)this.txtTaxFileNo).Name = "txtTaxFileNo";
		resources.ApplyResources(this.lblTaxOfficeName, "lblTaxOfficeName");
		((AppearanceBase)val23).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val23).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val23, "appearance23");
		((ControlBase)this.lblTaxOfficeName).Appearance = (AppearanceBase)(object)val23;
		this.lblTaxOfficeName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaxOfficeName).Name = "lblTaxOfficeName";
		((ControlBase)this.lblTaxOfficeName).WrapText = false;
		resources.ApplyResources(this.txtTaxOfficeName, "txtTaxOfficeName");
		((System.Windows.Forms.Control)(object)this.txtTaxOfficeName).Name = "txtTaxOfficeName";
		resources.ApplyResources(this.lblEINVBranchCode, "lblEINVBranchCode");
		((AppearanceBase)val24).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val24).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val24, "appearance24");
		((ControlBase)this.lblEINVBranchCode).Appearance = (AppearanceBase)(object)val24;
		this.lblEINVBranchCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEINVBranchCode).Name = "lblEINVBranchCode";
		((ControlBase)this.lblEINVBranchCode).WrapText = false;
		resources.ApplyResources(this.txtEINVBranchCode, "txtEINVBranchCode");
		((System.Windows.Forms.Control)(object)this.txtEINVBranchCode).Name = "txtEINVBranchCode";
		resources.ApplyResources(this.txtBuildingNumber, "txtBuildingNumber");
		((System.Windows.Forms.Control)(object)this.txtBuildingNumber).Name = "txtBuildingNumber";
		resources.ApplyResources(this.lblBuildingNumber, "lblBuildingNumber");
		((AppearanceBase)val25).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val25).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val25, "appearance25");
		((ControlBase)this.lblBuildingNumber).Appearance = (AppearanceBase)(object)val25;
		this.lblBuildingNumber.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBuildingNumber).Name = "lblBuildingNumber";
		((ControlBase)this.lblBuildingNumber).WrapText = false;
		resources.ApplyResources(this.cboArea, "cboArea");
		((System.Windows.Forms.Control)(object)this.cboArea).Name = "cboArea";
		resources.ApplyResources(this.lblArea, "lblArea");
		((AppearanceBase)val26).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val26).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val26, "appearance26");
		((ControlBase)this.lblArea).Appearance = (AppearanceBase)(object)val26;
		this.lblArea.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArea).Name = "lblArea";
		((ControlBase)this.lblArea).WrapText = false;
		resources.ApplyResources(this.cboEINVTaxActivityType, "cboEINVTaxActivityType");
		((System.Windows.Forms.Control)(object)this.cboEINVTaxActivityType).Name = "cboEINVTaxActivityType";
		resources.ApplyResources(this.lblEINVTaxActivityType, "lblEINVTaxActivityType");
		((AppearanceBase)val27).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val27).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val27, "appearance27");
		((ControlBase)this.lblEINVTaxActivityType).Appearance = (AppearanceBase)(object)val27;
		this.lblEINVTaxActivityType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEINVTaxActivityType).Name = "lblEINVTaxActivityType";
		((ControlBase)this.lblEINVTaxActivityType).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboEINVTaxActivityType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEINVTaxActivityType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBuildingNumber);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBuildingNumber);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboArea);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArea);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEINVBranchCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEMail);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEINVBranchCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEMail);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtFax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMin);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAddress);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAddress);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCity);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCity);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtLicenseNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLicenseNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxOfficeName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxFileNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxOfficeName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxFileNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCommercialRegistrationNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCommercialRegistrationNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnBranchSubAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBranchSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBranchSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnBranchAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBranchAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAccountName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsMainBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBranchCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBranchCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBranchEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBranchEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBranchArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBranchArabicName);
		base.Name = "frmBranches";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBranchArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBranchArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBranchEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBranchEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBranchCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBranchCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsMainBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAccountName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBranchAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnBranchAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBranchSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBranchSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnBranchSubAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCommercialRegistrationNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCommercialRegistrationNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaxNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTaxNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaxFileNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaxOfficeName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTaxFileNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTaxOfficeName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLicenseNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtLicenseNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCity, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCity, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAddress, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAddress, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMin, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtFax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEMail, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEINVBranchCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEMail, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEINVBranchCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArea, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboArea, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBuildingNumber, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBuildingNumber, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEINVTaxActivityType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboEINVTaxActivityType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
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
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBranchEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBranchArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBranchCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsMainBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranchSubAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranchAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCommercialRegistrationNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLicenseNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxFileNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxOfficeName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEINVBranchCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBuildingNumber).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboEINVTaxActivityType).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
