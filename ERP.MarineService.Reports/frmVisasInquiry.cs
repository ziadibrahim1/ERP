using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.MarineService;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Reports;

public class frmVisasInquiry : frmBase
{
	private DataTable dtOperationServicesVisas;

	private ValueList vlPassengerType = new ValueList();

	private ValueList vlOnOff = new ValueList();

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	private UltraTextEditor txtVisaNo;

	private UltraLabel lblVisaNo;

	private UltraTextEditor txtPassportNo;

	private UltraLabel lblPassportNo;

	public UltraButton btnSearch;

	public UltraGrid ULGData;

	private UltraTextEditor txtName;

	private UltraLabel lblName;

	public frmVisasInquiry()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		vlOnOff.ValueListItems.Clear();
		vlOnOff.ValueListItems.Add((object)true, GlobalVariables.IsArabic ? "On" : "On");
		vlOnOff.ValueListItems.Add((object)false, GlobalVariables.IsArabic ? "OFF" : "OFF");
		vlPassengerType.ValueListItems.Clear();
		vlPassengerType.ValueListItems.Add((object)"1", GlobalVariables.IsArabic ? "بحار" : "Seaman");
		vlPassengerType.ValueListItems.Add((object)"2", GlobalVariables.IsArabic ? "راكب" : "Passenger");
		vlPassengerType.ValueListItems.Add((object)"3", GlobalVariables.IsArabic ? "فني" : "Technician");
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.UseFixedHeaders = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaDate"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassportNo"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaDate"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassportNo"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NationalityName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NationalityName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NationalityName"].Header).Caption = (GlobalVariables.IsArabic ? "الجنسية" : "Nationality");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VesselName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VesselName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VesselName"].Header).Caption = (GlobalVariables.IsArabic ? "الباخره" : "Vessel");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم" : "Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassportNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassportNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassportNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الجواز" : "Passport No.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم التأشيرة" : "Visa No.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSignOn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSignOn"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSignOn"].Header).Caption = (GlobalVariables.IsArabic ? "الحاله" : "ON/OFF");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSignOn"].ValueList = (IValueList)(object)vlOnOff;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الهوية" : "ID No.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDIssueDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDIssueDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDIssueDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الإصدار" : "ID Issue Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidFromDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidFromDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidFromDate"].Header).Caption = (GlobalVariables.IsArabic ? "صالح من" : "Visa Issue Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidToDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidToDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidToDate"].Header).Caption = (GlobalVariables.IsArabic ? "حتي" : "Visa Expiry Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualFromDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualFromDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualFromDate"].Header).Caption = (GlobalVariables.IsArabic ? "دخول" : "Entry Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualToDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualToDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualToDate"].Header).Caption = (GlobalVariables.IsArabic ? "خروج" : "Exit Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoyageNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoyageNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).Caption = (GlobalVariables.IsArabic ? "الرحلة" : "Voyage");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaState"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaState"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaState"].Header).Caption = (GlobalVariables.IsArabic ? "الحالة" : "Status");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCompleted"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCompleted"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCompleted"].Header).Caption = (GlobalVariables.IsArabic ? "تمت" : "Completed");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AirPort"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AirPort"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AirPort"].Header).Caption = (GlobalVariables.IsArabic ? "المطار" : "AirPort");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RejectionReason"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RejectionReason"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RejectionReason"].Header).Caption = (GlobalVariables.IsArabic ? "سبب الرفض" : "Rejection Reason");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsRejected"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsRejected"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsRejected"].Header).Caption = (GlobalVariables.IsArabic ? "مرفوضه" : "Rejected");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyName"].Header).Caption = (GlobalVariables.IsArabic ? "الشركة" : "Company");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsUrgent"].Header).Caption = (GlobalVariables.IsArabic ? "عاجل" : "Urgent");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsUrgent"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsUrgent"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassengerType"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الراكب" : "Passenger Type");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassengerType"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassengerType"].ValueList = (IValueList)(object)vlPassengerType;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassengerType"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCancelled"].Header).Caption = (GlobalVariables.IsArabic ? "ملغاة" : "Cancelled");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCancelled"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCancelled"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CancellationDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الالغاء" : "Cancellation Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CancellationDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CancellationDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CancellationReason"].Header).Caption = (GlobalVariables.IsArabic ? "سبب الالغاء" : "Cancellation Reason");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CancellationReason"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CancellationReason"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntryExpireDate"].Header).Caption = (GlobalVariables.IsArabic ? "الخروج المتوقع" : "Entry Expire");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntryExpireDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntryExpireDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
	}

	private void btnSearch_Click(object sender, EventArgs e)
	{
		if (((Control)(object)txtPassportNo).Text.Trim().Equals("") && ((Control)(object)txtVisaNo).Text.Trim().Equals("") && ((Control)(object)txtName).Text.Trim().Equals(""))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء ادخال رقم الجواز او الاسم او رقم التأشيرة للبحث" : "Please Enter a Valid Passport No. or Visa No. or Name ");
			return;
		}
		dtOperationServicesVisas = OperationsServicesVisas.Inquiry(GlobalVariables.CurrentBranchID, ((Control)(object)txtPassportNo).Text.Trim().Equals("") ? "Null" : ((Control)(object)txtPassportNo).Text.Trim(), ((Control)(object)txtVisaNo).Text.Trim().Equals("") ? "Null" : ((Control)(object)txtVisaNo).Text.Trim(), ((Control)(object)txtName).Text.Trim().Equals("") ? "Null" : ((Control)(object)txtName).Text.Trim(), GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtOperationServicesVisas;
		InitGrid();
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
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Expected O, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected O, but got Unknown
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Expected O, but got Unknown
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected O, but got Unknown
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Expected O, but got Unknown
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Expected O, but got Unknown
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Expected O, but got Unknown
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Reports.frmVisasInquiry));
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
		this.btnKeyboard = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.txtVisaNo = new UltraTextEditor();
		this.lblVisaNo = new UltraLabel();
		this.txtPassportNo = new UltraTextEditor();
		this.lblPassportNo = new UltraLabel();
		this.btnSearch = new UltraButton();
		this.ULGData = new UltraGrid();
		this.txtName = new UltraTextEditor();
		this.lblName = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassportNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtName).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance3.FontData");
		resources.ApplyResources(val, "appearance3");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance16");
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance16.FontData");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance21.Image");
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance21.FontData");
		resources.ApplyResources(val3, "appearance21");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val4, "appearance11");
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance11.FontData");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.txtVisaNo, "txtVisaNo");
		((System.Windows.Forms.Control)(object)this.txtVisaNo).Name = "txtVisaNo";
		resources.ApplyResources(this.lblVisaNo, "lblVisaNo");
		((System.Windows.Forms.Control)(object)this.lblVisaNo).Name = "lblVisaNo";
		resources.ApplyResources(this.txtPassportNo, "txtPassportNo");
		((System.Windows.Forms.Control)(object)this.txtPassportNo).Name = "txtPassportNo";
		resources.ApplyResources(this.lblPassportNo, "lblPassportNo");
		((System.Windows.Forms.Control)(object)this.lblPassportNo).Name = "lblPassportNo";
		((AppearanceBase)val5).Image = resources.GetObject("appearance2.Image");
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance2.FontData");
		resources.ApplyResources(val5, "appearance2");
		((SubObjectBase)val5).ForceApplyResources = "FontData|";
		((ControlBase)this.btnSearch).Appearance = (AppearanceBase)(object)val5;
		resources.ApplyResources(this.btnSearch, "btnSearch");
		((System.Windows.Forms.Control)(object)this.btnSearch).Name = "btnSearch";
		((System.Windows.Forms.Control)(object)this.btnSearch).Click += new System.EventHandler(btnSearch_Click);
		resources.ApplyResources(this.ULGData, "ULGData");
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val6).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val6).BorderColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).Image = resources.GetObject("appearance1.Image");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance1.FontData");
		resources.ApplyResources(val6, "appearance1");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val6;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance4.FontData");
		resources.ApplyResources(val7, "appearance4");
		((SubObjectBase)val7).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val7;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val8).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val8).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(((AppearanceBase)val8).FontData, "appearance13.FontData");
		resources.ApplyResources(val8, "appearance13");
		((SubObjectBase)val8).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val9).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(((AppearanceBase)val9).FontData, "appearance5.FontData");
		resources.ApplyResources(val9, "appearance5");
		((SubObjectBase)val9).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val9;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val10).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(((AppearanceBase)val10).FontData, "appearance15.FontData");
		resources.ApplyResources(val10, "appearance15");
		((SubObjectBase)val10).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(((AppearanceBase)val11).FontData, "appearance6.FontData");
		resources.ApplyResources(val11, "appearance6");
		((SubObjectBase)val11).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val11;
		((AppearanceBase)val12).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val12).TextTrimming = (TextTrimming)3;
		resources.ApplyResources(((AppearanceBase)val12).FontData, "appearance17.FontData");
		resources.ApplyResources(val12, "appearance17");
		((SubObjectBase)val12).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val13).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val13).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val13).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(((AppearanceBase)val13).FontData, "appearance18.FontData");
		resources.ApplyResources(val13, "appearance18");
		((SubObjectBase)val13).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val14).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val14).Image = resources.GetObject("appearance19.Image");
		resources.ApplyResources(((AppearanceBase)val14).FontData, "appearance19.FontData");
		resources.ApplyResources(val14, "appearance19");
		((SubObjectBase)val14).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val15).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(((AppearanceBase)val15).FontData, "appearance20.FontData");
		resources.ApplyResources(val15, "appearance20");
		((SubObjectBase)val15).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		resources.ApplyResources(this.txtName, "txtName");
		((System.Windows.Forms.Control)(object)this.txtName).Name = "txtName";
		resources.ApplyResources(this.lblName, "lblName");
		((System.Windows.Forms.Control)(object)this.lblName).Name = "lblName";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPassportNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVisaNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPassportNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaNo);
		base.Name = "frmVisasInquiry";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPassportNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVisaNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPassportNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtName, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassportNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtName).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
