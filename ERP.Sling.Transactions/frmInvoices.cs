using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Privilege;
using BusinessLayer.Sling;
using BusinessLayer.StockControl;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Sling.Transactions;

public class frmInvoices : frmHeaderDetails
{
	private DataTable dtQuotations;

	private DataTable dtReports;

	private DataTable dtUnits;

	private DataTable dtQuotationDetails;

	private DataTable dtClients;

	private DataTable dtCurrency;

	private ValueList vlUnits = new ValueList();

	private ValueList vlQuotationDetails = new ValueList();

	private int DiscountUserID = 0;

	private decimal UserSalesDiscount = default(decimal);

	private IContainer components = null;

	private UltraLabel lblDiscRatio;

	private UltraTextEditor txtDiscRatio;

	private UltraLabel lblDiscValue;

	private UltraTextEditor txtDiscValue;

	private UltraLabel lblGrossValue;

	private UltraTextEditor txtGrossValue;

	private UltraLabel lblNetPrice;

	private UltraTextEditor txtNetprice;

	private UltraLabel lblClient;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraComboEditor cboClient;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraTextEditor txtTotalQty;

	private UltraLabel ultraLabel1;

	private UltraComboEditor cboQuotations;

	private UltraLabel lblQuotation;

	public UltraButton btnQuotationsSearch;

	private UltraTextEditor txtExchangeRate;

	private UltraLabel lblExchangeRate;

	private UltraLabel lblCurrency;

	private UltraComboEditor cboCurrency;

	private UltraComboEditor cboConsignedTo;

	private UltraLabel lblConsignedTo;

