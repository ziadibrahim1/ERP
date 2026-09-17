using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.MarineService;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.MarineService.Transactions;

public class frmInsertSeaManCrew : frmBase
{
	private DataTable dtNationality;

	private DataTable dtSubAccountTypes;

	private DataTable dtSubAccountGroups;

	private string SubAccountID = "-1";

	private string OperationServiceID = "";

	private string OperationID = "";

	private bool IsSignOn = false;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraButton btnSave;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	private UltraLabel lblPassportNo;

	private UltraTextEditor txtPassportNo;

	public UltraGroupBox UGBDetails;

	private UltraTextEditor txtNo;

	private UltraLabel ultraLabel1;

	private UltraLabel lblCDCExpireDate;

	private UltraDateTimeEditor dtpCDCExpireDate;

	private UltraLabel lblCDCNo;

	private UltraTextEditor txtCDCNo;

	private UltraLabel lblPassportExpireDate;

	private UltraDateTimeEditor dtpPassportExpireDate;

	private UltraLabel lblNameAr;

	private UltraTextEditor txtNameAr;

	private UltraLabel lblNameEn;

	private UltraTextEditor txtNameEn;

	public UltraGroupBox UGBSignData;

	private UltraComboEditor cboSubAccountGroup;

	private UltraLabel ultraLabel2;

	private UltraComboEditor cboSubAccountType;

	private UltraLabel lblType;

	private UltraLabel lblIDNo;

	private UltraTextEditor txtIDNo;

	private UltraComboEditor cboNationality;

	private UltraLabel lblNationality;

	private UltraDateTimeEditor dtpIDIssueDate;

	private UltraLabel lblIDIssueDate;

	private UltraTextEditor txtNotes;

	private UltraLabel lblNotes;

	private UltraComboEditor cboPassengerType;

	private UltraLabel lblPassengerType;

	public frmInsertSeaManCrew()
	{
		InitializeComponent();
	}

	public frmInsertSeaManCrew(string OPERATIONID, string OPERATIONSERVICEID, bool ISSIGNON)
		: this()
	{
		OperationServiceID = OPERATIONSERVICEID;
		OperationID = OPERATIONID;
		IsSignOn = ISSIGNON;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtNationality = Nationalities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboNationality, dtNationality, "NationalityID", "NationalityName");
		dtSubAccountTypes = SuAccountsTypes.SelectBySuAccountsTypeIDs(GlobalVariables.SeaManSubAccountTypeIDs + GlobalVariables.CaptainSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0");
		GlobalFunctions.FillCombo(cboSubAccountType, dtSubAccountTypes, "SubAccountTypeID", GlobalVariables.IsArabic ? "SubAccountTypeNameAr" : "SubAccountTypeNameEn");
		dtSubAccountGroups = SubAccounts.GroupsFillComboForMarineService(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSubAccountGroup, dtSubAccountGroups, "SubAccountID", "SubAccountName");
		cboPassengerType.SelectedIndex = 0;
	}

	public virtual string GetCode()
	{
		if (cboSubAccountGroup.SelectedIndex > -1)
		{
			string text = "";
			string text2 = dtSubAccountGroups.Select("SubAccountID =  " + ((TextEditorControlBase)cboSubAccountGroup).Value.ToString())[0]["SubAccountNumber"].ToString();
			return text2 + ((Control)(object)txtNo).Text;
		}
		return ((Control)(object)txtNo).Text;
	}

