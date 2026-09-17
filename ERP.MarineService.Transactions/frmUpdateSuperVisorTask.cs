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
using ERP.SystemOptions.GeneralData;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.MarineService.Transactions;

public class frmUpdateSuperVisorTask : frmBase
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

	private UltraCheckEditor chkCancelled;

	private UltraCheckEditor chkHold;

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

	private UltraTextEditor txtServiceUser;

	private UltraLabel lblServiceUser;

	public frmUpdateSuperVisorTask()
	{
		InitializeComponent();
	}

	public frmUpdateSuperVisorTask(string ID, bool WithOperation)
		: this()
	{
		RowID = ID;
		OperationServiceStepTaskID = ID;
		TableName = "MS_OperationsServicesStepsTasks";
		this.WithOperation = WithOperation;
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
			if (drMaster["StartDate"] != DBNull.Value)
			{
				dtpStartDate.Value = (DateTime)drMaster["StartDate"];
			}
			((Control)(object)txtVoyage).Text = drMaster["VoyageNo"].ToString();
			((Control)(object)txtVessel).Text = drMaster["VesselName"].ToString();
			((Control)(object)txtService).Text = drMaster["ServiceName"].ToString();
			((Control)(object)txtServiceUser).Text = drMaster["ServiceUser"].ToString();
			((Control)(object)txtStep).Text = drMaster["StepName"].ToString();
			((Control)(object)txtTask).Text = drMaster["TaskName"].ToString();
			((TextEditorControlBase)cboUsers).Value = drMaster["User_ID"];
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((UltraToggleEditorBase)chkCancelled).Checked = bool.Parse(drMaster["IsCancelled"].ToString());
			((UltraToggleEditorBase)chkHold).Checked = bool.Parse(drMaster["IsHold"].ToString());
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
				OperationsServicesStepsTasks.Insert_Update(RowID, drMaster["OperationServiceStepID"].ToString(), drMaster["OperationServiceID"].ToString(), drMaster["OperationID"].ToString(), (drMaster["TaskID"].ToString() == "") ? "Null" : drMaster["TaskID"].ToString(), (drMaster["TaskOrder"].ToString() == "") ? "Null" : drMaster["TaskOrder"].ToString(), (drMaster["TaskPlaceID"].ToString() == "") ? "Null" : drMaster["TaskPlaceID"].ToString(), ((UltraToggleEditorBase)chkHold).Checked ? "1" : "0", ((UltraToggleEditorBase)chkForAllUsers).Checked ? "1" : "0", (cboUsers.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUsers).Value.ToString(), (dtpStartDate.Value == null) ? "Null" : dtpStartDate.DateTime.ToString(GlobalVariables.DateLongFormate), drMaster["ExpectedTime"].ToString(), (drMaster["EndDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["EndDate"].ToString()).ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkCompleted).Checked ? "1" : "0", ((UltraToggleEditorBase)chkCancelled).Checked ? "1" : "0", ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			}
			else
			{
				TaskWithoutOperation.Insert_Update(RowID, drMaster["TaskWithoutOperationNo"].ToString(), drMaster["TaskID"].ToString(), drMaster["TaskOrder"].ToString(), drMaster["TaskPlaceID"].ToString(), ((UltraToggleEditorBase)chkHold).Checked ? "1" : "0", ((UltraToggleEditorBase)chkForAllUsers).Checked ? "1" : "0", (cboUsers.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUsers).Value.ToString(), (dtpStartDate.Value == null) ? "Null" : dtpStartDate.DateTime.ToString(GlobalVariables.DateLongFormate), drMaster["ExpectedTime"].ToString(), (drMaster["EndDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["EndDate"].ToString()).ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkCompleted).Checked ? "1" : "0", ((UltraToggleEditorBase)chkCancelled).Checked ? "1" : "0", ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
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
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Expected O, but got Unknown
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Expected O, but got Unknown
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Expected O, but got Unknown
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Expected O, but got Unknown
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Expected O, but got Unknown
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmUpdateSuperVisorTask));
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
		this.chkCancelled = new UltraCheckEditor();
		this.chkHold = new UltraCheckEditor();
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
		this.txtServiceUser = new UltraTextEditor();
		this.lblServiceUser = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOperation).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtStep).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtService).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVessel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVoyage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkCancelled).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkHold).BeginInit();
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
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val).Image = resources.GetObject("appearance11.Image");
		resources.ApplyResources(val, "appearance11");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance12");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance13.Image");
		resources.ApplyResources(val3, "appearance13");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)7;
		((AppearanceBase)val4).Image = resources.GetObject("appearance14.Image");
		resources.ApplyResources(val4, "appearance14");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblVoyage, "lblVoyage");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val5, "appearance15");
		((ControlBase)this.lblVoyage).Appearance = (AppearanceBase)(object)val5;
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
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val6, "appearance16");
		((ControlBase)this.lblEntryPort).Appearance = (AppearanceBase)(object)val6;
		this.lblEntryPort.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEntryPort).Name = "lblEntryPort";
		((ControlBase)this.lblEntryPort).WrapText = false;
		resources.ApplyResources(this.lblVessels, "lblVessels");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance17");
		((ControlBase)this.lblVessels).Appearance = (AppearanceBase)(object)val7;
		this.lblVessels.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVessels).Name = "lblVessels";
		((ControlBase)this.lblVessels).WrapText = false;
		resources.ApplyResources(this.lblService, "lblService");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance18");
		((ControlBase)this.lblService).Appearance = (AppearanceBase)(object)val8;
		this.lblService.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblService).Name = "lblService";
		((ControlBase)this.lblService).WrapText = false;
		resources.ApplyResources(this.lblStep, "lblStep");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance19");
		((ControlBase)this.lblStep).Appearance = (AppearanceBase)(object)val9;
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
		resources.ApplyResources(this.chkCancelled, "chkCancelled");
		((System.Windows.Forms.Control)(object)this.chkCancelled).Name = "chkCancelled";
		resources.ApplyResources(this.chkHold, "chkHold");
		((System.Windows.Forms.Control)(object)this.chkHold).Name = "chkHold";
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
		((System.Windows.Forms.Control)(object)this.dtpStartDate).Enter += new System.EventHandler(dtpDateTime_Enter);
		resources.ApplyResources(this.txtEntryPort, "txtEntryPort");
		((System.Windows.Forms.Control)(object)this.txtEntryPort).Name = "txtEntryPort";
		((EditorButtonControlBase)this.txtEntryPort).ReadOnly = true;
		resources.ApplyResources(this.chkCompleted, "chkCompleted");
		((System.Windows.Forms.Control)(object)this.chkCompleted).Name = "chkCompleted";
		((UltraToggleEditorBase)this.chkCompleted).CheckedChanged += new System.EventHandler(chkCompleted_CheckedChanged);
		resources.ApplyResources(this.lblStartDate, "lblStartDate");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance20");
		((ControlBase)this.lblStartDate).Appearance = (AppearanceBase)(object)val10;
		this.lblStartDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStartDate).Name = "lblStartDate";
		((ControlBase)this.lblStartDate).WrapText = false;
		resources.ApplyResources(this.lblHistory, "lblHistory");
		((System.Windows.Forms.Control)(object)this.lblHistory).Name = "lblHistory";
		((System.Windows.Forms.Control)(object)this.lblHistory).Click += new System.EventHandler(lblHistory_Click);
		resources.ApplyResources(this.txtServiceUser, "txtServiceUser");
		((System.Windows.Forms.Control)(object)this.txtServiceUser).Name = "txtServiceUser";
		((EditorButtonControlBase)this.txtServiceUser).ReadOnly = true;
		resources.ApplyResources(this.lblServiceUser, "lblServiceUser");
		this.lblServiceUser.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblServiceUser).Name = "lblServiceUser";
		((ControlBase)this.lblServiceUser).WrapText = false;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblHistory);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtOperation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOperation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTask);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkHold);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboUsers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkForAllUsers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblServiceUser);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTask);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtStep);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVoyage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUser);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtService);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtServiceUser);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEntryPort);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStep);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkCancelled);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVessel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVessels);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEntryPort);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblService);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkCompleted);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpStartDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStartDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVoyage);
		base.Name = "frmUpdateSuperVisorTask";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVoyage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStartDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpStartDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkCompleted, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblService, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEntryPort, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVessels, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVessel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkCancelled, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStep, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEntryPort, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtServiceUser, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtService, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUser, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVoyage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtStep, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTask, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblServiceUser, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkForAllUsers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboUsers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkHold, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTask, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOperation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtOperation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOperation).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtStep).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtService).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVessel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVoyage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkCancelled).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkHold).EndInit();
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
