using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.CustomsClearence;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.CustomsClearence.Transactions;

public class frmOperationExpenses : frmHeaderDetails
{
	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private DataTable dtOperationsNo;

	private DataTable dtExpenses;

	private DataTable dtOperationExpenses;

	private ValueList vlExpenses = new ValueList();

	private ValueList vlAccounts = new ValueList();

	private ValueList vlEmployees = new ValueList();

	private string OperationID = "0";

	private string CustodyAccountID = "";

	private IContainer components = null;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	public UltraButton btnSubAccountSearch;

	private UltraComboEditor cboSubAccountName;

	private UltraLabel lblSubAccountName;

	public UltraButton btnAccountSearch;

	private UltraComboEditor cboAccountName;

	private UltraLabel lblAccountName;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblOperationsNo;

	private UltraComboEditor cboOperationNo;

	public UltraButton btnOperationNoSearch;

	private UltraLabel lblReceiptNo;

	private UltraTextEditor txtReceiptNo;

	private UltraCheckEditor chkOnClientAccount;

	private UltraLabel lblExpenses;

	private UltraComboEditor cboExpenseName;

	private UltraLabel lblAmount;

	private UltraTextEditor txtAmount;

	private UltraGroupBox ultraGroupBox1;

	protected internal UltraGrid ULGDataExpenses;

