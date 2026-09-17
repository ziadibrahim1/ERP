using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Clinics;
using ClinicScheduleReservationUserControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.Clinics.Transactions;

public class frmReservationNewSchedules : frmBase
{
	private DataTable dtClinics;

	private DateTime dtFromDate;

	public ArrayList ScheduleControllist = new ArrayList();

	private IContainer components = null;

	public UltraLabel lblTitle2;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	private UltraTabControl UTCClinics;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private Timer timer1;

	private UltraButton btnrefresh;

	public frmReservationNewSchedules()
	{
		InitializeComponent();
		((Control)(object)lblTitle).Text = (GlobalVariables.IsArabic ? "الحجز" : "Reservations");
	}

	public override void PrepareData()
	{
		dtClinics = BusinessLayer.Clinics.Clinics.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtFromDate = LastSaturday(DateTime.Now);
		CreateSchedules();
		timer1.Start();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void CreateSchedules()
	{
		for (int i = 0; i < dtClinics.Rows.Count; i++)
		{
			((UltraTabControlBase)UTCClinics).Tabs.Add(dtClinics.Rows[i]["ClinicID"].ToString(), dtClinics.Rows[i]["ClinicName"].ToString());
			global::ClinicScheduleReservationUserControl.ClinicScheduleReservationUserControl clinicScheduleReservationUserControl = new global::ClinicScheduleReservationUserControl.ClinicScheduleReservationUserControl();
			ScheduleControllist.Add(clinicScheduleReservationUserControl);
			clinicScheduleReservationUserControl.Name = dtClinics.Rows[i]["ClinicID"].ToString();
			clinicScheduleReservationUserControl.Dock = DockStyle.Fill;
			clinicScheduleReservationUserControl.FromDate = dtFromDate;
			clinicScheduleReservationUserControl.DateTimeMaskInput = "dd/mm/yyyy";
			clinicScheduleReservationUserControl.ReservedColor = Color.Red;
			clinicScheduleReservationUserControl.AvailableColor = Color.LightSteelBlue;
			clinicScheduleReservationUserControl.ClosedColor = Color.Gray;
			clinicScheduleReservationUserControl.ArrivedColor = Color.Green;
			clinicScheduleReservationUserControl.btnDoctorPeriod += btnDoctorPeriod_Click;
			clinicScheduleReservationUserControl.btnNextClick += DS_btnNextClick;
			clinicScheduleReservationUserControl.btnPreviousClick += DS_btnPreviousClick;
			((Control)(object)((UltraTabControlBase)UTCClinics).Tabs[dtClinics.Rows[i]["ClinicID"].ToString()].TabPage).Controls.Add(clinicScheduleReservationUserControl);
			GenerateSchedule(clinicScheduleReservationUserControl, clinicScheduleReservationUserControl.FromDate, dtClinics.Rows[i]["ClinicID"].ToString());
		}
	}

	private void btnDoctorPeriod_Click(object sender, global::ClinicScheduleReservationUserControl.ClinicScheduleReservationUserControl.MyEventArgs e)
	{
		frmReservations frmReservations2 = new frmReservations(e.ReservationID, int.Parse(((KeyedSubObjectBase)((UltraTabControlBase)UTCClinics).ActiveTab).Key.ToString()), (e.AlternativeDoctorID == -1) ? e.DoctorID : e.AlternativeDoctorID, (e.IndexButton + 1).ToString(), dtFromDate.AddDays(e.IndexRow).Date.Add(e.ReservationTime), e.ClinicScheduleID);
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
		global::ClinicScheduleReservationUserControl.ClinicScheduleReservationUserControl clinicScheduleReservationUserControl = (global::ClinicScheduleReservationUserControl.ClinicScheduleReservationUserControl)sender;
		GenerateSchedule(clinicScheduleReservationUserControl, clinicScheduleReservationUserControl.FromDate, ((KeyedSubObjectBase)((UltraTabControlBase)UTCClinics).ActiveTab).Key);
	}

	private void DS_btnPreviousClick(object sender, EventArgs e)
	{
		global::ClinicScheduleReservationUserControl.ClinicScheduleReservationUserControl clinicScheduleReservationUserControl = (global::ClinicScheduleReservationUserControl.ClinicScheduleReservationUserControl)sender;
		GenerateSchedule(clinicScheduleReservationUserControl, clinicScheduleReservationUserControl.FromDate.AddDays(-7.0), ((KeyedSubObjectBase)((UltraTabControlBase)UTCClinics).ActiveTab).Key);
	}

	private void DS_btnNextClick(object sender, EventArgs e)
	{
		global::ClinicScheduleReservationUserControl.ClinicScheduleReservationUserControl clinicScheduleReservationUserControl = (global::ClinicScheduleReservationUserControl.ClinicScheduleReservationUserControl)sender;
		GenerateSchedule(clinicScheduleReservationUserControl, clinicScheduleReservationUserControl.FromDate.AddDays(7.0), ((KeyedSubObjectBase)((UltraTabControlBase)UTCClinics).ActiveTab).Key);
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
		if (base.Controls.Find(((KeyedSubObjectBase)((UltraTabControlBase)UTCClinics).ActiveTab).Key.ToString(), searchAllChildren: true).Length != 0)
		{
			global::ClinicScheduleReservationUserControl.ClinicScheduleReservationUserControl clinicScheduleReservationUserControl = (global::ClinicScheduleReservationUserControl.ClinicScheduleReservationUserControl)base.Controls.Find(((KeyedSubObjectBase)((UltraTabControlBase)UTCClinics).ActiveTab).Key.ToString(), searchAllChildren: true)[0];
			GenerateSchedule(clinicScheduleReservationUserControl, clinicScheduleReservationUserControl.FromDate, ((KeyedSubObjectBase)((UltraTabControlBase)UTCClinics).ActiveTab).Key);
		}
	}

	public void GenerateSchedule(global::ClinicScheduleReservationUserControl.ClinicScheduleReservationUserControl CS, DateTime FromDateTime, string ClinicID)
	{
		CS.FromDate = (dtFromDate = FromDateTime);
		CS.ClinicSchedule = ClinicsSchedule.SelectByClinicIDFromTo(ClinicID, CS.FromDate.ToString(GlobalVariables.DateShortFormate), CS.FromDate.Date.AddDays(7.0).AddSeconds(-1.0).ToString(GlobalVariables.DateLongFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		CS.ClinicReservation = Reservations.SelectByDoctorIDClinicID("-1", ClinicID, dtFromDate.ToString(GlobalVariables.DateShortFormate), dtFromDate.AddDays(7.0).AddSeconds(-1.0).ToString(GlobalVariables.DateLongFormate), "," + GlobalVariables.CurrentBranchID + ",", GlobalVariables.IsArabic ? "1" : "0");
		CS.MinStartTime = ((CS.ClinicSchedule.Compute("Min(FromDateHour)", null) != DBNull.Value) ? int.Parse(CS.ClinicSchedule.Compute("Min(FromDateHour)", null).ToString()) : 0);
		DataRow[] array = CS.ClinicSchedule.Select(" ToDateHour<FromDateHour");
		DataRow[] array2 = CS.ClinicSchedule.Select(" ToDateHour=max(ToDateHour)");
		if (array.Length == 0 && array2.Length != 0)
		{
			CS.MaxEndTime = int.Parse(array2[0]["ToDateHour"].ToString());
		}
		else
		{
			CS.MaxEndTime = 24;
		}
		CS.CreateSchedule();
		CS.Invalidate();
	}

	private void btnrefresh_Click(object sender, EventArgs e)
	{
		if (base.Controls.Find(((KeyedSubObjectBase)((UltraTabControlBase)UTCClinics).ActiveTab).Key.ToString(), searchAllChildren: true).Length != 0)
		{
			global::ClinicScheduleReservationUserControl.ClinicScheduleReservationUserControl clinicScheduleReservationUserControl = (global::ClinicScheduleReservationUserControl.ClinicScheduleReservationUserControl)base.Controls.Find(((KeyedSubObjectBase)((UltraTabControlBase)UTCClinics).ActiveTab).Key.ToString(), searchAllChildren: true)[0];
			GenerateSchedule(clinicScheduleReservationUserControl, clinicScheduleReservationUserControl.FromDate, ((KeyedSubObjectBase)((UltraTabControlBase)UTCClinics).ActiveTab).Key);
		}
		GC.Collect();
		GC.SuppressFinalize(this);
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
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Clinics.Transactions.frmReservationNewSchedules));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		this.lblTitle2 = new UltraLabel();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.UTCClinics = new UltraTabControl();
		this.ultraTabSharedControlsPage1 = new UltraTabSharedControlsPage();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.btnrefresh = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UTCClinics).BeginInit();
		((System.Windows.Forms.Control)(object)this.UTCClinics).SuspendLayout();
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
		resources.ApplyResources(this.UTCClinics, "UTCClinics");
		((AppearanceBase)val4).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.UTCClinics).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.UTCClinics).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.UTCClinics).Name = "UTCClinics";
		((UltraTabControlBase)this.UTCClinics).SharedControlsPage = this.ultraTabSharedControlsPage1;
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		this.timer1.Interval = 600000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		resources.ApplyResources(this.btnrefresh, "btnrefresh");
		((System.Windows.Forms.Control)(object)this.btnrefresh).Name = "btnrefresh";
		((System.Windows.Forms.Control)(object)this.btnrefresh).Click += new System.EventHandler(btnrefresh_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnrefresh);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UTCClinics);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmReservationNewSchedules";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UTCClinics, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnrefresh, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UTCClinics).EndInit();
		((System.Windows.Forms.Control)(object)this.UTCClinics).ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
