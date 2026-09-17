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
using CrystalDecisions.CrystalReports.Engine;
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

namespace ERP.POS.MasterData;

public class frmPOSClientsWithDetails : frmButtons
{
	private DataTable dtGender;

	private DataTable dtBranches;

	private DataTable dtCities;

	private DataTable dtAreas;

	private DataTable dtDetails;

	private DataTable dtClientBranches;

	private DataTable dtSubAccounts;

	private DataTable dtReligions;

	private DataTable dtPriceType;

	private DataRow drMaster;

	private string CurrentBranchCode = "";

	private bool CanEditVoucherNumber = true;

	private ValueList vlCity = new ValueList();

	private ValueList vlArea = new ValueList();

	private ValueList vlBranches = new ValueList();

	private int Client = 0;

	public int ClientID = 0;

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

	private UltraTextEditor txtEMail;

	private UltraLabel lblEMail;

	private UltraTextEditor txtNotes;

	private UltraLabel lblNotes;

	private UltraCheckEditor chkIsActive;

	public UltraGrid ULGData;

	private UltraComboEditor cboSubAccount;

	private UltraLabel lblSubAccount;

	private UltraComboEditor cboReligion;

	private UltraLabel lblReligion;

	private UltraTextEditor txtDiscountPercentage;

	private UltraLabel lblDiscountPercentage;

	private UltraComboEditor cboPriceType;

	private UltraLabel lblPriceType;

	private UltraTextEditor txtClientBarcode;

	private UltraLabel lblClientBarcode;

	public UltraButton btnClientPrintRoll;

	private UltraCheckEditor chkForAllBranch;

	public UltraTabControl UTCDetails;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	public UltraTabPageControl ultraTabPageControl1;

	private UltraTabPageControl ultraTabPageControl2;

	public UltraGrid ULGClientBranches;

