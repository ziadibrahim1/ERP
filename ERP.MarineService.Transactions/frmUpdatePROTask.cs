using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.MarineService;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.MarineService.Transactions;

public class frmUpdatePROTask : frmBase
{
	private DataTable dtUsers;

	private DataRow drMaster;

	private string OperationServiceStepTaskID = "";

	private bool WithOperation = true;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraButton btnSave;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	private UltraLabel lblVoyage;

	private UltraLabel lblEntryPort;

	private UltraTextEditor txtOperation;

	private UltraLabel lblOperation;

	private UltraLabel lblVessels;

	private UltraLabel lblService;

	private UltraLabel lblStep;

	private UltraTextEditor txtStep;

	private UltraTextEditor txtService;

	private UltraTextEditor txtVessel;

	private UltraTextEditor txtVoyage;

	private UltraCheckEditor chkForAllUsers;

	private UltraComboEditor cboUsers;

	private UltraLabel lblUser;

	private UltraTextEditor txtTask;

	private UltraLabel lblTask;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraDateTimeEditor dtpStartDate;

	private UltraTextEditor txtEntryPort;

	private UltraCheckEditor chkCompleted;

	private UltraLabel lblStartDate;

	public UltraLabel lblHistory;

	private UltraLabel lblServiceUser;

	private UltraTextEditor txtServiceUser;

	public frmUpdatePROTask()
	{
		InitializeComponent();
	}

