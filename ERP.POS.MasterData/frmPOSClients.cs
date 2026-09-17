using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.HR;
using BusinessLayer.POS;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Accounting.MasterData;
using ERP.Classes;
using ERP.Properties;
using ERP.StockControl.MasterData;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.POS.MasterData;

public class frmPOSClients : frmButtons
{
	private DataTable dtGender;

	private DataTable dtBranches;

	private DataTable dtCountries;

	private DataTable dtCities;

	private DataTable dtAreas;

	private DataTable dtReligions;

	private DataTable dtSubAccounts;

	private DataTable dtPriceType;

	private DataRow drMaster;

	private string CurrentBranchCode = "";

	private bool CanEditVoucherNumber = true;

	private decimal Client = default(decimal);

	public decimal ClientID = default(decimal);

	public string ClientName = "";

	private IContainer components = null;

	public UltraTextEditor txtCode;

	public UltraButton btnSearch;

	public UltraButton btnPriveous;

	public UltraButton btnNext;

	public UltraLabel lblTitle;

	public UltraLabel lblTitle2;

	public UltraLabel lblCode;

	public UltraButton btnCopyTo;

	private UltraTextEditor txtNameEn;

	private UltraLabel lblNameEn;

	private UltraTextEditor txtNameAr;

	private UltraLabel lblNameAr;

	private UltraDateTimeEditor dtpBirthDate;

	private UltraLabel lblBirthDate;

	private UltraComboEditor cboGender;

	private UltraLabel lblGender;

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

	private UltraTextEditor txtMobileNo1;

	private UltraLabel lblMobileNo1;

	private UltraTextEditor txtTel;

	private UltraLabel lblTel;

	private UltraTextEditor txtEMail;

	private UltraLabel lblEMail;

	private UltraTextEditor txtNotes;

	private UltraLabel lblNotes;

	private UltraTextEditor txtMobileNo2;

	private UltraLabel lblMobileNo2;

	private UltraCheckEditor chkIsActive;

	private UltraComboEditor cboReligion;

	private UltraLabel lblReligion;

	private UltraComboEditor cboSubAccount;

	private UltraLabel lblSubAccount;

	private UltraComboEditor cboPriceType;

	private UltraLabel lblPriceType;

	private UltraTextEditor txtDiscountPercentage;

	private UltraLabel lblDiscountPercentage;

	public UltraButton btnPriceTypeSearch;

	public UltraButton btnSubAccountSearch;

	private UltraLabel lblCardNo;

	private UltraTextEditor txtCardNo;

	private UltraLabel lblClientBarcode;

	private UltraTextEditor txtClientBarcode;

	private UltraCheckEditor chkForAllBranch;

	public frmPOSClients()
	{
		InitializeComponent();
		TableName = "POS_Clients";
		NoCol = "ClientCode";
		IDCol = "ClientID";
		DateCol = "GetDate()";
	}