	public frmOperationExpenses()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		InitializeComponent();
		TableName = "CST_OperationsExpenses";
		IDCol = "OperationExpenseID";
		NoCol = "OperationExpenseNo";
		DateCol = "OperationExpenseDate";
	}

	public frmOperationExpenses(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public frmOperationExpenses(string OPERATIONID)
		: this()
	{
		OperationID = OPERATIONID;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboAccountName, dtAccounts, "AccountID", "Name");
		CustodyAccountID = GlobalVariables.dtSystemAccounts.Select(" AccountNameEn ='CustodyAccount' ")[0]["AccountID"].ToString();
		if (CustodyAccountID != "")
		{
			dtSubAccounts = SubAccounts.SelectByAccountID(CustodyAccountID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboSubAccountName, dtSubAccounts, "SubAccountID", "Name");
			vlEmployees.ValueListItems.Clear();
			for (int i = 0; i < dtSubAccounts.Rows.Count; i++)
			{
				vlEmployees.ValueListItems.Add(dtSubAccounts.Rows[i]["SubAccountID"], dtSubAccounts.Rows[i]["Name"].ToString());
			}
			dtOperationsNo = Operations.FillCombo(GlobalVariables.BranchIDs);
			GlobalFunctions.FillCombo(cboOperationNo, dtOperationsNo, "OperationID", "OperationNo");
			dtExpenses = Expenses.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboExpenseName, dtExpenses, "ExpenseID", "ExpenseName");
			vlExpenses.ValueListItems.Clear();
			for (int j = 0; j < dtExpenses.Rows.Count; j++)
			{
				vlExpenses.ValueListItems.Add(dtExpenses.Rows[j]["ExpenseID"], dtExpenses.Rows[j]["ExpenseName"].ToString());
			}
			dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlAccounts.ValueListItems.Clear();
			for (int k = 0; k < dtAccounts.Rows.Count; k++)
			{
				vlAccounts.ValueListItems.Add(dtAccounts.Rows[k]["AccountID"], dtAccounts.Rows[k]["Name"].ToString());
			}
		}
		else
		{
			GlobalVariables.InformationMB.Show("حساب العهد غير معرف فى حسابات النظام", "Custody Account Not Defined in System Account");
		}
	}

	public override void FillData()
	{
		if (OperationID != "0")
		{
			drMaster = null;
			btnAddClick();
			((TextEditorControlBase)cboOperationNo).Value = OperationID;
			return;
		}
		if (RowID == "")
		{
			drMaster = null;
			DisplayData();
			return;
		}
		DataTable dataTable = OperationsExpenses.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
		if (dataTable.Rows.Count > 0)
		{
			drMaster = dataTable.Rows[0];
		}
		else
		{
			drMaster = null;
		}
		DisplayData();
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (drMaster != null)
		{
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["OperationExpenseNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["OperationExpenseDate"];
			((TextEditorControlBase)cboOperationNo).Value = drMaster["OperationID"];
			((TextEditorControlBase)cboExpenseName).Value = drMaster["ExpenseID"];
			((TextEditorControlBase)cboAccountName).Value = drMaster["AccountID"];
			((TextEditorControlBase)cboSubAccountName).Value = drMaster["SubAccountID"];
			((Control)(object)txtReceiptNo).Text = drMaster["ReceiptNo"].ToString();
			((UltraToggleEditorBase)chkOnClientAccount).Checked = bool.Parse(drMaster["OnClientAccount"].ToString());
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtAmount).Text = decimal.Parse(drMaster["Amount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtOperationExpenses = OperationsExpenses.SelectByOperationID(drMaster["OperationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGDataExpenses).DataSource = dtOperationExpenses;
			InitGridExpenses();
			if (drMaster["Approved"].Equals(true))
			{
				((Control)(object)btnDelete).Enabled = false;
				((Control)(object)btnUpdate).Enabled = false;
			}
			else
			{
				((Control)(object)btnDelete).Enabled = true;
				((Control)(object)btnUpdate).Enabled = true;
			}
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
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboOperationNo).ReadOnly = NavMode || Updating || OperationID != "0";
		((EditorButtonControlBase)cboAccountName).ReadOnly = true;
		((Control)(object)chkOnClientAccount).Enabled = !NavMode;
		((EditorButtonControlBase)cboSubAccountName).ReadOnly = NavMode;
		((EditorButtonControlBase)cboExpenseName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtReceiptNo).ReadOnly = NavMode;
		((Control)(object)btnSubAccountSearch).Visible = !NavMode;
		((Control)(object)btnOperationNoSearch).Visible = Adding && OperationID == "0";
		((Control)(object)btnPrint).Visible = false;
		if (Adding)
		{
			DataView dataView = new DataView(dtOperationsNo);
			dataView.RowFilter = " Approved = 0 And BranchID=" + GlobalVariables.CurrentBranchID;
			DataTable dt = dataView.ToTable();
			int num = 0;
			if (cboOperationNo.SelectedIndex > -1)
			{
				num = int.Parse(((TextEditorControlBase)cboOperationNo).Value.ToString());
			}
			GlobalFunctions.FillCombo(cboOperationNo, dt, "OperationID", "OperationNo");
			if (num != 0)
			{
				((TextEditorControlBase)cboOperationNo).Value = num;
			}
		}
		else
		{
			int num2 = 0;
			if (cboOperationNo.SelectedIndex > -1)
			{
				num2 = int.Parse(((TextEditorControlBase)cboOperationNo).Value.ToString());
			}
			GlobalFunctions.FillCombo(cboOperationNo, dtOperationsNo, "OperationID", "OperationNo");
			if (num2 != 0)
			{
				((TextEditorControlBase)cboOperationNo).Value = num2;
			}
		}
		if (cboAccountName.SelectedIndex != -1)
		{
			int num3 = 0;
			if (cboSubAccountName.SelectedIndex != -1)
			{
				num3 = int.Parse(((TextEditorControlBase)cboSubAccountName).Value.ToString());
			}
			DataView dataView2 = new DataView(dtSubAccounts);
			dataView2.RowFilter = "AccountID=" + ((TextEditorControlBase)cboAccountName).Value.ToString();
			dataView2.RowStateFilter = DataViewRowState.CurrentRows;
			cboSubAccountName.DataSource = dataView2;
			if (num3 != 0)
			{
				((TextEditorControlBase)cboSubAccountName).Value = num3;
			}
		}
	}

	public override void ClearControls()
	{
		base.ClearControls();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? OperationsExpenses.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)cboAccountName).Value = CustodyAccountID;
		cboSubAccountName.SelectedIndex = -1;
		cboExpenseName.SelectedIndex = -1;
		((TextEditorControlBase)txtReceiptNo).Clear();
		((TextEditorControlBase)txtNotes).Clear();
		((UltraToggleEditorBase)chkOnClientAccount).Checked = false;
		((Control)(object)txtAmount).Text = "0";
		((UltraToggleEditorBase)chkOnClientAccount).Checked = false;
		if (OperationID != "0")
		{
			((TextEditorControlBase)cboOperationNo).Value = OperationID;
		}
		if (((TextEditorControlBase)cboOperationNo).Value == null)
		{
			((UltraGridBase)ULGDataExpenses).DataSource = null;
			return;
		}
		dtOperationExpenses = OperationsExpenses.SelectByOperationID(((TextEditorControlBase)cboOperationNo).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGDataExpenses).DataSource = dtOperationExpenses;
		InitGridExpenses();
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
		if (Main.CheckForValueByBranchIDAndFiscalYearID("CST_OperationsExpenses", "OperationExpenseNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["OperationExpenseNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = OperationsExpenses.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Voucher Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByBranchID;
		}
		else
		{
			if (((Control)(object)txtAmount).Text == "" || decimal.Parse(((Control)(object)txtAmount).Text) <= 0m)
			{
				GlobalVariables.InformationMB.Show("إجمالى القيمة لابد ان يكون اكبر من الصفر", "Total Value Must Be Greater Than Zero");
				((TextEditorControlBase)txtAmount).Focus();
				return false;
			}
			if (cboOperationNo.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار رقم العملية" : "Please Select Operation No");
				((TextEditorControlBase)cboOperationNo).Focus();
				cboOperationNo.DropDown();
				return false;
			}
		}
		if (cboExpenseName.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار المصروف", "Please Select Expense Item");
			((TextEditorControlBase)cboExpenseName).Focus();
			return false;
		}
		if (cboAccountName.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الحساب" : "Please Select Account Name");
			((TextEditorControlBase)cboAccountName).Focus();
			cboAccountName.DropDown();
			return false;
		}
		if (cboSubAccountName.SelectedIndex == -1 && dtSubAccounts.Select(" AccountID = " + ((TextEditorControlBase)cboAccountName).Value.ToString()).Length != 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الحساب التحليلى" : "Please Select SubAccount Name");
			((TextEditorControlBase)cboSubAccountName).Focus();
			cboSubAccountName.DropDown();
			return false;
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			OperationsExpenses.GenerateJvs(OperationsExpenses.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboExpenseName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboExpenseName).Value.ToString(), (cboOperationNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOperationNo).Value.ToString(), ((Control)(object)txtReceiptNo).Text, (((Control)(object)txtAmount).Text == "") ? "0" : ((Control)(object)txtAmount).Text, (cboAccountName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAccountName).Value.ToString(), (cboSubAccountName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccountName).Value.ToString(), ((UltraToggleEditorBase)chkOnClientAccount).Checked ? "1" : "0", ((Control)(object)txtNotes).Text, "Null", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID).ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
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
			OperationsExpenses.GenerateJvs(OperationsExpenses.Insert_Update(drMaster["OperationExpenseID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboExpenseName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboExpenseName).Value.ToString(), (cboOperationNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOperationNo).Value.ToString(), ((Control)(object)txtReceiptNo).Text, (((Control)(object)txtAmount).Text == "") ? "0" : ((Control)(object)txtAmount).Text, (cboAccountName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAccountName).Value.ToString(), (cboSubAccountName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccountName).Value.ToString(), ((UltraToggleEditorBase)chkOnClientAccount).Checked ? "1" : "0", ((Control)(object)txtNotes).Text, (drMaster["JVID"] == DBNull.Value) ? "Null" : drMaster["JVID"].ToString(), bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID).ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
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
			OperationsExpenses.DeleteVirtual(drMaster["OperationExpenseID"].ToString(), GlobalVariables.UserID);
			if (drMaster["JVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["JVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
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
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboAccountName, dtAccounts, "AccountID", "Name");
		CustodyAccountID = GlobalVariables.dtSystemAccounts.Select(" AccountNameEn ='CustodyAccount' ")[0]["AccountID"].ToString();
		if (CustodyAccountID != "")
		{
			dtSubAccounts = SubAccounts.SelectByAccountID(CustodyAccountID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboSubAccountName, dtSubAccounts, "SubAccountID", "Name");
			dtOperationsNo = Operations.FillCombo(GlobalVariables.BranchIDs);
			GlobalFunctions.FillCombo(cboOperationNo, dtOperationsNo, "OperationID", "OperationNo");
			dtExpenses = Expenses.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboExpenseName, dtExpenses, "ExpenseID", "ExpenseName");
		}
		else
		{
			GlobalVariables.InformationMB.Show("حساب العهد غير معرف فى حسابات النظام", "Custody Account Not Defined in System Account");
		}
	}

	public override void btnPrintClick()
	{
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.CSTOperationsExpensesReport(IsFromServer: false);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["OperationExpenseID"].ToString();
			FillData();
		}
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void cboAccountName_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.Accounts(IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboAccountName).Value = num;
			}
		}
	}

	private void btnAccountSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Accounts(IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboAccountName).Value = num;
		}
	}

	private void cboAccountName_ValueChanged(object sender, EventArgs e)
	{
		if (cboAccountName.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtSubAccounts);
			dataView.RowFilter = "AccountID=" + ((TextEditorControlBase)cboAccountName).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboSubAccountName.DataSource = dataView;
		}
	}

	private void cboSubAccountName_KeyDown(object sender, KeyEventArgs e)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if ((Adding || Updating) && e.KeyValue == 119 && CustodyAccountID != "")
		{
			int num = SearchFunctions.SubAccounts(CustodyAccountID, IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)(UltraComboEditor)sender).Value = num;
			}
		}
	}

	private void btnSubAccountSearch_Click(object sender, EventArgs e)
	{
		if ((Adding || Updating) && CustodyAccountID != "")
		{
			int num = SearchFunctions.SubAccounts(CustodyAccountID, IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboSubAccountName).Value = num;
			}
		}
	}

	private void textBox_Enter(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		((Control)(UltraTextEditor)sender).Select();
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = OperationsExpenses.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void cboOperationNo_ValueChanged(object sender, EventArgs e)
	{
		if (Adding && ((TextEditorControlBase)cboOperationNo).Value != null)
		{
			dtOperationExpenses = OperationsExpenses.SelectByOperationID(((TextEditorControlBase)cboOperationNo).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGDataExpenses).DataSource = dtOperationExpenses;
			InitGridExpenses();
		}
	}

	public void InitGridExpenses()
	{
		GlobalFunctions.PrepareGrid(ULGDataExpenses);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["OperationExpenseID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["OperationExpenseNo"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["OperationExpenseDate"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.1);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExpenseID"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.1);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["AccountID"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.12);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.12);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ReceiptNo"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.1);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Amount"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.1);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["OnClientAccount"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.1);
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGDataExpenses).Width * 0.16);
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["OperationExpenseNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم المصروف" : "Expense No");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["OperationExpenseDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExpenseID"].Header).Caption = (GlobalVariables.IsArabic ? "المصروف" : "Expense");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["AccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب" : "Account");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب التحليلى" : "SubAccount");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ReceiptNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الايصال" : "Receipt No");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Amount"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة" : "Amount");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["OnClientAccount"].Header).Caption = (GlobalVariables.IsArabic ? "على حساب العميل" : "On Client Account");
		((HeaderBase)((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["OperationExpenseNo"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["OperationExpenseDate"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExpenseID"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["AccountID"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ReceiptNo"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Amount"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["OnClientAccount"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["ExpenseID"].ValueList = (IValueList)(object)vlExpenses;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["AccountID"].ValueList = (IValueList)(object)vlAccounts;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlEmployees;
		((UltraGridBase)ULGDataExpenses).DisplayLayout.Bands[0].Columns["Amount"].DefaultCellValue = 0;
	}

	private void ULGDataExpenses_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGDataExpenses).ActiveRow).Selected = true;
	}

	private void btnOperationNoSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.CSTOperationsSearch(0, IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboOperationNo).Value = num;
		}
	}

	public override void btnDeleteClick()
	{
		if (drMaster == null)
		{
			return;
		}
		RowID = drMaster[IDCol].ToString();
		if (!CanDelete)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		if (!CanModifyOtherBranch)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفرع آخر", "Cannot Update This Transaction Because It Related to Another Branch ");
			return;
		}
		if (ClosedPeriod)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Delete This Transaction Because It Related to ClosedPeriod ");
			return;
		}
		if (((UltraToggleEditorBase)chkOnClientAccount).Checked && InvoicesExpenses.SelectByOperationExpenseID(drMaster["OperationExpenseID"].ToString(), GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لأنها مسدده", "Cannot Delete This Transaction Because It Was Paid");
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Are You Sure You want to Delete this Data?");
		DataSaved = true;
		if (GlobalVariables.MessageBoxResult == 'Y')
		{
			DeleteData();
			if (DataSaved)
			{
				FillData();
				drMaster = null;
			}
		}
	}

	public override void btnUpdateClick()
	{
		if (drMaster != null)
		{
			RowID = drMaster[IDCol].ToString();
			if (!CanUpdate)
			{
				GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
				return;
			}
			if (!CanModifyOtherBranch)
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لإنها تابعة لفرع آخر", "Cannot Update This Transaction Because It Related to Another Branch ");
				return;
			}
			if (ClosedPeriod)
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Update This Transaction Because It Related to ClosedPeriod ");
				return;
			}
			if (((UltraToggleEditorBase)chkOnClientAccount).Checked && InvoicesExpenses.SelectByOperationExpenseID(drMaster["OperationExpenseID"].ToString(), GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لأنها مسدده", "Cannot Update This Transaction Because It Was Paid");
				return;
			}
			Updating = true;
			SetControls(NavMode: false);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.CustomsClearence.Transactions.frmOperationExpenses));
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
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.btnSubAccountSearch = new UltraButton();
		this.cboSubAccountName = new UltraComboEditor();
		this.lblSubAccountName = new UltraLabel();
		this.btnAccountSearch = new UltraButton();
		this.cboAccountName = new UltraComboEditor();
		this.lblAccountName = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblOperationsNo = new UltraLabel();
		this.cboOperationNo = new UltraComboEditor();
		this.btnOperationNoSearch = new UltraButton();
		this.lblReceiptNo = new UltraLabel();
		this.txtReceiptNo = new UltraTextEditor();
		this.chkOnClientAccount = new UltraCheckEditor();
		this.lblExpenses = new UltraLabel();
		this.cboExpenseName = new UltraComboEditor();
		this.lblAmount = new UltraLabel();
		this.txtAmount = new UltraTextEditor();
		this.ultraGroupBox1 = new UltraGroupBox();
		this.ULGDataExpenses = new UltraGrid();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccountName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAccountName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboOperationNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtReceiptNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkOnClientAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboExpenseName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraGroupBox1).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataExpenses).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.UGBDetails, "UGBDetails");
		resources.ApplyResources(base.ULGData, "ULGData");
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
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
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
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.btnSearch, "btnSearch");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.btnCopyTo, "btnCopyTo");
		resources.ApplyResources(base.btnSetting, "btnSetting");
		resources.ApplyResources(base.btnAttachFile, "btnAttachFile");
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
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.btnSubAccountSearch, "btnSubAccountSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance17");
		((ControlBase)this.btnSubAccountSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).Name = "btnSubAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).Click += new System.EventHandler(btnSubAccountSearch_Click);
		resources.ApplyResources(this.cboSubAccountName, "cboSubAccountName");
		this.cboSubAccountName.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSubAccountName).Name = "cboSubAccountName";
		((System.Windows.Forms.Control)(object)this.cboSubAccountName).KeyDown += new System.Windows.Forms.KeyEventHandler(cboSubAccountName_KeyDown);
		resources.ApplyResources(this.lblSubAccountName, "lblSubAccountName");
		this.lblSubAccountName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSubAccountName).Name = "lblSubAccountName";
		((ControlBase)this.lblSubAccountName).WrapText = false;
		resources.ApplyResources(this.btnAccountSearch, "btnAccountSearch");
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val10, "appearance18");
		((ControlBase)this.btnAccountSearch).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.btnAccountSearch).Name = "btnAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnAccountSearch).Click += new System.EventHandler(btnAccountSearch_Click);
		resources.ApplyResources(this.cboAccountName, "cboAccountName");
		this.cboAccountName.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboAccountName).Name = "cboAccountName";
		((TextEditorControlBase)this.cboAccountName).ValueChanged += new System.EventHandler(cboAccountName_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboAccountName).KeyDown += new System.Windows.Forms.KeyEventHandler(cboAccountName_KeyDown);
		resources.ApplyResources(this.lblAccountName, "lblAccountName");
		this.lblAccountName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAccountName).Name = "lblAccountName";
		((ControlBase)this.lblAccountName).WrapText = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblOperationsNo, "lblOperationsNo");
		this.lblOperationsNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOperationsNo).Name = "lblOperationsNo";
		((ControlBase)this.lblOperationsNo).WrapText = false;
		resources.ApplyResources(this.cboOperationNo, "cboOperationNo");
		this.cboOperationNo.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboOperationNo).Name = "cboOperationNo";
		((TextEditorControlBase)this.cboOperationNo).ValueChanged += new System.EventHandler(cboOperationNo_ValueChanged);
		resources.ApplyResources(this.btnOperationNoSearch, "btnOperationNoSearch");
		((AppearanceBase)val11).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val11, "appearance19");
		((ControlBase)this.btnOperationNoSearch).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.btnOperationNoSearch).Name = "btnOperationNoSearch";
		((System.Windows.Forms.Control)(object)this.btnOperationNoSearch).Click += new System.EventHandler(btnOperationNoSearch_Click);
		resources.ApplyResources(this.lblReceiptNo, "lblReceiptNo");
		this.lblReceiptNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReceiptNo).Name = "lblReceiptNo";
		((ControlBase)this.lblReceiptNo).WrapText = false;
		resources.ApplyResources(this.txtReceiptNo, "txtReceiptNo");
		((System.Windows.Forms.Control)(object)this.txtReceiptNo).Name = "txtReceiptNo";
		resources.ApplyResources(this.chkOnClientAccount, "chkOnClientAccount");
		((System.Windows.Forms.Control)(object)this.chkOnClientAccount).Name = "chkOnClientAccount";
		resources.ApplyResources(this.lblExpenses, "lblExpenses");
		this.lblExpenses.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExpenses).Name = "lblExpenses";
		((ControlBase)this.lblExpenses).WrapText = false;
		resources.ApplyResources(this.cboExpenseName, "cboExpenseName");
		this.cboExpenseName.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboExpenseName).Name = "cboExpenseName";
		resources.ApplyResources(this.lblAmount, "lblAmount");
		this.lblAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAmount).Name = "lblAmount";
		((ControlBase)this.lblAmount).WrapText = false;
		resources.ApplyResources(this.txtAmount, "txtAmount");
		((System.Windows.Forms.Control)(object)this.txtAmount).Name = "txtAmount";
		((System.Windows.Forms.Control)(object)this.txtAmount).Enter += new System.EventHandler(textBox_Enter);
		((System.Windows.Forms.Control)(object)this.txtAmount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraGroupBox1, "ultraGroupBox1");
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataExpenses);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Name = "ultraGroupBox1";
		resources.ApplyResources(this.ULGDataExpenses, "ULGDataExpenses");
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val12, "appearance12");
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val13).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val13).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val13, "appearance13");
		((AppearanceBase)val13).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val14).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val14, "appearance14");
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val14;
		((AppearanceBase)val15).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val15).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val15).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val15, "appearance15");
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val16).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val16).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val16).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val16, "appearance16");
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val16;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataExpenses).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataExpenses).Name = "ULGDataExpenses";
		((UltraControlBase)this.ULGDataExpenses).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataExpenses.AfterEnterEditMode += new System.EventHandler(ULGDataExpenses_AfterEnterEditMode);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraGroupBox1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExpenses);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboExpenseName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkOnClientAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReceiptNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtReceiptNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOperationsNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboOperationNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOperationNoSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSubAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSubAccountName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSubAccountName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboAccountName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAccountName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmOperationExpenses";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAccountName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboAccountName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSubAccountName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSubAccountName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSubAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOperationNoSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboOperationNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOperationsNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtReceiptNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReceiptNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkOnClientAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboExpenseName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExpenses, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraGroupBox1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
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
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccountName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAccountName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboOperationNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtReceiptNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkOnClientAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboExpenseName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraGroupBox1).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataExpenses).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
