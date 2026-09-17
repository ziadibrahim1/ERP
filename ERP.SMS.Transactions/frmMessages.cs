using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.CRM;
using BusinessLayer.POS;
using BusinessLayer.Privilege;
using BusinessLayer.SMS;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SMS.Transactions;

public class frmMessages : frmHeaderDetails
{
	private DataTable dtSMSTemplates;

	private DataTable dtPeriodicEvents;

	private DataTable dtUsers;

	private DataTable dtLanguages;

	private DataTable dtStatus;

	private DataTable dtSubAccounts;

	private DataTable dtPosClients;

	private DataTable dtCustomers;

	private DataTable dtMessageType;

	private ValueList vlSMSTemplates = new ValueList();

	private ValueList vlPeriodicEvents = new ValueList();

	private ValueList vlAddedByUsers = new ValueList();

	private ValueList vlSentByUsers = new ValueList();

	private ValueList vlLanguages = new ValueList();

	private ValueList vlStatus = new ValueList();

	private ValueList vlSubAccounts = new ValueList();

	private ValueList vlCustomers = new ValueList();

	private ValueList vlPosClients = new ValueList();

	private ValueList vlMessageType = new ValueList();

	private bool CRMModuleInstalled = true;

	private bool POSModuleInstalled = true;

	private IContainer components = null;

	private UltraTextEditor txtNots;

	private UltraLabel lblNotes;

	private UltraTextEditor txtMessageText;

	private UltraLabel lblMessageText;

	private UltraDateTimeEditor dtpSentDate;

	private UltraDateTimeEditor dtpExpectedSendDate;

	private UltraDateTimeEditor dtpAddedDate;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboMessageType;

	private UltraComboEditor cboSentByUsers;

	private UltraComboEditor cboLanguage;

	private UltraComboEditor cboAddedByUsers;

	private UltraComboEditor cboTemplate;

	private UltraLabel lblSentByUser;

	private UltraLabel lblLanguage;

	private UltraComboEditor cboPeriodicEvent;

	private UltraLabel lblAddedByUser;

	private UltraLabel lblMessageType;

	private UltraLabel lblSentDate;

	private UltraLabel lblExpectedSendDate;

	private UltraLabel lblTemplate;

	private UltraLabel lblAddedDate;

	private UltraLabel lblMessageDate;

	private UltraLabel lblEvent;