	public frmPOSClients(decimal ClientID)
		: this()
	{
		Client = ClientID;
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
		((EditorButtonControlBase)txtClientBarcode).ReadOnly = NavMode;
		((Control)(object)btnSearch).Visible = NavMode;
		((Control)(object)btnNext).Visible = NavMode;
		((Control)(object)btnCopyTo).Visible = NavMode;
		((Control)(object)btnPriveous).Visible = NavMode;
		((EditorButtonControlBase)txtNameAr).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNameEn).ReadOnly = NavMode;
		((EditorButtonControlBase)cboGender).ReadOnly = NavMode;
		((EditorButtonControlBase)cboReligion).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPriceType).ReadOnly = NavMode || !CanModifyPriceType;
		((EditorButtonControlBase)txtDiscountPercentage).ReadOnly = NavMode || !CanModifyPriceType;
		((EditorButtonControlBase)cboSubAccount).ReadOnly = !CanModifySubAccount;
		((EditorButtonControlBase)txtMobileNo1).ReadOnly = NavMode;
		((EditorButtonControlBase)txtMobileNo2).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTel).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCardNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEMail).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPersonalIDNo).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpBirthDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCountry).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCity).ReadOnly = NavMode;
		((EditorButtonControlBase)cboArea).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAddress).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)chkIsActive).Enabled = !NavMode;
		((Control)(object)btnPriceTypeSearch).Visible = !NavMode && CanModifyPriceType;
		((Control)(object)btnSubAccountSearch).Visible = !NavMode && CanModifySubAccount;
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
		dtGender = Gender.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboGender, dtGender, "GenderID", "GenderName");
		dtBranches = Branches.Select(GlobalVariables.CurrentBranchID, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		CurrentBranchCode = dtBranches.Rows[0]["BranchCode"].ToString();
		dtReligions = Religions.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboReligion, dtReligions, "ReligionID", "ReligionName");
		dtCountries = Countries.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCountry, dtCountries, "CountryID", "CountryName");
		dtCities = Cities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCity, dtCities, "CityID", "CityName");
		dtAreas = Areas.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboArea, dtAreas, "AreaID", "AreaName");
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
		dtSubAccounts = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSubAccount, dtSubAccounts, "SubAccountID", "SubAccountName");
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		((Control)(object)txtCode).Text = (Adding ? Clients.GetCode(IsFromServer: false) : "");
		((Control)(object)txtClientBarcode).Text = CurrentBranchCode + "-" + ((Control)(object)txtCode).Text;
		((TextEditorControlBase)txtNameAr).Clear();
		((TextEditorControlBase)txtNameEn).Clear();
		cboGender.SelectedIndex = -1;
		cboReligion.SelectedIndex = -1;
		cboPriceType.SelectedIndex = ((dtPriceType.Rows.Count <= 0) ? (-1) : 0);
		cboSubAccount.SelectedIndex = -1;
		((Control)(object)txtDiscountPercentage).Text = "0";
		((TextEditorControlBase)txtMobileNo1).Clear();
		((TextEditorControlBase)txtMobileNo2).Clear();
		((TextEditorControlBase)txtTel).Clear();
		((TextEditorControlBase)txtCardNo).Clear();
		((TextEditorControlBase)txtEMail).Clear();
		((TextEditorControlBase)txtPersonalIDNo).Clear();
		dtpBirthDate.DateTime = DateTime.Today;
		cboCountry.SelectedIndex = -1;
		cboCity.SelectedIndex = -1;
		cboArea.SelectedIndex = -1;
		((TextEditorControlBase)txtAddress).Clear();
		((TextEditorControlBase)txtNotes).Clear();
		((UltraToggleEditorBase)chkIsActive).Checked = true;
	}

	public override void FillData()
	{
		if (Client == -1m)
		{
			drMaster = null;
			btnAddClick();
			((Control)(object)btnOK).Visible = false;
		}
		else if (Client > 0m || Client < -1m)
		{
			DataTable dataTable = Clients.Select(Client.ToString(), GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
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
			DataTable dataTable2 = Clients.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
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
			((TextEditorControlBase)cboTransactionBranch).Value = drMaster["BranchID"];
			((Control)(object)txtCode).Text = drMaster["ClientCode"].ToString();
			((Control)(object)txtClientBarcode).Text = drMaster["ClientBarcode"].ToString();
			((Control)(object)txtNameAr).Text = drMaster["ClientNameAr"].ToString();
			((Control)(object)txtNameEn).Text = drMaster["ClientNameEn"].ToString();
			((TextEditorControlBase)cboGender).Value = drMaster["GenderID"];
			((TextEditorControlBase)cboReligion).Value = drMaster["ReligionID"];
			((Control)(object)txtDiscountPercentage).Text = drMaster["DiscountPercentage"].ToString();
			((TextEditorControlBase)cboPriceType).Value = drMaster["PriceTypeID"];
			((TextEditorControlBase)cboSubAccount).Value = drMaster["SubAccountID"];
			((Control)(object)txtMobileNo1).Text = drMaster["MobileNumber"].ToString();
			((Control)(object)txtMobileNo2).Text = drMaster["MobileNumber2"].ToString();
			((Control)(object)txtTel).Text = drMaster["PhoneNumber"].ToString();
			((Control)(object)txtCardNo).Text = drMaster["CardNo"].ToString();
			((Control)(object)txtEMail).Text = drMaster["EMail"].ToString();
			((Control)(object)txtPersonalIDNo).Text = drMaster["IDNumber"].ToString();
			dtpBirthDate.Value = drMaster["BirthDate"];
			((UltraToggleEditorBase)chkIsActive).Checked = Convert.ToBoolean(drMaster["IsActive"]);
			((TextEditorControlBase)cboCountry).Value = drMaster["CountryID"];
			((TextEditorControlBase)cboCity).Value = drMaster["CityID"];
			((TextEditorControlBase)cboArea).Value = drMaster["AreaID"];
			((Control)(object)txtAddress).Text = drMaster["Address"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
		}
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال رقم العميل", "Please Enter The Client Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtNameAr).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال الاسم بالعربية", "Please Enter The Client Arabic Name");
			((TextEditorControlBase)txtNameAr).Focus();
			return false;
		}
		if (((Control)(object)txtMobileNo1).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال رقم محمول 1", "Please Enter Mobile Number1");
			((TextEditorControlBase)txtMobileNo1).Focus();
			return false;
		}
		if (Main.CheckForValue("POS_Clients", "ClientNameAr", ((Control)(object)txtNameAr).Text, Adding ? "0" : drMaster["ClientNameAr"].ToString(), IsFromServer: false) > 0)
		{
			GlobalVariables.InformationMB.Show("هذا الاسم متواجد من قبل", "Client Name Already Exists");
			((TextEditorControlBase)txtNameAr).Focus();
			return false;
		}
		if (((Control)(object)txtClientBarcode).Text.Trim() != "")
		{
			if (Main.CheckForValue("POS_Clients", "ClientBarcode", ((Control)(object)txtClientBarcode).Text, Adding ? "0" : drMaster["ClientBarcode"].ToString(), IsFromServer: false) > 0)
			{
				GlobalVariables.InformationMB.Show("هذا الكود متواجد من قبل", "Client Barcode Already Exists");
				((TextEditorControlBase)txtClientBarcode).Focus();
				return false;
			}
		}
		else if (Main.CheckForValue("dbo.POS_Clients", "ClientCode", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["ClientCode"].ToString(), IsFromServer: false) > 0)
		{
			string code = Clients.GetCode(IsFromServer: false);
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
			ClientID = Clients.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtNameAr).Text, (((Control)(object)txtNameEn).Text == "") ? "Null" : ((Control)(object)txtNameEn).Text, (((Control)(object)txtClientBarcode).Text == "") ? "Null" : ((Control)(object)txtClientBarcode).Text, (cboGender.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboGender).Value.ToString(), (cboReligion.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboReligion).Value.ToString(), ((Control)(object)txtTel).Text, ((Control)(object)txtMobileNo1).Text, ((Control)(object)txtMobileNo2).Text, ((Control)(object)txtEMail).Text, (((Control)(object)txtPersonalIDNo).Text == "") ? "Null" : ((Control)(object)txtPersonalIDNo).Text, (dtpBirthDate.Value == null) ? "Null" : dtpBirthDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboCountry.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCountry).Value.ToString(), (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), ((Control)(object)txtAddress).Text, ((Control)(object)txtNotes).Text, (((Control)(object)txtDiscountPercentage).Text == "") ? "0" : ((Control)(object)txtDiscountPercentage).Text, (cboPriceType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), (cboSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccount).Value.ToString(), ((Control)(object)txtCardNo).Text, ((UltraToggleEditorBase)chkIsActive).Checked ? "1" : "0", "1", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
			ClientName = (GlobalVariables.IsArabic ? ((Control)(object)txtNameAr).Text : ((Control)(object)txtNameEn).Text);
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
			ClientID = Clients.Insert_Update(drMaster["ClientID"].ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtNameAr).Text, (((Control)(object)txtNameEn).Text == "") ? "Null" : ((Control)(object)txtNameEn).Text, (((Control)(object)txtClientBarcode).Text == "") ? "Null" : ((Control)(object)txtClientBarcode).Text, (cboGender.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboGender).Value.ToString(), (cboReligion.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboReligion).Value.ToString(), ((Control)(object)txtTel).Text, ((Control)(object)txtMobileNo1).Text, ((Control)(object)txtMobileNo2).Text, ((Control)(object)txtEMail).Text, (((Control)(object)txtPersonalIDNo).Text == "") ? "Null" : ((Control)(object)txtPersonalIDNo).Text, (dtpBirthDate.Value == null) ? "Null" : dtpBirthDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboCountry.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCountry).Value.ToString(), (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), ((Control)(object)txtAddress).Text, ((Control)(object)txtNotes).Text, (((Control)(object)txtDiscountPercentage).Text == "") ? "0" : ((Control)(object)txtDiscountPercentage).Text, (cboPriceType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), (cboSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccount).Value.ToString(), ((Control)(object)txtCardNo).Text, ((UltraToggleEditorBase)chkIsActive).Checked ? "1" : "0", "1", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
			ClientName = (GlobalVariables.IsArabic ? ((Control)(object)txtNameAr).Text : ((Control)(object)txtNameEn).Text);
		}
		catch
		{
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
		}
	}

	public override void DeleteData()
	{
		Clients.Delete(drMaster["ClientID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
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
		dtGender = Gender.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboGender, dtGender, "GenderID", "GenderName");
		dtReligions = Religions.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboReligion, dtReligions, "ReligionID", "ReligionName");
		dtCountries = Countries.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCountry, dtCountries, "CountryID", "CountryName");
		dtCities = Cities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCity, dtCities, "CityID", "CityName");
		dtAreas = Areas.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboArea, dtAreas, "AreaID", "AreaName");
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
		dtSubAccounts = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSubAccount, dtSubAccounts, "SubAccountID", "SubAccountName");
	}

	public virtual void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.POSClientsSearchReport("-1", "-1", IsFromServer: false);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["ClientID"].ToString();
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

	private void txtDiscountPercentage_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
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

	public override void btnPrintClick()
	{
		GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_POS_Clients_A_nologo.rpt" : "Rep_POS_Clients_E_nologo.rpt"));
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@ClientIDs", "-1");
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
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Expected O, but got Unknown
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Expected O, but got Unknown
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Expected O, but got Unknown
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Expected O, but got Unknown
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Expected O, but got Unknown
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Expected O, but got Unknown
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Expected O, but got Unknown
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Expected O, but got Unknown
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Expected O, but got Unknown
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Expected O, but got Unknown
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Expected O, but got Unknown
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Expected O, but got Unknown
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Expected O, but got Unknown
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Expected O, but got Unknown
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Expected O, but got Unknown
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Expected O, but got Unknown
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Expected O, but got Unknown
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Expected O, but got Unknown
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Expected O, but got Unknown
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Expected O, but got Unknown
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Expected O, but got Unknown
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Expected O, but got Unknown
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Expected O, but got Unknown
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Expected O, but got Unknown
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Expected O, but got Unknown
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Expected O, but got Unknown
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Expected O, but got Unknown
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Expected O, but got Unknown
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Expected O, but got Unknown
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Expected O, but got Unknown
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Expected O, but got Unknown
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Expected O, but got Unknown
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Expected O, but got Unknown
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Expected O, but got Unknown
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Expected O, but got Unknown
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Expected O, but got Unknown
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Expected O, but got Unknown
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Expected O, but got Unknown
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Expected O, but got Unknown
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Expected O, but got Unknown
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Expected O, but got Unknown
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Expected O, but got Unknown
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Expected O, but got Unknown
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Expected O, but got Unknown
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Expected O, but got Unknown
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Expected O, but got Unknown
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Expected O, but got Unknown
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Expected O, but got Unknown
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Expected O, but got Unknown
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.MasterData.frmPOSClients));
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
		this.txtCode = new UltraTextEditor();
		this.btnSearch = new UltraButton();
		this.btnPriveous = new UltraButton();
		this.btnNext = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.lblTitle2 = new UltraLabel();
		this.lblCode = new UltraLabel();
		this.btnCopyTo = new UltraButton();
		this.txtNameEn = new UltraTextEditor();
		this.lblNameEn = new UltraLabel();
		this.txtNameAr = new UltraTextEditor();
		this.lblNameAr = new UltraLabel();
		this.dtpBirthDate = new UltraDateTimeEditor();
		this.lblBirthDate = new UltraLabel();
		this.cboGender = new UltraComboEditor();
		this.lblGender = new UltraLabel();
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
		this.txtMobileNo1 = new UltraTextEditor();
		this.lblMobileNo1 = new UltraLabel();
		this.txtTel = new UltraTextEditor();
		this.lblTel = new UltraLabel();
		this.txtEMail = new UltraTextEditor();
		this.lblEMail = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblNotes = new UltraLabel();
		this.txtMobileNo2 = new UltraTextEditor();
		this.lblMobileNo2 = new UltraLabel();
		this.chkIsActive = new UltraCheckEditor();
		this.cboReligion = new UltraComboEditor();
		this.lblReligion = new UltraLabel();
		this.cboSubAccount = new UltraComboEditor();
		this.lblSubAccount = new UltraLabel();
		this.cboPriceType = new UltraComboEditor();
		this.lblPriceType = new UltraLabel();
		this.txtDiscountPercentage = new UltraTextEditor();
		this.lblDiscountPercentage = new UltraLabel();
		this.btnPriceTypeSearch = new UltraButton();
		this.btnSubAccountSearch = new UltraButton();
		this.lblCardNo = new UltraLabel();
		this.txtCardNo = new UltraTextEditor();
		this.lblClientBarcode = new UltraLabel();
		this.txtClientBarcode = new UltraTextEditor();
		this.chkForAllBranch = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBirthDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboGender).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPersonalIDNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobileNo1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobileNo2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboReligion).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCardNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientBarcode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkForAllBranch).BeginInit();
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
		((System.Windows.Forms.Control)(object)this.txtCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtCode_KeyUp);
		resources.ApplyResources(this.btnSearch, "btnSearch");
		((AppearanceBase)val3).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.btnSearch).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.btnSearch).Name = "btnSearch";
		((System.Windows.Forms.Control)(object)this.btnSearch).Click += new System.EventHandler(btnSearch_Click);
		resources.ApplyResources(this.btnPriveous, "btnPriveous");
		((AppearanceBase)val4).Image = ERP.Properties.Resources.BarLeft;
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.btnPriveous).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.btnPriveous).Name = "btnPriveous";
		((System.Windows.Forms.Control)(object)this.btnPriveous).Click += new System.EventHandler(btnPriveous_Click);
		resources.ApplyResources(this.btnNext, "btnNext");
		((AppearanceBase)val5).Image = ERP.Properties.Resources.BarRight;
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.btnNext).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.btnNext).Name = "btnNext";
		((System.Windows.Forms.Control)(object)this.btnNext).Click += new System.EventHandler(btnNext_Click);
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
		((System.Windows.Forms.Control)(object)this.btnCopyTo).Click += new System.EventHandler(btnCopyTo_Click);
		resources.ApplyResources(this.txtNameEn, "txtNameEn");
		((System.Windows.Forms.Control)(object)this.txtNameEn).Name = "txtNameEn";
		resources.ApplyResources(this.lblNameEn, "lblNameEn");
		this.lblNameEn.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNameEn).Name = "lblNameEn";
		((ControlBase)this.lblNameEn).WrapText = false;
		resources.ApplyResources(this.txtNameAr, "txtNameAr");
		((System.Windows.Forms.Control)(object)this.txtNameAr).Name = "txtNameAr";
		resources.ApplyResources(this.lblNameAr, "lblNameAr");
		this.lblNameAr.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNameAr).Name = "lblNameAr";
		((ControlBase)this.lblNameAr).WrapText = false;
		resources.ApplyResources(this.dtpBirthDate, "dtpBirthDate");
		((UltraWinEditorMaskedControlBase)this.dtpBirthDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpBirthDate).Name = "dtpBirthDate";
		resources.ApplyResources(this.lblBirthDate, "lblBirthDate");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.lblBirthDate).Appearance = (AppearanceBase)(object)val9;
		this.lblBirthDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBirthDate).Name = "lblBirthDate";
		((ControlBase)this.lblBirthDate).WrapText = false;
		resources.ApplyResources(this.cboGender, "cboGender");
		((System.Windows.Forms.Control)(object)this.cboGender).Name = "cboGender";
		resources.ApplyResources(this.lblGender, "lblGender");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.lblGender).Appearance = (AppearanceBase)(object)val10;
		this.lblGender.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGender).Name = "lblGender";
		((ControlBase)this.lblGender).WrapText = false;
		resources.ApplyResources(this.txtPersonalIDNo, "txtPersonalIDNo");
		((System.Windows.Forms.Control)(object)this.txtPersonalIDNo).Name = "txtPersonalIDNo";
		((System.Windows.Forms.Control)(object)this.txtPersonalIDNo).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPersonalIDNo_KeyPress);
		resources.ApplyResources(this.lblPersonalIDNo, "lblPersonalIDNo");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.lblPersonalIDNo).Appearance = (AppearanceBase)(object)val11;
		this.lblPersonalIDNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPersonalIDNo).Name = "lblPersonalIDNo";
		((ControlBase)this.lblPersonalIDNo).WrapText = false;
		resources.ApplyResources(this.cboCountry, "cboCountry");
		((System.Windows.Forms.Control)(object)this.cboCountry).Name = "cboCountry";
		resources.ApplyResources(this.lblCountry, "lblCountry");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.lblCountry).Appearance = (AppearanceBase)(object)val12;
		this.lblCountry.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCountry).Name = "lblCountry";
		((ControlBase)this.lblCountry).WrapText = false;
		resources.ApplyResources(this.cboArea, "cboArea");
		((System.Windows.Forms.Control)(object)this.cboArea).Name = "cboArea";
		resources.ApplyResources(this.lblArea, "lblArea");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val13, "appearance13");
		((ControlBase)this.lblArea).Appearance = (AppearanceBase)(object)val13;
		this.lblArea.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArea).Name = "lblArea";
		((ControlBase)this.lblArea).WrapText = false;
		resources.ApplyResources(this.cboCity, "cboCity");
		((System.Windows.Forms.Control)(object)this.cboCity).Name = "cboCity";
		((TextEditorControlBase)this.cboCity).ValueChanged += new System.EventHandler(cboCity_ValueChanged);
		resources.ApplyResources(this.lblCity, "lblCity");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val14, "appearance14");
		((ControlBase)this.lblCity).Appearance = (AppearanceBase)(object)val14;
		this.lblCity.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCity).Name = "lblCity";
		((ControlBase)this.lblCity).WrapText = false;
		resources.ApplyResources(this.txtAddress, "txtAddress");
		((System.Windows.Forms.Control)(object)this.txtAddress).Name = "txtAddress";
		resources.ApplyResources(this.lblAddress, "lblAddress");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val15, "appearance15");
		((ControlBase)this.lblAddress).Appearance = (AppearanceBase)(object)val15;
		this.lblAddress.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAddress).Name = "lblAddress";
		((ControlBase)this.lblAddress).WrapText = false;
		resources.ApplyResources(this.txtMobileNo1, "txtMobileNo1");
		((System.Windows.Forms.Control)(object)this.txtMobileNo1).Name = "txtMobileNo1";
		((System.Windows.Forms.Control)(object)this.txtMobileNo1).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPersonalIDNo_KeyPress);
		resources.ApplyResources(this.lblMobileNo1, "lblMobileNo1");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val16, "appearance16");
		((ControlBase)this.lblMobileNo1).Appearance = (AppearanceBase)(object)val16;
		this.lblMobileNo1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMobileNo1).Name = "lblMobileNo1";
		((ControlBase)this.lblMobileNo1).WrapText = false;
		resources.ApplyResources(this.txtTel, "txtTel");
		((System.Windows.Forms.Control)(object)this.txtTel).Name = "txtTel";
		resources.ApplyResources(this.lblTel, "lblTel");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val17, "appearance17");
		((ControlBase)this.lblTel).Appearance = (AppearanceBase)(object)val17;
		this.lblTel.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTel).Name = "lblTel";
		((ControlBase)this.lblTel).WrapText = false;
		resources.ApplyResources(this.txtEMail, "txtEMail");
		((System.Windows.Forms.Control)(object)this.txtEMail).Name = "txtEMail";
		resources.ApplyResources(this.lblEMail, "lblEMail");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val18).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val18, "appearance18");
		((ControlBase)this.lblEMail).Appearance = (AppearanceBase)(object)val18;
		this.lblEMail.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEMail).Name = "lblEMail";
		((ControlBase)this.lblEMail).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val19, "appearance19");
		((ControlBase)this.lblNotes).Appearance = (AppearanceBase)(object)val19;
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtMobileNo2, "txtMobileNo2");
		((System.Windows.Forms.Control)(object)this.txtMobileNo2).Name = "txtMobileNo2";
		((System.Windows.Forms.Control)(object)this.txtMobileNo2).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPersonalIDNo_KeyPress);
		resources.ApplyResources(this.lblMobileNo2, "lblMobileNo2");
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val20, "appearance20");
		((ControlBase)this.lblMobileNo2).Appearance = (AppearanceBase)(object)val20;
		this.lblMobileNo2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMobileNo2).Name = "lblMobileNo2";
		((ControlBase)this.lblMobileNo2).WrapText = false;
		resources.ApplyResources(this.chkIsActive, "chkIsActive");
		((AppearanceBase)val21).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val21, "appearance21");
		((UltraToggleEditorBase)this.chkIsActive).Appearance = (AppearanceBase)(object)val21;
		((UltraToggleEditorBase)this.chkIsActive).Checked = true;
		((UltraToggleEditorBase)this.chkIsActive).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsActive).Name = "chkIsActive";
		resources.ApplyResources(this.cboReligion, "cboReligion");
		((System.Windows.Forms.Control)(object)this.cboReligion).Name = "cboReligion";
		resources.ApplyResources(this.lblReligion, "lblReligion");
		((AppearanceBase)val22).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val22).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val22, "appearance22");
		((ControlBase)this.lblReligion).Appearance = (AppearanceBase)(object)val22;
		this.lblReligion.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReligion).Name = "lblReligion";
		((ControlBase)this.lblReligion).WrapText = false;
		resources.ApplyResources(this.cboSubAccount, "cboSubAccount");
		((System.Windows.Forms.Control)(object)this.cboSubAccount).Name = "cboSubAccount";
		resources.ApplyResources(this.lblSubAccount, "lblSubAccount");
		this.lblSubAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSubAccount).Name = "lblSubAccount";
		((ControlBase)this.lblSubAccount).WrapText = false;
		resources.ApplyResources(this.cboPriceType, "cboPriceType");
		((System.Windows.Forms.Control)(object)this.cboPriceType).Name = "cboPriceType";
		resources.ApplyResources(this.lblPriceType, "lblPriceType");
		this.lblPriceType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPriceType).Name = "lblPriceType";
		((ControlBase)this.lblPriceType).WrapText = false;
		resources.ApplyResources(this.txtDiscountPercentage, "txtDiscountPercentage");
		((System.Windows.Forms.Control)(object)this.txtDiscountPercentage).Name = "txtDiscountPercentage";
		((System.Windows.Forms.Control)(object)this.txtDiscountPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDiscountPercentage_KeyPress);
		resources.ApplyResources(this.lblDiscountPercentage, "lblDiscountPercentage");
		((AppearanceBase)val23).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val23).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val23, "appearance23");
		((ControlBase)this.lblDiscountPercentage).Appearance = (AppearanceBase)(object)val23;
		this.lblDiscountPercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscountPercentage).Name = "lblDiscountPercentage";
		((ControlBase)this.lblDiscountPercentage).WrapText = false;
		resources.ApplyResources(this.btnPriceTypeSearch, "btnPriceTypeSearch");
		((AppearanceBase)val24).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val24, "appearance24");
		((ControlBase)this.btnPriceTypeSearch).Appearance = (AppearanceBase)(object)val24;
		((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch).Name = "btnPriceTypeSearch";
		((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch).Click += new System.EventHandler(btnPriceTypeSearch_Click);
		resources.ApplyResources(this.btnSubAccountSearch, "btnSubAccountSearch");
		((AppearanceBase)val25).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val25, "appearance25");
		((ControlBase)this.btnSubAccountSearch).Appearance = (AppearanceBase)(object)val25;
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).Name = "btnSubAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).Click += new System.EventHandler(btnSubAccountSearch_Click);
		resources.ApplyResources(this.lblCardNo, "lblCardNo");
		((AppearanceBase)val26).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val26).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val26, "appearance26");
		((ControlBase)this.lblCardNo).Appearance = (AppearanceBase)(object)val26;
		this.lblCardNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCardNo).Name = "lblCardNo";
		((ControlBase)this.lblCardNo).WrapText = false;
		resources.ApplyResources(this.txtCardNo, "txtCardNo");
		((System.Windows.Forms.Control)(object)this.txtCardNo).Name = "txtCardNo";
		resources.ApplyResources(this.lblClientBarcode, "lblClientBarcode");
		((AppearanceBase)val27).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val27).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val27, "appearance27");
		((ControlBase)this.lblClientBarcode).Appearance = (AppearanceBase)(object)val27;
		this.lblClientBarcode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClientBarcode).Name = "lblClientBarcode";
		((ControlBase)this.lblClientBarcode).WrapText = false;
		resources.ApplyResources(this.txtClientBarcode, "txtClientBarcode");
		((System.Windows.Forms.Control)(object)this.txtClientBarcode).Name = "txtClientBarcode";
		resources.ApplyResources(this.chkForAllBranch, "chkForAllBranch");
		((AppearanceBase)val28).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val28, "appearance28");
		((UltraToggleEditorBase)this.chkForAllBranch).Appearance = (AppearanceBase)(object)val28;
		((UltraToggleEditorBase)this.chkForAllBranch).Checked = true;
		((UltraToggleEditorBase)this.chkForAllBranch).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkForAllBranch).Name = "chkForAllBranch";
		base.AcceptButton = (System.Windows.Forms.IButtonControl)base.btnAdd;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSubAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboReligion);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReligion);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkForAllBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsActive);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtMobileNo2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMobileNo2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEMail);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEMail);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCardNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCardNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtClientBarcode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClientBarcode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtMobileNo1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMobileNo1);
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
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboGender);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGender);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpBirthDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBirthDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNameAr);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNameAr);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCopyTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPriveous);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNext);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmPOSClients";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBirthDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpBirthDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGender, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboGender, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMobileNo1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtMobileNo1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClientBarcode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtClientBarcode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCardNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCardNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEMail, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEMail, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMobileNo2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtMobileNo2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsActive, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkForAllBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReligion, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboReligion, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPriceTypeSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSubAccountSearch, 0);
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBirthDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboGender).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPersonalIDNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobileNo1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobileNo2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboReligion).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCardNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientBarcode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkForAllBranch).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
