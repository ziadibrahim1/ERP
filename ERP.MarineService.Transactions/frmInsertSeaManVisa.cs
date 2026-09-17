using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.MarineService;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.MarineService.Transactions;

public class frmInsertSeaManVisa : frmBase
{
	private DataTable dtNationality;

	private DataTable dtSubAccountTypes;

	private DataTable dtAirPorts;

	private DataTable dtVisaStates;

	private DataTable dtVessels;

	private DataTable dtSettings;

	private DataTable dtSettingsVisaBanks;

	private DataTable dtCompanies;

	private DataTable dtSubAccountGroups;

	private string SubAccountID = "-1";

	private string OperationServiceID = "";

	private string OperationID = "";

	private string VesselID;

	private int VisaActualPeriodDays = 0;

	private int VisaValidPeriodDays = 0;

	private string VisaCost = "0";

	private string UrgentVisaCost = "0";

	private string VisaPrintOutCost = "0";

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraButton btnSave;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	private UltraLabel lblPassportNo;

	private UltraTextEditor txtPassportNo;

	public UltraGroupBox UGBDetails;

	private UltraTextEditor txtNo;

	private UltraLabel ultraLabel1;

	private UltraLabel lblCDCExpireDate;

	private UltraDateTimeEditor dtpCDCExpireDate;

	private UltraLabel lblCDCNo;

	private UltraTextEditor txtCDCNo;

	private UltraLabel lblPassportExpireDate;

	private UltraDateTimeEditor dtpPassportExpireDate;

	private UltraLabel lblNameAr;

	private UltraTextEditor txtNameAr;

	private UltraLabel lblNameEn;

	private UltraTextEditor txtNameEn;

	public UltraGroupBox ultraGroupBox1;

	private UltraComboEditor cboSubAccountGroup;

	private UltraLabel ultraLabel2;

	private UltraComboEditor cboSubAccountType;

	private UltraLabel lblType;

	private UltraTextEditor txtVisaNo;

	private UltraLabel lblVisaNo;

	private UltraComboEditor cboVisaState;

	private UltraDateTimeEditor dtpActualToDate;

	private UltraLabel lblActualFromDate;

	private UltraDateTimeEditor dtpActualFromDate;

	private UltraLabel lblValidToDate;

	private UltraDateTimeEditor dtpValidToDate;

	private UltraLabel lblIDNo;

	private UltraTextEditor txtIDNo;

	private UltraComboEditor cboSignState;

	private UltraLabel lblSignState;

	private UltraComboEditor cboNationality;

	private UltraLabel lblNationality;

	private UltraDateTimeEditor dtpIDIssueDate;

	private UltraLabel lblIDIssueDate;

	private UltraLabel lblVisaState;

	private UltraComboEditor cboVessels;

	private UltraLabel lblVessels;

	private UltraTextEditor txtNotes;

	private UltraLabel lblNotes;

	private UltraComboEditor cboAirPort;

	private UltraLabel lblAirPort;

	private UltraDateTimeEditor dtpVisaIssueDate;

	private UltraComboEditor cboPassengerType;

	private UltraLabel lblPassengerType;

	private UltraDateTimeEditor dtpEntryExpireDate;

	private UltraLabel lblEntryExpire;

	private UltraCheckEditor chkIsUrgent;

	private UltraComboEditor cboCompanies;

	private UltraLabel lblCompany;

	private UltraLabel lblExitDate;

	private UltraDateTimeEditor dtpVisaDate;

	private UltraLabel lblVisaDate;

	private UltraTextEditor txtVisaCost;

	private UltraLabel lblVisaCost;

	private UltraCheckEditor chkIsPrintOut;

	private UltraLabel ultraLabel3;

	private UltraTextEditor txtVisaSerial;

	private UltraLabel lblVisaSerialNo;

	private UltraTextEditor txtPrintOutCost;

	private UltraComboEditor cboVisaBankSettings;

	private UltraLabel ultraLabel9;

	public frmInsertSeaManVisa()
	{
		InitializeComponent();
	}

