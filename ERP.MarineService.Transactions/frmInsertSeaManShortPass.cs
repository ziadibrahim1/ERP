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

public class frmInsertSeaManShortPass : frmBase
{
	private DataTable dtNationality;

	private DataTable dtSubAccountTypes;

	private DataTable dtSubAccountGroups;

	private string SubAccountID = "-1";

	private string OperationServiceID = "";

	private string OperationID = "";

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

	public UltraGroupBox ultraGroupBox1;

	private UltraComboEditor cboSubAccountGroup;

	private UltraLabel ultraLabel2;

	private UltraComboEditor cboSubAccountType;

	private UltraLabel lblType;

	private UltraLabel lblExitDate;

	private UltraDateTimeEditor dtpExitDate;

	private UltraLabel lblEntryDate;

	private UltraDateTimeEditor dtpEntryDate;

	private UltraLabel lblIDNo;

	private UltraTextEditor txtIDNo;

	private UltraComboEditor cboNationality;

	private UltraLabel lblNationality;

	private UltraDateTimeEditor dtpIDIssueDate;

	private UltraLabel lblIDIssueDate;

	private UltraTextEditor txtReason;

	private UltraLabel lblReason;

	private UltraTextEditor txtRemarks;

	private UltraLabel lblRemarks;

	public frmInsertSeaManShortPass()
	{
		InitializeComponent();
	}

