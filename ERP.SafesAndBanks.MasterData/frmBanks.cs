using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using BusinessLayer.SafesAndBanks;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SafesAndBanks.MasterData;

public class frmBanks : frmGrid
{
	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private DataTable dtCurrency = new DataTable();

	private ValueList vlCurrency = new ValueList();

	private ValueList vlAccounts = new ValueList();

	private ValueList vlSubAccounts = new ValueList();

	private bool cantUpdate = false;

	private IContainer components = null;

	public UltraButton btnNPAccountSearch;

	private UltraTextEditor txtBankMinimumLimit;

	private UltraLabel lblBankMinimumLimit;

	private UltraComboEditor cboNPAccount;

	private UltraLabel lblNPAccount;

	private UltraTextEditor txtBankNameEn;

	private UltraLabel lblBankNameEn;

	private UltraTextEditor txtBankNameAr;

	private UltraLabel lblBankNameAr;

	private UltraTextEditor txtBankCode;

	private UltraLabel lblBankCode;

	private UltraTextEditor txtBankAccNo;

	private UltraLabel lblBankAccNumber;

	private UltraLabel lblCurrency;

	private UltraComboEditor cboCurrency;

	public UltraButton btnBankAccountSearch;

	private UltraComboEditor cboBankAccount;

	private UltraLabel lblBankAccount;

	public UltraButton btnNPUnderCollectionSearch;

	private UltraComboEditor cboNPUnderCollectionAccount;

	private UltraLabel lblNPUnderCollectionAccount;

	private UltraLabel lblNRAccount;

	private UltraComboEditor cboNRAccount;

	public UltraButton btnNRAccountSearch;

	private UltraLabel lblNRUnderCollectionAccount;

	private UltraComboEditor cboNRUnderCollectionAccount;

	public UltraButton btnNRUnderCollectionAccountSearch;

	public UltraButton btnCollectionExpenseAccount;

	private UltraComboEditor cboCollectionExpenseAccount;

	private UltraLabel lblCollectionExpenceAccount;

	public UltraButton btnBankSubAccountSearch;

	private UltraComboEditor cboBankSubAccount;

	private UltraLabel lblBankSubAccount;

	private UltraLabel lblBankDescription;

	private UltraTextEditor txtBankDescription;

	private UltraLabel lblSenderBankExpensesAcc;

	private UltraComboEditor cboSenderBankExpensesAcc;

	public UltraButton btnSenderBankExpensesAcc;

