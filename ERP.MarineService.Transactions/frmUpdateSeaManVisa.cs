using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.MarineService;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.MarineService.Transactions;

public class frmUpdateSeaManVisa : frmBase
{
	private DataTable dtAirPorts;

	private DataTable dtVisaStates;

	private DataTable dtVessels;

	private DataTable dtSettings;

	private DataTable dtSettingsVisaBanks;

	private DataTable dtCompanies;

	private DataTable dtRejectionReasons;

	private int VisaActualPeriodDays = 0;

	private int VisaValidPeriodDays = 0;

	private string VisaCost;

	private string UrgentVisaCost;

	private string VisaPrintOutCost;

	private DataRow drMaster;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraButton btnSave;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	private UltraLabel lblPassportNo;

	private UltraTextEditor txtPassportNo;

	private UltraTextEditor txtNameAr;

	private UltraTextEditor txtVisaNo;

	private UltraLabel lblVisaNo;

	private UltraComboEditor cboVisaState;

	private UltraLabel lblActualFromDate;

	private UltraDateTimeEditor dtpActualFromDate;

	private UltraLabel lblValidToDate;

	private UltraDateTimeEditor dtpValidToDate;

	private UltraLabel lblIDNo;

	private UltraTextEditor txtIDNo;

	private UltraComboEditor cboSignState;

	private UltraLabel lblSignState;

	private UltraLabel lblNationality;

	private UltraDateTimeEditor dtpIDIssueDate;

	private UltraLabel lblIDIssueDate;

	private UltraLabel lblVisaState;

	private UltraComboEditor cboVessels;

	private UltraLabel lblVessels;

	private UltraComboEditor cboAirPort;

	private UltraLabel lblAirPort;

	private UltraDateTimeEditor dtpVisaIssueDate;

	private UltraComboEditor cboPassengerType;

	private UltraLabel lblPassengerType;

	private UltraDateTimeEditor dtpEntryExpireDate;

	private UltraLabel lblEntryExpire;

	private UltraCheckEditor chkIsUrgent;

	private UltraComboEditor cboCompanies;

	private UltraDateTimeEditor dtpVisaDate;

	private UltraLabel lblVisaDate;

	private UltraTextEditor txtVisaCost;

	private UltraLabel lblVisaCost;

	private UltraCheckEditor chkIsPrintOut;

	private UltraLabel ultraLabel3;

	private UltraTextEditor txtVisaSerial;

	private UltraLabel lblVisaSerialNo;

	private UltraTextEditor txtPrintOutCost;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraTextEditor txtNationality;

	private UltraLabel ultraLabel4;

	private UltraDateTimeEditor dtpActualExitDate;

	private UltraTextEditor txtVoyage;

	private UltraLabel ultraLabel5;

	private UltraCheckEditor chkIsCompleted;

	private UltraCheckEditor chkIsRejected;

	private UltraLabel ultraLabel6;

	private UltraLabel ultraLabel7;

	private UltraTextEditor txtCancellationReason;

	private UltraCheckEditor chkIsCancelled;

	private UltraDateTimeEditor dtpCancellationDate;

	private UltraLabel ultraLabel8;

	private UltraComboEditor cboRejectionReason;

	private UltraLabel lblBankName;

	private UltraComboEditor cboVisaBankSettings;

	public UltraButton btnUpdateIDNo;

	public UltraLabel lblHistory;

	public frmUpdateSeaManVisa()
	{
		InitializeComponent();
	}

