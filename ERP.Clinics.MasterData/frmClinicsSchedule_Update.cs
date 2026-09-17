using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Clinics;
using BusinessLayer.Privilege;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.Clinics.MasterData;

public class frmClinicsSchedule_Update : frmBase
{
	private DataRow drMaster;

	private DataTable dtStores;

	private DataTable dtDoctors;

	private DataTable dtAlternativeDoctors;

	private DataTable dtClinics;

	private string ClinicScheduleID = "";

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraButton btnSave;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	private UltraTextEditor txtNotes;

	private UltraLabel lblNotes;

	private UltraDateTimeEditor dtpToDate;

	private UltraLabel lblFromDate;

	private UltraDateTimeEditor dtpFromDate;

	private UltraLabel lblToDate;

	public UltraLabel lblHistory;

	private UltraLabel lblDoctor;

	private UltraComboEditor cboDoctor;

	private UltraLabel lblClinics;

	private UltraComboEditor cboClinic;

	private UltraComboEditor cboStore;

	private UltraLabel lblStore;

	private UltraComboEditor cboAlternativeDoctor;

	private UltraLabel ultraLabel1;

	private UltraCheckEditor chkIsBlocked;

	public frmClinicsSchedule_Update()
	{
		InitializeComponent();
	}

	public frmClinicsSchedule_Update(string _ClinicScheduleID)
		: this()
	{
		RowID = _ClinicScheduleID;
		ClinicScheduleID = _ClinicScheduleID;
		TableName = "CL_ClinicsSchedule";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtClinics = BusinessLayer.Clinics.Clinics.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClinic, dtClinics, "ClinicID", "ClinicName");
		dtDoctors = Doctors.FillCombo("-1", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDoctor, dtDoctors, "DoctorID", "DoctorName");
		dtAlternativeDoctors = dtDoctors.Copy();
		dtStores = Stores.FillCombo("-1", "0", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboStore, dtStores, "StoreID", "StoreName");
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = ClinicsSchedule.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
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

	public void DisplayData()
	{
		if (drMaster != null)
		{
			((Control)(object)lblHistory).Text = Trans_Log.GetRowHistory(TableName, RowID, GlobalVariables.IsArabic ? "1" : "0");
			DisplayDataDate = GlobalFunctions.GetServerDateTimeNow();
			dtAlternativeDoctors.Rows.Remove(dtAlternativeDoctors.Select("DoctorID = " + drMaster["DoctorID"].ToString())[0]);
			GlobalFunctions.FillCombo(cboAlternativeDoctor, dtAlternativeDoctors, "DoctorID", "DoctorName");
			((TextEditorControlBase)cboClinic).Value = drMaster["ClinicID"];
			((TextEditorControlBase)cboDoctor).Value = drMaster["DoctorID"];
			((TextEditorControlBase)cboAlternativeDoctor).Value = drMaster["AlternativeDoctorID"];
			((TextEditorControlBase)cboStore).Value = drMaster["StoreID"];
			dtpFromDate.DateTime = (DateTime)drMaster["FromDate"];
			dtpToDate.DateTime = (DateTime)drMaster["ToDate"];
			((UltraToggleEditorBase)chkIsBlocked).Checked = Convert.ToBoolean(drMaster["IsBlocked"]);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			dtpFromDate.MaskInput = "hh:mm tt";
			dtpToDate.MaskInput = "hh:mm tt";
		}
	}

