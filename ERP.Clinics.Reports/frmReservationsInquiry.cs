using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Clinics;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Clinics.Transactions;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Clinics.Reports;

public class frmReservationsInquiry : frmBase
{
	private DataTable dtReservations;

	private DataTable dtPatients;

	private DataTable dtDoctors;

	private DataTable dtClinics;

	private ValueList vlPatients = new ValueList();

	private ValueList vlDoctors = new ValueList();

	private ValueList vlClinics = new ValueList();

	private int PatientID;

	private int ClinicID;

	private int DoctorID;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	public UltraButton btnSearch;

	public UltraGrid ULGData;

	public UltraButton btnPatientSearch;

	private UltraLabel lblPatientName;

	private UltraComboEditor cboPatients;

	private UltraComboEditor cboClinic;

	private UltraLabel lblCinic;

	private UltraLabel lblDoctor;

	private UltraComboEditor cboDoctor;

	public UltraButton btnDoctorSearch;

	public frmReservationsInquiry()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		InitializeComponent();
	}

	public frmReservationsInquiry(int _PatientID, int _ClinicID, int _DoctorID)
		: this()
	{
		PatientID = _PatientID;
		ClinicID = _ClinicID;
		DoctorID = _DoctorID;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtDoctors = Doctors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboDoctor, dtDoctors, "DoctorID", "DoctorName");
		vlDoctors.ValueListItems.Clear();
		for (int i = 0; i < dtDoctors.Rows.Count; i++)
		{
			vlDoctors.ValueListItems.Add(dtDoctors.Rows[i]["DoctorID"], dtDoctors.Rows[i]["DoctorName"].ToString());
		}
		dtClinics = BusinessLayer.Clinics.Clinics.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboClinic, dtClinics, "ClinicID", "ClinicName");
		vlClinics.ValueListItems.Clear();
		for (int j = 0; j < dtClinics.Rows.Count; j++)
		{
			vlClinics.ValueListItems.Add(dtClinics.Rows[j]["ClinicID"], dtClinics.Rows[j]["ClinicName"].ToString());
		}
		dtPatients = Patients.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboPatients, dtPatients, "PatientID", "PatientName");
		vlPatients.ValueListItems.Clear();
		for (int k = 0; k < dtPatients.Rows.Count; k++)
		{
			vlPatients.ValueListItems.Add(dtPatients.Rows[k]["PatientID"], dtPatients.Rows[k]["PatientName"].ToString());
		}
		FillData();
	}

	public void FillData()
	{
		if (PatientID != -1)
		{
			((TextEditorControlBase)cboPatients).Value = PatientID;
			((TextEditorControlBase)cboClinic).Value = ClinicID;
			((TextEditorControlBase)cboDoctor).Value = DoctorID;
			dtReservations = Reservations.Inquiry(GlobalVariables.CurrentBranchID, PatientID.ToString(), ClinicID.ToString(), DoctorID.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		}
		else
		{
			dtReservations = Reservations.Inquiry(GlobalVariables.CurrentBranchID, "0", "0", "0", GlobalVariables.IsArabic ? "1" : "0");
		}
		((UltraGridBase)ULGData).DataSource = dtReservations;
		InitGrid();
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الحجز" : "Reservation Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OrderNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.12);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OrderNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OrderNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الكشف" : "Order No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AppointmentDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AppointmentDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AppointmentDate"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AppointmentDate"].Header).Caption = (GlobalVariables.IsArabic ? "ميعاد الحجز" : "Appointment Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsArrived"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsArrived"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsArrived"].Header).Caption = (GlobalVariables.IsArabic ? "الحضور" : "Arrived");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsClosed"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsClosed"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsClosed"].Header).Caption = (GlobalVariables.IsArabic ? "مغلق" : "Closed");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PatientID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PatientID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PatientID"].ValueList = (IValueList)(object)vlPatients;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PatientID"].Header).Caption = (GlobalVariables.IsArabic ? "المريض" : "Patient");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DoctorID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DoctorID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DoctorID"].ValueList = (IValueList)(object)vlDoctors;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DoctorID"].Header).Caption = (GlobalVariables.IsArabic ? "الطبيب" : "Doctor");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClinicID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClinicID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClinicID"].ValueList = (IValueList)(object)vlClinics;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClinicID"].Header).Caption = (GlobalVariables.IsArabic ? "العيادة" : "Clinic");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الصافي" : "Net Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaidAmount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaidAmount"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaidAmount"].Header).Caption = (GlobalVariables.IsArabic ? "المدفوع" : "Paid");
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("View"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns.Insert(0, "View");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["View"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["View"].Header).Caption = (GlobalVariables.IsArabic ? "عرض" : "View");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["View"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["View"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["View"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["View"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["View"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count - 1));
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["View"].Value = (GlobalVariables.IsArabic ? "عرض" : "View");
		}
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
		if (cboPatients.SelectedIndex == -1 && cboDoctor.SelectedIndex == -1 && cboClinic.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء اختيار المريض او العياده او الطبيب للبحث" : "Please Enter Valid Data to search Using It");
			return;
		}
		dtReservations = Reservations.Inquiry(GlobalVariables.CurrentBranchID, (cboPatients.SelectedIndex > -1) ? ((TextEditorControlBase)cboPatients).Value.ToString() : "-1", (cboClinic.SelectedIndex > -1) ? ((TextEditorControlBase)cboClinic).Value.ToString() : "-1", (cboDoctor.SelectedIndex > -1) ? ((TextEditorControlBase)cboDoctor).Value.ToString() : "-1", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtReservations;
		InitGrid();
	}

	private void btnPatientSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.PatientsSearch(IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboPatients).Value = num;
		}
	}

	private void btnDoctorSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.DoctorsSearch(IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboDoctor).Value = num;
		}
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)ULGData.ActiveCell).Selected = true;
	}

	private void ULGData_ClickCellButton(object sender, CellEventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && ULGData.ActiveCell != null && ((KeyedSubObjectBase)e.Cell.Column).Key == "View" && ((UltraGridBase)ULGData).ActiveRow.Cells["ReservationID"].Value != DBNull.Value)
		{
			frmReservations frmReservations2 = new frmReservations(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ReservationID"].Value.ToString()), int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ClinicID"].Value.ToString()), int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["DoctorID"].Value.ToString()), ((UltraGridBase)ULGData).ActiveRow.Cells["OrderNo"].Value.ToString(), Convert.ToDateTime(((UltraGridBase)ULGData).ActiveRow.Cells["AppointmentDate"].Value), (((UltraGridBase)ULGData).ActiveRow.Cells["ClinicScheduleID"].Value == DBNull.Value) ? (-1) : int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ClinicScheduleID"].Value.ToString()));
			frmReservations2.WindowState = FormWindowState.Maximized;
			((Control)(object)frmReservations2.lblTitle).Text = (GlobalVariables.IsArabic ? "الحجز" : "Reservation");
			frmReservations2.ShowDialog();
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
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Expected O, but got Unknown
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Expected O, but got Unknown
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_09ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f7: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Clinics.Reports.frmReservationsInquiry));
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
		this.btnKeyboard = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.btnSearch = new UltraButton();
		this.ULGData = new UltraGrid();
		this.btnPatientSearch = new UltraButton();
		this.lblPatientName = new UltraLabel();
		this.cboPatients = new UltraComboEditor();
		this.cboClinic = new UltraComboEditor();
		this.lblCinic = new UltraLabel();
		this.lblDoctor = new UltraLabel();
		this.cboDoctor = new UltraComboEditor();
		this.btnDoctorSearch = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPatients).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClinic).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDoctor).BeginInit();
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
		((AppearanceBase)val5).Image = resources.GetObject("appearance7.Image");
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance7.FontData");
		resources.ApplyResources(val5, "appearance7");
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
		this.ULGData.AfterEnterEditMode += new System.EventHandler(ULGData_AfterEnterEditMode);
		this.ULGData.ClickCellButton += new CellEventHandler(ULGData_ClickCellButton);
		((AppearanceBase)val16).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(((AppearanceBase)val16).FontData, "appearance2.FontData");
		resources.ApplyResources(val16, "appearance2");
		((SubObjectBase)val16).ForceApplyResources = "FontData|";
		((ControlBase)this.btnPatientSearch).Appearance = (AppearanceBase)(object)val16;
		resources.ApplyResources(this.btnPatientSearch, "btnPatientSearch");
		((System.Windows.Forms.Control)(object)this.btnPatientSearch).Name = "btnPatientSearch";
		((System.Windows.Forms.Control)(object)this.btnPatientSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnPatientSearch).Click += new System.EventHandler(btnPatientSearch_Click);
		resources.ApplyResources(this.lblPatientName, "lblPatientName");
		((System.Windows.Forms.Control)(object)this.lblPatientName).Name = "lblPatientName";
		((TextEditorControlBase)this.cboPatients).AlwaysInEditMode = true;
		this.cboPatients.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboPatients, "cboPatients");
		((System.Windows.Forms.Control)(object)this.cboPatients).Name = "cboPatients";
		((TextEditorControlBase)this.cboClinic).AlwaysInEditMode = true;
		resources.ApplyResources(this.cboClinic, "cboClinic");
		this.cboClinic.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClinic).Name = "cboClinic";
		resources.ApplyResources(this.lblCinic, "lblCinic");
		((System.Windows.Forms.Control)(object)this.lblCinic).Name = "lblCinic";
		resources.ApplyResources(this.lblDoctor, "lblDoctor");
		((System.Windows.Forms.Control)(object)this.lblDoctor).Name = "lblDoctor";
		((TextEditorControlBase)this.cboDoctor).AlwaysInEditMode = true;
		resources.ApplyResources(this.cboDoctor, "cboDoctor");
		this.cboDoctor.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboDoctor).Name = "cboDoctor";
		resources.ApplyResources(this.btnDoctorSearch, "btnDoctorSearch");
		((AppearanceBase)val17).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(((AppearanceBase)val17).FontData, "appearance8.FontData");
		resources.ApplyResources(val17, "appearance8");
		((SubObjectBase)val17).ForceApplyResources = "FontData|";
		((ControlBase)this.btnDoctorSearch).Appearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.btnDoctorSearch).Name = "btnDoctorSearch";
		((System.Windows.Forms.Control)(object)this.btnDoctorSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnDoctorSearch).Click += new System.EventHandler(btnDoctorSearch_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDoctor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboDoctor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDoctorSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClinic);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCinic);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPatientSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPatientName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPatients);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmReservationsInquiry";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPatients, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPatientName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPatientSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCinic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClinic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDoctorSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboDoctor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDoctor, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPatients).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClinic).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDoctor).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
