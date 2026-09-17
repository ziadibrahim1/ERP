using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Photos;
using BusinessLayer.POS;
using BusinessLayer.SafesAndBanks;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Photos.Transactions;

public class frmPHOInvoicesPayments : frmHeaderDetails
{
	private DataTable dtInvoices;

	private DataTable dtsafes;

	private DataTable dtPOSDefaultData;

	private int InvoiceID = 0;

	private IContainer components = null;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	public UltraButton btnInvoiceNoSearch;

	private UltraComboEditor cboInvoiceNo;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblTotal;

	private UltraTextEditor txtTotal;

	private UltraLabel lblInvoiceNo;

	private UltraLabel lblSafe;

	private UltraComboEditor cboSafe;

	public frmPHOInvoicesPayments()
	{
		InitializeComponent();
		TableName = "PHO_InvoicesPayments";
		IDCol = "InvoicePaymentID";
		NoCol = "InvoicePaymentNo";
		DateCol = "PaymentDate";
	}

	public frmPHOInvoicesPayments(int invoiceID)
		: this()
	{
		InvoiceID = invoiceID;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtPOSDefaultData = BusinessLayer.POS.Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtInvoices = Invoices.FillCombo(GlobalVariables.BranchIDs, "-1");
		GlobalFunctions.FillCombo(cboInvoiceNo, dtInvoices, "InvoiceID", "InvoiceNo");
		dtsafes = Safes.FillComboBySafeIDs(GlobalVariables.SafeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSafe, dtsafes, "SafeID", "SafeName");
	}

