using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Privilege.MasterData;

public class frmUsers : frmGrid
{
	private int GroupLevel;

	private DataTable dtGroups;

	private DataTable dtBranches;

	private DataTable dtEmployees;

	private ValueList vlGroups = new ValueList();

	private ValueList vlBranches = new ValueList();

	private ValueList vlEmployees = new ValueList();

	private UltraTextEditor txtPasswordGrid = new UltraTextEditor();

	private IContainer components = null;

	private UltraCheckEditor chkEnabled;

	private UltraComboEditor cboDefaultBranch;

	private UltraLabel lblDefaultBranch;

	private UltraLabel lblPassword;

	private UltraTextEditor txtPassword;

	private UltraComboEditor cboGroups;

	private UltraLabel lblGroup;

	private UltraLabel lblUserName;

	private UltraTextEditor txtUserName;

	private UltraLabel lblSubAccount;

	private UltraComboEditor cboEmployee;

	private UltraTextEditor txtUserNameEn;

	private UltraLabel ultraLabel1;

	private UltraTextEditor txtLoginName;

	private UltraLabel ultraLabel2;

	private UltraLabel lblSalesDiscountPercentage;

	private UltraTextEditor txtSalesDiscountPercentage;

	private UltraLabel ultraLabel3;

	protected internal UltraButton btnDirectSalesApp;

	private UltraComboEditor cboEmployeeCode;