	public frmInsertSeaManVisa(string OPERATIONID, string OPERATIONSERVICEID, string VESSELID)
		: this()
	{
		OperationServiceID = OPERATIONSERVICEID;
		OperationID = OPERATIONID;
		VesselID = VESSELID;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtNationality = Nationalities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboNationality, dtNationality, "NationalityID", "NationalityName");
		dtSubAccountTypes = SuAccountsTypes.SelectBySuAccountsTypeIDs(GlobalVariables.SeaManSubAccountTypeIDs + GlobalVariables.CaptainSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0");
		GlobalFunctions.FillCombo(cboSubAccountType, dtSubAccountTypes, "SubAccountTypeID", GlobalVariables.IsArabic ? "SubAccountTypeNameAr" : "SubAccountTypeNameEn");
		dtSubAccountGroups = SubAccounts.GroupsFillComboForMarineService(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSubAccountGroup, dtSubAccountGroups, "SubAccountID", "SubAccountName");
		dtVessels = Vessels.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboVessels, dtVessels, "VesselID", "VesselName");
		if (!string.IsNullOrEmpty(VesselID))
		{
			((TextEditorControlBase)cboVessels).Value = VesselID;
			((Control)(object)cboVessels).Enabled = false;
		}
		dtVisaStates = VisaStates.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboVisaState, dtVisaStates, "VisaStateID", "VisaStateName");
		dtCompanies = Companies.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCompanies, dtCompanies, "CompanyID", "CompanyName");
		dtAirPorts = AirPorts.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboAirPort, dtAirPorts, "AirPortID", "AirPortName");
		dtSettings = BusinessLayer.MarineService.Settings.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (dtSettings.Rows.Count > 0)
		{
			VisaActualPeriodDays = ((dtSettings.Select("BranchID = " + GlobalVariables.CurrentBranchID.ToString())[0]["VisaActualPeriodDays"] != DBNull.Value) ? int.Parse(dtSettings.Select("BranchID = " + GlobalVariables.CurrentBranchID.ToString())[0]["VisaActualPeriodDays"].ToString()) : 0);
			VisaValidPeriodDays = ((dtSettings.Select("BranchID = " + GlobalVariables.CurrentBranchID.ToString())[0]["VisaValidPeriodDays"] != DBNull.Value) ? int.Parse(dtSettings.Select("BranchID = " + GlobalVariables.CurrentBranchID.ToString())[0]["VisaValidPeriodDays"].ToString()) : 0);
			VisaPrintOutCost = dtSettings.Select("BranchID = " + GlobalVariables.CurrentBranchID.ToString())[0]["VisaPrintOutCost"].ToString();
		}
		dtSettingsVisaBanks = SettingsVisaBanks.FillCombo(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboVisaBankSettings, dtSettingsVisaBanks, "SettingVisaBankID", "VisaBankName");
		dtpVisaIssueDate.ValueChanged -= dtpIssueDate_ValueChanged;
		dtpActualFromDate.ValueChanged -= dtpActualFromDate_ValueChanged;
		UltraDateTimeEditor obj = dtpVisaIssueDate;
		UltraDateTimeEditor obj2 = dtpValidToDate;
		UltraDateTimeEditor obj3 = dtpActualToDate;
		UltraDateTimeEditor obj4 = dtpActualFromDate;
		UltraDateTimeEditor obj5 = dtpPassportExpireDate;
		UltraDateTimeEditor obj6 = dtpCDCExpireDate;
		UltraDateTimeEditor obj7 = dtpEntryExpireDate;
		object obj8 = (dtpIDIssueDate.Value = null);
		object obj10 = (obj7.Value = obj8);
		object obj12 = (obj6.Value = obj10);
		object obj14 = (obj5.Value = obj12);
		object obj16 = (obj4.Value = obj14);
		object obj18 = (obj3.Value = obj16);
		object value = (obj2.Value = obj18);
		obj.Value = value;
		dtpVisaIssueDate.ValueChanged += dtpIssueDate_ValueChanged;
		dtpActualFromDate.ValueChanged += dtpActualFromDate_ValueChanged;
		cboPassengerType.SelectedIndex = 0;
		((Control)(object)txtVisaCost).Text = VisaCost;
		((Control)(object)txtVisaSerial).Text = OperationsServicesVisas.GetCodeByBranchID(dtpVisaDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
	}

	public virtual string GetCode()
	{
		if (cboSubAccountGroup.SelectedIndex > -1)
		{
			string text = "";
			string text2 = dtSubAccountGroups.Select("SubAccountID =  " + ((TextEditorControlBase)cboSubAccountGroup).Value.ToString())[0]["SubAccountNumber"].ToString();
			return text2 + ((Control)(object)txtNo).Text;
		}
		return ((Control)(object)txtNo).Text;
	}

	public bool ValidateData()
	{
		if (SubAccountID == "-1")
		{
			if (cboSubAccountType.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال النوع", "Please Select Account Type");
				((TextEditorControlBase)cboSubAccountType).Focus();
				return false;
			}
			if (cboSubAccountGroup.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال المجموعة", "Please Select Group");
				((TextEditorControlBase)cboSubAccountGroup).Focus();
				return false;
			}
			if (((Control)(object)txtNo).Text.Trim() == "")
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال رقم الحساب", "Please Enter The Account Number");
				((TextEditorControlBase)txtNo).Focus();
				return false;
			}
			if (((Control)(object)txtNameAr).Text.Trim() == "")
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال الاسم بالعربية", "Please Enter The Arabic Name");
				((TextEditorControlBase)txtNameAr).Focus();
				return false;
			}
			if (((Control)(object)txtPassportNo).Text.Trim() == "")
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال رقم الجواز", "Please Enter The Passport Number");
				((TextEditorControlBase)txtPassportNo).Focus();
				return false;
			}
			if (((Control)(object)txtCDCNo).Text.Trim() == "")
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال رقم الجواز الاسود", "Please Enter The CDC Number");
				((TextEditorControlBase)txtCDCNo).Focus();
				return false;
			}
		}
		if (dtSettings.Rows.Count > 0)
		{
			if (((UltraToggleEditorBase)chkIsPrintOut).Checked)
			{
				if (dtSettings.Select("BranchID = " + GlobalVariables.CurrentBranchID.ToString())[0]["VisaPrintSupplierAccountID"] == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("برجاء إدخال حساب مورد الطباعة في اعدادات الخدمات البحريه", "Please Enter Visa PrintOut Supplier Account in Marine Services Settings");
					return false;
				}
				if (dtSettings.Select("BranchID = " + GlobalVariables.CurrentBranchID.ToString())[0]["VisaPrintOutCost"] == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("برجاء إدخال تكلفة طباعة التأشيرة في اعدادات الخدمات البحريه", "Please Enter Visa Printout Cost in Marine Services Settings");
					return false;
				}
			}
			if (dtSettingsVisaBanks.Rows.Count == 0)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال اعدادات التأشيرة في اعدادات الخدمات البحريه", "Please Enter Visa Settings in Marine Services Settings");
				return false;
			}
			if (cboVisaBankSettings.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("برجاء اختيار اسم البنك", "Please Select The Bank Name");
				((TextEditorControlBase)cboVisaBankSettings).Focus();
				return false;
			}
			if (((UltraToggleEditorBase)chkIsUrgent).Checked)
			{
				if (dtSettingsVisaBanks.Select("SettingVisaBankID = " + ((TextEditorControlBase)cboVisaBankSettings).Value.ToString())[0]["UrgentVisaCost"] == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("برجاء إدخال تكلفة التأشيرة العاجله في اعدادات الخدمات البحريه", "Please Enter Urgent Visa Cost in Marine Services Settings");
					return false;
				}
			}
			else if (dtSettingsVisaBanks.Select("SettingVisaBankID = " + ((TextEditorControlBase)cboVisaBankSettings).Value.ToString())[0]["VisaCost"] == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال تكلفة التأشيرة في اعدادات الخدمات البحريه", "Please Enter Visa Cost in Marine Services Settings");
				return false;
			}
			if (((UltraToggleEditorBase)chkIsPrintOut).Checked && (((Control)(object)txtPrintOutCost).Text == null || ((Control)(object)txtPrintOutCost).Text.Trim() == "" || decimal.Parse(((Control)(object)txtPrintOutCost).Text.Trim()) == 0m))
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال تكلفة طباعة التأشيرة", "Please Enter The Visa PrintOut Cost");
				((TextEditorControlBase)txtPrintOutCost).Focus();
				return false;
			}
			if (Main.CheckForValueByBranchIDAndFiscalYearID("MS_OperationsServicesVisas", "OperationServiceVisaNo", ((Control)(object)txtVisaSerial).Text, "0", GlobalVariables.CurrentBranchID, "VisaDate", dtpVisaDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
			{
				string codeByBranchID = OperationsServicesVisas.GetCodeByBranchID(dtpVisaDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
				GlobalVariables.InformationMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Voucher Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
				((Control)(object)txtVisaSerial).Text = codeByBranchID;
			}
			if (cboVisaState.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال الحالة", "Please Select The Visa Status");
				((TextEditorControlBase)cboVisaState).Focus();
				return false;
			}
			if (cboSignState.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال حالة البحار", "Please Select Sign On/Off");
				((TextEditorControlBase)cboSignState).Focus();
				return false;
			}
			if (cboPassengerType.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال نوع الراكب", "Please Select Passenger Type");
				((TextEditorControlBase)cboPassengerType).Focus();
				return false;
			}
			if (dtpActualFromDate.Value != null)
			{
				if (Convert.ToDateTime(dtpActualFromDate.Value) < Convert.ToDateTime(dtpVisaIssueDate.Value))
				{
					GlobalVariables.InformationMB.Show("تاريخ الدخول قبل تاريخ اصدار التأشيرة", "The Issue date is after the Entry date and this is not allowed");
					((Control)(object)dtpActualFromDate).Focus();
					return false;
				}
			}
			else if (dtpActualToDate.Value != null && Convert.ToDateTime(dtpActualToDate.Value) < Convert.ToDateTime(dtpActualFromDate.Value))
			{
				GlobalVariables.InformationMB.Show("تاريخ الخروج قبل تاريخ الدخول", "The Entry date is after the Exit date and this is not allowed");
				((Control)(object)dtpActualToDate).Focus();
				return false;
			}
			return true;
		}
		GlobalVariables.InformationMB.Show("برجاء إدخال اعدادات التأشيرة في اعدادات الخدمات البحريه", "Please Enter Visa Settings in Marine Services Settings");
		return false;
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (!ValidateData())
		{
			return;
		}
		try
		{
			if (SubAccountID == "-1")
			{
				Main.StartBulkTrans(FromServer: true);
				SubAccountID = SubAccounts.Insert_Update("-1", GetCode(), ((Control)(object)txtNameAr).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? "Null" : ((Control)(object)txtNameEn).Text.Trim(), (cboSubAccountGroup.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccountGroup).Value.ToString(), "0", (int.Parse(dtSubAccountGroups.Select("SubAccountID =  " + ((TextEditorControlBase)cboSubAccountGroup).Value.ToString())[0]["LevelID"].ToString()) + 1).ToString(), ((TextEditorControlBase)cboSubAccountType).Value.ToString(), "1", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true).ToString();
				SubAccounts_Details.Insert_UpdateByAccIDs(SubAccountID, "," + dtSubAccountGroups.Select("SubAccountID =  " + ((TextEditorControlBase)cboSubAccountGroup).Value.ToString())[0]["DefaultAccountID"].ToString() + ",", "0", GlobalVariables.CurrentBranchID, IsFromServer: true);
				if ("," + ((TextEditorControlBase)cboSubAccountType).Value.ToString() + "," == GlobalVariables.SeaManSubAccountTypeIDs)
				{
					SeaMen.Insert_Update("-1", (((Control)(object)txtNo).Text == "") ? GetCode() : ((Control)(object)txtNo).Text, SubAccountID, "Null", "Null", "Null", "Null", "Null", (cboNationality.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboNationality).Value.ToString(), ((Control)(object)txtPassportNo).Text, "Null", (dtpPassportExpireDate.Value == null) ? "Null" : dtpPassportExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtCDCNo).Text, "Null", (dtpCDCExpireDate.Value == null) ? "Null" : dtpCDCExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtIDNo).Text, (dtpIDIssueDate.Value == null) ? "Null" : dtpIDIssueDate.DateTime.ToString(GlobalVariables.DateShortFormate), "Null", "Null", "Null", "Null", "Null", "Null", dtSubAccountGroups.Select("SubAccountID =  " + ((TextEditorControlBase)cboSubAccountGroup).Value.ToString())[0]["DefaultAccountID"].ToString(), "Null", "Null", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				}
				else if ("," + ((TextEditorControlBase)cboSubAccountType).Value.ToString() + "," == GlobalVariables.CaptainSubAccountTypeIDs)
				{
					Captains.Insert_Update("-1", (((Control)(object)txtNo).Text == "") ? GetCode() : ((Control)(object)txtNo).Text, SubAccountID, "Null", "Null", "Null", "Null", "Null", (cboNationality.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboNationality).Value.ToString(), ((Control)(object)txtPassportNo).Text, "Null", (dtpPassportExpireDate.Value == null) ? "Null" : dtpPassportExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtCDCNo).Text, "Null", (dtpCDCExpireDate.Value == null) ? "Null" : dtpCDCExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtIDNo).Text, (dtpIDIssueDate.Value == null) ? "Null" : dtpIDIssueDate.DateTime.ToString(GlobalVariables.DateShortFormate), "Null", "Null", "Null", "Null", "Null", "Null", dtSubAccountGroups.Select("SubAccountID =  " + ((TextEditorControlBase)cboSubAccountGroup).Value.ToString())[0]["DefaultAccountID"].ToString(), "Null", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				}
				Main.EndBulkTrans(FromServer: true);
			}
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			return;
		}
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = OperationsServicesVisas.Insert_Update("-1", ((Control)(object)txtVisaSerial).Text, (OperationServiceID == "") ? "Null" : OperationServiceID, (OperationID == "") ? "Null" : OperationID, (dtpVisaDate.Value == null) ? "Null" : dtpVisaDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboVessels.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboVessels).Value.ToString(), (cboCompanies.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCompanies).Value.ToString(), SubAccountID, ((UltraToggleEditorBase)chkIsUrgent).Checked ? "1" : "0", (cboPassengerType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPassengerType).Value.ToString(), bool.Parse(cboSignState.SelectedItem.DataValue.ToString()) ? "1" : "0", (cboVisaState.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboVisaState).Value.ToString(), ((Control)(object)txtVisaNo).Text, "0", "Null", "0", "Null", "Null", (dtpVisaIssueDate.Value == null) ? "Null" : dtpVisaIssueDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpValidToDate.Value == null) ? "Null" : dtpValidToDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpActualFromDate.Value == null) ? "Null" : dtpActualFromDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpEntryExpireDate.Value == null) ? "Null" : dtpEntryExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpActualToDate.Value == null) ? "Null" : dtpActualToDate.DateTime.ToString(GlobalVariables.DateShortFormate), "Null", (cboAirPort.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAirPort).Value.ToString(), ((Control)(object)txtNotes).Text, ((TextEditorControlBase)cboVisaBankSettings).Value.ToString(), dtSettings.Select("BranchID = " + GlobalVariables.CurrentBranchID.ToString())[0]["VisaPrintSupplierAccountID"].ToString(), (dtSettings.Select("BranchID = " + GlobalVariables.CurrentBranchID.ToString())[0]["VisaPrintSupplierSubAccountID"] == DBNull.Value) ? "Null" : dtSettings.Select("BranchID = " + GlobalVariables.CurrentBranchID.ToString())[0]["VisaPrintSupplierSubAccountID"].ToString(), (((Control)(object)txtVisaCost).Text == "") ? "0" : ((Control)(object)txtVisaCost).Text, ((UltraToggleEditorBase)chkIsPrintOut).Checked ? "1" : "0", (((Control)(object)txtPrintOutCost).Text == "") ? "0" : ((Control)(object)txtPrintOutCost).Text, "Null", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			if (OperationServiceID != "")
			{
				OperationsServices.UpdateTotalExpenses(OperationServiceID, GlobalVariables.UserID);
			}
			OperationsServicesVisas.GenerateJvs(num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
			ClearAllControls();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	public void DisplayData(DataRow drMaster)
	{
		((TextEditorControlBase)cboSubAccountType).Value = drMaster["SubAccountTypeID"];
		((TextEditorControlBase)cboSubAccountGroup).Value = drMaster["ParentID"];
		((Control)(object)txtNo).Text = drMaster["SeaManNo"].ToString();
		((Control)(object)txtNameAr).Text = drMaster["SubAccountNameAr"].ToString();
		((Control)(object)txtNameEn).Text = drMaster["SubAccountNameEn"].ToString();
		((Control)(object)txtPassportNo).Text = drMaster["PassportNo"].ToString();
		((Control)(object)txtCDCNo).Text = drMaster["CDCNo"].ToString();
		((Control)(object)txtIDNo).Text = drMaster["IDNo"].ToString();
		dtpPassportExpireDate.Value = drMaster["PassportExpireDate"];
		dtpCDCExpireDate.Value = drMaster["CDCExpireDate"];
		dtpIDIssueDate.Value = drMaster["IDIssueDate"];
		((TextEditorControlBase)cboNationality).Value = drMaster["NationalityID"];
	}

	public void ClearSubAccountControls()
	{
		cboSubAccountType.SelectedIndex = -1;
		cboNationality.SelectedIndex = -1;
		UltraDateTimeEditor obj = dtpPassportExpireDate;
		UltraDateTimeEditor obj2 = dtpCDCExpireDate;
		object obj3 = (dtpIDIssueDate.Value = null);
		object value = (obj2.Value = obj3);
		obj.Value = value;
		((TextEditorControlBase)txtNameAr).Clear();
		((TextEditorControlBase)txtNameEn).Clear();
		((TextEditorControlBase)txtCDCNo).Clear();
		((TextEditorControlBase)txtIDNo).Clear();
		((TextEditorControlBase)txtNo).Clear();
	}

	public void SetSubAccountControls(bool NavMode)
	{
		((EditorButtonControlBase)cboSubAccountType).ReadOnly = NavMode;
		((EditorButtonControlBase)cboNationality).ReadOnly = NavMode;
		UltraDateTimeEditor obj = dtpPassportExpireDate;
		UltraDateTimeEditor obj2 = dtpCDCExpireDate;
		bool flag = (((EditorButtonControlBase)dtpIDIssueDate).ReadOnly = NavMode);
		bool readOnly = (((EditorButtonControlBase)obj2).ReadOnly = flag);
		((EditorButtonControlBase)obj).ReadOnly = readOnly;
		((EditorButtonControlBase)txtNameAr).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNameEn).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCDCNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtIDNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNo).ReadOnly = NavMode;
	}

	public void ClearAllControls()
	{
		ClearSubAccountControls();
		((TextEditorControlBase)txtVisaNo).Clear();
		((Control)(object)txtVisaSerial).Text = OperationsServicesVisas.GetCodeByBranchID(dtpVisaDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		((UltraToggleEditorBase)chkIsUrgent).Checked = false;
		cboAirPort.SelectedIndex = -1;
		cboPassengerType.SelectedIndex = 0;
		cboCompanies.SelectedIndex = -1;
		cboVisaState.SelectedIndex = -1;
		if (string.IsNullOrEmpty(VesselID))
		{
			cboVessels.SelectedIndex = -1;
		}
		else
		{
			((TextEditorControlBase)cboVessels).Value = VesselID;
		}
		dtpVisaIssueDate.ValueChanged -= dtpIssueDate_ValueChanged;
		dtpActualFromDate.ValueChanged -= dtpActualFromDate_ValueChanged;
		((UltraToggleEditorBase)chkIsPrintOut).CheckedChanged -= chkIsPrintOut_CheckedChanged;
		UltraDateTimeEditor obj = dtpVisaIssueDate;
		UltraDateTimeEditor obj2 = dtpValidToDate;
		UltraDateTimeEditor obj3 = dtpActualToDate;
		UltraDateTimeEditor obj4 = dtpEntryExpireDate;
		object obj5 = (dtpActualFromDate.Value = null);
		object obj7 = (obj4.Value = obj5);
		object obj9 = (obj3.Value = obj7);
		object value = (obj2.Value = obj9);
		obj.Value = value;
		((UltraToggleEditorBase)chkIsPrintOut).Checked = false;
		dtpVisaIssueDate.ValueChanged += dtpIssueDate_ValueChanged;
		dtpActualFromDate.ValueChanged += dtpActualFromDate_ValueChanged;
		((UltraToggleEditorBase)chkIsPrintOut).CheckedChanged += chkIsPrintOut_CheckedChanged;
		SubAccountID = "-1";
		((TextEditorControlBase)txtPassportNo).Clear();
		((TextEditorControlBase)txtNotes).Clear();
		((Control)(object)txtVisaCost).Text = VisaCost;
		((Control)(object)txtPrintOutCost).Enabled = false;
		((TextEditorControlBase)txtPrintOutCost).Clear();
	}

	private void cboSubAccountType_ValueChanged(object sender, EventArgs e)
	{
		if (cboSubAccountType.SelectedIndex == -1)
		{
			((TextEditorControlBase)cboSubAccountGroup).Clear();
			cboSubAccountGroup.Items.Clear();
			return;
		}
		DataView dataView = new DataView(dtSubAccountGroups);
		dataView.RowFilter = " SubAccountTypeID = " + ((TextEditorControlBase)cboSubAccountType).Value.ToString();
		GlobalFunctions.FillCombo(cboSubAccountGroup, dataView.ToTable(), "SubAccountID", "SubAccountName");
		cboSubAccountGroup.SelectedIndex = ((DisposableObjectCollectionBase)cboSubAccountGroup.Items).Count - 1;
	}

	private void cboFolder_ValueChanged(object sender, EventArgs e)
	{
		((Control)(object)txtNo).Text = SubAccounts.GetCode((cboSubAccountGroup.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccountGroup).Value.ToString(), IsFromServer: true);
	}

	private void CheckForSubAccount()
	{
		DataTable dataTable = SubAccounts.SelectByPassportNumber(((Control)(object)txtPassportNo).Text, IsFromServer: true);
		if (dataTable.Rows.Count > 0)
		{
			SubAccountID = dataTable.Rows[0]["SubAccountID"].ToString();
			DataRow drMaster = dataTable.Rows[0];
			dtSearchResult = null;
			DisplayData(drMaster);
			DataTable dataTable2 = OperationsServicesVisas.SelectBySubAccountIDVisaValidPeriod(SubAccountID, GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
			if (dataTable2.Rows.Count > 0)
			{
				string text = dataTable2.Rows[0]["VisaNo"].ToString();
				string text2 = dataTable2.Rows[0]["VisaDate"].ToString();
				string text3 = dataTable2.Rows[0]["ValidFromDate"].ToString();
				string text4 = dataTable2.Rows[0]["VesselName"].ToString();
				string text5 = " هذا البحار له تأشيرة غير منتهية برقم" + text + " وبتاريخ" + (text3.Equals("") ? text2 : text3) + " على الباخرة " + text4;
				string text6 = "This SeaMan has a Valid Visa " + text + " with Date " + (text3.Equals("") ? text2 : text3) + " On Vessel " + text4;
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? text5 : text6);
			}
		}
		else
		{
			SubAccountID = "-1";
			ClearSubAccountControls();
		}
		SetSubAccountControls(dataTable.Rows.Count > 0);
	}

	private void txtPassport_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && ((Control)(object)txtPassportNo).Text != "")
		{
			CheckForSubAccount();
		}
	}

	private void txtPassportNo_Leave(object sender, EventArgs e)
	{
		if (SubAccountID == "-1")
		{
			CheckForSubAccount();
		}
	}

	private void dtpDateTime_Enter(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		((UltraWinEditorMaskedControlBase)(UltraDateTimeEditor)sender).SelectAll();
	}

	private void dtpIssueDate_ValueChanged(object sender, EventArgs e)
	{
		if (!dtpVisaIssueDate.Value.Equals(null))
		{
			dtpValidToDate.Value = Convert.ToDateTime(dtpVisaIssueDate.Value).AddDays(VisaValidPeriodDays - 1);
		}
	}

	private void dtpActualFromDate_ValueChanged(object sender, EventArgs e)
	{
		if (!dtpActualFromDate.Value.Equals(null))
		{
			dtpEntryExpireDate.Value = Convert.ToDateTime(dtpActualFromDate.Value).AddDays(VisaActualPeriodDays - 1);
		}
	}

	private void txtVisaCost_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers((object)txtVisaCost, e);
	}

	private void dtpVisaDate_ValueChanged(object sender, EventArgs e)
	{
		((Control)(object)txtVisaSerial).Text = OperationsServicesVisas.GetCodeByBranchID(dtpVisaDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
	}

	private void chkIsPrintOut_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)txtPrintOutCost).Enabled = ((UltraToggleEditorBase)chkIsPrintOut).Checked;
		if (((UltraToggleEditorBase)chkIsPrintOut).Checked)
		{
			((Control)(object)txtPrintOutCost).Text = VisaPrintOutCost;
		}
		else
		{
			((TextEditorControlBase)txtPrintOutCost).Clear();
		}
	}

	private void txtPrintOutCost_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers((object)txtPrintOutCost, e);
	}

	private void cboVisaBankSettings_ValueChanged(object sender, EventArgs e)
	{
		if (cboVisaBankSettings.SelectedIndex > -1)
		{
			VisaCost = dtSettingsVisaBanks.Select("SettingVisaBankID = " + ((TextEditorControlBase)cboVisaBankSettings).Value.ToString())[0]["VisaCost"].ToString();
			UrgentVisaCost = dtSettingsVisaBanks.Select("SettingVisaBankID = " + ((TextEditorControlBase)cboVisaBankSettings).Value.ToString())[0]["UrgentVisaCost"].ToString();
		}
		else
		{
			VisaCost = "0";
			UrgentVisaCost = "0";
		}
		((Control)(object)txtVisaCost).Text = (((UltraToggleEditorBase)chkIsUrgent).Checked ? UrgentVisaCost : VisaCost);
	}

	private void chkIsUrgent_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)txtVisaCost).Text = (((UltraToggleEditorBase)chkIsUrgent).Checked ? UrgentVisaCost : VisaCost);
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
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Expected O, but got Unknown
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Expected O, but got Unknown
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Expected O, but got Unknown
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Expected O, but got Unknown
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Expected O, but got Unknown
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Expected O, but got Unknown
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Expected O, but got Unknown
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Expected O, but got Unknown
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Expected O, but got Unknown
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Expected O, but got Unknown
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Expected O, but got Unknown
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Expected O, but got Unknown
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Expected O, but got Unknown
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Expected O, but got Unknown
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Expected O, but got Unknown
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Expected O, but got Unknown
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Expected O, but got Unknown
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Expected O, but got Unknown
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Expected O, but got Unknown
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Expected O, but got Unknown
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Expected O, but got Unknown
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Expected O, but got Unknown
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Expected O, but got Unknown
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Expected O, but got Unknown
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Expected O, but got Unknown
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Expected O, but got Unknown
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Expected O, but got Unknown
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Expected O, but got Unknown
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Expected O, but got Unknown
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Expected O, but got Unknown
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Expected O, but got Unknown
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Expected O, but got Unknown
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Expected O, but got Unknown
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Expected O, but got Unknown
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Expected O, but got Unknown
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Expected O, but got Unknown
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Expected O, but got Unknown
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Expected O, but got Unknown
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Expected O, but got Unknown
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Expected O, but got Unknown
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Expected O, but got Unknown
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Expected O, but got Unknown
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Expected O, but got Unknown
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Expected O, but got Unknown
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Expected O, but got Unknown
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Expected O, but got Unknown
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Expected O, but got Unknown
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Expected O, but got Unknown
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Expected O, but got Unknown
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Expected O, but got Unknown
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Expected O, but got Unknown
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Expected O, but got Unknown
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Expected O, but got Unknown
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Expected O, but got Unknown
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Expected O, but got Unknown
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Expected O, but got Unknown
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Expected O, but got Unknown
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Expected O, but got Unknown
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Expected O, but got Unknown
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Expected O, but got Unknown
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Expected O, but got Unknown
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Expected O, but got Unknown
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Expected O, but got Unknown
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Expected O, but got Unknown
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Expected O, but got Unknown
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Expected O, but got Unknown
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Expected O, but got Unknown
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Expected O, but got Unknown
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Expected O, but got Unknown
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Expected O, but got Unknown
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmInsertSeaManVisa));
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
		ValueListItem val20 = new ValueListItem();
		ValueListItem val21 = new ValueListItem();
		ValueListItem val22 = new ValueListItem();
		Appearance val23 = new Appearance();
		Appearance val24 = new Appearance();
		Appearance val25 = new Appearance();
		Appearance val26 = new Appearance();
		Appearance val27 = new Appearance();
		Appearance val28 = new Appearance();
		Appearance val29 = new Appearance();
		Appearance val30 = new Appearance();
		Appearance val31 = new Appearance();
		ValueListItem val32 = new ValueListItem();
		ValueListItem val33 = new ValueListItem();
		ValueListItem val34 = new ValueListItem();
		Appearance val35 = new Appearance();
		Appearance val36 = new Appearance();
		Appearance val37 = new Appearance();
		Appearance val38 = new Appearance();
		Appearance val39 = new Appearance();
		Appearance val40 = new Appearance();
		Appearance val41 = new Appearance();
		Appearance val42 = new Appearance();
		ValueListItem val43 = new ValueListItem();
		ValueListItem val44 = new ValueListItem();
		Appearance val45 = new Appearance();
		Appearance val46 = new Appearance();
		this.btnKeyboard = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.lblPassportNo = new UltraLabel();
		this.txtPassportNo = new UltraTextEditor();
		this.UGBDetails = new UltraGroupBox();
		this.cboNationality = new UltraComboEditor();
		this.lblNationality = new UltraLabel();
		this.lblIDNo = new UltraLabel();
		this.txtIDNo = new UltraTextEditor();
		this.cboSubAccountGroup = new UltraComboEditor();
		this.ultraLabel2 = new UltraLabel();
		this.cboSubAccountType = new UltraComboEditor();
		this.lblType = new UltraLabel();
		this.lblCDCExpireDate = new UltraLabel();
		this.lblNameAr = new UltraLabel();
		this.dtpIDIssueDate = new UltraDateTimeEditor();
		this.dtpCDCExpireDate = new UltraDateTimeEditor();
		this.txtNameAr = new UltraTextEditor();
		this.lblNameEn = new UltraLabel();
		this.txtNameEn = new UltraTextEditor();
		this.lblIDIssueDate = new UltraLabel();
		this.lblCDCNo = new UltraLabel();
		this.txtNo = new UltraTextEditor();
		this.ultraLabel1 = new UltraLabel();
		this.txtCDCNo = new UltraTextEditor();
		this.dtpPassportExpireDate = new UltraDateTimeEditor();
		this.lblPassportExpireDate = new UltraLabel();
		this.ultraGroupBox1 = new UltraGroupBox();
		this.cboVisaBankSettings = new UltraComboEditor();
		this.ultraLabel9 = new UltraLabel();
		this.ultraLabel3 = new UltraLabel();
		this.txtPrintOutCost = new UltraTextEditor();
		this.txtVisaCost = new UltraTextEditor();
		this.lblVisaCost = new UltraLabel();
		this.chkIsPrintOut = new UltraCheckEditor();
		this.dtpVisaDate = new UltraDateTimeEditor();
		this.lblVisaDate = new UltraLabel();
		this.lblExitDate = new UltraLabel();
		this.cboCompanies = new UltraComboEditor();
		this.lblVessels = new UltraLabel();
		this.lblCompany = new UltraLabel();
		this.txtVisaNo = new UltraTextEditor();
		this.lblVisaState = new UltraLabel();
		this.cboPassengerType = new UltraComboEditor();
		this.lblVisaNo = new UltraLabel();
		this.lblPassengerType = new UltraLabel();
		this.dtpValidToDate = new UltraDateTimeEditor();
		this.cboVisaState = new UltraComboEditor();
		this.lblValidToDate = new UltraLabel();
		this.dtpVisaIssueDate = new UltraDateTimeEditor();
		this.dtpActualFromDate = new UltraDateTimeEditor();
		this.dtpEntryExpireDate = new UltraDateTimeEditor();
		this.txtNotes = new UltraTextEditor();
		this.lblActualFromDate = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.lblEntryExpire = new UltraLabel();
		this.dtpActualToDate = new UltraDateTimeEditor();
		this.cboAirPort = new UltraComboEditor();
		this.lblSignState = new UltraLabel();
		this.lblAirPort = new UltraLabel();
		this.cboSignState = new UltraComboEditor();
		this.cboVessels = new UltraComboEditor();
		this.chkIsUrgent = new UltraCheckEditor();
		this.txtVisaSerial = new UltraTextEditor();
		this.lblVisaSerialNo = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassportNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboNationality).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtIDNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccountGroup).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccountType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpIDIssueDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCDCExpireDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCDCNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpPassportExpireDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraGroupBox1).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboVisaBankSettings).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPrintOutCost).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaCost).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsPrintOut).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpVisaDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCompanies).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPassengerType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpValidToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaState).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpVisaIssueDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpActualFromDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpEntryExpireDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpActualToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAirPort).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSignState).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVessels).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsUrgent).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaSerial).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val, "appearance39");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val2).Image = resources.GetObject("appearance40.Image");
		resources.ApplyResources(val2, "appearance40");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val3).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val3, "appearance41");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val4).Image = resources.GetObject("appearance42.Image");
		resources.ApplyResources(val4, "appearance42");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val4;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val5, "appearance43");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblPassportNo, "lblPassportNo");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val6, "appearance44");
		((ControlBase)this.lblPassportNo).Appearance = (AppearanceBase)(object)val6;
		this.lblPassportNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPassportNo).Name = "lblPassportNo";
		((ControlBase)this.lblPassportNo).WrapText = false;
		resources.ApplyResources(this.txtPassportNo, "txtPassportNo");
		((System.Windows.Forms.Control)(object)this.txtPassportNo).Name = "txtPassportNo";
		((System.Windows.Forms.Control)(object)this.txtPassportNo).KeyUp += new System.Windows.Forms.KeyEventHandler(txtPassport_KeyUp);
		((System.Windows.Forms.Control)(object)this.txtPassportNo).Leave += new System.EventHandler(txtPassportNo_Leave);
		resources.ApplyResources(this.UGBDetails, "UGBDetails");
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance45");
		this.UGBDetails.Appearance = (AppearanceBase)(object)val7;
		this.UGBDetails.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.cboNationality);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblNationality);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblIDNo);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtIDNo);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.cboSubAccountGroup);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.cboSubAccountType);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblType);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblCDCExpireDate);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblNameAr);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.dtpIDIssueDate);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.dtpCDCExpireDate);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtNameAr);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblNameEn);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtNameEn);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblIDIssueDate);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblCDCNo);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtNo);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtCDCNo);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.dtpPassportExpireDate);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblPassportExpireDate);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Name = "UGBDetails";
		resources.ApplyResources(this.cboNationality, "cboNationality");
		this.cboNationality.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboNationality).Name = "cboNationality";
		resources.ApplyResources(this.lblNationality, "lblNationality");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance46");
		((ControlBase)this.lblNationality).Appearance = (AppearanceBase)(object)val8;
		this.lblNationality.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNationality).Name = "lblNationality";
		((ControlBase)this.lblNationality).WrapText = false;
		resources.ApplyResources(this.lblIDNo, "lblIDNo");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance47");
		((ControlBase)this.lblIDNo).Appearance = (AppearanceBase)(object)val9;
		this.lblIDNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblIDNo).Name = "lblIDNo";
		((ControlBase)this.lblIDNo).WrapText = false;
		resources.ApplyResources(this.txtIDNo, "txtIDNo");
		((System.Windows.Forms.Control)(object)this.txtIDNo).Name = "txtIDNo";
		resources.ApplyResources(this.cboSubAccountGroup, "cboSubAccountGroup");
		this.cboSubAccountGroup.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSubAccountGroup).Name = "cboSubAccountGroup";
		((TextEditorControlBase)this.cboSubAccountGroup).ValueChanged += new System.EventHandler(cboFolder_ValueChanged);
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance48");
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val10;
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.cboSubAccountType, "cboSubAccountType");
		this.cboSubAccountType.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSubAccountType).Name = "cboSubAccountType";
		((TextEditorControlBase)this.cboSubAccountType).ValueChanged += new System.EventHandler(cboSubAccountType_ValueChanged);
		resources.ApplyResources(this.lblType, "lblType");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance49");
		((ControlBase)this.lblType).Appearance = (AppearanceBase)(object)val11;
		this.lblType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblType).Name = "lblType";
		((ControlBase)this.lblType).WrapText = false;
		resources.ApplyResources(this.lblCDCExpireDate, "lblCDCExpireDate");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance50");
		((ControlBase)this.lblCDCExpireDate).Appearance = (AppearanceBase)(object)val12;
		this.lblCDCExpireDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCDCExpireDate).Name = "lblCDCExpireDate";
		((ControlBase)this.lblCDCExpireDate).WrapText = false;
		resources.ApplyResources(this.lblNameAr, "lblNameAr");
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance51");
		((ControlBase)this.lblNameAr).Appearance = (AppearanceBase)(object)val13;
		this.lblNameAr.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNameAr).Name = "lblNameAr";
		((ControlBase)this.lblNameAr).WrapText = false;
		resources.ApplyResources(this.dtpIDIssueDate, "dtpIDIssueDate");
		((UltraWinEditorMaskedControlBase)this.dtpIDIssueDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpIDIssueDate).Name = "dtpIDIssueDate";
		((System.Windows.Forms.Control)(object)this.dtpIDIssueDate).Enter += new System.EventHandler(dtpDateTime_Enter);
		resources.ApplyResources(this.dtpCDCExpireDate, "dtpCDCExpireDate");
		((UltraWinEditorMaskedControlBase)this.dtpCDCExpireDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpCDCExpireDate).Name = "dtpCDCExpireDate";
		((System.Windows.Forms.Control)(object)this.dtpCDCExpireDate).Enter += new System.EventHandler(dtpDateTime_Enter);
		resources.ApplyResources(this.txtNameAr, "txtNameAr");
		((System.Windows.Forms.Control)(object)this.txtNameAr).Name = "txtNameAr";
		resources.ApplyResources(this.lblNameEn, "lblNameEn");
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val14, "appearance52");
		((ControlBase)this.lblNameEn).Appearance = (AppearanceBase)(object)val14;
		this.lblNameEn.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNameEn).Name = "lblNameEn";
		((ControlBase)this.lblNameEn).WrapText = false;
		resources.ApplyResources(this.txtNameEn, "txtNameEn");
		((System.Windows.Forms.Control)(object)this.txtNameEn).Name = "txtNameEn";
		resources.ApplyResources(this.lblIDIssueDate, "lblIDIssueDate");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val15, "appearance53");
		((ControlBase)this.lblIDIssueDate).Appearance = (AppearanceBase)(object)val15;
		this.lblIDIssueDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblIDIssueDate).Name = "lblIDIssueDate";
		((ControlBase)this.lblIDIssueDate).WrapText = false;
		resources.ApplyResources(this.lblCDCNo, "lblCDCNo");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val16, "appearance54");
		((ControlBase)this.lblCDCNo).Appearance = (AppearanceBase)(object)val16;
		this.lblCDCNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCDCNo).Name = "lblCDCNo";
		((ControlBase)this.lblCDCNo).WrapText = false;
		resources.ApplyResources(this.txtNo, "txtNo");
		((System.Windows.Forms.Control)(object)this.txtNo).Name = "txtNo";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val17, "appearance55");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val17;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.txtCDCNo, "txtCDCNo");
		((System.Windows.Forms.Control)(object)this.txtCDCNo).Name = "txtCDCNo";
		resources.ApplyResources(this.dtpPassportExpireDate, "dtpPassportExpireDate");
		((UltraWinEditorMaskedControlBase)this.dtpPassportExpireDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpPassportExpireDate).Name = "dtpPassportExpireDate";
		((System.Windows.Forms.Control)(object)this.dtpPassportExpireDate).Enter += new System.EventHandler(dtpDateTime_Enter);
		resources.ApplyResources(this.lblPassportExpireDate, "lblPassportExpireDate");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val18).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val18, "appearance56");
		((ControlBase)this.lblPassportExpireDate).Appearance = (AppearanceBase)(object)val18;
		this.lblPassportExpireDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPassportExpireDate).Name = "lblPassportExpireDate";
		((ControlBase)this.lblPassportExpireDate).WrapText = false;
		resources.ApplyResources(this.ultraGroupBox1, "ultraGroupBox1");
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val19, "appearance57");
		this.ultraGroupBox1.Appearance = (AppearanceBase)(object)val19;
		this.ultraGroupBox1.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.cboVisaBankSettings);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel9);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.txtPrintOutCost);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.txtVisaCost);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaCost);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.chkIsPrintOut);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.dtpVisaDate);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaDate);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.lblExitDate);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.cboCompanies);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.lblVessels);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.lblCompany);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.txtVisaNo);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaState);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.cboPassengerType);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaNo);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.lblPassengerType);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.dtpValidToDate);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.cboVisaState);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.lblValidToDate);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.dtpVisaIssueDate);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.dtpActualFromDate);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.dtpEntryExpireDate);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.lblActualFromDate);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.lblEntryExpire);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.dtpActualToDate);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.cboAirPort);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.lblSignState);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.lblAirPort);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.cboSignState);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.cboVessels);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Name = "ultraGroupBox1";
		resources.ApplyResources(this.cboVisaBankSettings, "cboVisaBankSettings");
		this.cboVisaBankSettings.AutoCompleteMode = (AutoCompleteMode)4;
		val20.DataValue = "1";
		resources.ApplyResources(val20, "valueListItem5");
		((SubObjectBase)val20).ForceApplyResources = "";
		val21.DataValue = "2";
		resources.ApplyResources(val21, "valueListItem2");
		((SubObjectBase)val21).ForceApplyResources = "";
		val22.DataValue = "3";
		resources.ApplyResources(val22, "valueListItem3");
		((SubObjectBase)val22).ForceApplyResources = "";
		this.cboVisaBankSettings.Items.AddRange((ValueListItem[])(object)new ValueListItem[3] { val20, val21, val22 });
		((System.Windows.Forms.Control)(object)this.cboVisaBankSettings).Name = "cboVisaBankSettings";
		((TextEditorControlBase)this.cboVisaBankSettings).ValueChanged += new System.EventHandler(cboVisaBankSettings_ValueChanged);
		resources.ApplyResources(this.ultraLabel9, "ultraLabel9");
		((AppearanceBase)val23).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val23).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val23, "appearance58");
		((ControlBase)this.ultraLabel9).Appearance = (AppearanceBase)(object)val23;
		this.ultraLabel9.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel9).Name = "ultraLabel9";
		((ControlBase)this.ultraLabel9).WrapText = false;
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		((AppearanceBase)val24).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val24).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val24, "appearance59");
		((ControlBase)this.ultraLabel3).Appearance = (AppearanceBase)(object)val24;
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.txtPrintOutCost, "txtPrintOutCost");
		((System.Windows.Forms.Control)(object)this.txtPrintOutCost).Name = "txtPrintOutCost";
		((System.Windows.Forms.Control)(object)this.txtPrintOutCost).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPrintOutCost_KeyPress);
		resources.ApplyResources(this.txtVisaCost, "txtVisaCost");
		((System.Windows.Forms.Control)(object)this.txtVisaCost).Name = "txtVisaCost";
		((System.Windows.Forms.Control)(object)this.txtVisaCost).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtVisaCost_KeyPress);
		resources.ApplyResources(this.lblVisaCost, "lblVisaCost");
		((AppearanceBase)val25).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val25).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val25, "appearance60");
		((ControlBase)this.lblVisaCost).Appearance = (AppearanceBase)(object)val25;
		this.lblVisaCost.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaCost).Name = "lblVisaCost";
		((ControlBase)this.lblVisaCost).WrapText = false;
		resources.ApplyResources(this.chkIsPrintOut, "chkIsPrintOut");
		((AppearanceBase)val26).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val26, "appearance61");
		((UltraToggleEditorBase)this.chkIsPrintOut).Appearance = (AppearanceBase)(object)val26;
		((UltraToggleEditorBase)this.chkIsPrintOut).CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		((System.Windows.Forms.Control)(object)this.chkIsPrintOut).Name = "chkIsPrintOut";
		((UltraToggleEditorBase)this.chkIsPrintOut).CheckedChanged += new System.EventHandler(chkIsPrintOut_CheckedChanged);
		resources.ApplyResources(this.dtpVisaDate, "dtpVisaDate");
		((UltraWinEditorMaskedControlBase)this.dtpVisaDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpVisaDate).Name = "dtpVisaDate";
		this.dtpVisaDate.ValueChanged += new System.EventHandler(dtpVisaDate_ValueChanged);
		resources.ApplyResources(this.lblVisaDate, "lblVisaDate");
		((AppearanceBase)val27).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val27).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val27, "appearance62");
		((ControlBase)this.lblVisaDate).Appearance = (AppearanceBase)(object)val27;
		this.lblVisaDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaDate).Name = "lblVisaDate";
		((ControlBase)this.lblVisaDate).WrapText = false;
		resources.ApplyResources(this.lblExitDate, "lblExitDate");
		((AppearanceBase)val28).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val28).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val28, "appearance63");
		((ControlBase)this.lblExitDate).Appearance = (AppearanceBase)(object)val28;
		this.lblExitDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExitDate).Name = "lblExitDate";
		((ControlBase)this.lblExitDate).WrapText = false;
		resources.ApplyResources(this.cboCompanies, "cboCompanies");
		this.cboCompanies.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCompanies).Name = "cboCompanies";
		resources.ApplyResources(this.lblVessels, "lblVessels");
		((AppearanceBase)val29).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val29).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val29, "appearance64");
		((ControlBase)this.lblVessels).Appearance = (AppearanceBase)(object)val29;
		this.lblVessels.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVessels).Name = "lblVessels";
		((ControlBase)this.lblVessels).WrapText = false;
		resources.ApplyResources(this.lblCompany, "lblCompany");
		((AppearanceBase)val30).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val30).ForeColor = System.Drawing.Color.Navy;
		((AppearanceBase)val30).Image = resources.GetObject("appearance65.Image");
		resources.ApplyResources(val30, "appearance65");
		((ControlBase)this.lblCompany).Appearance = (AppearanceBase)(object)val30;
		this.lblCompany.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCompany).Name = "lblCompany";
		((ControlBase)this.lblCompany).WrapText = false;
		resources.ApplyResources(this.txtVisaNo, "txtVisaNo");
		((System.Windows.Forms.Control)(object)this.txtVisaNo).Name = "txtVisaNo";
		resources.ApplyResources(this.lblVisaState, "lblVisaState");
		((AppearanceBase)val31).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val31).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val31, "appearance66");
		((ControlBase)this.lblVisaState).Appearance = (AppearanceBase)(object)val31;
		this.lblVisaState.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaState).Name = "lblVisaState";
		((ControlBase)this.lblVisaState).WrapText = false;
		resources.ApplyResources(this.cboPassengerType, "cboPassengerType");
		this.cboPassengerType.AutoCompleteMode = (AutoCompleteMode)4;
		val32.DataValue = "1";
		resources.ApplyResources(val32, "valueListItem6");
		((SubObjectBase)val32).ForceApplyResources = "";
		val33.DataValue = "2";
		resources.ApplyResources(val33, "valueListItem7");
		((SubObjectBase)val33).ForceApplyResources = "";
		val34.DataValue = "3";
		resources.ApplyResources(val34, "valueListItem8");
		((SubObjectBase)val34).ForceApplyResources = "";
		this.cboPassengerType.Items.AddRange((ValueListItem[])(object)new ValueListItem[3] { val32, val33, val34 });
		((System.Windows.Forms.Control)(object)this.cboPassengerType).Name = "cboPassengerType";
		resources.ApplyResources(this.lblVisaNo, "lblVisaNo");
		((AppearanceBase)val35).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val35).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val35, "appearance67");
		((ControlBase)this.lblVisaNo).Appearance = (AppearanceBase)(object)val35;
		this.lblVisaNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaNo).Name = "lblVisaNo";
		((ControlBase)this.lblVisaNo).WrapText = false;
		resources.ApplyResources(this.lblPassengerType, "lblPassengerType");
		((AppearanceBase)val36).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val36).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val36, "appearance68");
		((ControlBase)this.lblPassengerType).Appearance = (AppearanceBase)(object)val36;
		this.lblPassengerType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPassengerType).Name = "lblPassengerType";
		((ControlBase)this.lblPassengerType).WrapText = false;
		resources.ApplyResources(this.dtpValidToDate, "dtpValidToDate");
		((UltraWinEditorMaskedControlBase)this.dtpValidToDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpValidToDate).Name = "dtpValidToDate";
		((System.Windows.Forms.Control)(object)this.dtpValidToDate).Enter += new System.EventHandler(dtpDateTime_Enter);
		resources.ApplyResources(this.cboVisaState, "cboVisaState");
		this.cboVisaState.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboVisaState).Name = "cboVisaState";
		resources.ApplyResources(this.lblValidToDate, "lblValidToDate");
		((AppearanceBase)val37).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val37).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val37, "appearance69");
		((ControlBase)this.lblValidToDate).Appearance = (AppearanceBase)(object)val37;
		this.lblValidToDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblValidToDate).Name = "lblValidToDate";
		((ControlBase)this.lblValidToDate).WrapText = false;
		resources.ApplyResources(this.dtpVisaIssueDate, "dtpVisaIssueDate");
		((UltraWinEditorMaskedControlBase)this.dtpVisaIssueDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpVisaIssueDate).Name = "dtpVisaIssueDate";
		this.dtpVisaIssueDate.ValueChanged += new System.EventHandler(dtpIssueDate_ValueChanged);
		resources.ApplyResources(this.dtpActualFromDate, "dtpActualFromDate");
		((UltraWinEditorMaskedControlBase)this.dtpActualFromDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpActualFromDate).Name = "dtpActualFromDate";
		this.dtpActualFromDate.ValueChanged += new System.EventHandler(dtpActualFromDate_ValueChanged);
		((System.Windows.Forms.Control)(object)this.dtpActualFromDate).Enter += new System.EventHandler(dtpDateTime_Enter);
		resources.ApplyResources(this.dtpEntryExpireDate, "dtpEntryExpireDate");
		((UltraWinEditorMaskedControlBase)this.dtpEntryExpireDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpEntryExpireDate).Name = "dtpEntryExpireDate";
		((System.Windows.Forms.Control)(object)this.dtpEntryExpireDate).Enter += new System.EventHandler(dtpDateTime_Enter);
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblActualFromDate, "lblActualFromDate");
		((AppearanceBase)val38).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val38).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val38, "appearance70");
		((ControlBase)this.lblActualFromDate).Appearance = (AppearanceBase)(object)val38;
		this.lblActualFromDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblActualFromDate).Name = "lblActualFromDate";
		((ControlBase)this.lblActualFromDate).WrapText = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((AppearanceBase)val39).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val39, "appearance71");
		((ControlBase)this.lblNotes).Appearance = (AppearanceBase)(object)val39;
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.lblEntryExpire, "lblEntryExpire");
		((AppearanceBase)val40).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val40).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val40, "appearance72");
		((ControlBase)this.lblEntryExpire).Appearance = (AppearanceBase)(object)val40;
		this.lblEntryExpire.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEntryExpire).Name = "lblEntryExpire";
		((ControlBase)this.lblEntryExpire).WrapText = false;
		resources.ApplyResources(this.dtpActualToDate, "dtpActualToDate");
		((UltraWinEditorMaskedControlBase)this.dtpActualToDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpActualToDate).Name = "dtpActualToDate";
		((System.Windows.Forms.Control)(object)this.dtpActualToDate).Enter += new System.EventHandler(dtpDateTime_Enter);
		resources.ApplyResources(this.cboAirPort, "cboAirPort");
		this.cboAirPort.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboAirPort).Name = "cboAirPort";
		resources.ApplyResources(this.lblSignState, "lblSignState");
		((AppearanceBase)val41).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val41).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val41, "appearance73");
		((ControlBase)this.lblSignState).Appearance = (AppearanceBase)(object)val41;
		this.lblSignState.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSignState).Name = "lblSignState";
		((ControlBase)this.lblSignState).WrapText = false;
		resources.ApplyResources(this.lblAirPort, "lblAirPort");
		((AppearanceBase)val42).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val42).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val42, "appearance74");
		((ControlBase)this.lblAirPort).Appearance = (AppearanceBase)(object)val42;
		this.lblAirPort.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAirPort).Name = "lblAirPort";
		((ControlBase)this.lblAirPort).WrapText = false;
		resources.ApplyResources(this.cboSignState, "cboSignState");
		this.cboSignState.AutoCompleteMode = (AutoCompleteMode)4;
		val43.DataValue = true;
		resources.ApplyResources(val43, "valueListItem1");
		((SubObjectBase)val43).ForceApplyResources = "";
		val44.DataValue = false;
		resources.ApplyResources(val44, "valueListItem4");
		((SubObjectBase)val44).ForceApplyResources = "";
		this.cboSignState.Items.AddRange((ValueListItem[])(object)new ValueListItem[2] { val43, val44 });
		((System.Windows.Forms.Control)(object)this.cboSignState).Name = "cboSignState";
		resources.ApplyResources(this.cboVessels, "cboVessels");
		this.cboVessels.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboVessels).Name = "cboVessels";
		resources.ApplyResources(this.chkIsUrgent, "chkIsUrgent");
		((AppearanceBase)val45).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val45, "appearance75");
		((UltraToggleEditorBase)this.chkIsUrgent).Appearance = (AppearanceBase)(object)val45;
		((UltraToggleEditorBase)this.chkIsUrgent).CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		((System.Windows.Forms.Control)(object)this.chkIsUrgent).Name = "chkIsUrgent";
		((UltraToggleEditorBase)this.chkIsUrgent).CheckedChanged += new System.EventHandler(chkIsUrgent_CheckedChanged);
		resources.ApplyResources(this.txtVisaSerial, "txtVisaSerial");
		((System.Windows.Forms.Control)(object)this.txtVisaSerial).Name = "txtVisaSerial";
		((EditorButtonControlBase)this.txtVisaSerial).ReadOnly = true;
		resources.ApplyResources(this.lblVisaSerialNo, "lblVisaSerialNo");
		((AppearanceBase)val46).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val46).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val46, "appearance76");
		((ControlBase)this.lblVisaSerialNo).Appearance = (AppearanceBase)(object)val46;
		this.lblVisaSerialNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaSerialNo).Name = "lblVisaSerialNo";
		((ControlBase)this.lblVisaSerialNo).WrapText = false;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsUrgent);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraGroupBox1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBDetails);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaSerialNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPassportNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVisaSerial);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPassportNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmInsertSeaManVisa";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPassportNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVisaSerial, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPassportNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaSerialNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraGroupBox1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsUrgent, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassportNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBDetails).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.UGBDetails).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cboNationality).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtIDNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccountGroup).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccountType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpIDIssueDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCDCExpireDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCDCNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpPassportExpireDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraGroupBox1).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cboVisaBankSettings).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPrintOutCost).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaCost).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsPrintOut).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpVisaDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCompanies).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPassengerType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpValidToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaState).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpVisaIssueDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpActualFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpEntryExpireDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpActualToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAirPort).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSignState).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVessels).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsUrgent).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaSerial).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
