using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Defaults;
using BusinessLayer.General;
using BusinessLayer.Lenses;
using BusinessLayer.POS;
using BusinessLayer.Privilege;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Classes.DirectPrinting;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Lenses.Transactions;

public class frmOtherRevenues : frmHeaderDetails
{
	private DataTable dtReports;

	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private DataTable dtConn;

	private DataTable dtShiftDetails;

	private DataTable dtPOSDefaultData;

	private DataTable dtCurrency;

	private string ShiftDetailID;

	private string ShiftDetailUserID;

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

	private UltraLabel lblShiftNo;

	private UltraTextEditor txtShiftNo;

	private UltraLabel lblShiftDate;

	private UltraDateTimeEditor dtpShiftDate;

	public frmOtherRevenues()
	{
		InitializeComponent();
		TableName = "Lns_Revenues";
		IDCol = "RevenueID";
		NoCol = "RevenueNo";
		DateCol = "RevenueDate";
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
		dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSubAccountName, dtSubAccounts, "SubAccountID", "Name");
	}

	public override void SetSecurity()
	{
		if (Main.IsSynchronization)
		{
			dtConn = SyncConnection.Select("-1", "-1", "0");
			if (dtConn.Rows[0]["SyncBranchID"].Equals(DBNull.Value) && !bool.Parse(Branches.Select(GlobalVariables.CurrentBranchID, GlobalVariables.BranchIDs, "0", IsFromServer: false).Rows[0]["IsMainBranch"].ToString()))
			{
				CanEditFromServer = false;
			}
			CanAdd = CanEditFromServer && CanAdd;
			CanDelete = CanEditFromServer && CanDelete;
			((Control)(object)btnAdd).Enabled = CanAdd;
			((Control)(object)btnUpdate).Enabled = CanUpdate;
			((Control)(object)btnDelete).Enabled = CanDelete;
		}
		else
		{
			((Control)(object)btnAdd).Enabled = CanAdd;
			((Control)(object)btnUpdate).Enabled = CanUpdate;
			((Control)(object)btnDelete).Enabled = CanDelete;
			((Control)(object)btnPrint).Enabled = CanPrint;
		}
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = Revenues.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["RevenueNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["RevenueDate"];
			((TextEditorControlBase)cboAccountName).Value = drMaster["AccountID"];
			((TextEditorControlBase)cboSubAccountName).Value = drMaster["SubAccountID"];
			dtShiftDetails = ShiftsDetails.SelectByShiftDetailID(drMaster["ShiftDetailID"].ToString());
			if (dtShiftDetails.Rows.Count > 0)
			{
				((Control)(object)txtShiftNo).Text = dtShiftDetails.Rows[0]["ShiftDetailNo"].ToString();
				dtpShiftDate.Value = (DateTime)dtShiftDetails.Rows[0]["StartDate"];
			}
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtTotal).Text = decimal.Parse(drMaster["TotalAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
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
			if (!Main.IsSynchronization || (dtConn != null && dtConn.Rows.Count > 0 && dtConn.Rows[0]["SyncBranchID"].Equals(DBNull.Value)))
			{
				((Control)(object)btnAdd).Enabled = CanAdd && CanEditFromServer;
				((Control)(object)btnUpdate).Enabled = CanUpdate;
				((Control)(object)btnDelete).Enabled = CanDelete && CanEditFromServer;
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
		((EditorButtonControlBase)cboAccountName).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSubAccountName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode || (!Main.IsSynchronization && drMaster != null && drMaster["Approved"].Equals(true)) || (dtConn != null && dtConn.Rows[0]["SyncBranchID"].Equals(DBNull.Value) && !bool.Parse(Branches.Select(GlobalVariables.CurrentBranchID, GlobalVariables.BranchIDs, "0", IsFromServer: false).Rows[0]["IsMainBranch"].ToString()));
		((EditorButtonControlBase)txtTotal).ReadOnly = NavMode || (!Main.IsSynchronization && drMaster != null && drMaster["Approved"].Equals(true)) || (dtConn != null && dtConn.Rows[0]["SyncBranchID"].Equals(DBNull.Value) && !bool.Parse(Branches.Select(GlobalVariables.CurrentBranchID, GlobalVariables.BranchIDs, "0", IsFromServer: false).Rows[0]["IsMainBranch"].ToString()));
		((Control)(object)btnAccountSearch).Visible = !NavMode;
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
		cboAccountName.SelectedIndex = -1;
		cboSubAccountName.SelectedIndex = -1;
		((TextEditorControlBase)txtNotes).Clear();
		((Control)(object)txtTotal).Text = "0";
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
		return true;
	}

	public bool ValidateForShift()
	{
		if (!Main.IsSynchronization || (dtConn != null && !dtConn.Rows[0]["SyncBranchID"].Equals(DBNull.Value)))
		{
			DataTable dataTable = ShiftsDetails.SelectNotClosed(GlobalVariables.CurrentBranchID);
			if (dataTable.Rows.Count != 1)
			{
				GlobalVariables.InformationMB.Show("برجاء فتح وردية اولا", "Please open Shift First");
				return false;
			}
			DateTime dateTime = new DateTime(DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()).Year, DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()).Month, DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()).Day, DateTime.Parse(dataTable.Rows[0]["StartTime"].ToString()).Hour, DateTime.Parse(dataTable.Rows[0]["StartTime"].ToString()).Minute, DateTime.Parse(dataTable.Rows[0]["StartTime"].ToString()).Second);
			DateTime dateTime2 = dateTime.AddHours(DateTime.Parse(dataTable.Rows[0]["ShiftPeriod"].ToString()).Hour).AddMinutes(DateTime.Parse(dataTable.Rows[0]["ShiftPeriod"].ToString()).Minute).AddSeconds(DateTime.Parse(dataTable.Rows[0]["ShiftPeriod"].ToString()).Second);
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
			if (Adding && dtpDate.DateTime < DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()))
			{
				GlobalVariables.InformationMB.Show("تاريخ الشيك أقل من تاريخ بداية الوردية تاكد من تاريخ الجهاز", "Check Date More Less Than Shift End Date Check Your pc ");
				return false;
			}
			if (drMaster != null && drMaster["Approved"].Equals(false) && Updating && dtpDate.DateTime < DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()))
			{
				GlobalVariables.InformationMB.Show("تاريخ الشيك أقل من تاريخ بداية الوردية تاكد من تاريخ الجهاز", "Check Date More Less Than Shift End Date Check Your pc ");
				return false;
			}
			DataTable dataTable2 = ShiftsDetailsUsers.SelectNotClosed(dataTable.Rows[0]["ShiftDetailID"].ToString(), GlobalVariables.UserID, GlobalVariables.CurrentBranchID);
			ShiftDetailID = dataTable.Rows[0]["ShiftDetailID"].ToString();
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
			if (dataTable2.Rows.Count == 0)
			{
				ShiftDetailUserID = ShiftsDetailsUsers.Insert_Update("-1", ShiftDetailID, GlobalVariables.UserID, GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate), "Null", "0", dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), "0", "0", "0", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID).ToString();
			}
			else
			{
				ShiftDetailUserID = dataTable2.Rows[0]["ShiftDetailUserID"].ToString();
			}
		}
		if (dtConn != null && dtConn.Rows[0]["SyncBranchID"].Equals(DBNull.Value) && Updating && drMaster != null && !bool.Parse(Branches.Select(GlobalVariables.CurrentBranchID, GlobalVariables.BranchIDs, "0", IsFromServer: false).Rows[0]["IsMainBranch"].ToString()))
		{
			DataTable dataTable3 = ShiftsDetails.SelectByShiftDetailID(drMaster["ShiftDetailID"].ToString());
			if (dataTable3.Rows.Count > 0 && !bool.Parse(dataTable3.Rows[0]["IsClosed"].ToString()))
			{
				GlobalVariables.InformationMB.Show("لايمكن تعديل الايراد حتى يتم إغلاق الوردية", "Cannot Update The Revenues Till The Shift Closed");
				return false;
			}
			if (dataTable3.Rows.Count > 0 && int.Parse(dataTable3.Rows[0]["RevenueCount"].ToString()) != 0 && int.Parse(dataTable3.Rows[0]["RevenueCount"].ToString()) != Revenues.SelectCountByShiftDetailID(drMaster["ShiftDetailID"].ToString()))
			{
				GlobalVariables.InformationMB.Show("لايمكن تعديل الايراد حتى يتم رفع جميع ايرادات الوردية", "Cannot Update The Revenue Till All Revenues Uploaded For This Shift");
				return false;
			}
		}
		if (dtConn != null && dtConn.Rows[0]["SyncBranchID"].Equals(DBNull.Value) && Adding)
		{
			DataTable dataTable4 = ShiftsDetails.SelectNotClosed(GlobalVariables.CurrentBranchID);
			if (dataTable4.Rows.Count != 1)
			{
				GlobalVariables.InformationMB.Show("برجاء فتح وردية اولا", "Please open Shift First");
				return false;
			}
			DateTime dateTime3 = new DateTime(DateTime.Parse(dataTable4.Rows[0]["StartDate"].ToString()).Year, DateTime.Parse(dataTable4.Rows[0]["StartDate"].ToString()).Month, DateTime.Parse(dataTable4.Rows[0]["StartDate"].ToString()).Day, DateTime.Parse(dataTable4.Rows[0]["StartTime"].ToString()).Hour, DateTime.Parse(dataTable4.Rows[0]["StartTime"].ToString()).Minute, DateTime.Parse(dataTable4.Rows[0]["StartTime"].ToString()).Second);
			DateTime dateTime4 = dateTime3.AddHours(DateTime.Parse(dataTable4.Rows[0]["ShiftPeriod"].ToString()).Hour).AddMinutes(DateTime.Parse(dataTable4.Rows[0]["ShiftPeriod"].ToString()).Minute).AddSeconds(DateTime.Parse(dataTable4.Rows[0]["ShiftPeriod"].ToString()).Second);
			if (GlobalFunctions.GetServerDateTimeNow() < dateTime3)
			{
				GlobalVariables.InformationMB.Show(dateTime3.ToShortTimeString() + " وقت بداية الوردية ", " Shift Start Time Is " + dateTime3.ToShortTimeString());
				return false;
			}
			if (GlobalFunctions.GetServerDateTimeNow() > dateTime4.AddHours(2.0))
			{
				GlobalVariables.InformationMB.Show(dateTime4.ToShortTimeString() + " وقت نهاية الوردية ", " Shift End Time Is " + dateTime4.ToShortTimeString());
				return false;
			}
			if (dtpDate.DateTime < DateTime.Parse(dataTable4.Rows[0]["StartDate"].ToString()))
			{
				GlobalVariables.InformationMB.Show("تاريخ الشيك أقل من تاريخ بداية الوردية تاكد من تاريخ الجهاز", "Check Date More Less Than Shift End Date Check Your pc ");
				return false;
			}
			DataTable dataTable5 = ShiftsDetailsUsers.SelectNotClosed(dataTable4.Rows[0]["ShiftDetailID"].ToString(), GlobalVariables.UserID, GlobalVariables.CurrentBranchID);
			ShiftDetailID = dataTable4.Rows[0]["ShiftDetailID"].ToString();
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
			if (dataTable5.Rows.Count == 0)
			{
				ShiftDetailUserID = ShiftsDetailsUsers.Insert_Update("-1", ShiftDetailID, GlobalVariables.UserID, GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate), "Null", "0", dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), "0", "0", "0", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID).ToString();
			}
			else
			{
				ShiftDetailUserID = dataTable5.Rows[0]["ShiftDetailUserID"].ToString();
			}
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
			num = Revenues.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "1", ShiftDetailID, ShiftDetailUserID, GlobalVariables.UserID, "Null", (cboAccountName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAccountName).Value.ToString(), (cboSubAccountName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccountName).Value.ToString(), dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), ((Control)(object)txtNotes).Text, (((Control)(object)txtTotal).Text == "") ? "0" : ((Control)(object)txtTotal).Text, "Null", (cboSubAccountName.SelectedIndex > -1 && cboAccountName.SelectedIndex > -1 && dtSubAccounts.Select(" (SubAccountTypeID=4 or SubAccountTypeID=5 or SubAccountTypeID=7)And DefaultClientAccountID=" + ((TextEditorControlBase)cboAccountName).Value.ToString() + "  and SubAccountID= " + ((TextEditorControlBase)cboSubAccountName).Value.ToString()).Length != 0) ? dtSubAccounts.Select(" (SubAccountTypeID=4 or SubAccountTypeID=5 or SubAccountTypeID=7)And DefaultClientAccountID=" + ((TextEditorControlBase)cboAccountName).Value.ToString() + " and SubAccountID= " + ((TextEditorControlBase)cboSubAccountName).Value.ToString())[0]["BranchID"].ToString() : "Null", "Null", "Null", "Null", "1", "Null", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
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
			int num = Revenues.Insert_Update(drMaster["RevenueID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "1", drMaster["ShiftDetailID"].ToString(), drMaster["ShiftDetailUserID"].ToString(), (drMaster["User_ID"] == DBNull.Value) ? "Null" : drMaster["User_ID"].ToString(), "Null", (cboAccountName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAccountName).Value.ToString(), (cboSubAccountName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccountName).Value.ToString(), drMaster["CurrencyID"].ToString(), drMaster["ExchangeRate"].ToString(), ((Control)(object)txtNotes).Text, (((Control)(object)txtTotal).Text == "") ? "0" : ((Control)(object)txtTotal).Text, "Null", (cboSubAccountName.SelectedIndex > -1 && cboAccountName.SelectedIndex > -1 && dtSubAccounts.Select(" (SubAccountTypeID=4 or SubAccountTypeID=5 or SubAccountTypeID=7)And DefaultClientAccountID=" + ((TextEditorControlBase)cboAccountName).Value.ToString() + "  and SubAccountID= " + ((TextEditorControlBase)cboSubAccountName).Value.ToString()).Length != 0) ? dtSubAccounts.Select(" (SubAccountTypeID=4 or SubAccountTypeID=5 or SubAccountTypeID=7)And DefaultClientAccountID=" + ((TextEditorControlBase)cboAccountName).Value.ToString() + " and SubAccountID= " + ((TextEditorControlBase)cboSubAccountName).Value.ToString())[0]["BranchID"].ToString() : "Null", (drMaster["TransferJVID"] == DBNull.Value) ? "Null" : drMaster["TransferJVID"].ToString(), (drMaster["TransferJVID2"] == DBNull.Value) ? "Null" : drMaster["TransferJVID2"].ToString(), "Null", "1", "Null", bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			DataTable dataTable = ShiftsDetails.SelectByShiftDetailID(drMaster["ShiftDetailID"].ToString());
			if (dataTable.Rows.Count > 0 && int.Parse(dataTable.Rows[0]["RevenueCount"].ToString()) == 0)
			{
				ShiftsDetails.SalesJVRegenerate(drMaster["ShiftDetailID"].ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				ShiftsDetails.ReturnJVRegenerate(drMaster["ShiftDetailID"].ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				ShiftsDetails.RevenueJVRegenerate(drMaster["ShiftDetailID"].ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				ShiftsDetails.ExpenseJVRegenerate(drMaster["ShiftDetailID"].ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				ShiftsDetails.CloseJVRegenerate(drMaster["ShiftDetailID"].ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			}
			else
			{
				ShiftsDetails.RevenueJVRegenerate(drMaster["ShiftDetailID"].ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
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
		dtPOSDefaultData = BusinessLayer.POS.Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboAccountName, dtAccounts, "AccountID", "Name");
		dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSubAccountName, dtSubAccounts, "SubAccountID", "Name");
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
		dtSearchResult = SearchFunctions.LnsRevenuesReport(1, -1, 0);
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
		((TextEditorControlBase)cboAccountName).Value = SearchFunctions.Accounts(IsFromServer: false);
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
		if ((Adding || Updating) && e.KeyValue == 119 && cboAccountName.SelectedIndex != -1)
		{
			int num = SearchFunctions.SubAccounts(((TextEditorControlBase)cboAccountName).Value.ToString(), IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboSubAccountName).Value = num;
			}
		}
	}

	private void btnSubAccountSearch_Click(object sender, EventArgs e)
	{
		if (cboAccountName.SelectedIndex != -1)
		{
			int num = SearchFunctions.SubAccounts(((TextEditorControlBase)cboAccountName).Value.ToString(), IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboSubAccountName).Value = num;
			}
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
		DataTable comboData = Main.GetComboData(TableName, IDCol, NoCol + "=''" + ((Control)(object)txtCode).Text.Trim() + "'' And Deleted=0 And IsOtherRevenue=1");
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
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, " And IsOtherRevenue=1 ", "0");
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
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, " And IsOtherRevenue=1 ", "1");
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
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Expected O, but got Unknown
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Expected O, but got Unknown
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Expected O, but got Unknown
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Expected O, but got Unknown
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.Transactions.frmOtherRevenues));
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
		this.lblShiftNo = new UltraLabel();
		this.txtShiftNo = new UltraTextEditor();
		this.lblShiftDate = new UltraLabel();
		this.dtpShiftDate = new UltraDateTimeEditor();
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
		((System.ComponentModel.ISupportInitialize)this.txtShiftNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpShiftDate).BeginInit();
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
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnSubAccountSearch).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(this.btnSubAccountSearch, "btnSubAccountSearch");
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).Name = "btnSubAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).Click += new System.EventHandler(btnSubAccountSearch_Click);
		this.cboSubAccountName.AutoCompleteMode = (AutoCompleteMode)2;
		resources.ApplyResources(this.cboSubAccountName, "cboSubAccountName");
		((System.Windows.Forms.Control)(object)this.cboSubAccountName).Name = "cboSubAccountName";
		((System.Windows.Forms.Control)(object)this.cboSubAccountName).KeyDown += new System.Windows.Forms.KeyEventHandler(cboSubAccountName_KeyDown);
		this.lblSubAccountName.AutoEllipsis = false;
		resources.ApplyResources(this.lblSubAccountName, "lblSubAccountName");
		((System.Windows.Forms.Control)(object)this.lblSubAccountName).Name = "lblSubAccountName";
		((ControlBase)this.lblSubAccountName).WrapText = false;
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnAccountSearch).Appearance = (AppearanceBase)(object)val10;
		resources.ApplyResources(this.btnAccountSearch, "btnAccountSearch");
		((System.Windows.Forms.Control)(object)this.btnAccountSearch).Name = "btnAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnAccountSearch).Click += new System.EventHandler(btnAccountSearch_Click);
		this.cboAccountName.AutoCompleteMode = (AutoCompleteMode)2;
		resources.ApplyResources(this.cboAccountName, "cboAccountName");
		((System.Windows.Forms.Control)(object)this.cboAccountName).Name = "cboAccountName";
		((TextEditorControlBase)this.cboAccountName).ValueChanged += new System.EventHandler(cboAccountName_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboAccountName).KeyDown += new System.Windows.Forms.KeyEventHandler(cboAccountName_KeyDown);
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
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.lblBackTitle).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.lblBackTitle).Name = "lblBackTitle";
		((UltraControlBase)this.lblBackTitle).UseAppStyling = false;
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
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblShiftNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtShiftNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblShiftDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpShiftDate);
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
		base.Name = "frmOtherRevenues";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpShiftDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblShiftDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtShiftNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblShiftNo, 0);
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
		((System.ComponentModel.ISupportInitialize)this.txtShiftNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpShiftDate).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
