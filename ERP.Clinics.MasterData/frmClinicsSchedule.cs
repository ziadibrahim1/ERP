using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Clinics;
using ClinicScheduleControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.Clinics.MasterData;

public class frmClinicsSchedule : frmBase
{
	private DataTable dtClinics;

	private int ClinicID;

	private DateTime dtFromDate;

	public ArrayList ScheduleControllist = new ArrayList();

	private IContainer components = null;

	public UltraLabel lblTitle2;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	private UltraComboEditor cboClinic;

	private UltraLabel lblClinic;

	private UltraTabControl UTCClinics;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	public frmClinicsSchedule()
	{
		InitializeComponent();
		((Control)(object)lblTitle).Text = (GlobalVariables.IsArabic ? " جدول مواعيد العيادة " : "Clinic Schedule");
	}

	public frmClinicsSchedule(int ID)
		: this()
	{
		ClinicID = ID;
	}

	public override void PrepareData()
	{
		dtClinics = BusinessLayer.Clinics.Clinics.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboClinic, dtClinics, "ClinicID", "ClinicName");
		dtFromDate = LastSaturday(DateTime.Now);
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void CreateSchedules()
	{
		((UltraTabControlBase)UTCClinics).Tabs.Clear();
		((UltraTabControlBase)UTCClinics).Tabs.Add(((TextEditorControlBase)cboClinic).Value.ToString(), ((Control)(object)cboClinic).Text.ToString());
		ClinicScheduleUserControl clinicScheduleUserControl = new ClinicScheduleUserControl();
		ScheduleControllist.Add(clinicScheduleUserControl);
		clinicScheduleUserControl.Name = ((TextEditorControlBase)cboClinic).Value.ToString();
		clinicScheduleUserControl.Dock = DockStyle.Fill;
		clinicScheduleUserControl.FromDate = dtFromDate;
		clinicScheduleUserControl.MinStartTime = 0;
		clinicScheduleUserControl.MaxEndTime = 25;
		clinicScheduleUserControl.DateTimeMaskInput = "dd/mm/yyyy";
		clinicScheduleUserControl.ClinicSchedule = ClinicsSchedule.SelectByClinicIDFromTo(((TextEditorControlBase)cboClinic).Value.ToString(), clinicScheduleUserControl.FromDate.ToString(GlobalVariables.DateShortFormate), clinicScheduleUserControl.FromDate.AddDays(7.0).ToString(GlobalVariables.DateLongFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		clinicScheduleUserControl.BlockedColor = Color.Red;
		clinicScheduleUserControl.AvailableColor = Color.LightSteelBlue;
		clinicScheduleUserControl.btnDoctorPeriod += DS_btnDoctorPeriodClick;
		clinicScheduleUserControl.btnNextClick += DS_btnNextClick;
		clinicScheduleUserControl.btnPreviousClick += DS_btnPreviousClick;
		((Control)(object)((UltraTabControlBase)UTCClinics).Tabs[((TextEditorControlBase)cboClinic).Value.ToString()].TabPage).Controls.Add(clinicScheduleUserControl);
		clinicScheduleUserControl.CreateSchedule();
		clinicScheduleUserControl.Invalidate();
	}

	private void DS_btnDoctorPeriodClick(object sender, ClinicScheduleUserControl.MyEventArgs e)
	{
		frmClinicsSchedule_Update frmClinicsSchedule_Update2 = new frmClinicsSchedule_Update(e.ClinicScheduleID.ToString());
		frmClinicsSchedule_Update2.StartPosition = FormStartPosition.CenterParent;
		((Control)(object)frmClinicsSchedule_Update2.lblTitle).Text = (GlobalVariables.IsArabic ? " تعديل جدول العيادات" : "Clinic Schedule Update");
		frmClinicsSchedule_Update2.ShowDialog();
		ClinicScheduleUserControl clinicScheduleUserControl = (ClinicScheduleUserControl)sender;
		clinicScheduleUserControl.ClinicSchedule = ClinicsSchedule.SelectByClinicIDFromTo(((TextEditorControlBase)cboClinic).Value.ToString(), clinicScheduleUserControl.FromDate.ToString(GlobalVariables.DateShortFormate), clinicScheduleUserControl.FromDate.AddDays(7.0).ToString(GlobalVariables.DateLongFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		clinicScheduleUserControl.CreateSchedule();
		clinicScheduleUserControl.Invalidate();
	}

	private void DS_btnPreviousClick(object sender, EventArgs e)
	{
		ClinicScheduleUserControl clinicScheduleUserControl = (ClinicScheduleUserControl)sender;
		clinicScheduleUserControl.FromDate = clinicScheduleUserControl.FromDate.AddDays(-7.0);
		clinicScheduleUserControl.ClinicSchedule = ClinicsSchedule.SelectByClinicIDFromTo(((TextEditorControlBase)cboClinic).Value.ToString(), clinicScheduleUserControl.FromDate.ToString(GlobalVariables.DateShortFormate), clinicScheduleUserControl.FromDate.AddDays(7.0).ToString(GlobalVariables.DateLongFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		clinicScheduleUserControl.CreateSchedule();
		clinicScheduleUserControl.Invalidate();
	}

	private void DS_btnNextClick(object sender, EventArgs e)
	{
		ClinicScheduleUserControl clinicScheduleUserControl = (ClinicScheduleUserControl)sender;
		clinicScheduleUserControl.FromDate = clinicScheduleUserControl.FromDate.AddDays(7.0);
		clinicScheduleUserControl.ClinicSchedule = ClinicsSchedule.SelectByClinicIDFromTo(((TextEditorControlBase)cboClinic).Value.ToString(), clinicScheduleUserControl.FromDate.ToString(GlobalVariables.DateShortFormate), clinicScheduleUserControl.FromDate.AddDays(7.0).ToString(GlobalVariables.DateLongFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		clinicScheduleUserControl.CreateSchedule();
		clinicScheduleUserControl.Invalidate();
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
		}
	}

	private void cboClinic_ValueChanged(object sender, EventArgs e)
	{
		if (cboClinic.SelectedIndex != -1)
		{
			CreateSchedules();
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
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Expected O, but got Unknown
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected O, but got Unknown
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Clinics.MasterData.frmClinicsSchedule));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		this.lblTitle2 = new UltraLabel();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.cboClinic = new UltraComboEditor();
		this.lblClinic = new UltraLabel();
		this.UTCClinics = new UltraTabControl();
		this.ultraTabSharedControlsPage1 = new UltraTabSharedControlsPage();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClinic).BeginInit();
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
		((AppearanceBase)val2).Image = resources.GetObject("appearance2.Image");
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance3.Image");
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.cboClinic, "cboClinic");
		((System.Windows.Forms.Control)(object)this.cboClinic).Name = "cboClinic";
		((TextEditorControlBase)this.cboClinic).ValueChanged += new System.EventHandler(cboClinic_ValueChanged);
		resources.ApplyResources(this.lblClinic, "lblClinic");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.lblClinic).Appearance = (AppearanceBase)(object)val4;
		this.lblClinic.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClinic).Name = "lblClinic";
		((ControlBase)this.lblClinic).WrapText = false;
		resources.ApplyResources(this.UTCClinics, "UTCClinics");
		resources.ApplyResources(val5, "appearance5");
		((AppearanceBase)val5).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.UTCClinics).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.UTCClinics).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.UTCClinics).Name = "UTCClinics";
		((UltraTabControlBase)this.UTCClinics).SharedControlsPage = this.ultraTabSharedControlsPage1;
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UTCClinics);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClinic);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClinic);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmClinicsSchedule";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClinic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClinic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UTCClinics, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClinic).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UTCClinics).EndInit();
		((System.Windows.Forms.Control)(object)this.UTCClinics).ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
