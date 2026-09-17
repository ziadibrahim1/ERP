using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.POS;
using BusinessLayer.Privilege;
using BusinessLayer.Security;
using BusinessLayer.StockControl;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Classes.DirectPrinting;
using ERP.Properties;
using ERP.Ticketing;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.POS.Transactions;

public class frmShiftsDetails : frmHeaderDetails
{
	private DataTable dtReports;

	private DataTable dtPOSSettingsData;

	private DataTable dtUsers = new DataTable();

	private DataTable dtShifts = new DataTable();

	private ValueList vlUsers = new ValueList();

	private IContainer components = null;

	public UltraButton btnShiftSearch;

	private UltraLabel lblShift;

	private UltraComboEditor cboShift;

	private UltraLabel lblStartDate;

	private UltraDateTimeEditor dtpStartDate;

	private UltraLabel lblEndDate;

	private UltraDateTimeEditor dtpEndDate;

	private UltraTextEditor txtActualValue;

	private UltraLabel lblActualValue;

	private UltraTextEditor txtBookValue;

	private UltraLabel lblBookValue;

	private UltraTextEditor txtDifference;

	private UltraLabel lblDifference;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraCheckEditor chkClose;

	private UltraCheckEditor chkIsDirectSalesApp;

	public frmShiftsDetails()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		InitializeComponent();
		TableName = "POS_ShiftsDetails";
		IDCol = "ShiftDetailID";
		NoCol = "ShiftDetailNo";
		DateCol = "StartDate";
	}

	public frmShiftsDetails(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpStartDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtpEndDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtShifts = Shifts.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboShift, dtShifts, "ShiftID", "ShiftName");
		dtUsers = Users.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUsers.ValueListItems.Clear();
		for (int i = 0; i < dtUsers.Rows.Count; i++)
		{
			vlUsers.ValueListItems.Add(dtUsers.Rows[i]["User_ID"], dtUsers.Rows[i][GlobalVariables.IsArabic ? "UserNameAr" : "UserNameEn"].ToString());
		}
		dtPOSSettingsData = Main.ExecuteQuery_DataTable(" Rep_POS_Settings_SelectByBranchID " + GlobalVariables.CurrentBranchID + "," + (GlobalVariables.IsArabic ? "1" : "0"));
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtDetails = ShiftsDetailsUsers.SelectByShiftDetailID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftDetailUserID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["User_ID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["User_ID"].Header).Caption = (GlobalVariables.IsArabic ? "المستخدم" : "User Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["User_ID"].ValueList = (IValueList)(object)vlUsers;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StartDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StartDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ البدأ " : "Start Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StartDate"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EndDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EndDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ النهاية " : "End Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EndDate"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualValue"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualValue"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة الفعلية " : "Actual Value");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BookValue"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BookValue"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة الدفترية " : "Book value");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Difference"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Difference"].Header).Caption = (GlobalVariables.IsArabic ? "الفرق" : "Difference");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsClosed"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsClosed"].Header).Caption = (GlobalVariables.IsArabic ? "إغلاق" : "Closed");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["User_ID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StartDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EndDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BookValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Difference"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsClosed"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = ShiftsDetails.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
		if (drMaster != null)
		{
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["ShiftDetailNo"].ToString();
			((TextEditorControlBase)cboShift).Value = drMaster["ShiftID"];
			dtpStartDate.Value = drMaster["StartDate"];
			dtpEndDate.Value = drMaster["EndDate"];
			((Control)(object)txtActualValue).Text = decimal.Parse(drMaster["ActualValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtBookValue).Text = decimal.Parse(drMaster["BookValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDifference).Text = decimal.Parse(drMaster["Difference"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((UltraToggleEditorBase)chkClose).Checked = bool.Parse(drMaster["IsClosed"].ToString());
			((UltraToggleEditorBase)chkIsDirectSalesApp).Checked = bool.Parse(drMaster["IsDirectSalesApp"].ToString());
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpStartDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = ShiftsDetailsUsers.SelectByShiftDetailID(drMaster["ShiftDetailID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
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
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((EditorButtonControlBase)cboShift).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpStartDate).ReadOnly = true;
		((EditorButtonControlBase)dtpEndDate).ReadOnly = true;
		((EditorButtonControlBase)txtActualValue).ReadOnly = true;
		((EditorButtonControlBase)txtBookValue).ReadOnly = true;
		((EditorButtonControlBase)txtDifference).ReadOnly = true;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)chkClose).Visible = !Adding;
		((Control)(object)chkClose).Enabled = !Adding && !NavMode;
		((Control)(object)btnShiftSearch).Visible = !NavMode;
		((Control)(object)chkIsDirectSalesApp).Visible = Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='DirectSalesApp'")[0]["Installed"]);
		((Control)(object)btnDelete).Visible = false;
		((Control)(object)btnCopyTo).Visible = false;
		((Control)(object)btnRefreshData).Visible = false;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		dtpStartDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? ShiftsDetails.GetCodeByBranchID(dtpStartDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		cboShift.SelectedIndex = -1;
		((UltraToggleEditorBase)chkClose).Checked = false;
		((UltraToggleEditorBase)chkIsDirectSalesApp).Checked = false;
		dtpEndDate.Value = null;
		((Control)(object)txtActualValue).Text = "0";
		((Control)(object)txtBookValue).Text = "0";
		((Control)(object)txtDifference).Text = "0";
		((TextEditorControlBase)txtNotes).Clear();
	}

	public override bool ValidateData()
	{
		if (!FiscalYear.ChkForConfirmedFiscalYear(dtpStartDate.DateTime.ToString(GlobalVariables.DateShortFormate), IsFromServer: false))
		{
			GlobalVariables.InformationMB.Show("السنة المالية غير معتمدة", "The Fiscal Year is not confirmed..");
			return false;
		}
		if (FiscalYear.ChkForClosingFsicalPeriod(dtpStartDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false))
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
		if (cboShift.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الوردية" : "Please Select Shift");
			((TextEditorControlBase)cboShift).Focus();
			cboShift.DropDown();
			return false;
		}
		if (drMaster != null && bool.Parse(dtPOSSettingsData.Rows[0]["EnforceCloseChecks"].ToString()) && int.Parse(Main.ExecuteQuery_DataTable(" Select Count(*) AS Counter From POS_Checks Where   ShiftDetailID = " + drMaster["ShiftDetailID"].ToString() + "  And Closed=0 And Deleted=0 And BranchID = " + GlobalVariables.CurrentBranchID).Rows[0]["Counter"].ToString()) > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن غلق الوردية لوجود شيكات مفتوحه", "Cannot Close Shift Because of Open Checks");
			return false;
		}
		if (int.Parse(Main.ExecuteQuery_DataTable(" Select Count(*) AS Counter From POS_ShiftsDetails Where " + ((drMaster != null) ? (" ShiftDetailID<> " + drMaster["ShiftDetailID"].ToString() + " And ") : "") + " IsClosed=0 And IsDirectSalesApp = " + (((UltraToggleEditorBase)chkIsDirectSalesApp).Checked ? "1" : "0") + " And BranchID = " + GlobalVariables.CurrentBranchID).Rows[0]["Counter"].ToString()) > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن فتح أكثر من وردية", "Cannot open More Than One Shift");
			return false;
		}
		DataTable dataTable = Main.ExecuteQuery_DataTable(" Select Max(EndDate) AS MaxEndDate From POS_ShiftsDetails Where Deleted=0 And BranchID=" + GlobalVariables.CurrentBranchID);
		if (Adding && dataTable.Rows.Count > 0 && dataTable.Rows[0]["MaxEndDate"] != DBNull.Value && dtpStartDate.DateTime < DateTime.Parse(dataTable.Rows[0]["MaxEndDate"].ToString()))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا يمكن بدإ وردية بتاريخ أقل من أنتهاء أخر وردية" : "Cannot Start Shift With Date Less Than End Last Shift");
			return false;
		}
		if (Adding && dataTable.Rows[0]["MaxEndDate"] != DBNull.Value && dtpStartDate.DateTime > DateTime.Parse(dataTable.Rows[0]["MaxEndDate"].ToString()).AddDays(7.0))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا يمكن بدإ وردية بتاريخ أكبر من تاريخ انتهاء اخر وردية بإسبوع" : "Cannot Start Shift With Date More Than End Last Shift Week");
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("POS_ShiftsDetails", "ShiftDetailNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["ShiftDetailNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpStartDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = ShiftsDetails.GetCodeByBranchID(dtpStartDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Voucher Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByBranchID;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (!bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsClosed"].Value.ToString()) && ((UltraToggleEditorBase)chkClose).Checked)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا يمكن إغلاق هذه الوردية لوجود خزائن المستخدمين غير مغلقة" : "Cannot Close This Shift Because They Are Safes Users Not Closed");
				return false;
			}
		}
		DataTable dataTable2 = BusinessLayer.POS.Settings.ValidateCashierData(GlobalVariables.UserID, GlobalVariables.CurrentBranchID);
		if (dataTable2.Rows[0]["SubAccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد الحساب التحليلى للمستخدم  ", "Please Set user SubAccount");
			return false;
		}
		if (dataTable2.Rows[0]["CashierAccount"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب الكاشير من إعدادات البيع المباشر  ", "Please Set Cashier Account From POS Setting");
			return false;
		}
		if (int.Parse(dataTable2.Rows[0]["Relation"].ToString()) == 0)
		{
			GlobalVariables.InformationMB.Show("لايوجد ربط بين حساب الكاشير وحساب المستخدم  ", "There is No Relation Between Cashier Account And user SubAccount");
			return false;
		}
		if (Main.ExecuteQuery_DataTable(" Select BranchSafeID from POS_Settings Where BranchID= " + GlobalVariables.CurrentBranchID).Rows[0][0] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("  برجاء تحديد خزينة الفرع من إعدادات البيع المباشر ", "Please Set Branch Safe From POS Setting");
			return false;
		}
		return true;
	}

	public override void btnAddClick()
	{
		if (!CanAdd)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		if (int.Parse(Main.ExecuteQuery_DataTable(" Select Count(*) AS Counter From POS_ShiftsDetails Where IsClosed=0 And IsDirectSalesApp = " + (((UltraToggleEditorBase)chkIsDirectSalesApp).Checked ? "1" : "0") + " And BranchID = " + GlobalVariables.CurrentBranchID).Rows[0]["Counter"].ToString()) > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن فتح أكثر من وردية", "Cannot open More Than One Shift");
			return;
		}
		Adding = true;
		ClearControls();
		SetControls(NavMode: false);
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = ShiftsDetails.Insert_Update("-1", (cboShift.SelectedIndex > -1) ? ((TextEditorControlBase)cboShift).Value.ToString() : "Null", GlobalVariables.UserID, ((Control)(object)txtCode).Text, dtpStartDate.DateTime.ToString(GlobalVariables.DateLongFormate), (dtpEndDate.Value == null) ? "Null" : dtpEndDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtActualValue).Text, ((Control)(object)txtBookValue).Text, ((Control)(object)txtDifference).Text, ((UltraToggleEditorBase)chkClose).Checked ? "1" : "0", ((Control)(object)txtNotes).Text, ((UltraToggleEditorBase)chkIsDirectSalesApp).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
			RowID = num.ToString();
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
			if (bool.Parse(drMaster["IsClosed"].ToString()))
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الوردية لإنها مغلقه", "Cannot Update This Shift Because It is Closed ");
				return;
			}
			DisplayData();
			CalculateValues();
			Updating = true;
			SetControls(NavMode: false);
		}
	}

	public override void btnSaveClose_Click(object sender, EventArgs e)
	{
		if (Updating)
		{
			CalculateValues();
		}
		base.btnSaveClose_Click(sender, e);
	}

	public override void btnOKClick()
	{
		if (Updating)
		{
			CalculateValues();
		}
		((UltraGridBase)ULGData).UpdateData();
		((Control)(object)btnOK).Focus();
		if (!ValidateData())
		{
			return;
		}
		DataSaved = true;
		Main.TicketReq = false;
		if (Adding)
		{
			try
			{
				AddData();
				if (AutoPrint && RowID != "")
				{
					try
					{
						GlobalVariables.ReportDocument = new ReportDocument();
						btnPrintClick();
					}
					catch (Exception ex)
					{
						if (ex.Message == "Load report failed.")
						{
							GlobalVariables.InformationMB.Show("مسار التقارير غير سليم \r\n برجاء مراجعة مسار التقارير من إعدادات النظام", "Invalid Reports Path \r\n Please Check Reports Path from System Tools");
						}
					}
				}
			}
			catch
			{
				DataSaved = false;
				Main.TicketReq = true;
			}
			if (DataSaved)
			{
				Adding = false;
				SetControls(NavMode: true);
				if (RowID != "" && TableName != "")
				{
					UsersTransactions.DeleteByRowID(TableName, RowID);
				}
				FillData();
			}
		}
		else
		{
			try
			{
				UpdateData();
			}
			catch
			{
				DataSaved = false;
				Main.TicketReq = true;
			}
			if (DataSaved)
			{
				Updating = false;
				SetControls(NavMode: true);
				if (RowID != "" && TableName != "")
				{
					UsersTransactions.DeleteByRowID(TableName, RowID);
				}
				FillData();
			}
		}
		if (Main.TicketReq && MessageBox.Show(GlobalVariables.IsArabic ? "حدث خطأ. هل تريد إنشاء طلب دعم؟" : "Error occurred. Do you want to create a support ticket?", GlobalVariables.IsArabic ? "خطأ" : "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
		{
			Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
			frmSupportingTickets frmSupportingTickets2 = new frmSupportingTickets(isError: true, isMessage: false, isFormQst: false, base.Name, Main.strMessageDetail.Replace("'", "\""), bitmap);
			frmSupportingTickets2.StartPosition = FormStartPosition.CenterScreen;
			((Control)(object)frmSupportingTickets2.lblTitle).Text = (GlobalVariables.IsArabic ? "طلب دعم" : "Supporting Tickets");
			frmSupportingTickets2.Tag = GlobalVariables.dtAllForms.Select("Form = 'ERP.Ticketing.frmSupportingTickets'")[0];
			frmSupportingTickets2.ShowDialog();
		}
	}

	public override void UpdateData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			DateTime serverDateTimeNow = GlobalFunctions.GetServerDateTimeNow();
			int num = ShiftsDetails.Insert_Update(drMaster["ShiftDetailID"].ToString(), (cboShift.SelectedIndex > -1) ? ((TextEditorControlBase)cboShift).Value.ToString() : "Null", drMaster["User_ID"].ToString(), ((Control)(object)txtCode).Text, dtpStartDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkClose).Checked ? serverDateTimeNow.ToString(GlobalVariables.DateLongFormate) : "Null", ((Control)(object)txtActualValue).Text, ((Control)(object)txtBookValue).Text, ((Control)(object)txtDifference).Text, ((UltraToggleEditorBase)chkClose).Checked ? "1" : "0", ((Control)(object)txtNotes).Text, ((UltraToggleEditorBase)chkIsDirectSalesApp).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			if ((bool.Parse(GlobalVariables.dtSystemModules.Select(" ModuleEnName= 'Lenses'")[0]["Installed"].ToString()) || bool.Parse(GlobalVariables.dtSystemModules.Select(" ModuleEnName= 'LensesLab'")[0]["Installed"].ToString())) && ((UltraToggleEditorBase)chkClose).Checked)
			{
				ShiftsDetails.SalesJVRegenerate(num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				ShiftsDetails.ReturnJVRegenerate(num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				ShiftsDetails.RevenueJVRegenerate(num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				ShiftsDetails.ExpenseJVRegenerate(num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				ShiftsDetails.CloseJVRegenerate(num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				ShiftsDetails.CalculateVoucherCount(num.ToString(), GlobalVariables.CurrentBranchID);
				ItemsTransactions.ManagementInsertUpdateDelete();
				ItemsTransactions.RecalculateCurrentQtyOnly();
				ShiftsDetails.InsertRecipe(dtpStartDate.DateTime.ToString(GlobalVariables.DateLongFormate), serverDateTimeNow.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID);
			}
			else if (bool.Parse(GlobalVariables.dtSystemModules.Select(" ModuleEnName= 'POS'")[0]["Installed"].ToString()) && ((UltraToggleEditorBase)chkClose).Checked)
			{
				if (GlobalFunctions.GetOption("POSSalesAutoGenerateJVs"))
				{
					ShiftsDetails.SalesJVDineInRegenerate(num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					ShiftsDetails.ReturnJVDineInRegenerate(num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					ShiftsDetails.RevenueJVRegenerate(num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					ShiftsDetails.ExpenseJVRegenerate(num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					ShiftsDetails.CloseJVRegenerate(num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					ShiftsDetails.CalculateVoucherCount(num.ToString(), GlobalVariables.CurrentBranchID);
				}
				ItemsTransactions.ManagementInsertUpdateDelete();
				ItemsTransactions.RecalculateCurrentQtyOnly();
				ShiftsDetails.InsertRecipe(dtpStartDate.DateTime.ToString(GlobalVariables.DateLongFormate), serverDateTimeNow.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID);
			}
			else if (bool.Parse(GlobalVariables.dtSystemModules.Select(" ModuleEnName= 'Clinics'")[0]["Installed"].ToString()) && ((UltraToggleEditorBase)chkClose).Checked)
			{
				ShiftsDetails.SalesClinicJVRegenerate(num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				ShiftsDetails.RevenueJVRegenerate(num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				ShiftsDetails.ExpenseJVRegenerate(num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				ShiftsDetails.CloseJVRegenerate(num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				ShiftsDetails.CalculateVoucherCount(num.ToString(), GlobalVariables.CurrentBranchID);
				ItemsTransactions.ManagementInsertUpdateDelete();
				ItemsTransactions.RecalculateCurrentQtyOnly();
				ShiftsDetails.InsertRecipe(dtpStartDate.DateTime.ToString(GlobalVariables.DateLongFormate), serverDateTimeNow.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID);
			}
			Main.EndBulkTrans(FromServer: false);
		}
		catch (Exception)
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			return;
		}
		if (!DataSaved || !(GlobalVariables.POSPrinter != "") || !((UltraToggleEditorBase)chkClose).Checked)
		{
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد طباعة إقفال اليومية ؟", "Are You Sure You want to Print Shift Closing?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			return;
		}
		ReportDocument reportDocument = new ReportDocument();
		try
		{
			if (dtReports.Rows.Count > 0)
			{
				if (dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString().Trim() == "Rep_POS_ShiftsDetailsFastPrint")
				{
					FastPrint();
					return;
				}
				reportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
			}
			else
			{
				reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_POS_ShiftsDetails_A.rpt" : "Rep_POS_ShiftsDetails_E.rpt"));
			}
			GlobalFunctions.ConfigureReport(reportDocument);
			reportDocument.SetParameterValue("@ShiftDetailIDs", "," + RowID + ",");
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			reportDocument.PrintOptions.PrinterName = GlobalVariables.POSPrinter;
			try
			{
				reportDocument.PrintToPrinter(1, collated: true, 0, 10000);
			}
			catch (Exception ex2)
			{
				GlobalVariables.InformationMB.Show("تأكد من وصلات الطابعة  \n" + ex2.Message, "Check Printer Cable\n" + ex2.Message);
			}
		}
		catch
		{
			GlobalVariables.InformationMB.Show("خطأ فى مسار التقارير ", "Load Report Failed");
		}
		reportDocument.Dispose();
		GC.Collect();
	}

	public override void btnPrintClick()
	{
		if (!(RowID != ""))
		{
			return;
		}
		if (dtReports.Rows.Count > 0)
		{
			if (dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString().Trim() == "Rep_POS_ShiftsDetailsFastPrint")
			{
				FastPrint();
				return;
			}
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
		}
		else
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_POS_ShiftsDetails_A.rpt" : "Rep_POS_ShiftsDetails_E.rpt"));
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@ShiftDetailIDs", "," + RowID + ",");
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
	}

	public void FastPrint()
	{
		DataTable dataTable = Main.ExecuteQuery_DataTable(" Rep_POS_ShiftsDetails '," + RowID + ",'," + (GlobalVariables.IsArabic ? "1" : "0"));
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.Rows[0];
			Font font = new Font("Times New Roman", 10f, FontStyle.Bold);
			Font font2 = new Font("Times New Roman", 8f, FontStyle.Bold);
			Font font3 = new Font("Times New Roman", 8f, FontStyle.Regular);
			Font font4 = new Font("Times New Roman", 7f, FontStyle.Regular);
			Font font5 = new Font("Times New Roman", 9f, FontStyle.Regular);
			Font font6 = new Font("Times New Roman", 9f, FontStyle.Bold);
			Font font7 = new Font("Times New Roman", 10f, FontStyle.Bold);
			FastPrint instance = ERP.Classes.DirectPrinting.FastPrint.Instance;
			instance.PrinterSettings.PrinterName = GlobalVariables.POSPrinter;
			instance.GraphicsUnit = GraphicsUnit.Millimeter;
			instance.Margins = new Margins(0, 0, 0, 0);
			instance.OverallWidth = 70f;
			instance.AddTextCell(dataRow["ShiftDetailNo"].ToString(), font5, 0.76f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("رقم الوردية", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
			instance.AddTextCell(dataRow["ShiftName"].ToString(), font5, 0.76f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("اسم الوردية", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
			instance.AddTextCell(dataRow["BranchName"].ToString(), font5, 0.76f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("الفرع", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
			instance.AddTextCell(dataRow["UserName"].ToString(), font5, 0.76f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("المستخدم", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
			if (dataRow["EndDate"] != DBNull.Value)
			{
				instance.AddTextCell(((DateTime)dataRow["EndDate"]).ToString("dd/MM/yyyy"), font5, 0.26f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
			}
			else
			{
				instance.AddTextCell("", font5, 0.26f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
			}
			instance.AddTextCell("تاريخ النهاية", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell(((DateTime)dataRow["StartDate"]).ToString("dd/MM/yyyy"), font5, 0.26f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("تاريخ البداية", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
			instance.AddEmptyCell(1f, 1f, DrawRectangle: false);
			instance.AcceptChanges();
			instance.AddTextCell("الفرق", font2, 0.3f, 5f);
			instance.AddTextCell("القيمة الدفترية", font2, 0.35f, 5f);
			instance.AddTextCell("القيمة الفعلية", font2, 0.35f, 5f);
			instance.AcceptChanges();
			instance.AddTextCell(decimal.Parse(dataRow["Difference"].ToString(), NumberStyles.Currency).ToString("0.00"), font3, 0.3f, 4f);
			instance.AddTextCell(decimal.Parse(dataRow["BookValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font3, 0.35f, 4f);
			instance.AddTextCell(decimal.Parse(dataRow["ActualValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font3, 0.35f, 4f);
			instance.AcceptChanges();
			instance.AddEmptyCell(1f, 1f, DrawRectangle: false);
			instance.AcceptChanges();
			if (dataRow["Notes"].ToString().Trim().Length > 0)
			{
				instance.AddTextCell(dataRow["Notes"].ToString(), font5, 0.76f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
				instance.AddTextCell("ملاحظات", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
				instance.AcceptChanges();
			}
			instance.AddEmptyCell(1f, 1f, DrawRectangle: false);
			instance.AcceptChanges();
			instance.AddTextCell("الفرق", font2, 0.15f, 5f);
			instance.AddTextCell("القيمة الدفترية", font2, 0.25f, 5f);
			instance.AddTextCell("القيمة الفعلية", font2, 0.2f, 5f);
			instance.AddTextCell("المستخدم", font2, 0.4f, 5f);
			instance.AcceptChanges();
			foreach (DataRow row in dataTable.Rows)
			{
				instance.AddTextCell(decimal.Parse(row["DDifference"].ToString(), NumberStyles.Currency).ToString("0.00"), font4, 0.15f, 4f);
				instance.AddTextCell(decimal.Parse(row["DBookValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font4, 0.25f, 4f);
				instance.AddTextCell(decimal.Parse(row["DActualValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font4, 0.2f, 4f);
				instance.AddTextCell(row["DUserName"].ToString(), font4, 0.4f, 4f);
				instance.AcceptChanges();
			}
			instance.AddEmptyCell(1f, 2f, DrawRectangle: false);
			instance.AcceptChanges();
			if (int.Parse(dataRow["OpenChecks"].ToString()) > 0)
			{
				instance.AddTextCell(dataRow["OpenChecks"].ToString(), font5, 0.6f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
				instance.AddTextCell("عدد الشيكات المفتوحة", font5, 0.4f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
				instance.AcceptChanges();
				instance.AddTextCell(dataRow["OpenChecksNet"].ToString(), font5, 0.6f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
				instance.AddTextCell("اجمالي الشيكات المفتوحة", font5, 0.4f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
				instance.AcceptChanges();
				instance.AddEmptyCell(1f, 2f, DrawRectangle: false);
				instance.AcceptChanges();
			}
			instance.AddTextCell(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), font3, 1f, 5f, StringAlignment.Center, Color.Black, DrawRectangle: false);
			instance.AcceptChanges();
			try
			{
				instance.Print();
				return;
			}
			catch (Exception ex)
			{
				GlobalVariables.InformationMB.Show("تأكد من وصلات الطابعة  \n" + ex.Message, "Check Printer Cable\n" + ex.Message);
				return;
			}
		}
		GlobalVariables.InformationMB.Show("لا يوجد بيانات لطباعتها", "There Is No Data To Be Shown");
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ShiftsDetailsReport(GlobalVariables.BranchIDs);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["ShiftDetailID"].ToString();
			FillData();
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
	}

	public void CalculateValues()
	{
		dtDetails = ShiftsDetailsUsers.SelectByShiftDetailID(drMaster["ShiftDetailID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		decimal num3 = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse((((UltraGridBase)ULGData).Rows[i].Cells["ActualValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["ActualValue"].Value.ToString());
			num2 += decimal.Parse((((UltraGridBase)ULGData).Rows[i].Cells["BookValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["BookValue"].Value.ToString());
			num3 += decimal.Parse((((UltraGridBase)ULGData).Rows[i].Cells["Difference"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["Difference"].Value.ToString());
		}
		((Control)(object)txtActualValue).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtBookValue).Text = decimal.Parse(num2.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtDifference).Text = decimal.Parse(num3.ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void btnShiftSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.ShiftsSearch(IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboShift).Value = num;
		}
	}

	private void dtpStartDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = ShiftsDetails.GetCodeByBranchID(dtpStartDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void txtActualValue_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.Transactions.frmShiftsDetails));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.btnShiftSearch = new UltraButton();
		this.lblShift = new UltraLabel();
		this.cboShift = new UltraComboEditor();
		this.lblStartDate = new UltraLabel();
		this.dtpStartDate = new UltraDateTimeEditor();
		this.lblEndDate = new UltraLabel();
		this.dtpEndDate = new UltraDateTimeEditor();
		this.txtActualValue = new UltraTextEditor();
		this.lblActualValue = new UltraLabel();
		this.txtBookValue = new UltraTextEditor();
		this.lblBookValue = new UltraLabel();
		this.txtDifference = new UltraTextEditor();
		this.lblDifference = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.chkClose = new UltraCheckEditor();
		this.chkIsDirectSalesApp = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboShift).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpStartDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpEndDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtActualValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBookValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDifference).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkClose).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsDirectSalesApp).BeginInit();
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
		resources.ApplyResources(this.btnShiftSearch, "btnShiftSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance10");
		((ControlBase)this.btnShiftSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnShiftSearch).Name = "btnShiftSearch";
		((System.Windows.Forms.Control)(object)this.btnShiftSearch).Click += new System.EventHandler(btnShiftSearch_Click);
		resources.ApplyResources(this.lblShift, "lblShift");
		this.lblShift.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblShift).Name = "lblShift";
		((ControlBase)this.lblShift).WrapText = false;
		resources.ApplyResources(this.cboShift, "cboShift");
		((TextEditorControlBase)this.cboShift).AlwaysInEditMode = true;
		this.cboShift.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboShift).Name = "cboShift";
		resources.ApplyResources(this.lblStartDate, "lblStartDate");
		this.lblStartDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStartDate).Name = "lblStartDate";
		((ControlBase)this.lblStartDate).WrapText = false;
		resources.ApplyResources(this.dtpStartDate, "dtpStartDate");
		((UltraWinEditorMaskedControlBase)this.dtpStartDate).AlwaysInEditMode = true;
		this.dtpStartDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpStartDate).Name = "dtpStartDate";
		this.dtpStartDate.ValueChanged += new System.EventHandler(dtpStartDate_ValueChanged);
		resources.ApplyResources(this.lblEndDate, "lblEndDate");
		this.lblEndDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEndDate).Name = "lblEndDate";
		((ControlBase)this.lblEndDate).WrapText = false;
		resources.ApplyResources(this.dtpEndDate, "dtpEndDate");
		((UltraWinEditorMaskedControlBase)this.dtpEndDate).AlwaysInEditMode = true;
		this.dtpEndDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpEndDate).Name = "dtpEndDate";
		resources.ApplyResources(this.txtActualValue, "txtActualValue");
		((System.Windows.Forms.Control)(object)this.txtActualValue).Name = "txtActualValue";
		((System.Windows.Forms.Control)(object)this.txtActualValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtActualValue_KeyPress);
		resources.ApplyResources(this.lblActualValue, "lblActualValue");
		this.lblActualValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblActualValue).Name = "lblActualValue";
		((ControlBase)this.lblActualValue).WrapText = false;
		resources.ApplyResources(this.txtBookValue, "txtBookValue");
		((System.Windows.Forms.Control)(object)this.txtBookValue).Name = "txtBookValue";
		resources.ApplyResources(this.lblBookValue, "lblBookValue");
		this.lblBookValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBookValue).Name = "lblBookValue";
		((ControlBase)this.lblBookValue).WrapText = false;
		resources.ApplyResources(this.txtDifference, "txtDifference");
		((System.Windows.Forms.Control)(object)this.txtDifference).Name = "txtDifference";
		resources.ApplyResources(this.lblDifference, "lblDifference");
		this.lblDifference.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDifference).Name = "lblDifference";
		((ControlBase)this.lblDifference).WrapText = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.chkClose, "chkClose");
		((System.Windows.Forms.Control)(object)this.chkClose).Name = "chkClose";
		resources.ApplyResources(this.chkIsDirectSalesApp, "chkIsDirectSalesApp");
		((System.Windows.Forms.Control)(object)this.chkIsDirectSalesApp).Name = "chkIsDirectSalesApp";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsDirectSalesApp);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDifference);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDifference);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBookValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBookValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtActualValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblActualValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEndDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpEndDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStartDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpStartDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnShiftSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblShift);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboShift);
		base.Name = "frmShiftsDetails";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboShift, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblShift, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnShiftSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpStartDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStartDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpEndDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEndDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblActualValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtActualValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBookValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBookValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDifference, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDifference, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsDirectSalesApp, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboShift).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpStartDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpEndDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtActualValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBookValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDifference).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkClose).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsDirectSalesApp).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
