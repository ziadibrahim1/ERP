using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.Lenses;
using BusinessLayer.POS;
using BusinessLayer.Privilege;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Classes.DirectPrinting;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Lenses.Transactions;

public class frmRevenues : frmHeaderDetails
{
	private DataTable dtReports;

	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private DataTable dtShiftDetails;

	private DataTable dtBranches;

	private DataTable dtSalesMan;

	private DataTable dtPOSDefaultData;

	private DataTable dtCurrency;

	private string ShiftDetailID;

	private string ShiftDetailUserID;

	private string ManufacturingClientAccountID = "";

	private int RevenueID;

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

	private UltraLabel lblTotal;

	private UltraTextEditor txtTotal;

	private UltraLabel lblBackTitle;

	private UltraComboEditor cboClientCode;

	private UltraLabel lblBalance;

	private UltraTextEditor txtBrabnchBalance;

	private UltraLabel lblShiftNo;

	private UltraTextEditor txtShiftNo;

	private UltraLabel lblShiftDate;

	private UltraDateTimeEditor dtpShiftDate;

	public UltraButton btnBranchesSearch;

	private UltraLabel lblBranches;

	private UltraComboEditor cboBranches;

	public UltraButton btnSalesMan2Search;

	private UltraLabel lblSalesMan2;

	private UltraComboEditor cboSalesMan2;

	private UltraTextEditor txtAccountBalance;

	public frmRevenues()
	{
		InitializeComponent();
		TableName = "Lns_Revenues";
		IDCol = "RevenueID";
		NoCol = "RevenueNo";
		DateCol = "RevenueDate";
	}

	public frmRevenues(int _RevenueID)
		: this()
	{
		RevenueID = _RevenueID;
	}