	public frmUpdateSeaManVisa(string OPERATIONSERVICEVISAID)
		: this()
	{
		RowID = OPERATIONSERVICEVISAID;
		TableName = "MS_OperationsServicesVisas";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtRejectionReasons = RejectionReasons.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboRejectionReason, dtRejectionReasons, "RejectionReasonID", "RejectionReasonName");
		dtVessels = Vessels.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboVessels, dtVessels, "VesselID", "VesselName");
		dtVisaStates = VisaStates.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboVisaState, dtVisaStates, "VisaStateID", "VisaStateName");
		dtCompanies = Companies.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
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
		dtpVisaIssueDate.ValueChanged -= dtpIssueDate_ValueChanged;
		dtpActualFromDate.ValueChanged -= dtpActualFromDate_ValueChanged;
		UltraDateTimeEditor obj = dtpVisaIssueDate;
		UltraDateTimeEditor obj2 = dtpValidToDate;
		UltraDateTimeEditor obj3 = dtpActualFromDate;
		UltraDateTimeEditor obj4 = dtpEntryExpireDate;
		object obj5 = (dtpIDIssueDate.Value = null);
		object obj7 = (obj4.Value = obj5);
		object obj9 = (obj3.Value = obj7);
		object value = (obj2.Value = obj9);
		obj.Value = value;
		dtpVisaIssueDate.ValueChanged += dtpIssueDate_ValueChanged;
		dtpActualFromDate.ValueChanged += dtpActualFromDate_ValueChanged;
		cboPassengerType.SelectedIndex = 0;
		((Control)(object)txtVisaCost).Text = VisaCost;
		((Control)(object)txtVisaSerial).Text = OperationsServicesVisas.GetCodeByBranchID(dtpVisaDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = OperationsServicesVisas.SelectByID(RowID, GlobalVariables.IsArabic ? "1" : "0");
			drMaster = ((dataTable.Rows.Count > 0) ? dataTable.Rows[0] : null);
		}
		dtSettingsVisaBanks = SettingsVisaBanks.FillCombo((GlobalVariables.CurrentBranchID == drMaster["BranchID"].ToString()) ? drMaster["BranchID"].ToString() : "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboVisaBankSettings, dtSettingsVisaBanks, "SettingVisaBankID", "VisaBankName");
		DisplayData();
	}

	public bool ValidateData()
	{
		if (GlobalVariables.CurrentBranchID != drMaster["BranchID"].ToString())
		{
			GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لإنها تابعة لفرع آخر", "Cannot Update This Transaction Because It Related to Another Branch ");
			return false;
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
			if (Main.CheckForValueByBranchIDAndFiscalYearID("MS_OperationsServicesVisas", "OperationServiceVisaNo", ((Control)(object)txtVisaSerial).Text, drMaster["OperationServiceVisaNo"].ToString(), GlobalVariables.CurrentBranchID, "VisaDate", dtpVisaDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
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
			if (dtpActualFromDate.Value != null && dtpVisaIssueDate.Value != null && Convert.ToDateTime(dtpActualFromDate.Value) < Convert.ToDateTime(dtpVisaIssueDate.Value))
			{
				GlobalVariables.InformationMB.Show("تاريخ الدخول قبل تاريخ اصدار التأشيرة", "The Issue date is after the Entry date and this is not allowed");
				((Control)(object)dtpActualFromDate).Focus();
				return false;
			}
			if (dtpActualFromDate.Value != null && dtpValidToDate.Value != null && Convert.ToDateTime(dtpActualFromDate.Value) > Convert.ToDateTime(dtpValidToDate.Value))
			{
				GlobalVariables.InformationMB.Show("تاريخ الدخول بعد تاريخ انتهاء التأشيرة", "The Entry date is after the Expiry date and this is not allowed");
				((Control)(object)dtpValidToDate).Focus();
				return false;
			}
			if (dtpActualExitDate.Value != null && dtpActualFromDate.Value != null && Convert.ToDateTime(dtpActualExitDate.Value) < Convert.ToDateTime(dtpActualFromDate.Value))
			{
				GlobalVariables.InformationMB.Show("تاريخ الخروج قبل تاريخ الدخول", "The Entry date is after the Exit date and this is not allowed");
				((Control)(object)dtpActualExitDate).Focus();
				return false;
			}
			return true;
		}
		GlobalVariables.InformationMB.Show("برجاء إدخال اعدادات التأشيرة في اعدادات الخدمات البحريه", "Please Enter Visa Settings in Marine Services Settings");
		return false;
	}

	public void DisplayData()
	{
		if (drMaster != null)
		{
			((Control)(object)lblHistory).Text = Trans_Log.GetRowHistory(TableName, RowID, GlobalVariables.IsArabic ? "1" : "0");
			DisplayDataDate = GlobalFunctions.GetServerDateTimeNow();
			((UltraToggleEditorBase)chkIsCancelled).CheckedChanged -= chkIsCancelled_CheckedChanged;
			((UltraToggleEditorBase)chkIsPrintOut).CheckedChanged -= chkIsPrintOut_CheckedChanged;
			((UltraToggleEditorBase)chkIsRejected).CheckedChanged -= chkIsRejected_CheckedChanged;
			((UltraToggleEditorBase)chkIsUrgent).CheckedChanged -= chkIsUrgent_CheckedChanged;
			((TextEditorControlBase)cboVisaBankSettings).ValueChanged -= cboVisaBankSettings_ValueChanged;
			dtpActualFromDate.ValueChanged -= dtpActualFromDate_ValueChanged;
			dtpVisaIssueDate.ValueChanged -= dtpIssueDate_ValueChanged;
			dtpVisaDate.ValueChanged -= dtpVisaDate_ValueChanged;
			((TextEditorControlBase)txtNationality).Value = drMaster["NationalityName"];
			((TextEditorControlBase)cboVessels).Value = drMaster["VesselID"];
			dtpVisaDate.Value = (DateTime)drMaster["VisaDate"];
			((Control)(object)txtVisaSerial).Text = drMaster["OperationServiceVisaNo"].ToString();
			((Control)(object)txtNameAr).Text = drMaster["SubAccountName"].ToString();
			((Control)(object)txtPassportNo).Text = drMaster["PassportNo"].ToString();
			((Control)(object)txtVisaNo).Text = drMaster["VisaNo"].ToString();
			((TextEditorControlBase)cboSignState).Value = drMaster["IsSignOn"];
			((Control)(object)txtIDNo).Text = drMaster["IDNo"].ToString();
			dtpIDIssueDate.Value = drMaster["IDIssueDate"];
			dtpVisaIssueDate.Value = drMaster["ValidFromDate"];
			dtpValidToDate.Value = drMaster["ValidToDate"];
			dtpActualFromDate.Value = drMaster["ActualFromDate"];
			dtpActualExitDate.Value = drMaster["ActualToDate"];
			dtpEntryExpireDate.Value = drMaster["EntryExpireDate"];
			((Control)(object)txtVoyage).Text = drMaster["VoyageNo"].ToString();
			((TextEditorControlBase)cboVisaState).Value = drMaster["VisaStateID"];
			((UltraToggleEditorBase)chkIsCompleted).Checked = Convert.ToBoolean(drMaster["IsCompleted"]);
			((UltraToggleEditorBase)chkIsPrintOut).Checked = Convert.ToBoolean(drMaster["IsPrintOut"]);
			((Control)(object)txtVisaCost).Text = decimal.Parse(drMaster["VisaCost"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtPrintOutCost).Text = decimal.Parse(drMaster["PrintOutcost"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)cboAirPort).Value = drMaster["AirPortID"];
			((TextEditorControlBase)cboRejectionReason).Value = drMaster["RejectionReasonID"];
			((UltraToggleEditorBase)chkIsRejected).Checked = Convert.ToBoolean(drMaster["IsRejected"]);
			((TextEditorControlBase)cboCompanies).Value = drMaster["CompanyID"];
			((UltraToggleEditorBase)chkIsUrgent).Checked = Convert.ToBoolean(drMaster["IsUrgent"]);
			((TextEditorControlBase)cboPassengerType).Value = drMaster["PassengerType"];
			((UltraToggleEditorBase)chkIsCancelled).Checked = Convert.ToBoolean(drMaster["IsCancelled"]);
			dtpCancellationDate.Value = drMaster["CancellationDate"];
			((Control)(object)txtCancellationReason).Text = drMaster["CancellationReason"].ToString();
			((TextEditorControlBase)cboVisaBankSettings).Value = drMaster["SettingVisaBankID"];
			((Control)(object)txtPrintOutCost).Enabled = ((UltraToggleEditorBase)chkIsPrintOut).Checked;
			UltraTextEditor obj = txtCancellationReason;
			bool enabled = (((Control)(object)dtpCancellationDate).Enabled = ((UltraToggleEditorBase)chkIsCancelled).Checked);
			((Control)(object)obj).Enabled = enabled;
			((Control)(object)cboRejectionReason).Enabled = ((UltraToggleEditorBase)chkIsRejected).Checked;
			if (cboVisaBankSettings.SelectedIndex > -1)
			{
				VisaCost = dtSettingsVisaBanks.Select("SettingVisaBankID = " + ((TextEditorControlBase)cboVisaBankSettings).Value.ToString())[0]["VisaCost"].ToString();
				UrgentVisaCost = dtSettingsVisaBanks.Select("SettingVisaBankID = " + ((TextEditorControlBase)cboVisaBankSettings).Value.ToString())[0]["UrgentVisaCost"].ToString();
			}
			((UltraToggleEditorBase)chkIsCancelled).CheckedChanged += chkIsCancelled_CheckedChanged;
			((UltraToggleEditorBase)chkIsPrintOut).CheckedChanged += chkIsPrintOut_CheckedChanged;
			((UltraToggleEditorBase)chkIsRejected).CheckedChanged += chkIsRejected_CheckedChanged;
			((UltraToggleEditorBase)chkIsUrgent).CheckedChanged += chkIsUrgent_CheckedChanged;
			((TextEditorControlBase)cboVisaBankSettings).ValueChanged += cboVisaBankSettings_ValueChanged;
			dtpActualFromDate.ValueChanged += dtpActualFromDate_ValueChanged;
			dtpVisaIssueDate.ValueChanged += dtpIssueDate_ValueChanged;
			dtpVisaDate.ValueChanged += dtpVisaDate_ValueChanged;
			if (drMaster["OperationID"] != DBNull.Value)
			{
				((Control)(object)cboVessels).Enabled = false;
				((Control)(object)cboPassengerType).Enabled = false;
				((Control)(object)cboRejectionReason).Enabled = false;
				((Control)(object)chkIsRejected).Enabled = false;
			}
		}
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (!ValidateData())
		{
			return;
		}
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = OperationsServicesVisas.UpdateByTable_ForVisaTracking(drMaster["OperationServiceVisaID"].ToString(), ((Control)(object)txtVisaSerial).Text, (dtpVisaDate.Value == null) ? "Null" : dtpVisaDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboVessels.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboVessels).Value.ToString(), (cboCompanies.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCompanies).Value.ToString(), drMaster["SubAccountID"].ToString(), ((UltraToggleEditorBase)chkIsUrgent).Checked ? "1" : "0", (cboPassengerType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPassengerType).Value.ToString(), bool.Parse(cboSignState.SelectedItem.DataValue.ToString()) ? "1" : "0", (cboVisaState.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboVisaState).Value.ToString(), ((Control)(object)txtVisaNo).Text, ((UltraToggleEditorBase)chkIsRejected).Checked ? "1" : "0", (cboRejectionReason.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboRejectionReason).Value.ToString(), ((UltraToggleEditorBase)chkIsCancelled).Checked ? "1" : "0", (dtpCancellationDate.Value == null) ? "Null" : dtpCancellationDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtCancellationReason).Text, (dtpVisaIssueDate.Value == null) ? "Null" : dtpVisaIssueDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpValidToDate.Value == null) ? "Null" : dtpValidToDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpActualFromDate.Value == null) ? "Null" : dtpActualFromDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpEntryExpireDate.Value == null) ? "Null" : dtpEntryExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpActualExitDate.Value == null) ? "Null" : dtpActualExitDate.DateTime.ToString(GlobalVariables.DateShortFormate), (drMaster["AirLineID"] == DBNull.Value) ? "Null" : drMaster["AirLineID"].ToString(), (cboAirPort.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAirPort).Value.ToString(), drMaster["Notes"].ToString(), ((TextEditorControlBase)cboVisaBankSettings).Value.ToString(), (((UltraToggleEditorBase)chkIsPrintOut).Checked && drMaster["PrintSupplierAccountID"] != DBNull.Value) ? drMaster["PrintSupplierAccountID"].ToString() : dtSettings.Select("BranchID = " + GlobalVariables.CurrentBranchID.ToString())[0]["VisaPrintSupplierAccountID"].ToString(), (((UltraToggleEditorBase)chkIsPrintOut).Checked && drMaster["PrintSupplierSubAccountID"] != DBNull.Value) ? drMaster["PrintSupplierSubAccountID"].ToString() : ((dtSettings.Select("BranchID = " + GlobalVariables.CurrentBranchID.ToString())[0]["VisaPrintSupplierSubAccountID"] == DBNull.Value) ? "Null" : dtSettings.Select("BranchID = " + GlobalVariables.CurrentBranchID.ToString())[0]["VisaPrintSupplierSubAccountID"].ToString()), (((Control)(object)txtVisaCost).Text == "") ? "0" : ((Control)(object)txtVisaCost).Text, ((UltraToggleEditorBase)chkIsPrintOut).Checked ? "1" : "0", (((Control)(object)txtPrintOutCost).Text == "") ? "0" : ((Control)(object)txtPrintOutCost).Text, (drMaster["ExpenseJVID"] == DBNull.Value) ? "Null" : drMaster["ExpenseJVID"].ToString(), ((UltraToggleEditorBase)chkIsCompleted).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			if (drMaster["OperationServiceID"].ToString() != "")
			{
				OperationsServices.UpdateTotalExpenses(drMaster["OperationServiceID"].ToString(), GlobalVariables.UserID);
			}
			if (drMaster["ExpenseJVID"] != DBNull.Value && decimal.Parse((((Control)(object)txtPrintOutCost).Text == "") ? "0" : ((Control)(object)txtPrintOutCost).Text) + decimal.Parse((((Control)(object)txtVisaCost).Text == "") ? "0" : ((Control)(object)txtVisaCost).Text) == 0m)
			{
				JVDetails.DeleteVirtualByJVID(drMaster["ExpenseJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
				JV.DeleteVirtual(drMaster["ExpenseJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			OperationsServicesVisas.GenerateJvs(num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
			Close();
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

	private void txtVisaCost_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers((object)txtVisaCost, e);
	}

	private void txtPrintOutCost_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers((object)txtPrintOutCost, e);
	}

	private void dtpVisaDate_ValueChanged(object sender, EventArgs e)
	{
		((Control)(object)txtVisaSerial).Text = OperationsServicesVisas.GetCodeByBranchID(dtpVisaDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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

	private void chkIsPrintOut_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)txtPrintOutCost).Enabled = ((UltraToggleEditorBase)chkIsPrintOut).Checked;
		if (((UltraToggleEditorBase)chkIsPrintOut).Checked)
		{
			((Control)(object)txtPrintOutCost).Text = decimal.Parse(VisaPrintOutCost).ToString(GlobalVariables.txtDecimalFormate);
		}
		else
		{
			((Control)(object)txtPrintOutCost).Text = "0";
		}
	}

	private void chkIsRejected_CheckedChanged(object sender, EventArgs e)
	{
		((UltraToggleEditorBase)chkIsRejected).CheckedChanged -= chkIsRejected_CheckedChanged;
		if (((UltraToggleEditorBase)chkIsRejected).Checked)
		{
			((UltraToggleEditorBase)chkIsCancelled).Checked = false;
		}
		else
		{
			cboRejectionReason.SelectedIndex = -1;
		}
		((Control)(object)cboRejectionReason).Enabled = ((UltraToggleEditorBase)chkIsRejected).Checked;
		((UltraToggleEditorBase)chkIsRejected).CheckedChanged += chkIsRejected_CheckedChanged;
	}

	private void chkIsCancelled_CheckedChanged(object sender, EventArgs e)
	{
		((UltraToggleEditorBase)chkIsCancelled).CheckedChanged -= chkIsCancelled_CheckedChanged;
		if (((UltraToggleEditorBase)chkIsCancelled).Checked)
		{
			((UltraToggleEditorBase)chkIsRejected).Checked = false;
		}
		else
		{
			((Control)(object)txtCancellationReason).Text = "";
			dtpCancellationDate.Value = null;
		}
		((Control)(object)txtCancellationReason).Enabled = ((UltraToggleEditorBase)chkIsCancelled).Checked;
		((Control)(object)dtpCancellationDate).Enabled = ((UltraToggleEditorBase)chkIsCancelled).Checked;
		((UltraToggleEditorBase)chkIsCancelled).CheckedChanged += chkIsCancelled_CheckedChanged;
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

	private void btnUpdateIDNo_Click(object sender, EventArgs e)
	{
		if (drMaster != null)
		{
			frmUpdateIDNo frmUpdateIDNo2 = new frmUpdateIDNo(drMaster["SubAccountID"].ToString(), drMaster["SubAccountTypeID"].ToString(), GlobalVariables.IsArabic ? ":رقم الهوية" : "ID No.:", "IDNo", drMaster["IDNo"].ToString());
			frmUpdateIDNo2.WindowState = FormWindowState.Normal;
			frmUpdateIDNo2.ShowDialog();
			if (frmUpdateIDNo2.Value != "")
			{
				((Control)(object)txtIDNo).Text = frmUpdateIDNo2.Value;
			}
		}
	}

	private void lblHistory_Click(object sender, EventArgs e)
	{
		if (TableName != "" && RowID != "")
		{
			frmHistory frmHistory2 = new frmHistory(Trans_Log.SelectByRowID(TableName, RowID, GlobalVariables.IsArabic ? "1" : "0"));
			frmHistory2.StartPosition = FormStartPosition.CenterScreen;
			((Control)(object)frmHistory2.lblTitle).Text = "History";
			frmHistory2.ShowDialog();
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
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Expected O, but got Unknown
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Expected O, but got Unknown
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Expected O, but got Unknown
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmUpdateSeaManVisa));
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
		ValueListItem val16 = new ValueListItem();
		ValueListItem val17 = new ValueListItem();
		ValueListItem val18 = new ValueListItem();
		Appearance val19 = new Appearance();
		Appearance val20 = new Appearance();
		Appearance val21 = new Appearance();
		Appearance val22 = new Appearance();
		Appearance val23 = new Appearance();
		Appearance val24 = new Appearance();
		Appearance val25 = new Appearance();
		ValueListItem val26 = new ValueListItem();
		ValueListItem val27 = new ValueListItem();
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
		ValueListItem val41 = new ValueListItem();
		ValueListItem val42 = new ValueListItem();
		ValueListItem val43 = new ValueListItem();
		Appearance val44 = new Appearance();
		this.btnKeyboard = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.lblPassportNo = new UltraLabel();
		this.txtPassportNo = new UltraTextEditor();
		this.lblNationality = new UltraLabel();
		this.lblIDNo = new UltraLabel();
		this.txtIDNo = new UltraTextEditor();
		this.dtpIDIssueDate = new UltraDateTimeEditor();
		this.lblIDIssueDate = new UltraLabel();
		this.txtNameAr = new UltraTextEditor();
		this.ultraLabel3 = new UltraLabel();
		this.txtPrintOutCost = new UltraTextEditor();
		this.txtVisaCost = new UltraTextEditor();
		this.lblVisaCost = new UltraLabel();
		this.chkIsPrintOut = new UltraCheckEditor();
		this.dtpVisaDate = new UltraDateTimeEditor();
		this.lblVisaDate = new UltraLabel();
		this.cboCompanies = new UltraComboEditor();
		this.lblVessels = new UltraLabel();
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
		this.lblActualFromDate = new UltraLabel();
		this.lblEntryExpire = new UltraLabel();
		this.cboAirPort = new UltraComboEditor();
		this.lblSignState = new UltraLabel();
		this.lblAirPort = new UltraLabel();
		this.cboSignState = new UltraComboEditor();
		this.cboVessels = new UltraComboEditor();
		this.chkIsUrgent = new UltraCheckEditor();
		this.txtVisaSerial = new UltraTextEditor();
		this.lblVisaSerialNo = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.txtNationality = new UltraTextEditor();
		this.ultraLabel4 = new UltraLabel();
		this.dtpActualExitDate = new UltraDateTimeEditor();
		this.txtVoyage = new UltraTextEditor();
		this.ultraLabel5 = new UltraLabel();
		this.chkIsCompleted = new UltraCheckEditor();
		this.chkIsRejected = new UltraCheckEditor();
		this.ultraLabel6 = new UltraLabel();
		this.ultraLabel7 = new UltraLabel();
		this.txtCancellationReason = new UltraTextEditor();
		this.chkIsCancelled = new UltraCheckEditor();
		this.dtpCancellationDate = new UltraDateTimeEditor();
		this.ultraLabel8 = new UltraLabel();
		this.cboRejectionReason = new UltraComboEditor();
		this.lblBankName = new UltraLabel();
		this.cboVisaBankSettings = new UltraComboEditor();
		this.btnUpdateIDNo = new UltraButton();
		this.lblHistory = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassportNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtIDNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpIDIssueDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).BeginInit();
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
		((System.ComponentModel.ISupportInitialize)this.cboAirPort).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSignState).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVessels).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsUrgent).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaSerial).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNationality).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpActualExitDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVoyage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsCompleted).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsRejected).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCancellationReason).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsCancelled).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCancellationDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboRejectionReason).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaBankSettings).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val, "appearance37");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val2).Image = resources.GetObject("appearance38.Image");
		resources.ApplyResources(val2, "appearance38");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val3).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val3, "appearance39");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val4).Image = resources.GetObject("appearance40.Image");
		resources.ApplyResources(val4, "appearance40");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val4;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val5, "appearance41");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblPassportNo, "lblPassportNo");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val6, "appearance42");
		((ControlBase)this.lblPassportNo).Appearance = (AppearanceBase)(object)val6;
		this.lblPassportNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPassportNo).Name = "lblPassportNo";
		((ControlBase)this.lblPassportNo).WrapText = false;
		resources.ApplyResources(this.txtPassportNo, "txtPassportNo");
		((System.Windows.Forms.Control)(object)this.txtPassportNo).Name = "txtPassportNo";
		resources.ApplyResources(this.lblNationality, "lblNationality");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance43");
		((ControlBase)this.lblNationality).Appearance = (AppearanceBase)(object)val7;
		this.lblNationality.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNationality).Name = "lblNationality";
		((ControlBase)this.lblNationality).WrapText = false;
		resources.ApplyResources(this.lblIDNo, "lblIDNo");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance44");
		((ControlBase)this.lblIDNo).Appearance = (AppearanceBase)(object)val8;
		this.lblIDNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblIDNo).Name = "lblIDNo";
		((ControlBase)this.lblIDNo).WrapText = false;
		resources.ApplyResources(this.txtIDNo, "txtIDNo");
		((System.Windows.Forms.Control)(object)this.txtIDNo).Name = "txtIDNo";
		resources.ApplyResources(this.dtpIDIssueDate, "dtpIDIssueDate");
		((UltraWinEditorMaskedControlBase)this.dtpIDIssueDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpIDIssueDate).Name = "dtpIDIssueDate";
		((System.Windows.Forms.Control)(object)this.dtpIDIssueDate).Enter += new System.EventHandler(dtpDateTime_Enter);
		resources.ApplyResources(this.lblIDIssueDate, "lblIDIssueDate");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance45");
		((ControlBase)this.lblIDIssueDate).Appearance = (AppearanceBase)(object)val9;
		this.lblIDIssueDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblIDIssueDate).Name = "lblIDIssueDate";
		((ControlBase)this.lblIDIssueDate).WrapText = false;
		resources.ApplyResources(this.txtNameAr, "txtNameAr");
		((System.Windows.Forms.Control)(object)this.txtNameAr).Name = "txtNameAr";
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance46");
		((ControlBase)this.ultraLabel3).Appearance = (AppearanceBase)(object)val10;
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
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance47");
		((ControlBase)this.lblVisaCost).Appearance = (AppearanceBase)(object)val11;
		this.lblVisaCost.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaCost).Name = "lblVisaCost";
		((ControlBase)this.lblVisaCost).WrapText = false;
		resources.ApplyResources(this.chkIsPrintOut, "chkIsPrintOut");
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance48");
		((UltraToggleEditorBase)this.chkIsPrintOut).Appearance = (AppearanceBase)(object)val12;
		((UltraToggleEditorBase)this.chkIsPrintOut).CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		((System.Windows.Forms.Control)(object)this.chkIsPrintOut).Name = "chkIsPrintOut";
		((UltraToggleEditorBase)this.chkIsPrintOut).CheckedChanged += new System.EventHandler(chkIsPrintOut_CheckedChanged);
		resources.ApplyResources(this.dtpVisaDate, "dtpVisaDate");
		((UltraWinEditorMaskedControlBase)this.dtpVisaDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpVisaDate).Name = "dtpVisaDate";
		this.dtpVisaDate.ValueChanged += new System.EventHandler(dtpVisaDate_ValueChanged);
		resources.ApplyResources(this.lblVisaDate, "lblVisaDate");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance49");
		((ControlBase)this.lblVisaDate).Appearance = (AppearanceBase)(object)val13;
		this.lblVisaDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaDate).Name = "lblVisaDate";
		((ControlBase)this.lblVisaDate).WrapText = false;
		resources.ApplyResources(this.cboCompanies, "cboCompanies");
		this.cboCompanies.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCompanies).Name = "cboCompanies";
		resources.ApplyResources(this.lblVessels, "lblVessels");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val14, "appearance50");
		((ControlBase)this.lblVessels).Appearance = (AppearanceBase)(object)val14;
		this.lblVessels.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVessels).Name = "lblVessels";
		((ControlBase)this.lblVessels).WrapText = false;
		resources.ApplyResources(this.txtVisaNo, "txtVisaNo");
		((System.Windows.Forms.Control)(object)this.txtVisaNo).Name = "txtVisaNo";
		resources.ApplyResources(this.lblVisaState, "lblVisaState");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val15, "appearance51");
		((ControlBase)this.lblVisaState).Appearance = (AppearanceBase)(object)val15;
		this.lblVisaState.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaState).Name = "lblVisaState";
		((ControlBase)this.lblVisaState).WrapText = false;
		resources.ApplyResources(this.cboPassengerType, "cboPassengerType");
		this.cboPassengerType.AutoCompleteMode = (AutoCompleteMode)4;
		val16.DataValue = "1";
		resources.ApplyResources(val16, "valueListItem6");
		((SubObjectBase)val16).ForceApplyResources = "";
		val17.DataValue = "2";
		resources.ApplyResources(val17, "valueListItem7");
		((SubObjectBase)val17).ForceApplyResources = "";
		val18.DataValue = "3";
		resources.ApplyResources(val18, "valueListItem8");
		((SubObjectBase)val18).ForceApplyResources = "";
		this.cboPassengerType.Items.AddRange((ValueListItem[])(object)new ValueListItem[3] { val16, val17, val18 });
		((System.Windows.Forms.Control)(object)this.cboPassengerType).Name = "cboPassengerType";
		resources.ApplyResources(this.lblVisaNo, "lblVisaNo");
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val19, "appearance52");
		((ControlBase)this.lblVisaNo).Appearance = (AppearanceBase)(object)val19;
		this.lblVisaNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaNo).Name = "lblVisaNo";
		((ControlBase)this.lblVisaNo).WrapText = false;
		resources.ApplyResources(this.lblPassengerType, "lblPassengerType");
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val20, "appearance53");
		((ControlBase)this.lblPassengerType).Appearance = (AppearanceBase)(object)val20;
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
		((AppearanceBase)val21).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val21).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val21, "appearance54");
		((ControlBase)this.lblValidToDate).Appearance = (AppearanceBase)(object)val21;
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
		resources.ApplyResources(this.lblActualFromDate, "lblActualFromDate");
		((AppearanceBase)val22).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val22).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val22, "appearance55");
		((ControlBase)this.lblActualFromDate).Appearance = (AppearanceBase)(object)val22;
		this.lblActualFromDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblActualFromDate).Name = "lblActualFromDate";
		((ControlBase)this.lblActualFromDate).WrapText = false;
		resources.ApplyResources(this.lblEntryExpire, "lblEntryExpire");
		((AppearanceBase)val23).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val23).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val23, "appearance56");
		((ControlBase)this.lblEntryExpire).Appearance = (AppearanceBase)(object)val23;
		this.lblEntryExpire.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEntryExpire).Name = "lblEntryExpire";
		((ControlBase)this.lblEntryExpire).WrapText = false;
		resources.ApplyResources(this.cboAirPort, "cboAirPort");
		this.cboAirPort.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboAirPort).Name = "cboAirPort";
		resources.ApplyResources(this.lblSignState, "lblSignState");
		((AppearanceBase)val24).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val24).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val24, "appearance57");
		((ControlBase)this.lblSignState).Appearance = (AppearanceBase)(object)val24;
		this.lblSignState.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSignState).Name = "lblSignState";
		((ControlBase)this.lblSignState).WrapText = false;
		resources.ApplyResources(this.lblAirPort, "lblAirPort");
		((AppearanceBase)val25).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val25).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val25, "appearance58");
		((ControlBase)this.lblAirPort).Appearance = (AppearanceBase)(object)val25;
		this.lblAirPort.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAirPort).Name = "lblAirPort";
		((ControlBase)this.lblAirPort).WrapText = false;
		resources.ApplyResources(this.cboSignState, "cboSignState");
		this.cboSignState.AutoCompleteMode = (AutoCompleteMode)4;
		val26.DataValue = true;
		resources.ApplyResources(val26, "valueListItem1");
		((SubObjectBase)val26).ForceApplyResources = "";
		val27.DataValue = false;
		resources.ApplyResources(val27, "valueListItem4");
		((SubObjectBase)val27).ForceApplyResources = "";
		this.cboSignState.Items.AddRange((ValueListItem[])(object)new ValueListItem[2] { val26, val27 });
		((System.Windows.Forms.Control)(object)this.cboSignState).Name = "cboSignState";
		resources.ApplyResources(this.cboVessels, "cboVessels");
		this.cboVessels.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboVessels).Name = "cboVessels";
		resources.ApplyResources(this.chkIsUrgent, "chkIsUrgent");
		((AppearanceBase)val28).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val28, "appearance59");
		((UltraToggleEditorBase)this.chkIsUrgent).Appearance = (AppearanceBase)(object)val28;
		((UltraToggleEditorBase)this.chkIsUrgent).CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		((System.Windows.Forms.Control)(object)this.chkIsUrgent).Name = "chkIsUrgent";
		((UltraToggleEditorBase)this.chkIsUrgent).CheckedChanged += new System.EventHandler(chkIsUrgent_CheckedChanged);
		resources.ApplyResources(this.txtVisaSerial, "txtVisaSerial");
		((System.Windows.Forms.Control)(object)this.txtVisaSerial).Name = "txtVisaSerial";
		((EditorButtonControlBase)this.txtVisaSerial).ReadOnly = true;
		resources.ApplyResources(this.lblVisaSerialNo, "lblVisaSerialNo");
		((AppearanceBase)val29).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val29).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val29, "appearance60");
		((ControlBase)this.lblVisaSerialNo).Appearance = (AppearanceBase)(object)val29;
		this.lblVisaSerialNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaSerialNo).Name = "lblVisaSerialNo";
		((ControlBase)this.lblVisaSerialNo).WrapText = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val30).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val30).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val30, "appearance61");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val30;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val31).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val31).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val31, "appearance62");
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val31;
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.txtNationality, "txtNationality");
		((System.Windows.Forms.Control)(object)this.txtNationality).Name = "txtNationality";
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		((AppearanceBase)val32).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val32).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val32, "appearance63");
		((ControlBase)this.ultraLabel4).Appearance = (AppearanceBase)(object)val32;
		this.ultraLabel4.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		((ControlBase)this.ultraLabel4).WrapText = false;
		resources.ApplyResources(this.dtpActualExitDate, "dtpActualExitDate");
		((UltraWinEditorMaskedControlBase)this.dtpActualExitDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpActualExitDate).Name = "dtpActualExitDate";
		((System.Windows.Forms.Control)(object)this.dtpActualExitDate).Enter += new System.EventHandler(dtpDateTime_Enter);
		resources.ApplyResources(this.txtVoyage, "txtVoyage");
		((System.Windows.Forms.Control)(object)this.txtVoyage).Name = "txtVoyage";
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		((AppearanceBase)val33).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val33).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val33, "appearance64");
		((ControlBase)this.ultraLabel5).Appearance = (AppearanceBase)(object)val33;
		this.ultraLabel5.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.chkIsCompleted, "chkIsCompleted");
		((AppearanceBase)val34).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val34, "appearance65");
		((UltraToggleEditorBase)this.chkIsCompleted).Appearance = (AppearanceBase)(object)val34;
		((UltraToggleEditorBase)this.chkIsCompleted).CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		((System.Windows.Forms.Control)(object)this.chkIsCompleted).Name = "chkIsCompleted";
		resources.ApplyResources(this.chkIsRejected, "chkIsRejected");
		((AppearanceBase)val35).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val35, "appearance66");
		((UltraToggleEditorBase)this.chkIsRejected).Appearance = (AppearanceBase)(object)val35;
		((UltraToggleEditorBase)this.chkIsRejected).CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		((System.Windows.Forms.Control)(object)this.chkIsRejected).Name = "chkIsRejected";
		((UltraToggleEditorBase)this.chkIsRejected).CheckedChanged += new System.EventHandler(chkIsRejected_CheckedChanged);
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		((AppearanceBase)val36).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val36).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val36, "appearance67");
		((ControlBase)this.ultraLabel6).Appearance = (AppearanceBase)(object)val36;
		this.ultraLabel6.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		((ControlBase)this.ultraLabel6).WrapText = false;
		resources.ApplyResources(this.ultraLabel7, "ultraLabel7");
		((AppearanceBase)val37).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val37).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val37, "appearance68");
		((ControlBase)this.ultraLabel7).Appearance = (AppearanceBase)(object)val37;
		this.ultraLabel7.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel7).Name = "ultraLabel7";
		((ControlBase)this.ultraLabel7).WrapText = false;
		resources.ApplyResources(this.txtCancellationReason, "txtCancellationReason");
		((System.Windows.Forms.Control)(object)this.txtCancellationReason).Name = "txtCancellationReason";
		resources.ApplyResources(this.chkIsCancelled, "chkIsCancelled");
		((AppearanceBase)val38).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val38, "appearance69");
		((UltraToggleEditorBase)this.chkIsCancelled).Appearance = (AppearanceBase)(object)val38;
		((UltraToggleEditorBase)this.chkIsCancelled).CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		((System.Windows.Forms.Control)(object)this.chkIsCancelled).Name = "chkIsCancelled";
		((UltraToggleEditorBase)this.chkIsCancelled).CheckedChanged += new System.EventHandler(chkIsCancelled_CheckedChanged);
		resources.ApplyResources(this.dtpCancellationDate, "dtpCancellationDate");
		((UltraWinEditorMaskedControlBase)this.dtpCancellationDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpCancellationDate).Name = "dtpCancellationDate";
		((System.Windows.Forms.Control)(object)this.dtpCancellationDate).Enter += new System.EventHandler(dtpDateTime_Enter);
		resources.ApplyResources(this.ultraLabel8, "ultraLabel8");
		((AppearanceBase)val39).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val39).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val39, "appearance70");
		((ControlBase)this.ultraLabel8).Appearance = (AppearanceBase)(object)val39;
		this.ultraLabel8.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel8).Name = "ultraLabel8";
		((ControlBase)this.ultraLabel8).WrapText = false;
		resources.ApplyResources(this.cboRejectionReason, "cboRejectionReason");
		this.cboRejectionReason.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboRejectionReason).Name = "cboRejectionReason";
		resources.ApplyResources(this.lblBankName, "lblBankName");
		((AppearanceBase)val40).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val40).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val40, "appearance71");
		((ControlBase)this.lblBankName).Appearance = (AppearanceBase)(object)val40;
		this.lblBankName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBankName).Name = "lblBankName";
		((ControlBase)this.lblBankName).WrapText = false;
		resources.ApplyResources(this.cboVisaBankSettings, "cboVisaBankSettings");
		this.cboVisaBankSettings.AutoCompleteMode = (AutoCompleteMode)4;
		val41.DataValue = "1";
		resources.ApplyResources(val41, "valueListItem5");
		((SubObjectBase)val41).ForceApplyResources = "";
		val42.DataValue = "2";
		resources.ApplyResources(val42, "valueListItem2");
		((SubObjectBase)val42).ForceApplyResources = "";
		val43.DataValue = "3";
		resources.ApplyResources(val43, "valueListItem3");
		((SubObjectBase)val43).ForceApplyResources = "";
		this.cboVisaBankSettings.Items.AddRange((ValueListItem[])(object)new ValueListItem[3] { val41, val42, val43 });
		((System.Windows.Forms.Control)(object)this.cboVisaBankSettings).Name = "cboVisaBankSettings";
		((TextEditorControlBase)this.cboVisaBankSettings).ValueChanged += new System.EventHandler(cboVisaBankSettings_ValueChanged);
		resources.ApplyResources(this.btnUpdateIDNo, "btnUpdateIDNo");
		((AppearanceBase)val44).Image = ERP.Properties.Resources.Update;
		resources.ApplyResources(val44, "appearance72");
		((ControlBase)this.btnUpdateIDNo).Appearance = (AppearanceBase)(object)val44;
		((System.Windows.Forms.Control)(object)this.btnUpdateIDNo).Name = "btnUpdateIDNo";
		((System.Windows.Forms.Control)(object)this.btnUpdateIDNo).Click += new System.EventHandler(btnUpdateIDNo_Click);
		resources.ApplyResources(this.lblHistory, "lblHistory");
		((System.Windows.Forms.Control)(object)this.lblHistory).Name = "lblHistory";
		((System.Windows.Forms.Control)(object)this.lblHistory).Click += new System.EventHandler(lblHistory_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblHistory);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnUpdateIDNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboVisaBankSettings);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboRejectionReason);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPrintOutCost);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsCancelled);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsRejected);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsCompleted);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsPrintOut);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCancellationReason);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVisaCost);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel8);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaCost);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblIDNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtIDNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNationality);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpActualExitDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpEntryExpireDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpActualFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpValidToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboAirPort);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAirPort);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblValidToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEntryExpire);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsUrgent);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpCancellationDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpIDIssueDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblActualFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpVisaIssueDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaSerialNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblIDIssueDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaState);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVisaNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCompanies);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboVisaState);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel7);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPassportNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVessels);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpVisaDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVisaSerial);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNationality);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVoyage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPassportNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPassengerType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBankName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPassengerType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNameAr);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSignState);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboVessels);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSignState);
		base.Name = "frmUpdateSeaManVisa";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSignState, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboVessels, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSignState, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPassengerType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBankName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPassengerType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPassportNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVoyage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNationality, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVisaSerial, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpVisaDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVessels, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPassportNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel5, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel6, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel7, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboVisaState, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCompanies, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVisaNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaState, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblIDIssueDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaSerialNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpVisaIssueDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblActualFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpIDIssueDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpCancellationDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsUrgent, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEntryExpire, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblValidToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel4, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAirPort, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboAirPort, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpValidToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpActualFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpEntryExpireDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpActualExitDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNationality, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtIDNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblIDNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaCost, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel8, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVisaCost, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCancellationReason, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsPrintOut, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsCompleted, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsRejected, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsCancelled, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPrintOutCost, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboRejectionReason, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboVisaBankSettings, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnUpdateIDNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblHistory, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassportNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtIDNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpIDIssueDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).EndInit();
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
		((System.ComponentModel.ISupportInitialize)this.cboAirPort).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSignState).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVessels).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsUrgent).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaSerial).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNationality).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpActualExitDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVoyage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsCompleted).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsRejected).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCancellationReason).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsCancelled).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCancellationDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboRejectionReason).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaBankSettings).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
