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
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.MarineService.Transactions;

public class frmUpdateSeaManCrew : frmBase
{
	private DataTable dtUsers;

	private DataRow drMaster;

	private string OperationServiceCrewPassengerID = "";

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraButton btnSave;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	private UltraLabel lblVoyage;

	private UltraTextEditor txtOperation;

	private UltraLabel lblOperation;

	private UltraLabel lblVessels;

	private UltraTextEditor txtVessel;

	private UltraTextEditor txtVoyage;

	private UltraCheckEditor chkIsJoined;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraDateTimeEditor dtpStartDate;

	private UltraCheckEditor chkIsExit;

	private UltraLabel lblStartDate;

	private UltraTextEditor txtSeaMan;

	private UltraLabel lblSeaMan;

	private UltraLabel lblPassengerType;

	private UltraTextEditor txtPassPortNo;

	private UltraLabel lblPassPortNo;

	private UltraLabel lblIDNo;

	private UltraTextEditor txtIDNo;

	private UltraLabel lblNationality;

	private UltraTextEditor txtNationality;

	private UltraCheckEditor chkIsUnitedUpdated;

	private UltraCheckEditor chkHasEyeScan;

	private UltraCheckEditor chkHasEnterStamp;

	private UltraComboEditor cboPassengerType;

	public UltraLabel lblHistory;

	private UltraDateTimeEditor dtpExitDate;

	private UltraDateTimeEditor dtpJoinedDate;

	private UltraDateTimeEditor dtpUpdatedDate;

	private UltraDateTimeEditor dtpEnterStampDate;

	private UltraComboEditor cboEyeScanUsers;

	private UltraComboEditor cboEnterStampUsers;

	private UltraComboEditor cboJoinedUsers;

	private UltraComboEditor cboUpdatedUsers;

	private UltraComboEditor cboExitUsers;

	public frmUpdateSeaManCrew()
	{
		InitializeComponent();
	}