	public frmUsers()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		InitializeComponent();
		TableName = "Prv_Users";
		IDCol = "User_ID";
		((TextEditorControlBase)txtPassword).Appearance.TextHAlign = (HAlign)2;
		txtPassword.PasswordChar = '*';
		if (GlobalVariables.UserID == "1")
		{
			GroupLevel = -1;
		}
		else
		{
			GroupLevel = Convert.ToInt32(Groups.Select(GlobalVariables.GroupID, "-1", "1", IsFromServer: true).Rows[0]["GroupLevel"]);
		}
	}

	public override void PrepareData()
	{
		dtGroups = Groups.SelectByLevel(GroupLevel.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlGroups.ValueListItems.Clear();
		for (int i = 0; i < dtGroups.Rows.Count; i++)
		{
			vlGroups.ValueListItems.Add(dtGroups.Rows[i]["GroupID"], dtGroups.Rows[i][GlobalVariables.IsArabic ? "GroupNameAr" : "GroupNameEn"].ToString());
		}
		GlobalFunctions.FillCombo(cboGroups, dtGroups, "GroupID", GlobalVariables.IsArabic ? "GroupNameAr" : "GroupNameEn");
		dtBranches = Branches.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlBranches.ValueListItems.Clear();
		for (int j = 0; j < dtBranches.Rows.Count; j++)
		{
			vlBranches.ValueListItems.Add(dtBranches.Rows[j]["BranchID"], dtBranches.Rows[j][GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn"].ToString());
		}
		GlobalFunctions.FillCombo(cboDefaultBranch, dtBranches, "BranchID", GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn");
		dtEmployees = SubAccounts.SelectBySubAccountTypeIDs(",2,7,", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlEmployees.ValueListItems.Clear();
		for (int k = 0; k < dtEmployees.Rows.Count; k++)
		{
			vlEmployees.ValueListItems.Add(dtEmployees.Rows[k]["SubAccountID"], dtEmployees.Rows[k]["SubAccountName"].ToString());
		}
		GlobalFunctions.FillCombo(cboEmployee, dtEmployees, "SubAccountID", "SubAccountName");
		GlobalFunctions.FillCombo(cboEmployeeCode, dtEmployees, "SubAccountID", "EmployeeNo");
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnDirectSalesApp).Visible = bool.Parse(GlobalVariables.dtSystemModules.Select(" ModuleEnName= 'DirectSalesApp'")[0]["Installed"].ToString());
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtUserName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtUserNameEn).ReadOnly = NavMode;
		((EditorButtonControlBase)txtLoginName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPassword).ReadOnly = NavMode;
		((EditorButtonControlBase)cboGroups).ReadOnly = NavMode;
		((EditorButtonControlBase)cboDefaultBranch).ReadOnly = NavMode;
		((EditorButtonControlBase)cboEmployee).ReadOnly = NavMode;
		((EditorButtonControlBase)cboEmployeeCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSalesDiscountPercentage).ReadOnly = NavMode;
		((Control)(object)chkEnabled).Enabled = !NavMode;
		((Control)(object)btnDirectSalesApp).Enabled = !NavMode && !Adding;
		((TextEditorControlBase)txtUserName).Focus();
	}

	public override void ClearControls()
	{
		((TextEditorControlBase)txtUserName).Clear();
		((TextEditorControlBase)txtUserNameEn).Clear();
		((TextEditorControlBase)txtLoginName).Clear();
		((TextEditorControlBase)txtPassword).Clear();
		cboGroups.SelectedIndex = -1;
		cboDefaultBranch.SelectedIndex = -1;
		cboEmployee.SelectedIndex = -1;
		cboEmployeeCode.SelectedIndex = -1;
		((Control)(object)txtSalesDiscountPercentage).Text = "0";
		((UltraToggleEditorBase)chkEnabled).Checked = false;
	}

	public override void FillData()
	{
		dataTable = Users.SelectByGroupLevel(GroupLevel.ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
	}

	public override void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = dataTable;
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["User_ID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "اسم المستخدم" : "User Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "اسم المستخدم" : "User Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LoginName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم المستخدم" : "User Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LoginName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LoginName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Password"].Header).Caption = (GlobalVariables.IsArabic ? "كلمة المرور" : "Password");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Password"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Password"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Password"].EditorComponent = (Component)(object)txtPassword;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GroupID"].Header).Caption = (GlobalVariables.IsArabic ? "المجموعة" : "Group");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GroupID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GroupID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GroupID"].ValueList = (IValueList)(object)vlGroups;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SalesDiscountPercentage"].Header).Caption = (GlobalVariables.IsArabic ? "الخصم" : "Discount");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SalesDiscountPercentage"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SalesDiscountPercentage"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Enabled"].Header).Caption = (GlobalVariables.IsArabic ? "فع\u0651ال" : "Enabled");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Enabled"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Enabled"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Enabled"].DefaultCellValue = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OnLine"].Header).Caption = (GlobalVariables.IsArabic ? "مستخدم حالي" : "OnLine");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OnLine"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OnLine"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OnLine"].DefaultCellValue = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DefaultBranchID"].Header).Caption = (GlobalVariables.IsArabic ? "تابع لفرع" : "Default Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DefaultBranchID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DefaultBranchID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DefaultBranchID"].ValueList = (IValueList)(object)vlBranches;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الموظف" : "Employee");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlEmployees;
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtUserName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["UserNameAr"].Value.ToString();
		((Control)(object)txtUserNameEn).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["UserNameEn"].Value.ToString();
		((Control)(object)txtLoginName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["LoginName"].Value.ToString();
		((Control)(object)txtPassword).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["Password"].Value.ToString();
		((TextEditorControlBase)cboGroups).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["GroupID"].Value;
		((TextEditorControlBase)cboDefaultBranch).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["DefaultBranchID"].Value;
		((TextEditorControlBase)cboEmployeeCode).ValueChanged -= cboEmployeeCode_ValueChanged;
		((TextEditorControlBase)cboEmployee).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["SubAccountID"].Value;
		((TextEditorControlBase)cboEmployeeCode).ValueChanged += cboEmployeeCode_ValueChanged;
		((Control)(object)txtSalesDiscountPercentage).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["SalesDiscountPercentage"].Value.ToString();
		((UltraToggleEditorBase)chkEnabled).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Enabled"].Value.ToString());
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtUserName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال الإسم العربي", "Please Insert User Name");
			((TextEditorControlBase)txtUserName).Focus();
			return false;
		}
		if (dataTable.Select("UserNameAr='" + ((Control)(object)txtUserName).Text + "'" + (Adding ? "" : (" And User_ID <>" + ((UltraGridBase)ULGData).ActiveRow.Cells["User_ID"].Value.ToString()))).Length != 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن تكرار الإسم العربي", "Please Insert User Name");
			((TextEditorControlBase)txtUserName).Focus();
			return false;
		}
		if (((Control)(object)txtLoginName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال المستخدم", "Please Insert Login Name");
			((TextEditorControlBase)txtLoginName).Focus();
			return false;
		}
		if (dataTable.Select("LoginName='" + ((Control)(object)txtLoginName).Text + "'" + (Adding ? "" : (" And User_ID <>" + ((UltraGridBase)ULGData).ActiveRow.Cells["User_ID"].Value.ToString()))).Length != 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن تكرار المستخدم", "Please Insert User Name");
			((TextEditorControlBase)txtLoginName).Focus();
			return false;
		}
		if (cboGroups.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار إسم المجموعة", "Please Select Group Name");
			((TextEditorControlBase)cboGroups).Focus();
			return false;
		}
		if (cboDefaultBranch.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار الفرع التابع له", "Please Select Default Branch");
			((TextEditorControlBase)cboDefaultBranch).Focus();
			return false;
		}
		if (((Control)(object)txtSalesDiscountPercentage).Text != "" && decimal.Parse(((Control)(object)txtSalesDiscountPercentage).Text) > 100m)
		{
			GlobalVariables.InformationMB.Show("لابد أن نسبة الخصم على المبيعات لا تتعدى 100", "Sales Discount Percentage must Increase From 100");
			((TextEditorControlBase)txtSalesDiscountPercentage).Focus();
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		int num = Users.Insert_Update("-1", ((Control)(object)txtUserName).Text, (((Control)(object)txtUserNameEn).Text == "") ? "Null" : ((Control)(object)txtUserNameEn).Text, ((Control)(object)txtLoginName).Text, ((Control)(object)txtPassword).Text, ((TextEditorControlBase)cboGroups).Value.ToString(), ((UltraToggleEditorBase)chkEnabled).Checked ? "1" : "0", "0", "0", ((TextEditorControlBase)cboDefaultBranch).Value.ToString(), (cboEmployee.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboEmployee).Value.ToString(), (((Control)(object)txtSalesDiscountPercentage).Text == "") ? "0" : ((Control)(object)txtSalesDiscountPercentage).Text, GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID, IsFromServer: true);
		Groups.ResetUserPrivileges(((TextEditorControlBase)cboGroups).Value.ToString(), num.ToString(), GlobalVariables.CurrentBranchID, IsFromServer: true);
	}

	public override void UpdateData()
	{
		if (GlobalVariables.UserID != ((UltraGridBase)ULGData).ActiveRow.Cells["User_ID"].Value.ToString())
		{
			Users.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["User_ID"].Value.ToString(), ((Control)(object)txtUserName).Text, (((Control)(object)txtUserNameEn).Text == "") ? "Null" : ((Control)(object)txtUserNameEn).Text, ((Control)(object)txtLoginName).Text, ((Control)(object)txtPassword).Text, ((TextEditorControlBase)cboGroups).Value.ToString(), ((UltraToggleEditorBase)chkEnabled).Checked ? "1" : "0", ((UltraGridBase)ULGData).ActiveRow.Cells["ISPOS"].Value.ToString().Equals("True") ? "1" : "0", ((UltraGridBase)ULGData).ActiveRow.Cells["OnLine"].Value.ToString().Equals("True") ? "1" : "0", ((TextEditorControlBase)cboDefaultBranch).Value.ToString(), (cboEmployee.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboEmployee).Value.ToString(), (((Control)(object)txtSalesDiscountPercentage).Text == "") ? "0" : ((Control)(object)txtSalesDiscountPercentage).Text, GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID, IsFromServer: true);
		}
		else
		{
			GlobalVariables.InformationMB.Show("لا يمكن تعديل المستخدم الحالي.برجاء الدخول بمستخدم آخر لتعديل بيانات هذا المستخدم", "Can not Edit active user.To Edit this user ,Please log in with another account");
		}
	}

	public override void DeleteData()
	{
		if (GlobalVariables.UserID != ((UltraGridBase)ULGData).ActiveRow.Cells["User_ID"].Value.ToString())
		{
			if (int.Parse(Main.ExecuteQuery_DataTable(" Select Count(*) As Counter from Trans_Log Where User_ID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["User_ID"].Value.ToString()).Rows[0]["Counter"].ToString()) <= 0)
			{
				Main.StartBulkTrans(FromServer: true);
				try
				{
					UsersSafes.DeleteByUser_ID(((UltraGridBase)ULGData).ActiveRow.Cells["User_ID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
					UsersBranches.DeleteByUser_ID(((UltraGridBase)ULGData).ActiveRow.Cells["User_ID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
					UsersFormsFunctions.DeleteByUser_ID(((UltraGridBase)ULGData).ActiveRow.Cells["User_ID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
					UsersForms.DeleteByUser_ID(((UltraGridBase)ULGData).ActiveRow.Cells["User_ID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
					UsersFunctions.DeleteByUser_ID(((UltraGridBase)ULGData).ActiveRow.Cells["User_ID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
					UsersReports.DeleteByUser_ID(((UltraGridBase)ULGData).ActiveRow.Cells["User_ID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
					Users.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["User_ID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
					Main.EndBulkTrans(FromServer: true);
					return;
				}
				catch
				{
					Main.RollbackBulkTrans(FromServer: true);
					DataSaved = false;
					GlobalVariables.InformationMB.Show("حدث خطا فى عملية الحذف  ", "Error Occured");
					return;
				}
			}
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذا المستخدم لقيامه بحركات فى النظام", "Can not Delete User Because He Working Transactions in The System");
		}
		else
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف المستخدم الحالي.برجاء الدخول بمستخدم آخر لحذف هذا المستخدم", "Can not delete active user.To delete this user ,Please log in with another account");
		}
	}

	private void txtSalesDiscountPercentage_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtSalesDiscountPercentage_Leave(object sender, EventArgs e)
	{
		if (((Control)(object)txtSalesDiscountPercentage).Text != "" && decimal.Parse(((Control)(object)txtSalesDiscountPercentage).Text) > 100m)
		{
			((Control)(object)txtSalesDiscountPercentage).Text = "100";
		}
	}

	private void btnDirectSalesApp_Click(object sender, EventArgs e)
	{
		frmUsersDirectSalesApp frmUsersDirectSalesApp2 = new frmUsersDirectSalesApp(((UltraGridBase)ULGData).ActiveRow.Cells["User_ID"].Value.ToString());
		((Control)(object)frmUsersDirectSalesApp2.lblTitle).Text = (GlobalVariables.IsArabic ? "صلاحيات مستخدم البيع المباشر" : "Direct Sales App User");
		frmUsersDirectSalesApp2.ShowDialog();
	}

	private void cboEmployee_ValueChanged(object sender, EventArgs e)
	{
		if (cboEmployee.SelectedIndex > -1)
		{
			cboEmployeeCode.SelectedIndex = cboEmployee.SelectedIndex;
		}
	}

	private void cboEmployeeCode_ValueChanged(object sender, EventArgs e)
	{
		if (cboEmployeeCode.SelectedIndex > -1)
		{
			cboEmployee.SelectedIndex = cboEmployeeCode.SelectedIndex;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Privilege.MasterData.frmUsers));
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
		this.chkEnabled = new UltraCheckEditor();
		this.cboDefaultBranch = new UltraComboEditor();
		this.lblDefaultBranch = new UltraLabel();
		this.lblPassword = new UltraLabel();
		this.txtPassword = new UltraTextEditor();
		this.cboGroups = new UltraComboEditor();
		this.lblGroup = new UltraLabel();
		this.lblUserName = new UltraLabel();
		this.txtUserName = new UltraTextEditor();
		this.lblSubAccount = new UltraLabel();
		this.cboEmployee = new UltraComboEditor();
		this.txtUserNameEn = new UltraTextEditor();
		this.ultraLabel1 = new UltraLabel();
		this.txtLoginName = new UltraTextEditor();
		this.ultraLabel2 = new UltraLabel();
		this.lblSalesDiscountPercentage = new UltraLabel();
		this.txtSalesDiscountPercentage = new UltraTextEditor();
		this.ultraLabel3 = new UltraLabel();
		this.btnDirectSalesApp = new UltraButton();
		this.cboEmployeeCode = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkEnabled).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassword).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboGroups).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboEmployee).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLoginName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalesDiscountPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboEmployeeCode).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.ULGData, "ULGData");
		((UltraGridBase)base.ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val, "appearance1");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val2, "appearance2");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val3, "appearance3");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val4, "appearance4");
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(base.btnAdd, "btnAdd");
		resources.ApplyResources(base.btnUpdate, "btnUpdate");
		resources.ApplyResources(base.btnDelete, "btnDelete");
		resources.ApplyResources(base.btnPrint, "btnPrint");
		resources.ApplyResources(base.btnOK, "btnOK");
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		resources.ApplyResources(base.btnClose, "btnClose");
		resources.ApplyResources(base.btnSaveClose, "btnSaveClose");
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.lblHistory, "lblHistory");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val8, "appearance11");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance9");
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val9;
		base.cboTransactionBranch.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboTransactionBranch.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.chkEnabled, "chkEnabled");
		((System.Windows.Forms.Control)(object)this.chkEnabled).Name = "chkEnabled";
		resources.ApplyResources(this.cboDefaultBranch, "cboDefaultBranch");
		this.cboDefaultBranch.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboDefaultBranch).Name = "cboDefaultBranch";
		resources.ApplyResources(this.lblDefaultBranch, "lblDefaultBranch");
		this.lblDefaultBranch.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDefaultBranch).Name = "lblDefaultBranch";
		((ControlBase)this.lblDefaultBranch).WrapText = false;
		resources.ApplyResources(this.lblPassword, "lblPassword");
		this.lblPassword.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPassword).Name = "lblPassword";
		((ControlBase)this.lblPassword).WrapText = false;
		resources.ApplyResources(this.txtPassword, "txtPassword");
		resources.ApplyResources(val10, "appearance10");
		((TextEditorControlBase)this.txtPassword).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.txtPassword).Name = "txtPassword";
		this.txtPassword.PasswordChar = '*';
		resources.ApplyResources(this.cboGroups, "cboGroups");
		this.cboGroups.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboGroups).Name = "cboGroups";
		resources.ApplyResources(this.lblGroup, "lblGroup");
		this.lblGroup.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGroup).Name = "lblGroup";
		((ControlBase)this.lblGroup).WrapText = false;
		resources.ApplyResources(this.lblUserName, "lblUserName");
		this.lblUserName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUserName).Name = "lblUserName";
		((ControlBase)this.lblUserName).WrapText = false;
		resources.ApplyResources(this.txtUserName, "txtUserName");
		((System.Windows.Forms.Control)(object)this.txtUserName).Name = "txtUserName";
		resources.ApplyResources(this.lblSubAccount, "lblSubAccount");
		this.lblSubAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSubAccount).Name = "lblSubAccount";
		((ControlBase)this.lblSubAccount).WrapText = false;
		resources.ApplyResources(this.cboEmployee, "cboEmployee");
		this.cboEmployee.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboEmployee).Name = "cboEmployee";
		((TextEditorControlBase)this.cboEmployee).ValueChanged += new System.EventHandler(cboEmployee_ValueChanged);
		resources.ApplyResources(this.txtUserNameEn, "txtUserNameEn");
		((System.Windows.Forms.Control)(object)this.txtUserNameEn).Name = "txtUserNameEn";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.txtLoginName, "txtLoginName");
		((System.Windows.Forms.Control)(object)this.txtLoginName).Name = "txtLoginName";
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.lblSalesDiscountPercentage, "lblSalesDiscountPercentage");
		this.lblSalesDiscountPercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSalesDiscountPercentage).Name = "lblSalesDiscountPercentage";
		((ControlBase)this.lblSalesDiscountPercentage).WrapText = false;
		resources.ApplyResources(this.txtSalesDiscountPercentage, "txtSalesDiscountPercentage");
		((System.Windows.Forms.Control)(object)this.txtSalesDiscountPercentage).Name = "txtSalesDiscountPercentage";
		((System.Windows.Forms.Control)(object)this.txtSalesDiscountPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSalesDiscountPercentage_KeyPress);
		((System.Windows.Forms.Control)(object)this.txtSalesDiscountPercentage).Leave += new System.EventHandler(txtSalesDiscountPercentage_Leave);
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.btnDirectSalesApp, "btnDirectSalesApp");
		((System.Windows.Forms.Control)(object)this.btnDirectSalesApp).Name = "btnDirectSalesApp";
		((System.Windows.Forms.Control)(object)this.btnDirectSalesApp).Click += new System.EventHandler(btnDirectSalesApp_Click);
		resources.ApplyResources(this.cboEmployeeCode, "cboEmployeeCode");
		((TextEditorControlBase)this.cboEmployeeCode).AlwaysInEditMode = true;
		this.cboEmployeeCode.AutoCompleteMode = (AutoCompleteMode)3;
		((System.Windows.Forms.Control)(object)this.cboEmployeeCode).Name = "cboEmployeeCode";
		((TextEditorControlBase)this.cboEmployeeCode).ValueChanged += new System.EventHandler(cboEmployeeCode_ValueChanged);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboEmployeeCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDirectSalesApp);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesDiscountPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSalesDiscountPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkEnabled);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboEmployee);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboDefaultBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDefaultBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPassword);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPassword);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboGroups);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGroup);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUserName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtLoginName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtUserNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtUserName);
		base.Name = "frmUsers";
		base.ShowIcon = false;
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtUserName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtUserNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtLoginName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUserName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGroup, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboGroups, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPassword, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPassword, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDefaultBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboDefaultBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboEmployee, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkEnabled, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSalesDiscountPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSalesDiscountPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDirectSalesApp, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboEmployeeCode, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkEnabled).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassword).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboGroups).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboEmployee).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLoginName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalesDiscountPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboEmployeeCode).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