	public bool ValidateData()
	{
		return true;
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			ClinicsSchedule.Insert_Update(RowID, drMaster["ClinicID"].ToString(), drMaster["DoctorID"].ToString(), (cboStore.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboStore).Value.ToString(), (dtpFromDate.Value == null) ? "Null" : dtpFromDate.DateTime.ToString(GlobalVariables.DateLongFormate), (dtpToDate.Value == null) ? "Null" : dtpToDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboAlternativeDoctor.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAlternativeDoctor).Value.ToString(), ((UltraToggleEditorBase)chkIsBlocked).Checked ? "1" : "0", ((Control)(object)txtNotes).Text, drMaster["DoctorScheduleDetailPeriodID"].ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
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

	private void dtpDateTime_Enter(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		((UltraWinEditorMaskedControlBase)(UltraDateTimeEditor)sender).SelectAll();
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
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Expected O, but got Unknown
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Expected O, but got Unknown
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Clinics.MasterData.frmClinicsSchedule_Update));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		this.btnKeyboard = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblNotes = new UltraLabel();
		this.dtpToDate = new UltraDateTimeEditor();
		this.lblFromDate = new UltraLabel();
		this.dtpFromDate = new UltraDateTimeEditor();
		this.lblToDate = new UltraLabel();
		this.lblHistory = new UltraLabel();
		this.lblDoctor = new UltraLabel();
		this.cboDoctor = new UltraComboEditor();
		this.lblClinics = new UltraLabel();
		this.cboClinic = new UltraComboEditor();
		this.cboStore = new UltraComboEditor();
		this.lblStore = new UltraLabel();
		this.cboAlternativeDoctor = new UltraComboEditor();
		this.ultraLabel1 = new UltraLabel();
		this.chkIsBlocked = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDoctor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClinic).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAlternativeDoctor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsBlocked).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val2).Image = resources.GetObject("appearance2.Image");
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val3).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val4).Image = resources.GetObject("appearance4.Image");
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val4;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.dtpToDate, "dtpToDate");
		((UltraWinEditorMaskedControlBase)this.dtpToDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpToDate).Name = "dtpToDate";
		this.dtpToDate.Value = null;
		resources.ApplyResources(this.lblFromDate, "lblFromDate");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.lblFromDate).Appearance = (AppearanceBase)(object)val6;
		this.lblFromDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFromDate).Name = "lblFromDate";
		((ControlBase)this.lblFromDate).WrapText = false;
		resources.ApplyResources(this.dtpFromDate, "dtpFromDate");
		((UltraWinEditorMaskedControlBase)this.dtpFromDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpFromDate).Name = "dtpFromDate";
		resources.ApplyResources(this.lblToDate, "lblToDate");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.lblToDate).Appearance = (AppearanceBase)(object)val7;
		this.lblToDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblToDate).Name = "lblToDate";
		((ControlBase)this.lblToDate).WrapText = false;
		resources.ApplyResources(this.lblHistory, "lblHistory");
		((System.Windows.Forms.Control)(object)this.lblHistory).Name = "lblHistory";
		((System.Windows.Forms.Control)(object)this.lblHistory).Click += new System.EventHandler(lblHistory_Click);
		resources.ApplyResources(this.lblDoctor, "lblDoctor");
		this.lblDoctor.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDoctor).Name = "lblDoctor";
		((ControlBase)this.lblDoctor).WrapText = false;
		resources.ApplyResources(this.cboDoctor, "cboDoctor");
		((TextEditorControlBase)this.cboDoctor).AlwaysInEditMode = true;
		this.cboDoctor.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboDoctor).Name = "cboDoctor";
		((EditorButtonControlBase)this.cboDoctor).ReadOnly = true;
		resources.ApplyResources(this.lblClinics, "lblClinics");
		this.lblClinics.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClinics).Name = "lblClinics";
		((ControlBase)this.lblClinics).WrapText = false;
		resources.ApplyResources(this.cboClinic, "cboClinic");
		((TextEditorControlBase)this.cboClinic).AlwaysInEditMode = true;
		this.cboClinic.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClinic).Name = "cboClinic";
		((EditorButtonControlBase)this.cboClinic).ReadOnly = true;
		resources.ApplyResources(this.cboStore, "cboStore");
		((TextEditorControlBase)this.cboStore).AlwaysInEditMode = true;
		this.cboStore.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboStore).Name = "cboStore";
		resources.ApplyResources(this.lblStore, "lblStore");
		this.lblStore.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStore).Name = "lblStore";
		((ControlBase)this.lblStore).WrapText = false;
		resources.ApplyResources(this.cboAlternativeDoctor, "cboAlternativeDoctor");
		((TextEditorControlBase)this.cboAlternativeDoctor).AlwaysInEditMode = true;
		this.cboAlternativeDoctor.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboAlternativeDoctor).Name = "cboAlternativeDoctor";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.chkIsBlocked, "chkIsBlocked");
		((System.Windows.Forms.Control)(object)this.chkIsBlocked).Name = "chkIsBlocked";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsBlocked);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDoctor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboAlternativeDoctor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboDoctor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClinics);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClinic);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblHistory);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Name = "frmClinicsSchedule_Update";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClinic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClinics, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboDoctor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboAlternativeDoctor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDoctor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsBlocked, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDoctor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClinic).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAlternativeDoctor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsBlocked).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
