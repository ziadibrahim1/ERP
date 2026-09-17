using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.HR;
using BusinessLayer.Privilege;
using BusinessLayer.SafesAndBanks;
using BusinessLayer.Sales;
using BusinessLayer.Security;
using ERP.AbstractForms;
using ERP.Accounting.Transactions;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Sales.Transactions;

public class frmClientsPayments : frmHeaderDetails
{
	private DataTable dtClients;

	private DataTable dtCurrency;

	private DataTable dtsafes;

	private DataTable dtReports;

	private DataTable dtJvDetails;

	private DataTable dtSalesMan;

	private DataTable dtSafeIn;

	private bool UseCurrency;

	private ValueList vlClients = new ValueList();

	private ValueList vlClientsCodes = new ValueList();

	private IContainer components = null;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboCurrency;

	private UltraLabel lblCurrency;

	private UltraLabel lblBank;

	private UltraTextEditor txtExchangeRate;

	private UltraLabel lblExchangrRate;

	private UltraTextEditor txtChargedPerson;

	private UltraLabel lblChargePerson;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblTotal;

	private UltraTextEditor txtTotal;

	public UltraButton btnJV;

	private UltraLabel lblSafe;

	private UltraComboEditor cboSafe;

	private UltraLabel lblTotalPaidAmount;

	private UltraTextEditor txtPaidAmount;

	public UltraButton btnSalesManSearch;

	private UltraComboEditor cboSalesMan;

	private UltraLabel lblSalesMan;

	public frmClientsPayments()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
		TableName = "SL_ClientsPayments";
		IDCol = "ClientPaymentID";
		NoCol = "ClientPaymentNo";
		DateCol = "ClientPaymentDate";
		((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? "قيد " : "JV");
	}