	public frmUpdatePROTask(string ID, bool ForAllUsers, bool WithOperation)
		: this()
	{
		RowID = ID;
		OperationServiceStepTaskID = ID;
		this.WithOperation = WithOperation;
		((Control)(object)chkForAllUsers).Enabled = (((Control)(object)cboUsers).Enabled = ForAllUsers);
		TableName = "MS_OperationsServicesStepsTasks";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpStartDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboUsers, dtUsers, "UserID", "UserName");
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = ((!WithOperation) ? TaskWithoutOperation.SelectByID(RowID, GlobalVariables.IsArabic ? "1" : "0") : OperationsServicesStepsTasks.SelectByID(RowID, GlobalVariables.IsArabic ? "1" : "0"));
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
			((Control)(object)txtOperation).Text = drMaster["OperationNo"].ToString();
			dtpStartDate.Value = (DateTime)drMaster["StartDate"];
			((Control)(object)txtVoyage).Text = drMaster["VoyageNo"].ToString();
			((Control)(object)txtVessel).Text = drMaster["VesselName"].ToString();
			((Control)(object)txtService).Text = drMaster["ServiceName"].ToString();
			((Control)(object)txtServiceUser).Text = drMaster["ServiceUser"].ToString();
			((Control)(object)txtStep).Text = drMaster["StepName"].ToString();
			((Control)(object)txtTask).Text = drMaster["TaskName"].ToString();
			((TextEditorControlBase)cboUsers).Value = drMaster["User_ID"];
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((UltraToggleEditorBase)chkCompleted).Checked = bool.Parse(drMaster["IsCompleted"].ToString());
			((UltraToggleEditorBase)chkForAllUsers).Checked = bool.Parse(drMaster["ForAllUsers"].ToString());
			((Control)(object)txtEntryPort).Text = drMaster["EntryPort"].ToString();
		}
	}

	public bool ValidateData()
	{
		if (!((UltraToggleEditorBase)chkForAllUsers).Checked && cboUsers.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء اختيار المستخدم", "Please Select User");
			((TextEditorControlBase)cboUsers).Focus();
			return false;
		}
		return true;
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (!ValidateData())
		{
			return;
		}
		Main.StartBulkTrans(FromServer: false);
		try
		{
			if (WithOperation)
			{
				OperationsServicesStepsTasks.Insert_Update(RowID, drMaster["OperationServiceStepID"].ToString(), drMaster["OperationServiceID"].ToString(), drMaster["OperationID"].ToString(), (drMaster["TaskID"].ToString() == "") ? "Null" : drMaster["TaskID"].ToString(), (drMaster["TaskOrder"].ToString() == "") ? "Null" : drMaster["TaskOrder"].ToString(), (drMaster["TaskPlaceID"].ToString() == "") ? "Null" : drMaster["TaskPlaceID"].ToString(), drMaster["IsHold"].ToString(), ((UltraToggleEditorBase)chkForAllUsers).Checked ? "1" : "0", (cboUsers.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUsers).Value.ToString(), drMaster["StartDate"].ToString(), drMaster["ExpectedTime"].ToString(), (drMaster["EndDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["EndDate"].ToString()).ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkCompleted).Checked ? "1" : "0", drMaster["IsCancelled"].ToString(), ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			}
			else
			{
				TaskWithoutOperation.Insert_Update(RowID, drMaster["TaskWithoutOperationNo"].ToString(), drMaster["TaskID"].ToString(), drMaster["TaskOrder"].ToString(), drMaster["TaskPlaceID"].ToString(), drMaster["IsHold"].ToString(), ((UltraToggleEditorBase)chkForAllUsers).Checked ? "1" : "0", (cboUsers.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUsers).Value.ToString(), drMaster["StartDate"].ToString(), drMaster["ExpectedTime"].ToString(), (drMaster["EndDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["EndDate"].ToString()).ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkCompleted).Checked ? "1" : "0", drMaster["IsCancelled"].ToString(), ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			}
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

	public void ClearAllControls()
	{
		dtpStartDate.Value = DateTime.Now;
		((TextEditorControlBase)txtOperation).Clear();
		cboUsers.SelectedIndex = -1;
	}

	private void dtpDateTime_Enter(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		((UltraWinEditorMaskedControlBase)(UltraDateTimeEditor)sender).SelectAll();
	}

	private void chkForAllUsers_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboUsers).Enabled = !((UltraToggleEditorBase)chkForAllUsers).Checked;
		if (((UltraToggleEditorBase)chkForAllUsers).Checked)
		{
			cboUsers.SelectedIndex = -1;
		}
	}

	private void chkCompleted_CheckedChanged(object sender, EventArgs e)
	{
		if (((UltraToggleEditorBase)chkCompleted).Checked)
		{
			drMaster["EndDate"] = DateTime.Now;
		}
		else
		{
			drMaster["EndDate"] = DBNull.Value;
		}
	}

	private void frmUpdatePROTask_Load(object sender, EventArgs e)
	{
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
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Expected O, but got Unknown
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Expected O, but got Unknown
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Expected O, but got Unknown
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Expected O, but got Unknown
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Expected O, but got Unknown
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Expected O, but got Unknown
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Expected O, but got Unknown
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Expected O, but got Unknown
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Expected O, but got Unknown
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Expected O, but got Unknown
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Expected O, but got Unknown
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Expected O, but got Unknown
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Expected O, but got Unknown
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Expected O, but got Unknown
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Expected O, but got Unknown
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Expected O, but got Unknown
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmUpdatePROTask));
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
		this.btnKeyboard = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.lblVoyage = new UltraLabel();
		this.txtOperation = new UltraTextEditor();
		this.lblOperation = new UltraLabel();
		this.lblEntryPort = new UltraLabel();
		this.lblVessels = new UltraLabel();
		this.lblService = new UltraLabel();
		this.lblStep = new UltraLabel();
		this.txtStep = new UltraTextEditor();
		this.txtService = new UltraTextEditor();
		this.txtVessel = new UltraTextEditor();
		this.txtVoyage = new UltraTextEditor();
		this.chkForAllUsers = new UltraCheckEditor();
		this.cboUsers = new UltraComboEditor();
		this.lblUser = new UltraLabel();
		this.txtTask = new UltraTextEditor();
		this.lblTask = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.dtpStartDate = new UltraDateTimeEditor();
		this.txtEntryPort = new UltraTextEditor();
		this.chkCompleted = new UltraCheckEditor();
		this.lblStartDate = new UltraLabel();
		this.lblHistory = new UltraLabel();
		this.lblServiceUser = new UltraLabel();
		this.txtServiceUser = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOperation).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtStep).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtService).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVessel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVoyage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkForAllUsers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboUsers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTask).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpStartDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEntryPort).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkCompleted).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtServiceUser).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val, "appearance12");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val2).Image = resources.GetObject("appearance13.Image");
		resources.ApplyResources(val2, "appearance13");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val3).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val3, "appearance14");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val4).Image = resources.GetObject("appearance15.Image");
		resources.ApplyResources(val4, "appearance15");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val4;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val5, "appearance16");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblVoyage, "lblVoyage");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val6, "appearance17");
		((ControlBase)this.lblVoyage).Appearance = (AppearanceBase)(object)val6;
		this.lblVoyage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVoyage).Name = "lblVoyage";
		((ControlBase)this.lblVoyage).WrapText = false;
		resources.ApplyResources(this.txtOperation, "txtOperation");
		((System.Windows.Forms.Control)(object)this.txtOperation).Name = "txtOperation";
		((EditorButtonControlBase)this.txtOperation).ReadOnly = true;
		resources.ApplyResources(this.lblOperation, "lblOperation");
		this.lblOperation.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOperation).Name = "lblOperation";
		((ControlBase)this.lblOperation).WrapText = false;
		resources.ApplyResources(this.lblEntryPort, "lblEntryPort");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance18");
		((ControlBase)this.lblEntryPort).Appearance = (AppearanceBase)(object)val7;
		this.lblEntryPort.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEntryPort).Name = "lblEntryPort";
		((ControlBase)this.lblEntryPort).WrapText = false;
		resources.ApplyResources(this.lblVessels, "lblVessels");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance19");
		((ControlBase)this.lblVessels).Appearance = (AppearanceBase)(object)val8;
		this.lblVessels.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVessels).Name = "lblVessels";
		((ControlBase)this.lblVessels).WrapText = false;
		resources.ApplyResources(this.lblService, "lblService");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance20");
		((ControlBase)this.lblService).Appearance = (AppearanceBase)(object)val9;
		this.lblService.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblService).Name = "lblService";
		((ControlBase)this.lblService).WrapText = false;
		resources.ApplyResources(this.lblStep, "lblStep");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance21");
		((ControlBase)this.lblStep).Appearance = (AppearanceBase)(object)val10;
		this.lblStep.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStep).Name = "lblStep";
		((ControlBase)this.lblStep).WrapText = false;
		resources.ApplyResources(this.txtStep, "txtStep");
		((System.Windows.Forms.Control)(object)this.txtStep).Name = "txtStep";
		((EditorButtonControlBase)this.txtStep).ReadOnly = true;
		resources.ApplyResources(this.txtService, "txtService");
		((System.Windows.Forms.Control)(object)this.txtService).Name = "txtService";
		((EditorButtonControlBase)this.txtService).ReadOnly = true;
		resources.ApplyResources(this.txtVessel, "txtVessel");
		((System.Windows.Forms.Control)(object)this.txtVessel).Name = "txtVessel";
		((EditorButtonControlBase)this.txtVessel).ReadOnly = true;
		resources.ApplyResources(this.txtVoyage, "txtVoyage");
		((System.Windows.Forms.Control)(object)this.txtVoyage).Name = "txtVoyage";
		((EditorButtonControlBase)this.txtVoyage).ReadOnly = true;
		resources.ApplyResources(this.chkForAllUsers, "chkForAllUsers");
		((System.Windows.Forms.Control)(object)this.chkForAllUsers).Name = "chkForAllUsers";
		((UltraToggleEditorBase)this.chkForAllUsers).CheckedChanged += new System.EventHandler(chkForAllUsers_CheckedChanged);
		resources.ApplyResources(this.cboUsers, "cboUsers");
		this.cboUsers.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboUsers).Name = "cboUsers";
		resources.ApplyResources(this.lblUser, "lblUser");
		this.lblUser.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUser).Name = "lblUser";
		((ControlBase)this.lblUser).WrapText = false;
		resources.ApplyResources(this.txtTask, "txtTask");
		((System.Windows.Forms.Control)(object)this.txtTask).Name = "txtTask";
		((EditorButtonControlBase)this.txtTask).ReadOnly = true;
		resources.ApplyResources(this.lblTask, "lblTask");
		this.lblTask.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTask).Name = "lblTask";
		((ControlBase)this.lblTask).WrapText = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.dtpStartDate, "dtpStartDate");
		((UltraWinEditorMaskedControlBase)this.dtpStartDate).AlwaysInEditMode = true;
		this.dtpStartDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpStartDate).Name = "dtpStartDate";
		((EditorButtonControlBase)this.dtpStartDate).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.dtpStartDate).Enter += new System.EventHandler(dtpDateTime_Enter);
		resources.ApplyResources(this.txtEntryPort, "txtEntryPort");
		((System.Windows.Forms.Control)(object)this.txtEntryPort).Name = "txtEntryPort";
		((EditorButtonControlBase)this.txtEntryPort).ReadOnly = true;
		resources.ApplyResources(this.chkCompleted, "chkCompleted");
		((System.Windows.Forms.Control)(object)this.chkCompleted).Name = "chkCompleted";
		((UltraToggleEditorBase)this.chkCompleted).CheckedChanged += new System.EventHandler(chkCompleted_CheckedChanged);
		resources.ApplyResources(this.lblStartDate, "lblStartDate");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance22");
		((ControlBase)this.lblStartDate).Appearance = (AppearanceBase)(object)val11;
		this.lblStartDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStartDate).Name = "lblStartDate";
		((ControlBase)this.lblStartDate).WrapText = false;
		resources.ApplyResources(this.lblHistory, "lblHistory");
		((System.Windows.Forms.Control)(object)this.lblHistory).Name = "lblHistory";
		((System.Windows.Forms.Control)(object)this.lblHistory).Click += new System.EventHandler(lblHistory_Click);
		resources.ApplyResources(this.lblServiceUser, "lblServiceUser");
		this.lblServiceUser.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblServiceUser).Name = "lblServiceUser";
		((ControlBase)this.lblServiceUser).WrapText = false;
		resources.ApplyResources(this.txtServiceUser, "txtServiceUser");
		((System.Windows.Forms.Control)(object)this.txtServiceUser).Name = "txtServiceUser";
		((EditorButtonControlBase)this.txtServiceUser).ReadOnly = true;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblHistory);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtServiceUser);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTask);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblServiceUser);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTask);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkForAllUsers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUser);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVoyage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVessel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtService);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboUsers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtStep);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtOperation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOperation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVessels);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEntryPort);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEntryPort);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStep);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblService);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVoyage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkCompleted);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStartDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpStartDate);
		base.Name = "frmUpdatePROTask";
		base.Load += new System.EventHandler(frmUpdatePROTask_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpStartDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStartDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkCompleted, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVoyage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblService, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStep, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEntryPort, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEntryPort, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVessels, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOperation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtOperation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtStep, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboUsers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtService, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVessel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVoyage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUser, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkForAllUsers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTask, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblServiceUser, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTask, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtServiceUser, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblHistory, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOperation).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtStep).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtService).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVessel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVoyage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkForAllUsers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboUsers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTask).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpStartDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEntryPort).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkCompleted).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtServiceUser).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
