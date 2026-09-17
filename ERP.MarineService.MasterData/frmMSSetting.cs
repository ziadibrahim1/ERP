using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.MarineService;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;

namespace ERP.MarineService.MasterData;

public class frmMSSetting : frmBase
{
	private DataTable dtMSSetting;

	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private DataTable dtSettingsVisaBanks;

	private DataTable dtBranches;

	private ValueList vlVisaBankAccountID = new ValueList();

	private ValueList vlVisaBankSubAccountID = new ValueList();

	private ValueList vlVisaExpenseAccountID = new ValueList();

	private ValueList vlVisaExpenseSubAccountID = new ValueList();

	private ValueList vlBranches = new ValueList();

	private IContainer components = null;

	public UltraLabel lblTitle2;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	private UltraButton btnSave;

	private OpenFileDialog ofdPicture;

	private UltraComboEditor cboCustodyAccount;

	private UltraLabel lblCustodyAccount;

	private UltraComboEditor cboEDHSAccount;

	private UltraLabel lblEDHSAccount;

	private UltraLabel lblEDHSExpenseAddAmount;

	private UltraLabel lblEDHSExpenseIssueAmount;

	private UltraNumericEditor UNEDHSExpenseIssueAmount;

	private UltraNumericEditor UNEDHSExpenseAddAmount;

	private UltraTextEditor txtOpVoyageNoEnd;

	private UltraLabel ultraLabel4;

	private UltraTextEditor txtOpVoyageNoStart;

	private UltraLabel ultraLabel3;

	private UltraTextEditor txtExpManifestEnd;

	private UltraLabel ultraLabel1;

	private UltraTextEditor txtExpManifestStart;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel5;

	private UltraTextEditor txtImpManifestStart;

	private UltraLabel ultraLabel6;

	private UltraTextEditor txtImpManifestEnd;

	private UltraTextEditor txtInvoiceMessage;

	private UltraLabel lblInvoiceMessage;

	public UltraTabControl tabItemType;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl tabItem;

	private UltraTabPageControl ultraTabPageControl2;

	private NumericUpDown txtActualPeriod;

	private UltraLabel lblVisaActualPeriod;

	private NumericUpDown txtValidPeriod;

	private UltraLabel ultraLabel14;

	private UltraLabel ultraLabel16;

	private UltraLabel lblVisaValidPeriod;

	public UltraButton btnEDHSAccountSearch;

	public UltraButton btnCustodyAccountSearch;

	public UltraButton btnPrintSupplierAccount;

	public UltraButton btnPrintSupplierSubAccount;

	private UltraLabel lblPrintSupplierAccount;

	private UltraComboEditor cboPrintSupplierAccount;

	private UltraLabel ultraLabel7;

	private UltraNumericEditor txtPrintOutCost;

	private UltraComboEditor cboPrintSupplierSubAccount;

	private UltraLabel lblPrintOutCost;

	public UltraGrid ULGData;

	private NumericUpDown numVesselAlertBefore;

	private UltraLabel ultraLabel8;

	private NumericUpDown numVesselStayValidPeriod;

	private UltraLabel ultraLabel9;

	private UltraLabel ultraLabel10;

	private UltraLabel ultraLabel11;

	private UltraTextEditor txtInvoiceMSG2;

	private UltraLabel lblInvoiceMsg2;