	public bool ValidateData()
	{
		if (SubAccountID == "-1")
		{
			if (cboSubAccountType.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال النوع", "Please Select Account Type");
				((TextEditorControlBase)cboSubAccountType).Focus();
				return false;
			}
			if (cboSubAccountGroup.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال المجموعة", "Please Select Group");
				((TextEditorControlBase)cboSubAccountGroup).Focus();
				return false;
			}
			if (((Control)(object)txtNo).Text.Trim() == "")
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال رقم الحساب", "Please Enter The Account Number");
				((TextEditorControlBase)txtNo).Focus();
				return false;
			}
			if (((Control)(object)txtNameAr).Text.Trim() == "")
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال الاسم بالعربية", "Please Enter The Client Arabic Name");
				((TextEditorControlBase)txtNameAr).Focus();
				return false;
			}
			if (((Control)(object)txtPassportNo).Text.Trim() == "")
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال رقم الجواز", "Please Enter The Passport Number");
				((TextEditorControlBase)txtPassportNo).Focus();
				return false;
			}
		}
		if (cboPassengerType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال نوع الراكب", "Please Select Passenger Type");
			((TextEditorControlBase)cboPassengerType).Focus();
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
		try
		{
			if (SubAccountID == "-1")
			{
				Main.StartBulkTrans(FromServer: true);
				SubAccountID = SubAccounts.Insert_Update("-1", GetCode(), ((Control)(object)txtNameAr).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? "Null" : ((Control)(object)txtNameEn).Text.Trim(), (cboSubAccountGroup.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccountGroup).Value.ToString(), "0", (int.Parse(dtSubAccountGroups.Select("SubAccountID =  " + ((TextEditorControlBase)cboSubAccountGroup).Value.ToString())[0]["LevelID"].ToString()) + 1).ToString(), ((TextEditorControlBase)cboSubAccountType).Value.ToString(), "1", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true).ToString();
				SubAccounts_Details.Insert_UpdateByAccIDs(SubAccountID, "," + dtSubAccountGroups.Select("SubAccountID =  " + ((TextEditorControlBase)cboSubAccountGroup).Value.ToString())[0]["DefaultAccountID"].ToString() + ",", "0", GlobalVariables.CurrentBranchID, IsFromServer: true);
				if ("," + ((TextEditorControlBase)cboSubAccountType).Value.ToString() + "," == GlobalVariables.SeaManSubAccountTypeIDs)
				{
					SeaMen.Insert_Update("-1", (((Control)(object)txtNo).Text == "") ? GetCode() : ((Control)(object)txtNo).Text, SubAccountID, "Null", "Null", "Null", "Null", "Null", (cboNationality.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboNationality).Value.ToString(), ((Control)(object)txtPassportNo).Text, "Null", dtpPassportExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtCDCNo).Text, "Null", dtpCDCExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtIDNo).Text, dtpIDIssueDate.DateTime.ToString(GlobalVariables.DateShortFormate), "Null", "Null", "Null", "Null", "Null", "Null", dtSubAccountGroups.Select("SubAccountID =  " + ((TextEditorControlBase)cboSubAccountGroup).Value.ToString())[0]["DefaultAccountID"].ToString(), "Null", "Null", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				}
				else if ("," + ((TextEditorControlBase)cboSubAccountType).Value.ToString() + "," == GlobalVariables.CaptainSubAccountTypeIDs)
				{
					Captains.Insert_Update("-1", (((Control)(object)txtNo).Text == "") ? GetCode() : ((Control)(object)txtNo).Text, SubAccountID, "Null", "Null", "Null", "Null", "Null", (cboNationality.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboNationality).Value.ToString(), ((Control)(object)txtPassportNo).Text, "Null", dtpPassportExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtCDCNo).Text, "Null", dtpCDCExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtIDNo).Text, dtpIDIssueDate.DateTime.ToString(GlobalVariables.DateShortFormate), "Null", "Null", "Null", "Null", "Null", "Null", dtSubAccountGroups.Select("SubAccountID =  " + ((TextEditorControlBase)cboSubAccountGroup).Value.ToString())[0]["DefaultAccountID"].ToString(), "Null", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				}
				Main.EndBulkTrans(FromServer: true);
			}
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			return;
		}
		Main.StartBulkTrans(FromServer: false);
		try
		{
			OperationsServicesCrewPassengers.Insert_Update("-1", (OperationServiceID == "") ? "Null" : OperationServiceID, (OperationID == "") ? "Null" : OperationID, "Null", SubAccountID, (cboPassengerType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPassengerType).Value.ToString(), IsSignOn ? "1" : "0", "0", "Null", "Null", "0", "Null", "Null", "0", "Null", "Null", "0", "Null", "0", "Null", "Null", ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
			ClearAllControls();
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

	public void DisplayData(DataRow drMaster)
	{
		((TextEditorControlBase)cboSubAccountType).Value = drMaster["SubAccountTypeID"];
		((TextEditorControlBase)cboSubAccountGroup).Value = drMaster["ParentID"];
		((Control)(object)txtNo).Text = drMaster["SeaManNo"].ToString();
		((Control)(object)txtNameAr).Text = drMaster["SubAccountNameAr"].ToString();
		((Control)(object)txtNameEn).Text = drMaster["SubAccountNameEn"].ToString();
		((Control)(object)txtPassportNo).Text = drMaster["PassportNo"].ToString();
		((Control)(object)txtCDCNo).Text = drMaster["CDCNo"].ToString();
		((Control)(object)txtIDNo).Text = drMaster["IDNo"].ToString();
		dtpPassportExpireDate.Value = drMaster["PassportExpireDate"];
		dtpCDCExpireDate.Value = drMaster["CDCExpireDate"];
		dtpIDIssueDate.Value = drMaster["IDIssueDate"];
		((TextEditorControlBase)cboNationality).Value = drMaster["NationalityID"];
	}

	public void ClearSubAccountControls()
	{
		cboSubAccountType.SelectedIndex = -1;
		cboNationality.SelectedIndex = -1;
		dtpPassportExpireDate.DateTime = DateTime.Today;
		dtpCDCExpireDate.DateTime = DateTime.Today;
		dtpIDIssueDate.DateTime = DateTime.Today;
		((TextEditorControlBase)txtNameAr).Clear();
		((TextEditorControlBase)txtNameEn).Clear();
		((TextEditorControlBase)txtCDCNo).Clear();
		((TextEditorControlBase)txtIDNo).Clear();
		((TextEditorControlBase)txtNo).Clear();
	}

	public void ClearAllControls()
	{
		ClearSubAccountControls();
		cboPassengerType.SelectedIndex = 0;
		((TextEditorControlBase)txtNotes).Clear();
		SubAccountID = "-1";
		((TextEditorControlBase)txtPassportNo).Clear();
	}

	private void cboSubAccountType_ValueChanged(object sender, EventArgs e)
	{
		if (cboSubAccountType.SelectedIndex == -1)
		{
			((TextEditorControlBase)cboSubAccountGroup).Clear();
			cboSubAccountGroup.Items.Clear();
			return;
		}
		DataView dataView = new DataView(dtSubAccountGroups);
		dataView.RowFilter = " SubAccountTypeID = " + ((TextEditorControlBase)cboSubAccountType).Value.ToString();
		GlobalFunctions.FillCombo(cboSubAccountGroup, dataView.ToTable(), "SubAccountID", "SubAccountName");
		cboSubAccountGroup.SelectedIndex = ((DisposableObjectCollectionBase)cboSubAccountGroup.Items).Count - 1;
	}

	private void cboSubAccountGroup_ValueChanged(object sender, EventArgs e)
	{
		((Control)(object)txtNo).Text = SubAccounts.GetCode((cboSubAccountGroup.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccountGroup).Value.ToString(), IsFromServer: true);
	}

	private void CheckForSubAccount()
	{
		DataTable dataTable = SubAccounts.SelectByPassportNumber(((Control)(object)txtPassportNo).Text, IsFromServer: true);
		if (dataTable.Rows.Count > 0)
		{
			SubAccountID = dataTable.Rows[0]["SubAccountID"].ToString();
			DataRow drMaster = dataTable.Rows[0];
			dtSearchResult = null;
			DisplayData(drMaster);
		}
		else
		{
			SubAccountID = "-1";
			ClearSubAccountControls();
		}
	}

	private void txtPassport_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && ((Control)(object)txtPassportNo).Text != "")
		{
			CheckForSubAccount();
		}
	}

	private void txtPassportNo_Leave(object sender, EventArgs e)
	{
		if (SubAccountID == "-1")
		{
			CheckForSubAccount();
		}
	}

	private void dtpDateTime_Enter(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		((UltraWinEditorMaskedControlBase)(UltraDateTimeEditor)sender).SelectAll();
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
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected O, but got Unknown
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected O, but got Unknown
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Expected O, but got Unknown
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Expected O, but got Unknown
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Expected O, but got Unknown
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Expected O, but got Unknown
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Expected O, but got Unknown
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Expected O, but got Unknown
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Expected O, but got Unknown
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Expected O, but got Unknown
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Expected O, but got Unknown
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Expected O, but got Unknown
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Expected O, but got Unknown
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Expected O, but got Unknown
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Expected O, but got Unknown
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Expected O, but got Unknown
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Expected O, but got Unknown
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Expected O, but got Unknown
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Expected O, but got Unknown
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Expected O, but got Unknown
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Expected O, but got Unknown
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Expected O, but got Unknown
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmInsertSeaManCrew));
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
		ValueListItem val16 = new ValueListItem();
		ValueListItem val17 = new ValueListItem();
		ValueListItem val18 = new ValueListItem();
		Appearance val19 = new Appearance();
		this.btnKeyboard = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.lblPassportNo = new UltraLabel();
		this.txtPassportNo = new UltraTextEditor();
		this.UGBDetails = new UltraGroupBox();
		this.cboNationality = new UltraComboEditor();
		this.lblNationality = new UltraLabel();
		this.lblIDNo = new UltraLabel();
		this.txtIDNo = new UltraTextEditor();
		this.cboSubAccountGroup = new UltraComboEditor();
		this.ultraLabel2 = new UltraLabel();
		this.cboSubAccountType = new UltraComboEditor();
		this.lblType = new UltraLabel();
		this.lblCDCExpireDate = new UltraLabel();
		this.lblNameAr = new UltraLabel();
		this.dtpIDIssueDate = new UltraDateTimeEditor();
		this.dtpCDCExpireDate = new UltraDateTimeEditor();
		this.txtNameAr = new UltraTextEditor();
		this.lblNameEn = new UltraLabel();
		this.txtNameEn = new UltraTextEditor();
		this.lblIDIssueDate = new UltraLabel();
		this.lblCDCNo = new UltraLabel();
		this.txtNo = new UltraTextEditor();
		this.ultraLabel1 = new UltraLabel();
		this.txtCDCNo = new UltraTextEditor();
		this.dtpPassportExpireDate = new UltraDateTimeEditor();
		this.lblPassportExpireDate = new UltraLabel();
		this.UGBSignData = new UltraGroupBox();
		this.cboPassengerType = new UltraComboEditor();
		this.lblPassengerType = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblNotes = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassportNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboNationality).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtIDNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccountGroup).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccountType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpIDIssueDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCDCExpireDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCDCNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpPassportExpireDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBSignData).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBSignData).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboPassengerType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
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
		resources.ApplyResources(this.lblPassportNo, "lblPassportNo");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.lblPassportNo).Appearance = (AppearanceBase)(object)val6;
		this.lblPassportNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPassportNo).Name = "lblPassportNo";
		((ControlBase)this.lblPassportNo).WrapText = false;
		resources.ApplyResources(this.txtPassportNo, "txtPassportNo");
		((System.Windows.Forms.Control)(object)this.txtPassportNo).Name = "txtPassportNo";
		((System.Windows.Forms.Control)(object)this.txtPassportNo).KeyUp += new System.Windows.Forms.KeyEventHandler(txtPassport_KeyUp);
		((System.Windows.Forms.Control)(object)this.txtPassportNo).Leave += new System.EventHandler(txtPassportNo_Leave);
		resources.ApplyResources(this.UGBDetails, "UGBDetails");
		this.UGBDetails.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.cboNationality);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblNationality);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblIDNo);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtIDNo);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.cboSubAccountGroup);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.cboSubAccountType);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblType);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblCDCExpireDate);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblNameAr);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.dtpIDIssueDate);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.dtpCDCExpireDate);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtNameAr);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblNameEn);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtNameEn);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblIDIssueDate);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblCDCNo);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtNo);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtCDCNo);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.dtpPassportExpireDate);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblPassportExpireDate);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Name = "UGBDetails";
		resources.ApplyResources(this.cboNationality, "cboNationality");
		this.cboNationality.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboNationality).Name = "cboNationality";
		resources.ApplyResources(this.lblNationality, "lblNationality");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.lblNationality).Appearance = (AppearanceBase)(object)val7;
		this.lblNationality.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNationality).Name = "lblNationality";
		((ControlBase)this.lblNationality).WrapText = false;
		resources.ApplyResources(this.lblIDNo, "lblIDNo");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.lblIDNo).Appearance = (AppearanceBase)(object)val8;
		this.lblIDNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblIDNo).Name = "lblIDNo";
		((ControlBase)this.lblIDNo).WrapText = false;
		resources.ApplyResources(this.txtIDNo, "txtIDNo");
		((System.Windows.Forms.Control)(object)this.txtIDNo).Name = "txtIDNo";
		resources.ApplyResources(this.cboSubAccountGroup, "cboSubAccountGroup");
		this.cboSubAccountGroup.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSubAccountGroup).Name = "cboSubAccountGroup";
		((TextEditorControlBase)this.cboSubAccountGroup).ValueChanged += new System.EventHandler(cboSubAccountGroup_ValueChanged);
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val9;
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.cboSubAccountType, "cboSubAccountType");
		this.cboSubAccountType.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSubAccountType).Name = "cboSubAccountType";
		((TextEditorControlBase)this.cboSubAccountType).ValueChanged += new System.EventHandler(cboSubAccountType_ValueChanged);
		resources.ApplyResources(this.lblType, "lblType");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.lblType).Appearance = (AppearanceBase)(object)val10;
		this.lblType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblType).Name = "lblType";
		((ControlBase)this.lblType).WrapText = false;
		resources.ApplyResources(this.lblCDCExpireDate, "lblCDCExpireDate");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.lblCDCExpireDate).Appearance = (AppearanceBase)(object)val11;
		this.lblCDCExpireDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCDCExpireDate).Name = "lblCDCExpireDate";
		((ControlBase)this.lblCDCExpireDate).WrapText = false;
		resources.ApplyResources(this.lblNameAr, "lblNameAr");
		this.lblNameAr.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNameAr).Name = "lblNameAr";
		((ControlBase)this.lblNameAr).WrapText = false;
		resources.ApplyResources(this.dtpIDIssueDate, "dtpIDIssueDate");
		((UltraWinEditorMaskedControlBase)this.dtpIDIssueDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpIDIssueDate).Name = "dtpIDIssueDate";
		((System.Windows.Forms.Control)(object)this.dtpIDIssueDate).Enter += new System.EventHandler(dtpDateTime_Enter);
		resources.ApplyResources(this.dtpCDCExpireDate, "dtpCDCExpireDate");
		((UltraWinEditorMaskedControlBase)this.dtpCDCExpireDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpCDCExpireDate).Name = "dtpCDCExpireDate";
		((System.Windows.Forms.Control)(object)this.dtpCDCExpireDate).Enter += new System.EventHandler(dtpDateTime_Enter);
		resources.ApplyResources(this.txtNameAr, "txtNameAr");
		((System.Windows.Forms.Control)(object)this.txtNameAr).Name = "txtNameAr";
		resources.ApplyResources(this.lblNameEn, "lblNameEn");
		this.lblNameEn.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNameEn).Name = "lblNameEn";
		((ControlBase)this.lblNameEn).WrapText = false;
		resources.ApplyResources(this.txtNameEn, "txtNameEn");
		((System.Windows.Forms.Control)(object)this.txtNameEn).Name = "txtNameEn";
		resources.ApplyResources(this.lblIDIssueDate, "lblIDIssueDate");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.lblIDIssueDate).Appearance = (AppearanceBase)(object)val12;
		this.lblIDIssueDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblIDIssueDate).Name = "lblIDIssueDate";
		((ControlBase)this.lblIDIssueDate).WrapText = false;
		resources.ApplyResources(this.lblCDCNo, "lblCDCNo");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance13");
		((ControlBase)this.lblCDCNo).Appearance = (AppearanceBase)(object)val13;
		this.lblCDCNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCDCNo).Name = "lblCDCNo";
		((ControlBase)this.lblCDCNo).WrapText = false;
		resources.ApplyResources(this.txtNo, "txtNo");
		((System.Windows.Forms.Control)(object)this.txtNo).Name = "txtNo";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val14, "appearance14");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val14;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.txtCDCNo, "txtCDCNo");
		((System.Windows.Forms.Control)(object)this.txtCDCNo).Name = "txtCDCNo";
		resources.ApplyResources(this.dtpPassportExpireDate, "dtpPassportExpireDate");
		((UltraWinEditorMaskedControlBase)this.dtpPassportExpireDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpPassportExpireDate).Name = "dtpPassportExpireDate";
		((System.Windows.Forms.Control)(object)this.dtpPassportExpireDate).Enter += new System.EventHandler(dtpDateTime_Enter);
		resources.ApplyResources(this.lblPassportExpireDate, "lblPassportExpireDate");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val15, "appearance15");
		((ControlBase)this.lblPassportExpireDate).Appearance = (AppearanceBase)(object)val15;
		this.lblPassportExpireDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPassportExpireDate).Name = "lblPassportExpireDate";
		((ControlBase)this.lblPassportExpireDate).WrapText = false;
		resources.ApplyResources(this.UGBSignData, "UGBSignData");
		this.UGBSignData.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBSignData).Controls.Add((System.Windows.Forms.Control)(object)this.cboPassengerType);
		((System.Windows.Forms.Control)(object)this.UGBSignData).Controls.Add((System.Windows.Forms.Control)(object)this.lblPassengerType);
		((System.Windows.Forms.Control)(object)this.UGBSignData).Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		((System.Windows.Forms.Control)(object)this.UGBSignData).Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		((System.Windows.Forms.Control)(object)this.UGBSignData).Name = "UGBSignData";
		resources.ApplyResources(this.cboPassengerType, "cboPassengerType");
		this.cboPassengerType.AutoCompleteMode = (AutoCompleteMode)4;
		val16.DataValue = "1";
		resources.ApplyResources(val16, "valueListItem1");
		((SubObjectBase)val16).ForceApplyResources = "";
		val17.DataValue = "2";
		resources.ApplyResources(val17, "valueListItem2");
		((SubObjectBase)val17).ForceApplyResources = "";
		val18.DataValue = "3";
		resources.ApplyResources(val18, "valueListItem3");
		((SubObjectBase)val18).ForceApplyResources = "";
		this.cboPassengerType.Items.AddRange((ValueListItem[])(object)new ValueListItem[3] { val16, val17, val18 });
		((System.Windows.Forms.Control)(object)this.cboPassengerType).Name = "cboPassengerType";
		resources.ApplyResources(this.lblPassengerType, "lblPassengerType");
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val19, "appearance16");
		((ControlBase)this.lblPassengerType).Appearance = (AppearanceBase)(object)val19;
		this.lblPassengerType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPassengerType).Name = "lblPassengerType";
		((ControlBase)this.lblPassengerType).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBSignData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBDetails);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPassportNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPassportNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmInsertSeaManCrew";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPassportNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPassportNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBSignData, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassportNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBDetails).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.UGBDetails).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cboNationality).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtIDNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccountGroup).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccountType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpIDIssueDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCDCExpireDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCDCNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpPassportExpireDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBSignData).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBSignData).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.UGBSignData).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cboPassengerType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