	public frmClientsPayments(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtpDate.ValueChanged -= dtpJVDate_ValueChanged;
		dtpDate.DateTime = DateTime.Now;
		dtpDate.ValueChanged += dtpJVDate_ValueChanged;
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlClients.ValueListItems.Clear();
		vlClientsCodes.ValueListItems.Clear();
		for (int i = 0; i < dtClients.Rows.Count; i++)
		{
			vlClients.ValueListItems.Add(dtClients.Rows[i]["SubAccountID"], dtClients.Rows[i]["SubAccountName"].ToString());
			vlClientsCodes.ValueListItems.Add(dtClients.Rows[i]["SubAccountID"], dtClients.Rows[i]["ClientSupplierNo"].ToString());
		}
		dtSalesMan = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.BranchIDs, "1", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSalesMan, dtSalesMan, "SubAccountID", "SubAccountName");
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		UseCurrency = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from Currency ").Rows[0][0].ToString()) > 1;
		dtsafes = Safes.FillComboBySafeIDs(GlobalVariables.SafeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSafe, dtsafes, "SafeID", "SafeName");
		FillCurrencyDropDown();
		dtDetails = ClientsPaymentsDetails.SelectByClientPaymentID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
			DisplayData();
			return;
		}
		DataTable dataTable = ClientsPayments.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
			dtpDate.ValueChanged -= dtpJVDate_ValueChanged;
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			((TextEditorControlBase)cboSalesMan).ValueChanged -= cboSalesMan_ValueChanged;
			dtSafeIn = null;
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["ClientPaymentNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["ClientPaymentDate"];
			((TextEditorControlBase)cboSalesMan).Value = drMaster["DefaultSalesManSubAccountID"];
			((TextEditorControlBase)cboSafe).Value = drMaster["SafeID"];
			((TextEditorControlBase)cboCurrency).Value = drMaster["CurrencyID"];
			((Control)(object)txtExchangeRate).Text = decimal.Parse(drMaster["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtChargedPerson).Text = drMaster["ChargedPerson"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtPaidAmount).Text = decimal.Parse(drMaster["Total"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if (drMaster["SafeInID"] != DBNull.Value)
			{
				dtSafeIn = SafeIn.Select(drMaster["SafeInID"].ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0");
			}
			if (dtSafeIn != null && dtSafeIn.Rows.Count > 0)
			{
				((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? ("(" + dtSafeIn.Rows[0]["JVNo"].ToString() + ")قيد") : (" No( " + dtSafeIn.Rows[0]["JVNo"].ToString() + " )"));
				((Control)(object)btnJV).Visible = ((dtSafeIn.Rows[0]["JVNo"] != DBNull.Value) ? true : false) && CanViewJV;
			}
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = ClientsPaymentsDetails.SelectByClientPaymentID(drMaster["ClientPaymentID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
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
			((TextEditorControlBase)cboSalesMan).ValueChanged += cboSalesMan_ValueChanged;
		}
		else
		{
			ClearControls();
			((Control)(object)btnJV).Visible = false;
		}
		((TextEditorControlBase)txtCode).Focus();
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSalesMan).ReadOnly = !Adding;
		((Control)(object)btnSalesManSearch).Visible = !NavMode && Adding;
		((EditorButtonControlBase)cboSafe).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCurrency).ReadOnly = NavMode;
		((EditorButtonControlBase)txtExchangeRate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtChargedPerson).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTotal).ReadOnly = true;
		((EditorButtonControlBase)txtPaidAmount).ReadOnly = true;
		((Control)(object)btnJV).Visible = NavMode && drMaster != null && CanViewJV;
		vlClients.ValueListItems.Clear();
		vlClientsCodes.ValueListItems.Clear();
		for (int i = 0; i < dtClients.Rows.Count; i++)
		{
			vlClients.ValueListItems.Add(dtClients.Rows[i]["SubAccountID"], dtClients.Rows[i]["SubAccountName"].ToString());
			vlClientsCodes.ValueListItems.Add(dtClients.Rows[i]["SubAccountID"], dtClients.Rows[i]["ClientSupplierNo"].ToString());
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlClients;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountCode"].ValueList = (IValueList)(object)vlClientsCodes;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		cboSafe.SelectedIndex = ((((DisposableObjectCollectionBase)cboSafe.Items).Count <= 0) ? (-1) : 0);
		dtpDate.DateTime = DateTime.Now;
		cboCurrency.SelectedIndex = -1;
		((Control)(object)txtCode).Text = (Adding ? ClientsPayments.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)cboSalesMan).ValueChanged -= cboSalesMan_ValueChanged;
		cboSalesMan.SelectedIndex = -1;
		((TextEditorControlBase)cboSalesMan).ValueChanged += cboSalesMan_ValueChanged;
		((TextEditorControlBase)txtChargedPerson).Clear();
		((TextEditorControlBase)txtNotes).Clear();
		((Control)(object)txtTotal).Text = "0";
		((Control)(object)txtPaidAmount).Text = "0";
		((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? "قيد" : "JV");
		dtSafeIn = null;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientPaymentDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Value"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Balance"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "اسم العميل" : "Client Name");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود العميل" : "Client Code");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Value"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة المسدده" : "Paid Amount");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Balance"].Header).Caption = (GlobalVariables.IsArabic ? "الرصيد" : "Balance");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Value"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Balance"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlClients;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountCode"].ValueList = (IValueList)(object)vlClientsCodes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Value"].DefaultCellValue = 0;
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
		if (Updating && DateTime.Parse(drMaster["ClientPaymentDate"].ToString()).Year != dtpDate.DateTime.Year)
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
		if (Main.CheckForValueByBranchIDAndFiscalYearID("SL_ClientsPayments", "ClientPaymentNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["ClientPaymentNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = ClientsPayments.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
			if (cboSafe.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار الخزينة", "Please select Safe");
				((TextEditorControlBase)cboSafe).Focus();
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
			if (decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text) == 0m)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "المبلغ المدفوع لايجب ان يساوي صفرا" : "Paid Amount Cannot Be Zero");
				return false;
			}
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا الإذن", "Please insert details for this Voucher");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم العميل  ", "Please Enter Client Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"];
				((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].DroppedDown = true;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["Value"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Value"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال القيمة المدفوعة  ", "Please Enter Paid Amount");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Value"];
				((UltraGridBase)ULGData).Rows[i].Cells["Value"].DroppedDown = true;
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show("لا يمكن تكرار العميل ", "Cannot Duplicate The Same Client");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"];
					return false;
				}
			}
		}
		return true;
	}

	public override void AddData()
	{
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			string text = ((((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0) ? "" : (GlobalVariables.IsArabic ? "  سداد عملاء رقم  " : " Clients Payment No "));
			int num = ClientsPayments.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "Null", (cboSalesMan.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesMan).Value.ToString(), (cboSafe.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSafe).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, ((Control)(object)txtChargedPerson).Text, ((Control)(object)txtNotes).Text, ((Control)(object)txtPaidAmount).Text, "0", GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID);
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ClientPaymentID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				ClientsPaymentsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			if (((Control)(object)txtPaidAmount).Text != "" && decimal.Parse(((Control)(object)txtPaidAmount).Text) > 0m)
			{
				string codeByBranchID = SafeIn.GetCodeByBranchID((cboSafe.SelectedIndex == -1) ? "" : ((TextEditorControlBase)cboSafe).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
				int num2 = SafeIn.Insert_Update("-1", codeByBranchID, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboSafe).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, ((Control)(object)txtChargedPerson).Text, (GlobalVariables.IsArabic ? " إيصال رقم " : " Receipt No ") + ((Control)(object)txtCode).Text + "  " + ((Control)(object)txtNotes).Text, ((Control)(object)txtPaidAmount).Text, "Null", "Null", "0", GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID);
				dtJvDetails = JVDetails.Select("0", "-1", GlobalVariables.IsArabic ? "1" : "0");
				DataRow dataRow = dtJvDetails.NewRow();
				dataRow["JVDetailID"] = "-1";
				dataRow["AccountID"] = dtsafes.Select(" SafeID= " + ((TextEditorControlBase)cboSafe).Value.ToString())[0]["AccountID"];
				dataRow["SubAccountID"] = dtsafes.Select(" SafeID= " + ((TextEditorControlBase)cboSafe).Value.ToString())[0]["SubAccountID"];
				dataRow["Debit"] = ((Control)(object)txtPaidAmount).Text;
				dataRow["Credit"] = "0";
				dataRow["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
				dataRow["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
				dataRow["LocalDebit"] = decimal.Parse(dataRow["Debit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
				dataRow["LocalCredit"] = "0";
				dataRow["Deleted"] = false;
				dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
				dtJvDetails.Rows.Add(dataRow);
				DataTable dataTable = SafeInDetails.SelectBySafeInID("-1", GlobalVariables.UserID);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["SafeInDetailsID"] = -1;
					dataRow2["SafeInID"] = num2;
					dataRow2["Value"] = ((UltraGridBase)ULGData).Rows[j].Cells["Value"].Value;
					dataRow2["AccountID"] = ((UltraGridBase)ULGData).Rows[j].Cells["AccountID"].Value;
					dataRow2["SubAccountID"] = ((UltraGridBase)ULGData).Rows[j].Cells["SubAccountID"].Value;
					dataRow2["Notes"] = "";
					dataRow2["BranchID"] = GlobalVariables.CurrentBranchID;
					dataRow2["Deleted"] = 0;
					dataTable.Rows.Add(dataRow2);
					dataRow = dtJvDetails.NewRow();
					dataRow["JVDetailID"] = "-1";
					dataRow["AccountID"] = ((UltraGridBase)ULGData).Rows[j].Cells["AccountID"].Value;
					dataRow["SubAccountID"] = ((UltraGridBase)ULGData).Rows[j].Cells["SubAccountID"].Value;
					dataRow["Debit"] = "0";
					dataRow["Credit"] = ((UltraGridBase)ULGData).Rows[j].Cells["Value"].Value;
					dataRow["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
					dataRow["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
					dataRow["LocalDebit"] = "0";
					dataRow["LocalCredit"] = decimal.Parse(dataRow["Credit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
					dataRow["Notes"] = "";
					dataRow["Deleted"] = false;
					dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
					dtJvDetails.Rows.Add(dataRow);
				}
				SafeInDetails.Insert_UpdateByTable(dataTable, GlobalVariables.UserID);
				string code = JV.GetCode(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, "1");
				int num3 = JV.GenerateJV_Insert(code, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "1", num2.ToString(), codeByBranchID, " ( " + ((Control)(object)txtChargedPerson).Text + " ) " + (GlobalVariables.IsArabic ? " إيصال رقم " : " Receipt No ") + ((Control)(object)txtCode).Text + "  " + ((Control)(object)txtNotes).Text, "0", "1", dtJvDetails, "0", GlobalVariables.CurrentBranchID, "1", GlobalVariables.UserID);
				((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? ("(" + code + ")قيد رقم ") : ("JV No ( " + code + " )"));
				Main.ExecuteNonQuery(" Update SB_SafeIn  set JvID= " + num3 + " Where SafeInID=" + num2.ToString());
				Main.ExecuteNonQuery(" Update SL_ClientsPayments  set SafeInID= " + num2 + " Where ClientPaymentID=" + num);
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
			int num = ClientsPayments.Insert_Update(drMaster["ClientPaymentID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (drMaster["SafeInID"] == DBNull.Value) ? "Null" : drMaster["SafeInID"].ToString(), (cboSalesMan.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesMan).Value.ToString(), (cboSafe.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSafe).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, ((Control)(object)txtChargedPerson).Text, ((Control)(object)txtNotes).Text, ((Control)(object)txtPaidAmount).Text, "0", GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["ClientPaymentDetailID"].Value.ToString() + ",";
				((UltraGridBase)ULGData).Rows[i].Cells["ClientPaymentID"].Value = num;
			}
			Main.DeleteForUpdate("SL_ClientsPaymentsDetails", "ClientPaymentID", drMaster["ClientPaymentID"].ToString(), "ClientPaymentDetailID", text);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				ClientsPaymentsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			}
			int num2 = SafeIn.Insert_Update(dtSafeIn.Rows[0]["SafeInID"].ToString(), dtSafeIn.Rows[0]["SafeInNo"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboSafe).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), ((Control)(object)txtExchangeRate).Text, ((Control)(object)txtChargedPerson).Text, (GlobalVariables.IsArabic ? " إيصال رقم " : " Receipt No ") + ((Control)(object)txtCode).Text + "  " + ((Control)(object)txtNotes).Text, ((Control)(object)txtPaidAmount).Text, "Null", dtSafeIn.Rows[0]["JVID"].ToString(), bool.Parse(dtSafeIn.Rows[0]["Approved"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID);
			SafeInDetails.DeleteBySafeInID(num2.ToString(), GlobalVariables.UserID);
			dtJvDetails = JVDetails.Select("0", "-1", GlobalVariables.IsArabic ? "1" : "0");
			DataRow dataRow = dtJvDetails.NewRow();
			dataRow["JVDetailID"] = "-1";
			dataRow["AccountID"] = dtsafes.Select(" SafeID= " + ((TextEditorControlBase)cboSafe).Value.ToString())[0]["AccountID"];
			dataRow["SubAccountID"] = dtsafes.Select(" SafeID= " + ((TextEditorControlBase)cboSafe).Value.ToString())[0]["SubAccountID"];
			dataRow["Debit"] = ((Control)(object)txtPaidAmount).Text;
			dataRow["Credit"] = "0";
			dataRow["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
			dataRow["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
			dataRow["LocalDebit"] = decimal.Parse(dataRow["Debit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
			dataRow["LocalCredit"] = "0";
			dataRow["Deleted"] = false;
			dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
			dtJvDetails.Rows.Add(dataRow);
			DataTable dataTable = SafeInDetails.SelectBySafeInID("-1", GlobalVariables.UserID);
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["SafeInDetailsID"] = -1;
				dataRow2["SafeInID"] = num2;
				dataRow2["Value"] = ((UltraGridBase)ULGData).Rows[j].Cells["Value"].Value;
				dataRow2["AccountID"] = ((UltraGridBase)ULGData).Rows[j].Cells["AccountID"].Value;
				dataRow2["SubAccountID"] = ((UltraGridBase)ULGData).Rows[j].Cells["SubAccountID"].Value;
				dataRow2["Notes"] = "";
				dataRow2["BranchID"] = GlobalVariables.CurrentBranchID;
				dataRow2["Deleted"] = 0;
				dataTable.Rows.Add(dataRow2);
				dataRow = dtJvDetails.NewRow();
				dataRow["JVDetailID"] = "-1";
				dataRow["AccountID"] = ((UltraGridBase)ULGData).Rows[j].Cells["AccountID"].Value;
				dataRow["SubAccountID"] = ((UltraGridBase)ULGData).Rows[j].Cells["SubAccountID"].Value;
				dataRow["Debit"] = "0";
				dataRow["Credit"] = ((UltraGridBase)ULGData).Rows[j].Cells["Value"].Value;
				dataRow["CurrencyID"] = ((TextEditorControlBase)cboCurrency).Value.ToString();
				dataRow["ExchangeRate"] = ((Control)(object)txtExchangeRate).Text;
				dataRow["LocalDebit"] = "0";
				dataRow["LocalCredit"] = decimal.Parse(dataRow["Credit"].ToString()) * decimal.Parse(((Control)(object)txtExchangeRate).Text);
				dataRow["Notes"] = "";
				dataRow["Deleted"] = false;
				dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
				dtJvDetails.Rows.Add(dataRow);
			}
			SafeInDetails.Insert_UpdateByTable(dataTable, GlobalVariables.UserID);
			string code = JV.GetCode(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, "1");
			int num3 = JV.GenerateJV_Insert(code, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "1", num2.ToString(), dtSafeIn.Rows[0]["SafeInNo"].ToString(), " ( " + ((Control)(object)txtChargedPerson).Text + " ) " + (GlobalVariables.IsArabic ? " إيصال رقم " : " Receipt No ") + ((Control)(object)txtCode).Text + "  " + ((Control)(object)txtNotes).Text, "0", "1", dtJvDetails, "0", GlobalVariables.CurrentBranchID, "1", GlobalVariables.UserID);
			((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? ("(" + code + ")قيد رقم ") : ("JV No ( " + code + " )"));
			Main.ExecuteNonQuery(" Update SB_SafeIn  set JvID= " + num3 + " Where SafeInID=" + num2.ToString());
			Main.ExecuteNonQuery(" Update SL_ClientsPayments  set SafeInID= " + num2 + " Where ClientPaymentID=" + num);
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
			SafeInDetails.DeleteVirtualBySafeInID(drMaster["SafeInID"].ToString(), GlobalVariables.UserID);
			SafeIn.DeleteVirtual(drMaster["SafeInID"].ToString(), GlobalVariables.UserID);
			if (dtSafeIn != null && dtSafeIn.Rows.Count > 0)
			{
				JVDetails.DeleteByJVID(dtSafeIn.Rows[0]["JVID"].ToString(), GlobalVariables.UserID);
			}
			ClientsPaymentsDetails.DeleteVirtualByClientPaymentID(drMaster["ClientPaymentID"].ToString(), GlobalVariables.UserID);
			ClientsPayments.DeleteVirtual(drMaster["ClientPaymentID"].ToString(), GlobalVariables.UserID);
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
		if (RowID == "")
		{
			drMaster = null;
			DisplayData();
		}
		else
		{
			DataTable dataTable = ClientsPayments.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_SL_ClientsPayments_A.rpt" : "Rep_SL_ClientsPayments_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@ClientPaymentIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ClientsPaymentsSearchReport(-1, 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["ClientPaymentID"].ToString();
			FillData();
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Balance")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (e.Cell != null && ((KeyedSubObjectBase)e.Cell.Column).Key == "Value")
		{
			CalculateTotalsPaid();
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
		CalculateTotalsPaid();
	}

	private void FillCurrencyDropDown()
	{
		dtCurrency = Currency.FillCurrencyByDate(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", "CurrencyName");
	}

	private void dtpJVDate_ValueChanged(object sender, EventArgs e)
	{
		FillCurrencyDropDown();
		if (Adding)
		{
			((Control)(object)txtCode).Text = ClientsPayments.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void cboCurrency_ValueChanged(object sender, EventArgs e)
	{
		if (cboCurrency.SelectedIndex != -1)
		{
			((Control)(object)txtExchangeRate).Text = decimal.Parse(dtCurrency.Select(" CurrencyID= " + ((TextEditorControlBase)cboCurrency).Value.ToString())[0]["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			string currencyID = ((TextEditorControlBase)cboCurrency).Value.ToString();
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["Balance"].Value = CalculateClientBalance(((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value.ToString(), currencyID);
			}
			CalculateTotals();
		}
		else
		{
			((Control)(object)txtExchangeRate).Text = "";
		}
	}

	private void cboSalesMan_ValueChanged(object sender, EventArgs e)
	{
		if (cboSalesMan.SelectedIndex != -1)
		{
			getClientsValueList(((TextEditorControlBase)cboSalesMan).Value.ToString());
		}
	}

	private void getClientsValueList(string SalesmanSubAcountID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		ValueList val = new ValueList();
		ValueList val2 = new ValueList();
		DataRow[] array = dtClients.Select("EmployeeID =" + SalesmanSubAcountID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["SubAccountID"].ToString(), array[i]["SubAccountName"].ToString());
			val2.ValueListItems.Add((object)array[i]["SubAccountID"].ToString(), array[i]["ClientSupplierNo"].ToString());
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)val;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountCode"].ValueList = (IValueList)(object)val2;
	}

	private void btnSalesManSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Employees("1", "1", IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboSalesMan).Value = num;
		}
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Expected O, but got Unknown
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).UpdateData();
		if ((((KeyedSubObjectBase)e.Cell.Column).Key == "SubAccountCode" || ((KeyedSubObjectBase)e.Cell.Column).Key == "SubAccountID") && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			DataRow dataRow = dtClients.Select(" SubAccountID= " + e.Cell.Value.ToString())[0];
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["SubAccountCode"];
			object value = (((UltraGridBase)ULGData).ActiveRow.Cells["SubAccountID"].Value = e.Cell.Value);
			obj.Value = value;
			((UltraGridBase)ULGData).ActiveRow.Cells["AccountID"].Value = dataRow["AccountID"];
			if (cboCurrency.SelectedIndex != -1)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["Balance"].Value = CalculateClientBalance(e.Cell.Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString());
			}
			CalculateTotals();
		}
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Value")
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void CalculateTotals()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["Balance"].Value != DBNull.Value)
			{
				num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Balance"].Value.ToString());
			}
		}
		((Control)(object)txtTotal).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void CalculateTotalsPaid()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["Value"].Value != DBNull.Value)
			{
				num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Value"].Value.ToString());
			}
		}
		((Control)(object)txtPaidAmount).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private decimal CalculateClientBalance(string ClientSubAccountID, string CurrencyID)
	{
		return SubAccounts.Balance(ClientSubAccountID, "-1", "," + GlobalVariables.CurrentBranchID + ",", CurrencyID, IsFromServer: false, 0);
	}

	public override void btnRefreshDataClick()
	{
		object value = ((TextEditorControlBase)cboSalesMan).Value;
		object value2 = ((TextEditorControlBase)cboCurrency).Value;
		object value3 = ((TextEditorControlBase)cboSafe).Value;
		object value4 = ((TextEditorControlBase)cboTransactionBranch).Value;
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlClientsCodes.ValueListItems.Clear();
		for (int i = 0; i < dtClients.Rows.Count; i++)
		{
			vlClients.ValueListItems.Add(dtClients.Rows[i]["SubAccountID"], dtClients.Rows[i]["SubAccountName"].ToString());
			vlClientsCodes.ValueListItems.Add(dtClients.Rows[i]["SubAccountID"], dtClients.Rows[i]["ClientSupplierNo"].ToString());
		}
		dtSalesMan = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.BranchIDs, "1", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSalesMan, dtSalesMan, "SubAccountID", "SubAccountName");
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		UseCurrency = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from Currency ").Rows[0][0].ToString()) > 1;
		dtsafes = Safes.FillComboBySafeIDs(GlobalVariables.SafeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSafe, dtsafes, "SafeID", "SafeName");
		FillCurrencyDropDown();
		((TextEditorControlBase)cboSalesMan).Value = value;
		((TextEditorControlBase)cboCurrency).Value = value2;
		((TextEditorControlBase)cboSafe).Value = value3;
		((TextEditorControlBase)cboTransactionBranch).Value = value4;
	}

	private void txtExchangeRate_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void btnJV_Click(object sender, EventArgs e)
	{
		if (dtSafeIn != null && dtSafeIn.Rows.Count > 0 && !Adding && !Updating && dtSafeIn.Rows[0]["JVID"] != DBNull.Value)
		{
			frmJV frmJV2 = new frmJV(int.Parse(dtSafeIn.Rows[0]["JVID"].ToString()));
			frmJV2.Size = new Size(base.Width, base.Height);
			frmJV2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmJV2.lblTitle).Text = (GlobalVariables.IsArabic ? "قيود اليومية" : "JV");
			frmJV2.ShowDialog();
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
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Expected O, but got Unknown
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Expected O, but got Unknown
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Expected O, but got Unknown
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Expected O, but got Unknown
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Expected O, but got Unknown
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Expected O, but got Unknown
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Expected O, but got Unknown
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Expected O, but got Unknown
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Sales.Transactions.frmClientsPayments));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboCurrency = new UltraComboEditor();
		this.lblCurrency = new UltraLabel();
		this.lblBank = new UltraLabel();
		this.txtExchangeRate = new UltraTextEditor();
		this.lblExchangrRate = new UltraLabel();
		this.txtChargedPerson = new UltraTextEditor();
		this.lblChargePerson = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblTotal = new UltraLabel();
		this.txtTotal = new UltraTextEditor();
		this.btnJV = new UltraButton();
		this.lblSafe = new UltraLabel();
		this.cboSafe = new UltraComboEditor();
		this.lblTotalPaidAmount = new UltraLabel();
		this.txtPaidAmount = new UltraTextEditor();
		this.btnSalesManSearch = new UltraButton();
		this.cboSalesMan = new UltraComboEditor();
		this.lblSalesMan = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtChargedPerson).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotal).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSafe).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaidAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesMan).BeginInit();
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
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
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
		resources.ApplyResources(this.lblSafe, "lblSafe");
		this.lblSafe.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSafe).Name = "lblSafe";
		((ControlBase)this.lblSafe).WrapText = false;
		resources.ApplyResources(this.cboSafe, "cboSafe");
		((TextEditorControlBase)this.cboSafe).AlwaysInEditMode = true;
		this.cboSafe.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSafe).Name = "cboSafe";
		resources.ApplyResources(this.lblTotalPaidAmount, "lblTotalPaidAmount");
		this.lblTotalPaidAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalPaidAmount).Name = "lblTotalPaidAmount";
		((ControlBase)this.lblTotalPaidAmount).WrapText = false;
		resources.ApplyResources(this.txtPaidAmount, "txtPaidAmount");
		((System.Windows.Forms.Control)(object)this.txtPaidAmount).Name = "txtPaidAmount";
		((EditorButtonControlBase)this.txtPaidAmount).ReadOnly = true;
		resources.ApplyResources(this.btnSalesManSearch, "btnSalesManSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.btnSalesManSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnSalesManSearch).Name = "btnSalesManSearch";
		((System.Windows.Forms.Control)(object)this.btnSalesManSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnSalesManSearch).Click += new System.EventHandler(btnSalesManSearch_Click);
		resources.ApplyResources(this.cboSalesMan, "cboSalesMan");
		((TextEditorControlBase)this.cboSalesMan).AlwaysInEditMode = true;
		this.cboSalesMan.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSalesMan).Name = "cboSalesMan";
		((TextEditorControlBase)this.cboSalesMan).ValueChanged += new System.EventHandler(cboSalesMan_ValueChanged);
		resources.ApplyResources(this.lblSalesMan, "lblSalesMan");
		this.lblSalesMan.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSalesMan).Name = "lblSalesMan";
		((ControlBase)this.lblSalesMan).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSalesManSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSalesMan);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesMan);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalPaidAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPaidAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSafe);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSafe);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnJV);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtChargedPerson);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblChargePerson);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBank);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangrRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmClientsPayments";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangrRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBank, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblChargePerson, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtChargedPerson, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnJV, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPaidAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalPaidAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSalesMan, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSalesMan, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSalesManSearch, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtChargedPerson).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotal).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSafe).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaidAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesMan).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
