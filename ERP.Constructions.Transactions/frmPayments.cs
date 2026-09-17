using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Constructions;
using BusinessLayer.Privilege;
using BusinessLayer.SafesAndBanks;
using BusinessLayer.Security;
using ERP.AbstractForms;
using ERP.Accounting.Transactions;
using ERP.Classes;
using ERP.Properties;
using ERP.SafesAndBanks.Approved;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Constructions.Transactions;

public class frmPayments : frmHeaderDetails
{
	private DataTable dtClients;

	private DataTable dtCurrency;

	private DataTable dtBanks;

	private DataTable dtsafes;

	private DataTable dtReports;

	private DataTable dtJvDetails;

	private DataTable dtBankIn;

	private DataTable dtSafeIn;

	private DataTable dtInstallmentsTypes;

	private ValueList vlInstallmentsTypes = new ValueList();

	private bool UseCurrency;

	private string SubAccountID = "0";

	private IContainer components = null;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboCurrency;

	private UltraLabel lblCurrency;

	private UltraLabel lblBank;

	private UltraComboEditor cboBank;

	private UltraTextEditor txtExchangeRate;

	private UltraLabel lblExchangrRate;

	private UltraTextEditor txtChargedPerson;

	private UltraLabel lblChargePerson;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblTotal;

	private UltraTextEditor txtTotal;

	public UltraButton btnJV;

	private UltraPanel pnlCheckType;

	private RadioButton rbDeposit;

	private RadioButton rbIsCheck;

	private UltraTextEditor txtCheckNo;

	private UltraLabel lblCheckNo;

	private UltraLabel lblCheckDate;

	private UltraDateTimeEditor dtpCheckDate;

	private UltraTextEditor txtReceivingBank;

	private UltraLabel lblReceivingBank;

	public UltraButton btnJV2;

	public UltraButton btnJV3;

	private UltraLabel lblCheckStatus;

	private UltraButton btnChangeStatus;

	private UltraLabel lblCheckBinder;

	private UltraComboEditor cboBinderSafe;

	private RadioButton rbIsSafe;

	private UltraLabel lblSafe;

	private UltraComboEditor cboSafe;

	private UltraButton btnSelectInstallment;

	public UltraButton btnClientSearch;

	private UltraComboEditor cboClient;

	private UltraLabel lblClient;

	public frmPayments()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		TableName = "Con_Payments";
		IDCol = "PaymentID";
		NoCol = "PaymentNo";
		DateCol = "PaymentDate";
		((Control)(object)btnJV3).Text = (((Control)(object)btnJV2).Text = (((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? "قيد " : "JV")));
	}