	public frmBanks()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		InitializeComponent();
		TableName = "SB_Banks";
		IDCol = "BankID";
	}

	public override void PrepareData()
	{
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboBankAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboNPAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboNPUnderCollectionAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboNRAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboNRUnderCollectionAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboCollectionExpenseAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboSenderBankExpensesAcc, dtAccounts, "AccountID", "Name");
		vlAccounts.ValueListItems.Clear();
		for (int i = 0; i < dtAccounts.Rows.Count; i++)
		{
			vlAccounts.ValueListItems.Add((object)dtAccounts.Rows[i]["AccountID"].ToString(), dtAccounts.Rows[i]["Name"].ToString());
		}
		dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboBankSubAccount, dtSubAccounts, "SubAccountID", "Name");
		vlSubAccounts.ValueListItems.Clear();
		for (int j = 0; j < dtSubAccounts.Rows.Count; j++)
		{
			vlSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[j]["SubAccountID"], dtSubAccounts.Rows[j]["Name"].ToString());
		}
		dtCurrency = Currency.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", GlobalVariables.IsArabic ? "CurrencyNameAr" : "CurrencyNameEn");
		vlCurrency.ValueListItems.Clear();
		for (int k = 0; k < dtCurrency.Rows.Count; k++)
		{
			vlCurrency.ValueListItems.Add((object)dtCurrency.Rows[k]["CurrencyID"].ToString(), dtCurrency.Rows[k]["CurrencyCode"].ToString());
		}
	}

	public override void SetControls(bool NavMode)
	{
		if (Updating)
		{
			cantUpdate = BankIn.SelectByBankID(((UltraGridBase)ULGData).ActiveRow.Cells["BankID"].Value.ToString(), "1").Rows.Count > 0 || BankOut.SelectByBankID(((UltraGridBase)ULGData).ActiveRow.Cells["BankID"].Value.ToString(), "1").Rows.Count > 0;
		}
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtBankCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBankNameAr).ReadOnly = (Updating && cantUpdate) || NavMode;
		((EditorButtonControlBase)txtBankNameEn).ReadOnly = (Updating && cantUpdate) || NavMode;
		((EditorButtonControlBase)txtBankAccNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBankDescription).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBankMinimumLimit).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCurrency).ReadOnly = (Updating && cantUpdate) || NavMode;
		((EditorButtonControlBase)cboBankAccount).ReadOnly = (Updating && cantUpdate) || NavMode;
		((EditorButtonControlBase)cboBankSubAccount).ReadOnly = (Updating && cantUpdate) || NavMode;
		((EditorButtonControlBase)cboNPAccount).ReadOnly = (Updating && cantUpdate) || NavMode;
		((EditorButtonControlBase)cboNPUnderCollectionAccount).ReadOnly = (Updating && cantUpdate) || NavMode;
		((EditorButtonControlBase)cboNRAccount).ReadOnly = (Updating && cantUpdate) || NavMode;
		((EditorButtonControlBase)cboNRUnderCollectionAccount).ReadOnly = (Updating && cantUpdate) || NavMode;
		((EditorButtonControlBase)cboCollectionExpenseAccount).ReadOnly = (Updating && cantUpdate) || NavMode;
		((EditorButtonControlBase)cboSenderBankExpensesAcc).ReadOnly = (Updating && cantUpdate) || NavMode;
		((Control)(object)btnBankAccountSearch).Visible = !((Updating && cantUpdate) || NavMode);
		((Control)(object)btnBankSubAccountSearch).Visible = !((Updating && cantUpdate) || NavMode);
		((Control)(object)btnNPAccountSearch).Visible = !((Updating && cantUpdate) || NavMode);
		((Control)(object)btnNPUnderCollectionSearch).Visible = !((Updating && cantUpdate) || NavMode);
		((Control)(object)btnNRAccountSearch).Visible = !((Updating && cantUpdate) || NavMode);
		((Control)(object)btnNRUnderCollectionAccountSearch).Visible = !((Updating && cantUpdate) || NavMode);
		((Control)(object)btnCollectionExpenseAccount).Visible = !((Updating && cantUpdate) || NavMode);
		((Control)(object)btnSenderBankExpensesAcc).Visible = !((Updating && cantUpdate) || NavMode);
		((TextEditorControlBase)txtBankCode).Focus();
	}

	public override void ClearControls()
	{
		((Control)(object)txtBankCode).Text = (Adding ? Banks.GetCode(IsFromServer: true) : "");
		((TextEditorControlBase)txtBankNameAr).Clear();
		((TextEditorControlBase)txtBankNameEn).Clear();
		((TextEditorControlBase)txtBankAccNo).Clear();
		((TextEditorControlBase)txtBankDescription).Clear();
		((Control)(object)txtBankMinimumLimit).Text = "0";
		cboCurrency.SelectedIndex = -1;
		cboBankAccount.SelectedIndex = -1;
		cboBankSubAccount.SelectedIndex = -1;
		cboNPAccount.SelectedIndex = -1;
		cboNPUnderCollectionAccount.SelectedIndex = -1;
		cboNRAccount.SelectedIndex = -1;
		cboNRUnderCollectionAccount.SelectedIndex = -1;
		cboCollectionExpenseAccount.SelectedIndex = -1;
		cboSenderBankExpensesAcc.SelectedIndex = -1;
	}

	public override void FillData()
	{
		dataTable = Banks.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
	}

	public override void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = dataTable;
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود البنك" : "Bank Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankAccNumber"].Header).Caption = (GlobalVariables.IsArabic ? "رقم حساب البنك" : "Bank Acc No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankAccNumber"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankAccNumber"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyID"].Header).Caption = (GlobalVariables.IsArabic ? "العملة" : "Currency");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyID"].ValueList = (IValueList)(object)vlCurrency;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Header).Caption = (GlobalVariables.IsArabic ? "حساب البنك" : "Bank Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].ValueList = (IValueList)(object)vlAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب التحليلى " : "SubAccount");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlSubAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankMinimumLimit"].Header).Caption = (GlobalVariables.IsArabic ? "الحد الإدنى" : "Minimum level");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankMinimumLimit"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankMinimumLimit"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankMinimumLimit"].DefaultCellValue = 0;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NPAccID"].Header).Caption = (GlobalVariables.IsArabic ? "حساب اد" : "NP Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NPAccID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NPAccID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NPAccID"].ValueList = (IValueList)(object)vlAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NPUnderCollectionAccID"].Header).Caption = (GlobalVariables.IsArabic ? "حساب اد تحت التحصيل" : "NP Under Collection Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NPUnderCollectionAccID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NPUnderCollectionAccID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NPUnderCollectionAccID"].ValueList = (IValueList)(object)vlAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NRAccID"].Header).Caption = (GlobalVariables.IsArabic ? "حساب اق" : "NR Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NRAccID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NRAccID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NRAccID"].ValueList = (IValueList)(object)vlAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NRUnderCollectionAccID"].Header).Caption = (GlobalVariables.IsArabic ? "حساب اق تحت التحصيل" : "NR Under Collection Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NRUnderCollectionAccID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NRUnderCollectionAccID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NRUnderCollectionAccID"].ValueList = (IValueList)(object)vlAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CollectionExpensesAccID"].Header).Caption = (GlobalVariables.IsArabic ? "حساب مصاريف التحصيل" : "Collection Expense Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CollectionExpensesAccID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CollectionExpensesAccID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CollectionExpensesAccID"].ValueList = (IValueList)(object)vlAccounts;
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtBankCode).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["BankCode"].Value.ToString();
		((Control)(object)txtBankNameAr).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["BankNameAr"].Value.ToString();
		((Control)(object)txtBankNameEn).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["BankNameEn"].Value.ToString();
		((Control)(object)txtBankAccNo).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["BankAccNumber"].Value.ToString();
		((Control)(object)txtBankDescription).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["BankDescription"].Value.ToString();
		((Control)(object)txtBankMinimumLimit).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["BankMinimumLimit"].Value.ToString();
		((TextEditorControlBase)cboCurrency).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["CurrencyID"].Value;
		((TextEditorControlBase)cboBankAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["AccountID"].Value;
		((TextEditorControlBase)cboBankSubAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["SubAccountID"].Value;
		((TextEditorControlBase)cboNPAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["NPAccID"].Value;
		((TextEditorControlBase)cboNPUnderCollectionAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["NPUnderCollectionAccID"].Value;
		((TextEditorControlBase)cboNRAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["NRAccID"].Value;
		((TextEditorControlBase)cboNRUnderCollectionAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["NRUnderCollectionAccID"].Value;
		((TextEditorControlBase)cboCollectionExpenseAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["CollectionExpensesAccID"].Value;
		((TextEditorControlBase)cboSenderBankExpensesAcc).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["SenderBankExpensesAccID"].Value;
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtBankCode).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال كود البنك", "Please Insert Bank Code");
			((TextEditorControlBase)txtBankCode).Focus();
			return false;
		}
		if (((Control)(object)txtBankNameAr).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال إسم البنك بالعربية", "Please Insert Bank Arabic Name");
			((TextEditorControlBase)txtBankNameAr).Focus();
			return false;
		}
		if (((Control)(object)txtBankAccNo).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال رقم حساب البنك", "Please Insert Bank Account Number");
			((TextEditorControlBase)txtBankAccNo).Focus();
			return false;
		}
		if (cboBankAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار إسم حساب الخزينة", "Please Select Safe Account Name");
			((TextEditorControlBase)cboBankAccount).Focus();
			cboBankAccount.DropDown();
			return false;
		}
		if (cboBankSubAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(" برجاء إختيار إسم الحساب التحليلى", "Please Select SubAccount Name");
			((TextEditorControlBase)cboBankSubAccount).Focus();
			cboBankSubAccount.DropDown();
			return false;
		}
		DataRow[] array = dataTable.Select(" BankNameAr= '" + ((Control)(object)txtBankNameAr).Text + "'");
		if (array.Length != 0 && (Adding || array[0]["BankID"].ToString() != ((UltraGridBase)ULGData).ActiveRow.Cells["BankID"].Value.ToString()))
		{
			GlobalVariables.InformationMB.Show("هذا الاسم موجود من قبل", "This Name is Already Exists..");
			return false;
		}
		array = dataTable.Select(" SubAccountID= " + ((TextEditorControlBase)cboBankSubAccount).Value.ToString());
		if (array.Length != 0 && (Adding || array[0]["SubAccountID"].ToString() != ((UltraGridBase)ULGData).ActiveRow.Cells["SubAccountID"].Value.ToString()))
		{
			GlobalVariables.InformationMB.Show("هذا الحساب التحليلى موجود من قبل", "This SubAccount is Already Exists..");
			return false;
		}
		if (cboCurrency.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار العملة", "Please Select Currency");
			((TextEditorControlBase)cboCurrency).Focus();
			cboCurrency.DropDown();
			return false;
		}
		if (cboNPAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار حساب اد", "Please Select NP Account");
			((TextEditorControlBase)cboNPAccount).Focus();
			cboNPAccount.DropDown();
			return false;
		}
		if (cboNPUnderCollectionAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار حساب اد نحن التحصيل", "Please Select NP Under Collection Account ");
			((TextEditorControlBase)cboNPUnderCollectionAccount).Focus();
			cboNPUnderCollectionAccount.DropDown();
			return false;
		}
		if (cboNRAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار حساب اق", "Please Select NR Account ");
			((TextEditorControlBase)cboNRAccount).Focus();
			cboNRAccount.DropDown();
			return false;
		}
		if (cboNRUnderCollectionAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار حساب اق تحت التحصيل", "Please Select NR Under Collection Account");
			((TextEditorControlBase)cboNRUnderCollectionAccount).Focus();
			cboNRUnderCollectionAccount.DropDown();
			return false;
		}
		if (cboCollectionExpenseAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار حساب مصاريف التحصيل", "Please Select Collection Expense Account");
			((TextEditorControlBase)cboCollectionExpenseAccount).Focus();
			cboCollectionExpenseAccount.DropDown();
			return false;
		}
		if (cboSenderBankExpensesAcc.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار حساب مصاريف البنك الراسل", "Please Select Sender Bank Expense Account");
			((TextEditorControlBase)cboSenderBankExpensesAcc).Focus();
			cboSenderBankExpensesAcc.DropDown();
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		Banks.Insert_Update("-1", ((Control)(object)txtBankCode).Text, ((Control)(object)txtBankNameAr).Text, (((Control)(object)txtBankNameEn).Text == "") ? "Null" : ((Control)(object)txtBankNameEn).Text, (((Control)(object)txtBankAccNo).Text == "") ? "Null" : ((Control)(object)txtBankAccNo).Text, ((TextEditorControlBase)cboCurrency).Value.ToString(), ((TextEditorControlBase)cboBankAccount).Value.ToString(), ((TextEditorControlBase)cboBankSubAccount).Value.ToString(), ((TextEditorControlBase)cboNPAccount).Value.ToString(), ((TextEditorControlBase)cboNPUnderCollectionAccount).Value.ToString(), ((TextEditorControlBase)cboNRAccount).Value.ToString(), ((TextEditorControlBase)cboNRUnderCollectionAccount).Value.ToString(), ((TextEditorControlBase)cboCollectionExpenseAccount).Value.ToString(), ((TextEditorControlBase)cboSenderBankExpensesAcc).Value.ToString(), (((Control)(object)txtBankMinimumLimit).Text == "") ? "0" : ((Control)(object)txtBankMinimumLimit).Text, ((Control)(object)txtBankDescription).Text, GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID, IsFromServer: true);
	}

	public override void UpdateData()
	{
		Banks.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["BankID"].Value.ToString(), ((Control)(object)txtBankCode).Text, ((Control)(object)txtBankNameAr).Text, (((Control)(object)txtBankNameEn).Text == "") ? "Null" : ((Control)(object)txtBankNameEn).Text, (((Control)(object)txtBankAccNo).Text == "") ? "Null" : ((Control)(object)txtBankAccNo).Text, ((TextEditorControlBase)cboCurrency).Value.ToString(), ((TextEditorControlBase)cboBankAccount).Value.ToString(), ((TextEditorControlBase)cboBankSubAccount).Value.ToString(), ((TextEditorControlBase)cboNPAccount).Value.ToString(), ((TextEditorControlBase)cboNPUnderCollectionAccount).Value.ToString(), ((TextEditorControlBase)cboNRAccount).Value.ToString(), ((TextEditorControlBase)cboNRUnderCollectionAccount).Value.ToString(), ((TextEditorControlBase)cboCollectionExpenseAccount).Value.ToString(), ((TextEditorControlBase)cboSenderBankExpensesAcc).Value.ToString(), (((Control)(object)txtBankMinimumLimit).Text == "") ? "0" : ((Control)(object)txtBankMinimumLimit).Text, ((Control)(object)txtBankDescription).Text, GlobalVariables.CurrentBranchID, ((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString().Equals("True") ? "1" : "0", GlobalVariables.UserID, IsFromServer: true);
	}

	public override void DeleteData()
	{
		Banks.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["BankID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
		ClearControls();
	}

	public override void btnRefreshDataClick()
	{
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboBankAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboNPAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboNPUnderCollectionAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboNRAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboNRUnderCollectionAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboCollectionExpenseAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboSenderBankExpensesAcc, dtAccounts, "AccountID", "Name");
		vlAccounts.ValueListItems.Clear();
		for (int i = 0; i < dtAccounts.Rows.Count; i++)
		{
			vlAccounts.ValueListItems.Add((object)dtAccounts.Rows[i]["AccountID"].ToString(), dtAccounts.Rows[i]["Name"].ToString());
		}
		dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboBankSubAccount, dtSubAccounts, "SubAccountID", "Name");
		vlSubAccounts.ValueListItems.Clear();
		for (int j = 0; j < dtSubAccounts.Rows.Count; j++)
		{
			vlSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[j]["SubAccountID"], dtSubAccounts.Rows[j]["Name"].ToString());
		}
		dtCurrency = Currency.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", GlobalVariables.IsArabic ? "CurrencyNameAr" : "CurrencyNameEn");
		vlCurrency.ValueListItems.Clear();
		for (int k = 0; k < dtCurrency.Rows.Count; k++)
		{
			vlCurrency.ValueListItems.Add((object)dtCurrency.Rows[k]["CurrencyID"].ToString(), dtCurrency.Rows[k]["CurrencyCode"].ToString());
		}
	}

	private void txtBankMinimumLimit_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void ComboAccount_KeyDown(object sender, KeyEventArgs e)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.Accounts(IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)(UltraComboEditor)sender).Value = num;
			}
		}
	}

	private void btnAccountSearch_Click(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Expected O, but got Unknown
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Expected O, but got Unknown
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Expected O, but got Unknown
		if (((Control)(UltraButton)sender).Name == "btnBankAccountSearch")
		{
			((TextEditorControlBase)cboBankAccount).Value = SearchFunctions.Accounts(IsFromServer: true);
		}
		else if (((Control)(UltraButton)sender).Name == "btnNPAccountSearch")
		{
			((TextEditorControlBase)cboNPAccount).Value = SearchFunctions.Accounts(IsFromServer: true);
		}
		else if (((Control)(UltraButton)sender).Name == "btnNPUnderCollectionSearch")
		{
			((TextEditorControlBase)cboNPUnderCollectionAccount).Value = SearchFunctions.Accounts(IsFromServer: true);
		}
		else if (((Control)(UltraButton)sender).Name == "btnNRAccountSearch")
		{
			((TextEditorControlBase)cboNRAccount).Value = SearchFunctions.Accounts(IsFromServer: true);
		}
		else if (((Control)(UltraButton)sender).Name == "btnNRUnderCollectionAccountSearch")
		{
			((TextEditorControlBase)cboNRUnderCollectionAccount).Value = SearchFunctions.Accounts(IsFromServer: true);
		}
		else if (((Control)(UltraButton)sender).Name == "btnCollectionExpenseAccount")
		{
			((TextEditorControlBase)cboCollectionExpenseAccount).Value = SearchFunctions.Accounts(IsFromServer: true);
		}
		else if (((Control)(UltraButton)sender).Name == "btnSenderBankExpensesAcc")
		{
			((TextEditorControlBase)cboSenderBankExpensesAcc).Value = SearchFunctions.Accounts(IsFromServer: true);
		}
	}

	private void cboBankAccount_ValueChanged(object sender, EventArgs e)
	{
		if (cboBankAccount.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtSubAccounts);
			dataView.RowFilter = "AccountID=" + ((TextEditorControlBase)cboBankAccount).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboBankSubAccount.DataSource = dataView;
		}
	}

	private void cboBankSubAccount_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119 && cboBankAccount.SelectedIndex != -1)
		{
			int num = SearchFunctions.SubAccounts(((TextEditorControlBase)cboBankAccount).Value.ToString(), IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboBankSubAccount).Value = num;
			}
		}
	}

	private void btnBankSubAccountSearch_Click(object sender, EventArgs e)
	{
		if (cboBankAccount.SelectedIndex != -1)
		{
			int num = SearchFunctions.SubAccounts(((TextEditorControlBase)cboBankAccount).Value.ToString(), IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboBankSubAccount).Value = num;
			}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SafesAndBanks.MasterData.frmBanks));
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
		this.btnNPAccountSearch = new UltraButton();
		this.txtBankMinimumLimit = new UltraTextEditor();
		this.lblBankMinimumLimit = new UltraLabel();
		this.cboNPAccount = new UltraComboEditor();
		this.lblNPAccount = new UltraLabel();
		this.txtBankNameEn = new UltraTextEditor();
		this.lblBankNameEn = new UltraLabel();
		this.txtBankNameAr = new UltraTextEditor();
		this.lblBankNameAr = new UltraLabel();
		this.txtBankCode = new UltraTextEditor();
		this.lblBankCode = new UltraLabel();
		this.txtBankAccNo = new UltraTextEditor();
		this.lblBankAccNumber = new UltraLabel();
		this.lblCurrency = new UltraLabel();
		this.cboCurrency = new UltraComboEditor();
		this.btnBankAccountSearch = new UltraButton();
		this.cboBankAccount = new UltraComboEditor();
		this.lblBankAccount = new UltraLabel();
		this.btnNPUnderCollectionSearch = new UltraButton();
		this.cboNPUnderCollectionAccount = new UltraComboEditor();
		this.lblNPUnderCollectionAccount = new UltraLabel();
		this.lblNRAccount = new UltraLabel();
		this.cboNRAccount = new UltraComboEditor();
		this.btnNRAccountSearch = new UltraButton();
		this.lblNRUnderCollectionAccount = new UltraLabel();
		this.cboNRUnderCollectionAccount = new UltraComboEditor();
		this.btnNRUnderCollectionAccountSearch = new UltraButton();
		this.btnCollectionExpenseAccount = new UltraButton();
		this.cboCollectionExpenseAccount = new UltraComboEditor();
		this.lblCollectionExpenceAccount = new UltraLabel();
		this.btnBankSubAccountSearch = new UltraButton();
		this.cboBankSubAccount = new UltraComboEditor();
		this.lblBankSubAccount = new UltraLabel();
		this.lblBankDescription = new UltraLabel();
		this.txtBankDescription = new UltraTextEditor();
		this.lblSenderBankExpensesAcc = new UltraLabel();
		this.cboSenderBankExpensesAcc = new UltraComboEditor();
		this.btnSenderBankExpensesAcc = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBankMinimumLimit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboNPAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBankNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBankNameAr).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBankCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBankAccNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBankAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboNPUnderCollectionAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboNRAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboNRUnderCollectionAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCollectionExpenseAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBankSubAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBankDescription).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSenderBankExpensesAcc).BeginInit();
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
		resources.ApplyResources(val8, "appearance18");
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
		resources.ApplyResources(this.btnNPAccountSearch, "btnNPAccountSearch");
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val10, "appearance19");
		((ControlBase)this.btnNPAccountSearch).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.btnNPAccountSearch).Name = "btnNPAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnNPAccountSearch).Click += new System.EventHandler(btnAccountSearch_Click);
		resources.ApplyResources(this.txtBankMinimumLimit, "txtBankMinimumLimit");
		((System.Windows.Forms.Control)(object)this.txtBankMinimumLimit).Name = "txtBankMinimumLimit";
		((System.Windows.Forms.Control)(object)this.txtBankMinimumLimit).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtBankMinimumLimit_KeyPress);
		resources.ApplyResources(this.lblBankMinimumLimit, "lblBankMinimumLimit");
		this.lblBankMinimumLimit.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBankMinimumLimit).Name = "lblBankMinimumLimit";
		((ControlBase)this.lblBankMinimumLimit).WrapText = false;
		resources.ApplyResources(this.cboNPAccount, "cboNPAccount");
		this.cboNPAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboNPAccount).Name = "cboNPAccount";
		((System.Windows.Forms.Control)(object)this.cboNPAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(ComboAccount_KeyDown);
		resources.ApplyResources(this.lblNPAccount, "lblNPAccount");
		this.lblNPAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNPAccount).Name = "lblNPAccount";
		((ControlBase)this.lblNPAccount).WrapText = false;
		resources.ApplyResources(this.txtBankNameEn, "txtBankNameEn");
		((System.Windows.Forms.Control)(object)this.txtBankNameEn).Name = "txtBankNameEn";
		resources.ApplyResources(this.lblBankNameEn, "lblBankNameEn");
		this.lblBankNameEn.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBankNameEn).Name = "lblBankNameEn";
		((ControlBase)this.lblBankNameEn).WrapText = false;
		resources.ApplyResources(this.txtBankNameAr, "txtBankNameAr");
		((System.Windows.Forms.Control)(object)this.txtBankNameAr).Name = "txtBankNameAr";
		resources.ApplyResources(this.lblBankNameAr, "lblBankNameAr");
		this.lblBankNameAr.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBankNameAr).Name = "lblBankNameAr";
		((ControlBase)this.lblBankNameAr).WrapText = false;
		resources.ApplyResources(this.txtBankCode, "txtBankCode");
		((System.Windows.Forms.Control)(object)this.txtBankCode).Name = "txtBankCode";
		resources.ApplyResources(this.lblBankCode, "lblBankCode");
		this.lblBankCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBankCode).Name = "lblBankCode";
		((ControlBase)this.lblBankCode).WrapText = false;
		resources.ApplyResources(this.txtBankAccNo, "txtBankAccNo");
		((System.Windows.Forms.Control)(object)this.txtBankAccNo).Name = "txtBankAccNo";
		resources.ApplyResources(this.lblBankAccNumber, "lblBankAccNumber");
		this.lblBankAccNumber.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBankAccNumber).Name = "lblBankAccNumber";
		((ControlBase)this.lblBankAccNumber).WrapText = false;
		resources.ApplyResources(this.lblCurrency, "lblCurrency");
		this.lblCurrency.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCurrency).Name = "lblCurrency";
		((ControlBase)this.lblCurrency).WrapText = false;
		resources.ApplyResources(this.cboCurrency, "cboCurrency");
		this.cboCurrency.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboCurrency).Name = "cboCurrency";
		resources.ApplyResources(this.btnBankAccountSearch, "btnBankAccountSearch");
		((AppearanceBase)val11).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val11, "appearance20");
		((ControlBase)this.btnBankAccountSearch).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.btnBankAccountSearch).Name = "btnBankAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnBankAccountSearch).Click += new System.EventHandler(btnAccountSearch_Click);
		resources.ApplyResources(this.cboBankAccount, "cboBankAccount");
		this.cboBankAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboBankAccount).Name = "cboBankAccount";
		((TextEditorControlBase)this.cboBankAccount).ValueChanged += new System.EventHandler(cboBankAccount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboBankAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(ComboAccount_KeyDown);
		resources.ApplyResources(this.lblBankAccount, "lblBankAccount");
		this.lblBankAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBankAccount).Name = "lblBankAccount";
		((ControlBase)this.lblBankAccount).WrapText = false;
		resources.ApplyResources(this.btnNPUnderCollectionSearch, "btnNPUnderCollectionSearch");
		((AppearanceBase)val12).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val12, "appearance21");
		((ControlBase)this.btnNPUnderCollectionSearch).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.btnNPUnderCollectionSearch).Name = "btnNPUnderCollectionSearch";
		((System.Windows.Forms.Control)(object)this.btnNPUnderCollectionSearch).Click += new System.EventHandler(btnAccountSearch_Click);
		resources.ApplyResources(this.cboNPUnderCollectionAccount, "cboNPUnderCollectionAccount");
		this.cboNPUnderCollectionAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboNPUnderCollectionAccount).Name = "cboNPUnderCollectionAccount";
		((System.Windows.Forms.Control)(object)this.cboNPUnderCollectionAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(ComboAccount_KeyDown);
		resources.ApplyResources(this.lblNPUnderCollectionAccount, "lblNPUnderCollectionAccount");
		this.lblNPUnderCollectionAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNPUnderCollectionAccount).Name = "lblNPUnderCollectionAccount";
		((ControlBase)this.lblNPUnderCollectionAccount).WrapText = false;
		resources.ApplyResources(this.lblNRAccount, "lblNRAccount");
		this.lblNRAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNRAccount).Name = "lblNRAccount";
		((ControlBase)this.lblNRAccount).WrapText = false;
		resources.ApplyResources(this.cboNRAccount, "cboNRAccount");
		this.cboNRAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboNRAccount).Name = "cboNRAccount";
		((System.Windows.Forms.Control)(object)this.cboNRAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(ComboAccount_KeyDown);
		resources.ApplyResources(this.btnNRAccountSearch, "btnNRAccountSearch");
		((AppearanceBase)val13).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val13, "appearance22");
		((ControlBase)this.btnNRAccountSearch).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.btnNRAccountSearch).Name = "btnNRAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnNRAccountSearch).Click += new System.EventHandler(btnAccountSearch_Click);
		resources.ApplyResources(this.lblNRUnderCollectionAccount, "lblNRUnderCollectionAccount");
		this.lblNRUnderCollectionAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNRUnderCollectionAccount).Name = "lblNRUnderCollectionAccount";
		((ControlBase)this.lblNRUnderCollectionAccount).WrapText = false;
		resources.ApplyResources(this.cboNRUnderCollectionAccount, "cboNRUnderCollectionAccount");
		this.cboNRUnderCollectionAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboNRUnderCollectionAccount).Name = "cboNRUnderCollectionAccount";
		((System.Windows.Forms.Control)(object)this.cboNRUnderCollectionAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(ComboAccount_KeyDown);
		resources.ApplyResources(this.btnNRUnderCollectionAccountSearch, "btnNRUnderCollectionAccountSearch");
		((AppearanceBase)val14).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val14, "appearance23");
		((ControlBase)this.btnNRUnderCollectionAccountSearch).Appearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.btnNRUnderCollectionAccountSearch).Name = "btnNRUnderCollectionAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnNRUnderCollectionAccountSearch).Click += new System.EventHandler(btnAccountSearch_Click);
		resources.ApplyResources(this.btnCollectionExpenseAccount, "btnCollectionExpenseAccount");
		((AppearanceBase)val15).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val15, "appearance24");
		((ControlBase)this.btnCollectionExpenseAccount).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.btnCollectionExpenseAccount).Name = "btnCollectionExpenseAccount";
		((System.Windows.Forms.Control)(object)this.btnCollectionExpenseAccount).Click += new System.EventHandler(btnAccountSearch_Click);
		resources.ApplyResources(this.cboCollectionExpenseAccount, "cboCollectionExpenseAccount");
		this.cboCollectionExpenseAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboCollectionExpenseAccount).Name = "cboCollectionExpenseAccount";
		((System.Windows.Forms.Control)(object)this.cboCollectionExpenseAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(ComboAccount_KeyDown);
		resources.ApplyResources(this.lblCollectionExpenceAccount, "lblCollectionExpenceAccount");
		this.lblCollectionExpenceAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCollectionExpenceAccount).Name = "lblCollectionExpenceAccount";
		((ControlBase)this.lblCollectionExpenceAccount).WrapText = false;
		resources.ApplyResources(this.btnBankSubAccountSearch, "btnBankSubAccountSearch");
		((AppearanceBase)val16).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val16, "appearance25");
		((ControlBase)this.btnBankSubAccountSearch).Appearance = (AppearanceBase)(object)val16;
		((System.Windows.Forms.Control)(object)this.btnBankSubAccountSearch).Name = "btnBankSubAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnBankSubAccountSearch).Click += new System.EventHandler(btnBankSubAccountSearch_Click);
		resources.ApplyResources(this.cboBankSubAccount, "cboBankSubAccount");
		this.cboBankSubAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboBankSubAccount).Name = "cboBankSubAccount";
		((System.Windows.Forms.Control)(object)this.cboBankSubAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(cboBankSubAccount_KeyDown);
		resources.ApplyResources(this.lblBankSubAccount, "lblBankSubAccount");
		this.lblBankSubAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBankSubAccount).Name = "lblBankSubAccount";
		((ControlBase)this.lblBankSubAccount).WrapText = false;
		resources.ApplyResources(this.lblBankDescription, "lblBankDescription");
		this.lblBankDescription.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBankDescription).Name = "lblBankDescription";
		((ControlBase)this.lblBankDescription).WrapText = false;
		resources.ApplyResources(this.txtBankDescription, "txtBankDescription");
		((System.Windows.Forms.Control)(object)this.txtBankDescription).Name = "txtBankDescription";
		resources.ApplyResources(this.lblSenderBankExpensesAcc, "lblSenderBankExpensesAcc");
		this.lblSenderBankExpensesAcc.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSenderBankExpensesAcc).Name = "lblSenderBankExpensesAcc";
		((ControlBase)this.lblSenderBankExpensesAcc).WrapText = false;
		resources.ApplyResources(this.cboSenderBankExpensesAcc, "cboSenderBankExpensesAcc");
		this.cboSenderBankExpensesAcc.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSenderBankExpensesAcc).Name = "cboSenderBankExpensesAcc";
		((System.Windows.Forms.Control)(object)this.cboSenderBankExpensesAcc).KeyDown += new System.Windows.Forms.KeyEventHandler(ComboAccount_KeyDown);
		resources.ApplyResources(this.btnSenderBankExpensesAcc, "btnSenderBankExpensesAcc");
		((AppearanceBase)val17).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val17, "appearance26");
		((ControlBase)this.btnSenderBankExpensesAcc).Appearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.btnSenderBankExpensesAcc).Name = "btnSenderBankExpensesAcc";
		((System.Windows.Forms.Control)(object)this.btnSenderBankExpensesAcc).Click += new System.EventHandler(btnAccountSearch_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBankDescription);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBankDescription);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnBankSubAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBankSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBankSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSenderBankExpensesAcc);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCollectionExpenseAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSenderBankExpensesAcc);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCollectionExpenseAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSenderBankExpensesAcc);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCollectionExpenceAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNRUnderCollectionAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNPUnderCollectionSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboNPUnderCollectionAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNPUnderCollectionAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnBankAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBankAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBankAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBankAccNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBankAccNumber);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNRAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNPAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBankMinimumLimit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBankMinimumLimit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboNRAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboNPAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNRAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNPAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBankNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboNRUnderCollectionAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBankNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNRUnderCollectionAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBankNameAr);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBankNameAr);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBankCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBankCode);
		base.Name = "frmBanks";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBankCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBankCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBankNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBankNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNRUnderCollectionAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBankNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboNRUnderCollectionAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBankNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNPAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNRAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboNPAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboNRAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBankMinimumLimit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBankMinimumLimit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNPAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNRAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBankAccNumber, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBankAccNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBankAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBankAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnBankAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNPUnderCollectionAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboNPUnderCollectionAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNPUnderCollectionSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNRUnderCollectionAccountSearch, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCollectionExpenceAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSenderBankExpensesAcc, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCollectionExpenseAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSenderBankExpensesAcc, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCollectionExpenseAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSenderBankExpensesAcc, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBankSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBankSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnBankSubAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBankDescription, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBankDescription, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBankMinimumLimit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboNPAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBankNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBankNameAr).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBankCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBankAccNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBankAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboNPUnderCollectionAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboNRAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboNRUnderCollectionAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCollectionExpenseAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBankSubAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBankDescription).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSenderBankExpensesAcc).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
