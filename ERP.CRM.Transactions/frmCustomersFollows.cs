using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.CRM;
using BusinessLayer.HR;
using BusinessLayer.Lenses;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.CRM.Transactions;

public class frmCustomersFollows : frmHeaderDetails
{
	private DataTable dtUsers;

	private DataTable dtReports;

	private DataTable dtSalesMan;

	private DataTable dtCustomers;

	private DataTable dtCommunicationSteps;

	private int CustomerID = 0;

	private IContainer components = null;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	public UltraButton btnStepsSearch;

	private UltraComboEditor cboCommunicationStep;

	private UltraLabel lblCommunicationStep;

	public UltraButton btnCustomersSearch;

	private UltraComboEditor cboCustomers;

	private UltraLabel lblCustomerName;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblBackTitle;

	private UltraLabel lblCurrentStepEmployee;

	private UltraComboEditor cboCurrentStepEmployeeSubAccounts;

	private UltraLabel lblNextStepEmployee;

	private UltraComboEditor cboNextStepEmpoyee;

	private UltraDateTimeEditor dtpNextStepDate;

	private UltraLabel lblNextStepDate;

	private UltraTextEditor txtComments;

	private UltraLabel lblComments;

	public UltraButton btnNextStepEmployeeSubAccount;

	public frmCustomersFollows()
	{
		InitializeComponent();
		TableName = "CRM_CustomersFollows";
		IDCol = "CustomerFollowID";
		NoCol = "CustomerFollowNo";
		DateCol = "CustomerFollowDate";
	}