	public frmInvoices()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
		TableName = "SLN_Invoices";
		IDCol = "InvoiceID";
		NoCol = "InvoiceNo";
		DateCol = "InvoiceDate";
	}

	public frmInvoices(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		UserSalesDiscount = Users.SelectSalesDiscount(GlobalVariables.UserID, IsFromServer: false);
		((Control)(object)lblQuotation).Text = GlobalFunctions.GetFormName("Sling", "Transactions", "frmSLNQuotations");
		dtQuotations = Quotations.FillCombo(GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboQuotations, dtQuotations, "QuotationID", "QuotationNo");
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		GlobalFunctions.FillCombo(cboConsignedTo, dtClients, "SubAccountID", "SubAccountName");
		dtQuotationDetails = QuotationsDetails.FillComboByQuotationID("-1");
		vlQuotationDetails.ValueListItems.Clear();
		for (int i = 0; i < dtQuotationDetails.Rows.Count; i++)
		{
			vlQuotationDetails.ValueListItems.Add(dtQuotationDetails.Rows[i]["QuotationDetailID"], dtQuotationDetails.Rows[i]["Notes"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int j = 0; j < dtUnits.Rows.Count; j++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[j]["UnitID"], dtUnits.Rows[j]["UnitName"].ToString());
		}
		FillCurrencyDropDown();
		dtDetails = InvoicesDetails.SelectByInvoiceID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	private void FillCurrencyDropDown()
	{
		dtCurrency = Currency.FillCurrencyByDate(GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", "CurrencyName");
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = Invoices.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Expected O, but got Unknown
		base.DisplayData();
		if (drMaster != null)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["InvoiceNo"].ToString();
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			dtpDate.Value = (DateTime)drMaster["InvoiceDate"];
			dtpDate.ValueChanged += dtpDate_ValueChanged;
			((TextEditorControlBase)cboQuotations).ValueChanged -= cboQuotations_ValueChanged;
			((TextEditorControlBase)cboQuotations).Value = drMaster["QuotationID"];
			((TextEditorControlBase)cboQuotations).ValueChanged += cboQuotations_ValueChanged;
			((TextEditorControlBase)cboClient).Value = drMaster["ClientSubAccountID"];
			((TextEditorControlBase)cboConsignedTo).Value = drMaster["ConsignedToSubAccountID"];
			((TextEditorControlBase)cboCurrency).Value = drMaster["CurrencyID"];
			((Control)(object)txtGrossValue).Text = decimal.Parse(drMaster["GrossValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscValue).ValueChanged -= txtDiscValue_ValueChanged;
			((Control)(object)txtDiscValue).Text = decimal.Parse(drMaster["DiscountValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscValue).ValueChanged += txtDiscValue_ValueChanged;
			((TextEditorControlBase)txtDiscRatio).ValueChanged -= txtDiscRatio_ValueChanged;
			((Control)(object)txtDiscRatio).Text = decimal.Parse(drMaster["DiscountRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscRatio).ValueChanged += txtDiscRatio_ValueChanged;
			((Control)(object)txtExchangeRate).Text = decimal.Parse(drMaster["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNetprice).Text = decimal.Parse(drMaster["NetPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = InvoicesDetails.SelectByInvoiceID(drMaster["InvoiceID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
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
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		else
		{
			ClearControls();
		}
		((TextEditorControlBase)txtCode).Focus();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "اجمالي السعر" : "Total Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotationDetailID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotationDetailID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotationDetailID"].ValueList = (IValueList)(object)vlQuotationDetails;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotationDetailID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحدة" : "Unit Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "كمية" : "Qty");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AllowedQty"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AllowedQty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية المتاحه" : "Allowed Qty");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AllowedQty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الوصف" : "Description");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboConsignedTo).ReadOnly = NavMode;
		((EditorButtonControlBase)cboClient).ReadOnly = true;
		((EditorButtonControlBase)cboQuotations).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCurrency).ReadOnly = true;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtExchangeRate).ReadOnly = true;
		((Control)(object)btnQuotationsSearch).Visible = !NavMode;
		((EditorButtonControlBase)txtDiscValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscRatio).ReadOnly = NavMode;
		DiscountUserID = 0;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		DiscountUserID = 0;
		((TextEditorControlBase)txtCode).Clear();
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		dtpDate.ValueChanged += dtpDate_ValueChanged;
		((Control)(object)txtCode).Text = (Adding ? Invoices.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)cboQuotations).ValueChanged -= cboQuotations_ValueChanged;
		cboCurrency.SelectedIndex = -1;
		cboClient.SelectedIndex = -1;
		cboConsignedTo.SelectedIndex = -1;
		cboQuotations.SelectedIndex = -1;
		((TextEditorControlBase)cboQuotations).ValueChanged += cboQuotations_ValueChanged;
		((TextEditorControlBase)txtNotes).Clear();
		((Control)(object)txtGrossValue).Text = "0";
		((TextEditorControlBase)txtDiscValue).ValueChanged -= txtDiscValue_ValueChanged;
		((Control)(object)txtDiscValue).Text = "0";
		((TextEditorControlBase)txtDiscValue).ValueChanged += txtDiscValue_ValueChanged;
		((TextEditorControlBase)txtDiscRatio).ValueChanged -= txtDiscRatio_ValueChanged;
		((Control)(object)txtDiscRatio).Text = "0";
		((TextEditorControlBase)txtDiscRatio).ValueChanged += txtDiscRatio_ValueChanged;
		((Control)(object)txtNetprice).Text = "0";
		((Control)(object)txtTotalQty).Text = "0";
	}

	public override void CallButtons(KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1 && ((Control)(object)btnAdd).Enabled && ((Control)(object)btnAdd).Visible)
		{
			SendKeys.Send("{tab}");
			btnAddClick();
		}
		else if (e.KeyCode == Keys.F2 && ((Control)(object)btnUpdate).Enabled && ((Control)(object)btnUpdate).Visible)
		{
			btnUpdateClick();
		}
		else if (e.KeyCode == Keys.F3 && ((Control)(object)btnDelete).Enabled && ((Control)(object)btnDelete).Visible)
		{
			btnDeleteClick();
		}
		else if (e.KeyCode == Keys.F4 && ((Control)(object)btnPrint).Enabled && ((Control)(object)btnPrint).Visible)
		{
			btnPrint_Click(null, null);
		}
		else if (e.KeyCode == Keys.F5 && ((Control)(object)btnRefreshData).Enabled && ((Control)(object)btnRefreshData).Visible)
		{
			btnRefreshDataClick();
		}
		if (!Adding && !Updating)
		{
			if (e.KeyCode == Keys.F8 && ((Control)(object)btnSearch).Enabled && ((Control)(object)btnSearch).Visible)
			{
				btnSearch_Click(null, null);
			}
			else if (e.KeyValue == 39 && ((Control)(object)btnNext).Enabled && ((Control)(object)btnNext).Visible)
			{
				NextData();
			}
			else if (e.KeyValue == 37 && ((Control)(object)btnPriveous).Enabled && ((Control)(object)btnPriveous).Visible)
			{
				PriveousData();
			}
			else if (e.KeyCode == Keys.F7 && ((Control)(object)btnCopyTo).Enabled && ((Control)(object)btnCopyTo).Visible)
			{
				btnCopyToClick();
			}
		}
	}

	public override void btnPrintClick()
	{
		if (RowID != "")
		{
			ReportDocument reportDocument = new ReportDocument();
			string val = "";
			if (dtReports.Rows.Count > 0)
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
				val = dtReports.Rows[0]["isoCode"].ToString();
			}
			else
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_SLN_Invoices_A.rpt" : "Rep_SLN_Invoices_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@InvoiceIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.SLNInvoicesReport("-1", 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["InvoiceID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtQuotations = Quotations.FillCombo(GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboQuotations, dtQuotations, "QuotationID", "QuotationNo");
		dtQuotationDetails = QuotationsDetails.FillComboByQuotationID("-1");
		vlQuotationDetails.ValueListItems.Clear();
		for (int i = 0; i < dtQuotationDetails.Rows.Count; i++)
		{
			vlQuotationDetails.ValueListItems.Add(dtQuotationDetails.Rows[i]["QuotationDetailID"], dtQuotationDetails.Rows[i]["Notes"].ToString());
		}
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		GlobalFunctions.FillCombo(cboConsignedTo, dtClients, "SubAccountID", "SubAccountName");
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int j = 0; j < dtUnits.Rows.Count; j++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[j]["UnitID"], dtUnits.Rows[j]["UnitName"].ToString());
		}
		FillCurrencyDropDown();
	}

	public override bool ValidateData()
	{
		if (dtpDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال تاريخ الأذن", "Please Enter The Voucher Date");
			((Control)(object)dtpDate).Focus();
			dtpDate.DropDown();
			return false;
		}
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
		if (cboClient.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار العميل", "Please Select Client");
			((TextEditorControlBase)cboClient).Focus();
			cboClient.DropDown();
			return false;
		}
		if (cboConsignedTo.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار المرسل اليه", "Please Select Consigned To");
			((TextEditorControlBase)cboConsignedTo).Focus();
			cboConsignedTo.DropDown();
			return false;
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='SalesDiscountAccount' ")[0]["AccountID"] == DBNull.Value && (decimal.Parse(((Control)(object)txtDiscValue).Text) > 0m || decimal.Parse(((Control)(object)txtDiscValue).Text) > 0m))
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب الخصم المسموح به من حسابات النظام  ", "Please Select Sales Discount Account From SystemAccounts ");
			return false;
		}
		((UltraGridBase)ULGData).UpdateData();
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا الإذن", "Please insert details for this Voucher");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["QuotationDetailID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الصنف  ", "Please Enter Item Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["QuotationDetailID"];
				((UltraGridBase)ULGData).Rows[i].Cells["QuotationDetailID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Qty"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال سعر الوحدة  ", "Please Enter Unit Price ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إدخال إجمالى السعر  ", "Please Enter Total price");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) > decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["AllowedQty"].Value.ToString()))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("الكمية اكبر من الكمية المتاحة", "The Quantity Is Greater Than The Allowed Qty");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Qty"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("SLN_Invoices", "InvoiceNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["InvoiceNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = Invoices.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Voucher Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByBranchID;
		}
		return true;
	}

	public override void AddData()
	{
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Expected O, but got Unknown
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = Invoices.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboQuotations).Value.ToString(), ((TextEditorControlBase)cboClient).Value.ToString(), (cboConsignedTo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboConsignedTo).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "") ? "0" : ((Control)(object)txtExchangeRate).Text, (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscValue).Text == "") ? "0" : ((Control)(object)txtDiscValue).Text, (((Control)(object)txtDiscRatio).Text == "") ? "0" : ((Control)(object)txtDiscRatio).Text, (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, ((Control)(object)txtNotes).Text, "Null", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).UpdateData();
			dtDetails.AcceptChanges();
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["InvoiceID"].Value = num.ToString();
				((UltraGridBase)ULGData).Rows[i].Cells["InvoiceDetailID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			InvoicesDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			Invoices.GenerateJvs("," + num + ",", GlobalVariables.UserID);
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

	public override void UpdateData()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Expected O, but got Unknown
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).UpdateData();
			CalculateGoss();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			int num = Invoices.Insert_Update(drMaster["InvoiceID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboQuotations).Value.ToString(), ((TextEditorControlBase)cboClient).Value.ToString(), (cboConsignedTo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboConsignedTo).Value.ToString(), ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "") ? "0" : ((Control)(object)txtExchangeRate).Text, (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscValue).Text == "") ? "0" : ((Control)(object)txtDiscValue).Text, (((Control)(object)txtDiscRatio).Text == "") ? "0" : ((Control)(object)txtDiscRatio).Text, (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, ((Control)(object)txtNotes).Text, (drMaster["SalesJVID"] == DBNull.Value) ? "Null" : drMaster["SalesJVID"].ToString(), bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).UpdateData();
			dtDetails.AcceptChanges();
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["InvoiceID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["InvoiceDetailID"].Value.ToString() + ",";
			}
			((UltraGridBase)ULGData).UpdateData();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			Main.DeleteForUpdate("SLN_InvoicesDetails", "InvoiceID", drMaster["InvoiceID"].ToString(), "InvoiceDetailID", text);
			InvoicesDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			Invoices.GenerateJvs("," + num + ",", GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
			ItemsTransactions.ManageInThread();
		}
		catch (Exception)
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
			JVDetails.DeleteVirtualByJVID(drMaster["SalesJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			JV.DeleteVirtual(drMaster["SalesJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			Invoices.DeleteVirtual(drMaster["InvoiceID"].ToString(), GlobalVariables.UserID);
			InvoicesDetails.DeleteVirtualByInvoiceID(drMaster["InvoiceID"].ToString(), GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void CalculateGoss()
	{
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num2 += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
			((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString());
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
		}
		CalcTotalQty();
		((Control)(object)txtGrossValue).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		CalculateNetTotals();
	}

	public void CalcTotalQty()
	{
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count <= 0)
		{
			return;
		}
		((Control)(object)txtTotalQty).Text = "0";
		((UltraGridBase)ULGData).UpdateData();
		DataView dataView = new DataView((DataTable)((UltraGridBase)ULGData).DataSource);
		dataView.RowFilter = " UnitID is not null";
		DataTable dataTable = dataView.ToTable();
		if (dataTable.Rows.Count > 0 && dataTable.Select(" UnitID<> " + dataTable.Rows[0]["UnitID"].ToString()).Length == 0)
		{
			object obj = dataTable.Compute(" Sum(Qty) ", "");
			if (obj != DBNull.Value)
			{
				((Control)(object)txtTotalQty).Text = decimal.Parse(obj.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
		}
	}

	private void CalculateNetTotals()
	{
		((TextEditorControlBase)txtDiscValue).ValueChanged -= txtDiscValue_ValueChanged;
		((Control)(object)txtDiscValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscRatio).Text == "" || ((Control)(object)txtDiscRatio).Text == ".") ? "0" : ((Control)(object)txtDiscRatio).Text) / 100m * decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtDiscValue).ValueChanged += txtDiscValue_ValueChanged;
		((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscValue).Text == "" || ((Control)(object)txtDiscValue).Text == ".") ? "0" : ((Control)(object)txtDiscValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (!(((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty") && !(((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice"))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyValue == 38 && (ULGData.ActiveCell.ValueList != null || ULGData.ActiveCell.Column.ValueList != null))
		{
			ULGData.PerformAction((UltraGridAction)19);
			ULGData.PerformAction((UltraGridAction)24);
			e.Handled = true;
		}
		else if (e.KeyValue == 40 && (ULGData.ActiveCell.ValueList != null || ULGData.ActiveCell.Column.ValueList != null))
		{
			ULGData.PerformAction((UltraGridAction)20);
			ULGData.PerformAction((UltraGridAction)24);
			e.Handled = true;
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue"))
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Expected O, but got Unknown
		if (ULGData.ActiveCell != null)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice") && ULGData.ActiveCell.Value == DBNull.Value)
			{
				ULGData.ActiveCell.Value = 0;
			}
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice")
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				CalculateGoss();
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > 0m)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value.ToString()) / decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				CalculateGoss();
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		CalculateGoss();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	public void OpenChangeDiscountForm()
	{
		frmChangeDiscount frmChangeDiscount2 = new frmChangeDiscount(decimal.Parse(((Control)(object)txtGrossValue).Text), decimal.Parse(((Control)(object)txtDiscValue).Text), decimal.Parse(((Control)(object)txtDiscRatio).Text));
		frmChangeDiscount2.WindowState = FormWindowState.Normal;
		frmChangeDiscount2.ShowDialog();
		if (frmChangeDiscount2.Cancel)
		{
			((Control)(object)txtDiscRatio).Text = decimal.Parse((cboClient.SelectedIndex > -1 && decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) > UserSalesDiscount) ? dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString() : UserSalesDiscount.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscRatio).Text == "" || ((Control)(object)txtDiscRatio).Text == ".") ? "0" : ((Control)(object)txtDiscRatio).Text) / 100m * decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			return;
		}
		((Control)(object)txtDiscRatio).Text = string.Concat(frmChangeDiscount2.DiscountRatio);
		((Control)(object)txtDiscValue).Text = string.Concat(frmChangeDiscount2.DiscountValue);
		DiscountUserID = frmChangeDiscount2.UserID;
		frmChangeDiscount2.Close();
	}

	private void btnQuotationsSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.SLNQuotationsSearch(-1, 0, 0);
		if (num != 0)
		{
			((TextEditorControlBase)cboQuotations).Value = num;
		}
	}

	private void cboQuotations_ValueChanged(object sender, EventArgs e)
	{
		((TextEditorControlBase)cboQuotations).ValueChanged -= cboQuotations_ValueChanged;
		if (cboQuotations.SelectedIndex > -1)
		{
			DataRow dataRow = dtQuotations.Select("QuotationID = " + ((TextEditorControlBase)cboQuotations).Value.ToString())[0];
			((TextEditorControlBase)cboClient).Value = dataRow["ClientSubAccountID"];
			((TextEditorControlBase)cboConsignedTo).Value = dataRow["ClientSubAccountID"];
			((TextEditorControlBase)cboCurrency).Value = dataRow["CurrencyID"];
			((Control)(object)txtExchangeRate).Text = decimal.Parse(dataRow["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscRatio).ValueChanged -= txtDiscRatio_ValueChanged;
			((Control)(object)txtDiscRatio).Text = decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscRatio).ValueChanged += txtDiscRatio_ValueChanged;
			dtDetails = InvoicesDetails.FillByQuotationID(((TextEditorControlBase)cboQuotations).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			CalculateGoss();
			CalculateNetTotals();
		}
		InitGrid();
		((TextEditorControlBase)cboQuotations).ValueChanged += cboQuotations_ValueChanged;
	}

	private void cboClient_ValueChanged(object sender, EventArgs e)
	{
		if (cboClient.SelectedIndex > -1)
		{
			((TextEditorControlBase)txtDiscRatio).ValueChanged -= txtDiscRatio_ValueChanged;
			((Control)(object)txtDiscRatio).Text = decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscRatio).ValueChanged += txtDiscRatio_ValueChanged;
			CalculateNetTotals();
		}
	}

	private void txtDiscValue_ValueChanged(object sender, EventArgs e)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Expected O, but got Unknown
		if (Adding || Updating)
		{
			((TextEditorControlBase)txtDiscRatio).ValueChanged -= txtDiscRatio_ValueChanged;
			((TextEditorControlBase)txtDiscValue).ValueChanged -= txtDiscValue_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscRatio).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscValue).Text == "" || ((Control)(object)txtDiscValue).Text == "0" || ((Control)(object)txtDiscValue).Text == ".") ? "0" : ((Control)(object)txtDiscValue).Text) * 100m / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if ((cboClient.SelectedIndex > -1 && decimal.Parse((((Control)(object)txtDiscRatio).Text == "") ? "0" : ((Control)(object)txtDiscRatio).Text) > decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) && decimal.Parse((((Control)(object)txtDiscRatio).Text == "") ? "0" : ((Control)(object)txtDiscRatio).Text) > UserSalesDiscount) || (cboClient.SelectedIndex == -1 && decimal.Parse((((Control)(object)txtDiscRatio).Text == "") ? "0" : ((Control)(object)txtDiscRatio).Text) > UserSalesDiscount))
			{
				OpenChangeDiscountForm();
			}
			((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscValue).Text == "" || ((Control)(object)txtDiscValue).Text == ".") ? "0" : ((Control)(object)txtDiscValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscRatio).ValueChanged += txtDiscRatio_ValueChanged;
			((TextEditorControlBase)txtDiscValue).ValueChanged += txtDiscValue_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
	}

	private void txtDiscRatio_ValueChanged(object sender, EventArgs e)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Expected O, but got Unknown
		if (Adding || Updating)
		{
			((TextEditorControlBase)txtDiscValue).ValueChanged -= txtDiscValue_ValueChanged;
			((TextEditorControlBase)txtDiscRatio).ValueChanged -= txtDiscRatio_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscRatio).Text == "" || ((Control)(object)txtDiscRatio).Text == ".") ? "0" : ((Control)(object)txtDiscRatio).Text) / 100m * decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text), 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if ((cboClient.SelectedIndex > -1 && decimal.Parse((((Control)(object)txtDiscRatio).Text == "" || ((Control)(object)txtDiscRatio).Text == ".") ? "0" : ((Control)(object)txtDiscRatio).Text) > decimal.Parse(dtClients.Select(" SubAccountID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) && decimal.Parse((((Control)(object)txtDiscRatio).Text == "" || ((Control)(object)txtDiscRatio).Text == ".") ? "0" : ((Control)(object)txtDiscRatio).Text) > UserSalesDiscount) || (cboClient.SelectedIndex == -1 && decimal.Parse((((Control)(object)txtDiscRatio).Text == "" || ((Control)(object)txtDiscRatio).Text == ".") ? "0" : ((Control)(object)txtDiscRatio).Text) > UserSalesDiscount))
			{
				OpenChangeDiscountForm();
			}
			((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscValue).Text == "" || ((Control)(object)txtDiscValue).Text == ".") ? "0" : ((Control)(object)txtDiscValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscRatio).ValueChanged += txtDiscRatio_ValueChanged;
			((TextEditorControlBase)txtDiscValue).ValueChanged += txtDiscValue_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
	}

	private void textBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
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
			((Control)(object)txtCode).Text = Invoices.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Expected O, but got Unknown
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Expected O, but got Unknown
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Expected O, but got Unknown
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Expected O, but got Unknown
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Sling.Transactions.frmInvoices));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.lblDiscRatio = new UltraLabel();
		this.txtDiscRatio = new UltraTextEditor();
		this.lblDiscValue = new UltraLabel();
		this.txtDiscValue = new UltraTextEditor();
		this.lblGrossValue = new UltraLabel();
		this.txtGrossValue = new UltraTextEditor();
		this.lblNetPrice = new UltraLabel();
		this.txtNetprice = new UltraTextEditor();
		this.lblClient = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.cboClient = new UltraComboEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.txtTotalQty = new UltraTextEditor();
		this.ultraLabel1 = new UltraLabel();
		this.cboQuotations = new UltraComboEditor();
		this.lblQuotation = new UltraLabel();
		this.btnQuotationsSearch = new UltraButton();
		this.txtExchangeRate = new UltraTextEditor();
		this.lblExchangeRate = new UltraLabel();
		this.lblCurrency = new UltraLabel();
		this.cboCurrency = new UltraComboEditor();
		this.cboConsignedTo = new UltraComboEditor();
		this.lblConsignedTo = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboQuotations).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboConsignedTo).BeginInit();
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
		base.ULGData.AfterEnterEditMode += new System.EventHandler(SelectFullRow);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)base.txtCode).TabStop = false;
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
		resources.ApplyResources(this.lblDiscRatio, "lblDiscRatio");
		this.lblDiscRatio.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscRatio).Name = "lblDiscRatio";
		((ControlBase)this.lblDiscRatio).WrapText = false;
		resources.ApplyResources(this.txtDiscRatio, "txtDiscRatio");
		((System.Windows.Forms.Control)(object)this.txtDiscRatio).Name = "txtDiscRatio";
		((TextEditorControlBase)this.txtDiscRatio).ValueChanged += new System.EventHandler(txtDiscRatio_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscRatio).Enter += new System.EventHandler(textBox_Enter);
		((System.Windows.Forms.Control)(object)this.txtDiscRatio).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblDiscValue, "lblDiscValue");
		this.lblDiscValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscValue).Name = "lblDiscValue";
		((ControlBase)this.lblDiscValue).WrapText = false;
		resources.ApplyResources(this.txtDiscValue, "txtDiscValue");
		((System.Windows.Forms.Control)(object)this.txtDiscValue).Name = "txtDiscValue";
		((TextEditorControlBase)this.txtDiscValue).ValueChanged += new System.EventHandler(txtDiscValue_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscValue).Enter += new System.EventHandler(textBox_Enter);
		((System.Windows.Forms.Control)(object)this.txtDiscValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblGrossValue, "lblGrossValue");
		this.lblGrossValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGrossValue).Name = "lblGrossValue";
		((ControlBase)this.lblGrossValue).WrapText = false;
		resources.ApplyResources(this.txtGrossValue, "txtGrossValue");
		((System.Windows.Forms.Control)(object)this.txtGrossValue).Name = "txtGrossValue";
		((EditorButtonControlBase)this.txtGrossValue).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtGrossValue).TabStop = false;
		resources.ApplyResources(this.lblNetPrice, "lblNetPrice");
		this.lblNetPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNetPrice).Name = "lblNetPrice";
		((ControlBase)this.lblNetPrice).WrapText = false;
		resources.ApplyResources(this.txtNetprice, "txtNetprice");
		((System.Windows.Forms.Control)(object)this.txtNetprice).Name = "txtNetprice";
		((EditorButtonControlBase)this.txtNetprice).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtNetprice).TabStop = false;
		resources.ApplyResources(this.lblClient, "lblClient");
		this.lblClient.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClient).Name = "lblClient";
		((ControlBase)this.lblClient).WrapText = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.cboClient, "cboClient");
		((TextEditorControlBase)this.cboClient).AlwaysInEditMode = true;
		this.cboClient.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClient).Name = "cboClient";
		((TextEditorControlBase)this.cboClient).ValueChanged += new System.EventHandler(cboClient_ValueChanged);
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		((System.Windows.Forms.Control)(object)this.dtpDate).TabStop = false;
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.txtTotalQty, "txtTotalQty");
		((System.Windows.Forms.Control)(object)this.txtTotalQty).Name = "txtTotalQty";
		((EditorButtonControlBase)this.txtTotalQty).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtTotalQty).TabStop = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.cboQuotations, "cboQuotations");
		this.cboQuotations.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboQuotations).Name = "cboQuotations";
		((TextEditorControlBase)this.cboQuotations).ValueChanged += new System.EventHandler(cboQuotations_ValueChanged);
		resources.ApplyResources(this.lblQuotation, "lblQuotation");
		this.lblQuotation.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblQuotation).Name = "lblQuotation";
		((ControlBase)this.lblQuotation).WrapText = false;
		resources.ApplyResources(this.btnQuotationsSearch, "btnQuotationsSearch");
		((AppearanceBase)val9).Image = resources.GetObject("appearance9.Image");
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.btnQuotationsSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnQuotationsSearch).Name = "btnQuotationsSearch";
		((System.Windows.Forms.Control)(object)this.btnQuotationsSearch).Click += new System.EventHandler(btnQuotationsSearch_Click);
		resources.ApplyResources(this.txtExchangeRate, "txtExchangeRate");
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).Name = "txtExchangeRate";
		resources.ApplyResources(this.lblExchangeRate, "lblExchangeRate");
		this.lblExchangeRate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExchangeRate).Name = "lblExchangeRate";
		((ControlBase)this.lblExchangeRate).WrapText = false;
		resources.ApplyResources(this.lblCurrency, "lblCurrency");
		this.lblCurrency.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCurrency).Name = "lblCurrency";
		((ControlBase)this.lblCurrency).WrapText = false;
		resources.ApplyResources(this.cboCurrency, "cboCurrency");
		((TextEditorControlBase)this.cboCurrency).AlwaysInEditMode = true;
		this.cboCurrency.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCurrency).Name = "cboCurrency";
		resources.ApplyResources(this.cboConsignedTo, "cboConsignedTo");
		((TextEditorControlBase)this.cboConsignedTo).AlwaysInEditMode = true;
		this.cboConsignedTo.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboConsignedTo).Name = "cboConsignedTo";
		((TextEditorControlBase)this.cboConsignedTo).ValueChanged += new System.EventHandler(cboClient_ValueChanged);
		resources.ApplyResources(this.lblConsignedTo, "lblConsignedTo");
		this.lblConsignedTo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblConsignedTo).Name = "lblConsignedTo";
		((ControlBase)this.lblConsignedTo).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboQuotations);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblQuotation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnQuotationsSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblConsignedTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboConsignedTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNetPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNetprice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Name = "frmInvoices";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNetprice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNetPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboConsignedTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblConsignedTo, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnQuotationsSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblQuotation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboQuotations, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExchangeRate, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboQuotations).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboConsignedTo).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
