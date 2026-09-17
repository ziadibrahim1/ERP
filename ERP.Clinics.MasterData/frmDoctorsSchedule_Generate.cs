using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Clinics;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.Clinics.MasterData;

public class frmDoctorsSchedule_Generate : frmBase
{
	private int DoctorID = 0;

	private int DoctorScheduleID = 0;

	private IContainer components = null;

	public UltraLabel lblTitle;

	private UltraButton btnGenerate;

	private UltraButton btnCancel;

	private UltraNumericEditor UNWeekNumber;

	private UltraLabel lblWeekNumber;

	private UltraLabel lblToDate;

	private UltraLabel lblFromDate;

	private UltraDateTimeEditor dtpToDate;

	private UltraDateTimeEditor dtpFromDate;

	public frmDoctorsSchedule_Generate()
	{
		InitializeComponent();
	}

	public frmDoctorsSchedule_Generate(int _DoctorScheduleID, int _DoctorID, int WeeksCount)
		: this()
	{
		UNWeekNumber.MaxValue = WeeksCount;
		DoctorID = _DoctorID;
		DoctorScheduleID = _DoctorScheduleID;
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (ValidateData())
		{
			if (ClinicsSchedule.SelectByDoctorIDFromTO(DoctorID.ToString(), dtpFromDate.DateTime.ToString(GlobalVariables.DateShortFormate), dtpToDate.DateTime.AddDays(1.0).AddSeconds(-1.0).ToString(GlobalVariables.DateLongFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false).Rows.Count > 0)
			{
				GlobalVariables.InformationMB.Show("يوجد بالفعل جدول في هذه الفتره", "There Is A Schedule During This Period Of Time");
				frmDoctorsSchedule_Update frmDoctorsSchedule_Update2 = new frmDoctorsSchedule_Update(DoctorID, dtpFromDate.DateTime, dtpToDate.DateTime.AddDays(1.0).AddSeconds(-1.0));
				frmDoctorsSchedule_Update2.ShowDialog();
			}
			else
			{
				ClinicsSchedule.Generate(DoctorScheduleID, dtpFromDate.DateTime.ToString(GlobalVariables.DateShortFormate), dtpToDate.DateTime.ToString(GlobalVariables.DateShortFormate), Convert.ToInt16(UNWeekNumber.Value), IsFromServer: false);
			}
			Close();
		}
	}

	public bool ValidateData()
	{
		if (dtpFromDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال تاريخ البداية", "Please Insert From Date");
			((Control)(object)dtpFromDate).Focus();
			return false;
		}
		if (dtpToDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال تاريخ النهاية", "Please Insert To Date");
			((Control)(object)dtpToDate).Focus();
			return false;
		}
		return true;
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
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Expected O, but got Unknown
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Clinics.MasterData.frmDoctorsSchedule_Generate));
		Appearance val = new Appearance();
		this.lblTitle = new UltraLabel();
		this.btnGenerate = new UltraButton();
		this.btnCancel = new UltraButton();
		this.UNWeekNumber = new UltraNumericEditor();
		this.lblWeekNumber = new UltraLabel();
		this.lblToDate = new UltraLabel();
		this.lblFromDate = new UltraLabel();
		this.dtpToDate = new UltraDateTimeEditor();
		this.dtpFromDate = new UltraDateTimeEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UNWeekNumber).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnGenerate, "btnGenerate");
		((System.Windows.Forms.Control)(object)this.btnGenerate).Name = "btnGenerate";
		((System.Windows.Forms.Control)(object)this.btnGenerate).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.UNWeekNumber, "UNWeekNumber");
		((UltraNumericEditorBase)this.UNWeekNumber).FormatString = "";
		this.UNWeekNumber.MaxValue = 4;
		this.UNWeekNumber.MinValue = 1;
		((System.Windows.Forms.Control)(object)this.UNWeekNumber).Name = "UNWeekNumber";
		((UltraNumericEditorBase)this.UNWeekNumber).PromptChar = ' ';
		((UltraNumericEditorBase)this.UNWeekNumber).SpinButtonDisplayStyle = (ButtonDisplayStyle)1;
		this.UNWeekNumber.SpinIncrement = 1;
		this.UNWeekNumber.Value = 1;
		resources.ApplyResources(this.lblWeekNumber, "lblWeekNumber");
		this.lblWeekNumber.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblWeekNumber).Name = "lblWeekNumber";
		((ControlBase)this.lblWeekNumber).WrapText = false;
		resources.ApplyResources(this.lblToDate, "lblToDate");
		this.lblToDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblToDate).Name = "lblToDate";
		((ControlBase)this.lblToDate).WrapText = false;
		resources.ApplyResources(this.lblFromDate, "lblFromDate");
		this.lblFromDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFromDate).Name = "lblFromDate";
		((ControlBase)this.lblFromDate).WrapText = false;
		resources.ApplyResources(this.dtpToDate, "dtpToDate");
		((UltraWinEditorMaskedControlBase)this.dtpToDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpToDate).Name = "dtpToDate";
		resources.ApplyResources(this.dtpFromDate, "dtpFromDate");
		((UltraWinEditorMaskedControlBase)this.dtpFromDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpFromDate).Name = "dtpFromDate";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UNWeekNumber);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblWeekNumber);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnGenerate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Name = "frmDoctorsSchedule_Generate";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnGenerate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblWeekNumber, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UNWeekNumber, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblToDate, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UNWeekNumber).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