	public override void PrepareData()
	{
		((Control)(object)lblBackTitle).Text = ((Control)(object)lblTitle).Text;
		base.PrepareData();
		AutoPrint = false;
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtPOSDefaultData = BusinessLayer.POS.Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboAccountName, dtAccounts, "AccountID", "Name");
		dtSubAccounts = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSubAccountName, dtSubAccounts, "SubAccountID", "SubAccountName");
		GlobalFunctions.FillCombo(cboClientCode, dtSubAccounts, "SubAccountID", "ClientSupplierNo");
		dtBranches = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBranches, dtBranches, "BranchID", "BranchName");
		dtSalesMan = SubAccounts.SelectBySubAccountTypeIDs(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSalesMan2, dtSalesMan, "SubAccountID", "SubAccountName");
		if (dtPOSDefaultData.Rows.Count > 0)
		{
			ManufacturingClientAccountID = dtPOSDefaultData.Rows[0]["ManufacturingClientAccountID"].ToString();
		}
	}

	public override void FillData()
	{
		if (RevenueID != 0)
		{
			DataTable dataTable = Revenues.Select(RevenueID.ToString(), GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
			if (dataTable.Rows.Count > 0)
			{
				drMaster = dataTable.Rows[0];
			}
			else
			{
				drMaster = null;
			}
		}
		else if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable2 = Revenues.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
			if (dataTable2.Rows.Count > 0)
			{
				drMaster = dataTable2.Rows[0];
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
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["RevenueNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["RevenueDate"];
			((TextEditorControlBase)cboBranches).Value = drMaster["SubAccountBranchID"];
			((TextEditorControlBase)cboAccountName).ValueChanged -= cboAccountName_ValueChanged;
			((TextEditorControlBase)cboAccountName).Value = drMaster["AccountID"];
			((TextEditorControlBase)cboAccountName).ValueChanged += cboAccountName_ValueChanged;
			((TextEditorControlBase)cboClientCode).ValueChanged -= cboClientCode_ValueChanged;
			((TextEditorControlBase)cboSubAccountName).ValueChanged -= cboSubAccountName_ValueChanged;
			UltraComboEditor obj = cboClientCode;
			object value = (((TextEditorControlBase)cboSubAccountName).Value = drMaster["SubAccountID"]);
			((TextEditorControlBase)obj).Value = value;
			((TextEditorControlBase)cboSubAccountName).ValueChanged += cboSubAccountName_ValueChanged;
			((TextEditorControlBase)cboClientCode).ValueChanged += cboClientCode_ValueChanged;
			dtShiftDetails = ShiftsDetails.SelectByShiftDetailID(drMaster["ShiftDetailID"].ToString());
			if (dtShiftDetails.Rows.Count > 0)
			{
				((Control)(object)txtShiftNo).Text = dtShiftDetails.Rows[0]["ShiftDetailNo"].ToString();
				dtpShiftDate.Value = (DateTime)dtShiftDetails.Rows[0]["StartDate"];
			}
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtTotal).Text = decimal.Parse(drMaster["TotalAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)cboSalesMan2).Value = drMaster["EmployeeID2"];
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			if (drMaster["Approved"].Equals(true) || !CanEditFromServer)
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
		((EditorButtonControlBase)txtCode).ReadOnly = Adding;
		((EditorButtonControlBase)dtpDate).ReadOnly = true;
		((EditorButtonControlBase)cboSubAccountName).ReadOnly = NavMode || (cboBranches.SelectedIndex > -1 && ((TextEditorControlBase)cboBranches).Value.ToString() != GlobalVariables.CurrentBranchID && !ViewAllBranches);
		((EditorButtonControlBase)cboAccountName).ReadOnly = NavMode || (cboBranches.SelectedIndex > -1 && ((TextEditorControlBase)cboBranches).Value.ToString() != GlobalVariables.CurrentBranchID && !ViewAllBranches);
		((EditorButtonControlBase)cboClientCode).ReadOnly = NavMode || (cboBranches.SelectedIndex > -1 && ((TextEditorControlBase)cboBranches).Value.ToString() != GlobalVariables.CurrentBranchID && !ViewAllBranches);
		((EditorButtonControlBase)cboBranches).ReadOnly = !ViewAllBranches;
		((Control)(object)btnBranchesSearch).Visible = !NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTotal).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSalesMan2).ReadOnly = NavMode;
		((Control)(object)btnAccountSearch).Visible = false;
		((Control)(object)btnSubAccountSearch).Visible = !NavMode;
		((Control)(object)dtpShiftDate).Visible = !Adding;
		((Control)(object)txtShiftNo).Visible = !Adding;
		((Control)(object)lblShiftDate).Visible = !Adding;
		((Control)(object)lblShiftNo).Visible = !Adding;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? Revenues.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)cboClientCode).ValueChanged -= cboClientCode_ValueChanged;
		((TextEditorControlBase)cboSubAccountName).ValueChanged -= cboSubAccountName_ValueChanged;
		((TextEditorControlBase)cboAccountName).ValueChanged -= cboAccountName_ValueChanged;
		cboAccountName.SelectedIndex = -1;
		cboSubAccountName.SelectedIndex = -1;
		cboClientCode.SelectedIndex = -1;
		((TextEditorControlBase)cboClientCode).ValueChanged += cboClientCode_ValueChanged;
		((TextEditorControlBase)cboSubAccountName).ValueChanged += cboSubAccountName_ValueChanged;
		((TextEditorControlBase)cboAccountName).ValueChanged += cboAccountName_ValueChanged;
		((TextEditorControlBase)cboBranches).Value = GlobalVariables.CurrentBranchID;
		((TextEditorControlBase)txtNotes).Clear();
		((Control)(object)txtTotal).Text = "0";
		((Control)(object)txtBrabnchBalance).Text = "0";
		((Control)(object)txtAccountBalance).Text = "0";
		cboSalesMan2.SelectedIndex = -1;
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
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم  الأذن" : "Please Enter The Voucher Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (cboSubAccountName.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار حساب تحليلى" : "Please Select SubAccount Name");
			((TextEditorControlBase)cboSubAccountName).Focus();
			return false;
		}
		if (cboAccountName.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار إسم الحساب" : "Please Select Account Name");
			((TextEditorControlBase)cboAccountName).Focus();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("Lns_Revenues", "RevenueNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["RevenueNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = Revenues.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Voucher Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByBranchID;
		}
		else if (((Control)(object)txtTotal).Text == "" || decimal.Parse(((Control)(object)txtTotal).Text) <= 0m)
		{
			GlobalVariables.InformationMB.Show("إجمالى السعر لابد ان يكون اكبر من الصفر", "Total Price Must Be Greater Than Zero");
			((TextEditorControlBase)txtTotal).Focus();
			return false;
		}
		return true;
	}

	public bool ValidateForShift()
	{
		DataTable dataTable = BusinessLayer.POS.Settings.ValidateCashierData(GlobalVariables.UserID, GlobalVariables.CurrentBranchID);
		if (dataTable.Rows[0]["SubAccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد الحساب التحليلى للمستخدم  ", "Please Set user SubAccount");
			return false;
		}
		if (dataTable.Rows[0]["CashierAccount"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب الكاشير من إعدادات البيع المباشر  ", "Please Set Cashier Account From POS Setting");
			return false;
		}
		if (int.Parse(dataTable.Rows[0]["Relation"].ToString()) == 0)
		{
			GlobalVariables.InformationMB.Show("لايوجد ربط بين حساب الكاشير وحساب المستخدم  ", "There is No Relation Between Cashier Account And user SubAccount");
			return false;
		}
		DataTable dataTable2 = ShiftsDetails.SelectNotClosed(GlobalVariables.CurrentBranchID);
		if (dataTable2.Rows.Count != 1)
		{
			GlobalVariables.InformationMB.Show("برجاء فتح وردية اولا", "Please open Shift First");
			return false;
		}
		DateTime dateTime = new DateTime(DateTime.Parse(dataTable2.Rows[0]["StartDate"].ToString()).Year, DateTime.Parse(dataTable2.Rows[0]["StartDate"].ToString()).Month, DateTime.Parse(dataTable2.Rows[0]["StartDate"].ToString()).Day, DateTime.Parse(dataTable2.Rows[0]["StartTime"].ToString()).Hour, DateTime.Parse(dataTable2.Rows[0]["StartTime"].ToString()).Minute, DateTime.Parse(dataTable2.Rows[0]["StartTime"].ToString()).Second);
		DateTime dateTime2 = dateTime.AddHours(DateTime.Parse(dataTable2.Rows[0]["ShiftPeriod"].ToString()).Hour).AddMinutes(DateTime.Parse(dataTable2.Rows[0]["ShiftPeriod"].ToString()).Minute).AddSeconds(DateTime.Parse(dataTable2.Rows[0]["ShiftPeriod"].ToString()).Second);
		if (GlobalFunctions.GetServerDateTimeNow() < dateTime)
		{
			GlobalVariables.InformationMB.Show(dateTime.ToShortTimeString() + " وقت بداية الوردية ", " Shift Start Time Is " + dateTime.ToShortTimeString());
			return false;
		}
		if (GlobalFunctions.GetServerDateTimeNow() > dateTime2.AddHours(2.0))
		{
			GlobalVariables.InformationMB.Show(dateTime2.ToShortTimeString() + " وقت نهاية الوردية ", " Shift End Time Is " + dateTime2.ToShortTimeString());
			return false;
		}
		if (dtpDate.DateTime < DateTime.Parse(dataTable2.Rows[0]["StartDate"].ToString()))
		{
			GlobalVariables.InformationMB.Show("تاريخ الشيك أقل من تاريخ بداية الوردية تاكد من تاريخ الجهاز", "Check Date More Less Than Shift End Date Check Your pc ");
			return false;
		}
		DataTable dataTable3 = ShiftsDetailsUsers.SelectNotClosed(dataTable2.Rows[0]["ShiftDetailID"].ToString(), GlobalVariables.UserID, GlobalVariables.CurrentBranchID);
		ShiftDetailID = dataTable2.Rows[0]["ShiftDetailID"].ToString();
		if (dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["CurrencyID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال عملة الفرع من إعدادات البيع المباشر ", "Please Enter Default Currency From POS Settings");
			return false;
		}
		dtCurrency = Currency.FillCurrencyByDate(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (decimal.Parse(dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString()) <= 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال سعر الصرف" : "Please Enter The Exchange Rate");
			return false;
		}
		if (dataTable3.Rows.Count == 0)
		{
			ShiftDetailUserID = ShiftsDetailsUsers.Insert_Update("-1", ShiftDetailID, GlobalVariables.UserID, GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate), "Null", "0", dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), "0", "0", "0", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID).ToString();
		}
		else
		{
			ShiftDetailUserID = dataTable3.Rows[0]["ShiftDetailUserID"].ToString();
		}
		return true;
	}

	public override void btnSaveClose_Click(object sender, EventArgs e)
	{
		if (ValidateForShift())
		{
			base.btnSaveClose_Click(sender, e);
		}
	}

	public override void btnOKClick()
	{
		if (ValidateForShift())
		{
			base.btnOKClick();
		}
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		int num;
		try
		{
			num = Revenues.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", ShiftDetailID, ShiftDetailUserID, GlobalVariables.UserID, "Null", (cboAccountName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAccountName).Value.ToString(), (cboSubAccountName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccountName).Value.ToString(), dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), ((Control)(object)txtNotes).Text, (((Control)(object)txtTotal).Text == "") ? "0" : ((Control)(object)txtTotal).Text, (cboSalesMan2.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesMan2).Value.ToString(), dtSubAccounts.Select(" SubAccountID= " + ((TextEditorControlBase)cboSubAccountName).Value.ToString())[0]["BranchID"].ToString(), "Null", "Null", "Null", "1", "Null", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ShiftsDetails.RevenueJVAdding(num.ToString(), ShiftDetailID, GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
			return;
		}
		if (!DataSaved || !(GlobalVariables.POSPrinter != ""))
		{
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد طباعة فاتورة الايراد ؟", "Are You Sure You want to Print This Revenue Invoice?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			return;
		}
		ReportDocument reportDocument = new ReportDocument();
		try
		{
			if (dtReports.Rows.Count > 0)
			{
				if (dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString().Trim() == "Rep_Lns_RevenuesFastPrint")
				{
					RowID = num.ToString();
					FastPrint();
					return;
				}
				reportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
			}
			else
			{
				reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Lns_Revenues_A.rpt" : "Rep_Lns_Revenues_E.rpt"));
			}
			GlobalFunctions.ConfigureReport(reportDocument);
			reportDocument.SetParameterValue("@RevenueIDs", "," + num + ",");
			reportDocument.SetParameterValue("@BranchID", GlobalVariables.CurrentBranchID);
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			reportDocument.PrintOptions.PrinterName = GlobalVariables.POSPrinter;
			reportDocument.PrintOptions.PrinterName = GlobalVariables.POSPrinter;
			try
			{
				reportDocument.PrintToPrinter(1, collated: true, 0, 10000);
			}
			catch (Exception ex)
			{
				GlobalVariables.InformationMB.Show("تأكد من وصلات الطابعة  \n" + ex.Message, "Check Printer Cable\n" + ex.Message);
			}
		}
		catch
		{
			GlobalVariables.InformationMB.Show("خطأ فى مسار التقارير ", "Load Report Failed");
		}
		reportDocument.Dispose();
		GC.Collect();
	}

	public override void UpdateData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = Revenues.Insert_Update(drMaster["RevenueID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", drMaster["ShiftDetailID"].ToString(), drMaster["ShiftDetailUserID"].ToString(), (drMaster["User_ID"] == DBNull.Value) ? "Null" : drMaster["User_ID"].ToString(), "Null", (cboAccountName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAccountName).Value.ToString(), (cboSubAccountName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccountName).Value.ToString(), dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), ((Control)(object)txtNotes).Text, (((Control)(object)txtTotal).Text == "") ? "0" : ((Control)(object)txtTotal).Text, (cboSalesMan2.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesMan2).Value.ToString(), dtSubAccounts.Select(" SubAccountID= " + ((TextEditorControlBase)cboSubAccountName).Value.ToString())[0]["BranchID"].ToString(), (drMaster["TransferJVID"] == DBNull.Value) ? "Null" : drMaster["TransferJVID"].ToString(), (drMaster["TransferJVID2"] == DBNull.Value) ? "Null" : drMaster["TransferJVID2"].ToString(), "Null", "1", "Null", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ShiftsDetails.RevenueJVRegenerate(ShiftDetailID, GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnUpdateClick()
	{
		if (Main.IsSynchronization && !GlobalVariables.dtSyncConn.Rows[0]["SyncBranchID"].Equals(DBNull.Value) && Revenues.SyncCanUpdate(RowID) == 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن تعديل هذا البيان. هذا البيان تم تعديله في المركز الرئيسي برجاء الانتظار حتي الانتهاء من تحديث البيانات من الخادم  ", "Can not Update this Data. This data is Modified in The Central Point Please wait for Syncronization ");
		}
		else
		{
			base.btnUpdateClick();
		}
	}

	public override void btnDeleteClick()
	{
		if (ValidateForShift())
		{
			if (bool.Parse(Main.ExecuteQuery_DataTable(" Select  IsClosed  From POS_ShiftsDetailsUsers Where Deleted=0 AND ShiftDetailUserID = " + drMaster["ShiftDetailUserID"].ToString()).Rows[0]["IsClosed"].ToString()))
			{
				GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لوجود خزينة المستخدم مغلقة ", "Cannot Delete This Transaction Because User Safe Is Already Closed ");
			}
			else
			{
				base.btnDeleteClick();
			}
		}
	}

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			Revenues.DeleteVirtual(drMaster["RevenueID"].ToString(), GlobalVariables.UserID);
			ShiftsDetails.RevenueJVRegenerate(ShiftDetailID, GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
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
		((TextEditorControlBase)cboAccountName).ValueChanged -= cboAccountName_ValueChanged;
		object value = ((TextEditorControlBase)cboAccountName).Value;
		object value2 = ((TextEditorControlBase)cboSubAccountName).Value;
		object value3 = ((TextEditorControlBase)cboBranches).Value;
		object value4 = ((TextEditorControlBase)cboSalesMan2).Value;
		dtPOSDefaultData = BusinessLayer.POS.Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboAccountName, dtAccounts, "AccountID", "Name");
		dtSubAccounts = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (cboBranches.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtSubAccounts);
			dataView.RowFilter = " BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString();
			GlobalFunctions.FillCombo(cboSubAccountName, dataView.ToTable(), "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboClientCode, dataView.ToTable(), "SubAccountID", "ClientSupplierNo");
		}
		else
		{
			GlobalFunctions.FillCombo(cboSubAccountName, dtSubAccounts, "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboClientCode, dtSubAccounts, "SubAccountID", "ClientSupplierNo");
		}
		dtBranches = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBranches, dtBranches, "BranchID", "BranchName");
		dtSalesMan = SubAccounts.SelectBySubAccountTypeIDs(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSalesMan2, dtSalesMan, "SubAccountID", "SubAccountName");
		((TextEditorControlBase)cboAccountName).Value = value;
		((TextEditorControlBase)cboSubAccountName).Value = value2;
		((TextEditorControlBase)cboBranches).Value = value3;
		((TextEditorControlBase)cboSalesMan2).Value = value4;
		((TextEditorControlBase)cboAccountName).ValueChanged += cboAccountName_ValueChanged;
	}

	public void FastPrint()
	{
		DataTable dataTable = Main.ExecuteQuery_DataTable(" Rep_Lns_Revenues '," + RowID + ",'," + (GlobalVariables.IsArabic ? "1" : "0"));
		DataTable dataTable2 = Main.ExecuteQuery_DataTable(" Rep_POS_Settings_SelectByBranchID " + GlobalVariables.CurrentBranchID + "," + (GlobalVariables.IsArabic ? "1" : "0"));
		Color black = Color.Black;
		Color darkBlue = Color.DarkBlue;
		Color black2 = Color.Black;
		float lineWidth = 0.03f;
		DataRow dataRow = dataTable.Rows[0];
		Font font = new Font("Times New Roman", 7f, FontStyle.Bold);
		Font font2 = new Font("Times New Roman", 7f, FontStyle.Bold);
		Font font3 = new Font("Times New Roman", 9f, FontStyle.Regular);
		Font font4 = new Font("Times New Roman", 7f, FontStyle.Bold);
		Font font5 = new Font("Times New Roman", 8f, FontStyle.Bold);
		FastPrint instance = ERP.Classes.DirectPrinting.FastPrint.Instance;
		instance.PrinterSettings.PrinterName = GlobalVariables.POSPrinter;
		instance.GraphicsUnit = GraphicsUnit.Millimeter;
		instance.Margins = new Margins(0, 0, 0, 0);
		instance.OverallWidth = 70f;
		try
		{
			if (dataTable2.Rows[0]["Logo"] != null)
			{
				Image image = GlobalFunctions.BinaryToImage((byte[])dataTable2.Rows[0]["Logo"]);
				float num = 20f;
				float value = 20f;
				instance.AddEmptyCell((1f - num / instance.OverallWidth) / 2f, 1f, DrawRectangle: false);
				instance.AddImageCell(image, num / instance.OverallWidth, value, DrawRectangle: false);
				instance.AddEmptyCell((1f - num / instance.OverallWidth) / 2f, 1f, DrawRectangle: false);
				instance.AcceptChanges();
			}
		}
		catch
		{
		}
		instance.AddEmptyCell((1f - 44f / instance.OverallWidth) / 2f, 1f, DrawRectangle: false);
		instance.AddTextCell(dataTable2.Rows[0]["CompanyName"].ToString(), font, 44f / instance.OverallWidth, 7f, StringAlignment.Center, black, DrawRectangle: false);
		instance.AddEmptyCell((1f - 44f / instance.OverallWidth) / 2f, 1f, DrawRectangle: false);
		instance.AcceptChanges();
		instance.AddEmptyCell(1f, 1f, DrawRectangle: false);
		instance.AcceptChanges();
		instance.AddTextCell(dataRow["RevenueNo"].ToString(), font5, 0.7f, 4f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("اذن ايرادات برقم", font5, 0.3f, 4f, StringAlignment.Near, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AcceptChanges();
		instance.AddTextCell(((DateTime)dataRow["RevenueDate"]).ToString("dd/MM/yyyy hh:mm:ss tt"), font, 0.7f, 4f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("بتاريخ", font, 0.3f, 4f, StringAlignment.Near, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AcceptChanges();
		instance.AddTextCell(dataRow["BranchName"].ToString(), font, 0.7f, 4f, StringAlignment.Near, black, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("فرع", font, 0.3f, 4f, StringAlignment.Near, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AcceptChanges();
		instance.AddTextCell(dataRow["UserName"].ToString(), font, 0.7f, 4f, StringAlignment.Near, black, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("اسم البائع", font, 0.3f, 4f, StringAlignment.Near, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AcceptChanges();
		instance.AddEmptyCell(1f, 1f, DrawRectangle: false);
		instance.AcceptChanges();
		instance.AddTextCell("القيمة", font, 18f / instance.OverallWidth, 5f, StringAlignment.Center, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("اسم الحساب التحليلي", font, 31f / instance.OverallWidth, 5f, StringAlignment.Center, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AddTextCell("اسم الحساب", font, 21f / instance.OverallWidth, 5f, StringAlignment.Center, darkBlue, DrawRectangle: true, black2, lineWidth);
		instance.AcceptChanges();
		foreach (DataRow row in dataTable.Rows)
		{
			instance.AddTextCell(decimal.Parse(row["TotalAmount"].ToString(), NumberStyles.Currency).ToString("0.00"), font2, 18f / instance.OverallWidth, 3f, StringAlignment.Far, black, DrawRectangle: true, black2, lineWidth);
			instance.AddTextCell(row["SubAccountName"].ToString(), font2, 31f / instance.OverallWidth, 3f, StringAlignment.Center, black, DrawRectangle: true, black2, lineWidth);
			instance.AddTextCell(row["AccountName"].ToString(), font2, 21f / instance.OverallWidth, 3f, StringAlignment.Center, black, DrawRectangle: true, black2, lineWidth);
			instance.AcceptChanges();
		}
		instance.AddEmptyCell(1f, 1f, DrawRectangle: false);
		instance.AcceptChanges();
		instance.AddTextCell(dataRow["Notes"].ToString().Trim(), font2, 60f / instance.OverallWidth, float.Parse((Math.Ceiling((double)dataRow["Notes"].ToString().Trim().Length / 31.0) * 3.0).ToString()), StringAlignment.Center, Color.Black, DrawRectangle: false);
		instance.AddTextCell("ملاحظات :", font4, 10f / instance.OverallWidth, 4f, StringAlignment.Center, darkBlue);
		instance.AcceptChanges();
		instance.AddTextCell(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), font2, 1f, 4f, StringAlignment.Center, Color.Black, DrawRectangle: false);
		instance.AcceptChanges();
		if (dataTable2.Rows[0]["Message"].ToString().Trim().Length > 0)
		{
			instance.AddTextCell(dataTable2.Rows[0]["Message"].ToString().Trim(), font2, 1f, float.Parse((Math.Ceiling((double)dataTable2.Rows[0]["Message"].ToString().Trim().Length / 85.0) * 3.0).ToString()), StringAlignment.Center, Color.Black, DrawRectangle: false);
			instance.AcceptChanges();
		}
		instance.PrinterSettings.Copies = 1;
		instance.Print();
	}

	public override void btnPrintClick()
	{
		if (!(RowID != ""))
		{
			return;
		}
		ReportDocument reportDocument = new ReportDocument();
		try
		{
			if (dtReports.Rows.Count > 0)
			{
				if (dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString().Trim() == "Rep_Lns_RevenuesFastPrint")
				{
					FastPrint();
					return;
				}
				reportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
			}
			else
			{
				reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Lns_Revenues_A.rpt" : "Rep_Lns_Revenues_E.rpt"));
			}
			GlobalFunctions.ConfigureReport(reportDocument);
			reportDocument.SetParameterValue("@RevenueIDs", "," + RowID + ",");
			reportDocument.SetParameterValue("@BranchID", GlobalVariables.CurrentBranchID);
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			reportDocument.PrintOptions.PrinterName = GlobalVariables.POSPrinter;
			reportDocument.PrintOptions.PrinterName = GlobalVariables.POSPrinter;
			try
			{
				reportDocument.PrintToPrinter(1, collated: true, 0, 10000);
			}
			catch (Exception ex)
			{
				GlobalVariables.InformationMB.Show("تأكد من وصلات الطابعة  \n" + ex.Message, "Check Printer Cable\n" + ex.Message);
			}
		}
		catch
		{
			GlobalVariables.InformationMB.Show("خطأ فى مسار التقارير ", "Load Report Failed");
		}
		reportDocument.Dispose();
		GC.Collect();
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.LnsRevenuesReport(0, -1, 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["RevenueID"].ToString();
			FillData();
		}
	}

	private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void cboSubAccountName_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.Clients((cboBranches.SelectedIndex == -1) ? "-1" : ((TextEditorControlBase)cboBranches).Value.ToString(), "1", IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboSubAccountName).Value = num;
			}
		}
	}

	private void btnSubAccountSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Clients((cboBranches.SelectedIndex == -1) ? "-1" : ((TextEditorControlBase)cboBranches).Value.ToString(), "1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboSubAccountName).Value = num;
		}
	}

	private void cboSubAccountName_ValueChanged(object sender, EventArgs e)
	{
		((TextEditorControlBase)cboClientCode).ValueChanged -= cboClientCode_ValueChanged;
		((TextEditorControlBase)cboClientCode).Value = ((TextEditorControlBase)cboSubAccountName).Value;
		((TextEditorControlBase)cboClientCode).ValueChanged += cboClientCode_ValueChanged;
		if (cboSubAccountName.SelectedIndex != -1)
		{
			DataRow dataRow = dtSubAccounts.Select(" SubAccountID= " + ((TextEditorControlBase)cboSubAccountName).Value)[0];
			((TextEditorControlBase)cboAccountName).ValueChanged -= cboAccountName_ValueChanged;
			((Control)(object)txtBrabnchBalance).Text = decimal.Parse(Math.Round(SubAccounts.Balance(((TextEditorControlBase)cboSubAccountName).Value.ToString(), "-1", Adding ? ("," + GlobalVariables.CurrentBranchID + ",") : ("," + drMaster["BranchID"].ToString() + ","), dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), IsFromServer: false, 0), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			DataView dataView = new DataView(dtAccounts);
			dataView.RowFilter = " AccountID = " + dtSubAccounts.Select("  SubAccountID =" + ((TextEditorControlBase)cboSubAccountName).Value.ToString())[0]["AccountID"].ToString() + ((ManufacturingClientAccountID == "") ? "" : (" Or AccountID = " + ManufacturingClientAccountID));
			DataTable dt = dataView.ToTable();
			GlobalFunctions.FillCombo(cboAccountName, dt, "AccountID", "Name");
			((TextEditorControlBase)cboAccountName).Value = dtSubAccounts.Select("  SubAccountID =" + ((TextEditorControlBase)cboSubAccountName).Value.ToString())[0]["AccountID"];
			((Control)(object)txtAccountBalance).Text = ((cboSubAccountName.SelectedIndex > -1 && cboAccountName.SelectedIndex > -1) ? decimal.Parse(Math.Round(SubAccounts.Balance(((TextEditorControlBase)cboSubAccountName).Value.ToString(), "," + ((TextEditorControlBase)cboAccountName).Value.ToString() + ",", Adding ? ("," + GlobalVariables.CurrentBranchID + ",") : ("," + drMaster["BranchID"].ToString() + ","), dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), IsFromServer: false, 0), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate) : "0");
			((TextEditorControlBase)cboAccountName).ValueChanged += cboAccountName_ValueChanged;
		}
		else
		{
			((TextEditorControlBase)cboAccountName).ValueChanged -= cboAccountName_ValueChanged;
			cboAccountName.SelectedIndex = -1;
			((Control)(object)txtAccountBalance).Text = "0";
			((TextEditorControlBase)cboAccountName).ValueChanged += cboAccountName_ValueChanged;
		}
	}

	private void cboClientCode_ValueChanged(object sender, EventArgs e)
	{
		if (cboClientCode.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboSubAccountName).Value = ((TextEditorControlBase)cboClientCode).Value;
			return;
		}
		((TextEditorControlBase)cboSubAccountName).ValueChanged -= cboSubAccountName_ValueChanged;
		cboSubAccountName.SelectedIndex = -1;
		((TextEditorControlBase)cboSubAccountName).ValueChanged += cboSubAccountName_ValueChanged;
	}

	private void cboAccountName_ValueChanged(object sender, EventArgs e)
	{
		if (cboSubAccountName.SelectedIndex > -1 && cboAccountName.SelectedIndex > -1)
		{
			((Control)(object)txtAccountBalance).Text = decimal.Parse(Math.Round(SubAccounts.Balance(((TextEditorControlBase)cboSubAccountName).Value.ToString(), "," + ((TextEditorControlBase)cboAccountName).Value.ToString() + ",", Adding ? ("," + GlobalVariables.CurrentBranchID + ",") : ("," + drMaster["BranchID"].ToString() + ","), dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), IsFromServer: false, 0), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
	}

	public override void txtCode_KeyUp(object sender, KeyEventArgs e)
	{
		if (!CanSearching || e.KeyCode != Keys.Return || TableName.Length <= 0 || NoCol.Length <= 0 || ((Control)(object)txtCode).Text.Length <= 0)
		{
			return;
		}
		if (Adding || Updating)
		{
			e.Handled = true;
			SendKeys.Send("{tab}");
			return;
		}
		DataTable comboData = Main.GetComboData(TableName, IDCol, NoCol + "=''" + ((Control)(object)txtCode).Text.Trim() + "'' And Deleted=0 And IsOtherRevenue=0");
		if (comboData.Rows.Count > 0)
		{
			RowID = comboData.Rows[0][IDCol].ToString();
			dtSearchResult = null;
		}
		else
		{
			RowID = "";
		}
		FillData();
	}

	public override void PriveousData()
	{
		if (dtSearchResult != null && dtSearchResult.Rows.Count > 1)
		{
			if (drMaster != null)
			{
				for (int i = 0; i < dtSearchResult.Rows.Count - 1; i++)
				{
					if (dtSearchResult.Rows[i][IDCol].ToString() == drMaster[IDCol].ToString())
					{
						RowID = dtSearchResult.Rows[i + 1][IDCol].ToString();
						FillData();
						break;
					}
				}
			}
			else
			{
				RowID = dtSearchResult.Rows[dtSearchResult.Rows.Count - 1][IDCol].ToString();
				FillData();
			}
		}
		else
		{
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, " And IsOtherRevenue=0 ", "0");
			if (dataTable.Rows.Count > 0)
			{
				RowID = dataTable.Rows[0][IDCol].ToString();
			}
			else
			{
				RowID = "";
			}
			FillData();
		}
	}

	public override void NextData()
	{
		if (dtSearchResult != null && dtSearchResult.Rows.Count > 1)
		{
			if (drMaster != null)
			{
				for (int i = 1; i < dtSearchResult.Rows.Count; i++)
				{
					if (dtSearchResult.Rows[i][IDCol].ToString() == drMaster[IDCol].ToString())
					{
						RowID = dtSearchResult.Rows[i - 1][IDCol].ToString();
						FillData();
						break;
					}
				}
			}
			else
			{
				RowID = dtSearchResult.Rows[0][IDCol].ToString();
				FillData();
			}
		}
		else
		{
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, " And IsOtherRevenue=0 ", "1");
			if (dataTable.Rows.Count > 0)
			{
				RowID = dataTable.Rows[0][IDCol].ToString();
			}
			else
			{
				RowID = "";
			}
			FillData();
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
			((Control)(object)txtCode).Text = Revenues.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void btnBranchesSearch_Click(object sender, EventArgs e)
	{
		frmBranchesChange frmBranchesChange2 = new frmBranchesChange(base.Name);
		frmBranchesChange2.WindowState = FormWindowState.Normal;
		frmBranchesChange2.ShowDialog();
		((TextEditorControlBase)cboBranches).Value = ((frmBranchesChange2.BranchID > 0) ? ((object)frmBranchesChange2.BranchID) : ((TextEditorControlBase)cboBranches).Value);
	}

	private void cboBranches_ValueChanged(object sender, EventArgs e)
	{
		if (cboBranches.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtSubAccounts);
			dataView.RowFilter = " BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString();
			GlobalFunctions.FillCombo(cboSubAccountName, dataView.ToTable(), "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboClientCode, dataView.ToTable(), "SubAccountID", "ClientSupplierNo");
		}
		else
		{
			GlobalFunctions.FillCombo(cboSubAccountName, dtSubAccounts, "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboClientCode, dtSubAccounts, "SubAccountID", "ClientSupplierNo");
		}
	}

	private void btnSalesMan2Search_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Employees("-1", "-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboSalesMan2).Value = num;
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
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected O, but got Unknown
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected O, but got Unknown
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected O, but got Unknown
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Expected O, but got Unknown
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected O, but got Unknown
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Expected O, but got Unknown
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Expected O, but got Unknown
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Expected O, but got Unknown
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Expected O, but got Unknown
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Expected O, but got Unknown
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Expected O, but got Unknown
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Expected O, but got Unknown
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Expected O, but got Unknown
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Expected O, but got Unknown
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Expected O, but got Unknown
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Expected O, but got Unknown
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Expected O, but got Unknown
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.Transactions.frmRevenues));
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
		this.lblTotal = new UltraLabel();
		this.txtTotal = new UltraTextEditor();
		this.lblBackTitle = new UltraLabel();
		this.cboClientCode = new UltraComboEditor();
		this.lblBalance = new UltraLabel();
		this.txtBrabnchBalance = new UltraTextEditor();
		this.lblShiftNo = new UltraLabel();
		this.txtShiftNo = new UltraTextEditor();
		this.lblShiftDate = new UltraLabel();
		this.dtpShiftDate = new UltraDateTimeEditor();
		this.btnBranchesSearch = new UltraButton();
		this.lblBranches = new UltraLabel();
		this.cboBranches = new UltraComboEditor();
		this.btnSalesMan2Search = new UltraButton();
		this.lblSalesMan2 = new UltraLabel();
		this.cboSalesMan2 = new UltraComboEditor();
		this.txtAccountBalance = new UltraTextEditor();
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
		((System.ComponentModel.ISupportInitialize)this.txtTotal).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClientCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBrabnchBalance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtShiftNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpShiftDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesMan2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAccountBalance).BeginInit();
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
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblCode, "lblCode");
		this.lblDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblDate, "lblDate");
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		((System.Windows.Forms.Control)(object)this.dtpDate).TabStop = false;
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.btnSubAccountSearch).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(this.btnSubAccountSearch, "btnSubAccountSearch");
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).Name = "btnSubAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).Click += new System.EventHandler(btnSubAccountSearch_Click);
		this.cboSubAccountName.AutoCompleteMode = (AutoCompleteMode)2;
		resources.ApplyResources(this.cboSubAccountName, "cboSubAccountName");
		((System.Windows.Forms.Control)(object)this.cboSubAccountName).Name = "cboSubAccountName";
		((TextEditorControlBase)this.cboSubAccountName).ValueChanged += new System.EventHandler(cboSubAccountName_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboSubAccountName).KeyDown += new System.Windows.Forms.KeyEventHandler(cboSubAccountName_KeyDown);
		this.lblSubAccountName.AutoEllipsis = false;
		resources.ApplyResources(this.lblSubAccountName, "lblSubAccountName");
		((System.Windows.Forms.Control)(object)this.lblSubAccountName).Name = "lblSubAccountName";
		((ControlBase)this.lblSubAccountName).WrapText = false;
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnAccountSearch).Appearance = (AppearanceBase)(object)val10;
		resources.ApplyResources(this.btnAccountSearch, "btnAccountSearch");
		((System.Windows.Forms.Control)(object)this.btnAccountSearch).Name = "btnAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnAccountSearch).TabStop = false;
		this.cboAccountName.AutoCompleteMode = (AutoCompleteMode)2;
		resources.ApplyResources(this.cboAccountName, "cboAccountName");
		((System.Windows.Forms.Control)(object)this.cboAccountName).Name = "cboAccountName";
		((System.Windows.Forms.Control)(object)this.cboAccountName).TabStop = false;
		((TextEditorControlBase)this.cboAccountName).ValueChanged += new System.EventHandler(cboAccountName_ValueChanged);
		this.lblAccountName.AutoEllipsis = false;
		resources.ApplyResources(this.lblAccountName, "lblAccountName");
		((System.Windows.Forms.Control)(object)this.lblAccountName).Name = "lblAccountName";
		((ControlBase)this.lblAccountName).WrapText = false;
		this.lblNotes.AutoEllipsis = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		this.lblTotal.AutoEllipsis = false;
		resources.ApplyResources(this.lblTotal, "lblTotal");
		((System.Windows.Forms.Control)(object)this.lblTotal).Name = "lblTotal";
		((ControlBase)this.lblTotal).WrapText = false;
		resources.ApplyResources(this.txtTotal, "txtTotal");
		((System.Windows.Forms.Control)(object)this.txtTotal).Name = "txtTotal";
		((System.Windows.Forms.Control)(object)this.txtTotal).Enter += new System.EventHandler(textBox_Enter);
		((System.Windows.Forms.Control)(object)this.txtTotal).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTotal_KeyPress);
		resources.ApplyResources(this.lblBackTitle, "lblBackTitle");
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((ControlBase)this.lblBackTitle).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.lblBackTitle).Name = "lblBackTitle";
		((UltraControlBase)this.lblBackTitle).UseAppStyling = false;
		((TextEditorControlBase)this.cboClientCode).AlwaysInEditMode = true;
		this.cboClientCode.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboClientCode, "cboClientCode");
		((System.Windows.Forms.Control)(object)this.cboClientCode).Name = "cboClientCode";
		((TextEditorControlBase)this.cboClientCode).ValueChanged += new System.EventHandler(cboClientCode_ValueChanged);
		this.lblBalance.AutoEllipsis = false;
		resources.ApplyResources(this.lblBalance, "lblBalance");
		((System.Windows.Forms.Control)(object)this.lblBalance).Name = "lblBalance";
		((ControlBase)this.lblBalance).WrapText = false;
		resources.ApplyResources(this.txtBrabnchBalance, "txtBrabnchBalance");
		((System.Windows.Forms.Control)(object)this.txtBrabnchBalance).Name = "txtBrabnchBalance";
		((EditorButtonControlBase)this.txtBrabnchBalance).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtBrabnchBalance).TabStop = false;
		this.lblShiftNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblShiftNo, "lblShiftNo");
		((System.Windows.Forms.Control)(object)this.lblShiftNo).Name = "lblShiftNo";
		((ControlBase)this.lblShiftNo).WrapText = false;
		resources.ApplyResources(this.txtShiftNo, "txtShiftNo");
		((System.Windows.Forms.Control)(object)this.txtShiftNo).Name = "txtShiftNo";
		((EditorButtonControlBase)this.txtShiftNo).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtShiftNo).TabStop = false;
		this.lblShiftDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblShiftDate, "lblShiftDate");
		((System.Windows.Forms.Control)(object)this.lblShiftDate).Name = "lblShiftDate";
		((ControlBase)this.lblShiftDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpShiftDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpShiftDate, "dtpShiftDate");
		((System.Windows.Forms.Control)(object)this.dtpShiftDate).Name = "dtpShiftDate";
		((EditorButtonControlBase)this.dtpShiftDate).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.dtpShiftDate).TabStop = false;
		((AppearanceBase)val12).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnBranchesSearch).Appearance = (AppearanceBase)(object)val12;
		resources.ApplyResources(this.btnBranchesSearch, "btnBranchesSearch");
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).Name = "btnBranchesSearch";
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).Click += new System.EventHandler(btnBranchesSearch_Click);
		this.lblBranches.AutoEllipsis = false;
		resources.ApplyResources(this.lblBranches, "lblBranches");
		((System.Windows.Forms.Control)(object)this.lblBranches).Name = "lblBranches";
		((ControlBase)this.lblBranches).WrapText = false;
		((TextEditorControlBase)this.cboBranches).AlwaysInEditMode = true;
		this.cboBranches.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboBranches, "cboBranches");
		((System.Windows.Forms.Control)(object)this.cboBranches).Name = "cboBranches";
		((EditorButtonControlBase)this.cboBranches).ReadOnly = true;
		((TextEditorControlBase)this.cboBranches).ValueChanged += new System.EventHandler(cboBranches_ValueChanged);
		((AppearanceBase)val13).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnSalesMan2Search).Appearance = (AppearanceBase)(object)val13;
		resources.ApplyResources(this.btnSalesMan2Search, "btnSalesMan2Search");
		((System.Windows.Forms.Control)(object)this.btnSalesMan2Search).Name = "btnSalesMan2Search";
		((System.Windows.Forms.Control)(object)this.btnSalesMan2Search).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnSalesMan2Search).Click += new System.EventHandler(btnSalesMan2Search_Click);
		this.lblSalesMan2.AutoEllipsis = false;
		resources.ApplyResources(this.lblSalesMan2, "lblSalesMan2");
		((System.Windows.Forms.Control)(object)this.lblSalesMan2).Name = "lblSalesMan2";
		((ControlBase)this.lblSalesMan2).WrapText = false;
		((TextEditorControlBase)this.cboSalesMan2).AlwaysInEditMode = true;
		this.cboSalesMan2.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboSalesMan2, "cboSalesMan2");
		((System.Windows.Forms.Control)(object)this.cboSalesMan2).Name = "cboSalesMan2";
		((System.Windows.Forms.Control)(object)this.cboSalesMan2).TabStop = false;
		resources.ApplyResources(this.txtAccountBalance, "txtAccountBalance");
		((System.Windows.Forms.Control)(object)this.txtAccountBalance).Name = "txtAccountBalance";
		((EditorButtonControlBase)this.txtAccountBalance).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtAccountBalance).TabStop = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAccountBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSalesMan2Search);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesMan2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSalesMan2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnBranchesSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblShiftNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtShiftNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblShiftDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpShiftDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBrabnchBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClientCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotal);
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
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBackTitle);
		base.Name = "frmRevenues";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnImport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnExport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBackTitle, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAccountName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboAccountName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSubAccountName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSubAccountName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSubAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClientCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBrabnchBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpShiftDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblShiftDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtShiftNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblShiftNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnBranchesSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSalesMan2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSalesMan2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSalesMan2Search, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAccountBalance, 0);
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
		((System.ComponentModel.ISupportInitialize)this.txtTotal).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClientCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBrabnchBalance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtShiftNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpShiftDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesMan2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAccountBalance).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
