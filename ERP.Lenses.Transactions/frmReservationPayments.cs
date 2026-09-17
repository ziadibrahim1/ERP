using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.Lenses;
using BusinessLayer.POS;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.SystemOptions.GeneralData;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Lenses.Transactions;

public class frmReservationPayments : frmHeaderDetails
{
	private DataTable dtReservations;

	private DataTable dtVisaType;

	private DataTable dtPOSDefaultData;

	private DataTable dtCurrency;

	private string ShiftDetailID;

	private string ShiftDetailUserID;

	private int ReservationID = 0;

	private int ReservationPaymentID;

	private decimal RestAmount = -1m;

	private IContainer components = null;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboReservationNo;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblOrderNo;

	public UltraButton btnVisaAmount;

	private UltraLabel lblVisaAmount;

	public UltraTextEditor txtVisaAmount;

	public UltraCheckEditor chkVisa;

	private UltraLabel lblCashAmount;

	public UltraTextEditor txtCashAmount;

	public UltraButton btnVisaNo;

	public UltraButton btnCashAmount;

	public UltraTextEditor txtVisaNo;

	private UltraLabel lblVisaNo;

	private UltraLabel lblVisaType;

	public UltraCheckEditor chkCash;

	public UltraComboEditor cboVisaType;

	public UltraCheckEditor chkIsOnAccount;

	public UltraTextEditor txtOnAccountAmount;

	private UltraLabel lblOnAccountAmount;

	public frmReservationPayments()
	{
		InitializeComponent();
		TableName = "Lns_ReservationsPayments";
		IDCol = "ReservationPaymentID";
		NoCol = "ReservationPaymentNo";
		DateCol = "ReservationPaymentDate";
	}

	public frmReservationPayments(int _ReservationPaymentID)
		: this()
	{
		ReservationPaymentID = _ReservationPaymentID;
	}

