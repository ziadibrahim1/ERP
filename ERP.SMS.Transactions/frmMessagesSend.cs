using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.SMS;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using ERP.Ticketing;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ERP.SMS.Transactions;

public class frmMessagesSend : frmBase
{
	private DataTable dtDetails;

	private DataTable dtStatus;

	private DataTable dtSMSSettings;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	private UltraDateTimeEditor dtpFromDate;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpToDate;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel6;

	public UltraButton btnOpenTicket;

	public UltraButton btnCopy;

	public UltraButton btnSearch;

	public UltraButton btnSendSMS;

	public UltraButton btnPrintInvoices;

	public UltraLabel lblNotSent;

	public UltraLabel lblNotSentColor;

	public UltraLabel lblSentFailed;

	public UltraLabel lblSentFailedColor;

	public UltraGrid ULGData;

	public frmMessagesSend()
	{
		InitializeComponent();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
		dtpToDate.Value = dtpFromDate.DateTime.AddMonths(1).AddDays(-1.0);
		dtSMSSettings = BusinessLayer.SMS.Settings.SelectByBranchID(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtStatus = MessagesStatus.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowMultiCellOperations = (AllowMultiCellOperation)4;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SendSMS"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SendSMS"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SendSMS"].DefaultCellValue = 0;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SendSMS"].Header).Caption = (GlobalVariables.IsArabic ? "ارسال" : "Send");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SendSMS"].Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SendSMS"].Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MessageNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MessageNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MessageNo"].Header).Caption = (GlobalVariables.IsArabic ? "الرقم" : "No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MessageDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MessageDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MessageDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date ");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Header).Caption = (GlobalVariables.IsArabic ? "إسم العميل" : "Client Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerName"].Header).Caption = (GlobalVariables.IsArabic ? " عميل CRM" : "Customer Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MessageText"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MessageText"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MessageText"].Header).Caption = (GlobalVariables.IsArabic ? "محتوى الرساله" : "Message Text");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "البيان" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PhoneNumber"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PhoneNumber"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PhoneNumber"].Header).Caption = (GlobalVariables.IsArabic ? "رفم المحمول" : "Phone Number");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpectedSendDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpectedSendDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpectedSendDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الارسال المتوقع" : "Expected Send Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpectedSendDate"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AddedUserName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AddedUserName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AddedUserName"].Header).Caption = (GlobalVariables.IsArabic ? "اضيفت بواسطة" : "Added By");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AddedDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AddedDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AddedDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الاضافة" : "Added Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AddedDate"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("Open"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns.Insert(0, "Open");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Header).Caption = (GlobalVariables.IsArabic ? "مراجعة" : "View");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count - 1));
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["Open"].Value = (GlobalVariables.IsArabic ? "مراجعة" : "View");
			if (((UltraGridBase)ULGData).Rows[i].Cells["StatusID"].Value == DBNull.Value)
			{
				((AppearanceBase)((UltraGridBase)ULGData).Rows[i].Appearance).BackColor = ((ControlBase)lblNotSentColor).Appearance.BackColor;
				((AppearanceBase)((UltraGridBase)ULGData).Rows[i].Appearance).ForeColor = Color.Black;
			}
			else if (((UltraGridBase)ULGData).Rows[i].Cells["StatusID"].Value != DBNull.Value)
			{
				((AppearanceBase)((UltraGridBase)ULGData).Rows[i].Appearance).BackColor = ((ControlBase)lblSentFailedColor).Appearance.BackColor;
				((AppearanceBase)((UltraGridBase)ULGData).Rows[i].Appearance).ForeColor = Color.Black;
			}
		}
	}

	private void FillGrid()
	{
		dtDetails = MessagesLog.SelectToSend("," + GlobalVariables.CurrentBranchID + ",", (dtpFromDate.Value == null) ? "Null" : dtpFromDate.DateTime.ToString(GlobalVariables.DateLongFormate), (dtpToDate.Value == null) ? "Null" : dtpToDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "SendSMS")
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
		}
	}

	private void ULGData_ClickCellButton(object sender, CellEventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			frmMessages frmMessages2 = new frmMessages(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["MessageID"].Value.ToString()));
			frmMessages2.Size = new Size(base.Width, base.Height);
			frmMessages2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmMessages2.lblTitle).Text = (GlobalVariables.IsArabic ? "الرسائل" : "Messages");
			frmMessages2.ShowDialog();
		}
	}

	private void btnSearch_Click(object sender, EventArgs e)
	{
		if (dtpFromDate.Value == null && dtpToDate.Value == null)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء ادخال اي من بيانات البحث" : "Please Enter at least one Search Criteria");
		}
		else
		{
			FillGrid();
		}
	}

	private async void btnSendSMS_Click(object sender, EventArgs e)
	{
		if (dtSMSSettings.Rows.Count == 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال الاعدادات" : "There are no settings for your branch");
			return;
		}
		((UltraGridBase)ULGData).UpdateData();
		if (((DataTable)((UltraGridBase)ULGData).DataSource).Select("SendSMS = 1").Length == 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا توجد رسائل لإرسالها " : "There are No Messages To Send");
			return;
		}
		HttpClient client = new HttpClient();
		string Login_URL1 = "https://smsmisr.com/api/webapi/?";
		string strUpdateDetailStatus = "";
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["SendSMS"].Value.ToString()))
			{
				string strData = "Username=" + dtSMSSettings.Rows[0]["UserName"].ToString().Trim() + "&password=" + dtSMSSettings.Rows[0]["UserPassword"].ToString().Trim() + "&language=" + ((UltraGridBase)ULGData).Rows[i].Cells["language"].Value.ToString() + "&sender=" + dtSMSSettings.Rows[0]["SenderID"].ToString().Trim() + "&Mobile=" + ((UltraGridBase)ULGData).Rows[i].Cells["PhoneNumber"].Value.ToString().Trim() + "&message=" + ((UltraGridBase)ULGData).Rows[i].Cells["MessageText"].Value.ToString().Trim() + "&DelayUntil=" + DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ExpectedSendDate"].Value.ToString()).ToString("yyyy-MM-dd-HH-mm");
				string json = JsonConvert.SerializeObject((object)strData);
				StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage responseMessage = null;
				try
				{
					responseMessage = await client.PostAsync(Login_URL1 + strData, (HttpContent)(object)data);
				}
				catch (Exception ex)
				{
					Exception ex2 = ex;
					if (responseMessage == null)
					{
						responseMessage = new HttpResponseMessage();
					}
					responseMessage.StatusCode = HttpStatusCode.InternalServerError;
					responseMessage.ReasonPhrase = $"RestHttpClient.SendRequest failed: {ex2}";
				}
				if (responseMessage.IsSuccessStatusCode)
				{
					JObject resultobj = JObject.Parse(await responseMessage.Content.ReadAsStringAsync());
					string statusID = dtStatus.Select("MessageStatusCode = " + ((object)resultobj.GetValue("code")).ToString())[0]["MessageStatusID"].ToString();
					strUpdateDetailStatus = strUpdateDetailStatus + " EXEC SMS_MessagesLogDetails_UpdateStatusID  " + ((UltraGridBase)ULGData).Rows[i].Cells["MessageDetailID"].Value.ToString() + "," + statusID + "," + GlobalVariables.UserID + " ; ";
				}
			}
			if (strUpdateDetailStatus != "")
			{
				Main.ExecuteNonQuery(strUpdateDetailStatus);
			}
		}
		FillGrid();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnCopy_Click(object sender, EventArgs e)
	{
	}

	private void btnOpenTicket_Click(object sender, EventArgs e)
	{
		try
		{
			Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
			frmSupportingTickets frmSupportingTickets2 = new frmSupportingTickets(isError: false, isMessage: false, isFormQst: true, base.Name, "Question on form : " + base.Name, bitmap);
			frmSupportingTickets2.StartPosition = FormStartPosition.CenterScreen;
			((Control)(object)frmSupportingTickets2.lblTitle).Text = (GlobalVariables.IsArabic ? "طلب دعم" : "Supporting Tickets");
			frmSupportingTickets2.Tag = GlobalVariables.dtAllForms.Select("Form = 'ERP.Ticketing.frmSupportingTickets'")[0];
			frmSupportingTickets2.ShowDialog();
		}
		catch
		{
		}
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
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
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Expected O, but got Unknown
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Expected O, but got Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Expected O, but got Unknown
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Expected O, but got Unknown
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Expected O, but got Unknown
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Expected O, but got Unknown
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Expected O, but got Unknown
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Expected O, but got Unknown
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Expected O, but got Unknown
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Expected O, but got Unknown
		//IL_0cdb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce5: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SMS.Transactions.frmMessagesSend));
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
		this.btnKeyboard = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.dtpFromDate = new UltraDateTimeEditor();
		this.lblDate = new UltraLabel();
		this.dtpToDate = new UltraDateTimeEditor();
		this.ultraLabel5 = new UltraLabel();
		this.ultraLabel6 = new UltraLabel();
		this.btnOpenTicket = new UltraButton();
		this.btnCopy = new UltraButton();
		this.btnSearch = new UltraButton();
		this.btnSendSMS = new UltraButton();
		this.btnPrintInvoices = new UltraButton();
		this.lblNotSent = new UltraLabel();
		this.lblNotSentColor = new UltraLabel();
		this.lblSentFailed = new UltraLabel();
		this.lblSentFailedColor = new UltraLabel();
		this.ULGData = new UltraGrid();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val, "appearance22");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance23");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance24.Image");
		resources.ApplyResources(val3, "appearance24");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)7;
		((AppearanceBase)val4).Image = resources.GetObject("appearance25.Image");
		resources.ApplyResources(val4, "appearance25");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.dtpFromDate, "dtpFromDate");
		((UltraWinEditorMaskedControlBase)this.dtpFromDate).AlwaysInEditMode = true;
		this.dtpFromDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpFromDate).Name = "dtpFromDate";
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.dtpToDate, "dtpToDate");
		((UltraWinEditorMaskedControlBase)this.dtpToDate).AlwaysInEditMode = true;
		this.dtpToDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpToDate).Name = "dtpToDate";
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		this.ultraLabel5.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		this.ultraLabel6.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		((ControlBase)this.ultraLabel6).WrapText = false;
		resources.ApplyResources(this.btnOpenTicket, "btnOpenTicket");
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Name = "btnOpenTicket";
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Click += new System.EventHandler(btnOpenTicket_Click);
		resources.ApplyResources(this.btnCopy, "btnCopy");
		((AppearanceBase)val5).Image = resources.GetObject("appearance26.Image");
		resources.ApplyResources(val5, "appearance26");
		((ControlBase)this.btnCopy).Appearance = (AppearanceBase)(object)val5;
		((ControlBase)this.btnCopy).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCopy).Name = "btnCopy";
		((System.Windows.Forms.Control)(object)this.btnCopy).Click += new System.EventHandler(btnCopy_Click);
		resources.ApplyResources(this.btnSearch, "btnSearch");
		((System.Windows.Forms.Control)(object)this.btnSearch).Name = "btnSearch";
		((System.Windows.Forms.Control)(object)this.btnSearch).Click += new System.EventHandler(btnSearch_Click);
		resources.ApplyResources(this.btnSendSMS, "btnSendSMS");
		((AppearanceBase)val6).Image = resources.GetObject("appearance27.Image");
		resources.ApplyResources(val6, "appearance27");
		((ControlBase)this.btnSendSMS).Appearance = (AppearanceBase)(object)val6;
		((ControlBase)this.btnSendSMS).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSendSMS).Name = "btnSendSMS";
		((System.Windows.Forms.Control)(object)this.btnSendSMS).Click += new System.EventHandler(btnSendSMS_Click);
		resources.ApplyResources(this.btnPrintInvoices, "btnPrintInvoices");
		((AppearanceBase)val7).Image = resources.GetObject("appearance28.Image");
		resources.ApplyResources(val7, "appearance28");
		((ControlBase)this.btnPrintInvoices).Appearance = (AppearanceBase)(object)val7;
		((ControlBase)this.btnPrintInvoices).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnPrintInvoices).Name = "btnPrintInvoices";
		resources.ApplyResources(this.lblNotSent, "lblNotSent");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val8).Image = resources.GetObject("appearance29.Image");
		resources.ApplyResources(val8, "appearance29");
		((ControlBase)this.lblNotSent).Appearance = (AppearanceBase)(object)val8;
		this.lblNotSent.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotSent).Name = "lblNotSent";
		((ControlBase)this.lblNotSent).WrapText = false;
		resources.ApplyResources(this.lblNotSentColor, "lblNotSentColor");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.LemonChiffon;
		resources.ApplyResources(val9, "appearance30");
		((ControlBase)this.lblNotSentColor).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.lblNotSentColor).Name = "lblNotSentColor";
		((UltraControlBase)this.lblNotSentColor).UseAppStyling = false;
		resources.ApplyResources(this.lblSentFailed, "lblSentFailed");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val10, "appearance31");
		((ControlBase)this.lblSentFailed).Appearance = (AppearanceBase)(object)val10;
		this.lblSentFailed.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSentFailed).Name = "lblSentFailed";
		((ControlBase)this.lblSentFailed).WrapText = false;
		resources.ApplyResources(this.lblSentFailedColor, "lblSentFailedColor");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Crimson;
		resources.ApplyResources(val11, "appearance32");
		((ControlBase)this.lblSentFailedColor).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.lblSentFailedColor).Name = "lblSentFailedColor";
		((UltraControlBase)this.lblSentFailedColor).UseAppStyling = false;
		resources.ApplyResources(this.ULGData, "ULGData");
		((UltraGridBase)this.ULGData).DisplayLayout.DefaultSelectedBackColor = System.Drawing.Color.Empty;
		((UltraGridBase)this.ULGData).DisplayLayout.DefaultSelectedForeColor = System.Drawing.Color.Empty;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val12).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val12).BorderColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val12).Image = resources.GetObject("appearance12.Image");
		resources.ApplyResources(val12, "appearance12");
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val12;
		((AppearanceBase)val13).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val13, "appearance13");
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val13;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val14).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val14).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val14, "appearance14");
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val15).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val15).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val15, "appearance15");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val15;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val16, "appearance16");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val16;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val17).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val17, "appearance17");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val17;
		((AppearanceBase)val18).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val18, "appearance18");
		((AppearanceBase)val18).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val18;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val19).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val19).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val19).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val19).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val19).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val19, "appearance19");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val19;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val20).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val20).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val20).Image = resources.GetObject("appearance20.Image");
		resources.ApplyResources(val20, "appearance20");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val20;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val21).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val21, "appearance21");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val21;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		((UltraControlBase)this.ULGData).UseAppStyling = false;
		this.ULGData.AfterEnterEditMode += new System.EventHandler(ULGData_AfterEnterEditMode);
		this.ULGData.ClickCellButton += new CellEventHandler(ULGData_ClickCellButton);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotSent);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotSentColor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSentFailed);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSentFailedColor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPrintInvoices);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSendSMS);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCopy);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOpenTicket);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmMessagesSend";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel5, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel6, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCopy, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSendSMS, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPrintInvoices, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSentFailedColor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSentFailed, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotSentColor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotSent, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