	public override void FillData()
	{
		if (InvoiceID > 0)
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
			DataTable dataTable = InvoicesPayments.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
			((Control)(object)txtCode).Text = drMaster["InvoicePaymentNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["PaymentDate"];
			((TextEditorControlBase)cboInvoiceNo).Value = drMaster["InvoiceID"];
			((TextEditorControlBase)cboSafe).Value = drMaster["SafeID"];
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtTotal).Text = decimal.Parse(drMaster["TotalAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
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
		((EditorButtonControlBase)cboInvoiceNo).ReadOnly = NavMode || InvoiceID > 0;
		((EditorButtonControlBase)cboSafe).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTotal).ReadOnly = NavMode;
		((Control)(object)btnInvoiceNoSearch).Visible = !NavMode && InvoiceID == 0;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? InvoicesPayments.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		cboInvoiceNo.SelectedIndex = -1;
		if (InvoiceID > 0)
		{
			((TextEditorControlBase)cboInvoiceNo).Value = InvoiceID.ToString();
		}
		cboSafe.SelectedIndex = ((dtsafes.Rows.Count != 1) ? (-1) : 0);
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
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم  الأذن" : "Please Enter The Voucher Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (cboInvoiceNo.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار رقم الفاتورة" : "Please Select Invoice No");
			((TextEditorControlBase)cboInvoiceNo).Focus();
			return false;
		}
		if (cboSafe.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الخزينة" : "Please Select Safe");
			((TextEditorControlBase)cboSafe).Focus();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("PHO_InvoicesPayments", "InvoicePaymentNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["InvoicePaymentNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = InvoicesPayments.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
			if (((Control)(object)txtTotal).Text == "" || decimal.Parse(((Control)(object)txtTotal).Text) <= 0m)
			{
				GlobalVariables.InformationMB.Show("إجمالى السعر لابد ان يكون اكبر من الصفر", "Total Price Must Be Greater Than Zero");
				((TextEditorControlBase)txtTotal).Focus();
				return false;
			}
			if (dtPOSDefaultData.Rows[0]["DefaultSubAccountID"] == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء تحديد حساب العميل الافتراضى من اعدادات البيع  ", "Please Select Default Client ID From Sales Settings ");
				return false;
			}
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		int num;
		try
		{
			num = InvoicesPayments.Insert_Update("-1", ((Control)(object)txtCode).Text, (cboInvoiceNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboInvoiceNo).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtTotal).Text, ((Control)(object)txtNotes).Text, (cboSafe.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSafe).Value.ToString(), GlobalVariables.UserID, "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			InvoicesPayments.GenerateJvs("," + num + ",", GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
			return;
		}
		if (!DataSaved)
		{
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد طباعة سداد الفاتورة ؟", "Are You Sure You want to Print This Invoice Payment?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			return;
		}
		ReportDocument reportDocument = new ReportDocument();
		try
		{
			reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_PHO_InvoicesPayments_A.rpt" : "Rep_PHO_InvoicesPayments_E.rpt"));
			GlobalFunctions.ConfigureReport(reportDocument);
			reportDocument.SetParameterValue("@InvoicePaymentIDs", "," + num + ",");
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
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
			InvoicesPayments.GenerateJvs("," + InvoicesPayments.Insert_Update(drMaster["InvoicePaymentID"].ToString(), ((Control)(object)txtCode).Text, (cboInvoiceNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboInvoiceNo).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtTotal).Text, ((Control)(object)txtNotes).Text, (cboSafe.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSafe).Value.ToString(), (drMaster["User_ID"] == DBNull.Value) ? "Null" : drMaster["User_ID"].ToString(), (drMaster["JVID"] == DBNull.Value) ? "Null" : drMaster["JVID"].ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID) + ",", GlobalVariables.UserID);
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
			JV.DeleteVirtual(drMaster["JVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			InvoicesPayments.DeleteVirtual(drMaster["InvoicePaymentID"].ToString(), GlobalVariables.UserID);
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
		dtInvoices = Invoices.FillCombo(GlobalVariables.BranchIDs, "-1");
		GlobalFunctions.FillCombo(cboInvoiceNo, dtInvoices, "InvoiceID", "InvoiceNo");
		dtsafes = Safes.FillComboBySafeIDs(GlobalVariables.SafeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSafe, dtsafes, "SafeID", "SafeName");
	}

	public override void btnPrintClick()
	{
		if (RowID != "")
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_PHO_InvoicesPayments_A.rpt" : "Rep_PHO_InvoicesPayments_E.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@InvoicePaymentIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			GlobalVariables.ReportDocument.PrintOptions.PrinterName = GlobalVariables.POSPrinter;
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.PHOInvoicesPaymentsReport(0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["InvoicePaymentID"].ToString();
			FillData();
		}
	}

	private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = InvoicesPayments.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void btnInvoiceNoSearch_Click(object sender, EventArgs e)
	{
		((TextEditorControlBase)cboInvoiceNo).Value = SearchFunctions.PHOInvoicesSearch(GlobalVariables.BranchIDs, -1, 0, -1);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Photos.Transactions.frmPHOInvoicesPayments));
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
		this.btnInvoiceNoSearch = new UltraButton();
		this.cboInvoiceNo = new UltraComboEditor();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblTotal = new UltraLabel();
		this.txtTotal = new UltraTextEditor();
		this.lblInvoiceNo = new UltraLabel();
		this.lblSafe = new UltraLabel();
		this.cboSafe = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboInvoiceNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotal).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSafe).BeginInit();
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
		((System.Windows.Forms.Control)(object)this.dtpDate).TabStop = false;
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.btnInvoiceNoSearch, "btnInvoiceNoSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance10");
		((ControlBase)this.btnInvoiceNoSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnInvoiceNoSearch).Name = "btnInvoiceNoSearch";
		((System.Windows.Forms.Control)(object)this.btnInvoiceNoSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnInvoiceNoSearch).Click += new System.EventHandler(btnInvoiceNoSearch_Click);
		resources.ApplyResources(this.cboInvoiceNo, "cboInvoiceNo");
		this.cboInvoiceNo.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboInvoiceNo).Name = "cboInvoiceNo";
		((System.Windows.Forms.Control)(object)this.cboInvoiceNo).TabStop = false;
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
		((System.Windows.Forms.Control)(object)this.txtTotal).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTotal_KeyPress);
		resources.ApplyResources(this.lblInvoiceNo, "lblInvoiceNo");
		this.lblInvoiceNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInvoiceNo).Name = "lblInvoiceNo";
		((ControlBase)this.lblInvoiceNo).WrapText = false;
		resources.ApplyResources(this.lblSafe, "lblSafe");
		this.lblSafe.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSafe).Name = "lblSafe";
		((ControlBase)this.lblSafe).WrapText = false;
		resources.ApplyResources(this.cboSafe, "cboSafe");
		((TextEditorControlBase)this.cboSafe).AlwaysInEditMode = true;
		this.cboSafe.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSafe).Name = "cboSafe";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInvoiceNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnInvoiceNoSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboInvoiceNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSafe);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSafe);
		base.Name = "frmPHOInvoicesPayments";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSafe, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSafe, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboInvoiceNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnInvoiceNoSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInvoiceNo, 0);
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
		((System.ComponentModel.ISupportInitialize)this.cboInvoiceNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotal).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSafe).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