	public frmReservationPayments(int reservationID, decimal restamount)
		: this()
	{
		ReservationID = reservationID;
		RestAmount = restamount;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtPOSDefaultData = Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtReservations = Reservations.FillCombo("-1", GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboReservationNo, dtReservations, "ReservationID", "ReservationNo");
		dtVisaType = VisaTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboVisaType, dtVisaType, "VisaTypeID", "VisaTypeName");
	}

	public override void FillData()
	{
		if (ReservationPaymentID != 0)
		{
			DataTable dataTable = ReservationsPayments.Select(ReservationPaymentID.ToString(), GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
			if (dataTable.Rows.Count > 0)
			{
				drMaster = dataTable.Rows[0];
			}
			else
			{
				drMaster = null;
			}
		}
		else if (ReservationID != 0)
		{
			drMaster = null;
			btnAddClick();
			((Control)(object)btnOK).Visible = false;
		}
		else if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable2 = ReservationsPayments.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
			((Control)(object)txtCode).Text = drMaster["ReservationPaymentNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["ReservationPaymentDate"];
			((TextEditorControlBase)cboReservationNo).Value = drMaster["ReservationID"];
			((UltraToggleEditorBase)chkCash).Checked = bool.Parse(drMaster["IsCash"].ToString());
			((Control)(object)txtCashAmount).Text = decimal.Parse(drMaster["CashAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((UltraToggleEditorBase)chkIsOnAccount).Checked = bool.Parse(drMaster["IsOnAccount"].ToString());
			((Control)(object)txtOnAccountAmount).Text = decimal.Parse(drMaster["OnAccountAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((UltraToggleEditorBase)chkVisa).Checked = bool.Parse(drMaster["IsVisa"].ToString());
			((Control)(object)txtVisaAmount).Text = decimal.Parse(drMaster["VisaAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)cboVisaType).Value = drMaster["VisaTypeID"];
			((Control)(object)txtVisaNo).Text = drMaster["VisaNo"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
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
		((EditorButtonControlBase)cboReservationNo).ReadOnly = NavMode || ReservationID != 0;
		((Control)(object)chkCash).Enabled = !NavMode;
		((Control)(object)chkIsOnAccount).Enabled = !NavMode;
		((Control)(object)chkVisa).Enabled = !NavMode;
		((EditorButtonControlBase)txtCashAmount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtOnAccountAmount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtVisaNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)btnCopyTo).Visible = false;
		if (Adding)
		{
			DataView dataView = new DataView(dtReservations);
			dataView.RowFilter = " IsDeliverd = 0 And IsCancelled = 0 And BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboReservationNo, dataView.ToTable(), "ReservationID", "ReservationNo");
			DataView dataView2 = new DataView(dtVisaType);
			dataView2.RowFilter = " IsActive =1 ";
			DataTable dt = dataView2.ToTable();
			GlobalFunctions.FillCombo(cboVisaType, dt, "VisaTypeID", "VisaTypeName");
		}
		else
		{
			GlobalFunctions.FillCombo(cboReservationNo, dtReservations, "ReservationID", "ReservationNo");
			GlobalFunctions.FillCombo(cboVisaType, dtVisaType, "VisaTypeID", "VisaTypeName");
		}
	}

	public override void ClearControls()
	{
		base.ClearControls();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? ReservationsPayments.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		cboReservationNo.SelectedIndex = -1;
		if (ReservationID != 0)
		{
			((TextEditorControlBase)cboReservationNo).Value = ReservationID.ToString();
		}
		((UltraToggleEditorBase)chkCash).Checked = true;
		((Control)(object)txtCashAmount).Text = "0";
		((UltraToggleEditorBase)chkIsOnAccount).Checked = false;
		((Control)(object)txtOnAccountAmount).Text = "0";
		((UltraToggleEditorBase)chkVisa).Checked = false;
		cboVisaType.SelectedIndex = -1;
		((TextEditorControlBase)txtVisaNo).Clear();
		((Control)(object)txtVisaAmount).Text = "0";
		((TextEditorControlBase)txtNotes).Clear();
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
		if (cboReservationNo.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار رقم الحجز" : "Please Select Reservation No");
			((TextEditorControlBase)cboReservationNo).Focus();
			return false;
		}
		if (Reservations.CheckIsDeliverdOrCancelled(((TextEditorControlBase)cboReservationNo).Value.ToString()))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "تم عمل فاتورة لهذا الحجز او تم الغاؤه" : "There is An invoice For This Reservation or Has Been Canceled");
			((TextEditorControlBase)cboReservationNo).Focus();
			return false;
		}
		if ((((Control)(object)txtCashAmount).Text == "" || ((Control)(object)txtCashAmount).Text == "." || decimal.Parse(((Control)(object)txtCashAmount).Text) <= 0m) && ((UltraToggleEditorBase)chkCash).Checked)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال القيمة فى المبلغ المدفوع نقدآ" : "Please Enter Amount in Cash Amount");
			return false;
		}
		if ((((Control)(object)txtOnAccountAmount).Text == "" || ((Control)(object)txtOnAccountAmount).Text == "." || decimal.Parse(((Control)(object)txtOnAccountAmount).Text) <= 0m) && ((UltraToggleEditorBase)chkIsOnAccount).Checked)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال القيمة فى المبلغ على الحساب " : "Please Enter Amount in OnAccount Amount");
			return false;
		}
		if ((((Control)(object)txtVisaAmount).Text == "" || ((Control)(object)txtVisaAmount).Text == "." || decimal.Parse(((Control)(object)txtVisaAmount).Text) <= 0m) && ((UltraToggleEditorBase)chkVisa).Checked)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال القيمة فى المبلغ المدفوع بالفيزا" : "Please Enter Amount in Visa Amount");
			return false;
		}
		decimal num = decimal.Parse(dtReservations.Select(" ReservationID = " + ((TextEditorControlBase)cboReservationNo).Value.ToString())[0]["PaidAmount"].ToString()) + decimal.Parse(((Control)(object)txtVisaAmount).Text) + decimal.Parse(((Control)(object)txtCashAmount).Text) + decimal.Parse(((Control)(object)txtOnAccountAmount).Text);
		decimal num2 = decimal.Parse(dtReservations.Select(" ReservationID = " + ((TextEditorControlBase)cboReservationNo).Value.ToString())[0]["NetPrice"].ToString());
		if (num > num2)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "أجمالي المدفوع أكبر من القيمة الكلية" : "Total Paid Amount Greater Than Net Price");
			return false;
		}
		if (((Control)(object)txtVisaNo).Text == "" && ((UltraToggleEditorBase)chkVisa).Checked)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم الفيزا" : "Please Enter Visa No");
			return false;
		}
		if (cboVisaType.SelectedIndex == -1 && ((UltraToggleEditorBase)chkVisa).Checked)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار نوع الفيزا" : "Please Select Visa Type");
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("Lns_ReservationsPayments", "ReservationPaymentNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["ReservationPaymentNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = ReservationsPayments.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Voucher Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByBranchID;
		}
		else if (dtPOSDefaultData.Rows[0]["DefaultSubAccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب العميل الافتراضى من اعدادات البيع  ", "Please Select Default Client ID From Sales Settings ");
			return false;
		}
		return true;
	}

	public bool ValidateForShift()
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
		if (dtpDate.DateTime < DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()))
		{
			GlobalVariables.InformationMB.Show("تاريخ الحجز أقل من تاريخ بداية الوردية تاكد من تاريخ الجهاز", "Reservation Date Less Than Shift End Date Check Your pc ");
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
		try
		{
			int num = ReservationsPayments.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboReservationNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboReservationNo).Value.ToString(), ((UltraToggleEditorBase)chkCash).Checked ? "1" : "0", (((Control)(object)txtCashAmount).Text == "") ? "0" : ((Control)(object)txtCashAmount).Text, ((UltraToggleEditorBase)chkIsOnAccount).Checked ? "1" : "0", (((Control)(object)txtOnAccountAmount).Text == "") ? "0" : ((Control)(object)txtOnAccountAmount).Text, ((UltraToggleEditorBase)chkVisa).Checked ? "1" : "0", (cboVisaType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboVisaType).Value.ToString(), (((Control)(object)txtVisaNo).Text == "") ? "Null" : ((Control)(object)txtVisaNo).Text, (((Control)(object)txtVisaAmount).Text == "") ? "0" : ((Control)(object)txtVisaAmount).Text, dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), ((Control)(object)txtNotes).Text, ShiftDetailID, ShiftDetailUserID, GlobalVariables.UserID, "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Reservations.UpdatePaidAmount(((TextEditorControlBase)cboReservationNo).Value.ToString(), GlobalVariables.UserID);
			ShiftsDetails.ReservationsPaymentsJVAdding(num.ToString(), ShiftDetailID, GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
			RowID = num.ToString();
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
			int num = ReservationsPayments.Insert_Update(drMaster["ReservationPaymentID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboReservationNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboReservationNo).Value.ToString(), ((UltraToggleEditorBase)chkCash).Checked ? "1" : "0", (((Control)(object)txtCashAmount).Text == "") ? "0" : ((Control)(object)txtCashAmount).Text, ((UltraToggleEditorBase)chkIsOnAccount).Checked ? "1" : "0", (((Control)(object)txtOnAccountAmount).Text == "") ? "0" : ((Control)(object)txtOnAccountAmount).Text, ((UltraToggleEditorBase)chkVisa).Checked ? "1" : "0", (cboVisaType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboVisaType).Value.ToString(), (((Control)(object)txtVisaNo).Text == "") ? "Null" : ((Control)(object)txtVisaNo).Text, (((Control)(object)txtVisaAmount).Text == "") ? "0" : ((Control)(object)txtVisaAmount).Text, dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), ((Control)(object)txtNotes).Text, ShiftDetailID, ShiftDetailUserID, GlobalVariables.UserID, "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Reservations.UpdatePaidAmount(drMaster["ReservationID"].ToString(), GlobalVariables.UserID);
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

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			ReservationsPayments.DeleteVirtual(drMaster["ReservationPaymentID"].ToString(), GlobalVariables.UserID);
			Reservations.UpdatePaidAmount(drMaster["ReservationID"].ToString(), GlobalVariables.UserID);
			ShiftsDetails.RevenueJVRegenerate(drMaster["ShiftDetailID"].ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
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
		dtReservations = Reservations.FillCombo("-1", GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboReservationNo, dtReservations, "ReservationID", "ReservationNo");
		dtVisaType = VisaTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (Adding)
		{
			DataView dataView = new DataView(dtVisaType);
			dataView.RowFilter = " IsActive =1 ";
			DataTable dt = dataView.ToTable();
			GlobalFunctions.FillCombo(cboVisaType, dt, "VisaTypeID", "VisaTypeName");
		}
		else
		{
			GlobalFunctions.FillCombo(cboVisaType, dtVisaType, "VisaTypeID", "VisaTypeName");
		}
	}

	public override void btnPrintClick()
	{
		if (RowID != "")
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Lns_ReservationsPayments_A.rpt" : "Rep_Lns_ReservationsPayments_E.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@ReservationPaymentIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			GlobalVariables.ReportDocument.PrintOptions.PrinterName = GlobalVariables.POSPrinter;
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ReservationsPaymentsReport(0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["ReservationPaymentID"].ToString();
			FillData();
		}
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = ReservationsPayments.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void chkCash_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)txtCashAmount).Enabled = ((UltraToggleEditorBase)chkCash).Checked;
		if (!((UltraToggleEditorBase)chkCash).Checked)
		{
			((Control)(object)txtCashAmount).Text = "0";
		}
	}

	private void btnCashAmount_Click(object sender, EventArgs e)
	{
		frmDecimal frmDecimal2 = new frmDecimal((Control)(object)txtCashAmount, ((Control)(object)txtCashAmount).Text);
		frmDecimal2.StartPosition = FormStartPosition.Manual;
		frmDecimal2.Location = new Point(((Control)(object)txtCashAmount).Location.X + frmDecimal2.Width, ((Control)(object)txtCashAmount).Location.Y + ((Control)(object)txtCashAmount).Height + 10);
		frmDecimal2.Show();
	}

	private void chkVisa_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)txtVisaNo).Enabled = ((UltraToggleEditorBase)chkVisa).Checked;
		((Control)(object)txtVisaAmount).Enabled = ((UltraToggleEditorBase)chkVisa).Checked;
		((Control)(object)cboVisaType).Enabled = ((UltraToggleEditorBase)chkVisa).Checked;
		if (!((UltraToggleEditorBase)chkVisa).Checked)
		{
			((Control)(object)txtVisaAmount).Text = "0";
		}
	}

	private void btnVisaNo_Click(object sender, EventArgs e)
	{
		frmDecimal frmDecimal2 = new frmDecimal((Control)(object)txtVisaNo, ((Control)(object)txtVisaNo).Text);
		frmDecimal2.StartPosition = FormStartPosition.Manual;
		frmDecimal2.Location = new Point(((Control)(object)txtVisaNo).Location.X + frmDecimal2.Width, ((Control)(object)txtVisaNo).Location.Y + ((Control)(object)txtVisaNo).Height + 10);
		frmDecimal2.Show();
	}

	private void btnVisaAmount_Click(object sender, EventArgs e)
	{
		frmDecimal frmDecimal2 = new frmDecimal((Control)(object)txtVisaAmount, ((Control)(object)txtVisaAmount).Text);
		frmDecimal2.StartPosition = FormStartPosition.Manual;
		frmDecimal2.Location = new Point(((Control)(object)txtVisaAmount).Location.X + frmDecimal2.Width, ((Control)(object)txtVisaAmount).Location.Y + ((Control)(object)txtVisaAmount).Height + 10);
		frmDecimal2.Show();
	}

	private void txtCashAmount_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtVisaAmount_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void chkIsOnAccount_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)txtOnAccountAmount).Enabled = ((UltraToggleEditorBase)chkIsOnAccount).Checked;
		if (!((UltraToggleEditorBase)chkIsOnAccount).Checked)
		{
			((Control)(object)txtOnAccountAmount).Text = "0";
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
		if (Reservations.CheckIsDeliverdOrCancelled(((TextEditorControlBase)cboReservationNo).Value.ToString()))
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذا الحجز ", "Cannot Delete This Transaction Because It Related to ClosedPeriod ");
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
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Expected O, but got Unknown
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected O, but got Unknown
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected O, but got Unknown
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Expected O, but got Unknown
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Expected O, but got Unknown
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.Transactions.frmReservationPayments));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboReservationNo = new UltraComboEditor();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblOrderNo = new UltraLabel();
		this.btnVisaAmount = new UltraButton();
		this.lblVisaAmount = new UltraLabel();
		this.txtVisaAmount = new UltraTextEditor();
		this.chkVisa = new UltraCheckEditor();
		this.lblCashAmount = new UltraLabel();
		this.txtCashAmount = new UltraTextEditor();
		this.btnVisaNo = new UltraButton();
		this.btnCashAmount = new UltraButton();
		this.txtVisaNo = new UltraTextEditor();
		this.lblVisaNo = new UltraLabel();
		this.lblVisaType = new UltraLabel();
		this.chkCash = new UltraCheckEditor();
		this.cboVisaType = new UltraComboEditor();
		this.chkIsOnAccount = new UltraCheckEditor();
		this.txtOnAccountAmount = new UltraTextEditor();
		this.lblOnAccountAmount = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboReservationNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkVisa).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCashAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkCash).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsOnAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOnAccountAmount).BeginInit();
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
		this.cboReservationNo.AutoCompleteMode = (AutoCompleteMode)2;
		resources.ApplyResources(this.cboReservationNo, "cboReservationNo");
		((System.Windows.Forms.Control)(object)this.cboReservationNo).Name = "cboReservationNo";
		((System.Windows.Forms.Control)(object)this.cboReservationNo).TabStop = false;
		this.lblNotes.AutoEllipsis = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		this.lblOrderNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblOrderNo, "lblOrderNo");
		((System.Windows.Forms.Control)(object)this.lblOrderNo).Name = "lblOrderNo";
		((ControlBase)this.lblOrderNo).WrapText = false;
		resources.ApplyResources(this.btnVisaAmount, "btnVisaAmount");
		((System.Windows.Forms.Control)(object)this.btnVisaAmount).Name = "btnVisaAmount";
		((System.Windows.Forms.Control)(object)this.btnVisaAmount).Click += new System.EventHandler(btnVisaAmount_Click);
		this.lblVisaAmount.AutoEllipsis = false;
		resources.ApplyResources(this.lblVisaAmount, "lblVisaAmount");
		((System.Windows.Forms.Control)(object)this.lblVisaAmount).Name = "lblVisaAmount";
		((ControlBase)this.lblVisaAmount).WrapText = false;
		resources.ApplyResources(this.txtVisaAmount, "txtVisaAmount");
		((System.Windows.Forms.Control)(object)this.txtVisaAmount).Name = "txtVisaAmount";
		((System.Windows.Forms.Control)(object)this.txtVisaAmount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtVisaAmount_KeyPress);
		resources.ApplyResources(this.chkVisa, "chkVisa");
		((System.Windows.Forms.Control)(object)this.chkVisa).Name = "chkVisa";
		((UltraToggleEditorBase)this.chkVisa).CheckedChanged += new System.EventHandler(chkVisa_CheckedChanged);
		this.lblCashAmount.AutoEllipsis = false;
		resources.ApplyResources(this.lblCashAmount, "lblCashAmount");
		((System.Windows.Forms.Control)(object)this.lblCashAmount).Name = "lblCashAmount";
		((ControlBase)this.lblCashAmount).WrapText = false;
		resources.ApplyResources(this.txtCashAmount, "txtCashAmount");
		((System.Windows.Forms.Control)(object)this.txtCashAmount).Name = "txtCashAmount";
		((System.Windows.Forms.Control)(object)this.txtCashAmount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCashAmount_KeyPress);
		resources.ApplyResources(this.btnVisaNo, "btnVisaNo");
		((System.Windows.Forms.Control)(object)this.btnVisaNo).Name = "btnVisaNo";
		((System.Windows.Forms.Control)(object)this.btnVisaNo).Click += new System.EventHandler(btnVisaNo_Click);
		resources.ApplyResources(this.btnCashAmount, "btnCashAmount");
		((System.Windows.Forms.Control)(object)this.btnCashAmount).Name = "btnCashAmount";
		((System.Windows.Forms.Control)(object)this.btnCashAmount).Click += new System.EventHandler(btnCashAmount_Click);
		resources.ApplyResources(this.txtVisaNo, "txtVisaNo");
		((System.Windows.Forms.Control)(object)this.txtVisaNo).Name = "txtVisaNo";
		this.lblVisaNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblVisaNo, "lblVisaNo");
		((System.Windows.Forms.Control)(object)this.lblVisaNo).Name = "lblVisaNo";
		((ControlBase)this.lblVisaNo).WrapText = false;
		this.lblVisaType.AutoEllipsis = false;
		resources.ApplyResources(this.lblVisaType, "lblVisaType");
		((System.Windows.Forms.Control)(object)this.lblVisaType).Name = "lblVisaType";
		((ControlBase)this.lblVisaType).WrapText = false;
		resources.ApplyResources(this.chkCash, "chkCash");
		((System.Windows.Forms.Control)(object)this.chkCash).Name = "chkCash";
		((UltraToggleEditorBase)this.chkCash).CheckedChanged += new System.EventHandler(chkCash_CheckedChanged);
		resources.ApplyResources(this.cboVisaType, "cboVisaType");
		((System.Windows.Forms.Control)(object)this.cboVisaType).Name = "cboVisaType";
		resources.ApplyResources(this.chkIsOnAccount, "chkIsOnAccount");
		((System.Windows.Forms.Control)(object)this.chkIsOnAccount).Name = "chkIsOnAccount";
		((UltraToggleEditorBase)this.chkIsOnAccount).CheckedChanged += new System.EventHandler(chkIsOnAccount_CheckedChanged);
		resources.ApplyResources(this.txtOnAccountAmount, "txtOnAccountAmount");
		((System.Windows.Forms.Control)(object)this.txtOnAccountAmount).Name = "txtOnAccountAmount";
		((System.Windows.Forms.Control)(object)this.txtOnAccountAmount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCashAmount_KeyPress);
		this.lblOnAccountAmount.AutoEllipsis = false;
		resources.ApplyResources(this.lblOnAccountAmount, "lblOnAccountAmount");
		((System.Windows.Forms.Control)(object)this.lblOnAccountAmount).Name = "lblOnAccountAmount";
		((ControlBase)this.lblOnAccountAmount).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnVisaAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVisaAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkVisa);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCashAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCashAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnVisaNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCashAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVisaNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboVisaType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkCash);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOrderNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboReservationNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtOnAccountAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsOnAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOnAccountAmount);
		base.Name = "frmReservationPayments";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnImport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnExport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOnAccountAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsOnAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtOnAccountAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboReservationNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOrderNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkCash, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboVisaType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVisaNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCashAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnVisaNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCashAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCashAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkVisa, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVisaAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnVisaAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
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
		((System.ComponentModel.ISupportInitialize)this.cboReservationNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkVisa).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCashAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkCash).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsOnAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOnAccountAmount).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