	public frmPayments(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public frmPayments(string subaccountid)
		: this()
	{
		SubAccountID = subaccountid;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtpDate.ValueChanged -= dtpJVDate_ValueChanged;
		dtpDate.DateTime = DateTime.Now;
		dtpDate.ValueChanged += dtpJVDate_ValueChanged;
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		UseCurrency = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from Currency ").Rows[0][0].ToString()) > 1;
		dtBanks = Banks.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBank, dtBanks, "BankID", "BankName");
		dtInstallmentsTypes = InstallmentsTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInstallmentsTypes.ValueListItems.Clear();
		for (int i = 0; i < dtInstallmentsTypes.Rows.Count; i++)
		{
			vlInstallmentsTypes.ValueListItems.Add(dtInstallmentsTypes.Rows[i]["InstallmentTypeID"], dtInstallmentsTypes.Rows[i]["InstallmentTypeName"].ToString());
		}
		dtsafes = Safes.FillComboBySafeIDs(GlobalVariables.SafeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBinderSafe, dtsafes, "SafeID", "SafeName");
		GlobalFunctions.FillCombo(cboSafe, dtsafes, "SafeID", "SafeName");
		FillCurrencyDropDown();
		dtDetails = ContractsInstallments.SelectByPaymentID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void FillData()
	{
		if (SubAccountID != "0")
		{
			drMaster = null;
			btnAddClick();
			((TextEditorControlBase)cboClient).Value = SubAccountID;
			DisplayData();
			bool adding = Adding;
			btnSelectInstallment_Click(null, null);
		}
		else if (RowID == "")
		{
			drMaster = null;
			DisplayData();
		}
		else
		{
			DataTable dataTable = Payments.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (drMaster != null)
		{
			dtpDate.ValueChanged -= dtpJVDate_ValueChanged;
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			dtSafeIn = null;
			dtBankIn = null;
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["PaymentNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["PaymentDate"];
			((TextEditorControlBase)cboClient).Value = drMaster["SubAccountID"];
			rbDeposit.Checked = bool.Parse(drMaster["IsDeposit"].ToString());
			rbIsCheck.Checked = bool.Parse(drMaster["IsCheck"].ToString());
			rbIsSafe.Checked = bool.Parse(drMaster["IsSafe"].ToString());
			((TextEditorControlBase)cboSafe).Value = drMaster["SafeID"];
			((TextEditorControlBase)cboCurrency).Value = drMaster["CurrencyID"];
			((Control)(object)txtExchangeRate).Text = drMaster["ExchangeRate"].ToString();
			((Control)(object)txtCheckNo).Text = drMaster["CheckNo"].ToString();
			dtpCheckDate.Value = drMaster["CheckDate"];
			((TextEditorControlBase)cboBank).Value = drMaster["BankID"];
			((Control)(object)txtChargedPerson).Text = drMaster["ChargedPerson"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtTotal).Text = drMaster["Total"].ToString();
			((Control)(object)txtReceivingBank).Text = drMaster["ReceivingBank"].ToString();
			((TextEditorControlBase)cboBinderSafe).Value = drMaster["BinderSafeID"];
			if (rbIsSafe.Checked && drMaster["SafeInID"] != DBNull.Value)
			{
				dtSafeIn = SafeIn.Select(drMaster["SafeInID"].ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0");
			}
			else if (!rbIsSafe.Checked && drMaster["BankInID"] != DBNull.Value)
			{
				dtBankIn = BankIn.Select(drMaster["BankInID"].ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0");
			}
			if (dtSafeIn != null && dtSafeIn.Rows.Count > 0)
			{
				((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? ("(" + dtSafeIn.Rows[0]["JVNo"].ToString() + ")قيد") : (" No( " + dtSafeIn.Rows[0]["JVNo"].ToString() + " )"));
				((Control)(object)btnJV).Visible = ((dtSafeIn.Rows[0]["JVNo"] != DBNull.Value) ? true : false) && CanViewJV;
			}
			else if (dtBankIn != null && dtBankIn.Rows.Count > 0)
			{
				((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? ("(" + dtBankIn.Rows[0]["JVNo"].ToString() + ")قيد ") : ("JV( " + dtBankIn.Rows[0]["JVNo"].ToString() + " )"));
				((Control)(object)btnJV2).Text = (GlobalVariables.IsArabic ? ("(" + dtBankIn.Rows[0]["JVNo2"].ToString() + ")قيد ") : ("JV( " + dtBankIn.Rows[0]["JVNo2"].ToString() + " )"));
				((Control)(object)btnJV3).Text = (GlobalVariables.IsArabic ? ("(" + dtBankIn.Rows[0]["JVNo3"].ToString() + ")قيد ") : ("JV( " + dtBankIn.Rows[0]["JVNo3"].ToString() + " )"));
				((Control)(object)btnJV).Visible = ((dtBankIn.Rows[0]["JVNo"] != DBNull.Value) ? true : false) && CanViewJV;
				((Control)(object)btnJV2).Visible = ((dtBankIn.Rows[0]["JVNo2"] != DBNull.Value) ? true : false) && CanViewJV;
				((Control)(object)btnJV3).Visible = ((dtBankIn.Rows[0]["JVNo3"] != DBNull.Value) ? true : false) && CanViewJV;
				((Control)(object)lblCheckStatus).Text = dtBankIn.Rows[0]["CheckStatus"].ToString();
				((Control)(object)lblCheckStatus).Visible = ((dtBankIn.Rows[0]["CheckStatus"] != DBNull.Value) ? true : false);
				((Control)(object)btnChangeStatus).Visible = ((dtBankIn.Rows[0]["CheckStatus"] != DBNull.Value) ? true : false) && !bool.Parse(dtBankIn.Rows[0]["Collected"].ToString()) && !bool.Parse(dtBankIn.Rows[0]["Returned"].ToString());
			}
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = ContractsInstallments.SelectByPaymentID(drMaster["PaymentID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
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
			dtpDate.ValueChanged += dtpJVDate_ValueChanged;
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
		}
		else
		{
			ClearControls();
			UltraLabel obj = lblCheckStatus;
			UltraButton obj2 = btnChangeStatus;
			UltraButton obj3 = btnJV;
			UltraButton obj4 = btnJV2;
			bool flag = (((Control)(object)btnJV3).Visible = false);
			bool flag3 = (((Control)(object)obj4).Visible = flag);
			bool flag5 = (((Control)(object)obj3).Visible = flag3);
			bool visible = (((Control)(object)obj2).Visible = flag5);
			((Control)(object)obj).Visible = visible;
		}
		((TextEditorControlBase)txtCode).Focus();
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboClient).ReadOnly = !Adding;
		rbIsCheck.Enabled = !NavMode;
		rbDeposit.Enabled = !NavMode;
		rbIsSafe.Enabled = !NavMode;
		((EditorButtonControlBase)cboSafe).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCheckNo).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpCheckDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBank).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBinderSafe).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCurrency).ReadOnly = !rbIsSafe.Checked || NavMode;
		((EditorButtonControlBase)txtExchangeRate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtChargedPerson).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTotal).ReadOnly = true;
		((EditorButtonControlBase)txtReceivingBank).ReadOnly = NavMode;
		((Control)(object)btnJV).Visible = NavMode && drMaster != null && CanViewJV;
		((Control)(object)btnJV2).Visible = NavMode && drMaster != null && CanViewJV;
		((Control)(object)btnJV3).Visible = NavMode && drMaster != null && CanViewJV;
		((Control)(object)btnClientSearch).Visible = Adding;
		((Control)(object)btnSelectInstallment).Visible = Adding;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		cboBank.SelectedIndex = ((((DisposableObjectCollectionBase)cboBank.Items).Count <= 0) ? (-1) : 0);
		cboSafe.SelectedIndex = ((((DisposableObjectCollectionBase)cboSafe.Items).Count <= 0) ? (-1) : 0);
		dtpDate.DateTime = DateTime.Now;
		if (SubAccountID == "0")
		{
			cboClient.SelectedIndex = -1;
		}
		((EditorButtonControlBase)cboClient).ReadOnly = false;
		cboCurrency.SelectedIndex = -1;
		((Control)(object)txtCode).Text = (Adding ? Payments.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		dtpCheckDate.DateTime = DateTime.Now;
		rbIsSafe.Checked = true;
		cboBinderSafe.SelectedIndex = -1;
		((TextEditorControlBase)txtCheckNo).Clear();
		((TextEditorControlBase)txtReceivingBank).Clear();
		((TextEditorControlBase)txtChargedPerson).Clear();
		((TextEditorControlBase)txtNotes).Clear();
		((Control)(object)txtTotal).Text = "0";
		UltraButton obj = btnJV3;
		UltraButton obj2 = btnJV2;
		string text = (((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? "قيد" : "JV"));
		string text3 = (((Control)(object)obj2).Text = text);
		((Control)(object)obj).Text = text3;
		dtSafeIn = null;
		dtBankIn = null;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPaid"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InstallmentTypeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContractInstallmentDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContractInstallmentAmount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPaid"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InstallmentTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع القسط" : "Installment Type");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContractInstallmentDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ القسط" : "Installment Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContractInstallmentAmount"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة القسط" : "Installment Amount");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPaid"].Header).Caption = (GlobalVariables.IsArabic ? "مدفوع" : "Paid");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InstallmentTypeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContractInstallmentDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContractInstallmentAmount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPaid"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InstallmentTypeID"].ValueList = (IValueList)(object)vlInstallmentsTypes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContractInstallmentAmount"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPaid"].DefaultCellValue = false;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("Split"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns.Insert(0, "Split");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Split"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Split"].Header).Caption = (GlobalVariables.IsArabic ? "تقسيم" : "Split");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Split"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Split"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Split"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Split"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Split"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count - 1));
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["Split"].Value = (GlobalVariables.IsArabic ? "تقسيم" : "Split");
		}
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
		if (rbIsCheck.Checked && dtpCheckDate.Value == null)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار تاريخ الشيك", "Please Select Cheque Date");
			((Control)(object)dtpCheckDate).Focus();
			return false;
		}
		if (rbIsCheck.Checked && !FiscalYear.ChkForConfirmedFiscalYear(dtpCheckDate.DateTime.ToString(GlobalVariables.DateShortFormate), IsFromServer: false))
		{
			GlobalVariables.InformationMB.Show("السنة المالية لتاريخ الشيك غير معتمدة", "The Fiscal Year For Check Date is not confirmed..");
			return false;
		}
		if (rbIsCheck.Checked && FiscalYear.ChkForClosingFsicalPeriod(dtpCheckDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false))
		{
			GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ الشيك تاريخ يقع في فتره ماليه مغلقة", "The Date you choosed For Check Date\n\r exists in closed fisical period");
			return false;
		}
		if (Updating && DateTime.Parse(drMaster["PaymentDate"].ToString()).Year != dtpDate.DateTime.Year)
		{
			GlobalVariables.InformationMB.Show("لايمكن تغيير الإذن الى سنة مالية أخرى", "Cannot Change Voucher Date To Another Fiscal Year");
			return false;
		}
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم  الأذن" : "Please Enter The Voucher Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (cboBank.SelectedIndex < 0 && !rbIsSafe.Checked)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار البنك" : "Please Select the Bank ");
			((TextEditorControlBase)cboBank).Focus();
			return false;
		}
		if (rbIsCheck.Checked && ((Control)(object)txtCheckNo).Text == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم الشيك" : "Please Enter  Check No ");
			((TextEditorControlBase)txtCheckNo).Focus();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("Con_Payments", "PaymentNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["PaymentNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = Payments.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
			if (cboClient.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار عميل", "Please select Client");
				((TextEditorControlBase)cboClient).Focus();
				return false;
			}
			if (cboCurrency.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار العملة", "Please select Currency");
				((TextEditorControlBase)cboCurrency).Focus();
				return false;
			}
			if (decimal.Parse(((Control)(object)txtExchangeRate).Text) <= 0m || ((Control)(object)txtExchangeRate).Text == "")
			{
				GlobalVariables.InformationMB.Show("سعر التحويل لابد ان يكون اكبر من الصفر", "Exchange Rate Must Be Greater Than Zero");
				((TextEditorControlBase)txtExchangeRate).Focus();
				return false;
			}
			if (((Control)(object)txtChargedPerson).Text == "")
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "من فضلك قم بادخال من السيد" : "Please Insert From Mrs.");
				((TextEditorControlBase)txtChargedPerson).Focus();
				return false;
			}
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا الإذن", "Please insert details for this Voucher");
			return false;
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = Payments.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboClient).Value.ToString(), rbIsCheck.Checked ? "1" : "0", rbDeposit.Checked ? "1" : "0", rbIsSafe.Checked ? "1" : "0", (cboSafe.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSafe).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, rbIsCheck.Checked ? ((Control)(object)txtCheckNo).Text : "Null", rbIsCheck.Checked ? dtpCheckDate.DateTime.ToString(GlobalVariables.DateLongFormate) : "Null", (!rbIsCheck.Checked) ? "Null" : ((cboBank.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBank).Value.ToString()), ((Control)(object)txtChargedPerson).Text, ((Control)(object)txtNotes).Text, ((Control)(object)txtTotal).Text, ((Control)(object)txtReceivingBank).Text, (cboBinderSafe.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBinderSafe).Value.ToString(), "Null", "Null", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				ContractsInstallments.UpdateContractInstallmentIDs("," + ((UltraGridBase)ULGData).Rows[i].Cells["ContractInstallmentID"].Value.ToString() + ",", bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsPaid"].Value.ToString()) ? "1" : "0", bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsPaid"].Value.ToString()) ? num.ToString() : "Null", ((UltraGridBase)ULGData).Rows[i].Cells["Notes"].Value.ToString());
			}
			if (rbIsSafe.Checked)
			{
				int num2 = SafeIn.Insert_Update("-1", SafeIn.GetCodeByBranchID((cboSafe.SelectedIndex == -1) ? "" : ((TextEditorControlBase)cboSafe).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboSafe).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, ((Control)(object)txtChargedPerson).Text, (GlobalVariables.IsArabic ? " إيصال رقم " : " Receipt No ") + ((Control)(object)txtCode).Text + "  " + ((Control)(object)txtNotes).Text, ((Control)(object)txtTotal).Text, "Null", "Null", "0", GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
				{
					if (((UltraGridBase)ULGData).Rows[j].Cells["IsPaid"].Value.Equals(true))
					{
						SafeInDetails.Insert_Update("-1", num2.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["ContractInstallmentAmount"].Value.ToString(), dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["AccountID"].ToString(), dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"].ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["CostCenterID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["CostCenterID"].Value.ToString(), "Null", "0", ((UltraGridBase)ULGData).Rows[j].Cells["Notes"].Value.ToString(), GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID);
					}
				}
				dtJvDetails = JVDetails.Select("0", "-1", GlobalVariables.IsArabic ? "1" : "0");
				DataRow dataRow = dtJvDetails.NewRow();
				dataRow["JVDetailID"] = "-1";
				dataRow["AccountID"] = dtsafes.Select(" SafeID= " + ((TextEditorControlBase)cboSafe).Value.ToString())[0]["AccountID"];
				dataRow["SubAccountID"] = dtsafes.Select(" SafeID= " + ((TextEditorControlBase)cboSafe).Value.ToString())[0]["SubAccountID"];
				dataRow["Debit"] = ((Control)(object)txtTotal).Text;
				dataRow["Credit"] = "0";
				dataRow["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
				dataRow["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
				dataRow["LocalDebit"] = decimal.Parse(dataRow["Debit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
				dataRow["LocalCredit"] = "0";
				dataRow["Deleted"] = false;
				dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
				dtJvDetails.Rows.Add(dataRow);
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
				{
					if (((UltraGridBase)ULGData).Rows[k].Cells["IsPaid"].Value.Equals(true))
					{
						dataRow = dtJvDetails.NewRow();
						dataRow["JVDetailID"] = "-1";
						dataRow["AccountID"] = dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["AccountID"];
						dataRow["SubAccountID"] = dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"];
						dataRow["CostCenterID"] = ((UltraGridBase)ULGData).Rows[k].Cells["CostCenterID"].Value;
						dataRow["Debit"] = "0";
						dataRow["Credit"] = ((UltraGridBase)ULGData).Rows[k].Cells["ContractInstallmentAmount"].Value.ToString();
						dataRow["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
						dataRow["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
						dataRow["LocalDebit"] = "0";
						dataRow["LocalCredit"] = decimal.Parse(dataRow["Credit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
						dataRow["Notes"] = ((UltraGridBase)ULGData).Rows[k].Cells["Notes"].Value;
						dataRow["Deleted"] = false;
						dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
						dtJvDetails.Rows.Add(dataRow);
					}
				}
				string code = JV.GetCode(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, "1");
				int num3 = JV.GenerateJV_Insert(code, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "1", num2.ToString(), ((Control)(object)txtCode).Text, " ( " + ((Control)(object)txtChargedPerson).Text + " ) " + (GlobalVariables.IsArabic ? " إيصال رقم " : " Receipt No ") + ((Control)(object)txtCode).Text + "  " + ((Control)(object)txtNotes).Text, "0", "1", dtJvDetails, "0", GlobalVariables.CurrentBranchID, "1", GlobalVariables.UserID);
				((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? ("(" + code + ")قيد رقم ") : ("JV No ( " + code + " )"));
				Main.ExecuteNonQuery(" Update SB_SafeIn  set JvID= " + num3 + " Where SafeInID=" + num2.ToString());
				Main.ExecuteNonQuery(" Update Con_Payments  set SafeInID= " + num2 + " Where PaymentID=" + num);
			}
			else
			{
				int num4 = BankIn.Insert_Update("-1", BankIn.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, rbIsCheck.Checked ? "1" : "0", ((Control)(object)txtCheckNo).Text, rbIsCheck.Checked ? dtpCheckDate.DateTime.ToString(GlobalVariables.DateLongFormate) : "Null", ((TextEditorControlBase)cboBank).Value.ToString(), ((Control)(object)txtChargedPerson).Text, (GlobalVariables.IsArabic ? " إيصال رقم " : " Receipt No ") + ((Control)(object)txtCode).Text + "  " + ((Control)(object)txtNotes).Text, ((Control)(object)txtTotal).Text, ((Control)(object)txtReceivingBank).Text, (cboBinderSafe.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBinderSafe).Value.ToString(), (dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["NRAccID"].ToString() == dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["NRUnderCollectionAccID"].ToString()) ? "1" : "0", "Null", "0", "0", "0", "Null", "0", "Null", "Null", "Null", "Null", "0", "Null", "Null", "Null", "Null", "0", GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID);
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; l++)
				{
					if (((UltraGridBase)ULGData).Rows[l].Cells["IsPaid"].Value.Equals(true))
					{
						BankInDetails.Insert_Update("-1", num4.ToString(), ((UltraGridBase)ULGData).Rows[l].Cells["ContractInstallmentAmount"].Value.ToString(), dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["AccountID"].ToString(), dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"].ToString(), (((UltraGridBase)ULGData).Rows[l].Cells["CostCenterID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].Cells["CostCenterID"].Value.ToString(), "Null", "0", ((UltraGridBase)ULGData).Rows[l].Cells["Notes"].Value.ToString(), GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID);
					}
				}
				dtJvDetails = JVDetails.Select("0", "-1", GlobalVariables.IsArabic ? "1" : "0");
				DataRow dataRow2 = dtJvDetails.NewRow();
				dataRow2["JVDetailID"] = "-1";
				dataRow2["AccountID"] = (rbIsCheck.Checked ? dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["NRAccID"].ToString() : dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["AccountID"].ToString());
				dataRow2["SubAccountID"] = (rbIsCheck.Checked ? DBNull.Value : dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["SubAccountID"]);
				dataRow2["Debit"] = ((Control)(object)txtTotal).Text;
				dataRow2["Credit"] = "0";
				dataRow2["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
				dataRow2["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
				dataRow2["LocalDebit"] = decimal.Parse(dataRow2["Debit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
				dataRow2["LocalCredit"] = "0";
				dataRow2["Deleted"] = false;
				dataRow2["BranchID"] = GlobalVariables.CurrentBranchID;
				dtJvDetails.Rows.Add(dataRow2);
				for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; m++)
				{
					if (((UltraGridBase)ULGData).Rows[m].Cells["IsPaid"].Value.Equals(true))
					{
						dataRow2 = dtJvDetails.NewRow();
						dataRow2["JVDetailID"] = "-1";
						dataRow2["AccountID"] = dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["AccountID"];
						dataRow2["SubAccountID"] = dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"];
						dataRow2["CostCenterID"] = ((UltraGridBase)ULGData).Rows[m].Cells["CostCenterID"].Value;
						dataRow2["Debit"] = "0";
						dataRow2["Credit"] = ((UltraGridBase)ULGData).Rows[m].Cells["ContractInstallmentAmount"].Value.ToString();
						dataRow2["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
						dataRow2["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
						dataRow2["LocalDebit"] = "0";
						dataRow2["LocalCredit"] = decimal.Parse(dataRow2["Credit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
						dataRow2["Notes"] = ((UltraGridBase)ULGData).Rows[m].Cells["Notes"].Value;
						dataRow2["Deleted"] = false;
						dataRow2["BranchID"] = GlobalVariables.CurrentBranchID;
						dtJvDetails.Rows.Add(dataRow2);
					}
				}
				string code2 = JV.GetCode(dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, "3");
				int num5 = JV.GenerateJV_Insert(code2, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "3", num4.ToString(), ((Control)(object)txtCode).Text, " ( " + ((Control)(object)txtChargedPerson).Text + " ) " + (GlobalVariables.IsArabic ? " إيصال رقم " : " Receipt No ") + ((Control)(object)txtCode).Text + "  " + ((Control)(object)txtNotes).Text, "0", "1", dtJvDetails, "0", GlobalVariables.CurrentBranchID, "1", GlobalVariables.UserID);
				((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? ("(" + code2 + ")قيد رقم ") : ("JV No ( " + code2 + " )"));
				Main.ExecuteNonQuery(" Update SB_BankIn  set JvID= " + num5 + " Where BankInID=" + num4.ToString());
				Main.ExecuteNonQuery(" Update Con_Payments  set BankInID= " + num4 + " Where PaymentID=" + num);
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

	public override void UpdateData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = Payments.Insert_Update(drMaster["PaymentID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboClient).Value.ToString(), rbIsCheck.Checked ? "1" : "0", rbDeposit.Checked ? "1" : "0", rbIsSafe.Checked ? "1" : "0", (cboSafe.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSafe).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, rbIsCheck.Checked ? ((Control)(object)txtCheckNo).Text : "Null", rbIsCheck.Checked ? dtpCheckDate.DateTime.ToString(GlobalVariables.DateLongFormate) : "Null", (!rbIsCheck.Checked) ? "Null" : ((cboBank.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBank).Value.ToString()), ((Control)(object)txtChargedPerson).Text, ((Control)(object)txtNotes).Text, ((Control)(object)txtTotal).Text, ((Control)(object)txtReceivingBank).Text, (cboBinderSafe.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBinderSafe).Value.ToString(), (drMaster["BankInID"] == DBNull.Value) ? "Null" : drMaster["BankInID"].ToString(), (drMaster["SafeInID"] == DBNull.Value) ? "Null" : drMaster["SafeInID"].ToString(), "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				ContractsInstallments.UpdateContractInstallmentIDs("," + ((UltraGridBase)ULGData).Rows[i].Cells["ContractInstallmentID"].Value.ToString() + ",", bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsPaid"].Value.ToString()) ? "1" : "0", bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsPaid"].Value.ToString()) ? num.ToString() : "Null", ((UltraGridBase)ULGData).Rows[i].Cells["Notes"].Value.ToString());
			}
			if (rbIsSafe.Checked && drMaster != null && bool.Parse(drMaster["IsSafe"].ToString()))
			{
				int num2 = SafeIn.Insert_Update(dtSafeIn.Rows[0]["SafeInID"].ToString(), dtSafeIn.Rows[0]["SafeInNo"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboSafe).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, ((Control)(object)txtChargedPerson).Text, (GlobalVariables.IsArabic ? " إيصال رقم " : " Receipt No ") + ((Control)(object)txtCode).Text + "  " + ((Control)(object)txtNotes).Text, ((Control)(object)txtTotal).Text, "Null", dtSafeIn.Rows[0]["JVID"].ToString(), bool.Parse(dtSafeIn.Rows[0]["Approved"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID);
				SafeInDetails.DeleteBySafeInID(num2.ToString(), GlobalVariables.UserID);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
				{
					if (((UltraGridBase)ULGData).Rows[j].Cells["IsPaid"].Value.Equals(true))
					{
						SafeInDetails.Insert_Update("-1", num2.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["ContractInstallmentAmount"].Value.ToString(), dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["AccountID"].ToString(), dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"].ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["CostCenterID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["CostCenterID"].Value.ToString(), "Null", "0", ((UltraGridBase)ULGData).Rows[j].Cells["Notes"].Value.ToString(), GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID);
					}
				}
				JVDetails.DeleteByJVID(dtSafeIn.Rows[0]["JVID"].ToString(), GlobalVariables.UserID);
				dtJvDetails = JVDetails.Select("0", "-1", GlobalVariables.IsArabic ? "1" : "0");
				DataRow dataRow = dtJvDetails.NewRow();
				dataRow["JVDetailID"] = "-1";
				dataRow["AccountID"] = dtsafes.Select(" SafeID= " + ((TextEditorControlBase)cboSafe).Value.ToString())[0]["AccountID"];
				dataRow["SubAccountID"] = dtsafes.Select(" SafeID= " + ((TextEditorControlBase)cboSafe).Value.ToString())[0]["SubAccountID"];
				dataRow["Debit"] = ((Control)(object)txtTotal).Text;
				dataRow["Credit"] = "0";
				dataRow["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
				dataRow["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
				dataRow["LocalDebit"] = decimal.Parse(dataRow["Debit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
				dataRow["LocalCredit"] = "0";
				dataRow["Deleted"] = false;
				dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
				dtJvDetails.Rows.Add(dataRow);
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
				{
					if (((UltraGridBase)ULGData).Rows[k].Cells["IsPaid"].Value.Equals(true))
					{
						dataRow = dtJvDetails.NewRow();
						dataRow["JVDetailID"] = "-1";
						dataRow["AccountID"] = dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["AccountID"];
						dataRow["SubAccountID"] = dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"];
						dataRow["CostCenterID"] = ((UltraGridBase)ULGData).Rows[k].Cells["CostCenterID"].Value;
						dataRow["Debit"] = "0";
						dataRow["Credit"] = ((UltraGridBase)ULGData).Rows[k].Cells["ContractInstallmentAmount"].Value.ToString();
						dataRow["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
						dataRow["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
						dataRow["LocalDebit"] = "0";
						dataRow["LocalCredit"] = decimal.Parse(dataRow["Credit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
						dataRow["Notes"] = ((UltraGridBase)ULGData).Rows[k].Cells["Notes"].Value;
						dataRow["Deleted"] = false;
						dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
						dtJvDetails.Rows.Add(dataRow);
					}
				}
				string code = JV.GetCode(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, "1");
				int num3 = JV.GenerateJV_Insert(code, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "1", num2.ToString(), ((Control)(object)txtCode).Text, " ( " + ((Control)(object)txtChargedPerson).Text + " ) " + (GlobalVariables.IsArabic ? " إيصال رقم " : " Receipt No ") + ((Control)(object)txtCode).Text + "  " + ((Control)(object)txtNotes).Text, "0", "1", dtJvDetails, "0", GlobalVariables.CurrentBranchID, "1", GlobalVariables.UserID);
				((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? ("(" + code + ")قيد رقم ") : ("JV No ( " + code + " )"));
				Main.ExecuteNonQuery(" Update SB_SafeIn  set JvID= " + num3 + " Where SafeInID=" + num2.ToString());
				Main.ExecuteNonQuery(" Update Con_Payments  set SafeInID= " + num2 + " Where PaymentID=" + num);
			}
			else if (!rbIsSafe.Checked && drMaster != null && !bool.Parse(drMaster["IsSafe"].ToString()))
			{
				int num4 = BankIn.Insert_Update(dtBankIn.Rows[0]["BankInID"].ToString(), dtBankIn.Rows[0]["BankInNo"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, rbIsCheck.Checked ? "1" : "0", ((Control)(object)txtCheckNo).Text, rbIsCheck.Checked ? dtpCheckDate.DateTime.ToString(GlobalVariables.DateLongFormate) : "Null", ((TextEditorControlBase)cboBank).Value.ToString(), ((Control)(object)txtChargedPerson).Text, (GlobalVariables.IsArabic ? " إيصال رقم " : " Receipt No ") + ((Control)(object)txtCode).Text + "  " + ((Control)(object)txtNotes).Text, ((Control)(object)txtTotal).Text, ((Control)(object)txtReceivingBank).Text, (cboBinderSafe.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBinderSafe).Value.ToString(), (bool.Parse(dtBankIn.Rows[0]["IsCheck"].ToString()) != rbIsCheck.Checked && dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["NRAccID"].ToString() != dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["NRUnderCollectionAccID"].ToString()) ? "0" : (dtBankIn.Rows[0]["UnderCollection"].Equals(true) ? "1" : "0"), (bool.Parse(dtBankIn.Rows[0]["IsCheck"].ToString()) != rbIsCheck.Checked) ? "Null" : ((dtBankIn.Rows[0]["UnderCollectionDate"] == DBNull.Value) ? "Null" : DateTime.Parse(dtBankIn.Rows[0]["UnderCollectionDate"].ToString()).ToString(GlobalVariables.DateLongFormate)), (bool.Parse(dtBankIn.Rows[0]["IsCheck"].ToString()) != rbIsCheck.Checked) ? "0" : (dtBankIn.Rows[0]["Collected"].Equals(true) ? "1" : "0"), (bool.Parse(dtBankIn.Rows[0]["IsCheck"].ToString()) != rbIsCheck.Checked) ? "0" : (dtBankIn.Rows[0]["Returned"].Equals(true) ? "1" : "0"), (bool.Parse(dtBankIn.Rows[0]["IsCheck"].ToString()) != rbIsCheck.Checked) ? "0" : (dtBankIn.Rows[0]["ToSafe"].Equals(true) ? "1" : "0"), (bool.Parse(dtBankIn.Rows[0]["IsCheck"].ToString()) != rbIsCheck.Checked) ? "Null" : ((dtBankIn.Rows[0]["SafeID"] == DBNull.Value) ? "Null" : dtBankIn.Rows[0]["SafeID"].ToString()), (bool.Parse(dtBankIn.Rows[0]["IsCheck"].ToString()) != rbIsCheck.Checked) ? "0" : (dtBankIn.Rows[0]["IsEndorsement"].Equals(true) ? "1" : "0"), (bool.Parse(dtBankIn.Rows[0]["IsCheck"].ToString()) != rbIsCheck.Checked) ? "Null" : ((dtBankIn.Rows[0]["AccountID"] == DBNull.Value) ? "Null" : dtBankIn.Rows[0]["AccountID"].ToString()), (bool.Parse(dtBankIn.Rows[0]["IsCheck"].ToString()) != rbIsCheck.Checked) ? "Null" : ((dtBankIn.Rows[0]["SubAccountID"] == DBNull.Value) ? "Null" : dtBankIn.Rows[0]["SubAccountID"].ToString()), (bool.Parse(dtBankIn.Rows[0]["IsCheck"].ToString()) != rbIsCheck.Checked) ? "Null" : ((dtBankIn.Rows[0]["CollectionDate"] == DBNull.Value) ? "Null" : DateTime.Parse(dtBankIn.Rows[0]["CollectionDate"].ToString()).ToString(GlobalVariables.DateLongFormate)), (bool.Parse(dtBankIn.Rows[0]["IsCheck"].ToString()) != rbIsCheck.Checked) ? "Null" : ((dtBankIn.Rows[0]["CollectionExpense"] == DBNull.Value) ? "Null" : dtBankIn.Rows[0]["CollectionExpense"].ToString()), "0", "Null", (dtBankIn.Rows[0]["JVID"] == DBNull.Value) ? "Null" : dtBankIn.Rows[0]["JVID"].ToString(), (bool.Parse(dtBankIn.Rows[0]["IsCheck"].ToString()) != rbIsCheck.Checked) ? "Null" : ((dtBankIn.Rows[0]["JVID2"] == DBNull.Value) ? "Null" : dtBankIn.Rows[0]["JVID2"].ToString()), (bool.Parse(dtBankIn.Rows[0]["IsCheck"].ToString()) != rbIsCheck.Checked) ? "Null" : ((dtBankIn.Rows[0]["JVID3"] == DBNull.Value) ? "Null" : dtBankIn.Rows[0]["JVID3"].ToString()), bool.Parse(dtBankIn.Rows[0]["Approved"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, bool.Parse(dtBankIn.Rows[0]["Deleted"].ToString()) ? "1" : "0", GlobalVariables.UserID);
				BankInDetails.DeleteByBankInID(dtBankIn.Rows[0]["BankInID"].ToString(), GlobalVariables.UserID);
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; l++)
				{
					if (((UltraGridBase)ULGData).Rows[l].Cells["IsPaid"].Value.Equals(true))
					{
						BankInDetails.Insert_Update("-1", num4.ToString(), ((UltraGridBase)ULGData).Rows[l].Cells["ContractInstallmentAmount"].Value.ToString(), dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["AccountID"].ToString(), dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"].ToString(), (((UltraGridBase)ULGData).Rows[l].Cells["CostCenterID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[l].Cells["CostCenterID"].Value.ToString(), "Null", "0", ((UltraGridBase)ULGData).Rows[l].Cells["Notes"].Value.ToString(), GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID);
					}
				}
				if (bool.Parse(drMaster["IsCheck"].ToString()) != rbIsCheck.Checked)
				{
					if (dtBankIn.Rows[0]["JVID2"] != DBNull.Value)
					{
						JV.DeleteVirtual(dtBankIn.Rows[0]["JVID2"].ToString(), GlobalVariables.UserID, IsFromServer: false);
					}
					if (dtBankIn.Rows[0]["JVID3"] != DBNull.Value)
					{
						JV.DeleteVirtual(dtBankIn.Rows[0]["JVID3"].ToString(), GlobalVariables.UserID, IsFromServer: false);
					}
					JVDetails.DeleteByJVID(dtBankIn.Rows[0]["JVID"].ToString(), GlobalVariables.UserID);
				}
				else
				{
					JVDetails.DeleteByJVID(dtBankIn.Rows[0]["JVID"].ToString(), GlobalVariables.UserID);
					if (dtBankIn.Rows[0]["JVID2"] != DBNull.Value)
					{
						JVDetails.DeleteByJVID(dtBankIn.Rows[0]["JVID2"].ToString(), GlobalVariables.UserID);
					}
					if (dtBankIn.Rows[0]["JVID3"] != DBNull.Value)
					{
						JVDetails.DeleteByJVID(dtBankIn.Rows[0]["JVID3"].ToString(), GlobalVariables.UserID);
					}
				}
				dtJvDetails = JVDetails.Select("0", "-1", GlobalVariables.IsArabic ? "1" : "0");
				DataRow dataRow2 = dtJvDetails.NewRow();
				dataRow2["JVDetailID"] = "-1";
				dataRow2["AccountID"] = (rbIsCheck.Checked ? dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["NRAccID"].ToString() : dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["AccountID"].ToString());
				dataRow2["SubAccountID"] = (rbIsCheck.Checked ? DBNull.Value : dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["SubAccountID"]);
				dataRow2["Debit"] = ((Control)(object)txtTotal).Text;
				dataRow2["Credit"] = "0";
				dataRow2["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
				dataRow2["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
				dataRow2["LocalDebit"] = decimal.Parse(dataRow2["Debit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
				dataRow2["LocalCredit"] = "0";
				dataRow2["Deleted"] = false;
				dataRow2["BranchID"] = GlobalVariables.CurrentBranchID;
				dtJvDetails.Rows.Add(dataRow2);
				for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; m++)
				{
					if (((UltraGridBase)ULGData).Rows[m].Cells["IsPaid"].Value.Equals(true))
					{
						dataRow2 = dtJvDetails.NewRow();
						dataRow2["JVDetailID"] = "-1";
						dataRow2["AccountID"] = dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["AccountID"];
						dataRow2["SubAccountID"] = dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"];
						dataRow2["CostCenterID"] = ((UltraGridBase)ULGData).Rows[m].Cells["CostCenterID"].Value;
						dataRow2["Debit"] = "0";
						dataRow2["Credit"] = ((UltraGridBase)ULGData).Rows[m].Cells["ContractInstallmentAmount"].Value.ToString();
						dataRow2["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
						dataRow2["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
						dataRow2["LocalDebit"] = "0";
						dataRow2["LocalCredit"] = decimal.Parse(dataRow2["Credit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
						dataRow2["Notes"] = ((UltraGridBase)ULGData).Rows[m].Cells["Notes"].Value;
						dataRow2["Deleted"] = false;
						dataRow2["BranchID"] = GlobalVariables.CurrentBranchID;
						dtJvDetails.Rows.Add(dataRow2);
					}
				}
				JV.GenerateJV_Update(JVNo: (dtBankIn.Rows[0]["JVDate"] == DBNull.Value || (DateTime.Parse(dtBankIn.Rows[0]["JVDate"].ToString()).Month == dtpDate.DateTime.Month && DateTime.Parse(dtBankIn.Rows[0]["JVDate"].ToString()).Year == dtpDate.DateTime.Year)) ? dtBankIn.Rows[0]["JVNo"].ToString() : JV.GetCode(dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, "3"), JVID: dtBankIn.Rows[0]["JVID"].ToString(), JVDate: dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), TransTypeID: "3", VoucherID: num4.ToString(), ReceiptNo: ((Control)(object)txtCode).Text, HNotes: " ( " + ((Control)(object)txtChargedPerson).Text + " ) " + ((Control)(object)txtNotes).Text, IsOpenningJv: "0", Approved: "1", dtJvDetails: dtJvDetails, Deleted: "0", BranchID: GlobalVariables.CurrentBranchID, IsInternalJV: "1", UserID: GlobalVariables.UserID);
				dtJvDetails.Clear();
				dataRow2.Delete();
				if (dtBankIn.Rows[0]["JVID2"] != DBNull.Value && bool.Parse(dtBankIn.Rows[0]["IsCheck"].ToString()) == rbIsCheck.Checked)
				{
					dataRow2 = dtJvDetails.NewRow();
					dataRow2["JVDetailID"] = "-1";
					dataRow2["AccountID"] = (bool.Parse(dtBankIn.Rows[0]["IsEndorsement"].ToString()) ? dtBankIn.Rows[0]["AccountID"] : dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["NRUnderCollectionAccID"].ToString());
					dataRow2["SubAccountID"] = (bool.Parse(dtBankIn.Rows[0]["IsEndorsement"].ToString()) ? dtBankIn.Rows[0]["SubAccountID"] : DBNull.Value);
					dataRow2["Debit"] = ((Control)(object)txtTotal).Text;
					dataRow2["Credit"] = "0";
					dataRow2["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
					dataRow2["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
					dataRow2["LocalDebit"] = decimal.Parse(dataRow2["Debit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
					dataRow2["LocalCredit"] = "0";
					dataRow2["Deleted"] = false;
					dataRow2["BranchID"] = GlobalVariables.CurrentBranchID;
					dtJvDetails.Rows.Add(dataRow2);
					dataRow2 = dtJvDetails.NewRow();
					dataRow2["JVDetailID"] = "-1";
					dataRow2["AccountID"] = dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["NRAccID"].ToString();
					dataRow2["Debit"] = "0";
					dataRow2["Credit"] = ((Control)(object)txtTotal).Text;
					dataRow2["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
					dataRow2["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
					dataRow2["LocalDebit"] = "0";
					dataRow2["LocalCredit"] = decimal.Parse(dataRow2["Credit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
					dataRow2["Deleted"] = false;
					dataRow2["BranchID"] = GlobalVariables.CurrentBranchID;
					dtJvDetails.Rows.Add(dataRow2);
					JV.GenerateJV_Update(dtBankIn.Rows[0]["JVID2"].ToString(), dtBankIn.Rows[0]["JVNo2"].ToString(), DateTime.Parse(dtBankIn.Rows[0]["UnderCollectionDate"].ToString()).ToString(GlobalVariables.DateLongFormate), "16", num4.ToString(), ((Control)(object)txtCode).Text, " ( " + ((Control)(object)txtChargedPerson).Text + " ) " + ((Control)(object)txtNotes).Text, "0", "1", dtJvDetails, "0", GlobalVariables.CurrentBranchID, "1", GlobalVariables.UserID);
					dtJvDetails.Clear();
					dataRow2.Delete();
				}
				if (dtBankIn.Rows[0]["JVID3"] != DBNull.Value && bool.Parse(dtBankIn.Rows[0]["IsCheck"].ToString()) == rbIsCheck.Checked)
				{
					string text = "";
					string text2 = "";
					if (dtBankIn.Rows[0]["ToSafe"].Equals(true))
					{
						DataTable dataTable = Safes.Select(dtBankIn.Rows[0]["SafeID"].ToString(), "-1", "1", IsFromServer: false);
						text = dataTable.Rows[0]["AccountID"].ToString();
						text2 = dataTable.Rows[0]["SubAccountID"].ToString();
					}
					dataRow2 = dtJvDetails.NewRow();
					dataRow2["JVDetailID"] = "-1";
					dataRow2["AccountID"] = (dtBankIn.Rows[0]["ToSafe"].Equals(true) ? text : dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["AccountID"]);
					dataRow2["SubAccountID"] = (dtBankIn.Rows[0]["ToSafe"].Equals(true) ? text2 : dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["SubAccountID"]);
					dataRow2["Debit"] = decimal.Parse(((Control)(object)txtTotal).Text) - ((dtBankIn.Rows[0]["CollectionExpense"] == DBNull.Value) ? 0m : decimal.Parse(dtBankIn.Rows[0]["CollectionExpense"].ToString()));
					dataRow2["Credit"] = "0";
					dataRow2["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
					dataRow2["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
					dataRow2["LocalDebit"] = decimal.Parse(dataRow2["Debit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
					dataRow2["LocalCredit"] = "0";
					dataRow2["Deleted"] = false;
					dataRow2["BranchID"] = GlobalVariables.CurrentBranchID;
					dtJvDetails.Rows.Add(dataRow2);
					if (dtBankIn.Rows[0]["CollectionExpense"] != DBNull.Value && decimal.Parse(dtBankIn.Rows[0]["CollectionExpense"].ToString()) > 0m)
					{
						dataRow2 = dtJvDetails.NewRow();
						dataRow2["JVDetailID"] = "-1";
						dataRow2["AccountID"] = dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["CollectionExpensesAccID"];
						dataRow2["Debit"] = ((dtBankIn.Rows[0]["CollectionExpense"] == DBNull.Value) ? 0m : decimal.Parse(dtBankIn.Rows[0]["CollectionExpense"].ToString()));
						dataRow2["Credit"] = "0";
						dataRow2["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
						dataRow2["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
						dataRow2["LocalDebit"] = decimal.Parse(dataRow2["Debit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
						dataRow2["LocalCredit"] = "0";
						dataRow2["Deleted"] = false;
						dataRow2["BranchID"] = GlobalVariables.CurrentBranchID;
						dtJvDetails.Rows.Add(dataRow2);
					}
					dataRow2 = dtJvDetails.NewRow();
					dataRow2["JVDetailID"] = "-1";
					dataRow2["AccountID"] = dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["NRUnderCollectionAccID"].ToString();
					dataRow2["Debit"] = "0";
					dataRow2["Credit"] = ((Control)(object)txtTotal).Text;
					dataRow2["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
					dataRow2["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
					dataRow2["LocalDebit"] = "0";
					dataRow2["LocalCredit"] = decimal.Parse(dataRow2["Credit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
					dataRow2["Deleted"] = false;
					dataRow2["BranchID"] = GlobalVariables.CurrentBranchID;
					dtJvDetails.Rows.Add(dataRow2);
					JV.GenerateJV_Update(dtBankIn.Rows[0]["JVID3"].ToString(), dtBankIn.Rows[0]["JVNo3"].ToString(), DateTime.Parse(dtBankIn.Rows[0]["CollectionDate"].ToString()).ToString(GlobalVariables.DateLongFormate), "17", num4.ToString(), ((Control)(object)txtCode).Text, " ( " + ((Control)(object)txtChargedPerson).Text + " ) " + ((Control)(object)txtNotes).Text, "0", "1", dtJvDetails, "0", GlobalVariables.CurrentBranchID, "1", GlobalVariables.UserID);
					dtJvDetails.Clear();
					dataRow2.Delete();
				}
			}
			else if (rbIsSafe.Checked && drMaster != null && !bool.Parse(drMaster["IsSafe"].ToString()))
			{
				if (drMaster["BankInID"] != DBNull.Value)
				{
					BankInDetails.DeleteVirtualByBankInID(drMaster["BankInID"].ToString(), GlobalVariables.UserID);
					BankIn.DeleteVirtual(drMaster["BankInID"].ToString(), GlobalVariables.UserID);
					if (dtBankIn != null && dtBankIn.Rows.Count > 0)
					{
						if (dtBankIn.Rows[0]["JVID2"] != DBNull.Value)
						{
							JV.DeleteVirtual(dtBankIn.Rows[0]["JVID2"].ToString(), GlobalVariables.UserID, IsFromServer: false);
						}
						if (dtBankIn.Rows[0]["JVID3"] != DBNull.Value)
						{
							JV.DeleteVirtual(dtBankIn.Rows[0]["JVID3"].ToString(), GlobalVariables.UserID, IsFromServer: false);
						}
						JVDetails.DeleteByJVID(dtBankIn.Rows[0]["JVID"].ToString(), GlobalVariables.UserID);
					}
					Main.ExecuteNonQuery(" Update Con_Payments  set BankInID=Null  Where PaymentID=" + num);
				}
				int num5 = SafeIn.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboSafe).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, ((Control)(object)txtChargedPerson).Text, ((Control)(object)txtNotes).Text, ((Control)(object)txtTotal).Text, "Null", "Null", "0", GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID);
				for (int n = 0; n < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; n++)
				{
					if (((UltraGridBase)ULGData).Rows[n].Cells["IsPaid"].Value.Equals(true))
					{
						SafeInDetails.Insert_Update("-1", num5.ToString(), ((UltraGridBase)ULGData).Rows[n].Cells["ContractInstallmentAmount"].Value.ToString(), dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["AccountID"].ToString(), dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"].ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["CostCenterID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[n].Cells["CostCenterID"].Value.ToString(), "Null", "0", ((UltraGridBase)ULGData).Rows[n].Cells["Notes"].Value.ToString(), GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID);
					}
				}
				dtJvDetails = JVDetails.Select("0", "-1", GlobalVariables.IsArabic ? "1" : "0");
				DataRow dataRow3 = dtJvDetails.NewRow();
				dataRow3["JVDetailID"] = "-1";
				dataRow3["AccountID"] = dtsafes.Select(" SafeID= " + ((TextEditorControlBase)cboSafe).Value.ToString())[0]["AccountID"];
				dataRow3["SubAccountID"] = dtsafes.Select(" SafeID= " + ((TextEditorControlBase)cboSafe).Value.ToString())[0]["SubAccountID"];
				dataRow3["Debit"] = ((Control)(object)txtTotal).Text;
				dataRow3["Credit"] = "0";
				dataRow3["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
				dataRow3["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
				dataRow3["LocalDebit"] = decimal.Parse(dataRow3["Debit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
				dataRow3["LocalCredit"] = "0";
				dataRow3["Deleted"] = false;
				dataRow3["BranchID"] = GlobalVariables.CurrentBranchID;
				dtJvDetails.Rows.Add(dataRow3);
				for (int num6 = 0; num6 < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; num6++)
				{
					if (((UltraGridBase)ULGData).Rows[num6].Cells["IsPaid"].Value.Equals(true))
					{
						dataRow3 = dtJvDetails.NewRow();
						dataRow3["JVDetailID"] = "-1";
						dataRow3["AccountID"] = dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["AccountID"];
						dataRow3["SubAccountID"] = dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"];
						dataRow3["CostCenterID"] = ((UltraGridBase)ULGData).Rows[num6].Cells["CostCenterID"].Value;
						dataRow3["Debit"] = "0";
						dataRow3["Credit"] = ((UltraGridBase)ULGData).Rows[num6].Cells["ContractInstallmentAmount"].Value.ToString();
						dataRow3["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
						dataRow3["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
						dataRow3["LocalDebit"] = "0";
						dataRow3["LocalCredit"] = decimal.Parse(dataRow3["Credit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
						dataRow3["Notes"] = ((UltraGridBase)ULGData).Rows[num6].Cells["Notes"].Value;
						dataRow3["Deleted"] = false;
						dataRow3["BranchID"] = GlobalVariables.CurrentBranchID;
						dtJvDetails.Rows.Add(dataRow3);
					}
				}
				string code2 = JV.GetCode(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, "1");
				int num7 = JV.GenerateJV_Insert(code2, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "1", num5.ToString(), ((Control)(object)txtCode).Text, " ( " + ((Control)(object)txtChargedPerson).Text + " ) " + (GlobalVariables.IsArabic ? " إيصال رقم " : " Receipt No ") + ((Control)(object)txtCode).Text + "  " + ((Control)(object)txtNotes).Text, "0", "1", dtJvDetails, "0", GlobalVariables.CurrentBranchID, "1", GlobalVariables.UserID);
				((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? ("(" + code2 + ")قيد رقم ") : ("JV No ( " + code2 + " )"));
				Main.ExecuteNonQuery(" Update SB_SafeIn  set JvID= " + num7 + " Where SafeInID=" + num5.ToString());
				Main.ExecuteNonQuery(" Update Con_Payments  set SafeInID= " + num5 + " Where PaymentID=" + num);
			}
			else if ((rbIsCheck.Checked || rbDeposit.Checked) && drMaster != null && bool.Parse(drMaster["IsSafe"].ToString()))
			{
				if (drMaster["SafeInID"] != DBNull.Value)
				{
					SafeInDetails.DeleteVirtualBySafeInID(drMaster["SafeInID"].ToString(), GlobalVariables.UserID);
					SafeIn.DeleteVirtual(drMaster["SafeInID"].ToString(), GlobalVariables.UserID);
					if (dtSafeIn != null && dtSafeIn.Rows.Count > 0)
					{
						JVDetails.DeleteByJVID(dtSafeIn.Rows[0]["JVID"].ToString(), GlobalVariables.UserID);
					}
					Main.ExecuteNonQuery(" Update Con_Payments  set SafeInID=Null  Where PaymentID=" + num);
				}
				int num8 = BankIn.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, rbIsCheck.Checked ? "1" : "0", ((Control)(object)txtCheckNo).Text, rbIsCheck.Checked ? dtpCheckDate.DateTime.ToString(GlobalVariables.DateLongFormate) : "Null", ((TextEditorControlBase)cboBank).Value.ToString(), ((Control)(object)txtChargedPerson).Text, ((Control)(object)txtNotes).Text, ((Control)(object)txtTotal).Text, ((Control)(object)txtReceivingBank).Text, (cboBinderSafe.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBinderSafe).Value.ToString(), (dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["NRAccID"].ToString() == dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["NRUnderCollectionAccID"].ToString()) ? "1" : "0", "Null", "0", "0", "0", "Null", "0", "Null", "Null", "Null", "Null", "0", "Null", "Null", "Null", "Null", "0", GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID);
				for (int num9 = 0; num9 < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; num9++)
				{
					if (((UltraGridBase)ULGData).Rows[num9].Cells["IsPaid"].Value.Equals(true))
					{
						BankInDetails.Insert_Update("-1", num8.ToString(), ((UltraGridBase)ULGData).Rows[num9].Cells["ContractInstallmentAmount"].Value.ToString(), dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["AccountID"].ToString(), dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"].ToString(), (((UltraGridBase)ULGData).Rows[num9].Cells["CostCenterID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[num9].Cells["CostCenterID"].Value.ToString(), "Null", "0", ((UltraGridBase)ULGData).Rows[num9].Cells["Notes"].Value.ToString(), GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID);
					}
				}
				dtJvDetails = JVDetails.Select("0", "-1", GlobalVariables.IsArabic ? "1" : "0");
				DataRow dataRow4 = dtJvDetails.NewRow();
				dataRow4["JVDetailID"] = "-1";
				dataRow4["AccountID"] = (rbIsCheck.Checked ? dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["NRAccID"].ToString() : dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["AccountID"].ToString());
				dataRow4["SubAccountID"] = (rbIsCheck.Checked ? DBNull.Value : dtBanks.Select(" BankID= " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["SubAccountID"]);
				dataRow4["Debit"] = ((Control)(object)txtTotal).Text;
				dataRow4["Credit"] = "0";
				dataRow4["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
				dataRow4["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
				dataRow4["LocalDebit"] = decimal.Parse(dataRow4["Debit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
				dataRow4["LocalCredit"] = "0";
				dataRow4["Deleted"] = false;
				dataRow4["BranchID"] = GlobalVariables.CurrentBranchID;
				dtJvDetails.Rows.Add(dataRow4);
				for (int num10 = 0; num10 < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; num10++)
				{
					if (((UltraGridBase)ULGData).Rows[num10].Cells["IsPaid"].Value.Equals(true))
					{
						dataRow4 = dtJvDetails.NewRow();
						dataRow4["JVDetailID"] = "-1";
						dataRow4["AccountID"] = dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["AccountID"];
						dataRow4["SubAccountID"] = dtClients.Select(" SubAccountID = " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"];
						dataRow4["CostCenterID"] = ((UltraGridBase)ULGData).Rows[num10].Cells["CostCenterID"].Value;
						dataRow4["Debit"] = "0";
						dataRow4["Credit"] = ((UltraGridBase)ULGData).Rows[num10].Cells["ContractInstallmentAmount"].Value.ToString();
						dataRow4["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
						dataRow4["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
						dataRow4["LocalDebit"] = "0";
						dataRow4["LocalCredit"] = decimal.Parse(dataRow4["Credit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
						dataRow4["Notes"] = ((UltraGridBase)ULGData).Rows[num10].Cells["Notes"].Value;
						dataRow4["Deleted"] = false;
						dataRow4["BranchID"] = GlobalVariables.CurrentBranchID;
						dtJvDetails.Rows.Add(dataRow4);
					}
				}
				string code3 = JV.GetCode(dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, "3");
				int num11 = JV.GenerateJV_Insert(code3, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "3", num8.ToString(), ((Control)(object)txtCode).Text, " ( " + ((Control)(object)txtChargedPerson).Text + " ) " + (GlobalVariables.IsArabic ? " إيصال رقم " : " Receipt No ") + ((Control)(object)txtCode).Text + "  " + ((Control)(object)txtNotes).Text, "0", "1", dtJvDetails, "0", GlobalVariables.CurrentBranchID, "1", GlobalVariables.UserID);
				((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? ("(" + code3 + ")قيد رقم ") : ("JV No ( " + code3 + " )"));
				Main.ExecuteNonQuery(" Update SB_BankIn  set JvID= " + num11 + " Where BankInID=" + num8.ToString());
				Main.ExecuteNonQuery(" Update Con_Payments  set BankInID= " + num8 + " Where PaymentID=" + num);
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

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			if (drMaster["SafeInID"] != DBNull.Value)
			{
				SafeInDetails.DeleteVirtualBySafeInID(drMaster["SafeInID"].ToString(), GlobalVariables.UserID);
				SafeIn.DeleteVirtual(drMaster["SafeInID"].ToString(), GlobalVariables.UserID);
				if (dtSafeIn != null && dtSafeIn.Rows.Count > 0)
				{
					JVDetails.DeleteByJVID(dtSafeIn.Rows[0]["JVID"].ToString(), GlobalVariables.UserID);
				}
			}
			if (drMaster["BankInID"] != DBNull.Value)
			{
				BankInDetails.DeleteVirtualByBankInID(drMaster["BankInID"].ToString(), GlobalVariables.UserID);
				BankIn.DeleteVirtual(drMaster["BankInID"].ToString(), GlobalVariables.UserID);
				if (dtBankIn != null && dtBankIn.Rows.Count > 0)
				{
					if (dtBankIn.Rows[0]["JVID2"] != DBNull.Value)
					{
						JV.DeleteVirtual(dtBankIn.Rows[0]["JVID2"].ToString(), GlobalVariables.UserID, IsFromServer: false);
					}
					if (dtBankIn.Rows[0]["JVID3"] != DBNull.Value)
					{
						JV.DeleteVirtual(dtBankIn.Rows[0]["JVID3"].ToString(), GlobalVariables.UserID, IsFromServer: false);
					}
					JVDetails.DeleteByJVID(dtBankIn.Rows[0]["JVID"].ToString(), GlobalVariables.UserID);
				}
			}
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				ContractsInstallments.UpdateContractInstallmentIDs("," + ((UltraGridBase)ULGData).Rows[i].Cells["ContractInstallmentID"].Value.ToString() + ",", "0", "Null", ((UltraGridBase)ULGData).Rows[i].Cells["Notes"].Value.ToString());
			}
			Payments.Delete(drMaster["PaymentID"].ToString(), GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnCancelClick()
	{
		if (Updating && RowID != "" && TableName != "")
		{
			UsersTransactions.DeleteByUserLoginID(GlobalVariables.UserLoginID, TableName, RowID);
		}
		Adding = false;
		Updating = false;
		if (SubAccountID != "0")
		{
			drMaster = null;
			((TextEditorControlBase)cboClient).Value = SubAccountID;
			DisplayData();
		}
		else if (RowID == "")
		{
			drMaster = null;
			DisplayData();
		}
		else
		{
			DataTable dataTable = Payments.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
		SetControls(NavMode: true);
	}

	public override void btnPrintClick()
	{
		string val = "";
		if (RowID != "")
		{
			if (dtReports.Rows.Count > 0)
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
				val = dtReports.Rows[0]["isoCode"].ToString();
			}
			else
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Con_Payments_A.rpt" : "Rep_Con_Payments_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@PaymentIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.PaymentsSearchReport();
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["PaymentID"].ToString();
			FillData();
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "InstallmentTypeID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ContractInstallmentDate" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ContractInstallmentAmount"))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)e.Cell.Column).Key != "IsPaid")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "IsPaid")
		{
			CalculateTotals();
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
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
		GlobalVariables.QuestionMB.Show("سوف يتم حذف الإذن وحذف القيد هل تريد حذف هذه البيانات؟", "This Voucher And its JV Will Be Deleted Are you Sure You Want To Delete This Information ?");
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

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalculateTotals();
	}

	private void FillCurrencyDropDown()
	{
		dtCurrency = Currency.FillCurrencyByDate(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", "CurrencyName");
		if (cboBank.SelectedIndex != -1)
		{
			((TextEditorControlBase)cboCurrency).Value = dtBanks.Select("BankID =  " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["CurrencyID"];
		}
	}

	private void dtpJVDate_ValueChanged(object sender, EventArgs e)
	{
		FillCurrencyDropDown();
		if (Adding)
		{
			((Control)(object)txtCode).Text = Payments.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void cboCurrency_ValueChanged(object sender, EventArgs e)
	{
		if (cboCurrency.SelectedIndex != -1)
		{
			((Control)(object)txtExchangeRate).Text = dtCurrency.Select(" CurrencyID= " + ((TextEditorControlBase)cboCurrency).Value.ToString())[0]["ExchangeRate"].ToString();
		}
		else
		{
			((Control)(object)txtExchangeRate).Text = "";
		}
	}

	private void CalculateTotals()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsPaid"].Value.ToString()))
			{
				num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ContractInstallmentAmount"].Value.ToString());
			}
		}
		((Control)(object)txtTotal).Text = num.ToString();
	}

	public override void btnRefreshDataClick()
	{
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		UseCurrency = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from Currency ").Rows[0][0].ToString()) > 1;
		dtBanks = Banks.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBank, dtBanks, "BankID", "BankName");
		dtInstallmentsTypes = InstallmentsTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInstallmentsTypes.ValueListItems.Clear();
		for (int i = 0; i < dtInstallmentsTypes.Rows.Count; i++)
		{
			vlInstallmentsTypes.ValueListItems.Add(dtInstallmentsTypes.Rows[i]["InstallmentTypeID"], dtInstallmentsTypes.Rows[i]["InstallmentTypeName"].ToString());
		}
		dtsafes = Safes.FillComboBySafeIDs(GlobalVariables.SafeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBinderSafe, dtsafes, "SafeID", "SafeName");
		GlobalFunctions.FillCombo(cboSafe, dtsafes, "SafeID", "SafeName");
		FillCurrencyDropDown();
	}

	private void txtExchangeRate_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void cboBank_ValueChanged(object sender, EventArgs e)
	{
		if (cboBank.SelectedIndex != -1 && !rbIsSafe.Checked)
		{
			((TextEditorControlBase)cboCurrency).Value = dtBanks.Select("BankID =  " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["CurrencyID"];
		}
	}

	private void btnJV_Click(object sender, EventArgs e)
	{
		if (rbIsSafe.Checked && dtSafeIn != null && dtSafeIn.Rows.Count > 0 && !Adding && !Updating && dtSafeIn.Rows[0]["JVID"] != DBNull.Value)
		{
			frmJV frmJV2 = new frmJV(int.Parse(dtSafeIn.Rows[0]["JVID"].ToString()));
			frmJV2.Size = new Size(base.Width, base.Height);
			frmJV2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmJV2.lblTitle).Text = (GlobalVariables.IsArabic ? "قيود اليومية" : "JV");
			frmJV2.ShowDialog();
		}
		else if (!rbIsSafe.Checked && dtBankIn != null && dtBankIn.Rows.Count > 0 && !Adding && !Updating && dtBankIn.Rows[0]["JVID"] != DBNull.Value)
		{
			frmJV frmJV3 = new frmJV(int.Parse(dtBankIn.Rows[0]["JVID"].ToString()));
			frmJV3.Size = new Size(base.Width, base.Height);
			frmJV3.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmJV3.lblTitle).Text = (GlobalVariables.IsArabic ? "قيود اليومية" : "JV");
			frmJV3.ShowDialog();
		}
	}

	private void btnJV2_Click(object sender, EventArgs e)
	{
		if (!rbIsSafe.Checked && dtBankIn != null && dtBankIn.Rows.Count > 0 && !Adding && !Updating && dtBankIn.Rows[0]["JVID2"] != DBNull.Value)
		{
			frmJV frmJV2 = new frmJV(int.Parse(dtBankIn.Rows[0]["JVID2"].ToString()));
			frmJV2.Size = new Size(base.Width, base.Height);
			frmJV2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmJV2.lblTitle).Text = (GlobalVariables.IsArabic ? "قيود اليومية" : "JV");
			frmJV2.ShowDialog();
		}
	}

	private void btnJV3_Click(object sender, EventArgs e)
	{
		if (!rbIsSafe.Checked && dtBankIn != null && dtBankIn.Rows.Count > 0 && !Adding && !Updating && dtBankIn.Rows[0]["JVID3"] != DBNull.Value)
		{
			frmJV frmJV2 = new frmJV(int.Parse(dtBankIn.Rows[0]["JVID3"].ToString()));
			frmJV2.Size = new Size(base.Width, base.Height);
			frmJV2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmJV2.lblTitle).Text = (GlobalVariables.IsArabic ? "قيود اليومية" : "JV");
			frmJV2.ShowDialog();
		}
	}

	private void btnChangeStatus_Click(object sender, EventArgs e)
	{
		if (dtBankIn != null && dtBankIn.Rows.Count > 0)
		{
			if (rbIsCheck.Checked && !bool.Parse(dtBankIn.Rows[0]["UnderCollection"].ToString()))
			{
				frmNPAndNRApproval frmNPAndNRApproval2 = new frmNPAndNRApproval(int.Parse(dtBankIn.Rows[0]["BankInID"].ToString()), IsBankIn: true);
				frmNPAndNRApproval2.Size = new Size(base.Width, base.Height);
				frmNPAndNRApproval2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmNPAndNRApproval2.lblTitle).Text = (GlobalVariables.IsArabic ? "تـرحـيـــــل ا ق  و  ا د" : "NP And NR Approval");
				frmNPAndNRApproval2.ShowDialog();
				DisplayData();
			}
			else if (rbIsCheck.Checked && !bool.Parse(dtBankIn.Rows[0]["Collected"].ToString()) && !bool.Parse(dtBankIn.Rows[0]["Returned"].ToString()))
			{
				frmNotesUnderCollection frmNotesUnderCollection2 = new frmNotesUnderCollection(int.Parse(dtBankIn.Rows[0]["BankInID"].ToString()), IsBankIn: true);
				frmNotesUnderCollection2.Size = new Size(base.Width, base.Height);
				frmNotesUnderCollection2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmNotesUnderCollection2.lblTitle).Text = (GlobalVariables.IsArabic ? "اوراق تحـت التحصيــل" : "Notes Under Collection");
				frmNotesUnderCollection2.ShowDialog();
				DisplayData();
			}
		}
	}

	private void RadioButtons_CheckedChanged(object sender, EventArgs e)
	{
		((EditorButtonControlBase)cboCurrency).ReadOnly = !rbIsSafe.Checked && !Adding && !Updating;
		((Control)(object)lblSafe).Visible = rbIsSafe.Checked;
		((Control)(object)cboSafe).Visible = rbIsSafe.Checked;
		((Control)(object)lblBank).Visible = !rbIsSafe.Checked;
		((Control)(object)cboBank).Visible = !rbIsSafe.Checked;
		((Control)(object)lblReceivingBank).Visible = !rbIsSafe.Checked;
		((Control)(object)txtReceivingBank).Visible = !rbIsSafe.Checked;
		((Control)(object)lblCheckNo).Visible = rbIsCheck.Checked;
		((Control)(object)txtCheckNo).Visible = rbIsCheck.Checked;
		((Control)(object)lblCheckDate).Visible = rbIsCheck.Checked;
		((Control)(object)dtpCheckDate).Visible = rbIsCheck.Checked;
		((Control)(object)cboBinderSafe).Visible = rbIsCheck.Checked;
		((Control)(object)lblCheckBinder).Visible = rbIsCheck.Checked;
		((Control)(object)btnChangeStatus).Visible = !Adding && !Updating && dtBankIn != null && dtBankIn.Rows.Count > 0 && rbIsCheck.Checked && !bool.Parse(dtBankIn.Rows[0]["Collected"].ToString()) && !bool.Parse(dtBankIn.Rows[0]["Returned"].ToString());
		((Control)(object)lblCheckStatus).Visible = !Adding && !Updating && dtBankIn != null && dtBankIn.Rows.Count > 0 && rbIsCheck.Checked;
		((Control)(object)btnJV2).Visible = rbIsCheck.Checked;
		((Control)(object)btnJV3).Visible = rbIsCheck.Checked;
		if (cboBank.SelectedIndex != -1)
		{
			((TextEditorControlBase)cboCurrency).Value = dtBanks.Select("BankID =  " + ((TextEditorControlBase)cboBank).Value.ToString())[0]["CurrencyID"];
		}
		((EditorButtonControlBase)cboCurrency).ReadOnly = !rbIsSafe.Checked && (Adding || Updating);
	}

	private void btnClientSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Clients("-1", "-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboClient).Value = num;
		}
	}

	private void btnSelectInstallment_Click(object sender, EventArgs e)
	{
		if (cboClient.SelectedIndex == -1)
		{
			return;
		}
		dtSearchResult = SearchFunctions.ContractsInstallmentsSearchReport(GlobalVariables.BranchIDs, ((TextEditorControlBase)cboClient).Value.ToString(), "0", "0");
		string text = ",";
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			text = text + dtSearchResult.Rows[i]["ContractInstallmentID"].ToString() + ",";
		}
		if (!(text != ","))
		{
			return;
		}
		DataTable dataTable = ContractsInstallments.SelectByContractInstallmentIDs(text, "-1", "1");
		for (int j = 0; j < dataTable.Rows.Count; j++)
		{
			if (dtDetails.Select(" ContractInstallmentID = " + dataTable.Rows[j]["ContractInstallmentID"].ToString()).Length == 0)
			{
				dataTable.Rows[j]["IsPaid"] = true;
				dtDetails.ImportRow(dataTable.Rows[j]);
			}
		}
		InitGrid();
		CalculateTotals();
		((EditorButtonControlBase)cboClient).ReadOnly = true;
		((Control)(object)btnClientSearch).Visible = false;
	}

	private void ULGData_ClickCellButton(object sender, CellEventArgs e)
	{
		if (!(((KeyedSubObjectBase)e.Cell.Column).Key == "Split") || (!Adding && !Updating))
		{
			return;
		}
		frmEnterValue frmEnterValue2 = new frmEnterValue(GlobalVariables.IsArabic ? "قيمة القسط الجديد" : "New Installment Value", _IsInt: false, _IsNumeric: true);
		frmEnterValue2.WindowState = FormWindowState.Normal;
		frmEnterValue2.ShowDialog();
		if (!(frmEnterValue2.Value != "") || !(decimal.Parse(frmEnterValue2.Value) > 0m) || !(decimal.Parse(frmEnterValue2.Value) < decimal.Parse(e.Cell.Row.Cells["ContractInstallmentAmount"].Value.ToString())))
		{
			return;
		}
		decimal num = decimal.Parse(e.Cell.Row.Cells["ContractInstallmentAmount"].Value.ToString()) - decimal.Parse(frmEnterValue2.Value);
		e.Cell.Row.Cells["ContractInstallmentAmount"].Value = num;
		Main.StartBulkTrans(FromServer: false);
		try
		{
			ContractsInstallments.Insert_Update(e.Cell.Row.Cells["ContractInstallmentID"].Value.ToString(), e.Cell.Row.Cells["InstallmentTypeID"].Value.ToString(), DateTime.Parse(e.Cell.Row.Cells["ContractInstallmentDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), e.Cell.Row.Cells["ContractInstallmentAmount"].Value.ToString(), bool.Parse(e.Cell.Row.Cells["IsPaid"].Value.ToString()) ? "1" : "0", e.Cell.Row.Cells["Notes"].Value.ToString(), e.Cell.Row.Cells["ContractID"].Value.ToString(), (e.Cell.Row.Cells["PaymentID"].Value == DBNull.Value) ? "Null" : e.Cell.Row.Cells["PaymentID"].Value.ToString(), (e.Cell.Row.Cells["JVID"].Value == DBNull.Value) ? "Null" : e.Cell.Row.Cells["JVID"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			DataTable dataTable = ContractsInstallments.SelectByContractInstallmentIDs("," + ContractsInstallments.Insert_Update("-1", e.Cell.Row.Cells["InstallmentTypeID"].Value.ToString(), DateTime.Parse(e.Cell.Row.Cells["ContractInstallmentDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), frmEnterValue2.Value, "0", e.Cell.Row.Cells["Notes"].Value.ToString(), e.Cell.Row.Cells["ContractID"].Value.ToString(), "Null", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID) + ",", "-1", "1");
			if (dataTable.Rows.Count > 0)
			{
				dtDetails.ImportRow(dataTable.Rows[0]);
			}
			ContractsInstallments.GenerateSalesJV(e.Cell.Row.Cells["ContractID"].Value.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			return;
		}
		InitGrid();
		CalculateTotals();
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
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Expected O, but got Unknown
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Expected O, but got Unknown
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Expected O, but got Unknown
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Expected O, but got Unknown
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Expected O, but got Unknown
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Expected O, but got Unknown
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Expected O, but got Unknown
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Expected O, but got Unknown
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Expected O, but got Unknown
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Expected O, but got Unknown
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Expected O, but got Unknown
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Expected O, but got Unknown
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Expected O, but got Unknown
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Expected O, but got Unknown
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Expected O, but got Unknown
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Expected O, but got Unknown
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Expected O, but got Unknown
		//IL_0622: Unknown result type (might be due to invalid IL or missing references)
		//IL_062c: Expected O, but got Unknown
		//IL_063a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0644: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Constructions.Transactions.frmPayments));
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
		this.pnlCheckType = new UltraPanel();
		this.rbIsSafe = new System.Windows.Forms.RadioButton();
		this.rbDeposit = new System.Windows.Forms.RadioButton();
		this.rbIsCheck = new System.Windows.Forms.RadioButton();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboCurrency = new UltraComboEditor();
		this.lblCurrency = new UltraLabel();
		this.lblBank = new UltraLabel();
		this.cboBank = new UltraComboEditor();
		this.txtExchangeRate = new UltraTextEditor();
		this.lblExchangrRate = new UltraLabel();
		this.txtChargedPerson = new UltraTextEditor();
		this.lblChargePerson = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblTotal = new UltraLabel();
		this.txtTotal = new UltraTextEditor();
		this.btnJV = new UltraButton();
		this.txtCheckNo = new UltraTextEditor();
		this.lblCheckNo = new UltraLabel();
		this.lblCheckDate = new UltraLabel();
		this.dtpCheckDate = new UltraDateTimeEditor();
		this.txtReceivingBank = new UltraTextEditor();
		this.lblReceivingBank = new UltraLabel();
		this.btnJV2 = new UltraButton();
		this.btnJV3 = new UltraButton();
		this.lblCheckStatus = new UltraLabel();
		this.btnChangeStatus = new UltraButton();
		this.lblCheckBinder = new UltraLabel();
		this.cboBinderSafe = new UltraComboEditor();
		this.lblSafe = new UltraLabel();
		this.cboSafe = new UltraComboEditor();
		this.btnSelectInstallment = new UltraButton();
		this.btnClientSearch = new UltraButton();
		this.cboClient = new UltraComboEditor();
		this.lblClient = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBank).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtChargedPerson).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotal).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCheckNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCheckDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtReceivingBank).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBinderSafe).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSafe).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).BeginInit();
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
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		base.ULGData.ClickCellButton += new CellEventHandler(ULGData_ClickCellButton);
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
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
		resources.ApplyResources(this.pnlCheckType, "pnlCheckType");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val9, "appearance13");
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(this.pnlCheckType.ClientArea, "pnlCheckType.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsSafe);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbDeposit);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsCheck);
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
		resources.ApplyResources(this.rbIsSafe, "rbIsSafe");
		this.rbIsSafe.BackColor = System.Drawing.Color.Transparent;
		this.rbIsSafe.Name = "rbIsSafe";
		this.rbIsSafe.TabStop = true;
		this.rbIsSafe.UseVisualStyleBackColor = false;
		this.rbIsSafe.CheckedChanged += new System.EventHandler(RadioButtons_CheckedChanged);
		resources.ApplyResources(this.rbDeposit, "rbDeposit");
		this.rbDeposit.BackColor = System.Drawing.Color.Transparent;
		this.rbDeposit.Name = "rbDeposit";
		this.rbDeposit.TabStop = true;
		this.rbDeposit.UseVisualStyleBackColor = false;
		this.rbDeposit.CheckedChanged += new System.EventHandler(RadioButtons_CheckedChanged);
		resources.ApplyResources(this.rbIsCheck, "rbIsCheck");
		this.rbIsCheck.BackColor = System.Drawing.Color.Transparent;
		this.rbIsCheck.Name = "rbIsCheck";
		this.rbIsCheck.TabStop = true;
		this.rbIsCheck.UseVisualStyleBackColor = false;
		this.rbIsCheck.CheckedChanged += new System.EventHandler(RadioButtons_CheckedChanged);
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpJVDate_ValueChanged);
		resources.ApplyResources(this.cboCurrency, "cboCurrency");
		((TextEditorControlBase)this.cboCurrency).AlwaysInEditMode = true;
		this.cboCurrency.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCurrency).Name = "cboCurrency";
		((TextEditorControlBase)this.cboCurrency).ValueChanged += new System.EventHandler(cboCurrency_ValueChanged);
		resources.ApplyResources(this.lblCurrency, "lblCurrency");
		this.lblCurrency.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCurrency).Name = "lblCurrency";
		((ControlBase)this.lblCurrency).WrapText = false;
		resources.ApplyResources(this.lblBank, "lblBank");
		((System.Windows.Forms.Control)(object)this.lblBank).Name = "lblBank";
		resources.ApplyResources(this.cboBank, "cboBank");
		((TextEditorControlBase)this.cboBank).AlwaysInEditMode = true;
		this.cboBank.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboBank).Name = "cboBank";
		((TextEditorControlBase)this.cboBank).ValueChanged += new System.EventHandler(cboBank_ValueChanged);
		resources.ApplyResources(this.txtExchangeRate, "txtExchangeRate");
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).Name = "txtExchangeRate";
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtExchangeRate_KeyPress);
		resources.ApplyResources(this.lblExchangrRate, "lblExchangrRate");
		this.lblExchangrRate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExchangrRate).Name = "lblExchangrRate";
		((ControlBase)this.lblExchangrRate).WrapText = false;
		resources.ApplyResources(this.txtChargedPerson, "txtChargedPerson");
		((System.Windows.Forms.Control)(object)this.txtChargedPerson).Name = "txtChargedPerson";
		resources.ApplyResources(this.lblChargePerson, "lblChargePerson");
		this.lblChargePerson.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblChargePerson).Name = "lblChargePerson";
		((ControlBase)this.lblChargePerson).WrapText = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblTotal, "lblTotal");
		this.lblTotal.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotal).Name = "lblTotal";
		((ControlBase)this.lblTotal).WrapText = false;
		resources.ApplyResources(this.txtTotal, "txtTotal");
		((System.Windows.Forms.Control)(object)this.txtTotal).Name = "txtTotal";
		resources.ApplyResources(this.btnJV, "btnJV");
		((System.Windows.Forms.Control)(object)this.btnJV).Name = "btnJV";
		((System.Windows.Forms.Control)(object)this.btnJV).Click += new System.EventHandler(btnJV_Click);
		resources.ApplyResources(this.txtCheckNo, "txtCheckNo");
		((System.Windows.Forms.Control)(object)this.txtCheckNo).Name = "txtCheckNo";
		resources.ApplyResources(this.lblCheckNo, "lblCheckNo");
		this.lblCheckNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCheckNo).Name = "lblCheckNo";
		((ControlBase)this.lblCheckNo).WrapText = false;
		resources.ApplyResources(this.lblCheckDate, "lblCheckDate");
		this.lblCheckDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCheckDate).Name = "lblCheckDate";
		((ControlBase)this.lblCheckDate).WrapText = false;
		resources.ApplyResources(this.dtpCheckDate, "dtpCheckDate");
		((UltraWinEditorMaskedControlBase)this.dtpCheckDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpCheckDate).Name = "dtpCheckDate";
		resources.ApplyResources(this.txtReceivingBank, "txtReceivingBank");
		((System.Windows.Forms.Control)(object)this.txtReceivingBank).Name = "txtReceivingBank";
		resources.ApplyResources(this.lblReceivingBank, "lblReceivingBank");
		this.lblReceivingBank.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReceivingBank).Name = "lblReceivingBank";
		((ControlBase)this.lblReceivingBank).WrapText = false;
		resources.ApplyResources(this.btnJV2, "btnJV2");
		((System.Windows.Forms.Control)(object)this.btnJV2).Name = "btnJV2";
		((System.Windows.Forms.Control)(object)this.btnJV2).Click += new System.EventHandler(btnJV2_Click);
		resources.ApplyResources(this.btnJV3, "btnJV3");
		((System.Windows.Forms.Control)(object)this.btnJV3).Name = "btnJV3";
		((System.Windows.Forms.Control)(object)this.btnJV3).Click += new System.EventHandler(btnJV3_Click);
		resources.ApplyResources(this.lblCheckStatus, "lblCheckStatus");
		resources.ApplyResources(val10, "appearance9");
		((ControlBase)this.lblCheckStatus).Appearance = (AppearanceBase)(object)val10;
		this.lblCheckStatus.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCheckStatus).Name = "lblCheckStatus";
		((ControlBase)this.lblCheckStatus).WrapText = false;
		resources.ApplyResources(this.btnChangeStatus, "btnChangeStatus");
		((System.Windows.Forms.Control)(object)this.btnChangeStatus).Name = "btnChangeStatus";
		((System.Windows.Forms.Control)(object)this.btnChangeStatus).Click += new System.EventHandler(btnChangeStatus_Click);
		resources.ApplyResources(this.lblCheckBinder, "lblCheckBinder");
		resources.ApplyResources(val11, "appearance14");
		((ControlBase)this.lblCheckBinder).Appearance = (AppearanceBase)(object)val11;
		this.lblCheckBinder.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCheckBinder).Name = "lblCheckBinder";
		((ControlBase)this.lblCheckBinder).WrapText = false;
		resources.ApplyResources(this.cboBinderSafe, "cboBinderSafe");
		((TextEditorControlBase)this.cboBinderSafe).AlwaysInEditMode = true;
		this.cboBinderSafe.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboBinderSafe).Name = "cboBinderSafe";
		resources.ApplyResources(this.lblSafe, "lblSafe");
		this.lblSafe.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSafe).Name = "lblSafe";
		((ControlBase)this.lblSafe).WrapText = false;
		resources.ApplyResources(this.cboSafe, "cboSafe");
		((TextEditorControlBase)this.cboSafe).AlwaysInEditMode = true;
		this.cboSafe.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSafe).Name = "cboSafe";
		resources.ApplyResources(this.btnSelectInstallment, "btnSelectInstallment");
		((System.Windows.Forms.Control)(object)this.btnSelectInstallment).Name = "btnSelectInstallment";
		((System.Windows.Forms.Control)(object)this.btnSelectInstallment).Click += new System.EventHandler(btnSelectInstallment_Click);
		resources.ApplyResources(this.btnClientSearch, "btnClientSearch");
		((AppearanceBase)val12).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val12, "appearance15");
		((ControlBase)this.btnClientSearch).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Name = "btnClientSearch";
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Click += new System.EventHandler(btnClientSearch_Click);
		resources.ApplyResources(this.cboClient, "cboClient");
		((TextEditorControlBase)this.cboClient).AlwaysInEditMode = true;
		this.cboClient.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClient).Name = "cboClient";
		resources.ApplyResources(this.lblClient, "lblClient");
		this.lblClient.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClient).Name = "lblClient";
		((ControlBase)this.lblClient).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSelectInstallment);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSafe);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSafe);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBinderSafe);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCheckBinder);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnChangeStatus);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCheckStatus);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnJV3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnJV2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtReceivingBank);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReceivingBank);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCheckDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpCheckDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCheckNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCheckNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnJV);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtChargedPerson);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblChargePerson);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBank);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBank);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangrRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmPayments";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangrRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBank, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBank, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblChargePerson, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtChargedPerson, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnJV, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCheckNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCheckNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpCheckDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCheckDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReceivingBank, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtReceivingBank, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnJV2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnJV3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCheckStatus, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnChangeStatus, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCheckBinder, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBinderSafe, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSafe, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSafe, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSelectInstallment, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientSearch, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBank).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtChargedPerson).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotal).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCheckNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCheckDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtReceivingBank).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBinderSafe).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSafe).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