	public frmInsertSeaManShortPass(string OPERATIONID, string OPERATIONSERVICEID)
		: this()
	{
		OperationServiceID = OPERATIONSERVICEID;
		OperationID = OPERATIONID;
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
		UltraDateTimeEditor obj = dtpExitDate;
		UltraDateTimeEditor obj2 = dtpEntryDate;
		UltraDateTimeEditor obj3 = dtpPassportExpireDate;
		UltraDateTimeEditor obj4 = dtpCDCExpireDate;
		object obj5 = (dtpIDIssueDate.Value = DBNull.Value);
		object obj6 = (obj4.Value = obj5);
		object obj8 = (obj3.Value = obj6);
		object value2 = (obj2.Value = obj8);
		obj.Value = value2;
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
			if (((Control)(object)txtCDCNo).Text.Trim() == "")
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال رقم الجواز الاسود", "Please Enter The CDC Number");
				((TextEditorControlBase)txtCDCNo).Focus();
				return false;
			}
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
					SeaMen.Insert_Update("-1", (((Control)(object)txtNo).Text == "") ? GetCode() : ((Control)(object)txtNo).Text, SubAccountID, "Null", "Null", "Null", "Null", "Null", (cboNationality.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboNationality).Value.ToString(), ((Control)(object)txtPassportNo).Text, "Null", (dtpPassportExpireDate.Value == null) ? "Null" : dtpPassportExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtCDCNo).Text, "Null", (dtpCDCExpireDate.Value == null) ? "Null" : dtpCDCExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtIDNo).Text, (dtpIDIssueDate.Value == null) ? "Null" : dtpIDIssueDate.DateTime.ToString(GlobalVariables.DateShortFormate), "Null", "Null", "Null", "Null", "Null", "Null", dtSubAccountGroups.Select("SubAccountID =  " + ((TextEditorControlBase)cboSubAccountGroup).Value.ToString())[0]["DefaultAccountID"].ToString(), "Null", "Null", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				}
				else if ("," + ((TextEditorControlBase)cboSubAccountType).Value.ToString() + "," == GlobalVariables.CaptainSubAccountTypeIDs)
				{
					Captains.Insert_Update("-1", (((Control)(object)txtNo).Text == "") ? GetCode() : ((Control)(object)txtNo).Text, SubAccountID, "Null", "Null", "Null", "Null", "Null", (cboNationality.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboNationality).Value.ToString(), ((Control)(object)txtPassportNo).Text, "Null", (dtpPassportExpireDate.Value == null) ? "Null" : dtpPassportExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtCDCNo).Text, "Null", (dtpCDCExpireDate.Value == null) ? "Null" : dtpCDCExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtIDNo).Text, (dtpIDIssueDate.Value == null) ? "Null" : dtpIDIssueDate.DateTime.ToString(GlobalVariables.DateShortFormate), "Null", "Null", "Null", "Null", "Null", "Null", dtSubAccountGroups.Select("SubAccountID =  " + ((TextEditorControlBase)cboSubAccountGroup).Value.ToString())[0]["DefaultAccountID"].ToString(), "Null", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
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
			OperationsServicesShortPass.Insert_Update("-1", (OperationServiceID == "") ? "Null" : OperationServiceID, (OperationID == "") ? "Null" : OperationID, SubAccountID, (dtpEntryDate.Value == null) ? "Null" : dtpEntryDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpExitDate.Value == null) ? "Null" : dtpExitDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtReason).Text, ((Control)(object)txtRemarks).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
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
		UltraDateTimeEditor obj = dtpPassportExpireDate;
		UltraDateTimeEditor obj2 = dtpCDCExpireDate;
		object obj3 = (dtpIDIssueDate.Value = DBNull.Value);
		object value2 = (obj2.Value = obj3);
		obj.Value = value2;
		((TextEditorControlBase)txtNameAr).Clear();
		((TextEditorControlBase)txtNameEn).Clear();
		((TextEditorControlBase)txtCDCNo).Clear();
		((TextEditorControlBase)txtIDNo).Clear();
		((TextEditorControlBase)txtNo).Clear();
	}

	public void SetSubAccountControls(bool NavMode)
	{
		((EditorButtonControlBase)cboSubAccountType).ReadOnly = NavMode;
		((EditorButtonControlBase)cboNationality).ReadOnly = NavMode;
		UltraDateTimeEditor obj = dtpPassportExpireDate;
		UltraDateTimeEditor obj2 = dtpCDCExpireDate;
		bool flag = (((EditorButtonControlBase)dtpIDIssueDate).ReadOnly = NavMode);
		bool readOnly = (((EditorButtonControlBase)obj2).ReadOnly = flag);
		((EditorButtonControlBase)obj).ReadOnly = readOnly;
		((EditorButtonControlBase)txtNameAr).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNameEn).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCDCNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtIDNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNo).ReadOnly = NavMode;
	}

	public void ClearAllControls()
	{
		ClearSubAccountControls();
		UltraDateTimeEditor obj = dtpExitDate;
		object value = (dtpEntryDate.Value = DBNull.Value);
		obj.Value = value;
		((TextEditorControlBase)txtRemarks).Clear();
		((TextEditorControlBase)txtReason).Clear();
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

	private void cboRejectionReason_ValueChanged(object sender, EventArgs e)
	{
	}

	private void cboFolder_ValueChanged(object sender, EventArgs e)
	{
		((Control)(object)txtNo).Text = SubAccounts.GetCode((cboSubAccountGroup.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccountGroup).Value.ToString(), IsFromServer: true);
	}

	private void chkRejected_CheckedChanged(object sender, EventArgs e)
	{
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
		SetSubAccountControls(dataTable.Rows.Count > 0);
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
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Expected O, but got Unknown
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Expected O, but got Unknown
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Expected O, but got Unknown
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Expected O, but got Unknown
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Expected O, but got Unknown
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Expected O, but got Unknown
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Expected O, but got Unknown
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Expected O, but got Unknown
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Expected O, but got Unknown
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Expected O, but got Unknown
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Expected O, but got Unknown
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Expected O, but got Unknown
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Expected O, but got Unknown
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Expected O, but got Unknown
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Expected O, but got Unknown
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Expected O, but got Unknown
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Expected O, but got Unknown
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Expected O, but got Unknown
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Expected O, but got Unknown
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Expected O, but got Unknown
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Expected O, but got Unknown
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Expected O, but got Unknown
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Expected O, but got Unknown
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Expected O, but got Unknown
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Expected O, but got Unknown
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Expected O, but got Unknown
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Expected O, but got Unknown
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Expected O, but got Unknown
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Expected O, but got Unknown
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Expected O, but got Unknown
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Expected O, but got Unknown
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmInsertSeaManShortPass));
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
		Appearance val16 = new Appearance();
		Appearance val17 = new Appearance();
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
		this.ultraGroupBox1 = new UltraGroupBox();
		this.txtRemarks = new UltraTextEditor();
		this.lblRemarks = new UltraLabel();
		this.txtReason = new UltraTextEditor();
		this.lblReason = new UltraLabel();
		this.lblExitDate = new UltraLabel();
		this.dtpExitDate = new UltraDateTimeEditor();
		this.lblEntryDate = new UltraLabel();
		this.dtpEntryDate = new UltraDateTimeEditor();
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
		((System.ComponentModel.ISupportInitialize)this.ultraGroupBox1).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtRemarks).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtReason).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpExitDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpEntryDate).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val, "appearance18");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val2).Image = resources.GetObject("appearance19.Image");
		resources.ApplyResources(val2, "appearance19");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val3).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val3, "appearance20");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val4).Image = resources.GetObject("appearance21.Image");
		resources.ApplyResources(val4, "appearance21");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val4;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val5, "appearance22");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblPassportNo, "lblPassportNo");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val6, "appearance23");
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
		resources.ApplyResources(val7, "appearance24");
		((ControlBase)this.lblNationality).Appearance = (AppearanceBase)(object)val7;
		this.lblNationality.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNationality).Name = "lblNationality";
		((ControlBase)this.lblNationality).WrapText = false;
		resources.ApplyResources(this.lblIDNo, "lblIDNo");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance25");
		((ControlBase)this.lblIDNo).Appearance = (AppearanceBase)(object)val8;
		this.lblIDNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblIDNo).Name = "lblIDNo";
		((ControlBase)this.lblIDNo).WrapText = false;
		resources.ApplyResources(this.txtIDNo, "txtIDNo");
		((System.Windows.Forms.Control)(object)this.txtIDNo).Name = "txtIDNo";
		resources.ApplyResources(this.cboSubAccountGroup, "cboSubAccountGroup");
		this.cboSubAccountGroup.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSubAccountGroup).Name = "cboSubAccountGroup";
		((TextEditorControlBase)this.cboSubAccountGroup).ValueChanged += new System.EventHandler(cboFolder_ValueChanged);
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance26");
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
		resources.ApplyResources(val10, "appearance27");
		((ControlBase)this.lblType).Appearance = (AppearanceBase)(object)val10;
		this.lblType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblType).Name = "lblType";
		((ControlBase)this.lblType).WrapText = false;
		resources.ApplyResources(this.lblCDCExpireDate, "lblCDCExpireDate");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance28");
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
		resources.ApplyResources(val12, "appearance29");
		((ControlBase)this.lblIDIssueDate).Appearance = (AppearanceBase)(object)val12;
		this.lblIDIssueDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblIDIssueDate).Name = "lblIDIssueDate";
		((ControlBase)this.lblIDIssueDate).WrapText = false;
		resources.ApplyResources(this.lblCDCNo, "lblCDCNo");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance30");
		((ControlBase)this.lblCDCNo).Appearance = (AppearanceBase)(object)val13;
		this.lblCDCNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCDCNo).Name = "lblCDCNo";
		((ControlBase)this.lblCDCNo).WrapText = false;
		resources.ApplyResources(this.txtNo, "txtNo");
		((System.Windows.Forms.Control)(object)this.txtNo).Name = "txtNo";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val14, "appearance31");
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
		resources.ApplyResources(val15, "appearance32");
		((ControlBase)this.lblPassportExpireDate).Appearance = (AppearanceBase)(object)val15;
		this.lblPassportExpireDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPassportExpireDate).Name = "lblPassportExpireDate";
		((ControlBase)this.lblPassportExpireDate).WrapText = false;
		resources.ApplyResources(this.ultraGroupBox1, "ultraGroupBox1");
		this.ultraGroupBox1.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.txtRemarks);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.lblRemarks);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.txtReason);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.lblReason);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.lblExitDate);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.dtpExitDate);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.lblEntryDate);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.dtpEntryDate);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Name = "ultraGroupBox1";
		resources.ApplyResources(this.txtRemarks, "txtRemarks");
		((System.Windows.Forms.Control)(object)this.txtRemarks).Name = "txtRemarks";
		resources.ApplyResources(this.lblRemarks, "lblRemarks");
		this.lblRemarks.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRemarks).Name = "lblRemarks";
		((ControlBase)this.lblRemarks).WrapText = false;
		resources.ApplyResources(this.txtReason, "txtReason");
		((System.Windows.Forms.Control)(object)this.txtReason).Name = "txtReason";
		resources.ApplyResources(this.lblReason, "lblReason");
		this.lblReason.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReason).Name = "lblReason";
		((ControlBase)this.lblReason).WrapText = false;
		resources.ApplyResources(this.lblExitDate, "lblExitDate");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Navy;
		((AppearanceBase)val16).Image = resources.GetObject("appearance33.Image");
		resources.ApplyResources(val16, "appearance33");
		((ControlBase)this.lblExitDate).Appearance = (AppearanceBase)(object)val16;
		this.lblExitDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExitDate).Name = "lblExitDate";
		((ControlBase)this.lblExitDate).WrapText = false;
		resources.ApplyResources(this.dtpExitDate, "dtpExitDate");
		((UltraWinEditorMaskedControlBase)this.dtpExitDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpExitDate).Name = "dtpExitDate";
		((System.Windows.Forms.Control)(object)this.dtpExitDate).Enter += new System.EventHandler(dtpDateTime_Enter);
		resources.ApplyResources(this.lblEntryDate, "lblEntryDate");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val17, "appearance34");
		((ControlBase)this.lblEntryDate).Appearance = (AppearanceBase)(object)val17;
		this.lblEntryDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEntryDate).Name = "lblEntryDate";
		((ControlBase)this.lblEntryDate).WrapText = false;
		resources.ApplyResources(this.dtpEntryDate, "dtpEntryDate");
		((UltraWinEditorMaskedControlBase)this.dtpEntryDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpEntryDate).Name = "dtpEntryDate";
		((System.Windows.Forms.Control)(object)this.dtpEntryDate).Enter += new System.EventHandler(dtpDateTime_Enter);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraGroupBox1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBDetails);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPassportNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPassportNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmInsertSeaManShortPass";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraGroupBox1, 0);
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
		((System.ComponentModel.ISupportInitialize)this.ultraGroupBox1).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtRemarks).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtReason).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpExitDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpEntryDate).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