	public frmCustomersFollows(int _customerID)
		: this()
	{
		CustomerID = _customerID;
		Adding = true;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtSalesMan = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.BranchIDs, "1", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrentStepEmployeeSubAccounts, dtSalesMan, "SubAccountID", "SubAccountName");
		GlobalFunctions.FillCombo(cboNextStepEmpoyee, dtSalesMan, "SubAccountID", "SubAccountName");
		dtCustomers = Customers.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCustomers, dtCustomers, "CustomerID", "CustomerName");
		dtCommunicationSteps = CommunicationSteps.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCommunicationStep, dtCommunicationSteps, "CommunicationStepID", "CommunicationStepName");
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
	}

	public override void FillData()
	{
		if (CustomerID != 0)
		{
			drMaster = null;
			btnAddClick();
			((Control)(object)btnCustomersSearch).Visible = false;
			((Control)(object)btnAdd).Visible = false;
			((Control)(object)btnOK).Visible = false;
		}
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = CustomersFollows.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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

	public override void DisplayData()
	{
		base.DisplayData();
		if (drMaster != null)
		{
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			((TextEditorControlBase)cboCustomers).ValueChanged -= cboCustomers_ValueChanged;
			((TextEditorControlBase)cboTransactionBranch).Value = drMaster["BranchID"];
			((Control)(object)txtCode).Text = drMaster["CustomerFollowNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["CustomerFollowDate"];
			((TextEditorControlBase)cboCustomers).Value = drMaster["CustomerID"];
			((TextEditorControlBase)cboCommunicationStep).Value = drMaster["CommunicationStepID"];
			((TextEditorControlBase)cboCurrentStepEmployeeSubAccounts).Value = drMaster["EmployeeSubAccountID"];
			((TextEditorControlBase)cboNextStepEmpoyee).Value = drMaster["NextEmployeeSubAccountID"];
			dtpNextStepDate.Value = (DateTime)drMaster["NextStepDate"];
			((Control)(object)txtComments).Text = drMaster["Comment"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((TextEditorControlBase)cboCustomers).ValueChanged += cboCustomers_ValueChanged;
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
		}
		else
		{
			ClearControls();
		}
		((TextEditorControlBase)txtCode).Focus();
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)txtCode).ReadOnly = Adding;
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCommunicationStep).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCustomers).ReadOnly = NavMode;
		((EditorButtonControlBase)cboNextStepEmpoyee).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCurrentStepEmployeeSubAccounts).ReadOnly = true;
		((EditorButtonControlBase)dtpNextStepDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtComments).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)btnCustomersSearch).Visible = !NavMode;
		((Control)(object)btnStepsSearch).Visible = !NavMode;
		((Control)(object)btnNextStepEmployeeSubAccount).Visible = !NavMode;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		dtpNextStepDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? CustomersFollows.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		cboCommunicationStep.SelectedIndex = -1;
		cboCustomers.SelectedIndex = -1;
		if (CustomerID == 0)
		{
			cboCustomers.SelectedIndex = -1;
		}
		else
		{
			((TextEditorControlBase)cboCustomers).Value = CustomerID;
		}
		if (dtUsers.Select("User_ID =" + GlobalVariables.UserID)[0]["SubAccountID"] != DBNull.Value)
		{
			((TextEditorControlBase)cboCurrentStepEmployeeSubAccounts).Value = dtUsers.Select("User_ID =" + GlobalVariables.UserID)[0]["SubAccountID"];
			((TextEditorControlBase)cboNextStepEmpoyee).Value = dtUsers.Select("User_ID =" + GlobalVariables.UserID)[0]["SubAccountID"];
		}
		else
		{
			cboCurrentStepEmployeeSubAccounts.SelectedIndex = -1;
			cboNextStepEmpoyee.SelectedIndex = -1;
		}
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)txtComments).Clear();
		((TextEditorControlBase)cboCustomers).Focus();
	}

	public override bool ValidateData()
	{
		if (!FiscalYear.ChkForConfirmedFiscalYear(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), IsFromServer: false))
		{
			GlobalVariables.InformationMB.Show("السنة المالية غير معتمدة", "The Fiscal Year is not confirmed..");
			return false;
		}
		if (FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false))
		{
			GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ يقع في فتره ماليه مغلقة", "The Date you choosed\n\r exists in closed fisical period");
			return false;
		}
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال رقم  الأذن", "Please Enter The Voucher Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (cboCustomers.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال العميل", "Please Select Customer");
			((TextEditorControlBase)cboCustomers).Focus();
			return false;
		}
		if (cboCommunicationStep.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار خطوة التواصل", "Please Select Communication Step");
			((TextEditorControlBase)cboCommunicationStep).Focus();
			return false;
		}
		if (cboNextStepEmpoyee.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال المسئول عن الخطوة القادمة", "Please Select Next Step Employee");
			((TextEditorControlBase)cboNextStepEmpoyee).Focus();
			return false;
		}
		if (dtpNextStepDate.Value == null)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل تاريخ الخطوة القادمة ", "Please Enter Next Step Date");
			((Control)(object)dtpNextStepDate).Focus();
			return false;
		}
		if (dtpDate.DateTime > dtpNextStepDate.DateTime)
		{
			GlobalVariables.InformationMB.Show("تاريخ الخطوة القادمة قبل تاريخ هذه الخطوة", "Next Step Date is Before This Step Date");
			((Control)(object)dtpNextStepDate).Focus();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("CRM_CustomersFollows", "CustomerFollowNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["CustomerFollowNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = CustomersFollows.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Voucher Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByBranchID;
		}
		if (dtUsers.Select("User_ID =" + GlobalVariables.UserID)[0]["SubAccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("هذا المستخدم ليس له حساب تحليلي", "This User Has ");
			return false;
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = CustomersFollows.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboCustomers.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCustomers).Value.ToString(), (cboCommunicationStep.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCommunicationStep).Value.ToString(), (cboCurrentStepEmployeeSubAccounts.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCurrentStepEmployeeSubAccounts).Value.ToString(), ((Control)(object)txtComments).Text, dtpNextStepDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboNextStepEmpoyee.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboNextStepEmpoyee).Value.ToString(), ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
		}
	}

	public override void UpdateData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = CustomersFollows.Insert_Update(drMaster["CustomerFollowID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboCustomers.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCustomers).Value.ToString(), (cboCommunicationStep.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCommunicationStep).Value.ToString(), (cboCurrentStepEmployeeSubAccounts.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCurrentStepEmployeeSubAccounts).Value.ToString(), ((Control)(object)txtComments).Text, dtpNextStepDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboNextStepEmpoyee.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboNextStepEmpoyee).Value.ToString(), ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			CustomersFollows.DeleteVirtual(drMaster["CustomerFollowID"].ToString(), GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnRefreshDataClick()
	{
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtSalesMan = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.BranchIDs, "-1", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrentStepEmployeeSubAccounts, dtSalesMan, "SubAccountID", "SubAccountName");
		GlobalFunctions.FillCombo(cboNextStepEmpoyee, dtSalesMan, "SubAccountID", "SubAccountName");
		dtCustomers = Customers.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCustomers, dtCustomers, "CustomerID", "CustomerName");
		dtCommunicationSteps = CommunicationSteps.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCommunicationStep, dtCommunicationSteps, "CommunicationStepID", "CommunicationStepName");
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
	}

	public override void btnPrintClick()
	{
		string val = "";
		if (dtReports.Rows.Count > 0)
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
			val = dtReports.Rows[0]["isoCode"].ToString();
		}
		else
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_CRM_CustomersFollows_A.rpt" : "Rep_CRM_CustomersFollows_E.rpt"));
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@CustomerFollowIDs", "," + RowID + ",");
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.CRMCustomersFollows(-1, 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["CustomerFollowID"].ToString();
			FillData();
		}
	}

	private void cboCustomers_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.CRMCustomers(-1, IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboCustomers).Value = num;
			}
		}
	}

	private void btnNextStepEmployeeSubAccount_Click(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			int num = SearchFunctions.Employees("1", "-1", IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboNextStepEmpoyee).Value = num;
			}
		}
	}

	private void cboCustomers_ValueChanged(object sender, EventArgs e)
	{
		if (cboCustomers.SelectedIndex > -1)
		{
			int num = Customers.SelectNextStep(((TextEditorControlBase)cboCustomers).Value.ToString(), IsFromServer: false);
			((TextEditorControlBase)cboCommunicationStep).Value = num;
		}
	}

	private void btnCustomersSearch_Click(object sender, EventArgs e)
	{
		((TextEditorControlBase)cboCustomers).Value = SearchFunctions.CRMCustomers(-1, IsFromServer: false);
	}

	private void cboCommunicationStep_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119 && cboCommunicationStep.SelectedIndex != -1)
		{
			int num = SearchFunctions.CRMCommunicationSteps(IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboCommunicationStep).Value = num;
			}
		}
	}

	private void btnStepsSearch_Click(object sender, EventArgs e)
	{
		if (cboCommunicationStep.SelectedIndex != -1)
		{
			int num = SearchFunctions.CRMCommunicationSteps(IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboCommunicationStep).Value = num;
			}
		}
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = InvoicesReturns.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Expected O, but got Unknown
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Expected O, but got Unknown
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Expected O, but got Unknown
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Expected O, but got Unknown
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Expected O, but got Unknown
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Expected O, but got Unknown
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Expected O, but got Unknown
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Expected O, but got Unknown
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Expected O, but got Unknown
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Expected O, but got Unknown
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.CRM.Transactions.frmCustomersFollows));
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
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.btnStepsSearch = new UltraButton();
		this.cboCommunicationStep = new UltraComboEditor();
		this.lblCommunicationStep = new UltraLabel();
		this.btnCustomersSearch = new UltraButton();
		this.cboCustomers = new UltraComboEditor();
		this.lblCustomerName = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblBackTitle = new UltraLabel();
		this.lblCurrentStepEmployee = new UltraLabel();
		this.cboCurrentStepEmployeeSubAccounts = new UltraComboEditor();
		this.lblNextStepEmployee = new UltraLabel();
		this.cboNextStepEmpoyee = new UltraComboEditor();
		this.dtpNextStepDate = new UltraDateTimeEditor();
		this.lblNextStepDate = new UltraLabel();
		this.txtComments = new UltraTextEditor();
		this.lblComments = new UltraLabel();
		this.btnNextStepEmployeeSubAccount = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCommunicationStep).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCustomers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrentStepEmployeeSubAccounts).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboNextStepEmpoyee).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpNextStepDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtComments).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.UGBDetails, "UGBDetails");
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		resources.ApplyResources(base.ULGData, "ULGData");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.txtCode, "txtCode");
		resources.ApplyResources(base.lblCode, "lblCode");
		this.lblDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblDate, "lblDate");
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnStepsSearch).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(this.btnStepsSearch, "btnStepsSearch");
		((System.Windows.Forms.Control)(object)this.btnStepsSearch).Name = "btnStepsSearch";
		((System.Windows.Forms.Control)(object)this.btnStepsSearch).Click += new System.EventHandler(btnStepsSearch_Click);
		this.cboCommunicationStep.AutoCompleteMode = (AutoCompleteMode)2;
		resources.ApplyResources(this.cboCommunicationStep, "cboCommunicationStep");
		((System.Windows.Forms.Control)(object)this.cboCommunicationStep).Name = "cboCommunicationStep";
		((System.Windows.Forms.Control)(object)this.cboCommunicationStep).KeyDown += new System.Windows.Forms.KeyEventHandler(cboCommunicationStep_KeyDown);
		this.lblCommunicationStep.AutoEllipsis = false;
		resources.ApplyResources(this.lblCommunicationStep, "lblCommunicationStep");
		((System.Windows.Forms.Control)(object)this.lblCommunicationStep).Name = "lblCommunicationStep";
		((ControlBase)this.lblCommunicationStep).WrapText = false;
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnCustomersSearch).Appearance = (AppearanceBase)(object)val10;
		resources.ApplyResources(this.btnCustomersSearch, "btnCustomersSearch");
		((System.Windows.Forms.Control)(object)this.btnCustomersSearch).Name = "btnCustomersSearch";
		((System.Windows.Forms.Control)(object)this.btnCustomersSearch).Click += new System.EventHandler(btnCustomersSearch_Click);
		this.cboCustomers.AutoCompleteMode = (AutoCompleteMode)2;
		resources.ApplyResources(this.cboCustomers, "cboCustomers");
		((System.Windows.Forms.Control)(object)this.cboCustomers).Name = "cboCustomers";
		((TextEditorControlBase)this.cboCustomers).ValueChanged += new System.EventHandler(cboCustomers_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboCustomers).KeyDown += new System.Windows.Forms.KeyEventHandler(cboCustomers_KeyDown);
		this.lblCustomerName.AutoEllipsis = false;
		resources.ApplyResources(this.lblCustomerName, "lblCustomerName");
		((System.Windows.Forms.Control)(object)this.lblCustomerName).Name = "lblCustomerName";
		((ControlBase)this.lblCustomerName).WrapText = false;
		this.lblNotes.AutoEllipsis = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblBackTitle, "lblBackTitle");
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.FromArgb(191, 200, 234);
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.lblBackTitle).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.lblBackTitle).Name = "lblBackTitle";
		((UltraControlBase)this.lblBackTitle).UseAppStyling = false;
		this.lblCurrentStepEmployee.AutoEllipsis = false;
		resources.ApplyResources(this.lblCurrentStepEmployee, "lblCurrentStepEmployee");
		((System.Windows.Forms.Control)(object)this.lblCurrentStepEmployee).Name = "lblCurrentStepEmployee";
		((ControlBase)this.lblCurrentStepEmployee).WrapText = false;
		this.cboCurrentStepEmployeeSubAccounts.AutoCompleteMode = (AutoCompleteMode)2;
		resources.ApplyResources(this.cboCurrentStepEmployeeSubAccounts, "cboCurrentStepEmployeeSubAccounts");
		((System.Windows.Forms.Control)(object)this.cboCurrentStepEmployeeSubAccounts).Name = "cboCurrentStepEmployeeSubAccounts";
		((System.Windows.Forms.Control)(object)this.cboCurrentStepEmployeeSubAccounts).KeyDown += new System.Windows.Forms.KeyEventHandler(cboCommunicationStep_KeyDown);
		this.lblNextStepEmployee.AutoEllipsis = false;
		resources.ApplyResources(this.lblNextStepEmployee, "lblNextStepEmployee");
		((System.Windows.Forms.Control)(object)this.lblNextStepEmployee).Name = "lblNextStepEmployee";
		((ControlBase)this.lblNextStepEmployee).WrapText = false;
		this.cboNextStepEmpoyee.AutoCompleteMode = (AutoCompleteMode)2;
		resources.ApplyResources(this.cboNextStepEmpoyee, "cboNextStepEmpoyee");
		((System.Windows.Forms.Control)(object)this.cboNextStepEmpoyee).Name = "cboNextStepEmpoyee";
		((System.Windows.Forms.Control)(object)this.cboNextStepEmpoyee).KeyDown += new System.Windows.Forms.KeyEventHandler(cboCommunicationStep_KeyDown);
		((UltraWinEditorMaskedControlBase)this.dtpNextStepDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpNextStepDate, "dtpNextStepDate");
		((System.Windows.Forms.Control)(object)this.dtpNextStepDate).Name = "dtpNextStepDate";
		((System.Windows.Forms.Control)(object)this.dtpNextStepDate).TabStop = false;
		this.lblNextStepDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblNextStepDate, "lblNextStepDate");
		((System.Windows.Forms.Control)(object)this.lblNextStepDate).Name = "lblNextStepDate";
		((ControlBase)this.lblNextStepDate).WrapText = false;
		resources.ApplyResources(this.txtComments, "txtComments");
		((System.Windows.Forms.Control)(object)this.txtComments).Name = "txtComments";
		this.lblComments.AutoEllipsis = false;
		resources.ApplyResources(this.lblComments, "lblComments");
		((System.Windows.Forms.Control)(object)this.lblComments).Name = "lblComments";
		((ControlBase)this.lblComments).WrapText = false;
		((AppearanceBase)val12).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnNextStepEmployeeSubAccount).Appearance = (AppearanceBase)(object)val12;
		resources.ApplyResources(this.btnNextStepEmployeeSubAccount, "btnNextStepEmployeeSubAccount");
		((System.Windows.Forms.Control)(object)this.btnNextStepEmployeeSubAccount).Name = "btnNextStepEmployeeSubAccount";
		((System.Windows.Forms.Control)(object)this.btnNextStepEmployeeSubAccount).Click += new System.EventHandler(btnNextStepEmployeeSubAccount_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNextStepDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpNextStepDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblComments);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtComments);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNextStepEmployeeSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnStepsSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboNextStepEmpoyee);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNextStepEmployee);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrentStepEmployeeSubAccounts);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrentStepEmployee);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCommunicationStep);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCommunicationStep);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCustomersSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCustomers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCustomerName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBackTitle);
		base.Name = "frmCustomersFollows";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBackTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UGBDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCustomerName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCustomers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCustomersSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCommunicationStep, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCommunicationStep, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrentStepEmployee, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrentStepEmployeeSubAccounts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNextStepEmployee, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboNextStepEmpoyee, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnStepsSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNextStepEmployeeSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtComments, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblComments, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpNextStepDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNextStepDate, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCommunicationStep).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCustomers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrentStepEmployeeSubAccounts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboNextStepEmpoyee).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpNextStepDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtComments).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