	public frmUpdateSeaManCrew(string OPERATIONSERVICECREWPASSENGERID, bool IsSignOn)
		: this()
	{
		RowID = OPERATIONSERVICECREWPASSENGERID;
		OperationServiceCrewPassengerID = OPERATIONSERVICECREWPASSENGERID;
		((Control)(object)chkIsJoined).Visible = (((Control)(object)cboJoinedUsers).Visible = (((Control)(object)chkIsUnitedUpdated).Visible = (((Control)(object)dtpJoinedDate).Visible = (((Control)(object)dtpUpdatedDate).Visible = (((Control)(object)cboUpdatedUsers).Visible = IsSignOn)))));
		((Control)(object)cboEyeScanUsers).Visible = (((Control)(object)chkHasEyeScan).Visible = !IsSignOn);
		TableName = "MS_OperationsServicesCrewPassengers";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpStartDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboEyeScanUsers, dtUsers, "UserID", "UserName");
		GlobalFunctions.FillCombo(cboEnterStampUsers, dtUsers, "UserID", "UserName");
		GlobalFunctions.FillCombo(cboExitUsers, dtUsers, "UserID", "UserName");
		GlobalFunctions.FillCombo(cboJoinedUsers, dtUsers, "UserID", "UserName");
		GlobalFunctions.FillCombo(cboUpdatedUsers, dtUsers, "UserID", "UserName");
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = OperationsServicesCrewPassengers.SelectByID(RowID, GlobalVariables.IsArabic ? "1" : "0");
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
			((UltraToggleEditorBase)chkIsExit).CheckedChanged -= chkIsExit_CheckedChanged;
			((UltraToggleEditorBase)chkIsJoined).CheckedChanged -= chkIsJoined_CheckedChanged;
			((UltraToggleEditorBase)chkHasEyeScan).CheckedChanged -= chkHasEyeScan_CheckedChanged;
			((UltraToggleEditorBase)chkIsUnitedUpdated).CheckedChanged -= chkIsUnitedUpdated_CheckedChanged;
			((UltraToggleEditorBase)chkHasEnterStamp).CheckedChanged -= chkHasEnterStamp_CheckedChanged;
			((Control)(object)lblHistory).Text = Trans_Log.GetRowHistory(TableName, RowID, GlobalVariables.IsArabic ? "1" : "0");
			DisplayDataDate = GlobalFunctions.GetServerDateTimeNow();
			((Control)(object)txtOperation).Text = drMaster["OperationNo"].ToString();
			dtpStartDate.Value = (DateTime)drMaster["StartDate"];
			((Control)(object)txtVoyage).Text = drMaster["VoyageNo"].ToString();
			((Control)(object)txtVessel).Text = drMaster["VesselName"].ToString();
			((Control)(object)txtSeaMan).Text = drMaster["SeaMan"].ToString();
			((Control)(object)txtIDNo).Text = drMaster["IDNo"].ToString();
			((Control)(object)txtPassPortNo).Text = drMaster["PassPortNo"].ToString();
			((Control)(object)txtNationality).Text = drMaster["NationalityName"].ToString();
			((TextEditorControlBase)cboPassengerType).Value = drMaster["PassengerType"];
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((UltraToggleEditorBase)chkIsExit).Checked = bool.Parse(drMaster["IsExit"].ToString());
			((UltraToggleEditorBase)chkIsJoined).Checked = bool.Parse(drMaster["IsJoined"].ToString());
			((UltraToggleEditorBase)chkHasEyeScan).Checked = bool.Parse(drMaster["HasEyesScan"].ToString());
			((UltraToggleEditorBase)chkIsUnitedUpdated).Checked = bool.Parse(drMaster["IsUnitedUpdate"].ToString());
			((UltraToggleEditorBase)chkHasEnterStamp).Checked = bool.Parse(drMaster["HasEnterStamp"].ToString());
			((Control)(object)chkIsExit).Enabled = !((UltraToggleEditorBase)chkIsExit).Checked;
			((Control)(object)chkIsJoined).Enabled = !((UltraToggleEditorBase)chkIsJoined).Checked;
			((Control)(object)chkHasEyeScan).Enabled = !((UltraToggleEditorBase)chkHasEyeScan).Checked;
			((Control)(object)chkIsUnitedUpdated).Enabled = !((UltraToggleEditorBase)chkIsUnitedUpdated).Checked;
			((Control)(object)chkHasEnterStamp).Enabled = !((UltraToggleEditorBase)chkHasEnterStamp).Checked;
			dtpExitDate.Value = drMaster["ExitDate"];
			dtpJoinedDate.Value = drMaster["JoinedDate"];
			dtpUpdatedDate.Value = drMaster["UnitedUpdateDate"];
			dtpEnterStampDate.Value = drMaster["EnterStampDate"];
			((TextEditorControlBase)cboEyeScanUsers).Value = drMaster["EyesScanUserID"];
			((TextEditorControlBase)cboExitUsers).Value = drMaster["ExitUserID"];
			((TextEditorControlBase)cboJoinedUsers).Value = drMaster["JoinedUserID"];
			((TextEditorControlBase)cboUpdatedUsers).Value = drMaster["UnitedUpdateUserID"];
			((TextEditorControlBase)cboEnterStampUsers).Value = drMaster["EnterStampUserID"];
			((UltraToggleEditorBase)chkIsExit).CheckedChanged += chkIsExit_CheckedChanged;
			((UltraToggleEditorBase)chkIsJoined).CheckedChanged += chkIsJoined_CheckedChanged;
			((UltraToggleEditorBase)chkHasEyeScan).CheckedChanged += chkHasEyeScan_CheckedChanged;
			((UltraToggleEditorBase)chkIsUnitedUpdated).CheckedChanged += chkIsUnitedUpdated_CheckedChanged;
			((UltraToggleEditorBase)chkHasEnterStamp).CheckedChanged += chkHasEnterStamp_CheckedChanged;
		}
	}

	public bool ValidateData()
	{
		if (((UltraToggleEditorBase)chkIsExit).Checked && dtpExitDate.Value == null)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ الخروج" : "Please Enter Exit Date");
			((Control)(object)dtpExitDate).Focus();
			dtpExitDate.DropDown();
			return false;
		}
		if (((UltraToggleEditorBase)chkIsUnitedUpdated).Checked && dtpUpdatedDate.Value == null)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ التحديث على النظام" : "Please Select Updated On System Date");
			((Control)(object)dtpUpdatedDate).Focus();
			dtpUpdatedDate.DropDown();
			return false;
		}
		if (((UltraToggleEditorBase)chkIsJoined).Checked && dtpJoinedDate.Value == null)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ الالحاق" : "Please Select Joined Date");
			((Control)(object)dtpJoinedDate).Focus();
			dtpJoinedDate.DropDown();
			return false;
		}
		if (((UltraToggleEditorBase)chkHasEnterStamp).Checked && dtpEnterStampDate.Value == null)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ ختم الدخول" : "Please Select Enter Stamp date ");
			((Control)(object)dtpEnterStampDate).Focus();
			dtpEnterStampDate.DropDown();
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
			OperationsServicesCrewPassengers.Insert_Update(RowID, drMaster["OperationServiceID"].ToString(), drMaster["OperationID"].ToString(), (drMaster["OperationServiceVisaID"].ToString() == "") ? "Null" : drMaster["OperationServiceVisaID"].ToString(), drMaster["SubAccountID"].ToString(), drMaster["PAssengerType"].ToString(), drMaster["IsSignOn"].ToString(), ((UltraToggleEditorBase)chkIsExit).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsExit).Checked ? GlobalVariables.UserID : "Null", (dtpExitDate.Value == null) ? "Null" : dtpExitDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkIsJoined).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsJoined).Checked ? GlobalVariables.UserID : "Null", (dtpJoinedDate.Value == null) ? "Null" : dtpJoinedDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkIsUnitedUpdated).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsUnitedUpdated).Checked ? GlobalVariables.UserID : "Null", (dtpUpdatedDate.Value == null) ? "Null" : dtpUpdatedDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkHasEyeScan).Checked ? "1" : "0", ((UltraToggleEditorBase)chkHasEyeScan).Checked ? GlobalVariables.UserID : "Null", ((UltraToggleEditorBase)chkHasEnterStamp).Checked ? "1" : "0", ((UltraToggleEditorBase)chkHasEnterStamp).Checked ? GlobalVariables.UserID : "Null", (dtpEnterStampDate.Value == null) ? "Null" : dtpEnterStampDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
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

	private void chkHasEnterStamp_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)dtpEnterStampDate).Enabled = ((Control)(object)chkHasEnterStamp).Enabled && ((UltraToggleEditorBase)chkHasEnterStamp).Checked;
		if (((UltraToggleEditorBase)chkHasEnterStamp).Checked)
		{
			dtpEnterStampDate.Value = DateTime.Now;
			((TextEditorControlBase)cboEnterStampUsers).Value = GlobalVariables.UserID;
		}
		else
		{
			((TextEditorControlBase)cboEnterStampUsers).Value = null;
			dtpEnterStampDate.Value = null;
		}
	}

	private void chkIsExit_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)dtpExitDate).Enabled = ((Control)(object)chkIsExit).Enabled && ((UltraToggleEditorBase)chkIsExit).Checked;
		if (((UltraToggleEditorBase)chkIsExit).Checked)
		{
			dtpExitDate.Value = DateTime.Now;
			((TextEditorControlBase)cboExitUsers).Value = GlobalVariables.UserID;
		}
		else
		{
			dtpExitDate.Value = null;
			((TextEditorControlBase)cboExitUsers).Value = null;
		}
	}

	private void chkIsJoined_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)dtpJoinedDate).Enabled = ((Control)(object)chkIsJoined).Enabled && ((UltraToggleEditorBase)chkIsJoined).Checked;
		if (((UltraToggleEditorBase)chkIsJoined).Checked)
		{
			dtpJoinedDate.Value = DateTime.Now;
			((TextEditorControlBase)cboJoinedUsers).Value = GlobalVariables.UserID;
		}
		else
		{
			dtpJoinedDate.Value = null;
			((TextEditorControlBase)cboJoinedUsers).Value = null;
		}
	}

	private void chkIsUnitedUpdated_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)dtpUpdatedDate).Enabled = ((Control)(object)chkIsUnitedUpdated).Enabled && ((UltraToggleEditorBase)chkIsUnitedUpdated).Checked;
		if (((UltraToggleEditorBase)chkIsUnitedUpdated).Checked)
		{
			dtpUpdatedDate.Value = DateTime.Now;
			((TextEditorControlBase)cboUpdatedUsers).Value = GlobalVariables.UserID;
		}
		else
		{
			dtpUpdatedDate.Value = null;
			((TextEditorControlBase)cboUpdatedUsers).Value = null;
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

	private void chkHasEyeScan_CheckedChanged(object sender, EventArgs e)
	{
		if (((UltraToggleEditorBase)chkHasEyeScan).Checked)
		{
			((TextEditorControlBase)cboEyeScanUsers).Value = GlobalVariables.UserID;
		}
		else
		{
			((TextEditorControlBase)cboEyeScanUsers).Value = null;
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
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected O, but got Unknown
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected O, but got Unknown
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Expected O, but got Unknown
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Expected O, but got Unknown
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Expected O, but got Unknown
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Expected O, but got Unknown
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Expected O, but got Unknown
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Expected O, but got Unknown
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Expected O, but got Unknown
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Expected O, but got Unknown
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Expected O, but got Unknown
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Expected O, but got Unknown
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Expected O, but got Unknown
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Expected O, but got Unknown
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Expected O, but got Unknown
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Expected O, but got Unknown
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Expected O, but got Unknown
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Expected O, but got Unknown
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Expected O, but got Unknown
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Expected O, but got Unknown
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Expected O, but got Unknown
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Expected O, but got Unknown
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Expected O, but got Unknown
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Expected O, but got Unknown
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Expected O, but got Unknown
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Expected O, but got Unknown
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Expected O, but got Unknown
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Expected O, but got Unknown
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Expected O, but got Unknown
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Expected O, but got Unknown
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Expected O, but got Unknown
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmUpdateSeaManCrew));
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
		ValueListItem val14 = new ValueListItem();
		ValueListItem val15 = new ValueListItem();
		ValueListItem val16 = new ValueListItem();
		this.btnKeyboard = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.lblVoyage = new UltraLabel();
		this.txtOperation = new UltraTextEditor();
		this.lblOperation = new UltraLabel();
		this.lblVessels = new UltraLabel();
		this.txtVessel = new UltraTextEditor();
		this.txtVoyage = new UltraTextEditor();
		this.chkIsJoined = new UltraCheckEditor();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.dtpStartDate = new UltraDateTimeEditor();
		this.chkIsExit = new UltraCheckEditor();
		this.lblStartDate = new UltraLabel();
		this.txtSeaMan = new UltraTextEditor();
		this.lblSeaMan = new UltraLabel();
		this.lblPassengerType = new UltraLabel();
		this.txtPassPortNo = new UltraTextEditor();
		this.lblPassPortNo = new UltraLabel();
		this.lblIDNo = new UltraLabel();
		this.txtIDNo = new UltraTextEditor();
		this.lblNationality = new UltraLabel();
		this.txtNationality = new UltraTextEditor();
		this.chkIsUnitedUpdated = new UltraCheckEditor();
		this.chkHasEyeScan = new UltraCheckEditor();
		this.chkHasEnterStamp = new UltraCheckEditor();
		this.cboPassengerType = new UltraComboEditor();
		this.lblHistory = new UltraLabel();
		this.dtpExitDate = new UltraDateTimeEditor();
		this.dtpJoinedDate = new UltraDateTimeEditor();
		this.dtpUpdatedDate = new UltraDateTimeEditor();
		this.dtpEnterStampDate = new UltraDateTimeEditor();
		this.cboEyeScanUsers = new UltraComboEditor();
		this.cboEnterStampUsers = new UltraComboEditor();
		this.cboJoinedUsers = new UltraComboEditor();
		this.cboUpdatedUsers = new UltraComboEditor();
		this.cboExitUsers = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOperation).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVessel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVoyage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsJoined).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpStartDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsExit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSeaMan).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassPortNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtIDNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNationality).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsUnitedUpdated).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkHasEyeScan).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkHasEnterStamp).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPassengerType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpExitDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpJoinedDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpUpdatedDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpEnterStampDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboEyeScanUsers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboEnterStampUsers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboJoinedUsers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboUpdatedUsers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboExitUsers).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val, "appearance14");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val2).Image = resources.GetObject("appearance15.Image");
		resources.ApplyResources(val2, "appearance15");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val3).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val3, "appearance16");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val4).Image = resources.GetObject("appearance17.Image");
		resources.ApplyResources(val4, "appearance17");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val4;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val5, "appearance18");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblVoyage, "lblVoyage");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val6, "appearance19");
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
		resources.ApplyResources(this.lblVessels, "lblVessels");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance20");
		((ControlBase)this.lblVessels).Appearance = (AppearanceBase)(object)val7;
		this.lblVessels.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVessels).Name = "lblVessels";
		((ControlBase)this.lblVessels).WrapText = false;
		resources.ApplyResources(this.txtVessel, "txtVessel");
		((System.Windows.Forms.Control)(object)this.txtVessel).Name = "txtVessel";
		((EditorButtonControlBase)this.txtVessel).ReadOnly = true;
		resources.ApplyResources(this.txtVoyage, "txtVoyage");
		((System.Windows.Forms.Control)(object)this.txtVoyage).Name = "txtVoyage";
		((EditorButtonControlBase)this.txtVoyage).ReadOnly = true;
		resources.ApplyResources(this.chkIsJoined, "chkIsJoined");
		((System.Windows.Forms.Control)(object)this.chkIsJoined).Name = "chkIsJoined";
		((UltraToggleEditorBase)this.chkIsJoined).CheckedChanged += new System.EventHandler(chkIsJoined_CheckedChanged);
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
		resources.ApplyResources(this.chkIsExit, "chkIsExit");
		((System.Windows.Forms.Control)(object)this.chkIsExit).Name = "chkIsExit";
		((UltraToggleEditorBase)this.chkIsExit).CheckedChanged += new System.EventHandler(chkIsExit_CheckedChanged);
		resources.ApplyResources(this.lblStartDate, "lblStartDate");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance21");
		((ControlBase)this.lblStartDate).Appearance = (AppearanceBase)(object)val8;
		this.lblStartDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStartDate).Name = "lblStartDate";
		((ControlBase)this.lblStartDate).WrapText = false;
		resources.ApplyResources(this.txtSeaMan, "txtSeaMan");
		((System.Windows.Forms.Control)(object)this.txtSeaMan).Name = "txtSeaMan";
		((EditorButtonControlBase)this.txtSeaMan).ReadOnly = true;
		resources.ApplyResources(this.lblSeaMan, "lblSeaMan");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance22");
		((ControlBase)this.lblSeaMan).Appearance = (AppearanceBase)(object)val9;
		this.lblSeaMan.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSeaMan).Name = "lblSeaMan";
		((ControlBase)this.lblSeaMan).WrapText = false;
		resources.ApplyResources(this.lblPassengerType, "lblPassengerType");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance23");
		((ControlBase)this.lblPassengerType).Appearance = (AppearanceBase)(object)val10;
		this.lblPassengerType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPassengerType).Name = "lblPassengerType";
		((ControlBase)this.lblPassengerType).WrapText = false;
		resources.ApplyResources(this.txtPassPortNo, "txtPassPortNo");
		((System.Windows.Forms.Control)(object)this.txtPassPortNo).Name = "txtPassPortNo";
		((EditorButtonControlBase)this.txtPassPortNo).ReadOnly = true;
		resources.ApplyResources(this.lblPassPortNo, "lblPassPortNo");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance24");
		((ControlBase)this.lblPassPortNo).Appearance = (AppearanceBase)(object)val11;
		this.lblPassPortNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPassPortNo).Name = "lblPassPortNo";
		((ControlBase)this.lblPassPortNo).WrapText = false;
		resources.ApplyResources(this.lblIDNo, "lblIDNo");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance25");
		((ControlBase)this.lblIDNo).Appearance = (AppearanceBase)(object)val12;
		this.lblIDNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblIDNo).Name = "lblIDNo";
		((ControlBase)this.lblIDNo).WrapText = false;
		resources.ApplyResources(this.txtIDNo, "txtIDNo");
		((System.Windows.Forms.Control)(object)this.txtIDNo).Name = "txtIDNo";
		((EditorButtonControlBase)this.txtIDNo).ReadOnly = true;
		resources.ApplyResources(this.lblNationality, "lblNationality");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance26");
		((ControlBase)this.lblNationality).Appearance = (AppearanceBase)(object)val13;
		this.lblNationality.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNationality).Name = "lblNationality";
		((ControlBase)this.lblNationality).WrapText = false;
		resources.ApplyResources(this.txtNationality, "txtNationality");
		((System.Windows.Forms.Control)(object)this.txtNationality).Name = "txtNationality";
		((EditorButtonControlBase)this.txtNationality).ReadOnly = true;
		resources.ApplyResources(this.chkIsUnitedUpdated, "chkIsUnitedUpdated");
		((System.Windows.Forms.Control)(object)this.chkIsUnitedUpdated).Name = "chkIsUnitedUpdated";
		((UltraToggleEditorBase)this.chkIsUnitedUpdated).CheckedChanged += new System.EventHandler(chkIsUnitedUpdated_CheckedChanged);
		resources.ApplyResources(this.chkHasEyeScan, "chkHasEyeScan");
		((System.Windows.Forms.Control)(object)this.chkHasEyeScan).Name = "chkHasEyeScan";
		((UltraToggleEditorBase)this.chkHasEyeScan).CheckedChanged += new System.EventHandler(chkHasEyeScan_CheckedChanged);
		resources.ApplyResources(this.chkHasEnterStamp, "chkHasEnterStamp");
		((System.Windows.Forms.Control)(object)this.chkHasEnterStamp).Name = "chkHasEnterStamp";
		((UltraToggleEditorBase)this.chkHasEnterStamp).CheckedChanged += new System.EventHandler(chkHasEnterStamp_CheckedChanged);
		resources.ApplyResources(this.cboPassengerType, "cboPassengerType");
		this.cboPassengerType.AutoCompleteMode = (AutoCompleteMode)4;
		val14.DataValue = "1";
		resources.ApplyResources(val14, "valueListItem1");
		((SubObjectBase)val14).ForceApplyResources = "";
		val15.DataValue = "2";
		resources.ApplyResources(val15, "valueListItem2");
		((SubObjectBase)val15).ForceApplyResources = "";
		val16.DataValue = "3";
		resources.ApplyResources(val16, "valueListItem3");
		((SubObjectBase)val16).ForceApplyResources = "";
		this.cboPassengerType.Items.AddRange((ValueListItem[])(object)new ValueListItem[3] { val14, val15, val16 });
		((System.Windows.Forms.Control)(object)this.cboPassengerType).Name = "cboPassengerType";
		((EditorButtonControlBase)this.cboPassengerType).ReadOnly = true;
		resources.ApplyResources(this.lblHistory, "lblHistory");
		((System.Windows.Forms.Control)(object)this.lblHistory).Name = "lblHistory";
		((System.Windows.Forms.Control)(object)this.lblHistory).Click += new System.EventHandler(lblHistory_Click);
		resources.ApplyResources(this.dtpExitDate, "dtpExitDate");
		((UltraWinEditorMaskedControlBase)this.dtpExitDate).AlwaysInEditMode = true;
		this.dtpExitDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpExitDate).Name = "dtpExitDate";
		resources.ApplyResources(this.dtpJoinedDate, "dtpJoinedDate");
		((UltraWinEditorMaskedControlBase)this.dtpJoinedDate).AlwaysInEditMode = true;
		this.dtpJoinedDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpJoinedDate).Name = "dtpJoinedDate";
		resources.ApplyResources(this.dtpUpdatedDate, "dtpUpdatedDate");
		((UltraWinEditorMaskedControlBase)this.dtpUpdatedDate).AlwaysInEditMode = true;
		this.dtpUpdatedDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpUpdatedDate).Name = "dtpUpdatedDate";
		resources.ApplyResources(this.dtpEnterStampDate, "dtpEnterStampDate");
		((UltraWinEditorMaskedControlBase)this.dtpEnterStampDate).AlwaysInEditMode = true;
		this.dtpEnterStampDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpEnterStampDate).Name = "dtpEnterStampDate";
		resources.ApplyResources(this.cboEyeScanUsers, "cboEyeScanUsers");
		this.cboEyeScanUsers.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboEyeScanUsers).Name = "cboEyeScanUsers";
		((EditorButtonControlBase)this.cboEyeScanUsers).ReadOnly = true;
		resources.ApplyResources(this.cboEnterStampUsers, "cboEnterStampUsers");
		this.cboEnterStampUsers.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboEnterStampUsers).Name = "cboEnterStampUsers";
		((EditorButtonControlBase)this.cboEnterStampUsers).ReadOnly = true;
		resources.ApplyResources(this.cboJoinedUsers, "cboJoinedUsers");
		this.cboJoinedUsers.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboJoinedUsers).Name = "cboJoinedUsers";
		((EditorButtonControlBase)this.cboJoinedUsers).ReadOnly = true;
		resources.ApplyResources(this.cboUpdatedUsers, "cboUpdatedUsers");
		this.cboUpdatedUsers.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboUpdatedUsers).Name = "cboUpdatedUsers";
		((EditorButtonControlBase)this.cboUpdatedUsers).ReadOnly = true;
		resources.ApplyResources(this.cboExitUsers, "cboExitUsers");
		this.cboExitUsers.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboExitUsers).Name = "cboExitUsers";
		((EditorButtonControlBase)this.cboExitUsers).ReadOnly = true;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboEnterStampUsers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboEyeScanUsers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpEnterStampDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpUpdatedDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboJoinedUsers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpJoinedDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpExitDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboUpdatedUsers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboExitUsers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblHistory);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPassengerType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtOperation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOperation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVoyage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVessel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPassPortNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPassPortNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkHasEnterStamp);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNationality);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNationality);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtIDNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsJoined);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblIDNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPassengerType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVessels);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVoyage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpStartDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsExit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStartDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSeaMan);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSeaMan);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsUnitedUpdated);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkHasEyeScan);
		base.Name = "frmUpdateSeaManCrew";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkHasEyeScan, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsUnitedUpdated, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSeaMan, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSeaMan, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStartDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsExit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpStartDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVoyage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVessels, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPassengerType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblIDNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsJoined, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtIDNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNationality, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNationality, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkHasEnterStamp, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPassPortNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPassPortNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVessel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVoyage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOperation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtOperation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPassengerType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboExitUsers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboUpdatedUsers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpExitDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpJoinedDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboJoinedUsers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpUpdatedDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpEnterStampDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboEyeScanUsers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboEnterStampUsers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOperation).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVessel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVoyage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsJoined).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpStartDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsExit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSeaMan).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassPortNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtIDNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNationality).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsUnitedUpdated).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkHasEyeScan).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkHasEnterStamp).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPassengerType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpExitDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpJoinedDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpUpdatedDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpEnterStampDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboEyeScanUsers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboEnterStampUsers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboJoinedUsers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboUpdatedUsers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboExitUsers).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
