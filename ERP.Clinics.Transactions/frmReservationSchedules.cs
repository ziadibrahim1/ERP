using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Clinics;
using DoctorScheduleControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.Clinics.Transactions;

public class frmReservationSchedules : frmBase
{
	private DataTable dtDoctors;

	private int ClinicID;

	private DateTime dtFromDate;

	public ArrayList ScheduleControllist = new ArrayList();

	private IContainer components = null;

	public UltraLabel lblTitle2;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	private UltraTabControl UTCDoctors;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private Timer timer1;

	public frmReservationSchedules()
	{
		InitializeComponent();
		((Control)(object)lblTitle).Text = (GlobalVariables.IsArabic ? "الحجز" : "Reservations");
	}

	public frmReservationSchedules(int ID)
		: this()
	{
		ClinicID = ID;
	}

	public override void PrepareData()
	{
		dtDoctors = Doctors.FillComboByClinicID(ClinicID.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtFromDate = LastSaturday(DateTime.Now);
		CreateSchedules();
		timer1.Start();
	}

	private void FillData()
	{
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void CreateSchedules()
	{
		for (int i = 0; i < dtDoctors.Rows.Count; i++)
		{
			((UltraTabControlBase)UTCDoctors).Tabs.Add(dtDoctors.Rows[i]["DoctorID"].ToString(), dtDoctors.Rows[i]["DoctorName"].ToString());
			DoctorScheduleUserControl doctorScheduleUserControl = new DoctorScheduleUserControl();
			ScheduleControllist.Add(doctorScheduleUserControl);
			doctorScheduleUserControl.Name = dtDoctors.Rows[i]["DoctorID"].ToString();
			doctorScheduleUserControl.Dock = DockStyle.Fill;
			doctorScheduleUserControl.FromDate = dtFromDate;
			doctorScheduleUserControl.MinStartTime = int.Parse(dtDoctors.Rows[i]["MinFromTime"].ToString());
			doctorScheduleUserControl.MaxEndTime = int.Parse(dtDoctors.Rows[i]["MaxToTime"].ToString()) + 1;
			doctorScheduleUserControl.DateTimeMaskInput = "dd/mm/yyyy";
			doctorScheduleUserControl.DoctorSchedule = DoctorsSchedules.SelectByDoctorIDClinicID(dtDoctors.Rows[i]["DoctorID"].ToString(), ClinicID.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			doctorScheduleUserControl.DoctorReservation = Reservations.SelectByDoctorIDClinicID(dtDoctors.Rows[i]["DoctorID"].ToString(), ClinicID.ToString(), dtFromDate.ToString(GlobalVariables.DateShortFormate), dtFromDate.AddDays(7.0).ToString(GlobalVariables.DateShortFormate), "," + GlobalVariables.CurrentBranchID + ",", GlobalVariables.IsArabic ? "1" : "0");
			doctorScheduleUserControl.ReservedColor = Color.Red;
			doctorScheduleUserControl.AvailableColor = Color.LightSteelBlue;
			doctorScheduleUserControl.ClosedColor = Color.Gray;
			doctorScheduleUserControl.ArrivedColor = Color.Green;
			doctorScheduleUserControl.btnItemsClick += DS_btnItemsClick;
			doctorScheduleUserControl.btnNextClick += DS_btnNextClick;
			doctorScheduleUserControl.btnPreviousClick += DS_btnPreviousClick;
			((Control)(object)((UltraTabControlBase)UTCDoctors).Tabs[dtDoctors.Rows[i]["DoctorID"].ToString()].TabPage).Controls.Add(doctorScheduleUserControl);
			doctorScheduleUserControl.CreateSchedule();
			doctorScheduleUserControl.Invalidate();
		}
	}

	private void DS_btnItemsClick(object sender, DoctorScheduleUserControl.MyEventArgs e)
	{
		frmReservations frmReservations2 = new frmReservations(e.ReservationID, int.Parse(ClinicID.ToString()), int.Parse(((KeyedSubObjectBase)((UltraTabControlBase)UTCDoctors).ActiveTab).Key), (e.IndexButton + 1).ToString(), dtFromDate.AddDays(e.IndexRow).Date.Add(e.ReservationTime), -1);
		frmReservations2.StartPosition = FormStartPosition.CenterParent;
		frmReservations2.Tag = base.Tag;
		frmReservations2.Width = base.Parent.Width;
		frmReservations2.Height = base.Parent.Height;
		((Control)(object)frmReservations2.lblTitle).Text = (GlobalVariables.IsArabic ? "مواعيد الحجز" : "Reservation Schedule");
		frmReservations2.CanAdd = CanAdd;
		frmReservations2.CanUpdate = CanUpdate;
		frmReservations2.CanDelete = CanDelete;
		frmReservations2.CanDiscount = CanDiscount;
		frmReservations2.CanSearching = CanSearching;
		frmReservations2.CanExport = CanExport;
		frmReservations2.CanPrint = CanPrint;
		frmReservations2.CanPrintReport = CanPrintReport;
		frmReservations2.CanViewReport = CanViewReport;
		frmReservations2.ShowDialog();
		frmReservations2.BringToFront();
		DoctorScheduleUserControl doctorScheduleUserControl = (DoctorScheduleUserControl)sender;
		doctorScheduleUserControl.DoctorReservation = Reservations.SelectByDoctorIDClinicID(((KeyedSubObjectBase)((UltraTabControlBase)UTCDoctors).ActiveTab).Key, ClinicID.ToString(), doctorScheduleUserControl.FromDate.ToString(GlobalVariables.DateShortFormate), doctorScheduleUserControl.FromDate.AddDays(7.0).ToString(GlobalVariables.DateShortFormate), "," + GlobalVariables.CurrentBranchID + ",", GlobalVariables.IsArabic ? "1" : "0");
		doctorScheduleUserControl.RefreshSchedule();
	}

	private void DS_btnPreviousClick(object sender, EventArgs e)
	{
		DoctorScheduleUserControl doctorScheduleUserControl = (DoctorScheduleUserControl)sender;
		doctorScheduleUserControl.FromDate = (dtFromDate = doctorScheduleUserControl.FromDate.AddDays(-7.0));
		doctorScheduleUserControl.DoctorReservation = Reservations.SelectByDoctorIDClinicID(((KeyedSubObjectBase)((UltraTabControlBase)UTCDoctors).ActiveTab).Key, ClinicID.ToString(), doctorScheduleUserControl.FromDate.ToString(GlobalVariables.DateShortFormate), doctorScheduleUserControl.FromDate.AddDays(7.0).ToString(GlobalVariables.DateShortFormate), "," + GlobalVariables.CurrentBranchID + ",", GlobalVariables.IsArabic ? "1" : "0");
		doctorScheduleUserControl.RefreshSchedule();
	}

	private void DS_btnNextClick(object sender, EventArgs e)
	{
		DoctorScheduleUserControl doctorScheduleUserControl = (DoctorScheduleUserControl)sender;
		doctorScheduleUserControl.FromDate = (dtFromDate = doctorScheduleUserControl.FromDate.AddDays(7.0));
		doctorScheduleUserControl.DoctorReservation = Reservations.SelectByDoctorIDClinicID(((KeyedSubObjectBase)((UltraTabControlBase)UTCDoctors).ActiveTab).Key, ClinicID.ToString(), doctorScheduleUserControl.FromDate.ToString(GlobalVariables.DateShortFormate), doctorScheduleUserControl.FromDate.AddDays(7.0).ToString(GlobalVariables.DateShortFormate), "," + GlobalVariables.CurrentBranchID + ",", GlobalVariables.IsArabic ? "1" : "0");
		doctorScheduleUserControl.RefreshSchedule();
	}

	private DateTime LastSaturday(DateTime now)
	{
		while (now.DayOfWeek != DayOfWeek.Saturday)
		{
			now = now.AddDays(-1.0);
		}
		return now;
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		for (int i = 0; i < ScheduleControllist.Count; i++)
		{
			DoctorScheduleUserControl doctorScheduleUserControl = (DoctorScheduleUserControl)ScheduleControllist[i];
			doctorScheduleUserControl.DoctorReservation = Reservations.SelectByDoctorIDClinicID(((KeyedSubObjectBase)((UltraTabControlBase)UTCDoctors).ActiveTab).Key, ClinicID.ToString(), doctorScheduleUserControl.FromDate.ToString(GlobalVariables.DateShortFormate), doctorScheduleUserControl.FromDate.AddDays(7.0).ToString(GlobalVariables.DateShortFormate), "," + GlobalVariables.CurrentBranchID + ",", GlobalVariables.IsArabic ? "1" : "0");
			doctorScheduleUserControl.RefreshSchedule();
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
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Clinics.Transactions.frmReservationSchedules));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		this.lblTitle2 = new UltraLabel();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.UTCDoctors = new UltraTabControl();
		this.ultraTabSharedControlsPage1 = new UltraTabSharedControlsPage();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UTCDoctors).BeginInit();
		((System.Windows.Forms.Control)(object)this.UTCDoctors).SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance3.Image");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.UTCDoctors, "UTCDoctors");
		((AppearanceBase)val4).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.UTCDoctors).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.UTCDoctors).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.UTCDoctors).Name = "UTCDoctors";
		((UltraTabControlBase)this.UTCDoctors).SharedControlsPage = this.ultraTabSharedControlsPage1;
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		this.timer1.Interval = 30000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UTCDoctors);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmReservationSchedules";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UTCDoctors, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UTCDoctors).EndInit();
		((System.Windows.Forms.Control)(object)this.UTCDoctors).ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
