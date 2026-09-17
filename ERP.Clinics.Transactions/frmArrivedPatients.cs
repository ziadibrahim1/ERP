using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Clinics;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Clinics.Transactions;

public class frmArrivedPatients : frmBase
{
	private DataTable dtClinics;

	private DataTable dtDoctors;

	private DataTable dtDetails;

	private ValueList vlEmployees = new ValueList();

	private ValueList vlVacationtypes = new ValueList();

	private ValueList vlShifts = new ValueList();

	private ValueList vlReservationType = new ValueList();

	private string DoctorID = "-1";

	private IContainer components = null;

	public UltraGrid ULGData;

	public UltraLabel lblTitle;

	private UltraComboEditor cboClinic;

	private UltraLabel lblCinic;

	public UltraButton btnClose;

	protected Timer timer1;

	public frmArrivedPatients()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		InitializeComponent();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		vlReservationType.ValueListItems.Clear();
		vlReservationType.ValueListItems.Add((object)"1", GlobalVariables.IsArabic ? "كشف" : "Examination");
		vlReservationType.ValueListItems.Add((object)"2", GlobalVariables.IsArabic ? "استشارة" : "Consultation");
		vlReservationType.ValueListItems.Add((object)"3", GlobalVariables.IsArabic ? "تمديد" : "Extension");
		dtDoctors = Doctors.SelectByDoctorUserID(GlobalVariables.UserID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (dtDoctors.Rows.Count > 0)
		{
			DoctorID = dtDoctors.Rows[0]["DoctorID"].ToString();
			dtClinics = BusinessLayer.Clinics.Clinics.FillComboByDoctorID(DoctorID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboClinic, dtClinics, "ClinicID", "ClinicName");
			if (dtClinics.Rows.Count > 0)
			{
				cboClinic.SelectedIndex = 0;
				DisplayData();
			}
		}
		timer1.Start();
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OrderNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OrderNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الحجز" : "Order No.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OrderNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReservationType"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReservationType"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الحجز" : "Reservation Type");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReservationType"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReservationType"].ValueList = (IValueList)(object)vlReservationType;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AppointmentDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AppointmentDate"].Header).Caption = (GlobalVariables.IsArabic ? "ميعاد الحجز" : "Appointment Time");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AppointmentDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AppointmentDate"].MaskInput = "dd/mm/yyyy hh:mm tt";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ArrivalDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ArrivalDate"].Header).Caption = (GlobalVariables.IsArabic ? "ميعاد الوصول" : "Arrival Time");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ArrivalDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ArrivalDate"].MaskInput = "hh:mm tt";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PatientName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PatientName"].Header).Caption = (GlobalVariables.IsArabic ? "المريض" : "Patient Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PatientName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
	}

	public void DisplayData()
	{
		if (cboClinic.SelectedIndex > -1 && DoctorID != "-1")
		{
			dtDetails = Reservations.SelectArrived(DoctorID, ((TextEditorControlBase)cboClinic).Value.ToString(), GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
		}
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)ULGData.ActiveCell).Selected = true;
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void cboClinic_ValueChanged(object sender, EventArgs e)
	{
		DisplayData();
	}

	private void ULGData_ClickCell(object sender, ClickCellEventArgs e)
	{
		frmMedicalRecords frmMedicalRecords2 = new frmMedicalRecords(dtDetails.Rows[e.Cell.Row.Index], DoctorID, ((TextEditorControlBase)cboClinic).Value.ToString());
		frmMedicalRecords2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmMedicalRecords2.Tag = base.Tag;
		frmMedicalRecords2.Location = new Point(0, 0);
		frmMedicalRecords2.CanAdd = CanAdd;
		frmMedicalRecords2.CanUpdate = CanUpdate;
		frmMedicalRecords2.CanDelete = CanDelete;
		frmMedicalRecords2.CanDiscount = CanDiscount;
		frmMedicalRecords2.CanSearching = CanSearching;
		frmMedicalRecords2.CanExport = CanExport;
		frmMedicalRecords2.CanPrint = CanPrint;
		frmMedicalRecords2.CanPrintReport = CanPrintReport;
		frmMedicalRecords2.CanViewReport = CanViewReport;
		frmMedicalRecords2.CanMinimunCharge = CanMinimunCharge;
		frmMedicalRecords2.ShowDialog();
		DisplayData();
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		DisplayData();
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
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Clinics.Transactions.frmArrivedPatients));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		this.ULGData = new UltraGrid();
		this.lblTitle = new UltraLabel();
		this.cboClinic = new UltraComboEditor();
		this.lblCinic = new UltraLabel();
		this.btnClose = new UltraButton();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClinic).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ULGData, "ULGData");
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowColMoving = (AllowColMoving)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowColSizing = (AllowColSizing)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeCell = (SelectType)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeCol = (SelectType)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeRow = (SelectType)2;
		((UltraGridBase)this.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		((UltraControlBase)this.ULGData).UseAppStyling = false;
		((UltraControlBase)this.ULGData).UseOsThemes = (DefaultableBoolean)2;
		this.ULGData.AfterEnterEditMode += new System.EventHandler(ULGData_AfterEnterEditMode);
		this.ULGData.ClickCell += new ClickCellEventHandler(ULGData_ClickCell);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		((TextEditorControlBase)this.cboClinic).AlwaysInEditMode = true;
		resources.ApplyResources(this.cboClinic, "cboClinic");
		this.cboClinic.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClinic).Name = "cboClinic";
		((TextEditorControlBase)this.cboClinic).ValueChanged += new System.EventHandler(cboClinic_ValueChanged);
		resources.ApplyResources(this.lblCinic, "lblCinic");
		this.lblCinic.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCinic).Name = "lblCinic";
		((ControlBase)this.lblCinic).WrapText = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val2).Image = resources.GetObject("appearance2.Image");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val2;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		this.timer1.Interval = 5000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClinic);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCinic);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Name = "frmArrivedPatients";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCinic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClinic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClinic).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