	public frmMessages()
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
		InitializeComponent();
		TableName = "SMS_MessagesLog";
		IDCol = "MessageID";
		NoCol = "MessageNo";
		DateCol = "MessageDate";
	}

	public frmMessages(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		dtSMSTemplates = Templates.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTemplate, dtSMSTemplates, "TemplateID", "TemplateName");
		vlSMSTemplates.ValueListItems.Clear();
		for (int i = 0; i < dtSMSTemplates.Rows.Count; i++)
		{
			vlSMSTemplates.ValueListItems.Add((object)dtSMSTemplates.Rows[i]["TemplateID"].ToString(), dtSMSTemplates.Rows[i]["TemplateName"].ToString());
		}
		dtPeriodicEvents = PeriodicEvents.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPeriodicEvent, dtPeriodicEvents, "PeriodicEventID", "PeriodicEventName");
		vlPeriodicEvents.ValueListItems.Clear();
		for (int j = 0; j < dtPeriodicEvents.Rows.Count; j++)
		{
			vlPeriodicEvents.ValueListItems.Add((object)dtPeriodicEvents.Rows[j]["PeriodicEventID"].ToString(), dtPeriodicEvents.Rows[j]["PeriodicEventName"].ToString());
		}
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboAddedByUsers, dtUsers, "User_ID", "UserName");
		GlobalFunctions.FillCombo(cboSentByUsers, dtUsers, "User_ID", "UserName");
		vlAddedByUsers.ValueListItems.Clear();
		vlSentByUsers.ValueListItems.Clear();
		for (int k = 0; k < dtUsers.Rows.Count; k++)
		{
			vlAddedByUsers.ValueListItems.Add((object)dtUsers.Rows[k]["User_ID"].ToString(), dtUsers.Rows[k]["UserName"].ToString());
			vlSentByUsers.ValueListItems.Add((object)dtUsers.Rows[k]["User_ID"].ToString(), dtUsers.Rows[k]["UserName"].ToString());
		}
		dtLanguages = SMSLanguages.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboLanguage, dtLanguages, "SMSLanguageID", "SMSLanguageName");
		vlLanguages.ValueListItems.Clear();
		for (int l = 0; l < dtLanguages.Rows.Count; l++)
		{
			vlLanguages.ValueListItems.Add((object)dtLanguages.Rows[l]["SMSLanguageID"].ToString(), dtLanguages.Rows[l]["SMSLanguageName"].ToString());
		}
		dtStatus = MessagesStatus.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlStatus.ValueListItems.Clear();
		for (int m = 0; m < dtStatus.Rows.Count; m++)
		{
			vlStatus.ValueListItems.Add((object)dtStatus.Rows[m]["MessageStatusID"].ToString(), dtStatus.Rows[m]["MessageStatusName"].ToString());
		}
		dtSubAccounts = SubAccounts.FillComboForSMS(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlSubAccounts.ValueListItems.Clear();
		for (int n = 0; n < dtSubAccounts.Rows.Count; n++)
		{
			vlSubAccounts.ValueListItems.Add((object)dtSubAccounts.Rows[n]["SubAccountID"].ToString(), dtSubAccounts.Rows[n]["SubAccountName"].ToString());
		}
		CRMModuleInstalled = Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='CRM'")[0]["Installed"]);
		if (CRMModuleInstalled)
		{
			dtCustomers = Customers.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlCustomers.ValueListItems.Clear();
			for (int num = 0; num < dtCustomers.Rows.Count; num++)
			{
				vlCustomers.ValueListItems.Add((object)dtCustomers.Rows[num]["CustomerID"].ToString(), dtCustomers.Rows[num]["CustomerName"].ToString());
			}
		}
		POSModuleInstalled = Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='POS'")[0]["Installed"]) || Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='Photos'")[0]["Installed"]) || Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='LensesLab'")[0]["Installed"]);
		if (POSModuleInstalled)
		{
			dtPosClients = Clients.FillCombo("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlPosClients.ValueListItems.Clear();
			for (int num2 = 0; num2 < dtPosClients.Rows.Count; num2++)
			{
				vlPosClients.ValueListItems.Add((object)dtPosClients.Rows[num2]["ClientID"].ToString(), dtPosClients.Rows[num2]["ClientName"].ToString());
			}
		}
		dtMessageType = MessagesTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboMessageType, dtMessageType, "MessageTypeID", "MessageTypeName");
		vlMessageType.ValueListItems.Clear();
		for (int num3 = 0; num3 < dtMessageType.Rows.Count; num3++)
		{
			vlMessageType.ValueListItems.Add((object)dtMessageType.Rows[num3]["MessageTypeID"].ToString(), dtMessageType.Rows[num3]["MessageTypeName"].ToString());
		}
		dtDetails = MessagesLogDetails.SelectByMessageID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MessageDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubaccountID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubaccountID"].Header).Caption = (GlobalVariables.IsArabic ? "اسم العميل" : "Client Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubaccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubaccountID"].ValueList = (IValueList)(object)vlSubAccounts;
		if (CRMModuleInstalled)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerID"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerID"].Header).Caption = (GlobalVariables.IsArabic ? "CRM عميل" : " CRM Client");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerID"].ValueList = (IValueList)(object)vlCustomers;
		}
		if (POSModuleInstalled)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientID"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientID"].Header).Caption = (GlobalVariables.IsArabic ? "عميل POS" : " POS Client");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientID"].ValueList = (IValueList)(object)vlPosClients;
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PhoneNumber"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PhoneNumber"].Header).Caption = (GlobalVariables.IsArabic ? "رقم المحمول" : "Phone Number");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PhoneNumber"].Width = (int)((double)((Control)(object)ULGData).Width * 0.125);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الملاحظات" : "Notes");
		if (POSModuleInstalled && CRMModuleInstalled)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		}
		else if (POSModuleInstalled || CRMModuleInstalled)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.35);
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.55);
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StatusID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StatusID"].Header).Caption = (GlobalVariables.IsArabic ? "الحالة" : "Status");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StatusID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.125);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StatusID"].ValueList = (IValueList)(object)vlStatus;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = MessagesLog.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
			((TextEditorControlBase)cboPeriodicEvent).ValueChanged -= cboPeriodicEvent_ValueChanged;
			((TextEditorControlBase)cboTemplate).ValueChanged -= cboTemplate_ValueChanged;
			((Control)(object)txtCode).Text = drMaster["MessageNo"].ToString();
			((Control)(object)txtMessageText).Text = drMaster["MessageText"].ToString();
			((Control)(object)txtNots).Text = drMaster["Notes"].ToString();
			((TextEditorControlBase)cboAddedByUsers).Value = drMaster["AddedByUserID"];
			((TextEditorControlBase)cboLanguage).Value = drMaster["Language"];
			((TextEditorControlBase)cboMessageType).Value = drMaster["MessageTypeID"];
			((TextEditorControlBase)cboPeriodicEvent).Value = drMaster["PeriodicEventID"];
			((TextEditorControlBase)cboSentByUsers).Value = drMaster["SentByUserID"];
			((TextEditorControlBase)cboTemplate).Value = drMaster["TemplateID"];
			dtpAddedDate.Value = drMaster["AddedDate"];
			dtpDate.Value = drMaster["MessageDate"];
			dtpSentDate.Value = drMaster["SentDate"];
			dtpExpectedSendDate.Value = drMaster["ExpectedSendDate"];
			((TextEditorControlBase)cboTemplate).ValueChanged += cboTemplate_ValueChanged;
			((TextEditorControlBase)cboPeriodicEvent).ValueChanged += cboPeriodicEvent_ValueChanged;
			dtpAddedDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
			dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
			dtpExpectedSendDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
			dtDetails = MessagesLogDetails.SelectByMessageID(drMaster["MessageID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
		}
		else
		{
			ClearControls();
		}
		((TextEditorControlBase)txtCode).Focus();
	}

	private void cboPeriodicEvent_ValueChanged(object sender, EventArgs e)
	{
		if (cboPeriodicEvent.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboTemplate).Value = dtPeriodicEvents.Select("PeriodicEventID = " + ((TextEditorControlBase)cboPeriodicEvent).Value.ToString())[0]["TemplateID"];
			DateTime dateTime = default(DateTime);
			dateTime = Convert.ToDateTime(dtPeriodicEvents.Select("PeriodicEventID = " + ((TextEditorControlBase)cboPeriodicEvent).Value.ToString())[0]["EventDate"].ToString()).Date.Add(Convert.ToDateTime(dtPeriodicEvents.Select("PeriodicEventID = " + ((TextEditorControlBase)cboPeriodicEvent).Value.ToString())[0]["EventTime"].ToString()).TimeOfDay);
			dtpExpectedSendDate.DateTime = dateTime;
		}
	}

	public override void btnCopyToClick()
	{
		if (!CanAdd)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		Adding = true;
		drMaster = null;
		for (int i = 0; i < dtDetails.Rows.Count; i++)
		{
			dtDetails.Rows[i]["StatusID"] = DBNull.Value;
		}
		SetControls(NavMode: false);
	}

	private void cboTemplate_ValueChanged(object sender, EventArgs e)
	{
		if (cboTemplate.SelectedIndex > -1)
		{
			((TextEditorControlBase)txtMessageText).Value = dtSMSTemplates.Select("TemplateID = " + ((TextEditorControlBase)cboTemplate).Value.ToString())[0]["TemplateText"];
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtMessageText).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNots).ReadOnly = NavMode;
		((EditorButtonControlBase)cboAddedByUsers).ReadOnly = true;
		((EditorButtonControlBase)cboLanguage).ReadOnly = NavMode;
		((EditorButtonControlBase)cboMessageType).ReadOnly = true;
		((EditorButtonControlBase)cboPeriodicEvent).ReadOnly = NavMode;
		((EditorButtonControlBase)cboTemplate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSentByUsers).ReadOnly = true;
		((EditorButtonControlBase)cboTemplate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpAddedDate).ReadOnly = true;
		((EditorButtonControlBase)dtpExpectedSendDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpSentDate).ReadOnly = true;
		((TextEditorControlBase)txtMessageText).Focus();
	}

	public override void ClearControls()
	{
		base.ClearControls();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? MessagesLog.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)cboPeriodicEvent).ValueChanged -= cboPeriodicEvent_ValueChanged;
		((TextEditorControlBase)cboTemplate).ValueChanged -= cboTemplate_ValueChanged;
		((TextEditorControlBase)cboAddedByUsers).Value = GlobalVariables.UserID;
		((TextEditorControlBase)cboLanguage).Value = ((!GlobalVariables.IsArabic) ? 1 : 2);
		cboMessageType.SelectedIndex = 0;
		cboPeriodicEvent.SelectedIndex = -1;
		cboSentByUsers.SelectedIndex = -1;
		cboTemplate.SelectedIndex = -1;
		((TextEditorControlBase)txtMessageText).Clear();
		((TextEditorControlBase)txtNots).Clear();
		dtpAddedDate.Value = GlobalFunctions.GetServerDateTimeNow();
		dtpExpectedSendDate.Value = GlobalFunctions.GetServerDateTimeNow();
		dtpSentDate.Value = null;
		dtpDate.Value = GlobalFunctions.GetServerDateTimeNow();
		dtpAddedDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtpExpectedSendDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((TextEditorControlBase)cboPeriodicEvent).ValueChanged += cboPeriodicEvent_ValueChanged;
		((TextEditorControlBase)cboTemplate).ValueChanged += cboTemplate_ValueChanged;
	}

	public override bool ValidateData()
	{
		if (dtpDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ الأذن" : "Please Enter The Voucher Date");
			((Control)(object)dtpDate).Focus();
			dtpDate.DropDown();
			return false;
		}
		if (!FiscalYear.ChkForConfirmedFiscalYear(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), IsFromServer: false))
		{
			GlobalVariables.InformationMB.Show("السنة المالية غير معتمدة", "The Fiscal Year is not confirmed..");
			return false;
		}
		if (FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false))
		{
			GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ يقع في فتره ماليه مغلقة", "The Date you choosed\n\r exists in closed fisical period");
			return false;
		}
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم  الأذن" : "Please Enter The Voucher Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtMessageText).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال  نص الرسالة", "Please Enter Message Text");
			((TextEditorControlBase)txtMessageText).Focus();
			return false;
		}
		if (dtpDate.Value == null)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال تاريخ الرسالة", "Please Enter Message Date");
			((Control)(object)dtpDate).Focus();
			return false;
		}
		if (dtpExpectedSendDate.Value == null)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال تاريخ الارسال المتوقع", "Please Enter Expected Send Date");
			((Control)(object)dtpExpectedSendDate).Focus();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("SMS_MessagesLog", "MessageNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["MessageNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = MessagesLog.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Voucher Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByBranchID;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذه الرسالة", "Please insert details for this Message");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["PhoneNumber"].Value == DBNull.Value || ((UltraGridBase)ULGData).Rows[i].Cells["PhoneNumber"].Value == null || ((UltraGridBase)ULGData).Rows[i].Cells["PhoneNumber"].Value.ToString().Trim().Equals(""))
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال رقم المحمول   ", "Please Enter Mobile Number");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["PhoneNumber"];
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["PhoneNumber"].Value.ToString().Trim().Length != 12)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال رقم محمول صحيح   ", "Please Enter A Valid Mobile Number");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["PhoneNumber"];
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGData).Rows[i].Cells["PhoneNumber"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["PhoneNumber"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show("لا يمكن تكرار رقم المحمول ", "Cannot Duplicate The Same Mobile Numbrt");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["PhoneNumber"];
					return false;
				}
			}
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = MessagesLog.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "1", (cboPeriodicEvent.SelectedIndex > -1) ? ((TextEditorControlBase)cboPeriodicEvent).Value.ToString() : "Null", (cboTemplate.SelectedIndex > -1) ? ((TextEditorControlBase)cboTemplate).Value.ToString() : "Null", (cboLanguage.SelectedIndex > -1) ? ((TextEditorControlBase)cboLanguage).Value.ToString() : "Null", ((Control)(object)txtMessageText).Text, dtpExpectedSendDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtNots).Text, GlobalVariables.UserID, GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate), "Null", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["MessageDetailID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["MessageID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			MessagesLogDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
			RowID = num.ToString();
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
			int num = MessagesLog.Insert_Update(drMaster["MessageID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboMessageType.SelectedIndex > -1) ? ((TextEditorControlBase)cboMessageType).Value.ToString() : "Null", (cboPeriodicEvent.SelectedIndex > -1) ? ((TextEditorControlBase)cboPeriodicEvent).Value.ToString() : "Null", (cboTemplate.SelectedIndex > -1) ? ((TextEditorControlBase)cboTemplate).Value.ToString() : "Null", (cboLanguage.SelectedIndex > -1) ? ((TextEditorControlBase)cboLanguage).Value.ToString() : "Null", ((Control)(object)txtMessageText).Text, dtpExpectedSendDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtNots).Text, drMaster["AddedByUserID"].ToString(), drMaster["AddedDate"].ToString(), (drMaster["SentByUserID"] == DBNull.Value) ? "Null" : drMaster["SentByUserID"].ToString(), (drMaster["SentDate"] == DBNull.Value) ? "Null" : drMaster["SentDate"].ToString(), bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["MessageID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["MessageDetailID"].Value.ToString() + ",";
			}
			Main.DeleteForUpdate("SMS_MessagesLogDetails", "MessageID", drMaster["MessageID"].ToString(), "MessageDetailID", text);
			MessagesLogDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch (Exception)
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
			MessagesLog.DeleteVirtual(drMaster["MessageID"].ToString(), GlobalVariables.UserID);
			MessagesLogDetails.DeleteVirtualByMessageID(drMaster["MessageID"].ToString(), GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.MessagesLogReport("-1", IsFromServer: false);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["MessageID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		for (int i = 0; i < e.Rows.Length; i++)
		{
			if (e.Rows[i].Cells["StatusID"].Value != null && e.Rows[i].Cells["StatusID"].Value.ToString() == "1")
			{
				GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الرسالة لانه تم ارسالها", "Cannot Delete This Message Because It Has Been Sent");
				((CancelEventArgs)(object)e).Cancel = true;
				return;
			}
		}
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
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
		if (!CanModifyOtherBranch)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفرع آخر", "Cannot Update This Transaction Because It Related to Another Branch ");
			return;
		}
		if (ClosedPeriod)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Delete This Transaction Because It Related to ClosedPeriod ");
			return;
		}
		if (dtDetails.Select("StatusID = 1").Length != 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الرسالة لانه تم ارسالها", "Cannot Delete This Message Because It Has Been Sent");
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

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		((UltraGridBase)ULGData).UpdateData();
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "SubAccountID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			e.Cell.Row.Cells["CustomerID"].Value = DBNull.Value;
			e.Cell.Row.Cells["ClientID"].Value = DBNull.Value;
			e.Cell.Row.Cells["PhoneNumber"].Value = dtSubAccounts.Select("SubAccountID = " + e.Cell.Value.ToString())[0]["MobileNumber"];
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "CustomerID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			e.Cell.Row.Cells["SubAccountID"].Value = DBNull.Value;
			e.Cell.Row.Cells["ClientID"].Value = DBNull.Value;
			e.Cell.Row.Cells["PhoneNumber"].Value = dtCustomers.Select("CustomerID = " + e.Cell.Value.ToString())[0]["MobileNumber"];
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "ClientID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			e.Cell.Row.Cells["SubAccountID"].Value = DBNull.Value;
			e.Cell.Row.Cells["CustomerID"].Value = DBNull.Value;
			e.Cell.Row.Cells["PhoneNumber"].Value = dtPosClients.Select("ClientID = " + e.Cell.Value.ToString())[0]["MobileNumber"];
		}
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (ULGData.ActiveCell.Row.Cells["StatusID"].Value != null && ULGData.ActiveCell.Row.Cells["StatusID"].Value.ToString() == "1")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "StatusID")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "PhoneNumber")
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void dtpMessageDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = MessagesLog.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Expected O, but got Unknown
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Expected O, but got Unknown
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Expected O, but got Unknown
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Expected O, but got Unknown
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected O, but got Unknown
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected O, but got Unknown
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Expected O, but got Unknown
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Expected O, but got Unknown
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Expected O, but got Unknown
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Expected O, but got Unknown
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Expected O, but got Unknown
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Expected O, but got Unknown
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Expected O, but got Unknown
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Expected O, but got Unknown
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Expected O, but got Unknown
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Expected O, but got Unknown
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Expected O, but got Unknown
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Expected O, but got Unknown
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Expected O, but got Unknown
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SMS.Transactions.frmMessages));
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
		this.txtNots = new UltraTextEditor();
		this.lblNotes = new UltraLabel();
		this.txtMessageText = new UltraTextEditor();
		this.lblMessageText = new UltraLabel();
		this.dtpSentDate = new UltraDateTimeEditor();
		this.dtpExpectedSendDate = new UltraDateTimeEditor();
		this.dtpAddedDate = new UltraDateTimeEditor();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboMessageType = new UltraComboEditor();
		this.cboSentByUsers = new UltraComboEditor();
		this.cboLanguage = new UltraComboEditor();
		this.cboAddedByUsers = new UltraComboEditor();
		this.cboTemplate = new UltraComboEditor();
		this.lblSentByUser = new UltraLabel();
		this.lblLanguage = new UltraLabel();
		this.cboPeriodicEvent = new UltraComboEditor();
		this.lblAddedByUser = new UltraLabel();
		this.lblMessageType = new UltraLabel();
		this.lblSentDate = new UltraLabel();
		this.lblExpectedSendDate = new UltraLabel();
		this.lblTemplate = new UltraLabel();
		this.lblAddedDate = new UltraLabel();
		this.lblMessageDate = new UltraLabel();
		this.lblEvent = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNots).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMessageText).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpSentDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpExpectedSendDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpAddedDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboMessageType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSentByUsers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLanguage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAddedByUsers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTemplate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPeriodicEvent).BeginInit();
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
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
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
		resources.ApplyResources(this.txtNots, "txtNots");
		((TextEditorControlBase)this.txtNots).MaxLength = 70;
		((System.Windows.Forms.Control)(object)this.txtNots).Name = "txtNots";
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtMessageText, "txtMessageText");
		((TextEditorControlBase)this.txtMessageText).MaxLength = 70;
		((System.Windows.Forms.Control)(object)this.txtMessageText).Name = "txtMessageText";
		resources.ApplyResources(this.lblMessageText, "lblMessageText");
		this.lblMessageText.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMessageText).Name = "lblMessageText";
		((ControlBase)this.lblMessageText).WrapText = false;
		resources.ApplyResources(this.dtpSentDate, "dtpSentDate");
		((UltraWinEditorMaskedControlBase)this.dtpSentDate).AlwaysInEditMode = true;
		this.dtpSentDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpSentDate).Name = "dtpSentDate";
		resources.ApplyResources(this.dtpExpectedSendDate, "dtpExpectedSendDate");
		((UltraWinEditorMaskedControlBase)this.dtpExpectedSendDate).AlwaysInEditMode = true;
		this.dtpExpectedSendDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpExpectedSendDate).Name = "dtpExpectedSendDate";
		resources.ApplyResources(this.dtpAddedDate, "dtpAddedDate");
		((UltraWinEditorMaskedControlBase)this.dtpAddedDate).AlwaysInEditMode = true;
		this.dtpAddedDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpAddedDate).Name = "dtpAddedDate";
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		resources.ApplyResources(this.cboMessageType, "cboMessageType");
		((System.Windows.Forms.Control)(object)this.cboMessageType).Name = "cboMessageType";
		((EditorButtonControlBase)this.cboMessageType).ReadOnly = true;
		resources.ApplyResources(this.cboSentByUsers, "cboSentByUsers");
		((System.Windows.Forms.Control)(object)this.cboSentByUsers).Name = "cboSentByUsers";
		((EditorButtonControlBase)this.cboSentByUsers).ReadOnly = true;
		resources.ApplyResources(this.cboLanguage, "cboLanguage");
		((System.Windows.Forms.Control)(object)this.cboLanguage).Name = "cboLanguage";
		((EditorButtonControlBase)this.cboLanguage).ReadOnly = true;
		resources.ApplyResources(this.cboAddedByUsers, "cboAddedByUsers");
		((System.Windows.Forms.Control)(object)this.cboAddedByUsers).Name = "cboAddedByUsers";
		((EditorButtonControlBase)this.cboAddedByUsers).ReadOnly = true;
		resources.ApplyResources(this.cboTemplate, "cboTemplate");
		((System.Windows.Forms.Control)(object)this.cboTemplate).Name = "cboTemplate";
		((EditorButtonControlBase)this.cboTemplate).ReadOnly = true;
		resources.ApplyResources(this.lblSentByUser, "lblSentByUser");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance19");
		((ControlBase)this.lblSentByUser).Appearance = (AppearanceBase)(object)val9;
		this.lblSentByUser.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSentByUser).Name = "lblSentByUser";
		((ControlBase)this.lblSentByUser).WrapText = false;
		resources.ApplyResources(this.lblLanguage, "lblLanguage");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance20");
		((ControlBase)this.lblLanguage).Appearance = (AppearanceBase)(object)val10;
		this.lblLanguage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLanguage).Name = "lblLanguage";
		((ControlBase)this.lblLanguage).WrapText = false;
		resources.ApplyResources(this.cboPeriodicEvent, "cboPeriodicEvent");
		((System.Windows.Forms.Control)(object)this.cboPeriodicEvent).Name = "cboPeriodicEvent";
		((EditorButtonControlBase)this.cboPeriodicEvent).ReadOnly = true;
		resources.ApplyResources(this.lblAddedByUser, "lblAddedByUser");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance21");
		((ControlBase)this.lblAddedByUser).Appearance = (AppearanceBase)(object)val11;
		this.lblAddedByUser.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAddedByUser).Name = "lblAddedByUser";
		((ControlBase)this.lblAddedByUser).WrapText = false;
		resources.ApplyResources(this.lblMessageType, "lblMessageType");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance22");
		((ControlBase)this.lblMessageType).Appearance = (AppearanceBase)(object)val12;
		this.lblMessageType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMessageType).Name = "lblMessageType";
		((ControlBase)this.lblMessageType).WrapText = false;
		resources.ApplyResources(this.lblSentDate, "lblSentDate");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance23");
		((ControlBase)this.lblSentDate).Appearance = (AppearanceBase)(object)val13;
		this.lblSentDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSentDate).Name = "lblSentDate";
		((ControlBase)this.lblSentDate).WrapText = false;
		resources.ApplyResources(this.lblExpectedSendDate, "lblExpectedSendDate");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val14, "appearance24");
		((ControlBase)this.lblExpectedSendDate).Appearance = (AppearanceBase)(object)val14;
		this.lblExpectedSendDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExpectedSendDate).Name = "lblExpectedSendDate";
		((ControlBase)this.lblExpectedSendDate).WrapText = false;
		resources.ApplyResources(this.lblTemplate, "lblTemplate");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val15, "appearance25");
		((ControlBase)this.lblTemplate).Appearance = (AppearanceBase)(object)val15;
		this.lblTemplate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTemplate).Name = "lblTemplate";
		((ControlBase)this.lblTemplate).WrapText = false;
		resources.ApplyResources(this.lblAddedDate, "lblAddedDate");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val16, "appearance26");
		((ControlBase)this.lblAddedDate).Appearance = (AppearanceBase)(object)val16;
		this.lblAddedDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAddedDate).Name = "lblAddedDate";
		((ControlBase)this.lblAddedDate).WrapText = false;
		resources.ApplyResources(this.lblMessageDate, "lblMessageDate");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val17, "appearance27");
		((ControlBase)this.lblMessageDate).Appearance = (AppearanceBase)(object)val17;
		this.lblMessageDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMessageDate).Name = "lblMessageDate";
		((ControlBase)this.lblMessageDate).WrapText = false;
		resources.ApplyResources(this.lblEvent, "lblEvent");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val18).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val18, "appearance28");
		((ControlBase)this.lblEvent).Appearance = (AppearanceBase)(object)val18;
		this.lblEvent.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEvent).Name = "lblEvent";
		((ControlBase)this.lblEvent).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNots);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtMessageText);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMessageText);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpSentDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpExpectedSendDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpAddedDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboMessageType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSentByUsers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboLanguage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboAddedByUsers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTemplate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSentByUser);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLanguage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPeriodicEvent);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAddedByUser);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMessageType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSentDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExpectedSendDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTemplate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAddedDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMessageDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEvent);
		base.Name = "frmMessages";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEvent, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMessageDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAddedDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTemplate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExpectedSendDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSentDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMessageType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAddedByUser, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPeriodicEvent, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLanguage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSentByUser, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTemplate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboAddedByUsers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboLanguage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSentByUsers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboMessageType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpAddedDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpExpectedSendDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpSentDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMessageText, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtMessageText, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNots, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNots).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMessageText).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpSentDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpExpectedSendDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpAddedDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboMessageType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSentByUsers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLanguage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAddedByUsers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPeriodicEvent).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