	public frmPOSClientsWithDetails()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		InitializeComponent();
		TableName = "POS_Clients";
		NoCol = "ClientCode";
		IDCol = "ClientID";
		DateCol = "GetDate()";
	}

	public frmPOSClientsWithDetails(int ClientID)
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
		((Control)(object)btnSearch).Visible = NavMode;
		((Control)(object)btnNext).Visible = NavMode;
		((Control)(object)btnCopyTo).Visible = NavMode;
		((Control)(object)btnPriveous).Visible = NavMode;
		((EditorButtonControlBase)txtClientBarcode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNameAr).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNameEn).ReadOnly = NavMode;
		((EditorButtonControlBase)cboGender).ReadOnly = NavMode;
		((EditorButtonControlBase)cboReligion).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEMail).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPersonalIDNo).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpBirthDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPriceType).ReadOnly = NavMode || !CanModifyPriceType;
		((EditorButtonControlBase)txtDiscountPercentage).ReadOnly = NavMode || !CanModifyPriceType;
		((EditorButtonControlBase)cboSubAccount).ReadOnly = NavMode;
		((Control)(object)chkIsActive).Enabled = !NavMode;
		((Control)(object)chkForAllBranch).Enabled = !NavMode;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((UltraGridBase)ULGClientBranches).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((UltraGridBase)ULGClientBranches).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((!NavMode) ? 1 : 2);
		((UltraGridBase)ULGClientBranches).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)((!NavMode) ? 1 : 2);
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
		dtBranches = Branches.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		CurrentBranchCode = dtBranches.Select("BranchID = " + GlobalVariables.CurrentBranchID)[0]["BranchCode"].ToString();
		vlBranches.ValueListItems.Clear();
		for (int i = 0; i < dtBranches.Rows.Count; i++)
		{
			vlBranches.ValueListItems.Add(dtBranches.Rows[i]["BranchID"], GlobalVariables.IsArabic ? dtBranches.Rows[i]["BranchNameAr"].ToString() : dtBranches.Rows[i]["BranchNameEn"].ToString());
		}
		dtGender = Gender.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboGender, dtGender, "GenderID", "GenderName");
		dtReligions = Religions.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboReligion, dtReligions, "ReligionID", "ReligionName");
		dtSubAccounts = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSubAccount, dtSubAccounts, "SubAccountID", "SubAccountName");
		dtCities = Cities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlCity.ValueListItems.Clear();
		for (int j = 0; j < dtCities.Rows.Count; j++)
		{
			vlCity.ValueListItems.Add(dtCities.Rows[j]["CityID"], dtCities.Rows[j]["CityName"].ToString());
		}
		dtAreas = BusinessLayer.General.Areas.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlArea.ValueListItems.Clear();
		for (int k = 0; k < dtAreas.Rows.Count; k++)
		{
			vlArea.ValueListItems.Add(dtAreas.Rows[k]["AreaID"], dtAreas.Rows[k]["AreaName"].ToString());
		}
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
		dtDetails = ClientsDetails.SelectByClientID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtClientBranches = ClientsBranches.SelectByClientID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		InitGrid();
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
		((TextEditorControlBase)txtEMail).Clear();
		((TextEditorControlBase)txtPersonalIDNo).Clear();
		dtpBirthDate.DateTime = DateTime.Today;
		((TextEditorControlBase)txtNotes).Clear();
		((Control)(object)txtDiscountPercentage).Text = "0";
		cboPriceType.SelectedIndex = -1;
		cboSubAccount.SelectedIndex = -1;
		((UltraToggleEditorBase)chkIsActive).Checked = true;
		((UltraToggleEditorBase)chkForAllBranch).Checked = true;
		dtDetails.Rows.Clear();
		dtClientBranches.Rows.Clear();
	}

	public override void FillData()
	{
		if (Client == -1)
		{
			drMaster = null;
			btnAddClick();
			((Control)(object)btnOK).Visible = false;
		}
		else if (Client > 0)
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
			((Control)(object)txtEMail).Text = drMaster["EMail"].ToString();
			((Control)(object)txtPersonalIDNo).Text = drMaster["IDNumber"].ToString();
			dtpBirthDate.Value = drMaster["BirthDate"];
			((UltraToggleEditorBase)chkIsActive).Checked = Convert.ToBoolean(drMaster["IsActive"]);
			((UltraToggleEditorBase)chkForAllBranch).Checked = Convert.ToBoolean(drMaster["ForAllBranch"]);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtDiscountPercentage).Text = drMaster["DiscountPercentage"].ToString();
			((TextEditorControlBase)cboPriceType).Value = drMaster["PriceTypeID"];
			((TextEditorControlBase)cboSubAccount).Value = drMaster["SubAccountID"];
			dtDetails = ClientsDetails.SelectByClientID(drMaster["ClientID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			dtClientBranches = ClientsBranches.SelectByClientID(drMaster["ClientID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			InitGrid();
		}
		else
		{
			ClearControls();
		}
	}

	private void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = dtDetails;
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PhoneNumber"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PhoneNumber"].Header).Caption = (GlobalVariables.IsArabic ? "تليفون" : "phone");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PhoneNumber"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MobileNumber"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MobileNumber"].Header).Caption = (GlobalVariables.IsArabic ? "محمول" : "Mobile");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MobileNumber"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityID"].Header).Caption = (GlobalVariables.IsArabic ? "المحافظة" : "City");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityID"].ValueList = (IValueList)(object)vlCity;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AreaID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AreaID"].Header).Caption = (GlobalVariables.IsArabic ? "المنطقة" : "Area");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AreaID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AreaID"].ValueList = (IValueList)(object)vlArea;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Address"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Address"].Header).Caption = (GlobalVariables.IsArabic ? "عنوان" : "Address");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Address"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGClientBranches).DataSource = dtClientBranches;
		GlobalFunctions.PrepareGrid(ULGClientBranches);
		((UltraGridBase)ULGClientBranches).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGClientBranches).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGClientBranches).DisplayLayout.Bands[0].Columns["ClientBranchID"].DefaultCellValue = -1;
		((UltraGridBase)ULGClientBranches).DisplayLayout.Bands[0].Columns["BranchID"].Width = ((Control)(object)ULGClientBranches).Width - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGClientBranches).DisplayLayout.Bands[0].Columns["BranchID"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
		((UltraGridBase)ULGClientBranches).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = false;
		((UltraGridBase)ULGClientBranches).DisplayLayout.Bands[0].Columns["BranchID"].ValueList = (IValueList)(object)vlBranches;
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
		if (((Control)(object)txtClientBarcode).Text.Trim() != "")
		{
			if (Main.CheckForValue("POS_Clients", "ClientBarcode", ((Control)(object)txtClientBarcode).Text, Adding ? "0" : drMaster["ClientBarcode"].ToString(), IsFromServer: false) > 0)
			{
				GlobalVariables.InformationMB.Show("هذا الكود متواجد من قبل", "Client Barcode Already Exists");
				((TextEditorControlBase)txtClientBarcode).Focus();
				return false;
			}
		}
		else
		{
			if (cboSubAccount.SelectedIndex > -1 && Main.CheckForValue("POS_Clients", "SubAccountID", ((TextEditorControlBase)cboSubAccount).Value.ToString(), Adding ? "0" : drMaster["SubAccountID"].ToString(), IsFromServer: false) > 0)
			{
				GlobalVariables.InformationMB.Show("الحساب التحليلى متواجد من قبل", "SubAccount  Name Already Exists");
				((TextEditorControlBase)cboSubAccount).Focus();
				return false;
			}
			if (Main.CheckForValue("POS_Clients", "ClientNameAr", ((Control)(object)txtNameAr).Text, Adding ? "0" : drMaster["ClientNameAr"].ToString(), IsFromServer: false) > 0)
			{
				GlobalVariables.InformationMB.Show("هذا الاسم متواجد من قبل", "Patient Name Already Exists");
				((TextEditorControlBase)txtNameAr).Focus();
				return false;
			}
			if (Main.CheckForValue("POS_Clients", "ClientCode", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["ClientCode"].ToString(), IsFromServer: false) > 0)
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
		}
		if (!((UltraToggleEditorBase)chkForAllBranch).Checked && ((DisposableObjectCollectionBase)((UltraGridBase)ULGClientBranches).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال فروع لهذا العميل", "Please insert Branches for this Client");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGClientBranches).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGClientBranches).Rows[i].Cells["BranchID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الفرع  ", "Please Enter Item Branch");
				ULGClientBranches.ActiveCell = ((UltraGridBase)ULGClientBranches).Rows[i].Cells["BranchID"];
				((UltraGridBase)ULGClientBranches).Rows[i].Cells["BranchID"].DroppedDown = true;
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGClientBranches).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGClientBranches).Rows[i].Cells["BranchID"].Value.ToString() == ((UltraGridBase)ULGClientBranches).Rows[j].Cells["BranchID"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show("لا يمكن تكرار الفرع ", "Cannot Duplicate The Same Branch");
					ULGClientBranches.ActiveCell = ((UltraGridBase)ULGClientBranches).Rows[i].Cells["BranchID"];
					return false;
				}
			}
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا العميل", "Please insert details for this Client");
			return false;
		}
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
		{
			if (((UltraGridBase)ULGData).Rows[k].Cells["PhoneNumber"].Value == DBNull.Value && ((UltraGridBase)ULGData).Rows[k].Cells["MobileNumber"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال التليفون او المحمول", "Please Enter Telephone No Or Mobile No");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[k].Cells["PhoneNumber"];
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[k].Cells["PhoneNumber"].Value.ToString() == ((UltraGridBase)ULGData).Rows[k].Cells["MobileNumber"].Value.ToString())
			{
				GlobalVariables.InformationMB.Show("لابد ان يكون رقم التليفون مختلف عن رقم المحمول", "Tel No Must be different from Mobile No");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[k].Cells["PhoneNumber"];
				return false;
			}
			DataTable dataTable = Clients.ValidateDetailsPhonesNo(((UltraGridBase)ULGData).Rows[k].Cells["PhoneNumber"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].Cells["MobileNumber"].Value.ToString(), Adding ? "-1" : drMaster["ClientID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			if (dataTable.Rows.Count > 0)
			{
				GlobalVariables.InformationMB.Show("رقم الهاتف متواجد من قبل \n إسم العميل :  " + dataTable.Rows[0]["ClientName"].ToString(), "The Phone Number Already Exists Client Name : " + dataTable.Rows[0]["ClientName"].ToString());
				return false;
			}
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			ClientID = Clients.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtNameAr).Text, ((Control)(object)txtNameEn).Text, (((Control)(object)txtClientBarcode).Text == "") ? "Null" : ((Control)(object)txtClientBarcode).Text, (cboGender.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboGender).Value.ToString(), (cboReligion.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboReligion).Value.ToString(), "Null", "Null", "Null", ((Control)(object)txtEMail).Text, (((Control)(object)txtPersonalIDNo).Text == "") ? "Null" : ((Control)(object)txtPersonalIDNo).Text, (dtpBirthDate.Value == null) ? "Null" : dtpBirthDate.DateTime.ToString(GlobalVariables.DateShortFormate), "1", "Null", "Null", "Null", ((Control)(object)txtNotes).Text, (((Control)(object)txtDiscountPercentage).Text == "") ? "0" : ((Control)(object)txtDiscountPercentage).Text, (cboPriceType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), (cboSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccount).Value.ToString(), "Null", ((UltraToggleEditorBase)chkIsActive).Checked ? "1" : "0", ((UltraToggleEditorBase)chkForAllBranch).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ClientID"].Value = ClientID.ToString();
				((UltraGridBase)ULGData).Rows[i].Cells["ClientDetailID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				ClientsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: false);
			}
			if (((UltraToggleEditorBase)chkForAllBranch).Checked)
			{
				((DataTable)((UltraGridBase)ULGClientBranches).DataSource).Clear();
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGClientBranches).Rows).Count; j++)
			{
				((UltraGridBase)ULGClientBranches).Rows[j].Cells["ClientID"].Value = ClientID.ToString();
				((UltraGridBase)ULGClientBranches).Rows[j].Cells["ClientBranchID"].Value = -1;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGClientBranches).Rows).Count > 0)
			{
				ClientsBranches.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGClientBranches).DataSource, GlobalVariables.UserID, IsFromServer: false);
			}
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
		}
	}

	public override void UpdateData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			ClientID = Clients.Insert_Update(drMaster["ClientID"].ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtNameAr).Text, ((Control)(object)txtNameEn).Text, (((Control)(object)txtClientBarcode).Text == "") ? "Null" : ((Control)(object)txtClientBarcode).Text, (cboGender.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboGender).Value.ToString(), (cboReligion.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboReligion).Value.ToString(), "Null", "Null", "Null", ((Control)(object)txtEMail).Text, (((Control)(object)txtPersonalIDNo).Text == "") ? "Null" : ((Control)(object)txtPersonalIDNo).Text, (dtpBirthDate.Value == null) ? "Null" : dtpBirthDate.DateTime.ToString(GlobalVariables.DateShortFormate), "1", "Null", "Null", "Null", ((Control)(object)txtNotes).Text, (((Control)(object)txtDiscountPercentage).Text == "") ? "0" : ((Control)(object)txtDiscountPercentage).Text, (cboPriceType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), (cboSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccount).Value.ToString(), "Null", ((UltraToggleEditorBase)chkIsActive).Checked ? "1" : "0", ((UltraToggleEditorBase)chkForAllBranch).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
			string text = ",";
			((UltraGridBase)ULGData).UpdateData();
			dtDetails.AcceptChanges();
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ClientID"].Value = ClientID.ToString();
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["ClientDetailID"].Value.ToString() + ",";
			}
			Main.DeleteForUpdate("POS_ClientsDetails", "ClientID", drMaster["ClientID"].ToString(), "ClientDetailID", text);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				ClientsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: false);
			}
			if (((UltraToggleEditorBase)chkForAllBranch).Checked)
			{
				((DataTable)((UltraGridBase)ULGClientBranches).DataSource).Clear();
			}
			string text2 = ",";
			((UltraGridBase)ULGClientBranches).UpdateData();
			dtClientBranches.AcceptChanges();
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGClientBranches).Rows).Count; j++)
			{
				((UltraGridBase)ULGClientBranches).Rows[j].Cells["ClientID"].Value = ClientID.ToString();
				text2 = text2 + ((UltraGridBase)ULGClientBranches).Rows[j].Cells["ClientBranchID"].Value.ToString() + ",";
			}
			Main.DeleteForUpdate("POS_ClientsBranches", "ClientID", drMaster["ClientID"].ToString(), "ClientBranchID", text2);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGClientBranches).Rows).Count > 0)
			{
				ClientsBranches.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGClientBranches).DataSource, GlobalVariables.UserID, IsFromServer: false);
			}
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
		}
	}

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			ClientsBranches.DeleteByClientID(drMaster["ClientID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			ClientsDetails.DeleteByClientID(drMaster["ClientID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			Clients.Delete(drMaster["ClientID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
		}
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
		dtSubAccounts = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSubAccount, dtSubAccounts, "SubAccountID", "SubAccountName");
		dtCities = Cities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlCity.ValueListItems.Clear();
		for (int i = 0; i < dtCities.Rows.Count; i++)
		{
			vlCity.ValueListItems.Add(dtCities.Rows[i]["CityID"], dtCities.Rows[i]["CityName"].ToString());
		}
		dtAreas = BusinessLayer.General.Areas.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlArea.ValueListItems.Clear();
		for (int j = 0; j < dtAreas.Rows.Count; j++)
		{
			vlArea.ValueListItems.Add(dtAreas.Rows[j]["AreaID"], dtAreas.Rows[j]["AreaName"].ToString());
		}
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
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

	private void btnClientPrintRoll_Click(object sender, EventArgs e)
	{
		if ((GlobalVariables.IsArabic ? ((Control)(object)txtNameAr).Text : ((Control)(object)txtNameEn).Text) == "" || ((Control)(object)txtClientBarcode).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال بيانات العميل", "Please Enter The Client Data");
			return;
		}
		GlobalVariables.ReportDocument = new ReportDocument();
		GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + "Rep_POS_Clients_Roll.rpt");
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@ClientName", GlobalVariables.IsArabic ? ((Control)(object)txtNameAr).Text : ((Control)(object)txtNameEn).Text);
		GlobalVariables.ReportDocument.SetParameterValue("@ClientBarCode", ((Control)(object)txtClientBarcode).Text);
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
	}

	private void chkForAllBranch_CheckedChanged(object sender, EventArgs e)
	{
		((UltraTabControlBase)UTCDetails).Tabs["Branches"].Visible = !((UltraToggleEditorBase)chkForAllBranch).Checked;
	}

	private void btnCopyTo_Click(object sender, EventArgs e)
	{
		btnCopyToClick();
	}

	private void txtPersonalIDNo_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "CityID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			int num = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtCities.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["CityID"].ToString()) : 0);
			if (num != 0)
			{
				e.Cell.Row.Cells["AreaID"].Value = DBNull.Value;
				e.Cell.Row.Cells["AreaID"].ValueList = (IValueList)(object)getAreasValueList(num);
			}
			else
			{
				e.Cell.Row.Cells["AreaID"].ValueList = null;
				e.Cell.Row.Cells["AreaID"].Value = DBNull.Value;
			}
		}
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
	}

	private ValueList getAreasValueList(int CityID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtAreas.Select("CityID=" + CityID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["AreaID"].ToString(), array[i]["AreaName"].ToString());
		}
		return val;
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "AreaID" && ((UltraGridBase)ULGData).ActiveRow.Cells["CityID"].Value == DBNull.Value)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "PhoneNumber" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "MobileNumber"))
		{
			GlobalFunctions.CheckForIntegers(ULGData.ActiveCell, e);
		}
	}

	private void txtDiscountPercentage_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
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
		//IL_0a99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa3: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.MasterData.frmPOSClientsWithDetails));
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
		Appearance val34 = new Appearance();
		Appearance val35 = new Appearance();
		Appearance val36 = new Appearance();
		Appearance val37 = new Appearance();
		Appearance val38 = new Appearance();
		Appearance val39 = new Appearance();
		UltraTab val40 = new UltraTab();
		UltraTab val41 = new UltraTab();
		this.ultraTabPageControl1 = new UltraTabPageControl();
		this.ULGData = new UltraGrid();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGClientBranches = new UltraGrid();
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
		this.txtEMail = new UltraTextEditor();
		this.lblEMail = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblNotes = new UltraLabel();
		this.chkIsActive = new UltraCheckEditor();
		this.cboSubAccount = new UltraComboEditor();
		this.lblSubAccount = new UltraLabel();
		this.cboReligion = new UltraComboEditor();
		this.lblReligion = new UltraLabel();
		this.txtDiscountPercentage = new UltraTextEditor();
		this.lblDiscountPercentage = new UltraLabel();
		this.cboPriceType = new UltraComboEditor();
		this.lblPriceType = new UltraLabel();
		this.txtClientBarcode = new UltraTextEditor();
		this.lblClientBarcode = new UltraLabel();
		this.btnClientPrintRoll = new UltraButton();
		this.chkForAllBranch = new UltraCheckEditor();
		this.UTCDetails = new UltraTabControl();
		this.ultraTabSharedControlsPage1 = new UltraTabSharedControlsPage();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGClientBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBirthDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboGender).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPersonalIDNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboReligion).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientBarcode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkForAllBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)this.UTCDetails).SuspendLayout();
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
		resources.ApplyResources(base.btnImport, "btnImport");
		resources.ApplyResources(base.btnExport, "btnExport");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ultraTabPageControl1, "ultraTabPageControl1");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Name = "ultraTabPageControl1";
		resources.ApplyResources(this.ULGData, "ULGData");
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val3).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val3).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val3, "appearance3");
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val4, "appearance4");
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val4;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val6;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val8, "appearance8");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val8;
		((AppearanceBase)val9).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val9, "appearance9");
		((AppearanceBase)val9).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val10).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val10).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val10).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val10).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val11).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val11, "appearance11");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val12, "appearance12");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		this.ULGData.AfterEnterEditMode += new System.EventHandler(ULGData_AfterEnterEditMode);
		this.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		((System.Windows.Forms.Control)(object)this.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGClientBranches);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.ULGClientBranches, "ULGClientBranches");
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val13).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val13).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val13, "appearance13");
		((SpecialBoxBase)((UltraGridBase)this.ULGClientBranches).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val13;
		((AppearanceBase)val14).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val14, "appearance14");
		((UltraGridBase)this.ULGClientBranches).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val14;
		((SpecialBoxBase)((UltraGridBase)this.ULGClientBranches).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val15).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val15).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val15).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val15).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val15, "appearance15");
		((UltraGridBase)this.ULGClientBranches).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGClientBranches).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGClientBranches).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val16).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val16).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val16, "appearance16");
		((UltraGridBase)this.ULGClientBranches).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val16;
		((AppearanceBase)val17).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val17).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val17, "appearance17");
		((UltraGridBase)this.ULGClientBranches).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val17;
		((UltraGridBase)this.ULGClientBranches).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGClientBranches).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val18).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val18, "appearance18");
		((UltraGridBase)this.ULGClientBranches).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val18;
		((AppearanceBase)val19).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val19, "appearance19");
		((AppearanceBase)val19).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGClientBranches).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val19;
		((UltraGridBase)this.ULGClientBranches).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGClientBranches).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val20).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val20).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val20).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val20).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val20).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val20, "appearance20");
		((UltraGridBase)this.ULGClientBranches).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val20;
		((UltraGridBase)this.ULGClientBranches).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGClientBranches).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val21).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val21).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val21, "appearance21");
		((UltraGridBase)this.ULGClientBranches).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val21;
		((UltraGridBase)this.ULGClientBranches).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val22).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val22, "appearance22");
		((UltraGridBase)this.ULGClientBranches).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val22;
		((System.Windows.Forms.Control)(object)this.ULGClientBranches).Name = "ULGClientBranches";
		resources.ApplyResources(this.txtCode, "txtCode");
		((System.Windows.Forms.Control)(object)this.txtCode).Name = "txtCode";
		((System.Windows.Forms.Control)(object)this.txtCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtCode_KeyUp);
		resources.ApplyResources(this.btnSearch, "btnSearch");
		((AppearanceBase)val23).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val23, "appearance23");
		((ControlBase)this.btnSearch).Appearance = (AppearanceBase)(object)val23;
		((System.Windows.Forms.Control)(object)this.btnSearch).Name = "btnSearch";
		((System.Windows.Forms.Control)(object)this.btnSearch).Click += new System.EventHandler(btnSearch_Click);
		resources.ApplyResources(this.btnPriveous, "btnPriveous");
		((AppearanceBase)val24).Image = ERP.Properties.Resources.BarLeft;
		resources.ApplyResources(val24, "appearance24");
		((ControlBase)this.btnPriveous).Appearance = (AppearanceBase)(object)val24;
		((System.Windows.Forms.Control)(object)this.btnPriveous).Name = "btnPriveous";
		((System.Windows.Forms.Control)(object)this.btnPriveous).Click += new System.EventHandler(btnPriveous_Click);
		resources.ApplyResources(this.btnNext, "btnNext");
		((AppearanceBase)val25).Image = ERP.Properties.Resources.BarRight;
		resources.ApplyResources(val25, "appearance25");
		((ControlBase)this.btnNext).Appearance = (AppearanceBase)(object)val25;
		((System.Windows.Forms.Control)(object)this.btnNext).Name = "btnNext";
		((System.Windows.Forms.Control)(object)this.btnNext).Click += new System.EventHandler(btnNext_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val26).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val26).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val26).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val26, "appearance26");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val26;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val27).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val27).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val27).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val27, "appearance27");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val27;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblCode, "lblCode");
		((AppearanceBase)val28).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val28).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val28).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val28, "appearance28");
		((ControlBase)this.lblCode).Appearance = (AppearanceBase)(object)val28;
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
		((AppearanceBase)val29).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val29).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val29, "appearance29");
		((ControlBase)this.lblBirthDate).Appearance = (AppearanceBase)(object)val29;
		this.lblBirthDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBirthDate).Name = "lblBirthDate";
		((ControlBase)this.lblBirthDate).WrapText = false;
		resources.ApplyResources(this.cboGender, "cboGender");
		((System.Windows.Forms.Control)(object)this.cboGender).Name = "cboGender";
		resources.ApplyResources(this.lblGender, "lblGender");
		((AppearanceBase)val30).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val30).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val30, "appearance30");
		((ControlBase)this.lblGender).Appearance = (AppearanceBase)(object)val30;
		this.lblGender.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGender).Name = "lblGender";
		((ControlBase)this.lblGender).WrapText = false;
		resources.ApplyResources(this.txtPersonalIDNo, "txtPersonalIDNo");
		((System.Windows.Forms.Control)(object)this.txtPersonalIDNo).Name = "txtPersonalIDNo";
		((System.Windows.Forms.Control)(object)this.txtPersonalIDNo).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPersonalIDNo_KeyPress);
		resources.ApplyResources(this.lblPersonalIDNo, "lblPersonalIDNo");
		((AppearanceBase)val31).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val31).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val31, "appearance31");
		((ControlBase)this.lblPersonalIDNo).Appearance = (AppearanceBase)(object)val31;
		this.lblPersonalIDNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPersonalIDNo).Name = "lblPersonalIDNo";
		((ControlBase)this.lblPersonalIDNo).WrapText = false;
		resources.ApplyResources(this.txtEMail, "txtEMail");
		((System.Windows.Forms.Control)(object)this.txtEMail).Name = "txtEMail";
		resources.ApplyResources(this.lblEMail, "lblEMail");
		((AppearanceBase)val32).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val32).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val32, "appearance32");
		((ControlBase)this.lblEMail).Appearance = (AppearanceBase)(object)val32;
		this.lblEMail.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEMail).Name = "lblEMail";
		((ControlBase)this.lblEMail).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((AppearanceBase)val33).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val33).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val33, "appearance33");
		((ControlBase)this.lblNotes).Appearance = (AppearanceBase)(object)val33;
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.chkIsActive, "chkIsActive");
		((AppearanceBase)val34).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val34, "appearance34");
		((UltraToggleEditorBase)this.chkIsActive).Appearance = (AppearanceBase)(object)val34;
		((UltraToggleEditorBase)this.chkIsActive).Checked = true;
		((UltraToggleEditorBase)this.chkIsActive).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsActive).Name = "chkIsActive";
		resources.ApplyResources(this.cboSubAccount, "cboSubAccount");
		((System.Windows.Forms.Control)(object)this.cboSubAccount).Name = "cboSubAccount";
		resources.ApplyResources(this.lblSubAccount, "lblSubAccount");
		this.lblSubAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSubAccount).Name = "lblSubAccount";
		((ControlBase)this.lblSubAccount).WrapText = false;
		resources.ApplyResources(this.cboReligion, "cboReligion");
		((System.Windows.Forms.Control)(object)this.cboReligion).Name = "cboReligion";
		resources.ApplyResources(this.lblReligion, "lblReligion");
		this.lblReligion.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReligion).Name = "lblReligion";
		((ControlBase)this.lblReligion).WrapText = false;
		resources.ApplyResources(this.txtDiscountPercentage, "txtDiscountPercentage");
		((System.Windows.Forms.Control)(object)this.txtDiscountPercentage).Name = "txtDiscountPercentage";
		((System.Windows.Forms.Control)(object)this.txtDiscountPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDiscountPercentage_KeyPress);
		resources.ApplyResources(this.lblDiscountPercentage, "lblDiscountPercentage");
		((AppearanceBase)val35).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val35).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val35, "appearance35");
		((ControlBase)this.lblDiscountPercentage).Appearance = (AppearanceBase)(object)val35;
		this.lblDiscountPercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscountPercentage).Name = "lblDiscountPercentage";
		((ControlBase)this.lblDiscountPercentage).WrapText = false;
		resources.ApplyResources(this.cboPriceType, "cboPriceType");
		((System.Windows.Forms.Control)(object)this.cboPriceType).Name = "cboPriceType";
		resources.ApplyResources(this.lblPriceType, "lblPriceType");
		this.lblPriceType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPriceType).Name = "lblPriceType";
		((ControlBase)this.lblPriceType).WrapText = false;
		resources.ApplyResources(this.txtClientBarcode, "txtClientBarcode");
		((System.Windows.Forms.Control)(object)this.txtClientBarcode).Name = "txtClientBarcode";
		resources.ApplyResources(this.lblClientBarcode, "lblClientBarcode");
		((AppearanceBase)val36).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val36).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val36, "appearance36");
		((ControlBase)this.lblClientBarcode).Appearance = (AppearanceBase)(object)val36;
		this.lblClientBarcode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClientBarcode).Name = "lblClientBarcode";
		((ControlBase)this.lblClientBarcode).WrapText = false;
		resources.ApplyResources(this.btnClientPrintRoll, "btnClientPrintRoll");
		((AppearanceBase)val37).Image = ERP.Properties.Resources.Print;
		resources.ApplyResources(val37, "appearance37");
		((ControlBase)this.btnClientPrintRoll).Appearance = (AppearanceBase)(object)val37;
		((System.Windows.Forms.Control)(object)this.btnClientPrintRoll).Name = "btnClientPrintRoll";
		((System.Windows.Forms.Control)(object)this.btnClientPrintRoll).Click += new System.EventHandler(btnClientPrintRoll_Click);
		resources.ApplyResources(this.chkForAllBranch, "chkForAllBranch");
		((AppearanceBase)val38).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val38, "appearance38");
		((UltraToggleEditorBase)this.chkForAllBranch).Appearance = (AppearanceBase)(object)val38;
		((UltraToggleEditorBase)this.chkForAllBranch).Checked = true;
		((UltraToggleEditorBase)this.chkForAllBranch).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkForAllBranch).Name = "chkForAllBranch";
		((UltraToggleEditorBase)this.chkForAllBranch).CheckedChanged += new System.EventHandler(chkForAllBranch_CheckedChanged);
		resources.ApplyResources(this.UTCDetails, "UTCDetails");
		resources.ApplyResources(val39, "appearance39");
		((AppearanceBase)val39).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.UTCDetails).Appearance = (AppearanceBase)(object)val39;
		((System.Windows.Forms.Control)(object)this.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl1);
		((System.Windows.Forms.Control)(object)this.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((System.Windows.Forms.Control)(object)this.UTCDetails).Name = "UTCDetails";
		((UltraTabControlBase)this.UTCDetails).SharedControlsPage = this.ultraTabSharedControlsPage1;
		((UltraTabControlBase)this.UTCDetails).TabOrientation = (TabOrientation)2;
		((KeyedSubObjectBase)val40).Key = "Details";
		val40.TabPage = this.ultraTabPageControl1;
		resources.ApplyResources(val40, "ultraTab2");
		((SubObjectBase)val40).ForceApplyResources = "";
		((KeyedSubObjectBase)val41).Key = "Branches";
		val41.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val41, "ultraTab1");
		val41.Visible = false;
		((SubObjectBase)val41).ForceApplyResources = "";
		((UltraTabControlBase)this.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[2] { val40, val41 });
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		base.AcceptButton = (System.Windows.Forms.IButtonControl)base.btnAdd;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UTCDetails);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientPrintRoll);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkForAllBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtClientBarcode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClientBarcode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboReligion);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReligion);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsActive);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEMail);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEMail);
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
		base.Name = "frmPOSClientsWithDetails";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnImport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnExport, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEMail, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEMail, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsActive, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReligion, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboReligion, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClientBarcode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtClientBarcode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkForAllBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientPrintRoll, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UTCDetails, 0);
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGClientBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBirthDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboGender).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPersonalIDNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboReligion).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientBarcode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkForAllBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)this.UTCDetails).ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