	public frmMSSetting()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		InitializeComponent();
	}

	public override void PrepareData()
	{
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCustodyAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboEDHSAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboPrintSupplierAccount, dtAccounts, "AccountID", "Name");
		vlVisaBankAccountID.ValueListItems.Clear();
		vlVisaExpenseAccountID.ValueListItems.Clear();
		for (int i = 0; i < dtAccounts.Rows.Count; i++)
		{
			vlVisaBankAccountID.ValueListItems.Add((object)dtAccounts.Rows[i]["AccountID"].ToString(), dtAccounts.Rows[i]["Name"].ToString());
			vlVisaExpenseAccountID.ValueListItems.Add((object)dtAccounts.Rows[i]["AccountID"].ToString(), dtAccounts.Rows[i]["Name"].ToString());
		}
		dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboPrintSupplierSubAccount, dtSubAccounts, "SubAccountID", "Name");
		vlVisaBankSubAccountID.ValueListItems.Clear();
		vlVisaExpenseSubAccountID.ValueListItems.Clear();
		for (int j = 0; j < dtSubAccounts.Rows.Count; j++)
		{
			vlVisaBankSubAccountID.ValueListItems.Add(dtSubAccounts.Rows[j]["SubAccountID"], dtSubAccounts.Rows[j]["Name"].ToString());
			vlVisaExpenseSubAccountID.ValueListItems.Add(dtSubAccounts.Rows[j]["SubAccountID"], dtSubAccounts.Rows[j]["Name"].ToString());
		}
		dtBranches = Branches.Select("-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlBranches.ValueListItems.Clear();
		for (int k = 0; k < dtBranches.Rows.Count; k++)
		{
			vlBranches.ValueListItems.Add(dtBranches.Rows[k]["BranchID"], dtBranches.Rows[k][GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn"].ToString());
		}
		FillData();
		dtSettingsVisaBanks = SettingsVisaBanks.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dtSettingsVisaBanks;
		InitGrid();
		for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; l++)
		{
			if (((UltraGridBase)ULGData).Rows[l].Cells["VisaBankAccountID"].Value != DBNull.Value)
			{
				int accountID = int.Parse(((UltraGridBase)ULGData).Rows[l].Cells["VisaBankAccountID"].Value.ToString());
				ValueList subAccountValueList = getSubAccountValueList(accountID);
				((UltraGridBase)ULGData).Rows[l].Cells["VisaBankSubAccountID"].ValueList = (IValueList)(object)subAccountValueList;
				if (((DisposableObjectCollectionBase)subAccountValueList.ValueListItems).Count == 0)
				{
					((UltraGridBase)ULGData).Rows[l].Cells["VisaBankSubAccountID"].Value = DBNull.Value;
				}
			}
			else if (((UltraGridBase)ULGData).Rows[l].Cells["VisaExpenseAccountID"].Value != DBNull.Value)
			{
				int accountID2 = int.Parse(((UltraGridBase)ULGData).Rows[l].Cells["VisaExpenseAccountID"].Value.ToString());
				ValueList subAccountValueList2 = getSubAccountValueList(accountID2);
				((UltraGridBase)ULGData).Rows[l].Cells["VisaExpenseSubAccountID"].ValueList = (IValueList)(object)subAccountValueList2;
				if (((DisposableObjectCollectionBase)subAccountValueList2.ValueListItems).Count == 0)
				{
					((UltraGridBase)ULGData).Rows[l].Cells["VisaExpenseSubAccountID"].Value = DBNull.Value;
				}
			}
		}
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaBankNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaBankNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaBankAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaBankSubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaExpenseAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaExpenseSubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaCost"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UrgentVisaCost"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaBankNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "اسم البنك بالعربية" : "Bank Name Ar");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaBankNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "اسم البنك بالانجليزية" : "Bank Name En");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaBankAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "حساب البنك" : "Bank Account");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaBankSubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب التحليلى للبنك" : "Bank SubAccount");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaExpenseAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "حساب المصروف" : "Expense Account");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaExpenseSubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب التحليلى للمصروف" : "Expense SubAccount");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaCost"].Header).Caption = (GlobalVariables.IsArabic ? "تكلفة عادية" : "Normal Cost");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UrgentVisaCost"].Header).Caption = (GlobalVariables.IsArabic ? "تكلفة مستعجل" : "Urgent Cost");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchID"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaBankNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaBankNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaBankAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaBankSubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaExpenseAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaExpenseSubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaCost"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UrgentVisaCost"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaBankAccountID"].ValueList = (IValueList)(object)vlVisaBankAccountID;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaBankSubAccountID"].ValueList = (IValueList)(object)vlVisaBankSubAccountID;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaExpenseAccountID"].ValueList = (IValueList)(object)vlVisaExpenseAccountID;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaExpenseSubAccountID"].ValueList = (IValueList)(object)vlVisaExpenseSubAccountID;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchID"].ValueList = (IValueList)(object)vlBranches;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SettingVisaBankID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaCost"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UrgentVisaCost"].DefaultCellValue = 0;
	}

	private void FillData()
	{
		dtMSSetting = BusinessLayer.MarineService.Settings.SelectByBranchID(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dtMSSetting.Rows.Count > 0)
		{
			((Control)(object)txtImpManifestEnd).Text = dtMSSetting.Rows[0]["ImportManifestEndNo"].ToString();
			((Control)(object)txtImpManifestStart).Text = dtMSSetting.Rows[0]["ImportManifestStartNo"].ToString();
			((Control)(object)txtExpManifestEnd).Text = dtMSSetting.Rows[0]["ExportManifestEndNo"].ToString();
			((Control)(object)txtExpManifestStart).Text = dtMSSetting.Rows[0]["ExportManifestStartNo"].ToString();
			((Control)(object)txtOpVoyageNoEnd).Text = dtMSSetting.Rows[0]["OperationsVoyageNoEndNo"].ToString();
			((Control)(object)txtOpVoyageNoStart).Text = dtMSSetting.Rows[0]["OperationsVoyageNoStartNo"].ToString();
			UNEDHSExpenseAddAmount.Value = dtMSSetting.Rows[0]["EDHSExpenseAddAmount"].ToString();
			UNEDHSExpenseIssueAmount.Value = dtMSSetting.Rows[0]["EDHSExpenseIssueAmount"].ToString();
			((TextEditorControlBase)cboCustodyAccount).Value = dtMSSetting.Rows[0]["CustodyAccountID"];
			((TextEditorControlBase)cboEDHSAccount).Value = dtMSSetting.Rows[0]["EDHSAccountID"];
			((Control)(object)txtInvoiceMessage).Text = dtMSSetting.Rows[0]["InvoiceMessage"].ToString();
			((Control)(object)txtInvoiceMSG2).Text = dtMSSetting.Rows[0]["InvoiceMessage2"].ToString();
			numVesselStayValidPeriod.Text = dtMSSetting.Rows[0]["VesselStayValidityPeriodDays"].ToString();
			numVesselAlertBefore.Text = dtMSSetting.Rows[0]["VesselStayAlertBeforeDays"].ToString();
			((TextEditorControlBase)cboPrintSupplierAccount).Value = dtMSSetting.Rows[0]["VisaPrintSupplierAccountID"];
			((TextEditorControlBase)cboPrintSupplierSubAccount).Value = dtMSSetting.Rows[0]["VisaPrintSupplierSubAccountID"];
			txtActualPeriod.Value = ((dtMSSetting.Rows[0]["VisaActualPeriodDays"] == DBNull.Value) ? 1 : int.Parse(dtMSSetting.Rows[0]["VisaActualPeriodDays"].ToString()));
			txtValidPeriod.Value = ((dtMSSetting.Rows[0]["VisaValidPeriodDays"] == DBNull.Value) ? 1 : int.Parse(dtMSSetting.Rows[0]["VisaValidPeriodDays"].ToString()));
			txtPrintOutCost.Value = ((dtMSSetting.Rows[0]["VisaPrintOutCost"] == DBNull.Value) ? ((object)0) : dtMSSetting.Rows[0]["VisaPrintOutCost"]);
		}
	}

	private bool ValidateData()
	{
		if (((TextEditorControlBase)cboPrintSupplierAccount).Value != DBNull.Value && cboPrintSupplierSubAccount.SelectedIndex == -1 && ((DisposableObjectCollectionBase)cboPrintSupplierSubAccount.Items).Count > 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار حساب المورد التحليلى", "Please Select Supplier SubAccountAccount");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["VisaBankNameAr"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال الاسم بالعربية", "Please Enter Name Ar");
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["VisaBankAccountID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار حساب البنك", "Please Select Bank Account");
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["VisaBankSubAccountID"].ValueList != null && ((UltraGridBase)ULGData).Rows[i].Cells["VisaBankSubAccountID"].ValueList.ItemCount > 0 && ((UltraGridBase)ULGData).Rows[i].Cells["VisaBankSubAccountID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار حساب البنك التحليلى", "Please Select Bank SubAccountAccount");
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["VisaExpenseAccountID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار حساب مصروف الفيزا", "Please Select Bank Expense Account");
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["VisaExpenseSubAccountID"].ValueList != null && ((UltraGridBase)ULGData).Rows[i].Cells["VisaExpenseSubAccountID"].ValueList.ItemCount > 0 && ((UltraGridBase)ULGData).Rows[i].Cells["VisaExpenseSubAccountID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار الحساب التحليلى مصروف الفيزا", "Please Select Bank Expense SubAccount");
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال الفرع", "Please Select Branch");
				return false;
			}
		}
		return true;
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (ValidateData())
		{
			int num = BusinessLayer.MarineService.Settings.Insert_Update((dtMSSetting.Rows.Count > 0) ? dtMSSetting.Rows[0]["SettingID"].ToString() : "-1", (cboCustodyAccount.SelectedIndex > -1) ? ((TextEditorControlBase)cboCustodyAccount).Value.ToString() : "Null", (cboEDHSAccount.SelectedIndex > -1) ? ((TextEditorControlBase)cboEDHSAccount).Value.ToString() : "Null", UNEDHSExpenseIssueAmount.Value.ToString(), UNEDHSExpenseAddAmount.Value.ToString(), txtValidPeriod.Value.ToString(), txtActualPeriod.Value.ToString(), (cboPrintSupplierAccount.SelectedIndex > -1) ? ((TextEditorControlBase)cboPrintSupplierAccount).Value.ToString() : "Null", (cboPrintSupplierSubAccount.SelectedIndex > -1) ? ((TextEditorControlBase)cboPrintSupplierSubAccount).Value.ToString() : "Null", txtPrintOutCost.Value.ToString(), ((Control)(object)txtOpVoyageNoStart).Text, ((Control)(object)txtOpVoyageNoEnd).Text, ((Control)(object)txtExpManifestStart).Text, ((Control)(object)txtExpManifestEnd).Text, ((Control)(object)txtImpManifestStart).Text, ((Control)(object)txtImpManifestEnd).Text, ((Control)(object)txtInvoiceMessage).Text, ((Control)(object)txtInvoiceMSG2).Text, numVesselStayValidPeriod.Value.ToString(), numVesselAlertBefore.Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			SettingsVisaBanks.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
			FillData();
			GlobalVariables.InformationMB.Show("تم الحفظ بنجاح ", " Data Saved Successfuly ");
		}
	}

	private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void btnCustodyAccountSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Accounts(IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboCustodyAccount).Value = num;
		}
	}

	private void btnEDHSAccountSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Accounts(IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboEDHSAccount).Value = num;
		}
	}

	private void btnPrintSupplierAccount_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Accounts(IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboPrintSupplierAccount).Value = num;
		}
	}

	private void btnPrintSupplierSubAccount_Click(object sender, EventArgs e)
	{
		if (cboPrintSupplierAccount.SelectedIndex != -1)
		{
			int num = SearchFunctions.SubAccounts(((TextEditorControlBase)cboPrintSupplierAccount).Value.ToString(), IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboPrintSupplierSubAccount).Value = num;
			}
		}
	}

	private void cboPrintSupplierAccount_ValueChanged(object sender, EventArgs e)
	{
		if (cboPrintSupplierAccount.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtSubAccounts);
			dataView.RowFilter = "AccountID=" + ((TextEditorControlBase)cboPrintSupplierAccount).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboPrintSupplierSubAccount.DataSource = dataView;
		}
	}

	private ValueList getSubAccountValueList(int AccountID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtSubAccounts.Select("AccountID=" + AccountID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["SubAccountID"].ToString(), array[i]["Name"].ToString());
		}
		return val;
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "VisaBankAccountID")
		{
			int num = ((vlVisaBankAccountID.SelectedItem != null) ? int.Parse(dtAccounts.Rows[vlVisaBankAccountID.SelectedIndex]["AccountID"].ToString()) : 0);
			if (num != 0)
			{
				e.Cell.Row.Cells["VisaBankSubAccountID"].Value = DBNull.Value;
				e.Cell.Row.Cells["VisaBankSubAccountID"].ValueList = (IValueList)(object)getSubAccountValueList(num);
			}
			else
			{
				e.Cell.Row.Cells["VisaBankSubAccountID"].ValueList = null;
				e.Cell.Row.Cells["VisaBankSubAccountID"].Value = DBNull.Value;
			}
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "VisaExpenseAccountID")
		{
			int num2 = ((vlVisaExpenseAccountID.SelectedItem != null) ? int.Parse(dtAccounts.Rows[vlVisaExpenseAccountID.SelectedIndex]["AccountID"].ToString()) : 0);
			if (num2 != 0)
			{
				e.Cell.Row.Cells["VisaExpenseSubAccountID"].Value = DBNull.Value;
				e.Cell.Row.Cells["VisaExpenseSubAccountID"].ValueList = (IValueList)(object)getSubAccountValueList(num2);
			}
			else
			{
				e.Cell.Row.Cells["VisaExpenseSubAccountID"].ValueList = null;
				e.Cell.Row.Cells["VisaExpenseSubAccountID"].Value = DBNull.Value;
			}
		}
	}

	private void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
			return;
		}
		for (int i = 0; i < e.Rows.Length; i++)
		{
			SettingsVisaBanks.Delete(e.Rows[i].Cells["SettingVisaBankID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
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
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Expected O, but got Unknown
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Expected O, but got Unknown
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
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Expected O, but got Unknown
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Expected O, but got Unknown
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Expected O, but got Unknown
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Expected O, but got Unknown
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Expected O, but got Unknown
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Expected O, but got Unknown
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Expected O, but got Unknown
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Expected O, but got Unknown
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Expected O, but got Unknown
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Expected O, but got Unknown
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Expected O, but got Unknown
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Expected O, but got Unknown
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Expected O, but got Unknown
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Expected O, but got Unknown
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Expected O, but got Unknown
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Expected O, but got Unknown
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Expected O, but got Unknown
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Expected O, but got Unknown
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Expected O, but got Unknown
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Expected O, but got Unknown
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Expected O, but got Unknown
		//IL_1506: Unknown result type (might be due to invalid IL or missing references)
		//IL_1510: Expected O, but got Unknown
		//IL_151e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1528: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.MasterData.frmMSSetting));
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
		Appearance val18 = new Appearance();
		Appearance val19 = new Appearance();
		Appearance val20 = new Appearance();
		Appearance val21 = new Appearance();
		Appearance val22 = new Appearance();
		Appearance val23 = new Appearance();
		Appearance val24 = new Appearance();
		Appearance val25 = new Appearance();
		Appearance val26 = new Appearance();
		UltraTab val27 = new UltraTab();
		UltraTab val28 = new UltraTab();
		this.tabItem = new UltraTabPageControl();
		this.numVesselAlertBefore = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel8 = new UltraLabel();
		this.numVesselStayValidPeriod = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel9 = new UltraLabel();
		this.ultraLabel10 = new UltraLabel();
		this.ultraLabel11 = new UltraLabel();
		this.btnEDHSAccountSearch = new UltraButton();
		this.btnCustodyAccountSearch = new UltraButton();
		this.lblCustodyAccount = new UltraLabel();
		this.txtInvoiceMSG2 = new UltraTextEditor();
		this.txtInvoiceMessage = new UltraTextEditor();
		this.UNEDHSExpenseAddAmount = new UltraNumericEditor();
		this.txtImpManifestEnd = new UltraTextEditor();
		this.cboCustodyAccount = new UltraComboEditor();
		this.txtExpManifestEnd = new UltraTextEditor();
		this.UNEDHSExpenseIssueAmount = new UltraNumericEditor();
		this.ultraLabel6 = new UltraLabel();
		this.lblEDHSAccount = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.cboEDHSAccount = new UltraComboEditor();
		this.txtImpManifestStart = new UltraTextEditor();
		this.lblEDHSExpenseIssueAmount = new UltraLabel();
		this.txtExpManifestStart = new UltraTextEditor();
		this.lblInvoiceMsg2 = new UltraLabel();
		this.lblEDHSExpenseAddAmount = new UltraLabel();
		this.lblInvoiceMessage = new UltraLabel();
		this.ultraLabel3 = new UltraLabel();
		this.ultraLabel5 = new UltraLabel();
		this.txtOpVoyageNoStart = new UltraTextEditor();
		this.ultraLabel2 = new UltraLabel();
		this.ultraLabel4 = new UltraLabel();
		this.txtOpVoyageNoEnd = new UltraTextEditor();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGData = new UltraGrid();
		this.btnPrintSupplierAccount = new UltraButton();
		this.btnPrintSupplierSubAccount = new UltraButton();
		this.lblPrintSupplierAccount = new UltraLabel();
		this.cboPrintSupplierAccount = new UltraComboEditor();
		this.ultraLabel7 = new UltraLabel();
		this.txtPrintOutCost = new UltraNumericEditor();
		this.cboPrintSupplierSubAccount = new UltraComboEditor();
		this.lblPrintOutCost = new UltraLabel();
		this.txtActualPeriod = new System.Windows.Forms.NumericUpDown();
		this.lblVisaActualPeriod = new UltraLabel();
		this.txtValidPeriod = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel14 = new UltraLabel();
		this.ultraLabel16 = new UltraLabel();
		this.lblVisaValidPeriod = new UltraLabel();
		this.lblTitle2 = new UltraLabel();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.btnSave = new UltraButton();
		this.ofdPicture = new System.Windows.Forms.OpenFileDialog();
		this.tabItemType = new UltraTabControl();
		this.ultraTabSharedControlsPage1 = new UltraTabSharedControlsPage();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabItem).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.numVesselAlertBefore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numVesselStayValidPeriod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvoiceMSG2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvoiceMessage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UNEDHSExpenseAddAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtImpManifestEnd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCustodyAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExpManifestEnd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UNEDHSExpenseIssueAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboEDHSAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtImpManifestStart).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExpManifestStart).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOpVoyageNoStart).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOpVoyageNoEnd).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPrintSupplierAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPrintOutCost).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPrintSupplierSubAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtActualPeriod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtValidPeriod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tabItemType).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabItemType).SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.tabItem, "tabItem");
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add(this.numVesselAlertBefore);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel8);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add(this.numVesselStayValidPeriod);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel9);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel10);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel11);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.btnEDHSAccountSearch);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.btnCustodyAccountSearch);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblCustodyAccount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtInvoiceMSG2);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtInvoiceMessage);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.UNEDHSExpenseAddAmount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtImpManifestEnd);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboCustodyAccount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtExpManifestEnd);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.UNEDHSExpenseIssueAmount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblEDHSAccount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboEDHSAccount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtImpManifestStart);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblEDHSExpenseIssueAmount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtExpManifestStart);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblInvoiceMsg2);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblEDHSExpenseAddAmount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblInvoiceMessage);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtOpVoyageNoStart);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtOpVoyageNoEnd);
		((System.Windows.Forms.Control)(object)this.tabItem).Name = "tabItem";
		resources.ApplyResources(this.numVesselAlertBefore, "numVesselAlertBefore");
		this.numVesselAlertBefore.Maximum = new decimal(new int[4] { 3600, 0, 0, 0 });
		this.numVesselAlertBefore.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.numVesselAlertBefore.Name = "numVesselAlertBefore";
		this.numVesselAlertBefore.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		resources.ApplyResources(this.ultraLabel8, "ultraLabel8");
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.ultraLabel8).Appearance = (AppearanceBase)(object)val;
		this.ultraLabel8.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel8).Name = "ultraLabel8";
		((ControlBase)this.ultraLabel8).WrapText = false;
		resources.ApplyResources(this.numVesselStayValidPeriod, "numVesselStayValidPeriod");
		this.numVesselStayValidPeriod.Maximum = new decimal(new int[4] { 3600, 0, 0, 0 });
		this.numVesselStayValidPeriod.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.numVesselStayValidPeriod.Name = "numVesselStayValidPeriod";
		this.numVesselStayValidPeriod.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		resources.ApplyResources(this.ultraLabel9, "ultraLabel9");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.ultraLabel9).Appearance = (AppearanceBase)(object)val2;
		this.ultraLabel9.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel9).Name = "ultraLabel9";
		((ControlBase)this.ultraLabel9).WrapText = false;
		resources.ApplyResources(this.ultraLabel10, "ultraLabel10");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.ultraLabel10).Appearance = (AppearanceBase)(object)val3;
		this.ultraLabel10.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel10).Name = "ultraLabel10";
		((ControlBase)this.ultraLabel10).WrapText = false;
		resources.ApplyResources(this.ultraLabel11, "ultraLabel11");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.ultraLabel11).Appearance = (AppearanceBase)(object)val4;
		this.ultraLabel11.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel11).Name = "ultraLabel11";
		((ControlBase)this.ultraLabel11).WrapText = false;
		resources.ApplyResources(this.btnEDHSAccountSearch, "btnEDHSAccountSearch");
		((AppearanceBase)val5).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.btnEDHSAccountSearch).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.btnEDHSAccountSearch).Name = "btnEDHSAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnEDHSAccountSearch).Click += new System.EventHandler(btnEDHSAccountSearch_Click);
		resources.ApplyResources(this.btnCustodyAccountSearch, "btnCustodyAccountSearch");
		((AppearanceBase)val6).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.btnCustodyAccountSearch).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.btnCustodyAccountSearch).Name = "btnCustodyAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnCustodyAccountSearch).Click += new System.EventHandler(btnCustodyAccountSearch_Click);
		resources.ApplyResources(this.lblCustodyAccount, "lblCustodyAccount");
		this.lblCustodyAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCustodyAccount).Name = "lblCustodyAccount";
		((ControlBase)this.lblCustodyAccount).WrapText = false;
		resources.ApplyResources(this.txtInvoiceMSG2, "txtInvoiceMSG2");
		((System.Windows.Forms.Control)(object)this.txtInvoiceMSG2).Name = "txtInvoiceMSG2";
		resources.ApplyResources(this.txtInvoiceMessage, "txtInvoiceMessage");
		((System.Windows.Forms.Control)(object)this.txtInvoiceMessage).Name = "txtInvoiceMessage";
		resources.ApplyResources(this.UNEDHSExpenseAddAmount, "UNEDHSExpenseAddAmount");
		((UltraNumericEditorBase)this.UNEDHSExpenseAddAmount).FormatString = "";
		this.UNEDHSExpenseAddAmount.MaxValue = 100000000.0;
		this.UNEDHSExpenseAddAmount.MinValue = 0.0;
		((System.Windows.Forms.Control)(object)this.UNEDHSExpenseAddAmount).Name = "UNEDHSExpenseAddAmount";
		this.UNEDHSExpenseAddAmount.NullText = "0";
		this.UNEDHSExpenseAddAmount.NumericType = (NumericType)1;
		((UltraNumericEditorBase)this.UNEDHSExpenseAddAmount).PromptChar = ' ';
		((UltraNumericEditorBase)this.UNEDHSExpenseAddAmount).SpinButtonDisplayStyle = (ButtonDisplayStyle)1;
		this.UNEDHSExpenseAddAmount.SpinIncrement = 1;
		resources.ApplyResources(this.txtImpManifestEnd, "txtImpManifestEnd");
		((System.Windows.Forms.Control)(object)this.txtImpManifestEnd).Name = "txtImpManifestEnd";
		resources.ApplyResources(this.cboCustodyAccount, "cboCustodyAccount");
		this.cboCustodyAccount.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCustodyAccount).Name = "cboCustodyAccount";
		resources.ApplyResources(this.txtExpManifestEnd, "txtExpManifestEnd");
		((System.Windows.Forms.Control)(object)this.txtExpManifestEnd).Name = "txtExpManifestEnd";
		resources.ApplyResources(this.UNEDHSExpenseIssueAmount, "UNEDHSExpenseIssueAmount");
		((UltraNumericEditorBase)this.UNEDHSExpenseIssueAmount).FormatString = "";
		this.UNEDHSExpenseIssueAmount.MaxValue = 100000000.0;
		this.UNEDHSExpenseIssueAmount.MinValue = 0.0;
		((System.Windows.Forms.Control)(object)this.UNEDHSExpenseIssueAmount).Name = "UNEDHSExpenseIssueAmount";
		this.UNEDHSExpenseIssueAmount.NullText = "0";
		this.UNEDHSExpenseIssueAmount.NumericType = (NumericType)1;
		((UltraNumericEditorBase)this.UNEDHSExpenseIssueAmount).PromptChar = ' ';
		((UltraNumericEditorBase)this.UNEDHSExpenseIssueAmount).SpinButtonDisplayStyle = (ButtonDisplayStyle)1;
		this.UNEDHSExpenseIssueAmount.SpinIncrement = 1;
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.ultraLabel6).Appearance = (AppearanceBase)(object)val7;
		this.ultraLabel6.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		((ControlBase)this.ultraLabel6).WrapText = false;
		resources.ApplyResources(this.lblEDHSAccount, "lblEDHSAccount");
		this.lblEDHSAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEDHSAccount).Name = "lblEDHSAccount";
		((ControlBase)this.lblEDHSAccount).WrapText = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val8;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.cboEDHSAccount, "cboEDHSAccount");
		this.cboEDHSAccount.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboEDHSAccount).Name = "cboEDHSAccount";
		resources.ApplyResources(this.txtImpManifestStart, "txtImpManifestStart");
		((System.Windows.Forms.Control)(object)this.txtImpManifestStart).Name = "txtImpManifestStart";
		resources.ApplyResources(this.lblEDHSExpenseIssueAmount, "lblEDHSExpenseIssueAmount");
		this.lblEDHSExpenseIssueAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEDHSExpenseIssueAmount).Name = "lblEDHSExpenseIssueAmount";
		((ControlBase)this.lblEDHSExpenseIssueAmount).WrapText = false;
		resources.ApplyResources(this.txtExpManifestStart, "txtExpManifestStart");
		((System.Windows.Forms.Control)(object)this.txtExpManifestStart).Name = "txtExpManifestStart";
		resources.ApplyResources(this.lblInvoiceMsg2, "lblInvoiceMsg2");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.lblInvoiceMsg2).Appearance = (AppearanceBase)(object)val9;
		this.lblInvoiceMsg2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInvoiceMsg2).Name = "lblInvoiceMsg2";
		((ControlBase)this.lblInvoiceMsg2).WrapText = false;
		resources.ApplyResources(this.lblEDHSExpenseAddAmount, "lblEDHSExpenseAddAmount");
		this.lblEDHSExpenseAddAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEDHSExpenseAddAmount).Name = "lblEDHSExpenseAddAmount";
		((ControlBase)this.lblEDHSExpenseAddAmount).WrapText = false;
		resources.ApplyResources(this.lblInvoiceMessage, "lblInvoiceMessage");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.lblInvoiceMessage).Appearance = (AppearanceBase)(object)val10;
		this.lblInvoiceMessage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInvoiceMessage).Name = "lblInvoiceMessage";
		((ControlBase)this.lblInvoiceMessage).WrapText = false;
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.ultraLabel3).Appearance = (AppearanceBase)(object)val11;
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.ultraLabel5).Appearance = (AppearanceBase)(object)val12;
		this.ultraLabel5.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.txtOpVoyageNoStart, "txtOpVoyageNoStart");
		((System.Windows.Forms.Control)(object)this.txtOpVoyageNoStart).Name = "txtOpVoyageNoStart";
		((System.Windows.Forms.Control)(object)this.txtOpVoyageNoStart).KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val13, "appearance13");
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val13;
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val14, "appearance14");
		((ControlBase)this.ultraLabel4).Appearance = (AppearanceBase)(object)val14;
		this.ultraLabel4.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		((ControlBase)this.ultraLabel4).WrapText = false;
		resources.ApplyResources(this.txtOpVoyageNoEnd, "txtOpVoyageNoEnd");
		((System.Windows.Forms.Control)(object)this.txtOpVoyageNoEnd).Name = "txtOpVoyageNoEnd";
		((System.Windows.Forms.Control)(object)this.txtOpVoyageNoEnd).KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBox_KeyPress);
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.btnPrintSupplierAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.btnPrintSupplierSubAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblPrintSupplierAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.cboPrintSupplierAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel7);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.txtPrintOutCost);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.cboPrintSupplierSubAccount);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblPrintOutCost);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add(this.txtActualPeriod);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaActualPeriod);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add(this.txtValidPeriod);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel14);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel16);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaValidPeriod);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.ULGData, "ULGData");
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowColMoving = (AllowColMoving)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowColSizing = (AllowColSizing)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeCell = (SelectType)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeCol = (SelectType)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeRow = (SelectType)2;
		((UltraGridBase)this.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		((UltraControlBase)this.ULGData).UseOsThemes = (DefaultableBoolean)2;
		this.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		this.ULGData.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
		resources.ApplyResources(this.btnPrintSupplierAccount, "btnPrintSupplierAccount");
		((AppearanceBase)val15).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val15, "appearance15");
		((ControlBase)this.btnPrintSupplierAccount).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.btnPrintSupplierAccount).Name = "btnPrintSupplierAccount";
		((System.Windows.Forms.Control)(object)this.btnPrintSupplierAccount).Click += new System.EventHandler(btnPrintSupplierAccount_Click);
		resources.ApplyResources(this.btnPrintSupplierSubAccount, "btnPrintSupplierSubAccount");
		((AppearanceBase)val16).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val16, "appearance16");
		((ControlBase)this.btnPrintSupplierSubAccount).Appearance = (AppearanceBase)(object)val16;
		((System.Windows.Forms.Control)(object)this.btnPrintSupplierSubAccount).Name = "btnPrintSupplierSubAccount";
		((System.Windows.Forms.Control)(object)this.btnPrintSupplierSubAccount).Click += new System.EventHandler(btnPrintSupplierSubAccount_Click);
		resources.ApplyResources(this.lblPrintSupplierAccount, "lblPrintSupplierAccount");
		this.lblPrintSupplierAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPrintSupplierAccount).Name = "lblPrintSupplierAccount";
		((ControlBase)this.lblPrintSupplierAccount).WrapText = false;
		resources.ApplyResources(this.cboPrintSupplierAccount, "cboPrintSupplierAccount");
		this.cboPrintSupplierAccount.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboPrintSupplierAccount).Name = "cboPrintSupplierAccount";
		((TextEditorControlBase)this.cboPrintSupplierAccount).ValueChanged += new System.EventHandler(cboPrintSupplierAccount_ValueChanged);
		resources.ApplyResources(this.ultraLabel7, "ultraLabel7");
		this.ultraLabel7.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel7).Name = "ultraLabel7";
		((ControlBase)this.ultraLabel7).WrapText = false;
		resources.ApplyResources(this.txtPrintOutCost, "txtPrintOutCost");
		((UltraNumericEditorBase)this.txtPrintOutCost).FormatString = "";
		this.txtPrintOutCost.MaxValue = 100000000.0;
		this.txtPrintOutCost.MinValue = 0.0;
		((System.Windows.Forms.Control)(object)this.txtPrintOutCost).Name = "txtPrintOutCost";
		this.txtPrintOutCost.NullText = "0";
		this.txtPrintOutCost.NumericType = (NumericType)1;
		((UltraNumericEditorBase)this.txtPrintOutCost).PromptChar = ' ';
		((UltraNumericEditorBase)this.txtPrintOutCost).SpinButtonDisplayStyle = (ButtonDisplayStyle)1;
		this.txtPrintOutCost.SpinIncrement = 1;
		resources.ApplyResources(this.cboPrintSupplierSubAccount, "cboPrintSupplierSubAccount");
		this.cboPrintSupplierSubAccount.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboPrintSupplierSubAccount).Name = "cboPrintSupplierSubAccount";
		resources.ApplyResources(this.lblPrintOutCost, "lblPrintOutCost");
		this.lblPrintOutCost.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPrintOutCost).Name = "lblPrintOutCost";
		((ControlBase)this.lblPrintOutCost).WrapText = false;
		resources.ApplyResources(this.txtActualPeriod, "txtActualPeriod");
		this.txtActualPeriod.Maximum = new decimal(new int[4] { 3600, 0, 0, 0 });
		this.txtActualPeriod.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.txtActualPeriod.Name = "txtActualPeriod";
		this.txtActualPeriod.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		resources.ApplyResources(this.lblVisaActualPeriod, "lblVisaActualPeriod");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val17, "appearance17");
		((ControlBase)this.lblVisaActualPeriod).Appearance = (AppearanceBase)(object)val17;
		this.lblVisaActualPeriod.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaActualPeriod).Name = "lblVisaActualPeriod";
		((ControlBase)this.lblVisaActualPeriod).WrapText = false;
		resources.ApplyResources(this.txtValidPeriod, "txtValidPeriod");
		this.txtValidPeriod.Maximum = new decimal(new int[4] { 3600, 0, 0, 0 });
		this.txtValidPeriod.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.txtValidPeriod.Name = "txtValidPeriod";
		this.txtValidPeriod.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		resources.ApplyResources(this.ultraLabel14, "ultraLabel14");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val18, "appearance18");
		((ControlBase)this.ultraLabel14).Appearance = (AppearanceBase)(object)val18;
		this.ultraLabel14.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel14).Name = "ultraLabel14";
		((ControlBase)this.ultraLabel14).WrapText = false;
		resources.ApplyResources(this.ultraLabel16, "ultraLabel16");
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val19, "appearance19");
		((ControlBase)this.ultraLabel16).Appearance = (AppearanceBase)(object)val19;
		this.ultraLabel16.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel16).Name = "ultraLabel16";
		((ControlBase)this.ultraLabel16).WrapText = false;
		resources.ApplyResources(this.lblVisaValidPeriod, "lblVisaValidPeriod");
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val20, "appearance20");
		((ControlBase)this.lblVisaValidPeriod).Appearance = (AppearanceBase)(object)val20;
		this.lblVisaValidPeriod.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaValidPeriod).Name = "lblVisaValidPeriod";
		((ControlBase)this.lblVisaValidPeriod).WrapText = false;
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val21).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val21).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val21).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val21, "appearance21");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val21;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val22).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val22).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val22).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val22, "appearance22");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val22;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val23).Image = resources.GetObject("appearance23.Image");
		resources.ApplyResources(val23, "appearance23");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val23;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.ofdPicture, "ofdPicture");
		resources.ApplyResources(this.tabItemType, "tabItemType");
		((AppearanceBase)val24).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val24, "appearance24");
		((AppearanceBase)val24).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.tabItemType).Appearance = (AppearanceBase)(object)val24;
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.tabItem);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((System.Windows.Forms.Control)(object)this.tabItemType).Name = "tabItemType";
		((UltraTabControlBase)this.tabItemType).SharedControlsPage = this.ultraTabSharedControlsPage1;
		resources.ApplyResources(val25, "appearance25");
		((UltraTabControlBase)this.tabItemType).TabHeaderAreaAppearance = (AppearanceBase)(object)val25;
		resources.ApplyResources(val26, "appearance26");
		((UltraTabControlBase)this.tabItemType).TabListButtonAppearance = (AppearanceBase)(object)val26;
		((UltraTabControlBase)this.tabItemType).TabOrientation = (TabOrientation)2;
		((KeyedSubObjectBase)val27).Key = "Item";
		val27.TabPage = this.tabItem;
		resources.ApplyResources(val27, "ultraTab2");
		((SubObjectBase)val27).ForceApplyResources = "";
		((KeyedSubObjectBase)val28).Key = "Visas";
		val28.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val28, "ultraTab3");
		((SubObjectBase)val28).ForceApplyResources = "";
		((UltraTabControlBase)this.tabItemType).Tabs.AddRange((UltraTab[])(object)new UltraTab[2] { val27, val28 });
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.tabItemType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmMSSetting";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.tabItemType, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.tabItem).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.tabItem).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.numVesselAlertBefore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numVesselStayValidPeriod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvoiceMSG2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvoiceMessage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UNEDHSExpenseAddAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtImpManifestEnd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCustodyAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExpManifestEnd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UNEDHSExpenseIssueAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboEDHSAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtImpManifestStart).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExpManifestStart).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOpVoyageNoStart).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOpVoyageNoEnd).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPrintSupplierAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPrintOutCost).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPrintSupplierSubAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtActualPeriod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtValidPeriod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tabItemType).EndInit();
		((System.Windows.Forms.Control)(object)this.tabItemType).ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
